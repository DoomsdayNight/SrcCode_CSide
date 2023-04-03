using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Tooltip
{
	// Token: 0x02000B05 RID: 2821
	public class NKCUITooltipEtc : NKCUITooltipBase
	{
		// Token: 0x0600804C RID: 32844 RVA: 0x002B44C9 File Offset: 0x002B26C9
		public override void Init()
		{
		}

		// Token: 0x0600804D RID: 32845 RVA: 0x002B44CC File Offset: 0x002B26CC
		public override void SetData(NKCUITooltip.Data data)
		{
			NKCUITooltip.EtcData etcData = data as NKCUITooltip.EtcData;
			if (etcData == null)
			{
				Debug.LogError("Tooltip EtcData is null");
				return;
			}
			NKCUtil.SetLabelText(this.m_lbName, etcData.m_Title);
			NKCUtil.SetLabelText(this.m_lbDesc, etcData.m_Desc);
		}

		// Token: 0x0600804E RID: 32846 RVA: 0x002B4510 File Offset: 0x002B2710
		public override void SetData(string title, string desc)
		{
			NKCUtil.SetLabelText(this.m_lbName, title);
			NKCUtil.SetLabelText(this.m_lbDesc, desc);
		}

		// Token: 0x04006C87 RID: 27783
		public Text m_lbName;

		// Token: 0x04006C88 RID: 27784
		public Text m_lbDesc;
	}
}
