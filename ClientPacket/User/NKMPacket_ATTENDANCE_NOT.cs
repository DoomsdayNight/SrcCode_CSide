using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CD2 RID: 3282
	[PacketId(ClientPacketId.kNKMPacket_ATTENDANCE_NOT)]
	public sealed class NKMPacket_ATTENDANCE_NOT : ISerializable
	{
		// Token: 0x060094A1 RID: 38049 RVA: 0x00329038 File Offset: 0x00327238
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.lastUpdateDate);
			stream.PutOrGet<NKMAttendance>(ref this.attendanceData);
		}

		// Token: 0x04008622 RID: 34338
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008623 RID: 34339
		public long lastUpdateDate;

		// Token: 0x04008624 RID: 34340
		public List<NKMAttendance> attendanceData = new List<NKMAttendance>();
	}
}
