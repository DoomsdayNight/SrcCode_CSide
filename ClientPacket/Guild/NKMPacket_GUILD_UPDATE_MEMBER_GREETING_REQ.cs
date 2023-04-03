using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EFC RID: 3836
	[PacketId(ClientPacketId.kNKMPacket_GUILD_UPDATE_MEMBER_GREETING_REQ)]
	public sealed class NKMPacket_GUILD_UPDATE_MEMBER_GREETING_REQ : ISerializable
	{
		// Token: 0x060098D8 RID: 39128 RVA: 0x0032F151 File Offset: 0x0032D351
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.greeting);
		}

		// Token: 0x04008B91 RID: 35729
		public long guildUid;

		// Token: 0x04008B92 RID: 35730
		public string greeting;
	}
}
