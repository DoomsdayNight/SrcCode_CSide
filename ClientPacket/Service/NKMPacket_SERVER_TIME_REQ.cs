using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Service
{
	// Token: 0x02000D42 RID: 3394
	[PacketId(ClientPacketId.kNKMPacket_SERVER_TIME_REQ)]
	public sealed class NKMPacket_SERVER_TIME_REQ : ISerializable
	{
		// Token: 0x06009581 RID: 38273 RVA: 0x0032A44E File Offset: 0x0032864E
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
