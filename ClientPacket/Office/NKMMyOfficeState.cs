using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DE9 RID: 3561
	public sealed class NKMMyOfficeState : ISerializable
	{
		// Token: 0x060096C6 RID: 38598 RVA: 0x0032BF78 File Offset: 0x0032A178
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.openedSectionIds);
			stream.PutOrGet<NKMOfficeRoom>(ref this.rooms);
			stream.PutOrGet<NKMInteriorData>(ref this.interiors);
			stream.PutOrGet<NKMOfficePostState>(ref this.postState);
			stream.PutOrGet<NKMOfficePreset>(ref this.presets);
		}

		// Token: 0x040088C1 RID: 35009
		public List<int> openedSectionIds = new List<int>();

		// Token: 0x040088C2 RID: 35010
		public List<NKMOfficeRoom> rooms = new List<NKMOfficeRoom>();

		// Token: 0x040088C3 RID: 35011
		public List<NKMInteriorData> interiors = new List<NKMInteriorData>();

		// Token: 0x040088C4 RID: 35012
		public NKMOfficePostState postState = new NKMOfficePostState();

		// Token: 0x040088C5 RID: 35013
		public List<NKMOfficePreset> presets = new List<NKMOfficePreset>();
	}
}
