using System;
using System.Collections.Generic;
using NKM;
using NKM.Item;
using NKM.Shop;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Shop
{
	// Token: 0x02000ACB RID: 2763
	public class NKCPopupShopCustomPackage : NKCUIBase
	{
		// Token: 0x1700149B RID: 5275
		// (get) Token: 0x06007BC1 RID: 31681 RVA: 0x00294B90 File Offset: 0x00292D90
		public static NKCPopupShopCustomPackage Instance
		{
			get
			{
				if (NKCPopupShopCustomPackage.m_Instance == null)
				{
					NKCPopupShopCustomPackage.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupShopCustomPackage>("ab_ui_nkm_ui_shop", "NKM_UI_POPUP_SHOP_BUY_PACKAGE_CUSTOM", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupShopCustomPackage.CleanupInstance)).GetInstance<NKCPopupShopCustomPackage>();
					NKCPopupShopCustomPackage.m_Instance.Init();
				}
				return NKCPopupShopCustomPackage.m_Instance;
			}
		}

		// Token: 0x06007BC2 RID: 31682 RVA: 0x00294BDF File Offset: 0x00292DDF
		private static void CleanupInstance()
		{
			NKCPopupShopCustomPackage.m_Instance = null;
		}

		// Token: 0x1700149C RID: 5276
		// (get) Token: 0x06007BC3 RID: 31683 RVA: 0x00294BE7 File Offset: 0x00292DE7
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.DEFAULT;
			}
		}

		// Token: 0x1700149D RID: 5277
		// (get) Token: 0x06007BC4 RID: 31684 RVA: 0x00294BEA File Offset: 0x00292DEA
		public static bool HasInstance
		{
			get
			{
				return NKCPopupShopCustomPackage.m_Instance != null;
			}
		}

		// Token: 0x1700149E RID: 5278
		// (get) Token: 0x06007BC5 RID: 31685 RVA: 0x00294BF7 File Offset: 0x00292DF7
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupShopCustomPackage.m_Instance != null && NKCPopupShopCustomPackage.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007BC6 RID: 31686 RVA: 0x00294C12 File Offset: 0x00292E12
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupShopCustomPackage.m_Instance != null && NKCPopupShopCustomPackage.m_Instance.IsOpen)
			{
				NKCPopupShopCustomPackage.m_Instance.Close();
			}
		}

		// Token: 0x1700149F RID: 5279
		// (get) Token: 0x06007BC7 RID: 31687 RVA: 0x00294C37 File Offset: 0x00292E37
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170014A0 RID: 5280
		// (get) Token: 0x06007BC8 RID: 31688 RVA: 0x00294C3A File Offset: 0x00292E3A
		public override string MenuName
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_SHOP_ITEM_PURCHASE_CONFIRM", false);
			}
		}

		// Token: 0x06007BC9 RID: 31689 RVA: 0x00294C47 File Offset: 0x00292E47
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007BCA RID: 31690 RVA: 0x00294C58 File Offset: 0x00292E58
		public void Init()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnCancel, new UnityAction(base.Close));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnConfirm, new UnityAction(this.OnOK));
			NKCUtil.SetHotkey(this.m_csbtnConfirm, HotkeyEventType.Confirm, null, false);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnSkinPreview, new UnityAction(this.OnDetail));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnProbability, new UnityAction(this.OnDetail));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnDetail, new UnityAction(this.OnDetail));
			foreach (NKCUISlotCustomData nkcuislotCustomData in this.m_lstPackageSlot)
			{
				if (nkcuislotCustomData != null)
				{
					nkcuislotCustomData.Init();
					if (this.m_spEmpty != null)
					{
						nkcuislotCustomData.Slot.SetCustomizedEmptySP(this.m_spEmpty);
					}
				}
			}
			this.m_srSelection.dOnGetObject += this.GetObject;
			this.m_srSelection.dOnReturnObject += this.ReturnObject;
			this.m_srSelection.dOnProvideData += this.ProvideData;
			this.m_srSelection.PrepareCells(0);
			NKCUtil.SetScrollHotKey(this.m_srSelection, null);
		}

		// Token: 0x06007BCB RID: 31691 RVA: 0x00294DB0 File Offset: 0x00292FB0
		private void OnOK()
		{
			if (!NKCShopManager.IsAllCustomSlotSelected(this.m_targetItemTemplet, this.m_lstSelectedItems))
			{
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_CUSTOM_PACKAGE_INVALID_SELECTION_DATA, null, "");
				return;
			}
			NKCPopupShopPackageConfirm.Instance.Open(this.m_cNKMShopItemTemplet, new NKCUIShop.OnProductBuyDelegate(this.OnBuyConfirm), this.m_lstSelectedItems);
		}

		// Token: 0x06007BCC RID: 31692 RVA: 0x00294E03 File Offset: 0x00293003
		private void OnBuyConfirm(int ProductID, int ProductCount, List<int> lstSelection)
		{
			base.Close();
			NKCUIShop.OnProductBuyDelegate onProductBuyDelegate = this.dOnProductBuy;
			if (onProductBuyDelegate == null)
			{
				return;
			}
			onProductBuyDelegate(this.m_cNKMShopItemTemplet.m_ProductID, 1, this.m_lstSelectedItems);
		}

		// Token: 0x06007BCD RID: 31693 RVA: 0x00294E30 File Offset: 0x00293030
		public void Open(ShopItemTemplet shopItemTemplet, NKCUIShop.OnProductBuyDelegate onProductBuy)
		{
			if (shopItemTemplet == null)
			{
				Debug.LogError("NKCPopupShopCustomPackage opened null ShopItemTemplet");
				base.gameObject.SetActive(false);
				return;
			}
			if (shopItemTemplet.m_ItemType != NKM_REWARD_TYPE.RT_MISC)
			{
				Debug.LogError("NKCPopupShopCustomPackage opened with non-misc ShopItemTemplet");
				base.gameObject.SetActive(false);
				return;
			}
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(shopItemTemplet.m_ItemID);
			if (itemMiscTempletByID == null || !itemMiscTempletByID.IsCustomPackageItem)
			{
				Debug.LogError("NKCPopupShopCustomPackage opened with bad misc item");
				base.gameObject.SetActive(false);
				return;
			}
			if (itemMiscTempletByID.CustomPackageTemplets == null || itemMiscTempletByID.CustomPackageTemplets.Count == 0)
			{
				Debug.LogError("NKCPopupShopCustomPackage opened but customPackage has no selectable Items");
				base.gameObject.SetActive(false);
				return;
			}
			this.m_currentPackageSlotIndex = -1;
			this.m_customItemStartIndex = 0;
			this.m_lstCurrentSelectableElements = null;
			this.m_cNKMShopItemTemplet = shopItemTemplet;
			this.m_targetItemTemplet = itemMiscTempletByID;
			this.dOnProductBuy = onProductBuy;
			this.m_lstSelectedItems.Clear();
			for (int i = 0; i < this.m_targetItemTemplet.CustomPackageTemplets.Count; i++)
			{
				this.m_lstSelectedItems.Add(-1);
			}
			base.UIOpened(true);
			this.UpdateCurrentPackageItems(this.m_targetItemTemplet);
			this.UpdateSelectionItems(true);
			this.UpdateConfirmButton();
			NKCUtil.SetGameobjectActive(this.m_objRootDescItemSelected, false);
			NKCUtil.SetGameobjectActive(this.m_objRootDescItemNotSelected, true);
		}

		// Token: 0x06007BCE RID: 31694 RVA: 0x00294F68 File Offset: 0x00293168
		private void UpdateCurrentPackageItems(NKMItemMiscTemplet itemTemplet)
		{
			if (itemTemplet == null)
			{
				return;
			}
			List<NKMRandomBoxItemTemplet> randomBoxItemTempletList = NKCRandomBoxManager.GetRandomBoxItemTempletList(itemTemplet.m_RewardGroupID);
			if (itemTemplet.m_RewardGroupID != 0 && randomBoxItemTempletList == null)
			{
				Debug.LogError("rewardgroup null! ID : " + itemTemplet.m_RewardGroupID.ToString());
			}
			int num = 0;
			if (randomBoxItemTempletList != null)
			{
				for (int i = 0; i < randomBoxItemTempletList.Count; i++)
				{
					if (num >= this.m_lstPackageSlot.Count)
					{
						Debug.LogError("Package Item has too much item!");
						return;
					}
					NKCUISlotCustomData nkcuislotCustomData = this.m_lstPackageSlot[num];
					NKCUtil.SetGameobjectActive(nkcuislotCustomData, true);
					num++;
					NKMRandomBoxItemTemplet nkmrandomBoxItemTemplet = randomBoxItemTempletList[i];
					NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeRewardTypeData(nkmrandomBoxItemTemplet.m_reward_type, nkmrandomBoxItemTemplet.m_RewardID, nkmrandomBoxItemTemplet.TotalQuantity_Max, 0);
					nkcuislotCustomData.SetData(-1, slotData, false, slotData.eType == NKCUISlot.eSlotMode.ItemMisc, false, null);
					nkcuislotCustomData.Slot.SetOnClickAction(new NKCUISlot.SlotClickType[1]);
					bool redudantMark = NKCShopManager.WillOverflowOnGain(nkmrandomBoxItemTemplet.m_reward_type, nkmrandomBoxItemTemplet.m_RewardID, nkmrandomBoxItemTemplet.TotalQuantity_Max) || NKCShopManager.IsHaveUnit(nkmrandomBoxItemTemplet.m_reward_type, nkmrandomBoxItemTemplet.m_RewardID);
					nkcuislotCustomData.Slot.SetRedudantMark(redudantMark);
					NKCShopManager.ShowShopItemCashCount(nkcuislotCustomData.Slot, slotData, nkmrandomBoxItemTemplet.FreeQuantity_Max, nkmrandomBoxItemTemplet.PaidQuantity_Max);
				}
			}
			this.m_customItemStartIndex = num;
			if (itemTemplet.CustomPackageTemplets != null)
			{
				for (int j = 0; j < itemTemplet.CustomPackageTemplets.Count; j++)
				{
					if (num >= this.m_lstPackageSlot.Count)
					{
						Debug.LogError("Package Item has too much item!");
						return;
					}
					NKCUISlotCustomData nkcuislotCustomData2 = this.m_lstPackageSlot[num];
					NKCUtil.SetGameobjectActive(nkcuislotCustomData2, true);
					num++;
					int index = this.m_lstSelectedItems[j];
					NKMCustomPackageElement nkmcustomPackageElement = itemTemplet.CustomPackageTemplets[j].Get(index);
					if (nkmcustomPackageElement != null)
					{
						NKCUISlot.SlotData slotData2 = NKCUISlot.SlotData.MakeRewardTypeData(nkmcustomPackageElement.RewardType, nkmcustomPackageElement.RewardId, nkmcustomPackageElement.TotalRewardCount, 0);
						nkcuislotCustomData2.SetData(j, slotData2, false, slotData2.eType == NKCUISlot.eSlotMode.ItemMisc, false, new NKCUISlotCustomData.OnClick(this.OnSelectPackageSlot));
						NKCShopManager.ShowShopItemCashCount(nkcuislotCustomData2.Slot, slotData2, nkmcustomPackageElement.FreeRewardCount, nkmcustomPackageElement.PaidRewardCount);
						if (NKCShopManager.WillOverflowOnGain(nkmcustomPackageElement.RewardType, nkmcustomPackageElement.RewardId, nkmcustomPackageElement.TotalRewardCount))
						{
							nkcuislotCustomData2.Slot.SetHaveCountString(true, NKCStringTable.GetString("SI_DP_ICON_SLOT_ALREADY_HAVE", false));
						}
						else if (NKCShopManager.IsHaveUnit(nkmcustomPackageElement.RewardType, nkmcustomPackageElement.RewardId))
						{
							nkcuislotCustomData2.Slot.SetHaveCountString(true, NKCStringTable.GetString("SI_DP_ICON_SLOT_ALREADY_HAVE", false));
						}
						else if (NKCShopManager.IsCustomPackageSelectionHasDuplicate(itemTemplet, j, this.m_lstSelectedItems, false))
						{
							nkcuislotCustomData2.Slot.SetHaveCountString(true, NKCStringTable.GetString("SI_DP_SHOP_CUSTOM_DUPLICATE", false));
						}
						else
						{
							nkcuislotCustomData2.Slot.SetHaveCountString(false, null);
						}
						nkcuislotCustomData2.Slot.SetShowArrowBGText(true);
						nkcuislotCustomData2.Slot.SetArrowBGText(NKCStringTable.GetString("SI_DP_SHOP_SLOT_CHOICE", false), new Color(0.6666667f, 0.03137255f, 0.03137255f));
					}
					else
					{
						nkcuislotCustomData2.Slot.SetEmpty(null);
						nkcuislotCustomData2.SetOnClick(new NKCUISlotCustomData.OnClick(this.OnSelectPackageSlot));
						nkcuislotCustomData2.m_Data = j;
					}
					nkcuislotCustomData2.Slot.SetSelected(this.m_currentPackageSlotIndex == j);
				}
			}
			for (int k = num; k < this.m_lstPackageSlot.Count; k++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstPackageSlot[k], false);
			}
		}

		// Token: 0x06007BCF RID: 31695 RVA: 0x002952D8 File Offset: 0x002934D8
		private void OnSelectPackageSlot(NKCUISlot.SlotData slotData, bool bLocked, int index)
		{
			if (index == this.m_currentPackageSlotIndex)
			{
				return;
			}
			NKCUISlotCustomData customSlot = this.GetCustomSlot(this.m_currentPackageSlotIndex);
			if (customSlot != null)
			{
				customSlot.Slot.SetSelected(false);
			}
			this.m_currentPackageSlotIndex = index;
			NKCUISlotCustomData customSlot2 = this.GetCustomSlot(index);
			if (customSlot2 != null)
			{
				customSlot2.Slot.SetSelected(true);
			}
			this.SetDescriptionData(index);
			this.UpdateSelectionItems(true);
		}

		// Token: 0x06007BD0 RID: 31696 RVA: 0x00295344 File Offset: 0x00293544
		private void SetDescriptionData(int packageSlotIndex)
		{
			int index = this.m_lstSelectedItems[packageSlotIndex];
			NKMCustomPackageElement nkmcustomPackageElement = this.m_targetItemTemplet.CustomPackageTemplets[packageSlotIndex].Get(index);
			if (nkmcustomPackageElement == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objRootDescItemSelected, false);
				NKCUtil.SetGameobjectActive(this.m_objRootDescItemNotSelected, true);
				NKCUtil.SetGameobjectActive(this.m_csbtnSkinPreview, false);
				NKCUtil.SetGameobjectActive(this.m_csbtnProbability, false);
				NKCUtil.SetGameobjectActive(this.m_csbtnDetail, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objRootDescItemSelected, true);
			NKCUtil.SetGameobjectActive(this.m_objRootDescItemNotSelected, false);
			NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeRewardTypeData(nkmcustomPackageElement.RewardType, nkmcustomPackageElement.RewardId, nkmcustomPackageElement.FreeRewardCount, 0);
			NKCUtil.SetLabelText(this.m_lbItemTitle, NKCUISlot.GetName(slotData));
			NKCUtil.SetLabelText(this.m_lbItemDesc, NKCUISlot.GetDesc(slotData, true));
			NKCUtil.SetGameobjectActive(this.m_csbtnSkinPreview, nkmcustomPackageElement.RewardType == NKM_REWARD_TYPE.RT_SKIN);
			if (nkmcustomPackageElement.RewardType == NKM_REWARD_TYPE.RT_MISC)
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(nkmcustomPackageElement.RewardId);
				NKCUtil.SetGameobjectActive(this.m_csbtnProbability, itemMiscTempletByID.IsUsable() && itemMiscTempletByID.IsRatioOpened());
				NKCUtil.SetGameobjectActive(this.m_csbtnDetail, itemMiscTempletByID.IsUsable() && itemMiscTempletByID.IsChoiceItem());
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_csbtnProbability, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnDetail, false);
		}

		// Token: 0x06007BD1 RID: 31697 RVA: 0x00295488 File Offset: 0x00293688
		private NKCUISlotCustomData GetCustomSlot(int customSlotIndex)
		{
			if (customSlotIndex < 0)
			{
				return null;
			}
			int num = this.m_customItemStartIndex + customSlotIndex;
			if (num >= this.m_lstPackageSlot.Count)
			{
				return null;
			}
			return this.m_lstPackageSlot[num];
		}

		// Token: 0x06007BD2 RID: 31698 RVA: 0x002954C0 File Offset: 0x002936C0
		private RectTransform GetObject(int idx)
		{
			NKCUISlotCustomData nkcuislotCustomData = UnityEngine.Object.Instantiate<NKCUISlotCustomData>(this.m_pfbSlot);
			nkcuislotCustomData.Init();
			return nkcuislotCustomData.GetComponent<RectTransform>();
		}

		// Token: 0x06007BD3 RID: 31699 RVA: 0x002954D8 File Offset: 0x002936D8
		private void ReturnObject(Transform tr)
		{
			if (tr != null)
			{
				tr.SetParent(null);
			}
			tr.gameObject.SetActive(false);
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x06007BD4 RID: 31700 RVA: 0x00295504 File Offset: 0x00293704
		private void ProvideData(Transform tr, int idx)
		{
			NKCUISlotCustomData component = tr.GetComponent<NKCUISlotCustomData>();
			if (component == null)
			{
				tr.gameObject.SetActive(false);
				return;
			}
			if (idx < 0)
			{
				tr.gameObject.SetActive(false);
				return;
			}
			if (this.m_lstCurrentSelectableElements == null || idx >= this.m_lstCurrentSelectableElements.Count)
			{
				tr.gameObject.SetActive(false);
				return;
			}
			NKMCustomPackageElement nkmcustomPackageElement = this.m_lstCurrentSelectableElements[idx];
			NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeRewardTypeData(nkmcustomPackageElement.RewardType, nkmcustomPackageElement.RewardId, nkmcustomPackageElement.TotalRewardCount, 0);
			component.SetData(nkmcustomPackageElement.Index, slotData, false, slotData.eType == NKCUISlot.eSlotMode.ItemMisc, false, new NKCUISlotCustomData.OnClick(this.OnSelectSelectableSlot));
			bool redudantMark = NKCShopManager.WillOverflowOnGain(nkmcustomPackageElement.RewardType, nkmcustomPackageElement.RewardId, nkmcustomPackageElement.TotalRewardCount) || NKCShopManager.IsHaveUnit(nkmcustomPackageElement.RewardType, nkmcustomPackageElement.RewardId);
			component.Slot.SetRedudantMark(redudantMark);
			component.Slot.SetSelected(this.m_lstSelectedItems[this.m_currentPackageSlotIndex] == nkmcustomPackageElement.Index);
			NKCShopManager.ShowShopItemCashCount(component.Slot, slotData, nkmcustomPackageElement.FreeRewardCount, nkmcustomPackageElement.PaidRewardCount);
		}

		// Token: 0x06007BD5 RID: 31701 RVA: 0x00295624 File Offset: 0x00293824
		private void UpdateSelectionItems(bool bResetScrollPosition)
		{
			NKMCustomPackageGroupTemplet selectedCustomPackageGroup = this.GetSelectedCustomPackageGroup();
			if (selectedCustomPackageGroup == null)
			{
				this.m_lstCurrentSelectableElements = null;
				this.m_srSelection.TotalCount = 0;
				this.m_srSelection.RefreshCells(false);
				return;
			}
			this.m_lstCurrentSelectableElements = new List<NKMCustomPackageElement>(selectedCustomPackageGroup.OpenedElements);
			this.m_srSelection.TotalCount = this.m_lstCurrentSelectableElements.Count;
			if (bResetScrollPosition)
			{
				this.m_srSelection.SetIndexPosition(0);
				return;
			}
			this.m_srSelection.RefreshCells(false);
		}

		// Token: 0x06007BD6 RID: 31702 RVA: 0x0029569E File Offset: 0x0029389E
		private void OnSelectSelectableSlot(NKCUISlot.SlotData slotData, bool bLocked, int data)
		{
			this.m_lstSelectedItems[this.m_currentPackageSlotIndex] = data;
			this.UpdateCurrentPackageItems(this.m_targetItemTemplet);
			this.SetDescriptionData(this.m_currentPackageSlotIndex);
			this.UpdateConfirmButton();
			this.m_srSelection.RefreshCells(false);
		}

		// Token: 0x06007BD7 RID: 31703 RVA: 0x002956DC File Offset: 0x002938DC
		private NKMCustomPackageGroupTemplet GetSelectedCustomPackageGroup()
		{
			if (this.m_currentPackageSlotIndex < 0)
			{
				return null;
			}
			if (this.m_currentPackageSlotIndex >= this.m_targetItemTemplet.CustomPackageTemplets.Count)
			{
				return null;
			}
			return this.m_targetItemTemplet.CustomPackageTemplets[this.m_currentPackageSlotIndex];
		}

		// Token: 0x06007BD8 RID: 31704 RVA: 0x00295719 File Offset: 0x00293919
		private void UpdateConfirmButton()
		{
			this.m_csbtnConfirm.SetLock(!NKCShopManager.IsAllCustomSlotSelected(this.m_targetItemTemplet, this.m_lstSelectedItems), false);
		}

		// Token: 0x06007BD9 RID: 31705 RVA: 0x0029573C File Offset: 0x0029393C
		private void OnDetail()
		{
			int index = this.m_lstSelectedItems[this.m_currentPackageSlotIndex];
			NKMCustomPackageElement nkmcustomPackageElement = this.m_targetItemTemplet.CustomPackageTemplets[this.m_currentPackageSlotIndex].Get(index);
			NKM_REWARD_TYPE rewardType = nkmcustomPackageElement.RewardType;
			if (rewardType != NKM_REWARD_TYPE.RT_MISC)
			{
				if (rewardType == NKM_REWARD_TYPE.RT_SKIN)
				{
					NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(nkmcustomPackageElement.RewardId);
					if (skinTemplet != null)
					{
						NKCUIShopSkinPopup.Instance.OpenForSkinInfo(skinTemplet, this.m_cNKMShopItemTemplet.m_ProductID);
						return;
					}
				}
			}
			else
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(nkmcustomPackageElement.RewardId);
				if (itemMiscTempletByID.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_RANDOMBOX && itemMiscTempletByID.IsRatioOpened())
				{
					NKCUISlotListViewer newInstance = NKCUISlotListViewer.GetNewInstance();
					if (newInstance != null)
					{
						newInstance.OpenItemBoxRatio(nkmcustomPackageElement.RewardId);
						return;
					}
				}
				else if (itemMiscTempletByID.IsChoiceItem())
				{
					NKCUISlotListViewer newInstance2 = NKCUISlotListViewer.GetNewInstance();
					if (newInstance2 != null)
					{
						newInstance2.OpenChoiceInfo(nkmcustomPackageElement.RewardId);
					}
				}
			}
		}

		// Token: 0x04006884 RID: 26756
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_shop";

		// Token: 0x04006885 RID: 26757
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_SHOP_BUY_PACKAGE_CUSTOM";

		// Token: 0x04006886 RID: 26758
		private static NKCPopupShopCustomPackage m_Instance;

		// Token: 0x04006887 RID: 26759
		[Header("왼쪽 패키지 내용")]
		public List<NKCUISlotCustomData> m_lstPackageSlot;

		// Token: 0x04006888 RID: 26760
		public Sprite m_spEmpty;

		// Token: 0x04006889 RID: 26761
		[Header("왼쪽 아래 아이템 설명부")]
		public GameObject m_objRootDescItemNotSelected;

		// Token: 0x0400688A RID: 26762
		public GameObject m_objRootDescItemSelected;

		// Token: 0x0400688B RID: 26763
		public Text m_lbItemTitle;

		// Token: 0x0400688C RID: 26764
		public Text m_lbItemDesc;

		// Token: 0x0400688D RID: 26765
		[Header("오른쪽 스크롤바")]
		public NKCUISlotCustomData m_pfbSlot;

		// Token: 0x0400688E RID: 26766
		public LoopScrollRect m_srSelection;

		// Token: 0x0400688F RID: 26767
		[Header("아래쪽 버튼")]
		public NKCUIComStateButton m_csbtnCancel;

		// Token: 0x04006890 RID: 26768
		public NKCUIComStateButton m_csbtnConfirm;

		// Token: 0x04006891 RID: 26769
		[Header("디테일 버튼")]
		public NKCUIComStateButton m_csbtnSkinPreview;

		// Token: 0x04006892 RID: 26770
		public NKCUIComStateButton m_csbtnProbability;

		// Token: 0x04006893 RID: 26771
		public NKCUIComStateButton m_csbtnDetail;

		// Token: 0x04006894 RID: 26772
		private NKCUIShop.OnProductBuyDelegate dOnProductBuy;

		// Token: 0x04006895 RID: 26773
		private List<int> m_lstSelectedItems = new List<int>();

		// Token: 0x04006896 RID: 26774
		private ShopItemTemplet m_cNKMShopItemTemplet;

		// Token: 0x04006897 RID: 26775
		private NKMItemMiscTemplet m_targetItemTemplet;

		// Token: 0x04006898 RID: 26776
		private int m_currentPackageSlotIndex = -1;

		// Token: 0x04006899 RID: 26777
		private int m_customItemStartIndex;

		// Token: 0x0400689A RID: 26778
		private List<NKMCustomPackageElement> m_lstCurrentSelectableElements;
	}
}
