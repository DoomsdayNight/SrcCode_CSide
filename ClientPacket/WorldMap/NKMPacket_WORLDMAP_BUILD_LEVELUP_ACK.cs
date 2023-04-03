using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C8A RID: 3210
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_BUILD_LEVELUP_ACK)]
	public sealed class NKMPacket_WORLDMAP_BUILD_LEVELUP_ACK : ISerializable
	{
		// Token: 0x06009413 RID: 37907 RVA: 0x00327FF7 File Offset: 0x003261F7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.cityID);
			stream.PutOrGet<NKMWorldmapCityBuildingData>(ref this.worldMapCityBuildingData);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
		}

		// Token: 0x04008526 RID: 34086
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008527 RID: 34087
		public int cityID;

		// Token: 0x04008528 RID: 34088
		public NKMWorldmapCityBuildingData worldMapCityBuildingData = new NKMWorldmapCityBuildingData();

		// Token: 0x04008529 RID: 34089
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();
	}
}
