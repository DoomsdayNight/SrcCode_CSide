using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FD0 RID: 4048
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_RECOMMEND_REQ)]
	public sealed class NKMPacket_FRIEND_RECOMMEND_REQ : ISerializable
	{
		// Token: 0x06009A70 RID: 39536 RVA: 0x0033176E File Offset: 0x0032F96E
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
