using System;
using ClientPacket.Common;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EE2 RID: 3810
	[PacketId(ClientPacketId.kNKMPacket_GUILD_ACCEPT_JOIN_NOT)]
	public sealed class NKMPacket_GUILD_ACCEPT_JOIN_NOT : ISerializable
	{
		// Token: 0x060098A4 RID: 39076 RVA: 0x0032ECBE File Offset: 0x0032CEBE
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.isAllow);
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.guildName);
			stream.PutOrGet<PrivateGuildData>(ref this.privateGuildData);
		}

		// Token: 0x04008B48 RID: 35656
		public bool isAllow;

		// Token: 0x04008B49 RID: 35657
		public long guildUid;

		// Token: 0x04008B4A RID: 35658
		public string guildName;

		// Token: 0x04008B4B RID: 35659
		public PrivateGuildData privateGuildData = new PrivateGuildData();
	}
}
