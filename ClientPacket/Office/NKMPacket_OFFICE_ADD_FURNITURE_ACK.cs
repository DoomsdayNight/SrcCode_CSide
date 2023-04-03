using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DFB RID: 3579
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_ADD_FURNITURE_ACK)]
	public sealed class NKMPacket_OFFICE_ADD_FURNITURE_ACK : ISerializable
	{
		// Token: 0x060096EA RID: 38634 RVA: 0x0032C399 File Offset: 0x0032A599
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMOfficeRoom>(ref this.room);
			stream.PutOrGet<NKMOfficeFurniture>(ref this.furniture);
			stream.PutOrGet<NKMInteriorData>(ref this.changedInterior);
			stream.PutOrGet<NKMUnitData>(ref this.updatedUnits);
		}

		// Token: 0x040088F4 RID: 35060
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040088F5 RID: 35061
		public NKMOfficeRoom room = new NKMOfficeRoom();

		// Token: 0x040088F6 RID: 35062
		public NKMOfficeFurniture furniture = new NKMOfficeFurniture();

		// Token: 0x040088F7 RID: 35063
		public NKMInteriorData changedInterior = new NKMInteriorData();

		// Token: 0x040088F8 RID: 35064
		public List<NKMUnitData> updatedUnits = new List<NKMUnitData>();
	}
}
