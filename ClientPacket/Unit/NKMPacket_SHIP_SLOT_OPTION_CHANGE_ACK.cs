using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D15 RID: 3349
	[PacketId(ClientPacketId.kNKMPacket_SHIP_SLOT_OPTION_CHANGE_ACK)]
	public sealed class NKMPacket_SHIP_SLOT_OPTION_CHANGE_ACK : ISerializable
	{
		// Token: 0x06009527 RID: 38183 RVA: 0x00329BE2 File Offset: 0x00327DE2
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMUnitData>(ref this.shipData);
			stream.PutOrGet<NKMShipModuleCandidate>(ref this.candidateOption);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
		}

		// Token: 0x040086C2 RID: 34498
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040086C3 RID: 34499
		public NKMUnitData shipData;

		// Token: 0x040086C4 RID: 34500
		public NKMShipModuleCandidate candidateOption = new NKMShipModuleCandidate();

		// Token: 0x040086C5 RID: 34501
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();
	}
}
