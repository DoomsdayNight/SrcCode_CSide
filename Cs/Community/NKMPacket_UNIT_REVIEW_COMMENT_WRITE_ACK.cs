using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FF0 RID: 4080
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_COMMENT_WRITE_ACK)]
	public sealed class NKMPacket_UNIT_REVIEW_COMMENT_WRITE_ACK : ISerializable
	{
		// Token: 0x06009AB0 RID: 39600 RVA: 0x00331BD8 File Offset: 0x0032FDD8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.unitID);
			stream.PutOrGet<NKMUnitReviewCommentData>(ref this.myUnitReviewCommentData);
		}

		// Token: 0x04008E0C RID: 36364
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E0D RID: 36365
		public int unitID;

		// Token: 0x04008E0E RID: 36366
		public NKMUnitReviewCommentData myUnitReviewCommentData = new NKMUnitReviewCommentData();
	}
}
