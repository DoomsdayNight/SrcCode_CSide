using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Chat
{
	// Token: 0x02001065 RID: 4197
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_CHAT_ALL_LIST_REQ)]
	public sealed class NKMPacket_PRIVATE_CHAT_ALL_LIST_REQ : ISerializable
	{
		// Token: 0x06009B87 RID: 39815 RVA: 0x0033331C File Offset: 0x0033151C
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
