using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FEF RID: 4079
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_COMMENT_WRITE_REQ)]
	public sealed class NKMPacket_UNIT_REVIEW_COMMENT_WRITE_REQ : ISerializable
	{
		// Token: 0x06009AAE RID: 39598 RVA: 0x00331BAA File Offset: 0x0032FDAA
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitID);
			stream.PutOrGet(ref this.content);
			stream.PutOrGet(ref this.isRewrite);
		}

		// Token: 0x04008E09 RID: 36361
		public int unitID;

		// Token: 0x04008E0A RID: 36362
		public string content;

		// Token: 0x04008E0B RID: 36363
		public bool isRewrite;
	}
}
