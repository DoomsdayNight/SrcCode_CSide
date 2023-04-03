using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FFE RID: 4094
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_TAG_VOTE_CANCEL_ACK)]
	public sealed class NKMPacket_UNIT_REVIEW_TAG_VOTE_CANCEL_ACK : ISerializable
	{
		// Token: 0x06009ACC RID: 39628 RVA: 0x00331E0E File Offset: 0x0033000E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.unitID);
			stream.PutOrGet<NKMUnitReviewTagData>(ref this.unitReviewTagData);
		}

		// Token: 0x04008E2A RID: 36394
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E2B RID: 36395
		public int unitID;

		// Token: 0x04008E2C RID: 36396
		public NKMUnitReviewTagData unitReviewTagData = new NKMUnitReviewTagData();
	}
}
