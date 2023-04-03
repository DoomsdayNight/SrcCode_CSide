using System;
using NKC.Publisher;
using NKM;
using NKM.Shop;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Shop
{
	// Token: 0x02000AD0 RID: 2768
	public class NKCUIComShopBuyButton : MonoBehaviour
	{
		// Token: 0x06007C23 RID: 31779 RVA: 0x002971AC File Offset: 0x002953AC
		public bool SetData(ShopItemTemplet shopTemplet, UnityAction onBtnBuy, bool bIsFirstBuy)
		{
			int buyCountLeft = NKCShopManager.GetBuyCountLeft(shopTemplet.m_ProductID);
			NKCUtil.SetButtonClickDelegate(this.m_cbtnBuy, new UnityAction(this.OnClickBtn));
			this.m_ReddotType = NKCShopManager.GetReddotType(shopTemplet);
			this.m_onBtnBuy = onBtnBuy;
			int productID = shopTemplet.m_ProductID;
			bool flag2;
			bool flag = NKCShopManager.IsProductAvailable(shopTemplet, out flag2, true, false);
			if (buyCountLeft != -1)
			{
				if (buyCountLeft == 0)
				{
					NKCUtil.SetGameobjectActive(this.m_objPriceRoot, false);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objPriceRoot, true);
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objPriceRoot, true);
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return false;
			}
			int realPrice = nkmuserData.m_ShopData.GetRealPrice(shopTemplet, 1, false);
			bool flag3 = realPrice < shopTemplet.m_Price;
			if (shopTemplet.m_PriceItemID == 0)
			{
				if (flag3)
				{
					this.SetInappPurchasePrice(shopTemplet, realPrice, true, shopTemplet.m_Price);
				}
				else
				{
					this.SetInappPurchasePrice(shopTemplet, shopTemplet.m_Price, false, 0);
				}
			}
			else if (flag3)
			{
				this.SetPrice(shopTemplet.m_PriceItemID, realPrice, true, shopTemplet.m_Price);
			}
			else
			{
				this.SetPrice(shopTemplet.m_PriceItemID, realPrice, false, 0);
			}
			bool flag4 = false;
			if (shopTemplet.m_DiscountRate > 0f && NKCSynchronizedTime.IsEventTime(shopTemplet.discountIntervalId, shopTemplet.DiscountStartDateUtc, shopTemplet.DiscountEndDateUtc) && shopTemplet.DiscountEndDateUtc != DateTime.MinValue && shopTemplet.DiscountEndDateUtc != DateTime.MaxValue)
			{
				flag4 = true;
				this.m_tEndDateDiscountTime = shopTemplet.DiscountEndDateUtc;
				this.UpdateDiscountTime(this.m_tEndDateDiscountTime);
			}
			else
			{
				this.m_tEndDateDiscountTime = default(DateTime);
			}
			if (!shopTemplet.HasDiscountDateLimit)
			{
				NKCUtil.SetGameobjectActive(this.m_objDiscountRate, shopTemplet.m_DiscountRate > 0f);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objDiscountRate, shopTemplet.m_DiscountRate > 0f && flag4);
			}
			float num = (100f - shopTemplet.m_DiscountRate) / 10f;
			NKCUtil.SetLabelText(this.m_txtDiscountRate, NKCStringTable.GetString("SI_DP_SHOP_DISCOUNT_RATE", new object[]
			{
				(int)shopTemplet.m_DiscountRate,
				num
			}));
			if (!flag)
			{
				NKCUtil.SetGameobjectActive(this.m_objPriceRoot, false);
			}
			this.m_bTimerUpdate = flag4;
			this.SetShowBadgeTime(flag4);
			NKCUtil.SetShopReddotImage(NKCShopManager.GetReddotType(shopTemplet), this.m_objReddot, this.m_objReddot_RED, this.m_objReddot_YELLOW);
			return true;
		}

		// Token: 0x06007C24 RID: 31780 RVA: 0x002973EC File Offset: 0x002955EC
		private void SetPrice(int priceItemID, int Price, bool bSale = false, int oldPrice = 0)
		{
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
				NKCUtil.SetGameobjectActive(this.m_imgPrice, true);
				Sprite priceImage = this.GetPriceImage(priceItemID);
				NKCUtil.SetImageSprite(this.m_imgPrice, priceImage, true);
				this.m_lbOldPrice.text = oldPrice.ToString();
				this.m_lbPrice.text = Price.ToString();
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_imgPrice, true);
			Sprite priceImage2 = this.GetPriceImage(priceItemID);
			NKCUtil.SetImageSprite(this.m_imgPrice, priceImage2, true);
			this.m_lbPrice.text = Price.ToString();
		}

		// Token: 0x06007C25 RID: 31781 RVA: 0x002974B8 File Offset: 0x002956B8
		private void SetInappPurchasePrice(ShopItemTemplet cShopItemTemplet, int price, bool bSale = false, int oldPrice = 0)
		{
			NKCUtil.SetGameobjectActive(this.m_objSalePriceRoot, bSale);
			NKCUtil.SetGameobjectActive(this.m_objPriceParent, price > 0);
			NKCUtil.SetGameobjectActive(this.m_lbFreePrice, price <= 0);
			if (price <= 0)
			{
				NKCUtil.SetLabelText(this.m_lbFreePrice, NKCUtilString.GET_STRING_SHOP_FREE);
				return;
			}
			if (bSale)
			{
				NKCUtil.SetGameobjectActive(this.m_imgPrice, false);
				this.m_lbOldPrice.text = NKCUtilString.GetInAppPurchasePriceString(oldPrice, cShopItemTemplet.m_ProductID);
				this.m_lbPrice.text = NKCPublisherModule.InAppPurchase.GetLocalPriceString(cShopItemTemplet.m_MarketID, cShopItemTemplet.m_ProductID);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_imgPrice, false);
			this.m_lbPrice.text = NKCPublisherModule.InAppPurchase.GetLocalPriceString(cShopItemTemplet.m_MarketID, cShopItemTemplet.m_ProductID);
		}

		// Token: 0x06007C26 RID: 31782 RVA: 0x00297582 File Offset: 0x00295782
		private Sprite GetPriceImage(int priceItemID)
		{
			return NKCResourceUtility.GetOrLoadMiscItemSmallIcon(priceItemID);
		}

		// Token: 0x06007C27 RID: 31783 RVA: 0x0029758A File Offset: 0x0029578A
		private void SetShowBadgeTime(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objDiscountDay, bValue);
		}

		// Token: 0x06007C28 RID: 31784 RVA: 0x00297598 File Offset: 0x00295798
		private void UpdateDiscountTime(DateTime endTime)
		{
			string msg;
			if (NKCSynchronizedTime.IsFinished(endTime))
			{
				msg = NKCUtilString.GET_STRING_QUIT;
			}
			else
			{
				msg = NKCUtilString.GetRemainTimeStringOneParam(endTime);
			}
			NKCUtil.SetLabelText(this.m_txtDiscountDay, msg);
		}

		// Token: 0x06007C29 RID: 31785 RVA: 0x002975C8 File Offset: 0x002957C8
		private void OnClickBtn()
		{
			UnityAction onBtnBuy = this.m_onBtnBuy;
			if (onBtnBuy != null)
			{
				onBtnBuy();
			}
			if (this.m_ReddotType != ShopReddotType.REDDOT_PURCHASED)
			{
				NKCUtil.SetGameobjectActive(this.m_objReddot, false);
			}
		}

		// Token: 0x06007C2A RID: 31786 RVA: 0x002975F0 File Offset: 0x002957F0
		private void Update()
		{
			if (this.m_bTimerUpdate)
			{
				this.m_updateTimer += Time.deltaTime;
				if (1f < this.m_updateTimer)
				{
					this.m_updateTimer = 0f;
					if (this.m_tEndDateDiscountTime != DateTime.MinValue)
					{
						this.UpdateDiscountTime(this.m_tEndDateDiscountTime);
					}
				}
			}
		}

		// Token: 0x040068E3 RID: 26851
		public NKCUIComButton m_cbtnBuy;

		// Token: 0x040068E4 RID: 26852
		public GameObject m_objPriceRoot;

		// Token: 0x040068E5 RID: 26853
		[Header("할인 관련")]
		public GameObject m_objDiscountDay;

		// Token: 0x040068E6 RID: 26854
		public Text m_txtDiscountDay;

		// Token: 0x040068E7 RID: 26855
		public GameObject m_objDiscountRate;

		// Token: 0x040068E8 RID: 26856
		public Text m_txtDiscountRate;

		// Token: 0x040068E9 RID: 26857
		[Header("세일/가격 관련")]
		public GameObject m_objSalePriceRoot;

		// Token: 0x040068EA RID: 26858
		public Text m_lbOldPrice;

		// Token: 0x040068EB RID: 26859
		public Text m_lbPrice;

		// Token: 0x040068EC RID: 26860
		public Image m_imgPrice;

		// Token: 0x040068ED RID: 26861
		public GameObject m_objPriceParent;

		// Token: 0x040068EE RID: 26862
		public Text m_lbFreePrice;

		// Token: 0x040068EF RID: 26863
		public GameObject m_objReddot;

		// Token: 0x040068F0 RID: 26864
		public GameObject m_objReddot_RED;

		// Token: 0x040068F1 RID: 26865
		public GameObject m_objReddot_YELLOW;

		// Token: 0x040068F2 RID: 26866
		private DateTime m_tEndDateDiscountTime;

		// Token: 0x040068F3 RID: 26867
		private float m_updateTimer;

		// Token: 0x040068F4 RID: 26868
		private bool m_bTimerUpdate;

		// Token: 0x040068F5 RID: 26869
		private const float TIMER_UPDATE_INTERVAL = 1f;

		// Token: 0x040068F6 RID: 26870
		private ShopReddotType m_ReddotType;

		// Token: 0x040068F7 RID: 26871
		private UnityAction m_onBtnBuy;
	}
}
