using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.LeaderBoard
{
	// Token: 0x02000E7D RID: 3709
	[PacketId(ClientPacketId.kNKMPacket_LEADERBOARD_GUILD_UNION_RANK_LIST_REQ)]
	public sealed class NKMPacket_LEADERBOARD_GUILD_UNION_RANK_LIST_REQ : ISerializable
	{
		// Token: 0x060097E8 RID: 38888 RVA: 0x0032DA80 File Offset: 0x0032BC80
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.seasonId);
		}

		// Token: 0x04008A27 RID: 35367
		public int seasonId;
	}
}
