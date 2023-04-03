using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EED RID: 3821
	[PacketId(ClientPacketId.kNKMPacket_GUILD_SET_MEMBER_GRADE_REQ)]
	public sealed class NKMPacket_GUILD_SET_MEMBER_GRADE_REQ : ISerializable
	{
		// Token: 0x060098BA RID: 39098 RVA: 0x0032EE6E File Offset: 0x0032D06E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.targetUserUid);
			stream.PutOrGetEnum<GuildMemberGrade>(ref this.grade);
		}

		// Token: 0x04008B61 RID: 35681
		public long guildUid;

		// Token: 0x04008B62 RID: 35682
		public long targetUserUid;

		// Token: 0x04008B63 RID: 35683
		public GuildMemberGrade grade;
	}
}
