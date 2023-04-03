using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Negotiation
{
	// Token: 0x02000E28 RID: 3624
	[PacketId(ClientPacketId.kNKMPacket_NEGOTIATE_NOT_USED_00)]
	public sealed class NKMPacket_NEGOTIATE_NOT_USED_00 : ISerializable
	{
		// Token: 0x06009740 RID: 38720 RVA: 0x0032CBCB File Offset: 0x0032ADCB
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
