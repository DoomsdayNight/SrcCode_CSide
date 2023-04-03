using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CED RID: 3309
	[PacketId(ClientPacketId.kNKMPacket_SHIP_BUILD_ACK)]
	public sealed class NKMPacket_SHIP_BUILD_ACK : ISerializable
	{
		// Token: 0x060094D7 RID: 38103 RVA: 0x0032948B File Offset: 0x0032768B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMUnitData>(ref this.shipData);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemList);
		}

		// Token: 0x0400865A RID: 34394
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400865B RID: 34395
		public NKMUnitData shipData;

		// Token: 0x0400865C RID: 34396
		public List<NKMItemMiscData> costItemList = new List<NKMItemMiscData>();
	}
}
