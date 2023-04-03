using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EFE RID: 3838
	[PacketId(ClientPacketId.kNKMPacket_GUILD_ATTENDANCE_REQ)]
	public sealed class NKMPacket_GUILD_ATTENDANCE_REQ : ISerializable
	{
		// Token: 0x060098DC RID: 39132 RVA: 0x0032F195 File Offset: 0x0032D395
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
		}

		// Token: 0x04008B95 RID: 35733
		public long guildUid;
	}
}
