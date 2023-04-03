using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D02 RID: 3330
	[PacketId(ClientPacketId.kNKMPacket_RECALL_UNIT_REQ)]
	public sealed class NKMPacket_RECALL_UNIT_REQ : ISerializable
	{
		// Token: 0x06009501 RID: 38145 RVA: 0x0032985D File Offset: 0x00327A5D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.recallUnitUid);
			stream.PutOrGet(ref this.exchangeUnitId);
		}

		// Token: 0x0400868F RID: 34447
		public long recallUnitUid;

		// Token: 0x04008690 RID: 34448
		public int exchangeUnitId;
	}
}
