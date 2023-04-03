using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FFC RID: 4092
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_TAG_VOTE_ACK)]
	public sealed class NKMPacket_UNIT_REVIEW_TAG_VOTE_ACK : ISerializable
	{
		// Token: 0x06009AC8 RID: 39624 RVA: 0x00331DB3 File Offset: 0x0032FFB3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.unitID);
			stream.PutOrGet<NKMUnitReviewTagData>(ref this.unitReviewTagData);
		}

		// Token: 0x04008E25 RID: 36389
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E26 RID: 36390
		public int unitID;

		// Token: 0x04008E27 RID: 36391
		public NKMUnitReviewTagData unitReviewTagData = new NKMUnitReviewTagData();
	}
}
