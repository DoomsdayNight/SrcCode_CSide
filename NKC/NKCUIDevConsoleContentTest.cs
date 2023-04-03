using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000785 RID: 1925
	public class NKCUIDevConsoleContentTest : NKCUIDevConsoleContentBase2
	{
		// Token: 0x04003B48 RID: 15176
		[Header("KILLCOUNT")]
		public NKCUIComStateButton m_USER_KILLCOUNT_RESET;

		// Token: 0x04003B49 RID: 15177
		public NKCUIComStateButton m_SERVER_KILLCOUNT_RESET;

		// Token: 0x04003B4A RID: 15178
		public NKCUIComStateButton m_KILLCOUNT_REWARD_RESET;

		// Token: 0x04003B4B RID: 15179
		public InputField m_USER_KILLCOUNT;

		// Token: 0x04003B4C RID: 15180
		public InputField m_SERVER_KILLCOUNT;

		// Token: 0x04003B4D RID: 15181
		public InputField m_USER_REWARD_STEP;

		// Token: 0x04003B4E RID: 15182
		public InputField m_SERVER_REWARD_STEP;

		// Token: 0x04003B4F RID: 15183
		public InputField m_KILLCOUNT_ID;

		// Token: 0x04003B50 RID: 15184
		public NKCUIComStateButton m_USER_KILLCOUNT_APPLY;

		// Token: 0x04003B51 RID: 15185
		public NKCUIComStateButton m_SERVER_KILLCOUNT_APPLY;

		// Token: 0x04003B52 RID: 15186
		public NKCUIComStateButton m_KILLCOUNT_REWARD_STEP_APPLY;

		// Token: 0x04003B53 RID: 15187
		[Header("Raid Point")]
		public NKCUIComStateButton m_RaidPointChange;

		// Token: 0x04003B54 RID: 15188
		public InputField m_TargetPoint;

		// Token: 0x04003B55 RID: 15189
		public NKCUIComToggle m_ResetRewardPoint;
	}
}
