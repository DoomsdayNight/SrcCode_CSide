using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DEB RID: 3563
	public sealed class NKMOfficeState : ISerializable
	{
		// Token: 0x060096CA RID: 38602 RVA: 0x0032C023 File Offset: 0x0032A223
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMCommonProfile>(ref this.commonProfile);
			stream.PutOrGet(ref this.openedSectionIds);
			stream.PutOrGet<NKMOfficeRoom>(ref this.rooms);
			stream.PutOrGet<NKMOfficeUnitData>(ref this.units);
		}

		// Token: 0x040088C9 RID: 35017
		public NKMCommonProfile commonProfile = new NKMCommonProfile();

		// Token: 0x040088CA RID: 35018
		public List<int> openedSectionIds = new List<int>();

		// Token: 0x040088CB RID: 35019
		public List<NKMOfficeRoom> rooms = new List<NKMOfficeRoom>();

		// Token: 0x040088CC RID: 35020
		public List<NKMOfficeUnitData> units = new List<NKMOfficeUnitData>();
	}
}
