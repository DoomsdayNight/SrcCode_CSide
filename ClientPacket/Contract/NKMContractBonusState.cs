using System;
using Cs.Protocol;

namespace ClientPacket.Contract
{
	// Token: 0x02000FB6 RID: 4022
	public sealed class NKMContractBonusState : ISerializable
	{
		// Token: 0x06009A40 RID: 39488 RVA: 0x00331228 File Offset: 0x0032F428
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.bonusGroupId);
			stream.PutOrGet(ref this.useCount);
			stream.PutOrGet(ref this.resetCount);
		}

		// Token: 0x04008D80 RID: 36224
		public int bonusGroupId;

		// Token: 0x04008D81 RID: 36225
		public int useCount;

		// Token: 0x04008D82 RID: 36226
		public int resetCount;
	}
}
