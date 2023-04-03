using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EEE RID: 3822
	[PacketId(ClientPacketId.kNKMPacket_GUILD_SET_MEMBER_GRADE_ACK)]
	public sealed class NKMPacket_GUILD_SET_MEMBER_GRADE_ACK : ISerializable
	{
		// Token: 0x060098BC RID: 39100 RVA: 0x0032EE9C File Offset: 0x0032D09C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.targetUserUid);
			stream.PutOrGetEnum<GuildMemberGrade>(ref this.grade);
		}

		// Token: 0x04008B64 RID: 35684
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008B65 RID: 35685
		public long guildUid;

		// Token: 0x04008B66 RID: 35686
		public long targetUserUid;

		// Token: 0x04008B67 RID: 35687
		public GuildMemberGrade grade;
	}
}
