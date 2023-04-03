using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000ED7 RID: 3799
	[PacketId(ClientPacketId.kNKMPacket_GUILD_LIST_REQ)]
	public sealed class NKMPacket_GUILD_LIST_REQ : ISerializable
	{
		// Token: 0x0600988E RID: 39054 RVA: 0x0032EB04 File Offset: 0x0032CD04
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<GuildListType>(ref this.guildListType);
		}

		// Token: 0x04008B30 RID: 35632
		public GuildListType guildListType;
	}
}
