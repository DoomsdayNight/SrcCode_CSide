using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Negotiation
{
	// Token: 0x02000E29 RID: 3625
	[PacketId(ClientPacketId.kNKMPacket_NEGOTIATE_NOT_USED_01)]
	public sealed class NKMPacket_NEGOTIATE_NOT_USED_01 : ISerializable
	{
		// Token: 0x06009742 RID: 38722 RVA: 0x0032CBD5 File Offset: 0x0032ADD5
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
