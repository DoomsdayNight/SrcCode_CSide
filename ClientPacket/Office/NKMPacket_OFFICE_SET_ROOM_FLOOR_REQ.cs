using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DF4 RID: 3572
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_SET_ROOM_FLOOR_REQ)]
	public sealed class NKMPacket_OFFICE_SET_ROOM_FLOOR_REQ : ISerializable
	{
		// Token: 0x060096DC RID: 38620 RVA: 0x0032C209 File Offset: 0x0032A409
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.roomId);
			stream.PutOrGet(ref this.floorInteriorId);
		}

		// Token: 0x040088DF RID: 35039
		public int roomId;

		// Token: 0x040088E0 RID: 35040
		public int floorInteriorId;
	}
}
