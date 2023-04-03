using System;
using Cs.Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C72 RID: 3186
	public sealed class NKMWorldmapCityBuildingData : ISerializable
	{
		// Token: 0x060093E3 RID: 37859 RVA: 0x00327B8C File Offset: 0x00325D8C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.buildUID);
			stream.PutOrGet(ref this.id);
			stream.PutOrGet(ref this.level);
		}

		// Token: 0x040084E8 RID: 34024
		public long buildUID;

		// Token: 0x040084E9 RID: 34025
		public int id;

		// Token: 0x040084EA RID: 34026
		public int level;
	}
}
