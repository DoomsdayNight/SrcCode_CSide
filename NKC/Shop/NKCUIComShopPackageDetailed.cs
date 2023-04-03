using System;
using System.Collections.Generic;
using NKM;
using NKM.Shop;
using UnityEngine;

namespace NKC.UI.Shop
{
	// Token: 0x02000AD5 RID: 2773
	public class NKCUIComShopPackageDetailed : MonoBehaviour, IShopPrefab
	{
		// Token: 0x06007C3A RID: 31802 RVA: 0x00297DFF File Offset: 0x00295FFF
		public bool IsHideLockObject()
		{
			return this.m_bHideLockObject;
		}

		// Token: 0x06007C3B RID: 31803 RVA: 0x00297E08 File Offset: 0x00296008
		public void SetData(ShopItemTemplet productTemplet)
		{
			if (productTemplet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (productTemplet.MiscProductTemplet == null || !productTemplet.MiscProductTemplet.IsPackageItem)
			{
				Debug.LogError(string.Format("Product {0} is not a Package Item!", productTemplet.m_ProductID));
				return;
			}
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(productTemplet.m_ItemID);
			if (itemMiscTempletByID.m_RewardGroupID == 0)
			{
				Debug.LogError("no rewardgroup! ID : " + productTemplet.m_ItemID.ToString());
				return;
			}
			List<NKMRandomBoxItemTemplet> randomBoxItemTempletList = NKCRandomBoxManager.GetRandomBoxItemTempletList(itemMiscTempletByID.m_RewardGroupID);
			if (itemMiscTempletByID.m_RewardGroupID != 0 && randomBoxItemTempletList == null)
			{
				Debug.LogError("rewardgroup null! ID : " + itemMiscTempletByID.m_RewardGroupID.ToString());
				return;
			}
			NKCUIComSlotDataInjector[] componentsInChildren = base.GetComponentsInChildren<NKCUIComSlotDataInjector>(true);
			int num = 0;
			NKCUIComSlotDataInjector[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				NKCUIComSlotDataInjector injector = array[i];
				NKMRandomBoxItemTemplet nkmrandomBoxItemTemplet = randomBoxItemTempletList.Find((NKMRandomBoxItemTemplet x) => x.m_reward_type == injector.m_rewardType && x.m_RewardID == injector.m_rewardID);
				NKCUtil.SetGameobjectActive(injector, nkmrandomBoxItemTemplet != null);
				if (this.bShowError && nkmrandomBoxItemTemplet == null)
				{
					Debug.LogError(string.Format("reward_type {0} / reward_id {1} 리워드가 패키지 {2} 안에 없음", injector.m_rewardType, injector.m_rewardID, itemMiscTempletByID.m_ItemMiscID));
				}
				else
				{
					injector.SetData(nkmrandomBoxItemTemplet);
					num++;
				}
			}
			if (this.bShowError && num != randomBoxItemTempletList.Count)
			{
				Debug.LogError("패키지 내의 아이템 전체를 표기하지 못했음");
			}
		}

		// Token: 0x04006907 RID: 26887
		[Header("패키지에 들어가는 아이템 숫자만큼 NKCUIComSlotDataInjector가 프리팹 안에 있어야 합니다!")]
		public List<NKCUIComSlotDataInjector> m_lstDataSlot;

		// Token: 0x04006908 RID: 26888
		public bool m_bHideLockObject;

		// Token: 0x04006909 RID: 26889
		public bool bShowError = true;
	}
}
