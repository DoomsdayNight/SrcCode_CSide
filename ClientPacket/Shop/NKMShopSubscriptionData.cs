using System;
using Cs.Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D1E RID: 3358
	public sealed class NKMShopSubscriptionData : ISerializable
	{
		// Token: 0x06009539 RID: 38201 RVA: 0x00329D42 File Offset: 0x00327F42
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.productId);
			stream.PutOrGet(ref this.rewardCount);
			stream.PutOrGet(ref this.lastUpdateDate);
			stream.PutOrGet(ref this.startDate);
			stream.PutOrGet(ref this.endDate);
		}

		// Token: 0x040086D6 RID: 34518
		public int productId;

		// Token: 0x040086D7 RID: 34519
		public int rewardCount;

		// Token: 0x040086D8 RID: 34520
		public DateTime lastUpdateDate;

		// Token: 0x040086D9 RID: 34521
		public DateTime startDate;

		// Token: 0x040086DA RID: 34522
		public DateTime endDate;
	}
}
