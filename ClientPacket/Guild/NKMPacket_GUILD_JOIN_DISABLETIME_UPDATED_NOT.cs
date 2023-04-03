using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F09 RID: 3849
	[PacketId(ClientPacketId.kNKMPacket_GUILD_JOIN_DISABLETIME_UPDATED_NOT)]
	public sealed class NKMPacket_GUILD_JOIN_DISABLETIME_UPDATED_NOT : ISerializable
	{
		// Token: 0x060098F2 RID: 39154 RVA: 0x0032F3AE File Offset: 0x0032D5AE
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.joinDisableTime);
		}

		// Token: 0x04008BB3 RID: 35763
		public DateTime joinDisableTime;
	}
}
