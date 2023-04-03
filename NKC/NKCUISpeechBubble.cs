using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000772 RID: 1906
	public class NKCUISpeechBubble : MonoBehaviour
	{
		// Token: 0x06004C09 RID: 19465 RVA: 0x0016C1D4 File Offset: 0x0016A3D4
		public void SetText(string text)
		{
			NKCUtil.SetLabelText(this.m_lbText, text);
		}

		// Token: 0x04003A78 RID: 14968
		public Text m_lbText;

		// Token: 0x04003A79 RID: 14969
		public Image m_imgBubble;
	}
}
