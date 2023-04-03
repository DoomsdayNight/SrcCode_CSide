using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EF9 RID: 3833
	[PacketId(ClientPacketId.kNKMPacket_GUILD_UPDATE_DATA_ACK)]
	public sealed class NKMPacket_GUILD_UPDATE_DATA_ACK : ISerializable
	{
		// Token: 0x060098D2 RID: 39122 RVA: 0x0032F098 File Offset: 0x0032D298
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.greetingBefore);
			stream.PutOrGet(ref this.greeting);
			stream.PutOrGetEnum<GuildJoinType>(ref this.guildJoinType);
			stream.PutOrGet(ref this.badgeId);
		}

		// Token: 0x04008B85 RID: 35717
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008B86 RID: 35718
		public long guildUid;

		// Token: 0x04008B87 RID: 35719
		public string greetingBefore;

		// Token: 0x04008B88 RID: 35720
		public string greeting;

		// Token: 0x04008B89 RID: 35721
		public GuildJoinType guildJoinType;

		// Token: 0x04008B8A RID: 35722
		public long badgeId;
	}
}
