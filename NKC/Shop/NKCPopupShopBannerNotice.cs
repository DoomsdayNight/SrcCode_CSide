using System;
using System.Collections.Generic;
using NKC.Publisher;
using NKC.UI.NPC;
using NKM.Shop;
using UnityEngine.Events;

namespace NKC.UI.Shop
{
	// Token: 0x02000AC8 RID: 2760
	public class NKCPopupShopBannerNotice : NKCUIBase
	{
		// Token: 0x1700148D RID: 5261
		// (get) Token: 0x06007B7A RID: 31610 RVA: 0x00292CC8 File Offset: 0x00290EC8
		public static NKCPopupShopBannerNotice Instance
		{
			get
			{
				if (NKCPopupShopBannerNotice.m_Instance == null)
				{
					NKCPopupShopBannerNotice.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupShopBannerNotice>("AB_UI_NKM_UI_SHOP", "NKM_UI_POPUP_SHOP_BANNER_NOTICE", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupShopBannerNotice.CleanupInstance)).GetInstance<NKCPopupShopBannerNotice>();
					NKCPopupShopBannerNotice.m_Instance.Init();
				}
				return NKCPopupShopBannerNotice.m_Instance;
			}
		}

		// Token: 0x06007B7B RID: 31611 RVA: 0x00292D17 File Offset: 0x00290F17
		private static void CleanupInstance()
		{
			NKCPopupShopBannerNotice.m_Instance = null;
		}

		// Token: 0x1700148E RID: 5262
		// (get) Token: 0x06007B7C RID: 31612 RVA: 0x00292D1F File Offset: 0x00290F1F
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x1700148F RID: 5263
		// (get) Token: 0x06007B7D RID: 31613 RVA: 0x00292D22 File Offset: 0x00290F22
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001490 RID: 5264
		// (get) Token: 0x06007B7E RID: 31614 RVA: 0x00292D25 File Offset: 0x00290F25
		public override string MenuName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06007B7F RID: 31615 RVA: 0x00292D2C File Offset: 0x00290F2C
		private void Init()
		{
			if (this.m_NPCShop != null)
			{
				this.m_NPCShop.Init(true);
			}
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_btnBuy.PointerClick.RemoveAllListeners();
			this.m_btnBuy.PointerClick.AddListener(new UnityAction(this.OnProductBuy));
			this.m_PrefabSlot.Init(new NKCUIShopSlotBase.OnBuy(this.OnBtnProductBuy), null);
		}

		// Token: 0x06007B80 RID: 31616 RVA: 0x00292DC4 File Offset: 0x00290FC4
		public static void Open(int productID, Action onClose)
		{
			NKCPublisherModule.OnComplete <>9__1;
			NKCShopManager.FetchShopItemList(NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, delegate(bool bSuccess)
			{
				if (!bSuccess)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_SHOP_WAS_NOT_ABLE_TO_GET_PRODUCT_LIST_FROM_SERVER, null, "");
					return;
				}
				if (!NKCPublisherModule.InAppPurchase.CheckReceivedBillingProductList)
				{
					NKCPublisherModule.NKCPMInAppPurchase inAppPurchase = NKCPublisherModule.InAppPurchase;
					NKCPublisherModule.OnComplete dOnComplete;
					if ((dOnComplete = <>9__1) == null)
					{
						dOnComplete = (<>9__1 = delegate(NKC_PUBLISHER_RESULT_CODE resultCode, string add)
						{
							if (resultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_OK)
							{
								NKCPopupShopBannerNotice.Instance._Open(productID, onClose);
							}
						});
					}
					inAppPurchase.RequestBillingProductList(dOnComplete);
					return;
				}
				NKCPopupShopBannerNotice.Instance._Open(productID, onClose);
			}, true);
		}

		// Token: 0x06007B81 RID: 31617 RVA: 0x00292DF8 File Offset: 0x00290FF8
		private void _Open(int productID, Action onClose)
		{
			ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(productID);
			if (shopItemTemplet != null)
			{
				this._Open(shopItemTemplet, onClose);
			}
		}

		// Token: 0x06007B82 RID: 31618 RVA: 0x00292E17 File Offset: 0x00291017
		private void _Open(ShopItemTemplet shopTemplet, Action onClose)
		{
			this.CleanUp();
			this.m_NKMShopItemTemplet = shopTemplet;
			this.dOnClose = onClose;
			this.m_PrefabSlot.SetData(null, shopTemplet, -1, false);
			this.SetButton();
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			base.UIOpened(true);
		}

		// Token: 0x06007B83 RID: 31619 RVA: 0x00292E56 File Offset: 0x00291056
		private void OnBtnProductBuy(int ProductID)
		{
			NKCShopManager.OnBtnProductBuy(ProductID, false);
		}

		// Token: 0x06007B84 RID: 31620 RVA: 0x00292E60 File Offset: 0x00291060
		private void CleanUp()
		{
			if (this.m_prefabInstance != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_prefabInstance);
				this.m_prefabInstance = null;
			}
		}

		// Token: 0x06007B85 RID: 31621 RVA: 0x00292E7C File Offset: 0x0029107C
		private void SetButton()
		{
			this.m_PriceTag.SetData(this.m_NKMShopItemTemplet, false, false);
		}

		// Token: 0x06007B86 RID: 31622 RVA: 0x00292E92 File Offset: 0x00291092
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.CleanUp();
			Action action = this.dOnClose;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06007B87 RID: 31623 RVA: 0x00292EB6 File Offset: 0x002910B6
		public void OnProductBuy()
		{
			NKCPopupShopBuyConfirm.Instance.Open(this.m_NKMShopItemTemplet, new NKCUIShop.OnProductBuyDelegate(this.OnBuyConrifim));
		}

		// Token: 0x06007B88 RID: 31624 RVA: 0x00292ED4 File Offset: 0x002910D4
		private void OnBuyConrifim(int id, int count, List<int> lstSelection)
		{
			NKCShopManager.TryProductBuy(id, count, null);
			base.Close();
		}

		// Token: 0x04006827 RID: 26663
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_SHOP";

		// Token: 0x04006828 RID: 26664
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_SHOP_BANNER_NOTICE";

		// Token: 0x04006829 RID: 26665
		private static NKCPopupShopBannerNotice m_Instance;

		// Token: 0x0400682A RID: 26666
		public NKCUIComStateButton m_btnClose;

		// Token: 0x0400682B RID: 26667
		public NKCUINPCShop m_NPCShop;

		// Token: 0x0400682C RID: 26668
		public NKCUIShopSlotPrefab m_PrefabSlot;

		// Token: 0x0400682D RID: 26669
		public NKCUIComStateButton m_btnBuy;

		// Token: 0x0400682E RID: 26670
		public NKCUIPriceTag m_PriceTag;

		// Token: 0x0400682F RID: 26671
		private ShopItemTemplet m_NKMShopItemTemplet;

		// Token: 0x04006830 RID: 26672
		private NKCAssetInstanceData m_prefabInstance;

		// Token: 0x04006831 RID: 26673
		private Action dOnClose;
	}
}
