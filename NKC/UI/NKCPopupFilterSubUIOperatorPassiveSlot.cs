using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A55 RID: 2645
	public class NKCPopupFilterSubUIOperatorPassiveSlot : MonoBehaviour
	{
		// Token: 0x06007415 RID: 29717 RVA: 0x00269B8B File Offset: 0x00267D8B
		public NKCUIComStateButton GetButton()
		{
			return this.m_btn;
		}

		// Token: 0x06007416 RID: 29718 RVA: 0x00269B93 File Offset: 0x00267D93
		public NKMOperatorSkillTemplet GetPassiveTemplet()
		{
			return this.m_PassiveSkillTemplet;
		}

		// Token: 0x06007417 RID: 29719 RVA: 0x00269B9C File Offset: 0x00267D9C
		public void SetData(NKMOperatorSkillTemplet skillTemplet, bool bSelected = false)
		{
			this.m_PassiveSkillTemplet = skillTemplet;
			this.m_btn.Select(bSelected, true, true);
			if (skillTemplet == null)
			{
				NKCUtil.SetGameobjectActive(this.m_imgIconOff, false);
				NKCUtil.SetLabelText(this.m_lbNameOff, NKCStringTable.GetString("SI_PF_SORT_OPR_PASSIVE_SKILL_OPTION", false));
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_imgIconOff, true);
			NKCUtil.SetImageSprite(this.m_imgIconOn, NKCUtil.GetSkillIconSprite(skillTemplet), false);
			NKCUtil.SetImageSprite(this.m_imgIconOff, NKCUtil.GetSkillIconSprite(skillTemplet), false);
			NKCUtil.SetLabelText(this.m_lbNameOn, NKCStringTable.GetString(skillTemplet.m_OperSkillNameStrID, false));
			NKCUtil.SetLabelText(this.m_lbNameOff, NKCStringTable.GetString(skillTemplet.m_OperSkillNameStrID, false));
		}

		// Token: 0x06007418 RID: 29720 RVA: 0x00269C44 File Offset: 0x00267E44
		public void SetData(int skillID, bool bSelected = false)
		{
			if (skillID > 0)
			{
				NKMOperatorSkillTemplet skillTemplet = NKCOperatorUtil.GetSkillTemplet(skillID);
				this.SetData(skillTemplet, bSelected);
				return;
			}
			this.SetData(null, false);
		}

		// Token: 0x0400602A RID: 24618
		public NKCUIComStateButton m_btn;

		// Token: 0x0400602B RID: 24619
		public Image m_imgIconOff;

		// Token: 0x0400602C RID: 24620
		public Text m_lbNameOff;

		// Token: 0x0400602D RID: 24621
		public Image m_imgIconOn;

		// Token: 0x0400602E RID: 24622
		public Text m_lbNameOn;

		// Token: 0x0400602F RID: 24623
		private NKMOperatorSkillTemplet m_PassiveSkillTemplet;
	}
}
