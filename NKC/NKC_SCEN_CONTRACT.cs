using System;
using System.Collections.Generic;
using ClientPacket.Contract;
using NKC.UI;
using NKC.UI.Collection;
using NKC.UI.Contract;
using NKC.UI.Module;
using NKC.UI.Result;
using NKM;
using NKM.Contract2;
using NKM.Event;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006FF RID: 1791
	public class NKC_SCEN_CONTRACT : NKC_SCEN_BASIC
	{
		// Token: 0x06004645 RID: 17989 RVA: 0x001557B0 File Offset: 0x001539B0
		public NKC_SCEN_CONTRACT()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_CONTRACT;
		}

		// Token: 0x06004646 RID: 17990 RVA: 0x001557CA File Offset: 0x001539CA
		public override void ScenLoadUIStart()
		{
			if (!NKCUIManager.IsValid(this.m_UIContractData))
			{
				this.m_UIContractData = NKCUIManager.OpenNewInstanceAsync<NKCUIContractV3>("AB_UI_NKM_UI_CONTRACT_V2", "NKM_UI_CONTRACT_V2", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontCommon), null);
			}
			base.ScenLoadUIStart();
		}

		// Token: 0x06004647 RID: 17991 RVA: 0x001557FC File Offset: 0x001539FC
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			if (this.m_NKCUIContract == null)
			{
				if (this.m_UIContractData != null && this.m_UIContractData.CheckLoadAndGetInstance<NKCUIContractV3>(out this.m_NKCUIContract))
				{
					this.m_NKCUIContract.Init();
					return;
				}
				Debug.LogError("Error - NKC_SCEN_CONTRACT.ScenLoadComplete() : UI Load Failed!");
			}
		}

		// Token: 0x06004648 RID: 17992 RVA: 0x0015584E File Offset: 0x00153A4E
		public override void ScenStart()
		{
			base.ScenStart();
			this.Open();
		}

		// Token: 0x06004649 RID: 17993 RVA: 0x0015585C File Offset: 0x00153A5C
		public override void ScenEnd()
		{
			base.ScenEnd();
			this.Close();
			this.m_NKCUIContract = null;
			NKCUIManager.LoadedUIData uicontractData = this.m_UIContractData;
			if (uicontractData != null)
			{
				uicontractData.CloseInstance();
			}
			this.m_UIContractData = null;
			NKCUICollectionUnitInfo.CheckInstanceAndClose();
			NKCUICollectionOperatorInfo.CheckInstanceAndClose();
			NKCCamera.GetTrackingPos().SetPause(false);
		}

		// Token: 0x0600464A RID: 17994 RVA: 0x001558A9 File Offset: 0x00153AA9
		public void Open()
		{
			if (this.m_NKCUIContract != null)
			{
				this.m_NKCUIContract.Open(this.m_sReserveContractStrID);
			}
			this.m_sReserveContractStrID = "";
		}

		// Token: 0x0600464B RID: 17995 RVA: 0x001558D5 File Offset: 0x00153AD5
		public void Close()
		{
			if (this.m_NKCUIContract != null)
			{
				this.m_NKCUIContract.Close();
			}
		}

		// Token: 0x0600464C RID: 17996 RVA: 0x001558F0 File Offset: 0x00153AF0
		public void SetReserveContractID(string reserveStrID)
		{
			this.m_sReserveContractStrID = reserveStrID;
		}

		// Token: 0x0600464D RID: 17997 RVA: 0x001558F9 File Offset: 0x00153AF9
		public override bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x0600464E RID: 17998 RVA: 0x001558FC File Offset: 0x00153AFC
		public void OnUIForceRefresh(bool bForce = false)
		{
			if (this.m_NKCUIContract != null)
			{
				this.m_NKCUIContract.ResetContractUI(bForce);
			}
		}

		// Token: 0x0600464F RID: 17999 RVA: 0x00155918 File Offset: 0x00153B18
		public void OnRecv(NKMPacket_CONTRACT_ACK sPacket)
		{
			ContractTempletV2 contractTempletV = ContractTempletV2.Find(sPacket.contractId);
			if (contractTempletV == null)
			{
				return;
			}
			if (sPacket.rewardData == null)
			{
				sPacket.rewardData = new NKMRewardData();
			}
			this.ContractComplet(sPacket.units, sPacket.operators, sPacket.rewardData.MiscItemDataList, false, contractTempletV.MissionCountIgnore, sPacket.requestCount, contractTempletV.EventCollectionMergeID);
		}

		// Token: 0x06004650 RID: 18000 RVA: 0x00155978 File Offset: 0x00153B78
		public void OnRecv(NKMPacket_SELECTABLE_CONTRACT_CONFIRM_ACK sPacket)
		{
			if (sPacket.units != null && sPacket.units.Count > 0)
			{
				this.ContractComplet(sPacket.units, null, null, true, false, 0, 0);
			}
		}

		// Token: 0x06004651 RID: 18001 RVA: 0x001559A4 File Offset: 0x00153BA4
		private void ContractComplet(List<NKMUnitData> lstUnit, List<NKMOperator> lstOper, List<NKMItemMiscData> lstMisc = null, bool bSelectableContract = false, bool bMissionCountIgnore = false, int requestCount = 0, int eventMergeID = 0)
		{
			if (!(this.m_NKCUIContract != null))
			{
				using (IEnumerator<NKMEventCollectionIndexTemplet> enumerator = NKMTempletContainer<NKMEventCollectionIndexTemplet>.Values.GetEnumerator())
				{
					NKCUIResult.OnClose <>9__2;
					while (enumerator.MoveNext())
					{
						NKMEventCollectionIndexTemplet collectionIdxTemplet = enumerator.Current;
						if (collectionIdxTemplet.IsOpen && collectionIdxTemplet.IsOpen && collectionIdxTemplet.CollectionMergeId == eventMergeID)
						{
							if (string.IsNullOrEmpty(collectionIdxTemplet.EventContractAnimationPrefabID))
							{
								break;
							}
							NKCUIModuleContractResult moduleContractResult = NKCUIModuleContractResult.MakeInstance(collectionIdxTemplet.EventContractAnimationPrefabID, collectionIdxTemplet.EventContractAnimationPrefabID);
							if (moduleContractResult != null)
							{
								moduleContractResult.Open(lstUnit, delegate
								{
									NKMRewardData nkmrewardData2 = new NKMRewardData();
									nkmrewardData2.SetUnitData(lstUnit);
									if (lstMisc != null)
									{
										nkmrewardData2.SetMiscItemData(lstMisc);
									}
									moduleContractResult.Close();
									NKCUIModuleContractResult.CheckInstanceAndClose();
									moduleContractResult = null;
									if (string.IsNullOrEmpty(collectionIdxTemplet.EventResultPrefabID))
									{
										NKCUIResult instance = NKCUIResult.Instance;
										NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
										NKMRewardData rewardData = nkmrewardData2;
										string get_STRING_CONTRACT_SLOT_UNIT = NKCUtilString.GET_STRING_CONTRACT_SLOT_UNIT;
										NKCUIResult.OnClose onClose;
										if ((onClose = <>9__2) == null)
										{
											onClose = (<>9__2 = delegate()
											{
												this.ClosedReward(true);
											});
										}
										instance.OpenBoxGain(armyData, rewardData, get_STRING_CONTRACT_SLOT_UNIT, onClose, true, requestCount, false);
										return;
									}
									NKCUIPopupModuleResult moduleResultPopup = NKCUIPopupModuleResult.MakeInstance(collectionIdxTemplet.EventResultPrefabID, collectionIdxTemplet.EventResultPrefabID);
									if (null != moduleResultPopup)
									{
										moduleResultPopup.Init();
										moduleResultPopup.Open(nkmrewardData2, delegate()
										{
											moduleResultPopup.Close();
											NKCUIPopupModuleResult.CheckInstanceAndClose();
											moduleResultPopup = null;
											this.ClosedReward(true);
										});
										return;
									}
								});
								break;
							}
							break;
						}
					}
				}
				return;
			}
			NKMRewardData nkmrewardData = new NKMRewardData();
			nkmrewardData.SetUnitData(lstUnit);
			if (lstOper != null)
			{
				nkmrewardData.SetOperatorList(lstOper);
			}
			if (lstMisc != null)
			{
				nkmrewardData.SetMiscItemData(lstMisc);
			}
			this.m_RewardUnit = nkmrewardData;
			if (bSelectableContract)
			{
				this.OpenReward(requestCount, false);
				return;
			}
			NKM_UNIT_GRADE nkm_UNIT_GRADE = NKM_UNIT_GRADE.NUG_N;
			bool bAwaken = false;
			if (this.m_RewardUnit.UnitDataList != null)
			{
				foreach (NKMUnitData nkmunitData in this.m_RewardUnit.UnitDataList)
				{
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(nkmunitData.m_UnitID);
					if (unitTempletBase.m_NKM_UNIT_GRADE > nkm_UNIT_GRADE)
					{
						nkm_UNIT_GRADE = unitTempletBase.m_NKM_UNIT_GRADE;
					}
					if (unitTempletBase.m_bAwaken)
					{
						bAwaken = true;
					}
				}
			}
			if (this.m_RewardUnit.OperatorList != null)
			{
				foreach (NKMOperator nkmoperator in this.m_RewardUnit.OperatorList)
				{
					NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(nkmoperator.id);
					if (unitTempletBase2.m_NKM_UNIT_GRADE > nkm_UNIT_GRADE)
					{
						nkm_UNIT_GRADE = unitTempletBase2.m_NKM_UNIT_GRADE;
					}
					if (unitTempletBase2.m_bAwaken)
					{
						bAwaken = true;
					}
				}
			}
			if (!bMissionCountIgnore)
			{
				NKCUIContractSequence.Instance.Open(nkm_UNIT_GRADE, bAwaken, delegate
				{
					this.OpenReward(requestCount, true);
				});
				return;
			}
			this.OpenReward(requestCount, true);
		}

		// Token: 0x06004652 RID: 18002 RVA: 0x00155C40 File Offset: 0x00153E40
		public void OpenReward(int requestCount, bool bDisplayGetUnit = true)
		{
			this.bWaitEvent = false;
			if (NKCGameEventManager.IsWaiting())
			{
				this.bWaitEvent = true;
				NKCUIResult.Instance.OpenBoxGain(NKCScenManager.CurrentUserData().m_ArmyData, this.m_RewardUnit, NKCUtilString.GET_STRING_CONTRACT_SLOT_UNIT, delegate()
				{
					this.ClosedReward(bDisplayGetUnit);
				}, bDisplayGetUnit, requestCount, false);
				return;
			}
			NKCUIResult.Instance.OpenBoxGain(NKCScenManager.CurrentUserData().m_ArmyData, this.m_RewardUnit, NKCUtilString.GET_STRING_CONTRACT_SLOT_UNIT, delegate()
			{
				this.ClosedReward(bDisplayGetUnit);
			}, bDisplayGetUnit, requestCount, false);
		}

		// Token: 0x06004653 RID: 18003 RVA: 0x00155CDD File Offset: 0x00153EDD
		private void ClosedReward(bool bDisplayGetUnit = true)
		{
			if (this.bWaitEvent)
			{
				NKCGameEventManager.WaitFinished();
			}
			if (!bDisplayGetUnit)
			{
				NKCUIContractSelection.Instance.Close();
			}
			NKCUIContractV3 nkcuicontract = this.m_NKCUIContract;
			if (nkcuicontract == null)
			{
				return;
			}
			nkcuicontract.OnContractCompleteAck();
		}

		// Token: 0x06004654 RID: 18004 RVA: 0x00155D0C File Offset: 0x00153F0C
		public void OnRecv(NKMPacket_SELECTABLE_CONTRACT_CHANGE_POOL_ACK sPacket)
		{
			if (this.m_NKCUIContract != null)
			{
				this.m_NKCUIContract.UpdateChildUI();
			}
			List<NKMUnitData> lstDummyData = new List<NKMUnitData>();
			foreach (int unitID in sPacket.selectableContractState.unitIdList)
			{
				NKMUnitData nkmunitData = NKCUtil.MakeDummyUnit(unitID, false);
				if (nkmunitData != null)
				{
					lstDummyData.Add(nkmunitData);
				}
			}
			NKM_UNIT_GRADE nkm_UNIT_GRADE = NKM_UNIT_GRADE.NUG_N;
			bool bAwaken = false;
			foreach (NKMUnitData nkmunitData2 in lstDummyData)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(nkmunitData2.m_UnitID);
				if (unitTempletBase.m_NKM_UNIT_GRADE > nkm_UNIT_GRADE)
				{
					nkm_UNIT_GRADE = unitTempletBase.m_NKM_UNIT_GRADE;
				}
				if (unitTempletBase.m_bAwaken)
				{
					bAwaken = true;
				}
			}
			if (sPacket.selectableContractState.unitPoolChangeCount <= 1)
			{
				NKCUIGameResultGetUnit.NKCUIGRGetUnitCallBack <>9__2;
				NKCUIContractSequence.Instance.Open(nkm_UNIT_GRADE, bAwaken, delegate
				{
					List<NKMUnitData> lstDummyData = lstDummyData;
					NKCUIGameResultGetUnit.NKCUIGRGetUnitCallBack callBack;
					if ((callBack = <>9__2) == null)
					{
						callBack = (<>9__2 = delegate()
						{
							NKCUIContractSelection.Instance.Open(sPacket.selectableContractState);
						});
					}
					NKCUIGameResultGetUnit.ShowNewUnitGetUIForSelectableContract(lstDummyData, callBack);
				});
				return;
			}
			NKCUIGameResultGetUnit.ShowNewUnitGetUIForSelectableContract(lstDummyData, delegate
			{
				NKCUIContractSelection.Instance.Open(sPacket.selectableContractState);
			});
		}

		// Token: 0x06004655 RID: 18005 RVA: 0x00155E58 File Offset: 0x00154058
		public void SelectRecruitBanner(string contractStrID)
		{
			if (this.m_NKCUIContract != null && this.m_NKCUIContract.IsOpen)
			{
				this.m_NKCUIContract.SelectRecruitBanner(contractStrID);
			}
		}

		// Token: 0x0400376D RID: 14189
		public const double CONTRACT_SLOT_CLIENT_TIME_DELAY_SECONDS = -1.0;

		// Token: 0x0400376E RID: 14190
		private NKCUIContractV3 m_NKCUIContract;

		// Token: 0x0400376F RID: 14191
		private NKCUIManager.LoadedUIData m_UIContractData;

		// Token: 0x04003770 RID: 14192
		private string m_sReserveContractStrID = "";

		// Token: 0x04003771 RID: 14193
		private NKMRewardData m_RewardUnit;

		// Token: 0x04003772 RID: 14194
		private bool bWaitEvent;
	}
}
