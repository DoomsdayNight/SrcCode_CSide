using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EE9 RID: 3817
	[PacketId(ClientPacketId.kNKMPacket_GUILD_ACCEPT_INVITE_REQ)]
	public sealed class NKMPacket_GUILD_ACCEPT_INVITE_REQ : ISerializable
	{
		// Token: 0x060098B2 RID: 39090 RVA: 0x0032EDC3 File Offset: 0x0032CFC3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.isAllow);
		}

		// Token: 0x04008B57 RID: 35671
		public long guildUid;

		// Token: 0x04008B58 RID: 35672
		public bool isAllow;
	}
}
