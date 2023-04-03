using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D2D RID: 3373
	[PacketId(ClientPacketId.kNKMPacket_SHOP_FIX_SHOP_CASH_BUY_POSSIBLE_ACK)]
	public sealed class NKMPacket_SHOP_FIX_SHOP_CASH_BUY_POSSIBLE_ACK : ISerializable
	{
		// Token: 0x06009557 RID: 38231 RVA: 0x0032A040 File Offset: 0x00328240
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.productMarketID);
			stream.PutOrGet<NKMShopPurchaseHistory>(ref this.histroy);
			stream.PutOrGet(ref this.selectIndices);
		}

		// Token: 0x04008701 RID: 34561
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008702 RID: 34562
		public string productMarketID;

		// Token: 0x04008703 RID: 34563
		public NKMShopPurchaseHistory histroy = new NKMShopPurchaseHistory();

		// Token: 0x04008704 RID: 34564
		public List<int> selectIndices = new List<int>();
	}
}
