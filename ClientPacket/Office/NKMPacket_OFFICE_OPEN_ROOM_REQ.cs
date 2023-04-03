using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DEE RID: 3566
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_OPEN_ROOM_REQ)]
	public sealed class NKMPacket_OFFICE_OPEN_ROOM_REQ : ISerializable
	{
		// Token: 0x060096D0 RID: 38608 RVA: 0x0032C0EF File Offset: 0x0032A2EF
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.roomId);
		}

		// Token: 0x040088D2 RID: 35026
		public int roomId;
	}
}
