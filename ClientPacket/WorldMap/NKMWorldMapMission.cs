using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C70 RID: 3184
	public sealed class NKMWorldMapMission : ISerializable
	{
		// Token: 0x060093DF RID: 37855 RVA: 0x00327B19 File Offset: 0x00325D19
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.currentMissionID);
			stream.PutOrGet(ref this.completeTime);
			stream.PutOrGet(ref this.startDate);
			stream.PutOrGet(ref this.stMissionIDList);
		}

		// Token: 0x040084E1 RID: 34017
		public int currentMissionID;

		// Token: 0x040084E2 RID: 34018
		public long completeTime;

		// Token: 0x040084E3 RID: 34019
		public DateTime startDate;

		// Token: 0x040084E4 RID: 34020
		public List<int> stMissionIDList = new List<int>();
	}
}
