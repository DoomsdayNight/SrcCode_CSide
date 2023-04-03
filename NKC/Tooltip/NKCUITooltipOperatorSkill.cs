using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Tooltip
{
	// Token: 0x02000B0E RID: 2830
	public class NKCUITooltipOperatorSkill : NKCUITooltipBase
	{
		// Token: 0x06008069 RID: 32873 RVA: 0x002B4C26 File Offset: 0x002B2E26
		public override void Init()
		{
		}

		// Token: 0x0600806A RID: 32874 RVA: 0x002B4C28 File Offset: 0x002B2E28
		public override void SetData(NKCUITooltip.Data data)
		{
			NKCUITooltip.OperatorSkillData operatorSkillData = data as NKCUITooltip.OperatorSkillData;
			if (operatorSkillData != null)
			{
				NKCUtil.SetImageSprite(this.SkillIcon, NKCUtil.GetSkillIconSprite(operatorSkillData.skillTemplet), false);
				NKCUtil.SetLabelText(this.m_SkillLv, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, operatorSkillData.skillLevel));
				NKCUtil.SetLabelText(this.m_SkillName, NKCStringTable.GetString(operatorSkillData.skillTemplet.m_OperSkillNameStrID, false));
				string msg;
				if (operatorSkillData.skillTemplet.m_OperSkillType == OperatorSkillType.m_Tactical)
				{
					msg = NKCUtilString.GET_STRING_OPERATOR_TOOLTIP_ACTIVE_SKILL_TITLE;
					NKMTacticalCommandTemplet tacticalCommandTempletByID = NKMTacticalCommandManager.GetTacticalCommandTempletByID(operatorSkillData.skillTemplet.m_OperSkillID);
					if (tacticalCommandTempletByID != null)
					{
						NKCUtil.SetLabelText(this.m_SkillCoolTime, string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_SECONDS", false), (int)tacticalCommandTempletByID.m_fCoolTime));
					}
					NKCUtil.SetGameobjectActive(this.m_SkillCoolTime, tacticalCommandTempletByID != null);
				}
				else
				{
					msg = NKCUtilString.GET_STRING_OPERATOR_TOOLTIP_PASSIVE_SKILL_TITLE;
				}
				NKCUtil.SetGameobjectActive(this.m_SkillCool, operatorSkillData.skillTemplet.m_OperSkillType == OperatorSkillType.m_Tactical);
				NKCUtil.SetLabelText(this.m_SkillType, msg);
			}
		}

		// Token: 0x04006CAA RID: 27818
		public Image SkillIcon;

		// Token: 0x04006CAB RID: 27819
		public Text m_SkillLv;

		// Token: 0x04006CAC RID: 27820
		public Text m_SkillName;

		// Token: 0x04006CAD RID: 27821
		public Text m_SkillType;

		// Token: 0x04006CAE RID: 27822
		public GameObject m_SkillCool;

		// Token: 0x04006CAF RID: 27823
		public Text m_SkillCoolTime;
	}
}
