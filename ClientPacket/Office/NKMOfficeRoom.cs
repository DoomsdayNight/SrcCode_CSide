using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM.Templet.Office;

namespace ClientPacket.Office
{
	// Token: 0x02000DE5 RID: 3557
	public sealed class NKMOfficeRoom : ISerializable
	{
		// Token: 0x060096BE RID: 38590 RVA: 0x0032BE1C File Offset: 0x0032A01C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.id);
			stream.PutOrGet(ref this.name);
			stream.PutOrGetEnum<OfficeGrade>(ref this.grade);
			stream.PutOrGet(ref this.interiorScore);
			stream.PutOrGet<NKMOfficeFurniture>(ref this.furnitures);
			stream.PutOrGet(ref this.unitUids);
			stream.PutOrGet(ref this.floorInteriorId);
			stream.PutOrGet(ref this.wallInteriorId);
			stream.PutOrGet(ref this.backgroundId);
		}

		// Token: 0x040088AC RID: 34988
		public int id;

		// Token: 0x040088AD RID: 34989
		public string name;

		// Token: 0x040088AE RID: 34990
		public OfficeGrade grade;

		// Token: 0x040088AF RID: 34991
		public int interiorScore;

		// Token: 0x040088B0 RID: 34992
		public List<NKMOfficeFurniture> furnitures = new List<NKMOfficeFurniture>();

		// Token: 0x040088B1 RID: 34993
		public List<long> unitUids = new List<long>();

		// Token: 0x040088B2 RID: 34994
		public int floorInteriorId;

		// Token: 0x040088B3 RID: 34995
		public int wallInteriorId;

		// Token: 0x040088B4 RID: 34996
		public int backgroundId;
	}
}
