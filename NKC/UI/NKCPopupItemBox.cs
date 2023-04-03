using System;
using NKM;
using NKM.Shop;
using NKM.Templet;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A67 RID: 2663
	public class NKCPopupItemBox : NKCUIBase
	{
		// Token: 0x1700138B RID: 5003
		// (get) Token: 0x0600753F RID: 30015 RVA: 0x0026F98C File Offset: 0x0026DB8C
		public static NKCPopupItemBox Instance
		{
			get
			{
				if (NKCPopupItemBox.m_Instance == null)
				{
					NKCPopupItemBox.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupItemBox>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_ITEM_BOX", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupItemBox.CleanupInstance)).GetInstance<NKCPopupItemBox>();
					NKCPopupItemBox.m_Instance.InitUI();
				}
				return NKCPopupItemBox.m_Instance;
			}
		}

		// Token: 0x06007540 RID: 30016 RVA: 0x0026F9DC File Offset: 0x0026DBDC
		public static NKCPopupItemBox OpenNewInstance()
		{
			NKCPopupItemBox instance = NKCUIManager.OpenNewInstance<NKCPopupItemBox>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_ITEM_BOX", NKCUIManager.eUIBaseRect.UIFrontPopup, null).GetInstance<NKCPopupItemBox>();
			if (instance != null)
			{
				instance.InitUI();
			}
			return instance;
		}

		// Token: 0x06007541 RID: 30017 RVA: 0x0026FA10 File Offset: 0x0026DC10
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupItemBox.m_Instance != null && NKCPopupItemBox.m_Instance.IsOpen)
			{
				NKCPopupItemBox.m_Instance.Close();
			}
		}

		// Token: 0x06007542 RID: 30018 RVA: 0x0026FA35 File Offset: 0x0026DC35
		private static void CleanupInstance()
		{
			NKCPopupItemBox.m_Instance = null;
		}

		// Token: 0x1700138C RID: 5004
		// (get) Token: 0x06007543 RID: 30019 RVA: 0x0026FA3D File Offset: 0x0026DC3D
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700138D RID: 5005
		// (get) Token: 0x06007544 RID: 30020 RVA: 0x0026FA40 File Offset: 0x0026DC40
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_POPUP_ITEM_BOX;
			}
		}

		// Token: 0x06007545 RID: 30021 RVA: 0x0026FA48 File Offset: 0x0026DC48
		public void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			this.m_NKCUIItemSlot.Init();
			NKCUIComItemDropInfo itemDropInfo = this.m_ItemDropInfo;
			if (itemDropInfo != null)
			{
				itemDropInfo.Init();
			}
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData eventData)
			{
				base.Close();
			});
			this.m_etBG.triggers.Add(entry);
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_btnCancel.PointerClick.RemoveAllListeners();
			this.m_btnCancel.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_btnAction1.PointerClick.RemoveAllListeners();
			this.m_btnAction1.PointerClick.AddListener(new UnityAction(this.OnOK));
			this.m_btnAction2.PointerClick.RemoveAllListeners();
			this.m_btnAction2.PointerClick.AddListener(new UnityAction(this.OnAction2));
			NKCUtil.SetButtonClickDelegate(this.m_btnAdOn, new UnityAction(this.OnAdWatch));
			this.m_NKCPopupEmoticonSlotSD.SetClickEvent(delegate(NKCUISlot.SlotData a, bool b)
			{
				this.m_NKCPopupEmoticonSlotSD.PlaySDAni();
			});
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007546 RID: 30022 RVA: 0x0026FBA4 File Offset: 0x0026DDA4
		public void OpenItemBox(int itemMiscID, NKCPopupItemBox.eMode mode = NKCPopupItemBox.eMode.Normal, NKCPopupItemBox.OnButton onOkButton = null)
		{
			NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeMiscItemData(itemMiscID, 1L, 0);
			this.Open(mode, data, onOkButton, false, false, true);
		}

		// Token: 0x06007547 RID: 30023 RVA: 0x0026FBC8 File Offset: 0x0026DDC8
		public void OpenMoldItemBox(int itemMoldID, NKCPopupItemBox.eMode mode = NKCPopupItemBox.eMode.Normal, NKCPopupItemBox.OnButton onOkButton = null)
		{
			NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeMoldItemData(itemMoldID, 1L);
			this.Open(mode, data, onOkButton, false, false, true);
		}

		// Token: 0x06007548 RID: 30024 RVA: 0x0026FBEC File Offset: 0x0026DDEC
		public void OpenUnitBox(int unitID, NKCPopupItemBox.eMode mode = NKCPopupItemBox.eMode.Normal, NKCPopupItemBox.OnButton onOkButton = null)
		{
			NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeUnitData(unitID, 1, 0, 0);
			this.Open(mode, data, onOkButton, false, false, true);
		}

		// Token: 0x06007549 RID: 30025 RVA: 0x0026FC10 File Offset: 0x0026DE10
		public void OpenEmoticonBox(int emoticonID, NKCPopupItemBox.eMode mode = NKCPopupItemBox.eMode.Normal, NKCPopupItemBox.OnButton onOkButton = null)
		{
			NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeEmoticonData(emoticonID, 1);
			this.Open(mode, data, onOkButton, false, false, true);
		}

		// Token: 0x0600754A RID: 30026 RVA: 0x0026FC34 File Offset: 0x0026DE34
		public void Open(ShopItemTemplet productTemplet, NKCPopupItemBox.OnButton onOkButton)
		{
			bool bShowCount = productTemplet != null && this.IsCountVisible(productTemplet.m_ItemType);
			bool bFirstBuy = productTemplet != null && NKCShopManager.IsFirstBuy(productTemplet.m_ProductID, NKCScenManager.CurrentUserData());
			this.Open(NKCPopupItemBox.eMode.ShopBuy, NKCUISlot.SlotData.MakeShopItemData(productTemplet, bFirstBuy), onOkButton, false, bShowCount, true);
			NKCPopupItemBox.Instance.m_lbItemDesc.text = NKCUtilString.GetShopDescriptionText(productTemplet.GetItemDescPopup(), bFirstBuy);
			if (productTemplet.m_PriceItemID == 0)
			{
				NKCPopupItemBox.Instance.m_priceTag.SetData(productTemplet, false, false);
			}
			else
			{
				int realPrice = NKCScenManager.CurrentUserData().m_ShopData.GetRealPrice(productTemplet, 1, false);
				NKCPopupItemBox.Instance.m_priceTag.SetData(productTemplet.m_PriceItemID, realPrice, false, false, true);
			}
			this.SetOldPrice(productTemplet);
		}

		// Token: 0x0600754B RID: 30027 RVA: 0x0026FCEC File Offset: 0x0026DEEC
		public void Open(NKMShopRandomListData randomProductData, bool showDropInfo, NKCPopupItemBox.OnButton onOkButton)
		{
			bool bShowCount = randomProductData != null && this.IsCountVisible(randomProductData.itemType);
			this.Open(NKCPopupItemBox.eMode.ShopBuy, NKCUISlot.SlotData.MakeShopItemData(randomProductData), onOkButton, false, bShowCount, showDropInfo);
			int price = randomProductData.GetPrice();
			NKCPopupItemBox.Instance.m_priceTag.SetData(randomProductData.priceItemId, price, false, false, true);
			this.SetOldPrice(randomProductData.priceItemId, price, randomProductData.price);
		}

		// Token: 0x0600754C RID: 30028 RVA: 0x0026FD54 File Offset: 0x0026DF54
		public void Open(NKCPopupItemBox.eMode mode, NKCUISlot.SlotData data, NKCPopupItemBox.OnButton onOkButton = null, bool singleOpenOnly = false, bool bShowCount = false, bool showDropInfo = true)
		{
			if (data == null)
			{
				return;
			}
			if (data.eType == NKCUISlot.eSlotMode.Emoticon)
			{
				this.EmoticonOpenProcess(data, mode, bShowCount, showDropInfo);
			}
			else
			{
				this.NormalOpenProcess(data, mode, bShowCount, showDropInfo);
			}
			this.SetButton(mode, data.Count, onOkButton, singleOpenOnly);
			NKCAdManager.SetItemRewardAdButtonState(mode, data.ID, this.m_btnAdOn, this.m_btnAdOff, this.m_lbAdLeftCount);
		}

		// Token: 0x0600754D RID: 30029 RVA: 0x0026FDB8 File Offset: 0x0026DFB8
		private void NormalOpenProcess(NKCUISlot.SlotData data, NKCPopupItemBox.eMode mode, bool bShowNumber, bool showDropInfo)
		{
			NKCUtil.SetGameobjectActive(this.m_objEmoticonComment, false);
			NKCUtil.SetGameobjectActive(this.m_objEmoticonSD, false);
			NKCUtil.SetGameobjectActive(this.m_objNormalIcons, true);
			this.m_NKCUIItemSlot.SetData(data, false, bShowNumber, false, null);
			this.m_NKCUIItemSlot.SetOnClickAction(new NKCUISlot.SlotClickType[]
			{
				NKCUISlot.SlotClickType.RatioList,
				NKCUISlot.SlotClickType.ChoiceList,
				NKCUISlot.SlotClickType.BoxList
			});
			this.CommonOpenProcess(data, mode, bShowNumber, showDropInfo);
		}

		// Token: 0x0600754E RID: 30030 RVA: 0x0026FE20 File Offset: 0x0026E020
		private void CommonOpenProcess(NKCUISlot.SlotData data, NKCPopupItemBox.eMode mode, bool bShowNumber, bool showDropInfo)
		{
			base.gameObject.SetActive(true);
			this.m_bReservedPlayComment = false;
			this.m_ReservedCommentEmoticonID = -1;
			this.SetTextFromSlotdata(data);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			this.currentItemData = data;
			NKCUtil.SetGameobjectActive(this.m_objPriceRoot, mode == NKCPopupItemBox.eMode.ShopBuy);
			NKCUIComItemDropInfo itemDropInfo = this.m_ItemDropInfo;
			if (itemDropInfo != null)
			{
				itemDropInfo.SetData(showDropInfo ? data : null, true);
			}
			this.SetIntervalItemTimeLeft(data);
			base.UIOpened(true);
		}

		// Token: 0x0600754F RID: 30031 RVA: 0x0026FE98 File Offset: 0x0026E098
		private void EmoticonOpenProcess(NKCUISlot.SlotData data, NKCPopupItemBox.eMode mode, bool bShowNumber, bool showDropInfo)
		{
			NKMEmoticonTemplet nkmemoticonTemplet = NKMEmoticonTemplet.Find(data.ID);
			if (nkmemoticonTemplet == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objEmoticonComment, nkmemoticonTemplet.m_EmoticonType == NKM_EMOTICON_TYPE.NET_TEXT);
			NKCUtil.SetGameobjectActive(this.m_objEmoticonSD, nkmemoticonTemplet.m_EmoticonType == NKM_EMOTICON_TYPE.NET_ANI);
			NKCUtil.SetGameobjectActive(this.m_objNormalIcons, false);
			this.CommonOpenProcess(data, mode, bShowNumber, showDropInfo);
			if (nkmemoticonTemplet.m_EmoticonType == NKM_EMOTICON_TYPE.NET_ANI)
			{
				this.m_NKCPopupEmoticonSlotSD.SetUI(data.ID);
				this.m_NKCPopupEmoticonSlotSD.PlaySDAni();
				return;
			}
			if (nkmemoticonTemplet.m_EmoticonType == NKM_EMOTICON_TYPE.NET_TEXT)
			{
				this.m_bReservedPlayComment = true;
				this.m_ReservedCommentEmoticonID = data.ID;
			}
		}

		// Token: 0x06007550 RID: 30032 RVA: 0x0026FF38 File Offset: 0x0026E138
		private void SetButton(NKCPopupItemBox.eMode mode, long count, NKCPopupItemBox.OnButton onOkButton1 = null, bool singleOpenOnly = false)
		{
			if (singleOpenOnly)
			{
				count = 1L;
			}
			switch (mode)
			{
			case NKCPopupItemBox.eMode.Normal:
				NKCUtil.SetGameobjectActive(this.m_btnCancel, false);
				NKCUtil.SetGameobjectActive(this.m_btnAction1, true);
				NKCUtil.SetGameobjectActive(this.m_btnAction2, false);
				NKCUtil.SetHotkey(this.m_btnAction1, HotkeyEventType.Confirm);
				NKCUtil.SetHotkey(this.m_btnAction2, HotkeyEventType.None);
				NKCUtil.SetLabelText(this.m_txtAction1, NKCUtilString.GET_STRING_CONFIRM);
				this.dOnOKButton = onOkButton1;
				this.dOnOKButton2 = null;
				return;
			case NKCPopupItemBox.eMode.OKCancel:
				NKCUtil.SetGameobjectActive(this.m_btnCancel, true);
				NKCUtil.SetGameobjectActive(this.m_btnAction1, true);
				NKCUtil.SetGameobjectActive(this.m_btnAction2, false);
				NKCUtil.SetHotkey(this.m_btnAction1, HotkeyEventType.Confirm);
				NKCUtil.SetHotkey(this.m_btnAction2, HotkeyEventType.None);
				NKCUtil.SetLabelText(this.m_txtAction1, NKCUtilString.GET_STRING_CONFIRM);
				this.dOnOKButton = onOkButton1;
				this.dOnOKButton2 = null;
				return;
			case NKCPopupItemBox.eMode.ItemBoxOpen:
			{
				NKCUtil.SetGameobjectActive(this.m_btnCancel, true);
				NKCUtil.SetGameobjectActive(this.m_btnAction1, true);
				NKCUtil.SetGameobjectActive(this.m_btnAction2, count > 1L);
				if (count > 1L)
				{
					NKCUtil.SetHotkey(this.m_btnAction1, HotkeyEventType.None);
					NKCUtil.SetHotkey(this.m_btnAction2, HotkeyEventType.Confirm);
				}
				else
				{
					NKCUtil.SetHotkey(this.m_btnAction1, HotkeyEventType.Confirm);
					NKCUtil.SetHotkey(this.m_btnAction2, HotkeyEventType.None);
				}
				long num = Math.Min(count, 10000L);
				NKCUtil.SetLabelText(this.m_txtAction1, string.Format(NKCUtilString.GET_STRING_USE_ONE_PARAM, 1));
				NKCUtil.SetLabelText(this.m_txtAction2, string.Format(NKCUtilString.GET_STRING_USE_ONE_PARAM, num));
				this.dOnOKButton = new NKCPopupItemBox.OnButton(this.OnUseSingle);
				this.dOnOKButton2 = new NKCPopupItemBox.OnButton(this.OnUseMany);
				return;
			}
			case NKCPopupItemBox.eMode.ShopBuy:
			case NKCPopupItemBox.eMode.MoveToShop:
				NKCUtil.SetGameobjectActive(this.m_btnCancel, true);
				NKCUtil.SetGameobjectActive(this.m_btnAction1, true);
				NKCUtil.SetGameobjectActive(this.m_btnAction2, false);
				NKCUtil.SetHotkey(this.m_btnAction1, HotkeyEventType.Confirm);
				NKCUtil.SetHotkey(this.m_btnAction2, HotkeyEventType.None);
				NKCUtil.SetLabelText(this.m_txtAction1, NKCUtilString.GET_STRING_SHOP_PURCHASE);
				this.dOnOKButton = onOkButton1;
				this.dOnOKButton2 = null;
				return;
			case NKCPopupItemBox.eMode.Choice:
				NKCUtil.SetGameobjectActive(this.m_btnCancel, true);
				NKCUtil.SetGameobjectActive(this.m_btnAction1, true);
				NKCUtil.SetGameobjectActive(this.m_btnAction2, false);
				NKCUtil.SetHotkey(this.m_btnAction1, HotkeyEventType.Confirm);
				NKCUtil.SetHotkey(this.m_btnAction2, HotkeyEventType.None);
				NKCUtil.SetLabelText(this.m_txtAction1, NKCUtilString.GET_STRING_USE_CHOICE);
				this.dOnOKButton = new NKCPopupItemBox.OnButton(this.OnUseChoice);
				this.dOnOKButton2 = null;
				return;
			default:
				NKCUtil.SetGameobjectActive(this.m_btnCancel, true);
				NKCUtil.SetGameobjectActive(this.m_btnAction1, false);
				NKCUtil.SetGameobjectActive(this.m_btnAction2, false);
				NKCUtil.SetHotkey(this.m_btnAction1, HotkeyEventType.Confirm);
				NKCUtil.SetHotkey(this.m_btnAction2, HotkeyEventType.None);
				this.dOnOKButton = null;
				this.dOnOKButton2 = null;
				return;
			}
		}

		// Token: 0x06007551 RID: 30033 RVA: 0x002701F8 File Offset: 0x0026E3F8
		private void SetOldPrice(ShopItemTemplet productTemplet)
		{
			if (productTemplet == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objSalePriceRoot, false);
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			int realPrice = nkmuserData.m_ShopData.GetRealPrice(productTemplet, 1, false);
			this.SetOldPrice(productTemplet.m_PriceItemID, realPrice, productTemplet.m_Price);
		}

		// Token: 0x06007552 RID: 30034 RVA: 0x00270244 File Offset: 0x0026E444
		private void SetOldPrice(int priceItemId, int realPrice, int oldPrice)
		{
			NKCUtil.SetGameobjectActive(this.m_objSalePriceRoot, oldPrice > realPrice);
			if (oldPrice > realPrice)
			{
				Sprite orLoadMiscItemSmallIcon = NKCResourceUtility.GetOrLoadMiscItemSmallIcon(priceItemId);
				NKCUtil.SetImageSprite(this.m_imgOldPrice, orLoadMiscItemSmallIcon, true);
				NKCUtil.SetLabelText(this.m_lbOldPrice, oldPrice.ToString());
			}
		}

		// Token: 0x06007553 RID: 30035 RVA: 0x0027028C File Offset: 0x0026E48C
		private bool IsCountVisible(NKM_REWARD_TYPE type)
		{
			switch (type)
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

		// Token: 0x06007554 RID: 30036 RVA: 0x002702DC File Offset: 0x0026E4DC
		private void SetTextFromSlotdata(NKCUISlot.SlotData data)
		{
			NKCUtil.SetGameobjectActive(this.m_lbItemType, false);
			NKCUtil.SetGameobjectActive(this.m_lbItemCount, false);
			switch (data.eType)
			{
			case NKCUISlot.eSlotMode.Unit:
			case NKCUISlot.eSlotMode.UnitCount:
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(data.ID);
				if (unitTempletBase != null)
				{
					this.m_lbItemName.text = unitTempletBase.GetUnitName();
					NKCUtil.SetGameobjectActive(this.m_lbItemType, true);
					this.m_lbItemType.text = unitTempletBase.GetUnitTitle();
					this.m_lbItemDesc.text = unitTempletBase.GetUnitDesc();
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
				NKMOfficeInteriorTemplet nkmofficeInteriorTemplet = NKMItemMiscTemplet.FindInterior(data.ID);
				if (nkmofficeInteriorTemplet != null)
				{
					this.m_lbItemName.text = itemMiscTempletByID.GetItemName();
					InteriorCategory interiorCategory = nkmofficeInteriorTemplet.InteriorCategory;
					if (interiorCategory != InteriorCategory.DECO)
					{
						if (interiorCategory == InteriorCategory.FURNITURE)
						{
							NKCUtil.SetGameobjectActive(this.m_lbItemCount, true);
							NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
							NKCUtil.SetLabelText(this.m_lbItemCount, NKCStringTable.GetString("SI_DP_INTERIOR_COUNT_TWO_PARAM", new object[]
							{
								nkmuserData.OfficeData.GetFreeInteriorCount(data.ID).ToString("N0"),
								nkmuserData.OfficeData.GetInteriorCount(data.ID).ToString("N0")
							}));
						}
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_lbItemCount, false);
					}
					this.m_lbItemDesc.text = itemMiscTempletByID.GetItemDesc();
					return;
				}
				if (itemMiscTempletByID != null)
				{
					this.m_lbItemName.text = itemMiscTempletByID.GetItemName();
					this.m_lbItemDesc.text = itemMiscTempletByID.GetItemDesc();
					NKCUtil.SetGameobjectActive(this.m_lbItemCount, itemMiscTempletByID.m_ItemMiscType != NKM_ITEM_MISC_TYPE.IMT_VIEW);
					long num;
					if (data.ID == 203)
					{
						num = NKMMissionManager.GetRepeatMissionDataTimes(NKM_MISSION_TYPE.REPEAT_DAILY);
					}
					else if (data.ID == 204)
					{
						num = NKMMissionManager.GetRepeatMissionDataTimes(NKM_MISSION_TYPE.REPEAT_WEEKLY);
					}
					else if (data.ID == 202)
					{
						num = NKCScenManager.CurrentUserData().GetMissionAchievePoint();
					}
					else
					{
						num = NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(data.ID);
					}
					NKCUtil.SetLabelText(this.m_lbItemCount, NKCUtilString.GET_STRING_ITEM_COUNT_ONE_PARAM, new object[]
					{
						num.ToString("N0")
					});
					return;
				}
				Debug.LogError("ItemTemplet Not Found. ItemID : " + data.ID.ToString());
				this.m_lbItemName.text = "";
				this.m_lbItemDesc.text = "";
				return;
			}
			case NKCUISlot.eSlotMode.Equip:
			case NKCUISlot.eSlotMode.EquipCount:
			{
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(data.ID);
				if (equipTemplet != null)
				{
					if (data.Count > 0L)
					{
						this.m_lbItemName.text = string.Format("{0} +{1}", NKCUtilString.GetItemEquipNameWithTier(equipTemplet), data.Count);
					}
					else
					{
						this.m_lbItemName.text = NKCUtilString.GetItemEquipNameWithTier(equipTemplet);
					}
					NKCUtil.SetGameobjectActive(this.m_lbItemType, true);
					this.m_lbItemType.text = NKCUtilString.GetEquipTypeString(equipTemplet);
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
					NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(skinTemplet.m_SkinEquipUnitID);
					this.m_lbItemName.text = skinTemplet.GetTitle();
					this.m_lbItemDesc.text = skinTemplet.GetSkinDesc();
					NKCUtil.SetGameobjectActive(this.m_lbItemType, true);
					this.m_lbItemType.text = string.Format(NKCUtilString.GET_STRING_SKIN_ONE_PARAM, unitTempletBase2.GetUnitName());
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
					return;
				}
				Debug.LogError("NKMItemMoldTemplet Not Found. moldItemID : " + data.ID.ToString());
				this.m_lbItemName.text = "";
				this.m_lbItemDesc.text = "";
				return;
			}
			case NKCUISlot.eSlotMode.Buff:
			{
				NKMCompanyBuffTemplet companyBuffTemplet = NKMCompanyBuffManager.GetCompanyBuffTemplet(data.ID);
				this.m_lbItemName.text = companyBuffTemplet.GetBuffName();
				this.m_lbItemDesc.text = companyBuffTemplet.GetBuffDescForItemPopup();
				return;
			}
			case NKCUISlot.eSlotMode.Emoticon:
			{
				NKMEmoticonTemplet nkmemoticonTemplet = NKMEmoticonTemplet.Find(data.ID);
				if (nkmemoticonTemplet != null)
				{
					this.m_lbItemName.text = nkmemoticonTemplet.GetEmoticonName();
					this.m_lbItemDesc.text = nkmemoticonTemplet.GetEmoticonDesc();
					return;
				}
				return;
			}
			}
			Debug.LogError("Undefined type");
			this.m_lbItemName.text = "";
			this.m_lbItemDesc.text = "";
		}

		// Token: 0x06007555 RID: 30037 RVA: 0x002707D8 File Offset: 0x0026E9D8
		private void SetIntervalItemTimeLeft(NKCUISlot.SlotData slotData)
		{
			bool flag = false;
			if (slotData.eType == NKCUISlot.eSlotMode.ItemMisc)
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

		// Token: 0x06007556 RID: 30038 RVA: 0x00270830 File Offset: 0x0026EA30
		public void RefreshDropInfo(bool initScrollPosition)
		{
			if (this.m_ItemDropInfo != null && this.m_ItemDropInfo.gameObject.activeSelf)
			{
				this.m_ItemDropInfo.SetData(this.currentItemData, initScrollPosition);
			}
		}

		// Token: 0x06007557 RID: 30039 RVA: 0x00270868 File Offset: 0x0026EA68
		public void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
				if (this.m_bReservedPlayComment)
				{
					this.m_bReservedPlayComment = false;
					this.m_NKCGameHudEmoticonComment.PlayPreview(this.m_ReservedCommentEmoticonID);
					this.m_ReservedCommentEmoticonID = -1;
				}
				if (this.currentItemData != null)
				{
					NKCAdManager.UpdateItemRewardAdCoolTime(this.currentItemData.ID, this.m_btnAdOn, this.m_btnAdOff, this.m_lbAdCoolTime, this.m_lbAdLeftCount);
				}
			}
		}

		// Token: 0x06007558 RID: 30040 RVA: 0x002708DF File Offset: 0x0026EADF
		public void OnOK()
		{
			base.Close();
			if (this.dOnOKButton != null)
			{
				this.dOnOKButton();
			}
		}

		// Token: 0x06007559 RID: 30041 RVA: 0x002708FA File Offset: 0x0026EAFA
		public void OnAction2()
		{
			base.Close();
			if (this.dOnOKButton2 != null)
			{
				this.dOnOKButton2();
			}
		}

		// Token: 0x0600755A RID: 30042 RVA: 0x00270915 File Offset: 0x0026EB15
		public void OnUseSingle()
		{
			if (this.currentItemData != null)
			{
				NKCPacketSender.Send_NKMPacket_RANDOM_ITEM_BOX_OPEN_REQ(this.currentItemData.ID, 1);
			}
		}

		// Token: 0x0600755B RID: 30043 RVA: 0x00270930 File Offset: 0x0026EB30
		public void OnUseMany()
		{
			if (this.currentItemData != null)
			{
				long num = Math.Min(this.currentItemData.Count, 10000L);
				NKCPacketSender.Send_NKMPacket_RANDOM_ITEM_BOX_OPEN_REQ(this.currentItemData.ID, (int)num);
			}
		}

		// Token: 0x0600755C RID: 30044 RVA: 0x0027096E File Offset: 0x0026EB6E
		public void OnBuy()
		{
		}

		// Token: 0x0600755D RID: 30045 RVA: 0x00270970 File Offset: 0x0026EB70
		public void OnUseChoice()
		{
			if (this.currentItemData != null)
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(this.currentItemData.ID);
				if (itemMiscTempletByID != null)
				{
					NKM_ITEM_MISC_TYPE itemMiscType = itemMiscTempletByID.m_ItemMiscType;
					switch (itemMiscType)
					{
					case NKM_ITEM_MISC_TYPE.IMT_CHOICE_UNIT:
					case NKM_ITEM_MISC_TYPE.IMT_CHOICE_SHIP:
						NKCUISelection.Instance.Open(itemMiscTempletByID);
						return;
					case NKM_ITEM_MISC_TYPE.IMT_CHOICE_EQUIP:
						NKCUISelectionEquip.Instance.Open(itemMiscTempletByID);
						return;
					case NKM_ITEM_MISC_TYPE.IMT_CHOICE_MISC:
						NKCUISelectionMisc.Instance.Open(itemMiscTempletByID);
						return;
					case NKM_ITEM_MISC_TYPE.IMT_CHOICE_MOLD:
						Debug.LogError("필요시 추가해야함");
						return;
					case NKM_ITEM_MISC_TYPE.IMT_CHOICE_OPERATOR:
						NKCUISelectionOperator.Instance.Open(itemMiscTempletByID);
						return;
					default:
						if (itemMiscType != NKM_ITEM_MISC_TYPE.IMT_CHOICE_SKIN)
						{
							return;
						}
						NKCUISelectionSkin.Instance.Open(itemMiscTempletByID);
						break;
					}
				}
			}
		}

		// Token: 0x0600755E RID: 30046 RVA: 0x00270A0F File Offset: 0x0026EC0F
		public void OnAdWatch()
		{
			base.Close();
			if (this.currentItemData != null)
			{
				NKCAdManager.WatchItemRewardAd(this.currentItemData.ID);
			}
		}

		// Token: 0x0600755F RID: 30047 RVA: 0x00270A2F File Offset: 0x0026EC2F
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x0400618B RID: 24971
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_ok_cancel_box";

		// Token: 0x0400618C RID: 24972
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_ITEM_BOX";

		// Token: 0x0400618D RID: 24973
		private static NKCPopupItemBox m_Instance;

		// Token: 0x0400618E RID: 24974
		public Text m_lbItemName;

		// Token: 0x0400618F RID: 24975
		public Text m_lbItemType;

		// Token: 0x04006190 RID: 24976
		public Text m_lbItemCount;

		// Token: 0x04006191 RID: 24977
		public Text m_lbItemDesc;

		// Token: 0x04006192 RID: 24978
		public GameObject m_objNormalIcons;

		// Token: 0x04006193 RID: 24979
		public NKCUISlot m_NKCUIItemSlot;

		// Token: 0x04006194 RID: 24980
		public GameObject m_objEmoticonComment;

		// Token: 0x04006195 RID: 24981
		public NKCGameHudEmoticonComment m_NKCGameHudEmoticonComment;

		// Token: 0x04006196 RID: 24982
		public GameObject m_objEmoticonSD;

		// Token: 0x04006197 RID: 24983
		public NKCPopupEmoticonSlotSD m_NKCPopupEmoticonSlotSD;

		// Token: 0x04006198 RID: 24984
		public NKCUIComItemDropInfo m_ItemDropInfo;

		// Token: 0x04006199 RID: 24985
		public EventTrigger m_etBG;

		// Token: 0x0400619A RID: 24986
		public NKCUIComButton m_btnClose;

		// Token: 0x0400619B RID: 24987
		[Header("가격")]
		public GameObject m_objPriceRoot;

		// Token: 0x0400619C RID: 24988
		public NKCUIPriceTag m_priceTag;

		// Token: 0x0400619D RID: 24989
		public GameObject m_objSalePriceRoot;

		// Token: 0x0400619E RID: 24990
		public Text m_lbOldPrice;

		// Token: 0x0400619F RID: 24991
		public Image m_imgOldPrice;

		// Token: 0x040061A0 RID: 24992
		[Header("하단 버튼")]
		public NKCUIComButton m_btnCancel;

		// Token: 0x040061A1 RID: 24993
		public NKCUIComButton m_btnAction1;

		// Token: 0x040061A2 RID: 24994
		public NKCUIComButton m_btnAction2;

		// Token: 0x040061A3 RID: 24995
		public Text m_txtAction1;

		// Token: 0x040061A4 RID: 24996
		public Text m_txtAction2;

		// Token: 0x040061A5 RID: 24997
		public NKCUIComStateButton m_btnAdOn;

		// Token: 0x040061A6 RID: 24998
		public NKCUIComStateButton m_btnAdOff;

		// Token: 0x040061A7 RID: 24999
		public Text m_lbAdLeftCount;

		// Token: 0x040061A8 RID: 25000
		public Text m_lbAdCoolTime;

		// Token: 0x040061A9 RID: 25001
		[Header("기간제 아이템 표시")]
		public GameObject m_objTimeInterval;

		// Token: 0x040061AA RID: 25002
		public Text m_lbTimeLeft;

		// Token: 0x040061AB RID: 25003
		private NKCUISlot.SlotData currentItemData;

		// Token: 0x040061AC RID: 25004
		private const int ITEM_MULTIPLE_USE_MAX_COUNT = 10000;

		// Token: 0x040061AD RID: 25005
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x040061AE RID: 25006
		private bool m_bReservedPlayComment;

		// Token: 0x040061AF RID: 25007
		private int m_ReservedCommentEmoticonID = -1;

		// Token: 0x040061B0 RID: 25008
		private NKCPopupItemBox.OnButton dOnOKButton;

		// Token: 0x040061B1 RID: 25009
		private NKCPopupItemBox.OnButton dOnOKButton2;

		// Token: 0x020017C6 RID: 6086
		// (Invoke) Token: 0x0600B42E RID: 46126
		public delegate void OnButton();

		// Token: 0x020017C7 RID: 6087
		public enum eMode
		{
			// Token: 0x0400A77F RID: 42879
			Normal,
			// Token: 0x0400A780 RID: 42880
			OKCancel,
			// Token: 0x0400A781 RID: 42881
			ItemBoxOpen,
			// Token: 0x0400A782 RID: 42882
			ShopBuy,
			// Token: 0x0400A783 RID: 42883
			Choice,
			// Token: 0x0400A784 RID: 42884
			MoveToShop
		}
	}
}
