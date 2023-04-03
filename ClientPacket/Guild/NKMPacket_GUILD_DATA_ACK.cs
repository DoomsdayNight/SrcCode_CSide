using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EDE RID: 3806
	[PacketId(ClientPacketId.kNKMPacket_GUILD_DATA_ACK)]
	public sealed class NKMPacket_GUILD_DATA_ACK : ISerializable
	{
		// Token: 0x0600989C RID: 39068 RVA: 0x0032EBFC File Offset: 0x0032CDFC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet<NKMGuildData>(ref this.guildData);
		}

		// Token: 0x04008B3D RID: 35645
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008B3E RID: 35646
		public long guildUid;

		// Token: 0x04008B3F RID: 35647
		public NKMGuildData guildData = new NKMGuildData();
	}
}
