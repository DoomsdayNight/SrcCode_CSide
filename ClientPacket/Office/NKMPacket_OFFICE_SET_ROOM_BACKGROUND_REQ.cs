using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DF8 RID: 3576
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_SET_ROOM_BACKGROUND_REQ)]
	public sealed class NKMPacket_OFFICE_SET_ROOM_BACKGROUND_REQ : ISerializable
	{
		// Token: 0x060096E4 RID: 38628 RVA: 0x0032C2D5 File Offset: 0x0032A4D5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.roomId);
			stream.PutOrGet(ref this.backgroundId);
		}

		// Token: 0x040088E9 RID: 35049
		public int roomId;

		// Token: 0x040088EA RID: 35050
		public int backgroundId;
	}
}
