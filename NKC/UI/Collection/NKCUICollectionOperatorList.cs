using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Collection
{
	// Token: 0x02000C25 RID: 3109
	public class NKCUICollectionOperatorList : MonoBehaviour
	{
		// Token: 0x0600900B RID: 36875 RVA: 0x0030F7C0 File Offset: 0x0030D9C0
		public static List<NKMOperator> UpdateCollectionUnitList(ref int collectionUnit, ref int totalUnit, bool createOperatorDataList)
		{
			List<NKMOperator> list = null;
			if (createOperatorDataList)
			{
				list = new List<NKMOperator>();
			}
			List<int> unitList = NKCCollectionManager.GetUnitList(NKM_UNIT_TYPE.NUT_OPERATOR);
			for (int i = 0; i < unitList.Count; i++)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitList[i]);
				if (unitTempletBase != null && unitTempletBase.CollectionEnableByTag)
				{
					NKCCollectionUnitTemplet unitTemplet = NKCCollectionManager.GetUnitTemplet(unitTempletBase.m_UnitID);
					if (unitTemplet == null || !unitTemplet.m_bExclude || NKCUICollectionOperatorList.IsHasOperator(unitList[i]))
					{
						if (createOperatorDataList)
						{
							list.Add(NKCOperatorUtil.GetDummyOperator(unitList[i], true));
						}
						if (NKCUICollectionOperatorList.IsHasOperator(unitList[i]))
						{
							collectionUnit++;
						}
						totalUnit++;
					}
				}
			}
			return list;
		}

		// Token: 0x0600900C RID: 36876 RVA: 0x0030F864 File Offset: 0x0030DA64
		private static bool IsHasOperator(int UnitID = 0)
		{
			bool flag = false;
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			return (armyData != null && !armyData.IsFirstGetUnit(UnitID)) || flag;
		}

		// Token: 0x0600900D RID: 36877 RVA: 0x0030F890 File Offset: 0x0030DA90
		public void Init(NKCUICollection.OnSyncCollectingData callBack)
		{
			if (null != this.m_LoopScrollRect)
			{
				this.m_LoopScrollRect.dOnGetObject += this.GetSlot;
				this.m_LoopScrollRect.dOnReturnObject += this.ReturnSlot;
				this.m_LoopScrollRect.dOnProvideData += this.ProvideSlotData;
			}
			if (this.m_SortUI != null)
			{
				this.m_SortUI.Init(new NKCUIComUnitSortOptions.OnSorted(this.OnSortChanged), true);
				this.m_SortUI.RegisterCategories(this.eOperatorFilterCategories, this.eOperatorSortCategories, false);
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
			this.m_bCellPrepared = false;
		}

		// Token: 0x0600900E RID: 36878 RVA: 0x0030F970 File Offset: 0x0030DB70
		public void Open()
		{
			this.m_iTotalUnit = 0;
			this.m_iCollectionUnit = 0;
			this.m_OperatorSortSystem = null;
			NKCUIUnitSelectList.UnitSelectListOptions currentOption = new NKCUIUnitSelectList.UnitSelectListOptions(NKM_UNIT_TYPE.NUT_OPERATOR, true, NKM_DECK_TYPE.NDT_NORMAL, NKCUIUnitSelectList.eUnitSelectListMode.Normal, true);
			currentOption.setOperatorFilterOption = new HashSet<NKCOperatorSortSystem.eFilterOption>();
			currentOption.lstOperatorSortOption = NKCOperatorSortSystem.GetDefaultSortOptions(true, false);
			currentOption.bShowHideDeckedUnitMenu = true;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_currentOption = currentOption;
			this.PrepareUnitList(false);
			this.UpdateCollectingRate();
		}

		// Token: 0x0600900F RID: 36879 RVA: 0x0030F9E0 File Offset: 0x0030DBE0
		public void Clear()
		{
			NKCUICollectionOperatorInfo.CheckInstanceAndClose();
			NKCScenManager.GetScenManager().m_NKCMemoryCleaner.UnloadObjectPool();
		}

		// Token: 0x06009010 RID: 36880 RVA: 0x0030F9F6 File Offset: 0x0030DBF6
		private void UpdateCollectingRate()
		{
			if (this.dOnOnSyncCollectingData != null)
			{
				this.dOnOnSyncCollectingData(NKCUICollection.CollectionType.CT_OPERATOR, this.m_iCollectionUnit, this.m_iTotalUnit);
			}
		}

		// Token: 0x06009011 RID: 36881 RVA: 0x0030FA18 File Offset: 0x0030DC18
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

		// Token: 0x06009012 RID: 36882 RVA: 0x0030FAA4 File Offset: 0x0030DCA4
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

		// Token: 0x06009013 RID: 36883 RVA: 0x0030FAE8 File Offset: 0x0030DCE8
		private void ProvideSlotData(Transform tr, int idx)
		{
			if (this.m_OperatorSortSystem == null)
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
			if (this.m_OperatorSortSystem.SortedOperatorList.Count <= idx)
			{
				return;
			}
			NKMOperator nkmoperator = this.m_OperatorSortSystem.SortedOperatorList[idx];
			long uid = nkmoperator.uid;
			NKMDeckIndex deckIndexCache = this.m_OperatorSortSystem.GetDeckIndexCache(uid, true);
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			component.SetDataForCollection(nkmoperator, deckIndexCache, new NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot(this.OnSlotSelected), NKCUICollectionOperatorList.IsHasOperator(nkmoperator.id));
			if (this.m_OperatorSortSystem.lstSortOption.Count > 0)
			{
				switch (this.m_OperatorSortSystem.lstSortOption[0])
				{
				case NKCOperatorSortSystem.eSortOption.Power_Low:
				case NKCOperatorSortSystem.eSortOption.Power_High:
					component.SetSortingTypeValue(true, NKCOperatorSortSystem.eSortOption.Power_High, this.m_OperatorSortSystem.GetUnitPowerCache(uid));
					goto IL_18A;
				case NKCOperatorSortSystem.eSortOption.Attack_Low:
				case NKCOperatorSortSystem.eSortOption.Attack_High:
					component.SetSortingTypeValue(true, NKCOperatorSortSystem.eSortOption.Attack_High, this.m_OperatorSortSystem.GetUnitAttackCache(uid));
					goto IL_18A;
				case NKCOperatorSortSystem.eSortOption.Health_Low:
				case NKCOperatorSortSystem.eSortOption.Health_High:
					component.SetSortingTypeValue(true, NKCOperatorSortSystem.eSortOption.Health_High, this.m_OperatorSortSystem.GetUnitHPCache(uid));
					goto IL_18A;
				case NKCOperatorSortSystem.eSortOption.Unit_Defense_Low:
				case NKCOperatorSortSystem.eSortOption.Unit_Defense_High:
					component.SetSortingTypeValue(true, NKCOperatorSortSystem.eSortOption.Unit_Defense_High, this.m_OperatorSortSystem.GetUnitDefCache(uid));
					goto IL_18A;
				case NKCOperatorSortSystem.eSortOption.Unit_ReduceSkillCool_Low:
				case NKCOperatorSortSystem.eSortOption.Unit_ReduceSkillCool_High:
					component.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Unit_ReduceSkillCool_High, this.m_OperatorSortSystem.GetUnitSkillCoolCache(uid));
					goto IL_18A;
				}
				component.SetSortingTypeValue(false, NKCOperatorSortSystem.eSortOption.Level_High, 0);
			}
			else
			{
				component.SetSortingTypeValue(false, NKCOperatorSortSystem.eSortOption.Level_High, 0);
			}
			IL_18A:
			component.SetSlotState(this.m_OperatorSortSystem.GetUnitSlotState(nkmoperator.uid));
			NKCUIUnitSelectList.UnitSelectListOptions.OnSlotOperatorSetData dOnSlotOperatorSetData = this.m_currentOption.dOnSlotOperatorSetData;
			if (dOnSlotOperatorSetData == null)
			{
				return;
			}
			dOnSlotOperatorSetData(component, nkmoperator, deckIndexCache);
		}

		// Token: 0x06009014 RID: 36884 RVA: 0x0030FCB0 File Offset: 0x0030DEB0
		private void OnSlotSelected(NKMOperator selectedUnit, NKMUnitTempletBase unitTempletBase, NKMDeckIndex selectedUnitDeckIndex, NKCUnitSortSystem.eUnitState unitSlotState, NKCUIUnitSelectList.eUnitSlotSelectState unitSlotSelectState)
		{
			if (selectedUnit == null)
			{
				return;
			}
			this.m_lstCurOperatorData = this.m_OperatorSortSystem.GetCurrentOperatorList();
			int num = -1;
			for (int i = 0; i < this.m_lstCurOperatorData.Count; i++)
			{
				if (this.m_lstCurOperatorData[i].id == selectedUnit.id)
				{
					num = i;
					break;
				}
			}
			if (num > -1)
			{
				if (!this.m_lstCurOperatorData.Contains(selectedUnit))
				{
					return;
				}
				NKCUIOperatorInfo.OpenOption openOption = new NKCUIOperatorInfo.OpenOption(this.m_lstCurOperatorData, num);
				NKCUICollectionOperatorInfo.Instance.Open(this.m_lstCurOperatorData[num], openOption, NKCUICollectionOperatorInfo.eCollectionState.CS_PROFILE, NKCUIUpsideMenu.eMode.Normal, false, false);
			}
		}

		// Token: 0x06009015 RID: 36885 RVA: 0x0030FD44 File Offset: 0x0030DF44
		private void PrepareUnitList(bool bForceRebuildList = false)
		{
			if (!this.m_bCellPrepared)
			{
				this.m_bCellPrepared = true;
				this.CalculateContentRectSize();
				this.m_LoopScrollRect.PrepareCells(0);
			}
			this.m_lstCollectionOperatorData = NKCUICollectionOperatorList.UpdateCollectionUnitList(ref this.m_iCollectionUnit, ref this.m_iTotalUnit, true);
			this.m_OperatorSortSystem = new NKCGenericOperatorSort(null, this.m_currentOption.m_OperatorSortOptions, this.m_lstCollectionOperatorData);
			this.m_SortUI.RegisterOperatorSort(this.m_OperatorSortSystem);
			this.m_SortUI.ResetUI(false);
			this.OnSortChanged(true);
		}

		// Token: 0x06009016 RID: 36886 RVA: 0x0030FDCB File Offset: 0x0030DFCB
		private void CalculateContentRectSize()
		{
			if (this.m_safeArea != null)
			{
				this.m_safeArea.SetSafeAreaBase();
			}
			NKCUtil.CalculateContentRectSize(this.m_LoopScrollRect, this.m_GridLayoutGroup, this.minColumnUnit, this.m_vUnitSlotSize, this.m_vUnitSlotSpacing, false);
		}

		// Token: 0x06009017 RID: 36887 RVA: 0x0030FE0C File Offset: 0x0030E00C
		private void OnSortChanged(bool bResetScroll)
		{
			if (this.m_OperatorSortSystem != null)
			{
				this.m_LoopScrollRect.TotalCount = this.m_OperatorSortSystem.SortedOperatorList.Count;
				if (bResetScroll)
				{
					this.m_LoopScrollRect.SetIndexPosition(0);
					return;
				}
				this.m_LoopScrollRect.RefreshCells(false);
			}
		}

		// Token: 0x04007D02 RID: 32002
		[Header("유닛 슬롯 프리팹 & 사이즈 설정")]
		public NKCUIUnitSelectListSlotBase m_pfbUnitSlot;

		// Token: 0x04007D03 RID: 32003
		public Vector2 m_vUnitSlotSize;

		// Token: 0x04007D04 RID: 32004
		public Vector2 m_vUnitSlotSpacing;

		// Token: 0x04007D05 RID: 32005
		[Header("UI Components")]
		public RectTransform m_rectContentRect;

		// Token: 0x04007D06 RID: 32006
		public RectTransform m_rectSlotPoolRect;

		// Token: 0x04007D07 RID: 32007
		public LoopScrollRect m_LoopScrollRect;

		// Token: 0x04007D08 RID: 32008
		public GridLayoutGroup m_GridLayoutGroup;

		// Token: 0x04007D09 RID: 32009
		public NKCUIComSafeArea m_safeArea;

		// Token: 0x04007D0A RID: 32010
		[Header("정렬/필터 통합UI")]
		public NKCUIComUnitSortOptions m_SortUI;

		// Token: 0x04007D0B RID: 32011
		[Header("유닛 설정")]
		public int minColumnUnit = 5;

		// Token: 0x04007D0C RID: 32012
		private int m_iTotalUnit;

		// Token: 0x04007D0D RID: 32013
		private int m_iCollectionUnit;

		// Token: 0x04007D0E RID: 32014
		private NKCOperatorSortSystem m_OperatorSortSystem;

		// Token: 0x04007D0F RID: 32015
		private readonly HashSet<NKCOperatorSortSystem.eFilterCategory> eOperatorFilterCategories = new HashSet<NKCOperatorSortSystem.eFilterCategory>
		{
			NKCOperatorSortSystem.eFilterCategory.Rarity
		};

		// Token: 0x04007D10 RID: 32016
		private readonly HashSet<NKCOperatorSortSystem.eSortCategory> eOperatorSortCategories = new HashSet<NKCOperatorSortSystem.eSortCategory>
		{
			NKCOperatorSortSystem.eSortCategory.IDX,
			NKCOperatorSortSystem.eSortCategory.Rarity,
			NKCOperatorSortSystem.eSortCategory.UnitPower,
			NKCOperatorSortSystem.eSortCategory.UnitAttack,
			NKCOperatorSortSystem.eSortCategory.UnitHealth,
			NKCOperatorSortSystem.eSortCategory.UnitDefense,
			NKCOperatorSortSystem.eSortCategory.UnitReduceSkillCool
		};

		// Token: 0x04007D11 RID: 32017
		private List<NKMOperator> m_lstCollectionOperatorData = new List<NKMOperator>();

		// Token: 0x04007D12 RID: 32018
		private NKCUIUnitSelectList.UnitSelectListOptions m_currentOption;

		// Token: 0x04007D13 RID: 32019
		private NKCUICollection.OnSyncCollectingData dOnOnSyncCollectingData;

		// Token: 0x04007D14 RID: 32020
		private List<NKCUIUnitSelectListSlotBase> m_lstVisibleSlot = new List<NKCUIUnitSelectListSlotBase>();

		// Token: 0x04007D15 RID: 32021
		private Stack<NKCUIUnitSelectListSlotBase> m_stkUnitSlotPool = new Stack<NKCUIUnitSelectListSlotBase>();

		// Token: 0x04007D16 RID: 32022
		private List<NKMOperator> m_lstCurOperatorData = new List<NKMOperator>();

		// Token: 0x04007D17 RID: 32023
		private bool m_bCellPrepared;
	}
}
