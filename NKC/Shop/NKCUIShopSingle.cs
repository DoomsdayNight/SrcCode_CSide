using System;
using System.Collections.Generic;
using NKC.UI.NPC;
using NKM.Shop;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI.Shop
{
	// Token: 0x02000ADB RID: 2779
	public class NKCUIShopSingle : NKCUIShop
	{
		// Token: 0x170014B8 RID: 5304
		// (get) Token: 0x06007CBA RID: 31930 RVA: 0x0029C01F File Offset: 0x0029A21F
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.DEFAULT;
			}
		}

		// Token: 0x06007CBB RID: 31931 RVA: 0x0029C022 File Offset: 0x0029A222
		public static NKCUIShopSingle GetInstance(string bundleName, string assetName)
		{
			NKCUIShopSingle instance = NKCUIManager.OpenNewInstance<NKCUIShopSingle>(bundleName, assetName, NKCUIManager.eUIBaseRect.UIFrontCommon, null).GetInstance<NKCUIShopSingle>();
			instance.Init();
			return instance;
		}

		// Token: 0x170014B9 RID: 5305
		// (get) Token: 0x06007CBC RID: 31932 RVA: 0x0029C038 File Offset: 0x0029A238
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170014BA RID: 5306
		// (get) Token: 0x06007CBD RID: 31933 RVA: 0x0029C03B File Offset: 0x0029A23B
		public override string MenuName
		{
			get
			{
				return NKCStringTable.GetString(this.m_MenuNameStringKey, false);
			}
		}

		// Token: 0x170014BB RID: 5307
		// (get) Token: 0x06007CBE RID: 31934 RVA: 0x0029C049 File Offset: 0x0029A249
		protected override bool AlwaysShowNPC
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170014BC RID: 5308
		// (get) Token: 0x06007CBF RID: 31935 RVA: 0x0029C04C File Offset: 0x0029A24C
		protected override bool UseTabVisible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007CC0 RID: 31936 RVA: 0x0029C04F File Offset: 0x0029A24F
		public override void OnBackButton()
		{
			base.Close();
		}

		// Token: 0x06007CC1 RID: 31937 RVA: 0x0029C058 File Offset: 0x0029A258
		public void Open(NKCShopManager.ShopTabCategory category, string selectedTab = "TAB_NONE", int subTabIndex = 0)
		{
			base.gameObject.SetActive(true);
			this.m_eTabMode = this.m_eShopTabMode;
			NKCShopManager.FetchShopItemList(NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, delegate(bool bSuccess)
			{
				if (bSuccess)
				{
					this.OpenWithItemList(category, selectedTab, subTabIndex);
					return;
				}
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_SHOP_WAS_NOT_ABLE_TO_GET_PRODUCT_LIST_FROM_SERVER, new NKCPopupOKCancel.OnButton(this.Close), "");
			}, false);
		}

		// Token: 0x06007CC2 RID: 31938 RVA: 0x0029C0B4 File Offset: 0x0029A2B4
		private void OpenWithItemList(NKCShopManager.ShopTabCategory category, string selectedTab = "TAB_NONE", int subTabIndex = 0)
		{
			base.BuildProductList(false);
			if (this.m_eCategory != category)
			{
				base.CleanupTab();
			}
			this.m_eCategory = category;
			List<ShopTabTemplet> useTabList = NKCShopManager.GetUseTabList(this.m_eCategory);
			if (this.m_dicTab.Count == 0)
			{
				base.BuildTabs(useTabList);
			}
			if (!string.IsNullOrEmpty(selectedTab) && selectedTab != "TAB_NONE" && !useTabList.Exists((ShopTabTemplet x) => x.TabType == selectedTab))
			{
				Debug.LogError(string.Format("Tab {0} does not exist in category {1}!", selectedTab, category));
				selectedTab = "TAB_NONE";
			}
			if (selectedTab == "TAB_NONE")
			{
				selectedTab = useTabList[0].TabType;
			}
			base.SelectTab(selectedTab, subTabIndex, true, true);
			NKCUtil.SetLabelText(this.m_lbSupplyRefreshCost, 15.ToString());
			base.gameObject.SetActive(true);
			base.UIOpened(true);
			NKCUINPCSpine uinpcshop = this.m_UINPCShop;
			if (uinpcshop == null)
			{
				return;
			}
			uinpcshop.PlayAni(NPC_ACTION_TYPE.START, false);
		}

		// Token: 0x06007CC3 RID: 31939 RVA: 0x0029C1D1 File Offset: 0x0029A3D1
		public override void OnProductBuy(ShopItemTemplet productTemplet)
		{
			if (productTemplet != null && productTemplet.m_ItemType != NKM_REWARD_TYPE.RT_SKIN)
			{
				NKCUINPCSpine uinpcshop = this.m_UINPCShop;
				if (uinpcshop == null)
				{
					return;
				}
				uinpcshop.PlayAni(NPC_ACTION_TYPE.THANKS, false);
			}
		}

		// Token: 0x04006973 RID: 26995
		[Header("싱글샵 관련")]
		public string m_MenuNameStringKey;

		// Token: 0x04006974 RID: 26996
		public NKCUIShop.eTabMode m_eShopTabMode = NKCUIShop.eTabMode.All;
	}
}
