using System;
using System.Collections;
using UnityEngine;

namespace NKC.UI.Result
{
	// Token: 0x02000BA6 RID: 2982
	public class NKCUIResultSubUITip : NKCUIResultSubUIBase
	{
		// Token: 0x060089DC RID: 35292 RVA: 0x002EBB85 File Offset: 0x002E9D85
		public override void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060089DD RID: 35293 RVA: 0x002EBB93 File Offset: 0x002E9D93
		public void SetData(BATTLE_RESULT_TYPE resultType, bool bIgnoreAutoClose = false)
		{
			if (resultType != BATTLE_RESULT_TYPE.BRT_WIN)
			{
				if (resultType - BATTLE_RESULT_TYPE.BRT_LOSE <= 1)
				{
					base.ProcessRequired = true;
				}
			}
			else
			{
				base.ProcessRequired = false;
			}
			this.m_bIgnoreAutoClose = bIgnoreAutoClose;
		}

		// Token: 0x060089DE RID: 35294 RVA: 0x002EBBB7 File Offset: 0x002E9DB7
		protected override IEnumerator InnerProcess(bool bAutoSkip)
		{
			this.m_bFinished = false;
			yield return null;
			this.m_bHadUserInput = false;
			float currentTime = 0f;
			if (NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetAlarmRepeatOperationQuitByDefeat())
			{
				this.m_bPause = true;
				NKCScenManager.GetScenManager().GetNKCRepeatOperaion().SetAlarmRepeatOperationQuitByDefeat(false);
				NKCScenManager.GetScenManager().GetNKCRepeatOperaion().Init();
				NKCScenManager.GetScenManager().GetNKCRepeatOperaion().SetStopReason(NKCStringTable.GetString("SI_POPUP_REPEAT_FAIL_DEFEAT", false));
				NKCPopupRepeatOperation.Instance.OpenForResult(delegate
				{
					this.m_bPause = false;
				});
			}
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

		// Token: 0x060089DF RID: 35295 RVA: 0x002EBBCD File Offset: 0x002E9DCD
		public override void FinishProcess()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			this.m_bFinished = true;
			base.StopAllCoroutines();
		}

		// Token: 0x060089E0 RID: 35296 RVA: 0x002EBBEA File Offset: 0x002E9DEA
		public override bool IsProcessFinished()
		{
			return this.m_bFinished;
		}

		// Token: 0x04007650 RID: 30288
		public float UI_END_DELAY_TIME = 2f;

		// Token: 0x04007651 RID: 30289
		private bool m_bFinished;
	}
}
