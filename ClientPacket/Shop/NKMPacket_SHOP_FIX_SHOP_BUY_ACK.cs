using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D24 RID: 3364
	[PacketId(ClientPacketId.kNKMPacket_SHOP_FIX_SHOP_BUY_ACK)]
	public sealed class NKMPacket_SHOP_FIX_SHOP_BUY_ACK : ISerializable
	{
		// Token: 0x06009545 RID: 38213 RVA: 0x00329EA8 File Offset: 0x003280A8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet(ref this.productID);
			stream.PutOrGet<NKMShopPurchaseHistory>(ref this.histroy);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemData);
			stream.PutOrGet<NKMShopSubscriptionData>(ref this.subScriptionData);
			stream.PutOrGet(ref this.totalPaidAmount);
		}

		// Token: 0x040086EC RID: 34540
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040086ED RID: 34541
		public NKMRewardData rewardData;

		// Token: 0x040086EE RID: 34542
		public int productID;

		// Token: 0x040086EF RID: 34543
		public NKMShopPurchaseHistory histroy = new NKMShopPurchaseHistory();

		// Token: 0x040086F0 RID: 34544
		public NKMItemMiscData costItemData;

		// Token: 0x040086F1 RID: 34545
		public NKMShopSubscriptionData subScriptionData = new NKMShopSubscriptionData();

		// Token: 0x040086F2 RID: 34546
		public double totalPaidAmount;
	}
}
