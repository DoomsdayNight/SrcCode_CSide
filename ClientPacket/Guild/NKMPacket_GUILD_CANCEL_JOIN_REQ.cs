using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EDB RID: 3803
	[PacketId(ClientPacketId.kNKMPacket_GUILD_CANCEL_JOIN_REQ)]
	public sealed class NKMPacket_GUILD_CANCEL_JOIN_REQ : ISerializable
	{
		// Token: 0x06009896 RID: 39062 RVA: 0x0032EBAE File Offset: 0x0032CDAE
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
		}

		// Token: 0x04008B39 RID: 35641
		public long guildUid;
	}
}
