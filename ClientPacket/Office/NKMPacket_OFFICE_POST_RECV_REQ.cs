using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E09 RID: 3593
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_POST_RECV_REQ)]
	public sealed class NKMPacket_OFFICE_POST_RECV_REQ : ISerializable
	{
		// Token: 0x06009706 RID: 38662 RVA: 0x0032C6CD File Offset: 0x0032A8CD
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
