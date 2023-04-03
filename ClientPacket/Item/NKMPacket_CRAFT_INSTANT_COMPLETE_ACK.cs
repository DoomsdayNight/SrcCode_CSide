using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E96 RID: 3734
	[PacketId(ClientPacketId.kNKMPacket_CRAFT_INSTANT_COMPLETE_ACK)]
	public sealed class NKMPacket_CRAFT_INSTANT_COMPLETE_ACK : ISerializable
	{
		// Token: 0x06009818 RID: 38936 RVA: 0x0032DF00 File Offset: 0x0032C100
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMCraftSlotData>(ref this.craftSlotData);
			stream.PutOrGet<NKMItemMiscData>(ref this.extraCostItemData);
			stream.PutOrGet<NKMRewardData>(ref this.createdRewardData);
		}

		// Token: 0x04008A6B RID: 35435
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008A6C RID: 35436
		public NKMCraftSlotData craftSlotData;

		// Token: 0x04008A6D RID: 35437
		public NKMItemMiscData extraCostItemData;

		// Token: 0x04008A6E RID: 35438
		public NKMRewardData createdRewardData;
	}
}
