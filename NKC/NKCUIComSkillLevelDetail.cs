using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x0200075F RID: 1887
	public class NKCUIComSkillLevelDetail : MonoBehaviour
	{
		// Token: 0x06004B60 RID: 19296 RVA: 0x00169034 File Offset: 0x00167234
		public void SetData(int skillID, bool bTrained, int iLevel = -1)
		{
			if (-1 != iLevel)
			{
				this.m_iLevel = iLevel;
			}
			NKMUnitSkillTemplet skillTemplet = NKMUnitSkillManager.GetSkillTemplet(skillID, this.m_iLevel);
			if (skillTemplet == null)
			{
				return;
			}
			NKCUtil.SetLabelText(this.m_lbLevel, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, skillTemplet.m_Level));
			NKCUtil.SetLabelText(this.m_lbSkillLevelDesc, skillTemplet.GetSkillDesc());
			NKCUtil.SetLabelTextColor(this.m_lbSkillLevelDesc, bTrained ? this.m_colTextTrained : this.m_colTextUnTrained);
			NKCUtil.SetImageColor(this.m_imgLevel, bTrained ? this.m_colTextTrained : this.m_colTextUnTrained);
			if (this.m_LayoutElement == null)
			{
				this.m_LayoutElement = base.GetComponent<LayoutElement>();
			}
			if (this.m_LayoutElement != null && this.m_lbSkillLevelDesc != null)
			{
				float num = this.CalculatePreferredHeight();
				this.m_LayoutElement.minHeight = num;
				this.m_LayoutElement.preferredHeight = num;
			}
		}

		// Token: 0x06004B61 RID: 19297 RVA: 0x0016911C File Offset: 0x0016731C
		private float CalculatePreferredHeight()
		{
			RectTransform component = this.m_imgLevel.GetComponent<RectTransform>();
			Vector2 extents = this.m_lbSkillLevelDesc.cachedTextGenerator.rectExtents.size * 0.5f;
			float preferredHeight = this.m_lbSkillLevelDesc.cachedTextGeneratorForLayout.GetPreferredHeight("1", this.m_lbSkillLevelDesc.GetGenerationSettings(extents));
			float height = component.GetHeight();
			float num = height - preferredHeight;
			if (num < 0f)
			{
				num = 0f;
			}
			return Mathf.Max(this.m_lbSkillLevelDesc.preferredHeight + num, height);
		}

		// Token: 0x040039F8 RID: 14840
		public int m_iLevel;

		// Token: 0x040039F9 RID: 14841
		public Image m_imgLevel;

		// Token: 0x040039FA RID: 14842
		public Text m_lbLevel;

		// Token: 0x040039FB RID: 14843
		public Text m_lbSkillLevelDesc;

		// Token: 0x040039FC RID: 14844
		public Color m_colTextTrained = Color.white;

		// Token: 0x040039FD RID: 14845
		public Color m_colTextUnTrained = new Color(0.4f, 0.4f, 0.4f, 1f);

		// Token: 0x040039FE RID: 14846
		public LayoutElement m_LayoutElement;
	}
}
