using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using ClientPacket.Shop;
using Cs.Logging;
using NKC.Publisher;
using NKC.Templet;
using NKC.UI;
using NKC.UI.Result;
using NKC.UI.Shop;
using NKM;
using NKM.Item;
using NKM.Shop;
using NKM.Templet;
using NKM.Templet.Base;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020006C9 RID: 1737
	public static class NKCShopManager
	{
		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06003C35 RID: 15413 RVA: 0x001349B9 File Offset: 0x00132BB9
		// (set) Token: 0x06003C36 RID: 15414 RVA: 0x001349C0 File Offset: 0x00132BC0
		public static List<int> ShopItemList { get; private set; }

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06003C37 RID: 15415 RVA: 0x001349C8 File Offset: 0x00132BC8
		// (set) Token: 0x06003C38 RID: 15416 RVA: 0x001349CF File Offset: 0x00132BCF
		public static Dictionary<int, InstantProduct> InstantProducts { get; private set; }

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06003C39 RID: 15417 RVA: 0x001349D7 File Offset: 0x00132BD7
		// (set) Token: 0x06003C3A RID: 15418 RVA: 0x001349DE File Offset: 0x00132BDE
		public static long ShopItemUpdatedTimestamp { get; private set; } = 0L;

		// Token: 0x06003C3B RID: 15419 RVA: 0x001349E8 File Offset: 0x00132BE8
		public static InstantProduct GetInstantProduct(int id)
		{
			InstantProduct result;
			if (NKCShopManager.InstantProducts != null && NKCShopManager.InstantProducts.TryGetValue(id, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06003C3C RID: 15420 RVA: 0x00134A0E File Offset: 0x00132C0E
		public static bool IsShopItemListReady
		{
			get
			{
				return NKCShopManager.ShopItemList != null && NKCPublisherModule.InAppPurchase.CheckReceivedBillingProductList;
			}
		}

		// Token: 0x06003C3D RID: 15421 RVA: 0x00134A24 File Offset: 0x00132C24
		public static void SetShopItemList(List<int> lstShopItem, List<InstantProduct> lstInstantProduct)
		{
			NKCShopManager.ShopItemList = lstShopItem;
			NKCShopManager.InstantProducts = new Dictionary<int, InstantProduct>();
			foreach (InstantProduct instantProduct in lstInstantProduct)
			{
				NKCShopManager.InstantProducts.Add(instantProduct.productId, instantProduct);
			}
			NKCShopManager.ShopItemUpdatedTimestamp = NKCSynchronizedTime.GetServerUTCTime(0.0).Ticks;
		}

		// Token: 0x06003C3E RID: 15422 RVA: 0x00134AA8 File Offset: 0x00132CA8
		public static void InvalidateShopItemList()
		{
			NKCShopManager.ShopItemList = null;
		}

		// Token: 0x06003C3F RID: 15423 RVA: 0x00134AB0 File Offset: 0x00132CB0
		public static void RequestShopItemList(NKC_OPEN_WAIT_BOX_TYPE waitBoxType, bool bForceRefreshServerItemList = false)
		{
			if (bForceRefreshServerItemList || NKCShopManager.ShopItemList == null)
			{
				NKCShopManager.ShopItemList = null;
				NKCPacketSender.Send_NKMPacket_SHOP_FIXED_LIST_REQ(waitBoxType);
			}
			if (!NKCPublisherModule.InAppPurchase.CheckReceivedBillingProductList)
			{
				NKCPublisherModule.InAppPurchase.RequestBillingProductList(null);
			}
		}

		// Token: 0x06003C40 RID: 15424 RVA: 0x00134ADF File Offset: 0x00132CDF
		public static void SetReserveRefreshShop()
		{
			NKCShopManager.m_bReserveForceRefreshShop = true;
		}

		// Token: 0x06003C41 RID: 15425 RVA: 0x00134AE8 File Offset: 0x00132CE8
		public static void FetchShopItemList(NKC_OPEN_WAIT_BOX_TYPE waitBoxType, NKCShopManager.OnWaitComplete onWaitComplete, bool bForceRefreshServerItemList = false)
		{
			if (NKCShopManager.m_bReserveForceRefreshShop)
			{
				bForceRefreshServerItemList = true;
			}
			if (!bForceRefreshServerItemList && NKCShopManager.IsShopItemListReady && NKCEmoticonManager.m_bReceivedEmoticonData)
			{
				Debug.Log("Skip fecth shop item list...");
				if (onWaitComplete != null)
				{
					onWaitComplete(true);
				}
				return;
			}
			NKCScenManager.GetScenManager().StartCoroutine(NKCShopManager.WaitForShopItemList(waitBoxType, onWaitComplete, bForceRefreshServerItemList));
			NKCShopManager.m_bReserveForceRefreshShop = false;
		}

		// Token: 0x06003C42 RID: 15426 RVA: 0x00134B3E File Offset: 0x00132D3E
		private static IEnumerator WaitForShopItemList(NKC_OPEN_WAIT_BOX_TYPE waitBoxType, NKCShopManager.OnWaitComplete onWaitComplete, bool bForceRefreshServerItemList = false)
		{
			Debug.Log("Fecthing shop item list...");
			float waitTime = 0f;
			if (!NKCShopManager.IsShopItemListReady || bForceRefreshServerItemList)
			{
				NKCShopManager.RequestShopItemList(waitBoxType, bForceRefreshServerItemList);
			}
			if (!NKCEmoticonManager.m_bReceivedEmoticonData || bForceRefreshServerItemList)
			{
				NKCPacketSender.Send_NKMPacket_EMOTICON_DATA_REQ(waitBoxType);
			}
			while (!NKCShopManager.IsShopItemListReady || !NKCEmoticonManager.m_bReceivedEmoticonData)
			{
				if (!NKMPopUpBox.IsOpenedWaitBox())
				{
					NKMPopUpBox.OpenWaitBox(waitBoxType, 0f, "", null);
				}
				waitTime += Time.unscaledDeltaTime;
				if (waitTime > 5f)
				{
					NKMPopUpBox.CloseWaitBox();
					if (onWaitComplete != null)
					{
						onWaitComplete(false);
					}
					yield break;
				}
				yield return null;
			}
			NKCShopManager.m_lstSpecialItemTemplet = NKCShopManager.GetLimitedCountSpecialItems();
			NKMPopUpBox.CloseWaitBox();
			if (onWaitComplete != null)
			{
				onWaitComplete(true);
			}
			yield break;
		}

		// Token: 0x06003C43 RID: 15427 RVA: 0x00134B5C File Offset: 0x00132D5C
		public static NKM_ERROR_CODE CanBuyFixShop(NKMUserData user_data, ShopItemTemplet shop_templet, out bool is_init, out long next_reset_date, bool bCheckChainIndex = true)
		{
			is_init = false;
			next_reset_date = 0L;
			if (user_data == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_USER_DATA_NULL;
			}
			if (shop_templet.IsNewbieProduct && !user_data.IsNewbieUser(shop_templet.m_NewbieDate))
			{
				return NKM_ERROR_CODE.NKE_FAIL_SHOP_NOT_EVENT_TIME;
			}
			if (!NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), shop_templet.m_UnlockInfo, false))
			{
				return NKM_ERROR_CODE.NEC_FAIL_INVALID_LIMITED_ITEM_UNLOCK;
			}
			if (!shop_templet.IsReturningProduct)
			{
				if (!NKCSynchronizedTime.IsEventTime(shop_templet.eventIntervalId, shop_templet.EventDateStartUtc, shop_templet.EventDateEndUtc))
				{
					return NKM_ERROR_CODE.NKE_FAIL_SHOP_NOT_EVENT_TIME;
				}
			}
			else
			{
				if (!user_data.IsReturnUser())
				{
					return NKM_ERROR_CODE.NKE_FAIL_SHOP_NOT_EVENT_TIME;
				}
				if (!NKCSynchronizedTime.IsEventTime(user_data.GetReturnStartDate(shop_templet.m_ReturningUserType), shop_templet.eventIntervalId, shop_templet.EventDateStartUtc, shop_templet.EventDateEndUtc))
				{
					return NKM_ERROR_CODE.NKE_FAIL_SHOP_NOT_EVENT_TIME;
				}
			}
			ShopTabTemplet shopTabTemplet = ShopTabTemplet.Find(shop_templet.m_TabID, shop_templet.m_TabSubIndex);
			if (shopTabTemplet != null)
			{
				if (!NKCSynchronizedTime.IsEventTime(shopTabTemplet.EventDateStartUtc, shopTabTemplet.EventDateEndUtc))
				{
					return NKM_ERROR_CODE.NKE_FAIL_SHOP_NOT_EVENT_TIME;
				}
				if (bCheckChainIndex && shop_templet.m_ChainIndex > 0 && NKCShopManager.GetCurrentTargetChainIndex(shopTabTemplet) != shop_templet.m_ChainIndex)
				{
					return NKM_ERROR_CODE.NKE_FAIL_SHOP_INVALID_CHAIN_TAB;
				}
			}
			if (NKCShopManager.WillOverflowOnGain(shop_templet.m_ItemType, shop_templet.m_ItemID, shop_templet.TotalValue))
			{
				NKM_REWARD_TYPE itemType = shop_templet.m_ItemType;
				if (itemType != NKM_REWARD_TYPE.RT_MISC)
				{
					if (itemType == NKM_REWARD_TYPE.RT_SKIN)
					{
						return NKM_ERROR_CODE.NKE_FAIL_SHOP_SKIN_ALREADY_OWNED;
					}
				}
				else
				{
					NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(shop_templet.m_ItemID);
					if (itemMiscTempletByID == null)
					{
						return NKM_ERROR_CODE.NEC_FAIL_INVALID_ITEM_ID;
					}
					switch (itemMiscTempletByID.m_ItemMiscType)
					{
					case NKM_ITEM_MISC_TYPE.IMT_BACKGROUND:
						return NKM_ERROR_CODE.NEC_FAIL_SHOP_BACKGROUND_ALREADY_OWNED;
					case NKM_ITEM_MISC_TYPE.IMT_SELFIE_FRAME:
						return NKM_ERROR_CODE.NEC_FAIL_SHOP_FRAME_ALREADY_OWNED;
					case NKM_ITEM_MISC_TYPE.IMT_INTERIOR:
					{
						NKMOfficeInteriorTemplet nkmofficeInteriorTemplet = NKMOfficeInteriorTemplet.Find(shop_templet.m_ItemID);
						if (nkmofficeInteriorTemplet != null)
						{
							InteriorCategory interiorCategory = nkmofficeInteriorTemplet.InteriorCategory;
							if (interiorCategory == InteriorCategory.DECO)
							{
								return NKM_ERROR_CODE.NEC_FAIL_SHOP_DECORATION_ALREADY_OWNED;
							}
							if (interiorCategory == InteriorCategory.FURNITURE)
							{
								return NKM_ERROR_CODE.NEC_FAIL_SHOP_FURNITURE_OWNED_MAX;
							}
						}
						break;
					}
					}
				}
				return NKM_ERROR_CODE.NEC_FAIL_SHOP_ALREADY_OWNED;
			}
			if (shop_templet.NeedHistory && shop_templet.resetType != SHOP_RESET_TYPE.Unlimited)
			{
				NKMShopPurchaseHistory nkmshopPurchaseHistory;
				if (!user_data.m_ShopData.histories.TryGetValue(shop_templet.m_ProductID, out nkmshopPurchaseHistory))
				{
					is_init = true;
					next_reset_date = NKCShopManager.GetNextResetDate(shop_templet.resetType);
				}
				else
				{
					next_reset_date = nkmshopPurchaseHistory.nextResetDate;
					if (shop_templet.IsCountResetType() && NKCSynchronizedTime.IsFinished(nkmshopPurchaseHistory.nextResetDate))
					{
						is_init = true;
						next_reset_date = NKCShopManager.GetNextResetDate(shop_templet.resetType);
					}
					int num = nkmshopPurchaseHistory.purchaseCount;
					if (is_init)
					{
						num = 0;
					}
					if (shop_templet.m_QuantityLimit <= num)
					{
						return NKM_ERROR_CODE.NEC_FAIL_LIMITED_SHOP_COUNT_FAIL;
					}
				}
				if (shop_templet.m_paidAmountRequired > 0.0 && NKCScenManager.CurrentUserData().m_ShopData.GetTotalPayment() < shop_templet.m_paidAmountRequired)
				{
					return NKM_ERROR_CODE.NEC_FAIL_SHOP_NOT_ENOUGH_PAID_AMOUNT;
				}
			}
			if (!shop_templet.m_bEnabled)
			{
				return NKM_ERROR_CODE.NKE_FAIL_SHOP_NOT_EVENT_TIME;
			}
			if (shop_templet.m_PriceItemID == 0)
			{
				if (string.IsNullOrEmpty(shop_templet.m_MarketID))
				{
					return NKM_ERROR_CODE.NEC_FAIL_INVALID_SHOP_ID;
				}
				if (!NKCPublisherModule.InAppPurchase.IsRegisteredProduct(shop_templet.m_MarketID, shop_templet.m_ProductID))
				{
					return NKM_ERROR_CODE.NEC_FAIL_INVALID_SHOP_ID;
				}
			}
			NKMConsumerPackageData nkmconsumerPackageData;
			if (shop_templet.m_PurchaseEventType == PURCHASE_EVENT_REWARD_TYPE.CONSUMER_PACKAGE && NKCScenManager.CurrentUserData().GetConsumerPackageData(shop_templet.m_ProductID, out nkmconsumerPackageData))
			{
				return NKM_ERROR_CODE.NEC_FAIL_CONSUMER_PACKAGE_ALREADY_PURCHASED;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06003C44 RID: 15428 RVA: 0x00134E34 File Offset: 0x00133034
		public static ShopItemTemplet GetShopTempletBySkinID(int skinID)
		{
			foreach (ShopItemTemplet shopItemTemplet in NKMTempletContainer<ShopItemTemplet>.Values)
			{
				if (shopItemTemplet.m_bEnabled && shopItemTemplet.m_ItemType == NKM_REWARD_TYPE.RT_SKIN && shopItemTemplet.m_ItemID == skinID)
				{
					return shopItemTemplet;
				}
			}
			return null;
		}

		// Token: 0x06003C45 RID: 15429 RVA: 0x00134E9C File Offset: 0x0013309C
		public static bool IsPackageItem(int shopID)
		{
			ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(shopID);
			if (shopItemTemplet == null)
			{
				Log.Error("ShopTemplet not found. ID : " + shopID.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCShopManager.cs", 365);
				return false;
			}
			if (shopItemTemplet.m_ItemType != NKM_REWARD_TYPE.RT_MISC)
			{
				return false;
			}
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(shopItemTemplet.m_ItemID);
			if (itemMiscTempletByID == null)
			{
				Log.Error(string.Format("ItemTemplet {0} from ShopTemplet {1} not found.", shopItemTemplet.m_ItemID, shopID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCShopManager.cs", 377);
				return false;
			}
			return itemMiscTempletByID.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_PACKAGE;
		}

		// Token: 0x06003C46 RID: 15430 RVA: 0x00134F24 File Offset: 0x00133124
		public static bool IsCustomPackageItem(int shopID)
		{
			ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(shopID);
			if (shopItemTemplet == null)
			{
				Log.Error("ShopTemplet not found. ID : " + shopID.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCShopManager.cs", 390);
				return false;
			}
			if (shopItemTemplet.m_ItemType != NKM_REWARD_TYPE.RT_MISC)
			{
				return false;
			}
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(shopItemTemplet.m_ItemID);
			if (itemMiscTempletByID == null)
			{
				Log.Error(string.Format("ItemTemplet {0} from ShopTemplet {1} not found.", shopItemTemplet.m_ItemID, shopID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCShopManager.cs", 402);
				return false;
			}
			return itemMiscTempletByID.IsCustomPackageItem;
		}

		// Token: 0x06003C47 RID: 15431 RVA: 0x00134FAC File Offset: 0x001331AC
		public static ShopItemTemplet GetShopTempletByMarketID(string marketID)
		{
			if (string.IsNullOrEmpty(marketID))
			{
				return null;
			}
			foreach (ShopItemTemplet shopItemTemplet in NKMTempletContainer<ShopItemTemplet>.Values)
			{
				if (string.Equals(shopItemTemplet.m_MarketID, marketID))
				{
					return shopItemTemplet;
				}
			}
			return null;
		}

		// Token: 0x06003C48 RID: 15432 RVA: 0x00135010 File Offset: 0x00133210
		public static ShopItemTemplet GetShopTempletByProductID(int productID)
		{
			foreach (ShopItemTemplet shopItemTemplet in NKMTempletContainer<ShopItemTemplet>.Values)
			{
				if (shopItemTemplet.m_ProductID == productID)
				{
					return shopItemTemplet;
				}
			}
			return null;
		}

		// Token: 0x06003C49 RID: 15433 RVA: 0x00135068 File Offset: 0x00133268
		public static long GetNextResetDate(SHOP_RESET_TYPE limit_cond)
		{
			DateTime serverUTCTime = NKCSynchronizedTime.GetServerUTCTime(0.0);
			switch (limit_cond)
			{
			case SHOP_RESET_TYPE.DAY:
				return NKMTime.GetNextResetTime(serverUTCTime, NKMTime.TimePeriod.Day).Ticks;
			case SHOP_RESET_TYPE.WEEK:
			case SHOP_RESET_TYPE.WEEK_SUN:
			case SHOP_RESET_TYPE.WEEK_MON:
			case SHOP_RESET_TYPE.WEEK_TUE:
			case SHOP_RESET_TYPE.WEEK_WED:
			case SHOP_RESET_TYPE.WEEK_THU:
			case SHOP_RESET_TYPE.WEEK_FRI:
			case SHOP_RESET_TYPE.WEEK_SAT:
				return NKMTime.GetNextResetTime(serverUTCTime, NKMTime.TimePeriod.Week).Ticks;
			case SHOP_RESET_TYPE.MONTH:
				return NKMTime.GetNextResetTime(serverUTCTime, NKMTime.TimePeriod.Month).Ticks;
			case SHOP_RESET_TYPE.FIXED:
				return serverUTCTime.AddYears(100).Ticks;
			default:
				return 0L;
			}
		}

		// Token: 0x06003C4A RID: 15434 RVA: 0x00135100 File Offset: 0x00133300
		public static int GetBundleItemPrice(ShopTabTemplet tabTemplet)
		{
			int num = 0;
			if (tabTemplet != null)
			{
				NKMUserData user_data = NKCScenManager.CurrentUserData();
				if (tabTemplet.IsBundleTab)
				{
					for (int i = 0; i < tabTemplet.Goods.Count; i++)
					{
						bool flag;
						long finishTimeUTCTicks;
						if (NKCShopManager.CanBuyFixShop(user_data, tabTemplet.Goods[i], out flag, out finishTimeUTCTicks, true) == NKM_ERROR_CODE.NEC_OK || NKCSynchronizedTime.IsFinished(finishTimeUTCTicks))
						{
							num += tabTemplet.Goods[i].m_Price * NKCShopManager.GetBuyCountLeft(tabTemplet.Goods[i].m_ProductID);
						}
					}
				}
				else
				{
					Log.Error(string.Format("Bundle 타입이 아님 - {0}", tabTemplet.m_ShopDisplay), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCShopManager.cs", 483);
				}
			}
			return num;
		}

		// Token: 0x06003C4B RID: 15435 RVA: 0x001351B0 File Offset: 0x001333B0
		public static int GetBundleItemPriceItemID(ShopTabTemplet tabTemplet)
		{
			int num = -1;
			if (tabTemplet.IsBundleTab)
			{
				for (int i = 0; i < tabTemplet.Goods.Count; i++)
				{
					if (num < 0)
					{
						num = tabTemplet.Goods[i].m_PriceItemID;
					}
					if (num >= 0 && num != tabTemplet.Goods[i].m_PriceItemID)
					{
						Log.Error("Bundle 탭의 소모재화 종류가 다름", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCShopManager.cs", 502);
						return num;
					}
				}
			}
			else
			{
				Log.Error(string.Format("Bundle 타입이 아님 - {0}", tabTemplet.m_ShopDisplay), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCShopManager.cs", 509);
			}
			return num;
		}

		// Token: 0x06003C4C RID: 15436 RVA: 0x00135248 File Offset: 0x00133448
		public static int GetCurrentTargetChainIndex(ShopTabTemplet tabTemplet)
		{
			NKMUserData user_data = NKCScenManager.CurrentUserData();
			NKMShopData shopData = NKCScenManager.CurrentUserData().m_ShopData;
			int num = 0;
			if (tabTemplet != null)
			{
				int num2 = 1;
				while (num2 <= 3 && tabTemplet.GetChainGoods(num2) != null)
				{
					num = num2;
					foreach (ShopItemTemplet shopItemTemplet in tabTemplet.GetChainGoods(num2))
					{
						ShopItemTemplet shopItemTemplet2 = ShopItemTemplet.Find(shopItemTemplet.m_ProductID);
						bool flag;
						long num3;
						if (shopItemTemplet2 != null && NKCShopManager.CanBuyFixShop(user_data, shopItemTemplet2, out flag, out num3, false) == NKM_ERROR_CODE.NEC_OK)
						{
							if (!shopData.histories.ContainsKey(shopItemTemplet.m_ProductID))
							{
								return num2;
							}
							if (shopData.histories[shopItemTemplet.m_ProductID].purchaseCount < shopItemTemplet2.m_QuantityLimit)
							{
								return num2;
							}
							if (shopData.histories[shopItemTemplet.m_ProductID].nextResetDate < NKCSynchronizedTime.GetServerUTCTime(0.0).Ticks)
							{
								return num2;
							}
						}
					}
					num2++;
				}
				if (num == 0)
				{
					Log.Debug(string.Format("Invalid ChianGoods - tabID : {0}, subIndex : {1}", tabTemplet.TabType, tabTemplet.SubIndex), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCShopManager.cs", 551);
				}
			}
			return num;
		}

		// Token: 0x06003C4D RID: 15437 RVA: 0x001353A4 File Offset: 0x001335A4
		public static List<NKCShopBannerTemplet> GetHomeBannerTemplet()
		{
			List<NKCShopBannerTemplet> list = new List<NKCShopBannerTemplet>();
			foreach (NKCShopBannerTemplet nkcshopBannerTemplet in NKMTempletContainer<NKCShopBannerTemplet>.Values)
			{
				if (NKCSynchronizedTime.IsEventTime(NKCSynchronizedTime.GetServerUTCTime(0.0), nkcshopBannerTemplet.m_DateStrID) && nkcshopBannerTemplet.m_Enable && nkcshopBannerTemplet.EnableByTag && NKCShopManager.CheckRecommendCond(nkcshopBannerTemplet.m_DisplayCond, nkcshopBannerTemplet.m_DisplayCondValue))
				{
					if (nkcshopBannerTemplet.m_ProductID > 0)
					{
						ShopItemTemplet shop_templet = ShopItemTemplet.Find(nkcshopBannerTemplet.m_ProductID);
						bool flag;
						long num;
						if (NKCShopManager.CanBuyFixShop(NKCScenManager.CurrentUserData(), shop_templet, out flag, out num, true) != NKM_ERROR_CODE.NEC_OK)
						{
							continue;
						}
					}
					list.Add(nkcshopBannerTemplet);
				}
			}
			return list;
		}

		// Token: 0x06003C4E RID: 15438 RVA: 0x0013545C File Offset: 0x0013365C
		public static List<ShopItemTemplet> GetItemTempletListByTab(ShopTabTemplet tabTemplet, bool bIncludeLockedItemWithReason = false)
		{
			List<ShopItemTemplet> list = new List<ShopItemTemplet>();
			if (tabTemplet == null)
			{
				return list;
			}
			for (int i = 0; i < tabTemplet.Goods.Count; i++)
			{
				if (NKCShopManager.CanExhibitItem(tabTemplet.Goods[i], bIncludeLockedItemWithReason, false))
				{
					list.Add(tabTemplet.Goods[i]);
				}
			}
			return list;
		}

		// Token: 0x06003C4F RID: 15439 RVA: 0x001354B4 File Offset: 0x001336B4
		public static bool IsTabSoldOut(ShopTabTemplet tabTemplet)
		{
			new List<ShopItemTemplet>();
			if (tabTemplet == null)
			{
				return true;
			}
			for (int i = 0; i < tabTemplet.Goods.Count; i++)
			{
				if (NKCShopManager.CanExhibitItem(tabTemplet.Goods[i], true, false) && NKCShopManager.GetBuyCountLeft(tabTemplet.Goods[i].m_ProductID) != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003C50 RID: 15440 RVA: 0x00135514 File Offset: 0x00133714
		public static bool CanExhibitItem(ShopItemTemplet shopItemTemplet, bool bIncludeLockedItemWithReason = false, bool bIgnoreContentUnlock = false)
		{
			bool flag;
			return shopItemTemplet != null && shopItemTemplet.m_bVisible && shopItemTemplet.EnableByTag && shopItemTemplet.ItemEnableByTag && NKCShopManager.IsProductAvailable(shopItemTemplet, out flag, bIncludeLockedItemWithReason, bIgnoreContentUnlock);
		}

		// Token: 0x06003C51 RID: 15441 RVA: 0x00135553 File Offset: 0x00133753
		private static int CompareByLimitShowIndex(ShopItemTemplet left, ShopItemTemplet right)
		{
			return right.m_LimitShowIndex.CompareTo(left.m_LimitShowIndex);
		}

		// Token: 0x06003C52 RID: 15442 RVA: 0x00135568 File Offset: 0x00133768
		public static bool IsFirstBuy(int ProductID, NKMUserData userData)
		{
			NKMShopPurchaseHistory nkmshopPurchaseHistory;
			return userData == null || !userData.m_ShopData.histories.TryGetValue(ProductID, out nkmshopPurchaseHistory) || nkmshopPurchaseHistory.purchaseCount == 0;
		}

		// Token: 0x06003C53 RID: 15443 RVA: 0x00135598 File Offset: 0x00133798
		public static bool IsProductAvailable(ShopItemTemplet shopTemplet, out bool bAdmin, bool bIncludeLockedItemWithReason = false, bool bIgnoreContentUnlock = false)
		{
			bAdmin = false;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return false;
			}
			if (shopTemplet == null)
			{
				Log.Error("ShopTemplet null!", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCShopManager.cs", 708);
				return false;
			}
			if (!shopTemplet.IsReturningProduct)
			{
				if (shopTemplet.HasDateLimit && !NKCSynchronizedTime.IsEventTime(shopTemplet.eventIntervalId))
				{
					return false;
				}
			}
			else if (nkmuserData.IsReturnUser())
			{
				if (!NKCSynchronizedTime.IsEventTime(nkmuserData.GetReturnStartDate(shopTemplet.m_ReturningUserType), shopTemplet.eventIntervalId, shopTemplet.EventDateStartUtc, shopTemplet.EventDateEndUtc))
				{
					if (!NKCShopManager.<IsProductAvailable>g__IsAdmin|42_0(nkmuserData))
					{
						return false;
					}
					bAdmin = true;
				}
			}
			else
			{
				if (!NKCShopManager.<IsProductAvailable>g__IsAdmin|42_0(nkmuserData))
				{
					return false;
				}
				bAdmin = true;
			}
			if (shopTemplet.IsNewbieProduct && !nkmuserData.IsNewbieUser(shopTemplet.m_NewbieDate))
			{
				return false;
			}
			DayOfWeek dayOfWeek;
			if (shopTemplet.resetType.ToDayOfWeek(out dayOfWeek) && NKCSynchronizedTime.ServiceTime.DayOfWeek != dayOfWeek)
			{
				return false;
			}
			if (!bIgnoreContentUnlock)
			{
				bool flag2;
				bool flag = NKMContentUnlockManager.IsContentUnlocked(nkmuserData, shopTemplet.m_UnlockInfo, out flag2);
				bAdmin = (bAdmin || flag2);
				if (!flag && (!bIncludeLockedItemWithReason || string.IsNullOrEmpty(shopTemplet.m_UnlockReqStrID)))
				{
					return false;
				}
				if (shopTemplet.IsInstantProduct)
				{
					InstantProduct instantProduct = NKCShopManager.GetInstantProduct(shopTemplet.m_ProductID);
					if (instantProduct != null)
					{
						if (NKCSynchronizedTime.IsFinished(NKMTime.LocalToUTC(instantProduct.endDate, 0)))
						{
							return false;
						}
					}
					else if (flag)
					{
						return false;
					}
				}
			}
			if (!shopTemplet.m_bEnabled)
			{
				return false;
			}
			if (shopTemplet.m_PriceItemID == 0)
			{
				if (string.IsNullOrEmpty(shopTemplet.m_MarketID))
				{
					return false;
				}
				if (!NKCPublisherModule.InAppPurchase.IsRegisteredProduct(shopTemplet.m_MarketID, shopTemplet.m_ProductID))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003C54 RID: 15444 RVA: 0x0013570D File Offset: 0x0013390D
		public static int GetDailyMissionTicketShopID(int episodeID)
		{
			switch (episodeID)
			{
			case 101:
				return 40121;
			case 102:
				return 40123;
			case 103:
				return 40122;
			default:
				return 0;
			}
		}

		// Token: 0x06003C55 RID: 15445 RVA: 0x00135739 File Offset: 0x00133939
		public static int GetBundleCount()
		{
			return NKCShopManager.m_totalBundleCount;
		}

		// Token: 0x06003C56 RID: 15446 RVA: 0x00135740 File Offset: 0x00133940
		public static void SetBundleItemIds(HashSet<int> lstBundleItemIds)
		{
			NKCShopManager.m_totalBundleCount = lstBundleItemIds.Count;
			NKCShopManager.m_lstBundleItemIds = lstBundleItemIds;
			NKCShopManager.m_lstBundleItemReward = new List<NKMRewardData>();
		}

		// Token: 0x06003C57 RID: 15447 RVA: 0x00135760 File Offset: 0x00133960
		public static void RemoveBundleItemId(int bundleItemId, NKMRewardData rewardData)
		{
			if (NKCShopManager.m_lstBundleItemIds.Contains(bundleItemId))
			{
				NKCShopManager.m_lstBundleItemIds.Remove(bundleItemId);
				NKCShopManager.m_lstBundleItemReward.Add(rewardData);
			}
			if (NKCShopManager.m_lstBundleItemIds.Count == 0)
			{
				NKCUIResult.Instance.OpenBoxGain(NKCScenManager.CurrentUserData().m_ArmyData, NKCShopManager.m_lstBundleItemReward, NKCUtilString.GET_STRING_SHOP_BUY_ALL_TITLE, null);
				NKCShopManager.m_totalBundleCount = 0;
			}
		}

		// Token: 0x06003C58 RID: 15448 RVA: 0x001357C4 File Offset: 0x001339C4
		public static List<ShopItemTemplet> GetLockedProductList()
		{
			List<ShopItemTemplet> list = new List<ShopItemTemplet>();
			foreach (ShopItemTemplet shopItemTemplet in NKMTempletContainer<ShopItemTemplet>.Values)
			{
				if (shopItemTemplet.m_bEnabled && shopItemTemplet.m_UnlockInfo.eReqType != STAGE_UNLOCK_REQ_TYPE.SURT_ALWAYS_UNLOCKED && !NKMContentUnlockManager.IsContentUnlocked(NKCScenManager.CurrentUserData(), shopItemTemplet.m_UnlockInfo, false))
				{
					list.Add(shopItemTemplet);
				}
			}
			return list;
		}

		// Token: 0x06003C59 RID: 15449 RVA: 0x00135844 File Offset: 0x00133A44
		public static List<ShopItemTemplet> GetLimitedCountSpecialItems()
		{
			List<ShopItemTemplet> list = new List<ShopItemTemplet>();
			NKMShopData shopData = NKCScenManager.CurrentUserData().m_ShopData;
			foreach (ShopItemTemplet shopItemTemplet in NKMTempletContainer<ShopItemTemplet>.Values)
			{
				if (!shopItemTemplet.HasDateLimit || !NKCSynchronizedTime.IsFinished(shopItemTemplet.EventDateEndUtc))
				{
					if (shopItemTemplet.m_TagImage == ShopItemRibbon.SPECIAL && shopItemTemplet.resetType != SHOP_RESET_TYPE.Unlimited)
					{
						list.Add(shopItemTemplet);
					}
					else if (shopData.GetRealPrice(shopItemTemplet, 1, false) == 0)
					{
						list.Add(shopItemTemplet);
					}
					else if (NKCShopManager.GetReddotType(shopItemTemplet) != ShopReddotType.NONE)
					{
						list.Add(shopItemTemplet);
					}
				}
			}
			return list;
		}

		// Token: 0x06003C5A RID: 15450 RVA: 0x001358F0 File Offset: 0x00133AF0
		public static Dictionary<int, string> GetMarketProductList()
		{
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			foreach (ShopItemTemplet shopItemTemplet in NKMTempletContainer<ShopItemTemplet>.Values)
			{
				if (!string.IsNullOrEmpty(shopItemTemplet.m_MarketID) && shopItemTemplet.m_bEnabled && !dictionary.ContainsKey(shopItemTemplet.m_ProductID))
				{
					dictionary.Add(shopItemTemplet.m_ProductID, shopItemTemplet.m_MarketID);
				}
			}
			return dictionary;
		}

		// Token: 0x06003C5B RID: 15451 RVA: 0x00135974 File Offset: 0x00133B74
		public static Dictionary<int, string> GetMarketAllProductList()
		{
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			foreach (ShopItemTemplet shopItemTemplet in NKMTempletContainer<ShopItemTemplet>.Values)
			{
				if (!string.IsNullOrEmpty(shopItemTemplet.m_MarketID) && !dictionary.ContainsKey(shopItemTemplet.m_ProductID))
				{
					dictionary.Add(shopItemTemplet.m_ProductID, shopItemTemplet.m_MarketID);
				}
			}
			return dictionary;
		}

		// Token: 0x06003C5C RID: 15452 RVA: 0x001359F0 File Offset: 0x00133BF0
		public static bool IsMoveToShopDefined(int itemID)
		{
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(itemID);
			if (itemMiscTempletByID == null)
			{
				return false;
			}
			if (itemMiscTempletByID.m_lstRecommandProductItemIfNotEnough != null && itemMiscTempletByID.m_lstRecommandProductItemIfNotEnough.Count > 0)
			{
				return true;
			}
			bool categoryFromTab = NKCShopManager.GetCategoryFromTab(itemMiscTempletByID.m_ShortCutShopTabID) != null;
			ShopTabTemplet shopTabTemplet = ShopTabTemplet.Find(itemMiscTempletByID.m_ShortCutShopTabID, itemMiscTempletByID.m_ShortCutShopIndex);
			return categoryFromTab && shopTabTemplet != null;
		}

		// Token: 0x06003C5D RID: 15453 RVA: 0x00135A48 File Offset: 0x00133C48
		public static bool CanUsePopupShopBuy(int itemID)
		{
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(itemID);
			if (itemMiscTempletByID == null)
			{
				return false;
			}
			if (itemMiscTempletByID.m_lstRecommandProductItemIfNotEnough == null || itemMiscTempletByID.m_lstRecommandProductItemIfNotEnough.Count == 0)
			{
				return false;
			}
			for (int i = 0; i < itemMiscTempletByID.m_lstRecommandProductItemIfNotEnough.Count; i++)
			{
				if (NKCShopManager.CanExhibitItem(ShopItemTemplet.Find(itemMiscTempletByID.m_lstRecommandProductItemIfNotEnough[i]), false, false))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003C5E RID: 15454 RVA: 0x00135AAC File Offset: 0x00133CAC
		public static TabId GetShopMoveTab(int itemID)
		{
			TabId result = default(TabId);
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(itemID);
			if (itemMiscTempletByID != null)
			{
				result = new TabId(itemMiscTempletByID.m_ShortCutShopTabID, itemMiscTempletByID.m_ShortCutShopIndex);
			}
			return result;
		}

		// Token: 0x06003C5F RID: 15455 RVA: 0x00135AE0 File Offset: 0x00133CE0
		public static bool CheckRecommendCond(SHOP_RECOMMEND_COND cond, string value)
		{
			NKMUserData user_data = NKCScenManager.CurrentUserData();
			if (cond != SHOP_RECOMMEND_COND.NONE && cond - SHOP_RECOMMEND_COND.PURCHASE_ABLE <= 1)
			{
				string[] array = value.Split(new char[]
				{
					',',
					' '
				});
				for (int i = 0; i < array.Length; i++)
				{
					int num;
					if (int.TryParse(array[i], out num) && num > 0)
					{
						ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(num);
						bool flag;
						bool flag2;
						long num2;
						if (NKCShopManager.IsProductAvailable(shopItemTemplet, out flag, false, false) && NKCShopManager.CanBuyFixShop(user_data, shopItemTemplet, out flag2, out num2, true) == NKM_ERROR_CODE.NEC_OK)
						{
							return true;
						}
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x06003C60 RID: 15456 RVA: 0x00135B5C File Offset: 0x00133D5C
		public static void OnInappPurchase(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			Debug.Log(string.Format("[InappPurchase] OnInappPurchase ResultCode[{0}] Additional[{1}]", resultCode, additionalError));
			if (resultCode <= NKC_PUBLISHER_RESULT_CODE.NPRC_INAPP_FAIL_NOT_SUPPORTED)
			{
				if (resultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_OK)
				{
					return;
				}
				if (resultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_INAPP_FAIL_NOT_SUPPORTED)
				{
					NKCPopupMessageManager.AddPopupMessage(resultCode, additionalError, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
			}
			else if (resultCode != NKC_PUBLISHER_RESULT_CODE.NPRC_INAPP_FAIL_NO_INAPP_PRODUCT)
			{
				switch (resultCode)
				{
				case NKC_PUBLISHER_RESULT_CODE.NPRC_INAPP_FAIL:
				case NKC_PUBLISHER_RESULT_CODE.NPRC_INAPP_FAIL_TRANSACTION_ERROR:
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_TOY_BILLING_PAYMENT_FAIL, NKCPublisherModule.LastError, null, "");
					return;
				case NKC_PUBLISHER_RESULT_CODE.NPRC_INAPP_RESTORE_NEEDED_ITEM_VENDOR_NOT_CONSUMED:
					NKCPopupOKCancel.OpenOKBox(NKC_PUBLISHER_RESULT_CODE.NPRC_INAPP_RESTORE_NEEDED_ITEM_VENDOR_NOT_CONSUMED, additionalError, delegate()
					{
						NKCPublisherModule.InAppPurchase.BillingRestore(new NKCPublisherModule.OnComplete(NKCShopManager.OnBillingRestore));
					}, "");
					return;
				case NKC_PUBLISHER_RESULT_CODE.NPRC_INAPP_FAIL_USER_CANCEL:
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCPublisherModule.GetErrorMessage(resultCode, null), null, "");
					return;
				}
			}
			NKCPopupOKCancel.OpenOKBox(resultCode, additionalError, null, "");
		}

		// Token: 0x06003C61 RID: 15457 RVA: 0x00135C41 File Offset: 0x00133E41
		public static void OnBillingRestore(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			if (resultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_INAPP_NOT_EXIST_RESTORE_ITEM)
			{
				NKMPopUpBox.CloseWaitBox();
				return;
			}
			NKCPublisherModule.CheckError(resultCode, additionalError, true, null, true);
		}

		// Token: 0x06003C62 RID: 15458 RVA: 0x00135C5C File Offset: 0x00133E5C
		public static bool IsAllCustomSlotSelected(NKMItemMiscTemplet customItemTemplet, List<int> lstSelection)
		{
			if (customItemTemplet == null || !customItemTemplet.IsCustomPackageItem)
			{
				return false;
			}
			if (lstSelection == null)
			{
				return false;
			}
			if (lstSelection.Count != customItemTemplet.CustomPackageTemplets.Count)
			{
				return false;
			}
			for (int i = 0; i < customItemTemplet.CustomPackageTemplets.Count; i++)
			{
				int index = lstSelection[i];
				if (customItemTemplet.CustomPackageTemplets[i].Get(index) == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003C63 RID: 15459 RVA: 0x00135CC8 File Offset: 0x00133EC8
		public static bool IsCustomPackageSelectionHasDuplicate(NKMItemMiscTemplet customItemTemplet, int targetIndex, List<int> lstSelection, bool bIgnoreIfFirstItem)
		{
			if (customItemTemplet == null || !customItemTemplet.IsCustomPackageItem)
			{
				return false;
			}
			if (lstSelection == null)
			{
				return false;
			}
			if (targetIndex >= customItemTemplet.CustomPackageTemplets.Count)
			{
				return false;
			}
			if (targetIndex < 0)
			{
				return false;
			}
			NKMCustomPackageElement nkmcustomPackageElement = customItemTemplet.CustomPackageTemplets[targetIndex].Get(lstSelection[targetIndex]);
			if (!NKMItemManager.IsRedudantItemProhibited(nkmcustomPackageElement.RewardType, nkmcustomPackageElement.RewardId))
			{
				return false;
			}
			for (int i = 0; i < lstSelection.Count; i++)
			{
				if (i == targetIndex)
				{
					if (bIgnoreIfFirstItem)
					{
						return false;
					}
				}
				else
				{
					int index = lstSelection[i];
					NKMCustomPackageElement nkmcustomPackageElement2 = customItemTemplet.CustomPackageTemplets[i].Get(index);
					if (nkmcustomPackageElement2 != null && nkmcustomPackageElement2.RewardType == nkmcustomPackageElement.RewardType && nkmcustomPackageElement2.RewardId == nkmcustomPackageElement.RewardId)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003C64 RID: 15460 RVA: 0x00135D84 File Offset: 0x00133F84
		public static NKMRewardInfo GetSubstituteItem(NKM_REWARD_TYPE rewardType, int ID, int overCount)
		{
			if (rewardType != NKM_REWARD_TYPE.RT_MISC)
			{
				if (rewardType == NKM_REWARD_TYPE.RT_SKIN)
				{
					NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(ID);
					if (skinTemplet != null)
					{
						return new NKMRewardInfo
						{
							ID = skinTemplet.m_ReturnItemId,
							Count = skinTemplet.m_ReturnItemCount * overCount,
							rewardType = NKM_REWARD_TYPE.RT_MISC,
							paymentType = NKM_ITEM_PAYMENT_TYPE.NIPT_FREE
						};
					}
				}
			}
			else
			{
				NKMItemMiscTemplet nkmitemMiscTemplet = NKMItemMiscTemplet.Find(ID);
				if (nkmitemMiscTemplet != null && nkmitemMiscTemplet.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_INTERIOR)
				{
					NKMOfficeInteriorTemplet nkmofficeInteriorTemplet = NKMOfficeInteriorTemplet.Find(ID);
					if (nkmofficeInteriorTemplet != null)
					{
						return new NKMRewardInfo
						{
							ID = nkmofficeInteriorTemplet.RefundItem.m_ItemMiscID,
							Count = (int)nkmofficeInteriorTemplet.RefundItemPrice * overCount,
							rewardType = NKM_REWARD_TYPE.RT_MISC,
							paymentType = NKM_ITEM_PAYMENT_TYPE.NIPT_FREE
						};
					}
				}
			}
			return null;
		}

		// Token: 0x06003C65 RID: 15461 RVA: 0x00135E2C File Offset: 0x0013402C
		public static int GetItemOverCount(NKM_REWARD_TYPE rewardType, int itemID, int gainCount)
		{
			if (rewardType != NKM_REWARD_TYPE.RT_MISC)
			{
				if (rewardType != NKM_REWARD_TYPE.RT_SKIN)
				{
					if (rewardType == NKM_REWARD_TYPE.RT_EMOTICON)
					{
						if (!NKCEmoticonManager.HasEmoticon(itemID))
						{
							return gainCount - 1;
						}
						return gainCount;
					}
				}
				else
				{
					if (!NKCScenManager.CurrentUserData().m_InventoryData.HasItemSkin(itemID))
					{
						return gainCount - 1;
					}
					return gainCount;
				}
			}
			else
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(itemID);
				if (itemMiscTempletByID == null)
				{
					return 0;
				}
				if (itemMiscTempletByID.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_INTERIOR)
				{
					NKMOfficeInteriorTemplet nkmofficeInteriorTemplet = NKMOfficeInteriorTemplet.Find(itemID);
					int num = (int)NKCScenManager.CurrentUserData().OfficeData.GetInteriorCount(itemID);
					int maxStack = nkmofficeInteriorTemplet.MaxStack;
					return gainCount + num - maxStack;
				}
				if (NKMItemManager.IsRedudantItemProhibited(itemMiscTempletByID.m_ItemMiscType, itemMiscTempletByID.m_ItemMiscSubType))
				{
					return (int)(NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(itemID) + (long)gainCount - 1L);
				}
			}
			return 0;
		}

		// Token: 0x06003C66 RID: 15462 RVA: 0x00135EDA File Offset: 0x001340DA
		public static bool WillOverflowOnGain(NKM_REWARD_TYPE rewardType, int itemID, int gainCount)
		{
			return NKCShopManager.GetItemOverCount(rewardType, itemID, gainCount) > 0;
		}

		// Token: 0x06003C67 RID: 15463 RVA: 0x00135EE8 File Offset: 0x001340E8
		public static bool IsHaveUnit(NKM_REWARD_TYPE rewardType, int itemID)
		{
			if (rewardType == NKM_REWARD_TYPE.RT_UNIT)
			{
				return NKCScenManager.CurrentUserData().m_ArmyData.HaveUnit(itemID, false);
			}
			if (rewardType != NKM_REWARD_TYPE.RT_SHIP)
			{
				return rewardType == NKM_REWARD_TYPE.RT_OPERATOR && NKCScenManager.CurrentUserData().m_ArmyData.GetOperatorCountByID(itemID) > 0;
			}
			return NKCScenManager.CurrentUserData().m_ArmyData.GetSameKindShipCountFromID(itemID) > 0;
		}

		// Token: 0x06003C68 RID: 15464 RVA: 0x00135F40 File Offset: 0x00134140
		public static List<NKCShopManager.ShopRewardSubstituteData> MakeShopBuySubstituteItemList(ShopItemTemplet shopItemTemplet, int buyCount, List<int> lstSelection)
		{
			if (shopItemTemplet == null)
			{
				return null;
			}
			List<NKCShopManager.ShopRewardSubstituteData> list = new List<NKCShopManager.ShopRewardSubstituteData>();
			NKMItemMiscTemplet nkmitemMiscTemplet = null;
			if (shopItemTemplet.m_ItemType == NKM_REWARD_TYPE.RT_MISC)
			{
				nkmitemMiscTemplet = NKMItemManager.GetItemMiscTempletByID(shopItemTemplet.m_ItemID);
			}
			if (nkmitemMiscTemplet != null && (nkmitemMiscTemplet.IsCustomPackageItem || nkmitemMiscTemplet.IsPackageItem))
			{
				List<NKMRandomBoxItemTemplet> randomBoxItemTempletList = NKCRandomBoxManager.GetRandomBoxItemTempletList(nkmitemMiscTemplet.m_RewardGroupID);
				if (nkmitemMiscTemplet.m_RewardGroupID != 0 && randomBoxItemTempletList == null)
				{
					Log.Error("rewardgroup null! ID : " + nkmitemMiscTemplet.m_RewardGroupID.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCShopManager.cs", 1373);
				}
				if (randomBoxItemTempletList != null)
				{
					for (int i = 0; i < randomBoxItemTempletList.Count; i++)
					{
						NKCShopManager.ShopRewardSubstituteData item;
						if (NKCShopManager.MakeSubstituteItem(randomBoxItemTempletList[i], buyCount, out item))
						{
							list.Add(item);
						}
					}
				}
				if (nkmitemMiscTemplet.CustomPackageTemplets != null && lstSelection != null)
				{
					for (int j = 0; j < nkmitemMiscTemplet.CustomPackageTemplets.Count; j++)
					{
						int index = lstSelection[j];
						NKMCustomPackageElement nkmcustomPackageElement = nkmitemMiscTemplet.CustomPackageTemplets[j].Get(index);
						if (nkmcustomPackageElement != null)
						{
							int itemOverCount = NKCShopManager.GetItemOverCount(nkmcustomPackageElement.RewardType, nkmcustomPackageElement.RewardId, nkmcustomPackageElement.TotalRewardCount);
							if (itemOverCount > 0 || NKCShopManager.IsCustomPackageSelectionHasDuplicate(nkmitemMiscTemplet, j, lstSelection, true))
							{
								NKMRewardInfo substituteItem = NKCShopManager.GetSubstituteItem(nkmcustomPackageElement.RewardType, nkmcustomPackageElement.RewardId, itemOverCount);
								if (substituteItem != null)
								{
									NKMRewardInfo before = new NKMRewardInfo
									{
										rewardType = nkmcustomPackageElement.RewardType,
										ID = nkmcustomPackageElement.RewardId,
										Count = itemOverCount
									};
									list.Add(new NKCShopManager.ShopRewardSubstituteData
									{
										Before = before,
										After = substituteItem
									});
								}
							}
						}
					}
				}
				return list;
			}
			int itemOverCount2 = NKCShopManager.GetItemOverCount(shopItemTemplet.m_ItemType, shopItemTemplet.m_ItemID, buyCount);
			if (itemOverCount2 <= 0)
			{
				return null;
			}
			if (itemOverCount2 == buyCount)
			{
				return null;
			}
			NKMRewardInfo substituteItem2 = NKCShopManager.GetSubstituteItem(shopItemTemplet.m_ItemType, shopItemTemplet.m_ItemID, itemOverCount2);
			if (substituteItem2 == null)
			{
				return null;
			}
			NKMRewardInfo before2 = new NKMRewardInfo
			{
				rewardType = shopItemTemplet.m_ItemType,
				ID = shopItemTemplet.m_ItemID,
				Count = itemOverCount2
			};
			list.Add(new NKCShopManager.ShopRewardSubstituteData
			{
				Before = before2,
				After = substituteItem2
			});
			return list;
		}

		// Token: 0x06003C69 RID: 15465 RVA: 0x00136164 File Offset: 0x00134364
		public static bool MakeSubstituteItem(NKMRandomBoxItemTemplet boxItemTemplet, int count, out NKCShopManager.ShopRewardSubstituteData data)
		{
			int itemOverCount = NKCShopManager.GetItemOverCount(boxItemTemplet.m_reward_type, boxItemTemplet.m_RewardID, boxItemTemplet.TotalQuantity_Max * count);
			if (itemOverCount <= 0)
			{
				data = default(NKCShopManager.ShopRewardSubstituteData);
				return false;
			}
			NKMRewardInfo substituteItem = NKCShopManager.GetSubstituteItem(boxItemTemplet.m_reward_type, boxItemTemplet.m_RewardID, itemOverCount);
			if (substituteItem == null)
			{
				data = default(NKCShopManager.ShopRewardSubstituteData);
				return false;
			}
			NKMRewardInfo before = new NKMRewardInfo
			{
				rewardType = boxItemTemplet.m_reward_type,
				ID = boxItemTemplet.m_RewardID,
				Count = itemOverCount
			};
			data = new NKCShopManager.ShopRewardSubstituteData
			{
				Before = before,
				After = substituteItem
			};
			return true;
		}

		// Token: 0x06003C6A RID: 15466 RVA: 0x001361FC File Offset: 0x001343FC
		public static string EncodeCustomPackageSelectList(List<int> lstSelection)
		{
			if (lstSelection == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (int value in lstSelection)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(',');
			}
			return stringBuilder.ToString().TrimEnd(new char[]
			{
				','
			});
		}

		// Token: 0x06003C6B RID: 15467 RVA: 0x00136278 File Offset: 0x00134478
		public static List<int> DecodeCustomPackageSelectList(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}
			string[] array = value.Split(new char[]
			{
				',',
				' '
			});
			List<int> list = new List<int>();
			for (int i = 0; i < array.Length; i++)
			{
				int item;
				if (!int.TryParse(array[i], out item))
				{
					Log.Error("DecodeCustomPackageSelectList : Bad input!", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCShopManager.cs", 1503);
					return null;
				}
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06003C6C RID: 15468 RVA: 0x001362E8 File Offset: 0x001344E8
		public static NKM_ERROR_CODE OnBtnProductBuy(int ProductID, bool bSupply)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (bSupply)
			{
				NKMShopRandomData randomShop = myUserData.m_ShopData.randomShop;
				if (!randomShop.datas.ContainsKey(ProductID))
				{
					Log.Error("invalid index " + ProductID.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCShopManager.cs", 1524);
					return NKM_ERROR_CODE.NEC_FAIL_INVALID_SHOP_ID;
				}
				NKMShopRandomListData nkmshopRandomListData = randomShop.datas[ProductID];
				if (nkmshopRandomListData.isBuy)
				{
					return NKM_ERROR_CODE.NEC_FAIL_LIMITED_SHOP_COUNT_FAIL;
				}
				NKCPopupItemBox.Instance.Open(nkmshopRandomListData, false, delegate()
				{
					NKCShopManager.TrySupplyProductBuy(ProductID);
				});
			}
			else
			{
				ShopItemTemplet productTemplet = ShopItemTemplet.Find(ProductID);
				if (!myUserData.IsSuperUser())
				{
					bool flag;
					long num;
					NKM_ERROR_CODE nkm_ERROR_CODE = NKCShopManager.CanBuyFixShop(myUserData, productTemplet, out flag, out num, true);
					if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
					{
						return nkm_ERROR_CODE;
					}
				}
				if (productTemplet.IsSubscribeItem() || productTemplet.m_PurchaseEventType == PURCHASE_EVENT_REWARD_TYPE.LEVELUP_PACKAGE)
				{
					NKCPopupShopBuyConfirm.Instance.Open(productTemplet, new NKCUIShop.OnProductBuyDelegate(NKCShopManager.TryProductBuy));
					NKCShopManager.SetLastCheckedUTCTime(productTemplet);
					return NKM_ERROR_CODE.NEC_OK;
				}
				NKM_REWARD_TYPE itemType = productTemplet.m_ItemType;
				if (itemType != NKM_REWARD_TYPE.RT_MISC)
				{
					if (itemType != NKM_REWARD_TYPE.RT_SKIN)
					{
						if (itemType == NKM_REWARD_TYPE.RT_EMOTICON)
						{
							NKCPopupItemBox.Instance.Open(productTemplet, delegate()
							{
								NKCPopupShopBuyConfirm.Instance.Open(productTemplet, new NKCUIShop.OnProductBuyDelegate(NKCShopManager.TryProductBuy));
							});
						}
						else
						{
							NKCPopupShopBuyConfirm.Instance.Open(productTemplet, new NKCUIShop.OnProductBuyDelegate(NKCShopManager.TryProductBuy));
						}
					}
					else
					{
						NKCUIShopSkinPopup.Instance.OpenForShop(productTemplet);
					}
				}
				else
				{
					NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(productTemplet.m_ItemID);
					if (itemMiscTempletByID != null)
					{
						if (itemMiscTempletByID.IsPackageItem)
						{
							NKCPopupShopPackageConfirm.Instance.Open(productTemplet, new NKCUIShop.OnProductBuyDelegate(NKCShopManager.TryProductBuy), null);
						}
						else if (itemMiscTempletByID.IsCustomPackageItem)
						{
							NKCPopupShopCustomPackage.Instance.Open(productTemplet, new NKCUIShop.OnProductBuyDelegate(NKCShopManager.TryProductBuy));
						}
						else
						{
							NKCPopupShopBuyConfirm.Instance.Open(productTemplet, new NKCUIShop.OnProductBuyDelegate(NKCShopManager.TryProductBuy));
						}
					}
					else
					{
						Log.Error(string.Format("NKMItemMiscTemplet is null - {0}", productTemplet.m_ItemID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCShopManager.cs", 1592);
					}
				}
				NKCShopManager.SetLastCheckedUTCTime(productTemplet);
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06003C6D RID: 15469 RVA: 0x00136558 File Offset: 0x00134758
		public static void TryProductBuy(int ProductID, int ProductCount = 1, List<int> lstSelection = null)
		{
			ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(ProductID);
			if (NKCShopManager.GetBuyCountLeft(ProductID) == 0)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString("SI_PF_SHOP_SOLD_OUT", false), null, "");
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (!myUserData.HaveEnoughResourceToBuy(shopItemTemplet, ProductCount))
			{
				NKCShopManager.OpenItemLackPopup(shopItemTemplet.m_PriceItemID, myUserData.m_ShopData.GetRealPrice(shopItemTemplet, 1, false) * ProductCount);
				return;
			}
			if (shopItemTemplet.m_PurchaseEventType == PURCHASE_EVENT_REWARD_TYPE.CONSUMER_PACKAGE)
			{
				NKMConsumerPackageData nkmconsumerPackageData;
				if (NKCScenManager.CurrentUserData().GetConsumerPackageData(ProductID, out nkmconsumerPackageData))
				{
					NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_CONSUMER_PACKAGE_ALREADY_PURCHASED, null, "");
					return;
				}
				if (NKCSynchronizedTime.IsFinished(shopItemTemplet.EventDateEndUtc.AddDays(-5.0)))
				{
					NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString("SI_DP_SHOP_PURCHASE_WARNING", new object[]
					{
						NKCUtilString.GetRemainTimeString(shopItemTemplet.EventDateEndUtc, 1)
					}), delegate()
					{
						NKCShopManager.BuyProductInternal(ProductID, ProductCount, lstSelection);
					}, null, false);
					return;
				}
			}
			if (shopItemTemplet.m_ItemType == NKM_REWARD_TYPE.RT_SKIN)
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(shopItemTemplet.m_ItemID);
				if (skinTemplet == null)
				{
					NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_INVALID_SKIN_ITEM_ID, null, "");
					return;
				}
				if (!NKCScenManager.CurrentUserData().m_ArmyData.HaveUnit(skinTemplet.m_SkinEquipUnitID, true))
				{
					NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString("SI_PF_SHOP_SKIN_NO_UNIT_NOTICE", false), delegate()
					{
						NKCShopManager.BuyProductInternal(ProductID, ProductCount, lstSelection);
					}, null, false);
					return;
				}
			}
			NKCShopManager.BuyProductInternal(ProductID, ProductCount, lstSelection);
		}

		// Token: 0x06003C6E RID: 15470 RVA: 0x001366F4 File Offset: 0x001348F4
		private static void BuyProductInternal(int ProductID, int ProductCount = 1, List<int> lstSelection = null)
		{
			ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(ProductID);
			if (shopItemTemplet.m_PriceItemID == 0)
			{
				NKCPacketSender.Send_NKMPacket_SHOP_FIX_SHOP_CASH_BUY_POSSIBLE_REQ(shopItemTemplet.m_MarketID, lstSelection);
				return;
			}
			NKCPacketSender.Send_NKMPacket_SHOP_FIX_SHOP_BUY_REQ(ProductID, ProductCount, lstSelection);
		}

		// Token: 0x06003C6F RID: 15471 RVA: 0x00136728 File Offset: 0x00134928
		private static void TrySupplyProductBuy(int index)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMShopRandomListData nkmshopRandomListData = myUserData.m_ShopData.randomShop.datas[index];
			int price = nkmshopRandomListData.GetPrice();
			if (myUserData.CheckPrice(price, nkmshopRandomListData.priceItemId))
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_SHOP().Send_NKMPacket_SHOP_RANDOM_SHOP_BUY_REQ(index);
				return;
			}
			NKCShopManager.OpenItemLackPopup(nkmshopRandomListData.priceItemId, nkmshopRandomListData.GetPrice());
		}

		// Token: 0x06003C70 RID: 15472 RVA: 0x0013678D File Offset: 0x0013498D
		private static void LoadFeaturedTemplet()
		{
			if (!NKCShopManager.bFeaturedTempletLoaded)
			{
				NKCShopManager.bFeaturedTempletLoaded = true;
				NKCShopFeaturedTemplet.Load();
			}
		}

		// Token: 0x06003C71 RID: 15473 RVA: 0x001367A4 File Offset: 0x001349A4
		public static List<NKCShopFeaturedTemplet> GetFeaturedList(NKMUserData userData, string packageGroupID, bool bUseExhibitCount)
		{
			if (!NKCShopManager.bFeaturedTempletLoaded)
			{
				NKCShopManager.LoadFeaturedTemplet();
			}
			List<NKCShopFeaturedTemplet> list = new List<NKCShopFeaturedTemplet>();
			foreach (NKCShopFeaturedTemplet nkcshopFeaturedTemplet in NKMTempletContainer<NKCShopFeaturedTemplet>.Values)
			{
				if (packageGroupID.Equals(nkcshopFeaturedTemplet.m_PackageGroupID, StringComparison.InvariantCultureIgnoreCase) && nkcshopFeaturedTemplet.CheckCondition(userData))
				{
					ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(nkcshopFeaturedTemplet.m_PackageID);
					if (shopItemTemplet == null)
					{
						Log.Error(string.Format("피쳐드 템플릿에 지정된 상품 {0}이 존재하지 않음", nkcshopFeaturedTemplet.m_PackageID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCShopManager.cs", 1737);
					}
					else if ((!nkcshopFeaturedTemplet.m_ReddotRequired || (shopItemTemplet.m_Reddot != ShopReddotType.NONE && shopItemTemplet.m_hsReddotAllow.Contains(NKMContentsVersionManager.GetCountryTag()))) && NKCShopManager.CanExhibitItem(shopItemTemplet, false, false) && NKCShopManager.GetBuyCountLeft(shopItemTemplet.m_ProductID) != 0)
					{
						list.Add(nkcshopFeaturedTemplet);
					}
				}
			}
			list.Sort(new Comparison<NKCShopFeaturedTemplet>(NKCShopManager.CompareByReddotDescending));
			if (userData.m_ShopData.GetTotalPayment() >= NKMCommonConst.FeaturedListTotalPaymentThreshold)
			{
				list.Sort(new Comparison<NKCShopFeaturedTemplet>(NKCShopFeaturedTemplet.CompareHighPriceFirst));
			}
			else
			{
				list.Sort(new Comparison<NKCShopFeaturedTemplet>(NKCShopFeaturedTemplet.CompareLowPriceFirst));
			}
			if (bUseExhibitCount && list.Count > NKMCommonConst.FeaturedListExhibitCount)
			{
				list.RemoveRange(NKMCommonConst.FeaturedListExhibitCount, list.Count - NKMCommonConst.FeaturedListExhibitCount);
			}
			return list;
		}

		// Token: 0x06003C72 RID: 15474 RVA: 0x00136900 File Offset: 0x00134B00
		private static int CompareByReddotDescending(NKCShopFeaturedTemplet lhs, NKCShopFeaturedTemplet rhs)
		{
			ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(lhs.m_PackageID);
			return NKCShopManager.GetReddotType(ShopItemTemplet.Find(rhs.m_PackageID)).CompareTo(NKCShopManager.GetReddotType(shopItemTemplet));
		}

		// Token: 0x06003C73 RID: 15475 RVA: 0x00136942 File Offset: 0x00134B42
		public static void Drop()
		{
			NKCShopManager.bLevelupPackageTempletLoaded = false;
		}

		// Token: 0x06003C74 RID: 15476 RVA: 0x0013694C File Offset: 0x00134B4C
		public static void LoadLevelupPackageTemplet()
		{
			if (!NKCShopManager.bLevelupPackageTempletLoaded)
			{
				NKMTempletContainer<ShopLevelUpPackageGroupTemplet>.Load(from e in NKMTempletLoader<ShopLevelUpPackageGroupData>.LoadGroup("AB_SCRIPT", "LUA_LEVELUP_PACKAGE_TEMPLET", "LEVELUP_PACKAGE_TEMPLET", new Func<NKMLua, ShopLevelUpPackageGroupData>(ShopLevelUpPackageGroupData.LoadFromLUA))
				select new ShopLevelUpPackageGroupTemplet(e.Key, e.Value), null);
				NKCShopManager.bLevelupPackageTempletLoaded = true;
			}
		}

		// Token: 0x06003C75 RID: 15477 RVA: 0x001369B0 File Offset: 0x00134BB0
		public static ShopLevelUpPackageGroupTemplet GetLevelUpPackageGroupTemplet(int key)
		{
			if (!NKCShopManager.bLevelupPackageTempletLoaded)
			{
				NKCShopManager.LoadLevelupPackageTemplet();
			}
			return NKMTempletContainer<ShopLevelUpPackageGroupTemplet>.Find(key);
		}

		// Token: 0x06003C76 RID: 15478 RVA: 0x001369C4 File Offset: 0x00134BC4
		public static int GetBuyCountLeft(int ProductID)
		{
			ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(ProductID);
			if (shopItemTemplet == null)
			{
				return 0;
			}
			NKM_REWARD_TYPE itemType = shopItemTemplet.m_ItemType;
			if (itemType != NKM_REWARD_TYPE.RT_MISC)
			{
				if (itemType != NKM_REWARD_TYPE.RT_SKIN)
				{
					if (itemType == NKM_REWARD_TYPE.RT_EMOTICON)
					{
						if (NKCEmoticonManager.HasEmoticon(shopItemTemplet.m_ItemID))
						{
							return 0;
						}
					}
				}
				else if (NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.HasItemSkin(shopItemTemplet.m_ItemID))
				{
					return 0;
				}
			}
			else
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(shopItemTemplet.m_ItemID);
				if (itemMiscTempletByID == null)
				{
					return 0;
				}
				if (NKMItemManager.IsRedudantItemProhibited(itemMiscTempletByID.m_ItemMiscType, itemMiscTempletByID.m_ItemMiscSubType) && NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(shopItemTemplet.m_ItemID) > 0L)
				{
					return 0;
				}
			}
			if (shopItemTemplet.TabTemplet != null)
			{
				if (shopItemTemplet.TabTemplet.IsChainTab)
				{
					if (shopItemTemplet.resetType == SHOP_RESET_TYPE.Unlimited && shopItemTemplet.m_QuantityLimit <= 0)
					{
						return -1;
					}
				}
				else if (shopItemTemplet.resetType == SHOP_RESET_TYPE.Unlimited)
				{
					return -1;
				}
			}
			NKMShopPurchaseHistory nkmshopPurchaseHistory;
			if (!NKCScenManager.GetScenManager().GetMyUserData().m_ShopData.histories.TryGetValue(ProductID, out nkmshopPurchaseHistory))
			{
				int a = int.MaxValue;
				if (shopItemTemplet.m_ItemType == NKM_REWARD_TYPE.RT_MISC)
				{
					NKMItemMiscTemplet itemMiscTempletByID2 = NKMItemManager.GetItemMiscTempletByID(shopItemTemplet.m_ItemID);
					if (itemMiscTempletByID2.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_INTERIOR)
					{
						NKMOfficeInteriorTemplet nkmofficeInteriorTemplet = NKMOfficeInteriorTemplet.Find(itemMiscTempletByID2.Key);
						if (nkmofficeInteriorTemplet != null)
						{
							int num = (int)NKCScenManager.CurrentUserData().OfficeData.GetInteriorCount(nkmofficeInteriorTemplet);
							a = nkmofficeInteriorTemplet.MaxStack - num;
						}
					}
				}
				return Mathf.Min(a, shopItemTemplet.m_QuantityLimit);
			}
			if (shopItemTemplet.IsCountResetType() && NKCSynchronizedTime.IsFinished(nkmshopPurchaseHistory.nextResetDate))
			{
				return shopItemTemplet.m_QuantityLimit;
			}
			return shopItemTemplet.m_QuantityLimit - nkmshopPurchaseHistory.purchaseCount;
		}

		// Token: 0x06003C77 RID: 15479 RVA: 0x00136B44 File Offset: 0x00134D44
		public static string GetBuyCountString(SHOP_RESET_TYPE type, int currentCount, int maxCount, bool bRemoveBracket = false)
		{
			string text = string.Empty;
			switch (type)
			{
			case SHOP_RESET_TYPE.Unlimited:
				return "";
			case SHOP_RESET_TYPE.DAY:
				text = string.Format(NKCUtilString.GET_STRING_SHOP_DAY_PURCHASE_COUNT_TWO_PARAM, currentCount, maxCount);
				break;
			case SHOP_RESET_TYPE.WEEK:
			case SHOP_RESET_TYPE.WEEK_SUN:
			case SHOP_RESET_TYPE.WEEK_MON:
			case SHOP_RESET_TYPE.WEEK_TUE:
			case SHOP_RESET_TYPE.WEEK_WED:
			case SHOP_RESET_TYPE.WEEK_THU:
			case SHOP_RESET_TYPE.WEEK_FRI:
			case SHOP_RESET_TYPE.WEEK_SAT:
				text = string.Format(NKCUtilString.GET_STRING_SHOP_WEEK_PURCHASE_COUNT_TWO_PARAM, currentCount, maxCount);
				break;
			case SHOP_RESET_TYPE.MONTH:
				text = string.Format(NKCUtilString.GET_STRING_SHOP_MONTH_PURCHASE_COUNT_TWO_PARAM, currentCount, maxCount);
				break;
			case SHOP_RESET_TYPE.FIXED:
				text = string.Format(NKCUtilString.GET_STRING_SHOP_ACCOUNT_PURCHASE_COUNT_TWO_PARAM, currentCount, maxCount);
				break;
			default:
				text = string.Format(NKCUtilString.GET_STRING_SHOP_PURCHASE_COUNT_TWO_PARAM, currentCount, maxCount);
				break;
			}
			if (bRemoveBracket)
			{
				text = text.Substring(1, text.Length - 2);
			}
			return text;
		}

		// Token: 0x06003C78 RID: 15480 RVA: 0x00136C28 File Offset: 0x00134E28
		public static void OpenItemLackPopup(int itemID, int itemCnt)
		{
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(itemID);
			bool flag = false;
			if (itemMiscTempletByID != null && itemMiscTempletByID.m_lstRecommandProductItemIfNotEnough != null && itemMiscTempletByID.m_lstRecommandProductItemIfNotEnough.Count > 0)
			{
				foreach (int num in itemMiscTempletByID.m_lstRecommandProductItemIfNotEnough)
				{
					if (NKCShopManager.CanExhibitItem(ShopItemTemplet.Find(num), false, false) && NKCShopManager.GetBuyCountLeft(num) != 0)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				NKCPopupItemLack.Instance.OpenItemMiscLackPopup(itemID, itemCnt);
				return;
			}
			if (itemMiscTempletByID.m_lstRecommandProductItemIfNotEnough.Count == 1)
			{
				NKCShopManager.OnBtnProductBuy(itemMiscTempletByID.m_lstRecommandProductItemIfNotEnough[0], false);
				return;
			}
			NKCPopupShopBuyShortcut.Open(itemMiscTempletByID);
		}

		// Token: 0x06003C79 RID: 15481 RVA: 0x00136CEC File Offset: 0x00134EEC
		public static List<ShopItemTemplet> GetLinkedItem(int productID)
		{
			List<ShopItemTemplet> result;
			if (NKCShopManager.s_dicLinkedItemCache.TryGetValue(productID, out result))
			{
				return result;
			}
			List<ShopItemTemplet> list = new List<ShopItemTemplet>();
			foreach (ShopItemTemplet shopItemTemplet in NKMTempletContainer<ShopItemTemplet>.Values)
			{
				if (shopItemTemplet.m_UnlockInfo.eReqType == STAGE_UNLOCK_REQ_TYPE.SURT_SHOP_BUY_ITEM_ALL && shopItemTemplet.m_UnlockInfo.reqValue == productID && NKCShopManager.CanExhibitItem(shopItemTemplet, false, true))
				{
					list.Add(shopItemTemplet);
				}
			}
			if (list.Count == 0)
			{
				NKCShopManager.s_dicLinkedItemCache[productID] = null;
				return null;
			}
			NKCShopManager.s_dicLinkedItemCache[productID] = list;
			return list;
		}

		// Token: 0x06003C7A RID: 15482 RVA: 0x00136D9C File Offset: 0x00134F9C
		public static bool HasLinkedItem(int productID)
		{
			List<ShopItemTemplet> linkedItem = NKCShopManager.GetLinkedItem(productID);
			return linkedItem != null && linkedItem.Count > 0;
		}

		// Token: 0x06003C7B RID: 15483 RVA: 0x00136DBE File Offset: 0x00134FBE
		public static void ClearLinkedItemCache()
		{
			NKCShopManager.s_dicLinkedItemCache.Clear();
		}

		// Token: 0x06003C7C RID: 15484 RVA: 0x00136DCC File Offset: 0x00134FCC
		public static List<int> GetUpsideMenuItemList(ShopItemTemplet shopTemplet)
		{
			if (shopTemplet == null)
			{
				return NKCShopManager.PACKAGE_RESOURCE_LIST;
			}
			ShopTabTemplet shopTabTemplet = ShopTabTemplet.Find(shopTemplet.m_TabID, shopTemplet.m_TabSubIndex);
			if (shopTabTemplet != null)
			{
				List<int> list = NKCShopManager.MakeShopTabResourceList(shopTabTemplet);
				if (list.Count > 0)
				{
					if (shopTemplet.m_PriceItemID != 0 && !list.Contains(shopTemplet.m_PriceItemID))
					{
						list.Add(shopTemplet.m_PriceItemID);
					}
					return list;
				}
			}
			if (shopTemplet.m_PriceItemID == 0)
			{
				return NKCShopManager.PACKAGE_RESOURCE_LIST;
			}
			if (NKCShopManager.PACKAGE_RESOURCE_LIST.Contains(shopTemplet.m_PriceItemID))
			{
				return NKCShopManager.PACKAGE_RESOURCE_LIST;
			}
			return new List<int>
			{
				101,
				102,
				shopTemplet.m_PriceItemID
			};
		}

		// Token: 0x06003C7D RID: 15485 RVA: 0x00136E74 File Offset: 0x00135074
		public static List<int> MakeShopTabResourceList(ShopTabTemplet tabTemplet)
		{
			if (tabTemplet == null)
			{
				return NKCShopManager.PACKAGE_RESOURCE_LIST;
			}
			List<int> list = new List<int>();
			if (tabTemplet.m_ResourceTypeID_1 > 0)
			{
				list.Add(tabTemplet.m_ResourceTypeID_1);
			}
			if (tabTemplet.m_ResourceTypeID_2 > 0)
			{
				list.Add(tabTemplet.m_ResourceTypeID_2);
			}
			if (tabTemplet.m_ResourceTypeID_3 > 0)
			{
				list.Add(tabTemplet.m_ResourceTypeID_3);
			}
			if (tabTemplet.m_ResourceTypeID_4 > 0)
			{
				list.Add(tabTemplet.m_ResourceTypeID_4);
			}
			if (tabTemplet.m_ResourceTypeID_5 > 0)
			{
				list.Add(tabTemplet.m_ResourceTypeID_5);
			}
			if (list.Count == 0)
			{
				list = NKCShopManager.PACKAGE_RESOURCE_LIST;
			}
			return list;
		}

		// Token: 0x06003C7E RID: 15486 RVA: 0x00136F08 File Offset: 0x00135108
		public static bool CanDisplayTab(string tabType, bool UseTabVisible)
		{
			foreach (ShopTabTemplet shopTabTemplet in ShopTabTemplet.Values)
			{
				if (shopTabTemplet.TabType == tabType && shopTabTemplet.m_ShopDisplay != ShopDisplayType.None && (!UseTabVisible || shopTabTemplet.m_Visible) && shopTabTemplet.EnableByTag && NKCSynchronizedTime.IsEventTime(shopTabTemplet.EventDateStartUtc, shopTabTemplet.EventDateEndUtc))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003C7F RID: 15487 RVA: 0x00136F94 File Offset: 0x00135194
		public static Color GetRibbonColor(ShopItemRibbon ribbonType)
		{
			if (ribbonType == ShopItemRibbon.NEW)
			{
				return NKCUtil.GetColor("#FFC528");
			}
			switch (ribbonType)
			{
			case ShopItemRibbon.EVENT:
				return NKCUtil.GetColor("#E996F3");
			case ShopItemRibbon.HOT:
				return NKCUtil.GetColor("#FF4B40");
			case ShopItemRibbon.BEST:
				return NKCUtil.GetColor("#FFC528");
			default:
				return Color.white;
			}
		}

		// Token: 0x06003C80 RID: 15488 RVA: 0x00136FEC File Offset: 0x001351EC
		public static string GetRibbonString(ShopItemRibbon ribbonType)
		{
			switch (ribbonType)
			{
			case ShopItemRibbon.ONE_PLUS_ONE:
				return "1 + 1";
			case ShopItemRibbon.FIRST_PURCHASE:
				return NKCUtilString.GET_STRING_SHOP_FIRST_PURCHASE;
			case ShopItemRibbon.NEW:
				return NKCUtilString.GET_STRING_SHOP_NEW;
			case ShopItemRibbon.LIMITED:
				return NKCUtilString.GET_STRING_SHOP_LIMIT;
			case ShopItemRibbon.TIME_LIMITED:
				return NKCUtilString.GET_STRING_SHOP_TIME_LIMIT;
			case ShopItemRibbon.POPULAR:
				return NKCUtilString.GET_STRING_SHOP_POPULAR;
			case ShopItemRibbon.SPECIAL:
				return NKCUtilString.GET_STRING_SHOP_SPECIAL;
			case ShopItemRibbon.EVENT:
				return NKCStringTable.GetString("SI_DP_SHOP_EVENT", false);
			case ShopItemRibbon.HOT:
				return NKCStringTable.GetString("SI_DP_SHOP_HOT", false);
			case ShopItemRibbon.BEST:
				return NKCUtilString.GET_STRING_SHOP_BEST;
			default:
				return "";
			}
		}

		// Token: 0x06003C81 RID: 15489 RVA: 0x00137078 File Offset: 0x00135278
		public static bool IsShowPurchasePolicy()
		{
			return NKCPublisherModule.InAppPurchase.ShowPurchasePolicy();
		}

		// Token: 0x06003C82 RID: 15490 RVA: 0x00137084 File Offset: 0x00135284
		public static bool IsShowPurchasePolicyBtn()
		{
			return NKCPublisherModule.InAppPurchase.ShowPurchasePolicyBtn();
		}

		// Token: 0x06003C83 RID: 15491 RVA: 0x00137090 File Offset: 0x00135290
		public static bool ShowShopItemCashCount(NKCUISlot slot, NKCUISlot.SlotData slotData, int freeCount, int paidCount)
		{
			if (slot == null)
			{
				return false;
			}
			if (NKCShopManager.UseSuperuserItemCount())
			{
				string itemCountString = NKCShopManager.GetItemCountString((long)freeCount, (long)paidCount);
				slot.SetSlotItemCountString(NKCUISlot.WillShowCount(slotData), itemCountString);
				return true;
			}
			return false;
		}

		// Token: 0x06003C84 RID: 15492 RVA: 0x001370CC File Offset: 0x001352CC
		public static bool ShowShopItemCashCount(Text label, int freeCount, int paidCount)
		{
			if (label == null)
			{
				return false;
			}
			if (NKCShopManager.UseSuperuserItemCount())
			{
				string itemCountString = NKCShopManager.GetItemCountString((long)freeCount, (long)paidCount);
				NKCUtil.SetLabelText(label, itemCountString);
				return true;
			}
			return false;
		}

		// Token: 0x06003C85 RID: 15493 RVA: 0x00137100 File Offset: 0x00135300
		public static string GetItemCountString(long freeCount, long cashCount)
		{
			return (freeCount + cashCount).ToString("N0");
		}

		// Token: 0x06003C86 RID: 15494 RVA: 0x0013711D File Offset: 0x0013531D
		public static bool UseSuperuserItemCount()
		{
			return !NKCDefineManager.DEFINE_SERVICE() && NKCScenManager.CurrentUserData().IsSuperUser();
		}

		// Token: 0x06003C87 RID: 15495 RVA: 0x00137138 File Offset: 0x00135338
		public static NKM_ITEM_GRADE GetGrade(ShopItemTemplet templet)
		{
			switch (templet.m_ItemType)
			{
			case NKM_REWARD_TYPE.RT_NONE:
				return NKM_ITEM_GRADE.NIG_N;
			case NKM_REWARD_TYPE.RT_UNIT:
			case NKM_REWARD_TYPE.RT_SHIP:
			case NKM_REWARD_TYPE.RT_OPERATOR:
				switch (NKMUnitTempletBase.Find(templet.m_ItemID).m_NKM_UNIT_GRADE)
				{
				default:
					return NKM_ITEM_GRADE.NIG_N;
				case NKM_UNIT_GRADE.NUG_R:
					return NKM_ITEM_GRADE.NIG_R;
				case NKM_UNIT_GRADE.NUG_SR:
					return NKM_ITEM_GRADE.NIG_SR;
				case NKM_UNIT_GRADE.NUG_SSR:
					return NKM_ITEM_GRADE.NIG_SSR;
				}
				break;
			case NKM_REWARD_TYPE.RT_MISC:
				return NKMItemMiscTemplet.Find(templet.m_ItemID).m_NKM_ITEM_GRADE;
			case NKM_REWARD_TYPE.RT_EQUIP:
				return NKMItemManager.GetEquipTemplet(templet.m_ItemID).m_NKM_ITEM_GRADE;
			case NKM_REWARD_TYPE.RT_MOLD:
				return NKMItemMoldTemplet.Find(templet.m_ItemID).m_Grade;
			case NKM_REWARD_TYPE.RT_SKIN:
				switch (NKMSkinManager.GetSkinTemplet(templet.m_ItemID).m_SkinGrade)
				{
				case NKMSkinTemplet.SKIN_GRADE.SG_VARIATION:
					return NKM_ITEM_GRADE.NIG_N;
				case NKMSkinTemplet.SKIN_GRADE.SG_RARE:
					return NKM_ITEM_GRADE.NIG_SR;
				case NKMSkinTemplet.SKIN_GRADE.SG_PREMIUM:
				case NKMSkinTemplet.SKIN_GRADE.SG_SPECIAL:
					return NKM_ITEM_GRADE.NIG_SSR;
				}
				return NKM_ITEM_GRADE.NIG_R;
			case NKM_REWARD_TYPE.RT_EMOTICON:
				switch (NKMEmoticonTemplet.Find(templet.m_ItemID).m_EmoticonGrade)
				{
				default:
					return NKM_ITEM_GRADE.NIG_N;
				case NKM_EMOTICON_GRADE.NEG_R:
					return NKM_ITEM_GRADE.NIG_R;
				case NKM_EMOTICON_GRADE.NEG_SR:
					return NKM_ITEM_GRADE.NIG_SR;
				case NKM_EMOTICON_GRADE.NEG_SSR:
					return NKM_ITEM_GRADE.NIG_SSR;
				}
				break;
			}
			return NKM_ITEM_GRADE.NIG_N;
		}

		// Token: 0x06003C88 RID: 15496 RVA: 0x00137258 File Offset: 0x00135458
		public static List<ShopTabTemplet> GetUseTabList(NKCShopManager.ShopTabCategory category)
		{
			List<ShopTabTemplet> list = new List<ShopTabTemplet>();
			NKCShopCategoryTemplet nkcshopCategoryTemplet = NKCShopCategoryTemplet.Find(category);
			if (nkcshopCategoryTemplet == null)
			{
				Debug.LogError(string.Format("Category Templet for {0} not found!", category));
				return list;
			}
			HashSet<string> hashSet = new HashSet<string>(nkcshopCategoryTemplet.m_UseTabID);
			foreach (ShopTabTemplet shopTabTemplet in ShopTabTemplet.Values)
			{
				if (shopTabTemplet.m_Visible && hashSet.Contains(shopTabTemplet.TabType))
				{
					list.Add(shopTabTemplet);
				}
			}
			return list;
		}

		// Token: 0x06003C89 RID: 15497 RVA: 0x001372F4 File Offset: 0x001354F4
		public static NKCShopCategoryTemplet GetCategoryFromTab(string type)
		{
			foreach (NKCShopCategoryTemplet nkcshopCategoryTemplet in NKMTempletContainer<NKCShopCategoryTemplet>.Values)
			{
				if (nkcshopCategoryTemplet.HasTab(type))
				{
					return nkcshopCategoryTemplet;
				}
			}
			return null;
		}

		// Token: 0x06003C8A RID: 15498 RVA: 0x0013734C File Offset: 0x0013554C
		private static string GetReddotKey(ShopItemTemplet shopItemTemplet)
		{
			return string.Format("REDDOT_CHECK_UTC_TIME_{0}_{1}", NKCScenManager.CurrentUserData().m_UserUID, shopItemTemplet.m_ProductID);
		}

		// Token: 0x06003C8B RID: 15499 RVA: 0x00137374 File Offset: 0x00135574
		public static void SetLastCheckedUTCTime(string tabType, int tabSubIndex = 0)
		{
			if (tabType == "TAB_NONE")
			{
				return;
			}
			foreach (ShopItemTemplet shopItemTemplet in NKMTempletContainer<ShopItemTemplet>.Values)
			{
				if (!(shopItemTemplet.m_TabID != tabType) && (tabSubIndex <= 0 || shopItemTemplet.m_TabSubIndex == tabSubIndex) && shopItemTemplet.m_Reddot == ShopReddotType.REDDOT_CHECKED && (shopItemTemplet.m_hsReddotAllow.Count <= 0 || shopItemTemplet.m_hsReddotAllow.Contains(NKMContentsVersionManager.GetCountryTag())))
				{
					NKCShopManager.SetLastCheckedUTCTime(shopItemTemplet);
				}
			}
		}

		// Token: 0x06003C8C RID: 15500 RVA: 0x00137414 File Offset: 0x00135614
		public static void SetLastCheckedUTCTime(ShopItemTemplet shopItemTemplet)
		{
			if (shopItemTemplet.m_Reddot != ShopReddotType.REDDOT_CHECKED)
			{
				return;
			}
			if (shopItemTemplet.m_hsReddotAllow.Count > 0 && !shopItemTemplet.m_hsReddotAllow.Contains(NKMContentsVersionManager.GetCountryTag()))
			{
				return;
			}
			PlayerPrefs.SetString(NKCShopManager.GetReddotKey(shopItemTemplet), NKCSynchronizedTime.GetServerUTCTime(0.0).Ticks.ToString());
			if (NKCUIShop.IsInstanceOpen && NKCUIShop.Instance.gameObject.activeSelf)
			{
				NKCUIShop.Instance.RefreshShopRedDot();
			}
		}

		// Token: 0x06003C8D RID: 15501 RVA: 0x00137498 File Offset: 0x00135698
		public static ShopReddotType GetReddotType(ShopItemTemplet shopItemTemplet)
		{
			if (shopItemTemplet.m_hsReddotAllow.Count > 0 && !shopItemTemplet.m_hsReddotAllow.Contains(NKMContentsVersionManager.GetCountryTag()))
			{
				return ShopReddotType.NONE;
			}
			if (shopItemTemplet.m_Reddot != ShopReddotType.REDDOT_CHECKED)
			{
				return shopItemTemplet.m_Reddot;
			}
			string reddotKey = NKCShopManager.GetReddotKey(shopItemTemplet);
			if (!PlayerPrefs.HasKey(reddotKey))
			{
				return ShopReddotType.REDDOT_CHECKED;
			}
			long num = long.Parse(PlayerPrefs.GetString(reddotKey));
			NKMShopData shopData = NKCScenManager.CurrentUserData().m_ShopData;
			if (!shopData.histories.ContainsKey(shopItemTemplet.m_ProductID))
			{
				return ShopReddotType.NONE;
			}
			if (!NKCSynchronizedTime.IsFinished(shopData.histories[shopItemTemplet.m_ProductID].nextResetDate))
			{
				return ShopReddotType.NONE;
			}
			if (shopData.histories[shopItemTemplet.m_ProductID].nextResetDate < num)
			{
				return ShopReddotType.NONE;
			}
			return ShopReddotType.REDDOT_CHECKED;
		}

		// Token: 0x06003C8E RID: 15502 RVA: 0x00137550 File Offset: 0x00135750
		public static int CheckTabReddotCount(out ShopReddotType reddotType, string tabType = "TAB_NONE", int tabSubIndex = 0)
		{
			int num = 0;
			reddotType = ShopReddotType.NONE;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return num;
			}
			if (NKCShopManager.m_lstSpecialItemTemplet == null || NKCShopManager.m_lstSpecialItemTemplet.Count == 0)
			{
				NKCShopManager.m_lstSpecialItemTemplet = NKCShopManager.GetLimitedCountSpecialItems();
			}
			for (int i = 0; i < NKCShopManager.m_lstSpecialItemTemplet.Count; i++)
			{
				bool flag;
				long num2;
				if (NKCShopManager.CanBuyFixShop(nkmuserData, NKCShopManager.m_lstSpecialItemTemplet[i], out flag, out num2, true) == NKM_ERROR_CODE.NEC_OK && NKCShopManager.m_lstSpecialItemTemplet[i].m_bEnabled && NKCShopManager.m_lstSpecialItemTemplet[i].m_bVisible && NKCShopManager.m_lstSpecialItemTemplet[i].TabTemplet.m_Visible && (!(tabType != "TAB_NONE") || !(tabType != NKCShopManager.m_lstSpecialItemTemplet[i].m_TabID)) && (tabSubIndex <= 0 || tabSubIndex == NKCShopManager.m_lstSpecialItemTemplet[i].m_TabSubIndex) && NKCSynchronizedTime.IsEventTime(NKCShopManager.m_lstSpecialItemTemplet[i].EventDateStartUtc, NKCShopManager.m_lstSpecialItemTemplet[i].EventDateEndUtc) && NKCShopManager.GetReddotType(NKCShopManager.m_lstSpecialItemTemplet[i]) != ShopReddotType.NONE && NKCShopManager.CanExhibitItem(NKCShopManager.m_lstSpecialItemTemplet[i], true, false) && NKCShopManager.m_lstSpecialItemTemplet[i].m_QuantityLimit > nkmuserData.m_ShopData.GetPurchasedCount(NKCShopManager.m_lstSpecialItemTemplet[i]))
				{
					if (reddotType < NKCShopManager.GetReddotType(NKCShopManager.m_lstSpecialItemTemplet[i]))
					{
						reddotType = NKCShopManager.GetReddotType(NKCShopManager.m_lstSpecialItemTemplet[i]);
					}
					num++;
				}
			}
			return num;
		}

		// Token: 0x06003C8F RID: 15503 RVA: 0x001376E8 File Offset: 0x001358E8
		public static long OwnedItemCount(NKCUISlot.SlotData data)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return 0L;
			}
			switch (data.eType)
			{
			case NKCUISlot.eSlotMode.Unit:
			case NKCUISlot.eSlotMode.UnitCount:
				return (long)NKCShopManager.GetOwnedUnitCount(data.ID);
			case NKCUISlot.eSlotMode.ItemMisc:
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(data.ID);
				if (itemMiscTempletByID != null)
				{
					return nkmuserData.m_InventoryData.GetCountMiscItem(itemMiscTempletByID);
				}
				goto IL_13C;
			}
			case NKCUISlot.eSlotMode.Equip:
			case NKCUISlot.eSlotMode.EquipCount:
				return (long)nkmuserData.m_InventoryData.GetSameKindEquipCount(data.ID, 0);
			case NKCUISlot.eSlotMode.Skin:
				return nkmuserData.m_InventoryData.HasItemSkin(data.ID) ? 1L : 0L;
			case NKCUISlot.eSlotMode.Mold:
				return nkmuserData.m_CraftData.GetMoldCount(data.ID);
			case NKCUISlot.eSlotMode.Buff:
				return (nkmuserData.m_companyBuffDataList.Find((NKMCompanyBuffData e) => e.Id == data.ID) != null) ? 1L : 0L;
			case NKCUISlot.eSlotMode.Emoticon:
				return NKCEmoticonManager.HasEmoticon(data.ID) ? 1L : 0L;
			}
			Log.Error(string.Format("{0}: 소유할 수 있는 아이템 타입인지 확인", data.eType), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCShopManager.cs", 2562);
			IL_13C:
			return 0L;
		}

		// Token: 0x06003C90 RID: 15504 RVA: 0x00137834 File Offset: 0x00135A34
		public static int GetOwnedUnitCount(int unitId)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return 0;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitId);
			switch (unitTempletBase.m_NKM_UNIT_TYPE)
			{
			case NKM_UNIT_TYPE.NUT_NORMAL:
				return nkmuserData.m_ArmyData.GetUnitCountByID(unitId);
			case NKM_UNIT_TYPE.NUT_SHIP:
				return nkmuserData.m_ArmyData.GetSameKindShipCountFromID(unitId);
			case NKM_UNIT_TYPE.NUT_OPERATOR:
				return nkmuserData.m_ArmyData.GetOperatorCountByID(unitId);
			default:
				Log.Error(string.Format("{0}: 수량 표시할 유닛 타입인지 확인", unitTempletBase.m_NKM_UNIT_TYPE), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCShopManager.cs", 2586);
				return 0;
			}
		}

		// Token: 0x06003C92 RID: 15506 RVA: 0x00137955 File Offset: 0x00135B55
		[CompilerGenerated]
		internal static bool <IsProductAvailable>g__IsAdmin|42_0(NKMUserData userData)
		{
			return userData.IsSuperUser();
		}

		// Token: 0x040035D3 RID: 13779
		public const string UNLOCK_AUTO_STRING = "AUTO";

		// Token: 0x040035D4 RID: 13780
		internal static readonly TimeSpan ShopResetHour = TimeSpan.FromHours(0.0);

		// Token: 0x040035D5 RID: 13781
		private static List<ShopItemTemplet> m_lstLevelPackageTemplet = new List<ShopItemTemplet>();

		// Token: 0x040035D9 RID: 13785
		private static bool m_bReserveForceRefreshShop = false;

		// Token: 0x040035DA RID: 13786
		private static int m_totalBundleCount = 0;

		// Token: 0x040035DB RID: 13787
		private static HashSet<int> m_lstBundleItemIds = new HashSet<int>();

		// Token: 0x040035DC RID: 13788
		private static List<NKMRewardData> m_lstBundleItemReward = new List<NKMRewardData>();

		// Token: 0x040035DD RID: 13789
		private static bool bFeaturedTempletLoaded = false;

		// Token: 0x040035DE RID: 13790
		private static bool bLevelupPackageTempletLoaded = false;

		// Token: 0x040035DF RID: 13791
		private static Dictionary<int, List<ShopItemTemplet>> s_dicLinkedItemCache = new Dictionary<int, List<ShopItemTemplet>>();

		// Token: 0x040035E0 RID: 13792
		private static readonly List<int> PACKAGE_RESOURCE_LIST = new List<int>
		{
			1,
			2,
			101,
			102
		};

		// Token: 0x040035E1 RID: 13793
		private static List<ShopItemTemplet> m_lstSpecialItemTemplet = new List<ShopItemTemplet>();

		// Token: 0x02001397 RID: 5015
		// (Invoke) Token: 0x0600A656 RID: 42582
		public delegate void OnWaitComplete(bool bSuccess);

		// Token: 0x02001398 RID: 5016
		public struct ShopRewardSubstituteData
		{
			// Token: 0x04009AA6 RID: 39590
			public NKMRewardInfo Before;

			// Token: 0x04009AA7 RID: 39591
			public NKMRewardInfo After;
		}

		// Token: 0x02001399 RID: 5017
		public enum ShopTabCategory
		{
			// Token: 0x04009AA9 RID: 39593
			NONE = -1,
			// Token: 0x04009AAA RID: 39594
			PACKAGE,
			// Token: 0x04009AAB RID: 39595
			SEASON,
			// Token: 0x04009AAC RID: 39596
			EXCHANGE,
			// Token: 0x04009AAD RID: 39597
			SKIN,
			// Token: 0x04009AAE RID: 39598
			COUNT
		}
	}
}
