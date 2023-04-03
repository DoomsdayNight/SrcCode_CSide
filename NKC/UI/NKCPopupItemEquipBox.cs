using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A68 RID: 2664
	public class NKCPopupItemEquipBox : NKCUIBase
	{
		// Token: 0x06007563 RID: 30051 RVA: 0x00270A61 File Offset: 0x0026EC61
		public static NKCPopupItemEquipBox OpenInstance()
		{
			NKCPopupItemEquipBox instance = NKCUIManager.OpenNewInstance<NKCPopupItemEquipBox>("AB_UI_ITEM_EQUIP_SLOT_CARD", "NKM_UI_POPUP_ITEM_EQUIP_BOX", NKCUIManager.eUIBaseRect.UIFrontPopup, null).GetInstance<NKCPopupItemEquipBox>();
			if (instance == null)
			{
				return instance;
			}
			instance.InitUI();
			return instance;
		}

		// Token: 0x1700138E RID: 5006
		// (get) Token: 0x06007564 RID: 30052 RVA: 0x00270A84 File Offset: 0x0026EC84
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700138F RID: 5007
		// (get) Token: 0x06007565 RID: 30053 RVA: 0x00270A87 File Offset: 0x0026EC87
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_POPUP_ITEM_EQUIP_BOX;
			}
		}

		// Token: 0x06007566 RID: 30054 RVA: 0x00270A90 File Offset: 0x0026EC90
		public void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			this.m_UnEquipButton.PointerClick.RemoveAllListeners();
			this.m_UnEquipButton.PointerClick.AddListener(new UnityAction(this.OnClickUnEquip));
			this.m_ReinforceButton.PointerClick.RemoveAllListeners();
			this.m_ReinforceButton.PointerClick.AddListener(new UnityAction(this.OnClickEquipEnhance));
			this.m_ReinforceButtonLock.PointerClick.RemoveAllListeners();
			this.m_ReinforceButtonLock.PointerClick.AddListener(new UnityAction(this.OnClickEquipEnhance));
			this.m_EquipButton.PointerClick.RemoveAllListeners();
			this.m_EquipButton.PointerClick.AddListener(new UnityAction(this.OnClickEquipBtn));
			NKCUtil.SetHotkey(this.m_EquipButton, HotkeyEventType.Confirm);
			this.m_ChangeButton.PointerClick.RemoveAllListeners();
			this.m_ChangeButton.PointerClick.AddListener(new UnityAction(this.OnClickEquipBtn));
			NKCUtil.SetHotkey(this.m_ChangeButton, HotkeyEventType.Confirm);
			this.m_OkButton.PointerClick.RemoveAllListeners();
			this.m_OkButton.PointerClick.AddListener(new UnityAction(base.Close));
			NKCUtil.SetHotkey(this.m_OkButton, HotkeyEventType.Confirm);
			this.m_NKM_UI_POPUP_OK_BOX_CANCEL.PointerClick.RemoveAllListeners();
			this.m_NKM_UI_POPUP_OK_BOX_CANCEL.PointerClick.AddListener(new UnityAction(base.Close));
			base.gameObject.SetActive(false);
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CHANGE_TEXT_1, NKCUtilString.GET_STRING_EQUIP_SELECT_ACC_1);
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CHANGE_TEXT_2, NKCUtilString.GET_STRING_EQUIP_SELECT_ACC_2);
		}

		// Token: 0x06007567 RID: 30055 RVA: 0x00270C34 File Offset: 0x0026EE34
		public static void Open(NKMEquipItemData cNKMEquipItemData, NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE bottomMenuType = NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_ENFORCE_AND_EQUIP, NKCPopupItemEquipBox.OnButton dOnEquipButton = null)
		{
			if (cNKMEquipItemData == null)
			{
				return;
			}
			if (NKCPopupItemEquipBox.m_Popup == null)
			{
				NKCPopupItemEquipBox.m_Popup = NKCPopupItemEquipBox.OpenInstance();
			}
			NKCPopupItemEquipBox.m_Popup.dOnEquipButton = dOnEquipButton;
			NKCPopupItemEquipBox.m_Popup.m_ChangeButton.PointerClick.RemoveAllListeners();
			NKCPopupItemEquipBox.m_Popup.m_ChangeButton.PointerClick.AddListener(new UnityAction(NKCPopupItemEquipBox.m_Popup.OnClickEquipBtn));
			if (NKMItemManager.GetEquipTemplet(cNKMEquipItemData.m_ItemEquipID) != null)
			{
				NKCUtil.SetLabelTextColor(NKCPopupItemEquipBox.m_Popup.m_txtNKM_UI_POPUP_REINFORCE_TEXT, NKCUtil.GetColor("#FFFFFF"));
				NKCUtil.SetImageSprite(NKCPopupItemEquipBox.m_Popup.m_imgNKM_UI_POPUP_OK_BOX_REINFORCE, NKCUtil.GetButtonSprite(NKCUtil.ButtonColor.BC_BLUE), false);
			}
			NKCPopupItemEquipBox.m_Popup.gameObject.SetActive(true);
			NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_lbTitle, false);
			NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_objUpgradeEffect, false);
			NKCPopupItemEquipBox.m_Popup.m_NKCUIInvenEquipSlot.SetData(cNKMEquipItemData, false, false);
			NKCPopupItemEquipBox.m_Popup.SetTextFromSlotdata(cNKMEquipItemData, bottomMenuType, true);
			NKCPopupItemEquipBox.m_Popup.HideEquipSelectUI();
			NKCPopupItemEquipBox.m_Popup.SetSlotData(cNKMEquipItemData, false);
			NKCPopupItemEquipBox.m_Popup.m_NKCUIOpenAnimator.PlayOpenAni();
			NKCPopupItemEquipBox.m_Popup.UIOpened(true);
		}

		// Token: 0x06007568 RID: 30056 RVA: 0x00270D5C File Offset: 0x0026EF5C
		public static void OpenForConfirm(NKMEquipItemData cNKMEquipItemData, UnityAction dOnEquipButton = null, bool bFierceInfo = false, bool bShowCancel = true, NKCPopupItemEquipBox.OnButton dOnClose = null)
		{
			if (cNKMEquipItemData == null)
			{
				return;
			}
			if (NKCPopupItemEquipBox.m_Popup == null)
			{
				NKCPopupItemEquipBox.m_Popup = NKCPopupItemEquipBox.OpenInstance();
			}
			if (dOnEquipButton != null)
			{
				NKCPopupItemEquipBox.m_Popup.m_OkButton.PointerClick.RemoveAllListeners();
				NKCPopupItemEquipBox.m_Popup.m_OkButton.PointerClick.AddListener(new UnityAction(NKCPopupItemEquipBox.m_Popup.Close));
				NKCPopupItemEquipBox.m_Popup.m_OkButton.PointerClick.AddListener(dOnEquipButton);
			}
			NKCPopupItemEquipBox.m_Popup.m_dOnClose = dOnClose;
			NKCPopupItemEquipBox.m_Popup.gameObject.SetActive(true);
			NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_lbTitle, false);
			NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_objUpgradeEffect, false);
			NKCPopupItemEquipBox.m_Popup.m_NKCUIInvenEquipSlot.SetData(cNKMEquipItemData, bFierceInfo, false);
			NKCPopupItemEquipBox.m_Popup.SetTextFromSlotdata(cNKMEquipItemData, NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_NONE, false);
			NKCPopupItemEquipBox.m_Popup.HideEquipSelectUI();
			NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_NKM_UI_POPUP_OK_BOX_CANCEL.gameObject, bShowCancel);
			NKCPopupItemEquipBox.m_Popup.SetSlotData(cNKMEquipItemData, false);
			NKCPopupItemEquipBox.m_Popup.m_NKCUIOpenAnimator.PlayOpenAni();
			NKCPopupItemEquipBox.m_Popup.UIOpened(true);
		}

		// Token: 0x06007569 RID: 30057 RVA: 0x00270E74 File Offset: 0x0026F074
		public static void OpenForChange(NKMEquipItemData cNKMEquipItemData, long unitUID, bool bShowFierceInfo = false)
		{
			if (cNKMEquipItemData == null)
			{
				return;
			}
			if (NKCPopupItemEquipBox.m_Popup == null)
			{
				NKCPopupItemEquipBox.m_Popup = NKCPopupItemEquipBox.OpenInstance();
			}
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(cNKMEquipItemData.m_ItemEquipID);
			if (equipTemplet != null)
			{
				NKCPopupItemEquipBox.m_Popup.m_ITEM_EQUIP_POSITION_For_Change = equipTemplet.m_ItemEquipPosition;
				if (equipTemplet.m_ItemEquipPosition == ITEM_EQUIP_POSITION.IEP_ACC && cNKMEquipItemData.m_OwnerUnitUID > 0L)
				{
					NKMUnitData unitFromUID = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetUnitFromUID(cNKMEquipItemData.m_OwnerUnitUID);
					if (unitFromUID != null && unitFromUID.GetEquipItemAccessory2Uid() == cNKMEquipItemData.m_ItemUid)
					{
						NKCPopupItemEquipBox.m_Popup.m_ITEM_EQUIP_POSITION_For_Change = ITEM_EQUIP_POSITION.IEP_ACC2;
					}
				}
			}
			NKCPopupItemEquipBox.m_Popup.m_SelectedItemUID = cNKMEquipItemData.m_ItemUid;
			NKCPopupItemEquipBox.m_Popup.m_UnitUIDForChange = unitUID;
			NKCPopupItemEquipBox.m_Popup.m_bShowFierceInfo = bShowFierceInfo;
			NKCPopupItemEquipBox.m_Popup.gameObject.SetActive(true);
			NKCPopupItemEquipBox.m_Popup.m_ChangeButton.PointerClick.RemoveAllListeners();
			NKCPopupItemEquipBox.m_Popup.m_ChangeButton.PointerClick.AddListener(new UnityAction(NKCPopupItemEquipBox.m_Popup.OnClickChange));
			NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_lbTitle, false);
			NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_objUpgradeEffect, false);
			NKCPopupItemEquipBox.m_Popup.m_NKCUIInvenEquipSlot.SetData(cNKMEquipItemData, bShowFierceInfo, false);
			NKCPopupItemEquipBox.m_Popup.SetTextFromSlotdata(cNKMEquipItemData, NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_CHANGE, true);
			NKCPopupItemEquipBox.m_Popup.HideEquipSelectUI();
			NKCPopupItemEquipBox.m_Popup.SetSlotData(cNKMEquipItemData, bShowFierceInfo);
			NKCPopupItemEquipBox.m_Popup.m_NKCUIOpenAnimator.PlayOpenAni();
			NKCPopupItemEquipBox.m_Popup.UIOpened(true);
		}

		// Token: 0x0600756A RID: 30058 RVA: 0x00270FE4 File Offset: 0x0026F1E4
		public static void OpenForPresetChange(long unitUID, long equipItemUId, ITEM_EQUIP_POSITION equipPosition, int presetIndex, List<long> presetEquipList, bool bShowFierceInfo = false)
		{
			NKMEquipItemData itemEquip = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(equipItemUId);
			if (itemEquip == null)
			{
				return;
			}
			if (NKCPopupItemEquipBox.m_Popup == null)
			{
				NKCPopupItemEquipBox.m_Popup = NKCPopupItemEquipBox.OpenInstance();
			}
			NKCPopupItemEquipBox.m_Popup.m_ITEM_EQUIP_POSITION_For_Change = equipPosition;
			NKCPopupItemEquipBox.m_Popup.m_SelectedItemUID = itemEquip.m_ItemUid;
			NKCPopupItemEquipBox.m_Popup.m_UnitUIDForChange = unitUID;
			NKCPopupItemEquipBox.m_Popup.m_bShowFierceInfo = bShowFierceInfo;
			NKCPopupItemEquipBox.m_Popup.m_iPresetIndex = presetIndex;
			NKCPopupItemEquipBox.m_Popup.m_listPresetEquip = presetEquipList;
			NKCPopupItemEquipBox.m_Popup.gameObject.SetActive(true);
			NKCPopupItemEquipBox.m_Popup.m_ChangeButton.PointerClick.RemoveAllListeners();
			NKCPopupItemEquipBox.m_Popup.m_ChangeButton.PointerClick.AddListener(new UnityAction(NKCPopupItemEquipBox.m_Popup.OnClickPresetChange));
			NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_lbTitle, false);
			NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_objUpgradeEffect, false);
			NKCPopupItemEquipBox.m_Popup.m_NKCUIInvenEquipSlot.SetData(itemEquip, bShowFierceInfo, false);
			NKCPopupItemEquipBox.m_Popup.SetTextFromSlotdata(itemEquip, NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_PRESET_CHANGE, true);
			NKCPopupItemEquipBox.m_Popup.HideEquipSelectUI();
			NKCPopupItemEquipBox.m_Popup.SetSlotData(itemEquip, bShowFierceInfo);
			NKCPopupItemEquipBox.m_Popup.m_UnEquipButton.PointerClick.RemoveAllListeners();
			NKCPopupItemEquipBox.m_Popup.m_UnEquipButton.PointerClick.AddListener(new UnityAction(NKCPopupItemEquipBox.m_Popup.OnClickPresetUnEquip));
			NKCPopupItemEquipBox.m_Popup.m_NKCUIOpenAnimator.PlayOpenAni();
			NKCPopupItemEquipBox.m_Popup.UIOpened(true);
		}

		// Token: 0x0600756B RID: 30059 RVA: 0x0027115C File Offset: 0x0026F35C
		public static void OpenForSelectItem(long itemUID1, long itemUID2, UnityAction func1, UnityAction func2)
		{
			if (NKCPopupItemEquipBox.m_Popup == null)
			{
				NKCPopupItemEquipBox.m_Popup = NKCPopupItemEquipBox.OpenInstance();
			}
			NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_UnEquipButton, false);
			NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_ReinforceButton, false);
			NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_ReinforceButtonLock, false);
			NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_EquipButton, false);
			NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_ChangeButton, false);
			NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_OkButton, false);
			NKMEquipItemData itemEquip = NKCScenManager.CurrentUserData().m_InventoryData.GetItemEquip(itemUID1);
			NKMEquipItemData itemEquip2 = NKCScenManager.CurrentUserData().m_InventoryData.GetItemEquip(itemUID2);
			if (itemEquip != null && itemEquip2 != null)
			{
				if (itemEquip.m_OwnerUnitUID > 0L)
				{
					NKCScenManager.CurrentUserData();
					NKMUnitData unitFromUID = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetUnitFromUID(itemEquip.m_OwnerUnitUID);
					if (unitFromUID != null)
					{
						NKCUIUnitSelectListSlot nkm_UI_UNIT_SELECT_LIST_UNIT_SLOT = NKCPopupItemEquipBox.m_Popup.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT;
						if (nkm_UI_UNIT_SELECT_LIST_UNIT_SLOT != null)
						{
							nkm_UI_UNIT_SELECT_LIST_UNIT_SLOT.SetData(unitFromUID, NKMDeckIndex.None, false, null);
						}
						NKCUIUnitSelectListSlot nkm_UI_UNIT_SELECT_LIST_UNIT_SLOT2 = NKCPopupItemEquipBox.m_Popup.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT;
						if (nkm_UI_UNIT_SELECT_LIST_UNIT_SLOT2 != null)
						{
							nkm_UI_UNIT_SELECT_LIST_UNIT_SLOT2.SetSlotState(NKCUnitSortSystem.eUnitState.NONE);
						}
					}
				}
				NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_NKM_UI_POPUP_ITEM_EQUIP_UNIT, itemEquip.m_OwnerUnitUID > 0L);
				NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_lbTitle, false);
				NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_objUpgradeEffect, false);
				NKCUIInvenEquipSlot nkcuiinvenEquipSlot = NKCPopupItemEquipBox.m_Popup.m_NKCUIInvenEquipSlot;
				if (nkcuiinvenEquipSlot != null)
				{
					nkcuiinvenEquipSlot.SetData(itemEquip, false, false);
				}
				NKCUIInvenEquipSlot nkcuiinvenEquipSlot2 = NKCPopupItemEquipBox.m_Popup.m_NKCUIInvenEquipSlot2;
				if (nkcuiinvenEquipSlot2 != null)
				{
					nkcuiinvenEquipSlot2.SetData(itemEquip2, false, false);
				}
				if (NKCPopupItemEquipBox.m_Popup.m_NKM_UI_POPUP_OK_BOX_SELECT_1 != null)
				{
					NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_NKM_UI_POPUP_OK_BOX_SELECT_1.gameObject, true);
					NKCPopupItemEquipBox.m_Popup.m_NKM_UI_POPUP_OK_BOX_SELECT_1.PointerClick.RemoveAllListeners();
					NKCPopupItemEquipBox.m_Popup.m_NKM_UI_POPUP_OK_BOX_SELECT_1.PointerClick.AddListener(func1);
				}
				if (NKCPopupItemEquipBox.m_Popup.m_NKM_UI_POPUP_OK_BOX_SELECT_2 != null)
				{
					NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_NKM_UI_POPUP_OK_BOX_SELECT_2.gameObject, true);
					NKCPopupItemEquipBox.m_Popup.m_NKM_UI_POPUP_OK_BOX_SELECT_2.PointerClick.RemoveAllListeners();
					NKCPopupItemEquipBox.m_Popup.m_NKM_UI_POPUP_OK_BOX_SELECT_2.PointerClick.AddListener(func2);
				}
				NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_NKM_UI_POPUP_OK_BOX_TOP_TEXT, true);
				NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_NKM_UI_POPUP_ITEM_ICON_02, true);
				NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_NKM_UI_POPUP_ITEM_SLOT_01_NUMBER, true);
				NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_NKM_UI_POPUP_ITEM_SLOT_02_NUMBER, true);
				NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_NKM_UI_POPUP_OK_BOX_CANCEL.gameObject, true);
				NKCPopupItemEquipBox.m_Popup.m_NKCUIOpenAnimator.PlayOpenAni();
				NKCPopupItemEquipBox.m_Popup.UIOpened(true);
			}
		}

		// Token: 0x0600756C RID: 30060 RVA: 0x002713E7 File Offset: 0x0026F5E7
		private void OnClickEmptySlot(NKCUISlotEquip cItemSlot, NKMEquipItemData equipData)
		{
			if (this.m_NKCUIInvenEquipSlot.GetNKMEquipTemplet() == null)
			{
				return;
			}
			if (this.m_NKCUIInvenEquipSlot.GetNKMEquipItemData() == null)
			{
				return;
			}
			NKMItemManager.UnEquip(this.m_NKCUIInvenEquipSlot.GetNKMEquipItemData().m_ItemUid);
		}

		// Token: 0x0600756D RID: 30061 RVA: 0x0027141A File Offset: 0x0026F61A
		public void OnClickChange()
		{
			NKCUtil.ChangeEquip(this.m_UnitUIDForChange, this.m_ITEM_EQUIP_POSITION_For_Change, new NKCUISlotEquip.OnSelectedEquipSlot(this.OnClickEmptySlot), this.m_SelectedItemUID, this.m_bShowFierceInfo);
		}

		// Token: 0x0600756E RID: 30062 RVA: 0x00271448 File Offset: 0x0026F648
		public void OnClickPresetChange()
		{
			if (this.m_NKCUIInvenEquipSlot.GetNKMEquipTemplet() == null)
			{
				return;
			}
			int iPresetIndex = this.m_iPresetIndex;
			NKCUtil.ChangePresetEquip(this.m_UnitUIDForChange, this.m_iPresetIndex, this.m_SelectedItemUID, this.m_listPresetEquip, this.m_ITEM_EQUIP_POSITION_For_Change, this.m_NKCUIInvenEquipSlot.GetNKMEquipTemplet().m_EquipUnitStyleType, this.m_bShowFierceInfo, delegate(NKCUISlotEquip cItemSlot, NKMEquipItemData equipData)
			{
				NKCPacketSender.Send_NKMPacket_EQUIP_PRESET_REGISTER_REQ(iPresetIndex, this.m_ITEM_EQUIP_POSITION_For_Change, 0L);
			});
		}

		// Token: 0x0600756F RID: 30063 RVA: 0x002714C1 File Offset: 0x0026F6C1
		private void OnClickEquipBtn()
		{
			if (this.dOnEquipButton != null)
			{
				this.dOnEquipButton();
				this.dOnEquipButton = null;
			}
		}

		// Token: 0x06007570 RID: 30064 RVA: 0x002714E0 File Offset: 0x0026F6E0
		public void OnClickEquipEnhance()
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
			if (this.m_NKCUIInvenEquipSlot != null && this.m_NKCUIInvenEquipSlot.GetNKMEquipItemData() != null)
			{
				NKM_ERROR_CODE nkm_ERROR_CODE = NKMItemManager.CanEnchantItem(NKCScenManager.GetScenManager().GetMyUserData(), this.m_NKCUIInvenEquipSlot.GetNKMEquipItemData());
				if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
				{
					NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString(nkm_ERROR_CODE.ToString(), false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
				base.Close();
				NKCUIForge.Instance.Open(NKCUIForge.NKC_FORGE_TAB.NFT_ENCHANT, this.m_NKCUIInvenEquipSlot.GetNKMEquipItemData().m_ItemUid, null);
			}
		}

		// Token: 0x06007571 RID: 30065 RVA: 0x00271594 File Offset: 0x0026F794
		public void OnClickUnEquip()
		{
			NKMItemManager.UnEquip(this.m_NKCUIInvenEquipSlot.GetEquipItemUID());
		}

		// Token: 0x06007572 RID: 30066 RVA: 0x002715A6 File Offset: 0x0026F7A6
		public void OnClickPresetUnEquip()
		{
			NKCPacketSender.Send_NKMPacket_EQUIP_PRESET_REGISTER_REQ(this.m_iPresetIndex, this.m_ITEM_EQUIP_POSITION_For_Change, 0L);
		}

		// Token: 0x06007573 RID: 30067 RVA: 0x002715BC File Offset: 0x0026F7BC
		private void SetTextFromSlotdata(NKMEquipItemData cNKMEquipItemData, NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE bottomMenuType = NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_ENFORCE_AND_EQUIP, bool bInitOKBtn = true)
		{
			if (cNKMEquipItemData == null)
			{
				return;
			}
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(cNKMEquipItemData.m_ItemEquipID);
			if (equipTemplet == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_OK_ROOT, true);
			if (bottomMenuType == NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_NONE)
			{
				NKCUtil.SetGameobjectActive(this.m_UnEquipButton, false);
				NKCUtil.SetGameobjectActive(this.m_ReinforceButton, false);
				NKCUtil.SetGameobjectActive(this.m_ReinforceButtonLock, false);
				NKCUtil.SetGameobjectActive(this.m_EquipButton, false);
				NKCUtil.SetGameobjectActive(this.m_ChangeButton, false);
				NKCUtil.SetGameobjectActive(this.m_OkButton, true);
			}
			else if (bottomMenuType == NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_PRESET_CHANGE)
			{
				NKCUtil.SetGameobjectActive(this.m_UnEquipButton, true);
				NKCUtil.SetGameobjectActive(this.m_ReinforceButton, NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_ENCHANT, 0, 0) && equipTemplet.m_ItemEquipPosition != ITEM_EQUIP_POSITION.IEP_ENCHANT);
				NKCUtil.SetGameobjectActive(this.m_ReinforceButtonLock, !NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_ENCHANT, 0, 0) && equipTemplet.m_ItemEquipPosition != ITEM_EQUIP_POSITION.IEP_ENCHANT);
				NKCUtil.SetGameobjectActive(this.m_EquipButton, false);
				NKCUtil.SetGameobjectActive(this.m_ChangeButton, true);
				NKCUtil.SetGameobjectActive(this.m_OkButton, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_UnEquipButton, cNKMEquipItemData.m_OwnerUnitUID > 0L);
				NKCUtil.SetGameobjectActive(this.m_ReinforceButton, NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_ENCHANT, 0, 0) && equipTemplet.m_ItemEquipPosition != ITEM_EQUIP_POSITION.IEP_ENCHANT);
				NKCUtil.SetGameobjectActive(this.m_ReinforceButtonLock, !NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_ENCHANT, 0, 0) && equipTemplet.m_ItemEquipPosition != ITEM_EQUIP_POSITION.IEP_ENCHANT);
				NKCUtil.SetGameobjectActive(this.m_EquipButton, cNKMEquipItemData.m_OwnerUnitUID <= 0L && equipTemplet.m_ItemEquipPosition != ITEM_EQUIP_POSITION.IEP_ENCHANT);
				NKCUtil.SetGameobjectActive(this.m_ChangeButton, cNKMEquipItemData.m_OwnerUnitUID > 0L);
				NKCUtil.SetGameobjectActive(this.m_OkButton, equipTemplet.m_ItemEquipPosition == ITEM_EQUIP_POSITION.IEP_ENCHANT);
			}
			if (bInitOKBtn)
			{
				this.m_OkButton.PointerClick.RemoveAllListeners();
				this.m_OkButton.PointerClick.AddListener(new UnityAction(base.Close));
			}
		}

		// Token: 0x06007574 RID: 30068 RVA: 0x00271797 File Offset: 0x0026F997
		public static void ShowTitle(string title)
		{
			if (NKCPopupItemEquipBox.m_Popup != null)
			{
				NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_lbTitle, true);
				NKCUtil.SetLabelText(NKCPopupItemEquipBox.m_Popup.m_lbTitle, title);
			}
		}

		// Token: 0x06007575 RID: 30069 RVA: 0x002717C6 File Offset: 0x0026F9C6
		public static void ShowUpgradeCompleteEffect()
		{
			if (NKCPopupItemEquipBox.m_Popup != null)
			{
				NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_objUpgradeEffect, true);
			}
		}

		// Token: 0x06007576 RID: 30070 RVA: 0x002717E8 File Offset: 0x0026F9E8
		private void HideEquipSelectUI()
		{
			NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_NKM_UI_POPUP_OK_BOX_SELECT_1.gameObject, false);
			NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_NKM_UI_POPUP_OK_BOX_SELECT_2.gameObject, false);
			NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_NKM_UI_POPUP_ITEM_ICON_02, false);
			NKCUtil.SetGameobjectActive(NKCPopupItemEquipBox.m_Popup.m_NKM_UI_POPUP_OK_BOX_TOP_TEXT, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_ITEM_SLOT_01_NUMBER, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_OK_BOX_CANCEL.gameObject, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_OK_BOX_SELECT_1.gameObject, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_OK_BOX_SELECT_2.gameObject, false);
		}

		// Token: 0x06007577 RID: 30071 RVA: 0x0027187E File Offset: 0x0026FA7E
		private void SetSlotData(NKMEquipItemData equipItemData, bool bShowFierceInfo = false)
		{
			if (equipItemData == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_ITEM_SLOT_01_NUMBER, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_ITEM_SLOT_02_NUMBER, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_ITEM_EQUIP_UNIT, false);
		}

		// Token: 0x06007578 RID: 30072 RVA: 0x002718A8 File Offset: 0x0026FAA8
		public void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x06007579 RID: 30073 RVA: 0x002718BD File Offset: 0x0026FABD
		public static void CloseItemBox()
		{
			if (NKCPopupItemEquipBox.m_Popup != null && NKCPopupItemEquipBox.m_Popup.IsOpen)
			{
				NKCPopupItemEquipBox.m_Popup.Close();
			}
		}

		// Token: 0x0600757A RID: 30074 RVA: 0x002718E2 File Offset: 0x0026FAE2
		public void OnOK()
		{
			base.Close();
		}

		// Token: 0x0600757B RID: 30075 RVA: 0x002718EC File Offset: 0x0026FAEC
		public override void CloseInternal()
		{
			this.m_UnEquipButton.PointerClick.RemoveAllListeners();
			this.m_UnEquipButton.PointerClick.AddListener(new UnityAction(this.OnClickUnEquip));
			this.m_iPresetIndex = -1;
			this.m_listPresetEquip = null;
			base.gameObject.SetActive(false);
			if (this.m_dOnClose != null)
			{
				this.m_dOnClose();
				this.m_dOnClose = null;
			}
		}

		// Token: 0x040061B2 RID: 25010
		private const string ASSET_BUNDLE_NAME = "AB_UI_ITEM_EQUIP_SLOT_CARD";

		// Token: 0x040061B3 RID: 25011
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_ITEM_EQUIP_BOX";

		// Token: 0x040061B4 RID: 25012
		private static NKCPopupItemEquipBox m_Popup;

		// Token: 0x040061B5 RID: 25013
		public Text m_lbTitle;

		// Token: 0x040061B6 RID: 25014
		public NKCUIInvenEquipSlot m_NKCUIInvenEquipSlot;

		// Token: 0x040061B7 RID: 25015
		public GameObject m_NKM_UI_POPUP_OK_ROOT;

		// Token: 0x040061B8 RID: 25016
		public GameObject m_NKM_UI_POPUP_OK_BOX_UNEQUIP;

		// Token: 0x040061B9 RID: 25017
		public GameObject m_NKM_UI_POPUP_OK_BOX_REINFORCE;

		// Token: 0x040061BA RID: 25018
		public GameObject m_NKM_UI_POPUP_OK_BOX_EQUIP;

		// Token: 0x040061BB RID: 25019
		public GameObject m_NKM_UI_POPUP_OK_BOX_CHANGE;

		// Token: 0x040061BC RID: 25020
		public Image m_imgNKM_UI_POPUP_OK_BOX_REINFORCE;

		// Token: 0x040061BD RID: 25021
		public Text m_txtNKM_UI_POPUP_REINFORCE_TEXT;

		// Token: 0x040061BE RID: 25022
		public NKCUIComButton m_UnEquipButton;

		// Token: 0x040061BF RID: 25023
		public NKCUIComButton m_ReinforceButton;

		// Token: 0x040061C0 RID: 25024
		public NKCUIComButton m_ReinforceButtonLock;

		// Token: 0x040061C1 RID: 25025
		public NKCUIComButton m_EquipButton;

		// Token: 0x040061C2 RID: 25026
		public NKCUIComButton m_ChangeButton;

		// Token: 0x040061C3 RID: 25027
		public NKCUIComButton m_OkButton;

		// Token: 0x040061C4 RID: 25028
		[Header("업그레이드 완료창 전용")]
		public GameObject m_objUpgradeEffect;

		// Token: 0x040061C5 RID: 25029
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x040061C6 RID: 25030
		private NKCPopupItemEquipBox.OnButton dOnEquipButton;

		// Token: 0x040061C7 RID: 25031
		public NKCPopupItemEquipBox.OnButton m_dOnClose;

		// Token: 0x040061C8 RID: 25032
		private long m_UnitUIDForChange;

		// Token: 0x040061C9 RID: 25033
		private ITEM_EQUIP_POSITION m_ITEM_EQUIP_POSITION_For_Change;

		// Token: 0x040061CA RID: 25034
		private long m_SelectedItemUID;

		// Token: 0x040061CB RID: 25035
		[Header("equip set option")]
		public GameObject m_NKM_UI_POPUP_ITEM_EQUIP_UNIT;

		// Token: 0x040061CC RID: 25036
		public NKCUIUnitSelectListSlot m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT;

		// Token: 0x040061CD RID: 25037
		[Header("select item")]
		public GameObject m_NKM_UI_POPUP_OK_BOX_TOP_TEXT;

		// Token: 0x040061CE RID: 25038
		public NKCUIComStateButton m_NKM_UI_POPUP_OK_BOX_CANCEL;

		// Token: 0x040061CF RID: 25039
		public GameObject m_NKM_UI_POPUP_ITEM_ICON_02;

		// Token: 0x040061D0 RID: 25040
		public GameObject m_NKM_UI_POPUP_ITEM_SLOT_01_NUMBER;

		// Token: 0x040061D1 RID: 25041
		public GameObject m_NKM_UI_POPUP_ITEM_SLOT_02_NUMBER;

		// Token: 0x040061D2 RID: 25042
		public NKCUIInvenEquipSlot m_NKCUIInvenEquipSlot2;

		// Token: 0x040061D3 RID: 25043
		public NKCUIComButton m_NKM_UI_POPUP_OK_BOX_SELECT_1;

		// Token: 0x040061D4 RID: 25044
		public NKCUIComButton m_NKM_UI_POPUP_OK_BOX_SELECT_2;

		// Token: 0x040061D5 RID: 25045
		public Text m_NKM_UI_POPUP_CHANGE_TEXT_1;

		// Token: 0x040061D6 RID: 25046
		public Text m_NKM_UI_POPUP_CHANGE_TEXT_2;

		// Token: 0x040061D7 RID: 25047
		private bool m_bShowFierceInfo;

		// Token: 0x040061D8 RID: 25048
		private int m_iPresetIndex = -1;

		// Token: 0x040061D9 RID: 25049
		private List<long> m_listPresetEquip;

		// Token: 0x020017C8 RID: 6088
		public enum EQUIP_BOX_BOTTOM_MENU_TYPE
		{
			// Token: 0x0400A786 RID: 42886
			EBBMT_NONE,
			// Token: 0x0400A787 RID: 42887
			EBBMT_ENFORCE_AND_EQUIP,
			// Token: 0x0400A788 RID: 42888
			EBBMT_CHANGE,
			// Token: 0x0400A789 RID: 42889
			EBBMT_PRESET_CHANGE,
			// Token: 0x0400A78A RID: 42890
			EBBMT_OK
		}

		// Token: 0x020017C9 RID: 6089
		// (Invoke) Token: 0x0600B432 RID: 46130
		public delegate void OnButton();
	}
}
