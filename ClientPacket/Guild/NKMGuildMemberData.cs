using System;
using ClientPacket.Common;
using Cs.Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EC6 RID: 3782
	public sealed class NKMGuildMemberData : ISerializable
	{
		// Token: 0x0600986C RID: 39020 RVA: 0x0032E644 File Offset: 0x0032C844
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMCommonProfile>(ref this.commonProfile);
			stream.PutOrGet(ref this.createdAt);
			stream.PutOrGetEnum<GuildMemberGrade>(ref this.grade);
			stream.PutOrGet(ref this.lastOnlineTime);
			stream.PutOrGet(ref this.greeting);
			stream.PutOrGet(ref this.lastAttendanceDate);
			stream.PutOrGet(ref this.weeklyContributionPoint);
			stream.PutOrGet(ref this.totalContributionPoint);
			stream.PutOrGet(ref this.hasOffice);
		}

		// Token: 0x04008AE8 RID: 35560
		public NKMCommonProfile commonProfile = new NKMCommonProfile();

		// Token: 0x04008AE9 RID: 35561
		public DateTime createdAt;

		// Token: 0x04008AEA RID: 35562
		public GuildMemberGrade grade;

		// Token: 0x04008AEB RID: 35563
		public DateTime lastOnlineTime;

		// Token: 0x04008AEC RID: 35564
		public string greeting;

		// Token: 0x04008AED RID: 35565
		public DateTime lastAttendanceDate;

		// Token: 0x04008AEE RID: 35566
		public long weeklyContributionPoint;

		// Token: 0x04008AEF RID: 35567
		public long totalContributionPoint;

		// Token: 0x04008AF0 RID: 35568
		public bool hasOffice;
	}
}
