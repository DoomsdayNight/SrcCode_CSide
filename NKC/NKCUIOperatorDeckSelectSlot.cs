using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A1B RID: 2587
	public class NKCUIOperatorDeckSelectSlot : NKCUIUnitSelectListSlotBase
	{
		// Token: 0x060070EE RID: 28910 RVA: 0x002579B4 File Offset: 0x00255BB4
		public static NKCUIOperatorDeckSelectSlot GetNewInstance(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("ab_ui_nkm_ui_operator_deck", "NKM_UI_OPERATOR_DECK_SELECT_SLOT", false, null);
			NKCUIOperatorDeckSelectSlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIOperatorDeckSelectSlot>();
			if (component == null)
			{
				Debug.LogError("NKCUIOperatorDeckSlot Prefab null!");
				return null;
			}
			component.m_InstanceData = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
				component.GetComponent<RectTransform>().localScale = Vector3.one;
				component.Init(false);
			}
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x060070EF RID: 28911 RVA: 0x00257A34 File Offset: 0x00255C34
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x170012D3 RID: 4819
		// (get) Token: 0x060070F0 RID: 28912 RVA: 0x00257A53 File Offset: 0x00255C53
		protected override NKCResourceUtility.eUnitResourceType UseResourceType
		{
			get
			{
				return NKCResourceUtility.eUnitResourceType.INVEN_ICON;
			}
		}

		// Token: 0x060070F1 RID: 28913 RVA: 0x00257A58 File Offset: 0x00255C58
		public override void SetData(NKMUnitData cNKMUnitData, NKMDeckIndex deckIndex, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot)
		{
			base.SetData(cNKMUnitData, deckIndex, bEnableLayoutElement, onSelectThisSlot);
			if (cNKMUnitData != null)
			{
				NKMOperator operatorData = NKCOperatorUtil.GetOperatorData(cNKMUnitData.m_UnitUID);
				if (operatorData != null)
				{
					this.UpdateSkillEnhanceUI(operatorData, false, false, false);
				}
				this.UpdateBackgroundGradationByGrade(cNKMUnitData.m_UnitID);
			}
		}

		// Token: 0x060070F2 RID: 28914 RVA: 0x00257A98 File Offset: 0x00255C98
		public override void SetEmpty(bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot, NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot onSelectThisOperatorSlot = null)
		{
			base.SetEmpty(bEnableLayoutElement, onSelectThisSlot, onSelectThisOperatorSlot);
			NKCUtil.SetGameobjectActive(this.m_objSkillCombo, false);
		}

		// Token: 0x060070F3 RID: 28915 RVA: 0x00257AB0 File Offset: 0x00255CB0
		public override void SetData(NKMOperator operatorData, NKMDeckIndex deckIndex, bool bEnableLayoutElement, NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot onSelectThisSlot)
		{
			base.SetData(operatorData, deckIndex, bEnableLayoutElement, onSelectThisSlot);
			base.ProcessBanUIForOperator();
			if (operatorData != null)
			{
				this.UpdateSkillEnhanceUI(operatorData, false, false, false);
				this.UpdateBackgroundGradationByGrade(operatorData.id);
				this.m_TacticalSkillCombo.SetData(operatorData.id);
			}
			NKCUtil.SetGameobjectActive(this.m_objSkillCombo, operatorData != null);
		}

		// Token: 0x060070F4 RID: 28916 RVA: 0x00257B07 File Offset: 0x00255D07
		protected override void OnClick()
		{
			if (this.dOnSelectThisOperatorSlot != null)
			{
				this.dOnSelectThisOperatorSlot(this.m_OperatorData, this.m_NKMUnitTempletBase, this.m_DeckIndex, this.m_eUnitSlotState, this.m_eUnitSelectState);
			}
		}

		// Token: 0x060070F5 RID: 28917 RVA: 0x00257B3A File Offset: 0x00255D3A
		public override void SetSlotState(NKCUnitSortSystem.eUnitState eUnitSlotState)
		{
			base.SetSlotState(eUnitSlotState);
			if (eUnitSlotState == NKCUnitSortSystem.eUnitState.DUPLICATE)
			{
				NKCUtil.SetGameobjectActive(this.m_objInCityMission, true);
				NKCUtil.SetLabelText(this.m_lbMissionStatus, NKCUtilString.GET_STRING_DECK_UNIT_STATE_DUPLICATE);
			}
		}

		// Token: 0x170012D4 RID: 4820
		// (get) Token: 0x060070F6 RID: 28918 RVA: 0x00257B63 File Offset: 0x00255D63
		public long OperatorUID
		{
			get
			{
				return this.m_curOperatorUID;
			}
		}

		// Token: 0x060070F7 RID: 28919 RVA: 0x00257B6C File Offset: 0x00255D6C
		public void SetData(NKMOperator mainOperator, NKMOperator curOperatorData, NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot onSelectThisSlot)
		{
			this.m_curOperatorUID = 0L;
			base.SetData(curOperatorData, NKMDeckIndex.None, true, onSelectThisSlot);
			NKCUtil.SetGameobjectActive(this.m_objSkillCombo, false);
			if (curOperatorData != null)
			{
				this.m_curOperatorUID = curOperatorData.uid;
				NKMOperator operatorData = NKCOperatorUtil.GetOperatorData(this.m_curOperatorUID);
				if (operatorData != null)
				{
					bool bEnhanceMain = NKCOperatorUtil.IsCanEnhanceMainSkill(mainOperator, curOperatorData);
					bool flag = NKCOperatorUtil.IsCanEnhanceSubSkill(mainOperator, curOperatorData);
					bool bImplantSub = false;
					if (!flag)
					{
						NKMOperator operatorData2 = NKCOperatorUtil.GetOperatorData(mainOperator.uid);
						if (operatorData2 != null)
						{
							bImplantSub = (operatorData2.subSkill.id != operatorData.subSkill.id);
						}
					}
					this.UpdateSkillEnhanceUI(operatorData, bEnhanceMain, flag, bImplantSub);
				}
				this.UpdateBackgroundGradationByGrade(curOperatorData.id);
			}
		}

		// Token: 0x060070F8 RID: 28920 RVA: 0x00257C14 File Offset: 0x00255E14
		private void UpdateSkillEnhanceUI(NKMOperator operatorData, bool bEnhanceMain = false, bool bEnhanceSub = false, bool bImplantSub = false)
		{
			for (int i = 0; i < this.m_lstSkill.Count; i++)
			{
				bool bValue;
				if (i == 0)
				{
					int level = (int)operatorData.mainSkill.level;
					bValue = NKCOperatorUtil.IsMaximumSkillLevel(operatorData.mainSkill.id, level);
					NKCUtil.SetGameobjectActive(this.m_lstSkill[i].m_ENHANCE, bEnhanceMain);
					NKCUtil.SetGameobjectActive(this.m_lstSkill[i].m_IMPLANT, false);
					this.m_lstSkill[i].m_SkillInfo.SetData(operatorData.mainSkill.id, (int)operatorData.mainSkill.level, false);
				}
				else
				{
					int level = (int)operatorData.subSkill.level;
					bValue = NKCOperatorUtil.IsMaximumSkillLevel(operatorData.subSkill.id, level);
					NKCUtil.SetGameobjectActive(this.m_lstSkill[i].m_ENHANCE, bEnhanceSub);
					NKCUtil.SetGameobjectActive(this.m_lstSkill[i].m_IMPLANT, bImplantSub);
					this.m_lstSkill[i].m_SkillInfo.SetData(operatorData.subSkill.id, (int)operatorData.subSkill.level, false);
				}
				NKCUtil.SetGameobjectActive(this.m_lstSkill[i].m_Obj, true);
				NKCUtil.SetGameobjectActive(this.m_lstSkill[i].m_MAX, bValue);
			}
		}

		// Token: 0x060070F9 RID: 28921 RVA: 0x00257D68 File Offset: 0x00255F68
		private void UpdateBackgroundGradationByGrade(int unitID)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
			Color color = Color.clear;
			if (unitTempletBase != null)
			{
				if (unitTempletBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SSR)
				{
					color = NKCUtil.GetColor("#FFAE14");
				}
				else if (unitTempletBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_SR)
				{
					color = NKCUtil.GetColor("#C414FF");
				}
				else if (unitTempletBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_R)
				{
					color = NKCUtil.GetColor("#1366FF");
				}
				else if (unitTempletBase.m_NKM_UNIT_GRADE == NKM_UNIT_GRADE.NUG_N)
				{
					color = NKCUtil.GetColor("#767676");
				}
			}
			NKCUtil.SetImageColor(this.m_ImgBGGradation, color);
		}

		// Token: 0x04005CB9 RID: 23737
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x04005CBA RID: 23738
		public List<NKCUIOperatorDeckSelectSlot.Skill> m_lstSkill;

		// Token: 0x04005CBB RID: 23739
		public Image m_ImgBGGradation;

		// Token: 0x04005CBC RID: 23740
		public GameObject m_objSkillCombo;

		// Token: 0x04005CBD RID: 23741
		public NKCUIOperatorTacticalSkillCombo m_TacticalSkillCombo;

		// Token: 0x04005CBE RID: 23742
		private long m_curOperatorUID;

		// Token: 0x02001759 RID: 5977
		[Serializable]
		public struct Skill
		{
			// Token: 0x0400A68A RID: 42634
			public GameObject m_Obj;

			// Token: 0x0400A68B RID: 42635
			public GameObject m_MAX;

			// Token: 0x0400A68C RID: 42636
			public GameObject m_ENHANCE;

			// Token: 0x0400A68D RID: 42637
			public GameObject m_IMPLANT;

			// Token: 0x0400A68E RID: 42638
			public NKCUIOperatorSkill m_SkillInfo;
		}
	}
}
