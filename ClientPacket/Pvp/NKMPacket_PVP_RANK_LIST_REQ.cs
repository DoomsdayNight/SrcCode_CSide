using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D77 RID: 3447
	[PacketId(ClientPacketId.kNKMPacket_PVP_RANK_LIST_REQ)]
	public sealed class NKMPacket_PVP_RANK_LIST_REQ : ISerializable
	{
		// Token: 0x060095E9 RID: 38377 RVA: 0x0032AF7C File Offset: 0x0032917C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<RANK_TYPE>(ref this.rankType);
			stream.PutOrGet(ref this.isAll);
		}

		// Token: 0x040087E2 RID: 34786
		public RANK_TYPE rankType;

		// Token: 0x040087E3 RID: 34787
		public bool isAll;
	}
}
