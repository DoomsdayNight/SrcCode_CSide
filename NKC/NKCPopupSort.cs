using System;
using System.Collections.Generic;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007FC RID: 2044
	public class NKCPopupSort : MonoBehaviour
	{
		// Token: 0x060050E9 RID: 20713 RVA: 0x001888F0 File Offset: 0x00186AF0
		private void Init()
		{
			bool bInitComplete = this.m_bInitComplete;
			this.m_dicSortOption.Clear();
			if (this.m_arrayCustomSortMenu != null)
			{
				for (int i = 0; i < this.m_arrayCustomSortMenu.Length; i++)
				{
					if (i == 0)
					{
						this.AddSortOption(NKCUnitSortSystem.eSortCategory.Custom1, this.m_arrayCustomSortMenu[i].m_cTglSortTypeCustom);
					}
					else if (i == 1)
					{
						this.AddSortOption(NKCUnitSortSystem.eSortCategory.Custom2, this.m_arrayCustomSortMenu[i].m_cTglSortTypeCustom);
					}
					else
					{
						if (i != 2)
						{
							break;
						}
						this.AddSortOption(NKCUnitSortSystem.eSortCategory.Custom3, this.m_arrayCustomSortMenu[i].m_cTglSortTypeCustom);
					}
				}
			}
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.IDX, this.m_cTglSortTypeIdx);
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.Level, this.m_cTglSortTypeLevel);
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.Rarity, this.m_cTglSortTypeRarity);
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.SetOption, this.m_cTglSortTypeSetOption);
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.UnitSummonCost, this.m_cTglSortTypeCost);
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.TacticUpdatePossible, this.m_cTglSortTypeTacticUpdatePossible);
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.UID, this.m_cTglSortTypeUID);
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.UnitPower, this.m_cTglSortTypePower);
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.UnitAttack, this.m_cTglSortTypeAttack);
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.UnitHealth, this.m_cTglSortTypeHP);
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.UnitDefense, this.m_cTglSortTypeDefense);
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.UnitCrit, this.m_cTglSortTypeCritical);
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.UnitHit, this.m_cTglSortTypeHit);
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.UnitEvade, this.m_cTglSortTypeEvade);
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.UnitReduceSkillCool, this.m_cTglSortTypeReduceSkillCool);
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.PlayerLevel, this.m_cTglSortTypePlayerLevel);
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.LoginTime, this.m_cTglSortTypeLoginTime);
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.ScoutProgress, this.m_cTglSortTypeScoutProgress);
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.GuildGrade, this.m_cTglSortTypeGuildGrade);
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.GuildWeeklyPoint, this.m_cTglSortTypeGuildWeeklyPoint);
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.GuildTotalPoint, this.m_cTglSortTypeGuildTotalPoint);
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.LimitBreakPossible, this.m_cTglSortTypeLimitBreakPossible);
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.Transcendence, this.m_cTglSortTypeTranscendence);
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.UnitLoyalty, this.m_cTglSortTypeLoyalty);
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.Squad_Dungeon, this.m_cTglSortTypeSquadDungeon);
			this.AddSortOption(NKCUnitSortSystem.eSortCategory.Squad_Gauntlet, this.m_cTglSortTypeSquadGauntlet);
			this.m_bInitComplete = true;
		}

		// Token: 0x060050EA RID: 20714 RVA: 0x00188AF0 File Offset: 0x00186CF0
		private void AddSortOption(NKCUnitSortSystem.eSortCategory sortCategory, NKCUIComToggle tgl)
		{
			if (tgl != null)
			{
				tgl.m_DataInt = (int)sortCategory;
				this.m_dicSortOption.Add(NKCUnitSortSystem.GetSortOptionByCategory(sortCategory, true), tgl);
				this.m_dicSortOption.Add(NKCUnitSortSystem.GetSortOptionByCategory(sortCategory, false), tgl);
				this.m_dicToggle.Add(sortCategory, tgl);
				tgl.OnValueChanged.RemoveAllListeners();
				tgl.OnValueChangedWithData = new NKCUIComToggle.ValueChangedWithData(this.OnTglSortOption);
			}
		}

		// Token: 0x060050EB RID: 20715 RVA: 0x00188B60 File Offset: 0x00186D60
		private void OnTglSortOption(bool value, int data)
		{
			if (value)
			{
				NKCUnitSortSystem.eSortOption sortOptionByCategory = NKCUnitSortSystem.GetSortOptionByCategory((NKCUnitSortSystem.eSortCategory)data, this.m_bDescending);
				List<NKCUnitSortSystem.eSortOption> sortList;
				if (this.m_dicSortOptionDetails.TryGetValue(sortOptionByCategory, out sortList))
				{
					this.OnSort(sortList);
					return;
				}
				this.OnSort(sortOptionByCategory);
			}
		}

		// Token: 0x060050EC RID: 20716 RVA: 0x00188B9C File Offset: 0x00186D9C
		public void AddSortOptionDetail(NKCUnitSortSystem.eSortOption sortOption, List<NKCUnitSortSystem.eSortOption> lstDetail)
		{
			this.m_dicSortOptionDetails.Add(sortOption, lstDetail);
		}

		// Token: 0x060050ED RID: 20717 RVA: 0x00188BAB File Offset: 0x00186DAB
		public void OpenSortMenu(NKM_UNIT_TYPE targetType, NKCUnitSortSystem.eSortOption selectedSortOption, NKCPopupSort.OnSortOption onSortOption, bool bIsCollection, bool bDescending, bool bOpen)
		{
			this.OpenSortMenu(NKCPopupSort.MakeDefaultSortSet(targetType, bIsCollection), selectedSortOption, onSortOption, bOpen, targetType, bIsCollection, null);
		}

		// Token: 0x060050EE RID: 20718 RVA: 0x00188BC4 File Offset: 0x00186DC4
		public static HashSet<NKCUnitSortSystem.eSortCategory> MakeGlobalBanSortSet()
		{
			return new HashSet<NKCUnitSortSystem.eSortCategory>
			{
				NKCUnitSortSystem.eSortCategory.IDX,
				NKCUnitSortSystem.eSortCategory.UnitPower,
				NKCUnitSortSystem.eSortCategory.Rarity,
				NKCUnitSortSystem.eSortCategory.UnitAttack,
				NKCUnitSortSystem.eSortCategory.UnitHealth,
				NKCUnitSortSystem.eSortCategory.UnitSummonCost,
				NKCUnitSortSystem.eSortCategory.UnitDefense,
				NKCUnitSortSystem.eSortCategory.UnitHit,
				NKCUnitSortSystem.eSortCategory.UnitEvade
			};
		}

		// Token: 0x060050EF RID: 20719 RVA: 0x00188C24 File Offset: 0x00186E24
		public static HashSet<NKCUnitSortSystem.eSortCategory> MakeDefaultSortSet(NKM_UNIT_TYPE targetType, bool bIsCollection)
		{
			HashSet<NKCUnitSortSystem.eSortCategory> hashSet = new HashSet<NKCUnitSortSystem.eSortCategory>();
			if (bIsCollection)
			{
				hashSet.Add(NKCUnitSortSystem.eSortCategory.IDX);
			}
			else
			{
				hashSet.Add(NKCUnitSortSystem.eSortCategory.Level);
				hashSet.Add(NKCUnitSortSystem.eSortCategory.UID);
				hashSet.Add(NKCUnitSortSystem.eSortCategory.UnitPower);
				hashSet.Add(NKCUnitSortSystem.eSortCategory.UnitLoyalty);
				hashSet.Add(NKCUnitSortSystem.eSortCategory.Squad_Dungeon);
				hashSet.Add(NKCUnitSortSystem.eSortCategory.Squad_Gauntlet);
			}
			hashSet.Add(NKCUnitSortSystem.eSortCategory.Rarity);
			hashSet.Add(NKCUnitSortSystem.eSortCategory.UnitAttack);
			hashSet.Add(NKCUnitSortSystem.eSortCategory.UnitHealth);
			if (targetType == NKM_UNIT_TYPE.NUT_NORMAL)
			{
				hashSet.Add(NKCUnitSortSystem.eSortCategory.UnitSummonCost);
				hashSet.Add(NKCUnitSortSystem.eSortCategory.UnitDefense);
				hashSet.Add(NKCUnitSortSystem.eSortCategory.UnitHit);
				hashSet.Add(NKCUnitSortSystem.eSortCategory.UnitEvade);
			}
			return hashSet;
		}

		// Token: 0x060050F0 RID: 20720 RVA: 0x00188CB8 File Offset: 0x00186EB8
		public static HashSet<NKCOperatorSortSystem.eSortCategory> MakeDefaultOprSortSet(NKM_UNIT_TYPE targetType, bool bIsCollection)
		{
			HashSet<NKCOperatorSortSystem.eSortCategory> hashSet = new HashSet<NKCOperatorSortSystem.eSortCategory>();
			if (bIsCollection)
			{
				hashSet.Add(NKCOperatorSortSystem.eSortCategory.IDX);
			}
			else
			{
				hashSet.Add(NKCOperatorSortSystem.eSortCategory.Level);
				hashSet.Add(NKCOperatorSortSystem.eSortCategory.UID);
				hashSet.Add(NKCOperatorSortSystem.eSortCategory.UnitPower);
			}
			hashSet.Add(NKCOperatorSortSystem.eSortCategory.Rarity);
			hashSet.Add(NKCOperatorSortSystem.eSortCategory.UnitAttack);
			hashSet.Add(NKCOperatorSortSystem.eSortCategory.UnitHealth);
			if (targetType == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				hashSet.Add(NKCOperatorSortSystem.eSortCategory.UnitPower);
				hashSet.Add(NKCOperatorSortSystem.eSortCategory.UnitAttack);
				hashSet.Add(NKCOperatorSortSystem.eSortCategory.UnitDefense);
				hashSet.Add(NKCOperatorSortSystem.eSortCategory.UnitHealth);
				hashSet.Add(NKCOperatorSortSystem.eSortCategory.UnitReduceSkillCool);
			}
			return hashSet;
		}

		// Token: 0x060050F1 RID: 20721 RVA: 0x00188D38 File Offset: 0x00186F38
		public void OpenGuildMemberSortMenu(HashSet<NKCUnitSortSystem.eSortCategory> setCategory, NKCUnitSortSystem.eSortOption selectedSortOption, NKCPopupSort.OnSortOption onSortOption, bool bOpen)
		{
			if (!this.m_bInitComplete)
			{
				this.Init();
			}
			if (bOpen)
			{
				this.dOnSortOption = onSortOption;
				foreach (KeyValuePair<NKCUnitSortSystem.eSortCategory, NKCUIComToggle> keyValuePair in this.m_dicToggle)
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

		// Token: 0x060050F2 RID: 20722 RVA: 0x00188E14 File Offset: 0x00187014
		public void OpenSortMenu(HashSet<NKCUnitSortSystem.eSortCategory> setCategory, NKCUnitSortSystem.eSortOption selectedSortOption, NKCPopupSort.OnSortOption onSortOption, bool bOpen, NKM_UNIT_TYPE targetType, bool bIsCollection, List<string> customSortName)
		{
			if (!this.m_bInitComplete)
			{
				this.Init();
			}
			this.m_eUnitType = targetType;
			this.m_bIsCollection = bIsCollection;
			if (bOpen)
			{
				this.dOnSortOption = onSortOption;
				this.m_bDescending = NKCUnitSortSystem.IsDescending(selectedSortOption);
				foreach (KeyValuePair<NKCUnitSortSystem.eSortCategory, NKCUIComToggle> keyValuePair in this.m_dicToggle)
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
				if (customSortName != null && this.m_arrayCustomSortMenu != null)
				{
					int count = customSortName.Count;
					for (int i = 0; i < count; i++)
					{
						if (i >= this.m_arrayCustomSortMenu.Length)
						{
							return;
						}
						this.m_arrayCustomSortMenu[i].m_lbCustomOffText.text = customSortName[i];
						this.m_arrayCustomSortMenu[i].m_lbCustomOnText.text = customSortName[i];
						this.m_arrayCustomSortMenu[i].m_lbCustomPressText.text = customSortName[i];
					}
					return;
				}
			}
			else
			{
				this.Close();
			}
		}

		// Token: 0x060050F3 RID: 20723 RVA: 0x00188F98 File Offset: 0x00187198
		public void OpenSquadSortMenu(NKCUnitSortSystem.eSortOption selectedSortOption, NKCPopupSort.OnSortOption onSortOption, bool bValue)
		{
			if (!this.m_bInitComplete)
			{
				this.Init();
			}
			if (bValue)
			{
				this.dOnSortOption = onSortOption;
				NKCUtil.SetGameobjectActive(this.m_cTglSortTypeIdx, false);
				NKCUtil.SetGameobjectActive(this.m_cTglSortTypeLevel, false);
				NKCUtil.SetGameobjectActive(this.m_cTglSortTypeRarity, false);
				NKCUtil.SetGameobjectActive(this.m_cTglSortTypeCost, false);
				NKCUtil.SetGameobjectActive(this.m_cTglSortTypeUID, false);
				NKCUtil.SetGameobjectActive(this.m_cTglSortTypePower, true);
				NKCUtil.SetGameobjectActive(this.m_cTglSortTypeAttack, false);
				NKCUtil.SetGameobjectActive(this.m_cTglSortTypeHP, false);
				NKCUtil.SetGameobjectActive(this.m_cTglSortTypeDefense, false);
				NKCUtil.SetGameobjectActive(this.m_cTglSortTypeCritical, false);
				NKCUtil.SetGameobjectActive(this.m_cTglSortTypeHit, false);
				NKCUtil.SetGameobjectActive(this.m_cTglSortTypeEvade, false);
				NKCUtil.SetGameobjectActive(this.m_cTglSortTypePlayerLevel, true);
				NKCUtil.SetGameobjectActive(this.m_cTglSortTypeLoginTime, true);
				NKCUtil.SetGameobjectActive(this.m_cTglSortTypeSetOption, false);
				NKCUtil.SetGameobjectActive(this.m_cTglSortTypeLimitBreakPossible, false);
				NKCUtil.SetGameobjectActive(this.m_cTglSortTypeLoyalty, false);
				NKCUtil.SetGameobjectActive(this.m_cTglSortTypeSquadDungeon, false);
				NKCUtil.SetGameobjectActive(this.m_cTglSortTypeTacticUpdatePossible, false);
				NKCUtil.SetGameobjectActive(this.m_cTglSortTypeSquadGauntlet, false);
				if (this.m_arrayCustomSortMenu != null)
				{
					int num = this.m_arrayCustomSortMenu.Length;
					for (int i = 0; i < num; i++)
					{
						NKCUtil.SetGameobjectActive(this.m_arrayCustomSortMenu[i].m_cTglSortTypeCustom, false);
					}
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
				this.StartRectMove(bValue, true);
				return;
			}
			this.Close();
		}

		// Token: 0x060050F4 RID: 20724 RVA: 0x0018913E File Offset: 0x0018733E
		public void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060050F5 RID: 20725 RVA: 0x0018914C File Offset: 0x0018734C
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

		// Token: 0x060050F6 RID: 20726 RVA: 0x001891D8 File Offset: 0x001873D8
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

		// Token: 0x060050F7 RID: 20727 RVA: 0x0018921B File Offset: 0x0018741B
		public void OnSort(List<NKCUnitSortSystem.eSortOption> sortList)
		{
			if (this.m_bUseDefaultSortAdd)
			{
				sortList = NKCUnitSortSystem.AddDefaultSortOptions(sortList, this.m_eUnitType, this.m_bIsCollection);
			}
			this.dOnSortOption(sortList);
			this.Close();
		}

		// Token: 0x060050F8 RID: 20728 RVA: 0x0018924C File Offset: 0x0018744C
		public void OnSort(NKCUnitSortSystem.eSortOption sortOption)
		{
			List<NKCUnitSortSystem.eSortOption> list = new List<NKCUnitSortSystem.eSortOption>();
			list.Add(sortOption);
			if (this.m_bUseDefaultSortAdd)
			{
				list = NKCUnitSortSystem.AddDefaultSortOptions(list, this.m_eUnitType, this.m_bIsCollection);
			}
			this.dOnSortOption(list);
			this.Close();
		}

		// Token: 0x0400413F RID: 16703
		[Header("정렬 방식 선택")]
		public NKCUIRectMove m_rmSortTypeMenu;

		// Token: 0x04004140 RID: 16704
		public NKCUIComToggle m_cTglSortTypeIdx;

		// Token: 0x04004141 RID: 16705
		public NKCUIComToggle m_cTglSortTypeScoutProgress;

		// Token: 0x04004142 RID: 16706
		public NKCUIComToggle m_cTglSortTypeLevel;

		// Token: 0x04004143 RID: 16707
		public NKCUIComToggle m_cTglSortTypeRarity;

		// Token: 0x04004144 RID: 16708
		public NKCUIComToggle m_cTglSortTypeSetOption;

		// Token: 0x04004145 RID: 16709
		public NKCUIComToggle m_cTglSortTypeCost;

		// Token: 0x04004146 RID: 16710
		public NKCUIComToggle m_cTglSortTypeTacticUpdatePossible;

		// Token: 0x04004147 RID: 16711
		public NKCUIComToggle m_cTglSortTypeUID;

		// Token: 0x04004148 RID: 16712
		public NKCUIComToggle m_cTglSortTypePower;

		// Token: 0x04004149 RID: 16713
		public NKCUIComToggle m_cTglSortTypeAttack;

		// Token: 0x0400414A RID: 16714
		public NKCUIComToggle m_cTglSortTypeHP;

		// Token: 0x0400414B RID: 16715
		public NKCUIComToggle m_cTglSortTypeDefense;

		// Token: 0x0400414C RID: 16716
		public NKCUIComToggle m_cTglSortTypeCritical;

		// Token: 0x0400414D RID: 16717
		public NKCUIComToggle m_cTglSortTypeHit;

		// Token: 0x0400414E RID: 16718
		public NKCUIComToggle m_cTglSortTypeEvade;

		// Token: 0x0400414F RID: 16719
		public NKCUIComToggle m_cTglSortTypeReduceSkillCool;

		// Token: 0x04004150 RID: 16720
		public NKCUIComToggle m_cTglSortTypePlayerLevel;

		// Token: 0x04004151 RID: 16721
		public NKCUIComToggle m_cTglSortTypeLoginTime;

		// Token: 0x04004152 RID: 16722
		public NKCUIComToggle m_cTglSortTypeGuildGrade;

		// Token: 0x04004153 RID: 16723
		public NKCUIComToggle m_cTglSortTypeGuildWeeklyPoint;

		// Token: 0x04004154 RID: 16724
		public NKCUIComToggle m_cTglSortTypeGuildTotalPoint;

		// Token: 0x04004155 RID: 16725
		public NKCUIComToggle m_cTglSortTypeLimitBreakPossible;

		// Token: 0x04004156 RID: 16726
		public NKCUIComToggle m_cTglSortTypeTranscendence;

		// Token: 0x04004157 RID: 16727
		public NKCUIComToggle m_cTglSortTypeLoyalty;

		// Token: 0x04004158 RID: 16728
		public NKCUIComToggle m_cTglSortTypeSquadDungeon;

		// Token: 0x04004159 RID: 16729
		public NKCUIComToggle m_cTglSortTypeSquadGauntlet;

		// Token: 0x0400415A RID: 16730
		[Header("커스텀 정렬 메뉴 텍스트")]
		public NKCPopupSort.CustomSortMenu[] m_arrayCustomSortMenu;

		// Token: 0x0400415B RID: 16731
		private NKCPopupSort.OnSortOption dOnSortOption;

		// Token: 0x0400415C RID: 16732
		private Dictionary<NKCUnitSortSystem.eSortOption, NKCUIComToggle> m_dicSortOption = new Dictionary<NKCUnitSortSystem.eSortOption, NKCUIComToggle>();

		// Token: 0x0400415D RID: 16733
		private NKM_UNIT_TYPE m_eUnitType;

		// Token: 0x0400415E RID: 16734
		private bool m_bIsCollection;

		// Token: 0x0400415F RID: 16735
		private bool m_bDescending;

		// Token: 0x04004160 RID: 16736
		private bool m_bInitComplete;

		// Token: 0x04004161 RID: 16737
		public bool m_bUseDefaultSortAdd = true;

		// Token: 0x04004162 RID: 16738
		private Dictionary<NKCUnitSortSystem.eSortCategory, NKCUIComToggle> m_dicToggle = new Dictionary<NKCUnitSortSystem.eSortCategory, NKCUIComToggle>();

		// Token: 0x04004163 RID: 16739
		private Dictionary<NKCUnitSortSystem.eSortOption, List<NKCUnitSortSystem.eSortOption>> m_dicSortOptionDetails = new Dictionary<NKCUnitSortSystem.eSortOption, List<NKCUnitSortSystem.eSortOption>>();

		// Token: 0x020014BA RID: 5306
		[Serializable]
		public struct CustomSortMenu
		{
			// Token: 0x04009EEF RID: 40687
			public NKCUIComToggle m_cTglSortTypeCustom;

			// Token: 0x04009EF0 RID: 40688
			public Text m_lbCustomOffText;

			// Token: 0x04009EF1 RID: 40689
			public Text m_lbCustomOnText;

			// Token: 0x04009EF2 RID: 40690
			public Text m_lbCustomPressText;
		}

		// Token: 0x020014BB RID: 5307
		// (Invoke) Token: 0x0600A9BF RID: 43455
		public delegate void OnSortOption(List<NKCUnitSortSystem.eSortOption> lstSortOptions);
	}
}
