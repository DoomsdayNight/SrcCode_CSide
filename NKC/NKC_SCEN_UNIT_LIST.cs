using System;
using System.Collections.Generic;
using NKC.UI;
using NKM;
using NKM.Templet;

namespace NKC
{
	// Token: 0x02000730 RID: 1840
	public class NKC_SCEN_UNIT_LIST : NKC_SCEN_BASIC
	{
		// Token: 0x06004943 RID: 18755 RVA: 0x00160A04 File Offset: 0x0015EC04
		public NKC_SCEN_UNIT_LIST()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_UNIT_LIST;
		}

		// Token: 0x06004944 RID: 18756 RVA: 0x00160A14 File Offset: 0x0015EC14
		public override void ScenStart()
		{
			base.ScenStart();
			NKCUIUnitSelectList.UnitSelectListOptions options = new NKCUIUnitSelectList.UnitSelectListOptions(NKM_UNIT_TYPE.NUT_NORMAL, false, NKM_DECK_TYPE.NDT_DAILY, NKCUIUnitSelectList.eUnitSelectListMode.Normal, true);
			options.bShowUnitShipChangeMenu = true;
			options.bEnableLockUnitSystem = true;
			options.bEnableRemoveUnitSystem = true;
			options.dOnAutoSelectFilter = new NKCUIUnitSelectList.UnitSelectListOptions.OnAutoSelectFilter(this.FilterRemoveAuto);
			options.bUseRemoveSmartAutoSelect = true;
			options.bShowHideDeckedUnitMenu = false;
			options.bCanSelectUnitInMission = true;
			options.m_SortOptions.bIncludeSeizure = true;
			options.m_SortOptions.bIgnoreWorldMapLeader = true;
			options.m_SortOptions.lstDeckTypeOrder = new List<NKM_DECK_TYPE>
			{
				NKM_DECK_TYPE.NDT_NORMAL
			};
			options.m_OperatorSortOptions.SetBuildOption(true, new BUILD_OPTIONS[]
			{
				BUILD_OPTIONS.INCLUDE_SEIZURE,
				BUILD_OPTIONS.IGNORE_WORLD_MAP_LEADER
			});
			options.strUpsideMenuName = NKCUtilString.GET_STRING_MANAGEMENT;
			options.dOnClose = new NKCUIUnitSelectList.UnitSelectListOptions.OnClose(this.MoveToHomeScene);
			options.bPushBackUnselectable = false;
			options.setUnitFilterCategory = NKCUnitSortSystem.setDefaultUnitFilterCategory;
			options.setUnitSortCategory = NKCUnitSortSystem.setDefaultUnitSortCategory;
			options.setShipFilterCategory = NKCUnitSortSystem.setDefaultShipFilterCategory;
			options.setShipSortCategory = NKCUnitSortSystem.setDefaultShipSortCategory;
			options.ShopShortcutTargetTab = "TAB_EXCHANGE_REMOVE_CARD";
			options.setOperatorFilterCategory = NKCPopupFilterOperator.MakeDefaultFilterCategory(NKCPopupFilterOperator.FILTER_OPEN_TYPE.NORMAL);
			options.setOperatorSortCategory = NKCOperatorSortSystem.setDefaultOperatorSortCategory;
			options.m_bUseFavorite = true;
			NKCUIUnitSelectList.Instance.Open(options, new NKCUIUnitSelectList.OnUnitSelectCommand(NKC_SCEN_UNIT_LIST.OpenUnitData), new NKCUIUnitSelectList.OnUnitSortList(NKC_SCEN_UNIT_LIST.OnUnitSortList), new NKCUIUnitSelectList.OnOperatorSortList(NKC_SCEN_UNIT_LIST.OnOperatorSortList), null, null);
			if (this.m_eUIOpenReserve != NKC_SCEN_UNIT_LIST.eUIOpenReserve.Nothing)
			{
				this.OpenUnitData(new List<long>
				{
					this.m_reserveUnitUID
				}, this.m_eUIOpenReserve);
				this.m_eUIOpenReserve = NKC_SCEN_UNIT_LIST.eUIOpenReserve.Nothing;
			}
			if (this.m_reservedTab != NKC_SCEN_UNIT_LIST.UNIT_LIST_TAB.ULT_NORMAL)
			{
				switch (this.m_reservedTab)
				{
				case NKC_SCEN_UNIT_LIST.UNIT_LIST_TAB.ULT_SHIP:
					NKCUIUnitSelectList.Instance.OnSelectShipMode(true);
					break;
				case NKC_SCEN_UNIT_LIST.UNIT_LIST_TAB.ULT_OPERATOR:
					NKCUIUnitSelectList.Instance.OnSelectOperatorMode(true);
					break;
				case NKC_SCEN_UNIT_LIST.UNIT_LIST_TAB.ULT_TROPHY:
					NKCUIUnitSelectList.Instance.OnSelectTrophyMode(true);
					break;
				}
				this.m_reservedTab = NKC_SCEN_UNIT_LIST.UNIT_LIST_TAB.ULT_NORMAL;
			}
		}

