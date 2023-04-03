using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.WorldMap;
using NKC.UI;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006E4 RID: 1764
	public abstract class NKCUnitSortSystem
	{
		// Token: 0x06003D92 RID: 15762 RVA: 0x0013CF5C File Offset: 0x0013B15C
		public static HashSet<int> GetDefaultExcludeUnitIDs()
		{
			HashSet<int> hashSet = new HashSet<int>(NKMMain.excludeUnitID);
			if (!NKMOpenTagManager.IsOpened("TAG_DELETE_YOO_MI_NA"))
			{
				hashSet.Add(1001);
			}
			if (!NKMOpenTagManager.IsOpened("TAG_DELETE_TEAM_FENRIR"))
			{
				hashSet.Add(1002);
			}
			if (!NKMOpenTagManager.IsOpened("TAG_DELETE_JOO_SHI_YOON"))
			{
				hashSet.Add(1003);
			}
			return hashSet;
		}

		// Token: 0x06003D93 RID: 15763 RVA: 0x0013CFC0 File Offset: 0x0013B1C0
		public static NKCUnitSortSystem.eSortCategory GetSortCategoryFromOption(NKCUnitSortSystem.eSortOption option)
		{
			foreach (KeyValuePair<NKCUnitSortSystem.eSortCategory, Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>> keyValuePair in NKCUnitSortSystem.s_dicSortCategory)
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
			return NKCUnitSortSystem.eSortCategory.None;
		}

		// Token: 0x06003D94 RID: 15764 RVA: 0x0013D040 File Offset: 0x0013B240
		public static NKCUnitSortSystem.eSortOption GetSortOptionByCategory(NKCUnitSortSystem.eSortCategory category, bool bDescending)
		{
			Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption> tuple = NKCUnitSortSystem.s_dicSortCategory[category];
			if (!bDescending)
			{
				return tuple.Item1;
			}
			return tuple.Item2;
		}

		// Token: 0x06003D95 RID: 15765 RVA: 0x0013D06C File Offset: 0x0013B26C
		public static bool IsDescending(NKCUnitSortSystem.eSortOption option)
		{
			foreach (KeyValuePair<NKCUnitSortSystem.eSortCategory, Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>> keyValuePair in NKCUnitSortSystem.s_dicSortCategory)
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

		// Token: 0x06003D96 RID: 15766 RVA: 0x0013D0E0 File Offset: 0x0013B2E0
		public static NKCUnitSortSystem.eSortOption GetInvertedAscendOption(NKCUnitSortSystem.eSortOption option)
		{
			foreach (KeyValuePair<NKCUnitSortSystem.eSortCategory, Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>> keyValuePair in NKCUnitSortSystem.s_dicSortCategory)
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

		// Token: 0x06003D97 RID: 15767 RVA: 0x0013D16C File Offset: 0x0013B36C
		public static List<NKCUnitSortSystem.eSortOption> ChangeAscend(List<NKCUnitSortSystem.eSortOption> targetList)
		{
			List<NKCUnitSortSystem.eSortOption> list = new List<NKCUnitSortSystem.eSortOption>(targetList);
			if (list == null || list.Count == 0)
			{
				return list;
			}
			list[0] = NKCUnitSortSystem.GetInvertedAscendOption(list[0]);
			return list;
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06003D98 RID: 15768 RVA: 0x0013D1A1 File Offset: 0x0013B3A1
		public NKCUnitSortSystem.UnitListOptions Options
		{
			get
			{
				return this.m_Options;
			}
		}

		// Token: 0x06003D99 RID: 15769 RVA: 0x0013D1A9 File Offset: 0x0013B3A9
		public void SetEnableShowBan(bool bSet)
		{
			this.m_bEnableShowBan = bSet;
		}

		// Token: 0x06003D9A RID: 15770 RVA: 0x0013D1B2 File Offset: 0x0013B3B2
		public void SetEnableShowUpUnit(bool bSet)
		{
			this.m_bEnableShowUpUnit = bSet;
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06003D9B RID: 15771 RVA: 0x0013D1BC File Offset: 0x0013B3BC
		public List<NKMUnitData> SortedUnitList
		{
			get
			{
				if (this.m_lstCurrentUnitList == null)
				{
					if (this.m_Options.setFilterOption == null)
					{
						this.m_Options.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
						this.FilterList(this.m_Options.setFilterOption, this.m_Options.bHideDeckedUnit);
					}
					else
					{
						this.FilterList(this.m_Options.setFilterOption, this.m_Options.bHideDeckedUnit);
					}
				}
				return this.m_lstCurrentUnitList;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06003D9C RID: 15772 RVA: 0x0013D22E File Offset: 0x0013B42E
		// (set) Token: 0x06003D9D RID: 15773 RVA: 0x0013D23B File Offset: 0x0013B43B
		public HashSet<NKCUnitSortSystem.eFilterOption> FilterSet
		{
			get
			{
				return this.m_Options.setFilterOption;
			}
			set
			{
				this.FilterList(value, this.m_Options.bHideDeckedUnit);
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06003D9E RID: 15774 RVA: 0x0013D24F File Offset: 0x0013B44F
		// (set) Token: 0x06003D9F RID: 15775 RVA: 0x0013D25C File Offset: 0x0013B45C
		public List<NKCUnitSortSystem.eSortOption> lstSortOption
		{
			get
			{
				return this.m_Options.lstSortOption;
			}
			set
			{
				this.SortList(value, this.Descending);
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06003DA0 RID: 15776 RVA: 0x0013D26B File Offset: 0x0013B46B
		// (set) Token: 0x06003DA1 RID: 15777 RVA: 0x0013D278 File Offset: 0x0013B478
		public bool Descending
		{
			get
			{
				return this.m_Options.bDescending;
			}
			set
			{
				if (this.m_Options.lstSortOption != null)
				{
					this.m_Options.bDescending = value;
					this.SortList(this.m_Options.lstSortOption, false);
					return;
				}
				this.m_Options.lstSortOption = NKCUnitSortSystem.GetDefaultSortOptions(NKM_UNIT_TYPE.NUT_NORMAL, false, false);
				this.m_Options.bDescending = value;
				this.SortList(this.m_Options.lstSortOption, false);
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06003DA2 RID: 15778 RVA: 0x0013D2E2 File Offset: 0x0013B4E2
		// (set) Token: 0x06003DA3 RID: 15779 RVA: 0x0013D2F0 File Offset: 0x0013B4F0
		public bool bHideDeckedUnit
		{
			get
			{
				return this.m_Options.bHideDeckedUnit;
			}
			set
			{
				if (this.m_Options.setFilterOption != null)
				{
					this.FilterList(this.m_Options.setFilterOption, value);
					return;
				}
				this.m_Options.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
				this.FilterList(this.m_Options.setFilterOption, value);
			}
		}

		// Token: 0x06003DA4 RID: 15780
		protected abstract IEnumerable<NKMUnitData> GetTargetUnitList(NKMUserData userData);

		// Token: 0x06003DA5 RID: 15781 RVA: 0x0013D33F File Offset: 0x0013B53F
		private NKCUnitSortSystem()
		{
		}

		// Token: 0x06003DA6 RID: 15782 RVA: 0x0013D368 File Offset: 0x0013B568
		public NKCUnitSortSystem(NKMUserData userData, NKCUnitSortSystem.UnitListOptions options)
		{
			this.m_Options = options;
			this.BuildUnitStateCache(userData, options.eDeckType);
			this.m_dicAllUnitList = this.BuildFullUnitList(userData, this.GetTargetUnitList(userData), options);
		}

		// Token: 0x06003DA7 RID: 15783 RVA: 0x0013D3C8 File Offset: 0x0013B5C8
		public NKCUnitSortSystem(NKMUserData userData, NKCUnitSortSystem.UnitListOptions options, IEnumerable<NKMUnitData> lstUnitData)
		{
			this.m_Options = options;
			this.BuildUnitStateCache(userData, lstUnitData, options.eDeckType);
			this.m_dicAllUnitList = this.BuildFullUnitList(userData, lstUnitData, options);
		}

		// Token: 0x06003DA8 RID: 15784 RVA: 0x0013D420 File Offset: 0x0013B620
		public NKCUnitSortSystem(NKMUserData userData, NKCUnitSortSystem.UnitListOptions options, bool useLocal)
		{
			this.m_Options = options;
			if (useLocal)
			{
				this.BuildLocalUnitStateCache(userData, options.eDeckType);
			}
			else
			{
				this.BuildUnitStateCache(userData, options.eDeckType);
			}
			this.m_dicAllUnitList = this.BuildFullUnitList(userData, this.GetTargetUnitList(userData), options);
		}

		// Token: 0x06003DA9 RID: 15785 RVA: 0x0013D48F File Offset: 0x0013B68F
		public virtual void BuildFilterAndSortedList(HashSet<NKCUnitSortSystem.eFilterOption> setfilterType, List<NKCUnitSortSystem.eSortOption> lstSortOption, bool bHideDeckedUnit)
		{
			this.m_Options.bHideDeckedUnit = bHideDeckedUnit;
			this.m_Options.setFilterOption = setfilterType;
			this.m_Options.lstSortOption = lstSortOption;
			this.FilterList(setfilterType, bHideDeckedUnit);
		}

		// Token: 0x06003DAA RID: 15786 RVA: 0x0013D4C0 File Offset: 0x0013B6C0
		public void SetDeckIndexCache(long uid, NKMDeckIndex deckindex)
		{
			if (!this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				NKCUnitSortSystem.UnitInfoCache value = new NKCUnitSortSystem.UnitInfoCache();
				this.m_dicUnitInfoCache.Add(uid, value);
			}
			if (this.m_dicUnitInfoCache[uid].dicDeckIndex == null)
			{
				this.m_dicUnitInfoCache[uid].dicDeckIndex = new Dictionary<NKM_DECK_TYPE, byte>();
			}
			Dictionary<NKM_DECK_TYPE, byte> dicDeckIndex = this.m_dicUnitInfoCache[uid].dicDeckIndex;
			byte b;
			if (dicDeckIndex.TryGetValue(deckindex.m_eDeckType, out b))
			{
				if (deckindex.m_iIndex < b)
				{
					dicDeckIndex[deckindex.m_eDeckType] = deckindex.m_iIndex;
					return;
				}
			}
			else
			{
				dicDeckIndex.Add(deckindex.m_eDeckType, deckindex.m_iIndex);
			}
		}

		// Token: 0x06003DAB RID: 15787 RVA: 0x0013D568 File Offset: 0x0013B768
		public NKMDeckIndex GetDeckIndexCache(long uid, bool bTargetDecktypeOnly = false)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				if (this.m_dicUnitInfoCache[uid].dicDeckIndex == null)
				{
					return NKMDeckIndex.None;
				}
				if (!bTargetDecktypeOnly)
				{
					if (this.m_Options.lstDeckTypeOrder != null)
					{
						foreach (NKM_DECK_TYPE nkm_DECK_TYPE in this.m_Options.lstDeckTypeOrder)
						{
							byte index;
							if (this.m_dicUnitInfoCache[uid].dicDeckIndex.TryGetValue(nkm_DECK_TYPE, out index))
							{
								return new NKMDeckIndex(nkm_DECK_TYPE, (int)index);
							}
						}
					}
					foreach (object obj in Enum.GetValues(typeof(NKM_DECK_TYPE)))
					{
						NKM_DECK_TYPE nkm_DECK_TYPE2 = (NKM_DECK_TYPE)obj;
						byte index2;
						if (this.m_dicUnitInfoCache[uid].dicDeckIndex.TryGetValue(nkm_DECK_TYPE2, out index2))
						{
							return new NKMDeckIndex(nkm_DECK_TYPE2, (int)index2);
						}
					}
					goto IL_1B1;
				}
				byte index3;
				if (this.m_dicUnitInfoCache[uid].dicDeckIndex.TryGetValue(this.m_Options.eDeckType, out index3))
				{
					return new NKMDeckIndex(this.m_Options.eDeckType, (int)index3);
				}
				if (this.m_Options.lstDeckTypeOrder != null)
				{
					foreach (NKM_DECK_TYPE nkm_DECK_TYPE3 in this.m_Options.lstDeckTypeOrder)
					{
						if (this.m_dicUnitInfoCache[uid].dicDeckIndex.TryGetValue(nkm_DECK_TYPE3, out index3))
						{
							return new NKMDeckIndex(nkm_DECK_TYPE3, (int)index3);
						}
					}
				}
				return NKMDeckIndex.None;
			}
			IL_1B1:
			return NKMDeckIndex.None;
		}

		// Token: 0x06003DAC RID: 15788 RVA: 0x0013D758 File Offset: 0x0013B958
		public NKMDeckIndex GetDeckIndexCache(long uid, NKM_DECK_TYPE deckType)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				if (this.m_dicUnitInfoCache[uid].dicDeckIndex == null)
				{
					return NKMDeckIndex.None;
				}
				byte index;
				if (this.m_dicUnitInfoCache[uid].dicDeckIndex.TryGetValue(deckType, out index))
				{
					return new NKMDeckIndex(deckType, (int)index);
				}
				if (this.m_Options.lstDeckTypeOrder != null)
				{
					foreach (NKM_DECK_TYPE nkm_DECK_TYPE in this.m_Options.lstDeckTypeOrder)
					{
						if (this.m_dicUnitInfoCache[uid].dicDeckIndex.TryGetValue(nkm_DECK_TYPE, out index))
						{
							return new NKMDeckIndex(nkm_DECK_TYPE, (int)index);
						}
					}
				}
				foreach (object obj in Enum.GetValues(typeof(NKM_DECK_TYPE)))
				{
					NKM_DECK_TYPE nkm_DECK_TYPE2 = (NKM_DECK_TYPE)obj;
					if (this.m_dicUnitInfoCache[uid].dicDeckIndex.TryGetValue(nkm_DECK_TYPE2, out index))
					{
						return new NKMDeckIndex(nkm_DECK_TYPE2, (int)index);
					}
				}
			}
			return NKMDeckIndex.None;
		}

		// Token: 0x06003DAD RID: 15789 RVA: 0x0013D8AC File Offset: 0x0013BAAC
		public NKMDeckIndex GetDeckIndexCacheByOption(long uid, bool bTargetDeckTypeOnly)
		{
			if (this.m_Options.lstSortOption.Contains(NKCUnitSortSystem.eSortOption.Squad_Dungeon_High) || this.m_Options.lstSortOption.Contains(NKCUnitSortSystem.eSortOption.Squad_Dungeon_Low))
			{
				return this.GetDeckIndexCache(uid, NKM_DECK_TYPE.NDT_DAILY);
			}
			if (!this.m_Options.lstSortOption.Contains(NKCUnitSortSystem.eSortOption.Squad_Gauntlet_High) && !this.m_Options.lstSortOption.Contains(NKCUnitSortSystem.eSortOption.Squad_Gauntlet_Low))
			{
				return this.GetDeckIndexCache(uid, bTargetDeckTypeOnly);
			}
			NKMDeckIndex deckIndexCache = this.GetDeckIndexCache(uid, NKM_DECK_TYPE.NDT_PVP);
			if (deckIndexCache.m_eDeckType == NKM_DECK_TYPE.NDT_PVP)
			{
				return deckIndexCache;
			}
			return this.GetDeckIndexCache(uid, NKM_DECK_TYPE.NDT_PVP_DEFENCE);
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x0013D938 File Offset: 0x0013BB38
		private void SetUnitAttackCache(long uid, int atk)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				this.m_dicUnitInfoCache[uid].Attack = atk;
				return;
			}
			NKCUnitSortSystem.UnitInfoCache unitInfoCache = new NKCUnitSortSystem.UnitInfoCache();
			unitInfoCache.Attack = atk;
			this.m_dicUnitInfoCache.Add(uid, unitInfoCache);
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x0013D980 File Offset: 0x0013BB80
		public int GetUnitAttackCache(long uid)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				return this.m_dicUnitInfoCache[uid].Attack;
			}
			return 0;
		}

		// Token: 0x06003DB0 RID: 15792 RVA: 0x0013D9A4 File Offset: 0x0013BBA4
		private void SetUnitHPCache(long uid, int hp)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				this.m_dicUnitInfoCache[uid].HP = hp;
				return;
			}
			NKCUnitSortSystem.UnitInfoCache unitInfoCache = new NKCUnitSortSystem.UnitInfoCache();
			unitInfoCache.HP = hp;
			this.m_dicUnitInfoCache.Add(uid, unitInfoCache);
		}

		// Token: 0x06003DB1 RID: 15793 RVA: 0x0013D9EC File Offset: 0x0013BBEC
		public int GetUnitHPCache(long uid)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				return this.m_dicUnitInfoCache[uid].HP;
			}
			return 0;
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x0013DA10 File Offset: 0x0013BC10
		private void SetUnitDefCache(long uid, int def)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				this.m_dicUnitInfoCache[uid].Defense = def;
				return;
			}
			NKCUnitSortSystem.UnitInfoCache unitInfoCache = new NKCUnitSortSystem.UnitInfoCache();
			unitInfoCache.Defense = def;
			this.m_dicUnitInfoCache.Add(uid, unitInfoCache);
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x0013DA58 File Offset: 0x0013BC58
		public int GetUnitDefCache(long uid)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				return this.m_dicUnitInfoCache[uid].Defense;
			}
			return 0;
		}

		// Token: 0x06003DB4 RID: 15796 RVA: 0x0013DA7C File Offset: 0x0013BC7C
		private void SetUnitCritCache(long uid, int crit)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				this.m_dicUnitInfoCache[uid].Critical = crit;
				return;
			}
			NKCUnitSortSystem.UnitInfoCache unitInfoCache = new NKCUnitSortSystem.UnitInfoCache();
			unitInfoCache.Critical = crit;
			this.m_dicUnitInfoCache.Add(uid, unitInfoCache);
		}

		// Token: 0x06003DB5 RID: 15797 RVA: 0x0013DAC4 File Offset: 0x0013BCC4
		public int GetUnitCritCache(long uid)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				return this.m_dicUnitInfoCache[uid].Critical;
			}
			return 0;
		}

		// Token: 0x06003DB6 RID: 15798 RVA: 0x0013DAE8 File Offset: 0x0013BCE8
		private void SetUnitHitCache(long uid, int Hit)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				this.m_dicUnitInfoCache[uid].Hit = Hit;
				return;
			}
			NKCUnitSortSystem.UnitInfoCache unitInfoCache = new NKCUnitSortSystem.UnitInfoCache();
			unitInfoCache.Hit = Hit;
			this.m_dicUnitInfoCache.Add(uid, unitInfoCache);
		}

		// Token: 0x06003DB7 RID: 15799 RVA: 0x0013DB30 File Offset: 0x0013BD30
		public int GetUnitHitCache(long uid)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				return this.m_dicUnitInfoCache[uid].Hit;
			}
			return 0;
		}

		// Token: 0x06003DB8 RID: 15800 RVA: 0x0013DB54 File Offset: 0x0013BD54
		private void SetUnitEvadeCache(long uid, int Evade)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				this.m_dicUnitInfoCache[uid].Evade = Evade;
				return;
			}
			NKCUnitSortSystem.UnitInfoCache unitInfoCache = new NKCUnitSortSystem.UnitInfoCache();
			unitInfoCache.Evade = Evade;
			this.m_dicUnitInfoCache.Add(uid, unitInfoCache);
		}

		// Token: 0x06003DB9 RID: 15801 RVA: 0x0013DB9C File Offset: 0x0013BD9C
		public int GetUnitEvadeCache(long uid)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				return this.m_dicUnitInfoCache[uid].Evade;
			}
			return 0;
		}

		// Token: 0x06003DBA RID: 15802 RVA: 0x0013DBC0 File Offset: 0x0013BDC0
		private void SetUnitSkillCoolCache(long uid, int ReduceSkillColl)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				this.m_dicUnitInfoCache[uid].ReduceSkillCollTime = ReduceSkillColl;
				return;
			}
			NKCUnitSortSystem.UnitInfoCache unitInfoCache = new NKCUnitSortSystem.UnitInfoCache();
			unitInfoCache.ReduceSkillCollTime = ReduceSkillColl;
			this.m_dicUnitInfoCache.Add(uid, unitInfoCache);
		}

		// Token: 0x06003DBB RID: 15803 RVA: 0x0013DC08 File Offset: 0x0013BE08
		public int GetUnitSkillCoolCache(long uid)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				return this.m_dicUnitInfoCache[uid].ReduceSkillCollTime;
			}
			return 0;
		}

		// Token: 0x06003DBC RID: 15804 RVA: 0x0013DC2C File Offset: 0x0013BE2C
		private void SetUnitPowerCache(long uid, int Power)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				this.m_dicUnitInfoCache[uid].Power = Power;
				return;
			}
			NKCUnitSortSystem.UnitInfoCache unitInfoCache = new NKCUnitSortSystem.UnitInfoCache();
			unitInfoCache.Power = Power;
			this.m_dicUnitInfoCache.Add(uid, unitInfoCache);
		}

		// Token: 0x06003DBD RID: 15805 RVA: 0x0013DC74 File Offset: 0x0013BE74
		public int GetUnitPowerCache(long uid)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				return this.m_dicUnitInfoCache[uid].Power;
			}
			return 0;
		}

		// Token: 0x06003DBE RID: 15806 RVA: 0x0013DC98 File Offset: 0x0013BE98
		private void SetEnhanceProgressCache(long uid, float value)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				this.m_dicUnitInfoCache[uid].EnhanceProgress = value;
				return;
			}
			NKCUnitSortSystem.UnitInfoCache unitInfoCache = new NKCUnitSortSystem.UnitInfoCache();
			unitInfoCache.EnhanceProgress = value;
			this.m_dicUnitInfoCache.Add(uid, unitInfoCache);
		}

		// Token: 0x06003DBF RID: 15807 RVA: 0x0013DCE0 File Offset: 0x0013BEE0
		public float GetEnhanceProgressCache(long uid)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				return this.m_dicUnitInfoCache[uid].EnhanceProgress;
			}
			return 0f;
		}

		// Token: 0x06003DC0 RID: 15808 RVA: 0x0013DD08 File Offset: 0x0013BF08
		private float MakeEnhanceProgressValue(NKMUnitData cNKMUnitData)
		{
			if (cNKMUnitData.m_UnitLevel == 1)
			{
				return 0f;
			}
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < cNKMUnitData.m_listStatEXP.Count; i++)
			{
				int num3 = NKMEnhanceManager.CalculateMaxEXP(cNKMUnitData, (NKM_STAT_TYPE)i);
				num += num3;
				num2 += Math.Min(cNKMUnitData.m_listStatEXP[i], num3);
			}
			if (num == 0)
			{
				return 0f;
			}
			return (float)num2 / (float)num;
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x0013DD70 File Offset: 0x0013BF70
		private void SetScoutProgressCache(long uid, float value)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				this.m_dicUnitInfoCache[uid].ScoutProgress = value;
				return;
			}
			NKCUnitSortSystem.UnitInfoCache unitInfoCache = new NKCUnitSortSystem.UnitInfoCache();
			unitInfoCache.ScoutProgress = value;
			this.m_dicUnitInfoCache.Add(uid, unitInfoCache);
		}

		// Token: 0x06003DC2 RID: 15810 RVA: 0x0013DDB8 File Offset: 0x0013BFB8
		public float GetScoutProgressCache(long uid)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				return this.m_dicUnitInfoCache[uid].ScoutProgress;
			}
			return 0f;
		}

		// Token: 0x06003DC3 RID: 15811 RVA: 0x0013DDE0 File Offset: 0x0013BFE0
		private void SetLimitBreakProgressCache(long uid, int value)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				this.m_dicUnitInfoCache[uid].LimitBreakProgress = value;
				return;
			}
			NKCUnitSortSystem.UnitInfoCache unitInfoCache = new NKCUnitSortSystem.UnitInfoCache();
			unitInfoCache.LimitBreakProgress = value;
			this.m_dicUnitInfoCache.Add(uid, unitInfoCache);
		}

		// Token: 0x06003DC4 RID: 15812 RVA: 0x0013DE28 File Offset: 0x0013C028
		public int GetLimitBreakCache(long uid)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				return this.m_dicUnitInfoCache[uid].LimitBreakProgress;
			}
			return 0;
		}

		// Token: 0x06003DC5 RID: 15813 RVA: 0x0013DE4C File Offset: 0x0013C04C
		private void SetLoyaltyCache(long uid, int value)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				this.m_dicUnitInfoCache[uid].Loyalty = value / 100;
				return;
			}
			NKCUnitSortSystem.UnitInfoCache unitInfoCache = new NKCUnitSortSystem.UnitInfoCache();
			unitInfoCache.Loyalty = value / 100;
			this.m_dicUnitInfoCache.Add(uid, unitInfoCache);
		}

		// Token: 0x06003DC6 RID: 15814 RVA: 0x0013DE9A File Offset: 0x0013C09A
		public int GetLoyaltyCache(long uid)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				return this.m_dicUnitInfoCache[uid].Loyalty;
			}
			return 0;
		}

		// Token: 0x06003DC7 RID: 15815 RVA: 0x0013DEC0 File Offset: 0x0013C0C0
		private float MakeScoutProgressValue(NKMUnitData cNKMUnitData)
		{
			NKMPieceTemplet nkmpieceTemplet = NKMTempletContainer<NKMPieceTemplet>.Find((int)cNKMUnitData.m_UnitUID);
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			long num = (long)(nkmuserData.m_ArmyData.IsCollectedUnit(nkmpieceTemplet.m_PieceGetUintId) ? nkmpieceTemplet.m_PieceReq : nkmpieceTemplet.m_PieceReqFirst);
			return (float)nkmuserData.m_InventoryData.GetCountMiscItem(nkmpieceTemplet.m_PieceId) / (float)num;
		}

		// Token: 0x06003DC8 RID: 15816 RVA: 0x0013DF18 File Offset: 0x0013C118
		private void SetCityInfoCache(long uid, NKMWorldMapManager.WorldMapLeaderState state)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				this.m_dicUnitInfoCache[uid].CityState = state;
				return;
			}
			NKCUnitSortSystem.UnitInfoCache unitInfoCache = new NKCUnitSortSystem.UnitInfoCache();
			unitInfoCache.CityState = state;
			this.m_dicUnitInfoCache.Add(uid, unitInfoCache);
		}

		// Token: 0x06003DC9 RID: 15817 RVA: 0x0013DF60 File Offset: 0x0013C160
		public NKMWorldMapManager.WorldMapLeaderState GetCityStateCache(long uid)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				return this.m_dicUnitInfoCache[uid].CityState;
			}
			return NKMWorldMapManager.WorldMapLeaderState.None;
		}

		// Token: 0x06003DCA RID: 15818 RVA: 0x0013DF84 File Offset: 0x0013C184
		private void SetUnitSlotState(long uid, NKCUnitSortSystem.eUnitState state)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				this.m_dicUnitInfoCache[uid].UnitSlotState = state;
				return;
			}
			NKCUnitSortSystem.UnitInfoCache unitInfoCache = new NKCUnitSortSystem.UnitInfoCache();
			unitInfoCache.UnitSlotState = state;
			this.m_dicUnitInfoCache.Add(uid, unitInfoCache);
		}

		// Token: 0x06003DCB RID: 15819 RVA: 0x0013DFCC File Offset: 0x0013C1CC
		public NKCUnitSortSystem.eUnitState GetUnitSlotState(long uid)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				return this.m_dicUnitInfoCache[uid].UnitSlotState;
			}
			return NKCUnitSortSystem.eUnitState.NONE;
		}

		// Token: 0x06003DCC RID: 15820 RVA: 0x0013DFF0 File Offset: 0x0013C1F0
		private void SetTacticUpdateProgressCache(long uid, int value)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				this.m_dicUnitInfoCache[uid].TacticUpdateProcess = value;
				return;
			}
			NKCUnitSortSystem.UnitInfoCache unitInfoCache = new NKCUnitSortSystem.UnitInfoCache();
			unitInfoCache.TacticUpdateProcess = value;
			this.m_dicUnitInfoCache.Add(uid, unitInfoCache);
		}

		// Token: 0x06003DCD RID: 15821 RVA: 0x0013E038 File Offset: 0x0013C238
		public int GetTacticUpdateCache(long uid)
		{
			if (this.m_dicUnitInfoCache.ContainsKey(uid))
			{
				return this.m_dicUnitInfoCache[uid].TacticUpdateProcess;
			}
			return 0;
		}

		// Token: 0x06003DCE RID: 15822 RVA: 0x0013E05C File Offset: 0x0013C25C
		protected virtual void BuildUnitStateCache(NKMUserData userData, IEnumerable<NKMUnitData> lstUnitData, NKM_DECK_TYPE eNKM_DECK_TYPE)
		{
			this.m_dicUnitInfoCache.Clear();
			this.m_dicDeckedUnitIdCache.Clear();
			this.m_dicDeckedShipGroupIdCache.Clear();
			if (lstUnitData == null)
			{
				return;
			}
			if (eNKM_DECK_TYPE == NKM_DECK_TYPE.NDT_NONE)
			{
				return;
			}
			bool flag = false;
			foreach (NKMUnitData nkmunitData in lstUnitData)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(nkmunitData);
				NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(nkmunitData.m_UnitID);
				if (unitTempletBase != null && unitStatTemplet != null)
				{
					if (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL)
					{
						NKMStatData nkmstatData = new NKMStatData();
						nkmstatData.Init();
						nkmstatData.MakeBaseStat(null, flag, nkmunitData, unitStatTemplet.m_StatData, false, 0, null);
						NKMStatData nkmstatData2 = nkmstatData;
						NKMUnitData unitData = nkmunitData;
						NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
						nkmstatData2.MakeBaseBonusFactor(unitData, (myUserData != null) ? myUserData.m_InventoryData.EquipItems : null, null, null, flag);
						this.SetUnitAttackCache(nkmunitData.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_ATK, nkmstatData));
						this.SetUnitHPCache(nkmunitData.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_HP, nkmstatData));
						this.SetUnitDefCache(nkmunitData.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_DEF, nkmstatData));
						this.SetUnitCritCache(nkmunitData.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_CRITICAL, nkmstatData));
						this.SetUnitHitCache(nkmunitData.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_HIT, nkmstatData));
						this.SetUnitEvadeCache(nkmunitData.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_EVADE, nkmstatData));
						this.SetUnitCritCache(nkmunitData.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_CRITICAL, nkmstatData));
						this.SetEnhanceProgressCache(nkmunitData.m_UnitUID, this.MakeEnhanceProgressValue(nkmunitData));
						int power = nkmunitData.CalculateOperationPower((userData != null) ? userData.m_InventoryData : null, 0, null, null);
						this.SetUnitPowerCache(nkmunitData.m_UnitUID, power);
					}
					else if (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
					{
						NKMStatData statData = NKMUnitStatManager.MakeFinalStat(nkmunitData, null, null);
						this.SetUnitAttackCache(nkmunitData.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_ATK, statData));
						this.SetUnitHPCache(nkmunitData.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_HP, statData));
						int power2 = nkmunitData.CalculateOperationPower((userData != null) ? userData.m_InventoryData : null, 0, null, null);
						this.SetUnitPowerCache(nkmunitData.m_UnitUID, power2);
					}
				}
			}
		}

		// Token: 0x06003DCF RID: 15823 RVA: 0x0013E2A4 File Offset: 0x0013C4A4
		public void UpdateScoutProgressCache()
		{
			foreach (KeyValuePair<long, NKMUnitData> keyValuePair in this.m_dicAllUnitList)
			{
				this.SetScoutProgressCache(keyValuePair.Key, this.MakeScoutProgressValue(keyValuePair.Value));
			}
		}

		// Token: 0x06003DD0 RID: 15824 RVA: 0x0013E30C File Offset: 0x0013C50C
		public void UpdateLimitBreakProcessCache()
		{
			foreach (KeyValuePair<long, NKMUnitData> keyValuePair in this.m_dicAllUnitList)
			{
				this.SetLimitBreakProgressCache(keyValuePair.Key, NKMUnitLimitBreakManager.CanThisUnitLimitBreakNow(keyValuePair.Value, NKCScenManager.CurrentUserData()));
			}
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x0013E378 File Offset: 0x0013C578
		public void UpdateLoyaltyCache()
		{
			foreach (KeyValuePair<long, NKMUnitData> keyValuePair in this.m_dicAllUnitList)
			{
				this.SetLoyaltyCache(keyValuePair.Key, keyValuePair.Value.loyalty);
			}
		}

		// Token: 0x06003DD2 RID: 15826 RVA: 0x0013E3E0 File Offset: 0x0013C5E0
		public void UpdateTacticUpdateProcessCache()
		{
			foreach (KeyValuePair<long, NKMUnitData> keyValuePair in this.m_dicAllUnitList)
			{
				this.SetLimitBreakProgressCache(keyValuePair.Key, NKMUnitLimitBreakManager.CanThisUnitLimitBreakNow(keyValuePair.Value, NKCScenManager.CurrentUserData()));
			}
		}

		// Token: 0x06003DD3 RID: 15827 RVA: 0x0013E44C File Offset: 0x0013C64C
		public void UpdateUnitData(NKMUnitData unitData)
		{
			if (this.m_dicAllUnitList.ContainsKey(unitData.m_UnitUID))
			{
				this.m_dicAllUnitList[unitData.m_UnitUID] = unitData;
			}
		}

		// Token: 0x06003DD4 RID: 15828 RVA: 0x0013E473 File Offset: 0x0013C673
		private void SetDeckedUnitIdCache(int unitId, int deckIndex)
		{
			if (!this.m_dicDeckedUnitIdCache.ContainsKey(unitId))
			{
				this.m_dicDeckedUnitIdCache.Add(unitId, new HashSet<int>());
			}
			this.m_dicDeckedUnitIdCache[unitId].Add(deckIndex);
		}

		// Token: 0x06003DD5 RID: 15829 RVA: 0x0013E4A7 File Offset: 0x0013C6A7
		private void SetDeckedShipIdCache(int shipGroupId, int deckIndex)
		{
			if (!this.m_dicDeckedShipGroupIdCache.ContainsKey(shipGroupId))
			{
				this.m_dicDeckedShipGroupIdCache.Add(shipGroupId, new HashSet<int>());
			}
			this.m_dicDeckedShipGroupIdCache[shipGroupId].Add(deckIndex);
		}

		// Token: 0x06003DD6 RID: 15830 RVA: 0x0013E4DB File Offset: 0x0013C6DB
		public bool IsDeckedUnitId(NKM_UNIT_TYPE unitType, int unitId)
		{
			if (unitType != NKM_UNIT_TYPE.NUT_NORMAL)
			{
				return unitType == NKM_UNIT_TYPE.NUT_SHIP && this.GetDeckedShipIdCache(unitId).Count > 0;
			}
			return this.GetDeckedUnitIdCache(unitId).Count > 0;
		}

		// Token: 0x06003DD7 RID: 15831 RVA: 0x0013E508 File Offset: 0x0013C708
		private HashSet<int> GetDeckedUnitIdCache(int unitId)
		{
			if (this.m_dicDeckedUnitIdCache.ContainsKey(unitId) && this.m_dicDeckedUnitIdCache[unitId] != null)
			{
				return this.m_dicDeckedUnitIdCache[unitId];
			}
			return new HashSet<int>();
		}

		// Token: 0x06003DD8 RID: 15832 RVA: 0x0013E538 File Offset: 0x0013C738
		private HashSet<int> GetDeckedShipIdCache(int shipId)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(shipId);
			if (unitTempletBase == null)
			{
				return new HashSet<int>();
			}
			if (this.m_dicDeckedShipGroupIdCache.ContainsKey(unitTempletBase.m_ShipGroupID) && this.m_dicDeckedShipGroupIdCache[unitTempletBase.m_ShipGroupID] != null)
			{
				return this.m_dicDeckedShipGroupIdCache[unitTempletBase.m_ShipGroupID];
			}
			return new HashSet<int>();
		}

		// Token: 0x06003DD9 RID: 15833 RVA: 0x0013E594 File Offset: 0x0013C794
		protected virtual void BuildUnitStateCache(NKMUserData userData, NKM_DECK_TYPE eNKM_DECK_TYPE)
		{
			this.m_dicUnitInfoCache.Clear();
			this.m_dicDeckedUnitIdCache.Clear();
			this.m_dicDeckedShipGroupIdCache.Clear();
			if (userData == null)
			{
				return;
			}
			if (eNKM_DECK_TYPE == NKM_DECK_TYPE.NDT_NONE)
			{
				return;
			}
			NKMArmyData armyData = userData.m_ArmyData;
			foreach (KeyValuePair<NKMDeckIndex, NKMDeckData> keyValuePair in armyData.GetAllDecks())
			{
				NKMDeckData value = keyValuePair.Value;
				long shipUID = value.m_ShipUID;
				if (shipUID != 0L && armyData.m_dicMyShip.ContainsKey(shipUID))
				{
					this.SetDeckIndexCache(shipUID, keyValuePair.Key);
				}
				for (int i = 0; i < value.m_listDeckUnitUID.Count; i++)
				{
					long num = value.m_listDeckUnitUID[i];
					if (armyData.m_dicMyUnit.ContainsKey(num))
					{
						this.SetDeckIndexCache(num, keyValuePair.Key);
					}
				}
			}
			if (eNKM_DECK_TYPE == NKM_DECK_TYPE.NDT_NORMAL)
			{
				foreach (NKMWorldMapCityData nkmworldMapCityData in userData.m_WorldmapData.worldMapCityDataMap.Values)
				{
					if (nkmworldMapCityData.leaderUnitUID != 0L)
					{
						this.SetCityInfoCache(nkmworldMapCityData.leaderUnitUID, NKMWorldMapManager.WorldMapLeaderState.CityLeader);
					}
				}
			}
			foreach (KeyValuePair<long, NKMUnitData> keyValuePair2 in armyData.m_dicMyUnit)
			{
				NKMUnitData value2 = keyValuePair2.Value;
				NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(value2.m_UnitID);
				this.SetTacticUpdateProgressCache(value2.m_UnitUID, NKCUITacticUpdate.CanThisUnitTactocUpdateNow(value2, NKCScenManager.CurrentUserData()));
				this.SetLimitBreakProgressCache(value2.m_UnitUID, NKMUnitLimitBreakManager.CanThisUnitLimitBreakNow(value2, NKCScenManager.CurrentUserData()));
				this.SetLoyaltyCache(value2.m_UnitUID, value2.loyalty);
				if (unitStatTemplet != null)
				{
					bool flag = false;
					NKMStatData nkmstatData = new NKMStatData();
					nkmstatData.Init();
					nkmstatData.MakeBaseStat(null, flag, value2, unitStatTemplet.m_StatData, false, 0, null);
					NKMStatData nkmstatData2 = nkmstatData;
					NKMUnitData unitData = value2;
					NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
					nkmstatData2.MakeBaseBonusFactor(unitData, (myUserData != null) ? myUserData.m_InventoryData.EquipItems : null, null, null, flag);
					this.SetUnitAttackCache(value2.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_ATK, nkmstatData));
					this.SetUnitHPCache(value2.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_HP, nkmstatData));
					this.SetUnitDefCache(value2.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_DEF, nkmstatData));
					this.SetUnitCritCache(value2.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_CRITICAL, nkmstatData));
					this.SetUnitHitCache(value2.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_HIT, nkmstatData));
					this.SetUnitEvadeCache(value2.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_EVADE, nkmstatData));
					this.SetUnitCritCache(value2.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_CRITICAL, nkmstatData));
					this.SetEnhanceProgressCache(value2.m_UnitUID, this.MakeEnhanceProgressValue(value2));
					int power = value2.CalculateOperationPower(userData.m_InventoryData, 0, null, null);
					this.SetUnitPowerCache(value2.m_UnitUID, power);
				}
			}
			foreach (KeyValuePair<long, NKMUnitData> keyValuePair3 in armyData.m_dicMyShip)
			{
				NKMUnitData value3 = keyValuePair3.Value;
				if (NKMUnitManager.GetUnitStatTemplet(value3.m_UnitID) != null)
				{
					NKMStatData statData = NKMUnitStatManager.MakeFinalStat(value3, null, null);
					this.SetUnitAttackCache(value3.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_ATK, statData));
					this.SetUnitHPCache(value3.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_HP, statData));
					int power2 = value3.CalculateOperationPower(userData.m_InventoryData, 0, null, null);
					this.SetUnitPowerCache(value3.m_UnitUID, power2);
				}
			}
		}

		// Token: 0x06003DDA RID: 15834 RVA: 0x0013E9C0 File Offset: 0x0013CBC0
		protected virtual void BuildLocalUnitStateCache(NKMUserData userData, NKM_DECK_TYPE eNKM_DECK_TYPE)
		{
			this.m_dicUnitInfoCache.Clear();
			this.m_dicDeckedUnitIdCache.Clear();
			this.m_dicDeckedShipGroupIdCache.Clear();
			if (userData == null)
			{
				return;
			}
			NKMArmyData armyData = userData.m_ArmyData;
			Dictionary<int, NKMEventDeckData> allLocalDeckData = NKCLocalDeckDataManager.GetAllLocalDeckData();
			foreach (KeyValuePair<int, NKMEventDeckData> keyValuePair in allLocalDeckData)
			{
				NKMDeckIndex deckindex = new NKMDeckIndex(eNKM_DECK_TYPE, keyValuePair.Key);
				foreach (KeyValuePair<int, long> keyValuePair2 in keyValuePair.Value.m_dicUnit)
				{
					long value = keyValuePair2.Value;
					if (armyData.m_dicMyUnit.ContainsKey(value))
					{
						this.SetDeckIndexCache(value, deckindex);
					}
					NKMUnitData unitFromUID = armyData.GetUnitFromUID(value);
					if (unitFromUID != null)
					{
						this.SetDeckedUnitIdCache(unitFromUID.m_UnitID, keyValuePair.Key);
					}
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitFromUID);
					if (unitTempletBase != null && unitTempletBase.m_BaseUnitID > 0)
					{
						this.SetDeckedUnitIdCache(unitTempletBase.m_BaseUnitID, keyValuePair.Key);
					}
				}
			}
			foreach (KeyValuePair<int, NKMEventDeckData> keyValuePair3 in allLocalDeckData)
			{
				NKMDeckIndex deckindex2 = new NKMDeckIndex(eNKM_DECK_TYPE, keyValuePair3.Key);
				long shipUID = keyValuePair3.Value.m_ShipUID;
				if (armyData.m_dicMyShip.ContainsKey(shipUID))
				{
					this.SetDeckIndexCache(shipUID, deckindex2);
				}
				NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(armyData.GetShipFromUID(shipUID));
				if (unitTempletBase2 != null)
				{
					this.SetDeckedShipIdCache(unitTempletBase2.m_ShipGroupID, keyValuePair3.Key);
				}
			}
			if (eNKM_DECK_TYPE == NKM_DECK_TYPE.NDT_NORMAL)
			{
				foreach (NKMWorldMapCityData nkmworldMapCityData in userData.m_WorldmapData.worldMapCityDataMap.Values)
				{
					if (nkmworldMapCityData.leaderUnitUID != 0L)
					{
						this.SetCityInfoCache(nkmworldMapCityData.leaderUnitUID, NKMWorldMapManager.WorldMapLeaderState.CityLeader);
					}
				}
			}
			foreach (KeyValuePair<long, NKMUnitData> keyValuePair4 in armyData.m_dicMyUnit)
			{
				NKMUnitData value2 = keyValuePair4.Value;
				NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(value2.m_UnitID);
				this.SetLimitBreakProgressCache(value2.m_UnitUID, NKMUnitLimitBreakManager.CanThisUnitLimitBreakNow(value2, NKCScenManager.CurrentUserData()));
				this.SetTacticUpdateProgressCache(value2.m_UnitUID, NKMUnitLimitBreakManager.CanThisUnitLimitBreakNow(value2, NKCScenManager.CurrentUserData()));
				this.SetLoyaltyCache(value2.m_UnitUID, value2.loyalty);
				if (unitStatTemplet != null)
				{
					bool flag = false;
					NKMStatData nkmstatData = new NKMStatData();
					nkmstatData.Init();
					nkmstatData.MakeBaseStat(null, flag, value2, unitStatTemplet.m_StatData, false, 0, null);
					NKMStatData nkmstatData2 = nkmstatData;
					NKMUnitData unitData = value2;
					NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
					nkmstatData2.MakeBaseBonusFactor(unitData, (myUserData != null) ? myUserData.m_InventoryData.EquipItems : null, null, null, flag);
					this.SetUnitAttackCache(value2.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_ATK, nkmstatData));
					this.SetUnitHPCache(value2.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_HP, nkmstatData));
					this.SetUnitDefCache(value2.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_DEF, nkmstatData));
					this.SetUnitCritCache(value2.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_CRITICAL, nkmstatData));
					this.SetUnitHitCache(value2.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_HIT, nkmstatData));
					this.SetUnitEvadeCache(value2.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_EVADE, nkmstatData));
					this.SetUnitCritCache(value2.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_CRITICAL, nkmstatData));
					this.SetEnhanceProgressCache(value2.m_UnitUID, this.MakeEnhanceProgressValue(value2));
					int power = value2.CalculateOperationPower(userData.m_InventoryData, 0, null, null);
					this.SetUnitPowerCache(value2.m_UnitUID, power);
				}
			}
			foreach (KeyValuePair<long, NKMUnitData> keyValuePair5 in armyData.m_dicMyShip)
			{
				NKMUnitData value3 = keyValuePair5.Value;
				if (NKMUnitManager.GetUnitStatTemplet(value3.m_UnitID) != null)
				{
					NKMStatData statData = NKMUnitStatManager.MakeFinalStat(value3, null, null);
					this.SetUnitAttackCache(value3.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_ATK, statData));
					this.SetUnitHPCache(value3.m_UnitUID, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_HP, statData));
					int power2 = value3.CalculateOperationPower(userData.m_InventoryData, 0, null, null);
					this.SetUnitPowerCache(value3.m_UnitUID, power2);
				}
			}
		}

		// Token: 0x06003DDB RID: 15835 RVA: 0x0013EEE0 File Offset: 0x0013D0E0
		private Dictionary<long, NKMUnitData> BuildFullUnitList(NKMUserData userData, IEnumerable<NKMUnitData> lstTargetUnits, NKCUnitSortSystem.UnitListOptions options)
		{
			Dictionary<long, NKMUnitData> dictionary = new Dictionary<long, NKMUnitData>();
			HashSet<int> setOnlyIncludeUnitID = options.setOnlyIncludeUnitID;
			HashSet<int> setOnlyIncludeUnitBaseID = options.setOnlyIncludeUnitBaseID;
			HashSet<int> setExcludeUnitID = options.setExcludeUnitID;
			HashSet<int> setExcludeUnitBaseID = options.setExcludeUnitBaseID;
			foreach (NKMUnitData nkmunitData in lstTargetUnits)
			{
				long unitUID = nkmunitData.m_UnitUID;
				if ((options.AdditionalExcludeFilterFunc == null || options.AdditionalExcludeFilterFunc(nkmunitData)) && (options.setExcludeUnitUID == null || !options.setExcludeUnitUID.Contains(unitUID)) && (!options.bExcludeDeckedUnit || (this.GetDeckIndexCache(unitUID, false).m_eDeckType == NKM_DECK_TYPE.NDT_NONE && this.GetCityStateCache(unitUID) == NKMWorldMapManager.WorldMapLeaderState.None && !this.IsMainUnit(unitUID, userData) && nkmunitData.OfficeRoomId <= 0)) && (!options.bExcludeLockedUnit || !nkmunitData.m_bLock) && (setOnlyIncludeUnitID == null || setOnlyIncludeUnitID.Contains(nkmunitData.m_UnitID)))
				{
					if (setOnlyIncludeUnitBaseID != null)
					{
						NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(nkmunitData);
						if (unitTempletBase == null || !setOnlyIncludeUnitBaseID.Any((int x) => unitTempletBase.IsSameBaseUnit(x)))
						{
							continue;
						}
					}
					if ((setExcludeUnitID == null || !setExcludeUnitID.Contains(nkmunitData.m_UnitID) || (options.bIncludeSeizure && nkmunitData.IsSeized)) && (setExcludeUnitBaseID == null || !NKMUnitManager.CheckContainsBaseUnit(setExcludeUnitBaseID, nkmunitData.m_UnitID)) && (options.setOnlyIncludeFilterOption == null || this.CheckFilter(nkmunitData, options.setOnlyIncludeFilterOption)))
					{
						NKCUnitSortSystem.eUnitState state = NKCUnitSortSystem.eUnitState.NONE;
						if (userData != null)
						{
							NKMDeckIndex deckIndexCache = this.GetDeckIndexCache(unitUID, !this.m_Options.bUseDeckedState);
							NKMWorldMapManager.WorldMapLeaderState cityStateCache = this.GetCityStateCache(unitUID);
							NKMDeckData deckData = userData.m_ArmyData.GetDeckData(deckIndexCache);
							if (nkmunitData.IsSeized)
							{
								state = NKCUnitSortSystem.eUnitState.SEIZURE;
							}
							else if (options.setDuplicateUnitID != null && NKMUnitManager.CheckContainsBaseUnit(options.setDuplicateUnitID, nkmunitData.m_UnitID))
							{
								state = NKCUnitSortSystem.eUnitState.DUPLICATE;
							}
							else if (!this.m_Options.bIgnoreMissionState && deckData != null && deckData.GetState() == NKM_DECK_STATE.DECK_STATE_WARFARE)
							{
								state = NKCUnitSortSystem.eUnitState.WARFARE_BATCH;
							}
							else if (!this.m_Options.bIgnoreMissionState && deckData != null && deckData.GetState() == NKM_DECK_STATE.DECK_STATE_DIVE)
							{
								state = NKCUnitSortSystem.eUnitState.DIVE_BATCH;
							}
							else if (!this.m_Options.bIgnoreMissionState && !this.m_Options.bIgnoreCityState && deckData != null && deckData.GetState() == NKM_DECK_STATE.DECK_STATE_WORLDMAP_MISSION)
							{
								state = NKCUnitSortSystem.eUnitState.CITY_MISSION;
							}
							else if ((!this.m_Options.bIgnoreCityState || !this.m_Options.bIgnoreWorldMapLeader) && cityStateCache != NKMWorldMapManager.WorldMapLeaderState.None)
							{
								if (cityStateCache != NKMWorldMapManager.WorldMapLeaderState.None)
								{
									if (cityStateCache - NKMWorldMapManager.WorldMapLeaderState.CityLeader <= 1)
									{
										using (Dictionary<int, NKMWorldMapCityData>.ValueCollection.Enumerator enumerator2 = userData.m_WorldmapData.worldMapCityDataMap.Values.GetEnumerator())
										{
											while (enumerator2.MoveNext())
											{
												NKMWorldMapCityData nkmworldMapCityData = enumerator2.Current;
												if (nkmworldMapCityData.leaderUnitUID == unitUID)
												{
													if (nkmworldMapCityData.HasMission())
													{
														if (!this.m_Options.bIgnoreMissionState && !this.m_Options.bIgnoreCityState)
														{
															state = NKCUnitSortSystem.eUnitState.CITY_MISSION;
															break;
														}
														break;
													}
													else
													{
														if (!this.m_Options.bIgnoreWorldMapLeader)
														{
															state = NKCUnitSortSystem.eUnitState.CITY_SET;
															break;
														}
														break;
													}
												}
											}
											goto IL_3F1;
										}
									}
									Debug.LogError("City Setstate added?");
								}
							}
							else if (this.m_Options.bUseDeckedState && deckIndexCache.m_eDeckType != NKM_DECK_TYPE.NDT_NONE)
							{
								state = NKCUnitSortSystem.eUnitState.DECKED;
							}
							else if (this.m_Options.bUseDeckedState && this.IsMainUnit(unitUID, userData))
							{
								state = NKCUnitSortSystem.eUnitState.MAINUNIT;
							}
							else if (this.m_Options.bUseLockedState && nkmunitData.m_bLock)
							{
								state = NKCUnitSortSystem.eUnitState.LOCKED;
							}
							else if (this.m_Options.bUseLobbyState && this.IsMainUnit(unitUID, userData))
							{
								if (userData.GetBackgroundUnitIndex(unitUID) >= 0)
								{
									state = NKCUnitSortSystem.eUnitState.LOBBY_UNIT;
								}
							}
							else if (this.m_Options.AdditionalUnitStateFunc != null)
							{
								state = this.m_Options.AdditionalUnitStateFunc(nkmunitData);
							}
							else if (this.m_Options.bUseDormInState && nkmunitData.OfficeRoomId > 0)
							{
								state = NKCUnitSortSystem.eUnitState.OFFICE_DORM_IN;
							}
							else
							{
								state = NKCUnitSortSystem.eUnitState.NONE;
							}
						}
						IL_3F1:
						this.SetUnitSlotState(unitUID, state);
						dictionary.Add(unitUID, nkmunitData);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06003DDC RID: 15836 RVA: 0x0013F344 File Offset: 0x0013D544
		private bool IsMainUnit(long unitUID, NKMUserData userData)
		{
			return userData != null && userData.GetBackgroundUnitIndex(unitUID) >= 0;
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x0013F358 File Offset: 0x0013D558
		protected bool FilterData(NKMUnitData unitData, List<HashSet<NKCUnitSortSystem.eFilterOption>> setFilter)
		{
			if (!this.m_Options.bIncludeSeizure && unitData.IsSeized)
			{
				return false;
			}
			if (this.m_Options.bHideDeckedUnit)
			{
				if (!this.m_Options.bIgnoreCityState && this.IsUnitIsCityLeaderOnMission(unitData.m_UnitUID))
				{
					return false;
				}
				if (this.GetDeckIndexCache(unitData.m_UnitUID, false).m_eDeckType == this.m_Options.eDeckType)
				{
					return false;
				}
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID);
			if (!this.m_Options.bIncludeUndeckableUnit && !NKMUnitManager.CanUnitUsedInDeck(unitTempletBase))
			{
				return false;
			}
			if (setFilter == null || setFilter.Count == 0)
			{
				return true;
			}
			for (int i = 0; i < setFilter.Count; i++)
			{
				if (!this.CheckFilter(unitData, setFilter[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x0013F41C File Offset: 0x0013D61C
		protected bool FilterData(NKMUnitData unitData, NKM_UNIT_STYLE_TYPE targetType)
		{
			if (this.m_Options.bHideDeckedUnit)
			{
				if (this.GetCityStateCache(unitData.m_UnitUID) != NKMWorldMapManager.WorldMapLeaderState.None)
				{
					return false;
				}
				if (this.GetDeckIndexCache(unitData.m_UnitUID, false).m_eDeckType == this.m_Options.eDeckType)
				{
					return false;
				}
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID);
			return (this.m_Options.bIncludeUndeckableUnit || NKMUnitManager.CanUnitUsedInDeck(unitTempletBase)) && (targetType == NKM_UNIT_STYLE_TYPE.NUST_INVALID || unitTempletBase.m_NKM_UNIT_STYLE_TYPE == targetType);
		}

		// Token: 0x06003DDF RID: 15839 RVA: 0x0013F49C File Offset: 0x0013D69C
		protected bool IsUnitSelectable(NKMUnitData unitData)
		{
			switch (this.GetUnitSlotState(unitData.m_UnitUID))
			{
			case NKCUnitSortSystem.eUnitState.NONE:
			case NKCUnitSortSystem.eUnitState.SEIZURE:
			case NKCUnitSortSystem.eUnitState.LOBBY_UNIT:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06003DE0 RID: 15840 RVA: 0x0013F500 File Offset: 0x0013D700
		private bool CheckFilter(NKMUnitData unitData, HashSet<NKCUnitSortSystem.eFilterOption> setFilter)
		{
			foreach (NKCUnitSortSystem.eFilterOption filterOption in setFilter)
			{
				if (this.CheckFilter(unitData, filterOption))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003DE1 RID: 15841 RVA: 0x0013F558 File Offset: 0x0013D758
		private bool CheckFinalUnitCost(NKMUnitStatTemplet unitStatTemplet, int cost)
		{
			if (this.m_bEnableShowBan && NKCBanManager.IsBanUnit(unitStatTemplet.m_UnitID, NKCBanManager.BAN_DATA_TYPE.FINAL))
			{
				if (unitStatTemplet.GetRespawnCost(true, false, NKCBanManager.GetBanData(NKCBanManager.BAN_DATA_TYPE.FINAL), null) == cost)
				{
					return true;
				}
			}
			else if (this.m_bEnableShowUpUnit && NKCBanManager.IsUpUnit(unitStatTemplet.m_UnitID))
			{
				if (unitStatTemplet.GetRespawnCost(true, false, null, NKCBanManager.m_dicNKMUpData) == cost)
				{
					return true;
				}
			}
			else if (unitStatTemplet.GetRespawnCost(false, null, null) == cost)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06003DE2 RID: 15842 RVA: 0x0013F5C8 File Offset: 0x0013D7C8
		private bool CheckFilter(NKMUnitData unitData, NKCUnitSortSystem.eFilterOption filterOption)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData);
			if (unitTempletBase == null)
			{
				Debug.LogError(string.Format("UnitTemplet Null! unitID : {0}", unitData.m_UnitID));
				return false;
			}
			switch (filterOption)
			{
			case NKCUnitSortSystem.eFilterOption.Nothing:
				return false;
			case NKCUnitSortSystem.eFilterOption.Everything:
				return true;
			case NKCUnitSortSystem.eFilterOption.Unit_Counter:
				if (unitTempletBase.HasUnitStyleType(NKM_UNIT_STYLE_TYPE.NUST_COUNTER))
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Unit_Mechanic:
				if (unitTempletBase.HasUnitStyleType(NKM_UNIT_STYLE_TYPE.NUST_MECHANIC))
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Unit_Soldier:
				if (unitTempletBase.HasUnitStyleType(NKM_UNIT_STYLE_TYPE.NUST_SOLDIER))
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Unit_Corrupted:
				if (unitTempletBase.HasUnitStyleType(NKM_UNIT_STYLE_TYPE.NUST_CORRUPTED))
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Unit_Replacer:
				if (unitTempletBase.HasUnitStyleType(NKM_UNIT_STYLE_TYPE.NUST_REPLACER))
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Unit_Trainer:
				if (unitTempletBase.HasUnitStyleType(NKM_UNIT_STYLE_TYPE.NUST_TRAINER))
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Unit_Target_Ground:
			case NKCUnitSortSystem.eFilterOption.Unit_Target_Air:
			case NKCUnitSortSystem.eFilterOption.Unit_Target_All:
				if (unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc != NKM_FIND_TARGET_TYPE.NFTT_INVALID)
				{
					if (NKCUnitSortSystem.GetFilterOption(unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc) == filterOption)
					{
						return true;
					}
				}
				else if (NKCUnitSortSystem.GetFilterOption(unitTempletBase.m_NKM_FIND_TARGET_TYPE) == filterOption)
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Unit_Move_Ground:
				return !unitTempletBase.m_bAirUnit;
			case NKCUnitSortSystem.eFilterOption.Unit_Move_Air:
				return unitTempletBase.m_bAirUnit;
			case NKCUnitSortSystem.eFilterOption.Ship_Assault:
				if (unitTempletBase.HasUnitStyleType(NKM_UNIT_STYLE_TYPE.NUST_SHIP_ASSAULT))
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Ship_Heavy:
				if (unitTempletBase.HasUnitStyleType(NKM_UNIT_STYLE_TYPE.NUST_SHIP_HEAVY))
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Ship_Cruiser:
				if (unitTempletBase.HasUnitStyleType(NKM_UNIT_STYLE_TYPE.NUST_SHIP_CRUISER))
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Ship_Special:
				if (unitTempletBase.HasUnitStyleType(NKM_UNIT_STYLE_TYPE.NUST_SHIP_SPECIAL))
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Ship_Patrol:
				if (unitTempletBase.HasUnitStyleType(NKM_UNIT_STYLE_TYPE.NUST_SHIP_PATROL))
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Ship_Etc:
				if (unitTempletBase.HasUnitStyleType(NKM_UNIT_STYLE_TYPE.NUST_SHIP_ETC))
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Role_Striker:
				if (unitTempletBase.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_STRIKER)
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Role_Ranger:
				if (unitTempletBase.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_RANGER)
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Role_Sniper:
				if (unitTempletBase.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_SNIPER)
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Role_Defender:
				if (unitTempletBase.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_DEFENDER)
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Role_Siege:
				if (unitTempletBase.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_SIEGE)
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Role_Supporter:
				if (unitTempletBase.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER)
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Role_Tower:
				if (unitTempletBase.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_TOWER)
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Rarily_SSR:
				if (unitTempletBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SSR)
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Rarily_SR:
				if (unitTempletBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SR)
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Rarily_R:
				if (unitTempletBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_R)
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Rarily_N:
				if (unitTempletBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_N)
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Unit_Cost_10:
			{
				NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitTempletBase.m_UnitID);
				if (unitStatTemplet == null)
				{
					return false;
				}
				if (this.CheckFinalUnitCost(unitStatTemplet, 10))
				{
					return true;
				}
				break;
			}
			case NKCUnitSortSystem.eFilterOption.Unit_Cost_9:
			{
				NKMUnitStatTemplet unitStatTemplet2 = NKMUnitManager.GetUnitStatTemplet(unitTempletBase.m_UnitID);
				if (unitStatTemplet2 == null)
				{
					return false;
				}
				if (this.CheckFinalUnitCost(unitStatTemplet2, 9))
				{
					return true;
				}
				break;
			}
			case NKCUnitSortSystem.eFilterOption.Unit_Cost_8:
			{
				NKMUnitStatTemplet unitStatTemplet3 = NKMUnitManager.GetUnitStatTemplet(unitTempletBase.m_UnitID);
				if (unitStatTemplet3 == null)
				{
					return false;
				}
				if (this.CheckFinalUnitCost(unitStatTemplet3, 8))
				{
					return true;
				}
				break;
			}
			case NKCUnitSortSystem.eFilterOption.Unit_Cost_7:
			{
				NKMUnitStatTemplet unitStatTemplet4 = NKMUnitManager.GetUnitStatTemplet(unitTempletBase.m_UnitID);
				if (unitStatTemplet4 == null)
				{
					return false;
				}
				if (this.CheckFinalUnitCost(unitStatTemplet4, 7))
				{
					return true;
				}
				break;
			}
			case NKCUnitSortSystem.eFilterOption.Unit_Cost_6:
			{
				NKMUnitStatTemplet unitStatTemplet5 = NKMUnitManager.GetUnitStatTemplet(unitTempletBase.m_UnitID);
				if (unitStatTemplet5 == null)
				{
					return false;
				}
				if (this.CheckFinalUnitCost(unitStatTemplet5, 6))
				{
					return true;
				}
				break;
			}
			case NKCUnitSortSystem.eFilterOption.Unit_Cost_5:
			{
				NKMUnitStatTemplet unitStatTemplet6 = NKMUnitManager.GetUnitStatTemplet(unitTempletBase.m_UnitID);
				if (unitStatTemplet6 == null)
				{
					return false;
				}
				if (this.CheckFinalUnitCost(unitStatTemplet6, 5))
				{
					return true;
				}
				break;
			}
			case NKCUnitSortSystem.eFilterOption.Unit_Cost_4:
			{
				NKMUnitStatTemplet unitStatTemplet7 = NKMUnitManager.GetUnitStatTemplet(unitTempletBase.m_UnitID);
				if (unitStatTemplet7 == null)
				{
					return false;
				}
				if (this.CheckFinalUnitCost(unitStatTemplet7, 4))
				{
					return true;
				}
				break;
			}
			case NKCUnitSortSystem.eFilterOption.Unit_Cost_3:
			{
				NKMUnitStatTemplet unitStatTemplet8 = NKMUnitManager.GetUnitStatTemplet(unitTempletBase.m_UnitID);
				if (unitStatTemplet8 == null)
				{
					return false;
				}
				if (this.CheckFinalUnitCost(unitStatTemplet8, 3))
				{
					return true;
				}
				break;
			}
			case NKCUnitSortSystem.eFilterOption.Unit_Cost_2:
			{
				NKMUnitStatTemplet unitStatTemplet9 = NKMUnitManager.GetUnitStatTemplet(unitTempletBase.m_UnitID);
				if (unitStatTemplet9 == null)
				{
					return false;
				}
				if (this.CheckFinalUnitCost(unitStatTemplet9, 2))
				{
					return true;
				}
				break;
			}
			case NKCUnitSortSystem.eFilterOption.Unit_Cost_1:
			{
				NKMUnitStatTemplet unitStatTemplet10 = NKMUnitManager.GetUnitStatTemplet(unitTempletBase.m_UnitID);
				if (unitStatTemplet10 == null)
				{
					return false;
				}
				if (this.CheckFinalUnitCost(unitStatTemplet10, 1))
				{
					return true;
				}
				break;
			}
			case NKCUnitSortSystem.eFilterOption.Unit_TacticLv_6:
				return unitData.tacticLevel == 6;
			case NKCUnitSortSystem.eFilterOption.Unit_TacticLv_5:
				return unitData.tacticLevel == 5;
			case NKCUnitSortSystem.eFilterOption.Unit_TacticLv_4:
				return unitData.tacticLevel == 4;
			case NKCUnitSortSystem.eFilterOption.Unit_TacticLv_3:
				return unitData.tacticLevel == 3;
			case NKCUnitSortSystem.eFilterOption.Unit_TacticLv_2:
				return unitData.tacticLevel == 2;
			case NKCUnitSortSystem.eFilterOption.Unit_TacticLv_1:
				return unitData.tacticLevel == 1;
			case NKCUnitSortSystem.eFilterOption.Unit_TacticLv_0:
				return unitData.tacticLevel == 0;
			case NKCUnitSortSystem.eFilterOption.Level_1:
				if (unitData.m_UnitLevel == 1)
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Level_other:
			{
				if (unitData.m_UnitLevel == 1)
				{
					return false;
				}
				NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(unitData);
				if (unitTempletBase2 == null)
				{
					return false;
				}
				if (unitTempletBase2.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
				{
					NKMShipBuildTemplet shipBuildTemplet = NKMShipManager.GetShipBuildTemplet(unitData.m_UnitID);
					if (shipBuildTemplet == null)
					{
						return false;
					}
					if (shipBuildTemplet.ShipUpgradeTarget1 > 0)
					{
						return true;
					}
				}
				if (unitData.m_UnitLevel != NKCExpManager.GetUnitMaxLevel(unitData))
				{
					return true;
				}
				break;
			}
			case NKCUnitSortSystem.eFilterOption.Level_Max:
			{
				if (unitData.m_UnitLevel != NKCExpManager.GetUnitMaxLevel(unitData))
				{
					return false;
				}
				NKMUnitTempletBase unitTempletBase3 = NKMUnitManager.GetUnitTempletBase(unitData);
				if (unitTempletBase3 == null)
				{
					return false;
				}
				if (unitTempletBase3.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
				{
					NKMShipBuildTemplet shipBuildTemplet2 = NKMShipManager.GetShipBuildTemplet(unitData.m_UnitID);
					if (shipBuildTemplet2 == null || shipBuildTemplet2.ShipUpgradeTarget1 > 0)
					{
						return false;
					}
				}
				return true;
			}
			case NKCUnitSortSystem.eFilterOption.Decked:
				if (this.GetDeckIndexCache(unitData.m_UnitUID, false) != NKMDeckIndex.None)
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.NotDecked:
				if (this.GetDeckIndexCache(unitData.m_UnitUID, false) == NKMDeckIndex.None)
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Locked:
				if (unitData.m_bLock)
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Unlocked:
				if (!unitData.m_bLock)
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.Have:
				if (unitTempletBase.IsShip())
				{
					if (NKCScenManager.CurrentUserData().m_ArmyData.GetSameKindShipCountFromID(unitData.m_UnitID) > 0)
					{
						return true;
					}
				}
				else if (NKCScenManager.CurrentUserData().m_ArmyData.GetUnitCountByID(unitData.m_UnitID) > 0)
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.NotHave:
				if (unitTempletBase.IsShip())
				{
					if (NKCScenManager.CurrentUserData().m_ArmyData.GetSameKindShipCountFromID(unitData.m_UnitID) == 0)
					{
						return true;
					}
				}
				else if (NKCScenManager.CurrentUserData().m_ArmyData.GetUnitCountByID(unitData.m_UnitID) == 0)
				{
					return true;
				}
				break;
			case NKCUnitSortSystem.eFilterOption.InRoom:
				return unitData.OfficeRoomId > 0;
			case NKCUnitSortSystem.eFilterOption.OutRoom:
				return unitData.OfficeRoomId <= 0;
			case NKCUnitSortSystem.eFilterOption.Loyalty_Zero:
				return unitData.loyalty <= 0;
			case NKCUnitSortSystem.eFilterOption.Loyalty_Intermediate:
				return unitData.loyalty > 0 && unitData.loyalty < 10000;
			case NKCUnitSortSystem.eFilterOption.Loyalty_Max:
				return unitData.loyalty >= 10000;
			case NKCUnitSortSystem.eFilterOption.LifeContract_Unsigned:
				return !unitData.IsPermanentContract;
			case NKCUnitSortSystem.eFilterOption.LifeContract_Signed:
				return unitData.IsPermanentContract;
			case NKCUnitSortSystem.eFilterOption.CanScout:
			{
				NKMPieceTemplet nkmpieceTemplet = NKMTempletContainer<NKMPieceTemplet>.Find((int)unitData.m_UnitUID);
				if (nkmpieceTemplet != null && nkmpieceTemplet.CanExchange(NKCScenManager.CurrentUserData()) == NKM_ERROR_CODE.NEC_OK)
				{
					return true;
				}
				break;
			}
			case NKCUnitSortSystem.eFilterOption.NoScout:
			{
				NKMPieceTemplet nkmpieceTemplet2 = NKMTempletContainer<NKMPieceTemplet>.Find((int)unitData.m_UnitUID);
				if (nkmpieceTemplet2 != null && nkmpieceTemplet2.CanExchange(NKCScenManager.CurrentUserData()) != NKM_ERROR_CODE.NEC_OK)
				{
					return true;
				}
				break;
			}
			case NKCUnitSortSystem.eFilterOption.SpecialType_Rearm:
			{
				NKMUnitTempletBase unitTempletBase4 = NKMUnitManager.GetUnitTempletBase(unitData);
				return unitTempletBase4 != null && unitTempletBase4.IsRearmUnit;
			}
			case NKCUnitSortSystem.eFilterOption.SpecialType_Awaken:
			{
				NKMUnitTempletBase unitTempletBase5 = NKMUnitManager.GetUnitTempletBase(unitData);
				return unitTempletBase5 != null && unitTempletBase5.m_bAwaken;
			}
			case NKCUnitSortSystem.eFilterOption.Collected:
				return NKCScenManager.CurrentUserData().m_ArmyData.IsCollectedUnit(unitData.m_UnitID);
			case NKCUnitSortSystem.eFilterOption.NotCollected:
				return !NKCScenManager.CurrentUserData().m_ArmyData.IsCollectedUnit(unitData.m_UnitID);
			case NKCUnitSortSystem.eFilterOption.Collection_HasAchieve:
				return NKCUnitMissionManager.HasRewardEnableMission(unitData.m_UnitID);
			case NKCUnitSortSystem.eFilterOption.Collection_CompleteAchieve:
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				NKCUnitMissionManager.GetUnitMissionRewardEnableCount(unitData.m_UnitID, ref num, ref num2, ref num3);
				return num == num2 && num3 == 0;
			}
			case NKCUnitSortSystem.eFilterOption.Favorite:
				return unitData != null && unitData.isFavorite;
			case NKCUnitSortSystem.eFilterOption.Favorite_Not:
				return unitData == null || !unitData.isFavorite;
			}
			return false;
		}

		// Token: 0x06003DE3 RID: 15843 RVA: 0x0013FD2C File Offset: 0x0013DF2C
		public void FilterList(NKCUnitSortSystem.eFilterOption filterOption, bool bHideDeckedUnit)
		{
			this.FilterList(new HashSet<NKCUnitSortSystem.eFilterOption>
			{
				filterOption
			}, bHideDeckedUnit);
		}

		// Token: 0x06003DE4 RID: 15844 RVA: 0x0013FD50 File Offset: 0x0013DF50
		public virtual void FilterList(HashSet<NKCUnitSortSystem.eFilterOption> setFilter, bool bHideDeckedUnit)
		{
			this.m_Options.setFilterOption = setFilter;
			this.m_Options.bHideDeckedUnit = bHideDeckedUnit;
			if (this.m_lstCurrentUnitList == null)
			{
				this.m_lstCurrentUnitList = new List<NKMUnitData>();
			}
			this.m_lstCurrentUnitList.Clear();
			List<HashSet<NKCUnitSortSystem.eFilterOption>> setFilter2 = new List<HashSet<NKCUnitSortSystem.eFilterOption>>();
			this.SetFilterCategory(setFilter, ref setFilter2);
			foreach (KeyValuePair<long, NKMUnitData> keyValuePair in this.m_dicAllUnitList)
			{
				NKMUnitData value = keyValuePair.Value;
				if (this.FilterData(value, setFilter2))
				{
					this.m_lstCurrentUnitList.Add(value);
				}
			}
			if (this.m_Options.lstSortOption != null)
			{
				this.SortList(this.m_Options.lstSortOption, true);
				return;
			}
			this.m_Options.lstSortOption = new List<NKCUnitSortSystem.eSortOption>();
			this.SortList(this.m_Options.lstSortOption, true);
		}

		// Token: 0x06003DE5 RID: 15845 RVA: 0x0013FE44 File Offset: 0x0013E044
		private void SetFilterCategory(HashSet<NKCUnitSortSystem.eFilterOption> setFilter, ref List<HashSet<NKCUnitSortSystem.eFilterOption>> needFilterSet)
		{
			if (setFilter.Count == 0)
			{
				return;
			}
			for (int i = 0; i < NKCUnitSortSystem.m_lstFilterCategory.Count; i++)
			{
				HashSet<NKCUnitSortSystem.eFilterOption> hashSet = new HashSet<NKCUnitSortSystem.eFilterOption>();
				foreach (NKCUnitSortSystem.eFilterOption item in setFilter)
				{
					hashSet.Add(item);
				}
				hashSet.IntersectWith(NKCUnitSortSystem.m_lstFilterCategory[i]);
				if (hashSet.Count > 0)
				{
					needFilterSet.Add(hashSet);
				}
			}
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x0013FEDC File Offset: 0x0013E0DC
		public void SortList(NKCUnitSortSystem.eSortOption sortOption, bool bForce = false)
		{
			this.SortList(new List<NKCUnitSortSystem.eSortOption>
			{
				sortOption
			}, bForce);
		}

		// Token: 0x06003DE7 RID: 15847 RVA: 0x0013FF00 File Offset: 0x0013E100
		public void SortList(List<NKCUnitSortSystem.eSortOption> lstSortOption, bool bForce = false)
		{
			if (this.m_lstCurrentUnitList != null)
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
				this.SortUnitDataList(ref this.m_lstCurrentUnitList, lstSortOption);
				this.m_Options.lstSortOption = lstSortOption;
				return;
			}
			this.m_Options.lstSortOption = lstSortOption;
			if (this.m_Options.setFilterOption != null)
			{
				this.FilterList(this.m_Options.setFilterOption, this.m_Options.bHideDeckedUnit);
				return;
			}
			this.m_Options.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
			this.FilterList(this.m_Options.setFilterOption, this.m_Options.bHideDeckedUnit);
		}

		// Token: 0x06003DE8 RID: 15848 RVA: 0x0013FFE4 File Offset: 0x0013E1E4
		private void SortUnitDataList(ref List<NKMUnitData> lstUnitData, List<NKCUnitSortSystem.eSortOption> lstSortOption)
		{
			NKCUnitSortSystem.NKCDataComparerer<NKMUnitData> nkcdataComparerer = new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>(Array.Empty<NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc>());
			HashSet<NKCUnitSortSystem.eSortCategory> hashSet = new HashSet<NKCUnitSortSystem.eSortCategory>();
			if (this.m_Options.PreemptiveSortFunc != null)
			{
				nkcdataComparerer.AddFunc(this.m_Options.PreemptiveSortFunc);
			}
			if (this.m_Options.bPushBackUnselectable)
			{
				nkcdataComparerer.AddFunc(new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByState));
			}
			foreach (NKCUnitSortSystem.eSortOption eSortOption in lstSortOption)
			{
				if (eSortOption != NKCUnitSortSystem.eSortOption.None)
				{
					NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc dataComparer = this.GetDataComparer(eSortOption);
					if (dataComparer != null)
					{
						nkcdataComparerer.AddFunc(dataComparer);
						hashSet.Add(NKCUnitSortSystem.GetSortCategoryFromOption(eSortOption));
					}
				}
			}
			if (this.m_Options.lstDefaultSortOption != null)
			{
				foreach (NKCUnitSortSystem.eSortOption eSortOption2 in this.m_Options.lstDefaultSortOption)
				{
					NKCUnitSortSystem.eSortCategory sortCategoryFromOption = NKCUnitSortSystem.GetSortCategoryFromOption(eSortOption2);
					if (!hashSet.Contains(sortCategoryFromOption))
					{
						nkcdataComparerer.AddFunc(this.GetDataComparer(eSortOption2));
						hashSet.Add(sortCategoryFromOption);
					}
				}
			}
			if (!hashSet.Contains(NKCUnitSortSystem.eSortCategory.UID))
			{
				nkcdataComparerer.AddFunc(new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByUIDAscending));
			}
			lstUnitData.Sort(nkcdataComparerer);
		}

		// Token: 0x06003DE9 RID: 15849 RVA: 0x0014013C File Offset: 0x0013E33C
		private NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc GetDataComparer(NKCUnitSortSystem.eSortOption sortOption)
		{
			switch (sortOption)
			{
			case NKCUnitSortSystem.eSortOption.Level_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(NKCUnitSortSystem.CompareByLevelAscending);
			case NKCUnitSortSystem.eSortOption.Level_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(NKCUnitSortSystem.CompareByLevelDescending);
			case NKCUnitSortSystem.eSortOption.Rarity_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(NKCUnitSortSystem.CompareByRarityAscending);
			case NKCUnitSortSystem.eSortOption.Rarity_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(NKCUnitSortSystem.CompareByRarityDescending);
			case NKCUnitSortSystem.eSortOption.Unit_SummonCost_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByCostAscending);
			case NKCUnitSortSystem.eSortOption.Unit_SummonCost_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByCostDescending);
			case NKCUnitSortSystem.eSortOption.Power_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByPowerAscending);
			case NKCUnitSortSystem.eSortOption.Power_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByPowerDescending);
			case NKCUnitSortSystem.eSortOption.UID_Last:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByUIDDescending);
			case NKCUnitSortSystem.eSortOption.ID_First:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByIDAscending);
			case NKCUnitSortSystem.eSortOption.ID_Last:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByIDDescending);
			case NKCUnitSortSystem.eSortOption.IDX_First:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByIdxAscending);
			case NKCUnitSortSystem.eSortOption.IDX_Last:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByIdxDescending);
			case NKCUnitSortSystem.eSortOption.Attack_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByAttackAscending);
			case NKCUnitSortSystem.eSortOption.Attack_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByAttackDescending);
			case NKCUnitSortSystem.eSortOption.Health_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByHealthAscending);
			case NKCUnitSortSystem.eSortOption.Health_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByHealthDescending);
			case NKCUnitSortSystem.eSortOption.Unit_Defense_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByDefenseAscending);
			case NKCUnitSortSystem.eSortOption.Unit_Defense_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByDefenseDescending);
			case NKCUnitSortSystem.eSortOption.Unit_Crit_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByCriticalAscending);
			case NKCUnitSortSystem.eSortOption.Unit_Crit_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByCriticalDescending);
			case NKCUnitSortSystem.eSortOption.Unit_Hit_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByHitAscending);
			case NKCUnitSortSystem.eSortOption.Unit_Hit_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByHitDescending);
			case NKCUnitSortSystem.eSortOption.Unit_Evade_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByEvadeAscending);
			case NKCUnitSortSystem.eSortOption.Unit_Evade_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByEvadeDescending);
			case NKCUnitSortSystem.eSortOption.Unit_ReduceSkillCool_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByReduceSkillAscending);
			case NKCUnitSortSystem.eSortOption.Unit_ReduceSkillCool_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByReduceSkillDescending);
			case NKCUnitSortSystem.eSortOption.Decked_First:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByDeckedFirst);
			case NKCUnitSortSystem.eSortOption.Decked_Last:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByDeckedLast);
			case NKCUnitSortSystem.eSortOption.ScoutProgress_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByScoutProgressDescending);
			case NKCUnitSortSystem.eSortOption.ScoutProgress_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByScoutProgressAscending);
			case NKCUnitSortSystem.eSortOption.LimitBreak_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByLimitBreakUnitDescending);
			case NKCUnitSortSystem.eSortOption.LimitBreak_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByLimitBreakUnitAscending);
			case NKCUnitSortSystem.eSortOption.Transcendence_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByTranscendenceDescending);
			case NKCUnitSortSystem.eSortOption.Transcendence_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByTranscendenceAscending);
			case NKCUnitSortSystem.eSortOption.Unit_Loyalty_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByLoyaltyDescending);
			case NKCUnitSortSystem.eSortOption.Unit_Loyalty_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByLoyaltyAscending);
			case NKCUnitSortSystem.eSortOption.Squad_Dungeon_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareBySquadDungeonDescending);
			case NKCUnitSortSystem.eSortOption.Squad_Dungeon_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareBySquadDungeonAscending);
			case NKCUnitSortSystem.eSortOption.Squad_Gauntlet_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareBySquadPvPDescending);
			case NKCUnitSortSystem.eSortOption.Squad_Gauntlet_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareBySquadPvPAscending);
			case NKCUnitSortSystem.eSortOption.Favorite_First:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByFavoriteFirst);
			case NKCUnitSortSystem.eSortOption.Favorite_Last:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByFavoriteLast);
			case NKCUnitSortSystem.eSortOption.TacticUpdatePossible_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByTacticUpdatePossibleDescending);
			case NKCUnitSortSystem.eSortOption.TacticUpdatePossible_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByTacticUpdatePossibleAscending);
			case NKCUnitSortSystem.eSortOption.TacticUpdateLevel_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByTacticUpdateLevelDescending);
			case NKCUnitSortSystem.eSortOption.TacticUpdateLevel_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByTacticUpdateLevelAscending);
			case NKCUnitSortSystem.eSortOption.CustomAscend1:
			case NKCUnitSortSystem.eSortOption.CustomAscend2:
			case NKCUnitSortSystem.eSortOption.CustomAscend3:
				if (this.m_Options.lstCustomSortFunc.ContainsKey(NKCUnitSortSystem.GetSortCategoryFromOption(sortOption)))
				{
					return (NKMUnitData a, NKMUnitData b) => this.m_Options.lstCustomSortFunc[NKCUnitSortSystem.GetSortCategoryFromOption(sortOption)].Value(b, a);
				}
				return null;
			case NKCUnitSortSystem.eSortOption.CustomDescend1:
			case NKCUnitSortSystem.eSortOption.CustomDescend2:
			case NKCUnitSortSystem.eSortOption.CustomDescend3:
				if (this.m_Options.lstCustomSortFunc.ContainsKey(NKCUnitSortSystem.GetSortCategoryFromOption(sortOption)))
				{
					return this.m_Options.lstCustomSortFunc[NKCUnitSortSystem.GetSortCategoryFromOption(sortOption)].Value;
				}
				return null;
			}
			return new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.CompareByUIDAscending);
		}

		// Token: 0x06003DEA RID: 15850 RVA: 0x0014054C File Offset: 0x0013E74C
		private int CompareByDeckedFirst(NKMUnitData lhs, NKMUnitData rhs)
		{
			NKMDeckIndex deckIndexCache = this.GetDeckIndexCache(lhs.m_UnitUID, !this.m_Options.bUseDeckedState);
			NKMDeckIndex deckIndexCache2 = this.GetDeckIndexCache(rhs.m_UnitUID, !this.m_Options.bUseDeckedState);
			if (deckIndexCache.m_eDeckType != NKM_DECK_TYPE.NDT_NONE && deckIndexCache2.m_eDeckType != NKM_DECK_TYPE.NDT_NONE)
			{
				return deckIndexCache.m_iIndex.CompareTo(deckIndexCache2.m_iIndex);
			}
			return deckIndexCache2.m_eDeckType.CompareTo(deckIndexCache.m_eDeckType);
		}

		// Token: 0x06003DEB RID: 15851 RVA: 0x001405D0 File Offset: 0x0013E7D0
		private int CompareByDeckedLast(NKMUnitData lhs, NKMUnitData rhs)
		{
			NKMDeckIndex deckIndexCache = this.GetDeckIndexCache(lhs.m_UnitUID, !this.m_Options.bUseDeckedState);
			NKMDeckIndex deckIndexCache2 = this.GetDeckIndexCache(rhs.m_UnitUID, !this.m_Options.bUseDeckedState);
			if (deckIndexCache.m_eDeckType != NKM_DECK_TYPE.NDT_NONE && deckIndexCache2.m_eDeckType != NKM_DECK_TYPE.NDT_NONE)
			{
				return deckIndexCache.m_iIndex.CompareTo(deckIndexCache2.m_iIndex);
			}
			return deckIndexCache.m_eDeckType.CompareTo(deckIndexCache2.m_eDeckType);
		}

		// Token: 0x06003DEC RID: 15852 RVA: 0x00140654 File Offset: 0x0013E854
		private int CompareByState(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.IsUnitSelectable(rhs).CompareTo(this.IsUnitSelectable(lhs));
		}

		// Token: 0x06003DED RID: 15853 RVA: 0x00140678 File Offset: 0x0013E878
		public static int CompareByLevelAscending(NKMUnitData lhs, NKMUnitData rhs)
		{
			if (lhs.m_UnitLevel != rhs.m_UnitLevel)
			{
				return lhs.m_UnitLevel.CompareTo(rhs.m_UnitLevel);
			}
			int num = lhs.m_iUnitLevelEXP.CompareTo(rhs.m_iUnitLevelEXP);
			if (num != 0)
			{
				return num;
			}
			return lhs.m_LimitBreakLevel.CompareTo(rhs.m_LimitBreakLevel);
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x001406D0 File Offset: 0x0013E8D0
		public static int CompareByLevelDescending(NKMUnitData lhs, NKMUnitData rhs)
		{
			if (lhs.m_UnitLevel != rhs.m_UnitLevel)
			{
				return rhs.m_UnitLevel.CompareTo(lhs.m_UnitLevel);
			}
			int num = rhs.m_iUnitLevelEXP.CompareTo(lhs.m_iUnitLevelEXP);
			if (num != 0)
			{
				return num;
			}
			return rhs.m_LimitBreakLevel.CompareTo(lhs.m_LimitBreakLevel);
		}

		// Token: 0x06003DEF RID: 15855 RVA: 0x00140728 File Offset: 0x0013E928
		public static int CompareByRarityAscending(NKMUnitData lhs, NKMUnitData rhs)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(lhs.m_UnitID);
			NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(rhs.m_UnitID);
			if (unitTempletBase.m_NKM_UNIT_GRADE != unitTempletBase2.m_NKM_UNIT_GRADE)
			{
				return unitTempletBase.m_NKM_UNIT_GRADE.CompareTo(unitTempletBase2.m_NKM_UNIT_GRADE);
			}
			if (unitTempletBase.m_bAwaken != unitTempletBase2.m_bAwaken)
			{
				return unitTempletBase.m_bAwaken.CompareTo(unitTempletBase2.m_bAwaken);
			}
			return unitTempletBase.IsRearmUnit.CompareTo(unitTempletBase2.IsRearmUnit);
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x001407AC File Offset: 0x0013E9AC
		public static int CompareByRarityDescending(NKMUnitData lhs, NKMUnitData rhs)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(lhs.m_UnitID);
			NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(rhs.m_UnitID);
			if (unitTempletBase.m_NKM_UNIT_GRADE != unitTempletBase2.m_NKM_UNIT_GRADE)
			{
				return unitTempletBase2.m_NKM_UNIT_GRADE.CompareTo(unitTempletBase.m_NKM_UNIT_GRADE);
			}
			if (unitTempletBase.m_bAwaken != unitTempletBase2.m_bAwaken)
			{
				return unitTempletBase2.m_bAwaken.CompareTo(unitTempletBase.m_bAwaken);
			}
			return unitTempletBase2.IsRearmUnit.CompareTo(unitTempletBase.IsRearmUnit);
		}

		// Token: 0x06003DF1 RID: 15857 RVA: 0x00140830 File Offset: 0x0013EA30
		private int CompareByCostAscending(NKMUnitData lhs, NKMUnitData rhs)
		{
			NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(lhs.m_UnitID);
			NKMUnitStatTemplet unitStatTemplet2 = NKMUnitManager.GetUnitStatTemplet(rhs.m_UnitID);
			if (unitStatTemplet == null || unitStatTemplet2 == null)
			{
				return -1;
			}
			int respawnCost;
			if (this.m_bEnableShowBan && NKCBanManager.IsBanUnit(unitStatTemplet.m_UnitID, NKCBanManager.BAN_DATA_TYPE.FINAL))
			{
				respawnCost = unitStatTemplet.GetRespawnCost(true, false, NKCBanManager.GetBanData(NKCBanManager.BAN_DATA_TYPE.FINAL), null);
			}
			else if (this.m_bEnableShowUpUnit && NKCBanManager.IsUpUnit(unitStatTemplet.m_UnitID))
			{
				respawnCost = unitStatTemplet.GetRespawnCost(true, false, null, NKCBanManager.m_dicNKMUpData);
			}
			else
			{
				respawnCost = unitStatTemplet.GetRespawnCost(false, null, null);
			}
			int respawnCost2;
			if (this.m_bEnableShowBan && NKCBanManager.IsBanUnit(unitStatTemplet2.m_UnitID, NKCBanManager.BAN_DATA_TYPE.FINAL))
			{
				respawnCost2 = unitStatTemplet2.GetRespawnCost(true, false, NKCBanManager.GetBanData(NKCBanManager.BAN_DATA_TYPE.FINAL), null);
			}
			else if (this.m_bEnableShowUpUnit && NKCBanManager.IsUpUnit(unitStatTemplet2.m_UnitID))
			{
				respawnCost2 = unitStatTemplet2.GetRespawnCost(true, false, null, NKCBanManager.m_dicNKMUpData);
			}
			else
			{
				respawnCost2 = unitStatTemplet2.GetRespawnCost(false, null, null);
			}
			return respawnCost.CompareTo(respawnCost2);
		}

		// Token: 0x06003DF2 RID: 15858 RVA: 0x0014091C File Offset: 0x0013EB1C
		private int CompareByCostDescending(NKMUnitData lhs, NKMUnitData rhs)
		{
			NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(lhs.m_UnitID);
			NKMUnitStatTemplet unitStatTemplet2 = NKMUnitManager.GetUnitStatTemplet(rhs.m_UnitID);
			if (unitStatTemplet == null || unitStatTemplet2 == null)
			{
				return -1;
			}
			int respawnCost;
			if (this.m_bEnableShowBan && NKCBanManager.IsBanUnit(unitStatTemplet.m_UnitID, NKCBanManager.BAN_DATA_TYPE.FINAL))
			{
				respawnCost = unitStatTemplet.GetRespawnCost(true, false, NKCBanManager.GetBanData(NKCBanManager.BAN_DATA_TYPE.FINAL), null);
			}
			else if (this.m_bEnableShowUpUnit && NKCBanManager.IsUpUnit(unitStatTemplet.m_UnitID))
			{
				respawnCost = unitStatTemplet.GetRespawnCost(true, false, null, NKCBanManager.m_dicNKMUpData);
			}
			else
			{
				respawnCost = unitStatTemplet.GetRespawnCost(false, null, null);
			}
			int respawnCost2;
			if (this.m_bEnableShowBan && NKCBanManager.IsBanUnit(unitStatTemplet2.m_UnitID, NKCBanManager.BAN_DATA_TYPE.FINAL))
			{
				respawnCost2 = unitStatTemplet2.GetRespawnCost(true, false, NKCBanManager.GetBanData(NKCBanManager.BAN_DATA_TYPE.FINAL), null);
			}
			else if (this.m_bEnableShowUpUnit && NKCBanManager.IsUpUnit(unitStatTemplet2.m_UnitID))
			{
				respawnCost2 = unitStatTemplet2.GetRespawnCost(true, false, null, NKCBanManager.m_dicNKMUpData);
			}
			else
			{
				respawnCost2 = unitStatTemplet2.GetRespawnCost(false, null, null);
			}
			return respawnCost2.CompareTo(respawnCost);
		}

		// Token: 0x06003DF3 RID: 15859 RVA: 0x00140A08 File Offset: 0x0013EC08
		private int CompareByPowerAscending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.GetUnitPowerCache(lhs.m_UnitUID).CompareTo(this.GetUnitPowerCache(rhs.m_UnitUID));
		}

		// Token: 0x06003DF4 RID: 15860 RVA: 0x00140A38 File Offset: 0x0013EC38
		private int CompareByPowerDescending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.GetUnitPowerCache(rhs.m_UnitUID).CompareTo(this.GetUnitPowerCache(lhs.m_UnitUID));
		}

		// Token: 0x06003DF5 RID: 15861 RVA: 0x00140A65 File Offset: 0x0013EC65
		private int CompareByUIDAscending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return lhs.m_UnitUID.CompareTo(rhs.m_UnitUID);
		}

		// Token: 0x06003DF6 RID: 15862 RVA: 0x00140A78 File Offset: 0x0013EC78
		private int CompareByUIDDescending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return rhs.m_UnitUID.CompareTo(lhs.m_UnitUID);
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x00140A8B File Offset: 0x0013EC8B
		private int CompareByIDAscending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return lhs.m_UnitID.CompareTo(rhs.m_UnitID);
		}

		// Token: 0x06003DF8 RID: 15864 RVA: 0x00140A9E File Offset: 0x0013EC9E
		private int CompareByIDDescending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return rhs.m_UnitID.CompareTo(lhs.m_UnitID);
		}

		// Token: 0x06003DF9 RID: 15865 RVA: 0x00140AB4 File Offset: 0x0013ECB4
		private int CompareByIdxAscending(NKMUnitData lhs, NKMUnitData rhs)
		{
			NKCCollectionUnitTemplet unitTemplet = NKCCollectionManager.GetUnitTemplet(lhs.m_UnitID);
			NKCCollectionUnitTemplet unitTemplet2 = NKCCollectionManager.GetUnitTemplet(rhs.m_UnitID);
			if (unitTemplet != null && unitTemplet2 != null)
			{
				return unitTemplet.Idx.CompareTo(unitTemplet2.Idx);
			}
			if (unitTemplet == null && unitTemplet2 != null)
			{
				return 1;
			}
			if (unitTemplet != null && unitTemplet2 == null)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x00140B04 File Offset: 0x0013ED04
		private int CompareByIdxDescending(NKMUnitData lhs, NKMUnitData rhs)
		{
			NKCCollectionUnitTemplet unitTemplet = NKCCollectionManager.GetUnitTemplet(lhs.m_UnitID);
			NKCCollectionUnitTemplet unitTemplet2 = NKCCollectionManager.GetUnitTemplet(rhs.m_UnitID);
			if (unitTemplet != null && unitTemplet2 != null)
			{
				return unitTemplet2.Idx.CompareTo(unitTemplet.Idx);
			}
			if (unitTemplet == null && unitTemplet2 != null)
			{
				return 1;
			}
			if (unitTemplet != null && unitTemplet2 == null)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x00140B54 File Offset: 0x0013ED54
		private int CompareByAttackAscending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.GetUnitAttackCache(lhs.m_UnitUID).CompareTo(this.GetUnitAttackCache(rhs.m_UnitUID));
		}

		// Token: 0x06003DFC RID: 15868 RVA: 0x00140B84 File Offset: 0x0013ED84
		private int CompareByAttackDescending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.GetUnitAttackCache(rhs.m_UnitUID).CompareTo(this.GetUnitAttackCache(lhs.m_UnitUID));
		}

		// Token: 0x06003DFD RID: 15869 RVA: 0x00140BB4 File Offset: 0x0013EDB4
		private int CompareByHealthAscending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.GetUnitHPCache(lhs.m_UnitUID).CompareTo(this.GetUnitHPCache(rhs.m_UnitUID));
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x00140BE4 File Offset: 0x0013EDE4
		private int CompareByHealthDescending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.GetUnitHPCache(rhs.m_UnitUID).CompareTo(this.GetUnitHPCache(lhs.m_UnitUID));
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x00140C14 File Offset: 0x0013EE14
		private int CompareByDefenseAscending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.GetUnitDefCache(lhs.m_UnitUID).CompareTo(this.GetUnitDefCache(rhs.m_UnitUID));
		}

		// Token: 0x06003E00 RID: 15872 RVA: 0x00140C44 File Offset: 0x0013EE44
		private int CompareByDefenseDescending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.GetUnitDefCache(rhs.m_UnitUID).CompareTo(this.GetUnitDefCache(lhs.m_UnitUID));
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x00140C74 File Offset: 0x0013EE74
		private int CompareByCriticalAscending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.GetUnitCritCache(lhs.m_UnitUID).CompareTo(this.GetUnitCritCache(rhs.m_UnitUID));
		}

		// Token: 0x06003E02 RID: 15874 RVA: 0x00140CA4 File Offset: 0x0013EEA4
		private int CompareByCriticalDescending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.GetUnitCritCache(rhs.m_UnitUID).CompareTo(this.GetUnitCritCache(lhs.m_UnitUID));
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x00140CD4 File Offset: 0x0013EED4
		private int CompareByHitAscending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.GetUnitHitCache(lhs.m_UnitUID).CompareTo(this.GetUnitHitCache(rhs.m_UnitUID));
		}

		// Token: 0x06003E04 RID: 15876 RVA: 0x00140D04 File Offset: 0x0013EF04
		private int CompareByHitDescending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.GetUnitHitCache(rhs.m_UnitUID).CompareTo(this.GetUnitHitCache(lhs.m_UnitUID));
		}

		// Token: 0x06003E05 RID: 15877 RVA: 0x00140D34 File Offset: 0x0013EF34
		private int CompareByEvadeAscending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.GetUnitEvadeCache(lhs.m_UnitUID).CompareTo(this.GetUnitEvadeCache(rhs.m_UnitUID));
		}

		// Token: 0x06003E06 RID: 15878 RVA: 0x00140D64 File Offset: 0x0013EF64
		private int CompareByEvadeDescending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.GetUnitEvadeCache(rhs.m_UnitUID).CompareTo(this.GetUnitEvadeCache(lhs.m_UnitUID));
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x00140D94 File Offset: 0x0013EF94
		private int CompareByReduceSkillAscending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.GetUnitSkillCoolCache(lhs.m_UnitUID).CompareTo(this.GetUnitSkillCoolCache(rhs.m_UnitUID));
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x00140DC4 File Offset: 0x0013EFC4
		private int CompareByReduceSkillDescending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.GetUnitSkillCoolCache(rhs.m_UnitUID).CompareTo(this.GetUnitSkillCoolCache(lhs.m_UnitUID));
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x00140DF4 File Offset: 0x0013EFF4
		private int CompareByScoutProgressDescending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.GetScoutProgressCache(rhs.m_UnitUID).CompareTo(this.GetScoutProgressCache(lhs.m_UnitUID));
		}

		// Token: 0x06003E0A RID: 15882 RVA: 0x00140E24 File Offset: 0x0013F024
		private int CompareByScoutProgressAscending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.GetScoutProgressCache(lhs.m_UnitUID).CompareTo(this.GetScoutProgressCache(rhs.m_UnitUID));
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x00140E54 File Offset: 0x0013F054
		private int CompareByLimitBreakUnitDescending(NKMUnitData lhs, NKMUnitData rhs)
		{
			int num = this.CompareLBStatus(lhs, rhs);
			if (num != 0)
			{
				return num;
			}
			return this.GetLimitBreakCache(lhs.m_UnitUID).CompareTo(this.GetLimitBreakCache(rhs.m_UnitUID));
		}

		// Token: 0x06003E0C RID: 15884 RVA: 0x00140E90 File Offset: 0x0013F090
		private int CompareByLimitBreakUnitAscending(NKMUnitData lhs, NKMUnitData rhs)
		{
			int num = this.CompareLBStatus(lhs, rhs);
			if (num != 0)
			{
				return num;
			}
			return this.GetLimitBreakCache(rhs.m_UnitUID).CompareTo(this.GetLimitBreakCache(lhs.m_UnitUID));
		}

		// Token: 0x06003E0D RID: 15885 RVA: 0x00140ECC File Offset: 0x0013F0CC
		private int GetLBStatus(NKMUnitData unitData)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData);
			if (unitTempletBase != null && unitTempletBase.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_TRAINER)
			{
				return 100;
			}
			if (this.GetLimitBreakCache(unitData.m_UnitUID) == -1)
			{
				return 5;
			}
			if (unitData.m_LimitBreakLevel < 3)
			{
				return 0;
			}
			if (unitData.m_LimitBreakLevel < 8)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x00140F18 File Offset: 0x0013F118
		private int CompareLBStatus(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.GetLBStatus(lhs).CompareTo(this.GetLBStatus(rhs));
		}

		// Token: 0x06003E0F RID: 15887 RVA: 0x00140F3C File Offset: 0x0013F13C
		private int CompareByTranscendenceDescending(NKMUnitData lhs, NKMUnitData rhs)
		{
			int num = this.CompareTrStatus(lhs, rhs);
			if (num != 0)
			{
				return num;
			}
			return this.GetLimitBreakCache(lhs.m_UnitUID).CompareTo(this.GetLimitBreakCache(rhs.m_UnitUID));
		}

		// Token: 0x06003E10 RID: 15888 RVA: 0x00140F78 File Offset: 0x0013F178
		private int CompareByTranscendenceAscending(NKMUnitData lhs, NKMUnitData rhs)
		{
			int num = this.CompareTrStatus(lhs, rhs);
			if (num != 0)
			{
				return num;
			}
			return this.GetLimitBreakCache(rhs.m_UnitUID).CompareTo(this.GetLimitBreakCache(lhs.m_UnitUID));
		}

		// Token: 0x06003E11 RID: 15889 RVA: 0x00140FB4 File Offset: 0x0013F1B4
		private int GetTranscendenceStatus(NKMUnitData unitData)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData);
			if (unitTempletBase != null && unitTempletBase.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_TRAINER)
			{
				return 100;
			}
			if (this.GetLimitBreakCache(unitData.m_UnitUID) == -1)
			{
				return 5;
			}
			if (unitData.m_LimitBreakLevel < 3)
			{
				return 1;
			}
			if (unitData.m_LimitBreakLevel < 8)
			{
				return 0;
			}
			return 2;
		}

		// Token: 0x06003E12 RID: 15890 RVA: 0x00141000 File Offset: 0x0013F200
		private int CompareTrStatus(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.GetTranscendenceStatus(lhs).CompareTo(this.GetTranscendenceStatus(rhs));
		}

		// Token: 0x06003E13 RID: 15891 RVA: 0x00141024 File Offset: 0x0013F224
		private int CompareByLoyaltyAscending(NKMUnitData lhs, NKMUnitData rhs)
		{
			if (this.GetLoyaltyCache(lhs.m_UnitUID) == this.GetLoyaltyCache(rhs.m_UnitUID))
			{
				return rhs.IsPermanentContract.CompareTo(lhs.IsPermanentContract);
			}
			return this.GetLoyaltyCache(lhs.m_UnitUID).CompareTo(this.GetLoyaltyCache(rhs.m_UnitUID));
		}

		// Token: 0x06003E14 RID: 15892 RVA: 0x00141080 File Offset: 0x0013F280
		private int CompareByLoyaltyDescending(NKMUnitData lhs, NKMUnitData rhs)
		{
			if (this.GetLoyaltyCache(lhs.m_UnitUID) == this.GetLoyaltyCache(rhs.m_UnitUID))
			{
				return rhs.IsPermanentContract.CompareTo(lhs.IsPermanentContract);
			}
			return this.GetLoyaltyCache(rhs.m_UnitUID).CompareTo(this.GetLoyaltyCache(lhs.m_UnitUID));
		}

		// Token: 0x06003E15 RID: 15893 RVA: 0x001410DC File Offset: 0x0013F2DC
		private int CompareByDeckTypeIndexAscending(NKMUnitData lhs, NKMUnitData rhs, NKM_DECK_TYPE deckType)
		{
			NKMDeckIndex deckIndexCache = this.GetDeckIndexCache(lhs.m_UnitUID, deckType);
			NKMDeckIndex deckIndexCache2 = this.GetDeckIndexCache(rhs.m_UnitUID, deckType);
			if (deckIndexCache.m_eDeckType == deckIndexCache2.m_eDeckType)
			{
				if (deckIndexCache.m_eDeckType == NKM_DECK_TYPE.NDT_NONE)
				{
					return 0;
				}
				int num = deckIndexCache.m_iIndex.CompareTo(deckIndexCache2.m_iIndex);
				if (num != 0)
				{
					return num;
				}
				NKMDeckData deckData = NKCScenManager.CurrentUserData().m_ArmyData.GetDeckData(deckIndexCache);
				if (deckData == null)
				{
					return 0;
				}
				int num2;
				deckData.HasUnitUid(lhs.m_UnitUID, out num2);
				int value;
				deckData.HasUnitUid(rhs.m_UnitUID, out value);
				return num2.CompareTo(value);
			}
			else
			{
				if (deckIndexCache.m_eDeckType == deckType)
				{
					return -1;
				}
				if (deckIndexCache2.m_eDeckType == deckType)
				{
					return 1;
				}
				if (deckIndexCache.m_eDeckType == NKM_DECK_TYPE.NDT_NONE)
				{
					return 1;
				}
				if (deckIndexCache2.m_eDeckType == NKM_DECK_TYPE.NDT_NONE)
				{
					return -1;
				}
				return deckIndexCache.m_eDeckType.CompareTo(deckIndexCache2.m_eDeckType);
			}
		}

		// Token: 0x06003E16 RID: 15894 RVA: 0x001411BC File Offset: 0x0013F3BC
		private int CompareByDeckTypeIndexDescending(NKMUnitData lhs, NKMUnitData rhs, NKM_DECK_TYPE deckType)
		{
			NKMDeckIndex deckIndexCache = this.GetDeckIndexCache(lhs.m_UnitUID, deckType);
			NKMDeckIndex deckIndexCache2 = this.GetDeckIndexCache(rhs.m_UnitUID, deckType);
			if (deckIndexCache.m_eDeckType == deckIndexCache2.m_eDeckType)
			{
				if (deckIndexCache.m_eDeckType == NKM_DECK_TYPE.NDT_NONE)
				{
					return 0;
				}
				int num = deckIndexCache2.m_iIndex.CompareTo(deckIndexCache.m_iIndex);
				if (num != 0)
				{
					return num;
				}
				NKMDeckData deckData = NKCScenManager.CurrentUserData().m_ArmyData.GetDeckData(deckIndexCache);
				if (deckData == null)
				{
					return 0;
				}
				int value;
				deckData.HasUnitUid(lhs.m_UnitUID, out value);
				int num2;
				deckData.HasUnitUid(rhs.m_UnitUID, out num2);
				return num2.CompareTo(value);
			}
			else
			{
				if (deckIndexCache.m_eDeckType == deckType)
				{
					return -1;
				}
				if (deckIndexCache2.m_eDeckType == deckType)
				{
					return 1;
				}
				if (deckIndexCache.m_eDeckType == NKM_DECK_TYPE.NDT_NONE)
				{
					return 1;
				}
				if (deckIndexCache2.m_eDeckType == NKM_DECK_TYPE.NDT_NONE)
				{
					return -1;
				}
				return deckIndexCache.m_eDeckType.CompareTo(deckIndexCache2.m_eDeckType);
			}
		}

		// Token: 0x06003E17 RID: 15895 RVA: 0x0014129A File Offset: 0x0013F49A
		private int CompareBySquadDungeonDescending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.CompareByDeckTypeIndexDescending(lhs, rhs, NKM_DECK_TYPE.NDT_DAILY);
		}

		// Token: 0x06003E18 RID: 15896 RVA: 0x001412A5 File Offset: 0x0013F4A5
		private int CompareBySquadDungeonAscending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.CompareByDeckTypeIndexAscending(lhs, rhs, NKM_DECK_TYPE.NDT_DAILY);
		}

		// Token: 0x06003E19 RID: 15897 RVA: 0x001412B0 File Offset: 0x0013F4B0
		private int CompareBySquadWarfareDescending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.CompareByDeckTypeIndexDescending(lhs, rhs, NKM_DECK_TYPE.NDT_NORMAL);
		}

		// Token: 0x06003E1A RID: 15898 RVA: 0x001412BB File Offset: 0x0013F4BB
		private int CompareBySquadWarfareAscending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.CompareByDeckTypeIndexAscending(lhs, rhs, NKM_DECK_TYPE.NDT_NORMAL);
		}

		// Token: 0x06003E1B RID: 15899 RVA: 0x001412C8 File Offset: 0x0013F4C8
		private int CompareBySquadPvPDescending(NKMUnitData lhs, NKMUnitData rhs)
		{
			NKMDeckIndex deckIndexCache = this.GetDeckIndexCache(lhs.m_UnitUID, NKM_DECK_TYPE.NDT_PVP);
			NKMDeckIndex deckIndexCache2 = this.GetDeckIndexCache(rhs.m_UnitUID, NKM_DECK_TYPE.NDT_PVP);
			if (deckIndexCache.m_eDeckType == NKM_DECK_TYPE.NDT_PVP && deckIndexCache2.m_eDeckType == NKM_DECK_TYPE.NDT_PVP)
			{
				int num = deckIndexCache2.m_iIndex.CompareTo(deckIndexCache.m_iIndex);
				if (num != 0)
				{
					return num;
				}
				NKMDeckData deckData = NKCScenManager.CurrentUserData().m_ArmyData.GetDeckData(deckIndexCache);
				if (deckData == null)
				{
					return 0;
				}
				int value;
				deckData.HasUnitUid(lhs.m_UnitUID, out value);
				int num2;
				deckData.HasUnitUid(rhs.m_UnitUID, out num2);
				return num2.CompareTo(value);
			}
			else
			{
				if (deckIndexCache.m_eDeckType == NKM_DECK_TYPE.NDT_PVP)
				{
					return -1;
				}
				if (deckIndexCache2.m_eDeckType == NKM_DECK_TYPE.NDT_PVP)
				{
					return 1;
				}
				return this.CompareByDeckTypeIndexDescending(lhs, rhs, NKM_DECK_TYPE.NDT_PVP_DEFENCE);
			}
		}

		// Token: 0x06003E1C RID: 15900 RVA: 0x00141378 File Offset: 0x0013F578
		private int CompareBySquadPvPAscending(NKMUnitData lhs, NKMUnitData rhs)
		{
			NKMDeckIndex deckIndexCache = this.GetDeckIndexCache(lhs.m_UnitUID, NKM_DECK_TYPE.NDT_PVP);
			NKMDeckIndex deckIndexCache2 = this.GetDeckIndexCache(rhs.m_UnitUID, NKM_DECK_TYPE.NDT_PVP);
			if (deckIndexCache.m_eDeckType == NKM_DECK_TYPE.NDT_PVP && deckIndexCache2.m_eDeckType == NKM_DECK_TYPE.NDT_PVP)
			{
				int num = deckIndexCache.m_iIndex.CompareTo(deckIndexCache2.m_iIndex);
				if (num != 0)
				{
					return num;
				}
				NKMDeckData deckData = NKCScenManager.CurrentUserData().m_ArmyData.GetDeckData(deckIndexCache);
				if (deckData == null)
				{
					return 0;
				}
				int num2;
				deckData.HasUnitUid(lhs.m_UnitUID, out num2);
				int value;
				deckData.HasUnitUid(rhs.m_UnitUID, out value);
				return num2.CompareTo(value);
			}
			else
			{
				if (deckIndexCache.m_eDeckType == NKM_DECK_TYPE.NDT_PVP)
				{
					return -1;
				}
				if (deckIndexCache2.m_eDeckType == NKM_DECK_TYPE.NDT_PVP)
				{
					return 1;
				}
				return this.CompareByDeckTypeIndexAscending(lhs, rhs, NKM_DECK_TYPE.NDT_PVP_DEFENCE);
			}
		}

		// Token: 0x06003E1D RID: 15901 RVA: 0x00141428 File Offset: 0x0013F628
		private int CompareByFavoriteFirst(NKMUnitData lhs, NKMUnitData rhs)
		{
			return rhs.isFavorite.CompareTo(lhs.isFavorite);
		}

		// Token: 0x06003E1E RID: 15902 RVA: 0x0014143B File Offset: 0x0013F63B
		private int CompareByFavoriteLast(NKMUnitData lhs, NKMUnitData rhs)
		{
			return lhs.isFavorite.CompareTo(rhs.isFavorite);
		}

		// Token: 0x06003E1F RID: 15903 RVA: 0x00141450 File Offset: 0x0013F650
		public static int CompareByRoleAscending(NKMUnitData lhs, NKMUnitData rhs)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(lhs.m_UnitID);
			NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(rhs.m_UnitID);
			return unitTempletBase.m_NKM_UNIT_ROLE_TYPE.CompareTo(unitTempletBase2.m_NKM_UNIT_ROLE_TYPE);
		}

		// Token: 0x06003E20 RID: 15904 RVA: 0x00141490 File Offset: 0x0013F690
		public static int CompareByRoleDescending(NKMUnitData lhs, NKMUnitData rhs)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(lhs.m_UnitID);
			return NKMUnitManager.GetUnitTempletBase(rhs.m_UnitID).m_NKM_UNIT_ROLE_TYPE.CompareTo(unitTempletBase.m_NKM_UNIT_ROLE_TYPE);
		}

		// Token: 0x06003E21 RID: 15905 RVA: 0x001414D0 File Offset: 0x0013F6D0
		private int CompareByTacticUpdatePossibleDescending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.GetTacticUpdateCache(lhs.m_UnitUID).CompareTo(this.GetTacticUpdateCache(rhs.m_UnitUID));
		}

		// Token: 0x06003E22 RID: 15906 RVA: 0x00141500 File Offset: 0x0013F700
		private int CompareByTacticUpdatePossibleAscending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return this.GetTacticUpdateCache(rhs.m_UnitUID).CompareTo(this.GetTacticUpdateCache(lhs.m_UnitUID));
		}

		// Token: 0x06003E23 RID: 15907 RVA: 0x0014152D File Offset: 0x0013F72D
		private int CompareByTacticUpdateLevelDescending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return rhs.tacticLevel.CompareTo(lhs.tacticLevel);
		}

		// Token: 0x06003E24 RID: 15908 RVA: 0x00141540 File Offset: 0x0013F740
		private int CompareByTacticUpdateLevelAscending(NKMUnitData lhs, NKMUnitData rhs)
		{
			return lhs.tacticLevel.CompareTo(rhs.tacticLevel);
		}

		// Token: 0x06003E25 RID: 15909 RVA: 0x00141553 File Offset: 0x0013F753
		public static string GetFilterName(NKM_UNIT_STYLE_TYPE type)
		{
			if (type == NKM_UNIT_STYLE_TYPE.NUST_INVALID)
			{
				return NKCUtilString.GET_STRING_FILTER_ALL;
			}
			return NKCUtilString.GetUnitStyleName(type);
		}

		// Token: 0x06003E26 RID: 15910 RVA: 0x00141564 File Offset: 0x0013F764
		public static string GetSortName(NKCUnitSortSystem.eSortOption option)
		{
			switch (option)
			{
			default:
				return NKCUtilString.GET_STRING_SORT_LEVEL;
			case NKCUnitSortSystem.eSortOption.Rarity_Low:
			case NKCUnitSortSystem.eSortOption.Rarity_High:
				return NKCUtilString.GET_STRING_SORT_RARITY;
			case NKCUnitSortSystem.eSortOption.Unit_SummonCost_Low:
			case NKCUnitSortSystem.eSortOption.Unit_SummonCost_High:
				return NKCUtilString.GET_STRING_SORT_COST;
			case NKCUnitSortSystem.eSortOption.Power_Low:
			case NKCUnitSortSystem.eSortOption.Power_High:
				return NKCUtilString.GET_STRING_SORT_POPWER;
			case NKCUnitSortSystem.eSortOption.UID_First:
			case NKCUnitSortSystem.eSortOption.UID_Last:
				return NKCUtilString.GET_STRING_SORT_UID;
			case NKCUnitSortSystem.eSortOption.ID_First:
			case NKCUnitSortSystem.eSortOption.ID_Last:
			case NKCUnitSortSystem.eSortOption.IDX_First:
			case NKCUnitSortSystem.eSortOption.IDX_Last:
				return NKCUtilString.GET_STRING_SORT_IDX;
			case NKCUnitSortSystem.eSortOption.Attack_Low:
			case NKCUnitSortSystem.eSortOption.Attack_High:
				return NKCUtilString.GET_STRING_SORT_ATTACK;
			case NKCUnitSortSystem.eSortOption.Health_Low:
			case NKCUnitSortSystem.eSortOption.Health_High:
				return NKCUtilString.GET_STRING_SORT_HEALTH;
			case NKCUnitSortSystem.eSortOption.Unit_Defense_Low:
			case NKCUnitSortSystem.eSortOption.Unit_Defense_High:
				return NKCUtilString.GET_STRING_SORT_DEFENSE;
			case NKCUnitSortSystem.eSortOption.Unit_Crit_Low:
			case NKCUnitSortSystem.eSortOption.Unit_Crit_High:
				return NKCUtilString.GET_STRING_SORT_CRIT;
			case NKCUnitSortSystem.eSortOption.Unit_Hit_Low:
			case NKCUnitSortSystem.eSortOption.Unit_Hit_High:
				return NKCUtilString.GET_STRING_SORT_HIT;
			case NKCUnitSortSystem.eSortOption.Unit_Evade_Low:
			case NKCUnitSortSystem.eSortOption.Unit_Evade_High:
				return NKCUtilString.GET_STRING_SORT_EVADE;
			case NKCUnitSortSystem.eSortOption.Unit_ReduceSkillCool_Low:
			case NKCUnitSortSystem.eSortOption.Unit_ReduceSkillCool_High:
				return NKCUtilString.GET_STRING_OPERATOR_SKILL_COOL_REDUCE;
			case NKCUnitSortSystem.eSortOption.Player_Level_Low:
			case NKCUnitSortSystem.eSortOption.Player_Level_High:
				return NKCUtilString.GET_STRING_SORT_PLAYER_LEVEL;
			case NKCUnitSortSystem.eSortOption.LoginTime_Latly:
			case NKCUnitSortSystem.eSortOption.LoginTime_Old:
				return NKCUtilString.GET_STRING_SORT_LOGIN_TIME;
			case NKCUnitSortSystem.eSortOption.ScoutProgress_High:
			case NKCUnitSortSystem.eSortOption.ScoutProgress_Low:
				return NKCUtilString.GET_STRING_SORT_SCOUT_PROGRESS;
			case NKCUnitSortSystem.eSortOption.LimitBreak_High:
			case NKCUnitSortSystem.eSortOption.LimitBreak_Low:
				return NKCUtilString.GET_STRING_SORT_LIMIT_BREAK_PROGRESS;
			case NKCUnitSortSystem.eSortOption.Transcendence_High:
			case NKCUnitSortSystem.eSortOption.Transcendence_Low:
				return NKCUtilString.GET_STRING_SORT_TRANSCENDENCE;
			case NKCUnitSortSystem.eSortOption.Unit_Loyalty_High:
			case NKCUnitSortSystem.eSortOption.Unit_Loyalty_Low:
				return NKCUtilString.GET_STRING_SORT_LOYALTY;
			case NKCUnitSortSystem.eSortOption.Squad_Dungeon_High:
			case NKCUnitSortSystem.eSortOption.Squad_Dungeon_Low:
				return NKCUtilString.GET_STRING_SORT_SQUAD_DUNGEON;
			case NKCUnitSortSystem.eSortOption.Squad_Gauntlet_High:
			case NKCUnitSortSystem.eSortOption.Squad_Gauntlet_Low:
				return NKCUtilString.GET_STRING_SORT_SQUAD_PVP;
			case NKCUnitSortSystem.eSortOption.Guild_Grade_High:
			case NKCUnitSortSystem.eSortOption.Guild_Grade_Low:
				return NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_SORT_LIST_GRADE;
			case NKCUnitSortSystem.eSortOption.Guild_WeeklyPoint_High:
			case NKCUnitSortSystem.eSortOption.Guild_WeeklyPoint_Low:
				return NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_SORT_LIST_SCORE;
			case NKCUnitSortSystem.eSortOption.Guild_TotalPoint_High:
			case NKCUnitSortSystem.eSortOption.Guild_TotalPoint_Low:
				return NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_SORT_LIST_SCORE_ALL;
			case NKCUnitSortSystem.eSortOption.TacticUpdatePossible_High:
			case NKCUnitSortSystem.eSortOption.TacticUpdatePossible_Low:
				return NKCUtilString.GET_STRING_TACTIC_UPDATE_SORT_TITLE;
			case NKCUnitSortSystem.eSortOption.CustomAscend1:
			case NKCUnitSortSystem.eSortOption.CustomDescend1:
			case NKCUnitSortSystem.eSortOption.CustomAscend2:
			case NKCUnitSortSystem.eSortOption.CustomDescend2:
			case NKCUnitSortSystem.eSortOption.CustomAscend3:
			case NKCUnitSortSystem.eSortOption.CustomDescend3:
				return string.Empty;
			}
		}

		// Token: 0x06003E27 RID: 15911 RVA: 0x00141714 File Offset: 0x0013F914
		public NKMUnitData AutoSelect(HashSet<long> setExcludeUnitUid, NKCUnitSortSystem.AutoSelectExtraFilter extrafilter = null)
		{
			List<NKMUnitData> list = this.AutoSelect(setExcludeUnitUid, 1, extrafilter);
			if (list.Count > 0)
			{
				return list[0];
			}
			return null;
		}

		// Token: 0x06003E28 RID: 15912 RVA: 0x00141740 File Offset: 0x0013F940
		public List<NKMUnitData> AutoSelect(HashSet<long> setExcludeUnitUid, int count, NKCUnitSortSystem.AutoSelectExtraFilter extrafilter = null)
		{
			List<NKMUnitData> list = new List<NKMUnitData>();
			int num = 0;
			while (num < this.SortedUnitList.Count && list.Count < count)
			{
				NKMUnitData nkmunitData = this.SortedUnitList[num];
				if (nkmunitData != null && (setExcludeUnitUid == null || !setExcludeUnitUid.Contains(nkmunitData.m_UnitUID)) && (extrafilter == null || extrafilter(nkmunitData)) && this.IsUnitSelectable(nkmunitData))
				{
					list.Add(nkmunitData);
				}
				num++;
			}
			return list;
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x001417B0 File Offset: 0x0013F9B0
		public static NKMUnitData MakeTempUnitData(int unitID, int level = 1, short limitBreakLevel = 0)
		{
			return new NKMUnitData
			{
				m_UnitID = unitID,
				m_UnitUID = (long)unitID,
				m_UserUID = 0L,
				m_UnitLevel = level,
				m_iUnitLevelEXP = 0,
				m_LimitBreakLevel = limitBreakLevel,
				m_bLock = false
			};
		}

		// Token: 0x06003E2A RID: 15914 RVA: 0x001417EC File Offset: 0x0013F9EC
		public static NKMUnitData MakeTempUnitData(NKMSkinTemplet skinTemplet, int level = 1, short limitBreakLevel = 0)
		{
			return new NKMUnitData
			{
				m_UnitID = skinTemplet.m_SkinEquipUnitID,
				m_UnitUID = (long)skinTemplet.m_SkinID,
				m_SkinID = skinTemplet.m_SkinID,
				m_UserUID = 0L,
				m_UnitLevel = level,
				m_iUnitLevelEXP = 0,
				m_LimitBreakLevel = limitBreakLevel,
				m_bLock = false
			};
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x00141848 File Offset: 0x0013FA48
		public static NKCUnitSortSystem.eFilterOption GetFilterOption(NKM_UNIT_STYLE_TYPE styleType)
		{
			switch (styleType)
			{
			case NKM_UNIT_STYLE_TYPE.NUST_INVALID:
				return NKCUnitSortSystem.eFilterOption.Everything;
			case NKM_UNIT_STYLE_TYPE.NUST_COUNTER:
				return NKCUnitSortSystem.eFilterOption.Unit_Counter;
			case NKM_UNIT_STYLE_TYPE.NUST_SOLDIER:
				return NKCUnitSortSystem.eFilterOption.Unit_Soldier;
			case NKM_UNIT_STYLE_TYPE.NUST_MECHANIC:
				return NKCUnitSortSystem.eFilterOption.Unit_Mechanic;
			case NKM_UNIT_STYLE_TYPE.NUST_CORRUPTED:
				return NKCUnitSortSystem.eFilterOption.Unit_Corrupted;
			case NKM_UNIT_STYLE_TYPE.NUST_REPLACER:
				return NKCUnitSortSystem.eFilterOption.Unit_Replacer;
			case NKM_UNIT_STYLE_TYPE.NUST_TRAINER:
				return NKCUnitSortSystem.eFilterOption.Unit_Trainer;
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_ASSAULT:
				return NKCUnitSortSystem.eFilterOption.Ship_Assault;
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_HEAVY:
				return NKCUnitSortSystem.eFilterOption.Ship_Heavy;
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_CRUISER:
				return NKCUnitSortSystem.eFilterOption.Ship_Cruiser;
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_SPECIAL:
				return NKCUnitSortSystem.eFilterOption.Ship_Special;
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_PATROL:
				return NKCUnitSortSystem.eFilterOption.Ship_Patrol;
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_ETC:
				return NKCUnitSortSystem.eFilterOption.Ship_Etc;
			}
			return NKCUnitSortSystem.eFilterOption.Nothing;
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x001418BA File Offset: 0x0013FABA
		public static NKCUnitSortSystem.eFilterOption GetFilterOption(NKM_UNIT_ROLE_TYPE roleType)
		{
			switch (roleType)
			{
			default:
				return NKCUnitSortSystem.eFilterOption.Everything;
			case NKM_UNIT_ROLE_TYPE.NURT_STRIKER:
				return NKCUnitSortSystem.eFilterOption.Role_Striker;
			case NKM_UNIT_ROLE_TYPE.NURT_RANGER:
				return NKCUnitSortSystem.eFilterOption.Role_Ranger;
			case NKM_UNIT_ROLE_TYPE.NURT_DEFENDER:
				return NKCUnitSortSystem.eFilterOption.Role_Defender;
			case NKM_UNIT_ROLE_TYPE.NURT_SNIPER:
				return NKCUnitSortSystem.eFilterOption.Role_Sniper;
			case NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER:
				return NKCUnitSortSystem.eFilterOption.Role_Supporter;
			case NKM_UNIT_ROLE_TYPE.NURT_SIEGE:
				return NKCUnitSortSystem.eFilterOption.Role_Siege;
			case NKM_UNIT_ROLE_TYPE.NURT_TOWER:
				return NKCUnitSortSystem.eFilterOption.Role_Tower;
			}
		}

		// Token: 0x06003E2D RID: 15917 RVA: 0x001418F8 File Offset: 0x0013FAF8
		public static NKCUnitSortSystem.eFilterOption GetFilterOption(NKM_FIND_TARGET_TYPE targetType)
		{
			switch (targetType)
			{
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY:
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_AIR_FIRST:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_BOSS_LAST:
			case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_ONLY:
			case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS:
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM:
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LOW_HP:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_MY_TEAM:
				return NKCUnitSortSystem.eFilterOption.Unit_Target_All;
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_LAND:
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_LAND_RANGER_SUPPORTER_SNIPER_FIRST:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_LAND:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_LAND_BOSS_LAST:
			case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_LAND:
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LAND:
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LOW_HP_LAND:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_MY_TEAM_LAND:
				return NKCUnitSortSystem.eFilterOption.Unit_Target_Ground;
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_AIR:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_AIR:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_AIR_BOSS_LAST:
			case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_AIR:
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_AIR:
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LOW_HP_AIR:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_MY_TEAM_AIR:
				return NKCUnitSortSystem.eFilterOption.Unit_Target_Air;
			}
			return NKCUnitSortSystem.eFilterOption.Nothing;
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x0014197C File Offset: 0x0013FB7C
		public static List<long> MakeAutoCompleteDeck(NKMUserData userData, NKMDeckIndex currentDeckIndex, NKMDeckData deckData)
		{
			if (deckData == null)
			{
				Debug.LogError("deckData is null");
				return new List<long>();
			}
			NKCUnitSortSystem.UnitListOptions unitListOptions = new NKCUnitSortSystem.UnitListOptions
			{
				eDeckType = currentDeckIndex.m_eDeckType,
				setExcludeUnitID = null,
				setOnlyIncludeUnitID = null,
				setDuplicateUnitID = new HashSet<int>(),
				setExcludeUnitUID = new HashSet<long>(),
				bExcludeLockedUnit = false,
				bExcludeDeckedUnit = false,
				setFilterOption = null,
				lstSortOption = new List<NKCUnitSortSystem.eSortOption>
				{
					NKCUnitSortSystem.eSortOption.Power_High,
					NKCUnitSortSystem.eSortOption.UID_First
				},
				bDescending = true,
				bHideDeckedUnit = !NKMArmyData.IsAllowedSameUnitInMultipleDeck(currentDeckIndex.m_eDeckType),
				bPushBackUnselectable = true,
				bIncludeUndeckableUnit = false,
				bIgnoreCityState = true,
				bIgnoreWorldMapLeader = true
			};
			NKM_UNIT_ROLE_TYPE search_role = NKM_UNIT_ROLE_TYPE.NURT_STRIKER;
			NKM_UNIT_ROLE_TYPE search_role2 = search_role;
			if (userData != null)
			{
				unitListOptions.MakeDuplicateUnitSet(currentDeckIndex, 0L, userData.m_ArmyData);
			}
			List<long> list = new List<long>();
			List<string> list2 = new List<string>();
			Predicate<NKMUnitData> <>9__0;
			for (int i = 0; i < deckData.m_listDeckUnitUID.Count; i++)
			{
				if (deckData.m_listDeckUnitUID[i] != 0L)
				{
					list.Add(deckData.m_listDeckUnitUID[i]);
				}
				else
				{
					unitListOptions.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>
					{
						NKCUnitSortSystem.GetFilterOption(NKM_UNIT_ROLE_TYPE.NURT_INVALID)
					};
					List<NKMUnitData> list3 = new NKCUnitSort(userData, unitListOptions).AutoSelect(null, 4, null);
					search_role2 = search_role;
					NKMUnitData nkmunitData;
					do
					{
						List<NKMUnitData> list4 = list3;
						Predicate<NKMUnitData> match;
						if ((match = <>9__0) == null)
						{
							match = (<>9__0 = delegate(NKMUnitData v)
							{
								NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(v);
								return unitTempletBase != null && unitTempletBase.m_NKM_UNIT_ROLE_TYPE == search_role;
							});
						}
						nkmunitData = list4.Find(match);
						search_role = NKCUnitSortSystem.GetAutoUnitRoleNext(search_role);
					}
					while (nkmunitData == null && search_role != search_role2);
					if (nkmunitData == null)
					{
						nkmunitData = new NKCUnitSort(userData, unitListOptions).AutoSelect(null, null);
					}
					if (nkmunitData == null)
					{
						while (list.Count < deckData.m_listDeckUnitUID.Count)
						{
							list.Add(deckData.m_listDeckUnitUID[list.Count]);
						}
						return list;
					}
					unitListOptions.setDuplicateUnitID.Add(nkmunitData.m_UnitID);
					unitListOptions.setExcludeUnitUID.Add(nkmunitData.m_UnitUID);
					list.Add(nkmunitData.m_UnitUID);
					list2.Add(NKMUnitManager.GetUnitTempletBase(nkmunitData.m_UnitID).GetUnitName());
				}
			}
			for (int j = 0; j < list2.Count; j++)
			{
				Debug.Log(list2[j]);
			}
			return list;
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x00141BFC File Offset: 0x0013FDFC
		public static List<long> MakeLocalAutoCompleteDeck(NKMUserData userData, NKMDeckIndex deckIndex, List<long> deckUnitList, bool prohibitSameUnitId)
		{
			if (deckUnitList == null)
			{
				Debug.LogError("deckData is null");
				return new List<long>();
			}
			NKCUnitSortSystem.UnitListOptions unitListOptions = new NKCUnitSortSystem.UnitListOptions
			{
				eDeckType = deckIndex.m_eDeckType,
				setExcludeUnitID = null,
				setOnlyIncludeUnitID = null,
				setDuplicateUnitID = new HashSet<int>(),
				setExcludeUnitUID = new HashSet<long>(),
				bExcludeLockedUnit = false,
				bExcludeDeckedUnit = false,
				setFilterOption = null,
				lstSortOption = new List<NKCUnitSortSystem.eSortOption>
				{
					NKCUnitSortSystem.eSortOption.Power_High,
					NKCUnitSortSystem.eSortOption.UID_First
				},
				bDescending = true,
				bHideDeckedUnit = !NKMArmyData.IsAllowedSameUnitInMultipleDeck(deckIndex.m_eDeckType),
				bPushBackUnselectable = true,
				bIncludeUndeckableUnit = false,
				bIgnoreCityState = true,
				bIgnoreWorldMapLeader = true
			};
			NKM_UNIT_ROLE_TYPE search_role = NKM_UNIT_ROLE_TYPE.NURT_STRIKER;
			NKM_UNIT_ROLE_TYPE search_role2 = search_role;
			foreach (KeyValuePair<int, NKMEventDeckData> keyValuePair in NKCLocalDeckDataManager.GetAllLocalDeckData())
			{
				if (prohibitSameUnitId || keyValuePair.Key != (int)deckIndex.m_iIndex)
				{
					foreach (KeyValuePair<int, long> keyValuePair2 in keyValuePair.Value.m_dicUnit)
					{
						long value = keyValuePair2.Value;
						if (value != 0L && userData != null)
						{
							NKMUnitData unitFromUID = userData.m_ArmyData.GetUnitFromUID(value);
							if (NKMUnitManager.GetUnitTempletBase(unitFromUID) != null)
							{
								unitListOptions.setExcludeUnitUID.Add(value);
								unitListOptions.setDuplicateUnitID.Add(unitFromUID.m_UnitID);
							}
						}
					}
				}
			}
			List<long> list = new List<long>();
			List<string> list2 = new List<string>();
			Predicate<NKMUnitData> <>9__0;
			for (int i = 0; i < deckUnitList.Count; i++)
			{
				if (deckUnitList[i] != 0L)
				{
					list.Add(deckUnitList[i]);
				}
				else
				{
					unitListOptions.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>
					{
						NKCUnitSortSystem.GetFilterOption(NKM_UNIT_ROLE_TYPE.NURT_INVALID)
					};
					List<NKMUnitData> list3 = new NKCUnitSort(userData, unitListOptions, true).AutoSelect(null, 4, null);
					search_role2 = search_role;
					NKMUnitData nkmunitData;
					do
					{
						List<NKMUnitData> list4 = list3;
						Predicate<NKMUnitData> match;
						if ((match = <>9__0) == null)
						{
							match = (<>9__0 = delegate(NKMUnitData v)
							{
								NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(v);
								return unitTempletBase != null && unitTempletBase.m_NKM_UNIT_ROLE_TYPE == search_role;
							});
						}
						nkmunitData = list4.Find(match);
						search_role = NKCUnitSortSystem.GetAutoUnitRoleNext(search_role);
					}
					while (nkmunitData == null && search_role != search_role2);
					if (nkmunitData == null)
					{
						nkmunitData = new NKCUnitSort(userData, unitListOptions, true).AutoSelect(null, null);
					}
					if (nkmunitData == null)
					{
						while (list.Count < deckUnitList.Count)
						{
							list.Add(deckUnitList[list.Count]);
						}
						return list;
					}
					unitListOptions.setDuplicateUnitID.Add(nkmunitData.m_UnitID);
					unitListOptions.setExcludeUnitUID.Add(nkmunitData.m_UnitUID);
					list.Add(nkmunitData.m_UnitUID);
					list2.Add(NKMUnitManager.GetUnitTempletBase(nkmunitData.m_UnitID).GetUnitName());
				}
			}
			for (int j = 0; j < list2.Count; j++)
			{
				Debug.Log(list2[j]);
			}
			return list;
		}

		// Token: 0x06003E30 RID: 15920 RVA: 0x00141F38 File Offset: 0x00140138
		public static long LocalAutoSelectShip(NKMUserData userData, NKMDeckIndex selectedDeckIndex, long deckShip, bool prohibitSameUnitId)
		{
			if (deckShip > 0L)
			{
				return deckShip;
			}
			long result = 0L;
			NKCUnitSortSystem.UnitListOptions unitListOptions = new NKCUnitSortSystem.UnitListOptions
			{
				eDeckType = selectedDeckIndex.m_eDeckType,
				setExcludeUnitID = null,
				setOnlyIncludeUnitID = null,
				setDuplicateUnitID = new HashSet<int>(),
				setExcludeUnitUID = new HashSet<long>(),
				bExcludeLockedUnit = false,
				bExcludeDeckedUnit = false,
				setFilterOption = null,
				lstSortOption = new List<NKCUnitSortSystem.eSortOption>
				{
					NKCUnitSortSystem.eSortOption.Power_High,
					NKCUnitSortSystem.eSortOption.UID_First
				},
				bDescending = true,
				bHideDeckedUnit = !NKMArmyData.IsAllowedSameUnitInMultipleDeck(selectedDeckIndex.m_eDeckType),
				bPushBackUnselectable = true,
				bIncludeUndeckableUnit = false
			};
			foreach (KeyValuePair<int, NKMEventDeckData> keyValuePair in NKCLocalDeckDataManager.GetAllLocalDeckData())
			{
				NKMUnitData shipFromUID = userData.m_ArmyData.GetShipFromUID(keyValuePair.Value.m_ShipUID);
				if (shipFromUID != null)
				{
					unitListOptions.setExcludeUnitUID.Add(shipFromUID.m_UnitUID);
					NKMUnitTempletBase shipTempletBase = NKMUnitManager.GetUnitTempletBase(shipFromUID.m_UnitID);
					if (shipTempletBase != null)
					{
						foreach (NKMUnitTempletBase nkmunitTempletBase in from e in NKMUnitTempletBase.Values
						where e.m_ShipGroupID == shipTempletBase.m_ShipGroupID
						select e)
						{
							unitListOptions.setDuplicateUnitID.Add(nkmunitTempletBase.m_UnitID);
						}
					}
				}
			}
			NKMUnitData nkmunitData = new NKCShipSort(userData, unitListOptions, true).AutoSelect(null, null);
			if (nkmunitData != null)
			{
				result = nkmunitData.m_UnitUID;
			}
			return result;
		}

		// Token: 0x06003E31 RID: 15921 RVA: 0x00142104 File Offset: 0x00140304
		private static NKM_UNIT_ROLE_TYPE GetAutoUnitRoleNext(NKM_UNIT_ROLE_TYPE current)
		{
			switch (current)
			{
			case NKM_UNIT_ROLE_TYPE.NURT_STRIKER:
				return NKM_UNIT_ROLE_TYPE.NURT_DEFENDER;
			case NKM_UNIT_ROLE_TYPE.NURT_RANGER:
				return NKM_UNIT_ROLE_TYPE.NURT_SNIPER;
			case NKM_UNIT_ROLE_TYPE.NURT_DEFENDER:
				return NKM_UNIT_ROLE_TYPE.NURT_RANGER;
			case NKM_UNIT_ROLE_TYPE.NURT_SNIPER:
				return NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER;
			}
			return NKM_UNIT_ROLE_TYPE.NURT_STRIKER;
		}

		// Token: 0x06003E32 RID: 15922 RVA: 0x0014212D File Offset: 0x0014032D
		public static List<NKCUnitSortSystem.eSortOption> GetDefaultSortOptions(NKM_UNIT_TYPE unitType, bool bIsCollection, bool bIsSelection = false)
		{
			if (bIsCollection)
			{
				if (bIsSelection)
				{
					return NKCUnitSortSystem.DEFAULT_UNIT_SELECTION_SORT_OPTION_LIST;
				}
				return NKCUnitSortSystem.DEFAULT_UNIT_COLLECTION_SORT_OPTION_LIST;
			}
			else
			{
				if (unitType == NKM_UNIT_TYPE.NUT_NORMAL)
				{
					return NKCUnitSortSystem.DEFAULT_UNIT_SORT_OPTION_LIST;
				}
				if (unitType != NKM_UNIT_TYPE.NUT_SHIP)
				{
					return null;
				}
				return NKCUnitSortSystem.DEFAULT_SHIP_SORT_OPTION_LIST;
			}
		}

		// Token: 0x06003E33 RID: 15923 RVA: 0x00142158 File Offset: 0x00140358
		public static List<NKCUnitSortSystem.eSortOption> AddDefaultSortOptions(List<NKCUnitSortSystem.eSortOption> sortOptions, NKM_UNIT_TYPE unitType, bool bIsCollection)
		{
			List<NKCUnitSortSystem.eSortOption> defaultSortOptions = NKCUnitSortSystem.GetDefaultSortOptions(unitType, bIsCollection, false);
			if (defaultSortOptions != null)
			{
				sortOptions.AddRange(defaultSortOptions);
			}
			return sortOptions;
		}

		// Token: 0x06003E34 RID: 15924 RVA: 0x00142179 File Offset: 0x00140379
		public List<NKMUnitData> GetCurrentUnitList()
		{
			return this.m_lstCurrentUnitList;
		}

		// Token: 0x06003E35 RID: 15925 RVA: 0x00142184 File Offset: 0x00140384
		private bool IsUnitIsCityLeaderOnMission(long unitUID)
		{
			if (this.GetCityStateCache(unitUID) == NKMWorldMapManager.WorldMapLeaderState.None)
			{
				return false;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				foreach (NKMWorldMapCityData nkmworldMapCityData in nkmuserData.m_WorldmapData.worldMapCityDataMap.Values)
				{
					if (nkmworldMapCityData.leaderUnitUID == unitUID && nkmworldMapCityData.HasMission())
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x040036ED RID: 14061
		private static readonly Dictionary<NKCUnitSortSystem.eSortCategory, Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>> s_dicSortCategory = new Dictionary<NKCUnitSortSystem.eSortCategory, Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>>
		{
			{
				NKCUnitSortSystem.eSortCategory.None,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.None, NKCUnitSortSystem.eSortOption.None)
			},
			{
				NKCUnitSortSystem.eSortCategory.Level,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.Level_Low, NKCUnitSortSystem.eSortOption.Level_High)
			},
			{
				NKCUnitSortSystem.eSortCategory.Rarity,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.Rarity_Low, NKCUnitSortSystem.eSortOption.Rarity_High)
			},
			{
				NKCUnitSortSystem.eSortCategory.UnitSummonCost,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.Unit_SummonCost_Low, NKCUnitSortSystem.eSortOption.Unit_SummonCost_High)
			},
			{
				NKCUnitSortSystem.eSortCategory.UnitPower,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.Power_Low, NKCUnitSortSystem.eSortOption.Power_High)
			},
			{
				NKCUnitSortSystem.eSortCategory.UID,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.UID_First, NKCUnitSortSystem.eSortOption.UID_Last)
			},
			{
				NKCUnitSortSystem.eSortCategory.ID,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.ID_First, NKCUnitSortSystem.eSortOption.ID_Last)
			},
			{
				NKCUnitSortSystem.eSortCategory.IDX,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.IDX_First, NKCUnitSortSystem.eSortOption.IDX_Last)
			},
			{
				NKCUnitSortSystem.eSortCategory.UnitAttack,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.Attack_Low, NKCUnitSortSystem.eSortOption.Attack_High)
			},
			{
				NKCUnitSortSystem.eSortCategory.UnitHealth,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.Health_Low, NKCUnitSortSystem.eSortOption.Health_High)
			},
			{
				NKCUnitSortSystem.eSortCategory.UnitDefense,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.Unit_Defense_Low, NKCUnitSortSystem.eSortOption.Unit_Defense_High)
			},
			{
				NKCUnitSortSystem.eSortCategory.UnitCrit,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.Unit_Crit_Low, NKCUnitSortSystem.eSortOption.Unit_Crit_High)
			},
			{
				NKCUnitSortSystem.eSortCategory.UnitHit,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.Unit_Hit_Low, NKCUnitSortSystem.eSortOption.Unit_Hit_High)
			},
			{
				NKCUnitSortSystem.eSortCategory.UnitEvade,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.Unit_Evade_Low, NKCUnitSortSystem.eSortOption.Unit_Evade_High)
			},
			{
				NKCUnitSortSystem.eSortCategory.UnitReduceSkillCool,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.Unit_ReduceSkillCool_Low, NKCUnitSortSystem.eSortOption.Unit_ReduceSkillCool_High)
			},
			{
				NKCUnitSortSystem.eSortCategory.Decked,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.Decked_Last, NKCUnitSortSystem.eSortOption.Decked_First)
			},
			{
				NKCUnitSortSystem.eSortCategory.PlayerLevel,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.Player_Level_Low, NKCUnitSortSystem.eSortOption.Player_Level_High)
			},
			{
				NKCUnitSortSystem.eSortCategory.LoginTime,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.LoginTime_Latly, NKCUnitSortSystem.eSortOption.LoginTime_Old)
			},
			{
				NKCUnitSortSystem.eSortCategory.ScoutProgress,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.ScoutProgress_Low, NKCUnitSortSystem.eSortOption.ScoutProgress_High)
			},
			{
				NKCUnitSortSystem.eSortCategory.GuildGrade,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.Guild_Grade_High, NKCUnitSortSystem.eSortOption.Guild_Grade_Low)
			},
			{
				NKCUnitSortSystem.eSortCategory.GuildWeeklyPoint,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.Guild_WeeklyPoint_High, NKCUnitSortSystem.eSortOption.Guild_WeeklyPoint_Low)
			},
			{
				NKCUnitSortSystem.eSortCategory.GuildTotalPoint,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.Guild_TotalPoint_High, NKCUnitSortSystem.eSortOption.Guild_TotalPoint_Low)
			},
			{
				NKCUnitSortSystem.eSortCategory.LimitBreakPossible,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.LimitBreak_High, NKCUnitSortSystem.eSortOption.LimitBreak_Low)
			},
			{
				NKCUnitSortSystem.eSortCategory.Transcendence,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.Transcendence_High, NKCUnitSortSystem.eSortOption.Transcendence_Low)
			},
			{
				NKCUnitSortSystem.eSortCategory.UnitLoyalty,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.Unit_Loyalty_Low, NKCUnitSortSystem.eSortOption.Unit_Loyalty_High)
			},
			{
				NKCUnitSortSystem.eSortCategory.Squad_Dungeon,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.Squad_Dungeon_Low, NKCUnitSortSystem.eSortOption.Squad_Dungeon_High)
			},
			{
				NKCUnitSortSystem.eSortCategory.Squad_Gauntlet,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.Squad_Gauntlet_Low, NKCUnitSortSystem.eSortOption.Squad_Gauntlet_High)
			},
			{
				NKCUnitSortSystem.eSortCategory.Favorite,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.Favorite_First, NKCUnitSortSystem.eSortOption.Favorite_Last)
			},
			{
				NKCUnitSortSystem.eSortCategory.TacticUpdatePossible,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.TacticUpdatePossible_High, NKCUnitSortSystem.eSortOption.TacticUpdatePossible_Low)
			},
			{
				NKCUnitSortSystem.eSortCategory.TacticUpdateLevel,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.TacticUpdateLevel_High, NKCUnitSortSystem.eSortOption.TacticUpdateLevel_Low)
			},
			{
				NKCUnitSortSystem.eSortCategory.Custom1,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.CustomAscend1, NKCUnitSortSystem.eSortOption.CustomDescend1)
			},
			{
				NKCUnitSortSystem.eSortCategory.Custom2,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.CustomAscend2, NKCUnitSortSystem.eSortOption.CustomDescend2)
			},
			{
				NKCUnitSortSystem.eSortCategory.Custom3,
				new Tuple<NKCUnitSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCUnitSortSystem.eSortOption.CustomAscend3, NKCUnitSortSystem.eSortOption.CustomDescend3)
			}
		};

		// Token: 0x040036EE RID: 14062
		private static readonly List<NKCUnitSortSystem.eSortOption> DEFAULT_UNIT_SORT_OPTION_LIST = new List<NKCUnitSortSystem.eSortOption>
		{
			NKCUnitSortSystem.eSortOption.Level_High,
			NKCUnitSortSystem.eSortOption.Rarity_High,
			NKCUnitSortSystem.eSortOption.Unit_SummonCost_High,
			NKCUnitSortSystem.eSortOption.ID_First,
			NKCUnitSortSystem.eSortOption.TacticUpdateLevel_High,
			NKCUnitSortSystem.eSortOption.UID_Last
		};

		// Token: 0x040036EF RID: 14063
		private static readonly List<NKCUnitSortSystem.eSortOption> DEFAULT_SHIP_SORT_OPTION_LIST = new List<NKCUnitSortSystem.eSortOption>
		{
			NKCUnitSortSystem.eSortOption.Level_High,
			NKCUnitSortSystem.eSortOption.Rarity_High,
			NKCUnitSortSystem.eSortOption.UID_Last
		};

		// Token: 0x040036F0 RID: 14064
		private static readonly List<NKCUnitSortSystem.eSortOption> DEFAULT_UNIT_COLLECTION_SORT_OPTION_LIST = new List<NKCUnitSortSystem.eSortOption>
		{
			NKCUnitSortSystem.eSortOption.IDX_First
		};

		// Token: 0x040036F1 RID: 14065
		private static readonly List<NKCUnitSortSystem.eSortOption> DEFAULT_UNIT_SELECTION_SORT_OPTION_LIST = new List<NKCUnitSortSystem.eSortOption>
		{
			NKCUnitSortSystem.eSortOption.Rarity_High,
			NKCUnitSortSystem.eSortOption.IDX_First
		};

		// Token: 0x040036F2 RID: 14066
		private static readonly HashSet<NKCUnitSortSystem.eFilterOption> m_setFilterCategory_Monster = new HashSet<NKCUnitSortSystem.eFilterOption>
		{
			NKCUnitSortSystem.eFilterOption.Unit_Corrupted,
			NKCUnitSortSystem.eFilterOption.Unit_Replacer
		};

		// Token: 0x040036F3 RID: 14067
		private static readonly HashSet<NKCUnitSortSystem.eFilterOption> m_setFilterCategory_UnitType = new HashSet<NKCUnitSortSystem.eFilterOption>
		{
			NKCUnitSortSystem.eFilterOption.Unit_Counter,
			NKCUnitSortSystem.eFilterOption.Unit_Mechanic,
			NKCUnitSortSystem.eFilterOption.Unit_Soldier,
			NKCUnitSortSystem.eFilterOption.Unit_Corrupted,
			NKCUnitSortSystem.eFilterOption.Unit_Replacer,
			NKCUnitSortSystem.eFilterOption.Unit_Trainer
		};

		// Token: 0x040036F4 RID: 14068
		private static readonly HashSet<NKCUnitSortSystem.eFilterOption> m_setFilterCategory_ShipType = new HashSet<NKCUnitSortSystem.eFilterOption>
		{
			NKCUnitSortSystem.eFilterOption.Ship_Assault,
			NKCUnitSortSystem.eFilterOption.Ship_Heavy,
			NKCUnitSortSystem.eFilterOption.Ship_Cruiser,
			NKCUnitSortSystem.eFilterOption.Ship_Special,
			NKCUnitSortSystem.eFilterOption.Ship_Patrol,
			NKCUnitSortSystem.eFilterOption.Ship_Etc
		};

		// Token: 0x040036F5 RID: 14069
		private static readonly HashSet<NKCUnitSortSystem.eFilterOption> m_setFilterCategory_UnitRole = new HashSet<NKCUnitSortSystem.eFilterOption>
		{
			NKCUnitSortSystem.eFilterOption.Role_Striker,
			NKCUnitSortSystem.eFilterOption.Role_Ranger,
			NKCUnitSortSystem.eFilterOption.Role_Sniper,
			NKCUnitSortSystem.eFilterOption.Role_Defender,
			NKCUnitSortSystem.eFilterOption.Role_Siege,
			NKCUnitSortSystem.eFilterOption.Role_Supporter,
			NKCUnitSortSystem.eFilterOption.Role_Tower
		};

		// Token: 0x040036F6 RID: 14070
		private static readonly HashSet<NKCUnitSortSystem.eFilterOption> m_setFilterCategory_UnitTargetType = new HashSet<NKCUnitSortSystem.eFilterOption>
		{
			NKCUnitSortSystem.eFilterOption.Unit_Target_Ground,
			NKCUnitSortSystem.eFilterOption.Unit_Target_Air,
			NKCUnitSortSystem.eFilterOption.Unit_Target_All
		};

		// Token: 0x040036F7 RID: 14071
		private static readonly HashSet<NKCUnitSortSystem.eFilterOption> m_setFilterCategory_UnitMoveType = new HashSet<NKCUnitSortSystem.eFilterOption>
		{
			NKCUnitSortSystem.eFilterOption.Unit_Move_Ground,
			NKCUnitSortSystem.eFilterOption.Unit_Move_Air
		};

		// Token: 0x040036F8 RID: 14072
		private static readonly HashSet<NKCUnitSortSystem.eFilterOption> m_setFilterCategory_Rarity = new HashSet<NKCUnitSortSystem.eFilterOption>
		{
			NKCUnitSortSystem.eFilterOption.Rarily_SSR,
			NKCUnitSortSystem.eFilterOption.Rarily_SR,
			NKCUnitSortSystem.eFilterOption.Rarily_R,
			NKCUnitSortSystem.eFilterOption.Rarily_N
		};

		// Token: 0x040036F9 RID: 14073
		private static readonly HashSet<NKCUnitSortSystem.eFilterOption> m_setFilterCategory_Cost = new HashSet<NKCUnitSortSystem.eFilterOption>
		{
			NKCUnitSortSystem.eFilterOption.Unit_Cost_10,
			NKCUnitSortSystem.eFilterOption.Unit_Cost_9,
			NKCUnitSortSystem.eFilterOption.Unit_Cost_8,
			NKCUnitSortSystem.eFilterOption.Unit_Cost_7,
			NKCUnitSortSystem.eFilterOption.Unit_Cost_6,
			NKCUnitSortSystem.eFilterOption.Unit_Cost_5,
			NKCUnitSortSystem.eFilterOption.Unit_Cost_4,
			NKCUnitSortSystem.eFilterOption.Unit_Cost_3,
			NKCUnitSortSystem.eFilterOption.Unit_Cost_2,
			NKCUnitSortSystem.eFilterOption.Unit_Cost_1
		};

		// Token: 0x040036FA RID: 14074
		private static readonly HashSet<NKCUnitSortSystem.eFilterOption> m_setFilterCategory_TacticLevel = new HashSet<NKCUnitSortSystem.eFilterOption>
		{
			NKCUnitSortSystem.eFilterOption.Unit_TacticLv_6,
			NKCUnitSortSystem.eFilterOption.Unit_TacticLv_5,
			NKCUnitSortSystem.eFilterOption.Unit_TacticLv_4,
			NKCUnitSortSystem.eFilterOption.Unit_TacticLv_3,
			NKCUnitSortSystem.eFilterOption.Unit_TacticLv_2,
			NKCUnitSortSystem.eFilterOption.Unit_TacticLv_1,
			NKCUnitSortSystem.eFilterOption.Unit_TacticLv_0
		};

		// Token: 0x040036FB RID: 14075
		private static readonly HashSet<NKCUnitSortSystem.eFilterOption> m_setFilterCategory_Level = new HashSet<NKCUnitSortSystem.eFilterOption>
		{
			NKCUnitSortSystem.eFilterOption.Level_1,
			NKCUnitSortSystem.eFilterOption.Level_other,
			NKCUnitSortSystem.eFilterOption.Level_Max
		};

		// Token: 0x040036FC RID: 14076
		private static readonly HashSet<NKCUnitSortSystem.eFilterOption> m_setFilterCategory_Decked = new HashSet<NKCUnitSortSystem.eFilterOption>
		{
			NKCUnitSortSystem.eFilterOption.Decked,
			NKCUnitSortSystem.eFilterOption.NotDecked
		};

		// Token: 0x040036FD RID: 14077
		private static readonly HashSet<NKCUnitSortSystem.eFilterOption> m_setFilterCategory_Locked = new HashSet<NKCUnitSortSystem.eFilterOption>
		{
			NKCUnitSortSystem.eFilterOption.Locked,
			NKCUnitSortSystem.eFilterOption.Unlocked
		};

		// Token: 0x040036FE RID: 14078
		private static readonly HashSet<NKCUnitSortSystem.eFilterOption> m_setFilterCategory_Have = new HashSet<NKCUnitSortSystem.eFilterOption>
		{
			NKCUnitSortSystem.eFilterOption.Have,
			NKCUnitSortSystem.eFilterOption.NotHave
		};

		// Token: 0x040036FF RID: 14079
		private static readonly HashSet<NKCUnitSortSystem.eFilterOption> m_setFilterCategory_Scout = new HashSet<NKCUnitSortSystem.eFilterOption>
		{
			NKCUnitSortSystem.eFilterOption.CanScout,
			NKCUnitSortSystem.eFilterOption.NoScout
		};

		// Token: 0x04003700 RID: 14080
		private static readonly HashSet<NKCUnitSortSystem.eFilterOption> m_setFilterCategory_Collected = new HashSet<NKCUnitSortSystem.eFilterOption>
		{
			NKCUnitSortSystem.eFilterOption.Collected,
			NKCUnitSortSystem.eFilterOption.NotCollected
		};

		// Token: 0x04003701 RID: 14081
		private static readonly HashSet<NKCUnitSortSystem.eFilterOption> m_setFilterCategory_SpecialType = new HashSet<NKCUnitSortSystem.eFilterOption>
		{
			NKCUnitSortSystem.eFilterOption.SpecialType_Rearm,
			NKCUnitSortSystem.eFilterOption.SpecialType_Awaken
		};

		// Token: 0x04003702 RID: 14082
		private static readonly HashSet<NKCUnitSortSystem.eFilterOption> m_setFilterCategory_RoomIn = new HashSet<NKCUnitSortSystem.eFilterOption>
		{
			NKCUnitSortSystem.eFilterOption.InRoom,
			NKCUnitSortSystem.eFilterOption.OutRoom
		};

		// Token: 0x04003703 RID: 14083
		private static readonly HashSet<NKCUnitSortSystem.eFilterOption> m_setFilterCategory_Loyalty = new HashSet<NKCUnitSortSystem.eFilterOption>
		{
			NKCUnitSortSystem.eFilterOption.Loyalty_Zero,
			NKCUnitSortSystem.eFilterOption.Loyalty_Intermediate,
			NKCUnitSortSystem.eFilterOption.Loyalty_Max
		};

		// Token: 0x04003704 RID: 14084
		private static readonly HashSet<NKCUnitSortSystem.eFilterOption> m_setFilterCategory_LifeContract = new HashSet<NKCUnitSortSystem.eFilterOption>
		{
			NKCUnitSortSystem.eFilterOption.LifeContract_Unsigned,
			NKCUnitSortSystem.eFilterOption.LifeContract_Signed
		};

		// Token: 0x04003705 RID: 14085
		private static readonly HashSet<NKCUnitSortSystem.eFilterOption> m_setFilterCategory_CollectionAchieve = new HashSet<NKCUnitSortSystem.eFilterOption>
		{
			NKCUnitSortSystem.eFilterOption.Collection_HasAchieve,
			NKCUnitSortSystem.eFilterOption.Collection_CompleteAchieve
		};

		// Token: 0x04003706 RID: 14086
		private static readonly HashSet<NKCUnitSortSystem.eFilterOption> m_setFilterCategory_Favorite = new HashSet<NKCUnitSortSystem.eFilterOption>
		{
			NKCUnitSortSystem.eFilterOption.Favorite,
			NKCUnitSortSystem.eFilterOption.Favorite_Not
		};

		// Token: 0x04003707 RID: 14087
		private static readonly List<HashSet<NKCUnitSortSystem.eFilterOption>> m_lstFilterCategory = new List<HashSet<NKCUnitSortSystem.eFilterOption>>
		{
			NKCUnitSortSystem.m_setFilterCategory_Monster,
			NKCUnitSortSystem.m_setFilterCategory_UnitType,
			NKCUnitSortSystem.m_setFilterCategory_ShipType,
			NKCUnitSortSystem.m_setFilterCategory_UnitRole,
			NKCUnitSortSystem.m_setFilterCategory_UnitMoveType,
			NKCUnitSortSystem.m_setFilterCategory_UnitTargetType,
			NKCUnitSortSystem.m_setFilterCategory_Rarity,
			NKCUnitSortSystem.m_setFilterCategory_Cost,
			NKCUnitSortSystem.m_setFilterCategory_TacticLevel,
			NKCUnitSortSystem.m_setFilterCategory_Level,
			NKCUnitSortSystem.m_setFilterCategory_Decked,
			NKCUnitSortSystem.m_setFilterCategory_Locked,
			NKCUnitSortSystem.m_setFilterCategory_Have,
			NKCUnitSortSystem.m_setFilterCategory_Scout,
			NKCUnitSortSystem.m_setFilterCategory_Collected,
			NKCUnitSortSystem.m_setFilterCategory_RoomIn,
			NKCUnitSortSystem.m_setFilterCategory_Loyalty,
			NKCUnitSortSystem.m_setFilterCategory_LifeContract,
			NKCUnitSortSystem.m_setFilterCategory_CollectionAchieve,
			NKCUnitSortSystem.m_setFilterCategory_SpecialType,
			NKCUnitSortSystem.m_setFilterCategory_Favorite
		};

		// Token: 0x04003708 RID: 14088
		public static readonly HashSet<NKCUnitSortSystem.eFilterCategory> setDefaultUnitFilterCategory = new HashSet<NKCUnitSortSystem.eFilterCategory>
		{
			NKCUnitSortSystem.eFilterCategory.UnitType,
			NKCUnitSortSystem.eFilterCategory.UnitRole,
			NKCUnitSortSystem.eFilterCategory.UnitMoveType,
			NKCUnitSortSystem.eFilterCategory.UnitTargetType,
			NKCUnitSortSystem.eFilterCategory.Rarity,
			NKCUnitSortSystem.eFilterCategory.Cost,
			NKCUnitSortSystem.eFilterCategory.TacticLv,
			NKCUnitSortSystem.eFilterCategory.Level,
			NKCUnitSortSystem.eFilterCategory.Decked,
			NKCUnitSortSystem.eFilterCategory.Locked,
			NKCUnitSortSystem.eFilterCategory.SpecialType
		};

		// Token: 0x04003709 RID: 14089
		public static readonly HashSet<NKCUnitSortSystem.eSortCategory> setDefaultUnitSortCategory = new HashSet<NKCUnitSortSystem.eSortCategory>
		{
			NKCUnitSortSystem.eSortCategory.Level,
			NKCUnitSortSystem.eSortCategory.Rarity,
			NKCUnitSortSystem.eSortCategory.UnitSummonCost,
			NKCUnitSortSystem.eSortCategory.TacticUpdatePossible,
			NKCUnitSortSystem.eSortCategory.UID,
			NKCUnitSortSystem.eSortCategory.UnitPower,
			NKCUnitSortSystem.eSortCategory.UnitAttack,
			NKCUnitSortSystem.eSortCategory.UnitHealth,
			NKCUnitSortSystem.eSortCategory.UnitDefense,
			NKCUnitSortSystem.eSortCategory.UnitHit,
			NKCUnitSortSystem.eSortCategory.UnitEvade,
			NKCUnitSortSystem.eSortCategory.UnitCrit,
			NKCUnitSortSystem.eSortCategory.UnitLoyalty,
			NKCUnitSortSystem.eSortCategory.LimitBreakPossible,
			NKCUnitSortSystem.eSortCategory.Transcendence,
			NKCUnitSortSystem.eSortCategory.Squad_Dungeon,
			NKCUnitSortSystem.eSortCategory.Squad_Gauntlet
		};

		// Token: 0x0400370A RID: 14090
		public static readonly HashSet<NKCUnitSortSystem.eFilterCategory> setDefaultShipFilterCategory = new HashSet<NKCUnitSortSystem.eFilterCategory>
		{
			NKCUnitSortSystem.eFilterCategory.ShipType,
			NKCUnitSortSystem.eFilterCategory.Rarity,
			NKCUnitSortSystem.eFilterCategory.Level,
			NKCUnitSortSystem.eFilterCategory.Decked,
			NKCUnitSortSystem.eFilterCategory.Locked
		};

		// Token: 0x0400370B RID: 14091
		public static readonly HashSet<NKCUnitSortSystem.eSortCategory> setDefaultShipSortCategory = new HashSet<NKCUnitSortSystem.eSortCategory>
		{
			NKCUnitSortSystem.eSortCategory.Level,
			NKCUnitSortSystem.eSortCategory.Rarity,
			NKCUnitSortSystem.eSortCategory.UID,
			NKCUnitSortSystem.eSortCategory.UnitPower,
			NKCUnitSortSystem.eSortCategory.UnitAttack,
			NKCUnitSortSystem.eSortCategory.UnitHealth
		};

		// Token: 0x0400370C RID: 14092
		public static readonly HashSet<NKCUnitSortSystem.eFilterCategory> setDefaultTrophyFilterCategory = new HashSet<NKCUnitSortSystem.eFilterCategory>
		{
			NKCUnitSortSystem.eFilterCategory.UnitRole,
			NKCUnitSortSystem.eFilterCategory.Rarity,
			NKCUnitSortSystem.eFilterCategory.Locked
		};

		// Token: 0x0400370D RID: 14093
		public static readonly HashSet<NKCUnitSortSystem.eSortCategory> setDefaultTrophySortCategory = new HashSet<NKCUnitSortSystem.eSortCategory>
		{
			NKCUnitSortSystem.eSortCategory.Rarity,
			NKCUnitSortSystem.eSortCategory.UID
		};

		// Token: 0x0400370E RID: 14094
		protected NKCUnitSortSystem.UnitListOptions m_Options;

		// Token: 0x0400370F RID: 14095
		protected Dictionary<long, NKMUnitData> m_dicAllUnitList;

		// Token: 0x04003710 RID: 14096
		protected List<NKMUnitData> m_lstCurrentUnitList;

		// Token: 0x04003711 RID: 14097
		protected bool m_bEnableShowBan;

		// Token: 0x04003712 RID: 14098
		protected bool m_bEnableShowUpUnit;

		// Token: 0x04003713 RID: 14099
		private Dictionary<long, NKCUnitSortSystem.UnitInfoCache> m_dicUnitInfoCache = new Dictionary<long, NKCUnitSortSystem.UnitInfoCache>();

		// Token: 0x04003714 RID: 14100
		private Dictionary<int, HashSet<int>> m_dicDeckedUnitIdCache = new Dictionary<int, HashSet<int>>();

		// Token: 0x04003715 RID: 14101
		private Dictionary<int, HashSet<int>> m_dicDeckedShipGroupIdCache = new Dictionary<int, HashSet<int>>();

		// Token: 0x020013B3 RID: 5043
		public enum eFilterCategory
		{
			// Token: 0x04009B08 RID: 39688
			UnitType,
			// Token: 0x04009B09 RID: 39689
			ShipType,
			// Token: 0x04009B0A RID: 39690
			UnitRole,
			// Token: 0x04009B0B RID: 39691
			UnitTargetType,
			// Token: 0x04009B0C RID: 39692
			Rarity,
			// Token: 0x04009B0D RID: 39693
			Cost,
			// Token: 0x04009B0E RID: 39694
			TacticLv,
			// Token: 0x04009B0F RID: 39695
			Level,
			// Token: 0x04009B10 RID: 39696
			Decked,
			// Token: 0x04009B11 RID: 39697
			Locked,
			// Token: 0x04009B12 RID: 39698
			Have,
			// Token: 0x04009B13 RID: 39699
			UnitMoveType,
			// Token: 0x04009B14 RID: 39700
			InRoom,
			// Token: 0x04009B15 RID: 39701
			Loyalty,
			// Token: 0x04009B16 RID: 39702
			LifeContract,
			// Token: 0x04009B17 RID: 39703
			Scout,
			// Token: 0x04009B18 RID: 39704
			Collected,
			// Token: 0x04009B19 RID: 39705
			SpecialType,
			// Token: 0x04009B1A RID: 39706
			Collection_Achieve,
			// Token: 0x04009B1B RID: 39707
			MonsterType
		}

		// Token: 0x020013B4 RID: 5044
		public enum eFilterOption
		{
			// Token: 0x04009B1D RID: 39709
			Nothing,
			// Token: 0x04009B1E RID: 39710
			Everything,
			// Token: 0x04009B1F RID: 39711
			Unit_Counter,
			// Token: 0x04009B20 RID: 39712
			Unit_Mechanic,
			// Token: 0x04009B21 RID: 39713
			Unit_Soldier,
			// Token: 0x04009B22 RID: 39714
			Unit_Corrupted,
			// Token: 0x04009B23 RID: 39715
			Unit_Replacer,
			// Token: 0x04009B24 RID: 39716
			Unit_Trainer,
			// Token: 0x04009B25 RID: 39717
			Unit_Target_Ground,
			// Token: 0x04009B26 RID: 39718
			Unit_Target_Air,
			// Token: 0x04009B27 RID: 39719
			Unit_Target_All,
			// Token: 0x04009B28 RID: 39720
			Unit_Move_Ground,
			// Token: 0x04009B29 RID: 39721
			Unit_Move_Air,
			// Token: 0x04009B2A RID: 39722
			Ship_Assault,
			// Token: 0x04009B2B RID: 39723
			Ship_Heavy,
			// Token: 0x04009B2C RID: 39724
			Ship_Cruiser,
			// Token: 0x04009B2D RID: 39725
			Ship_Special,
			// Token: 0x04009B2E RID: 39726
			Ship_Patrol,
			// Token: 0x04009B2F RID: 39727
			Ship_Etc,
			// Token: 0x04009B30 RID: 39728
			Role_Striker,
			// Token: 0x04009B31 RID: 39729
			Role_Ranger,
			// Token: 0x04009B32 RID: 39730
			Role_Sniper,
			// Token: 0x04009B33 RID: 39731
			Role_Defender,
			// Token: 0x04009B34 RID: 39732
			Role_Siege,
			// Token: 0x04009B35 RID: 39733
			Role_Supporter,
			// Token: 0x04009B36 RID: 39734
			Role_Tower,
			// Token: 0x04009B37 RID: 39735
			Rarily_SSR,
			// Token: 0x04009B38 RID: 39736
			Rarily_SR,
			// Token: 0x04009B39 RID: 39737
			Rarily_R,
			// Token: 0x04009B3A RID: 39738
			Rarily_N,
			// Token: 0x04009B3B RID: 39739
			Unit_Cost_10,
			// Token: 0x04009B3C RID: 39740
			Unit_Cost_9,
			// Token: 0x04009B3D RID: 39741
			Unit_Cost_8,
			// Token: 0x04009B3E RID: 39742
			Unit_Cost_7,
			// Token: 0x04009B3F RID: 39743
			Unit_Cost_6,
			// Token: 0x04009B40 RID: 39744
			Unit_Cost_5,
			// Token: 0x04009B41 RID: 39745
			Unit_Cost_4,
			// Token: 0x04009B42 RID: 39746
			Unit_Cost_3,
			// Token: 0x04009B43 RID: 39747
			Unit_Cost_2,
			// Token: 0x04009B44 RID: 39748
			Unit_Cost_1,
			// Token: 0x04009B45 RID: 39749
			Unit_TacticLv_6,
			// Token: 0x04009B46 RID: 39750
			Unit_TacticLv_5,
			// Token: 0x04009B47 RID: 39751
			Unit_TacticLv_4,
			// Token: 0x04009B48 RID: 39752
			Unit_TacticLv_3,
			// Token: 0x04009B49 RID: 39753
			Unit_TacticLv_2,
			// Token: 0x04009B4A RID: 39754
			Unit_TacticLv_1,
			// Token: 0x04009B4B RID: 39755
			Unit_TacticLv_0,
			// Token: 0x04009B4C RID: 39756
			Level_1,
			// Token: 0x04009B4D RID: 39757
			Level_other,
			// Token: 0x04009B4E RID: 39758
			Level_Max,
			// Token: 0x04009B4F RID: 39759
			Decked,
			// Token: 0x04009B50 RID: 39760
			NotDecked,
			// Token: 0x04009B51 RID: 39761
			Locked,
			// Token: 0x04009B52 RID: 39762
			Unlocked,
			// Token: 0x04009B53 RID: 39763
			Have,
			// Token: 0x04009B54 RID: 39764
			NotHave,
			// Token: 0x04009B55 RID: 39765
			InRoom,
			// Token: 0x04009B56 RID: 39766
			OutRoom,
			// Token: 0x04009B57 RID: 39767
			Loyalty_Zero,
			// Token: 0x04009B58 RID: 39768
			Loyalty_Intermediate,
			// Token: 0x04009B59 RID: 39769
			Loyalty_Max,
			// Token: 0x04009B5A RID: 39770
			LifeContract_Unsigned,
			// Token: 0x04009B5B RID: 39771
			LifeContract_Signed,
			// Token: 0x04009B5C RID: 39772
			CanScout,
			// Token: 0x04009B5D RID: 39773
			NoScout,
			// Token: 0x04009B5E RID: 39774
			SpecialType_Rearm,
			// Token: 0x04009B5F RID: 39775
			SpecialType_Awaken,
			// Token: 0x04009B60 RID: 39776
			Collected,
			// Token: 0x04009B61 RID: 39777
			NotCollected,
			// Token: 0x04009B62 RID: 39778
			Collection_HasAchieve,
			// Token: 0x04009B63 RID: 39779
			Collection_CompleteAchieve,
			// Token: 0x04009B64 RID: 39780
			Favorite,
			// Token: 0x04009B65 RID: 39781
			Favorite_Not
		}

		// Token: 0x020013B5 RID: 5045
		public enum eSortCategory
		{
			// Token: 0x04009B67 RID: 39783
			None,
			// Token: 0x04009B68 RID: 39784
			Level,
			// Token: 0x04009B69 RID: 39785
			Rarity,
			// Token: 0x04009B6A RID: 39786
			UnitSummonCost,
			// Token: 0x04009B6B RID: 39787
			UnitPower,
			// Token: 0x04009B6C RID: 39788
			UID,
			// Token: 0x04009B6D RID: 39789
			ID,
			// Token: 0x04009B6E RID: 39790
			IDX,
			// Token: 0x04009B6F RID: 39791
			UnitAttack,
			// Token: 0x04009B70 RID: 39792
			UnitHealth,
			// Token: 0x04009B71 RID: 39793
			UnitDefense,
			// Token: 0x04009B72 RID: 39794
			UnitCrit,
			// Token: 0x04009B73 RID: 39795
			UnitHit,
			// Token: 0x04009B74 RID: 39796
			UnitEvade,
			// Token: 0x04009B75 RID: 39797
			UnitReduceSkillCool,
			// Token: 0x04009B76 RID: 39798
			Decked,
			// Token: 0x04009B77 RID: 39799
			PlayerLevel,
			// Token: 0x04009B78 RID: 39800
			LoginTime,
			// Token: 0x04009B79 RID: 39801
			ScoutProgress,
			// Token: 0x04009B7A RID: 39802
			SetOption,
			// Token: 0x04009B7B RID: 39803
			LimitBreakPossible,
			// Token: 0x04009B7C RID: 39804
			Transcendence,
			// Token: 0x04009B7D RID: 39805
			UnitLoyalty,
			// Token: 0x04009B7E RID: 39806
			Squad_Dungeon,
			// Token: 0x04009B7F RID: 39807
			Squad_Gauntlet,
			// Token: 0x04009B80 RID: 39808
			GuildGrade,
			// Token: 0x04009B81 RID: 39809
			GuildWeeklyPoint,
			// Token: 0x04009B82 RID: 39810
			GuildTotalPoint,
			// Token: 0x04009B83 RID: 39811
			Favorite,
			// Token: 0x04009B84 RID: 39812
			TacticUpdatePossible,
			// Token: 0x04009B85 RID: 39813
			TacticUpdateLevel,
			// Token: 0x04009B86 RID: 39814
			Custom1,
			// Token: 0x04009B87 RID: 39815
			Custom2,
			// Token: 0x04009B88 RID: 39816
			Custom3
		}

		// Token: 0x020013B6 RID: 5046
		public enum eSortOption
		{
			// Token: 0x04009B8A RID: 39818
			None,
			// Token: 0x04009B8B RID: 39819
			Level_Low,
			// Token: 0x04009B8C RID: 39820
			Level_High,
			// Token: 0x04009B8D RID: 39821
			Rarity_Low,
			// Token: 0x04009B8E RID: 39822
			Rarity_High,
			// Token: 0x04009B8F RID: 39823
			Unit_SummonCost_Low,
			// Token: 0x04009B90 RID: 39824
			Unit_SummonCost_High,
			// Token: 0x04009B91 RID: 39825
			Power_Low,
			// Token: 0x04009B92 RID: 39826
			Power_High,
			// Token: 0x04009B93 RID: 39827
			UID_First,
			// Token: 0x04009B94 RID: 39828
			UID_Last,
			// Token: 0x04009B95 RID: 39829
			ID_First,
			// Token: 0x04009B96 RID: 39830
			ID_Last,
			// Token: 0x04009B97 RID: 39831
			IDX_First,
			// Token: 0x04009B98 RID: 39832
			IDX_Last,
			// Token: 0x04009B99 RID: 39833
			Attack_Low,
			// Token: 0x04009B9A RID: 39834
			Attack_High,
			// Token: 0x04009B9B RID: 39835
			Health_Low,
			// Token: 0x04009B9C RID: 39836
			Health_High,
			// Token: 0x04009B9D RID: 39837
			Unit_Defense_Low,
			// Token: 0x04009B9E RID: 39838
			Unit_Defense_High,
			// Token: 0x04009B9F RID: 39839
			Unit_Crit_Low,
			// Token: 0x04009BA0 RID: 39840
			Unit_Crit_High,
			// Token: 0x04009BA1 RID: 39841
			Unit_Hit_Low,
			// Token: 0x04009BA2 RID: 39842
			Unit_Hit_High,
			// Token: 0x04009BA3 RID: 39843
			Unit_Evade_Low,
			// Token: 0x04009BA4 RID: 39844
			Unit_Evade_High,
			// Token: 0x04009BA5 RID: 39845
			Unit_ReduceSkillCool_Low,
			// Token: 0x04009BA6 RID: 39846
			Unit_ReduceSkillCool_High,
			// Token: 0x04009BA7 RID: 39847
			Decked_First,
			// Token: 0x04009BA8 RID: 39848
			Decked_Last,
			// Token: 0x04009BA9 RID: 39849
			Player_Level_Low,
			// Token: 0x04009BAA RID: 39850
			Player_Level_High,
			// Token: 0x04009BAB RID: 39851
			LoginTime_Latly,
			// Token: 0x04009BAC RID: 39852
			LoginTime_Old,
			// Token: 0x04009BAD RID: 39853
			ScoutProgress_High,
			// Token: 0x04009BAE RID: 39854
			ScoutProgress_Low,
			// Token: 0x04009BAF RID: 39855
			LimitBreak_High,
			// Token: 0x04009BB0 RID: 39856
			LimitBreak_Low,
			// Token: 0x04009BB1 RID: 39857
			Transcendence_High,
			// Token: 0x04009BB2 RID: 39858
			Transcendence_Low,
			// Token: 0x04009BB3 RID: 39859
			Unit_Loyalty_High,
			// Token: 0x04009BB4 RID: 39860
			Unit_Loyalty_Low,
			// Token: 0x04009BB5 RID: 39861
			Squad_Dungeon_High,
			// Token: 0x04009BB6 RID: 39862
			Squad_Dungeon_Low,
			// Token: 0x04009BB7 RID: 39863
			Squad_Gauntlet_High,
			// Token: 0x04009BB8 RID: 39864
			Squad_Gauntlet_Low,
			// Token: 0x04009BB9 RID: 39865
			Guild_Grade_High,
			// Token: 0x04009BBA RID: 39866
			Guild_Grade_Low,
			// Token: 0x04009BBB RID: 39867
			Guild_WeeklyPoint_High,
			// Token: 0x04009BBC RID: 39868
			Guild_WeeklyPoint_Low,
			// Token: 0x04009BBD RID: 39869
			Guild_TotalPoint_High,
			// Token: 0x04009BBE RID: 39870
			Guild_TotalPoint_Low,
			// Token: 0x04009BBF RID: 39871
			Favorite_First,
			// Token: 0x04009BC0 RID: 39872
			Favorite_Last,
			// Token: 0x04009BC1 RID: 39873
			TacticUpdatePossible_High,
			// Token: 0x04009BC2 RID: 39874
			TacticUpdatePossible_Low,
			// Token: 0x04009BC3 RID: 39875
			TacticUpdateLevel_High,
			// Token: 0x04009BC4 RID: 39876
			TacticUpdateLevel_Low,
			// Token: 0x04009BC5 RID: 39877
			CustomAscend1,
			// Token: 0x04009BC6 RID: 39878
			CustomDescend1,
			// Token: 0x04009BC7 RID: 39879
			CustomAscend2,
			// Token: 0x04009BC8 RID: 39880
			CustomDescend2,
			// Token: 0x04009BC9 RID: 39881
			CustomAscend3,
			// Token: 0x04009BCA RID: 39882
			CustomDescend3
		}

		// Token: 0x020013B7 RID: 5047
		public enum eUnitState
		{
			// Token: 0x04009BCC RID: 39884
			NONE,
			// Token: 0x04009BCD RID: 39885
			DUPLICATE,
			// Token: 0x04009BCE RID: 39886
			CITY_SET,
			// Token: 0x04009BCF RID: 39887
			CITY_MISSION,
			// Token: 0x04009BD0 RID: 39888
			WARFARE_BATCH,
			// Token: 0x04009BD1 RID: 39889
			DIVE_BATCH,
			// Token: 0x04009BD2 RID: 39890
			DECKED,
			// Token: 0x04009BD3 RID: 39891
			MAINUNIT,
			// Token: 0x04009BD4 RID: 39892
			LOCKED,
			// Token: 0x04009BD5 RID: 39893
			SEIZURE,
			// Token: 0x04009BD6 RID: 39894
			LOBBY_UNIT,
			// Token: 0x04009BD7 RID: 39895
			DUNGEON_RESTRICTED,
			// Token: 0x04009BD8 RID: 39896
			CHECKED,
			// Token: 0x04009BD9 RID: 39897
			LEAGUE_BANNED,
			// Token: 0x04009BDA RID: 39898
			LEAGUE_DECKED_LEFT,
			// Token: 0x04009BDB RID: 39899
			LEAGUE_DECKED_RIGHT,
			// Token: 0x04009BDC RID: 39900
			OFFICE_DORM_IN
		}

		// Token: 0x020013B8 RID: 5048
		public enum eWorldmapLeaderDataProcessType
		{
			// Token: 0x04009BDE RID: 39902
			Ignore,
			// Token: 0x04009BDF RID: 39903
			DisableOnMission,
			// Token: 0x04009BE0 RID: 39904
			DisableLeader,
			// Token: 0x04009BE1 RID: 39905
			DisableOnlyOtherCity
		}

		// Token: 0x020013B9 RID: 5049
		public struct UnitListOptions
		{
			// Token: 0x0600A693 RID: 42643 RVA: 0x0034745C File Offset: 0x0034565C
			public void MakeDuplicateUnitSet(NKMDeckIndex currentDeckIndex, long selectedUnitUID, NKMArmyData armyData)
			{
				NKM_DECK_TYPE nkm_DECK_TYPE = currentDeckIndex.m_eDeckType;
				bool flag = nkm_DECK_TYPE - NKM_DECK_TYPE.NDT_TRIM <= 1;
				if (this.setDuplicateUnitID == null)
				{
					this.setDuplicateUnitID = new HashSet<int>();
				}
				if (flag)
				{
					IReadOnlyList<NKMDeckData> deckList = armyData.GetDeckList(currentDeckIndex.m_eDeckType);
					for (int i = 0; i < deckList.Count; i++)
					{
						NKMDeckData nkmdeckData = deckList[i];
						if (nkmdeckData == null)
						{
							return;
						}
						if (nkmdeckData.m_ShipUID != selectedUnitUID)
						{
							NKMUnitData shipFromUID = armyData.GetShipFromUID(nkmdeckData.m_ShipUID);
							if (shipFromUID != null)
							{
								NKMUnitTempletBase shipTempletBase = NKMUnitManager.GetUnitTempletBase(shipFromUID.m_UnitID);
								if (shipTempletBase != null)
								{
									foreach (NKMUnitTempletBase nkmunitTempletBase in from e in NKMUnitTempletBase.Values
									where e.m_ShipGroupID == shipTempletBase.m_ShipGroupID
									select e)
									{
										this.setDuplicateUnitID.Add(nkmunitTempletBase.m_UnitID);
									}
								}
							}
						}
						for (int j = 0; j < nkmdeckData.m_listDeckUnitUID.Count; j++)
						{
							long num = nkmdeckData.m_listDeckUnitUID[j];
							if (num != 0L && selectedUnitUID != num)
							{
								NKMUnitData unitFromUID = armyData.GetUnitFromUID(num);
								if (unitFromUID != null)
								{
									this.setDuplicateUnitID.Add(unitFromUID.m_UnitID);
								}
							}
						}
					}
					return;
				}
				NKMDeckData deckData = armyData.GetDeckData(currentDeckIndex);
				if (deckData == null)
				{
					return;
				}
				if (deckData.m_ShipUID != selectedUnitUID)
				{
					NKMUnitData shipFromUID2 = armyData.GetShipFromUID(deckData.m_ShipUID);
					if (shipFromUID2 != null)
					{
						this.setDuplicateUnitID.Add(shipFromUID2.m_UnitID);
					}
				}
				for (int k = 0; k < deckData.m_listDeckUnitUID.Count; k++)
				{
					long num2 = deckData.m_listDeckUnitUID[k];
					if (num2 != 0L && selectedUnitUID != num2)
					{
						NKMUnitData unitFromUID2 = armyData.GetUnitFromUID(num2);
						if (unitFromUID2 != null)
						{
							this.setDuplicateUnitID.Add(unitFromUID2.m_UnitID);
						}
					}
				}
			}

			// Token: 0x04009BE2 RID: 39906
			public NKM_DECK_TYPE eDeckType;

			// Token: 0x04009BE3 RID: 39907
			public List<NKM_DECK_TYPE> lstDeckTypeOrder;

			// Token: 0x04009BE4 RID: 39908
			public HashSet<int> setExcludeUnitID;

			// Token: 0x04009BE5 RID: 39909
			public HashSet<int> setExcludeUnitBaseID;

			// Token: 0x04009BE6 RID: 39910
			public HashSet<int> setOnlyIncludeUnitID;

			// Token: 0x04009BE7 RID: 39911
			public HashSet<int> setOnlyIncludeUnitBaseID;

			// Token: 0x04009BE8 RID: 39912
			public HashSet<int> setDuplicateUnitID;

			// Token: 0x04009BE9 RID: 39913
			public HashSet<long> setExcludeUnitUID;

			// Token: 0x04009BEA RID: 39914
			public HashSet<NKCUnitSortSystem.eFilterOption> setOnlyIncludeFilterOption;

			// Token: 0x04009BEB RID: 39915
			public bool bExcludeLockedUnit;

			// Token: 0x04009BEC RID: 39916
			public bool bExcludeDeckedUnit;

			// Token: 0x04009BED RID: 39917
			public HashSet<NKCUnitSortSystem.eFilterOption> setFilterOption;

			// Token: 0x04009BEE RID: 39918
			public List<NKCUnitSortSystem.eSortOption> lstSortOption;

			// Token: 0x04009BEF RID: 39919
			public NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc PreemptiveSortFunc;

			// Token: 0x04009BF0 RID: 39920
			public Dictionary<NKCUnitSortSystem.eSortCategory, KeyValuePair<string, NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc>> lstCustomSortFunc;

			// Token: 0x04009BF1 RID: 39921
			public NKCUnitSortSystem.UnitListOptions.CustomFilterFunc AdditionalExcludeFilterFunc;

			// Token: 0x04009BF2 RID: 39922
			public List<NKCUnitSortSystem.eSortOption> lstDefaultSortOption;

			// Token: 0x04009BF3 RID: 39923
			public bool bHideDeckedUnit;

			// Token: 0x04009BF4 RID: 39924
			public bool bPushBackUnselectable;

			// Token: 0x04009BF5 RID: 39925
			public bool bIncludeUndeckableUnit;

			// Token: 0x04009BF6 RID: 39926
			public bool bIgnoreCityState;

			// Token: 0x04009BF7 RID: 39927
			public bool bIgnoreWorldMapLeader;

			// Token: 0x04009BF8 RID: 39928
			public bool bIncludeSeizure;

			// Token: 0x04009BF9 RID: 39929
			public bool bIgnoreMissionState;

			// Token: 0x04009BFA RID: 39930
			public bool bDescending;

			// Token: 0x04009BFB RID: 39931
			public bool bUseDeckedState;

			// Token: 0x04009BFC RID: 39932
			public bool bUseLockedState;

			// Token: 0x04009BFD RID: 39933
			public bool bUseLobbyState;

			// Token: 0x04009BFE RID: 39934
			public bool bUseDormInState;

			// Token: 0x04009BFF RID: 39935
			public NKCUnitSortSystem.UnitListOptions.CustomUnitStateFunc AdditionalUnitStateFunc;

			// Token: 0x02001A5B RID: 6747
			// (Invoke) Token: 0x0600BBB6 RID: 48054
			public delegate bool CustomFilterFunc(NKMUnitData unitData);

			// Token: 0x02001A5C RID: 6748
			// (Invoke) Token: 0x0600BBBA RID: 48058
			public delegate NKCUnitSortSystem.eUnitState CustomUnitStateFunc(NKMUnitData unitData);
		}

		// Token: 0x020013BA RID: 5050
		private class UnitInfoCache
		{
			// Token: 0x04009C00 RID: 39936
			public Dictionary<NKM_DECK_TYPE, byte> dicDeckIndex;

			// Token: 0x04009C01 RID: 39937
			public NKMWorldMapManager.WorldMapLeaderState CityState;

			// Token: 0x04009C02 RID: 39938
			public NKCUnitSortSystem.eUnitState UnitSlotState;

			// Token: 0x04009C03 RID: 39939
			public int Power;

			// Token: 0x04009C04 RID: 39940
			public int Attack;

			// Token: 0x04009C05 RID: 39941
			public int HP;

			// Token: 0x04009C06 RID: 39942
			public int Defense;

			// Token: 0x04009C07 RID: 39943
			public int Critical;

			// Token: 0x04009C08 RID: 39944
			public int Hit;

			// Token: 0x04009C09 RID: 39945
			public int Evade;

			// Token: 0x04009C0A RID: 39946
			public int ReduceSkillCollTime;

			// Token: 0x04009C0B RID: 39947
			public float EnhanceProgress;

			// Token: 0x04009C0C RID: 39948
			public float ScoutProgress;

			// Token: 0x04009C0D RID: 39949
			public int LimitBreakProgress;

			// Token: 0x04009C0E RID: 39950
			public int TacticUpdateProcess;

			// Token: 0x04009C0F RID: 39951
			public int Loyalty;
		}

		// Token: 0x020013BB RID: 5051
		public class NKCDataComparerer<T> : Comparer<T>
		{
			// Token: 0x0600A695 RID: 42645 RVA: 0x00347660 File Offset: 0x00345860
			public NKCDataComparerer(params NKCUnitSortSystem.NKCDataComparerer<T>.CompareFunc[] comparers)
			{
				foreach (NKCUnitSortSystem.NKCDataComparerer<T>.CompareFunc item in comparers)
				{
					this.m_lstFunc.Add(item);
				}
			}

			// Token: 0x0600A696 RID: 42646 RVA: 0x0034769E File Offset: 0x0034589E
			public void AddFunc(NKCUnitSortSystem.NKCDataComparerer<T>.CompareFunc func)
			{
				this.m_lstFunc.Add(func);
			}

			// Token: 0x0600A697 RID: 42647 RVA: 0x003476AC File Offset: 0x003458AC
			public override int Compare(T lhs, T rhs)
			{
				foreach (NKCUnitSortSystem.NKCDataComparerer<T>.CompareFunc compareFunc in this.m_lstFunc)
				{
					int num = compareFunc(lhs, rhs);
					if (num != 0)
					{
						return num;
					}
				}
				return 0;
			}

			// Token: 0x0600A698 RID: 42648 RVA: 0x0034770C File Offset: 0x0034590C
			public int GetComparerCount()
			{
				return this.m_lstFunc.Count;
			}

			// Token: 0x04009C10 RID: 39952
			private List<NKCUnitSortSystem.NKCDataComparerer<T>.CompareFunc> m_lstFunc = new List<NKCUnitSortSystem.NKCDataComparerer<T>.CompareFunc>();

			// Token: 0x02001A5E RID: 6750
			// (Invoke) Token: 0x0600BBC0 RID: 48064
			public delegate int CompareFunc(T lhs, T rhs);
		}

		// Token: 0x020013BC RID: 5052
		// (Invoke) Token: 0x0600A69A RID: 42650
		public delegate bool AutoSelectExtraFilter(NKMUnitData unitData);
	}
}
