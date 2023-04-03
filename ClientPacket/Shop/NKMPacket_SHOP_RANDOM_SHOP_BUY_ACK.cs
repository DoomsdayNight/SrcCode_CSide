using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;
using Protocol;

namespace ClientPacket.Shop
{
	// Token: 0x02000D26 RID: 3366
	[PacketId(ClientPacketId.kNKMPacket_SHOP_RANDOM_SHOP_BUY_ACK)]
	public sealed class NKMPacket_SHOP_RANDOM_SHOP_BUY_ACK : ISerializable
	{
		// Token: 0x06009549 RID: 38217 RVA: 0x00329F3D File Offset: 0x0032813D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.slotIndex);
			stream.PutOrGet<NKMRewardData>(ref this.rewardData);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemData);
		}

		// Token: 0x040086F4 RID: 34548
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040086F5 RID: 34549
		public int slotIndex;

		// Token: 0x040086F6 RID: 34550
		public NKMRewardData rewardData;

		// Token: 0x040086F7 RID: 34551
		public NKMItemMiscData costItemData;
	}
}
