using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001024 RID: 4132
	[PacketId(ClientPacketId.kNKMPacket_MENTORING_COMPLETE_INVITE_REWARD_ACK)]
	public sealed class NKMPacket_MENTORING_COMPLETE_INVITE_REWARD_ACK : ISerializable
	{
		// Token: 0x06009B18 RID: 39704 RVA: 0x0033238F File Offset: 0x0033058F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet(ref this.inviteRequireCount);
		}

		// Token: 0x04008E6F RID: 36463
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E70 RID: 36464
		public NKMRewardData rewardData;

		// Token: 0x04008E71 RID: 36465
		public int inviteRequireCount;
	}
}
