using System;
using NKC.Publisher;
using NKM;
using NKM.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Shop
{
	// Token: 0x02000AE9 RID: 2793
	public class NKCUIShopSlotSmall : NKCUIShopSlotBase
	{
		// Token: 0x06007D5A RID: 32090 RVA: 0x002A0480 File Offset: 0x0029E680
		public override void Init(NKCUIShopSlotBase.OnBuy onBuy, NKCUIShopSlotBase.OnRefreshRequired onRefreshRequired)
		{
			base.Init(onBuy, onRefreshRequired);
			this.m_Slot.Init();
		}

		// Token: 0x06007D5B RID: 32091 RVA: 0x002A0498 File Offset: 0x0029E698
		protected override void SetGoodsImage(ShopItemTemplet shopTemplet, bool bFirstBuy)
		{
			if (!string.IsNullOrEmpty(this.m_OverrideImageAsset) && this.m_Image != null)
			{
				NKCUtil.SetGameobjectActive(this.m_Slot, false);
				NKCUtil.SetGameobjectActive(this.m_Image, true);
				Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(NKMAssetName.ParseBundleName("AB_UI_NKM_UI_SHOP_IMG", this.m_OverrideImageAsset));
				if (orLoadAssetResource == null)
				{
					Debug.LogError(string.Format("Shop Sprite {0}(from productID {1}) null", this.m_OverrideImageAsset, shopTemplet.m_ProductID));
				}
				this.m_Image.sprite = orLoadAssetResource;
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_Slot, true);
			NKCUtil.SetGameobjectActive(this.m_Image, false);
			NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeShopItemData(shopTemplet, bFirstBuy);
			this.m_Slot.SetData(slotData, false, true, false, new NKCUISlot.OnClick(this.OnSlotClick));
			NKCShopManager.ShowShopItemCashCount(this.m_Slot, slotData, shopTemplet.m_FreeValue, shopTemplet.m_PaidValue);
		}

		// Token: 0x06007D5C RID: 32092 RVA: 0x002A0578 File Offset: 0x0029E778
		protected override void SetGoodsImage(NKMShopRandomListData shopRandomTemplet)
		{
			NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeShopItemData(shopRandomTemplet);
			this.m_Slot.SetData(data, false, true, false, new NKCUISlot.OnClick(this.OnSlotClick));
		}

		// Token: 0x06007D5D RID: 32093 RVA: 0x002A05A7 File Offset: 0x0029E7A7
		private void OnSlotClick(NKCUISlot.SlotData slotData, bool bLocked)
		{
			base.OnBtnBuy();
		}

		// Token: 0x06007D5E RID: 32094 RVA: 0x002A05B0 File Offset: 0x0029E7B0
		protected override void SetPrice(int priceItemID, int Price, bool bSale = false, int oldPrice = 0)
		{
			NKCUtil.SetGameobjectActive(this.m_objNormalPriceRoot, !bSale);
			NKCUtil.SetGameobjectActive(this.m_objSalePriceRoot, bSale);
			NKCUtil.SetGameobjectActive(this.m_objPriceParent, Price > 0);
			NKCUtil.SetGameobjectActive(this.m_lbFreePrice, Price <= 0);
			if (Price <= 0)
			{
				NKCUtil.SetLabelText(this.m_lbFreePrice, NKCUtilString.GET_STRING_SHOP_FREE);
				return;
			}
			if (bSale)
			{
				NKCUtil.SetGameobjectActive(this.m_imgSalePrice, true);
				Sprite priceImage = base.GetPriceImage(priceItemID);
				this.m_imgSalePrice.sprite = priceImage;
				this.m_lbSaleOldPrice.text = oldPrice.ToString();
				this.m_lbSalePrice.text = Price.ToString();
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_imgPrice, true);
			Sprite priceImage2 = base.GetPriceImage(priceItemID);
			this.m_imgPrice.sprite = priceImage2;
			this.m_lbNormalPrice.text = Price.ToString();
		}

		// Token: 0x06007D5F RID: 32095 RVA: 0x002A0688 File Offset: 0x0029E888
		protected override void SetInappPurchasePrice(ShopItemTemplet cShopItemTemplet, int price, bool bSale = false, int oldPrice = 0)
		{
			if (cShopItemTemplet == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objNormalPriceRoot, !bSale);
			NKCUtil.SetGameobjectActive(this.m_objSalePriceRoot, bSale);
			NKCUtil.SetGameobjectActive(this.m_objPriceParent, price > 0);
			NKCUtil.SetGameobjectActive(this.m_lbFreePrice, price <= 0);
			if (bSale)
			{
				NKCUtil.SetGameobjectActive(this.m_imgPrice, false);
				NKCUtil.SetGameobjectActive(this.m_imgSalePrice, false);
				this.m_lbSaleOldPrice.text = NKCUtilString.GetInAppPurchasePriceString(oldPrice, cShopItemTemplet.m_ProductID);
				this.m_lbNormalPrice.text = NKCPublisherModule.InAppPurchase.GetLocalPriceString(cShopItemTemplet.m_MarketID, cShopItemTemplet.m_ProductID);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_imgPrice, false);
			NKCUtil.SetGameobjectActive(this.m_imgSalePrice, false);
			this.m_lbNormalPrice.text = NKCPublisherModule.InAppPurchase.GetLocalPriceString(cShopItemTemplet.m_MarketID, cShopItemTemplet.m_ProductID);
		}

		// Token: 0x06007D60 RID: 32096 RVA: 0x002A0768 File Offset: 0x0029E968
		protected override void UpdateTimeLeft(DateTime eventEndTime)
		{
		}

		// Token: 0x06007D61 RID: 32097 RVA: 0x002A076A File Offset: 0x0029E96A
		protected override void SetShowTimeLeft(bool bValue)
		{
		}

		// Token: 0x04006A39 RID: 27193
		[Header("카드형 슬롯 전용")]
		public NKCUISlot m_Slot;

		// Token: 0x04006A3A RID: 27194
		public Image m_Image;

		// Token: 0x04006A3B RID: 27195
		public GameObject m_objSalePriceRoot;

		// Token: 0x04006A3C RID: 27196
		public Text m_lbSaleOldPrice;

		// Token: 0x04006A3D RID: 27197
		public Image m_imgSalePrice;

		// Token: 0x04006A3E RID: 27198
		public Text m_lbSalePrice;

		// Token: 0x04006A3F RID: 27199
		public GameObject m_objNormalPriceRoot;

		// Token: 0x04006A40 RID: 27200
		public Text m_lbNormalPrice;

		// Token: 0x04006A41 RID: 27201
		public Image m_imgPrice;

		// Token: 0x04006A42 RID: 27202
		public GameObject m_objPriceParent;

		// Token: 0x04006A43 RID: 27203
		public Text m_lbFreePrice;
	}
}
