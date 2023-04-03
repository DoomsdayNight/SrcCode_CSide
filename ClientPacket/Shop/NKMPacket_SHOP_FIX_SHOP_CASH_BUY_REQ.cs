using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D23 RID: 3363
	[PacketId(ClientPacketId.kNKMPacket_SHOP_FIX_SHOP_CASH_BUY_REQ)]
	public sealed class NKMPacket_SHOP_FIX_SHOP_CASH_BUY_REQ : ISerializable
	{
		// Token: 0x06009543 RID: 38211 RVA: 0x00329E40 File Offset: 0x00328040
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.productMarketID);
			stream.PutOrGet(ref this.validationToken);
			stream.PutOrGet(ref this.realCash);
			stream.PutOrGet(ref this.currencyType);
			stream.PutOrGet(ref this.currencyCode);
			stream.PutOrGet(ref this.selectIndices);
		}

		// Token: 0x040086E6 RID: 34534
		public string productMarketID;

		// Token: 0x040086E7 RID: 34535
		public string validationToken;

		// Token: 0x040086E8 RID: 34536
		public double realCash;

		// Token: 0x040086E9 RID: 34537
		public int currencyType;

		// Token: 0x040086EA RID: 34538
		public string currencyCode;

		// Token: 0x040086EB RID: 34539
		public List<int> selectIndices = new List<int>();
	}
}
