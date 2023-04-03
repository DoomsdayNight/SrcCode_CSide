using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000CF3 RID: 3315
	[PacketId(ClientPacketId.kNKMPacket_SHIP_DIVISION_ACK)]
	public sealed class NKMPacket_SHIP_DIVISION_ACK : ISerializable
	{
		// Token: 0x060094E3 RID: 38115 RVA: 0x0032959B File Offset: 0x0032779B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.removeShipUIDList);
			stream.PutOrGet<NKMItemMiscData>(ref this.rewardItemDataList);
		}

		// Token: 0x04008668 RID: 34408
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008669 RID: 34409
		public List<long> removeShipUIDList = new List<long>();

		// Token: 0x0400866A RID: 34410
		public List<NKMItemMiscData> rewardItemDataList = new List<NKMItemMiscData>();
	}
}
