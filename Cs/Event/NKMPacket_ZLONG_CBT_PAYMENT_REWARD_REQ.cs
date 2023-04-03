using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000FA2 RID: 4002
	[PacketId(ClientPacketId.kNKMPacket_ZLONG_CBT_PAYMENT_REWARD_REQ)]
	public sealed class NKMPacket_ZLONG_CBT_PAYMENT_REWARD_REQ : ISerializable
	{
		// Token: 0x06009A18 RID: 39448 RVA: 0x00330EA6 File Offset: 0x0032F0A6
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
