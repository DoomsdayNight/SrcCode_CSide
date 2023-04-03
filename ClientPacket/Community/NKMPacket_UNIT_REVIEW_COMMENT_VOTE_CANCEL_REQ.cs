using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FF5 RID: 4085
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_COMMENT_VOTE_CANCEL_REQ)]
	public sealed class NKMPacket_UNIT_REVIEW_COMMENT_VOTE_CANCEL_REQ : ISerializable
	{
		// Token: 0x06009ABA RID: 39610 RVA: 0x00331C98 File Offset: 0x0032FE98
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitID);
			stream.PutOrGet(ref this.commentUID);
		}

		// Token: 0x04008E16 RID: 36374
		public int unitID;

		// Token: 0x04008E17 RID: 36375
		public long commentUID;
	}
}
