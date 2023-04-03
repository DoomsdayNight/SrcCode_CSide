using System;
using System.Collections.Generic;
using NKC.Templet;
using NKM;
using NKM.Templet.Office;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006A8 RID: 1704
	public class NKCMiscSortSystem
	{
		// Token: 0x06003828 RID: 14376 RVA: 0x0012243C File Offset: 0x0012063C
		public static bool GetDescendingBySorting(List<NKCMiscSortSystem.eSortOption> lstSortOption)
		{
			return lstSortOption.Count <= 0 || NKCMiscSortSystem.GetDescendingBySorting(lstSortOption[0]);
		}

		// Token: 0x06003829 RID: 14377 RVA: 0x00122458 File Offset: 0x00120658
		public static bool GetDescendingBySorting(NKCMiscSortSystem.eSortOption sortOption)
		{
			switch (sortOption)
			{
			default:
				return true;
			case NKCMiscSortSystem.eSortOption.ID_Last:
			case NKCMiscSortSystem.eSortOption.Point_Low:
			case NKCMiscSortSystem.eSortOption.Rarity_Low:
			case NKCMiscSortSystem.eSortOption.CannotPlace:
			case NKCMiscSortSystem.eSortOption.CustomAscend1:
			case NKCMiscSortSystem.eSortOption.CustomAscend2:
			case NKCMiscSortSystem.eSortOption.CustomAscend3:
				return false;
			}
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x001224A8 File Offset: 0x001206A8
		public static NKCMiscSortSystem.eSortCategory GetSortCategoryFromOption(NKCMiscSortSystem.eSortOption option)
		{
			foreach (KeyValuePair<NKCMiscSortSystem.eSortCategory, Tuple<NKCMiscSortSystem.eSortOption, NKCMiscSortSystem.eSortOption>> keyValuePair in NKCMiscSortSystem.s_dicSortCategory)
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
			return NKCMiscSortSystem.eSortCategory.None;
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x00122528 File Offset: 0x00120728
		public static NKCMiscSortSystem.eSortOption GetSortOptionByCategory(NKCMiscSortSystem.eSortCategory category, bool bDescending)
		{
			Tuple<NKCMiscSortSystem.eSortOption, NKCMiscSortSystem.eSortOption> tuple = NKCMiscSortSystem.s_dicSortCategory[category];
			if (!bDescending)
			{
				return tuple.Item1;
			}
			return tuple.Item2;
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x00122554 File Offset: 0x00120754
		public static bool IsDescending(NKCMiscSortSystem.eSortOption option)
		{
			foreach (KeyValuePair<NKCMiscSortSystem.eSortCategory, Tuple<NKCMiscSortSystem.eSortOption, NKCMiscSortSystem.eSortOption>> keyValuePair in NKCMiscSortSystem.s_dicSortCategory)
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

		// Token: 0x0600382D RID: 14381 RVA: 0x001225C8 File Offset: 0x001207C8
		public static NKCMiscSortSystem.eSortOption GetInvertedAscendOption(NKCMiscSortSystem.eSortOption option)
		{
			foreach (KeyValuePair<NKCMiscSortSystem.eSortCategory, Tuple<NKCMiscSortSystem.eSortOption, NKCMiscSortSystem.eSortOption>> keyValuePair in NKCMiscSortSystem.s_dicSortCategory)
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

		// Token: 0x0600382E RID: 14382 RVA: 0x00122654 File Offset: 0x00120854
		public static List<NKCMiscSortSystem.eSortOption> ChangeAscend(List<NKCMiscSortSystem.eSortOption> targetList)
		{
			List<NKCMiscSortSystem.eSortOption> list = new List<NKCMiscSortSystem.eSortOption>(targetList);
			if (list == null || list.Count == 0)
			{
				return list;
			}
			list[0] = NKCMiscSortSystem.GetInvertedAscendOption(list[0]);
			return list;
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x00122689 File Offset: 0x00120889
		public static HashSet<NKCMiscSortSystem.eFilterCategory> GetDefaultFilterCategory()
		{
			return NKCMiscSortSystem.setDefaultMiscFilterCategory;
		}

		// Token: 0x06003830 RID: 14384 RVA: 0x00122690 File Offset: 0x00120890
		public static HashSet<NKCMiscSortSystem.eFilterCategory> GetDefaultInteriorFilterCategory()
		{
			return NKCMiscSortSystem.setDefaultInteriorFilterCategory;
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x00122697 File Offset: 0x00120897
		public static HashSet<NKCMiscSortSystem.eSortCategory> GetDefaultSortCategory()
		{
			return NKCMiscSortSystem.setDefaultMiscSortCategory;
		}

		// Token: 0x06003832 RID: 14386 RVA: 0x0012269E File Offset: 0x0012089E
		public static HashSet<NKCMiscSortSystem.eSortCategory> GetDefaultInteriorSortCategory()
		{
			return NKCMiscSortSystem.setDefaultInteriorSortCategory;
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x001226A5 File Offset: 0x001208A5
		public static List<NKCMiscSortSystem.eSortOption> GetDefaultSortList()
		{
			return NKCMiscSortSystem.DEFAULT_MISC_SORT_OPTION_LIST;
		}

		// Token: 0x06003834 RID: 14388 RVA: 0x001226AC File Offset: 0x001208AC
		public static List<NKCMiscSortSystem.eSortOption> GetDefaultInteriorSortList()
		{
			return NKCMiscSortSystem.DEFAULT_INTERIOR_SORT_OPTION_LIST;
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06003835 RID: 14389 RVA: 0x001226B3 File Offset: 0x001208B3
		// (set) Token: 0x06003836 RID: 14390 RVA: 0x001226C0 File Offset: 0x001208C0
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

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06003837 RID: 14391 RVA: 0x001226CE File Offset: 0x001208CE
		public NKCMiscSortSystem.MiscListOptions Options
		{
			get
			{
				return this.m_Options;
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06003838 RID: 14392 RVA: 0x001226D8 File Offset: 0x001208D8
		public List<NKMItemMiscTemplet> SortedMiscList
		{
			get
			{
				if (this.m_lstCurrentMiscList == null)
				{
					if (this.m_Options.setFilterOption == null)
					{
						this.m_Options.setFilterOption = new HashSet<NKCMiscSortSystem.eFilterOption>();
						this.FilterList(this.m_Options.setFilterOption);
					}
					else
					{
						this.FilterList(this.m_Options.setFilterOption);
					}
				}
				return this.m_lstCurrentMiscList;
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06003839 RID: 14393 RVA: 0x00122734 File Offset: 0x00120934
		public HashSet<NKCMiscSortSystem.eFilterOption> OnlyIncludeFilterSet
		{
			get
			{
				return this.m_Options.setOnlyIncludeFilterOption;
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x0600383A RID: 14394 RVA: 0x00122741 File Offset: 0x00120941
		// (set) Token: 0x0600383B RID: 14395 RVA: 0x0012274E File Offset: 0x0012094E
		public HashSet<NKCMiscSortSystem.eFilterOption> FilterSet
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

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x0600383C RID: 14396 RVA: 0x00122757 File Offset: 0x00120957
		// (set) Token: 0x0600383D RID: 14397 RVA: 0x00122764 File Offset: 0x00120964
		public List<NKCMiscSortSystem.eSortOption> lstSortOption
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

		// Token: 0x0600383E RID: 14398 RVA: 0x0012276E File Offset: 0x0012096E
		protected NKCMiscSortSystem()
		{
		}

		// Token: 0x0600383F RID: 14399 RVA: 0x00122776 File Offset: 0x00120976
		public NKCMiscSortSystem(NKMUserData userData, IEnumerable<NKMItemMiscTemplet> lstTargetMiscs, NKCMiscSortSystem.MiscListOptions options)
		{
			this.m_Options = options;
			this.m_dicAllMiscList = this.BuildFullMiscList(userData, lstTargetMiscs, options);
		}

		// Token: 0x06003840 RID: 14400 RVA: 0x00122794 File Offset: 0x00120994
		public void BuildFilterAndSortedList(HashSet<NKCMiscSortSystem.eFilterOption> setfilterType, List<NKCMiscSortSystem.eSortOption> lstSortOption)
		{
			this.m_Options.setFilterOption = setfilterType;
			this.m_Options.lstSortOption = lstSortOption;
			this.FilterList(setfilterType);
		}

		// Token: 0x06003841 RID: 14401 RVA: 0x001227B8 File Offset: 0x001209B8
		private Dictionary<int, NKMItemMiscTemplet> BuildFullMiscList(NKMUserData userData, IEnumerable<NKMItemMiscTemplet> lstTargetMiscs, NKCMiscSortSystem.MiscListOptions options)
		{
			Dictionary<int, NKMItemMiscTemplet> dictionary = new Dictionary<int, NKMItemMiscTemplet>();
			HashSet<int> setOnlyIncludeMiscID = options.setOnlyIncludeMiscID;
			HashSet<int> setExcludeMiscID = options.setExcludeMiscID;
			foreach (NKMItemMiscTemplet nkmitemMiscTemplet in lstTargetMiscs)
			{
				if ((options.AdditionalExcludeFilterFunc == null || options.AdditionalExcludeFilterFunc(nkmitemMiscTemplet)) && (options.setExcludeMiscID == null || !options.setExcludeMiscID.Contains(nkmitemMiscTemplet.m_ItemMiscID)) && (setOnlyIncludeMiscID == null || setOnlyIncludeMiscID.Contains(nkmitemMiscTemplet.m_ItemMiscID)) && (setExcludeMiscID == null || !setExcludeMiscID.Contains(nkmitemMiscTemplet.m_ItemMiscID)) && (options.setOnlyIncludeFilterOption == null || this.CheckFilter(nkmitemMiscTemplet, options.setOnlyIncludeFilterOption)))
				{
					dictionary.Add(nkmitemMiscTemplet.m_ItemMiscID, nkmitemMiscTemplet);
				}
			}
			return dictionary;
		}

		// Token: 0x06003842 RID: 14402 RVA: 0x00122894 File Offset: 0x00120A94
		protected bool FilterData(NKMItemMiscTemplet miscData, List<HashSet<NKCMiscSortSystem.eFilterOption>> setFilter)
		{
			if (setFilter == null || setFilter.Count == 0)
			{
				return true;
			}
			for (int i = 0; i < setFilter.Count; i++)
			{
				if (!this.CheckFilter(miscData, setFilter[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003843 RID: 14403 RVA: 0x001228D4 File Offset: 0x00120AD4
		private bool CheckFilter(NKMItemMiscTemplet miscTmplet, HashSet<NKCMiscSortSystem.eFilterOption> setFilter)
		{
			foreach (NKCMiscSortSystem.eFilterOption filterOption in setFilter)
			{
				if (this.CheckFilter(miscTmplet, filterOption))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003844 RID: 14404 RVA: 0x0012292C File Offset: 0x00120B2C
		private bool CheckFilter(NKMItemMiscTemplet miscTemplet, NKCMiscSortSystem.eFilterOption filterOption)
		{
			switch (filterOption)
			{
			case NKCMiscSortSystem.eFilterOption.Nothing:
				return false;
			case NKCMiscSortSystem.eFilterOption.Everything:
				return true;
			case NKCMiscSortSystem.eFilterOption.InteriorTarget_Floor:
			{
				NKMOfficeInteriorTemplet nkmofficeInteriorTemplet = NKMOfficeInteriorTemplet.Find(miscTemplet.m_ItemMiscID);
				return nkmofficeInteriorTemplet != null && nkmofficeInteriorTemplet.Target == InteriorTarget.Floor;
			}
			case NKCMiscSortSystem.eFilterOption.InteriorTarget_Tile:
			{
				NKMOfficeInteriorTemplet nkmofficeInteriorTemplet2 = NKMOfficeInteriorTemplet.Find(miscTemplet.m_ItemMiscID);
				return nkmofficeInteriorTemplet2 != null && nkmofficeInteriorTemplet2.Target == InteriorTarget.Tile;
			}
			case NKCMiscSortSystem.eFilterOption.InteriorTarget_Wall:
			{
				NKMOfficeInteriorTemplet nkmofficeInteriorTemplet3 = NKMOfficeInteriorTemplet.Find(miscTemplet.m_ItemMiscID);
				return nkmofficeInteriorTemplet3 != null && nkmofficeInteriorTemplet3.Target == InteriorTarget.Wall;
			}
			case NKCMiscSortSystem.eFilterOption.InteriorTarget_Background:
			{
				NKMOfficeInteriorTemplet nkmofficeInteriorTemplet4 = NKMOfficeInteriorTemplet.Find(miscTemplet.m_ItemMiscID);
				return nkmofficeInteriorTemplet4 != null && nkmofficeInteriorTemplet4.Target == InteriorTarget.Background;
			}
			case NKCMiscSortSystem.eFilterOption.InteriorCategory_DECO:
			{
				NKMOfficeInteriorTemplet nkmofficeInteriorTemplet5 = NKMOfficeInteriorTemplet.Find(miscTemplet.m_ItemMiscID);
				return nkmofficeInteriorTemplet5 != null && nkmofficeInteriorTemplet5.InteriorCategory == InteriorCategory.DECO;
			}
			case NKCMiscSortSystem.eFilterOption.InteriorCategory_FURNITURE:
			{
				NKMOfficeInteriorTemplet nkmofficeInteriorTemplet6 = NKMOfficeInteriorTemplet.Find(miscTemplet.m_ItemMiscID);
				return nkmofficeInteriorTemplet6 != null && nkmofficeInteriorTemplet6.InteriorCategory == InteriorCategory.FURNITURE;
			}
			case NKCMiscSortSystem.eFilterOption.InteriorCanPlace:
				return NKCScenManager.CurrentUserData().OfficeData.GetFreeInteriorCount(miscTemplet.m_ItemMiscID) > 0L;
			case NKCMiscSortSystem.eFilterOption.InteriorCannotPlace:
				return NKCScenManager.CurrentUserData().OfficeData.GetFreeInteriorCount(miscTemplet.m_ItemMiscID) <= 0L;
			case NKCMiscSortSystem.eFilterOption.Theme:
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
				NKMOfficeInteriorTemplet nkmofficeInteriorTemplet7 = NKMOfficeInteriorTemplet.Find(miscTemplet.m_ItemMiscID);
				return nkmofficeInteriorTemplet7 != null && nkcthemeGroupTemplet.GroupID.Contains(nkmofficeInteriorTemplet7.GroupID);
			}
			case NKCMiscSortSystem.eFilterOption.Have:
				return NKCScenManager.CurrentUserData().OfficeData.GetInteriorCount(miscTemplet.m_ItemMiscID) > 0L;
			case NKCMiscSortSystem.eFilterOption.NotHave:
				return NKCScenManager.CurrentUserData().OfficeData.GetInteriorCount(miscTemplet.m_ItemMiscID) <= 0L;
			case NKCMiscSortSystem.eFilterOption.Tier_SSR:
				return miscTemplet.m_NKM_ITEM_GRADE == NKM_ITEM_GRADE.NIG_SSR;
			case NKCMiscSortSystem.eFilterOption.Tier_SR:
				return miscTemplet.m_NKM_ITEM_GRADE == NKM_ITEM_GRADE.NIG_SR;
			case NKCMiscSortSystem.eFilterOption.Tier_R:
				return miscTemplet.m_NKM_ITEM_GRADE == NKM_ITEM_GRADE.NIG_R;
			case NKCMiscSortSystem.eFilterOption.Tier_N:
				return miscTemplet.m_NKM_ITEM_GRADE == NKM_ITEM_GRADE.NIG_N;
			default:
				return false;
			}
		}

		// Token: 0x06003845 RID: 14405 RVA: 0x00122B38 File Offset: 0x00120D38
		public void FilterList(HashSet<NKCMiscSortSystem.eFilterOption> setFilter)
		{
			if (setFilter == null)
			{
				setFilter = new HashSet<NKCMiscSortSystem.eFilterOption>();
			}
			this.m_Options.setFilterOption = setFilter;
			if (this.m_lstCurrentMiscList == null)
			{
				this.m_lstCurrentMiscList = new List<NKMItemMiscTemplet>();
			}
			this.m_lstCurrentMiscList.Clear();
			List<HashSet<NKCMiscSortSystem.eFilterOption>> setFilter2 = new List<HashSet<NKCMiscSortSystem.eFilterOption>>();
			this.SetFilterCategory(setFilter, ref setFilter2);
			foreach (KeyValuePair<int, NKMItemMiscTemplet> keyValuePair in this.m_dicAllMiscList)
			{
				NKMItemMiscTemplet value = keyValuePair.Value;
				if (this.FilterData(value, setFilter2))
				{
					this.m_lstCurrentMiscList.Add(value);
				}
			}
			if (this.m_Options.lstSortOption != null)
			{
				this.SortList(this.m_Options.lstSortOption, true);
				return;
			}
			this.m_Options.lstSortOption = new List<NKCMiscSortSystem.eSortOption>();
			this.SortList(this.m_Options.lstSortOption, true);
		}

		// Token: 0x06003846 RID: 14406 RVA: 0x00122C28 File Offset: 0x00120E28
		private void SetFilterCategory(HashSet<NKCMiscSortSystem.eFilterOption> setFilter, ref List<HashSet<NKCMiscSortSystem.eFilterOption>> needFilterSet)
		{
			if (setFilter.Count == 0)
			{
				return;
			}
			for (int i = 0; i < NKCMiscSortSystem.m_lstFilterCategory.Count; i++)
			{
				HashSet<NKCMiscSortSystem.eFilterOption> hashSet = new HashSet<NKCMiscSortSystem.eFilterOption>();
				foreach (NKCMiscSortSystem.eFilterOption item in setFilter)
				{
					hashSet.Add(item);
				}
				hashSet.IntersectWith(NKCMiscSortSystem.m_lstFilterCategory[i]);
				if (hashSet.Count > 0)
				{
					needFilterSet.Add(hashSet);
				}
			}
		}

		// Token: 0x06003847 RID: 14407 RVA: 0x00122CC0 File Offset: 0x00120EC0
		public void SortList(List<NKCMiscSortSystem.eSortOption> lstSortOption, bool bForce = false)
		{
			if (this.m_lstCurrentMiscList != null)
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
				this.SortMiscDataList(ref this.m_lstCurrentMiscList, lstSortOption);
				this.m_Options.lstSortOption = lstSortOption;
				return;
			}
			this.m_Options.lstSortOption = lstSortOption;
			if (this.m_Options.setFilterOption != null)
			{
				this.FilterList(this.m_Options.setFilterOption);
				return;
			}
			this.m_Options.setFilterOption = new HashSet<NKCMiscSortSystem.eFilterOption>();
			this.FilterList(this.m_Options.setFilterOption);
		}

		// Token: 0x06003848 RID: 14408 RVA: 0x00122D8B File Offset: 0x00120F8B
		public List<NKMItemMiscTemplet> GetCurrentMiscList()
		{
			return this.m_lstCurrentMiscList;
		}

		// Token: 0x06003849 RID: 14409 RVA: 0x00122D94 File Offset: 0x00120F94
		private void SortMiscDataList(ref List<NKMItemMiscTemplet> lstMiscTemplet, List<NKCMiscSortSystem.eSortOption> lstSortOption)
		{
			NKCMiscSortSystem.NKCDataComparerer<NKMItemMiscTemplet> nkcdataComparerer = new NKCMiscSortSystem.NKCDataComparerer<NKMItemMiscTemplet>(Array.Empty<NKCMiscSortSystem.NKCDataComparerer<NKMItemMiscTemplet>.CompareFunc>());
			HashSet<NKCMiscSortSystem.eSortCategory> hashSet = new HashSet<NKCMiscSortSystem.eSortCategory>();
			if (this.m_Options.PreemptiveSortFunc != null)
			{
				nkcdataComparerer.AddFunc(this.m_Options.PreemptiveSortFunc);
			}
			foreach (NKCMiscSortSystem.eSortOption eSortOption in lstSortOption)
			{
				if (eSortOption != NKCMiscSortSystem.eSortOption.None)
				{
					NKCMiscSortSystem.NKCDataComparerer<NKMItemMiscTemplet>.CompareFunc dataComparer = this.GetDataComparer(eSortOption);
					if (dataComparer != null)
					{
						nkcdataComparerer.AddFunc(dataComparer);
						hashSet.Add(NKCMiscSortSystem.GetSortCategoryFromOption(eSortOption));
					}
				}
			}
			if (this.m_Options.lstDefaultSortOption != null)
			{
				foreach (NKCMiscSortSystem.eSortOption eSortOption2 in this.m_Options.lstDefaultSortOption)
				{
					NKCMiscSortSystem.eSortCategory sortCategoryFromOption = NKCMiscSortSystem.GetSortCategoryFromOption(eSortOption2);
					if (!hashSet.Contains(sortCategoryFromOption))
					{
						nkcdataComparerer.AddFunc(this.GetDataComparer(eSortOption2));
						hashSet.Add(sortCategoryFromOption);
					}
				}
			}
			if (!hashSet.Contains(NKCMiscSortSystem.eSortCategory.ID))
			{
				nkcdataComparerer.AddFunc(new NKCMiscSortSystem.NKCDataComparerer<NKMItemMiscTemplet>.CompareFunc(this.CompareByIDAscending));
			}
			lstMiscTemplet.Sort(nkcdataComparerer);
		}

		// Token: 0x0600384A RID: 14410 RVA: 0x00122ECC File Offset: 0x001210CC
		private NKCMiscSortSystem.NKCDataComparerer<NKMItemMiscTemplet>.CompareFunc GetDataComparer(NKCMiscSortSystem.eSortOption sortOption)
		{
			switch (sortOption)
			{
			case NKCMiscSortSystem.eSortOption.ID_First:
				return new NKCMiscSortSystem.NKCDataComparerer<NKMItemMiscTemplet>.CompareFunc(this.CompareByIDAscending);
			case NKCMiscSortSystem.eSortOption.ID_Last:
				return new NKCMiscSortSystem.NKCDataComparerer<NKMItemMiscTemplet>.CompareFunc(this.CompareByIDDescending);
			case NKCMiscSortSystem.eSortOption.Point_High:
				return new NKCMiscSortSystem.NKCDataComparerer<NKMItemMiscTemplet>.CompareFunc(this.CompareByPointDescending);
			case NKCMiscSortSystem.eSortOption.Point_Low:
				return new NKCMiscSortSystem.NKCDataComparerer<NKMItemMiscTemplet>.CompareFunc(this.CompareByPointAscending);
			default:
				return new NKCMiscSortSystem.NKCDataComparerer<NKMItemMiscTemplet>.CompareFunc(NKCMiscSortSystem.CompareByRarityDescending);
			case NKCMiscSortSystem.eSortOption.Rarity_Low:
				return new NKCMiscSortSystem.NKCDataComparerer<NKMItemMiscTemplet>.CompareFunc(NKCMiscSortSystem.CompareByRarityAscending);
			case NKCMiscSortSystem.eSortOption.CanPlace:
				return new NKCMiscSortSystem.NKCDataComparerer<NKMItemMiscTemplet>.CompareFunc(this.CompareByCanPlace);
			case NKCMiscSortSystem.eSortOption.CannotPlace:
				return new NKCMiscSortSystem.NKCDataComparerer<NKMItemMiscTemplet>.CompareFunc(this.CompareByCannotPlace);
			case NKCMiscSortSystem.eSortOption.CustomAscend1:
			case NKCMiscSortSystem.eSortOption.CustomAscend2:
			case NKCMiscSortSystem.eSortOption.CustomAscend3:
				if (this.m_Options.lstCustomSortFunc.ContainsKey(NKCMiscSortSystem.GetSortCategoryFromOption(sortOption)))
				{
					return (NKMItemMiscTemplet a, NKMItemMiscTemplet b) => this.m_Options.lstCustomSortFunc[NKCMiscSortSystem.GetSortCategoryFromOption(sortOption)].Value(b, a);
				}
				return null;
			case NKCMiscSortSystem.eSortOption.CustomDescend1:
			case NKCMiscSortSystem.eSortOption.CustomDescend2:
			case NKCMiscSortSystem.eSortOption.CustomDescend3:
				if (this.m_Options.lstCustomSortFunc.ContainsKey(NKCMiscSortSystem.GetSortCategoryFromOption(sortOption)))
				{
					return this.m_Options.lstCustomSortFunc[NKCMiscSortSystem.GetSortCategoryFromOption(sortOption)].Value;
				}
				return null;
			}
		}

		// Token: 0x0600384B RID: 14411 RVA: 0x0012300C File Offset: 0x0012120C
		public static int CompareByRarityAscending(NKMItemMiscTemplet lhs, NKMItemMiscTemplet rhs)
		{
			NKMItemMiscTemplet nkmitemMiscTemplet = NKMItemMiscTemplet.Find(lhs.m_ItemMiscID);
			NKMItemMiscTemplet nkmitemMiscTemplet2 = NKMItemMiscTemplet.Find(rhs.m_ItemMiscID);
			return nkmitemMiscTemplet.m_NKM_ITEM_GRADE.CompareTo(nkmitemMiscTemplet2.m_NKM_ITEM_GRADE);
		}

		// Token: 0x0600384C RID: 14412 RVA: 0x0012304C File Offset: 0x0012124C
		public static int CompareByRarityDescending(NKMItemMiscTemplet lhs, NKMItemMiscTemplet rhs)
		{
			NKMItemMiscTemplet nkmitemMiscTemplet = NKMItemMiscTemplet.Find(lhs.m_ItemMiscID);
			return NKMItemMiscTemplet.Find(rhs.m_ItemMiscID).m_NKM_ITEM_GRADE.CompareTo(nkmitemMiscTemplet.m_NKM_ITEM_GRADE);
		}

		// Token: 0x0600384D RID: 14413 RVA: 0x0012308B File Offset: 0x0012128B
		private int CompareByIDAscending(NKMItemMiscTemplet lhs, NKMItemMiscTemplet rhs)
		{
			return lhs.m_ItemMiscID.CompareTo(rhs.m_ItemMiscID);
		}

		// Token: 0x0600384E RID: 14414 RVA: 0x0012309E File Offset: 0x0012129E
		private int CompareByIDDescending(NKMItemMiscTemplet lhs, NKMItemMiscTemplet rhs)
		{
			return rhs.m_ItemMiscID.CompareTo(lhs.m_ItemMiscID);
		}

		// Token: 0x0600384F RID: 14415 RVA: 0x001230B4 File Offset: 0x001212B4
		private int CompareByPointAscending(NKMItemMiscTemplet lhs, NKMItemMiscTemplet rhs)
		{
			NKMOfficeInteriorTemplet nkmofficeInteriorTemplet = NKMOfficeInteriorTemplet.Find(lhs.m_ItemMiscID);
			NKMOfficeInteriorTemplet nkmofficeInteriorTemplet2 = NKMOfficeInteriorTemplet.Find(rhs.m_ItemMiscID);
			return nkmofficeInteriorTemplet.InteriorScore.CompareTo(nkmofficeInteriorTemplet2.InteriorScore);
		}

		// Token: 0x06003850 RID: 14416 RVA: 0x001230EC File Offset: 0x001212EC
		private int CompareByPointDescending(NKMItemMiscTemplet lhs, NKMItemMiscTemplet rhs)
		{
			NKMOfficeInteriorTemplet nkmofficeInteriorTemplet = NKMOfficeInteriorTemplet.Find(lhs.m_ItemMiscID);
			return NKMOfficeInteriorTemplet.Find(rhs.m_ItemMiscID).InteriorScore.CompareTo(nkmofficeInteriorTemplet.InteriorScore);
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x00123124 File Offset: 0x00121324
		private int CompareByCanPlace(NKMItemMiscTemplet lhs, NKMItemMiscTemplet rhs)
		{
			long freeInteriorCount = NKCScenManager.CurrentUserData().OfficeData.GetFreeInteriorCount(lhs.m_ItemMiscID);
			return NKCScenManager.CurrentUserData().OfficeData.GetFreeInteriorCount(rhs.m_ItemMiscID).CompareTo(freeInteriorCount);
		}

		// Token: 0x06003852 RID: 14418 RVA: 0x00123168 File Offset: 0x00121368
		private int CompareByCannotPlace(NKMItemMiscTemplet lhs, NKMItemMiscTemplet rhs)
		{
			long freeInteriorCount = NKCScenManager.CurrentUserData().OfficeData.GetFreeInteriorCount(lhs.m_ItemMiscID);
			long freeInteriorCount2 = NKCScenManager.CurrentUserData().OfficeData.GetFreeInteriorCount(rhs.m_ItemMiscID);
			return freeInteriorCount.CompareTo(freeInteriorCount2);
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x001231A9 File Offset: 0x001213A9
		public static string GetSortName(NKCMiscSortSystem.eSortOption sortOption)
		{
			return NKCMiscSortSystem.GetSortName(NKCMiscSortSystem.GetSortCategoryFromOption(sortOption));
		}

		// Token: 0x06003854 RID: 14420 RVA: 0x001231B6 File Offset: 0x001213B6
		public static string GetSortName(NKCMiscSortSystem.eSortCategory sortCategory)
		{
			switch (sortCategory)
			{
			case NKCMiscSortSystem.eSortCategory.ID:
				return NKCUtilString.GET_STRING_SORT_IDX;
			case NKCMiscSortSystem.eSortCategory.Point:
				return NKCUtilString.GET_STRING_SORT_INTERIOR_POINT;
			default:
				return NKCUtilString.GET_STRING_SORT_RARITY;
			case NKCMiscSortSystem.eSortCategory.CanPlace:
				return NKCUtilString.GET_STRING_SORT_PLACE_TYPE;
			}
		}

		// Token: 0x040034A9 RID: 13481
		private static readonly Dictionary<NKCMiscSortSystem.eSortCategory, Tuple<NKCMiscSortSystem.eSortOption, NKCMiscSortSystem.eSortOption>> s_dicSortCategory = new Dictionary<NKCMiscSortSystem.eSortCategory, Tuple<NKCMiscSortSystem.eSortOption, NKCMiscSortSystem.eSortOption>>
		{
			{
				NKCMiscSortSystem.eSortCategory.None,
				new Tuple<NKCMiscSortSystem.eSortOption, NKCMiscSortSystem.eSortOption>(NKCMiscSortSystem.eSortOption.None, NKCMiscSortSystem.eSortOption.None)
			},
			{
				NKCMiscSortSystem.eSortCategory.ID,
				new Tuple<NKCMiscSortSystem.eSortOption, NKCMiscSortSystem.eSortOption>(NKCMiscSortSystem.eSortOption.ID_First, NKCMiscSortSystem.eSortOption.ID_Last)
			},
			{
				NKCMiscSortSystem.eSortCategory.Point,
				new Tuple<NKCMiscSortSystem.eSortOption, NKCMiscSortSystem.eSortOption>(NKCMiscSortSystem.eSortOption.Point_Low, NKCMiscSortSystem.eSortOption.Point_High)
			},
			{
				NKCMiscSortSystem.eSortCategory.Rarity,
				new Tuple<NKCMiscSortSystem.eSortOption, NKCMiscSortSystem.eSortOption>(NKCMiscSortSystem.eSortOption.Rarity_Low, NKCMiscSortSystem.eSortOption.Rarity_High)
			},
			{
				NKCMiscSortSystem.eSortCategory.CanPlace,
				new Tuple<NKCMiscSortSystem.eSortOption, NKCMiscSortSystem.eSortOption>(NKCMiscSortSystem.eSortOption.CannotPlace, NKCMiscSortSystem.eSortOption.CanPlace)
			},
			{
				NKCMiscSortSystem.eSortCategory.Custom1,
				new Tuple<NKCMiscSortSystem.eSortOption, NKCMiscSortSystem.eSortOption>(NKCMiscSortSystem.eSortOption.CustomAscend1, NKCMiscSortSystem.eSortOption.CustomDescend1)
			},
			{
				NKCMiscSortSystem.eSortCategory.Custom2,
				new Tuple<NKCMiscSortSystem.eSortOption, NKCMiscSortSystem.eSortOption>(NKCMiscSortSystem.eSortOption.CustomAscend2, NKCMiscSortSystem.eSortOption.CustomDescend2)
			},
			{
				NKCMiscSortSystem.eSortCategory.Custom3,
				new Tuple<NKCMiscSortSystem.eSortOption, NKCMiscSortSystem.eSortOption>(NKCMiscSortSystem.eSortOption.CustomAscend3, NKCMiscSortSystem.eSortOption.CustomDescend3)
			}
		};

		// Token: 0x040034AA RID: 13482
		private static readonly List<NKCMiscSortSystem.eSortOption> DEFAULT_MISC_SORT_OPTION_LIST = new List<NKCMiscSortSystem.eSortOption>
		{
			NKCMiscSortSystem.eSortOption.Rarity_High,
			NKCMiscSortSystem.eSortOption.ID_First
		};

		// Token: 0x040034AB RID: 13483
		private static readonly List<NKCMiscSortSystem.eSortOption> DEFAULT_INTERIOR_SORT_OPTION_LIST = new List<NKCMiscSortSystem.eSortOption>
		{
			NKCMiscSortSystem.eSortOption.Rarity_High,
			NKCMiscSortSystem.eSortOption.Point_High,
			NKCMiscSortSystem.eSortOption.ID_First
		};

		// Token: 0x040034AC RID: 13484
		private static readonly HashSet<NKCMiscSortSystem.eFilterOption> m_setFilterCategory_InteriorTarget = new HashSet<NKCMiscSortSystem.eFilterOption>
		{
			NKCMiscSortSystem.eFilterOption.InteriorTarget_Floor,
			NKCMiscSortSystem.eFilterOption.InteriorTarget_Tile,
			NKCMiscSortSystem.eFilterOption.InteriorTarget_Wall,
			NKCMiscSortSystem.eFilterOption.InteriorTarget_Background
		};

		// Token: 0x040034AD RID: 13485
		private static readonly HashSet<NKCMiscSortSystem.eFilterOption> m_setFilterCategory_InteriorCategory = new HashSet<NKCMiscSortSystem.eFilterOption>
		{
			NKCMiscSortSystem.eFilterOption.InteriorCategory_DECO,
			NKCMiscSortSystem.eFilterOption.InteriorCategory_FURNITURE
		};

		// Token: 0x040034AE RID: 13486
		private static readonly HashSet<NKCMiscSortSystem.eFilterOption> m_setFilterCategory_InteriorCanPlace = new HashSet<NKCMiscSortSystem.eFilterOption>
		{
			NKCMiscSortSystem.eFilterOption.InteriorCanPlace,
			NKCMiscSortSystem.eFilterOption.InteriorCannotPlace
		};

		// Token: 0x040034AF RID: 13487
		private static readonly HashSet<NKCMiscSortSystem.eFilterOption> m_setFilterCategory_Have = new HashSet<NKCMiscSortSystem.eFilterOption>
		{
			NKCMiscSortSystem.eFilterOption.Have,
			NKCMiscSortSystem.eFilterOption.NotHave
		};

		// Token: 0x040034B0 RID: 13488
		private static readonly HashSet<NKCMiscSortSystem.eFilterOption> m_setFilterCategory_Rarity = new HashSet<NKCMiscSortSystem.eFilterOption>
		{
			NKCMiscSortSystem.eFilterOption.Tier_SSR,
			NKCMiscSortSystem.eFilterOption.Tier_SR,
			NKCMiscSortSystem.eFilterOption.Tier_R,
			NKCMiscSortSystem.eFilterOption.Tier_N
		};

		// Token: 0x040034B1 RID: 13489
		private static readonly HashSet<NKCMiscSortSystem.eFilterOption> m_setFilterCategory_Theme = new HashSet<NKCMiscSortSystem.eFilterOption>
		{
			NKCMiscSortSystem.eFilterOption.Theme
		};

		// Token: 0x040034B2 RID: 13490
		private static readonly List<HashSet<NKCMiscSortSystem.eFilterOption>> m_lstFilterCategory = new List<HashSet<NKCMiscSortSystem.eFilterOption>>
		{
			NKCMiscSortSystem.m_setFilterCategory_InteriorCategory,
			NKCMiscSortSystem.m_setFilterCategory_InteriorTarget,
			NKCMiscSortSystem.m_setFilterCategory_InteriorCanPlace,
			NKCMiscSortSystem.m_setFilterCategory_Have,
			NKCMiscSortSystem.m_setFilterCategory_Rarity,
			NKCMiscSortSystem.m_setFilterCategory_Theme
		};

		// Token: 0x040034B3 RID: 13491
		private static readonly HashSet<NKCMiscSortSystem.eFilterCategory> setDefaultMiscFilterCategory = new HashSet<NKCMiscSortSystem.eFilterCategory>
		{
			NKCMiscSortSystem.eFilterCategory.Tier
		};

		// Token: 0x040034B4 RID: 13492
		private static readonly HashSet<NKCMiscSortSystem.eFilterCategory> setDefaultInteriorFilterCategory = new HashSet<NKCMiscSortSystem.eFilterCategory>
		{
			NKCMiscSortSystem.eFilterCategory.InteriorCanPlace,
			NKCMiscSortSystem.eFilterCategory.InteriorCategory,
			NKCMiscSortSystem.eFilterCategory.InteriorTarget,
			NKCMiscSortSystem.eFilterCategory.Tier,
			NKCMiscSortSystem.eFilterCategory.Theme
		};

		// Token: 0x040034B5 RID: 13493
		private static readonly HashSet<NKCMiscSortSystem.eSortCategory> setDefaultMiscSortCategory = new HashSet<NKCMiscSortSystem.eSortCategory>
		{
			NKCMiscSortSystem.eSortCategory.Rarity,
			NKCMiscSortSystem.eSortCategory.ID
		};

		// Token: 0x040034B6 RID: 13494
		private static readonly HashSet<NKCMiscSortSystem.eSortCategory> setDefaultInteriorSortCategory = new HashSet<NKCMiscSortSystem.eSortCategory>
		{
			NKCMiscSortSystem.eSortCategory.Rarity,
			NKCMiscSortSystem.eSortCategory.Point,
			NKCMiscSortSystem.eSortCategory.CanPlace,
			NKCMiscSortSystem.eSortCategory.ID
		};

		// Token: 0x040034B7 RID: 13495
		protected NKCMiscSortSystem.MiscListOptions m_Options;

		// Token: 0x040034B8 RID: 13496
		protected Dictionary<int, NKMItemMiscTemplet> m_dicAllMiscList;

		// Token: 0x040034B9 RID: 13497
		protected List<NKMItemMiscTemplet> m_lstCurrentMiscList;

		// Token: 0x0200136B RID: 4971
		public enum eFilterCategory
		{
			// Token: 0x0400997E RID: 39294
			InteriorTarget,
			// Token: 0x0400997F RID: 39295
			InteriorCategory,
			// Token: 0x04009980 RID: 39296
			InteriorCanPlace,
			// Token: 0x04009981 RID: 39297
			Theme,
			// Token: 0x04009982 RID: 39298
			Have,
			// Token: 0x04009983 RID: 39299
			Tier
		}

		// Token: 0x0200136C RID: 4972
		public enum eFilterOption
		{
			// Token: 0x04009985 RID: 39301
			Nothing,
			// Token: 0x04009986 RID: 39302
			Everything,
			// Token: 0x04009987 RID: 39303
			InteriorTarget_Floor,
			// Token: 0x04009988 RID: 39304
			InteriorTarget_Tile,
			// Token: 0x04009989 RID: 39305
			InteriorTarget_Wall,
			// Token: 0x0400998A RID: 39306
			InteriorTarget_Background,
			// Token: 0x0400998B RID: 39307
			InteriorCategory_DECO,
			// Token: 0x0400998C RID: 39308
			InteriorCategory_FURNITURE,
			// Token: 0x0400998D RID: 39309
			InteriorCanPlace,
			// Token: 0x0400998E RID: 39310
			InteriorCannotPlace,
			// Token: 0x0400998F RID: 39311
			Theme,
			// Token: 0x04009990 RID: 39312
			Have,
			// Token: 0x04009991 RID: 39313
			NotHave,
			// Token: 0x04009992 RID: 39314
			Tier_SSR,
			// Token: 0x04009993 RID: 39315
			Tier_SR,
			// Token: 0x04009994 RID: 39316
			Tier_R,
			// Token: 0x04009995 RID: 39317
			Tier_N
		}

		// Token: 0x0200136D RID: 4973
		public enum eSortCategory
		{
			// Token: 0x04009997 RID: 39319
			None,
			// Token: 0x04009998 RID: 39320
			ID,
			// Token: 0x04009999 RID: 39321
			Point,
			// Token: 0x0400999A RID: 39322
			Rarity,
			// Token: 0x0400999B RID: 39323
			CanPlace,
			// Token: 0x0400999C RID: 39324
			Custom1,
			// Token: 0x0400999D RID: 39325
			Custom2,
			// Token: 0x0400999E RID: 39326
			Custom3
		}

		// Token: 0x0200136E RID: 4974
		public enum eSortOption
		{
			// Token: 0x040099A0 RID: 39328
			None,
			// Token: 0x040099A1 RID: 39329
			ID_First,
			// Token: 0x040099A2 RID: 39330
			ID_Last,
			// Token: 0x040099A3 RID: 39331
			Point_High,
			// Token: 0x040099A4 RID: 39332
			Point_Low,
			// Token: 0x040099A5 RID: 39333
			Rarity_High,
			// Token: 0x040099A6 RID: 39334
			Rarity_Low,
			// Token: 0x040099A7 RID: 39335
			CanPlace,
			// Token: 0x040099A8 RID: 39336
			CannotPlace,
			// Token: 0x040099A9 RID: 39337
			CustomAscend1,
			// Token: 0x040099AA RID: 39338
			CustomDescend1,
			// Token: 0x040099AB RID: 39339
			CustomAscend2,
			// Token: 0x040099AC RID: 39340
			CustomDescend2,
			// Token: 0x040099AD RID: 39341
			CustomAscend3,
			// Token: 0x040099AE RID: 39342
			CustomDescend3
		}

		// Token: 0x0200136F RID: 4975
		public struct MiscListOptions
		{
			// Token: 0x040099AF RID: 39343
			public HashSet<int> setOnlyIncludeMiscID;

			// Token: 0x040099B0 RID: 39344
			public HashSet<int> setExcludeMiscID;

			// Token: 0x040099B1 RID: 39345
			public HashSet<NKCMiscSortSystem.eFilterOption> setOnlyIncludeFilterOption;

			// Token: 0x040099B2 RID: 39346
			public HashSet<NKCMiscSortSystem.eFilterOption> setFilterOption;

			// Token: 0x040099B3 RID: 39347
			public List<NKCMiscSortSystem.eSortOption> lstSortOption;

			// Token: 0x040099B4 RID: 39348
			public NKCMiscSortSystem.NKCDataComparerer<NKMItemMiscTemplet>.CompareFunc PreemptiveSortFunc;

			// Token: 0x040099B5 RID: 39349
			public Dictionary<NKCMiscSortSystem.eSortCategory, KeyValuePair<string, NKCMiscSortSystem.NKCDataComparerer<NKMItemMiscTemplet>.CompareFunc>> lstCustomSortFunc;

			// Token: 0x040099B6 RID: 39350
			public NKCMiscSortSystem.MiscListOptions.CustomFilterFunc AdditionalExcludeFilterFunc;

			// Token: 0x040099B7 RID: 39351
			public List<NKCMiscSortSystem.eSortOption> lstDefaultSortOption;

			// Token: 0x040099B8 RID: 39352
			public bool bPushBackUnselectable;

			// Token: 0x040099B9 RID: 39353
			public int m_filterThemeID;

			// Token: 0x02001A55 RID: 6741
			// (Invoke) Token: 0x0600BB9F RID: 48031
			public delegate bool CustomFilterFunc(NKMItemMiscTemplet miscTemplet);
		}

		// Token: 0x02001370 RID: 4976
		public class NKCDataComparerer<T> : Comparer<T>
		{
			// Token: 0x0600A5ED RID: 42477 RVA: 0x0034616C File Offset: 0x0034436C
			public NKCDataComparerer(params NKCMiscSortSystem.NKCDataComparerer<T>.CompareFunc[] comparers)
			{
				foreach (NKCMiscSortSystem.NKCDataComparerer<T>.CompareFunc item in comparers)
				{
					this.m_lstFunc.Add(item);
				}
			}

			// Token: 0x0600A5EE RID: 42478 RVA: 0x003461AA File Offset: 0x003443AA
			public void AddFunc(NKCMiscSortSystem.NKCDataComparerer<T>.CompareFunc func)
			{
				this.m_lstFunc.Add(func);
			}

			// Token: 0x0600A5EF RID: 42479 RVA: 0x003461B8 File Offset: 0x003443B8
			public override int Compare(T lhs, T rhs)
			{
				foreach (NKCMiscSortSystem.NKCDataComparerer<T>.CompareFunc compareFunc in this.m_lstFunc)
				{
					int num = compareFunc(lhs, rhs);
					if (num != 0)
					{
						return num;
					}
				}
				return 0;
			}

			// Token: 0x040099BA RID: 39354
			private List<NKCMiscSortSystem.NKCDataComparerer<T>.CompareFunc> m_lstFunc = new List<NKCMiscSortSystem.NKCDataComparerer<T>.CompareFunc>();

			// Token: 0x02001A56 RID: 6742
			// (Invoke) Token: 0x0600BBA3 RID: 48035
			public delegate int CompareFunc(T lhs, T rhs);
		}
	}
}
