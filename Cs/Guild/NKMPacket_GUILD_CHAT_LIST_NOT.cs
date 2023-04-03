using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F07 RID: 3847
	[PacketId(ClientPacketId.kNKMPacket_GUILD_CHAT_LIST_NOT)]
	public sealed class NKMPacket_GUILD_CHAT_LIST_NOT : ISerializable
	{
		// Token: 0x060098EE RID: 39150 RVA: 0x0032F354 File Offset: 0x0032D554
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet<NKMChatMessageData>(ref this.messages);
		}

		// Token: 0x04008BAF RID: 35759
		public long guildUid;

		// Token: 0x04008BB0 RID: 35760
		public List<NKMChatMessageData> messages = new List<NKMChatMessageData>();
	}
}
