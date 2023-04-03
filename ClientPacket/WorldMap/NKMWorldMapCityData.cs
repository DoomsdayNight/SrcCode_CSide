using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C73 RID: 3187
	public sealed class NKMWorldMapCityData : ISerializable
	{
		// Token: 0x060093E5 RID: 37861 RVA: 0x00327BBC File Offset: 0x00325DBC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.cityID);
			stream.PutOrGet(ref this.leaderUnitUID);
			stream.PutOrGet(ref this.exp);
			stream.PutOrGet(ref this.level);
			stream.PutOrGet<NKMWorldMapMission>(ref this.worldMapMission);
			stream.PutOrGet<NKMWorldMapEventGroup>(ref this.worldMapEventGroup);
			stream.PutOrGet<NKMWorldmapCityBuildingData>(ref this.worldMapCityBuildingDataMap);
		}

		// Token: 0x040084EB RID: 34027
		public int cityID;

		// Token: 0x040084EC RID: 34028
		public long leaderUnitUID;

		// Token: 0x040084ED RID: 34029
		public int exp;

		// Token: 0x040084EE RID: 34030
		public int level;

		// Token: 0x040084EF RID: 34031
		public NKMWorldMapMission worldMapMission = new NKMWorldMapMission();

		// Token: 0x040084F0 RID: 34032
		public NKMWorldMapEventGroup worldMapEventGroup = new NKMWorldMapEventGroup();

		// Token: 0x040084F1 RID: 34033
		public Dictionary<int, NKMWorldmapCityBuildingData> worldMapCityBuildingDataMap = new Dictionary<int, NKMWorldmapCityBuildingData>();
	}
}
