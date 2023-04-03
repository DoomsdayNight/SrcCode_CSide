using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D28 RID: 3368
	[PacketId(ClientPacketId.kNKMPacket_SHOP_FIXED_LIST_ACK)]
	public sealed class NKMPacket_SHOP_FIXED_LIST_ACK : ISerializable
	{
		// Token: 0x0600954D RID: 38221 RVA: 0x00329F81 File Offset: 0x00328181
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.shopList);
			stream.PutOrGet<InstantProduct>(ref this.InstantProductList);
		}

		// Token: 0x040086F8 RID: 34552
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040086F9 RID: 34553
		public List<int> shopList = new List<int>();

		// Token: 0x040086FA RID: 34554
		public List<InstantProduct> InstantProductList = new List<InstantProduct>();
	}
}
