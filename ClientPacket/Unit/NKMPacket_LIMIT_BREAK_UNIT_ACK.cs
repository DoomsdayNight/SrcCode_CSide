using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CE9 RID: 3305
	[PacketId(ClientPacketId.kNKMPacket_LIMIT_BREAK_UNIT_ACK)]
	public sealed class NKMPacket_LIMIT_BREAK_UNIT_ACK : ISerializable
	{
		// Token: 0x060094CF RID: 38095 RVA: 0x003293C9 File Offset: 0x003275C9
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMUnitData>(ref this.unitData);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
		}

		// Token: 0x0400864F RID: 34383
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008650 RID: 34384
		public NKMUnitData unitData;

		// Token: 0x04008651 RID: 34385
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();
	}
}
