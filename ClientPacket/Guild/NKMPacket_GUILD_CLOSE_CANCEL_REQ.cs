using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000ED3 RID: 3795
	[PacketId(ClientPacketId.kNKMPacket_GUILD_CLOSE_CANCEL_REQ)]
	public sealed class NKMPacket_GUILD_CLOSE_CANCEL_REQ : ISerializable
	{
		// Token: 0x06009886 RID: 39046 RVA: 0x0032EA95 File Offset: 0x0032CC95
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
		}

		// Token: 0x04008B2B RID: 35627
		public long guildUid;
	}
}
