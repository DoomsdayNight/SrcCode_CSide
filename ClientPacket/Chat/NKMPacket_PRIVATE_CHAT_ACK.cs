using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Chat
{
	// Token: 0x02001061 RID: 4193
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_CHAT_ACK)]
	public sealed class NKMPacket_PRIVATE_CHAT_ACK : ISerializable
	{
		// Token: 0x06009B7F RID: 39807 RVA: 0x0033328A File Offset: 0x0033148A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.messageUid);
		}

		// Token: 0x04008F73 RID: 36723
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008F74 RID: 36724
		public long messageUid;
	}
}
