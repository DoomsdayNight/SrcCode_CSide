using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F68 RID: 3944
	[PacketId(ClientPacketId.kNKMPacket_FIERCE_PENALTY_REQ)]
	public sealed class NKMPacket_FIERCE_PENALTY_REQ : ISerializable
	{
		// Token: 0x060099B0 RID: 39344 RVA: 0x00330593 File Offset: 0x0032E793
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.fierceBossId);
			stream.PutOrGet(ref this.penaltyIds);
		}

		// Token: 0x04008CB8 RID: 36024
		public int fierceBossId;

		// Token: 0x04008CB9 RID: 36025
		public List<int> penaltyIds = new List<int>();
	}
}
