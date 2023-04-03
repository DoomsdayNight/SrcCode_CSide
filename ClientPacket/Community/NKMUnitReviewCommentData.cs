using System;
using Cs.Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FC8 RID: 4040
	public sealed class NKMUnitReviewCommentData : ISerializable
	{
		// Token: 0x06009A60 RID: 39520 RVA: 0x00331574 File Offset: 0x0032F774
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.commentUID);
			stream.PutOrGet(ref this.userUID);
			stream.PutOrGet(ref this.nickName);
			stream.PutOrGet(ref this.level);
			stream.PutOrGet(ref this.content);
			stream.PutOrGet(ref this.votedCount);
			stream.PutOrGet(ref this.isVoted);
			stream.PutOrGet(ref this.regDate);
		}

		// Token: 0x04008DB1 RID: 36273
		public long commentUID;

		// Token: 0x04008DB2 RID: 36274
		public long userUID;

		// Token: 0x04008DB3 RID: 36275
		public string nickName;

		// Token: 0x04008DB4 RID: 36276
		public int level;

		// Token: 0x04008DB5 RID: 36277
		public string content;

		// Token: 0x04008DB6 RID: 36278
		public int votedCount;

		// Token: 0x04008DB7 RID: 36279
		public bool isVoted;

		// Token: 0x04008DB8 RID: 36280
		public long regDate;
	}
}
