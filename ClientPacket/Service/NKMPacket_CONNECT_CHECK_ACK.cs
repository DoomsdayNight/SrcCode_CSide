using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Service
{
	// Token: 0x02000D41 RID: 3393
	[PacketId(ClientPacketId.kNKMPacket_CONNECT_CHECK_ACK)]
	public sealed class NKMPacket_CONNECT_CHECK_ACK : ISerializable
	{
		// Token: 0x0600957F RID: 38271 RVA: 0x0032A444 File Offset: 0x00328644
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
