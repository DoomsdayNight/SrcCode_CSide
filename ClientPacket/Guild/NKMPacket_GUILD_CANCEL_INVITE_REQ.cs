using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EE6 RID: 3814
	[PacketId(ClientPacketId.kNKMPacket_GUILD_CANCEL_INVITE_REQ)]
	public sealed class NKMPacket_GUILD_CANCEL_INVITE_REQ : ISerializable
	{
		// Token: 0x060098AC RID: 39084 RVA: 0x0032ED5D File Offset: 0x0032CF5D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.userUid);
		}

		// Token: 0x04008B51 RID: 35665
		public long guildUid;

		// Token: 0x04008B52 RID: 35666
		public long userUid;
	}
}
