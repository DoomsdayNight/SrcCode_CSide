using System;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x02001045 RID: 4165
	public sealed class NKMStagePlayData : ISerializable
	{
		// Token: 0x06009B4A RID: 39754 RVA: 0x00332B00 File Offset: 0x00330D00
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.stageId);
			stream.PutOrGet(ref this.playCount);
			stream.PutOrGet(ref this.restoreCount);
			stream.PutOrGet(ref this.bestKillCount);
			stream.PutOrGet(ref this.nextResetDate);
			stream.PutOrGet(ref this.bestClearTimeSec);
			stream.PutOrGet(ref this.totalPlayCount);
		}

		// Token: 0x04008EF5 RID: 36597
		public int stageId;

		// Token: 0x04008EF6 RID: 36598
		public long playCount;

		// Token: 0x04008EF7 RID: 36599
		public long restoreCount;

		// Token: 0x04008EF8 RID: 36600
		public long bestKillCount;

		// Token: 0x04008EF9 RID: 36601
		public DateTime nextResetDate;

		// Token: 0x04008EFA RID: 36602
		public int bestClearTimeSec;

		// Token: 0x04008EFB RID: 36603
		public long totalPlayCount;
	}
}
