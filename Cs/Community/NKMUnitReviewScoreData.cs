using System;
using Cs.Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FC9 RID: 4041
	public sealed class NKMUnitReviewScoreData : ISerializable
	{
		// Token: 0x06009A62 RID: 39522 RVA: 0x003315E9 File Offset: 0x0032F7E9
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.avgScore);
			stream.PutOrGet(ref this.votedCount);
			stream.PutOrGet(ref this.myScore);
		}

		// Token: 0x04008DB9 RID: 36281
		public float avgScore;

		// Token: 0x04008DBA RID: 36282
		public int votedCount;

		// Token: 0x04008DBB RID: 36283
		public byte myScore;
	}
}
