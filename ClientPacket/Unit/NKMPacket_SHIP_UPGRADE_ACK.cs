using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CF1 RID: 3313
	[PacketId(ClientPacketId.kNKMPacket_SHIP_UPGRADE_ACK)]
	public sealed class NKMPacket_SHIP_UPGRADE_ACK : ISerializable
	{
		// Token: 0x060094DF RID: 38111 RVA: 0x00329541 File Offset: 0x00327741
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMUnitData>(ref this.shipUnitData);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
		}

		// Token: 0x04008664 RID: 34404
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008665 RID: 34405
		public NKMUnitData shipUnitData;

		// Token: 0x04008666 RID: 34406
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();
	}
}