		// Token: 0x06004945 RID: 18757 RVA: 0x00160BF8 File Offset: 0x0015EDF8
		private bool FilterRemoveAuto(NKMUnitData unitData)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData);
			if (unitTempletBase == null)
			{
				return false;
			}
			if (unitData.m_UnitLevel > 1)
			{
				return false;
			}
			if (unitTempletBase.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_TRAINER)
			{
				return false;
			}
			NKM_UNIT_GRADE nkm_UNIT_GRADE = unitTempletBase.m_NKM_UNIT_GRADE;
			return nkm_UNIT_GRADE - NKM_UNIT_GRADE.NUG_SR > 2;
		}

		// Token: 0x06004946 RID: 18758 RVA: 0x00160C37 File Offset: 0x0015EE37
		private void MoveToHomeScene()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, false);
		}

		// Token: 0x06004947 RID: 18759 RVA: 0x00160C45 File Offset: 0x0015EE45
		public override void ScenEnd()
		{
			NKCUIUnitSelectList.CheckInstanceAndClose();
			base.ScenEnd();
		}

		// Token: 0x06004948 RID: 18760 RVA: 0x00160C52 File Offset: 0x0015EE52
		public void SetReservedTab(NKC_SCEN_UNIT_LIST.UNIT_LIST_TAB tabType)
		{
			this.m_reservedTab = tabType;
		}

		// Token: 0x06004949 RID: 18761 RVA: 0x00160C5C File Offset: 0x0015EE5C
		private static void OpenUnitData(List<long> lstUnitUIDs)
		{
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			NKMUnitData unitOrShipFromUID = armyData.GetUnitOrShipFromUID(lstUnitUIDs[0]);
			if (unitOrShipFromUID != null)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitOrShipFromUID.m_UnitID);
				NKCUIUnitInfo.OpenOption openOption = new NKCUIUnitInfo.OpenOption(NKC_SCEN_UNIT_LIST.m_UnitSortList, NKC_SCEN_UNIT_LIST.m_SelectUnitIndex);
				if (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL)
				{
					NKCUIUnitInfo.Instance.Open(unitOrShipFromUID, new NKCUIUnitInfo.OnRemoveFromDeck(NKC_SCEN_UNIT_LIST.OnRemoveUnit), openOption, NKC_SCEN_UNIT_LIST.eUIOpenReserve.Nothing);
					return;
				}
				if (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
				{
					NKMDeckIndex shipDeckIndex = armyData.GetShipDeckIndex(NKM_DECK_TYPE.NDT_NORMAL, unitOrShipFromUID.m_UnitUID);
					NKCUIShipInfo.Instance.Open(unitOrShipFromUID, shipDeckIndex, openOption, NKC_SCEN_UNIT_LIST.eUIOpenReserve.Nothing);
					return;
				}
			}
			else
			{
				NKMOperator operatorData = NKCOperatorUtil.GetOperatorData(lstUnitUIDs[0]);
				if (operatorData != null)
				{
					NKCUIOperatorInfo.OpenOption option = new NKCUIOperatorInfo.OpenOption(NKC_SCEN_UNIT_LIST.m_OperatorSortList, NKC_SCEN_UNIT_LIST.m_SelectUnitIndex);
					NKCUIOperatorInfo.Instance.Open(operatorData, option);
				}
			}
		}

		// Token: 0x0600494A RID: 18762 RVA: 0x00160D24 File Offset: 0x0015EF24
		private void OpenUnitData(List<long> lstUnitUIDs, NKC_SCEN_UNIT_LIST.eUIOpenReserve reserveUI)
		{
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			NKMUnitData unitOrShipFromUID = armyData.GetUnitOrShipFromUID(lstUnitUIDs[0]);
			if (unitOrShipFromUID != null)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitOrShipFromUID.m_UnitID);
				NKCUIUnitInfo.OpenOption openOption = new NKCUIUnitInfo.OpenOption(NKC_SCEN_UNIT_LIST.m_UnitSortList, NKC_SCEN_UNIT_LIST.m_SelectUnitIndex);
				if (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL)
				{
					NKCUIUnitInfo.Instance.Open(unitOrShipFromUID, new NKCUIUnitInfo.OnRemoveFromDeck(NKC_SCEN_UNIT_LIST.OnRemoveUnit), openOption, reserveUI);
					return;
				}
				if (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
				{
					NKMDeckIndex shipDeckIndex = armyData.GetShipDeckIndex(NKM_DECK_TYPE.NDT_NORMAL, unitOrShipFromUID.m_UnitUID);
					NKCUIShipInfo.Instance.Open(unitOrShipFromUID, shipDeckIndex, openOption, reserveUI);
					return;
				}
			}
		}

		// Token: 0x0600494B RID: 18763 RVA: 0x00160DBC File Offset: 0x0015EFBC
		private static void OnUnitSortList(long UID, List<NKMUnitData> unitUIDList)
		{
			NKC_SCEN_UNIT_LIST.m_UnitSortList = unitUIDList;
			if (NKC_SCEN_UNIT_LIST.m_UnitSortList.Count > 1)
			{
				for (int i = 0; i < NKC_SCEN_UNIT_LIST.m_UnitSortList.Count; i++)
				{
					if (NKC_SCEN_UNIT_LIST.m_UnitSortList[i].m_UnitUID == UID)
					{
						NKC_SCEN_UNIT_LIST.m_SelectUnitIndex = i;
						return;
					}
				}
			}
		}

		// Token: 0x0600494C RID: 18764 RVA: 0x00160E0C File Offset: 0x0015F00C
		private static void OnOperatorSortList(long UID, List<NKMOperator> operatorUIDList)
		{
			NKC_SCEN_UNIT_LIST.m_OperatorSortList = operatorUIDList;
			if (NKC_SCEN_UNIT_LIST.m_OperatorSortList.Count > 1)
			{
				for (int i = 0; i < NKC_SCEN_UNIT_LIST.m_OperatorSortList.Count; i++)
				{
					if (NKC_SCEN_UNIT_LIST.m_OperatorSortList[i].uid == UID)
					{
						NKC_SCEN_UNIT_LIST.m_SelectUnitIndex = i;
						return;
					}
				}
			}
		}

		// Token: 0x0600494D RID: 18765 RVA: 0x00160E5C File Offset: 0x0015F05C
		public void OnUnitUpdate(long uid, NKMUnitData unitData)
		{
			int num = NKC_SCEN_UNIT_LIST.m_UnitSortList.FindIndex((NKMUnitData x) => x.m_UnitUID == uid);
			if (num >= 0 && num < NKC_SCEN_UNIT_LIST.m_UnitSortList.Count)
			{
				NKC_SCEN_UNIT_LIST.m_UnitSortList[num] = unitData;
			}
		}

		// Token: 0x0600494E RID: 18766 RVA: 0x00160EAC File Offset: 0x0015F0AC
		public static void OnRemoveUnit(NKMUnitData UnitData)
		{
			NKMDeckIndex deckIndex;
			sbyte slotIndex;
			if (NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetUnitDeckPosition(NKM_DECK_TYPE.NDT_NORMAL, UnitData.m_UnitUID, out deckIndex, out slotIndex))
			{
				NKCPacketSender.Send_NKMPacket_DECK_UNIT_SET_REQ(deckIndex, (int)slotIndex, 0L);
			}
		}

		// Token: 0x0600494F RID: 18767 RVA: 0x00160EE3 File Offset: 0x0015F0E3
		public void SetOpenReserve(NKC_SCEN_UNIT_LIST.eUIOpenReserve UIToOpen, long unitUID = 0L, bool bForce = false)
		{
			if (!bForce && this.CheckIgnoreReservedUI(UIToOpen))
			{
				return;
			}
			this.m_eUIOpenReserve = UIToOpen;
			this.m_reserveUnitUID = unitUID;
		}

		// Token: 0x06004950 RID: 18768 RVA: 0x00160F00 File Offset: 0x0015F100
		private bool CheckIgnoreReservedUI(NKC_SCEN_UNIT_LIST.eUIOpenReserve UIToOpen)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_UNIT_LIST)
			{
				return false;
			}
			if (UIToOpen != NKC_SCEN_UNIT_LIST.eUIOpenReserve.ShipRepair)
			{
				return UIToOpen - NKC_SCEN_UNIT_LIST.eUIOpenReserve.UnitSkillTraining <= 2 && NKCUIUnitInfo.IsInstanceOpen;
			}
			return NKCUIShipInfo.IsInstanceOpen;
		}

		// Token: 0x04003891 RID: 14481
		private NKC_SCEN_UNIT_LIST.UNIT_LIST_TAB m_reservedTab;

		// Token: 0x04003892 RID: 14482
		private static int m_SelectUnitIndex = 0;

		// Token: 0x04003893 RID: 14483
		private static List<NKMUnitData> m_UnitSortList = new List<NKMUnitData>();

		// Token: 0x04003894 RID: 14484
		private static List<NKMOperator> m_OperatorSortList = new List<NKMOperator>();

		// Token: 0x04003895 RID: 14485
		private NKC_SCEN_UNIT_LIST.eUIOpenReserve m_eUIOpenReserve;

		// Token: 0x04003896 RID: 14486
		private long m_reserveUnitUID;

		// Token: 0x020013FC RID: 5116
		public enum UNIT_LIST_TAB
		{
			// Token: 0x04009CD5 RID: 40149
			ULT_NORMAL,
			// Token: 0x04009CD6 RID: 40150
			ULT_SHIP,
			// Token: 0x04009CD7 RID: 40151
			ULT_OPERATOR,
			// Token: 0x04009CD8 RID: 40152
			ULT_TROPHY
		}

		// Token: 0x020013FD RID: 5117
		public enum eUIOpenReserve
		{
			// Token: 0x04009CDA RID: 40154
			Nothing,
			// Token: 0x04009CDB RID: 40155
			ShipRepair,
			// Token: 0x04009CDC RID: 40156
			ShipModule,
			// Token: 0x04009CDD RID: 40157
			UnitSkillTraining,
			// Token: 0x04009CDE RID: 40158
			UnitLimitbreak,
			// Token: 0x04009CDF RID: 40159
			UnitNegotiate
		}
	}
}
