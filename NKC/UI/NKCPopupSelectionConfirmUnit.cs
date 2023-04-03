using System;
using System.Collections.Generic;
using NKC.UI.Tooltip;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A84 RID: 2692
	public class NKCPopupSelectionConfirmUnit : MonoBehaviour
	{
		// Token: 0x06007716 RID: 30486 RVA: 0x00279C60 File Offset: 0x00277E60
		public void SetData(int unitID)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
			if (unitTempletBase == null)
			{
				return;
			}
			this.m_slot.SetData(unitTempletBase, 1, false, null);
			this.m_slot.SetSlotState(NKCUnitSortSystem.eUnitState.NONE);
			if (this.m_imgBattleType != null)
			{
				this.m_imgBattleType.sprite = NKCUtil.GetMoveTypeImg(unitTempletBase.m_bAirUnit);
			}
			if (this.m_lbBattleType != null)
			{
				this.m_lbBattleType.text = NKCUtilString.GetMoveTypeText(unitTempletBase.m_bAirUnit);
			}
			if (this.m_imgAttackType != null)
			{
				if (unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc == NKM_FIND_TARGET_TYPE.NFTT_INVALID)
				{
					this.m_imgAttackType.sprite = NKCResourceUtility.GetOrLoadUnitAttackTypeIcon(unitTempletBase.m_NKM_FIND_TARGET_TYPE, false);
				}
				else
				{
					this.m_imgAttackType.sprite = NKCResourceUtility.GetOrLoadUnitAttackTypeIcon(unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc, false);
				}
			}
			if (this.m_lbAttackType != null)
			{
				if (unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc == NKM_FIND_TARGET_TYPE.NFTT_INVALID)
				{
					this.m_lbAttackType.text = NKCUtilString.GetAtkTypeText(unitTempletBase.m_NKM_FIND_TARGET_TYPE);
				}
				else
				{
					this.m_lbAttackType.text = NKCUtilString.GetAtkTypeText(unitTempletBase.m_NKM_FIND_TARGET_TYPE_Desc);
				}
			}
			this.SetUnitStat(unitID);
			int skillCount = unitTempletBase.GetSkillCount();
			for (int i = 0; i < this.m_lstSkillSlot.Count; i++)
			{
				if (i < skillCount)
				{
					NKCUtil.SetGameobjectActive(this.m_lstSkillSlot[i], true);
					string skillStrID = unitTempletBase.GetSkillStrID(i);
					NKMUnitSkillTemplet unitSkillTemplet = NKMUnitSkillManager.GetUnitSkillTemplet(skillStrID, 1);
					if (unitSkillTemplet != null)
					{
						this.m_lstSkillSlot[i].SetData(unitSkillTemplet, unitSkillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_HYPER);
						bool value = NKMUnitSkillManager.IsLockedSkill(unitSkillTemplet.m_ID, 0);
						this.m_lstSkillSlot[i].LockSkill(value);
						int unitStarGradeMax = unitTempletBase.m_StarGradeMax;
						NKCUIComButton component = this.m_lstSkillSlot[i].GetComponent<NKCUIComButton>();
						component.PointerDown.RemoveAllListeners();
						component.PointerDown.AddListener(delegate(PointerEventData e)
						{
							this.OnPointDownSkill(unitSkillTemplet, unitStarGradeMax, e);
						});
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_lstSkillSlot[i], false);
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstSkillSlot[i], false);
				}
			}
		}

		// Token: 0x06007717 RID: 30487 RVA: 0x00279E88 File Offset: 0x00278088
		private void SetUnitStat(int unitID)
		{
			NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitID);
			NKMUnitData unitData = new NKMUnitData(unitID, (long)unitID, false, false, false, false);
			NKMStatData nkmstatData = new NKMStatData();
			nkmstatData.Init();
			nkmstatData.MakeBaseStat(null, false, unitData, unitStatTemplet.m_StatData, false, 0, null);
			NKCUtil.SetLabelText(this.m_lbHP, ((int)nkmstatData.GetStatBase(NKM_STAT_TYPE.NST_HP)).ToString());
			NKCUtil.SetLabelText(this.m_lbAtt, ((int)nkmstatData.GetStatBase(NKM_STAT_TYPE.NST_ATK)).ToString());
			NKCUtil.SetLabelText(this.m_lbDef, ((int)nkmstatData.GetStatBase(NKM_STAT_TYPE.NST_DEF)).ToString());
			NKCUtil.SetLabelText(this.m_lbCrit, ((int)nkmstatData.GetStatBase(NKM_STAT_TYPE.NST_CRITICAL)).ToString());
			NKCUtil.SetLabelText(this.m_lbHit, ((int)nkmstatData.GetStatBase(NKM_STAT_TYPE.NST_HIT)).ToString());
			NKCUtil.SetLabelText(this.m_lbEvd, ((int)nkmstatData.GetStatBase(NKM_STAT_TYPE.NST_EVADE)).ToString());
		}

		// Token: 0x06007718 RID: 30488 RVA: 0x00279F69 File Offset: 0x00278169
		private void OnPointDownSkill(NKMUnitSkillTemplet unitSkillTemplet, int unitStarGradeMax, PointerEventData eventData)
		{
			if (unitSkillTemplet != null)
			{
				NKCUITooltip.Instance.Open(unitSkillTemplet, new Vector2?(eventData.position), unitStarGradeMax, 0);
			}
		}

		// Token: 0x0400639B RID: 25499
		public NKCUIUnitSelectListSlot m_slot;

		// Token: 0x0400639C RID: 25500
		[Header("타입")]
		public Image m_imgBattleType;

		// Token: 0x0400639D RID: 25501
		public Text m_lbBattleType;

		// Token: 0x0400639E RID: 25502
		public Image m_imgAttackType;

		// Token: 0x0400639F RID: 25503
		public Text m_lbAttackType;

		// Token: 0x040063A0 RID: 25504
		[Header("스탯")]
		public Text m_lbHP;

		// Token: 0x040063A1 RID: 25505
		public Text m_lbAtt;

		// Token: 0x040063A2 RID: 25506
		public Text m_lbDef;

		// Token: 0x040063A3 RID: 25507
		public Text m_lbCrit;

		// Token: 0x040063A4 RID: 25508
		public Text m_lbHit;

		// Token: 0x040063A5 RID: 25509
		public Text m_lbEvd;

		// Token: 0x040063A6 RID: 25510
		[Header("스킬")]
		public List<NKCUISkillSlot> m_lstSkillSlot = new List<NKCUISkillSlot>(4);
	}
}
