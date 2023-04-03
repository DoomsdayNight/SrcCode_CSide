using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FF3 RID: 4083
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_COMMENT_VOTE_REQ)]
	public sealed class NKMPacket_UNIT_REVIEW_COMMENT_VOTE_REQ : ISerializable
	{
		// Token: 0x06009AB6 RID: 39606 RVA: 0x00331C3D File Offset: 0x0032FE3D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitID);
			stream.PutOrGet(ref this.commentUID);
		}

		// Token: 0x04008E11 RID: 36369
		public int unitID;

		// Token: 0x04008E12 RID: 36370
		public long commentUID;
	}
}
