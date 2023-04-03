using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D3C RID: 3388
	[PacketId(ClientPacketId.kNKMPacket_STEAM_BUY_REQ)]
	public sealed class NKMPacket_STEAM_BUY_REQ : ISerializable
	{
		// Token: 0x06009575 RID: 38261 RVA: 0x0032A384 File Offset: 0x00328584
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.steamId);
			stream.PutOrGet(ref this.orderId);
			stream.PutOrGet(ref this.productId);
			stream.PutOrGet(ref this.country);
			stream.PutOrGet(ref this.currency);
			stream.PutOrGet(ref this.selectIndices);
		}

		// Token: 0x0400872E RID: 34606
		public string steamId;

		// Token: 0x0400872F RID: 34607
		public string orderId;

		// Token: 0x04008730 RID: 34608
		public int productId;

		// Token: 0x04008731 RID: 34609
		public string country;

		// Token: 0x04008732 RID: 34610
		public string currency;

		// Token: 0x04008733 RID: 34611
		public List<int> selectIndices = new List<int>();
	}
}
