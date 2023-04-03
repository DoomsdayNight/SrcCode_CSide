using System;

namespace NKC
{
	// Token: 0x02000784 RID: 1924
	public class NKCUIDevConsoleContentSystem : NKCUIDevConsoleContentBase2
	{
		// Token: 0x06004C22 RID: 19490 RVA: 0x0016C360 File Offset: 0x0016A560
		public static bool GetShowDebugFrame()
		{
			return NKCUIDevConsoleContentSystem.m_ShowDebugFrame;
		}

		// Token: 0x04003B45 RID: 15173
		public NKCUIDevConsoleMail m_NKM_DEV_CONSOLE_MAIL;

		// Token: 0x04003B46 RID: 15174
		public NKCUIDevConsoleTagList m_NKM_DEV_CONSOLE_TAG_LIST;

		// Token: 0x04003B47 RID: 15175
		private static bool m_ShowDebugFrame;
	}
}
