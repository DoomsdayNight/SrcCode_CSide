using System;
using System.Collections.Generic;
using NKC.Templet;
using NKM;
using NKM.Shop;
using NKM.Templet;
using NKM.Templet.Office;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006CA RID: 1738
	public class NKCShopProductSortSystem
	{
		// Token: 0x06003C93 RID: 15507 RVA: 0x00137962 File Offset: 0x00135B62
		public static bool GetDescendingBySorting(List<NKCShopProductSortSystem.eSortOption> lstSortOption)
		{
			return lstSortOption.Count <= 0 || NKCShopProductSortSystem.GetDescendingBySorting(lstSortOption[0]);
		}

		// Token: 0x06003C94 RID: 15508 RVA: 0x0013797C File Offset: 0x00135B7C
		public static bool GetDescendingBySorting(NKCShopProductSortSystem.eSortOption sortOption)
		{
			switch (sortOption)
			{
			default:
				return true;
			case NKCShopProductSortSystem.eSortOption.Default_Last:
			case NKCShopProductSortSystem.eSortOption.ProductID_Last:
			case NKCShopProductSortSystem.eSortOption.ItemID_Last:
			case NKCShopProductSortSystem.eSortOption.Rarity_Low:
			case NKCShopProductSortSystem.eSortOption.CustomAscend1:
			case NKCShopProductSortSystem.eSortOption.CustomAscend2:
			case NKCShopProductSortSystem.eSortOption.CustomAscend3:
				return false;
			}
		}

		// Token: 0x06003C95 RID: 15509 RVA: 0x001379CC File Offset: 0x00135BCC
		public static NKCShopProductSortSystem.eSortCategory GetSortCategoryFromOption(NKCShopProductSortSystem.eSortOption option)
		{
			foreach (KeyValuePair<NKCShopProductSortSystem.eSortCategory, Tuple<NKCShopProductSortSystem.eSortOption, NKCShopProductSortSystem.eSortOption>> keyValuePair in NKCShopProductSortSystem.s_dicSortCategory)
			{
				if (keyValuePair.Value.Item1 == option)
				{
					return keyValuePair.Key;
				}
				if (keyValuePair.Value.Item2 == option)
				{
					return keyValuePair.Key;
				}
			}
			return NKCShopProductSortSystem.eSortCategory.None;
		}

		// Token: 0x06003C96 RID: 15510 RVA: 0x00137A4C File Offset: 0x00135C4C
		public static NKCShopProductSortSystem.eSortOption GetSortOptionByCategory(NKCShopProductSortSystem.eSortCategory category, bool bDescending)
		{
			Tuple<NKCShopProductSortSystem.eSortOption, NKCShopProductSortSystem.eSortOption> tuple = NKCShopProductSortSystem.s_dicSortCategory[category];
			if (!bDescending)
			{
				return tuple.Item1;
			}
			return tuple.Item2;
		}

		// Token: 0x06003C97 RID: 15511 RVA: 0x00137A78 File Offset: 0x00135C78
		public static bool IsDescending(NKCShopProductSortSystem.eSortOption option)
		{
			foreach (KeyValuePair<NKCShopProductSortSystem.eSortCategory, Tuple<NKCShopProductSortSystem.eSortOption, NKCShopProductSortSystem.eSortOption>> keyValuePair in NKCShopProductSortSystem.s_dicSortCategory)
			{
				if (keyValuePair.Value.Item1 == option)
				{
					return false;
				}
				if (keyValuePair.Value.Item2 == option)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003C98 RID: 15512 RVA: 0x00137AEC File Offset: 0x00135CEC
		public static NKCShopProductSortSystem.eSortOption GetInvertedAscendOption(NKCShopProductSortSystem.eSortOption option)
		{
			foreach (KeyValuePair<NKCShopProductSortSystem.eSortCategory, Tuple<NKCShopProductSortSystem.eSortOption, NKCShopProductSortSystem.eSortOption>> keyValuePair in NKCShopProductSortSystem.s_dicSortCategory)
			{
				if (keyValuePair.Value.Item1 == option)
				{
					return keyValuePair.Value.Item2;
				}
				if (keyValuePair.Value.Item2 == option)
				{
					return keyValuePair.Value.Item1;
				}
			}
			return option;
		}

		// Token: 0x06003C99 RID: 15513 RVA: 0x00137B78 File Offset: 0x00135D78
		public static List<NKCShopProductSortSystem.eSortOption> ChangeAscend(List<NKCShopProductSortSystem.eSortOption> targetList)
		{
			List<NKCShopProductSortSystem.eSortOption> list = new List<NKCShopProductSortSystem.eSortOption>(targetList);
			if (list == null || list.Count == 0)
			{
				return list;
			}
			list[0] = NKCShopProductSortSystem.GetInvertedAscendOption(list[0]);
			return list;
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06003C9A RID: 15514 RVA: 0x00137BAD File Offset: 0x00135DAD
		// (set) Token: 0x06003C9B RID: 15515 RVA: 0x00137BBA File Offset: 0x00135DBA
		public int FilterStatType_ThemeID
		{
			get
			{
				return this.m_Options.m_filterThemeID;
			}
			set
			{
				this.m_Options.m_filterThemeID = value;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06003C9C RID: 15516 RVA: 0x00137BC8 File Offset: 0x00135DC8
		public List<ShopItemTemplet> SortedProductList
		{
			get
			{
				if (this.m_lstCurrentProductList == null)
				{
					if (this.m_Options.setFilterOption == null)
					{
						this.m_Options.setFilterOption = new HashSet<NKCShopProductSortSystem.eFilterOption>();
						this.FilterList(this.m_Options.setFilterOption);
					}
					else
					{
						this.FilterList(this.m_Options.setFilterOption);
					}
				}
				return this.m_lstCurrentProductList;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06003C9D RID: 15517 RVA: 0x00137C24 File Offset: 0x00135E24
		// (set) Token: 0x06003C9E RID: 15518 RVA: 0x00137C31 File Offset: 0x00135E31
		public HashSet<NKCShopProductSortSystem.eFilterOption> FilterSet
		{
			get
			{
				return this.m_Options.setFilterOption;
			}
			set
			{
				this.FilterList(value);
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06003C9F RID: 15519 RVA: 0x00137C3A File Offset: 0x00135E3A
		// (set) Token: 0x06003CA0 RID: 15520 RVA: 0x00137C47 File Offset: 0x00135E47
		public List<NKCShopProductSortSystem.eSortOption> lstSortOption
		{
			get
			{
				return this.m_Options.lstSortOption;
			}
			set
			{
				this.SortList(value, false);
			}
		}

		// Token: 0x06003CA1 RID: 15521 RVA: 0x00137C51 File Offset: 0x00135E51
		protected NKCShopProductSortSystem()
		{
		}

		// Token: 0x06003CA2 RID: 15522 RVA: 0x00137C59 File Offset: 0x00135E59
		public NKCShopProductSortSystem(NKMUserData userData, IEnumerable<ShopItemTemplet> lstTargetProducts, NKCShopProductSortSystem.ShopProductListOptions options)
		{
			this.m_Options = options;
			this.m_dicAllProductList = this.BuildFullList(userData, lstTargetProducts, options);
		}

		// Token: 0x06003CA3 RID: 15523 RVA: 0x00137C77 File Offset: 0x00135E77
		public void BuildFilterAndSortedList(HashSet<NKCShopProductSortSystem.eFilterOption> setfilterType, List<NKCShopProductSortSystem.eSortOption> lstSortOption)
		{
			this.m_Options.setFilterOption = setfilterType;
			this.m_Options.lstSortOption = lstSortOption;
			this.FilterList(setfilterType);
		}

		// Token: 0x06003CA4 RID: 15524 RVA: 0x00137C98 File Offset: 0x00135E98
		private Dictionary<int, ShopItemTemplet> BuildFullList(NKMUserData userData, IEnumerable<ShopItemTemplet> lstTarget, NKCShopProductSortSystem.ShopProductListOptions options)
		{
			Dictionary<int, ShopItemTemplet> dictionary = new Dictionary<int, ShopItemTemplet>();
			foreach (ShopItemTemplet shopItemTemplet in lstTarget)
			{
				if (options.AdditionalExcludeFilterFunc == null || options.AdditionalExcludeFilterFunc(shopItemTemplet))
				{
					dictionary.Add(shopItemTemplet.Key, shopItemTemplet);
				}
			}
			return dictionary;
		}

		// Token: 0x06003CA5 RID: 15525 RVA: 0x00137D04 File Offset: 0x00135F04
		protected bool FilterData(ShopItemTemplet shopTemplet, List<HashSet<NKCShopProductSortSystem.eFilterOption>> setFilter)
		{
			if (setFilter == null || setFilter.Count == 0)
			{
				return true;
			}
			for (int i = 0; i < setFilter.Count; i++)
			{
				if (!this.CheckFilter(shopTemplet, setFilter[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003CA6 RID: 15526 RVA: 0x00137D44 File Offset: 0x00135F44
		private bool CheckFilter(ShopItemTemplet shopTemplet, HashSet<NKCShopProductSortSystem.eFilterOption> setFilter)
		{
			foreach (NKCShopProductSortSystem.eFilterOption filterOption in setFilter)
			{
				if (this.CheckFilter(shopTemplet, filterOption))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003CA7 RID: 15527 RVA: 0x00137D9C File Offset: 0x00135F9C
		private bool CheckFilter(ShopItemTemplet shopTemplet, NKCShopProductSortSystem.eFilterOption filterOption)
		{
			switch (filterOption)
			{
			case NKCShopProductSortSystem.eFilterOption.Nothing:
				return false;
			case NKCShopProductSortSystem.eFilterOption.Everything:
				return true;
			case NKCShopProductSortSystem.eFilterOption.Theme:
			{
				if (this.m_Options.m_filterThemeID == 0)
				{
					return true;
				}
				NKCThemeGroupTemplet nkcthemeGroupTemplet = NKCThemeGroupTemplet.Find(this.m_Options.m_filterThemeID);
				if (nkcthemeGroupTemplet == null)
				{
					Debug.LogError(string.Format("Logic Error : theme templet not found. id : {0}", this.m_Options.m_filterThemeID));
					return true;
				}
				if (shopTemplet.m_ItemType != NKM_REWARD_TYPE.RT_MISC)
				{
					return false;
				}
				NKMOfficeInteriorTemplet nkmofficeInteriorTemplet = NKMOfficeInteriorTemplet.Find(shopTemplet.m_ItemID);
				return nkmofficeInteriorTemplet != null && nkcthemeGroupTemplet.GroupID.Contains(nkmofficeInteriorTemplet.GroupID);
			}
			default:
				return false;
			}
		}

		// Token: 0x06003CA8 RID: 15528 RVA: 0x00137E34 File Offset: 0x00136034
		public void FilterList(HashSet<NKCShopProductSortSystem.eFilterOption> setFilter)
		{
			if (setFilter == null)
			{
				setFilter = new HashSet<NKCShopProductSortSystem.eFilterOption>();
			}
			this.m_Options.setFilterOption = setFilter;
			if (this.m_lstCurrentProductList == null)
			{
				this.m_lstCurrentProductList = new List<ShopItemTemplet>();
			}
			this.m_lstCurrentProductList.Clear();
			List<HashSet<NKCShopProductSortSystem.eFilterOption>> setFilter2 = new List<HashSet<NKCShopProductSortSystem.eFilterOption>>();
			this.SetFilterCategory(setFilter, ref setFilter2);
			foreach (KeyValuePair<int, ShopItemTemplet> keyValuePair in this.m_dicAllProductList)
			{
				ShopItemTemplet value = keyValuePair.Value;
				if (this.FilterData(value, setFilter2))
				{
					this.m_lstCurrentProductList.Add(value);
				}
			}
			if (this.m_Options.lstSortOption != null)
			{
				this.SortList(this.m_Options.lstSortOption, true);
				return;
			}
			this.m_Options.lstSortOption = new List<NKCShopProductSortSystem.eSortOption>();
			this.SortList(this.m_Options.lstSortOption, true);
		}

		// Token: 0x06003CA9 RID: 15529 RVA: 0x00137F24 File Offset: 0x00136124
		private void SetFilterCategory(HashSet<NKCShopProductSortSystem.eFilterOption> setFilter, ref List<HashSet<NKCShopProductSortSystem.eFilterOption>> needFilterSet)
		{
			if (setFilter.Count == 0)
			{
				return;
			}
			for (int i = 0; i < NKCShopProductSortSystem.m_lstFilterCategory.Count; i++)
			{
				HashSet<NKCShopProductSortSystem.eFilterOption> hashSet = new HashSet<NKCShopProductSortSystem.eFilterOption>();
				foreach (NKCShopProductSortSystem.eFilterOption item in setFilter)
				{
					hashSet.Add(item);
				}
				hashSet.IntersectWith(NKCShopProductSortSystem.m_lstFilterCategory[i]);
				if (hashSet.Count > 0)
				{
					needFilterSet.Add(hashSet);
				}
			}
		}

		// Token: 0x06003CAA RID: 15530 RVA: 0x00137FBC File Offset: 0x001361BC
		public void SortList(List<NKCShopProductSortSystem.eSortOption> lstSortOption, bool bForce = false)
		{
			if (this.m_lstCurrentProductList != null)
			{
				if (!bForce && lstSortOption.Count == this.m_Options.lstSortOption.Count)
				{
					bool flag = true;
					for (int i = 0; i < lstSortOption.Count; i++)
					{
						if (lstSortOption[i] != this.m_Options.lstSortOption[i])
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						return;
					}
				}
				this.SortDataList(ref this.m_lstCurrentProductList, lstSortOption);
				this.m_Options.lstSortOption = lstSortOption;
				return;
			}
			this.m_Options.lstSortOption = lstSortOption;
			if (this.m_Options.setFilterOption != null)
			{
				this.FilterList(this.m_Options.setFilterOption);
				return;
			}
			this.m_Options.setFilterOption = new HashSet<NKCShopProductSortSystem.eFilterOption>();
			this.FilterList(this.m_Options.setFilterOption);
		}

		// Token: 0x06003CAB RID: 15531 RVA: 0x00138087 File Offset: 0x00136287
		public List<ShopItemTemplet> GetCurrentProductList()
		{
			return this.m_lstCurrentProductList;
		}

		// Token: 0x06003CAC RID: 15532 RVA: 0x00138090 File Offset: 0x00136290
		private void SortDataList(ref List<ShopItemTemplet> lstMiscTemplet, List<NKCShopProductSortSystem.eSortOption> lstSortOption)
		{
			NKCUnitSortSystem.NKCDataComparerer<ShopItemTemplet> nkcdataComparerer = new NKCUnitSortSystem.NKCDataComparerer<ShopItemTemplet>(Array.Empty<NKCUnitSortSystem.NKCDataComparerer<ShopItemTemplet>.CompareFunc>());
			HashSet<NKCShopProductSortSystem.eSortCategory> hashSet = new HashSet<NKCShopProductSortSystem.eSortCategory>();
			if (this.m_Options.PreemptiveSortFunc != null)
			{
				nkcdataComparerer.AddFunc(this.m_Options.PreemptiveSortFunc);
			}
			foreach (NKCShopProductSortSystem.eSortOption eSortOption in lstSortOption)
			{
				if (eSortOption != NKCShopProductSortSystem.eSortOption.None)
				{
					NKCUnitSortSystem.NKCDataComparerer<ShopItemTemplet>.CompareFunc dataComparer = this.GetDataComparer(eSortOption);
					if (dataComparer != null)
					{
						nkcdataComparerer.AddFunc(dataComparer);
						hashSet.Add(NKCShopProductSortSystem.GetSortCategoryFromOption(eSortOption));
					}
				}
			}
			if (this.m_Options.lstDefaultSortOption != null)
			{
				foreach (NKCShopProductSortSystem.eSortOption eSortOption2 in this.m_Options.lstDefaultSortOption)
				{
					NKCShopProductSortSystem.eSortCategory sortCategoryFromOption = NKCShopProductSortSystem.GetSortCategoryFromOption(eSortOption2);
					if (!hashSet.Contains(sortCategoryFromOption))
					{
						nkcdataComparerer.AddFunc(this.GetDataComparer(eSortOption2));
						hashSet.Add(sortCategoryFromOption);
					}
				}
			}
			if (!hashSet.Contains(NKCShopProductSortSystem.eSortCategory.Default))
			{
				nkcdataComparerer.AddFunc(new NKCUnitSortSystem.NKCDataComparerer<ShopItemTemplet>.CompareFunc(this.CompareByDefaultAscending));
			}
			if (nkcdataComparerer.GetComparerCount() > 0)
			{
				lstMiscTemplet.Sort(nkcdataComparerer);
			}
		}

		// Token: 0x06003CAD RID: 15533 RVA: 0x001381D0 File Offset: 0x001363D0
		private NKCUnitSortSystem.NKCDataComparerer<ShopItemTemplet>.CompareFunc GetDataComparer(NKCShopProductSortSystem.eSortOption sortOption)
		{
			switch (sortOption)
			{
			default:
				return new NKCUnitSortSystem.NKCDataComparerer<ShopItemTemplet>.CompareFunc(this.CompareByDefaultAscending);
			case NKCShopProductSortSystem.eSortOption.Default_Last:
				return new NKCUnitSortSystem.NKCDataComparerer<ShopItemTemplet>.CompareFunc(this.CompareByDefaultDescending);
			case NKCShopProductSortSystem.eSortOption.ProductID_First:
				return new NKCUnitSortSystem.NKCDataComparerer<ShopItemTemplet>.CompareFunc(this.CompareByProductIDAscending);
			case NKCShopProductSortSystem.eSortOption.ProductID_Last:
				return new NKCUnitSortSystem.NKCDataComparerer<ShopItemTemplet>.CompareFunc(this.CompareByProductIDDescending);
			case NKCShopProductSortSystem.eSortOption.ItemID_First:
				return new NKCUnitSortSystem.NKCDataComparerer<ShopItemTemplet>.CompareFunc(this.CompareByItemIDAscending);
			case NKCShopProductSortSystem.eSortOption.ItemID_Last:
				return new NKCUnitSortSystem.NKCDataComparerer<ShopItemTemplet>.CompareFunc(this.CompareByItemIDDescending);
			case NKCShopProductSortSystem.eSortOption.Rarity_High:
				return new NKCUnitSortSystem.NKCDataComparerer<ShopItemTemplet>.CompareFunc(NKCShopProductSortSystem.CompareByRarityDescending);
			case NKCShopProductSortSystem.eSortOption.Rarity_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<ShopItemTemplet>.CompareFunc(NKCShopProductSortSystem.CompareByRarityAscending);
			case NKCShopProductSortSystem.eSortOption.CustomAscend1:
			case NKCShopProductSortSystem.eSortOption.CustomAscend2:
			case NKCShopProductSortSystem.eSortOption.CustomAscend3:
				if (this.m_Options.lstCustomSortFunc.ContainsKey(NKCShopProductSortSystem.GetSortCategoryFromOption(sortOption)))
				{
					return (ShopItemTemplet a, ShopItemTemplet b) => this.m_Options.lstCustomSortFunc[NKCShopProductSortSystem.GetSortCategoryFromOption(sortOption)].Value(b, a);
				}
				return null;
			case NKCShopProductSortSystem.eSortOption.CustomDescend1:
			case NKCShopProductSortSystem.eSortOption.CustomDescend2:
			case NKCShopProductSortSystem.eSortOption.CustomDescend3:
				if (this.m_Options.lstCustomSortFunc.ContainsKey(NKCShopProductSortSystem.GetSortCategoryFromOption(sortOption)))
				{
					return this.m_Options.lstCustomSortFunc[NKCShopProductSortSystem.GetSortCategoryFromOption(sortOption)].Value;
				}
				return null;
			}
		}

		// Token: 0x06003CAE RID: 15534 RVA: 0x00138310 File Offset: 0x00136510
		public static int CompareByRarityAscending(ShopItemTemplet lhs, ShopItemTemplet rhs)
		{
			NKM_ITEM_GRADE grade = NKCShopManager.GetGrade(lhs);
			NKM_ITEM_GRADE grade2 = NKCShopManager.GetGrade(rhs);
			return grade.CompareTo(grade2);
		}

		// Token: 0x06003CAF RID: 15535 RVA: 0x00138340 File Offset: 0x00136540
		public static int CompareByRarityDescending(ShopItemTemplet lhs, ShopItemTemplet rhs)
		{
			NKM_ITEM_GRADE grade = NKCShopManager.GetGrade(lhs);
			return NKCShopManager.GetGrade(rhs).CompareTo(grade);
		}

		// Token: 0x06003CB0 RID: 15536 RVA: 0x0013836E File Offset: 0x0013656E
		private int CompareByProductIDAscending(ShopItemTemplet lhs, ShopItemTemplet rhs)
		{
			return lhs.m_ProductID.CompareTo(rhs.Key);
		}

		// Token: 0x06003CB1 RID: 15537 RVA: 0x00138381 File Offset: 0x00136581
		private int CompareByProductIDDescending(ShopItemTemplet lhs, ShopItemTemplet rhs)
		{
			return rhs.m_ProductID.CompareTo(lhs.Key);
		}

		// Token: 0x06003CB2 RID: 15538 RVA: 0x00138394 File Offset: 0x00136594
		private int CompareByItemIDAscending(ShopItemTemplet lhs, ShopItemTemplet rhs)
		{
			return lhs.m_ItemID.CompareTo(rhs.Key);
		}

		// Token: 0x06003CB3 RID: 15539 RVA: 0x001383A7 File Offset: 0x001365A7
		private int CompareByItemIDDescending(ShopItemTemplet lhs, ShopItemTemplet rhs)
		{
			return rhs.m_ItemID.CompareTo(lhs.Key);
		}

		// Token: 0x06003CB4 RID: 15540 RVA: 0x001383BA File Offset: 0x001365BA
		private int CompareByDefaultAscending(ShopItemTemplet lhs, ShopItemTemplet rhs)
		{
			return lhs.m_OrderList.CompareTo(rhs.m_OrderList);
		}

		// Token: 0x06003CB5 RID: 15541 RVA: 0x001383CD File Offset: 0x001365CD
		private int CompareByDefaultDescending(ShopItemTemplet lhs, ShopItemTemplet rhs)
		{
			return rhs.m_OrderList.CompareTo(lhs.m_OrderList);
		}

		// Token: 0x06003CB6 RID: 15542 RVA: 0x001383E0 File Offset: 0x001365E0
		public static string GetSortName(NKCShopProductSortSystem.eSortOption sortOption)
		{
			return NKCShopProductSortSystem.GetSortName(NKCShopProductSortSystem.GetSortCategoryFromOption(sortOption));
		}

		// Token: 0x06003CB7 RID: 15543 RVA: 0x001383ED File Offset: 0x001365ED
		public static string GetSortName(NKCShopProductSortSystem.eSortCategory sortCategory)
		{
			return "";
		}

		// Token: 0x040035E2 RID: 13794
		private static readonly Dictionary<NKCShopProductSortSystem.eSortCategory, Tuple<NKCShopProductSortSystem.eSortOption, NKCShopProductSortSystem.eSortOption>> s_dicSortCategory = new Dictionary<NKCShopProductSortSystem.eSortCategory, Tuple<NKCShopProductSortSystem.eSortOption, NKCShopProductSortSystem.eSortOption>>
		{
			{
				NKCShopProductSortSystem.eSortCategory.None,
				new Tuple<NKCShopProductSortSystem.eSortOption, NKCShopProductSortSystem.eSortOption>(NKCShopProductSortSystem.eSortOption.None, NKCShopProductSortSystem.eSortOption.None)
			},
			{
				NKCShopProductSortSystem.eSortCategory.Default,
				new Tuple<NKCShopProductSortSystem.eSortOption, NKCShopProductSortSystem.eSortOption>(NKCShopProductSortSystem.eSortOption.Default_First, NKCShopProductSortSystem.eSortOption.Default_Last)
			},
			{
				NKCShopProductSortSystem.eSortCategory.ProductID,
				new Tuple<NKCShopProductSortSystem.eSortOption, NKCShopProductSortSystem.eSortOption>(NKCShopProductSortSystem.eSortOption.ProductID_First, NKCShopProductSortSystem.eSortOption.ProductID_Last)
			},
			{
				NKCShopProductSortSystem.eSortCategory.ItemID,
				new Tuple<NKCShopProductSortSystem.eSortOption, NKCShopProductSortSystem.eSortOption>(NKCShopProductSortSystem.eSortOption.ItemID_First, NKCShopProductSortSystem.eSortOption.ItemID_Last)
			},
			{
				NKCShopProductSortSystem.eSortCategory.Rarity,
				new Tuple<NKCShopProductSortSystem.eSortOption, NKCShopProductSortSystem.eSortOption>(NKCShopProductSortSystem.eSortOption.Rarity_Low, NKCShopProductSortSystem.eSortOption.Rarity_High)
			},
			{
				NKCShopProductSortSystem.eSortCategory.Custom1,
				new Tuple<NKCShopProductSortSystem.eSortOption, NKCShopProductSortSystem.eSortOption>(NKCShopProductSortSystem.eSortOption.CustomAscend1, NKCShopProductSortSystem.eSortOption.CustomDescend1)
			},
			{
				NKCShopProductSortSystem.eSortCategory.Custom2,
				new Tuple<NKCShopProductSortSystem.eSortOption, NKCShopProductSortSystem.eSortOption>(NKCShopProductSortSystem.eSortOption.CustomAscend2, NKCShopProductSortSystem.eSortOption.CustomDescend2)
			},
			{
				NKCShopProductSortSystem.eSortCategory.Custom3,
				new Tuple<NKCShopProductSortSystem.eSortOption, NKCShopProductSortSystem.eSortOption>(NKCShopProductSortSystem.eSortOption.CustomAscend3, NKCShopProductSortSystem.eSortOption.CustomDescend3)
			}
		};

		// Token: 0x040035E3 RID: 13795
		private static readonly List<NKCShopProductSortSystem.eSortOption> DEFAULT_SHOP_SORT_OPTION_LIST = new List<NKCShopProductSortSystem.eSortOption>
		{
			NKCShopProductSortSystem.eSortOption.Default_First,
			NKCShopProductSortSystem.eSortOption.ItemID_First
		};

		// Token: 0x040035E4 RID: 13796
		private static readonly HashSet<NKCShopProductSortSystem.eFilterOption> m_setFilterCategory_Theme = new HashSet<NKCShopProductSortSystem.eFilterOption>
		{
			NKCShopProductSortSystem.eFilterOption.Theme
		};

		// Token: 0x040035E5 RID: 13797
		private static readonly List<HashSet<NKCShopProductSortSystem.eFilterOption>> m_lstFilterCategory = new List<HashSet<NKCShopProductSortSystem.eFilterOption>>
		{
			NKCShopProductSortSystem.m_setFilterCategory_Theme
		};

		// Token: 0x040035E6 RID: 13798
		protected NKCShopProductSortSystem.ShopProductListOptions m_Options;

		// Token: 0x040035E7 RID: 13799
		protected Dictionary<int, ShopItemTemplet> m_dicAllProductList;

		// Token: 0x040035E8 RID: 13800
		protected List<ShopItemTemplet> m_lstCurrentProductList;

		// Token: 0x020013A0 RID: 5024
		public enum eFilterCategory
		{
			// Token: 0x04009ABF RID: 39615
			Theme
		}

		// Token: 0x020013A1 RID: 5025
		public enum eFilterOption
		{
			// Token: 0x04009AC1 RID: 39617
			Nothing,
			// Token: 0x04009AC2 RID: 39618
			Everything,
			// Token: 0x04009AC3 RID: 39619
			Theme
		}

		// Token: 0x020013A2 RID: 5026
		public enum eSortCategory
		{
			// Token: 0x04009AC5 RID: 39621
			None,
			// Token: 0x04009AC6 RID: 39622
			Default,
			// Token: 0x04009AC7 RID: 39623
			ProductID,
			// Token: 0x04009AC8 RID: 39624
			ItemID,
			// Token: 0x04009AC9 RID: 39625
			Rarity,
			// Token: 0x04009ACA RID: 39626
			Custom1,
			// Token: 0x04009ACB RID: 39627
			Custom2,
			// Token: 0x04009ACC RID: 39628
			Custom3
		}

		// Token: 0x020013A3 RID: 5027
		public enum eSortOption
		{
			// Token: 0x04009ACE RID: 39630
			None,
			// Token: 0x04009ACF RID: 39631
			Default_First,
			// Token: 0x04009AD0 RID: 39632
			Default_Last,
			// Token: 0x04009AD1 RID: 39633
			ProductID_First,
			// Token: 0x04009AD2 RID: 39634
			ProductID_Last,
			// Token: 0x04009AD3 RID: 39635
			ItemID_First,
			// Token: 0x04009AD4 RID: 39636
			ItemID_Last,
			// Token: 0x04009AD5 RID: 39637
			Rarity_High,
			// Token: 0x04009AD6 RID: 39638
			Rarity_Low,
			// Token: 0x04009AD7 RID: 39639
			CustomAscend1,
			// Token: 0x04009AD8 RID: 39640
			CustomDescend1,
			// Token: 0x04009AD9 RID: 39641
			CustomAscend2,
			// Token: 0x04009ADA RID: 39642
			CustomDescend2,
			// Token: 0x04009ADB RID: 39643
			CustomAscend3,
			// Token: 0x04009ADC RID: 39644
			CustomDescend3
		}

		// Token: 0x020013A4 RID: 5028
		public struct ShopProductListOptions
		{
			// Token: 0x04009ADD RID: 39645
			public HashSet<NKCShopProductSortSystem.eFilterOption> setFilterOption;

			// Token: 0x04009ADE RID: 39646
			public List<NKCShopProductSortSystem.eSortOption> lstSortOption;

			// Token: 0x04009ADF RID: 39647
			public NKCUnitSortSystem.NKCDataComparerer<ShopItemTemplet>.CompareFunc PreemptiveSortFunc;

			// Token: 0x04009AE0 RID: 39648
			public Dictionary<NKCShopProductSortSystem.eSortCategory, KeyValuePair<string, NKCUnitSortSystem.NKCDataComparerer<ShopItemTemplet>.CompareFunc>> lstCustomSortFunc;

			// Token: 0x04009AE1 RID: 39649
			public NKCShopProductSortSystem.ShopProductListOptions.CustomFilterFunc AdditionalExcludeFilterFunc;

			// Token: 0x04009AE2 RID: 39650
			public List<NKCShopProductSortSystem.eSortOption> lstDefaultSortOption;

			// Token: 0x04009AE3 RID: 39651
			public int m_filterThemeID;

			// Token: 0x02001A5A RID: 6746
			// (Invoke) Token: 0x0600BBB2 RID: 48050
			public delegate bool CustomFilterFunc(ShopItemTemplet miscTemplet);
		}
	}
}
