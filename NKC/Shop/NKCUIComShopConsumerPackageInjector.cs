using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Shop;
using NKM;
using NKM.Shop;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Shop
{
	// Token: 0x02000AD1 RID: 2769
	public class NKCUIComShopConsumerPackageInjector : MonoBehaviour, IShopDataInjector
	{
		// Token: 0x06007C2C RID: 31788 RVA: 0x00297658 File Offset: 0x00295858
		private void LoadConsumerPackageData()
		{
			if (NKMTempletContainer<ConsumerPackageGroupTemplet>.HasValue())
			{
				return;
			}
			NKMTempletContainer<ConsumerPackageGroupTemplet>.Load(from e in NKMTempletLoader<ConsumerPackageGroupData>.LoadGroup("AB_SCRIPT", "LUA_ACQ_PACKAGE_TEMPLET", "ACQ_PACKAGE_TEMPLET", new Func<NKMLua, ConsumerPackageGroupData>(ConsumerPackageGroupData.LoadFromLUA))
			select new ConsumerPackageGroupTemplet(e.Key, e.Value), null);
		}

		// Token: 0x06007C2D RID: 31789 RVA: 0x002976B8 File Offset: 0x002958B8
		public void TriggerInjectData(ShopItemTemplet productTemplet)
		{
			this.LoadConsumerPackageData();
			ConsumerPackageGroupTemplet consumerPackageGroupTemplet = ConsumerPackageGroupTemplet.Find(productTemplet.m_PurchaseEventValue);
			if (productTemplet.m_PurchaseEventType != PURCHASE_EVENT_REWARD_TYPE.CONSUMER_PACKAGE || consumerPackageGroupTemplet == null)
			{
				Debug.LogError(string.Format("[ShopTemplet] 소비자 패키지 정보가 존재하지 않음 m_ProductId:{0}, m_PurchaseEventValue:{1}", productTemplet.m_ProductID, productTemplet.m_PurchaseEventValue));
				return;
			}
			if (consumerPackageGroupTemplet.MaxLevel > (long)this.m_lstSlots.Count)
			{
				Debug.LogError(string.Format("[ShopTemplet] 소비자 패키지 정보 프리팹에 슬롯 수가 부족함 m_ProductID {0}", productTemplet.m_ProductID));
				return;
			}
			NKMConsumerPackageData nkmconsumerPackageData;
			if (!NKCScenManager.CurrentUserData().GetConsumerPackageData(productTemplet.m_ProductID, out nkmconsumerPackageData))
			{
				nkmconsumerPackageData = null;
			}
			long num = (nkmconsumerPackageData != null) ? nkmconsumerPackageData.spendCount : 0L;
			long maxLevelRequireValue = consumerPackageGroupTemplet.MaxLevelRequireValue;
			NKCUtil.SetLabelText(this.m_lbUseCount, string.Format(this.useCountFormat, num, maxLevelRequireValue));
			NKCUtil.SetImageFillAmount(this.m_imgGauge, this.GetCurrentProgress(nkmconsumerPackageData, consumerPackageGroupTemplet));
			for (int i = 0; i < this.m_lstSlots.Count; i++)
			{
				if (this.m_lstSlots[i] != null)
				{
					int num2 = i + 1;
					bool bValue = nkmconsumerPackageData != null && nkmconsumerPackageData.rewardedLevel >= num2;
					NKCUtil.SetGameobjectActive(this.m_lstSlots[i].m_objCompleteMark, bValue);
					this.m_lstSlots[i].slot.SetData(nkmconsumerPackageData, consumerPackageGroupTemplet, num2);
				}
			}
			bool bValue2 = nkmconsumerPackageData != null && (long)nkmconsumerPackageData.rewardedLevel >= consumerPackageGroupTemplet.MaxLevel;
			NKCUtil.SetGameobjectActive(this.m_objAllCompleteMark, bValue2);
		}

		// Token: 0x06007C2E RID: 31790 RVA: 0x00297838 File Offset: 0x00295A38
		private float GetCurrentProgress(NKMConsumerPackageData data, ConsumerPackageGroupTemplet templet)
		{
			if (data == null)
			{
				return 0f;
			}
			if ((long)data.rewardedLevel == templet.MaxLevel)
			{
				return 1f;
			}
			ConsumerPackageGroupData rewardData = templet.GetRewardData(data.rewardedLevel);
			long num = (rewardData != null) ? rewardData.ConsumeRequireItemValue : 0L;
			ConsumerPackageGroupData rewardData2 = templet.GetRewardData(data.rewardedLevel + 1);
			long num2 = (rewardData2 != null) ? rewardData2.ConsumeRequireItemValue : templet.MaxLevelRequireValue;
			float num3 = (float)(data.spendCount - num) / (float)(num2 - num);
			float levelProgress = this.GetLevelProgress(data.rewardedLevel, 0f);
			float levelProgress2 = this.GetLevelProgress(data.rewardedLevel + 1, 1f);
			return levelProgress + (levelProgress2 - levelProgress) * num3;
		}

		// Token: 0x06007C2F RID: 31791 RVA: 0x002978E4 File Offset: 0x00295AE4
		private float GetLevelProgress(int level, float defaultValue)
		{
			NKCUIComShopConsumerPackageInjector.SlotSet slot = this.GetSlot(level);
			if (slot == null)
			{
				return defaultValue;
			}
			return slot.m_fMarkPosition;
		}

		// Token: 0x06007C30 RID: 31792 RVA: 0x00297904 File Offset: 0x00295B04
		private NKCUIComShopConsumerPackageInjector.SlotSet GetSlot(int level)
		{
			int num = level - 1;
			if (num < 0)
			{
				return null;
			}
			if (num < this.m_lstSlots.Count)
			{
				return this.m_lstSlots[num];
			}
			return null;
		}

		// Token: 0x040068F8 RID: 26872
		[Header("컨슈머 패키지 관련")]
		public Text m_lbUseCount;

		// Token: 0x040068F9 RID: 26873
		public Image m_imgGauge;

		// Token: 0x040068FA RID: 26874
		public GameObject m_objAllCompleteMark;

		// Token: 0x040068FB RID: 26875
		[Multiline]
		public string useCountFormat = "{0}/{1}";

		// Token: 0x040068FC RID: 26876
		public List<NKCUIComShopConsumerPackageInjector.SlotSet> m_lstSlots;

		// Token: 0x02001842 RID: 6210
		[Serializable]
		public class SlotSet
		{
			// Token: 0x0400A873 RID: 43123
			public float m_fMarkPosition;

			// Token: 0x0400A874 RID: 43124
			public GameObject m_objCompleteMark;

			// Token: 0x0400A875 RID: 43125
			public NKCUIComShopConsumerPackageSlot slot;
		}
	}
}
