using System;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EEA RID: 3818
	[PacketId(ClientPacketId.kNKMPacket_GUILD_ACCEPT_INVITE_ACK)]
	public sealed class NKMPacket_GUILD_ACCEPT_INVITE_ACK : ISerializable
	{
		// Token: 0x060098B4 RID: 39092 RVA: 0x0032EDE5 File Offset: 0x0032CFE5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.isAllow);
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet<PrivateGuildData>(ref this.privateGuildData);
		}

		// Token: 0x04008B59 RID: 35673
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008B5A RID: 35674
		public bool isAllow;

		// Token: 0x04008B5B RID: 35675
		public long guildUid;

		// Token: 0x04008B5C RID: 35676
		public PrivateGuildData privateGuildData = new PrivateGuildData();
	}
}
