using System;
using System.Collections;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Result
{
	// Token: 0x02000BAB RID: 2987
	public class NKCUIResultSubUIWorldmapBranchExp : NKCUIResultSubUIBase
	{
		// Token: 0x060089F8 RID: 35320 RVA: 0x002EC320 File Offset: 0x002EA520
		private void Init()
		{
			if (this.m_bInit)
			{
				return;
			}
			Transform transform = base.transform;
			this.m_lbLevel = transform.Find("Branch_EXP/LEVEL/NKM_UI_UNIT_INFO_DESC_SUMMARY_LEVEL_TEXT").GetComponent<Text>();
			this.m_lbCityName = transform.Find("Branch_EXP/Branch_NAME").GetComponent<Text>();
			this.m_lbExp = transform.Find("Branch_EXP/Branch_EXP").GetComponent<Text>();
			this.m_imgExpGauge = transform.Find("Branch_EXP/LEVEL/NKM_UI_UNIT_INFO_DESC_SUMMARY_LEVEL_SLIDER/NKM_UI_UNIT_INFO_DESC_SUMMARY_LEVEL_GAUGE").GetComponent<Image>();
			this.m_bInit = true;
		}

		// Token: 0x060089F9 RID: 35321 RVA: 0x002EC39C File Offset: 0x002EA59C
		public void SetData(int cityID, int cityOldLevel, int cityNewLevel, int cityOldExp, int cityNewExp, bool bIgnoreAutoClose = false)
		{
			this.Init();
			NKMWorldMapCityTemplet cityTemplet = NKMWorldMapManager.GetCityTemplet(cityID);
			if (cityTemplet == null)
			{
				base.ProcessRequired = false;
				return;
			}
			base.ProcessRequired = true;
			this.m_bIgnoreAutoClose = bIgnoreAutoClose;
			NKMWorldMapCityExpTemplet cityExpTable = NKMWorldMapManager.GetCityExpTable(cityOldLevel);
			if (cityExpTable == null)
			{
				base.ProcessRequired = false;
				return;
			}
			this.m_lbCityName.text = cityTemplet.GetName();
			this.m_lbLevel.text = cityOldLevel.ToString();
			this.m_oldLevel = cityOldLevel;
			this.m_newLevel = cityNewLevel;
			this.m_oldExp = cityOldExp;
			this.m_newExp = cityNewExp;
			if (cityOldLevel == cityNewLevel)
			{
				this.m_lbExp.text = string.Format("+{0}", (cityNewExp - cityOldExp).ToString());
			}
			else
			{
				this.m_lbExp.text = string.Format("+{0}", (cityExpTable.m_ExpRequired - cityOldExp + cityNewExp).ToString());
			}
			if (cityExpTable.m_ExpRequired == 0)
			{
				this.m_fExpBeginRatio = 1f;
				this.m_fExpTargetRatio = 1f;
			}
			else
			{
				NKMWorldMapCityExpTemplet cityExpTable2 = NKMWorldMapManager.GetCityExpTable(cityNewLevel);
				if (cityExpTable2 != null)
				{
					this.m_fExpBeginRatio = (float)this.m_oldExp / (float)cityExpTable.m_ExpRequired;
					this.m_fExpTargetRatio = (float)this.m_newExp / (float)cityExpTable2.m_ExpRequired + (float)(cityNewLevel - cityOldLevel);
				}
			}
			this.m_imgExpGauge.fillAmount = this.m_fExpBeginRatio;
		}

		// Token: 0x060089FA RID: 35322 RVA: 0x002EC4DE File Offset: 0x002EA6DE
		public override bool IsProcessFinished()
		{
			return this.m_bFinished;
		}

		// Token: 0x060089FB RID: 35323 RVA: 0x002EC4E6 File Offset: 0x002EA6E6
		protected override IEnumerator InnerProcess(bool bAutoSkip)
		{
			yield return null;
			while (this.m_cgLayout.alpha < 1f)
			{
				yield return null;
			}
			this.m_bFinished = false;
			float totalTime = (this.m_fExpTargetRatio - this.m_fExpBeginRatio) * 0.6f;
			float deltaTime = 0f;
			int levelDelta = 0;
			while (deltaTime < totalTime)
			{
				float num = NKCUtil.TrackValue(TRACKING_DATA_TYPE.TDT_SLOWER, this.m_fExpBeginRatio, this.m_fExpTargetRatio, deltaTime, totalTime);
				if (num - (float)levelDelta >= 1f)
				{
					int num2 = levelDelta;
					levelDelta = num2 + 1;
					this.m_lbLevel.text = (this.m_oldLevel + levelDelta).ToString();
				}
				this.m_imgExpGauge.fillAmount = num - (float)levelDelta;
				deltaTime += Time.deltaTime;
				yield return null;
			}
			yield return null;
			this.FinishProcess();
			yield break;
		}

		// Token: 0x060089FC RID: 35324 RVA: 0x002EC4F8 File Offset: 0x002EA6F8
		public override void FinishProcess()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			this.m_imgExpGauge.fillAmount = this.m_fExpTargetRatio - (float)(this.m_newLevel - this.m_oldLevel);
			this.m_lbLevel.text = this.m_newLevel.ToString();
			this.m_bFinished = true;
		}

		// Token: 0x04007670 RID: 30320
		public CanvasGroup m_cgLayout;

		// Token: 0x04007671 RID: 30321
		private Text m_lbLevel;

		// Token: 0x04007672 RID: 30322
		private Text m_lbCityName;

		// Token: 0x04007673 RID: 30323
		private Text m_lbExp;

		// Token: 0x04007674 RID: 30324
		private Image m_imgExpGauge;

		// Token: 0x04007675 RID: 30325
		private bool m_bInit;

		// Token: 0x04007676 RID: 30326
		private bool m_bFinished;

		// Token: 0x04007677 RID: 30327
		private int m_oldLevel;

		// Token: 0x04007678 RID: 30328
		private int m_newLevel;

		// Token: 0x04007679 RID: 30329
		private int m_oldExp;

		// Token: 0x0400767A RID: 30330
		private int m_newExp;

		// Token: 0x0400767B RID: 30331
		private float m_fExpBeginRatio;

		// Token: 0x0400767C RID: 30332
		private float m_fExpTargetRatio;
	}
}
