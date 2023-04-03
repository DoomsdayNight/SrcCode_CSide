using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EE8 RID: 3816
	[PacketId(ClientPacketId.kNKMPacket_GUILD_CANCEL_REQUEST_NOT)]
	public sealed class NKMPacket_GUILD_CANCEL_REQUEST_NOT : ISerializable
	{
		// Token: 0x060098B0 RID: 39088 RVA: 0x0032EDA1 File Offset: 0x0032CFA1
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.isRequest);
		}

		// Token: 0x04008B55 RID: 35669
		public long guildUid;

		// Token: 0x04008B56 RID: 35670
		public bool isRequest;
	}
}
