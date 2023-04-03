using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Result
{
	// Token: 0x02000B98 RID: 2968
	public class NKCUIResultSubUIFierceBattle : NKCUIResultSubUIBase
	{
		// Token: 0x06008906 RID: 35078 RVA: 0x002E5764 File Offset: 0x002E3964
		public void SetData(NKCUIResult.BattleResultData data, bool bIgnoreAutoClose = false)
		{
			if (data == null)
			{
				base.ProcessRequired = false;
				return;
			}
			bool flag = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr().GetBossGroupPoint() < data.m_iFierceScore;
			NKCUtil.SetGameobjectActive(this.m_NEW_RECORD_TAG_TEXT, flag);
			NKCUtil.SetLabelText(this.m_FIERCE_BATTLE_SCORE_INFO_TEXT, flag ? NKCUtilString.GET_FIERCE_BATTLE_END_TEXT_NEW_RECORD : NKCUtilString.GET_FIERCE_BATTLE_END_TEXT);
			NKCUtil.SetLabelText(this.m_FIERCE_BATTLE_SCORE, data.m_iFierceScore.ToString());
			NKCUtil.SetLabelText(this.m_FIERCE_BATTLE_BEST_SCORE, data.m_iFierceBestScore.ToString());
			float num = Math.Max(data.m_fFierceLastBossHPPercent, 0f);
			NKCUtil.SetLabelText(this.m_FIERCE_BATTLE_HP, string.Format("{0}%", num));
			string text = "-:--:--";
			if (data.m_fFierceRestTime > 0f)
			{
				text = NKCUtilString.GetTimeSpanString(TimeSpan.FromSeconds((double)data.m_fFierceRestTime));
			}
			NKCUtil.SetLabelText(this.m_FIERCE_BATTLE_TIME, text.ToString());
			base.ProcessRequired = true;
			this.m_bIgnoreAutoClose = bIgnoreAutoClose;
		}

		// Token: 0x06008907 RID: 35079 RVA: 0x002E5856 File Offset: 0x002E3A56
		public override void FinishProcess()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			this.m_bFinished = true;
			base.StopAllCoroutines();
		}

		// Token: 0x06008908 RID: 35080 RVA: 0x002E5873 File Offset: 0x002E3A73
		public override bool IsProcessFinished()
		{
			return this.m_bFinished;
		}

		// Token: 0x06008909 RID: 35081 RVA: 0x002E587B File Offset: 0x002E3A7B
		protected override IEnumerator InnerProcess(bool bAutoSkip)
		{
			this.m_bFinished = false;
			this.m_bHadUserInput = false;
			yield return null;
			yield break;
		}

		// Token: 0x0600890A RID: 35082 RVA: 0x002E588A File Offset: 0x002E3A8A
		public override void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0400757D RID: 30077
		[Header("격전지원")]
		public GameObject m_Result_FierceBattle;

		// Token: 0x0400757E RID: 30078
		public Text m_FIERCE_BATTLE_SCORE;

		// Token: 0x0400757F RID: 30079
		public Text m_FIERCE_BATTLE_BEST_SCORE;

		// Token: 0x04007580 RID: 30080
		public Text m_FIERCE_BATTLE_HP;

		// Token: 0x04007581 RID: 30081
		public Text m_FIERCE_BATTLE_TIME;

		// Token: 0x04007582 RID: 30082
		public GameObject m_NEW_RECORD_TAG_TEXT;

		// Token: 0x04007583 RID: 30083
		public Text m_FIERCE_BATTLE_SCORE_INFO_TEXT;

		// Token: 0x04007584 RID: 30084
		private bool m_bFinished;
	}
}
