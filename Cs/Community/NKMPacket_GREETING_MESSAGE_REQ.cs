using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001003 RID: 4099
	[PacketId(ClientPacketId.kNKMPacket_GREETING_MESSAGE_REQ)]
	public sealed class NKMPacket_GREETING_MESSAGE_REQ : ISerializable
	{
		// Token: 0x06009AD6 RID: 39638 RVA: 0x00331F38 File Offset: 0x00330138
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
