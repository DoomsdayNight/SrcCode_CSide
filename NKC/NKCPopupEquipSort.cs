using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007F5 RID: 2037
	public class NKCPopupEquipSort : MonoBehaviour
	{
		// Token: 0x060050AE RID: 20654 RVA: 0x001865B8 File Offset: 0x001847B8
		private void Init()
		{
			this.m_dicSortOption.Clear();
			this.m_dicToggle.Clear();
			this.m_dicSortOptionDetails.Clear();
			this.AddSortOption(NKCEquipSortSystem.eSortCategory.Enhance, this.m_cTglSortTypeEnhance);
			this.AddSortOption(NKCEquipSortSystem.eSortCategory.Tier, this.m_cTglSortTypeTier);
			this.AddSortOption(NKCEquipSortSystem.eSortCategory.Rarity, this.m_cTglSortTypeRarity);
			this.AddSortOption(NKCEquipSortSystem.eSortCategory.UID, this.m_cTglSortTypeUID);
			this.AddSortOption(NKCEquipSortSystem.eSortCategory.SetOption, this.m_cTglSortTypeSetOption);
			if (this.m_arrayCustomSortMenu != null)
			{
				for (int i = 0; i < this.m_arrayCustomSortMenu.Length; i++)
				{
					if (i == 0)
					{
						this.AddSortOption(NKCEquipSortSystem.eSortCategory.Custom1, this.m_arrayCustomSortMenu[i].m_cTglSortTypeCustom);
					}
					else if (i == 1)
					{
						this.AddSortOption(NKCEquipSortSystem.eSortCategory.Custom2, this.m_arrayCustomSortMenu[i].m_cTglSortTypeCustom);
					}
					else
					{
						if (i != 2)
						{
							break;
						}
						this.AddSortOption(NKCEquipSortSystem.eSortCategory.Custom3, this.m_arrayCustomSortMenu[i].m_cTglSortTypeCustom);
					}
				}
			}
			this.m_bInitComplete = true;
		}

		// Token: 0x060050AF RID: 20655 RVA: 0x001866A4 File Offset: 0x001848A4
		private void AddSortOption(NKCEquipSortSystem.eSortCategory sortCategory, NKCUIComToggle tgl)
		{
			if (tgl != null)
			{
				tgl.m_DataInt = (int)sortCategory;
				this.m_dicSortOption.Add(NKCEquipSortSystem.GetSortOptionByCategory(sortCategory, true), tgl);
				this.m_dicSortOption.Add(NKCEquipSortSystem.GetSortOptionByCategory(sortCategory, false), tgl);
				this.m_dicToggle.Add(sortCategory, tgl);
				tgl.OnValueChanged.RemoveAllListeners();
				tgl.OnValueChangedWithData = new NKCUIComToggle.ValueChangedWithData(this.OnTglSortOption);
			}
		}

		// Token: 0x060050B0 RID: 20656 RVA: 0x00186714 File Offset: 0x00184914
		private void OnTglSortOption(bool value, int data)
		{
			if (value)
			{
				NKCEquipSortSystem.eSortOption sortOptionByCategory = NKCEquipSortSystem.GetSortOptionByCategory((NKCEquipSortSystem.eSortCategory)data, this.m_bDescending);
				List<NKCEquipSortSystem.eSortOption> lstSortOption;
				if (this.m_dicSortOptionDetails.TryGetValue(sortOptionByCategory, out lstSortOption))
				{
					this.OnSort(lstSortOption);
					return;
				}
				this.OnSort(sortOptionByCategory);
			}
		}

		// Token: 0x060050B1 RID: 20657 RVA: 0x00186750 File Offset: 0x00184950
		public void OpenEquipSortMenu(HashSet<NKCEquipSortSystem.eSortCategory> setSortCategory, NKCEquipSortSystem.eSortOption selectedSortOption, NKCPopupEquipSort.OnSortOption onSortOption, bool bDescending, bool bValue)
		{
			if (!this.m_bInitComplete)
			{
				this.Init();
			}
			NKCUtil.SetGameobjectActive(this.m_cTglSortCraftable, setSortCategory.Contains(NKCEquipSortSystem.eSortCategory.Craftable));
			NKCUtil.SetGameobjectActive(this.m_cTglSortTypeEnhance, setSortCategory.Contains(NKCEquipSortSystem.eSortCategory.Enhance));
			NKCUtil.SetGameobjectActive(this.m_cTglSortTypeTier, setSortCategory.Contains(NKCEquipSortSystem.eSortCategory.Tier));
			NKCUtil.SetGameobjectActive(this.m_cTglSortTypeRarity, setSortCategory.Contains(NKCEquipSortSystem.eSortCategory.Rarity));
			NKCUtil.SetGameobjectActive(this.m_cTglSortTypeUID, setSortCategory.Contains(NKCEquipSortSystem.eSortCategory.UID));
			if (bValue)
			{
				this.dOnSortOption = onSortOption;
				this.m_bDescending = bDescending;
				NKCUtil.SetGameobjectActive(base.gameObject, true);
				if (this.m_dicSortOption.ContainsKey(selectedSortOption) && this.m_dicSortOption[selectedSortOption] != null)
				{
					this.m_dicSortOption[selectedSortOption].Select(true, true, false);
				}
				else
				{
					this.ResetSortMenu();
				}
				this.StartRectMove(bValue, true);
				return;
			}
			this.Close();
		}

		// Token: 0x060050B2 RID: 20658 RVA: 0x00186834 File Offset: 0x00184A34
		public void OpenEquipSortMenu(NKCEquipSortSystem.eSortOption selectedSortOption, NKCPopupEquipSort.OnSortOption onSortOption, bool bDescending, bool bValue, NKCPopupEquipSort.SORT_OPEN_TYPE openType = NKCPopupEquipSort.SORT_OPEN_TYPE.NORMAL)
		{
			if (!this.m_bInitComplete)
			{
				this.Init();
			}
			NKCUtil.SetGameobjectActive(this.m_cTglSortCraftable, openType == NKCPopupEquipSort.SORT_OPEN_TYPE.CRAFT);
			NKCUtil.SetGameobjectActive(this.m_cTglSortTypeEnhance, openType == NKCPopupEquipSort.SORT_OPEN_TYPE.NORMAL);
			NKCUtil.SetGameobjectActive(this.m_cTglSortTypeUID, openType == NKCPopupEquipSort.SORT_OPEN_TYPE.NORMAL);
			NKCUtil.SetGameobjectActive(this.m_cTglSortTypeSetOption, openType == NKCPopupEquipSort.SORT_OPEN_TYPE.NORMAL);
			if (bValue)
			{
				this.dOnSortOption = onSortOption;
				this.m_bDescending = bDescending;
				NKCUtil.SetGameobjectActive(base.gameObject, true);
				if (this.m_dicSortOption.ContainsKey(selectedSortOption) && this.m_dicSortOption[selectedSortOption] != null)
				{
					this.m_dicSortOption[selectedSortOption].Select(true, true, false);
				}
				else
				{
					this.ResetSortMenu();
				}
				this.StartRectMove(bValue, true);
				return;
			}
			this.Close();
		}

		// Token: 0x060050B3 RID: 20659 RVA: 0x001868FC File Offset: 0x00184AFC
		public void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060050B4 RID: 20660 RVA: 0x0018690C File Offset: 0x00184B0C
		public void StartRectMove(bool bOpen, bool bAnimate = true)
		{
			if (!bAnimate)
			{
				this.m_rmSortTypeMenu.gameObject.SetActive(bOpen);
				this.m_rmSortTypeMenu.Set(bOpen ? "Open" : "Close");
				return;
			}
			if (bOpen)
			{
				this.m_rmSortTypeMenu.gameObject.SetActive(true);
				this.m_rmSortTypeMenu.Transit("Open", null);
				return;
			}
			this.m_rmSortTypeMenu.Transit("Close", delegate()
			{
				this.m_rmSortTypeMenu.gameObject.SetActive(false);
			});
		}

		// Token: 0x060050B5 RID: 20661 RVA: 0x0018698C File Offset: 0x00184B8C
		private void ResetSortMenu()
		{
			NKCUIComToggle[] componentsInChildren = base.transform.GetComponentsInChildren<NKCUIComToggle>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Select(false, true, true);
			}
		}

		// Token: 0x060050B6 RID: 20662 RVA: 0x001869C0 File Offset: 0x00184BC0
		public void OnSort(List<NKCEquipSortSystem.eSortOption> lstSortOption)
		{
			lstSortOption = NKCEquipSortSystem.AddDefaultSortOptions(lstSortOption);
			this.dOnSortOption(lstSortOption);
			this.Close();
		}

		// Token: 0x060050B7 RID: 20663 RVA: 0x001869DC File Offset: 0x00184BDC
		public void OnSort(NKCEquipSortSystem.eSortOption sortOption)
		{
			List<NKCEquipSortSystem.eSortOption> lstSortOptions = NKCEquipSortSystem.AddDefaultSortOptions(new List<NKCEquipSortSystem.eSortOption>
			{
				sortOption
			});
			this.dOnSortOption(lstSortOptions);
			this.Close();
		}

		// Token: 0x060050B8 RID: 20664 RVA: 0x00186A10 File Offset: 0x00184C10
		public List<NKCEquipSortSystem.eSortOption> ChangeAscend(List<NKCEquipSortSystem.eSortOption> targetList)
		{
			if (targetList == null || targetList.Count == 0)
			{
				return targetList;
			}
			switch (targetList[0])
			{
			case NKCEquipSortSystem.eSortOption.Enhance_High:
				targetList[0] = NKCEquipSortSystem.eSortOption.Enhance_Low;
				return targetList;
			case NKCEquipSortSystem.eSortOption.Enhance_Low:
				targetList[0] = NKCEquipSortSystem.eSortOption.Enhance_High;
				return targetList;
			case NKCEquipSortSystem.eSortOption.Tier_High:
				targetList[0] = NKCEquipSortSystem.eSortOption.Tier_Low;
				return targetList;
			case NKCEquipSortSystem.eSortOption.Tier_Low:
				targetList[0] = NKCEquipSortSystem.eSortOption.Tier_High;
				return targetList;
			case NKCEquipSortSystem.eSortOption.Rarity_High:
				targetList[0] = NKCEquipSortSystem.eSortOption.Rarity_Low;
				return targetList;
			case NKCEquipSortSystem.eSortOption.Rarity_Low:
				targetList[0] = NKCEquipSortSystem.eSortOption.Rarity_High;
				return targetList;
			case NKCEquipSortSystem.eSortOption.UID_First:
				targetList[0] = NKCEquipSortSystem.eSortOption.UID_Last;
				return targetList;
			case NKCEquipSortSystem.eSortOption.UID_Last:
				targetList[0] = NKCEquipSortSystem.eSortOption.UID_First;
				return targetList;
			case NKCEquipSortSystem.eSortOption.SetOption_High:
				targetList[0] = NKCEquipSortSystem.eSortOption.SetOption_Low;
				return targetList;
			case NKCEquipSortSystem.eSortOption.SetOption_Low:
				targetList[0] = NKCEquipSortSystem.eSortOption.SetOption_High;
				return targetList;
			case NKCEquipSortSystem.eSortOption.Equipped_First:
				targetList[0] = NKCEquipSortSystem.eSortOption.Equipped_Last;
				if (targetList[1] == NKCEquipSortSystem.eSortOption.Enhance_High)
				{
					targetList[1] = NKCEquipSortSystem.eSortOption.Enhance_Low;
					return targetList;
				}
				return targetList;
			case NKCEquipSortSystem.eSortOption.Equipped_Last:
				targetList[0] = NKCEquipSortSystem.eSortOption.Equipped_First;
				if (targetList[1] == NKCEquipSortSystem.eSortOption.Enhance_Low)
				{
					targetList[1] = NKCEquipSortSystem.eSortOption.Enhance_High;
					return targetList;
				}
				return targetList;
			}
			targetList.RemoveAt(0);
			this.ChangeAscend(targetList);
			return targetList;
		}

		// Token: 0x040040AA RID: 16554
		[Header("정렬 방식 선택")]
		public NKCUIRectMove m_rmSortTypeMenu;

		// Token: 0x040040AB RID: 16555
		public NKCUIComToggle m_cTglSortCraftable;

		// Token: 0x040040AC RID: 16556
		public NKCUIComToggle m_cTglSortTypeEnhance;

		// Token: 0x040040AD RID: 16557
		public NKCUIComToggle m_cTglSortTypeTier;

		// Token: 0x040040AE RID: 16558
		public NKCUIComToggle m_cTglSortTypeRarity;

		// Token: 0x040040AF RID: 16559
		public NKCUIComToggle m_cTglSortTypeUID;

		// Token: 0x040040B0 RID: 16560
		public NKCUIComToggle m_cTglSortTypeSetOption;

		// Token: 0x040040B1 RID: 16561
		[Header("커스텀 정렬 메뉴 텍스트")]
		public NKCPopupEquipSort.CustomSortMenu[] m_arrayCustomSortMenu;

		// Token: 0x040040B2 RID: 16562
		private NKCPopupEquipSort.OnSortOption dOnSortOption;

		// Token: 0x040040B3 RID: 16563
		private Dictionary<NKCEquipSortSystem.eSortCategory, NKCUIComToggle> m_dicToggle = new Dictionary<NKCEquipSortSystem.eSortCategory, NKCUIComToggle>();

		// Token: 0x040040B4 RID: 16564
		private Dictionary<NKCEquipSortSystem.eSortOption, List<NKCEquipSortSystem.eSortOption>> m_dicSortOptionDetails = new Dictionary<NKCEquipSortSystem.eSortOption, List<NKCEquipSortSystem.eSortOption>>();

		// Token: 0x040040B5 RID: 16565
		private Dictionary<NKCEquipSortSystem.eSortOption, NKCUIComToggle> m_dicSortOption = new Dictionary<NKCEquipSortSystem.eSortOption, NKCUIComToggle>();

		// Token: 0x040040B6 RID: 16566
		private bool m_bDescending;

		// Token: 0x040040B7 RID: 16567
		private bool m_bInitComplete;

		// Token: 0x020014B0 RID: 5296
		[Serializable]
		public struct CustomSortMenu
		{
			// Token: 0x04009ED8 RID: 40664
			public NKCUIComToggle m_cTglSortTypeCustom;

			// Token: 0x04009ED9 RID: 40665
			public Text m_lbCustomOffText;

			// Token: 0x04009EDA RID: 40666
			public Text m_lbCustomOnText;

			// Token: 0x04009EDB RID: 40667
			public Text m_lbCustomPressText;
		}

		// Token: 0x020014B1 RID: 5297
		public enum SORT_OPEN_TYPE
		{
			// Token: 0x04009EDD RID: 40669
			NORMAL,
			// Token: 0x04009EDE RID: 40670
			CRAFT,
			// Token: 0x04009EDF RID: 40671
			SELECTION,
			// Token: 0x04009EE0 RID: 40672
			OPERATION_POWER,
			// Token: 0x04009EE1 RID: 40673
			OPTION_WEIGHT
		}

		// Token: 0x020014B2 RID: 5298
		// (Invoke) Token: 0x0600A9A9 RID: 43433
		public delegate void OnSortOption(List<NKCEquipSortSystem.eSortOption> lstSortOptions);
	}
}
