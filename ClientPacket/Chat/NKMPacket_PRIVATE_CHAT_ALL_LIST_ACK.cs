using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Chat
{
	// Token: 0x02001067 RID: 4199
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_CHAT_ALL_LIST_ACK)]
	public sealed class NKMPacket_PRIVATE_CHAT_ALL_LIST_ACK : ISerializable
	{
		// Token: 0x06009B8B RID: 39819 RVA: 0x0033335E File Offset: 0x0033155E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<PrivateChatListData>(ref this.friends);
			stream.PutOrGet<PrivateChatListData>(ref this.guildMembers);
		}

		// Token: 0x04008F7C RID: 36732
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008F7D RID: 36733
		public List<PrivateChatListData> friends = new List<PrivateChatListData>();

		// Token: 0x04008F7E RID: 36734
		public List<PrivateChatListData> guildMembers = new List<PrivateChatListData>();
	}
}
