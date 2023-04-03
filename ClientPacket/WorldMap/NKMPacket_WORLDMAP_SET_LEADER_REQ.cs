using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C79 RID: 3193
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_SET_LEADER_REQ)]
	public sealed class NKMPacket_WORLDMAP_SET_LEADER_REQ : ISerializable
	{
		// Token: 0x060093F1 RID: 37873 RVA: 0x00327D05 File Offset: 0x00325F05
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.cityID);
			stream.PutOrGet(ref this.leaderUID);
		}

		// Token: 0x040084FB RID: 34043
		public int cityID;

		// Token: 0x040084FC RID: 34044
		public long leaderUID;
	}
}
