using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM.Templet;

namespace ClientPacket.Common
{
	// Token: 0x0200103C RID: 4156
	public sealed class NKMDungeonClearData : ISerializable
	{
		// Token: 0x06009B3A RID: 39738 RVA: 0x003326B4 File Offset: 0x003308B4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.dungeonId);
			stream.PutOrGet(ref this.missionResult1);
			stream.PutOrGet(ref this.missionResult2);
			stream.PutOrGet<NKMRewardData>(ref this.missionReward);
			stream.PutOrGet(ref this.missionRewardResult);
			stream.PutOrGet<NKMRewardData>(ref this.oneTimeRewards);
			stream.PutOrGet(ref this.onetimeRewardResults);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
		}

		// Token: 0x04008EB6 RID: 36534
		public int dungeonId;

		// Token: 0x04008EB7 RID: 36535
		public bool missionResult1;

		// Token: 0x04008EB8 RID: 36536
		public bool missionResult2;

		// Token: 0x04008EB9 RID: 36537
		public NKMRewardData missionReward;

		// Token: 0x04008EBA RID: 36538
		public bool missionRewardResult;

		// Token: 0x04008EBB RID: 36539
		public NKMRewardData oneTimeRewards;

		// Token: 0x04008EBC RID: 36540
		public List<bool> onetimeRewardResults = new List<bool>();

		// Token: 0x04008EBD RID: 36541
		public NKMRewardData rewardData;
	}
}
