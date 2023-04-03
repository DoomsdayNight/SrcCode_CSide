using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000781 RID: 1921
	public class NKCUIDevConsoleContentGame : NKCUIDevConsoleContentBase2
	{
		// Token: 0x06004C19 RID: 19481 RVA: 0x0016C305 File Offset: 0x0016A505
		public static bool GetShowDebugUnitInfo()
		{
			return NKCUIDevConsoleContentGame.m_ShowDebugUnitInfo;
		}

		// Token: 0x06004C1A RID: 19482 RVA: 0x0016C30C File Offset: 0x0016A50C
		public static NKC_DEV_UNIT_INFO GetUnitInfoType()
		{
			return NKCUIDevConsoleContentGame.m_DebugInfoType;
		}

		// Token: 0x06004C1B RID: 19483 RVA: 0x0016C313 File Offset: 0x0016A513
		public static bool GetShowDebugDEInfo()
		{
			return NKCUIDevConsoleContentGame.m_ShowDebugDEInfo;
		}

		// Token: 0x06004C1C RID: 19484 RVA: 0x0016C31A File Offset: 0x0016A51A
		public static bool GetShowDebugCollisionBox()
		{
			return NKCUIDevConsoleContentGame.m_ShowDebugCollisionBox;
		}

		// Token: 0x06004C1D RID: 19485 RVA: 0x0016C321 File Offset: 0x0016A521
		public static bool GetShowDebugAttackBox()
		{
			return NKCUIDevConsoleContentGame.m_ShowDebugAttackBox;
		}

		// Token: 0x04003B33 RID: 15155
		public NKCUIDevConsoleTutorial m_NKM_DEV_CONSOLE_TUTORIAL;

		// Token: 0x04003B34 RID: 15156
		private bool isMooJuckMode;

		// Token: 0x04003B35 RID: 15157
		private bool isWarfareUnbreakableMode;

		// Token: 0x04003B36 RID: 15158
		[Header("unit debug info toggle")]
		public NKCUIComToggle m_NKM_DEV_CONSOLE_MENU_SHOW_UNIT_DEBUG_INFO_TOGGLE;

		// Token: 0x04003B37 RID: 15159
		public Dropdown m_ddUnitDebugInfoType;

		// Token: 0x04003B38 RID: 15160
		private static bool m_ShowDebugDEInfo = false;

		// Token: 0x04003B39 RID: 15161
		private static NKC_DEV_UNIT_INFO m_DebugInfoType = NKC_DEV_UNIT_INFO.STAT;

		// Token: 0x04003B3A RID: 15162
		private static bool m_ShowDebugCollisionBox = false;

		// Token: 0x04003B3B RID: 15163
		private static bool m_ShowDebugAttackBox = false;

		// Token: 0x04003B3C RID: 15164
		private static bool m_ShowDebugUnitInfo = false;
	}
}
