using System;

namespace NKC
{
	// Token: 0x020006A6 RID: 1702
	public class NKCMessageData
	{
		// Token: 0x06003821 RID: 14369 RVA: 0x0012231B File Offset: 0x0012051B
		public NKCMessageData()
		{
			this.Init();
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x00122329 File Offset: 0x00120529
		public void Init()
		{
			this.m_NKC_EVENT_MESSAGE = NKC_EVENT_MESSAGE.NEM_INVALID;
			this.m_MsgID2 = 0;
			this.m_Param1 = null;
			this.m_Param2 = null;
			this.m_Param3 = null;
			this.m_fLatency = 0f;
		}

		// Token: 0x040034A2 RID: 13474
		public NKC_EVENT_MESSAGE m_NKC_EVENT_MESSAGE;

		// Token: 0x040034A3 RID: 13475
		public int m_MsgID2;

		// Token: 0x040034A4 RID: 13476
		public object m_Param1;

		// Token: 0x040034A5 RID: 13477
		public object m_Param2;

		// Token: 0x040034A6 RID: 13478
		public object m_Param3;

		// Token: 0x040034A7 RID: 13479
		public float m_fLatency;
	}
}
