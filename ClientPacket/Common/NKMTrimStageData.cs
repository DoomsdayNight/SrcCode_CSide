using System;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x0200105E RID: 4190
	public sealed class NKMTrimStageData : ISerializable
	{
		// Token: 0x06009B7A RID: 39802 RVA: 0x0033321A File Offset: 0x0033141A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.index);
			stream.PutOrGet(ref this.dungeonId);
			stream.PutOrGet(ref this.score);
			stream.PutOrGet(ref this.isWin);
		}

		// Token: 0x04008F6C RID: 36716
		public int index;

		// Token: 0x04008F6D RID: 36717
		public int dungeonId;

		// Token: 0x04008F6E RID: 36718
		public int score;

		// Token: 0x04008F6F RID: 36719
		public bool isWin;
	}
}
