using System;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x02001056 RID: 4182
	public sealed class NKMUnitHistory : ISerializable
	{
		// Token: 0x06009B6C RID: 39788 RVA: 0x00332F5D File Offset: 0x0033115D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitId);
			stream.PutOrGet(ref this.maxLevel);
			stream.PutOrGet(ref this.maxLoyalty);
		}

		// Token: 0x04008F3D RID: 36669
		public int unitId;

		// Token: 0x04008F3E RID: 36670
		public int maxLevel;

		// Token: 0x04008F3F RID: 36671
		public int maxLoyalty;
	}
}
