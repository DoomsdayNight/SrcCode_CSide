using System;
using NKC.UI.Shop;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A69 RID: 2665
	public class NKCPopupItemLack : NKCUIBase
	{
		// Token: 0x17001390 RID: 5008
		// (get) Token: 0x0600757D RID: 30077 RVA: 0x00271968 File Offset: 0x0026FB68
		public static NKCPopupItemLack Instance
		{
			get
			{
				if (NKCPopupItemLack.m_Instance == null)
				{
					NKCPopupItemLack.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupItemLack>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_BOX_ITEM_LACK", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupItemLack.CleanupInstance)).GetInstance<NKCPopupItemLack>();
					NKCPopupItemLack.m_Instance.InitUI();
				}
				return NKCPopupItemLack.m_Instance;
			}
		}

		// Token: 0x0600757E RID: 30078 RVA: 0x002719B7 File Offset: 0x0026FBB7
		private static void CleanupInstance()
		{
			NKCPopupItemLack.m_Instance = null;
		}

		// Token: 0x17001391 RID: 5009
		// (get) Token: 0x0600757F RID: 30079 RVA: 0x002719BF File Offset: 0x0026FBBF
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001392 RID: 5010
		// (get) Token: 0x06007580 RID: 30080 RVA: 0x002719C2 File Offset: 0x0026FBC2
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_POPUP_ITEM_LACK;
			}
		}

		// Token: 0x06007581 RID: 30081 RVA: 0x002719C9 File Offset: 0x0026FBC9
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06007582 RID: 30082 RVA: 0x002719D8 File Offset: 0x0026FBD8
		private void InitUI()
		{
			this.m_btnOK.PointerClick.RemoveAllListeners();
			this.m_btnOK.PointerClick.AddListener(new UnityAction(this.OnClickOK));
			NKCUtil.SetHotkey(this.m_btnOK, HotkeyEventType.Confirm, null, false);
			this.m_btnBuy.PointerClick.RemoveAllListeners();
			this.m_btnBuy.PointerClick.AddListener(new UnityAction(this.OnClickBuy));
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData eventData)
			{
				base.Close();
			});
			this.m_eventTriggerBG.triggers.Add(entry);
			this.m_openAni = new NKCUIOpenAnimator(base.gameObject);
			NKCUIComItemDropInfo itemDropInfo = this.m_itemDropInfo;
			if (itemDropInfo != null)
			{
				itemDropInfo.Init();
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnAdOn, new UnityAction(this.OnClickAd));
			NKCUtil.SetButtonClickDelegate(this.m_btnCancel_OkCancel, new UnityAction(this.OnClickCancel));
		}

		// Token: 0x06007583 RID: 30083 RVA: 0x00271AD0 File Offset: 0x0026FCD0
		public void OpenItemMiscLackPopup(int needItemID, int needItemCount)
		{
			NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeMiscItemData(needItemID, (long)needItemCount, 0);
			this.OpenItemMiscLackPopup(slotData, needItemCount, NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(slotData.ID));
		}

		// Token: 0x06007584 RID: 30084 RVA: 0x00271B04 File Offset: 0x0026FD04
		public void OpenItemMiscLackPopup(int needItemID, int needItemCount, long curItemCount)
		{
			NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeMiscItemData(needItemID, (long)needItemCount, 0);
			this.OpenItemMiscLackPopup(slotData, needItemCount, curItemCount);
		}

		// Token: 0x06007585 RID: 30085 RVA: 0x00271B24 File Offset: 0x0026FD24
		public void OpenItemMiscLackPopup(NKCUISlot.SlotData slotData, int needCount, long curItemCount)
		{
			if (this.m_openAni != null)
			{
				this.m_openAni.PlayOpenAni();
			}
			NKCUtil.SetGameobjectActive(this.m_objOkRoot, true);
			NKCUtil.SetGameobjectActive(this.m_objOkCancelRoot, false);
			NKCUtil.SetGameobjectActive(this.m_objLayout, true);
			NKCUtil.SetGameobjectActive(this.m_objAdImage, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnAdOn, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnAdOff, false);
			this.m_slot.SetData(slotData.ID, needCount, curItemCount, true, true, false);
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(slotData.ID);
			this.m_lbTitle.text = NKCUtilString.GET_STRING_NOTICE;
			if (itemMiscTempletByID != null)
			{
				this.m_lbDesc.text = string.Format(NKCUtilString.GET_STRING_ITEM_LACK_DESC_ONE_PARAM, itemMiscTempletByID.GetItemName());
			}
			this.shopTab = NKCShopManager.GetShopMoveTab(slotData.ID);
			NKCUtil.SetGameobjectActive(this.m_btnBuy, this.shopTab.Type != "TAB_NONE");
			NKCUIComItemDropInfo itemDropInfo = this.m_itemDropInfo;
			bool valueOrDefault = ((itemDropInfo != null) ? new bool?(itemDropInfo.SetData(slotData, true)) : null).GetValueOrDefault();
			NKCUtil.SetGameobjectActive(this.m_objDummy, valueOrDefault);
			base.UIOpened(true);
		}

		// Token: 0x06007586 RID: 30086 RVA: 0x00271C4C File Offset: 0x0026FE4C
		public void OpenItemLackAdRewardPopup(int itemId, NKCPopupItemLack.OnCancel onCancel)
		{
			if (this.m_openAni != null)
			{
				this.m_openAni.PlayOpenAni();
			}
			this.m_slot.SetData(itemId, 0, 0L, true, true, false);
			NKCUtil.SetGameobjectActive(this.m_objOkRoot, false);
			NKCUtil.SetGameobjectActive(this.m_objOkCancelRoot, true);
			NKCUtil.SetGameobjectActive(this.m_objLayout, false);
			NKCUtil.SetGameobjectActive(this.m_objAdImage, true);
			NKCUtil.SetGameobjectActive(this.m_btnBuy, false);
			NKCAdManager.SetItemRewardAdButtonState(NKCPopupItemBox.eMode.MoveToShop, itemId, this.m_csbtnAdOn, this.m_csbtnAdOff, this.m_lbAdRemainCount);
			this.m_dOnCancel = onCancel;
			base.UIOpened(true);
		}

		// Token: 0x06007587 RID: 30087 RVA: 0x00271CE1 File Offset: 0x0026FEE1
		public void OnClickOK()
		{
			base.Close();
		}

		// Token: 0x06007588 RID: 30088 RVA: 0x00271CEC File Offset: 0x0026FEEC
		public void OnClickBuy()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.LOBBY_SUBMENU, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.LOBBY_SUBMENU, 0);
				return;
			}
			base.Close();
			if (this.shopTab.Type == "TAB_NONE")
			{
				return;
			}
			NKCUIShop.ShopShortcut(this.shopTab.Type, this.shopTab.SubIndex, 0);
		}

		// Token: 0x06007589 RID: 30089 RVA: 0x00271D48 File Offset: 0x0026FF48
		private void Update()
		{
			if (base.IsOpen)
			{
				if (this.m_openAni != null)
				{
					this.m_openAni.Update();
				}
				if (this.m_slot != null)
				{
					NKCAdManager.UpdateItemRewardAdCoolTime(this.m_slot.ItemID, this.m_csbtnAdOn, this.m_csbtnAdOff, this.m_lbAdCoolTime, this.m_lbAdRemainCount);
				}
			}
		}

		// Token: 0x0600758A RID: 30090 RVA: 0x00271DA6 File Offset: 0x0026FFA6
		private void OnClickAd()
		{
			base.Close();
			if (this.m_slot != null)
			{
				NKCAdManager.WatchItemRewardAd(this.m_slot.ItemID);
			}
		}

		// Token: 0x0600758B RID: 30091 RVA: 0x00271DCC File Offset: 0x0026FFCC
		private void OnClickCancel()
		{
			base.Close();
			if (this.m_dOnCancel != null)
			{
				this.m_dOnCancel();
			}
		}

		// Token: 0x040061DA RID: 25050
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_ok_cancel_box";

		// Token: 0x040061DB RID: 25051
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_BOX_ITEM_LACK";

		// Token: 0x040061DC RID: 25052
		private static NKCPopupItemLack m_Instance;

		// Token: 0x040061DD RID: 25053
		public Text m_lbTitle;

		// Token: 0x040061DE RID: 25054
		public Text m_lbDesc;

		// Token: 0x040061DF RID: 25055
		public NKCUIItemCostSlot m_slot;

		// Token: 0x040061E0 RID: 25056
		public NKCUIComStateButton m_btnOK;

		// Token: 0x040061E1 RID: 25057
		public NKCUIComStateButton m_btnBuy;

		// Token: 0x040061E2 RID: 25058
		public NKCUIComStateButton m_btnCancel_OkCancel;

		// Token: 0x040061E3 RID: 25059
		public EventTrigger m_eventTriggerBG;

		// Token: 0x040061E4 RID: 25060
		public GameObject m_objOkRoot;

		// Token: 0x040061E5 RID: 25061
		public GameObject m_objOkCancelRoot;

		// Token: 0x040061E6 RID: 25062
		public GameObject m_objLayout;

		// Token: 0x040061E7 RID: 25063
		[Header("아이템 획득처")]
		public GameObject m_objDummy;

		// Token: 0x040061E8 RID: 25064
		public NKCUIComItemDropInfo m_itemDropInfo;

		// Token: 0x040061E9 RID: 25065
		[Header("광고")]
		public GameObject m_objAdImage;

		// Token: 0x040061EA RID: 25066
		public NKCUIComStateButton m_csbtnAdOn;

		// Token: 0x040061EB RID: 25067
		public NKCUIComStateButton m_csbtnAdOff;

		// Token: 0x040061EC RID: 25068
		public Text m_lbAdRemainCount;

		// Token: 0x040061ED RID: 25069
		public Text m_lbAdCoolTime;

		// Token: 0x040061EE RID: 25070
		private NKCUIOpenAnimator m_openAni;

		// Token: 0x040061EF RID: 25071
		private TabId shopTab;

		// Token: 0x040061F0 RID: 25072
		private NKCPopupItemLack.OnCancel m_dOnCancel;

		// Token: 0x020017CB RID: 6091
		// (Invoke) Token: 0x0600B438 RID: 46136
		public delegate void OnCancel();
	}
}
