using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D0C RID: 3340
	[PacketId(ClientPacketId.kNKMPacket_UNIT_MISSION_REWARD_ALL_ACK)]
	public sealed class NKMPacket_UNIT_MISSION_REWARD_ALL_ACK : ISerializable
	{
		// Token: 0x06009515 RID: 38165 RVA: 0x00329A3C File Offset: 0x00327C3C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMUnitMissionData>(ref this.missionData);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
		}

		// Token: 0x040086AA RID: 34474
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040086AB RID: 34475
		public List<NKMUnitMissionData> missionData = new List<NKMUnitMissionData>();

		// Token: 0x040086AC RID: 34476
		public NKMRewardData rewardData;
	}
}
