using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F69 RID: 3945
	[PacketId(ClientPacketId.kNKMPacket_FIERCE_PENALTY_ACK)]
	public sealed class NKMPacket_FIERCE_PENALTY_ACK : ISerializable
	{
		// Token: 0x060099B2 RID: 39346 RVA: 0x003305C0 File Offset: 0x0032E7C0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.fierceBossId);
			stream.PutOrGet(ref this.penaltyIds);
		}

		// Token: 0x04008CBA RID: 36026
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008CBB RID: 36027
		public int fierceBossId;

		// Token: 0x04008CBC RID: 36028
		public List<int> penaltyIds = new List<int>();
	}
}
