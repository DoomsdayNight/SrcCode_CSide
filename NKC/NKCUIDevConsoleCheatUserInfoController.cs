using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x0200077C RID: 1916
	public class NKCUIDevConsoleCheatUserInfoController : MonoBehaviour
	{
		// Token: 0x04003B14 RID: 15124
		public NKCUIComStateButton m_ConfirmButton;

		// Token: 0x04003B15 RID: 15125
		public NKCUIComStateButton m_ConfirmButton2;

		// Token: 0x04003B16 RID: 15126
		public InputField m_CountInputField;

		// Token: 0x0200144C RID: 5196
		// (Invoke) Token: 0x0600A857 RID: 43095
		public delegate void OnConfirmCallBack();
	}
}
