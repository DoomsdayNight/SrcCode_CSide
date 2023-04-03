using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F18 RID: 3864
	[PacketId(ClientPacketId.kNKMPacket_GUILD_DUNGEON_MEMBER_INFO_REQ)]
	public sealed class NKMPacket_GUILD_DUNGEON_MEMBER_INFO_REQ : ISerializable
	{
		// Token: 0x06009910 RID: 39184 RVA: 0x0032F71B File Offset: 0x0032D91B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
		}

		// Token: 0x04008BE6 RID: 35814
		public long guildUid;
	}
}
