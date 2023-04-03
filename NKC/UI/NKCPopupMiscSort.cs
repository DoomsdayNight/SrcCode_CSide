using System;
using System.Collections.Generic;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x02000A6D RID: 2669
	public class NKCPopupMiscSort : MonoBehaviour
	{
		// Token: 0x060075BB RID: 30139 RVA: 0x002728BD File Offset: 0x00270ABD
		public void Init()
		{
			this.m_dicSortOption.Clear();
			this.AddSortOption(NKCMiscSortSystem.eSortCategory.Point, this.m_cTglSortTypePoint);
			this.AddSortOption(NKCMiscSortSystem.eSortCategory.Rarity, this.m_cTglSortTypeRarity);
			this.AddSortOption(NKCMiscSortSystem.eSortCategory.CanPlace, this.m_cTglSortTypeCanPlace);
			this.m_bInitComplete = true;
		}

		// Token: 0x060075BC RID: 30140 RVA: 0x002728F8 File Offset: 0x00270AF8
		private void AddSortOption(NKCMiscSortSystem.eSortCategory sortCategory, NKCUIComToggle tgl)
		{
			if (tgl != null)
			{
				tgl.m_DataInt = (int)sortCategory;
				this.m_dicSortOption.Add(NKCMiscSortSystem.GetSortOptionByCategory(sortCategory, true), tgl);
				this.m_dicSortOption.Add(NKCMiscSortSystem.GetSortOptionByCategory(sortCategory, false), tgl);
				this.m_dicToggle.Add(sortCategory, tgl);
				tgl.OnValueChanged.RemoveAllListeners();
				tgl.OnValueChangedWithData = new NKCUIComToggle.ValueChangedWithData(this.OnTglSortOption);
			}
		}

		// Token: 0x060075BD RID: 30141 RVA: 0x00272965 File Offset: 0x00270B65
		public void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060075BE RID: 30142 RVA: 0x00272974 File Offset: 0x00270B74
		public void OpenSortMenu(HashSet<NKCMiscSortSystem.eSortCategory> setCategory, NKCMiscSortSystem.eSortOption selectedSortOption, NKCPopupMiscSort.OnSortOption onSortOption, bool bOpen)
		{
			if (!this.m_bInitComplete)
			{
				this.Init();
			}
			if (bOpen)
			{
				this.dOnSortOption = onSortOption;
				this.m_bDescending = NKCMiscSortSystem.IsDescending(selectedSortOption);
				foreach (KeyValuePair<NKCMiscSortSystem.eSortCategory, NKCUIComToggle> keyValuePair in this.m_dicToggle)
				{
					NKCUtil.SetGameobjectActive(keyValuePair.Value, setCategory.Contains(keyValuePair.Key));
				}
				NKCUtil.SetGameobjectActive(base.gameObject, true);
				if (this.m_dicSortOption.ContainsKey(selectedSortOption) && this.m_dicSortOption[selectedSortOption] != null)
				{
					this.m_dicSortOption[selectedSortOption].Select(true, true, false);
				}
				else
				{
					this.ResetSortMenu();
				}
				this.StartRectMove(bOpen, true);
				return;
			}
			this.Close();
		}

		// Token: 0x060075BF RID: 30143 RVA: 0x00272A5C File Offset: 0x00270C5C
		public void StartRectMove(bool bOpen, bool bAnimate = true)
		{
			if (!this.m_bInitComplete)
			{
				this.Init();
			}
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

		// Token: 0x060075C0 RID: 30144 RVA: 0x00272AE8 File Offset: 0x00270CE8
		private void ResetSortMenu()
		{
			NKCUIComToggle[] componentsInChildren = base.transform.GetComponentsInChildren<NKCUIComToggle>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].gameObject.activeSelf)
				{
					componentsInChildren[i].Select(false, true, true);
				}
			}
		}

		// Token: 0x060075C1 RID: 30145 RVA: 0x00272B2C File Offset: 0x00270D2C
		private void OnTglSortOption(bool value, int data)
		{
			if (value)
			{
				NKCMiscSortSystem.eSortOption sortOptionByCategory = NKCMiscSortSystem.GetSortOptionByCategory((NKCMiscSortSystem.eSortCategory)data, this.m_bDescending);
				List<NKCMiscSortSystem.eSortOption> sortList;
				if (this.m_dicSortOptionDetails.TryGetValue(sortOptionByCategory, out sortList))
				{
					this.OnSort(sortList);
					return;
				}
				this.OnSort(sortOptionByCategory);
			}
		}

		// Token: 0x060075C2 RID: 30146 RVA: 0x00272B68 File Offset: 0x00270D68
		public void OnSort(List<NKCMiscSortSystem.eSortOption> sortList)
		{
			this.dOnSortOption(sortList);
			this.Close();
		}

		// Token: 0x060075C3 RID: 30147 RVA: 0x00272B7C File Offset: 0x00270D7C
		public void OnSort(NKCMiscSortSystem.eSortOption sortOption)
		{
			List<NKCMiscSortSystem.eSortOption> list = new List<NKCMiscSortSystem.eSortOption>();
			list.Add(sortOption);
			this.dOnSortOption(list);
			this.Close();
		}

		// Token: 0x04006221 RID: 25121
		[Header("정렬 방식 선택")]
		public NKCUIRectMove m_rmSortTypeMenu;

		// Token: 0x04006222 RID: 25122
		public NKCUIComToggle m_cTglSortTypePoint;

		// Token: 0x04006223 RID: 25123
		public NKCUIComToggle m_cTglSortTypeRarity;

		// Token: 0x04006224 RID: 25124
		public NKCUIComToggle m_cTglSortTypeCanPlace;

		// Token: 0x04006225 RID: 25125
		private NKCPopupMiscSort.OnSortOption dOnSortOption;

		// Token: 0x04006226 RID: 25126
		private Dictionary<NKCMiscSortSystem.eSortOption, NKCUIComToggle> m_dicSortOption = new Dictionary<NKCMiscSortSystem.eSortOption, NKCUIComToggle>();

		// Token: 0x04006227 RID: 25127
		private Dictionary<NKCMiscSortSystem.eSortCategory, NKCUIComToggle> m_dicToggle = new Dictionary<NKCMiscSortSystem.eSortCategory, NKCUIComToggle>();

		// Token: 0x04006228 RID: 25128
		private Dictionary<NKCMiscSortSystem.eSortOption, List<NKCMiscSortSystem.eSortOption>> m_dicSortOptionDetails = new Dictionary<NKCMiscSortSystem.eSortOption, List<NKCMiscSortSystem.eSortOption>>();

		// Token: 0x04006229 RID: 25129
		private bool m_bDescending;

		// Token: 0x0400622A RID: 25130
		private bool m_bInitComplete;

		// Token: 0x020017CE RID: 6094
		public enum SORT_OPEN_TYPE
		{
			// Token: 0x0400A78E RID: 42894
			Normal,
			// Token: 0x0400A78F RID: 42895
			Interior
		}

		// Token: 0x020017CF RID: 6095
		// (Invoke) Token: 0x0600B444 RID: 46148
		public delegate void OnSortOption(List<NKCMiscSortSystem.eSortOption> lstSortOptions);
	}
}
