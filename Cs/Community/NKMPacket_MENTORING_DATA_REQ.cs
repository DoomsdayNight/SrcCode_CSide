using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001013 RID: 4115
	[PacketId(ClientPacketId.kNKMPacket_MENTORING_DATA_REQ)]
	public sealed class NKMPacket_MENTORING_DATA_REQ : ISerializable
	{
		// Token: 0x06009AF6 RID: 39670 RVA: 0x00332131 File Offset: 0x00330331
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
