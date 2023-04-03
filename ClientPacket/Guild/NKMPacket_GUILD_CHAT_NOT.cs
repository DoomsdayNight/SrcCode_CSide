using System;
using ClientPacket.Common;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F04 RID: 3844
	[PacketId(ClientPacketId.kNKMPacket_GUILD_CHAT_NOT)]
	public sealed class NKMPacket_GUILD_CHAT_NOT : ISerializable
	{
		// Token: 0x060098E8 RID: 39144 RVA: 0x0032F2E4 File Offset: 0x0032D4E4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMChatMessageData>(ref this.message);
		}

		// Token: 0x04008BAA RID: 35754
		public NKMChatMessageData message = new NKMChatMessageData();
	}
}
