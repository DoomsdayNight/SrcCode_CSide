using System;
using NKM;
using NKM.Shop;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Shop
{
	// Token: 0x02000AE2 RID: 2786
	public class NKCUIShopSlotHomeBanner : MonoBehaviour
	{
		// Token: 0x06007D3A RID: 32058 RVA: 0x0029FA86 File Offset: 0x0029DC86
		public void Init()
		{
			NKCUtil.SetButtonClickDelegate(this.m_btn, new UnityAction(this.OnClickBtn));
			if (this.m_rtPrefabRoot == null)
			{
				this.m_rtPrefabRoot = base.GetComponent<RectTransform>();
			}
			this.m_bInitComplete = true;
		}

		// Token: 0x06007D3B RID: 32059 RVA: 0x0029FAC0 File Offset: 0x0029DCC0
		public void SetData(NKCShopBannerTemplet bannerTemplet, NKCUIShopSlotHomeBanner.OnBtn onBtn)
		{
			if (!this.m_bInitComplete)
			{
				this.Init();
			}
			if (bannerTemplet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			this.cNKCShopBannerTemplet = bannerTemplet;
			this.dOnBtn = onBtn;
			if (!string.IsNullOrEmpty(bannerTemplet.m_ShopHome_BannerPrefab))
			{
				NKMAssetName prefabData = NKMAssetName.ParseBundleName(bannerTemplet.m_ShopHome_BannerPrefab, bannerTemplet.m_ShopHome_BannerPrefab);
				this.SetPrefabData(prefabData);
			}
			else
			{
				NKMAssetName imageData = NKMAssetName.ParseBundleName("AB_UI_NKM_UI_SHOP_THUMBNAIL", bannerTemplet.m_ShopHome_BannerImage);
				this.SetImageData(imageData);
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
		}

		// Token: 0x06007D3C RID: 32060 RVA: 0x0029FB48 File Offset: 0x0029DD48
		public void SetPrefabData(NKMAssetName assetName)
		{
			this.CleanUp();
			this.m_prefabInstance = NKCAssetResourceManager.OpenInstance<GameObject>(assetName, false, null);
			if (this.m_prefabInstance != null && this.m_prefabInstance.m_Instant != null)
			{
				this.m_prefabInstance.m_Instant.transform.SetParent(this.m_rtPrefabRoot, false);
				ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(this.cNKCShopBannerTemplet.m_ProductID);
				IShopPrefab component = this.m_prefabInstance.m_Instant.GetComponent<IShopPrefab>();
				if (component != null)
				{
					component.SetData(shopItemTemplet);
				}
				IShopDataInjector[] componentsInChildren = this.m_prefabInstance.m_Instant.GetComponentsInChildren<IShopDataInjector>(true);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].TriggerInjectData(shopItemTemplet);
				}
			}
			else
			{
				Debug.Log(string.Format("SetPrefabData Fail, file : {0}", assetName));
			}
			NKCUtil.SetGameobjectActive(this.m_rtPrefabRoot, true);
			NKCUtil.SetGameobjectActive(this.m_Image, false);
		}

		// Token: 0x06007D3D RID: 32061 RVA: 0x0029FC22 File Offset: 0x0029DE22
		public void SetImageData(NKMAssetName assetName)
		{
			this.CleanUp();
			NKCUtil.SetImageSprite(this.m_Image, NKCResourceUtility.GetOrLoadAssetResource<Sprite>(assetName), false);
			NKCUtil.SetGameobjectActive(this.m_Image, true);
			NKCUtil.SetGameobjectActive(this.m_rtPrefabRoot, false);
		}

		// Token: 0x06007D3E RID: 32062 RVA: 0x0029FC54 File Offset: 0x0029DE54
		private void CleanUp()
		{
			if (this.m_prefabInstance != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_prefabInstance);
				this.m_prefabInstance = null;
			}
		}

		// Token: 0x06007D3F RID: 32063 RVA: 0x0029FC70 File Offset: 0x0029DE70
		private void OnClickBtn()
		{
			NKCUIShopSlotHomeBanner.OnBtn onBtn = this.dOnBtn;
			if (onBtn == null)
			{
				return;
			}
			onBtn(this.cNKCShopBannerTemplet);
		}

		// Token: 0x06007D40 RID: 32064 RVA: 0x0029FC88 File Offset: 0x0029DE88
		private void OnDestroy()
		{
			this.CleanUp();
		}

		// Token: 0x04006A07 RID: 27143
		public NKCUIComStateButton m_btn;

		// Token: 0x04006A08 RID: 27144
		public Image m_Image;

		// Token: 0x04006A09 RID: 27145
		public RectTransform m_rtPrefabRoot;

		// Token: 0x04006A0A RID: 27146
		private NKCAssetInstanceData m_prefabInstance;

		// Token: 0x04006A0B RID: 27147
		private NKCUIShopSlotHomeBanner.OnBtn dOnBtn;

		// Token: 0x04006A0C RID: 27148
		private NKCShopBannerTemplet cNKCShopBannerTemplet;

		// Token: 0x04006A0D RID: 27149
		private bool m_bInitComplete;

		// Token: 0x0200185C RID: 6236
		// (Invoke) Token: 0x0600B5BB RID: 46523
		public delegate void OnBtn(NKCShopBannerTemplet bannerTemplet);
	}
}
