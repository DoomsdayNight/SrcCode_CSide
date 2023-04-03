using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F01 RID: 3841
	[PacketId(ClientPacketId.kNKMPacket_GUILD_DELETED_NOT)]
	public sealed class NKMPacket_GUILD_DELETED_NOT : ISerializable
	{
		// Token: 0x060098E2 RID: 39138 RVA: 0x0032F272 File Offset: 0x0032D472
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
		}

		// Token: 0x04008BA3 RID: 35747
		public long guildUid;
	}
}
