using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200093D RID: 2365
	public class NKCUIComMiscSortOptions : MonoBehaviour
	{
		// Token: 0x06005E85 RID: 24197 RVA: 0x001D54E5 File Offset: 0x001D36E5
		public void ClearFilterSet()
		{
			if (this.m_SSCurrent != null)
			{
				this.m_SSCurrent.FilterSet.Clear();
			}
		}

		// Token: 0x06005E86 RID: 24198 RVA: 0x001D54FF File Offset: 0x001D36FF
		public HashSet<NKCMiscSortSystem.eFilterOption> GetFilterOption()
		{
			if (this.m_SSCurrent != null)
			{
				return this.m_SSCurrent.FilterSet;
			}
			return null;
		}

		// Token: 0x06005E87 RID: 24199 RVA: 0x001D5516 File Offset: 0x001D3716
		public List<NKCMiscSortSystem.eSortOption> GetSortOption()
		{
			if (this.m_SSCurrent != null)
			{
				return this.m_SSCurrent.lstSortOption;
			}
			return null;
		}

		// Token: 0x06005E88 RID: 24200 RVA: 0x001D5530 File Offset: 0x001D3730
		public void Init(NKCUIComMiscSortOptions.OnSorted onSorted)
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

		// Token: 0x06005E89 RID: 24201 RVA: 0x001D5634 File Offset: 0x001D3834
		public void RegisterCategories(HashSet<NKCMiscSortSystem.eFilterCategory> filterCategory, HashSet<NKCMiscSortSystem.eSortCategory> sortCategory)
		{
			this.m_setFilterCategory = filterCategory;
			this.m_setSortCategory = sortCategory;
		}

		// Token: 0x06005E8A RID: 24202 RVA: 0x001D5644 File Offset: 0x001D3844
		public void RegisterMiscSort(NKCMiscSortSystem sortSystem)
		{
			this.m_SSCurrent = sortSystem;
			this.SetUIByCurrentSortSystem();
		}

		// Token: 0x06005E8B RID: 24203 RVA: 0x001D5654 File Offset: 0x001D3854
		private void OnCheckAscend(bool bValue)
		{
			if (this.m_SSCurrent != null && this.m_SSCurrent.lstSortOption.Count > 0)
			{
				this.m_SSCurrent.lstSortOption = NKCMiscSortSystem.ChangeAscend(this.m_SSCurrent.lstSortOption);
				this.ProcessUIFromCurrentDisplayedSortData(false, this.m_SSCurrent.FilterSet.Count > 0);
			}
		}

		// Token: 0x06005E8C RID: 24204 RVA: 0x001D56B1 File Offset: 0x001D38B1
		private void OnClickFilterBtn()
		{
			if (this.m_SSCurrent != null)
			{
				NKCPopupFilterMisc.Instance.Open(this.m_setFilterCategory, this.m_SSCurrent.FilterSet, new NKCPopupFilterMisc.OnMiscFilterSetChange(this.OnSelectFilter), this.m_SSCurrent.FilterStatType_ThemeID);
			}
		}

		// Token: 0x06005E8D RID: 24205 RVA: 0x001D56ED File Offset: 0x001D38ED
		private void OnSelectFilter(HashSet<NKCMiscSortSystem.eFilterOption> setFilterOption, int selectedTheme)
		{
			if (this.m_SSCurrent != null)
			{
				this.m_SSCurrent.FilterStatType_ThemeID = selectedTheme;
				this.m_SSCurrent.FilterSet = setFilterOption;
				this.ProcessUIFromCurrentDisplayedSortData(true, setFilterOption.Count > 0);
			}
		}

		// Token: 0x06005E8E RID: 24206 RVA: 0x001D5720 File Offset: 0x001D3920
		private void OnSortMenuOpen(bool bValue)
		{
			if (this.m_SSCurrent != null)
			{
				NKCMiscSortSystem.eSortOption selectedSortOption = (this.m_SSCurrent.lstSortOption.Count > 0) ? this.m_SSCurrent.lstSortOption[0] : this.defaultSortOption;
				new List<string>();
				this.m_NKCPopupSort.OpenSortMenu(this.m_setSortCategory, selectedSortOption, new NKCPopupMiscSort.OnSortOption(this.OnSort), true);
				NKCUtil.SetGameobjectActive(this.m_objSortSelect, false);
				this.SetOpenSortingMenu(bValue, true);
				return;
			}
		}

		// Token: 0x06005E8F RID: 24207 RVA: 0x001D579E File Offset: 0x001D399E
		private void SetOpenSortingMenu(bool bOpen, bool bAnimate = true)
		{
			this.m_cbtnSortTypeMenu.Select(bOpen, true, false);
			this.m_NKCPopupSort.StartRectMove(bOpen, bAnimate);
		}

		// Token: 0x06005E90 RID: 24208 RVA: 0x001D57BC File Offset: 0x001D39BC
		private void OnSort(List<NKCMiscSortSystem.eSortOption> sortOptionList)
		{
			if (this.m_SSCurrent != null)
			{
				this.m_SSCurrent.lstSortOption = sortOptionList;
				this.ProcessUIFromCurrentDisplayedSortData(false, this.m_SSCurrent.FilterSet.Count > 0);
				NKCUtil.SetGameobjectActive(this.m_objSortSelect, true);
				this.SetOpenSortingMenu(false, true);
				return;
			}
		}

		// Token: 0x06005E91 RID: 24209 RVA: 0x001D580E File Offset: 0x001D3A0E
		private void ProcessUIFromCurrentDisplayedSortData(bool bResetScroll, bool bSelectedAnyFilter)
		{
			NKCUtil.SetGameobjectActive(this.m_btnFilterSelected, bSelectedAnyFilter);
			this.SetUIByCurrentSortSystem();
			NKCUIComMiscSortOptions.OnSorted onSorted = this.dOnSorted;
			if (onSorted == null)
			{
				return;
			}
			onSorted(bResetScroll);
		}

		// Token: 0x06005E92 RID: 24210 RVA: 0x001D5833 File Offset: 0x001D3A33
		private string GetSortName(NKCMiscSortSystem.eSortOption sortOption)
		{
			if (this.m_SSCurrent != null)
			{
				return NKCMiscSortSystem.GetSortName(this.m_SSCurrent.lstSortOption[0]);
			}
			return string.Empty;
		}

		// Token: 0x06005E93 RID: 24211 RVA: 0x001D585C File Offset: 0x001D3A5C
		private void SetUIByCurrentSortSystem()
		{
			NKCUIComToggle ctgDescending = this.m_ctgDescending;
			if (ctgDescending != null)
			{
				ctgDescending.Select(NKCMiscSortSystem.IsDescending(this.m_SSCurrent.lstSortOption[0]), true, false);
			}
			if (this.m_SSCurrent != null && this.m_SSCurrent.lstSortOption.Count > 0)
			{
				string sortName = this.GetSortName(this.m_SSCurrent.lstSortOption[0]);
				NKCUtil.SetLabelText(this.m_lbSortType, sortName);
				NKCUtil.SetLabelText(this.m_lbSelectedSortType, sortName);
				return;
			}
			NKCUtil.SetLabelText(this.m_lbSortType, NKCUtilString.GET_STRING_SORT_RARITY);
			NKCUtil.SetLabelText(this.m_lbSelectedSortType, NKCUtilString.GET_STRING_SORT_RARITY);
		}

		// Token: 0x06005E94 RID: 24212 RVA: 0x001D5900 File Offset: 0x001D3B00
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
					ctgDescending.Select(NKCMiscSortSystem.IsDescending(this.m_SSCurrent.lstSortOption[0]), true, false);
				}
				if (this.m_SSCurrent.lstSortOption != null && this.m_SSCurrent.lstSortOption.Count > 0)
				{
					string sortName = this.GetSortName(this.m_SSCurrent.lstSortOption[0]);
					NKCUtil.SetLabelText(this.m_lbSortType, sortName);
					NKCUtil.SetLabelText(this.m_lbSelectedSortType, sortName);
					return;
				}
				NKCUtil.SetLabelText(this.m_lbSortType, NKCUtilString.GET_STRING_SORT_RARITY);
				NKCUtil.SetLabelText(this.m_lbSelectedSortType, NKCUtilString.GET_STRING_SORT_RARITY);
			}
		}

		// Token: 0x06005E95 RID: 24213 RVA: 0x001D59F5 File Offset: 0x001D3BF5
		public void AddSortOptionDetail(NKCMiscSortSystem.eSortOption sortOption, List<NKCMiscSortSystem.eSortOption> lstDetail)
		{
		}

		// Token: 0x04004AA7 RID: 19111
		[Header("내림차순/오름차순 토글")]
		public NKCUIComToggle m_ctgDescending;

		// Token: 0x04004AA8 RID: 19112
		[Header("필터링 선택")]
		public NKCUIComStateButton m_btnFilterOption;

		// Token: 0x04004AA9 RID: 19113
		public NKCUIComStateButton m_btnFilterSelected;

		// Token: 0x04004AAA RID: 19114
		[Header("정렬 방식 선택")]
		public NKCPopupMiscSort m_NKCPopupSort;

		// Token: 0x04004AAB RID: 19115
		public NKCUIComToggle m_cbtnSortTypeMenu;

		// Token: 0x04004AAC RID: 19116
		public GameObject m_objSortSelect;

		// Token: 0x04004AAD RID: 19117
		public Text m_lbSortType;

		// Token: 0x04004AAE RID: 19118
		public Text m_lbSelectedSortType;

		// Token: 0x04004AAF RID: 19119
		private NKCUIComMiscSortOptions.OnSorted dOnSorted;

		// Token: 0x04004AB0 RID: 19120
		private NKCMiscSortSystem.eSortOption defaultSortOption = NKCMiscSortSystem.eSortOption.Rarity_High;

		// Token: 0x04004AB1 RID: 19121
		private NKCMiscSortSystem m_SSCurrent;

		// Token: 0x04004AB2 RID: 19122
		protected HashSet<NKCMiscSortSystem.eSortCategory> m_setSortCategory;

		// Token: 0x04004AB3 RID: 19123
		protected HashSet<NKCMiscSortSystem.eFilterCategory> m_setFilterCategory;

		// Token: 0x020015CA RID: 5578
		// (Invoke) Token: 0x0600AE4E RID: 44622
		public delegate void OnSorted(bool bResetScroll);
	}
}
