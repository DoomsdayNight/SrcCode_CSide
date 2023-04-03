using System;
using System.Collections.Generic;
using UnityEngine;

namespace NKC
{
	// Token: 0x020007F7 RID: 2039
	public class NKCPopupFilterSubUIMold : MonoBehaviour
	{
		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x060050C8 RID: 20680 RVA: 0x00187712 File Offset: 0x00185912
		public RectTransform RectTransform
		{
			get
			{
				if (this.m_RectTransform == null)
				{
					this.m_RectTransform = base.GetComponent<RectTransform>();
				}
				return this.m_RectTransform;
			}
		}

		// Token: 0x060050C9 RID: 20681 RVA: 0x00187734 File Offset: 0x00185934
		private void Init()
		{
			this.m_dicFilterBtn.Clear();
			this.SetToggleListner(this.m_tglAll, NKCMoldSortSystem.eFilterOption.Mold_Parts_All);
			this.SetToggleListner(this.m_tglWeapon, NKCMoldSortSystem.eFilterOption.Mold_Parts_Weapon);
			this.SetToggleListner(this.m_tglDefence, NKCMoldSortSystem.eFilterOption.Mold_Parts_Defence);
			this.SetToggleListner(this.m_tglAcc, NKCMoldSortSystem.eFilterOption.Mold_Parts_Acc);
			this.SetToggleListner(this.m_tglTier1, NKCMoldSortSystem.eFilterOption.Mold_Tier_1);
			this.SetToggleListner(this.m_tglTier2, NKCMoldSortSystem.eFilterOption.Mold_Tier_2);
			this.SetToggleListner(this.m_tglTier3, NKCMoldSortSystem.eFilterOption.Mold_Tier_3);
			this.SetToggleListner(this.m_tglTier4, NKCMoldSortSystem.eFilterOption.Mold_Tier_4);
			this.SetToggleListner(this.m_tglTier5, NKCMoldSortSystem.eFilterOption.Mold_Tier_5);
			this.SetToggleListner(this.m_tglTier6, NKCMoldSortSystem.eFilterOption.Mold_Tier_6);
			this.SetToggleListner(this.m_tglTier7, NKCMoldSortSystem.eFilterOption.Mold_Tier_7);
			this.SetToggleListner(this.m_tglNormal, NKCMoldSortSystem.eFilterOption.Mold_Type_Normal);
			this.SetToggleListner(this.m_tglRaid, NKCMoldSortSystem.eFilterOption.Mold_Type_Raid);
			this.SetToggleListner(this.m_tglEtc, NKCMoldSortSystem.eFilterOption.Mold_Type_Etc);
			this.SetToggleListner(this.m_tglEnable, NKCMoldSortSystem.eFilterOption.Mold_Status_Enable);
			this.SetToggleListner(this.m_tglDisable, NKCMoldSortSystem.eFilterOption.Mold_Status_Disable);
			this.SetToggleListner(this.m_tglTypeCounter, NKCMoldSortSystem.eFilterOption.Mold_Unit_Counter);
			this.SetToggleListner(this.m_tglTypeSoldier, NKCMoldSortSystem.eFilterOption.Mold_Unit_Soldier);
			this.SetToggleListner(this.m_tglTypeMechanic, NKCMoldSortSystem.eFilterOption.Mold_Unit_Mechanic);
			this.SetToggleListner(this.m_tglTypeEtc, NKCMoldSortSystem.eFilterOption.Mold_Unit_Etc);
			this.SetToggleListner(this.m_tglSSR, NKCMoldSortSystem.eFilterOption.Mold_Grade_SSR);
			this.SetToggleListner(this.m_tglSR, NKCMoldSortSystem.eFilterOption.Mold_Grade_SR);
			this.SetToggleListner(this.m_tglR, NKCMoldSortSystem.eFilterOption.Mold_Grade_R);
			this.SetToggleListner(this.m_tglN, NKCMoldSortSystem.eFilterOption.Mold_Grade_N);
			this.m_bInitComplete = true;
		}

