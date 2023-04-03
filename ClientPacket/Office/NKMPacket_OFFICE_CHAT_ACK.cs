using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E14 RID: 3604
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_CHAT_ACK)]
	public sealed class NKMPacket_OFFICE_CHAT_ACK : ISerializable
	{
		// Token: 0x0600971C RID: 38684 RVA: 0x0032C878 File Offset: 0x0032AA78
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.messageUid);
			stream.PutOrGet<NKMOfficeChatMessageData>(ref this.messages);
		}

		// Token: 0x04008931 RID: 35121
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008932 RID: 35122
		public long messageUid;

		// Token: 0x04008933 RID: 35123
		public List<NKMOfficeChatMessageData> messages = new List<NKMOfficeChatMessageData>();
	}
}
