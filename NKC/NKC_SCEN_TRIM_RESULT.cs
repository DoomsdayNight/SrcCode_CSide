using System;
using ClientPacket.Common;
using ClientPacket.Mode;
using NKC.UI.Result;
using NKM;

namespace NKC
{
	// Token: 0x0200072F RID: 1839
	public class NKC_SCEN_TRIM_RESULT : NKC_SCEN_BASIC
	{
		// Token: 0x0600493C RID: 18748 RVA: 0x00160914 File Offset: 0x0015EB14
		public NKC_SCEN_TRIM_RESULT()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_TRIM_RESULT;
		}

		// Token: 0x0600493D RID: 18749 RVA: 0x00160924 File Offset: 0x0015EB24
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			this.m_resultUI = NKCUIWarfareResult.Instance;
		}

		// Token: 0x0600493E RID: 18750 RVA: 0x00160937 File Offset: 0x0015EB37
		public void SetUnitUId(long unitUId)
		{
			this.m_unitUId = unitUId;
		}

		// Token: 0x0600493F RID: 18751 RVA: 0x00160940 File Offset: 0x0015EB40
		public void SetData(NKMTrimClearData trimClearData, TrimModeState trimModeState, int bestScore, bool firstClear)
		{
			this.m_trimModeState = trimModeState;
			this.m_trimClearData = trimClearData;
			this.m_bestScore = bestScore;
			this.m_bFirstClear = firstClear;
		}

		// Token: 0x06004940 RID: 18752 RVA: 0x00160960 File Offset: 0x0015EB60
		public override void ScenStart()
		{
			base.ScenStart();
			int dummyUnitId = (this.m_unitUId == 0L) ? 999 : 0;
			NKCUIWarfareResult.Instance.OpenForTrim(this.m_trimClearData, this.m_trimModeState, this.m_unitUId, dummyUnitId, this.m_bFirstClear, this.m_bestScore, new NKCUIWarfareResult.CallBackWhenClosed(this.OnCloseResultUI));
		}

		// Token: 0x06004941 RID: 18753 RVA: 0x001609B9 File Offset: 0x0015EBB9
		public override void ScenEnd()
		{
			base.ScenEnd();
			NKCUIWarfareResult resultUI = this.m_resultUI;
			if (resultUI != null)
			{
				resultUI.Close();
			}
			this.m_resultUI = null;
			this.m_trimClearData = null;
			this.m_trimModeState = null;
			this.m_bFirstClear = false;
			this.m_unitUId = 0L;
		}

		// Token: 0x06004942 RID: 18754 RVA: 0x001609F6 File Offset: 0x0015EBF6
		private void OnCloseResultUI(NKM_SCEN_ID scenID)
		{
			NKCScenManager.GetScenManager().ScenChangeFade(scenID, true);
		}

		// Token: 0x0400388B RID: 14475
		private NKCUIWarfareResult m_resultUI;

		// Token: 0x0400388C RID: 14476
		private TrimModeState m_trimModeState;

		// Token: 0x0400388D RID: 14477
		private NKMTrimClearData m_trimClearData;

		// Token: 0x0400388E RID: 14478
		private long m_unitUId;

		// Token: 0x0400388F RID: 14479
		private int m_bestScore;

		// Token: 0x04003890 RID: 14480
		private bool m_bFirstClear;
	}
}
