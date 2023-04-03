using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.LeaderBoard
{
	// Token: 0x02000E7A RID: 3706
	[PacketId(ClientPacketId.kNKMPacket_LEADERBOARD_FIERCE_LIST_ACK)]
	public sealed class NKMPacket_LEADERBOARD_FIERCE_LIST_ACK : ISerializable
	{
		// Token: 0x060097E2 RID: 38882 RVA: 0x0032D9A3 File Offset: 0x0032BBA3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMLeaderBoardFierceData>(ref this.leaderBoardfierceData);
			stream.PutOrGet(ref this.userRank);
			stream.PutOrGet(ref this.fierceId);
			stream.PutOrGet(ref this.isAll);
		}

		// Token: 0x04008A1A RID: 35354
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008A1B RID: 35355
		public NKMLeaderBoardFierceData leaderBoardfierceData = new NKMLeaderBoardFierceData();

		// Token: 0x04008A1C RID: 35356
		public int userRank;

		// Token: 0x04008A1D RID: 35357
		public int fierceId;

		// Token: 0x04008A1E RID: 35358
		public bool isAll;
	}
}
