using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F9B RID: 3995
	[PacketId(ClientPacketId.kNKMPacket_WECHAT_COUPON_CHECK_ACK)]
	public sealed class NKMPacket_WECHAT_COUPON_CHECK_ACK : ISerializable
	{
		// Token: 0x06009A0C RID: 39436 RVA: 0x00330DB9 File Offset: 0x0032EFB9
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.zlongInfoCode);
			stream.PutOrGet<WechatCouponData>(ref this.data);
		}

		// Token: 0x04008D3F RID: 36159
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008D40 RID: 36160
		public int zlongInfoCode;

		// Token: 0x04008D41 RID: 36161
		public WechatCouponData data = new WechatCouponData();
	}
}
