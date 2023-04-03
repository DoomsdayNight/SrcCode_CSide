using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FFD RID: 4093
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_TAG_VOTE_CANCEL_REQ)]
	public sealed class NKMPacket_UNIT_REVIEW_TAG_VOTE_CANCEL_REQ : ISerializable
	{
		// Token: 0x06009ACA RID: 39626 RVA: 0x00331DEC File Offset: 0x0032FFEC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitID);
			stream.PutOrGet(ref this.tagType);
		}

		// Token: 0x04008E28 RID: 36392
		public int unitID;

		// Token: 0x04008E29 RID: 36393
		public short tagType;
	}
}
