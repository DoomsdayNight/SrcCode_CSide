using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet.Base;

namespace NKC.Templet
{
	// Token: 0x02000858 RID: 2136
	public class NKCShopCategoryTemplet : INKMTemplet
	{
		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x060054D1 RID: 21713 RVA: 0x0019D06D File Offset: 0x0019B26D
		int INKMTemplet.Key
		{
			get
			{
				return (int)this.m_eCategory;
			}
		}

		// Token: 0x060054D2 RID: 21714 RVA: 0x0019D075 File Offset: 0x0019B275
		public static NKCShopCategoryTemplet Find(NKCShopManager.ShopTabCategory category)
		{
			return NKMTempletContainer<NKCShopCategoryTemplet>.Find((int)category);
		}

		// Token: 0x060054D3 RID: 21715 RVA: 0x0019D07D File Offset: 0x0019B27D
		public static void Load()
		{
			NKMTempletLoader.Load<NKCShopCategoryTemplet>("AB_SCRIPT", "SHOP_CATEGORY_TEMPLET", new Func<NKMLua, NKCShopCategoryTemplet>(NKCShopCategoryTemplet.LoadFromLUA), new string[]
			{
				"LUA_SHOP_CATEGORY_TEMPLET_01",
				"LUA_SHOP_CATEGORY_TEMPLET_02"
			});
		}

		// Token: 0x060054D4 RID: 21716 RVA: 0x0019D0B0 File Offset: 0x0019B2B0
		public static NKCShopCategoryTemplet LoadFromLUA(NKMLua lua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(lua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCShopCategoryTemplet.cs", 31))
			{
				return null;
			}
			NKCShopCategoryTemplet nkcshopCategoryTemplet = new NKCShopCategoryTemplet();
			bool flag = true;
			flag &= lua.GetData<NKCShopManager.ShopTabCategory>("m_ShopTabCategory", ref nkcshopCategoryTemplet.m_eCategory);
			flag &= lua.GetData("m_TabCategoryName", ref nkcshopCategoryTemplet.m_TabCategoryName);
			flag &= lua.GetData("m_OrderList", ref nkcshopCategoryTemplet.m_OrderList);
			flag &= lua.GetData("m_ThumbnailImg", ref nkcshopCategoryTemplet.m_ThumbnailImg);
			if (lua.OpenTable("m_UseTabID"))
			{
				nkcshopCategoryTemplet.m_UseTabID = new List<string>();
				int num = 1;
				string item = "";
				while (lua.GetData(num, ref item))
				{
					nkcshopCategoryTemplet.m_UseTabID.Add(item);
					num++;
				}
				lua.CloseTable();
			}
			else
			{
				flag = false;
			}
			if (lua.OpenTable("m_UnusedResourceID"))
			{
				nkcshopCategoryTemplet.m_UnusedResourceID = new HashSet<int>();
				int num2 = 1;
				int item2 = 0;
				while (lua.GetData(num2, ref item2))
				{
					nkcshopCategoryTemplet.m_UnusedResourceID.Add(item2);
					num2++;
				}
				lua.CloseTable();
			}
			if (!flag)
			{
				return null;
			}
			return nkcshopCategoryTemplet;
		}

		// Token: 0x060054D5 RID: 21717 RVA: 0x0019D1BB File Offset: 0x0019B3BB
		public bool HasTab(string tabType)
		{
			return this.m_UseTabID.Contains(tabType);
		}

		// Token: 0x060054D6 RID: 21718 RVA: 0x0019D1C9 File Offset: 0x0019B3C9
		void INKMTemplet.Join()
		{
		}

		// Token: 0x060054D7 RID: 21719 RVA: 0x0019D1CB File Offset: 0x0019B3CB
		void INKMTemplet.Validate()
		{
		}

		// Token: 0x040043BE RID: 17342
		public NKCShopManager.ShopTabCategory m_eCategory;

		// Token: 0x040043BF RID: 17343
		public string m_TabCategoryName;

		// Token: 0x040043C0 RID: 17344
		public int m_OrderList;

		// Token: 0x040043C1 RID: 17345
		public List<string> m_UseTabID;

		// Token: 0x040043C2 RID: 17346
		public string m_ThumbnailImg;

		// Token: 0x040043C3 RID: 17347
		public HashSet<int> m_UnusedResourceID;
	}
}
