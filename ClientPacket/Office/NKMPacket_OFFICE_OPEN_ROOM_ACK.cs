using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DEF RID: 3567
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_OPEN_ROOM_ACK)]
	public sealed class NKMPacket_OFFICE_OPEN_ROOM_ACK : ISerializable
	{
		// Token: 0x060096D2 RID: 38610 RVA: 0x0032C105 File Offset: 0x0032A305
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItems);
			stream.PutOrGet<NKMOfficeRoom>(ref this.room);
		}

		// Token: 0x040088D3 RID: 35027
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040088D4 RID: 35028
		public List<NKMItemMiscData> costItems = new List<NKMItemMiscData>();

		// Token: 0x040088D5 RID: 35029
		public NKMOfficeRoom room = new NKMOfficeRoom();
	}
}
