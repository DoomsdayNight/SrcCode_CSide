using System;
using Cs.Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EC7 RID: 3783
	public sealed class NKMGuildAttendanceData : ISerializable
	{
		// Token: 0x0600986E RID: 39022 RVA: 0x0032E6D0 File Offset: 0x0032C8D0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.date);
			stream.PutOrGet(ref this.count);
		}

		// Token: 0x04008AF1 RID: 35569
		public DateTime date;

		// Token: 0x04008AF2 RID: 35570
		public int count;
	}
}
