using System;
using ClientPacket.Common;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F02 RID: 3842
	[PacketId(ClientPacketId.kNKMPacket_GUILD_CHAT_REQ)]
	public sealed class NKMPacket_GUILD_CHAT_REQ : ISerializable
	{
		// Token: 0x060098E4 RID: 39140 RVA: 0x0032F288 File Offset: 0x0032D488
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGetEnum<ChatMessageType>(ref this.messageType);
			stream.PutOrGet(ref this.emotionId);
			stream.PutOrGet(ref this.message);
		}

		// Token: 0x04008BA4 RID: 35748
		public long guildUid;

		// Token: 0x04008BA5 RID: 35749
		public ChatMessageType messageType;

		// Token: 0x04008BA6 RID: 35750
		public int emotionId;

		// Token: 0x04008BA7 RID: 35751
		public string message;
	}
}
