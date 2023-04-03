using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM.Templet;

namespace ClientPacket.Common
{
	// Token: 0x0200103D RID: 4157
	public sealed class NKMPhaseClearData : ISerializable
	{
		// Token: 0x06009B3C RID: 39740 RVA: 0x00332734 File Offset: 0x00330934
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.stageId);
			stream.PutOrGet(ref this.missionResult1);
			stream.PutOrGet(ref this.missionResult2);
			stream.PutOrGet<NKMRewardData>(ref this.missionReward);
			stream.PutOrGet(ref this.missionRewardResult);
			stream.PutOrGet<NKMRewardData>(ref this.oneTimeRewards);
			stream.PutOrGet(ref this.onetimeRewardResults);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
		}

		// Token: 0x04008EBE RID: 36542
		public int stageId;

		// Token: 0x04008EBF RID: 36543
		public bool missionResult1;

		// Token: 0x04008EC0 RID: 36544
		public bool missionResult2;

		// Token: 0x04008EC1 RID: 36545
		public NKMRewardData missionReward;

		// Token: 0x04008EC2 RID: 36546
		public bool missionRewardResult;

		// Token: 0x04008EC3 RID: 36547
		public NKMRewardData oneTimeRewards;

		// Token: 0x04008EC4 RID: 36548
		public List<bool> onetimeRewardResults = new List<bool>();

		// Token: 0x04008EC5 RID: 36549
		public NKMRewardData rewardData;
	}
}
