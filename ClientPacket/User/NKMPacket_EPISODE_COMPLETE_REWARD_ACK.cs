using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CC9 RID: 3273
	[PacketId(ClientPacketId.kNKMPacket_EPISODE_COMPLETE_REWARD_ACK)]
	public sealed class NKMPacket_EPISODE_COMPLETE_REWARD_ACK : ISerializable
	{
		// Token: 0x0600948F RID: 38031 RVA: 0x00328E6B File Offset: 0x0032706B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet<NKMEpisodeCompleteData>(ref this.episodeCompleteData);
		}

		// Token: 0x04008606 RID: 34310
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008607 RID: 34311
		public NKMRewardData rewardData;

		// Token: 0x04008608 RID: 34312
		public NKMEpisodeCompleteData episodeCompleteData;
	}
}
