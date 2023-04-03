using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Chat
{
	// Token: 0x02001063 RID: 4195
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_CHAT_LIST_REQ)]
	public sealed class NKMPacket_PRIVATE_CHAT_LIST_REQ : ISerializable
	{
		// Token: 0x06009B83 RID: 39811 RVA: 0x003332CD File Offset: 0x003314CD
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.userUid);
		}

		// Token: 0x04008F76 RID: 36726
		public long userUid;
	}
}
