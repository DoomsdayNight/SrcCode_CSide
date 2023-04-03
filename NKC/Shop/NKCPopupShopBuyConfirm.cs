using System;
using System.Collections.Generic;
using NKC.Publisher;
using NKC.UI.Component.Office;
using NKM;
using NKM.Shop;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Shop
{
	// Token: 0x02000AC9 RID: 2761
	public class NKCPopupShopBuyConfirm : NKCUIBase, IScrollHandler, IEventSystemHandler
	{
		// Token: 0x17001491 RID: 5265
		// (get) Token: 0x06007B8A RID: 31626 RVA: 0x00292EEC File Offset: 0x002910EC
		public static NKCPopupShopBuyConfirm Instance
		{
			get
			{
				if (NKCPopupShopBuyConfirm.m_Instance == null)
				{
					NKCPopupShopBuyConfirm.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupShopBuyConfirm>("ab_ui_nkm_ui_shop", "NKM_UI_POPUP_SHOP_BUY_CONFIRM", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupShopBuyConfirm.CleanupInstance)).GetInstance<NKCPopupShopBuyConfirm>();
					NKCPopupShopBuyConfirm.m_Instance.InitUI();
				}
				return NKCPopupShopBuyConfirm.m_Instance;
			}
		}

		// Token: 0x06007B8B RID: 31627 RVA: 0x00292F3B File Offset: 0x0029113B
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupShopBuyConfirm.m_Instance != null && NKCPopupShopBuyConfirm.m_Instance.IsOpen)
			{
				NKCPopupShopBuyConfirm.m_Instance.Close();
			}
		}

		// Token: 0x06007B8C RID: 31628 RVA: 0x00292F60 File Offset: 0x00291160
		private static void CleanupInstance()
		{
			NKCPopupShopBuyConfirm.m_Instance = null;
		}

		// Token: 0x17001492 RID: 5266
		// (get) Token: 0x06007B8D RID: 31629 RVA: 0x00292F68 File Offset: 0x00291168
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001493 RID: 5267
		// (get) Token: 0x06007B8E RID: 31630 RVA: 0x00292F6B File Offset: 0x0029116B
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_POPUP_ITEM_BOX;
			}
		}

		// Token: 0x17001494 RID: 5268
		// (get) Token: 0x06007B8F RID: 31631 RVA: 0x00292F72 File Offset: 0x00291172
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.ResourceOnly;
			}
		}

		// Token: 0x17001495 RID: 5269
		// (get) Token: 0x06007B91 RID: 31633 RVA: 0x00292F7E File Offset: 0x0029117E
		// (set) Token: 0x06007B90 RID: 31632 RVA: 0x00292F75 File Offset: 0x00291175
		private int m_iMaxBuyCount { get; set; }

		// Token: 0x17001496 RID: 5270
		// (get) Token: 0x06007B92 RID: 31634 RVA: 0x00292F86 File Offset: 0x00291186
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return NKCShopManager.GetUpsideMenuItemList(this.m_cNKMShopItemTemplet);
			}
		}

		// Token: 0x06007B93 RID: 31635 RVA: 0x00292F94 File Offset: 0x00291194
		public void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			this.m_NKCUIItemSlot.Init();
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData eventData)
			{
				base.Close();
			});
			EventTrigger eventTrigger = this.m_objBG.GetComponent<EventTrigger>();
			if (eventTrigger == null)
			{
				eventTrigger = this.m_objBG.AddComponent<EventTrigger>();
			}
			eventTrigger.triggers.Add(entry);
			this.m_btnCancel.PointerClick.RemoveAllListeners();
			this.m_btnCancel.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_btnBuy.m_bGetCallbackWhileLocked = true;
			this.m_btnBuy.PointerClick.RemoveAllListeners();
			this.m_btnBuy.PointerClick.AddListener(new UnityAction(this.OnOK));
			NKCUtil.SetHotkey(this.m_btnBuy, HotkeyEventType.Confirm, null, false);
			this.m_btnBuyLocked.m_bGetCallbackWhileLocked = true;
			this.m_btnBuyLocked.PointerClick.RemoveAllListeners();
			this.m_btnBuyLocked.PointerClick.AddListener(new UnityAction(this.OnOK));
			this.m_btnPolicy.PointerClick.RemoveAllListeners();
			this.m_btnPolicy.PointerClick.AddListener(new UnityAction(this.OnPolicy));
			NKCUIComStateButton btnBuyCountMinus = this.m_btnBuyCountMinus;
			if (btnBuyCountMinus != null)
			{
				btnBuyCountMinus.PointerDown.RemoveAllListeners();
			}
			NKCUIComStateButton btnBuyCountMinus2 = this.m_btnBuyCountMinus;
			if (btnBuyCountMinus2 != null)
			{
				btnBuyCountMinus2.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnMinusDown));
			}
			NKCUIComStateButton btnBuyCountMinus3 = this.m_btnBuyCountMinus;
			if (btnBuyCountMinus3 != null)
			{
				btnBuyCountMinus3.PointerUp.RemoveAllListeners();
			}
			NKCUIComStateButton btnBuyCountMinus4 = this.m_btnBuyCountMinus;
			if (btnBuyCountMinus4 != null)
			{
				btnBuyCountMinus4.PointerUp.AddListener(new UnityAction(this.OnButtonUp));
			}
			NKCUtil.SetHotkey(this.m_btnBuyCountMinus, HotkeyEventType.Minus, this, true);
			NKCUIComStateButton btnBuyCountPlus = this.m_btnBuyCountPlus;
			if (btnBuyCountPlus != null)
			{
				btnBuyCountPlus.PointerDown.RemoveAllListeners();
			}
			NKCUIComStateButton btnBuyCountPlus2 = this.m_btnBuyCountPlus;
			if (btnBuyCountPlus2 != null)
			{
				btnBuyCountPlus2.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnPlusDown));
			}
			NKCUIComStateButton btnBuyCountPlus3 = this.m_btnBuyCountPlus;
			if (btnBuyCountPlus3 != null)
			{
				btnBuyCountPlus3.PointerUp.RemoveAllListeners();
			}
			NKCUIComStateButton btnBuyCountPlus4 = this.m_btnBuyCountPlus;
			if (btnBuyCountPlus4 != null)
			{
				btnBuyCountPlus4.PointerUp.AddListener(new UnityAction(this.OnButtonUp));
			}
			NKCUtil.SetHotkey(this.m_btnBuyCountPlus, HotkeyEventType.Plus, this, true);
			NKCUtil.SetBindFunction(this.m_btnBuyCountMinus, delegate()
			{
				this.OnChangeCount(false);
			});
			NKCUtil.SetBindFunction(this.m_btnBuyCountPlus, delegate()
			{
				this.OnChangeCount(true);
			});
			NKCUtil.SetBindFunction(this.m_csbtnLinkedItem, new UnityAction(this.OnBtnLinkedItem));
			NKCUtil.SetGameobjectActive(this.m_JPN_BTN, false);
			NKCUtil.SetGameobjectActive(this.m_JPN_POLICY, false);
			if (NKCPublisherModule.InAppPurchase.ShowJPNPaymentPolicy())
			{
				NKCUtil.SetBindFunction(this.m_csbtnJPNPaymentLaw, new UnityAction(this.OnBtnJPNPaymentLaw));
				NKCUtil.SetBindFunction(this.m_csbtnJPNCommercialLaw, new UnityAction(this.OnBtnJPNCommercialLaw));
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007B94 RID: 31636 RVA: 0x00293288 File Offset: 0x00291488
		private void UpdateByPriceItemId()
		{
			if (this.m_cNKMShopItemTemplet.m_PriceItemID == 0)
			{
				NKCPopupShopBuyConfirm.Instance.m_priceTag.SetData(this.m_cNKMShopItemTemplet, false, false);
				NKCPopupShopBuyConfirm.Instance.m_priceTag.SetLabelTextColor(Color.white);
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData != null)
				{
					int realPrice = nkmuserData.m_ShopData.GetRealPrice(this.m_cNKMShopItemTemplet, 1, false);
					NKCUtil.SetGameobjectActive(this.m_objSalePriceRoot, this.m_cNKMShopItemTemplet.m_Price > realPrice);
					if (this.m_cNKMShopItemTemplet.m_Price > realPrice)
					{
						NKCResourceUtility.GetOrLoadMiscItemSmallIcon(this.m_cNKMShopItemTemplet.m_PriceItemID);
						NKCUtil.SetLabelText(this.m_lbOldPrice, this.m_cNKMShopItemTemplet.m_Price.ToString());
					}
					NKCUtil.SetGameobjectActive(this.m_objPolicyParent, NKCShopManager.IsShowPurchasePolicy());
					return;
				}
			}
			else
			{
				this.UpdatePriceInfo();
				NKCUtil.SetGameobjectActive(this.m_objPolicyParent, false);
			}
		}

		// Token: 0x06007B95 RID: 31637 RVA: 0x00293368 File Offset: 0x00291568
		public void Open(ShopItemTemplet productTemplet, NKCUIShop.OnProductBuyDelegate onOkButton)
		{
			bool bShowCount = productTemplet != null && this.IsCountVisible(productTemplet);
			bool bFirstBuy = productTemplet != null && NKCShopManager.IsFirstBuy(productTemplet.m_ProductID, NKCScenManager.CurrentUserData());
			this.m_iBuyCount = 1;
			this.m_cNKMShopItemTemplet = productTemplet;
			NKCUtil.SetGameobjectActive(this.m_objRemainCount, this.m_cNKMShopItemTemplet.resetType > SHOP_RESET_TYPE.Unlimited);
			NKCUtil.SetLabelText(this.m_lbItemRemainCount, NKCShopManager.GetBuyCountString(this.m_cNKMShopItemTemplet.resetType, NKCShopManager.GetBuyCountLeft(this.m_cNKMShopItemTemplet.m_ProductID), this.m_cNKMShopItemTemplet.m_QuantityLimit, true));
			NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeShopItemData(this.m_cNKMShopItemTemplet, bFirstBuy);
			this.SetData(slotData, onOkButton, bShowCount);
			NKCShopManager.ShowShopItemCashCount(this.m_NKCUIItemSlot, slotData, this.m_cNKMShopItemTemplet.m_FreeValue, this.m_cNKMShopItemTemplet.m_PaidValue);
			NKCPopupShopBuyConfirm.Instance.m_lbItemDesc.text = NKCUtilString.GetShopDescriptionText(this.m_cNKMShopItemTemplet.GetItemDescPopup(), bFirstBuy);
			this.CheckDiscount(this.m_cNKMShopItemTemplet);
			this.UpdateByPriceItemId();
			int buyCountLeft = NKCShopManager.GetBuyCountLeft(this.m_cNKMShopItemTemplet.m_ProductID);
			if (this.m_cNKMShopItemTemplet.m_PriceItemID != 0)
			{
				if (this.m_cNKMShopItemTemplet.m_Price > 0)
				{
					int realPrice = NKCScenManager.CurrentUserData().m_ShopData.GetRealPrice(this.m_cNKMShopItemTemplet, 1, false);
					this.m_iMaxBuyCount = (int)(NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(this.m_cNKMShopItemTemplet.m_PriceItemID) / (long)realPrice);
					if (this.m_cNKMShopItemTemplet.m_QuantityLimit > 0)
					{
						this.m_iMaxBuyCount = Math.Min(buyCountLeft, this.m_iMaxBuyCount);
					}
				}
				else if (this.m_cNKMShopItemTemplet.m_QuantityLimit > 0)
				{
					this.m_iMaxBuyCount = Math.Min(buyCountLeft, this.m_iMaxBuyCount);
				}
				else
				{
					this.m_iMaxBuyCount = 1;
				}
			}
			if (buyCountLeft >= 0)
			{
				this.m_iMaxBuyCount = Math.Min(this.m_iMaxBuyCount, buyCountLeft);
			}
			if (this.m_iMaxBuyCount < 1)
			{
				this.m_iMaxBuyCount = 1;
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
			List<ShopItemTemplet> linkedItem = NKCShopManager.GetLinkedItem(this.m_cNKMShopItemTemplet.m_ProductID);
			NKCUtil.SetGameobjectActive(this.m_csbtnLinkedItem, linkedItem != null && linkedItem.Count > 0);
			bool flag = NKCPublisherModule.InAppPurchase.IsJPNPaymentPolicy();
			bool flag2 = this.m_cNKMShopItemTemplet.m_PriceItemID == 0;
			bool flag3 = NKCUtil.IsJPNPolicyRelatedItem(productTemplet.m_PriceItemID);
			if (flag)
			{
				NKCUtil.SetGameobjectActive(this.m_JPN_BTN, flag2);
				NKCUtil.SetGameobjectActive(this.m_JPN_POLICY, !flag2 && flag3);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_JPN_BTN, false);
			NKCUtil.SetGameobjectActive(this.m_JPN_POLICY, false);
		}

		// Token: 0x06007B96 RID: 31638 RVA: 0x002935FD File Offset: 0x002917FD
		private void SetData(NKCUISlot.SlotData data, NKCUIShop.OnProductBuyDelegate onOkButton = null, bool bShowCount = false)
		{
			NKCPopupShopBuyConfirm.Instance.CommonOpenProcess(data, bShowCount);
			this.SetButton(onOkButton);
		}

		// Token: 0x06007B97 RID: 31639 RVA: 0x00293614 File Offset: 0x00291814
		private void CommonOpenProcess(NKCUISlot.SlotData data, bool bShowNumber)
		{
			base.gameObject.SetActive(true);
			bool flag = data.eType == NKCUISlot.eSlotMode.Equip || data.eType == NKCUISlot.eSlotMode.EquipCount;
			if (flag)
			{
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(data.ID);
				if (equipTemplet != null && (equipTemplet.m_EquipUnitStyleType == NKM_UNIT_STYLE_TYPE.NUST_ENCHANT || equipTemplet.m_ItemEquipPosition == ITEM_EQUIP_POSITION.IEP_ENCHANT))
				{
					flag = false;
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objEquipSlot, flag);
			NKCUtil.SetGameobjectActive(this.m_objItemSlot, !flag);
			if (flag)
			{
				this.m_NKCUIInvenEquipSlot.SetData(NKCEquipSortSystem.MakeTempEquipData(data.ID, data.GroupID, false), false, false);
			}
			else if (this.m_NKCUIItemSlot != null)
			{
				this.m_NKCUIItemSlot.SetData(data, false, bShowNumber, false, null);
				this.m_NKCUIItemSlot.SetOnClickAction(new NKCUISlot.SlotClickType[]
				{
					NKCUISlot.SlotClickType.RatioList,
					NKCUISlot.SlotClickType.ChoiceList,
					NKCUISlot.SlotClickType.BoxList
				});
			}
			if (data.eType == NKCUISlot.eSlotMode.ItemMisc)
			{
				if (this.m_comInteriorDetail != null)
				{
					this.m_comInteriorDetail.SetData(data.ID);
				}
				if (this.m_comInteriorInteractionBubble != null)
				{
					this.m_comInteriorInteractionBubble.SetData(data.ID);
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_comInteriorDetail, false);
				NKCUtil.SetGameobjectActive(this.m_comInteriorInteractionBubble, false);
			}
			this.SetTextFromSlotdata(data);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			NKCUtil.SetGameobjectActive(this.m_objBuyCount, this.CanChangeBuyCount(data, flag));
			this.SetIntervalItem(data);
			base.UIOpened(true);
		}

		// Token: 0x06007B98 RID: 31640 RVA: 0x00293778 File Offset: 0x00291978
		private void CheckDiscount(ShopItemTemplet productTemplet)
		{
			bool flag = false;
			if (productTemplet.m_DiscountRate > 0f && NKCSynchronizedTime.IsEventTime(productTemplet.discountIntervalId, productTemplet.DiscountStartDateUtc, productTemplet.DiscountEndDateUtc) && productTemplet.HasDiscountDateLimit)
			{
				flag = true;
				this.m_tEndDateDiscountTime = productTemplet.DiscountEndDateUtc;
				this.UpdateDiscountTime(this.m_tEndDateDiscountTime);
			}
			else
			{
				this.m_tEndDateDiscountTime = DateTime.MinValue;
			}
			NKCUtil.SetGameobjectActive(this.m_objDiscountDay, flag);
			if (!productTemplet.HasDiscountDateLimit)
			{
				NKCUtil.SetGameobjectActive(this.m_objDiscountRate, productTemplet.m_DiscountRate > 0f);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objDiscountRate, productTemplet.m_DiscountRate > 0f && flag);
			}
			NKCUtil.SetLabelText(this.m_txtDiscountRate, string.Format("-{0}%", (int)productTemplet.m_DiscountRate));
		}

		// Token: 0x06007B99 RID: 31641 RVA: 0x00293848 File Offset: 0x00291A48
		public void UpdateDiscountTime(DateTime endTime)
		{
			string msg;
			if (NKCSynchronizedTime.IsFinished(endTime))
			{
				msg = NKCUtilString.GET_STRING_QUIT;
			}
			else
			{
				msg = NKCUtilString.GetRemainTimeStringOneParam(endTime);
			}
			NKCUtil.SetLabelText(this.m_txtDiscountDay, msg);
		}

		// Token: 0x06007B9A RID: 31642 RVA: 0x00293878 File Offset: 0x00291A78
		private bool HaveDetail(NKCUISlot.SlotData data)
		{
			if (data.eType == NKCUISlot.eSlotMode.ItemMisc)
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(data.ID);
				if (itemMiscTempletByID != null && itemMiscTempletByID.IsUsable() && (itemMiscTempletByID.IsPackageItem || itemMiscTempletByID.IsRatioOpened()))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007B9B RID: 31643 RVA: 0x002938B8 File Offset: 0x00291AB8
		private bool CanChangeBuyCount(NKCUISlot.SlotData data, bool bShowEquipSlot)
		{
			bool flag = true;
			if (this.m_cNKMShopItemTemplet.m_QuantityLimit > 0)
			{
				flag &= (this.m_cNKMShopItemTemplet.m_QuantityLimit > 1);
			}
			flag &= (this.m_cNKMShopItemTemplet.m_PriceItemID != 0);
			flag &= !this.m_cNKMShopItemTemplet.IsSubscribeItem();
			switch (this.m_cNKMShopItemTemplet.m_ItemType)
			{
			default:
				return false;
			case NKM_REWARD_TYPE.RT_UNIT:
			case NKM_REWARD_TYPE.RT_SHIP:
			case NKM_REWARD_TYPE.RT_MISC:
			case NKM_REWARD_TYPE.RT_USER_EXP:
			case NKM_REWARD_TYPE.RT_MOLD:
			case NKM_REWARD_TYPE.RT_MISSION_POINT:
			case NKM_REWARD_TYPE.RT_OPERATOR:
				break;
			case NKM_REWARD_TYPE.RT_EQUIP:
				if (bShowEquipSlot)
				{
					return false;
				}
				break;
			}
			switch (data.eType)
			{
			case NKCUISlot.eSlotMode.Unit:
			case NKCUISlot.eSlotMode.ItemMisc:
			case NKCUISlot.eSlotMode.Mold:
			case NKCUISlot.eSlotMode.DiveArtifact:
			case NKCUISlot.eSlotMode.Buff:
			case NKCUISlot.eSlotMode.UnitCount:
			case NKCUISlot.eSlotMode.Emoticon:
			case NKCUISlot.eSlotMode.Etc:
				break;
			case NKCUISlot.eSlotMode.Equip:
			case NKCUISlot.eSlotMode.EquipCount:
				if (bShowEquipSlot)
				{
					return false;
				}
				break;
			default:
				return false;
			}
			return flag;
		}

		// Token: 0x06007B9C RID: 31644 RVA: 0x0029399E File Offset: 0x00291B9E
		private void SetButton(NKCUIShop.OnProductBuyDelegate onOkButton1 = null)
		{
			NKCUtil.SetGameobjectActive(this.m_btnCancel, true);
			NKCUtil.SetGameobjectActive(this.m_btnBuy, true);
			NKCUtil.SetGameobjectActive(this.m_btnBuyLocked, false);
			NKCUtil.SetGameobjectActive(this.m_btnPolicy, NKCShopManager.IsShowPurchasePolicyBtn());
			this.dOnOKButton = onOkButton1;
		}

		// Token: 0x06007B9D RID: 31645 RVA: 0x002939DC File Offset: 0x00291BDC
		private bool IsCountVisible(ShopItemTemplet productTemplet)
		{
			switch (productTemplet.m_ItemType)
			{
			case NKM_REWARD_TYPE.RT_NONE:
			case NKM_REWARD_TYPE.RT_UNIT:
			case NKM_REWARD_TYPE.RT_SHIP:
			case NKM_REWARD_TYPE.RT_OPERATOR:
				return false;
			case NKM_REWARD_TYPE.RT_MISC:
			case NKM_REWARD_TYPE.RT_USER_EXP:
			case NKM_REWARD_TYPE.RT_MOLD:
			case NKM_REWARD_TYPE.RT_SKIN:
			case NKM_REWARD_TYPE.RT_MISSION_POINT:
				return true;
			}
			return productTemplet.TotalValue > 1;
		}

		// Token: 0x06007B9E RID: 31646 RVA: 0x00293A40 File Offset: 0x00291C40
		private void SetTextFromSlotdata(NKCUISlot.SlotData data)
		{
			switch (data.eType)
			{
			case NKCUISlot.eSlotMode.Unit:
			case NKCUISlot.eSlotMode.UnitCount:
			{
				NKCUtil.SetGameobjectActive(this.m_objItemInventoryCountParent, true);
				string arg = string.Format("<color=#ffcf3b>{0}</color>", NKCShopManager.OwnedItemCount(data));
				this.m_lbItemInventoryCount.text = string.Format(NKCUtilString.GET_STRING_ITEM_COUNT_ONE_PARAM, arg);
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(data.ID);
				if (unitTempletBase != null)
				{
					this.m_lbItemName.text = unitTempletBase.GetUnitName();
					this.m_lbItemDesc.text = "";
					return;
				}
				Debug.LogError("UnitTemplet Not Found. UnitID : " + data.ID.ToString());
				this.m_lbItemName.text = "";
				this.m_lbItemDesc.text = "";
				return;
			}
			case NKCUISlot.eSlotMode.ItemMisc:
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(data.ID);
				if (itemMiscTempletByID != null)
				{
					this.m_lbItemName.text = itemMiscTempletByID.GetItemName();
					this.m_lbItemDesc.text = itemMiscTempletByID.GetItemDesc();
					NKM_ITEM_MISC_TYPE itemMiscType = itemMiscTempletByID.m_ItemMiscType;
					if (itemMiscType <= NKM_ITEM_MISC_TYPE.IMT_RESOURCE)
					{
						if (itemMiscType != NKM_ITEM_MISC_TYPE.IMT_MISC && itemMiscType != NKM_ITEM_MISC_TYPE.IMT_RESOURCE)
						{
							goto IL_CF;
						}
					}
					else if (itemMiscType != NKM_ITEM_MISC_TYPE.IMT_CHOICE_MISC && itemMiscType != NKM_ITEM_MISC_TYPE.IMT_PIECE)
					{
						goto IL_CF;
					}
					NKCUtil.SetGameobjectActive(this.m_objItemInventoryCountParent, true);
					string arg2 = string.Format("<color=#ffcf3b>{0}</color>", NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(itemMiscTempletByID));
					this.m_lbItemInventoryCount.text = string.Format(NKCUtilString.GET_STRING_ITEM_COUNT_ONE_PARAM, arg2);
					return;
					IL_CF:
					NKCUtil.SetGameobjectActive(this.m_objItemInventoryCountParent, false);
					return;
				}
				Debug.LogError("ItemTemplet Not Found. ItemID : " + data.ID.ToString());
				this.m_lbItemName.text = "";
				this.m_lbItemDesc.text = "";
				NKCUtil.SetGameobjectActive(this.m_objItemInventoryCountParent, false);
				return;
			}
			case NKCUISlot.eSlotMode.Equip:
			case NKCUISlot.eSlotMode.EquipCount:
			{
				NKCUtil.SetGameobjectActive(this.m_objItemInventoryCountParent, false);
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(data.ID);
				if (equipTemplet != null)
				{
					if (data.Count > 1L && data.eType == NKCUISlot.eSlotMode.Equip)
					{
						this.m_lbItemName.text = string.Format("{0} +{1}", NKCUtilString.GetItemEquipNameWithTier(equipTemplet), data.Count);
					}
					else
					{
						this.m_lbItemName.text = NKCUtilString.GetItemEquipNameWithTier(equipTemplet);
					}
					this.m_lbItemDesc.text = equipTemplet.GetItemDesc();
					return;
				}
				Debug.LogError("EquipTemplet Not Found. EquipID : " + data.ID.ToString());
				this.m_lbItemName.text = "";
				this.m_lbItemDesc.text = "";
				return;
			}
			case NKCUISlot.eSlotMode.Skin:
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(data.ID);
				if (skinTemplet != null)
				{
					NKMUnitManager.GetUnitTempletBase(skinTemplet.m_SkinEquipUnitID);
					this.m_lbItemName.text = skinTemplet.GetTitle();
					this.m_lbItemDesc.text = skinTemplet.GetSkinDesc();
					NKCUtil.SetGameobjectActive(this.m_objItemInventoryCountParent, false);
					return;
				}
				return;
			}
			case NKCUISlot.eSlotMode.Mold:
			{
				NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(data.ID);
				if (itemMoldTempletByID != null)
				{
					this.m_lbItemName.text = itemMoldTempletByID.GetItemName();
					this.m_lbItemDesc.text = itemMoldTempletByID.GetItemDesc();
					NKCUtil.SetGameobjectActive(this.m_objItemInventoryCountParent, false);
					return;
				}
				Debug.LogError("NKMItemMoldTemplet Not Found. moldItemID : " + data.ID.ToString());
				this.m_lbItemName.text = "";
				this.m_lbItemDesc.text = "";
				NKCUtil.SetGameobjectActive(this.m_objItemInventoryCountParent, false);
				return;
			}
			case NKCUISlot.eSlotMode.Buff:
			{
				NKMCompanyBuffTemplet companyBuffTemplet = NKMCompanyBuffManager.GetCompanyBuffTemplet(data.ID);
				this.m_lbItemName.text = companyBuffTemplet.GetBuffName();
				this.m_lbItemDesc.text = companyBuffTemplet.GetBuffDescForItemPopup();
				NKCUtil.SetGameobjectActive(this.m_objItemInventoryCountParent, false);
				return;
			}
			case NKCUISlot.eSlotMode.Emoticon:
			{
				NKMEmoticonTemplet nkmemoticonTemplet = NKMEmoticonTemplet.Find(data.ID);
				this.m_lbItemName.text = nkmemoticonTemplet.GetEmoticonName();
				this.m_lbItemDesc.text = nkmemoticonTemplet.GetEmoticonDesc();
				NKCUtil.SetGameobjectActive(this.m_objItemInventoryCountParent, false);
				return;
			}
			}
			Debug.LogError("Undefined type");
			this.m_lbItemName.text = "";
			this.m_lbItemDesc.text = "";
			NKCUtil.SetGameobjectActive(this.m_objItemInventoryCountParent, false);
		}

		// Token: 0x06007B9F RID: 31647 RVA: 0x00293E60 File Offset: 0x00292060
		private void UpdatePriceInfo()
		{
			if (NKCUtil.IsNullObject<ShopItemTemplet>(this.m_cNKMShopItemTemplet, ""))
			{
				return;
			}
			int realPrice = NKCScenManager.CurrentUserData().m_ShopData.GetRealPrice(this.m_cNKMShopItemTemplet, this.m_iBuyCount, false);
			bool flag = NKCPopupShopBuyConfirm.Instance.m_priceTag.SetData(this.m_cNKMShopItemTemplet.m_PriceItemID, realPrice, false, true, true);
			NKCUtil.SetLabelText(this.m_lbBuyCount, this.m_iBuyCount.ToString());
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			int realPrice2 = nkmuserData.m_ShopData.GetRealPrice(this.m_cNKMShopItemTemplet, 1, false);
			NKCUtil.SetGameobjectActive(this.m_objSalePriceRoot, this.m_cNKMShopItemTemplet.m_Price > realPrice2);
			if (this.m_cNKMShopItemTemplet.m_Price > realPrice2)
			{
				NKCResourceUtility.GetOrLoadMiscItemSmallIcon(this.m_cNKMShopItemTemplet.m_PriceItemID);
				NKCUtil.SetLabelText(this.m_lbOldPrice, (this.m_cNKMShopItemTemplet.m_Price * this.m_iBuyCount).ToString());
			}
			if (!flag)
			{
				NKCPopupShopBuyConfirm.Instance.m_priceTag.SetLabelTextColor(Color.red);
				NKCUtil.SetLabelTextColor(this.m_lbBuyCount, Color.red);
			}
			else if (this.m_iBuyCount > 1)
			{
				NKCPopupShopBuyConfirm.Instance.m_priceTag.SetLabelTextColor(new Color(1f, 0.8117647f, 0.23137255f));
				NKCUtil.SetLabelTextColor(this.m_lbBuyCount, new Color(1f, 0.8117647f, 0.23137255f));
			}
			else
			{
				NKCPopupShopBuyConfirm.Instance.m_priceTag.SetLabelTextColor(Color.white);
				NKCUtil.SetLabelTextColor(this.m_lbBuyCount, Color.white);
			}
			if (this.m_iBuyCount > 0 && this.m_cNKMShopItemTemplet != null && this.m_NKCUIItemSlot != null)
			{
				bool bShowNumber = this.IsCountVisible(this.m_cNKMShopItemTemplet);
				bool bFirstBuy = NKCShopManager.IsFirstBuy(this.m_cNKMShopItemTemplet.m_ProductID, NKCScenManager.CurrentUserData());
				NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeShopItemData(this.m_cNKMShopItemTemplet, bFirstBuy);
				slotData.Count *= (long)this.m_iBuyCount;
				this.m_NKCUIItemSlot.SetData(slotData, false, bShowNumber, false, null);
				this.m_NKCUIItemSlot.SetOnClickAction(new NKCUISlot.SlotClickType[]
				{
					NKCUISlot.SlotClickType.RatioList,
					NKCUISlot.SlotClickType.ChoiceList,
					NKCUISlot.SlotClickType.BoxList
				});
			}
		}

		// Token: 0x06007BA0 RID: 31648 RVA: 0x00294084 File Offset: 0x00292284
		private void SetIntervalItem(NKCUISlot.SlotData slotData)
		{
			bool flag = false;
			if (slotData != null && slotData.eType == NKCUISlot.eSlotMode.ItemMisc)
			{
				NKMItemMiscTemplet nkmitemMiscTemplet = NKMItemMiscTemplet.Find(slotData.ID);
				flag = (nkmitemMiscTemplet != null && nkmitemMiscTemplet.IsTimeIntervalItem);
				if (flag)
				{
					string timeSpanStringDHM = NKCUtilString.GetTimeSpanStringDHM(nkmitemMiscTemplet.GetIntervalTimeSpanLeft());
					NKCUtil.SetLabelText(this.m_lbTimeLeft, timeSpanStringDHM);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objTimeInterval, flag);
		}

		// Token: 0x06007BA1 RID: 31649 RVA: 0x002940E0 File Offset: 0x002922E0
		public void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
			if (this.m_tEndDateDiscountTime != DateTime.MinValue)
			{
				this.m_tDeltaTime += Time.unscaledDeltaTime;
				if (this.m_tDeltaTime > this.ONE_SECOND)
				{
					this.UpdateDiscountTime(this.m_tEndDateDiscountTime);
					this.m_tDeltaTime = 0f;
				}
			}
			this.OnUpdateButtonHold();
		}

		// Token: 0x06007BA2 RID: 31650 RVA: 0x00294150 File Offset: 0x00292350
		public void OnOK()
		{
			List<NKCShopManager.ShopRewardSubstituteData> list = NKCShopManager.MakeShopBuySubstituteItemList(this.m_cNKMShopItemTemplet, this.m_iBuyCount, null);
			if (list != null && list.Count > 0)
			{
				NKCPopupShopCustomPackageSubstitude.Instance.Open(list, new NKCPopupShopCustomPackageSubstitude.OnClose(this.ConfirmBuy));
				return;
			}
			this.ConfirmBuy();
		}

		// Token: 0x06007BA3 RID: 31651 RVA: 0x0029419A File Offset: 0x0029239A
		private void ConfirmBuy()
		{
			NKCPopupShopCustomPackageSubstitude.CheckInstanceAndClose();
			base.Close();
			if (this.dOnOKButton != null)
			{
				this.dOnOKButton(this.m_cNKMShopItemTemplet.m_ProductID, this.m_iBuyCount, null);
			}
		}

		// Token: 0x06007BA4 RID: 31652 RVA: 0x002941CC File Offset: 0x002923CC
		public void OnPolicy()
		{
			NKCPublisherModule.InAppPurchase.OpenPolicy(null);
		}

		// Token: 0x06007BA5 RID: 31653 RVA: 0x002941DC File Offset: 0x002923DC
		public void OnChangeCount(bool bPlus = true)
		{
			if (this.m_bWasHold)
			{
				this.m_bWasHold = false;
				return;
			}
			if (!bPlus && this.m_iBuyCount == 1)
			{
				if (this.m_iMaxBuyCount > 0 && this.m_bUseMinusToMax)
				{
					this.m_iBuyCount = this.m_iMaxBuyCount;
					this.UpdatePriceInfo();
				}
				this.OnButtonUp();
				return;
			}
			this.m_iBuyCount += (bPlus ? 1 : -1);
			if (!bPlus && this.m_iBuyCount <= 1)
			{
				this.m_iBuyCount = 1;
			}
			if (bPlus && this.m_iBuyCount >= this.m_iMaxBuyCount)
			{
				this.m_iBuyCount = this.m_iMaxBuyCount;
			}
			this.UpdatePriceInfo();
		}

		// Token: 0x06007BA6 RID: 31654 RVA: 0x00294279 File Offset: 0x00292479
		private void OnMinusDown(PointerEventData eventData)
		{
			this.m_iChangeValue = -1;
			this.m_bPress = true;
			this.m_fDelay = 0.35f;
			this.m_fHoldTime = 0f;
			this.m_bWasHold = false;
		}

		// Token: 0x06007BA7 RID: 31655 RVA: 0x002942A6 File Offset: 0x002924A6
		private void OnPlusDown(PointerEventData eventData)
		{
			this.m_iChangeValue = 1;
			this.m_bPress = true;
			this.m_fDelay = 0.35f;
			this.m_fHoldTime = 0f;
			this.m_bWasHold = false;
		}

		// Token: 0x06007BA8 RID: 31656 RVA: 0x002942D3 File Offset: 0x002924D3
		private void OnButtonUp()
		{
			this.m_iChangeValue = 0;
			this.m_fDelay = 0.35f;
			this.m_bPress = false;
		}

		// Token: 0x06007BA9 RID: 31657 RVA: 0x002942F0 File Offset: 0x002924F0
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
				int num = (this.m_fDelay < 0.01f) ? 5 : 1;
				this.m_fDelay = Mathf.Clamp(this.m_fDelay, 0.01f, 0.35f);
				this.m_iBuyCount += this.m_iChangeValue * num;
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
				this.UpdatePriceInfo();
			}
		}

		// Token: 0x06007BAA RID: 31658 RVA: 0x002943E0 File Offset: 0x002925E0
		public void OnScroll(PointerEventData eventData)
		{
			if (!this.m_objBuyCount.activeSelf)
			{
				return;
			}
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
			this.UpdateByPriceItemId();
		}

		// Token: 0x06007BAB RID: 31659 RVA: 0x00294483 File Offset: 0x00292683
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007BAC RID: 31660 RVA: 0x00294491 File Offset: 0x00292691
		private void OnBtnLinkedItem()
		{
			if (this.m_cNKMShopItemTemplet != null)
			{
				NKCPopupShopLinkPreview.Instance.Open(this.m_cNKMShopItemTemplet.m_ProductID);
			}
		}

		// Token: 0x06007BAD RID: 31661 RVA: 0x002944B0 File Offset: 0x002926B0
		private void OnBtnJPNPaymentLaw()
		{
			NKCPublisherModule.InAppPurchase.OpenPaymentLaw(null);
		}

		// Token: 0x06007BAE RID: 31662 RVA: 0x002944BD File Offset: 0x002926BD
		private void OnBtnJPNCommercialLaw()
		{
			NKCPublisherModule.InAppPurchase.OpenCommercialLaw(null);
		}

		// Token: 0x04006832 RID: 26674
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_shop";

		// Token: 0x04006833 RID: 26675
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_SHOP_BUY_CONFIRM";

		// Token: 0x04006834 RID: 26676
		private static NKCPopupShopBuyConfirm m_Instance;

		// Token: 0x04006835 RID: 26677
		private const string COUNT_COLOR = "ffcf3b";

		// Token: 0x04006836 RID: 26678
		public Text m_lbItemName;

		// Token: 0x04006837 RID: 26679
		public GameObject m_objItemInventoryCountParent;

		// Token: 0x04006838 RID: 26680
		public Text m_lbItemInventoryCount;

		// Token: 0x04006839 RID: 26681
		public Text m_lbItemDesc;

		// Token: 0x0400683A RID: 26682
		public GameObject m_objRemainCount;

		// Token: 0x0400683B RID: 26683
		public Text m_lbItemRemainCount;

		// Token: 0x0400683C RID: 26684
		public GameObject m_objItemSlot;

		// Token: 0x0400683D RID: 26685
		public NKCUISlot m_NKCUIItemSlot;

		// Token: 0x0400683E RID: 26686
		public GameObject m_objEquipSlot;

		// Token: 0x0400683F RID: 26687
		public NKCUIInvenEquipSlot m_NKCUIInvenEquipSlot;

		// Token: 0x04006840 RID: 26688
		public GameObject m_objBG;

		// Token: 0x04006841 RID: 26689
		public GameObject m_objBuyCount;

		// Token: 0x04006842 RID: 26690
		public NKCUIComStateButton m_btnBuyCountMinus;

		// Token: 0x04006843 RID: 26691
		public NKCUIComStateButton m_btnBuyCountPlus;

		// Token: 0x04006844 RID: 26692
		public Text m_lbBuyCount;

		// Token: 0x04006845 RID: 26693
		[Header("가격")]
		public NKCUIPriceTag m_priceTag;

		// Token: 0x04006846 RID: 26694
		public GameObject m_objSalePriceRoot;

		// Token: 0x04006847 RID: 26695
		public Text m_lbOldPrice;

		// Token: 0x04006848 RID: 26696
		public GameObject m_objDiscountDay;

		// Token: 0x04006849 RID: 26697
		public Text m_txtDiscountDay;

		// Token: 0x0400684A RID: 26698
		public GameObject m_objDiscountRate;

		// Token: 0x0400684B RID: 26699
		public Text m_txtDiscountRate;

		// Token: 0x0400684C RID: 26700
		[Header("하단 버튼")]
		public NKCUIComStateButton m_btnCancel;

		// Token: 0x0400684D RID: 26701
		public NKCUIComStateButton m_btnBuy;

		// Token: 0x0400684E RID: 26702
		public NKCUIComStateButton m_btnBuyLocked;

		// Token: 0x0400684F RID: 26703
		[Header("청약철회")]
		public GameObject m_objPolicyParent;

		// Token: 0x04006850 RID: 26704
		public NKCUIComStateButton m_btnPolicy;

		// Token: 0x04006851 RID: 26705
		[Header("연계 상품")]
		public NKCUIComStateButton m_csbtnLinkedItem;

		// Token: 0x04006852 RID: 26706
		[Header("가구 관련")]
		public NKCUIComOfficeInteriorDetail m_comInteriorDetail;

		// Token: 0x04006853 RID: 26707
		public NKCUIComOfficeInteriorInteractionBubble m_comInteriorInteractionBubble;

		// Token: 0x04006854 RID: 26708
		[Header("일본 법무 대응")]
		public GameObject m_JPN_BTN;

		// Token: 0x04006855 RID: 26709
		public GameObject m_JPN_POLICY;

		// Token: 0x04006856 RID: 26710
		public NKCUIComStateButton m_csbtnJPNPaymentLaw;

		// Token: 0x04006857 RID: 26711
		public NKCUIComStateButton m_csbtnJPNCommercialLaw;

		// Token: 0x04006858 RID: 26712
		[Header("기간제 아이템")]
		public GameObject m_objTimeInterval;

		// Token: 0x04006859 RID: 26713
		public Text m_lbTimeLeft;

		// Token: 0x0400685A RID: 26714
		private const int ITEM_MULTIPLE_USE_MAX_COUNT = 10;

		// Token: 0x0400685B RID: 26715
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x0400685C RID: 26716
		private NKCUIShop.OnProductBuyDelegate dOnOKButton;

		// Token: 0x0400685D RID: 26717
		private DateTime m_tEndDateDiscountTime = DateTime.MinValue;

		// Token: 0x0400685E RID: 26718
		private ShopItemTemplet m_cNKMShopItemTemplet;

		// Token: 0x0400685F RID: 26719
		private int m_iBuyCount;

		// Token: 0x04006861 RID: 26721
		private bool m_bUseMinusToMax;

		// Token: 0x04006862 RID: 26722
		private float m_tDeltaTime;

		// Token: 0x04006863 RID: 26723
		private float ONE_SECOND = 1f;

		// Token: 0x04006864 RID: 26724
		public const float PRESS_GAP_MAX = 0.35f;

		// Token: 0x04006865 RID: 26725
		public const float PRESS_GAP_MIN = 0.01f;

		// Token: 0x04006866 RID: 26726
		public const float DAMPING = 0.8f;

		// Token: 0x04006867 RID: 26727
		private float m_fDelay = 0.5f;

		// Token: 0x04006868 RID: 26728
		private float m_fHoldTime;

		// Token: 0x04006869 RID: 26729
		private int m_iChangeValue;

		// Token: 0x0400686A RID: 26730
		private bool m_bPress;

		// Token: 0x0400686B RID: 26731
		private bool m_bWasHold;
	}
}
