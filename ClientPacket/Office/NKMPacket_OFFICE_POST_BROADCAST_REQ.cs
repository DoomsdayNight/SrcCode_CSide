using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E0B RID: 3595
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_POST_BROADCAST_REQ)]
	public sealed class NKMPacket_OFFICE_POST_BROADCAST_REQ : ISerializable
	{
		// Token: 0x0600970A RID: 38666 RVA: 0x0032C733 File Offset: 0x0032A933
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
