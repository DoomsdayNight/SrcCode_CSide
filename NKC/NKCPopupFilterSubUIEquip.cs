using System;
using System.Collections.Generic;
using NKC.UI;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007F6 RID: 2038
	public class NKCPopupFilterSubUIEquip : MonoBehaviour
	{
		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x060050BB RID: 20667 RVA: 0x00186B73 File Offset: 0x00184D73
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

		// Token: 0x060050BC RID: 20668 RVA: 0x00186B98 File Offset: 0x00184D98
		private void Init()
		{
			this.m_dicFilterTgl.Clear();
			this.m_dicFilterStatBtn.Clear();
			if (this.m_objStatType != null)
			{
				this.SetButtonListner(this.m_btnStat_01, NKCEquipSortSystem.eFilterOption.Equip_Stat, false, 0);
				this.SetButtonListner(this.m_btnStat_02, NKCEquipSortSystem.eFilterOption.Equip_Stat, false, 1);
				this.SetButtonListner(this.m_btnStat_03, NKCEquipSortSystem.eFilterOption.Equip_Stat, false, 2);
				this.SetButtonListner(this.m_btnStatSet, NKCEquipSortSystem.eFilterOption.Equip_Stat, true, 3);
			}
			this.SetToggleListner(this.m_tglCounter, NKCEquipSortSystem.eFilterOption.Equip_Counter);
			this.SetToggleListner(this.m_tglSoldier, NKCEquipSortSystem.eFilterOption.Equip_Soldier);
			this.SetToggleListner(this.m_tglMechanic, NKCEquipSortSystem.eFilterOption.Equip_Mechanic);
			this.SetToggleListner(this.m_tglWeapon, NKCEquipSortSystem.eFilterOption.Equip_Weapon);
			this.SetToggleListner(this.m_tglArmor, NKCEquipSortSystem.eFilterOption.Equip_Armor);
			this.SetToggleListner(this.m_tglAcc, NKCEquipSortSystem.eFilterOption.Equip_Acc);
			this.SetToggleListner(this.m_tglEnchant, NKCEquipSortSystem.eFilterOption.Equip_Enchant);
			this.SetToggleListner(this.m_tglTier_7, NKCEquipSortSystem.eFilterOption.Equip_Tier_7);
			this.SetToggleListner(this.m_tglTier_6, NKCEquipSortSystem.eFilterOption.Equip_Tier_6);
			this.SetToggleListner(this.m_tglTier_5, NKCEquipSortSystem.eFilterOption.Equip_Tier_5);
			this.SetToggleListner(this.m_tglTier_4, NKCEquipSortSystem.eFilterOption.Equip_Tier_4);
			this.SetToggleListner(this.m_tglTier_3, NKCEquipSortSystem.eFilterOption.Equip_Tier_3);
			this.SetToggleListner(this.m_tglTier_2, NKCEquipSortSystem.eFilterOption.Equip_Tier_2);
			this.SetToggleListner(this.m_tglTier_1, NKCEquipSortSystem.eFilterOption.Equip_Tier_1);
			this.SetToggleListner(this.m_tglRarity_SSR, NKCEquipSortSystem.eFilterOption.Equip_Rarity_SSR);
			this.SetToggleListner(this.m_tglRarity_SR, NKCEquipSortSystem.eFilterOption.Equip_Rarity_SR);
			this.SetToggleListner(this.m_tglRarity_R, NKCEquipSortSystem.eFilterOption.Equip_Rarity_R);
			this.SetToggleListner(this.m_tglRarity_N, NKCEquipSortSystem.eFilterOption.Equip_Rarity_N);
			this.SetToggleListner(this.m_tglSetOption_Part2, NKCEquipSortSystem.eFilterOption.Equip_Set_Part_2);
			this.SetToggleListner(this.m_tglSetOption_Part3, NKCEquipSortSystem.eFilterOption.Equip_Set_Part_3);
			this.SetToggleListner(this.m_tglSetOption_Part4, NKCEquipSortSystem.eFilterOption.Equip_Set_Part_4);
			this.SetToggleListner(this.m_tglSetOption_Attack, NKCEquipSortSystem.eFilterOption.Equip_Set_Effect_Red);
			this.SetToggleListner(this.m_tglSetOption_Defence, NKCEquipSortSystem.eFilterOption.Equip_Set_Effect_Blue);
			this.SetToggleListner(this.m_tglSetOption_Etc, NKCEquipSortSystem.eFilterOption.Equip_Set_Effect_Yellow);
			this.SetToggleListner(this.m_tglEquipped, NKCEquipSortSystem.eFilterOption.Equip_Equipped);
			this.SetToggleListner(this.m_tglUnUsed, NKCEquipSortSystem.eFilterOption.Equip_Unused);
			this.SetToggleListner(this.m_tglLocked, NKCEquipSortSystem.eFilterOption.Equip_Locked);
			this.SetToggleListner(this.m_tglUnlocked, NKCEquipSortSystem.eFilterOption.Equip_Unlocked);
			this.SetToggleListner(this.m_tglHave, NKCEquipSortSystem.eFilterOption.Equip_Have);
			this.SetToggleListner(this.m_tglNotHave, NKCEquipSortSystem.eFilterOption.Equip_NotHave);
			this.SetToggleListner(this.m_tglPrivate, NKCEquipSortSystem.eFilterOption.Equip_Private);
			this.SetToggleListner(this.m_tglNonPrivate, NKCEquipSortSystem.eFilterOption.Equip_Non_Private);
			this.SetToggleListner(this.m_tglRelic, NKCEquipSortSystem.eFilterOption.Equip_Relic);
			NKCUtil.SetGameobjectActive(this.m_subFilter, false);
			this.m_bInitComplete = true;
		}

		// Token: 0x060050BD RID: 20669 RVA: 0x00186DE4 File Offset: 0x00184FE4
		private void SetButtonListner(NKCPopupFilterSubUIEquipStatSlot statSlot, NKCEquipSortSystem.eFilterOption filterOption, bool isSetOptionSlot, int idx)
		{
			if (statSlot != null)
			{
				if (!this.m_dicFilterStatBtn.ContainsKey(filterOption))
				{
					this.m_dicFilterStatBtn.Add(filterOption, new List<NKCPopupFilterSubUIEquipStatSlot>());
				}
				this.m_dicFilterStatBtn[filterOption].Add(statSlot);
				if (isSetOptionSlot)
				{
					statSlot.SetData(0, false);
				}
				else
				{
					statSlot.SetData(NKM_STAT_TYPE.NST_RANDOM, false);
				}
				NKCUIComStateButton button = statSlot.GetButton();
				if (button != null)
				{
					button.PointerClick.RemoveAllListeners();
					button.PointerClick.AddListener(delegate()
					{
						this.OpenStatPopup(isSetOptionSlot, filterOption, idx);
					});
				}
			}
		}

		// Token: 0x060050BE RID: 20670 RVA: 0x00186EAC File Offset: 0x001850AC
		private void SetToggleListner(NKCUIComToggle toggle, NKCEquipSortSystem.eFilterOption filterOption)
		{
			if (toggle != null)
			{
				this.m_dicFilterTgl.Add(filterOption, toggle);
				toggle.OnValueChanged.RemoveAllListeners();
				toggle.OnValueChanged.AddListener(delegate(bool value)
				{
					this.OnFilterButton(value, filterOption);
				});
			}
		}

		// Token: 0x060050BF RID: 20671 RVA: 0x00186F0C File Offset: 0x0018510C
		public void OpenFilterPopup(NKCEquipSortSystem ssActive, HashSet<NKCEquipSortSystem.eFilterCategory> setFilterCategory, NKCPopupFilterSubUIEquip.OnFilterOptionChange onFilterOptionChange, bool bEnableEnchantModuleFilter = false)
		{
			if (!this.m_bInitComplete)
			{
				this.Init();
			}
			NKCUtil.SetGameobjectActive(this.m_subFilter, false);
			this.m_ssActive = ssActive;
			this.dOnFilterOptionChange = onFilterOptionChange;
			this.SetFilter(ssActive.m_EquipListOptions, bEnableEnchantModuleFilter);
			this.SetStatFilter(ssActive.m_EquipListOptions);
			NKCUtil.SetGameobjectActive(this.m_objEquipped, setFilterCategory.Contains(NKCEquipSortSystem.eFilterCategory.Equipped));
			NKCUtil.SetGameobjectActive(this.m_objEquipType, setFilterCategory.Contains(NKCEquipSortSystem.eFilterCategory.EquipType));
			NKCUtil.SetGameobjectActive(this.m_objHave, setFilterCategory.Contains(NKCEquipSortSystem.eFilterCategory.Have));
			NKCUtil.SetGameobjectActive(this.m_objLocked, setFilterCategory.Contains(NKCEquipSortSystem.eFilterCategory.Locked));
			NKCUtil.SetGameobjectActive(this.m_objRarity, setFilterCategory.Contains(NKCEquipSortSystem.eFilterCategory.Rarity));
			NKCUtil.SetGameobjectActive(this.m_objSetOptionPart, setFilterCategory.Contains(NKCEquipSortSystem.eFilterCategory.SetOptionPart));
			NKCUtil.SetGameobjectActive(this.m_objSetOptionType, setFilterCategory.Contains(NKCEquipSortSystem.eFilterCategory.SetOptionType));
			NKCUtil.SetGameobjectActive(this.m_objTier, setFilterCategory.Contains(NKCEquipSortSystem.eFilterCategory.Tier));
			NKCUtil.SetGameobjectActive(this.m_objUnitType, setFilterCategory.Contains(NKCEquipSortSystem.eFilterCategory.UnitType));
			NKCUtil.SetGameobjectActive(this.m_objStatType, setFilterCategory.Contains(NKCEquipSortSystem.eFilterCategory.StatType));
			NKCUtil.SetGameobjectActive(this.m_objPrivate, setFilterCategory.Contains(NKCEquipSortSystem.eFilterCategory.PrivateEquip));
			NKCUtil.SetGameobjectActive(base.gameObject, true);
		}

		// Token: 0x060050C0 RID: 20672 RVA: 0x00187030 File Offset: 0x00185230
		private void SetFilter(NKCEquipSortSystem.EquipListOptions equipListOption, bool bEnableEnchant = false)
		{
			this.m_bReset = true;
			foreach (KeyValuePair<NKCEquipSortSystem.eFilterOption, NKCUIComToggle> keyValuePair in this.m_dicFilterTgl)
			{
				if (equipListOption.setFilterOption.Contains(keyValuePair.Key))
				{
					this.m_dicFilterTgl[keyValuePair.Key].Select(true, false, false);
				}
				else
				{
					this.m_dicFilterTgl[keyValuePair.Key].Select(false, false, false);
				}
			}
			if (this.m_dicFilterTgl.ContainsKey(NKCEquipSortSystem.eFilterOption.Equip_Enchant))
			{
				if (!bEnableEnchant)
				{
					this.m_dicFilterTgl[NKCEquipSortSystem.eFilterOption.Equip_Enchant].Lock(false);
				}
				else
				{
					this.m_dicFilterTgl[NKCEquipSortSystem.eFilterOption.Equip_Enchant].UnLock(false);
				}
			}
			this.m_bReset = false;
		}

		// Token: 0x060050C1 RID: 20673 RVA: 0x00187110 File Offset: 0x00185310
		private void SetStatFilter(NKCEquipSortSystem.EquipListOptions equipListOption)
		{
			this.m_bReset = true;
			if (this.m_dicFilterStatBtn.ContainsKey(NKCEquipSortSystem.eFilterOption.Equip_Stat))
			{
				this.m_btnStat_01.SetData(equipListOption.FilterStatType_01, equipListOption.FilterStatType_01 != NKM_STAT_TYPE.NST_RANDOM);
				this.m_btnStat_02.SetData(equipListOption.FilterStatType_02, equipListOption.FilterStatType_02 != NKM_STAT_TYPE.NST_RANDOM);
				this.m_btnStat_03.SetData(equipListOption.FilterStatType_03, equipListOption.FilterStatType_03 != NKM_STAT_TYPE.NST_RANDOM);
				if (equipListOption.FilterSetOptionID <= 0)
				{
					this.m_btnStatSet.SetData(null, false);
				}
				else
				{
					this.m_btnStatSet.SetData(equipListOption.FilterSetOptionID, true);
				}
			}
			this.m_bReset = false;
		}

		// Token: 0x060050C2 RID: 20674 RVA: 0x001871C0 File Offset: 0x001853C0
		private void OnFilterButton(bool bSelect, NKCEquipSortSystem.eFilterOption filterOption)
		{
			if (this.m_dicFilterTgl.ContainsKey(filterOption))
			{
				NKCUIComToggle nkcuicomToggle = this.m_dicFilterTgl[filterOption];
				if (nkcuicomToggle != null)
				{
					nkcuicomToggle.Select(bSelect, true, true);
					if (this.m_bReset)
					{
						return;
					}
					if (this.m_ssActive.FilterSet == null)
					{
						this.m_ssActive.FilterSet = new HashSet<NKCEquipSortSystem.eFilterOption>();
					}
					if (this.m_ssActive.FilterSet.Contains(filterOption))
					{
						this.m_ssActive.FilterSet.Remove(filterOption);
					}
					else
					{
						this.m_ssActive.FilterSet.Add(filterOption);
					}
					NKCPopupFilterSubUIEquip.OnFilterOptionChange onFilterOptionChange = this.dOnFilterOptionChange;
					if (onFilterOptionChange == null)
					{
						return;
					}
					onFilterOptionChange(this.m_ssActive, filterOption);
				}
			}
		}

		// Token: 0x060050C3 RID: 20675 RVA: 0x00187278 File Offset: 0x00185478
		private void OnStatFilterOptionChanged(NKM_STAT_TYPE statType, int setOptionID, int selectedSlotIdx)
		{
			if (this.m_subFilter != null && this.m_subFilter.gameObject.activeSelf)
			{
				this.m_subFilter.Close();
			}
			switch (selectedSlotIdx)
			{
			case 0:
				if (this.m_ssActive.FilterStatType_01 != NKM_STAT_TYPE.NST_RANDOM)
				{
					this.m_ssActive.FilterStatType_01 = NKM_STAT_TYPE.NST_RANDOM;
				}
				else
				{
					this.m_ssActive.FilterStatType_01 = statType;
				}
				this.m_dicFilterStatBtn[NKCEquipSortSystem.eFilterOption.Equip_Stat][selectedSlotIdx].SetData(this.m_ssActive.FilterStatType_01, this.m_ssActive.FilterStatType_01 != NKM_STAT_TYPE.NST_RANDOM);
				break;
			case 1:
				if (this.m_ssActive.FilterStatType_02 != NKM_STAT_TYPE.NST_RANDOM)
				{
					this.m_ssActive.FilterStatType_02 = NKM_STAT_TYPE.NST_RANDOM;
				}
				else
				{
					this.m_ssActive.FilterStatType_02 = statType;
				}
				this.m_dicFilterStatBtn[NKCEquipSortSystem.eFilterOption.Equip_Stat][selectedSlotIdx].SetData(this.m_ssActive.FilterStatType_02, this.m_ssActive.FilterStatType_02 != NKM_STAT_TYPE.NST_RANDOM);
				break;
			case 2:
				if (this.m_ssActive.FilterStatType_03 != NKM_STAT_TYPE.NST_RANDOM)
				{
					this.m_ssActive.FilterStatType_03 = NKM_STAT_TYPE.NST_RANDOM;
				}
				else
				{
					this.m_ssActive.FilterStatType_03 = statType;
				}
				this.m_dicFilterStatBtn[NKCEquipSortSystem.eFilterOption.Equip_Stat][selectedSlotIdx].SetData(this.m_ssActive.FilterStatType_03, this.m_ssActive.FilterStatType_03 != NKM_STAT_TYPE.NST_RANDOM);
				break;
			case 3:
				if (this.m_ssActive.FilterStatType_SetOptionID > 0)
				{
					this.m_ssActive.FilterStatType_SetOptionID = 0;
				}
				else
				{
					this.m_ssActive.FilterStatType_SetOptionID = setOptionID;
				}
				this.m_dicFilterStatBtn[NKCEquipSortSystem.eFilterOption.Equip_Stat][selectedSlotIdx].SetData(this.m_ssActive.FilterStatType_SetOptionID, this.m_ssActive.FilterStatType_SetOptionID > 0);
				break;
			}
			if (this.IsAnyStatSlotSelected())
			{
				if (!this.m_ssActive.FilterSet.Contains(NKCEquipSortSystem.eFilterOption.Equip_Stat))
				{
					this.m_ssActive.FilterSet.Add(NKCEquipSortSystem.eFilterOption.Equip_Stat);
				}
			}
			else if (this.m_ssActive.FilterSet.Contains(NKCEquipSortSystem.eFilterOption.Equip_Stat))
			{
				this.m_ssActive.FilterSet.Remove(NKCEquipSortSystem.eFilterOption.Equip_Stat);
			}
			NKCPopupFilterSubUIEquip.OnFilterOptionChange onFilterOptionChange = this.dOnFilterOptionChange;
			if (onFilterOptionChange == null)
			{
				return;
			}
			onFilterOptionChange(this.m_ssActive, NKCEquipSortSystem.eFilterOption.Equip_Stat);
		}

		// Token: 0x060050C4 RID: 20676 RVA: 0x001874B4 File Offset: 0x001856B4
		private void OpenStatPopup(bool bIsSetOptionSlot, NKCEquipSortSystem.eFilterOption filterOption, int selectedSlotIdx)
		{
			if (this.m_ssActive.FilterSet.Contains(filterOption) && this.m_dicFilterStatBtn.ContainsKey(filterOption) && this.m_dicFilterStatBtn[filterOption][selectedSlotIdx].GetButton().m_bSelect)
			{
				this.OnStatFilterOptionChanged(NKM_STAT_TYPE.NST_RANDOM, 0, selectedSlotIdx);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_subFilter, true);
			List<NKM_STAT_TYPE> list = new List<NKM_STAT_TYPE>
			{
				NKM_STAT_TYPE.NST_RANDOM,
				NKM_STAT_TYPE.NST_RANDOM,
				NKM_STAT_TYPE.NST_RANDOM
			};
			if (this.m_ssActive.FilterStatType_01 != NKM_STAT_TYPE.NST_RANDOM)
			{
				list[0] = this.m_ssActive.FilterStatType_01;
			}
			if (this.m_ssActive.FilterStatType_02 != NKM_STAT_TYPE.NST_RANDOM)
			{
				list[1] = this.m_ssActive.FilterStatType_02;
			}
			if (this.m_ssActive.FilterStatType_03 != NKM_STAT_TYPE.NST_RANDOM)
			{
				list[2] = this.m_ssActive.FilterStatType_03;
			}
			this.m_subFilter.Open(bIsSetOptionSlot, list, new NKCPopupFilterSubUIEquipStat.OnClickStatSlot(this.OnStatFilterOptionChanged), selectedSlotIdx);
		}

		// Token: 0x060050C5 RID: 20677 RVA: 0x001875AC File Offset: 0x001857AC
		private bool IsAnyStatSlotSelected()
		{
			return this.m_ssActive.FilterStatType_01 != NKM_STAT_TYPE.NST_RANDOM || this.m_ssActive.FilterStatType_02 != NKM_STAT_TYPE.NST_RANDOM || this.m_ssActive.FilterStatType_03 != NKM_STAT_TYPE.NST_RANDOM || this.m_ssActive.FilterStatType_SetOptionID > 0;
		}

		// Token: 0x060050C6 RID: 20678 RVA: 0x001875FC File Offset: 0x001857FC
		public void ResetFilter()
		{
			this.m_bReset = true;
			NKCUIComToggle[] componentsInChildren = base.transform.GetComponentsInChildren<NKCUIComToggle>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Select(false, false, false);
			}
			if (this.m_dicFilterStatBtn.ContainsKey(NKCEquipSortSystem.eFilterOption.Equip_Stat))
			{
				for (int j = 0; j < this.m_dicFilterStatBtn[NKCEquipSortSystem.eFilterOption.Equip_Stat].Count; j++)
				{
					NKCPopupFilterSubUIEquipStatSlot nkcpopupFilterSubUIEquipStatSlot = this.m_dicFilterStatBtn[NKCEquipSortSystem.eFilterOption.Equip_Stat][j];
					if (nkcpopupFilterSubUIEquipStatSlot.IsSetOptionSlot)
					{
						nkcpopupFilterSubUIEquipStatSlot.SetData(0, false);
					}
					else
					{
						nkcpopupFilterSubUIEquipStatSlot.SetData(NKM_STAT_TYPE.NST_RANDOM, false);
					}
				}
			}
			this.m_ssActive.FilterStatType_SetOptionID = 0;
			this.m_ssActive.FilterStatType_01 = NKM_STAT_TYPE.NST_RANDOM;
			this.m_ssActive.FilterStatType_02 = NKM_STAT_TYPE.NST_RANDOM;
			this.m_ssActive.FilterStatType_03 = NKM_STAT_TYPE.NST_RANDOM;
			NKCUtil.SetGameobjectActive(this.m_subFilter, false);
			this.m_bReset = false;
		}

		// Token: 0x040040B8 RID: 16568
		[Header("StatType")]
		public GameObject m_objStatType;

		// Token: 0x040040B9 RID: 16569
		public NKCPopupFilterSubUIEquipStatSlot m_btnStat_01;

		// Token: 0x040040BA RID: 16570
		public NKCPopupFilterSubUIEquipStatSlot m_btnStat_02;

		// Token: 0x040040BB RID: 16571
		public NKCPopupFilterSubUIEquipStatSlot m_btnStat_03;

		// Token: 0x040040BC RID: 16572
		public NKCPopupFilterSubUIEquipStatSlot m_btnStatSet;

		// Token: 0x040040BD RID: 16573
		[Header("UnitType")]
		public GameObject m_objUnitType;

		// Token: 0x040040BE RID: 16574
		public NKCUIComToggle m_tglCounter;

		// Token: 0x040040BF RID: 16575
		public NKCUIComToggle m_tglSoldier;

		// Token: 0x040040C0 RID: 16576
		public NKCUIComToggle m_tglMechanic;

		// Token: 0x040040C1 RID: 16577
		[Header("EquipType")]
		public GameObject m_objEquipType;

		// Token: 0x040040C2 RID: 16578
		public NKCUIComToggle m_tglWeapon;

		// Token: 0x040040C3 RID: 16579
		public NKCUIComToggle m_tglArmor;

		// Token: 0x040040C4 RID: 16580
		public NKCUIComToggle m_tglAcc;

		// Token: 0x040040C5 RID: 16581
		public NKCUIComToggle m_tglEnchant;

		// Token: 0x040040C6 RID: 16582
		[Header("Tier")]
		public GameObject m_objTier;

		// Token: 0x040040C7 RID: 16583
		public NKCUIComToggle m_tglTier_7;

		// Token: 0x040040C8 RID: 16584
		public NKCUIComToggle m_tglTier_6;

		// Token: 0x040040C9 RID: 16585
		public NKCUIComToggle m_tglTier_5;

		// Token: 0x040040CA RID: 16586
		public NKCUIComToggle m_tglTier_4;

		// Token: 0x040040CB RID: 16587
		public NKCUIComToggle m_tglTier_3;

		// Token: 0x040040CC RID: 16588
		public NKCUIComToggle m_tglTier_2;

		// Token: 0x040040CD RID: 16589
		public NKCUIComToggle m_tglTier_1;

		// Token: 0x040040CE RID: 16590
		[Header("Rarity")]
		public GameObject m_objRarity;

		// Token: 0x040040CF RID: 16591
		public NKCUIComToggle m_tglRarity_SSR;

		// Token: 0x040040D0 RID: 16592
		public NKCUIComToggle m_tglRarity_SR;

		// Token: 0x040040D1 RID: 16593
		public NKCUIComToggle m_tglRarity_R;

		// Token: 0x040040D2 RID: 16594
		public NKCUIComToggle m_tglRarity_N;

		// Token: 0x040040D3 RID: 16595
		[Header("SetOptionPart")]
		public GameObject m_objSetOptionPart;

		// Token: 0x040040D4 RID: 16596
		public NKCUIComToggle m_tglSetOption_Part2;

		// Token: 0x040040D5 RID: 16597
		public NKCUIComToggle m_tglSetOption_Part3;

		// Token: 0x040040D6 RID: 16598
		public NKCUIComToggle m_tglSetOption_Part4;

		// Token: 0x040040D7 RID: 16599
		[Header("SetOptionPart")]
		public GameObject m_objSetOptionType;

		// Token: 0x040040D8 RID: 16600
		public NKCUIComToggle m_tglSetOption_Attack;

		// Token: 0x040040D9 RID: 16601
		public NKCUIComToggle m_tglSetOption_Defence;

		// Token: 0x040040DA RID: 16602
		public NKCUIComToggle m_tglSetOption_Etc;

		// Token: 0x040040DB RID: 16603
		[Header("Equipped")]
		public GameObject m_objEquipped;

		// Token: 0x040040DC RID: 16604
		public NKCUIComToggle m_tglEquipped;

		// Token: 0x040040DD RID: 16605
		public NKCUIComToggle m_tglUnUsed;

		// Token: 0x040040DE RID: 16606
		[Header("Locked")]
		public GameObject m_objLocked;

		// Token: 0x040040DF RID: 16607
		public NKCUIComToggle m_tglLocked;

		// Token: 0x040040E0 RID: 16608
		public NKCUIComToggle m_tglUnlocked;

		// Token: 0x040040E1 RID: 16609
		[Header("Have")]
		public GameObject m_objHave;

		// Token: 0x040040E2 RID: 16610
		public NKCUIComToggle m_tglHave;

		// Token: 0x040040E3 RID: 16611
		public NKCUIComToggle m_tglNotHave;

		// Token: 0x040040E4 RID: 16612
		[Header("전용장비")]
		public GameObject m_objPrivate;

		// Token: 0x040040E5 RID: 16613
		public NKCUIComToggle m_tglPrivate;

		// Token: 0x040040E6 RID: 16614
		public NKCUIComToggle m_tglNonPrivate;

		// Token: 0x040040E7 RID: 16615
		public NKCUIComToggle m_tglRelic;

		// Token: 0x040040E8 RID: 16616
		[Header("옵션선택창")]
		public NKCPopupFilterSubUIEquipStat m_subFilter;

		// Token: 0x040040E9 RID: 16617
		public Text m_lbSubFilterTitle;

		// Token: 0x040040EA RID: 16618
		private RectTransform m_RectTransform;

		// Token: 0x040040EB RID: 16619
		private Dictionary<NKCEquipSortSystem.eFilterOption, List<NKCPopupFilterSubUIEquipStatSlot>> m_dicFilterStatBtn = new Dictionary<NKCEquipSortSystem.eFilterOption, List<NKCPopupFilterSubUIEquipStatSlot>>();

		// Token: 0x040040EC RID: 16620
		private Dictionary<NKCEquipSortSystem.eFilterOption, NKCUIComToggle> m_dicFilterTgl = new Dictionary<NKCEquipSortSystem.eFilterOption, NKCUIComToggle>();

		// Token: 0x040040ED RID: 16621
		private NKCPopupFilterSubUIEquip.OnFilterOptionChange dOnFilterOptionChange;

		// Token: 0x040040EE RID: 16622
		private NKCEquipSortSystem m_ssActive;

		// Token: 0x040040EF RID: 16623
		private List<NKM_STAT_TYPE> m_lstSelectedType = new List<NKM_STAT_TYPE>
		{
			NKM_STAT_TYPE.NST_RANDOM,
			NKM_STAT_TYPE.NST_RANDOM,
			NKM_STAT_TYPE.NST_RANDOM
		};

		// Token: 0x040040F0 RID: 16624
		private bool m_bInitComplete;

		// Token: 0x040040F1 RID: 16625
		private bool m_bReset;

		// Token: 0x020014B3 RID: 5299
		// (Invoke) Token: 0x0600A9AD RID: 43437
		public delegate void OnFilterOptionChange(NKCEquipSortSystem equipListOption, NKCEquipSortSystem.eFilterOption filterOption);
	}
}
