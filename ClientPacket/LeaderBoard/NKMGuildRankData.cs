using System;
using Cs.Protocol;

namespace ClientPacket.LeaderBoard
{
	// Token: 0x02000E71 RID: 3697
	public sealed class NKMGuildRankData : ISerializable
	{
		// Token: 0x060097D0 RID: 38864 RVA: 0x0032D7D0 File Offset: 0x0032B9D0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.badgeId);
			stream.PutOrGet(ref this.guildName);
			stream.PutOrGet(ref this.masterNickname);
			stream.PutOrGet(ref this.guildLevel);
			stream.PutOrGet(ref this.memberCount);
			stream.PutOrGet(ref this.rankValue);
		}

		// Token: 0x04008A01 RID: 35329
		public long guildUid;

		// Token: 0x04008A02 RID: 35330
		public long badgeId;

		// Token: 0x04008A03 RID: 35331
		public string guildName;

		// Token: 0x04008A04 RID: 35332
		public string masterNickname;

		// Token: 0x04008A05 RID: 35333
		public int guildLevel;

		// Token: 0x04008A06 RID: 35334
		public int memberCount;

		// Token: 0x04008A07 RID: 35335
		public long rankValue;
	}
}
