using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F93 RID: 3987
	[PacketId(ClientPacketId.kNKMPACKET_RACE_RESET_NOT)]
	public sealed class NKMPACKET_RACE_RESET_NOT : ISerializable
	{
		// Token: 0x06009A00 RID: 39424 RVA: 0x00330CD7 File Offset: 0x0032EED7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.currentRaceId);
			stream.PutOrGet(ref this.currentRaceIndex);
			stream.PutOrGet<NKMRaceSummary>(ref this.summary);
		}

		// Token: 0x04008D25 RID: 36133
		public int currentRaceId;

		// Token: 0x04008D26 RID: 36134
		public int currentRaceIndex;

		// Token: 0x04008D27 RID: 36135
		public NKMRaceSummary summary = new NKMRaceSummary();
	}
}
