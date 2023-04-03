using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x0200039F RID: 927
	public class NKMAttendance : ISerializable
	{
		// Token: 0x06001833 RID: 6195 RVA: 0x00061C36 File Offset: 0x0005FE36
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.IDX);
			stream.PutOrGet(ref this.Count);
			stream.PutOrGet(ref this.EventEndDate);
		}

		// Token: 0x04001017 RID: 4119
		public int IDX;

		// Token: 0x04001018 RID: 4120
		public int Count;

		// Token: 0x04001019 RID: 4121
		public DateTime EventEndDate;
	}
}
