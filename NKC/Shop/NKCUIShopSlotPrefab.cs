using System;
using NKM;
using NKM.Shop;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.Shop
{
	// Token: 0x02000AE5 RID: 2789
	public class NKCUIShopSlotPrefab : NKCUIShopSlotCard
	{
		// Token: 0x06007D45 RID: 32069 RVA: 0x0029FC98 File Offset: 0x0029DE98
		protected override void SetGoodsImage(ShopItemTemplet shopTemplet, bool bFirstBuy)
		{
			if (!string.IsNullOrEmpty(shopTemplet.m_CardPrefab))
			{
				NKMAssetName assetName = NKMAssetName.ParseBundleName(shopTemplet.m_CardPrefab, shopTemplet.m_CardPrefab);
				this.SetPrefabData(assetName, shopTemplet);
				return;
			}
			NKMAssetName imageData = NKMAssetName.ParseBundleName("AB_UI_NKM_UI_SHOP_IMG", shopTemplet.m_CardImage);
			this.SetImageData(imageData);
		}

		// Token: 0x06007D46 RID: 32070 RVA: 0x0029FCE8 File Offset: 0x0029DEE8
		private void SetPrefabData(NKMAssetName assetName, ShopItemTemplet shopTemplet)
		{
			this.CleanUp();
			this.m_prefabInstance = NKCAssetResourceManager.OpenInstance<GameObject>(assetName, false, null);
			if (this.m_prefabInstance != null && this.m_prefabInstance.m_Instant != null)
			{
				this.m_prefabInstance.m_Instant.transform.SetParent(this.m_rtPrefabRoot, false);
				IShopPrefab component = this.m_prefabInstance.m_Instant.GetComponent<IShopPrefab>();
				if (component != null)
				{
					component.SetData(shopTemplet);
				}
				IShopDataInjector[] componentsInChildren = this.m_prefabInstance.m_Instant.GetComponentsInChildren<IShopDataInjector>(true);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].TriggerInjectData(shopTemplet);
				}
			}
			else
			{
				Debug.Log(string.Format("SetPrefabData Fail, file : {0}", assetName));
			}
			NKCUtil.SetGameobjectActive(this.m_imgItem, false);
			NKCUtil.SetGameobjectActive(this.m_rtPrefabRoot, true);
		}

		// Token: 0x06007D47 RID: 32071 RVA: 0x0029FDAE File Offset: 0x0029DFAE
		public void SetImageData(NKMAssetName assetName)
		{
			this.CleanUp();
			NKCUtil.SetImageSprite(this.m_imgItem, NKCResourceUtility.GetOrLoadAssetResource<Sprite>(assetName), false);
			NKCUtil.SetGameobjectActive(this.m_imgItem, true);
			NKCUtil.SetGameobjectActive(this.m_rtPrefabRoot, false);
		}

		// Token: 0x06007D48 RID: 32072 RVA: 0x0029FDE0 File Offset: 0x0029DFE0
		protected override void PostSetData(ShopItemTemplet shopTemplet)
		{
			bool bIsFirstBuy = NKCShopManager.IsFirstBuy(shopTemplet.m_ProductID, NKCScenManager.CurrentUserData());
			if (this.m_prefabInstance != null && this.m_prefabInstance.m_Instant != null)
			{
				IShopPrefab component = this.m_prefabInstance.m_Instant.GetComponent<IShopPrefab>();
				if (component != null && component.IsHideLockObject())
				{
					NKCUtil.SetGameobjectActive(this.m_objLocked, false);
					NKCUtil.SetGameobjectActive(this.m_objLockedTime, false);
				}
				NKCUIComShopBuyButton componentInChildren = this.m_prefabInstance.m_Instant.GetComponentInChildren<NKCUIComShopBuyButton>();
				if (componentInChildren != null)
				{
					componentInChildren.SetData(shopTemplet, new UnityAction(base.OnBtnBuy), bIsFirstBuy);
					NKCUtil.SetGameobjectActive(this.m_cbtnBuy, false);
					NKCUtil.SetGameobjectActive(this.m_objSoldOut, false);
				}
			}
		}

		// Token: 0x06007D49 RID: 32073 RVA: 0x0029FE97 File Offset: 0x0029E097
		private void CleanUp()
		{
			if (this.m_prefabInstance != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_prefabInstance);
				this.m_prefabInstance = null;
			}
		}

		// Token: 0x06007D4A RID: 32074 RVA: 0x0029FEB3 File Offset: 0x0029E0B3
		public void ShowBannerOnly(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_objUIRoot, !value);
		}

		// Token: 0x04006A0E RID: 27150
		[Header("프리팹 슬롯 대응")]
		public RectTransform m_rtPrefabRoot;

		// Token: 0x04006A0F RID: 27151
		public GameObject m_objUIRoot;

		// Token: 0x04006A10 RID: 27152
		private NKCAssetInstanceData m_prefabInstance;
	}
}
