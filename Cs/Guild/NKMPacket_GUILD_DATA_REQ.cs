using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EDD RID: 3805
	[PacketId(ClientPacketId.kNKMPacket_GUILD_DATA_REQ)]
	public sealed class NKMPacket_GUILD_DATA_REQ : ISerializable
	{
		// Token: 0x0600989A RID: 39066 RVA: 0x0032EBE6 File Offset: 0x0032CDE6
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
		}

		// Token: 0x04008B3C RID: 35644
		public long guildUid;
	}
}
