using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.LeaderBoard
{
	// Token: 0x02000E77 RID: 3703
	[PacketId(ClientPacketId.kNKMPacket_LEADERBOARD_SHADOWPALACE_LIST_REQ)]
	public sealed class NKMPacket_LEADERBOARD_SHADOWPALACE_LIST_REQ : ISerializable
	{
		// Token: 0x060097DC RID: 38876 RVA: 0x0032D91A File Offset: 0x0032BB1A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.actId);
			stream.PutOrGet(ref this.isAll);
		}

		// Token: 0x04008A12 RID: 35346
		public int actId;

		// Token: 0x04008A13 RID: 35347
		public bool isAll;
	}
}
