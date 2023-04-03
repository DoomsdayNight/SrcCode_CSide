using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D35 RID: 3381
	[PacketId(ClientPacketId.kNKMPacket_ZLONG_USE_COUPON_REQ2)]
	public sealed class NKMPacket_ZLONG_USE_COUPON_REQ2 : ISerializable
	{
		// Token: 0x06009567 RID: 38247 RVA: 0x0032A1E5 File Offset: 0x003283E5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.couponCode);
			stream.PutOrGet(ref this.zlongServerId);
		}

		// Token: 0x04008717 RID: 34583
		public string couponCode;

		// Token: 0x04008718 RID: 34584
		public int zlongServerId;
	}
}
