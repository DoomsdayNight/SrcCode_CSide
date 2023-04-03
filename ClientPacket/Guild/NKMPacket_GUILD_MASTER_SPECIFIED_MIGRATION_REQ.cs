using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EF5 RID: 3829
	[PacketId(ClientPacketId.kNKMPacket_GUILD_MASTER_SPECIFIED_MIGRATION_REQ)]
	public sealed class NKMPacket_GUILD_MASTER_SPECIFIED_MIGRATION_REQ : ISerializable
	{
		// Token: 0x060098CA RID: 39114 RVA: 0x0032EFD2 File Offset: 0x0032D1D2
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.targetUserUid);
		}

		// Token: 0x04008B78 RID: 35704
		public long guildUid;

		// Token: 0x04008B79 RID: 35705
		public long targetUserUid;
	}
}
