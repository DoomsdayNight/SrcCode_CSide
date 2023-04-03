using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D84 RID: 3460
	[PacketId(ClientPacketId.kNKMPacket_ASYNC_PVP_RANK_LIST_REQ)]
	public sealed class NKMPacket_ASYNC_PVP_RANK_LIST_REQ : ISerializable
	{
		// Token: 0x06009603 RID: 38403 RVA: 0x0032B185 File Offset: 0x00329385
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<RANK_TYPE>(ref this.rankType);
			stream.PutOrGet(ref this.isAll);
		}

		// Token: 0x040087FE RID: 34814
		public RANK_TYPE rankType;

		// Token: 0x040087FF RID: 34815
		public bool isAll;
	}
}
