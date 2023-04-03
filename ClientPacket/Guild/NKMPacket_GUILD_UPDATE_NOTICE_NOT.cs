using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F0E RID: 3854
	[PacketId(ClientPacketId.kNKMPacket_GUILD_UPDATE_NOTICE_NOT)]
	public sealed class NKMPacket_GUILD_UPDATE_NOTICE_NOT : ISerializable
	{
		// Token: 0x060098FC RID: 39164 RVA: 0x0032F4AB File Offset: 0x0032D6AB
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.notice);
		}

		// Token: 0x04008BC0 RID: 35776
		public long guildUid;

		// Token: 0x04008BC1 RID: 35777
		public string notice;
	}
}
