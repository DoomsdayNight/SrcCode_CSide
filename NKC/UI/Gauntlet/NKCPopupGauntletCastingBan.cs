using System;
using System.Collections.Generic;
using System.Linq;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B61 RID: 2913
	public class NKCPopupGauntletCastingBan : NKCUIBase
	{
		// Token: 0x17001599 RID: 5529
		// (get) Token: 0x060084F5 RID: 34037 RVA: 0x002CEC71 File Offset: 0x002CCE71
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700159A RID: 5530
		// (get) Token: 0x060084F6 RID: 34038 RVA: 0x002CEC74 File Offset: 0x002CCE74
		public override string MenuName
		{
			get
			{
				return "PopupGauntletBanList";
			}
		}

		// Token: 0x1700159B RID: 5531
		// (get) Token: 0x060084F7 RID: 34039 RVA: 0x002CEC7C File Offset: 0x002CCE7C
		public static NKCPopupGauntletCastingBan Instance
		{
			get
			{
				if (NKCPopupGauntletCastingBan.m_Instance == null)
				{
					NKCPopupGauntletCastingBan.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupGauntletCastingBan>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_POPUP_BANNED_LIST_CASTING", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupGauntletCastingBan.CleanupInstance)).GetInstance<NKCPopupGauntletCastingBan>();
					NKCPopupGauntletCastingBan.m_Instance.InitUI();
				}
				return NKCPopupGauntletCastingBan.m_Instance;
			}
		}

		// Token: 0x1700159C RID: 5532
		// (get) Token: 0x060084F8 RID: 34040 RVA: 0x002CECCB File Offset: 0x002CCECB
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupGauntletCastingBan.m_Instance != null && NKCPopupGauntletCastingBan.m_Instance.IsOpen;
			}
		}

		// Token: 0x060084F9 RID: 34041 RVA: 0x002CECE6 File Offset: 0x002CCEE6
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupGauntletCastingBan.m_Instance != null && NKCPopupGauntletCastingBan.m_Instance.IsOpen)
			{
				NKCPopupGauntletCastingBan.m_Instance.Close();
			}
		}

		// Token: 0x060084FA RID: 34042 RVA: 0x002CED0B File Offset: 0x002CCF0B
		private static void CleanupInstance()
		{
			NKCPopupGauntletCastingBan.m_Instance = null;
		}

		// Token: 0x060084FB RID: 34043 RVA: 0x002CED14 File Offset: 0x002CCF14
		public void InitUI()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			NKCUtil.SetEventTriggerDelegate(this.m_etBG, new UnityAction(base.Close));
			NKCUtil.SetBindFunction(this.m_csbtnVote, new UnityAction(this.OnClickSelectList));
			NKCUtil.SetBindFunction(this.m_csbtnClose, new UnityAction(base.Close));
			if (null != this.m_lvsrUnit)
			{
				this.m_lvsrUnit.dOnGetObject += this.GetUnitSlot;
				this.m_lvsrUnit.dOnReturnObject += this.ReturnUnitSlot;
				this.m_lvsrUnit.dOnProvideData += this.ProvideUnitSlotData;
				NKCUtil.SetScrollHotKey(this.m_lvsrUnit, null);
				this.m_lvsrUnit.PrepareCells(0);
			}
			if (null != this.m_lvsrShip)
			{
				this.m_lvsrShip.dOnGetObject += this.GetShipSlot;
				this.m_lvsrShip.dOnReturnObject += this.ReturnShipSlot;
				this.m_lvsrShip.dOnProvideData += this.ProvideShipSlotData;
				NKCUtil.SetScrollHotKey(this.m_lvsrShip, null);
				this.m_lvsrShip.PrepareCells(0);
			}
			if (null != this.m_lvsrOper)
			{
				this.m_lvsrOper.dOnGetObject += this.GetOperSlot;
				this.m_lvsrOper.dOnReturnObject += this.ReturnOperSlot;
				this.m_lvsrOper.dOnProvideData += this.ProvideOperSlotData;
				NKCUtil.SetScrollHotKey(this.m_lvsrOper, null);
				this.m_lvsrOper.PrepareCells(0);
			}
			NKCUtil.SetGameobjectActive(this.m_ctglOper.gameObject, NKCOperatorUtil.IsActiveCastingBan());
			NKCUtil.SetToggleValueChangedDelegate(this.m_ctglUnit, delegate(bool b)
			{
				this.OnChangeTab(NKM_UNIT_TYPE.NUT_NORMAL);
			});
			NKCUtil.SetToggleValueChangedDelegate(this.m_ctglShip, delegate(bool b)
			{
				this.OnChangeTab(NKM_UNIT_TYPE.NUT_SHIP);
			});
			NKCUtil.SetToggleValueChangedDelegate(this.m_ctglOper, delegate(bool b)
			{
				this.OnChangeTab(NKM_UNIT_TYPE.NUT_OPERATOR);
			});
		}

		// Token: 0x060084FC RID: 34044 RVA: 0x002CEF0D File Offset: 0x002CD10D
		private RectTransform GetUnitSlot(int index)
		{
			NKCUIUnitSelectListSlot nkcuiunitSelectListSlot = UnityEngine.Object.Instantiate<NKCUIUnitSelectListSlot>(this.m_pfbUnitSlotForBan);
			nkcuiunitSelectListSlot.Init(false);
			NKCUtil.SetGameobjectActive(nkcuiunitSelectListSlot, true);
			nkcuiunitSelectListSlot.transform.localScale = Vector3.one;
			return nkcuiunitSelectListSlot.GetComponent<RectTransform>();
		}

		// Token: 0x060084FD RID: 34045 RVA: 0x002CEF3D File Offset: 0x002CD13D
		private void ReturnUnitSlot(Transform go)
		{
			go.SetParent(base.transform);
			UnityEngine.Object.Destroy(go.gameObject);
		}

		// Token: 0x060084FE RID: 34046 RVA: 0x002CEF58 File Offset: 0x002CD158
		private void ProvideUnitSlotData(Transform tr, int idx)
		{
			NKCUIUnitSelectListSlotBase component = tr.GetComponent<NKCUIUnitSelectListSlotBase>();
			if (component == null)
			{
				return;
			}
			if (idx < 0 || idx >= this.m_lstCastingVotedUnits.Count)
			{
				NKCUtil.SetGameobjectActive(component, false);
				return;
			}
			NKCUtil.SetGameobjectActive(component, true);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_lstCastingVotedUnits[idx]);
			component.SetEnableShowBan(false);
			component.SetDataForBan(unitTempletBase, true, null, false, true);
			component.SetSlotState(NKCUnitSortSystem.eUnitState.NONE);
		}

		// Token: 0x060084FF RID: 34047 RVA: 0x002CEFC2 File Offset: 0x002CD1C2
		private RectTransform GetShipSlot(int index)
		{
			NKCUIShipSelectListSlot nkcuishipSelectListSlot = UnityEngine.Object.Instantiate<NKCUIShipSelectListSlot>(this.m_pfbShipSlotForBan);
			nkcuishipSelectListSlot.Init(false);
			NKCUtil.SetGameobjectActive(nkcuishipSelectListSlot, true);
			nkcuishipSelectListSlot.transform.localScale = Vector3.one;
			return nkcuishipSelectListSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06008500 RID: 34048 RVA: 0x002CEFF2 File Offset: 0x002CD1F2
		private void ReturnShipSlot(Transform go)
		{
			go.SetParent(base.transform);
			UnityEngine.Object.Destroy(go.gameObject);
		}

		// Token: 0x06008501 RID: 34049 RVA: 0x002CF00C File Offset: 0x002CD20C
		private void ProvideShipSlotData(Transform tr, int idx)
		{
			NKCUIUnitSelectListSlotBase component = tr.GetComponent<NKCUIUnitSelectListSlotBase>();
			if (component == null)
			{
				return;
			}
			if (idx < 0 || idx >= this.m_lstCastingVotedUnits.Count)
			{
				NKCUtil.SetGameobjectActive(component, false);
				return;
			}
			NKCUtil.SetGameobjectActive(component, true);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_lstCastingVotedUnits[idx]);
			component.SetEnableShowBan(false);
			component.SetDataForBan(unitTempletBase, true, null, false, false);
		}

		// Token: 0x06008502 RID: 34050 RVA: 0x002CF06F File Offset: 0x002CD26F
		private RectTransform GetOperSlot(int index)
		{
			NKCUIOperatorSelectListSlot nkcuioperatorSelectListSlot = UnityEngine.Object.Instantiate<NKCUIOperatorSelectListSlot>(this.m_pfbOperatorSlotForBan);
			nkcuioperatorSelectListSlot.Init(false);
			NKCUtil.SetGameobjectActive(nkcuioperatorSelectListSlot, true);
			nkcuioperatorSelectListSlot.transform.localScale = Vector3.one;
			return nkcuioperatorSelectListSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06008503 RID: 34051 RVA: 0x002CF09F File Offset: 0x002CD29F
		private void ReturnOperSlot(Transform go)
		{
			go.SetParent(base.transform);
			UnityEngine.Object.Destroy(go.gameObject);
		}

		// Token: 0x06008504 RID: 34052 RVA: 0x002CF0B8 File Offset: 0x002CD2B8
		private void ProvideOperSlotData(Transform tr, int idx)
		{
			NKCUIOperatorSelectListSlot component = tr.GetComponent<NKCUIOperatorSelectListSlot>();
			if (component == null)
			{
				return;
			}
			if (idx < 0 || idx >= this.m_lstCastingVotedUnits.Count)
			{
				NKCUtil.SetGameobjectActive(component, false);
				return;
			}
			NKCUtil.SetGameobjectActive(component, true);
			component.SetEnableShowBan(false);
			component.SetDataForBan(NKCOperatorUtil.GetDummyOperator(this.m_lstCastingVotedUnits[idx], false), true, null);
			component.SetSlotState(NKCUnitSortSystem.eUnitState.NONE);
		}

		// Token: 0x06008505 RID: 34053 RVA: 0x002CF11F File Offset: 0x002CD31F
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			NKCUIUnitSelectList unitSelectList = this.UnitSelectList;
			if (unitSelectList != null)
			{
				unitSelectList.Close();
			}
			this.m_UIUnitSelectList = null;
		}

		// Token: 0x06008506 RID: 34054 RVA: 0x002CF145 File Offset: 0x002CD345
		public void Open()
		{
			this.m_ctglUnit.Select(true, true, false);
			this.m_SelectTabType = NKM_UNIT_TYPE.NUT_NORMAL;
			this.m_bCheckCastingBanRemainTime = true;
			this.UpdateUI();
			base.UIOpened(true);
		}

		// Token: 0x06008507 RID: 34055 RVA: 0x002CF171 File Offset: 0x002CD371
		public void OnChangeTab(NKM_UNIT_TYPE newTab)
		{
			if (this.m_SelectTabType == newTab)
			{
				return;
			}
			this.m_SelectTabType = newTab;
			this.UpdateUI();
		}

		// Token: 0x06008508 RID: 34056 RVA: 0x002CF18C File Offset: 0x002CD38C
		public void UpdateUI()
		{
			bool flag = false;
			if (this.m_SelectTabType == NKM_UNIT_TYPE.NUT_NORMAL)
			{
				this.m_lstCastingVotedUnits.Clear();
				this.m_lstCastingVotedUnits = NKCBanManager.m_CastingVoteData.unitIdList.ToList<int>().FindAll((int e) => e > 0);
				flag = (this.m_lstCastingVotedUnits.Count > 0);
				if (flag)
				{
					this.m_lstCastingVotedUnits.Sort();
					this.m_lvsrUnit.TotalCount = this.m_lstCastingVotedUnits.Count;
					this.m_lvsrUnit.RefreshCells(true);
				}
				this.m_lvsrUnit.SetIndexPosition(0);
			}
			else if (this.m_SelectTabType == NKM_UNIT_TYPE.NUT_SHIP)
			{
				this.m_lstCastingVotedUnits.Clear();
				List<int> unitList = NKCCollectionManager.GetUnitList(NKM_UNIT_TYPE.NUT_SHIP);
				using (List<int>.Enumerator enumerator = NKCBanManager.m_CastingVoteData.shipIdList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int ShipGroupID = enumerator.Current;
						List<NKMUnitTempletBase> list = (from e in NKMUnitTempletBase.Values
						where e.m_ShipGroupID == ShipGroupID
						select e).ToList<NKMUnitTempletBase>();
						if (list.Count > 0)
						{
							using (List<int>.Enumerator enumerator2 = unitList.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									int collectionShips = enumerator2.Current;
									NKMUnitTempletBase nkmunitTempletBase = list.Find((NKMUnitTempletBase id) => id.m_UnitID == collectionShips);
									if (nkmunitTempletBase != null)
									{
										this.m_lstCastingVotedUnits.Add(collectionShips);
										break;
									}
								}
							}
						}
					}
				}
				flag = (this.m_lstCastingVotedUnits.Count > 0);
				if (flag)
				{
					this.m_lstCastingVotedUnits.Sort();
					this.m_lvsrShip.TotalCount = this.m_lstCastingVotedUnits.Count;
					this.m_lvsrShip.RefreshCells(true);
				}
				this.m_lvsrShip.SetIndexPosition(0);
			}
			else if (this.m_SelectTabType == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				this.m_lstCastingVotedUnits.Clear();
				this.m_lstCastingVotedUnits = NKCBanManager.m_CastingVoteData.operatorIdList.ToList<int>().FindAll((int e) => e > 0);
				flag = (this.m_lstCastingVotedUnits.Count > 0);
				if (flag)
				{
					this.m_lstCastingVotedUnits.Sort();
					this.m_lvsrOper.TotalCount = this.m_lstCastingVotedUnits.Count;
					this.m_lvsrOper.RefreshCells(true);
				}
				this.m_lvsrOper.SetIndexPosition(0);
			}
			string msg = "";
			switch (this.m_SelectTabType)
			{
			case NKM_UNIT_TYPE.NUT_NORMAL:
				msg = NKCUtilString.GET_STRING_GAUNTLET_CASTING_BAN_SELECT_UNIT;
				break;
			case NKM_UNIT_TYPE.NUT_SHIP:
				msg = NKCUtilString.GET_STRING_GAUNTLET_CASTING_BAN_SELECT_SHIP;
				break;
			case NKM_UNIT_TYPE.NUT_OPERATOR:
				msg = NKCUtilString.GET_STRING_GAUNTLET_CASTING_BAN_SELECT_OPER;
				break;
			}
			NKCUtil.SetLabelText(this.m_lbVote, msg);
			NKCUtil.SetLabelText(this.m_lbVoteLock, msg);
			NKCUtil.SetGameobjectActive(this.m_objUnitList, this.m_SelectTabType == NKM_UNIT_TYPE.NUT_NORMAL && flag);
			NKCUtil.SetGameobjectActive(this.m_objShipList, this.m_SelectTabType == NKM_UNIT_TYPE.NUT_SHIP && flag);
			NKCUtil.SetGameobjectActive(this.m_objOperList, this.m_SelectTabType == NKM_UNIT_TYPE.NUT_OPERATOR && flag);
			NKCUtil.SetGameobjectActive(this.m_objNone, !flag);
		}

		// Token: 0x06008509 RID: 34057 RVA: 0x002CF4D8 File Offset: 0x002CD6D8
		private void Update()
		{
			if (this.m_bCheckCastingBanRemainTime)
			{
				this.UpdateRemainTimeUI();
			}
		}

		// Token: 0x0600850A RID: 34058 RVA: 0x002CF4E8 File Offset: 0x002CD6E8
		private void UpdateRemainTimeUI()
		{
			if (NKCPVPManager.GetPvpRankSeasonTemplet(NKCUtil.FindPVPSeasonIDForRank(NKCSynchronizedTime.GetServerUTCTime(0.0))) != null)
			{
				if (!NKCUIGauntletLobbyRightSideRank.CheckCanPlayPVPRankGame())
				{
					this.m_bCheckCastingBanRemainTime = false;
					this.m_csbtnVote.SetLock(!this.m_bCheckCastingBanRemainTime, false);
					return;
				}
				NKCUtil.SetLabelText(this.m_lbRemainTime, string.Format(NKCUtilString.GET_STRING_GAUNTLET_THIS_WEEK_LEAGUE_CASTING_BEN_ONE_PARAM, NKCUtilString.GetRemainTimeStringForGauntletWeekly()));
			}
		}

		// Token: 0x0600850B RID: 34059 RVA: 0x002CF550 File Offset: 0x002CD750
		private void OnClickSelectList()
		{
			NKCUIUnitSelectList.UnitSelectListOptions unitSelectListOptions = new NKCUIUnitSelectList.UnitSelectListOptions(this.m_SelectTabType, true, NKM_DECK_TYPE.NDT_NORMAL, NKCUIUnitSelectList.eUnitSelectListMode.CUSTOM_LIST, true);
			unitSelectListOptions.setDuplicateUnitID = null;
			unitSelectListOptions.setExcludeUnitUID = null;
			unitSelectListOptions.bExcludeLockedUnit = false;
			unitSelectListOptions.bExcludeDeckedUnit = false;
			unitSelectListOptions.strUpsideMenuName = NKCUtilString.GET_STRING_GAUNTLET_CASTING_BAN_SELECT_LIST_TITLE;
			unitSelectListOptions.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
			unitSelectListOptions.lstSortOption = NKCUnitSortSystem.GetDefaultSortOptions(this.m_SelectTabType, false, false);
			unitSelectListOptions.bDescending = false;
			unitSelectListOptions.bShowRemoveSlot = false;
			unitSelectListOptions.iMaxMultipleSelect = 3;
			unitSelectListOptions.m_SortOptions.bUseDeckedState = true;
			unitSelectListOptions.m_SortOptions.bUseLockedState = true;
			unitSelectListOptions.m_SortOptions.bUseDormInState = true;
			unitSelectListOptions.m_SortOptions.bIncludeSeizure = false;
			unitSelectListOptions.m_SortOptions.bIgnoreWorldMapLeader = false;
			unitSelectListOptions.bShowHideDeckedUnitMenu = false;
			unitSelectListOptions.bHideDeckedUnit = false;
			unitSelectListOptions.dOnAutoSelectFilter = null;
			unitSelectListOptions.bUseRemoveSmartAutoSelect = false;
			unitSelectListOptions.setSelectedUnitUID = new HashSet<long>();
			unitSelectListOptions.bCanSelectUnitInMission = false;
			unitSelectListOptions.dOnClose = null;
			unitSelectListOptions.bPushBackUnselectable = false;
			unitSelectListOptions.setUnitFilterCategory = null;
			unitSelectListOptions.setUnitSortCategory = null;
			if (this.m_SelectTabType == NKM_UNIT_TYPE.NUT_NORMAL)
			{
				unitSelectListOptions.setUnitFilterCategory = new HashSet<NKCUnitSortSystem.eFilterCategory>
				{
					NKCUnitSortSystem.eFilterCategory.UnitType,
					NKCUnitSortSystem.eFilterCategory.UnitRole,
					NKCUnitSortSystem.eFilterCategory.UnitMoveType,
					NKCUnitSortSystem.eFilterCategory.UnitTargetType,
					NKCUnitSortSystem.eFilterCategory.Rarity,
					NKCUnitSortSystem.eFilterCategory.Cost
				};
				unitSelectListOptions.lstSortOption = NKCUnitSortSystem.AddDefaultSortOptions(new List<NKCUnitSortSystem.eSortOption>
				{
					NKCUnitSortSystem.eSortOption.Rarity_High,
					NKCUnitSortSystem.eSortOption.Unit_SummonCost_High
				}, this.m_SelectTabType, false);
				unitSelectListOptions.setUnitSortCategory = new HashSet<NKCUnitSortSystem.eSortCategory>
				{
					NKCUnitSortSystem.eSortCategory.Rarity,
					NKCUnitSortSystem.eSortCategory.UnitSummonCost
				};
			}
			else if (this.m_SelectTabType == NKM_UNIT_TYPE.NUT_SHIP)
			{
				unitSelectListOptions.setUnitFilterCategory = new HashSet<NKCUnitSortSystem.eFilterCategory>
				{
					NKCUnitSortSystem.eFilterCategory.ShipType,
					NKCUnitSortSystem.eFilterCategory.Rarity
				};
				List<NKCUnitSortSystem.eSortOption> list = new List<NKCUnitSortSystem.eSortOption>();
				list.Add(NKCUnitSortSystem.eSortOption.Rarity_High);
				unitSelectListOptions.lstSortOption = new List<NKCUnitSortSystem.eSortOption>();
				unitSelectListOptions.lstSortOption = NKCUnitSortSystem.AddDefaultSortOptions(list, this.m_SelectTabType, false);
				unitSelectListOptions.setUnitSortCategory = new HashSet<NKCUnitSortSystem.eSortCategory>
				{
					NKCUnitSortSystem.eSortCategory.Rarity
				};
				unitSelectListOptions.setShipFilterCategory = new HashSet<NKCUnitSortSystem.eFilterCategory>
				{
					NKCUnitSortSystem.eFilterCategory.ShipType,
					NKCUnitSortSystem.eFilterCategory.Rarity
				};
			}
			else if (this.m_SelectTabType == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				unitSelectListOptions.setOperatorFilterCategory = new HashSet<NKCOperatorSortSystem.eFilterCategory>
				{
					NKCOperatorSortSystem.eFilterCategory.Rarity
				};
				unitSelectListOptions.lstOperatorSortOption = new List<NKCOperatorSortSystem.eSortOption>
				{
					NKCOperatorSortSystem.eSortOption.Rarity_High
				};
				unitSelectListOptions.setOperatorSortCategory = new HashSet<NKCOperatorSortSystem.eSortCategory>
				{
					NKCOperatorSortSystem.eSortCategory.Rarity
				};
			}
			if (this.m_lstCastingVotedUnits.Count > 0)
			{
				foreach (int num in this.m_lstCastingVotedUnits)
				{
					unitSelectListOptions.setSelectedUnitUID.Add((long)num);
				}
			}
			unitSelectListOptions.bShowBanMsg = true;
			unitSelectListOptions.bOpenedAtRearmExtract = true;
			unitSelectListOptions.m_bHideUnitCount = true;
			List<int> unitList = NKCCollectionManager.GetUnitList(this.m_SelectTabType);
			unitSelectListOptions.setOnlyIncludeUnitID = new HashSet<int>();
			foreach (int num2 in unitList)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(num2);
				if (unitTempletBase != null && unitTempletBase.PickupEnableByTag && (this.m_SelectTabType != NKM_UNIT_TYPE.NUT_NORMAL || !NKCBanManager.IsBanUnit(num2, NKCBanManager.BAN_DATA_TYPE.FINAL) || NKCBanManager.GetUnitBanLevel(num2, NKCBanManager.BAN_DATA_TYPE.FINAL) < 2) && (this.m_SelectTabType != NKM_UNIT_TYPE.NUT_SHIP || !NKCBanManager.IsBanShip(unitTempletBase.m_ShipGroupID, NKCBanManager.BAN_DATA_TYPE.FINAL) || NKCBanManager.GetShipBanLevel(unitTempletBase.m_ShipGroupID, NKCBanManager.BAN_DATA_TYPE.FINAL) < 2) && (this.m_SelectTabType != NKM_UNIT_TYPE.NUT_OPERATOR || !NKCBanManager.IsBanOperator(num2, NKCBanManager.BAN_DATA_TYPE.FINAL) || NKCBanManager.GetOperBanLevel(num2, NKCBanManager.BAN_DATA_TYPE.FINAL) < 2))
				{
					unitSelectListOptions.setOnlyIncludeUnitID.Add(num2);
				}
			}
			this.UnitSelectList.Open(unitSelectListOptions, new NKCUIUnitSelectList.OnUnitSelectCommand(this.OnSelectedUnits), null, null, null, null);
		}

		// Token: 0x1700159D RID: 5533
		// (get) Token: 0x0600850C RID: 34060 RVA: 0x002CF930 File Offset: 0x002CDB30
		private NKCUIUnitSelectList UnitSelectList
		{
			get
			{
				if (this.m_UIUnitSelectList == null)
				{
					this.m_UIUnitSelectList = NKCUIUnitSelectList.OpenNewInstance(false);
				}
				return this.m_UIUnitSelectList;
			}
		}

		// Token: 0x0600850D RID: 34061 RVA: 0x002CF954 File Offset: 0x002CDB54
		private void OnSelectedUnits(List<long> lstUnits)
		{
			if (this.UnitSelectList.IsOpen)
			{
				this.UnitSelectList.Close();
			}
			if (this.m_SelectTabType == NKM_UNIT_TYPE.NUT_NORMAL)
			{
				NKCPacketSender.Send_NKMPacket_PVP_CASTING_VOTE_UNIT_REQ(lstUnits.ConvertAll<int>((long i) => (int)i));
				return;
			}
			if (this.m_SelectTabType == NKM_UNIT_TYPE.NUT_SHIP)
			{
				List<int> list = new List<int>();
				foreach (long num in lstUnits)
				{
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase((int)num);
					if (unitTempletBase != null)
					{
						list.Add(unitTempletBase.m_ShipGroupID);
					}
				}
				NKCPacketSender.Send_NKMPacket_PVP_CASTING_VOTE_SHIP_REQ(list);
				return;
			}
			if (this.m_SelectTabType == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				NKCPacketSender.Send_NKMPacket_PVP_CASTING_VOTE_OPERATOR_REQ(lstUnits.ConvertAll<int>((long i) => (int)i));
			}
		}

		// Token: 0x04007167 RID: 29031
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_GAUNTLET";

		// Token: 0x04007168 RID: 29032
		private const string UI_ASSET_NAME = "NKM_UI_GAUNTLET_POPUP_BANNED_LIST_CASTING";

		// Token: 0x04007169 RID: 29033
		private static NKCPopupGauntletCastingBan m_Instance;

		// Token: 0x0400716A RID: 29034
		private const int MAX_CASTING_BAN_CNT = 3;

		// Token: 0x0400716B RID: 29035
		[Header("공통")]
		public EventTrigger m_etBG;

		// Token: 0x0400716C RID: 29036
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x0400716D RID: 29037
		public NKCUIComStateButton m_csbtnVote;

		// Token: 0x0400716E RID: 29038
		public Text m_lbRemainTime;

		// Token: 0x0400716F RID: 29039
		public Text m_lbVote;

		// Token: 0x04007170 RID: 29040
		public Text m_lbVoteLock;

		// Token: 0x04007171 RID: 29041
		[Header("상단")]
		public Text m_lbSubTitle;

		// Token: 0x04007172 RID: 29042
		[Header("왼쪽")]
		public NKCUIComToggle m_ctglUnit;

		// Token: 0x04007173 RID: 29043
		public NKCUIComToggle m_ctglShip;

		// Token: 0x04007174 RID: 29044
		public NKCUIComToggle m_ctglOper;

		// Token: 0x04007175 RID: 29045
		[Header("오른쪽")]
		public LoopVerticalScrollRect m_lvsrUnit;

		// Token: 0x04007176 RID: 29046
		public LoopVerticalScrollRect m_lvsrShip;

		// Token: 0x04007177 RID: 29047
		public LoopVerticalScrollRect m_lvsrOper;

		// Token: 0x04007178 RID: 29048
		public GameObject m_objUnitList;

		// Token: 0x04007179 RID: 29049
		public GameObject m_objShipList;

		// Token: 0x0400717A RID: 29050
		public GameObject m_objOperList;

		// Token: 0x0400717B RID: 29051
		public GameObject m_objNone;

		// Token: 0x0400717C RID: 29052
		public NKCUIUnitSelectListSlot m_pfbUnitSlotForBan;

		// Token: 0x0400717D RID: 29053
		public NKCUIShipSelectListSlot m_pfbShipSlotForBan;

		// Token: 0x0400717E RID: 29054
		public NKCUIOperatorSelectListSlot m_pfbOperatorSlotForBan;

		// Token: 0x0400717F RID: 29055
		private NKM_UNIT_TYPE m_SelectTabType = NKM_UNIT_TYPE.NUT_NORMAL;

		// Token: 0x04007180 RID: 29056
		private List<int> m_lstCastingVotedUnits = new List<int>();

		// Token: 0x04007181 RID: 29057
		private bool m_bCheckCastingBanRemainTime = true;

		// Token: 0x04007182 RID: 29058
		private NKCUIUnitSelectList m_UIUnitSelectList;
	}
}
