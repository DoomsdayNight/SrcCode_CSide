using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.LeaderBoard
{
	// Token: 0x02000E7F RID: 3711
	[PacketId(ClientPacketId.kNKMPacket_LEADERBOARD_GUILD_LEVEL_RANK_LIST_REQ)]
	public sealed class NKMPacket_LEADERBOARD_GUILD_LEVEL_RANK_LIST_REQ : ISerializable
	{
		// Token: 0x060097EC RID: 38892 RVA: 0x0032DADA File Offset: 0x0032BCDA
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
