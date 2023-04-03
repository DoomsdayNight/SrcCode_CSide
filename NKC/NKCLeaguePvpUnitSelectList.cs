using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKC.UI;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007AD RID: 1965
	[RequireComponent(typeof(RectTransform))]
	public class NKCLeaguePvpUnitSelectList : MonoBehaviour
	{
		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x06004D6B RID: 19819 RVA: 0x00174E1C File Offset: 0x0017301C
		public bool IsOpen
		{
			get
			{
				return this.m_bOpen;
			}
		}

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x06004D6C RID: 19820 RVA: 0x00174E24 File Offset: 0x00173024
		// (set) Token: 0x06004D6D RID: 19821 RVA: 0x00174E2C File Offset: 0x0017302C
		public NKM_UNIT_TYPE CurrentTargetUnitType { get; private set; }

		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x06004D6E RID: 19822 RVA: 0x00174E35 File Offset: 0x00173035
		public NKCUnitSortSystem.UnitListOptions SortOptions
		{
			get
			{
				return this.m_sortOptions;
			}
		}

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x06004D6F RID: 19823 RVA: 0x00174E3D File Offset: 0x0017303D
		public NKCOperatorSortSystem.OperatorListOptions SortOperatorOptions
		{
			get
			{
				return this.m_OperatorSortOptions;
			}
		}

		// Token: 0x06004D70 RID: 19824 RVA: 0x00174E48 File Offset: 0x00173048
		public void Init(NKCUIDeckViewer deckView, NKCLeaguePvpUnitSelectList.OnDeckUnitChangeClicked onDeckUnitChangeClicked, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot, NKCLeaguePvpUnitSelectList.OnClose onClose)
		{
			this.m_banCandidateSearchString = "";
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
			this.dOnDeckUnitChangeClicked = onDeckUnitChangeClicked;
			this.dOnSelectThisSlot = onSelectThisSlot;
			this.dOnClose = onClose;
			NKCUtil.SetGameobjectActive(this.m_rectSlotPoolRect, false);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06004D71 RID: 19825 RVA: 0x00174FE8 File Offset: 0x001731E8
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

		// Token: 0x06004D72 RID: 19826 RVA: 0x001750BC File Offset: 0x001732BC
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

		// Token: 0x06004D73 RID: 19827 RVA: 0x00175130 File Offset: 0x00173330
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
			if (this.m_DeckViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.LeaguePvPMain)
			{
				this.ProvideSlotDataForLeaguePvp(component, idx);
				return;
			}
			this.ProvideSlotDataForLegueGlobalBan(component, idx);
		}

		// Token: 0x06004D74 RID: 19828 RVA: 0x001751B0 File Offset: 0x001733B0
		private void ProvideSlotDataForLeaguePvp(NKCUIUnitSelectListSlotBase slot, int idx)
		{
			if (this.CurrentTargetUnitType == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				if (this.m_OperatorSortSystem.SortedOperatorList.Count <= idx)
				{
					return;
				}
				NKMOperator nkmoperator = this.m_OperatorSortSystem.SortedOperatorList[idx];
				long num = nkmoperator.uid;
				if (slot.NKMOperatorData == null || slot.NKMOperatorData.uid != num)
				{
					NKMDeckIndex deckIndexCache = this.m_OperatorSortSystem.GetDeckIndexCache(num, true);
					slot.SetData(nkmoperator, deckIndexCache, true, new NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot(this.OnSlotSelected));
				}
				NKCUnitSortSystem.eUnitState unitSlotState = NKCLeaguePVPMgr.GetUnitSlotState(this.CurrentTargetUnitType, nkmoperator.id, false);
				if (unitSlotState != NKCUnitSortSystem.eUnitState.NONE)
				{
					slot.SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState.NONE);
					slot.SetSlotState(unitSlotState);
				}
			}
			else
			{
				if (this.m_ssActive.SortedUnitList.Count <= idx)
				{
					return;
				}
				NKMUnitData nkmunitData = this.m_ssActive.SortedUnitList[idx];
				long num = nkmunitData.m_UnitUID;
				if (slot.NKMUnitData == null || slot.NKMUnitData.m_UnitUID != num)
				{
					NKMDeckIndex deckIndexCacheByOption = this.m_ssActive.GetDeckIndexCacheByOption(num, true);
					slot.SetData(nkmunitData, deckIndexCacheByOption, true, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnSlotSelected));
				}
				NKCUnitSortSystem.eUnitState unitSlotState2 = NKCLeaguePVPMgr.GetUnitSlotState(this.CurrentTargetUnitType, nkmunitData.m_UnitID, false);
				if (unitSlotState2 != NKCUnitSortSystem.eUnitState.NONE)
				{
					slot.SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState.NONE);
					slot.SetSlotState(unitSlotState2);
				}
			}
			slot.SetEnableShowBan(false);
			slot.SetEnableShowUpUnit(false);
			slot.SetCityLeaderMark(false);
		}

		// Token: 0x06004D75 RID: 19829 RVA: 0x00175308 File Offset: 0x00173508
		private void ProvideSlotDataForLegueGlobalBan(NKCUIUnitSelectListSlotBase slot, int idx)
		{
			if (this.m_ssActive.SortedUnitList.Count <= idx)
			{
				return;
			}
			NKMUnitData nkmunitData = this.m_ssActive.SortedUnitList[idx];
			if (slot.NKMUnitData == null || slot.NKMUnitData.m_UnitID != nkmunitData.m_UnitID)
			{
				NKMUnitTempletBase templetBase = NKMUnitTempletBase.Find(nkmunitData.m_UnitID);
				slot.SetData(templetBase, 0, 0, false, this.dOnSelectThisSlot);
			}
			NKCUnitSortSystem.eUnitState unitSlotState = NKCLeaguePVPMgr.GetUnitSlotState(this.CurrentTargetUnitType, nkmunitData.m_UnitID, true);
			if (unitSlotState != NKCUnitSortSystem.eUnitState.NONE)
			{
				slot.SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState.DISABLE);
				slot.SetSlotState(unitSlotState);
			}
		}

		// Token: 0x06004D76 RID: 19830 RVA: 0x00175396 File Offset: 0x00173596
		public void UpdateSearchString(string searchString)
		{
			this.m_banCandidateSearchString = searchString;
			this.InvalidateSortData(NKM_UNIT_TYPE.NUT_NORMAL);
		}

		// Token: 0x06004D77 RID: 19831 RVA: 0x001753A8 File Offset: 0x001735A8
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

		// Token: 0x06004D78 RID: 19832 RVA: 0x00175420 File Offset: 0x00173620
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

		// Token: 0x06004D79 RID: 19833 RVA: 0x001754A8 File Offset: 0x001736A8
		private int GetSlotCount()
		{
			return this.m_ssActive.SortedUnitList.Count;
		}

		// Token: 0x06004D7A RID: 19834 RVA: 0x001754BC File Offset: 0x001736BC
		private NKCUnitSortSystem GetUnitSortSystem(NKM_UNIT_TYPE type)
		{
			if (this.m_dicUnitSortSystem.ContainsKey(type) && this.m_dicUnitSortSystem[type] != null)
			{
				return this.m_dicUnitSortSystem[type];
			}
			NKCUnitSortSystem nkcunitSortSystem;
			if (type == NKM_UNIT_TYPE.NUT_NORMAL || type != NKM_UNIT_TYPE.NUT_SHIP)
			{
				if (this.m_DeckViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.LeaguePvPGlobalBan)
				{
					this.UpdateGlobalBanUnitList();
					nkcunitSortSystem = new NKCGenericUnitSort(null, this.m_sortOptions, this.m_unitCandidateList);
				}
				else
				{
					this.UpdateLeaguePickUnitList();
					nkcunitSortSystem = new NKCGenericUnitSort(NKCScenManager.CurrentUserData(), this.m_sortOptions, this.m_unitCandidateList);
				}
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

		// Token: 0x06004D7B RID: 19835 RVA: 0x00175590 File Offset: 0x00173790
		private void UpdateGlobalBanUnitList()
		{
			this.m_unitCandidateList.Clear();
			foreach (NKMUnitTempletBase nkmunitTempletBase in NKMUnitTempletBase.Get_listNKMUnitTempletBaseForUnit())
			{
				if (nkmunitTempletBase.CollectionEnableByTag && nkmunitTempletBase.m_bContractable && NKMUnitManager.CanUnitUsedInDeck(nkmunitTempletBase) && nkmunitTempletBase.m_NKM_UNIT_GRADE >= NKM_UNIT_GRADE.NUG_SR && (string.IsNullOrEmpty(this.m_banCandidateSearchString) || nkmunitTempletBase.GetUnitName().Contains(this.m_banCandidateSearchString)))
				{
					this.m_unitCandidateList.Add(NKCUtil.MakeDummyUnit(nkmunitTempletBase.m_UnitID, true));
				}
			}
		}

		// Token: 0x06004D7C RID: 19836 RVA: 0x00175640 File Offset: 0x00173840
		private void UpdateLeaguePickUnitList()
		{
			this.m_unitCandidateList.Clear();
			foreach (NKMUnitData nkmunitData in NKCScenManager.CurrentUserData().m_ArmyData.m_dicMyUnit.Values)
			{
				NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(nkmunitData.m_UnitID);
				if (nkmunitTempletBase != null && nkmunitTempletBase.CollectionEnableByTag && nkmunitTempletBase.m_bContractable && NKMUnitManager.CanUnitUsedInDeck(nkmunitTempletBase))
				{
					this.m_unitCandidateList.Add(nkmunitData);
				}
			}
		}

		// Token: 0x06004D7D RID: 19837 RVA: 0x001756D8 File Offset: 0x001738D8
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

		// Token: 0x06004D7E RID: 19838 RVA: 0x00175710 File Offset: 0x00173910
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
			if (sortOptions.eDeckType != NKM_DECK_TYPE.NDT_NORMAL)
			{
				this.m_sortOptions.bHideDeckedUnit = false;
				this.m_OperatorSortOptions.SetBuildOption(false, new BUILD_OPTIONS[]
				{
					BUILD_OPTIONS.HIDE_DECKED_UNIT
				});
			}
			this.m_bOpen = true;
			this.m_sortOptions = sortOptions;
			this.m_DeckViewerOptions = deckViewerOption;
			this.RefreshLoopScrollList(targetType, true);
		}

		// Token: 0x06004D7F RID: 19839 RVA: 0x00175850 File Offset: 0x00173A50
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
			if (sortOptions.eDeckType != NKM_DECK_TYPE.NDT_NORMAL)
			{
				this.m_sortOptions.bHideDeckedUnit = false;
				this.m_OperatorSortOptions.SetBuildOption(false, new BUILD_OPTIONS[]
				{
					BUILD_OPTIONS.HIDE_DECKED_UNIT
				});
			}
			this.m_bOpen = true;
			this.m_OperatorSortOptions = sortOptions;
			this.m_DeckViewerOptions = deckViewerOption;
			this.RefreshLoopScrollList(targetType, true);
		}

		// Token: 0x06004D80 RID: 19840 RVA: 0x00175920 File Offset: 0x00173B20
		public void Close(bool bAnimate)
		{
			this.m_bOpen = false;
			this.Cleanup();
			NKCLeaguePvpUnitSelectList.OnClose onClose = this.dOnClose;
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

		// Token: 0x06004D81 RID: 19841 RVA: 0x001759D6 File Offset: 0x00173BD6
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

		// Token: 0x06004D82 RID: 19842 RVA: 0x00175A04 File Offset: 0x00173C04
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

		// Token: 0x06004D83 RID: 19843 RVA: 0x00175AEC File Offset: 0x00173CEC
		private void OnSortChanged(bool bResetScroll)
		{
			if (this.CurrentTargetUnitType == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				if (this.m_OperatorSortSystem != null)
				{
					this.m_OperatorSortOptions = this.m_OperatorSortSystem.Options;
					this.m_LoopScrollRect.TotalCount = this.m_OperatorSortSystem.SortedOperatorList.Count;
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
				this.m_LoopScrollRect.TotalCount = this.m_ssActive.SortedUnitList.Count;
				if (bResetScroll)
				{
					this.m_LoopScrollRect.SetIndexPosition(0);
					return;
				}
				this.m_LoopScrollRect.RefreshCells(false);
			}
		}

		// Token: 0x06004D84 RID: 19844 RVA: 0x00175BA6 File Offset: 0x00173DA6
		private void OnCloseBtn()
		{
			this.Close(true);
		}

		// Token: 0x06004D85 RID: 19845 RVA: 0x00175BB0 File Offset: 0x00173DB0
		public void UpdateLoopScrollList(NKM_UNIT_TYPE eType, NKCUnitSortSystem.UnitListOptions options)
		{
			if (!this.m_bOpen)
			{
				return;
			}
			if (this.CurrentTargetUnitType != eType)
			{
				this.m_sortOptions = options;
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

		// Token: 0x06004D86 RID: 19846 RVA: 0x00175C54 File Offset: 0x00173E54
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
			this.m_dicUnitSortSystem.Remove(eType);
			this.RefreshLoopScrollList(eType, false);
		}

		// Token: 0x06004D87 RID: 19847 RVA: 0x00175CC4 File Offset: 0x00173EC4
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
			if (this.CurrentTargetUnitType == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				this.m_OperatorSortSystem = new NKCOperatorSort(NKCScenManager.CurrentUserData(), this.m_OperatorSortOptions);
				if (this.m_SortUI != null)
				{
					this.m_SortUI.RegisterOperatorSort(this.m_OperatorSortSystem);
				}
			}
			else
			{
				this.m_ssActive = this.GetUnitSortSystem(targetType);
				if (this.m_SortUI != null)
				{
					this.m_SortUI.RegisterUnitSort(this.m_ssActive);
				}
			}
			if (flag)
			{
				if (this.m_SortUI != null)
				{
					this.m_SortUI.RegisterCategories(this.GetFilterCategorySet(), this.GetSortCategorySet(), false);
					bool bUseFavorite = this.m_DeckViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.LeaguePvPMain;
					this.m_SortUI.ResetUI(bUseFavorite);
				}
				this.m_sortOptions.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
				this.m_OperatorSortOptions.setFilterOption = new HashSet<NKCOperatorSortSystem.eFilterOption>();
				switch (targetType)
				{
				case NKM_UNIT_TYPE.NUT_NORMAL:
					this.m_LoopScrollRect.ContentConstraintCount = this.GetContentContraintCount();
					this.m_GridLayoutGroup.constraintCount = this.GetContentContraintCount();
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

		// Token: 0x06004D88 RID: 19848 RVA: 0x00175F57 File Offset: 0x00174157
		private int GetContentContraintCount()
		{
			if (this.m_DeckViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.LeaguePvPMain)
			{
				return 3;
			}
			return 7;
		}

		// Token: 0x06004D89 RID: 19849 RVA: 0x00175F6C File Offset: 0x0017416C
		private HashSet<NKCUnitSortSystem.eFilterCategory> GetFilterCategorySet()
		{
			switch (this.CurrentTargetUnitType)
			{
			case NKM_UNIT_TYPE.NUT_NORMAL:
				return NKCPopupFilterUnit.MakeDefaultFilterOption(NKM_UNIT_TYPE.NUT_NORMAL, NKCPopupFilterUnit.FILTER_OPEN_TYPE.NORMAL);
			case NKM_UNIT_TYPE.NUT_SHIP:
				return NKCPopupFilterUnit.MakeDefaultFilterOption(NKM_UNIT_TYPE.NUT_SHIP, NKCPopupFilterUnit.FILTER_OPEN_TYPE.NORMAL);
			case NKM_UNIT_TYPE.NUT_OPERATOR:
				return NKCPopupFilterUnit.MakeDefaultFilterOption(NKM_UNIT_TYPE.NUT_OPERATOR, NKCPopupFilterUnit.FILTER_OPEN_TYPE.NORMAL);
			default:
				Debug.LogError(string.Format("여기서 {0} 을 사용하는게 맞는지 확인 필요함. 사용할 경우 코드 추가", this.CurrentTargetUnitType));
				return new HashSet<NKCUnitSortSystem.eFilterCategory>();
			}
		}

		// Token: 0x06004D8A RID: 19850 RVA: 0x00175FD0 File Offset: 0x001741D0
		private HashSet<NKCUnitSortSystem.eSortCategory> GetSortCategorySet()
		{
			switch (this.CurrentTargetUnitType)
			{
			case NKM_UNIT_TYPE.NUT_NORMAL:
				return NKCPopupSort.MakeDefaultSortSet(NKM_UNIT_TYPE.NUT_NORMAL, false);
			case NKM_UNIT_TYPE.NUT_SHIP:
				return NKCPopupSort.MakeDefaultSortSet(NKM_UNIT_TYPE.NUT_SHIP, false);
			case NKM_UNIT_TYPE.NUT_OPERATOR:
				return NKCPopupSort.MakeDefaultSortSet(NKM_UNIT_TYPE.NUT_OPERATOR, false);
			default:
				return new HashSet<NKCUnitSortSystem.eSortCategory>();
			}
		}

		// Token: 0x06004D8B RID: 19851 RVA: 0x00176018 File Offset: 0x00174218
		private void OnSlotSelected(NKMUnitData selectedUnit, NKMUnitTempletBase unitTempletBase, NKMDeckIndex selectedUnitDeckIndex, NKCUnitSortSystem.eUnitState unitSlotState, NKCUIUnitSelectList.eUnitSlotSelectState unitSlotSelectState)
		{
			long targetUID = (selectedUnit != null) ? selectedUnit.m_UnitUID : 0L;
			this.OnSlotSelected(targetUID, unitTempletBase, selectedUnitDeckIndex, unitSlotState, unitSlotSelectState);
		}

		// Token: 0x06004D8C RID: 19852 RVA: 0x00176040 File Offset: 0x00174240
		private void OnSlotSelected(NKMOperator selectedOperator, NKMUnitTempletBase unitTempletBase, NKMDeckIndex selectedUnitDeckIndex, NKCUnitSortSystem.eUnitState unitSlotState, NKCUIUnitSelectList.eUnitSlotSelectState unitSlotSelectState)
		{
			long targetUID = (selectedOperator != null) ? selectedOperator.uid : 0L;
			this.OnSlotSelected(targetUID, unitTempletBase, selectedUnitDeckIndex, unitSlotState, unitSlotSelectState);
		}

		// Token: 0x06004D8D RID: 19853 RVA: 0x00176068 File Offset: 0x00174268
		private void OnSlotSelected(long targetUID, NKMUnitTempletBase unitTempletBase, NKMDeckIndex selectedUnitDeckIndex, NKCUnitSortSystem.eUnitState unitSlotState, NKCUIUnitSelectList.eUnitSlotSelectState unitSlotSelectState)
		{
			switch (unitSlotState)
			{
			case NKCUnitSortSystem.eUnitState.NONE:
			case NKCUnitSortSystem.eUnitState.SEIZURE:
			case NKCUnitSortSystem.eUnitState.LOBBY_UNIT:
				if (this.m_sortOptions.eDeckType == NKM_DECK_TYPE.NDT_NORMAL && ((this.CurrentTargetUnitType == NKM_UNIT_TYPE.NUT_NORMAL && this.m_ssActive.GetDeckIndexCache(targetUID, true) != NKMDeckIndex.None) || (this.CurrentTargetUnitType == NKM_UNIT_TYPE.NUT_OPERATOR && this.m_OperatorSortSystem.GetDeckIndexCache(targetUID, true) != NKMDeckIndex.None)))
				{
					this.OpenUnitDeckChangeWarning(targetUID);
					return;
				}
				this.ConfirmSlotSelected(targetUID);
				return;
			default:
				return;
			}
		}

		// Token: 0x06004D8E RID: 19854 RVA: 0x00176120 File Offset: 0x00174320
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
			NKCLeaguePvpUnitSelectList.OnDeckUnitChangeClicked onDeckUnitChangeClicked = this.dOnDeckUnitChangeClicked;
			if (onDeckUnitChangeClicked == null)
			{
				return;
			}
			onDeckUnitChangeClicked(deckIndexCache, UID, this.CurrentTargetUnitType);
		}

		// Token: 0x06004D8F RID: 19855 RVA: 0x0017616C File Offset: 0x0017436C
		public NKCUIUnitSelectListSlotBase FindSlotFromCurrentList(NKM_UNIT_TYPE unitType, long unitUID)
		{
			if (unitType == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				return this.m_lstVisibleSlot.Find((NKCUIUnitSelectListSlotBase x) => x.NKMOperatorData != null && x.NKMOperatorData.uid == unitUID);
			}
			return this.m_lstVisibleSlot.Find((NKCUIUnitSelectListSlotBase x) => x.NKMUnitData != null && x.NKMUnitData.m_UnitUID == unitUID);
		}

		// Token: 0x06004D90 RID: 19856 RVA: 0x001761BC File Offset: 0x001743BC
		public NKCUIUnitSelectListSlotBase FindSlotFromCurrentList(int unitID)
		{
			return this.m_lstVisibleSlot.Find((NKCUIUnitSelectListSlotBase x) => x.NKMUnitTempletBase != null && x.NKMUnitTempletBase.m_UnitID == unitID);
		}

		// Token: 0x06004D91 RID: 19857 RVA: 0x001761F0 File Offset: 0x001743F0
		private void OpenUnitDeckChangeWarning(long UID)
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_DECK_CHANGE_UNIT_WARNING, delegate()
			{
				this.ConfirmSlotSelected(UID);
			}, null, false);
		}

		// Token: 0x04003D4C RID: 15692
		private RectTransform m_rtRoot;

		// Token: 0x04003D4D RID: 15693
		public NKCDeckViewUnitSelectListSlot m_pfbUnitSlot;

		// Token: 0x04003D4E RID: 15694
		public Vector2 m_vUnitSlotSize;

		// Token: 0x04003D4F RID: 15695
		public Vector2 m_vUnitSlotSpacing;

		// Token: 0x04003D50 RID: 15696
		public NKCUIShipSelectListSlot m_pfbShipSlot;

		// Token: 0x04003D51 RID: 15697
		public Vector2 m_vShipSlotSize;

		// Token: 0x04003D52 RID: 15698
		public Vector2 m_vShipSlotSpacing;

		// Token: 0x04003D53 RID: 15699
		public NKCUIOperatorDeckSelectSlot m_pfbOperatorSlot;

		// Token: 0x04003D54 RID: 15700
		public Vector2 m_vOperatorSlotSize;

		// Token: 0x04003D55 RID: 15701
		public Vector2 m_vOperatorSlotSpacing;

		// Token: 0x04003D56 RID: 15702
		public LoopScrollRect m_LoopScrollRect;

		// Token: 0x04003D57 RID: 15703
		public GridLayoutGroup m_GridLayoutGroup;

		// Token: 0x04003D58 RID: 15704
		public RectTransform m_rectSlotPoolRect;

		// Token: 0x04003D59 RID: 15705
		[Header("정렬 관련 통합 UI")]
		public NKCUIComUnitSortOptions m_SortUI;

		// Token: 0x04003D5A RID: 15706
		[Header("그 외")]
		public NKCUIComStateButton m_sbtnFinish;

		// Token: 0x04003D5B RID: 15707
		private bool m_bOpen;

		// Token: 0x04003D5C RID: 15708
		private NKCLeaguePvpUnitSelectList.OnDeckUnitChangeClicked dOnDeckUnitChangeClicked;

		// Token: 0x04003D5D RID: 15709
		private NKCUIUnitSelectListSlotBase.OnSelectThisSlot dOnSelectThisSlot;

		// Token: 0x04003D5E RID: 15710
		private NKCLeaguePvpUnitSelectList.OnClose dOnClose;

		// Token: 0x04003D5F RID: 15711
		private UnityAction dOnClearDeck;

		// Token: 0x04003D60 RID: 15712
		private UnityAction dOnAutoCompleteDeck;

		// Token: 0x04003D62 RID: 15714
		private List<NKCUIUnitSelectListSlotBase> m_lstVisibleSlot = new List<NKCUIUnitSelectListSlotBase>();

		// Token: 0x04003D63 RID: 15715
		private Stack<NKCUIUnitSelectListSlotBase> m_stkUnitSlotPool = new Stack<NKCUIUnitSelectListSlotBase>();

		// Token: 0x04003D64 RID: 15716
		private Stack<NKCUIUnitSelectListSlotBase> m_stkShipSlotPool = new Stack<NKCUIUnitSelectListSlotBase>();

		// Token: 0x04003D65 RID: 15717
		private Stack<NKCUIUnitSelectListSlotBase> m_stkOperatorSlotPool = new Stack<NKCUIUnitSelectListSlotBase>();

		// Token: 0x04003D66 RID: 15718
		private NKCUnitSortSystem m_ssActive;

		// Token: 0x04003D67 RID: 15719
		private Dictionary<NKM_UNIT_TYPE, NKCUnitSortSystem> m_dicUnitSortSystem = new Dictionary<NKM_UNIT_TYPE, NKCUnitSortSystem>();

		// Token: 0x04003D68 RID: 15720
		private NKCUnitSortSystem.UnitListOptions m_sortOptions;

		// Token: 0x04003D69 RID: 15721
		private NKCUIDeckViewer.DeckViewerOption m_DeckViewerOptions;

		// Token: 0x04003D6A RID: 15722
		private NKCOperatorSortSystem m_OperatorSortSystem;

		// Token: 0x04003D6B RID: 15723
		private NKCOperatorSortSystem.OperatorListOptions m_OperatorSortOptions;

		// Token: 0x04003D6C RID: 15724
		private List<NKMUnitData> m_unitCandidateList = new List<NKMUnitData>();

		// Token: 0x04003D6D RID: 15725
		private string m_banCandidateSearchString = "";

		// Token: 0x0200146A RID: 5226
		// (Invoke) Token: 0x0600A8B4 RID: 43188
		public delegate void OnDeckUnitChangeClicked(NKMDeckIndex deckIndex, long uid, NKM_UNIT_TYPE eType);

		// Token: 0x0200146B RID: 5227
		// (Invoke) Token: 0x0600A8B8 RID: 43192
		public delegate void OnClose(NKM_UNIT_TYPE eType);
	}
}
