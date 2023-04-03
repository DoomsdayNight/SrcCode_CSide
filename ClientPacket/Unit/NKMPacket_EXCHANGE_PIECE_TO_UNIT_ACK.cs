using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CF9 RID: 3321
	[PacketId(ClientPacketId.kNKMPacket_EXCHANGE_PIECE_TO_UNIT_ACK)]
	public sealed class NKMPacket_EXCHANGE_PIECE_TO_UNIT_ACK : ISerializable
	{
		// Token: 0x060094EF RID: 38127 RVA: 0x00329695 File Offset: 0x00327895
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMUnitData>(ref this.unitDataList);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemData);
		}

		// Token: 0x04008676 RID: 34422
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008677 RID: 34423
		public List<NKMUnitData> unitDataList = new List<NKMUnitData>();

		// Token: 0x04008678 RID: 34424
		public NKMItemMiscData costItemData;
	}
}
