using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D31 RID: 3377
	[PacketId(ClientPacketId.kNKMPacket_SHOP_BUY_BUNDLE_TAB_ACK)]
	public sealed class NKMPacket_SHOP_BUY_BUNDLE_TAB_ACK : ISerializable
	{
		// Token: 0x0600955F RID: 38239 RVA: 0x0032A0E9 File Offset: 0x003282E9
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemData);
			stream.PutOrGet<NKMShopPurchaseHistory>(ref this.histroy);
			stream.PutOrGet<NKMShopSubscriptionData>(ref this.subScriptionData);
		}

		// Token: 0x04008709 RID: 34569
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400870A RID: 34570
		public NKMRewardData rewardData;

		// Token: 0x0400870B RID: 34571
		public NKMItemMiscData costItemData;

		// Token: 0x0400870C RID: 34572
		public List<NKMShopPurchaseHistory> histroy = new List<NKMShopPurchaseHistory>();

		// Token: 0x0400870D RID: 34573
		public List<NKMShopSubscriptionData> subScriptionData = new List<NKMShopSubscriptionData>();
	}
}
