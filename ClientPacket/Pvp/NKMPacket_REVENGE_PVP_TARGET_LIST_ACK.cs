using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DDE RID: 3550
	[PacketId(ClientPacketId.kNKMPacket_REVENGE_PVP_TARGET_LIST_ACK)]
	public sealed class NKMPacket_REVENGE_PVP_TARGET_LIST_ACK : ISerializable
	{
		// Token: 0x060096B3 RID: 38579 RVA: 0x0032BD20 File Offset: 0x00329F20
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<RevengePvpTarget>(ref this.targetList);
		}

		// Token: 0x0400889A RID: 34970
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400889B RID: 34971
		public List<RevengePvpTarget> targetList = new List<RevengePvpTarget>();
	}
}
