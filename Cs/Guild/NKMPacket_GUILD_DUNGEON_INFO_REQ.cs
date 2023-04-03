using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F16 RID: 3862
	[PacketId(ClientPacketId.kNKMPacket_GUILD_DUNGEON_INFO_REQ)]
	public sealed class NKMPacket_GUILD_DUNGEON_INFO_REQ : ISerializable
	{
		// Token: 0x0600990C RID: 39180 RVA: 0x0032F631 File Offset: 0x0032D831
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
		}

		// Token: 0x04008BD7 RID: 35799
		public long guildUid;
	}
}
