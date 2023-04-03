using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000FA8 RID: 4008
	[PacketId(ClientPacketId.kNKMPacket_EVENT_BAR_GET_REWARD_ACK)]
	public sealed class NKMPacket_EVENT_BAR_GET_REWARD_ACK : ISerializable
	{
		// Token: 0x06009A24 RID: 39460 RVA: 0x00330F7C File Offset: 0x0032F17C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItems);
			stream.PutOrGet(ref this.remainDeliveryLimitValue);
		}

		// Token: 0x04008D5B RID: 36187
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008D5C RID: 36188
		public NKMRewardData rewardData;

		// Token: 0x04008D5D RID: 36189
		public List<NKMItemMiscData> costItems = new List<NKMItemMiscData>();

		// Token: 0x04008D5E RID: 36190
		public int remainDeliveryLimitValue;
	}
}
