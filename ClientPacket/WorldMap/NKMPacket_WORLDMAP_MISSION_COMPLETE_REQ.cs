using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C81 RID: 3201
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_MISSION_COMPLETE_REQ)]
	public sealed class NKMPacket_WORLDMAP_MISSION_COMPLETE_REQ : ISerializable
	{
		// Token: 0x06009401 RID: 37889 RVA: 0x00327E50 File Offset: 0x00326050
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.cityID);
		}

		// Token: 0x0400850F RID: 34063
		public int cityID;
	}
}
