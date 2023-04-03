using System;
using System.Collections.Generic;
using ClientPacket.Mode;
using NKC.UI.Result;
using NKM;

namespace NKC
{
	// Token: 0x0200072B RID: 1835
	public class NKC_SCEN_SHADOW_RESULT : NKC_SCEN_BASIC
	{
		// Token: 0x0600490F RID: 18703 RVA: 0x00160112 File Offset: 0x0015E312
		public NKC_SCEN_SHADOW_RESULT()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_SHADOW_RESULT;
		}

		// Token: 0x06004910 RID: 18704 RVA: 0x0016012D File Offset: 0x0015E32D
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			this.m_resultUI = NKCUIWarfareResult.Instance;
		}

		// Token: 0x06004911 RID: 18705 RVA: 0x00160140 File Offset: 0x0015E340
		public void SetData(NKMShadowGameResult result, List<int> lstBestTime)
		{
			this.m_shadowResult = result;
			this.m_lstBestTime = lstBestTime;
			this.m_unitID = 999;
			this.m_shipID = 26001;
		}

		// Token: 0x06004912 RID: 18706 RVA: 0x00160166 File Offset: 0x0015E366
		public override void ScenStart()
		{
			base.ScenStart();
			NKCUIWarfareResult resultUI = this.m_resultUI;
			if (resultUI == null)
			{
				return;
			}
			resultUI.OpenForShadow(this.m_shadowResult, this.m_lstBestTime, this.m_unitID, this.m_shipID, new NKCUIWarfareResult.CallBackWhenClosed(this.OnCloseResultUI));
		}

		// Token: 0x06004913 RID: 18707 RVA: 0x001601A2 File Offset: 0x0015E3A2
		public override void ScenEnd()
		{
			base.ScenEnd();
			NKCUIWarfareResult resultUI = this.m_resultUI;
			if (resultUI == null)
			{
				return;
			}
			resultUI.Close();
		}

		// Token: 0x06004914 RID: 18708 RVA: 0x001601BA File Offset: 0x0015E3BA
		private void OnCloseResultUI(NKM_SCEN_ID scenID)
		{
			NKCScenManager.GetScenManager().ScenChangeFade(scenID, true);
		}

		// Token: 0x0400387B RID: 14459
		private NKCUIWarfareResult m_resultUI;

		// Token: 0x0400387C RID: 14460
		private NKMShadowGameResult m_shadowResult;

		// Token: 0x0400387D RID: 14461
		private List<int> m_lstBestTime = new List<int>();

		// Token: 0x0400387E RID: 14462
		private int m_unitID;

		// Token: 0x0400387F RID: 14463
		private int m_shipID;
	}
}
