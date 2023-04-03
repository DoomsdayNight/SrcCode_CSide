using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D11 RID: 3345
	[PacketId(ClientPacketId.kNKMPacket_LIMIT_BREAK_SHIP_ACK)]
	public sealed class NKMPacket_LIMIT_BREAK_SHIP_ACK : ISerializable
	{
		// Token: 0x0600951F RID: 38175 RVA: 0x00329B08 File Offset: 0x00327D08
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMUnitData>(ref this.shipData);
			stream.PutOrGet(ref this.consumeUnitUid);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
		}

		// Token: 0x040086B5 RID: 34485
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040086B6 RID: 34486
		public NKMUnitData shipData;

		// Token: 0x040086B7 RID: 34487
		public long consumeUnitUid;

		// Token: 0x040086B8 RID: 34488
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();
	}
}
