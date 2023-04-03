using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E9D RID: 3741
	[PacketId(ClientPacketId.kNKMPacket_CHOICE_ITEM_USE_REQ)]
	public sealed class NKMPacket_CHOICE_ITEM_USE_REQ : ISerializable
	{
		// Token: 0x06009826 RID: 38950 RVA: 0x0032E09B File Offset: 0x0032C29B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.itemId);
			stream.PutOrGet(ref this.rewardId);
			stream.PutOrGet(ref this.count);
			stream.PutOrGet(ref this.setOptionId);
		}

		// Token: 0x04008A82 RID: 35458
		public int itemId;

		// Token: 0x04008A83 RID: 35459
		public int rewardId;

		// Token: 0x04008A84 RID: 35460
		public int count;

		// Token: 0x04008A85 RID: 35461
		public int setOptionId;
	}
}
