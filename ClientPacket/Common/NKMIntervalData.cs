using System;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x02001058 RID: 4184
	public sealed class NKMIntervalData : ISerializable
	{
		// Token: 0x06009B70 RID: 39792 RVA: 0x00333098 File Offset: 0x00331298
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.key);
			stream.PutOrGet(ref this.strKey);
			stream.PutOrGet(ref this.startDate);
			stream.PutOrGet(ref this.endDate);
			stream.PutOrGet(ref this.repeatStartDate);
			stream.PutOrGet(ref this.repeatEndDate);
		}

		// Token: 0x04008F4A RID: 36682
		public int key;

		// Token: 0x04008F4B RID: 36683
		public string strKey;

		// Token: 0x04008F4C RID: 36684
		public DateTime startDate;

		// Token: 0x04008F4D RID: 36685
		public DateTime endDate;

		// Token: 0x04008F4E RID: 36686
		public int repeatStartDate;

		// Token: 0x04008F4F RID: 36687
		public int repeatEndDate;
	}
}
