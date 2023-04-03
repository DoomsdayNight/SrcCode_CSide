using System;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x0200105B RID: 4187
	public sealed class NKMTrimIntervalData : ISerializable
	{
		// Token: 0x06009B74 RID: 39796 RVA: 0x00333178 File Offset: 0x00331378
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.trimTryCount);
			stream.PutOrGet(ref this.trimRetryCount);
			stream.PutOrGet(ref this.trimRestoreCount);
		}

		// Token: 0x04008F61 RID: 36705
		public int trimTryCount;

		// Token: 0x04008F62 RID: 36706
		public int trimRetryCount;

		// Token: 0x04008F63 RID: 36707
		public int trimRestoreCount;
	}
}
