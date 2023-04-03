using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Result
{
	// Token: 0x02000BA5 RID: 2981
	public class NKCUIResultSubUIShadowTime : NKCUIResultSubUIBase
	{
		// Token: 0x060089D6 RID: 35286 RVA: 0x002EBAAC File Offset: 0x002E9CAC
		public void SetData(BATTLE_RESULT_TYPE resultType, int currClearTime, int bestClearTime, bool bIgnoreAutoClose = false)
		{
			if (resultType != BATTLE_RESULT_TYPE.BRT_WIN || currClearTime == 0)
			{
				base.ProcessRequired = false;
				return;
			}
			TimeSpan timeSpan = TimeSpan.FromSeconds((double)currClearTime);
			NKCUtil.SetLabelText(this.m_txtRecentTime, NKCUtilString.GetTimeSpanString(timeSpan));
			string msg = "-:--:--";
			if (bestClearTime > 0)
			{
				timeSpan = TimeSpan.FromSeconds((double)bestClearTime);
				msg = NKCUtilString.GetTimeSpanString(timeSpan);
			}
			NKCUtil.SetLabelText(this.m_txtBestTime, msg);
			NKCUtil.SetGameobjectActive(this.m_objNewRecord, bestClearTime == 0 || currClearTime < bestClearTime);
			base.ProcessRequired = true;
			this.m_bIgnoreAutoClose = bIgnoreAutoClose;
		}

		// Token: 0x060089D7 RID: 35287 RVA: 0x002EBB29 File Offset: 0x002E9D29
		public override void FinishProcess()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			this.m_bFinished = true;
			base.StopAllCoroutines();
		}

		// Token: 0x060089D8 RID: 35288 RVA: 0x002EBB46 File Offset: 0x002E9D46
		public override bool IsProcessFinished()
		{
			return this.m_bFinished;
		}

		// Token: 0x060089D9 RID: 35289 RVA: 0x002EBB4E File Offset: 0x002E9D4E
		protected override IEnumerator InnerProcess(bool bAutoSkip)
		{
			this.m_bFinished = false;
			this.m_bHadUserInput = false;
			float currentTime = 0f;
			if (bAutoSkip)
			{
				while (this.UI_END_DELAY_TIME > currentTime)
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

		// Token: 0x060089DA RID: 35290 RVA: 0x002EBB64 File Offset: 0x002E9D64
		public override void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0400764B RID: 30283
		public Text m_txtRecentTime;

		// Token: 0x0400764C RID: 30284
		public Text m_txtBestTime;

		// Token: 0x0400764D RID: 30285
		public GameObject m_objNewRecord;

		// Token: 0x0400764E RID: 30286
		public float UI_END_DELAY_TIME = 2f;

		// Token: 0x0400764F RID: 30287
		private bool m_bFinished;
	}
}
