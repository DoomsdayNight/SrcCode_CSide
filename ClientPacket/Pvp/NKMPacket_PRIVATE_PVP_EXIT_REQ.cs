using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DC5 RID: 3525
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_PVP_EXIT_REQ)]
	public sealed class NKMPacket_PRIVATE_PVP_EXIT_REQ : ISerializable
	{
		// Token: 0x06009683 RID: 38531 RVA: 0x0032BA34 File Offset: 0x00329C34
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
