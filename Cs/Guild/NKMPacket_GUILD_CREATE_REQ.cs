using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000ECF RID: 3791
	[PacketId(ClientPacketId.kNKMPacket_GUILD_CREATE_REQ)]
	public sealed class NKMPacket_GUILD_CREATE_REQ : ISerializable
	{
		// Token: 0x0600987E RID: 39038 RVA: 0x0032E9C8 File Offset: 0x0032CBC8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildName);
			stream.PutOrGetEnum<GuildJoinType>(ref this.guildJoinType);
			stream.PutOrGet(ref this.badgeId);
			stream.PutOrGet(ref this.greeting);
		}

		// Token: 0x04008B20 RID: 35616
		public string guildName;

		// Token: 0x04008B21 RID: 35617
		public GuildJoinType guildJoinType;

		// Token: 0x04008B22 RID: 35618
		public long badgeId;

		// Token: 0x04008B23 RID: 35619
		public string greeting;
	}
}
