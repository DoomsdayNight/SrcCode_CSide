using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D10 RID: 3344
	[PacketId(ClientPacketId.kNKMPacket_LIMIT_BREAK_SHIP_REQ)]
	public sealed class NKMPacket_LIMIT_BREAK_SHIP_REQ : ISerializable
	{
		// Token: 0x0600951D RID: 38173 RVA: 0x00329AE6 File Offset: 0x00327CE6
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.shipUid);
			stream.PutOrGet(ref this.consumeShipUid);
		}

		// Token: 0x040086B3 RID: 34483
		public long shipUid;

		// Token: 0x040086B4 RID: 34484
		public long consumeShipUid;
	}
}
