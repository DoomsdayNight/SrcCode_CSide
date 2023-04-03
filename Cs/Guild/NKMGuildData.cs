using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EC8 RID: 3784
	public sealed class NKMGuildData : ISerializable
	{
		// Token: 0x06009870 RID: 39024 RVA: 0x0032E6F4 File Offset: 0x0032C8F4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.name);
			stream.PutOrGet(ref this.badgeId);
			stream.PutOrGet(ref this.guildLevel);
			stream.PutOrGet(ref this.guildLevelExp);
			stream.PutOrGetEnum<GuildJoinType>(ref this.guildJoinType);
			stream.PutOrGetEnum<GuildState>(ref this.guildState);
			stream.PutOrGet(ref this.closingTime);
			stream.PutOrGet(ref this.greeting);
			stream.PutOrGet(ref this.notice);
			stream.PutOrGet<FriendListData>(ref this.inviteList);
			stream.PutOrGet<FriendListData>(ref this.joinWaitingList);
			stream.PutOrGet<NKMGuildMemberData>(ref this.members);
			stream.PutOrGet<NKMGuildAttendanceData>(ref this.attendanceList);
			stream.PutOrGet(ref this.unionPoint);
		}

		// Token: 0x04008AF3 RID: 35571
		public long guildUid;

		// Token: 0x04008AF4 RID: 35572
		public string name;

		// Token: 0x04008AF5 RID: 35573
		public long badgeId;

		// Token: 0x04008AF6 RID: 35574
		public int guildLevel;

		// Token: 0x04008AF7 RID: 35575
		public long guildLevelExp;

		// Token: 0x04008AF8 RID: 35576
		public GuildJoinType guildJoinType;

		// Token: 0x04008AF9 RID: 35577
		public GuildState guildState;

		// Token: 0x04008AFA RID: 35578
		public DateTime closingTime;

		// Token: 0x04008AFB RID: 35579
		public string greeting;

		// Token: 0x04008AFC RID: 35580
		public string notice;

		// Token: 0x04008AFD RID: 35581
		public List<FriendListData> inviteList = new List<FriendListData>();

		// Token: 0x04008AFE RID: 35582
		public List<FriendListData> joinWaitingList = new List<FriendListData>();

		// Token: 0x04008AFF RID: 35583
		public List<NKMGuildMemberData> members = new List<NKMGuildMemberData>();

		// Token: 0x04008B00 RID: 35584
		public List<NKMGuildAttendanceData> attendanceList = new List<NKMGuildAttendanceData>();

		// Token: 0x04008B01 RID: 35585
		public long unionPoint;
	}
}
