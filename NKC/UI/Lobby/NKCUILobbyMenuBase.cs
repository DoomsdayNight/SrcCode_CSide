using System;
using System.Collections;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.Lobby
{
	// Token: 0x02000C0B RID: 3083
	public class NKCUILobbyMenuBase : NKCUILobbyMenuButtonBase
	{
		// Token: 0x06008EBC RID: 36540 RVA: 0x00308DB0 File Offset: 0x00306FB0
		public void Init(NKCUILobbyMenuBase.DotEnableConditionFunction conditionFunc, NKCUILobbyMenuBase.OnButton onButton, ContentsType contentsType)
		{
			this.dDotEnableConditionFunction = conditionFunc;
			this.dOnButton = onButton;
			this.m_ContentsType = contentsType;
			this.m_csbtnButton.PointerClick.RemoveAllListeners();
			this.m_csbtnButton.PointerClick.AddListener(new UnityAction(this.OnBtn));
		}

		// Token: 0x06008EBD RID: 36541 RVA: 0x00308E00 File Offset: 0x00307000
		protected override void ContentsUpdate(NKMUserData userData)
		{
			if (userData == null)
			{
				return;
			}
			NKCUILobbyMenuBase.HeadquartersWorkState headquartersWorkState = this.CheckNewUnlockShipBuild();
			NKCUILobbyMenuBase.HeadquartersWorkState headquartersWorkState2 = NKCUILobbyMenuBase.HeadquartersWorkState.Idle;
			if (headquartersWorkState != NKCUILobbyMenuBase.HeadquartersWorkState.Complete)
			{
				headquartersWorkState2 = this.CheckEquipCreationState(userData.m_CraftData);
			}
			NKCUILobbyMenuBase.HeadquartersWorkState headquartersWorkState3 = NKCUILobbyMenuBase.HeadquartersWorkState.Idle;
			if (headquartersWorkState == NKCUILobbyMenuBase.HeadquartersWorkState.Complete || headquartersWorkState2 == NKCUILobbyMenuBase.HeadquartersWorkState.Complete)
			{
				headquartersWorkState3 = NKCUILobbyMenuBase.HeadquartersWorkState.Complete;
			}
			else if (headquartersWorkState2 == NKCUILobbyMenuBase.HeadquartersWorkState.Working)
			{
				headquartersWorkState3 = NKCUILobbyMenuBase.HeadquartersWorkState.Working;
			}
			if (headquartersWorkState3 != NKCUILobbyMenuBase.HeadquartersWorkState.Complete && NKCAlarmManager.CheckScoutNotify(userData))
			{
				headquartersWorkState3 = NKCUILobbyMenuBase.HeadquartersWorkState.Complete;
			}
			if (headquartersWorkState3 != NKCUILobbyMenuBase.HeadquartersWorkState.Complete && NKCAlarmManager.CheckOfficeDormNotify(userData))
			{
				headquartersWorkState3 = NKCUILobbyMenuBase.HeadquartersWorkState.Complete;
			}
			this.UpdateState(headquartersWorkState3);
			this.CheckEvent();
		}

		// Token: 0x06008EBE RID: 36542 RVA: 0x00308E65 File Offset: 0x00307065
		public override void CleanUp()
		{
			base.StopCoroutine(this.CheckState());
		}

		// Token: 0x06008EBF RID: 36543 RVA: 0x00308E74 File Offset: 0x00307074
		private void CheckEvent()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objEvent, false);
				return;
			}
			bool flag = false;
			flag |= NKCCompanyBuff.NeedShowEventMark(nkmuserData.m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_PERSONNAL_NEGOTIATION_CREDIT_DISCOUNT);
			flag |= NKCCompanyBuff.NeedShowEventMark(nkmuserData.m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_FACTORY_CRAFT_CREDIT_DISCOUNT);
			flag |= NKCCompanyBuff.NeedShowEventMark(nkmuserData.m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_FACTORY_ENCHANT_TUNING_CREDIT_DISCOUNT);
			NKCUtil.SetGameobjectActive(this.m_objEvent, flag);
		}

		// Token: 0x06008EC0 RID: 36544 RVA: 0x00308ED8 File Offset: 0x003070D8
		private NKCUILobbyMenuBase.HeadquartersWorkState CheckNewUnlockShipBuild()
		{
			NKCUILobbyMenuBase.HeadquartersWorkState result = NKCUILobbyMenuBase.HeadquartersWorkState.Idle;
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.HANGER_SHIPBUILD, 0, 0))
			{
				return NKCUILobbyMenuBase.HeadquartersWorkState.Idle;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				foreach (NKMShipBuildTemplet nkmshipBuildTemplet in NKMTempletContainer<NKMShipBuildTemplet>.Values)
				{
					if (nkmshipBuildTemplet.ShipBuildUnlockType != NKMShipBuildTemplet.BuildUnlockType.BUT_UNABLE)
					{
						bool flag = false;
						foreach (KeyValuePair<long, NKMUnitData> keyValuePair in nkmuserData.m_ArmyData.m_dicMyShip)
						{
							if (NKMShipManager.IsSameKindShip(keyValuePair.Value.m_UnitID, nkmshipBuildTemplet.Key))
							{
								flag = true;
								break;
							}
						}
						if (NKMShipManager.CanUnlockShip(nkmuserData, nkmshipBuildTemplet))
						{
							string key = string.Format("{0}_{1}_{2}", "SHIP_BUILD_SLOT_CHECK", nkmuserData.m_UserUID, nkmshipBuildTemplet.ShipID);
							if (!flag && !PlayerPrefs.HasKey(key))
							{
								result = NKCUILobbyMenuBase.HeadquartersWorkState.Complete;
								break;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06008EC1 RID: 36545 RVA: 0x00308FF0 File Offset: 0x003071F0
		private NKCUILobbyMenuBase.HeadquartersWorkState CheckEquipCreationState(NKMCraftData creationData)
		{
			NKCUILobbyMenuBase.HeadquartersWorkState result = NKCUILobbyMenuBase.HeadquartersWorkState.Idle;
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_CRAFT, 0, 0))
			{
				return NKCUILobbyMenuBase.HeadquartersWorkState.Idle;
			}
			foreach (KeyValuePair<byte, NKMCraftSlotData> keyValuePair in creationData.SlotList)
			{
				if (keyValuePair.Value.GetState(NKCSynchronizedTime.GetServerUTCTime(0.0)) == NKM_CRAFT_SLOT_STATE.NECSS_COMPLETED)
				{
					result = NKCUILobbyMenuBase.HeadquartersWorkState.Complete;
					return result;
				}
				if (keyValuePair.Value.GetState(NKCSynchronizedTime.GetServerUTCTime(0.0)) == NKM_CRAFT_SLOT_STATE.NECSS_CREATING_NOW)
				{
					result = NKCUILobbyMenuBase.HeadquartersWorkState.Working;
				}
			}
			return result;
		}

		// Token: 0x06008EC2 RID: 36546 RVA: 0x00309090 File Offset: 0x00307290
		private void UpdateState(NKCUILobbyMenuBase.HeadquartersWorkState state)
		{
			switch (state)
			{
			case NKCUILobbyMenuBase.HeadquartersWorkState.Idle:
				NKCUtil.SetGameobjectActive(this.m_objReddot, false);
				return;
			case NKCUILobbyMenuBase.HeadquartersWorkState.Working:
				NKCUtil.SetGameobjectActive(this.m_objReddot, false);
				base.StartCoroutine(this.CheckState());
				return;
			case NKCUILobbyMenuBase.HeadquartersWorkState.Complete:
				NKCUtil.SetGameobjectActive(this.m_objReddot, true);
				return;
			default:
				return;
			}
		}

		// Token: 0x06008EC3 RID: 36547 RVA: 0x003090E5 File Offset: 0x003072E5
		private IEnumerator CheckState()
		{
			NKMUserData userData = NKCScenManager.CurrentUserData();
			if (userData != null)
			{
				while (this.IsWork(userData))
				{
					yield return new WaitForSeconds(1f);
				}
			}
			yield return null;
			yield break;
		}

		// Token: 0x06008EC4 RID: 36548 RVA: 0x003090F4 File Offset: 0x003072F4
		private bool IsWork(NKMUserData userData)
		{
			int num = -1;
			NKCUILobbyMenuBase.HeadquartersWorkState headquartersWorkState = NKCUILobbyMenuBase.HeadquartersWorkState.Idle;
			if (num != 1)
			{
				headquartersWorkState = this.CheckEquipCreationState(userData.m_CraftData);
			}
			bool flag = num == 1 || headquartersWorkState == NKCUILobbyMenuBase.HeadquartersWorkState.Complete;
			NKCUtil.SetGameobjectActive(this.m_objReddot, flag);
			return !flag;
		}

		// Token: 0x06008EC5 RID: 36549 RVA: 0x00309130 File Offset: 0x00307330
		private void OnBtn()
		{
			if (this.m_bLocked)
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.BASE, 0);
				return;
			}
			if (this.dOnButton != null)
			{
				this.dOnButton();
			}
		}

		// Token: 0x04007BCF RID: 31695
		public GameObject m_objEvent;

		// Token: 0x04007BD0 RID: 31696
		public GameObject m_objReddot;

		// Token: 0x04007BD1 RID: 31697
		public NKCUIComStateButton m_csbtnButton;

		// Token: 0x04007BD2 RID: 31698
		private NKCUILobbyMenuBase.DotEnableConditionFunction dDotEnableConditionFunction;

		// Token: 0x04007BD3 RID: 31699
		private NKCUILobbyMenuBase.OnButton dOnButton;

		// Token: 0x020019D2 RID: 6610
		// (Invoke) Token: 0x0600BA3F RID: 47679
		public delegate bool DotEnableConditionFunction(NKMUserData userData);

		// Token: 0x020019D3 RID: 6611
		// (Invoke) Token: 0x0600BA43 RID: 47683
		public delegate void OnButton();

		// Token: 0x020019D4 RID: 6612
		private enum HeadquartersWorkState
		{
			// Token: 0x0400ACFD RID: 44285
			Idle = -1,
			// Token: 0x0400ACFE RID: 44286
			Working,
			// Token: 0x0400ACFF RID: 44287
			Complete
		}
	}
}
