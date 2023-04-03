using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000673 RID: 1651
	public class NKCEquipSortSystem
	{
		// Token: 0x0600344A RID: 13386 RVA: 0x00107784 File Offset: 0x00105984
		public static NKCEquipSortSystem.eSortCategory GetSortCategoryFromOption(NKCEquipSortSystem.eSortOption option)
		{
			foreach (KeyValuePair<NKCEquipSortSystem.eSortCategory, Tuple<NKCEquipSortSystem.eSortOption, NKCEquipSortSystem.eSortOption>> keyValuePair in NKCEquipSortSystem.s_dicSortCategory)
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
			return NKCEquipSortSystem.eSortCategory.None;
		}

		// Token: 0x0600344B RID: 13387 RVA: 0x00107804 File Offset: 0x00105A04
		public static NKCEquipSortSystem.eSortOption GetSortOptionByCategory(NKCEquipSortSystem.eSortCategory category, bool bDescending)
		{
			Tuple<NKCEquipSortSystem.eSortOption, NKCEquipSortSystem.eSortOption> tuple = NKCEquipSortSystem.s_dicSortCategory[category];
			if (!bDescending)
			{
				return tuple.Item1;
			}
			return tuple.Item2;
		}

		// Token: 0x0600344C RID: 13388 RVA: 0x00107830 File Offset: 0x00105A30
		public static bool IsDescending(NKCEquipSortSystem.eSortOption option)
		{
			foreach (KeyValuePair<NKCEquipSortSystem.eSortCategory, Tuple<NKCEquipSortSystem.eSortOption, NKCEquipSortSystem.eSortOption>> keyValuePair in NKCEquipSortSystem.s_dicSortCategory)
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

		// Token: 0x0600344D RID: 13389 RVA: 0x001078A4 File Offset: 0x00105AA4
		public static NKCEquipSortSystem.eSortOption GetInvertedAscendOption(NKCEquipSortSystem.eSortOption option)
		{
			foreach (KeyValuePair<NKCEquipSortSystem.eSortCategory, Tuple<NKCEquipSortSystem.eSortOption, NKCEquipSortSystem.eSortOption>> keyValuePair in NKCEquipSortSystem.s_dicSortCategory)
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

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x0600344E RID: 13390 RVA: 0x00107930 File Offset: 0x00105B30
		// (set) Token: 0x0600344F RID: 13391 RVA: 0x00107938 File Offset: 0x00105B38
		public NKCEquipSortSystem.EquipListOptions m_EquipListOptions
		{
			get
			{
				return this.m_Options;
			}
			set
			{
				this.m_Options = value;
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06003450 RID: 13392 RVA: 0x00107944 File Offset: 0x00105B44
		public List<NKMEquipItemData> SortedEquipList
		{
			get
			{
				if (this.m_lstCurrentEquipList == null)
				{
					if (this.m_Options.setFilterOption == null)
					{
						this.m_Options.setFilterOption = new HashSet<NKCEquipSortSystem.eFilterOption>();
						this.FilterList(this.m_Options.setFilterOption, this.m_Options.bHideEquippedItem);
					}
					else
					{
						this.FilterList(this.m_Options.setFilterOption, this.m_Options.bHideEquippedItem);
					}
				}
				return this.m_lstCurrentEquipList;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06003451 RID: 13393 RVA: 0x001079B6 File Offset: 0x00105BB6
		public HashSet<NKCEquipSortSystem.eFilterOption> ExcludeFilterSet
		{
			get
			{
				return this.m_Options.setExcludeFilterOption;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06003452 RID: 13394 RVA: 0x001079C3 File Offset: 0x00105BC3
		// (set) Token: 0x06003453 RID: 13395 RVA: 0x001079D0 File Offset: 0x00105BD0
		public HashSet<NKCEquipSortSystem.eFilterOption> FilterSet
		{
			get
			{
				return this.m_Options.setFilterOption;
			}
			set
			{
				this.FilterList(value, this.m_Options.bHideEquippedItem);
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06003454 RID: 13396 RVA: 0x001079E4 File Offset: 0x00105BE4
		// (set) Token: 0x06003455 RID: 13397 RVA: 0x001079F1 File Offset: 0x00105BF1
		public List<NKCEquipSortSystem.eSortOption> lstSortOption
		{
			get
			{
				return this.m_Options.lstSortOption;
			}
			set
			{
				this.SortList(value, NKCEquipSortSystem.GetDescendingBySorting(value));
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06003456 RID: 13398 RVA: 0x00107A00 File Offset: 0x00105C00
		// (set) Token: 0x06003457 RID: 13399 RVA: 0x00107A10 File Offset: 0x00105C10
		public bool bHideEquippedItem
		{
			get
			{
				return this.m_Options.bHideEquippedItem;
			}
			set
			{
				if (this.m_Options.setFilterOption != null)
				{
					this.FilterList(this.m_Options.setFilterOption, value);
					return;
				}
				this.m_Options.setFilterOption = new HashSet<NKCEquipSortSystem.eFilterOption>();
				this.FilterList(this.m_Options.setFilterOption, value);
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06003458 RID: 13400 RVA: 0x00107A5F File Offset: 0x00105C5F
		// (set) Token: 0x06003459 RID: 13401 RVA: 0x00107A6C File Offset: 0x00105C6C
		public NKM_STAT_TYPE FilterStatType_01
		{
			get
			{
				return this.m_Options.FilterStatType_01;
			}
			set
			{
				this.m_Options.FilterStatType_01 = value;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x0600345A RID: 13402 RVA: 0x00107A7A File Offset: 0x00105C7A
		// (set) Token: 0x0600345B RID: 13403 RVA: 0x00107A87 File Offset: 0x00105C87
		public NKM_STAT_TYPE FilterStatType_02
		{
			get
			{
				return this.m_Options.FilterStatType_02;
			}
			set
			{
				this.m_Options.FilterStatType_02 = value;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x0600345C RID: 13404 RVA: 0x00107A95 File Offset: 0x00105C95
		// (set) Token: 0x0600345D RID: 13405 RVA: 0x00107AA2 File Offset: 0x00105CA2
		public NKM_STAT_TYPE FilterStatType_03
		{
			get
			{
				return this.m_Options.FilterStatType_03;
			}
			set
			{
				this.m_Options.FilterStatType_03 = value;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x0600345E RID: 13406 RVA: 0x00107AB0 File Offset: 0x00105CB0
		// (set) Token: 0x0600345F RID: 13407 RVA: 0x00107ABD File Offset: 0x00105CBD
		public int FilterStatType_SetOptionID
		{
			get
			{
				return this.m_Options.FilterSetOptionID;
			}
			set
			{
				this.m_Options.FilterSetOptionID = value;
			}
		}

		// Token: 0x06003460 RID: 13408 RVA: 0x00107ACB File Offset: 0x00105CCB
		private NKCEquipSortSystem()
		{
		}

		// Token: 0x06003461 RID: 13409 RVA: 0x00107AEC File Offset: 0x00105CEC
		public NKCEquipSortSystem(NKMUserData userData, NKCEquipSortSystem.EquipListOptions options)
		{
			this.m_Options = options;
			List<NKMEquipItemData> lstTargetEquips = new List<NKMEquipItemData>(userData.m_InventoryData.EquipItems.Values);
			this.m_dicAllEquipList = this.BuildFullEquipList(userData, lstTargetEquips, options);
		}

		// Token: 0x06003462 RID: 13410 RVA: 0x00107B41 File Offset: 0x00105D41
		public NKCEquipSortSystem(NKMUserData userData, NKCEquipSortSystem.EquipListOptions options, IEnumerable<NKMEquipItemData> lstEquipData)
		{
			this.m_Options = options;
			this.m_dicAllEquipList = this.BuildFullEquipList(userData, lstEquipData, options);
		}

		// Token: 0x06003463 RID: 13411 RVA: 0x00107B75 File Offset: 0x00105D75
		public virtual void BuildFilterAndSortedList(HashSet<NKCEquipSortSystem.eFilterOption> setfilterType, List<NKCEquipSortSystem.eSortOption> lstSortOption, bool bHideEquippedItem)
		{
			this.m_Options.bHideEquippedItem = bHideEquippedItem;
			this.m_Options.setFilterOption = setfilterType;
			this.m_Options.lstSortOption = lstSortOption;
			this.FilterList(setfilterType, bHideEquippedItem);
		}

		// Token: 0x06003464 RID: 13412 RVA: 0x00107BA4 File Offset: 0x00105DA4
		private Dictionary<long, NKMEquipItemData> BuildFullEquipList(NKMUserData userData, IEnumerable<NKMEquipItemData> lstTargetEquips, NKCEquipSortSystem.EquipListOptions options)
		{
			Dictionary<long, NKMEquipItemData> dictionary = new Dictionary<long, NKMEquipItemData>();
			HashSet<int> setOnlyIncludeEquipID = options.setOnlyIncludeEquipID;
			HashSet<int> setExcludeEquipID = options.setExcludeEquipID;
			foreach (NKMEquipItemData nkmequipItemData in lstTargetEquips)
			{
				long itemUid = nkmequipItemData.m_ItemUid;
				if ((options.AdditionalExcludeFilterFunc == null || options.AdditionalExcludeFilterFunc(nkmequipItemData)) && (options.setExcludeEquipUID == null || !options.setExcludeEquipUID.Contains(itemUid)) && (!options.bHideLockItem || !nkmequipItemData.m_bLock))
				{
					if (options.bHideMaxLvItem)
					{
						NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(nkmequipItemData.m_ItemEquipID);
						if (equipTemplet != null && nkmequipItemData.m_EnchantLevel >= NKMItemManager.GetMaxEquipEnchantLevel(equipTemplet.m_NKM_ITEM_TIER))
						{
							continue;
						}
					}
					if ((setOnlyIncludeEquipID == null || setOnlyIncludeEquipID.Count <= 0 || setOnlyIncludeEquipID.Contains(nkmequipItemData.m_ItemEquipID)) && (setExcludeEquipID == null || setExcludeEquipID.Count <= 0 || !setExcludeEquipID.Contains(nkmequipItemData.m_ItemEquipID)) && (options.setExcludeFilterOption == null || options.setExcludeFilterOption.Count <= 0 || !NKCEquipSortSystem.CheckFilter(nkmequipItemData, options.setExcludeFilterOption, options)))
					{
						dictionary.Add(itemUid, nkmequipItemData);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06003465 RID: 13413 RVA: 0x00107CF4 File Offset: 0x00105EF4
		public void UpdateEquipData(NKMEquipItemData equipData)
		{
			if (this.m_dicAllEquipList.ContainsKey(equipData.m_ItemUid))
			{
				this.m_dicAllEquipList[equipData.m_ItemUid] = equipData;
			}
		}

		// Token: 0x06003466 RID: 13414 RVA: 0x00107D1C File Offset: 0x00105F1C
		protected bool FilterData(NKMEquipItemData equipData, List<HashSet<NKCEquipSortSystem.eFilterOption>> setFilter)
		{
			if (this.m_Options.bHideEquippedItem && equipData.m_OwnerUnitUID <= 0L)
			{
				return false;
			}
			if (this.m_Options.bHideNotPossibleSetOptionItem)
			{
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(equipData.m_ItemEquipID);
				if (equipTemplet != null && (equipTemplet.SetGroupList == null || equipTemplet.SetGroupList.Count <= 0))
				{
					return false;
				}
			}
			if (this.m_Options.iTargetUnitID != 0)
			{
				NKMEquipTemplet equipTemplet2 = NKMItemManager.GetEquipTemplet(equipData.m_ItemEquipID);
				if (equipTemplet2 != null && equipTemplet2.IsPrivateEquip() && !equipTemplet2.IsPrivateEquipForUnit(this.m_Options.iTargetUnitID))
				{
					return false;
				}
			}
			if (setFilter == null || setFilter.Count == 0)
			{
				return true;
			}
			for (int i = 0; i < setFilter.Count; i++)
			{
				if (!NKCEquipSortSystem.CheckFilter(equipData, setFilter[i], this.m_Options))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003467 RID: 13415 RVA: 0x00107DE5 File Offset: 0x00105FE5
		protected bool IsEquipSelectable(NKMEquipItemData equipData)
		{
			return true;
		}

		// Token: 0x06003468 RID: 13416 RVA: 0x00107DE8 File Offset: 0x00105FE8
		private static bool CheckFilter(NKMEquipItemData equipData, HashSet<NKCEquipSortSystem.eFilterOption> setFilter, NKCEquipSortSystem.EquipListOptions equipListOptions)
		{
			foreach (NKCEquipSortSystem.eFilterOption filterOption in setFilter)
			{
				if (NKCEquipSortSystem.CheckFilter(equipData, filterOption, equipListOptions))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003469 RID: 13417 RVA: 0x00107E40 File Offset: 0x00106040
		private static bool CheckFilter(NKMEquipItemData equipData, NKCEquipSortSystem.eFilterOption filterOption, NKCEquipSortSystem.EquipListOptions equipListOptions)
		{
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(equipData.m_ItemEquipID);
			if (equipTemplet == null)
			{
				Debug.LogError(string.Format("UnitTemplet Null! unitID : {0}", equipData.m_ItemEquipID));
				return false;
			}
			switch (filterOption)
			{
			default:
				return false;
			case NKCEquipSortSystem.eFilterOption.All:
				return true;
			case NKCEquipSortSystem.eFilterOption.Equip_Counter:
				if (equipTemplet.m_EquipUnitStyleType == NKM_UNIT_STYLE_TYPE.NUST_COUNTER)
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Soldier:
				if (equipTemplet.m_EquipUnitStyleType == NKM_UNIT_STYLE_TYPE.NUST_SOLDIER)
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Mechanic:
				if (equipTemplet.m_EquipUnitStyleType == NKM_UNIT_STYLE_TYPE.NUST_MECHANIC)
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Weapon:
				if (equipTemplet.m_ItemEquipPosition == ITEM_EQUIP_POSITION.IEP_WEAPON)
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Armor:
				if (equipTemplet.m_ItemEquipPosition == ITEM_EQUIP_POSITION.IEP_DEFENCE)
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Acc:
				if (equipTemplet.m_ItemEquipPosition == ITEM_EQUIP_POSITION.IEP_ACC || equipTemplet.m_ItemEquipPosition == ITEM_EQUIP_POSITION.IEP_ACC2)
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Enchant:
				if (equipTemplet.m_ItemEquipPosition == ITEM_EQUIP_POSITION.IEP_ENCHANT)
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Tier_7:
				if (equipTemplet.m_NKM_ITEM_TIER == 7)
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Tier_6:
				if (equipTemplet.m_NKM_ITEM_TIER == 6)
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Tier_5:
				if (equipTemplet.m_NKM_ITEM_TIER == 5)
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Tier_4:
				if (equipTemplet.m_NKM_ITEM_TIER == 4)
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Tier_3:
				if (equipTemplet.m_NKM_ITEM_TIER == 3)
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Tier_2:
				if (equipTemplet.m_NKM_ITEM_TIER == 2)
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Tier_1:
				if (equipTemplet.m_NKM_ITEM_TIER == 1)
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Rarity_SSR:
				if (equipTemplet.m_NKM_ITEM_GRADE == NKM_ITEM_GRADE.NIG_SSR)
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Rarity_SR:
				if (equipTemplet.m_NKM_ITEM_GRADE == NKM_ITEM_GRADE.NIG_SR)
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Rarity_R:
				if (equipTemplet.m_NKM_ITEM_GRADE == NKM_ITEM_GRADE.NIG_R)
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Rarity_N:
				if (equipTemplet.m_NKM_ITEM_GRADE == NKM_ITEM_GRADE.NIG_N)
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Set_Part_2:
				if (equipData.m_SetOptionId > 0)
				{
					NKMItemEquipSetOptionTemplet equipSetOptionTemplet = NKMItemManager.GetEquipSetOptionTemplet(equipData.m_SetOptionId);
					if (equipSetOptionTemplet != null && equipSetOptionTemplet.m_EquipSetPart == 2)
					{
						return true;
					}
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Set_Part_3:
				if (equipData.m_SetOptionId > 0)
				{
					NKMItemEquipSetOptionTemplet equipSetOptionTemplet2 = NKMItemManager.GetEquipSetOptionTemplet(equipData.m_SetOptionId);
					if (equipSetOptionTemplet2 != null && equipSetOptionTemplet2.m_EquipSetPart == 3)
					{
						return true;
					}
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Set_Part_4:
				if (equipData.m_SetOptionId > 0)
				{
					NKMItemEquipSetOptionTemplet equipSetOptionTemplet3 = NKMItemManager.GetEquipSetOptionTemplet(equipData.m_SetOptionId);
					if (equipSetOptionTemplet3 != null && equipSetOptionTemplet3.m_EquipSetPart == 4)
					{
						return true;
					}
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Set_Effect_Red:
				if (equipData.m_SetOptionId > 0)
				{
					NKMItemEquipSetOptionTemplet equipSetOptionTemplet4 = NKMItemManager.GetEquipSetOptionTemplet(equipData.m_SetOptionId);
					if (equipSetOptionTemplet4 != null && string.Equals(equipSetOptionTemplet4.m_EquipSetIconEffect, "EFFECT_RED"))
					{
						return true;
					}
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Set_Effect_Blue:
				if (equipData.m_SetOptionId > 0)
				{
					NKMItemEquipSetOptionTemplet equipSetOptionTemplet5 = NKMItemManager.GetEquipSetOptionTemplet(equipData.m_SetOptionId);
					if (equipSetOptionTemplet5 != null && string.Equals(equipSetOptionTemplet5.m_EquipSetIconEffect, "EFFECT_BLUE"))
					{
						return true;
					}
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Set_Effect_Yellow:
				if (equipData.m_SetOptionId > 0)
				{
					NKMItemEquipSetOptionTemplet equipSetOptionTemplet6 = NKMItemManager.GetEquipSetOptionTemplet(equipData.m_SetOptionId);
					if (equipSetOptionTemplet6 != null && string.Equals(equipSetOptionTemplet6.m_EquipSetIconEffect, "EFFECT_YELLOW"))
					{
						return true;
					}
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Equipped:
				if (equipData.m_OwnerUnitUID > 0L)
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Unused:
				if (equipData.m_OwnerUnitUID <= 0L)
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Locked:
				if (equipData.m_bLock)
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Unlocked:
				if (!equipData.m_bLock)
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Have:
				if (NKCScenManager.CurrentUserData().m_InventoryData.GetSameKindEquipCount(equipData.m_ItemEquipID, 0) > 0)
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_NotHave:
				if (NKCScenManager.CurrentUserData().m_InventoryData.GetSameKindEquipCount(equipData.m_ItemEquipID, 0) == 0)
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Stat:
			{
				bool flag = true;
				if (equipListOptions.FilterStatType_01 != NKM_STAT_TYPE.NST_RANDOM)
				{
					flag &= (equipData.m_Stat.Find((EQUIP_ITEM_STAT x) => x.type == equipListOptions.FilterStatType_01) != null);
				}
				if (equipListOptions.FilterStatType_02 != NKM_STAT_TYPE.NST_RANDOM)
				{
					flag &= (equipData.m_Stat.Find((EQUIP_ITEM_STAT x) => x.type == equipListOptions.FilterStatType_02) != null);
				}
				if (equipListOptions.FilterStatType_03 != NKM_STAT_TYPE.NST_RANDOM)
				{
					flag &= (equipData.m_Stat.Find((EQUIP_ITEM_STAT x) => x.type == equipListOptions.FilterStatType_03) != null);
				}
				if (equipListOptions.FilterSetOptionID > 0)
				{
					flag &= (equipData.m_SetOptionId == equipListOptions.FilterSetOptionID);
				}
				if (flag)
				{
					return true;
				}
				break;
			}
			case NKCEquipSortSystem.eFilterOption.Equip_Private:
				if (equipTemplet.IsPrivateEquip())
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Non_Private:
				if (!equipTemplet.IsPrivateEquip() && !equipTemplet.m_bRelic)
				{
					return true;
				}
				break;
			case NKCEquipSortSystem.eFilterOption.Equip_Relic:
				if (equipTemplet.m_bRelic)
				{
					return true;
				}
				break;
			}
			return false;
		}

		// Token: 0x0600346A RID: 13418 RVA: 0x001082AC File Offset: 0x001064AC
		public void FilterList(NKCEquipSortSystem.eFilterOption filterOption, bool bHideDeckedUnit)
		{
			this.FilterList(new HashSet<NKCEquipSortSystem.eFilterOption>
			{
				filterOption
			}, bHideDeckedUnit);
		}

		// Token: 0x0600346B RID: 13419 RVA: 0x001082D0 File Offset: 0x001064D0
		public virtual void FilterList(HashSet<NKCEquipSortSystem.eFilterOption> setFilter, bool bHideEquippedItem)
		{
			this.m_Options.setFilterOption = setFilter;
			this.m_Options.bHideEquippedItem = bHideEquippedItem;
			if (this.m_lstCurrentEquipList == null)
			{
				this.m_lstCurrentEquipList = new List<NKMEquipItemData>();
			}
			this.m_lstCurrentEquipList.Clear();
			List<HashSet<NKCEquipSortSystem.eFilterOption>> setFilter2 = NKCEquipSortSystem.SetFilterCategory(setFilter);
			foreach (KeyValuePair<long, NKMEquipItemData> keyValuePair in this.m_dicAllEquipList)
			{
				NKMEquipItemData value = keyValuePair.Value;
				if (this.FilterData(value, setFilter2))
				{
					this.m_lstCurrentEquipList.Add(value);
				}
			}
			if (this.m_Options.lstSortOption != null)
			{
				this.SortList(this.m_Options.lstSortOption, true);
				return;
			}
			this.m_Options.lstSortOption = new List<NKCEquipSortSystem.eSortOption>();
			this.SortList(this.m_Options.lstSortOption, true);
		}

		// Token: 0x0600346C RID: 13420 RVA: 0x001083BC File Offset: 0x001065BC
		private static List<HashSet<NKCEquipSortSystem.eFilterOption>> SetFilterCategory(HashSet<NKCEquipSortSystem.eFilterOption> setFilter)
		{
			List<HashSet<NKCEquipSortSystem.eFilterOption>> list = new List<HashSet<NKCEquipSortSystem.eFilterOption>>();
			if (setFilter.Count == 0)
			{
				return list;
			}
			for (int i = 0; i < NKCEquipSortSystem.m_lstFilterCategory.Count; i++)
			{
				HashSet<NKCEquipSortSystem.eFilterOption> hashSet = new HashSet<NKCEquipSortSystem.eFilterOption>();
				foreach (NKCEquipSortSystem.eFilterOption item in setFilter)
				{
					hashSet.Add(item);
				}
				hashSet.IntersectWith(NKCEquipSortSystem.m_lstFilterCategory[i]);
				if (hashSet.Count > 0)
				{
					list.Add(hashSet);
				}
			}
			return list;
		}

		// Token: 0x0600346D RID: 13421 RVA: 0x0010845C File Offset: 0x0010665C
		public void SortList(NKCEquipSortSystem.eSortOption sortOption, bool bForce = false)
		{
			this.SortList(new List<NKCEquipSortSystem.eSortOption>
			{
				sortOption
			}, bForce);
		}

		// Token: 0x0600346E RID: 13422 RVA: 0x00108480 File Offset: 0x00106680
		public void SortList(List<NKCEquipSortSystem.eSortOption> lstSortOption, bool bForce = false)
		{
			if (this.m_lstCurrentEquipList != null)
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
				this.SortEquipDataList(ref this.m_lstCurrentEquipList, lstSortOption);
				this.m_Options.lstSortOption = lstSortOption;
				return;
			}
			this.m_Options.lstSortOption = lstSortOption;
			if (this.m_Options.setFilterOption != null)
			{
				this.FilterList(this.m_Options.setFilterOption, this.m_Options.bHideEquippedItem);
				return;
			}
			this.m_Options.setFilterOption = new HashSet<NKCEquipSortSystem.eFilterOption>();
			this.FilterList(this.m_Options.setFilterOption, this.m_Options.bHideEquippedItem);
		}

		// Token: 0x0600346F RID: 13423 RVA: 0x00108564 File Offset: 0x00106764
		private void SortEquipDataList(ref List<NKMEquipItemData> lstEquipData, List<NKCEquipSortSystem.eSortOption> lstSortOption)
		{
			NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData> nkcdataComparerer = new NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>(Array.Empty<NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc>());
			if (this.m_Options.PreemptiveSortFunc != null)
			{
				nkcdataComparerer.AddFunc(this.m_Options.PreemptiveSortFunc);
			}
			if (this.m_Options.bPushBackUnselectable)
			{
				nkcdataComparerer.AddFunc(new NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc(this.CompareBySelectable));
			}
			this.m_dicCacheOperationPower.Clear();
			this.m_dicCachePriorityByClassValue.Clear();
			foreach (NKCEquipSortSystem.eSortOption eSortOption in lstSortOption)
			{
				if (eSortOption == NKCEquipSortSystem.eSortOption.OperationPower_High || eSortOption == NKCEquipSortSystem.eSortOption.OperationPower_Low)
				{
					this.UpdateCacheOperationPowerData(lstEquipData);
				}
				if (eSortOption == NKCEquipSortSystem.eSortOption.OptionWeightByClassType_High || eSortOption == NKCEquipSortSystem.eSortOption.OptionWeightByClassType_Low)
				{
					this.UpdateCachePriorityByClassData(lstEquipData);
				}
				nkcdataComparerer.AddFunc(this.GetDataComparer(eSortOption));
			}
			if (!lstSortOption.Contains(NKCEquipSortSystem.eSortOption.UID_First) && !lstSortOption.Contains(NKCEquipSortSystem.eSortOption.UID_Last))
			{
				nkcdataComparerer.AddFunc(new NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc(this.CompareByUIDAscending));
			}
			lstEquipData.Sort(nkcdataComparerer);
		}

		// Token: 0x06003470 RID: 13424 RVA: 0x00108668 File Offset: 0x00106868
		private void UpdateCacheOperationPowerData(List<NKMEquipItemData> lstEquipData)
		{
			if (this.m_dicCacheOperationPower.Count > 0)
			{
				return;
			}
			foreach (NKMEquipItemData nkmequipItemData in lstEquipData)
			{
				if (nkmequipItemData != null)
				{
					NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(nkmequipItemData.m_ItemEquipID);
					if (equipTemplet != null)
					{
						float equipOperationPower = this.GetEquipOperationPower(equipTemplet.m_NKM_ITEM_TIER, (int)equipTemplet.m_NKM_ITEM_GRADE, nkmequipItemData.m_EnchantLevel, nkmequipItemData.m_Precision, nkmequipItemData.m_Precision2);
						Debug.Log(string.Format("작전능력 테스트 - 아이템 : {0}, 티어 : {1} 등급: {2}, 레벨:{3} 조정률1:{4} 조정률2:{5} 작전능력 : {6}", new object[]
						{
							equipTemplet.GetItemName(),
							equipTemplet.m_NKM_ITEM_TIER,
							(int)equipTemplet.m_NKM_ITEM_GRADE,
							nkmequipItemData.m_EnchantLevel,
							nkmequipItemData.m_Precision,
							nkmequipItemData.m_Precision2,
							equipOperationPower.ToString()
						}));
						this.m_dicCacheOperationPower.Add(nkmequipItemData.m_ItemUid, equipOperationPower);
					}
				}
			}
		}

		// Token: 0x06003471 RID: 13425 RVA: 0x00108784 File Offset: 0x00106984
		private float GetEquipOperationPower(int Tier, int Grade, int level, int Precision1, int Precision2)
		{
			float num = (float)(100 * (Tier + 1));
			float num2 = (float)(Grade * (30 + 15 * (Tier - 1)));
			float num3 = (num + num2) * (1f + 0.075f * (float)level);
			if (Precision2 > 0)
			{
				float num4 = (float)(100 * (Tier + 1) + Grade * (30 + 15 * (Tier - 1))) * (0.5f + (float)Precision1 * 0.01f * 0.5f);
				float num5 = (float)(100 * (Tier + 1) + Grade * (30 + 15 * (Tier - 1))) * (0.5f + (float)Precision2 * 0.01f * 0.5f);
				return num3 + num4 + num5;
			}
			return num3 + (float)((4 + Tier * 5) * 8) * (0.5f + (float)Precision1 * 0.01f * 0.5f);
		}

		// Token: 0x06003472 RID: 13426 RVA: 0x00108838 File Offset: 0x00106A38
		private void UpdateCachePriorityByClassData(List<NKMEquipItemData> lstEquipData)
		{
			if (this.m_dicCachePriorityByClassValue.Count > 0)
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKMInventoryData nkminventoryData = null;
			if (nkmuserData != null)
			{
				nkminventoryData = nkmuserData.m_InventoryData;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_Options.OwnerUnitID);
			foreach (NKMEquipItemData nkmequipItemData in lstEquipData)
			{
				if (nkmequipItemData != null)
				{
					NKMEquipItemData nkmequipItemData2 = null;
					if (nkminventoryData != null)
					{
						nkmequipItemData2 = nkminventoryData.GetItemEquip(nkmequipItemData.m_ItemUid);
						if (nkmequipItemData2 == null)
						{
							continue;
						}
					}
					NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(nkmequipItemData.m_ItemEquipID);
					if (equipTemplet != null)
					{
						float equipPriorityValueByClass = this.GetEquipPriorityValueByClass(unitTempletBase, nkmequipItemData2, equipTemplet);
						Debug.Log(string.Format("테스트 적용 정보 : {0}, 장착 부위 : {1} 조정률1:{2} 조정률2:{3} 결과 : {4}", new object[]
						{
							equipTemplet.GetItemName(),
							equipTemplet.m_ItemEquipPosition,
							nkmequipItemData.m_Precision,
							nkmequipItemData.m_Precision2,
							equipPriorityValueByClass
						}));
						this.m_dicCachePriorityByClassValue.Add(nkmequipItemData.m_ItemUid, equipPriorityValueByClass);
					}
				}
			}
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x00108964 File Offset: 0x00106B64
		private float GetEquipPriorityValueByClass(NKMUnitTempletBase unitTemplet, NKMEquipItemData itemData, NKMEquipTemplet equipTemplet)
		{
			NKM_STAT_TYPE nkm_STAT_TYPE = NKM_STAT_TYPE.NST_RANDOM;
			if (equipTemplet.m_ItemEquipPosition == ITEM_EQUIP_POSITION.IEP_ACC || equipTemplet.m_ItemEquipPosition == ITEM_EQUIP_POSITION.IEP_ACC2)
			{
				if (unitTemplet.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_RANGER || unitTemplet.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_SNIPER)
				{
					nkm_STAT_TYPE = NKM_STAT_TYPE.NST_HIT;
				}
				else
				{
					nkm_STAT_TYPE = NKM_STAT_TYPE.NST_EVADE;
				}
			}
			float num = 1f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			NKM_FIND_TARGET_TYPE nkm_FIND_TARGET_TYPE = (unitTemplet.m_NKM_FIND_TARGET_TYPE_Desc == NKM_FIND_TARGET_TYPE.NFTT_INVALID) ? unitTemplet.m_NKM_FIND_TARGET_TYPE : unitTemplet.m_NKM_FIND_TARGET_TYPE_Desc;
			for (int i = 0; i < itemData.m_Stat.Count; i++)
			{
				EQUIP_ITEM_STAT equip_ITEM_STAT = itemData.m_Stat[i];
				if (i == 0)
				{
					if (nkm_STAT_TYPE != NKM_STAT_TYPE.NST_RANDOM && equip_ITEM_STAT.type != nkm_STAT_TYPE)
					{
						num = 0.5f;
					}
					if (NKMUnitStatManager.IsPercentStat(equip_ITEM_STAT.type))
					{
						num2 = (float)(Math.Round(new decimal(equip_ITEM_STAT.stat_value + (float)itemData.m_EnchantLevel * equip_ITEM_STAT.stat_level_value) * 1000m) / 1000m);
					}
					else
					{
						num2 = equip_ITEM_STAT.stat_value + (float)itemData.m_EnchantLevel * equip_ITEM_STAT.stat_level_value;
					}
				}
				else if ((equip_ITEM_STAT.type != NKM_STAT_TYPE.NST_MOVE_TYPE_LAND_DAMAGE_RATE || unitTemplet.m_NKM_FIND_TARGET_TYPE != NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_AIR) && (equip_ITEM_STAT.type != NKM_STAT_TYPE.NST_MOVE_TYPE_AIR_DAMAGE_RATE || nkm_FIND_TARGET_TYPE != NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_LAND))
				{
					if (i == 1)
					{
						num3 = NKMItemManager.GetOptionWeight(equip_ITEM_STAT.type, unitTemplet.m_NKM_UNIT_ROLE_TYPE);
					}
					else if (i == 2)
					{
						num4 = NKMItemManager.GetOptionWeight(equip_ITEM_STAT.type, unitTemplet.m_NKM_UNIT_ROLE_TYPE);
					}
				}
			}
			Debug.Log(string.Format("주옵션 능력치 : {0}, a : {1}, 옵션1계수 : {2} , 옵션1정밀도 : {3}, 옵션2계수 : {4} , 옵션1정밀도 : {5}", new object[]
			{
				num2,
				num,
				num3,
				(float)itemData.m_Precision * 0.01f,
				num4,
				(float)itemData.m_Precision2 * 0.01f
			}));
			return num2 * (num + num3 * ((float)itemData.m_Precision * 0.01f) + num4 * ((float)itemData.m_Precision2 * 0.01f));
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x00108B68 File Offset: 0x00106D68
		private NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc GetDataComparer(NKCEquipSortSystem.eSortOption sortOption)
		{
			switch (sortOption)
			{
			default:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc(this.CompareByEnhanceDescending);
			case NKCEquipSortSystem.eSortOption.Enhance_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc(this.CompareByEnhanceAscending);
			case NKCEquipSortSystem.eSortOption.Tier_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc(this.CompareByTierDescending);
			case NKCEquipSortSystem.eSortOption.Tier_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc(this.CompareByTierAscending);
			case NKCEquipSortSystem.eSortOption.Rarity_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc(this.CompareByRarityDescending);
			case NKCEquipSortSystem.eSortOption.Rarity_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc(this.CompareByRarityAscending);
			case NKCEquipSortSystem.eSortOption.UID_First:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc(this.CompareByUIDAscending);
			case NKCEquipSortSystem.eSortOption.UID_Last:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc(this.CompareByUIDDescending);
			case NKCEquipSortSystem.eSortOption.SetOption_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc(this.CompareBySetOptionHigh);
			case NKCEquipSortSystem.eSortOption.SetOption_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc(this.CompareBySetOptionLow);
			case NKCEquipSortSystem.eSortOption.OperationPower_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc(this.CompareByOperationPowerHigh);
			case NKCEquipSortSystem.eSortOption.OperationPower_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc(this.CompareByOperationPowerLow);
			case NKCEquipSortSystem.eSortOption.OptionWeightByClassType_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc(this.CompareByPriorityByClassHigh);
			case NKCEquipSortSystem.eSortOption.OptionWeightByClassType_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc(this.CompareByPriorityByClassLow);
			case NKCEquipSortSystem.eSortOption.Equipped_First:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc(this.CompareByEquippedFirst);
			case NKCEquipSortSystem.eSortOption.Equipped_Last:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc(this.CompareByEquippedLast);
			case NKCEquipSortSystem.eSortOption.ID_First:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc(this.CompareByIDAscending);
			case NKCEquipSortSystem.eSortOption.ID_Last:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc(this.CompareByIDDescending);
			case NKCEquipSortSystem.eSortOption.CustomAscend1:
			case NKCEquipSortSystem.eSortOption.CustomAscend2:
			case NKCEquipSortSystem.eSortOption.CustomAscend3:
				if (this.m_Options.lstCustomSortFunc.ContainsKey(NKCEquipSortSystem.GetSortCategoryFromOption(sortOption)))
				{
					return (NKMEquipItemData a, NKMEquipItemData b) => this.m_Options.lstCustomSortFunc[NKCEquipSortSystem.GetSortCategoryFromOption(sortOption)].Value(b, a);
				}
				return null;
			case NKCEquipSortSystem.eSortOption.CustomDescend1:
			case NKCEquipSortSystem.eSortOption.CustomDescend2:
			case NKCEquipSortSystem.eSortOption.CustomDescend3:
				if (this.m_Options.lstCustomSortFunc.ContainsKey(NKCEquipSortSystem.GetSortCategoryFromOption(sortOption)))
				{
					return this.m_Options.lstCustomSortFunc[NKCEquipSortSystem.GetSortCategoryFromOption(sortOption)].Value;
				}
				return null;
			}
		}

		// Token: 0x06003475 RID: 13429 RVA: 0x00108D66 File Offset: 0x00106F66
		private int CompareByUIDAscending(NKMEquipItemData lhs, NKMEquipItemData rhs)
		{
			return lhs.m_ItemUid.CompareTo(rhs.m_ItemUid);
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x00108D79 File Offset: 0x00106F79
		private int CompareByUIDDescending(NKMEquipItemData lhs, NKMEquipItemData rhs)
		{
			return rhs.m_ItemUid.CompareTo(lhs.m_ItemUid);
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x00108D8C File Offset: 0x00106F8C
		private int CompareByEquippedFirst(NKMEquipItemData lhs, NKMEquipItemData rhs)
		{
			if (lhs.m_OwnerUnitUID == -1L || rhs.m_OwnerUnitUID == -1L)
			{
				return rhs.m_OwnerUnitUID.CompareTo(lhs.m_OwnerUnitUID);
			}
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			NKMDeckIndex deckIndexByUnitUID = armyData.GetDeckIndexByUnitUID(NKM_DECK_TYPE.NDT_NORMAL, lhs.m_OwnerUnitUID);
			NKMDeckIndex deckIndexByUnitUID2 = armyData.GetDeckIndexByUnitUID(NKM_DECK_TYPE.NDT_NORMAL, rhs.m_OwnerUnitUID);
			if (deckIndexByUnitUID.m_eDeckType != NKM_DECK_TYPE.NDT_NONE && deckIndexByUnitUID2.m_eDeckType != NKM_DECK_TYPE.NDT_NONE)
			{
				return deckIndexByUnitUID.m_iIndex.CompareTo(deckIndexByUnitUID2.m_iIndex);
			}
			return deckIndexByUnitUID2.m_eDeckType.CompareTo(deckIndexByUnitUID.m_eDeckType);
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x00108E24 File Offset: 0x00107024
		private int CompareByEquippedLast(NKMEquipItemData lhs, NKMEquipItemData rhs)
		{
			if (lhs.m_OwnerUnitUID == -1L || rhs.m_OwnerUnitUID == -1L)
			{
				return lhs.m_OwnerUnitUID.CompareTo(rhs.m_OwnerUnitUID);
			}
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			NKMDeckIndex deckIndexByUnitUID = armyData.GetDeckIndexByUnitUID(NKM_DECK_TYPE.NDT_NORMAL, lhs.m_OwnerUnitUID);
			NKMDeckIndex deckIndexByUnitUID2 = armyData.GetDeckIndexByUnitUID(NKM_DECK_TYPE.NDT_NORMAL, rhs.m_OwnerUnitUID);
			if (deckIndexByUnitUID.m_eDeckType != NKM_DECK_TYPE.NDT_NONE && deckIndexByUnitUID2.m_eDeckType != NKM_DECK_TYPE.NDT_NONE)
			{
				return deckIndexByUnitUID.m_iIndex.CompareTo(deckIndexByUnitUID2.m_iIndex);
			}
			return deckIndexByUnitUID.m_eDeckType.CompareTo(deckIndexByUnitUID2.m_eDeckType);
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x00108EBC File Offset: 0x001070BC
		private int CompareByEnhanceAscending(NKMEquipItemData lhs, NKMEquipItemData rhs)
		{
			return lhs.m_EnchantLevel.CompareTo(rhs.m_EnchantLevel);
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x00108ECF File Offset: 0x001070CF
		private int CompareByEnhanceDescending(NKMEquipItemData lhs, NKMEquipItemData rhs)
		{
			return rhs.m_EnchantLevel.CompareTo(lhs.m_EnchantLevel);
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x00108EE4 File Offset: 0x001070E4
		private int CompareByTierAscending(NKMEquipItemData lhs, NKMEquipItemData rhs)
		{
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(lhs.m_ItemEquipID);
			NKMEquipTemplet equipTemplet2 = NKMItemManager.GetEquipTemplet(rhs.m_ItemEquipID);
			if (equipTemplet == null || equipTemplet2 == null)
			{
				return -1;
			}
			return equipTemplet.m_NKM_ITEM_TIER.CompareTo(equipTemplet2.m_NKM_ITEM_TIER);
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x00108F24 File Offset: 0x00107124
		private int CompareByTierDescending(NKMEquipItemData lhs, NKMEquipItemData rhs)
		{
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(lhs.m_ItemEquipID);
			NKMEquipTemplet equipTemplet2 = NKMItemManager.GetEquipTemplet(rhs.m_ItemEquipID);
			if (equipTemplet == null || equipTemplet2 == null)
			{
				return -1;
			}
			return equipTemplet2.m_NKM_ITEM_TIER.CompareTo(equipTemplet.m_NKM_ITEM_TIER);
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x00108F64 File Offset: 0x00107164
		private int CompareByRarityAscending(NKMEquipItemData lhs, NKMEquipItemData rhs)
		{
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(lhs.m_ItemEquipID);
			NKMEquipTemplet equipTemplet2 = NKMItemManager.GetEquipTemplet(rhs.m_ItemEquipID);
			if (equipTemplet == null || equipTemplet2 == null)
			{
				return -1;
			}
			return equipTemplet.m_NKM_ITEM_GRADE.CompareTo(equipTemplet2.m_NKM_ITEM_GRADE);
		}

		// Token: 0x0600347E RID: 13438 RVA: 0x00108FB0 File Offset: 0x001071B0
		private int CompareByRarityDescending(NKMEquipItemData lhs, NKMEquipItemData rhs)
		{
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(lhs.m_ItemEquipID);
			NKMEquipTemplet equipTemplet2 = NKMItemManager.GetEquipTemplet(rhs.m_ItemEquipID);
			if (equipTemplet == null || equipTemplet2 == null)
			{
				return -1;
			}
			return equipTemplet2.m_NKM_ITEM_GRADE.CompareTo(equipTemplet.m_NKM_ITEM_GRADE);
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x00108FF9 File Offset: 0x001071F9
		private int CompareByIDAscending(NKMEquipItemData lhs, NKMEquipItemData rhs)
		{
			return lhs.m_ItemEquipID.CompareTo(rhs.m_ItemEquipID);
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x0010900C File Offset: 0x0010720C
		private int CompareByIDDescending(NKMEquipItemData lhs, NKMEquipItemData rhs)
		{
			return rhs.m_ItemEquipID.CompareTo(lhs.m_ItemEquipID);
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x00109020 File Offset: 0x00107220
		private int CompareBySelectable(NKMEquipItemData lhs, NKMEquipItemData rhs)
		{
			return this.IsEquipSelectable(rhs).CompareTo(this.IsEquipSelectable(lhs));
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x00109044 File Offset: 0x00107244
		private int CompareByOperationPowerHigh(NKMEquipItemData lhs, NKMEquipItemData rhs)
		{
			if (!this.m_dicCacheOperationPower.ContainsKey(lhs.m_ItemUid) || !this.m_dicCacheOperationPower.ContainsKey(rhs.m_ItemUid))
			{
				return 0;
			}
			float value = this.m_dicCacheOperationPower[lhs.m_ItemUid];
			return this.m_dicCacheOperationPower[rhs.m_ItemUid].CompareTo(value);
		}

		// Token: 0x06003483 RID: 13443 RVA: 0x001090A8 File Offset: 0x001072A8
		private int CompareByOperationPowerLow(NKMEquipItemData lhs, NKMEquipItemData rhs)
		{
			if (!this.m_dicCacheOperationPower.ContainsKey(lhs.m_ItemUid) || !this.m_dicCacheOperationPower.ContainsKey(rhs.m_ItemUid))
			{
				return 0;
			}
			float num = this.m_dicCacheOperationPower[lhs.m_ItemUid];
			float value = this.m_dicCacheOperationPower[rhs.m_ItemUid];
			return num.CompareTo(value);
		}

		// Token: 0x06003484 RID: 13444 RVA: 0x0010910C File Offset: 0x0010730C
		private int CompareByPriorityByClassHigh(NKMEquipItemData lhs, NKMEquipItemData rhs)
		{
			if (!this.m_dicCachePriorityByClassValue.ContainsKey(lhs.m_ItemUid) || !this.m_dicCachePriorityByClassValue.ContainsKey(rhs.m_ItemUid))
			{
				return 0;
			}
			float value = this.m_dicCachePriorityByClassValue[lhs.m_ItemUid];
			return this.m_dicCachePriorityByClassValue[rhs.m_ItemUid].CompareTo(value);
		}

		// Token: 0x06003485 RID: 13445 RVA: 0x00109170 File Offset: 0x00107370
		private int CompareByPriorityByClassLow(NKMEquipItemData lhs, NKMEquipItemData rhs)
		{
			if (!this.m_dicCachePriorityByClassValue.ContainsKey(lhs.m_ItemUid) || !this.m_dicCachePriorityByClassValue.ContainsKey(rhs.m_ItemUid))
			{
				return 0;
			}
			float num = this.m_dicCachePriorityByClassValue[lhs.m_ItemUid];
			float value = this.m_dicCachePriorityByClassValue[rhs.m_ItemUid];
			return num.CompareTo(value);
		}

		// Token: 0x06003486 RID: 13446 RVA: 0x001091D1 File Offset: 0x001073D1
		private int CompareBySetOptionHigh(NKMEquipItemData lhs, NKMEquipItemData rhs)
		{
			if (lhs.m_SetOptionId <= 0 || rhs.m_SetOptionId <= 0)
			{
				return 0;
			}
			return rhs.m_SetOptionId.CompareTo(lhs.m_SetOptionId);
		}

		// Token: 0x06003487 RID: 13447 RVA: 0x001091F8 File Offset: 0x001073F8
		private int CompareBySetOptionLow(NKMEquipItemData lhs, NKMEquipItemData rhs)
		{
			if (lhs.m_SetOptionId <= 0 || rhs.m_SetOptionId <= 0)
			{
				return 0;
			}
			return lhs.m_SetOptionId.CompareTo(rhs.m_SetOptionId);
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x0010921F File Offset: 0x0010741F
		public static string GetFilterName(NKCEquipSortSystem.eFilterOption type)
		{
			if (type == NKCEquipSortSystem.eFilterOption.All)
			{
				return NKCUtilString.GET_STRING_FILTER_ALL;
			}
			return "";
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x00109230 File Offset: 0x00107430
		public static NKCEquipSortSystem.eFilterOption GetFilterOptionByEquipPosition(ITEM_EQUIP_POSITION m_ITEM_EQUIP_POSITION)
		{
			switch (m_ITEM_EQUIP_POSITION)
			{
			case ITEM_EQUIP_POSITION.IEP_WEAPON:
				return NKCEquipSortSystem.eFilterOption.Equip_Weapon;
			case ITEM_EQUIP_POSITION.IEP_DEFENCE:
				return NKCEquipSortSystem.eFilterOption.Equip_Armor;
			case ITEM_EQUIP_POSITION.IEP_ACC:
			case ITEM_EQUIP_POSITION.IEP_ACC2:
				return NKCEquipSortSystem.eFilterOption.Equip_Acc;
			default:
				return NKCEquipSortSystem.eFilterOption.All;
			}
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x00109254 File Offset: 0x00107454
		public static string GetSortName(NKCEquipSortSystem.eSortOption option)
		{
			switch (option)
			{
			default:
				return NKCUtilString.GET_STRING_SORT_ENHANCE;
			case NKCEquipSortSystem.eSortOption.Tier_High:
			case NKCEquipSortSystem.eSortOption.Tier_Low:
				return NKCUtilString.GET_STRING_SORT_TIER;
			case NKCEquipSortSystem.eSortOption.Rarity_High:
			case NKCEquipSortSystem.eSortOption.Rarity_Low:
				return NKCUtilString.GET_STRING_SORT_RARITY;
			case NKCEquipSortSystem.eSortOption.UID_First:
			case NKCEquipSortSystem.eSortOption.UID_Last:
				return NKCUtilString.GET_STRING_SORT_UID;
			case NKCEquipSortSystem.eSortOption.SetOption_High:
			case NKCEquipSortSystem.eSortOption.SetOption_Low:
				return NKCUtilString.GET_STRING_SORT_SETOPTION;
			}
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x001092AC File Offset: 0x001074AC
		public static bool GetDescendingBySorting(List<NKCEquipSortSystem.eSortOption> lstSortOption)
		{
			return lstSortOption.Count <= 0 || NKCEquipSortSystem.GetDescendingBySorting(lstSortOption[0]);
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x001092C8 File Offset: 0x001074C8
		public static bool GetDescendingBySorting(NKCEquipSortSystem.eSortOption sortOption)
		{
			switch (sortOption)
			{
			default:
				return true;
			case NKCEquipSortSystem.eSortOption.Enhance_Low:
			case NKCEquipSortSystem.eSortOption.Tier_Low:
			case NKCEquipSortSystem.eSortOption.Rarity_Low:
			case NKCEquipSortSystem.eSortOption.UID_First:
			case NKCEquipSortSystem.eSortOption.SetOption_Low:
			case NKCEquipSortSystem.eSortOption.OperationPower_Low:
			case NKCEquipSortSystem.eSortOption.OptionWeightByClassType_Low:
			case NKCEquipSortSystem.eSortOption.Equipped_Last:
			case NKCEquipSortSystem.eSortOption.UnitType_Last:
			case NKCEquipSortSystem.eSortOption.EquipType_Last:
			case NKCEquipSortSystem.eSortOption.ID_Last:
				return false;
			}
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x00109338 File Offset: 0x00107538
		public NKMEquipItemData AutoSelect(HashSet<long> setExcludeEquipUid, NKCEquipSortSystem.AutoSelectExtraFilter extrafilter = null)
		{
			for (int i = 0; i < this.SortedEquipList.Count; i++)
			{
				NKMEquipItemData nkmequipItemData = this.SortedEquipList[i];
				if (nkmequipItemData != null && (setExcludeEquipUid == null || setExcludeEquipUid.Count <= 0 || !setExcludeEquipUid.Contains(nkmequipItemData.m_ItemUid)) && (extrafilter == null || extrafilter(nkmequipItemData)) && this.IsEquipSelectable(nkmequipItemData))
				{
					return nkmequipItemData;
				}
			}
			return null;
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x001093A0 File Offset: 0x001075A0
		public static NKMEquipItemData MakeTempEquipData(int equipID, int setOptionID = 0, bool bMaxValue = false)
		{
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(equipID);
			if (equipTemplet == null)
			{
				return new NKMEquipItemData();
			}
			NKMEquipItemData nkmequipItemData = new NKMEquipItemData((long)equipID, equipTemplet);
			nkmequipItemData.m_EnchantLevel = 0;
			nkmequipItemData.m_EnchantExp = 0;
			nkmequipItemData.m_OwnerUnitUID = -1L;
			nkmequipItemData.m_bLock = false;
			nkmequipItemData.m_Precision = (bMaxValue ? 100 : 0);
			nkmequipItemData.m_Precision2 = (bMaxValue ? 100 : 0);
			nkmequipItemData.m_SetOptionId = setOptionID;
			if (bMaxValue)
			{
				NKMEquipTemplet equipTemplet2 = NKMItemManager.GetEquipTemplet(equipID);
				if (equipTemplet2 != null)
				{
					nkmequipItemData.m_EnchantLevel = equipTemplet2.m_MaxEnchantLevel;
				}
			}
			if (equipTemplet.m_ItemEquipPosition == ITEM_EQUIP_POSITION.IEP_ENCHANT)
			{
				return nkmequipItemData;
			}
			if (equipTemplet.m_StatGroupID > 0)
			{
				EQUIP_ITEM_STAT equip_ITEM_STAT = new EQUIP_ITEM_STAT();
				IReadOnlyList<NKMEquipRandomStatTemplet> equipRandomStatGroupList = NKMEquipTuningManager.GetEquipRandomStatGroupList(equipTemplet.m_StatGroupID);
				if (equipRandomStatGroupList != null && equipRandomStatGroupList.Count == 1)
				{
					equip_ITEM_STAT.type = equipRandomStatGroupList[0].m_StatType;
					equip_ITEM_STAT.stat_value = 0f;
					if (bMaxValue)
					{
						NKCEquipSortSystem.SetMaximumStatValue(ref equip_ITEM_STAT, equipRandomStatGroupList[0]);
					}
				}
				else
				{
					equip_ITEM_STAT.type = NKM_STAT_TYPE.NST_RANDOM;
				}
				nkmequipItemData.m_Stat.Add(equip_ITEM_STAT);
			}
			if (equipTemplet.m_StatGroupID_2 > 0)
			{
				EQUIP_ITEM_STAT equip_ITEM_STAT2 = new EQUIP_ITEM_STAT();
				IReadOnlyList<NKMEquipRandomStatTemplet> equipRandomStatGroupList2 = NKMEquipTuningManager.GetEquipRandomStatGroupList(equipTemplet.m_StatGroupID_2);
				if (equipRandomStatGroupList2 != null && equipRandomStatGroupList2.Count == 1)
				{
					equip_ITEM_STAT2.type = equipRandomStatGroupList2[0].m_StatType;
					equip_ITEM_STAT2.stat_value = 0f;
					if (bMaxValue)
					{
						NKCEquipSortSystem.SetMaximumStatValue(ref equip_ITEM_STAT2, equipRandomStatGroupList2[0]);
					}
				}
				else
				{
					equip_ITEM_STAT2.type = NKM_STAT_TYPE.NST_RANDOM;
				}
				nkmequipItemData.m_Stat.Add(equip_ITEM_STAT2);
			}
			return nkmequipItemData;
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x00109511 File Offset: 0x00107711
		private static void SetMaximumStatValue(ref EQUIP_ITEM_STAT stat, NKMEquipRandomStatTemplet randomStatTemplet)
		{
			if (randomStatTemplet.m_MaxStatValue > 0f)
			{
				stat.stat_value = randomStatTemplet.m_MaxStatValue;
				return;
			}
			if (randomStatTemplet.m_MaxStatRate > 0f)
			{
				stat.stat_factor = randomStatTemplet.m_MaxStatRate;
			}
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x00109548 File Offset: 0x00107748
		public static List<NKMEquipItemData> MakeTempEquipDataWithAllSet(int equipID)
		{
			List<NKMEquipItemData> list = new List<NKMEquipItemData>();
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(equipID);
			if (equipTemplet != null && equipTemplet.SetGroupList != null && equipTemplet.SetGroupList.Count > 0)
			{
				for (int i = 0; i < equipTemplet.SetGroupList.Count; i++)
				{
					NKMEquipItemData nkmequipItemData = NKCEquipSortSystem.MakeTempEquipData(equipID, equipTemplet.SetGroupList[i], false);
					nkmequipItemData.m_ItemUid = (long)(equipID * 100 + i);
					list.Add(nkmequipItemData);
				}
			}
			return list;
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x001095BA File Offset: 0x001077BA
		public void GetCurrentEquipList(ref List<NKMEquipItemData> currentList)
		{
			currentList = this.m_lstCurrentEquipList;
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x001095C4 File Offset: 0x001077C4
		public static List<NKCEquipSortSystem.eSortOption> GetDefaultSortOption(NKCPopupEquipSort.SORT_OPEN_TYPE openType = NKCPopupEquipSort.SORT_OPEN_TYPE.NORMAL)
		{
			switch (openType)
			{
			default:
				return NKCEquipSortSystem.DEFAULT_EQUIP_SORT_LIST;
			case NKCPopupEquipSort.SORT_OPEN_TYPE.SELECTION:
				return NKCEquipSortSystem.DEFAULT_EQUIP_SELECTION_SORT_LIST;
			case NKCPopupEquipSort.SORT_OPEN_TYPE.OPERATION_POWER:
				return NKCEquipSortSystem.DEFAULT_EQUIP_OPERATION_POWER_SORT_LIST;
			case NKCPopupEquipSort.SORT_OPEN_TYPE.OPTION_WEIGHT:
				return NKCEquipSortSystem.DEFAULT_EQUIP_OPTION_WEIGHT_SORT_LIST;
			}
		}

		// Token: 0x06003493 RID: 13459 RVA: 0x001095F7 File Offset: 0x001077F7
		public static List<NKCEquipSortSystem.eSortOption> AddDefaultSortOptions(List<NKCEquipSortSystem.eSortOption> sortOptions)
		{
			sortOptions.AddRange(NKCEquipSortSystem.GetDefaultSortOption(NKCPopupEquipSort.SORT_OPEN_TYPE.NORMAL));
			return sortOptions;
		}

		// Token: 0x040032B3 RID: 12979
		private const int ITEM_TIER_7 = 7;

		// Token: 0x040032B4 RID: 12980
		private const int ITEM_TIER_6 = 6;

		// Token: 0x040032B5 RID: 12981
		private const int ITEM_TIER_5 = 5;

		// Token: 0x040032B6 RID: 12982
		private const int ITEM_TIER_4 = 4;

		// Token: 0x040032B7 RID: 12983
		private const int ITEM_TIER_3 = 3;

		// Token: 0x040032B8 RID: 12984
		private const int ITEM_TIER_2 = 2;

		// Token: 0x040032B9 RID: 12985
		private const int ITEM_TIER_1 = 1;

		// Token: 0x040032BA RID: 12986
		private static readonly Dictionary<NKCEquipSortSystem.eSortCategory, Tuple<NKCEquipSortSystem.eSortOption, NKCEquipSortSystem.eSortOption>> s_dicSortCategory = new Dictionary<NKCEquipSortSystem.eSortCategory, Tuple<NKCEquipSortSystem.eSortOption, NKCEquipSortSystem.eSortOption>>
		{
			{
				NKCEquipSortSystem.eSortCategory.None,
				new Tuple<NKCEquipSortSystem.eSortOption, NKCEquipSortSystem.eSortOption>(NKCEquipSortSystem.eSortOption.None, NKCEquipSortSystem.eSortOption.None)
			},
			{
				NKCEquipSortSystem.eSortCategory.Enhance,
				new Tuple<NKCEquipSortSystem.eSortOption, NKCEquipSortSystem.eSortOption>(NKCEquipSortSystem.eSortOption.Enhance_Low, NKCEquipSortSystem.eSortOption.Enhance_High)
			},
			{
				NKCEquipSortSystem.eSortCategory.Tier,
				new Tuple<NKCEquipSortSystem.eSortOption, NKCEquipSortSystem.eSortOption>(NKCEquipSortSystem.eSortOption.Tier_Low, NKCEquipSortSystem.eSortOption.Tier_High)
			},
			{
				NKCEquipSortSystem.eSortCategory.Rarity,
				new Tuple<NKCEquipSortSystem.eSortOption, NKCEquipSortSystem.eSortOption>(NKCEquipSortSystem.eSortOption.Rarity_Low, NKCEquipSortSystem.eSortOption.Rarity_High)
			},
			{
				NKCEquipSortSystem.eSortCategory.UID,
				new Tuple<NKCEquipSortSystem.eSortOption, NKCEquipSortSystem.eSortOption>(NKCEquipSortSystem.eSortOption.UID_First, NKCEquipSortSystem.eSortOption.UID_Last)
			},
			{
				NKCEquipSortSystem.eSortCategory.OperationPower,
				new Tuple<NKCEquipSortSystem.eSortOption, NKCEquipSortSystem.eSortOption>(NKCEquipSortSystem.eSortOption.OperationPower_Low, NKCEquipSortSystem.eSortOption.OperationPower_High)
			},
			{
				NKCEquipSortSystem.eSortCategory.OptionWeightByClassType,
				new Tuple<NKCEquipSortSystem.eSortOption, NKCEquipSortSystem.eSortOption>(NKCEquipSortSystem.eSortOption.OptionWeightByClassType_Low, NKCEquipSortSystem.eSortOption.OptionWeightByClassType_High)
			},
			{
				NKCEquipSortSystem.eSortCategory.Equipped,
				new Tuple<NKCEquipSortSystem.eSortOption, NKCEquipSortSystem.eSortOption>(NKCEquipSortSystem.eSortOption.Equipped_First, NKCEquipSortSystem.eSortOption.Equipped_Last)
			},
			{
				NKCEquipSortSystem.eSortCategory.UnitType,
				new Tuple<NKCEquipSortSystem.eSortOption, NKCEquipSortSystem.eSortOption>(NKCEquipSortSystem.eSortOption.UnitType_First, NKCEquipSortSystem.eSortOption.UnitType_Last)
			},
			{
				NKCEquipSortSystem.eSortCategory.EquipType,
				new Tuple<NKCEquipSortSystem.eSortOption, NKCEquipSortSystem.eSortOption>(NKCEquipSortSystem.eSortOption.EquipType_FIrst, NKCEquipSortSystem.eSortOption.EquipType_Last)
			},
			{
				NKCEquipSortSystem.eSortCategory.ID,
				new Tuple<NKCEquipSortSystem.eSortOption, NKCEquipSortSystem.eSortOption>(NKCEquipSortSystem.eSortOption.ID_First, NKCEquipSortSystem.eSortOption.ID_Last)
			},
			{
				NKCEquipSortSystem.eSortCategory.Craftable,
				new Tuple<NKCEquipSortSystem.eSortOption, NKCEquipSortSystem.eSortOption>(NKCEquipSortSystem.eSortOption.Craftable_First, NKCEquipSortSystem.eSortOption.Craftable_Last)
			},
			{
				NKCEquipSortSystem.eSortCategory.SetOption,
				new Tuple<NKCEquipSortSystem.eSortOption, NKCEquipSortSystem.eSortOption>(NKCEquipSortSystem.eSortOption.SetOption_Low, NKCEquipSortSystem.eSortOption.SetOption_High)
			},
			{
				NKCEquipSortSystem.eSortCategory.Custom1,
				new Tuple<NKCEquipSortSystem.eSortOption, NKCEquipSortSystem.eSortOption>(NKCEquipSortSystem.eSortOption.CustomAscend1, NKCEquipSortSystem.eSortOption.CustomDescend1)
			},
			{
				NKCEquipSortSystem.eSortCategory.Custom2,
				new Tuple<NKCEquipSortSystem.eSortOption, NKCEquipSortSystem.eSortOption>(NKCEquipSortSystem.eSortOption.CustomAscend2, NKCEquipSortSystem.eSortOption.CustomDescend2)
			},
			{
				NKCEquipSortSystem.eSortCategory.Custom3,
				new Tuple<NKCEquipSortSystem.eSortOption, NKCEquipSortSystem.eSortOption>(NKCEquipSortSystem.eSortOption.CustomAscend3, NKCEquipSortSystem.eSortOption.CustomDescend3)
			}
		};

		// Token: 0x040032BB RID: 12987
		public static readonly List<NKCEquipSortSystem.eSortOption> DEFAULT_EQUIP_SORT_LIST = new List<NKCEquipSortSystem.eSortOption>
		{
			NKCEquipSortSystem.eSortOption.Equipped_First,
			NKCEquipSortSystem.eSortOption.Enhance_High,
			NKCEquipSortSystem.eSortOption.Tier_High,
			NKCEquipSortSystem.eSortOption.Rarity_High,
			NKCEquipSortSystem.eSortOption.UnitType_First,
			NKCEquipSortSystem.eSortOption.EquipType_FIrst,
			NKCEquipSortSystem.eSortOption.ID_First,
			NKCEquipSortSystem.eSortOption.UID_First,
			NKCEquipSortSystem.eSortOption.SetOption_High
		};

		// Token: 0x040032BC RID: 12988
		public static readonly List<NKCEquipSortSystem.eSortOption> DEFAULT_EQUIP_SELECTION_SORT_LIST = new List<NKCEquipSortSystem.eSortOption>
		{
			NKCEquipSortSystem.eSortOption.Tier_High,
			NKCEquipSortSystem.eSortOption.Rarity_High
		};

		// Token: 0x040032BD RID: 12989
		public static readonly List<NKCEquipSortSystem.eSortOption> DEFAULT_EQUIP_OPERATION_POWER_SORT_LIST = new List<NKCEquipSortSystem.eSortOption>
		{
			NKCEquipSortSystem.eSortOption.OperationPower_High,
			NKCEquipSortSystem.eSortOption.UID_Last
		};

		// Token: 0x040032BE RID: 12990
		public static readonly List<NKCEquipSortSystem.eSortOption> DEFAULT_EQUIP_OPTION_WEIGHT_SORT_LIST = new List<NKCEquipSortSystem.eSortOption>
		{
			NKCEquipSortSystem.eSortOption.OptionWeightByClassType_High,
			NKCEquipSortSystem.eSortOption.Tier_High,
			NKCEquipSortSystem.eSortOption.Rarity_High,
			NKCEquipSortSystem.eSortOption.UID_Last
		};

		// Token: 0x040032BF RID: 12991
		public static readonly List<NKCEquipSortSystem.eSortOption> FORGE_TARGET_SORT_LIST = new List<NKCEquipSortSystem.eSortOption>
		{
			NKCEquipSortSystem.eSortOption.Enhance_High,
			NKCEquipSortSystem.eSortOption.Equipped_First,
			NKCEquipSortSystem.eSortOption.Tier_High,
			NKCEquipSortSystem.eSortOption.Rarity_High,
			NKCEquipSortSystem.eSortOption.UnitType_First,
			NKCEquipSortSystem.eSortOption.EquipType_FIrst,
			NKCEquipSortSystem.eSortOption.ID_First,
			NKCEquipSortSystem.eSortOption.UID_First
		};

		// Token: 0x040032C0 RID: 12992
		public static readonly List<NKCEquipSortSystem.eSortOption> FORGE_MATERIAL_SORT_LIST = new List<NKCEquipSortSystem.eSortOption>
		{
			NKCEquipSortSystem.eSortOption.Enhance_Low,
			NKCEquipSortSystem.eSortOption.Rarity_Low,
			NKCEquipSortSystem.eSortOption.Tier_Low,
			NKCEquipSortSystem.eSortOption.UnitType_First,
			NKCEquipSortSystem.eSortOption.EquipType_FIrst,
			NKCEquipSortSystem.eSortOption.ID_First,
			NKCEquipSortSystem.eSortOption.UID_First
		};

		// Token: 0x040032C1 RID: 12993
		public static readonly List<NKCEquipSortSystem.eSortOption> EQUIP_BREAK_SORT_LIST = new List<NKCEquipSortSystem.eSortOption>
		{
			NKCEquipSortSystem.eSortOption.Enhance_Low,
			NKCEquipSortSystem.eSortOption.Rarity_Low,
			NKCEquipSortSystem.eSortOption.Tier_Low,
			NKCEquipSortSystem.eSortOption.UnitType_First,
			NKCEquipSortSystem.eSortOption.EquipType_FIrst,
			NKCEquipSortSystem.eSortOption.ID_First
		};

		// Token: 0x040032C2 RID: 12994
		public static readonly List<NKCEquipSortSystem.eSortOption> EQUIP_UPGRADE_SORT_LIST = new List<NKCEquipSortSystem.eSortOption>
		{
			NKCEquipSortSystem.eSortOption.CustomDescend1,
			NKCEquipSortSystem.eSortOption.ID_First
		};

		// Token: 0x040032C3 RID: 12995
		private static readonly HashSet<NKCEquipSortSystem.eFilterOption> m_setFilterCategory_EquipUnitType = new HashSet<NKCEquipSortSystem.eFilterOption>
		{
			NKCEquipSortSystem.eFilterOption.Equip_Counter,
			NKCEquipSortSystem.eFilterOption.Equip_Soldier,
			NKCEquipSortSystem.eFilterOption.Equip_Mechanic
		};

		// Token: 0x040032C4 RID: 12996
		private static readonly HashSet<NKCEquipSortSystem.eFilterOption> m_setFilterCategory_EquipType = new HashSet<NKCEquipSortSystem.eFilterOption>
		{
			NKCEquipSortSystem.eFilterOption.Equip_Weapon,
			NKCEquipSortSystem.eFilterOption.Equip_Armor,
			NKCEquipSortSystem.eFilterOption.Equip_Acc,
			NKCEquipSortSystem.eFilterOption.Equip_Enchant
		};

		// Token: 0x040032C5 RID: 12997
		private static readonly HashSet<NKCEquipSortSystem.eFilterOption> m_setFilterCategory_Tier = new HashSet<NKCEquipSortSystem.eFilterOption>
		{
			NKCEquipSortSystem.eFilterOption.Equip_Tier_7,
			NKCEquipSortSystem.eFilterOption.Equip_Tier_6,
			NKCEquipSortSystem.eFilterOption.Equip_Tier_5,
			NKCEquipSortSystem.eFilterOption.Equip_Tier_4,
			NKCEquipSortSystem.eFilterOption.Equip_Tier_3,
			NKCEquipSortSystem.eFilterOption.Equip_Tier_2,
			NKCEquipSortSystem.eFilterOption.Equip_Tier_1
		};

		// Token: 0x040032C6 RID: 12998
		private static readonly HashSet<NKCEquipSortSystem.eFilterOption> m_setFilterCategory_Rarity = new HashSet<NKCEquipSortSystem.eFilterOption>
		{
			NKCEquipSortSystem.eFilterOption.Equip_Rarity_SSR,
			NKCEquipSortSystem.eFilterOption.Equip_Rarity_SR,
			NKCEquipSortSystem.eFilterOption.Equip_Rarity_R,
			NKCEquipSortSystem.eFilterOption.Equip_Rarity_N
		};

		// Token: 0x040032C7 RID: 12999
		private static readonly HashSet<NKCEquipSortSystem.eFilterOption> m_setFilterCategory_SetOptionPart = new HashSet<NKCEquipSortSystem.eFilterOption>
		{
			NKCEquipSortSystem.eFilterOption.Equip_Set_Part_2,
			NKCEquipSortSystem.eFilterOption.Equip_Set_Part_3,
			NKCEquipSortSystem.eFilterOption.Equip_Set_Part_4
		};

		// Token: 0x040032C8 RID: 13000
		private static readonly HashSet<NKCEquipSortSystem.eFilterOption> m_setFilterCategory_SetOptionType = new HashSet<NKCEquipSortSystem.eFilterOption>
		{
			NKCEquipSortSystem.eFilterOption.Equip_Set_Effect_Red,
			NKCEquipSortSystem.eFilterOption.Equip_Set_Effect_Blue,
			NKCEquipSortSystem.eFilterOption.Equip_Set_Effect_Yellow
		};

		// Token: 0x040032C9 RID: 13001
		private static readonly HashSet<NKCEquipSortSystem.eFilterOption> m_setFilterCategory_Equipped = new HashSet<NKCEquipSortSystem.eFilterOption>
		{
			NKCEquipSortSystem.eFilterOption.Equip_Equipped,
			NKCEquipSortSystem.eFilterOption.Equip_Unused
		};

		// Token: 0x040032CA RID: 13002
		private static readonly HashSet<NKCEquipSortSystem.eFilterOption> m_setFilterCategory_Locked = new HashSet<NKCEquipSortSystem.eFilterOption>
		{
			NKCEquipSortSystem.eFilterOption.Equip_Locked,
			NKCEquipSortSystem.eFilterOption.Equip_Unlocked
		};

		// Token: 0x040032CB RID: 13003
		private static readonly HashSet<NKCEquipSortSystem.eFilterOption> m_setFilterCategory_Have = new HashSet<NKCEquipSortSystem.eFilterOption>
		{
			NKCEquipSortSystem.eFilterOption.Equip_Have,
			NKCEquipSortSystem.eFilterOption.Equip_NotHave
		};

		// Token: 0x040032CC RID: 13004
		private static readonly HashSet<NKCEquipSortSystem.eFilterOption> m_setFilterCategory_Upgrade = new HashSet<NKCEquipSortSystem.eFilterOption>
		{
			NKCEquipSortSystem.eFilterOption.Equip_CanUpgrade,
			NKCEquipSortSystem.eFilterOption.Equip_CannotUpgrade
		};

		// Token: 0x040032CD RID: 13005
		private static readonly HashSet<NKCEquipSortSystem.eFilterOption> m_setFilterCategory_StatType = new HashSet<NKCEquipSortSystem.eFilterOption>
		{
			NKCEquipSortSystem.eFilterOption.Equip_Stat
		};

		// Token: 0x040032CE RID: 13006
		private static readonly HashSet<NKCEquipSortSystem.eFilterOption> m_setFilterCategory_Private = new HashSet<NKCEquipSortSystem.eFilterOption>
		{
			NKCEquipSortSystem.eFilterOption.Equip_Private,
			NKCEquipSortSystem.eFilterOption.Equip_Non_Private,
			NKCEquipSortSystem.eFilterOption.Equip_Relic
		};

		// Token: 0x040032CF RID: 13007
		private static List<HashSet<NKCEquipSortSystem.eFilterOption>> m_lstFilterCategory = new List<HashSet<NKCEquipSortSystem.eFilterOption>>
		{
			NKCEquipSortSystem.m_setFilterCategory_EquipUnitType,
			NKCEquipSortSystem.m_setFilterCategory_EquipType,
			NKCEquipSortSystem.m_setFilterCategory_Tier,
			NKCEquipSortSystem.m_setFilterCategory_Rarity,
			NKCEquipSortSystem.m_setFilterCategory_Equipped,
			NKCEquipSortSystem.m_setFilterCategory_Locked,
			NKCEquipSortSystem.m_setFilterCategory_Have,
			NKCEquipSortSystem.m_setFilterCategory_SetOptionType,
			NKCEquipSortSystem.m_setFilterCategory_SetOptionPart,
			NKCEquipSortSystem.m_setFilterCategory_Upgrade,
			NKCEquipSortSystem.m_setFilterCategory_StatType,
			NKCEquipSortSystem.m_setFilterCategory_Private
		};

		// Token: 0x040032D0 RID: 13008
		public static readonly HashSet<NKCEquipSortSystem.eFilterCategory> m_hsEquipUpgradeFilterSet = new HashSet<NKCEquipSortSystem.eFilterCategory>
		{
			NKCEquipSortSystem.eFilterCategory.UnitType,
			NKCEquipSortSystem.eFilterCategory.EquipType,
			NKCEquipSortSystem.eFilterCategory.Tier,
			NKCEquipSortSystem.eFilterCategory.PrivateEquip
		};

		// Token: 0x040032D1 RID: 13009
		public static readonly HashSet<NKCEquipSortSystem.eSortCategory> m_hsEquipUpgradeSortSet = new HashSet<NKCEquipSortSystem.eSortCategory>
		{
			NKCEquipSortSystem.eSortCategory.Custom1,
			NKCEquipSortSystem.eSortCategory.ID
		};

		// Token: 0x040032D2 RID: 13010
		protected NKCEquipSortSystem.EquipListOptions m_Options;

		// Token: 0x040032D3 RID: 13011
		protected Dictionary<long, NKMEquipItemData> m_dicAllEquipList;

		// Token: 0x040032D4 RID: 13012
		protected List<NKMEquipItemData> m_lstCurrentEquipList;

		// Token: 0x040032D5 RID: 13013
		private Dictionary<long, float> m_dicCacheOperationPower = new Dictionary<long, float>();

		// Token: 0x040032D6 RID: 13014
		private Dictionary<long, float> m_dicCachePriorityByClassValue = new Dictionary<long, float>();

		// Token: 0x02001315 RID: 4885
		public enum eSortCategory
		{
			// Token: 0x040097F4 RID: 38900
			None,
			// Token: 0x040097F5 RID: 38901
			Enhance,
			// Token: 0x040097F6 RID: 38902
			Tier,
			// Token: 0x040097F7 RID: 38903
			Rarity,
			// Token: 0x040097F8 RID: 38904
			UID,
			// Token: 0x040097F9 RID: 38905
			OperationPower,
			// Token: 0x040097FA RID: 38906
			OptionWeightByClassType,
			// Token: 0x040097FB RID: 38907
			Equipped,
			// Token: 0x040097FC RID: 38908
			UnitType,
			// Token: 0x040097FD RID: 38909
			EquipType,
			// Token: 0x040097FE RID: 38910
			ID,
			// Token: 0x040097FF RID: 38911
			Craftable,
			// Token: 0x04009800 RID: 38912
			SetOption,
			// Token: 0x04009801 RID: 38913
			Custom1,
			// Token: 0x04009802 RID: 38914
			Custom2,
			// Token: 0x04009803 RID: 38915
			Custom3
		}

		// Token: 0x02001316 RID: 4886
		public enum eSortOption
		{
			// Token: 0x04009805 RID: 38917
			Enhance_High,
			// Token: 0x04009806 RID: 38918
			Enhance_Low,
			// Token: 0x04009807 RID: 38919
			Tier_High,
			// Token: 0x04009808 RID: 38920
			Tier_Low,
			// Token: 0x04009809 RID: 38921
			Rarity_High,
			// Token: 0x0400980A RID: 38922
			Rarity_Low,
			// Token: 0x0400980B RID: 38923
			UID_First,
			// Token: 0x0400980C RID: 38924
			UID_Last,
			// Token: 0x0400980D RID: 38925
			SetOption_High,
			// Token: 0x0400980E RID: 38926
			SetOption_Low,
			// Token: 0x0400980F RID: 38927
			OperationPower_High,
			// Token: 0x04009810 RID: 38928
			OperationPower_Low,
			// Token: 0x04009811 RID: 38929
			OptionWeightByClassType_High,
			// Token: 0x04009812 RID: 38930
			OptionWeightByClassType_Low,
			// Token: 0x04009813 RID: 38931
			Equipped_First,
			// Token: 0x04009814 RID: 38932
			Equipped_Last,
			// Token: 0x04009815 RID: 38933
			UnitType_First,
			// Token: 0x04009816 RID: 38934
			UnitType_Last,
			// Token: 0x04009817 RID: 38935
			EquipType_FIrst,
			// Token: 0x04009818 RID: 38936
			EquipType_Last,
			// Token: 0x04009819 RID: 38937
			ID_First,
			// Token: 0x0400981A RID: 38938
			ID_Last,
			// Token: 0x0400981B RID: 38939
			Craftable_First,
			// Token: 0x0400981C RID: 38940
			Craftable_Last,
			// Token: 0x0400981D RID: 38941
			CustomAscend1,
			// Token: 0x0400981E RID: 38942
			CustomDescend1,
			// Token: 0x0400981F RID: 38943
			CustomAscend2,
			// Token: 0x04009820 RID: 38944
			CustomDescend2,
			// Token: 0x04009821 RID: 38945
			CustomAscend3,
			// Token: 0x04009822 RID: 38946
			CustomDescend3,
			// Token: 0x04009823 RID: 38947
			None
		}

		// Token: 0x02001317 RID: 4887
		public enum eFilterCategory
		{
			// Token: 0x04009825 RID: 38949
			UnitType,
			// Token: 0x04009826 RID: 38950
			EquipType,
			// Token: 0x04009827 RID: 38951
			Tier,
			// Token: 0x04009828 RID: 38952
			Rarity,
			// Token: 0x04009829 RID: 38953
			Equipped,
			// Token: 0x0400982A RID: 38954
			Locked,
			// Token: 0x0400982B RID: 38955
			Have,
			// Token: 0x0400982C RID: 38956
			SetOptionPart,
			// Token: 0x0400982D RID: 38957
			SetOptionType,
			// Token: 0x0400982E RID: 38958
			Upgrade,
			// Token: 0x0400982F RID: 38959
			StatType,
			// Token: 0x04009830 RID: 38960
			PrivateEquip
		}

		// Token: 0x02001318 RID: 4888
		public enum eFilterOption
		{
			// Token: 0x04009832 RID: 38962
			Nothing,
			// Token: 0x04009833 RID: 38963
			All,
			// Token: 0x04009834 RID: 38964
			Equip_Counter,
			// Token: 0x04009835 RID: 38965
			Equip_Soldier,
			// Token: 0x04009836 RID: 38966
			Equip_Mechanic,
			// Token: 0x04009837 RID: 38967
			Equip_Weapon,
			// Token: 0x04009838 RID: 38968
			Equip_Armor,
			// Token: 0x04009839 RID: 38969
			Equip_Acc,
			// Token: 0x0400983A RID: 38970
			Equip_Enchant,
			// Token: 0x0400983B RID: 38971
			Equip_Tier_7,
			// Token: 0x0400983C RID: 38972
			Equip_Tier_6,
			// Token: 0x0400983D RID: 38973
			Equip_Tier_5,
			// Token: 0x0400983E RID: 38974
			Equip_Tier_4,
			// Token: 0x0400983F RID: 38975
			Equip_Tier_3,
			// Token: 0x04009840 RID: 38976
			Equip_Tier_2,
			// Token: 0x04009841 RID: 38977
			Equip_Tier_1,
			// Token: 0x04009842 RID: 38978
			Equip_Rarity_SSR,
			// Token: 0x04009843 RID: 38979
			Equip_Rarity_SR,
			// Token: 0x04009844 RID: 38980
			Equip_Rarity_R,
			// Token: 0x04009845 RID: 38981
			Equip_Rarity_N,
			// Token: 0x04009846 RID: 38982
			Equip_Set_Part_2,
			// Token: 0x04009847 RID: 38983
			Equip_Set_Part_3,
			// Token: 0x04009848 RID: 38984
			Equip_Set_Part_4,
			// Token: 0x04009849 RID: 38985
			Equip_Set_Effect_Red,
			// Token: 0x0400984A RID: 38986
			Equip_Set_Effect_Blue,
			// Token: 0x0400984B RID: 38987
			Equip_Set_Effect_Yellow,
			// Token: 0x0400984C RID: 38988
			Equip_Equipped,
			// Token: 0x0400984D RID: 38989
			Equip_Unused,
			// Token: 0x0400984E RID: 38990
			Equip_Locked,
			// Token: 0x0400984F RID: 38991
			Equip_Unlocked,
			// Token: 0x04009850 RID: 38992
			Equip_Have,
			// Token: 0x04009851 RID: 38993
			Equip_NotHave,
			// Token: 0x04009852 RID: 38994
			Equip_CanUpgrade,
			// Token: 0x04009853 RID: 38995
			Equip_CannotUpgrade,
			// Token: 0x04009854 RID: 38996
			Equip_Stat,
			// Token: 0x04009855 RID: 38997
			Equip_Private,
			// Token: 0x04009856 RID: 38998
			Equip_Non_Private,
			// Token: 0x04009857 RID: 38999
			Equip_Relic
		}

		// Token: 0x02001319 RID: 4889
		public struct EquipListOptions
		{
			// Token: 0x04009858 RID: 39000
			public HashSet<int> setOnlyIncludeEquipID;

			// Token: 0x04009859 RID: 39001
			public HashSet<int> setExcludeEquipID;

			// Token: 0x0400985A RID: 39002
			public HashSet<long> setExcludeEquipUID;

			// Token: 0x0400985B RID: 39003
			public HashSet<NKCEquipSortSystem.eFilterOption> setExcludeFilterOption;

			// Token: 0x0400985C RID: 39004
			public HashSet<NKCEquipSortSystem.eFilterOption> setFilterOption;

			// Token: 0x0400985D RID: 39005
			public List<NKCEquipSortSystem.eSortOption> lstSortOption;

			// Token: 0x0400985E RID: 39006
			public NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc PreemptiveSortFunc;

			// Token: 0x0400985F RID: 39007
			public NKCEquipSortSystem.EquipListOptions.CustomFilterFunc AdditionalExcludeFilterFunc;

			// Token: 0x04009860 RID: 39008
			public Dictionary<NKCEquipSortSystem.eSortCategory, KeyValuePair<string, NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc>> lstCustomSortFunc;

			// Token: 0x04009861 RID: 39009
			public bool bHideEquippedItem;

			// Token: 0x04009862 RID: 39010
			public bool bPushBackUnselectable;

			// Token: 0x04009863 RID: 39011
			public bool bHideLockItem;

			// Token: 0x04009864 RID: 39012
			public bool bHideMaxLvItem;

			// Token: 0x04009865 RID: 39013
			public bool bLockMaxItem;

			// Token: 0x04009866 RID: 39014
			public int OwnerUnitID;

			// Token: 0x04009867 RID: 39015
			public bool bHideNotPossibleSetOptionItem;

			// Token: 0x04009868 RID: 39016
			public int iTargetUnitID;

			// Token: 0x04009869 RID: 39017
			public NKM_STAT_TYPE FilterStatType_01;

			// Token: 0x0400986A RID: 39018
			public NKM_STAT_TYPE FilterStatType_02;

			// Token: 0x0400986B RID: 39019
			public NKM_STAT_TYPE FilterStatType_03;

			// Token: 0x0400986C RID: 39020
			public int FilterSetOptionID;

			// Token: 0x02001A53 RID: 6739
			// (Invoke) Token: 0x0600BB96 RID: 48022
			public delegate bool CustomFilterFunc(NKMEquipItemData equipData);
		}

		// Token: 0x0200131A RID: 4890
		// (Invoke) Token: 0x0600A51A RID: 42266
		public delegate bool AutoSelectExtraFilter(NKMEquipItemData equipData);
	}
}
