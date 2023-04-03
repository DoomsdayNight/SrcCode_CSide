using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E88 RID: 3720
	[PacketId(ClientPacketId.kNKMPAcket_EQUIP_ITEM_ENCHANT_ACK)]
	public sealed class NKMPAcket_EQUIP_ITEM_ENCHANT_ACK : ISerializable
	{
		// Token: 0x060097FC RID: 38908 RVA: 0x0032DC84 File Offset: 0x0032BE84
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.equipItemUID);
			stream.PutOrGet(ref this.enchantLevel);
			stream.PutOrGet(ref this.enchantExp);
			stream.PutOrGet(ref this.consumeEquipItemUIDList);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
		}

		// Token: 0x04008A49 RID: 35401
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008A4A RID: 35402
		public long equipItemUID;

		// Token: 0x04008A4B RID: 35403
		public int enchantLevel;

		// Token: 0x04008A4C RID: 35404
		public int enchantExp;

		// Token: 0x04008A4D RID: 35405
		public List<long> consumeEquipItemUIDList = new List<long>();

		// Token: 0x04008A4E RID: 35406
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();
	}
}
