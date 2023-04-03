using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C89 RID: 3209
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_BUILD_LEVELUP_REQ)]
	public sealed class NKMPacket_WORLDMAP_BUILD_LEVELUP_REQ : ISerializable
	{
		// Token: 0x06009411 RID: 37905 RVA: 0x00327FD5 File Offset: 0x003261D5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.cityID);
			stream.PutOrGet(ref this.buildID);
		}

		// Token: 0x04008524 RID: 34084
		public int cityID;

		// Token: 0x04008525 RID: 34085
		public int buildID;
	}
}
