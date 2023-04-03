using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F00 RID: 3840
	[PacketId(ClientPacketId.kNKMPacket_GUILD_LEVEL_UP_NOT)]
	public sealed class NKMPacket_GUILD_LEVEL_UP_NOT : ISerializable
	{
		// Token: 0x060098E0 RID: 39136 RVA: 0x0032F22C File Offset: 0x0032D42C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.guildLevel);
			stream.PutOrGet(ref this.guildLevelExp);
			stream.PutOrGet(ref this.guildTotalExp);
			stream.PutOrGet(ref this.levelUpTime);
		}

		// Token: 0x04008B9E RID: 35742
		public long guildUid;

		// Token: 0x04008B9F RID: 35743
		public int guildLevel;

		// Token: 0x04008BA0 RID: 35744
		public long guildLevelExp;

		// Token: 0x04008BA1 RID: 35745
		public long guildTotalExp;

		// Token: 0x04008BA2 RID: 35746
		public DateTime levelUpTime;
	}
}
