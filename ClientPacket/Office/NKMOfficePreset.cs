using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DE8 RID: 3560
	public sealed class NKMOfficePreset : ISerializable
	{
		// Token: 0x060096C4 RID: 38596 RVA: 0x0032BF10 File Offset: 0x0032A110
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.presetId);
			stream.PutOrGet(ref this.name);
			stream.PutOrGet<NKMOfficeFurniture>(ref this.furnitures);
			stream.PutOrGet(ref this.floorInteriorId);
			stream.PutOrGet(ref this.wallInteriorId);
			stream.PutOrGet(ref this.backgroundId);
		}

		// Token: 0x040088BB RID: 35003
		public int presetId;

		// Token: 0x040088BC RID: 35004
		public string name;

		// Token: 0x040088BD RID: 35005
		public List<NKMOfficeFurniture> furnitures = new List<NKMOfficeFurniture>();

		// Token: 0x040088BE RID: 35006
		public int floorInteriorId;

		// Token: 0x040088BF RID: 35007
		public int wallInteriorId;

		// Token: 0x040088C0 RID: 35008
		public int backgroundId;
	}
}
