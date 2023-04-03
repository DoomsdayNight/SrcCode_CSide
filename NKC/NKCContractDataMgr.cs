using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Contract;
using Cs.Core.Util;
using Cs.Shared.Time;
using NKM;
using NKM.Contract2;
using NKM.Templet;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200065C RID: 1628
	public class NKCContractDataMgr
	{
		// Token: 0x06003338 RID: 13112 RVA: 0x001004C8 File Offset: 0x000FE6C8
		public static NKM_ERROR_CODE CanTryContract(NKMUserData userData, ContractTempletV2 templet, ContractCostType costType, int tryCount)
		{
			if (userData == null)
			{
				return NKM_ERROR_CODE.NEC_DB_FAIL_USER_DATA;
			}
			if (templet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_CONTRACT_TEMPLET_NULL;
			}
			if (costType == ContractCostType.FreeChance)
			{
				return NKM_ERROR_CODE.NEC_OK;
			}
			MiscItemUnit[] singleTryRequireItems = templet.m_SingleTryRequireItems;
			bool flag = false;
			foreach (MiscItemUnit miscItemUnit in singleTryRequireItems)
			{
				if (userData.m_InventoryData.GetCountMiscItem(miscItemUnit.ItemId) >= miscItemUnit.Count * (long)tryCount)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_RESOURCE;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x0010052C File Offset: 0x000FE72C
		private bool IsContractFinished(ContractTempletBase templet)
		{
			if (templet is ContractTempletV2)
			{
				ContractTempletV2 contractTempletV = templet as ContractTempletV2;
				if (contractTempletV != null)
				{
					NKMContractState contractState = this.GetContractState(contractTempletV.Key);
					if (contractState != null)
					{
						if (contractTempletV.m_TotalLimit > 0 && contractState.totalUseCount >= contractTempletV.m_TotalLimit)
						{
							return true;
						}
						if (contractTempletV.m_ContractGetUnitClose && this.GetContractBonusResetCnt(contractTempletV.Key) > 0)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x00100590 File Offset: 0x000FE790
		public bool CheckOpenCond(ContractTempletBase templet)
		{
			if (templet == null)
			{
				return false;
			}
			if (!templet.EnableByTag)
			{
				return false;
			}
			if (NKCSynchronizedTime.IsFinished(templet.GetDateEndUtc()) || !NKCSynchronizedTime.IsEventTime(templet.GetDateStartUtc(), templet.GetDateEndUtc()))
			{
				return false;
			}
			if (templet is SelectableContractTemplet)
			{
				NKMSelectableContractState selectableContractState = this.GetSelectableContractState();
				if (selectableContractState != null && !selectableContractState.isActive)
				{
					return false;
				}
			}
			if (templet is ContractTempletV2)
			{
				ContractTempletV2 contractTempletV = templet as ContractTempletV2;
				if (contractTempletV != null)
				{
					NKMContractState contractState = this.GetContractState(contractTempletV.Key);
					if (contractState != null)
					{
						if (this.IsContractFinished(contractTempletV))
						{
							return false;
						}
						if (contractTempletV.m_DailyLimit > 0 && contractState.dailyUseCount >= contractTempletV.m_DailyLimit)
						{
							if ((contractTempletV.GetDateEndUtc() - contractState.nextResetDate).TotalSeconds <= 0.0)
							{
								return false;
							}
						}
						else if (contractTempletV.IsFreeOnlyContract && !contractTempletV.m_resetFreeCount && contractTempletV.m_FreeTryCnt * contractTempletV.m_freeCountDays <= contractState.totalUseCount)
						{
							return false;
						}
					}
					if (contractTempletV.IsInstantContract && NKCSynchronizedTime.IsFinished(NKCSynchronizedTime.ToUtcTime(this.GetInstantContractEndDateTime(templet.Key))))
					{
						return false;
					}
					if (contractTempletV.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_OPERATOR && (!NKMContentsVersionManager.HasTag("OPERATOR") || !NKCContentManager.IsContentsUnlocked(ContentsType.OPERATOR, 0, 0)))
					{
						return false;
					}
				}
			}
			switch (templet.OpenCondition)
			{
			case CONTRACT_CONDITION.CONTRACT_FINISH:
			{
				ContractTempletV2 contractTempletV2 = ContractTempletV2.Find(templet.OpenConditionValue);
				if (contractTempletV2 != null)
				{
					if (this.IsContractFinished(contractTempletV2))
					{
						return true;
					}
				}
				else if (SelectableContractTemplet.Find(templet.Key) != null)
				{
					NKMSelectableContractState selectableContractState2 = this.GetSelectableContractState();
					return selectableContractState2 == null || selectableContractState2.contractId != templet.Key || selectableContractState2.isActive;
				}
				Debug.Log(string.Format("name : {0}에 CONTRACT_FINISH - OpenConditionValue({1})를 확인할 수 없습니다.", templet.GetContractName(), templet.OpenConditionValue));
				return false;
			}
			case CONTRACT_CONDITION.WARFARE_CLEARED:
			{
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(templet.OpenConditionValue);
				if (nkmwarfareTemplet != null)
				{
					return NKCScenManager.GetScenManager().GetMyUserData().CheckWarfareClear(nkmwarfareTemplet.m_WarfareStrID);
				}
				Debug.Log(string.Format("name : {0}에 WARFARE_CLEARED - OpenConditionValue({1})를 확인할 수 없습니다.", templet.GetContractName(), templet.OpenConditionValue));
				return false;
			}
			case CONTRACT_CONDITION.STAGE_CLEARED:
			{
				NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(templet.OpenConditionValue);
				if (nkmstageTempletV != null)
				{
					return NKCScenManager.GetScenManager().GetMyUserData().CheckStageCleared(nkmstageTempletV.Key);
				}
				Debug.Log(string.Format("name : {0}에 STAGE_CLEARED - OpenConditionValue({1})를 확인할 수 없습니다.", templet.GetContractName(), templet.OpenConditionValue));
				return false;
			}
			}
			return true;
		}

		// Token: 0x0600333B RID: 13115 RVA: 0x001007EF File Offset: 0x000FE9EF
		public void SetContractState(List<NKMContractState> contractState)
		{
			this.m_lstContractState.Clear();
			if (contractState.Count <= 0)
			{
				return;
			}
			this.m_lstContractState = contractState;
			this.UpdateContractFreeTryState();
		}

		// Token: 0x0600333C RID: 13116 RVA: 0x00100814 File Offset: 0x000FEA14
		public void UpdateContractState(NKMContractState contractState)
		{
			if (contractState != null)
			{
				bool flag = false;
				foreach (NKMContractState nkmcontractState in this.m_lstContractState)
				{
					if (nkmcontractState.contractId == contractState.contractId)
					{
						nkmcontractState.nextResetDate = contractState.nextResetDate;
						nkmcontractState.remainFreeChance = contractState.remainFreeChance;
						nkmcontractState.isActive = contractState.isActive;
						nkmcontractState.dailyUseCount = contractState.dailyUseCount;
						nkmcontractState.totalUseCount = contractState.totalUseCount;
						nkmcontractState.bonusCandidate = contractState.bonusCandidate;
						flag = true;
						Debug.Log(string.Format("id : {0} / remainFreeChance : {1} / totalUseCount : {2}", contractState.contractId, nkmcontractState.remainFreeChance, nkmcontractState.totalUseCount));
						break;
					}
				}
				if (!flag)
				{
					this.m_lstContractState.Add(contractState);
				}
				this.UpdateContractFreeTryState();
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x0600333D RID: 13117 RVA: 0x00100910 File Offset: 0x000FEB10
		public bool PossibleFreeContract
		{
			get
			{
				return this.m_bIsPossibleFreeContract;
			}
		}

		// Token: 0x0600333E RID: 13118 RVA: 0x00100918 File Offset: 0x000FEB18
		private void UpdateContractFreeTryState()
		{
			this.m_bCanFreeChance = false;
			this.m_FreeContractResetTime = DateTime.MinValue;
			DateTime recent = ServiceTime.Recent;
			foreach (ContractTempletV2 contractTempletV in ContractTempletV2.Values)
			{
				if (NKCSynchronizedTime.IsEventTime(contractTempletV.GetDateStartUtc(), contractTempletV.GetDateEndUtc()) && contractTempletV.m_FreeTryCnt > 0)
				{
					NKMContractState contractState = this.GetContractState(contractTempletV.Key);
					if (contractState == null || contractState.remainFreeChance > 0 || (!NKCSynchronizedTime.IsFinished(contractTempletV.GetDateStartUtc().AddDays((double)contractTempletV.m_freeCountDays)) && NKCSynchronizedTime.IsFinished(contractState.nextResetDate)))
					{
						this.m_bIsPossibleFreeContract = true;
						this.m_bCanFreeChance = true;
					}
				}
			}
			this.m_FreeContractResetTime = this.GetFixedResetTime();
		}

		// Token: 0x0600333F RID: 13119 RVA: 0x001009F0 File Offset: 0x000FEBF0
		private DateTime GetFixedResetTime()
		{
			return NKMTime.GetNextResetTime(NKCSynchronizedTime.GetServerUTCTime(0.0), NKMTime.TimePeriod.Day);
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x00100A08 File Offset: 0x000FEC08
		public bool IsActiveContrctConfirmation(int contractId)
		{
			NKMContractState contractState = this.GetContractState(contractId);
			if (contractState != null)
			{
				return contractState.isActive;
			}
			ContractTempletV2 contractTempletV = ContractTempletV2.Find(contractId);
			return contractTempletV != null && contractTempletV.m_ContractBonusCountGroupID != 0;
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x00100A3C File Offset: 0x000FEC3C
		public int GetRemainFreeChangeCnt(int contractId)
		{
			NKMContractState contractState = this.GetContractState(contractId);
			if (contractState != null)
			{
				return contractState.remainFreeChance;
			}
			ContractTempletV2 contractTempletV = ContractTempletV2.Find(contractId);
			if (contractTempletV == null)
			{
				return 0;
			}
			if (contractTempletV.m_resetFreeCount)
			{
				return contractTempletV.m_FreeTryCnt;
			}
			DateTime current = contractTempletV.EventIntervalTemplet.CalcStartDate(ServiceTime.Recent);
			int num = 1;
			DateTime dateTime = DailyReset.CalcNextReset(current);
			while (dateTime <= ServiceTime.Recent)
			{
				num++;
				dateTime += TimeSpan.FromDays(1.0);
			}
			int num2 = Math.Min(num, contractTempletV.m_freeCountDays);
			return contractTempletV.m_FreeTryCnt * num2;
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x00100ACD File Offset: 0x000FECCD
		public bool IsHasContractStateData(int contractId)
		{
			return this.GetContractState(contractId) != null;
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x00100ADC File Offset: 0x000FECDC
		public bool IsActiveNextFreeChance(int contractId)
		{
			NKMContractState contractState = this.GetContractState(contractId);
			return contractState != null && NKCSynchronizedTime.GetTimeLeft(contractState.nextResetDate).Ticks <= 0L;
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x00100B10 File Offset: 0x000FED10
		public DateTime GetNextResetTime(int contractID)
		{
			NKMContractState contractState = this.GetContractState(contractID);
			if (contractState != null)
			{
				return contractState.nextResetDate;
			}
			return DateTime.MinValue;
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x00100B34 File Offset: 0x000FED34
		private NKMContractState GetContractState(int contractId)
		{
			if (this.m_lstContractState == null)
			{
				return null;
			}
			return this.m_lstContractState.Find((NKMContractState x) => x.contractId == contractId);
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x00100B70 File Offset: 0x000FED70
		public List<int> GetCurSelectableUnitList(int contractID)
		{
			NKMContractState contractState = this.GetContractState(contractID);
			if (contractState != null)
			{
				return contractState.bonusCandidate;
			}
			return new List<int>();
		}

		// Token: 0x06003347 RID: 13127 RVA: 0x00100B94 File Offset: 0x000FED94
		public bool IsPossibleFreeChance()
		{
			return this.m_bCanFreeChance || this.IsActiveFreeContract();
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x00100BAC File Offset: 0x000FEDAC
		private bool IsActiveFreeContract()
		{
			bool result = false;
			DateTime recent = ServiceTime.Recent;
			foreach (ContractTempletV2 contractTempletV in ContractTempletV2.Values)
			{
				if (NKCSynchronizedTime.IsEventTime(contractTempletV.GetDateStartUtc(), contractTempletV.GetDateEndUtc()) && contractTempletV.m_FreeTryCnt > 0)
				{
					NKMContractState contractState = this.GetContractState(contractTempletV.Key);
					if (contractState == null || contractState.remainFreeChance > 0 || (!NKCSynchronizedTime.IsFinished(contractTempletV.GetDateStartUtc().AddDays((double)contractTempletV.m_freeCountDays)) && NKCSynchronizedTime.IsFinished(contractState.nextResetDate)))
					{
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x00100C5C File Offset: 0x000FEE5C
		public bool hasContractLimit(int contractID)
		{
			ContractTempletV2 contractTempletV = ContractTempletV2.Find(contractID);
			return contractTempletV != null && (contractTempletV.m_TotalLimit > 0 || contractTempletV.m_DailyLimit > 0);
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x00100C8C File Offset: 0x000FEE8C
		public int GetContractLimitCnt(int contractID)
		{
			int num = -1;
			ContractTempletV2 contractTempletV = ContractTempletV2.Find(contractID);
			if (contractTempletV != null)
			{
				NKMContractState contractState = this.GetContractState(contractID);
				if (contractTempletV.m_TotalLimit > 0)
				{
					num = contractTempletV.m_TotalLimit;
					if (contractState != null)
					{
						num -= contractState.totalUseCount;
					}
				}
				else if (contractTempletV.m_DailyLimit > 0)
				{
					DateTime recent = ServiceTime.Recent;
					return this.CalculateLimitContractCount(recent, contractTempletV);
				}
			}
			return num;
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x00100CE4 File Offset: 0x000FEEE4
		public bool IsDailyContractLimit(int contractID)
		{
			ContractTempletV2 templet = ContractTempletV2.Find(contractID);
			return this.IsDailyContractLimit(templet);
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x00100D00 File Offset: 0x000FEF00
		public bool IsDailyContractLimit(ContractTempletV2 templet)
		{
			if (templet != null)
			{
				NKMContractState contractState = this.GetContractState(templet.Key);
				if (contractState != null && templet.m_DailyLimit > 0 && !this.IsActiveNextFreeChance(templet.Key))
				{
					return templet.m_DailyLimit <= contractState.dailyUseCount;
				}
			}
			return false;
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x00100D4A File Offset: 0x000FEF4A
		public DateTime GetNextResetTime()
		{
			if (this.IsNextResetTimeOver())
			{
				return this.GetFixedResetTime();
			}
			return this.m_FreeContractResetTime;
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x00100D61 File Offset: 0x000FEF61
		private bool IsNextResetTimeOver()
		{
			return NKCSynchronizedTime.IsFinished(this.m_FreeContractResetTime);
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x00100D6E File Offset: 0x000FEF6E
		public void SetContractBonusState(List<NKMContractBonusState> contractBonus)
		{
			this.m_lstBonusState.Clear();
			if (contractBonus.Count <= 0)
			{
				return;
			}
			this.m_lstBonusState = contractBonus;
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x00100D8C File Offset: 0x000FEF8C
		public void UpdateContractBonusState(NKMContractBonusState contractBonus)
		{
			if (contractBonus == null)
			{
				return;
			}
			bool flag = false;
			for (int i = 0; i < this.m_lstBonusState.Count; i++)
			{
				if (this.m_lstBonusState[i].bonusGroupId == contractBonus.bonusGroupId)
				{
					this.m_lstBonusState[i].useCount = contractBonus.useCount;
					this.m_lstBonusState[i].resetCount = contractBonus.resetCount;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this.m_lstBonusState.Add(contractBonus);
			}
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x00100E10 File Offset: 0x000FF010
		public int GetContractBonusCnt(int groupId)
		{
			NKMContractBonusState nkmcontractBonusState = this.m_lstBonusState.Find((NKMContractBonusState x) => x.bonusGroupId == groupId);
			if (nkmcontractBonusState != null)
			{
				return nkmcontractBonusState.useCount;
			}
			return 0;
		}

		// Token: 0x06003352 RID: 13138 RVA: 0x00100E50 File Offset: 0x000FF050
		private int GetContractBonusResetCnt(int groupId)
		{
			NKMContractBonusState nkmcontractBonusState = this.m_lstBonusState.Find((NKMContractBonusState x) => x.bonusGroupId == groupId);
			if (nkmcontractBonusState != null)
			{
				return nkmcontractBonusState.resetCount;
			}
			return 0;
		}

		// Token: 0x06003353 RID: 13139 RVA: 0x00100E8D File Offset: 0x000FF08D
		public void SetSelectableContractState(NKMSelectableContractState state)
		{
			this.m_SelectableContractState = state;
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x00100E96 File Offset: 0x000FF096
		public NKMSelectableContractState GetSelectableContractState()
		{
			return this.m_SelectableContractState;
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x00100E9E File Offset: 0x000FF09E
		public int GetSelectableContractChangeCnt(int ContractID)
		{
			if (this.m_SelectableContractState != null && this.m_SelectableContractState.contractId == ContractID)
			{
				return this.m_SelectableContractState.unitPoolChangeCount;
			}
			return 0;
		}

		// Token: 0x06003356 RID: 13142 RVA: 0x00100EC4 File Offset: 0x000FF0C4
		public int GetContractTryCnt(int bonusGroupID)
		{
			foreach (NKMContractBonusState nkmcontractBonusState in this.m_lstBonusState)
			{
				if (nkmcontractBonusState.bonusGroupId == bonusGroupID)
				{
					return nkmcontractBonusState.useCount;
				}
			}
			return 0;
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x00100F28 File Offset: 0x000FF128
		public bool IsActiveNewFreeChance()
		{
			foreach (ContractTempletV2 contractTempletV in (from e in ContractTempletV2.Values
			where this.CheckOpenCond(e)
			orderby e.GetOrder(), e.GetDateStart()
			select e).ToList<ContractTempletV2>())
			{
				if (!this.IsHasContractStateData(contractTempletV.Key) && NKCSynchronizedTime.IsEventTime(contractTempletV.m_DateStartUtc, contractTempletV.m_DateEndUtc) && contractTempletV.m_FreeTryCnt > 0)
				{
					this.UpdateContractFreeTryState();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003358 RID: 13144 RVA: 0x00101008 File Offset: 0x000FF208
		private int CalculateLimitContractCount(DateTime current, ContractTempletV2 templet)
		{
			NKMContractState nkmcontractState = this.m_lstContractState.Find((NKMContractState e) => e.contractId == templet.Key);
			if (nkmcontractState == null)
			{
				return templet.m_DailyLimit;
			}
			if (DailyReset.IsOutOfDate(current, this.GetNextResetTime(templet.Key)))
			{
				return templet.m_DailyLimit;
			}
			return templet.m_DailyLimit - nkmcontractState.dailyUseCount;
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x00101080 File Offset: 0x000FF280
		public void UpdateInstantContract(List<NKMInstantContract> InstantContract)
		{
			this.m_lstInstantContract = InstantContract;
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x0010108C File Offset: 0x000FF28C
		public DateTime GetInstantContractEndDateTime(int iCntractID)
		{
			if (this.m_lstInstantContract != null)
			{
				foreach (NKMInstantContract nkminstantContract in this.m_lstInstantContract)
				{
					if (nkminstantContract.contractId == iCntractID)
					{
						return nkminstantContract.endDate;
					}
				}
			}
			return default(DateTime);
		}

		// Token: 0x040031B9 RID: 12729
		private List<NKMContractState> m_lstContractState = new List<NKMContractState>();

		// Token: 0x040031BA RID: 12730
		private bool m_bIsPossibleFreeContract;

		// Token: 0x040031BB RID: 12731
		private DateTime m_FreeContractResetTime;

		// Token: 0x040031BC RID: 12732
		private bool m_bCanFreeChance;

		// Token: 0x040031BD RID: 12733
		private List<NKMContractBonusState> m_lstBonusState = new List<NKMContractBonusState>();

		// Token: 0x040031BE RID: 12734
		private NKMSelectableContractState m_SelectableContractState;

		// Token: 0x040031BF RID: 12735
		private List<NKMInstantContract> m_lstInstantContract;
	}
}
