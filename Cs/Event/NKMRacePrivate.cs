using System;
using Cs.Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F8D RID: 3981
	public sealed class NKMRacePrivate : ISerializable
	{
		// Token: 0x060099F4 RID: 39412 RVA: 0x00330B99 File Offset: 0x0032ED99
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.RaceId);
			stream.PutOrGet(ref this.RaceIndex);
			stream.PutOrGetEnum<RaceTeam>(ref this.SelectTeam);
			stream.PutOrGet(ref this.racePlayCount);
		}

		// Token: 0x04008D15 RID: 36117
		public int RaceId;

		// Token: 0x04008D16 RID: 36118
		public int RaceIndex;

		// Token: 0x04008D17 RID: 36119
		public RaceTeam SelectTeam;

		// Token: 0x04008D18 RID: 36120
		public int racePlayCount;
	}
}
