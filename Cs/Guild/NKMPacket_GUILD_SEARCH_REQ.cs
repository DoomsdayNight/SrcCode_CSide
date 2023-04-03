using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000ED5 RID: 3797
	[PacketId(ClientPacketId.kNKMPacket_GUILD_SEARCH_REQ)]
	public sealed class NKMPacket_GUILD_SEARCH_REQ : ISerializable
	{
		// Token: 0x0600988A RID: 39050 RVA: 0x0032EAC1 File Offset: 0x0032CCC1
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.keyword);
		}

		// Token: 0x04008B2D RID: 35629
		public string keyword;
	}
}
