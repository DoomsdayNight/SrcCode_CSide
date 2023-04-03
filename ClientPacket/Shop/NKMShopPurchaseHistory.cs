using System;
using Cs.Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D1F RID: 3359
	public sealed class NKMShopPurchaseHistory : ISerializable
	{
		// Token: 0x0600953B RID: 38203 RVA: 0x00329D88 File Offset: 0x00327F88
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.shopId);
			stream.PutOrGet(ref this.purchaseCount);
			stream.PutOrGet(ref this.nextResetDate);
		}

		// Token: 0x040086DB RID: 34523
		public int shopId;

		// Token: 0x040086DC RID: 34524
		public int purchaseCount;

		// Token: 0x040086DD RID: 34525
		public long nextResetDate;
	}
}
