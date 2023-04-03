using System;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CC1 RID: 3265
	[PacketId(ClientPacketId.kNKMPacket_MISSION_COMPLETE_ACK)]
	public sealed class NKMPacket_MISSION_COMPLETE_ACK : ISerializable
	{
		// Token: 0x0600947F RID: 38015 RVA: 0x00328CC4 File Offset: 0x00326EC4
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.missionID);
			stream.PutOrGet<NKMRewardData>(ref this.rewardDate);
			stream.PutOrGet<NKMAdditionalReward>(ref this.additionalReward);
		}

		// Token: 0x040085EE RID: 34286
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040085EF RID: 34287
		public int missionID;

		// Token: 0x040085F0 RID: 34288
		public NKMRewardData rewardDate;

		// Token: 0x040085F1 RID: 34289
		public NKMAdditionalReward additionalReward = new NKMAdditionalReward();
	}
}
