using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F06 RID: 3846
	[PacketId(ClientPacketId.kNKMPacket_GUILD_CHAT_LIST_ACK)]
	public sealed class NKMPacket_GUILD_CHAT_LIST_ACK : ISerializable
	{
		// Token: 0x060098EC RID: 39148 RVA: 0x0032F31B File Offset: 0x0032D51B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet<NKMChatMessageData>(ref this.messages);
		}

		// Token: 0x04008BAC RID: 35756
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008BAD RID: 35757
		public long guildUid;

		// Token: 0x04008BAE RID: 35758
		public List<NKMChatMessageData> messages = new List<NKMChatMessageData>();
	}
}
