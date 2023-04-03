using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000801 RID: 2049
	public class NKCUIPopupRearmamentConfirmSkillInfo : MonoBehaviour
	{
		// Token: 0x06005127 RID: 20775 RVA: 0x00189D10 File Offset: 0x00187F10
		public void SetData(NKMUnitSkillTemplet skillTemplet)
		{
			if (skillTemplet == null)
			{
				return;
			}
			NKCUtil.SetImageSprite(this.m_SkillIcon, NKCUtil.GetSkillIconSprite(skillTemplet), false);
			NKCUtil.SetLabelText(this.m_SkillType, NKCUtilString.GetSkillTypeName(skillTemplet.m_NKM_SKILL_TYPE));
			NKCUtil.SetLabelTextColor(this.m_SkillType, NKCUtil.GetSkillTypeColor(skillTemplet.m_NKM_SKILL_TYPE));
			NKCUtil.SetLabelText(this.m_SkillName, skillTemplet.GetSkillName());
			NKCUtil.SetLabelText(this.m_SkillLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, skillTemplet.m_Level));
			NKCUtil.SetLabelText(this.m_SkillDesc, skillTemplet.GetSkillDesc());
		}

		// Token: 0x04004184 RID: 16772
		public Image m_SkillIcon;

		// Token: 0x04004185 RID: 16773
		public Text m_SkillType;

		// Token: 0x04004186 RID: 16774
		public Text m_SkillName;

		// Token: 0x04004187 RID: 16775
		public Text m_SkillLevel;

		// Token: 0x04004188 RID: 16776
		public Text m_SkillDesc;
	}
}
