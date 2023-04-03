using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F0A RID: 3850
	[PacketId(ClientPacketId.kNKMPacket_GUILD_RECOMMEND_INVITE_LIST_REQ)]
	public sealed class NKMPacket_GUILD_RECOMMEND_INVITE_LIST_REQ : ISerializable
	{
		// Token: 0x060098F4 RID: 39156 RVA: 0x0032F3C4 File Offset: 0x0032D5C4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
		}

		// Token: 0x04008BB4 RID: 35764
		public long guildUid;
	}
}
