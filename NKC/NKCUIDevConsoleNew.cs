using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000790 RID: 1936
	public class NKCUIDevConsoleNew : MonoBehaviour
	{
		// Token: 0x04003BB3 RID: 15283
		private NKCUIDevConsoleContentBase[] m_Contents = new NKCUIDevConsoleContentBase[7];

		// Token: 0x04003BB4 RID: 15284
		[Header("Dummy Object")]
		public GameObject m_pfbButton;

		// Token: 0x04003BB5 RID: 15285
		public GameObject m_pfbCheckBox;

		// Token: 0x04003BB6 RID: 15286
		[Header("첫번째 메뉴")]
		public List<ConsoleMainMenu> m_lstFirstMenu;

		// Token: 0x04003BB7 RID: 15287
		[Header("Contents")]
		public NKCUIDevConsoleMenu m_ContentMenu;

		// Token: 0x04003BB8 RID: 15288
		[Space]
		public NKCUIDevConsoleLog m_NKM_DEV_CONSOLE_LOG;

		// Token: 0x04003BB9 RID: 15289
		public NKCUIDevConsoleContentSystem m_NKM_DEV_CONSOLE_SYSTEM;

		// Token: 0x04003BBA RID: 15290
		public NKCUIDevConsoleContentGame m_NKM_UI_DEV_CONSOLE_GAME;

		// Token: 0x04003BBB RID: 15291
		public NKCUIDevConsoleContentTest m_NKM_UI_DEV_CONSOLE_TEST;

		// Token: 0x04003BBC RID: 15292
		public NKCUIDevConsoleContentShop m_NKM_UI_DEV_CONSOLE_SHOP;

		// Token: 0x04003BBD RID: 15293
		public NKCUIDevConsoleContentPVP m_NKM_UI_DEV_CONSOLE_PVP;

		// Token: 0x04003BBE RID: 15294
		public NKCUIDevConsoleContentCheat m_NKM_UI_DEV_CONSOLE_CHEAT;

		// Token: 0x04003BBF RID: 15295
		[Header("button")]
		public NKCUIComStateButton m_NKM_DEV_CONSOLE_MENU_CLOSE_BUTTON;

		// Token: 0x04003BC0 RID: 15296
		[Header("etc")]
		public Text m_PatchVersionText;

		// Token: 0x04003BC1 RID: 15297
		public Text m_lbServerTime;
	}
}
