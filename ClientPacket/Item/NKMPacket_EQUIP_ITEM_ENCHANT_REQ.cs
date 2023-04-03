using System;
using System.Collections.Generic;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E87 RID: 3719
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_ITEM_ENCHANT_REQ)]
	public sealed class NKMPacket_EQUIP_ITEM_ENCHANT_REQ : ISerializable
	{
		// Token: 0x060097FA RID: 38906 RVA: 0x0032DC54 File Offset: 0x0032BE54
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.equipItemUID);
			stream.PutOrGet(ref this.consumeEquipItemUIDList);
		}

		// Token: 0x04008A47 RID: 35399
		public long equipItemUID;

		// Token: 0x04008A48 RID: 35400
		public List<long> consumeEquipItemUIDList = new List<long>();
	}
}
