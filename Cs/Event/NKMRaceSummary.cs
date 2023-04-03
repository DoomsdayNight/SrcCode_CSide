using System;
using Cs.Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F8E RID: 3982
	public sealed class NKMRaceSummary : ISerializable
	{
		// Token: 0x060099F6 RID: 39414 RVA: 0x00330BD3 File Offset: 0x0032EDD3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMRaceResult>(ref this.raceResult);
			stream.PutOrGet<NKMRacePrivate>(ref this.racePrivate);
		}

		// Token: 0x04008D19 RID: 36121
		public NKMRaceResult raceResult = new NKMRaceResult();

		// Token: 0x04008D1A RID: 36122
		public NKMRacePrivate racePrivate = new NKMRacePrivate();
	}
}
