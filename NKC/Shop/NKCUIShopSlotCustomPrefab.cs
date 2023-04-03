using System;
using System.Collections.Generic;
using NKC.Templet;
using NKM.Shop;
using UnityEngine;

namespace NKC.UI.Shop
{
	// Token: 0x02000AE0 RID: 2784
	public class NKCUIShopSlotCustomPrefab : MonoBehaviour
	{
		// Token: 0x06007D31 RID: 32049 RVA: 0x0029F898 File Offset: 0x0029DA98
		public void Init(NKCUIShopSlotBase.OnBuy onBuy, NKCUIShopSlotBase.OnRefreshRequired onRefreshRequired)
		{
			if (this.m_bInit)
			{
				return;
			}
			this.m_bInit = true;
			foreach (NKCUIShopSlotBase nkcuishopSlotBase in this.m_lstShopSlot)
			{
				nkcuishopSlotBase.Init(onBuy, onRefreshRequired);
			}
		}

		// Token: 0x06007D32 RID: 32050 RVA: 0x0029F8FC File Offset: 0x0029DAFC
		public void SetData(NKCUIShop uiShop, NKCShopCustomTabTemplet tabTemplet, NKCUIShopSlotBase.OnBuy onBuy, NKCUIShopSlotBase.OnRefreshRequired onRefreshRequired)
		{
			if (tabTemplet.m_UseProductID.Count != this.m_lstShopSlot.Count)
			{
				Debug.LogError("NKCShopCustomTabTemplet에 지정된 상품 수가 프리팹의 슬롯과 수가 맞지 않음");
			}
			if (!this.m_bInit)
			{
				this.Init(onBuy, onRefreshRequired);
			}
			for (int i = 0; i < this.m_lstShopSlot.Count; i++)
			{
				NKCUIShopSlotBase nkcuishopSlotBase = this.m_lstShopSlot[i];
				if (i < tabTemplet.m_UseProductID.Count)
				{
					NKCUtil.SetGameobjectActive(nkcuishopSlotBase, true);
					int num = tabTemplet.m_UseProductID[i];
					ShopItemTemplet shopTemplet = ShopItemTemplet.Find(num);
					bool bFirstBuy = NKCShopManager.IsFirstBuy(num, NKCScenManager.GetScenManager().GetMyUserData());
					int buyCountLeft = NKCShopManager.GetBuyCountLeft(num);
					nkcuishopSlotBase.SetData(uiShop, shopTemplet, buyCountLeft, bFirstBuy);
				}
				else
				{
					NKCUtil.SetGameobjectActive(nkcuishopSlotBase, false);
				}
			}
		}

		// Token: 0x04006A03 RID: 27139
		public List<NKCUIShopSlotBase> m_lstShopSlot;

		// Token: 0x04006A04 RID: 27140
		private bool m_bInit;
	}
}
