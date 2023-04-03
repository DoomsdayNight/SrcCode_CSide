using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DE0 RID: 3552
	[PacketId(ClientPacketId.kNKMPacket_NPC_PVP_TARGET_LIST_ACK)]
	public sealed class NKMPacket_NPC_PVP_TARGET_LIST_ACK : ISerializable
	{
		// Token: 0x060096B7 RID: 38583 RVA: 0x0032BD63 File Offset: 0x00329F63
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NpcPvpTarget>(ref this.targetList);
		}

		// Token: 0x0400889D RID: 34973
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400889E RID: 34974
		public List<NpcPvpTarget> targetList = new List<NpcPvpTarget>();
	}
}
