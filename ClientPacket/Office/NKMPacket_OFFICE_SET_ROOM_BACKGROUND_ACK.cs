using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DF9 RID: 3577
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_SET_ROOM_BACKGROUND_ACK)]
	public sealed class NKMPacket_OFFICE_SET_ROOM_BACKGROUND_ACK : ISerializable
	{
		// Token: 0x060096E6 RID: 38630 RVA: 0x0032C2F7 File Offset: 0x0032A4F7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMOfficeRoom>(ref this.room);
			stream.PutOrGet<NKMUnitData>(ref this.updatedUnits);
		}

		// Token: 0x040088EB RID: 35051
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040088EC RID: 35052
		public NKMOfficeRoom room = new NKMOfficeRoom();

		// Token: 0x040088ED RID: 35053
		public List<NKMUnitData> updatedUnits = new List<NKMUnitData>();
	}
}
