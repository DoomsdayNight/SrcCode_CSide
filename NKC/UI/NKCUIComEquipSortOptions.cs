using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000934 RID: 2356
	public class NKCUIComEquipSortOptions : MonoBehaviour
	{
		// Token: 0x06005E1D RID: 24093 RVA: 0x001D2018 File Offset: 0x001D0218
		public void Init(NKCUIComEquipSortOptions.OnSorted onSorted)
		{
			this.dOnSorted = onSorted;
			if (this.m_ctgDescending != null)
			{
				this.m_ctgDescending.OnValueChanged.RemoveAllListeners();
				this.m_ctgDescending.OnValueChanged.AddListener(new UnityAction<bool>(this.OnCheckAscend));
			}
			if (this.m_btnFilterOption != null)
			{
				this.m_btnFilterOption.PointerClick.RemoveAllListeners();
				this.m_btnFilterOption.PointerClick.AddListener(new UnityAction(this.OnClickFilterBtn));
			}
			if (this.m_btnFilterSelected != null)
			{
				this.m_btnFilterSelected.PointerClick.RemoveAllListeners();
				this.m_btnFilterSelected.PointerClick.AddListener(new UnityAction(this.OnClickFilterBtn));
			}
			if (null != this.m_cbtnSortTypeMenu)
			{
				this.m_cbtnSortTypeMenu.OnValueChanged.RemoveAllListeners();
				this.m_cbtnSortTypeMenu.OnValueChanged.AddListener(new UnityAction<bool>(this.OnSortMenuOpen));
			}
			this.SetOpenSortingMenu(false, false);
		}

		// Token: 0x06005E1E RID: 24094 RVA: 0x001D211C File Offset: 0x001D031C
		public void RegisterCategories(HashSet<NKCEquipSortSystem.eFilterCategory> filterCategory, HashSet<NKCEquipSortSystem.eSortCategory> sortCategory)
		{
			this.m_setFilterCategory = filterCategory;
			this.m_setSortCategory = sortCategory;
		}

		// Token: 0x06005E1F RID: 24095 RVA: 0x001D212C File Offset: 0x001D032C
		public void RegisterEquipSort(NKCEquipSortSystem sortSystem)
		{
			this.m_SSCurrent = sortSystem;
			this.SetUIByCurrentSortSystem();
		}

		// Token: 0x06005E20 RID: 24096 RVA: 0x001D213C File Offset: 0x001D033C
		private void OnCheckAscend(bool bValue)
		{
			if (this.m_SSCurrent == null)
			{
				return;
			}
			if (this.m_SSCurrent.lstSortOption.Count == 0)
			{
				return;
			}
			this.m_SSCurrent.lstSortOption = this.m_NKCPopupEquipSort.ChangeAscend(this.m_SSCurrent.lstSortOption);
			this.ProcessUIFromCurrentDisplayedSortData(false, this.m_SSCurrent.FilterSet.Count > 0);
		}

		// Token: 0x06005E21 RID: 24097 RVA: 0x001D21A0 File Offset: 0x001D03A0
		private void OnClickFilterBtn()
		{
			if (this.m_SSCurrent == null)
			{
				return;
			}
			NKCPopupFilterEquip.Instance.Open(this.m_setFilterCategory, this.m_SSCurrent, new NKCPopupFilterEquip.OnEquipFilterSetChange(this.OnSelectFilter), this.m_SSCurrent.ExcludeFilterSet != null && !this.m_SSCurrent.ExcludeFilterSet.Contains(NKCEquipSortSystem.eFilterOption.Equip_Enchant));
		}

		// Token: 0x06005E22 RID: 24098 RVA: 0x001D21FC File Offset: 0x001D03FC
		private void OnSelectFilter(NKCEquipSortSystem ssActive)
		{
			if (this.m_SSCurrent == null || ssActive == null)
			{
				return;
			}
			this.m_SSCurrent = ssActive;
			this.ProcessUIFromCurrentDisplayedSortData(true, ssActive.FilterSet.Count > 0);
		}

		// Token: 0x06005E23 RID: 24099 RVA: 0x001D2228 File Offset: 0x001D0428
		private void OnSortMenuOpen(bool bValue)
		{
			if (this.m_SSCurrent == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objSortSelect, false);
			NKCEquipSortSystem.eSortOption selectedSortOption = (this.m_SSCurrent.lstSortOption.Count > 0) ? this.m_SSCurrent.lstSortOption[0] : this.defaultSortOption;
			this.m_NKCPopupEquipSort.OpenEquipSortMenu(this.m_setSortCategory, selectedSortOption, new NKCPopupEquipSort.OnSortOption(this.OnSort), this.m_ctgDescending.m_bSelect, true);
			this.SetOpenSortingMenu(bValue, true);
		}

		// Token: 0x06005E24 RID: 24100 RVA: 0x001D22A9 File Offset: 0x001D04A9
		private void SetOpenSortingMenu(bool bOpen, bool bAnimate = true)
		{
			this.m_cbtnSortTypeMenu.Select(bOpen, true, false);
			this.m_NKCPopupEquipSort.StartRectMove(bOpen, bAnimate);
		}

		// Token: 0x06005E25 RID: 24101 RVA: 0x001D22C7 File Offset: 0x001D04C7
		private void OnSort(List<NKCEquipSortSystem.eSortOption> sortOptionList)
		{
			if (this.m_SSCurrent == null)
			{
				return;
			}
			this.m_SSCurrent.lstSortOption = sortOptionList;
			this.ProcessUIFromCurrentDisplayedSortData(false, this.m_SSCurrent.FilterSet.Count > 0);
		}

		// Token: 0x06005E26 RID: 24102 RVA: 0x001D22F8 File Offset: 0x001D04F8
		private void ProcessUIFromCurrentDisplayedSortData(bool bResetScroll, bool bSelectedAnyFilter)
		{
			NKCUtil.SetGameobjectActive(this.m_btnFilterSelected, bSelectedAnyFilter);
			this.SetUIByCurrentSortSystem();
			NKCUIComEquipSortOptions.OnSorted onSorted = this.dOnSorted;
			if (onSorted == null)
			{
				return;
			}
			onSorted(bResetScroll);
		}

		// Token: 0x06005E27 RID: 24103 RVA: 0x001D2320 File Offset: 0x001D0520
		private void SetUIByCurrentSortSystem()
		{
			NKCUIComToggle ctgDescending = this.m_ctgDescending;
			if (ctgDescending != null)
			{
				ctgDescending.Select(NKCEquipSortSystem.GetDescendingBySorting(this.m_SSCurrent.lstSortOption), true, false);
			}
			if (this.m_SSCurrent.lstSortOption.Count == 0)
			{
				NKCUtil.SetLabelText(this.m_lbSortType, NKCUtilString.GET_STRING_I_D);
				NKCUtil.SetLabelText(this.m_lbSelectedSortType, NKCUtilString.GET_STRING_I_D);
				return;
			}
			NKCUtil.SetLabelText(this.m_lbSortType, NKCEquipSortSystem.GetSortName(this.m_SSCurrent.lstSortOption[0]));
			NKCUtil.SetLabelText(this.m_lbSelectedSortType, NKCEquipSortSystem.GetSortName(this.m_SSCurrent.lstSortOption[0]));
		}

		// Token: 0x06005E28 RID: 24104 RVA: 0x001D23C8 File Offset: 0x001D05C8
		public void ResetUI()
		{
			if (this.m_SSCurrent != null)
			{
				NKCUtil.SetGameobjectActive(this.m_btnFilterSelected, this.m_SSCurrent.FilterSet != null && this.m_SSCurrent.FilterSet.Count > 0);
				NKCUtil.SetGameobjectActive(this.m_objSortSelect, false);
				this.SetOpenSortingMenu(false, false);
				NKCUIComToggle ctgDescending = this.m_ctgDescending;
				if (ctgDescending != null)
				{
					ctgDescending.Select(NKCEquipSortSystem.GetDescendingBySorting(this.m_SSCurrent.lstSortOption), true, false);
				}
				if (this.m_SSCurrent.lstSortOption.Count == 0)
				{
					NKCUtil.SetLabelText(this.m_lbSortType, NKCUtilString.GET_STRING_I_D);
					NKCUtil.SetLabelText(this.m_lbSelectedSortType, NKCUtilString.GET_STRING_I_D);
					return;
				}
				NKCUtil.SetLabelText(this.m_lbSortType, NKCEquipSortSystem.GetSortName(this.m_SSCurrent.lstSortOption[0]));
				NKCUtil.SetLabelText(this.m_lbSelectedSortType, NKCEquipSortSystem.GetSortName(this.m_SSCurrent.lstSortOption[0]));
			}
		}

		// Token: 0x04004A47 RID: 19015
		[Header("내림차순/오름차순 토글")]
		public NKCUIComToggle m_ctgDescending;

		// Token: 0x04004A48 RID: 19016
		[Header("필터링 선택")]
		public NKCUIComStateButton m_btnFilterOption;

		// Token: 0x04004A49 RID: 19017
		public NKCUIComStateButton m_btnFilterSelected;

		// Token: 0x04004A4A RID: 19018
		[Header("정렬 방식 선택")]
		public NKCPopupEquipSort m_NKCPopupEquipSort;

		// Token: 0x04004A4B RID: 19019
		public NKCUIComToggle m_cbtnSortTypeMenu;

		// Token: 0x04004A4C RID: 19020
		public GameObject m_objSortSelect;

		// Token: 0x04004A4D RID: 19021
		public Text m_lbSortType;

		// Token: 0x04004A4E RID: 19022
		public Text m_lbSelectedSortType;

		// Token: 0x04004A4F RID: 19023
		private NKCUIComEquipSortOptions.OnSorted dOnSorted;

		// Token: 0x04004A50 RID: 19024
		private NKCEquipSortSystem.eSortOption defaultSortOption = NKCEquipSortSystem.eSortOption.Rarity_High;

		// Token: 0x04004A51 RID: 19025
		private NKCEquipSortSystem m_SSCurrent;

		// Token: 0x04004A52 RID: 19026
		private HashSet<NKCEquipSortSystem.eSortCategory> m_setSortCategory;

		// Token: 0x04004A53 RID: 19027
		private HashSet<NKCEquipSortSystem.eFilterCategory> m_setFilterCategory;

		// Token: 0x020015B9 RID: 5561
		// (Invoke) Token: 0x0600AE0A RID: 44554
		public delegate void OnSorted(bool bResetScroll);
	}
}
