using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009D5 RID: 2517
	public class NKCUISelection : NKCUIBase
	{
		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x06006BCB RID: 27595 RVA: 0x00232B88 File Offset: 0x00230D88
		public static NKCUISelection Instance
		{
			get
			{
				if (NKCUISelection.m_Instance == null)
				{
					NKCUISelection.m_Instance = NKCUIManager.OpenNewInstance<NKCUISelection>("AB_UI_NKM_UI_UNIT_SELECTION", "NKM_UI_UNIT_SELECTION", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUISelection.CleanupInstance)).GetInstance<NKCUISelection>();
					NKCUISelection.m_Instance.InitUI();
				}
				return NKCUISelection.m_Instance;
			}
		}

		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x06006BCC RID: 27596 RVA: 0x00232BD7 File Offset: 0x00230DD7
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUISelection.m_Instance != null && NKCUISelection.m_Instance.IsOpen;
			}
		}

		// Token: 0x06006BCD RID: 27597 RVA: 0x00232BF2 File Offset: 0x00230DF2
		public static void CheckInstanceAndClose()
		{
			if (NKCUISelection.m_Instance != null && NKCUISelection.m_Instance.IsOpen)
			{
				NKCUISelection.m_Instance.Close();
			}
		}

		// Token: 0x06006BCE RID: 27598 RVA: 0x00232C17 File Offset: 0x00230E17
		private static void CleanupInstance()
		{
			NKCUISelection.m_Instance = null;
		}

		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x06006BCF RID: 27599 RVA: 0x00232C1F File Offset: 0x00230E1F
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x06006BD0 RID: 27600 RVA: 0x00232C24 File Offset: 0x00230E24
		public override string MenuName
		{
			get
			{
				NKM_ITEM_MISC_TYPE nkm_ITEM_MISC_TYPE = this.m_NKM_ITEM_MISC_TYPE;
				if (nkm_ITEM_MISC_TYPE == NKM_ITEM_MISC_TYPE.IMT_CHOICE_UNIT)
				{
					return NKCUtilString.GET_STRING_CHOICE_UNIT;
				}
				if (nkm_ITEM_MISC_TYPE != NKM_ITEM_MISC_TYPE.IMT_CHOICE_SHIP)
				{
					return NKCUtilString.GET_STRING_USE_CHOICE;
				}
				return NKCUtilString.GET_STRING_CHOICE_SHIP;
			}
		}

		// Token: 0x06006BD1 RID: 27601 RVA: 0x00232C54 File Offset: 0x00230E54
		private void InitUI()
		{
			this.m_loopScrollRectUnit.dOnGetObject += this.GetObject;
			this.m_loopScrollRectUnit.dOnReturnObject += this.ReturnObject;
			this.m_loopScrollRectUnit.dOnProvideData += this.ProvideData;
			this.m_loopScrollRectUnit.dOnRepopulate += this.CalculateContentRectSize;
			NKCUtil.SetScrollHotKey(this.m_loopScrollRectUnit, null);
			this.m_loopScrollRectShip.dOnGetObject += this.GetObject;
			this.m_loopScrollRectShip.dOnReturnObject += this.ReturnObject;
			this.m_loopScrollRectShip.dOnProvideData += this.ProvideData;
			this.m_loopScrollRectShip.dOnRepopulate += this.CalculateContentRectSize;
			NKCUtil.SetScrollHotKey(this.m_loopScrollRectShip, null);
			if (this.m_SortUI != null)
			{
				this.m_SortUI.Init(new NKCUIComUnitSortOptions.OnSorted(this.OnSortChanged), false);
				if (this.m_SortUI.m_NKCPopupSort != null)
				{
					this.m_SortUI.m_NKCPopupSort.m_bUseDefaultSortAdd = false;
				}
			}
		}

		// Token: 0x06006BD2 RID: 27602 RVA: 0x00232D7B File Offset: 0x00230F7B
		public override void CloseInternal()
		{
			this.m_lstRewardId = new List<int>();
			this.m_SortUI.ResetUI(false);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06006BD3 RID: 27603 RVA: 0x00232DA0 File Offset: 0x00230FA0
		public override void OnCloseInstance()
		{
			this.m_ssActive = null;
			this.m_ssActive = null;
		}

		// Token: 0x06006BD4 RID: 27604 RVA: 0x00232DB0 File Offset: 0x00230FB0
		public void Open(NKMItemMiscTemplet itemMiscTemplet)
		{
			if (itemMiscTemplet == null)
			{
				return;
			}
			this.m_NKMItemMiscTemplet = itemMiscTemplet;
			List<NKMRandomBoxItemTemplet> randomBoxItemTempletList = NKCRandomBoxManager.GetRandomBoxItemTempletList(itemMiscTemplet.m_RewardGroupID);
			if (randomBoxItemTempletList == null)
			{
				return;
			}
			for (int i = 0; i < randomBoxItemTempletList.Count; i++)
			{
				this.m_lstRewardId.Add(randomBoxItemTempletList[i].m_RewardID);
			}
			this.m_NKM_ITEM_MISC_TYPE = itemMiscTemplet.m_ItemMiscType;
			NKCUtil.SetGameobjectActive(this.m_objUnitChoice, this.m_NKM_ITEM_MISC_TYPE == NKM_ITEM_MISC_TYPE.IMT_CHOICE_UNIT);
			NKCUtil.SetGameobjectActive(this.m_objShipChoice, this.m_NKM_ITEM_MISC_TYPE == NKM_ITEM_MISC_TYPE.IMT_CHOICE_SHIP);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.CalculateContentRectSize();
			NKM_ITEM_MISC_TYPE nkm_ITEM_MISC_TYPE = this.m_NKM_ITEM_MISC_TYPE;
			if (nkm_ITEM_MISC_TYPE != NKM_ITEM_MISC_TYPE.IMT_CHOICE_UNIT)
			{
				if (nkm_ITEM_MISC_TYPE == NKM_ITEM_MISC_TYPE.IMT_CHOICE_SHIP)
				{
					this.SetShipChoiceList();
					Sprite titleSprite = this.GetTitleSprite(itemMiscTemplet.m_BannerImage);
					NKCUtil.SetImageSprite(this.m_imgBannerShip, titleSprite, false);
				}
			}
			else
			{
				this.SetUnitChoiceList();
				Sprite titleSprite2 = this.GetTitleSprite(itemMiscTemplet.m_BannerImage);
				NKCUtil.SetImageSprite(this.m_imgBannerUnit, titleSprite2, false);
			}
			base.UIOpened(true);
		}

		// Token: 0x06006BD5 RID: 27605 RVA: 0x00232EA4 File Offset: 0x002310A4
		private Sprite GetTitleSprite(string imageAsset)
		{
			Sprite sprite = null;
			if (!string.IsNullOrEmpty(imageAsset))
			{
				sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(NKMAssetName.ParseBundleName("AB_UI_NKM_UI_UNIT_SELECTION_TEXTURE", imageAsset));
			}
			if (sprite == null)
			{
				sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_UNIT_SELECTION_TEXTURE", "NKM_UI_UNIT_SELECTION_UNIT", false);
			}
			return sprite;
		}

		// Token: 0x06006BD6 RID: 27606 RVA: 0x00232EE8 File Offset: 0x002310E8
		private void CalculateContentRectSize()
		{
			NKM_ITEM_MISC_TYPE nkm_ITEM_MISC_TYPE = this.m_NKM_ITEM_MISC_TYPE;
			NKCUIComSafeArea safeArea = this.m_SafeArea;
			if (safeArea != null)
			{
				safeArea.SetSafeAreaBase();
			}
			Vector2 cellSize = Vector2.zero;
			Vector2 spacing = Vector2.zero;
			LoopScrollRect loopScrollRect;
			GridLayoutGroup component;
			int minColumn;
			if (nkm_ITEM_MISC_TYPE != NKM_ITEM_MISC_TYPE.IMT_CHOICE_UNIT)
			{
				if (nkm_ITEM_MISC_TYPE != NKM_ITEM_MISC_TYPE.IMT_CHOICE_SHIP)
				{
					return;
				}
				loopScrollRect = this.m_loopScrollRectShip;
				component = this.m_trContentParentShip.GetComponent<GridLayoutGroup>();
				minColumn = 2;
				cellSize = this.SHIP_SELECTION_CELL_SIZE;
				spacing = component.spacing;
			}
			else
			{
				loopScrollRect = this.m_loopScrollRectUnit;
				component = this.m_trContentParentUnit.GetComponent<GridLayoutGroup>();
				minColumn = 4;
				cellSize = component.cellSize;
				spacing = component.spacing;
			}
			NKCUtil.CalculateContentRectSize(loopScrollRect, component, minColumn, cellSize, spacing, nkm_ITEM_MISC_TYPE == NKM_ITEM_MISC_TYPE.IMT_CHOICE_SHIP);
		}

		// Token: 0x06006BD7 RID: 27607 RVA: 0x00232F8C File Offset: 0x0023118C
		private RectTransform GetObject(int index)
		{
			NKM_ITEM_MISC_TYPE nkm_ITEM_MISC_TYPE = this.m_NKM_ITEM_MISC_TYPE;
			if (nkm_ITEM_MISC_TYPE - NKM_ITEM_MISC_TYPE.IMT_CHOICE_UNIT <= 1)
			{
				NKM_ITEM_MISC_TYPE nkm_ITEM_MISC_TYPE2 = this.m_NKM_ITEM_MISC_TYPE;
				Stack<NKCUIUnitSelectListSlotBase> stack;
				NKCUIUnitSelectListSlotBase original;
				if (nkm_ITEM_MISC_TYPE2 != NKM_ITEM_MISC_TYPE.IMT_CHOICE_UNIT)
				{
					if (nkm_ITEM_MISC_TYPE2 != NKM_ITEM_MISC_TYPE.IMT_CHOICE_SHIP)
					{
						return null;
					}
					stack = this.m_stkShipSlotPool;
					original = this.m_pfbShipSlot;
				}
				else
				{
					stack = this.m_stkUnitSlotPool;
					original = this.m_pfbUnitSlot;
				}
				NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase;
				if (stack.Count > 0)
				{
					nkcuiunitSelectListSlotBase = stack.Pop();
				}
				else
				{
					nkcuiunitSelectListSlotBase = UnityEngine.Object.Instantiate<NKCUIUnitSelectListSlotBase>(original);
					nkcuiunitSelectListSlotBase.Init(false);
				}
				NKCUtil.SetGameobjectActive(nkcuiunitSelectListSlotBase, true);
				nkcuiunitSelectListSlotBase.transform.localScale = Vector3.one;
				this.m_lstVisibleSlot.Add(nkcuiunitSelectListSlotBase);
				return nkcuiunitSelectListSlotBase.GetComponent<RectTransform>();
			}
			return null;
		}

		// Token: 0x06006BD8 RID: 27608 RVA: 0x0023302C File Offset: 0x0023122C
		private void ReturnObject(Transform go)
		{
			NKCUIUnitSelectListSlotBase component = go.GetComponent<NKCUIUnitSelectListSlotBase>();
			if (component != null)
			{
				NKCUtil.SetGameobjectActive(component, false);
				go.SetParent(base.transform);
				this.m_lstVisibleSlot.Remove(component);
				if (component is NKCUIUnitSelectListSlot)
				{
					this.m_stkUnitSlotPool.Push(component);
					return;
				}
				if (component is NKCUIShipSelectListSlot)
				{
					this.m_stkShipSlotPool.Push(component);
				}
			}
		}

		// Token: 0x06006BD9 RID: 27609 RVA: 0x00233094 File Offset: 0x00231294
		private void ProvideData(Transform tr, int idx)
		{
			NKM_ITEM_MISC_TYPE nkm_ITEM_MISC_TYPE = this.m_NKM_ITEM_MISC_TYPE;
			if (nkm_ITEM_MISC_TYPE != NKM_ITEM_MISC_TYPE.IMT_CHOICE_UNIT)
			{
				if (nkm_ITEM_MISC_TYPE != NKM_ITEM_MISC_TYPE.IMT_CHOICE_SHIP)
				{
					return;
				}
				if (this.m_ssActive == null)
				{
					Debug.LogError("Slot Sort System Null!!");
					return;
				}
				NKCUIUnitSelectListSlotBase component = tr.GetComponent<NKCUIUnitSelectListSlotBase>();
				if (component == null)
				{
					return;
				}
				if (this.m_ssActive.SortedUnitList.Count <= idx)
				{
					return;
				}
				NKCUtil.SetGameobjectActive(component, true);
				NKMUnitData unitData = this.m_ssActive.SortedUnitList[idx];
				this.SetShipSlotData(component, unitData);
				return;
			}
			else
			{
				if (this.m_ssActive == null)
				{
					Debug.LogError("Slot Sort System Null!!");
					return;
				}
				NKCUIUnitSelectListSlotBase component2 = tr.GetComponent<NKCUIUnitSelectListSlotBase>();
				if (component2 == null)
				{
					return;
				}
				if (this.m_ssActive.SortedUnitList.Count <= idx)
				{
					return;
				}
				NKCUtil.SetGameobjectActive(component2, true);
				NKMUnitData unitData2 = this.m_ssActive.SortedUnitList[idx];
				this.SetUnitSlotData(component2, unitData2);
				return;
			}
		}

		// Token: 0x06006BDA RID: 27610 RVA: 0x00233168 File Offset: 0x00231368
		private void SetUnitChoiceList()
		{
			NKCUnitSortSystem.UnitListOptions unitListOptions = default(NKCUnitSortSystem.UnitListOptions);
			unitListOptions.setOnlyIncludeUnitID = new HashSet<int>();
			unitListOptions.eDeckType = NKM_DECK_TYPE.NDT_NORMAL;
			unitListOptions.lstSortOption = NKCUnitSortSystem.GetDefaultSortOptions(NKM_UNIT_TYPE.NUT_NORMAL, true, true);
			unitListOptions.bIncludeUndeckableUnit = true;
			List<NKMUnitData> list = new List<NKMUnitData>();
			for (int i = 0; i < this.m_lstRewardId.Count; i++)
			{
				NKMUnitData item = NKMDungeonManager.MakeUnitDataFromID(this.m_lstRewardId[i], (long)this.m_lstRewardId[i], 1, 0, 0, 0);
				list.Add(item);
				if (!unitListOptions.setOnlyIncludeUnitID.Contains(this.m_lstRewardId[i]))
				{
					unitListOptions.setOnlyIncludeUnitID.Add(this.m_lstRewardId[i]);
				}
			}
			this.m_ssActive = new NKCGenericUnitSort(NKCScenManager.CurrentUserData(), unitListOptions, list);
			this.m_SortUI.RegisterCategories(this.m_setUnitFilterCategory, this.m_setUnitSortCategory, false);
			this.m_SortUI.RegisterUnitSort(this.m_ssActive);
			this.m_SortUI.ResetUI(false);
			this.m_loopScrollRectUnit.PrepareCells(0);
			this.m_loopScrollRectUnit.TotalCount = list.Count;
			this.m_loopScrollRectUnit.RefreshCells(true);
		}

		// Token: 0x06006BDB RID: 27611 RVA: 0x00233290 File Offset: 0x00231490
		private void SetUnitSlotData(NKCUIUnitSelectListSlotBase slot, NKMUnitData unitData)
		{
			long unitUID = unitData.m_UnitUID;
			NKMDeckIndex deckIndexCache = this.m_ssActive.GetDeckIndexCache(unitUID, true);
			slot.SetData(unitData, deckIndexCache, true, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnSelectUnitSlot));
			slot.SetLock(unitData.m_bLock, false);
			slot.SetFavorite(unitData);
			slot.SetSlotState(NKCUnitSortSystem.eUnitState.NONE);
			if (this.m_ssActive.Options.lstSortOption.Count > 0)
			{
				switch (this.m_ssActive.Options.lstSortOption[0])
				{
				case NKCUnitSortSystem.eSortOption.Power_Low:
				case NKCUnitSortSystem.eSortOption.Power_High:
					slot.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Power_High, this.m_ssActive.GetUnitPowerCache(unitUID));
					goto IL_1B2;
				case NKCUnitSortSystem.eSortOption.Attack_Low:
				case NKCUnitSortSystem.eSortOption.Attack_High:
					slot.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Attack_High, this.m_ssActive.GetUnitAttackCache(unitUID));
					goto IL_1B2;
				case NKCUnitSortSystem.eSortOption.Health_Low:
				case NKCUnitSortSystem.eSortOption.Health_High:
					slot.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Health_High, this.m_ssActive.GetUnitHPCache(unitUID));
					goto IL_1B2;
				case NKCUnitSortSystem.eSortOption.Unit_Defense_Low:
				case NKCUnitSortSystem.eSortOption.Unit_Defense_High:
					slot.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Unit_Defense_High, this.m_ssActive.GetUnitDefCache(unitUID));
					goto IL_1B2;
				case NKCUnitSortSystem.eSortOption.Unit_Crit_Low:
				case NKCUnitSortSystem.eSortOption.Unit_Crit_High:
					slot.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Unit_Crit_High, this.m_ssActive.GetUnitCritCache(unitUID));
					goto IL_1B2;
				case NKCUnitSortSystem.eSortOption.Unit_Hit_Low:
				case NKCUnitSortSystem.eSortOption.Unit_Hit_High:
					slot.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Unit_Hit_High, this.m_ssActive.GetUnitHitCache(unitUID));
					goto IL_1B2;
				case NKCUnitSortSystem.eSortOption.Unit_Evade_Low:
				case NKCUnitSortSystem.eSortOption.Unit_Evade_High:
					slot.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Unit_Evade_High, this.m_ssActive.GetUnitEvadeCache(unitUID));
					goto IL_1B2;
				case NKCUnitSortSystem.eSortOption.Unit_ReduceSkillCool_Low:
				case NKCUnitSortSystem.eSortOption.Unit_ReduceSkillCool_High:
					slot.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Unit_ReduceSkillCool_High, this.m_ssActive.GetUnitSkillCoolCache(unitUID));
					goto IL_1B2;
				}
				slot.SetSortingTypeValue(false, NKCOperatorSortSystem.eSortOption.Level_High, 0);
			}
			else
			{
				slot.SetSortingTypeValue(false, NKCOperatorSortSystem.eSortOption.Level_High, 0);
			}
			IL_1B2:
			NKCUIUnitSelectList.eUnitSlotSelectState slotSelectState = NKCUIUnitSelectList.eUnitSlotSelectState.NONE;
			slot.SetSlotSelectState(slotSelectState);
			slot.SetCityLeaderMark(false);
			int unitCountByID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitCountByID(unitData.m_UnitID);
			slot.SetHaveCount(unitCountByID, true);
		}

		// Token: 0x06006BDC RID: 27612 RVA: 0x00233480 File Offset: 0x00231680
		private void SetShipChoiceList()
		{
			NKCUnitSortSystem.UnitListOptions unitListOptions = default(NKCUnitSortSystem.UnitListOptions);
			unitListOptions.setOnlyIncludeUnitID = new HashSet<int>();
			unitListOptions.eDeckType = NKM_DECK_TYPE.NDT_NORMAL;
			unitListOptions.lstSortOption = NKCUnitSortSystem.GetDefaultSortOptions(NKM_UNIT_TYPE.NUT_SHIP, true, true);
			List<NKMUnitData> list = new List<NKMUnitData>();
			for (int i = 0; i < this.m_lstRewardId.Count; i++)
			{
				NKMUnitData item = NKMDungeonManager.MakeUnitDataFromID(this.m_lstRewardId[i], (long)this.m_lstRewardId[i], 1, 0, 0, 0);
				list.Add(item);
				if (!unitListOptions.setOnlyIncludeUnitID.Contains(this.m_lstRewardId[i]))
				{
					unitListOptions.setOnlyIncludeUnitID.Add(this.m_lstRewardId[i]);
				}
			}
			this.m_ssActive = new NKCGenericUnitSort(NKCScenManager.CurrentUserData(), unitListOptions, list);
			this.m_SortUI.RegisterCategories(this.m_setShipFilterCategory, this.m_setShipSortCategory, false);
			this.m_SortUI.RegisterUnitSort(this.m_ssActive);
			this.m_SortUI.ResetUI(false);
			this.m_loopScrollRectShip.PrepareCells(0);
			this.m_loopScrollRectShip.TotalCount = list.Count;
			this.m_loopScrollRectShip.RefreshCells(true);
		}

		// Token: 0x06006BDD RID: 27613 RVA: 0x002335A0 File Offset: 0x002317A0
		private void SetShipSlotData(NKCUIUnitSelectListSlotBase slot, NKMUnitData unitData)
		{
			long unitUID = unitData.m_UnitUID;
			NKMDeckIndex deckIndexCache = this.m_ssActive.GetDeckIndexCache(unitUID, true);
			slot.SetData(unitData, deckIndexCache, true, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnSelectUnitSlot));
			slot.SetLock(unitData.m_bLock, false);
			slot.SetFavorite(unitData);
			if (this.m_ssActive.Options.lstSortOption.Count > 0)
			{
				switch (this.m_ssActive.Options.lstSortOption[0])
				{
				case NKCUnitSortSystem.eSortOption.Power_Low:
				case NKCUnitSortSystem.eSortOption.Power_High:
					slot.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Power_High, this.m_ssActive.GetUnitPowerCache(unitUID));
					goto IL_1AB;
				case NKCUnitSortSystem.eSortOption.Attack_Low:
				case NKCUnitSortSystem.eSortOption.Attack_High:
					slot.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Attack_High, this.m_ssActive.GetUnitAttackCache(unitUID));
					goto IL_1AB;
				case NKCUnitSortSystem.eSortOption.Health_Low:
				case NKCUnitSortSystem.eSortOption.Health_High:
					slot.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Health_High, this.m_ssActive.GetUnitHPCache(unitUID));
					goto IL_1AB;
				case NKCUnitSortSystem.eSortOption.Unit_Defense_Low:
				case NKCUnitSortSystem.eSortOption.Unit_Defense_High:
					slot.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Unit_Defense_High, this.m_ssActive.GetUnitDefCache(unitUID));
					goto IL_1AB;
				case NKCUnitSortSystem.eSortOption.Unit_Crit_Low:
				case NKCUnitSortSystem.eSortOption.Unit_Crit_High:
					slot.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Unit_Crit_High, this.m_ssActive.GetUnitCritCache(unitUID));
					goto IL_1AB;
				case NKCUnitSortSystem.eSortOption.Unit_Hit_Low:
				case NKCUnitSortSystem.eSortOption.Unit_Hit_High:
					slot.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Unit_Hit_High, this.m_ssActive.GetUnitHitCache(unitUID));
					goto IL_1AB;
				case NKCUnitSortSystem.eSortOption.Unit_Evade_Low:
				case NKCUnitSortSystem.eSortOption.Unit_Evade_High:
					slot.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Unit_Evade_High, this.m_ssActive.GetUnitEvadeCache(unitUID));
					goto IL_1AB;
				case NKCUnitSortSystem.eSortOption.Unit_ReduceSkillCool_Low:
				case NKCUnitSortSystem.eSortOption.Unit_ReduceSkillCool_High:
					slot.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Unit_ReduceSkillCool_High, this.m_ssActive.GetUnitSkillCoolCache(unitUID));
					goto IL_1AB;
				}
				slot.SetSortingTypeValue(false, NKCOperatorSortSystem.eSortOption.Level_High, 0);
			}
			else
			{
				slot.SetSortingTypeValue(false, NKCOperatorSortSystem.eSortOption.Level_High, 0);
			}
			IL_1AB:
			NKCUIUnitSelectList.eUnitSlotSelectState slotSelectState = NKCUIUnitSelectList.eUnitSlotSelectState.NONE;
			slot.SetSlotSelectState(slotSelectState);
			slot.SetCityLeaderMark(false);
			int sameKindShipCountFromID = NKCScenManager.CurrentUserData().m_ArmyData.GetSameKindShipCountFromID(unitData.m_UnitID);
			slot.SetHaveCount(sameKindShipCountFromID, true);
		}

		// Token: 0x06006BDE RID: 27614 RVA: 0x00233788 File Offset: 0x00231988
		private void OnSortChanged(bool bResetScroll)
		{
			if (this.m_ssActive != null)
			{
				LoopScrollRect loopScrollRect = null;
				NKM_ITEM_MISC_TYPE nkm_ITEM_MISC_TYPE = this.m_NKM_ITEM_MISC_TYPE;
				if (nkm_ITEM_MISC_TYPE != NKM_ITEM_MISC_TYPE.IMT_CHOICE_UNIT)
				{
					if (nkm_ITEM_MISC_TYPE == NKM_ITEM_MISC_TYPE.IMT_CHOICE_SHIP)
					{
						loopScrollRect = this.m_loopScrollRectShip;
					}
				}
				else
				{
					loopScrollRect = this.m_loopScrollRectUnit;
				}
				if (loopScrollRect != null)
				{
					loopScrollRect.TotalCount = this.m_ssActive.SortedUnitList.Count;
					if (bResetScroll)
					{
						loopScrollRect.SetIndexPosition(0);
						return;
					}
					loopScrollRect.RefreshCells(false);
				}
			}
		}

		// Token: 0x06006BDF RID: 27615 RVA: 0x002337F1 File Offset: 0x002319F1
		public void OnSelectUnitSlot(NKMUnitData unitData, NKMUnitTempletBase unitTempletBase, NKMDeckIndex deckIndex, NKCUnitSortSystem.eUnitState slotState, NKCUIUnitSelectList.eUnitSlotSelectState unitSlotSelectState)
		{
			NKCPopupSelectionConfirm.Instance.Open(this.m_NKMItemMiscTemplet, unitData.m_UnitID, 1L, 0);
		}

		// Token: 0x04005792 RID: 22418
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_UNIT_SELECTION";

		// Token: 0x04005793 RID: 22419
		private const string UI_ASSET_NAME = "NKM_UI_UNIT_SELECTION";

		// Token: 0x04005794 RID: 22420
		private static NKCUISelection m_Instance;

		// Token: 0x04005795 RID: 22421
		public NKCUIComSafeArea m_SafeArea;

		// Token: 0x04005796 RID: 22422
		[Header("유닛")]
		public GameObject m_objUnitChoice;

		// Token: 0x04005797 RID: 22423
		public LoopScrollRect m_loopScrollRectUnit;

		// Token: 0x04005798 RID: 22424
		public Transform m_trContentParentUnit;

		// Token: 0x04005799 RID: 22425
		public Image m_imgBannerUnit;

		// Token: 0x0400579A RID: 22426
		[Header("함선")]
		public GameObject m_objShipChoice;

		// Token: 0x0400579B RID: 22427
		public LoopScrollRect m_loopScrollRectShip;

		// Token: 0x0400579C RID: 22428
		public Transform m_trContentParentShip;

		// Token: 0x0400579D RID: 22429
		public Image m_imgBannerShip;

		// Token: 0x0400579E RID: 22430
		[Header("프리팹")]
		public NKCUIUnitSelectListSlot m_pfbUnitSlot;

		// Token: 0x0400579F RID: 22431
		public NKCUIShipSelectListSlot m_pfbShipSlot;

		// Token: 0x040057A0 RID: 22432
		[Header("필터/정렬 통합ui")]
		public NKCUIComUnitSortOptions m_SortUI;

		// Token: 0x040057A1 RID: 22433
		private NKM_ITEM_MISC_TYPE m_NKM_ITEM_MISC_TYPE = NKM_ITEM_MISC_TYPE.IMT_CHOICE_UNIT;

		// Token: 0x040057A2 RID: 22434
		private List<int> m_lstRewardId = new List<int>();

		// Token: 0x040057A3 RID: 22435
		private NKCUnitSortSystem m_ssActive;

		// Token: 0x040057A4 RID: 22436
		private List<NKCUIUnitSelectListSlotBase> m_lstVisibleSlot = new List<NKCUIUnitSelectListSlotBase>();

		// Token: 0x040057A5 RID: 22437
		private Stack<NKCUIUnitSelectListSlotBase> m_stkUnitSlotPool = new Stack<NKCUIUnitSelectListSlotBase>();

		// Token: 0x040057A6 RID: 22438
		private Stack<NKCUIUnitSelectListSlotBase> m_stkShipSlotPool = new Stack<NKCUIUnitSelectListSlotBase>();

		// Token: 0x040057A7 RID: 22439
		private NKMItemMiscTemplet m_NKMItemMiscTemplet;

		// Token: 0x040057A8 RID: 22440
		private Vector2 SHIP_SELECTION_CELL_SIZE = new Vector2(565f, 266f);

		// Token: 0x040057A9 RID: 22441
		private readonly HashSet<NKCUnitSortSystem.eFilterCategory> m_setUnitFilterCategory = new HashSet<NKCUnitSortSystem.eFilterCategory>
		{
			NKCUnitSortSystem.eFilterCategory.Have,
			NKCUnitSortSystem.eFilterCategory.UnitType,
			NKCUnitSortSystem.eFilterCategory.UnitRole,
			NKCUnitSortSystem.eFilterCategory.UnitTargetType,
			NKCUnitSortSystem.eFilterCategory.Rarity,
			NKCUnitSortSystem.eFilterCategory.Cost
		};

		// Token: 0x040057AA RID: 22442
		private readonly HashSet<NKCUnitSortSystem.eSortCategory> m_setUnitSortCategory = new HashSet<NKCUnitSortSystem.eSortCategory>
		{
			NKCUnitSortSystem.eSortCategory.ID,
			NKCUnitSortSystem.eSortCategory.Rarity,
			NKCUnitSortSystem.eSortCategory.UnitSummonCost,
			NKCUnitSortSystem.eSortCategory.UnitAttack,
			NKCUnitSortSystem.eSortCategory.UnitHealth,
			NKCUnitSortSystem.eSortCategory.UnitDefense,
			NKCUnitSortSystem.eSortCategory.UnitHit,
			NKCUnitSortSystem.eSortCategory.UnitEvade
		};

		// Token: 0x040057AB RID: 22443
		private readonly HashSet<NKCUnitSortSystem.eFilterCategory> m_setShipFilterCategory = new HashSet<NKCUnitSortSystem.eFilterCategory>
		{
			NKCUnitSortSystem.eFilterCategory.Have,
			NKCUnitSortSystem.eFilterCategory.ShipType,
			NKCUnitSortSystem.eFilterCategory.Rarity
		};

		// Token: 0x040057AC RID: 22444
		private readonly HashSet<NKCUnitSortSystem.eSortCategory> m_setShipSortCategory = new HashSet<NKCUnitSortSystem.eSortCategory>
		{
			NKCUnitSortSystem.eSortCategory.ID,
			NKCUnitSortSystem.eSortCategory.Rarity,
			NKCUnitSortSystem.eSortCategory.UnitAttack,
			NKCUnitSortSystem.eSortCategory.UnitHealth
		};
	}
}
