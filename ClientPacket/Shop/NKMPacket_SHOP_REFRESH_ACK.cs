using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D2A RID: 3370
	[PacketId(ClientPacketId.kNKMPacket_SHOP_REFRESH_ACK)]
	public sealed class NKMPacket_SHOP_REFRESH_ACK : ISerializable
	{
		// Token: 0x06009551 RID: 38225 RVA: 0x00329FDB File Offset: 0x003281DB
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMShopRandomData>(ref this.randomShopData);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemData);
		}

		// Token: 0x040086FC RID: 34556
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040086FD RID: 34557
		public NKMShopRandomData randomShopData;

		// Token: 0x040086FE RID: 34558
		public NKMItemMiscData costItemData;
	}
}
