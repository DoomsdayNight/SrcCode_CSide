using System;
using ClientPacket.Common;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.LeaderBoard
{
	// Token: 0x02000E80 RID: 3712
	[PacketId(ClientPacketId.kNKMPacket_LEADERBOARD_GUILD_LEVEL_RANK_LIST_ACK)]
	public sealed class NKMPacket_LEADERBOARD_GUILD_LEVEL_RANK_LIST_ACK : ISerializable
	{
		// Token: 0x060097EE RID: 38894 RVA: 0x0032DAE4 File Offset: 0x0032BCE4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMLeaderBoardGuildData>(ref this.leaderBoard);
			stream.PutOrGet<NKMRankData>(ref this.myRankData);
		}

		// Token: 0x04008A2B RID: 35371
		public NKMLeaderBoardGuildData leaderBoard = new NKMLeaderBoardGuildData();

		// Token: 0x04008A2C RID: 35372
		public NKMRankData myRankData = new NKMRankData();
	}
}
