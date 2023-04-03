using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Chat
{
	// Token: 0x02001064 RID: 4196
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_CHAT_LIST_ACK)]
	public sealed class NKMPacket_PRIVATE_CHAT_LIST_ACK : ISerializable
	{
		// Token: 0x06009B85 RID: 39813 RVA: 0x003332E3 File Offset: 0x003314E3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.userUid);
			stream.PutOrGet<NKMChatMessageData>(ref this.messages);
		}

		// Token: 0x04008F77 RID: 36727
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008F78 RID: 36728
		public long userUid;

		// Token: 0x04008F79 RID: 36729
		public List<NKMChatMessageData> messages = new List<NKMChatMessageData>();
	}
}
