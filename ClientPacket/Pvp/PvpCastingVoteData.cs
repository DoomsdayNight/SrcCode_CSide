using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DBE RID: 3518
	public sealed class PvpCastingVoteData : ISerializable
	{
		// Token: 0x06009675 RID: 38517 RVA: 0x0032B906 File Offset: 0x00329B06
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitIdList);
			stream.PutOrGet(ref this.shipIdList);
			stream.PutOrGet(ref this.operatorIdList);
		}

		// Token: 0x04008868 RID: 34920
		public List<int> unitIdList = new List<int>();

		// Token: 0x04008869 RID: 34921
		public List<int> shipIdList = new List<int>();

		// Token: 0x0400886A RID: 34922
		public List<int> operatorIdList = new List<int>();
	}
}
