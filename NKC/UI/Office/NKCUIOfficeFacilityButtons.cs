using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.Office
{
	// Token: 0x02000AF0 RID: 2800
	public class NKCUIOfficeFacilityButtons : MonoBehaviour
	{
		// Token: 0x06007E07 RID: 32263 RVA: 0x002A441E File Offset: 0x002A261E
		public void UpdateAlarm()
		{
			NKCUtil.SetGameobjectActive(this.m_objHangarBuildReddot, NKCAlarmManager.CheckHangarNotify(NKCScenManager.CurrentUserData()));
			NKCUtil.SetGameobjectActive(this.m_objCEOScoutReddot, NKCAlarmManager.CheckScoutNotify(NKCScenManager.CurrentUserData()));
		}

		// Token: 0x06007E08 RID: 32264 RVA: 0x002A444C File Offset: 0x002A264C
		public void Init(UnityAction OnClose)
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnMinimap, OnClose);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnLabUnitList, new UnityAction(this.OnLabUnitList));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnLabUnitRearm, new UnityAction(this.OnLabUnitRearm));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnLabUnitExtract, new UnityAction(this.OnLabUnitExtract));
			if (!NKMOpenTagManager.IsOpened("EQUIP_UPGRADE"))
			{
				NKCUtil.SetGameobjectActive(this.m_csbtnForgeUpgrade, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_csbtnForgeUpgrade, true);
				if (NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_UPGRADE, 0, 0))
				{
					this.m_csbtnForgeUpgrade.UnLock(false);
				}
				else
				{
					this.m_csbtnForgeUpgrade.Lock(false);
				}
				NKCUtil.SetButtonClickDelegate(this.m_csbtnForgeUpgrade, new UnityAction(this.OnClickUpgrade));
				this.m_csbtnForgeUpgrade.m_bGetCallbackWhileLocked = true;
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnForgeEnhance, new UnityAction(this.OnForgeEnhance));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnForgeEquipList, new UnityAction(this.OnForgeEquipList));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnForgeBuild, new UnityAction(this.OnForgeBuild));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnForgeFinishAll, new UnityAction(this.OnForgeFinishAll));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnHangarBuild, new UnityAction(this.OnHangarBuild));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnHangerShipList, new UnityAction(this.OnHangerShipList));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnCEOLifetime, new UnityAction(this.OnBtnCEOLifetime));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnCEOScout, new UnityAction(this.OnCEOScout));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnJukeBox, new UnityAction(this.OnJukeBox));
			bool bValue = NKCRearmamentUtil.IsCanUseContent();
			NKCUtil.SetGameobjectActive(this.m_csbtnLabUnitRearm.gameObject, bValue);
			NKCUtil.SetGameobjectActive(this.m_csbtnLabUnitExtract.gameObject, bValue);
		}

		// Token: 0x06007E09 RID: 32265 RVA: 0x002A4610 File Offset: 0x002A2810
		public void SetMode(NKMOfficeRoomTemplet.RoomType type)
		{
			NKCUtil.SetGameobjectActive(this.m_objLabRoot, type == NKMOfficeRoomTemplet.RoomType.Lab);
			NKCUtil.SetGameobjectActive(this.m_objForgeRoot, type == NKMOfficeRoomTemplet.RoomType.Forge);
			NKCUtil.SetGameobjectActive(this.m_objHangarRoot, type == NKMOfficeRoomTemplet.RoomType.Hangar);
			NKCUtil.SetGameobjectActive(this.m_objCEORoot, type == NKMOfficeRoomTemplet.RoomType.CEO);
			if (this.m_csbtnCEOLifetime != null)
			{
				this.m_csbtnCEOLifetime.SetLock(!NKCContentManager.IsContentsUnlocked(ContentsType.PERSONNAL_LIFETIME, 0, 0), false);
			}
			if (this.m_csbtnCEOScout != null)
			{
				this.m_csbtnCEOScout.SetLock(!NKCContentManager.IsContentsUnlocked(ContentsType.PERSONNAL_SCOUT, 0, 0), false);
			}
			if (this.m_csbtnForgeBuild != null)
			{
				this.m_csbtnForgeBuild.SetLock(!NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_CRAFT, 0, 0), false);
			}
			if (this.m_csbtnForgeEnhance != null)
			{
				this.m_csbtnForgeEnhance.SetLock(!NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_ENCHANT, 0, 0), false);
			}
			if (this.m_csbtnHangarBuild != null)
			{
				this.m_csbtnHangarBuild.SetLock(!NKCContentManager.IsContentsUnlocked(ContentsType.HANGER_SHIPBUILD, 0, 0), false);
			}
			if (this.m_csbtnLabUnitRearm != null)
			{
				this.m_csbtnLabUnitRearm.SetLock(!NKCContentManager.IsContentsUnlocked(ContentsType.REARM, 0, 0), false);
			}
			if (this.m_csbtnLabUnitExtract != null)
			{
				this.m_csbtnLabUnitExtract.SetLock(!NKCContentManager.IsContentsUnlocked(ContentsType.EXTRACT, 0, 0), false);
			}
			this.UpdateAlarm();
		}

		// Token: 0x06007E0A RID: 32266 RVA: 0x002A4769 File Offset: 0x002A2969
		public void OnCEOScout()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.PERSONNAL_SCOUT, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.PERSONNAL_SCOUT, 0);
				return;
			}
			NKCUIScout.Instance.Open();
		}

		// Token: 0x06007E0B RID: 32267 RVA: 0x002A478A File Offset: 0x002A298A
		private void OnBtnCEOLifetime()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.PERSONNAL_LIFETIME, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.PERSONNAL_LIFETIME, 0);
				return;
			}
			this.OnCEOLifetime(0L);
		}

		// Token: 0x06007E0C RID: 32268 RVA: 0x002A47AC File Offset: 0x002A29AC
		public void OnCEOLifetime(long uid)
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.PERSONNAL_LIFETIME, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.PERSONNAL_LIFETIME, 0);
				return;
			}
			NKCUIPersonnel.Instance.Open();
			if (uid > 0L)
			{
				NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(uid);
				NKCUIPersonnel.Instance.ReserveUnitData(unitFromUID);
			}
		}

		// Token: 0x06007E0D RID: 32269 RVA: 0x002A47F9 File Offset: 0x002A29F9
		private void OnJukeBox()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.BASE_PERSONNAL, 0, 0))
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_JUKEBOX_CONTENTS_UNLOCK, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			NKCUIJukeBox.Instance.Open(false, 0, null);
		}

		// Token: 0x06007E0E RID: 32270 RVA: 0x002A4828 File Offset: 0x002A2A28
		public void OnHangerShipList()
		{
			NKCUIUnitSelectList.UnitSelectListOptions options = this.MakeUnitListOptions(NKM_UNIT_TYPE.NUT_SHIP);
			NKCUIUnitSelectList.Instance.Open(options, new NKCUIUnitSelectList.OnUnitSelectCommand(NKCUIOfficeFacilityButtons.OpenUnitData), new NKCUIUnitSelectList.OnUnitSortList(NKCUIOfficeFacilityButtons.OnUnitSortList), new NKCUIUnitSelectList.OnOperatorSortList(NKCUIOfficeFacilityButtons.OnOperatorSortList), null, null);
		}

		// Token: 0x06007E0F RID: 32271 RVA: 0x002A486E File Offset: 0x002A2A6E
		public void OnHangarBuild()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.HANGER_SHIPBUILD, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.HANGER_SHIPBUILD, 0);
				return;
			}
			NKCUIHangarBuild.Instance.Open();
		}

		// Token: 0x06007E10 RID: 32272 RVA: 0x002A4890 File Offset: 0x002A2A90
		private void OnClickUpgrade()
		{
			if (!NKMOpenTagManager.IsOpened("EQUIP_UPGRADE"))
			{
				return;
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.BASE_FACTORY, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.BASE_FACTORY, 0);
				return;
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_UPGRADE, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.FACTORY_UPGRADE, 0);
				return;
			}
			NKCUIForgeUpgrade.Instance.Open();
		}

		// Token: 0x06007E11 RID: 32273 RVA: 0x002A48DE File Offset: 0x002A2ADE
		private void OnForgeEnhance()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.BASE_FACTORY, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.BASE_FACTORY, 0);
				return;
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_ENCHANT, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.FACTORY_ENCHANT, 0);
				return;
			}
			NKCUIForge.Instance.Open(NKCUIForge.NKC_FORGE_TAB.NFT_ENCHANT, 0L, null);
		}

		// Token: 0x06007E12 RID: 32274 RVA: 0x002A4918 File Offset: 0x002A2B18
		public void OnForgeEquipList()
		{
			NKCUIInventory.EquipSelectListOptions options = new NKCUIInventory.EquipSelectListOptions(NKC_INVENTORY_OPEN_TYPE.NIOT_NORMAL, false, true);
			options.lstSortOption = NKCEquipSortSystem.FORGE_TARGET_SORT_LIST;
			options.strEmptyMessage = NKCUtilString.GET_STRING_INVEN_MISC_NO_EXIST;
			options.m_dOnSelectedEquipSlot = delegate(NKCUISlotEquip slot, NKMEquipItemData data)
			{
				NKCPopupItemEquipBox.Open(data, NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_ENFORCE_AND_EQUIP, delegate
				{
					this.SetLatestOpenNKMEquipItemData(data);
				});
			};
			NKCUIInventory.Instance.Open(options, null, 0L, NKCUIInventory.NKC_INVENTORY_TAB.NIT_NONE);
		}

		// Token: 0x06007E13 RID: 32275 RVA: 0x002A4969 File Offset: 0x002A2B69
		private void SetLatestOpenNKMEquipItemData(NKMEquipItemData equipItemData)
		{
			NKCUIInventory.Instance.SetLatestOpenNKMEquipItemDataAndOpenUnitSelect(equipItemData);
		}

		// Token: 0x06007E14 RID: 32276 RVA: 0x002A4978 File Offset: 0x002A2B78
		public void OnForgeBuild()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.BASE_FACTORY, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.BASE_FACTORY, 0);
				return;
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_CRAFT, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.FACTORY_CRAFT, 0);
				return;
			}
			NKMCraftData craftData = NKCScenManager.GetScenManager().GetMyUserData().m_CraftData;
			if (craftData == null)
			{
				return;
			}
			int firstEmptySlotIndex = craftData.GetFirstEmptySlotIndex();
			if (firstEmptySlotIndex >= 0)
			{
				NKCUIForgeCraftMold.Instance.Open(firstEmptySlotIndex);
			}
		}

		// Token: 0x06007E15 RID: 32277 RVA: 0x002A49D9 File Offset: 0x002A2BD9
		public void OnForgeFinishAll()
		{
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_COMING_SOON_SYSTEM, null, "");
		}

		// Token: 0x06007E16 RID: 32278 RVA: 0x002A49F0 File Offset: 0x002A2BF0
		public void OnLabUnitList()
		{
			NKCUIUnitSelectList.UnitSelectListOptions options = this.MakeUnitListOptions(NKM_UNIT_TYPE.NUT_NORMAL);
			NKCUIUnitSelectList.Instance.Open(options, new NKCUIUnitSelectList.OnUnitSelectCommand(NKCUIOfficeFacilityButtons.OpenUnitData), new NKCUIUnitSelectList.OnUnitSortList(NKCUIOfficeFacilityButtons.OnUnitSortList), new NKCUIUnitSelectList.OnOperatorSortList(NKCUIOfficeFacilityButtons.OnOperatorSortList), null, null);
		}

		// Token: 0x06007E17 RID: 32279 RVA: 0x002A4A36 File Offset: 0x002A2C36
		public void OnLabUnitRearm()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.REARM, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.REARM, 0);
				return;
			}
			NKCUIRearmament.Instance.Open(NKCUIRearmament.REARM_TYPE.RT_LIST);
		}

		// Token: 0x06007E18 RID: 32280 RVA: 0x002A4A58 File Offset: 0x002A2C58
		public void OnLabUnitExtract()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.EXTRACT, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.EXTRACT, 0);
				return;
			}
			NKCUIRearmament.Instance.Open(NKCUIRearmament.REARM_TYPE.RT_EXTRACT);
		}

		// Token: 0x06007E19 RID: 32281 RVA: 0x002A4A7C File Offset: 0x002A2C7C
		private NKCUIUnitSelectList.UnitSelectListOptions MakeUnitListOptions(NKM_UNIT_TYPE type)
		{
			NKCUIUnitSelectList.UnitSelectListOptions result = new NKCUIUnitSelectList.UnitSelectListOptions(type, false, NKM_DECK_TYPE.NDT_NORMAL, NKCUIUnitSelectList.eUnitSelectListMode.Normal, true);
			result.bShowUnitShipChangeMenu = false;
			result.bEnableLockUnitSystem = false;
			result.bEnableRemoveUnitSystem = false;
			result.dOnAutoSelectFilter = new NKCUIUnitSelectList.UnitSelectListOptions.OnAutoSelectFilter(this.FilterRemoveAuto);
			result.bUseRemoveSmartAutoSelect = true;
			result.bShowHideDeckedUnitMenu = false;
			result.bCanSelectUnitInMission = true;
			result.m_SortOptions.bIncludeSeizure = true;
			result.m_SortOptions.bIgnoreWorldMapLeader = true;
			result.m_OperatorSortOptions.SetBuildOption(true, new BUILD_OPTIONS[]
			{
				BUILD_OPTIONS.INCLUDE_SEIZURE,
				BUILD_OPTIONS.IGNORE_WORLD_MAP_LEADER
			});
			result.strUpsideMenuName = NKCUtilString.GET_STRING_MANAGEMENT;
			result.bPushBackUnselectable = false;
			result.setUnitFilterCategory = NKCUnitSortSystem.setDefaultUnitFilterCategory;
			result.setUnitSortCategory = NKCUnitSortSystem.setDefaultUnitSortCategory;
			result.setShipFilterCategory = NKCUnitSortSystem.setDefaultShipFilterCategory;
			result.setShipSortCategory = NKCUnitSortSystem.setDefaultShipSortCategory;
			result.ShopShortcutTargetTab = null;
			result.setOperatorFilterCategory = NKCOperatorSortSystem.setDefaultOperatorFilterCategory;
			result.setOperatorSortCategory = NKCOperatorSortSystem.setDefaultOperatorSortCategory;
			result.m_bUseFavorite = true;
			return result;
		}

		// Token: 0x06007E1A RID: 32282 RVA: 0x002A4B7C File Offset: 0x002A2D7C
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

		// Token: 0x06007E1B RID: 32283 RVA: 0x002A4BBC File Offset: 0x002A2DBC
		private static void OpenUnitData(List<long> lstUnitUIDs)
		{
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			NKMUnitData unitOrShipFromUID = armyData.GetUnitOrShipFromUID(lstUnitUIDs[0]);
			if (unitOrShipFromUID != null)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitOrShipFromUID.m_UnitID);
				NKCUIUnitInfo.OpenOption openOption = new NKCUIUnitInfo.OpenOption(NKCUIOfficeFacilityButtons.m_UnitSortList, NKCUIOfficeFacilityButtons.m_SelectUnitIndex);
				if (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL)
				{
					NKCUIUnitInfo.Instance.Open(unitOrShipFromUID, null, openOption, NKC_SCEN_UNIT_LIST.eUIOpenReserve.Nothing);
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
					NKCUIOperatorInfo.OpenOption option = new NKCUIOperatorInfo.OpenOption(NKCUIOfficeFacilityButtons.m_OperatorSortList, NKCUIOfficeFacilityButtons.m_SelectUnitIndex);
					NKCUIOperatorInfo.Instance.Open(operatorData, option);
				}
			}
		}

		// Token: 0x06007E1C RID: 32284 RVA: 0x002A4C78 File Offset: 0x002A2E78
		private static void OnUnitSortList(long UID, List<NKMUnitData> unitUIDList)
		{
			NKCUIOfficeFacilityButtons.m_UnitSortList = unitUIDList;
			if (NKCUIOfficeFacilityButtons.m_UnitSortList.Count > 1)
			{
				for (int i = 0; i < NKCUIOfficeFacilityButtons.m_UnitSortList.Count; i++)
				{
					if (NKCUIOfficeFacilityButtons.m_UnitSortList[i].m_UnitUID == UID)
					{
						NKCUIOfficeFacilityButtons.m_SelectUnitIndex = i;
						return;
					}
				}
			}
		}

		// Token: 0x06007E1D RID: 32285 RVA: 0x002A4CC8 File Offset: 0x002A2EC8
		private static void OnOperatorSortList(long UID, List<NKMOperator> operatorUIDList)
		{
			NKCUIOfficeFacilityButtons.m_OperatorSortList = operatorUIDList;
			if (NKCUIOfficeFacilityButtons.m_OperatorSortList.Count > 1)
			{
				for (int i = 0; i < NKCUIOfficeFacilityButtons.m_OperatorSortList.Count; i++)
				{
					if (NKCUIOfficeFacilityButtons.m_OperatorSortList[i].uid == UID)
					{
						NKCUIOfficeFacilityButtons.m_SelectUnitIndex = i;
						return;
					}
				}
			}
		}

		// Token: 0x04006AE3 RID: 27363
		public NKCUIComStateButton m_csbtnMinimap;

		// Token: 0x04006AE4 RID: 27364
		[Header("연구소")]
		public GameObject m_objLabRoot;

		// Token: 0x04006AE5 RID: 27365
		public NKCUIComStateButton m_csbtnLabUnitList;

		// Token: 0x04006AE6 RID: 27366
		public NKCUIComStateButton m_csbtnLabUnitRearm;

		// Token: 0x04006AE7 RID: 27367
		public NKCUIComStateButton m_csbtnLabUnitExtract;

		// Token: 0x04006AE8 RID: 27368
		[Header("공방")]
		public GameObject m_objForgeRoot;

		// Token: 0x04006AE9 RID: 27369
		public NKCUIComStateButton m_csbtnForgeUpgrade;

		// Token: 0x04006AEA RID: 27370
		public NKCUIComStateButton m_csbtnForgeEnhance;

		// Token: 0x04006AEB RID: 27371
		public NKCUIComStateButton m_csbtnForgeEquipList;

		// Token: 0x04006AEC RID: 27372
		public NKCUIComStateButton m_csbtnForgeBuild;

		// Token: 0x04006AED RID: 27373
		public NKCUIComStateButton m_csbtnForgeFinishAll;

		// Token: 0x04006AEE RID: 27374
		[Header("격납고")]
		public GameObject m_objHangarRoot;

		// Token: 0x04006AEF RID: 27375
		public NKCUIComStateButton m_csbtnHangarBuild;

		// Token: 0x04006AF0 RID: 27376
		public GameObject m_objHangarBuildReddot;

		// Token: 0x04006AF1 RID: 27377
		public NKCUIComStateButton m_csbtnHangerShipList;

		// Token: 0x04006AF2 RID: 27378
		[Header("사장실")]
		public GameObject m_objCEORoot;

		// Token: 0x04006AF3 RID: 27379
		public NKCUIComStateButton m_csbtnCEOLifetime;

		// Token: 0x04006AF4 RID: 27380
		public NKCUIComStateButton m_csbtnCEOScout;

		// Token: 0x04006AF5 RID: 27381
		public NKCUIComStateButton m_csbtnJukeBox;

		// Token: 0x04006AF6 RID: 27382
		public GameObject m_objCEOScoutReddot;

		// Token: 0x04006AF7 RID: 27383
		private static int m_SelectUnitIndex = 0;

		// Token: 0x04006AF8 RID: 27384
		private static List<NKMUnitData> m_UnitSortList = new List<NKMUnitData>();

		// Token: 0x04006AF9 RID: 27385
		private static List<NKMOperator> m_OperatorSortList = new List<NKMOperator>();
	}
}
