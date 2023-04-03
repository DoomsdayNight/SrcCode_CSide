using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C87 RID: 3207
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_BUILD_REQ)]
	public sealed class NKMPacket_WORLDMAP_BUILD_REQ : ISerializable
	{
		// Token: 0x0600940D RID: 37901 RVA: 0x00327F6E File Offset: 0x0032616E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.cityID);
			stream.PutOrGet(ref this.buildID);
		}

		// Token: 0x0400851E RID: 34078
		public int cityID;

		// Token: 0x0400851F RID: 34079
		public int buildID;
	}
}
