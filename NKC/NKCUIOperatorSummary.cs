using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007EC RID: 2028
	public class NKCUIOperatorSummary : MonoBehaviour
	{
		// Token: 0x0600507D RID: 20605 RVA: 0x00185944 File Offset: 0x00183B44
		public void SetData(NKMOperator opeatorData)
		{
			if (opeatorData == null)
			{
				return;
			}
			if (!NKCOperatorUtil.IsOperatorUnit(opeatorData.id))
			{
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(opeatorData.id);
			if (unitTempletBase != null)
			{
				NKCUtil.SetLabelText(this.m_NKM_UI_UNIT_INFO_DESC_SUMMARY_TITLE, unitTempletBase.GetUnitTitle());
				NKCUtil.SetLabelText(this.m_NKM_UI_UNIT_INFO_DESC_SUMMARY_NAME, unitTempletBase.GetUnitName() ?? "");
				NKCUtil.SetImageSprite(this.m_Rarity_icon, NKCUtil.GetSpriteUnitGrade(unitTempletBase.m_NKM_UNIT_GRADE), false);
				NKCUtil.SetLabelText(this.m_NKM_UI_UNIT_INFO_DESC_SUMMARY_LEVEL_TEXT, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, opeatorData.level));
				NKCUtil.SetLabelText(this.m_NKM_UI_UNIT_INFO_DESC_SUMMARY_LEVEL_MAX_TEXT, "/" + NKMCommonConst.OperatorConstTemplet.unitMaximumLevel.ToString());
				this.ShowLevelExpGauge(true);
				int num = opeatorData.exp;
				if (this.m_NKM_UI_UNIT_INFO_DESC_SUMMARY_LEVEL_GAUGE != null)
				{
					if (NKMCommonConst.OperatorConstTemplet.unitMaximumLevel == opeatorData.level)
					{
						num = 0;
						this.m_NKM_UI_UNIT_INFO_DESC_SUMMARY_LEVEL_GAUGE.fillAmount = 1f;
					}
					else
					{
						this.m_NKM_UI_UNIT_INFO_DESC_SUMMARY_LEVEL_GAUGE.fillAmount = NKCExpManager.GetOperatorNextLevelExpProgress(NKCOperatorUtil.GetOperatorData(opeatorData.uid));
					}
				}
				NKCUtil.SetLabelText(this.m_NKM_UI_UNIT_INFO_DESC_SUMMARY_EXP, string.Format("{0}/{1}", num, NKCOperatorUtil.GetRequiredExp(opeatorData)));
			}
		}

		// Token: 0x0600507E RID: 20606 RVA: 0x00185A7E File Offset: 0x00183C7E
		public void ShowLevelExpGauge(bool bShow)
		{
			if (this.m_NKM_UI_UNIT_INFO_DESC_SUMMARY_LEVEL_GAUGE != null)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_INFO_DESC_SUMMARY_LEVEL_GAUGE.transform.parent, bShow);
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_UNIT_INFO_DESC_SUMMARY_EXP, bShow);
		}

		// Token: 0x0400406E RID: 16494
		public Text m_NKM_UI_UNIT_INFO_DESC_SUMMARY_TITLE;

		// Token: 0x0400406F RID: 16495
		public Text m_NKM_UI_UNIT_INFO_DESC_SUMMARY_NAME;

		// Token: 0x04004070 RID: 16496
		public Image m_Rarity_icon;

		// Token: 0x04004071 RID: 16497
		public Image m_NKM_UI_UNIT_INFO_DESC_SUMMARY_LEVEL_GAUGE;

		// Token: 0x04004072 RID: 16498
		public Text m_NKM_UI_UNIT_INFO_DESC_SUMMARY_EXP;

		// Token: 0x04004073 RID: 16499
		public Text m_NKM_UI_UNIT_INFO_DESC_SUMMARY_LEVEL_TEXT;

		// Token: 0x04004074 RID: 16500
		public Text m_NKM_UI_UNIT_INFO_DESC_SUMMARY_LEVEL_MAX_TEXT;

		// Token: 0x04004075 RID: 16501
		public Image m_OPERATOR;
	}
}
