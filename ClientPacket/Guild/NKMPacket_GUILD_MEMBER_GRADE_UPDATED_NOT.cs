using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EEF RID: 3823
	[PacketId(ClientPacketId.kNKMPacket_GUILD_MEMBER_GRADE_UPDATED_NOT)]
	public sealed class NKMPacket_GUILD_MEMBER_GRADE_UPDATED_NOT : ISerializable
	{
		// Token: 0x060098BE RID: 39102 RVA: 0x0032EED6 File Offset: 0x0032D0D6
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGetEnum<GuildMemberGrade>(ref this.gradeBefore);
			stream.PutOrGetEnum<GuildMemberGrade>(ref this.gradeAfter);
		}

		// Token: 0x04008B68 RID: 35688
		public long guildUid;

		// Token: 0x04008B69 RID: 35689
		public GuildMemberGrade gradeBefore;

		// Token: 0x04008B6A RID: 35690
		public GuildMemberGrade gradeAfter;
	}
}
