using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C7D RID: 3197
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_CITY_MISSION_CANCEL_REQ)]
	public sealed class NKMPacket_WORLDMAP_CITY_MISSION_CANCEL_REQ : ISerializable
	{
		// Token: 0x060093F9 RID: 37881 RVA: 0x00327DB1 File Offset: 0x00325FB1
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.cityID);
			stream.PutOrGet(ref this.missionID);
		}

		// Token: 0x04008506 RID: 34054
		public int cityID;

		// Token: 0x04008507 RID: 34055
		public int missionID;
	}
}
