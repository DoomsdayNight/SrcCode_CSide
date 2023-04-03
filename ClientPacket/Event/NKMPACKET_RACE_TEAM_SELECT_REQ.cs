using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F8F RID: 3983
	[PacketId(ClientPacketId.kNKMPACKET_RACE_TEAM_SELECT_REQ)]
	public sealed class NKMPACKET_RACE_TEAM_SELECT_REQ : ISerializable
	{
		// Token: 0x060099F8 RID: 39416 RVA: 0x00330C0B File Offset: 0x0032EE0B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<RaceTeam>(ref this.selectTeam);
		}

		// Token: 0x04008D1B RID: 36123
		public RaceTeam selectTeam;
	}
}
