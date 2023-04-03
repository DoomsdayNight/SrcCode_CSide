using System;
using System.Collections.Generic;
using NKC.UI.Component;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000AA0 RID: 2720
	public class NKCUIScout : NKCUIBase
	{
		// Token: 0x1700143C RID: 5180
		// (get) Token: 0x06007885 RID: 30853 RVA: 0x0028029C File Offset: 0x0027E49C
		public static NKCUIScout Instance
		{
			get
			{
				if (NKCUIScout.m_Instance == null)
				{
					NKCUIScout.m_Instance = NKCUIManager.OpenNewInstance<NKCUIScout>("ab_ui_nkm_ui_personnel", "NKM_UI_PERSONNEL_SCOUT", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIScout.CleanupInstance)).GetInstance<NKCUIScout>();
					NKCUIScout.m_Instance.Init();
				}
				return NKCUIScout.m_Instance;
			}
		}

		// Token: 0x1700143D RID: 5181
		// (get) Token: 0x06007886 RID: 30854 RVA: 0x002802EB File Offset: 0x0027E4EB
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIScout.m_Instance != null && NKCUIScout.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007887 RID: 30855 RVA: 0x00280306 File Offset: 0x0027E506
		public static void CheckInstanceAndClose()
		{
			if (NKCUIScout.m_Instance != null && NKCUIScout.m_Instance.IsOpen)
			{
				NKCUIScout.m_Instance.Close();
			}
		}

		// Token: 0x06007888 RID: 30856 RVA: 0x0028032B File Offset: 0x0027E52B
		private static void CleanupInstance()
		{
			NKCUIScout.m_Instance = null;
		}

		// Token: 0x1700143E RID: 5182
		// (get) Token: 0x06007889 RID: 30857 RVA: 0x00280333 File Offset: 0x0027E533
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_SYSTEM_SCOUT";
			}
		}

		// Token: 0x1700143F RID: 5183
		// (get) Token: 0x0600788A RID: 30858 RVA: 0x0028033A File Offset: 0x0027E53A
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001440 RID: 5184
		// (get) Token: 0x0600788B RID: 30859 RVA: 0x0028033D File Offset: 0x0027E53D
		public override string MenuName
		{
			get
			{
				return NKCStringTable.GetString("SI_PF_PERSONNEL_SCOUT_TEXT", false);
			}
		}

		// Token: 0x0600788C RID: 30860 RVA: 0x0028034A File Offset: 0x0027E54A
		public override void CloseInternal()
		{
			this.m_CharView.CleanUp();
			base.gameObject.SetActive(false);
		}

		// Token: 0x0600788D RID: 30861 RVA: 0x00280364 File Offset: 0x0027E564
		private void Init()
		{
			this.m_CharView.Init(null, null);
			this.m_unitInfoSummary.Init(false);
			if (this.m_SortOptions != null)
			{
				this.m_SortOptions.Init(new NKCUIComUnitSortOptions.OnSorted(this.OnSortChanged), false);
				this.m_SortOptions.RegisterCategories(this.setFilterCategory, this.setSortCategory, false);
				if (this.m_SortOptions.m_NKCPopupSort != null)
				{
					this.m_SortOptions.m_NKCPopupSort.m_bUseDefaultSortAdd = false;
				}
			}
			if (this.m_LoopScrollRect != null)
			{
				this.m_LoopScrollRect.dOnGetObject += this.GetObject;
				this.m_LoopScrollRect.dOnReturnObject += this.ReturnObject;
				this.m_LoopScrollRect.dOnProvideData += this.ProvideData;
				NKCUtil.SetScrollHotKey(this.m_LoopScrollRect, null);
			}
			if (this.m_csbtnScout != null)
			{
				this.m_csbtnScout.PointerClick.RemoveAllListeners();
				this.m_csbtnScout.PointerClick.AddListener(new UnityAction(this.OnBtnScout));
			}
			NKCUtil.SetGameobjectActive(this.m_csbtnCollection, false);
			if (this.m_csbtnCollection != null)
			{
				this.m_csbtnCollection.PointerClick.RemoveAllListeners();
				this.m_csbtnCollection.PointerClick.AddListener(new UnityAction(this.OnBtnCollection));
			}
		}

		// Token: 0x0600788E RID: 30862 RVA: 0x002804CC File Offset: 0x0027E6CC
		public void Open()
		{
			this.m_UnitSortSystem = this.MakeScoutSortSystem();
			this.m_SortOptions.RegisterUnitSort(this.m_UnitSortSystem);
			base.gameObject.SetActive(true);
			this.SetCommonUI();
			if (!this.m_bCellPrepared)
			{
				this.m_LoopScrollRect.PrepareCells(0);
				this.m_bCellPrepared = true;
			}
			NKMPieceTemplet nkmpieceTemplet = this.FindReddotTargetUnit();
			if (nkmpieceTemplet != null)
			{
				this.m_SelectedPieceTemplet = nkmpieceTemplet;
			}
			this.SetUnitData(this.m_SelectedPieceTemplet);
			this.m_LoopScrollRect.TotalCount = this.m_UnitSortSystem.SortedUnitList.Count;
			this.m_LoopScrollRect.SetIndexPosition(0);
			base.UIOpened(true);
			this.CheckTutorial();
		}

		// Token: 0x0600788F RID: 30863 RVA: 0x00280574 File Offset: 0x0027E774
		public void Refresh()
		{
			this.SetUnitData(this.m_SelectedPieceTemplet);
			this.RefreshScoutList();
		}

		// Token: 0x06007890 RID: 30864 RVA: 0x00280588 File Offset: 0x0027E788
		public override void UnHide()
		{
			base.UnHide();
			this.Refresh();
		}

		// Token: 0x06007891 RID: 30865 RVA: 0x00280596 File Offset: 0x0027E796
		private void SetCommonUI()
		{
			this.m_NKCUIPersonnelShortCutMenu.SetData(NKC_SCEN_BASE.eUIOpenReserve.Personnel_Scout);
		}

		// Token: 0x06007892 RID: 30866 RVA: 0x002805A8 File Offset: 0x0027E7A8
		private void SetUnitData(NKMPieceTemplet templet)
		{
			this.m_SelectedPieceTemplet = templet;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKMUnitData nkmunitData;
			if (templet == null || !this.m_dicFakeUnit.TryGetValue(templet.Key, out nkmunitData))
			{
				nkmunitData = null;
				NKCUtil.SetGameobjectActive(this.m_unitInfoSummary, false);
				NKCUtil.SetGameobjectActive(this.m_objUnitNotSelected, true);
				NKCUtil.SetGameobjectActive(this.m_objUnitNotInCollection, false);
				NKCUtil.SetGameobjectActive(this.m_UnitPiece, false);
				this.m_csbtnScout.Lock(false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objUnitNotSelected, false);
			NKCUtil.SetGameobjectActive(this.m_unitInfoSummary, true);
			NKCUtil.SetGameobjectActive(this.m_UnitPiece, true);
			NKCUICharacterView charView = this.m_CharView;
			if (charView != null)
			{
				charView.SetCharacterIllust(nkmunitData, false, true, true, 0);
			}
			NKCUICharInfoSummary unitInfoSummary = this.m_unitInfoSummary;
			if (unitInfoSummary != null)
			{
				unitInfoSummary.SetData(nkmunitData);
			}
			NKCUIScoutUnitPiece unitPiece = this.m_UnitPiece;
			if (unitPiece != null)
			{
				unitPiece.SetData(templet);
			}
			bool flag = nkmuserData.m_ArmyData.IsCollectedUnit(templet.m_PieceGetUintId);
			long num = (long)(flag ? templet.m_PieceReq : templet.m_PieceReqFirst);
			long countMiscItem = nkmuserData.m_InventoryData.GetCountMiscItem(templet.m_PieceId);
			NKCUtil.SetGameobjectActive(this.m_objUnitNotInCollection, !flag);
			bool flag2 = countMiscItem >= num;
			if (flag2)
			{
				this.m_csbtnScout.UnLock(false);
			}
			else
			{
				this.m_csbtnScout.Lock(false);
			}
			if (flag2)
			{
				NKCUIScout.RegisterAlarmOff(templet.Key);
			}
		}

		// Token: 0x06007893 RID: 30867 RVA: 0x002806EC File Offset: 0x0027E8EC
		private NKCUnitSortSystem MakeScoutSortSystem()
		{
			this.m_dicFakeUnit.Clear();
			List<NKMUnitData> list = new List<NKMUnitData>();
			foreach (NKMPieceTemplet nkmpieceTemplet in NKMTempletContainer<NKMPieceTemplet>.Values)
			{
				if (nkmpieceTemplet.EnableByTag)
				{
					NKMUnitData nkmunitData = NKCUnitSortSystem.MakeTempUnitData(nkmpieceTemplet.m_PieceGetUintId, 1, 0);
					nkmunitData.m_UnitUID = (long)nkmpieceTemplet.Key;
					list.Add(nkmunitData);
					this.m_dicFakeUnit.Add(nkmpieceTemplet.Key, nkmunitData);
				}
			}
			NKCUnitSortSystem.UnitListOptions options = new NKCUnitSortSystem.UnitListOptions
			{
				eDeckType = NKM_DECK_TYPE.NDT_NONE,
				lstSortOption = new List<NKCUnitSortSystem.eSortOption>
				{
					NKCUnitSortSystem.eSortOption.Rarity_High
				},
				bDescending = true,
				AdditionalExcludeFilterFunc = new NKCUnitSortSystem.UnitListOptions.CustomFilterFunc(this.ScoutListFilterFunc),
				lstDefaultSortOption = new List<NKCUnitSortSystem.eSortOption>
				{
					NKCUnitSortSystem.eSortOption.Rarity_High,
					NKCUnitSortSystem.eSortOption.UID_First
				},
				bIncludeUndeckableUnit = true
			};
			NKCGenericUnitSort nkcgenericUnitSort = new NKCGenericUnitSort(NKCScenManager.CurrentUserData(), options, list);
			nkcgenericUnitSort.UpdateScoutProgressCache();
			return nkcgenericUnitSort;
		}

		// Token: 0x06007894 RID: 30868 RVA: 0x002807F8 File Offset: 0x0027E9F8
		private bool ScoutListFilterFunc(NKMUnitData unitData)
		{
			NKMPieceTemplet nkmpieceTemplet = NKMTempletContainer<NKMPieceTemplet>.Find((int)unitData.m_UnitUID);
			if (nkmpieceTemplet == null)
			{
				return false;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			return nkmuserData.m_ArmyData.IsCollectedUnit(unitData.m_UnitID) || nkmuserData.m_InventoryData.GetCountMiscItem(nkmpieceTemplet.m_PieceId) != 0L;
		}

		// Token: 0x06007895 RID: 30869 RVA: 0x00280848 File Offset: 0x0027EA48
		private NKMPieceTemplet FindReddotTargetUnit()
		{
			List<NKMUnitData> list = new List<NKMUnitData>();
			foreach (NKMPieceTemplet nkmpieceTemplet in NKMTempletContainer<NKMPieceTemplet>.Values)
			{
				if (nkmpieceTemplet.EnableByTag && NKCUIScout.IsReddotNeeded(NKCScenManager.CurrentUserData(), nkmpieceTemplet.Key))
				{
					NKMUnitData nkmunitData = NKCUnitSortSystem.MakeTempUnitData(nkmpieceTemplet.m_PieceGetUintId, 1, 0);
					nkmunitData.m_UnitUID = (long)nkmpieceTemplet.Key;
					list.Add(nkmunitData);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			NKCUnitSortSystem.UnitListOptions options = new NKCUnitSortSystem.UnitListOptions
			{
				eDeckType = NKM_DECK_TYPE.NDT_NONE,
				lstSortOption = new List<NKCUnitSortSystem.eSortOption>
				{
					NKCUnitSortSystem.eSortOption.Rarity_High,
					NKCUnitSortSystem.eSortOption.ID_First
				},
				bDescending = true,
				bIncludeUndeckableUnit = true
			};
			return NKMTempletContainer<NKMPieceTemplet>.Find((int)new NKCGenericUnitSort(NKCScenManager.CurrentUserData(), options, list).AutoSelect(null, null).m_UnitUID);
		}

		// Token: 0x06007896 RID: 30870 RVA: 0x00280938 File Offset: 0x0027EB38
		private RectTransform GetObject(int index)
		{
			if (this.m_stkObj.Count > 0)
			{
				RectTransform rectTransform = this.m_stkObj.Pop();
				NKCUtil.SetGameobjectActive(rectTransform, true);
				return rectTransform;
			}
			if (this.m_pfbScoutSlot == null)
			{
				Debug.LogError("Scout slot prefab null!");
				return null;
			}
			NKCUIScoutSelectListSlot nkcuiscoutSelectListSlot = UnityEngine.Object.Instantiate<NKCUIScoutSelectListSlot>(this.m_pfbScoutSlot);
			nkcuiscoutSelectListSlot.Init();
			return nkcuiscoutSelectListSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06007897 RID: 30871 RVA: 0x00280996 File Offset: 0x0027EB96
		private void ReturnObject(Transform go)
		{
			NKCUtil.SetGameobjectActive(go, false);
			go.SetParent(this.m_rtSlotPool);
			this.m_stkObj.Push(go.GetComponent<RectTransform>());
		}

		// Token: 0x06007898 RID: 30872 RVA: 0x002809BC File Offset: 0x0027EBBC
		private void ProvideData(Transform tr, int idx)
		{
			if (idx < 0)
			{
				NKCUtil.SetGameobjectActive(tr, false);
				return;
			}
			NKCUIScoutSelectListSlot component = tr.GetComponent<NKCUIScoutSelectListSlot>();
			if (component == null)
			{
				return;
			}
			if (idx >= this.m_UnitSortSystem.SortedUnitList.Count)
			{
				NKCUtil.SetGameobjectActive(tr, false);
				return;
			}
			NKMUnitData nkmunitData = this.m_UnitSortSystem.SortedUnitList[idx];
			if (nkmunitData == null)
			{
				NKCUtil.SetGameobjectActive(tr, false);
				return;
			}
			int num = (int)nkmunitData.m_UnitUID;
			NKCUtil.SetGameobjectActive(tr, true);
			bool bSelected = this.m_SelectedPieceTemplet != null && this.m_SelectedPieceTemplet.Key == num;
			if (nkmunitData == null)
			{
				Debug.LogError("Potential logic error : null unit in UIScout");
				component.SetData(null, null, bSelected, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnSelectSlot));
				return;
			}
			NKMPieceTemplet templet = NKMTempletContainer<NKMPieceTemplet>.Find(num);
			component.SetData(templet, nkmunitData, bSelected, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnSelectSlot));
		}

		// Token: 0x06007899 RID: 30873 RVA: 0x00280A88 File Offset: 0x0027EC88
		private void RefreshScoutList()
		{
			this.m_UnitSortSystem.UpdateScoutProgressCache();
			this.m_LoopScrollRect.TotalCount = this.m_UnitSortSystem.SortedUnitList.Count;
			this.m_LoopScrollRect.RefreshCells(false);
		}

		// Token: 0x0600789A RID: 30874 RVA: 0x00280ABC File Offset: 0x0027ECBC
		private void OnSortChanged(bool bResetScroll)
		{
			this.m_LoopScrollRect.TotalCount = this.m_UnitSortSystem.SortedUnitList.Count;
			if (bResetScroll)
			{
				this.m_LoopScrollRect.SetIndexPosition(0);
				return;
			}
			this.m_LoopScrollRect.RefreshCells(false);
		}

		// Token: 0x0600789B RID: 30875 RVA: 0x00280AF8 File Offset: 0x0027ECF8
		private void OnSelectSlot(NKMUnitData unitData, NKMUnitTempletBase unitTempletBase, NKMDeckIndex deckIndex, NKCUnitSortSystem.eUnitState slotState, NKCUIUnitSelectList.eUnitSlotSelectState unitSlotSelectState)
		{
			bool flag = false;
			if (unitData != null)
			{
				NKMPieceTemplet nkmpieceTemplet = NKMTempletContainer<NKMPieceTemplet>.Find((int)unitData.m_UnitUID);
				if (this.m_SelectedPieceTemplet != nkmpieceTemplet)
				{
					flag = true;
				}
				this.m_SelectedPieceTemplet = nkmpieceTemplet;
			}
			if (flag)
			{
				this.SetUnitData(this.m_SelectedPieceTemplet);
			}
			this.m_LoopScrollRect.RefreshCells(false);
		}

		// Token: 0x0600789C RID: 30876 RVA: 0x00280B44 File Offset: 0x0027ED44
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(itemData.ItemID);
			if (itemMiscTempletByID == null)
			{
				return;
			}
			if (itemMiscTempletByID.m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_PIECE)
			{
				this.m_UnitSortSystem.UpdateScoutProgressCache();
				this.m_LoopScrollRect.RefreshCells(false);
			}
		}

		// Token: 0x0600789D RID: 30877 RVA: 0x00280B84 File Offset: 0x0027ED84
		private void OnBtnScout()
		{
			if (this.m_SelectedPieceTemplet == null)
			{
				return;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = this.m_SelectedPieceTemplet.CanExchange(NKCScenManager.CurrentUserData());
			if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_OK)
			{
				NKCUIPopupScoutConfirm.Instance.Open(this.m_SelectedPieceTemplet, new NKCUIPopupScoutConfirm.OnConfirm(this.OnScoutConfirm));
				return;
			}
			NKCPopupOKCancel.OpenOKBox(nkm_ERROR_CODE, null, "");
		}

		// Token: 0x0600789E RID: 30878 RVA: 0x00280BD7 File Offset: 0x0027EDD7
		private void OnScoutConfirm()
		{
			NKCPacketSender.Send_NKMPacket_EXCHANGE_PIECE_TO_UNIT_REQ(this.m_SelectedPieceTemplet.m_PieceId, 1);
		}

		// Token: 0x0600789F RID: 30879 RVA: 0x00280BEA File Offset: 0x0027EDEA
		private void OnBtnCollection()
		{
		}

		// Token: 0x060078A0 RID: 30880 RVA: 0x00280BEC File Offset: 0x0027EDEC
		public static bool IsReddotNeeded(NKMUserData userData, int templetKey)
		{
			NKMPieceTemplet nkmpieceTemplet = NKMTempletContainer<NKMPieceTemplet>.Find(templetKey);
			long num = (long)(userData.m_ArmyData.IsCollectedUnit(nkmpieceTemplet.m_PieceGetUintId) ? nkmpieceTemplet.m_PieceReq : nkmpieceTemplet.m_PieceReqFirst);
			return userData.m_InventoryData.GetCountMiscItem(nkmpieceTemplet.m_PieceId) >= num && !NKCUIScout.IsAlarmOffRegistered(templetKey);
		}

		// Token: 0x060078A1 RID: 30881 RVA: 0x00280C49 File Offset: 0x0027EE49
		public static bool IsAlarmOffRegistered(int templetKey)
		{
			return PlayerPrefs.GetInt(string.Format("SCOUT_ALARM_OFF_{0}", templetKey), 0) == 1;
		}

		// Token: 0x060078A2 RID: 30882 RVA: 0x00280C64 File Offset: 0x0027EE64
		public static void RegisterAlarmOff(int templetKey)
		{
			PlayerPrefs.SetInt(string.Format("SCOUT_ALARM_OFF_{0}", templetKey), 1);
			PlayerPrefs.Save();
		}

		// Token: 0x060078A3 RID: 30883 RVA: 0x00280C81 File Offset: 0x0027EE81
		public static void UnregisgerAlarmOff(int templetKey)
		{
			PlayerPrefs.DeleteKey(string.Format("SCOUT_ALARM_OFF_{0}", templetKey));
			PlayerPrefs.Save();
		}

		// Token: 0x060078A4 RID: 30884 RVA: 0x00280C9D File Offset: 0x0027EE9D
		private void CheckTutorial()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.Scout, true);
		}

		// Token: 0x04006513 RID: 25875
		public const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_personnel";

		// Token: 0x04006514 RID: 25876
		public const string UI_ASSET_NAME = "NKM_UI_PERSONNEL_SCOUT";

		// Token: 0x04006515 RID: 25877
		private static NKCUIScout m_Instance;

		// Token: 0x04006516 RID: 25878
		[Header("사장실 공통")]
		public NKCUIPersonnelShortCutMenu m_NKCUIPersonnelShortCutMenu;

		// Token: 0x04006517 RID: 25879
		public NKCUICharInfoSummary m_unitInfoSummary;

		// Token: 0x04006518 RID: 25880
		public NKCUICharacterView m_CharView;

		// Token: 0x04006519 RID: 25881
		[Header("오른쪽 목록")]
		public LoopScrollRect m_LoopScrollRect;

		// Token: 0x0400651A RID: 25882
		public NKCUIScoutSelectListSlot m_pfbScoutSlot;

		// Token: 0x0400651B RID: 25883
		public NKCUIComUnitSortOptions m_SortOptions;

		// Token: 0x0400651C RID: 25884
		public RectTransform m_rtSlotPool;

		// Token: 0x0400651D RID: 25885
		[Header("스카우트 관련")]
		public NKCUIScoutUnitPiece m_UnitPiece;

		// Token: 0x0400651E RID: 25886
		public GameObject m_objUnitNotSelected;

		// Token: 0x0400651F RID: 25887
		public GameObject m_objUnitNotInCollection;

		// Token: 0x04006520 RID: 25888
		public NKCUIComStateButton m_csbtnScout;

		// Token: 0x04006521 RID: 25889
		[Header("도감 버튼")]
		public NKCUIComStateButton m_csbtnCollection;

		// Token: 0x04006522 RID: 25890
		private NKCUnitSortSystem m_UnitSortSystem;

		// Token: 0x04006523 RID: 25891
		private NKMPieceTemplet m_SelectedPieceTemplet;

		// Token: 0x04006524 RID: 25892
		private bool m_bCellPrepared;

		// Token: 0x04006525 RID: 25893
		private readonly HashSet<NKCUnitSortSystem.eSortCategory> setSortCategory = new HashSet<NKCUnitSortSystem.eSortCategory>
		{
			NKCUnitSortSystem.eSortCategory.IDX,
			NKCUnitSortSystem.eSortCategory.Rarity,
			NKCUnitSortSystem.eSortCategory.UnitSummonCost,
			NKCUnitSortSystem.eSortCategory.ScoutProgress
		};

		// Token: 0x04006526 RID: 25894
		private readonly HashSet<NKCUnitSortSystem.eFilterCategory> setFilterCategory = new HashSet<NKCUnitSortSystem.eFilterCategory>
		{
			NKCUnitSortSystem.eFilterCategory.UnitType,
			NKCUnitSortSystem.eFilterCategory.UnitRole,
			NKCUnitSortSystem.eFilterCategory.UnitTargetType,
			NKCUnitSortSystem.eFilterCategory.Rarity,
			NKCUnitSortSystem.eFilterCategory.Cost,
			NKCUnitSortSystem.eFilterCategory.Collected,
			NKCUnitSortSystem.eFilterCategory.Scout
		};

		// Token: 0x04006527 RID: 25895
		private Dictionary<int, NKMUnitData> m_dicFakeUnit = new Dictionary<int, NKMUnitData>();

		// Token: 0x04006528 RID: 25896
		private Stack<RectTransform> m_stkObj = new Stack<RectTransform>();

		// Token: 0x04006529 RID: 25897
		private const string SCOUT_ALARM_OFF_KEY = "SCOUT_ALARM_OFF_{0}";
	}
}
