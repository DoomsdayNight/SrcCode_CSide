using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000792 RID: 1938
	public class NKCUIDevConsoleTagList : NKCUIDevConsoleContentBase
	{
		// Token: 0x04003BC6 RID: 15302
		private Queue<NKCUIDevConsoleLogScrollItem> m_LogScrollItemPool = new Queue<NKCUIDevConsoleLogScrollItem>();

		// Token: 0x04003BC7 RID: 15303
		private List<string> m_lstTags = new List<string>();

		// Token: 0x04003BC8 RID: 15304
		public NKCUIDevConsoleLogScrollItem m_LogScrollItem;

		// Token: 0x04003BC9 RID: 15305
		public InputField m_ifSearch;

		// Token: 0x04003BCA RID: 15306
		public LoopScrollRect m_LSLog;

		// Token: 0x04003BCB RID: 15307
		public RectTransform m_LogPoolObject;

		// Token: 0x04003BCC RID: 15308
		public RectTransform m_LogFilePoolObject;

		// Token: 0x04003BCD RID: 15309
		public NKCUIComStateButton m_NKM_UI_DEV_CONSOLE_OPEN_TAG_LIST_BUTTON;

		// Token: 0x04003BCE RID: 15310
		public NKCUIComStateButton m_NKM_UI_DEV_CONSOLE_CONTENT_TAG_LIST_BUTTON;

		// Token: 0x04003BCF RID: 15311
		public NKCUIComStateButton m_NKM_UI_DEV_CONSOLE_INTERVAL_LIST_BUTTON;

		// Token: 0x04003BD0 RID: 15312
		public NKCUIComToggle m_IntervalToggle;
	}
}
