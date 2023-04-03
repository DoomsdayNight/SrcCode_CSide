using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Shop;
using Cs.Protocol;
using NKC;
using NKM.Shop;

namespace NKM
{
	// Token: 0x02000473 RID: 1139
	public class NKMShopData : ISerializable
	{
		// Token: 0x06001EFC RID: 7932 RVA: 0x000934AA File Offset: 0x000916AA
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMShopPurchaseHistory>(ref this.histories);
			stream.PutOrGet<NKMShopRandomData>(ref this.randomShop);
			stream.PutOrGet<NKMShopSubscriptionData>(ref this.subscriptions);
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x000934D0 File Offset: 0x000916D0
		public int GetRealPrice(ShopItemTemplet shopTemplet, int count = 1, bool useSteamPrice = false)
		{
			int num = shopTemplet.m_Price;
			if (useSteamPrice)
			{
				num = shopTemplet.m_PriceSteam;
			}
			if (shopTemplet.m_DiscountRate > 0f && NKCSynchronizedTime.IsEventTime(shopTemplet.discountIntervalId, shopTemplet.DiscountStartDateUtc, shopTemplet.DiscountEndDateUtc))
			{
				num -= (int)((float)num * (shopTemplet.m_DiscountRate / 100f));
			}
			if (shopTemplet.m_PurchaseEventType == PURCHASE_EVENT_REWARD_TYPE.INCREACE_PRICE_PER_PURCHASE_COUNT)
			{
				int purchasedCount = this.GetPurchasedCount(shopTemplet);
				int num2 = purchasedCount + count;
				num = (num2 * (num2 + 1) - purchasedCount * (purchasedCount + 1)) * num / 2;
			}
			else
			{
				num *= count;
			}
			return num;
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x00093554 File Offset: 0x00091754
		public int GetPurchasedCount(ShopItemTemplet shopTemplet)
		{
			NKMShopPurchaseHistory nkmshopPurchaseHistory;
			if (!this.histories.TryGetValue(shopTemplet.m_ProductID, out nkmshopPurchaseHistory))
			{
				return 0;
			}
			if (shopTemplet.IsCountResetType() && NKCSynchronizedTime.IsFinished(nkmshopPurchaseHistory.nextResetDate))
			{
				return 0;
			}
			return nkmshopPurchaseHistory.purchaseCount;
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x00093598 File Offset: 0x00091798
		public void SetTotalPayment(double totalPaid)
		{
			this.totalPaidAmount = totalPaid;
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x000935A1 File Offset: 0x000917A1
		public double GetTotalPayment()
		{
			return this.totalPaidAmount;
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x000935A9 File Offset: 0x000917A9
		public void SetChainTabResetData(List<ShopChainTabNextResetData> lstChainTabResetData)
		{
			this.m_lstChainTabResetData = lstChainTabResetData;
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x000935B4 File Offset: 0x000917B4
		public DateTime GetChainTabResetTime(string tabType, int subIndex)
		{
			for (int i = 0; i < this.m_lstChainTabResetData.Count; i++)
			{
				if (this.m_lstChainTabResetData[i].tabType == tabType && this.m_lstChainTabResetData[i].subIndex == subIndex)
				{
					return this.m_lstChainTabResetData[i].nextResetUtc;
				}
			}
			return default(DateTime);
		}

		// Token: 0x04001F6B RID: 8043
		public Dictionary<int, NKMShopPurchaseHistory> histories = new Dictionary<int, NKMShopPurchaseHistory>();

		// Token: 0x04001F6C RID: 8044
		public NKMShopRandomData randomShop;

		// Token: 0x04001F6D RID: 8045
		public Dictionary<int, NKMShopSubscriptionData> subscriptions;

		// Token: 0x04001F6E RID: 8046
		private double totalPaidAmount;

		// Token: 0x04001F6F RID: 8047
		private List<ShopChainTabNextResetData> m_lstChainTabResetData = new List<ShopChainTabNextResetData>();
	}
}
