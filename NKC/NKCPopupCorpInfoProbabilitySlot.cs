using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007F2 RID: 2034
	public class NKCPopupCorpInfoProbabilitySlot : MonoBehaviour
	{
		// Token: 0x0600509E RID: 20638 RVA: 0x00186266 File Offset: 0x00184466
		public void SetData(string name, float probability)
		{
			NKCUtil.SetLabelText(this.m_lbName, name);
			NKCUtil.SetLabelText(this.m_lbProbability, string.Format("{0:0.0000}%", probability));
		}

		// Token: 0x04004096 RID: 16534
		public Text m_lbName;

		// Token: 0x04004097 RID: 16535
		public Text m_lbProbability;
	}
}
