using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EF2 RID: 3826
	[PacketId(ClientPacketId.kNKMPacket_GUILD_BAN_NOT)]
	public sealed class NKMPacket_GUILD_BAN_NOT : ISerializable
	{
		// Token: 0x060098C4 RID: 39108 RVA: 0x0032EF60 File Offset: 0x0032D160
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.banReason);
		}

		// Token: 0x04008B71 RID: 35697
		public long guildUid;

		// Token: 0x04008B72 RID: 35698
		public int banReason;
	}
}
