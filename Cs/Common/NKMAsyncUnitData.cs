using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;

namespace ClientPacket.Common
{
	// Token: 0x0200103F RID: 4159
	public sealed class NKMAsyncUnitData : ISerializable
	{
		// Token: 0x06009B40 RID: 39744 RVA: 0x003327D8 File Offset: 0x003309D8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitId);
			stream.PutOrGet(ref this.unitLevel);
			stream.PutOrGet(ref this.skinId);
			stream.PutOrGet(ref this.limitBreakLevel);
			stream.PutOrGet(ref this.skillLevel);
			stream.PutOrGet(ref this.statExp);
			stream.PutOrGet(ref this.equipUids);
			stream.PutOrGet<NKMShipCmdModule>(ref this.shipModules);
			stream.PutOrGet(ref this.tacticLevel);
		}

		// Token: 0x04008EC8 RID: 36552
		public int unitId;

		// Token: 0x04008EC9 RID: 36553
		public int unitLevel;

		// Token: 0x04008ECA RID: 36554
		public int skinId;

		// Token: 0x04008ECB RID: 36555
		public int limitBreakLevel;

		// Token: 0x04008ECC RID: 36556
		public List<int> skillLevel = new List<int>();

		// Token: 0x04008ECD RID: 36557
		public List<int> statExp = new List<int>();

		// Token: 0x04008ECE RID: 36558
		public List<long> equipUids = new List<long>();

		// Token: 0x04008ECF RID: 36559
		public List<NKMShipCmdModule> shipModules = new List<NKMShipCmdModule>();

		// Token: 0x04008ED0 RID: 36560
		public int tacticLevel;
	}
}
