using System;
using ClientPacket.Shop;
using Cs.Logging;
using NKM;
using NKM.Shop;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Shop
{
	// Token: 0x02000ADD RID: 2781
	public abstract class NKCUIShopSlotBase : MonoBehaviour
	{
		// Token: 0x170014C4 RID: 5316
		// (get) Token: 0x06007D00 RID: 32000 RVA: 0x0029DF02 File Offset: 0x0029C102
		// (set) Token: 0x06007D01 RID: 32001 RVA: 0x0029DF0A File Offset: 0x0029C10A
		public int ProductID { get; private set; }

		// Token: 0x06007D02 RID: 32002 RVA: 0x0029DF13 File Offset: 0x0029C113
		public void SetOverrideImageAsset(string value)
		{
			this.m_OverrideImageAsset = value;
		}

		// Token: 0x06007D03 RID: 32003 RVA: 0x0029DF1C File Offset: 0x0029C11C
		public virtual void Init(NKCUIShopSlotBase.OnBuy onBuy, NKCUIShopSlotBase.OnRefreshRequired onRefreshRequired)
		{
			this.dOnBuy = onBuy;
			this.dOnRefreshRequired = onRefreshRequired;
			NKCUtil.SetButtonClickDelegate(this.m_cbtnBuy, new UnityAction(this.OnBtnBuy));
			NKCUtil.SetGameobjectActive(this.m_objAdmin, false);
		}

		// Token: 0x06007D04 RID: 32004 RVA: 0x0029DF50 File Offset: 0x0029C150
		protected void OnBtnBuy()
		{
			if (this.dOnBuy != null)
			{
				if (this.m_objLocked != null && this.m_objLocked.activeSelf && this.m_ProductTemplet != null)
				{
					if (this.m_ProductTemplet.IsSubscribeItem() && NKCScenManager.CurrentUserData().m_ShopData.subscriptions.ContainsKey(this.m_ProductTemplet.m_ProductID))
					{
						NKMShopSubscriptionData nkmshopSubscriptionData = NKCScenManager.CurrentUserData().m_ShopData.subscriptions[this.m_ProductTemplet.m_ProductID];
						if (NKCSynchronizedTime.GetServerUTCTime(0.0).AddDays((double)NKMCommonConst.SubscriptionBuyCriteriaDate) < nkmshopSubscriptionData.endDate)
						{
							NKCUIManager.NKCPopupMessage.Open(new PopupMessage(string.Format(NKCUtilString.GET_STRING_SHOP_SUBSCRIBE_DAY_ENOUGH_DESC, NKMCommonConst.SubscriptionBuyCriteriaDate), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
							return;
						}
					}
					bool flag = true;
					if (this.m_ProductTemplet.m_paidAmountRequired > 0.0)
					{
						flag = false;
					}
					else if (!NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), this.m_ProductTemplet.m_UnlockInfo, false))
					{
						flag = false;
					}
					if (!flag)
					{
						PopupMessage msg = new PopupMessage(NKCUtilString.GET_STRING_SHOP_NOT_ENOUGH_REQUIREMENT, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false);
						NKCUIManager.NKCPopupMessage.Open(msg);
						return;
					}
				}
				this.dOnBuy(this.ProductID);
				if (this.m_objRedDot != null && this.m_ProductTemplet != null && NKCShopManager.GetReddotType(this.m_ProductTemplet) != ShopReddotType.REDDOT_PURCHASED)
				{
					NKCUtil.SetGameobjectActive(this.m_objRedDot, false);
				}
			}
		}

		// Token: 0x06007D05 RID: 32005
		protected abstract void SetPrice(int priceItemID, int Price, bool bSale = false, int oldPrice = 0);

		// Token: 0x06007D06 RID: 32006
		protected abstract void SetInappPurchasePrice(ShopItemTemplet cShopItemTemplet, int price, bool bSale = false, int oldPrice = 0);

		// Token: 0x06007D07 RID: 32007
		protected abstract void SetGoodsImage(ShopItemTemplet shopTemplet, bool bFirstBuy);

		// Token: 0x06007D08 RID: 32008 RVA: 0x0029E0D5 File Offset: 0x0029C2D5
		protected virtual void SetGoodsImage(NKMShopRandomListData shopRandomTemplet)
		{
		}

		// Token: 0x06007D09 RID: 32009 RVA: 0x0029E0D7 File Offset: 0x0029C2D7
		protected virtual void PostSetData(ShopItemTemplet shopTemplet)
		{
		}

		// Token: 0x06007D0A RID: 32010 RVA: 0x0029E0D9 File Offset: 0x0029C2D9
		protected virtual void UpdateTimeLeft(DateTime eventEndTime)
		{
		}

		// Token: 0x06007D0B RID: 32011 RVA: 0x0029E0DB File Offset: 0x0029C2DB
		protected virtual void SetShowTimeLeft(bool bValue)
		{
		}

		// Token: 0x06007D0C RID: 32012 RVA: 0x0029E0DD File Offset: 0x0029C2DD
		protected virtual bool IsProductAvailable(ShopItemTemplet shopTemplet, out bool bAdmin, bool bIncludeLockedItemWithReason)
		{
			return NKCShopManager.IsProductAvailable(shopTemplet, out bAdmin, bIncludeLockedItemWithReason, false);
		}

		// Token: 0x06007D0D RID: 32013 RVA: 0x0029E0E8 File Offset: 0x0029C2E8
		public bool SetData(NKCUIShop uiShop, ShopItemTemplet shopTemplet, int buyCountLeft = -1, bool bFirstBuy = false)
		{
			NKCUtil.SetGameobjectActive(this.m_imgRibbon, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnAdminReset, false);
			this.m_uiShop = uiShop;
			this.m_ProductTemplet = shopTemplet;
			this.ProductID = shopTemplet.m_ProductID;
			this.SetRedDot();
			bool bValue;
			bool flag = this.IsProductAvailable(shopTemplet, out bValue, true);
			NKCUtil.SetGameobjectActive(this.m_objAdmin, bValue);
			if (!flag && this.m_ProductTemplet.TabTemplet.m_ShopDisplay != ShopDisplayType.Custom)
			{
				NKCUtil.SetGameobjectActive(this.m_objLocked, true);
				NKCUtil.SetGameobjectActive(this.m_lbLockedReason, false);
				this.PostSetData(shopTemplet);
				return false;
			}
			bool flag2 = false;
			bool flag3 = false;
			this.m_tEndDateLockedTime = default(DateTime);
			NKCUtil.SetGameobjectActive(this.m_objLocked, false);
			NKCUtil.SetGameobjectActive(this.m_lbLockedReason, false);
			if (buyCountLeft != -1)
			{
				if (buyCountLeft == 0)
				{
					NKCUtil.SetGameobjectActive(this.m_objPriceRoot, false);
					NKCUtil.SetGameobjectActive(this.m_objSoldOut, true);
					NKMShopData shopData = NKCScenManager.CurrentUserData().m_ShopData;
					if (shopData.histories.ContainsKey(this.ProductID) && !NKCSynchronizedTime.IsFinished(shopData.histories[this.ProductID].nextResetDate))
					{
						NKCUtil.SetLabelText(this.m_lbLockedTime, NKCSynchronizedTime.GetTimeLeftString(shopData.histories[this.ProductID].nextResetDate));
						this.m_tEndDateLockedTime = new DateTime(shopData.histories[this.ProductID].nextResetDate);
						flag2 = true;
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objPriceRoot, true);
					NKCUtil.SetGameobjectActive(this.m_objSoldOut, false);
				}
				NKCUtil.SetGameobjectActive(this.m_lbBuyCount, true);
				if (shopTemplet.TabTemplet == null)
				{
					Log.Error(string.Format("[Error] ShopTemplet[{0}] ShopTabTemplet is null!!  [{1}]", shopTemplet.m_ItemID, shopTemplet.m_TabID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Shop/NKCUIShopSlotBase.cs", 267);
				}
				if (shopTemplet.TabTemplet.IsCountResetType)
				{
					NKCUtil.SetLabelText(this.m_lbBuyCount, string.Format(NKCUtilString.GET_STRING_SHOP_PURCHASE_COUNT_TWO_PARAM, buyCountLeft, shopTemplet.m_QuantityLimit));
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbBuyCount, NKCShopManager.GetBuyCountString(shopTemplet.resetType, buyCountLeft, shopTemplet.m_QuantityLimit, false));
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objPriceRoot, true);
				NKCUtil.SetGameobjectActive(this.m_lbBuyCount, false);
				NKCUtil.SetGameobjectActive(this.m_objSoldOut, false);
			}
			switch (shopTemplet.m_TagImage)
			{
			case ShopItemRibbon.ONE_PLUS_ONE:
				if (shopTemplet.m_PurchaseEventType == PURCHASE_EVENT_REWARD_TYPE.FIRST_PURCHASE_CHANGE_REWARD_VALUE && bFirstBuy)
				{
					this.SetRibbon(ShopItemRibbon.ONE_PLUS_ONE);
					goto IL_28E;
				}
				goto IL_28E;
			}
			this.SetRibbon(shopTemplet.m_TagImage);
			IL_28E:
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				this.PostSetData(shopTemplet);
				return false;
			}
			int realPrice = nkmuserData.m_ShopData.GetRealPrice(shopTemplet, 1, false);
			bool flag4 = realPrice < shopTemplet.m_Price;
			if (shopTemplet.m_PriceItemID == 0)
			{
				if (flag4)
				{
					this.SetInappPurchasePrice(shopTemplet, realPrice, true, shopTemplet.m_Price);
				}
				else
				{
					this.SetInappPurchasePrice(shopTemplet, shopTemplet.m_Price, false, 0);
				}
			}
			else if (flag4)
			{
				this.SetPrice(shopTemplet.m_PriceItemID, realPrice, true, shopTemplet.m_Price);
			}
			else
			{
				this.SetPrice(shopTemplet.m_PriceItemID, realPrice, false, 0);
			}
			NKCUtil.SetLabelText(this.m_lbName, shopTemplet.GetItemName());
			this.SetGoodsImage(shopTemplet, bFirstBuy);
			bool flag5 = false;
			if (shopTemplet.m_DiscountRate > 0f && NKCSynchronizedTime.IsEventTime(shopTemplet.discountIntervalId, shopTemplet.DiscountStartDateUtc, shopTemplet.DiscountEndDateUtc) && shopTemplet.DiscountEndDateUtc != DateTime.MinValue && shopTemplet.DiscountEndDateUtc != DateTime.MaxValue)
			{
				flag5 = true;
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
				NKCUtil.SetGameobjectActive(this.m_objDiscountRate, shopTemplet.m_DiscountRate > 0f && flag5);
			}
			float num = (100f - shopTemplet.m_DiscountRate) / 10f;
			NKCUtil.SetLabelText(this.m_txtDiscountRate, NKCStringTable.GetString("SI_DP_SHOP_DISCOUNT_RATE", new object[]
			{
				(int)shopTemplet.m_DiscountRate,
				num
			}));
			if (shopTemplet.m_ProfitRate > 100)
			{
				NKCUtil.SetGameobjectActive(this.m_objProfitRate, true);
				NKCUtil.SetLabelText(this.m_lbProfitRate, string.Format("X {0:#.##}", (float)shopTemplet.m_ProfitRate / 100f));
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objProfitRate, false);
			}
			bool flag6 = false;
			DayOfWeek dayOfWeek;
			if (shopTemplet.IsReturningProduct)
			{
				if (!nkmuserData.IsReturnUser())
				{
					this.PostSetData(shopTemplet);
					return false;
				}
				flag6 = true;
				this.m_tEndDateEventTime = nkmuserData.GetReturnEndDate(shopTemplet.m_ReturningUserType);
				this.UpdateEventTime(this.m_tEndDateEventTime);
			}
			else if (shopTemplet.IsNewbieProduct)
			{
				DateTime newbieEndDate = NKCScenManager.CurrentUserData().GetNewbieEndDate(shopTemplet.m_NewbieDate);
				if (shopTemplet.HasDateLimit && shopTemplet.EventDateEndUtc < newbieEndDate)
				{
					this.m_tEndDateEventTime = shopTemplet.EventDateEndUtc;
				}
				else
				{
					this.m_tEndDateEventTime = newbieEndDate;
				}
				flag6 = true;
				this.UpdateEventTime(this.m_tEndDateEventTime);
			}
			else if (shopTemplet.HasDateLimit)
			{
				if (NKCSynchronizedTime.IsEventTime(shopTemplet.eventIntervalId, shopTemplet.EventDateStartUtc, shopTemplet.EventDateEndUtc))
				{
					flag6 = true;
					this.m_tEndDateEventTime = shopTemplet.EventDateEndUtc;
					this.UpdateEventTime(this.m_tEndDateEventTime);
				}
				else if (!NKCSynchronizedTime.IsFinished(shopTemplet.EventDateStartUtc))
				{
					flag2 = true;
					this.m_tEndDateLockedTime = shopTemplet.EventDateStartUtc;
					this.UpdateLockedTime(this.m_tEndDateLockedTime);
					NKCUtil.SetGameobjectActive(this.m_objLocked, true);
				}
			}
			else if (shopTemplet.IsInstantProduct)
			{
				InstantProduct instantProduct = NKCShopManager.GetInstantProduct(shopTemplet.m_ProductID);
				if (instantProduct != null)
				{
					this.m_tEndDateEventTime = NKMTime.LocalToUTC(instantProduct.endDate, 0);
					flag6 = true;
					this.UpdateEventTime(this.m_tEndDateEventTime);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objLocked, true);
				}
			}
			else if (shopTemplet.resetType.ToDayOfWeek(out dayOfWeek))
			{
				this.m_tEndDateEventTime = NKCSynchronizedTime.GetStartOfServiceTime(1);
				flag6 = true;
				this.UpdateEventTime(this.m_tEndDateEventTime);
			}
			else
			{
				this.m_tEndDateEventTime = default(DateTime);
			}
			bool flag7 = false;
			if (NKCScenManager.CurrentUserData().m_ShopData.subscriptions.ContainsKey(shopTemplet.m_ProductID))
			{
				NKMShopSubscriptionData nkmshopSubscriptionData = NKCScenManager.CurrentUserData().m_ShopData.subscriptions[shopTemplet.m_ProductID];
				if (NKCSynchronizedTime.IsEventTime(nkmshopSubscriptionData.startDate, nkmshopSubscriptionData.endDate))
				{
					flag7 = true;
					this.m_tEndDateSubscriptionTime = NKCScenManager.CurrentUserData().m_ShopData.subscriptions[shopTemplet.m_ProductID].endDate;
					this.UpdateTimeLeft(this.m_tEndDateSubscriptionTime);
					if (NKCSynchronizedTime.GetServerUTCTime(0.0).AddDays((double)NKMCommonConst.SubscriptionBuyCriteriaDate) < nkmshopSubscriptionData.endDate)
					{
						flag3 = true;
						NKCUtil.SetGameobjectActive(this.m_objLocked, true);
					}
				}
				else
				{
					this.m_tEndDateSubscriptionTime = default(DateTime);
				}
			}
			else
			{
				this.m_tEndDateSubscriptionTime = default(DateTime);
			}
			if (NKCScenManager.CurrentUserData().m_ShopData.GetTotalPayment() < shopTemplet.m_paidAmountRequired)
			{
				NKCUtil.SetGameobjectActive(this.m_objLocked, true);
			}
			else if (buyCountLeft != 0 && !NKMContentUnlockManager.IsContentUnlocked(nkmuserData, this.m_ProductTemplet.m_UnlockInfo, false))
			{
				NKCUtil.SetGameobjectActive(this.m_objLocked, true);
				if (!string.IsNullOrEmpty(this.m_ProductTemplet.m_UnlockReqStrID))
				{
					NKCUtil.SetGameobjectActive(this.m_lbLockedReason, true);
					if (this.m_ProductTemplet.m_UnlockReqStrID == "AUTO")
					{
						NKCUtil.SetLabelText(this.m_lbLockedReason, NKCContentManager.MakeUnlockConditionString(this.m_ProductTemplet.m_UnlockInfo, false));
					}
					else
					{
						NKCUtil.SetLabelText(this.m_lbLockedReason, NKCStringTable.GetString(this.m_ProductTemplet.m_UnlockReqStrID, false));
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lbLockedReason, false);
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objLocked, flag2 || flag3);
			}
			if (!flag)
			{
				NKCUtil.SetGameobjectActive(this.m_objLocked, true);
				NKCUtil.SetGameobjectActive(this.m_lbLockedReason, true);
				NKCUtil.SetGameobjectActive(this.m_objPriceRoot, false);
				NKCUtil.SetLabelText(this.m_lbLockedReason, NKCStringTable.GetString("SI_DP_SHOP_PRODUCT_PROHIBITED", false));
			}
			this.m_bTimerUpdate = (flag5 || flag6 || flag7 || flag2);
			this.SetShowBadgeTime(flag5);
			this.SetShowEventTime(flag6);
			this.SetShowLockedTime(flag2);
			this.SetShowTimeLeft(flag7);
			this.PostSetData(shopTemplet);
			return true;
		}

		// Token: 0x06007D0E RID: 32014 RVA: 0x0029E93C File Offset: 0x0029CB3C
		public bool SetData(NKCUIShop uiShop, NKMShopRandomListData shopRandomTemplet, int index)
		{
			this.m_uiShop = uiShop;
			this.m_ProductTemplet = null;
			this.ProductID = index;
			NKCUtil.SetGameobjectActive(this.m_imgRibbon, false);
			NKCUtil.SetGameobjectActive(this.m_objLocked, false);
			this.SetRedDot();
			switch (shopRandomTemplet.itemType)
			{
			case NKM_REWARD_TYPE.RT_NONE:
				Debug.LogError("RandomShopTemplet Type None! Index : " + index.ToString());
				return false;
			case NKM_REWARD_TYPE.RT_UNIT:
			case NKM_REWARD_TYPE.RT_SHIP:
			case NKM_REWARD_TYPE.RT_OPERATOR:
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(shopRandomTemplet.itemId);
				if (unitTempletBase == null)
				{
					Debug.LogError("UnitTemplet null! ID : " + shopRandomTemplet.itemId.ToString());
					return false;
				}
				NKCUtil.SetLabelText(this.m_lbName, unitTempletBase.GetUnitName());
				break;
			}
			case NKM_REWARD_TYPE.RT_MISC:
			case NKM_REWARD_TYPE.RT_MISSION_POINT:
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(shopRandomTemplet.itemId);
				if (itemMiscTempletByID == null)
				{
					Debug.LogError("itemTemplet null! ID : " + shopRandomTemplet.itemId.ToString());
					return false;
				}
				NKCUtil.SetLabelText(this.m_lbName, itemMiscTempletByID.GetItemName());
				break;
			}
			case NKM_REWARD_TYPE.RT_USER_EXP:
			{
				NKMItemMiscTemplet itemMiscTempletByRewardType = NKMItemManager.GetItemMiscTempletByRewardType(shopRandomTemplet.itemType);
				if (itemMiscTempletByRewardType == null)
				{
					Debug.LogError("itemTemplet null! ID : " + shopRandomTemplet.itemId.ToString());
					return false;
				}
				NKCUtil.SetLabelText(this.m_lbName, itemMiscTempletByRewardType.GetItemName());
				break;
			}
			case NKM_REWARD_TYPE.RT_EQUIP:
			{
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(shopRandomTemplet.itemId);
				if (equipTemplet == null)
				{
					Debug.LogError("equipTemplet null! ID : " + shopRandomTemplet.itemId.ToString());
					return false;
				}
				NKCUtil.SetLabelText(this.m_lbName, NKCUtilString.GetItemEquipNameWithTier(equipTemplet));
				break;
			}
			case NKM_REWARD_TYPE.RT_MOLD:
			{
				NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(shopRandomTemplet.itemId);
				if (itemMoldTempletByID == null)
				{
					Debug.LogError("MoldTemplet null! ID : " + shopRandomTemplet.itemId.ToString());
					return false;
				}
				NKCUtil.SetLabelText(this.m_lbName, itemMoldTempletByID.GetItemName());
				break;
			}
			case NKM_REWARD_TYPE.RT_SKIN:
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(shopRandomTemplet.itemId);
				if (skinTemplet == null)
				{
					Debug.LogError("SkinTemplet null! ID : " + shopRandomTemplet.itemId.ToString());
					return false;
				}
				NKCUtil.SetLabelText(this.m_lbName, skinTemplet.GetTitle());
				break;
			}
			}
			NKCUtil.SetGameobjectActive(this.m_lbBuyCount, false);
			NKCUtil.SetGameobjectActive(this.m_objPriceRoot, true);
			NKCUtil.SetGameobjectActive(this.m_objSoldOut, shopRandomTemplet.isBuy);
			this.SetPrice(shopRandomTemplet.priceItemId, shopRandomTemplet.GetPrice(), shopRandomTemplet.discountRatio != 0, shopRandomTemplet.price);
			NKCUtil.SetGameobjectActive(this.m_objDiscountRate, shopRandomTemplet.discountRatio > 0);
			float num = (float)(100 - shopRandomTemplet.discountRatio) / 10f;
			NKCUtil.SetLabelText(this.m_txtDiscountRate, NKCStringTable.GetString("SI_DP_SHOP_DISCOUNT_RATE", new object[]
			{
				shopRandomTemplet.discountRatio,
				num
			}));
			NKCUtil.SetGameobjectActive(this.m_lbRibbon, false);
			NKCUtil.SetGameobjectActive(this.m_imgRibbon, false);
			this.SetGoodsImage(shopRandomTemplet);
			this.m_bTimerUpdate = false;
			this.SetShowTimeLeft(false);
			this.SetShowEventTime(false);
			this.SetShowBadgeTime(false);
			this.SetShowLockedTime(false);
			return true;
		}

		// Token: 0x06007D0F RID: 32015 RVA: 0x0029EC50 File Offset: 0x0029CE50
		private void Update()
		{
			if (this.m_bTimerUpdate && this.m_ProductTemplet != null)
			{
				this.m_updateTimer += Time.deltaTime;
				if (1f < this.m_updateTimer)
				{
					this.m_updateTimer = 0f;
					if (this.m_tEndDateDiscountTime != DateTime.MinValue)
					{
						this.UpdateDiscountTime(this.m_tEndDateDiscountTime);
					}
					if (this.m_tEndDateEventTime != DateTime.MinValue)
					{
						this.UpdateEventTime(this.m_tEndDateEventTime);
					}
					if (this.m_tEndDateSubscriptionTime != DateTime.MinValue)
					{
						this.UpdateTimeLeft(this.m_tEndDateSubscriptionTime);
					}
					if (this.m_tEndDateLockedTime != DateTime.MinValue)
					{
						this.UpdateLockedTime(this.m_tEndDateLockedTime);
					}
				}
			}
		}

		// Token: 0x06007D10 RID: 32016 RVA: 0x0029ED18 File Offset: 0x0029CF18
		public void UpdateEventTime(DateTime eventEndTime)
		{
			string msg;
			if (NKCSynchronizedTime.IsFinished(eventEndTime))
			{
				msg = NKCUtilString.GET_STRING_QUIT;
				if (this.m_ProductTemplet != null)
				{
					NKCUIShopSlotBase.OnRefreshRequired onRefreshRequired = this.dOnRefreshRequired;
					if (onRefreshRequired == null)
					{
						return;
					}
					onRefreshRequired();
					return;
				}
			}
			else
			{
				msg = NKCUtilString.GetRemainTimeStringOneParam(eventEndTime);
			}
			NKCUtil.SetLabelText(this.m_txtEventTime, msg);
		}

		// Token: 0x06007D11 RID: 32017 RVA: 0x0029ED5F File Offset: 0x0029CF5F
		public void SetShowEventTime(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objEventTimeRoot, bValue);
		}

		// Token: 0x06007D12 RID: 32018 RVA: 0x0029ED70 File Offset: 0x0029CF70
		public void UpdateDiscountTime(DateTime endTime)
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

		// Token: 0x06007D13 RID: 32019 RVA: 0x0029EDA0 File Offset: 0x0029CFA0
		public void SetShowBadgeTime(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objDiscountDay, bValue);
		}

		// Token: 0x06007D14 RID: 32020 RVA: 0x0029EDB0 File Offset: 0x0029CFB0
		public void UpdateLockedTime(DateTime endTimeUTC)
		{
			string msg;
			if (NKCSynchronizedTime.IsFinished(endTimeUTC))
			{
				msg = NKCUtilString.GET_STRING_QUIT;
				if (this.m_uiShop != null)
				{
					this.m_uiShop.RefreshCurrentTab();
					this.m_uiShop.RefreshShopRedDot();
				}
			}
			else
			{
				msg = NKCSynchronizedTime.GetTimeLeftString(endTimeUTC);
			}
			NKCUtil.SetLabelText(this.m_lbLockedTime, msg);
		}

		// Token: 0x06007D15 RID: 32021 RVA: 0x0029EE04 File Offset: 0x0029D004
		public void SetShowLockedTime(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objLockedTime, bValue);
			NKCUtil.SetGameobjectActive(this.m_lbLockedTime, bValue);
		}

		// Token: 0x06007D16 RID: 32022 RVA: 0x0029EE20 File Offset: 0x0029D020
		public void SetRedDot()
		{
			if (this.m_ProductTemplet == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objRedDot, false);
				return;
			}
			if (this.m_ProductTemplet.m_QuantityLimit <= NKCScenManager.CurrentUserData().m_ShopData.GetPurchasedCount(this.m_ProductTemplet))
			{
				NKCUtil.SetGameobjectActive(this.m_objRedDot, false);
				return;
			}
			bool flag;
			long num;
			if (NKCShopManager.CanBuyFixShop(NKCScenManager.CurrentUserData(), this.m_ProductTemplet, out flag, out num, true) == NKM_ERROR_CODE.NEC_OK)
			{
				NKCUtil.SetShopReddotImage(NKCShopManager.GetReddotType(this.m_ProductTemplet), this.m_objRedDot, this.m_objReddot_RED, this.m_objReddot_YELLOW);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objRedDot, false);
		}

		// Token: 0x06007D17 RID: 32023 RVA: 0x0029EEB7 File Offset: 0x0029D0B7
		public void SetNameText(string name)
		{
			NKCUtil.SetLabelText(this.m_lbName, name);
		}

		// Token: 0x06007D18 RID: 32024 RVA: 0x0029EEC8 File Offset: 0x0029D0C8
		protected virtual void SetRibbon(ShopItemRibbon ribbonType)
		{
			NKCUtil.SetImageColor(this.m_imgRibbon, NKCShopManager.GetRibbonColor(ribbonType));
			NKCUtil.SetLabelText(this.m_lbRibbon, NKCShopManager.GetRibbonString(ribbonType));
			NKCUtil.SetGameobjectActive(this.m_lbRibbon, ribbonType > ShopItemRibbon.None);
			NKCUtil.SetGameobjectActive(this.m_imgRibbon, ribbonType > ShopItemRibbon.None);
		}

		// Token: 0x06007D19 RID: 32025 RVA: 0x0029EF15 File Offset: 0x0029D115
		protected Sprite GetPriceImage(int priceItemID)
		{
			return NKCResourceUtility.GetOrLoadMiscItemSmallIcon(priceItemID);
		}

		// Token: 0x040069C9 RID: 27081
		[Header("공통")]
		public Image m_imgRibbon;

		// Token: 0x040069CA RID: 27082
		public Text m_lbRibbon;

		// Token: 0x040069CB RID: 27083
		public Text m_lbName;

		// Token: 0x040069CC RID: 27084
		[Header("구매 관련")]
		public Text m_lbBuyCount;

		// Token: 0x040069CD RID: 27085
		public GameObject m_objSoldOut;

		// Token: 0x040069CE RID: 27086
		public NKCUIComButton m_cbtnBuy;

		// Token: 0x040069CF RID: 27087
		public GameObject m_objPriceRoot;

		// Token: 0x040069D0 RID: 27088
		public GameObject m_objRedDot;

		// Token: 0x040069D1 RID: 27089
		public GameObject m_objReddot_RED;

		// Token: 0x040069D2 RID: 27090
		public GameObject m_objReddot_YELLOW;

		// Token: 0x040069D3 RID: 27091
		[Header("할인 관련")]
		public GameObject m_objDiscountDay;

		// Token: 0x040069D4 RID: 27092
		public Text m_txtDiscountDay;

		// Token: 0x040069D5 RID: 27093
		public GameObject m_objDiscountRate;

		// Token: 0x040069D6 RID: 27094
		public Text m_txtDiscountRate;

		// Token: 0x040069D7 RID: 27095
		public GameObject m_objProfitRate;

		// Token: 0x040069D8 RID: 27096
		public Text m_lbProfitRate;

		// Token: 0x040069D9 RID: 27097
		[Header("판매 기간")]
		public GameObject m_objEventTimeRoot;

		// Token: 0x040069DA RID: 27098
		public Text m_txtEventTime;

		// Token: 0x040069DB RID: 27099
		[Header("잠김")]
		public GameObject m_objLocked;

		// Token: 0x040069DC RID: 27100
		public Text m_lbLockedReason;

		// Token: 0x040069DD RID: 27101
		public GameObject m_objLockedTime;

		// Token: 0x040069DE RID: 27102
		public Text m_lbLockedTime;

		// Token: 0x040069DF RID: 27103
		[Header("어드민 오브젝트")]
		public GameObject m_objAdmin;

		// Token: 0x040069E0 RID: 27104
		public NKCUIComStateButton m_csbtnAdminReset;

		// Token: 0x040069E2 RID: 27106
		private bool m_bTimerUpdate;

		// Token: 0x040069E3 RID: 27107
		private const float TIMER_UPDATE_INTERVAL = 1f;

		// Token: 0x040069E4 RID: 27108
		private float m_updateTimer;

		// Token: 0x040069E5 RID: 27109
		private ShopItemTemplet m_ProductTemplet;

		// Token: 0x040069E6 RID: 27110
		private DateTime m_tEndDateDiscountTime;

		// Token: 0x040069E7 RID: 27111
		private DateTime m_tEndDateEventTime;

		// Token: 0x040069E8 RID: 27112
		private DateTime m_tEndDateSubscriptionTime;

		// Token: 0x040069E9 RID: 27113
		private DateTime m_tEndDateLockedTime;

		// Token: 0x040069EA RID: 27114
		protected string m_OverrideImageAsset;

		// Token: 0x040069EB RID: 27115
		protected bool m_bUseCommonTimeText = true;

		// Token: 0x040069EC RID: 27116
		protected NKCUIShop m_uiShop;

		// Token: 0x040069ED RID: 27117
		protected const string SHOP_ICON_BUNDLE_NAME = "AB_UI_NKM_UI_SHOP_IMG";

		// Token: 0x040069EE RID: 27118
		private NKCUIShopSlotBase.OnBuy dOnBuy;

		// Token: 0x040069EF RID: 27119
		private NKCUIShopSlotBase.OnRefreshRequired dOnRefreshRequired;

		// Token: 0x02001859 RID: 6233
		private enum eSaleRibbonType
		{
			// Token: 0x0400A8AE RID: 43182
			None,
			// Token: 0x0400A8AF RID: 43183
			Sale10,
			// Token: 0x0400A8B0 RID: 43184
			Sale25,
			// Token: 0x0400A8B1 RID: 43185
			Sale50,
			// Token: 0x0400A8B2 RID: 43186
			Sale70
		}

		// Token: 0x0200185A RID: 6234
		// (Invoke) Token: 0x0600B5B3 RID: 46515
		public delegate void OnBuy(int shopItemID);

		// Token: 0x0200185B RID: 6235
		// (Invoke) Token: 0x0600B5B7 RID: 46519
		public delegate void OnRefreshRequired();
	}
}
