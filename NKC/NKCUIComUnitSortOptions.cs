using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200094F RID: 2383
	public class NKCUIComUnitSortOptions : MonoBehaviour
	{
		// Token: 0x06005EF6 RID: 24310 RVA: 0x001D7B96 File Offset: 0x001D5D96
		public void ClearFilterSet()
		{
			if (this.m_SSCurrent != null)
			{
				this.m_SSCurrent.FilterSet.Clear();
			}
			if (this.m_OperatorSSCurrent != null)
			{
				this.m_OperatorSSCurrent.FilterSet.Clear();
			}
		}

		// Token: 0x06005EF7 RID: 24311 RVA: 0x001D7BC8 File Offset: 0x001D5DC8
		public HashSet<NKCUnitSortSystem.eFilterOption> GetUnitFilterOption()
		{
			if (this.m_SSCurrent != null)
			{
				return this.m_SSCurrent.FilterSet;
			}
			return null;
		}

		// Token: 0x06005EF8 RID: 24312 RVA: 0x001D7BDF File Offset: 0x001D5DDF
		public HashSet<NKCOperatorSortSystem.eFilterOption> GetOperatorFilterOption()
		{
			if (this.m_OperatorSSCurrent != null)
			{
				return this.m_OperatorSSCurrent.FilterSet;
			}
			return null;
		}

		// Token: 0x06005EF9 RID: 24313 RVA: 0x001D7BF6 File Offset: 0x001D5DF6
		public List<NKCUnitSortSystem.eSortOption> GetUnitSortOption()
		{
			if (this.m_SSCurrent != null)
			{
				return this.m_SSCurrent.lstSortOption;
			}
			return null;
		}

		// Token: 0x06005EFA RID: 24314 RVA: 0x001D7C0D File Offset: 0x001D5E0D
		public List<NKCOperatorSortSystem.eSortOption> GetOperatorSortOption()
		{
			if (this.m_OperatorSSCurrent != null)
			{
				return this.m_OperatorSSCurrent.lstSortOption;
			}
			return null;
		}

		// Token: 0x06005EFB RID: 24315 RVA: 0x001D7C24 File Offset: 0x001D5E24
		public void Init(NKCUIComUnitSortOptions.OnSorted onSorted, bool bIsCollection)
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
			NKCUtil.SetButtonClickDelegate(this.m_tglFavorite, new UnityAction<bool>(this.OnTglFavorite));
			if (this.m_tglFavorite != null)
			{
				this.m_tglFavorite.Select(false, true, false);
			}
			this.m_bUseFavorite = false;
			this.m_bFavoriteFilterActive = false;
			this.m_bIsCollection = bIsCollection;
			this.SetOpenSortingMenu(false, false);
		}

		// Token: 0x06005EFC RID: 24316 RVA: 0x001D7D71 File Offset: 0x001D5F71
		public void RegisterCategories(HashSet<NKCUnitSortSystem.eFilterCategory> filterCategory, HashSet<NKCUnitSortSystem.eSortCategory> sortCategory, bool bFavoriteFilterActive)
		{
			this.m_setFilterCategory = filterCategory;
			this.m_setSortCategory = sortCategory;
			this.m_setOprFilterCategory = new HashSet<NKCOperatorSortSystem.eFilterCategory>();
			this.m_setOprSortCategory = new HashSet<NKCOperatorSortSystem.eSortCategory>();
			this.m_bFavoriteFilterActive = bFavoriteFilterActive;
		}

		// Token: 0x06005EFD RID: 24317 RVA: 0x001D7D9E File Offset: 0x001D5F9E
		public void RegisterCategories(HashSet<NKCOperatorSortSystem.eFilterCategory> filterCategory, HashSet<NKCOperatorSortSystem.eSortCategory> sortCategory, bool bFavoriteFilterActive)
		{
			this.m_setFilterCategory = new HashSet<NKCUnitSortSystem.eFilterCategory>();
			this.m_setSortCategory = new HashSet<NKCUnitSortSystem.eSortCategory>();
			this.m_setOprFilterCategory = filterCategory;
			this.m_setOprSortCategory = sortCategory;
			this.m_bFavoriteFilterActive = bFavoriteFilterActive;
		}

		// Token: 0x06005EFE RID: 24318 RVA: 0x001D7DCB File Offset: 0x001D5FCB
		public void RegisterUnitSort(NKCUnitSortSystem sortSystem)
		{
			this.m_SSCurrent = sortSystem;
			this.m_OperatorSSCurrent = null;
			this.SetUIByCurrentSortSystem();
		}

		// Token: 0x06005EFF RID: 24319 RVA: 0x001D7DE1 File Offset: 0x001D5FE1
		public void RegisterOperatorSort(NKCOperatorSortSystem operatorSortSystem)
		{
			this.m_OperatorSSCurrent = operatorSortSystem;
			this.m_SSCurrent = null;
			this.SetUIByCurrentOperatorSortSystem();
		}

		// Token: 0x06005F00 RID: 24320 RVA: 0x001D7DF8 File Offset: 0x001D5FF8
		private void OnCheckAscend(bool bValue)
		{
			if (this.m_SSCurrent != null && this.m_SSCurrent.lstSortOption.Count > 0)
			{
				this.m_SSCurrent.Descending = bValue;
				this.m_SSCurrent.lstSortOption = NKCUnitSortSystem.ChangeAscend(this.m_SSCurrent.lstSortOption);
				this.ProcessUIFromCurrentDisplayedSortData(false, this.CheckAnyFilterSelected(this.m_SSCurrent.FilterSet));
				return;
			}
			if (this.m_OperatorSSCurrent != null && this.m_OperatorSSCurrent.lstSortOption.Count > 0)
			{
				this.m_OperatorSSCurrent.Descending = bValue;
				this.m_OperatorSSCurrent.lstSortOption = NKCOperatorSortSystem.ChangeAscend(this.m_OperatorSSCurrent.lstSortOption);
				this.ProcessUIFromCurrentDisplayedSortData(false, this.m_OperatorSSCurrent.FilterSet.Count > 0);
			}
		}

		// Token: 0x06005F01 RID: 24321 RVA: 0x001D7EBC File Offset: 0x001D60BC
		private void OnClickFilterBtn()
		{
			if (this.m_SSCurrent != null)
			{
				NKCPopupFilterUnit.Instance.Open(this.m_setFilterCategory, this.m_SSCurrent.FilterSet, new NKCPopupFilterUnit.OnFilterSetChange(this.OnSelectFilter), NKCPopupFilterUnit.FILTER_TYPE.UNIT);
				return;
			}
			if (this.m_OperatorSSCurrent != null)
			{
				NKCPopupFilterOperator.Instance.Open(this.m_OperatorSSCurrent, this.m_setOprFilterCategory, new NKCPopupFilterOperator.OnFilterSetChange(this.OnSelectFilter));
			}
		}

		// Token: 0x06005F02 RID: 24322 RVA: 0x001D7F24 File Offset: 0x001D6124
		private void OnSelectFilter(HashSet<NKCUnitSortSystem.eFilterOption> setFilterOption)
		{
			if (this.m_SSCurrent != null)
			{
				if (this.m_bFavoriteFilterActive)
				{
					this.m_SSCurrent.FilterSet.Add(NKCUnitSortSystem.eFilterOption.Favorite);
				}
				this.m_SSCurrent.FilterSet = setFilterOption;
				this.ProcessUIFromCurrentDisplayedSortData(true, this.CheckAnyFilterSelected(setFilterOption));
			}
		}

		// Token: 0x06005F03 RID: 24323 RVA: 0x001D7F63 File Offset: 0x001D6163
		private void OnSelectFilter(NKCOperatorSortSystem ssActive)
		{
			if (this.m_OperatorSSCurrent != null)
			{
				this.m_OperatorSSCurrent = ssActive;
				this.ProcessUIFromCurrentDisplayedSortData(true, ssActive.FilterSet.Count > 0);
			}
		}

		// Token: 0x06005F04 RID: 24324 RVA: 0x001D7F8C File Offset: 0x001D618C
		private void OnTglFavorite(bool value)
		{
			this.m_bFavoriteFilterActive = (value && this.m_bUseFavorite);
			if (this.m_bFavoriteFilterActive)
			{
				this.m_SSCurrent.FilterSet.Add(NKCUnitSortSystem.eFilterOption.Favorite);
			}
			else
			{
				this.m_SSCurrent.FilterSet.Remove(NKCUnitSortSystem.eFilterOption.Favorite);
			}
			this.m_SSCurrent.FilterList(this.m_SSCurrent.FilterSet, this.m_SSCurrent.bHideDeckedUnit);
			bool bSelectedAnyFilter = this.m_bFavoriteFilterActive ? (this.m_SSCurrent.FilterSet.Count > 1) : (this.m_SSCurrent.FilterSet.Count > 0);
			this.ProcessUIFromCurrentDisplayedSortData(true, bSelectedAnyFilter);
		}

		// Token: 0x06005F05 RID: 24325 RVA: 0x001D8035 File Offset: 0x001D6235
		private bool CheckAnyFilterSelected(HashSet<NKCUnitSortSystem.eFilterOption> setFilter)
		{
			if (setFilter.Count == 1 && setFilter.Contains(NKCUnitSortSystem.eFilterOption.Collection_HasAchieve))
			{
				return false;
			}
			if (!this.m_bFavoriteFilterActive)
			{
				return setFilter.Count > 0;
			}
			return setFilter.Count > 1;
		}

		// Token: 0x06005F06 RID: 24326 RVA: 0x001D8068 File Offset: 0x001D6268
		private void OnSortMenuOpen(bool bValue)
		{
			if (this.m_SSCurrent != null)
			{
				NKCUnitSortSystem.eSortOption selectedSortOption = (this.m_SSCurrent.lstSortOption.Count > 0) ? this.m_SSCurrent.lstSortOption[0] : this.defaultSortOption;
				List<string> list = new List<string>();
				if (this.m_SSCurrent.Options.lstCustomSortFunc != null)
				{
					foreach (KeyValuePair<NKCUnitSortSystem.eSortCategory, KeyValuePair<string, NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc>> keyValuePair in this.m_SSCurrent.Options.lstCustomSortFunc)
					{
						list.Add(keyValuePair.Value.Key);
					}
				}
				this.m_NKCPopupSort.OpenSortMenu(this.m_setSortCategory, selectedSortOption, new NKCPopupSort.OnSortOption(this.OnSort), true, NKM_UNIT_TYPE.NUT_NORMAL, this.m_bIsCollection, list);
			}
			else
			{
				if (this.m_OperatorSSCurrent == null)
				{
					return;
				}
				NKCOperatorSortSystem.eSortOption option = (this.m_OperatorSSCurrent.lstSortOption.Count > 0) ? this.m_OperatorSSCurrent.lstSortOption[0] : this.defaultOperatorSortOption;
				List<string> list2 = new List<string>();
				if (this.m_OperatorSSCurrent.Options.lstCustomSortFunc != null)
				{
					foreach (KeyValuePair<NKCOperatorSortSystem.eSortCategory, KeyValuePair<string, NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc>> keyValuePair2 in this.m_OperatorSSCurrent.Options.lstCustomSortFunc)
					{
						list2.Add(keyValuePair2.Value.Key);
					}
				}
				this.m_NKCPopupSort.OpenSortMenu(NKCOperatorSortSystem.ConvertSortCategory(this.m_setOprSortCategory), NKCOperatorSortSystem.ConvertSortOption(option), new NKCPopupSort.OnSortOption(this.OnSort), true, NKM_UNIT_TYPE.NUT_OPERATOR, this.m_bIsCollection, list2);
			}
			NKCUtil.SetGameobjectActive(this.m_objSortSelect, false);
			this.SetOpenSortingMenu(bValue, true);
		}

		// Token: 0x06005F07 RID: 24327 RVA: 0x001D824C File Offset: 0x001D644C
		private void SetOpenSortingMenu(bool bOpen, bool bAnimate = true)
		{
			this.m_cbtnSortTypeMenu.Select(bOpen, true, false);
			this.m_NKCPopupSort.StartRectMove(bOpen, bAnimate);
		}

		// Token: 0x06005F08 RID: 24328 RVA: 0x001D826C File Offset: 0x001D646C
		private void OnSort(List<NKCUnitSortSystem.eSortOption> sortOptionList)
		{
			if (this.m_SSCurrent != null)
			{
				this.m_SSCurrent.lstSortOption = sortOptionList;
				this.ProcessUIFromCurrentDisplayedSortData(false, this.CheckAnyFilterSelected(this.m_SSCurrent.FilterSet));
			}
			else
			{
				if (this.m_OperatorSSCurrent == null)
				{
					return;
				}
				this.m_OperatorSSCurrent.lstSortOption = NKCOperatorSortSystem.ConvertSortOption(sortOptionList);
				this.ProcessUIFromCurrentDisplayedSortData(false, this.m_OperatorSSCurrent.FilterSet.Count > 0);
			}
			NKCUtil.SetGameobjectActive(this.m_objSortSelect, true);
			this.SetOpenSortingMenu(false, true);
		}

		// Token: 0x06005F09 RID: 24329 RVA: 0x001D82F1 File Offset: 0x001D64F1
		private void ProcessUIFromCurrentDisplayedSortData(bool bResetScroll, bool bSelectedAnyFilter)
		{
			NKCUtil.SetGameobjectActive(this.m_btnFilterSelected, bSelectedAnyFilter);
			if (this.m_SSCurrent != null)
			{
				this.SetUIByCurrentSortSystem();
			}
			else if (this.m_OperatorSSCurrent != null)
			{
				this.SetUIByCurrentOperatorSortSystem();
			}
			NKCUIComUnitSortOptions.OnSorted onSorted = this.dOnSorted;
			if (onSorted == null)
			{
				return;
			}
			onSorted(bResetScroll);
		}

		// Token: 0x06005F0A RID: 24330 RVA: 0x001D8330 File Offset: 0x001D6530
		private string GetSortName(NKCUnitSortSystem.eSortOption sortOption)
		{
			if (this.m_SSCurrent != null)
			{
				string text = NKCUnitSortSystem.GetSortName(this.m_SSCurrent.lstSortOption[0]);
				if (string.IsNullOrEmpty(text))
				{
					NKCUnitSortSystem.eSortCategory sortCategoryFromOption = NKCUnitSortSystem.GetSortCategoryFromOption(sortOption);
					if (this.m_SSCurrent.Options.lstCustomSortFunc.ContainsKey(sortCategoryFromOption))
					{
						text = this.m_SSCurrent.Options.lstCustomSortFunc[sortCategoryFromOption].Key;
					}
				}
				return text;
			}
			if (this.m_OperatorSSCurrent != null)
			{
				string text2 = NKCOperatorSortSystem.GetSortName(this.m_OperatorSSCurrent.lstSortOption[0]);
				if (string.IsNullOrEmpty(text2))
				{
					NKCOperatorSortSystem.eSortCategory key = NKCOperatorSortSystem.ConvertSortCategory(NKCUnitSortSystem.GetSortCategoryFromOption(sortOption));
					if (this.m_OperatorSSCurrent.Options.lstCustomSortFunc.ContainsKey(key))
					{
						text2 = this.m_OperatorSSCurrent.Options.lstCustomSortFunc[key].Key;
					}
				}
				return text2;
			}
			return string.Empty;
		}

		// Token: 0x06005F0B RID: 24331 RVA: 0x001D841C File Offset: 0x001D661C
		private string GetSortName(NKCOperatorSortSystem.eSortOption sortOption)
		{
			if (this.m_OperatorSSCurrent != null)
			{
				string text = NKCOperatorSortSystem.GetSortName(this.m_OperatorSSCurrent.lstSortOption[0]);
				if (string.IsNullOrEmpty(text))
				{
					NKCOperatorSortSystem.eSortCategory sortCategoryFromOption = NKCOperatorSortSystem.GetSortCategoryFromOption(sortOption);
					if (this.m_OperatorSSCurrent.Options.lstCustomSortFunc.ContainsKey(sortCategoryFromOption))
					{
						text = this.m_OperatorSSCurrent.Options.lstCustomSortFunc[sortCategoryFromOption].Key;
					}
				}
				return text;
			}
			return string.Empty;
		}

		// Token: 0x06005F0C RID: 24332 RVA: 0x001D8498 File Offset: 0x001D6698
		private void SetUIByCurrentSortSystem()
		{
			if (this.m_ctgDescending != null)
			{
				this.m_ctgDescending.Select((this.m_SSCurrent != null) ? this.m_SSCurrent.Descending : this.m_OperatorSSCurrent.Descending, true, false);
			}
			if (((this.m_SSCurrent != null) ? this.m_SSCurrent.lstSortOption.Count : this.m_OperatorSSCurrent.lstSortOption.Count) == 0)
			{
				NKCUtil.SetLabelText(this.m_lbSortType, NKCUtilString.GET_STRING_I_D);
				NKCUtil.SetLabelText(this.m_lbSelectedSortType, NKCUtilString.GET_STRING_I_D);
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbSortType, this.GetSortName(this.m_SSCurrent.lstSortOption[0]));
				NKCUtil.SetLabelText(this.m_lbSelectedSortType, this.GetSortName(this.m_SSCurrent.lstSortOption[0]));
			}
			this.SetFilterByCurrentOption();
		}

		// Token: 0x06005F0D RID: 24333 RVA: 0x001D857C File Offset: 0x001D677C
		private void SetFilterByCurrentOption()
		{
			if (this.m_tglFavorite != null)
			{
				if (this.m_SSCurrent != null && this.m_SSCurrent.FilterSet != null)
				{
					this.m_tglFavorite.Select(this.m_SSCurrent.FilterSet.Contains(NKCUnitSortSystem.eFilterOption.Favorite), true, false);
					return;
				}
				this.m_tglFavorite.Select(false, true, false);
			}
		}

		// Token: 0x06005F0E RID: 24334 RVA: 0x001D85DC File Offset: 0x001D67DC
		private void SetUIByCurrentOperatorSortSystem()
		{
			if (this.m_ctgDescending != null)
			{
				this.m_ctgDescending.Select(this.m_OperatorSSCurrent.Descending, true, false);
			}
			if (this.m_OperatorSSCurrent.lstSortOption.Count == 0)
			{
				NKCUtil.SetLabelText(this.m_lbSortType, NKCUtilString.GET_STRING_I_D);
				NKCUtil.SetLabelText(this.m_lbSelectedSortType, NKCUtilString.GET_STRING_I_D);
				return;
			}
			NKCUtil.SetLabelText(this.m_lbSortType, this.GetSortName(this.m_OperatorSSCurrent.lstSortOption[0]));
			NKCUtil.SetLabelText(this.m_lbSelectedSortType, this.GetSortName(this.m_OperatorSSCurrent.lstSortOption[0]));
		}

		// Token: 0x06005F0F RID: 24335 RVA: 0x001D8688 File Offset: 0x001D6888
		public void ResetUI(bool bUseFavorite = false)
		{
			if (this.m_SSCurrent == null)
			{
				if (this.m_OperatorSSCurrent != null)
				{
					this.m_bUseFavorite = false;
					NKCUtil.SetGameobjectActive(this.m_tglFavorite, false);
					if (this.m_OperatorSSCurrent != null)
					{
						NKCUtil.SetGameobjectActive(this.m_btnFilterSelected, this.m_OperatorSSCurrent.FilterSet != null && this.m_OperatorSSCurrent.FilterSet.Count > 0);
						NKCUtil.SetGameobjectActive(this.m_objSortSelect, false);
						this.SetOpenSortingMenu(false, false);
						if (this.m_ctgDescending != null)
						{
							this.m_ctgDescending.Select(this.m_OperatorSSCurrent.Descending, true, false);
						}
						if (this.m_OperatorSSCurrent.lstSortOption == null || this.m_OperatorSSCurrent.lstSortOption.Count == 0)
						{
							NKCUtil.SetLabelText(this.m_lbSortType, NKCUtilString.GET_STRING_I_D);
							NKCUtil.SetLabelText(this.m_lbSelectedSortType, NKCUtilString.GET_STRING_I_D);
							return;
						}
						string sortName = this.GetSortName(this.m_OperatorSSCurrent.lstSortOption[0]);
						NKCUtil.SetLabelText(this.m_lbSortType, sortName);
						NKCUtil.SetLabelText(this.m_lbSelectedSortType, sortName);
					}
				}
				return;
			}
			this.m_bUseFavorite = bUseFavorite;
			NKCUtil.SetGameobjectActive(this.m_tglFavorite, this.m_bUseFavorite);
			this.SetFilterByCurrentOption();
			NKCUtil.SetGameobjectActive(this.m_btnFilterSelected, this.m_SSCurrent.FilterSet != null && this.m_SSCurrent.FilterSet.Count > 0);
			NKCUtil.SetGameobjectActive(this.m_objSortSelect, false);
			this.SetOpenSortingMenu(false, false);
			if (this.m_ctgDescending != null)
			{
				this.m_ctgDescending.Select(this.m_SSCurrent.Descending, true, false);
			}
			if (this.m_SSCurrent.lstSortOption == null || this.m_SSCurrent.lstSortOption.Count == 0)
			{
				NKCUtil.SetLabelText(this.m_lbSortType, NKCUtilString.GET_STRING_I_D);
				NKCUtil.SetLabelText(this.m_lbSelectedSortType, NKCUtilString.GET_STRING_I_D);
				return;
			}
			string sortName2 = this.GetSortName(this.m_SSCurrent.lstSortOption[0]);
			NKCUtil.SetLabelText(this.m_lbSortType, sortName2);
			NKCUtil.SetLabelText(this.m_lbSelectedSortType, sortName2);
		}

		// Token: 0x06005F10 RID: 24336 RVA: 0x001D889A File Offset: 0x001D6A9A
		public void AddSortOptionDetail(NKCUnitSortSystem.eSortOption sortOption, List<NKCUnitSortSystem.eSortOption> lstDetail)
		{
			NKCPopupSort nkcpopupSort = this.m_NKCPopupSort;
			if (nkcpopupSort == null)
			{
				return;
			}
			nkcpopupSort.AddSortOptionDetail(sortOption, lstDetail);
		}

		// Token: 0x06005F11 RID: 24337 RVA: 0x001D88B0 File Offset: 0x001D6AB0
		public bool IsLimitBreakState()
		{
			return this.m_SSCurrent != null && this.m_SSCurrent.lstSortOption != null && this.m_SSCurrent.lstSortOption.Count > 0 && (this.m_SSCurrent.lstSortOption[0] == NKCUnitSortSystem.eSortOption.LimitBreak_High || this.m_SSCurrent.lstSortOption[0] == NKCUnitSortSystem.eSortOption.LimitBreak_Low || this.m_SSCurrent.lstSortOption[0] == NKCUnitSortSystem.eSortOption.Transcendence_High || this.m_SSCurrent.lstSortOption[0] == NKCUnitSortSystem.eSortOption.Transcendence_Low);
		}

		// Token: 0x06005F12 RID: 24338 RVA: 0x001D8940 File Offset: 0x001D6B40
		public bool IsTacticUpdateState()
		{
			return this.m_SSCurrent != null && this.m_SSCurrent.lstSortOption != null && this.m_SSCurrent.lstSortOption.Count > 0 && (this.m_SSCurrent.lstSortOption[0] == NKCUnitSortSystem.eSortOption.TacticUpdatePossible_High || this.m_SSCurrent.lstSortOption[0] == NKCUnitSortSystem.eSortOption.TacticUpdatePossible_Low);
		}

		// Token: 0x04004B1A RID: 19226
		[Header("내림차순/오름차순 토글")]
		public NKCUIComToggle m_ctgDescending;

		// Token: 0x04004B1B RID: 19227
		[Header("필터링 선택")]
		public NKCUIComStateButton m_btnFilterOption;

		// Token: 0x04004B1C RID: 19228
		public NKCUIComStateButton m_btnFilterSelected;

		// Token: 0x04004B1D RID: 19229
		[Header("정렬 방식 선택")]
		public NKCPopupSort m_NKCPopupSort;

		// Token: 0x04004B1E RID: 19230
		public NKCUIComToggle m_cbtnSortTypeMenu;

		// Token: 0x04004B1F RID: 19231
		public GameObject m_objSortSelect;

		// Token: 0x04004B20 RID: 19232
		public Text m_lbSortType;

		// Token: 0x04004B21 RID: 19233
		public Text m_lbSelectedSortType;

		// Token: 0x04004B22 RID: 19234
		[Header("즐겨찾기")]
		public NKCUIComToggle m_tglFavorite;

		// Token: 0x04004B23 RID: 19235
		private bool m_bUseFavorite;

		// Token: 0x04004B24 RID: 19236
		private bool m_bFavoriteFilterActive;

		// Token: 0x04004B25 RID: 19237
		private NKCUIComUnitSortOptions.OnSorted dOnSorted;

		// Token: 0x04004B26 RID: 19238
		protected HashSet<NKCUnitSortSystem.eSortCategory> m_setSortCategory;

		// Token: 0x04004B27 RID: 19239
		protected HashSet<NKCUnitSortSystem.eFilterCategory> m_setFilterCategory;

		// Token: 0x04004B28 RID: 19240
		protected HashSet<NKCOperatorSortSystem.eSortCategory> m_setOprSortCategory;

		// Token: 0x04004B29 RID: 19241
		protected HashSet<NKCOperatorSortSystem.eFilterCategory> m_setOprFilterCategory;

		// Token: 0x04004B2A RID: 19242
		protected NKCUnitSortSystem.eSortOption defaultSortOption = NKCUnitSortSystem.eSortOption.Rarity_High;

		// Token: 0x04004B2B RID: 19243
		protected NKCOperatorSortSystem.eSortOption defaultOperatorSortOption = NKCOperatorSortSystem.eSortOption.Rarity_High;

		// Token: 0x04004B2C RID: 19244
		private NKCUnitSortSystem m_SSCurrent;

		// Token: 0x04004B2D RID: 19245
		private NKCOperatorSortSystem m_OperatorSSCurrent;

		// Token: 0x04004B2E RID: 19246
		private bool m_bIsCollection;

		// Token: 0x020015D4 RID: 5588
		// (Invoke) Token: 0x0600AE5F RID: 44639
		public delegate void OnSorted(bool bResetScroll);
	}
}
