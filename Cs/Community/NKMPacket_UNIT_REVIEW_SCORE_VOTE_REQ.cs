using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FF7 RID: 4087
	[PacketId(ClientPacketId.kNKMPacket_UNIT_REVIEW_SCORE_VOTE_REQ)]
	public sealed class NKMPacket_UNIT_REVIEW_SCORE_VOTE_REQ : ISerializable
	{
		// Token: 0x06009ABE RID: 39614 RVA: 0x00331CF3 File Offset: 0x0032FEF3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitID);
			stream.PutOrGet(ref this.score);
		}

		// Token: 0x04008E1B RID: 36379
		public int unitID;

		// Token: 0x04008E1C RID: 36380
		public int score;
	}
}
