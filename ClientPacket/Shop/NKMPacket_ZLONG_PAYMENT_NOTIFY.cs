using System;
using Cs.Protocol;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D32 RID: 3378
	[PacketId(ClientPacketId.kNKMPacket_ZLONG_PAYMENT_NOTIFY)]
	public sealed class NKMPacket_ZLONG_PAYMENT_NOTIFY : ISerializable
	{
		// Token: 0x06009561 RID: 38241 RVA: 0x0032A145 File Offset: 0x00328345
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet(ref this.productID);
			stream.PutOrGet<NKMShopPurchaseHistory>(ref this.history);
			stream.PutOrGet<NKMShopSubscriptionData>(ref this.subScriptionData);
			stream.PutOrGet(ref this.totalPaidAmount);
		}

		// Token: 0x0400870E RID: 34574
		public NKMRewardData rewardData;

		// Token: 0x0400870F RID: 34575
		public int productID;

		// Token: 0x04008710 RID: 34576
		public NKMShopPurchaseHistory history = new NKMShopPurchaseHistory();

		// Token: 0x04008711 RID: 34577
		public NKMShopSubscriptionData subScriptionData = new NKMShopSubscriptionData();

		// Token: 0x04008712 RID: 34578
		public double totalPaidAmount;
	}
}
