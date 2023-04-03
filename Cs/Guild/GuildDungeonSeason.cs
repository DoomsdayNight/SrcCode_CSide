using System;
using Cs.Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000ECA RID: 3786
	public sealed class GuildDungeonSeason : ISerializable
	{
		// Token: 0x06009874 RID: 39028 RVA: 0x0032E861 File Offset: 0x0032CA61
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.seasonId);
			stream.PutOrGet(ref this.score);
			stream.PutOrGet(ref this.joinCount);
			stream.PutOrGet(ref this.lastRewardScore);
			stream.PutOrGet(ref this.lastRewardJoin);
		}

		// Token: 0x04008B0A RID: 35594
		public int seasonId;

		// Token: 0x04008B0B RID: 35595
		public long score;

		// Token: 0x04008B0C RID: 35596
		public int joinCount;

		// Token: 0x04008B0D RID: 35597
		public int lastRewardScore;

		// Token: 0x04008B0E RID: 35598
		public int lastRewardJoin;
	}
}
