using System;
using System.Collections.Generic;
using NKC.UI.Component.Office;
using NKC.UI.Office;
using NKM;
using NKM.Shop;
using NKM.Templet;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.Shop
{
	// Token: 0x02000AE6 RID: 2790
	public class NKCUIShopSlotPrefabInteriorPackage : NKCUIShopSlotPrefab
	{
		// Token: 0x06007D4C RID: 32076 RVA: 0x0029FECC File Offset: 0x0029E0CC
		public override void Init(NKCUIShopSlotBase.OnBuy onBuy, NKCUIShopSlotBase.OnRefreshRequired onRefreshRequired)
		{
			base.Init(onBuy, onRefreshRequired);
			NKCUtil.SetButtonClickDelegate(this.m_cstbnDetail, new UnityAction(this.OnClickDetail));
		}

		// Token: 0x06007D4D RID: 32077 RVA: 0x0029FEF0 File Offset: 0x0029E0F0
		protected override void PostSetData(ShopItemTemplet shopTemplet)
		{
			base.PostSetData(shopTemplet);
			if (shopTemplet.m_ItemType != NKM_REWARD_TYPE.RT_MISC)
			{
				NKCUtil.SetGameobjectActive(this.m_OfficeInteriorDetail, false);
				NKCUtil.SetGameobjectActive(this.m_cstbnDetail, false);
				return;
			}
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(shopTemplet.m_ItemID);
			if (!itemMiscTempletByID.IsPackageItem)
			{
				NKCUtil.SetGameobjectActive(this.m_OfficeInteriorDetail, false);
				NKCUtil.SetGameobjectActive(this.m_cstbnDetail, false);
				return;
			}
			List<NKMRandomBoxItemTemplet> randomBoxItemTempletList = NKCRandomBoxManager.GetRandomBoxItemTempletList(itemMiscTempletByID.m_RewardGroupID);
			this.m_dicPackageInterior = new Dictionary<int, int>();
			foreach (NKMRandomBoxItemTemplet nkmrandomBoxItemTemplet in randomBoxItemTempletList)
			{
				if (nkmrandomBoxItemTemplet.m_reward_type == NKM_REWARD_TYPE.RT_MISC && NKMOfficeInteriorTemplet.Find(nkmrandomBoxItemTemplet.m_RewardID) != null)
				{
					if (this.m_dicPackageInterior.ContainsKey(nkmrandomBoxItemTemplet.m_RewardID))
					{
						Dictionary<int, int> dicPackageInterior = this.m_dicPackageInterior;
						int rewardID = nkmrandomBoxItemTemplet.m_RewardID;
						dicPackageInterior[rewardID] += nkmrandomBoxItemTemplet.TotalQuantity_Max;
					}
					else
					{
						this.m_dicPackageInterior[nkmrandomBoxItemTemplet.m_RewardID] = nkmrandomBoxItemTemplet.TotalQuantity_Max;
					}
				}
			}
			this.m_OfficeInteriorDetail.SetData(this.m_dicPackageInterior.Keys);
			NKCUtil.SetGameobjectActive(this.m_cstbnDetail, this.m_dicPackageInterior.Count > 0);
		}

		// Token: 0x06007D4E RID: 32078 RVA: 0x002A003C File Offset: 0x0029E23C
		private void OnClickDetail()
		{
			ShopItemTemplet.Find(base.ProductID);
			NKCUIPopupOfficeInteriorSelect.Instance.OpenForListView(this.m_dicPackageInterior);
		}

		// Token: 0x04006A11 RID: 27153
		[Header("가구 관련")]
		public NKCUIComOfficeInteriorDetail m_OfficeInteriorDetail;

		// Token: 0x04006A12 RID: 27154
		public NKCUIComStateButton m_cstbnDetail;

		// Token: 0x04006A13 RID: 27155
		private ShopItemTemplet m_shopTemplet;

		// Token: 0x04006A14 RID: 27156
		private Dictionary<int, int> m_dicPackageInterior;
	}
}
