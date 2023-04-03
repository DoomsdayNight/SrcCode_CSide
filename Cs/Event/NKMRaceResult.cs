using System;
using Cs.Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F8C RID: 3980
	public sealed class NKMRaceResult : ISerializable
	{
		// Token: 0x060099F2 RID: 39410 RVA: 0x00330B6B File Offset: 0x0032ED6B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.RaceIndex);
			stream.PutOrGet(ref this.TeamAPoint);
			stream.PutOrGet(ref this.TeamBPoint);
		}

		// Token: 0x04008D12 RID: 36114
		public int RaceIndex;

		// Token: 0x04008D13 RID: 36115
		public long TeamAPoint;

		// Token: 0x04008D14 RID: 36116
		public long TeamBPoint;
	}
}
