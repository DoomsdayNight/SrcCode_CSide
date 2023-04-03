using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001009 RID: 4105
	[PacketId(ClientPacketId.kNKMPacket_EMOTICON_TEXT_CHANGE_REQ)]
	public sealed class NKMPacket_EMOTICON_TEXT_CHANGE_REQ : ISerializable
	{
		// Token: 0x06009AE2 RID: 39650 RVA: 0x00332002 File Offset: 0x00330202
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.presetIndex);
			stream.PutOrGet(ref this.emoticonId);
		}

		// Token: 0x04008E42 RID: 36418
		public int presetIndex;

		// Token: 0x04008E43 RID: 36419
		public int emoticonId;
	}
}
