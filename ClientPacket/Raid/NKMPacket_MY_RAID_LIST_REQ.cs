using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Raid
{
	// Token: 0x02000D5B RID: 3419
	[PacketId(ClientPacketId.kNKMPacket_MY_RAID_LIST_REQ)]
	public sealed class NKMPacket_MY_RAID_LIST_REQ : ISerializable
	{
		// Token: 0x060095B1 RID: 38321 RVA: 0x0032A9B6 File Offset: 0x00328BB6
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
