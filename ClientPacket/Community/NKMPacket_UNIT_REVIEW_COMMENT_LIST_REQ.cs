using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FED RID: 4077
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_COMMENT_LIST_REQ)]
	public sealed class NKMPacket_UNIT_REVIEW_COMMENT_LIST_REQ : ISerializable
	{
		// Token: 0x06009AAA RID: 39594 RVA: 0x00331B4F File Offset: 0x0032FD4F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitID);
			stream.PutOrGet(ref this.isOrderByVotedCount);
			stream.PutOrGet(ref this.pageNumber);
		}

		// Token: 0x04008E04 RID: 36356
		public int unitID;

		// Token: 0x04008E05 RID: 36357
		public bool isOrderByVotedCount;

		// Token: 0x04008E06 RID: 36358
		public int pageNumber;
	}
}
