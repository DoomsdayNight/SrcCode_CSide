using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D33 RID: 3379
	[PacketId(ClientPacketId.kNKMPacket_ZLONG_USE_COUPON_REQ)]
	public sealed class NKMPacket_ZLONG_USE_COUPON_REQ : ISerializable
	{
		// Token: 0x06009563 RID: 38243 RVA: 0x0032A1A1 File Offset: 0x003283A1
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.couponCode);
		}

		// Token: 0x04008713 RID: 34579
		public string couponCode;
	}
}
