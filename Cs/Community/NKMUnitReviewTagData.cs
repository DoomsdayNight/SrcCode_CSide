using System;
using Cs.Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FCA RID: 4042
	public sealed class NKMUnitReviewTagData : ISerializable
	{
		// Token: 0x06009A64 RID: 39524 RVA: 0x00331617 File Offset: 0x0032F817
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.tagType);
			stream.PutOrGet(ref this.votedCount);
			stream.PutOrGet(ref this.isVoted);
		}

		// Token: 0x04008DBC RID: 36284
		public short tagType;

		// Token: 0x04008DBD RID: 36285
		public int votedCount;

		// Token: 0x04008DBE RID: 36286
		public bool isVoted;
	}
}
