using System;
using System.Collections.Generic;
using NKM;
using NKM.Shop;
using UnityEngine;

namespace NKC.UI.Shop
{
	// Token: 0x02000AD4 RID: 2772
	public class NKCUIComShopPackageCommon : MonoBehaviour, IShopPrefab
	{
		// Token: 0x06007C37 RID: 31799 RVA: 0x00297CD8 File Offset: 0x00295ED8
		public bool IsHideLockObject()
		{
			return this.m_bHideLockObject;
		}

		// Token: 0x06007C38 RID: 31800 RVA: 0x00297CE0 File Offset: 0x00295EE0
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
			for (int i = 0; i < this.m_lstDataSlot.Count; i++)
			{
				NKCUIComSlotDataInjector nkcuicomSlotDataInjector = this.m_lstDataSlot[i];
				if (!(nkcuicomSlotDataInjector == null))
				{
					if (i < randomBoxItemTempletList.Count)
					{
						NKMRandomBoxItemTemplet data = randomBoxItemTempletList[i];
						NKCUtil.SetGameobjectActive(nkcuicomSlotDataInjector, true);
						nkcuicomSlotDataInjector.SetData(data);
					}
					else
					{
						NKCUtil.SetGameobjectActive(nkcuicomSlotDataInjector, false);
					}
				}
			}
		}

		// Token: 0x04006904 RID: 26884
		[Header("NKCUIComSlotDataInjector의 설정을 무시하고 패키지의 데이터를 집어넣습니다")]
		public List<NKCUIComSlotDataInjector> m_lstDataSlot;

		// Token: 0x04006905 RID: 26885
		public bool m_bHideLockObject;

		// Token: 0x04006906 RID: 26886
		public bool bShowError = true;
	}
}
