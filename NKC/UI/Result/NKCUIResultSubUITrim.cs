using System;
using System.Collections;
using System.Collections.Generic;
using NKC.Trim;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Result
{
	// Token: 0x02000BA7 RID: 2983
	public class NKCUIResultSubUITrim : NKCUIResultSubUIBase
	{
		// Token: 0x060089E3 RID: 35299 RVA: 0x002EBC10 File Offset: 0x002E9E10
		public void SetData(NKCUIResult.BattleResultData data)
		{
			base.ProcessRequired = (data != null && data.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_TRIM && NKCTrimManager.TrimModeState != null);
			if (!base.ProcessRequired)
			{
				return;
			}
			if (this.m_lstSlot != null)
			{
				for (int i = 0; i < this.m_lstSlot.Count; i++)
				{
					if (!(this.m_lstSlot[i] == null))
					{
						this.m_lstSlot[i].SetData(i);
					}
				}
			}
			NKCUtil.SetLabelText(this.m_lbTrimLevel, NKCTrimManager.TrimModeState.trimLevel.ToString());
		}

		// Token: 0x060089E4 RID: 35300 RVA: 0x002EBCA2 File Offset: 0x002E9EA2
		public override void FinishProcess()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			this.m_bFinished = true;
			base.StopAllCoroutines();
		}

		// Token: 0x060089E5 RID: 35301 RVA: 0x002EBCBF File Offset: 0x002E9EBF
		public override bool IsProcessFinished()
		{
			return this.m_bFinished;
		}

		// Token: 0x060089E6 RID: 35302 RVA: 0x002EBCC7 File Offset: 0x002E9EC7
		protected override IEnumerator InnerProcess(bool bAutoSkip)
		{
			this.m_bFinished = false;
			this.m_bHadUserInput = false;
			yield return null;
			NKCGameClient gameClient = NKCScenManager.GetScenManager().GetGameClient();
			NKMGameRuntimeData gameRuntimeData = gameClient.GetGameRuntimeData();
			NKMGameRuntimeTeamData nkmgameRuntimeTeamData = (gameRuntimeData != null) ? gameRuntimeData.GetMyRuntimeTeamData(gameClient.m_MyTeam) : null;
			if (nkmgameRuntimeTeamData != null && nkmgameRuntimeTeamData.m_bAutoRespawn)
			{
				float currentTime = 0f;
				while (this.DelayBeforeClose > currentTime && !this.m_bHadUserInput)
				{
					if (!this.m_bPause)
					{
						currentTime += Time.deltaTime;
					}
					yield return null;
				}
				this.m_bFinished = true;
			}
			yield break;
		}

		// Token: 0x04007652 RID: 30290
		public List<NKCUIResultSubUITrimSlot> m_lstSlot;

		// Token: 0x04007653 RID: 30291
		public Text m_lbTrimLevel;

		// Token: 0x04007654 RID: 30292
		private bool m_bFinished;

		// Token: 0x04007655 RID: 30293
		private float DelayBeforeClose = 3f;
	}
}
