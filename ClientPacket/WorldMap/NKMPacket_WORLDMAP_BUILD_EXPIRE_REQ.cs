using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C8B RID: 3211
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_BUILD_EXPIRE_REQ)]
	public sealed class NKMPacket_WORLDMAP_BUILD_EXPIRE_REQ : ISerializable
	{
		// Token: 0x06009415 RID: 37909 RVA: 0x00328047 File Offset: 0x00326247
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.cityID);
			stream.PutOrGet(ref this.buildID);
		}

		// Token: 0x0400852A RID: 34090
		public int cityID;

		// Token: 0x0400852B RID: 34091
		public int buildID;
	}
}
