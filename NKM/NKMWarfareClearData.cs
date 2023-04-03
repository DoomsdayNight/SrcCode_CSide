using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM.Templet;

namespace NKM
{
	// Token: 0x020004F9 RID: 1273
	public class NKMWarfareClearData : ISerializable
	{
		// Token: 0x0600240C RID: 9228 RVA: 0x000BB724 File Offset: 0x000B9924
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_WarfareID);
			stream.PutOrGet(ref this.m_mission_result_1);
			stream.PutOrGet(ref this.m_mission_result_2);
			stream.PutOrGet<NKMRewardData>(ref this.m_RewardDataList);
			stream.PutOrGet<NKMRewardData>(ref this.m_ContainerRewards);
			stream.PutOrGet(ref this.m_enemiesKillCount);
			stream.PutOrGet<NKMRewardData>(ref this.m_MissionReward);
			stream.PutOrGet(ref this.m_MissionRewardResult);
			stream.PutOrGet<NKMRewardData>(ref this.m_OnetimeRewards);
			stream.PutOrGet(ref this.m_OnetimeRewardResults);
			stream.PutOrGet<NKMStagePlayData>(ref this.m_StagePlayData);
		}

		// Token: 0x040025C6 RID: 9670
		public int m_WarfareID;

		// Token: 0x040025C7 RID: 9671
		public bool m_mission_result_1;

		// Token: 0x040025C8 RID: 9672
		public bool m_mission_result_2;

		// Token: 0x040025C9 RID: 9673
		public NKMRewardData m_RewardDataList;

		// Token: 0x040025CA RID: 9674
		public NKMRewardData m_ContainerRewards;

		// Token: 0x040025CB RID: 9675
		public int m_enemiesKillCount;

		// Token: 0x040025CC RID: 9676
		public NKMRewardData m_MissionReward;

		// Token: 0x040025CD RID: 9677
		public bool m_MissionRewardResult;

		// Token: 0x040025CE RID: 9678
		public NKMRewardData m_OnetimeRewards;

		// Token: 0x040025CF RID: 9679
		public List<bool> m_OnetimeRewardResults = new List<bool>(3);

		// Token: 0x040025D0 RID: 9680
		public NKMStagePlayData m_StagePlayData;
	}
}
