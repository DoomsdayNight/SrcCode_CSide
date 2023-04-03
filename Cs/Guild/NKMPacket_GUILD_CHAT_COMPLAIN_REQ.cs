using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F13 RID: 3859
	[PacketId(ClientPacketId.kNKMPacket_GUILD_CHAT_COMPLAIN_REQ)]
	public sealed class NKMPacket_GUILD_CHAT_COMPLAIN_REQ : ISerializable
	{
		// Token: 0x06009906 RID: 39174 RVA: 0x0032F5BF File Offset: 0x0032D7BF
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.messageUid);
		}

		// Token: 0x04008BD0 RID: 35792
		public long guildUid;

		// Token: 0x04008BD1 RID: 35793
		public long messageUid;
	}
}
