using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DF1 RID: 3569
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_SET_ROOM_NAME_ACK)]
	public sealed class NKMPacket_OFFICE_SET_ROOM_NAME_ACK : ISerializable
	{
		// Token: 0x060096D6 RID: 38614 RVA: 0x0032C16B File Offset: 0x0032A36B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMOfficeRoom>(ref this.room);
		}

		// Token: 0x040088D8 RID: 35032
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040088D9 RID: 35033
		public NKMOfficeRoom room = new NKMOfficeRoom();
	}
}
