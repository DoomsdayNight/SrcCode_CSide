using System;
using Cs.Protocol;

namespace ClientPacket.Contract
{
	// Token: 0x02000FC5 RID: 4037
	public sealed class NKMInstantContract : ISerializable
	{
		// Token: 0x06009A5A RID: 39514 RVA: 0x00331519 File Offset: 0x0032F719
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.contractId);
			stream.PutOrGet(ref this.endDate);
		}

		// Token: 0x04008DAD RID: 36269
		public int contractId;

		// Token: 0x04008DAE RID: 36270
		public DateTime endDate;
	}
}
