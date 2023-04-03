using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C84 RID: 3204
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_REMOVE_EVENT_DUNGEON_ACK)]
	public sealed class NKMPacket_WORLDMAP_REMOVE_EVENT_DUNGEON_ACK : ISerializable
	{
		// Token: 0x06009407 RID: 37895 RVA: 0x00327F15 File Offset: 0x00326115
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.cityID);
		}

		// Token: 0x0400851A RID: 34074
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400851B RID: 34075
		public int cityID;
	}
}
