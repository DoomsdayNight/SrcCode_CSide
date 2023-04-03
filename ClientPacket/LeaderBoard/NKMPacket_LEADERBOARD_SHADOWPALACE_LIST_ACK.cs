using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.LeaderBoard
{
	// Token: 0x02000E78 RID: 3704
	[PacketId(ClientPacketId.kNKMPacket_LEADERBOARD_SHADOWPALACE_LIST_ACK)]
	public sealed class NKMPacket_LEADERBOARD_SHADOWPALACE_LIST_ACK : ISerializable
	{
		// Token: 0x060097DE RID: 38878 RVA: 0x0032D93C File Offset: 0x0032BB3C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMLeaderBoardShadowPalaceData>(ref this.leaderBoardShadowPalaceData);
			stream.PutOrGet(ref this.userRank);
			stream.PutOrGet(ref this.actId);
			stream.PutOrGet(ref this.isAll);
		}

		// Token: 0x04008A14 RID: 35348
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008A15 RID: 35349
		public NKMLeaderBoardShadowPalaceData leaderBoardShadowPalaceData = new NKMLeaderBoardShadowPalaceData();

		// Token: 0x04008A16 RID: 35350
		public int userRank;

		// Token: 0x04008A17 RID: 35351
		public int actId;

		// Token: 0x04008A18 RID: 35352
		public bool isAll;
	}
}
