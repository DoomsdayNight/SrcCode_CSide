using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D3A RID: 3386
	[PacketId(ClientPacketId.kNKMPacket_STEAM_BUY_INIT_REQ)]
	public sealed class NKMPacket_STEAM_BUY_INIT_REQ : ISerializable
	{
		// Token: 0x06009571 RID: 38257 RVA: 0x0032A30D File Offset: 0x0032850D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.steamId);
			stream.PutOrGet(ref this.productId);
			stream.PutOrGet(ref this.language);
			stream.PutOrGet(ref this.country);
			stream.PutOrGet(ref this.itemShopDesc);
		}

		// Token: 0x04008726 RID: 34598
		public string steamId;

		// Token: 0x04008727 RID: 34599
		public int productId;

		// Token: 0x04008728 RID: 34600
		public string language;

		// Token: 0x04008729 RID: 34601
		public string country;

		// Token: 0x0400872A RID: 34602
		public string itemShopDesc;
	}
}
