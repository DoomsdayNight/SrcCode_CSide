using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C7C RID: 3196
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_CITY_MISSION_ACK)]
	public sealed class NKMPacket_WORLDMAP_CITY_MISSION_ACK : ISerializable
	{
		// Token: 0x060093F7 RID: 37879 RVA: 0x00327D77 File Offset: 0x00325F77
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.cityID);
			stream.PutOrGet(ref this.missionID);
			stream.PutOrGet(ref this.completeTime);
		}

		// Token: 0x04008502 RID: 34050
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008503 RID: 34051
		public int cityID;

		// Token: 0x04008504 RID: 34052
		public int missionID;

		// Token: 0x04008505 RID: 34053
		public long completeTime;
	}
}
