using System;
using ClientPacket.LeaderBoard;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DA4 RID: 3492
	[PacketId(ClientPacketId.kNKMPacket_LEAGUE_PVP_RANK_LIST_REQ)]
	public sealed class NKMPacket_LEAGUE_PVP_RANK_LIST_REQ : ISerializable
	{
		// Token: 0x06009641 RID: 38465 RVA: 0x0032B66E File Offset: 0x0032986E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<RANK_TYPE>(ref this.rankType);
			stream.PutOrGetEnum<LeaderBoardRangeType>(ref this.range);
		}

		// Token: 0x0400884A RID: 34890
		public RANK_TYPE rankType;

		// Token: 0x0400884B RID: 34891
		public LeaderBoardRangeType range;
	}
}
