using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.LeaderBoard
{
	// Token: 0x02000E7C RID: 3708
	[PacketId(ClientPacketId.kNKMPacket_LEADERBOARD_FIERCE_BOSSGROUP_LIST_ACK)]
	public sealed class NKMPacket_LEADERBOARD_FIERCE_BOSSGROUP_LIST_ACK : ISerializable
	{
		// Token: 0x060097E6 RID: 38886 RVA: 0x0032DA18 File Offset: 0x0032BC18
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMLeaderBoardFierceData>(ref this.leaderBoardfierceData);
			stream.PutOrGet(ref this.userRank);
			stream.PutOrGet(ref this.fierceId);
			stream.PutOrGet(ref this.fierceBossGroupId);
			stream.PutOrGet(ref this.isAll);
		}

		// Token: 0x04008A21 RID: 35361
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008A22 RID: 35362
		public NKMLeaderBoardFierceData leaderBoardfierceData = new NKMLeaderBoardFierceData();

		// Token: 0x04008A23 RID: 35363
		public int userRank;

		// Token: 0x04008A24 RID: 35364
		public int fierceId;

		// Token: 0x04008A25 RID: 35365
		public int fierceBossGroupId;

		// Token: 0x04008A26 RID: 35366
		public bool isAll;
	}
}
