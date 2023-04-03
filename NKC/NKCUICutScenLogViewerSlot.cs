using System;
using TMPro;
using UnityEngine;

namespace NKC
{
	// Token: 0x020007B8 RID: 1976
	public class NKCUICutScenLogViewerSlot : MonoBehaviour
	{
		// Token: 0x06004E48 RID: 20040 RVA: 0x00179A41 File Offset: 0x00177C41
		public void SetData(string desc)
		{
			TextMeshProUGUI lbTmpDesc = this.m_lbTmpDesc;
			if (lbTmpDesc == null)
			{
				return;
			}
			lbTmpDesc.SetText(desc, true);
		}

		// Token: 0x04003DE6 RID: 15846
		public TextMeshProUGUI m_lbTmpDesc;
	}
}
