using System;

namespace NKC.UI
{
	// Token: 0x02000A26 RID: 2598
	public class PopupMessage
	{
		// Token: 0x060071CB RID: 29131 RVA: 0x0025D497 File Offset: 0x0025B697
		public PopupMessage(string message, NKCPopupMessage.eMessagePosition messagePosition, float delayTime, bool bPreemptive, bool bShowFX, bool bWaitForGameEnd)
		{
			this.m_message = message;
			this.m_messagePosition = messagePosition;
			this.m_delayTime = delayTime;
			this.m_bPreemptive = bPreemptive;
			this.m_bShowFX = bShowFX;
			this.m_bWaitForGameEnd = bWaitForGameEnd;
		}

		// Token: 0x04005DB7 RID: 23991
		public string m_message;

		// Token: 0x04005DB8 RID: 23992
		public NKCPopupMessage.eMessagePosition m_messagePosition;

		// Token: 0x04005DB9 RID: 23993
		public float m_delayTime;

		// Token: 0x04005DBA RID: 23994
		public bool m_bPreemptive;

		// Token: 0x04005DBB RID: 23995
		public bool m_bShowFX;

		// Token: 0x04005DBC RID: 23996
		public bool m_bWaitForGameEnd;
	}
}
