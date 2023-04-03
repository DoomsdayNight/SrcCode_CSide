using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Raid
{
	// Token: 0x02000D5D RID: 3421
	[PacketId(ClientPacketId.kNKMPacket_RAID_COOP_LIST_REQ)]
	public sealed class NKMPacket_RAID_COOP_LIST_REQ : ISerializable
	{
		// Token: 0x060095B5 RID: 38325 RVA: 0x0032A9ED File Offset: 0x00328BED
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
