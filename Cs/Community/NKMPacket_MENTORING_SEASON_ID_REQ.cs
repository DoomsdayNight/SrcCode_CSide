using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001029 RID: 4137
	[PacketId(ClientPacketId.kNKMPacket_MENTORING_SEASON_ID_REQ)]
	public sealed class NKMPacket_MENTORING_SEASON_ID_REQ : ISerializable
	{
		// Token: 0x06009B22 RID: 39714 RVA: 0x00332438 File Offset: 0x00330638
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
