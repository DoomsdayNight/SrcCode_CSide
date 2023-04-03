using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F74 RID: 3956
	[PacketId(ClientPacketId.kNKMPacket_EVENT_BINGO_RANDOM_MARK_ACK)]
	public sealed class NKMPacket_EVENT_BINGO_RANDOM_MARK_ACK : ISerializable
	{
		// Token: 0x060099C4 RID: 39364 RVA: 0x003307CB File Offset: 0x0032E9CB
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.eventId);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemData);
			stream.PutOrGet(ref this.mileage);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
		}

		// Token: 0x04008CDA RID: 36058
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008CDB RID: 36059
		public int eventId;

		// Token: 0x04008CDC RID: 36060
		public NKMItemMiscData costItemData;

		// Token: 0x04008CDD RID: 36061
		public int mileage;

		// Token: 0x04008CDE RID: 36062
		public NKMRewardData rewardData;
	}
}
