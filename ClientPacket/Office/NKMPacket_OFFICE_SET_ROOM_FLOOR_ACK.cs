using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DF5 RID: 3573
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_SET_ROOM_FLOOR_ACK)]
	public sealed class NKMPacket_OFFICE_SET_ROOM_FLOOR_ACK : ISerializable
	{
		// Token: 0x060096DE RID: 38622 RVA: 0x0032C22B File Offset: 0x0032A42B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMOfficeRoom>(ref this.room);
			stream.PutOrGet<NKMUnitData>(ref this.updatedUnits);
		}

		// Token: 0x040088E1 RID: 35041
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040088E2 RID: 35042
		public NKMOfficeRoom room = new NKMOfficeRoom();

		// Token: 0x040088E3 RID: 35043
		public List<NKMUnitData> updatedUnits = new List<NKMUnitData>();
	}
}
