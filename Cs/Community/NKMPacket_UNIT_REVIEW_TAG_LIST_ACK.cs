using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FFA RID: 4090
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_TAG_LIST_ACK)]
	public sealed class NKMPacket_UNIT_REVIEW_TAG_LIST_ACK : ISerializable
	{
		// Token: 0x06009AC4 RID: 39620 RVA: 0x00331D64 File Offset: 0x0032FF64
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMUnitReviewTagData>(ref this.unitReviewTagDataList);
		}

		// Token: 0x04008E21 RID: 36385
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E22 RID: 36386
		public List<NKMUnitReviewTagData> unitReviewTagDataList = new List<NKMUnitReviewTagData>();
	}
}
