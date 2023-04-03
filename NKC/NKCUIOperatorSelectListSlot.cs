using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A23 RID: 2595
	public class NKCUIOperatorSelectListSlot : NKCUIUnitSelectListSlotBase
	{
		// Token: 0x060071AD RID: 29101 RVA: 0x0025C91C File Offset: 0x0025AB1C
		public static NKCUIOperatorSelectListSlot GetNewInstance(Transform parent = null)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("ab_ui_nkm_ui_operator_card", "NKM_UI_OPERATOR_CARD_SLOT", false, null);
			NKCUIOperatorSelectListSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIOperatorSelectListSlot>();
			if (component == null)
			{
				Debug.LogError("NKM_UI_OPERATOR_CARD_SLOT Prefab null!");
				return null;
			}
			if (parent != null)
			{
				component.transform.SetParent(parent);
				component.GetComponent<RectTransform>().localScale = Vector3.one;
				component.Init();
			}
			component.m_Instance = nkcassetInstanceData;
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x060071AE RID: 29102 RVA: 0x0025C99B File Offset: 0x0025AB9B
		public override void SetData(NKMUnitData cNKMUnitData, NKMDeckIndex deckIndex, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot)
		{
		}

		// Token: 0x060071AF RID: 29103 RVA: 0x0025C9A0 File Offset: 0x0025ABA0
		public override void SetData(NKMOperator operatorData, NKMDeckIndex deckIndex, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot onSelectThisSlot)
		{
			base.SetData(operatorData, deckIndex, bEnableLayoutElement, onSelectThisSlot);
			if (operatorData == null)
			{
				return;
			}
			this.m_CurOperator = operatorData;
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_CurOperator.id);
			if (unitTempletBase != null)
			{
				NKCUtil.SetLabelText(this.m_NKM_UI_OPERATOR_CARD_TITLE_TEXT, unitTempletBase.GetUnitTitle());
			}
			for (int i = 0; i < this.m_lstSkill.Count; i++)
			{
				if (operatorData == null)
				{
					NKCUtil.SetGameobjectActive(this.m_lstSkill[i].m_Object, false);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstSkill[i].m_Object, true);
					int level;
					NKMOperatorSkillTemplet skillTemplet;
					if (i == 0)
					{
						level = (int)this.m_CurOperator.mainSkill.level;
						skillTemplet = NKCOperatorUtil.GetSkillTemplet(this.m_CurOperator.mainSkill.id);
						this.m_lstSkill[i].m_SkillInfo.SetData(this.m_CurOperator.mainSkill.id, (int)this.m_CurOperator.mainSkill.level, false);
					}
					else
					{
						level = (int)this.m_CurOperator.subSkill.level;
						skillTemplet = NKCOperatorUtil.GetSkillTemplet(this.m_CurOperator.subSkill.id);
						this.m_lstSkill[i].m_SkillInfo.SetData(this.m_CurOperator.subSkill.id, (int)this.m_CurOperator.subSkill.level, false);
					}
					if (skillTemplet != null && skillTemplet.m_MaxSkillLevel == level)
					{
						NKCUtil.SetGameobjectActive(this.m_lstSkill[i].m_Max, true);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_lstSkill[i].m_Max, false);
					}
				}
			}
			if (this.m_NKM_UI_OPERATOR_CARD_LEVEL_GAUGE != null)
			{
				if (NKMCommonConst.OperatorConstTemplet.unitMaximumLevel == this.m_CurOperator.level)
				{
					this.m_NKM_UI_OPERATOR_CARD_LEVEL_GAUGE.fillAmount = 1f;
				}
				else
				{
					this.m_NKM_UI_OPERATOR_CARD_LEVEL_GAUGE.fillAmount = NKCExpManager.GetOperatorNextLevelExpProgress(this.m_CurOperator);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATOR_CARD_LEVEL_BG, true);
			this.SetFierceBattleOtherBossAlreadyUsed(false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_DISABLE, false);
			NKCUtil.SetGameobjectActive(this.m_objBan, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_LEVEL, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATOR_CARD_SKILL, true);
		}

		// Token: 0x060071B0 RID: 29104 RVA: 0x0025CBCB File Offset: 0x0025ADCB
		private void Init()
		{
			if (this.m_NKM_UI_OPERATOR_CARD_SLOT != null)
			{
				this.m_NKM_UI_OPERATOR_CARD_SLOT.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_OPERATOR_CARD_SLOT.PointerClick.AddListener(new UnityAction(this.OnClick));
			}
		}

		// Token: 0x060071B1 RID: 29105 RVA: 0x0025CC08 File Offset: 0x0025AE08
		public void Clear()
		{
			if (this.m_Instance != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_Instance);
			}
		}

		// Token: 0x060071B2 RID: 29106 RVA: 0x0025CC1D File Offset: 0x0025AE1D
		public override void SetDataForCollection(NKMUnitData cNKMUnitData, NKMDeckIndex deckIndex, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot, bool bEnable = false)
		{
		}

		// Token: 0x060071B3 RID: 29107 RVA: 0x0025CC20 File Offset: 0x0025AE20
		public override void SetDataForCollection(NKMOperator operatorData, NKMDeckIndex deckIndex, NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot onSelectThisSlot, bool bEnable = false)
		{
			base.SetData(operatorData, deckIndex, true, onSelectThisSlot);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(operatorData.id);
			if (unitTempletBase != null)
			{
				NKCUtil.SetLabelText(this.m_NKM_UI_OPERATOR_CARD_TITLE_TEXT, unitTempletBase.GetUnitTitle());
			}
			NKCCollectionUnitTemplet unitTemplet = NKCCollectionManager.GetUnitTemplet(operatorData.id);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATOR_CARD_SLOT_COLLECTION_EMPLOYEE, unitTemplet != null && !unitTemplet.m_bExclude);
			NKCUtil.SetLabelText(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_EMPLOYEE_TEXT, NKCCollectionManager.GetEmployeeNumber(operatorData.id));
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_DISABLE, !bEnable);
		}

		// Token: 0x060071B4 RID: 29108 RVA: 0x0025CCA4 File Offset: 0x0025AEA4
		public override void SetDataForContractSelection(NKMOperator cNKMOperData)
		{
			base.SetData(cNKMOperData, NKMDeckIndex.None, true, null);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(cNKMOperData.id);
			if (unitTempletBase != null)
			{
				NKCUtil.SetLabelText(this.m_NKM_UI_OPERATOR_CARD_TITLE_TEXT, unitTempletBase.GetUnitTitle());
			}
			NKCUtil.SetLabelText(this.m_lbLevel, 1.ToString());
			NKCUtil.SetGameobjectActive(this.m_objMaxExp, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATOR_CARD_LEVEL_BG, false);
			if (this.m_NKM_UI_OPERATOR_CARD_LEVEL_GAUGE != null)
			{
				this.m_NKM_UI_OPERATOR_CARD_LEVEL_GAUGE.fillAmount = 0f;
			}
			foreach (NKCUIOperatorSelectListSlot.SkillInfo skillInfo in this.m_lstSkill)
			{
				NKCUtil.SetGameobjectActive(skillInfo.m_Object, false);
			}
		}

		// Token: 0x060071B5 RID: 29109 RVA: 0x0025CD74 File Offset: 0x0025AF74
		public override void SetData(NKMUnitTempletBase templetBase, int levelToDisplay, int skinID, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot onSelectThisSlot)
		{
			base.SetData(templetBase, levelToDisplay, skinID, bEnableLayoutElement, onSelectThisSlot);
			if (templetBase != null)
			{
				NKCUtil.SetLabelText(this.m_NKM_UI_OPERATOR_CARD_TITLE_TEXT, templetBase.GetUnitTitle());
			}
			this.m_CurOperator = null;
			for (int i = 0; i < this.m_lstSkill.Count; i++)
			{
				if (this.m_CurOperator == null)
				{
					NKCUtil.SetGameobjectActive(this.m_lstSkill[i].m_Object, false);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstSkill[i].m_Object, true);
					int level;
					NKMOperatorSkillTemplet skillTemplet;
					if (i == 0)
					{
						level = (int)this.m_CurOperator.mainSkill.level;
						skillTemplet = NKCOperatorUtil.GetSkillTemplet(this.m_CurOperator.mainSkill.id);
						this.m_lstSkill[i].m_SkillInfo.SetData(this.m_CurOperator.mainSkill.id, (int)this.m_CurOperator.mainSkill.level, false);
					}
					else
					{
						level = (int)this.m_CurOperator.subSkill.level;
						skillTemplet = NKCOperatorUtil.GetSkillTemplet(this.m_CurOperator.subSkill.id);
						this.m_lstSkill[i].m_SkillInfo.SetData(this.m_CurOperator.subSkill.id, (int)this.m_CurOperator.subSkill.level, false);
					}
					if (skillTemplet != null && skillTemplet.m_MaxSkillLevel == level)
					{
						NKCUtil.SetGameobjectActive(this.m_lstSkill[i].m_Max, true);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_lstSkill[i].m_Max, false);
					}
				}
			}
			if (this.m_NKM_UI_OPERATOR_CARD_LEVEL_GAUGE != null)
			{
				this.m_NKM_UI_OPERATOR_CARD_LEVEL_GAUGE.fillAmount = 0f;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATOR_CARD_LEVEL_BG, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_DISABLE, false);
			NKCUtil.SetGameobjectActive(this.m_objBan, false);
		}

		// Token: 0x060071B6 RID: 29110 RVA: 0x0025CF43 File Offset: 0x0025B143
		public override void SetDataForRearm(NKMUnitData unitData, NKMDeckIndex deckIndex, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot, bool bShowEqup = true, bool bShowLevel = false, bool bUnable = false)
		{
		}

		// Token: 0x060071B7 RID: 29111 RVA: 0x0025CF48 File Offset: 0x0025B148
		public override void SetDataForBan(NKMOperator operData, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot onSelectThisSlot)
		{
			this.SetData(operData, NKMDeckIndex.None, bEnableLayoutElement, onSelectThisSlot);
			NKCUtil.SetGameobjectActive(this.m_objContractGainUnit, false);
			this.ProcessBanUI();
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATOR_CARD_SKILL, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_LEVEL, false);
			this.SetCityLeaderMark(false);
			this.SetFierceBattleOtherBossAlreadyUsed(false);
		}

		// Token: 0x060071B8 RID: 29112 RVA: 0x0025CF9C File Offset: 0x0025B19C
		private void ProcessBanUI()
		{
			if (this.m_NKMUnitTempletBase != null)
			{
				if (this.m_bEnableShowBan && NKCBanManager.IsBanOperator(this.m_NKMUnitTempletBase.m_UnitID, this.m_eBanDataType))
				{
					NKCUtil.SetGameobjectActive(this.m_objBan, true);
					int operBanLevel = NKCBanManager.GetOperBanLevel(this.m_NKMUnitTempletBase.m_UnitID, this.m_eBanDataType);
					NKCUtil.SetLabelText(this.m_lbBanLevel, string.Format(NKCUtilString.GET_STRING_GAUNTLET_BAN_LEVEL_ONE_PARAM, operBanLevel));
					return;
				}
				NKCUtil.SetGameobjectActive(this.m_objBan, false);
			}
		}

		// Token: 0x060071B9 RID: 29113 RVA: 0x0025D01C File Offset: 0x0025B21C
		protected override void OnClick()
		{
			if (this.dOnSelectThisOperatorSlot != null)
			{
				this.dOnSelectThisOperatorSlot(this.m_OperatorData, this.m_NKMUnitTempletBase, this.m_DeckIndex, this.m_eUnitSlotState, this.m_eUnitSelectState);
			}
		}

		// Token: 0x060071BA RID: 29114 RVA: 0x0025D04F File Offset: 0x0025B24F
		public override void SetLock(bool bLocked, bool bBig = false)
		{
			base.SetLock(bLocked, bBig);
			this.SetPassiveSkillInfo(bLocked && bBig);
		}

		// Token: 0x060071BB RID: 29115 RVA: 0x0025D062 File Offset: 0x0025B262
		public override void SetDelete(bool bSet)
		{
			base.SetDelete(bSet);
			this.SetPassiveSkillInfo(bSet);
		}

		// Token: 0x060071BC RID: 29116 RVA: 0x0025D072 File Offset: 0x0025B272
		public override void SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState eUnitSelectState)
		{
			base.SetSlotSelectState(eUnitSelectState);
			this.SetPassiveSkillInfo(eUnitSelectState == NKCUIUnitSelectList.eUnitSlotSelectState.DELETE);
		}

		// Token: 0x060071BD RID: 29117 RVA: 0x0025D088 File Offset: 0x0025B288
		private void SetPassiveSkillInfo(bool bActive)
		{
			if (bActive && this.m_CurOperator != null)
			{
				NKMOperatorSkillTemplet skillTemplet = NKCOperatorUtil.GetSkillTemplet(this.m_CurOperator.subSkill.id);
				if (skillTemplet != null)
				{
					NKCUtil.SetImageSprite(this.m_ImgPassiveSkill, NKCUtil.GetSkillIconSprite(skillTemplet), false);
					NKCUtil.SetLabelText(this.m_txtPassiveSkillName, NKCStringTable.GetString(skillTemplet.m_OperSkillNameStrID, false));
				}
			}
			NKCUtil.SetGameobjectActive(this.m_ObjPassiveSkill, bActive);
		}

		// Token: 0x060071BE RID: 29118 RVA: 0x0025D0EE File Offset: 0x0025B2EE
		public override void SetContractedUnitMark(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_objContractGainUnit, value);
		}

		// Token: 0x04005D9F RID: 23967
		public NKCAssetInstanceData m_Instance;

		// Token: 0x04005DA0 RID: 23968
		[Header("오퍼레이터")]
		public NKCUIComButton m_NKM_UI_OPERATOR_CARD_SLOT;

		// Token: 0x04005DA1 RID: 23969
		public GameObject m_NKM_UI_OPERATOR_CARD_LEVEL_BG;

		// Token: 0x04005DA2 RID: 23970
		public Image m_NKM_UI_OPERATOR_CARD_LEVEL_GAUGE;

		// Token: 0x04005DA3 RID: 23971
		public GameObject m_objContractGainUnit;

		// Token: 0x04005DA4 RID: 23972
		public GameObject m_NKM_UI_OPERATOR_CARD_SKILL;

		// Token: 0x04005DA5 RID: 23973
		public List<NKCUIOperatorSelectListSlot.SkillInfo> m_lstSkill;

		// Token: 0x04005DA6 RID: 23974
		public Text m_NKM_UI_OPERATOR_CARD_TITLE_TEXT;

		// Token: 0x04005DA7 RID: 23975
		[Header("격전 지원")]
		public Text m_NKM_UI_UNIT_SELECT_LIST_FIERCE_BATTLE_TEXT;

		// Token: 0x04005DA8 RID: 23976
		[Header("보조 스킬")]
		public GameObject m_ObjPassiveSkill;

		// Token: 0x04005DA9 RID: 23977
		public Image m_ImgPassiveSkill;

		// Token: 0x04005DAA RID: 23978
		public Text m_txtPassiveSkillName;

		// Token: 0x04005DAB RID: 23979
		[Header("도감")]
		public GameObject m_NKM_UI_OPERATOR_CARD_SLOT_COLLECTION_EMPLOYEE;

		// Token: 0x04005DAC RID: 23980
		public Text m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_EMPLOYEE_TEXT;

		// Token: 0x04005DAD RID: 23981
		public GameObject m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_DISABLE;

		// Token: 0x04005DAE RID: 23982
		private NKMOperator m_CurOperator;

		// Token: 0x02001763 RID: 5987
		[Serializable]
		public struct SkillInfo
		{
			// Token: 0x0400A6B0 RID: 42672
			public GameObject m_Object;

			// Token: 0x0400A6B1 RID: 42673
			public GameObject m_Max;

			// Token: 0x0400A6B2 RID: 42674
			public NKCUIOperatorSkill m_SkillInfo;
		}
	}
}
