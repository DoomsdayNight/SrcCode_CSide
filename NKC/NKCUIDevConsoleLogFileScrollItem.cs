using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000787 RID: 1927
	public class NKCUIDevConsoleLogFileScrollItem : MonoBehaviour
	{
		// Token: 0x06004C27 RID: 19495 RVA: 0x0016C3B8 File Offset: 0x0016A5B8
		public void SetData(string fullPathForFilename)
		{
			this.m_Text.text = Path.GetFileName(fullPathForFilename);
		}

		// Token: 0x04003B66 RID: 15206
		public Text m_Text;

		// Token: 0x04003B67 RID: 15207
		public NKCUIComStateButton m_Button;
	}
}
