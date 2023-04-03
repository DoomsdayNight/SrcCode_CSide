using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cs.Logging;
using NKM;
using NKM.Templet;
using NKM.Templet.Recall;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A75 RID: 2677
	public class NKCPopupRecall : NKCUIBase
	{
		// Token: 0x170013B9 RID: 5049
		// (get) Token: 0x06007654 RID: 30292 RVA: 0x00275A5C File Offset: 0x00273C5C
		public static NKCPopupRecall Instance
		{
			get
			{
				if (NKCPopupRecall.m_Instance == null)
				{
					NKCPopupRecall.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupRecall>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX", "NKM_UI_POPUP_RECALL", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupRecall.CleanupInstance)).GetInstance<NKCPopupRecall>();
					NKCPopupRecall.m_Instance.Init();
				}
				return NKCPopupRecall.m_Instance;
			}
		}

		// Token: 0x170013BA RID: 5050
		// (get) Token: 0x06007655 RID: 30293 RVA: 0x00275AAB File Offset: 0x00273CAB
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupRecall.m_Instance != null && NKCPopupRecall.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007656 RID: 30294 RVA: 0x00275AC6 File Offset: 0x00273CC6
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupRecall.m_Instance != null && NKCPopupRecall.m_Instance.IsOpen)
			{
				NKCPopupRecall.m_Instance.Close();
			}
		}

		// Token: 0x06007657 RID: 30295 RVA: 0x00275AEB File Offset: 0x00273CEB
		private static void CleanupInstance()
		{
			NKCPopupRecall.m_Instance = null;
		}

		// Token: 0x170013BB RID: 5051
		// (get) Token: 0x06007658 RID: 30296 RVA: 0x00275AF3 File Offset: 0x00273CF3
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170013BC RID: 5052
		// (get) Token: 0x06007659 RID: 30297 RVA: 0x00275AF6 File Offset: 0x00273CF6
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x0600765A RID: 30298 RVA: 0x00275AFD File Offset: 0x00273CFD
		public override void CloseInternal()
		{
			if (this.m_UIUnitSelectList != null && NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_UNIT_LIST)
			{
				this.UnitSelectList.Close();
			}
			this.m_UIUnitSelectList = null;
			base.gameObject.SetActive(false);
		}

		// Token: 0x0600765B RID: 30299 RVA: 0x00275B3C File Offset: 0x00273D3C
		private void Init()
		{
			this.m_slotSourceUnit.Init(false);
			this.m_slotTargetUnit.Init(false);
			this.m_slotSourceShip.Init(false);
			this.m_slotRefoundUnit.Init(false);
			NKCUtil.SetBindFunction(this.m_btnCancel, new UnityAction(base.Close));
			NKCUtil.SetBindFunction(this.m_btnOK, new UnityAction(this.OnClickOK));
			this.m_btnOK.m_bGetCallbackWhileLocked = true;
		}

		// Token: 0x0600765C RID: 30300 RVA: 0x00275BB3 File Offset: 0x00273DB3
		public override void Hide()
		{
			base.Hide();
		}

		// Token: 0x0600765D RID: 30301 RVA: 0x00275BBB File Offset: 0x00273DBB
		public override void UnHide()
		{
			base.UnHide();
			this.SetButtonState();
		}

		// Token: 0x0600765E RID: 30302 RVA: 0x00275BCC File Offset: 0x00273DCC
		public void Open(NKMUnitData sourceUnitData)
		{
			if (sourceUnitData == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			this.m_NKMUnitData = sourceUnitData;
			this.m_TargetUnitID = 0;
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(sourceUnitData);
			if (unitTempletBase == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				Log.Error(string.Format("UnitTempletBase is null - ID : {0}", sourceUnitData.m_UnitID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Popup/NKCPopupRecall.cs", 153);
				return;
			}
			NKCUtil.SetLabelText(this.m_lbTitle, NKCStringTable.GetString("SI_PF_RECALL_POPUP_TITLE", false));
			NKCUtil.SetLabelText(this.m_lbDesc, NKCStringTable.GetString("SI_PF_RECALL_POPUP_SUBTITLE", false));
			NKCUtil.SetGameobjectActive(this.m_objRefoundUnit, false);
			NKCUtil.SetGameobjectActive(this.m_objRecallUnit, true);
			this.m_CurUnitType = unitTempletBase.m_NKM_UNIT_TYPE;
			if (this.m_CurUnitType == NKM_UNIT_TYPE.NUT_NORMAL)
			{
				this.m_RecallTemplet = NKMRecallTemplet.Find(sourceUnitData.m_UnitID, NKMTime.UTCtoLocal(NKCSynchronizedTime.GetServerUTCTime(0.0), 0));
			}
			else if (this.m_CurUnitType == NKM_UNIT_TYPE.NUT_SHIP)
			{
				this.m_RecallTemplet = NKMRecallTemplet.Find(NKCRecallManager.GetFirstLevelShipID(sourceUnitData.m_UnitID), NKMTime.UTCtoLocal(NKCSynchronizedTime.GetServerUTCTime(0.0), 0));
			}
			if (this.m_RecallTemplet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				Log.Error(string.Format("RecallTemplet is null - ID : {0}", sourceUnitData.m_UnitID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Popup/NKCPopupRecall.cs", 173);
				return;
			}
			switch (this.m_CurUnitType)
			{
			case NKM_UNIT_TYPE.NUT_NORMAL:
				NKCUtil.SetLabelText(this.m_lbSourceType, NKCUtilString.GET_STRING_COLLECTION_UNIT);
				this.SetUnitRecallReward();
				goto IL_1B7;
			case NKM_UNIT_TYPE.NUT_SHIP:
				NKCUtil.SetLabelText(this.m_lbSourceType, NKCUtilString.GET_STRING_COLLECTION_SHIP);
				this.SetShipRecallReward();
				goto IL_1B7;
			}
			Log.Debug("유닛 / 함선 외에는 리콜 대상이 아님", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Popup/NKCPopupRecall.cs", 193);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			return;
			IL_1B7:
			NKCUtil.SetBindFunction(this.m_btnOK, new UnityAction(this.OnClickOK));
			NKCUtil.SetGameobjectActive(this.m_objRefoundLimitCnt, false);
			this.SetButtonState();
			base.UIOpened(true);
		}

		// Token: 0x0600765F RID: 30303 RVA: 0x00275DC0 File Offset: 0x00273FC0
		public void Open(NKMUnitData refoundUnitData, UnityAction callBack)
		{
			if (refoundUnitData == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			this.m_NKMUnitData = refoundUnitData;
			this.m_TargetUnitID = 0;
			this.m_dOK = callBack;
			NKCUtil.SetLabelText(this.m_lbSourceType, NKCUtilString.GET_STRING_COLLECTION_UNIT);
			NKCUtil.SetLabelText(this.m_lbTitle, NKCUtilString.GET_STRING_TACTIC_UPDATE_REFOUND_POPUP_TITLE);
			NKCUtil.SetLabelText(this.m_lbDesc, NKCUtilString.GET_STRING_TACTIC_UPDATE_REFOUND_POPUP_DESC);
			NKCUtil.SetLabelText(this.m_lbOk, NKCUtilString.GET_STRING_DECK_BUTTON_OK);
			NKCUtil.SetBindFunction(this.m_btnOK, new UnityAction(this.OnClickRefoundOK));
			NKCUtil.SetGameobjectActive(this.m_objRefoundUnit, true);
			NKCUtil.SetGameobjectActive(this.m_objRecallUnit, false);
			NKCUtil.SetGameobjectActive(this.m_objRefoundLimitCnt, true);
			NKCUtil.SetLabelText(this.m_lbRefoundLimitCount, string.Format(NKCUtilString.GET_STRING_TACTIC_UPDATE_RETURN_COUNT, NKMCommonConst.TacticReturnMaxCount - NKCScenManager.CurrentUserData().m_unitTacticReturnCount));
			this.SetUnitRefoundReward();
			this.SetButtonState();
			base.UIOpened(true);
		}

		// Token: 0x06007660 RID: 30304 RVA: 0x00275EAC File Offset: 0x002740AC
		private NKCPopupRecallSlot GetRecallRewardSlot()
		{
			NKCPopupRecallSlot nkcpopupRecallSlot = UnityEngine.Object.Instantiate<NKCPopupRecallSlot>(this.m_pfbSlot);
			if (nkcpopupRecallSlot != null)
			{
				nkcpopupRecallSlot.transform.SetParent(this.m_trRewardSlotParent);
				nkcpopupRecallSlot.transform.localPosition = Vector3.zero;
				nkcpopupRecallSlot.transform.localScale = Vector3.one;
			}
			return nkcpopupRecallSlot;
		}

		// Token: 0x06007661 RID: 30305 RVA: 0x00275F00 File Offset: 0x00274100
		private void SetButtonState()
		{
			if (this.m_CurUnitType == NKM_UNIT_TYPE.NUT_NORMAL && this.m_TargetUnitID == 0)
			{
				this.m_btnOK.Lock(false);
				NKCUtil.SetLabelTextColor(this.m_lbOk, NKCUtil.GetColor("#212122"));
				return;
			}
			this.m_btnOK.UnLock(false);
			NKCUtil.SetLabelTextColor(this.m_lbOk, NKCUtil.GetColor("#582817"));
		}

		// Token: 0x06007662 RID: 30306 RVA: 0x00275F64 File Offset: 0x00274164
		private void SetRewardSlot(string title, List<NKMItemMiscData> lstReward, int slotIdx)
		{
			if (slotIdx >= this.m_lstRewardSlot.Count)
			{
				this.m_lstRewardSlot.Add(this.GetRecallRewardSlot());
			}
			List<NKCUISlot.SlotData> list = new List<NKCUISlot.SlotData>();
			for (int i = 0; i < lstReward.Count; i++)
			{
				list.Add(NKCUISlot.SlotData.MakeMiscItemData(lstReward[i], 0));
			}
			NKCUtil.SetGameobjectActive(this.m_lstRewardSlot[slotIdx], true);
			this.m_lstRewardSlot[slotIdx].SetData(title, list);
		}

		// Token: 0x06007663 RID: 30307 RVA: 0x00275FE0 File Offset: 0x002741E0
		private void SetUnitRecallReward()
		{
			NKCUtil.SetGameobjectActive(this.m_objUnit, true);
			NKCUtil.SetGameobjectActive(this.m_objShip, false);
			this.m_slotSourceUnit.SetSlotState(NKCUnitSortSystem.eUnitState.NONE);
			this.m_slotSourceUnit.SetData(this.m_NKMUnitData, NKMDeckIndex.None, false, null);
			this.m_slotTargetUnit.SetSlotState(NKCUnitSortSystem.eUnitState.NONE);
			this.m_slotTargetUnit.SetMode(NKCUIUnitSelectListSlotBase.eUnitSlotMode.Add, false, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnClickTargetUnitSlot), null);
			this.SetUnitRewardSlot();
		}

		// Token: 0x06007664 RID: 30308 RVA: 0x00276058 File Offset: 0x00274258
		private void SetUnitRewardSlot()
		{
			int num = 0;
			for (int i = 0; i < this.m_lstRewardSlot.Count; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstRewardSlot[i], false);
			}
			List<NKMItemMiscData> list = NKCRecallManager.ConvertUnitExpToResources(this.m_NKMUnitData).Values.ToList<NKMItemMiscData>();
			if (list.Count > 0)
			{
				list.Sort(new Comparison<NKMItemMiscData>(this.SortByID));
			}
			this.SetRewardSlot(NKCUtilString.GET_STRING_SORT_LEVEL, list, num++);
			List<NKMItemMiscData> list2 = NKCRecallManager.ConvertLimitBreakToResources(this.m_NKMUnitData).Values.ToList<NKMItemMiscData>();
			if (list2.Count > 0)
			{
				list2.Sort(new Comparison<NKMItemMiscData>(this.SortByID));
			}
			this.SetRewardSlot(NKCStringTable.GetString("SI_DP_LAB_MENU_NAME_LDS_UNIT_LIMITBREAK", false), list2, num++);
			List<NKMItemMiscData> list3 = NKCRecallManager.ConvertSkillToResources(this.m_NKMUnitData).Values.ToList<NKMItemMiscData>();
			if (list3.Count > 0)
			{
				list3.Sort(new Comparison<NKMItemMiscData>(this.SortByID));
			}
			this.SetRewardSlot(NKCStringTable.GetString("SI_DP_LAB_MENU_NAME_LDS_UNIT_SKILL_TRAIN", false), list3, num++);
			List<NKMItemMiscData> list4 = NKCRecallManager.ConvertUnitLifeTimeToResource(this.m_NKMUnitData).Values.ToList<NKMItemMiscData>();
			if (list4.Count > 0)
			{
				list4.Sort(new Comparison<NKMItemMiscData>(this.SortByID));
			}
			this.SetRewardSlot(NKCStringTable.GetString("SI_PF_BASE_MENU_PERSONNEL_LIFETIME_TEXT", false), list4, num++);
		}

		// Token: 0x06007665 RID: 30309 RVA: 0x002761B4 File Offset: 0x002743B4
		private void SetShipRecallReward()
		{
			NKCUtil.SetGameobjectActive(this.m_objUnit, false);
			NKCUtil.SetGameobjectActive(this.m_objShip, true);
			this.m_slotSourceShip.SetData(this.m_NKMUnitData, NKMDeckIndex.None, false, null);
			int num = 0;
			for (int i = 0; i < this.m_lstRewardSlot.Count; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstRewardSlot[i], false);
			}
			List<NKMItemMiscData> list = NKCRecallManager.ConvertShipBuildToResource(this.m_NKMUnitData).Values.ToList<NKMItemMiscData>();
			if (list.Count > 0)
			{
				list.Sort(new Comparison<NKMItemMiscData>(this.SortByID));
				this.SetRewardSlot(NKCStringTable.GetString("SI_DP_HANGAR_BUILD", false), list, num++);
			}
			List<NKMItemMiscData> list2 = NKCRecallManager.ConvertShipToResources(this.m_NKMUnitData).Values.ToList<NKMItemMiscData>();
			if (list2.Count > 0)
			{
				list2.Sort(new Comparison<NKMItemMiscData>(this.SortByID));
				this.SetRewardSlot(NKCStringTable.GetString("SI_DP_HANGAR_SHIPYARD", false), list2, num++);
			}
			List<NKMItemMiscData> list3 = NKCRecallManager.ConvertLimitBreakToResources(this.m_NKMUnitData).Values.ToList<NKMItemMiscData>();
			if (list3.Count > 0)
			{
				list3.Sort(new Comparison<NKMItemMiscData>(this.SortByID));
				this.SetRewardSlot(NKCStringTable.GetString("SI_DP_LAB_MENU_NAME_LDS_UNIT_LIMITBREAK", false), list3, num++);
			}
		}

		// Token: 0x06007666 RID: 30310 RVA: 0x002762F8 File Offset: 0x002744F8
		private void SetUnitRefoundReward()
		{
			NKCUtil.SetGameobjectActive(this.m_objUnit, true);
			NKCUtil.SetGameobjectActive(this.m_objShip, false);
			this.m_slotRefoundUnit.SetSlotState(NKCUnitSortSystem.eUnitState.NONE);
			this.m_slotRefoundUnit.SetData(this.m_NKMUnitData, NKMDeckIndex.None, false, null);
			this.SetUnitRewardSlot();
		}

		// Token: 0x06007667 RID: 30311 RVA: 0x00276348 File Offset: 0x00274548
		private int SortByID(NKMItemMiscData lItem, NKMItemMiscData rItem)
		{
			return lItem.ItemID.CompareTo(rItem.ItemID);
		}

		// Token: 0x06007668 RID: 30312 RVA: 0x00276369 File Offset: 0x00274569
		public void OnClickTargetUnitSlot(NKMUnitData unitData, NKMUnitTempletBase unitTempletBase, NKMDeckIndex deckIndex, NKCUnitSortSystem.eUnitState slotState, NKCUIUnitSelectList.eUnitSlotSelectState unitSlotSelectState)
		{
			this.UnitSelectList.Open(this.GetUnitSelectListOption(), new NKCUIUnitSelectList.OnUnitSelectCommand(this.OnUnitSelected), null, null, null, null);
		}

		// Token: 0x06007669 RID: 30313 RVA: 0x0027638C File Offset: 0x0027458C
		public void OnClickTargetOperatorSlot(NKMOperator operatorData, NKMUnitTempletBase unitTempletBase, NKMDeckIndex deckIndex, NKCUnitSortSystem.eUnitState slotState, NKCUIUnitSelectList.eUnitSlotSelectState unitSlotSelectState)
		{
		}

		// Token: 0x0600766A RID: 30314 RVA: 0x00276390 File Offset: 0x00274590
		private void OnUnitSelected(List<long> lstUnitUID)
		{
			this.m_TargetUnitID = (int)lstUnitUID.FirstOrDefault<long>();
			this.UnitSelectList.Close();
			NKMUnitTempletBase templetBase = NKMUnitTempletBase.Find(this.m_TargetUnitID);
			this.m_slotTargetUnit.SetData(templetBase, 1, 0, false, new NKCUIUnitSelectListSlotBase.OnSelectThisSlot(this.OnClickTargetUnitSlot));
		}

		// Token: 0x0600766B RID: 30315 RVA: 0x002763DC File Offset: 0x002745DC
		public void OnClickOK()
		{
			if (this.m_CurUnitType == NKM_UNIT_TYPE.NUT_NORMAL && this.m_TargetUnitID <= 0)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_RECALL_ERROR_ALT_UNIT_SELECT, null, "");
				return;
			}
			if (this.m_RecallTemplet == null)
			{
				return;
			}
			if (!NKCRecallManager.IsValidTime(this.m_RecallTemplet, NKCSynchronizedTime.GetServerUTCTime(0.0)))
			{
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_RECALL_PERIOD_EXPIRED, null, "");
				return;
			}
			if (this.m_CurUnitType != NKM_UNIT_TYPE.NUT_NORMAL)
			{
				if (this.m_CurUnitType == NKM_UNIT_TYPE.NUT_SHIP)
				{
					NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(this.m_NKMUnitData.m_UnitID);
					if (nkmunitTempletBase == null)
					{
						return;
					}
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendLine("[" + nkmunitTempletBase.GetUnitName() + "]");
					stringBuilder.AppendLine(NKCUtilString.GET_STRING_RECALL_FINAL_CHECK_POPUP_DESC);
					stringBuilder.Append(string.Format(NKCUtilString.GET_STRING_RECALL_FINAL_CHECK_POPUP_DATE, this.m_RecallTemplet.IntervalTemplet.GetStartDateUtc(), this.m_RecallTemplet.IntervalTemplet.GetEndDateUtc(), NKCSynchronizedTime.GetTimeLeftString(this.m_RecallTemplet.IntervalTemplet.GetEndDateUtc().Ticks)));
					NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, stringBuilder.ToString(), new NKCPopupOKCancel.OnButton(this.OnConfirm), null, false);
					stringBuilder.Clear();
				}
				return;
			}
			NKMUnitTempletBase nkmunitTempletBase2 = NKMUnitTempletBase.Find(this.m_NKMUnitData.m_UnitID);
			if (nkmunitTempletBase2 == null)
			{
				return;
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			stringBuilder2.AppendLine(string.Concat(new string[]
			{
				"[",
				nkmunitTempletBase2.GetUnitTitle(),
				"] [",
				nkmunitTempletBase2.GetUnitName(),
				"]"
			}));
			stringBuilder2.AppendLine(NKCUtilString.GET_STRING_RECALL_FINAL_CHECK_POPUP_DESC);
			stringBuilder2.Append(string.Format(NKCUtilString.GET_STRING_RECALL_FINAL_CHECK_POPUP_DATE, this.m_RecallTemplet.IntervalTemplet.GetStartDate(), this.m_RecallTemplet.IntervalTemplet.GetEndDate(), NKCSynchronizedTime.GetTimeLeftString(this.m_RecallTemplet.IntervalTemplet.GetEndDateUtc().Ticks)));
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, stringBuilder2.ToString(), new NKCPopupOKCancel.OnButton(this.OnConfirm), null, false);
			stringBuilder2.Clear();
		}

		// Token: 0x0600766C RID: 30316 RVA: 0x00276605 File Offset: 0x00274805
		private void OnClickRefoundOK()
		{
			UnityAction dOK = this.m_dOK;
			if (dOK != null)
			{
				dOK();
			}
			NKCPopupRecall.CheckInstanceAndClose();
		}

		// Token: 0x0600766D RID: 30317 RVA: 0x0027661D File Offset: 0x0027481D
		public void OnConfirm()
		{
			NKCRecallManager.UnEquipAndRecall(this.m_NKMUnitData, this.m_TargetUnitID);
		}

		// Token: 0x170013BD RID: 5053
		// (get) Token: 0x0600766E RID: 30318 RVA: 0x00276630 File Offset: 0x00274830
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

		// Token: 0x0600766F RID: 30319 RVA: 0x00276654 File Offset: 0x00274854
		public NKCUIUnitSelectList.UnitSelectListOptions GetUnitSelectListOption()
		{
			NKCUIUnitSelectList.UnitSelectListOptions result = new NKCUIUnitSelectList.UnitSelectListOptions(this.m_CurUnitType, false, NKM_DECK_TYPE.NDT_NORMAL, NKCUIUnitSelectList.eUnitSelectListMode.CUSTOM_LIST, true);
			result.lstSortOption = NKCUnitSortSystem.GetDefaultSortOptions(this.m_CurUnitType, false, false);
			result.bShowRemoveSlot = false;
			result.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
			result.setExcludeUnitUID = new HashSet<long>();
			result.m_bHideUnitCount = true;
			result.bDescending = true;
			string strUpsideMenuName = "";
			result.strUpsideMenuName = strUpsideMenuName;
			result.setUnitFilterCategory = NKCUnitSortSystem.setDefaultUnitFilterCategory;
			result.setUnitSortCategory = NKCUnitSortSystem.setDefaultUnitSortCategory;
			result.setShipFilterCategory = NKCUnitSortSystem.setDefaultShipFilterCategory;
			result.setShipSortCategory = NKCUnitSortSystem.setDefaultShipSortCategory;
			result.bEnableLockUnitSystem = false;
			result.setOnlyIncludeUnitID = this.GetUnitIDs();
			result.m_bUseFavorite = true;
			return result;
		}

		// Token: 0x06007670 RID: 30320 RVA: 0x00276714 File Offset: 0x00274914
		private HashSet<int> GetUnitIDs()
		{
			List<NKMRecallUnitExchangeTemplet> list = NKMRecallUnitExchangeTemplet.GetUnitGroupTemplet(this.m_RecallTemplet.UnitExchangeGroupId).ToList<NKMRecallUnitExchangeTemplet>();
			HashSet<int> hashSet = new HashSet<int>();
			for (int i = 0; i < list.Count; i++)
			{
				hashSet.Add(list[i].UnitId);
			}
			return hashSet;
		}

		// Token: 0x06007671 RID: 30321 RVA: 0x00276764 File Offset: 0x00274964
		public static int GetRecallRewardCnt(NKMUnitData unitData)
		{
			List<NKMItemMiscData> list = NKCRecallManager.ConvertUnitExpToResources(unitData).Values.ToList<NKMItemMiscData>();
			List<NKMItemMiscData> list2 = NKCRecallManager.ConvertLimitBreakToResources(unitData).Values.ToList<NKMItemMiscData>();
			List<NKMItemMiscData> list3 = NKCRecallManager.ConvertSkillToResources(unitData).Values.ToList<NKMItemMiscData>();
			List<NKMItemMiscData> list4 = NKCRecallManager.ConvertUnitLifeTimeToResource(unitData).Values.ToList<NKMItemMiscData>();
			return list.Count + list2.Count + list3.Count + list4.Count;
		}

		// Token: 0x040062BF RID: 25279
		public const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX";

		// Token: 0x040062C0 RID: 25280
		public const string UI_ASSET_NAME = "NKM_UI_POPUP_RECALL";

		// Token: 0x040062C1 RID: 25281
		private static NKCPopupRecall m_Instance;

		// Token: 0x040062C2 RID: 25282
		public NKCPopupRecallSlot m_pfbSlot;

		// Token: 0x040062C3 RID: 25283
		public Text m_lbTitle;

		// Token: 0x040062C4 RID: 25284
		public Text m_lbDesc;

		// Token: 0x040062C5 RID: 25285
		[Header("유닛/함선 텍스트")]
		public Text m_lbSourceType;

		// Token: 0x040062C6 RID: 25286
		[Header("유닛")]
		public GameObject m_objRecallUnit;

		// Token: 0x040062C7 RID: 25287
		public GameObject m_objUnit;

		// Token: 0x040062C8 RID: 25288
		public NKCUIUnitSelectListSlot m_slotSourceUnit;

		// Token: 0x040062C9 RID: 25289
		public NKCUIUnitSelectListSlot m_slotTargetUnit;

		// Token: 0x040062CA RID: 25290
		[Header("함선")]
		public GameObject m_objShip;

		// Token: 0x040062CB RID: 25291
		public NKCUIShipSelectListSlot m_slotSourceShip;

		// Token: 0x040062CC RID: 25292
		[Header("보상들")]
		public Transform m_trRewardSlotParent;

		// Token: 0x040062CD RID: 25293
		[Header("하단버튼")]
		public NKCUIComStateButton m_btnCancel;

		// Token: 0x040062CE RID: 25294
		public NKCUIComStateButton m_btnOK;

		// Token: 0x040062CF RID: 25295
		public Text m_lbOk;

		// Token: 0x040062D0 RID: 25296
		[Header("임시 리콜")]
		public GameObject m_objRefoundUnit;

		// Token: 0x040062D1 RID: 25297
		public NKCUIUnitSelectListSlot m_slotRefoundUnit;

		// Token: 0x040062D2 RID: 25298
		public GameObject m_objRefoundLimitCnt;

		// Token: 0x040062D3 RID: 25299
		public Text m_lbRefoundLimitCount;

		// Token: 0x040062D4 RID: 25300
		private List<NKCPopupRecallSlot> m_lstRewardSlot = new List<NKCPopupRecallSlot>();

		// Token: 0x040062D5 RID: 25301
		private NKMRecallTemplet m_RecallTemplet;

		// Token: 0x040062D6 RID: 25302
		private NKMUnitData m_NKMUnitData;

		// Token: 0x040062D7 RID: 25303
		private int m_TargetUnitID;

		// Token: 0x040062D8 RID: 25304
		private NKM_UNIT_TYPE m_CurUnitType;

		// Token: 0x040062D9 RID: 25305
		private UnityAction m_dOK;

		// Token: 0x040062DA RID: 25306
		private NKCUIUnitSelectList m_UIUnitSelectList;
	}
}
