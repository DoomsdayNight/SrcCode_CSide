using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CD4 RID: 3284
	[PacketId(ClientPacketId.kNKMPacket_TEAM_COLLECTION_REWARD_ACK)]
	public sealed class NKMPacket_TEAM_COLLECTION_REWARD_ACK : ISerializable
	{
		// Token: 0x060094A5 RID: 38053 RVA: 0x00329087 File Offset: 0x00327287
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet<NKMTeamCollectionData>(ref this.teamCollectionData);
		}

		// Token: 0x04008626 RID: 34342
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008627 RID: 34343
		public NKMRewardData rewardData;

		// Token: 0x04008628 RID: 34344
		public NKMTeamCollectionData teamCollectionData;
	}
}
