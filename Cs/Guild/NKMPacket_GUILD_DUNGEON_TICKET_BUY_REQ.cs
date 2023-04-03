using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F22 RID: 3874
	[PacketId(ClientPacketId.kNKMPacket_GUILD_DUNGEON_TICKET_BUY_REQ)]
	public sealed class NKMPacket_GUILD_DUNGEON_TICKET_BUY_REQ : ISerializable
	{
		// Token: 0x06009924 RID: 39204 RVA: 0x0032F8EF File Offset: 0x0032DAEF
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
