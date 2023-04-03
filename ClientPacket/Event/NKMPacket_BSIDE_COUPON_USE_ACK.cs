using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000FB0 RID: 4016
	[PacketId(ClientPacketId.kNKMPacket_BSIDE_COUPON_USE_ACK)]
	public sealed class NKMPacket_BSIDE_COUPON_USE_ACK : ISerializable
	{
		// Token: 0x06009A34 RID: 39476 RVA: 0x003310DC File Offset: 0x0032F2DC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008D6E RID: 36206
		public NKM_ERROR_CODE errorCode;
	}
}
