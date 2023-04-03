using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D2C RID: 3372
	[PacketId(ClientPacketId.kNKMPacket_SHOP_FIX_SHOP_CASH_BUY_POSSIBLE_REQ)]
	public sealed class NKMPacket_SHOP_FIX_SHOP_CASH_BUY_POSSIBLE_REQ : ISerializable
	{
		// Token: 0x06009555 RID: 38229 RVA: 0x0032A013 File Offset: 0x00328213
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.productMarketID);
			stream.PutOrGet(ref this.selectIndices);
		}

		// Token: 0x040086FF RID: 34559
		public string productMarketID;

		// Token: 0x04008700 RID: 34560
		public List<int> selectIndices = new List<int>();
	}
}
