using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F78 RID: 3960
	[PacketId(ClientPacketId.kNKMPacket_EVENT_BINGO_REWARD_ACK)]
	public sealed class NKMPacket_EVENT_BINGO_REWARD_ACK : ISerializable
	{
		// Token: 0x060099CC RID: 39372 RVA: 0x0033088F File Offset: 0x0032EA8F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.eventId);
			stream.PutOrGet(ref this.rewardIndex);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
		}

		// Token: 0x04008CE7 RID: 36071
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008CE8 RID: 36072
		public int eventId;

		// Token: 0x04008CE9 RID: 36073
		public int rewardIndex;

		// Token: 0x04008CEA RID: 36074
		public NKMRewardData rewardData;
	}
}
