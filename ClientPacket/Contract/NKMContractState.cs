using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.Contract
{
	// Token: 0x02000FB5 RID: 4021
	public sealed class NKMContractState : ISerializable
	{
		// Token: 0x06009A3E RID: 39486 RVA: 0x003311B4 File Offset: 0x0032F3B4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.contractId);
			stream.PutOrGet(ref this.remainFreeChance);
			stream.PutOrGet(ref this.nextResetDate);
			stream.PutOrGet(ref this.isActive);
			stream.PutOrGet(ref this.totalUseCount);
			stream.PutOrGet(ref this.dailyUseCount);
			stream.PutOrGet(ref this.bonusCandidate);
		}

		// Token: 0x04008D79 RID: 36217
		public int contractId;

		// Token: 0x04008D7A RID: 36218
		public int remainFreeChance;

		// Token: 0x04008D7B RID: 36219
		public DateTime nextResetDate;

		// Token: 0x04008D7C RID: 36220
		public bool isActive;

		// Token: 0x04008D7D RID: 36221
		public int totalUseCount;

		// Token: 0x04008D7E RID: 36222
		public int dailyUseCount;

		// Token: 0x04008D7F RID: 36223
		public List<int> bonusCandidate = new List<int>();
	}
}
