using System;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x02001054 RID: 4180
	public sealed class NKMKillCountData : ISerializable
	{
		// Token: 0x06009B68 RID: 39784 RVA: 0x00332E64 File Offset: 0x00331064
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.killCountId);
			stream.PutOrGet(ref this.killCount);
			stream.PutOrGet(ref this.userCompleteStep);
			stream.PutOrGet(ref this.serverCompleteStep);
		}

		// Token: 0x04008F2B RID: 36651
		public int killCountId;

		// Token: 0x04008F2C RID: 36652
		public long killCount;

		// Token: 0x04008F2D RID: 36653
		public int userCompleteStep;

		// Token: 0x04008F2E RID: 36654
		public int serverCompleteStep;
	}
}
