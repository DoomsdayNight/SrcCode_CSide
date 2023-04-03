using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Unit
{
	// Token: 0x02000D0A RID: 3338
	[PacketId(ClientPacketId.kNKMPacket_UNIT_MISSION_REWARD_ACK)]
	public sealed class NKMPacket_UNIT_MISSION_REWARD_ACK : ISerializable
	{
		// Token: 0x06009511 RID: 38161 RVA: 0x003299ED File Offset: 0x00327BED
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMUnitMissionData>(ref this.missionData);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
		}

		// Token: 0x040086A6 RID: 34470
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040086A7 RID: 34471
		public NKMUnitMissionData missionData = new NKMUnitMissionData();

		// Token: 0x040086A8 RID: 34472
		public NKMRewardData rewardData;
	}
}
