using System;
using System.Collections.Generic;
using ClientPacket.Shop;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Shop
{
	// Token: 0x02000AD2 RID: 2770
	public class NKCUIComShopConsumerPackageSlot : MonoBehaviour
	{
		// Token: 0x06007C32 RID: 31794 RVA: 0x0029794C File Offset: 0x00295B4C
		public void SetData(NKMConsumerPackageData data, ConsumerPackageGroupTemplet templet, int level)
		{
			bool bValue = data != null && data.rewardedLevel >= level;
			NKCUtil.SetGameobjectActive(this.m_objCompleted, bValue);
			ConsumerPackageGroupData rewardData = templet.GetRewardData(level);
			if (this.m_imgReq != null)
			{
				NKCUtil.SetImageSprite(this.m_imgReq, NKCResourceUtility.GetOrLoadMiscItemSmallIcon(rewardData.ConsumeRequireItemId), false);
			}
			NKCUtil.SetLabelText(this.m_lbReqCount, rewardData.ConsumeRequireItemValue.ToString());
			if (rewardData.RewardInfos.Count > this.m_lstRewardDatas.Count)
			{
				Debug.LogError(string.Format("Reward 종류가 슬롯이 가진 표시 수보다 많음! ConsumerPackageGroupTemplet : {0}, level : {1}", templet.ShopTemplet.m_ProductID, level));
			}
			for (int i = 0; i < this.m_lstRewardDatas.Count; i++)
			{
				if (rewardData.RewardInfos.Count <= i)
				{
					NKCUtil.SetGameobjectActive(this.m_lstRewardDatas[i].m_objRoot, false);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstRewardDatas[i].m_objRoot, true);
					NKCUtil.SetImageSprite(this.m_lstRewardDatas[i].m_imgReward, NKCResourceUtility.GetRewardInvenIcon(rewardData.RewardInfos[i], false), false);
					NKCUtil.SetLabelText(this.m_lstRewardDatas[i].m_lbRewardCount, rewardData.RewardInfos[i].Count.ToString());
				}
			}
		}

		// Token: 0x040068FD RID: 26877
		public Image m_imgReq;

		// Token: 0x040068FE RID: 26878
		public Text m_lbReqCount;

		// Token: 0x040068FF RID: 26879
		public List<NKCUIComShopConsumerPackageSlot.RewardData> m_lstRewardDatas;

		// Token: 0x04006900 RID: 26880
		public GameObject m_objCompleted;

		// Token: 0x02001844 RID: 6212
		[Serializable]
		public class RewardData
		{
			// Token: 0x0400A878 RID: 43128
			public GameObject m_objRoot;

			// Token: 0x0400A879 RID: 43129
			public Image m_imgReward;

			// Token: 0x0400A87A RID: 43130
			public Text m_lbRewardCount;
		}
	}
}
