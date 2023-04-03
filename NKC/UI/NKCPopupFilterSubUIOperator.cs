using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x02000A53 RID: 2643
	public class NKCPopupFilterSubUIOperator : MonoBehaviour
	{
		// Token: 0x17001358 RID: 4952
		// (get) Token: 0x060073FF RID: 29695 RVA: 0x00269358 File Offset: 0x00267558
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

		// Token: 0x17001359 RID: 4953
		// (get) Token: 0x06007400 RID: 29696 RVA: 0x0026937A File Offset: 0x0026757A
		public bool IsSubfilterOpened
		{
			get
			{
				return this.m_objSubFilter != null && this.m_objSubFilter.activeSelf;
			}
		}

		// Token: 0x06007401 RID: 29697 RVA: 0x00269397 File Offset: 0x00267597
		public void CloseSubFilter()
		{
			this.m_subFilter.Close();
		}

		// Token: 0x06007402 RID: 29698 RVA: 0x002693A4 File Offset: 0x002675A4
		private void Init()
		{
			this.m_dicFilterBtn.Clear();
			this.SetToggleListner(this.m_passiveSlot, NKCOperatorSortSystem.eFilterOption.PassiveSkill);
			this.SetToggleListner(this.m_tglHave, NKCOperatorSortSystem.eFilterOption.Have);
			this.SetToggleListner(this.m_tglNotHave, NKCOperatorSortSystem.eFilterOption.NotHave);
			this.SetToggleListner(this.m_tglRare_SSR, NKCOperatorSortSystem.eFilterOption.Rarily_SSR);
			this.SetToggleListner(this.m_tglRare_SR, NKCOperatorSortSystem.eFilterOption.Rarily_SR);
			this.SetToggleListner(this.m_tglRare_R, NKCOperatorSortSystem.eFilterOption.Rarily_R);
			this.SetToggleListner(this.m_tglRare_N, NKCOperatorSortSystem.eFilterOption.Rarily_N);
			this.SetToggleListner(this.m_tglLevel_1, NKCOperatorSortSystem.eFilterOption.Level_1);
			this.SetToggleListner(this.m_tglLevel_Other, NKCOperatorSortSystem.eFilterOption.Level_other);
			this.SetToggleListner(this.m_tglLevel_Max, NKCOperatorSortSystem.eFilterOption.Level_Max);
			this.SetToggleListner(this.m_tglDecked, NKCOperatorSortSystem.eFilterOption.Decked);
			this.SetToggleListner(this.m_tglWait, NKCOperatorSortSystem.eFilterOption.NotDecked);
			this.SetToggleListner(this.m_tglLocked, NKCOperatorSortSystem.eFilterOption.Locked);
			this.SetToggleListner(this.m_tglUnlocked, NKCOperatorSortSystem.eFilterOption.Unlocked);
			this.SetToggleListner(this.m_tglCollected, NKCOperatorSortSystem.eFilterOption.Collected);
			this.SetToggleListner(this.m_tglNotCollected, NKCOperatorSortSystem.eFilterOption.NotCollected);
			this.m_bInitComplete = true;
		}

		// Token: 0x06007403 RID: 29699 RVA: 0x0026949C File Offset: 0x0026769C
		public void Close()
		{
			NKCUtil.SetGameobjectActive(this.m_objSubFilter, false);
		}

		// Token: 0x06007404 RID: 29700 RVA: 0x002694AC File Offset: 0x002676AC
		private void SetToggleListner(NKCUIComToggle toggle, NKCOperatorSortSystem.eFilterOption filterOption)
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

		// Token: 0x06007405 RID: 29701 RVA: 0x0026950C File Offset: 0x0026770C
		private void SetToggleListner(NKCPopupFilterSubUIOperatorPassiveSlot slot, NKCOperatorSortSystem.eFilterOption filterOption)
		{
			if (slot != null)
			{
				this.m_dicFilterPassiveSlot.Add(filterOption, slot);
				slot.SetData(null, false);
				NKCUIComStateButton button = slot.GetButton();
				if (button != null)
				{
					button.PointerClick.RemoveAllListeners();
					button.PointerClick.AddListener(delegate()
					{
						this.OpenPassiveSkillPopup();
					});
				}
			}
		}

		// Token: 0x06007406 RID: 29702 RVA: 0x00269569 File Offset: 0x00267769
		public void OpenFilterPopup(NKCOperatorSortSystem ssActive, NKCPopupFilterSubUIOperator.OnFilterOptionChange onFilterOptionChange, NKCOperatorSortSystem.FILTER_OPEN_TYPE filterOpenType)
		{
			this.OpenFilterPopup(ssActive, NKCOperatorSortSystem.MakeDefaultFilterCategory(filterOpenType), onFilterOptionChange);
		}

		// Token: 0x06007407 RID: 29703 RVA: 0x0026957C File Offset: 0x0026777C
		public void OpenFilterPopup(NKCOperatorSortSystem ssActive, HashSet<NKCOperatorSortSystem.eFilterCategory> setFilterCategory, NKCPopupFilterSubUIOperator.OnFilterOptionChange onFilterOptionChange)
		{
			if (!this.m_bInitComplete)
			{
				this.Init();
			}
			this.m_ssActive = ssActive;
			this.dOnFilterOptionChange = onFilterOptionChange;
			this.SetFilter(this.m_ssActive.FilterSet);
			NKCUtil.SetGameobjectActive(this.m_objHave, setFilterCategory.Contains(NKCOperatorSortSystem.eFilterCategory.Have));
			NKCUtil.SetGameobjectActive(this.m_objRare, setFilterCategory.Contains(NKCOperatorSortSystem.eFilterCategory.Rarity));
			NKCUtil.SetGameobjectActive(this.m_objLevel, setFilterCategory.Contains(NKCOperatorSortSystem.eFilterCategory.Level));
			NKCUtil.SetGameobjectActive(this.m_objDeck, setFilterCategory.Contains(NKCOperatorSortSystem.eFilterCategory.Decked));
			NKCUtil.SetGameobjectActive(this.m_objLock, setFilterCategory.Contains(NKCOperatorSortSystem.eFilterCategory.Locked));
			NKCUtil.SetGameobjectActive(this.m_objCollected, setFilterCategory.Contains(NKCOperatorSortSystem.eFilterCategory.Collected));
			NKCUtil.SetGameobjectActive(this.m_objPassiveSkill, setFilterCategory.Contains(NKCOperatorSortSystem.eFilterCategory.PassiveSkill));
			NKCUtil.SetGameobjectActive(base.gameObject, true);
		}

		// Token: 0x06007408 RID: 29704 RVA: 0x00269640 File Offset: 0x00267840
		public void OpenPassiveSkillPopup()
		{
			GameObject objSubFilter = this.m_objSubFilter;
			if (objSubFilter != null && !objSubFilter.activeSelf)
			{
				this.m_subFilter.Open(this.m_ssActive.Options.passiveSkillID, new NKCPopupFilterSubUIOperatorPassive.OnClickSkillSlot(this.OnFilterButton), OperatorSkillType.m_Passive);
				return;
			}
			this.m_subFilter.Close();
		}

		// Token: 0x06007409 RID: 29705 RVA: 0x00269698 File Offset: 0x00267898
		private void SetFilter(HashSet<NKCOperatorSortSystem.eFilterOption> setFilterOption)
		{
			this.ResetFilterSlot();
			this.m_bReset = true;
			foreach (NKCOperatorSortSystem.eFilterOption key in setFilterOption)
			{
				if (this.m_dicFilterBtn.ContainsKey(key) && this.m_dicFilterBtn[key] != null)
				{
					this.m_dicFilterBtn[key].Select(true, false, false);
				}
			}
			if (setFilterOption.Contains(NKCOperatorSortSystem.eFilterOption.PassiveSkill))
			{
				this.m_dicFilterPassiveSlot[NKCOperatorSortSystem.eFilterOption.PassiveSkill].SetData(this.m_ssActive.m_PassiveSkillID, this.m_ssActive.m_PassiveSkillID != 0);
			}
			this.m_bReset = false;
		}

		// Token: 0x0600740A RID: 29706 RVA: 0x00269760 File Offset: 0x00267960
		private void OnFilterButton(bool bSelect, NKCOperatorSortSystem.eFilterOption filterOption)
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
					if (this.m_ssActive.FilterSet.Contains(filterOption))
					{
						this.m_ssActive.FilterSet.Remove(filterOption);
					}
					else
					{
						this.m_ssActive.FilterSet.Add(filterOption);
					}
					NKCPopupFilterSubUIOperator.OnFilterOptionChange onFilterOptionChange = this.dOnFilterOptionChange;
					if (onFilterOptionChange == null)
					{
						return;
					}
					onFilterOptionChange(this.m_ssActive);
				}
			}
		}

		// Token: 0x0600740B RID: 29707 RVA: 0x002697F4 File Offset: 0x002679F4
		private void OnFilterButton(int selectedSkillID)
		{
			if (this.m_dicFilterPassiveSlot.ContainsKey(NKCOperatorSortSystem.eFilterOption.PassiveSkill))
			{
				if (this.m_ssActive.FilterSet.Contains(NKCOperatorSortSystem.eFilterOption.PassiveSkill))
				{
					if (this.m_ssActive.m_PassiveSkillID == selectedSkillID)
					{
						this.m_ssActive.FilterSet.Remove(NKCOperatorSortSystem.eFilterOption.PassiveSkill);
						this.m_ssActive.m_PassiveSkillID = 0;
					}
					else
					{
						this.m_ssActive.m_PassiveSkillID = selectedSkillID;
					}
				}
				else
				{
					this.m_ssActive.FilterSet.Add(NKCOperatorSortSystem.eFilterOption.PassiveSkill);
					this.m_ssActive.m_PassiveSkillID = selectedSkillID;
				}
				this.m_passiveSlot.SetData(this.m_ssActive.m_PassiveSkillID, this.m_ssActive.m_PassiveSkillID != 0);
				if (this.m_bReset)
				{
					return;
				}
				NKCPopupFilterSubUIOperator.OnFilterOptionChange onFilterOptionChange = this.dOnFilterOptionChange;
				if (onFilterOptionChange == null)
				{
					return;
				}
				onFilterOptionChange(this.m_ssActive);
			}
		}

		// Token: 0x0600740C RID: 29708 RVA: 0x002698C8 File Offset: 0x00267AC8
		public void ResetFilterSlot()
		{
			this.m_bReset = true;
			NKCUIComToggle[] componentsInChildren = base.transform.GetComponentsInChildren<NKCUIComToggle>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Select(false, false, false);
			}
			this.m_passiveSlot.SetData(0, false);
			this.m_bReset = false;
		}

		// Token: 0x04006004 RID: 24580
		[Header("해당 프리팹에서 사용하는것만 연결")]
		[Header("Passive Skill")]
		public GameObject m_objPassiveSkill;

		// Token: 0x04006005 RID: 24581
		public NKCPopupFilterSubUIOperatorPassiveSlot m_passiveSlot;

		// Token: 0x04006006 RID: 24582
		[Header("In Collection")]
		public GameObject m_objCollected;

		// Token: 0x04006007 RID: 24583
		public NKCUIComToggle m_tglCollected;

		// Token: 0x04006008 RID: 24584
		public NKCUIComToggle m_tglNotCollected;

		// Token: 0x04006009 RID: 24585
		[Header("Have")]
		public GameObject m_objHave;

		// Token: 0x0400600A RID: 24586
		public NKCUIComToggle m_tglHave;

		// Token: 0x0400600B RID: 24587
		public NKCUIComToggle m_tglNotHave;

		// Token: 0x0400600C RID: 24588
		[Header("Rarity")]
		public GameObject m_objRare;

		// Token: 0x0400600D RID: 24589
		public NKCUIComToggle m_tglRare_SSR;

		// Token: 0x0400600E RID: 24590
		public NKCUIComToggle m_tglRare_SR;

		// Token: 0x0400600F RID: 24591
		public NKCUIComToggle m_tglRare_R;

		// Token: 0x04006010 RID: 24592
		public NKCUIComToggle m_tglRare_N;

		// Token: 0x04006011 RID: 24593
		[Header("Level")]
		public GameObject m_objLevel;

		// Token: 0x04006012 RID: 24594
		public NKCUIComToggle m_tglLevel_1;

		// Token: 0x04006013 RID: 24595
		public NKCUIComToggle m_tglLevel_Other;

		// Token: 0x04006014 RID: 24596
		public NKCUIComToggle m_tglLevel_Max;

		// Token: 0x04006015 RID: 24597
		[Header("Deck")]
		public GameObject m_objDeck;

		// Token: 0x04006016 RID: 24598
		public NKCUIComToggle m_tglDecked;

		// Token: 0x04006017 RID: 24599
		public NKCUIComToggle m_tglWait;

		// Token: 0x04006018 RID: 24600
		[Header("Lock")]
		public GameObject m_objLock;

		// Token: 0x04006019 RID: 24601
		public NKCUIComToggle m_tglLocked;

		// Token: 0x0400601A RID: 24602
		public NKCUIComToggle m_tglUnlocked;

		// Token: 0x0400601B RID: 24603
		[Header("Passive Skill")]
		public GameObject m_objSubFilter;

		// Token: 0x0400601C RID: 24604
		public NKCPopupFilterSubUIOperatorPassive m_subFilter;

		// Token: 0x0400601D RID: 24605
		private RectTransform m_RectTransform;

		// Token: 0x0400601E RID: 24606
		private Dictionary<NKCOperatorSortSystem.eFilterOption, NKCPopupFilterSubUIOperatorPassiveSlot> m_dicFilterPassiveSlot = new Dictionary<NKCOperatorSortSystem.eFilterOption, NKCPopupFilterSubUIOperatorPassiveSlot>();

		// Token: 0x0400601F RID: 24607
		private Dictionary<NKCOperatorSortSystem.eFilterOption, NKCUIComToggle> m_dicFilterBtn = new Dictionary<NKCOperatorSortSystem.eFilterOption, NKCUIComToggle>();

		// Token: 0x04006020 RID: 24608
		private NKCPopupFilterSubUIOperator.OnFilterOptionChange dOnFilterOptionChange;

		// Token: 0x04006021 RID: 24609
		private NKCOperatorSortSystem m_ssActive;

		// Token: 0x04006022 RID: 24610
		private bool m_bInitComplete;

		// Token: 0x04006023 RID: 24611
		private bool m_bReset;

		// Token: 0x020017A5 RID: 6053
		// (Invoke) Token: 0x0600B3DE RID: 46046
		public delegate void OnFilterOptionChange(NKCOperatorSortSystem ssActive);
	}
}
