using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000789 RID: 1929
	public class NKCUIDevConsoleMail : NKCUIDevConsoleContentBase
	{
		// Token: 0x04003B69 RID: 15209
		public Dropdown m_ddType;

		// Token: 0x04003B6A RID: 15210
		public InputField m_ifTitle;

		// Token: 0x04003B6B RID: 15211
		public InputField m_ifDesc;

		// Token: 0x04003B6C RID: 15212
		public InputField m_ifExpiration;

		// Token: 0x04003B6D RID: 15213
		public List<NKCUIDevConsoleMail.PostItem> m_itemList;

		// Token: 0x04003B6E RID: 15214
		public NKCUIComStateButton m_btnOK;

		// Token: 0x0200144E RID: 5198
		[Serializable]
		public class PostItem
		{
			// Token: 0x04009E0D RID: 40461
			public Dropdown ddType;

			// Token: 0x04009E0E RID: 40462
			public NKCUIComStateButton btnSearch;

			// Token: 0x04009E0F RID: 40463
			public Text txtName;

			// Token: 0x04009E10 RID: 40464
			public InputField ifCount;
		}
	}
}
