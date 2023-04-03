using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009D6 RID: 2518
	public class NKCUISelectionEquip : NKCUIBase
	{
		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x06006BE1 RID: 27617 RVA: 0x00233944 File Offset: 0x00231B44
		public static NKCUISelectionEquip Instance
		{
			get
			{
				if (NKCUISelectionEquip.m_Instance == null)
				{
					NKCUISelectionEquip.m_Instance = NKCUIManager.OpenNewInstance<NKCUISelectionEquip>("AB_UI_NKM_UI_UNIT_SELECTION", "NKM_UI_EQUIP_SELECTION", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUISelectionEquip.CleanupInstance)).GetInstance<NKCUISelectionEquip>();
					NKCUISelectionEquip.m_Instance.InitUI();
				}
				return NKCUISelectionEquip.m_Instance;
			}
		}

		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x06006BE2 RID: 27618 RVA: 0x00233993 File Offset: 0x00231B93
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUISelectionEquip.m_Instance != null && NKCUISelectionEquip.m_Instance.IsOpen;
			}
		}

		// Token: 0x06006BE3 RID: 27619 RVA: 0x002339AE File Offset: 0x00231BAE
		public static void CheckInstanceAndClose()
		{
			if (NKCUISelectionEquip.m_Instance != null && NKCUISelectionEquip.m_Instance.IsOpen)
			{
				NKCUISelectionEquip.m_Instance.Close();
			}
		}

		// Token: 0x06006BE4 RID: 27620 RVA: 0x002339D3 File Offset: 0x00231BD3
		private static void CleanupInstance()
		{
			NKCUISelectionEquip.m_Instance = null;
		}

		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x06006BE5 RID: 27621 RVA: 0x002339DB File Offset: 0x00231BDB
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x06006BE6 RID: 27622 RVA: 0x002339DE File Offset: 0x00231BDE
		public override string MenuName
		{
			get
			{
				if (this.m_NKM_ITEM_MISC_TYPE == NKM_ITEM_MISC_TYPE.IMT_CHOICE_EQUIP)
				{
					return NKCUtilString.GET_STRING_CHOICE_EQUIP;
				}
				return NKCUtilString.GET_STRING_USE_CHOICE;
			}
		}

		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x06006BE7 RID: 27623 RVA: 0x002339F5 File Offset: 0x00231BF5
		private bool CanChoiceSetOption
		{
			get
			{
				return this.m_NKMItemMiscTemplet != null && this.m_NKMItemMiscTemplet.m_ItemMiscSubType == NKM_ITEM_MISC_SUBTYPE.IMST_EQUIP_CHOICE_SET_OPTION;
			}
		}

		// Token: 0x06006BE8 RID: 27624 RVA: 0x00233A10 File Offset: 0x00231C10
		private void InitUI()
		{
			this.m_loopScrollRectEquip.dOnGetObject += this.GetObject;
			this.m_loopScrollRectEquip.dOnReturnObject += this.ReturnObject;
			this.m_loopScrollRectEquip.dOnProvideData += this.ProvideData;
			this.m_loopScrollRectEquip.dOnRepopulate += this.CalculateContentRectSize;
			NKCUtil.SetScrollHotKey(this.m_loopScrollRectEquip, null);
			this.m_tglDescending.OnValueChanged.RemoveAllListeners();
			this.m_tglDescending.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangeDescending));
			this.m_tglFilter.OnValueChanged.RemoveAllListeners();
			this.m_tglFilter.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangeFilter));
			this.m_tglSorting.OnValueChanged.RemoveAllListeners();
			this.m_tglSorting.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangeSorting));
			this.m_btnOK.PointerClick.RemoveAllListeners();
			this.m_btnOK.PointerClick.AddListener(new UnityAction(this.OnClickOk));
		}

		// Token: 0x06006BE9 RID: 27625 RVA: 0x00233B38 File Offset: 0x00231D38
		public override void CloseInternal()
		{
			this.m_lstRewardId = new List<int>();
			this.m_ssEquip = null;
			this.m_tglDescending.Select(true, true, false);
			this.m_tglFilter.Select(false, true, false);
			this.m_tglSorting.Select(false, true, false);
			NKCUtil.SetGameobjectActive(this.m_objFilterSelected, false);
			NKCUtil.SetGameobjectActive(this.m_objSortSelect, false);
			NKCUtil.SetGameobjectActive(this.m_EquipSort, false);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06006BEA RID: 27626 RVA: 0x00233BB4 File Offset: 0x00231DB4
		public override void OnCloseInstance()
		{
			this.m_ssEquip = null;
		}

		// Token: 0x06006BEB RID: 27627 RVA: 0x00233BC0 File Offset: 0x00231DC0
		public void Open(NKMItemMiscTemplet itemMiscTemplet)
		{
			if (itemMiscTemplet == null)
			{
				return;
			}
			this.m_NKMItemMiscTemplet = itemMiscTemplet;
			this.m_LatestSelectedSlot = null;
			NKCUtil.SetGameobjectActive(this.m_imgBannerEquip, true);
			NKCUtil.SetGameobjectActive(this.m_objEquipInfo, false);
			List<NKMRandomBoxItemTemplet> randomBoxItemTempletList = NKCRandomBoxManager.GetRandomBoxItemTempletList(this.m_NKMItemMiscTemplet.m_RewardGroupID);
			if (randomBoxItemTempletList == null)
			{
				return;
			}
			for (int i = 0; i < randomBoxItemTempletList.Count; i++)
			{
				this.m_lstRewardId.Add(randomBoxItemTempletList[i].m_RewardID);
			}
			this.m_NKM_ITEM_MISC_TYPE = this.m_NKMItemMiscTemplet.m_ItemMiscType;
			NKCUtil.SetGameobjectActive(this.m_objEquipChoice, this.m_NKM_ITEM_MISC_TYPE == NKM_ITEM_MISC_TYPE.IMT_CHOICE_EQUIP);
			NKCUtil.SetGameobjectActive(this.m_tglDescending, this.m_NKM_ITEM_MISC_TYPE != NKM_ITEM_MISC_TYPE.IMT_CHOICE_MISC);
			NKCUtil.SetGameobjectActive(this.m_tglFilter, this.m_NKM_ITEM_MISC_TYPE != NKM_ITEM_MISC_TYPE.IMT_CHOICE_MISC);
			NKCUtil.SetGameobjectActive(this.m_tglSorting, this.m_NKM_ITEM_MISC_TYPE != NKM_ITEM_MISC_TYPE.IMT_CHOICE_MISC);
			NKCScenManager.CurrentUserData();
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.CalculateContentRectSize();
			this.SetEquipChoiceList();
			NKCUtil.SetLabelText(this.m_lbSortDefault, NKCEquipSortSystem.GetSortName(this.m_ssEquip.lstSortOption[0]));
			NKCUtil.SetLabelText(this.m_lbSortSelected, NKCEquipSortSystem.GetSortName(this.m_ssEquip.lstSortOption[0]));
			NKCUtil.SetImageSprite(this.m_imgBannerEquip, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_UNIT_SELECTION_TEXTURE", itemMiscTemplet.m_BannerImage, false), false);
			base.UIOpened(true);
		}

		// Token: 0x06006BEC RID: 27628 RVA: 0x00233D28 File Offset: 0x00231F28
		private void CalculateContentRectSize()
		{
			NKCUIComSafeArea safeArea = this.m_SafeArea;
			if (safeArea != null)
			{
				safeArea.SetSafeAreaBase();
			}
			int minColumn = 4;
			Vector2 cellSize = this.m_trContentParentEquip.GetComponent<GridLayoutGroup>().cellSize;
			Vector2 spacing = this.m_trContentParentEquip.GetComponent<GridLayoutGroup>().spacing;
			NKCUtil.CalculateContentRectSize(this.m_loopScrollRectEquip, this.m_trContentParentEquip.GetComponent<GridLayoutGroup>(), minColumn, cellSize, spacing, false);
		}

		// Token: 0x06006BED RID: 27629 RVA: 0x00233D85 File Offset: 0x00231F85
		private int CompOrderList(NKMRandomBoxItemTemplet lItem, NKMRandomBoxItemTemplet rItem)
		{
			if (lItem.m_OrderList == rItem.m_OrderList)
			{
				return lItem.m_RewardID.CompareTo(rItem.m_RewardID);
			}
			return lItem.m_OrderList.CompareTo(rItem.m_OrderList);
		}

		// Token: 0x06006BEE RID: 27630 RVA: 0x00233DB8 File Offset: 0x00231FB8
		private RectTransform GetObject(int index)
		{
			NKCUISlotEquip nkcuislotEquip;
			if (this.m_stkEquipSlotPool.Count > 0)
			{
				nkcuislotEquip = this.m_stkEquipSlotPool.Pop();
			}
			else
			{
				nkcuislotEquip = UnityEngine.Object.Instantiate<NKCUISlotEquip>(this.m_pfbEquipSlot);
			}
			NKCUtil.SetGameobjectActive(nkcuislotEquip, true);
			this.m_lstVisibleEquipSlot.Add(nkcuislotEquip);
			return nkcuislotEquip.GetComponent<RectTransform>();
		}

		// Token: 0x06006BEF RID: 27631 RVA: 0x00233E08 File Offset: 0x00232008
		private void ReturnObject(Transform go)
		{
			NKCUISlotEquip component = go.GetComponent<NKCUISlotEquip>();
			if (component != null)
			{
				NKCUtil.SetGameobjectActive(component, false);
				this.m_lstVisibleEquipSlot.Remove(component);
				this.m_stkEquipSlotPool.Push(component);
			}
		}

		// Token: 0x06006BF0 RID: 27632 RVA: 0x00233E48 File Offset: 0x00232048
		private void ProvideData(Transform tr, int idx)
		{
			if (this.m_ssEquip == null)
			{
				Debug.LogError("Slot Sort System Null!!");
				return;
			}
			NKCUISlotEquip component = tr.GetComponent<NKCUISlotEquip>();
			NKMEquipItemData nkmequipItemData = new NKMEquipItemData();
			if (this.m_ssEquip.SortedEquipList.Count <= idx)
			{
				return;
			}
			nkmequipItemData = this.m_ssEquip.SortedEquipList[idx];
			NKCUtil.SetGameobjectActive(component.gameObject, true);
			component.SetData(nkmequipItemData, new NKCUISlotEquip.OnSelectedEquipSlot(this.OnSelectEquipSlot), false, false, false, false);
			component.SetHaveCount((long)NKCScenManager.CurrentUserData().m_InventoryData.GetSameKindEquipCount(nkmequipItemData.m_ItemEquipID, nkmequipItemData.m_SetOptionId), false);
		}

		// Token: 0x06006BF1 RID: 27633 RVA: 0x00233EE4 File Offset: 0x002320E4
		private void SetEquipChoiceList()
		{
			NKCEquipSortSystem.EquipListOptions equipListOptions = default(NKCEquipSortSystem.EquipListOptions);
			equipListOptions.setOnlyIncludeEquipID = new HashSet<int>();
			equipListOptions.lstSortOption = NKCEquipSortSystem.GetDefaultSortOption(NKCPopupEquipSort.SORT_OPEN_TYPE.SELECTION);
			List<NKMEquipItemData> list = new List<NKMEquipItemData>();
			if (this.CanChoiceSetOption)
			{
				for (int i = 0; i < this.m_lstRewardId.Count; i++)
				{
					List<NKMEquipItemData> collection = NKCEquipSortSystem.MakeTempEquipDataWithAllSet(this.m_lstRewardId[i]);
					list.AddRange(collection);
					if (!equipListOptions.setOnlyIncludeEquipID.Contains(this.m_lstRewardId[i]))
					{
						equipListOptions.setOnlyIncludeEquipID.Add(this.m_lstRewardId[i]);
					}
				}
			}
			else
			{
				for (int j = 0; j < this.m_lstRewardId.Count; j++)
				{
					NKMEquipItemData item = NKCEquipSortSystem.MakeTempEquipData(this.m_lstRewardId[j], 0, false);
					list.Add(item);
					if (!equipListOptions.setOnlyIncludeEquipID.Contains(this.m_lstRewardId[j]))
					{
						equipListOptions.setOnlyIncludeEquipID.Add(this.m_lstRewardId[j]);
					}
				}
			}
			if (this.m_ssEquip == null)
			{
				this.m_ssEquip = new NKCEquipSortSystem(NKCScenManager.CurrentUserData(), equipListOptions, list);
			}
			else
			{
				this.m_ssEquip.BuildFilterAndSortedList(this.m_ssEquip.FilterSet, this.m_ssEquip.lstSortOption, false);
			}
			this.m_tglDescending.Select(NKCEquipSortSystem.GetDescendingBySorting(this.m_ssEquip.lstSortOption), true, false);
			this.m_loopScrollRectEquip.PrepareCells(0);
			this.m_loopScrollRectEquip.TotalCount = list.Count;
			this.m_loopScrollRectEquip.RefreshCells(true);
		}

		// Token: 0x06006BF2 RID: 27634 RVA: 0x00234074 File Offset: 0x00232274
		private void FilterList(bool bForce = false)
		{
			NKCUtil.SetGameobjectActive(this.m_objFilterSelected, this.m_ssEquip.FilterSet.Count > 0);
			this.m_ssEquip.BuildFilterAndSortedList(this.m_ssEquip.FilterSet, this.m_ssEquip.lstSortOption, false);
			this.ResetSlotUI();
		}

		// Token: 0x06006BF3 RID: 27635 RVA: 0x002340C7 File Offset: 0x002322C7
		public void ResetSlotUI()
		{
			this.m_loopScrollRectEquip.TotalCount = this.m_ssEquip.SortedEquipList.Count;
			this.m_loopScrollRectEquip.RefreshCells(false);
			this.m_loopScrollRectEquip.SetIndexPosition(0);
		}

		// Token: 0x06006BF4 RID: 27636 RVA: 0x002340FC File Offset: 0x002322FC
		public void OnSelectEquipFilter(NKCEquipSortSystem ssActive)
		{
			if (ssActive != null)
			{
				this.m_ssEquip = ssActive;
			}
			this.FilterList(false);
		}

		// Token: 0x06006BF5 RID: 27637 RVA: 0x0023410F File Offset: 0x0023230F
		private void OnEquipSort(List<NKCEquipSortSystem.eSortOption> sortOptionList)
		{
			NKCUtil.SetGameobjectActive(this.m_objSortSelect, sortOptionList[0] != NKCEquipSortSystem.GetDefaultSortOption(NKCPopupEquipSort.SORT_OPEN_TYPE.SELECTION)[0]);
			this.SetOpenSortingMenu(false, true);
			this.SortEquipList(sortOptionList, true);
		}

		// Token: 0x06006BF6 RID: 27638 RVA: 0x00234144 File Offset: 0x00232344
		private void SortEquipList(List<NKCEquipSortSystem.eSortOption> lstSortOption, bool bForce)
		{
			if (this.m_NKM_ITEM_MISC_TYPE == NKM_ITEM_MISC_TYPE.IMT_CHOICE_EQUIP)
			{
				this.m_ssEquip.lstSortOption = lstSortOption;
				this.m_ssEquip.SortList(this.m_ssEquip.lstSortOption, bForce);
			}
			this.ProcessUIFromCurrentDisplayedSortData();
		}

		// Token: 0x06006BF7 RID: 27639 RVA: 0x0023417C File Offset: 0x0023237C
		private void ProcessUIFromCurrentDisplayedSortData()
		{
			if (this.m_ssEquip.lstSortOption.Count > 0)
			{
				NKCUtil.SetLabelText(this.m_lbSortSelected, NKCEquipSortSystem.GetSortName(this.m_ssEquip.lstSortOption[0]));
			}
			else
			{
				Debug.LogError("정렬 선택하지 않아도 기본 기준이 있어야함. NKCUnitSortSystem.DEFAULT_SORT_OPTION_LIST");
			}
			this.m_tglDescending.Select(NKCEquipSortSystem.GetDescendingBySorting(this.m_ssEquip.lstSortOption), true, false);
			this.m_loopScrollRectEquip.TotalCount = this.m_ssEquip.SortedEquipList.Count;
			this.m_loopScrollRectEquip.RefreshCells(false);
			this.m_loopScrollRectEquip.SetIndexPosition(0);
			NKCUtil.SetGameobjectActive(this.m_objFilterSelected, this.m_ssEquip.FilterSet.Count > 0);
		}

		// Token: 0x06006BF8 RID: 27640 RVA: 0x00234238 File Offset: 0x00232438
		public void SetOpenSortingMenu(bool bOpen, bool bAnimate = true)
		{
			this.m_tglSorting.Select(bOpen, true, false);
			this.m_EquipSort.StartRectMove(bOpen, bAnimate);
		}

		// Token: 0x06006BF9 RID: 27641 RVA: 0x00234258 File Offset: 0x00232458
		public void OnSelectEquipSlot(NKCUISlotEquip cItemSlot, NKMEquipItemData equipData)
		{
			if (this.m_LatestSelectedSlot != null)
			{
				this.m_LatestSelectedSlot.SetSelected(false);
			}
			this.m_LatestSelectedSlot = cItemSlot;
			cItemSlot.SetSelected(true);
			NKCUIInvenEquipSlot invenEquipSlot = this.m_InvenEquipSlot;
			if (invenEquipSlot != null)
			{
				invenEquipSlot.SetData(equipData, false, false);
			}
			NKCUtil.SetGameobjectActive(this.m_imgBannerEquip, cItemSlot == null || equipData == null);
			NKCUtil.SetGameobjectActive(this.m_objEquipInfo, cItemSlot != null && equipData != null);
		}

		// Token: 0x06006BFA RID: 27642 RVA: 0x002342D8 File Offset: 0x002324D8
		public void OnValueChangeDescending(bool bValue)
		{
			if (this.m_ssEquip.lstSortOption.Count == 0)
			{
				return;
			}
			this.m_ssEquip.lstSortOption = this.m_EquipSort.ChangeAscend(this.m_ssEquip.lstSortOption);
			this.SortEquipList(this.m_ssEquip.lstSortOption, true);
		}

		// Token: 0x06006BFB RID: 27643 RVA: 0x0023432C File Offset: 0x0023252C
		public void OnValueChangeFilter(bool bValue)
		{
			NKCPopupFilterEquip.Instance.Open(this.m_setEquipFilterCategory, this.m_ssEquip, new NKCPopupFilterEquip.OnEquipFilterSetChange(this.OnSelectEquipFilter), this.m_ssEquip.ExcludeFilterSet == null || !this.m_ssEquip.ExcludeFilterSet.Contains(NKCEquipSortSystem.eFilterOption.Equip_Enchant));
		}

		// Token: 0x06006BFC RID: 27644 RVA: 0x00234380 File Offset: 0x00232580
		public void OnValueChangeSorting(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_objSortSelect, this.m_ssEquip.lstSortOption[0] != NKCEquipSortSystem.GetDefaultSortOption(NKCPopupEquipSort.SORT_OPEN_TYPE.SELECTION)[0]);
			this.m_EquipSort.OpenEquipSortMenu(this.m_ssEquip.lstSortOption[0], new NKCPopupEquipSort.OnSortOption(this.OnEquipSort), NKCEquipSortSystem.GetDescendingBySorting(this.m_ssEquip.lstSortOption), bValue, NKCPopupEquipSort.SORT_OPEN_TYPE.SELECTION);
			this.SetOpenSortingMenu(bValue, true);
		}

		// Token: 0x06006BFD RID: 27645 RVA: 0x002343FC File Offset: 0x002325FC
		private void OnClickOk()
		{
			if (this.m_LatestSelectedSlot != null)
			{
				NKCPopupSelectionConfirm.Instance.Open(this.m_NKMItemMiscTemplet, this.m_LatestSelectedSlot.GetNKMEquipItemData().m_ItemEquipID, 1L, this.m_LatestSelectedSlot.GetNKMEquipItemData().m_SetOptionId);
				return;
			}
			Log.Debug("Selected Slot is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUISelectionEquip.cs", 453);
		}

		// Token: 0x040057AD RID: 22445
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_UNIT_SELECTION";

		// Token: 0x040057AE RID: 22446
		private const string UI_ASSET_NAME = "NKM_UI_EQUIP_SELECTION";

		// Token: 0x040057AF RID: 22447
		private static NKCUISelectionEquip m_Instance;

		// Token: 0x040057B0 RID: 22448
		public NKCUIComSafeArea m_SafeArea;

		// Token: 0x040057B1 RID: 22449
		[Header("장비")]
		public GameObject m_objEquipChoice;

		// Token: 0x040057B2 RID: 22450
		public LoopScrollRect m_loopScrollRectEquip;

		// Token: 0x040057B3 RID: 22451
		public Transform m_trContentParentEquip;

		// Token: 0x040057B4 RID: 22452
		public Image m_imgBannerEquip;

		// Token: 0x040057B5 RID: 22453
		[Header("토글")]
		public NKCUIComToggle m_tglDescending;

		// Token: 0x040057B6 RID: 22454
		public NKCUIComToggle m_tglFilter;

		// Token: 0x040057B7 RID: 22455
		public NKCUIComToggle m_tglSorting;

		// Token: 0x040057B8 RID: 22456
		[Header("프리팹")]
		public NKCUISlotEquip m_pfbEquipSlot;

		// Token: 0x040057B9 RID: 22457
		[Header("우측 정보")]
		public GameObject m_objEquipInfo;

		// Token: 0x040057BA RID: 22458
		public NKCUIInvenEquipSlot m_InvenEquipSlot;

		// Token: 0x040057BB RID: 22459
		public NKCUIComStateButton m_btnOK;

		// Token: 0x040057BC RID: 22460
		[Header("필터")]
		public GameObject m_objFilterSelected;

		// Token: 0x040057BD RID: 22461
		[Header("정렬")]
		public Text m_lbSortDefault;

		// Token: 0x040057BE RID: 22462
		public GameObject m_objSortSelect;

		// Token: 0x040057BF RID: 22463
		public Text m_lbSortSelected;

		// Token: 0x040057C0 RID: 22464
		public NKCPopupEquipSort m_EquipSort;

		// Token: 0x040057C1 RID: 22465
		private NKM_ITEM_MISC_TYPE m_NKM_ITEM_MISC_TYPE = NKM_ITEM_MISC_TYPE.IMT_CHOICE_UNIT;

		// Token: 0x040057C2 RID: 22466
		private List<int> m_lstRewardId = new List<int>();

		// Token: 0x040057C3 RID: 22467
		private NKCEquipSortSystem m_ssEquip;

		// Token: 0x040057C4 RID: 22468
		private List<NKCUISlotEquip> m_lstVisibleEquipSlot = new List<NKCUISlotEquip>();

		// Token: 0x040057C5 RID: 22469
		private Stack<NKCUISlotEquip> m_stkEquipSlotPool = new Stack<NKCUISlotEquip>();

		// Token: 0x040057C6 RID: 22470
		private NKMItemMiscTemplet m_NKMItemMiscTemplet;

		// Token: 0x040057C7 RID: 22471
		private NKCUISlotEquip m_LatestSelectedSlot;

		// Token: 0x040057C8 RID: 22472
		private readonly HashSet<NKCEquipSortSystem.eFilterCategory> m_setEquipFilterCategory = new HashSet<NKCEquipSortSystem.eFilterCategory>
		{
			NKCEquipSortSystem.eFilterCategory.UnitType,
			NKCEquipSortSystem.eFilterCategory.EquipType,
			NKCEquipSortSystem.eFilterCategory.Rarity,
			NKCEquipSortSystem.eFilterCategory.Tier,
			NKCEquipSortSystem.eFilterCategory.Have
		};
	}
}
