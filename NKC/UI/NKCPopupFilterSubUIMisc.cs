using System;
using System.Collections.Generic;
using NKC.Templet;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x02000A52 RID: 2642
	public class NKCPopupFilterSubUIMisc : MonoBehaviour
	{
		// Token: 0x060073F5 RID: 29685 RVA: 0x00268F04 File Offset: 0x00267104
		private void Init()
		{
			this.m_dicFilterBtn.Clear();
			this.SetToggleListner(this.m_tglFloor, NKCMiscSortSystem.eFilterOption.InteriorTarget_Floor);
			this.SetToggleListner(this.m_tglTile, NKCMiscSortSystem.eFilterOption.InteriorTarget_Tile);
			this.SetToggleListner(this.m_tglWall, NKCMiscSortSystem.eFilterOption.InteriorTarget_Wall);
			this.SetToggleListner(this.m_tglBackground, NKCMiscSortSystem.eFilterOption.InteriorTarget_Background);
			this.SetToggleListner(this.m_tglDeco, NKCMiscSortSystem.eFilterOption.InteriorCategory_DECO);
			this.SetToggleListner(this.m_tglFurniture, NKCMiscSortSystem.eFilterOption.InteriorCategory_FURNITURE);
			this.SetToggleListner(this.m_tglCanPlace, NKCMiscSortSystem.eFilterOption.InteriorCanPlace);
			this.SetToggleListner(this.m_tglCanNotPlace, NKCMiscSortSystem.eFilterOption.InteriorCannotPlace);
			this.SetToggleListner(this.m_tglHave, NKCMiscSortSystem.eFilterOption.Have);
			this.SetToggleListner(this.m_tglNotHave, NKCMiscSortSystem.eFilterOption.NotHave);
			this.SetToggleListner(this.m_tglRare_SSR, NKCMiscSortSystem.eFilterOption.Tier_SSR);
			this.SetToggleListner(this.m_tglRare_SR, NKCMiscSortSystem.eFilterOption.Tier_SR);
			this.SetToggleListner(this.m_tglRare_R, NKCMiscSortSystem.eFilterOption.Tier_R);
			this.SetToggleListner(this.m_tglRare_N, NKCMiscSortSystem.eFilterOption.Tier_N);
			this.m_dicFilterBtn.Add(NKCMiscSortSystem.eFilterOption.Theme, this.m_tglTheme);
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglTheme, new UnityAction<bool>(this.OnTglTheme));
			this.m_bInitComplete = true;
		}

		// Token: 0x060073F6 RID: 29686 RVA: 0x0026900C File Offset: 0x0026720C
		private void SetToggleListner(NKCUIComToggle toggle, NKCMiscSortSystem.eFilterOption filterOption)
		{
			if (toggle != null)
			{
				this.m_dicFilterBtn.Add(filterOption, toggle);
				toggle.OnValueChanged.RemoveAllListeners();
				toggle.OnValueChanged.AddListener(delegate(bool value)
				{
					this.OnFilterButton(value, filterOption);
				});
			}
		}

		// Token: 0x060073F7 RID: 29687 RVA: 0x0026906C File Offset: 0x0026726C
		public void OpenFilterPopup(HashSet<NKCMiscSortSystem.eFilterOption> setFilterOption, HashSet<NKCMiscSortSystem.eFilterCategory> setFilterCategory, NKCPopupFilterSubUIMisc.OnFilterOptionChange onFilterOptionChange, int currentSelectedTheme)
		{
			if (!this.m_bInitComplete)
			{
				this.Init();
			}
			this.m_currentSelectedTheme = currentSelectedTheme;
			this.dOnFilterOptionChange = onFilterOptionChange;
			if (setFilterCategory == null)
			{
				setFilterCategory = new HashSet<NKCMiscSortSystem.eFilterCategory>();
			}
			this.SetFilter(setFilterOption);
			NKCUtil.SetGameobjectActive(this.m_objInteriorTarget, setFilterCategory.Contains(NKCMiscSortSystem.eFilterCategory.InteriorTarget));
			NKCUtil.SetGameobjectActive(this.m_objInteriorCategory, setFilterCategory.Contains(NKCMiscSortSystem.eFilterCategory.InteriorCategory));
			NKCUtil.SetGameobjectActive(this.m_objInteriorCanPlace, setFilterCategory.Contains(NKCMiscSortSystem.eFilterCategory.InteriorCanPlace));
			NKCUtil.SetGameobjectActive(this.m_objHave, setFilterCategory.Contains(NKCMiscSortSystem.eFilterCategory.Have));
			NKCUtil.SetGameobjectActive(this.m_objRare, setFilterCategory.Contains(NKCMiscSortSystem.eFilterCategory.Tier));
			NKCUtil.SetGameobjectActive(this.m_objTheme, setFilterCategory.Contains(NKCMiscSortSystem.eFilterCategory.Theme));
			NKCUtil.SetGameobjectActive(base.gameObject, true);
		}

		// Token: 0x060073F8 RID: 29688 RVA: 0x00269120 File Offset: 0x00267320
		private void SetFilter(HashSet<NKCMiscSortSystem.eFilterOption> setFilterOption)
		{
			this.ResetFilter(false);
			this.m_bReset = true;
			foreach (NKCMiscSortSystem.eFilterOption key in setFilterOption)
			{
				if (this.m_dicFilterBtn.ContainsKey(key) && this.m_dicFilterBtn[key] != null)
				{
					this.m_dicFilterBtn[key].Select(true, false, false);
				}
			}
			this.SetThemeButton();
			this.m_bReset = false;
		}

		// Token: 0x060073F9 RID: 29689 RVA: 0x002691BC File Offset: 0x002673BC
		private void OnFilterButton(bool bSelect, NKCMiscSortSystem.eFilterOption filterOption)
		{
			if (this.m_dicFilterBtn.ContainsKey(filterOption))
			{
				NKCUIComToggle nkcuicomToggle = this.m_dicFilterBtn[filterOption];
				if (nkcuicomToggle != null)
				{
					nkcuicomToggle.Select(bSelect, true, true);
					if (this.m_bReset)
					{
						return;
					}
					NKCPopupFilterSubUIMisc.OnFilterOptionChange onFilterOptionChange = this.dOnFilterOptionChange;
					if (onFilterOptionChange != null)
					{
						onFilterOptionChange(filterOption, this.m_currentSelectedTheme);
					}
				}
				this.SetThemeButton();
			}
		}

		// Token: 0x060073FA RID: 29690 RVA: 0x00269220 File Offset: 0x00267420
		public void ResetFilter(bool resetSelectedTheme)
		{
			if (resetSelectedTheme)
			{
				this.m_currentSelectedTheme = 0;
			}
			this.m_bReset = true;
			NKCUIComToggle[] componentsInChildren = base.transform.GetComponentsInChildren<NKCUIComToggle>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Select(false, false, false);
			}
			this.SetThemeButton();
			this.m_bReset = false;
		}

		// Token: 0x060073FB RID: 29691 RVA: 0x00269274 File Offset: 0x00267474
		private void OnTglTheme(bool value)
		{
			if (this.m_bReset)
			{
				return;
			}
			if (value)
			{
				NKCPopupFilterTheme.Instance.Open(new NKCPopupFilterTheme.OnSelectTheme(this.OnSelectTheme), this.m_currentSelectedTheme);
				this.m_tglTheme.Select(false, true, false);
				return;
			}
			this.OnSelectTheme(0);
		}

		// Token: 0x060073FC RID: 29692 RVA: 0x002692C0 File Offset: 0x002674C0
		private void OnSelectTheme(int themeID)
		{
			this.m_currentSelectedTheme = themeID;
			this.OnFilterButton(themeID != 0, NKCMiscSortSystem.eFilterOption.Theme);
		}

		// Token: 0x060073FD RID: 29693 RVA: 0x002692D8 File Offset: 0x002674D8
		private void SetThemeButton()
		{
			if (this.m_currentSelectedTheme == 0)
			{
				this.m_tglTheme.Select(false, true, false);
				this.m_tglTheme.SetTitleText(NKCStringTable.GetString("SI_PF_FILTER_THEME_OPTION_SELECT", false));
				return;
			}
			NKCThemeGroupTemplet nkcthemeGroupTemplet = NKCThemeGroupTemplet.Find(this.m_currentSelectedTheme);
			this.m_tglTheme.SetTitleText(NKCStringTable.GetString(nkcthemeGroupTemplet.GroupStringKey, false));
			this.m_tglTheme.Select(true, true, false);
		}

		// Token: 0x04005FEA RID: 24554
		[Header("해당 프리팹에서 사용하는것만 연결")]
		[Header("Interior Target")]
		public GameObject m_objInteriorTarget;

		// Token: 0x04005FEB RID: 24555
		public NKCUIComToggle m_tglFloor;

		// Token: 0x04005FEC RID: 24556
		public NKCUIComToggle m_tglTile;

		// Token: 0x04005FED RID: 24557
		public NKCUIComToggle m_tglWall;

		// Token: 0x04005FEE RID: 24558
		public NKCUIComToggle m_tglBackground;

		// Token: 0x04005FEF RID: 24559
		[Header("Interior Category")]
		public GameObject m_objInteriorCategory;

		// Token: 0x04005FF0 RID: 24560
		public NKCUIComToggle m_tglDeco;

		// Token: 0x04005FF1 RID: 24561
		public NKCUIComToggle m_tglFurniture;

		// Token: 0x04005FF2 RID: 24562
		[Header("Interior CanPlace")]
		public GameObject m_objInteriorCanPlace;

		// Token: 0x04005FF3 RID: 24563
		public NKCUIComToggle m_tglCanPlace;

		// Token: 0x04005FF4 RID: 24564
		public NKCUIComToggle m_tglCanNotPlace;

		// Token: 0x04005FF5 RID: 24565
		[Header("Have")]
		public GameObject m_objHave;

		// Token: 0x04005FF6 RID: 24566
		public NKCUIComToggle m_tglHave;

		// Token: 0x04005FF7 RID: 24567
		public NKCUIComToggle m_tglNotHave;

		// Token: 0x04005FF8 RID: 24568
		[Header("Rarity")]
		public GameObject m_objRare;

		// Token: 0x04005FF9 RID: 24569
		public NKCUIComToggle m_tglRare_SSR;

		// Token: 0x04005FFA RID: 24570
		public NKCUIComToggle m_tglRare_SR;

		// Token: 0x04005FFB RID: 24571
		public NKCUIComToggle m_tglRare_R;

		// Token: 0x04005FFC RID: 24572
		public NKCUIComToggle m_tglRare_N;

		// Token: 0x04005FFD RID: 24573
		[Header("Theme")]
		public GameObject m_objTheme;

		// Token: 0x04005FFE RID: 24574
		public NKCUIComToggle m_tglTheme;

		// Token: 0x04005FFF RID: 24575
		private int m_currentSelectedTheme;

		// Token: 0x04006000 RID: 24576
		private Dictionary<NKCMiscSortSystem.eFilterOption, NKCUIComToggle> m_dicFilterBtn = new Dictionary<NKCMiscSortSystem.eFilterOption, NKCUIComToggle>();

		// Token: 0x04006001 RID: 24577
		private NKCPopupFilterSubUIMisc.OnFilterOptionChange dOnFilterOptionChange;

		// Token: 0x04006002 RID: 24578
		private bool m_bInitComplete;

		// Token: 0x04006003 RID: 24579
		private bool m_bReset;

		// Token: 0x020017A3 RID: 6051
		// (Invoke) Token: 0x0600B3D8 RID: 46040
		public delegate void OnFilterOptionChange(NKCMiscSortSystem.eFilterOption filterOption, int selectedThemeID);
	}
}
