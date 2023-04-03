using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DF7 RID: 3575
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_SET_ROOM_WALL_ACK)]
	public sealed class NKMPacket_OFFICE_SET_ROOM_WALL_ACK : ISerializable
	{
		// Token: 0x060096E2 RID: 38626 RVA: 0x0032C291 File Offset: 0x0032A491
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMOfficeRoom>(ref this.room);
			stream.PutOrGet<NKMUnitData>(ref this.updatedUnits);
		}

		// Token: 0x040088E6 RID: 35046
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040088E7 RID: 35047
		public NKMOfficeRoom room = new NKMOfficeRoom();

		// Token: 0x040088E8 RID: 35048
		public List<NKMUnitData> updatedUnits = new List<NKMUnitData>();
	}
}
