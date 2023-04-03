using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F6E RID: 3950
	public sealed class BingoInfo : ISerializable
	{
		// Token: 0x060099B8 RID: 39352 RVA: 0x003306B0 File Offset: 0x0032E8B0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.eventId);
			stream.PutOrGet(ref this.tileValueList);
			stream.PutOrGet(ref this.markTileIndexList);
			stream.PutOrGet(ref this.rewardList);
			stream.PutOrGet(ref this.mileage);
		}

		// Token: 0x04008CCC RID: 36044
		public int eventId;

		// Token: 0x04008CCD RID: 36045
		public List<int> tileValueList = new List<int>();

		// Token: 0x04008CCE RID: 36046
		public List<int> markTileIndexList = new List<int>();

		// Token: 0x04008CCF RID: 36047
		public List<int> rewardList = new List<int>();

		// Token: 0x04008CD0 RID: 36048
		public int mileage;
	}
}
