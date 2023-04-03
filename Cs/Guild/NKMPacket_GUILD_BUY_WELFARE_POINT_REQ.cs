using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F11 RID: 3857
	[PacketId(ClientPacketId.kNKMPacket_GUILD_BUY_WELFARE_POINT_REQ)]
	public sealed class NKMPacket_GUILD_BUY_WELFARE_POINT_REQ : ISerializable
	{
		// Token: 0x06009902 RID: 39170 RVA: 0x0032F558 File Offset: 0x0032D758
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.buyCount);
		}

		// Token: 0x04008BCA RID: 35786
		public long guildUid;

		// Token: 0x04008BCB RID: 35787
		public int buyCount;
	}
}