		// Token: 0x060050CA RID: 20682 RVA: 0x0018789C File Offset: 0x00185A9C
		private void SetToggleListner(NKCUIComToggle toggle, NKCMoldSortSystem.eFilterOption filterOption)
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

		// Token: 0x060050CB RID: 20683 RVA: 0x001878FC File Offset: 0x00185AFC
		public void OpenFilterPopup(HashSet<NKCMoldSortSystem.eFilterOption> setFilterOption, NKCPopupFilterSubUIMold.OnFilterOptionChange onFilterOptionChange, List<string> lstFilter, bool bSelection = false)
		{
			if (!this.m_bInitComplete)
			{
				this.Init();
			}
			this.dOnFilterOptionChange = onFilterOptionChange;
			NKCUtil.SetGameobjectActive(this.m_objMoldParts, lstFilter.Contains("FT_EquipPosition"));
			NKCUtil.SetGameobjectActive(this.m_objMoldTier, lstFilter.Contains("FT_Tier"));
			NKCUtil.SetGameobjectActive(this.m_objMoldType, lstFilter.Contains("FT_ContentType"));
			NKCUtil.SetGameobjectActive(this.m_objEnableType, lstFilter.Contains("FT_Makeable"));
			NKCUtil.SetGameobjectActive(this.m_objUnitType, lstFilter.Contains("FT_UnitType"));
			NKCUtil.SetGameobjectActive(this.m_objGrade, lstFilter.Contains("FT_Grade"));
			NKCUtil.SetGameobjectActive(this.m_objMiscEnableType, false);
			this.SetFilter(setFilterOption);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
		}

		// Token: 0x060050CC RID: 20684 RVA: 0x001879C4 File Offset: 0x00185BC4
		private void SetFilter(HashSet<NKCMoldSortSystem.eFilterOption> setFilterOption)
		{
			this.ResetFilter();
			this.m_bReset = true;
			foreach (NKCMoldSortSystem.eFilterOption key in setFilterOption)
			{
				if (this.m_dicFilterBtn.ContainsKey(key) && this.m_dicFilterBtn[key] != null)
				{
					this.m_dicFilterBtn[key].Select(true, false, false);
				}
			}
			this.m_bReset = false;
		}

