using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001000 RID: 4096
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_COMMENT_AND_SCORE_ACK)]
	public sealed class NKMPacket_UNIT_REVIEW_COMMENT_AND_SCORE_ACK : ISerializable
	{
		// Token: 0x06009AD0 RID: 39632 RVA: 0x00331E78 File Offset: 0x00330078
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.unitID);
			stream.PutOrGet<NKMUnitReviewCommentData>(ref this.bestUnitReviewCommentDataList);
			stream.PutOrGet<NKMUnitReviewCommentData>(ref this.unitReviewCommentDataList);
			stream.PutOrGet<NKMUnitReviewCommentData>(ref this.myUnitReviewCommentData);
			stream.PutOrGet<NKMUnitReviewScoreData>(ref this.unitReviewScoreData);
		}

		// Token: 0x04008E30 RID: 36400
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E31 RID: 36401
		public int unitID;

		// Token: 0x04008E32 RID: 36402
		public List<NKMUnitReviewCommentData> bestUnitReviewCommentDataList = new List<NKMUnitReviewCommentData>();

		// Token: 0x04008E33 RID: 36403
		public List<NKMUnitReviewCommentData> unitReviewCommentDataList = new List<NKMUnitReviewCommentData>();

		// Token: 0x04008E34 RID: 36404
		public NKMUnitReviewCommentData myUnitReviewCommentData = new NKMUnitReviewCommentData();

		// Token: 0x04008E35 RID: 36405
		public NKMUnitReviewScoreData unitReviewScoreData = new NKMUnitReviewScoreData();
	}
}
