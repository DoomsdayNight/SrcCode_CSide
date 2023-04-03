using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000ECD RID: 3789
	public sealed class GuildDungeonMemberInfo : ISerializable
	{
		// Token: 0x0600987A RID: 39034 RVA: 0x0032E903 File Offset: 0x0032CB03
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMCommonProfile>(ref this.profile);
			stream.PutOrGet<GuildDungeonMemberArena>(ref this.arenaList);
			stream.PutOrGet(ref this.bossPoint);
		}

		// Token: 0x04008B15 RID: 35605
		public NKMCommonProfile profile = new NKMCommonProfile();

		// Token: 0x04008B16 RID: 35606
		public List<GuildDungeonMemberArena> arenaList = new List<GuildDungeonMemberArena>();

		// Token: 0x04008B17 RID: 35607
		public int bossPoint;
	}
}
