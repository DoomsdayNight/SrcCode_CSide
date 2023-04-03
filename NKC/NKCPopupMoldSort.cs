using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NKC
{
	// Token: 0x020007F9 RID: 2041
	public class NKCPopupMoldSort : MonoBehaviour
	{
		// Token: 0x060050D3 RID: 20691 RVA: 0x00187BA4 File Offset: 0x00185DA4
		private void Init()
		{
			this.m_dicSortOption.Clear();
			if (this.m_cTglSortCraftable != null)
			{
				this.m_dicSortOption.Add(NKCMoldSortSystem.eSortOption.Craftable_High, this.m_cTglSortCraftable);
				this.m_dicSortOption.Add(NKCMoldSortSystem.eSortOption.Craftable_Low, this.m_cTglSortCraftable);
				this.m_cTglSortCraftable.OnValueChanged.RemoveAllListeners();
				this.m_cTglSortCraftable.OnValueChanged.AddListener(new UnityAction<bool>(this.OnSortCraftable));
			}
			if (this.m_cTglSortTypeTier != null)
			{
				this.m_dicSortOption.Add(NKCMoldSortSystem.eSortOption.Tier_High, this.m_cTglSortTypeTier);
				this.m_dicSortOption.Add(NKCMoldSortSystem.eSortOption.Tier_Low, this.m_cTglSortTypeTier);
				this.m_cTglSortTypeTier.OnValueChanged.RemoveAllListeners();
				this.m_cTglSortTypeTier.OnValueChanged.AddListener(new UnityAction<bool>(this.OnSortTier));
			}
			if (this.m_cTglSortTypeRarity != null)
			{
				this.m_dicSortOption.Add(NKCMoldSortSystem.eSortOption.Rarity_High, this.m_cTglSortTypeRarity);
				this.m_dicSortOption.Add(NKCMoldSortSystem.eSortOption.Rarity_Low, this.m_cTglSortTypeRarity);
				this.m_cTglSortTypeRarity.OnValueChanged.RemoveAllListeners();
				this.m_cTglSortTypeRarity.OnValueChanged.AddListener(new UnityAction<bool>(this.OnSortRarity));
			}
			this.m_bInitComplete = true;
		}

		// Token: 0x060050D4 RID: 20692 RVA: 0x00187CE0 File Offset: 0x00185EE0
		public void OpenMoldSortMenu(NKCMoldSortSystem.eSortOption selectedSortOption, NKCPopupMoldSort.OnSortOption onSortOption, bool bDescending, bool bValue, List<string> lstSort)
		{
			if (!this.m_bInitComplete)
			{
				this.Init();
			}
			NKCUtil.SetGameobjectActive(this.m_cTglSortCraftable.gameObject, false);
			NKCUtil.SetGameobjectActive(this.m_cTglSortTypeTier.gameObject, false);
			NKCUtil.SetGameobjectActive(this.m_cTglSortTypeTier.gameObject, false);
			if (lstSort.Count > 0)
			{
				for (int i = 0; i < lstSort.Count; i++)
				{
					if (lstSort[i].Contains("ST_Makeable"))
					{
						NKCUtil.SetGameobjectActive(this.m_cTglSortCraftable.gameObject, true);
					}
					else if (lstSort[i].Contains("ST_Tier"))
					{
						NKCUtil.SetGameobjectActive(this.m_cTglSortTypeTier.gameObject, true);
					}
					else if (lstSort[i].Contains("ST_Grade"))
					{
						NKCUtil.SetGameobjectActive(this.m_cTglSortTypeRarity.gameObject, true);
					}
				}
			}
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

		// Token: 0x060050D5 RID: 20693 RVA: 0x00187E30 File Offset: 0x00186030
		public void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060050D6 RID: 20694 RVA: 0x00187E40 File Offset: 0x00186040
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

		// Token: 0x060050D7 RID: 20695 RVA: 0x00187EC0 File Offset: 0x001860C0
		private void ResetSortMenu()
		{
			NKCUIComToggle[] componentsInChildren = base.transform.GetComponentsInChildren<NKCUIComToggle>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Select(false, true, true);
			}
		}

		// Token: 0x060050D8 RID: 20696 RVA: 0x00187EF4 File Offset: 0x001860F4
		public void OnSortCraftable(bool bSelect)
		{
			this.m_cTglSortCraftable.Select(bSelect, true, true);
			if (bSelect)
			{
				if (this.m_bDescending)
				{
					this.OnSort(NKCMoldSortSystem.eSortOption.Craftable_High);
					return;
				}
				this.OnSort(NKCMoldSortSystem.eSortOption.Craftable_Low);
			}
		}

		// Token: 0x060050D9 RID: 20697 RVA: 0x00187F1F File Offset: 0x0018611F
		public void OnSortTier(bool bSelect)
		{
			this.m_cTglSortTypeTier.Select(bSelect, true, true);
			if (bSelect)
			{
				if (this.m_bDescending)
				{
					this.OnSort(NKCMoldSortSystem.eSortOption.Tier_High);
					return;
				}
				this.OnSort(NKCMoldSortSystem.eSortOption.Tier_Low);
			}
		}

		// Token: 0x060050DA RID: 20698 RVA: 0x00187F4A File Offset: 0x0018614A
		public void OnSortRarity(bool bSelect)
		{
			this.m_cTglSortTypeRarity.Select(bSelect, true, true);
			if (bSelect)
			{
				if (this.m_bDescending)
				{
					this.OnSort(NKCMoldSortSystem.eSortOption.Rarity_High);
					return;
				}
				this.OnSort(NKCMoldSortSystem.eSortOption.Rarity_Low);
			}
		}

		// Token: 0x060050DB RID: 20699 RVA: 0x00187F75 File Offset: 0x00186175
		public void OnSort(NKCMoldSortSystem.eSortOption sortOption)
		{
			this.dOnSortOption(sortOption);
			this.Close();
		}

		// Token: 0x0400411B RID: 16667
		[Header("정렬 방식 선택")]
		public NKCUIRectMove m_rmSortTypeMenu;

		// Token: 0x0400411C RID: 16668
		public NKCUIComToggle m_cTglSortCraftable;

		// Token: 0x0400411D RID: 16669
		public NKCUIComToggle m_cTglSortTypeTier;

		// Token: 0x0400411E RID: 16670
		public NKCUIComToggle m_cTglSortTypeRarity;

		// Token: 0x0400411F RID: 16671
		private NKCPopupMoldSort.OnSortOption dOnSortOption;

		// Token: 0x04004120 RID: 16672
		private Dictionary<NKCMoldSortSystem.eSortOption, NKCUIComToggle> m_dicSortOption = new Dictionary<NKCMoldSortSystem.eSortOption, NKCUIComToggle>();

		// Token: 0x04004121 RID: 16673
		private bool m_bDescending;

		// Token: 0x04004122 RID: 16674
		private bool m_bInitComplete;

		// Token: 0x020014B8 RID: 5304
		// (Invoke) Token: 0x0600A9BB RID: 43451
		public delegate void OnSortOption(NKCMoldSortSystem.eSortOption lstSortOptions);
	}
}
