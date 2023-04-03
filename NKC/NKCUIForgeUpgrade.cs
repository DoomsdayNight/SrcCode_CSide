using System;
using System.Collections;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200099B RID: 2459
	public class NKCUIForgeUpgrade : NKCUIBase
	{
		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x06006635 RID: 26165 RVA: 0x0020A21C File Offset: 0x0020841C
		public static NKCUIForgeUpgrade Instance
		{
			get
			{
				if (NKCUIForgeUpgrade.m_Instance == null)
				{
					NKCUIForgeUpgrade.m_Instance = NKCUIManager.OpenNewInstance<NKCUIForgeUpgrade>("AB_UI_NKM_UI_FACTORY", "NKM_UI_FACTORY_UPGRADE", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIForgeUpgrade.CleanupInstance)).GetInstance<NKCUIForgeUpgrade>();
					NKCUIForgeUpgrade.m_Instance.InitUI();
				}
				return NKCUIForgeUpgrade.m_Instance;
			}
		}

		// Token: 0x06006636 RID: 26166 RVA: 0x0020A26B File Offset: 0x0020846B
		private static void CleanupInstance()
		{
			NKCUIForgeUpgrade.m_Instance = null;
		}

		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x06006637 RID: 26167 RVA: 0x0020A273 File Offset: 0x00208473
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIForgeUpgrade.m_Instance != null && NKCUIForgeUpgrade.m_Instance.IsOpen;
			}
		}

		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x06006638 RID: 26168 RVA: 0x0020A28E File Offset: 0x0020848E
		public static bool IsInstanceLoaded
		{
			get
			{
				return NKCUIForgeUpgrade.m_Instance != null;
			}
		}

		// Token: 0x06006639 RID: 26169 RVA: 0x0020A29B File Offset: 0x0020849B
		public static void CheckInstanceAndClose()
		{
			if (NKCUIForgeUpgrade.m_Instance != null && NKCUIForgeUpgrade.m_Instance.IsOpen)
			{
				NKCUIForgeUpgrade.m_Instance.Close();
			}
		}

		// Token: 0x0600663A RID: 26170 RVA: 0x0020A2C0 File Offset: 0x002084C0
		private void OnDestroy()
		{
			NKCUIForgeUpgrade.m_Instance = null;
		}

		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x0600663B RID: 26171 RVA: 0x0020A2C8 File Offset: 0x002084C8
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x0600663C RID: 26172 RVA: 0x0020A2CB File Offset: 0x002084CB
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x0600663D RID: 26173 RVA: 0x0020A2CE File Offset: 0x002084CE
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_FACTORY_UPGRADE_TITLE;
			}
		}

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x0600663E RID: 26174 RVA: 0x0020A2D5 File Offset: 0x002084D5
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_EQUIP_UPGRADE";
			}
		}

		// Token: 0x0600663F RID: 26175 RVA: 0x0020A2DC File Offset: 0x002084DC
		public void InitUI()
		{
			this.m_btnFilter.PointerClick.RemoveAllListeners();
			this.m_btnFilter.PointerClick.AddListener(new UnityAction(this.OnClickFilterBtn));
			this.m_loop.dOnGetObject += this.GetObject;
			this.m_loop.dOnReturnObject += this.ReturnObject;
			this.m_loop.dOnProvideData += this.ProvideData;
			NKCUtil.SetScrollHotKey(this.m_loop, null);
			if (this.m_bSetAutoResize)
			{
				this.m_loop.SetAutoResize(2, false);
			}
			this.m_loop.PrepareCells(0);
			this.m_btnUpgrade.PointerClick.RemoveAllListeners();
			this.m_btnUpgrade.PointerClick.AddListener(new UnityAction(this.OnClickUpgrade));
			this.m_btnUpgrade.m_bGetCallbackWhileLocked = true;
			for (int i = 0; i < this.m_lstStatSlot.Count; i++)
			{
				if (this.m_lstStatSlot[i] != null)
				{
					this.m_lstStatSlot[i].InitUI();
				}
			}
		}

		// Token: 0x06006640 RID: 26176 RVA: 0x0020A3FA File Offset: 0x002085FA
		public override void OnBackButton()
		{
			if (this.m_bWaitingPacket || this.bIsPlayingAni)
			{
				return;
			}
			base.OnBackButton();
		}

		// Token: 0x06006641 RID: 26177 RVA: 0x0020A413 File Offset: 0x00208613
		public override void UnHide()
		{
			base.UnHide();
			this.RefreshUI();
			NKCUtil.SetGameobjectActive(this.m_objNoTouchPanel, false);
		}

		// Token: 0x06006642 RID: 26178 RVA: 0x0020A430 File Offset: 0x00208630
		public override void CloseInternal()
		{
			this.bIsPlayingAni = false;
			this.m_LatestSelectedSlot = null;
			this.m_LatestSelectedTemplet = null;
			this.m_sourceEquipItemData = null;
			this.m_UpgradedEquipData = null;
			NKCUtil.SetGameobjectActive(this.m_objNoTouchPanel, false);
			base.StopAllCoroutines();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06006643 RID: 26179 RVA: 0x0020A480 File Offset: 0x00208680
		public RectTransform GetObject(int idx)
		{
			NKCUIForgeUpgradeSlot nkcuiforgeUpgradeSlot;
			if (this.m_stkSlot.Count > 0)
			{
				nkcuiforgeUpgradeSlot = this.m_stkSlot.Pop();
				nkcuiforgeUpgradeSlot.transform.SetParent(this.m_trSlotParent);
			}
			else
			{
				nkcuiforgeUpgradeSlot = UnityEngine.Object.Instantiate<NKCUIForgeUpgradeSlot>(this.m_pfbUpgradeSlot, this.m_trSlotParent);
			}
			NKCUtil.SetGameobjectActive(nkcuiforgeUpgradeSlot, false);
			return nkcuiforgeUpgradeSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06006644 RID: 26180 RVA: 0x0020A4DC File Offset: 0x002086DC
		public void ReturnObject(Transform tr)
		{
			NKCUIForgeUpgradeSlot component = tr.GetComponent<NKCUIForgeUpgradeSlot>();
			component.transform.SetParent(this.m_trObjectPool);
			NKCUtil.SetGameobjectActive(component, false);
			this.m_stkSlot.Push(component);
		}

		// Token: 0x06006645 RID: 26181 RVA: 0x0020A514 File Offset: 0x00208714
		public void ProvideData(Transform tr, int idx)
		{
			if (this.m_ssActive == null || idx >= this.m_ssActive.SortedEquipList.Count)
			{
				return;
			}
			NKCUIForgeUpgradeSlot component = tr.GetComponent<NKCUIForgeUpgradeSlot>();
			NKCUtil.SetGameobjectActive(component, true);
			NKMItemEquipUpgradeTemplet upgradeTemplet = NKMTempletContainer<NKMItemEquipUpgradeTemplet>.Find((NKMItemEquipUpgradeTemplet x) => x.UpgradeEquipTemplet.m_ItemEquipID == this.m_ssActive.SortedEquipList[idx].m_ItemEquipID);
			component.SetData(upgradeTemplet, new NKCUIForgeUpgradeSlot.OnClickUpgradeSlot(this.OnClickUpgradeSlot));
			bool flag = this.m_LatestSelectedTemplet != null && this.m_LatestSelectedTemplet.UpgradeEquipTemplet.m_ItemEquipID == this.m_ssActive.SortedEquipList[idx].m_ItemEquipID;
			component.SetSelected(flag);
			if (flag)
			{
				this.OnClickUpgradeSlot(component, component.GetUpgradeState());
			}
		}

		// Token: 0x06006646 RID: 26182 RVA: 0x0020A5DC File Offset: 0x002087DC
		public void Open()
		{
			this.bIsPlayingAni = false;
			this.m_LatestFilterOptionSet = new HashSet<NKCEquipSortSystem.eFilterOption>();
			this.m_LatestSelectedSlot = null;
			this.m_LatestSelectedTemplet = null;
			this.m_sourceEquipItemData = null;
			this.m_bWaitingPacket = false;
			NKCUtil.SetGameobjectActive(this.m_objNoTouchPanel, false);
			NKCUtil.SetGameobjectActive(this.m_objFilterSelected, false);
			this.SetUpgardeTempletData();
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_loop.TotalCount = this.m_ssActive.SortedEquipList.Count;
			this.m_loop.SetIndexPosition(0);
			this.SetRightSide(null, NKC_EQUIP_UPGRADE_STATE.NOT_HAVE);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			base.UIOpened(true);
			this.TutorialCheck();
		}

		// Token: 0x06006647 RID: 26183 RVA: 0x0020A68C File Offset: 0x0020888C
		private void RefreshUI()
		{
			this.m_LatestSelectedSlot = null;
			this.m_sourceEquipItemData = null;
			this.SetUpgardeTempletData();
			NKCUtil.SetGameobjectActive(this.m_objFilterSelected, this.m_ssActive.FilterSet.Count > 0);
			this.SetRightSide(null, NKC_EQUIP_UPGRADE_STATE.NOT_HAVE);
			this.m_loop.TotalCount = this.m_ssActive.SortedEquipList.Count;
			int indexPosition = 0;
			if (this.m_LatestSelectedTemplet != null)
			{
				indexPosition = this.m_ssActive.SortedEquipList.FindIndex((NKMEquipItemData x) => x.m_ItemEquipID == this.m_LatestSelectedTemplet.UpgradeEquipTemplet.m_ItemEquipID);
			}
			this.m_loop.SetIndexPosition(indexPosition);
		}

		// Token: 0x06006648 RID: 26184 RVA: 0x0020A724 File Offset: 0x00208924
		private void SetUpgardeTempletData()
		{
			List<NKMEquipItemData> list = new List<NKMEquipItemData>();
			foreach (NKMItemEquipUpgradeTemplet nkmitemEquipUpgradeTemplet in NKMTempletContainer<NKMItemEquipUpgradeTemplet>.Values)
			{
				if (nkmitemEquipUpgradeTemplet.EnableByTag)
				{
					NKMEquipItemData item = NKCEquipSortSystem.MakeTempEquipData(nkmitemEquipUpgradeTemplet.UpgradeEquipTemplet.m_ItemEquipID, 0, false);
					list.Add(item);
				}
			}
			NKCEquipSortSystem.EquipListOptions equipListOptions = default(NKCEquipSortSystem.EquipListOptions);
			equipListOptions.setOnlyIncludeEquipID = new HashSet<int>();
			equipListOptions.lstSortOption = NKCEquipSortSystem.EQUIP_UPGRADE_SORT_LIST;
			equipListOptions.lstCustomSortFunc = new Dictionary<NKCEquipSortSystem.eSortCategory, KeyValuePair<string, NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc>>();
			equipListOptions.lstCustomSortFunc.Add(NKCEquipSortSystem.eSortCategory.Custom1, new KeyValuePair<string, NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc>("SI_PF_FILTER_EQUIP_UPGRADE", new NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc(this.CompByState)));
			this.m_ssActive = new NKCEquipSortSystem(NKCScenManager.CurrentUserData(), equipListOptions, list);
			if (this.m_LatestFilterOptionSet.Count > 0)
			{
				this.m_ssActive.FilterSet = this.m_LatestFilterOptionSet;
				return;
			}
			this.m_ssActive.FilterSet = new HashSet<NKCEquipSortSystem.eFilterOption>();
		}

		// Token: 0x06006649 RID: 26185 RVA: 0x0020A824 File Offset: 0x00208A24
		private int CompByState(NKMEquipItemData lItem, NKMEquipItemData rItem)
		{
			NKMItemEquipUpgradeTemplet nkmitemEquipUpgradeTemplet = NKMTempletContainer<NKMItemEquipUpgradeTemplet>.Find((NKMItemEquipUpgradeTemplet x) => x.UpgradeEquipTemplet.m_ItemEquipID == lItem.m_ItemEquipID);
			NKMItemEquipUpgradeTemplet nkmitemEquipUpgradeTemplet2 = NKMTempletContainer<NKMItemEquipUpgradeTemplet>.Find((NKMItemEquipUpgradeTemplet x) => x.UpgradeEquipTemplet.m_ItemEquipID == rItem.m_ItemEquipID);
			List<NKMEquipItemData> list = new List<NKMEquipItemData>();
			NKC_EQUIP_UPGRADE_STATE setUpgradeSlotState = NKMItemManager.GetSetUpgradeSlotState(nkmitemEquipUpgradeTemplet, ref list);
			NKC_EQUIP_UPGRADE_STATE setUpgradeSlotState2 = NKMItemManager.GetSetUpgradeSlotState(nkmitemEquipUpgradeTemplet2, ref list);
			if (setUpgradeSlotState == setUpgradeSlotState2)
			{
				return nkmitemEquipUpgradeTemplet.IDX.CompareTo(nkmitemEquipUpgradeTemplet2.IDX);
			}
			return setUpgradeSlotState.CompareTo(setUpgradeSlotState2);
		}

		// Token: 0x0600664A RID: 26186 RVA: 0x0020A8AF File Offset: 0x00208AAF
		public void OnClickFilterBtn()
		{
			NKCPopupFilterEquip.Instance.Open(NKCEquipSortSystem.m_hsEquipUpgradeFilterSet, this.m_ssActive, new NKCPopupFilterEquip.OnEquipFilterSetChange(this.OnSelectFilter), false);
		}

		// Token: 0x0600664B RID: 26187 RVA: 0x0020A8D3 File Offset: 0x00208AD3
		public void OnSelectFilter(NKCEquipSortSystem ssActive)
		{
			if (ssActive != null)
			{
				this.m_ssActive = ssActive;
			}
			this.FilterList(this.m_ssActive.FilterSet, false);
		}

		// Token: 0x0600664C RID: 26188 RVA: 0x0020A8F4 File Offset: 0x00208AF4
		private void FilterList(HashSet<NKCEquipSortSystem.eFilterOption> setFilterOption, bool bForce = false)
		{
			NKCUtil.SetGameobjectActive(this.m_objFilterSelected, setFilterOption.Count > 0);
			this.m_LatestFilterOptionSet = setFilterOption;
			this.m_ssActive.FilterSet = setFilterOption;
			this.m_LatestSelectedTemplet = null;
			this.m_LatestSelectedSlot = null;
			this.SetRightSide(null, NKC_EQUIP_UPGRADE_STATE.NONE);
			this.m_loop.TotalCount = this.m_ssActive.SortedEquipList.Count;
			this.m_loop.SetIndexPosition(0);
		}

		// Token: 0x0600664D RID: 26189 RVA: 0x0020A968 File Offset: 0x00208B68
		public void OnClickUpgradeSlot(NKCUIForgeUpgradeSlot cSlot, NKC_EQUIP_UPGRADE_STATE state)
		{
			if (this.m_LatestSelectedSlot != null)
			{
				this.m_LatestSelectedSlot.SetSelected(false);
			}
			this.m_LatestSelectedSlot = cSlot;
			this.m_LatestSelectedSlot.SetSelected(true);
			this.m_LatestSelectedTemplet = cSlot.GetUpgradeTemplet();
			this.m_sourceEquipItemData = null;
			this.SetRightSide(this.m_LatestSelectedSlot.GetUpgradeTemplet(), this.m_LatestSelectedSlot.GetUpgradeState());
		}

		// Token: 0x0600664E RID: 26190 RVA: 0x0020A9D4 File Offset: 0x00208BD4
		private void SetRightSide(NKMItemEquipUpgradeTemplet upgradeTemplet, NKC_EQUIP_UPGRADE_STATE state)
		{
			NKCUtil.SetGameobjectActive(this.m_objEmpty, upgradeTemplet == null);
			NKCUtil.SetGameobjectActive(this.m_objUpgradeInfo, upgradeTemplet != null);
			if (upgradeTemplet == null)
			{
				return;
			}
			Animator ani = this.m_ani;
			if (ani != null)
			{
				ani.Play("NKM_UI_FACTORY_UPGRADE_BASE");
			}
			NKCUtil.SetLabelText(this.m_lbEquipName, this.m_LatestSelectedTemplet.UpgradeEquipTemplet.GetItemName());
			NKCUtil.SetGameobjectActive(this.m_objEquipNeedSelect, this.m_sourceEquipItemData == null && state != NKC_EQUIP_UPGRADE_STATE.NOT_HAVE);
			NKCUtil.SetGameobjectActive(this.m_objEquipNotHave, state == NKC_EQUIP_UPGRADE_STATE.NOT_HAVE);
			NKMEquipItemData nkmequipItemData = NKCEquipSortSystem.MakeTempEquipData(this.m_LatestSelectedTemplet.CoreEquipTemplet.m_ItemEquipID, 0, true);
			if (this.m_sourceEquipItemData != null)
			{
				nkmequipItemData = this.m_sourceEquipItemData;
			}
			if (state == NKC_EQUIP_UPGRADE_STATE.NOT_HAVE)
			{
				this.m_slotSourceEquip.SetData(nkmequipItemData, null, false, false, false, false);
			}
			else if (this.m_sourceEquipItemData == null)
			{
				this.m_slotSourceEquip.SetEmptyMaterial(delegate(NKCUISlotEquip _slot, NKMEquipItemData _data)
				{
					this.OnSelectTargetEquipSlot();
				});
			}
			else
			{
				this.m_slotSourceEquip.SetData(this.m_sourceEquipItemData, delegate(NKCUISlotEquip _slot, NKMEquipItemData _data)
				{
					this.OnSelectTargetEquipSlot();
				}, false, false, false, false);
			}
			NKMEquipItemData nkmequipItemData2 = NKCEquipSortSystem.MakeTempEquipData(this.m_LatestSelectedTemplet.UpgradeEquipTemplet.m_ItemEquipID, 0, false);
			nkmequipItemData2.m_Precision = 100;
			nkmequipItemData2.m_Precision2 = 100;
			nkmequipItemData2.m_SetOptionId = nkmequipItemData.m_SetOptionId;
			nkmequipItemData2.m_EnchantExp = NKMItemManager.GetMaxEquipEnchantExp(nkmequipItemData.m_ItemEquipID);
			nkmequipItemData2.m_EnchantLevel = this.GetEnchantLevel(nkmequipItemData2.m_ItemEquipID, NKMItemManager.GetMaxEquipEnchantExp(nkmequipItemData.m_ItemEquipID));
			nkmequipItemData2.m_Stat[0].stat_value = nkmequipItemData2.m_Stat[0].stat_value + (float)nkmequipItemData2.m_EnchantLevel * nkmequipItemData2.m_Stat[0].stat_level_value;
			this.m_slotTargetEquip.SetData(nkmequipItemData2, null, false, false, false, false);
			for (int i = 0; i < this.m_lstStatSlot.Count; i++)
			{
				this.m_lstStatSlot[i].SetData(i, nkmequipItemData, nkmequipItemData2);
			}
			bool flag = true;
			for (int j = 0; j < this.m_lstCostSlot.Count; j++)
			{
				if (j < upgradeTemplet.MiscMaterials.Count)
				{
					NKCUtil.SetGameobjectActive(this.m_lstCostSlot[j], true);
					int count = upgradeTemplet.MiscMaterials[j].Count32;
					long countMiscItem = NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(upgradeTemplet.MiscMaterials[j].ItemId);
					this.m_lstCostSlot[j].SetData(upgradeTemplet.MiscMaterials[j].ItemId, count, countMiscItem, true, true, false);
					if ((long)count > countMiscItem)
					{
						flag = false;
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstCostSlot[j], false);
				}
			}
			if (flag && NKMItemManager.CanUpgradeEquipByCoreID(this.m_sourceEquipItemData) == NKC_EQUIP_UPGRADE_STATE.UPGRADABLE)
			{
				this.m_btnUpgrade.UnLock(false);
				NKCUtil.SetLabelTextColor(this.m_lbUpgrade, NKCUtil.GetColor("#582817"));
				NKCUtil.SetImageColor(this.m_imgUpgrade, NKCUtil.GetColor("#582817"));
				return;
			}
			this.m_btnUpgrade.Lock(false);
			NKCUtil.SetLabelTextColor(this.m_lbUpgrade, NKCUtil.GetColor("#222222"));
			NKCUtil.SetImageColor(this.m_imgUpgrade, NKCUtil.GetColor("#222222"));
		}

		// Token: 0x0600664F RID: 26191 RVA: 0x0020AD00 File Offset: 0x00208F00
		private int GetEnchantLevel(int equipID, int enchantExp)
		{
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(equipID);
			if (equipTemplet == null)
			{
				return 0;
			}
			int num = 0;
			int enchantRequireExp = NKMItemManager.GetEnchantRequireExp(equipTemplet.m_NKM_ITEM_TIER, 0, equipTemplet.m_NKM_ITEM_GRADE);
			int num2 = enchantExp;
			while (enchantRequireExp <= num2 && num < NKMItemManager.GetMaxEquipEnchantLevel(equipTemplet.m_NKM_ITEM_TIER))
			{
				num++;
				num2 -= enchantRequireExp;
				enchantRequireExp = NKMItemManager.GetEnchantRequireExp(equipTemplet.m_NKM_ITEM_TIER, num, equipTemplet.m_NKM_ITEM_GRADE);
			}
			return num;
		}

		// Token: 0x06006650 RID: 26192 RVA: 0x0020AD60 File Offset: 0x00208F60
		private void OnSelectTargetEquipSlot()
		{
			NKCUIInventory.EquipSelectListOptions invenOption = new NKCUIInventory.EquipSelectListOptions(NKC_INVENTORY_OPEN_TYPE.NIOT_EQUIP_SELECT, false, true);
			invenOption.bEnableLockEquipSystem = false;
			invenOption.bMultipleSelect = false;
			invenOption.m_ButtonMenuType = NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_OK;
			invenOption.m_dOnSelectedEquipSlot = new NKCUISlotEquip.OnSelectedEquipSlot(this.OnSelectedTargetEquip);
			invenOption.m_EquipListOptions.setOnlyIncludeEquipID = new HashSet<int>
			{
				this.m_LatestSelectedTemplet.CoreEquipTemplet.m_ItemEquipID
			};
			invenOption.bShowEquipUpgradeState = true;
			invenOption.lstSortOption = new List<NKCEquipSortSystem.eSortOption>();
			invenOption.lstSortOption.Add(NKCEquipSortSystem.eSortOption.CustomDescend1);
			NKCEquipSortSystem.DEFAULT_EQUIP_SORT_LIST.ForEach(delegate(NKCEquipSortSystem.eSortOption e)
			{
				invenOption.lstSortOption.Add(e);
			});
			string @string = NKCStringTable.GetString("SI_PF_FILTER_EQUIP_UPGRADE", false);
			invenOption.m_EquipListOptions.lstCustomSortFunc.Add(NKCEquipSortSystem.eSortCategory.Custom1, new KeyValuePair<string, NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc>(@string, new NKCUnitSortSystem.NKCDataComparerer<NKMEquipItemData>.CompareFunc(this.SortByEquipCanUpgrade)));
			NKCUIInventory.Instance.Open(invenOption, null, 0L, NKCUIInventory.NKC_INVENTORY_TAB.NIT_NONE);
		}

		// Token: 0x06006651 RID: 26193 RVA: 0x0020AE78 File Offset: 0x00209078
		private int SortByEquipCanUpgrade(NKMEquipItemData lItem, NKMEquipItemData rItem)
		{
			return NKMItemManager.CanUpgradeEquipByCoreID(lItem).CompareTo(NKMItemManager.CanUpgradeEquipByCoreID(rItem));
		}

		// Token: 0x06006652 RID: 26194 RVA: 0x0020AEA4 File Offset: 0x002090A4
		private void OnSelectedTargetEquip(NKCUISlotEquip slot, NKMEquipItemData equipData)
		{
			if (NKCUIInventory.IsInstanceOpen)
			{
				NKCUIInventory.Instance.Close();
			}
			this.m_sourceEquipItemData = equipData;
			this.SetRightSide(this.m_LatestSelectedSlot.GetUpgradeTemplet(), this.m_LatestSelectedSlot.GetUpgradeState());
		}

		// Token: 0x06006653 RID: 26195 RVA: 0x0020AEDA File Offset: 0x002090DA
		public void OnClickUpgrade()
		{
			if (this.m_btnUpgrade.m_bLock)
			{
				return;
			}
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_FACTORY_UPGRADE_CONFIRM_POPUP, new NKCPopupOKCancel.OnButton(this.ProcessUpgrade), null, false);
		}

		// Token: 0x06006654 RID: 26196 RVA: 0x0020AF07 File Offset: 0x00209107
		private void ProcessUpgrade()
		{
			NKCUtil.SetGameobjectActive(this.m_objNoTouchPanel, true);
			this.m_btnUpgrade.Lock(false);
			this.m_bWaitingPacket = true;
			NKCPacketSender.Send_NKMPacket_EQUIP_UPGRADE_REQ(this.m_sourceEquipItemData.m_ItemUid, new List<long>());
		}

		// Token: 0x06006655 RID: 26197 RVA: 0x0020AF40 File Offset: 0x00209140
		private void Update()
		{
			if (this.bIsPlayingAni)
			{
				this.m_fPlayTime += Time.deltaTime;
				if (NKCUtil.GetAnimationClip(this.m_ani, "NKM_UI_FACTORY_UPGRADE_START").length < this.m_fPlayTime)
				{
					this.m_fPlayTime = 0f;
					this.bIsPlayingAni = false;
					this.m_btnUpgrade.UnLock(false);
				}
			}
		}

		// Token: 0x06006656 RID: 26198 RVA: 0x0020AFA2 File Offset: 0x002091A2
		public void UpgradeFinished(NKMEquipItemData equipData)
		{
			this.m_bWaitingPacket = false;
			this.m_UpgradedEquipData = equipData;
			base.StartCoroutine(this.Process(equipData));
		}

		// Token: 0x06006657 RID: 26199 RVA: 0x0020AFC0 File Offset: 0x002091C0
		private IEnumerator Process(NKMEquipItemData equipData)
		{
			this.bIsPlayingAni = true;
			this.m_fPlayTime = 0f;
			Animator ani = this.m_ani;
			if (ani != null)
			{
				ani.Play("NKM_UI_FACTORY_UPGRADE_START");
			}
			while (this.bIsPlayingAni)
			{
				yield return null;
			}
			this.ShowResultPopup();
			yield break;
		}

		// Token: 0x06006658 RID: 26200 RVA: 0x0020AFCF File Offset: 0x002091CF
		private void ShowResultPopup()
		{
			base.StopAllCoroutines();
			this.RefreshUI();
			NKCPopupItemEquipBox.OpenForConfirm(this.m_UpgradedEquipData, null, false, false, delegate
			{
				NKCUtil.SetGameobjectActive(this.m_objNoTouchPanel, false);
			});
			NKCPopupItemEquipBox.ShowTitle(NKCUtilString.GET_STRING_FACTORY_UPGRADE_COMPLETE);
			NKCPopupItemEquipBox.ShowUpgradeCompleteEffect();
			this.m_UpgradedEquipData = null;
		}

		// Token: 0x06006659 RID: 26201 RVA: 0x0020B00D File Offset: 0x0020920D
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			if (!base.gameObject.activeSelf)
			{
				return;
			}
			base.OnInventoryChange(itemData);
			if (this.m_bWaitingPacket)
			{
				return;
			}
			this.RefreshUI();
		}

		// Token: 0x0600665A RID: 26202 RVA: 0x0020B033 File Offset: 0x00209233
		public override void OnEquipChange(NKMUserData.eChangeNotifyType eType, long equipUID, NKMEquipItemData equipItem)
		{
			if (!base.gameObject.activeSelf)
			{
				return;
			}
			base.OnEquipChange(eType, equipUID, equipItem);
			if (this.m_bWaitingPacket)
			{
				return;
			}
			this.RefreshUI();
		}

		// Token: 0x0600665B RID: 26203 RVA: 0x0020B05B File Offset: 0x0020925B
		private void TutorialCheck()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.FactoryUpgrade, true);
		}

		// Token: 0x040051ED RID: 20973
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_FACTORY";

		// Token: 0x040051EE RID: 20974
		private const string UI_ASSET_NAME = "NKM_UI_FACTORY_UPGRADE";

		// Token: 0x040051EF RID: 20975
		private static NKCUIForgeUpgrade m_Instance;

		// Token: 0x040051F0 RID: 20976
		private const string DEFAULT_TEXT_COLOR = "#582817";

		// Token: 0x040051F1 RID: 20977
		private const string LOCKED_TEXT_COLOR = "#222222";

		// Token: 0x040051F2 RID: 20978
		[Header("좌측 UI")]
		public NKCUIForgeUpgradeSlot m_pfbUpgradeSlot;

		// Token: 0x040051F3 RID: 20979
		public LoopScrollRect m_loop;

		// Token: 0x040051F4 RID: 20980
		public Transform m_trSlotParent;

		// Token: 0x040051F5 RID: 20981
		public Transform m_trObjectPool;

		// Token: 0x040051F6 RID: 20982
		public NKCUIComStateButton m_btnFilter;

		// Token: 0x040051F7 RID: 20983
		public GameObject m_objFilterSelected;

		// Token: 0x040051F8 RID: 20984
		public bool m_bSetAutoResize;

		// Token: 0x040051F9 RID: 20985
		[Header("오른쪽 UI")]
		public GameObject m_objUpgradeInfo;

		// Token: 0x040051FA RID: 20986
		public GameObject m_objEmpty;

		// Token: 0x040051FB RID: 20987
		public Animator m_ani;

		// Token: 0x040051FC RID: 20988
		public Text m_lbEquipName;

		// Token: 0x040051FD RID: 20989
		public GameObject m_objEquipNeedSelect;

		// Token: 0x040051FE RID: 20990
		public GameObject m_objEquipNotHave;

		// Token: 0x040051FF RID: 20991
		public NKCUISlotEquip m_slotSourceEquip;

		// Token: 0x04005200 RID: 20992
		public NKCUISlotEquip m_slotTargetEquip;

		// Token: 0x04005201 RID: 20993
		public List<NKCUIForgeUpgradeStatSlot> m_lstStatSlot = new List<NKCUIForgeUpgradeStatSlot>();

		// Token: 0x04005202 RID: 20994
		public List<NKCUIItemCostSlot> m_lstCostSlot = new List<NKCUIItemCostSlot>();

		// Token: 0x04005203 RID: 20995
		public NKCUIComStateButton m_btnUpgrade;

		// Token: 0x04005204 RID: 20996
		public Image m_imgUpgrade;

		// Token: 0x04005205 RID: 20997
		public Text m_lbUpgrade;

		// Token: 0x04005206 RID: 20998
		[Header("터치")]
		public GameObject m_objNoTouchPanel;

		// Token: 0x04005207 RID: 20999
		private Stack<NKCUIForgeUpgradeSlot> m_stkSlot = new Stack<NKCUIForgeUpgradeSlot>();

		// Token: 0x04005208 RID: 21000
		private NKCEquipSortSystem m_ssActive;

		// Token: 0x04005209 RID: 21001
		private HashSet<NKCEquipSortSystem.eFilterOption> m_LatestFilterOptionSet = new HashSet<NKCEquipSortSystem.eFilterOption>();

		// Token: 0x0400520A RID: 21002
		private NKCUIForgeUpgradeSlot m_LatestSelectedSlot;

		// Token: 0x0400520B RID: 21003
		private NKMItemEquipUpgradeTemplet m_LatestSelectedTemplet;

		// Token: 0x0400520C RID: 21004
		private NKMEquipItemData m_sourceEquipItemData;

		// Token: 0x0400520D RID: 21005
		private NKMEquipItemData m_UpgradedEquipData;

		// Token: 0x0400520E RID: 21006
		private bool m_bWaitingPacket;

		// Token: 0x0400520F RID: 21007
		private bool bIsPlayingAni;

		// Token: 0x04005210 RID: 21008
		private float m_fPlayTime;
	}
}
