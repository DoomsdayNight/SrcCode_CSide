using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000775 RID: 1909
	public class NKCUIDevConsole : MonoBehaviour
	{
		// Token: 0x04003A85 RID: 14981
		private static NKCUIDevConsole instance;

		// Token: 0x04003A86 RID: 14982
		private bool m_bOpened;

		// Token: 0x04003A87 RID: 14983
		public NKCUIComStateButton m_NKM_DEV_CONSOLE_MENU_CLOSE_BUTTON;

		// Token: 0x04003A88 RID: 14984
		public NKCUIComStateButton m_NKM_DEV_CONSOLE_MENU_LOG_BUTTON;

		// Token: 0x04003A89 RID: 14985
		public NKCUIComStateButton m_NKM_DEV_CONSOLE_MENU_CHEAT_BUTTON;

		// Token: 0x04003A8A RID: 14986
		public NKCUIComStateButton m_NKM_DEV_CONSOLE_MENU_POST_BUTTON;

		// Token: 0x04003A8B RID: 14987
		public NKCUIComStateButton m_NKM_DEV_CONSOLE_MENU_TUTORIAL_BUTTON;

		// Token: 0x04003A8C RID: 14988
		public NKCUIComStateButton m_NKM_DEV_CONSOLE_MENU_ATTENDACE_BUTTON;

		// Token: 0x04003A8D RID: 14989
		public NKCUIComToggle m_NKM_DEV_CONSOLE_MENU_SHOW_UID_BUTTON;

		// Token: 0x04003A8E RID: 14990
		public NKCUIDevConsoleLog m_NKM_DEV_CONSOLE_LOG;

		// Token: 0x04003A8F RID: 14991
		public NKCUIDevConsoleCheat m_NKM_DEV_CONSOLE_CHEAT;

		// Token: 0x04003A90 RID: 14992
		public NKCUIDevConsoleTutorial m_NKM_DEV_CONSOLE_TUTORIAL;

		// Token: 0x04003A91 RID: 14993
		public NKCUIDevConsoleMail m_NKM_DEV_CONSOLE_MAIL;

		// Token: 0x04003A92 RID: 14994
		[Header("인게임 디버깅")]
		public GameObject m_NKM_DEV_CONSOLE_MENU_DEBUG_TOGGLES;

		// Token: 0x04003A93 RID: 14995
		public NKCUIComToggle m_NKM_DEV_CONSOLE_MENU_SHOW_FRAME_RATE_TOGGLE;

		// Token: 0x04003A94 RID: 14996
		public NKCUIComToggle m_NKM_DEV_CONSOLE_MENU_SHOW_UNIT_DEBUG_INFO_TOGGLE;

		// Token: 0x04003A95 RID: 14997
		public NKCUIComToggle m_NKM_DEV_CONSOLE_MENU_SHOW_DE_DEBUG_INFO_TOGGLE;

		// Token: 0x04003A96 RID: 14998
		public NKCUIComToggle m_NKM_DEV_CONSOLE_MENU_SHOW_UNIT_COLLISION_BOX_TOGGLE;

		// Token: 0x04003A97 RID: 14999
		public NKCUIComToggle m_NKM_DEV_CONSOLE_MENU_SHOW_ATTACK_BOX_TOGGLE;

		// Token: 0x04003A98 RID: 15000
		public NKCUIComToggle m_NKM_DEV_CONSOLE_MENU_SHOW_STRING_ID_TOGGLE;

		// Token: 0x04003A99 RID: 15001
		public NKCUIComToggle m_NKM_DEV_CONSOLE_MENU_HIDE_GAME_HUD_TOGGLE;

		// Token: 0x04003A9A RID: 15002
		public Dropdown m_ddUnitDebugInfoType;

		// Token: 0x04003A9B RID: 15003
		public Text m_PatchVersionText;

		// Token: 0x04003A9C RID: 15004
		public Text m_lbServerTime;
	}
}
