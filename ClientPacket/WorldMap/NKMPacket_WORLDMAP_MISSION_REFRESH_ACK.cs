using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C80 RID: 3200
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_MISSION_REFRESH_ACK)]
	public sealed class NKMPacket_WORLDMAP_MISSION_REFRESH_ACK : ISerializable
	{
		// Token: 0x060093FF RID: 37887 RVA: 0x00327E0B File Offset: 0x0032600B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.cityID);
			stream.PutOrGet(ref this.stMissionIDList);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemData);
		}

		// Token: 0x0400850B RID: 34059
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400850C RID: 34060
		public int cityID;

		// Token: 0x0400850D RID: 34061
		public List<int> stMissionIDList = new List<int>();

		// Token: 0x0400850E RID: 34062
		public NKMItemMiscData costItemData;
	}
}
