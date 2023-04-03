using System;

namespace NKC
{
	// Token: 0x0200078D RID: 1933
	[Serializable]
	public struct ConsoleSubMenu
	{
		// Token: 0x06004C2C RID: 19500 RVA: 0x0016C3F1 File Offset: 0x0016A5F1
		public ConsoleSubMenu(DEV_CONSOLE_SUB_MENU _type, string _strKey, SUB_MENU_TYPE _stype, bool _warning = false)
		{
			this.type = _type;
			this.strKey = _strKey;
			this.stype = _stype;
			this.bWarning = _warning;
		}

		// Token: 0x04003BA8 RID: 15272
		public DEV_CONSOLE_SUB_MENU type;

		// Token: 0x04003BA9 RID: 15273
		public string strKey;

		// Token: 0x04003BAA RID: 15274
		public SUB_MENU_TYPE stype;

		// Token: 0x04003BAB RID: 15275
		public bool bWarning;
	}
}
