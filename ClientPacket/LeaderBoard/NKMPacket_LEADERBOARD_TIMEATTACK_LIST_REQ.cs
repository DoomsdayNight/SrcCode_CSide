using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.LeaderBoard
{
	// Token: 0x02000E81 RID: 3713
	[PacketId(ClientPacketId.kNKMPacket_LEADERBOARD_TIMEATTACK_LIST_REQ)]
	public sealed class NKMPacket_LEADERBOARD_TIMEATTACK_LIST_REQ : ISerializable
	{
		// Token: 0x060097F0 RID: 38896 RVA: 0x0032DB1C File Offset: 0x0032BD1C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.stageId);
			stream.PutOrGet(ref this.isAll);
		}

		// Token: 0x04008A2D RID: 35373
		public int stageId;

		// Token: 0x04008A2E RID: 35374
		public bool isAll;
	}
}
