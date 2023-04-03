using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001026 RID: 4134
	[PacketId(ClientPacketId.kNKMPacket_MENTORING_COMPLETE_INVITE_REWARD_ALL_ACK)]
	public sealed class NKMPacket_MENTORING_COMPLETE_INVITE_REWARD_ALL_ACK : ISerializable
	{
		// Token: 0x06009B1C RID: 39708 RVA: 0x003323C7 File Offset: 0x003305C7
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet(ref this.inviteRequireCountList);
		}

		// Token: 0x04008E72 RID: 36466
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E73 RID: 36467
		public NKMRewardData rewardData;

		// Token: 0x04008E74 RID: 36468
		public List<int> inviteRequireCountList = new List<int>();
	}
}
