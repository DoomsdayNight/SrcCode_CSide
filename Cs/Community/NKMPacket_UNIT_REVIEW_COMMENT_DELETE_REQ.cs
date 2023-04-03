using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FF1 RID: 4081
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_COMMENT_DELETE_REQ)]
	public sealed class NKMPacket_UNIT_REVIEW_COMMENT_DELETE_REQ : ISerializable
	{
		// Token: 0x06009AB2 RID: 39602 RVA: 0x00331C11 File Offset: 0x0032FE11
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitID);
		}

		// Token: 0x04008E0F RID: 36367
		public int unitID;
	}
}
