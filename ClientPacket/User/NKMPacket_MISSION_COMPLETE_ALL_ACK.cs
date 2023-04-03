using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CC3 RID: 3267
	[PacketId(ClientPacketId.kNKMPacket_MISSION_COMPLETE_ALL_ACK)]
	public sealed class NKMPacket_MISSION_COMPLETE_ALL_ACK : ISerializable
	{
		// Token: 0x06009483 RID: 38019 RVA: 0x00328D1F File Offset: 0x00326F1F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.missionIDList);
			stream.PutOrGet<NKMRewardData>(ref this.rewardDate);
			stream.PutOrGet<NKMAdditionalReward>(ref this.additionalReward);
		}

		// Token: 0x040085F3 RID: 34291
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040085F4 RID: 34292
		public List<int> missionIDList = new List<int>();

		// Token: 0x040085F5 RID: 34293
		public NKMRewardData rewardDate;

		// Token: 0x040085F6 RID: 34294
		public NKMAdditionalReward additionalReward = new NKMAdditionalReward();
	}
}
