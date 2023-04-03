using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001007 RID: 4103
	[PacketId(ClientPacketId.kNKMPacket_EMOTICON_ANI_CHANGE_REQ)]
	public sealed class NKMPacket_EMOTICON_ANI_CHANGE_REQ : ISerializable
	{
		// Token: 0x06009ADE RID: 39646 RVA: 0x00331FB2 File Offset: 0x003301B2
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.presetIndex);
			stream.PutOrGet(ref this.emoticonId);
		}

		// Token: 0x04008E3D RID: 36413
		public int presetIndex;

		// Token: 0x04008E3E RID: 36414
		public int emoticonId;
	}
}
