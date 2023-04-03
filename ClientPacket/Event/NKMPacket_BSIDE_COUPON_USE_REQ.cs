using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000FAF RID: 4015
	[PacketId(ClientPacketId.kNKMPacket_BSIDE_COUPON_USE_REQ)]
	public sealed class NKMPacket_BSIDE_COUPON_USE_REQ : ISerializable
	{
		// Token: 0x06009A32 RID: 39474 RVA: 0x003310C6 File Offset: 0x0032F2C6
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.couponCode);
		}

		// Token: 0x04008D6D RID: 36205
		public string couponCode;
	}
}
