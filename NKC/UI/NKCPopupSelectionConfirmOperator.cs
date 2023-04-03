using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A81 RID: 2689
	public class NKCPopupSelectionConfirmOperator : MonoBehaviour
	{
		// Token: 0x0600770A RID: 30474 RVA: 0x00279854 File Offset: 0x00277A54
		public void SetData(int unitID)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
			if (unitTempletBase == null)
			{
				return;
			}
			this.m_slot.SetOperatorData(unitTempletBase, 1, false, null);
			this.m_slot.SetSlotState(NKCUnitSortSystem.eUnitState.NONE);
			this.SetOperatorStat(unitID);
			this.UpdateSkillInfo(unitID);
		}

		// Token: 0x0600770B RID: 30475 RVA: 0x00279898 File Offset: 0x00277A98
		private void SetOperatorStat(int unitID)
		{
			NKCUtil.SetLabelText(this.m_lbAtt, NKCOperatorUtil.GetStatPercentageString(unitID, 1, NKM_STAT_TYPE.NST_ATK) ?? "");
			NKCUtil.SetLabelText(this.m_lbDef, NKCOperatorUtil.GetStatPercentageString(unitID, 1, NKM_STAT_TYPE.NST_DEF) ?? "");
			NKCUtil.SetLabelText(this.m_lbHP, NKCOperatorUtil.GetStatPercentageString(unitID, 1, NKM_STAT_TYPE.NST_HP) ?? "");
			NKCUtil.SetLabelText(this.m_lbSkillCoolReduce, NKCOperatorUtil.GetStatPercentageString(unitID, 1, NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE) ?? "");
		}

		// Token: 0x0600770C RID: 30476 RVA: 0x00279918 File Offset: 0x00277B18
		private void UpdateSkillInfo(int unitID)
		{
			NKMOperatorSkillTemplet mainSkill = NKCOperatorUtil.GetMainSkill(unitID);
			this.m_skillMain.SetData(mainSkill, 1, false);
		}

		// Token: 0x04006389 RID: 25481
		public NKCUIOperatorSelectListSlot m_slot;

		// Token: 0x0400638A RID: 25482
		[Header("스탯")]
		public Text m_lbHP;

		// Token: 0x0400638B RID: 25483
		public Text m_lbAtt;

		// Token: 0x0400638C RID: 25484
		public Text m_lbDef;

		// Token: 0x0400638D RID: 25485
		public Text m_lbSkillCoolReduce;

		// Token: 0x0400638E RID: 25486
		[Header("스킬")]
		public NKCUIOperatorSkill m_skillMain;
	}
}
