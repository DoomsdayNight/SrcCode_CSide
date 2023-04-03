using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Raid
{
	// Token: 0x02000D61 RID: 3425
	[PacketId(ClientPacketId.kNKMPacket_RAID_DETAIL_INFO_REQ)]
	public sealed class NKMPacket_RAID_DETAIL_INFO_REQ : ISerializable
	{
		// Token: 0x060095BD RID: 38333 RVA: 0x0032AA73 File Offset: 0x00328C73
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.raidUID);
		}

		// Token: 0x0400879B RID: 34715
		public long raidUID;
	}
}
