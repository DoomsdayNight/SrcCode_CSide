using System;
using Cs.Protocol;
using NKM;

namespace ClientPacket.Common
{
	// Token: 0x02001047 RID: 4167
	public sealed class NKMFierceBoss : ISerializable
	{
		// Token: 0x06009B4E RID: 39758 RVA: 0x00332BB0 File Offset: 0x00330DB0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.bossId);
			stream.PutOrGet(ref this.point);
			stream.PutOrGet(ref this.rankNumber);
			stream.PutOrGet(ref this.rankPercent);
			stream.PutOrGet<NKMEventDeckData>(ref this.deckData);
			stream.PutOrGet(ref this.isCleared);
		}

		// Token: 0x04008F01 RID: 36609
		public int bossId;

		// Token: 0x04008F02 RID: 36610
		public int point;

		// Token: 0x04008F03 RID: 36611
		public int rankNumber;

		// Token: 0x04008F04 RID: 36612
		public int rankPercent;

		// Token: 0x04008F05 RID: 36613
		public NKMEventDeckData deckData;

		// Token: 0x04008F06 RID: 36614
		public bool isCleared;
	}
}
