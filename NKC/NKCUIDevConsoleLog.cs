using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000786 RID: 1926
	public class NKCUIDevConsoleLog : NKCUIDevConsoleContentBase
	{
		// Token: 0x04003B56 RID: 15190
		private Queue<NKCUIDevConsoleLogScrollItem> m_LogScrollItemPool = new Queue<NKCUIDevConsoleLogScrollItem>();

		// Token: 0x04003B57 RID: 15191
		private Queue<NKCUIDevConsoleLogFileScrollItem> m_LogFileScrollItemPool = new Queue<NKCUIDevConsoleLogFileScrollItem>();

		// Token: 0x04003B58 RID: 15192
		private List<string> m_lstLog = new List<string>();

		// Token: 0x04003B59 RID: 15193
		private List<string> m_lstOldLog = new List<string>();

		// Token: 0x04003B5A RID: 15194
		private List<string> m_lstLogfile = new List<string>();

		// Token: 0x04003B5B RID: 15195
		public NKCUIDevConsoleLogScrollItem m_LogScrollItem;

		// Token: 0x04003B5C RID: 15196
		public NKCUIDevConsoleLogFileScrollItem m_LogFileScrollItem;

		// Token: 0x04003B5D RID: 15197
		public LoopScrollRect m_LSLog;

		// Token: 0x04003B5E RID: 15198
		public LoopScrollRect m_LSOldLog;

		// Token: 0x04003B5F RID: 15199
		public LoopScrollRect m_LSFile;

		// Token: 0x04003B60 RID: 15200
		public RectTransform m_LogPoolObject;

		// Token: 0x04003B61 RID: 15201
		public RectTransform m_LogFilePoolObject;

		// Token: 0x04003B62 RID: 15202
		public NKCUIComStateButton m_NKM_UI_DEV_CONSOLE_MENU_CLEAR_LOG_BUTTON;

		// Token: 0x04003B63 RID: 15203
		public NKCUIComStateButton m_NKM_UI_DEV_CONSOLE_MENU_CURRENT_LOG_BUTTON;

		// Token: 0x04003B64 RID: 15204
		public NKCUIComStateButton m_NKM_UI_DEV_CONSOLE_MENU_PREV_LOG_BUTTON;

		// Token: 0x04003B65 RID: 15205
		public NKCUIComStateButton m_NKM_UI_DEV_CONSOLE_MENU_LATEST_LOG_BUTTON;
	}
}
