using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DF0 RID: 3568
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_SET_ROOM_NAME_REQ)]
	public sealed class NKMPacket_OFFICE_SET_ROOM_NAME_REQ : ISerializable
	{
		// Token: 0x060096D4 RID: 38612 RVA: 0x0032C149 File Offset: 0x0032A349
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.roomId);
			stream.PutOrGet(ref this.roomName);
		}

		// Token: 0x040088D6 RID: 35030
		public int roomId;

		// Token: 0x040088D7 RID: 35031
		public string roomName;
	}
}
