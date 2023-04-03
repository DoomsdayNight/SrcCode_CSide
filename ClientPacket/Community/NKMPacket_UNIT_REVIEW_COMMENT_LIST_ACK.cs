using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FEE RID: 4078
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_COMMENT_LIST_ACK)]
	public sealed class NKMPacket_UNIT_REVIEW_COMMENT_LIST_ACK : ISerializable
	{
		// Token: 0x06009AAC RID: 39596 RVA: 0x00331B7D File Offset: 0x0032FD7D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMUnitReviewCommentData>(ref this.unitReviewCommentDataList);
		}

		// Token: 0x04008E07 RID: 36359
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E08 RID: 36360
		public List<NKMUnitReviewCommentData> unitReviewCommentDataList = new List<NKMUnitReviewCommentData>();
	}
}
