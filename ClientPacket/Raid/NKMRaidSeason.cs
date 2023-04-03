using System;
using Cs.Protocol;

namespace ClientPacket.Raid
{
	// Token: 0x02000D5A RID: 3418
	public sealed class NKMRaidSeason : ISerializable
	{
		// Token: 0x060095AF RID: 38319 RVA: 0x0032A970 File Offset: 0x00328B70
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.seasonId);
			stream.PutOrGet(ref this.monthlyPoint);
			stream.PutOrGet(ref this.tryAssistCount);
			stream.PutOrGet(ref this.recvRewardRaidPoint);
			stream.PutOrGet(ref this.latestUpdateTime);
		}

		// Token: 0x0400878E RID: 34702
		public int seasonId;

		// Token: 0x0400878F RID: 34703
		public int monthlyPoint;

		// Token: 0x04008790 RID: 34704
		public int tryAssistCount;

		// Token: 0x04008791 RID: 34705
		public int recvRewardRaidPoint;

		// Token: 0x04008792 RID: 34706
		public DateTime latestUpdateTime;
	}
}
