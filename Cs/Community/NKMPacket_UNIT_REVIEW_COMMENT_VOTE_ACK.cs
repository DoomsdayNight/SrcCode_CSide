using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FF4 RID: 4084
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_COMMENT_VOTE_ACK)]
	public sealed class NKMPacket_UNIT_REVIEW_COMMENT_VOTE_ACK : ISerializable
	{
		// Token: 0x06009AB8 RID: 39608 RVA: 0x00331C5F File Offset: 0x0032FE5F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.unitID);
			stream.PutOrGet<NKMUnitReviewCommentData>(ref this.unitReviewCommentData);
		}

		// Token: 0x04008E13 RID: 36371
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E14 RID: 36372
		public int unitID;

		// Token: 0x04008E15 RID: 36373
		public NKMUnitReviewCommentData unitReviewCommentData = new NKMUnitReviewCommentData();
	}
}
