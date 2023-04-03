using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DF3 RID: 3571
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_SET_ROOM_UNIT_ACK)]
	public sealed class NKMPacket_OFFICE_SET_ROOM_UNIT_ACK : ISerializable
	{
		// Token: 0x060096DA RID: 38618 RVA: 0x0032C1C5 File Offset: 0x0032A3C5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMUnitData>(ref this.units);
			stream.PutOrGet<NKMOfficeRoom>(ref this.rooms);
		}

		// Token: 0x040088DC RID: 35036
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040088DD RID: 35037
		public List<NKMUnitData> units = new List<NKMUnitData>();

		// Token: 0x040088DE RID: 35038
		public List<NKMOfficeRoom> rooms = new List<NKMOfficeRoom>();
	}
}
