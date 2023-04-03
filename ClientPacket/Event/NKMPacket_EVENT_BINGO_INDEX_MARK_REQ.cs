using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F75 RID: 3957
	[PacketId(ClientPacketId.kNKMPacket_EVENT_BINGO_INDEX_MARK_REQ)]
	public sealed class NKMPacket_EVENT_BINGO_INDEX_MARK_REQ : ISerializable
	{
		// Token: 0x060099C6 RID: 39366 RVA: 0x00330811 File Offset: 0x0032EA11
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.eventId);
			stream.PutOrGet(ref this.tileIndex);
		}

		// Token: 0x04008CDF RID: 36063
		public int eventId;

		// Token: 0x04008CE0 RID: 36064
		public int tileIndex;
	}
}
