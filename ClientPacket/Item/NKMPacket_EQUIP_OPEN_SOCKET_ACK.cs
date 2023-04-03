using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EBD RID: 3773
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_OPEN_SOCKET_ACK)]
	public sealed class NKMPacket_EQUIP_OPEN_SOCKET_ACK : ISerializable
	{
		// Token: 0x06009866 RID: 39014 RVA: 0x0032E5BC File Offset: 0x0032C7BC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMEquipItemData>(ref this.equipItemData);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
		}

		// Token: 0x04008AC7 RID: 35527
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008AC8 RID: 35528
		public NKMEquipItemData equipItemData;

		// Token: 0x04008AC9 RID: 35529
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();
	}
}
