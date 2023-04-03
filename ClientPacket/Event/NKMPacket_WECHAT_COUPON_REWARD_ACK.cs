using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F9D RID: 3997
	[PacketId(ClientPacketId.kNKMPacket_WECHAT_COUPON_REWARD_ACK)]
	public sealed class NKMPacket_WECHAT_COUPON_REWARD_ACK : ISerializable
	{
		// Token: 0x06009A10 RID: 39440 RVA: 0x00330E08 File Offset: 0x0032F008
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet<WechatCouponData>(ref this.data);
		}

		// Token: 0x04008D43 RID: 36163
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008D44 RID: 36164
		public NKMRewardData rewardData;

		// Token: 0x04008D45 RID: 36165
		public WechatCouponData data = new WechatCouponData();
	}
}
