using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F90 RID: 3984
	[PacketId(ClientPacketId.kNKMPACKET_RACE_TEAM_SELECT_ACK)]
	public sealed class NKMPACKET_RACE_TEAM_SELECT_ACK : ISerializable
	{
		// Token: 0x060099FA RID: 39418 RVA: 0x00330C21 File Offset: 0x0032EE21
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRacePrivate>(ref this.racePrivate);
		}

		// Token: 0x04008D1C RID: 36124
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008D1D RID: 36125
		public NKMRacePrivate racePrivate = new NKMRacePrivate();
	}
}
