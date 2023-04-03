using System;
using System.Collections.Generic;
using Cs.Logging;
using NKC.UI.Shop;
using NKM;
using NKM.Shop;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x020009CB RID: 2507
	public class NKCUIPointExchangeSlot : MonoBehaviour
	{
		// Token: 0x06006AF8 RID: 27384 RVA: 0x0022C05C File Offset: 0x0022A25C
		public static NKCUIPointExchangeSlot GetNewInstance(Transform parent, string bundleName, string assetName)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>(bundleName, assetName, false, null);
			NKCUIPointExchangeSlot nkcuipointExchangeSlot = (nkcassetInstanceData != null) ? nkcassetInstanceData.m_Instant.GetComponent<NKCUIPointExchangeSlot>() : null;
			if (nkcuipointExchangeSlot == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKCUIPointExchangeSlot Prefab null!");
				return null;
			}
			nkcuipointExchangeSlot.m_InstanceData = nkcassetInstanceData;
			nkcuipointExchangeSlot.Init();
			if (parent != null)
			{
				nkcuipointExchangeSlot.transform.SetParent(parent);
			}
			nkcuipointExchangeSlot.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
			nkcuipointExchangeSlot.gameObject.SetActive(false);
			return nkcuipointExchangeSlot;
		}

		// Token: 0x06006AF9 RID: 27385 RVA: 0x0022C0EE File Offset: 0x0022A2EE
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06006AFA RID: 27386 RVA: 0x0022C110 File Offset: 0x0022A310
		private void Init()
		{
			NKMPointExchangeTemplet byTime = NKMPointExchangeTemplet.GetByTime(NKCSynchronizedTime.ServiceTime);
			if (byTime == null)
			{
				return;
			}
			List<ShopItemTemplet> itemTempletListByTab = NKCShopManager.GetItemTempletListByTab(ShopTabTemplet.Find(byTime.ShopTabStrId, byTime.ShopTabSubIndex), false);
			if (itemTempletListByTab == null)
			{
				Log.Debug("ShopItemTemplet not exist", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUIPointExchangeSlot.cs", 68);
				return;
			}
			List<ShopItemTemplet> list = itemTempletListByTab.FindAll((ShopItemTemplet e) => e.m_PointExchangeSpecial);
			int num = (list != null) ? list.Count : 0;
			this.InitProductSlot(num, true);
			this.InitProductSlot(itemTempletListByTab.Count - num, false);
		}

		// Token: 0x06006AFB RID: 27387 RVA: 0x0022C1A4 File Offset: 0x0022A3A4
		private void InitProductSlot(int productCount, bool isSpecialProduct)
		{
			Transform transform = isSpecialProduct ? this.m_specialRoot : this.m_normalRoot;
			if (transform == null)
			{
				return;
			}
			int childCount = transform.childCount;
			int num = productCount - childCount;
			for (int i = 0; i < num; i++)
			{
				if (childCount <= 0)
				{
					UnityEngine.Object.Instantiate<GameObject>(isSpecialProduct ? this.m_specialSlotPrefab : this.m_normalSlotPrefab, transform);
				}
				else
				{
					UnityEngine.Object.Instantiate<GameObject>(transform.GetChild(0).gameObject, transform);
				}
			}
			int childCount2 = transform.childCount;
			for (int j = 0; j < childCount2; j++)
			{
				NKCUIShopSlotSmall component = transform.GetChild(j).GetComponent<NKCUIShopSlotSmall>();
				if (component != null)
				{
					component.Init(new NKCUIShopSlotBase.OnBuy(this.OnClickProductBuy), null);
				}
				NKCUtil.SetGameobjectActive(transform.GetChild(j).gameObject, false);
			}
		}

		// Token: 0x06006AFC RID: 27388 RVA: 0x0022C268 File Offset: 0x0022A468
		public void SetData(NKMPointExchangeTemplet pointExchangeTemplet)
		{
			this.SetProductSlotData(pointExchangeTemplet, true);
			this.SetProductSlotData(pointExchangeTemplet, false);
		}

		// Token: 0x06006AFD RID: 27389 RVA: 0x0022C27C File Offset: 0x0022A47C
		private void SetProductSlotData(NKMPointExchangeTemplet pointExchangeTemplet, bool isSpecial)
		{
			if (pointExchangeTemplet == null)
			{
				return;
			}
			Transform transform = isSpecial ? this.m_specialRoot : this.m_normalRoot;
			if (transform == null)
			{
				return;
			}
			List<ShopItemTemplet> productList = this.GetProductList(pointExchangeTemplet, isSpecial);
			int count = productList.Count;
			int childCount = transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				if (i >= count)
				{
					NKCUtil.SetGameobjectActive(transform.GetChild(i).gameObject, false);
				}
				else
				{
					NKCUtil.SetGameobjectActive(transform.GetChild(i).gameObject, true);
					NKCUIShopSlotSmall component = transform.GetChild(i).GetComponent<NKCUIShopSlotSmall>();
					if (component != null)
					{
						component.SetData(null, productList[i], NKCShopManager.GetBuyCountLeft(productList[i].m_ProductID), false);
					}
				}
			}
		}

		// Token: 0x06006AFE RID: 27390 RVA: 0x0022C334 File Offset: 0x0022A534
		private List<ShopItemTemplet> GetProductList(NKMPointExchangeTemplet pointExchangeTemplet, bool isSpecial)
		{
			if (pointExchangeTemplet == null)
			{
				return new List<ShopItemTemplet>();
			}
			List<ShopItemTemplet> itemTempletListByTab = NKCShopManager.GetItemTempletListByTab(ShopTabTemplet.Find(pointExchangeTemplet.ShopTabStrId, pointExchangeTemplet.ShopTabSubIndex), false);
			if (itemTempletListByTab == null)
			{
				return new List<ShopItemTemplet>();
			}
			List<ShopItemTemplet> list = itemTempletListByTab.FindAll((ShopItemTemplet e) => e.m_PointExchangeSpecial == isSpecial);
			if (list == null)
			{
				return new List<ShopItemTemplet>();
			}
			return list;
		}

		// Token: 0x06006AFF RID: 27391 RVA: 0x0022C398 File Offset: 0x0022A598
		private void OnClickProductBuy(int ProductID)
		{
			NKM_ERROR_CODE nkm_ERROR_CODE = NKCShopManager.OnBtnProductBuy(ProductID, false);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK && nkm_ERROR_CODE == NKM_ERROR_CODE.NKE_FAIL_SHOP_INVALID_CHAIN_TAB)
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_SHOP_CHAIN_LOCKED, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
			}
		}

		// Token: 0x040056A1 RID: 22177
		public GameObject m_specialSlotPrefab;

		// Token: 0x040056A2 RID: 22178
		public GameObject m_normalSlotPrefab;

		// Token: 0x040056A3 RID: 22179
		public Transform m_specialRoot;

		// Token: 0x040056A4 RID: 22180
		public Transform m_normalRoot;

		// Token: 0x040056A5 RID: 22181
		private NKCAssetInstanceData m_InstanceData;
	}
}
