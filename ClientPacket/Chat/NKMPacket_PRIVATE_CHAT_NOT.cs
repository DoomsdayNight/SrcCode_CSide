using System;
using ClientPacket.Common;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Chat
{
	// Token: 0x02001062 RID: 4194
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_CHAT_NOT)]
	public sealed class NKMPacket_PRIVATE_CHAT_NOT : ISerializable
	{
		// Token: 0x06009B81 RID: 39809 RVA: 0x003332AC File Offset: 0x003314AC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMChatMessageData>(ref this.message);
		}

		// Token: 0x04008F75 RID: 36725
		public NKMChatMessageData message = new NKMChatMessageData();
	}
}
