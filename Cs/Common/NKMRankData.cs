using System;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x02001031 RID: 4145
	public sealed class NKMRankData : ISerializable
	{
		// Token: 0x06009B32 RID: 39730 RVA: 0x0033259C File Offset: 0x0033079C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.rank);
			stream.PutOrGet(ref this.score);
		}

		// Token: 0x04008E8D RID: 36493
		public int rank;

		// Token: 0x04008E8E RID: 36494
		public long score;
	}
}
