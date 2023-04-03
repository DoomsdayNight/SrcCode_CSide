using System;
using Cs.Protocol;
using NKM;

namespace ClientPacket.Common
{
	// Token: 0x0200104A RID: 4170
	public sealed class EquipProfileInfo : ISerializable
	{
		// Token: 0x06009B54 RID: 39764 RVA: 0x00332C6C File Offset: 0x00330E6C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.itemUid);
			stream.PutOrGet(ref this.ItemId);
			stream.PutOrGet(ref this.enchantLevel);
			stream.PutOrGetEnum<NKM_STAT_TYPE>(ref this.statType);
			stream.PutOrGet(ref this.statValue);
			stream.PutOrGetEnum<NKM_STAT_TYPE>(ref this.statType2);
			stream.PutOrGet(ref this.statValue2);
			stream.PutOrGet(ref this.setOptionId);
		}

		// Token: 0x04008F0D RID: 36621
		public long itemUid;

		// Token: 0x04008F0E RID: 36622
		public int ItemId;

		// Token: 0x04008F0F RID: 36623
		public int enchantLevel;

		// Token: 0x04008F10 RID: 36624
		public NKM_STAT_TYPE statType;

		// Token: 0x04008F11 RID: 36625
		public float statValue;

		// Token: 0x04008F12 RID: 36626
		public NKM_STAT_TYPE statType2;

		// Token: 0x04008F13 RID: 36627
		public float statValue2;

		// Token: 0x04008F14 RID: 36628
		public int setOptionId;
	}
}
