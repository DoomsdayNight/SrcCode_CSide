using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D73 RID: 3443
	[PacketId(ClientPacketId.kNKMPacket_PVP_GAME_MATCH_CANCEL_REQ)]
	public sealed class NKMPacket_PVP_GAME_MATCH_CANCEL_REQ : ISerializable
	{
		// Token: 0x060095E1 RID: 38369 RVA: 0x0032AF30 File Offset: 0x00329130
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
