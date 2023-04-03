using System;
using NKC.UI.NPC;
using NKM;
using NKM.Shop;
using NKM.Templet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Shop
{
	// Token: 0x02000AC7 RID: 2759
	public class NKCUIShopSingleTab : NKCUIShop
	{
		// Token: 0x17001488 RID: 5256
		// (get) Token: 0x06007B6F RID: 31599 RVA: 0x00292B13 File Offset: 0x00290D13
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.DEFAULT;
			}
		}

		// Token: 0x06007B70 RID: 31600 RVA: 0x00292B16 File Offset: 0x00290D16
		public static NKCUIShopSingleTab GetInstance(string bundleName, string assetName)
		{
			NKCUIShopSingleTab instance = NKCUIManager.OpenNewInstance<NKCUIShopSingleTab>(bundleName, assetName, NKCUIManager.eUIBaseRect.UIFrontCommon, null).GetInstance<NKCUIShopSingleTab>();
			instance.Init();
			return instance;
		}

		// Token: 0x17001489 RID: 5257
		// (get) Token: 0x06007B71 RID: 31601 RVA: 0x00292B2C File Offset: 0x00290D2C
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x1700148A RID: 5258
		// (get) Token: 0x06007B72 RID: 31602 RVA: 0x00292B2F File Offset: 0x00290D2F
		public override string MenuName
		{
			get
			{
				return NKCStringTable.GetString(this.m_MenuNameStringKey, false);
			}
		}

		// Token: 0x1700148B RID: 5259
		// (get) Token: 0x06007B73 RID: 31603 RVA: 0x00292B3D File Offset: 0x00290D3D
		protected override bool AlwaysShowNPC
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700148C RID: 5260
		// (get) Token: 0x06007B74 RID: 31604 RVA: 0x00292B40 File Offset: 0x00290D40
		protected override bool UseTabVisible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007B75 RID: 31605 RVA: 0x00292B43 File Offset: 0x00290D43
		public override void OnBackButton()
		{
			base.Close();
		}

		// Token: 0x06007B76 RID: 31606 RVA: 0x00292B4C File Offset: 0x00290D4C
		public void Open(string title, string subTitle, int resourceID, NKMAssetName cNKMAssetName, NKCShopManager.ShopTabCategory category, string selectedTab = "TAB_NONE", int subTabIndex = 0)
		{
			base.gameObject.SetActive(true);
			NKCUtil.SetLabelText(this.m_lbTitle, title);
			NKCUtil.SetLabelText(this.m_lbSubTitle, subTitle);
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(resourceID);
			if (itemMiscTempletByID != null)
			{
				NKCUtil.SetImageSprite(this.m_imgResource, NKCResourceUtility.GetOrLoadMiscItemIcon(itemMiscTempletByID), false);
				NKCUtil.SetLabelText(this.m_lbResourceCount, NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(resourceID).ToString());
			}
			if (this.m_imgBG != null && cNKMAssetName != null)
			{
				NKCUtil.SetImageSprite(this.m_imgBG, NKCResourceUtility.GetOrLoadAssetResource<Sprite>(cNKMAssetName), false);
			}
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

		// Token: 0x06007B77 RID: 31607 RVA: 0x00292C28 File Offset: 0x00290E28
		private void OpenWithItemList(NKCShopManager.ShopTabCategory category, string selectedTab = "TAB_NONE", int subTabIndex = 0)
		{
			base.BuildProductList(false);
			if (this.m_eCategory != category)
			{
				base.CleanupTab();
			}
			this.m_eCategory = category;
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

		// Token: 0x06007B78 RID: 31608 RVA: 0x00292C96 File Offset: 0x00290E96
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

		// Token: 0x04006820 RID: 26656
		public NKCUIShop.eTabMode m_eShopTabMode = NKCUIShop.eTabMode.All;

		// Token: 0x04006821 RID: 26657
		[Header("������ ����")]
		public TMP_Text m_lbTitle;

		// Token: 0x04006822 RID: 26658
		public TMP_Text m_lbSubTitle;

		// Token: 0x04006823 RID: 26659
		public Image m_imgResource;

		// Token: 0x04006824 RID: 26660
		public TMP_Text m_lbResourceCount;

		// Token: 0x04006825 RID: 26661
		public Image m_imgBG;

		// Token: 0x04006826 RID: 26662
		private string m_MenuNameStringKey;
	}
}
