using System;
using NKC.Publisher;
using NKM.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Shop
{
	// Token: 0x02000AD6 RID: 2774
	public class NKCUIComShopProductDataInjector : MonoBehaviour, IShopDataInjector
	{
		// Token: 0x06007C3D RID: 31805 RVA: 0x00297FA2 File Offset: 0x002961A2
		public void TriggerInjectData(ShopItemTemplet productTemplet)
		{
			if (productTemplet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (!this.m_bSetDataComplete)
			{
				this.SetData(productTemplet);
			}
		}

		// Token: 0x06007C3E RID: 31806 RVA: 0x00297FD0 File Offset: 0x002961D0
		public void SetData(ShopItemTemplet productTemplet)
		{
			if (productTemplet == null)
			{
				Debug.LogError("product not found!");
				base.gameObject.SetActive(false);
				return;
			}
			if (this.m_shopSlot != null)
			{
				this.m_shopSlot.Init(new NKCUIShopSlotBase.OnBuy(this.OnBtnProductBuy), null);
				this.m_shopSlot.SetData(null, productTemplet, -1, false);
			}
			NKCUtil.SetLabelText(this.m_lbName, productTemplet.GetItemName());
			NKCUtil.SetLabelText(this.m_lbDesc, productTemplet.GetItemDesc());
			int realPrice = NKCScenManager.CurrentUserData().m_ShopData.GetRealPrice(productTemplet, 1, false);
			bool flag = realPrice < productTemplet.m_Price;
			if (productTemplet.m_PriceItemID == 0)
			{
				if (flag)
				{
					this.SetInappPurchasePrice(productTemplet, realPrice, true, productTemplet.m_Price);
					return;
				}
				this.SetInappPurchasePrice(productTemplet, productTemplet.m_Price, false, 0);
				return;
			}
			else
			{
				if (flag)
				{
					this.SetPrice(productTemplet.m_PriceItemID, realPrice, true, productTemplet.m_Price);
					return;
				}
				this.SetPrice(productTemplet.m_PriceItemID, realPrice, false, 0);
				return;
			}
		}

		// Token: 0x06007C3F RID: 31807 RVA: 0x002980C0 File Offset: 0x002962C0
		private void SetInappPurchasePrice(ShopItemTemplet cShopItemTemplet, int price, bool bSale = false, int oldPrice = 0)
		{
			NKCUtil.SetGameobjectActive(this.m_imgPriceItem, false);
			if (price <= 0)
			{
				NKCUtil.SetGameobjectActive(this.m_lbBeforeSalePrice, false);
				NKCUtil.SetLabelText(this.m_lbRealPrice, NKCUtilString.GET_STRING_SHOP_FREE);
				return;
			}
			if (bSale)
			{
				NKCUtil.SetGameobjectActive(this.m_lbBeforeSalePrice, true);
				NKCUtil.SetLabelText(this.m_lbRealPrice, NKCPublisherModule.InAppPurchase.GetLocalPriceString(cShopItemTemplet.m_MarketID, cShopItemTemplet.m_ProductID));
				NKCUtil.SetLabelText(this.m_lbBeforeSalePrice, NKCUtilString.GetInAppPurchasePriceString(oldPrice, cShopItemTemplet.m_ProductID));
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_lbBeforeSalePrice, false);
			NKCUtil.SetLabelText(this.m_lbRealPrice, NKCPublisherModule.InAppPurchase.GetLocalPriceString(cShopItemTemplet.m_MarketID, cShopItemTemplet.m_ProductID));
		}

		// Token: 0x06007C40 RID: 31808 RVA: 0x00298178 File Offset: 0x00296378
		private void SetPrice(int priceItemID, int Price, bool bSale = false, int oldPrice = 0)
		{
			if (this.m_imgPriceItem != null)
			{
				Sprite orLoadMiscItemSmallIcon = NKCResourceUtility.GetOrLoadMiscItemSmallIcon(priceItemID);
				NKCUtil.SetImageSprite(this.m_imgPriceItem, orLoadMiscItemSmallIcon, true);
			}
			if (Price <= 0)
			{
				NKCUtil.SetGameobjectActive(this.m_lbBeforeSalePrice, false);
				NKCUtil.SetLabelText(this.m_lbRealPrice, NKCUtilString.GET_STRING_SHOP_FREE);
				return;
			}
			if (bSale)
			{
				NKCUtil.SetGameobjectActive(this.m_lbBeforeSalePrice, true);
				NKCUtil.SetLabelText(this.m_lbRealPrice, Price.ToString());
				NKCUtil.SetLabelText(this.m_lbBeforeSalePrice, oldPrice.ToString());
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_lbBeforeSalePrice, false);
			NKCUtil.SetLabelText(this.m_lbRealPrice, Price.ToString());
		}

		// Token: 0x06007C41 RID: 31809 RVA: 0x0029821A File Offset: 0x0029641A
		private void OnBtnProductBuy(int ProductID)
		{
			NKCShopManager.OnBtnProductBuy(ProductID, false);
		}

		// Token: 0x0400690A RID: 26890
		[Header("상품 데이터를 집어넣어 주는 컴포넌트")]
		public Text m_lbName;

		// Token: 0x0400690B RID: 26891
		public Text m_lbDesc;

		// Token: 0x0400690C RID: 26892
		public Image m_imgPriceItem;

		// Token: 0x0400690D RID: 26893
		[Tooltip("세일 전 가격. 세일 안하면 안 보임")]
		public Text m_lbBeforeSalePrice;

		// Token: 0x0400690E RID: 26894
		[Tooltip("실제 판매가격")]
		public Text m_lbRealPrice;

		// Token: 0x0400690F RID: 26895
		public NKCUIShopSlotBase m_shopSlot;

		// Token: 0x04006910 RID: 26896
		private bool m_bSetDataComplete;
	}
}
