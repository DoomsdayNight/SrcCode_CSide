using System;
using System.Collections;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009FC RID: 2556
	public class NKCUIUnitSelectListSlot : NKCUIUnitSelectListSlotBase
	{
		// Token: 0x06006F2E RID: 28462 RVA: 0x0024BCD0 File Offset: 0x00249ED0
		public override void SetData(NKMUnitData cNKMUnitData, NKMDeckIndex deckIndex, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot)
		{
			base.SetData(cNKMUnitData, deckIndex, bEnableLayoutElement, onSelectThisSlot);
			base.ProcessBanUIForUnit();
			NKCUtil.SetGameobjectActive(this.m_objExpBonus, false);
			NKCUtil.SetGameobjectActive(this.m_objCanLimitbreakNow, false);
			NKCUtil.SetGameobjectActive(this.m_objCanTacticUpdateNow, false);
			NKCUtil.SetGameobjectActive(this.m_objLifetime, false);
			NKCUtil.SetGameobjectActive(this.m_objContractGainUnit, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_UNIT_HAVE_COUNT, false);
			NKCUtil.SetGameobjectActive(this.m_objRecall, false);
			NKCUtil.SetGameobjectActive(this.m_objUnitAchievement, false);
			NKCUtil.SetGameobjectActive(this.m_objRedDot, false);
			if (cNKMUnitData != null)
			{
				if (this.m_lbSummonCost != null)
				{
					NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(cNKMUnitData.m_UnitID);
					if (this.m_bEnableShowBan && NKCBanManager.IsBanUnit(cNKMUnitData.m_UnitID, NKCBanManager.BAN_DATA_TYPE.FINAL))
					{
						int respawnCost = unitStatTemplet.GetRespawnCost(true, false, NKCBanManager.GetBanData(NKCBanManager.BAN_DATA_TYPE.FINAL), null);
						this.m_lbSummonCost.text = string.Format(NKCUtilString.GET_STRING_UNIT_BAN_COST, respawnCost.ToString());
					}
					else if (this.m_bEnableShowUpUnit && NKCBanManager.IsUpUnit(cNKMUnitData.m_UnitID))
					{
						int respawnCost2 = unitStatTemplet.GetRespawnCost(true, false, null, NKCBanManager.m_dicNKMUpData);
						this.m_lbSummonCost.text = string.Format(NKCUtilString.GET_STRING_UNIT_UP_COST, respawnCost2.ToString());
					}
					else
					{
						this.m_lbSummonCost.text = unitStatTemplet.GetRespawnCost(false, null, null).ToString();
					}
				}
				this.SetClassMark(this.m_NKMUnitTempletBase.m_NKM_UNIT_STYLE_TYPE);
				if (this.m_sliderExp != null)
				{
					if (NKCExpManager.GetUnitMaxLevel(cNKMUnitData) == cNKMUnitData.m_UnitLevel)
					{
						this.m_sliderExp.value = 1f;
					}
					else
					{
						this.m_sliderExp.value = NKCExpManager.GetUnitNextLevelExpProgress(cNKMUnitData);
					}
				}
				NKCUIComStarRank comStarRank = this.m_comStarRank;
				if (comStarRank != null)
				{
					comStarRank.SetStarRank(cNKMUnitData);
				}
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(cNKMUnitData);
				if (unitTempletBase != null)
				{
					Sprite unitClassIcon = this.GetUnitClassIcon(unitTempletBase);
					NKCUtil.SetGameobjectActive(this.m_imgUnitRole, unitClassIcon != null);
					if (this.m_imgUnitRole != null)
					{
						this.m_imgUnitRole.sprite = unitClassIcon;
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_imgUnitRole, false);
				}
				if (this.m_Img_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_LEVEL_GAUGE != null)
				{
					if (NKCExpManager.GetUnitMaxLevel(cNKMUnitData) == cNKMUnitData.m_UnitLevel)
					{
						this.m_Img_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_LEVEL_GAUGE.fillAmount = 1f;
					}
					else
					{
						this.m_Img_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_LEVEL_GAUGE.fillAmount = NKCExpManager.GetUnitNextLevelExpProgress(cNKMUnitData);
					}
				}
				NKCUtil.SetGameobjectActive(this.m_objLifetime, cNKMUnitData.IsPermanentContract);
			}
			this.SetEnableEquipListData(!this.m_bEnableShowCastingBan);
			this.SetEquipListData(cNKMUnitData);
			this.SetCityLeaderMark(false);
			this.SetFierceBattleOtherBossAlreadyUsed(false);
		}

		// Token: 0x06006F2F RID: 28463 RVA: 0x0024BF48 File Offset: 0x0024A148
		public override void SetData(NKMUnitTempletBase templetBase, int levelToDisplay, int skinID, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot)
		{
			base.SetData(templetBase, levelToDisplay, skinID, bEnableLayoutElement, onSelectThisSlot);
			NKCUtil.SetGameobjectActive(this.m_objExpBonus, false);
			NKCUtil.SetGameobjectActive(this.m_objCanLimitbreakNow, false);
			NKCUtil.SetGameobjectActive(this.m_objCanTacticUpdateNow, false);
			NKCUtil.SetGameobjectActive(this.m_objLifetime, false);
			NKCUtil.SetGameobjectActive(this.m_objContractGainUnit, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_UNIT_HAVE_COUNT, false);
			NKCUtil.SetGameobjectActive(this.m_objBan, false);
			NKCUtil.SetGameobjectActive(this.m_objRecall, false);
			NKCUtil.SetGameobjectActive(this.m_objUnitAchievement, false);
			NKCUtil.SetGameobjectActive(this.m_objRedDot, false);
			if (templetBase != null)
			{
				if (this.m_lbSummonCost != null)
				{
					NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(templetBase.m_UnitID);
					if (unitStatTemplet != null)
					{
						this.m_lbSummonCost.text = unitStatTemplet.GetRespawnCost(false, null, null).ToString();
					}
				}
				this.SetClassMark(templetBase.m_NKM_UNIT_STYLE_TYPE);
				if (this.m_sliderExp != null)
				{
					this.m_sliderExp.value = 0f;
				}
				NKCUIComStarRank comStarRank = this.m_comStarRank;
				if (comStarRank != null)
				{
					comStarRank.SetStarRank(templetBase.m_StarGradeMax - 3, templetBase.m_StarGradeMax, false);
				}
				Sprite unitClassIcon = this.GetUnitClassIcon(templetBase);
				NKCUtil.SetGameobjectActive(this.m_imgUnitRole, unitClassIcon != null);
				if (this.m_imgUnitRole != null)
				{
					this.m_imgUnitRole.sprite = unitClassIcon;
				}
			}
			this.SetEnableEquipListData(false);
			this.SetEquipListData(null);
			this.SetCityLeaderMark(false);
			this.SetFierceBattleOtherBossAlreadyUsed(false);
		}

		// Token: 0x06006F30 RID: 28464 RVA: 0x0024C0B4 File Offset: 0x0024A2B4
		public override void SetDataForBan(NKMUnitTempletBase templetBase, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot, bool bUp = false, bool bSetOriginalCost = false)
		{
			base.SetData(templetBase, 0, bEnableLayoutElement, onSelectThisSlot);
			NKCUtil.SetGameobjectActive(this.m_objExpBonus, false);
			NKCUtil.SetGameobjectActive(this.m_objCanLimitbreakNow, false);
			NKCUtil.SetGameobjectActive(this.m_objCanTacticUpdateNow, false);
			NKCUtil.SetGameobjectActive(this.m_objLifetime, false);
			NKCUtil.SetGameobjectActive(this.m_objContractGainUnit, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_UNIT_HAVE_COUNT, false);
			NKCUtil.SetGameobjectActive(this.m_objRecall, false);
			NKCUtil.SetGameobjectActive(this.m_objUnitAchievement, false);
			NKCUtil.SetGameobjectActive(this.m_objRedDot, false);
			base.ProcessBanUIForUnit();
			if (templetBase != null)
			{
				if (this.m_lbSummonCost != null)
				{
					NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(templetBase.m_UnitID);
					if (unitStatTemplet != null)
					{
						if (bSetOriginalCost)
						{
							int respawnCost = unitStatTemplet.GetRespawnCost(true, false, null, null);
							NKCUtil.SetLabelText(this.m_lbSummonCost, respawnCost.ToString());
						}
						else if (!bUp)
						{
							int respawnCost = unitStatTemplet.GetRespawnCost(true, false, NKCBanManager.GetBanData(this.m_eBanDataType), null);
							this.m_lbSummonCost.text = string.Format(NKCUtilString.GET_STRING_UNIT_BAN_COST, respawnCost.ToString());
						}
						else
						{
							int respawnCost = unitStatTemplet.GetRespawnCost(true, false, null, NKCBanManager.m_dicNKMUpData);
							this.m_lbSummonCost.text = string.Format(NKCUtilString.GET_STRING_UNIT_UP_COST, respawnCost.ToString());
						}
					}
				}
				this.SetClassMark(templetBase.m_NKM_UNIT_STYLE_TYPE);
				Sprite unitClassIcon = this.GetUnitClassIcon(templetBase);
				NKCUtil.SetGameobjectActive(this.m_imgUnitRole, unitClassIcon != null);
				if (this.m_imgUnitRole != null)
				{
					this.m_imgUnitRole.sprite = unitClassIcon;
				}
			}
			this.SetEnableEquipListData(false);
			this.SetEquipListData(null);
			this.SetCityLeaderMark(false);
			this.SetFierceBattleOtherBossAlreadyUsed(false);
		}

		// Token: 0x06006F31 RID: 28465 RVA: 0x0024C24C File Offset: 0x0024A44C
		public override void SetDataForContractSelection(NKMUnitData cNKMUnitData, bool bHave = true)
		{
			NKCUtil.SetGameobjectActive(this.m_objInCityMission, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_EMPLOYEE, false);
			base.SetData(cNKMUnitData, NKMDeckIndex.None, true, null);
			NKCUtil.SetGameobjectActive(this.m_objDisableSelectSlot, false);
			if (cNKMUnitData != null)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(cNKMUnitData);
				if (unitTempletBase != null)
				{
					NKCUIComStarRank comStarRank = this.m_comStarRank;
					if (comStarRank != null)
					{
						comStarRank.SetStarRank(unitTempletBase.m_StarGradeMax - 3 + (int)cNKMUnitData.m_LimitBreakLevel, unitTempletBase.m_StarGradeMax, false);
					}
					Sprite unitClassIcon = this.GetUnitClassIcon(unitTempletBase);
					NKCUtil.SetGameobjectActive(this.m_imgUnitRole, unitClassIcon != null);
					if (this.m_imgUnitRole != null)
					{
						this.m_imgUnitRole.sprite = unitClassIcon;
					}
					this.SetClassMark(unitTempletBase.m_NKM_UNIT_STYLE_TYPE);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_imgUnitRole, false);
				}
				if (this.m_lbSummonCost != null)
				{
					NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(cNKMUnitData.m_UnitID);
					if (unitStatTemplet != null)
					{
						this.m_lbSummonCost.text = unitStatTemplet.GetRespawnCost(false, null, null).ToString();
					}
				}
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_LEVEL, false);
				if (bHave)
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_UNIT_HAVE_COUNT, !NKCScenManager.CurrentUserData().m_ArmyData.IsFirstGetUnit(cNKMUnitData.m_UnitID));
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_UNIT_HAVE_COUNT, false);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_DISABLE, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_CARD_DENIED, false);
			this.UpdateCommonUIForMaxData(true);
		}

		// Token: 0x06006F32 RID: 28466 RVA: 0x0024C3A8 File Offset: 0x0024A5A8
		public override void SetDataForCollection(NKMUnitData cNKMUnitData, NKMDeckIndex deckIndex, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot, bool bEnable = false)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_UNIT_HAVE_COUNT, false);
			base.SetData(cNKMUnitData, deckIndex, true, onSelectThisSlot);
			if (cNKMUnitData != null)
			{
				NKCCollectionUnitTemplet unitTemplet = NKCCollectionManager.GetUnitTemplet(cNKMUnitData.m_UnitID);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_EMPLOYEE, unitTemplet != null && !unitTemplet.m_bExclude);
				NKCUtil.SetLabelText(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_EMPLOYEE_TEXT, NKCCollectionManager.GetEmployeeNumber(cNKMUnitData.m_UnitID));
				if (this.m_lbSummonCost != null)
				{
					NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(cNKMUnitData.m_UnitID);
					if (unitStatTemplet != null)
					{
						this.m_lbSummonCost.text = unitStatTemplet.GetRespawnCost(false, null, null).ToString();
					}
				}
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(cNKMUnitData);
				if (unitTempletBase != null)
				{
					NKCUIComStarRank comStarRank = this.m_comStarRank;
					if (comStarRank != null)
					{
						comStarRank.SetStarRank(unitTempletBase.m_StarGradeMax, unitTempletBase.m_StarGradeMax, false);
					}
					Sprite unitClassIcon = this.GetUnitClassIcon(unitTempletBase);
					NKCUtil.SetGameobjectActive(this.m_imgUnitRole, unitClassIcon != null);
					if (this.m_imgUnitRole != null)
					{
						this.m_imgUnitRole.sprite = unitClassIcon;
					}
					this.SetClassMark(unitTempletBase.m_NKM_UNIT_STYLE_TYPE);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_imgUnitRole, false);
				}
				if (this.m_Img_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_LEVEL_GAUGE != null)
				{
					if (NKCExpManager.GetUnitMaxLevel(cNKMUnitData) == cNKMUnitData.m_UnitLevel)
					{
						this.m_Img_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_LEVEL_GAUGE.fillAmount = 1f;
					}
					else
					{
						this.m_Img_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_LEVEL_GAUGE.fillAmount = NKCExpManager.GetUnitNextLevelExpProgress(cNKMUnitData);
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.m_imgAttackType, bEnable);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_DISABLE, !bEnable);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_CARD_DENIED, !bEnable);
			this.UpdateCommonUIForMaxData(bEnable);
		}

		// Token: 0x06006F33 RID: 28467 RVA: 0x0024C533 File Offset: 0x0024A733
		public override void SetDataForRearm(NKMUnitData unitData, NKMDeckIndex deckIndex, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot, bool bShowEqup = true, bool bShowLevel = false, bool bUnable = false)
		{
			this.SetData(unitData, deckIndex, bEnableLayoutElement, onSelectThisSlot);
			this.SetEnableLevelInfo(bShowLevel);
			this.SetEnableEquipListData(bShowEqup);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_DISABLE, bUnable);
		}

		// Token: 0x06006F34 RID: 28468 RVA: 0x0024C55D File Offset: 0x0024A75D
		public void SetSlotDisable(bool bDisable)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_DISABLE, bDisable);
		}

		// Token: 0x06006F35 RID: 28469 RVA: 0x0024C56B File Offset: 0x0024A76B
		protected override void OnClick()
		{
			if (this.dOnSelectThisSlot != null)
			{
				this.dOnSelectThisSlot(this.m_NKMUnitData, this.m_NKMUnitTempletBase, this.m_DeckIndex, this.m_eUnitSlotState, this.m_eUnitSelectState);
			}
		}

		// Token: 0x06006F36 RID: 28470 RVA: 0x0024C5A0 File Offset: 0x0024A7A0
		private void UpdateCommonUIForMaxData(bool bEnable)
		{
			NKCUtil.SetGameobjectActive(this.m_objDisableSelectSlot, false);
			NKCUtil.SetGameobjectActive(this.m_objExpBonus, false);
			NKCUtil.SetGameobjectActive(this.m_objCanLimitbreakNow, false);
			NKCUtil.SetGameobjectActive(this.m_objCanTacticUpdateNow, false);
			NKCUtil.SetGameobjectActive(this.m_objLifetime, false);
			NKCUtil.SetGameobjectActive(this.m_objContractGainUnit, false);
			NKCUtil.SetGameobjectActive(this.m_objBan, false);
			NKCUtil.SetGameobjectActive(this.m_objRecall, false);
			this.SetEnableEquipListData(false);
			this.SetFierceBattleOtherBossAlreadyUsed(false);
			this.SetEquipListData(null);
			this.SetCityLeaderMark(false);
			this.SetUnitAchievementInfo();
			NKCUtil.SetGameobjectActive(this.m_lbName.gameObject, bEnable);
			NKCUtil.SetGameobjectActive(this.m_imgUnitRole.gameObject, bEnable);
			NKCUtil.SetGameobjectActive(this.m_comStarRank.gameObject, bEnable);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_STAR_GRADE, bEnable);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_COST, bEnable);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_BOTTOM, bEnable);
		}

		// Token: 0x06006F37 RID: 28471 RVA: 0x0024C686 File Offset: 0x0024A886
		public void SetExpBonusMark(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_objExpBonus, value);
		}

		// Token: 0x06006F38 RID: 28472 RVA: 0x0024C694 File Offset: 0x0024A894
		public void SetLimitPossibleMark(bool value, bool bTranscendence)
		{
			NKCUtil.SetGameobjectActive(this.m_objCanLimitbreakNow, value);
			if (value)
			{
				NKCUtil.SetGameobjectActive(this.m_objRootLimitBreak, !bTranscendence);
				NKCUtil.SetGameobjectActive(this.m_objRootTranscendence, bTranscendence);
				NKCUtil.SetGameobjectActive(this.m_objLimitBreakCharCount, false);
				NKCUtil.SetGameobjectActive(this.m_objTranscendenceCharCount, false);
			}
		}

		// Token: 0x06006F39 RID: 28473 RVA: 0x0024C6E3 File Offset: 0x0024A8E3
		public void SetTacticPossibleMark(int sameCharCount)
		{
			NKCUtil.SetGameobjectActive(this.m_objCanTacticUpdateNow, sameCharCount > 0);
			NKCUtil.SetLabelText(this.m_lbTacticUpdateCharCount, sameCharCount.ToString());
		}

		// Token: 0x06006F3A RID: 28474 RVA: 0x0024C706 File Offset: 0x0024A906
		private void SetClassMark(NKM_UNIT_STYLE_TYPE type)
		{
			NKCUtil.SetLabelText(this.m_lbClassMark, NKCUtilString.GetUnitStyleName(type));
		}

		// Token: 0x06006F3B RID: 28475 RVA: 0x0024C719 File Offset: 0x0024A919
		protected override void SetMode(NKCUIUnitSelectListSlotBase.eUnitSlotMode mode)
		{
			base.SetMode(mode);
			if (mode != NKCUIUnitSelectListSlotBase.eUnitSlotMode.Character)
			{
				this.SetEquipListData(null);
				return;
			}
			this.SetEquipListData(this.m_NKMUnitData);
		}

		// Token: 0x06006F3C RID: 28476 RVA: 0x0024C739 File Offset: 0x0024A939
		public override void SetCityLeaderMark(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_objCityLeader, value);
		}

		// Token: 0x06006F3D RID: 28477 RVA: 0x0024C747 File Offset: 0x0024A947
		private Sprite GetUnitClassIcon(NKMUnitTempletBase unitTempletBase)
		{
			return NKCResourceUtility.GetOrLoadUnitRoleIcon(unitTempletBase, true);
		}

		// Token: 0x06006F3E RID: 28478 RVA: 0x0024C750 File Offset: 0x0024A950
		protected void SetEnableEquipListData(bool bEnable)
		{
			NKCUtil.SetGameobjectActive(this.m_objEquip, bEnable);
		}

		// Token: 0x06006F3F RID: 28479 RVA: 0x0024C75E File Offset: 0x0024A95E
		protected void SetEnableLevelInfo(bool bEnable)
		{
			NKCUtil.SetGameobjectActive(this.m_objLevelInfo, bEnable);
		}

		// Token: 0x06006F40 RID: 28480 RVA: 0x0024C76C File Offset: 0x0024A96C
		public override void SetContractedUnitMark(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_objContractGainUnit, value);
		}

		// Token: 0x06006F41 RID: 28481 RVA: 0x0024C77A File Offset: 0x0024A97A
		public override void SetRecall(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objRecall, bValue);
		}

		// Token: 0x06006F42 RID: 28482 RVA: 0x0024C788 File Offset: 0x0024A988
		protected void SetEquipListData(NKMUnitData unitData)
		{
			this.ClearSetOptionEffect();
			if (unitData == null)
			{
				using (IEnumerator enumerator = Enum.GetValues(typeof(ITEM_EQUIP_POSITION)).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						ITEM_EQUIP_POSITION position = (ITEM_EQUIP_POSITION)obj;
						NKCUtil.SetImageSprite(this.GetEquipIconImage(position), this.m_spEquipEmpty, false);
					}
					return;
				}
			}
			foreach (object obj2 in Enum.GetValues(typeof(ITEM_EQUIP_POSITION)))
			{
				ITEM_EQUIP_POSITION position2 = (ITEM_EQUIP_POSITION)obj2;
				this.SetWeaponImage(unitData, position2);
			}
		}

		// Token: 0x06006F43 RID: 28483 RVA: 0x0024C854 File Offset: 0x0024AA54
		private void ClearSetOptionEffect()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_EQUIP_1_SET, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_EQUIP_2_SET, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_EQUIP_3_SET, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_EQUIP_4_SET, false);
		}

		// Token: 0x06006F44 RID: 28484 RVA: 0x0024C888 File Offset: 0x0024AA88
		private void SetWeaponImage(NKMUnitData unitData, ITEM_EQUIP_POSITION position)
		{
			Image equipIconImage = this.GetEquipIconImage(position);
			if (equipIconImage == null)
			{
				return;
			}
			long equipUid = unitData.GetEquipUid(position);
			if (equipUid == 0L)
			{
				if (position == ITEM_EQUIP_POSITION.IEP_ACC2 && !unitData.IsUnlockAccessory2())
				{
					equipIconImage.sprite = this.m_spEquipLock;
					return;
				}
				equipIconImage.sprite = this.m_spEquipEmpty;
				return;
			}
			else
			{
				if (NKMItemManager.IsActiveSetOptionItem(equipUid))
				{
					switch (position)
					{
					case ITEM_EQUIP_POSITION.IEP_WEAPON:
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_EQUIP_1_SET, true);
						break;
					case ITEM_EQUIP_POSITION.IEP_DEFENCE:
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_EQUIP_2_SET, true);
						break;
					case ITEM_EQUIP_POSITION.IEP_ACC:
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_EQUIP_3_SET, true);
						break;
					case ITEM_EQUIP_POSITION.IEP_ACC2:
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_EQUIP_4_SET, true);
						break;
					}
				}
				NKMEquipItemData itemEquip = NKCScenManager.CurrentUserData().m_InventoryData.GetItemEquip(equipUid);
				if (itemEquip == null)
				{
					Debug.LogError(string.Format("equipped equip not exist. uid {0}", equipUid));
					equipIconImage.sprite = this.m_spEquipEmpty;
					return;
				}
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID);
				if (equipTemplet == null)
				{
					Debug.LogError(string.Format("equiptemplet not exist. id {0}", itemEquip.m_ItemEquipID));
					equipIconImage.sprite = this.m_spEquipEmpty;
					return;
				}
				equipIconImage.sprite = this.GetItemSprite(equipTemplet.m_NKM_ITEM_GRADE);
				return;
			}
		}

		// Token: 0x06006F45 RID: 28485 RVA: 0x0024C9AC File Offset: 0x0024ABAC
		private void SetUnitAchievementInfo()
		{
			bool openTagCollectionMission = NKCUnitMissionManager.GetOpenTagCollectionMission();
			NKCUtil.SetGameobjectActive(this.m_objUnitAchievement, openTagCollectionMission);
			if (!openTagCollectionMission)
			{
				NKCUtil.SetGameobjectActive(this.m_objRedDot, false);
				return;
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			NKCUnitMissionManager.GetUnitMissionRewardEnableCount(this.m_NKMUnitData.m_UnitID, ref num, ref num2, ref num3);
			NKCUtil.SetGameobjectActive(this.m_objAchievementGauge, num > num2);
			NKCUtil.SetGameobjectActive(this.m_objAchievementComplete, num <= num2);
			NKCUtil.SetLabelText(this.m_lbAchieveCount, string.Format("{0}/{1}", num2, num));
			NKCUtil.SetImageFillAmount(this.m_imgAchieveGauge, (float)num2 / (float)num);
			NKCUtil.SetGameobjectActive(this.m_objRedDot, num3 > 0);
		}

		// Token: 0x06006F46 RID: 28486 RVA: 0x0024CA58 File Offset: 0x0024AC58
		private Image GetEquipIconImage(ITEM_EQUIP_POSITION position)
		{
			switch (position)
			{
			case ITEM_EQUIP_POSITION.IEP_WEAPON:
				return this.m_imgEquipWeapon;
			case ITEM_EQUIP_POSITION.IEP_DEFENCE:
				return this.m_imgEquipArmor;
			case ITEM_EQUIP_POSITION.IEP_ACC:
				return this.m_imgEquipAcc;
			case ITEM_EQUIP_POSITION.IEP_ACC2:
				return this.m_imgEquipAcc2;
			default:
				return null;
			}
		}

		// Token: 0x06006F47 RID: 28487 RVA: 0x0024CA8F File Offset: 0x0024AC8F
		private Sprite GetItemSprite(NKM_ITEM_GRADE grade)
		{
			switch (grade)
			{
			default:
				return this.m_spEquipN;
			case NKM_ITEM_GRADE.NIG_R:
				return this.m_spEquipR;
			case NKM_ITEM_GRADE.NIG_SR:
				return this.m_spEquipSR;
			case NKM_ITEM_GRADE.NIG_SSR:
				return this.m_spEquipSSR;
			}
		}

		// Token: 0x04005AA0 RID: 23200
		[Header("일반 유닛 전용 정보")]
		public Slider m_sliderExp;

		// Token: 0x04005AA1 RID: 23201
		public Text m_lbSummonCost;

		// Token: 0x04005AA2 RID: 23202
		public Text m_lbClassMark;

		// Token: 0x04005AA3 RID: 23203
		public GameObject m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_EMPLOYEE;

		// Token: 0x04005AA4 RID: 23204
		public Text m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_EMPLOYEE_TEXT;

		// Token: 0x04005AA5 RID: 23205
		public GameObject m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_DISABLE;

		// Token: 0x04005AA6 RID: 23206
		public GameObject m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_CARD_DENIED;

		// Token: 0x04005AA7 RID: 23207
		public GameObject m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_STAR_GRADE;

		// Token: 0x04005AA8 RID: 23208
		public GameObject m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_COST;

		// Token: 0x04005AA9 RID: 23209
		public GameObject m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_BOTTOM;

		// Token: 0x04005AAA RID: 23210
		[Header("역할군 마크")]
		public Image m_imgUnitRole;

		// Token: 0x04005AAB RID: 23211
		[Header("레벨 정보")]
		public GameObject m_objLevelInfo;

		// Token: 0x04005AAC RID: 23212
		[Header("하단 장비 표시")]
		public GameObject m_objEquip;

		// Token: 0x04005AAD RID: 23213
		public Sprite m_spEquipEmpty;

		// Token: 0x04005AAE RID: 23214
		public Sprite m_spEquipN;

		// Token: 0x04005AAF RID: 23215
		public Sprite m_spEquipR;

		// Token: 0x04005AB0 RID: 23216
		public Sprite m_spEquipSR;

		// Token: 0x04005AB1 RID: 23217
		public Sprite m_spEquipSSR;

		// Token: 0x04005AB2 RID: 23218
		public Sprite m_spEquipLock;

		// Token: 0x04005AB3 RID: 23219
		public Image m_imgEquipWeapon;

		// Token: 0x04005AB4 RID: 23220
		public Image m_imgEquipArmor;

		// Token: 0x04005AB5 RID: 23221
		public Image m_imgEquipAcc;

		// Token: 0x04005AB6 RID: 23222
		public Image m_imgEquipAcc2;

		// Token: 0x04005AB7 RID: 23223
		public GameObject m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_EQUIP_1_SET;

		// Token: 0x04005AB8 RID: 23224
		public GameObject m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_EQUIP_2_SET;

		// Token: 0x04005AB9 RID: 23225
		public GameObject m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_EQUIP_3_SET;

		// Token: 0x04005ABA RID: 23226
		public GameObject m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_EQUIP_4_SET;

		// Token: 0x04005ABB RID: 23227
		[Header("경험치 보너스 있음")]
		public GameObject m_objExpBonus;

		// Token: 0x04005ABC RID: 23228
		[Header("당장 초월 가능")]
		public GameObject m_objCanLimitbreakNow;

		// Token: 0x04005ABD RID: 23229
		public GameObject m_objRootLimitBreak;

		// Token: 0x04005ABE RID: 23230
		public GameObject m_objLimitBreakCharCount;

		// Token: 0x04005ABF RID: 23231
		public Text m_lbLimitBreakCharCount;

		// Token: 0x04005AC0 RID: 23232
		public GameObject m_objRootTranscendence;

		// Token: 0x04005AC1 RID: 23233
		public GameObject m_objTranscendenceCharCount;

		// Token: 0x04005AC2 RID: 23234
		public Text m_lbTranscendenceCharCount;

		// Token: 0x04005AC3 RID: 23235
		[Space]
		public GameObject m_objCanTacticUpdateNow;

		// Token: 0x04005AC4 RID: 23236
		public Text m_lbTacticUpdateCharCount;

		// Token: 0x04005AC5 RID: 23237
		[Header("경험치 바")]
		public Image m_Img_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_LEVEL_GAUGE;

		// Token: 0x04005AC6 RID: 23238
		[Header("종신계약")]
		public GameObject m_objLifetime;

		// Token: 0x04005AC7 RID: 23239
		[Header("월드맵 지부장 표시")]
		public GameObject m_objCityLeader;

		// Token: 0x04005AC8 RID: 23240
		[Header("채용획득 표기")]
		public GameObject m_objContractGainUnit;

		// Token: 0x04005AC9 RID: 23241
		[Header("보유중")]
		public GameObject m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_UNIT_HAVE_COUNT;

		// Token: 0x04005ACA RID: 23242
		[Header("격전 지원")]
		public Text m_NKM_UI_UNIT_SELECT_LIST_FIERCE_BATTLE_TEXT;

		// Token: 0x04005ACB RID: 23243
		[Header("리콜")]
		public GameObject m_objRecall;

		// Token: 0x04005ACC RID: 23244
		[Header("미션 달성도")]
		public GameObject m_objUnitAchievement;

		// Token: 0x04005ACD RID: 23245
		public GameObject m_objAchievementGauge;

		// Token: 0x04005ACE RID: 23246
		public GameObject m_objAchievementComplete;

		// Token: 0x04005ACF RID: 23247
		public Image m_imgAchieveGauge;

		// Token: 0x04005AD0 RID: 23248
		public Text m_lbAchieveCount;

		// Token: 0x04005AD1 RID: 23249
		[Header("레드닷")]
		public GameObject m_objRedDot;
	}
}
