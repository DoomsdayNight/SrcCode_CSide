using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C7F RID: 3199
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_MISSION_REFRESH_REQ)]
	public sealed class NKMPacket_WORLDMAP_MISSION_REFRESH_REQ : ISerializable
	{
		// Token: 0x060093FD RID: 37885 RVA: 0x00327DF5 File Offset: 0x00325FF5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.cityID);
		}

		// Token: 0x0400850A RID: 34058
		public int cityID;
	}
}
