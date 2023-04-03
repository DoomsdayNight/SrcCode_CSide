using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EF1 RID: 3825
	[PacketId(ClientPacketId.kNKMPacket_GUILD_BAN_ACK)]
	public sealed class NKMPacket_GUILD_BAN_ACK : ISerializable
	{
		// Token: 0x060098C2 RID: 39106 RVA: 0x0032EF32 File Offset: 0x0032D132
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.targetUserUid);
		}

		// Token: 0x04008B6E RID: 35694
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008B6F RID: 35695
		public long guildUid;

		// Token: 0x04008B70 RID: 35696
		public long targetUserUid;
	}
}
