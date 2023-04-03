using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;

namespace NKC
{
	// Token: 0x020006A9 RID: 1705
	public class NKCMoldSortSystem
	{
		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06003856 RID: 14422 RVA: 0x0012343C File Offset: 0x0012163C
		public static Dictionary<string, List<NKCMoldSortSystem.eSortOption>> MoldSortData
		{
			get
			{
				return NKCMoldSortSystem.m_dicMoldSort;
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06003857 RID: 14423 RVA: 0x00123443 File Offset: 0x00121643
		public static List<string> MoldFilterData
		{
			get
			{
				return NKCMoldSortSystem.m_lstMoldFilter;
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06003858 RID: 14424 RVA: 0x0012344A File Offset: 0x0012164A
		public HashSet<NKCMoldSortSystem.eFilterOption> FilterSet
		{
			get
			{
				if (this.m_Options.setFilterOption == null)
				{
					this.m_Options.setFilterOption = new HashSet<NKCMoldSortSystem.eFilterOption>();
				}
				return this.m_Options.setFilterOption;
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06003859 RID: 14425 RVA: 0x00123474 File Offset: 0x00121674
		public List<NKCMoldSortSystem.eSortOption> lstSortOption
		{
			get
			{
				return this.m_Options.lstSortOption;
			}
		}

		// Token: 0x0600385A RID: 14426 RVA: 0x00123481 File Offset: 0x00121681
		private NKCMoldSortSystem()
		{
		}

		// Token: 0x0600385B RID: 14427 RVA: 0x0012349B File Offset: 0x0012169B
		public NKCMoldSortSystem(List<NKCMoldSortSystem.eSortOption> lstMoldOption)
		{
			if (lstMoldOption != null)
			{
				this.m_Options.lstSortOption = lstMoldOption;
				return;
			}
			this.m_Options.lstSortOption = NKCMoldSortSystem.GetDefaultSortOption();
		}

		// Token: 0x0600385C RID: 14428 RVA: 0x001234D5 File Offset: 0x001216D5
		public void FilterList(HashSet<NKCMoldSortSystem.eFilterOption> setFilterOption, NKM_CRAFT_TAB_TYPE type)
		{
			if (setFilterOption == null)
			{
				return;
			}
			this.m_Options.setFilterOption = setFilterOption;
			this.Filtering(type);
			this.Sort(this.GetCurActiveOption());
		}

		// Token: 0x0600385D RID: 14429 RVA: 0x001234FC File Offset: 0x001216FC
		private NKCUnitSortSystem.NKCDataComparerer<NKMMoldItemData>.CompareFunc GetDataComparer(NKCMoldSortSystem.eSortOption sortOption)
		{
			switch (sortOption)
			{
			case NKCMoldSortSystem.eSortOption.Craftable_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMMoldItemData>.CompareFunc(this.CompareByCraftbaleDescending);
			case NKCMoldSortSystem.eSortOption.Craftable_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMMoldItemData>.CompareFunc(this.CompareByCraftbaleAscending);
			case NKCMoldSortSystem.eSortOption.Tier_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMMoldItemData>.CompareFunc(this.CompareByTierDescending);
			case NKCMoldSortSystem.eSortOption.Tier_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMMoldItemData>.CompareFunc(this.CompareByTierAscending);
			case NKCMoldSortSystem.eSortOption.Rarity_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMMoldItemData>.CompareFunc(this.CompareByRarityDescending);
			case NKCMoldSortSystem.eSortOption.Rarity_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMMoldItemData>.CompareFunc(this.CompareByRarityAscending);
			case NKCMoldSortSystem.eSortOption.HaveMold_First:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMMoldItemData>.CompareFunc(this.CompareByHaveFirst);
			case NKCMoldSortSystem.eSortOption.HaveMold_Last:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMMoldItemData>.CompareFunc(this.CompareByHaveLast);
			case NKCMoldSortSystem.eSortOption.EquipType_FIrst:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMMoldItemData>.CompareFunc(this.CompareByEquipTypeDescending);
			case NKCMoldSortSystem.eSortOption.EquipType_Last:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMMoldItemData>.CompareFunc(this.CompareByEquipTypeAscending);
			case NKCMoldSortSystem.eSortOption.UnitType_First:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMMoldItemData>.CompareFunc(this.CompareByUnitTypeFirst);
			case NKCMoldSortSystem.eSortOption.UnitType_Last:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMMoldItemData>.CompareFunc(this.CompareByUnitTypeLast);
			case NKCMoldSortSystem.eSortOption.ID_First:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMMoldItemData>.CompareFunc(this.CompareByIDFirst);
			case NKCMoldSortSystem.eSortOption.ID_Last:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMMoldItemData>.CompareFunc(this.CompareByIDLast);
			default:
				return null;
			}
		}

		// Token: 0x0600385E RID: 14430 RVA: 0x00123608 File Offset: 0x00121808
		private int CompareByCraftbaleDescending(NKMMoldItemData lhs, NKMMoldItemData rhs)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (NKCUtil.GetEquipCreatableCount(lhs, myUserData.m_InventoryData) > 0 && NKCUtil.GetEquipCreatableCount(rhs, myUserData.m_InventoryData) <= 0)
			{
				return -1;
			}
			if (NKCUtil.GetEquipCreatableCount(lhs, myUserData.m_InventoryData) <= 0 && NKCUtil.GetEquipCreatableCount(rhs, myUserData.m_InventoryData) > 0)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x0600385F RID: 14431 RVA: 0x00123664 File Offset: 0x00121864
		private int CompareByCraftbaleAscending(NKMMoldItemData lhs, NKMMoldItemData rhs)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (NKCUtil.GetEquipCreatableCount(lhs, myUserData.m_InventoryData) > 0 && NKCUtil.GetEquipCreatableCount(rhs, myUserData.m_InventoryData) <= 0)
			{
				return 1;
			}
			if (NKCUtil.GetEquipCreatableCount(lhs, myUserData.m_InventoryData) <= 0 && NKCUtil.GetEquipCreatableCount(rhs, myUserData.m_InventoryData) > 0)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06003860 RID: 14432 RVA: 0x001236C0 File Offset: 0x001218C0
		private int CompareByTierAscending(NKMMoldItemData lhs, NKMMoldItemData rhs)
		{
			NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(lhs.m_MoldID);
			NKMItemMoldTemplet itemMoldTempletByID2 = NKMItemManager.GetItemMoldTempletByID(rhs.m_MoldID);
			if (itemMoldTempletByID == null || itemMoldTempletByID2 == null)
			{
				return 0;
			}
			return itemMoldTempletByID.m_Tier.CompareTo(itemMoldTempletByID2.m_Tier);
		}

		// Token: 0x06003861 RID: 14433 RVA: 0x00123700 File Offset: 0x00121900
		private int CompareByTierDescending(NKMMoldItemData lhs, NKMMoldItemData rhs)
		{
			NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(lhs.m_MoldID);
			NKMItemMoldTemplet itemMoldTempletByID2 = NKMItemManager.GetItemMoldTempletByID(rhs.m_MoldID);
			if (itemMoldTempletByID == null || itemMoldTempletByID2 == null)
			{
				return 0;
			}
			return itemMoldTempletByID2.m_Tier.CompareTo(itemMoldTempletByID.m_Tier);
		}

		// Token: 0x06003862 RID: 14434 RVA: 0x00123740 File Offset: 0x00121940
		private int CompareByRarityAscending(NKMMoldItemData lhs, NKMMoldItemData rhs)
		{
			NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(lhs.m_MoldID);
			NKMItemMoldTemplet itemMoldTempletByID2 = NKMItemManager.GetItemMoldTempletByID(rhs.m_MoldID);
			if (itemMoldTempletByID == null || itemMoldTempletByID2 == null)
			{
				return 0;
			}
			return itemMoldTempletByID.m_Grade.CompareTo(itemMoldTempletByID2.m_Grade);
		}

		// Token: 0x06003863 RID: 14435 RVA: 0x0012378C File Offset: 0x0012198C
		private int CompareByRarityDescending(NKMMoldItemData lhs, NKMMoldItemData rhs)
		{
			NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(lhs.m_MoldID);
			NKMItemMoldTemplet itemMoldTempletByID2 = NKMItemManager.GetItemMoldTempletByID(rhs.m_MoldID);
			if (itemMoldTempletByID == null || itemMoldTempletByID2 == null)
			{
				return 0;
			}
			return itemMoldTempletByID2.m_Grade.CompareTo(itemMoldTempletByID.m_Grade);
		}

		// Token: 0x06003864 RID: 14436 RVA: 0x001237D8 File Offset: 0x001219D8
		private int CompareByEquipTypeAscending(NKMMoldItemData lhs, NKMMoldItemData rhs)
		{
			NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(lhs.m_MoldID);
			NKMItemMoldTemplet itemMoldTempletByID2 = NKMItemManager.GetItemMoldTempletByID(rhs.m_MoldID);
			if (itemMoldTempletByID == null || itemMoldTempletByID2 == null)
			{
				return 0;
			}
			return itemMoldTempletByID2.m_RewardEquipPosition.CompareTo(itemMoldTempletByID.m_RewardEquipPosition);
		}

		// Token: 0x06003865 RID: 14437 RVA: 0x00123824 File Offset: 0x00121A24
		private int CompareByEquipTypeDescending(NKMMoldItemData lhs, NKMMoldItemData rhs)
		{
			NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(lhs.m_MoldID);
			NKMItemMoldTemplet itemMoldTempletByID2 = NKMItemManager.GetItemMoldTempletByID(rhs.m_MoldID);
			if (itemMoldTempletByID == null || itemMoldTempletByID2 == null)
			{
				return 0;
			}
			return itemMoldTempletByID.m_RewardEquipPosition.CompareTo(itemMoldTempletByID2.m_RewardEquipPosition);
		}

		// Token: 0x06003866 RID: 14438 RVA: 0x00123870 File Offset: 0x00121A70
		private int CompareByHaveFirst(NKMMoldItemData lhs, NKMMoldItemData rhs)
		{
			NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(lhs.m_MoldID);
			NKMItemMoldTemplet itemMoldTempletByID2 = NKMItemManager.GetItemMoldTempletByID(rhs.m_MoldID);
			if (itemMoldTempletByID != null && itemMoldTempletByID.m_bPermanent && itemMoldTempletByID2 != null && itemMoldTempletByID2.m_bPermanent)
			{
				return 0;
			}
			if (lhs.m_Count > 0L && rhs.m_Count <= 0L)
			{
				return -1;
			}
			if (rhs.m_Count > 0L && lhs.m_Count <= 0L)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06003867 RID: 14439 RVA: 0x001238DC File Offset: 0x00121ADC
		private int CompareByHaveLast(NKMMoldItemData lhs, NKMMoldItemData rhs)
		{
			NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(lhs.m_MoldID);
			NKMItemMoldTemplet itemMoldTempletByID2 = NKMItemManager.GetItemMoldTempletByID(rhs.m_MoldID);
			if (itemMoldTempletByID != null && itemMoldTempletByID.m_bPermanent && itemMoldTempletByID2 != null && itemMoldTempletByID2.m_bPermanent)
			{
				return 0;
			}
			if (lhs.m_Count > 0L && rhs.m_Count <= 0L)
			{
				return 1;
			}
			if (rhs.m_Count > 0L && lhs.m_Count <= 0L)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06003868 RID: 14440 RVA: 0x00123948 File Offset: 0x00121B48
		private int CompareByUnitTypeFirst(NKMMoldItemData lhs, NKMMoldItemData rhs)
		{
			NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(lhs.m_MoldID);
			NKMItemMoldTemplet itemMoldTempletByID2 = NKMItemManager.GetItemMoldTempletByID(rhs.m_MoldID);
			if (itemMoldTempletByID == null || itemMoldTempletByID2 == null)
			{
				return 0;
			}
			return itemMoldTempletByID.m_RewardEquipUnitType.CompareTo(itemMoldTempletByID2.m_RewardEquipUnitType);
		}

		// Token: 0x06003869 RID: 14441 RVA: 0x00123994 File Offset: 0x00121B94
		private int CompareByUnitTypeLast(NKMMoldItemData lhs, NKMMoldItemData rhs)
		{
			NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(lhs.m_MoldID);
			NKMItemMoldTemplet itemMoldTempletByID2 = NKMItemManager.GetItemMoldTempletByID(rhs.m_MoldID);
			if (itemMoldTempletByID == null || itemMoldTempletByID2 == null)
			{
				return 0;
			}
			return itemMoldTempletByID2.m_RewardEquipUnitType.CompareTo(itemMoldTempletByID.m_RewardEquipUnitType);
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x001239DD File Offset: 0x00121BDD
		private int CompareByIDFirst(NKMMoldItemData lhs, NKMMoldItemData rhs)
		{
			return lhs.m_MoldID.CompareTo(rhs.m_MoldID);
		}

		// Token: 0x0600386B RID: 14443 RVA: 0x001239F0 File Offset: 0x00121BF0
		private int CompareByIDLast(NKMMoldItemData lhs, NKMMoldItemData rhs)
		{
			return rhs.m_MoldID.CompareTo(lhs.m_MoldID);
		}

		// Token: 0x0600386C RID: 14444 RVA: 0x00123A03 File Offset: 0x00121C03
		public string GetSortName(NKCMoldSortSystem.eSortOption option)
		{
			switch (option)
			{
			default:
				return NKCUtilString.GET_STRING_SORT_CRAFTABLE;
			case NKCMoldSortSystem.eSortOption.Tier_High:
			case NKCMoldSortSystem.eSortOption.Tier_Low:
				return NKCUtilString.GET_STRING_SORT_TIER;
			case NKCMoldSortSystem.eSortOption.Rarity_High:
			case NKCMoldSortSystem.eSortOption.Rarity_Low:
				return NKCUtilString.GET_STRING_SORT_RARITY;
			}
		}

		// Token: 0x0600386D RID: 14445 RVA: 0x00123A36 File Offset: 0x00121C36
		public string GetSortName()
		{
			return this.GetSortName(this.GetCurActiveOption());
		}

		// Token: 0x0600386E RID: 14446 RVA: 0x00123A44 File Offset: 0x00121C44
		public NKCMoldSortSystem.eSortOption GetCurActiveOption()
		{
			if (this.m_Options.lstSortOption.Count > 0)
			{
				return this.m_Options.lstSortOption[1];
			}
			return NKCMoldSortSystem.eSortOption.None;
		}

		// Token: 0x0600386F RID: 14447 RVA: 0x00123A6C File Offset: 0x00121C6C
		public static bool GetDescendingBySorting(List<NKCMoldSortSystem.eSortOption> lstSortOption)
		{
			return lstSortOption.Count <= 0 || NKCMoldSortSystem.GetDescendingBySorting(lstSortOption[0]);
		}

		// Token: 0x06003870 RID: 14448 RVA: 0x00123A85 File Offset: 0x00121C85
		private static bool GetDescendingBySorting(NKCMoldSortSystem.eSortOption sortOption)
		{
			switch (sortOption)
			{
			default:
				return true;
			case NKCMoldSortSystem.eSortOption.Craftable_Low:
			case NKCMoldSortSystem.eSortOption.Tier_Low:
			case NKCMoldSortSystem.eSortOption.Rarity_Low:
			case NKCMoldSortSystem.eSortOption.EquipType_Last:
				return false;
			}
		}

		// Token: 0x06003871 RID: 14449 RVA: 0x00123ABA File Offset: 0x00121CBA
		public static NKMMoldItemData MakeTempMoldData(int moldID, int level = 1)
		{
			if (NKMItemManager.GetItemMoldTempletByID(moldID) != null)
			{
				return new NKMMoldItemData(moldID, 1L);
			}
			return new NKMMoldItemData();
		}

		// Token: 0x06003872 RID: 14450 RVA: 0x00123AD2 File Offset: 0x00121CD2
		public static List<NKCMoldSortSystem.eSortOption> GetDefaultSortOption(string sortKey)
		{
			if (NKCMoldSortSystem.m_dicMoldSort.ContainsKey(sortKey))
			{
				return NKCMoldSortSystem.m_dicMoldSort[sortKey];
			}
			return null;
		}

		// Token: 0x06003873 RID: 14451 RVA: 0x00123AEE File Offset: 0x00121CEE
		public static List<NKCMoldSortSystem.eSortOption> GetDefaultSortOption()
		{
			return NKCMoldSortSystem.EQUIP_CRAFTABLE_SORT_LIST_DESC;
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06003874 RID: 14452 RVA: 0x00123AF5 File Offset: 0x00121CF5
		public List<NKMMoldItemData> lstSortedList
		{
			get
			{
				return this.m_lstNKMMoldItemData;
			}
		}

		// Token: 0x06003875 RID: 14453 RVA: 0x00123AFD File Offset: 0x00121CFD
		public void Update(NKM_CRAFT_TAB_TYPE type)
		{
			this.Filtering(type);
			this.SortMoldDataList();
		}

		// Token: 0x06003876 RID: 14454 RVA: 0x00123B0C File Offset: 0x00121D0C
		private void Filtering(NKM_CRAFT_TAB_TYPE type)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			this.m_lstNKMMoldItemData = NKMItemManager.GetMoldItemData(type);
			for (int i = 0; i < this.m_lstNKMMoldItemData.Count; i++)
			{
				NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(this.m_lstNKMMoldItemData[i].m_MoldID);
				if (itemMoldTempletByID == null)
				{
					this.m_lstNKMMoldItemData.RemoveAt(i);
					i--;
				}
				else if (!itemMoldTempletByID.EnableByTag)
				{
					this.m_lstNKMMoldItemData.RemoveAt(i);
					i--;
				}
				else if (!itemMoldTempletByID.m_bPermanent && nkmuserData.m_CraftData.GetMoldItemDataByID(itemMoldTempletByID.m_MoldID) == null)
				{
					this.m_lstNKMMoldItemData.RemoveAt(i);
					i--;
				}
			}
			HashSet<NKCMoldSortSystem.eFilterOption> setFilterOption = this.m_Options.setFilterOption;
			if (setFilterOption != null && setFilterOption.Count > 0)
			{
				bool flag = setFilterOption.Contains(NKCMoldSortSystem.eFilterOption.Mold_Parts_All);
				bool flag2 = setFilterOption.Contains(NKCMoldSortSystem.eFilterOption.Mold_Parts_Weapon);
				bool flag3 = setFilterOption.Contains(NKCMoldSortSystem.eFilterOption.Mold_Parts_Defence);
				bool flag4 = setFilterOption.Contains(NKCMoldSortSystem.eFilterOption.Mold_Parts_Acc);
				bool flag5 = setFilterOption.Contains(NKCMoldSortSystem.eFilterOption.Mold_Tier_1);
				bool flag6 = setFilterOption.Contains(NKCMoldSortSystem.eFilterOption.Mold_Tier_2);
				bool flag7 = setFilterOption.Contains(NKCMoldSortSystem.eFilterOption.Mold_Tier_3);
				bool flag8 = setFilterOption.Contains(NKCMoldSortSystem.eFilterOption.Mold_Tier_4);
				bool flag9 = setFilterOption.Contains(NKCMoldSortSystem.eFilterOption.Mold_Tier_5);
				bool flag10 = setFilterOption.Contains(NKCMoldSortSystem.eFilterOption.Mold_Tier_6);
				bool flag11 = setFilterOption.Contains(NKCMoldSortSystem.eFilterOption.Mold_Tier_7);
				bool flag12 = setFilterOption.Contains(NKCMoldSortSystem.eFilterOption.Mold_Type_Normal);
				bool flag13 = setFilterOption.Contains(NKCMoldSortSystem.eFilterOption.Mold_Type_Raid);
				bool flag14 = setFilterOption.Contains(NKCMoldSortSystem.eFilterOption.Mold_Type_Etc);
				bool flag15 = setFilterOption.Contains(NKCMoldSortSystem.eFilterOption.Mold_Status_Enable);
				bool flag16 = setFilterOption.Contains(NKCMoldSortSystem.eFilterOption.Mold_Status_Disable);
				bool flag17 = setFilterOption.Contains(NKCMoldSortSystem.eFilterOption.Mold_Unit_Counter);
				bool flag18 = setFilterOption.Contains(NKCMoldSortSystem.eFilterOption.Mold_Unit_Soldier);
				bool flag19 = setFilterOption.Contains(NKCMoldSortSystem.eFilterOption.Mold_Unit_Mechanic);
				bool flag20 = setFilterOption.Contains(NKCMoldSortSystem.eFilterOption.Mold_Unit_Etc);
				bool flag21 = setFilterOption.Contains(NKCMoldSortSystem.eFilterOption.Mold_Grade_SSR);
				bool flag22 = setFilterOption.Contains(NKCMoldSortSystem.eFilterOption.Mold_Grade_SR);
				bool flag23 = setFilterOption.Contains(NKCMoldSortSystem.eFilterOption.Mold_Grade_R);
				bool flag24 = setFilterOption.Contains(NKCMoldSortSystem.eFilterOption.Mold_Grade_N);
				for (int j = 0; j < this.m_lstNKMMoldItemData.Count; j++)
				{
					NKMItemMoldTemplet itemMoldTempletByID2 = NKMItemManager.GetItemMoldTempletByID(this.m_lstNKMMoldItemData[j].m_MoldID);
					if (itemMoldTempletByID2 != null)
					{
						if ((flag || flag2 || flag3 || flag4) && !(false | (flag && itemMoldTempletByID2.m_RewardEquipPosition == ITEM_EQUIP_POSITION.IEP_MAX) | (flag2 && itemMoldTempletByID2.m_RewardEquipPosition == ITEM_EQUIP_POSITION.IEP_WEAPON) | (flag3 && itemMoldTempletByID2.m_RewardEquipPosition == ITEM_EQUIP_POSITION.IEP_DEFENCE) | (flag4 && (itemMoldTempletByID2.m_RewardEquipPosition == ITEM_EQUIP_POSITION.IEP_ACC || itemMoldTempletByID2.m_RewardEquipPosition == ITEM_EQUIP_POSITION.IEP_ACC2))))
						{
							this.m_lstNKMMoldItemData.RemoveAt(j);
							j--;
						}
						else if ((flag12 || flag13 || flag14) && !(false | (flag12 && itemMoldTempletByID2.m_ContentType == NKM_ITEM_DROP_POSITION.NORMAL) | (flag13 && itemMoldTempletByID2.m_ContentType == NKM_ITEM_DROP_POSITION.RAID) | (flag14 && itemMoldTempletByID2.m_ContentType == NKM_ITEM_DROP_POSITION.ETC)))
						{
							this.m_lstNKMMoldItemData.RemoveAt(j);
							j--;
						}
						else if ((flag5 || flag6 || flag7 || flag8 || flag9 || flag10 || flag11) && !(false | (flag5 && itemMoldTempletByID2.m_Tier == 1) | (flag6 && itemMoldTempletByID2.m_Tier == 2) | (flag7 && itemMoldTempletByID2.m_Tier == 3) | (flag8 && itemMoldTempletByID2.m_Tier == 4) | (flag9 && itemMoldTempletByID2.m_Tier == 5) | (flag10 && itemMoldTempletByID2.m_Tier == 6) | (flag11 && itemMoldTempletByID2.m_Tier == 7)))
						{
							this.m_lstNKMMoldItemData.RemoveAt(j);
							j--;
						}
						else
						{
							if (flag15)
							{
								if (NKCUtil.GetEquipCreatableCount(this.m_lstNKMMoldItemData[j], nkmuserData.m_InventoryData) <= 0)
								{
									this.m_lstNKMMoldItemData.RemoveAt(j);
									j--;
									goto IL_49A;
								}
							}
							else if (flag16 && NKCUtil.GetEquipCreatableCount(this.m_lstNKMMoldItemData[j], nkmuserData.m_InventoryData) > 0)
							{
								this.m_lstNKMMoldItemData.RemoveAt(j);
								j--;
								goto IL_49A;
							}
							if ((flag17 || flag18 || flag19 || flag20) && !(false | (flag17 && itemMoldTempletByID2.m_RewardEquipUnitType == NKM_UNIT_STYLE_TYPE.NUST_COUNTER) | (flag18 && itemMoldTempletByID2.m_RewardEquipUnitType == NKM_UNIT_STYLE_TYPE.NUST_SOLDIER) | (flag19 && itemMoldTempletByID2.m_RewardEquipUnitType == NKM_UNIT_STYLE_TYPE.NUST_MECHANIC) | (flag20 && itemMoldTempletByID2.m_RewardEquipUnitType == NKM_UNIT_STYLE_TYPE.NUST_ETC)))
							{
								this.m_lstNKMMoldItemData.RemoveAt(j);
								j--;
							}
							else if ((flag24 || flag23 || flag22 || flag21) && !(false | (flag21 && itemMoldTempletByID2.m_Grade == NKM_ITEM_GRADE.NIG_SSR) | (flag22 && itemMoldTempletByID2.m_Grade == NKM_ITEM_GRADE.NIG_SR) | (flag23 && itemMoldTempletByID2.m_Grade == NKM_ITEM_GRADE.NIG_R) | (flag24 && itemMoldTempletByID2.m_Grade == NKM_ITEM_GRADE.NIG_N)))
							{
								this.m_lstNKMMoldItemData.RemoveAt(j);
								j--;
							}
						}
					}
					IL_49A:;
				}
			}
		}

		// Token: 0x06003877 RID: 14455 RVA: 0x00123FCC File Offset: 0x001221CC
		public bool Sort(NKCMoldSortSystem.eSortOption selectedSortOption)
		{
			if (NKCMoldSortSystem.GetDescendingBySorting(selectedSortOption) != this.m_bDescendingOrder)
			{
				selectedSortOption = this.ChangeSortOption(selectedSortOption);
			}
			List<NKCMoldSortSystem.eSortOption> sortOption = this.GetSortOption(selectedSortOption);
			if (sortOption.Count > 0)
			{
				this.m_Options.lstSortOption = sortOption;
				this.SortMoldDataList();
			}
			return true;
		}

		// Token: 0x06003878 RID: 14456 RVA: 0x00124014 File Offset: 0x00122214
		private NKCMoldSortSystem.eSortOption ChangeSortOption(NKCMoldSortSystem.eSortOption option)
		{
			switch (option)
			{
			case NKCMoldSortSystem.eSortOption.Craftable_High:
				return NKCMoldSortSystem.eSortOption.Craftable_Low;
			case NKCMoldSortSystem.eSortOption.Craftable_Low:
				return NKCMoldSortSystem.eSortOption.Craftable_High;
			case NKCMoldSortSystem.eSortOption.Tier_High:
				return NKCMoldSortSystem.eSortOption.Tier_Low;
			case NKCMoldSortSystem.eSortOption.Tier_Low:
				return NKCMoldSortSystem.eSortOption.Tier_High;
			case NKCMoldSortSystem.eSortOption.Rarity_High:
				return NKCMoldSortSystem.eSortOption.Rarity_Low;
			case NKCMoldSortSystem.eSortOption.Rarity_Low:
				return NKCMoldSortSystem.eSortOption.Rarity_High;
			default:
				return NKCMoldSortSystem.eSortOption.None;
			}
		}

		// Token: 0x06003879 RID: 14457 RVA: 0x00124048 File Offset: 0x00122248
		private List<NKCMoldSortSystem.eSortOption> GetSortOption(NKCMoldSortSystem.eSortOption targetOption)
		{
			string sortKey = this.GetSortKey(targetOption);
			if (string.IsNullOrEmpty(sortKey))
			{
				Debug.LogError("허용되는 키가 없습니다! 확인해주세요!");
			}
			else if (NKCMoldSortSystem.m_dicMoldSort.ContainsKey(sortKey))
			{
				return NKCMoldSortSystem.m_dicMoldSort[sortKey];
			}
			return new List<NKCMoldSortSystem.eSortOption>();
		}

		// Token: 0x0600387A RID: 14458 RVA: 0x00124090 File Offset: 0x00122290
		private string GetSortKey(NKCMoldSortSystem.eSortOption targetOption)
		{
			switch (targetOption)
			{
			case NKCMoldSortSystem.eSortOption.Craftable_High:
				return "ST_Makeable";
			case NKCMoldSortSystem.eSortOption.Craftable_Low:
				return "ST_Makeable_ASC";
			case NKCMoldSortSystem.eSortOption.Tier_High:
				return "ST_Tier";
			case NKCMoldSortSystem.eSortOption.Tier_Low:
				return "ST_Tier_ASC";
			case NKCMoldSortSystem.eSortOption.Rarity_High:
				return "ST_Grade";
			case NKCMoldSortSystem.eSortOption.Rarity_Low:
				return "ST_Grade_ASC";
			default:
				return null;
			}
		}

		// Token: 0x0600387B RID: 14459 RVA: 0x001240E4 File Offset: 0x001222E4
		private void SortMoldDataList()
		{
			if (this.m_lstNKMMoldItemData.Count < 2)
			{
				return;
			}
			NKCUnitSortSystem.NKCDataComparerer<NKMMoldItemData> nkcdataComparerer = new NKCUnitSortSystem.NKCDataComparerer<NKMMoldItemData>(Array.Empty<NKCUnitSortSystem.NKCDataComparerer<NKMMoldItemData>.CompareFunc>());
			foreach (NKCMoldSortSystem.eSortOption sortOption in this.m_Options.lstSortOption)
			{
				nkcdataComparerer.AddFunc(this.GetDataComparer(sortOption));
			}
			this.m_lstNKMMoldItemData.Sort(nkcdataComparerer);
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x00124168 File Offset: 0x00122368
		public void OnCheckAscend(bool bAscend, UnityAction sortEndCallBack = null)
		{
			NKCMoldSortSystem.eSortOption selectedSortOption = NKCMoldSortSystem.eSortOption.Craftable_High;
			switch (this.GetCurActiveOption())
			{
			case NKCMoldSortSystem.eSortOption.Craftable_High:
			case NKCMoldSortSystem.eSortOption.Craftable_Low:
				if (!bAscend)
				{
					selectedSortOption = NKCMoldSortSystem.eSortOption.Craftable_High;
				}
				else
				{
					selectedSortOption = NKCMoldSortSystem.eSortOption.Craftable_Low;
				}
				break;
			case NKCMoldSortSystem.eSortOption.Tier_High:
			case NKCMoldSortSystem.eSortOption.Tier_Low:
				if (!bAscend)
				{
					selectedSortOption = NKCMoldSortSystem.eSortOption.Tier_High;
				}
				else
				{
					selectedSortOption = NKCMoldSortSystem.eSortOption.Tier_Low;
				}
				break;
			case NKCMoldSortSystem.eSortOption.Rarity_High:
			case NKCMoldSortSystem.eSortOption.Rarity_Low:
				if (!bAscend)
				{
					selectedSortOption = NKCMoldSortSystem.eSortOption.Rarity_High;
				}
				else
				{
					selectedSortOption = NKCMoldSortSystem.eSortOption.Rarity_Low;
				}
				break;
			}
			this.m_bDescendingOrder = !bAscend;
			this.Sort(selectedSortOption);
			if (sortEndCallBack != null)
			{
				sortEndCallBack();
			}
		}

		// Token: 0x040034BA RID: 13498
		private static Dictionary<string, List<NKCMoldSortSystem.eSortOption>> m_dicMoldSort = new Dictionary<string, List<NKCMoldSortSystem.eSortOption>>
		{
			{
				"ST_Makeable",
				new List<NKCMoldSortSystem.eSortOption>
				{
					NKCMoldSortSystem.eSortOption.HaveMold_First,
					NKCMoldSortSystem.eSortOption.Craftable_High,
					NKCMoldSortSystem.eSortOption.Tier_High,
					NKCMoldSortSystem.eSortOption.Rarity_High,
					NKCMoldSortSystem.eSortOption.UnitType_First,
					NKCMoldSortSystem.eSortOption.EquipType_FIrst,
					NKCMoldSortSystem.eSortOption.ID_First
				}
			},
			{
				"ST_Makeable_ASC",
				new List<NKCMoldSortSystem.eSortOption>
				{
					NKCMoldSortSystem.eSortOption.HaveMold_Last,
					NKCMoldSortSystem.eSortOption.Craftable_Low,
					NKCMoldSortSystem.eSortOption.Tier_Low,
					NKCMoldSortSystem.eSortOption.Rarity_Low,
					NKCMoldSortSystem.eSortOption.UnitType_Last,
					NKCMoldSortSystem.eSortOption.EquipType_Last,
					NKCMoldSortSystem.eSortOption.ID_Last
				}
			},
			{
				"ST_Tier",
				new List<NKCMoldSortSystem.eSortOption>
				{
					NKCMoldSortSystem.eSortOption.HaveMold_First,
					NKCMoldSortSystem.eSortOption.Tier_High,
					NKCMoldSortSystem.eSortOption.Rarity_High,
					NKCMoldSortSystem.eSortOption.UnitType_First,
					NKCMoldSortSystem.eSortOption.EquipType_FIrst,
					NKCMoldSortSystem.eSortOption.ID_First
				}
			},
			{
				"ST_Tier_ASC",
				new List<NKCMoldSortSystem.eSortOption>
				{
					NKCMoldSortSystem.eSortOption.HaveMold_Last,
					NKCMoldSortSystem.eSortOption.Tier_Low,
					NKCMoldSortSystem.eSortOption.Rarity_Low,
					NKCMoldSortSystem.eSortOption.UnitType_Last,
					NKCMoldSortSystem.eSortOption.EquipType_Last,
					NKCMoldSortSystem.eSortOption.ID_Last
				}
			},
			{
				"ST_Grade",
				new List<NKCMoldSortSystem.eSortOption>
				{
					NKCMoldSortSystem.eSortOption.HaveMold_First,
					NKCMoldSortSystem.eSortOption.Rarity_High,
					NKCMoldSortSystem.eSortOption.Tier_High,
					NKCMoldSortSystem.eSortOption.UnitType_First,
					NKCMoldSortSystem.eSortOption.EquipType_FIrst,
					NKCMoldSortSystem.eSortOption.ID_First
				}
			},
			{
				"ST_Grade_ASC",
				new List<NKCMoldSortSystem.eSortOption>
				{
					NKCMoldSortSystem.eSortOption.HaveMold_Last,
					NKCMoldSortSystem.eSortOption.Rarity_Low,
					NKCMoldSortSystem.eSortOption.Tier_Low,
					NKCMoldSortSystem.eSortOption.UnitType_Last,
					NKCMoldSortSystem.eSortOption.EquipType_Last,
					NKCMoldSortSystem.eSortOption.ID_Last
				}
			}
		};

		// Token: 0x040034BB RID: 13499
		public const string SORT_MAKEABLE_DESC = "ST_Makeable";

		// Token: 0x040034BC RID: 13500
		private const string SORT_MAKEABLE_ASC = "ST_Makeable_ASC";

		// Token: 0x040034BD RID: 13501
		public const string SORT_TIER_DESC = "ST_Tier";

		// Token: 0x040034BE RID: 13502
		private const string SORT_TIER_ASC = "ST_Tier_ASC";

		// Token: 0x040034BF RID: 13503
		public const string SORT_GRADE_DESC = "ST_Grade";

		// Token: 0x040034C0 RID: 13504
		private const string SORT_GRADE_ASC = "ST_Grade_ASC";

		// Token: 0x040034C1 RID: 13505
		public static readonly List<NKCMoldSortSystem.eSortOption> EQUIP_CRAFTABLE_SORT_LIST_DESC = new List<NKCMoldSortSystem.eSortOption>
		{
			NKCMoldSortSystem.eSortOption.HaveMold_First,
			NKCMoldSortSystem.eSortOption.Craftable_High,
			NKCMoldSortSystem.eSortOption.Tier_High,
			NKCMoldSortSystem.eSortOption.Rarity_High,
			NKCMoldSortSystem.eSortOption.EquipType_FIrst
		};

		// Token: 0x040034C2 RID: 13506
		public static List<string> m_lstMoldFilter = new List<string>
		{
			"FT_EquipPosition",
			"FT_Tier",
			"FT_ContentType",
			"FT_Makeable",
			"FT_UnitType",
			"FT_Grade",
			"FT_Makeable"
		};

		// Token: 0x040034C3 RID: 13507
		protected NKCMoldSortSystem.MoldListOptions m_Options;

		// Token: 0x040034C4 RID: 13508
		private List<NKMMoldItemData> m_lstNKMMoldItemData = new List<NKMMoldItemData>();

		// Token: 0x040034C5 RID: 13509
		private bool m_bDescendingOrder = true;

		// Token: 0x02001372 RID: 4978
		public enum eSortOption
		{
			// Token: 0x040099BE RID: 39358
			None,
			// Token: 0x040099BF RID: 39359
			Craftable_High,
			// Token: 0x040099C0 RID: 39360
			Craftable_Low,
			// Token: 0x040099C1 RID: 39361
			Tier_High,
			// Token: 0x040099C2 RID: 39362
			Tier_Low,
			// Token: 0x040099C3 RID: 39363
			Rarity_High,
			// Token: 0x040099C4 RID: 39364
			Rarity_Low,
			// Token: 0x040099C5 RID: 39365
			HaveMold_First,
			// Token: 0x040099C6 RID: 39366
			HaveMold_Last,
			// Token: 0x040099C7 RID: 39367
			EquipType_FIrst,
			// Token: 0x040099C8 RID: 39368
			EquipType_Last,
			// Token: 0x040099C9 RID: 39369
			UnitType_First,
			// Token: 0x040099CA RID: 39370
			UnitType_Last,
			// Token: 0x040099CB RID: 39371
			ID_First,
			// Token: 0x040099CC RID: 39372
			ID_Last
		}

		// Token: 0x02001373 RID: 4979
		public enum eFilterOption
		{
			// Token: 0x040099CE RID: 39374
			Mold_Parts_All,
			// Token: 0x040099CF RID: 39375
			Mold_Parts_Weapon,
			// Token: 0x040099D0 RID: 39376
			Mold_Parts_Defence,
			// Token: 0x040099D1 RID: 39377
			Mold_Parts_Acc,
			// Token: 0x040099D2 RID: 39378
			Mold_Tier_1,
			// Token: 0x040099D3 RID: 39379
			Mold_Tier_2,
			// Token: 0x040099D4 RID: 39380
			Mold_Tier_3,
			// Token: 0x040099D5 RID: 39381
			Mold_Tier_4,
			// Token: 0x040099D6 RID: 39382
			Mold_Tier_5,
			// Token: 0x040099D7 RID: 39383
			Mold_Tier_6,
			// Token: 0x040099D8 RID: 39384
			Mold_Tier_7,
			// Token: 0x040099D9 RID: 39385
			Mold_Type_Normal,
			// Token: 0x040099DA RID: 39386
			Mold_Type_Raid,
			// Token: 0x040099DB RID: 39387
			Mold_Type_Etc,
			// Token: 0x040099DC RID: 39388
			Mold_Status_Enable,
			// Token: 0x040099DD RID: 39389
			Mold_Status_Disable,
			// Token: 0x040099DE RID: 39390
			Mold_Unit_Counter,
			// Token: 0x040099DF RID: 39391
			Mold_Unit_Soldier,
			// Token: 0x040099E0 RID: 39392
			Mold_Unit_Mechanic,
			// Token: 0x040099E1 RID: 39393
			Mold_Unit_Etc,
			// Token: 0x040099E2 RID: 39394
			Mold_Grade_SSR,
			// Token: 0x040099E3 RID: 39395
			Mold_Grade_SR,
			// Token: 0x040099E4 RID: 39396
			Mold_Grade_R,
			// Token: 0x040099E5 RID: 39397
			Mold_Grade_N
		}

		// Token: 0x02001374 RID: 4980
		public struct MoldListOptions
		{
			// Token: 0x040099E6 RID: 39398
			public HashSet<NKCMoldSortSystem.eFilterOption> setFilterOption;

			// Token: 0x040099E7 RID: 39399
			public List<NKCMoldSortSystem.eSortOption> lstSortOption;
		}
	}
}
