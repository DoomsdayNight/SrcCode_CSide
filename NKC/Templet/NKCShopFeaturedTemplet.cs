using System;
using NKM;
using NKM.Shop;
using NKM.Templet.Base;

namespace NKC.Templet
{
	// Token: 0x0200085A RID: 2138
	public class NKCShopFeaturedTemplet : INKMTemplet
	{
		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x060054E1 RID: 21729 RVA: 0x0019D3ED File Offset: 0x0019B5ED
		int INKMTemplet.Key
		{
			get
			{
				return this.m_PackageID;
			}
		}

		// Token: 0x060054E2 RID: 21730 RVA: 0x0019D3F8 File Offset: 0x0019B5F8
		public static NKCShopFeaturedTemplet LoadFromLUA(NKMLua lua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(lua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCShopFeaturedTemplet.cs", 36))
			{
				return null;
			}
			NKCShopFeaturedTemplet nkcshopFeaturedTemplet = new NKCShopFeaturedTemplet();
			bool flag = true;
			flag &= lua.GetData("m_PackageID", ref nkcshopFeaturedTemplet.m_PackageID);
			flag &= lua.GetData("m_PackageGroupID", ref nkcshopFeaturedTemplet.m_PackageGroupID);
			flag &= lua.GetData("m_OrderList", ref nkcshopFeaturedTemplet.m_OrderList);
			lua.GetData<NKCShopFeaturedTemplet.DisplayCond>("m_DisplayCond", ref nkcshopFeaturedTemplet.m_DisplayCond);
			if (nkcshopFeaturedTemplet.m_DisplayCond != NKCShopFeaturedTemplet.DisplayCond.None)
			{
				flag &= lua.GetData("m_DisplayCondValue1", ref nkcshopFeaturedTemplet.m_DisplayCondValue1);
				lua.GetData("m_DisplayCondValue2", ref nkcshopFeaturedTemplet.m_DisplayCondValue2);
			}
			lua.GetData("m_FeaturedImage", ref nkcshopFeaturedTemplet.m_FeaturedImage);
			lua.GetData("m_ReddotRequired", ref nkcshopFeaturedTemplet.m_ReddotRequired);
			if (!flag)
			{
				return null;
			}
			return nkcshopFeaturedTemplet;
		}

		// Token: 0x060054E3 RID: 21731 RVA: 0x0019D4C4 File Offset: 0x0019B6C4
		public static void Load()
		{
			NKMTempletContainer<NKCShopFeaturedTemplet>.Load("AB_SCRIPT", "LUA_SHOP_FEATURED_TEMPLET", "SHOP_FEATURED_TEMPLET", new Func<NKMLua, NKCShopFeaturedTemplet>(NKCShopFeaturedTemplet.LoadFromLUA));
		}

		// Token: 0x060054E4 RID: 21732 RVA: 0x0019D4E6 File Offset: 0x0019B6E6
		void INKMTemplet.Join()
		{
		}

		// Token: 0x060054E5 RID: 21733 RVA: 0x0019D4E8 File Offset: 0x0019B6E8
		void INKMTemplet.Validate()
		{
		}

		// Token: 0x060054E6 RID: 21734 RVA: 0x0019D4EC File Offset: 0x0019B6EC
		public bool CheckCondition(NKMUserData userData)
		{
			switch (this.m_DisplayCond)
			{
			case NKCShopFeaturedTemplet.DisplayCond.HAVE_ITEM_UNDER:
				return userData.m_InventoryData.GetCountMiscItem(this.m_DisplayCondValue1) < (long)this.m_DisplayCondValue2;
			case NKCShopFeaturedTemplet.DisplayCond.PAID_AMOUNT_UNDER:
				return userData.m_ShopData.GetTotalPayment() < (double)this.m_DisplayCondValue1;
			case NKCShopFeaturedTemplet.DisplayCond.PAID_AMOUNT_OVER:
				return userData.m_ShopData.GetTotalPayment() >= (double)this.m_DisplayCondValue1;
			default:
				return true;
			}
		}

		// Token: 0x060054E7 RID: 21735 RVA: 0x0019D560 File Offset: 0x0019B760
		public static int CompareHighPriceFirst(NKCShopFeaturedTemplet lhs, NKCShopFeaturedTemplet rhs)
		{
			if (lhs.m_OrderList != rhs.m_OrderList)
			{
				return lhs.m_OrderList.CompareTo(rhs.m_OrderList);
			}
			ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(lhs.m_PackageID);
			ShopItemTemplet shopItemTemplet2 = ShopItemTemplet.Find(rhs.m_PackageID);
			int priceItemOrder = NKCShopFeaturedTemplet.GetPriceItemOrder(shopItemTemplet.m_PriceItemID);
			int priceItemOrder2 = NKCShopFeaturedTemplet.GetPriceItemOrder(shopItemTemplet2.m_PriceItemID);
			if (priceItemOrder != priceItemOrder2)
			{
				return priceItemOrder.CompareTo(priceItemOrder2);
			}
			if (shopItemTemplet.m_PriceItemID != shopItemTemplet2.m_PriceItemID)
			{
				return shopItemTemplet.m_PriceItemID.CompareTo(shopItemTemplet2.m_PriceItemID);
			}
			return shopItemTemplet2.m_Price.CompareTo(shopItemTemplet.m_Price);
		}

		// Token: 0x060054E8 RID: 21736 RVA: 0x0019D5FC File Offset: 0x0019B7FC
		public static int CompareLowPriceFirst(NKCShopFeaturedTemplet lhs, NKCShopFeaturedTemplet rhs)
		{
			if (lhs.m_OrderList != rhs.m_OrderList)
			{
				return lhs.m_OrderList.CompareTo(rhs.m_OrderList);
			}
			ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(lhs.m_PackageID);
			ShopItemTemplet shopItemTemplet2 = ShopItemTemplet.Find(rhs.m_PackageID);
			int priceItemOrder = NKCShopFeaturedTemplet.GetPriceItemOrder(shopItemTemplet.m_PriceItemID);
			int priceItemOrder2 = NKCShopFeaturedTemplet.GetPriceItemOrder(shopItemTemplet2.m_PriceItemID);
			if (priceItemOrder != priceItemOrder2)
			{
				return priceItemOrder.CompareTo(priceItemOrder2);
			}
			if (shopItemTemplet.m_PriceItemID != shopItemTemplet2.m_PriceItemID)
			{
				return shopItemTemplet.m_PriceItemID.CompareTo(shopItemTemplet2.m_PriceItemID);
			}
			return shopItemTemplet.m_Price.CompareTo(shopItemTemplet2.m_Price);
		}

		// Token: 0x060054E9 RID: 21737 RVA: 0x0019D697 File Offset: 0x0019B897
		private static int GetPriceItemOrder(int priceItemID)
		{
			if (priceItemID == 0)
			{
				return 0;
			}
			if (priceItemID == 101)
			{
				return 2;
			}
			if (priceItemID != 102)
			{
				return 3;
			}
			return 1;
		}

		// Token: 0x040043CD RID: 17357
		public int m_PackageID;

		// Token: 0x040043CE RID: 17358
		public string m_PackageGroupID;

		// Token: 0x040043CF RID: 17359
		public int m_OrderList;

		// Token: 0x040043D0 RID: 17360
		public NKCShopFeaturedTemplet.DisplayCond m_DisplayCond;

		// Token: 0x040043D1 RID: 17361
		public int m_DisplayCondValue1;

		// Token: 0x040043D2 RID: 17362
		public int m_DisplayCondValue2;

		// Token: 0x040043D3 RID: 17363
		public string m_FeaturedImage;

		// Token: 0x040043D4 RID: 17364
		public bool m_ReddotRequired;

		// Token: 0x020014F4 RID: 5364
		public enum DisplayCond
		{
			// Token: 0x04009F79 RID: 40825
			None,
			// Token: 0x04009F7A RID: 40826
			HAVE_ITEM_UNDER,
			// Token: 0x04009F7B RID: 40827
			PAID_AMOUNT_UNDER,
			// Token: 0x04009F7C RID: 40828
			PAID_AMOUNT_OVER
		}
	}
}
