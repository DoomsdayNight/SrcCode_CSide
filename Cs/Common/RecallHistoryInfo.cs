using System;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x02001052 RID: 4178
	public sealed class RecallHistoryInfo : ISerializable
	{
		// Token: 0x06009B64 RID: 39780 RVA: 0x00332E20 File Offset: 0x00331020
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitId);
			stream.PutOrGet(ref this.lastUpdateDate);
		}

		// Token: 0x04008F27 RID: 36647
		public int unitId;

		// Token: 0x04008F28 RID: 36648
		public DateTime lastUpdateDate;
	}
}
