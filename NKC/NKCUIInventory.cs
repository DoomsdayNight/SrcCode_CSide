using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009AB RID: 2475
	public class NKCUIInventory : NKCUIBase
	{
		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x06006713 RID: 26387 RVA: 0x00211048 File Offset: 0x0020F248
		public static NKCUIInventory Instance
		{
			get
			{
				if (NKCUIInventory.m_Instance == null)
				{
					NKCUIInventory.m_Instance = NKCUIManager.OpenNewInstance<NKCUIInventory>("ab_ui_nkm_ui_inventory", "NKM_UI_INVENTORY_V2", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIInventory.CleanupInstance)).GetInstance<NKCUIInventory>();
					NKCUIInventory.m_Instance.InitUI();
				}
				return NKCUIInventory.m_Instance;
			}
		}

		// Token: 0x06006714 RID: 26388 RVA: 0x00211097 File Offset: 0x0020F297
		private static void CleanupInstance()
		{
			NKCUIInventory.m_Instance = null;
		}

		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x06006715 RID: 26389 RVA: 0x0021109F File Offset: 0x0020F29F
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIInventory.m_Instance != null && NKCUIInventory.m_Instance.IsOpen;
			}
		}

		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x06006716 RID: 26390 RVA: 0x002110BA File Offset: 0x0020F2BA
		public static bool IsInstanceLoaded
		{
			get
			{
				return NKCUIInventory.m_Instance != null;
			}
		}

		// Token: 0x06006717 RID: 26391 RVA: 0x002110C7 File Offset: 0x0020F2C7
		public static void CheckInstanceAndClose()
		{
			if (NKCUIInventory.m_Instance != null && NKCUIInventory.m_Instance.IsOpen)
			{
				NKCUIInventory.m_Instance.Close();
			}
		}

		// Token: 0x06006718 RID: 26392 RVA: 0x002110EC File Offset: 0x0020F2EC
		private void OnDestroy()
		{
			NKCUIInventory.m_Instance = null;
		}

		// Token: 0x06006719 RID: 26393 RVA: 0x002110F4 File Offset: 0x0020F2F4
		public static NKCUIInventory OpenNewInstance()
		{
			NKCUIInventory instance = NKCUIManager.OpenNewInstance<NKCUIInventory>("ab_ui_nkm_ui_inventory", "NKM_UI_INVENTORY_V2", NKCUIManager.eUIBaseRect.UIFrontCommon, null).GetInstance<NKCUIInventory>();
			if (instance == null)
			{
				return instance;
			}
			instance.InitUI();
			return instance;
		}

		// Token: 0x0600671A RID: 26394 RVA: 0x00211117 File Offset: 0x0020F317
		public NKCUIInventory.EquipSelectListOptions GetNKCUIInventoryOption()
		{
			return this.m_currentOption;
		}

		// Token: 0x0600671B RID: 26395 RVA: 0x00211120 File Offset: 0x0020F320
		private NKCEquipSortSystem GetEquipSortSystem(NKC_INVENTORY_OPEN_TYPE type)
		{
			if (this.m_dicEquipSortSystem.ContainsKey(type) && this.m_dicEquipSortSystem[type] != null)
			{
				NKCEquipSortSystem nkcequipSortSystem = this.m_dicEquipSortSystem[type];
				nkcequipSortSystem.BuildFilterAndSortedList(this.m_currentOption.setFilterOption, this.m_currentOption.lstSortOption, this.m_currentOption.bHideEquippedItem);
				return nkcequipSortSystem;
			}
			if (this.m_NKC_INVENTORY_TAB != NKCUIInventory.NKC_INVENTORY_TAB.NIT_EQUIP)
			{
				return null;
			}
			switch (type)
			{
			default:
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData != null)
				{
					NKCEquipSortSystem nkcequipSortSystem2 = new NKCEquipSortSystem(nkmuserData, this.m_currentOption.m_EquipListOptions);
					this.m_dicEquipSortSystem[type] = nkcequipSortSystem2;
					return nkcequipSortSystem2;
				}
				return null;
			}
			case NKC_INVENTORY_OPEN_TYPE.NIOT_ITEM_DEV:
			case NKC_INVENTORY_OPEN_TYPE.NIOT_MOLD_DEV:
				return null;
			case NKC_INVENTORY_OPEN_TYPE.NIOT_EQUIP_DEV:
			{
				List<NKMEquipItemData> list = new List<NKMEquipItemData>();
				foreach (KeyValuePair<int, NKMEquipTemplet> keyValuePair in NKMItemManager.m_dicItemEquipTempletByID)
				{
					NKMEquipItemData item = NKCEquipSortSystem.MakeTempEquipData(keyValuePair.Value.m_ItemEquipID, 0, false);
					list.Add(item);
				}
				NKCEquipSortSystem nkcequipSortSystem2 = new NKCEquipSortSystem(NKCScenManager.CurrentUserData(), this.m_currentOption.m_EquipListOptions, list);
				this.m_dicEquipSortSystem[type] = nkcequipSortSystem2;
				return nkcequipSortSystem2;
			}
			}
		}

		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x0600671D RID: 26397 RVA: 0x0021136B File Offset: 0x0020F56B
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				if (this.m_currentOption.m_NKC_INVENTORY_OPEN_TYPE == NKC_INVENTORY_OPEN_TYPE.NIOT_ITEM_DEV || this.m_currentOption.m_NKC_INVENTORY_OPEN_TYPE == NKC_INVENTORY_OPEN_TYPE.NIOT_EQUIP_DEV || this.m_currentOption.m_NKC_INVENTORY_OPEN_TYPE == NKC_INVENTORY_OPEN_TYPE.NIOT_MOLD_DEV)
				{
					return NKCUIUpsideMenu.eMode.Disable;
				}
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x0600671E RID: 26398 RVA: 0x0021139C File Offset: 0x0020F59C
		public override string MenuName
		{
			get
			{
				if (this.m_currentOption.m_NKC_INVENTORY_OPEN_TYPE == NKC_INVENTORY_OPEN_TYPE.NIOT_NORMAL)
				{
					return NKCUtilString.GET_STRING_INVEN;
				}
				if (this.m_currentOption.strUpsideMenuName.Length > 0)
				{
					return this.m_currentOption.strUpsideMenuName;
				}
				if (this.m_currentOption.m_NKC_INVENTORY_OPEN_TYPE == NKC_INVENTORY_OPEN_TYPE.NIOT_EQUIP_SELECT)
				{
					return NKCUtilString.GET_STRING_INVEN_EQUIP_SELECT;
				}
				return NKCUtilString.GET_STRING_INVEN;
			}
		}

		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x0600671F RID: 26399 RVA: 0x002113F4 File Offset: 0x0020F5F4
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x06006720 RID: 26400 RVA: 0x002113F7 File Offset: 0x0020F5F7
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_SYSTEM_INVENTORY";
			}
		}

		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x06006721 RID: 26401 RVA: 0x002113FE File Offset: 0x0020F5FE
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return this.RESOURCE_LIST;
			}
		}

		// Token: 0x06006722 RID: 26402 RVA: 0x00211408 File Offset: 0x0020F608
		private void InitUI()
		{
			this.m_NKM_UI_UNIT_INVENTORY = base.gameObject;
			this.m_NKM_UI_UNIT_INVENTORY.SetActive(false);
			this.m_btnFilterOption.PointerClick.RemoveAllListeners();
			this.m_btnFilterOption.PointerClick.AddListener(new UnityAction(this.OnClickFilterBtn));
			this.m_cbtnSortTypeMenu.OnValueChanged.RemoveAllListeners();
			this.m_cbtnSortTypeMenu.OnValueChanged.AddListener(new UnityAction<bool>(this.OnSortMenuOpen));
			this.m_cbtnFinishMultiSelect.PointerClick.RemoveAllListeners();
			this.m_cbtnFinishMultiSelect.PointerClick.AddListener(new UnityAction(this.FinishMultiSelection));
			this.m_NKM_UI_INVENTORY_MENU_CANCEL_BUTTON.PointerClick.RemoveAllListeners();
			this.m_NKM_UI_INVENTORY_MENU_CANCEL_BUTTON.PointerClick.AddListener(new UnityAction(this.OnTouchMultiCancel));
			this.NKM_UI_INVENTORY_MENU_AUTO_BUTTON.PointerClick.RemoveAllListeners();
			this.NKM_UI_INVENTORY_MENU_AUTO_BUTTON.PointerClick.AddListener(new UnityAction(this.OnTouchAutoSelect));
			this.m_NKM_UI_INVENTORY_ADD.PointerClick.RemoveAllListeners();
			this.m_NKM_UI_INVENTORY_ADD.PointerClick.AddListener(new UnityAction(this.OnExpandInventoryPopup));
			this.m_NKM_UI_INVENTORY_TAP_MISC.PointerClick.RemoveAllListeners();
			this.m_NKM_UI_INVENTORY_TAP_MISC.PointerClick.AddListener(new UnityAction(this.OnSelectMiscTab));
			this.m_NKM_UI_INVENTORY_TAP_GEAR.PointerClick.RemoveAllListeners();
			this.m_NKM_UI_INVENTORY_TAP_GEAR.PointerClick.AddListener(new UnityAction(this.OnSelectEquipTab));
			this.m_ctgDescending.OnValueChanged.RemoveAllListeners();
			this.m_ctgDescending.OnValueChanged.AddListener(new UnityAction<bool>(this.OnCheckAscend));
			this.m_NKM_UI_INVENTORY_MENU_LOCK.OnValueChanged.RemoveAllListeners();
			this.m_NKM_UI_INVENTORY_MENU_LOCK.OnValueChanged.AddListener(new UnityAction<bool>(this.OnLockModeToggle));
			this.m_NKM_UI_INVENTORY_MENU_DELETE.PointerClick.RemoveAllListeners();
			this.m_NKM_UI_INVENTORY_MENU_DELETE.PointerClick.AddListener(delegate()
			{
				this.OnRemoveMode(true);
			});
			NKCUIComStateButton equipButton = this.m_EquipButton;
			if (equipButton != null)
			{
				equipButton.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton equipButton2 = this.m_EquipButton;
			if (equipButton2 != null)
			{
				equipButton2.PointerClick.AddListener(new UnityAction(this.OpenUnitSelect));
			}
			NKCUIComStateButton unEquipButton = this.m_UnEquipButton;
			if (unEquipButton != null)
			{
				unEquipButton.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton unEquipButton2 = this.m_UnEquipButton;
			if (unEquipButton2 != null)
			{
				unEquipButton2.PointerClick.AddListener(new UnityAction(this.OnClickUnEquip));
			}
			NKCUIComStateButton reinforceButton = this.m_ReinforceButton;
			if (reinforceButton != null)
			{
				reinforceButton.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton reinforceButton2 = this.m_ReinforceButton;
			if (reinforceButton2 != null)
			{
				reinforceButton2.PointerClick.AddListener(new UnityAction(this.OnClickEquipEnhance));
			}
			NKCUIComStateButton reinforceButtonLock = this.m_ReinforceButtonLock;
			if (reinforceButtonLock != null)
			{
				reinforceButtonLock.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton reinforceButtonLock2 = this.m_ReinforceButtonLock;
			if (reinforceButtonLock2 != null)
			{
				reinforceButtonLock2.PointerClick.AddListener(new UnityAction(this.OnClickEquipEnhance));
			}
			NKCUIComStateButton changeButton = this.m_ChangeButton;
			if (changeButton != null)
			{
				changeButton.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton changeButton2 = this.m_ChangeButton;
			if (changeButton2 != null)
			{
				changeButton2.PointerClick.AddListener(new UnityAction(this.OpenUnitSelect));
			}
			NKCUIComStateButton okButton = this.m_OkButton;
			if (okButton != null)
			{
				okButton.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton okButton2 = this.m_OkButton;
			if (okButton2 != null)
			{
				okButton2.PointerClick.AddListener(new UnityAction(this.OnClickOkButton));
			}
			this.m_LoopScrollRect.dOnGetObject += this.GetSlot;
			this.m_LoopScrollRect.dOnReturnObject += this.ReturnSlot;
			this.m_LoopScrollRect.dOnProvideData += this.ProvideSlotData;
			this.m_LoopScrollRect.dOnRepopulate += this.CalculateContentRectSize;
			NKCUtil.SetScrollHotKey(this.m_LoopScrollRect, null);
			this.m_LoopScrollRectEquip.dOnGetObject += this.GetSlot;
			this.m_LoopScrollRectEquip.dOnReturnObject += this.ReturnSlot;
			this.m_LoopScrollRectEquip.dOnProvideData += this.ProvideSlotData;
			this.m_LoopScrollRectEquip.dOnRepopulate += this.CalculateContentRectSize;
			NKCUtil.SetScrollHotKey(this.m_LoopScrollRectEquip, null);
		}

		// Token: 0x06006723 RID: 26403 RVA: 0x00211834 File Offset: 0x0020FA34
		private void CalculateContentRectSize()
		{
			int minColumn = 7;
			Vector2 cellSize = this.m_vUISlotSize;
			Vector2 spacing = this.m_vUISlotSpacing;
			float num = this.m_vOffsetX;
			if (this.m_NKC_INVENTORY_TAB == NKCUIInventory.NKC_INVENTORY_TAB.NIT_EQUIP)
			{
				minColumn = 5;
				cellSize = this.m_vEquipSlotSize;
				spacing = this.m_vEquipSlotSpacing;
				num = this.m_vEquipOffsetX;
				this.m_NKM_UI_INVENTORY_LIST_ITEM_EQUIP.offsetMin = new Vector2(num, this.m_NKM_UI_INVENTORY_LIST_ITEM_EQUIP.offsetMin.y);
				this.m_NKM_UI_INVENTORY_LIST_ITEM_EQUIP.offsetMax = new Vector2(-num, this.m_NKM_UI_INVENTORY_LIST_ITEM_EQUIP.offsetMax.y);
				if (this.m_safeAreaEquip != null)
				{
					this.m_safeAreaEquip.SetSafeAreaBase();
				}
				NKCUtil.CalculateContentRectSize(this.m_LoopScrollRectEquip, this.m_GridLayoutGroupEquip, minColumn, cellSize, spacing, false);
				Debug.Log(string.Format("CellSize : {0}, rectContentWidth : {1}", this.m_GridLayoutGroup.cellSize, this.m_rectContentRectEquip.GetWidth()));
				return;
			}
			this.m_NKM_UI_INVENTORY_LIST_ITEM.offsetMin = new Vector2(num, this.m_NKM_UI_INVENTORY_LIST_ITEM.offsetMin.y);
			this.m_NKM_UI_INVENTORY_LIST_ITEM.offsetMax = new Vector2(-num, this.m_NKM_UI_INVENTORY_LIST_ITEM.offsetMax.y);
			if (this.m_safeArea != null)
			{
				this.m_safeArea.SetSafeAreaBase();
			}
			NKCUtil.CalculateContentRectSize(this.m_LoopScrollRect, this.m_GridLayoutGroup, minColumn, cellSize, spacing, false);
			Debug.Log(string.Format("CellSize : {0}, rectContentWidth : {1}", this.m_GridLayoutGroup.cellSize, this.m_rectContentRect.GetWidth()));
		}

		// Token: 0x06006724 RID: 26404 RVA: 0x002119C0 File Offset: 0x0020FBC0
		private RectTransform GetSlot(int index)
		{
			if (this.m_NKC_INVENTORY_TAB == NKCUIInventory.NKC_INVENTORY_TAB.NIT_EQUIP)
			{
				if (this.m_stkEquipSlotPool.Count > 0)
				{
					NKCUISlotEquip nkcuislotEquip = this.m_stkEquipSlotPool.Pop();
					NKCUtil.SetGameobjectActive(nkcuislotEquip, true);
					this.m_lstVisibleEquipSlot.Add(nkcuislotEquip);
					return nkcuislotEquip.GetComponent<RectTransform>();
				}
				NKCUISlotEquip nkcuislotEquip2 = UnityEngine.Object.Instantiate<NKCUISlotEquip>(this.m_pfbEquipSlot);
				nkcuislotEquip2.transform.SetParent(this.m_rectSlotPoolRectEquip);
				NKCUtil.SetGameobjectActive(nkcuislotEquip2, true);
				this.m_lstVisibleEquipSlot.Add(nkcuislotEquip2);
				return nkcuislotEquip2.GetComponent<RectTransform>();
			}
			else
			{
				if (this.m_NKC_INVENTORY_TAB != NKCUIInventory.NKC_INVENTORY_TAB.NIT_MISC && this.m_NKC_INVENTORY_TAB != NKCUIInventory.NKC_INVENTORY_TAB.NIT_MOLD)
				{
					return null;
				}
				if (this.m_stkMiscSlotPool.Count > 0)
				{
					NKCUISlot nkcuislot = this.m_stkMiscSlotPool.Pop();
					nkcuislot.transform.SetParent(this.m_rectSlotPoolRect);
					NKCUtil.SetGameobjectActive(nkcuislot, true);
					this.m_lstVisibleMiscSlot.Add(nkcuislot);
					return nkcuislot.GetComponent<RectTransform>();
				}
				NKCUISlot nkcuislot2 = UnityEngine.Object.Instantiate<NKCUISlot>(this.m_pfbUISlot);
				nkcuislot2.Init();
				NKCUtil.SetGameobjectActive(nkcuislot2, true);
				this.m_lstVisibleMiscSlot.Add(nkcuislot2);
				return nkcuislot2.GetComponent<RectTransform>();
			}
		}

		// Token: 0x06006725 RID: 26405 RVA: 0x00211AC4 File Offset: 0x0020FCC4
		private void ReturnSlot(NKCUIInventory.NKC_INVENTORY_TAB oldType)
		{
			if (oldType == NKCUIInventory.NKC_INVENTORY_TAB.NIT_EQUIP)
			{
				for (int i = 0; i < this.m_lstVisibleEquipSlot.Count; i++)
				{
					this.m_lstVisibleEquipSlot[i].transform.SetParent(this.m_rectSlotPoolRectEquip);
					NKCUtil.SetGameobjectActive(this.m_lstVisibleEquipSlot[i].gameObject, false);
					this.m_stkEquipSlotPool.Push(this.m_lstVisibleEquipSlot[i]);
				}
				this.m_lstVisibleEquipSlot.Clear();
				return;
			}
			if (oldType == NKCUIInventory.NKC_INVENTORY_TAB.NIT_MISC || oldType == NKCUIInventory.NKC_INVENTORY_TAB.NIT_MOLD)
			{
				for (int j = 0; j < this.m_lstVisibleMiscSlot.Count; j++)
				{
					this.m_lstVisibleMiscSlot[j].transform.SetParent(this.m_rectSlotPoolRect);
					NKCUtil.SetGameobjectActive(this.m_lstVisibleMiscSlot[j].gameObject, false);
					this.m_stkMiscSlotPool.Push(this.m_lstVisibleMiscSlot[j]);
				}
				this.m_lstVisibleMiscSlot.Clear();
			}
		}

		// Token: 0x06006726 RID: 26406 RVA: 0x00211BB4 File Offset: 0x0020FDB4
		private void ReturnSlot(Transform go)
		{
			NKCUtil.SetGameobjectActive(go.gameObject, false);
			if (this.m_NKC_INVENTORY_TAB == NKCUIInventory.NKC_INVENTORY_TAB.NIT_EQUIP)
			{
				go.SetParent(this.m_rectSlotPoolRectEquip);
				NKCUISlotEquip component = go.GetComponent<NKCUISlotEquip>();
				if (component != null)
				{
					this.m_lstVisibleEquipSlot.Remove(component);
					this.m_stkEquipSlotPool.Push(component);
					return;
				}
			}
			else if (this.m_NKC_INVENTORY_TAB == NKCUIInventory.NKC_INVENTORY_TAB.NIT_MISC || this.m_NKC_INVENTORY_TAB == NKCUIInventory.NKC_INVENTORY_TAB.NIT_MOLD)
			{
				go.SetParent(this.m_rectSlotPoolRect);
				NKCUISlot component2 = go.GetComponent<NKCUISlot>();
				if (component2 != null)
				{
					this.m_lstVisibleMiscSlot.Remove(component2);
					this.m_stkMiscSlotPool.Push(component2);
				}
			}
		}

		// Token: 0x06006727 RID: 26407 RVA: 0x00211C54 File Offset: 0x0020FE54
		private void ProvideSlotData(Transform tr, int idx)
		{
			if (this.m_NKC_INVENTORY_TAB == NKCUIInventory.NKC_INVENTORY_TAB.NIT_MISC)
			{
				NKMItemMiscData nkmitemMiscData = this.m_lstMiscData[idx];
				NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeMiscItemData(nkmitemMiscData, 0);
				NKCUISlot component = tr.GetComponent<NKCUISlot>();
				component.SetUsable(false);
				if (this.m_eInventoryOpenType == NKC_INVENTORY_OPEN_TYPE.NIOT_NORMAL)
				{
					NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(nkmitemMiscData.ItemID);
					if (itemMiscTempletByID != null && itemMiscTempletByID.IsUsable())
					{
						component.SetData(data, true, true, true, new NKCUISlot.OnClick(this.OpenItemUsePopup));
						component.SetUsable(true);
					}
					else
					{
						component.SetData(data, true, true, true, null);
						component.SetOpenItemBoxOnClick();
					}
				}
				else if (this.m_eInventoryOpenType == NKC_INVENTORY_OPEN_TYPE.NIOT_ITEM_DEV)
				{
					component.SetData(data, true, true, true, this.m_currentOption.m_dOnClickItemSlot);
				}
				NKCUtil.SetGameobjectActive(component.gameObject, true);
				return;
			}
			if (this.m_NKC_INVENTORY_TAB == NKCUIInventory.NKC_INVENTORY_TAB.NIT_MOLD)
			{
				NKCUISlot.SlotData data2 = NKCUISlot.SlotData.MakeMoldItemData(this.m_lstMoldTemplet[idx].m_MoldID, 1L);
				NKCUISlot component2 = tr.GetComponent<NKCUISlot>();
				component2.SetUsable(false);
				if (this.m_eInventoryOpenType == NKC_INVENTORY_OPEN_TYPE.NIOT_MOLD_DEV)
				{
					component2.SetData(data2, true, true, true, this.m_currentOption.m_dOnClickItemMoldSlot);
					return;
				}
			}
			else if (this.m_NKC_INVENTORY_TAB == NKCUIInventory.NKC_INVENTORY_TAB.NIT_EQUIP)
			{
				if (this.m_ssActive == null)
				{
					Debug.LogError("Slot Sort System Null!!");
					return;
				}
				bool bValue = true;
				NKCUIInventory.EquipSelectListOptions currentOption = this.m_currentOption;
				NKCUISlotEquip component3 = tr.GetComponent<NKCUISlotEquip>();
				NKMEquipItemData nkmequipItemData = new NKMEquipItemData();
				if (this.m_currentOption.m_dOnClickEmptySlot != null)
				{
					if (idx == 0)
					{
						NKCUtil.SetGameobjectActive(component3.gameObject, bValue);
						if (this.m_currentOption.lastSelectedItemUID > 0L)
						{
							NKMEquipItemData itemEquip = NKCScenManager.CurrentUserData().m_InventoryData.GetItemEquip(this.m_currentOption.lastSelectedItemUID);
							component3.SetEmpty(new NKCUISlotEquip.OnSelectedEquipSlot(this.OnSelectedSlot), itemEquip);
							if (this.m_LatestSelectedSlot == null && this.m_LatestOpenNKMEquipItemData == null)
							{
								this.m_LatestOpenNKMEquipItemData = itemEquip;
								this.m_LatestSelectedSlot = component3;
								this.SetEquipInfo(component3);
							}
						}
						return;
					}
					idx--;
				}
				if (this.m_ssActive.SortedEquipList.Count <= idx)
				{
					return;
				}
				nkmequipItemData = this.m_ssActive.SortedEquipList[idx];
				NKCUtil.SetGameobjectActive(component3.gameObject, bValue);
				NKCUIInvenEquipSlot.EQUIP_SLOT_STATE equip_SLOT_STATE = NKCUIInvenEquipSlot.EQUIP_SLOT_STATE.ESS_NONE;
				if (this.m_eInventoryOpenType == NKC_INVENTORY_OPEN_TYPE.NIOT_EQUIP_SELECT)
				{
					bool bPresetContained = NKCEquipPresetDataManager.HSPresetEquipUId.Contains(nkmequipItemData.m_ItemUid);
					component3.SetData(nkmequipItemData, new NKCUISlotEquip.OnSelectedEquipSlot(this.OnSelectedSlot), this.m_bLockMaxItem, this.m_currentOption.bSkipItemEquipBox, this.m_currentOption.bShowFierceUI, bPresetContained);
					component3.SetSelected(!this.m_currentOption.bMultipleSelect && this.m_LatestOpenNKMEquipItemData == null);
					if ((this.m_currentOption.m_hsSelectedEquipUIDToShow != null && this.m_currentOption.m_hsSelectedEquipUIDToShow.Contains(nkmequipItemData.m_ItemUid)) || this.m_hsCurrentSelectedEquips.Contains(nkmequipItemData.m_ItemUid))
					{
						if (this.m_currentOption.bShowRemoveItem)
						{
							equip_SLOT_STATE = NKCUIInvenEquipSlot.EQUIP_SLOT_STATE.ESS_DELETE;
						}
						else
						{
							equip_SLOT_STATE = NKCUIInvenEquipSlot.EQUIP_SLOT_STATE.ESS_SELECTED;
						}
					}
					else if (!this.m_currentOption.bMultipleSelect && (this.m_LatestOpenNKMEquipItemData == null || this.m_LatestOpenNKMEquipItemData.m_ItemUid == nkmequipItemData.m_ItemUid))
					{
						equip_SLOT_STATE = NKCUIInvenEquipSlot.EQUIP_SLOT_STATE.ESS_SELECTED;
					}
				}
				else if (this.m_eInventoryOpenType == NKC_INVENTORY_OPEN_TYPE.NIOT_EQUIP_DEV)
				{
					component3.SetData(nkmequipItemData, new NKCUISlotEquip.OnSelectedEquipSlot(this.OnSelectedSlot), false, false, false, false);
					if (this.m_LatestOpenNKMEquipItemData == null || this.m_LatestOpenNKMEquipItemData.m_ItemUid == nkmequipItemData.m_ItemUid)
					{
						equip_SLOT_STATE = NKCUIInvenEquipSlot.EQUIP_SLOT_STATE.ESS_SELECTED;
					}
				}
				else
				{
					bool bPresetContained2 = NKCEquipPresetDataManager.HSPresetEquipUId.Contains(nkmequipItemData.m_ItemUid);
					component3.SetData(nkmequipItemData, new NKCUISlotEquip.OnSelectedEquipSlot(this.OnSelectedSlot), false, false, false, bPresetContained2);
					if (this.m_LatestOpenNKMEquipItemData == null || this.m_LatestOpenNKMEquipItemData.m_ItemUid == nkmequipItemData.m_ItemUid)
					{
						equip_SLOT_STATE = NKCUIInvenEquipSlot.EQUIP_SLOT_STATE.ESS_SELECTED;
					}
				}
				component3.SetSlotState(equip_SLOT_STATE);
				component3.SetLock(nkmequipItemData.m_bLock, this.m_currentOption.bEnableLockEquipSystem);
				if (this.m_currentOption.bShowEquipUpgradeState)
				{
					component3.SetUpgradeSlotState(NKMItemManager.CanUpgradeEquipByCoreID(nkmequipItemData));
				}
				if (equip_SLOT_STATE == NKCUIInvenEquipSlot.EQUIP_SLOT_STATE.ESS_SELECTED)
				{
					this.m_LatestOpenNKMEquipItemData = nkmequipItemData;
					this.m_LatestSelectedSlot = component3;
					component3.SetSlotState(NKCUIInvenEquipSlot.EQUIP_SLOT_STATE.ESS_SELECTED);
					this.SetEquipInfo(component3);
				}
			}
		}

		// Token: 0x06006728 RID: 26408 RVA: 0x00212058 File Offset: 0x00210258
		private void OnClickOkButton()
		{
			if (this.m_currentOption.m_ButtonMenuType == NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_CHANGE || this.m_currentOption.m_ButtonMenuType == NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_PRESET_CHANGE)
			{
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(this.m_LatestOpenNKMEquipItemData.m_ItemEquipID);
				this.CheckEquipChange(equipTemplet.m_ItemEquipPosition);
			}
		}

		// Token: 0x06006729 RID: 26409 RVA: 0x002120A0 File Offset: 0x002102A0
		private void OnSelectedSlot(NKCUISlotEquip slot, NKMEquipItemData equipData)
		{
			if (this.m_currentOption.bShowRemoveSlot || this.m_currentOption.bEnableLockEquipSystem || !this.m_currentOption.bMultipleSelect)
			{
				NKCUISlotEquip latestSelectedSlot = this.m_LatestSelectedSlot;
				if (latestSelectedSlot != null)
				{
					latestSelectedSlot.SetSelected(false);
				}
			}
			if (this.m_currentOption.bMultipleSelect)
			{
				this.ToggleSelectedState(slot, equipData);
			}
			else
			{
				slot.SetSelected(true);
			}
			this.m_LatestSelectedSlot = slot;
			this.m_LatestOpenNKMEquipItemData = slot.GetNKMEquipItemData();
			if (this.m_currentOption.bEnableLockEquipSystem)
			{
				NKCPacketSender.Send_NKMPacket_LOCK_ITEM_REQ(this.m_LatestOpenNKMEquipItemData.m_ItemUid, !this.m_LatestOpenNKMEquipItemData.m_bLock);
			}
			this.SetEquipInfo(slot);
		}

		// Token: 0x0600672A RID: 26410 RVA: 0x00212149 File Offset: 0x00210349
		public void SetLatestOpenNKMEquipItemDataAndOpenUnitSelect(NKMEquipItemData equipItemData)
		{
			this.m_LatestOpenNKMEquipItemData = equipItemData;
			this.OpenUnitSelect();
		}

		// Token: 0x0600672B RID: 26411 RVA: 0x00212158 File Offset: 0x00210358
		private void OpenUnitSelect()
		{
			NKCPopupItemEquipBox.CloseItemBox();
			NKCUIUnitSelectList.UnitSelectListOptions options = new NKCUIUnitSelectList.UnitSelectListOptions(NKM_UNIT_TYPE.NUT_NORMAL, true, NKM_DECK_TYPE.NDT_NORMAL, NKCUIUnitSelectList.eUnitSelectListMode.Normal, true);
			if (this.m_LatestOpenNKMEquipItemData != null)
			{
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(this.m_LatestOpenNKMEquipItemData.m_ItemEquipID);
				if (equipTemplet != null)
				{
					options.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>
					{
						NKCUnitSortSystem.GetFilterOption(equipTemplet.m_EquipUnitStyleType)
					};
					if (this.m_LatestOpenNKMEquipItemData.m_OwnerUnitUID > 0L)
					{
						options.m_SortOptions.setExcludeUnitUID = new HashSet<long>
						{
							this.m_LatestOpenNKMEquipItemData.m_OwnerUnitUID
						};
					}
					if (equipTemplet.IsPrivateEquip())
					{
						options.m_SortOptions.setOnlyIncludeUnitBaseID = new HashSet<int>(equipTemplet.PrivateUnitList);
					}
				}
			}
			options.lstSortOption = new List<NKCUnitSortSystem.eSortOption>
			{
				NKCUnitSortSystem.eSortOption.Level_High
			};
			options.bDescending = false;
			options.bShowRemoveSlot = false;
			options.bMultipleSelect = false;
			options.iMaxMultipleSelect = 0;
			options.bExcludeLockedUnit = false;
			options.bExcludeDeckedUnit = false;
			options.bShowHideDeckedUnitMenu = true;
			options.bHideDeckedUnit = false;
			options.m_SortOptions.bIgnoreCityState = true;
			options.m_SortOptions.bIgnoreWorldMapLeader = true;
			options.setUnitFilterCategory = NKCUnitSortSystem.setDefaultUnitFilterCategory;
			options.setUnitSortCategory = NKCUnitSortSystem.setDefaultUnitSortCategory;
			options.m_bUseFavorite = true;
			options.strEmptyMessage = NKCUtilString.GET_STRING_INVEN_THERE_IS_NO_UNIT_TO_EQUIP;
			NKCUIUnitSelectList.Instance.Open(options, new NKCUIUnitSelectList.OnUnitSelectCommand(this.OnSelectedUnitToEquip), null, null, null, null);
		}

		// Token: 0x0600672C RID: 26412 RVA: 0x002122B8 File Offset: 0x002104B8
		private void OnSelectedUnitToEquip(List<long> lstUnitUID)
		{
			if (lstUnitUID.Count != 1)
			{
				Debug.LogError("Fatal Error : OnSelectedUnitToEquip returned wrong list");
				return;
			}
			long num = lstUnitUID[0];
			if (this.m_LatestOpenNKMEquipItemData == null)
			{
				return;
			}
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(this.m_LatestOpenNKMEquipItemData.m_ItemEquipID);
			if (equipTemplet == null)
			{
				return;
			}
			NKMUnitData unitFromUID = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetUnitFromUID(num);
			if (unitFromUID == null)
			{
				return;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = equipTemplet.CanEquipByUnit(NKCScenManager.GetScenManager().GetMyUserData(), unitFromUID);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupMessageManager.AddPopupMessage(nkm_ERROR_CODE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			this.m_LatestTargetUnitUIDToEquip = num;
			this.m_currentOption.equipChangeTargetPosition = equipTemplet.m_ItemEquipPosition;
			this.OpenChangeBoxOrChangeDirectIfEmpty();
		}

		// Token: 0x0600672D RID: 26413 RVA: 0x0021235C File Offset: 0x0021055C
		private void OnClickUnEquip()
		{
			NKMEquipItemData itemEquip = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(this.m_LatestOpenNKMEquipItemData.m_ItemUid);
			if (itemEquip == null)
			{
				return;
			}
			if (itemEquip.m_OwnerUnitUID > 0L)
			{
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID);
				if (equipTemplet == null)
				{
					return;
				}
				NKMUnitData unitFromUID = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetUnitFromUID(itemEquip.m_OwnerUnitUID);
				if (unitFromUID == null)
				{
					return;
				}
				NKM_ERROR_CODE nkm_ERROR_CODE = equipTemplet.CanUnEquipByUnit(NKCScenManager.GetScenManager().GetMyUserData(), unitFromUID);
				if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
				{
					NKCPopupMessageManager.AddPopupMessage(nkm_ERROR_CODE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
				ITEM_EQUIP_POSITION itemEquipPosition = NKMItemManager.GetItemEquipPosition(itemEquip.m_ItemUid);
				NKCPacketSender.Send_NKMPacket_EQUIP_ITEM_EQUIP_REQ(false, itemEquip.m_OwnerUnitUID, itemEquip.m_ItemUid, itemEquipPosition);
			}
		}

		// Token: 0x0600672E RID: 26414 RVA: 0x0021240C File Offset: 0x0021060C
		public void OnClickEquipEnhance()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_ENCHANT, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.FACTORY_ENCHANT, 0);
				return;
			}
			if (this.m_LatestOpenNKMEquipItemData != null)
			{
				NKM_ERROR_CODE nkm_ERROR_CODE = NKMItemManager.CanEnchantItem(NKCScenManager.GetScenManager().GetMyUserData(), this.m_LatestOpenNKMEquipItemData);
				if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
				{
					NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString(nkm_ERROR_CODE.ToString(), false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
				NKCUIForge.Instance.Open(NKCUIForge.NKC_FORGE_TAB.NFT_ENCHANT, this.m_LatestOpenNKMEquipItemData.m_ItemUid, this.m_currentOption.setFilterOption);
			}
		}

		// Token: 0x0600672F RID: 26415 RVA: 0x00212494 File Offset: 0x00210694
		private void OpenItemUsePopup(NKCUISlot.SlotData slotData, bool bLocked)
		{
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(slotData.ID);
			if (itemMiscTempletByID != null)
			{
				if (itemMiscTempletByID.IsChoiceItem())
				{
					NKCPopupItemBox.Instance.Open(NKCPopupItemBox.eMode.Choice, slotData, null, false, false, true);
					return;
				}
				if (itemMiscTempletByID.IsContractItem)
				{
					NKCPopupMiscUseCount.Instance.Open(NKCPopupMiscUseCount.USE_ITEM_TYPE.Common, slotData.ID, slotData, delegate(int itemId, int count)
					{
						NKCPacketSender.Send_NKMPacket_MISC_CONTRACT_OPEN_REQ(itemId, count);
					});
					return;
				}
				NKCPopupMiscUseCount.Instance.Open(NKCPopupMiscUseCount.USE_ITEM_TYPE.Common, slotData.ID, slotData, delegate(int itemId, int count)
				{
					NKCPacketSender.Send_NKMPacket_RANDOM_ITEM_BOX_OPEN_REQ(itemId, count);
				});
			}
		}

		// Token: 0x06006730 RID: 26416 RVA: 0x00212538 File Offset: 0x00210738
		public void ToggleSelectedState(NKCUISlotEquip cItemSlot, NKMEquipItemData equipData)
		{
			if (cItemSlot != null && equipData != null)
			{
				if (cItemSlot.Get_EQUIP_SLOT_STATE() == NKCUIInvenEquipSlot.EQUIP_SLOT_STATE.ESS_NONE)
				{
					if (this.m_hsCurrentSelectedEquips != null && this.m_hsCurrentSelectedEquips.Count < this.m_currentOption.iMaxMultipleSelect)
					{
						if (this.m_currentOption.bShowRemoveItem)
						{
							cItemSlot.SetSlotState(NKCUIInvenEquipSlot.EQUIP_SLOT_STATE.ESS_DELETE);
						}
						else
						{
							cItemSlot.SetSlotState(NKCUIInvenEquipSlot.EQUIP_SLOT_STATE.ESS_SELECTED);
						}
						this.m_hsCurrentSelectedEquips.Add(equipData.m_ItemUid);
					}
				}
				else if (cItemSlot.Get_EQUIP_SLOT_STATE() == NKCUIInvenEquipSlot.EQUIP_SLOT_STATE.ESS_SELECTED || cItemSlot.Get_EQUIP_SLOT_STATE() == NKCUIInvenEquipSlot.EQUIP_SLOT_STATE.ESS_DELETE)
				{
					cItemSlot.SetSlotState(NKCUIInvenEquipSlot.EQUIP_SLOT_STATE.ESS_NONE);
					if (this.m_hsCurrentSelectedEquips != null)
					{
						this.m_hsCurrentSelectedEquips.Remove(equipData.m_ItemUid);
					}
				}
			}
			this.UpdateSelectedEquipCountUI();
			this.UpdateSelectedEquipBreakupResult();
		}

		// Token: 0x06006731 RID: 26417 RVA: 0x002125F0 File Offset: 0x002107F0
		private void SortByItemName(bool bDescending)
		{
			if (bDescending)
			{
				NKCUIInventory.CompDescending comparer = new NKCUIInventory.CompDescending();
				this.m_lstMiscData.Sort(comparer);
			}
			else
			{
				NKCUIInventory.CompAscending comparer2 = new NKCUIInventory.CompAscending();
				this.m_lstMiscData.Sort(comparer2);
			}
			this.ProcessUIFromCurrentDisplayedSortData(true);
		}

		// Token: 0x06006732 RID: 26418 RVA: 0x00212630 File Offset: 0x00210830
		public void Load()
		{
			foreach (KeyValuePair<int, NKMItemMiscData> keyValuePair in NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.MiscItems)
			{
				NKMItemMiscData value = keyValuePair.Value;
				if (value != null)
				{
					NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(value.ItemID);
					if (itemMiscTempletByID == null)
					{
						Log.Error(string.Format("NKMItemMiscTemplet 찾을 수 없음 - id {0}", value.ItemID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUIInventory.cs", 1271);
					}
					else if (!itemMiscTempletByID.IsHideInInven())
					{
						NKCResourceUtility.PreloadMiscItemIcon(itemMiscTempletByID, true);
					}
				}
			}
		}

		// Token: 0x06006733 RID: 26419 RVA: 0x002126B8 File Offset: 0x002108B8
		public void Open(NKCUIInventory.EquipSelectListOptions options, List<int> lstUpsideMenuResource = null, long latestTargetUnitUIDToEquip = 0L, NKCUIInventory.NKC_INVENTORY_TAB openTab = NKCUIInventory.NKC_INVENTORY_TAB.NIT_NONE)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_INVENTORY_EQUIPMENTS_UNLOCK, false);
			this.m_hsCurrentSelectedEquips.Clear();
			this.m_ssActive = null;
			if (this.m_currentOption.lEquipOptionCachingByUnitUID != 0L && this.m_currentOption.lEquipOptionCachingByUnitUID == options.lEquipOptionCachingByUnitUID)
			{
				options.lstSortOption = this.m_currentOption.lstSortOption;
				if (this.m_currentOption.setFilterOption.Count > 0)
				{
					foreach (NKCEquipSortSystem.eFilterOption eFilterOption in this.m_currentOption.setFilterOption)
					{
						if (eFilterOption - NKCEquipSortSystem.eFilterOption.Equip_Counter > 5)
						{
							options.setFilterOption.Add(eFilterOption);
						}
					}
				}
			}
			this.m_currentOption = options;
			this.m_dOnClickEquipSlot = options.m_dOnSelectedEquipSlot;
			this.m_dOnGetItemListAfterSelected = options.m_dOnGetItemListAfterSelected;
			this.m_bLockMaxItem = options.bLockMaxItem;
			this.m_LatestTargetUnitUIDToEquip = latestTargetUnitUIDToEquip;
			this.m_NKM_UI_UNIT_INVENTORY.SetActive(true);
			this.m_lbEmptyMessage.text = options.strEmptyMessage;
			if (this.m_currentOption.bMultipleSelect)
			{
				this.SetOnClickEquipSlot(new NKCUISlotEquip.OnSelectedEquipSlot(this.ToggleSelectedState));
			}
			else if (this.m_currentOption.iPresetIndex >= 0)
			{
				this.SetOnClickEquipSlot(new NKCUISlotEquip.OnSelectedEquipSlot(this.OpenPresetChangeBoxOrChangeDirectIfEmpty));
			}
			else
			{
				this.SetOnClickEquipSlot(this.m_currentOption.m_dOnSelectedEquipSlot);
			}
			if (options.m_hsSelectedEquipUIDToShow != null)
			{
				foreach (long item in options.m_hsSelectedEquipUIDToShow)
				{
					this.m_hsCurrentSelectedEquips.Add(item);
				}
			}
			this.lastSelectItemUID = options.lastSelectedItemUID;
			this.lastSelectEquipPos = options.equipChangeTargetPosition;
			NKCUtil.SetGameobjectActive(this.m_objSortSelect, false);
			NKCUtil.SetGameobjectActive(this.m_objFilterSelected, this.m_currentOption.setFilterOption.Count > 0);
			this.m_NKM_UI_INVENTORY_MENU_LOCK.Select(this.m_currentOption.bEnableLockEquipSystem, false, false);
			NKCUIInventory.NKC_INVENTORY_TAB newTab;
			switch (this.m_currentOption.m_NKC_INVENTORY_OPEN_TYPE)
			{
			default:
				newTab = NKCUIInventory.NKC_INVENTORY_TAB.NIT_MISC;
				break;
			case NKC_INVENTORY_OPEN_TYPE.NIOT_EQUIP_SELECT:
			case NKC_INVENTORY_OPEN_TYPE.NIOT_EQUIP_DEV:
				newTab = NKCUIInventory.NKC_INVENTORY_TAB.NIT_EQUIP;
				break;
			case NKC_INVENTORY_OPEN_TYPE.NIOT_MOLD_DEV:
				newTab = NKCUIInventory.NKC_INVENTORY_TAB.NIT_MOLD;
				break;
			}
			if (openTab != NKCUIInventory.NKC_INVENTORY_TAB.NIT_NONE)
			{
				newTab = openTab;
			}
			this.OnSelectTab(newTab, true);
			if (lstUpsideMenuResource != null)
			{
				this.RESOURCE_LIST = lstUpsideMenuResource;
			}
			else
			{
				this.RESOURCE_LIST = base.UpsideMenuShowResourceList;
			}
			NKCUIManager.UpdateUpsideMenu();
			base.UIOpened(true);
			this.SetOpenSortingMenu(false, false);
		}

		// Token: 0x06006734 RID: 26420 RVA: 0x00212944 File Offset: 0x00210B44
		private void OnSelectTab(NKCUIInventory.NKC_INVENTORY_TAB newTab, bool bForceOpen = false)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_INVENTORY_EQUIPMENTS_UNLOCK, false);
			if (this.m_NKC_INVENTORY_TAB == newTab && !bForceOpen)
			{
				return;
			}
			this.ReturnSlot(this.m_NKC_INVENTORY_TAB);
			this.m_NKC_INVENTORY_TAB = newTab;
			if (this.m_currentOption.lstSortOption.Count == 0)
			{
				this.m_currentOption.lstSortOption = NKCEquipSortSystem.GetDefaultSortOption(NKCPopupEquipSort.SORT_OPEN_TYPE.NORMAL);
			}
			this.CalculateContentRectSize();
			if (newTab == NKCUIInventory.NKC_INVENTORY_TAB.NIT_EQUIP)
			{
				this.m_LoopScrollRectEquip.PrepareCells(0);
			}
			else
			{
				this.m_LoopScrollRect.PrepareCells(0);
			}
			this.ChangeUI();
			NKCUtil.SetGameobjectActive(this.m_objMisc, this.m_NKC_INVENTORY_TAB != NKCUIInventory.NKC_INVENTORY_TAB.NIT_EQUIP);
			NKCUtil.SetGameobjectActive(this.m_objGear, this.m_NKC_INVENTORY_TAB == NKCUIInventory.NKC_INVENTORY_TAB.NIT_EQUIP);
			this.m_eInventoryOpenType = this.m_currentOption.m_NKC_INVENTORY_OPEN_TYPE;
			this.ProcessByType(this.m_eInventoryOpenType, true);
			if (this.m_NKC_INVENTORY_TAB == NKCUIInventory.NKC_INVENTORY_TAB.NIT_EQUIP)
			{
				this.UpdateSelectedEquipCountUI();
				this.UpdateSelectedEquipBreakupResult();
			}
		}

		// Token: 0x06006735 RID: 26421 RVA: 0x00212A28 File Offset: 0x00210C28
		private void ProcessByType(NKC_INVENTORY_OPEN_TYPE targetType, bool bForceRebuildList = false)
		{
			if (this.m_currentOption.m_NKC_INVENTORY_OPEN_TYPE != targetType)
			{
				this.m_currentOption.m_NKC_INVENTORY_OPEN_TYPE = targetType;
				this.CalculateContentRectSize();
				if (targetType == NKC_INVENTORY_OPEN_TYPE.NIOT_EQUIP_DEV || targetType == NKC_INVENTORY_OPEN_TYPE.NIOT_EQUIP_SELECT)
				{
					this.m_LoopScrollRectEquip.PrepareCells(0);
				}
				else
				{
					this.m_LoopScrollRect.PrepareCells(0);
				}
			}
			if (this.m_NKC_INVENTORY_TAB == NKCUIInventory.NKC_INVENTORY_TAB.NIT_EQUIP)
			{
				if (bForceRebuildList)
				{
					this.m_dicEquipSortSystem.Remove(targetType);
				}
				this.m_ssActive = this.GetEquipSortSystem(targetType);
			}
			else if (this.m_NKC_INVENTORY_TAB == NKCUIInventory.NKC_INVENTORY_TAB.NIT_MISC || this.m_NKC_INVENTORY_TAB == NKCUIInventory.NKC_INVENTORY_TAB.NIT_MOLD)
			{
				this.m_ssActive = null;
			}
			this.UpdateItemList(this.m_NKC_INVENTORY_TAB);
		}

		// Token: 0x06006736 RID: 26422 RVA: 0x00212AC4 File Offset: 0x00210CC4
		private void SetEquipInfo(NKCUISlotEquip slot)
		{
			if (slot == null || slot.GetNKMEquipItemData() == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objEquipNotSelected, true);
				NKCUtil.SetGameobjectActive(this.m_slotEquipInfo, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objEquipNotSelected, false);
			NKCUtil.SetGameobjectActive(this.m_slotEquipInfo, true);
			NKMEquipItemData equipItemData = slot.GetNKMEquipItemData();
			this.m_slotEquipInfo.SetData(equipItemData, this.m_currentOption.bShowFierceUI, false);
			if (this.m_currentOption.bEnableLockEquipSystem)
			{
				NKCUtil.SetGameobjectActive(this.m_ChangeButton, false);
				NKCUtil.SetGameobjectActive(this.m_EquipButton, false);
				NKCUtil.SetGameobjectActive(this.m_OkButton, false);
				NKCUtil.SetGameobjectActive(this.m_ReinforceButton, false);
				NKCUtil.SetGameobjectActive(this.m_ReinforceButtonLock, false);
				NKCUtil.SetGameobjectActive(this.m_UnEquipButton, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_UnEquipButton, equipItemData != null && equipItemData.m_OwnerUnitUID > 0L);
			NKCUtil.SetGameobjectActive(this.m_EquipButton, equipItemData == null || equipItemData.m_OwnerUnitUID <= 0L);
			if (this.m_dOnClickEquipSlot != null)
			{
				this.m_OkButton.PointerClick.RemoveAllListeners();
				this.m_OkButton.PointerClick.AddListener(delegate()
				{
					this.OnClickEquipSlot(slot, equipItemData);
				});
			}
			this.SetInventoryButtons(slot);
		}

		// Token: 0x06006737 RID: 26423 RVA: 0x00212C44 File Offset: 0x00210E44
		private void SetInventoryButtons(NKCUISlotEquip slot)
		{
			NKMEquipItemData cNKMEquipItemData = this.m_LatestOpenNKMEquipItemData;
			if (cNKMEquipItemData == null)
			{
				return;
			}
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(cNKMEquipItemData.m_ItemEquipID);
			if (equipTemplet == null)
			{
				return;
			}
			if (this.m_currentOption.m_ButtonMenuType != NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_PRESET_CHANGE && this.m_LatestTargetUnitUIDToEquip > 0L)
			{
				NKMUnitData unitFromUID = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetUnitFromUID(this.m_LatestTargetUnitUIDToEquip);
				if (this.m_currentOption.equipChangeTargetPosition != equipTemplet.m_ItemEquipPosition)
				{
					if (equipTemplet.m_ItemEquipPosition == ITEM_EQUIP_POSITION.IEP_ACC || equipTemplet.m_ItemEquipPosition == ITEM_EQUIP_POSITION.IEP_ACC2)
					{
						if (unitFromUID.GetEquipItemAccessoryUid() == 0L)
						{
							this.m_currentOption.equipChangeTargetPosition = ITEM_EQUIP_POSITION.IEP_ACC;
							this.m_currentOption.lastSelectedItemUID = 0L;
						}
						else if (unitFromUID.GetEquipItemAccessory2Uid() == 0L)
						{
							this.m_currentOption.equipChangeTargetPosition = ITEM_EQUIP_POSITION.IEP_ACC2;
							this.m_currentOption.lastSelectedItemUID = 0L;
						}
						else if (this.lastSelectEquipPos == ITEM_EQUIP_POSITION.IEP_ACC || this.lastSelectEquipPos == ITEM_EQUIP_POSITION.IEP_ACC2)
						{
							this.m_currentOption.equipChangeTargetPosition = this.lastSelectEquipPos;
							this.m_currentOption.lastSelectedItemUID = unitFromUID.GetEquipUid(this.lastSelectEquipPos);
						}
						else
						{
							this.m_currentOption.equipChangeTargetPosition = ITEM_EQUIP_POSITION.IEP_ACC;
							this.m_currentOption.lastSelectedItemUID = unitFromUID.GetEquipUid(ITEM_EQUIP_POSITION.IEP_ACC);
						}
					}
					else
					{
						this.m_currentOption.equipChangeTargetPosition = equipTemplet.m_ItemEquipPosition;
						this.m_currentOption.lastSelectedItemUID = unitFromUID.GetEquipUid(equipTemplet.m_ItemEquipPosition);
					}
				}
			}
			if (this.m_currentOption.m_dOnClickEmptySlot != null && this.m_currentOption.lastSelectedItemUID > 0L && cNKMEquipItemData.m_ItemUid == this.m_currentOption.lastSelectedItemUID)
			{
				NKCUtil.SetGameobjectActive(this.m_ChangeButton, false);
				NKCUtil.SetGameobjectActive(this.m_EquipButton, false);
				NKCUtil.SetGameobjectActive(this.m_OkButton, false);
				NKCUtil.SetGameobjectActive(this.m_ReinforceButton, false);
				NKCUtil.SetGameobjectActive(this.m_ReinforceButtonLock, false);
				NKCUtil.SetGameobjectActive(this.m_UnEquipButton, true);
				this.m_UnEquipButton.PointerClick.RemoveAllListeners();
				this.m_UnEquipButton.PointerClick.AddListener(delegate()
				{
					this.m_currentOption.m_dOnClickEmptySlot(slot, cNKMEquipItemData);
				});
				return;
			}
			NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE buttonMenuType = this.m_currentOption.m_ButtonMenuType;
			if (buttonMenuType == NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_NONE)
			{
				NKCUtil.SetGameobjectActive(this.m_UnEquipButton, false);
				NKCUtil.SetGameobjectActive(this.m_ReinforceButton, false);
				NKCUtil.SetGameobjectActive(this.m_ReinforceButtonLock, false);
				NKCUtil.SetGameobjectActive(this.m_EquipButton, false);
				NKCUtil.SetGameobjectActive(this.m_ChangeButton, false);
				NKCUtil.SetGameobjectActive(this.m_OkButton, false);
				return;
			}
			if (buttonMenuType == NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_OK)
			{
				NKCUtil.SetGameobjectActive(this.m_UnEquipButton, false);
				if (this.m_currentOption.bShowEquipUpgradeState)
				{
					NKCUtil.SetGameobjectActive(this.m_ReinforceButton, NKMItemManager.CanUpgradeEquipByCoreID(cNKMEquipItemData) != NKC_EQUIP_UPGRADE_STATE.UPGRADABLE);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_ReinforceButton, false);
				}
				NKCUtil.SetGameobjectActive(this.m_ReinforceButtonLock, false);
				NKCUtil.SetGameobjectActive(this.m_EquipButton, false);
				NKCUtil.SetGameobjectActive(this.m_ChangeButton, false);
				if (this.m_currentOption.bShowEquipUpgradeState)
				{
					NKCUtil.SetGameobjectActive(this.m_OkButton, NKMItemManager.CanUpgradeEquipByCoreID(cNKMEquipItemData) == NKC_EQUIP_UPGRADE_STATE.UPGRADABLE);
					return;
				}
				NKCUtil.SetGameobjectActive(this.m_OkButton, true);
				return;
			}
			else
			{
				if (buttonMenuType == NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_PRESET_CHANGE)
				{
					NKCUtil.SetGameobjectActive(this.m_UnEquipButton, this.m_currentOption.lastSelectedItemUID > 0L && slot.GetNKMEquipItemData().m_ItemUid == this.m_currentOption.lastSelectedItemUID);
					NKCUtil.SetGameobjectActive(this.m_ReinforceButton, NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_ENCHANT, 0, 0) && equipTemplet.m_ItemEquipPosition != ITEM_EQUIP_POSITION.IEP_ENCHANT);
					NKCUtil.SetGameobjectActive(this.m_ReinforceButtonLock, !NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_ENCHANT, 0, 0) && equipTemplet.m_ItemEquipPosition != ITEM_EQUIP_POSITION.IEP_ENCHANT);
					NKCUtil.SetGameobjectActive(this.m_EquipButton, false);
					NKCUtil.SetGameobjectActive(this.m_ChangeButton, false);
					NKCUtil.SetGameobjectActive(this.m_OkButton, slot.GetNKMEquipItemData().m_ItemUid != this.m_currentOption.lastSelectedItemUID);
					return;
				}
				if (buttonMenuType == NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_CHANGE)
				{
					NKCUtil.SetGameobjectActive(this.m_UnEquipButton, this.m_LatestOpenNKMEquipItemData.m_OwnerUnitUID == this.m_LatestTargetUnitUIDToEquip);
					NKCUtil.SetGameobjectActive(this.m_ReinforceButton, false);
					NKCUtil.SetGameobjectActive(this.m_ReinforceButtonLock, false);
					NKCUtil.SetGameobjectActive(this.m_EquipButton, false);
					NKCUtil.SetGameobjectActive(this.m_ChangeButton, false);
					NKCUtil.SetGameobjectActive(this.m_OkButton, this.m_LatestOpenNKMEquipItemData.m_OwnerUnitUID != this.m_LatestTargetUnitUIDToEquip);
					return;
				}
				NKCUtil.SetGameobjectActive(this.m_UnEquipButton, cNKMEquipItemData.m_OwnerUnitUID > 0L);
				NKCUtil.SetGameobjectActive(this.m_ReinforceButton, NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_ENCHANT, 0, 0) && equipTemplet.m_ItemEquipPosition != ITEM_EQUIP_POSITION.IEP_ENCHANT);
				NKCUtil.SetGameobjectActive(this.m_ReinforceButtonLock, !NKCContentManager.IsContentsUnlocked(ContentsType.FACTORY_ENCHANT, 0, 0) && equipTemplet.m_ItemEquipPosition != ITEM_EQUIP_POSITION.IEP_ENCHANT);
				NKCUtil.SetGameobjectActive(this.m_EquipButton, cNKMEquipItemData.m_OwnerUnitUID <= 0L && equipTemplet.m_ItemEquipPosition != ITEM_EQUIP_POSITION.IEP_ENCHANT);
				NKCUtil.SetGameobjectActive(this.m_ChangeButton, cNKMEquipItemData.m_OwnerUnitUID > 0L);
				NKCUtil.SetGameobjectActive(this.m_OkButton, equipTemplet.m_ItemEquipPosition == ITEM_EQUIP_POSITION.IEP_ENCHANT);
				return;
			}
		}

		// Token: 0x06006738 RID: 26424 RVA: 0x0021316A File Offset: 0x0021136A
		public void SetOnClickEquipSlot(NKCUISlotEquip.OnSelectedEquipSlot dOnClickEquipSlot = null)
		{
			if (dOnClickEquipSlot != null)
			{
				this.m_dOnClickEquipSlot = dOnClickEquipSlot;
				return;
			}
			this.m_dOnClickEquipSlot = delegate(NKCUISlotEquip slot, NKMEquipItemData data)
			{
				this.OpenChangeBoxOrChangeDirectIfEmpty();
			};
		}

		// Token: 0x06006739 RID: 26425 RVA: 0x0021318C File Offset: 0x0021138C
		private void OpenPresetChangeBoxOrChangeDirectIfEmpty(NKCUISlotEquip cItemSlot, NKMEquipItemData equipData)
		{
			if (cItemSlot == null || cItemSlot.GetNKMEquipItemData() == null)
			{
				return;
			}
			this.m_LatestSelectedSlot = cItemSlot;
			this.m_LatestOpenNKMEquipItemData = equipData;
			if (this.m_currentOption.lastSelectedItemUID > 0L && this.m_currentOption.m_ButtonMenuType != NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_PRESET_CHANGE)
			{
				NKCPopupItemEquipBox.OpenForConfirm(this.m_LatestOpenNKMEquipItemData, new UnityAction(this.OpenPresetChangeBoxOrChangeDirectIfEmpty), this.m_currentOption.bShowFierceUI, true, null);
				return;
			}
			this.OpenPresetChangeBoxOrChangeDirectIfEmpty();
		}

		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x0600673A RID: 26426 RVA: 0x00213204 File Offset: 0x00211404
		private NKCPopupEquipChange NKCPopupEquipChange
		{
			get
			{
				if (this.m_NKCPopupEquipChange == null)
				{
					NKCUIManager.LoadedUIData loadedUIData = NKCUIManager.OpenNewInstance<NKCPopupEquipChange>("AB_UI_NKM_UI_UNIT_CHANGE_POPUP", "NKM_UI_EQUIP_CHANGE_POPUP", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), null);
					this.m_NKCPopupEquipChange = loadedUIData.GetInstance<NKCPopupEquipChange>();
					NKCPopupEquipChange nkcpopupEquipChange = this.m_NKCPopupEquipChange;
					if (nkcpopupEquipChange != null)
					{
						nkcpopupEquipChange.InitUI();
					}
				}
				return this.m_NKCPopupEquipChange;
			}
		}

		// Token: 0x0600673B RID: 26427 RVA: 0x0021325C File Offset: 0x0021145C
		private void OpenChangeBoxOrChangeDirectIfEmpty()
		{
			if (this.m_LatestOpenNKMEquipItemData == null)
			{
				return;
			}
			NKMEquipTemplet cNKMEquipTemplet = NKMItemManager.GetEquipTemplet(this.m_LatestOpenNKMEquipItemData.m_ItemEquipID);
			if (cNKMEquipTemplet == null)
			{
				return;
			}
			NKMUnitData unitFromUID = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetUnitFromUID(this.m_LatestTargetUnitUIDToEquip);
			if (unitFromUID == null)
			{
				return;
			}
			if (cNKMEquipTemplet.IsPrivateEquip() && !cNKMEquipTemplet.IsPrivateEquipForUnit(unitFromUID.m_UnitID))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_CANNOT_EQUIP_ITEM_PRIVATE, null, "");
				return;
			}
			if (this.m_currentOption.equipChangeTargetPosition == ITEM_EQUIP_POSITION.IEP_ACC || this.m_currentOption.equipChangeTargetPosition == ITEM_EQUIP_POSITION.IEP_ACC2)
			{
				if (unitFromUID.GetEquipItemAccessoryUid() > 0L && unitFromUID.GetEquipItemAccessory2Uid() > 0L)
				{
					if (this.lastSelectItemUID == 0L || (this.lastSelectItemUID != unitFromUID.GetEquipItemAccessoryUid() && this.lastSelectItemUID != unitFromUID.GetEquipItemAccessory2Uid()))
					{
						NKCPopupItemEquipBox.OpenForSelectItem(unitFromUID.GetEquipItemAccessoryUid(), unitFromUID.GetEquipItemAccessory2Uid(), delegate
						{
							this.ChangeEquipAccessory(ITEM_EQUIP_POSITION.IEP_ACC);
						}, delegate
						{
							this.ChangeEquipAccessory(ITEM_EQUIP_POSITION.IEP_ACC2);
						});
						return;
					}
					if (this.lastSelectItemUID == unitFromUID.GetEquipItemAccessoryUid())
					{
						this.ChangeEquipAccessory(ITEM_EQUIP_POSITION.IEP_ACC);
						return;
					}
					if (this.lastSelectItemUID == unitFromUID.GetEquipItemAccessory2Uid())
					{
						this.ChangeEquipAccessory(ITEM_EQUIP_POSITION.IEP_ACC2);
						return;
					}
				}
				else
				{
					if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_INVENTORY || !unitFromUID.IsUnlockAccessory2() || (unitFromUID.GetEquipItemAccessoryUid() <= 0L && unitFromUID.GetEquipItemAccessory2Uid() <= 0L))
					{
						this.CheckEquipChange(this.m_currentOption.equipChangeTargetPosition);
						return;
					}
					if (unitFromUID.GetEquipItemAccessoryUid() > 0L)
					{
						this.CheckEquipChange(ITEM_EQUIP_POSITION.IEP_ACC2);
						return;
					}
					this.CheckEquipChange(ITEM_EQUIP_POSITION.IEP_ACC);
					return;
				}
			}
			else
			{
				long equipUid = unitFromUID.GetEquipUid(cNKMEquipTemplet.m_ItemEquipPosition);
				if (equipUid > 0L)
				{
					NKMEquipItemData itemEquip = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(equipUid);
					NKCPopupEquipChange nkcpopupEquipChange = this.NKCPopupEquipChange;
					if (nkcpopupEquipChange == null)
					{
						return;
					}
					nkcpopupEquipChange.Open(itemEquip, this.m_LatestOpenNKMEquipItemData, delegate
					{
						this.SendEquipPacket(cNKMEquipTemplet.m_ItemEquipPosition);
					}, this.m_currentOption.bShowFierceUI);
					return;
				}
				else
				{
					NKCPopupItemEquipBox.OpenForConfirm(this.m_LatestOpenNKMEquipItemData, delegate
					{
						this.SendEquipPacket(cNKMEquipTemplet.m_ItemEquipPosition);
					}, this.m_currentOption.bShowFierceUI, true, null);
				}
			}
		}

		// Token: 0x0600673C RID: 26428 RVA: 0x0021347C File Offset: 0x0021167C
		private void OpenPresetChangeBoxOrChangeDirectIfEmpty()
		{
			if (this.m_LatestOpenNKMEquipItemData == null)
			{
				return;
			}
			long equipUId = this.m_LatestOpenNKMEquipItemData.m_ItemUid;
			ITEM_EQUIP_POSITION presetPosition = this.m_currentOption.equipChangeTargetPosition;
			if (this.m_currentOption.equipChangeTargetPosition == ITEM_EQUIP_POSITION.IEP_ACC || this.m_currentOption.equipChangeTargetPosition == ITEM_EQUIP_POSITION.IEP_ACC2)
			{
				presetPosition = ITEM_EQUIP_POSITION.IEP_ACC;
			}
			List<long> lstEquipResult = new List<long>();
			if (NKCEquipPresetDataManager.ListEquipPresetData != null && NKCEquipPresetDataManager.ListEquipPresetData.Count > this.m_currentOption.iPresetIndex)
			{
				lstEquipResult.AddRange(NKCEquipPresetDataManager.ListEquipPresetData[this.m_currentOption.iPresetIndex].equipUids);
				int equipChangeTargetPosition = (int)this.m_currentOption.equipChangeTargetPosition;
				if (lstEquipResult.Count > equipChangeTargetPosition)
				{
					lstEquipResult[equipChangeTargetPosition] = equipUId;
				}
			}
			if (this.lastSelectItemUID <= 0L)
			{
				NKCPopupItemEquipBox.OpenForConfirm(this.m_LatestOpenNKMEquipItemData, delegate
				{
					NKMEquipTemplet equipTemplet3 = NKMItemManager.GetEquipTemplet(this.m_LatestOpenNKMEquipItemData.m_ItemEquipID);
					if (this.m_currentOption.presetUnitStyeType != NKM_UNIT_STYLE_TYPE.NUST_INVALID && this.m_currentOption.presetUnitStyeType != equipTemplet3.m_EquipUnitStyleType)
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_EQUIP_PRESET_DIFFERENT_TYPE, null, "");
						return;
					}
					if (presetPosition != equipTemplet3.m_ItemEquipPosition)
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_EQUIP_PRESET_DIFFERENT_POSITION, null, "");
						return;
					}
					if (NKCUtil.IsPrivateEquipAlreadyEquipped(lstEquipResult))
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_EQUIP_PRIVATE.ToString(), false), null, "");
						return;
					}
					NKCPacketSender.Send_NKMPacket_EQUIP_PRESET_REGISTER_REQ(this.m_currentOption.iPresetIndex, this.m_currentOption.equipChangeTargetPosition, equipUId);
				}, this.m_currentOption.bShowFierceUI, true, null);
				return;
			}
			NKMEquipItemData itemEquip = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(this.lastSelectItemUID);
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID);
			NKMEquipTemplet equipTemplet2 = NKMItemManager.GetEquipTemplet(this.m_LatestOpenNKMEquipItemData.m_ItemEquipID);
			if (equipTemplet.m_EquipUnitStyleType != equipTemplet2.m_EquipUnitStyleType)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_EQUIP_PRESET_DIFFERENT_TYPE, null, "");
				return;
			}
			if (equipTemplet.m_ItemEquipPosition != equipTemplet2.m_ItemEquipPosition)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_EQUIP_PRESET_DIFFERENT_POSITION, null, "");
				return;
			}
			if (NKCUtil.IsPrivateEquipAlreadyEquipped(lstEquipResult))
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_EQUIP_PRIVATE.ToString(), false), null, "");
				return;
			}
			NKCPopupEquipChange nkcpopupEquipChange = this.NKCPopupEquipChange;
			if (nkcpopupEquipChange == null)
			{
				return;
			}
			nkcpopupEquipChange.Open(itemEquip, this.m_LatestOpenNKMEquipItemData, delegate
			{
				NKCPacketSender.Send_NKMPacket_EQUIP_PRESET_REGISTER_REQ(this.m_currentOption.iPresetIndex, this.m_currentOption.equipChangeTargetPosition, equipUId);
			}, this.m_currentOption.bShowFierceUI);
		}

		// Token: 0x0600673D RID: 26429 RVA: 0x0021367C File Offset: 0x0021187C
		private void CheckEquipChange(ITEM_EQUIP_POSITION targetPosition)
		{
			List<long> lstEquipResult = new List<long>();
			NKMUnitData unitFromUID = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetUnitFromUID(this.m_LatestTargetUnitUIDToEquip);
			if (unitFromUID != null)
			{
				lstEquipResult.AddRange(unitFromUID.EquipItemUids);
				int equipChangeTargetPosition = (int)this.m_currentOption.equipChangeTargetPosition;
				if (lstEquipResult.Count > equipChangeTargetPosition)
				{
					lstEquipResult[equipChangeTargetPosition] = this.m_LatestOpenNKMEquipItemData.m_ItemUid;
				}
			}
			if (this.m_currentOption.lastSelectedItemUID > 0L)
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData != null)
				{
					NKMEquipItemData itemEquip = nkmuserData.m_InventoryData.GetItemEquip(this.m_currentOption.lastSelectedItemUID);
					if (itemEquip == null || itemEquip.m_OwnerUnitUID <= 0L)
					{
						NKCPopupItemEquipBox.OpenForConfirm(this.m_LatestOpenNKMEquipItemData, delegate
						{
							this.SendEquipPacket(targetPosition);
						}, false, true, null);
						return;
					}
					NKCPopupEquipChange nkcpopupEquipChange = this.NKCPopupEquipChange;
					if (nkcpopupEquipChange == null)
					{
						return;
					}
					nkcpopupEquipChange.Open(itemEquip, this.m_LatestOpenNKMEquipItemData, delegate
					{
						this.SendEquipPacket(targetPosition);
					}, false);
					return;
				}
			}
			else
			{
				NKCPopupItemEquipBox.OpenForConfirm(this.m_LatestOpenNKMEquipItemData, delegate
				{
					if (NKCUtil.IsPrivateEquipAlreadyEquipped(lstEquipResult))
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_EQUIP_PRIVATE.ToString(), false), null, "");
						return;
					}
					this.SendEquipPacket(targetPosition);
				}, false, true, null);
			}
		}

		// Token: 0x0600673E RID: 26430 RVA: 0x002137A8 File Offset: 0x002119A8
		private void SendEquipPacket(ITEM_EQUIP_POSITION equip_position)
		{
			NKMEquipItemData latestOpenNKMEquipItemData = this.m_LatestOpenNKMEquipItemData;
			if (latestOpenNKMEquipItemData != null)
			{
				if (latestOpenNKMEquipItemData.m_OwnerUnitUID <= 0L)
				{
					long equipUid = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(this.m_LatestTargetUnitUIDToEquip).GetEquipUid(equip_position);
					if (equipUid != 0L)
					{
						NKCPacketSender.Send_NKMPacket_EQUIP_ITEM_EQUIP_REQ(false, this.m_LatestTargetUnitUIDToEquip, equipUid, equip_position);
					}
					NKCPacketSender.Send_NKMPacket_EQUIP_ITEM_EQUIP_REQ(true, this.m_LatestTargetUnitUIDToEquip, latestOpenNKMEquipItemData.m_ItemUid, equip_position);
					return;
				}
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_INVEN_EQUIP_CHANGE_WARNING, new NKCPopupOKCancel.OnButton(this.ConfirmChangeEquip), null, false);
			}
		}

		// Token: 0x0600673F RID: 26431 RVA: 0x00213828 File Offset: 0x00211A28
		public void ConfirmChangeEquip()
		{
			NKMEquipItemData latestOpenNKMEquipItemData = this.m_LatestOpenNKMEquipItemData;
			if (latestOpenNKMEquipItemData != null)
			{
				NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(this.m_LatestTargetUnitUIDToEquip);
				if (unitFromUID != null)
				{
					long equipUid = unitFromUID.GetEquipUid(this.m_currentOption.equipChangeTargetPosition);
					if (equipUid != 0L)
					{
						NKCPacketSender.Send_NKMPacket_EQUIP_ITEM_EQUIP_REQ(false, this.m_LatestTargetUnitUIDToEquip, equipUid, this.m_currentOption.equipChangeTargetPosition);
					}
				}
				ITEM_EQUIP_POSITION itemEquipPosition = NKMItemManager.GetItemEquipPosition(latestOpenNKMEquipItemData.m_ItemUid);
				NKCPacketSender.Send_NKMPacket_EQUIP_ITEM_EQUIP_REQ(false, latestOpenNKMEquipItemData.m_OwnerUnitUID, latestOpenNKMEquipItemData.m_ItemUid, itemEquipPosition);
				if (this.m_currentOption.equipChangeTargetPosition != ITEM_EQUIP_POSITION.IEP_NONE)
				{
					NKCPacketSender.Send_NKMPacket_EQUIP_ITEM_EQUIP_REQ(true, this.m_LatestTargetUnitUIDToEquip, latestOpenNKMEquipItemData.m_ItemUid, this.m_currentOption.equipChangeTargetPosition);
					return;
				}
				NKCPacketSender.Send_NKMPacket_EQUIP_ITEM_EQUIP_REQ(true, this.m_LatestTargetUnitUIDToEquip, latestOpenNKMEquipItemData.m_ItemUid, itemEquipPosition);
			}
		}

		// Token: 0x06006740 RID: 26432 RVA: 0x002138E8 File Offset: 0x00211AE8
		private void ChangeEquipAccessory(ITEM_EQUIP_POSITION equipPosition)
		{
			if (equipPosition == ITEM_EQUIP_POSITION.IEP_ACC || equipPosition == ITEM_EQUIP_POSITION.IEP_ACC2)
			{
				NKMUnitData unitFromUID = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetUnitFromUID(this.m_LatestTargetUnitUIDToEquip);
				if (unitFromUID != null)
				{
					List<long> list = new List<long>();
					list.AddRange(unitFromUID.EquipItemUids);
					int equipPosition2 = (int)equipPosition;
					if (list.Count > equipPosition2)
					{
						list[equipPosition2] = this.m_LatestOpenNKMEquipItemData.m_ItemUid;
					}
					if (NKCUtil.IsPrivateEquipAlreadyEquipped(list))
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_EQUIP_PRIVATE.ToString(), false), null, "");
						return;
					}
					long itemUid = (equipPosition == ITEM_EQUIP_POSITION.IEP_ACC) ? unitFromUID.GetEquipItemAccessoryUid() : unitFromUID.GetEquipItemAccessory2Uid();
					NKMEquipItemData beforeItem = NKCScenManager.CurrentUserData().m_InventoryData.GetItemEquip(itemUid);
					if (beforeItem != null)
					{
						NKCPopupEquipChange nkcpopupEquipChange = this.NKCPopupEquipChange;
						if (nkcpopupEquipChange == null)
						{
							return;
						}
						nkcpopupEquipChange.Open(beforeItem, this.m_LatestOpenNKMEquipItemData, delegate
						{
							this.ChangeEquipItem(beforeItem, this.m_LatestOpenNKMEquipItemData, equipPosition);
						}, false);
						return;
					}
					else
					{
						this.SendEquipPacket(equipPosition);
					}
				}
			}
		}

		// Token: 0x06006741 RID: 26433 RVA: 0x00213A14 File Offset: 0x00211C14
		private void ChangeEquipItem(NKMEquipItemData beforeItem, NKMEquipItemData afterItem, ITEM_EQUIP_POSITION equipPosition)
		{
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(beforeItem.m_ItemEquipID);
			NKMEquipTemplet equipTemplet2 = NKMItemManager.GetEquipTemplet(afterItem.m_ItemEquipID);
			if (equipTemplet != null && equipTemplet2 != null)
			{
				Debug.Log("변경 요청한 아이템 명칭 : " + equipTemplet.GetItemName() + " -> " + equipTemplet2.GetItemName());
			}
			this.m_beforeItemUID = 0L;
			this.m_afterItemUId = 0L;
			this.m_equipPosition = ITEM_EQUIP_POSITION.IEP_NONE;
			if (afterItem.m_OwnerUnitUID > 0L)
			{
				this.m_beforeItemUID = beforeItem.m_ItemUid;
				this.m_afterItemUId = afterItem.m_ItemUid;
				this.m_equipPosition = equipPosition;
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_INVEN_EQUIP_CHANGE_WARNING, new NKCPopupOKCancel.OnButton(this.OnChangeEquip), null, false);
				return;
			}
			if (beforeItem.m_OwnerUnitUID > 0L)
			{
				NKCPacketSender.Send_NKMPacket_EQUIP_ITEM_EQUIP_REQ(false, beforeItem.m_OwnerUnitUID, beforeItem.m_ItemUid, equipPosition);
				NKCPacketSender.Send_NKMPacket_EQUIP_ITEM_EQUIP_REQ(true, beforeItem.m_OwnerUnitUID, afterItem.m_ItemUid, equipPosition);
			}
		}

		// Token: 0x06006742 RID: 26434 RVA: 0x00213AF0 File Offset: 0x00211CF0
		private void OnChangeEquip()
		{
			if (this.m_beforeItemUID == 0L || this.m_afterItemUId == 0L || this.m_equipPosition == ITEM_EQUIP_POSITION.IEP_NONE)
			{
				return;
			}
			NKMEquipItemData itemEquip = NKCScenManager.CurrentUserData().m_InventoryData.GetItemEquip(this.m_beforeItemUID);
			NKMEquipItemData itemEquip2 = NKCScenManager.CurrentUserData().m_InventoryData.GetItemEquip(this.m_afterItemUId);
			if (itemEquip2.m_OwnerUnitUID > 0L)
			{
				ITEM_EQUIP_POSITION itemEquipPosition = NKMItemManager.GetItemEquipPosition(itemEquip2.m_ItemUid);
				NKCPacketSender.Send_NKMPacket_EQUIP_ITEM_EQUIP_REQ(false, itemEquip2.m_OwnerUnitUID, itemEquip2.m_ItemUid, itemEquipPosition);
			}
			if (itemEquip.m_OwnerUnitUID > 0L)
			{
				NKCPacketSender.Send_NKMPacket_EQUIP_ITEM_EQUIP_REQ(false, itemEquip.m_OwnerUnitUID, itemEquip.m_ItemUid, this.m_equipPosition);
				NKCPacketSender.Send_NKMPacket_EQUIP_ITEM_EQUIP_REQ(true, itemEquip.m_OwnerUnitUID, itemEquip2.m_ItemUid, this.m_equipPosition);
			}
		}

		// Token: 0x06006743 RID: 26435 RVA: 0x00213BA8 File Offset: 0x00211DA8
		public void FinishMultiSelection()
		{
			if (this.m_currentOption.m_dOnFinishMultiSelection != null)
			{
				List<long> listEquipSlot = new List<long>(this.m_hsCurrentSelectedEquips);
				this.m_currentOption.m_dOnFinishMultiSelection(listEquipSlot);
			}
		}

		// Token: 0x06006744 RID: 26436 RVA: 0x00213BE4 File Offset: 0x00211DE4
		public override void OnBackButton()
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_OFFICE)
			{
				base.Close();
				return;
			}
			if (this.m_currentOption.m_NKC_INVENTORY_OPEN_TYPE == NKC_INVENTORY_OPEN_TYPE.NIOT_NORMAL || this.m_currentOption.bShowRemoveItem)
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, false);
				return;
			}
			if (this.m_currentOption.m_NKC_INVENTORY_OPEN_TYPE == NKC_INVENTORY_OPEN_TYPE.NIOT_EQUIP_SELECT)
			{
				base.Close();
				return;
			}
			if (this.m_currentOption.m_NKC_INVENTORY_OPEN_TYPE == NKC_INVENTORY_OPEN_TYPE.NIOT_EQUIP_DEV || this.m_currentOption.m_NKC_INVENTORY_OPEN_TYPE == NKC_INVENTORY_OPEN_TYPE.NIOT_ITEM_DEV || this.m_currentOption.m_NKC_INVENTORY_OPEN_TYPE == NKC_INVENTORY_OPEN_TYPE.NIOT_MOLD_DEV)
			{
				NKCUIInventory.EquipSelectListOptions.OnClose dOnClose = this.m_currentOption.dOnClose;
				if (dOnClose != null)
				{
					dOnClose();
				}
				base.Close();
				return;
			}
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, false);
		}

		// Token: 0x06006745 RID: 26437 RVA: 0x00213C95 File Offset: 0x00211E95
		public override void CloseInternal()
		{
			if (this.m_NKM_UI_UNIT_INVENTORY.activeSelf)
			{
				this.m_NKM_UI_UNIT_INVENTORY.SetActive(false);
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_INVENTORY_EQUIPMENTS_UNLOCK, false);
			this.m_LatestOpenNKMEquipItemData = null;
			this.m_LatestSelectedSlot = null;
			this.Clear();
		}

		// Token: 0x06006746 RID: 26438 RVA: 0x00213CD0 File Offset: 0x00211ED0
		public override void UnHide()
		{
			base.UnHide();
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_INVENTORY_EQUIPMENTS_UNLOCK, false);
			if (this.m_bNeedRefresh)
			{
				this.m_bNeedRefresh = false;
				this.UpdateItemList(this.m_NKC_INVENTORY_TAB);
				if (this.m_NKC_INVENTORY_TAB == NKCUIInventory.NKC_INVENTORY_TAB.NIT_EQUIP)
				{
					long equipItemUID = this.m_slotEquipInfo.GetEquipItemUID();
					NKMEquipItemData itemEquip = NKCScenManager.CurrentUserData().m_InventoryData.GetItemEquip(equipItemUID);
					if (itemEquip != null)
					{
						this.m_ssActive.UpdateEquipData(itemEquip);
						if (this.m_slotEquipInfo.GetEquipItemUID() == equipItemUID)
						{
							this.m_slotEquipInfo.SetData(itemEquip, this.m_currentOption.bShowFierceUI, false);
						}
					}
				}
			}
		}

		// Token: 0x06006747 RID: 26439 RVA: 0x00213D65 File Offset: 0x00211F65
		private void Clear()
		{
			this.ReturnSlot(this.m_NKC_INVENTORY_TAB);
			this.m_NKC_INVENTORY_TAB = NKCUIInventory.NKC_INVENTORY_TAB.NIT_NONE;
		}

		// Token: 0x06006748 RID: 26440 RVA: 0x00213D7A File Offset: 0x00211F7A
		public void ClearCachingData()
		{
			this.m_currentOption.lEquipOptionCachingByUnitUID = 0L;
		}

		// Token: 0x06006749 RID: 26441 RVA: 0x00213D8C File Offset: 0x00211F8C
		private void FilterList(HashSet<NKCEquipSortSystem.eFilterOption> setFilterOption, bool bForce = false)
		{
			NKCUtil.SetGameobjectActive(this.m_objFilterSelected, this.m_currentOption.setFilterOption.Count > 0);
			this.m_currentOption.setFilterOption = setFilterOption;
			this.m_currentOption.m_hsSelectedEquipUIDToShow.Clear();
			this.m_hsCurrentSelectedEquips.Clear();
			this.m_LatestOpenNKMEquipItemData = null;
			this.ResetEquipSlotUI(true);
		}

		// Token: 0x0600674A RID: 26442 RVA: 0x00213DEC File Offset: 0x00211FEC
		public void ResetEquipSlotUI(bool bResetScroll = false)
		{
			this.UpdateItemList(this.m_NKC_INVENTORY_TAB);
			if (this.m_NKC_INVENTORY_TAB == NKCUIInventory.NKC_INVENTORY_TAB.NIT_EQUIP)
			{
				this.m_LoopScrollRectEquip.TotalCount = this.GetSlotCount(this.m_NKC_INVENTORY_TAB);
				this.SetCurrentEquipCountUI();
				this.m_LoopScrollRectEquip.SetIndexPosition(0);
				NKCUtil.SetGameobjectActive(this.m_objEquipInfo, this.m_LoopScrollRectEquip.TotalCount > 0);
				NKCUtil.SetGameobjectActive(this.m_objEmpty, this.m_LoopScrollRectEquip.TotalCount == 0);
				NKCUtil.SetGameobjectActive(this.m_objGear, this.m_LoopScrollRectEquip.TotalCount > 0);
				this.UpdateSelectedEquipCountUI();
				this.UpdateSelectedEquipBreakupResult();
			}
		}

		// Token: 0x0600674B RID: 26443 RVA: 0x00213E91 File Offset: 0x00212091
		public void ResetEquipSlotList()
		{
			this.m_bNeedRefresh = true;
			this.m_dicEquipSortSystem.Clear();
			this.m_ssActive = this.GetEquipSortSystem(this.m_currentOption.m_NKC_INVENTORY_OPEN_TYPE);
			this.ResetEquipSlotUI(false);
		}

		// Token: 0x0600674C RID: 26444 RVA: 0x00213EC4 File Offset: 0x002120C4
		public void OnClickFilterBtn()
		{
			NKCPopupFilterEquip.Instance.Open(this.m_setEquipFilterCategory, this.m_ssActive, new NKCPopupFilterEquip.OnEquipFilterSetChange(this.OnSelectFilter), this.m_currentOption.m_EquipListOptions.setExcludeFilterOption == null || !this.m_currentOption.m_EquipListOptions.setExcludeFilterOption.Contains(NKCEquipSortSystem.eFilterOption.Equip_Enchant));
		}

		// Token: 0x0600674D RID: 26445 RVA: 0x00213F21 File Offset: 0x00212121
		public void OnSelectFilter(NKCEquipSortSystem ssActive)
		{
			if (ssActive != null)
			{
				this.m_ssActive = ssActive;
			}
			this.FilterList(this.m_ssActive.FilterSet, false);
		}

		// Token: 0x0600674E RID: 26446 RVA: 0x00213F40 File Offset: 0x00212140
		public void OnSortMenuOpen(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objSortSelect, this.m_currentOption.lstSortOption[0] != NKCEquipSortSystem.GetDefaultSortOption(NKCPopupEquipSort.SORT_OPEN_TYPE.NORMAL)[0]);
			this.m_NKCEquipPopupSort.OpenEquipSortMenu(this.m_currentOption.lstSortOption[0], new NKCPopupEquipSort.OnSortOption(this.OnSort), NKCEquipSortSystem.GetDescendingBySorting(this.m_currentOption.lstSortOption), bValue, NKCPopupEquipSort.SORT_OPEN_TYPE.NORMAL);
			this.SetOpenSortingMenu(bValue, true);
		}

		// Token: 0x0600674F RID: 26447 RVA: 0x00213FBC File Offset: 0x002121BC
		private void OnSort(List<NKCEquipSortSystem.eSortOption> sortOptionList)
		{
			NKCUtil.SetGameobjectActive(this.m_objSortSelect, sortOptionList[0] != NKCEquipSortSystem.GetDefaultSortOption(NKCPopupEquipSort.SORT_OPEN_TYPE.NORMAL)[0]);
			this.SetOpenSortingMenu(false, true);
			this.SortList(sortOptionList, true, true);
		}

		// Token: 0x06006750 RID: 26448 RVA: 0x00213FF2 File Offset: 0x002121F2
		private void SortList(List<NKCEquipSortSystem.eSortOption> lstSortOption, bool bForce, bool bResetScroll = false)
		{
			if (this.m_NKC_INVENTORY_TAB == NKCUIInventory.NKC_INVENTORY_TAB.NIT_EQUIP)
			{
				this.m_ssActive.SortList(lstSortOption, bForce);
				this.m_currentOption.lstSortOption = lstSortOption;
			}
			this.ProcessUIFromCurrentDisplayedSortData(bResetScroll);
		}

		// Token: 0x06006751 RID: 26449 RVA: 0x00214020 File Offset: 0x00212220
		private void ProcessUIFromCurrentDisplayedSortData(bool bResetScroll = false)
		{
			if (this.m_NKC_INVENTORY_TAB == NKCUIInventory.NKC_INVENTORY_TAB.NIT_EQUIP)
			{
				this.SetCurrentEquipCountUI();
				if (this.m_currentOption.lstSortOption.Count > 0)
				{
					this.m_lbSortType.text = NKCEquipSortSystem.GetSortName(this.m_currentOption.lstSortOption[0]);
					this.m_lbSelectedSortType.text = NKCEquipSortSystem.GetSortName(this.m_currentOption.lstSortOption[0]);
				}
				else
				{
					Debug.LogError("정렬 선택하지 않아도 기본 기준이 있어야함. NKCUnitSortSystem.DEFAULT_SORT_OPTION_LIST");
				}
				this.m_ctgDescending.Select(NKCEquipSortSystem.GetDescendingBySorting(this.m_currentOption.lstSortOption), true, false);
				if (this.m_dOnGetItemListAfterSelected != null)
				{
					this.m_dOnGetItemListAfterSelected(this.m_ssActive.SortedEquipList);
				}
				this.m_LatestSelectedSlot = null;
				this.m_LoopScrollRectEquip.TotalCount = this.GetSlotCount(this.m_NKC_INVENTORY_TAB);
				if (bResetScroll)
				{
					this.m_LatestOpenNKMEquipItemData = null;
					this.m_LoopScrollRectEquip.SetIndexPosition(0);
				}
				else
				{
					int indexPosition = 0;
					if (this.m_LatestOpenNKMEquipItemData != null)
					{
						indexPosition = this.m_ssActive.SortedEquipList.FindIndex((NKMEquipItemData x) => x.m_ItemUid == this.m_LatestOpenNKMEquipItemData.m_ItemUid);
					}
					this.m_LoopScrollRectEquip.SetIndexPosition(indexPosition);
				}
				NKCUtil.SetGameobjectActive(this.m_objFilterSelected, this.m_currentOption.setFilterOption.Count > 0);
				NKCUtil.SetGameobjectActive(this.m_objEquipInfo, this.m_LoopScrollRectEquip.TotalCount > 0);
				NKCUtil.SetGameobjectActive(this.m_objEmpty, this.m_LoopScrollRectEquip.TotalCount == 0);
				NKCUtil.SetGameobjectActive(this.m_objGear, this.m_LoopScrollRectEquip.TotalCount > 0);
				if (this.m_LatestSelectedSlot == null && this.m_ssActive.SortedEquipList.Count > 0 && this.m_lstVisibleEquipSlot.Count > 0)
				{
					for (int i = 0; i < this.m_lstVisibleEquipSlot.Count; i++)
					{
						this.m_lstVisibleEquipSlot[i].SetSelected(false);
					}
					this.SetEquipInfo(null);
					return;
				}
			}
			else
			{
				this.m_LoopScrollRect.TotalCount = this.GetSlotCount(this.m_NKC_INVENTORY_TAB);
				this.m_LoopScrollRect.SetIndexPosition(0);
				NKCUtil.SetGameobjectActive(this.m_objEmpty, this.m_LoopScrollRect.TotalCount == 0);
			}
		}

		// Token: 0x06006752 RID: 26450 RVA: 0x0021424C File Offset: 0x0021244C
		public void SetOpenSortingMenu(bool bOpen, bool bAnimate = true)
		{
			this.m_cbtnSortTypeMenu.Select(bOpen, true, false);
			this.m_NKCEquipPopupSort.StartRectMove(bOpen, bAnimate);
		}

		// Token: 0x06006753 RID: 26451 RVA: 0x0021426C File Offset: 0x0021246C
		public void UpdateEquipSlot(long equipUID)
		{
			bool flag = true;
			if (this.m_currentOption.m_NKC_INVENTORY_OPEN_TYPE == NKC_INVENTORY_OPEN_TYPE.NIOT_NORMAL && this.m_NKC_INVENTORY_TAB == NKCUIInventory.NKC_INVENTORY_TAB.NIT_EQUIP)
			{
				for (int i = 0; i < this.m_lstVisibleEquipSlot.Count; i++)
				{
					NKCUISlotEquip nkcuislotEquip = this.m_lstVisibleEquipSlot[i];
					if (nkcuislotEquip != null && nkcuislotEquip.IsActive() && nkcuislotEquip.GetNKMEquipItemData() != null && nkcuislotEquip.GetNKMEquipItemData().m_ItemUid == equipUID)
					{
						NKMEquipItemData itemEquip = NKCScenManager.CurrentUserData().m_InventoryData.GetItemEquip(equipUID);
						bool bPresetContained = false;
						if (itemEquip != null)
						{
							bPresetContained = NKCEquipPresetDataManager.HSPresetEquipUId.Contains(itemEquip.m_ItemUid);
						}
						nkcuislotEquip.SetData(itemEquip, null, this.m_bLockMaxItem, this.m_currentOption.bSkipItemEquipBox, this.m_currentOption.bShowFierceUI, bPresetContained);
						nkcuislotEquip.SetLock(itemEquip.m_bLock, this.m_currentOption.bEnableLockEquipSystem);
						if (this.m_LatestOpenNKMEquipItemData != null && this.m_LatestOpenNKMEquipItemData.m_ItemUid == equipUID)
						{
							nkcuislotEquip.SetSelected(true);
						}
						flag = false;
					}
				}
				this.m_LoopScrollRectEquip.TotalCount = this.m_ssActive.SortedEquipList.Count;
				if (flag)
				{
					int indexPosition = 0;
					if (equipUID > 0L)
					{
						indexPosition = this.m_ssActive.SortedEquipList.FindIndex((NKMEquipItemData x) => x.m_ItemUid == equipUID);
					}
					this.m_LoopScrollRectEquip.SetIndexPosition(indexPosition);
					return;
				}
				this.m_LoopScrollRectEquip.RefreshCells(false);
			}
		}

		// Token: 0x06006754 RID: 26452 RVA: 0x002143FC File Offset: 0x002125FC
		public void ForceRefreshMiscTab()
		{
			this.OnSelectTab(NKCUIInventory.NKC_INVENTORY_TAB.NIT_MISC, true);
		}

		// Token: 0x06006755 RID: 26453 RVA: 0x00214406 File Offset: 0x00212606
		public void ForceRefreshEquipTab()
		{
			this.OnSelectTab(NKCUIInventory.NKC_INVENTORY_TAB.NIT_EQUIP, true);
		}

		// Token: 0x06006756 RID: 26454 RVA: 0x00214410 File Offset: 0x00212610
		public void OnSelectMiscTab()
		{
			this.m_currentOption.strEmptyMessage = NKCUtilString.GET_STRING_INVEN_MISC_NO_EXIST;
			this.m_lbEmptyMessage.text = this.m_currentOption.strEmptyMessage;
			this.OnSelectTab(NKCUIInventory.NKC_INVENTORY_TAB.NIT_MISC, false);
		}

		// Token: 0x06006757 RID: 26455 RVA: 0x00214440 File Offset: 0x00212640
		public void OnSelectEquipTab()
		{
			this.m_currentOption.strEmptyMessage = NKCUtilString.GET_STRING_INVEN_EQUIP_NO_EXIST;
			this.m_lbEmptyMessage.text = this.m_currentOption.strEmptyMessage;
			this.OnSelectTab(NKCUIInventory.NKC_INVENTORY_TAB.NIT_EQUIP, false);
		}

		// Token: 0x06006758 RID: 26456 RVA: 0x00214470 File Offset: 0x00212670
		private void OnCheckAscend(bool bValue)
		{
			if (this.m_currentOption.lstSortOption.Count == 0)
			{
				return;
			}
			this.m_currentOption.lstSortOption = this.m_NKCEquipPopupSort.ChangeAscend(this.m_currentOption.lstSortOption);
			this.SortList(this.m_currentOption.lstSortOption, true, true);
		}

		// Token: 0x06006759 RID: 26457 RVA: 0x002144C4 File Offset: 0x002126C4
		private List<NKMItemMiscData> GetItemMiscDataList(NKC_INVENTORY_OPEN_TYPE _NKC_INVENTORY_OPEN_TYPE)
		{
			List<NKMItemMiscData> list = null;
			if (_NKC_INVENTORY_OPEN_TYPE != NKC_INVENTORY_OPEN_TYPE.NIOT_NORMAL)
			{
				if (_NKC_INVENTORY_OPEN_TYPE == NKC_INVENTORY_OPEN_TYPE.NIOT_ITEM_DEV)
				{
					list = new List<NKMItemMiscData>();
					foreach (NKMItemMiscTemplet nkmitemMiscTemplet in NKMItemMiscTemplet.Values)
					{
						NKMItemMiscData item = this.MakeTempItemMiscData(nkmitemMiscTemplet.m_ItemMiscID);
						list.Add(item);
					}
				}
			}
			else
			{
				list = new List<NKMItemMiscData>(NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.MiscItems.Values);
			}
			return list;
		}

		// Token: 0x0600675A RID: 26458 RVA: 0x00214550 File Offset: 0x00212750
		private NKMItemMiscData MakeTempItemMiscData(int itemID)
		{
			return new NKMItemMiscData
			{
				ItemID = itemID,
				CountFree = 1L,
				CountPaid = 0L
			};
		}

		// Token: 0x0600675B RID: 26459 RVA: 0x00214570 File Offset: 0x00212770
		private int GetSlotCount(NKCUIInventory.NKC_INVENTORY_TAB type)
		{
			if (type == NKCUIInventory.NKC_INVENTORY_TAB.NIT_MISC)
			{
				return this.m_lstMiscData.Count;
			}
			if (type == NKCUIInventory.NKC_INVENTORY_TAB.NIT_MOLD)
			{
				return this.m_lstMoldTemplet.Count;
			}
			if (type != NKCUIInventory.NKC_INVENTORY_TAB.NIT_EQUIP)
			{
				Debug.Log("NKCUIInventory.GetSlotCount() ERROR type : " + type.ToString());
				return 0;
			}
			if (this.m_currentOption.m_dOnClickEmptySlot != null)
			{
				return this.m_ssActive.SortedEquipList.Count + 1;
			}
			return this.m_ssActive.SortedEquipList.Count;
		}

		// Token: 0x0600675C RID: 26460 RVA: 0x002145F0 File Offset: 0x002127F0
		private void UpdateItemList(NKCUIInventory.NKC_INVENTORY_TAB type)
		{
			if (type == NKCUIInventory.NKC_INVENTORY_TAB.NIT_MISC)
			{
				this.m_lstMiscData.Clear();
				List<NKMItemMiscData> itemMiscDataList = this.GetItemMiscDataList(this.m_eInventoryOpenType);
				if (itemMiscDataList != null)
				{
					for (int i = 0; i < itemMiscDataList.Count; i++)
					{
						NKMItemMiscData nkmitemMiscData = itemMiscDataList[i];
						if (nkmitemMiscData.TotalCount > 0L && (this.m_currentOption.AdditionalExcludeFilterFunc == null || this.m_currentOption.AdditionalExcludeFilterFunc(nkmitemMiscData.ItemID, this.m_currentOption.m_NKC_INVENTORY_OPEN_TYPE)))
						{
							if (this.m_eInventoryOpenType == NKC_INVENTORY_OPEN_TYPE.NIOT_NORMAL)
							{
								NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(nkmitemMiscData.ItemID);
								if (itemMiscTempletByID != null && !itemMiscTempletByID.IsHideInInven())
								{
									this.m_lstMiscData.Add(itemMiscDataList[i]);
								}
							}
							else
							{
								this.m_lstMiscData.Add(itemMiscDataList[i]);
							}
						}
					}
				}
				this.SortByItemName(false);
				return;
			}
			if (type == NKCUIInventory.NKC_INVENTORY_TAB.NIT_MOLD)
			{
				this.m_lstMoldTemplet.Clear();
				foreach (NKMItemMoldTemplet nkmitemMoldTemplet in NKMItemMoldTemplet.Values)
				{
					if (this.m_currentOption.AdditionalExcludeFilterFunc == null || this.m_currentOption.AdditionalExcludeFilterFunc(nkmitemMoldTemplet.m_MoldID, this.m_currentOption.m_NKC_INVENTORY_OPEN_TYPE))
					{
						this.m_lstMoldTemplet.Add(nkmitemMoldTemplet);
					}
				}
				this.ProcessUIFromCurrentDisplayedSortData(false);
				return;
			}
			if (type == NKCUIInventory.NKC_INVENTORY_TAB.NIT_EQUIP)
			{
				List<NKMEquipItemData> list = new List<NKMEquipItemData>();
				this.m_ssActive.FilterList(this.m_ssActive.FilterSet, this.m_ssActive.bHideEquippedItem);
				this.m_ssActive.GetCurrentEquipList(ref list);
				this.SortList(this.m_currentOption.lstSortOption, true, false);
				return;
			}
			Debug.Log("NKCUIInventory.GetSlotCount() ERROR type : " + type.ToString());
		}

		// Token: 0x0600675D RID: 26461 RVA: 0x002147C4 File Offset: 0x002129C4
		private void ChangeUI()
		{
			bool flag = false;
			if (this.m_NKC_INVENTORY_TAB == NKCUIInventory.NKC_INVENTORY_TAB.NIT_EQUIP)
			{
				flag = true;
			}
			bool bValue = true;
			if (this.m_currentOption.m_NKC_INVENTORY_OPEN_TYPE == NKC_INVENTORY_OPEN_TYPE.NIOT_EQUIP_SELECT || this.m_currentOption.m_NKC_INVENTORY_OPEN_TYPE == NKC_INVENTORY_OPEN_TYPE.NIOT_ITEM_DEV || this.m_currentOption.m_NKC_INVENTORY_OPEN_TYPE == NKC_INVENTORY_OPEN_TYPE.NIOT_EQUIP_DEV || this.m_currentOption.m_NKC_INVENTORY_OPEN_TYPE == NKC_INVENTORY_OPEN_TYPE.NIOT_MOLD_DEV)
			{
				bValue = false;
			}
			float y = 0f;
			if (flag)
			{
				y = 131.05f;
			}
			this.m_RectMask.offsetMin = new Vector2(this.m_RectMask.offsetMin.x, y);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_INVENTORY_TAP_MISC.gameObject, bValue);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_INVENTORY_TAP_GEAR.gameObject, bValue);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_INVENTORY_MENU_VERTICAL, flag);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_INVENTORY_MENU_FILTER, flag);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_INVENTORY_MENU_ARRAY, flag);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_INVENTORY_TEXT1, flag);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_INVENTORY_LINE, flag);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_INVENTORY_TEXTS, flag);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_INVENTORY_TEXT, flag);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_INVENTORY_NUMBER_TEXT, flag);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_INVENTORY_ADD, flag);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_INVENTORY_MENU_LOCK, flag && this.m_currentOption.m_NKC_INVENTORY_OPEN_TYPE == NKC_INVENTORY_OPEN_TYPE.NIOT_NORMAL);
			NKCUtil.SetGameobjectActive(this.m_ctgDescending, flag);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_INVENTORY_MENU_DELETE, flag && this.m_currentOption.m_NKC_INVENTORY_OPEN_TYPE == NKC_INVENTORY_OPEN_TYPE.NIOT_NORMAL);
			this.m_NKM_UI_INVENTORY_TAP_MISC.Select(this.m_NKC_INVENTORY_TAB == NKCUIInventory.NKC_INVENTORY_TAB.NIT_MISC, false, false);
			this.m_NKM_UI_INVENTORY_TAP_GEAR.Select(this.m_NKC_INVENTORY_TAB == NKCUIInventory.NKC_INVENTORY_TAB.NIT_EQUIP, false, false);
			this.m_NKM_UI_INVENTORY_MENU_LOCK.Select(flag && this.m_currentOption.bEnableLockEquipSystem, false, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_INVENTORY_MENU_CHOICE, this.m_currentOption.bMultipleSelect);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_INVENTORY_MENU_CHOICE_GET_ITEM, this.m_currentOption.bShowRemoveItem);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_INVENTORY_MENU_LOCK_MSG, flag && this.m_currentOption.bEnableLockEquipSystem);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_INVENTORY_MENU_DELETE_MSG, flag && this.m_currentOption.bShowRemoveItem);
		}

		// Token: 0x0600675E RID: 26462 RVA: 0x002149D4 File Offset: 0x00212BD4
		private void UpdateSelectedEquipBreakupResult()
		{
			if (!this.m_currentOption.bShowRemoveItem)
			{
				for (int i = 0; i < this.m_lstBotNKCUISlot.Count; i++)
				{
					NKCUtil.SetGameobjectActive(this.m_lstBotNKCUISlot[i].gameObject, false);
				}
				return;
			}
			Dictionary<int, NKMEquipTemplet.OnRemoveItemData> dictionary = new Dictionary<int, NKMEquipTemplet.OnRemoveItemData>();
			List<long> list = new List<long>(this.m_hsCurrentSelectedEquips);
			NKMInventoryData inventoryData = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData;
			for (int i = 0; i < list.Count; i++)
			{
				NKMEquipItemData itemEquip = inventoryData.GetItemEquip(list[i]);
				if (itemEquip != null)
				{
					NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID);
					if (equipTemplet != null)
					{
						for (int j = 0; j < equipTemplet.m_OnRemoveItemList.Count; j++)
						{
							NKMEquipTemplet.OnRemoveItemData onRemoveItemData = equipTemplet.m_OnRemoveItemList[j];
							NKMEquipTemplet.OnRemoveItemData onRemoveItemData2;
							if (dictionary.TryGetValue(onRemoveItemData.m_ItemID, out onRemoveItemData2))
							{
								onRemoveItemData2.m_ItemCount += onRemoveItemData.m_ItemCount;
								dictionary[onRemoveItemData.m_ItemID] = onRemoveItemData2;
							}
							else
							{
								onRemoveItemData2 = default(NKMEquipTemplet.OnRemoveItemData);
								onRemoveItemData2.m_ItemID = onRemoveItemData.m_ItemID;
								onRemoveItemData2.m_ItemCount = onRemoveItemData.m_ItemCount;
								dictionary.Add(onRemoveItemData2.m_ItemID, onRemoveItemData2);
							}
						}
					}
				}
			}
			List<NKMEquipTemplet.OnRemoveItemData> list2 = new List<NKMEquipTemplet.OnRemoveItemData>(dictionary.Values);
			int num = list2.Count - this.m_lstBotNKCUISlot.Count;
			for (int i = 0; i < num; i++)
			{
				NKCUISlot newInstance = NKCUISlot.GetNewInstance(this.m_NKM_UI_UNIT_SELECT_LIST_CHOICE_GET_ITEM_LIST_Content);
				if (newInstance != null)
				{
					this.m_lstBotNKCUISlot.Add(newInstance);
				}
			}
			for (int i = 0; i < this.m_lstBotNKCUISlot.Count; i++)
			{
				if (i < list2.Count)
				{
					this.m_lstBotNKCUISlot[i].transform.localScale = new Vector3(0.65f, 0.65f, 0.65f);
					this.m_lstBotNKCUISlot[i].m_cbtnButton.UpdateOrgSize();
					NKCUtil.SetGameobjectActive(this.m_lstBotNKCUISlot[i].gameObject, true);
					this.m_lstBotNKCUISlot[i].SetData(NKCUISlot.SlotData.MakeMiscItemData(list2[i].m_ItemID, (long)list2[i].m_ItemCount, 0), false, true, true, null);
					this.m_lstBotNKCUISlot[i].SetOpenItemBoxOnClick();
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstBotNKCUISlot[i].gameObject, false);
				}
			}
		}

		// Token: 0x0600675F RID: 26463 RVA: 0x00214C4C File Offset: 0x00212E4C
		private void UpdateSelectedEquipCountUI()
		{
			if (this.m_currentOption.bMultipleSelect && this.m_hsCurrentSelectedEquips != null)
			{
				this.m_NKM_UI_INVENTORY_MENU_CHOICE_NUMBER_TEXT.text = string.Format("{0} / {1}", this.m_hsCurrentSelectedEquips.Count, this.m_currentOption.iMaxMultipleSelect);
			}
		}

		// Token: 0x06006760 RID: 26464 RVA: 0x00214CA4 File Offset: 0x00212EA4
		public void SetCurrentEquipCountUI()
		{
			NKMInventoryData inventoryData = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData;
			if (inventoryData == null)
			{
				return;
			}
			int countEquipItemTypes = inventoryData.GetCountEquipItemTypes();
			this.m_NKM_UI_INVENTORY_NUMBER_TEXT.GetComponent<Text>().text = string.Format("{0}/{1}", countEquipItemTypes, inventoryData.m_MaxItemEqipCount);
		}

		// Token: 0x06006761 RID: 26465 RVA: 0x00214CF8 File Offset: 0x00212EF8
		public void OnExpandInventoryPopup()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMInventoryData inventoryData = myUserData.m_InventoryData;
			if (inventoryData == null)
			{
				return;
			}
			int maxItemEqipCount = inventoryData.m_MaxItemEqipCount;
			NKM_INVENTORY_EXPAND_TYPE nkm_INVENTORY_EXPAND_TYPE = NKM_INVENTORY_EXPAND_TYPE.NIET_EQUIP;
			int count = 1;
			int num;
			bool flag = !NKCAdManager.IsAdRewardInventory(nkm_INVENTORY_EXPAND_TYPE) || !NKMInventoryManager.CanExpandInventoryByAd(nkm_INVENTORY_EXPAND_TYPE, myUserData, count, out num);
			if (!NKMInventoryManager.CanExpandInventory(nkm_INVENTORY_EXPAND_TYPE, myUserData, count, out num) && flag)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_CANNOT_EXPAND_INVENTORY), null, "");
				return;
			}
			string expandDesc = NKCUtilString.GetExpandDesc(nkm_INVENTORY_EXPAND_TYPE, false);
			NKCPopupInventoryAdd.SliderInfo sliderInfo = default(NKCPopupInventoryAdd.SliderInfo);
			sliderInfo.increaseCount = 5;
			sliderInfo.maxCount = 2000;
			sliderInfo.currentCount = maxItemEqipCount;
			sliderInfo.inventoryType = NKM_INVENTORY_EXPAND_TYPE.NIET_EQUIP;
			NKCPopupInventoryAdd.Instance.Open(NKCUtilString.GET_STRING_INVENTORY_EQUIP, expandDesc, sliderInfo, 50, 101, delegate(int value)
			{
				NKCPacketSender.Send_NKMPacket_INVENTORY_EXPAND_REQ(NKM_INVENTORY_EXPAND_TYPE.NIET_EQUIP, value);
			}, true);
		}

		// Token: 0x06006762 RID: 26466 RVA: 0x00214DDC File Offset: 0x00212FDC
		public void OnInventoryAdd()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_INVENTORY_EQUIPMENTS_UNLOCK, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_INVENTORY_EQUIPMENTS_UNLOCK, true);
		}

		// Token: 0x06006763 RID: 26467 RVA: 0x00214DF8 File Offset: 0x00212FF8
		public void OnLockModeToggle(bool bValue)
		{
			this.m_currentOption.bEnableLockEquipSystem = bValue;
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_INVENTORY_MENU_LOCK_MSG, bValue);
			this.m_NKM_UI_INVENTORY_MENU_DELETE_canvas.alpha = (bValue ? 0.3f : 1f);
			this.SetEquipInfo(this.m_LatestSelectedSlot);
			using (List<NKCUISlotEquip>.Enumerator enumerator = this.m_lstVisibleEquipSlot.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NKCUISlotEquip slot = enumerator.Current;
					if (!(null == slot))
					{
						NKMEquipItemData nkmequipItemData = this.m_ssActive.SortedEquipList.Find((NKMEquipItemData x) => x.m_ItemUid == slot.GetEquipItemUID());
						if (nkmequipItemData != null)
						{
							slot.SetLock(nkmequipItemData != null && nkmequipItemData.m_bLock, this.m_currentOption.bEnableLockEquipSystem);
						}
					}
				}
			}
		}

		// Token: 0x06006764 RID: 26468 RVA: 0x00214EE0 File Offset: 0x002130E0
		private void OnClickEquipSlot(NKCUISlotEquip slot, NKMEquipItemData equipData)
		{
			NKCUISlotEquip.OnSelectedEquipSlot dOnClickEquipSlot = this.m_dOnClickEquipSlot;
			if (dOnClickEquipSlot == null)
			{
				return;
			}
			dOnClickEquipSlot(slot, equipData);
		}

		// Token: 0x06006765 RID: 26469 RVA: 0x00214EF4 File Offset: 0x002130F4
		private void OnTouchMultiCancel()
		{
			if (this.m_eInventoryOpenType == NKC_INVENTORY_OPEN_TYPE.NIOT_EQUIP_SELECT)
			{
				if (this.m_currentOption.bShowRemoveItem)
				{
					this.OnRemoveMode(false);
					return;
				}
				base.Close();
			}
		}

		// Token: 0x06006766 RID: 26470 RVA: 0x00214F1C File Offset: 0x0021311C
		public void OnRemoveMode(bool bValue)
		{
			if (bValue)
			{
				this.m_prevOption = this.m_currentOption;
				this.m_currentOption = new NKCUIInventory.EquipSelectListOptions(NKC_INVENTORY_OPEN_TYPE.NIOT_EQUIP_SELECT, true, true);
				this.m_eInventoryOpenType = this.m_currentOption.m_NKC_INVENTORY_OPEN_TYPE;
				this.m_currentOption.m_dOnSelectedEquipSlot = null;
				this.m_currentOption.bMultipleSelect = bValue;
				this.m_currentOption.bShowRemoveItem = bValue;
				this.m_currentOption.iMaxMultipleSelect = 100;
				this.m_currentOption.bHideLockItem = true;
				this.m_currentOption.bHideMaxLvItem = false;
				this.m_currentOption.bLockMaxItem = false;
				this.m_currentOption.m_dOnFinishMultiSelection = new NKCUIInventory.OnFinishMultiSelection(this.RemoveItemList);
				this.m_currentOption.strEmptyMessage = NKCUtilString.GET_STRING_EQUIP_BREAK_UP_NO_EXIST_EQUIP;
				this.SetOnClickEquipSlot(new NKCUISlotEquip.OnSelectedEquipSlot(this.ToggleSelectedState));
			}
			else
			{
				this.m_hsCurrentSelectedEquips.Clear();
				this.m_currentOption = this.m_prevOption;
				this.m_currentOption.bEnableLockEquipSystem = false;
				this.SetOnClickEquipSlot(this.m_currentOption.m_dOnSelectedEquipSlot);
			}
			this.m_LatestSelectedSlot = null;
			this.m_LatestOpenNKMEquipItemData = null;
			this.OnSelectTab(this.m_NKC_INVENTORY_TAB, true);
		}

		// Token: 0x06006767 RID: 26471 RVA: 0x0021503C File Offset: 0x0021323C
		private void RemoveItemList(List<long> list)
		{
			if (list == null || list.Count <= 0)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_NO_EXIST_SELECTED_EQUIP, null, "");
				return;
			}
			NKMInventoryData inventoryData = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData;
			NKCPopupOKCancel.OnButton <>9__0;
			for (int i = 0; i < list.Count; i++)
			{
				NKMEquipItemData itemEquip = inventoryData.GetItemEquip(list[i]);
				if (itemEquip != null)
				{
					NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID);
					if (equipTemplet != null && equipTemplet.m_NKM_ITEM_GRADE >= NKM_ITEM_GRADE.NIG_SR)
					{
						string get_STRING_NOTICE = NKCUtilString.GET_STRING_NOTICE;
						string get_STRING_EQUIP_BREAK_UP_WARNING = NKCUtilString.GET_STRING_EQUIP_BREAK_UP_WARNING;
						NKCPopupOKCancel.OnButton onOkButton;
						if ((onOkButton = <>9__0) == null)
						{
							onOkButton = (<>9__0 = delegate()
							{
								NKCPacketSender.Send_NKMPacket_REMOVE_EQUIP_ITEM_REQ(list);
							});
						}
						NKCPopupOKCancel.OpenOKCancelBox(get_STRING_NOTICE, get_STRING_EQUIP_BREAK_UP_WARNING, onOkButton, null, false);
						return;
					}
				}
			}
			NKCPacketSender.Send_NKMPacket_REMOVE_EQUIP_ITEM_REQ(list);
		}

		// Token: 0x06006768 RID: 26472 RVA: 0x00215118 File Offset: 0x00213318
		private void OnTouchAutoSelect()
		{
			List<NKMEquipItemData> list = new List<NKMEquipItemData>();
			this.m_ssActive.FilterList(this.m_currentOption.setFilterOption, this.m_ssActive.bHideEquippedItem);
			this.m_ssActive.GetCurrentEquipList(ref list);
			int num = 0;
			while (this.m_hsCurrentSelectedEquips.Count < this.m_currentOption.iMaxMultipleSelect && num < list.Count)
			{
				NKMEquipItemData nkmequipItemData = list[num];
				if (this.AutoSelectFilter(nkmequipItemData) && !this.m_hsCurrentSelectedEquips.Contains(nkmequipItemData.m_ItemUid))
				{
					this.m_hsCurrentSelectedEquips.Add(nkmequipItemData.m_ItemUid);
					NKCUISlotEquip invenEquipSlot = this.GetInvenEquipSlot(nkmequipItemData.m_ItemUid);
					if (invenEquipSlot != null)
					{
						invenEquipSlot.SetSlotState(this.m_currentOption.bShowRemoveItem ? NKCUIInvenEquipSlot.EQUIP_SLOT_STATE.ESS_DELETE : NKCUIInvenEquipSlot.EQUIP_SLOT_STATE.ESS_SELECTED);
					}
				}
				num++;
			}
			this.UpdateSelectedEquipCountUI();
			this.UpdateSelectedEquipBreakupResult();
		}

		// Token: 0x06006769 RID: 26473 RVA: 0x002151F8 File Offset: 0x002133F8
		private bool AutoSelectFilter(NKMEquipItemData itemData)
		{
			if (itemData.m_OwnerUnitUID > 0L || itemData.m_EnchantLevel > 0 || itemData.m_bLock)
			{
				return false;
			}
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(itemData.m_ItemEquipID);
			return equipTemplet != null && equipTemplet.m_NKM_ITEM_GRADE <= NKM_ITEM_GRADE.NIG_R;
		}

		// Token: 0x0600676A RID: 26474 RVA: 0x00215240 File Offset: 0x00213440
		private NKCUISlotEquip GetInvenEquipSlot(long uid)
		{
			return this.m_lstVisibleEquipSlot.Find((NKCUISlotEquip v) => v.GetNKMEquipItemData() != null && v.GetEquipItemUID() == uid);
		}

		// Token: 0x0600676B RID: 26475 RVA: 0x00215274 File Offset: 0x00213474
		public void UpdateEquipment(long equipUID, NKMEquipItemData equipData)
		{
			NKMEquipItemData nkmequipItemData = this.m_ssActive.SortedEquipList.Find((NKMEquipItemData x) => x.m_ItemUid == equipUID);
			if (nkmequipItemData == null || equipData == null)
			{
				this.ForceRefreshEquipTab();
				return;
			}
			int i = 0;
			while (i < this.m_lstVisibleEquipSlot.Count)
			{
				if (this.m_lstVisibleEquipSlot[i].GetEquipItemUID() == equipUID)
				{
					this.m_lstVisibleEquipSlot[i].SetData(equipData, new NKCUISlotEquip.OnSelectedEquipSlot(this.OnSelectedSlot), this.m_bLockMaxItem, this.m_currentOption.bSkipItemEquipBox, this.m_currentOption.bShowFierceUI, NKCEquipPresetDataManager.HSPresetEquipUId.Contains(equipData.m_ItemUid));
					this.m_lstVisibleEquipSlot[i].SetLock(equipData != null && equipData.m_bLock, this.m_currentOption.bEnableLockEquipSystem);
					if (this.m_LatestOpenNKMEquipItemData != null && this.m_LatestOpenNKMEquipItemData.m_ItemUid == equipUID)
					{
						this.m_lstVisibleEquipSlot[i].SetSelected(true);
						this.SetEquipInfo(this.m_lstVisibleEquipSlot[i]);
						return;
					}
					break;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x0600676C RID: 26476 RVA: 0x002153A4 File Offset: 0x002135A4
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			if (this.m_NKC_INVENTORY_TAB == NKCUIInventory.NKC_INVENTORY_TAB.NIT_MISC)
			{
				if (base.IsHidden)
				{
					this.m_bNeedRefresh = true;
					return;
				}
				this.UpdateItemList(NKCUIInventory.NKC_INVENTORY_TAB.NIT_MISC);
			}
		}

		// Token: 0x0600676D RID: 26477 RVA: 0x002153C8 File Offset: 0x002135C8
		public override void OnEquipChange(NKMUserData.eChangeNotifyType eType, long equipUID, NKMEquipItemData equipItem)
		{
			if (this.m_NKC_INVENTORY_TAB == NKCUIInventory.NKC_INVENTORY_TAB.NIT_EQUIP)
			{
				if (this.m_ssActive != null && this.m_ssActive.SortedEquipList != null)
				{
					if (equipItem != null)
					{
						NKMEquipItemData nkmequipItemData = this.m_ssActive.SortedEquipList.Find((NKMEquipItemData x) => x.m_ItemUid == equipUID);
						if (nkmequipItemData != null)
						{
							this.m_ssActive.SortedEquipList.IndexOf(nkmequipItemData);
							this.m_ssActive.UpdateEquipData(equipItem);
							if (this.m_slotEquipInfo.GetEquipItemUID() == equipUID)
							{
								this.m_slotEquipInfo.SetData(equipItem, this.m_currentOption.bShowFierceUI, false);
							}
						}
					}
					else
					{
						this.m_dicEquipSortSystem.Remove(this.m_eInventoryOpenType);
						this.m_ssActive = this.GetEquipSortSystem(this.m_eInventoryOpenType);
					}
				}
				if (base.IsHidden)
				{
					this.m_bNeedRefresh = true;
					return;
				}
				this.UpdateEquipment(equipUID, equipItem);
			}
		}

		// Token: 0x0600676E RID: 26478 RVA: 0x002154B8 File Offset: 0x002136B8
		public HashSet<long> GetSelectedEquips()
		{
			return new HashSet<long>(this.m_hsCurrentSelectedEquips);
		}

		// Token: 0x0600676F RID: 26479 RVA: 0x002154C8 File Offset: 0x002136C8
		public RectTransform ScrollToUnitAndGetRect(long UID)
		{
			int num = this.m_ssActive.SortedEquipList.FindIndex((NKMEquipItemData x) => x != null && x.m_ItemUid == UID);
			if (num < 0)
			{
				Debug.LogError("Target unit not found!!");
				return null;
			}
			if (this.m_NKC_INVENTORY_TAB == NKCUIInventory.NKC_INVENTORY_TAB.NIT_EQUIP)
			{
				this.m_LoopScrollRectEquip.SetIndexPosition(num);
			}
			else
			{
				this.m_LoopScrollRect.SetIndexPosition(num);
			}
			NKCUISlotEquip invenEquipSlot = this.GetInvenEquipSlot(UID);
			if (invenEquipSlot == null)
			{
				return null;
			}
			return invenEquipSlot.gameObject.GetComponent<RectTransform>();
		}

		// Token: 0x0400533C RID: 21308
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_inventory";

		// Token: 0x0400533D RID: 21309
		private const string UI_ASSET_NAME = "NKM_UI_INVENTORY_V2";

		// Token: 0x0400533E RID: 21310
		private static NKCUIInventory m_Instance;

		// Token: 0x0400533F RID: 21311
		private GameObject m_NKM_UI_UNIT_INVENTORY;

		// Token: 0x04005340 RID: 21312
		public RectTransform m_rtNKM_UI_INVENTORY_LIST_ITEM;

		// Token: 0x04005341 RID: 21313
		public NKCUIComToggle m_ctgDescending;

		// Token: 0x04005342 RID: 21314
		public RectTransform m_RectMask;

		// Token: 0x04005343 RID: 21315
		[Header("멀티 선택")]
		public GameObject m_NKM_UI_INVENTORY_MENU_CHOICE;

		// Token: 0x04005344 RID: 21316
		public Text m_NKM_UI_INVENTORY_MENU_CHOICE_NUMBER_TEXT;

		// Token: 0x04005345 RID: 21317
		public GameObject m_NKM_UI_INVENTORY_MENU_CHOICE_GET_ITEM;

		// Token: 0x04005346 RID: 21318
		public Transform m_NKM_UI_UNIT_SELECT_LIST_CHOICE_GET_ITEM_LIST_Content;

		// Token: 0x04005347 RID: 21319
		public NKCUIComButton m_cbtnFinishMultiSelect;

		// Token: 0x04005348 RID: 21320
		public NKCUIComButton NKM_UI_INVENTORY_MENU_AUTO_BUTTON;

		// Token: 0x04005349 RID: 21321
		public NKCUIComButton m_NKM_UI_INVENTORY_MENU_CANCEL_BUTTON;

		// Token: 0x0400534A RID: 21322
		[Header("탭 버튼들")]
		public NKCUIComStateButton m_NKM_UI_INVENTORY_TAP_MISC;

		// Token: 0x0400534B RID: 21323
		public NKCUIComStateButton m_NKM_UI_INVENTORY_TAP_GEAR;

		// Token: 0x0400534C RID: 21324
		[Header("탭별 보이는 화면")]
		public GameObject m_objMisc;

		// Token: 0x0400534D RID: 21325
		public GameObject m_objGear;

		// Token: 0x0400534E RID: 21326
		[Header("비품탭에서 비활성화될것들")]
		public GameObject m_NKM_UI_INVENTORY_TEXTS;

		// Token: 0x0400534F RID: 21327
		public GameObject m_NKM_UI_INVENTORY_MENU_VERTICAL;

		// Token: 0x04005350 RID: 21328
		public GameObject m_NKM_UI_INVENTORY_MENU_ARRAY;

		// Token: 0x04005351 RID: 21329
		public GameObject m_NKM_UI_INVENTORY_MENU_FILTER;

		// Token: 0x04005352 RID: 21330
		public GameObject m_NKM_UI_INVENTORY_TEXT1;

		// Token: 0x04005353 RID: 21331
		public GameObject m_NKM_UI_INVENTORY_LINE;

		// Token: 0x04005354 RID: 21332
		public GameObject m_NKM_UI_INVENTORY_TEXT;

		// Token: 0x04005355 RID: 21333
		public GameObject m_NKM_UI_INVENTORY_NUMBER_TEXT;

		// Token: 0x04005356 RID: 21334
		public NKCUIComToggle m_NKM_UI_INVENTORY_MENU_LOCK;

		// Token: 0x04005357 RID: 21335
		public NKCUIComStateButton m_NKM_UI_INVENTORY_MENU_DELETE;

		// Token: 0x04005358 RID: 21336
		public CanvasGroup m_NKM_UI_INVENTORY_MENU_DELETE_canvas;

		// Token: 0x04005359 RID: 21337
		public NKCUIComStateButton m_NKM_UI_INVENTORY_ADD;

		// Token: 0x0400535A RID: 21338
		public GameObject m_NKM_UI_INVENTORY_EQUIPMENTS_UNLOCK;

		// Token: 0x0400535B RID: 21339
		public GameObject m_NKM_UI_INVENTORY_MENU_LOCK_MSG;

		// Token: 0x0400535C RID: 21340
		public GameObject m_NKM_UI_INVENTORY_MENU_DELETE_MSG;

		// Token: 0x0400535D RID: 21341
		[Header("필터링 유닛 타입 선택")]
		public NKCUIComStateButton m_btnFilterOption;

		// Token: 0x0400535E RID: 21342
		public GameObject m_objFilterSelected;

		// Token: 0x0400535F RID: 21343
		[Header("리스트가 비었을 때")]
		public GameObject m_objEmpty;

		// Token: 0x04005360 RID: 21344
		public Text m_lbEmptyMessage;

		// Token: 0x04005361 RID: 21345
		[Header("정렬 방식 선택")]
		public NKCUIComToggle m_cbtnSortTypeMenu;

		// Token: 0x04005362 RID: 21346
		public GameObject m_objSortSelect;

		// Token: 0x04005363 RID: 21347
		public NKCPopupEquipSort m_NKCEquipPopupSort;

		// Token: 0x04005364 RID: 21348
		public Text m_lbSortType;

		// Token: 0x04005365 RID: 21349
		public Text m_lbSelectedSortType;

		// Token: 0x04005366 RID: 21350
		[Header("우측 정보창")]
		public GameObject m_objEquipNotSelected;

		// Token: 0x04005367 RID: 21351
		public GameObject m_objEquipInfo;

		// Token: 0x04005368 RID: 21352
		public NKCUIInvenEquipSlot m_slotEquipInfo;

		// Token: 0x04005369 RID: 21353
		public NKCUIComStateButton m_UnEquipButton;

		// Token: 0x0400536A RID: 21354
		public NKCUIComStateButton m_ReinforceButton;

		// Token: 0x0400536B RID: 21355
		public NKCUIComStateButton m_ReinforceButtonLock;

		// Token: 0x0400536C RID: 21356
		public NKCUIComStateButton m_EquipButton;

		// Token: 0x0400536D RID: 21357
		public NKCUIComStateButton m_ChangeButton;

		// Token: 0x0400536E RID: 21358
		public NKCUIComStateButton m_OkButton;

		// Token: 0x0400536F RID: 21359
		[Header("Misc Loop Scroll")]
		public RectTransform m_rectContentRect;

		// Token: 0x04005370 RID: 21360
		public RectTransform m_rectSlotPoolRect;

		// Token: 0x04005371 RID: 21361
		public LoopScrollRect m_LoopScrollRect;

		// Token: 0x04005372 RID: 21362
		public GridLayoutGroup m_GridLayoutGroup;

		// Token: 0x04005373 RID: 21363
		public RectTransform m_NKM_UI_INVENTORY_LIST_ITEM;

		// Token: 0x04005374 RID: 21364
		public NKCUIComSafeArea m_safeArea;

		// Token: 0x04005375 RID: 21365
		[Header("Equip Loop Scroll")]
		public RectTransform m_rectContentRectEquip;

		// Token: 0x04005376 RID: 21366
		public RectTransform m_rectSlotPoolRectEquip;

		// Token: 0x04005377 RID: 21367
		public LoopScrollRect m_LoopScrollRectEquip;

		// Token: 0x04005378 RID: 21368
		public GridLayoutGroup m_GridLayoutGroupEquip;

		// Token: 0x04005379 RID: 21369
		public RectTransform m_NKM_UI_INVENTORY_LIST_ITEM_EQUIP;

		// Token: 0x0400537A RID: 21370
		public NKCUIComSafeArea m_safeAreaEquip;

		// Token: 0x0400537B RID: 21371
		private NKCUIInventory.EquipSelectListOptions m_currentOption = new NKCUIInventory.EquipSelectListOptions(NKC_INVENTORY_OPEN_TYPE.NIOT_NORMAL, false, false);

		// Token: 0x0400537C RID: 21372
		private NKCUIInventory.EquipSelectListOptions m_prevOption = new NKCUIInventory.EquipSelectListOptions(NKC_INVENTORY_OPEN_TYPE.NIOT_NORMAL, false, false);

		// Token: 0x0400537D RID: 21373
		private NKCUISlotEquip m_LatestSelectedSlot;

		// Token: 0x0400537E RID: 21374
		private List<NKCUISlot> m_lstBotNKCUISlot = new List<NKCUISlot>();

		// Token: 0x0400537F RID: 21375
		private bool m_bNeedRefresh;

		// Token: 0x04005380 RID: 21376
		private const int REMOVE_EQUIP_MAX = 100;

		// Token: 0x04005381 RID: 21377
		private readonly HashSet<NKCEquipSortSystem.eFilterCategory> m_setEquipFilterCategory = new HashSet<NKCEquipSortSystem.eFilterCategory>
		{
			NKCEquipSortSystem.eFilterCategory.UnitType,
			NKCEquipSortSystem.eFilterCategory.EquipType,
			NKCEquipSortSystem.eFilterCategory.Rarity,
			NKCEquipSortSystem.eFilterCategory.Equipped,
			NKCEquipSortSystem.eFilterCategory.Tier,
			NKCEquipSortSystem.eFilterCategory.Locked,
			NKCEquipSortSystem.eFilterCategory.SetOptionPart,
			NKCEquipSortSystem.eFilterCategory.SetOptionType,
			NKCEquipSortSystem.eFilterCategory.StatType,
			NKCEquipSortSystem.eFilterCategory.PrivateEquip
		};

		// Token: 0x04005382 RID: 21378
		private NKCEquipSortSystem m_ssActive;

		// Token: 0x04005383 RID: 21379
		private Dictionary<NKC_INVENTORY_OPEN_TYPE, NKCEquipSortSystem> m_dicEquipSortSystem = new Dictionary<NKC_INVENTORY_OPEN_TYPE, NKCEquipSortSystem>();

		// Token: 0x04005384 RID: 21380
		private List<int> RESOURCE_LIST;

		// Token: 0x04005385 RID: 21381
		private NKCUIInventory.NKC_INVENTORY_TAB m_NKC_INVENTORY_TAB = NKCUIInventory.NKC_INVENTORY_TAB.NIT_NONE;

		// Token: 0x04005386 RID: 21382
		private HashSet<long> m_hsCurrentSelectedEquips = new HashSet<long>();

		// Token: 0x04005387 RID: 21383
		[Header("유닛 슬롯 프리팹 & 사이즈 설정")]
		public NKCUISlot m_pfbUISlot;

		// Token: 0x04005388 RID: 21384
		public Vector2 m_vUISlotSize;

		// Token: 0x04005389 RID: 21385
		public Vector2 m_vUISlotSpacing;

		// Token: 0x0400538A RID: 21386
		public float m_vOffsetX;

		// Token: 0x0400538B RID: 21387
		public NKCUISlotEquip m_pfbEquipSlot;

		// Token: 0x0400538C RID: 21388
		public Vector2 m_vEquipSlotSize;

		// Token: 0x0400538D RID: 21389
		public Vector2 m_vEquipSlotSpacing;

		// Token: 0x0400538E RID: 21390
		public float m_vEquipOffsetX;

		// Token: 0x0400538F RID: 21391
		private List<NKCUISlotEquip> m_lstVisibleEquipSlot = new List<NKCUISlotEquip>();

		// Token: 0x04005390 RID: 21392
		private Stack<NKCUISlotEquip> m_stkEquipSlotPool = new Stack<NKCUISlotEquip>();

		// Token: 0x04005391 RID: 21393
		private List<NKCUISlot> m_lstVisibleMiscSlot = new List<NKCUISlot>();

		// Token: 0x04005392 RID: 21394
		private Stack<NKCUISlot> m_stkMiscSlotPool = new Stack<NKCUISlot>();

		// Token: 0x04005393 RID: 21395
		private bool m_bLockMaxItem;

		// Token: 0x04005394 RID: 21396
		private NKCUIInventory.OnGetItemListAfterSelected m_dOnGetItemListAfterSelected;

		// Token: 0x04005395 RID: 21397
		private NKCUISlotEquip.OnSelectedEquipSlot m_dOnClickEquipSlot;

		// Token: 0x04005396 RID: 21398
		private NKCPopupEquipChange m_NKCPopupEquipChange;

		// Token: 0x04005397 RID: 21399
		private long lastSelectItemUID;

		// Token: 0x04005398 RID: 21400
		private ITEM_EQUIP_POSITION lastSelectEquipPos = ITEM_EQUIP_POSITION.IEP_NONE;

		// Token: 0x04005399 RID: 21401
		private long m_beforeItemUID;

		// Token: 0x0400539A RID: 21402
		private long m_afterItemUId;

		// Token: 0x0400539B RID: 21403
		private ITEM_EQUIP_POSITION m_equipPosition = ITEM_EQUIP_POSITION.IEP_NONE;

		// Token: 0x0400539C RID: 21404
		private NKMEquipItemData m_LatestOpenNKMEquipItemData;

		// Token: 0x0400539D RID: 21405
		private long m_LatestTargetUnitUIDToEquip;

		// Token: 0x0400539E RID: 21406
		private List<NKMItemMiscData> m_lstMiscData = new List<NKMItemMiscData>();

		// Token: 0x0400539F RID: 21407
		private List<NKMEquipItemData> m_lstEquipData = new List<NKMEquipItemData>();

		// Token: 0x040053A0 RID: 21408
		private List<NKMItemMoldTemplet> m_lstMoldTemplet = new List<NKMItemMoldTemplet>();

		// Token: 0x040053A1 RID: 21409
		private NKC_INVENTORY_OPEN_TYPE m_eInventoryOpenType;

		// Token: 0x02001682 RID: 5762
		public struct EquipSelectListOptions
		{
			// Token: 0x0600B070 RID: 45168 RVA: 0x0035F114 File Offset: 0x0035D314
			public EquipSelectListOptions(NKC_INVENTORY_OPEN_TYPE inventoryOpenType, bool _bMultipleSelect, bool bUseDefaultString = true)
			{
				this.m_NKC_INVENTORY_OPEN_TYPE = inventoryOpenType;
				this.bShowRemoveSlot = false;
				this.bMultipleSelect = _bMultipleSelect;
				this.iMaxMultipleSelect = 8;
				this.bEnableLockEquipSystem = false;
				this.bUseMainEquipMark = false;
				this.bShowRemoveItem = false;
				this.bSkipItemEquipBox = false;
				this.bShowFierceUI = false;
				this.presetUnitStyeType = NKM_UNIT_STYLE_TYPE.NUST_INVALID;
				this.iPresetIndex = -1;
				if (bUseDefaultString)
				{
					this.strUpsideMenuName = NKCUtilString.GET_STRING_INVEN_EQUIP_SELECT;
				}
				else
				{
					this.strUpsideMenuName = "";
				}
				this.strEmptyMessage = "";
				this.strGuideTempletID = "";
				this.equipChangeTargetPosition = ITEM_EQUIP_POSITION.IEP_NONE;
				this.m_hsSelectedEquipUIDToShow = new HashSet<long>();
				this.dOnClose = null;
				this.m_dOnClickItemSlot = null;
				this.m_dOnSelectedEquipSlot = null;
				this.m_dOnClickItemMoldSlot = null;
				this.m_dOnFinishMultiSelection = null;
				this.m_dOnClickEmptySlot = null;
				this.m_dOnGetItemListAfterSelected = null;
				this.AdditionalExcludeFilterFunc = null;
				this.m_EquipListOptions = default(NKCEquipSortSystem.EquipListOptions);
				this.m_EquipListOptions.setOnlyIncludeEquipID = null;
				this.m_EquipListOptions.setExcludeEquipID = null;
				this.m_EquipListOptions.setExcludeEquipUID = null;
				this.m_EquipListOptions.setExcludeFilterOption = null;
				this.m_EquipListOptions.setFilterOption = new HashSet<NKCEquipSortSystem.eFilterOption>();
				this.m_EquipListOptions.lstSortOption = new List<NKCEquipSortSystem.eSortOption>();
				this.m_EquipListOptions.lstSortOption = NKCEquipSortSystem.AddDefaultSortOptions(this.m_EquipListOptions.lstSortOption);
				this.m_EquipListOptions.PreemptiveSortFunc = null;
				this.m_EquipListOptions.AdditionalExcludeFilterFunc = null;
				this.m_EquipListOptions.bHideEquippedItem = false;
				this.m_EquipListOptions.bPushBackUnselectable = true;
				this.m_EquipListOptions.bHideLockItem = false;
				this.m_EquipListOptions.bHideMaxLvItem = false;
				this.m_EquipListOptions.bLockMaxItem = false;
				this.m_EquipListOptions.OwnerUnitID = 0;
				this.m_EquipListOptions.bHideNotPossibleSetOptionItem = false;
				this.m_EquipListOptions.iTargetUnitID = 0;
				this.m_EquipListOptions.FilterStatType_01 = NKM_STAT_TYPE.NST_RANDOM;
				this.m_EquipListOptions.FilterStatType_02 = NKM_STAT_TYPE.NST_RANDOM;
				this.m_EquipListOptions.FilterStatType_03 = NKM_STAT_TYPE.NST_RANDOM;
				this.m_EquipListOptions.FilterSetOptionID = 0;
				this.m_EquipListOptions.lstCustomSortFunc = new Dictionary<NKCEquipSortSystem.eSortCategory, KeyValuePair<string, NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc>>();
				this.lastSelectedItemUID = 0L;
				this.lEquipOptionCachingByUnitUID = 0L;
				this.m_ButtonMenuType = NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_ENFORCE_AND_EQUIP;
				this.bShowEquipUpgradeState = false;
			}

			// Token: 0x1700190B RID: 6411
			// (get) Token: 0x0600B071 RID: 45169 RVA: 0x0035F336 File Offset: 0x0035D536
			// (set) Token: 0x0600B072 RID: 45170 RVA: 0x0035F343 File Offset: 0x0035D543
			public HashSet<NKCEquipSortSystem.eFilterOption> setFilterOption
			{
				get
				{
					return this.m_EquipListOptions.setFilterOption;
				}
				set
				{
					this.m_EquipListOptions.setFilterOption = value;
				}
			}

			// Token: 0x1700190C RID: 6412
			// (get) Token: 0x0600B073 RID: 45171 RVA: 0x0035F351 File Offset: 0x0035D551
			// (set) Token: 0x0600B074 RID: 45172 RVA: 0x0035F35E File Offset: 0x0035D55E
			public List<NKCEquipSortSystem.eSortOption> lstSortOption
			{
				get
				{
					return this.m_EquipListOptions.lstSortOption;
				}
				set
				{
					this.m_EquipListOptions.lstSortOption = value;
				}
			}

			// Token: 0x1700190D RID: 6413
			// (get) Token: 0x0600B075 RID: 45173 RVA: 0x0035F36C File Offset: 0x0035D56C
			// (set) Token: 0x0600B076 RID: 45174 RVA: 0x0035F379 File Offset: 0x0035D579
			public bool bPushBackUnselectable
			{
				get
				{
					return this.m_EquipListOptions.bPushBackUnselectable;
				}
				set
				{
					this.m_EquipListOptions.bPushBackUnselectable = value;
				}
			}

			// Token: 0x1700190E RID: 6414
			// (get) Token: 0x0600B077 RID: 45175 RVA: 0x0035F387 File Offset: 0x0035D587
			// (set) Token: 0x0600B078 RID: 45176 RVA: 0x0035F394 File Offset: 0x0035D594
			public bool bHideEquippedItem
			{
				get
				{
					return this.m_EquipListOptions.bHideEquippedItem;
				}
				set
				{
					this.m_EquipListOptions.bHideEquippedItem = value;
				}
			}

			// Token: 0x1700190F RID: 6415
			// (get) Token: 0x0600B079 RID: 45177 RVA: 0x0035F3A2 File Offset: 0x0035D5A2
			// (set) Token: 0x0600B07A RID: 45178 RVA: 0x0035F3AF File Offset: 0x0035D5AF
			public bool bHideLockItem
			{
				get
				{
					return this.m_EquipListOptions.bHideLockItem;
				}
				set
				{
					this.m_EquipListOptions.bHideLockItem = value;
				}
			}

			// Token: 0x17001910 RID: 6416
			// (get) Token: 0x0600B07B RID: 45179 RVA: 0x0035F3BD File Offset: 0x0035D5BD
			// (set) Token: 0x0600B07C RID: 45180 RVA: 0x0035F3CA File Offset: 0x0035D5CA
			public bool bLockMaxItem
			{
				get
				{
					return this.m_EquipListOptions.bLockMaxItem;
				}
				set
				{
					this.m_EquipListOptions.bLockMaxItem = value;
				}
			}

			// Token: 0x17001911 RID: 6417
			// (get) Token: 0x0600B07D RID: 45181 RVA: 0x0035F3D8 File Offset: 0x0035D5D8
			// (set) Token: 0x0600B07E RID: 45182 RVA: 0x0035F3E5 File Offset: 0x0035D5E5
			public bool bHideMaxLvItem
			{
				get
				{
					return this.m_EquipListOptions.bHideMaxLvItem;
				}
				set
				{
					this.m_EquipListOptions.bHideMaxLvItem = value;
				}
			}

			// Token: 0x17001912 RID: 6418
			// (get) Token: 0x0600B07F RID: 45183 RVA: 0x0035F3F3 File Offset: 0x0035D5F3
			// (set) Token: 0x0600B080 RID: 45184 RVA: 0x0035F400 File Offset: 0x0035D600
			public HashSet<long> setExcludeEquipUID
			{
				get
				{
					return this.m_EquipListOptions.setExcludeEquipUID;
				}
				set
				{
					this.m_EquipListOptions.setExcludeEquipUID = value;
				}
			}

			// Token: 0x17001913 RID: 6419
			// (get) Token: 0x0600B081 RID: 45185 RVA: 0x0035F40E File Offset: 0x0035D60E
			// (set) Token: 0x0600B082 RID: 45186 RVA: 0x0035F41B File Offset: 0x0035D61B
			public HashSet<int> setExcludeEquipID
			{
				get
				{
					return this.m_EquipListOptions.setExcludeEquipID;
				}
				set
				{
					this.m_EquipListOptions.setExcludeEquipID = value;
				}
			}

			// Token: 0x17001914 RID: 6420
			// (get) Token: 0x0600B083 RID: 45187 RVA: 0x0035F429 File Offset: 0x0035D629
			// (set) Token: 0x0600B084 RID: 45188 RVA: 0x0035F436 File Offset: 0x0035D636
			public HashSet<int> setOnlyIncludeEquipID
			{
				get
				{
					return this.m_EquipListOptions.setOnlyIncludeEquipID;
				}
				set
				{
					this.m_EquipListOptions.setOnlyIncludeEquipID = value;
				}
			}

			// Token: 0x0400A48C RID: 42124
			public NKCEquipSortSystem.EquipListOptions m_EquipListOptions;

			// Token: 0x0400A48D RID: 42125
			public NKC_INVENTORY_OPEN_TYPE m_NKC_INVENTORY_OPEN_TYPE;

			// Token: 0x0400A48E RID: 42126
			public bool bShowRemoveSlot;

			// Token: 0x0400A48F RID: 42127
			public bool bMultipleSelect;

			// Token: 0x0400A490 RID: 42128
			public int iMaxMultipleSelect;

			// Token: 0x0400A491 RID: 42129
			public bool bEnableLockEquipSystem;

			// Token: 0x0400A492 RID: 42130
			public bool bUseMainEquipMark;

			// Token: 0x0400A493 RID: 42131
			public bool bShowRemoveItem;

			// Token: 0x0400A494 RID: 42132
			public bool bSkipItemEquipBox;

			// Token: 0x0400A495 RID: 42133
			public bool bShowFierceUI;

			// Token: 0x0400A496 RID: 42134
			public string strUpsideMenuName;

			// Token: 0x0400A497 RID: 42135
			public string strEmptyMessage;

			// Token: 0x0400A498 RID: 42136
			public string strGuideTempletID;

			// Token: 0x0400A499 RID: 42137
			public ITEM_EQUIP_POSITION equipChangeTargetPosition;

			// Token: 0x0400A49A RID: 42138
			public long lastSelectedItemUID;

			// Token: 0x0400A49B RID: 42139
			public long lEquipOptionCachingByUnitUID;

			// Token: 0x0400A49C RID: 42140
			public HashSet<long> m_hsSelectedEquipUIDToShow;

			// Token: 0x0400A49D RID: 42141
			public NKM_UNIT_STYLE_TYPE presetUnitStyeType;

			// Token: 0x0400A49E RID: 42142
			public int iPresetIndex;

			// Token: 0x0400A49F RID: 42143
			public NKCUIInventory.EquipSelectListOptions.OnClose dOnClose;

			// Token: 0x0400A4A0 RID: 42144
			public NKCUISlot.OnClick m_dOnClickItemSlot;

			// Token: 0x0400A4A1 RID: 42145
			public NKCUISlotEquip.OnSelectedEquipSlot m_dOnSelectedEquipSlot;

			// Token: 0x0400A4A2 RID: 42146
			public NKCUISlot.OnClick m_dOnClickItemMoldSlot;

			// Token: 0x0400A4A3 RID: 42147
			public NKCUIInventory.OnFinishMultiSelection m_dOnFinishMultiSelection;

			// Token: 0x0400A4A4 RID: 42148
			public NKCUISlotEquip.OnSelectedEquipSlot m_dOnClickEmptySlot;

			// Token: 0x0400A4A5 RID: 42149
			public NKCUIInventory.OnGetItemListAfterSelected m_dOnGetItemListAfterSelected;

			// Token: 0x0400A4A6 RID: 42150
			public NKCUIInventory.EquipSelectListOptions.CustomFilterFunc AdditionalExcludeFilterFunc;

			// Token: 0x0400A4A7 RID: 42151
			public NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE m_ButtonMenuType;

			// Token: 0x0400A4A8 RID: 42152
			public bool bShowEquipUpgradeState;

			// Token: 0x02001A83 RID: 6787
			// (Invoke) Token: 0x0600BC2E RID: 48174
			public delegate void OnSlotSetData(NKCUISlotEquip cEquipSlot, NKMEquipItemData cNKMEquipData, NKMDeckIndex deckIndex);

			// Token: 0x02001A84 RID: 6788
			// (Invoke) Token: 0x0600BC32 RID: 48178
			public delegate void OnClose();

			// Token: 0x02001A85 RID: 6789
			// (Invoke) Token: 0x0600BC36 RID: 48182
			public delegate bool CustomFilterFunc(int id, NKC_INVENTORY_OPEN_TYPE type);
		}

		// Token: 0x02001683 RID: 5763
		// (Invoke) Token: 0x0600B086 RID: 45190
		public delegate void OnFinishMultiSelection(List<long> listEquipSlot);

		// Token: 0x02001684 RID: 5764
		// (Invoke) Token: 0x0600B08A RID: 45194
		public delegate void OnGetItemListAfterSelected(List<NKMEquipItemData> lstItemData);

		// Token: 0x02001685 RID: 5765
		public enum NKC_INVENTORY_TAB
		{
			// Token: 0x0400A4AA RID: 42154
			NIT_NONE = -1,
			// Token: 0x0400A4AB RID: 42155
			NIT_MISC,
			// Token: 0x0400A4AC RID: 42156
			NIT_EQUIP,
			// Token: 0x0400A4AD RID: 42157
			NIT_MOLD
		}

		// Token: 0x02001686 RID: 5766
		public class CompDescending : IComparer<NKMItemMiscData>
		{
			// Token: 0x0600B08D RID: 45197 RVA: 0x0035F444 File Offset: 0x0035D644
			public int Compare(NKMItemMiscData x, NKMItemMiscData y)
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(x.ItemID);
				NKMItemMiscTemplet itemMiscTempletByID2 = NKMItemManager.GetItemMiscTempletByID(y.ItemID);
				if (itemMiscTempletByID == null)
				{
					return 1;
				}
				if (itemMiscTempletByID2 == null)
				{
					return -1;
				}
				if (itemMiscTempletByID.WillBeDeletedSoon() && !itemMiscTempletByID2.WillBeDeletedSoon())
				{
					return 1;
				}
				if (!itemMiscTempletByID.WillBeDeletedSoon() && itemMiscTempletByID2.WillBeDeletedSoon())
				{
					return -1;
				}
				if (!itemMiscTempletByID.IsUsable() && itemMiscTempletByID2.IsUsable())
				{
					return 1;
				}
				if (itemMiscTempletByID.IsUsable() && !itemMiscTempletByID2.IsUsable())
				{
					return -1;
				}
				return y.ItemID.CompareTo(x.ItemID);
			}
		}

		// Token: 0x02001687 RID: 5767
		public class CompAscending : IComparer<NKMItemMiscData>
		{
			// Token: 0x0600B08F RID: 45199 RVA: 0x0035F4D8 File Offset: 0x0035D6D8
			public int Compare(NKMItemMiscData x, NKMItemMiscData y)
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(x.ItemID);
				NKMItemMiscTemplet itemMiscTempletByID2 = NKMItemManager.GetItemMiscTempletByID(y.ItemID);
				if (itemMiscTempletByID == null)
				{
					return 1;
				}
				if (itemMiscTempletByID2 == null)
				{
					return -1;
				}
				if (!itemMiscTempletByID.WillBeDeletedSoon() && itemMiscTempletByID2.WillBeDeletedSoon())
				{
					return 1;
				}
				if (itemMiscTempletByID.WillBeDeletedSoon() && !itemMiscTempletByID2.WillBeDeletedSoon())
				{
					return -1;
				}
				if (!itemMiscTempletByID.IsUsable() && itemMiscTempletByID2.IsUsable())
				{
					return 1;
				}
				if (itemMiscTempletByID.IsUsable() && !itemMiscTempletByID2.IsUsable())
				{
					return -1;
				}
				return x.ItemID.CompareTo(y.ItemID);
			}
		}
	}
}
