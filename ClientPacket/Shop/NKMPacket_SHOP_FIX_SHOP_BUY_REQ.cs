using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D22 RID: 3362
	[PacketId(ClientPacketId.kNKMPacket_SHOP_FIX_SHOP_BUY_REQ)]
	public sealed class NKMPacket_SHOP_FIX_SHOP_BUY_REQ : ISerializable
	{
		// Token: 0x06009541 RID: 38209 RVA: 0x00329E06 File Offset: 0x00328006
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.productID);
			stream.PutOrGet(ref this.productCount);
			stream.PutOrGet(ref this.selectIndices);
		}

		// Token: 0x040086E3 RID: 34531
		public int productID;

		// Token: 0x040086E4 RID: 34532
		public int productCount;

		// Token: 0x040086E5 RID: 34533
		public List<int> selectIndices = new List<int>();
	}
}
