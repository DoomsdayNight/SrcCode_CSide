using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D2F RID: 3375
	[PacketId(ClientPacketId.kNKMPacket_SHOP_CHAIN_TAB_RESET_TIME_ACK)]
	public sealed class NKMPacket_SHOP_CHAIN_TAB_RESET_TIME_ACK : ISerializable
	{
		// Token: 0x0600955B RID: 38235 RVA: 0x0032A09A File Offset: 0x0032829A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<ShopChainTabNextResetData>(ref this.list);
		}

		// Token: 0x04008705 RID: 34565
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008706 RID: 34566
		public List<ShopChainTabNextResetData> list = new List<ShopChainTabNextResetData>();
	}
}
