using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000788 RID: 1928
	public class NKCUIDevConsoleLogScrollItem : MonoBehaviour
	{
		// Token: 0x06004C29 RID: 19497 RVA: 0x0016C3D3 File Offset: 0x0016A5D3
		public void SetData(string text)
		{
			this.m_Text.text = text;
		}

		// Token: 0x04003B68 RID: 15208
		public Text m_Text;
	}
}
