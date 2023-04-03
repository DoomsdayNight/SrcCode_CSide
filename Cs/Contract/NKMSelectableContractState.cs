using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.Contract
{
	// Token: 0x02000FB7 RID: 4023
	public sealed class NKMSelectableContractState : ISerializable
	{
		// Token: 0x06009A42 RID: 39490 RVA: 0x00331256 File Offset: 0x0032F456
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.contractId);
			stream.PutOrGet(ref this.unitIdList);
			stream.PutOrGet(ref this.unitPoolChangeCount);
			stream.PutOrGet(ref this.isActive);
		}

		// Token: 0x04008D83 RID: 36227
		public int contractId;

		// Token: 0x04008D84 RID: 36228
		public List<int> unitIdList = new List<int>();

		// Token: 0x04008D85 RID: 36229
		public int unitPoolChangeCount;

		// Token: 0x04008D86 RID: 36230
		public bool isActive;
	}
}
