using System;
using Cs.Protocol;
using NKM.Templet;

namespace NKM
{
	// Token: 0x02000471 RID: 1137
	public class NKMShopRandomListData : ISerializable
	{
		// Token: 0x06001EF7 RID: 7927 RVA: 0x000933E0 File Offset: 0x000915E0
		public int GetPrice()
		{
			if (this.discountRatio > 0)
			{
				return this.price * (100 - this.discountRatio) / 100;
			}
			return this.price;
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x00093408 File Offset: 0x00091608
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.itemId);
			stream.PutOrGetEnum<NKM_REWARD_TYPE>(ref this.itemType);
			stream.PutOrGet(ref this.itemCount);
			stream.PutOrGet(ref this.priceItemId);
			stream.PutOrGet(ref this.price);
			stream.PutOrGet(ref this.isBuy);
			stream.PutOrGet(ref this.discountRatio);
		}

		// Token: 0x04001F61 RID: 8033
		public int itemId;

		// Token: 0x04001F62 RID: 8034
		public NKM_REWARD_TYPE itemType;

		// Token: 0x04001F63 RID: 8035
		public int itemCount;

		// Token: 0x04001F64 RID: 8036
		public int priceItemId;

		// Token: 0x04001F65 RID: 8037
		public int price;

		// Token: 0x04001F66 RID: 8038
		public bool isBuy;

		// Token: 0x04001F67 RID: 8039
		public int discountRatio;
	}
}
