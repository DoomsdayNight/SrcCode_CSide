using System;
using ClientPacket.Common;
using NKC.Trim;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Result
{
	// Token: 0x02000BA8 RID: 2984
	public class NKCUIResultSubUITrimSlot : MonoBehaviour
	{
		// Token: 0x060089E8 RID: 35304 RVA: 0x002EBCEC File Offset: 0x002E9EEC
		public void SetData(int index)
		{
			if (NKCTrimManager.TrimModeState == null)
			{
				this.SetData(index, 0, NKCUIResultSubUITrimSlot.State.Undecided, false);
				return;
			}
			NKMTrimStageData finishedTrimStageData = NKCTrimManager.GetFinishedTrimStageData(index);
			if (finishedTrimStageData == null)
			{
				this.SetData(index, 0, NKCUIResultSubUITrimSlot.State.Undecided, false);
				return;
			}
			bool bCurrent = NKCTrimManager.TrimModeState.lastClearStage != null && NKCTrimManager.TrimModeState.lastClearStage.index == index;
			this.SetData(index, finishedTrimStageData.score, finishedTrimStageData.isWin ? NKCUIResultSubUITrimSlot.State.Clear : NKCUIResultSubUITrimSlot.State.Fail, bCurrent);
		}

		// Token: 0x060089E9 RID: 35305 RVA: 0x002EBD5C File Offset: 0x002E9F5C
		private void SetData(int index, int score, NKCUIResultSubUITrimSlot.State state, bool bCurrent)
		{
			int num = index + 1;
			NKCUtil.SetLabelText(this.m_lbStageNumber, num.ToString());
			NKCUtil.SetLabelText(this.m_lbStageNumber_Current, num.ToString());
			NKCUtil.SetLabelText(this.m_lbScore, score.ToString());
			NKCUtil.SetGameobjectActive(this.m_goCurrent, bCurrent);
			NKCUtil.SetGameobjectActive(this.m_goNormal, !bCurrent);
			NKCUtil.SetGameobjectActive(this.m_goClear, state == NKCUIResultSubUITrimSlot.State.Clear);
			NKCUtil.SetGameobjectActive(this.m_goNone, state == NKCUIResultSubUITrimSlot.State.Undecided || (this.m_goFail == null && state > NKCUIResultSubUITrimSlot.State.Clear));
			NKCUtil.SetGameobjectActive(this.m_goFail, state == NKCUIResultSubUITrimSlot.State.Fail);
		}

		// Token: 0x04007656 RID: 30294
		public GameObject m_goCurrent;

		// Token: 0x04007657 RID: 30295
		public GameObject m_goNormal;

		// Token: 0x04007658 RID: 30296
		public Text m_lbStageNumber;

		// Token: 0x04007659 RID: 30297
		public Text m_lbStageNumber_Current;

		// Token: 0x0400765A RID: 30298
		public Text m_lbScore;

		// Token: 0x0400765B RID: 30299
		public GameObject m_goClear;

		// Token: 0x0400765C RID: 30300
		public GameObject m_goFail;

		// Token: 0x0400765D RID: 30301
		public GameObject m_goNone;

		// Token: 0x0200196F RID: 6511
		public enum State
		{
			// Token: 0x0400ABEA RID: 44010
			Clear,
			// Token: 0x0400ABEB RID: 44011
			Fail,
			// Token: 0x0400ABEC RID: 44012
			Undecided
		}
	}
}
