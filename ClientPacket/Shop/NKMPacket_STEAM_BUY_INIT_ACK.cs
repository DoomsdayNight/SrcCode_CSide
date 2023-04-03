using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D3B RID: 3387
	[PacketId(ClientPacketId.kNKMPacket_STEAM_BUY_INIT_ACK)]
	public sealed class NKMPacket_STEAM_BUY_INIT_ACK : ISerializable
	{
		// Token: 0x06009573 RID: 38259 RVA: 0x0032A353 File Offset: 0x00328553
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.productId);
			stream.PutOrGet(ref this.orderId);
		}

		// Token: 0x0400872B RID: 34603
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400872C RID: 34604
		public int productId;

		// Token: 0x0400872D RID: 34605
		public string orderId;
	}
}
