using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Raid
{
	// Token: 0x02000D5F RID: 3423
	[PacketId(ClientPacketId.kNKMPacket_RAID_SET_COOP_REQ)]
	public sealed class NKMPacket_RAID_SET_COOP_REQ : ISerializable
	{
		// Token: 0x060095B9 RID: 38329 RVA: 0x0032AA24 File Offset: 0x00328C24
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.raidUID);
		}

		// Token: 0x04008797 RID: 34711
		public long raidUID;
	}
}
