using System;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x0200102E RID: 4142
	public sealed class ShopChainTabNextResetData : ISerializable
	{
		// Token: 0x06009B2C RID: 39724 RVA: 0x0033249A File Offset: 0x0033069A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.tabType);
			stream.PutOrGet(ref this.subIndex);
			stream.PutOrGet(ref this.nextResetUtc);
		}

		// Token: 0x04008E7C RID: 36476
		public string tabType;

		// Token: 0x04008E7D RID: 36477
		public int subIndex;

		// Token: 0x04008E7E RID: 36478
		public DateTime nextResetUtc;
	}
}
