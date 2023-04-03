using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CCB RID: 3275
	[PacketId(ClientPacketId.kNKMPacket_EPISODE_COMPLETE_REWARD_ALL_ACK)]
	public sealed class NKMPacket_EPISODE_COMPLETE_REWARD_ALL_ACK : ISerializable
	{
		// Token: 0x06009493 RID: 38035 RVA: 0x00328EBB File Offset: 0x003270BB
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardDate);
			stream.PutOrGet<NKMEpisodeCompleteData>(ref this.episodeCompleteData);
		}

		// Token: 0x0400860B RID: 34315
		public NKM_ERROR_CODE errorCode;

		// Token: 0x0400860C RID: 34316
		public NKMRewardData rewardDate;

		// Token: 0x0400860D RID: 34317
		public List<NKMEpisodeCompleteData> episodeCompleteData = new List<NKMEpisodeCompleteData>();
	}
}
