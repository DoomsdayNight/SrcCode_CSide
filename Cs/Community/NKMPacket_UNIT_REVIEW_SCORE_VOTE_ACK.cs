using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FF8 RID: 4088
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_SCORE_VOTE_ACK)]
	public sealed class NKMPacket_UNIT_REVIEW_SCORE_VOTE_ACK : ISerializable
	{
		// Token: 0x06009AC0 RID: 39616 RVA: 0x00331D15 File Offset: 0x0032FF15
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.unitID);
			stream.PutOrGet<NKMUnitReviewScoreData>(ref this.unitReviewScoreData);
		}

		// Token: 0x04008E1D RID: 36381
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E1E RID: 36382
		public int unitID;

		// Token: 0x04008E1F RID: 36383
		public NKMUnitReviewScoreData unitReviewScoreData = new NKMUnitReviewScoreData();
	}
}
