using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E9E RID: 3742
	[PacketId(ClientPacketId.kNKMPacket_CHOICE_ITEM_USE_ACK)]
	public sealed class NKMPacket_CHOICE_ITEM_USE_ACK : ISerializable
	{
		// Token: 0x06009828 RID: 38952 RVA: 0x0032E0D5 File Offset: 0x0032C2D5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemData);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
		}

		// Token: 0x04008A86 RID: 35462
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008A87 RID: 35463
		public NKMItemMiscData costItemData;

		// Token: 0x04008A88 RID: 35464
		public NKMRewardData rewardData;
	}
}
