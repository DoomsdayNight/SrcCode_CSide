using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Tooltip
{
	// Token: 0x02000B0A RID: 2826
	public class NKCUITooltipSkillUnit : NKCUITooltipBase
	{
		// Token: 0x0600805E RID: 32862 RVA: 0x002B49AE File Offset: 0x002B2BAE
		public override void Init()
		{
			this.m_slot.Init(null);
		}

		// Token: 0x0600805F RID: 32863 RVA: 0x002B49BC File Offset: 0x002B2BBC
		public override void SetData(NKCUITooltip.Data data)
		{
			NKCUITooltip.UnitSkillData unitSkillData = data as NKCUITooltip.UnitSkillData;
			if (unitSkillData == null)
			{
				Debug.LogError("Tooltip UnitSkillData is null");
				return;
			}
			NKMUnitSkillTemplet unitSkillTemplet = unitSkillData.UnitSkillTemplet;
			bool bIsHyper = unitSkillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_HYPER;
			this.m_slot.SetData(unitSkillTemplet, bIsHyper);
			this.m_type.text = NKCUtilString.GetSkillTypeName(unitSkillTemplet.m_NKM_SKILL_TYPE);
			this.m_type.color = NKCUtil.GetSkillTypeColor(unitSkillTemplet.m_NKM_SKILL_TYPE);
			this.m_name.text = unitSkillTemplet.GetSkillName();
			bool flag = unitSkillTemplet.m_fCooltimeSecond > 0f;
			NKCUtil.SetGameobjectActive(this.m_objCooltime, flag);
			if (flag)
			{
				NKCUtil.SetLabelText(this.m_cooltime, string.Format(NKCUtilString.GET_STRING_TIME_SECOND_ONE_PARAM, unitSkillTemplet.m_fCooltimeSecond));
			}
			bool flag2 = unitSkillTemplet.m_AttackCount > 0;
			NKCUtil.SetGameobjectActive(this.m_objAttackCount, flag2);
			if (flag2)
			{
				NKCUtil.SetLabelText(this.m_attackCount, string.Format(NKCUtilString.GET_STRING_SKILL_ATTACK_COUNT_ONE_PARAM, unitSkillTemplet.m_AttackCount));
			}
			bool flag3 = NKMUnitSkillManager.IsLockedSkill(unitSkillTemplet.m_ID, unitSkillData.UnitLimitBreakLevel);
			NKCUtil.SetGameobjectActive(this.m_objCondUnlock, flag3);
			if (flag3)
			{
				NKCUtil.SetSkillUnlockStarRank(this.m_listCondUnlock, unitSkillTemplet, unitSkillData.UnitStarGradeMax);
			}
		}

		// Token: 0x04006C9A RID: 27802
		public NKCUISkillSlot m_slot;

		// Token: 0x04006C9B RID: 27803
		public Text m_type;

		// Token: 0x04006C9C RID: 27804
		public Text m_name;

		// Token: 0x04006C9D RID: 27805
		public GameObject m_objCooltime;

		// Token: 0x04006C9E RID: 27806
		public Text m_cooltime;

		// Token: 0x04006C9F RID: 27807
		public GameObject m_objAttackCount;

		// Token: 0x04006CA0 RID: 27808
		public Text m_attackCount;

		// Token: 0x04006CA1 RID: 27809
		public GameObject m_objCondUnlock;

		// Token: 0x04006CA2 RID: 27810
		public List<GameObject> m_listCondUnlock;
	}
}
