using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EE0 RID: 3808
	[PacketId(ClientPacketId.kNKMPacket_GUILD_ACCEPT_JOIN_REQ)]
	public sealed class NKMPacket_GUILD_ACCEPT_JOIN_REQ : ISerializable
	{
		// Token: 0x060098A0 RID: 39072 RVA: 0x0032EC56 File Offset: 0x0032CE56
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.joinUserUid);
			stream.PutOrGet(ref this.isAllow);
		}

		// Token: 0x04008B41 RID: 35649
		public long guildUid;

		// Token: 0x04008B42 RID: 35650
		public long joinUserUid;

		// Token: 0x04008B43 RID: 35651
		public bool isAllow;
	}
}
