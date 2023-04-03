using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000ED9 RID: 3801
	[PacketId(ClientPacketId.kNKMPacket_GUILD_JOIN_REQ)]
	public sealed class NKMPacket_GUILD_JOIN_REQ : ISerializable
	{
		// Token: 0x06009892 RID: 39058 RVA: 0x0032EB47 File Offset: 0x0032CD47
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGetEnum<GuildJoinType>(ref this.guildJoinType);
		}

		// Token: 0x04008B33 RID: 35635
		public long guildUid;

		// Token: 0x04008B34 RID: 35636
		public GuildJoinType guildJoinType;
	}
}
