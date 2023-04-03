using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Raid
{
	// Token: 0x02000D69 RID: 3433
	[PacketId(ClientPacketId.kNKMPacket_RAID_SEASON_NOT)]
	public sealed class NKMPacket_RAID_SEASON_NOT : ISerializable
	{
		// Token: 0x060095CD RID: 38349 RVA: 0x0032AB8C File Offset: 0x00328D8C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMRaidSeason>(ref this.raidSeason);
		}

		// Token: 0x040087A9 RID: 34729
		public NKMRaidSeason raidSeason = new NKMRaidSeason();
	}
}
