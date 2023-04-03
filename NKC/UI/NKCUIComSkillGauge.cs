using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000947 RID: 2375
	public class NKCUIComSkillGauge : MonoBehaviour
	{
		// Token: 0x06005ED1 RID: 24273 RVA: 0x001D710C File Offset: 0x001D530C
		public void SetSkillType(bool bIsFury = false)
		{
			if (bIsFury)
			{
				NKCUtil.SetImageColor(this.m_UNIT_GAGE_SKILL_BAR_Image, NKCUtil.GetColor("#FFB830"));
				NKCUtil.SetImageColor(this.m_UNIT_GAGE_HYPER_SKILL_BAR_Image, NKCUtil.GetColor("#FF7F1B"));
				return;
			}
			NKCUtil.SetImageColor(this.m_UNIT_GAGE_SKILL_BAR_Image, NKCUtil.GetColor("#008FFF"));
			NKCUtil.SetImageColor(this.m_UNIT_GAGE_HYPER_SKILL_BAR_Image, NKCUtil.GetColor("#9900FF"));
		}

		// Token: 0x06005ED2 RID: 24274 RVA: 0x001D7171 File Offset: 0x001D5371
		public GameObject GetSkillGauge()
		{
			return this.m_UNIT_SKILL_GAGE;
		}

		// Token: 0x06005ED3 RID: 24275 RVA: 0x001D7179 File Offset: 0x001D5379
		public void SetActiveSkillGauge(bool bActive)
		{
			NKCUtil.SetGameobjectActive(this.m_UNIT_SKILL_GAGE, bActive);
		}

		// Token: 0x06005ED4 RID: 24276 RVA: 0x001D7188 File Offset: 0x001D5388
		public void SetSkillCoolTime(float fSkillCollTimeRate)
		{
			if (base.gameObject.activeSelf)
			{
				if (this.m_UNIT_GAGE_SKILL_BAR_Image.fillAmount < 0.999f && fSkillCollTimeRate >= 0.999f)
				{
					this.m_UNIT_SKILL_GAGE_FX_Animator.Play("FULL", -1);
				}
				else if (this.m_UNIT_GAGE_SKILL_BAR_Image.fillAmount >= 0.999f && fSkillCollTimeRate < 0.999f)
				{
					this.m_UNIT_SKILL_GAGE_FX_Animator.Play("BASE", -1);
				}
			}
			this.m_UNIT_GAGE_SKILL_BAR_Image.fillAmount = fSkillCollTimeRate;
		}

		// Token: 0x06005ED5 RID: 24277 RVA: 0x001D7206 File Offset: 0x001D5406
		public GameObject GetHyperGauge()
		{
			return this.m_UNIT_HYPER_SKILL_GAGE;
		}

		// Token: 0x06005ED6 RID: 24278 RVA: 0x001D720E File Offset: 0x001D540E
		public void SetActiveHyperGauge(bool bActive)
		{
			NKCUtil.SetGameobjectActive(this.m_UNIT_HYPER_SKILL_GAGE, bActive);
		}

		// Token: 0x06005ED7 RID: 24279 RVA: 0x001D721C File Offset: 0x001D541C
		public void SetHyperCoolTime(float fHyperSkillCollTimeRate)
		{
			if (base.gameObject.activeSelf)
			{
				if (this.m_UNIT_GAGE_HYPER_SKILL_BAR_Image.fillAmount < 0.999f && fHyperSkillCollTimeRate >= 0.999f)
				{
					this.m_UNIT_HYPER_SKILL_GAGE_FX_Animator.Play("FULL", -1);
				}
				else if (this.m_UNIT_GAGE_HYPER_SKILL_BAR_Image.fillAmount >= 0.999f && fHyperSkillCollTimeRate < 0.999f)
				{
					this.m_UNIT_HYPER_SKILL_GAGE_FX_Animator.Play("BASE", -1);
				}
			}
			this.m_UNIT_GAGE_HYPER_SKILL_BAR_Image.fillAmount = fHyperSkillCollTimeRate;
		}

		// Token: 0x04004AEF RID: 19183
		private const string SKILL_COLOR_NORMAL = "#008FFF";

		// Token: 0x04004AF0 RID: 19184
		private const string SKILL_COLOR_FURY = "#FFB830";

		// Token: 0x04004AF1 RID: 19185
		private const string HYPER_COLOR_NORMAL = "#9900FF";

		// Token: 0x04004AF2 RID: 19186
		private const string HYPER_COLOR_FURY = "#FF7F1B";

		// Token: 0x04004AF3 RID: 19187
		public GameObject m_UNIT_SKILL_GAGE;

		// Token: 0x04004AF4 RID: 19188
		public Image m_UNIT_GAGE_SKILL_BAR_Image;

		// Token: 0x04004AF5 RID: 19189
		public Animator m_UNIT_SKILL_GAGE_FX_Animator;

		// Token: 0x04004AF6 RID: 19190
		public GameObject m_UNIT_HYPER_SKILL_GAGE;

		// Token: 0x04004AF7 RID: 19191
		public Image m_UNIT_GAGE_HYPER_SKILL_BAR_Image;

		// Token: 0x04004AF8 RID: 19192
		public Animator m_UNIT_HYPER_SKILL_GAGE_FX_Animator;
	}
}
