using System;
using NKC.Templet;
using NKM;
using NKM.Shop;
using UnityEngine;

namespace NKC.UI.Shop
{
	// Token: 0x02000AE1 RID: 2785
	public class NKCUIShopSlotCustomPrefabAdapter : NKCUIShopSlotBase
	{
		// Token: 0x06007D34 RID: 32052 RVA: 0x0029F9BC File Offset: 0x0029DBBC
		public void SetData(NKCUIShop uiShop, NKCShopCustomTabTemplet tabTemplet, NKCUIShopSlotBase.OnBuy onBuy, NKCUIShopSlotBase.OnRefreshRequired onRefreshRequired)
		{
			this.CleanUp();
			NKMAssetName nkmassetName = NKMAssetName.ParseBundleName(tabTemplet.m_UsePrefabName, tabTemplet.m_UsePrefabName);
			this.m_prefabInstance = NKCAssetResourceManager.OpenInstance<GameObject>(nkmassetName, false, null);
			if (this.m_prefabInstance != null && this.m_prefabInstance.m_Instant != null)
			{
				this.m_prefabInstance.m_Instant.transform.SetParent(this.m_rtPrefabRoot, false);
				NKCUIShopSlotCustomPrefab component = this.m_prefabInstance.m_Instant.GetComponent<NKCUIShopSlotCustomPrefab>();
				if (component != null)
				{
					component.SetData(uiShop, tabTemplet, onBuy, onRefreshRequired);
					return;
				}
			}
			else
			{
				Debug.Log(string.Format("SetData Fail, file : {0}", nkmassetName));
			}
		}

		// Token: 0x06007D35 RID: 32053 RVA: 0x0029FA5C File Offset: 0x0029DC5C
		private void CleanUp()
		{
			if (this.m_prefabInstance != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_prefabInstance);
				this.m_prefabInstance = null;
			}
		}

		// Token: 0x06007D36 RID: 32054 RVA: 0x0029FA78 File Offset: 0x0029DC78
		protected override void SetGoodsImage(ShopItemTemplet shopTemplet, bool bFirstBuy)
		{
		}

		// Token: 0x06007D37 RID: 32055 RVA: 0x0029FA7A File Offset: 0x0029DC7A
		protected override void SetInappPurchasePrice(ShopItemTemplet cShopItemTemplet, int price, bool bSale = false, int oldPrice = 0)
		{
		}

		// Token: 0x06007D38 RID: 32056 RVA: 0x0029FA7C File Offset: 0x0029DC7C
		protected override void SetPrice(int priceItemID, int Price, bool bSale = false, int oldPrice = 0)
		{
		}

		// Token: 0x04006A05 RID: 27141
		public RectTransform m_rtPrefabRoot;

		// Token: 0x04006A06 RID: 27142
		private NKCAssetInstanceData m_prefabInstance;
	}
}
