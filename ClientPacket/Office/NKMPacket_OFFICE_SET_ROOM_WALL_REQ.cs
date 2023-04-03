using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DF6 RID: 3574
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_SET_ROOM_WALL_REQ)]
	public sealed class NKMPacket_OFFICE_SET_ROOM_WALL_REQ : ISerializable
	{
		// Token: 0x060096E0 RID: 38624 RVA: 0x0032C26F File Offset: 0x0032A46F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.roomId);
			stream.PutOrGet(ref this.wallInteriorId);
		}

		// Token: 0x040088E4 RID: 35044
		public int roomId;

		// Token: 0x040088E5 RID: 35045
		public int wallInteriorId;
	}
}
