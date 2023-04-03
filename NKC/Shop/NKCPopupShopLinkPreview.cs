using System;
using System.Collections.Generic;
using NKM;
using NKM.Shop;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Shop
{
	// Token: 0x02000ACE RID: 2766
	public class NKCPopupShopLinkPreview : NKCUIBase
	{
		// Token: 0x170014A7 RID: 5287
		// (get) Token: 0x06007BEE RID: 31726 RVA: 0x00295BD8 File Offset: 0x00293DD8
		public static NKCPopupShopLinkPreview Instance
		{
			get
			{
				if (NKCPopupShopLinkPreview.m_Instance == null)
				{
					NKCPopupShopLinkPreview.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupShopLinkPreview>("ab_ui_nkm_ui_shop", "NKM_UI_POPUP_SHOP_BUY_PACKAGE_PREVIEW", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupShopLinkPreview.CleanupInstance)).GetInstance<NKCPopupShopLinkPreview>();
					NKCPopupShopLinkPreview.m_Instance.InitUI();
				}
				return NKCPopupShopLinkPreview.m_Instance;
			}
		}

		// Token: 0x06007BEF RID: 31727 RVA: 0x00295C27 File Offset: 0x00293E27
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupShopLinkPreview.m_Instance != null && NKCPopupShopLinkPreview.m_Instance.IsOpen)
			{
				NKCPopupShopLinkPreview.m_Instance.Close();
			}
		}

		// Token: 0x06007BF0 RID: 31728 RVA: 0x00295C4C File Offset: 0x00293E4C
		private static void CleanupInstance()
		{
			NKCPopupShopLinkPreview.m_Instance = null;
		}

		// Token: 0x170014A8 RID: 5288
		// (get) Token: 0x06007BF1 RID: 31729 RVA: 0x00295C54 File Offset: 0x00293E54
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170014A9 RID: 5289
		// (get) Token: 0x06007BF2 RID: 31730 RVA: 0x00295C57 File Offset: 0x00293E57
		public override string MenuName
		{
			get
			{
				return "LinkPreview";
			}
		}

		// Token: 0x06007BF3 RID: 31731 RVA: 0x00295C5E File Offset: 0x00293E5E
		public override void CloseInternal()
		{
			if (this.m_UIPackagePreview != null)
			{
				this.m_UIPackagePreview.Close();
				this.m_UIPackagePreview = null;
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007BF4 RID: 31732 RVA: 0x00295C8C File Offset: 0x00293E8C
		private void InitUI()
		{
			this.m_loopScroll.dOnGetObject += this.GetObject;
			this.m_loopScroll.dOnReturnObject += this.ReturnObject;
			this.m_loopScroll.dOnProvideData += this.ProvideData;
			this.m_loopScroll.PrepareCells(0);
			NKCUtil.SetScrollHotKey(this.m_loopScroll, null);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnOK, new UnityAction(base.Close));
			NKCUtil.SetHotkey(this.m_csbtnOK, HotkeyEventType.Confirm, null, false);
		}

		// Token: 0x06007BF5 RID: 31733 RVA: 0x00295D1C File Offset: 0x00293F1C
		public void Open(int ProductID)
		{
			this.m_lstLinkedItems = NKCShopManager.GetLinkedItem(ProductID);
			if (this.m_lstLinkedItems == null || this.m_lstLinkedItems.Count == 0)
			{
				return;
			}
			base.gameObject.SetActive(true);
			this.m_loopScroll.TotalCount = this.m_lstLinkedItems.Count;
			this.m_loopScroll.SetIndexPosition(0);
			base.UIOpened(true);
		}

		// Token: 0x06007BF6 RID: 31734 RVA: 0x00295D80 File Offset: 0x00293F80
		private RectTransform GetObject(int index)
		{
			if (this.m_stkObj.Count > 0)
			{
				RectTransform rectTransform = this.m_stkObj.Pop();
				NKCUtil.SetGameobjectActive(rectTransform, true);
				return rectTransform;
			}
			if (this.m_pfbPreviewSlot == null)
			{
				Debug.LogError("Scout slot prefab null!");
				return null;
			}
			NKCUIShopSlotPreview nkcuishopSlotPreview = UnityEngine.Object.Instantiate<NKCUIShopSlotPreview>(this.m_pfbPreviewSlot);
			nkcuishopSlotPreview.Init(new NKCUIShopSlotBase.OnBuy(this.OnSelectSlot), null);
			return nkcuishopSlotPreview.GetComponent<RectTransform>();
		}

		// Token: 0x06007BF7 RID: 31735 RVA: 0x00295DEB File Offset: 0x00293FEB
		private void ReturnObject(Transform go)
		{
			NKCUtil.SetGameobjectActive(go, false);
			go.SetParent(this.m_rtSlotPool);
			this.m_stkObj.Push(go.GetComponent<RectTransform>());
		}

		// Token: 0x06007BF8 RID: 31736 RVA: 0x00295E14 File Offset: 0x00294014
		private void ProvideData(Transform tr, int idx)
		{
			if (idx < 0)
			{
				NKCUtil.SetGameobjectActive(tr, false);
				return;
			}
			NKCUIShopSlotPreview component = tr.GetComponent<NKCUIShopSlotPreview>();
			if (component == null)
			{
				return;
			}
			ShopItemTemplet shopItemTemplet = this.m_lstLinkedItems[idx];
			component.SetData(null, shopItemTemplet, NKCShopManager.GetBuyCountLeft(shopItemTemplet.m_ProductID), false);
		}

		// Token: 0x06007BF9 RID: 31737 RVA: 0x00295E60 File Offset: 0x00294060
		private void OnSelectSlot(int productID)
		{
			ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(productID);
			if (shopItemTemplet == null)
			{
				return;
			}
			if (shopItemTemplet.m_ItemType != NKM_REWARD_TYPE.RT_MISC)
			{
				return;
			}
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(shopItemTemplet.m_ItemID);
			if (itemMiscTempletByID == null || !itemMiscTempletByID.IsPackageItem)
			{
				return;
			}
			if (this.m_UIPackagePreview == null)
			{
				this.m_UIPackagePreview = NKCPopupShopPackageConfirm.OpenNewInstance();
			}
			this.m_UIPackagePreview.OpenPreview(shopItemTemplet);
		}

		// Token: 0x040068A6 RID: 26790
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_shop";

		// Token: 0x040068A7 RID: 26791
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_SHOP_BUY_PACKAGE_PREVIEW";

		// Token: 0x040068A8 RID: 26792
		private static NKCPopupShopLinkPreview m_Instance;

		// Token: 0x040068A9 RID: 26793
		public RectTransform m_rtSlotPool;

		// Token: 0x040068AA RID: 26794
		public NKCUIShopSlotPreview m_pfbPreviewSlot;

		// Token: 0x040068AB RID: 26795
		public LoopScrollRect m_loopScroll;

		// Token: 0x040068AC RID: 26796
		public NKCUIComStateButton m_csbtnOK;

		// Token: 0x040068AD RID: 26797
		private List<ShopItemTemplet> m_lstLinkedItems;

		// Token: 0x040068AE RID: 26798
		private NKCPopupShopPackageConfirm m_UIPackagePreview;

		// Token: 0x040068AF RID: 26799
		private Stack<RectTransform> m_stkObj = new Stack<RectTransform>();
	}
}
