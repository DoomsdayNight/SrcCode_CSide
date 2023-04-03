using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C74 RID: 3188
	public sealed class NKMWorldMapData : ISerializable
	{
		// Token: 0x060093E7 RID: 37863 RVA: 0x00327C46 File Offset: 0x00325E46
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMWorldMapCityData>(ref this.worldMapCityDataMap);
			stream.PutOrGet(ref this.collectLastResetDate);
		}

		// Token: 0x040084F2 RID: 34034
		public Dictionary<int, NKMWorldMapCityData> worldMapCityDataMap = new Dictionary<int, NKMWorldMapCityData>();

		// Token: 0x040084F3 RID: 34035
		public DateTime collectLastResetDate;
	}
}
