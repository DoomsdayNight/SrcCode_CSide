using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Service
{
	// Token: 0x02000D40 RID: 3392
	[PacketId(ClientPacketId.kNKMPacket_CONNECT_CHECK_REQ)]
	public sealed class NKMPacket_CONNECT_CHECK_REQ : ISerializable
	{
		// Token: 0x0600957D RID: 38269 RVA: 0x0032A43A File Offset: 0x0032863A
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
