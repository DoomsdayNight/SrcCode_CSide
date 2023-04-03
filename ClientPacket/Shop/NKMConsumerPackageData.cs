using System;
using Cs.Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D21 RID: 3361
	public sealed class NKMConsumerPackageData : ISerializable
	{
		// Token: 0x0600953F RID: 38207 RVA: 0x00329DD8 File Offset: 0x00327FD8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.productId);
			stream.PutOrGet(ref this.rewardedLevel);
			stream.PutOrGet(ref this.spendCount);
		}

		// Token: 0x040086E0 RID: 34528
		public int productId;

		// Token: 0x040086E1 RID: 34529
		public int rewardedLevel;

		// Token: 0x040086E2 RID: 34530
		public long spendCount;
	}
}
