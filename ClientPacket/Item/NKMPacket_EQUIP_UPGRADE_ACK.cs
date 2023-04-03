using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EBB RID: 3771
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_UPGRADE_ACK)]
	public sealed class NKMPacket_EQUIP_UPGRADE_ACK : ISerializable
	{
		// Token: 0x06009862 RID: 39010 RVA: 0x0032E54A File Offset: 0x0032C74A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMEquipItemData>(ref this.equipItemData);
			stream.PutOrGet(ref this.consumeEquipItemUidList);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
		}

		// Token: 0x04008AC1 RID: 35521
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008AC2 RID: 35522
		public NKMEquipItemData equipItemData;

		// Token: 0x04008AC3 RID: 35523
		public List<long> consumeEquipItemUidList = new List<long>();

		// Token: 0x04008AC4 RID: 35524
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();
	}
}
