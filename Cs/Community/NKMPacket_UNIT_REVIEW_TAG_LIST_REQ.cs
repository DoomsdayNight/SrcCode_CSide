using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FF9 RID: 4089
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_TAG_LIST_REQ)]
	public sealed class NKMPacket_UNIT_REVIEW_TAG_LIST_REQ : ISerializable
	{
		// Token: 0x06009AC2 RID: 39618 RVA: 0x00331D4E File Offset: 0x0032FF4E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitID);
		}

		// Token: 0x04008E20 RID: 36384
		public int unitID;
	}
}
