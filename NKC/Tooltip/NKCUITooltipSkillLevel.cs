using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;

namespace NKC.UI.Tooltip
{
	// Token: 0x02000B09 RID: 2825
	public class NKCUITooltipSkillLevel : NKCUITooltipBase
	{
		// Token: 0x0600805B RID: 32859 RVA: 0x002B4909 File Offset: 0x002B2B09
		public override void Init()
		{
		}

		// Token: 0x0600805C RID: 32860 RVA: 0x002B490C File Offset: 0x002B2B0C
		public override void SetData(NKCUITooltip.Data data)
		{
			NKCUITooltip.SkillLevelData skillLevelData = data as NKCUITooltip.SkillLevelData;
			if (skillLevelData == null)
			{
				Debug.LogError("Tooltip SkillLevelData is null");
				return;
			}
			NKMUnitSkillTemplet skillTemplet = skillLevelData.SkillTemplet;
			NKMUnitSkillTempletContainer skillTempletContainer = NKMUnitSkillManager.GetSkillTempletContainer(skillTemplet.m_ID);
			for (int i = 0; i < this.m_skillLevelDetail.Count; i++)
			{
				NKCUIComSkillLevelDetail nkcuicomSkillLevelDetail = this.m_skillLevelDetail[i];
				if (skillTempletContainer.GetSkillTemplet(nkcuicomSkillLevelDetail.m_iLevel) != null)
				{
					NKCUtil.SetGameobjectActive(nkcuicomSkillLevelDetail, true);
					nkcuicomSkillLevelDetail.SetData(skillTemplet.m_ID, nkcuicomSkillLevelDetail.m_iLevel <= skillTemplet.m_Level, -1);
				}
				else
				{
					NKCUtil.SetGameobjectActive(nkcuicomSkillLevelDetail, false);
				}
			}
		}

		// Token: 0x04006C99 RID: 27801
		public List<NKCUIComSkillLevelDetail> m_skillLevelDetail;
	}
}
