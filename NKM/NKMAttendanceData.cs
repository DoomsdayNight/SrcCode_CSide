using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x0200039E RID: 926
	public class NKMAttendanceData : ISerializable
	{
		// Token: 0x06001831 RID: 6193 RVA: 0x00061C09 File Offset: 0x0005FE09
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.LastUpdateDate);
			stream.PutOrGet<NKMAttendance>(ref this.AttList);
		}

		// Token: 0x04001015 RID: 4117
		public DateTime LastUpdateDate;

		// Token: 0x04001016 RID: 4118
		public List<NKMAttendance> AttList = new List<NKMAttendance>();
	}
}
