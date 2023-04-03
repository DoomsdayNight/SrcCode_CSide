using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EF6 RID: 3830
	[PacketId(ClientPacketId.kNKMPacket_GUILD_MASTER_SPECIFIED_MIGRATION_ACK)]
	public sealed class NKMPacket_GUILD_MASTER_SPECIFIED_MIGRATION_ACK : ISerializable
	{
		// Token: 0x060098CC RID: 39116 RVA: 0x0032EFF4 File Offset: 0x0032D1F4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.oldMasterUserUid);
			stream.PutOrGet(ref this.newMasterUserUid);
		}

		// Token: 0x04008B7A RID: 35706
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008B7B RID: 35707
		public long guildUid;

		// Token: 0x04008B7C RID: 35708
		public long oldMasterUserUid;

		// Token: 0x04008B7D RID: 35709
		public long newMasterUserUid;
	}
}
