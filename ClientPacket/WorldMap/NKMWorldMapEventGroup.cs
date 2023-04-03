using System;
using Cs.Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C71 RID: 3185
	public sealed class NKMWorldMapEventGroup : ISerializable
	{
		// Token: 0x060093E1 RID: 37857 RVA: 0x00327B5E File Offset: 0x00325D5E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.worldmapEventID);
			stream.PutOrGet(ref this.eventGroupEndDate);
			stream.PutOrGet(ref this.eventUid);
		}

		// Token: 0x040084E5 RID: 34021
		public int worldmapEventID;

		// Token: 0x040084E6 RID: 34022
		public DateTime eventGroupEndDate;

		// Token: 0x040084E7 RID: 34023
		public long eventUid;
	}
}
