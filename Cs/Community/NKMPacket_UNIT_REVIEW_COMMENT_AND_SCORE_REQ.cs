using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FFF RID: 4095
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_COMMENT_AND_SCORE_REQ)]
	public sealed class NKMPacket_UNIT_REVIEW_COMMENT_AND_SCORE_REQ : ISerializable
	{
		// Token: 0x06009ACE RID: 39630 RVA: 0x00331E47 File Offset: 0x00330047
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitID);
			stream.PutOrGet(ref this.isOrderByVotedCount);
			stream.PutOrGet(ref this.pageNumber);
		}

		// Token: 0x04008E2D RID: 36397
		public int unitID;

		// Token: 0x04008E2E RID: 36398
		public bool isOrderByVotedCount;

		// Token: 0x04008E2F RID: 36399
		public int pageNumber;
	}
}
