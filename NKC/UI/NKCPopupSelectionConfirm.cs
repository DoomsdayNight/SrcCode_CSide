using System;
using System.Collections.Generic;
using NKC.UI.Shop;
using NKM;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A7E RID: 2686
	public class NKCPopupSelectionConfirm : NKCUIBase
	{
		// Token: 0x170013D8 RID: 5080
		// (get) Token: 0x060076F8 RID: 30456 RVA: 0x0027939C File Offset: 0x0027759C
		public static NKCPopupSelectionConfirm Instance
		{
			get
			{
				if (NKCPopupSelectionConfirm.m_Instance == null)
				{
					NKCPopupSelectionConfirm.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupSelectionConfirm>("ab_ui_nkm_ui_popup_selection", "NKM_UI_POPUP_SELECTION_CONFIRM", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupSelectionConfirm.CleanupInstance)).GetInstance<NKCPopupSelectionConfirm>();
					NKCPopupSelectionConfirm.m_Instance.InitUI();
				}
				return NKCPopupSelectionConfirm.m_Instance;
			}
		}

		// Token: 0x060076F9 RID: 30457 RVA: 0x002793EB File Offset: 0x002775EB
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupSelectionConfirm.m_Instance != null && NKCPopupSelectionConfirm.m_Instance.IsOpen)
			{
				NKCPopupSelectionConfirm.m_Instance.Close();
			}
		}

		// Token: 0x060076FA RID: 30458 RVA: 0x00279410 File Offset: 0x00277610
		private static void CleanupInstance()
		{
			NKCPopupSelectionConfirm.m_Instance = null;
		}

		// Token: 0x170013D9 RID: 5081
		// (get) Token: 0x060076FB RID: 30459 RVA: 0x00279418 File Offset: 0x00277618
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170013DA RID: 5082
		// (get) Token: 0x060076FC RID: 30460 RVA: 0x0027941B File Offset: 0x0027761B
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x170013DB RID: 5083
		// (get) Token: 0x060076FD RID: 30461 RVA: 0x00279422 File Offset: 0x00277622
		private int UseCount
		{
			get
			{
				if (this.m_NKMItemMiscTemplet.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_CHOICE_MISC)
				{
					return (int)this.m_misc.m_useCount;
				}
				return 1;
			}
		}

		// Token: 0x060076FE RID: 30462 RVA: 0x00279441 File Offset: 0x00277641
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060076FF RID: 30463 RVA: 0x00279450 File Offset: 0x00277650
		public void InitUI()
		{
			this.m_btnOk.PointerClick.RemoveAllListeners();
			this.m_btnOk.PointerClick.AddListener(new UnityAction(this.OnClickOk));
			NKCUtil.SetHotkey(this.m_btnOk, HotkeyEventType.Confirm, null, false);
			this.m_btnCancel.PointerClick.RemoveAllListeners();
			this.m_btnCancel.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_misc.Init();
			this.m_skin.Init();
		}

		// Token: 0x06007700 RID: 30464 RVA: 0x002794DC File Offset: 0x002776DC
		public void Open(NKMItemMiscTemplet itemMiscTemplet, int id, long count = 1L, int setItemID = 0)
		{
			if (itemMiscTemplet == null)
			{
				return;
			}
			this.m_NKMItemMiscTemplet = itemMiscTemplet;
			this.m_targetID = id;
			this.m_targetCount = count;
			this.m_setOptionID = setItemID;
			NKCUtil.SetGameobjectActive(this.m_unit, itemMiscTemplet.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_CHOICE_UNIT);
			NKCUtil.SetGameobjectActive(this.m_ship, itemMiscTemplet.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_CHOICE_SHIP);
			NKCUtil.SetGameobjectActive(this.m_equip, itemMiscTemplet.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_CHOICE_EQUIP);
			NKCUtil.SetGameobjectActive(this.m_misc, itemMiscTemplet.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_CHOICE_MISC);
			NKCUtil.SetGameobjectActive(this.m_operator, itemMiscTemplet.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_CHOICE_OPERATOR);
			NKCUtil.SetGameobjectActive(this.m_skin, itemMiscTemplet.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_CHOICE_SKIN);
			NKM_ITEM_MISC_TYPE itemMiscType = itemMiscTemplet.m_ItemMiscType;
			switch (itemMiscType)
			{
			case NKM_ITEM_MISC_TYPE.IMT_CHOICE_UNIT:
				NKCUtil.SetLabelText(this.m_lbTitle, NKCUtilString.GET_STRING_CHOICE_UNIT_CONFIRM);
				this.m_unit.SetData(this.m_targetID);
				break;
			case NKM_ITEM_MISC_TYPE.IMT_CHOICE_SHIP:
				NKCUtil.SetLabelText(this.m_lbTitle, NKCUtilString.GET_STRING_CHOICE_SHIP_CONFIRM);
				this.m_ship.SetData(this.m_targetID);
				break;
			case NKM_ITEM_MISC_TYPE.IMT_CHOICE_EQUIP:
				NKCUtil.SetLabelText(this.m_lbTitle, NKCUtilString.GET_STRING_CHOICE_EQUIP_CONFIRM);
				this.m_equip.SetData(this.m_targetID, setItemID);
				break;
			case NKM_ITEM_MISC_TYPE.IMT_CHOICE_MISC:
				NKCUtil.SetLabelText(this.m_lbTitle, NKCUtilString.GET_STRING_CHOICE_MISC_CONFIRM);
				this.m_misc.SetData(NKCPopupMiscUseCount.USE_ITEM_TYPE.Common, itemMiscTemplet.m_ItemMiscID, NKCUISlot.SlotData.MakeMiscItemData(this.m_targetID, this.m_targetCount, 0));
				break;
			case NKM_ITEM_MISC_TYPE.IMT_CHOICE_MOLD:
				return;
			case NKM_ITEM_MISC_TYPE.IMT_CHOICE_OPERATOR:
				NKCUtil.SetLabelText(this.m_lbTitle, NKCUtilString.GET_STRING_CHOICE_UNIT_CONFIRM);
				this.m_operator.SetData(this.m_targetID);
				break;
			default:
				if (itemMiscType != NKM_ITEM_MISC_TYPE.IMT_CHOICE_SKIN)
				{
					return;
				}
				NKCUtil.SetLabelText(this.m_lbTitle, NKCUtilString.GET_STRING_CHOICE_SKIN_CONFIRM);
				this.m_skin.SetData(this.m_targetID, itemMiscTemplet.m_ItemMiscID);
				break;
			}
			base.UIOpened(true);
		}

		// Token: 0x06007701 RID: 30465 RVA: 0x002796B4 File Offset: 0x002778B4
		public void OnClickOk()
		{
			base.Close();
			NKCPopupResourceConfirmBox.Instance.OpenForSelection(this.m_NKMItemMiscTemplet, this.m_targetID, this.m_targetCount * (long)this.UseCount, new NKCPopupResourceConfirmBox.OnButton(this.OnFinalConfirm), null, false, this.m_setOptionID);
		}

		// Token: 0x06007702 RID: 30466 RVA: 0x002796F4 File Offset: 0x002778F4
		private void OnFinalConfirm()
		{
			NKCShopManager.ShopRewardSubstituteData item;
			if (NKCShopManager.MakeSubstituteItem(NKCRandomBoxManager.GetRandomBoxItemTempletList(this.m_NKMItemMiscTemplet.m_RewardGroupID).Find((NKMRandomBoxItemTemplet x) => x.m_RewardID == this.m_targetID), this.UseCount, out item))
			{
				List<NKCShopManager.ShopRewardSubstituteData> list = new List<NKCShopManager.ShopRewardSubstituteData>();
				list.Add(item);
				NKCPopupShopCustomPackageSubstitude.Instance.Open(list, new NKCPopupShopCustomPackageSubstitude.OnClose(this.SendPacket));
				return;
			}
			this.SendPacket();
		}

		// Token: 0x06007703 RID: 30467 RVA: 0x0027975C File Offset: 0x0027795C
		private void SendPacket()
		{
			NKCPacketSender.Send_NKMPacket_CHOICE_ITEM_USE_REQ(this.m_NKMItemMiscTemplet.m_ItemMiscID, this.m_targetID, this.UseCount, this.m_setOptionID);
		}

		// Token: 0x04006374 RID: 25460
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_selection";

		// Token: 0x04006375 RID: 25461
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_SELECTION_CONFIRM";

		// Token: 0x04006376 RID: 25462
		private static NKCPopupSelectionConfirm m_Instance;

		// Token: 0x04006377 RID: 25463
		public Text m_lbTitle;

		// Token: 0x04006378 RID: 25464
		public NKCPopupSelectionConfirmUnit m_unit;

		// Token: 0x04006379 RID: 25465
		public NKCPopupSelectionConfirmShip m_ship;

		// Token: 0x0400637A RID: 25466
		public NKCPopupSelectionConfirmEquip m_equip;

		// Token: 0x0400637B RID: 25467
		public NKCPopupMiscUseCountContents m_misc;

		// Token: 0x0400637C RID: 25468
		public NKCPopupSelectionConfirmOperator m_operator;

		// Token: 0x0400637D RID: 25469
		public NKCPopupSelectionConfirmSkin m_skin;

		// Token: 0x0400637E RID: 25470
		public NKCUIComStateButton m_btnOk;

		// Token: 0x0400637F RID: 25471
		public NKCUIComStateButton m_btnCancel;

		// Token: 0x04006380 RID: 25472
		private NKMItemMiscTemplet m_NKMItemMiscTemplet;

		// Token: 0x04006381 RID: 25473
		private int m_targetID;

		// Token: 0x04006382 RID: 25474
		private long m_targetCount;

		// Token: 0x04006383 RID: 25475
		private int m_setOptionID;
	}
}
