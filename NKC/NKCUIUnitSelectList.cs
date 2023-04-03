using System;
using System.Collections.Generic;
using ClientPacket.User;
using ClientPacket.WorldMap;
using NKC.UI.NPC;
using NKC.UI.Shop;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009F8 RID: 2552
	public class NKCUIUnitSelectList : NKCUIBase
	{
		// Token: 0x170012A9 RID: 4777
		// (get) Token: 0x06006EA0 RID: 28320 RVA: 0x0024621C File Offset: 0x0024441C
		public static NKCUIUnitSelectList Instance
		{
			get
			{
				if (NKCUIUnitSelectList.m_Instance == null)
				{
					NKCUIUnitSelectList.m_Instance = NKCUIManager.OpenNewInstance<NKCUIUnitSelectList>("ab_ui_nkm_ui_unit_select_list", "NKM_UI_UNIT_SELECT_LIST", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIUnitSelectList.OnCleanupInstance)).GetInstance<NKCUIUnitSelectList>();
					NKCUIUnitSelectList.m_Instance.InitUI();
				}
				return NKCUIUnitSelectList.m_Instance;
			}
		}

		// Token: 0x170012AA RID: 4778
		// (get) Token: 0x06006EA1 RID: 28321 RVA: 0x0024626B File Offset: 0x0024446B
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIUnitSelectList.m_Instance != null && NKCUIUnitSelectList.m_Instance.IsOpen;
			}
		}

		// Token: 0x06006EA2 RID: 28322 RVA: 0x00246286 File Offset: 0x00244486
		public static void CheckInstanceAndClose()
		{
			if (NKCUIUnitSelectList.m_Instance != null && NKCUIUnitSelectList.m_Instance.IsOpen)
			{
				NKCUIUnitSelectList.m_Instance.Close();
			}
		}

		// Token: 0x170012AB RID: 4779
		// (get) Token: 0x06006EA3 RID: 28323 RVA: 0x002462AB File Offset: 0x002444AB
		public static bool IsInstanceLoaded
		{
			get
			{
				return NKCUIUnitSelectList.m_Instance != null;
			}
		}

		// Token: 0x170012AC RID: 4780
		// (get) Token: 0x06006EA4 RID: 28324 RVA: 0x002462B8 File Offset: 0x002444B8
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.ON_PLAY_GAME;
			}
		}

		// Token: 0x06006EA5 RID: 28325 RVA: 0x002462BC File Offset: 0x002444BC
		public static NKCUIUnitSelectList OpenNewInstance(bool bWillCloseUnderPopupOnOpen = true)
		{
			NKCUIUnitSelectList instance = NKCUIManager.OpenNewInstance<NKCUIUnitSelectList>("ab_ui_nkm_ui_unit_select_list", "NKM_UI_UNIT_SELECT_LIST", NKCUIManager.eUIBaseRect.UIFrontCommon, null).GetInstance<NKCUIUnitSelectList>();
			if (instance != null)
			{
				instance.InitUI();
			}
			instance.m_bWillCloseUnderPopupOnOpen = bWillCloseUnderPopupOnOpen;
			return instance;
		}

		// Token: 0x06006EA6 RID: 28326 RVA: 0x002462F7 File Offset: 0x002444F7
		public static void OnCleanupInstance()
		{
			NKCUIUnitSelectList.m_Instance = null;
		}

		// Token: 0x170012AD RID: 4781
		// (get) Token: 0x06006EA7 RID: 28327 RVA: 0x002462FF File Offset: 0x002444FF
		public override string GuideTempletID
		{
			get
			{
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_UNIT_LIST)
				{
					return "ARTICLE_SYSTEM_UNIT_LIST";
				}
				return "";
			}
		}

		// Token: 0x06006EA8 RID: 28328 RVA: 0x0024631A File Offset: 0x0024451A
		private static NKCUIUnitSelectList.TargetTabType ConvertTabType(NKM_UNIT_TYPE unitType)
		{
			switch (unitType)
			{
			default:
				return NKCUIUnitSelectList.TargetTabType.Unit;
			case NKM_UNIT_TYPE.NUT_SHIP:
				return NKCUIUnitSelectList.TargetTabType.Ship;
			case NKM_UNIT_TYPE.NUT_OPERATOR:
				return NKCUIUnitSelectList.TargetTabType.Operator;
			}
		}

		// Token: 0x06006EA9 RID: 28329 RVA: 0x00246335 File Offset: 0x00244535
		private static NKM_UNIT_TYPE GetUnitType(NKCUIUnitSelectList.TargetTabType tabType)
		{
			switch (tabType)
			{
			default:
				return NKM_UNIT_TYPE.NUT_NORMAL;
			case NKCUIUnitSelectList.TargetTabType.Ship:
				return NKM_UNIT_TYPE.NUT_SHIP;
			case NKCUIUnitSelectList.TargetTabType.Operator:
				return NKM_UNIT_TYPE.NUT_OPERATOR;
			}
		}

		// Token: 0x170012AE RID: 4782
		// (get) Token: 0x06006EAA RID: 28330 RVA: 0x0024634E File Offset: 0x0024454E
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170012AF RID: 4783
		// (get) Token: 0x06006EAB RID: 28331 RVA: 0x00246354 File Offset: 0x00244554
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				if (this.m_currentOption.eUpsideMenuMode != NKCUIUpsideMenu.eMode.Invalid)
				{
					return this.m_currentOption.eUpsideMenuMode;
				}
				NKCUIUnitSelectList.eUnitSelectListMode unitSelectListMode = this.m_currentOption.m_UnitSelectListMode;
				if (unitSelectListMode - NKCUIUnitSelectList.eUnitSelectListMode.ALLUNIT_DEV <= 1)
				{
					return NKCUIUpsideMenu.eMode.Disable;
				}
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x170012B0 RID: 4784
		// (get) Token: 0x06006EAC RID: 28332 RVA: 0x00246390 File Offset: 0x00244590
		public override string MenuName
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_currentOption.strUpsideMenuName))
				{
					return NKCUtilString.GET_STRING_UNIT_SELECT;
				}
				return this.m_currentOption.strUpsideMenuName;
			}
		}

		// Token: 0x170012B1 RID: 4785
		// (get) Token: 0x06006EAD RID: 28333 RVA: 0x002463B5 File Offset: 0x002445B5
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				if (this.IsRemoveMode && this.m_currentTargetUnitType == NKCUIUnitSelectList.TargetTabType.Unit)
				{
					return new List<int>
					{
						1022,
						1,
						101
					};
				}
				return this.RESOURCE_LIST;
			}
		}

		// Token: 0x170012B2 RID: 4786
		// (get) Token: 0x06006EAE RID: 28334 RVA: 0x002463ED File Offset: 0x002445ED
		public override bool WillCloseUnderPopupOnOpen
		{
			get
			{
				return this.m_bWillCloseUnderPopupOnOpen;
			}
		}

		// Token: 0x06006EAF RID: 28335 RVA: 0x002463F8 File Offset: 0x002445F8
		private NKCUnitSortSystem GetUnitSortSystem(NKCUIUnitSelectList.TargetTabType type)
		{
			if (this.m_dicUnitSortSystem.ContainsKey(type) && this.m_dicUnitSortSystem[type] != null)
			{
				NKCUnitSortSystem nkcunitSortSystem = this.m_dicUnitSortSystem[type];
				nkcunitSortSystem.BuildFilterAndSortedList(this.m_currentOption.setFilterOption, this.m_currentOption.lstSortOption, this.m_currentOption.bHideDeckedUnit);
				switch (type)
				{
				case NKCUIUnitSelectList.TargetTabType.Ship:
					this.m_SortUI.RegisterCategories(this.m_currentOption.setShipFilterCategory, this.m_currentOption.setShipSortCategory, this.m_currentOption.setFilterOption.Contains(NKCUnitSortSystem.eFilterOption.Favorite));
					goto IL_EE;
				case NKCUIUnitSelectList.TargetTabType.Trophy:
					this.m_SortUI.RegisterCategories(NKCUnitSortSystem.setDefaultTrophyFilterCategory, NKCUnitSortSystem.setDefaultTrophySortCategory, false);
					goto IL_EE;
				}
				this.m_SortUI.RegisterCategories(this.m_currentOption.setUnitFilterCategory, this.m_currentOption.setUnitSortCategory, this.m_currentOption.setFilterOption.Contains(NKCUnitSortSystem.eFilterOption.Favorite));
				IL_EE:
				this.m_SortUI.RegisterUnitSort(nkcunitSortSystem);
				this.m_SortUI.ResetUI(this.m_currentOption.m_bUseFavorite && (type == NKCUIUnitSelectList.TargetTabType.Unit || type == NKCUIUnitSelectList.TargetTabType.Trophy));
				return nkcunitSortSystem;
			}
			NKCUnitSortSystem nkcunitSortSystem2;
			switch (this.m_currentOption.m_UnitSelectListMode)
			{
			default:
				switch (type)
				{
				default:
					nkcunitSortSystem2 = new NKCUnitSort(NKCScenManager.CurrentUserData(), this.m_currentOption.m_SortOptions);
					break;
				case NKCUIUnitSelectList.TargetTabType.Ship:
					nkcunitSortSystem2 = new NKCShipSort(NKCScenManager.CurrentUserData(), this.m_currentOption.m_SortOptions);
					break;
				case NKCUIUnitSelectList.TargetTabType.Trophy:
					nkcunitSortSystem2 = new NKCGenericUnitSort(NKCScenManager.CurrentUserData(), this.m_currentOption.m_SortOptions, NKCScenManager.CurrentUserData().m_ArmyData.m_dicMyTrophy.Values);
					nkcunitSortSystem2.lstSortOption = new List<NKCUnitSortSystem.eSortOption>
					{
						NKCUnitSortSystem.eSortOption.Rarity_High,
						NKCUnitSortSystem.eSortOption.UID_First
					};
					break;
				}
				break;
			case NKCUIUnitSelectList.eUnitSelectListMode.CUSTOM_LIST:
			{
				List<NKMUnitData> list = new List<NKMUnitData>();
				foreach (int unitID in this.m_currentOption.setOnlyIncludeUnitID)
				{
					list.Add(NKCUnitSortSystem.MakeTempUnitData(unitID, 1, 0));
				}
				nkcunitSortSystem2 = new NKCGenericUnitSort(NKCScenManager.CurrentUserData(), this.m_currentOption.m_SortOptions, list);
				break;
			}
			case NKCUIUnitSelectList.eUnitSelectListMode.ALLUNIT_DEV:
				switch (type)
				{
				default:
					nkcunitSortSystem2 = new NKCAllUnitSort(NKCScenManager.CurrentUserData(), this.m_currentOption.m_SortOptions);
					break;
				case NKCUIUnitSelectList.TargetTabType.Ship:
					nkcunitSortSystem2 = new NKCAllShipSort(NKCScenManager.CurrentUserData(), this.m_currentOption.m_SortOptions);
					break;
				case NKCUIUnitSelectList.TargetTabType.Trophy:
				{
					List<NKMUnitData> list2 = new List<NKMUnitData>();
					foreach (NKMUnitTempletBase nkmunitTempletBase in NKMTempletContainer<NKMUnitTempletBase>.Values)
					{
						if (nkmunitTempletBase.IsTrophy)
						{
							list2.Add(NKCUnitSortSystem.MakeTempUnitData(nkmunitTempletBase.m_UnitID, 1, 0));
						}
					}
					nkcunitSortSystem2 = new NKCGenericUnitSort(NKCScenManager.CurrentUserData(), this.m_currentOption.m_SortOptions, list2);
					break;
				}
				}
				break;
			case NKCUIUnitSelectList.eUnitSelectListMode.ALLSKIN_DEV:
				switch (type)
				{
				default:
				{
					List<NKMUnitData> list3 = new List<NKMUnitData>();
					foreach (NKMSkinTemplet skinTemplet in NKMSkinManager.m_dicSkinTemplet.Values)
					{
						list3.Add(NKCUnitSortSystem.MakeTempUnitData(skinTemplet, 1, 0));
					}
					nkcunitSortSystem2 = new NKCGenericUnitSort(NKCScenManager.CurrentUserData(), this.m_currentOption.m_SortOptions, list3);
					break;
				}
				case NKCUIUnitSelectList.TargetTabType.Ship:
					nkcunitSortSystem2 = new NKCAllShipSort(NKCScenManager.CurrentUserData(), this.m_currentOption.m_SortOptions);
					break;
				case NKCUIUnitSelectList.TargetTabType.Trophy:
					nkcunitSortSystem2 = new NKCGenericUnitSort(NKCScenManager.CurrentUserData(), this.m_currentOption.m_SortOptions, new List<NKMUnitData>());
					break;
				}
				break;
			}
			if (this.m_bKeepSortFilterOptions)
			{
				List<NKCUnitSortSystem.eSortOption> unitSortOption = this.m_SortUI.GetUnitSortOption();
				if (unitSortOption != null)
				{
					nkcunitSortSystem2.lstSortOption = unitSortOption;
				}
				HashSet<NKCUnitSortSystem.eFilterOption> unitFilterOption = this.m_SortUI.GetUnitFilterOption();
				if (unitFilterOption != null)
				{
					nkcunitSortSystem2.FilterSet = unitFilterOption;
				}
			}
			this.m_dicUnitSortSystem[type] = nkcunitSortSystem2;
			switch (type)
			{
			default:
				this.m_SortUI.RegisterCategories(this.m_currentOption.setUnitFilterCategory, this.m_currentOption.setUnitSortCategory, this.m_currentOption.setFilterOption.Contains(NKCUnitSortSystem.eFilterOption.Favorite));
				break;
			case NKCUIUnitSelectList.TargetTabType.Ship:
				this.m_SortUI.RegisterCategories(this.m_currentOption.setShipFilterCategory, this.m_currentOption.setShipSortCategory, this.m_currentOption.setFilterOption.Contains(NKCUnitSortSystem.eFilterOption.Favorite));
				break;
			case NKCUIUnitSelectList.TargetTabType.Trophy:
				this.m_SortUI.RegisterCategories(NKCUnitSortSystem.setDefaultTrophyFilterCategory, NKCUnitSortSystem.setDefaultTrophySortCategory, false);
				break;
			}
			this.m_SortUI.RegisterUnitSort(nkcunitSortSystem2);
			this.m_SortUI.ResetUI(this.m_currentOption.m_bUseFavorite && (type == NKCUIUnitSelectList.TargetTabType.Unit || type == NKCUIUnitSelectList.TargetTabType.Trophy));
			return nkcunitSortSystem2;
		}

		// Token: 0x06006EB0 RID: 28336 RVA: 0x002468E8 File Offset: 0x00244AE8
		private NKCOperatorSortSystem GetOperatorSortSystem()
		{
			if (this.m_OperatorSortSystem != null)
			{
				this.m_OperatorSortSystem.BuildFilterAndSortedList(this.m_OperatorSortSystem.FilterSet, this.m_OperatorSortSystem.lstSortOption, this.m_currentOption.bHideDeckedUnit);
				this.m_SortUI.RegisterCategories(this.m_currentOption.setOperatorFilterCategory, this.m_currentOption.setOperatorSortCategory, this.m_currentOption.setFilterOption.Contains(NKCUnitSortSystem.eFilterOption.Favorite));
				this.m_SortUI.RegisterOperatorSort(this.m_OperatorSortSystem);
				this.m_SortUI.ResetUI(false);
				return this.m_OperatorSortSystem;
			}
			NKCOperatorSortSystem nkcoperatorSortSystem;
			switch (this.m_currentOption.m_UnitSelectListMode)
			{
			default:
				nkcoperatorSortSystem = new NKCOperatorSort(NKCScenManager.CurrentUserData(), this.m_currentOption.m_OperatorSortOptions);
				break;
			case NKCUIUnitSelectList.eUnitSelectListMode.CUSTOM_LIST:
			{
				List<NKMOperator> list = new List<NKMOperator>();
				foreach (int unitID in this.m_currentOption.setOnlyIncludeUnitID)
				{
					list.Add(NKCOperatorUtil.GetDummyOperator(unitID, false));
				}
				nkcoperatorSortSystem = new NKCGenericOperatorSort(NKCScenManager.CurrentUserData(), this.m_currentOption.m_OperatorSortOptions, list);
				break;
			}
			case NKCUIUnitSelectList.eUnitSelectListMode.ALLUNIT_DEV:
				nkcoperatorSortSystem = new NKCAllOperatorSort(NKCScenManager.CurrentUserData(), this.m_currentOption.m_OperatorSortOptions);
				break;
			case NKCUIUnitSelectList.eUnitSelectListMode.ALLSKIN_DEV:
				nkcoperatorSortSystem = new NKCAllOperatorSort(NKCScenManager.CurrentUserData(), this.m_currentOption.m_OperatorSortOptions);
				break;
			}
			if (this.m_bKeepSortFilterOptions)
			{
				List<NKCOperatorSortSystem.eSortOption> operatorSortOption = this.m_SortUI.GetOperatorSortOption();
				if (operatorSortOption != null)
				{
					nkcoperatorSortSystem.lstSortOption = operatorSortOption;
				}
				HashSet<NKCOperatorSortSystem.eFilterOption> operatorFilterOption = this.m_SortUI.GetOperatorFilterOption();
				if (operatorFilterOption != null)
				{
					nkcoperatorSortSystem.FilterSet = operatorFilterOption;
				}
			}
			this.m_OperatorSortSystem = nkcoperatorSortSystem;
			this.m_SortUI.RegisterCategories(this.m_currentOption.setOperatorFilterCategory, this.m_currentOption.setOperatorSortCategory, this.m_currentOption.setFilterOption.Contains(NKCUnitSortSystem.eFilterOption.Favorite));
			this.m_SortUI.RegisterOperatorSort(nkcoperatorSortSystem);
			this.m_SortUI.ResetUI(false);
			return nkcoperatorSortSystem;
		}

		// Token: 0x170012B3 RID: 4787
		// (get) Token: 0x06006EB1 RID: 28337 RVA: 0x00246AF0 File Offset: 0x00244CF0
		private NKMUserData UserData
		{
			get
			{
				return NKCScenManager.CurrentUserData();
			}
		}

		// Token: 0x170012B4 RID: 4788
		// (get) Token: 0x06006EB2 RID: 28338 RVA: 0x00246AF7 File Offset: 0x00244CF7
		private bool IsLockMode
		{
			get
			{
				return this.m_currentOption.bEnableLockUnitSystem && this.m_bLockModeEnabled;
			}
		}

		// Token: 0x170012B5 RID: 4789
		// (get) Token: 0x06006EB3 RID: 28339 RVA: 0x00246B0E File Offset: 0x00244D0E
		private bool IsRemoveMode
		{
			get
			{
				return this.m_currentOption.bEnableRemoveUnitSystem && this.m_currentOption.bShowRemoveItem;
			}
		}

		// Token: 0x170012B6 RID: 4790
		// (get) Token: 0x06006EB4 RID: 28340 RVA: 0x00246B2A File Offset: 0x00244D2A
		private bool IsBanMode
		{
			get
			{
				return this.m_currentOption.bShowBanMsg;
			}
		}

		// Token: 0x170012B7 RID: 4791
		// (get) Token: 0x06006EB5 RID: 28341 RVA: 0x00246B37 File Offset: 0x00244D37
		private bool IsOpendAtRearmExtract
		{
			get
			{
				return this.m_currentOption.bOpenedAtRearmExtract;
			}
		}

		// Token: 0x06006EB6 RID: 28342 RVA: 0x00246B44 File Offset: 0x00244D44
		private RectTransform GetSlot(int index)
		{
			NKCUIUnitSelectListSlotBase original;
			Stack<NKCUIUnitSelectListSlotBase> stack;
			switch (this.m_currentTargetUnitType)
			{
			case NKCUIUnitSelectList.TargetTabType.Unit:
			case NKCUIUnitSelectList.TargetTabType.Trophy:
				if (this.m_currentOption.bShowBanMsg)
				{
					original = this.m_pfbUnitSlotForCastingBan;
					stack = this.m_stkUnitCastingBanSlotPool;
				}
				else
				{
					original = this.m_pfbUnitSlot;
					stack = this.m_stkUnitSlotPool;
				}
				break;
			case NKCUIUnitSelectList.TargetTabType.Ship:
				stack = this.m_stkShipSlotPool;
				original = this.m_pfbShipSlot;
				break;
			case NKCUIUnitSelectList.TargetTabType.Operator:
				if (this.m_currentOption.bShowBanMsg)
				{
					original = this.m_pfbOperSlotForCastingBan;
					stack = this.m_stkOperCastingBanSlotPool;
				}
				else
				{
					stack = this.m_stkOperSlotPool;
					original = this.m_pfbOperatorSlot;
				}
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

		// Token: 0x06006EB7 RID: 28343 RVA: 0x00246C54 File Offset: 0x00244E54
		private void ReturnSlot(Transform go)
		{
			NKCUIUnitSelectListSlotBase component = go.GetComponent<NKCUIUnitSelectListSlotBase>();
			this.m_lstVisibleSlot.Remove(component);
			go.SetParent(this.m_rectSlotPoolRect);
			if (component is NKCUIUnitSelectListSlot)
			{
				if (this.m_bIsReturnCastingBanSlot)
				{
					this.m_stkUnitCastingBanSlotPool.Push(component);
					return;
				}
				this.m_stkUnitSlotPool.Push(component);
				return;
			}
			else
			{
				if (component is NKCUIShipSelectListSlot)
				{
					this.m_stkShipSlotPool.Push(component);
					return;
				}
				if (component is NKCUIOperatorSelectListSlot)
				{
					if (this.m_bIsReturnCastingBanSlot)
					{
						this.m_stkOperCastingBanSlotPool.Push(component);
						return;
					}
					this.m_stkOperSlotPool.Push(component);
				}
				return;
			}
		}

		// Token: 0x06006EB8 RID: 28344 RVA: 0x00246CEC File Offset: 0x00244EEC
		private void ProvideSlotData(Transform tr, int idx)
		{
			if (this.m_currentTargetUnitType == NKCUIUnitSelectList.TargetTabType.Operator)
			{
				if (this.m_OperatorSortSystem == null)
				{
					Debug.LogError("Slot Operator Sort System Null!!");
					return;
				}
			}
			else if (this.m_ssActive == null)
			{
				Debug.LogError("Slot Sort System Null!!");
				return;
			}
			NKCUIUnitSelectListSlotBase component = tr.GetComponent<NKCUIUnitSelectListSlotBase>();
			if (component == null)
			{
				return;
			}
			if (this.m_currentOption.bShowRemoveSlot)
			{
				if (idx == 0)
				{
					if (this.m_currentTargetUnitType == NKCUIUnitSelectList.TargetTabType.Operator)
					{
						component.SetEmpty(true, null, new NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot(this.OnOperatorSlotSelected));
						return;
					}
					component.SetEmpty(true, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnSlotSelected), null);
					return;
				}
				else
				{
					idx--;
				}
			}
			if (this.m_currentTargetUnitType == NKCUIUnitSelectList.TargetTabType.Operator)
			{
				if (this.m_OperatorSortSystem.SortedOperatorList.Count <= idx)
				{
					return;
				}
				NKMOperator nkmoperator = this.m_OperatorSortSystem.SortedOperatorList[idx];
				component.SetEnableShowCastingBanSelectedObject(this.m_currentOption.bShowBanMsg);
				this.SetSlotData(component, nkmoperator);
				if (this.IsRemoveMode)
				{
					component.SetContractedUnitMark(nkmoperator != null && nkmoperator.fromContract);
					return;
				}
			}
			else
			{
				if (this.m_ssActive.SortedUnitList.Count <= idx)
				{
					return;
				}
				NKMUnitData nkmunitData = this.m_ssActive.SortedUnitList[idx];
				component.SetEnableShowCastingBanSelectedObject(this.m_currentOption.bShowBanMsg);
				this.SetSlotData(component, nkmunitData);
				bool flag = this.m_SortUI.IsLimitBreakState();
				bool flag2 = this.m_SortUI.IsTacticUpdateState();
				if ((this.m_currentTargetUnitType == NKCUIUnitSelectList.TargetTabType.Unit && flag) || flag2)
				{
					NKCUIUnitSelectListSlot nkcuiunitSelectListSlot = component as NKCUIUnitSelectListSlot;
					if (null != nkcuiunitSelectListSlot)
					{
						if (flag)
						{
							int limitBreakCache = this.m_ssActive.GetLimitBreakCache(nkmunitData.m_UnitUID);
							nkcuiunitSelectListSlot.SetLimitPossibleMark(limitBreakCache >= 0, NKMUnitLimitBreakManager.IsMaxLimitBreak(nkmunitData, false));
						}
						if (flag2)
						{
							int tacticUpdateCache = this.m_ssActive.GetTacticUpdateCache(nkmunitData.m_UnitUID);
							nkcuiunitSelectListSlot.SetTacticPossibleMark(tacticUpdateCache);
						}
					}
				}
				if (this.IsRemoveMode || this.IsOpendAtRearmExtract)
				{
					component.SetContractedUnitMark(nkmunitData != null && nkmunitData.FromContract);
				}
			}
		}

		// Token: 0x06006EB9 RID: 28345 RVA: 0x00246ED0 File Offset: 0x002450D0
		private void SetSlotData(NKCUIUnitSelectListSlotBase slot, NKMUnitData unitData)
		{
			long unitUID = unitData.m_UnitUID;
			NKMDeckIndex deckIndexCacheByOption = this.m_ssActive.GetDeckIndexCacheByOption(unitUID, !this.m_currentOption.m_SortOptions.bUseDeckedState);
			slot.SetData(unitData, deckIndexCacheByOption, true, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnSlotSelected));
			if (NKCScenManager.CurrentUserData() != null)
			{
				slot.SetRecall(NKCRecallManager.IsRecallTargetUnit(unitData, NKCSynchronizedTime.GetServerUTCTime(0.0)));
			}
			slot.SetLock(unitData.m_bLock, this.m_bLockModeEnabled);
			slot.SetFavorite(unitData);
			if (this.m_ssActive.lstSortOption.Count > 0)
			{
				NKCUnitSortSystem.eSortOption eSortOption = this.m_ssActive.lstSortOption[0];
				switch (eSortOption)
				{
				case NKCUnitSortSystem.eSortOption.Power_Low:
				case NKCUnitSortSystem.eSortOption.Power_High:
					slot.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Power_High, this.m_ssActive.GetUnitPowerCache(unitUID));
					goto IL_1FE;
				case NKCUnitSortSystem.eSortOption.UID_First:
				case NKCUnitSortSystem.eSortOption.UID_Last:
				case NKCUnitSortSystem.eSortOption.ID_First:
				case NKCUnitSortSystem.eSortOption.ID_Last:
				case NKCUnitSortSystem.eSortOption.IDX_First:
				case NKCUnitSortSystem.eSortOption.IDX_Last:
					break;
				case NKCUnitSortSystem.eSortOption.Attack_Low:
				case NKCUnitSortSystem.eSortOption.Attack_High:
					slot.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Attack_High, this.m_ssActive.GetUnitAttackCache(unitUID));
					goto IL_1FE;
				case NKCUnitSortSystem.eSortOption.Health_Low:
				case NKCUnitSortSystem.eSortOption.Health_High:
					slot.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Health_High, this.m_ssActive.GetUnitHPCache(unitUID));
					goto IL_1FE;
				case NKCUnitSortSystem.eSortOption.Unit_Defense_Low:
				case NKCUnitSortSystem.eSortOption.Unit_Defense_High:
					slot.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Unit_Defense_High, this.m_ssActive.GetUnitDefCache(unitUID));
					goto IL_1FE;
				case NKCUnitSortSystem.eSortOption.Unit_Crit_Low:
				case NKCUnitSortSystem.eSortOption.Unit_Crit_High:
					slot.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Unit_Crit_High, this.m_ssActive.GetUnitCritCache(unitUID));
					goto IL_1FE;
				case NKCUnitSortSystem.eSortOption.Unit_Hit_Low:
				case NKCUnitSortSystem.eSortOption.Unit_Hit_High:
					slot.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Unit_Hit_High, this.m_ssActive.GetUnitHitCache(unitUID));
					goto IL_1FE;
				case NKCUnitSortSystem.eSortOption.Unit_Evade_Low:
				case NKCUnitSortSystem.eSortOption.Unit_Evade_High:
					slot.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Unit_Evade_High, this.m_ssActive.GetUnitEvadeCache(unitUID));
					goto IL_1FE;
				case NKCUnitSortSystem.eSortOption.Unit_ReduceSkillCool_Low:
				case NKCUnitSortSystem.eSortOption.Unit_ReduceSkillCool_High:
					slot.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Unit_ReduceSkillCool_High, this.m_ssActive.GetUnitSkillCoolCache(unitUID));
					goto IL_1FE;
				default:
					if (eSortOption - NKCUnitSortSystem.eSortOption.Unit_Loyalty_High <= 1)
					{
						slot.SetSortingTypeValue(true, NKCUnitSortSystem.eSortOption.Unit_Loyalty_High, this.m_ssActive.GetLoyaltyCache(unitUID));
						goto IL_1FE;
					}
					break;
				}
				slot.SetSortingTypeValue(false, NKCOperatorSortSystem.eSortOption.Level_High, 0);
			}
			else
			{
				slot.SetSortingTypeValue(false, NKCOperatorSortSystem.eSortOption.Level_High, 0);
			}
			IL_1FE:
			slot.SetSlotState(this.m_ssActive.GetUnitSlotState(unitData.m_UnitUID));
			NKCUIUnitSelectList.eUnitSlotSelectState slotSelectState;
			if (this.m_currentOption.bMultipleSelect)
			{
				if (this.m_listSelectedUnit.Contains(unitUID))
				{
					if (this.m_currentOption.bShowRemoveItem)
					{
						slotSelectState = NKCUIUnitSelectList.eUnitSlotSelectState.DELETE;
					}
					else
					{
						slotSelectState = NKCUIUnitSelectList.eUnitSlotSelectState.SELECTED;
					}
				}
				else if (this.m_currentOption.iMaxMultipleSelect <= this.m_listSelectedUnit.Count)
				{
					slotSelectState = NKCUIUnitSelectList.eUnitSlotSelectState.DISABLE;
				}
				else
				{
					slotSelectState = NKCUIUnitSelectList.eUnitSlotSelectState.NONE;
				}
			}
			else
			{
				slotSelectState = NKCUIUnitSelectList.eUnitSlotSelectState.NONE;
			}
			slot.SetSlotSelectState(slotSelectState);
			NKMWorldMapManager.WorldMapLeaderState cityStateCache = this.m_ssActive.GetCityStateCache(unitUID);
			slot.SetCityLeaderMark(!this.m_currentOption.bOpenedAtRearmExtract && cityStateCache > NKMWorldMapManager.WorldMapLeaderState.None);
			NKCUIUnitSelectList.UnitSelectListOptions.OnSlotSetData dOnSlotSetData = this.m_currentOption.dOnSlotSetData;
			if (dOnSlotSetData == null)
			{
				return;
			}
			dOnSlotSetData(slot, unitData, deckIndexCacheByOption);
		}

		// Token: 0x06006EBA RID: 28346 RVA: 0x0024718C File Offset: 0x0024538C
		private void SetSlotData(NKCUIUnitSelectListSlotBase slot, NKMOperator operatorData)
		{
			long uid = operatorData.uid;
			NKMDeckIndex deckIndexCache = this.m_OperatorSortSystem.GetDeckIndexCache(uid, !this.m_currentOption.m_OperatorSortOptions.IsHasBuildOption(BUILD_OPTIONS.USE_DECKED_STATE));
			if (slot is NKCUIOperatorSelectListSlot && this.m_currentOption.bShowBanMsg)
			{
				slot.SetDataForBan(operatorData, true, new NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot(this.OnOperatorSlotSelected));
			}
			else
			{
				slot.SetData(operatorData, deckIndexCache, true, new NKCUIUnitSelectListSlotBase.OnSelectThisOperatorSlot(this.OnOperatorSlotSelected));
			}
			slot.SetLock(operatorData.bLock, this.m_bLockModeEnabled);
			slot.SetFavorite(operatorData);
			if (this.m_OperatorSortSystem.lstSortOption.Count > 0)
			{
				switch (this.m_OperatorSortSystem.lstSortOption[0])
				{
				case NKCOperatorSortSystem.eSortOption.Power_Low:
				case NKCOperatorSortSystem.eSortOption.Power_High:
					slot.SetSortingTypeValue(true, NKCOperatorSortSystem.eSortOption.Power_High, this.m_OperatorSortSystem.GetUnitPowerCache(uid));
					goto IL_17A;
				case NKCOperatorSortSystem.eSortOption.Attack_Low:
				case NKCOperatorSortSystem.eSortOption.Attack_High:
					slot.SetSortingTypeValue(true, NKCOperatorSortSystem.eSortOption.Attack_High, this.m_OperatorSortSystem.GetUnitAttackCache(uid));
					goto IL_17A;
				case NKCOperatorSortSystem.eSortOption.Health_Low:
				case NKCOperatorSortSystem.eSortOption.Health_High:
					slot.SetSortingTypeValue(true, NKCOperatorSortSystem.eSortOption.Health_High, this.m_OperatorSortSystem.GetUnitHPCache(uid));
					goto IL_17A;
				case NKCOperatorSortSystem.eSortOption.Unit_Defense_Low:
				case NKCOperatorSortSystem.eSortOption.Unit_Defense_High:
					slot.SetSortingTypeValue(true, NKCOperatorSortSystem.eSortOption.Unit_Defense_High, this.m_OperatorSortSystem.GetUnitDefCache(uid));
					goto IL_17A;
				case NKCOperatorSortSystem.eSortOption.Unit_ReduceSkillCool_Low:
				case NKCOperatorSortSystem.eSortOption.Unit_ReduceSkillCool_High:
					slot.SetSortingTypeValue(true, NKCOperatorSortSystem.eSortOption.Unit_ReduceSkillCool_High, this.m_OperatorSortSystem.GetUnitSkillCoolCache(uid));
					goto IL_17A;
				}
				slot.SetSortingTypeValue(false, NKCOperatorSortSystem.eSortOption.Level_High, 0);
			}
			else
			{
				slot.SetSortingTypeValue(false, NKCOperatorSortSystem.eSortOption.Level_High, 0);
			}
			IL_17A:
			slot.SetSlotState(this.m_OperatorSortSystem.GetUnitSlotState(operatorData.uid));
			NKCUIUnitSelectList.eUnitSlotSelectState slotSelectState;
			if (this.m_currentOption.bMultipleSelect)
			{
				if (this.m_listSelectedUnit.Contains(uid))
				{
					if (this.m_currentOption.bShowRemoveItem)
					{
						slotSelectState = NKCUIUnitSelectList.eUnitSlotSelectState.DELETE;
					}
					else
					{
						slotSelectState = NKCUIUnitSelectList.eUnitSlotSelectState.SELECTED;
					}
				}
				else if (this.m_currentOption.iMaxMultipleSelect <= this.m_listSelectedUnit.Count)
				{
					slotSelectState = NKCUIUnitSelectList.eUnitSlotSelectState.DISABLE;
				}
				else
				{
					slotSelectState = NKCUIUnitSelectList.eUnitSlotSelectState.NONE;
				}
			}
			else
			{
				slotSelectState = NKCUIUnitSelectList.eUnitSlotSelectState.NONE;
			}
			slot.SetSlotSelectState(slotSelectState);
			slot.SetCityLeaderMark(false);
			NKCUIUnitSelectList.UnitSelectListOptions.OnSlotOperatorSetData dOnSlotOperatorSetData = this.m_currentOption.dOnSlotOperatorSetData;
			if (dOnSlotOperatorSetData == null)
			{
				return;
			}
			dOnSlotOperatorSetData(slot, operatorData, deckIndexCache);
		}

		// Token: 0x06006EBB RID: 28347 RVA: 0x002473A4 File Offset: 0x002455A4
		public void InitUI()
		{
			if (this.m_bInit)
			{
				return;
			}
			this.m_bInit = true;
			this.m_LoopScrollRect.dOnGetObject += this.GetSlot;
			this.m_LoopScrollRect.dOnReturnObject += this.ReturnSlot;
			this.m_LoopScrollRect.dOnProvideData += this.ProvideSlotData;
			this.m_LoopScrollRect.dOnRepopulate += this.CalculateContentRectSize;
			NKCUtil.SetScrollHotKey(this.m_LoopScrollRect, null);
			this.m_SortUI.Init(new NKCUIComUnitSortOptions.OnSorted(this.OnSortChanged), false);
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglSelectModeUnit, new UnityAction<bool>(this.OnSelectUnitMode));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglSelectModeShip, new UnityAction<bool>(this.OnSelectShipMode));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglSelectModeOperator, new UnityAction<bool>(this.OnSelectOperatorMode));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglSelectModeTrophy, new UnityAction<bool>(this.OnSelectTrophyMode));
			NKCUtil.SetGameobjectActive(this.m_tglSelectModeOperator, !NKCOperatorUtil.IsHide());
			this.m_cbtnMultipleSelectOK.PointerClick.RemoveAllListeners();
			this.m_cbtnMultipleSelectOK.PointerClick.AddListener(new UnityAction(this.OnUnitSelectCompleteInMulti));
			if (this.m_NKM_UI_UNIT_SELECT_LIST_POSSESS_ADD != null)
			{
				this.m_NKM_UI_UNIT_SELECT_LIST_POSSESS_ADD.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_UNIT_SELECT_LIST_POSSESS_ADD.PointerClick.AddListener(new UnityAction(this.OnExpandInventoryPopup));
			}
			this.m_ctgLockUnit.OnValueChanged.RemoveAllListeners();
			this.m_ctgLockUnit.OnValueChanged.AddListener(new UnityAction<bool>(this.OnLockModeButton));
			this.m_btnRemoveUnit.PointerClick.RemoveAllListeners();
			this.m_btnRemoveUnit.PointerClick.AddListener(delegate()
			{
				this.OnRemoveMode(true);
			});
			NKCUtil.SetButtonClickDelegate(this.m_btnShopShortcut, new UnityAction(this.OnShopShortcut));
			NKCUtil.SetButtonClickDelegate(this.m_btnShipBuildShortcut, new UnityAction(this.OnShipBuildShortcut));
			this.m_btnMultiCancel.PointerClick.RemoveAllListeners();
			this.m_btnMultiCancel.PointerClick.AddListener(new UnityAction(this.OnTouchMultiCancel));
			this.m_btnMultiAuto.PointerClick.RemoveAllListeners();
			this.m_btnMultiAuto.PointerClick.AddListener(new UnityAction(this.OnTouchAutoSelect));
			this.m_btnMultiAutoN.PointerClick.RemoveAllListeners();
			this.m_btnMultiAutoN.PointerClick.AddListener(delegate()
			{
				this.OnAutoSelectByGrade(NKM_UNIT_GRADE.NUG_N);
			});
			this.m_btnMultiAutoR.PointerClick.RemoveAllListeners();
			this.m_btnMultiAutoR.PointerClick.AddListener(delegate()
			{
				this.OnAutoSelectByGrade(NKM_UNIT_GRADE.NUG_R);
			});
			this.m_NKM_UI_UNIT_SELECT_LIST_UNIT = base.transform.Find("NKM_UI_UNIT_SELECT_LIST_UNIT").GetComponent<NKCUIRectMove>();
			NKCUtil.SetGameobjectActive(this.m_popupSmartSelect, false);
			this.m_NKM_UI_UNIT_SELECT_LIST_UNIT = base.transform.Find("NKM_UI_UNIT_SELECT_LIST_UNIT").GetComponent<NKCUIRectMove>();
			base.gameObject.SetActive(false);
		}

		// Token: 0x06006EBC RID: 28348 RVA: 0x0024769C File Offset: 0x0024589C
		private void CalculateContentRectSize()
		{
			int minColumn = 0;
			Vector2 cellSize = Vector2.zero;
			Vector2 spacing = Vector2.zero;
			switch (this.m_currentTargetUnitType)
			{
			case NKCUIUnitSelectList.TargetTabType.Unit:
			case NKCUIUnitSelectList.TargetTabType.Trophy:
				minColumn = 5;
				if (this.m_currentOption.bShowBanMsg)
				{
					cellSize = this.m_vUnitCastingSlotSize;
					spacing = this.m_vUnitCastingSlotSpacing;
				}
				else
				{
					cellSize = this.m_vUnitSlotSize;
					spacing = this.m_vUnitSlotSpacing;
				}
				break;
			case NKCUIUnitSelectList.TargetTabType.Ship:
				minColumn = 3;
				cellSize = this.m_vShipSlotSize;
				spacing = this.m_vShipSlotSpacing;
				break;
			case NKCUIUnitSelectList.TargetTabType.Operator:
				minColumn = 5;
				if (this.m_currentOption.bShowBanMsg)
				{
					cellSize = this.m_vOperCastingSlotSize;
					spacing = this.m_vOperCastingSlotSpacing;
				}
				else
				{
					cellSize = this.m_vOperatorSlotSize;
					spacing = this.m_vOperatorSlotSpacing;
				}
				break;
			}
			if (this.m_SafeArea != null)
			{
				this.m_SafeArea.SetSafeAreaBase();
			}
			NKCUtil.CalculateContentRectSize(this.m_LoopScrollRect, this.m_GridLayoutGroup, minColumn, cellSize, spacing, this.m_currentTargetUnitType == NKCUIUnitSelectList.TargetTabType.Ship);
		}

		// Token: 0x06006EBD RID: 28349 RVA: 0x0024777C File Offset: 0x0024597C
		public void Open(NKCUIUnitSelectList.UnitSelectListOptions options, NKCUIUnitSelectList.OnUnitSelectCommand onUnitSelectCommand, NKCUIUnitSelectList.OnUnitSortList OnUnitSortList = null, NKCUIUnitSelectList.OnOperatorSortList OnOperatorSortList = null, NKCUIUnitSelectList.OnUnitSortOption OnUnitSortOption = null, List<int> lstUpsideMenuResource = null)
		{
			this.SetUnitListAddEffect(false);
			this.m_listSelectedUnit.Clear();
			this.m_ssActive = null;
			this.m_OperatorSortSystem = null;
			this.m_dicUnitSortSystem.Clear();
			this.m_bKeepSortFilterOptions = (!string.IsNullOrEmpty(this.m_currentOption.m_strCachingUIName) && !string.IsNullOrEmpty(options.m_strCachingUIName) && string.Equals(this.m_currentOption.m_strCachingUIName, options.m_strCachingUIName) && this.m_currentOption.eTargetUnitType == options.eTargetUnitType);
			this.m_bIsReturnCastingBanSlot = false;
			if (this.m_currentOption.eTargetUnitType == options.eTargetUnitType && this.m_currentOption.bShowBanMsg != options.bShowBanMsg)
			{
				this.m_bIsReturnCastingBanSlot = this.m_currentOption.bShowBanMsg;
				this.m_bCellPrepared = false;
			}
			this.m_currentOption = options;
			this.m_BeforeUnit = options.beforeUnit;
			this.m_BeforeOperator = options.beforeOperator;
			this.m_BeforeUnitDeckIndex = options.beforeUnitDeckIndex;
			this.m_dOnUnitSelectCommand = onUnitSelectCommand;
			this.m_dOnUnitSortList = OnUnitSortList;
			this.m_dOnOperatorSortList = OnOperatorSortList;
			this.m_dOnUnitSortOption = OnUnitSortOption;
			if (lstUpsideMenuResource != null)
			{
				this.RESOURCE_LIST = lstUpsideMenuResource;
			}
			else
			{
				this.RESOURCE_LIST = base.UpsideMenuShowResourceList;
			}
			NKCUIManager.UpdateUpsideMenu();
			this.m_bLockModeEnabled = false;
			if (this.m_currentOption.setSelectedUnitUID != null)
			{
				foreach (long item in this.m_currentOption.setSelectedUnitUID)
				{
					this.m_listSelectedUnit.Add(item);
				}
			}
			this.ChangeUI();
			this.ProcessByType(this.m_currentOption.eTargetUnitType, false);
			this.SetUnitCount(this.m_currentOption.eTargetUnitType);
			this.UpdateMultipleSelectCountUI();
			this.UpdateDisableUIOnMultipleSelect();
			this.UpdateMultiSelectGetItemResult();
			NKCUtil.SetGameobjectActive(this.m_objRootUnitCount, !options.m_bHideUnitCount);
			NKCUtil.SetGameobjectActive(this.m_popupSmartSelect, false);
			NKCUtil.SetGameobjectActive(this.m_btnShipBuildShortcut, options.m_bShowShipBuildShortcut);
			base.UIOpened(true);
		}

		// Token: 0x06006EBE RID: 28350 RVA: 0x0024798C File Offset: 0x00245B8C
		private void ChangeUI()
		{
			NKCUtil.SetGameobjectActive(this.m_objMultipleSelectRoot, this.m_currentOption.bMultipleSelect);
			this.m_ctgLockUnit.Select(this.IsLockMode, false, false);
			if (this.m_NKM_UI_UNIT_SELECT_LIST_UNIT != null)
			{
				if (this.m_currentOption.bMultipleSelect)
				{
					this.m_NKM_UI_UNIT_SELECT_LIST_UNIT.Set("SELECT");
				}
				else
				{
					this.m_NKM_UI_UNIT_SELECT_LIST_UNIT.Set("BASE");
				}
				if (this.m_bPrevMultiple != this.m_currentOption.bMultipleSelect)
				{
					this.m_bCellPrepared = false;
					this.m_bPrevMultiple = this.m_currentOption.bMultipleSelect;
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objMultipleSelectGetItem, this.m_currentOption.bShowRemoveItem);
			NKCUtil.SetGameobjectActive(this.m_ctgHideDeckedUnit, this.m_currentOption.bShowHideDeckedUnitMenu);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_tglSelectModeShip, this.m_currentOption.bShowUnitShipChangeMenu);
			NKCUtil.SetGameobjectActive(this.m_tglSelectModeUnit, this.m_currentOption.bShowUnitShipChangeMenu);
			NKCUtil.SetGameobjectActive(this.m_tglSelectModeTrophy, this.m_currentOption.bShowUnitShipChangeMenu);
			if (!NKCOperatorUtil.IsHide())
			{
				NKCUtil.SetGameobjectActive(this.m_tglSelectModeOperator, this.m_currentOption.bShowUnitShipChangeMenu);
			}
			NKCUtil.SetGameobjectActive(this.m_ctgLockUnit, this.m_currentOption.bEnableLockUnitSystem);
			NKCUtil.SetGameobjectActive(this.m_btnRemoveUnit, this.m_currentOption.bEnableRemoveUnitSystem);
			NKCUtil.SetGameobjectActive(this.m_btnShopShortcut, !string.IsNullOrEmpty(this.m_currentOption.ShopShortcutTargetTab));
			NKCUtil.SetGameobjectActive(this.m_btnMultiAuto, this.m_currentOption.bUseRemoveSmartAutoSelect || this.m_currentOption.dOnAutoSelectFilter != null);
			NKCUtil.SetGameobjectActive(this.m_btnMultiAutoN, false);
			NKCUtil.SetGameobjectActive(this.m_btnMultiAutoR, false);
			NKCUtil.SetGameobjectActive(this.m_objLockMsg, this.IsLockMode);
			NKCUtil.SetGameobjectActive(this.m_objRemoveMsg, this.m_currentOption.bShowRemoveItem);
			NKCUtil.SetGameobjectActive(this.m_objBanMsg, this.m_currentOption.bShowBanMsg);
			if (this.m_currentOption.bMultipleSelect)
			{
				NKCUIUnitSelectList.TargetTabType eTargetUnitType = this.m_currentOption.eTargetUnitType;
				if (eTargetUnitType == NKCUIUnitSelectList.TargetTabType.Unit || eTargetUnitType != NKCUIUnitSelectList.TargetTabType.Ship)
				{
					this.m_lbMultipleSelectText.text = NKCUtilString.GET_STRING_UNIT_SELECT_UNIT_COUNT;
				}
				else
				{
					this.m_lbMultipleSelectText.text = NKCUtilString.GET_STRING_UNIT_SELECT_SHIP_COUNT;
				}
			}
			this.UpdateMultiOkButton();
		}

		// Token: 0x06006EBF RID: 28351 RVA: 0x00247BD2 File Offset: 0x00245DD2
		public void ClearMultipleSelect()
		{
			this.m_listSelectedUnit.Clear();
			this.UpdateMultipleSelectCountUI();
			this.UpdateDisableUIOnMultipleSelect();
			this.UpdateMultiSelectGetItemResult();
			this.UpdateUnitCount();
		}

		// Token: 0x06006EC0 RID: 28352 RVA: 0x00247BF7 File Offset: 0x00245DF7
		public void ClearCachOption()
		{
			this.m_currentOption.m_strCachingUIName = "";
		}

		// Token: 0x06006EC1 RID: 28353 RVA: 0x00247C0C File Offset: 0x00245E0C
		private void SetSortAndFilterButtons(NKCUIUnitSelectList.TargetTabType targetType)
		{
			switch (targetType)
			{
			case NKCUIUnitSelectList.TargetTabType.Unit:
				this.m_tglSelectModeUnit.Select(true, true, false);
				break;
			case NKCUIUnitSelectList.TargetTabType.Ship:
				this.m_tglSelectModeShip.Select(true, true, false);
				break;
			case NKCUIUnitSelectList.TargetTabType.Operator:
				this.m_tglSelectModeOperator.Select(true, true, false);
				break;
			case NKCUIUnitSelectList.TargetTabType.Trophy:
				this.m_tglSelectModeTrophy.Select(true, true, false);
				break;
			}
			if (this.m_currentOption.bShowUnitShipChangeMenu)
			{
				if (!NKCOperatorUtil.IsHide() && NKCOperatorUtil.IsActive())
				{
					this.m_tglSelectModeOperator.UnLock(false);
				}
				else
				{
					this.m_tglSelectModeOperator.Lock(false);
				}
			}
			this.m_SortUI.ResetUI(this.m_currentOption.m_bUseFavorite && (targetType == NKCUIUnitSelectList.TargetTabType.Unit || targetType == NKCUIUnitSelectList.TargetTabType.Trophy));
		}

		// Token: 0x06006EC2 RID: 28354 RVA: 0x00247CD0 File Offset: 0x00245ED0
		private void ProcessByType(NKCUIUnitSelectList.TargetTabType targetType, bool bForceRebuildList = false)
		{
			if (targetType == NKCUIUnitSelectList.TargetTabType.Operator)
			{
				if (this.m_currentOption.lstOperatorSortOption.Count == 0)
				{
					this.m_currentOption.lstOperatorSortOption = NKCOperatorSortSystem.GetDefaultSortOptions(false, false);
					this.m_currentOption.m_OperatorSortOptions.SetBuildOption(true, new BUILD_OPTIONS[]
					{
						BUILD_OPTIONS.DESCENDING
					});
				}
			}
			else if (this.m_dicUnitSortSystem.ContainsKey(targetType))
			{
				this.m_currentOption.setFilterOption = this.m_dicUnitSortSystem[targetType].FilterSet;
				this.m_currentOption.lstSortOption = this.m_dicUnitSortSystem[targetType].lstSortOption;
				this.m_currentOption.bDescending = this.m_dicUnitSortSystem[targetType].Descending;
			}
			else
			{
				if (this.m_currentOption.lstSortOption.Count == 0)
				{
					this.m_currentOption.lstSortOption = NKCUnitSortSystem.GetDefaultSortOptions(NKCUIUnitSelectList.GetUnitType(targetType), false, false);
				}
				this.m_currentOption.bDescending = true;
			}
			if (!this.m_bCellPrepared || this.m_currentTargetUnitType != targetType)
			{
				this.m_bCellPrepared = true;
				this.m_currentTargetUnitType = targetType;
				this.CalculateContentRectSize();
				this.m_LoopScrollRect.PrepareCells(0);
			}
			this.SetSortAndFilterButtons(targetType);
			if (bForceRebuildList)
			{
				if (targetType == NKCUIUnitSelectList.TargetTabType.Operator)
				{
					this.m_OperatorSortSystem = null;
				}
				else
				{
					this.m_dicUnitSortSystem.Remove(targetType);
				}
			}
			if (targetType == NKCUIUnitSelectList.TargetTabType.Operator)
			{
				this.m_OperatorSortSystem = this.GetOperatorSortSystem();
			}
			else
			{
				this.m_ssActive = this.GetUnitSortSystem(targetType);
			}
			this.m_bDataValid = true;
			this.OnSortChanged(true);
		}

		// Token: 0x06006EC3 RID: 28355 RVA: 0x00247E44 File Offset: 0x00246044
		public void OnUnitSelectCompleteInMulti()
		{
			this.OnUnitSelectComplete(this.m_listSelectedUnit);
		}

		// Token: 0x06006EC4 RID: 28356 RVA: 0x00247E52 File Offset: 0x00246052
		private void OnButtonRemoveConfirm()
		{
			this.OnUnitSelectComplete(this.m_listSelectedUnit);
		}

		// Token: 0x06006EC5 RID: 28357 RVA: 0x00247E60 File Offset: 0x00246060
		private void OnUnitSelectComplete(List<long> unitUID)
		{
			if (this.m_currentOption.m_IncludeUnitUID != 0L)
			{
				this.m_dicUnitSortSystem.Clear();
				if (this.m_currentOption.setExcludeUnitUID.Contains(this.m_currentOption.m_IncludeUnitUID))
				{
					this.m_currentOption.setExcludeUnitUID.Remove(this.m_currentOption.m_IncludeUnitUID);
				}
				this.m_ssActive = this.GetUnitSortSystem(this.m_currentTargetUnitType);
				int count = this.m_ssActive.SortedUnitList.Count;
			}
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_CASTING_BAN) && this.m_currentOption.bShowBanMsg && this.m_currentOption.iMaxMultipleSelect > unitUID.Count)
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_GAUNTLET_CASTING_BAN_SELECT_COMPLET, delegate()
				{
					NKCUIUnitSelectList.OnUnitSelectCommand dOnUnitSelectCommand = this.m_dOnUnitSelectCommand;
					if (dOnUnitSelectCommand == null)
					{
						return;
					}
					dOnUnitSelectCommand(new List<long>(unitUID));
				}, null, false);
				return;
			}
			if (this.m_dOnUnitSortList != null)
			{
				this.m_currentUnitList = this.m_ssActive.GetCurrentUnitList();
				if (this.m_currentUnitList.Count >= 1 && unitUID.Count > 0)
				{
					NKCUIUnitSelectList.OnUnitSortList dOnUnitSortList = this.m_dOnUnitSortList;
					if (dOnUnitSortList != null)
					{
						dOnUnitSortList(unitUID[0], this.m_currentUnitList);
					}
				}
			}
			if (this.m_dOnUnitSortOption != null)
			{
				this.m_dOnUnitSortOption(this.m_currentOption.m_SortOptions);
			}
			if (this.m_dOnUnitSelectCommand != null)
			{
				this.m_dOnUnitSelectCommand(new List<long>(unitUID));
				if (unitUID.Count > 0)
				{
					this.m_lastSelectedUnitUID = unitUID[0];
				}
			}
		}

		// Token: 0x06006EC6 RID: 28358 RVA: 0x00247FF4 File Offset: 0x002461F4
		private void OnOperatorSelectComplete(List<long> unitUID)
		{
			if (this.m_currentOption.m_IncludeUnitUID != 0L)
			{
				if (this.m_currentOption.setExcludeUnitUID.Contains(this.m_currentOption.m_IncludeUnitUID))
				{
					this.m_currentOption.setExcludeUnitUID.Remove(this.m_currentOption.m_IncludeUnitUID);
				}
				this.m_OperatorSortSystem = this.GetOperatorSortSystem();
			}
			List<NKMOperator> currentOperatorList = this.m_OperatorSortSystem.GetCurrentOperatorList();
			if (currentOperatorList.Count >= 1 && unitUID.Count > 0)
			{
				NKCUIUnitSelectList.OnOperatorSortList dOnOperatorSortList = this.m_dOnOperatorSortList;
				if (dOnOperatorSortList != null)
				{
					dOnOperatorSortList(unitUID[0], currentOperatorList);
				}
			}
			if (this.m_dOnUnitSelectCommand != null)
			{
				this.m_dOnUnitSelectCommand(new List<long>(unitUID));
			}
		}

		// Token: 0x06006EC7 RID: 28359 RVA: 0x002480A3 File Offset: 0x002462A3
		private void OnRemoveSlot()
		{
			this.OnSlotSelected(null, null, new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE), NKCUnitSortSystem.eUnitState.NONE, NKCUIUnitSelectList.eUnitSlotSelectState.NONE);
		}

		// Token: 0x06006EC8 RID: 28360 RVA: 0x002480B8 File Offset: 0x002462B8
		private void UpdateMultipleSelectCountUI()
		{
			if (this.m_currentOption.bMultipleSelect)
			{
				this.m_lbMultipleSelectCount.text = this.m_listSelectedUnit.Count.ToString() + " / " + this.m_currentOption.iMaxMultipleSelect.ToString();
			}
		}

		// Token: 0x06006EC9 RID: 28361 RVA: 0x0024810C File Offset: 0x0024630C
		private void UpdateDisableUIOnMultipleSelect()
		{
			if (this.m_currentOption.bMultipleSelect)
			{
				if (this.m_currentOption.iMaxMultipleSelect <= this.m_listSelectedUnit.Count)
				{
					using (List<NKCUIUnitSelectListSlotBase>.Enumerator enumerator = this.m_lstVisibleSlot.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase = enumerator.Current;
							if (nkcuiunitSelectListSlotBase.NKMUnitData != null && !this.m_listSelectedUnit.Contains(nkcuiunitSelectListSlotBase.NKMUnitData.m_UnitUID))
							{
								nkcuiunitSelectListSlotBase.SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState.DISABLE);
							}
						}
						return;
					}
				}
				foreach (NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase2 in this.m_lstVisibleSlot)
				{
					if (nkcuiunitSelectListSlotBase2.NKMUnitData != null && !this.m_listSelectedUnit.Contains(nkcuiunitSelectListSlotBase2.NKMUnitData.m_UnitUID))
					{
						nkcuiunitSelectListSlotBase2.SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState.NONE);
					}
				}
			}
		}

		// Token: 0x06006ECA RID: 28362 RVA: 0x0024820C File Offset: 0x0024640C
		private void UpdateMultiSelectGetItemResult()
		{
			if (!this.m_currentOption.bShowRemoveItem)
			{
				return;
			}
			List<NKCUISlot.SlotData> list = this.MakeRemoveUnitItemGainList(this.m_listSelectedUnit, this.m_currentTargetUnitType);
			int num = list.Count - this.m_listBotSlot.Count;
			for (int i = 0; i < num; i++)
			{
				NKCUISlot newInstance = NKCUISlot.GetNewInstance(this.m_trMultipleSelectGetItemListContent);
				if (newInstance != null)
				{
					this.m_listBotSlot.Add(newInstance);
				}
			}
			for (int j = 0; j < this.m_listBotSlot.Count; j++)
			{
				NKCUISlot nkcuislot = this.m_listBotSlot[j];
				if (j < list.Count)
				{
					nkcuislot.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
					nkcuislot.m_cbtnButton.UpdateOrgSize();
					NKCUtil.SetGameobjectActive(nkcuislot, true);
					nkcuislot.SetData(list[j], false, true, true, null);
					nkcuislot.SetOpenItemBoxOnClick();
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_listBotSlot[j], false);
				}
			}
		}

		// Token: 0x06006ECB RID: 28363 RVA: 0x00248318 File Offset: 0x00246518
		private List<NKCUISlot.SlotData> MakeRemoveUnitItemGainList(List<long> lstUnitUID, NKCUIUnitSelectList.TargetTabType unitType)
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			if (this.UserData == null)
			{
				return new List<NKCUISlot.SlotData>();
			}
			NKMArmyData armyData = this.UserData.m_ArmyData;
			NKMUnitData nkmunitData = null;
			NKMOperator nkmoperator = null;
			int i = 0;
			while (i < lstUnitUID.Count)
			{
				switch (unitType)
				{
				case NKCUIUnitSelectList.TargetTabType.Unit:
					goto IL_65;
				case NKCUIUnitSelectList.TargetTabType.Ship:
					nkmunitData = armyData.GetShipFromUID(lstUnitUID[i]);
					break;
				case NKCUIUnitSelectList.TargetTabType.Operator:
					nkmoperator = NKCOperatorUtil.GetOperatorData(lstUnitUID[i]);
					break;
				case NKCUIUnitSelectList.TargetTabType.Trophy:
					nkmunitData = armyData.GetTrophyFromUID(lstUnitUID[i]);
					break;
				default:
					goto IL_65;
				}
				IL_85:
				if ((nkmunitData != null || nkmoperator != null) && (nkmunitData == null || !nkmunitData.IsSeized))
				{
					int unitID = (nkmunitData != null) ? nkmunitData.m_UnitID : nkmoperator.id;
					bool flag = (nkmunitData != null) ? nkmunitData.FromContract : nkmoperator.fromContract;
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
					if (unitTempletBase != null)
					{
						for (int j = 0; j < unitTempletBase.RemoveRewards.Count; j++)
						{
							int id = unitTempletBase.RemoveRewards[j].ID;
							int count = unitTempletBase.RemoveRewards[j].Count;
							if (dictionary.ContainsKey(id))
							{
								Dictionary<int, int> dictionary2 = dictionary;
								int key = id;
								dictionary2[key] += count;
							}
							else
							{
								dictionary.Add(id, count);
							}
						}
						if (flag && unitTempletBase.RemoveRewardFromContract != null)
						{
							int id2 = unitTempletBase.RemoveRewardFromContract.ID;
							int count2 = unitTempletBase.RemoveRewardFromContract.Count;
							if (dictionary.ContainsKey(id2))
							{
								Dictionary<int, int> dictionary2 = dictionary;
								int key = id2;
								dictionary2[key] += count2;
							}
							else
							{
								dictionary.Add(id2, count2);
							}
						}
					}
				}
				i++;
				continue;
				IL_65:
				nkmunitData = armyData.GetUnitFromUID(lstUnitUID[i]);
				goto IL_85;
			}
			List<NKCUISlot.SlotData> list = new List<NKCUISlot.SlotData>();
			foreach (KeyValuePair<int, int> keyValuePair in dictionary)
			{
				list.Add(NKCUISlot.SlotData.MakeMiscItemData(keyValuePair.Key, (long)keyValuePair.Value, 0));
			}
			return list;
		}

		// Token: 0x06006ECC RID: 28364 RVA: 0x0024853C File Offset: 0x0024673C
		private void OnSlotSelectedInMuiltiSelect(NKMUnitData selectedUnit, NKMDeckIndex selectedUnitDeckIndex)
		{
			int beforeSelectedCount = this.m_listSelectedUnit.Count;
			if (this.m_listSelectedUnit.Contains(selectedUnit.m_UnitUID))
			{
				NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase = this.FindSlotFromCurrentList(selectedUnit.m_UnitUID);
				if (nkcuiunitSelectListSlotBase != null)
				{
					nkcuiunitSelectListSlotBase.SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState.NONE);
				}
				this.m_listSelectedUnit.Remove(selectedUnit.m_UnitUID);
			}
			else if (this.m_currentOption.iMaxMultipleSelect > this.m_listSelectedUnit.Count)
			{
				string content;
				if (this.m_currentOption.dOnSelectedUnitWarning != null && this.m_currentOption.dOnSelectedUnitWarning(selectedUnit.m_UnitUID, this.m_listSelectedUnit, out content))
				{
					NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, content, delegate()
					{
						this.UpdateMuiltiSelectUIForDismissible(selectedUnit.m_UnitUID, beforeSelectedCount);
					}, null, false);
					return;
				}
				this.UpdateMuiltiSelectUIForDismissible(selectedUnit.m_UnitUID, beforeSelectedCount);
				return;
			}
			this.UpdateMuiltiSelectUI(beforeSelectedCount, this.m_listSelectedUnit.Count);
		}

		// Token: 0x06006ECD RID: 28365 RVA: 0x00248654 File Offset: 0x00246854
		private void OnSlotSelectedInMuiltiSelect(NKMOperator selectedOperator, NKMDeckIndex selectedUnitDeckIndex)
		{
			int beforeSelectedCount = this.m_listSelectedUnit.Count;
			if (this.m_listSelectedUnit.Contains(selectedOperator.uid))
			{
				NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase = this.FindSlotFromCurrentList(selectedOperator.uid);
				if (nkcuiunitSelectListSlotBase != null)
				{
					nkcuiunitSelectListSlotBase.SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState.NONE);
				}
				this.m_listSelectedUnit.Remove(selectedOperator.uid);
			}
			else if (this.m_currentOption.iMaxMultipleSelect > this.m_listSelectedUnit.Count)
			{
				string content;
				if (this.m_currentOption.dOnSelectedUnitWarning != null && this.m_currentOption.dOnSelectedUnitWarning(selectedOperator.uid, this.m_listSelectedUnit, out content))
				{
					NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, content, delegate()
					{
						this.UpdateMuiltiSelectUIForDismissible(selectedOperator.uid, beforeSelectedCount);
					}, null, false);
					return;
				}
				this.UpdateMuiltiSelectUIForDismissible(selectedOperator.uid, beforeSelectedCount);
				return;
			}
			this.UpdateMuiltiSelectUI(beforeSelectedCount, this.m_listSelectedUnit.Count);
		}

		// Token: 0x06006ECE RID: 28366 RVA: 0x0024876C File Offset: 0x0024696C
		private void UpdateMuiltiSelectUIForDismissible(long unitUid, int beforeSelectedCount)
		{
			this.m_listSelectedUnit.Add(unitUid);
			bool bShowRemoveItem = this.m_currentOption.bShowRemoveItem;
			if (bShowRemoveItem && this.m_currentTargetUnitType == NKCUIUnitSelectList.TargetTabType.Unit)
			{
				NKCUINPCMachineGap.PlayVoice(NPC_TYPE.MACHINE_GAP, NPC_ACTION_TYPE.DISMISSAL_SELECT, false);
			}
			NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase = this.FindSlotFromCurrentList(unitUid);
			if (nkcuiunitSelectListSlotBase != null)
			{
				nkcuiunitSelectListSlotBase.SetSlotSelectState(bShowRemoveItem ? NKCUIUnitSelectList.eUnitSlotSelectState.DELETE : NKCUIUnitSelectList.eUnitSlotSelectState.SELECTED);
			}
			this.UpdateMuiltiSelectUI(beforeSelectedCount, this.m_listSelectedUnit.Count);
		}

		// Token: 0x06006ECF RID: 28367 RVA: 0x002487D5 File Offset: 0x002469D5
		private void UpdateMuiltiSelectUI(int beforeSelectedCount, int afterSelectedCount)
		{
			if (afterSelectedCount != beforeSelectedCount && (afterSelectedCount == this.m_currentOption.iMaxMultipleSelect || afterSelectedCount == this.m_currentOption.iMaxMultipleSelect - 1))
			{
				this.UpdateDisableUIOnMultipleSelect();
			}
			this.UpdateMultipleSelectCountUI();
			this.UpdateMultiSelectGetItemResult();
		}

		// Token: 0x06006ED0 RID: 28368 RVA: 0x0024880C File Offset: 0x00246A0C
		private void OnSlotSelected(NKMUnitData selectedUnit, NKMUnitTempletBase unitTempletBase, NKMDeckIndex selectedUnitDeckIndex, NKCUnitSortSystem.eUnitState unitSlotState, NKCUIUnitSelectList.eUnitSlotSelectState unitSlotSelectState)
		{
			if (unitSlotSelectState != NKCUIUnitSelectList.eUnitSlotSelectState.SELECTED && unitSlotSelectState == NKCUIUnitSelectList.eUnitSlotSelectState.DISABLE)
			{
				return;
			}
			switch (unitSlotState)
			{
			case NKCUnitSortSystem.eUnitState.NONE:
				goto IL_A2;
			case NKCUnitSortSystem.eUnitState.DUPLICATE:
			case NKCUnitSortSystem.eUnitState.DECKED:
			case NKCUnitSortSystem.eUnitState.MAINUNIT:
			case NKCUnitSortSystem.eUnitState.LOCKED:
			case NKCUnitSortSystem.eUnitState.DUNGEON_RESTRICTED:
			case NKCUnitSortSystem.eUnitState.LEAGUE_BANNED:
			case NKCUnitSortSystem.eUnitState.LEAGUE_DECKED_LEFT:
			case NKCUnitSortSystem.eUnitState.LEAGUE_DECKED_RIGHT:
			case NKCUnitSortSystem.eUnitState.OFFICE_DORM_IN:
				return;
			case NKCUnitSortSystem.eUnitState.CITY_MISSION:
			case NKCUnitSortSystem.eUnitState.WARFARE_BATCH:
			case NKCUnitSortSystem.eUnitState.DIVE_BATCH:
				if (!this.m_currentOption.bCanSelectUnitInMission)
				{
					return;
				}
				goto IL_A2;
			case NKCUnitSortSystem.eUnitState.SEIZURE:
				if (!this.IsRemoveMode && !this.m_currentOption.m_SortOptions.bIncludeSeizure)
				{
					return;
				}
				goto IL_A2;
			}
			if (this.m_currentOption.m_SortOptions.bUseDeckedState)
			{
				return;
			}
			IL_A2:
			if (this.IsLockMode)
			{
				if (selectedUnit != null)
				{
					NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(selectedUnit.m_UnitID);
					if (unitTempletBase2 != null && unitTempletBase2.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_OPERATOR)
					{
						NKCPacketSender.Send_NKMPacket_OPERATOR_LOCK_REQ(selectedUnit.m_UnitUID, !selectedUnit.m_bLock);
						return;
					}
					NKCPacketSender.Send_NKMPacket_LOCK_UNIT_REQ(selectedUnit.m_UnitUID, !selectedUnit.m_bLock);
					return;
				}
			}
			else
			{
				if (this.m_currentOption.bMultipleSelect)
				{
					this.OnSlotSelectedInMuiltiSelect(selectedUnit, selectedUnitDeckIndex);
					return;
				}
				List<long> singleSelectedList = new List<long>();
				if (selectedUnit != null)
				{
					singleSelectedList.Add(selectedUnit.m_UnitUID);
				}
				else
				{
					singleSelectedList.Add(0L);
				}
				if (this.m_BeforeUnit == null || this.m_BeforeUnit.m_UnitUID == 0L || selectedUnit == null)
				{
					this.OnUnitSelectComplete(singleSelectedList);
					return;
				}
				this.OpenUnitChangePopup(this.m_BeforeUnit, this.m_BeforeUnitDeckIndex, selectedUnit, selectedUnitDeckIndex, delegate()
				{
					this.OnUnitSelectComplete(singleSelectedList);
				});
			}
		}

		// Token: 0x06006ED1 RID: 28369 RVA: 0x00248994 File Offset: 0x00246B94
		private void OnOperatorSlotSelected(NKMOperator selectedOperator, NKMUnitTempletBase unitTempletBase, NKMDeckIndex selectedUnitDeckIndex, NKCUnitSortSystem.eUnitState unitSlotState, NKCUIUnitSelectList.eUnitSlotSelectState unitSlotSelectState)
		{
			if (unitSlotSelectState != NKCUIUnitSelectList.eUnitSlotSelectState.SELECTED && unitSlotSelectState == NKCUIUnitSelectList.eUnitSlotSelectState.DISABLE)
			{
				return;
			}
			switch (unitSlotState)
			{
			case NKCUnitSortSystem.eUnitState.NONE:
				goto IL_9E;
			case NKCUnitSortSystem.eUnitState.DUPLICATE:
			case NKCUnitSortSystem.eUnitState.DECKED:
			case NKCUnitSortSystem.eUnitState.MAINUNIT:
			case NKCUnitSortSystem.eUnitState.LOCKED:
			case NKCUnitSortSystem.eUnitState.DUNGEON_RESTRICTED:
			case NKCUnitSortSystem.eUnitState.LEAGUE_BANNED:
			case NKCUnitSortSystem.eUnitState.LEAGUE_DECKED_LEFT:
			case NKCUnitSortSystem.eUnitState.LEAGUE_DECKED_RIGHT:
				return;
			case NKCUnitSortSystem.eUnitState.CITY_MISSION:
			case NKCUnitSortSystem.eUnitState.WARFARE_BATCH:
			case NKCUnitSortSystem.eUnitState.DIVE_BATCH:
				if (!this.m_currentOption.bCanSelectUnitInMission)
				{
					return;
				}
				goto IL_9E;
			case NKCUnitSortSystem.eUnitState.SEIZURE:
				if (!this.IsRemoveMode && !this.m_currentOption.m_SortOptions.bIncludeSeizure)
				{
					return;
				}
				goto IL_9E;
			}
			if (this.m_currentOption.m_SortOptions.bUseDeckedState)
			{
				return;
			}
			IL_9E:
			if (this.IsLockMode)
			{
				if (selectedOperator != null)
				{
					NKCPacketSender.Send_NKMPacket_OPERATOR_LOCK_REQ(selectedOperator.uid, !selectedOperator.bLock);
					return;
				}
			}
			else
			{
				if (this.m_currentOption.bMultipleSelect)
				{
					this.OnSlotSelectedInMuiltiSelect(selectedOperator, selectedUnitDeckIndex);
					return;
				}
				List<long> singleSelectedList = new List<long>();
				if (selectedOperator != null)
				{
					singleSelectedList.Add(selectedOperator.uid);
				}
				else
				{
					singleSelectedList.Add(0L);
				}
				if (this.m_BeforeOperator == null || this.m_BeforeOperator.uid == 0L || selectedOperator == null)
				{
					if (this.m_currentTargetUnitType == NKCUIUnitSelectList.TargetTabType.Operator && singleSelectedList[0] != 0L && string.Equals(this.m_currentOption.m_strCachingUIName, NKCUtilString.GET_STRING_EVENT_DECK))
					{
						NKCUIOperatorPopupConfirm.Instance.Open(singleSelectedList[0], delegate()
						{
							this.OnOperatorSelectComplete(singleSelectedList);
						});
						return;
					}
					this.OnOperatorSelectComplete(singleSelectedList);
					return;
				}
				else
				{
					this.OpenUnitChangePopup(this.m_BeforeOperator, this.m_BeforeUnitDeckIndex, selectedOperator, selectedUnitDeckIndex, delegate()
					{
						this.OnOperatorSelectComplete(singleSelectedList);
					});
				}
			}
		}

		// Token: 0x06006ED2 RID: 28370 RVA: 0x00248B3C File Offset: 0x00246D3C
		private void OpenUnitChangePopup(NKMUnitData beforeUnit, NKMDeckIndex beforeUnitDeckIndex, NKMUnitData afterUnit, NKMDeckIndex afterUnitDeckIndex, NKCUIUnitSelectListChangePopup.OnUnitChangePopupOK onOK)
		{
			if (this.m_currentTargetUnitType == NKCUIUnitSelectList.TargetTabType.Unit)
			{
				NKCUIUnitSelectListChangePopup.Instance.Open(beforeUnit, beforeUnitDeckIndex, afterUnit, afterUnitDeckIndex, onOK, false, false);
				return;
			}
			if (this.m_currentTargetUnitType == NKCUIUnitSelectList.TargetTabType.Operator)
			{
				NKCUIOperatorPopupChange.Instance.Open(NKMDeckIndex.None, beforeUnit.m_UnitUID, afterUnit.m_UnitUID, delegate
				{
					NKCUIUnitSelectListChangePopup.OnUnitChangePopupOK onOK3 = onOK;
					if (onOK3 == null)
					{
						return;
					}
					onOK3();
				});
				return;
			}
			NKCUIUnitSelectListChangePopup.OnUnitChangePopupOK onOK2 = onOK;
			if (onOK2 == null)
			{
				return;
			}
			onOK2();
		}

		// Token: 0x06006ED3 RID: 28371 RVA: 0x00248BB8 File Offset: 0x00246DB8
		private void OpenUnitChangePopup(NKMOperator beforeOperator, NKMDeckIndex beforeUnitDeckIndex, NKMOperator afterOperator, NKMDeckIndex afterUnitDeckIndex, NKCUIUnitSelectListChangePopup.OnUnitChangePopupOK onOK)
		{
			NKCUIOperatorPopupChange.Instance.Open(NKMDeckIndex.None, beforeOperator.uid, afterOperator.uid, delegate
			{
				onOK();
			});
		}

		// Token: 0x06006ED4 RID: 28372 RVA: 0x00248BFC File Offset: 0x00246DFC
		public void OnLockModeButton(bool bValue)
		{
			this.m_bLockModeEnabled = bValue;
			NKCUtil.SetGameobjectActive(this.m_objLockMsg, bValue);
			this.m_canvasRemoveUnit.alpha = (bValue ? 0.3f : 1f);
			this.m_btnRemoveUnit.enabled = !bValue;
			this.ProcessByType(this.m_currentTargetUnitType, this.m_currentTargetUnitType == NKCUIUnitSelectList.TargetTabType.Operator);
		}

		// Token: 0x06006ED5 RID: 28373 RVA: 0x00248C5A File Offset: 0x00246E5A
		private void OnTouchMultiCancel()
		{
			if (this.m_currentOption.bShowRemoveItem)
			{
				this.OnRemoveMode(false);
				return;
			}
			base.Close();
		}

		// Token: 0x06006ED6 RID: 28374 RVA: 0x00248C78 File Offset: 0x00246E78
		public void OnRemoveMode(bool bValue)
		{
			if (bValue)
			{
				this.m_bLockModeEnabled = false;
				this.m_prevOption = this.m_currentOption;
				this.m_currentOption = new NKCUIUnitSelectList.UnitSelectListOptions(this.m_currentTargetUnitType, true, NKM_DECK_TYPE.NDT_NORMAL, NKCUIUnitSelectList.eUnitSelectListMode.Normal, true);
				this.m_currentOption.bDescending = false;
				this.m_currentOption.bExcludeLockedUnit = false;
				this.m_currentOption.bExcludeDeckedUnit = false;
				this.m_currentOption.bHideDeckedUnit = false;
				this.m_currentOption.m_SortOptions.bUseLockedState = true;
				this.m_currentOption.m_SortOptions.bUseDeckedState = true;
				this.m_currentOption.m_SortOptions.bUseDormInState = true;
				this.m_currentOption.m_SortOptions.bIncludeSeizure = true;
				this.m_currentOption.m_OperatorSortOptions.SetBuildOption(true, new BUILD_OPTIONS[]
				{
					BUILD_OPTIONS.USE_LOCKED_STATE,
					BUILD_OPTIONS.USE_DECKED_STATE,
					BUILD_OPTIONS.INCLUDE_SEIZURE
				});
				this.m_currentOption.bShowHideDeckedUnitMenu = false;
				this.m_currentOption.bShowRemoveItem = true;
				this.m_currentOption.bEnableRemoveUnitSystem = true;
				this.m_currentOption.setExcludeUnitID = NKCUnitSortSystem.GetDefaultExcludeUnitIDs();
				this.m_currentOption.bUseRemoveSmartAutoSelect = this.m_prevOption.bUseRemoveSmartAutoSelect;
				this.m_currentOption.m_SortOptions.AdditionalExcludeFilterFunc = new NKCUnitSortSystem.UnitListOptions.CustomFilterFunc(this.CheckCanRemove);
				this.m_currentOption.m_SortOptions.PreemptiveSortFunc = new NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc(this.SortRemoveUnitBySeized);
				this.m_currentOption.dOnAutoSelectFilter = this.m_prevOption.dOnAutoSelectFilter;
				this.m_currentOption.dOnClose = this.m_prevOption.dOnClose;
				this.m_currentOption.setUnitFilterCategory = this.m_prevOption.setUnitFilterCategory;
				this.m_currentOption.setUnitSortCategory = this.m_prevOption.setUnitSortCategory;
				this.m_currentOption.setShipFilterCategory = this.m_prevOption.setShipFilterCategory;
				this.m_currentOption.setShipSortCategory = this.m_prevOption.setShipSortCategory;
				this.m_currentOption.setOperatorFilterCategory = this.m_prevOption.setOperatorFilterCategory;
				this.m_currentOption.setOperatorSortCategory = this.m_prevOption.setOperatorSortCategory;
				this.m_prevOnUnitSelectCommand = this.m_dOnUnitSelectCommand;
				this.m_dOnUnitSelectCommand = new NKCUIUnitSelectList.OnUnitSelectCommand(this.RemoveUIDList);
				switch (this.m_currentTargetUnitType)
				{
				case NKCUIUnitSelectList.TargetTabType.Unit:
					this.m_currentOption.strEmptyMessage = NKCUtilString.GET_STRING_REMOVE_UNIT_NO_EXIST_UNIT;
					this.m_txtRemoveMsg.text = NKCUtilString.GET_STRING_REMOVE_UNIT_SELECT;
					this.m_currentOption.iMaxMultipleSelect = 1000;
					break;
				case NKCUIUnitSelectList.TargetTabType.Ship:
					this.m_currentOption.strEmptyMessage = NKCUtilString.GET_STRING_REMOVE_SHIP_NO_EXIST_SHIP;
					this.m_txtRemoveMsg.text = NKCUtilString.GET_STRING_REMOVE_SHIP_SELECT;
					this.m_currentOption.iMaxMultipleSelect = 100;
					break;
				case NKCUIUnitSelectList.TargetTabType.Operator:
					this.m_currentOption.strEmptyMessage = NKCUtilString.GET_STRING_REMOVE_UNIT_NO_EXIST_OPERATOR;
					this.m_txtRemoveMsg.text = NKCUtilString.GET_STRING_REMOVE_UNIT_SELECT;
					this.m_currentOption.iMaxMultipleSelect = 1000;
					break;
				case NKCUIUnitSelectList.TargetTabType.Trophy:
					this.m_currentOption.strEmptyMessage = NKCUtilString.GET_STRING_REMOVE_UNIT_NO_EXIST_TROPHY;
					this.m_txtRemoveMsg.text = NKCUtilString.GET_STRING_REMOVE_UNIT_SELECT;
					this.m_currentOption.iMaxMultipleSelect = 1000;
					break;
				}
				this.ClearMultipleSelect();
			}
			else
			{
				this.ClearMultipleSelect();
				this.m_dOnUnitSelectCommand = this.m_prevOnUnitSelectCommand;
				this.m_prevOnUnitSelectCommand = null;
				this.m_currentOption = this.m_prevOption;
			}
			NKCUIManager.UpdateUpsideMenu();
			this.ChangeUI();
			this.ProcessByType(this.m_currentTargetUnitType, true);
			this.UpdateUnitCount();
		}

		// Token: 0x06006ED7 RID: 28375 RVA: 0x00248FCC File Offset: 0x002471CC
		private bool CheckCanRemove(NKMUnitData unitData)
		{
			if (!unitData.IsSeized)
			{
				return true;
			}
			if (unitData.m_bLock)
			{
				return false;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return false;
			}
			long unitUID = unitData.m_UnitUID;
			for (int i = 0; i < 6; i++)
			{
				NKMBackgroundUnitInfo backgroundUnitInfo = nkmuserData.GetBackgroundUnitInfo(i);
				if (backgroundUnitInfo != null && backgroundUnitInfo.unitUid == unitUID)
				{
					return false;
				}
			}
			if (nkmuserData.m_ArmyData.GetDeckDataByUnitUID(unitUID) != null)
			{
				return false;
			}
			using (Dictionary<int, NKMWorldMapCityData>.ValueCollection.Enumerator enumerator = nkmuserData.m_WorldmapData.worldMapCityDataMap.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.leaderUnitUID == unitUID)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06006ED8 RID: 28376 RVA: 0x0024908C File Offset: 0x0024728C
		private int SortRemoveUnitBySeized(NKMUnitData a, NKMUnitData b)
		{
			return b.IsSeized.CompareTo(a.IsSeized);
		}

		// Token: 0x06006ED9 RID: 28377 RVA: 0x002490AD File Offset: 0x002472AD
		public void CloseRemoveMode()
		{
			if (this.m_currentOption.bShowRemoveItem)
			{
				this.OnRemoveMode(false);
			}
		}

		// Token: 0x06006EDA RID: 28378 RVA: 0x002490C4 File Offset: 0x002472C4
		public void RemoveUIDList(List<long> list)
		{
			switch (this.m_currentTargetUnitType)
			{
			case NKCUIUnitSelectList.TargetTabType.Unit:
				this.RemoveUnitList(list);
				return;
			case NKCUIUnitSelectList.TargetTabType.Ship:
				this.RemoveShipList(list);
				return;
			case NKCUIUnitSelectList.TargetTabType.Operator:
				this.RemoveOperatorList(list);
				return;
			case NKCUIUnitSelectList.TargetTabType.Trophy:
				this.RemoveTrophyList(list);
				return;
			default:
				return;
			}
		}

		// Token: 0x06006EDB RID: 28379 RVA: 0x00249110 File Offset: 0x00247310
		private void RemoveUnitList(List<long> list)
		{
			if (list == null || list.Count <= 0)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_NO_EXIST_SELECTED_UNIT, null, "");
				return;
			}
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			NKCPopupOKCancel.OnButton <>9__0;
			foreach (long unitUid in list)
			{
				NKMUnitData unitFromUID = armyData.GetUnitFromUID(unitUid);
				if (unitFromUID == null)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_NO_EXIST_UNIT, null, "");
					return;
				}
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitFromUID.m_UnitID);
				if (unitTempletBase != null && (unitFromUID.m_UnitLevel > 1 || unitFromUID.m_LimitBreakLevel > 0 || unitTempletBase.m_NKM_UNIT_GRADE >= NKM_UNIT_GRADE.NUG_SR))
				{
					string get_STRING_NOTICE = NKCUtilString.GET_STRING_NOTICE;
					string get_STRING_REMOVE_UNIT_WARNING = NKCUtilString.GET_STRING_REMOVE_UNIT_WARNING;
					NKCPopupOKCancel.OnButton onOkButton;
					if ((onOkButton = <>9__0) == null)
					{
						onOkButton = (<>9__0 = delegate()
						{
							this.OpenRemoveConfirmPopup(list);
						});
					}
					NKCPopupOKCancel.OpenOKCancelBox(get_STRING_NOTICE, get_STRING_REMOVE_UNIT_WARNING, onOkButton, null, false);
					return;
				}
			}
			this.OpenRemoveConfirmPopup(list);
		}

		// Token: 0x06006EDC RID: 28380 RVA: 0x0024924C File Offset: 0x0024744C
		private void RemoveTrophyList(List<long> list)
		{
			if (list == null || list.Count <= 0)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_NO_EXIST_SELECTED_UNIT, null, "");
				return;
			}
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			foreach (long trophy in list)
			{
				if (armyData.GetTrophyFromUID(trophy) == null)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_NO_EXIST_UNIT, null, "");
					return;
				}
			}
			this.OpenRemoveConfirmPopup(list);
		}

		// Token: 0x06006EDD RID: 28381 RVA: 0x002492EC File Offset: 0x002474EC
		private void RemoveOperatorList(List<long> list)
		{
			if (list == null || list.Count <= 0)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_NO_EXIST_SELECTED_OPERATOR, null, "");
				return;
			}
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			NKCPopupOKCancel.OnButton <>9__0;
			foreach (long operatorUid in list)
			{
				NKMOperator operatorFromUId = armyData.GetOperatorFromUId(operatorUid);
				if (operatorFromUId == null)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_NO_EXIST_OPERATOR, null, "");
					return;
				}
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(operatorFromUId.id);
				if (unitTempletBase != null && (operatorFromUId.level > 1 || unitTempletBase.m_NKM_UNIT_GRADE >= NKM_UNIT_GRADE.NUG_SR))
				{
					string get_STRING_NOTICE = NKCUtilString.GET_STRING_NOTICE;
					string get_STRING_REMOVE_OPERATOR_WARNING = NKCUtilString.GET_STRING_REMOVE_OPERATOR_WARNING;
					NKCPopupOKCancel.OnButton onOkButton;
					if ((onOkButton = <>9__0) == null)
					{
						onOkButton = (<>9__0 = delegate()
						{
							this.OpenRemoveConfirmPopup(list);
						});
					}
					NKCPopupOKCancel.OpenOKCancelBox(get_STRING_NOTICE, get_STRING_REMOVE_OPERATOR_WARNING, onOkButton, null, false);
					return;
				}
			}
			this.OpenRemoveConfirmPopup(list);
		}

		// Token: 0x06006EDE RID: 28382 RVA: 0x0024941C File Offset: 0x0024761C
		private void OpenRemoveConfirmPopup(List<long> unitList)
		{
			List<NKCUISlot.SlotData> lstSlot = this.MakeRemoveUnitItemGainList(this.m_listSelectedUnit, this.m_currentTargetUnitType);
			switch (this.m_currentTargetUnitType)
			{
			case NKCUIUnitSelectList.TargetTabType.Unit:
			case NKCUIUnitSelectList.TargetTabType.Trophy:
				NKCPopupResourceConfirmBox.Instance.OpenItemSlotList(NKCUtilString.GET_STRING_NOTICE, string.Format(NKCStringTable.GetString("SI_DP_POPUP_NOTICE_UNIT_REMOVE_ONE_PARAM", false), unitList.Count), lstSlot, delegate
				{
					NKCPacketSender.Send_NKMPacket_REMOVE_UNIT_REQ(unitList);
				}, null, false);
				return;
			case NKCUIUnitSelectList.TargetTabType.Ship:
				break;
			case NKCUIUnitSelectList.TargetTabType.Operator:
				NKCPopupResourceConfirmBox.Instance.OpenItemSlotList(NKCUtilString.GET_STRING_NOTICE, string.Format(NKCUtilString.GET_STRING_OPERATOR_REMOVE_CONFIRM_ONE_PARAM, unitList.Count), lstSlot, delegate
				{
					NKCPacketSender.Send_NKMPacket_OPERATOR_REMOVE_REQ(unitList);
				}, null, false);
				break;
			default:
				return;
			}
		}

		// Token: 0x06006EDF RID: 28383 RVA: 0x002494E0 File Offset: 0x002476E0
		private void RemoveShipList(List<long> list)
		{
			if (list == null || list.Count <= 0)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_REMOVE_SHIP_NO_EXIST_SHIP, null, "");
				return;
			}
			NKCPopupOKCancel.OnButton <>9__0;
			foreach (long shipUid in list)
			{
				if (this.UserData == null)
				{
					break;
				}
				NKMUnitData shipFromUID = this.UserData.m_ArmyData.GetShipFromUID(shipUid);
				if (shipFromUID == null)
				{
					Debug.LogError("Ship not exist! shipUID : " + shipUid.ToString());
				}
				else
				{
					NKM_UNIT_GRADE nkm_UNIT_GRADE = NKMUnitManager.GetUnitTempletBase(shipFromUID.m_UnitID).m_NKM_UNIT_GRADE;
					if (nkm_UNIT_GRADE != NKM_UNIT_GRADE.NUG_N)
					{
						if (nkm_UNIT_GRADE - NKM_UNIT_GRADE.NUG_R <= 2)
						{
							string get_STRING_WARNING = NKCUtilString.GET_STRING_WARNING;
							string get_STRING_REMOVE_SHIP_WARNING_MSG = NKCUtilString.GET_STRING_REMOVE_SHIP_WARNING_MSG;
							NKCPopupOKCancel.OnButton onOkButton;
							if ((onOkButton = <>9__0) == null)
							{
								onOkButton = (<>9__0 = delegate()
								{
									NKCPacketSender.Send_NKMPacket_SHIP_DIVISION_REQ(list);
								});
							}
							NKCPopupOKCancel.OpenOKCancelBox(get_STRING_WARNING, get_STRING_REMOVE_SHIP_WARNING_MSG, onOkButton, null, false);
							return;
						}
						Debug.LogWarning("Unit Grade undefined! unitID : " + shipFromUID.m_UnitID.ToString());
					}
				}
			}
			NKCPacketSender.Send_NKMPacket_SHIP_DIVISION_REQ(list);
		}

		// Token: 0x06006EE0 RID: 28384 RVA: 0x0024961C File Offset: 0x0024781C
		private void OnShopShortcut()
		{
			NKCUIShop.ShopShortcut(this.m_currentOption.ShopShortcutTargetTab, 0, 0);
		}

		// Token: 0x06006EE1 RID: 28385 RVA: 0x00249630 File Offset: 0x00247830
		private void OnShipBuildShortcut()
		{
			NKCUIHangarBuild.Instance.Open();
		}

		// Token: 0x06006EE2 RID: 28386 RVA: 0x0024963C File Offset: 0x0024783C
		private void OnTouchAutoSelect()
		{
			if (this.m_currentTargetUnitType != NKCUIUnitSelectList.TargetTabType.Ship && this.m_currentOption.bUseRemoveSmartAutoSelect && this.m_popupSmartSelect != null)
			{
				this.m_popupSmartSelect.Open(new NKCUIUnitSelectListRemovePopup.OnRemoveUnits(this.OnOKAutoSelectPopup), this.m_currentTargetUnitType > NKCUIUnitSelectList.TargetTabType.Unit);
			}
			else
			{
				new HashSet<long>();
				while (this.m_listSelectedUnit.Count < this.m_currentOption.iMaxMultipleSelect)
				{
					NKMUnitData nkmunitData = this.m_ssActive.AutoSelect(new HashSet<long>(this.m_listSelectedUnit), (NKMUnitData unit) => this.AutoSelectFilter(unit, NKM_UNIT_GRADE.NUG_COUNT));
					if (nkmunitData == null)
					{
						break;
					}
					this.m_listSelectedUnit.Add(nkmunitData.m_UnitUID);
					NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase = this.FindSlotFromCurrentList(nkmunitData.m_UnitUID);
					if (nkcuiunitSelectListSlotBase != null)
					{
						nkcuiunitSelectListSlotBase.SetSlotSelectState(this.m_currentOption.bShowRemoveItem ? NKCUIUnitSelectList.eUnitSlotSelectState.DELETE : NKCUIUnitSelectList.eUnitSlotSelectState.SELECTED);
					}
				}
			}
			this.UpdateMultipleSelectCountUI();
			this.UpdateMultiSelectGetItemResult();
		}

		// Token: 0x06006EE3 RID: 28387 RVA: 0x00249722 File Offset: 0x00247922
		private void OnOKAutoSelectPopup(HashSet<NKM_UNIT_GRADE> setGrade, bool bSmart)
		{
			if (bSmart)
			{
				this.OnSmartRemoveSelectByGrade(setGrade, true);
				return;
			}
			this.OnRemoveAllSelectByGrade(setGrade);
		}

		// Token: 0x06006EE4 RID: 28388 RVA: 0x00249738 File Offset: 0x00247938
		private void OnRemoveAllSelectByGrade(HashSet<NKM_UNIT_GRADE> setGrade)
		{
			foreach (long uid in this.m_listSelectedUnit)
			{
				NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase = this.FindSlotFromCurrentList(uid);
				if (nkcuiunitSelectListSlotBase != null)
				{
					nkcuiunitSelectListSlotBase.SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState.NONE);
				}
			}
			this.m_listSelectedUnit.Clear();
			if (this.m_currentTargetUnitType == NKCUIUnitSelectList.TargetTabType.Operator)
			{
				using (List<NKMOperator>.Enumerator enumerator2 = this.m_OperatorSortSystem.GetCurrentOperatorList().GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						NKMOperator nkmoperator = enumerator2.Current;
						if (nkmoperator != null && !nkmoperator.bLock && nkmoperator.level <= 1 && this.m_OperatorSortSystem.GetUnitSlotState(nkmoperator.uid) == NKCUnitSortSystem.eUnitState.NONE)
						{
							NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(nkmoperator.id);
							if (unitTempletBase != null && setGrade.Contains(unitTempletBase.m_NKM_UNIT_GRADE) && unitTempletBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_TRAINER)
							{
								this.m_listSelectedUnit.Add(nkmoperator.uid);
								NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase2 = this.FindSlotFromCurrentList(nkmoperator.uid);
								if (nkcuiunitSelectListSlotBase2 != null)
								{
									nkcuiunitSelectListSlotBase2.SetSlotSelectState(this.m_currentOption.bShowRemoveItem ? NKCUIUnitSelectList.eUnitSlotSelectState.DELETE : NKCUIUnitSelectList.eUnitSlotSelectState.SELECTED);
								}
								if (this.m_listSelectedUnit.Count >= this.m_currentOption.iMaxMultipleSelect)
								{
									break;
								}
							}
						}
					}
					goto IL_247;
				}
			}
			foreach (NKMUnitData nkmunitData in this.m_ssActive.GetCurrentUnitList())
			{
				if (nkmunitData != null && !nkmunitData.m_bLock && nkmunitData.m_UnitLevel <= 1 && this.m_ssActive.GetUnitSlotState(nkmunitData.m_UnitUID) == NKCUnitSortSystem.eUnitState.NONE)
				{
					NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(nkmunitData);
					if (unitTempletBase2 != null && setGrade.Contains(unitTempletBase2.m_NKM_UNIT_GRADE))
					{
						this.m_listSelectedUnit.Add(nkmunitData.m_UnitUID);
						NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase3 = this.FindSlotFromCurrentList(nkmunitData.m_UnitUID);
						if (nkcuiunitSelectListSlotBase3 != null)
						{
							nkcuiunitSelectListSlotBase3.SetSlotSelectState(this.m_currentOption.bShowRemoveItem ? NKCUIUnitSelectList.eUnitSlotSelectState.DELETE : NKCUIUnitSelectList.eUnitSlotSelectState.SELECTED);
						}
						if (this.m_listSelectedUnit.Count >= this.m_currentOption.iMaxMultipleSelect)
						{
							break;
						}
					}
				}
			}
			IL_247:
			this.UpdateMultipleSelectCountUI();
			this.UpdateMultiSelectGetItemResult();
		}

		// Token: 0x06006EE5 RID: 28389 RVA: 0x002499C0 File Offset: 0x00247BC0
		private void OnSmartRemoveSelectByGrade(HashSet<NKM_UNIT_GRADE> setGrade, bool bIncludeTranscendence)
		{
			foreach (long uid in this.m_listSelectedUnit)
			{
				NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase = this.FindSlotFromCurrentList(uid);
				if (nkcuiunitSelectListSlotBase != null)
				{
					nkcuiunitSelectListSlotBase.SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState.NONE);
				}
			}
			this.m_listSelectedUnit.Clear();
			List<NKMUnitData> currentUnitList = this.m_ssActive.GetCurrentUnitList();
			Dictionary<int, List<NKMUnitData>> dictionary = new Dictionary<int, List<NKMUnitData>>();
			Dictionary<int, List<NKMUnitData>> dictionary2 = new Dictionary<int, List<NKMUnitData>>();
			HashSet<long> hashSet = new HashSet<long>();
			foreach (NKMUnitData nkmunitData in currentUnitList)
			{
				if (nkmunitData != null)
				{
					hashSet.Add(nkmunitData.m_UnitUID);
				}
			}
			foreach (NKMUnitData nkmunitData2 in ((this.m_currentTargetUnitType == NKCUIUnitSelectList.TargetTabType.Ship) ? NKCScenManager.CurrentUserData().m_ArmyData.m_dicMyShip : NKCScenManager.CurrentUserData().m_ArmyData.m_dicMyUnit).Values)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(nkmunitData2);
				if (unitTempletBase != null)
				{
					bool flag = this.m_ssActive.GetDeckIndexCache(nkmunitData2.m_UnitUID, false).m_eDeckType > NKM_DECK_TYPE.NDT_NONE;
					int key;
					if (unitTempletBase.IsRearmUnit)
					{
						NKMUnitTempletBase baseUnit = unitTempletBase.BaseUnit;
						key = ((baseUnit != null && baseUnit.m_bContractable) ? baseUnit.m_BaseUnitID : nkmunitData2.m_UnitID);
					}
					else
					{
						key = nkmunitData2.m_UnitID;
					}
					Dictionary<int, List<NKMUnitData>> dictionary3;
					if (nkmunitData2.IsActiveUnit || flag)
					{
						dictionary3 = dictionary;
					}
					else
					{
						if (!setGrade.Contains(unitTempletBase.m_NKM_UNIT_GRADE))
						{
							continue;
						}
						dictionary3 = dictionary2;
					}
					if (!dictionary3.ContainsKey(key))
					{
						dictionary3[key] = new List<NKMUnitData>();
					}
					dictionary3[key].Add(nkmunitData2);
				}
			}
			Dictionary<int, int> dictionary4 = new Dictionary<int, int>();
			foreach (KeyValuePair<int, List<NKMUnitData>> keyValuePair in dictionary)
			{
				NKMUnitManager.GetUnitTempletBase(keyValuePair.Key);
				int num;
				if (!dictionary4.TryGetValue(keyValuePair.Key, out num))
				{
					num = 0;
				}
				foreach (NKMUnitData nkmunitData3 in keyValuePair.Value)
				{
					num += 6 - nkmunitData3.tacticLevel;
				}
				dictionary4[keyValuePair.Key] = num;
			}
			foreach (KeyValuePair<int, List<NKMUnitData>> keyValuePair2 in dictionary2)
			{
				keyValuePair2.Value.Sort(delegate(NKMUnitData a, NKMUnitData b)
				{
					NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(a);
					NKMUnitTempletBase unitTempletBase3 = NKMUnitManager.GetUnitTempletBase(b);
					bool flag2 = unitTempletBase2 != null && unitTempletBase2.IsRearmUnit;
					bool flag3 = unitTempletBase3 != null && unitTempletBase3.IsRearmUnit;
					if (flag2 != flag3)
					{
						return flag2.CompareTo(flag3);
					}
					return b.FromContract.CompareTo(a.FromContract);
				});
				int num2;
				if (!dictionary4.TryGetValue(keyValuePair2.Key, out num2))
				{
					num2 = 0;
				}
				int num3 = keyValuePair2.Value.Count - num2;
				if (num3 >= 0)
				{
					for (int i = 0; i < num3; i++)
					{
						if (keyValuePair2.Value[i] != null && hashSet.Contains(keyValuePair2.Value[i].m_UnitUID) && this.m_ssActive.GetUnitSlotState(keyValuePair2.Value[i].m_UnitUID) == NKCUnitSortSystem.eUnitState.NONE)
						{
							this.m_listSelectedUnit.Add(keyValuePair2.Value[i].m_UnitUID);
							NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase2 = this.FindSlotFromCurrentList(keyValuePair2.Value[i].m_UnitUID);
							if (nkcuiunitSelectListSlotBase2 != null)
							{
								nkcuiunitSelectListSlotBase2.SetSlotSelectState(this.m_currentOption.bShowRemoveItem ? NKCUIUnitSelectList.eUnitSlotSelectState.DELETE : NKCUIUnitSelectList.eUnitSlotSelectState.SELECTED);
							}
							if (this.m_listSelectedUnit.Count >= this.m_currentOption.iMaxMultipleSelect)
							{
								break;
							}
						}
					}
					if (this.m_listSelectedUnit.Count >= this.m_currentOption.iMaxMultipleSelect)
					{
						break;
					}
				}
			}
			this.UpdateMultipleSelectCountUI();
			this.UpdateMultiSelectGetItemResult();
		}

		// Token: 0x06006EE6 RID: 28390 RVA: 0x00249E58 File Offset: 0x00248058
		private void OnAutoSelectByGrade(NKM_UNIT_GRADE grade)
		{
			HashSet<long> hashSet = new HashSet<long>();
			int num = 0;
			NKCUnitSortSystem.AutoSelectExtraFilter <>9__0;
			while (num + this.m_listSelectedUnit.Count < this.m_currentOption.iMaxMultipleSelect)
			{
				NKCUnitSortSystem ssActive = this.m_ssActive;
				HashSet<long> setExcludeUnitUid = hashSet;
				NKCUnitSortSystem.AutoSelectExtraFilter extrafilter;
				if ((extrafilter = <>9__0) == null)
				{
					extrafilter = (<>9__0 = ((NKMUnitData unit) => this.AutoSelectFilter(unit, grade)));
				}
				NKMUnitData nkmunitData = ssActive.AutoSelect(setExcludeUnitUid, extrafilter);
				if (nkmunitData == null)
				{
					break;
				}
				hashSet.Add(nkmunitData.m_UnitUID);
				if (!this.m_listSelectedUnit.Contains(nkmunitData.m_UnitUID))
				{
					num++;
				}
			}
			if (num > 0)
			{
				using (HashSet<long>.Enumerator enumerator = hashSet.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						long num2 = enumerator.Current;
						if (!this.m_listSelectedUnit.Contains(num2))
						{
							this.m_listSelectedUnit.Add(num2);
							NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase = this.FindSlotFromCurrentList(num2);
							if (nkcuiunitSelectListSlotBase != null)
							{
								nkcuiunitSelectListSlotBase.SetSlotSelectState(this.m_currentOption.bShowRemoveItem ? NKCUIUnitSelectList.eUnitSlotSelectState.DELETE : NKCUIUnitSelectList.eUnitSlotSelectState.SELECTED);
							}
						}
					}
					goto IL_1D7;
				}
			}
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			hashSet.Clear();
			for (int i = 0; i < this.m_listSelectedUnit.Count; i++)
			{
				long num3 = this.m_listSelectedUnit[i];
				NKMUnitData unitFromUID = armyData.GetUnitFromUID(num3);
				if (unitFromUID != null)
				{
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitFromUID.m_UnitID);
					if (unitTempletBase != null && unitTempletBase.m_NKM_UNIT_GRADE == grade)
					{
						hashSet.Add(num3);
					}
				}
			}
			foreach (long num4 in hashSet)
			{
				this.m_listSelectedUnit.Remove(num4);
				NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase2 = this.FindSlotFromCurrentList(num4);
				if (nkcuiunitSelectListSlotBase2 != null)
				{
					nkcuiunitSelectListSlotBase2.SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState.NONE);
				}
			}
			IL_1D7:
			this.UpdateMultipleSelectCountUI();
			this.UpdateMultiSelectGetItemResult();
		}

		// Token: 0x06006EE7 RID: 28391 RVA: 0x0024A064 File Offset: 0x00248264
		private bool AutoSelectFilter(NKMUnitData unitData, NKM_UNIT_GRADE grade = NKM_UNIT_GRADE.NUG_COUNT)
		{
			if (grade != NKM_UNIT_GRADE.NUG_COUNT)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData);
				if (unitTempletBase != null && unitTempletBase.m_NKM_UNIT_GRADE != grade)
				{
					return false;
				}
			}
			return this.m_currentOption.dOnAutoSelectFilter != null && this.m_currentOption.dOnAutoSelectFilter(unitData);
		}

		// Token: 0x06006EE8 RID: 28392 RVA: 0x0024A0AC File Offset: 0x002482AC
		public override void OnBackButton()
		{
			if (this.m_popupSmartSelect != null && this.m_popupSmartSelect.IsOpen)
			{
				this.m_popupSmartSelect.Close();
				return;
			}
			if (this.IsRemoveMode)
			{
				this.OnRemoveMode(false);
				return;
			}
			base.OnBackButton();
			if (this.m_currentOption.dOnClose != null)
			{
				this.m_currentOption.dOnClose();
			}
		}

		// Token: 0x06006EE9 RID: 28393 RVA: 0x0024A113 File Offset: 0x00248313
		public override void UnHide()
		{
			base.UnHide();
			this.SetUnitListAddEffect(false);
			if (!this.m_bDataValid)
			{
				this.ProcessByType(this.m_currentTargetUnitType, true);
				this.SetUnitCount(this.m_currentTargetUnitType);
				this.MoveToScrollRectSelectedUnit();
			}
		}

		// Token: 0x06006EEA RID: 28394 RVA: 0x0024A14C File Offset: 0x0024834C
		public override void OnCloseInstance()
		{
			if (this.m_stkUnitSlotPool != null)
			{
				while (this.m_stkUnitSlotPool.Count > 0)
				{
					UnityEngine.Object.Destroy(this.m_stkUnitSlotPool.Pop().gameObject);
				}
			}
			if (this.m_stkUnitCastingBanSlotPool != null)
			{
				while (this.m_stkUnitCastingBanSlotPool.Count > 0)
				{
					UnityEngine.Object.Destroy(this.m_stkUnitCastingBanSlotPool.Pop().gameObject);
				}
			}
			if (this.m_stkShipSlotPool != null)
			{
				while (this.m_stkShipSlotPool.Count > 0)
				{
					UnityEngine.Object.Destroy(this.m_stkShipSlotPool.Pop().gameObject);
				}
			}
			if (this.m_stkOperCastingBanSlotPool != null)
			{
				while (this.m_stkOperCastingBanSlotPool.Count > 0)
				{
					UnityEngine.Object.Destroy(this.m_stkOperCastingBanSlotPool.Pop().gameObject);
				}
			}
			if (this.m_lstVisibleSlot != null)
			{
				for (int i = 0; i < this.m_lstVisibleSlot.Count; i++)
				{
					UnityEngine.Object.Destroy(this.m_lstVisibleSlot[i].gameObject);
				}
				this.m_lstVisibleSlot.Clear();
			}
		}

		// Token: 0x06006EEB RID: 28395 RVA: 0x0024A24C File Offset: 0x0024844C
		public override void CloseInternal()
		{
			this.SetUnitListAddEffect(false);
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
			this.m_ssActive = null;
			this.m_dicUnitSortSystem.Clear();
			for (int i = 0; i < this.m_listBotSlot.Count; i++)
			{
				UnityEngine.Object.Destroy(this.m_listBotSlot[i].gameObject);
			}
			this.m_listBotSlot.Clear();
		}

		// Token: 0x06006EEC RID: 28396 RVA: 0x0024A2C4 File Offset: 0x002484C4
		private void OnSortChanged(bool bResetScroll)
		{
			if (this.m_currentTargetUnitType == NKCUIUnitSelectList.TargetTabType.Operator && this.m_OperatorSortSystem != null)
			{
				if (this.m_currentOption.bShowRemoveSlot)
				{
					this.m_LoopScrollRect.TotalCount = this.m_OperatorSortSystem.SortedOperatorList.Count + 1;
				}
				else
				{
					this.m_LoopScrollRect.TotalCount = this.m_OperatorSortSystem.SortedOperatorList.Count;
				}
				if (bResetScroll)
				{
					this.m_LoopScrollRect.SetIndexPosition(0);
				}
				else
				{
					this.m_LoopScrollRect.RefreshCells(false);
				}
				NKCUtil.SetLabelText(this.m_lbEmptyMessage, this.m_currentOption.strEmptyMessage);
				NKCUtil.SetGameobjectActive(this.m_objEmpty, this.m_LoopScrollRect.TotalCount == 0);
				return;
			}
			if (this.m_ssActive != null)
			{
				if (this.m_currentOption.bShowRemoveSlot)
				{
					this.m_LoopScrollRect.TotalCount = this.m_ssActive.SortedUnitList.Count + 1;
				}
				else
				{
					this.m_LoopScrollRect.TotalCount = this.m_ssActive.SortedUnitList.Count;
				}
				if (bResetScroll)
				{
					this.m_LoopScrollRect.SetIndexPosition(0);
				}
				else
				{
					this.m_LoopScrollRect.RefreshCells(false);
				}
				NKCUtil.SetLabelText(this.m_lbEmptyMessage, this.m_currentOption.strEmptyMessage);
				NKCUtil.SetGameobjectActive(this.m_objEmpty, this.m_LoopScrollRect.TotalCount == 0);
			}
		}

		// Token: 0x06006EED RID: 28397 RVA: 0x0024A41A File Offset: 0x0024861A
		public void OnSelectUnitMode(bool value)
		{
			if (value)
			{
				this.SetUnitListAddEffect(false);
				this.m_currentOption.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
				this.m_currentOption.strEmptyMessage = NKCUtilString.GET_STRING_UNIT_SELECT_UNIT_NO_EXIST;
				this.ProcessByType(NKCUIUnitSelectList.TargetTabType.Unit, false);
				this.SetUnitCount(NKCUIUnitSelectList.TargetTabType.Unit);
			}
		}

		// Token: 0x06006EEE RID: 28398 RVA: 0x0024A455 File Offset: 0x00248655
		public void OnSelectShipMode(bool value)
		{
			if (value)
			{
				this.SetUnitListAddEffect(false);
				this.m_currentOption.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
				this.m_currentOption.strEmptyMessage = NKCUtilString.GET_STRING_UNIT_SELECT_SHIP_NO_EXIST;
				this.ProcessByType(NKCUIUnitSelectList.TargetTabType.Ship, false);
				this.SetUnitCount(NKCUIUnitSelectList.TargetTabType.Ship);
			}
		}

		// Token: 0x06006EEF RID: 28399 RVA: 0x0024A490 File Offset: 0x00248690
		public void OnSelectOperatorMode(bool value)
		{
			if (!NKCOperatorUtil.IsHide() && !NKCOperatorUtil.IsActive())
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.OPERATOR, 0);
				return;
			}
			if (value)
			{
				this.SetUnitListAddEffect(false);
				this.m_currentOption.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
				this.m_currentOption.strEmptyMessage = NKCUtilString.GET_STRING_UNIT_SELECT_OPERATOR_NO_EXIST;
				this.ProcessByType(NKCUIUnitSelectList.TargetTabType.Operator, false);
				this.SetUnitCount(NKCUIUnitSelectList.TargetTabType.Operator);
			}
			this.CheckTutorial();
		}

		// Token: 0x06006EF0 RID: 28400 RVA: 0x0024A4F4 File Offset: 0x002486F4
		public void OnSelectTrophyMode(bool value)
		{
			if (value)
			{
				this.SetUnitListAddEffect(false);
				this.m_currentOption.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
				this.m_currentOption.strEmptyMessage = NKCUtilString.GET_STRING_UNIT_SELECT_TROPHY_NO_EXIST;
				this.ProcessByType(NKCUIUnitSelectList.TargetTabType.Trophy, false);
				this.SetUnitCount(NKCUIUnitSelectList.TargetTabType.Trophy);
			}
		}

		// Token: 0x06006EF1 RID: 28401 RVA: 0x0024A530 File Offset: 0x00248730
		public void OnExpandInventoryPopup()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			NKM_INVENTORY_EXPAND_TYPE inventoryType = NKM_INVENTORY_EXPAND_TYPE.NIET_NONE;
			NKCPopupInventoryAdd.SliderInfo sliderInfo = default(NKCPopupInventoryAdd.SliderInfo);
			string title;
			int requiredItemCount;
			switch (this.m_currentTargetUnitType)
			{
			case NKCUIUnitSelectList.TargetTabType.Unit:
				inventoryType = NKM_INVENTORY_EXPAND_TYPE.NIET_UNIT;
				title = NKCUtilString.GET_STRING_INVENTORY_UNIT;
				sliderInfo.increaseCount = 5;
				sliderInfo.maxCount = 1100;
				sliderInfo.currentCount = myUserData.m_ArmyData.m_MaxUnitCount;
				requiredItemCount = 100;
				break;
			case NKCUIUnitSelectList.TargetTabType.Ship:
				inventoryType = NKM_INVENTORY_EXPAND_TYPE.NIET_SHIP;
				title = NKCUtilString.GET_STRING_INVENTORY_SHIP;
				sliderInfo.increaseCount = 1;
				sliderInfo.maxCount = 60;
				sliderInfo.currentCount = myUserData.m_ArmyData.m_MaxShipCount;
				requiredItemCount = 100;
				break;
			case NKCUIUnitSelectList.TargetTabType.Operator:
				inventoryType = NKM_INVENTORY_EXPAND_TYPE.NIET_OPERATOR;
				title = NKCUtilString.GET_STRING_INVEITORY_OPERATOR_TITLE;
				sliderInfo.increaseCount = 5;
				sliderInfo.maxCount = 500;
				sliderInfo.currentCount = myUserData.m_ArmyData.m_MaxOperatorCount;
				requiredItemCount = 100;
				break;
			case NKCUIUnitSelectList.TargetTabType.Trophy:
				inventoryType = NKM_INVENTORY_EXPAND_TYPE.NIET_TROPHY;
				title = NKCUtilString.GET_STRING_TROPHY_UNIT;
				sliderInfo.increaseCount = 10;
				sliderInfo.maxCount = 2000;
				sliderInfo.currentCount = myUserData.m_ArmyData.m_MaxTrophyCount;
				requiredItemCount = 50;
				break;
			default:
				return;
			}
			sliderInfo.inventoryType = inventoryType;
			int count = 1;
			int num;
			bool flag = !NKCAdManager.IsAdRewardInventory(inventoryType) || !NKMInventoryManager.CanExpandInventoryByAd(inventoryType, myUserData, count, out num);
			if (!NKMInventoryManager.CanExpandInventory(inventoryType, myUserData, count, out num) && flag)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_CANNOT_EXPAND_INVENTORY), null, "");
				return;
			}
			string expandDesc = NKCUtilString.GetExpandDesc(inventoryType, false);
			NKCPopupInventoryAdd.Instance.Open(title, expandDesc, sliderInfo, requiredItemCount, 101, delegate(int value)
			{
				NKCPacketSender.Send_NKMPacket_INVENTORY_EXPAND_REQ(inventoryType, value);
			}, false);
		}

		// Token: 0x06006EF2 RID: 28402 RVA: 0x0024A702 File Offset: 0x00248902
		public void OnExpandInventory()
		{
			this.SetUnitListAddEffect(false);
			this.SetUnitListAddEffect(true);
		}

		// Token: 0x06006EF3 RID: 28403 RVA: 0x0024A712 File Offset: 0x00248912
		public void UpdateUnitCount()
		{
			this.SetUnitCount(this.m_currentTargetUnitType);
		}

		// Token: 0x06006EF4 RID: 28404 RVA: 0x0024A720 File Offset: 0x00248920
		private void SetUnitCount(NKCUIUnitSelectList.TargetTabType type)
		{
			if (this.m_currentOption.m_UnitSelectListMode != NKCUIUnitSelectList.eUnitSelectListMode.Normal)
			{
				this.SetUnitCount("", "");
				return;
			}
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			switch (type)
			{
			default:
				this.SetUnitCount(string.Format("{0}/{1}", armyData.GetCurrentUnitCount(), armyData.m_MaxUnitCount), NKCUtilString.GET_STRING_UNIT_SELECT_HAVE_COUNT);
				return;
			case NKCUIUnitSelectList.TargetTabType.Ship:
				this.SetUnitCount(string.Format("{0}/{1}", armyData.GetCurrentShipCount(), armyData.m_MaxShipCount), NKCUtilString.GET_STRING_UNIT_SELECT_HAVE_COUNT);
				return;
			case NKCUIUnitSelectList.TargetTabType.Operator:
				this.SetUnitCount(string.Format("{0}/{1}", armyData.GetCurrentOperatorCount(), armyData.m_MaxOperatorCount), NKCUtilString.GET_STRING_UNIT_SELECT_HAVE_COUNT);
				return;
			case NKCUIUnitSelectList.TargetTabType.Trophy:
				this.SetUnitCount(string.Format("{0}/{1}", armyData.GetCurrentTrophyCount(), armyData.m_MaxTrophyCount), NKCUtilString.GET_STRING_UNIT_SELECT_HAVE_COUNT);
				return;
			}
		}

		// Token: 0x06006EF5 RID: 28405 RVA: 0x0024A81E File Offset: 0x00248A1E
		private void SetUnitCount(string unitCnt, string unitCntDesc)
		{
			NKCUtil.SetLabelText(this.m_lbUnitCount, unitCnt);
			NKCUtil.SetLabelText(this.m_lbUnitCountDesc, unitCntDesc);
		}

		// Token: 0x06006EF6 RID: 28406 RVA: 0x0024A838 File Offset: 0x00248A38
		public void ChangeUnitDeckIndex(long UID, NKMDeckIndex deckIndex)
		{
			foreach (KeyValuePair<NKCUIUnitSelectList.TargetTabType, NKCUnitSortSystem> keyValuePair in this.m_dicUnitSortSystem)
			{
				NKCUnitSortSystem value = keyValuePair.Value;
				if (value != null)
				{
					value.SetDeckIndexCache(UID, deckIndex);
				}
			}
			NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase = this.FindSlotFromCurrentList(UID);
			if (nkcuiunitSelectListSlotBase != null)
			{
				nkcuiunitSelectListSlotBase.SetDeckIndex(deckIndex);
			}
		}

		// Token: 0x06006EF7 RID: 28407 RVA: 0x0024A8B0 File Offset: 0x00248AB0
		private NKCUIUnitSelectListSlotBase FindSlotFromCurrentList(long UID)
		{
			if (this.m_currentTargetUnitType == NKCUIUnitSelectList.TargetTabType.Operator)
			{
				return this.m_lstVisibleSlot.Find((NKCUIUnitSelectListSlotBase x) => x.gameObject.activeSelf && x.NKMOperatorData != null && x.NKMOperatorData.uid == UID);
			}
			return this.m_lstVisibleSlot.Find((NKCUIUnitSelectListSlotBase x) => x.gameObject.activeSelf && x.NKMUnitData != null && x.NKMUnitData.m_UnitUID == UID);
		}

		// Token: 0x06006EF8 RID: 28408 RVA: 0x0024A904 File Offset: 0x00248B04
		public override void OnUnitUpdate(NKMUserData.eChangeNotifyType eEventType, NKM_UNIT_TYPE eUnitType, long uid, NKMUnitData unitData)
		{
			NKCUIUnitSelectList.TargetTabType targetTabType;
			switch (eUnitType)
			{
			default:
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData);
				if (unitTempletBase != null && unitTempletBase.IsTrophy)
				{
					targetTabType = NKCUIUnitSelectList.TargetTabType.Trophy;
				}
				else
				{
					targetTabType = NKCUIUnitSelectList.TargetTabType.Unit;
				}
				break;
			}
			case NKM_UNIT_TYPE.NUT_SHIP:
				targetTabType = NKCUIUnitSelectList.TargetTabType.Ship;
				break;
			case NKM_UNIT_TYPE.NUT_OPERATOR:
				targetTabType = NKCUIUnitSelectList.TargetTabType.Operator;
				break;
			}
			switch (eEventType)
			{
			case NKMUserData.eChangeNotifyType.Add:
			case NKMUserData.eChangeNotifyType.Remove:
				if (targetTabType != this.m_currentTargetUnitType)
				{
					this.m_dicUnitSortSystem.Remove(targetTabType);
					return;
				}
				if (this.m_bHide)
				{
					this.m_bDataValid = false;
					return;
				}
				this.ProcessByType(this.m_currentTargetUnitType, true);
				return;
			case NKMUserData.eChangeNotifyType.Update:
				if (this.m_dicUnitSortSystem.ContainsKey(targetTabType))
				{
					this.m_dicUnitSortSystem[targetTabType].UpdateLimitBreakProcessCache();
					this.m_dicUnitSortSystem[targetTabType].UpdateTacticUpdateProcessCache();
					this.m_dicUnitSortSystem[targetTabType].UpdateUnitData(unitData);
				}
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_UNIT_LIST)
				{
					NKCScenManager.GetScenManager().GET_NKC_SCEN_UNIT_LIST().OnUnitUpdate(uid, unitData);
				}
				if (targetTabType == this.m_currentTargetUnitType)
				{
					NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase = this.FindSlotFromCurrentList(unitData.m_UnitUID);
					if (nkcuiunitSelectListSlotBase != null)
					{
						this.SetSlotData(nkcuiunitSelectListSlotBase, unitData);
					}
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x06006EF9 RID: 28409 RVA: 0x0024AA1C File Offset: 0x00248C1C
		public override void OnOperatorUpdate(NKMUserData.eChangeNotifyType eEventType, long uid, NKMOperator operatorData)
		{
			switch (eEventType)
			{
			case NKMUserData.eChangeNotifyType.Add:
			case NKMUserData.eChangeNotifyType.Remove:
				if (NKCUIUnitSelectList.TargetTabType.Operator != this.m_currentTargetUnitType)
				{
					this.m_dicUnitSortSystem.Remove(NKCUIUnitSelectList.TargetTabType.Operator);
					return;
				}
				if (this.m_bHide)
				{
					this.m_bDataValid = false;
					return;
				}
				this.ProcessByType(this.m_currentTargetUnitType, true);
				return;
			case NKMUserData.eChangeNotifyType.Update:
				if (NKCUIUnitSelectList.TargetTabType.Operator == this.m_currentTargetUnitType)
				{
					NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase = this.FindSlotFromCurrentList(uid);
					if (nkcuiunitSelectListSlotBase != null)
					{
						this.SetSlotData(nkcuiunitSelectListSlotBase, operatorData);
					}
					foreach (NKMOperator nkmoperator in this.m_OperatorSortSystem.SortedOperatorList)
					{
						if (nkmoperator.uid == operatorData.uid)
						{
							nkmoperator.level = operatorData.level;
							nkmoperator.exp = operatorData.exp;
							nkmoperator.mainSkill = operatorData.mainSkill;
							nkmoperator.subSkill = operatorData.subSkill;
						}
					}
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x06006EFA RID: 28410 RVA: 0x0024AB18 File Offset: 0x00248D18
		public override void OnDeckUpdate(NKMDeckIndex deckIndex, NKMDeckData deckData)
		{
			this.ProcessByType(this.m_currentTargetUnitType, true);
		}

		// Token: 0x06006EFB RID: 28411 RVA: 0x0024AB28 File Offset: 0x00248D28
		private void UpdateMultiOkButton()
		{
			if (!this.m_currentOption.bMultipleSelect)
			{
				return;
			}
			if (this.m_currentOption.bShowRemoveItem)
			{
				this.m_cbtnMultipleSelectOK_img.sprite = this.m_spriteButton_03;
				this.m_cbtnMultipleSelectOK_text.color = Color.white;
				return;
			}
			this.m_cbtnMultipleSelectOK_img.sprite = this.m_spriteButton_01;
			this.m_cbtnMultipleSelectOK_text.color = NKCUtil.GetColor("#582817");
		}

		// Token: 0x06006EFC RID: 28412 RVA: 0x0024AB98 File Offset: 0x00248D98
		public static NKCUnitSortSystem GetUnitDummySortSystem(NKCUIUnitSelectList.UnitSelectListOptions options)
		{
			NKCUnitSortSystem result;
			switch (options.m_UnitSelectListMode)
			{
			default:
				switch (options.eTargetUnitType)
				{
				default:
					result = new NKCUnitSort(NKCScenManager.CurrentUserData(), options.m_SortOptions);
					break;
				case NKCUIUnitSelectList.TargetTabType.Ship:
					result = new NKCShipSort(NKCScenManager.CurrentUserData(), options.m_SortOptions);
					break;
				case NKCUIUnitSelectList.TargetTabType.Trophy:
					result = new NKCGenericUnitSort(NKCScenManager.CurrentUserData(), options.m_SortOptions, NKCScenManager.CurrentUserData().m_ArmyData.m_dicMyTrophy.Values);
					break;
				}
				break;
			case NKCUIUnitSelectList.eUnitSelectListMode.ALLUNIT_DEV:
				switch (options.eTargetUnitType)
				{
				default:
					result = new NKCAllUnitSort(NKCScenManager.CurrentUserData(), options.m_SortOptions);
					break;
				case NKCUIUnitSelectList.TargetTabType.Ship:
					result = new NKCAllShipSort(NKCScenManager.CurrentUserData(), options.m_SortOptions);
					break;
				case NKCUIUnitSelectList.TargetTabType.Trophy:
				{
					List<NKMUnitData> list = new List<NKMUnitData>();
					foreach (NKMUnitTempletBase nkmunitTempletBase in NKMTempletContainer<NKMUnitTempletBase>.Values)
					{
						if (nkmunitTempletBase.IsTrophy)
						{
							list.Add(NKCUnitSortSystem.MakeTempUnitData(nkmunitTempletBase.m_UnitID, 1, 0));
						}
					}
					result = new NKCGenericUnitSort(NKCScenManager.CurrentUserData(), options.m_SortOptions, list);
					break;
				}
				}
				break;
			case NKCUIUnitSelectList.eUnitSelectListMode.ALLSKIN_DEV:
				switch (options.eTargetUnitType)
				{
				default:
				{
					List<NKMUnitData> list2 = new List<NKMUnitData>();
					foreach (NKMSkinTemplet skinTemplet in NKMSkinManager.m_dicSkinTemplet.Values)
					{
						list2.Add(NKCUnitSortSystem.MakeTempUnitData(skinTemplet, 1, 0));
					}
					result = new NKCGenericUnitSort(NKCScenManager.CurrentUserData(), options.m_SortOptions, list2);
					break;
				}
				case NKCUIUnitSelectList.TargetTabType.Ship:
					result = new NKCAllShipSort(NKCScenManager.CurrentUserData(), options.m_SortOptions);
					break;
				case NKCUIUnitSelectList.TargetTabType.Trophy:
					result = new NKCGenericUnitSort(NKCScenManager.CurrentUserData(), options.m_SortOptions, new List<NKMUnitData>());
					break;
				}
				break;
			}
			return result;
		}

		// Token: 0x06006EFD RID: 28413 RVA: 0x0024ADAC File Offset: 0x00248FAC
		private void SetUnitListAddEffect(bool bActive)
		{
			NKCUtil.SetGameobjectActive(this.m_objUnitListAddEffect, bActive);
		}

		// Token: 0x06006EFE RID: 28414 RVA: 0x0024ADBC File Offset: 0x00248FBC
		public void UpdateOperatorLockState(long operatorUID, bool bLock)
		{
			if (NKCOperatorUtil.IsActive())
			{
				NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase = this.m_lstVisibleSlot.Find((NKCUIUnitSelectListSlotBase x) => x.NKMUnitData.m_UnitUID == operatorUID);
				if (nkcuiunitSelectListSlotBase != null)
				{
					nkcuiunitSelectListSlotBase.SetLock(bLock, false);
				}
			}
		}

		// Token: 0x06006EFF RID: 28415 RVA: 0x0024AE06 File Offset: 0x00249006
		private void MoveToScrollRectSelectedUnit()
		{
			if (this.m_lastSelectedUnitUID == 0L)
			{
				return;
			}
			if (this.m_currentTargetUnitType != NKCUIUnitSelectList.TargetTabType.Unit)
			{
				return;
			}
			if (NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(this.m_lastSelectedUnitUID) == null)
			{
				return;
			}
			this.ScrollToUnitAndGetRect(this.m_lastSelectedUnitUID);
		}

		// Token: 0x06006F00 RID: 28416 RVA: 0x0024AE3F File Offset: 0x0024903F
		public List<long> GetSelectedUnitList()
		{
			return this.m_listSelectedUnit;
		}

		// Token: 0x06006F01 RID: 28417 RVA: 0x0024AE48 File Offset: 0x00249048
		public RectTransform ScrollToUnitAndGetRect(long UID)
		{
			int num = this.m_ssActive.SortedUnitList.FindIndex((NKMUnitData x) => x.m_UnitUID == UID);
			if (num < 0)
			{
				Debug.LogError("Target unit not found!!");
				return null;
			}
			if (this.m_currentOption.bShowRemoveSlot)
			{
				num++;
			}
			this.m_LoopScrollRect.SetIndexPosition(num);
			NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase = this.m_lstVisibleSlot.Find((NKCUIUnitSelectListSlotBase x) => x.NKMUnitData.m_UnitUID == UID);
			if (nkcuiunitSelectListSlotBase == null)
			{
				return null;
			}
			return nkcuiunitSelectListSlotBase.gameObject.GetComponent<RectTransform>();
		}

		// Token: 0x06006F02 RID: 28418 RVA: 0x0024AEDA File Offset: 0x002490DA
		public void CheckTutorial()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.OperatorList, true);
		}

		// Token: 0x04005A06 RID: 23046
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_unit_select_list";

		// Token: 0x04005A07 RID: 23047
		private const string UI_ASSET_NAME = "NKM_UI_UNIT_SELECT_LIST";

		// Token: 0x04005A08 RID: 23048
		private static NKCUIUnitSelectList m_Instance;

		// Token: 0x04005A09 RID: 23049
		private List<int> RESOURCE_LIST = new List<int>();

		// Token: 0x04005A0A RID: 23050
		private bool m_bWillCloseUnderPopupOnOpen = true;

		// Token: 0x04005A0B RID: 23051
		[Header("유닛 슬롯 프리팹 & 사이즈 설정")]
		public NKCUIUnitSelectListSlot m_pfbUnitSlot;

		// Token: 0x04005A0C RID: 23052
		public Vector2 m_vUnitSlotSize;

		// Token: 0x04005A0D RID: 23053
		public Vector2 m_vUnitSlotSpacing;

		// Token: 0x04005A0E RID: 23054
		[Space]
		public NKCUIShipSelectListSlot m_pfbShipSlot;

		// Token: 0x04005A0F RID: 23055
		public Vector2 m_vShipSlotSize;

		// Token: 0x04005A10 RID: 23056
		public Vector2 m_vShipSlotSpacing;

		// Token: 0x04005A11 RID: 23057
		[Space]
		public NKCUIOperatorSelectListSlot m_pfbOperatorSlot;

		// Token: 0x04005A12 RID: 23058
		public Vector2 m_vOperatorSlotSize;

		// Token: 0x04005A13 RID: 23059
		public Vector2 m_vOperatorSlotSpacing;

		// Token: 0x04005A14 RID: 23060
		[Space]
		public NKCUIUnitSelectListSlot m_pfbUnitSlotForCastingBan;

		// Token: 0x04005A15 RID: 23061
		public Vector2 m_vUnitCastingSlotSize;

		// Token: 0x04005A16 RID: 23062
		public Vector2 m_vUnitCastingSlotSpacing;

		// Token: 0x04005A17 RID: 23063
		[Space]
		public NKCUIOperatorSelectListSlot m_pfbOperSlotForCastingBan;

		// Token: 0x04005A18 RID: 23064
		public Vector2 m_vOperCastingSlotSize;

		// Token: 0x04005A19 RID: 23065
		public Vector2 m_vOperCastingSlotSpacing;

		// Token: 0x04005A1A RID: 23066
		[Header("UI Components")]
		public RectTransform m_rectContentRect;

		// Token: 0x04005A1B RID: 23067
		public RectTransform m_rectSlotPoolRect;

		// Token: 0x04005A1C RID: 23068
		public LoopScrollRect m_LoopScrollRect;

		// Token: 0x04005A1D RID: 23069
		public GridLayoutGroup m_GridLayoutGroup;

		// Token: 0x04005A1E RID: 23070
		public NKCUIComSafeArea m_SafeArea;

		// Token: 0x04005A1F RID: 23071
		[Header("보유 유닛 카운트")]
		public GameObject m_objRootUnitCount;

		// Token: 0x04005A20 RID: 23072
		public Text m_lbUnitCountDesc;

		// Token: 0x04005A21 RID: 23073
		public Text m_lbUnitCount;

		// Token: 0x04005A22 RID: 23074
		public NKCUIComStateButton m_NKM_UI_UNIT_SELECT_LIST_POSSESS_ADD;

		// Token: 0x04005A23 RID: 23075
		public GameObject m_objUnitListAddEffect;

		// Token: 0x04005A24 RID: 23076
		[Header("편성 유닛 감추기 토글")]
		public NKCUIComToggle m_ctgHideDeckedUnit;

		// Token: 0x04005A25 RID: 23077
		[Header("다중 선택용 확인창")]
		public GameObject m_objMultipleSelectRoot;

		// Token: 0x04005A26 RID: 23078
		public NKCUIComButton m_cbtnMultipleSelectOK;

		// Token: 0x04005A27 RID: 23079
		public Text m_lbMultipleSelectCount;

		// Token: 0x04005A28 RID: 23080
		public Text m_lbMultipleSelectText;

		// Token: 0x04005A29 RID: 23081
		public GameObject m_objMultipleSelectGetItem;

		// Token: 0x04005A2A RID: 23082
		public Transform m_trMultipleSelectGetItemListContent;

		// Token: 0x04005A2B RID: 23083
		public NKCUIComButton m_btnMultiCancel;

		// Token: 0x04005A2C RID: 23084
		public NKCUIComButton m_btnMultiAuto;

		// Token: 0x04005A2D RID: 23085
		public NKCUIComButton m_btnMultiAutoN;

		// Token: 0x04005A2E RID: 23086
		public NKCUIComButton m_btnMultiAutoR;

		// Token: 0x04005A2F RID: 23087
		public NKCUIUnitSelectListRemovePopup m_popupSmartSelect;

		// Token: 0x04005A30 RID: 23088
		public Image m_cbtnMultipleSelectOK_img;

		// Token: 0x04005A31 RID: 23089
		public Text m_cbtnMultipleSelectOK_text;

		// Token: 0x04005A32 RID: 23090
		public Sprite m_spriteButton_01;

		// Token: 0x04005A33 RID: 23091
		public Sprite m_spriteButton_02;

		// Token: 0x04005A34 RID: 23092
		public Sprite m_spriteButton_03;

		// Token: 0x04005A35 RID: 23093
		[Header("필터/정렬 통합UI")]
		public NKCUIComUnitSortOptions m_SortUI;

		// Token: 0x04005A36 RID: 23094
		[Header("잠금/해고 설정 버튼")]
		public NKCUIComToggle m_ctgLockUnit;

		// Token: 0x04005A37 RID: 23095
		public NKCUIComStateButton m_btnRemoveUnit;

		// Token: 0x04005A38 RID: 23096
		public CanvasGroup m_canvasRemoveUnit;

		// Token: 0x04005A39 RID: 23097
		public GameObject m_objLockMsg;

		// Token: 0x04005A3A RID: 23098
		public GameObject m_objRemoveMsg;

		// Token: 0x04005A3B RID: 23099
		public GameObject m_objBanMsg;

		// Token: 0x04005A3C RID: 23100
		public Text m_txtRemoveMsg;

		// Token: 0x04005A3D RID: 23101
		[Header("상점 숏컷")]
		public NKCUIComStateButton m_btnShopShortcut;

		// Token: 0x04005A3E RID: 23102
		public NKCUIComStateButton m_btnShipBuildShortcut;

		// Token: 0x04005A3F RID: 23103
		[Header("유닛/함선 선택 버튼")]
		public NKCUIComToggle m_tglSelectModeUnit;

		// Token: 0x04005A40 RID: 23104
		public NKCUIComToggle m_tglSelectModeShip;

		// Token: 0x04005A41 RID: 23105
		public NKCUIComToggle m_tglSelectModeOperator;

		// Token: 0x04005A42 RID: 23106
		public NKCUIComToggle m_tglSelectModeTrophy;

		// Token: 0x04005A43 RID: 23107
		[Header("목록이 비었을 때")]
		public GameObject m_objEmpty;

		// Token: 0x04005A44 RID: 23108
		public Text m_lbEmptyMessage;

		// Token: 0x04005A45 RID: 23109
		[Header("오퍼레이터")]
		private NKCOperatorSortSystem m_OperatorSortSystem;

		// Token: 0x04005A46 RID: 23110
		private List<NKCUIUnitSelectListSlotBase> m_lstVisibleSlot = new List<NKCUIUnitSelectListSlotBase>();

		// Token: 0x04005A47 RID: 23111
		private Stack<NKCUIUnitSelectListSlotBase> m_stkUnitSlotPool = new Stack<NKCUIUnitSelectListSlotBase>();

		// Token: 0x04005A48 RID: 23112
		private Stack<NKCUIUnitSelectListSlotBase> m_stkUnitCastingBanSlotPool = new Stack<NKCUIUnitSelectListSlotBase>();

		// Token: 0x04005A49 RID: 23113
		private Stack<NKCUIUnitSelectListSlotBase> m_stkShipSlotPool = new Stack<NKCUIUnitSelectListSlotBase>();

		// Token: 0x04005A4A RID: 23114
		private Stack<NKCUIUnitSelectListSlotBase> m_stkOperSlotPool = new Stack<NKCUIUnitSelectListSlotBase>();

		// Token: 0x04005A4B RID: 23115
		private Stack<NKCUIUnitSelectListSlotBase> m_stkOperCastingBanSlotPool = new Stack<NKCUIUnitSelectListSlotBase>();

		// Token: 0x04005A4C RID: 23116
		private HashSet<NKCUnitSortSystem.eFilterCategory> m_hsFilterCategory = new HashSet<NKCUnitSortSystem.eFilterCategory>();

		// Token: 0x04005A4D RID: 23117
		private HashSet<NKCUnitSortSystem.eSortCategory> m_hsSortCategory = new HashSet<NKCUnitSortSystem.eSortCategory>();

		// Token: 0x04005A4E RID: 23118
		private NKCUnitSortSystem m_ssActive;

		// Token: 0x04005A4F RID: 23119
		private Dictionary<NKCUIUnitSelectList.TargetTabType, NKCUnitSortSystem> m_dicUnitSortSystem = new Dictionary<NKCUIUnitSelectList.TargetTabType, NKCUnitSortSystem>();

		// Token: 0x04005A50 RID: 23120
		private long m_lastSelectedUnitUID;

		// Token: 0x04005A51 RID: 23121
		private bool m_bKeepSortFilterOptions;

		// Token: 0x04005A52 RID: 23122
		private NKMUnitData m_BeforeUnit;

		// Token: 0x04005A53 RID: 23123
		private NKMOperator m_BeforeOperator;

		// Token: 0x04005A54 RID: 23124
		private NKMDeckIndex m_BeforeUnitDeckIndex;

		// Token: 0x04005A55 RID: 23125
		private NKCUIUnitSelectList.UnitSelectListOptions m_currentOption;

		// Token: 0x04005A56 RID: 23126
		private List<long> m_listSelectedUnit = new List<long>();

		// Token: 0x04005A57 RID: 23127
		private List<NKCUISlot> m_listBotSlot = new List<NKCUISlot>();

		// Token: 0x04005A58 RID: 23128
		private NKCUIRectMove m_NKM_UI_UNIT_SELECT_LIST_UNIT;

		// Token: 0x04005A59 RID: 23129
		private NKCUIUnitSelectList.OnUnitSelectCommand m_dOnUnitSelectCommand;

		// Token: 0x04005A5A RID: 23130
		private NKCUIUnitSelectList.OnUnitSortList m_dOnUnitSortList;

		// Token: 0x04005A5B RID: 23131
		private NKCUIUnitSelectList.OnOperatorSortList m_dOnOperatorSortList;

		// Token: 0x04005A5C RID: 23132
		private NKCUIUnitSelectList.OnUnitSortOption m_dOnUnitSortOption;

		// Token: 0x04005A5D RID: 23133
		private NKCUIUnitSelectList.TargetTabType m_currentTargetUnitType;

		// Token: 0x04005A5E RID: 23134
		private bool m_bLockModeEnabled;

		// Token: 0x04005A5F RID: 23135
		private bool m_bCellPrepared;

		// Token: 0x04005A60 RID: 23136
		private bool m_bPrevMultiple;

		// Token: 0x04005A61 RID: 23137
		private bool m_bIsReturnCastingBanSlot;

		// Token: 0x04005A62 RID: 23138
		private bool m_bDataValid;

		// Token: 0x04005A63 RID: 23139
		private bool m_bInit;

		// Token: 0x04005A64 RID: 23140
		private const int REMOVE_UNIT_MAX = 1000;

		// Token: 0x04005A65 RID: 23141
		private const int REMOVE_SHIP_MAX = 100;

		// Token: 0x04005A66 RID: 23142
		[Header("오픈애니메이터")]
		public Animator m_Animator;

		// Token: 0x04005A67 RID: 23143
		private List<NKMUnitData> m_currentUnitList = new List<NKMUnitData>();

		// Token: 0x04005A68 RID: 23144
		private NKCUIUnitSelectList.UnitSelectListOptions m_prevOption = new NKCUIUnitSelectList.UnitSelectListOptions(NKM_UNIT_TYPE.NUT_NORMAL, false, NKM_DECK_TYPE.NDT_NORMAL, NKCUIUnitSelectList.eUnitSelectListMode.Normal, false);

		// Token: 0x04005A69 RID: 23145
		private NKCUIUnitSelectList.OnUnitSelectCommand m_prevOnUnitSelectCommand;

		// Token: 0x02001713 RID: 5907
		public enum TargetTabType
		{
			// Token: 0x0400A5E2 RID: 42466
			Unit,
			// Token: 0x0400A5E3 RID: 42467
			Ship,
			// Token: 0x0400A5E4 RID: 42468
			Operator,
			// Token: 0x0400A5E5 RID: 42469
			Trophy
		}

		// Token: 0x02001714 RID: 5908
		public enum eUnitSlotSelectState
		{
			// Token: 0x0400A5E7 RID: 42471
			NONE,
			// Token: 0x0400A5E8 RID: 42472
			SELECTED,
			// Token: 0x0400A5E9 RID: 42473
			DISABLE,
			// Token: 0x0400A5EA RID: 42474
			DELETE
		}

		// Token: 0x02001715 RID: 5909
		public enum eUnitSelectListMode
		{
			// Token: 0x0400A5EC RID: 42476
			Normal,
			// Token: 0x0400A5ED RID: 42477
			CUSTOM_LIST,
			// Token: 0x0400A5EE RID: 42478
			ALLUNIT_DEV,
			// Token: 0x0400A5EF RID: 42479
			ALLSKIN_DEV
		}

		// Token: 0x02001716 RID: 5910
		public struct UnitSelectListOptions
		{
			// Token: 0x0600B22E RID: 45614 RVA: 0x00361EFF File Offset: 0x003600FF
			public UnitSelectListOptions(NKM_UNIT_TYPE unitType, bool _bMultipleSelect, NKM_DECK_TYPE _eDeckType, NKCUIUnitSelectList.eUnitSelectListMode UIMode = NKCUIUnitSelectList.eUnitSelectListMode.Normal, bool bUseDefaultString = true)
			{
				this = new NKCUIUnitSelectList.UnitSelectListOptions(NKCUIUnitSelectList.ConvertTabType(unitType), _bMultipleSelect, _eDeckType, UIMode, bUseDefaultString);
			}

			// Token: 0x0600B22F RID: 45615 RVA: 0x00361F14 File Offset: 0x00360114
			public UnitSelectListOptions(NKCUIUnitSelectList.TargetTabType targetUnitType, bool _bMultipleSelect, NKM_DECK_TYPE _eDeckType, NKCUIUnitSelectList.eUnitSelectListMode UIMode = NKCUIUnitSelectList.eUnitSelectListMode.Normal, bool bUseDefaultString = true)
			{
				this.m_UnitSelectListMode = UIMode;
				this.eTargetUnitType = targetUnitType;
				this.bShowUnitShipChangeMenu = false;
				this.m_bHideUnitCount = false;
				this.bShowRemoveSlot = false;
				this.bMultipleSelect = _bMultipleSelect;
				this.iMaxMultipleSelect = 8;
				this.dOnAutoSelectFilter = null;
				this.bUseRemoveSmartAutoSelect = false;
				this.bEnableLockUnitSystem = false;
				this.bEnableRemoveUnitSystem = false;
				this.ShopShortcutTargetTab = null;
				this.bShowHideDeckedUnitMenu = true;
				this.bShowRemoveItem = false;
				this.bShowBanMsg = false;
				this.m_bUseFavorite = false;
				this.dOnSelectedUnitWarning = null;
				this.bCanSelectUnitInMission = false;
				this.bOpenedAtRearmExtract = false;
				if (bUseDefaultString)
				{
					this.strUpsideMenuName = NKCUtilString.GET_STRING_UNIT_SELECT;
					this.strEmptyMessage = NKCUtilString.GET_STRING_UNIT_SELECT_UNIT_NO_EXIST;
				}
				else
				{
					this.strUpsideMenuName = "";
					this.strEmptyMessage = "";
				}
				this.strGuideTempletID = "";
				this.eUpsideMenuMode = NKCUIUpsideMenu.eMode.Invalid;
				this.dOnClose = null;
				this.setSelectedUnitUID = null;
				this.setDisabledUnitUID = null;
				this.setUnitFilterCategory = new HashSet<NKCUnitSortSystem.eFilterCategory>();
				this.setUnitSortCategory = new HashSet<NKCUnitSortSystem.eSortCategory>();
				this.setShipFilterCategory = new HashSet<NKCUnitSortSystem.eFilterCategory>();
				this.setShipSortCategory = new HashSet<NKCUnitSortSystem.eSortCategory>();
				this.setOperatorFilterCategory = new HashSet<NKCOperatorSortSystem.eFilterCategory>();
				this.setOperatorSortCategory = new HashSet<NKCOperatorSortSystem.eSortCategory>();
				this.beforeUnitDeckIndex = NKMDeckIndex.None;
				this.beforeUnit = null;
				this.beforeOperator = null;
				this.dOnSlotSetData = null;
				this.dOnSlotOperatorSetData = null;
				this.m_SortOptions.eDeckType = _eDeckType;
				this.m_SortOptions.lstDeckTypeOrder = null;
				this.m_SortOptions.bHideDeckedUnit = false;
				this.m_SortOptions.bDescending = true;
				this.m_SortOptions.bPushBackUnselectable = true;
				List<NKCUnitSortSystem.eSortOption> sortOptions = new List<NKCUnitSortSystem.eSortOption>();
				switch (targetUnitType)
				{
				default:
					this.m_SortOptions.lstSortOption = NKCUnitSortSystem.AddDefaultSortOptions(sortOptions, NKM_UNIT_TYPE.NUT_NORMAL, false);
					break;
				case NKCUIUnitSelectList.TargetTabType.Ship:
					this.m_SortOptions.lstSortOption = NKCUnitSortSystem.AddDefaultSortOptions(sortOptions, NKM_UNIT_TYPE.NUT_SHIP, false);
					break;
				case NKCUIUnitSelectList.TargetTabType.Operator:
					this.m_SortOptions.lstSortOption = NKCUnitSortSystem.AddDefaultSortOptions(sortOptions, NKM_UNIT_TYPE.NUT_OPERATOR, false);
					break;
				}
				this.m_SortOptions.lstDefaultSortOption = null;
				this.m_SortOptions.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
				this.m_SortOptions.lstCustomSortFunc = new Dictionary<NKCUnitSortSystem.eSortCategory, KeyValuePair<string, NKCUnitSortSystem.NKCDataComparerer<NKMUnitData>.CompareFunc>>();
				this.m_SortOptions.setOnlyIncludeFilterOption = null;
				this.m_SortOptions.PreemptiveSortFunc = null;
				this.m_SortOptions.AdditionalExcludeFilterFunc = null;
				this.m_SortOptions.bExcludeLockedUnit = false;
				this.m_SortOptions.bExcludeDeckedUnit = false;
				this.m_SortOptions.setExcludeUnitUID = null;
				this.m_SortOptions.setExcludeUnitID = null;
				this.m_SortOptions.setExcludeUnitBaseID = null;
				this.m_SortOptions.setOnlyIncludeUnitID = null;
				this.m_SortOptions.setOnlyIncludeUnitBaseID = null;
				this.m_SortOptions.setDuplicateUnitID = null;
				this.m_SortOptions.bIncludeUndeckableUnit = true;
				this.m_SortOptions.bIncludeSeizure = false;
				this.m_SortOptions.bUseDeckedState = false;
				this.m_SortOptions.bUseLockedState = false;
				this.m_SortOptions.bUseLobbyState = false;
				this.m_SortOptions.bUseDormInState = false;
				this.m_SortOptions.bIgnoreCityState = false;
				this.m_SortOptions.bIgnoreWorldMapLeader = false;
				this.m_SortOptions.AdditionalUnitStateFunc = null;
				this.m_SortOptions.bIgnoreMissionState = false;
				this.m_IncludeUnitUID = 0L;
				this.m_strCachingUIName = "";
				this.m_OperatorSortOptions = default(NKCOperatorSortSystem.OperatorListOptions);
				this.m_OperatorSortOptions.eDeckType = _eDeckType;
				this.m_OperatorSortOptions.lstSortOption = new List<NKCOperatorSortSystem.eSortOption>();
				this.m_OperatorSortOptions.lstSortOption = NKCOperatorSortSystem.AddDefaultSortOptions(this.m_OperatorSortOptions.lstSortOption, false);
				this.m_OperatorSortOptions.lstDefaultSortOption = null;
				this.m_OperatorSortOptions.setFilterOption = new HashSet<NKCOperatorSortSystem.eFilterOption>();
				this.m_OperatorSortOptions.lstCustomSortFunc = new Dictionary<NKCOperatorSortSystem.eSortCategory, KeyValuePair<string, NKCUnitSortSystem.NKCDataComparerer<NKMOperator>.CompareFunc>>();
				this.m_OperatorSortOptions.SetBuildOption(true, new BUILD_OPTIONS[]
				{
					BUILD_OPTIONS.DESCENDING,
					BUILD_OPTIONS.PUSHBACK_UNSELECTABLE,
					BUILD_OPTIONS.INCLUDE_UNDECKABLE_UNIT
				});
				this.m_bShowShipBuildShortcut = false;
			}

			// Token: 0x17001927 RID: 6439
			// (get) Token: 0x0600B230 RID: 45616 RVA: 0x003622C9 File Offset: 0x003604C9
			// (set) Token: 0x0600B231 RID: 45617 RVA: 0x003622D6 File Offset: 0x003604D6
			public NKM_DECK_TYPE eDeckType
			{
				get
				{
					return this.m_SortOptions.eDeckType;
				}
				set
				{
					this.m_SortOptions.eDeckType = value;
				}
			}

			// Token: 0x17001928 RID: 6440
			// (get) Token: 0x0600B232 RID: 45618 RVA: 0x003622E4 File Offset: 0x003604E4
			// (set) Token: 0x0600B233 RID: 45619 RVA: 0x003622F1 File Offset: 0x003604F1
			public HashSet<NKCUnitSortSystem.eFilterOption> setFilterOption
			{
				get
				{
					return this.m_SortOptions.setFilterOption;
				}
				set
				{
					this.m_SortOptions.setFilterOption = value;
				}
			}

			// Token: 0x17001929 RID: 6441
			// (get) Token: 0x0600B234 RID: 45620 RVA: 0x003622FF File Offset: 0x003604FF
			// (set) Token: 0x0600B235 RID: 45621 RVA: 0x0036230C File Offset: 0x0036050C
			public HashSet<NKCOperatorSortSystem.eFilterOption> setOperatorFilterOption
			{
				get
				{
					return this.m_OperatorSortOptions.setFilterOption;
				}
				set
				{
					this.m_OperatorSortOptions.setFilterOption = value;
				}
			}

			// Token: 0x1700192A RID: 6442
			// (get) Token: 0x0600B236 RID: 45622 RVA: 0x0036231A File Offset: 0x0036051A
			// (set) Token: 0x0600B237 RID: 45623 RVA: 0x00362327 File Offset: 0x00360527
			public List<NKCUnitSortSystem.eSortOption> lstSortOption
			{
				get
				{
					return this.m_SortOptions.lstSortOption;
				}
				set
				{
					this.m_SortOptions.lstSortOption = value;
				}
			}

			// Token: 0x1700192B RID: 6443
			// (get) Token: 0x0600B238 RID: 45624 RVA: 0x00362335 File Offset: 0x00360535
			// (set) Token: 0x0600B239 RID: 45625 RVA: 0x00362342 File Offset: 0x00360542
			public List<NKCOperatorSortSystem.eSortOption> lstOperatorSortOption
			{
				get
				{
					return this.m_OperatorSortOptions.lstSortOption;
				}
				set
				{
					this.m_OperatorSortOptions.lstSortOption = value;
				}
			}

			// Token: 0x1700192C RID: 6444
			// (get) Token: 0x0600B23A RID: 45626 RVA: 0x00362350 File Offset: 0x00360550
			// (set) Token: 0x0600B23B RID: 45627 RVA: 0x0036235D File Offset: 0x0036055D
			public bool bDescending
			{
				get
				{
					return this.m_SortOptions.bDescending;
				}
				set
				{
					this.m_SortOptions.bDescending = value;
				}
			}

			// Token: 0x1700192D RID: 6445
			// (get) Token: 0x0600B23C RID: 45628 RVA: 0x0036236B File Offset: 0x0036056B
			// (set) Token: 0x0600B23D RID: 45629 RVA: 0x00362378 File Offset: 0x00360578
			public bool bPushBackUnselectable
			{
				get
				{
					return this.m_SortOptions.bPushBackUnselectable;
				}
				set
				{
					this.m_SortOptions.bPushBackUnselectable = value;
				}
			}

			// Token: 0x1700192E RID: 6446
			// (get) Token: 0x0600B23E RID: 45630 RVA: 0x00362386 File Offset: 0x00360586
			// (set) Token: 0x0600B23F RID: 45631 RVA: 0x00362393 File Offset: 0x00360593
			public bool bHideDeckedUnit
			{
				get
				{
					return this.m_SortOptions.bHideDeckedUnit;
				}
				set
				{
					this.m_SortOptions.bHideDeckedUnit = value;
				}
			}

			// Token: 0x1700192F RID: 6447
			// (get) Token: 0x0600B240 RID: 45632 RVA: 0x003623A1 File Offset: 0x003605A1
			// (set) Token: 0x0600B241 RID: 45633 RVA: 0x003623AE File Offset: 0x003605AE
			public bool bExcludeLockedUnit
			{
				get
				{
					return this.m_SortOptions.bExcludeLockedUnit;
				}
				set
				{
					this.m_SortOptions.bExcludeLockedUnit = value;
				}
			}

			// Token: 0x17001930 RID: 6448
			// (get) Token: 0x0600B242 RID: 45634 RVA: 0x003623BC File Offset: 0x003605BC
			// (set) Token: 0x0600B243 RID: 45635 RVA: 0x003623C9 File Offset: 0x003605C9
			public bool bExcludeDeckedUnit
			{
				get
				{
					return this.m_SortOptions.bExcludeDeckedUnit;
				}
				set
				{
					this.m_SortOptions.bExcludeDeckedUnit = value;
				}
			}

			// Token: 0x17001931 RID: 6449
			// (get) Token: 0x0600B244 RID: 45636 RVA: 0x003623D7 File Offset: 0x003605D7
			// (set) Token: 0x0600B245 RID: 45637 RVA: 0x003623E4 File Offset: 0x003605E4
			public HashSet<long> setExcludeUnitUID
			{
				get
				{
					return this.m_SortOptions.setExcludeUnitUID;
				}
				set
				{
					this.m_SortOptions.setExcludeUnitUID = value;
				}
			}

			// Token: 0x17001932 RID: 6450
			// (get) Token: 0x0600B246 RID: 45638 RVA: 0x003623F2 File Offset: 0x003605F2
			// (set) Token: 0x0600B247 RID: 45639 RVA: 0x003623FF File Offset: 0x003605FF
			public HashSet<long> setExcludeOperatorUID
			{
				get
				{
					return this.m_OperatorSortOptions.setExcludeOperatorUID;
				}
				set
				{
					this.m_OperatorSortOptions.setExcludeOperatorUID = value;
				}
			}

			// Token: 0x17001933 RID: 6451
			// (get) Token: 0x0600B248 RID: 45640 RVA: 0x0036240D File Offset: 0x0036060D
			// (set) Token: 0x0600B249 RID: 45641 RVA: 0x0036241A File Offset: 0x0036061A
			public HashSet<int> setExcludeUnitID
			{
				get
				{
					return this.m_SortOptions.setExcludeUnitID;
				}
				set
				{
					this.m_SortOptions.setExcludeUnitID = value;
				}
			}

			// Token: 0x17001934 RID: 6452
			// (get) Token: 0x0600B24A RID: 45642 RVA: 0x00362428 File Offset: 0x00360628
			// (set) Token: 0x0600B24B RID: 45643 RVA: 0x00362435 File Offset: 0x00360635
			public HashSet<int> setExcludeUnitBaseID
			{
				get
				{
					return this.m_SortOptions.setExcludeUnitBaseID;
				}
				set
				{
					this.m_SortOptions.setExcludeUnitBaseID = value;
				}
			}

			// Token: 0x17001935 RID: 6453
			// (get) Token: 0x0600B24C RID: 45644 RVA: 0x00362443 File Offset: 0x00360643
			// (set) Token: 0x0600B24D RID: 45645 RVA: 0x00362450 File Offset: 0x00360650
			public HashSet<int> setOnlyIncludeUnitID
			{
				get
				{
					return this.m_SortOptions.setOnlyIncludeUnitID;
				}
				set
				{
					this.m_SortOptions.setOnlyIncludeUnitID = value;
				}
			}

			// Token: 0x17001936 RID: 6454
			// (get) Token: 0x0600B24E RID: 45646 RVA: 0x0036245E File Offset: 0x0036065E
			// (set) Token: 0x0600B24F RID: 45647 RVA: 0x0036246B File Offset: 0x0036066B
			public HashSet<int> setOnlyIncludeUnitBaseID
			{
				get
				{
					return this.m_SortOptions.setOnlyIncludeUnitBaseID;
				}
				set
				{
					this.m_SortOptions.setOnlyIncludeUnitBaseID = value;
				}
			}

			// Token: 0x17001937 RID: 6455
			// (get) Token: 0x0600B250 RID: 45648 RVA: 0x00362479 File Offset: 0x00360679
			// (set) Token: 0x0600B251 RID: 45649 RVA: 0x00362486 File Offset: 0x00360686
			public HashSet<int> setDuplicateUnitID
			{
				get
				{
					return this.m_SortOptions.setDuplicateUnitID;
				}
				set
				{
					this.m_SortOptions.setDuplicateUnitID = value;
				}
			}

			// Token: 0x17001938 RID: 6456
			// (get) Token: 0x0600B252 RID: 45650 RVA: 0x00362494 File Offset: 0x00360694
			// (set) Token: 0x0600B253 RID: 45651 RVA: 0x003624A1 File Offset: 0x003606A1
			public bool bIncludeUndeckableUnit
			{
				get
				{
					return this.m_SortOptions.bIncludeUndeckableUnit;
				}
				set
				{
					this.m_SortOptions.bIncludeUndeckableUnit = value;
				}
			}

			// Token: 0x0400A5F0 RID: 42480
			public NKCUIUnitSelectList.eUnitSelectListMode m_UnitSelectListMode;

			// Token: 0x0400A5F1 RID: 42481
			public NKCUnitSortSystem.UnitListOptions m_SortOptions;

			// Token: 0x0400A5F2 RID: 42482
			public NKCOperatorSortSystem.OperatorListOptions m_OperatorSortOptions;

			// Token: 0x0400A5F3 RID: 42483
			public NKCUIUnitSelectList.TargetTabType eTargetUnitType;

			// Token: 0x0400A5F4 RID: 42484
			public bool bShowUnitShipChangeMenu;

			// Token: 0x0400A5F5 RID: 42485
			public bool m_bHideUnitCount;

			// Token: 0x0400A5F6 RID: 42486
			public bool bShowRemoveSlot;

			// Token: 0x0400A5F7 RID: 42487
			public bool bMultipleSelect;

			// Token: 0x0400A5F8 RID: 42488
			public int iMaxMultipleSelect;

			// Token: 0x0400A5F9 RID: 42489
			public NKCUIUnitSelectList.UnitSelectListOptions.OnAutoSelectFilter dOnAutoSelectFilter;

			// Token: 0x0400A5FA RID: 42490
			public bool bUseRemoveSmartAutoSelect;

			// Token: 0x0400A5FB RID: 42491
			public bool bEnableLockUnitSystem;

			// Token: 0x0400A5FC RID: 42492
			public bool bShowHideDeckedUnitMenu;

			// Token: 0x0400A5FD RID: 42493
			public bool bEnableRemoveUnitSystem;

			// Token: 0x0400A5FE RID: 42494
			public string ShopShortcutTargetTab;

			// Token: 0x0400A5FF RID: 42495
			public bool bShowRemoveItem;

			// Token: 0x0400A600 RID: 42496
			public bool bShowBanMsg;

			// Token: 0x0400A601 RID: 42497
			public NKCUIUnitSelectList.UnitSelectListOptions.OnSelectedUnitWarning dOnSelectedUnitWarning;

			// Token: 0x0400A602 RID: 42498
			public bool bCanSelectUnitInMission;

			// Token: 0x0400A603 RID: 42499
			public bool bOpenedAtRearmExtract;

			// Token: 0x0400A604 RID: 42500
			public string strUpsideMenuName;

			// Token: 0x0400A605 RID: 42501
			public string strEmptyMessage;

			// Token: 0x0400A606 RID: 42502
			public string strGuideTempletID;

			// Token: 0x0400A607 RID: 42503
			public NKCUIUpsideMenu.eMode eUpsideMenuMode;

			// Token: 0x0400A608 RID: 42504
			public NKCUIUnitSelectList.UnitSelectListOptions.OnClose dOnClose;

			// Token: 0x0400A609 RID: 42505
			public NKMDeckIndex beforeUnitDeckIndex;

			// Token: 0x0400A60A RID: 42506
			public NKMUnitData beforeUnit;

			// Token: 0x0400A60B RID: 42507
			public NKMOperator beforeOperator;

			// Token: 0x0400A60C RID: 42508
			public HashSet<long> setSelectedUnitUID;

			// Token: 0x0400A60D RID: 42509
			public HashSet<long> setDisabledUnitUID;

			// Token: 0x0400A60E RID: 42510
			public HashSet<NKCUnitSortSystem.eFilterCategory> setUnitFilterCategory;

			// Token: 0x0400A60F RID: 42511
			public HashSet<NKCUnitSortSystem.eSortCategory> setUnitSortCategory;

			// Token: 0x0400A610 RID: 42512
			public HashSet<NKCUnitSortSystem.eFilterCategory> setShipFilterCategory;

			// Token: 0x0400A611 RID: 42513
			public HashSet<NKCUnitSortSystem.eSortCategory> setShipSortCategory;

			// Token: 0x0400A612 RID: 42514
			public HashSet<NKCOperatorSortSystem.eFilterCategory> setOperatorFilterCategory;

			// Token: 0x0400A613 RID: 42515
			public HashSet<NKCOperatorSortSystem.eSortCategory> setOperatorSortCategory;

			// Token: 0x0400A614 RID: 42516
			public NKCUIUnitSelectList.UnitSelectListOptions.OnSlotSetData dOnSlotSetData;

			// Token: 0x0400A615 RID: 42517
			public NKCUIUnitSelectList.UnitSelectListOptions.OnSlotOperatorSetData dOnSlotOperatorSetData;

			// Token: 0x0400A616 RID: 42518
			public long m_IncludeUnitUID;

			// Token: 0x0400A617 RID: 42519
			public string m_strCachingUIName;

			// Token: 0x0400A618 RID: 42520
			public bool m_bUseFavorite;

			// Token: 0x0400A619 RID: 42521
			public bool m_bShowShipBuildShortcut;

			// Token: 0x02001A8A RID: 6794
			// (Invoke) Token: 0x0600BC45 RID: 48197
			public delegate void OnSlotSetData(NKCUIUnitSelectListSlotBase cUnitSlot, NKMUnitData cNKMUnitData, NKMDeckIndex deckIndex);

			// Token: 0x02001A8B RID: 6795
			// (Invoke) Token: 0x0600BC49 RID: 48201
			public delegate void OnSlotOperatorSetData(NKCUIUnitSelectListSlotBase cUnitSlot, NKMOperator operatorData, NKMDeckIndex deckIndex);

			// Token: 0x02001A8C RID: 6796
			// (Invoke) Token: 0x0600BC4D RID: 48205
			public delegate bool OnAutoSelectFilter(NKMUnitData unitData);

			// Token: 0x02001A8D RID: 6797
			// (Invoke) Token: 0x0600BC51 RID: 48209
			public delegate bool OnSelectedUnitWarning(long unitUID, List<long> selectedUnitList, out string msg);

			// Token: 0x02001A8E RID: 6798
			// (Invoke) Token: 0x0600BC55 RID: 48213
			public delegate void OnClose();
		}

		// Token: 0x02001717 RID: 5911
		// (Invoke) Token: 0x0600B255 RID: 45653
		public delegate void OnUnitSelectCommand(List<long> unitUID);

		// Token: 0x02001718 RID: 5912
		// (Invoke) Token: 0x0600B259 RID: 45657
		public delegate void OnUnitSortList(long unitUID, List<NKMUnitData> unitUIDList);

		// Token: 0x02001719 RID: 5913
		// (Invoke) Token: 0x0600B25D RID: 45661
		public delegate void OnOperatorSortList(long unitUID, List<NKMOperator> operatorUIDList);

		// Token: 0x0200171A RID: 5914
		// (Invoke) Token: 0x0600B261 RID: 45665
		public delegate void OnUnitSortOption(NKCUnitSortSystem.UnitListOptions unitOption);
	}
}
