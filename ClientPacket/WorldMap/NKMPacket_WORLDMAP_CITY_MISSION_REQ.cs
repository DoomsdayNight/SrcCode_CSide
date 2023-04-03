using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C7B RID: 3195
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_CITY_MISSION_REQ)]
	public sealed class NKMPacket_WORLDMAP_CITY_MISSION_REQ : ISerializable
	{
		// Token: 0x060093F5 RID: 37877 RVA: 0x00327D55 File Offset: 0x00325F55
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.cityID);
			stream.PutOrGet(ref this.missionID);
		}

		// Token: 0x04008500 RID: 34048
		public int cityID;

		// Token: 0x04008501 RID: 34049
		public int missionID;
	}
}
