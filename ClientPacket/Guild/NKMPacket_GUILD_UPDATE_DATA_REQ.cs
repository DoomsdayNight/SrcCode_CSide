using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EF8 RID: 3832
	[PacketId(ClientPacketId.kNKMPacket_GUILD_UPDATE_DATA_REQ)]
	public sealed class NKMPacket_GUILD_UPDATE_DATA_REQ : ISerializable
	{
		// Token: 0x060098D0 RID: 39120 RVA: 0x0032F05C File Offset: 0x0032D25C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.greeting);
			stream.PutOrGetEnum<GuildJoinType>(ref this.guildJoinType);
			stream.PutOrGet(ref this.badgeId);
		}

		// Token: 0x04008B81 RID: 35713
		public long guildUid;

		// Token: 0x04008B82 RID: 35714
		public string greeting;

		// Token: 0x04008B83 RID: 35715
		public GuildJoinType guildJoinType;

		// Token: 0x04008B84 RID: 35716
		public long badgeId;
	}
}
