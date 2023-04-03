using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D3D RID: 3389
	[PacketId(ClientPacketId.kNKMPacket_SHOP_SUBSCRIPTION_NOT)]
	public sealed class NKMPacket_SHOP_SUBSCRIPTION_NOT : ISerializable
	{
		// Token: 0x06009577 RID: 38263 RVA: 0x0032A3EC File Offset: 0x003285EC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.productId);
			stream.PutOrGet(ref this.lastUpdateDate);
		}

		// Token: 0x04008734 RID: 34612
		public int productId;

		// Token: 0x04008735 RID: 34613
		public DateTime lastUpdateDate;
	}
}
