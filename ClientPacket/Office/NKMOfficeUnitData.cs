using System;
using Cs.Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DEA RID: 3562
	public sealed class NKMOfficeUnitData : ISerializable
	{
		// Token: 0x060096C8 RID: 38600 RVA: 0x0032BFF5 File Offset: 0x0032A1F5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitUid);
			stream.PutOrGet(ref this.unitId);
			stream.PutOrGet(ref this.skinId);
		}

		// Token: 0x040088C6 RID: 35014
		public long unitUid;

		// Token: 0x040088C7 RID: 35015
		public int unitId;

		// Token: 0x040088C8 RID: 35016
		public int skinId;
	}
}
