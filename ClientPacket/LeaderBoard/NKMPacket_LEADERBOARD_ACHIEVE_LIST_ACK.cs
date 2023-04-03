using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.LeaderBoard
{
	// Token: 0x02000E76 RID: 3702
	[PacketId(ClientPacketId.kNKMPacket_LEADERBOARD_ACHIEVE_LIST_ACK)]
	public sealed class NKMPacket_LEADERBOARD_ACHIEVE_LIST_ACK : ISerializable
	{
		// Token: 0x060097DA RID: 38874 RVA: 0x0032D8D5 File Offset: 0x0032BAD5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMLeaderBoardAchieveData>(ref this.leaderBoardAchieveData);
			stream.PutOrGet(ref this.userRank);
			stream.PutOrGet(ref this.isAll);
		}

		// Token: 0x04008A0E RID: 35342
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008A0F RID: 35343
		public NKMLeaderBoardAchieveData leaderBoardAchieveData = new NKMLeaderBoardAchieveData();

		// Token: 0x04008A10 RID: 35344
		public int userRank;

		// Token: 0x04008A11 RID: 35345
		public bool isAll;
	}
}
