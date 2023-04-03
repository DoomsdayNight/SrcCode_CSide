using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Negotiation
{
	// Token: 0x02000E2A RID: 3626
	[PacketId(ClientPacketId.kNKMPacket_NEGOTIATE_NOT_USED_02)]
	public sealed class NKMPacket_NEGOTIATE_NOT_USED_02 : ISerializable
	{
		// Token: 0x06009744 RID: 38724 RVA: 0x0032CBDF File Offset: 0x0032ADDF
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
