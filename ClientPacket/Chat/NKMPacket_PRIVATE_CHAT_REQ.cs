using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Chat
{
	// Token: 0x02001060 RID: 4192
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_CHAT_REQ)]
	public sealed class NKMPacket_PRIVATE_CHAT_REQ : ISerializable
	{
		// Token: 0x06009B7D RID: 39805 RVA: 0x0033325C File Offset: 0x0033145C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.userUid);
			stream.PutOrGet(ref this.emotionId);
			stream.PutOrGet(ref this.message);
		}

		// Token: 0x04008F70 RID: 36720
		public long userUid;

		// Token: 0x04008F71 RID: 36721
		public int emotionId;

		// Token: 0x04008F72 RID: 36722
		public string message;
	}
}
