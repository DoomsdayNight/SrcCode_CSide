using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FF6 RID: 4086
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_COMMENT_VOTE_CANCEL_ACK)]
	public sealed class NKMPacket_UNIT_REVIEW_COMMENT_VOTE_CANCEL_ACK : ISerializable
	{
		// Token: 0x06009ABC RID: 39612 RVA: 0x00331CBA File Offset: 0x0032FEBA
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.unitID);
			stream.PutOrGet<NKMUnitReviewCommentData>(ref this.unitReviewCommentData);
		}

		// Token: 0x04008E18 RID: 36376
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E19 RID: 36377
		public int unitID;

		// Token: 0x04008E1A RID: 36378
		public NKMUnitReviewCommentData unitReviewCommentData = new NKMUnitReviewCommentData();
	}
}
