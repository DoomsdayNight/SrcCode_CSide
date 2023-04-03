using System;
using NKC.Publisher;
using NKM;
using NKM.Shop;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Shop
{
	// Token: 0x02000ADE RID: 2782
	public class NKCUIShopSlotCard : NKCUIShopSlotBase
	{
		// Token: 0x06007D1B RID: 32027 RVA: 0x0029EF2C File Offset: 0x0029D12C
		protected override void SetGoodsImage(ShopItemTemplet shopTemplet, bool bFirstBuy)
		{
			string text = string.IsNullOrEmpty(this.m_OverrideImageAsset) ? shopTemplet.m_CardImage : this.m_OverrideImageAsset;
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(NKMAssetName.ParseBundleName("AB_UI_NKM_UI_SHOP_IMG", text));
			if (orLoadAssetResource == null)
			{
				Debug.LogError(string.Format("Shop Sprite {0}(from productID {1}) null", text, shopTemplet.m_ProductID));
			}
			NKCUtil.SetImageSprite(this.m_imgItem, orLoadAssetResource, false);
			if (this.m_lbDescription != null)
			{
				this.m_lbDescription.text = NKCUtilString.GetShopDescriptionText(shopTemplet.GetItemDesc(), bFirstBuy);
			}
		}

		// Token: 0x06007D1C RID: 32028 RVA: 0x0029EFBC File Offset: 0x0029D1BC
		protected override void SetGoodsImage(NKMShopRandomListData shopRandomTemplet)
		{
			Sprite sprite = null;
			switch (shopRandomTemplet.itemType)
			{
			case NKM_REWARD_TYPE.RT_NONE:
				Debug.LogError("RandomShopTemplet Type None!");
				break;
			case NKM_REWARD_TYPE.RT_UNIT:
			case NKM_REWARD_TYPE.RT_OPERATOR:
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(shopRandomTemplet.itemId);
				if (unitTempletBase != null)
				{
					sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, unitTempletBase);
					this.m_lbDescription.text = NKCUtilString.GetGradeString(unitTempletBase.m_NKM_UNIT_GRADE) + " " + NKCUtilString.GetUnitStyleName(unitTempletBase.m_NKM_UNIT_STYLE_TYPE);
				}
				break;
			}
			case NKM_REWARD_TYPE.RT_SHIP:
			{
				NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(shopRandomTemplet.itemId);
				if (unitTempletBase2 != null)
				{
					sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitTempletBase2);
					this.m_lbDescription.text = NKCUtilString.GetGradeString(unitTempletBase2.m_NKM_UNIT_GRADE) + " " + NKCUtilString.GetUnitStyleName(unitTempletBase2.m_NKM_UNIT_STYLE_TYPE);
				}
				break;
			}
			case NKM_REWARD_TYPE.RT_MISC:
			case NKM_REWARD_TYPE.RT_MISSION_POINT:
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(shopRandomTemplet.itemId);
				if (itemMiscTempletByID != null)
				{
					sprite = NKCResourceUtility.GetOrLoadMiscItemIcon(itemMiscTempletByID);
					this.m_lbDescription.text = itemMiscTempletByID.GetItemDesc();
				}
				break;
			}
			case NKM_REWARD_TYPE.RT_USER_EXP:
			{
				NKMItemMiscTemplet itemMiscTempletByRewardType = NKMItemManager.GetItemMiscTempletByRewardType(shopRandomTemplet.itemType);
				if (itemMiscTempletByRewardType != null)
				{
					sprite = NKCResourceUtility.GetOrLoadMiscItemIcon(itemMiscTempletByRewardType);
					this.m_lbDescription.text = itemMiscTempletByRewardType.GetItemDesc();
				}
				break;
			}
			case NKM_REWARD_TYPE.RT_EQUIP:
			{
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(shopRandomTemplet.itemId);
				if (equipTemplet != null)
				{
					sprite = NKCResourceUtility.GetOrLoadEquipIcon(equipTemplet);
					this.m_lbDescription.text = equipTemplet.GetItemDesc();
				}
				break;
			}
			case NKM_REWARD_TYPE.RT_MOLD:
			{
				NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(shopRandomTemplet.itemId);
				if (itemMoldTempletByID != null)
				{
					sprite = NKCResourceUtility.GetOrLoadMoldIcon(itemMoldTempletByID);
					this.m_lbDescription.text = itemMoldTempletByID.GetItemDesc();
				}
				break;
			}
			case NKM_REWARD_TYPE.RT_SKIN:
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(shopRandomTemplet.itemId);
				if (skinTemplet != null)
				{
					sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, skinTemplet);
					this.m_lbDescription.text = skinTemplet.GetTitle();
				}
				break;
			}
			}
			this.m_imgItem.sprite = sprite;
		}

		// Token: 0x06007D1D RID: 32029 RVA: 0x0029F1B2 File Offset: 0x0029D3B2
		protected override void UpdateTimeLeft(DateTime eventEndTime)
		{
			this.m_lbTimeLeft.text = this.GetUpdateTimeLeftString(eventEndTime);
		}

		// Token: 0x06007D1E RID: 32030 RVA: 0x0029F1C6 File Offset: 0x0029D3C6
		protected override void SetShowTimeLeft(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objTimeLeftRoot, bValue);
		}

		// Token: 0x06007D1F RID: 32031 RVA: 0x0029F1D4 File Offset: 0x0029D3D4
		protected override void SetPrice(int priceItemID, int Price, bool bSale = false, int oldPrice = 0)
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
				Sprite priceImage = base.GetPriceImage(priceItemID);
				NKCUtil.SetImageSprite(this.m_imgPrice, priceImage, true);
				this.m_lbOldPrice.text = oldPrice.ToString();
				this.m_lbPrice.text = Price.ToString();
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_imgPrice, true);
			Sprite priceImage2 = base.GetPriceImage(priceItemID);
			NKCUtil.SetImageSprite(this.m_imgPrice, priceImage2, true);
			this.m_lbPrice.text = Price.ToString();
		}

		// Token: 0x06007D20 RID: 32032 RVA: 0x0029F2A0 File Offset: 0x0029D4A0
		protected override void SetInappPurchasePrice(ShopItemTemplet cShopItemTemplet, int price, bool bSale = false, int oldPrice = 0)
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

		// Token: 0x06007D21 RID: 32033 RVA: 0x0029F36C File Offset: 0x0029D56C
		private string GetUpdateTimeLeftString(DateTime endTime)
		{
			if (NKCSynchronizedTime.IsFinished(endTime))
			{
				return NKCUtilString.GET_STRING_QUIT;
			}
			TimeSpan timeLeft = NKCSynchronizedTime.GetTimeLeft(endTime);
			if (timeLeft.Days > 0)
			{
				return string.Format(NKCUtilString.GET_STRING_TIME_DAY_ONE_PARAM, timeLeft.Days + 1);
			}
			return NKCUtilString.GET_STRING_TIME_REMAIN_SHOP_EXPIRE_TODAY;
		}

		// Token: 0x06007D22 RID: 32034 RVA: 0x0029F3B6 File Offset: 0x0029D5B6
		public void SetSlotCardItemImage(Sprite sprite)
		{
			NKCUtil.SetImageSprite(this.m_imgItem, sprite, false);
		}

		// Token: 0x040069F0 RID: 27120
		[Header("-- 카드형 슬롯 전용 --")]
		public Image m_imgItem;

		// Token: 0x040069F1 RID: 27121
		public Text m_lbDescription;

		// Token: 0x040069F2 RID: 27122
		[Header("월정액 등의 남은 기간")]
		public GameObject m_objTimeLeftRoot;

		// Token: 0x040069F3 RID: 27123
		public Text m_lbTimeLeft;

		// Token: 0x040069F4 RID: 27124
		[Header("세일/가격 관련")]
		public GameObject m_objSalePriceRoot;

		// Token: 0x040069F5 RID: 27125
		public Text m_lbOldPrice;

		// Token: 0x040069F6 RID: 27126
		public Text m_lbPrice;

		// Token: 0x040069F7 RID: 27127
		public Image m_imgPrice;

		// Token: 0x040069F8 RID: 27128
		public GameObject m_objPriceParent;

		// Token: 0x040069F9 RID: 27129
		public Text m_lbFreePrice;
	}
}
