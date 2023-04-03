using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D13 RID: 3347
	[PacketId(ClientPacketId.kNKMPacket_SHIP_SLOT_LOCK_ACK)]
	public sealed class NKMPacket_SHIP_SLOT_LOCK_ACK : ISerializable
	{
		// Token: 0x06009523 RID: 38179 RVA: 0x00329B87 File Offset: 0x00327D87
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMUnitData>(ref this.shipData);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
		}

		// Token: 0x040086BD RID: 34493
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040086BE RID: 34494
		public NKMUnitData shipData;

		// Token: 0x040086BF RID: 34495
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();
	}
}
