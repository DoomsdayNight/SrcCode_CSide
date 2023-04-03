using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F7A RID: 3962
	[PacketId(ClientPacketId.kNKMPacket_EVENT_PASS_LEVEL_COMPLETE_ACK)]
	public sealed class NKMPacket_EVENT_PASS_LEVEL_COMPLETE_ACK : ISerializable
	{
		// Token: 0x060099D0 RID: 39376 RVA: 0x003308D3 File Offset: 0x0032EAD3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.rewardNormalLevel);
			stream.PutOrGet(ref this.rewardCoreLevel);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
		}

		// Token: 0x04008CEB RID: 36075
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008CEC RID: 36076
		public int rewardNormalLevel;

		// Token: 0x04008CED RID: 36077
		public int rewardCoreLevel;

		// Token: 0x04008CEE RID: 36078
		public NKMRewardData rewardData;
	}
}
