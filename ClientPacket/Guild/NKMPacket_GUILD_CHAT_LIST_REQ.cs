using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F05 RID: 3845
	[PacketId(ClientPacketId.kNKMPacket_GUILD_CHAT_LIST_REQ)]
	public sealed class NKMPacket_GUILD_CHAT_LIST_REQ : ISerializable
	{
		// Token: 0x060098EA RID: 39146 RVA: 0x0032F305 File Offset: 0x0032D505
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
		}

		// Token: 0x04008BAB RID: 35755
		public long guildUid;
	}
}
