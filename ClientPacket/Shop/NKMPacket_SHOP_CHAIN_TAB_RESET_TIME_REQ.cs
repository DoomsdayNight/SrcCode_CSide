using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D2E RID: 3374
	[PacketId(ClientPacketId.kNKMPacket_SHOP_CHAIN_TAB_RESET_TIME_REQ)]
	public sealed class NKMPacket_SHOP_CHAIN_TAB_RESET_TIME_REQ : ISerializable
	{
		// Token: 0x06009559 RID: 38233 RVA: 0x0032A090 File Offset: 0x00328290
		void ISerializable.Serialize(IPacketStream stream)
		{
		}
	}
}
