using System;
using Cs.Protocol;
using NKM.Templet;

namespace ClientPacket.Common
{
	// Token: 0x0200105C RID: 4188
	public sealed class NKMTrimClearData : ISerializable
	{
		// Token: 0x06009B76 RID: 39798 RVA: 0x003331A6 File Offset: 0x003313A6
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.isWin);
			stream.PutOrGet(ref this.trimId);
			stream.PutOrGet(ref this.trimLevel);
			stream.PutOrGet(ref this.score);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
		}

		// Token: 0x04008F64 RID: 36708
		public bool isWin;

		// Token: 0x04008F65 RID: 36709
		public int trimId;

		// Token: 0x04008F66 RID: 36710
		public int trimLevel;

		// Token: 0x04008F67 RID: 36711
		public int score;

		// Token: 0x04008F68 RID: 36712
		public NKMRewardData rewardData;
	}
}
