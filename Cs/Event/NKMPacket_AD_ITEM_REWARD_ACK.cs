using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000FAC RID: 4012
	[PacketId(ClientPacketId.kNKMPacket_AD_ITEM_REWARD_ACK)]
	public sealed class NKMPacket_AD_ITEM_REWARD_ACK : ISerializable
	{
		// Token: 0x06009A2C RID: 39468 RVA: 0x00331049 File Offset: 0x0032F249
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMADItemRewardInfo>(ref this.itemRewardInfo);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
		}

		// Token: 0x04008D66 RID: 36198
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008D67 RID: 36199
		public NKMADItemRewardInfo itemRewardInfo = new NKMADItemRewardInfo();

		// Token: 0x04008D68 RID: 36200
		public NKMRewardData rewardData;
	}
}
