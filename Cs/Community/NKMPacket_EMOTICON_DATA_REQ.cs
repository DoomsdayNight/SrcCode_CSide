using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001005 RID: 4101
	[PacketId(ClientPacketId.kNKMPacket_EMOTICON_DATA_REQ)]
	public sealed class NKMPacket_EMOTICON_DATA_REQ : ISerializable
	{
		// Token: 0x06009ADA RID: 39642 RVA: 0x00331F64 File Offset: 0x00330164
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
