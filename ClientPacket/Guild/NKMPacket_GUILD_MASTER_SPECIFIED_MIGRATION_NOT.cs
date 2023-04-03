using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EF7 RID: 3831
	[PacketId(ClientPacketId.kNKMPacket_GUILD_MASTER_SPECIFIED_MIGRATION_NOT)]
	public sealed class NKMPacket_GUILD_MASTER_SPECIFIED_MIGRATION_NOT : ISerializable
	{
		// Token: 0x060098CE RID: 39118 RVA: 0x0032F02E File Offset: 0x0032D22E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.oldMasterUserUid);
			stream.PutOrGet(ref this.newMasterUserUid);
		}

		// Token: 0x04008B7E RID: 35710
		public long guildUid;

		// Token: 0x04008B7F RID: 35711
		public long oldMasterUserUid;

		// Token: 0x04008B80 RID: 35712
		public long newMasterUserUid;
	}
}
