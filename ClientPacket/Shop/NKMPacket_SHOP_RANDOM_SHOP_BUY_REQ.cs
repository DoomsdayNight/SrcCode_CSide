using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D25 RID: 3365
	[PacketId(ClientPacketId.kNKMPacket_SHOP_RANDOM_SHOP_BUY_REQ)]
	public sealed class NKMPacket_SHOP_RANDOM_SHOP_BUY_REQ : ISerializable
	{
		// Token: 0x06009547 RID: 38215 RVA: 0x00329F27 File Offset: 0x00328127
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.slotIndex);
		}

		// Token: 0x040086F3 RID: 34547
		public int slotIndex;
	}
}
