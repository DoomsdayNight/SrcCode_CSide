using System;
using Cs.Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000FA9 RID: 4009
	public sealed class NKMADItemRewardInfo : ISerializable
	{
		// Token: 0x06009A26 RID: 39462 RVA: 0x00330FC1 File Offset: 0x0032F1C1
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.adItemId);
			stream.PutOrGet(ref this.remainDailyLimit);
			stream.PutOrGet(ref this.latestRewardTime);
			stream.PutOrGet(ref this.latestDailyTime);
		}

		// Token: 0x04008D5F RID: 36191
		public int adItemId;

		// Token: 0x04008D60 RID: 36192
		public int remainDailyLimit;

		// Token: 0x04008D61 RID: 36193
		public DateTime latestRewardTime;

		// Token: 0x04008D62 RID: 36194
		public DateTime latestDailyTime;
	}
}
