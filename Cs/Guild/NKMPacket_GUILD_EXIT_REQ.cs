using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EEB RID: 3819
	[PacketId(ClientPacketId.kNKMPacket_GUILD_EXIT_REQ)]
	public sealed class NKMPacket_GUILD_EXIT_REQ : ISerializable
	{
		// Token: 0x060098B6 RID: 39094 RVA: 0x0032EE2A File Offset: 0x0032D02A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
		}

		// Token: 0x04008B5D RID: 35677
		public long guildUid;
	}
}
