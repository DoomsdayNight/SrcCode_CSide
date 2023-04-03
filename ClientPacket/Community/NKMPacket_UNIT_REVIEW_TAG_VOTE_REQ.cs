using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FFB RID: 4091
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_TAG_VOTE_REQ)]
	public sealed class NKMPacket_UNIT_REVIEW_TAG_VOTE_REQ : ISerializable
	{
		// Token: 0x06009AC6 RID: 39622 RVA: 0x00331D91 File Offset: 0x0032FF91
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitID);
			stream.PutOrGet(ref this.tagType);
		}

		// Token: 0x04008E23 RID: 36387
		public int unitID;

		// Token: 0x04008E24 RID: 36388
		public short tagType;
	}
}
