using System;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x0200077B RID: 1915
	public class NKCUIDevConsoleCheatUserInfo : NKCUIDevConsoleContentBase
	{
		// Token: 0x04003B0C RID: 15116
		public NKCUIDevConsoleCheatUserInfoController m_NKM_UI_DEV_CONSOLE_CHEAT_USER_INFO_LEVEL;

		// Token: 0x04003B0D RID: 15117
		public Toggle m_BeforeLevelUpTogglePaid;

		// Token: 0x04003B0E RID: 15118
		public Dropdown m_NKM_UI_DEV_CONSOLE_CHEAT_RESOURCE_TYPE;

		// Token: 0x04003B0F RID: 15119
		public NKCUIDevConsoleCheatUserInfoController m_NKM_UI_DEV_CONSOLE_CHEAT_USER_INFO_RESOURCE;

		// Token: 0x04003B10 RID: 15120
		public InputField m_ifRecourceID;

		// Token: 0x04003B11 RID: 15121
		public NKCUIComStateButton m_NKM_UI_DEV_CONSOLE_CHEAT_ADD_USER_BUFF;

		// Token: 0x04003B12 RID: 15122
		public InputField m_NKM_UI_DEV_CONSOLE_CHEAT_USER_BUFF_ID_INPUT_FIELD;

		// Token: 0x04003B13 RID: 15123
		public Toggle m_TogglePaid;

		// Token: 0x0200144A RID: 5194
		private enum ChangeResourceOp
		{
			// Token: 0x04009E01 RID: 40449
			Plus,
			// Token: 0x04009E02 RID: 40450
			Minus
		}

		// Token: 0x0200144B RID: 5195
		private enum ResourceType
		{
			// Token: 0x04009E04 RID: 40452
			Credit,
			// Token: 0x04009E05 RID: 40453
			Eternium,
			// Token: 0x04009E06 RID: 40454
			Information,
			// Token: 0x04009E07 RID: 40455
			CashPaid,
			// Token: 0x04009E08 RID: 40456
			CashFree,
			// Token: 0x04009E09 RID: 40457
			Max
		}
	}
}
