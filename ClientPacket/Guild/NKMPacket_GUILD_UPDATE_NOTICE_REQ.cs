using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EFA RID: 3834
	[PacketId(ClientPacketId.kNKMPacket_GUILD_UPDATE_NOTICE_REQ)]
	public sealed class NKMPacket_GUILD_UPDATE_NOTICE_REQ : ISerializable
	{
		// Token: 0x060098D4 RID: 39124 RVA: 0x0032F0F5 File Offset: 0x0032D2F5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.notice);
		}

		// Token: 0x04008B8B RID: 35723
		public long guildUid;

		// Token: 0x04008B8C RID: 35724
		public string notice;
	}
}
