using System;

namespace ClientPacket.Event
{
	// Token: 0x02000F94 RID: 3988
	public enum KakaoMissionState
	{
		// Token: 0x04008D29 RID: 36137
		Initialized,
		// Token: 0x04008D2A RID: 36138
		Registered,
		// Token: 0x04008D2B RID: 36139
		Sent,
		// Token: 0x04008D2C RID: 36140
		Confirmed,
		// Token: 0x04008D2D RID: 36141
		Failed,
		// Token: 0x04008D2E RID: 36142
		Flopped,
		// Token: 0x04008D2F RID: 36143
		NotEnoughBudget,
		// Token: 0x04008D30 RID: 36144
		OutOfDate
	}
}
