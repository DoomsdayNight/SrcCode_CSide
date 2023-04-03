using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EF3 RID: 3827
	[PacketId(ClientPacketId.kNKMPacket_GUILD_MASTER_MIGRATION_REQ)]
	public sealed class NKMPacket_GUILD_MASTER_MIGRATION_REQ : ISerializable
	{
		// Token: 0x060098C6 RID: 39110 RVA: 0x0032EF82 File Offset: 0x0032D182
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
		}

		// Token: 0x04008B73 RID: 35699
		public long guildUid;
	}
}
