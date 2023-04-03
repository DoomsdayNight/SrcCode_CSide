using System;
using System.Collections.Generic;
using NKM;
using NKM.Shop;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Shop
{
	// Token: 0x02000ACA RID: 2762
	public class NKCPopupShopBuyShortcut : NKCUIBase
	{
		// Token: 0x17001497 RID: 5271
		// (get) Token: 0x06007BB3 RID: 31667 RVA: 0x00294510 File Offset: 0x00292710
		public static NKCPopupShopBuyShortcut Instance
		{
			get
			{
				if (NKCPopupShopBuyShortcut.m_Instance == null)
				{
					NKCPopupShopBuyShortcut.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupShopBuyShortcut>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_SHOP_BUY_SHORTCUT", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupShopBuyShortcut.CleanupInstance)).GetInstance<NKCPopupShopBuyShortcut>();
					NKCPopupShopBuyShortcut.m_Instance.Init();
				}
				return NKCPopupShopBuyShortcut.m_Instance;
			}
		}

		// Token: 0x17001498 RID: 5272
		// (get) Token: 0x06007BB4 RID: 31668 RVA: 0x0029455F File Offset: 0x0029275F
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupShopBuyShortcut.m_Instance != null && NKCPopupShopBuyShortcut.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007BB5 RID: 31669 RVA: 0x0029457A File Offset: 0x0029277A
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupShopBuyShortcut.m_Instance != null && NKCPopupShopBuyShortcut.m_Instance.IsOpen)
			{
				NKCPopupShopBuyShortcut.m_Instance.Close();
			}
		}

		// Token: 0x06007BB6 RID: 31670 RVA: 0x0029459F File Offset: 0x0029279F
		private static void CleanupInstance()
		{
			NKCPopupShopBuyShortcut.m_Instance = null;
		}

		// Token: 0x17001499 RID: 5273
		// (get) Token: 0x06007BB7 RID: 31671 RVA: 0x002945A7 File Offset: 0x002927A7
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700149A RID: 5274
		// (get) Token: 0x06007BB8 RID: 31672 RVA: 0x002945AA File Offset: 0x002927AA
		public override string MenuName
		{
			get
			{
				return "상품 직접 구매 팝업";
			}
		}

		// Token: 0x06007BB9 RID: 31673 RVA: 0x002945B4 File Offset: 0x002927B4
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
			foreach (NKCUIShopSlotCard nkcuishopSlotCard in this.m_lstShopSlotCard)
			{
				NKCUtil.SetGameobjectActive(nkcuishopSlotCard.gameObject, false);
			}
			foreach (NKCUIComResourceButton nkcuicomResourceButton in this.m_lstResourceBtn)
			{
				UnityEngine.Object.Destroy(nkcuicomResourceButton.gameObject);
			}
			this.m_lstResourceBtn.Clear();
		}

		// Token: 0x06007BBA RID: 31674 RVA: 0x00294668 File Offset: 0x00292868
		private void Init()
		{
			NKCUtil.SetBindFunction(this.m_NKM_UI_POPUP_CLOSEBUTTON, new UnityAction(base.Close));
			NKCUtil.SetBindFunction(this.m_NKM_UI_POPUP_SHOP_BUY_SHORTCUT_BG, new UnityAction(base.Close));
			if (this.m_LayOutGroup != null)
			{
				this.m_Spacing = this.m_LayOutGroup.spacing;
				RectTransform component = this.m_LayOutGroup.GetComponent<RectTransform>();
				if (component != null)
				{
					this.m_ContectRectWidth = component.rect.width;
				}
			}
			if (this.m_NKM_UI_SHOP_CARD_SLOT != null)
			{
				this.m_CardSlotRectWidth = this.m_NKM_UI_SHOP_CARD_SLOT.rect.width;
			}
		}

		// Token: 0x06007BBB RID: 31675 RVA: 0x00294714 File Offset: 0x00292914
		public static void Open(int itemID)
		{
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(itemID);
			if (itemMiscTempletByID == null)
			{
				return;
			}
			NKCPopupShopBuyShortcut.Open(itemMiscTempletByID);
		}

		// Token: 0x06007BBC RID: 31676 RVA: 0x00294734 File Offset: 0x00292934
		public static void Open(NKMItemMiscTemplet templet)
		{
			NKCShopManager.FetchShopItemList(NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, delegate(bool bSuccess)
			{
				if (bSuccess)
				{
					NKCPopupShopBuyShortcut.Instance.OpenWindow(templet);
					return;
				}
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_SHOP_WAS_NOT_ABLE_TO_GET_PRODUCT_LIST_FROM_SERVER, null, "");
			}, false);
		}

		// Token: 0x06007BBD RID: 31677 RVA: 0x00294761 File Offset: 0x00292961
		private void OpenWindow(NKMItemMiscTemplet templet)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_CurMiscTemplet = templet;
			this.UpdateUI();
			base.UIOpened(true);
		}

		// Token: 0x06007BBE RID: 31678 RVA: 0x00294784 File Offset: 0x00292984
		public void UpdateUI()
		{
			if (this.m_CurMiscTemplet == null)
			{
				return;
			}
			if (this.m_CurMiscTemplet.m_lstRecommandProductItemIfNotEnough == null)
			{
				return;
			}
			if (this.m_CurMiscTemplet.m_lstRecommandProductItemIfNotEnough.Count <= 0)
			{
				return;
			}
			List<ShopItemTemplet> list = new List<ShopItemTemplet>();
			List<ShopItemTemplet> list2 = new List<ShopItemTemplet>();
			for (int i = 0; i < this.m_CurMiscTemplet.m_lstRecommandProductItemIfNotEnough.Count; i++)
			{
				int num = this.m_CurMiscTemplet.m_lstRecommandProductItemIfNotEnough[i];
				ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(num);
				if (NKCShopManager.CanExhibitItem(shopItemTemplet, true, false))
				{
					if (NKCShopManager.GetBuyCountLeft(num) == 0)
					{
						list2.Add(shopItemTemplet);
					}
					else
					{
						list.Add(shopItemTemplet);
					}
				}
			}
			list.AddRange(list2);
			while (this.m_lstShopSlotCard.Count < list.Count)
			{
				NKCUIShopSlotCard nkcuishopSlotCard = UnityEngine.Object.Instantiate<NKCUIShopSlotCard>(this.m_pfbNKM_UI_SHOP_SKIN_SLOT);
				if (nkcuishopSlotCard != null)
				{
					nkcuishopSlotCard.Init(new NKCUIShopSlotBase.OnBuy(this.OnBtnProductBuy), null);
					RectTransform component = nkcuishopSlotCard.GetComponent<RectTransform>();
					if (component != null)
					{
						component.localScale = Vector2.one;
					}
					nkcuishopSlotCard.transform.SetParent(this.m_Content, false);
					nkcuishopSlotCard.transform.localPosition = Vector3.zero;
					this.m_lstShopSlotCard.Add(nkcuishopSlotCard);
				}
			}
			for (int j = 0; j < this.m_lstShopSlotCard.Count; j++)
			{
				NKCUIShopSlotCard nkcuishopSlotCard2 = this.m_lstShopSlotCard[j];
				if (j < list.Count)
				{
					ShopItemTemplet shopItemTemplet2 = list[j];
					NKCUtil.SetGameobjectActive(nkcuishopSlotCard2, true);
					nkcuishopSlotCard2.SetData(null, shopItemTemplet2, NKCShopManager.GetBuyCountLeft(shopItemTemplet2.m_ProductID), NKCUIShop.IsFirstBuy(shopItemTemplet2.m_ProductID));
				}
				else
				{
					NKCUtil.SetGameobjectActive(nkcuishopSlotCard2, false);
				}
			}
			NKMInventoryData inventoryData = NKCScenManager.CurrentUserData().m_InventoryData;
			NKCUtil.SetGameobjectActive(this.m_RESOURCE_1, true);
			NKCUtil.SetGameobjectActive(this.m_RESOURCE_2, true);
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(102);
			if (itemMiscTempletByID != null)
			{
				Sprite orLoadMiscItemSmallIcon = NKCResourceUtility.GetOrLoadMiscItemSmallIcon(itemMiscTempletByID);
				NKCUtil.SetImageSprite(this.m_RESOURCE_ICON1, orLoadMiscItemSmallIcon, false);
				NKCUtil.SetLabelText(this.m_RESOURCE_TEXT1, inventoryData.GetCountMiscItem(102).ToString());
			}
			itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(101);
			if (itemMiscTempletByID != null)
			{
				Sprite orLoadMiscItemSmallIcon2 = NKCResourceUtility.GetOrLoadMiscItemSmallIcon(itemMiscTempletByID);
				NKCUtil.SetImageSprite(this.m_RESOURCE_ICON2, orLoadMiscItemSmallIcon2, false);
				NKCUtil.SetLabelText(this.m_RESOURCE_TEXT2, inventoryData.GetCountMiscItem(101).ToString());
			}
			if ((float)list.Count * (this.m_CardSlotRectWidth + this.m_Spacing) >= this.m_ContectRectWidth)
			{
				this.m_Content.pivot = new Vector2(0f, 0.5f);
			}
			else
			{
				this.m_Content.pivot = new Vector2(0.5f, 0.5f);
			}
			list.Sort((ShopItemTemplet a, ShopItemTemplet b) => a.m_OrderList.CompareTo(b.m_OrderList));
			HashSet<int> hashSet = new HashSet<int>();
			foreach (ShopItemTemplet shopItemTemplet3 in list)
			{
				hashSet.Add(shopItemTemplet3.m_PriceItemID);
			}
			foreach (int num2 in hashSet)
			{
				if (num2 != 101 && num2 != 102 && num2 != 0)
				{
					NKCUIComResourceButton nkcuicomResourceButton = UnityEngine.Object.Instantiate<NKCUIComResourceButton>(this.m_pbfNKM_UI_COMMON_RESOURCE);
					if (nkcuicomResourceButton != null)
					{
						nkcuicomResourceButton.gameObject.transform.SetParent(this.m_RESOURCE_LIST_LayoutGruop, false);
						nkcuicomResourceButton.SetData(num2, (int)inventoryData.GetCountMiscItem(num2));
						this.m_lstResourceBtn.Add(nkcuicomResourceButton);
					}
				}
			}
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_SHOP_BUY_SHORTCUT_TOP_TEXT, NKCStringTable.GetString("SI_DP_ITEM_NOT_ENOUGH_PRODUCT_POPUP_DESC", false, new object[]
			{
				this.m_CurMiscTemplet.GetItemName()
			}));
		}

		// Token: 0x06007BBF RID: 31679 RVA: 0x00294B68 File Offset: 0x00292D68
		private void OnBtnProductBuy(int ProductID)
		{
			NKCShopManager.OnBtnProductBuy(ProductID, false);
		}

		// Token: 0x0400686C RID: 26732
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_ok_cancel_box";

		// Token: 0x0400686D RID: 26733
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_SHOP_BUY_SHORTCUT";

		// Token: 0x0400686E RID: 26734
		private static NKCPopupShopBuyShortcut m_Instance;

		// Token: 0x0400686F RID: 26735
		public Text m_NKM_UI_POPUP_SHOP_BUY_SHORTCUT_TOP_TEXT;

		// Token: 0x04006870 RID: 26736
		public GameObject m_RESOURCE_1;

		// Token: 0x04006871 RID: 26737
		public Image m_RESOURCE_ICON1;

		// Token: 0x04006872 RID: 26738
		public Text m_RESOURCE_TEXT1;

		// Token: 0x04006873 RID: 26739
		public GameObject m_RESOURCE_2;

		// Token: 0x04006874 RID: 26740
		public Image m_RESOURCE_ICON2;

		// Token: 0x04006875 RID: 26741
		public Text m_RESOURCE_TEXT2;

		// Token: 0x04006876 RID: 26742
		public RectTransform m_Content;

		// Token: 0x04006877 RID: 26743
		public NKCUIShopSlotCard m_pfbNKM_UI_SHOP_SKIN_SLOT;

		// Token: 0x04006878 RID: 26744
		public NKCUIComStateButton m_NKM_UI_POPUP_CLOSEBUTTON;

		// Token: 0x04006879 RID: 26745
		public NKCUIComStateButton m_NKM_UI_POPUP_SHOP_BUY_SHORTCUT_BG;

		// Token: 0x0400687A RID: 26746
		private NKMItemMiscTemplet m_CurMiscTemplet;

		// Token: 0x0400687B RID: 26747
		private List<NKCUIShopSlotCard> m_lstShopSlotCard = new List<NKCUIShopSlotCard>();

		// Token: 0x0400687C RID: 26748
		public RectTransform m_RESOURCE_LIST_LayoutGruop;

		// Token: 0x0400687D RID: 26749
		public NKCUIComResourceButton m_pbfNKM_UI_COMMON_RESOURCE;

		// Token: 0x0400687E RID: 26750
		private List<NKCUIComResourceButton> m_lstResourceBtn = new List<NKCUIComResourceButton>();

		// Token: 0x0400687F RID: 26751
		[Space]
		[Header("UI 사이즈 조절용")]
		public HorizontalLayoutGroup m_LayOutGroup;

		// Token: 0x04006880 RID: 26752
		public RectTransform m_NKM_UI_SHOP_CARD_SLOT;

		// Token: 0x04006881 RID: 26753
		private float m_ContectRectWidth;

		// Token: 0x04006882 RID: 26754
		private float m_Spacing;

		// Token: 0x04006883 RID: 26755
		private float m_CardSlotRectWidth;
	}
}
