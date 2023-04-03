using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E8E RID: 3726
	[PacketId(ClientPacketId.kNKMPacket_RANDOM_ITEM_BOX_OPEN_ACK)]
	public sealed class NKMPacket_RANDOM_ITEM_BOX_OPEN_ACK : ISerializable
	{
		// Token: 0x06009808 RID: 38920 RVA: 0x0032DDCE File Offset: 0x0032BFCE
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemData);
		}

		// Token: 0x04008A5A RID: 35418
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008A5B RID: 35419
		public NKMRewardData rewardData;

		// Token: 0x04008A5C RID: 35420
		public NKMItemMiscData costItemData;
	}
}
