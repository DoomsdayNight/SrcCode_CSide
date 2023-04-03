using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E17 RID: 3607
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_CHAT_LIST_ACK)]
	public sealed class NKMPacket_OFFICE_CHAT_LIST_ACK : ISerializable
	{
		// Token: 0x06009722 RID: 38690 RVA: 0x0032C8E8 File Offset: 0x0032AAE8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.userUid);
			stream.PutOrGet<NKMOfficeChatMessageData>(ref this.messages);
		}

		// Token: 0x04008936 RID: 35126
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008937 RID: 35127
		public long userUid;

		// Token: 0x04008938 RID: 35128
		public List<NKMOfficeChatMessageData> messages = new List<NKMOfficeChatMessageData>();
	}
}
