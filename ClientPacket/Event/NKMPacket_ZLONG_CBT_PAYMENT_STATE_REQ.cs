using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000FA0 RID: 4000
	[PacketId(ClientPacketId.kNKMPacket_ZLONG_CBT_PAYMENT_STATE_REQ)]
	public sealed class NKMPacket_ZLONG_CBT_PAYMENT_STATE_REQ : ISerializable
	{
		// Token: 0x06009A14 RID: 39444 RVA: 0x00330E6F File Offset: 0x0032F06F
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
