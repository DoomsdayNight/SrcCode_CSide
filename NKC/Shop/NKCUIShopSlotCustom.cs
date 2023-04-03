using System;
using System.Collections.Generic;
using NKC.Publisher;
using NKM;
using NKM.Shop;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Shop
{
	// Token: 0x02000ADF RID: 2783
	public class NKCUIShopSlotCustom : NKCUIShopSlotBase
	{
		// Token: 0x06007D24 RID: 32036 RVA: 0x0029F3D0 File Offset: 0x0029D5D0
		public override void Init(NKCUIShopSlotBase.OnBuy onBuy, NKCUIShopSlotBase.OnRefreshRequired onRefreshRequired)
		{
			base.Init(onBuy, onRefreshRequired);
			foreach (NKCUISlot nkcuislot in this.m_lstItemSlot)
			{
				nkcuislot.Init();
				if (this.m_spEmptyCustom != null)
				{
					nkcuislot.SetCustomizedEmptySP(this.m_spEmptyCustom);
				}
			}
		}

		// Token: 0x06007D25 RID: 32037 RVA: 0x0029F444 File Offset: 0x0029D644
		protected override void SetGoodsImage(ShopItemTemplet shopTemplet, bool bFirstBuy)
		{
			if (shopTemplet.m_ItemType != NKM_REWARD_TYPE.RT_MISC)
			{
				this.DisableSlot();
				return;
			}
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(shopTemplet.m_ItemID);
			if (itemMiscTempletByID.m_ItemMiscType != NKM_ITEM_MISC_TYPE.IMT_CUSTOM_PACKAGE)
			{
				this.DisableSlot();
				return;
			}
			if (!string.IsNullOrEmpty(shopTemplet.m_CardImage))
			{
				Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(NKMAssetName.ParseBundleName("AB_UI_NKM_UI_SHOP_IMG", shopTemplet.m_CardImage));
				if (orLoadAssetResource == null)
				{
					Debug.LogError(string.Format("Shop Sprite {0}(from productID {1}) null", shopTemplet.m_CardImage, shopTemplet.m_ProductID));
				}
				this.m_imgItem.sprite = orLoadAssetResource;
				NKCUtil.SetGameobjectActive(this.m_imgItem, true);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_imgItem, false);
			}
			this.SetSlot(itemMiscTempletByID);
		}

		// Token: 0x06007D26 RID: 32038 RVA: 0x0029F4F8 File Offset: 0x0029D6F8
		private void SetSlot(NKMItemMiscTemplet itemTemplet)
		{
			List<NKCUISlot.SlotData> list = this.MakeCustomPackageItemList(itemTemplet);
			for (int i = 0; i < this.m_lstItemSlot.Count; i++)
			{
				NKCUISlot nkcuislot = this.m_lstItemSlot[i];
				if (i < list.Count)
				{
					NKCUISlot.SlotData slotData = list[i];
					NKCUtil.SetGameobjectActive(nkcuislot, true);
					if (slotData != null)
					{
						nkcuislot.SetData(slotData, false, slotData.eType == NKCUISlot.eSlotMode.ItemMisc, false, new NKCUISlot.OnClick(this.OnSlotClick));
					}
					else
					{
						nkcuislot.SetEmpty(new NKCUISlot.OnClick(this.OnSlotClick));
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(nkcuislot, false);
				}
			}
		}

		// Token: 0x06007D27 RID: 32039 RVA: 0x0029F588 File Offset: 0x0029D788
		private List<NKCUISlot.SlotData> MakeCustomPackageItemList(NKMItemMiscTemplet itemTemplet)
		{
			List<NKCUISlot.SlotData> list = new List<NKCUISlot.SlotData>();
			if (itemTemplet == null)
			{
				return list;
			}
			List<NKMRandomBoxItemTemplet> randomBoxItemTempletList = NKCRandomBoxManager.GetRandomBoxItemTempletList(itemTemplet.m_RewardGroupID);
			if (itemTemplet.m_RewardGroupID != 0 && randomBoxItemTempletList == null)
			{
				Debug.LogError("rewardgroup null! ID : " + itemTemplet.m_RewardGroupID.ToString());
			}
			if (randomBoxItemTempletList != null)
			{
				for (int i = 0; i < randomBoxItemTempletList.Count; i++)
				{
					NKMRandomBoxItemTemplet nkmrandomBoxItemTemplet = randomBoxItemTempletList[i];
					NKCUISlot.SlotData item = NKCUISlot.SlotData.MakeRewardTypeData(nkmrandomBoxItemTemplet.m_reward_type, nkmrandomBoxItemTemplet.m_RewardID, nkmrandomBoxItemTemplet.TotalQuantity_Max, 0);
					list.Add(item);
				}
			}
			if (itemTemplet.CustomPackageTemplets != null)
			{
				for (int j = 0; j < itemTemplet.CustomPackageTemplets.Count; j++)
				{
					list.Add(null);
				}
			}
			return list;
		}

		// Token: 0x06007D28 RID: 32040 RVA: 0x0029F63C File Offset: 0x0029D83C
		private void DisableSlot()
		{
			foreach (NKCUISlot targetMono in this.m_lstItemSlot)
			{
				NKCUtil.SetGameobjectActive(targetMono, false);
			}
		}

		// Token: 0x06007D29 RID: 32041 RVA: 0x0029F690 File Offset: 0x0029D890
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

		// Token: 0x06007D2A RID: 32042 RVA: 0x0029F75C File Offset: 0x0029D95C
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

		// Token: 0x06007D2B RID: 32043 RVA: 0x0029F827 File Offset: 0x0029DA27
		protected override void UpdateTimeLeft(DateTime eventEndTime)
		{
		}

		// Token: 0x06007D2C RID: 32044 RVA: 0x0029F829 File Offset: 0x0029DA29
		protected override void SetShowTimeLeft(bool bValue)
		{
		}

		// Token: 0x06007D2D RID: 32045 RVA: 0x0029F82B File Offset: 0x0029DA2B
		private string GetUpdateTimeLeftString(DateTime endTime)
		{
			if (NKCSynchronizedTime.IsFinished(endTime))
			{
				return NKCUtilString.GET_STRING_QUIT;
			}
			return NKCStringTable.GetString("SI_DP_SHOP_CUSTOM_TIME_LEFT", new object[]
			{
				NKCSynchronizedTime.GetTimeLeftString(endTime)
			});
		}

		// Token: 0x06007D2E RID: 32046 RVA: 0x0029F854 File Offset: 0x0029DA54
		private void OnSlotClick(NKCUISlot.SlotData slotData, bool bLocked)
		{
			base.OnBtnBuy();
		}

		// Token: 0x06007D2F RID: 32047 RVA: 0x0029F85C File Offset: 0x0029DA5C
		protected override void SetRibbon(ShopItemRibbon ribbonType)
		{
			NKCUtil.SetLabelText(this.m_lbRibbon, NKCShopManager.GetRibbonString(ribbonType));
			NKCUtil.SetGameobjectActive(this.m_lbRibbon, ribbonType > ShopItemRibbon.None);
			NKCUtil.SetGameobjectActive(this.m_imgRibbon, ribbonType > ShopItemRibbon.None);
		}

		// Token: 0x040069FA RID: 27130
		[Header("커스텀 슬롯 전용")]
		public Image m_imgItem;

		// Token: 0x040069FB RID: 27131
		public List<NKCUISlot> m_lstItemSlot;

		// Token: 0x040069FC RID: 27132
		public GameObject m_objSalePriceRoot;

		// Token: 0x040069FD RID: 27133
		public Text m_lbOldPrice;

		// Token: 0x040069FE RID: 27134
		public Text m_lbPrice;

		// Token: 0x040069FF RID: 27135
		public Image m_imgPrice;

		// Token: 0x04006A00 RID: 27136
		public Sprite m_spEmptyCustom;

		// Token: 0x04006A01 RID: 27137
		public GameObject m_objPriceParent;

		// Token: 0x04006A02 RID: 27138
		public Text m_lbFreePrice;
	}
}
