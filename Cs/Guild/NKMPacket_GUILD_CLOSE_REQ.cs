using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000ED1 RID: 3793
	[PacketId(ClientPacketId.kNKMPacket_GUILD_CLOSE_REQ)]
	public sealed class NKMPacket_GUILD_CLOSE_REQ : ISerializable
	{
		// Token: 0x06009882 RID: 39042 RVA: 0x0032EA5D File Offset: 0x0032CC5D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
		}

		// Token: 0x04008B28 RID: 35624
		public long guildUid;
	}
}
