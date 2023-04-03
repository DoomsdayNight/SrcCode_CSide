using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F0F RID: 3855
	[PacketId(ClientPacketId.kNKMPacket_GUILD_BUY_BUFF_REQ)]
	public sealed class NKMPacket_GUILD_BUY_BUFF_REQ : ISerializable
	{
		// Token: 0x060098FE RID: 39166 RVA: 0x0032F4CD File Offset: 0x0032D6CD
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.welfareId);
		}

		// Token: 0x04008BC2 RID: 35778
		public long guildUid;

		// Token: 0x04008BC3 RID: 35779
		public int welfareId;
	}
}
