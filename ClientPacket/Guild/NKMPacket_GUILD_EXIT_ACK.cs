using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EEC RID: 3820
	[PacketId(ClientPacketId.kNKMPacket_GUILD_EXIT_ACK)]
	public sealed class NKMPacket_GUILD_EXIT_ACK : ISerializable
	{
		// Token: 0x060098B8 RID: 39096 RVA: 0x0032EE40 File Offset: 0x0032D040
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.joinDisableTime);
		}

		// Token: 0x04008B5E RID: 35678
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008B5F RID: 35679
		public long guildUid;

		// Token: 0x04008B60 RID: 35680
		public DateTime joinDisableTime;
	}
}
