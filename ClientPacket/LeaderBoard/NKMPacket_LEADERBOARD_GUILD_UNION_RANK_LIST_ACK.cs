using System;
using ClientPacket.Common;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.LeaderBoard
{
	// Token: 0x02000E7E RID: 3710
	[PacketId(ClientPacketId.kNKMPacket_LEADERBOARD_GUILD_UNION_RANK_LIST_ACK)]
	public sealed class NKMPacket_LEADERBOARD_GUILD_UNION_RANK_LIST_ACK : ISerializable
	{
		// Token: 0x060097EA RID: 38890 RVA: 0x0032DA96 File Offset: 0x0032BC96
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.seasonId);
			stream.PutOrGet<NKMLeaderBoardGuildData>(ref this.leaderBoard);
			stream.PutOrGet<NKMRankData>(ref this.myRankData);
		}

		// Token: 0x04008A28 RID: 35368
		public int seasonId;

		// Token: 0x04008A29 RID: 35369
		public NKMLeaderBoardGuildData leaderBoard = new NKMLeaderBoardGuildData();

		// Token: 0x04008A2A RID: 35370
		public NKMRankData myRankData = new NKMRankData();
	}
}
