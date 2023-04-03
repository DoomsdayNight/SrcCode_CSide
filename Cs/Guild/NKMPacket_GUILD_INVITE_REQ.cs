using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EE3 RID: 3811
	[PacketId(ClientPacketId.kNKMPacket_GUILD_INVITE_REQ)]
	public sealed class NKMPacket_GUILD_INVITE_REQ : ISerializable
	{
		// Token: 0x060098A6 RID: 39078 RVA: 0x0032ED03 File Offset: 0x0032CF03
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.userUid);
		}

		// Token: 0x04008B4C RID: 35660
		public long guildUid;

		// Token: 0x04008B4D RID: 35661
		public long userUid;
	}
}
