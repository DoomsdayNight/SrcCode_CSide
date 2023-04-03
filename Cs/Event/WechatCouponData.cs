using System;
using Cs.Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F99 RID: 3993
	public sealed class WechatCouponData : ISerializable
	{
		// Token: 0x06009A08 RID: 39432 RVA: 0x00330D75 File Offset: 0x0032EF75
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.templetId);
			stream.PutOrGetEnum<WechatCouponState>(ref this.state);
		}

		// Token: 0x04008D3B RID: 36155
		public int templetId;

		// Token: 0x04008D3C RID: 36156
		public WechatCouponState state;
	}
}
