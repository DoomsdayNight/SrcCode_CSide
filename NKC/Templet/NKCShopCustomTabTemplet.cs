using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet.Base;

namespace NKC.Templet
{
	// Token: 0x02000859 RID: 2137
	public class NKCShopCustomTabTemplet
	{
		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x060054D9 RID: 21721 RVA: 0x0019D1D5 File Offset: 0x0019B3D5
		public TabId TabId
		{
			get
			{
				return this.tabId;
			}
		}

		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x060054DA RID: 21722 RVA: 0x0019D1DD File Offset: 0x0019B3DD
		public string TabType
		{
			get
			{
				return this.tabId.Type;
			}
		}

		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x060054DB RID: 21723 RVA: 0x0019D1EA File Offset: 0x0019B3EA
		public int SubIndex
		{
			get
			{
				return this.tabId.SubIndex;
			}
		}

		// Token: 0x060054DC RID: 21724 RVA: 0x0019D1F8 File Offset: 0x0019B3F8
		public static List<NKCShopCustomTabTemplet> Find(TabId tabID)
		{
			if (!NKCShopCustomTabTemplet.bLoaded)
			{
				NKCShopCustomTabTemplet.bLoaded = true;
				NKCShopCustomTabTemplet.groups = new Dictionary<TabId, List<NKCShopCustomTabTemplet>>();
				foreach (NKCShopCustomTabTemplet nkcshopCustomTabTemplet in NKMTempletLoader.LoadCommonPath<NKCShopCustomTabTemplet>("AB_SCRIPT", "LUA_SHOP_TAB_CUSTOM_TEMPLET", "SHOP_TAB_CUSTOM_TEMPLET", new Func<NKMLua, NKCShopCustomTabTemplet>(NKCShopCustomTabTemplet.LoadFromLUA)))
				{
					List<NKCShopCustomTabTemplet> list;
					if (!NKCShopCustomTabTemplet.groups.TryGetValue(nkcshopCustomTabTemplet.TabId, out list))
					{
						list = new List<NKCShopCustomTabTemplet>();
						NKCShopCustomTabTemplet.groups.Add(nkcshopCustomTabTemplet.TabId, list);
					}
					list.Add(nkcshopCustomTabTemplet);
				}
				foreach (KeyValuePair<TabId, List<NKCShopCustomTabTemplet>> keyValuePair in NKCShopCustomTabTemplet.groups)
				{
					keyValuePair.Value.Sort((NKCShopCustomTabTemplet a, NKCShopCustomTabTemplet b) => a.m_OrderList.CompareTo(b.m_OrderList));
				}
			}
			List<NKCShopCustomTabTemplet> result;
			if (NKCShopCustomTabTemplet.groups.TryGetValue(tabID, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060054DD RID: 21725 RVA: 0x0019D320 File Offset: 0x0019B520
		public static NKCShopCustomTabTemplet Find(TabId tabID, int index)
		{
			List<NKCShopCustomTabTemplet> list = NKCShopCustomTabTemplet.Find(tabID);
			if (list == null)
			{
				return null;
			}
			if (index < 0)
			{
				return null;
			}
			if (index < list.Count)
			{
				return list[index];
			}
			return null;
		}

		// Token: 0x060054DE RID: 21726 RVA: 0x0019D354 File Offset: 0x0019B554
		public static NKCShopCustomTabTemplet LoadFromLUA(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCShopCustomTabTemplet.cs", 77))
			{
				return null;
			}
			NKCShopCustomTabTemplet nkcshopCustomTabTemplet = new NKCShopCustomTabTemplet();
			string type;
			int subIndex;
			bool flag = true & cNKMLua.GetData("m_TabID", out type, "TAB_CASH") & cNKMLua.GetData("m_TabSubIndex", out subIndex, 0) & cNKMLua.GetData("m_OrderList", ref nkcshopCustomTabTemplet.m_OrderList) & cNKMLua.GetData("m_UsePrefabName", ref nkcshopCustomTabTemplet.m_UsePrefabName) & cNKMLua.GetDataList("m_UseProductID", out nkcshopCustomTabTemplet.m_UseProductID);
			nkcshopCustomTabTemplet.tabId = new TabId(type, subIndex);
			if (!flag)
			{
				return null;
			}
			return nkcshopCustomTabTemplet;
		}

		// Token: 0x040043C4 RID: 17348
		public const string CashTabName = "TAB_CASH";

		// Token: 0x040043C5 RID: 17349
		private TabId tabId;

		// Token: 0x040043C6 RID: 17350
		public string listContentsTagAllow;

		// Token: 0x040043C7 RID: 17351
		public string listContentsTagIgnore;

		// Token: 0x040043C8 RID: 17352
		public int m_OrderList;

		// Token: 0x040043C9 RID: 17353
		public string m_UsePrefabName;

		// Token: 0x040043CA RID: 17354
		public List<int> m_UseProductID;

		// Token: 0x040043CB RID: 17355
		private static Dictionary<TabId, List<NKCShopCustomTabTemplet>> groups;

		// Token: 0x040043CC RID: 17356
		private static bool bLoaded;
	}
}
