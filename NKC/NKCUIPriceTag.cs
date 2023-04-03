using System;
using NKC.Publisher;
using NKC.UI.Tooltip;
using NKM;
using NKM.Shop;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000951 RID: 2385
	public class NKCUIPriceTag : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
	{
		// Token: 0x06005F1B RID: 24347 RVA: 0x001D8C8E File Offset: 0x001D6E8E
		public bool SetData(NKCUIPriceTag.ConsumedResource consumedResource, bool showMinus = false, bool changeColor = true)
		{
			return this.SetData(consumedResource.m_priceItemID, consumedResource.m_Count, showMinus, changeColor, false);
		}

		// Token: 0x06005F1C RID: 24348 RVA: 0x001D8CA8 File Offset: 0x001D6EA8
		public bool SetData(int priceItemID, int price, bool showMinus = false, bool changeColor = true, bool bHidePriceIcon = false)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			this.m_priceItemID = priceItemID;
			bool flag = myUserData.CheckPrice(price, priceItemID);
			if (this.m_lbPrice != null)
			{
				if (price == 0 && bHidePriceIcon)
				{
					NKCUtil.SetGameobjectActive(this.m_imgIcon, false);
					this.m_lbPrice.text = NKCUtilString.GET_STRING_SHOP_FREE;
				}
				else
				{
					this.SetPriceSprite(priceItemID);
					if (priceItemID == 0)
					{
						Debug.LogError("NKCUIPriceTag : Inapp Purchase item should not passed by PriceItemID");
						if (changeColor)
						{
							this.m_lbPrice.color = this.m_colHasEnough;
						}
						this.m_lbPrice.text = NKCUtilString.GetInAppPurchasePriceString(price, 0);
					}
					else
					{
						if (changeColor)
						{
							this.m_lbPrice.color = (flag ? this.m_colHasEnough : this.m_colNotEnough);
						}
						this.m_lbPrice.text = (showMinus ? ("-" + price.ToString()) : price.ToString());
					}
				}
			}
			return flag;
		}

		// Token: 0x06005F1D RID: 24349 RVA: 0x001D8D94 File Offset: 0x001D6F94
		public bool SetData(ShopItemTemplet itemTemplet, bool showMinus = false, bool changeColor = true)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			this.m_priceItemID = itemTemplet.m_PriceItemID;
			this.SetPriceSprite(itemTemplet.m_PriceItemID);
			bool flag = myUserData.CheckPrice(itemTemplet.m_Price, itemTemplet.m_PriceItemID);
			if (this.m_lbPrice != null)
			{
				if (itemTemplet.m_Price == 0)
				{
					NKCUtil.SetGameobjectActive(this.m_imgIcon, false);
					this.m_lbPrice.text = NKCUtilString.GET_STRING_SHOP_FREE;
				}
				else if (itemTemplet.m_PriceItemID == 0)
				{
					if (changeColor)
					{
						this.m_lbPrice.color = this.m_colHasEnough;
					}
					this.m_lbPrice.text = NKCPublisherModule.InAppPurchase.GetLocalPriceString(itemTemplet.m_MarketID, itemTemplet.m_ProductID);
				}
				else
				{
					if (changeColor)
					{
						this.m_lbPrice.color = (flag ? this.m_colHasEnough : this.m_colNotEnough);
					}
					this.m_lbPrice.text = (showMinus ? ("-" + itemTemplet.m_Price.ToString()) : itemTemplet.m_Price.ToString());
				}
			}
			return flag;
		}

		// Token: 0x06005F1E RID: 24350 RVA: 0x001D8EA0 File Offset: 0x001D70A0
		public bool SetDataByHaveCount(int price, int haveCount, bool showMinus = false, bool changeColor = true)
		{
			bool flag = price <= haveCount;
			if (this.m_lbPrice != null)
			{
				if (changeColor)
				{
					this.m_lbPrice.color = (flag ? this.m_colHasEnough : this.m_colNotEnough);
				}
				this.m_lbPrice.text = (showMinus ? ("-" + price.ToString()) : price.ToString());
			}
			return flag;
		}

		// Token: 0x06005F1F RID: 24351 RVA: 0x001D8F0C File Offset: 0x001D710C
		public void SetLabelTextColor(Color color)
		{
			if (this.m_lbPrice != null)
			{
				this.m_lbPrice.color = color;
			}
		}

		// Token: 0x06005F20 RID: 24352 RVA: 0x001D8F28 File Offset: 0x001D7128
		private void SetPriceSprite(int itemID)
		{
			if (this.m_imgIcon == null)
			{
				return;
			}
			Sprite orLoadMiscItemSmallIcon = NKCResourceUtility.GetOrLoadMiscItemSmallIcon(itemID);
			NKCUtil.SetImageSprite(this.m_imgIcon, orLoadMiscItemSmallIcon, true);
		}

		// Token: 0x06005F21 RID: 24353 RVA: 0x001D8F58 File Offset: 0x001D7158
		public void OnPointerDown(PointerEventData eventData)
		{
			if (this.m_priceItemID == 0)
			{
				return;
			}
			NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeMiscItemData(this.m_priceItemID, 1L, 0);
			NKCUITooltip.Instance.Open(slotData, new Vector2?(eventData.position));
		}

		// Token: 0x04004B34 RID: 19252
		public int m_priceItemID;

		// Token: 0x04004B35 RID: 19253
		public Image m_imgIcon;

		// Token: 0x04004B36 RID: 19254
		public Text m_lbPrice;

		// Token: 0x04004B37 RID: 19255
		public Color m_colHasEnough = Color.white;

		// Token: 0x04004B38 RID: 19256
		public Color m_colNotEnough = Color.red;

		// Token: 0x020015D5 RID: 5589
		public struct ConsumedResource
		{
			// Token: 0x0600AE62 RID: 44642 RVA: 0x0035B119 File Offset: 0x00359319
			public ConsumedResource(int priceItemID, int count)
			{
				this.m_priceItemID = priceItemID;
				this.m_Count = count;
			}

			// Token: 0x0400A2A1 RID: 41633
			public int m_priceItemID;

			// Token: 0x0400A2A2 RID: 41634
			public int m_Count;
		}
	}
}
