using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Negotiation
{
	// Token: 0x02000E2B RID: 3627
	[PacketId(ClientPacketId.kNKMPacket_NEGOTIATE_NOT_USED_03)]
	public sealed class NKMPacket_NEGOTIATE_NOT_USED_03 : ISerializable
	{
		// Token: 0x06009746 RID: 38726 RVA: 0x0032CBE9 File Offset: 0x0032ADE9
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
