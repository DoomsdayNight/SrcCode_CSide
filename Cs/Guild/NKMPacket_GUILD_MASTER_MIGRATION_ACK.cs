using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EF4 RID: 3828
	[PacketId(ClientPacketId.kNKMPacket_GUILD_MASTER_MIGRATION_ACK)]
	public sealed class NKMPacket_GUILD_MASTER_MIGRATION_ACK : ISerializable
	{
		// Token: 0x060098C8 RID: 39112 RVA: 0x0032EF98 File Offset: 0x0032D198
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.oldMasterUserUid);
			stream.PutOrGet(ref this.newMasterUserUid);
		}

		// Token: 0x04008B74 RID: 35700
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008B75 RID: 35701
		public long guildUid;

		// Token: 0x04008B76 RID: 35702
		public long oldMasterUserUid;

		// Token: 0x04008B77 RID: 35703
		public long newMasterUserUid;
	}
}
