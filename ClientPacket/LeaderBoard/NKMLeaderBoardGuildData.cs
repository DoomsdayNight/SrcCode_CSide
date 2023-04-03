using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.LeaderBoard
{
	// Token: 0x02000E72 RID: 3698
	public sealed class NKMLeaderBoardGuildData : ISerializable
	{
		// Token: 0x060097D2 RID: 38866 RVA: 0x0032D839 File Offset: 0x0032BA39
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMGuildRankData>(ref this.rankDatas);
		}

		// Token: 0x04008A08 RID: 35336
		public List<NKMGuildRankData> rankDatas = new List<NKMGuildRankData>();
	}
}
