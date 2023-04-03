using System;
using System.Collections.Generic;
using ClientPacket.WorldMap;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006B2 RID: 1714
	public abstract class NKCOperatorSortSystem
	{
		// Token: 0x060038A5 RID: 14501 RVA: 0x001250B0 File Offset: 0x001232B0
		public static NKCOperatorSortSystem.eSortCategory GetSortCategoryFromOption(NKCOperatorSortSystem.eSortOption option)
		{
			foreach (KeyValuePair<NKCOperatorSortSystem.eSortCategory, Tuple<NKCOperatorSortSystem.eSortOption, NKCOperatorSortSystem.eSortOption>> keyValuePair in NKCOperatorSortSystem.s_dicSortCategory)
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
			return NKCOperatorSortSystem.eSortCategory.None;
		}

		// Token: 0x060038A6 RID: 14502 RVA: 0x00125130 File Offset: 0x00123330
		public static NKCOperatorSortSystem.eSortOption GetSortOptionByCategory(NKCOperatorSortSystem.eSortCategory category, bool bDescending)
		{
			Tuple<NKCOperatorSortSystem.eSortOption, NKCOperatorSortSystem.eSortOption> tuple = NKCOperatorSortSystem.s_dicSortCategory[category];
			if (!bDescending)
			{
				return tuple.Item1;
			}
			return tuple.Item2;
		}

		// Token: 0x060038A7 RID: 14503 RVA: 0x0012515C File Offset: 0x0012335C
		public static bool IsDescending(NKCOperatorSortSystem.eSortOption option)
		{
			foreach (KeyValuePair<NKCOperatorSortSystem.eSortCategory, Tuple<NKCOperatorSortSystem.eSortOption, NKCOperatorSortSystem.eSortOption>> keyValuePair in NKCOperatorSortSystem.s_dicSortCategory)
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

		// Token: 0x060038A8 RID: 14504 RVA: 0x001251D0 File Offset: 0x001233D0
		public static NKCOperatorSortSystem.eSortOption GetInvertedAscendOption(NKCOperatorSortSystem.eSortOption option)
		{
			foreach (KeyValuePair<NKCOperatorSortSystem.eSortCategory, Tuple<NKCOperatorSortSystem.eSortOption, NKCOperatorSortSystem.eSortOption>> keyValuePair in NKCOperatorSortSystem.s_dicSortCategory)
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

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x060038A9 RID: 14505 RVA: 0x0012525C File Offset: 0x0012345C
		// (set) Token: 0x060038AA RID: 14506 RVA: 0x00125269 File Offset: 0x00123469
		public int m_PassiveSkillID
		{
			get
			{
				return this.m_Options.passiveSkillID;
			}
			set
			{
				this.m_Options.passiveSkillID = value;
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x060038AB RID: 14507 RVA: 0x00125277 File Offset: 0x00123477
		// (set) Token: 0x060038AC RID: 14508 RVA: 0x0012527F File Offset: 0x0012347F
		public NKCOperatorSortSystem.OperatorListOptions Options
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

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x060038AD RID: 14509 RVA: 0x00125288 File Offset: 0x00123488
		public List<NKMOperator> SortedOperatorList
		{
			get
			{
				if (this.m_lstCurrentOperatorList == null)
				{
					if (this.m_Options.setFilterOption == null)
					{
						this.m_Options.setFilterOption = new HashSet<NKCOperatorSortSystem.eFilterOption>();
						this.FilterList(this.m_Options.setFilterOption, this.m_Options.IsHasBuildOption(BUILD_OPTIONS.HIDE_DECKED_UNIT));
					}
					else
					{
						this.FilterList(this.m_Options.setFilterOption, this.m_Options.IsHasBuildOption(BUILD_OPTIONS.HIDE_DECKED_UNIT));
					}
				}
				return this.m_lstCurrentOperatorList;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x060038AE RID: 14510 RVA: 0x001252FC File Offset: 0x001234FC
		// (set) Token: 0x060038AF RID: 14511 RVA: 0x00125309 File Offset: 0x00123509
		public HashSet<NKCOperatorSortSystem.eFilterOption> FilterSet
		{
			get
			{
				return this.m_Options.setFilterOption;
			}
			set
			{
				this.FilterList(value, this.m_Options.IsHasBuildOption(BUILD_OPTIONS.HIDE_DECKED_UNIT));
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x060038B0 RID: 14512 RVA: 0x0012531E File Offset: 0x0012351E
		// (set) Token: 0x060038B1 RID: 14513 RVA: 0x0012532B File Offset: 0x0012352B
		public List<NKCOperatorSortSystem.eSortOption> lstSortOption
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

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x060038B2 RID: 14514 RVA: 0x0012533A File Offset: 0x0012353A
		// (set) Token: 0x060038B3 RID: 14515 RVA: 0x00125348 File Offset: 0x00123548
		public bool Descending
		{
			get
			{
				return this.m_Options.IsHasBuildOption(BUILD_OPTIONS.DESCENDING);
			}
			set
			{
				if (this.m_Options.lstSortOption != null)
				{
					this.m_Options.SetBuildOption(value, new BUILD_OPTIONS[]
					{
						BUILD_OPTIONS.DESCENDING
					});
					this.SortList(this.m_Options.lstSortOption, false);
					return;
				}
				this.m_Options.lstSortOption = NKCOperatorSortSystem.GetDefaultSortOptions(false, false);
				this.m_Options.SetBuildOption(value, new BUILD_OPTIONS[]
				{
					BUILD_OPTIONS.DESCENDING
				});
				this.SortList(this.m_Options.lstSortOption, false);
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x060038B4 RID: 14516 RVA: 0x001253C5 File Offset: 0x001235C5
		// (set) Token: 0x060038B5 RID: 14517 RVA: 0x001253D4 File Offset: 0x001235D4
		public bool bHideDeckedUnit
		{
			get
			{
				return this.m_Options.IsHasBuildOption(BUILD_OPTIONS.HIDE_DECKED_UNIT);
			}
			set
			{
				if (this.m_Options.setFilterOption != null)
				{
					this.FilterList(this.m_Options.setFilterOption, value);
					return;
				}
				this.m_Options.setFilterOption = new HashSet<NKCOperatorSortSystem.eFilterOption>();
				this.FilterList(this.m_Options.setFilterOption, value);
			}
		}

		// Token: 0x060038B6 RID: 14518
		protected abstract IEnumerable<NKMOperator> GetTargetOperatorList(NKMUserData userData);

		// Token: 0x060038B7 RID: 14519 RVA: 0x00125423 File Offset: 0x00123623
		private NKCOperatorSortSystem()
		{
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x00125444 File Offset: 0x00123644
		public NKCOperatorSortSystem(NKMUserData userData, NKCOperatorSortSystem.OperatorListOptions options)
		{
			this.m_Options = options;
			this.BuildUnitStateCache(userData, options.eDeckType);
			this.m_dicAllOperatorList = this.BuildFullUnitList(userData, this.GetTargetOperatorList(userData), options);
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x00125498 File Offset: 0x00123698
		public NKCOperatorSortSystem(NKMUserData userData, NKCOperatorSortSystem.OperatorListOptions options, IEnumerable<NKMOperator> lstOperatorData)
		{
			this.m_Options = options;
			this.BuildUnitStateCache(userData, lstOperatorData, options.eDeckType);
			this.m_dicAllOperatorList = this.BuildFullUnitList(userData, lstOperatorData, options);
		}

		// Token: 0x060038BA RID: 14522 RVA: 0x001254E8 File Offset: 0x001236E8
		public NKCOperatorSortSystem(NKMUserData userData, NKCOperatorSortSystem.OperatorListOptions options, bool local)
		{
			this.m_Options = options;
			if (local)
			{
				this.BuildLocalUnitStateCache(userData, options.eDeckType);
			}
			else
			{
				this.BuildUnitStateCache(userData, options.eDeckType);
			}
			this.m_dicAllOperatorList = this.BuildFullUnitList(userData, this.GetTargetOperatorList(userData), options);
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x0012554C File Offset: 0x0012374C
		public virtual void BuildFilterAndSortedList(HashSet<NKCOperatorSortSystem.eFilterOption> setfilterType, List<NKCOperatorSortSystem.eSortOption> lstSortOption, bool bHideDeckedOperator)
		{
			this.m_Options.SetBuildOption(bHideDeckedOperator, new BUILD_OPTIONS[]
			{
				BUILD_OPTIONS.HIDE_DECKED_UNIT
			});
			this.m_Options.setFilterOption = setfilterType;
			this.m_Options.lstSortOption = lstSortOption;
			this.FilterList(setfilterType, bHideDeckedOperator);
		}

		// Token: 0x060038BC RID: 14524 RVA: 0x00125584 File Offset: 0x00123784
		public void SetDeckIndexCache(long uid, NKMDeckIndex deckindex)
		{
			if (this.m_dicOperatorInfoCache.ContainsKey(uid))
			{
				this.m_dicOperatorInfoCache[uid].DeckIndex = deckindex;
				return;
			}
			NKCOperatorSortSystem.OperatorInfoCache operatorInfoCache = new NKCOperatorSortSystem.OperatorInfoCache();
			operatorInfoCache.DeckIndex = deckindex;
			this.m_dicOperatorInfoCache.Add(uid, operatorInfoCache);
		}

		// Token: 0x060038BD RID: 14525 RVA: 0x001255CC File Offset: 0x001237CC
		public NKMDeckIndex GetDeckIndexCache(long uid, bool bTargetDecktypeOnly = false)
		{
			if (!this.m_dicOperatorInfoCache.ContainsKey(uid))
			{
				return NKMDeckIndex.None;
			}
			if (!bTargetDecktypeOnly)
			{
				return this.m_dicOperatorInfoCache[uid].DeckIndex;
			}
			if (this.m_Options.eDeckType == this.m_dicOperatorInfoCache[uid].DeckIndex.m_eDeckType)
			{
				return this.m_dicOperatorInfoCache[uid].DeckIndex;
			}
			return NKMDeckIndex.None;
		}

		// Token: 0x060038BE RID: 14526 RVA: 0x0012563C File Offset: 0x0012383C
		private void SetUnitAttackCache(long uid, int atk)
		{
			if (this.m_dicOperatorInfoCache.ContainsKey(uid))
			{
				this.m_dicOperatorInfoCache[uid].Attack = atk;
				return;
			}
			NKCOperatorSortSystem.OperatorInfoCache operatorInfoCache = new NKCOperatorSortSystem.OperatorInfoCache();
			operatorInfoCache.Attack = atk;
			this.m_dicOperatorInfoCache.Add(uid, operatorInfoCache);
		}

		// Token: 0x060038BF RID: 14527 RVA: 0x00125684 File Offset: 0x00123884
		public int GetUnitAttackCache(long uid)
		{
			if (this.m_dicOperatorInfoCache.ContainsKey(uid))
			{
				return this.m_dicOperatorInfoCache[uid].Attack;
			}
			return 0;
		}

		// Token: 0x060038C0 RID: 14528 RVA: 0x001256A8 File Offset: 0x001238A8
		private void SetUnitHPCache(long uid, int hp)
		{
			if (this.m_dicOperatorInfoCache.ContainsKey(uid))
			{
				this.m_dicOperatorInfoCache[uid].HP = hp;
				return;
			}
			NKCOperatorSortSystem.OperatorInfoCache operatorInfoCache = new NKCOperatorSortSystem.OperatorInfoCache();
			operatorInfoCache.HP = hp;
			this.m_dicOperatorInfoCache.Add(uid, operatorInfoCache);
		}

		// Token: 0x060038C1 RID: 14529 RVA: 0x001256F0 File Offset: 0x001238F0
		public int GetUnitHPCache(long uid)
		{
			if (this.m_dicOperatorInfoCache.ContainsKey(uid))
			{
				return this.m_dicOperatorInfoCache[uid].HP;
			}
			return 0;
		}

		// Token: 0x060038C2 RID: 14530 RVA: 0x00125714 File Offset: 0x00123914
		private void SetUnitDefCache(long uid, int def)
		{
			if (this.m_dicOperatorInfoCache.ContainsKey(uid))
			{
				this.m_dicOperatorInfoCache[uid].Defense = def;
				return;
			}
			NKCOperatorSortSystem.OperatorInfoCache operatorInfoCache = new NKCOperatorSortSystem.OperatorInfoCache();
			operatorInfoCache.Defense = def;
			this.m_dicOperatorInfoCache.Add(uid, operatorInfoCache);
		}

		// Token: 0x060038C3 RID: 14531 RVA: 0x0012575C File Offset: 0x0012395C
		public int GetUnitDefCache(long uid)
		{
			if (this.m_dicOperatorInfoCache.ContainsKey(uid))
			{
				return this.m_dicOperatorInfoCache[uid].Defense;
			}
			return 0;
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x00125780 File Offset: 0x00123980
		private void SetUnitSkillCoolCache(long uid, int ReduceSkillColl)
		{
			if (this.m_dicOperatorInfoCache.ContainsKey(uid))
			{
				this.m_dicOperatorInfoCache[uid].ReduceSkillCollTime = ReduceSkillColl;
				return;
			}
			NKCOperatorSortSystem.OperatorInfoCache operatorInfoCache = new NKCOperatorSortSystem.OperatorInfoCache();
			operatorInfoCache.ReduceSkillCollTime = ReduceSkillColl;
			this.m_dicOperatorInfoCache.Add(uid, operatorInfoCache);
		}

		// Token: 0x060038C5 RID: 14533 RVA: 0x001257C8 File Offset: 0x001239C8
		public int GetUnitSkillCoolCache(long uid)
		{
			if (this.m_dicOperatorInfoCache.ContainsKey(uid))
			{
				return this.m_dicOperatorInfoCache[uid].ReduceSkillCollTime;
			}
			return 0;
		}

		// Token: 0x060038C6 RID: 14534 RVA: 0x001257EC File Offset: 0x001239EC
		private void SetUnitPowerCache(long uid, int Power)
		{
			if (this.m_dicOperatorInfoCache.ContainsKey(uid))
			{
				this.m_dicOperatorInfoCache[uid].Power = Power;
				return;
			}
			NKCOperatorSortSystem.OperatorInfoCache operatorInfoCache = new NKCOperatorSortSystem.OperatorInfoCache();
			operatorInfoCache.Power = Power;
			this.m_dicOperatorInfoCache.Add(uid, operatorInfoCache);
		}

		// Token: 0x060038C7 RID: 14535 RVA: 0x00125834 File Offset: 0x00123A34
		public int GetUnitPowerCache(long uid)
		{
			if (this.m_dicOperatorInfoCache.ContainsKey(uid))
			{
				return this.m_dicOperatorInfoCache[uid].Power;
			}
			return 0;
		}

		// Token: 0x060038C8 RID: 14536 RVA: 0x00125857 File Offset: 0x00123A57
		public NKMWorldMapManager.WorldMapLeaderState GetCityStateCache(long uid)
		{
			if (this.m_dicOperatorInfoCache.ContainsKey(uid))
			{
				return this.m_dicOperatorInfoCache[uid].CityState;
			}
			return NKMWorldMapManager.WorldMapLeaderState.None;
		}

		// Token: 0x060038C9 RID: 14537 RVA: 0x0012587C File Offset: 0x00123A7C
		private void SetUnitSlotState(long uid, NKCUnitSortSystem.eUnitState state)
		{
			if (this.m_dicOperatorInfoCache.ContainsKey(uid))
			{
				this.m_dicOperatorInfoCache[uid].UnitSlotState = state;
				return;
			}
			NKCOperatorSortSystem.OperatorInfoCache operatorInfoCache = new NKCOperatorSortSystem.OperatorInfoCache();
			operatorInfoCache.UnitSlotState = state;
			this.m_dicOperatorInfoCache.Add(uid, operatorInfoCache);
		}

		// Token: 0x060038CA RID: 14538 RVA: 0x001258C4 File Offset: 0x00123AC4
		public NKCUnitSortSystem.eUnitState GetUnitSlotState(long uid)
		{
			if (this.m_dicOperatorInfoCache.ContainsKey(uid))
			{
				return this.m_dicOperatorInfoCache[uid].UnitSlotState;
			}
			return NKCUnitSortSystem.eUnitState.NONE;
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x001258E8 File Offset: 0x00123AE8
		protected virtual void BuildUnitStateCache(NKMUserData userData, IEnumerable<NKMOperator> lstOperatorData, NKM_DECK_TYPE eNKM_DECK_TYPE)
		{
			this.m_dicOperatorInfoCache.Clear();
			this.m_dicDeckedOperatorIdCache.Clear();
			if (lstOperatorData == null)
			{
				return;
			}
			if (eNKM_DECK_TYPE == NKM_DECK_TYPE.NDT_NONE)
			{
				return;
			}
			if (userData != null)
			{
				NKMArmyData armyData = userData.m_ArmyData;
				foreach (KeyValuePair<NKMDeckIndex, NKMDeckData> keyValuePair in armyData.GetAllDecks())
				{
					NKMDeckData value = keyValuePair.Value;
					if (armyData.m_dicMyOperator.ContainsKey(value.m_OperatorUID))
					{
						if (this.m_dicOperatorInfoCache.ContainsKey(value.m_OperatorUID))
						{
							if (this.m_dicOperatorInfoCache[value.m_OperatorUID].DeckIndex.m_eDeckType != eNKM_DECK_TYPE)
							{
								this.SetDeckIndexCache(value.m_OperatorUID, keyValuePair.Key);
							}
						}
						else
						{
							this.SetDeckIndexCache(value.m_OperatorUID, keyValuePair.Key);
						}
					}
				}
			}
			foreach (NKMOperator nkmoperator in lstOperatorData)
			{
				bool unitTempletBase = NKMUnitManager.GetUnitTempletBase(nkmoperator.id) != null;
				NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(nkmoperator.id);
				if (unitTempletBase && unitStatTemplet != null)
				{
					NKMStatData nkmstatData = new NKMStatData();
					nkmstatData.Init();
					nkmstatData.MakeOperatorBaseStat(nkmoperator, unitStatTemplet.m_StatData);
					this.SetUnitAttackCache(nkmoperator.uid, (int)nkmstatData.GetStatBase(NKM_STAT_TYPE.NST_ATK));
					this.SetUnitHPCache(nkmoperator.uid, (int)nkmstatData.GetStatBase(NKM_STAT_TYPE.NST_HP));
					this.SetUnitDefCache(nkmoperator.uid, (int)nkmstatData.GetStatBase(NKM_STAT_TYPE.NST_DEF));
					this.SetUnitSkillCoolCache(nkmoperator.uid, (int)nkmstatData.GetStatBase(NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE));
				}
			}
		}

		// Token: 0x060038CC RID: 14540 RVA: 0x00125AAC File Offset: 0x00123CAC
		protected virtual void BuildUnitStateCache(NKMUserData userData, NKM_DECK_TYPE eNKM_DECK_TYPE)
		{
			this.m_dicOperatorInfoCache.Clear();
			this.m_dicDeckedOperatorIdCache.Clear();
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
				if (armyData.m_dicMyOperator.ContainsKey(value.m_OperatorUID))
				{
					if (this.m_dicOperatorInfoCache.ContainsKey(value.m_OperatorUID))
					{
						if (this.m_dicOperatorInfoCache[value.m_OperatorUID].DeckIndex.m_eDeckType != eNKM_DECK_TYPE)
						{
							this.SetDeckIndexCache(value.m_OperatorUID, keyValuePair.Key);
						}
					}
					else
					{
						this.SetDeckIndexCache(value.m_OperatorUID, keyValuePair.Key);
					}
				}
			}
			foreach (NKMOperator nkmoperator in armyData.m_dicMyOperator.Values)
			{
				NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(nkmoperator.id);
				if (unitStatTemplet != null)
				{
					NKMStatData nkmstatData = new NKMStatData();
					nkmstatData.Init();
					nkmstatData.MakeOperatorBaseStat(nkmoperator, unitStatTemplet.m_StatData);
					this.SetUnitPowerCache(nkmoperator.uid, nkmoperator.CalculateOperatorOperationPower());
					this.SetUnitAttackCache(nkmoperator.uid, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_ATK, nkmstatData));
					this.SetUnitHPCache(nkmoperator.uid, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_HP, nkmstatData));
					this.SetUnitDefCache(nkmoperator.uid, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_DEF, nkmstatData));
					this.SetUnitSkillCoolCache(nkmoperator.uid, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE, nkmstatData));
				}
			}
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x00125C8C File Offset: 0x00123E8C
		private void SetDeckedOperatorIdCache(int operatorId, int deckIndex)
		{
			if (!this.m_dicDeckedOperatorIdCache.ContainsKey(operatorId))
			{
				this.m_dicDeckedOperatorIdCache.Add(operatorId, new HashSet<int>());
			}
			this.m_dicDeckedOperatorIdCache[operatorId].Add(deckIndex);
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x00125CC0 File Offset: 0x00123EC0
		public bool IsDeckedOperatorId(int operatorId)
		{
			return this.m_dicDeckedOperatorIdCache.ContainsKey(operatorId) && this.m_dicDeckedOperatorIdCache[operatorId].Count > 0;
		}

		// Token: 0x060038CF RID: 14543 RVA: 0x00125CE8 File Offset: 0x00123EE8
		protected virtual void BuildLocalUnitStateCache(NKMUserData userData, NKM_DECK_TYPE eNKM_DECK_TYPE)
		{
			this.m_dicOperatorInfoCache.Clear();
			this.m_dicDeckedOperatorIdCache.Clear();
			if (userData == null)
			{
				return;
			}
			if (eNKM_DECK_TYPE == NKM_DECK_TYPE.NDT_NONE)
			{
				return;
			}
			Dictionary<int, NKMEventDeckData> allLocalDeckData = NKCLocalDeckDataManager.GetAllLocalDeckData();
			NKMArmyData armyData = userData.m_ArmyData;
			foreach (KeyValuePair<int, NKMEventDeckData> keyValuePair in allLocalDeckData)
			{
				long operatorUID = keyValuePair.Value.m_OperatorUID;
				if (armyData.m_dicMyOperator.ContainsKey(operatorUID))
				{
					NKMDeckIndex deckindex = new NKMDeckIndex(eNKM_DECK_TYPE, keyValuePair.Key);
					this.SetDeckIndexCache(operatorUID, deckindex);
				}
				NKMOperator operatorFromUId = armyData.GetOperatorFromUId(operatorUID);
				if (operatorFromUId != null)
				{
					this.SetDeckedOperatorIdCache(operatorFromUId.id, keyValuePair.Key);
				}
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(operatorFromUId);
				if (unitTempletBase != null && unitTempletBase.m_BaseUnitID > 0)
				{
					this.SetDeckedOperatorIdCache(unitTempletBase.m_BaseUnitID, keyValuePair.Key);
				}
			}
			foreach (NKMOperator nkmoperator in armyData.m_dicMyOperator.Values)
			{
				NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(nkmoperator.id);
				if (unitStatTemplet != null)
				{
					NKMStatData nkmstatData = new NKMStatData();
					nkmstatData.Init();
					nkmstatData.MakeOperatorBaseStat(nkmoperator, unitStatTemplet.m_StatData);
					this.SetUnitPowerCache(nkmoperator.uid, nkmoperator.CalculateOperatorOperationPower());
					this.SetUnitAttackCache(nkmoperator.uid, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_ATK, nkmstatData));
					this.SetUnitHPCache(nkmoperator.uid, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_HP, nkmstatData));
					this.SetUnitDefCache(nkmoperator.uid, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_DEF, nkmstatData));
					this.SetUnitSkillCoolCache(nkmoperator.uid, (int)NKMUnitStatManager.GetFinalStatForUIOutput(NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE, nkmstatData));
				}
			}
		}

		// Token: 0x060038D0 RID: 14544 RVA: 0x00125ED8 File Offset: 0x001240D8
		private Dictionary<long, NKMOperator> BuildFullUnitList(NKMUserData userData, IEnumerable<NKMOperator> lstTargetOperators, NKCOperatorSortSystem.OperatorListOptions options)
		{
			Dictionary<long, NKMOperator> dictionary = new Dictionary<long, NKMOperator>();
			HashSet<int> setOnlyIncludeOperatorID = options.setOnlyIncludeOperatorID;
			HashSet<int> setExcludeOperatorID = options.setExcludeOperatorID;
			foreach (NKMOperator nkmoperator in lstTargetOperators)
			{
				long uid = nkmoperator.uid;
				if ((options.AdditionalExcludeFilterFunc == null || options.AdditionalExcludeFilterFunc(nkmoperator)) && (options.setExcludeOperatorUID == null || !options.setExcludeOperatorUID.Contains(uid)) && (!options.IsHasBuildOption(BUILD_OPTIONS.EXCLUDE_DECKED_UNIT) || (this.GetDeckIndexCache(uid, false).m_eDeckType == NKM_DECK_TYPE.NDT_NONE && this.GetCityStateCache(uid) == NKMWorldMapManager.WorldMapLeaderState.None && !this.IsMainUnit(uid, userData))) && (!options.IsHasBuildOption(BUILD_OPTIONS.EXCLUDE_LOCKED_UNIT) || !nkmoperator.bLock) && (setOnlyIncludeOperatorID == null || setOnlyIncludeOperatorID.Contains(nkmoperator.id)) && (options.setOnlyIncludeFilterOption == null || this.CheckFilter(nkmoperator, options.setOnlyIncludeFilterOption)))
				{
					NKCUnitSortSystem.eUnitState state = NKCUnitSortSystem.eUnitState.NONE;
					if (userData != null)
					{
						NKMDeckIndex deckIndexCache = this.GetDeckIndexCache(uid, !this.m_Options.IsHasBuildOption(BUILD_OPTIONS.USE_DECKED_STATE));
						this.GetCityStateCache(uid);
						NKMDeckData deckData = userData.m_ArmyData.GetDeckData(deckIndexCache);
						if (options.setDuplicateOperatorID != null && options.setDuplicateOperatorID.Contains(nkmoperator.id))
						{
							state = NKCUnitSortSystem.eUnitState.DUPLICATE;
						}
						else if (deckData != null && deckData.GetState() == NKM_DECK_STATE.DECK_STATE_WARFARE)
						{
							state = NKCUnitSortSystem.eUnitState.WARFARE_BATCH;
						}
						else if (deckData != null && deckData.GetState() == NKM_DECK_STATE.DECK_STATE_DIVE)
						{
							state = NKCUnitSortSystem.eUnitState.DIVE_BATCH;
						}
						else if (!options.bIgnoreMissionState && !options.IsHasBuildOption(BUILD_OPTIONS.IGNORE_CITY_STATE) && deckData != null && deckData.GetState() == NKM_DECK_STATE.DECK_STATE_WORLDMAP_MISSION)
						{
							state = NKCUnitSortSystem.eUnitState.CITY_MISSION;
						}
						else if (options.IsHasBuildOption(BUILD_OPTIONS.USE_DECKED_STATE) && deckIndexCache.m_eDeckType != NKM_DECK_TYPE.NDT_NONE)
						{
							state = NKCUnitSortSystem.eUnitState.DECKED;
						}
						else if (options.IsHasBuildOption(BUILD_OPTIONS.USE_DECKED_STATE) && this.IsMainUnit(uid, userData))
						{
							state = NKCUnitSortSystem.eUnitState.MAINUNIT;
						}
						else if (options.IsHasBuildOption(BUILD_OPTIONS.USE_LOCKED_STATE) && nkmoperator.bLock)
						{
							state = NKCUnitSortSystem.eUnitState.LOCKED;
						}
						else if (options.IsHasBuildOption(BUILD_OPTIONS.USE_LOBBY_STATE) && this.IsMainUnit(uid, userData))
						{
							if (userData.GetBackgroundUnitIndex(uid) >= 0)
							{
								state = NKCUnitSortSystem.eUnitState.LOBBY_UNIT;
							}
						}
						else if (this.m_Options.AdditionalUnitStateFunc != null)
						{
							state = this.m_Options.AdditionalUnitStateFunc(nkmoperator);
						}
						else
						{
							state = NKCUnitSortSystem.eUnitState.NONE;
						}
					}
					this.SetUnitSlotState(uid, state);
					dictionary.Add(uid, nkmoperator);
				}
			}
			return dictionary;
		}

		// Token: 0x060038D1 RID: 14545 RVA: 0x00126164 File Offset: 0x00124364
		private bool IsMainUnit(long unitUID, NKMUserData userData)
		{
			return userData != null && userData.GetBackgroundUnitIndex(unitUID) >= 0;
		}

		// Token: 0x060038D2 RID: 14546 RVA: 0x00126178 File Offset: 0x00124378
		protected bool FilterData(NKMOperator operatorData, List<HashSet<NKCOperatorSortSystem.eFilterOption>> setFilter)
		{
			if (this.m_Options.IsHasBuildOption(BUILD_OPTIONS.HIDE_DECKED_UNIT))
			{
				if (!this.m_Options.IsHasBuildOption(BUILD_OPTIONS.IGNORE_CITY_STATE) && this.IsUnitIsCityLeaderOnMission(operatorData.uid))
				{
					return false;
				}
				if (this.GetDeckIndexCache(operatorData.uid, false).m_eDeckType == this.m_Options.eDeckType)
				{
					return false;
				}
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(operatorData.id);
			if (!this.m_Options.IsHasBuildOption(BUILD_OPTIONS.INCLUDE_UNDECKABLE_UNIT) && !NKMUnitManager.CanUnitUsedInDeck(unitTempletBase))
			{
				return false;
			}
			if (setFilter == null || setFilter.Count == 0)
			{
				return true;
			}
			for (int i = 0; i < setFilter.Count; i++)
			{
				if (!this.CheckFilter(operatorData, setFilter[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060038D3 RID: 14547 RVA: 0x00126228 File Offset: 0x00124428
		protected bool FilterData(NKMOperator operatorData, NKM_UNIT_STYLE_TYPE targetType)
		{
			if (this.m_Options.IsHasBuildOption(BUILD_OPTIONS.HIDE_DECKED_UNIT))
			{
				if (this.GetCityStateCache(operatorData.uid) != NKMWorldMapManager.WorldMapLeaderState.None)
				{
					return false;
				}
				if (this.GetDeckIndexCache(operatorData.uid, false).m_eDeckType == this.m_Options.eDeckType)
				{
					return false;
				}
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(operatorData.id);
			return (this.m_Options.IsHasBuildOption(BUILD_OPTIONS.INCLUDE_UNDECKABLE_UNIT) || NKMUnitManager.CanUnitUsedInDeck(unitTempletBase)) && (targetType == NKM_UNIT_STYLE_TYPE.NUST_INVALID || unitTempletBase.m_NKM_UNIT_STYLE_TYPE == targetType);
		}

		// Token: 0x060038D4 RID: 14548 RVA: 0x001262A8 File Offset: 0x001244A8
		protected bool IsOperatorSelectable(NKMOperator operatorData)
		{
			switch (this.GetUnitSlotState(operatorData.uid))
			{
			case NKCUnitSortSystem.eUnitState.NONE:
			case NKCUnitSortSystem.eUnitState.SEIZURE:
			case NKCUnitSortSystem.eUnitState.LOBBY_UNIT:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x060038D5 RID: 14549 RVA: 0x0012630C File Offset: 0x0012450C
		private bool CheckFilter(NKMOperator unitData, HashSet<NKCOperatorSortSystem.eFilterOption> setFilter)
		{
			foreach (NKCOperatorSortSystem.eFilterOption filterOption in setFilter)
			{
				if (this.CheckFilter(unitData, filterOption))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x00126364 File Offset: 0x00124564
		private bool CheckFilter(NKMOperator operatorData, NKCOperatorSortSystem.eFilterOption filterOption)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(operatorData.id);
			if (unitTempletBase == null)
			{
				Debug.LogError(string.Format("UnitTemplet Null! unitID : {0}", operatorData.id));
				return false;
			}
			switch (filterOption)
			{
			case NKCOperatorSortSystem.eFilterOption.Nothing:
				return false;
			case NKCOperatorSortSystem.eFilterOption.Everything:
				return true;
			case NKCOperatorSortSystem.eFilterOption.Rarily_SSR:
				if (unitTempletBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SSR)
				{
					return true;
				}
				break;
			case NKCOperatorSortSystem.eFilterOption.Rarily_SR:
				if (unitTempletBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SR)
				{
					return true;
				}
				break;
			case NKCOperatorSortSystem.eFilterOption.Rarily_R:
				if (unitTempletBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_R)
				{
					return true;
				}
				break;
			case NKCOperatorSortSystem.eFilterOption.Rarily_N:
				if (unitTempletBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_N)
				{
					return true;
				}
				break;
			case NKCOperatorSortSystem.eFilterOption.Level_1:
				if (operatorData.level == 1)
				{
					return true;
				}
				break;
			case NKCOperatorSortSystem.eFilterOption.Level_other:
				if (operatorData.level == 1)
				{
					return false;
				}
				if (operatorData.level != NKMCommonConst.OperatorConstTemplet.unitMaximumLevel)
				{
					return true;
				}
				break;
			case NKCOperatorSortSystem.eFilterOption.Level_Max:
				return operatorData.level == NKMCommonConst.OperatorConstTemplet.unitMaximumLevel;
			case NKCOperatorSortSystem.eFilterOption.Decked:
				if (this.GetDeckIndexCache(operatorData.uid, false) != NKMDeckIndex.None)
				{
					return true;
				}
				break;
			case NKCOperatorSortSystem.eFilterOption.NotDecked:
				if (this.GetDeckIndexCache(operatorData.uid, false) == NKMDeckIndex.None)
				{
					return true;
				}
				break;
			case NKCOperatorSortSystem.eFilterOption.Locked:
				if (operatorData.bLock)
				{
					return true;
				}
				break;
			case NKCOperatorSortSystem.eFilterOption.Unlocked:
				if (!operatorData.bLock)
				{
					return true;
				}
				break;
			case NKCOperatorSortSystem.eFilterOption.Have:
				if (NKCScenManager.CurrentUserData().m_ArmyData.GetOperatorCountByID(operatorData.id) > 0)
				{
					return true;
				}
				break;
			case NKCOperatorSortSystem.eFilterOption.NotHave:
				if (NKCScenManager.CurrentUserData().m_ArmyData.GetOperatorCountByID(operatorData.id) == 0)
				{
					return true;
				}
				break;
			case NKCOperatorSortSystem.eFilterOption.Collected:
				return NKCScenManager.CurrentUserData().m_ArmyData.IsCollectedUnit(operatorData.id);
			case NKCOperatorSortSystem.eFilterOption.NotCollected:
				return !NKCScenManager.CurrentUserData().m_ArmyData.IsCollectedUnit(operatorData.id);
			case NKCOperatorSortSystem.eFilterOption.PassiveSkill:
				if (this.m_Options.passiveSkillID > 0 && NKMTempletContainer<NKMOperatorSkillTemplet>.Find(this.m_Options.passiveSkillID) != null)
				{
					return operatorData.subSkill.id == this.m_Options.passiveSkillID;
				}
				break;
			}
			return false;
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x00126560 File Offset: 0x00124760
		public void FilterList(NKCOperatorSortSystem.eFilterOption filterOption, bool bHideDeckedUnit)
		{
			this.FilterList(new HashSet<NKCOperatorSortSystem.eFilterOption>
			{
				filterOption
			}, bHideDeckedUnit);
		}

		// Token: 0x060038D8 RID: 14552 RVA: 0x00126584 File Offset: 0x00124784
		public virtual void FilterList(HashSet<NKCOperatorSortSystem.eFilterOption> setFilter, bool bHideDeckedUnit)
		{
			this.m_Options.setFilterOption = setFilter;
			this.m_Options.SetBuildOption(bHideDeckedUnit, new BUILD_OPTIONS[]
			{
				BUILD_OPTIONS.HIDE_DECKED_UNIT
			});
			if (this.m_lstCurrentOperatorList == null)
			{
				this.m_lstCurrentOperatorList = new List<NKMOperator>();
			}
			this.m_lstCurrentOperatorList.Clear();
			List<HashSet<NKCOperatorSortSystem.eFilterOption>> setFilter2 = new List<HashSet<NKCOperatorSortSystem.eFilterOption>>();
			this.SetFilterCategory(setFilter, ref setFilter2);
			foreach (KeyValuePair<long, NKMOperator> keyValuePair in this.m_dicAllOperatorList)
			{
				NKMOperator value = keyValuePair.Value;
				if (this.FilterData(value, setFilter2))
				{
					this.m_lstCurrentOperatorList.Add(value);
				}
			}
			if (this.m_Options.lstSortOption != null)
			{
				this.SortList(this.m_Options.lstSortOption, true);
				return;
			}
			this.m_Options.lstSortOption = new List<NKCOperatorSortSystem.eSortOption>();
			this.SortList(this.m_Options.lstSortOption, true);
		}

		// Token: 0x060038D9 RID: 14553 RVA: 0x00126680 File Offset: 0x00124880
		private void SetFilterCategory(HashSet<NKCOperatorSortSystem.eFilterOption> setFilter, ref List<HashSet<NKCOperatorSortSystem.eFilterOption>> needFilterSet)
		{
			if (setFilter.Count == 0)
			{
				return;
			}
			for (int i = 0; i < NKCOperatorSortSystem.m_lstFilterCategory.Count; i++)
			{
				HashSet<NKCOperatorSortSystem.eFilterOption> hashSet = new HashSet<NKCOperatorSortSystem.eFilterOption>();
				foreach (NKCOperatorSortSystem.eFilterOption item in setFilter)
				{
					hashSet.Add(item);
				}
				hashSet.IntersectWith(NKCOperatorSortSystem.m_lstFilterCategory[i]);
				if (hashSet.Count > 0)
				{
					needFilterSet.Add(hashSet);
				}
			}
		}

		// Token: 0x060038DA RID: 14554 RVA: 0x00126718 File Offset: 0x00124918
		public void SortList(NKCOperatorSortSystem.eSortOption sortOption, bool bForce = false)
		{
			this.SortList(new List<NKCOperatorSortSystem.eSortOption>
			{
				sortOption
			}, bForce);
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x0012673C File Offset: 0x0012493C
		public void SortList(List<NKCOperatorSortSystem.eSortOption> lstSortOption, bool bForce = false)
		{
			if (this.m_lstCurrentOperatorList != null)
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
				this.SortOperatorDataList(ref this.m_lstCurrentOperatorList, lstSortOption);
				this.m_Options.lstSortOption = lstSortOption;
				return;
			}
			this.m_Options.lstSortOption = lstSortOption;
			if (this.m_Options.setFilterOption != null)
			{
				this.FilterList(this.m_Options.setFilterOption, this.m_Options.IsHasBuildOption(BUILD_OPTIONS.HIDE_DECKED_UNIT));
				return;
			}
			this.m_Options.setFilterOption = new HashSet<NKCOperatorSortSystem.eFilterOption>();
			this.FilterList(this.m_Options.setFilterOption, this.m_Options.IsHasBuildOption(BUILD_OPTIONS.HIDE_DECKED_UNIT));
		}

		// Token: 0x060038DC RID: 14556 RVA: 0x00126820 File Offset: 0x00124A20
		private void SortOperatorDataList(ref List<NKMOperator> lstOperatorData, List<NKCOperatorSortSystem.eSortOption> lstSortOption)
		{
			NKCUnitSortSystem.NKCDataComparerer<NKMOperator> nkcdataComparerer = new NKCUnitSortSystem.NKCDataComparerer<NKMOperator>(Array.Empty<NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc>());
			HashSet<NKCOperatorSortSystem.eSortCategory> hashSet = new HashSet<NKCOperatorSortSystem.eSortCategory>();
			if (this.m_Options.PreemptiveSortFunc != null)
			{
				nkcdataComparerer.AddFunc(this.m_Options.PreemptiveSortFunc);
			}
			if (this.m_Options.IsHasBuildOption(BUILD_OPTIONS.PUSHBACK_UNSELECTABLE))
			{
				nkcdataComparerer.AddFunc(new NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc(this.CompareByState));
			}
			foreach (NKCOperatorSortSystem.eSortOption eSortOption in lstSortOption)
			{
				if (eSortOption != NKCOperatorSortSystem.eSortOption.None)
				{
					NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc dataComparer = this.GetDataComparer(eSortOption);
					if (dataComparer != null)
					{
						nkcdataComparerer.AddFunc(dataComparer);
						hashSet.Add(NKCOperatorSortSystem.GetSortCategoryFromOption(eSortOption));
					}
				}
			}
			if (this.m_Options.lstDefaultSortOption != null)
			{
				foreach (NKCOperatorSortSystem.eSortOption eSortOption2 in this.m_Options.lstDefaultSortOption)
				{
					NKCOperatorSortSystem.eSortCategory sortCategoryFromOption = NKCOperatorSortSystem.GetSortCategoryFromOption(eSortOption2);
					if (!hashSet.Contains(sortCategoryFromOption))
					{
						nkcdataComparerer.AddFunc(this.GetDataComparer(eSortOption2));
						hashSet.Add(sortCategoryFromOption);
					}
				}
			}
			if (!hashSet.Contains(NKCOperatorSortSystem.eSortCategory.UID))
			{
				nkcdataComparerer.AddFunc(new NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc(this.CompareByUIDAscending));
			}
			lstOperatorData.Sort(nkcdataComparerer);
		}

		// Token: 0x060038DD RID: 14557 RVA: 0x00126978 File Offset: 0x00124B78
		private NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc GetDataComparer(NKCOperatorSortSystem.eSortOption sortOption)
		{
			switch (sortOption)
			{
			case NKCOperatorSortSystem.eSortOption.Level_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc(NKCOperatorSortSystem.CompareByLevelAscending);
			case NKCOperatorSortSystem.eSortOption.Level_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc(NKCOperatorSortSystem.CompareByLevelDescending);
			case NKCOperatorSortSystem.eSortOption.Rarity_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc(NKCOperatorSortSystem.CompareByRarityAscending);
			case NKCOperatorSortSystem.eSortOption.Rarity_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc(NKCOperatorSortSystem.CompareByRarityDescending);
			case NKCOperatorSortSystem.eSortOption.Power_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc(this.CompareByPowerAscending);
			case NKCOperatorSortSystem.eSortOption.Power_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc(this.CompareByPowerDescending);
			case NKCOperatorSortSystem.eSortOption.UID_Last:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc(this.CompareByUIDDescending);
			case NKCOperatorSortSystem.eSortOption.ID_First:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc(this.CompareByIDAscending);
			case NKCOperatorSortSystem.eSortOption.ID_Last:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc(this.CompareByIDDescending);
			case NKCOperatorSortSystem.eSortOption.IDX_First:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc(this.CompareByIdxAscending);
			case NKCOperatorSortSystem.eSortOption.IDX_Last:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc(this.CompareByIdxDescending);
			case NKCOperatorSortSystem.eSortOption.Attack_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc(this.CompareByAttackAscending);
			case NKCOperatorSortSystem.eSortOption.Attack_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc(this.CompareByAttackDescending);
			case NKCOperatorSortSystem.eSortOption.Health_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc(this.CompareByHealthAscending);
			case NKCOperatorSortSystem.eSortOption.Health_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc(this.CompareByHealthDescending);
			case NKCOperatorSortSystem.eSortOption.Unit_Defense_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc(this.CompareByDefenseAscending);
			case NKCOperatorSortSystem.eSortOption.Unit_Defense_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc(this.CompareByDefenseDescending);
			case NKCOperatorSortSystem.eSortOption.Unit_ReduceSkillCool_Low:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc(this.CompareByReduceSkillAscending);
			case NKCOperatorSortSystem.eSortOption.Unit_ReduceSkillCool_High:
				return new NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc(this.CompareByReduceSkillDescending);
			case NKCOperatorSortSystem.eSortOption.CustomAscend1:
			case NKCOperatorSortSystem.eSortOption.CustomAscend2:
			case NKCOperatorSortSystem.eSortOption.CustomAscend3:
				if (this.m_Options.lstCustomSortFunc.ContainsKey(NKCOperatorSortSystem.GetSortCategoryFromOption(sortOption)))
				{
					return (NKMOperator a, NKMOperator b) => this.m_Options.lstCustomSortFunc[NKCOperatorSortSystem.GetSortCategoryFromOption(sortOption)].Value(b, a);
				}
				return null;
			case NKCOperatorSortSystem.eSortOption.CustomDescend1:
			case NKCOperatorSortSystem.eSortOption.CustomDescend2:
			case NKCOperatorSortSystem.eSortOption.CustomDescend3:
				if (this.m_Options.lstCustomSortFunc.ContainsKey(NKCOperatorSortSystem.GetSortCategoryFromOption(sortOption)))
				{
					return this.m_Options.lstCustomSortFunc[NKCOperatorSortSystem.GetSortCategoryFromOption(sortOption)].Value;
				}
				return null;
			}
			return new NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc(this.CompareByUIDAscending);
		}

		// Token: 0x060038DE RID: 14558 RVA: 0x00126B84 File Offset: 0x00124D84
		private int CompareByState(NKMOperator lhs, NKMOperator rhs)
		{
			return this.IsOperatorSelectable(rhs).CompareTo(this.IsOperatorSelectable(lhs));
		}

		// Token: 0x060038DF RID: 14559 RVA: 0x00126BA8 File Offset: 0x00124DA8
		public static int CompareByLevelAscending(NKMOperator lhs, NKMOperator rhs)
		{
			if (lhs.level == rhs.level)
			{
				int num = lhs.exp.CompareTo(rhs.exp);
				if (num != 0)
				{
					return num;
				}
			}
			return lhs.level.CompareTo(rhs.level);
		}

		// Token: 0x060038E0 RID: 14560 RVA: 0x00126BEC File Offset: 0x00124DEC
		public static int CompareByLevelDescending(NKMOperator lhs, NKMOperator rhs)
		{
			if (lhs.level == rhs.level)
			{
				int num = rhs.exp.CompareTo(lhs.exp);
				if (num != 0)
				{
					return num;
				}
			}
			return rhs.level.CompareTo(lhs.level);
		}

		// Token: 0x060038E1 RID: 14561 RVA: 0x00126C30 File Offset: 0x00124E30
		public static int CompareByRarityAscending(NKMOperator lhs, NKMOperator rhs)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(lhs.id);
			NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(rhs.id);
			return unitTempletBase.m_NKM_UNIT_GRADE.CompareTo(unitTempletBase2.m_NKM_UNIT_GRADE);
		}

		// Token: 0x060038E2 RID: 14562 RVA: 0x00126C70 File Offset: 0x00124E70
		public static int CompareByRarityDescending(NKMOperator lhs, NKMOperator rhs)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(lhs.id);
			return NKMUnitManager.GetUnitTempletBase(rhs.id).m_NKM_UNIT_GRADE.CompareTo(unitTempletBase.m_NKM_UNIT_GRADE);
		}

		// Token: 0x060038E3 RID: 14563 RVA: 0x00126CB0 File Offset: 0x00124EB0
		private int CompareByPowerAscending(NKMOperator lhs, NKMOperator rhs)
		{
			return this.GetUnitPowerCache(lhs.uid).CompareTo(this.GetUnitPowerCache(rhs.uid));
		}

		// Token: 0x060038E4 RID: 14564 RVA: 0x00126CE0 File Offset: 0x00124EE0
		private int CompareByPowerDescending(NKMOperator lhs, NKMOperator rhs)
		{
			return this.GetUnitPowerCache(rhs.uid).CompareTo(this.GetUnitPowerCache(lhs.uid));
		}

		// Token: 0x060038E5 RID: 14565 RVA: 0x00126D0D File Offset: 0x00124F0D
		private int CompareByUIDAscending(NKMOperator lhs, NKMOperator rhs)
		{
			return lhs.uid.CompareTo(rhs.uid);
		}

		// Token: 0x060038E6 RID: 14566 RVA: 0x00126D20 File Offset: 0x00124F20
		private int CompareByUIDDescending(NKMOperator lhs, NKMOperator rhs)
		{
			return rhs.uid.CompareTo(lhs.uid);
		}

		// Token: 0x060038E7 RID: 14567 RVA: 0x00126D33 File Offset: 0x00124F33
		private int CompareByIDAscending(NKMOperator lhs, NKMOperator rhs)
		{
			return lhs.id.CompareTo(rhs.id);
		}

		// Token: 0x060038E8 RID: 14568 RVA: 0x00126D46 File Offset: 0x00124F46
		private int CompareByIDDescending(NKMOperator lhs, NKMOperator rhs)
		{
			return rhs.id.CompareTo(lhs.id);
		}

		// Token: 0x060038E9 RID: 14569 RVA: 0x00126D59 File Offset: 0x00124F59
		private int CompareByIdxAscending(NKMOperator lhs, NKMOperator rhs)
		{
			return NKCCollectionManager.GetEmployeeNumber(lhs.id).CompareTo(NKCCollectionManager.GetEmployeeNumber(rhs.id));
		}

		// Token: 0x060038EA RID: 14570 RVA: 0x00126D76 File Offset: 0x00124F76
		private int CompareByIdxDescending(NKMOperator lhs, NKMOperator rhs)
		{
			return NKCCollectionManager.GetEmployeeNumber(rhs.id).CompareTo(NKCCollectionManager.GetEmployeeNumber(lhs.id));
		}

		// Token: 0x060038EB RID: 14571 RVA: 0x00126D94 File Offset: 0x00124F94
		private int CompareByAttackAscending(NKMOperator lhs, NKMOperator rhs)
		{
			return this.GetUnitAttackCache(lhs.uid).CompareTo(this.GetUnitAttackCache(rhs.uid));
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x00126DC4 File Offset: 0x00124FC4
		private int CompareByAttackDescending(NKMOperator lhs, NKMOperator rhs)
		{
			return this.GetUnitAttackCache(rhs.uid).CompareTo(this.GetUnitAttackCache(lhs.uid));
		}

		// Token: 0x060038ED RID: 14573 RVA: 0x00126DF4 File Offset: 0x00124FF4
		private int CompareByHealthAscending(NKMOperator lhs, NKMOperator rhs)
		{
			return this.GetUnitHPCache(lhs.uid).CompareTo(this.GetUnitHPCache(rhs.uid));
		}

		// Token: 0x060038EE RID: 14574 RVA: 0x00126E24 File Offset: 0x00125024
		private int CompareByHealthDescending(NKMOperator lhs, NKMOperator rhs)
		{
			return this.GetUnitHPCache(rhs.uid).CompareTo(this.GetUnitHPCache(lhs.uid));
		}

		// Token: 0x060038EF RID: 14575 RVA: 0x00126E54 File Offset: 0x00125054
		private int CompareByDefenseAscending(NKMOperator lhs, NKMOperator rhs)
		{
			return this.GetUnitDefCache(lhs.uid).CompareTo(this.GetUnitDefCache(rhs.uid));
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x00126E84 File Offset: 0x00125084
		private int CompareByDefenseDescending(NKMOperator lhs, NKMOperator rhs)
		{
			return this.GetUnitDefCache(rhs.uid).CompareTo(this.GetUnitDefCache(lhs.uid));
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x00126EB4 File Offset: 0x001250B4
		private int CompareByReduceSkillAscending(NKMOperator lhs, NKMOperator rhs)
		{
			return this.GetUnitSkillCoolCache(lhs.uid).CompareTo(this.GetUnitSkillCoolCache(rhs.uid));
		}

		// Token: 0x060038F2 RID: 14578 RVA: 0x00126EE4 File Offset: 0x001250E4
		private int CompareByReduceSkillDescending(NKMOperator lhs, NKMOperator rhs)
		{
			return this.GetUnitSkillCoolCache(rhs.uid).CompareTo(this.GetUnitSkillCoolCache(lhs.uid));
		}

		// Token: 0x060038F3 RID: 14579 RVA: 0x00126F11 File Offset: 0x00125111
		public static string GetFilterName(NKM_UNIT_STYLE_TYPE type)
		{
			if (type == NKM_UNIT_STYLE_TYPE.NUST_INVALID)
			{
				return NKCUtilString.GET_STRING_FILTER_ALL;
			}
			return NKCUtilString.GetUnitStyleName(type);
		}

		// Token: 0x060038F4 RID: 14580 RVA: 0x00126F24 File Offset: 0x00125124
		public static string GetSortName(NKCOperatorSortSystem.eSortOption option)
		{
			switch (option)
			{
			default:
				return NKCUtilString.GET_STRING_SORT_LEVEL;
			case NKCOperatorSortSystem.eSortOption.Rarity_Low:
			case NKCOperatorSortSystem.eSortOption.Rarity_High:
				return NKCUtilString.GET_STRING_SORT_RARITY;
			case NKCOperatorSortSystem.eSortOption.Power_Low:
			case NKCOperatorSortSystem.eSortOption.Power_High:
				return NKCUtilString.GET_STRING_SORT_POPWER;
			case NKCOperatorSortSystem.eSortOption.UID_First:
			case NKCOperatorSortSystem.eSortOption.UID_Last:
				return NKCUtilString.GET_STRING_SORT_UID;
			case NKCOperatorSortSystem.eSortOption.IDX_First:
			case NKCOperatorSortSystem.eSortOption.IDX_Last:
				return NKCUtilString.GET_STRING_SORT_IDX;
			case NKCOperatorSortSystem.eSortOption.Attack_Low:
			case NKCOperatorSortSystem.eSortOption.Attack_High:
				return NKCUtilString.GET_STRING_SORT_ATTACK;
			case NKCOperatorSortSystem.eSortOption.Health_Low:
			case NKCOperatorSortSystem.eSortOption.Health_High:
				return NKCUtilString.GET_STRING_SORT_HEALTH;
			case NKCOperatorSortSystem.eSortOption.Unit_Defense_Low:
			case NKCOperatorSortSystem.eSortOption.Unit_Defense_High:
				return NKCUtilString.GET_STRING_SORT_DEFENSE;
			case NKCOperatorSortSystem.eSortOption.Unit_ReduceSkillCool_Low:
			case NKCOperatorSortSystem.eSortOption.Unit_ReduceSkillCool_High:
				return NKCUtilString.GET_STRING_OPERATOR_SKILL_COOL_REDUCE;
			case NKCOperatorSortSystem.eSortOption.CustomAscend1:
			case NKCOperatorSortSystem.eSortOption.CustomDescend1:
			case NKCOperatorSortSystem.eSortOption.CustomAscend2:
			case NKCOperatorSortSystem.eSortOption.CustomDescend2:
			case NKCOperatorSortSystem.eSortOption.CustomAscend3:
			case NKCOperatorSortSystem.eSortOption.CustomDescend3:
				return string.Empty;
			}
		}

		// Token: 0x060038F5 RID: 14581 RVA: 0x00126FDC File Offset: 0x001251DC
		public NKMOperator AutoSelect(HashSet<long> setExcludeUnitUid, NKCOperatorSortSystem.AutoSelectExtraFilter extrafilter = null)
		{
			List<NKMOperator> list = this.AutoSelect(setExcludeUnitUid, 1, extrafilter);
			if (list.Count > 0)
			{
				return list[0];
			}
			return null;
		}

		// Token: 0x060038F6 RID: 14582 RVA: 0x00127008 File Offset: 0x00125208
		public List<NKMOperator> AutoSelect(HashSet<long> setExcludeUnitUid, int count, NKCOperatorSortSystem.AutoSelectExtraFilter extrafilter = null)
		{
			List<NKMOperator> list = new List<NKMOperator>();
			int num = 0;
			while (num < this.SortedOperatorList.Count && list.Count < count)
			{
				NKMOperator nkmoperator = this.SortedOperatorList[num];
				if (nkmoperator != null && (setExcludeUnitUid == null || !setExcludeUnitUid.Contains(nkmoperator.uid)) && (extrafilter == null || extrafilter(nkmoperator)) && this.IsOperatorSelectable(nkmoperator))
				{
					list.Add(nkmoperator);
				}
				num++;
			}
			return list;
		}

		// Token: 0x060038F7 RID: 14583 RVA: 0x00127078 File Offset: 0x00125278
		public static List<NKCOperatorSortSystem.eSortOption> GetDefaultSortOptions(bool bIsCollection, bool bIsSelection = false)
		{
			if (!bIsCollection)
			{
				return NKCOperatorSortSystem.DEFAULT_OPERATOR_SORT_OPTION_LIST;
			}
			if (bIsSelection)
			{
				return NKCOperatorSortSystem.DEFAULT_UNIT_SELECTION_SORT_OPTION_LIST;
			}
			return NKCOperatorSortSystem.DEFAULT_UNIT_COLLECTION_SORT_OPTION_LIST;
		}

		// Token: 0x060038F8 RID: 14584 RVA: 0x00127094 File Offset: 0x00125294
		public static List<NKCOperatorSortSystem.eSortOption> AddDefaultSortOptions(List<NKCOperatorSortSystem.eSortOption> sortOptions, bool bIsCollection)
		{
			List<NKCOperatorSortSystem.eSortOption> defaultSortOptions = NKCOperatorSortSystem.GetDefaultSortOptions(bIsCollection, false);
			if (defaultSortOptions != null)
			{
				sortOptions.AddRange(defaultSortOptions);
			}
			return sortOptions;
		}

		// Token: 0x060038F9 RID: 14585 RVA: 0x001270B4 File Offset: 0x001252B4
		public static HashSet<NKCOperatorSortSystem.eFilterCategory> ConvertFilterCategory(HashSet<NKCUnitSortSystem.eFilterCategory> hashSet)
		{
			HashSet<NKCOperatorSortSystem.eFilterCategory> hashSet2 = new HashSet<NKCOperatorSortSystem.eFilterCategory>();
			foreach (NKCUnitSortSystem.eFilterCategory option in hashSet)
			{
				hashSet2.Add(NKCOperatorSortSystem.ConvertFilterCategory(option));
			}
			return hashSet2;
		}

		// Token: 0x060038FA RID: 14586 RVA: 0x00127110 File Offset: 0x00125310
		public static HashSet<NKCUnitSortSystem.eFilterCategory> ConvertFilterCategory(HashSet<NKCOperatorSortSystem.eFilterCategory> hashSet)
		{
			HashSet<NKCUnitSortSystem.eFilterCategory> hashSet2 = new HashSet<NKCUnitSortSystem.eFilterCategory>();
			foreach (NKCOperatorSortSystem.eFilterCategory option in hashSet)
			{
				hashSet2.Add(NKCOperatorSortSystem.ConvertFilterCategory(option));
			}
			return hashSet2;
		}

		// Token: 0x060038FB RID: 14587 RVA: 0x0012716C File Offset: 0x0012536C
		public static NKCOperatorSortSystem.eFilterCategory ConvertFilterCategory(NKCUnitSortSystem.eFilterCategory option)
		{
			foreach (Tuple<NKCOperatorSortSystem.eFilterCategory, NKCUnitSortSystem.eFilterCategory> tuple in NKCOperatorSortSystem.m_listFilterCategory)
			{
				if (tuple.Item2 == option)
				{
					return tuple.Item1;
				}
			}
			return NKCOperatorSortSystem.eFilterCategory.Rarity;
		}

		// Token: 0x060038FC RID: 14588 RVA: 0x001271CC File Offset: 0x001253CC
		public static NKCUnitSortSystem.eFilterCategory ConvertFilterCategory(NKCOperatorSortSystem.eFilterCategory option)
		{
			foreach (Tuple<NKCOperatorSortSystem.eFilterCategory, NKCUnitSortSystem.eFilterCategory> tuple in NKCOperatorSortSystem.m_listFilterCategory)
			{
				if (tuple.Item1 == option)
				{
					return tuple.Item2;
				}
			}
			return NKCUnitSortSystem.eFilterCategory.Rarity;
		}

		// Token: 0x060038FD RID: 14589 RVA: 0x0012722C File Offset: 0x0012542C
		public static HashSet<NKCOperatorSortSystem.eFilterOption> ConvertFilterOption(HashSet<NKCUnitSortSystem.eFilterOption> hashSet)
		{
			HashSet<NKCOperatorSortSystem.eFilterOption> hashSet2 = new HashSet<NKCOperatorSortSystem.eFilterOption>();
			foreach (NKCUnitSortSystem.eFilterOption option in hashSet)
			{
				hashSet2.Add(NKCOperatorSortSystem.ConvertFilterOption(option));
			}
			return hashSet2;
		}

		// Token: 0x060038FE RID: 14590 RVA: 0x00127288 File Offset: 0x00125488
		public static HashSet<NKCUnitSortSystem.eFilterOption> ConvertFilterOption(HashSet<NKCOperatorSortSystem.eFilterOption> hashSet)
		{
			HashSet<NKCUnitSortSystem.eFilterOption> hashSet2 = new HashSet<NKCUnitSortSystem.eFilterOption>();
			foreach (NKCOperatorSortSystem.eFilterOption option in hashSet)
			{
				hashSet2.Add(NKCOperatorSortSystem.ConvertFilterOption(option));
			}
			return hashSet2;
		}

		// Token: 0x060038FF RID: 14591 RVA: 0x001272E4 File Offset: 0x001254E4
		public static NKCOperatorSortSystem.eFilterOption ConvertFilterOption(NKCUnitSortSystem.eFilterOption option)
		{
			foreach (Tuple<NKCOperatorSortSystem.eFilterOption, NKCUnitSortSystem.eFilterOption> tuple in NKCOperatorSortSystem.m_listFilterOptions)
			{
				if (tuple.Item2 == option)
				{
					return tuple.Item1;
				}
			}
			return NKCOperatorSortSystem.eFilterOption.Nothing;
		}

		// Token: 0x06003900 RID: 14592 RVA: 0x00127344 File Offset: 0x00125544
		public static NKCUnitSortSystem.eFilterOption ConvertFilterOption(NKCOperatorSortSystem.eFilterOption option)
		{
			foreach (Tuple<NKCOperatorSortSystem.eFilterOption, NKCUnitSortSystem.eFilterOption> tuple in NKCOperatorSortSystem.m_listFilterOptions)
			{
				if (tuple.Item1 == option)
				{
					return tuple.Item2;
				}
			}
			return NKCUnitSortSystem.eFilterOption.Nothing;
		}

		// Token: 0x06003901 RID: 14593 RVA: 0x001273A4 File Offset: 0x001255A4
		public static HashSet<NKCOperatorSortSystem.eSortCategory> ConvertSortCategory(HashSet<NKCUnitSortSystem.eSortCategory> hashSet)
		{
			HashSet<NKCOperatorSortSystem.eSortCategory> hashSet2 = new HashSet<NKCOperatorSortSystem.eSortCategory>();
			foreach (NKCUnitSortSystem.eSortCategory option in hashSet)
			{
				hashSet2.Add(NKCOperatorSortSystem.ConvertSortCategory(option));
			}
			return hashSet2;
		}

		// Token: 0x06003902 RID: 14594 RVA: 0x00127400 File Offset: 0x00125600
		public static HashSet<NKCUnitSortSystem.eSortCategory> ConvertSortCategory(HashSet<NKCOperatorSortSystem.eSortCategory> hashSet)
		{
			HashSet<NKCUnitSortSystem.eSortCategory> hashSet2 = new HashSet<NKCUnitSortSystem.eSortCategory>();
			foreach (NKCOperatorSortSystem.eSortCategory option in hashSet)
			{
				hashSet2.Add(NKCOperatorSortSystem.ConvertSortCategory(option));
			}
			return hashSet2;
		}

		// Token: 0x06003903 RID: 14595 RVA: 0x0012745C File Offset: 0x0012565C
		public static NKCOperatorSortSystem.eSortCategory ConvertSortCategory(NKCUnitSortSystem.eSortCategory option)
		{
			foreach (Tuple<NKCOperatorSortSystem.eSortCategory, NKCUnitSortSystem.eSortCategory> tuple in NKCOperatorSortSystem.m_listSortCategory)
			{
				if (tuple.Item2 == option)
				{
					return tuple.Item1;
				}
			}
			return NKCOperatorSortSystem.eSortCategory.None;
		}

		// Token: 0x06003904 RID: 14596 RVA: 0x001274BC File Offset: 0x001256BC
		public static NKCUnitSortSystem.eSortCategory ConvertSortCategory(NKCOperatorSortSystem.eSortCategory option)
		{
			foreach (Tuple<NKCOperatorSortSystem.eSortCategory, NKCUnitSortSystem.eSortCategory> tuple in NKCOperatorSortSystem.m_listSortCategory)
			{
				if (tuple.Item1 == option)
				{
					return tuple.Item2;
				}
			}
			return NKCUnitSortSystem.eSortCategory.None;
		}

		// Token: 0x06003905 RID: 14597 RVA: 0x0012751C File Offset: 0x0012571C
		public static List<NKCOperatorSortSystem.eSortOption> ConvertSortOption(List<NKCUnitSortSystem.eSortOption> hashSet)
		{
			List<NKCOperatorSortSystem.eSortOption> list = new List<NKCOperatorSortSystem.eSortOption>();
			foreach (NKCUnitSortSystem.eSortOption option in hashSet)
			{
				list.Add(NKCOperatorSortSystem.ConvertSortOption(option));
			}
			return list;
		}

		// Token: 0x06003906 RID: 14598 RVA: 0x00127578 File Offset: 0x00125778
		public static HashSet<NKCOperatorSortSystem.eSortOption> ConvertSortOption(HashSet<NKCUnitSortSystem.eSortOption> hashSet)
		{
			HashSet<NKCOperatorSortSystem.eSortOption> hashSet2 = new HashSet<NKCOperatorSortSystem.eSortOption>();
			foreach (NKCUnitSortSystem.eSortOption option in hashSet)
			{
				hashSet2.Add(NKCOperatorSortSystem.ConvertSortOption(option));
			}
			return hashSet2;
		}

		// Token: 0x06003907 RID: 14599 RVA: 0x001275D4 File Offset: 0x001257D4
		public static HashSet<NKCUnitSortSystem.eSortOption> ConvertSortOption(HashSet<NKCOperatorSortSystem.eSortOption> hashSet)
		{
			HashSet<NKCUnitSortSystem.eSortOption> hashSet2 = new HashSet<NKCUnitSortSystem.eSortOption>();
			foreach (NKCOperatorSortSystem.eSortOption option in hashSet)
			{
				hashSet2.Add(NKCOperatorSortSystem.ConvertSortOption(option));
			}
			return hashSet2;
		}

		// Token: 0x06003908 RID: 14600 RVA: 0x00127630 File Offset: 0x00125830
		public static List<NKCUnitSortSystem.eSortOption> ConvertSortOption(List<NKCOperatorSortSystem.eSortOption> hashSet)
		{
			List<NKCUnitSortSystem.eSortOption> list = new List<NKCUnitSortSystem.eSortOption>();
			foreach (NKCOperatorSortSystem.eSortOption option in hashSet)
			{
				list.Add(NKCOperatorSortSystem.ConvertSortOption(option));
			}
			return list;
		}

		// Token: 0x06003909 RID: 14601 RVA: 0x0012768C File Offset: 0x0012588C
		public static NKCOperatorSortSystem.eSortOption ConvertSortOption(NKCUnitSortSystem.eSortOption option)
		{
			foreach (Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption> tuple in NKCOperatorSortSystem.m_listSortOptions)
			{
				if (tuple.Item2 == option)
				{
					return tuple.Item1;
				}
			}
			return NKCOperatorSortSystem.eSortOption.None;
		}

		// Token: 0x0600390A RID: 14602 RVA: 0x001276EC File Offset: 0x001258EC
		public static NKCUnitSortSystem.eSortOption ConvertSortOption(NKCOperatorSortSystem.eSortOption option)
		{
			foreach (Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption> tuple in NKCOperatorSortSystem.m_listSortOptions)
			{
				if (tuple.Item1 == option)
				{
					return tuple.Item2;
				}
			}
			return NKCUnitSortSystem.eSortOption.None;
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x0012774C File Offset: 0x0012594C
		public static List<NKCOperatorSortSystem.eSortOption> ChangeAscend(List<NKCOperatorSortSystem.eSortOption> targetList)
		{
			List<NKCOperatorSortSystem.eSortOption> list = new List<NKCOperatorSortSystem.eSortOption>(targetList);
			if (list == null || list.Count == 0)
			{
				return list;
			}
			list[0] = NKCOperatorSortSystem.GetInvertedAscendOption(list[0]);
			return list;
		}

		// Token: 0x0600390C RID: 14604 RVA: 0x00127781 File Offset: 0x00125981
		public List<NKMOperator> GetCurrentOperatorList()
		{
			return this.m_lstCurrentOperatorList;
		}

		// Token: 0x0600390D RID: 14605 RVA: 0x0012778C File Offset: 0x0012598C
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

		// Token: 0x0600390E RID: 14606 RVA: 0x00127810 File Offset: 0x00125A10
		public static HashSet<NKCOperatorSortSystem.eFilterCategory> MakeDefaultFilterCategory(NKCOperatorSortSystem.FILTER_OPEN_TYPE filterOpenType)
		{
			HashSet<NKCOperatorSortSystem.eFilterCategory> hashSet = new HashSet<NKCOperatorSortSystem.eFilterCategory>();
			hashSet.Add(NKCOperatorSortSystem.eFilterCategory.Rarity);
			switch (filterOpenType)
			{
			case NKCOperatorSortSystem.FILTER_OPEN_TYPE.NORMAL:
				hashSet.Add(NKCOperatorSortSystem.eFilterCategory.Level);
				hashSet.Add(NKCOperatorSortSystem.eFilterCategory.Decked);
				hashSet.Add(NKCOperatorSortSystem.eFilterCategory.Locked);
				hashSet.Add(NKCOperatorSortSystem.eFilterCategory.PassiveSkill);
				break;
			case NKCOperatorSortSystem.FILTER_OPEN_TYPE.COLLECTION:
				hashSet.Add(NKCOperatorSortSystem.eFilterCategory.Collected);
				break;
			case NKCOperatorSortSystem.FILTER_OPEN_TYPE.SELECTION:
				hashSet.Add(NKCOperatorSortSystem.eFilterCategory.Have);
				break;
			}
			return hashSet;
		}

		// Token: 0x040034E8 RID: 13544
		private static readonly Dictionary<NKCOperatorSortSystem.eSortCategory, Tuple<NKCOperatorSortSystem.eSortOption, NKCOperatorSortSystem.eSortOption>> s_dicSortCategory = new Dictionary<NKCOperatorSortSystem.eSortCategory, Tuple<NKCOperatorSortSystem.eSortOption, NKCOperatorSortSystem.eSortOption>>
		{
			{
				NKCOperatorSortSystem.eSortCategory.None,
				new Tuple<NKCOperatorSortSystem.eSortOption, NKCOperatorSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.None, NKCOperatorSortSystem.eSortOption.None)
			},
			{
				NKCOperatorSortSystem.eSortCategory.Level,
				new Tuple<NKCOperatorSortSystem.eSortOption, NKCOperatorSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.Level_Low, NKCOperatorSortSystem.eSortOption.Level_High)
			},
			{
				NKCOperatorSortSystem.eSortCategory.Rarity,
				new Tuple<NKCOperatorSortSystem.eSortOption, NKCOperatorSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.Rarity_Low, NKCOperatorSortSystem.eSortOption.Rarity_High)
			},
			{
				NKCOperatorSortSystem.eSortCategory.UnitPower,
				new Tuple<NKCOperatorSortSystem.eSortOption, NKCOperatorSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.Power_Low, NKCOperatorSortSystem.eSortOption.Power_High)
			},
			{
				NKCOperatorSortSystem.eSortCategory.UID,
				new Tuple<NKCOperatorSortSystem.eSortOption, NKCOperatorSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.UID_First, NKCOperatorSortSystem.eSortOption.UID_Last)
			},
			{
				NKCOperatorSortSystem.eSortCategory.ID,
				new Tuple<NKCOperatorSortSystem.eSortOption, NKCOperatorSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.ID_First, NKCOperatorSortSystem.eSortOption.ID_Last)
			},
			{
				NKCOperatorSortSystem.eSortCategory.IDX,
				new Tuple<NKCOperatorSortSystem.eSortOption, NKCOperatorSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.IDX_First, NKCOperatorSortSystem.eSortOption.IDX_Last)
			},
			{
				NKCOperatorSortSystem.eSortCategory.UnitAttack,
				new Tuple<NKCOperatorSortSystem.eSortOption, NKCOperatorSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.Attack_Low, NKCOperatorSortSystem.eSortOption.Attack_High)
			},
			{
				NKCOperatorSortSystem.eSortCategory.UnitHealth,
				new Tuple<NKCOperatorSortSystem.eSortOption, NKCOperatorSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.Health_Low, NKCOperatorSortSystem.eSortOption.Health_High)
			},
			{
				NKCOperatorSortSystem.eSortCategory.UnitDefense,
				new Tuple<NKCOperatorSortSystem.eSortOption, NKCOperatorSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.Unit_Defense_Low, NKCOperatorSortSystem.eSortOption.Unit_Defense_High)
			},
			{
				NKCOperatorSortSystem.eSortCategory.UnitReduceSkillCool,
				new Tuple<NKCOperatorSortSystem.eSortOption, NKCOperatorSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.Unit_ReduceSkillCool_Low, NKCOperatorSortSystem.eSortOption.Unit_ReduceSkillCool_High)
			},
			{
				NKCOperatorSortSystem.eSortCategory.Custom1,
				new Tuple<NKCOperatorSortSystem.eSortOption, NKCOperatorSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.CustomAscend1, NKCOperatorSortSystem.eSortOption.CustomDescend1)
			},
			{
				NKCOperatorSortSystem.eSortCategory.Custom2,
				new Tuple<NKCOperatorSortSystem.eSortOption, NKCOperatorSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.CustomAscend2, NKCOperatorSortSystem.eSortOption.CustomDescend2)
			},
			{
				NKCOperatorSortSystem.eSortCategory.Custom3,
				new Tuple<NKCOperatorSortSystem.eSortOption, NKCOperatorSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.CustomAscend3, NKCOperatorSortSystem.eSortOption.CustomDescend3)
			}
		};

		// Token: 0x040034E9 RID: 13545
		private static readonly List<NKCOperatorSortSystem.eSortOption> DEFAULT_OPERATOR_SORT_OPTION_LIST = new List<NKCOperatorSortSystem.eSortOption>
		{
			NKCOperatorSortSystem.eSortOption.Level_High,
			NKCOperatorSortSystem.eSortOption.Power_High,
			NKCOperatorSortSystem.eSortOption.Level_High,
			NKCOperatorSortSystem.eSortOption.Rarity_High,
			NKCOperatorSortSystem.eSortOption.UID_Last
		};

		// Token: 0x040034EA RID: 13546
		private static readonly List<NKCOperatorSortSystem.eSortOption> DEFAULT_UNIT_SELECTION_SORT_OPTION_LIST = new List<NKCOperatorSortSystem.eSortOption>
		{
			NKCOperatorSortSystem.eSortOption.Rarity_High,
			NKCOperatorSortSystem.eSortOption.IDX_First
		};

		// Token: 0x040034EB RID: 13547
		private static readonly List<NKCOperatorSortSystem.eSortOption> DEFAULT_UNIT_COLLECTION_SORT_OPTION_LIST = new List<NKCOperatorSortSystem.eSortOption>
		{
			NKCOperatorSortSystem.eSortOption.IDX_First
		};

		// Token: 0x040034EC RID: 13548
		private static readonly HashSet<NKCOperatorSortSystem.eFilterOption> m_setFilterCategory_Rarity = new HashSet<NKCOperatorSortSystem.eFilterOption>
		{
			NKCOperatorSortSystem.eFilterOption.Rarily_SSR,
			NKCOperatorSortSystem.eFilterOption.Rarily_SR,
			NKCOperatorSortSystem.eFilterOption.Rarily_R,
			NKCOperatorSortSystem.eFilterOption.Rarily_N
		};

		// Token: 0x040034ED RID: 13549
		private static readonly HashSet<NKCOperatorSortSystem.eFilterOption> m_setFilterCategory_Level = new HashSet<NKCOperatorSortSystem.eFilterOption>
		{
			NKCOperatorSortSystem.eFilterOption.Level_1,
			NKCOperatorSortSystem.eFilterOption.Level_other,
			NKCOperatorSortSystem.eFilterOption.Level_Max
		};

		// Token: 0x040034EE RID: 13550
		private static readonly HashSet<NKCOperatorSortSystem.eFilterOption> m_setFilterCategory_Decked = new HashSet<NKCOperatorSortSystem.eFilterOption>
		{
			NKCOperatorSortSystem.eFilterOption.Decked,
			NKCOperatorSortSystem.eFilterOption.NotDecked
		};

		// Token: 0x040034EF RID: 13551
		private static readonly HashSet<NKCOperatorSortSystem.eFilterOption> m_setFilterCategory_Locked = new HashSet<NKCOperatorSortSystem.eFilterOption>
		{
			NKCOperatorSortSystem.eFilterOption.Locked,
			NKCOperatorSortSystem.eFilterOption.Unlocked
		};

		// Token: 0x040034F0 RID: 13552
		private static readonly HashSet<NKCOperatorSortSystem.eFilterOption> m_setFilterCategory_Have = new HashSet<NKCOperatorSortSystem.eFilterOption>
		{
			NKCOperatorSortSystem.eFilterOption.Have,
			NKCOperatorSortSystem.eFilterOption.NotHave
		};

		// Token: 0x040034F1 RID: 13553
		private static readonly HashSet<NKCOperatorSortSystem.eFilterOption> m_setFilterCategory_Collected = new HashSet<NKCOperatorSortSystem.eFilterOption>
		{
			NKCOperatorSortSystem.eFilterOption.Collected,
			NKCOperatorSortSystem.eFilterOption.NotCollected
		};

		// Token: 0x040034F2 RID: 13554
		private static readonly HashSet<NKCOperatorSortSystem.eFilterOption> m_setFilterCategory_PassiveSkill = new HashSet<NKCOperatorSortSystem.eFilterOption>
		{
			NKCOperatorSortSystem.eFilterOption.PassiveSkill
		};

		// Token: 0x040034F3 RID: 13555
		private static readonly List<HashSet<NKCOperatorSortSystem.eFilterOption>> m_lstFilterCategory = new List<HashSet<NKCOperatorSortSystem.eFilterOption>>
		{
			NKCOperatorSortSystem.m_setFilterCategory_Rarity,
			NKCOperatorSortSystem.m_setFilterCategory_Level,
			NKCOperatorSortSystem.m_setFilterCategory_Decked,
			NKCOperatorSortSystem.m_setFilterCategory_Locked,
			NKCOperatorSortSystem.m_setFilterCategory_Have,
			NKCOperatorSortSystem.m_setFilterCategory_Collected,
			NKCOperatorSortSystem.m_setFilterCategory_PassiveSkill
		};

		// Token: 0x040034F4 RID: 13556
		public static readonly HashSet<NKCOperatorSortSystem.eFilterCategory> setDefaultOperatorFilterCategory = new HashSet<NKCOperatorSortSystem.eFilterCategory>
		{
			NKCOperatorSortSystem.eFilterCategory.Rarity,
			NKCOperatorSortSystem.eFilterCategory.Locked
		};

		// Token: 0x040034F5 RID: 13557
		public static readonly HashSet<NKCOperatorSortSystem.eSortCategory> setDefaultOperatorSortCategory = new HashSet<NKCOperatorSortSystem.eSortCategory>
		{
			NKCOperatorSortSystem.eSortCategory.Level,
			NKCOperatorSortSystem.eSortCategory.Rarity,
			NKCOperatorSortSystem.eSortCategory.UID,
			NKCOperatorSortSystem.eSortCategory.UnitPower,
			NKCOperatorSortSystem.eSortCategory.UnitAttack,
			NKCOperatorSortSystem.eSortCategory.UnitHealth,
			NKCOperatorSortSystem.eSortCategory.UnitDefense,
			NKCOperatorSortSystem.eSortCategory.UnitReduceSkillCool
		};

		// Token: 0x040034F6 RID: 13558
		protected NKCOperatorSortSystem.OperatorListOptions m_Options;

		// Token: 0x040034F7 RID: 13559
		protected Dictionary<long, NKMOperator> m_dicAllOperatorList;

		// Token: 0x040034F8 RID: 13560
		protected List<NKMOperator> m_lstCurrentOperatorList;

		// Token: 0x040034F9 RID: 13561
		private Dictionary<long, NKCOperatorSortSystem.OperatorInfoCache> m_dicOperatorInfoCache = new Dictionary<long, NKCOperatorSortSystem.OperatorInfoCache>();

		// Token: 0x040034FA RID: 13562
		private Dictionary<int, HashSet<int>> m_dicDeckedOperatorIdCache = new Dictionary<int, HashSet<int>>();

		// Token: 0x040034FB RID: 13563
		public static List<Tuple<NKCOperatorSortSystem.eFilterCategory, NKCUnitSortSystem.eFilterCategory>> m_listFilterCategory = new List<Tuple<NKCOperatorSortSystem.eFilterCategory, NKCUnitSortSystem.eFilterCategory>>
		{
			new Tuple<NKCOperatorSortSystem.eFilterCategory, NKCUnitSortSystem.eFilterCategory>(NKCOperatorSortSystem.eFilterCategory.Rarity, NKCUnitSortSystem.eFilterCategory.Rarity),
			new Tuple<NKCOperatorSortSystem.eFilterCategory, NKCUnitSortSystem.eFilterCategory>(NKCOperatorSortSystem.eFilterCategory.Level, NKCUnitSortSystem.eFilterCategory.Level),
			new Tuple<NKCOperatorSortSystem.eFilterCategory, NKCUnitSortSystem.eFilterCategory>(NKCOperatorSortSystem.eFilterCategory.Decked, NKCUnitSortSystem.eFilterCategory.Decked),
			new Tuple<NKCOperatorSortSystem.eFilterCategory, NKCUnitSortSystem.eFilterCategory>(NKCOperatorSortSystem.eFilterCategory.Locked, NKCUnitSortSystem.eFilterCategory.Locked),
			new Tuple<NKCOperatorSortSystem.eFilterCategory, NKCUnitSortSystem.eFilterCategory>(NKCOperatorSortSystem.eFilterCategory.Have, NKCUnitSortSystem.eFilterCategory.Have),
			new Tuple<NKCOperatorSortSystem.eFilterCategory, NKCUnitSortSystem.eFilterCategory>(NKCOperatorSortSystem.eFilterCategory.Collected, NKCUnitSortSystem.eFilterCategory.Collected)
		};

		// Token: 0x040034FC RID: 13564
		public static List<Tuple<NKCOperatorSortSystem.eFilterOption, NKCUnitSortSystem.eFilterOption>> m_listFilterOptions = new List<Tuple<NKCOperatorSortSystem.eFilterOption, NKCUnitSortSystem.eFilterOption>>
		{
			new Tuple<NKCOperatorSortSystem.eFilterOption, NKCUnitSortSystem.eFilterOption>(NKCOperatorSortSystem.eFilterOption.Everything, NKCUnitSortSystem.eFilterOption.Everything),
			new Tuple<NKCOperatorSortSystem.eFilterOption, NKCUnitSortSystem.eFilterOption>(NKCOperatorSortSystem.eFilterOption.Nothing, NKCUnitSortSystem.eFilterOption.Nothing),
			new Tuple<NKCOperatorSortSystem.eFilterOption, NKCUnitSortSystem.eFilterOption>(NKCOperatorSortSystem.eFilterOption.Rarily_SSR, NKCUnitSortSystem.eFilterOption.Rarily_SSR),
			new Tuple<NKCOperatorSortSystem.eFilterOption, NKCUnitSortSystem.eFilterOption>(NKCOperatorSortSystem.eFilterOption.Rarily_SR, NKCUnitSortSystem.eFilterOption.Rarily_SR),
			new Tuple<NKCOperatorSortSystem.eFilterOption, NKCUnitSortSystem.eFilterOption>(NKCOperatorSortSystem.eFilterOption.Rarily_R, NKCUnitSortSystem.eFilterOption.Rarily_R),
			new Tuple<NKCOperatorSortSystem.eFilterOption, NKCUnitSortSystem.eFilterOption>(NKCOperatorSortSystem.eFilterOption.Rarily_N, NKCUnitSortSystem.eFilterOption.Rarily_N),
			new Tuple<NKCOperatorSortSystem.eFilterOption, NKCUnitSortSystem.eFilterOption>(NKCOperatorSortSystem.eFilterOption.Level_1, NKCUnitSortSystem.eFilterOption.Level_1),
			new Tuple<NKCOperatorSortSystem.eFilterOption, NKCUnitSortSystem.eFilterOption>(NKCOperatorSortSystem.eFilterOption.Level_other, NKCUnitSortSystem.eFilterOption.Level_other),
			new Tuple<NKCOperatorSortSystem.eFilterOption, NKCUnitSortSystem.eFilterOption>(NKCOperatorSortSystem.eFilterOption.Level_Max, NKCUnitSortSystem.eFilterOption.Level_Max),
			new Tuple<NKCOperatorSortSystem.eFilterOption, NKCUnitSortSystem.eFilterOption>(NKCOperatorSortSystem.eFilterOption.Decked, NKCUnitSortSystem.eFilterOption.Decked),
			new Tuple<NKCOperatorSortSystem.eFilterOption, NKCUnitSortSystem.eFilterOption>(NKCOperatorSortSystem.eFilterOption.NotDecked, NKCUnitSortSystem.eFilterOption.NotDecked),
			new Tuple<NKCOperatorSortSystem.eFilterOption, NKCUnitSortSystem.eFilterOption>(NKCOperatorSortSystem.eFilterOption.Locked, NKCUnitSortSystem.eFilterOption.Locked),
			new Tuple<NKCOperatorSortSystem.eFilterOption, NKCUnitSortSystem.eFilterOption>(NKCOperatorSortSystem.eFilterOption.Unlocked, NKCUnitSortSystem.eFilterOption.Unlocked),
			new Tuple<NKCOperatorSortSystem.eFilterOption, NKCUnitSortSystem.eFilterOption>(NKCOperatorSortSystem.eFilterOption.Have, NKCUnitSortSystem.eFilterOption.Have),
			new Tuple<NKCOperatorSortSystem.eFilterOption, NKCUnitSortSystem.eFilterOption>(NKCOperatorSortSystem.eFilterOption.NotHave, NKCUnitSortSystem.eFilterOption.NotHave),
			new Tuple<NKCOperatorSortSystem.eFilterOption, NKCUnitSortSystem.eFilterOption>(NKCOperatorSortSystem.eFilterOption.Collected, NKCUnitSortSystem.eFilterOption.Collected),
			new Tuple<NKCOperatorSortSystem.eFilterOption, NKCUnitSortSystem.eFilterOption>(NKCOperatorSortSystem.eFilterOption.NotCollected, NKCUnitSortSystem.eFilterOption.NotCollected)
		};

		// Token: 0x040034FD RID: 13565
		public static List<Tuple<NKCOperatorSortSystem.eSortCategory, NKCUnitSortSystem.eSortCategory>> m_listSortCategory = new List<Tuple<NKCOperatorSortSystem.eSortCategory, NKCUnitSortSystem.eSortCategory>>
		{
			new Tuple<NKCOperatorSortSystem.eSortCategory, NKCUnitSortSystem.eSortCategory>(NKCOperatorSortSystem.eSortCategory.None, NKCUnitSortSystem.eSortCategory.None),
			new Tuple<NKCOperatorSortSystem.eSortCategory, NKCUnitSortSystem.eSortCategory>(NKCOperatorSortSystem.eSortCategory.Level, NKCUnitSortSystem.eSortCategory.Level),
			new Tuple<NKCOperatorSortSystem.eSortCategory, NKCUnitSortSystem.eSortCategory>(NKCOperatorSortSystem.eSortCategory.Rarity, NKCUnitSortSystem.eSortCategory.Rarity),
			new Tuple<NKCOperatorSortSystem.eSortCategory, NKCUnitSortSystem.eSortCategory>(NKCOperatorSortSystem.eSortCategory.UnitPower, NKCUnitSortSystem.eSortCategory.UnitPower),
			new Tuple<NKCOperatorSortSystem.eSortCategory, NKCUnitSortSystem.eSortCategory>(NKCOperatorSortSystem.eSortCategory.UID, NKCUnitSortSystem.eSortCategory.UID),
			new Tuple<NKCOperatorSortSystem.eSortCategory, NKCUnitSortSystem.eSortCategory>(NKCOperatorSortSystem.eSortCategory.ID, NKCUnitSortSystem.eSortCategory.ID),
			new Tuple<NKCOperatorSortSystem.eSortCategory, NKCUnitSortSystem.eSortCategory>(NKCOperatorSortSystem.eSortCategory.IDX, NKCUnitSortSystem.eSortCategory.IDX),
			new Tuple<NKCOperatorSortSystem.eSortCategory, NKCUnitSortSystem.eSortCategory>(NKCOperatorSortSystem.eSortCategory.UnitAttack, NKCUnitSortSystem.eSortCategory.UnitAttack),
			new Tuple<NKCOperatorSortSystem.eSortCategory, NKCUnitSortSystem.eSortCategory>(NKCOperatorSortSystem.eSortCategory.UnitHealth, NKCUnitSortSystem.eSortCategory.UnitHealth),
			new Tuple<NKCOperatorSortSystem.eSortCategory, NKCUnitSortSystem.eSortCategory>(NKCOperatorSortSystem.eSortCategory.UnitDefense, NKCUnitSortSystem.eSortCategory.UnitDefense),
			new Tuple<NKCOperatorSortSystem.eSortCategory, NKCUnitSortSystem.eSortCategory>(NKCOperatorSortSystem.eSortCategory.UnitReduceSkillCool, NKCUnitSortSystem.eSortCategory.UnitReduceSkillCool),
			new Tuple<NKCOperatorSortSystem.eSortCategory, NKCUnitSortSystem.eSortCategory>(NKCOperatorSortSystem.eSortCategory.Decked, NKCUnitSortSystem.eSortCategory.Decked),
			new Tuple<NKCOperatorSortSystem.eSortCategory, NKCUnitSortSystem.eSortCategory>(NKCOperatorSortSystem.eSortCategory.Custom1, NKCUnitSortSystem.eSortCategory.Custom1),
			new Tuple<NKCOperatorSortSystem.eSortCategory, NKCUnitSortSystem.eSortCategory>(NKCOperatorSortSystem.eSortCategory.Custom2, NKCUnitSortSystem.eSortCategory.Custom2),
			new Tuple<NKCOperatorSortSystem.eSortCategory, NKCUnitSortSystem.eSortCategory>(NKCOperatorSortSystem.eSortCategory.Custom3, NKCUnitSortSystem.eSortCategory.Custom3)
		};

		// Token: 0x040034FE RID: 13566
		public static List<Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>> m_listSortOptions = new List<Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>>
		{
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.None, NKCUnitSortSystem.eSortOption.None),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.Level_Low, NKCUnitSortSystem.eSortOption.Level_Low),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.Level_High, NKCUnitSortSystem.eSortOption.Level_High),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.Rarity_Low, NKCUnitSortSystem.eSortOption.Rarity_Low),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.Rarity_High, NKCUnitSortSystem.eSortOption.Rarity_High),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.Power_Low, NKCUnitSortSystem.eSortOption.Power_Low),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.Power_High, NKCUnitSortSystem.eSortOption.Power_High),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.UID_First, NKCUnitSortSystem.eSortOption.UID_First),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.UID_Last, NKCUnitSortSystem.eSortOption.UID_Last),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.ID_First, NKCUnitSortSystem.eSortOption.ID_First),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.ID_Last, NKCUnitSortSystem.eSortOption.ID_Last),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.IDX_First, NKCUnitSortSystem.eSortOption.IDX_First),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.IDX_Last, NKCUnitSortSystem.eSortOption.IDX_Last),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.Attack_Low, NKCUnitSortSystem.eSortOption.Attack_Low),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.Attack_High, NKCUnitSortSystem.eSortOption.Attack_High),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.Health_Low, NKCUnitSortSystem.eSortOption.Health_Low),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.Health_High, NKCUnitSortSystem.eSortOption.Health_High),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.Unit_Defense_Low, NKCUnitSortSystem.eSortOption.Unit_Defense_Low),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.Unit_Defense_High, NKCUnitSortSystem.eSortOption.Unit_Defense_High),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.Unit_ReduceSkillCool_Low, NKCUnitSortSystem.eSortOption.Unit_ReduceSkillCool_Low),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.Unit_ReduceSkillCool_High, NKCUnitSortSystem.eSortOption.Unit_ReduceSkillCool_High),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.CustomAscend1, NKCUnitSortSystem.eSortOption.CustomAscend1),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.CustomDescend1, NKCUnitSortSystem.eSortOption.CustomDescend1),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.CustomAscend2, NKCUnitSortSystem.eSortOption.CustomAscend2),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.CustomDescend2, NKCUnitSortSystem.eSortOption.CustomDescend2),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.CustomAscend3, NKCUnitSortSystem.eSortOption.CustomAscend3),
			new Tuple<NKCOperatorSortSystem.eSortOption, NKCUnitSortSystem.eSortOption>(NKCOperatorSortSystem.eSortOption.CustomDescend3, NKCUnitSortSystem.eSortOption.CustomDescend3)
		};

		// Token: 0x0200137B RID: 4987
		public enum FILTER_OPEN_TYPE
		{
			// Token: 0x04009A04 RID: 39428
			NONE,
			// Token: 0x04009A05 RID: 39429
			NORMAL,
			// Token: 0x04009A06 RID: 39430
			COLLECTION,
			// Token: 0x04009A07 RID: 39431
			SELECTION,
			// Token: 0x04009A08 RID: 39432
			ALLUNIT_DEV
		}

		// Token: 0x0200137C RID: 4988
		public enum eFilterCategory
		{
			// Token: 0x04009A0A RID: 39434
			Rarity,
			// Token: 0x04009A0B RID: 39435
			Level,
			// Token: 0x04009A0C RID: 39436
			Decked,
			// Token: 0x04009A0D RID: 39437
			Locked,
			// Token: 0x04009A0E RID: 39438
			Have,
			// Token: 0x04009A0F RID: 39439
			Collected,
			// Token: 0x04009A10 RID: 39440
			PassiveSkill
		}

		// Token: 0x0200137D RID: 4989
		public enum eFilterOption
		{
			// Token: 0x04009A12 RID: 39442
			Nothing,
			// Token: 0x04009A13 RID: 39443
			Everything,
			// Token: 0x04009A14 RID: 39444
			Rarily_SSR,
			// Token: 0x04009A15 RID: 39445
			Rarily_SR,
			// Token: 0x04009A16 RID: 39446
			Rarily_R,
			// Token: 0x04009A17 RID: 39447
			Rarily_N,
			// Token: 0x04009A18 RID: 39448
			Level_1,
			// Token: 0x04009A19 RID: 39449
			Level_other,
			// Token: 0x04009A1A RID: 39450
			Level_Max,
			// Token: 0x04009A1B RID: 39451
			Decked,
			// Token: 0x04009A1C RID: 39452
			NotDecked,
			// Token: 0x04009A1D RID: 39453
			Locked,
			// Token: 0x04009A1E RID: 39454
			Unlocked,
			// Token: 0x04009A1F RID: 39455
			Have,
			// Token: 0x04009A20 RID: 39456
			NotHave,
			// Token: 0x04009A21 RID: 39457
			Collected,
			// Token: 0x04009A22 RID: 39458
			NotCollected,
			// Token: 0x04009A23 RID: 39459
			PassiveSkill
		}

		// Token: 0x0200137E RID: 4990
		public enum eSortCategory
		{
			// Token: 0x04009A25 RID: 39461
			None,
			// Token: 0x04009A26 RID: 39462
			Level,
			// Token: 0x04009A27 RID: 39463
			Rarity,
			// Token: 0x04009A28 RID: 39464
			UnitPower,
			// Token: 0x04009A29 RID: 39465
			UID,
			// Token: 0x04009A2A RID: 39466
			ID,
			// Token: 0x04009A2B RID: 39467
			IDX,
			// Token: 0x04009A2C RID: 39468
			UnitAttack,
			// Token: 0x04009A2D RID: 39469
			UnitHealth,
			// Token: 0x04009A2E RID: 39470
			UnitDefense,
			// Token: 0x04009A2F RID: 39471
			UnitReduceSkillCool,
			// Token: 0x04009A30 RID: 39472
			Decked,
			// Token: 0x04009A31 RID: 39473
			Custom1,
			// Token: 0x04009A32 RID: 39474
			Custom2,
			// Token: 0x04009A33 RID: 39475
			Custom3
		}

		// Token: 0x0200137F RID: 4991
		public enum eSortOption
		{
			// Token: 0x04009A35 RID: 39477
			None,
			// Token: 0x04009A36 RID: 39478
			Level_Low,
			// Token: 0x04009A37 RID: 39479
			Level_High,
			// Token: 0x04009A38 RID: 39480
			Rarity_Low,
			// Token: 0x04009A39 RID: 39481
			Rarity_High,
			// Token: 0x04009A3A RID: 39482
			Power_Low,
			// Token: 0x04009A3B RID: 39483
			Power_High,
			// Token: 0x04009A3C RID: 39484
			UID_First,
			// Token: 0x04009A3D RID: 39485
			UID_Last,
			// Token: 0x04009A3E RID: 39486
			ID_First,
			// Token: 0x04009A3F RID: 39487
			ID_Last,
			// Token: 0x04009A40 RID: 39488
			IDX_First,
			// Token: 0x04009A41 RID: 39489
			IDX_Last,
			// Token: 0x04009A42 RID: 39490
			Attack_Low,
			// Token: 0x04009A43 RID: 39491
			Attack_High,
			// Token: 0x04009A44 RID: 39492
			Health_Low,
			// Token: 0x04009A45 RID: 39493
			Health_High,
			// Token: 0x04009A46 RID: 39494
			Unit_Defense_Low,
			// Token: 0x04009A47 RID: 39495
			Unit_Defense_High,
			// Token: 0x04009A48 RID: 39496
			Unit_ReduceSkillCool_Low,
			// Token: 0x04009A49 RID: 39497
			Unit_ReduceSkillCool_High,
			// Token: 0x04009A4A RID: 39498
			CustomAscend1,
			// Token: 0x04009A4B RID: 39499
			CustomDescend1,
			// Token: 0x04009A4C RID: 39500
			CustomAscend2,
			// Token: 0x04009A4D RID: 39501
			CustomDescend2,
			// Token: 0x04009A4E RID: 39502
			CustomAscend3,
			// Token: 0x04009A4F RID: 39503
			CustomDescend3
		}

		// Token: 0x02001380 RID: 4992
		public struct OperatorListOptions
		{
			// Token: 0x170017F0 RID: 6128
			// (get) Token: 0x0600A5FF RID: 42495 RVA: 0x0034637C File Offset: 0x0034457C
			private HashSet<BUILD_OPTIONS> BuildOption
			{
				get
				{
					if (this.m_BuildOptions == null)
					{
						this.m_BuildOptions = new HashSet<BUILD_OPTIONS>();
					}
					return this.m_BuildOptions;
				}
			}

			// Token: 0x0600A600 RID: 42496 RVA: 0x00346397 File Offset: 0x00344597
			public bool IsHasBuildOption(BUILD_OPTIONS option)
			{
				return this.BuildOption.Contains(option);
			}

			// Token: 0x0600A601 RID: 42497 RVA: 0x003463A8 File Offset: 0x003445A8
			public void SetBuildOption(bool bSet, params BUILD_OPTIONS[] options)
			{
				foreach (BUILD_OPTIONS item in options)
				{
					if (bSet && !this.BuildOption.Contains(item))
					{
						this.BuildOption.Add(item);
					}
					else if (!bSet && this.BuildOption.Contains(item))
					{
						this.BuildOption.Remove(item);
					}
				}
			}

			// Token: 0x0600A602 RID: 42498 RVA: 0x00346408 File Offset: 0x00344608
			public OperatorListOptions(NKM_DECK_TYPE deckType = NKM_DECK_TYPE.NDT_NONE)
			{
				this.eDeckType = deckType;
				this.m_BuildOptions = new HashSet<BUILD_OPTIONS>();
				this.m_BuildOptions.Add(BUILD_OPTIONS.DESCENDING);
				this.m_BuildOptions.Add(BUILD_OPTIONS.PUSHBACK_UNSELECTABLE);
				this.m_BuildOptions.Add(BUILD_OPTIONS.INCLUDE_UNDECKABLE_UNIT);
				this.lstSortOption = new List<NKCOperatorSortSystem.eSortOption>();
				this.lstSortOption = NKCOperatorSortSystem.AddDefaultSortOptions(this.lstSortOption, false);
				this.lstDefaultSortOption = null;
				this.setFilterOption = new HashSet<NKCOperatorSortSystem.eFilterOption>();
				this.lstCustomSortFunc = new Dictionary<NKCOperatorSortSystem.eSortCategory, KeyValuePair<string, NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc>>();
				this.setOnlyIncludeFilterOption = null;
				this.PreemptiveSortFunc = null;
				this.AdditionalExcludeFilterFunc = null;
				this.setExcludeOperatorUID = null;
				this.setExcludeOperatorID = null;
				this.setOnlyIncludeOperatorID = null;
				this.setDuplicateOperatorID = null;
				this.AdditionalUnitStateFunc = null;
				this.bIgnoreMissionState = false;
				this.passiveSkillID = 0;
			}

			// Token: 0x0600A603 RID: 42499 RVA: 0x003464D0 File Offset: 0x003446D0
			public void MakeDuplicateSetFromAllDeck(NKMDeckIndex currentDeckIndex, long selectedOperUID, NKMArmyData armyData)
			{
				NKM_DECK_TYPE nkm_DECK_TYPE = currentDeckIndex.m_eDeckType;
				if (nkm_DECK_TYPE - NKM_DECK_TYPE.NDT_TRIM > 1)
				{
					return;
				}
				if (this.setDuplicateOperatorID == null)
				{
					this.setDuplicateOperatorID = new HashSet<int>();
				}
				IReadOnlyList<NKMDeckData> deckList = armyData.GetDeckList(currentDeckIndex.m_eDeckType);
				for (int i = 0; i < deckList.Count; i++)
				{
					NKMDeckData nkmdeckData = deckList[i];
					if (nkmdeckData == null)
					{
						return;
					}
					if (nkmdeckData.m_OperatorUID != selectedOperUID)
					{
						NKMOperator operatorFromUId = armyData.GetOperatorFromUId(nkmdeckData.m_OperatorUID);
						if (operatorFromUId != null)
						{
							this.setDuplicateOperatorID.Add(operatorFromUId.id);
						}
					}
				}
			}

			// Token: 0x04009A50 RID: 39504
			public NKM_DECK_TYPE eDeckType;

			// Token: 0x04009A51 RID: 39505
			public HashSet<int> setExcludeOperatorID;

			// Token: 0x04009A52 RID: 39506
			public HashSet<int> setOnlyIncludeOperatorID;

			// Token: 0x04009A53 RID: 39507
			public HashSet<int> setDuplicateOperatorID;

			// Token: 0x04009A54 RID: 39508
			public HashSet<long> setExcludeOperatorUID;

			// Token: 0x04009A55 RID: 39509
			public HashSet<NKCOperatorSortSystem.eFilterOption> setOnlyIncludeFilterOption;

			// Token: 0x04009A56 RID: 39510
			public HashSet<NKCOperatorSortSystem.eFilterOption> setFilterOption;

			// Token: 0x04009A57 RID: 39511
			public List<NKCOperatorSortSystem.eSortOption> lstSortOption;

			// Token: 0x04009A58 RID: 39512
			public NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc PreemptiveSortFunc;

			// Token: 0x04009A59 RID: 39513
			public Dictionary<NKCOperatorSortSystem.eSortCategory, KeyValuePair<string, NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc>> lstCustomSortFunc;

			// Token: 0x04009A5A RID: 39514
			public NKCOperatorSortSystem.OperatorListOptions.CustomFilterFunc AdditionalExcludeFilterFunc;

			// Token: 0x04009A5B RID: 39515
			public List<NKCOperatorSortSystem.eSortOption> lstDefaultSortOption;

			// Token: 0x04009A5C RID: 39516
			public NKCOperatorSortSystem.OperatorListOptions.CustomUnitStateFunc AdditionalUnitStateFunc;

			// Token: 0x04009A5D RID: 39517
			public bool bIgnoreMissionState;

			// Token: 0x04009A5E RID: 39518
			public int passiveSkillID;

			// Token: 0x04009A5F RID: 39519
			private HashSet<BUILD_OPTIONS> m_BuildOptions;

			// Token: 0x02001A57 RID: 6743
			// (Invoke) Token: 0x0600BBA7 RID: 48039
			public delegate bool CustomFilterFunc(NKMOperator operatorData);

			// Token: 0x02001A58 RID: 6744
			// (Invoke) Token: 0x0600BBAB RID: 48043
			public delegate NKCUnitSortSystem.eUnitState CustomUnitStateFunc(NKMOperator unitData);
		}

		// Token: 0x02001381 RID: 4993
		private class OperatorInfoCache
		{
			// Token: 0x04009A60 RID: 39520
			public NKMDeckIndex DeckIndex;

			// Token: 0x04009A61 RID: 39521
			public NKMWorldMapManager.WorldMapLeaderState CityState;

			// Token: 0x04009A62 RID: 39522
			public NKCUnitSortSystem.eUnitState UnitSlotState;

			// Token: 0x04009A63 RID: 39523
			public int Power;

			// Token: 0x04009A64 RID: 39524
			public int Attack;

			// Token: 0x04009A65 RID: 39525
			public int HP;

			// Token: 0x04009A66 RID: 39526
			public int Defense;

			// Token: 0x04009A67 RID: 39527
			public int ReduceSkillCollTime;
		}

		// Token: 0x02001382 RID: 4994
		// (Invoke) Token: 0x0600A606 RID: 42502
		public delegate bool AutoSelectExtraFilter(NKMOperator operatorData);
	}
}
