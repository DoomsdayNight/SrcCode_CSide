using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D37 RID: 3383
	[PacketId(ClientPacketId.kNKMPacket_GAMEBASE_BUY_ACK)]
	public sealed class NKMPacket_GAMEBASE_BUY_ACK : ISerializable
	{
		// Token: 0x0600956B RID: 38251 RVA: 0x0032A24C File Offset: 0x0032844C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet(ref this.productId);
			stream.PutOrGet<NKMShopPurchaseHistory>(ref this.histroy);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemData);
			stream.PutOrGet<NKMShopSubscriptionData>(ref this.subScriptionData);
			stream.PutOrGet(ref this.totalPaidAmount);
		}

		// Token: 0x0400871D RID: 34589
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400871E RID: 34590
		public NKMRewardData rewardData;

		// Token: 0x0400871F RID: 34591
		public int productId;

		// Token: 0x04008720 RID: 34592
		public NKMShopPurchaseHistory histroy = new NKMShopPurchaseHistory();

		// Token: 0x04008721 RID: 34593
		public NKMItemMiscData costItemData;

		// Token: 0x04008722 RID: 34594
		public NKMShopSubscriptionData subScriptionData = new NKMShopSubscriptionData();

		// Token: 0x04008723 RID: 34595
		public double totalPaidAmount;
	}
}
