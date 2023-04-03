using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EF0 RID: 3824
	[PacketId(ClientPacketId.kNKMPacket_GUILD_BAN_REQ)]
	public sealed class NKMPacket_GUILD_BAN_REQ : ISerializable
	{
		// Token: 0x060098C0 RID: 39104 RVA: 0x0032EF04 File Offset: 0x0032D104
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.targetUserUid);
			stream.PutOrGet(ref this.banReason);
		}

		// Token: 0x04008B6B RID: 35691
		public long guildUid;

		// Token: 0x04008B6C RID: 35692
		public long targetUserUid;

		// Token: 0x04008B6D RID: 35693
		public int banReason;
	}
}
