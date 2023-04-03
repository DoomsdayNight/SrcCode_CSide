using System;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x02001053 RID: 4179
	public sealed class NKMServerKillCountData : ISerializable
	{
		// Token: 0x06009B66 RID: 39782 RVA: 0x00332E42 File Offset: 0x00331042
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.killCountId);
			stream.PutOrGet(ref this.killCount);
		}

		// Token: 0x04008F29 RID: 36649
		public int killCountId;

		// Token: 0x04008F2A RID: 36650
		public long killCount;
	}
}