		// Token: 0x060050CD RID: 20685 RVA: 0x00187A58 File Offset: 0x00185C58
		private void OnFilterButton(bool bSelect, NKCMoldSortSystem.eFilterOption filterOption)
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
					NKCPopupFilterSubUIMold.OnFilterOptionChange onFilterOptionChange = this.dOnFilterOptionChange;
					if (onFilterOptionChange == null)
					{
						return;
					}
					onFilterOptionChange(filterOption);
				}
			}
		}

		// Token: 0x060050CE RID: 20686 RVA: 0x00187AB0 File Offset: 0x00185CB0
		public void ResetFilter()
		{
			this.m_bReset = true;
			NKCUIComToggle[] componentsInChildren = base.transform.GetComponentsInChildren<NKCUIComToggle>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Select(false, false, false);
			}
			this.m_bReset = false;
		}

		// Token: 0x060050CF RID: 20687 RVA: 0x00187AF2 File Offset: 0x00185CF2
		public void ResetMoldPartFilter(NKCMoldSortSystem.eFilterOption selectOption)
		{
			if (selectOption == NKCMoldSortSystem.eFilterOption.Mold_Status_Enable)
			{
				this.m_tglDisable.Select(false, true, false);
				return;
			}
			if (selectOption == NKCMoldSortSystem.eFilterOption.Mold_Status_Disable)
			{
				this.m_tglEnable.Select(false, true, false);
			}
		}

		// Token: 0x040040F2 RID: 16626
		[Header("MOLD_PARTS")]
		public GameObject m_objMoldParts;

		// Token: 0x040040F3 RID: 16627
		public NKCUIComToggle m_tglAll;

		// Token: 0x040040F4 RID: 16628
		public NKCUIComToggle m_tglWeapon;

		// Token: 0x040040F5 RID: 16629
		public NKCUIComToggle m_tglDefence;

		// Token: 0x040040F6 RID: 16630
		public NKCUIComToggle m_tglAcc;

		// Token: 0x040040F7 RID: 16631
		[Header("MOLD_TIER")]
		public GameObject m_objMoldTier;

		// Token: 0x040040F8 RID: 16632
		public NKCUIComToggle m_tglTier1;

		// Token: 0x040040F9 RID: 16633
		public NKCUIComToggle m_tglTier2;

		// Token: 0x040040FA RID: 16634
		public NKCUIComToggle m_tglTier3;

		// Token: 0x040040FB RID: 16635
		public NKCUIComToggle m_tglTier4;

		// Token: 0x040040FC RID: 16636
		public NKCUIComToggle m_tglTier5;

		// Token: 0x040040FD RID: 16637
		public NKCUIComToggle m_tglTier6;

		// Token: 0x040040FE RID: 16638
		public NKCUIComToggle m_tglTier7;

		// Token: 0x040040FF RID: 16639
		[Header("MOLD_TYPE")]
		public GameObject m_objMoldType;

		// Token: 0x04004100 RID: 16640
		public NKCUIComToggle m_tglNormal;

		// Token: 0x04004101 RID: 16641
		public NKCUIComToggle m_tglRaid;

		// Token: 0x04004102 RID: 16642
		public NKCUIComToggle m_tglEtc;

		// Token: 0x04004103 RID: 16643
		[Header("ENABLE_TYPE")]
		public GameObject m_objEnableType;

		// Token: 0x04004104 RID: 16644
		public NKCUIComToggle m_tglEnable;

		// Token: 0x04004105 RID: 16645
		public NKCUIComToggle m_tglDisable;

		// Token: 0x04004106 RID: 16646
		[Header("유닛 타입")]
		public GameObject m_objUnitType;

		// Token: 0x04004107 RID: 16647
		public NKCUIComToggle m_tglTypeCounter;

		// Token: 0x04004108 RID: 16648
		public NKCUIComToggle m_tglTypeSoldier;

		// Token: 0x04004109 RID: 16649
		public NKCUIComToggle m_tglTypeMechanic;

		// Token: 0x0400410A RID: 16650
		public NKCUIComToggle m_tglTypeEtc;

		// Token: 0x0400410B RID: 16651
		[Header("희귀도")]
		public GameObject m_objGrade;

		// Token: 0x0400410C RID: 16652
		public NKCUIComToggle m_tglSSR;

		// Token: 0x0400410D RID: 16653
		public NKCUIComToggle m_tglSR;

		// Token: 0x0400410E RID: 16654
		public NKCUIComToggle m_tglR;

		// Token: 0x0400410F RID: 16655
		public NKCUIComToggle m_tglN;

		// Token: 0x04004110 RID: 16656
		[Header("MISC_ENABLE_TYPE")]
		public GameObject m_objMiscEnableType;

		// Token: 0x04004111 RID: 16657
		public NKCUIComToggle m_tglMiscEnable;

		// Token: 0x04004112 RID: 16658
		public NKCUIComToggle m_tglMiscDisable;

		// Token: 0x04004113 RID: 16659
		private RectTransform m_RectTransform;

		// Token: 0x04004114 RID: 16660
		private Dictionary<NKCMoldSortSystem.eFilterOption, NKCUIComToggle> m_dicFilterBtn = new Dictionary<NKCMoldSortSystem.eFilterOption, NKCUIComToggle>();

		// Token: 0x04004115 RID: 16661
		private NKCPopupFilterSubUIMold.OnFilterOptionChange dOnFilterOptionChange;

		// Token: 0x04004116 RID: 16662
		private bool m_bInitComplete;

		// Token: 0x04004117 RID: 16663
		private bool m_bReset;

		// Token: 0x020014B6 RID: 5302
		// (Invoke) Token: 0x0600A9B5 RID: 43445
		public delegate void OnFilterOptionChange(NKCMoldSortSystem.eFilterOption filterOption);
	}
}
