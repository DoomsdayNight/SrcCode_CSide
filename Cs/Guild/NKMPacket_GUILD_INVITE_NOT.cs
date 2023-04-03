using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EE5 RID: 3813
	[PacketId(ClientPacketId.kNKMPacket_GUILD_INVITE_NOT)]
	public sealed class NKMPacket_GUILD_INVITE_NOT : ISerializable
	{
		// Token: 0x060098AA RID: 39082 RVA: 0x0032ED47 File Offset: 0x0032CF47
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
		}

		// Token: 0x04008B50 RID: 35664
		public long guildUid;
	}
}
