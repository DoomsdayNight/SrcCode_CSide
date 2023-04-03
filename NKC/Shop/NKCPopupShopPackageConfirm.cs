using System;
using System.Collections.Generic;
using NKC.Publisher;
using NKM;
using NKM.Item;
using NKM.Shop;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Shop
{
	// Token: 0x02000ACF RID: 2767
	public class NKCPopupShopPackageConfirm : NKCUIBase, IScrollHandler, IEventSystemHandler
	{
		// Token: 0x170014AA RID: 5290
		// (get) Token: 0x06007BFB RID: 31739 RVA: 0x00295ED4 File Offset: 0x002940D4
		public static NKCPopupShopPackageConfirm Instance
		{
			get
			{
				if (NKCPopupShopPackageConfirm.m_Instance == null)
				{
					NKCPopupShopPackageConfirm.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupShopPackageConfirm>("ab_ui_nkm_ui_shop", "NKM_UI_POPUP_SHOP_BUY_PACKAGE_CONFIRM", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupShopPackageConfirm.CleanupInstance)).GetInstance<NKCPopupShopPackageConfirm>();
					NKCPopupShopPackageConfirm.m_Instance.InitUI();
				}
				return NKCPopupShopPackageConfirm.m_Instance;
			}
		}

		// Token: 0x06007BFC RID: 31740 RVA: 0x00295F23 File Offset: 0x00294123
		private static void CleanupInstance()
		{
			NKCPopupShopPackageConfirm.m_Instance = null;
		}

		// Token: 0x06007BFD RID: 31741 RVA: 0x00295F2C File Offset: 0x0029412C
		public static NKCPopupShopPackageConfirm OpenNewInstance()
		{
			NKCPopupShopPackageConfirm instance = NKCUIManager.OpenNewInstance<NKCPopupShopPackageConfirm>("ab_ui_nkm_ui_shop", "NKM_UI_POPUP_SHOP_BUY_PACKAGE_CONFIRM", NKCUIManager.eUIBaseRect.UIFrontPopup, null).GetInstance<NKCPopupShopPackageConfirm>();
			if (instance != null)
			{
				instance.InitUI();
			}
			return instance;
		}

		// Token: 0x170014AB RID: 5291
		// (get) Token: 0x06007BFE RID: 31742 RVA: 0x00295F60 File Offset: 0x00294160
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170014AC RID: 5292
		// (get) Token: 0x06007BFF RID: 31743 RVA: 0x00295F63 File Offset: 0x00294163
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_SHOP_PACKAGE_INFO;
			}
		}

		// Token: 0x170014AD RID: 5293
		// (get) Token: 0x06007C00 RID: 31744 RVA: 0x00295F6A File Offset: 0x0029416A
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.ResourceOnly;
			}
		}

		// Token: 0x170014AE RID: 5294
		// (get) Token: 0x06007C01 RID: 31745 RVA: 0x00295F6D File Offset: 0x0029416D
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return NKCShopManager.GetUpsideMenuItemList(this.m_cNKMShopItemTemplet);
			}
		}

		// Token: 0x06007C02 RID: 31746 RVA: 0x00295F7C File Offset: 0x0029417C
		private void InitUI()
		{
			NKCUtil.SetBindFunction(this.m_NKM_UI_POPUP_BUY_BUTTON, new UnityAction(this.OnBtnBuy));
			NKCUtil.SetHotkey(this.m_NKM_UI_POPUP_BUY_BUTTON, HotkeyEventType.Confirm, null, false);
			NKCUtil.SetBindFunction(this.m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_BG, new UnityAction(base.Close));
			NKCUtil.SetBindFunction(this.m_NKM_UI_POPUP_CANCLE_BUTTON, new UnityAction(base.Close));
			NKCUtil.SetBindFunction(this.m_csbtnOK, new UnityAction(base.Close));
			NKCUtil.SetHotkey(this.m_csbtnOK, HotkeyEventType.Confirm, null, false);
			NKCUtil.SetBindFunction(this.m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_BUY_COUNT_PLUS, delegate()
			{
				this.ChangeValue(true);
			});
			NKCUtil.SetHotkey(this.m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_BUY_COUNT_PLUS, HotkeyEventType.Plus, null, false);
			NKCUtil.SetBindFunction(this.m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_BUY_COUNT_MINUS, delegate()
			{
				this.ChangeValue(false);
			});
			NKCUtil.SetHotkey(this.m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_BUY_COUNT_MINUS, HotkeyEventType.Minus, null, false);
			NKCUtil.SetBindFunction(this.m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_POLICY_BUTTON, new UnityAction(this.OnPolicy));
			NKCUtil.SetBindFunction(this.m_csbtnLinkedItem, new UnityAction(this.OnBtnLinkedItem));
			this.m_PackageIcon.Init();
			this.InitHoldButtonEvent();
			NKCUtil.SetGameobjectActive(this.m_JPN_BTN, false);
			NKCUtil.SetGameobjectActive(this.m_JPN_POLICY, false);
			if (NKCPublisherModule.InAppPurchase.ShowJPNPaymentPolicy())
			{
				NKCUtil.SetBindFunction(this.m_csbtnJPNPaymentLaw, new UnityAction(this.OnBtnJPNPaymentLaw));
				NKCUtil.SetBindFunction(this.m_csbtnJPNCommercialLaw, new UnityAction(this.OnBtnJPNCommercialLaw));
			}
		}

		// Token: 0x06007C03 RID: 31747 RVA: 0x002960E0 File Offset: 0x002942E0
		private void InitHoldButtonEvent()
		{
			this.UpdateHoldButtonEvent(this.m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_BUY_COUNT_PLUS.PointerDown, new UnityAction<PointerEventData>(this.OnPlusDown));
			this.UpdateHoldButtonEvent(this.m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_BUY_COUNT_PLUS.PointerUp, new UnityAction(this.OnButtonUp));
			this.UpdateHoldButtonEvent(this.m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_BUY_COUNT_MINUS.PointerDown, new UnityAction<PointerEventData>(this.OnMinusDown));
			this.UpdateHoldButtonEvent(this.m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_BUY_COUNT_MINUS.PointerUp, new UnityAction(this.OnButtonUp));
		}

		// Token: 0x06007C04 RID: 31748 RVA: 0x00296161 File Offset: 0x00294361
		private void UpdateHoldButtonEvent(NKCUnityEvent newEvent, UnityAction<PointerEventData> eventData)
		{
			if (newEvent != null)
			{
				newEvent.RemoveAllListeners();
				newEvent.AddListener(eventData);
			}
		}

		// Token: 0x06007C05 RID: 31749 RVA: 0x00296173 File Offset: 0x00294373
		private void UpdateHoldButtonEvent(UnityEvent newEvent, UnityAction eventData)
		{
			if (newEvent != null)
			{
				newEvent.RemoveAllListeners();
				newEvent.AddListener(eventData);
			}
		}

		// Token: 0x06007C06 RID: 31750 RVA: 0x00296185 File Offset: 0x00294385
		public void Open(ShopItemTemplet shopItemTemplet, NKCUIShop.OnProductBuyDelegate onClose, List<int> lstSelection = null)
		{
			if (shopItemTemplet == null)
			{
				return;
			}
			this.dOnOKButton = onClose;
			this.m_cNKMShopItemTemplet = shopItemTemplet;
			this.m_lstSelection = lstSelection;
			this.m_iBuyCount = 1;
			this.m_iMaxBuyCount = 1;
			this.SetData(NKCPopupShopPackageConfirm.Mode.Buy);
			base.UIOpened(true);
		}

		// Token: 0x06007C07 RID: 31751 RVA: 0x002961BC File Offset: 0x002943BC
		public void OpenPreview(ShopItemTemplet shopItemTemplet)
		{
			if (shopItemTemplet == null)
			{
				return;
			}
			this.dOnOKButton = null;
			this.m_cNKMShopItemTemplet = shopItemTemplet;
			this.m_lstSelection = null;
			this.m_iBuyCount = 1;
			this.m_iMaxBuyCount = 1;
			this.SetData(NKCPopupShopPackageConfirm.Mode.Preview);
			base.UIOpened(true);
		}

		// Token: 0x06007C08 RID: 31752 RVA: 0x002961F4 File Offset: 0x002943F4
		private void SetData(NKCPopupShopPackageConfirm.Mode mode)
		{
			if (this.m_cNKMShopItemTemplet == null)
			{
				return;
			}
			if (mode != NKCPopupShopPackageConfirm.Mode.Buy)
			{
				if (mode == NKCPopupShopPackageConfirm.Mode.Preview)
				{
					NKCUtil.SetGameobjectActive(this.m_csbtnLinkedItem, false);
					NKCUtil.SetLabelText(this.m_lbTitle, NKCStringTable.GetString("SI_PF_SHOP_ITEM_PACKAGE_PREVIEW", false));
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_CANCLE_BUTTON, false);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_BUY_BUTTON, false);
					NKCUtil.SetGameobjectActive(this.m_csbtnOK, true);
				}
			}
			else
			{
				List<ShopItemTemplet> linkedItem = NKCShopManager.GetLinkedItem(this.m_cNKMShopItemTemplet.m_ProductID);
				NKCUtil.SetGameobjectActive(this.m_csbtnLinkedItem, linkedItem != null && linkedItem.Count > 0);
				NKCUtil.SetLabelText(this.m_lbTitle, NKCStringTable.GetString("SI_PF_SHOP_ITEM_PURCHASE_CONFIRM", false));
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_CANCLE_BUTTON, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_BUY_BUTTON, true);
				NKCUtil.SetGameobjectActive(this.m_csbtnOK, false);
			}
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_INFO_TITLE_TEXT, this.m_cNKMShopItemTemplet.GetItemName());
			string text = this.m_cNKMShopItemTemplet.GetItemDesc();
			if (text.Contains("\n"))
			{
				text = text.Replace("\n", " ");
			}
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_INFO_DESC_TEXT, text);
			this.UpdateSlot();
			this.UpdatePriceInfo();
			int buyCountLeft = NKCShopManager.GetBuyCountLeft(this.m_cNKMShopItemTemplet.m_ProductID);
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_COUNT_TEXT, NKCShopManager.GetBuyCountString(this.m_cNKMShopItemTemplet.resetType, buyCountLeft, this.m_cNKMShopItemTemplet.m_QuantityLimit, false));
			NKCUtil.SetGameobjectActive(this.m_DISCOUNT_TIME_TEXT2, this.m_cNKMShopItemTemplet.HasDateLimit);
			if (this.m_cNKMShopItemTemplet.HasDateLimit)
			{
				if (NKCSynchronizedTime.IsFinished(this.m_cNKMShopItemTemplet.EventDateEndUtc))
				{
					NKCUtil.SetLabelText(this.m_DISCOUNT_TIME_TEXT2, NKCUtilString.GET_STRING_QUIT);
				}
				else
				{
					NKCUtil.SetLabelText(this.m_DISCOUNT_TIME_TEXT2, NKCUtilString.GetRemainTimeStringOneParam(this.m_cNKMShopItemTemplet.EventDateEndUtc));
				}
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_POLICY, this.m_cNKMShopItemTemplet.m_PriceItemID == 0 && NKCShopManager.IsShowPurchasePolicy());
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_POLICY_BUTTON, NKCShopManager.IsShowPurchasePolicyBtn());
			if (mode == NKCPopupShopPackageConfirm.Mode.Buy)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_BUY_COUNT, this.m_cNKMShopItemTemplet.m_PriceItemID != 0);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_BUY_COUNT, false);
			}
			if (this.m_cNKMShopItemTemplet.m_PriceItemID != 0)
			{
				if (this.m_cNKMShopItemTemplet.m_PriceItemID != 0 && this.m_cNKMShopItemTemplet.m_Price > 0)
				{
					this.m_iMaxBuyCount = (int)(NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(this.m_cNKMShopItemTemplet.m_PriceItemID) / (long)this.m_cNKMShopItemTemplet.m_Price);
				}
				if (this.m_cNKMShopItemTemplet.m_QuantityLimit > 0 && buyCountLeft > 0)
				{
					this.m_iMaxBuyCount = Math.Min(buyCountLeft, this.m_iMaxBuyCount);
				}
				if (this.m_iMaxBuyCount < 1)
				{
					this.m_iMaxBuyCount = 1;
				}
				bool flag = !NKCShopManager.IsCustomPackageItem(this.m_cNKMShopItemTemplet.m_ProductID);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_BUY_COUNT_MINUS.gameObject, flag && this.m_iMaxBuyCount > 1);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_BUY_COUNT_PLUS.gameObject, flag && this.m_iMaxBuyCount > 1);
				NKCUtil.SetGameobjectActive(this.m_BUY_COUNT_TEXT.gameObject, flag && this.m_iMaxBuyCount > 1);
			}
			int priceItemID = this.m_cNKMShopItemTemplet.m_PriceItemID;
			if (priceItemID == 0 || priceItemID - 101 <= 1)
			{
				this.m_bUseMinusToMax = (buyCountLeft > 0);
			}
			else
			{
				this.m_bUseMinusToMax = true;
			}
			bool flag2 = NKCPublisherModule.InAppPurchase.ShowJPNPaymentPolicy();
			bool flag3 = this.m_cNKMShopItemTemplet.m_PriceItemID == 0;
			bool flag4 = NKCUtil.IsJPNPolicyRelatedItem(this.m_cNKMShopItemTemplet.m_PriceItemID);
			if (flag2)
			{
				NKCUtil.SetGameobjectActive(this.m_JPN_BTN, flag3);
				NKCUtil.SetGameobjectActive(this.m_JPN_POLICY, !flag3 && flag4);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_JPN_BTN, false);
			NKCUtil.SetGameobjectActive(this.m_JPN_POLICY, false);
		}

		// Token: 0x06007C09 RID: 31753 RVA: 0x002965A4 File Offset: 0x002947A4
		private bool IsCountVisible(ShopItemTemplet productTemplet)
		{
			if (productTemplet == null)
			{
				return false;
			}
			switch (productTemplet.m_ItemType)
			{
			case NKM_REWARD_TYPE.RT_MISC:
			case NKM_REWARD_TYPE.RT_USER_EXP:
			case NKM_REWARD_TYPE.RT_MOLD:
			case NKM_REWARD_TYPE.RT_SKIN:
			case NKM_REWARD_TYPE.RT_MISSION_POINT:
				return true;
			}
			return false;
		}

		// Token: 0x06007C0A RID: 31754 RVA: 0x00296600 File Offset: 0x00294800
		private bool IsCountVisible(NKCUISlot.SlotData slotData)
		{
			switch (slotData.eType)
			{
			case NKCUISlot.eSlotMode.ItemMisc:
			case NKCUISlot.eSlotMode.Mold:
			case NKCUISlot.eSlotMode.UnitCount:
				return true;
			case NKCUISlot.eSlotMode.Skin:
			case NKCUISlot.eSlotMode.Emoticon:
				return false;
			}
			return slotData.Count > 1L;
		}

		// Token: 0x06007C0B RID: 31755 RVA: 0x00296650 File Offset: 0x00294850
		private void SetSlotCount(int count)
		{
			while (this.m_lstItemSlot.Count < count)
			{
				NKCUISlot nkcuislot = UnityEngine.Object.Instantiate<NKCUISlot>(this.m_pfbSlot);
				nkcuislot.Init();
				nkcuislot.transform.SetParent(this.m_rt_Package_Contents, false);
				nkcuislot.transform.localScale = Vector3.one;
				this.m_lstItemSlot.Add(nkcuislot);
			}
		}

		// Token: 0x06007C0C RID: 31756 RVA: 0x002966AD File Offset: 0x002948AD
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06007C0D RID: 31757 RVA: 0x002966BC File Offset: 0x002948BC
		private void OnBtnBuy()
		{
			List<NKCShopManager.ShopRewardSubstituteData> list = NKCShopManager.MakeShopBuySubstituteItemList(this.m_cNKMShopItemTemplet, this.m_iBuyCount, this.m_lstSelection);
			if (list != null && list.Count > 0)
			{
				NKCPopupShopCustomPackageSubstitude.Instance.Open(list, new NKCPopupShopCustomPackageSubstitude.OnClose(this.ConfirmBuy));
				return;
			}
			this.ConfirmBuy();
		}

		// Token: 0x06007C0E RID: 31758 RVA: 0x0029670B File Offset: 0x0029490B
		private void ConfirmBuy()
		{
			NKCPopupShopCustomPackageSubstitude.CheckInstanceAndClose();
			base.Close();
			if (this.m_cNKMShopItemTemplet != null && this.dOnOKButton != null)
			{
				this.dOnOKButton(this.m_cNKMShopItemTemplet.m_ProductID, this.m_iBuyCount, this.m_lstSelection);
			}
		}

		// Token: 0x06007C0F RID: 31759 RVA: 0x0029674C File Offset: 0x0029494C
		private void Update()
		{
			if (this.m_cNKMShopItemTemplet != null && this.m_cNKMShopItemTemplet.HasDateLimit)
			{
				this.m_deltaTime += Time.deltaTime;
				if (this.m_deltaTime > 1f)
				{
					this.m_deltaTime -= 1f;
					if (NKCSynchronizedTime.IsFinished(this.m_cNKMShopItemTemplet.EventDateEndUtc))
					{
						NKCUtil.SetLabelText(this.m_DISCOUNT_TIME_TEXT2, NKCUtilString.GET_STRING_QUIT);
					}
					else
					{
						NKCUtil.SetLabelText(this.m_DISCOUNT_TIME_TEXT2, NKCUtilString.GetRemainTimeStringOneParam(this.m_cNKMShopItemTemplet.EventDateEndUtc));
					}
				}
			}
			this.OnUpdateButtonHold();
		}

		// Token: 0x06007C10 RID: 31760 RVA: 0x002967E4 File Offset: 0x002949E4
		private void UpdatePriceInfo()
		{
			if (this.m_cNKMShopItemTemplet == null)
			{
				return;
			}
			int realPrice = NKCScenManager.CurrentUserData().m_ShopData.GetRealPrice(this.m_cNKMShopItemTemplet, 1, false);
			bool flag = realPrice < this.m_cNKMShopItemTemplet.m_Price;
			if (this.m_cNKMShopItemTemplet.m_PriceItemID == 0)
			{
				if (flag)
				{
					this.SetInappPurchasePrice(this.m_cNKMShopItemTemplet, realPrice * this.m_iBuyCount, true, this.m_cNKMShopItemTemplet.m_Price);
				}
				else
				{
					this.SetInappPurchasePrice(this.m_cNKMShopItemTemplet, this.m_cNKMShopItemTemplet.m_Price, false, 0);
				}
			}
			else if (flag)
			{
				this.SetPrice(this.m_cNKMShopItemTemplet.m_PriceItemID, realPrice * this.m_iBuyCount, true, this.m_cNKMShopItemTemplet.m_Price);
			}
			else
			{
				this.SetPrice(this.m_cNKMShopItemTemplet.m_PriceItemID, realPrice * this.m_iBuyCount, false, 0);
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_SHOP_BUY_CONFIRM_BADGE_DISCOUNT_RATE, flag);
			NKCUtil.SetLabelText(this.m_DISCOUNTRATE_TEXT, string.Format("-{0}%", (int)this.m_cNKMShopItemTemplet.m_DiscountRate));
			bool bValue = false;
			if (flag && NKCSynchronizedTime.IsEventTime(this.m_cNKMShopItemTemplet.discountIntervalId, this.m_cNKMShopItemTemplet.DiscountStartDateUtc, this.m_cNKMShopItemTemplet.DiscountEndDateUtc) && this.m_cNKMShopItemTemplet.HasDiscountDateLimit)
			{
				bValue = true;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_SHOP_BUY_CONFIRM_BADGE_DISCOUNT_TIME, bValue);
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				if (!nkmuserData.CheckPrice(realPrice, this.m_cNKMShopItemTemplet.m_PriceItemID))
				{
					NKCUtil.SetLabelTextColor(this.m_NKM_UI_POPUP_ITEM_BOX_PRICE_TEXT, Color.red);
					NKCUtil.SetLabelTextColor(this.m_BUY_COUNT_TEXT, Color.red);
					return;
				}
				if (this.m_iBuyCount > 1)
				{
					NKCUtil.SetLabelTextColor(this.m_NKM_UI_POPUP_ITEM_BOX_PRICE_TEXT, new Color(1f, 0.8117647f, 0.23137255f));
					NKCUtil.SetLabelTextColor(this.m_BUY_COUNT_TEXT, new Color(1f, 0.8117647f, 0.23137255f));
					return;
				}
				NKCUtil.SetLabelTextColor(this.m_NKM_UI_POPUP_ITEM_BOX_PRICE_TEXT, Color.white);
				NKCUtil.SetLabelTextColor(this.m_BUY_COUNT_TEXT, Color.white);
			}
		}

		// Token: 0x06007C11 RID: 31761 RVA: 0x002969D8 File Offset: 0x00294BD8
		private void SetPrice(int priceItemID, int Price, bool bSale = false, int oldPrice = 0)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_ITEM_BOX_Discountline, bSale);
			if (bSale)
			{
				NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_ITEM_BOX_BEFORE, oldPrice.ToString());
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_ITEM_BOX_PRICE_ICON, Price > 0);
			Sprite orLoadMiscItemSmallIcon = NKCResourceUtility.GetOrLoadMiscItemSmallIcon(priceItemID);
			NKCUtil.SetImageSprite(this.m_NKM_UI_POPUP_ITEM_BOX_PRICE_ICON, orLoadMiscItemSmallIcon, true);
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_ITEM_BOX_PRICE_TEXT, (Price > 0) ? Price.ToString() : NKCUtilString.GET_STRING_SHOP_FREE);
		}

		// Token: 0x06007C12 RID: 31762 RVA: 0x00296A48 File Offset: 0x00294C48
		private void SetInappPurchasePrice(ShopItemTemplet cShopItemTemplet, int price, bool bSale = false, int oldPrice = 0)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_ITEM_BOX_Discountline, bSale);
			if (bSale)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_ITEM_BOX_PRICE_ICON, false);
				NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_ITEM_BOX_BEFORE, NKCUtilString.GetInAppPurchasePriceString(oldPrice, cShopItemTemplet.m_ProductID));
				NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_ITEM_BOX_PRICE_TEXT, NKCPublisherModule.InAppPurchase.GetLocalPriceString(cShopItemTemplet.m_MarketID, cShopItemTemplet.m_ProductID));
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_ITEM_BOX_PRICE_ICON, false);
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_ITEM_BOX_PRICE_TEXT, NKCPublisherModule.InAppPurchase.GetLocalPriceString(cShopItemTemplet.m_MarketID, cShopItemTemplet.m_ProductID));
		}

		// Token: 0x06007C13 RID: 31763 RVA: 0x00296ADC File Offset: 0x00294CDC
		private void ChangeValue(bool bPlus)
		{
			if (this.m_cNKMShopItemTemplet == null)
			{
				return;
			}
			if (this.m_bWasHold)
			{
				this.m_bWasHold = false;
				return;
			}
			if (bPlus)
			{
				this.m_iBuyCount++;
			}
			else
			{
				this.m_iBuyCount--;
				if (this.m_iBuyCount <= 0 && this.m_bUseMinusToMax)
				{
					this.m_iBuyCount = this.m_iMaxBuyCount;
				}
			}
			this.m_iBuyCount = Mathf.Clamp(this.m_iBuyCount, 1, this.m_iMaxBuyCount);
			this.UpdateSlot();
			this.UpdatePriceInfo();
		}

		// Token: 0x06007C14 RID: 31764 RVA: 0x00296B64 File Offset: 0x00294D64
		private void UpdateSlot()
		{
			if (this.m_cNKMShopItemTemplet == null)
			{
				return;
			}
			NKCUtil.SetLabelText(this.m_BUY_COUNT_TEXT, this.m_iBuyCount.ToString());
			bool bShowNumber = this.IsCountVisible(this.m_cNKMShopItemTemplet);
			bool bFirstBuy = NKCShopManager.IsFirstBuy(this.m_cNKMShopItemTemplet.m_ProductID, NKCScenManager.CurrentUserData());
			NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeShopItemData(this.m_cNKMShopItemTemplet, bFirstBuy);
			slotData.Count *= (long)this.m_iBuyCount;
			this.m_PackageIcon.SetData(slotData, false, bShowNumber, false, null);
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(this.m_cNKMShopItemTemplet.m_ItemID);
			if (itemMiscTempletByID == null || (itemMiscTempletByID.IsPackageItem && itemMiscTempletByID.m_RewardGroupID == 0))
			{
				Debug.LogError("no rewardgroup! ID : " + this.m_cNKMShopItemTemplet.m_ItemID.ToString());
				return;
			}
			List<NKMRandomBoxItemTemplet> randomBoxItemTempletList = NKCRandomBoxManager.GetRandomBoxItemTempletList(itemMiscTempletByID.m_RewardGroupID);
			if (itemMiscTempletByID.m_RewardGroupID != 0 && randomBoxItemTempletList == null)
			{
				Debug.LogError("rewardgroup null! ID : " + itemMiscTempletByID.m_RewardGroupID.ToString());
				return;
			}
			int num = 0;
			if (randomBoxItemTempletList != null)
			{
				num += randomBoxItemTempletList.Count;
			}
			if (itemMiscTempletByID.CustomPackageTemplets != null)
			{
				num += itemMiscTempletByID.CustomPackageTemplets.Count;
			}
			this.SetSlotCount(num);
			if (this.m_lstItemSlot.Count > 0)
			{
				int num2 = 0;
				if (randomBoxItemTempletList != null)
				{
					int num3 = 0;
					while (num3 < randomBoxItemTempletList.Count && num2 < this.m_lstItemSlot.Count)
					{
						NKCUISlot nkcuislot = this.m_lstItemSlot[num2];
						num2++;
						NKMRandomBoxItemTemplet nkmrandomBoxItemTemplet = randomBoxItemTempletList[num3];
						NKCUtil.SetGameobjectActive(nkcuislot, true);
						NKCUISlot.SlotData slotData2 = NKCUISlot.SlotData.MakeRewardTypeData(nkmrandomBoxItemTemplet.m_reward_type, nkmrandomBoxItemTemplet.m_RewardID, nkmrandomBoxItemTemplet.TotalQuantity_Max * this.m_iBuyCount, 0);
						nkcuislot.TurnOffExtraUI();
						nkcuislot.SetData(slotData2, false, this.IsCountVisible(slotData2), false, null);
						NKCUISlot nkcuislot2 = nkcuislot;
						NKCUISlot.SlotClickType[] array = new NKCUISlot.SlotClickType[3];
						array[0] = NKCUISlot.SlotClickType.RatioList;
						array[1] = NKCUISlot.SlotClickType.ChoiceList;
						nkcuislot2.SetOnClickAction(array);
						NKCShopManager.ShowShopItemCashCount(nkcuislot, slotData2, nkmrandomBoxItemTemplet.FreeQuantity_Max * this.m_iBuyCount, nkmrandomBoxItemTemplet.PaidQuantity_Max * this.m_iBuyCount);
						num3++;
					}
				}
				if (itemMiscTempletByID.CustomPackageTemplets != null)
				{
					int num4 = 0;
					while (num4 < itemMiscTempletByID.CustomPackageTemplets.Count && num2 < this.m_lstItemSlot.Count)
					{
						NKCUISlot nkcuislot3 = this.m_lstItemSlot[num2];
						num2++;
						NKCUtil.SetGameobjectActive(nkcuislot3, true);
						NKMCustomPackageElement nkmcustomPackageElement = null;
						if (this.m_lstSelection != null && num4 < this.m_lstSelection.Count)
						{
							nkmcustomPackageElement = itemMiscTempletByID.CustomPackageTemplets[num4].Get(this.m_lstSelection[num4]);
						}
						if (nkmcustomPackageElement != null)
						{
							NKCUISlot.SlotData slotData3 = NKCUISlot.SlotData.MakeRewardTypeData(nkmcustomPackageElement.RewardType, nkmcustomPackageElement.RewardId, nkmcustomPackageElement.TotalRewardCount * this.m_iBuyCount, 0);
							nkcuislot3.SetData(slotData3, false, this.IsCountVisible(slotData3), false, null);
							NKCUISlot nkcuislot4 = nkcuislot3;
							NKCUISlot.SlotClickType[] array2 = new NKCUISlot.SlotClickType[3];
							array2[0] = NKCUISlot.SlotClickType.RatioList;
							array2[1] = NKCUISlot.SlotClickType.ChoiceList;
							nkcuislot4.SetOnClickAction(array2);
							nkcuislot3.SetShowArrowBGText(true);
							nkcuislot3.SetArrowBGText(NKCStringTable.GetString("SI_DP_SHOP_SLOT_CHOICE", false), new Color(0.6666667f, 0.03137255f, 0.03137255f));
							if (NKCShopManager.WillOverflowOnGain(nkmcustomPackageElement.RewardType, nkmcustomPackageElement.RewardId, nkmcustomPackageElement.TotalRewardCount * this.m_iBuyCount))
							{
								nkcuislot3.SetHaveCountString(true, NKCStringTable.GetString("SI_DP_ICON_SLOT_ALREADY_HAVE", false));
							}
							else if (NKCShopManager.IsCustomPackageSelectionHasDuplicate(itemMiscTempletByID, num4, this.m_lstSelection, false))
							{
								nkcuislot3.SetHaveCountString(true, NKCStringTable.GetString("SI_DP_SHOP_CUSTOM_DUPLICATE", false));
							}
							else
							{
								nkcuislot3.SetHaveCountString(false, null);
							}
						}
						else
						{
							nkcuislot3.SetEmpty(null);
						}
						num4++;
					}
				}
				for (int i = num2; i < this.m_lstItemSlot.Count; i++)
				{
					NKCUtil.SetGameobjectActive(this.m_lstItemSlot[i], false);
				}
			}
		}

		// Token: 0x06007C15 RID: 31765 RVA: 0x00296F30 File Offset: 0x00295130
		private void OnPolicy()
		{
			NKCPublisherModule.InAppPurchase.OpenPolicy(null);
		}

		// Token: 0x06007C16 RID: 31766 RVA: 0x00296F3D File Offset: 0x0029513D
		private void OnBtnLinkedItem()
		{
			if (this.m_cNKMShopItemTemplet != null)
			{
				NKCPopupShopLinkPreview.Instance.Open(this.m_cNKMShopItemTemplet.m_ProductID);
			}
		}

		// Token: 0x06007C17 RID: 31767 RVA: 0x00296F5C File Offset: 0x0029515C
		private void OnBtnJPNPaymentLaw()
		{
			NKCPublisherModule.InAppPurchase.OpenPaymentLaw(null);
		}

		// Token: 0x06007C18 RID: 31768 RVA: 0x00296F69 File Offset: 0x00295169
		private void OnBtnJPNCommercialLaw()
		{
			NKCPublisherModule.InAppPurchase.OpenCommercialLaw(null);
		}

		// Token: 0x170014AF RID: 5295
		// (get) Token: 0x06007C1A RID: 31770 RVA: 0x00296F7F File Offset: 0x0029517F
		// (set) Token: 0x06007C19 RID: 31769 RVA: 0x00296F76 File Offset: 0x00295176
		private int m_iMaxBuyCount { get; set; }

		// Token: 0x06007C1B RID: 31771 RVA: 0x00296F87 File Offset: 0x00295187
		private void OnMinusDown(PointerEventData eventData)
		{
			this.m_iChangeValue = -1;
			this.m_bPress = true;
			this.m_fDelay = 0.35f;
			this.m_fHoldTime = 0f;
			this.m_bWasHold = false;
		}

		// Token: 0x06007C1C RID: 31772 RVA: 0x00296FB4 File Offset: 0x002951B4
		private void OnPlusDown(PointerEventData eventData)
		{
			this.m_iChangeValue = 1;
			this.m_bPress = true;
			this.m_fDelay = 0.35f;
			this.m_fHoldTime = 0f;
			this.m_bWasHold = false;
		}

		// Token: 0x06007C1D RID: 31773 RVA: 0x00296FE1 File Offset: 0x002951E1
		private void OnButtonUp()
		{
			this.m_iChangeValue = 0;
			this.m_fDelay = 0.35f;
			this.m_bPress = false;
		}

		// Token: 0x06007C1E RID: 31774 RVA: 0x00296FFC File Offset: 0x002951FC
		private void OnUpdateButtonHold()
		{
			if (!this.m_bPress)
			{
				return;
			}
			this.m_fHoldTime += Time.deltaTime;
			if (this.m_fHoldTime >= this.m_fDelay)
			{
				this.m_fHoldTime = 0f;
				this.m_fDelay *= 0.8f;
				this.m_fDelay = Mathf.Clamp(this.m_fDelay, 0.01f, 0.35f);
				this.m_iBuyCount += this.m_iChangeValue;
				this.m_bWasHold = true;
				if (this.m_iChangeValue < 0 && this.m_iBuyCount < 1)
				{
					this.m_iBuyCount = 1;
					this.m_bPress = false;
				}
				if (this.m_iChangeValue > 0 && this.m_iBuyCount > this.m_iMaxBuyCount)
				{
					this.m_iBuyCount = this.m_iMaxBuyCount;
					this.m_bPress = false;
				}
				this.UpdateSlot();
				this.UpdatePriceInfo();
			}
		}

		// Token: 0x06007C1F RID: 31775 RVA: 0x002970E0 File Offset: 0x002952E0
		public void OnScroll(PointerEventData eventData)
		{
			if (eventData.scrollDelta.y < 0f)
			{
				if (this.m_iBuyCount > 0)
				{
					this.m_iBuyCount--;
				}
			}
			else if (eventData.scrollDelta.y > 0f && this.m_iBuyCount < this.m_iMaxBuyCount)
			{
				this.m_iBuyCount++;
			}
			if (this.m_iMaxBuyCount == 0)
			{
				this.m_iMaxBuyCount = 1;
			}
			int iBuyCount = Mathf.Clamp(this.m_iBuyCount, 1, this.m_iMaxBuyCount);
			this.m_iBuyCount = iBuyCount;
			this.UpdateSlot();
			this.UpdatePriceInfo();
		}

		// Token: 0x040068B0 RID: 26800
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_shop";

		// Token: 0x040068B1 RID: 26801
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_SHOP_BUY_PACKAGE_CONFIRM";

		// Token: 0x040068B2 RID: 26802
		private static NKCPopupShopPackageConfirm m_Instance;

		// Token: 0x040068B3 RID: 26803
		[Header("UI")]
		public Text m_lbTitle;

		// Token: 0x040068B4 RID: 26804
		public Text m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_INFO_TITLE_TEXT;

		// Token: 0x040068B5 RID: 26805
		public Text m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_INFO_DESC_TEXT;

		// Token: 0x040068B6 RID: 26806
		[Space]
		public NKCUISlot m_PackageIcon;

		// Token: 0x040068B7 RID: 26807
		public RectTransform m_rt_Package_Contents;

		// Token: 0x040068B8 RID: 26808
		[Header("구매횟수 제한")]
		public GameObject m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_COUNT;

		// Token: 0x040068B9 RID: 26809
		public Text m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_COUNT_TEXT;

		// Token: 0x040068BA RID: 26810
		[Header("갯수 변경")]
		public GameObject m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_BUY_COUNT;

		// Token: 0x040068BB RID: 26811
		public NKCUIComStateButton m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_BUY_COUNT_MINUS;

		// Token: 0x040068BC RID: 26812
		public NKCUIComStateButton m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_BUY_COUNT_PLUS;

		// Token: 0x040068BD RID: 26813
		public Text m_BUY_COUNT_TEXT;

		// Token: 0x040068BE RID: 26814
		[Header("사용 재화")]
		public Image m_NKM_UI_POPUP_ITEM_BOX_PRICE_ICON;

		// Token: 0x040068BF RID: 26815
		public Text m_NKM_UI_POPUP_ITEM_BOX_PRICE_TEXT;

		// Token: 0x040068C0 RID: 26816
		public GameObject m_NKM_UI_POPUP_ITEM_BOX_Discountline;

		// Token: 0x040068C1 RID: 26817
		public Text m_NKM_UI_POPUP_ITEM_BOX_BEFORE;

		// Token: 0x040068C2 RID: 26818
		[Header("버튼")]
		public NKCUIComStateButton m_NKM_UI_POPUP_CANCLE_BUTTON;

		// Token: 0x040068C3 RID: 26819
		public NKCUIComStateButton m_NKM_UI_POPUP_BUY_BUTTON;

		// Token: 0x040068C4 RID: 26820
		public NKCUIComStateButton m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_BG;

		// Token: 0x040068C5 RID: 26821
		public NKCUIComStateButton m_csbtnOK;

		// Token: 0x040068C6 RID: 26822
		[Header("할인율")]
		public GameObject m_NKM_UI_SHOP_BUY_CONFIRM_BADGE_DISCOUNT_RATE;

		// Token: 0x040068C7 RID: 26823
		public Text m_DISCOUNTRATE_TEXT;

		// Token: 0x040068C8 RID: 26824
		[Header("기간 한정")]
		public GameObject m_NKM_UI_SHOP_BUY_CONFIRM_BADGE_DISCOUNT_TIME;

		// Token: 0x040068C9 RID: 26825
		public Text m_DISCOUNT_TIME_TEXT1;

		// Token: 0x040068CA RID: 26826
		public Text m_DISCOUNT_TIME_TEXT2;

		// Token: 0x040068CB RID: 26827
		[Header("청약 철회")]
		public GameObject m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_POLICY;

		// Token: 0x040068CC RID: 26828
		public NKCUIComStateButton m_NKM_UI_POPUP_SHOP_BUY_CONFIRM_POLICY_BUTTON;

		// Token: 0x040068CD RID: 26829
		[Header("연계 상품")]
		public NKCUIComStateButton m_csbtnLinkedItem;

		// Token: 0x040068CE RID: 26830
		[Header("일본법무대응")]
		public GameObject m_JPN_BTN;

		// Token: 0x040068CF RID: 26831
		public GameObject m_JPN_POLICY;

		// Token: 0x040068D0 RID: 26832
		public NKCUIComStateButton m_csbtnJPNPaymentLaw;

		// Token: 0x040068D1 RID: 26833
		public NKCUIComStateButton m_csbtnJPNCommercialLaw;

		// Token: 0x040068D2 RID: 26834
		[Space]
		public NKCUISlot m_pfbSlot;

		// Token: 0x040068D3 RID: 26835
		private NKCUIShop.OnProductBuyDelegate dOnOKButton;

		// Token: 0x040068D4 RID: 26836
		private ShopItemTemplet m_cNKMShopItemTemplet;

		// Token: 0x040068D5 RID: 26837
		private List<int> m_lstSelection;

		// Token: 0x040068D6 RID: 26838
		private List<NKCUISlot> m_lstItemSlot = new List<NKCUISlot>();

		// Token: 0x040068D7 RID: 26839
		private float m_deltaTime;

		// Token: 0x040068D8 RID: 26840
		public const float PRESS_GAP_MAX = 0.35f;

		// Token: 0x040068D9 RID: 26841
		public const float PRESS_GAP_MIN = 0.01f;

		// Token: 0x040068DA RID: 26842
		public const float DAMPING = 0.8f;

		// Token: 0x040068DB RID: 26843
		private float m_fDelay = 0.5f;

		// Token: 0x040068DC RID: 26844
		private float m_fHoldTime;

		// Token: 0x040068DD RID: 26845
		private int m_iChangeValue;

		// Token: 0x040068DE RID: 26846
		private bool m_bPress;

		// Token: 0x040068DF RID: 26847
		private bool m_bWasHold;

		// Token: 0x040068E0 RID: 26848
		private int m_iBuyCount;

		// Token: 0x040068E2 RID: 26850
		private bool m_bUseMinusToMax;

		// Token: 0x02001841 RID: 6209
		private enum Mode
		{
			// Token: 0x0400A871 RID: 43121
			Buy,
			// Token: 0x0400A872 RID: 43122
			Preview
		}
	}
}
