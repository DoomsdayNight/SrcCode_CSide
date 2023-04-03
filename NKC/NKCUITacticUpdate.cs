using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Unit;
using ClientPacket.User;
using ClientPacket.WorldMap;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000AA4 RID: 2724
	public class NKCUITacticUpdate : NKCUIBase
	{
		// Token: 0x17001445 RID: 5189
		// (get) Token: 0x060078B7 RID: 30903 RVA: 0x0028138C File Offset: 0x0027F58C
		public static NKCUITacticUpdate Instance
		{
			get
			{
				if (NKCUITacticUpdate.m_Instance == null)
				{
					NKCUITacticUpdate.m_Instance = NKCUIManager.OpenNewInstance<NKCUITacticUpdate>("ab_ui_nkm_ui_unit_info", "AB_UI_TACTIC_UPDATE", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUITacticUpdate.CleanupInstance)).GetInstance<NKCUITacticUpdate>();
				}
				NKCUITacticUpdate.m_Instance.Initialize();
				return NKCUITacticUpdate.m_Instance;
			}
		}

		// Token: 0x060078B8 RID: 30904 RVA: 0x002813DB File Offset: 0x0027F5DB
		public static void CheckInstanceAndClose()
		{
			if (NKCUITacticUpdate.m_Instance != null && NKCUITacticUpdate.m_Instance.IsOpen)
			{
				NKCUITacticUpdate.m_Instance.Close();
			}
		}

		// Token: 0x060078B9 RID: 30905 RVA: 0x00281400 File Offset: 0x0027F600
		private static void CleanupInstance()
		{
			NKCUITacticUpdate.m_Instance = null;
		}

		// Token: 0x17001446 RID: 5190
		// (get) Token: 0x060078BA RID: 30906 RVA: 0x00281408 File Offset: 0x0027F608
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUITacticUpdate.m_Instance != null && NKCUITacticUpdate.m_Instance.IsOpen;
			}
		}

		// Token: 0x17001447 RID: 5191
		// (get) Token: 0x060078BB RID: 30907 RVA: 0x00281423 File Offset: 0x0027F623
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001448 RID: 5192
		// (get) Token: 0x060078BC RID: 30908 RVA: 0x00281426 File Offset: 0x0027F626
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_UNIT_TACTIC_UPDATE";
			}
		}

		// Token: 0x17001449 RID: 5193
		// (get) Token: 0x060078BD RID: 30909 RVA: 0x0028142D File Offset: 0x0027F62D
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_TACTIC_UPDATE;
			}
		}

		// Token: 0x1700144A RID: 5194
		// (get) Token: 0x060078BE RID: 30910 RVA: 0x00281434 File Offset: 0x0027F634
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.LeftsideOnly;
			}
		}

		// Token: 0x060078BF RID: 30911 RVA: 0x00281437 File Offset: 0x0027F637
		public override void CloseInternal()
		{
			NKCUIUnitSelectList uiunitSelectList = this.m_UIUnitSelectList;
			if (uiunitSelectList != null)
			{
				uiunitSelectList.Close();
			}
			this.m_UIUnitSelectList = null;
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060078C0 RID: 30912 RVA: 0x0028145D File Offset: 0x0027F65D
		public override void Initialize()
		{
			NKCUtil.SetBindFunction(this.m_csbtnReady, new UnityAction(this.OnClickTacticReady));
			NKCUtil.SetBindFunction(this.m_csbtnRun, new UnityAction(this.OnClickTacticUpdate));
			this.m_UnitSelectListSlot.Init(true);
		}

		// Token: 0x060078C1 RID: 30913 RVA: 0x00281499 File Offset: 0x0027F699
		public override void OnBackButton()
		{
			if (this.m_curTacticUpdateStep == NKCUITacticUpdate.TACTIC_UPDATE_STEP.READY)
			{
				this.UpdateTacticUpdateUI(NKCUITacticUpdate.TACTIC_UPDATE_STEP.BACK, false);
				return;
			}
			base.OnBackButton();
		}

		// Token: 0x060078C2 RID: 30914 RVA: 0x002814B3 File Offset: 0x0027F6B3
		public override void UnHide()
		{
			base.UnHide();
			if (this.m_curTacticUpdateStep == NKCUITacticUpdate.TACTIC_UPDATE_STEP.READY)
			{
				this.SetAni("READY_IDLE");
			}
		}

		// Token: 0x060078C3 RID: 30915 RVA: 0x002814D0 File Offset: 0x0027F6D0
		public void Open(long tacticUpdateUnitUID)
		{
			NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(tacticUpdateUnitUID);
			this.Open(unitFromUID);
		}

		// Token: 0x060078C4 RID: 30916 RVA: 0x002814F8 File Offset: 0x0027F6F8
		public void Open(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objFX, false);
			base.gameObject.SetActive(true);
			this.m_targetUnitData = unitData;
			this.m_consumeUnitData = null;
			this.m_UnitSelectListSlot.SetEmpty(true, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnSelectUnitSlot), null);
			NKCUtil.SetLabelText(this.m_lbName, unitData.GetUnitTemplet().m_UnitTempletBase.GetUnitName());
			NKCUtil.SetLabelText(this.m_lbTitle, unitData.GetUnitTemplet().m_UnitTempletBase.GetUnitTitle());
			this.m_CharView.SetCharacterIllust(unitData, false, false, true, 0);
			this.UpdateTacticUpdateUI(NKCUITacticUpdate.TACTIC_UPDATE_STEP.INTRO, true);
			base.UIOpened(true);
		}

		// Token: 0x060078C5 RID: 30917 RVA: 0x0028159D File Offset: 0x0027F79D
		private void OnClickTacticReady()
		{
			if (this.m_targetUnitData.tacticLevel == 6)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_TACTIC_UPDATE_MAX_UNIT, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			this.UpdateTacticUpdateUI(NKCUITacticUpdate.TACTIC_UPDATE_STEP.READY, false);
		}

		// Token: 0x060078C6 RID: 30918 RVA: 0x002815CC File Offset: 0x0027F7CC
		private void OnClickTacticUpdate()
		{
			switch (this.m_curTacticUpdateStep)
			{
			case NKCUITacticUpdate.TACTIC_UPDATE_STEP.INTRO:
				this.UpdateTacticUpdateUI(NKCUITacticUpdate.TACTIC_UPDATE_STEP.READY, false);
				return;
			case NKCUITacticUpdate.TACTIC_UPDATE_STEP.READY:
				if (this.m_iUnEquipCnt > 0)
				{
					return;
				}
				this.OnTryTacticUpdate();
				return;
			case NKCUITacticUpdate.TACTIC_UPDATE_STEP.BACK:
				this.UpdateTacticUpdateUI(NKCUITacticUpdate.TACTIC_UPDATE_STEP.READY, false);
				return;
			default:
				Debug.Log(string.Format("<color=red>OnClickTacticUpdate : {0}</color>", this.m_curTacticUpdateStep));
				return;
			}
		}

		// Token: 0x060078C7 RID: 30919 RVA: 0x00281634 File Offset: 0x0027F834
		private bool IsPocessibleStatusConsumeUnit()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (this.m_consumeUnitData == null)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_TACTIC_UPDATE_UNIT_SELECT_NONE, null, "");
				return false;
			}
			NKMEquipmentSet equipmentSet = this.m_consumeUnitData.GetEquipmentSet(nkmuserData.m_InventoryData);
			if (equipmentSet.Weapon != null || equipmentSet.Defence != null || equipmentSet.Accessory != null || equipmentSet.Accessory2 != null)
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_TACTIC_UPDATE_BLOCK_MESSAGE_EQUIPED, new NKCPopupOKCancel.OnButton(this.UnEquipAllItems), null, false);
				return false;
			}
			if (this.m_consumeUnitData.m_bLock || nkmuserData.m_ArmyData.IsUnitInAnyDeck(this.m_consumeUnitData.m_UnitUID))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_TACTIC_UPDATE_UNIT_WARNING, null, "");
				return false;
			}
			if (nkmuserData.backGroundInfo.unitInfoList.Find((NKMBackgroundUnitInfo e) => e.unitUid == this.m_consumeUnitData.m_UnitUID) != null)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_TACTIC_UPDATE_UNIT_WARNING, null, "");
				return false;
			}
			using (Dictionary<int, NKMWorldMapCityData>.ValueCollection.Enumerator enumerator = nkmuserData.m_WorldmapData.worldMapCityDataMap.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.leaderUnitUID == this.m_consumeUnitData.m_UnitUID)
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_TACTIC_UPDATE_UNIT_WARNING, null, "");
						return false;
					}
				}
			}
			if (this.m_consumeUnitData.OfficeRoomId > 0)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_TACTIC_UPDATE_UNIT_WARNING, null, "");
				return false;
			}
			return true;
		}

		// Token: 0x060078C8 RID: 30920 RVA: 0x002817C4 File Offset: 0x0027F9C4
		private bool IsPosibbleRecall()
		{
			NKMIntervalTemplet nkmintervalTemplet = NKMIntervalTemplet.Find(NKMCommonConst.TacticReturnDateString);
			if (nkmintervalTemplet == null)
			{
				return false;
			}
			bool flag = this.m_consumeUnitData.m_regDate < nkmintervalTemplet.GetStartDateUtc();
			bool flag2 = nkmintervalTemplet.IsValidTime(NKCSynchronizedTime.ServiceTime);
			return NKMCommonConst.TacticReturnMaxCount - NKCScenManager.GetScenManager().GetMyUserData().m_unitTacticReturnCount > 0 && flag2 && flag && NKCPopupRecall.GetRecallRewardCnt(this.m_consumeUnitData) > 0;
		}

		// Token: 0x060078C9 RID: 30921 RVA: 0x00281834 File Offset: 0x0027FA34
		private void OnTryTacticUpdate()
		{
			if (!this.IsPocessibleStatusConsumeUnit())
			{
				return;
			}
			if (this.IsPosibbleRecall())
			{
				NKCPopupRecall.Instance.Open(this.m_consumeUnitData, delegate()
				{
					NKCPacketSender.Send_NKMPacket_UNIT_TACTIC_UPDATE_REQ(this.m_targetUnitData.m_UnitUID, this.m_consumeUnitData.m_UnitUID);
				});
				return;
			}
			string strDesc = NKCRearmamentUtil.IsHasLeaderSkill(this.m_consumeUnitData) ? NKCUtilString.GET_STRING_TACTIC_UPDATE_DESC_REARM : null;
			NKCPopupUnitConfirm.Instance.Open(this.m_consumeUnitData, NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_TACTIC_UPDATE_DESC, strDesc, delegate()
			{
				NKCPacketSender.Send_NKMPacket_UNIT_TACTIC_UPDATE_REQ(this.m_targetUnitData.m_UnitUID, this.m_consumeUnitData.m_UnitUID);
			}, null);
		}

		// Token: 0x060078CA RID: 30922 RVA: 0x002818B0 File Offset: 0x0027FAB0
		private void UnEquipAllItems()
		{
			this.m_iUnEquipCnt = 0;
			if (this.m_consumeUnitData == null)
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKMEquipmentSet equipmentSet = this.m_consumeUnitData.GetEquipmentSet(nkmuserData.m_InventoryData);
			this.SendUnEquipItemREQ(this.m_consumeUnitData.m_UnitUID, equipmentSet.Weapon, ITEM_EQUIP_POSITION.IEP_WEAPON);
			this.SendUnEquipItemREQ(this.m_consumeUnitData.m_UnitUID, equipmentSet.Defence, ITEM_EQUIP_POSITION.IEP_DEFENCE);
			this.SendUnEquipItemREQ(this.m_consumeUnitData.m_UnitUID, equipmentSet.Accessory, ITEM_EQUIP_POSITION.IEP_ACC);
			this.SendUnEquipItemREQ(this.m_consumeUnitData.m_UnitUID, equipmentSet.Accessory2, ITEM_EQUIP_POSITION.IEP_ACC2);
			if (this.m_iUnEquipCnt == 0)
			{
				this.OnCompletUnEquip();
			}
		}

		// Token: 0x060078CB RID: 30923 RVA: 0x00281953 File Offset: 0x0027FB53
		private void SendUnEquipItemREQ(long unitUID, NKMEquipItemData equipData, ITEM_EQUIP_POSITION position)
		{
			if (equipData == null)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_EQUIP_ITEM_EQUIP_REQ(false, unitUID, equipData.m_ItemUid, position);
			this.m_iUnEquipCnt++;
		}

		// Token: 0x060078CC RID: 30924 RVA: 0x00281975 File Offset: 0x0027FB75
		private void OnCompletUnEquip()
		{
			if (this.m_consumeUnitData != null)
			{
				this.m_UnitSelectListSlot.SetData(this.m_consumeUnitData, new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE), false, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnSelectUnitSlot));
			}
			this.OnTryTacticUpdate();
		}

		// Token: 0x060078CD RID: 30925 RVA: 0x002819A9 File Offset: 0x0027FBA9
		public void OnUnEquipComplete()
		{
			this.m_iUnEquipCnt--;
			if (this.m_iUnEquipCnt == 0)
			{
				this.OnCompletUnEquip();
			}
		}

		// Token: 0x060078CE RID: 30926 RVA: 0x002819C8 File Offset: 0x0027FBC8
		private void UpdateTacticUpdateUI(NKCUITacticUpdate.TACTIC_UPDATE_STEP step, bool bRefresh = false)
		{
			if (bRefresh)
			{
				this.UpdateNodeUI(bRefresh);
			}
			switch (step)
			{
			case NKCUITacticUpdate.TACTIC_UPDATE_STEP.INTRO:
				NKCUtil.SetGameobjectActive(this.m_objReadyBtnON, this.m_targetUnitData.tacticLevel != 6);
				NKCUtil.SetGameobjectActive(this.m_objReadyBtnOFF, this.m_targetUnitData.tacticLevel == 6);
				this.SetAni("INTRO");
				break;
			case NKCUITacticUpdate.TACTIC_UPDATE_STEP.READY:
				this.SetAni("READY");
				break;
			case NKCUITacticUpdate.TACTIC_UPDATE_STEP.BACK:
				this.SetAni("BACK");
				break;
			}
			this.m_curTacticUpdateStep = step;
		}

		// Token: 0x060078CF RID: 30927 RVA: 0x00281A54 File Offset: 0x0027FC54
		private void UpdateNodeUI(bool bInitActiveFX = false)
		{
			this.UpdateNode(ref this.m_lstTacticUpdateNodeCenter, bInitActiveFX);
			this.UpdateNode(ref this.m_lstTacticUpdateNodeBottom, false);
			this.m_TacticUpdateLvSlot.SetLevel(this.m_targetUnitData.tacticLevel, false);
			this.m_TargetTacticUpdateLvSlot.SetLevel(this.m_targetUnitData.tacticLevel, true);
			NKMTacticUpdateTemplet nkmtacticUpdateTemplet = NKMTempletContainer<NKMTacticUpdateTemplet>.Find((this.m_targetUnitData.tacticLevel >= 6) ? 6 : (this.m_targetUnitData.tacticLevel + 1));
			if (nkmtacticUpdateTemplet != null)
			{
				NKCUtil.SetImageSprite(this.m_imgTargetUpdateIcon, this.GetNodeStatIcon(nkmtacticUpdateTemplet.m_StatIcon), false);
				NKCUtil.SetLabelText(this.m_lbTargetUpdateDesc, this.GetTacticUpdateDesc(nkmtacticUpdateTemplet));
			}
		}

		// Token: 0x060078D0 RID: 30928 RVA: 0x00281AFC File Offset: 0x0027FCFC
		private string GetTacticUpdateDesc(NKMTacticUpdateTemplet targetTacticUpdateTemplet)
		{
			string arg = (targetTacticUpdateTemplet.m_StatValue * 0.01f).ToString("N2");
			return string.Format(NKCStringTable.GetString(targetTacticUpdateTemplet.m_StringKey, false), arg);
		}

		// Token: 0x060078D1 RID: 30929 RVA: 0x00281B38 File Offset: 0x0027FD38
		private void UpdateNode(ref List<NKCUITacticUpdate.NodeSlot> lstNodeSlots, bool bInitActiveFX = false)
		{
			int tacticLevel = this.m_targetUnitData.tacticLevel;
			List<NKMTacticUpdateTemplet> list = NKMTempletContainer<NKMTacticUpdateTemplet>.Values.ToList<NKMTacticUpdateTemplet>();
			for (int i = 0; i < lstNodeSlots.Count; i++)
			{
				if (list[i] == null)
				{
					NKCUtil.SetGameobjectActive(lstNodeSlots[i].objOFF, true);
					NKCUtil.SetGameobjectActive(lstNodeSlots[i].objFX, false);
					NKCUtil.SetGameobjectActive(lstNodeSlots[i].objON, false);
				}
				else
				{
					Sprite nodeStatIcon = this.GetNodeStatIcon(list[i].m_StatIcon);
					foreach (Image image in lstNodeSlots[i].lstIcon)
					{
						NKCUtil.SetImageSprite(image, nodeStatIcon, false);
					}
					string tacticUpdateDesc = this.GetTacticUpdateDesc(list[i]);
					foreach (Text label in lstNodeSlots[i].lbDesc)
					{
						NKCUtil.SetLabelText(label, tacticUpdateDesc);
					}
					NKCUtil.SetGameobjectActive(lstNodeSlots[i].objON, i < tacticLevel);
					NKCUtil.SetGameobjectActive(lstNodeSlots[i].objOFF, i >= tacticLevel);
					if (bInitActiveFX)
					{
						NKCUtil.SetGameobjectActive(lstNodeSlots[i].objFX, i < tacticLevel);
					}
				}
			}
		}

		// Token: 0x060078D2 RID: 30930 RVA: 0x00281CBC File Offset: 0x0027FEBC
		private Sprite GetNodeStatIcon(string assetName)
		{
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_unit_info_texture", assetName, false);
		}

		// Token: 0x1700144B RID: 5195
		// (get) Token: 0x060078D3 RID: 30931 RVA: 0x00281CCA File Offset: 0x0027FECA
		private NKCUIUnitSelectList UnitSelectList
		{
			get
			{
				if (this.m_UIUnitSelectList == null)
				{
					this.m_UIUnitSelectList = NKCUIUnitSelectList.OpenNewInstance(true);
				}
				return this.m_UIUnitSelectList;
			}
		}

		// Token: 0x060078D4 RID: 30932 RVA: 0x00281CEC File Offset: 0x0027FEEC
		private void OnClickSelectResourceUnit(long lSelectedUnitUID = 0L)
		{
			NKCUtil.SetGameobjectActive(this.m_objFX, false);
			NKCUIUnitSelectList.UnitSelectListOptions unitSelectListOptions = new NKCUIUnitSelectList.UnitSelectListOptions(NKM_UNIT_TYPE.NUT_NORMAL, true, NKM_DECK_TYPE.NDT_NORMAL, NKCUIUnitSelectList.eUnitSelectListMode.Normal, true);
			unitSelectListOptions.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
			unitSelectListOptions.lstSortOption = NKCUnitSortSystem.GetDefaultSortOptions(NKM_UNIT_TYPE.NUT_NORMAL, false, false);
			unitSelectListOptions.bDescending = false;
			unitSelectListOptions.bShowRemoveSlot = false;
			unitSelectListOptions.bExcludeLockedUnit = false;
			unitSelectListOptions.bExcludeDeckedUnit = false;
			unitSelectListOptions.m_SortOptions.bUseDeckedState = false;
			unitSelectListOptions.m_SortOptions.bUseLockedState = false;
			unitSelectListOptions.m_SortOptions.bUseDormInState = false;
			unitSelectListOptions.dOnSelectedUnitWarning = null;
			unitSelectListOptions.m_SortOptions.bIgnoreCityState = true;
			unitSelectListOptions.m_SortOptions.bIgnoreWorldMapLeader = true;
			unitSelectListOptions.bShowHideDeckedUnitMenu = false;
			unitSelectListOptions.bHideDeckedUnit = false;
			unitSelectListOptions.setOnlyIncludeUnitID = new HashSet<int>();
			unitSelectListOptions.setOnlyIncludeUnitID.Add(this.m_targetUnitData.GetUnitTemplet().m_UnitTempletBase.m_UnitID);
			foreach (int item in NKCRearmamentUtil.GetSameBaseUnitIDList(this.m_targetUnitData))
			{
				unitSelectListOptions.setOnlyIncludeUnitID.Add(item);
			}
			unitSelectListOptions.setSelectedUnitUID = new HashSet<long>();
			if (lSelectedUnitUID != 0L)
			{
				unitSelectListOptions.setSelectedUnitUID.Add(lSelectedUnitUID);
			}
			unitSelectListOptions.setExcludeUnitUID = new HashSet<long>
			{
				this.m_targetUnitData.m_UnitUID
			};
			unitSelectListOptions.setExcludeUnitID = NKCUnitSortSystem.GetDefaultExcludeUnitIDs();
			unitSelectListOptions.strEmptyMessage = NKCUtilString.GET_STRING_TACTIC_UPDATE_UNIT_SELECT_LIST_EMPTY_MESSAGE;
			unitSelectListOptions.dOnSlotSetData = null;
			unitSelectListOptions.setUnitFilterCategory = new HashSet<NKCUnitSortSystem.eFilterCategory>
			{
				NKCUnitSortSystem.eFilterCategory.Level,
				NKCUnitSortSystem.eFilterCategory.Locked,
				NKCUnitSortSystem.eFilterCategory.Decked
			};
			unitSelectListOptions.setUnitSortCategory = new HashSet<NKCUnitSortSystem.eSortCategory>
			{
				NKCUnitSortSystem.eSortCategory.Level,
				NKCUnitSortSystem.eSortCategory.UnitLoyalty,
				NKCUnitSortSystem.eSortCategory.Squad_Gauntlet
			};
			unitSelectListOptions.m_bHideUnitCount = true;
			unitSelectListOptions.m_bUseFavorite = false;
			unitSelectListOptions.bMultipleSelect = false;
			unitSelectListOptions.iMaxMultipleSelect = 1;
			this.UnitSelectList.Open(unitSelectListOptions, new NKCUIUnitSelectList.OnUnitSelectCommand(this.OnConsumeUnitSelected), null, null, null, null);
		}

		// Token: 0x060078D5 RID: 30933 RVA: 0x00281F08 File Offset: 0x00280108
		public void OnConsumeUnitSelected(List<long> selectedList)
		{
			if (this.m_UIUnitSelectList != null && this.m_UIUnitSelectList.IsOpen)
			{
				this.m_UIUnitSelectList.Close();
			}
			if (selectedList.Count <= 0 || selectedList.Count > 1)
			{
				return;
			}
			this.m_consumeUnitData = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(selectedList[0]);
			if (this.m_consumeUnitData != null)
			{
				this.m_UnitSelectListSlot.SetData(this.m_consumeUnitData, new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE), false, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnSelectUnitSlot));
			}
		}

		// Token: 0x060078D6 RID: 30934 RVA: 0x00281F96 File Offset: 0x00280196
		public void OnSelectUnitSlot(NKMUnitData unitData, NKMUnitTempletBase unitTempletBase, NKMDeckIndex deckIndex, NKCUnitSortSystem.eUnitState slotState, NKCUIUnitSelectList.eUnitSlotSelectState unitSlotSelectState)
		{
			if (this.m_curTacticUpdateStep == NKCUITacticUpdate.TACTIC_UPDATE_STEP.BACK)
			{
				return;
			}
			this.OnClickSelectResourceUnit((this.m_consumeUnitData == null) ? 0L : this.m_consumeUnitData.m_UnitUID);
		}

		// Token: 0x060078D7 RID: 30935 RVA: 0x00281FBF File Offset: 0x002801BF
		private void SetAni(string aniName)
		{
			this.m_Ani.SetTrigger(aniName);
		}

		// Token: 0x060078D8 RID: 30936 RVA: 0x00281FD0 File Offset: 0x002801D0
		public void OnRecv(NKMPacket_UNIT_TACTIC_UPDATE_ACK sPacket)
		{
			if (sPacket.unitData.m_UnitUID == this.m_targetUnitData.m_UnitUID)
			{
				this.m_targetUnitData = sPacket.unitData;
				if (this.m_targetUnitData.tacticLevel == 6)
				{
					this.UpdateTacticUpdateUI(NKCUITacticUpdate.TACTIC_UPDATE_STEP.INTRO, true);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objFX, true);
					this.m_FXAni.SetTrigger("ACTIVE");
					this.UpdateNodeUI(false);
				}
				if (this.m_targetUnitData.tacticLevel > 0 && this.m_targetUnitData.tacticLevel - 1 <= this.m_lstTacticUpdateNodeCenter.Count)
				{
					NKCUtil.SetGameobjectActive(this.m_lstTacticUpdateNodeCenter[this.m_targetUnitData.tacticLevel - 1].objFX, true);
				}
			}
			if (this.m_consumeUnitData.m_UnitUID == sPacket.consumeUnitUid)
			{
				this.m_consumeUnitData = null;
			}
			this.m_UnitSelectListSlot.SetEmpty(true, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnSelectUnitSlot), null);
		}

		// Token: 0x060078D9 RID: 30937 RVA: 0x002820C0 File Offset: 0x002802C0
		public static int CanThisUnitTactocUpdateNow(NKMUnitData unitData, NKMUserData userData)
		{
			if (unitData == null || userData == null)
			{
				return -1;
			}
			if (unitData.tacticLevel == 6)
			{
				return -1;
			}
			HashSet<int> hashSet = new HashSet<int>();
			hashSet.Add(unitData.GetUnitTemplet().m_UnitTempletBase.m_UnitID);
			foreach (int item in NKCRearmamentUtil.GetSameBaseUnitIDList(unitData))
			{
				hashSet.Add(item);
			}
			int num = 0;
			foreach (KeyValuePair<long, NKMUnitData> keyValuePair in userData.m_ArmyData.m_dicMyUnit)
			{
				if (keyValuePair.Key != unitData.m_UnitUID && !keyValuePair.Value.m_bLock && !unitData.IsSeized && hashSet.Contains(keyValuePair.Value.m_UnitID))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x04006541 RID: 25921
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_unit_info";

		// Token: 0x04006542 RID: 25922
		private const string UI_ASSET_NAME = "AB_UI_TACTIC_UPDATE";

		// Token: 0x04006543 RID: 25923
		private static NKCUITacticUpdate m_Instance;

		// Token: 0x04006544 RID: 25924
		public Animator m_Ani;

		// Token: 0x04006545 RID: 25925
		public Text m_lbName;

		// Token: 0x04006546 RID: 25926
		public Text m_lbTitle;

		// Token: 0x04006547 RID: 25927
		public NKCUICharacterView m_CharView;

		// Token: 0x04006548 RID: 25928
		public GameObject m_objReadyBtnON;

		// Token: 0x04006549 RID: 25929
		public GameObject m_objReadyBtnOFF;

		// Token: 0x0400654A RID: 25930
		public NKCUIComStateButton m_csbtnReady;

		// Token: 0x0400654B RID: 25931
		public NKCUITacticUpdateLevelSlot m_TacticUpdateLvSlot;

		// Token: 0x0400654C RID: 25932
		public NKCUIComStateButton m_csbtnRun;

		// Token: 0x0400654D RID: 25933
		[Header("right ui")]
		public NKCUITacticUpdateLevelSlot m_TargetTacticUpdateLvSlot;

		// Token: 0x0400654E RID: 25934
		public Image m_imgTargetUpdateIcon;

		// Token: 0x0400654F RID: 25935
		public Text m_lbTargetUpdateDesc;

		// Token: 0x04006550 RID: 25936
		public NKCUIUnitSelectListSlot m_UnitSelectListSlot;

		// Token: 0x04006551 RID: 25937
		[Header("FX")]
		public GameObject m_objFX;

		// Token: 0x04006552 RID: 25938
		public Animator m_FXAni;

		// Token: 0x04006553 RID: 25939
		private NKCUITacticUpdate.TACTIC_UPDATE_STEP m_curTacticUpdateStep;

		// Token: 0x04006554 RID: 25940
		private NKMUnitData m_targetUnitData;

		// Token: 0x04006555 RID: 25941
		private NKMUnitData m_consumeUnitData;

		// Token: 0x04006556 RID: 25942
		private int m_iUnEquipCnt;

		// Token: 0x04006557 RID: 25943
		public List<NKCUITacticUpdate.NodeSlot> m_lstTacticUpdateNodeCenter = new List<NKCUITacticUpdate.NodeSlot>();

		// Token: 0x04006558 RID: 25944
		public List<NKCUITacticUpdate.NodeSlot> m_lstTacticUpdateNodeBottom = new List<NKCUITacticUpdate.NodeSlot>();

		// Token: 0x04006559 RID: 25945
		private NKCUIUnitSelectList m_UIUnitSelectList;

		// Token: 0x0400655A RID: 25946
		private const string ANI_TRIGGER_INTRO = "INTRO";

		// Token: 0x0400655B RID: 25947
		private const string ANI_TRIGGER_READY = "READY";

		// Token: 0x0400655C RID: 25948
		private const string ANI_TRIGGER_BACK = "BACK";

		// Token: 0x0400655D RID: 25949
		private const string ANI_TRIGGER_READY_IDLE = "READY_IDLE";

		// Token: 0x0400655E RID: 25950
		private const string ACTIVE = "ACTIVE";

		// Token: 0x020017F9 RID: 6137
		private enum TACTIC_UPDATE_STEP
		{
			// Token: 0x0400A7C7 RID: 42951
			INTRO,
			// Token: 0x0400A7C8 RID: 42952
			READY,
			// Token: 0x0400A7C9 RID: 42953
			BACK
		}

		// Token: 0x020017FA RID: 6138
		[Serializable]
		public class NodeSlot
		{
			// Token: 0x0400A7CA RID: 42954
			public List<Image> lstIcon;

			// Token: 0x0400A7CB RID: 42955
			public List<Text> lbDesc;

			// Token: 0x0400A7CC RID: 42956
			public GameObject objON;

			// Token: 0x0400A7CD RID: 42957
			public GameObject objOFF;

			// Token: 0x0400A7CE RID: 42958
			public GameObject objFX;
		}
	}
}
