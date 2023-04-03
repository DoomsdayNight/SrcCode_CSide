using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.LeaderBoard
{
	// Token: 0x02000E82 RID: 3714
	[PacketId(ClientPacketId.kNKMPacket_LEADERBOARD_TIMEATTACK_LIST_ACK)]
	public sealed class NKMPacket_LEADERBOARD_TIMEATTACK_LIST_ACK : ISerializable
	{
		// Token: 0x060097F2 RID: 38898 RVA: 0x0032DB3E File Offset: 0x0032BD3E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMLeaderBoardTimeAttackData>(ref this.leaderBoardTimeAttackData);
			stream.PutOrGet(ref this.userRank);
			stream.PutOrGet(ref this.stageId);
			stream.PutOrGet(ref this.isAll);
		}

		// Token: 0x04008A2F RID: 35375
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008A30 RID: 35376
		public NKMLeaderBoardTimeAttackData leaderBoardTimeAttackData = new NKMLeaderBoardTimeAttackData();

		// Token: 0x04008A31 RID: 35377
		public int userRank;

		// Token: 0x04008A32 RID: 35378
		public int stageId;

		// Token: 0x04008A33 RID: 35379
		public bool isAll;
	}
}
