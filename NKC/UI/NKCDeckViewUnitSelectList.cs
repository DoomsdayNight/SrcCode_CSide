using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000967 RID: 2407
	[RequireComponent(typeof(RectTransform))]
	public class NKCDeckViewUnitSelectList : MonoBehaviour
	{
		// Token: 0x17001132 RID: 4402
		// (get) Token: 0x0600605A RID: 24666 RVA: 0x001E0E14 File Offset: 0x001DF014
		public bool IsOpen
		{
			get
			{
				return this.m_bOpen;
			}
		}

		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x0600605B RID: 24667 RVA: 0x001E0E1C File Offset: 0x001DF01C
		// (set) Token: 0x0600605C RID: 24668 RVA: 0x001E0E24 File Offset: 0x001DF024
		public NKM_UNIT_TYPE CurrentTargetUnitType { get; private set; }

		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x0600605D RID: 24669 RVA: 0x001E0E2D File Offset: 0x001DF02D
		public NKCUnitSortSystem.UnitListOptions SortOptions
		{
			get
			{
				return this.m_sortOptions;
			}
		}

		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x0600605E RID: 24670 RVA: 0x001E0E35 File Offset: 0x001DF035
		public NKCOperatorSortSystem.OperatorListOptions SortOperatorOptions
		{
			get
			{
				return this.m_OperatorSortOptions;
			}
		}

		// Token: 0x0600605F RID: 24671 RVA: 0x001E0E40 File Offset: 0x001DF040
		public void Init(NKCUIDeckViewer deckView, NKCDeckViewUnitSelectList.OnDeckUnitChangeClicked onDeckUnitChangeClicked, NKCDeckViewUnitSelectList.OnClose onClose, UnityAction onClearDeck, UnityAction onAutoCompleteDeck)
		{
			this.m_sortOptions.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
			this.m_sortOptions.bDescending = true;
			this.m_sortOptions.lstSortOption = NKCUnitSortSystem.GetDefaultSortOptions(NKM_UNIT_TYPE.NUT_NORMAL, false, false);
			this.m_sortOptions.bIncludeUndeckableUnit = false;
			this.m_OperatorSortOptions.setFilterOption = new HashSet<NKCOperatorSortSystem.eFilterOption>();
			this.m_OperatorSortOptions.SetBuildOption(true, new BUILD_OPTIONS[]
			{
				BUILD_OPTIONS.DESCENDING
			});
			this.m_OperatorSortOptions.lstSortOption = NKCOperatorSortSystem.GetDefaultSortOptions(false, false);
			this.m_rtRoot = base.GetComponent<RectTransform>();
			if (this.m_LoopScrollRect != null)
			{
				this.m_LoopScrollRect.dOnGetObject += this.GetSlot;
				this.m_LoopScrollRect.dOnReturnObject += this.ReturnSlot;
				this.m_LoopScrollRect.dOnProvideData += this.ProvideSlotData;
				NKCUtil.SetScrollHotKey(this.m_LoopScrollRect, deckView);
			}
			if (this.m_sbtnFinish != null)
			{
				this.m_sbtnFinish.PointerClick.RemoveAllListeners();
				this.m_sbtnFinish.PointerClick.AddListener(new UnityAction(this.OnCloseBtn));
			}
			if (this.m_SortUI != null)
			{
				this.m_SortUI.Init(new NKCUIComUnitSortOptions.OnSorted(this.OnSortChanged), false);
				this.m_SortUI.RegisterCategories(NKCPopupFilterUnit.MakeDefaultFilterOption(NKM_UNIT_TYPE.NUT_NORMAL, NKCPopupFilterUnit.FILTER_OPEN_TYPE.NORMAL), NKCPopupSort.MakeDefaultSortSet(NKM_UNIT_TYPE.NUT_NORMAL, false), false);
			}
			if (this.m_tglHideDeckedUnit != null)
			{
				this.m_tglHideDeckedUnit.OnValueChanged.RemoveAllListeners();
				this.m_tglHideDeckedUnit.OnValueChanged.AddListener(new UnityAction<bool>(this.ToggleHideDeckedUnit));
			}
			if (this.m_sbtnUnitListAdd != null)
			{
				this.m_sbtnUnitListAdd.PointerClick.RemoveAllListeners();
				this.m_sbtnUnitListAdd.PointerClick.AddListener(new UnityAction(this.OnExpandInventoryPopup));
			}
			this.dOnDeckUnitChangeClicked = onDeckUnitChangeClicked;
			this.dOnClose = onClose;
			this.dOnAutoCompleteDeck = onAutoCompleteDeck;
			this.dOnClearDeck = onClearDeck;
			NKCUtil.SetGameobjectActive(this.m_rectSlotPoolRect, false);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06006060 RID: 24672 RVA: 0x001E1050 File Offset: 0x001DF250
		private RectTransform GetSlot(int index)
		{
			Stack<NKCUIUnitSelectListSlotBase> stack;
			NKCUIUnitSelectListSlotBase original;
			switch (this.CurrentTargetUnitType)
			{
			case NKM_UNIT_TYPE.NUT_NORMAL:
				stack = this.m_stkUnitSlotPool;
				original = this.m_pfbUnitSlot;
				break;
			case NKM_UNIT_TYPE.NUT_SHIP:
				stack = this.m_stkShipSlotPool;
				original = this.m_pfbShipSlot;
				break;
			case NKM_UNIT_TYPE.NUT_OPERATOR:
				stack = this.m_stkOperatorSlotPool;
				original = this.m_pfbOperatorSlot;
				break;
			default:
				return null;
			}
			if (stack.Count > 0)
			{
				NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase = stack.Pop();
				NKCUtil.SetGameobjectActive(nkcuiunitSelectListSlotBase, true);
				nkcuiunitSelectListSlotBase.transform.localScale = Vector3.one;
				this.m_lstVisibleSlot.Add(nkcuiunitSelectListSlotBase);
				return nkcuiunitSelectListSlotBase.GetComponent<RectTransform>();
			}
			NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase2 = UnityEngine.Object.Instantiate<NKCUIUnitSelectListSlotBase>(original);
			nkcuiunitSelectListSlotBase2.Init(false);
			NKCUtil.SetGameobjectActive(nkcuiunitSelectListSlotBase2, true);
			nkcuiunitSelectListSlotBase2.transform.localScale = Vector3.one;
			this.m_lstVisibleSlot.Add(nkcuiunitSelectListSlotBase2);
			return nkcuiunitSelectListSlotBase2.GetComponent<RectTransform>();
		}

		// Token: 0x06006061 RID: 24673 RVA: 0x001E1124 File Offset: 0x001DF324
		private void ReturnSlot(Transform go)
		{
			NKCUIUnitSelectListSlotBase component = go.GetComponent<NKCUIUnitSelectListSlotBase>();
			this.m_lstVisibleSlot.Remove(component);
			NKCUtil.SetGameobjectActive(go, false);
			go.SetParent(this.m_rectSlotPoolRect);
			if (component is NKCDeckViewUnitSelectListSlot)
			{
				this.m_stkUnitSlotPool.Push(component);
				return;
			}
			if (component is NKCUIShipSelectListSlot)
			{
				this.m_stkShipSlotPool.Push(component);
				return;
			}
			if (component is NKCUIOperatorDeckSelectSlot)
			{
				this.m_stkOperatorSlotPool.Push(component);
			}
		}

		// Token: 0x06006062 RID: 24674 RVA: 0x001E1198 File Offset: 0x001DF398
		private void ProvideSlotData(Transform tr, int idx)
		{
			if (!this.m_bOpen)
			{
				return;
			}
			if (this.CurrentTargetUnitType == NKM_UNIT_TYPE.NUT_OPERATOR && this.m_OperatorSortSystem == null)
			{
				Debug.LogError("Slot Operator Sort System Null!!");
				return;
			}
			if (this.CurrentTargetUnitType != NKM_UNIT_TYPE.NUT_OPERATOR && this.m_ssActive == null)
			{
				Debug.LogError("Slot Sort System Null!!");
				return;
			}
			NKCUIUnitSelectListSlotBase component = tr.GetComponent<NKCUIUnitSelectListSlotBase>();
			if (component == null)
			{
				return;
			}
			this.ProvideSlotDataDefault(component, idx);
		}

		// Token: 0x06006063 RID: 24675 RVA: 0x001E1200 File Offset: 0x001DF400
		private void ProvideSlotDataDefault(NKCUIUnitSelectListSlotBase slot, int idx)
		{
			switch (this.GetSlottypeByIndex(idx))
			{
			case NKCDeckViewUnitSelectList.SlotType.Empty:
				slot.SetEmpty(true, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnSlotSelected), new NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot(this.OnSlotSelected));
				return;
			case NKCDeckViewUnitSelectList.SlotType.ClearAll:
				slot.SetMode(NKCUIUnitSelectListSlotBase.eUnitSlotMode.ClearAll, true, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnSelectClearDeck), null);
				return;
			case NKCDeckViewUnitSelectList.SlotType.AutoComplete:
				slot.SetMode(NKCUIUnitSelectListSlotBase.eUnitSlotMode.AutoComplete, true, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnSelectAutoCompleteDeck), null);
				return;
			default:
			{
				idx -= this.GetExtraSlotCount();
				slot.SetEnableShowBan(NKCUtil.CheckPossibleShowBan(this.m_DeckViewerOptions.eDeckviewerMode));
				slot.SetEnableShowUpUnit(NKCUtil.CheckPossibleShowUpUnit(this.m_DeckViewerOptions.eDeckviewerMode));
				bool cityLeaderMark = false;
				long num;
				if (this.CurrentTargetUnitType == NKM_UNIT_TYPE.NUT_OPERATOR)
				{
					if (this.m_OperatorSortSystem.SortedOperatorList.Count <= idx)
					{
						return;
					}
					NKMOperator nkmoperator = this.m_OperatorSortSystem.SortedOperatorList[idx];
					num = nkmoperator.uid;
					NKMDeckIndex deckIndexCache = this.m_OperatorSortSystem.GetDeckIndexCache(num, true);
					slot.SetData(nkmoperator, deckIndexCache, true, new NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot(this.OnSlotSelected));
					slot.SetSlotState(this.m_OperatorSortSystem.GetUnitSlotState(num));
					slot.SetTouchHoldEvent(new UnityAction<NKMOperator>(this.ShowOperatorInfo));
				}
				else
				{
					if (this.m_ssActive.SortedUnitList.Count <= idx)
					{
						return;
					}
					NKMUnitData nkmunitData = this.m_ssActive.SortedUnitList[idx];
					num = nkmunitData.m_UnitUID;
					cityLeaderMark = (this.m_ssActive.GetCityStateCache(num) > NKMWorldMapManager.WorldMapLeaderState.None);
					NKMDeckIndex deckIndexCacheByOption = this.m_ssActive.GetDeckIndexCacheByOption(num, true);
					slot.SetData(nkmunitData, deckIndexCacheByOption, true, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnSlotSelected));
					slot.SetSlotState(this.m_ssActive.GetUnitSlotState(num));
					slot.SetTouchHoldEvent(new UnityAction<NKMUnitData>(this.ShowUnitInfo));
				}
				slot.SetCityLeaderMark(cityLeaderMark);
				if (this.m_DeckViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.WorldMapMissionDeckSelect)
				{
					NKCDeckViewUnitSelectListSlot nkcdeckViewUnitSelectListSlot = slot as NKCDeckViewUnitSelectListSlot;
					if (nkcdeckViewUnitSelectListSlot != null)
					{
						NKMWorldMapManager.WorldMapLeaderState unitWorldMapLeaderState = NKMWorldMapManager.GetUnitWorldMapLeaderState(NKCScenManager.CurrentUserData(), num, this.m_DeckViewerOptions.WorldMapMissionCityID);
						nkcdeckViewUnitSelectListSlot.SetCityLeaderTag(unitWorldMapLeaderState);
					}
				}
				return;
			}
			}
		}

		// Token: 0x06006064 RID: 24676 RVA: 0x001E1408 File Offset: 0x001DF608
		private void ShowUnitInfo(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID);
			NKCUIUnitInfo.OpenOption openOption = new NKCUIUnitInfo.OpenOption(this.m_ssActive.SortedUnitList, 0);
			NKM_UNIT_TYPE nkm_UNIT_TYPE = unitTempletBase.m_NKM_UNIT_TYPE;
			if (nkm_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL)
			{
				NKCUIUnitInfo.Instance.Open(unitData, null, openOption, NKC_SCEN_UNIT_LIST.eUIOpenReserve.Nothing);
				return;
			}
			if (nkm_UNIT_TYPE != NKM_UNIT_TYPE.NUT_SHIP)
			{
				return;
			}
			NKMDeckIndex shipDeckIndex = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetShipDeckIndex(NKM_DECK_TYPE.NDT_NORMAL, unitData.m_UnitUID);
			NKCUIShipInfo.Instance.Open(unitData, shipDeckIndex, openOption, NKC_SCEN_UNIT_LIST.eUIOpenReserve.Nothing);
		}

		// Token: 0x06006065 RID: 24677 RVA: 0x001E1480 File Offset: 0x001DF680
		private void ShowOperatorInfo(NKMOperator operatorData)
		{
			if (operatorData != null)
			{
				int num = 0;
				using (List<NKMOperator>.Enumerator enumerator = this.m_OperatorSortSystem.SortedOperatorList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.uid == operatorData.uid)
						{
							break;
						}
						num++;
					}
				}
				NKCUIOperatorInfo.OpenOption option = new NKCUIOperatorInfo.OpenOption(this.m_OperatorSortSystem.SortedOperatorList, num);
				NKCUIOperatorInfo.Instance.Open(operatorData, option);
			}
		}

		// Token: 0x06006066 RID: 24678 RVA: 0x001E1508 File Offset: 0x001DF708
		private int GetSlotCount()
		{
			return this.m_ssActive.SortedUnitList.Count + this.GetExtraSlotCount();
		}

		// Token: 0x06006067 RID: 24679 RVA: 0x001E1521 File Offset: 0x001DF721
		private int GetExtraSlotCount()
		{
			if (this.CurrentTargetUnitType == NKM_UNIT_TYPE.NUT_SHIP || this.CurrentTargetUnitType == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				return 1;
			}
			return 3;
		}

		// Token: 0x06006068 RID: 24680 RVA: 0x001E1538 File Offset: 0x001DF738
		private NKCDeckViewUnitSelectList.SlotType GetSlottypeByIndex(int index)
		{
			if (this.CurrentTargetUnitType == NKM_UNIT_TYPE.NUT_SHIP || this.CurrentTargetUnitType == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				if (this.GetExtraSlotCount() > 0 && index == 0)
				{
					return NKCDeckViewUnitSelectList.SlotType.Empty;
				}
				return NKCDeckViewUnitSelectList.SlotType.Normal;
			}
			else
			{
				if (this.GetExtraSlotCount() > 0)
				{
					if (index == 0)
					{
						return NKCDeckViewUnitSelectList.SlotType.Empty;
					}
					index--;
				}
				if (this.GetExtraSlotCount() > 1)
				{
					if (index == 0)
					{
						return NKCDeckViewUnitSelectList.SlotType.ClearAll;
					}
					index--;
				}
				if (this.GetExtraSlotCount() > 2 && index == 0)
				{
					return NKCDeckViewUnitSelectList.SlotType.AutoComplete;
				}
				return NKCDeckViewUnitSelectList.SlotType.Normal;
			}
		}

		// Token: 0x06006069 RID: 24681 RVA: 0x001E159C File Offset: 0x001DF79C
		private int GetIndexBySlottype(NKCDeckViewUnitSelectList.SlotType type)
		{
			int num = 0;
			if (this.CurrentTargetUnitType == NKM_UNIT_TYPE.NUT_SHIP)
			{
				if (type == NKCDeckViewUnitSelectList.SlotType.Empty)
				{
					return num;
				}
				return num + 1;
			}
			else
			{
				if (type == NKCDeckViewUnitSelectList.SlotType.Empty)
				{
					return num;
				}
				num++;
				if (type == NKCDeckViewUnitSelectList.SlotType.ClearAll)
				{
					return num;
				}
				num++;
				if (type == NKCDeckViewUnitSelectList.SlotType.AutoComplete)
				{
					return num;
				}
				return num + 1;
			}
		}

		// Token: 0x0600606A RID: 24682 RVA: 0x001E15DF File Offset: 0x001DF7DF
		private void OnSelectClearDeck(NKMUnitData selectedUnit, NKMUnitTempletBase unitTempletBase, NKMDeckIndex selectedUnitDeckIndex, NKCUnitSortSystem.eUnitState unitSlotState, NKCUIUnitSelectList.eUnitSlotSelectState unitSlotSelectState)
		{
			UnityAction unityAction = this.dOnClearDeck;
			if (unityAction == null)
			{
				return;
			}
			unityAction();
		}

		// Token: 0x0600606B RID: 24683 RVA: 0x001E15F1 File Offset: 0x001DF7F1
		private void OnSelectAutoCompleteDeck(NKMUnitData selectedUnit, NKMUnitTempletBase unitTempletBase, NKMDeckIndex selectedUnitDeckIndex, NKCUnitSortSystem.eUnitState unitSlotState, NKCUIUnitSelectList.eUnitSlotSelectState unitSlotSelectState)
		{
			UnityAction unityAction = this.dOnAutoCompleteDeck;
			if (unityAction == null)
			{
				return;
			}
			unityAction();
		}

		// Token: 0x0600606C RID: 24684 RVA: 0x001E1604 File Offset: 0x001DF804
		private NKCUnitSortSystem GetUnitSortSystem(NKM_UNIT_TYPE type)
		{
			if (this.m_dicUnitSortSystem.ContainsKey(type) && this.m_dicUnitSortSystem[type] != null)
			{
				return this.m_dicUnitSortSystem[type];
			}
			NKCUnitSortSystem nkcunitSortSystem;
			if (type == NKM_UNIT_TYPE.NUT_NORMAL || type != NKM_UNIT_TYPE.NUT_SHIP)
			{
				nkcunitSortSystem = new NKCUnitSort(NKCScenManager.CurrentUserData(), this.m_sortOptions);
			}
			else
			{
				nkcunitSortSystem = new NKCShipSort(NKCScenManager.CurrentUserData(), this.m_sortOptions);
			}
			nkcunitSortSystem.SetEnableShowBan(NKCUtil.CheckPossibleShowBan(this.m_DeckViewerOptions.eDeckviewerMode));
			nkcunitSortSystem.SetEnableShowUpUnit(NKCUtil.CheckPossibleShowUpUnit(this.m_DeckViewerOptions.eDeckviewerMode));
			this.m_dicUnitSortSystem[type] = nkcunitSortSystem;
			return nkcunitSortSystem;
		}

		// Token: 0x0600606D RID: 24685 RVA: 0x001E16A0 File Offset: 0x001DF8A0
		private NKCUnitSortSystem GetLocalUnitSortSystem(NKM_UNIT_TYPE type)
		{
			if (this.m_dicUnitSortSystem.ContainsKey(type) && this.m_dicUnitSortSystem[type] != null)
			{
				return this.m_dicUnitSortSystem[type];
			}
			NKCUnitSortSystem nkcunitSortSystem;
			if (type == NKM_UNIT_TYPE.NUT_NORMAL || type != NKM_UNIT_TYPE.NUT_SHIP)
			{
				nkcunitSortSystem = new NKCUnitSort(NKCScenManager.CurrentUserData(), this.m_sortOptions, true);
			}
			else
			{
				nkcunitSortSystem = new NKCShipSort(NKCScenManager.CurrentUserData(), this.m_sortOptions, true);
			}
			nkcunitSortSystem.SetEnableShowBan(NKCUtil.CheckPossibleShowBan(this.m_DeckViewerOptions.eDeckviewerMode));
			nkcunitSortSystem.SetEnableShowUpUnit(NKCUtil.CheckPossibleShowUpUnit(this.m_DeckViewerOptions.eDeckviewerMode));
			this.m_dicUnitSortSystem[type] = nkcunitSortSystem;
			return nkcunitSortSystem;
		}

		// Token: 0x0600606E RID: 24686 RVA: 0x001E173E File Offset: 0x001DF93E
		public void InvalidateSortData(NKM_UNIT_TYPE type)
		{
			if (this.m_bOpen && this.CurrentTargetUnitType == type)
			{
				this.m_dicUnitSortSystem.Remove(type);
				this.RefreshLoopScrollList(type, true);
				return;
			}
			this.m_dicUnitSortSystem.Remove(type);
		}

		// Token: 0x0600606F RID: 24687 RVA: 0x001E1774 File Offset: 0x001DF974
		public void Open(bool bAnimate, NKM_UNIT_TYPE targetType, NKCUnitSortSystem.UnitListOptions sortOptions, NKCUIDeckViewer.DeckViewerOption deckViewerOption)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			foreach (KeyValuePair<NKM_UNIT_TYPE, NKCUnitSortSystem> keyValuePair in this.m_dicUnitSortSystem)
			{
				NKCUnitSortSystem value = keyValuePair.Value;
				if (value != null)
				{
					value.SetEnableShowBan(NKCUtil.CheckPossibleShowBan(deckViewerOption.eDeckviewerMode));
					value.SetEnableShowUpUnit(NKCUtil.CheckPossibleShowUpUnit(deckViewerOption.eDeckviewerMode));
				}
			}
			if (!this.m_bOpen)
			{
				this.m_rtRoot.DOKill(false);
				if (bAnimate)
				{
					this.m_rtRoot.anchoredPosition = new Vector2(this.m_rtRoot.GetWidth() * 1.5f, 0f);
					this.m_rtRoot.DOAnchorPosX(0f, 0.4f, false).SetEase(Ease.OutCubic);
				}
				else
				{
					this.m_rtRoot.anchoredPosition = Vector2.zero;
				}
			}
			if (sortOptions.eDeckType == NKM_DECK_TYPE.NDT_NORMAL)
			{
				NKCUtil.SetGameobjectActive(this.m_tglHideDeckedUnit, true);
				if (this.m_tglHideDeckedUnit != null)
				{
					this.m_tglHideDeckedUnit.Select(sortOptions.bHideDeckedUnit, true, false);
				}
				if (this.m_AniHideDeckedUnit != null)
				{
					this.m_AniHideDeckedUnit.SetTrigger("Enable");
				}
			}
			else
			{
				this.m_sortOptions.bHideDeckedUnit = false;
				this.m_OperatorSortOptions.SetBuildOption(false, new BUILD_OPTIONS[]
				{
					BUILD_OPTIONS.HIDE_DECKED_UNIT
				});
				NKCUtil.SetGameobjectActive(this.m_tglHideDeckedUnit, false);
				if (this.m_AniHideDeckedUnit != null)
				{
					this.m_AniHideDeckedUnit.SetTrigger("Disable");
				}
			}
			this.m_bOpen = true;
			this.m_sortOptions = sortOptions;
			this.m_DeckViewerOptions = deckViewerOption;
			this.RefreshLoopScrollList(targetType, true);
			this.UpdateUnitCount();
		}

		// Token: 0x06006070 RID: 24688 RVA: 0x001E1930 File Offset: 0x001DFB30
		public void Open(bool bAnimate, NKM_UNIT_TYPE targetType, NKCOperatorSortSystem.OperatorListOptions sortOptions, NKCUIDeckViewer.DeckViewerOption deckViewerOption)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (!this.m_bOpen)
			{
				this.m_rtRoot.DOKill(false);
				if (bAnimate)
				{
					this.m_rtRoot.anchoredPosition = new Vector2(this.m_rtRoot.GetWidth() * 1.5f, 0f);
					this.m_rtRoot.DOAnchorPosX(0f, 0.4f, false).SetEase(Ease.OutCubic);
				}
				else
				{
					this.m_rtRoot.anchoredPosition = Vector2.zero;
				}
			}
			if (sortOptions.eDeckType == NKM_DECK_TYPE.NDT_NORMAL)
			{
				NKCUtil.SetGameobjectActive(this.m_tglHideDeckedUnit, true);
				if (this.m_tglHideDeckedUnit != null)
				{
					this.m_tglHideDeckedUnit.Select(sortOptions.IsHasBuildOption(BUILD_OPTIONS.HIDE_DECKED_UNIT), true, false);
				}
				if (this.m_AniHideDeckedUnit != null)
				{
					this.m_AniHideDeckedUnit.SetTrigger("Enable");
				}
			}
			else
			{
				this.m_sortOptions.bHideDeckedUnit = false;
				this.m_OperatorSortOptions.SetBuildOption(false, new BUILD_OPTIONS[]
				{
					BUILD_OPTIONS.HIDE_DECKED_UNIT
				});
				NKCUtil.SetGameobjectActive(this.m_tglHideDeckedUnit, false);
				if (this.m_AniHideDeckedUnit != null)
				{
					this.m_AniHideDeckedUnit.SetTrigger("Disable");
				}
			}
			this.m_bOpen = true;
			this.m_OperatorSortOptions = sortOptions;
			this.m_DeckViewerOptions = deckViewerOption;
			this.RefreshLoopScrollList(targetType, true);
			this.UpdateUnitCount();
		}

		// Token: 0x06006071 RID: 24689 RVA: 0x001E1A80 File Offset: 0x001DFC80
		public void Close(bool bAnimate)
		{
			this.m_bOpen = false;
			this.Cleanup();
			NKCDeckViewUnitSelectList.OnClose onClose = this.dOnClose;
			if (onClose != null)
			{
				onClose(this.CurrentTargetUnitType);
			}
			this.CurrentTargetUnitType = NKM_UNIT_TYPE.NUT_INVALID;
			this.m_rtRoot.DOKill(false);
			if (bAnimate)
			{
				this.m_rtRoot.DOAnchorPosX(this.m_rtRoot.GetWidth() * 1.5f, 0.4f, false).SetEase(Ease.OutCubic).OnComplete(delegate
				{
					base.gameObject.SetActive(false);
				});
				return;
			}
			this.m_rtRoot.anchoredPosition = new Vector2(this.m_rtRoot.GetWidth() * 1.5f, 0f);
			base.gameObject.SetActive(false);
		}

		// Token: 0x06006072 RID: 24690 RVA: 0x001E1B36 File Offset: 0x001DFD36
		public void Cleanup()
		{
			this.m_ssActive = null;
			this.m_OperatorSortSystem = null;
			NKCUIComUnitSortOptions sortUI = this.m_SortUI;
			if (sortUI != null)
			{
				sortUI.ResetUI(false);
			}
			this.m_dicUnitSortSystem.Clear();
		}

		// Token: 0x06006073 RID: 24691 RVA: 0x001E1B64 File Offset: 0x001DFD64
		public void UpdateUnitCount()
		{
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			int currentUnitCount = armyData.GetCurrentUnitCount();
			int maxUnitCount = armyData.m_MaxUnitCount;
			if (this.m_txtUnitCout != null)
			{
				this.m_txtUnitCout.text = string.Format("{0}/{1}", currentUnitCount, maxUnitCount);
			}
		}

		// Token: 0x06006074 RID: 24692 RVA: 0x001E1BB8 File Offset: 0x001DFDB8
		public void OnExpandInventoryPopup()
		{
			int maxUnitCount = NKCScenManager.CurrentUserData().m_ArmyData.m_MaxUnitCount;
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKM_INVENTORY_EXPAND_TYPE nkm_INVENTORY_EXPAND_TYPE = NKM_INVENTORY_EXPAND_TYPE.NIET_UNIT;
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
			sliderInfo.maxCount = 1100;
			sliderInfo.currentCount = myUserData.m_ArmyData.m_MaxUnitCount;
			sliderInfo.inventoryType = NKM_INVENTORY_EXPAND_TYPE.NIET_UNIT;
			NKCPopupInventoryAdd.Instance.Open(NKCUtilString.GET_STRING_INVENTORY_UNIT, expandDesc, sliderInfo, 100, 101, delegate(int value)
			{
				NKCPacketSender.Send_NKMPacket_INVENTORY_EXPAND_REQ(NKM_INVENTORY_EXPAND_TYPE.NIET_UNIT, value);
			}, false);
		}

		// Token: 0x06006075 RID: 24693 RVA: 0x001E1CA0 File Offset: 0x001DFEA0
		private void OnSortChanged(bool bResetScroll)
		{
			if (this.CurrentTargetUnitType == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				if (this.m_OperatorSortSystem != null)
				{
					this.m_OperatorSortOptions = this.m_OperatorSortSystem.Options;
					this.m_LoopScrollRect.TotalCount = this.m_OperatorSortSystem.SortedOperatorList.Count + this.GetExtraSlotCount();
					if (bResetScroll)
					{
						this.m_LoopScrollRect.SetIndexPosition(0);
						return;
					}
					this.m_LoopScrollRect.RefreshCells(false);
					return;
				}
			}
			else if (this.m_ssActive != null)
			{
				this.m_sortOptions = this.m_ssActive.Options;
				this.m_LoopScrollRect.TotalCount = this.m_ssActive.SortedUnitList.Count + this.GetExtraSlotCount();
				if (bResetScroll)
				{
					this.m_LoopScrollRect.SetIndexPosition(0);
					return;
				}
				this.m_LoopScrollRect.RefreshCells(false);
			}
		}

		// Token: 0x06006076 RID: 24694 RVA: 0x001E1D68 File Offset: 0x001DFF68
		private void ToggleHideDeckedUnit(bool value)
		{
			this.m_sortOptions.bHideDeckedUnit = value;
			this.m_OperatorSortOptions.SetBuildOption(value, new BUILD_OPTIONS[]
			{
				BUILD_OPTIONS.HIDE_DECKED_UNIT
			});
			if (this.CurrentTargetUnitType == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				this.m_OperatorSortSystem.FilterList(this.m_OperatorSortSystem.FilterSet, value);
			}
			else
			{
				this.m_ssActive.FilterList(this.m_ssActive.FilterSet, value);
			}
			this.RefreshLoopScrollList(this.CurrentTargetUnitType, true);
		}

		// Token: 0x06006077 RID: 24695 RVA: 0x001E1DDD File Offset: 0x001DFFDD
		private void OnCloseBtn()
		{
			this.Close(true);
		}

		// Token: 0x06006078 RID: 24696 RVA: 0x001E1DE8 File Offset: 0x001DFFE8
		public void UpdateLoopScrollList(NKM_UNIT_TYPE eType, NKCUnitSortSystem.UnitListOptions options)
		{
			if (!this.m_bOpen)
			{
				return;
			}
			if (this.CurrentTargetUnitType != eType || this.m_sortOptions.eDeckType != options.eDeckType)
			{
				this.m_sortOptions = options;
				if (this.m_sortOptions.eDeckType == NKM_DECK_TYPE.NDT_NORMAL)
				{
					NKCUtil.SetGameobjectActive(this.m_tglHideDeckedUnit, true);
					if (this.m_tglHideDeckedUnit != null)
					{
						this.m_tglHideDeckedUnit.Select(this.m_sortOptions.bHideDeckedUnit, true, false);
					}
					if (this.m_AniHideDeckedUnit != null)
					{
						this.m_AniHideDeckedUnit.SetTrigger("Enable");
					}
				}
				else
				{
					this.m_sortOptions.bHideDeckedUnit = false;
					this.m_OperatorSortOptions.SetBuildOption(false, new BUILD_OPTIONS[]
					{
						BUILD_OPTIONS.HIDE_DECKED_UNIT
					});
					NKCUtil.SetGameobjectActive(this.m_tglHideDeckedUnit, false);
					if (this.m_AniHideDeckedUnit != null)
					{
						this.m_AniHideDeckedUnit.SetTrigger("Disable");
					}
				}
			}
			else
			{
				this.m_sortOptions.setDuplicateUnitID = options.setDuplicateUnitID;
				this.m_sortOptions.setExcludeUnitID = options.setExcludeUnitID;
				this.m_sortOptions.setExcludeUnitUID = options.setExcludeUnitUID;
				this.m_OperatorSortOptions.setDuplicateOperatorID = options.setDuplicateUnitID;
				this.m_OperatorSortOptions.setExcludeOperatorID = options.setExcludeUnitID;
				this.m_OperatorSortOptions.setExcludeOperatorUID = options.setExcludeUnitUID;
			}
			this.m_dicUnitSortSystem.Remove(eType);
			this.RefreshLoopScrollList(eType, false);
		}

		// Token: 0x06006079 RID: 24697 RVA: 0x001E1F54 File Offset: 0x001E0154
		public void UpdateLoopScrollList(NKM_UNIT_TYPE eType, NKCOperatorSortSystem.OperatorListOptions options)
		{
			if (!this.m_bOpen)
			{
				return;
			}
			if (this.CurrentTargetUnitType != eType)
			{
				this.m_OperatorSortOptions = options;
			}
			else
			{
				this.m_OperatorSortOptions.setDuplicateOperatorID = options.setDuplicateOperatorID;
				this.m_OperatorSortOptions.setExcludeOperatorID = options.setExcludeOperatorID;
				this.m_OperatorSortOptions.setExcludeOperatorUID = options.setExcludeOperatorUID;
			}
			if (this.m_sortOptions.eDeckType == NKM_DECK_TYPE.NDT_NORMAL)
			{
				NKCUtil.SetGameobjectActive(this.m_tglHideDeckedUnit, true);
				if (this.m_tglHideDeckedUnit != null)
				{
					this.m_tglHideDeckedUnit.Select(this.m_sortOptions.bHideDeckedUnit, true, false);
				}
				if (this.m_AniHideDeckedUnit != null)
				{
					this.m_AniHideDeckedUnit.SetTrigger("Enable");
				}
			}
			else
			{
				this.m_sortOptions.bHideDeckedUnit = false;
				this.m_OperatorSortOptions.SetBuildOption(false, new BUILD_OPTIONS[]
				{
					BUILD_OPTIONS.HIDE_DECKED_UNIT
				});
				NKCUtil.SetGameobjectActive(this.m_tglHideDeckedUnit, false);
				if (this.m_AniHideDeckedUnit != null)
				{
					this.m_AniHideDeckedUnit.SetTrigger("Disable");
				}
			}
			this.m_dicUnitSortSystem.Remove(eType);
			this.RefreshLoopScrollList(eType, false);
		}

		// Token: 0x0600607A RID: 24698 RVA: 0x001E2074 File Offset: 0x001E0274
		private void RefreshLoopScrollList(NKM_UNIT_TYPE targetType, bool bResetPosition)
		{
			bool flag = this.CurrentTargetUnitType != targetType;
			this.CurrentTargetUnitType = targetType;
			if (flag)
			{
				this.m_sortOptions.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
				this.m_sortOptions.lstSortOption = NKCUnitSortSystem.GetDefaultSortOptions(targetType, false, false);
				this.m_sortOptions.bDescending = true;
				this.m_sortOptions.bHideDeckedUnit = (this.m_sortOptions.eDeckType == NKM_DECK_TYPE.NDT_NORMAL);
				this.m_OperatorSortOptions.setFilterOption = new HashSet<NKCOperatorSortSystem.eFilterOption>();
				this.m_OperatorSortOptions.lstSortOption = NKCOperatorSortSystem.GetDefaultSortOptions(false, false);
				this.m_OperatorSortOptions.SetBuildOption(false, new BUILD_OPTIONS[]
				{
					BUILD_OPTIONS.DESCENDING
				});
				this.m_OperatorSortOptions.SetBuildOption(this.m_OperatorSortOptions.eDeckType == NKM_DECK_TYPE.NDT_NORMAL, new BUILD_OPTIONS[]
				{
					BUILD_OPTIONS.HIDE_DECKED_UNIT
				});
			}
			if (this.m_tglHideDeckedUnit != null)
			{
				if (this.CurrentTargetUnitType == NKM_UNIT_TYPE.NUT_OPERATOR)
				{
					this.m_tglHideDeckedUnit.Select(this.m_OperatorSortOptions.IsHasBuildOption(BUILD_OPTIONS.HIDE_DECKED_UNIT), true, false);
				}
				else
				{
					this.m_tglHideDeckedUnit.Select(this.m_sortOptions.bHideDeckedUnit, true, false);
				}
			}
			if (this.CurrentTargetUnitType == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				if (this.m_DeckViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
				{
					this.m_OperatorSortSystem = new NKCOperatorSort(NKCScenManager.CurrentUserData(), this.m_OperatorSortOptions, true);
				}
				else
				{
					this.m_OperatorSortSystem = new NKCOperatorSort(NKCScenManager.CurrentUserData(), this.m_OperatorSortOptions);
				}
				if (this.m_SortUI != null)
				{
					this.m_SortUI.RegisterOperatorSort(this.m_OperatorSortSystem);
				}
			}
			else
			{
				if (this.m_DeckViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
				{
					this.m_ssActive = this.GetLocalUnitSortSystem(targetType);
				}
				else
				{
					this.m_ssActive = this.GetUnitSortSystem(targetType);
				}
				if (this.m_SortUI != null)
				{
					this.m_SortUI.RegisterUnitSort(this.m_ssActive);
				}
			}
			if (flag)
			{
				if (this.m_SortUI != null)
				{
					if (this.CurrentTargetUnitType == NKM_UNIT_TYPE.NUT_OPERATOR)
					{
						this.m_SortUI.RegisterCategories(this.GetOprFilterCategorySet(), this.GetOprSortCategorySet(), false);
					}
					else
					{
						this.m_SortUI.RegisterCategories(this.GetFilterCategorySet(), this.GetSortCategorySet(), false);
					}
					this.m_SortUI.ResetUI(this.CurrentTargetUnitType == NKM_UNIT_TYPE.NUT_NORMAL);
				}
				this.m_sortOptions.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
				this.m_OperatorSortOptions.setFilterOption = new HashSet<NKCOperatorSortSystem.eFilterOption>();
				switch (targetType)
				{
				case NKM_UNIT_TYPE.NUT_NORMAL:
					this.m_LoopScrollRect.ContentConstraintCount = 3;
					this.m_GridLayoutGroup.constraintCount = 3;
					this.m_GridLayoutGroup.cellSize = this.m_vUnitSlotSize;
					this.m_GridLayoutGroup.spacing = this.m_vUnitSlotSpacing;
					break;
				case NKM_UNIT_TYPE.NUT_SHIP:
					this.m_LoopScrollRect.ContentConstraintCount = 1;
					this.m_GridLayoutGroup.constraintCount = 1;
					this.m_GridLayoutGroup.cellSize = this.m_vShipSlotSize;
					this.m_GridLayoutGroup.spacing = this.m_vShipSlotSpacing;
					break;
				case NKM_UNIT_TYPE.NUT_OPERATOR:
					this.m_LoopScrollRect.ContentConstraintCount = 3;
					this.m_GridLayoutGroup.constraintCount = 3;
					this.m_GridLayoutGroup.cellSize = this.m_vOperatorSlotSize;
					this.m_GridLayoutGroup.spacing = this.m_vOperatorSlotSpacing;
					break;
				}
				this.m_LoopScrollRect.ResetContentSpacing();
				this.m_LoopScrollRect.PrepareCells(0);
			}
			this.OnSortChanged(flag || bResetPosition);
		}

		// Token: 0x0600607B RID: 24699 RVA: 0x001E23AC File Offset: 0x001E05AC
		private HashSet<NKCUnitSortSystem.eFilterCategory> GetFilterCategorySet()
		{
			switch (this.CurrentTargetUnitType)
			{
			case NKM_UNIT_TYPE.NUT_NORMAL:
				return NKCPopupFilterUnit.MakeDefaultFilterOption(NKM_UNIT_TYPE.NUT_NORMAL, NKCPopupFilterUnit.FILTER_OPEN_TYPE.NORMAL);
			case NKM_UNIT_TYPE.NUT_SHIP:
				return NKCPopupFilterUnit.MakeDefaultFilterOption(NKM_UNIT_TYPE.NUT_SHIP, NKCPopupFilterUnit.FILTER_OPEN_TYPE.NORMAL);
			}
			Debug.LogError(string.Format("여기서 {0} 을 사용하는게 맞는지 확인 필요함. 사용할 경우 코드 추가", this.CurrentTargetUnitType));
			return new HashSet<NKCUnitSortSystem.eFilterCategory>();
		}

		// Token: 0x0600607C RID: 24700 RVA: 0x001E2405 File Offset: 0x001E0605
		private HashSet<NKCOperatorSortSystem.eFilterCategory> GetOprFilterCategorySet()
		{
			return NKCPopupFilterOperator.MakeDefaultFilterCategory(NKCPopupFilterOperator.FILTER_OPEN_TYPE.NORMAL);
		}

		// Token: 0x0600607D RID: 24701 RVA: 0x001E2410 File Offset: 0x001E0610
		private HashSet<NKCUnitSortSystem.eSortCategory> GetSortCategorySet()
		{
			switch (this.CurrentTargetUnitType)
			{
			case NKM_UNIT_TYPE.NUT_NORMAL:
				return NKCPopupSort.MakeDefaultSortSet(NKM_UNIT_TYPE.NUT_NORMAL, false);
			case NKM_UNIT_TYPE.NUT_SHIP:
				return NKCPopupSort.MakeDefaultSortSet(NKM_UNIT_TYPE.NUT_SHIP, false);
			}
			return new HashSet<NKCUnitSortSystem.eSortCategory>();
		}

		// Token: 0x0600607E RID: 24702 RVA: 0x001E2450 File Offset: 0x001E0650
		private HashSet<NKCOperatorSortSystem.eSortCategory> GetOprSortCategorySet()
		{
			NKM_UNIT_TYPE currentTargetUnitType = this.CurrentTargetUnitType;
			if (currentTargetUnitType - NKM_UNIT_TYPE.NUT_NORMAL <= 1 || currentTargetUnitType != NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				return new HashSet<NKCOperatorSortSystem.eSortCategory>();
			}
			return NKCPopupSort.MakeDefaultOprSortSet(NKM_UNIT_TYPE.NUT_OPERATOR, false);
		}

		// Token: 0x0600607F RID: 24703 RVA: 0x001E247C File Offset: 0x001E067C
		private void OnSlotSelected(NKMUnitData selectedUnit, NKMUnitTempletBase unitTempletBase, NKMDeckIndex selectedUnitDeckIndex, NKCUnitSortSystem.eUnitState unitSlotState, NKCUIUnitSelectList.eUnitSlotSelectState unitSlotSelectState)
		{
			long targetUID = (selectedUnit != null) ? selectedUnit.m_UnitUID : 0L;
			this.OnSlotSelected(targetUID, unitTempletBase, selectedUnitDeckIndex, unitSlotState, unitSlotSelectState);
		}

		// Token: 0x06006080 RID: 24704 RVA: 0x001E24A4 File Offset: 0x001E06A4
		private void OnSlotSelected(NKMOperator selectedOperator, NKMUnitTempletBase unitTempletBase, NKMDeckIndex selectedUnitDeckIndex, NKCUnitSortSystem.eUnitState unitSlotState, NKCUIUnitSelectList.eUnitSlotSelectState unitSlotSelectState)
		{
			long targetUID = (selectedOperator != null) ? selectedOperator.uid : 0L;
			this.OnSlotSelected(targetUID, unitTempletBase, selectedUnitDeckIndex, unitSlotState, unitSlotSelectState);
		}

		// Token: 0x06006081 RID: 24705 RVA: 0x001E24CC File Offset: 0x001E06CC
		private void OnSlotSelected(long targetUID, NKMUnitTempletBase unitTempletBase, NKMDeckIndex selectedUnitDeckIndex, NKCUnitSortSystem.eUnitState unitSlotState, NKCUIUnitSelectList.eUnitSlotSelectState unitSlotSelectState)
		{
			switch (unitSlotState)
			{
			case NKCUnitSortSystem.eUnitState.NONE:
			case NKCUnitSortSystem.eUnitState.SEIZURE:
			case NKCUnitSortSystem.eUnitState.LOBBY_UNIT:
				break;
			case NKCUnitSortSystem.eUnitState.DUPLICATE:
				if (this.m_DeckViewerOptions.eDeckviewerMode != NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
				{
					return;
				}
				break;
			default:
				return;
			}
			if (this.m_sortOptions.eDeckType == NKM_DECK_TYPE.NDT_NORMAL && ((this.CurrentTargetUnitType == NKM_UNIT_TYPE.NUT_NORMAL && this.m_ssActive.GetDeckIndexCache(targetUID, true) != NKMDeckIndex.None) || (this.CurrentTargetUnitType == NKM_UNIT_TYPE.NUT_OPERATOR && this.m_OperatorSortSystem.GetDeckIndexCache(targetUID, true) != NKMDeckIndex.None)))
			{
				this.OpenUnitDeckChangeWarning(targetUID);
				return;
			}
			if (this.m_DeckViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck && this.IsLocalDeckedUnitId(this.CurrentTargetUnitType, unitTempletBase) && unitSlotState == NKCUnitSortSystem.eUnitState.DUPLICATE)
			{
				this.OpenUnitDeckChangeWarning(targetUID);
				return;
			}
			this.ConfirmSlotSelected(targetUID);
		}

		// Token: 0x06006082 RID: 24706 RVA: 0x001E25C0 File Offset: 0x001E07C0
		private void ConfirmSlotSelected(long UID)
		{
			NKMDeckIndex deckIndexCache;
			if (this.CurrentTargetUnitType == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				deckIndexCache = this.m_OperatorSortSystem.GetDeckIndexCache(UID, true);
			}
			else
			{
				deckIndexCache = this.m_ssActive.GetDeckIndexCache(UID, true);
			}
			NKCDeckViewUnitSelectList.OnDeckUnitChangeClicked onDeckUnitChangeClicked = this.dOnDeckUnitChangeClicked;
			if (onDeckUnitChangeClicked == null)
			{
				return;
			}
			onDeckUnitChangeClicked(deckIndexCache, UID, this.CurrentTargetUnitType);
		}

		// Token: 0x06006083 RID: 24707 RVA: 0x001E260C File Offset: 0x001E080C
		public void UpdateSlot(long uid, NKMUnitData unitData)
		{
			NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase = this.FindSlotFromCurrentList(unitData);
			if (nkcuiunitSelectListSlotBase != null)
			{
				NKMDeckIndex deckIndex = (this.m_ssActive == null) ? NKMDeckIndex.None : this.m_ssActive.GetDeckIndexCacheByOption(unitData.m_UnitUID, true);
				nkcuiunitSelectListSlotBase.SetData(unitData, deckIndex, true, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnSlotSelected));
			}
		}

		// Token: 0x06006084 RID: 24708 RVA: 0x001E2664 File Offset: 0x001E0864
		public void UpdateSlot(long uid, NKMOperator operatorData)
		{
			NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase = this.FindSlotFromCurrentList(operatorData);
			if (nkcuiunitSelectListSlotBase != null)
			{
				NKMDeckIndex deckIndex = (this.m_OperatorSortSystem == null) ? NKMDeckIndex.None : this.m_OperatorSortSystem.GetDeckIndexCache(operatorData.uid, true);
				nkcuiunitSelectListSlotBase.SetData(operatorData, deckIndex, true, new NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot(this.OnSlotSelected));
			}
		}

		// Token: 0x06006085 RID: 24709 RVA: 0x001E26BC File Offset: 0x001E08BC
		private NKCUIUnitSelectListSlotBase FindSlotFromCurrentList(NKMUnitData unitData)
		{
			return this.m_lstVisibleSlot.Find((NKCUIUnitSelectListSlotBase x) => x.NKMUnitData != null && x.NKMUnitData.m_UnitUID == unitData.m_UnitUID);
		}

		// Token: 0x06006086 RID: 24710 RVA: 0x001E26F0 File Offset: 0x001E08F0
		private NKCUIUnitSelectListSlotBase FindSlotFromCurrentList(NKMOperator operatorData)
		{
			return this.m_lstVisibleSlot.Find((NKCUIUnitSelectListSlotBase x) => x.NKMOperatorData != null && x.NKMOperatorData.uid == operatorData.uid);
		}

		// Token: 0x06006087 RID: 24711 RVA: 0x001E2724 File Offset: 0x001E0924
		public NKCUIUnitSelectListSlotBase FindSlotFromCurrentList(NKM_UNIT_TYPE unitType, long unitUID)
		{
			if (unitType == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				return this.m_lstVisibleSlot.Find((NKCUIUnitSelectListSlotBase x) => x.NKMOperatorData != null && x.NKMOperatorData.uid == unitUID);
			}
			return this.m_lstVisibleSlot.Find((NKCUIUnitSelectListSlotBase x) => x.NKMUnitData != null && x.NKMUnitData.m_UnitUID == unitUID);
		}

		// Token: 0x06006088 RID: 24712 RVA: 0x001E2774 File Offset: 0x001E0974
		private void OpenUnitDeckChangeWarning(long UID)
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_DECK_CHANGE_UNIT_WARNING, delegate()
			{
				this.ConfirmSlotSelected(UID);
			}, null, false);
		}

		// Token: 0x06006089 RID: 24713 RVA: 0x001E27B4 File Offset: 0x001E09B4
		private bool IsLocalDeckedUnitId(NKM_UNIT_TYPE unitType, NKMUnitTempletBase unitTempletBase)
		{
			if (unitTempletBase == null)
			{
				return false;
			}
			bool flag = false;
			if (unitType == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				if (this.m_OperatorSortSystem == null)
				{
					return flag;
				}
				flag = this.m_OperatorSortSystem.IsDeckedOperatorId(unitTempletBase.m_UnitID);
				if (!flag && unitTempletBase.m_BaseUnitID > 0)
				{
					flag = this.m_OperatorSortSystem.IsDeckedOperatorId(unitTempletBase.m_BaseUnitID);
				}
			}
			else
			{
				if (this.m_ssActive == null)
				{
					return flag;
				}
				flag = this.m_ssActive.IsDeckedUnitId(unitTempletBase.m_NKM_UNIT_TYPE, unitTempletBase.m_UnitID);
				if (!flag && unitTempletBase.m_BaseUnitID > 0)
				{
					flag = this.m_ssActive.IsDeckedUnitId(unitTempletBase.m_NKM_UNIT_TYPE, unitTempletBase.m_BaseUnitID);
				}
			}
			return flag;
		}

		// Token: 0x0600608A RID: 24714 RVA: 0x001E2850 File Offset: 0x001E0A50
		public NKCUIUnitSelectListSlotBase GetAndScrollToTargetUnitSlot(int unitID)
		{
			NKCUIUnitSelectListSlotBase result;
			if (this.CurrentTargetUnitType == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				int num = this.m_OperatorSortSystem.SortedOperatorList.FindIndex((NKMOperator x) => x.id == unitID);
				if (num < 0)
				{
					Debug.LogError("Target unit not found!!");
					return null;
				}
				NKMOperator operatorData = this.m_OperatorSortSystem.SortedOperatorList[num];
				num += this.GetExtraSlotCount();
				this.m_LoopScrollRect.SetIndexPosition(num);
				result = this.m_lstVisibleSlot.Find((NKCUIUnitSelectListSlotBase x) => x.NKMOperatorData != null && x.NKMOperatorData.uid == operatorData.uid);
			}
			else
			{
				int num2 = this.m_ssActive.SortedUnitList.FindIndex((NKMUnitData x) => x.m_UnitID == unitID);
				if (num2 < 0)
				{
					Debug.LogError("Target unit not found!!");
					return null;
				}
				NKMUnitData unitData = this.m_ssActive.SortedUnitList[num2];
				num2 += this.GetExtraSlotCount();
				this.m_LoopScrollRect.SetIndexPosition(num2);
				result = this.m_lstVisibleSlot.Find((NKCUIUnitSelectListSlotBase x) => x.NKMUnitData != null && x.NKMUnitData.m_UnitUID == unitData.m_UnitUID);
			}
			return result;
		}

		// Token: 0x0600608B RID: 24715 RVA: 0x001E2974 File Offset: 0x001E0B74
		public NKCUIUnitSelectListSlotBase GetAndScrollSlotBySlotType(NKCDeckViewUnitSelectList.SlotType type)
		{
			int indexBySlottype = this.GetIndexBySlottype(type);
			this.m_LoopScrollRect.SetIndexPosition(indexBySlottype);
			NKCUIUnitSelectListSlotBase result = null;
			if (indexBySlottype < this.m_lstVisibleSlot.Count)
			{
				result = this.m_lstVisibleSlot[indexBySlottype];
			}
			return result;
		}

		// Token: 0x04004CB0 RID: 19632
		private RectTransform m_rtRoot;

		// Token: 0x04004CB1 RID: 19633
		public NKCDeckViewUnitSelectListSlot m_pfbUnitSlot;

		// Token: 0x04004CB2 RID: 19634
		public Vector2 m_vUnitSlotSize;

		// Token: 0x04004CB3 RID: 19635
		public Vector2 m_vUnitSlotSpacing;

		// Token: 0x04004CB4 RID: 19636
		public NKCUIShipSelectListSlot m_pfbShipSlot;

		// Token: 0x04004CB5 RID: 19637
		public Vector2 m_vShipSlotSize;

		// Token: 0x04004CB6 RID: 19638
		public Vector2 m_vShipSlotSpacing;

		// Token: 0x04004CB7 RID: 19639
		public NKCUIOperatorDeckSelectSlot m_pfbOperatorSlot;

		// Token: 0x04004CB8 RID: 19640
		public Vector2 m_vOperatorSlotSize;

		// Token: 0x04004CB9 RID: 19641
		public Vector2 m_vOperatorSlotSpacing;

		// Token: 0x04004CBA RID: 19642
		public LoopScrollRect m_LoopScrollRect;

		// Token: 0x04004CBB RID: 19643
		public GridLayoutGroup m_GridLayoutGroup;

		// Token: 0x04004CBC RID: 19644
		public RectTransform m_rectSlotPoolRect;

		// Token: 0x04004CBD RID: 19645
		[Header("정렬 관련 통합 UI")]
		public NKCUIComUnitSortOptions m_SortUI;

		// Token: 0x04004CBE RID: 19646
		[Header("보유 유닛 카운트")]
		public Text m_txtUnitCout;

		// Token: 0x04004CBF RID: 19647
		public NKCUIComStateButton m_sbtnUnitListAdd;

		// Token: 0x04004CC0 RID: 19648
		[Header("그 외")]
		public NKCUIComToggle m_tglHideDeckedUnit;

		// Token: 0x04004CC1 RID: 19649
		public Animator m_AniHideDeckedUnit;

		// Token: 0x04004CC2 RID: 19650
		public NKCUIComStateButton m_sbtnFinish;

		// Token: 0x04004CC3 RID: 19651
		private bool m_bOpen;

		// Token: 0x04004CC4 RID: 19652
		private NKCDeckViewUnitSelectList.OnDeckUnitChangeClicked dOnDeckUnitChangeClicked;

		// Token: 0x04004CC5 RID: 19653
		private NKCDeckViewUnitSelectList.OnClose dOnClose;

		// Token: 0x04004CC6 RID: 19654
		private UnityAction dOnClearDeck;

		// Token: 0x04004CC7 RID: 19655
		private UnityAction dOnAutoCompleteDeck;

		// Token: 0x04004CC9 RID: 19657
		private List<NKCUIUnitSelectListSlotBase> m_lstVisibleSlot = new List<NKCUIUnitSelectListSlotBase>();

		// Token: 0x04004CCA RID: 19658
		private Stack<NKCUIUnitSelectListSlotBase> m_stkUnitSlotPool = new Stack<NKCUIUnitSelectListSlotBase>();

		// Token: 0x04004CCB RID: 19659
		private Stack<NKCUIUnitSelectListSlotBase> m_stkShipSlotPool = new Stack<NKCUIUnitSelectListSlotBase>();

		// Token: 0x04004CCC RID: 19660
		private Stack<NKCUIUnitSelectListSlotBase> m_stkOperatorSlotPool = new Stack<NKCUIUnitSelectListSlotBase>();

		// Token: 0x04004CCD RID: 19661
		private NKCUnitSortSystem m_ssActive;

		// Token: 0x04004CCE RID: 19662
		private Dictionary<NKM_UNIT_TYPE, NKCUnitSortSystem> m_dicUnitSortSystem = new Dictionary<NKM_UNIT_TYPE, NKCUnitSortSystem>();

		// Token: 0x04004CCF RID: 19663
		private NKCUnitSortSystem.UnitListOptions m_sortOptions;

		// Token: 0x04004CD0 RID: 19664
		private NKCUIDeckViewer.DeckViewerOption m_DeckViewerOptions;

		// Token: 0x04004CD1 RID: 19665
		private NKCOperatorSortSystem m_OperatorSortSystem;

		// Token: 0x04004CD2 RID: 19666
		private NKCOperatorSortSystem.OperatorListOptions m_OperatorSortOptions;

		// Token: 0x020015F4 RID: 5620
		public enum SlotType
		{
			// Token: 0x0400A2C7 RID: 41671
			Normal,
			// Token: 0x0400A2C8 RID: 41672
			Empty,
			// Token: 0x0400A2C9 RID: 41673
			ClearAll,
			// Token: 0x0400A2CA RID: 41674
			AutoComplete
		}

		// Token: 0x020015F5 RID: 5621
		// (Invoke) Token: 0x0600AEC5 RID: 44741
		public delegate void OnDeckUnitChangeClicked(NKMDeckIndex deckIndex, long uid, NKM_UNIT_TYPE eType);

		// Token: 0x020015F6 RID: 5622
		// (Invoke) Token: 0x0600AEC9 RID: 44745
		public delegate void OnClose(NKM_UNIT_TYPE eType);
	}
}
