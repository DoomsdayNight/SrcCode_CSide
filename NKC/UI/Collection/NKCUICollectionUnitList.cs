using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Collection
{
	// Token: 0x02000C30 RID: 3120
	public class NKCUICollectionUnitList : MonoBehaviour
	{
		// Token: 0x060090E3 RID: 37091 RVA: 0x00315D1C File Offset: 0x00313F1C
		public static List<NKMUnitData> UpdateCollectionUnitList(ref int collectionUnit, ref int totalUnit, NKM_UNIT_TYPE eUnitType, bool getUnitDataList)
		{
			List<NKMUnitData> list = null;
			if (getUnitDataList)
			{
				list = new List<NKMUnitData>();
			}
			List<int> unitList = NKCCollectionManager.GetUnitList(eUnitType);
			for (int i = 0; i < unitList.Count; i++)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitList[i]);
				if (unitTempletBase != null)
				{
					NKCCollectionUnitTemplet unitTemplet = NKCCollectionManager.GetUnitTemplet(unitTempletBase.m_UnitID);
					if ((unitTemplet == null || !unitTemplet.m_bExclude || NKCUICollectionUnitList.IsHasUnit(eUnitType, unitTempletBase.m_UnitID)) && unitTempletBase.PickupEnableByTag)
					{
						if (getUnitDataList)
						{
							list.Add(NKCUtil.MakeDummyUnit(unitList[i], true));
						}
						if (NKCUICollectionUnitList.IsHasUnit(eUnitType, unitList[i]))
						{
							collectionUnit++;
						}
						totalUnit++;
					}
				}
			}
			return list;
		}

		// Token: 0x060090E4 RID: 37092 RVA: 0x00315DC0 File Offset: 0x00313FC0
		public void UpdateCollectionMissionRate()
		{
			if (this.m_eUnitType != NKM_UNIT_TYPE.NUT_NORMAL)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objAchievementRate, NKCUnitMissionManager.GetOpenTagCollectionMission());
			if (this.m_objAchievementRate == null || !this.m_objAchievementRate.activeSelf)
			{
				return;
			}
			int num = 0;
			int num2 = 0;
			if (this.m_dicUnitMissionCompletedCount.Count != this.m_lstCollectionUnitData.Count)
			{
				this.m_dicUnitMissionCompletedCount.Clear();
			}
			if (this.m_dicUnitMissionCompletedCount.Count <= 0)
			{
				int count = this.m_lstCollectionUnitData.Count;
				for (int i = 0; i < count; i++)
				{
					int num3 = 0;
					int num4 = 0;
					NKCUnitMissionManager.GetUnitMissionAchievedCount(this.m_lstCollectionUnitData[i].m_UnitID, ref num3, ref num4);
					num += num3;
					num2 += num4;
					if (!this.m_dicUnitMissionCompletedCount.ContainsKey(this.m_lstCollectionUnitData[i].m_UnitID))
					{
						this.m_dicUnitMissionCompletedCount.Add(this.m_lstCollectionUnitData[i].m_UnitID, new ValueTuple<int, int>(num4, num3));
					}
				}
			}
			else
			{
				foreach (KeyValuePair<int, ValueTuple<int, int>> keyValuePair in this.m_dicUnitMissionCompletedCount)
				{
					num2 += keyValuePair.Value.Item1;
					num += keyValuePair.Value.Item2;
				}
			}
			this.UpdateUnitMissionRate(num2, num);
		}

		// Token: 0x060090E5 RID: 37093 RVA: 0x00315F2C File Offset: 0x0031412C
		public void UpdateCollectionMissionRate(List<int> unitIdList)
		{
			if (this.m_eUnitType != NKM_UNIT_TYPE.NUT_NORMAL || !NKCUnitMissionManager.GetOpenTagCollectionMission() || unitIdList == null)
			{
				return;
			}
			int item = 0;
			int item2 = 0;
			int count = unitIdList.Count;
			for (int i = 0; i < count; i++)
			{
				item = 0;
				item2 = 0;
				NKCUnitMissionManager.GetUnitMissionAchievedCount(unitIdList[i], ref item, ref item2);
				if (this.m_dicUnitMissionCompletedCount.ContainsKey(unitIdList[i]))
				{
					this.m_dicUnitMissionCompletedCount[unitIdList[i]] = new ValueTuple<int, int>(item2, item);
				}
			}
			int num = 0;
			int num2 = 0;
			foreach (KeyValuePair<int, ValueTuple<int, int>> keyValuePair in this.m_dicUnitMissionCompletedCount)
			{
				num2 += keyValuePair.Value.Item1;
				num += keyValuePair.Value.Item2;
			}
			this.UpdateUnitMissionRate(num2, num);
			this.m_LoopScrollRect.RefreshCells(false);
		}

		// Token: 0x060090E6 RID: 37094 RVA: 0x00316028 File Offset: 0x00314228
		private void UpdateUnitMissionRate(int achievedCount, int totalCount)
		{
			if (this.m_objAchievementRate != null && this.m_objAchievementRate.activeSelf)
			{
				float num = Mathf.Clamp(Mathf.Floor((float)achievedCount) / Mathf.Floor((float)totalCount), 0f, 1f);
				NKCUtil.SetImageFillAmount(this.m_imgAchievementGauge, num);
				NKCUtil.SetLabelText(this.m_lbAchievememtCount, string.Format("{0}/{1}", achievedCount, totalCount));
				NKCUtil.SetLabelText(this.m_lbAchievementPercent, string.Format("{0}%", Mathf.FloorToInt(num * 100f)));
			}
		}

		// Token: 0x060090E7 RID: 37095 RVA: 0x003160C4 File Offset: 0x003142C4
		public static bool IsHasUnit(NKM_UNIT_TYPE type, int UnitID = 0)
		{
			bool result = false;
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			if (!armyData.IsFirstGetUnit(UnitID))
			{
				return true;
			}
			if (type == NKM_UNIT_TYPE.NUT_SHIP)
			{
				int num = 1000;
				int num2 = UnitID / num;
				int num3 = UnitID % num;
				for (int i = 5; i > 0; i--)
				{
					num2--;
					int unitID = num2 * num + num3;
					if (!armyData.IsFirstGetUnit(unitID))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060090E8 RID: 37096 RVA: 0x0031612C File Offset: 0x0031432C
		public void Init(NKCUICollection.OnSyncCollectingData callBack)
		{
			if (null != this.m_LoopScrollRect)
			{
				this.m_LoopScrollRect.dOnGetObject += this.GetSlot;
				this.m_LoopScrollRect.dOnReturnObject += this.ReturnSlot;
				this.m_LoopScrollRect.dOnProvideData += this.ProvideSlotData;
				this.m_LoopScrollRect.dOnRepopulate += this.CalculateContentRectSize;
				NKCUtil.SetScrollHotKey(this.m_LoopScrollRect, null);
			}
			if (this.m_SortUI != null)
			{
				this.m_SortUI.Init(new NKCUIComUnitSortOptions.OnSorted(this.OnSortChanged), true);
				NKM_UNIT_TYPE eUnitType = this.m_eUnitType;
				if (eUnitType == NKM_UNIT_TYPE.NUT_NORMAL || eUnitType != NKM_UNIT_TYPE.NUT_SHIP)
				{
					this.m_SortUI.RegisterCategories(this.eUnitFilterCategories, this.eUnitSortCategories, false);
				}
				else
				{
					this.m_SortUI.RegisterCategories(this.eShipFilterCategories, this.eShipSortCategories, false);
				}
				if (this.m_SortUI.m_NKCPopupSort != null)
				{
					this.m_SortUI.m_NKCPopupSort.m_bUseDefaultSortAdd = false;
				}
			}
			base.gameObject.SetActive(false);
			if (callBack != null)
			{
				this.dOnOnSyncCollectingData = callBack;
			}
			if (this.m_tglShowHasReward != null)
			{
				this.m_tglShowHasReward.OnValueChanged.RemoveAllListeners();
				this.m_tglShowHasReward.OnValueChanged.AddListener(new UnityAction<bool>(this.OnToggleHasReward));
			}
			this.m_bCellPrepared = false;
		}

		// Token: 0x060090E9 RID: 37097 RVA: 0x00316294 File Offset: 0x00314494
		public void Open()
		{
			this.m_iTotalUnit = 0;
			this.m_iCollectionUnit = 0;
			this.m_ssActive = null;
			NKCUtil.SetGameobjectActive(this.m_tglShowHasReward, false);
			NKCUIUnitSelectList.UnitSelectListOptions currentOption = new NKCUIUnitSelectList.UnitSelectListOptions(this.m_eUnitType, true, NKM_DECK_TYPE.NDT_NORMAL, NKCUIUnitSelectList.eUnitSelectListMode.Normal, true);
			currentOption.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
			NKM_UNIT_TYPE eUnitType = this.m_eUnitType;
			if (eUnitType == NKM_UNIT_TYPE.NUT_NORMAL || eUnitType != NKM_UNIT_TYPE.NUT_SHIP)
			{
				currentOption.lstSortOption = NKCUnitSortSystem.GetDefaultSortOptions(NKM_UNIT_TYPE.NUT_NORMAL, true, false);
				bool bValue = NKCUnitMissionManager.HasRewardEnableMission();
				NKCUtil.SetGameobjectActive(this.m_tglShowHasReward, bValue);
				NKCUIComToggle tglShowHasReward = this.m_tglShowHasReward;
				if (tglShowHasReward != null)
				{
					tglShowHasReward.Select(false, true, false);
				}
			}
			else
			{
				currentOption.lstSortOption = NKCUnitSortSystem.GetDefaultSortOptions(NKM_UNIT_TYPE.NUT_SHIP, true, false);
			}
			currentOption.bDescending = false;
			currentOption.bShowRemoveSlot = false;
			currentOption.bMultipleSelect = false;
			currentOption.iMaxMultipleSelect = 0;
			currentOption.bExcludeLockedUnit = false;
			currentOption.bExcludeDeckedUnit = false;
			currentOption.bShowHideDeckedUnitMenu = true;
			currentOption.bHideDeckedUnit = false;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_currentOption = currentOption;
			this.AdjustScrollRectTop();
			this.PrepareUnitList(false);
			this.UpdateCollectingRate();
			this.UpdateCollectionMissionRate();
		}

		// Token: 0x060090EA RID: 37098 RVA: 0x003163A1 File Offset: 0x003145A1
		public void Clear()
		{
			NKCUICollectionShipInfo.CheckInstanceAndClose();
			NKCUICollectionUnitInfo.CheckInstanceAndClose();
			NKCScenManager.GetScenManager().m_NKCMemoryCleaner.UnloadObjectPool();
			this.m_dicUnitMissionCompletedCount.Clear();
			this.m_dicUnitMissionCompletedCount = null;
		}

		// Token: 0x060090EB RID: 37099 RVA: 0x003163D0 File Offset: 0x003145D0
		private void UpdateCollectingRate()
		{
			if (this.dOnOnSyncCollectingData != null)
			{
				NKM_UNIT_TYPE eUnitType = this.m_eUnitType;
				if (eUnitType == NKM_UNIT_TYPE.NUT_NORMAL)
				{
					this.dOnOnSyncCollectingData(NKCUICollection.CollectionType.CT_UNIT, this.m_iCollectionUnit, this.m_iTotalUnit);
					return;
				}
				if (eUnitType != NKM_UNIT_TYPE.NUT_SHIP)
				{
					return;
				}
				this.dOnOnSyncCollectingData(NKCUICollection.CollectionType.CT_SHIP, this.m_iCollectionUnit, this.m_iTotalUnit);
			}
		}

		// Token: 0x060090EC RID: 37100 RVA: 0x00316428 File Offset: 0x00314628
		private RectTransform GetSlot(int index)
		{
			Stack<NKCUIUnitSelectListSlotBase> stkUnitSlotPool = this.m_stkUnitSlotPool;
			NKCUIUnitSelectListSlotBase pfbUnitSlot = this.m_pfbUnitSlot;
			if (stkUnitSlotPool.Count > 0)
			{
				NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase = stkUnitSlotPool.Pop();
				NKCUtil.SetGameobjectActive(nkcuiunitSelectListSlotBase, true);
				nkcuiunitSelectListSlotBase.transform.localScale = Vector3.one;
				this.m_lstVisibleSlot.Add(nkcuiunitSelectListSlotBase);
				return nkcuiunitSelectListSlotBase.GetComponent<RectTransform>();
			}
			NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase2 = UnityEngine.Object.Instantiate<NKCUIUnitSelectListSlotBase>(pfbUnitSlot);
			nkcuiunitSelectListSlotBase2.Init(true);
			NKCUtil.SetGameobjectActive(nkcuiunitSelectListSlotBase2, true);
			nkcuiunitSelectListSlotBase2.transform.localScale = Vector3.one;
			this.m_lstVisibleSlot.Add(nkcuiunitSelectListSlotBase2);
			return nkcuiunitSelectListSlotBase2.GetComponent<RectTransform>();
		}

		// Token: 0x060090ED RID: 37101 RVA: 0x003164B4 File Offset: 0x003146B4
		private void ReturnSlot(Transform go)
		{
			NKCUIUnitSelectListSlotBase component = go.GetComponent<NKCUIUnitSelectListSlotBase>();
			this.m_lstVisibleSlot.Remove(component);
			go.SetParent(this.m_rectSlotPoolRect);
			if (component is NKCUIUnitSelectListSlot)
			{
				this.m_stkUnitSlotPool.Push(component);
			}
		}

		// Token: 0x060090EE RID: 37102 RVA: 0x003164F8 File Offset: 0x003146F8
		private void ProvideSlotData(Transform tr, int idx)
		{
			if (this.m_ssActive == null)
			{
				Debug.LogError("Slot Sort System Null!!");
				return;
			}
			NKCUIUnitSelectListSlotBase component = tr.GetComponent<NKCUIUnitSelectListSlotBase>();
			if (component == null)
			{
				return;
			}
			component.Init(true);
			if (this.m_currentOption.bShowRemoveSlot)
			{
				if (idx == 0)
				{
					component.SetEmpty(true, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnSlotSelected), null);
					return;
				}
				idx--;
			}
			if (this.m_ssActive.SortedUnitList.Count <= idx)
			{
				return;
			}
			NKMUnitData nkmunitData = this.m_ssActive.SortedUnitList[idx];
			long unitUID = nkmunitData.m_UnitUID;
			NKMDeckIndex deckIndexCache = this.m_ssActive.GetDeckIndexCache(unitUID, true);
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			component.SetDataForCollection(nkmunitData, deckIndexCache, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnSlotSelected), NKCUICollectionUnitList.IsHasUnit(this.m_eUnitType, nkmunitData.m_UnitID));
			if (this.m_ssActive.lstSortOption.Count > 0)
			{
				switch (this.m_ssActive.lstSortOption[0])
				{
				case NKCUnitSortSystem.eSortOption.Power_Low:
				case NKCUnitSortSystem.eSortOption.Power_High:
					component.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Power_High, this.m_ssActive.GetUnitPowerCache(unitUID));
					goto IL_223;
				case NKCUnitSortSystem.eSortOption.Attack_Low:
				case NKCUnitSortSystem.eSortOption.Attack_High:
					component.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Attack_High, this.m_ssActive.GetUnitAttackCache(unitUID));
					goto IL_223;
				case NKCUnitSortSystem.eSortOption.Health_Low:
				case NKCUnitSortSystem.eSortOption.Health_High:
					component.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Health_High, this.m_ssActive.GetUnitHPCache(unitUID));
					goto IL_223;
				case NKCUnitSortSystem.eSortOption.Unit_Defense_Low:
				case NKCUnitSortSystem.eSortOption.Unit_Defense_High:
					component.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Unit_Defense_High, this.m_ssActive.GetUnitDefCache(unitUID));
					goto IL_223;
				case NKCUnitSortSystem.eSortOption.Unit_Crit_Low:
				case NKCUnitSortSystem.eSortOption.Unit_Crit_High:
					component.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Unit_Crit_High, this.m_ssActive.GetUnitCritCache(unitUID));
					goto IL_223;
				case NKCUnitSortSystem.eSortOption.Unit_Hit_Low:
				case NKCUnitSortSystem.eSortOption.Unit_Hit_High:
					component.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Unit_Hit_High, this.m_ssActive.GetUnitHitCache(unitUID));
					goto IL_223;
				case NKCUnitSortSystem.eSortOption.Unit_Evade_Low:
				case NKCUnitSortSystem.eSortOption.Unit_Evade_High:
					component.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Unit_Evade_High, this.m_ssActive.GetUnitEvadeCache(unitUID));
					goto IL_223;
				case NKCUnitSortSystem.eSortOption.Unit_ReduceSkillCool_Low:
				case NKCUnitSortSystem.eSortOption.Unit_ReduceSkillCool_High:
					component.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Unit_ReduceSkillCool_High, this.m_ssActive.GetUnitSkillCoolCache(unitUID));
					goto IL_223;
				}
				component.SetSortingTypeValue(false, NKCOperatorSortSystem.eSortOption.Level_High, 0);
			}
			else
			{
				component.SetSortingTypeValue(false, NKCOperatorSortSystem.eSortOption.Level_High, 0);
			}
			IL_223:
			component.SetSlotState(this.m_ssActive.GetUnitSlotState(nkmunitData.m_UnitUID));
			NKCUIUnitSelectList.UnitSelectListOptions.OnSlotSetData dOnSlotSetData = this.m_currentOption.dOnSlotSetData;
			if (dOnSlotSetData == null)
			{
				return;
			}
			dOnSlotSetData(component, nkmunitData, deckIndexCache);
		}

		// Token: 0x060090EF RID: 37103 RVA: 0x00316758 File Offset: 0x00314958
		private void OnSlotSelected(NKMUnitData selectedUnit, NKMUnitTempletBase unitTempletBase, NKMDeckIndex selectedUnitDeckIndex, NKCUnitSortSystem.eUnitState unitSlotState, NKCUIUnitSelectList.eUnitSlotSelectState unitSlotSelectState)
		{
			if (selectedUnit == null)
			{
				return;
			}
			int unitID = selectedUnit.m_UnitID;
			this.m_lstCurUnitData = this.m_ssActive.GetCurrentUnitList();
			int num = -1;
			for (int i = 0; i < this.m_lstCurUnitData.Count; i++)
			{
				if (this.m_lstCurUnitData[i].m_UnitID == unitID)
				{
					num = i;
					break;
				}
			}
			if (num > -1)
			{
				if (!this.m_lstCurUnitData.Contains(selectedUnit))
				{
					return;
				}
				NKCUIUnitInfo.OpenOption openOption = new NKCUIUnitInfo.OpenOption(this.m_lstCurUnitData, num);
				NKM_UNIT_TYPE eUnitType = this.m_eUnitType;
				if (eUnitType == NKM_UNIT_TYPE.NUT_NORMAL || eUnitType != NKM_UNIT_TYPE.NUT_SHIP)
				{
					NKCUICollectionUnitInfo.CheckInstanceAndOpen(this.m_lstCurUnitData[num], openOption, null, NKCUICollectionUnitInfo.eCollectionState.CS_PROFILE, false, NKCUIUpsideMenu.eMode.Normal);
					return;
				}
				NKCUICollectionShipInfo.Instance.Open(this.m_lstCurUnitData[num], NKMDeckIndex.None, openOption, null, false);
			}
		}

		// Token: 0x060090F0 RID: 37104 RVA: 0x00316818 File Offset: 0x00314A18
		private void PrepareUnitList(bool bForceRebuildList = false)
		{
			if (!this.m_bCellPrepared)
			{
				this.m_bCellPrepared = true;
				this.CalculateContentRectSize();
				this.m_LoopScrollRect.PrepareCells(0);
			}
			this.m_lstCollectionUnitData = NKCUICollectionUnitList.UpdateCollectionUnitList(ref this.m_iCollectionUnit, ref this.m_iTotalUnit, this.m_eUnitType, true);
			this.m_ssActive = new NKCGenericUnitSort(null, this.m_currentOption.m_SortOptions, this.m_lstCollectionUnitData);
			this.m_SortUI.RegisterUnitSort(this.m_ssActive);
			this.m_SortUI.ResetUI(false);
			this.OnSortChanged(true);
		}

		// Token: 0x060090F1 RID: 37105 RVA: 0x003168A8 File Offset: 0x00314AA8
		private void CalculateContentRectSize()
		{
			if (this.m_safeArea != null)
			{
				this.m_safeArea.SetSafeAreaBase();
			}
			NKCUtil.CalculateContentRectSize(this.m_LoopScrollRect, this.m_GridLayoutGroup, this.minColumnUnit, this.m_vUnitSlotSize, this.m_vUnitSlotSpacing, this.m_eUnitType == NKM_UNIT_TYPE.NUT_SHIP);
		}

		// Token: 0x060090F2 RID: 37106 RVA: 0x003168FC File Offset: 0x00314AFC
		private void AdjustScrollRectTop()
		{
			if (this.m_rectScrollAreaRect == null || this.m_eUnitType != NKM_UNIT_TYPE.NUT_NORMAL)
			{
				return;
			}
			bool openTagCollectionMission = NKCUnitMissionManager.GetOpenTagCollectionMission();
			Vector2 offsetMax = this.m_rectScrollAreaRect.offsetMax;
			offsetMax.y = (openTagCollectionMission ? (-this.m_fScrollRectUnitMissionOnTop) : (-this.m_fScrollRectUnitMissionOffTop));
			this.m_rectScrollAreaRect.offsetMax = offsetMax;
		}

		// Token: 0x060090F3 RID: 37107 RVA: 0x0031695C File Offset: 0x00314B5C
		private void OnSortChanged(bool bResetScroll)
		{
			if (this.m_ssActive != null)
			{
				this.m_LoopScrollRect.TotalCount = this.m_ssActive.SortedUnitList.Count;
				NKCUtil.SetGameobjectActive(this.m_objNone, this.m_ssActive.SortedUnitList.Count == 0);
				if (bResetScroll)
				{
					this.m_LoopScrollRect.SetIndexPosition(0);
					return;
				}
				this.m_LoopScrollRect.RefreshCells(false);
			}
		}

		// Token: 0x060090F4 RID: 37108 RVA: 0x003169C8 File Offset: 0x00314BC8
		private void OnToggleHasReward(bool bValue)
		{
			bool bResetScroll = false;
			if (bValue)
			{
				if (!this.m_ssActive.FilterSet.Contains(NKCUnitSortSystem.eFilterOption.Collection_HasAchieve))
				{
					this.m_ssActive.FilterSet.Add(NKCUnitSortSystem.eFilterOption.Collection_HasAchieve);
				}
				bResetScroll = true;
			}
			else if (this.m_ssActive.FilterSet.Contains(NKCUnitSortSystem.eFilterOption.Collection_HasAchieve))
			{
				this.m_ssActive.FilterSet.Remove(NKCUnitSortSystem.eFilterOption.Collection_HasAchieve);
			}
			this.m_ssActive.FilterList(this.m_ssActive.FilterSet, this.m_ssActive.Options.bHideDeckedUnit);
			this.OnSortChanged(bResetScroll);
		}

		// Token: 0x060090F5 RID: 37109 RVA: 0x00316A5C File Offset: 0x00314C5C
		public void CheckRewardToggle()
		{
			if (this.m_tglShowHasReward == null)
			{
				return;
			}
			bool flag = NKCUnitMissionManager.HasRewardEnableMission();
			NKCUtil.SetGameobjectActive(this.m_tglShowHasReward, flag);
			this.m_tglShowHasReward.Select(this.m_ssActive != null && this.m_ssActive.FilterSet != null && this.m_ssActive.FilterSet.Contains(NKCUnitSortSystem.eFilterOption.Collection_HasAchieve), true, false);
			this.OnToggleHasReward(flag && this.m_tglShowHasReward.m_bSelect);
		}

		// Token: 0x060090F6 RID: 37110 RVA: 0x00316AD9 File Offset: 0x00314CD9
		public bool IsOpenUnitInfo()
		{
			return NKCUICollectionUnitInfo.IsInstanceOpen;
		}

		// Token: 0x04007E20 RID: 32288
		[Header("유닛 슬롯 프리팹 & 사이즈 설정")]
		public NKCUIUnitSelectListSlotBase m_pfbUnitSlot;

		// Token: 0x04007E21 RID: 32289
		public Vector2 m_vUnitSlotSize;

		// Token: 0x04007E22 RID: 32290
		public Vector2 m_vUnitSlotSpacing;

		// Token: 0x04007E23 RID: 32291
		[Header("UI Components")]
		public RectTransform m_rectContentRect;

		// Token: 0x04007E24 RID: 32292
		public RectTransform m_rectSlotPoolRect;

		// Token: 0x04007E25 RID: 32293
		public LoopScrollRect m_LoopScrollRect;

		// Token: 0x04007E26 RID: 32294
		public GridLayoutGroup m_GridLayoutGroup;

		// Token: 0x04007E27 RID: 32295
		public NKCUIComSafeArea m_safeArea;

		// Token: 0x04007E28 RID: 32296
		[Header("정렬/필터 통합UI")]
		public NKCUIComUnitSortOptions m_SortUI;

		// Token: 0x04007E29 RID: 32297
		[Header("유닛 설정")]
		public int minColumnUnit = 5;

		// Token: 0x04007E2A RID: 32298
		public NKM_UNIT_TYPE m_eUnitType = NKM_UNIT_TYPE.NUT_NORMAL;

		// Token: 0x04007E2B RID: 32299
		[Header("유닛 미션")]
		public GameObject m_objAchievementRate;

		// Token: 0x04007E2C RID: 32300
		public Image m_imgAchievementGauge;

		// Token: 0x04007E2D RID: 32301
		public Text m_lbAchievememtCount;

		// Token: 0x04007E2E RID: 32302
		public Text m_lbAchievementPercent;

		// Token: 0x04007E2F RID: 32303
		[Header("스크롤렉트 높이 조정")]
		public RectTransform m_rectScrollAreaRect;

		// Token: 0x04007E30 RID: 32304
		public float m_fScrollRectUnitMissionOffTop;

		// Token: 0x04007E31 RID: 32305
		public float m_fScrollRectUnitMissionOnTop;

		// Token: 0x04007E32 RID: 32306
		[Header("보상있는 유닛만 보이는 토글")]
		public NKCUIComToggle m_tglShowHasReward;

		// Token: 0x04007E33 RID: 32307
		[Header("etc")]
		public GameObject m_objNone;

		// Token: 0x04007E34 RID: 32308
		private int m_iTotalUnit;

		// Token: 0x04007E35 RID: 32309
		private int m_iCollectionUnit;

		// Token: 0x04007E36 RID: 32310
		private NKCUnitSortSystem m_ssActive;

		// Token: 0x04007E37 RID: 32311
		private readonly HashSet<NKCUnitSortSystem.eFilterCategory> eUnitFilterCategories = new HashSet<NKCUnitSortSystem.eFilterCategory>
		{
			NKCUnitSortSystem.eFilterCategory.UnitType,
			NKCUnitSortSystem.eFilterCategory.SpecialType,
			NKCUnitSortSystem.eFilterCategory.UnitRole,
			NKCUnitSortSystem.eFilterCategory.UnitTargetType,
			NKCUnitSortSystem.eFilterCategory.Rarity,
			NKCUnitSortSystem.eFilterCategory.Cost,
			NKCUnitSortSystem.eFilterCategory.Collected
		};

		// Token: 0x04007E38 RID: 32312
		private readonly HashSet<NKCUnitSortSystem.eFilterCategory> eShipFilterCategories = new HashSet<NKCUnitSortSystem.eFilterCategory>
		{
			NKCUnitSortSystem.eFilterCategory.ShipType,
			NKCUnitSortSystem.eFilterCategory.Rarity
		};

		// Token: 0x04007E39 RID: 32313
		private readonly HashSet<NKCUnitSortSystem.eSortCategory> eUnitSortCategories = new HashSet<NKCUnitSortSystem.eSortCategory>
		{
			NKCUnitSortSystem.eSortCategory.IDX,
			NKCUnitSortSystem.eSortCategory.Rarity,
			NKCUnitSortSystem.eSortCategory.UnitSummonCost,
			NKCUnitSortSystem.eSortCategory.UnitPower,
			NKCUnitSortSystem.eSortCategory.UnitAttack,
			NKCUnitSortSystem.eSortCategory.UnitHealth,
			NKCUnitSortSystem.eSortCategory.UnitDefense,
			NKCUnitSortSystem.eSortCategory.UnitCrit,
			NKCUnitSortSystem.eSortCategory.UnitHit,
			NKCUnitSortSystem.eSortCategory.UnitEvade
		};

		// Token: 0x04007E3A RID: 32314
		private readonly HashSet<NKCUnitSortSystem.eSortCategory> eShipSortCategories = new HashSet<NKCUnitSortSystem.eSortCategory>
		{
			NKCUnitSortSystem.eSortCategory.IDX,
			NKCUnitSortSystem.eSortCategory.Rarity,
			NKCUnitSortSystem.eSortCategory.UnitAttack,
			NKCUnitSortSystem.eSortCategory.UnitHealth
		};

		// Token: 0x04007E3B RID: 32315
		private List<NKMUnitData> m_lstCollectionUnitData = new List<NKMUnitData>();

		// Token: 0x04007E3C RID: 32316
		private Dictionary<int, ValueTuple<int, int>> m_dicUnitMissionCompletedCount = new Dictionary<int, ValueTuple<int, int>>();

		// Token: 0x04007E3D RID: 32317
		private NKCUIUnitSelectList.UnitSelectListOptions m_currentOption;

		// Token: 0x04007E3E RID: 32318
		private NKCUICollection.OnSyncCollectingData dOnOnSyncCollectingData;

		// Token: 0x04007E3F RID: 32319
		private List<NKCUIUnitSelectListSlotBase> m_lstVisibleSlot = new List<NKCUIUnitSelectListSlotBase>();

		// Token: 0x04007E40 RID: 32320
		private Stack<NKCUIUnitSelectListSlotBase> m_stkUnitSlotPool = new Stack<NKCUIUnitSelectListSlotBase>();

		// Token: 0x04007E41 RID: 32321
		private List<NKMUnitData> m_lstCurUnitData = new List<NKMUnitData>();

		// Token: 0x04007E42 RID: 32322
		private bool m_bCellPrepared;
	}
}
