using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Logging;
using NKC.Publisher;
using NKC.Templet;
using NKC.UI.Component;
using NKC.UI.NPC;
using NKM;
using NKM.Shop;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Shop
{
	// Token: 0x02000AD8 RID: 2776
	public class NKCUIShop : NKCUIBase
	{
		// Token: 0x170014B0 RID: 5296
		// (get) Token: 0x06007C4A RID: 31818 RVA: 0x00298544 File Offset: 0x00296744
		public static NKCUIShop Instance
		{
			get
			{
				if (NKCUIShop.m_Instance == null)
				{
					NKCUIShop.m_Instance = NKCUIManager.OpenNewInstance<NKCUIShop>("ab_ui_nkm_ui_shop", "NKM_UI_SHOP", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIShop.CleanupInstance)).GetInstance<NKCUIShop>();
					NKCUIShop.m_Instance.Init();
				}
				return NKCUIShop.m_Instance;
			}
		}

		// Token: 0x06007C4B RID: 31819 RVA: 0x00298593 File Offset: 0x00296793
		private static void CleanupInstance()
		{
			NKCUIShop.m_Instance = null;
		}

		// Token: 0x170014B1 RID: 5297
		// (get) Token: 0x06007C4C RID: 31820 RVA: 0x0029859B File Offset: 0x0029679B
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIShop.m_Instance != null && NKCUIShop.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007C4D RID: 31821 RVA: 0x002985B6 File Offset: 0x002967B6
		public static void CheckInstanceAndClose()
		{
			if (NKCUIShop.m_Instance != null && NKCUIShop.m_Instance.IsOpen)
			{
				NKCUIShop.m_Instance.Close();
			}
		}

		// Token: 0x06007C4E RID: 31822 RVA: 0x002985DC File Offset: 0x002967DC
		public static void ShopShortcut(string tabType = "TAB_NONE", int subIndex = 0, int reservedProductID = 0)
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.LOBBY_SUBMENU, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.LOBBY_SUBMENU, 0);
				return;
			}
			NKCShopCategoryTemplet categoryFromTab = NKCShopManager.GetCategoryFromTab(tabType);
			if (categoryFromTab != null)
			{
				NKCUIShop.ShopShortcut(categoryFromTab.m_eCategory, tabType, subIndex, reservedProductID);
				return;
			}
			Debug.LogError("Category not found from tab " + tabType + "!!");
		}

		// Token: 0x06007C4F RID: 31823 RVA: 0x0029862A File Offset: 0x0029682A
		public static void ShopShortcut(NKCShopManager.ShopTabCategory category, string tabType = "TAB_NONE", int subIndex = 0, int reservedProductID = 0)
		{
			if (!NKCUIShop.IsInstanceOpen)
			{
				NKCUIShop.Instance.Open(category, tabType, subIndex, reservedProductID, NKCUIShop.eTabMode.Fold);
				return;
			}
			NKCUIShop.Instance.ChangeCategory(category, tabType, subIndex);
			if (ShopItemTemplet.Find(reservedProductID) != null)
			{
				NKCShopManager.OnBtnProductBuy(reservedProductID, false);
			}
		}

		// Token: 0x170014B2 RID: 5298
		// (get) Token: 0x06007C50 RID: 31824 RVA: 0x00298660 File Offset: 0x00296860
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.ON_PLAY_GAME;
			}
		}

		// Token: 0x170014B3 RID: 5299
		// (get) Token: 0x06007C51 RID: 31825 RVA: 0x00298663 File Offset: 0x00296863
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170014B4 RID: 5300
		// (get) Token: 0x06007C52 RID: 31826 RVA: 0x00298666 File Offset: 0x00296866
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_SHOP;
			}
		}

		// Token: 0x06007C53 RID: 31827 RVA: 0x0029866D File Offset: 0x0029686D
		public override void CloseInternal()
		{
			NKCSoundManager.StopAllSound(SOUND_TRACK.VOICE);
			NKCShopManager.ClearLinkedItemCache();
			if (this.m_bPlayedScenMusic)
			{
				NKCSoundManager.PlayScenMusic();
				this.m_bPlayedScenMusic = false;
			}
			base.gameObject.SetActive(false);
			NKCShopManager.SetLastCheckedUTCTime(this.m_eCurrentTab, this.m_CurrentSubIndex);
		}

		// Token: 0x170014B5 RID: 5301
		// (get) Token: 0x06007C54 RID: 31828 RVA: 0x002986AB File Offset: 0x002968AB
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return this.RESOURCE_LIST;
			}
		}

		// Token: 0x06007C55 RID: 31829 RVA: 0x002986B4 File Offset: 0x002968B4
		public override void OnBackButton()
		{
			if (this.m_uiShopCategoryChange != null && this.m_uiShopCategoryChange.gameObject.activeSelf)
			{
				NKCUtil.SetGameobjectActive(this.m_uiShopCategoryChange, false);
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_SHOP)
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, false);
				return;
			}
			base.Close();
		}

		// Token: 0x06007C56 RID: 31830 RVA: 0x0029870F File Offset: 0x0029690F
		private IEnumerable<NKCUIShop.DisplaySet> GetAllDisplaySet()
		{
			foreach (KeyValuePair<ShopDisplayType, NKCUIShop.DisplaySet> keyValuePair in this.m_dicLoopScrollSet)
			{
				if (keyValuePair.Value.scrollRect != null)
				{
					yield return keyValuePair.Value;
				}
			}
			Dictionary<ShopDisplayType, NKCUIShop.DisplaySet>.Enumerator enumerator = default(Dictionary<ShopDisplayType, NKCUIShop.DisplaySet>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06007C57 RID: 31831 RVA: 0x0029871F File Offset: 0x0029691F
		private IEnumerable<NKCUIShop.FullDisplaySet> GetAllFullDisplaySet()
		{
			foreach (KeyValuePair<ShopDisplayType, NKCUIShop.FullDisplaySet> keyValuePair in this.m_dicFullDisplaySet)
			{
				if (keyValuePair.Value.draggablePanel != null)
				{
					yield return keyValuePair.Value;
				}
			}
			Dictionary<ShopDisplayType, NKCUIShop.FullDisplaySet>.Enumerator enumerator = default(Dictionary<ShopDisplayType, NKCUIShop.FullDisplaySet>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x170014B6 RID: 5302
		// (get) Token: 0x06007C58 RID: 31832 RVA: 0x0029872F File Offset: 0x0029692F
		protected virtual bool AlwaysShowNPC
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170014B7 RID: 5303
		// (get) Token: 0x06007C59 RID: 31833 RVA: 0x00298732 File Offset: 0x00296932
		protected virtual bool UseTabVisible
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007C5A RID: 31834 RVA: 0x00298738 File Offset: 0x00296938
		private NKCUIShopTab GetUIShopTab(ShopTabTemplet shopTabTemplet)
		{
			NKCUIShop.eTabMode eTabMode = this.m_eTabMode;
			string text;
			if (eTabMode == NKCUIShop.eTabMode.Fold || eTabMode != NKCUIShop.eTabMode.All)
			{
				text = string.Format("{0}[{1}]", shopTabTemplet.TabType, 0);
			}
			else
			{
				text = this.MakeTabUIKey(shopTabTemplet);
			}
			if (text == null)
			{
				return null;
			}
			if (this.m_dicTab.ContainsKey(text))
			{
				return this.m_dicTab[text];
			}
			return null;
		}

		// Token: 0x06007C5B RID: 31835 RVA: 0x00298795 File Offset: 0x00296995
		private string MakeTabUIKey(string tabType, int subIndex)
		{
			return string.Format("{0}[{1}]", tabType, subIndex);
		}

		// Token: 0x06007C5C RID: 31836 RVA: 0x002987A8 File Offset: 0x002969A8
		private string MakeTabUIKey(ShopTabTemplet tabTemplet)
		{
			if (tabTemplet == null)
			{
				return null;
			}
			return string.Format("{0}[{1}]", tabTemplet.TabType, tabTemplet.SubIndex);
		}

		// Token: 0x06007C5D RID: 31837 RVA: 0x002987CA File Offset: 0x002969CA
		public string GetShortcutParam()
		{
			return string.Format("{0},{1}", this.m_eCurrentTab, this.m_CurrentSubIndex);
		}

		// Token: 0x06007C5E RID: 31838 RVA: 0x002987E7 File Offset: 0x002969E7
		private RectTransform GetRecommendObject()
		{
			if (this.m_stkLevelPackageObjects.Count > 0)
			{
				return this.m_stkLevelPackageObjects.Pop().GetComponent<RectTransform>();
			}
			return UnityEngine.Object.Instantiate<NKCUIShopSlotHomeBanner>(this.m_pfbHomeLimitPackageSlot).GetComponent<RectTransform>();
		}

		// Token: 0x06007C5F RID: 31839 RVA: 0x00298818 File Offset: 0x00296A18
		private void ReturnRecommendObject(RectTransform rect)
		{
			NKCUIShopSlotHomeBanner component = rect.GetComponent<NKCUIShopSlotHomeBanner>();
			if (component != null)
			{
				this.m_stkLevelPackageObjects.Push(component);
			}
			rect.gameObject.SetActive(false);
			rect.parent = base.transform;
		}

		// Token: 0x06007C60 RID: 31840 RVA: 0x0029885C File Offset: 0x00296A5C
		private void ProvideRecommendData(RectTransform rect, int idx)
		{
			NKCUIShopSlotHomeBanner component = rect.GetComponent<NKCUIShopSlotHomeBanner>();
			if (component != null)
			{
				rect.SetParent(this.m_HomePackageSlidePanel.transform);
				NKCUtil.SetGameobjectActive(component, true);
				component.SetData(this.m_lstHomeBannerTemplet[idx], new NKCUIShopSlotHomeBanner.OnBtn(this.OnBtnBanner));
			}
		}

		// Token: 0x06007C61 RID: 31841 RVA: 0x002988AF File Offset: 0x00296AAF
		private void OnRecommendFocus(RectTransform rect, bool bFocus)
		{
			if (bFocus)
			{
				rect.GetComponent<NKCUIShopSlotHomeBanner>();
			}
		}

		// Token: 0x06007C62 RID: 31842 RVA: 0x002988BC File Offset: 0x00296ABC
		private List<NKCUIShopSlotBase> GetSlotList(ShopDisplayType type)
		{
			List<NKCUIShopSlotBase> result;
			if (this.m_dicCardSlot.TryGetValue(type, out result))
			{
				return result;
			}
			List<NKCUIShopSlotBase> list = new List<NKCUIShopSlotBase>();
			this.m_dicCardSlot.Add(type, list);
			return list;
		}

		// Token: 0x06007C63 RID: 31843 RVA: 0x002988F0 File Offset: 0x00296AF0
		private LoopScrollRect GetLoopScrollRect(ShopDisplayType type)
		{
			NKCUIShop.DisplaySet displaySet;
			if (this.m_dicLoopScrollSet.TryGetValue(type, out displaySet))
			{
				return displaySet.scrollRect;
			}
			return null;
		}

		// Token: 0x06007C64 RID: 31844 RVA: 0x00298918 File Offset: 0x00296B18
		private NKCUIComDragSelectablePanel GetDragPanel(ShopDisplayType type)
		{
			NKCUIShop.FullDisplaySet fullDisplaySet;
			if (this.m_dicFullDisplaySet.TryGetValue(type, out fullDisplaySet))
			{
				return fullDisplaySet.draggablePanel;
			}
			return null;
		}

		// Token: 0x06007C65 RID: 31845 RVA: 0x00298940 File Offset: 0x00296B40
		private RectTransform GetShopSlot(ShopDisplayType type, int index)
		{
			NKCUIShopSlotBase nkcuishopSlotBase;
			if (this.m_dicSlotStack.ContainsKey(type) && this.m_dicSlotStack[type] != null && this.m_dicSlotStack[type].Count > 0)
			{
				nkcuishopSlotBase = this.m_dicSlotStack[type].Pop();
			}
			else
			{
				NKCUIShop.DisplaySet displaySet;
				NKCUIShopSlotBase slotPrefab;
				if (this.m_dicLoopScrollSet.TryGetValue(type, out displaySet))
				{
					slotPrefab = displaySet.slotPrefab;
				}
				else
				{
					NKCUIShop.FullDisplaySet fullDisplaySet;
					if (!this.m_dicFullDisplaySet.TryGetValue(type, out fullDisplaySet))
					{
						return null;
					}
					slotPrefab = fullDisplaySet.slotPrefab;
				}
				if (slotPrefab == null)
				{
					Debug.LogError("shop slot prefab null!");
					return null;
				}
				nkcuishopSlotBase = UnityEngine.Object.Instantiate<NKCUIShopSlotBase>(slotPrefab);
			}
			nkcuishopSlotBase.Init(new NKCUIShopSlotBase.OnBuy(this.OnBtnProductBuy), new NKCUIShopSlotBase.OnRefreshRequired(this.ForceUpdateItemList));
			List<NKCUIShopSlotBase> slotList = this.GetSlotList(type);
			if (slotList != null)
			{
				slotList.Add(nkcuishopSlotBase);
			}
			return nkcuishopSlotBase.GetComponent<RectTransform>();
		}

		// Token: 0x06007C66 RID: 31846 RVA: 0x00298A18 File Offset: 0x00296C18
		private void ReturnShopSlot(ShopDisplayType type, Transform tr)
		{
			NKCUIShopSlotBase component = tr.GetComponent<NKCUIShopSlotBase>();
			this.GetSlotList(type).Remove(component);
			tr.gameObject.SetActive(false);
			tr.SetParent(this.m_ReturnedSlotParent);
			if (!this.m_dicSlotStack.ContainsKey(type))
			{
				this.m_dicSlotStack.Add(type, new Stack<NKCUIShopSlotBase>());
			}
			this.m_dicSlotStack[type].Push(component);
		}

		// Token: 0x06007C67 RID: 31847 RVA: 0x00298A83 File Offset: 0x00296C83
		private void ReturnCommonFullDisplayShopSlot(Transform tr)
		{
			this.ReturnShopSlot(this.m_eCurrentCommonFulldisplaySet, tr);
		}

		// Token: 0x06007C68 RID: 31848 RVA: 0x00298A94 File Offset: 0x00296C94
		private void ProvideShopSlotData(Transform transform, int idx)
		{
			NKCUIShopSlotBase component = transform.GetComponent<NKCUIShopSlotBase>();
			if (component == null)
			{
				return;
			}
			component.SetOverrideImageAsset("");
			if (this.m_eCurrentTab == "TAB_SUPPLY")
			{
				NKMShopRandomData randomShop = NKCScenManager.GetScenManager().GetMyUserData().m_ShopData.randomShop;
				int num = idx + 1;
				if (randomShop.datas.ContainsKey(num))
				{
					NKMShopRandomListData shopRandomTemplet = randomShop.datas[num];
					bool bValue = component.SetData(this, shopRandomTemplet, num);
					NKCUtil.SetGameobjectActive(component, bValue);
					return;
				}
				NKCUtil.SetGameobjectActive(component, false);
				return;
			}
			else
			{
				if (this.m_lstFeatured.Count > 0)
				{
					NKCShopFeaturedTemplet nkcshopFeaturedTemplet = this.m_lstFeatured[idx];
					ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(nkcshopFeaturedTemplet.m_PackageID);
					component.SetOverrideImageAsset(nkcshopFeaturedTemplet.m_FeaturedImage);
					bool bValue2 = component.SetData(this, shopItemTemplet, NKCShopManager.GetBuyCountLeft(shopItemTemplet.m_ProductID), NKCUIShop.IsFirstBuy(shopItemTemplet.m_ProductID));
					NKCUtil.SetGameobjectActive(component, bValue2);
					return;
				}
				if (this.m_ssProduct != null)
				{
					if (idx < this.m_ssProduct.SortedProductList.Count)
					{
						ShopItemTemplet shopItemTemplet2 = this.m_ssProduct.SortedProductList[idx];
						bool bValue3 = component.SetData(this, shopItemTemplet2, NKCShopManager.GetBuyCountLeft(shopItemTemplet2.m_ProductID), NKCUIShop.IsFirstBuy(shopItemTemplet2.m_ProductID));
						NKCUtil.SetGameobjectActive(component, bValue3);
						return;
					}
				}
				else
				{
					if (this.m_lstShopItem != null && idx < this.m_lstShopItem.Count)
					{
						ShopItemTemplet shopItemTemplet3 = this.m_lstShopItem[idx];
						bool bValue4 = component.SetData(this, shopItemTemplet3, NKCShopManager.GetBuyCountLeft(shopItemTemplet3.m_ProductID), NKCUIShop.IsFirstBuy(shopItemTemplet3.m_ProductID));
						NKCUtil.SetGameobjectActive(component, bValue4);
						return;
					}
					NKCUtil.SetGameobjectActive(component, false);
				}
				return;
			}
		}

		// Token: 0x06007C69 RID: 31849 RVA: 0x00298C40 File Offset: 0x00296E40
		public void Init()
		{
			this.m_dicLoopScrollSet.Clear();
			using (List<NKCUIShop.DisplaySet>.Enumerator enumerator = this.m_lstDisplaySet.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NKCUIShop.DisplaySet displaySet = enumerator.Current;
					this.m_dicLoopScrollSet.Add(displaySet.slotType, displaySet);
					LoopScrollRect scrollRect = displaySet.scrollRect;
					if (scrollRect != null)
					{
						scrollRect.dOnGetObject += ((int index) => this.GetShopSlot(displaySet.slotType, index));
						scrollRect.dOnReturnObject += delegate(Transform tr)
						{
							this.ReturnShopSlot(displaySet.slotType, tr);
						};
						scrollRect.dOnProvideData += this.ProvideShopSlotData;
						scrollRect.PrepareCells(0);
						NKCUtil.SetGameobjectActive(scrollRect, false);
						if (displaySet.slotType != ShopDisplayType.Main)
						{
							NKCUtil.SetScrollHotKey(scrollRect, null);
						}
					}
				}
			}
			this.m_dicFullDisplaySet.Clear();
			using (List<NKCUIShop.FullDisplaySet>.Enumerator enumerator2 = this.m_lstFullDisplaySet.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					NKCUIShop.FullDisplaySet fulldisplaySet = enumerator2.Current;
					this.m_dicFullDisplaySet.Add(fulldisplaySet.slotType, fulldisplaySet);
					NKCUIComDragSelectablePanel draggablePanel = fulldisplaySet.draggablePanel;
					if (draggablePanel != null)
					{
						draggablePanel.Init(false, true);
						draggablePanel.dOnGetObject += (() => this.GetShopSlot(fulldisplaySet.slotType, 0));
						draggablePanel.dOnReturnObject += delegate(RectTransform tr)
						{
							this.ReturnShopSlot(fulldisplaySet.slotType, tr);
						};
						draggablePanel.dOnProvideData += new NKCUIComDragSelectablePanel.OnProvideData(this.ProvideFullDisplayData);
						NKCUtil.SetGameobjectActive(draggablePanel, false);
					}
				}
			}
			if (this.m_CommonFullDisplayDragSelectPanel != null)
			{
				this.m_CommonFullDisplayDragSelectPanel.Init(false, true);
				this.m_CommonFullDisplayDragSelectPanel.dOnGetObject += (() => this.GetShopSlot(this.m_eDisplayType, 0));
				this.m_CommonFullDisplayDragSelectPanel.dOnReturnObject += new NKCUIComDragSelectablePanel.OnReturnObject(this.ReturnCommonFullDisplayShopSlot);
				this.m_CommonFullDisplayDragSelectPanel.dOnProvideData += new NKCUIComDragSelectablePanel.OnProvideData(this.ProvideFullDisplayData);
			}
			NKCUtil.SetButtonClickDelegate(this.m_cbtnSupplyRefresh, new UnityAction(this.OnBtnSupplyRefresh));
			NKCUINPCSpine uinpcshop = this.m_UINPCShop;
			if (uinpcshop != null)
			{
				uinpcshop.Init(true);
			}
			if (this.m_HomePackageSlidePanel != null)
			{
				this.m_HomePackageSlidePanel.Init(true, true);
				this.m_HomePackageSlidePanel.dOnGetObject += this.GetRecommendObject;
				this.m_HomePackageSlidePanel.dOnReturnObject += this.ReturnRecommendObject;
				this.m_HomePackageSlidePanel.dOnProvideData += this.ProvideRecommendData;
				this.m_HomePackageSlidePanel.dOnFocus += this.OnRecommendFocus;
			}
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglChain_01, new UnityAction<bool>(this.OnChainTab_01));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglChain_02, new UnityAction<bool>(this.OnChainTab_02));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglChain_03, new UnityAction<bool>(this.OnChainTab_03));
			NKCUtil.SetButtonClickDelegate(this.m_btnBuyAll, new UnityAction(this.OnBtnBuyAll));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnChangeCategory, new UnityAction(this.OnChangeCategory));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnFilter, new UnityAction(this.OnBtnFilter));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnFilterActive, new UnityAction(this.OnBtnFilter));
			NKCUIShopCategoryChange uiShopCategoryChange = this.m_uiShopCategoryChange;
			if (uiShopCategoryChange != null)
			{
				uiShopCategoryChange.Init(new NKCUIShopCategoryChangeSlot.OnSelectCategory(this._ChangeCategory));
			}
			NKCUtil.SetGameobjectActive(this.m_uiShopCategoryChange, false);
			this.SetJPNPolicyTabUI();
		}

		// Token: 0x06007C6A RID: 31850 RVA: 0x00298FEC File Offset: 0x002971EC
		public void Open(NKCShopManager.ShopTabCategory category, string selectedTab = "TAB_MAIN", int subTabIndex = 0, int reservedProductID = 0, NKCUIShop.eTabMode tabMode = NKCUIShop.eTabMode.Fold)
		{
			if (NKCShopCategoryTemplet.Find(category) == null)
			{
				foreach (object obj in Enum.GetValues(typeof(NKCShopManager.ShopTabCategory)))
				{
					NKCShopManager.ShopTabCategory category2 = (NKCShopManager.ShopTabCategory)obj;
					if (NKCShopCategoryTemplet.Find(category2) != null)
					{
						category = category2;
					}
				}
			}
			base.gameObject.SetActive(true);
			this.m_eTabMode = tabMode;
			NKCSoundManager.PlayScenMusic(NKM_SCEN_ID.NSI_SHOP, false);
			this.m_bPlayedScenMusic = true;
			this.SetOffAllObjects();
			NKCUtil.SetGameobjectActive(this.m_objFetchItem, true);
			NKCShopManager.FetchShopItemList(NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, delegate(bool bSuccess)
			{
				if (bSuccess)
				{
					this.OpenProcess(category, selectedTab, subTabIndex, reservedProductID, tabMode);
					return;
				}
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_SHOP_WAS_NOT_ABLE_TO_GET_PRODUCT_LIST_FROM_SERVER, new NKCPopupOKCancel.OnButton(this.Close), "");
			}, false);
		}

		// Token: 0x06007C6B RID: 31851 RVA: 0x002990E4 File Offset: 0x002972E4
		private void SetOffAllObjects()
		{
			foreach (NKCUIShop.DisplaySet displaySet in this.GetAllDisplaySet())
			{
				NKCUtil.SetGameobjectActive(displaySet.m_rtParent, false);
				NKCUtil.SetGameobjectActive(displaySet.scrollRect, false);
			}
			foreach (NKCUIShop.FullDisplaySet fullDisplaySet in this.GetAllFullDisplaySet())
			{
				NKCUtil.SetGameobjectActive(fullDisplaySet.m_rtParent, false);
				NKCUtil.SetGameobjectActive(fullDisplaySet.draggablePanel, false);
			}
			NKCUtil.SetGameobjectActive(this.m_lbSupplyTimeLeft, false);
			NKCUtil.SetGameobjectActive(this.m_cbtnSupplyRefresh, false);
			NKCUtil.SetGameobjectActive(this.m_lbSupplyCountLeft, false);
			NKCUtil.SetGameobjectActive(this.m_objTabResetTime, false);
			NKCUtil.SetGameobjectActive(this.m_objChainTab, false);
			NKCUtil.SetGameobjectActive(this.m_objTopBanner, false);
			NKCUtil.SetGameobjectActive(this.m_objEmptyList, false);
			NKCUtil.SetGameobjectActive(this.m_objChainLocked, false);
			NKCUtil.SetGameobjectActive(this.m_btnBuyAll, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnFilter, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnFilterActive, false);
			NKCUtil.SetGameobjectActive(this.m_uiShopCategoryChange, false);
			NKCUtil.SetGameobjectActive(this.m_objCategoryReddot, false);
		}

		// Token: 0x06007C6C RID: 31852 RVA: 0x00299228 File Offset: 0x00297428
		private void OpenProcess(NKCShopManager.ShopTabCategory category, string selectedTab, int subTabIndex, int reservedProductID, NKCUIShop.eTabMode tabMode)
		{
			NKCUtil.SetGameobjectActive(this.m_objFetchItem, false);
			this.BuildProductList(false);
			this.BuildTabs(category, selectedTab, subTabIndex, true);
			NKCUtil.SetLabelText(this.m_lbSupplyRefreshCost, 15.ToString());
			base.gameObject.SetActive(true);
			base.UIOpened(true);
			this.playingTutorial = this.TutorialCheck();
			if (!this.playingTutorial)
			{
				if (ShopItemTemplet.Find(reservedProductID) != null)
				{
					NKCShopManager.OnBtnProductBuy(reservedProductID, false);
					return;
				}
				NKCUINPCSpine uinpcshop = this.m_UINPCShop;
				if (uinpcshop == null)
				{
					return;
				}
				uinpcshop.PlayAni(NPC_ACTION_TYPE.START, false);
			}
		}

		// Token: 0x06007C6D RID: 31853 RVA: 0x002992B4 File Offset: 0x002974B4
		private void BuildTabs(NKCShopManager.ShopTabCategory category, string selectedTab, int subTabIndex, bool bForceRebuildTabs = false)
		{
			if (this.m_eCategory != category || bForceRebuildTabs)
			{
				this.CleanupTab();
			}
			this.m_eCategory = category;
			List<ShopTabTemplet> useTabList = NKCShopManager.GetUseTabList(this.m_eCategory);
			if (this.m_dicTab.Count == 0)
			{
				this.BuildTabs(useTabList);
			}
			if (!string.IsNullOrEmpty(selectedTab) && selectedTab != "TAB_NONE" && !useTabList.Exists((ShopTabTemplet x) => x.TabType == selectedTab))
			{
				Debug.LogError(string.Format("Tab {0} does not exist in category {1}!", selectedTab, category));
				selectedTab = "TAB_NONE";
			}
			if (selectedTab == "TAB_NONE")
			{
				if (this.m_lstEnabledTabs != null && this.m_lstEnabledTabs.Count > 0)
				{
					selectedTab = this.m_lstEnabledTabs[0].TabType;
				}
				else if (useTabList.Count > 0)
				{
					selectedTab = useTabList[0].TabType;
				}
			}
			this.SelectTab(selectedTab, subTabIndex, true, false);
		}

		// Token: 0x06007C6E RID: 31854 RVA: 0x002993D0 File Offset: 0x002975D0
		private void SetJPNPolicyTabUI()
		{
			if (this.m_srTab != null)
			{
				RectTransform component = this.m_srTab.GetComponent<RectTransform>();
				if (component != null)
				{
					if (NKCPublisherModule.InAppPurchase.ShowJPNPaymentPolicy())
					{
						NKCUtil.SetButtonClickDelegate(this.m_csbtnJPNPaymentLaw, new UnityAction(this.OnBtnJPNPaymentLaw));
						NKCUtil.SetButtonClickDelegate(this.m_csbtnJPNCommecialLaw, new UnityAction(this.OnBtnJPNCommercialLaw));
						component.offsetMin = new Vector2(component.offsetMin.x, 220f);
						NKCUtil.SetGameobjectActive(this.m_objJPNPolicy, true);
						return;
					}
					component.offsetMin = new Vector2(component.offsetMin.x, 116f);
					NKCUtil.SetGameobjectActive(this.m_objJPNPolicy, false);
				}
			}
		}

		// Token: 0x06007C6F RID: 31855 RVA: 0x0029948F File Offset: 0x0029768F
		public override void UnHide()
		{
			base.UnHide();
			if (this.playingTutorial)
			{
				NKCUINPCSpine uinpcshop = this.m_UINPCShop;
				if (uinpcshop != null)
				{
					uinpcshop.PlayAni(NPC_ACTION_TYPE.START, false);
				}
				this.playingTutorial = false;
			}
		}

		// Token: 0x06007C70 RID: 31856 RVA: 0x002994BC File Offset: 0x002976BC
		private void Update()
		{
			if (this.m_eCurrentTab == "TAB_SUPPLY")
			{
				this.updateTimer += Time.deltaTime;
				if (this.UPDATE_INTERVAL < this.updateTimer)
				{
					this.updateTimer = 0f;
					if (NKCSynchronizedTime.IsFinished(this.m_NextSupplyUpdateTick))
					{
						this.TrySupplyRefresh(false);
						return;
					}
					this.UpdateRefreshTimer(this.m_NextSupplyUpdateTick);
					return;
				}
			}
			else if (this.m_CurrentChainIndex > 0)
			{
				this.updateTimer += Time.deltaTime;
				if (this.UPDATE_INTERVAL < this.updateTimer)
				{
					this.updateTimer = 0f;
					if (NKCSynchronizedTime.IsFinished(this.m_NextChainUpdateTick))
					{
						this.TryChainRefresh();
						return;
					}
					this.UpdateChainRefreshTimer(this.m_NextChainUpdateTick);
					return;
				}
			}
			else if (this.m_bUseTabEndTimer)
			{
				this.updateTimer += Time.deltaTime;
				if (this.UPDATE_INTERVAL < this.updateTimer)
				{
					this.updateTimer = 0f;
					this.UpdateTabEndTimer(this.m_TabEndTick);
					if (NKCSynchronizedTime.IsFinished(this.m_TabEndTick))
					{
						this.m_bUseTabEndTimer = false;
						this.RefreshCurrentTab();
					}
				}
			}
		}

		// Token: 0x06007C71 RID: 31857 RVA: 0x002995DC File Offset: 0x002977DC
		private void UpdateRefreshTimer(long endTick)
		{
			this.m_lbSupplyTimeLeft.text = string.Format(NKCUtilString.GET_STRING_SHOP_NEXT_REFRESH_ONE_PARAM, NKCSynchronizedTime.GetTimeLeftString(endTick));
		}

		// Token: 0x06007C72 RID: 31858 RVA: 0x002995FC File Offset: 0x002977FC
		private void UpdateChainRefreshTimer(long endTick)
		{
			string arg = "<color=#FFDF5D>" + NKCUtilString.GetRemainTimeString(new DateTime(endTick), 3) + "</color>";
			this.m_lbTabRemainTime.text = string.Format(NKCUtilString.GET_STRING_SHOP_CHAIN_NEXT_RESET_ONE_PARAM, arg);
		}

		// Token: 0x06007C73 RID: 31859 RVA: 0x0029963C File Offset: 0x0029783C
		private void UpdateTabEndTimer(long endTick)
		{
			string arg = string.Empty;
			if (NKCSynchronizedTime.GetTimeLeft(endTick).Days < 1)
			{
				arg = "<color=#FF0000>" + NKCUtilString.GetRemainTimeString(new DateTime(endTick), 2) + "</color>";
			}
			else
			{
				arg = NKCUtilString.GetRemainTimeString(new DateTime(endTick), 2);
			}
			this.m_lbTabRemainTime.text = string.Format(NKCUtilString.GET_STRING_SHOP_CHAIN_NEXT_RESET_ONE_PARAM_CLOSE, arg);
		}

		// Token: 0x06007C74 RID: 31860 RVA: 0x002996A4 File Offset: 0x002978A4
		protected void BuildProductList(bool bForce)
		{
			if (!bForce && this.ShopItemUpdateTimeStamp >= NKCShopManager.ShopItemUpdatedTimestamp)
			{
				return;
			}
			if (NKCShopManager.ShopItemList == null)
			{
				return;
			}
			if (NKCScenManager.CurrentUserData() == null)
			{
				return;
			}
			this.m_dicProducts = new Dictionary<ShopTabTemplet, List<ShopItemTemplet>>();
			foreach (int productId in NKCShopManager.ShopItemList)
			{
				ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(productId);
				if (shopItemTemplet == null)
				{
					Debug.LogError("Product Templet null! ID : " + productId.ToString());
				}
				else if (NKCShopManager.CanExhibitItem(shopItemTemplet, true, false))
				{
					ShopTabTemplet shopTabTemplet = ShopTabTemplet.Find(shopItemTemplet.m_TabID, shopItemTemplet.m_TabSubIndex);
					if (shopTabTemplet != null)
					{
						if (this.m_dicProducts.ContainsKey(shopTabTemplet))
						{
							this.m_dicProducts[shopTabTemplet].Add(shopItemTemplet);
						}
						else
						{
							List<ShopItemTemplet> list = new List<ShopItemTemplet>();
							list.Add(shopItemTemplet);
							this.m_dicProducts.Add(shopTabTemplet, list);
						}
					}
				}
			}
			this.ShopItemUpdateTimeStamp = NKCShopManager.ShopItemUpdatedTimestamp;
		}

		// Token: 0x06007C75 RID: 31861 RVA: 0x002997AC File Offset: 0x002979AC
		public void ForceUpdateItemList()
		{
			this.BuildProductList(true);
			this.SelectTab(this.m_eCurrentTab, this.m_CurrentSubIndex, true, true);
		}

		// Token: 0x06007C76 RID: 31862 RVA: 0x002997CC File Offset: 0x002979CC
		public void RefreshRandomShopItem(int slotIndex)
		{
			if (this.m_eCurrentTab != "TAB_SUPPLY")
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetMyUserData().m_ShopData.randomShop == null)
			{
				this.SelectTab("TAB_SUPPLY", 0, true, true);
				return;
			}
			LoopScrollRect loopScrollRect = this.GetLoopScrollRect(this.GetDisplayType(this.m_eCurrentTab, this.m_CurrentSubIndex));
			if (loopScrollRect == null)
			{
				return;
			}
			loopScrollRect.RefreshCells(false);
		}

		// Token: 0x06007C77 RID: 31863 RVA: 0x00299834 File Offset: 0x00297A34
		public void RefreshShopItem(int shop_id)
		{
			ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(shop_id);
			if (shopItemTemplet == null)
			{
				return;
			}
			string eCurrentTab = this.m_eCurrentTab;
			if (eCurrentTab != null && eCurrentTab == "TAB_SUPPLY")
			{
				return;
			}
			ShopDisplayType displayType = this.GetDisplayType(this.m_eCurrentTab, this.m_CurrentSubIndex);
			if (displayType == ShopDisplayType.Custom)
			{
				this.ForceUpdateItemList();
				return;
			}
			if (shopItemTemplet.m_PriceItemID == 0)
			{
				this.ForceUpdateItemList();
				return;
			}
			if (NKCShopManager.GetBuyCountLeft(shopItemTemplet.m_ProductID) == 0 && NKCShopManager.HasLinkedItem(shopItemTemplet.m_ProductID))
			{
				this.ForceUpdateItemList();
				return;
			}
			ShopTabTemplet shopTabTemplet = ShopTabTemplet.Find(this.m_eCurrentTab, this.m_CurrentSubIndex);
			if (shopTabTemplet != null)
			{
				if (shopTabTemplet.IsChainTab)
				{
					if (NKCShopManager.GetCurrentTargetChainIndex(ShopTabTemplet.Find(this.m_eCurrentTab, this.m_CurrentSubIndex)) != this.m_CurrentChainIndex)
					{
						this.SelectTab(this.m_eCurrentTab, this.m_CurrentSubIndex, true, true);
						return;
					}
				}
				else if (shopTabTemplet.IsBundleTab)
				{
					this.UpdateBuyAllBtn(shopTabTemplet);
				}
			}
			List<NKCUIShopSlotBase> slotList = this.GetSlotList(displayType);
			if (slotList != null)
			{
				foreach (NKCUIShopSlotBase nkcuishopSlotBase in slotList)
				{
					if (nkcuishopSlotBase.gameObject.activeSelf && nkcuishopSlotBase.ProductID == shop_id && !nkcuishopSlotBase.SetData(this, shopItemTemplet, NKCShopManager.GetBuyCountLeft(nkcuishopSlotBase.ProductID), NKCUIShop.IsFirstBuy(nkcuishopSlotBase.ProductID)))
					{
						NKCUtil.SetGameobjectActive(nkcuishopSlotBase, false);
					}
				}
			}
		}

		// Token: 0x06007C78 RID: 31864 RVA: 0x002999A0 File Offset: 0x00297BA0
		public void RefreshShopRedDot()
		{
			foreach (KeyValuePair<string, NKCUIShopTab> keyValuePair in this.m_dicTab)
			{
				keyValuePair.Value.SetRedDot();
			}
		}

		// Token: 0x06007C79 RID: 31865 RVA: 0x002999F8 File Offset: 0x00297BF8
		public void RefreshHomePackageBanner()
		{
			this.m_lstHomeBannerTemplet = NKCShopManager.GetHomeBannerTemplet();
			if (this.m_HomePackageSlidePanel != null && this.m_lstHomeBannerTemplet.Count > 0)
			{
				this.m_HomePackageSlidePanel.TotalCount = this.m_lstHomeBannerTemplet.Count;
				this.m_HomePackageSlidePanel.SetIndex(0);
			}
		}

		// Token: 0x06007C7A RID: 31866 RVA: 0x00299A50 File Offset: 0x00297C50
		private void _SetSlotPoolSize(ref List<NKCUIShopSlotBase> lstSlot, NKCUIShopSlotBase pfbNewSlot, Transform parent, int count)
		{
			int num = count - lstSlot.Count;
			for (int i = 0; i < num; i++)
			{
				NKCUIShopSlotBase nkcuishopSlotBase = UnityEngine.Object.Instantiate<NKCUIShopSlotBase>(pfbNewSlot);
				nkcuishopSlotBase.Init(new NKCUIShopSlotBase.OnBuy(this.OnBtnProductBuy), new NKCUIShopSlotBase.OnRefreshRequired(this.ForceUpdateItemList));
				nkcuishopSlotBase.transform.SetParent(parent, false);
				nkcuishopSlotBase.transform.localPosition = Vector3.zero;
				lstSlot.Add(nkcuishopSlotBase);
				NKCUtil.SetGameobjectActive(nkcuishopSlotBase, false);
			}
		}

		// Token: 0x06007C7B RID: 31867 RVA: 0x00299AC8 File Offset: 0x00297CC8
		protected void BuildTabs(List<string> lstUseTab)
		{
			if (lstUseTab == null)
			{
				this.BuildTabs(null);
				return;
			}
			List<ShopTabTemplet> list = new List<ShopTabTemplet>();
			foreach (string tab in lstUseTab)
			{
				IEnumerable<ShopTabTemplet> allSubtabs = ShopTabTempletContainer.GetAllSubtabs(tab);
				if (allSubtabs != null)
				{
					foreach (ShopTabTemplet shopTabTemplet in allSubtabs)
					{
						if (shopTabTemplet != null)
						{
							list.Add(shopTabTemplet);
						}
					}
				}
			}
			this.BuildTabs(list);
		}

		// Token: 0x06007C7C RID: 31868 RVA: 0x00299B6C File Offset: 0x00297D6C
		protected void BuildTabs(IEnumerable<ShopTabTemplet> lstShopTabTemplet = null)
		{
			NKCUIShop.eTabMode eTabMode = this.m_eTabMode;
			if (eTabMode != NKCUIShop.eTabMode.Fold)
			{
				if (eTabMode == NKCUIShop.eTabMode.All)
				{
					this.BuildAllTabs(lstShopTabTemplet);
					return;
				}
			}
			else
			{
				this.BuildFoldTabs(lstShopTabTemplet);
			}
		}

		// Token: 0x06007C7D RID: 31869 RVA: 0x00299B98 File Offset: 0x00297D98
		protected void CleanupTab()
		{
			foreach (KeyValuePair<string, NKCUIShopTab> keyValuePair in this.m_dicTab)
			{
				keyValuePair.Value.Clear();
				UnityEngine.Object.Destroy(keyValuePair.Value.gameObject);
			}
			this.m_dicTab.Clear();
		}

		// Token: 0x06007C7E RID: 31870 RVA: 0x00299C0C File Offset: 0x00297E0C
		protected void BuildFoldTabs(IEnumerable<ShopTabTemplet> lstShopTabTemplet = null)
		{
			if (lstShopTabTemplet == null)
			{
				lstShopTabTemplet = ShopTabTemplet.Values;
			}
			this.m_lstEnabledTabs.Clear();
			Debug.Log("Shop Fold Tab Building");
			foreach (ShopTabTemplet shopTabTemplet in lstShopTabTemplet)
			{
				if (NKCSynchronizedTime.IsEventTime(shopTabTemplet.EventDateStartUtc, shopTabTemplet.EventDateEndUtc) && (!this.UseTabVisible || shopTabTemplet.m_Visible) && shopTabTemplet.EnableByTag)
				{
					if (this.GetUIShopTab(shopTabTemplet) == null)
					{
						if (shopTabTemplet.SubIndex == 0)
						{
							if (shopTabTemplet.m_ShopDisplay != ShopDisplayType.None || NKCShopManager.CanDisplayTab(shopTabTemplet.TabType, this.UseTabVisible))
							{
								NKCUIShopTab nkcuishopTab = UnityEngine.Object.Instantiate<NKCUIShopTab>(this.m_pfbTab);
								nkcuishopTab.SetData(shopTabTemplet, new NKCUIShopTab.OnTabSelected(this.onSelectTab), this.m_tgTabGroup);
								nkcuishopTab.transform.SetParent(this.m_trTabRoot, false);
								nkcuishopTab.transform.localPosition = Vector3.zero;
								this.m_dicTab.Add(this.MakeTabUIKey(shopTabTemplet.TabType, 0), nkcuishopTab);
							}
						}
						else
						{
							Log.Error("ShopTabTemplet : SubIndex must start with 0 - TabType : " + shopTabTemplet.TabType, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Shop/NKCUIShop.cs", 1249);
						}
					}
					else
					{
						NKCUIShopTabSlot nkcuishopTabSlot = UnityEngine.Object.Instantiate<NKCUIShopTabSlot>(this.m_pfbTabSubSlot);
						nkcuishopTabSlot.SetData(shopTabTemplet, new NKCUIShopTabSlot.OnClicked(this.onSelectTab));
						nkcuishopTabSlot.transform.SetParent(this.m_trTabRoot, false);
						nkcuishopTabSlot.transform.localPosition = Vector3.zero;
						this.GetUIShopTab(shopTabTemplet).AddSubSlot(nkcuishopTabSlot);
					}
				}
			}
			foreach (ShopTabTemplet shopTabTemplet2 in lstShopTabTemplet)
			{
				string tabType = shopTabTemplet2.TabType;
				NKCUIShopTab nkcuishopTab2;
				if (this.m_dicTab.TryGetValue(this.MakeTabUIKey(tabType, 0), out nkcuishopTab2))
				{
					if (shopTabTemplet2.m_HideWhenSoldOut)
					{
						if (shopTabTemplet2.SubIndex == 0 && nkcuishopTab2.HideTabRequired())
						{
							NKCUtil.SetGameobjectActive(nkcuishopTab2, false);
							continue;
						}
						if (!string.IsNullOrEmpty(shopTabTemplet2.m_PackageGroupID))
						{
							if (nkcuishopTab2.HideTabRequired())
							{
								NKCUtil.SetGameobjectActive(nkcuishopTab2, false);
								continue;
							}
						}
						else
						{
							if (!this.m_dicProducts.ContainsKey(shopTabTemplet2))
							{
								NKCUtil.SetGameobjectActive(nkcuishopTab2.GetSubSlotObject(shopTabTemplet2.SubIndex), false);
								continue;
							}
							GameObject subSlotObject = nkcuishopTab2.GetSubSlotObject(shopTabTemplet2.SubIndex);
							NKCUIShopTabSlot nkcuishopTabSlot2 = (subSlotObject != null) ? subSlotObject.GetComponent<NKCUIShopTabSlot>() : null;
							if (nkcuishopTabSlot2 != null && nkcuishopTabSlot2.HideTabRequired())
							{
								NKCUtil.SetGameobjectActive(nkcuishopTab2.GetSubSlotObject(shopTabTemplet2.SubIndex), false);
								continue;
							}
						}
					}
					if (!NKCSynchronizedTime.IsEventTime(shopTabTemplet2.EventDateStartUtc, shopTabTemplet2.EventDateEndUtc))
					{
						NKCUtil.SetGameobjectActive(nkcuishopTab2.GetSubSlotObject(shopTabTemplet2.SubIndex), false);
					}
					else if (this.UseTabVisible && !shopTabTemplet2.m_Visible)
					{
						NKCUtil.SetGameobjectActive(nkcuishopTab2.GetSubSlotObject(shopTabTemplet2.SubIndex), false);
					}
					else if (!shopTabTemplet2.EnableByTag)
					{
						NKCUtil.SetGameobjectActive(nkcuishopTab2.GetSubSlotObject(shopTabTemplet2.SubIndex), false);
					}
					else
					{
						if (shopTabTemplet2.m_ShopDisplay != ShopDisplayType.None)
						{
							this.m_lstEnabledTabs.Add(shopTabTemplet2);
						}
						NKCUtil.SetGameobjectActive(nkcuishopTab2, true);
					}
				}
			}
			this.RefreshShopRedDot();
		}

		// Token: 0x06007C7F RID: 31871 RVA: 0x00299F88 File Offset: 0x00298188
		protected void BuildAllTabs(IEnumerable<ShopTabTemplet> lstShopTabTemplet = null)
		{
			if (lstShopTabTemplet == null)
			{
				lstShopTabTemplet = ShopTabTemplet.Values;
			}
			this.m_lstEnabledTabs.Clear();
			Debug.Log("Shop All Tab Building");
			foreach (ShopTabTemplet shopTabTemplet in lstShopTabTemplet)
			{
				if (NKCSynchronizedTime.IsEventTime(shopTabTemplet.EventDateStartUtc, shopTabTemplet.EventDateEndUtc) && (!this.UseTabVisible || shopTabTemplet.m_Visible) && shopTabTemplet.EnableByTag && this.GetUIShopTab(shopTabTemplet) == null)
				{
					NKCUIShopTab nkcuishopTab = UnityEngine.Object.Instantiate<NKCUIShopTab>(this.m_pfbTab);
					nkcuishopTab.SetData(shopTabTemplet, new NKCUIShopTab.OnTabSelected(this.onSelectTab), this.m_tgTabGroup);
					nkcuishopTab.transform.SetParent(this.m_trTabRoot, false);
					nkcuishopTab.transform.localPosition = Vector3.zero;
					this.m_dicTab.Add(this.MakeTabUIKey(shopTabTemplet), nkcuishopTab);
				}
			}
			foreach (ShopTabTemplet shopTabTemplet2 in lstShopTabTemplet)
			{
				string text = this.MakeTabUIKey(shopTabTemplet2);
				NKCUIShopTab targetMono;
				if (text != null && this.m_dicTab.TryGetValue(text, out targetMono))
				{
					int count = NKCShopManager.GetItemTempletListByTab(shopTabTemplet2, true).Count;
					if (shopTabTemplet2.SubIndex == 0 && count == 0)
					{
						IEnumerable<ShopTabTemplet> allSubtabs = ShopTabTempletContainer.GetAllSubtabs(shopTabTemplet2.TabType);
						if (allSubtabs != null)
						{
							bool flag = false;
							using (IEnumerator<ShopTabTemplet> enumerator2 = allSubtabs.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									if (enumerator2.Current.SubIndex != 0)
									{
										flag = true;
										break;
									}
								}
							}
							if (flag)
							{
								NKCUtil.SetGameobjectActive(targetMono, false);
								continue;
							}
						}
					}
					if (shopTabTemplet2.m_HideWhenSoldOut)
					{
						if (!this.m_dicProducts.ContainsKey(shopTabTemplet2))
						{
							NKCUtil.SetGameobjectActive(targetMono, false);
							continue;
						}
						if (this.m_dicProducts[shopTabTemplet2].Count == 0)
						{
							NKCUtil.SetGameobjectActive(targetMono, false);
							continue;
						}
						if (count == 0)
						{
							NKCUtil.SetGameobjectActive(targetMono, false);
							continue;
						}
						if (NKCShopManager.IsTabSoldOut(shopTabTemplet2) && !NKCUtil.IsUsingSuperUserFunction())
						{
							NKCUtil.SetGameobjectActive(targetMono, false);
							continue;
						}
					}
					if (!NKCSynchronizedTime.IsEventTime(shopTabTemplet2.EventDateStartUtc, shopTabTemplet2.EventDateEndUtc))
					{
						NKCUtil.SetGameobjectActive(targetMono, false);
					}
					else if (this.UseTabVisible && !shopTabTemplet2.m_Visible)
					{
						NKCUtil.SetGameobjectActive(targetMono, false);
					}
					else if (!shopTabTemplet2.EnableByTag)
					{
						NKCUtil.SetGameobjectActive(targetMono, false);
					}
					else
					{
						this.m_lstEnabledTabs.Add(shopTabTemplet2);
						NKCUtil.SetGameobjectActive(targetMono, true);
					}
				}
			}
			this.RefreshShopRedDot();
		}

		// Token: 0x06007C80 RID: 31872 RVA: 0x0029A250 File Offset: 0x00298450
		protected void onSelectTab(string targetTab, int subIndex = 0)
		{
			this.SelectTab(targetTab, subIndex, false, true);
			if (targetTab != "TAB_MAIN")
			{
				NKCUIManager.NKCUIOverlayCaption.CloseAllCaption();
			}
		}

		// Token: 0x06007C81 RID: 31873 RVA: 0x0029A273 File Offset: 0x00298473
		public void RefreshCurrentTab()
		{
			this.SelectTab(this.m_eCurrentTab, this.m_CurrentSubIndex, true, true);
		}

		// Token: 0x06007C82 RID: 31874 RVA: 0x0029A28C File Offset: 0x0029848C
		public void SelectTab(string targetTab, int targetSubTabIndex = 0, bool bForce = false, bool bAnimate = true)
		{
			ShopTabTemplet shopTabTemplet = ShopTabTemplet.Find(targetTab, targetSubTabIndex);
			if (shopTabTemplet == null)
			{
				Debug.LogError("ShopTemplet for " + targetTab + " not exist. fallback to main tab!");
				targetTab = "TAB_MAIN";
				shopTabTemplet = ShopTabTemplet.Find("TAB_MAIN", 0);
				if (shopTabTemplet == null)
				{
					Debug.LogError("MainTab is null - tabType : TAB_MAIN, tabIndex = 0");
					return;
				}
			}
			NKCUIShopSkinPopup.CheckInstanceAndClose();
			if ((targetTab != "TAB_MAIN" && shopTabTemplet.m_ShopDisplay == ShopDisplayType.None) || (shopTabTemplet.HasDateLimit && !NKCSynchronizedTime.IsEventTime(shopTabTemplet.EventDateStartUtc, shopTabTemplet.EventDateEndUtc)))
			{
				bool flag = false;
				int num = int.MaxValue;
				foreach (ShopTabTemplet shopTabTemplet2 in ShopTabTempletContainer.Values)
				{
					if (shopTabTemplet2.TabType == targetTab)
					{
						NKCUIShopTab uishopTab = this.GetUIShopTab(shopTabTemplet);
						if (!(uishopTab == null) && (shopTabTemplet2.SubIndex != 0 || shopTabTemplet.m_ShopDisplay != ShopDisplayType.None))
						{
							if (this.m_eTabMode == NKCUIShop.eTabMode.Fold)
							{
								if (shopTabTemplet2.HasDateLimit && !NKCSynchronizedTime.IsEventTime(shopTabTemplet2.EventDateStartUtc, shopTabTemplet2.EventDateEndUtc))
								{
									NKCUtil.SetGameobjectActive(uishopTab.GetSubSlotObject(shopTabTemplet2.SubIndex), false);
									continue;
								}
								if (shopTabTemplet2.m_HideWhenSoldOut)
								{
									GameObject subSlotObject = uishopTab.GetSubSlotObject(shopTabTemplet2.SubIndex);
									NKCUIShopTabSlot nkcuishopTabSlot = (subSlotObject != null) ? uishopTab.GetSubSlotObject(shopTabTemplet2.SubIndex).GetComponent<NKCUIShopTabSlot>() : null;
									if (nkcuishopTabSlot != null && nkcuishopTabSlot.HideTabRequired())
									{
										NKCUtil.SetGameobjectActive(subSlotObject, false);
										continue;
									}
								}
							}
							if (shopTabTemplet2.SubIndex < num)
							{
								num = shopTabTemplet2.SubIndex;
							}
							flag = true;
						}
					}
				}
				if (!flag)
				{
					NKCUtil.SetGameobjectActive(this.GetUIShopTab(shopTabTemplet), false);
					NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NKE_FAIL_SHOP_NOT_EVENT_TIME, delegate()
					{
						this.SelectTab("TAB_MAIN", 0, false, true);
					}, "");
					return;
				}
				this.SelectTab(targetTab, num, bForce, bAnimate);
				return;
			}
			else
			{
				if (targetTab == "TAB_SUPPLY")
				{
					NKMShopRandomData randomShop = NKCScenManager.GetScenManager().GetMyUserData().m_ShopData.randomShop;
					if (randomShop == null || randomShop.datas.Count == 0 || NKCSynchronizedTime.IsFinished(randomShop.nextRefreshDate))
					{
						this.TrySupplyRefresh(false);
						return;
					}
					this.m_NextSupplyUpdateTick = randomShop.nextRefreshDate;
					NKCUtil.SetGameobjectActive(this.m_lbSupplyTimeLeft, true);
					NKCUtil.SetGameobjectActive(this.m_cbtnSupplyRefresh, true);
					NKCUtil.SetGameobjectActive(this.m_lbSupplyCountLeft, true);
					NKCUtil.SetLabelText(this.m_lbSupplyCountLeft, string.Format(NKCUtilString.GET_STRING_SHOP_REMAIN_NUMBER_TWO_PARAM, randomShop.refreshCount, 5));
					this.UpdateRefreshTimer(randomShop.nextRefreshDate);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lbSupplyTimeLeft, false);
					NKCUtil.SetGameobjectActive(this.m_cbtnSupplyRefresh, false);
					NKCUtil.SetGameobjectActive(this.m_lbSupplyCountLeft, false);
				}
				if (!bForce && this.m_eCurrentTab == targetTab && this.m_CurrentSubIndex == targetSubTabIndex)
				{
					return;
				}
				if (this.m_eCurrentTab != targetTab || this.m_CurrentSubIndex != targetSubTabIndex)
				{
					NKCShopManager.SetLastCheckedUTCTime(this.m_eCurrentTab, this.m_CurrentSubIndex);
				}
				this.m_eCurrentTab = targetTab;
				this.m_CurrentSubIndex = targetSubTabIndex;
				this.m_eDisplayType = shopTabTemplet.m_ShopDisplay;
				if (this.GetDisplayType(targetTab, targetSubTabIndex) == ShopDisplayType.Main)
				{
					this.RefreshHomePackageBanner();
				}
				NKCUtil.SetGameobjectActive(this.m_objTabResetTime, shopTabTemplet.IsCountResetType || shopTabTemplet.HasDateLimit);
				NKCUtil.SetGameobjectActive(this.m_objChainTab, shopTabTemplet.IsChainTab);
				if (shopTabTemplet.IsChainTab)
				{
					NKMShopData shopData = NKCScenManager.CurrentUserData().m_ShopData;
					long val = shopTabTemplet.HasDateLimit ? shopTabTemplet.EventDateEndUtc.Ticks : DateTime.MaxValue.Ticks;
					long ticks = shopData.GetChainTabResetTime(targetTab, targetSubTabIndex).Ticks;
					this.m_NextChainUpdateTick = Math.Min(val, ticks);
					if (this.m_NextChainUpdateTick < NKCSynchronizedTime.GetServerUTCTime(0.0).Ticks)
					{
						this.TryChainRefresh();
						return;
					}
					if (this.m_NextChainUpdateTick == 0L)
					{
						this.m_NextChainUpdateTick = DateTime.MaxValue.Ticks;
					}
					this.m_CurrentChainIndex = NKCShopManager.GetCurrentTargetChainIndex(shopTabTemplet);
					switch (this.m_CurrentChainIndex)
					{
					case 1:
						this.m_tglChain_01.Select(true, true, true);
						break;
					case 2:
						this.m_tglChain_02.Select(true, true, true);
						break;
					case 3:
						this.m_tglChain_03.Select(true, true, true);
						break;
					}
					this.UpdateChainRefreshTimer(this.m_NextChainUpdateTick);
					foreach (NKCUIShop.DisplaySet displaySet in this.GetAllDisplaySet())
					{
						LoopScrollRect scrollRect = displaySet.scrollRect;
						RectTransform rectTransform = (scrollRect != null) ? scrollRect.GetComponent<RectTransform>() : null;
						if (rectTransform != null)
						{
							rectTransform.GetComponent<RectTransform>().offsetMin = new Vector2(this.m_fChainTabOffsetX, rectTransform.offsetMin.y);
						}
					}
					this.m_bUseTabEndTimer = false;
					this.m_TabEndTick = 0L;
				}
				else
				{
					this.m_CurrentChainIndex = 0;
					foreach (NKCUIShop.DisplaySet displaySet2 in this.GetAllDisplaySet())
					{
						LoopScrollRect scrollRect2 = displaySet2.scrollRect;
						RectTransform rectTransform2 = (scrollRect2 != null) ? scrollRect2.GetComponent<RectTransform>() : null;
						if (rectTransform2 != null)
						{
							rectTransform2.GetComponent<RectTransform>().offsetMin = new Vector2(0f, rectTransform2.offsetMin.y);
						}
					}
					this.m_bUseTabEndTimer = shopTabTemplet.HasDateLimit;
					if (this.m_bUseTabEndTimer)
					{
						this.m_TabEndTick = shopTabTemplet.EventDateEndUtc.Ticks;
						this.UpdateTabEndTimer(shopTabTemplet.EventDateEndUtc.Ticks);
					}
					else
					{
						this.m_TabEndTick = 0L;
					}
				}
				this.ShowItemList(targetTab, targetSubTabIndex, false);
				NKCUtil.SetGameobjectActive(this.m_objTopBanner, !string.IsNullOrEmpty(shopTabTemplet.m_TopBannerText));
				if (!string.IsNullOrEmpty(shopTabTemplet.m_TopBannerText))
				{
					NKCUtil.SetLabelText(this.m_lbTopBanner, NKCStringTable.GetString(shopTabTemplet.m_TopBannerText, false));
				}
				this.UpdateBuyAllBtn(shopTabTemplet);
				this.RESOURCE_LIST = this.SetResourceList(shopTabTemplet);
				NKCUIManager.UpdateUpsideMenu();
				if (this.m_eCurrentTab != targetTab)
				{
					NKCUINPCSpine uinpcshop = this.m_UINPCShop;
					if (uinpcshop != null)
					{
						uinpcshop.PlayAni(this.GetNPCActionType(targetTab), false);
					}
				}
				NKCUIShop.eTabMode eTabMode = this.m_eTabMode;
				if (eTabMode != NKCUIShop.eTabMode.Fold)
				{
					if (eTabMode != NKCUIShop.eTabMode.All)
					{
						goto IL_6D7;
					}
				}
				else
				{
					using (Dictionary<string, NKCUIShopTab>.Enumerator enumerator3 = this.m_dicTab.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							KeyValuePair<string, NKCUIShopTab> keyValuePair = enumerator3.Current;
							keyValuePair.Value.SelectSubSlot(targetTab, targetSubTabIndex, bAnimate);
						}
						goto IL_6D7;
					}
				}
				foreach (KeyValuePair<string, NKCUIShopTab> keyValuePair2 in this.m_dicTab)
				{
					string text = this.MakeTabUIKey(targetTab, targetSubTabIndex);
					keyValuePair2.Value.m_ctglTab.Select(text != null && text == keyValuePair2.Key, true, false);
				}
				IL_6D7:
				if (!this.AlwaysShowNPC)
				{
					NKCUtil.SetGameobjectActive(this.m_UINPCShop, targetTab == "TAB_MAIN" || shopTabTemplet.m_ShopDisplay == ShopDisplayType.Item_Extend);
					NKCUtil.SetGameobjectActive(this.m_objNPCFront, targetTab == "TAB_MAIN");
				}
				if (shopTabTemplet.m_ShopDisplay == ShopDisplayType.Interior)
				{
					this.SetFilterButton();
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_csbtnFilter, false);
					NKCUtil.SetGameobjectActive(this.m_csbtnFilterActive, false);
				}
				ShopReddotType reddotType;
				int reddotCount = NKCShopManager.CheckTabReddotCount(out reddotType, "TAB_NONE", 0);
				NKCUtil.SetShopReddotImage(reddotType, this.m_objCategoryReddot, this.m_objReddot_RED, this.m_objReddot_YELLOW);
				NKCUtil.SetShopReddotLabel(reddotType, this.m_lbCategoryReddotCount, reddotCount);
				return;
			}
		}

		// Token: 0x06007C83 RID: 31875 RVA: 0x0029AA8C File Offset: 0x00298C8C
		private void ShowItemList(string targetTab, int targetSubTabIndex, bool bKeepSortedList = false)
		{
			ShopTabTemplet shopTabTemplet = ShopTabTemplet.Find(targetTab, targetSubTabIndex);
			if (shopTabTemplet == null)
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			LoopScrollRect loopScrollRect = this.GetLoopScrollRect(shopTabTemplet.m_ShopDisplay);
			NKCUIComDragSelectablePanel nkcuicomDragSelectablePanel = this.GetDragPanel(shopTabTemplet.m_ShopDisplay);
			if (this.m_dicFullDisplaySet.ContainsKey(shopTabTemplet.m_ShopDisplay) && nkcuicomDragSelectablePanel == null)
			{
				nkcuicomDragSelectablePanel = this.m_CommonFullDisplayDragSelectPanel;
				this.m_CommonFullDisplayDragSelectPanel.CleanUp();
				this.m_eCurrentCommonFulldisplaySet = shopTabTemplet.m_ShopDisplay;
				this.m_CommonFullDisplayDragSelectPanel.Prepare();
				NKCUtil.SetGameobjectActive(this.m_CommonFullDisplayDragSelectPanel, true);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_CommonFullDisplayDragSelectPanel, false);
			}
			foreach (NKCUIShop.DisplaySet displaySet in this.GetAllDisplaySet())
			{
				bool bValue = loopScrollRect != null && displaySet.scrollRect == loopScrollRect;
				NKCUtil.SetGameobjectActive(displaySet.scrollRect, bValue);
				NKCUtil.SetGameobjectActive(displaySet.m_rtParent, bValue);
			}
			foreach (NKCUIShop.FullDisplaySet fullDisplaySet in this.GetAllFullDisplaySet())
			{
				bool bValue2 = nkcuicomDragSelectablePanel != null && fullDisplaySet.draggablePanel == nkcuicomDragSelectablePanel;
				NKCUtil.SetGameobjectActive(fullDisplaySet.draggablePanel, bValue2);
				NKCUtil.SetGameobjectActive(fullDisplaySet.m_rtParent, bValue2);
			}
			this.m_lstFeatured.Clear();
			if (loopScrollRect != null || nkcuicomDragSelectablePanel != null)
			{
				if (targetTab == "TAB_SUPPLY")
				{
					NKMShopRandomData randomShop = NKCScenManager.GetScenManager().GetMyUserData().m_ShopData.randomShop;
					if (loopScrollRect != null)
					{
						loopScrollRect.TotalCount = randomShop.datas.Count;
						loopScrollRect.SetIndexPosition(0);
					}
					NKCUtil.SetGameobjectActive(this.m_objEmptyList, false);
				}
				else if (!string.IsNullOrEmpty(shopTabTemplet.m_PackageGroupID))
				{
					this.m_lstFeatured = NKCShopManager.GetFeaturedList(NKCScenManager.CurrentUserData(), shopTabTemplet.m_PackageGroupID, targetTab == "TAB_MAIN");
					loopScrollRect.TotalCount = this.m_lstFeatured.Count;
					loopScrollRect.SetIndexPosition(0);
					NKCUtil.SetGameobjectActive(this.m_objFeaturedEmpty, targetTab == "TAB_MAIN" && this.m_lstFeatured.Count == 0);
					NKCUtil.SetGameobjectActive(this.m_objEmptyList, false);
					NKCUtil.SetGameobjectActive(this.m_objChainLocked, false);
				}
				else
				{
					this.m_lstShopItem = this.GetSortedTabItemList(targetTab, targetSubTabIndex);
					if (shopTabTemplet.m_ShopDisplay == ShopDisplayType.Interior)
					{
						if (!bKeepSortedList)
						{
							this.m_ssProduct = new NKCShopProductSortSystem(nkmuserData, this.m_lstShopItem, this.InteriorSortOption());
						}
					}
					else
					{
						this.m_ssProduct = null;
					}
					int totalCount;
					if (this.m_ssProduct != null)
					{
						totalCount = this.m_ssProduct.SortedProductList.Count;
					}
					else if (shopTabTemplet.m_ShopDisplay == ShopDisplayType.Custom)
					{
						List<NKCShopCustomTabTemplet> list = NKCShopCustomTabTemplet.Find(shopTabTemplet.TabId);
						totalCount = ((list != null) ? list.Count : 0);
					}
					else
					{
						totalCount = this.m_lstShopItem.Count;
					}
					if (loopScrollRect != null)
					{
						loopScrollRect.TotalCount = totalCount;
						loopScrollRect.SetIndexPosition(0);
					}
					if (nkcuicomDragSelectablePanel != null)
					{
						nkcuicomDragSelectablePanel.TotalCount = totalCount;
						nkcuicomDragSelectablePanel.SetIndex(0);
					}
					ShopDisplayType eDisplayType = this.m_eDisplayType;
					if (eDisplayType - ShopDisplayType.None <= 1 || eDisplayType == ShopDisplayType.Custom)
					{
						NKCUtil.SetGameobjectActive(this.m_objEmptyList, false);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_objEmptyList, this.m_lstShopItem.Count == 0);
					}
				}
				NKCUtil.SetGameobjectActive(this.m_objChainLocked, shopTabTemplet.IsChainTab && this.m_CurrentChainIndex > NKCShopManager.GetCurrentTargetChainIndex(shopTabTemplet));
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objEmptyList, false);
			NKCUtil.SetGameobjectActive(this.m_objChainLocked, false);
		}

		// Token: 0x06007C84 RID: 31876 RVA: 0x0029AE48 File Offset: 0x00299048
		private List<int> SetResourceList(ShopTabTemplet tabTemplet)
		{
			return NKCShopManager.MakeShopTabResourceList(tabTemplet);
		}

		// Token: 0x06007C85 RID: 31877 RVA: 0x0029AE50 File Offset: 0x00299050
		public List<ShopItemTemplet> GetSortedTabItemList(string tabType, int subTab = 0)
		{
			ShopTabTemplet shopTabTemplet = ShopTabTemplet.Find(tabType, subTab);
			if (shopTabTemplet == null)
			{
				return new List<ShopItemTemplet>();
			}
			if (!this.m_dicProducts.ContainsKey(shopTabTemplet))
			{
				return new List<ShopItemTemplet>();
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return new List<ShopItemTemplet>();
			}
			List<ShopItemTemplet> list = this.m_dicProducts[shopTabTemplet];
			List<ShopItemTemplet> list2 = new List<ShopItemTemplet>();
			List<ShopItemTemplet> list3 = new List<ShopItemTemplet>();
			List<ShopItemTemplet> list4 = new List<ShopItemTemplet>();
			List<ShopItemTemplet> list5 = new List<ShopItemTemplet>();
			List<ShopItemTemplet> list6 = new List<ShopItemTemplet>();
			List<ShopItemTemplet> list7 = new List<ShopItemTemplet>();
			List<ShopItemTemplet> list8 = new List<ShopItemTemplet>();
			List<ShopItemTemplet> list9 = new List<ShopItemTemplet>();
			List<ShopItemTemplet> list10 = new List<ShopItemTemplet>();
			foreach (ShopItemTemplet shopItemTemplet in list)
			{
				if (shopItemTemplet.m_TabSubIndex == subTab && NKCShopManager.CanExhibitItem(shopItemTemplet, true, false) && (shopItemTemplet.m_ChainIndex <= 0 || shopItemTemplet.m_ChainIndex == this.m_CurrentChainIndex))
				{
					if (!NKMContentUnlockManager.IsContentUnlocked(nkmuserData, shopItemTemplet.m_UnlockInfo, false))
					{
						list9.Add(shopItemTemplet);
					}
					else if (NKCShopManager.GetBuyCountLeft(shopItemTemplet.m_ProductID) == 0)
					{
						list4.Add(shopItemTemplet);
					}
					else if (NKCShopManager.GetReddotType(shopItemTemplet) != ShopReddotType.NONE)
					{
						list10.Add(shopItemTemplet);
					}
					else if (shopItemTemplet.m_Price == 0)
					{
						list2.Add(shopItemTemplet);
					}
					else if (shopItemTemplet.IsReturningProduct)
					{
						list8.Add(shopItemTemplet);
					}
					else if (shopItemTemplet.HasDateLimit)
					{
						list3.Add(shopItemTemplet);
					}
					else if (this.IsOnPromotion(shopItemTemplet))
					{
						list5.Add(shopItemTemplet);
					}
					else if (shopItemTemplet.m_OrderList > 0)
					{
						list6.Add(shopItemTemplet);
					}
					else
					{
						list7.Add(shopItemTemplet);
					}
				}
			}
			list4.Sort(new Comparison<ShopItemTemplet>(this.CompByOrderList));
			list2.Sort(new Comparison<ShopItemTemplet>(this.CompByOrderList));
			list3.Sort(new Comparison<ShopItemTemplet>(this.CompByOrderList));
			list5.Sort(new Comparison<ShopItemTemplet>(this.CompByOrderList));
			list6.Sort(new Comparison<ShopItemTemplet>(this.CompByOrderList));
			list7.Sort(new Comparison<ShopItemTemplet>(this.CompByOrderList));
			list8.Sort(new Comparison<ShopItemTemplet>(this.CompByOrderList));
			list10.Sort(new Comparison<ShopItemTemplet>(this.CompByReddot));
			List<ShopItemTemplet> list11 = new List<ShopItemTemplet>();
			list11.AddRange(list10);
			list11.AddRange(list2);
			list11.AddRange(list8);
			list11.AddRange(list3);
			list11.AddRange(list5);
			list11.AddRange(list6);
			list11.AddRange(list7);
			list11.AddRange(list9);
			list11.AddRange(list4);
			return list11;
		}

		// Token: 0x06007C86 RID: 31878 RVA: 0x0029B0FC File Offset: 0x002992FC
		private int CompByReddot(ShopItemTemplet lItem, ShopItemTemplet rItem)
		{
			ShopReddotType reddotType = NKCShopManager.GetReddotType(lItem);
			ShopReddotType reddotType2 = NKCShopManager.GetReddotType(rItem);
			if (reddotType == reddotType2)
			{
				return this.CompByOrderList(lItem, rItem);
			}
			return reddotType2.CompareTo(reddotType);
		}

		// Token: 0x06007C87 RID: 31879 RVA: 0x0029B138 File Offset: 0x00299338
		private int CompByOrderList(ShopItemTemplet lItem, ShopItemTemplet rItem)
		{
			if (lItem.m_OrderList == rItem.m_OrderList)
			{
				return lItem.m_ItemID.CompareTo(rItem.m_ItemID);
			}
			if (rItem.m_OrderList == 0)
			{
				return -1;
			}
			if (lItem.m_OrderList == 0)
			{
				return 1;
			}
			return lItem.m_OrderList.CompareTo(rItem.m_OrderList);
		}

		// Token: 0x06007C88 RID: 31880 RVA: 0x0029B18C File Offset: 0x0029938C
		private bool IsOnPromotion(ShopItemTemplet productTemplet)
		{
			switch (productTemplet.m_TagImage)
			{
			case ShopItemRibbon.ONE_PLUS_ONE:
				return productTemplet.m_PurchaseEventType != PURCHASE_EVENT_REWARD_TYPE.FIRST_PURCHASE_CHANGE_REWARD_VALUE || NKCUIShop.IsFirstBuy(productTemplet.m_ProductID);
			case ShopItemRibbon.NEW:
			case ShopItemRibbon.LIMITED:
			case ShopItemRibbon.TIME_LIMITED:
			case ShopItemRibbon.POPULAR:
				return true;
			}
			return productTemplet.m_PurchaseEventType == PURCHASE_EVENT_REWARD_TYPE.FIRST_PURCHASE_CHANGE_REWARD_VALUE && NKCUIShop.IsFirstBuy(productTemplet.m_ProductID);
		}

		// Token: 0x06007C89 RID: 31881 RVA: 0x0029B200 File Offset: 0x00299400
		private ShopDisplayType GetDisplayType(string type, int subIndex)
		{
			ShopTabTemplet shopTabTemplet = ShopTabTemplet.Find(type, subIndex);
			if (shopTabTemplet == null)
			{
				Debug.LogError("Tab does not exist : " + type);
				return ShopDisplayType.Item;
			}
			return shopTabTemplet.m_ShopDisplay;
		}

		// Token: 0x06007C8A RID: 31882 RVA: 0x0029B230 File Offset: 0x00299430
		private void OnBtnSupplyRefresh()
		{
			NKMShopRandomData randomShop = NKCScenManager.GetScenManager().GetMyUserData().m_ShopData.randomShop;
			int leftCount = (randomShop != null) ? randomShop.refreshCount : 5;
			NKCPopupResourceConfirmBox.Instance.OpenWithLeftCount(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_SHOP_SUPPLY_LIST_INSTANTLY_REFRESH_REQ, 101, 15, leftCount, 5, delegate
			{
				this.TrySupplyRefresh(true);
			}, null);
		}

		// Token: 0x06007C8B RID: 31883 RVA: 0x0029B286 File Offset: 0x00299486
		private void TrySupplyRefresh(bool useCash)
		{
			NKCPopupItemBox.CheckInstanceAndClose();
			NKCScenManager.GetScenManager().Get_NKC_SCEN_SHOP().Send_NKMPacket_SHOP_REFRESH_REQ(useCash);
		}

		// Token: 0x06007C8C RID: 31884 RVA: 0x0029B29D File Offset: 0x0029949D
		private void TryChainRefresh()
		{
			NKCPopupItemBox.CheckInstanceAndClose();
			NKCScenManager.GetScenManager().Get_NKC_SCEN_SHOP().Send_NKMPacket_SHOP_CHAIN_TAB_RESET_TIME_REQ();
		}

		// Token: 0x06007C8D RID: 31885 RVA: 0x0029B2B4 File Offset: 0x002994B4
		private void OnBtnProductBuy(int ProductID)
		{
			NKM_ERROR_CODE nkm_ERROR_CODE = NKCShopManager.OnBtnProductBuy(ProductID, this.m_eCurrentTab == "TAB_SUPPLY");
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				if (nkm_ERROR_CODE == NKM_ERROR_CODE.NKE_FAIL_SHOP_INVALID_CHAIN_TAB)
				{
					NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_SHOP_CHAIN_LOCKED, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
					return;
				}
			}
			else if (this.m_eCurrentTab != "TAB_SUPPLY")
			{
				ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(ProductID);
				if (shopItemTemplet != null)
				{
					NKCUINPCSpine uinpcshop = this.m_UINPCShop;
					if (uinpcshop == null)
					{
						return;
					}
					uinpcshop.PlayAni(this.GetNPCActionType(shopItemTemplet.m_TagImage), false);
				}
			}
		}

		// Token: 0x06007C8E RID: 31886 RVA: 0x0029B33C File Offset: 0x0029953C
		private int GetRewardCountByItemID(int itemID)
		{
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(itemID);
			if (itemMiscTempletByID != null)
			{
				List<NKMRandomBoxItemTemplet> randomBoxItemTempletList = NKCRandomBoxManager.GetRandomBoxItemTempletList(itemMiscTempletByID.m_RewardGroupID);
				if (randomBoxItemTempletList != null)
				{
					return randomBoxItemTempletList.Count;
				}
			}
			return 0;
		}

		// Token: 0x06007C8F RID: 31887 RVA: 0x0029B36C File Offset: 0x0029956C
		private void OnBtnBanner(NKCShopBannerTemplet bannerTemplet)
		{
			if (bannerTemplet == null)
			{
				return;
			}
			if (!NKCSynchronizedTime.IsEventTime(bannerTemplet.m_DateStrID))
			{
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_DEACTIVATED_EVENT_POST, delegate()
				{
					this.SelectTab("TAB_MAIN", 0, true, true);
				}, "");
				return;
			}
			if (bannerTemplet.m_ProductID > 0)
			{
				bool flag;
				long num;
				if (NKCShopManager.CanBuyFixShop(NKCScenManager.CurrentUserData(), ShopItemTemplet.Find(bannerTemplet.m_ProductID), out flag, out num, true) != NKM_ERROR_CODE.NEC_OK)
				{
					return;
				}
				this.OnBtnProductBuy(bannerTemplet.m_ProductID);
				return;
			}
			else
			{
				NKCShopCategoryTemplet categoryFromTab = NKCShopManager.GetCategoryFromTab(bannerTemplet.m_TabID);
				if (categoryFromTab != null)
				{
					this.ChangeCategory(categoryFromTab.m_eCategory, bannerTemplet.m_TabID, bannerTemplet.m_TabSubIndex);
					return;
				}
				this.SelectTab(bannerTemplet.m_TabID, bannerTemplet.m_TabSubIndex, true, true);
				return;
			}
		}

		// Token: 0x06007C90 RID: 31888 RVA: 0x0029B418 File Offset: 0x00299618
		private void OnBtnBuyAll()
		{
			ShopTabTemplet tabTemplet = ShopTabTemplet.Find(this.m_eCurrentTab, this.m_CurrentSubIndex);
			if (tabTemplet != null)
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				int bundleItemPrice = NKCShopManager.GetBundleItemPrice(tabTemplet);
				int bundleItemPriceItemID = NKCShopManager.GetBundleItemPriceItemID(tabTemplet);
				if (nkmuserData != null && !nkmuserData.CheckPrice(bundleItemPrice, bundleItemPriceItemID))
				{
					NKCUIShop.OpenResourceAddPopup(bundleItemPriceItemID, bundleItemPrice);
					return;
				}
				if (bundleItemPrice == 0)
				{
					return;
				}
				NKCPopupResourceWithdraw.Instance.OpenForShopBuyAll(tabTemplet, delegate
				{
					this.TryBuyAll(tabTemplet);
				});
			}
		}

		// Token: 0x06007C91 RID: 31889 RVA: 0x0029B4A4 File Offset: 0x002996A4
		private void TryBuyAll(ShopTabTemplet tabTemplet)
		{
			List<ShopItemTemplet> itemTempletListByTab = NKCShopManager.GetItemTempletListByTab(tabTemplet, false);
			HashSet<int> hashSet = new HashSet<int>();
			for (int i = 0; i < itemTempletListByTab.Count; i++)
			{
				if (!hashSet.Contains(itemTempletListByTab[i].m_ProductID))
				{
					hashSet.Add(itemTempletListByTab[i].m_ProductID);
				}
				else
				{
					Log.Error(string.Format("번들탭에 동일한 상품 아이디가 존재함 - {0}", itemTempletListByTab[i].m_ProductID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Shop/NKCUIShop.cs", 2219);
				}
			}
			NKCShopManager.SetBundleItemIds(hashSet);
			for (int j = 0; j < itemTempletListByTab.Count; j++)
			{
				NKCPacketSender.Send_NKMPacket_SHOP_FIX_SHOP_BUY_REQ(itemTempletListByTab[j].m_ProductID, NKCShopManager.GetBuyCountLeft(itemTempletListByTab[j].m_ProductID), null);
			}
		}

		// Token: 0x06007C92 RID: 31890 RVA: 0x0029B55D File Offset: 0x0029975D
		private static void OpenResourceAddPopup(int priceItemID, int price)
		{
			NKCShopManager.OpenItemLackPopup(priceItemID, price);
		}

		// Token: 0x06007C93 RID: 31891 RVA: 0x0029B566 File Offset: 0x00299766
		public static bool IsFirstBuy(int ProductID)
		{
			return NKCShopManager.IsFirstBuy(ProductID, NKCScenManager.GetScenManager().GetMyUserData());
		}

		// Token: 0x06007C94 RID: 31892 RVA: 0x0029B578 File Offset: 0x00299778
		public void RandomShopItemUpdateComplete()
		{
			NKMShopRandomData randomShop = NKCScenManager.GetScenManager().GetMyUserData().m_ShopData.randomShop;
			if (randomShop == null || randomShop.datas.Count == 0 || NKCSynchronizedTime.IsFinished(randomShop.nextRefreshDate))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_SHOP_SUPPLY_LIST_GET_FAIL, null, "");
				this.SelectTab("TAB_MAIN", 0, true, true);
				return;
			}
			this.SelectTab("TAB_SUPPLY", 0, true, true);
		}

		// Token: 0x06007C95 RID: 31893 RVA: 0x0029B5EC File Offset: 0x002997EC
		public void ChainRefreshComplete(List<ShopChainTabNextResetData> lstChainResetData)
		{
			for (int i = 0; i < lstChainResetData.Count; i++)
			{
				if (lstChainResetData[i].tabType == this.m_eCurrentTab && lstChainResetData[i].subIndex == this.m_CurrentSubIndex)
				{
					this.SelectTab(this.m_eCurrentTab, this.m_CurrentSubIndex, true, true);
					return;
				}
			}
			this.SelectTab("TAB_MAIN", 0, true, true);
		}

		// Token: 0x06007C96 RID: 31894 RVA: 0x0029B65A File Offset: 0x0029985A
		private void OnChainTab_01(bool bSelect)
		{
			if (this.m_CurrentChainIndex == 1)
			{
				return;
			}
			if (bSelect)
			{
				this.m_CurrentChainIndex = 1;
				this.ShowItemList(this.m_eCurrentTab, this.m_CurrentSubIndex, false);
			}
		}

		// Token: 0x06007C97 RID: 31895 RVA: 0x0029B683 File Offset: 0x00299883
		private void OnChainTab_02(bool bSelect)
		{
			if (this.m_CurrentChainIndex == 2)
			{
				return;
			}
			if (bSelect)
			{
				this.m_CurrentChainIndex = 2;
				this.ShowItemList(this.m_eCurrentTab, this.m_CurrentSubIndex, false);
			}
		}

		// Token: 0x06007C98 RID: 31896 RVA: 0x0029B6AC File Offset: 0x002998AC
		private void OnChainTab_03(bool bSelect)
		{
			if (this.m_CurrentChainIndex == 3)
			{
				return;
			}
			if (bSelect)
			{
				this.m_CurrentChainIndex = 3;
				this.ShowItemList(this.m_eCurrentTab, this.m_CurrentSubIndex, false);
			}
		}

		// Token: 0x06007C99 RID: 31897 RVA: 0x0029B6D8 File Offset: 0x002998D8
		private void UpdateBuyAllBtn(ShopTabTemplet tabTemplet)
		{
			if (tabTemplet == null)
			{
				NKCUtil.SetGameobjectActive(this.m_btnBuyAll, false);
				return;
			}
			if (!tabTemplet.IsBundleTab)
			{
				NKCUtil.SetGameobjectActive(this.m_btnBuyAll, false);
				return;
			}
			int bundleItemPriceItemID = NKCShopManager.GetBundleItemPriceItemID(tabTemplet);
			int bundleItemPrice = NKCShopManager.GetBundleItemPrice(tabTemplet);
			if (NKCScenManager.CurrentUserData().CheckPrice(bundleItemPrice, bundleItemPriceItemID))
			{
				NKCUtil.SetGameobjectActive(this.m_btnBuyAll, true);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_btnBuyAll, false);
		}

		// Token: 0x06007C9A RID: 31898 RVA: 0x0029B740 File Offset: 0x00299940
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			ShopTabTemplet shopTabTemplet = ShopTabTemplet.Find(this.m_eCurrentTab, this.m_CurrentSubIndex);
			if (shopTabTemplet != null && shopTabTemplet.IsBundleTab)
			{
				this.UpdateBuyAllBtn(shopTabTemplet);
			}
		}

		// Token: 0x06007C9B RID: 31899 RVA: 0x0029B771 File Offset: 0x00299971
		public virtual void OnProductBuy(ShopItemTemplet productTemplet)
		{
			if (productTemplet != null && productTemplet.m_ItemType != NKM_REWARD_TYPE.RT_SKIN && this.m_UINPCShop != null)
			{
				this.m_UINPCShop.PlayAni(NPC_ACTION_TYPE.THANKS, false);
			}
		}

		// Token: 0x06007C9C RID: 31900 RVA: 0x0029B79A File Offset: 0x0029999A
		private void OnBtnJPNPaymentLaw()
		{
			NKCPublisherModule.InAppPurchase.OpenPaymentLaw(null);
		}

		// Token: 0x06007C9D RID: 31901 RVA: 0x0029B7A7 File Offset: 0x002999A7
		private void OnBtnJPNCommercialLaw()
		{
			NKCPublisherModule.InAppPurchase.OpenCommercialLaw(null);
		}

		// Token: 0x06007C9E RID: 31902 RVA: 0x0029B7B4 File Offset: 0x002999B4
		private void ProvideFullDisplayData(Transform tr, int idx)
		{
			ShopTabTemplet shopTabTemplet = ShopTabTemplet.Find(this.m_eCurrentTab, this.m_CurrentSubIndex);
			if (shopTabTemplet != null && shopTabTemplet.m_ShopDisplay == ShopDisplayType.Custom)
			{
				NKCUIShopSlotCustomPrefabAdapter component = tr.GetComponent<NKCUIShopSlotCustomPrefabAdapter>();
				if (component != null)
				{
					NKCShopCustomTabTemplet tabTemplet = NKCShopCustomTabTemplet.Find(shopTabTemplet.TabId, idx);
					component.SetData(this, tabTemplet, new NKCUIShopSlotBase.OnBuy(this.OnBtnProductBuy), new NKCUIShopSlotBase.OnRefreshRequired(this.ForceUpdateItemList));
					return;
				}
			}
			else
			{
				NKCUIShopSlotBase component2 = tr.GetComponent<NKCUIShopSlotBase>();
				if (component2 != null)
				{
					ShopItemTemplet shopItemTemplet = this.m_lstShopItem[idx];
					component2.SetData(this, shopItemTemplet, NKCShopManager.GetBuyCountLeft(shopItemTemplet.m_ProductID), NKCUIShop.IsFirstBuy(shopItemTemplet.m_ProductID));
				}
			}
		}

		// Token: 0x06007C9F RID: 31903 RVA: 0x0029B864 File Offset: 0x00299A64
		public NPC_ACTION_TYPE GetNPCActionType(string tabType)
		{
			if (tabType != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(tabType);
				if (num <= 1291786563U)
				{
					if (num <= 631607653U)
					{
						if (num != 144693732U)
						{
							if (num != 270047410U)
							{
								if (num == 631607653U)
								{
									if (tabType == "TAB_PVP")
									{
										return NPC_ACTION_TYPE.SELECT_TAB_PVP;
									}
								}
							}
							else if (tabType == "TAB_SUPPLY")
							{
								return NPC_ACTION_TYPE.SELECT_TAB_SUPPLY;
							}
						}
						else if (tabType == "TAB_SKIN")
						{
							return NPC_ACTION_TYPE.SELECT_TAB_SKIN;
						}
					}
					else if (num != 875505107U)
					{
						if (num != 926709398U)
						{
							if (num == 1291786563U)
							{
								if (tabType == "TAB_EVENT")
								{
									return NPC_ACTION_TYPE.SELECT_TAB_EVENT;
								}
							}
						}
						else if (tabType == "TAB_CASH")
						{
							return NPC_ACTION_TYPE.SELECT_TAB_CASH;
						}
					}
					else if (tabType == "TAB_HR")
					{
						return NPC_ACTION_TYPE.SELECT_TAB_HR;
					}
				}
				else if (num <= 2579508147U)
				{
					if (num != 1611994944U)
					{
						if (num != 2130044111U)
						{
							if (num == 2579508147U)
							{
								if (tabType == "TAB_DIVE")
								{
									return NPC_ACTION_TYPE.SELECT_TAB_DIVE;
								}
							}
						}
						else if (tabType == "TAB_COUPON")
						{
							return NPC_ACTION_TYPE.SELECT_TAB_COUPON;
						}
					}
					else if (tabType == "TAB_FACILITIES")
					{
						return NPC_ACTION_TYPE.SELECT_TAB_FACILITIES;
					}
				}
				else if (num != 3609906585U)
				{
					if (num != 3915787789U)
					{
						if (num == 4177446247U)
						{
							if (tabType == "TAB_PACKAGE")
							{
								return NPC_ACTION_TYPE.SELECT_TAB_PACKAGE;
							}
						}
					}
					else if (tabType == "TAB_FUNCTION")
					{
						return NPC_ACTION_TYPE.SELECT_TAB_FUNCTION;
					}
				}
				else if (tabType == "TAB_RESOURCE")
				{
					return NPC_ACTION_TYPE.SELECT_TAB_RESOURCE;
				}
			}
			return NPC_ACTION_TYPE.NONE;
		}

		// Token: 0x06007CA0 RID: 31904 RVA: 0x0029BA12 File Offset: 0x00299C12
		public NPC_ACTION_TYPE GetNPCActionType(ShopItemRibbon type)
		{
			if (type == ShopItemRibbon.ONE_PLUS_ONE)
			{
				return NPC_ACTION_TYPE.SELECT_GOODS_ONE_PLUS_ONE;
			}
			if (type == ShopItemRibbon.LIMITED)
			{
				return NPC_ACTION_TYPE.SELECT_GOODS_LIMITED;
			}
			if (type != ShopItemRibbon.POPULAR)
			{
				return NPC_ACTION_TYPE.NONE;
			}
			return NPC_ACTION_TYPE.SELECT_GOODS_POPULAR;
		}

		// Token: 0x06007CA1 RID: 31905 RVA: 0x0029BA2C File Offset: 0x00299C2C
		public void OnChangeCategory()
		{
			NKCUIShopCategoryChange uiShopCategoryChange = this.m_uiShopCategoryChange;
			if (uiShopCategoryChange == null)
			{
				return;
			}
			uiShopCategoryChange.Open();
		}

		// Token: 0x06007CA2 RID: 31906 RVA: 0x0029BA3E File Offset: 0x00299C3E
		private void _ChangeCategory(NKCShopManager.ShopTabCategory category)
		{
			this.ChangeCategory(category, "TAB_NONE", 0);
		}

		// Token: 0x06007CA3 RID: 31907 RVA: 0x0029BA4D File Offset: 0x00299C4D
		public void ChangeCategory(NKCShopManager.ShopTabCategory category, string selectedTab = "TAB_NONE", int subTabIndex = 0)
		{
			NKCUtil.SetGameobjectActive(this.m_uiShopCategoryChange, false);
			this.BuildTabs(category, selectedTab, subTabIndex, false);
		}

		// Token: 0x06007CA4 RID: 31908 RVA: 0x0029BA68 File Offset: 0x00299C68
		public override bool OnHotkey(HotkeyEventType hotkey)
		{
			switch (hotkey)
			{
			case HotkeyEventType.Up:
				return this.MoveTab(-1);
			case HotkeyEventType.Down:
				return this.MoveTab(1);
			case HotkeyEventType.PrevTab:
			{
				int num = 4;
				for (int i = 1; i < num; i++)
				{
					NKCShopManager.ShopTabCategory category = (NKCShopManager.ShopTabCategory)((this.m_eCategory + num - (NKCShopManager.ShopTabCategory)i) % num);
					if (NKCShopCategoryTemplet.Find(category) != null)
					{
						this._ChangeCategory(category);
						break;
					}
				}
				return true;
			}
			case HotkeyEventType.NextTab:
			{
				int num2 = 4;
				for (int j = 1; j < num2; j++)
				{
					NKCShopManager.ShopTabCategory category2 = (this.m_eCategory + j) % (NKCShopManager.ShopTabCategory)num2;
					if (NKCShopCategoryTemplet.Find(category2) != null)
					{
						this._ChangeCategory(category2);
						break;
					}
				}
				return true;
			}
			case HotkeyEventType.ShowHotkey:
				if (this.m_srTab != null)
				{
					NKCUIComHotkeyDisplay.OpenInstance(this.m_srTab.transform, new HotkeyEventType[]
					{
						HotkeyEventType.Up,
						HotkeyEventType.Down
					});
				}
				if (this.m_csbtnChangeCategory != null)
				{
					NKCUIComHotkeyDisplay.OpenInstance(this.m_csbtnChangeCategory.transform, HotkeyEventType.NextTab);
				}
				return false;
			}
			return false;
		}

		// Token: 0x06007CA5 RID: 31909 RVA: 0x0029BB64 File Offset: 0x00299D64
		private bool MoveTab(int moveCount)
		{
			int num = this.m_lstEnabledTabs.FindIndex((ShopTabTemplet x) => x.TabType == this.m_eCurrentTab && x.SubIndex == this.m_CurrentSubIndex);
			if (num >= 0)
			{
				int index = (num + moveCount + this.m_lstEnabledTabs.Count) % this.m_lstEnabledTabs.Count;
				ShopTabTemplet shopTabTemplet = this.m_lstEnabledTabs[index];
				this.SelectTab(shopTabTemplet.TabType, shopTabTemplet.SubIndex, false, true);
				return true;
			}
			return false;
		}

		// Token: 0x06007CA6 RID: 31910 RVA: 0x0029BBD0 File Offset: 0x00299DD0
		private void OnThemeSelected(int themeID)
		{
			if (this.m_ssProduct != null)
			{
				this.m_ssProduct.FilterStatType_ThemeID = themeID;
				this.m_ssProduct.FilterList(this.m_ssProduct.FilterSet);
			}
			this.SetFilterButton();
			this.ShowItemList(this.m_eCurrentTab, this.m_CurrentSubIndex, true);
		}

		// Token: 0x06007CA7 RID: 31911 RVA: 0x0029BC20 File Offset: 0x00299E20
		private void OnBtnFilter()
		{
			int currentSelectedThemeID = 0;
			if (this.m_ssProduct != null)
			{
				currentSelectedThemeID = this.m_ssProduct.FilterStatType_ThemeID;
			}
			NKCPopupFilterTheme.Instance.Open(new NKCPopupFilterTheme.OnSelectTheme(this.OnThemeSelected), currentSelectedThemeID);
		}

		// Token: 0x06007CA8 RID: 31912 RVA: 0x0029BC5C File Offset: 0x00299E5C
		private NKCShopProductSortSystem.ShopProductListOptions InteriorSortOption()
		{
			return new NKCShopProductSortSystem.ShopProductListOptions
			{
				setFilterOption = new HashSet<NKCShopProductSortSystem.eFilterOption>
				{
					NKCShopProductSortSystem.eFilterOption.Theme
				},
				lstSortOption = new List<NKCShopProductSortSystem.eSortOption>(),
				m_filterThemeID = 0
			};
		}

		// Token: 0x06007CA9 RID: 31913 RVA: 0x0029BC9C File Offset: 0x00299E9C
		private void SetFilterButton()
		{
			bool flag = this.m_ssProduct != null && this.m_ssProduct.FilterStatType_ThemeID != 0;
			NKCUtil.SetGameobjectActive(this.m_csbtnFilterActive, flag);
			NKCUtil.SetGameobjectActive(this.m_csbtnFilter, !flag);
		}

		// Token: 0x06007CAA RID: 31914 RVA: 0x0029BCDE File Offset: 0x00299EDE
		public bool TutorialCheck()
		{
			return NKCTutorialManager.TutorialRequired(TutorialPoint.Shop, true) > TutorialStep.None;
		}

		// Token: 0x0400691B RID: 26907
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_shop";

		// Token: 0x0400691C RID: 26908
		private const string UI_ASSET_NAME = "NKM_UI_SHOP";

		// Token: 0x0400691D RID: 26909
		private static NKCUIShop m_Instance;

		// Token: 0x0400691E RID: 26910
		private const string ASSET_BUNDLE_TOP_BANNER = "ab_ui_nkm_ui_shop_thumbnail";

		// Token: 0x0400691F RID: 26911
		private List<int> RESOURCE_LIST = new List<int>();

		// Token: 0x04006920 RID: 26912
		private Dictionary<ShopDisplayType, NKCUIShop.DisplaySet> m_dicLoopScrollSet = new Dictionary<ShopDisplayType, NKCUIShop.DisplaySet>();

		// Token: 0x04006921 RID: 26913
		private Dictionary<ShopDisplayType, NKCUIShop.FullDisplaySet> m_dicFullDisplaySet = new Dictionary<ShopDisplayType, NKCUIShop.FullDisplaySet>();

		// Token: 0x04006922 RID: 26914
		public float m_fChainTabOffsetX = 135f;

		// Token: 0x04006923 RID: 26915
		public bool m_bUseSlideBanner;

		// Token: 0x04006924 RID: 26916
		[Header("슬롯/탭 프리팹")]
		public NKCUIShopTab m_pfbTab;

		// Token: 0x04006925 RID: 26917
		public NKCUIShopTabSlot m_pfbTabSubSlot;

		// Token: 0x04006926 RID: 26918
		[Header("좌측 탭 메뉴")]
		public ScrollRect m_srTab;

		// Token: 0x04006927 RID: 26919
		public Transform m_trTabRoot;

		// Token: 0x04006928 RID: 26920
		public NKCUIComToggleGroup m_tgTabGroup;

		// Token: 0x04006929 RID: 26921
		[Header("상단 배너")]
		public GameObject m_objTopBanner;

		// Token: 0x0400692A RID: 26922
		public Text m_lbTopBanner;

		// Token: 0x0400692B RID: 26923
		public NKCUIComStateButton m_btnBuyAll;

		// Token: 0x0400692C RID: 26924
		[Header("우측 상품 스크롤뷰")]
		public List<NKCUIShop.DisplaySet> m_lstDisplaySet;

		// Token: 0x0400692D RID: 26925
		public List<NKCUIShop.FullDisplaySet> m_lstFullDisplaySet;

		// Token: 0x0400692E RID: 26926
		public NKCUIComDragSelectablePanel m_CommonFullDisplayDragSelectPanel;

		// Token: 0x0400692F RID: 26927
		public Transform m_ReturnedSlotParent;

		// Token: 0x04006930 RID: 26928
		private ShopDisplayType m_eCurrentCommonFulldisplaySet;

		// Token: 0x04006931 RID: 26929
		[Header("일본 법무 대응")]
		public GameObject m_objJPNPolicy;

		// Token: 0x04006932 RID: 26930
		public NKCUIComStateButton m_csbtnJPNPaymentLaw;

		// Token: 0x04006933 RID: 26931
		public NKCUIComStateButton m_csbtnJPNCommecialLaw;

		// Token: 0x04006934 RID: 26932
		[Header("단계형 상품 관련")]
		public GameObject m_objChainTab;

		// Token: 0x04006935 RID: 26933
		public NKCUIComToggle m_tglChain_01;

		// Token: 0x04006936 RID: 26934
		public NKCUIComToggle m_tglChain_02;

		// Token: 0x04006937 RID: 26935
		public NKCUIComToggle m_tglChain_03;

		// Token: 0x04006938 RID: 26936
		public GameObject m_objChainLocked;

		// Token: 0x04006939 RID: 26937
		public GameObject m_objTabResetTime;

		// Token: 0x0400693A RID: 26938
		public Text m_lbTabRemainTime;

		// Token: 0x0400693B RID: 26939
		[Header("홈 화면 추가 구성")]
		public NKCUIComDragSelectablePanel m_HomePackageSlidePanel;

		// Token: 0x0400693C RID: 26940
		public NKCUIShopSlotHomeBanner m_pfbHomeLimitPackageSlot;

		// Token: 0x0400693D RID: 26941
		public GameObject m_objFeaturedEmpty;

		// Token: 0x0400693E RID: 26942
		[Header("NPC")]
		public NKCUINPCSpine m_UINPCShop;

		// Token: 0x0400693F RID: 26943
		public GameObject m_objNPCFront;

		// Token: 0x04006940 RID: 26944
		[Header("보급소 갱신")]
		public Text m_lbSupplyTimeLeft;

		// Token: 0x04006941 RID: 26945
		public Text m_lbSupplyCountLeft;

		// Token: 0x04006942 RID: 26946
		public Text m_lbSupplyRefreshCost;

		// Token: 0x04006943 RID: 26947
		public NKCUIComButton m_cbtnSupplyRefresh;

		// Token: 0x04006944 RID: 26948
		[Header("상품이 없을 경우")]
		public GameObject m_objEmptyList;

		// Token: 0x04006945 RID: 26949
		[Header("상품 받아오는 중")]
		public GameObject m_objFetchItem;

		// Token: 0x04006946 RID: 26950
		[Header("카테고리 변경 UI")]
		public NKCUIComStateButton m_csbtnChangeCategory;

		// Token: 0x04006947 RID: 26951
		public NKCUIShopCategoryChange m_uiShopCategoryChange;

		// Token: 0x04006948 RID: 26952
		public GameObject m_objCategoryReddot;

		// Token: 0x04006949 RID: 26953
		public GameObject m_objReddot_RED;

		// Token: 0x0400694A RID: 26954
		public GameObject m_objReddot_YELLOW;

		// Token: 0x0400694B RID: 26955
		public Text m_lbCategoryReddotCount;

		// Token: 0x0400694C RID: 26956
		[Header("필터")]
		public NKCUIComStateButton m_csbtnFilter;

		// Token: 0x0400694D RID: 26957
		public NKCUIComStateButton m_csbtnFilterActive;

		// Token: 0x0400694E RID: 26958
		private NKCShopProductSortSystem m_ssProduct;

		// Token: 0x0400694F RID: 26959
		private List<NKCShopBannerTemplet> m_lstHomeBannerTemplet = new List<NKCShopBannerTemplet>();

		// Token: 0x04006950 RID: 26960
		private Stack<NKCUIShopSlotHomeBanner> m_stkLevelPackageObjects = new Stack<NKCUIShopSlotHomeBanner>();

		// Token: 0x04006951 RID: 26961
		private Dictionary<ShopDisplayType, List<NKCUIShopSlotBase>> m_dicCardSlot = new Dictionary<ShopDisplayType, List<NKCUIShopSlotBase>>();

		// Token: 0x04006952 RID: 26962
		private Dictionary<ShopDisplayType, Stack<NKCUIShopSlotBase>> m_dicSlotStack = new Dictionary<ShopDisplayType, Stack<NKCUIShopSlotBase>>();

		// Token: 0x04006953 RID: 26963
		protected List<ShopTabTemplet> m_lstEnabledTabs = new List<ShopTabTemplet>();

		// Token: 0x04006954 RID: 26964
		protected Dictionary<string, NKCUIShopTab> m_dicTab = new Dictionary<string, NKCUIShopTab>();

		// Token: 0x04006955 RID: 26965
		protected Dictionary<ShopTabTemplet, List<ShopItemTemplet>> m_dicProducts = new Dictionary<ShopTabTemplet, List<ShopItemTemplet>>();

		// Token: 0x04006956 RID: 26966
		private Color BUY_ALL_TEXT_COLOR_DEFAULT = new Color(0.34509805f, 0.15686275f, 0.09019608f);

		// Token: 0x04006957 RID: 26967
		private string m_eCurrentTab = "TAB_NONE";

		// Token: 0x04006958 RID: 26968
		private int m_CurrentSubIndex;

		// Token: 0x04006959 RID: 26969
		private int m_CurrentChainIndex;

		// Token: 0x0400695A RID: 26970
		private long m_NextSupplyUpdateTick;

		// Token: 0x0400695B RID: 26971
		private long m_NextChainUpdateTick;

		// Token: 0x0400695C RID: 26972
		private long m_TabEndTick;

		// Token: 0x0400695D RID: 26973
		private bool m_bUseTabEndTimer;

		// Token: 0x0400695E RID: 26974
		private long ShopItemUpdateTimeStamp;

		// Token: 0x0400695F RID: 26975
		private ShopDisplayType m_eDisplayType = ShopDisplayType.None;

		// Token: 0x04006960 RID: 26976
		private List<ShopItemTemplet> m_lstShopItem = new List<ShopItemTemplet>();

		// Token: 0x04006961 RID: 26977
		private List<NKCShopFeaturedTemplet> m_lstFeatured = new List<NKCShopFeaturedTemplet>();

		// Token: 0x04006962 RID: 26978
		private bool m_bPlayedScenMusic;

		// Token: 0x04006963 RID: 26979
		protected NKCUIShop.eTabMode m_eTabMode;

		// Token: 0x04006964 RID: 26980
		protected NKCShopManager.ShopTabCategory m_eCategory = NKCShopManager.ShopTabCategory.NONE;

		// Token: 0x04006965 RID: 26981
		private bool playingTutorial;

		// Token: 0x04006966 RID: 26982
		private float UPDATE_INTERVAL = 1f;

		// Token: 0x04006967 RID: 26983
		private float updateTimer;

		// Token: 0x02001846 RID: 6214
		[Serializable]
		public struct DisplaySet
		{
			// Token: 0x0600B57E RID: 46462 RVA: 0x0036525A File Offset: 0x0036345A
			public override string ToString()
			{
				return "LoopScroll " + this.slotType.ToString();
			}

			// Token: 0x0400A87C RID: 43132
			public ShopDisplayType slotType;

			// Token: 0x0400A87D RID: 43133
			public NKCUIShopSlotBase slotPrefab;

			// Token: 0x0400A87E RID: 43134
			public LoopScrollRect scrollRect;

			// Token: 0x0400A87F RID: 43135
			public RectTransform m_rtParent;
		}

		// Token: 0x02001847 RID: 6215
		[Serializable]
		public struct FullDisplaySet
		{
			// Token: 0x0600B57F RID: 46463 RVA: 0x00365277 File Offset: 0x00363477
			public override string ToString()
			{
				return "FullDisplay " + this.slotType.ToString();
			}

			// Token: 0x0400A880 RID: 43136
			public ShopDisplayType slotType;

			// Token: 0x0400A881 RID: 43137
			public NKCUIShopSlotBase slotPrefab;

			// Token: 0x0400A882 RID: 43138
			public NKCUIComDragSelectablePanel draggablePanel;

			// Token: 0x0400A883 RID: 43139
			public RectTransform m_rtParent;
		}

		// Token: 0x02001848 RID: 6216
		public enum eTabMode
		{
			// Token: 0x0400A885 RID: 43141
			Fold,
			// Token: 0x0400A886 RID: 43142
			All
		}

		// Token: 0x02001849 RID: 6217
		// (Invoke) Token: 0x0600B581 RID: 46465
		public delegate void OnProductBuyDelegate(int ProductID, int ProductCount = 1, List<int> lstSelection = null);
	}
}
