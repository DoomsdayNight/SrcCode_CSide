using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C83 RID: 3203
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_REMOVE_EVENT_DUNGEON_REQ)]
	public sealed class NKMPacket_WORLDMAP_REMOVE_EVENT_DUNGEON_REQ : ISerializable
	{
		// Token: 0x06009405 RID: 37893 RVA: 0x00327EFF File Offset: 0x003260FF
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.cityID);
		}

		// Token: 0x04008519 RID: 34073
		public int cityID;
	}
}
