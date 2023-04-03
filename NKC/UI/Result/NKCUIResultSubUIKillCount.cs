using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Result
{
	// Token: 0x02000BA0 RID: 2976
	public class NKCUIResultSubUIKillCount : NKCUIResultSubUIBase
	{
		// Token: 0x060089A7 RID: 35239 RVA: 0x002EAAB4 File Offset: 0x002E8CB4
		public void SetData(NKCUIResult.BattleResultData data, bool bIgnoreAutoClose = false)
		{
			if (data == null || data.m_KillCountGain == 0L)
			{
				base.ProcessRequired = false;
				return;
			}
			bool flag = data.m_KillCountGain > data.m_KillCountStageRecord;
			NKCUtil.SetGameobjectActive(this.m_objNewRecord, flag);
			NKCUtil.SetLabelText(this.m_lbNewRecordDesc, flag ? NKCUtilString.GET_FIERCE_BATTLE_END_TEXT_NEW_RECORD : "");
			NKCUtil.SetLabelText(this.m_lbScoreGain, data.m_KillCountGain.ToString("#,##0"));
			NKCUtil.SetLabelText(this.m_lbBestScore, data.m_KillCountStageRecord.ToString("#,##0"));
			NKCUtil.SetLabelText(this.m_lbTotalScore, data.m_KillCountTotal.ToString("#,##0"));
			string text = "-:--:--";
			if (data.m_battleData != null && data.m_battleData.playTime > 0f)
			{
				text = NKCUtilString.GetTimeSpanString(TimeSpan.FromSeconds((double)data.m_battleData.playTime));
			}
			NKCUtil.SetLabelText(this.m_lbBattleTime, text.ToString());
			base.ProcessRequired = true;
			this.m_bIgnoreAutoClose = bIgnoreAutoClose;
		}

		// Token: 0x060089A8 RID: 35240 RVA: 0x002EABB0 File Offset: 0x002E8DB0
		public override void FinishProcess()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			this.m_bFinished = true;
			base.StopAllCoroutines();
		}

		// Token: 0x060089A9 RID: 35241 RVA: 0x002EABCD File Offset: 0x002E8DCD
		public override bool IsProcessFinished()
		{
			return this.m_bFinished;
		}

		// Token: 0x060089AA RID: 35242 RVA: 0x002EABD5 File Offset: 0x002E8DD5
		protected override IEnumerator InnerProcess(bool bAutoSkip)
		{
			this.m_bFinished = false;
			this.m_bHadUserInput = false;
			float currentTime = 0f;
			if (bAutoSkip)
			{
				while (1f > currentTime)
				{
					if (this.m_bHadUserInput)
					{
						break;
					}
					if (!this.m_bPause)
					{
						currentTime += Time.deltaTime;
					}
					yield return null;
				}
			}
			else
			{
				while (this.m_bPause)
				{
					yield return null;
				}
				yield return base.WaitAniOrInput(null);
			}
			this.FinishProcess();
			yield break;
		}

		// Token: 0x060089AB RID: 35243 RVA: 0x002EABEB File Offset: 0x002E8DEB
		public override void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x04007610 RID: 30224
		[Header("킬카운트")]
		public Text m_lbScoreGain;

		// Token: 0x04007611 RID: 30225
		public Text m_lbBestScore;

		// Token: 0x04007612 RID: 30226
		public Text m_lbTotalScore;

		// Token: 0x04007613 RID: 30227
		public Text m_lbBattleTime;

		// Token: 0x04007614 RID: 30228
		public GameObject m_objNewRecord;

		// Token: 0x04007615 RID: 30229
		public Text m_lbNewRecordDesc;

		// Token: 0x04007616 RID: 30230
		private bool m_bFinished;
	}
}
