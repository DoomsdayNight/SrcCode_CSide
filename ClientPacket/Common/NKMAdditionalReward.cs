using System;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x0200104B RID: 4171
	public sealed class NKMAdditionalReward : ISerializable
	{
		// Token: 0x06009B56 RID: 39766 RVA: 0x00332CE1 File Offset: 0x00330EE1
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildExpDelta);
			stream.PutOrGet(ref this.unionPointDelta);
			stream.PutOrGet(ref this.eventPassExpDelta);
		}

		// Token: 0x04008F15 RID: 36629
		public long guildExpDelta;

		// Token: 0x04008F16 RID: 36630
		public long unionPointDelta;

		// Token: 0x04008F17 RID: 36631
		public long eventPassExpDelta;
	}
}
