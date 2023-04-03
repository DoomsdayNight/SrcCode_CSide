using System;
using System.Collections.Generic;
using ClientPacket.User;
using NKC.UI.NPC;
using NKC.UI.Result;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009BD RID: 2493
	public class NKCUIMissionAchievement : NKCUIBase
	{
		// Token: 0x1700122E RID: 4654
		// (get) Token: 0x060069B9 RID: 27065 RVA: 0x00223870 File Offset: 0x00221A70
		public static NKCUIMissionAchievement Instance
		{
			get
			{
				if (NKCUIMissionAchievement.m_Instance == null)
				{
					NKCUIMissionAchievement.m_Instance = NKCUIManager.OpenNewInstance<NKCUIMissionAchievement>("ab_ui_nkm_ui_mission", "NKM_UI_MISSION", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIMissionAchievement.CleanupInstance)).GetInstance<NKCUIMissionAchievement>();
					NKCUIMissionAchievement.m_Instance.InitUI();
				}
				return NKCUIMissionAchievement.m_Instance;
			}
		}

		// Token: 0x060069BA RID: 27066 RVA: 0x002238BF File Offset: 0x00221ABF
		private static void CleanupInstance()
		{
			NKCUIMissionAchievement.m_Instance = null;
		}

		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x060069BB RID: 27067 RVA: 0x002238C7 File Offset: 0x00221AC7
		public static bool HasInstance
		{
			get
			{
				return NKCUIMissionAchievement.m_Instance != null;
			}
		}

		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x060069BC RID: 27068 RVA: 0x002238D4 File Offset: 0x00221AD4
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIMissionAchievement.m_Instance != null && NKCUIMissionAchievement.m_Instance.IsOpen;
			}
		}

		// Token: 0x060069BD RID: 27069 RVA: 0x002238EF File Offset: 0x00221AEF
		public static void CheckInstanceAndClose()
		{
			if (NKCUIMissionAchievement.m_Instance != null && NKCUIMissionAchievement.m_Instance.IsOpen)
			{
				NKCUIMissionAchievement.m_Instance.Close();
			}
		}

		// Token: 0x17001231 RID: 4657
		// (get) Token: 0x060069BE RID: 27070 RVA: 0x00223914 File Offset: 0x00221B14
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_MISSION;
			}
		}

		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x060069BF RID: 27071 RVA: 0x0022391B File Offset: 0x00221B1B
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_SYSTEM_MISSION";
			}
		}

		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x060069C0 RID: 27072 RVA: 0x00223922 File Offset: 0x00221B22
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x060069C1 RID: 27073 RVA: 0x00223928 File Offset: 0x00221B28
		public void InitUI()
		{
			base.gameObject.SetActive(false);
			this.m_LoopScrollRect.dOnGetObject += this.GetMissionSlot;
			this.m_LoopScrollRect.dOnReturnObject += this.ReturnMissionSlot;
			this.m_LoopScrollRect.dOnProvideData += this.ProvideData;
			this.m_LoopScrollRect.ContentConstraintCount = 1;
			NKCUtil.SetScrollHotKey(this.m_LoopScrollRect, null);
			this.m_LoopScrollRectGrowth.dOnGetObject += this.GetGrowthMissionSlot;
			this.m_LoopScrollRectGrowth.dOnReturnObject += this.ReturnGrowthMissionSlot;
			this.m_LoopScrollRectGrowth.dOnProvideData += this.ProvideGrowthData;
			this.m_LoopScrollRectGrowth.ContentConstraintCount = 1;
			NKCUtil.SetScrollHotKey(this.m_LoopScrollRectGrowth, null);
			this.m_LoopScrollRectTab.dOnGetObject += this.GetMissionTab;
			this.m_LoopScrollRectTab.dOnReturnObject += this.ReturnMissionTab;
			this.m_LoopScrollRectTab.dOnProvideData += this.ProvideTabData;
			this.m_LoopScrollRectTab.ContentConstraintCount = 1;
			this.m_tglGroup.m_bCallbackOnUnSelect = true;
			this.m_GROWTH_BOTTOM_BUTTON_QUICK.PointerClick.RemoveAllListeners();
			this.m_GROWTH_BOTTOM_BUTTON_QUICK.PointerClick.AddListener(new UnityAction(this.OnClickGrowthMoveToCenter));
			this.m_GROWTH_BOTTOM_BUTTON_RECEIVE.PointerClick.RemoveAllListeners();
			this.m_GROWTH_BOTTOM_BUTTON_RECEIVE.PointerClick.AddListener(new UnityAction(this.OnClickGrowthComplete));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnCompleteAll, new UnityAction(this.OnClickAchievementBundleFinish));
			NKCUtil.SetButtonClickDelegate(this.m_NKM_UI_ALLCLEAR_MISSION_BOTTOM_BUTTON_ALL, new UnityAction(this.OnClickAchievementBundleFinish));
			this.m_REPEAT_BOTTOM_BUTTON_ALL.PointerClick.RemoveAllListeners();
			this.m_REPEAT_BOTTOM_BUTTON_ALL.PointerClick.AddListener(new UnityAction(this.OnClickAchievementBundleFinish));
			for (int i = 0; i < this.m_lstRewardSlot.Count; i++)
			{
				this.m_lstRewardSlot[i].Init();
			}
		}

		// Token: 0x060069C2 RID: 27074 RVA: 0x00223B34 File Offset: 0x00221D34
		private void RefreshTabTempletList()
		{
			this.m_lstMissionTabTemplet = new List<NKMMissionTabTemplet>();
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			foreach (NKMMissionTabTemplet nkmmissionTabTemplet in NKMMissionManager.DicMissionTab.Values)
			{
				if (nkmmissionTabTemplet.EnableByTag && nkmmissionTabTemplet.m_Visible && nkmmissionTabTemplet.m_MissionType != NKM_MISSION_TYPE.GROWTH_COMPLETE && nkmmissionTabTemplet.m_MissionType != NKM_MISSION_TYPE.COMBINE_GUIDE_MISSION)
				{
					if (!NKMMissionManager.CheckMissionTabUnlocked(nkmmissionTabTemplet.m_tabID, nkmuserData))
					{
						if (nkmmissionTabTemplet.m_VisibleWhenLocked && !NKMMissionManager.IsMissionTabExpired(nkmmissionTabTemplet, nkmuserData))
						{
							if (nkmmissionTabTemplet.m_UnlockInfo.Count <= 1)
							{
								this.m_lstMissionTabTemplet.Add(nkmmissionTabTemplet);
							}
							else
							{
								NKMUserData cNKMUserData = nkmuserData;
								UnlockInfo unlockInfo = nkmmissionTabTemplet.m_UnlockInfo[0];
								if (NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo, false))
								{
									this.m_lstMissionTabTemplet.Add(nkmmissionTabTemplet);
								}
							}
						}
					}
					else if (NKMContentUnlockManager.IsContentUnlocked(nkmuserData, nkmmissionTabTemplet.m_UnlockInfo, false))
					{
						if (nkmmissionTabTemplet.m_completeMissionID > 0)
						{
							NKMMissionData missionDataByMissionId = nkmuserData.m_MissionData.GetMissionDataByMissionId(nkmmissionTabTemplet.m_completeMissionID);
							if (missionDataByMissionId != null && missionDataByMissionId.isComplete)
							{
								continue;
							}
						}
						if (nkmmissionTabTemplet.m_firstMissionID > 0)
						{
							NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(nkmmissionTabTemplet.m_firstMissionID);
							if (missionTemplet != null && missionTemplet.m_MissionRequire > 0)
							{
								NKMMissionData missionDataByMissionId2 = nkmuserData.m_MissionData.GetMissionDataByMissionId(missionTemplet.m_MissionRequire);
								if (missionDataByMissionId2 == null || !missionDataByMissionId2.isComplete)
								{
									continue;
								}
							}
						}
						this.m_lstMissionTabTemplet.Add(nkmmissionTabTemplet);
					}
				}
			}
			this.m_lstMissionTabTemplet.Sort(new Comparison<NKMMissionTabTemplet>(this.CompTabSort));
			this.m_LoopScrollRectTab.TotalCount = this.m_lstMissionTabTemplet.Count;
			this.m_LoopScrollRectTab.RefreshCells(false);
		}

		// Token: 0x060069C3 RID: 27075 RVA: 0x00223D0C File Offset: 0x00221F0C
		private int CompTabSort(NKMMissionTabTemplet lItem, NKMMissionTabTemplet rItem)
		{
			return lItem.m_tabID.CompareTo(rItem.m_tabID);
		}

		// Token: 0x060069C4 RID: 27076 RVA: 0x00223D20 File Offset: 0x00221F20
		public RectTransform GetMissionTab(int index)
		{
			NKCUIMissionAchieveTab newInstance = NKCUIMissionAchieveTab.GetNewInstance(null, "AB_UI_NKM_UI_MISSION", "NKM_UI_MISSION_TAB");
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x060069C5 RID: 27077 RVA: 0x00223D50 File Offset: 0x00221F50
		public void ReturnMissionTab(Transform tr)
		{
			NKCUIMissionAchieveTab component = tr.GetComponent<NKCUIMissionAchieveTab>();
			tr.SetParent(base.transform);
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x060069C6 RID: 27078 RVA: 0x00223D8C File Offset: 0x00221F8C
		public void ProvideTabData(Transform tr, int index)
		{
			NKCUIMissionAchieveTab component = tr.GetComponent<NKCUIMissionAchieveTab>();
			if (!this.m_lstMissionTabTemplet[index].EnableByTag)
			{
				NKCUtil.SetGameobjectActive(component, false);
				return;
			}
			if (!this.m_lstMissionTabTemplet[index].m_Visible)
			{
				NKCUtil.SetGameobjectActive(component, false);
				return;
			}
			NKCUtil.SetGameobjectActive(component, true);
			component.SetData(this.m_lstMissionTabTemplet[index], this.m_tglGroup, new NKCUIMissionAchieveTab.OnClickTab(this.OnClickTab));
			component.SetCompleteObject(component.GetCompleted());
			component.SetLockObject(false);
			component.gameObject.name = this.m_lstMissionTabTemplet[index].m_tabID.ToString("D3");
			if (!this.m_dicMissionTab.ContainsKey(this.m_lstMissionTabTemplet[index].m_tabID))
			{
				this.m_dicMissionTab.Add(this.m_lstMissionTabTemplet[index].m_tabID, component);
				return;
			}
			this.m_dicMissionTab[this.m_lstMissionTabTemplet[index].m_tabID] = component;
		}

		// Token: 0x060069C7 RID: 27079 RVA: 0x00223E93 File Offset: 0x00222093
		public RectTransform GetMissionSlot(int index)
		{
			NKCUIMissionAchieveSlot newInstance = NKCUIMissionAchieveSlot.GetNewInstance(null, "AB_UI_NKM_UI_MISSION", "NKM_UI_MISSION_LIST_SLOT");
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x060069C8 RID: 27080 RVA: 0x00223EB0 File Offset: 0x002220B0
		public void ReturnMissionSlot(Transform tr)
		{
			NKCUIMissionAchieveSlot component = tr.GetComponent<NKCUIMissionAchieveSlot>();
			tr.SetParent(base.transform);
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x060069C9 RID: 27081 RVA: 0x00223EEC File Offset: 0x002220EC
		public void ProvideData(Transform tr, int index)
		{
			NKCUIMissionAchieveSlot component = tr.GetComponent<NKCUIMissionAchieveSlot>();
			if (component != null)
			{
				if (0 <= index && index < this.m_lstCurrentList.Count)
				{
					NKMMissionTemplet cNKMMissionTemplet = this.m_lstCurrentList[index];
					component.SetData(cNKMMissionTemplet, new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickMove), new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickComplete), null, null);
					return;
				}
				NKCUtil.SetGameobjectActive(component, false);
			}
		}

		// Token: 0x060069CA RID: 27082 RVA: 0x00223F54 File Offset: 0x00222154
		private void BuildMissionTempletListByTab(int tabID)
		{
			this.m_lstCurrentList.Clear();
			List<NKMMissionTemplet> list = this.m_dicNKMMissionTemplet[tabID];
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(tabID);
			if (missionTabTemplet == null)
			{
				return;
			}
			if (missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.REPEAT_DAILY)
			{
				this.m_lstDailyRepeatMissionTemplet.Clear();
			}
			if (missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.REPEAT_WEEKLY)
			{
				this.m_lstWeeklyRepeatMissionTemplet.Clear();
			}
			for (int i = 0; i < list.Count; i++)
			{
				NKMMissionTemplet nkmmissionTemplet = list[i];
				if (nkmmissionTemplet != null && (missionTabTemplet.m_MissionType != NKM_MISSION_TYPE.REPEAT_DAILY || nkmmissionTemplet.m_ResetInterval == NKM_MISSION_RESET_INTERVAL.DAILY) && (missionTabTemplet.m_MissionType != NKM_MISSION_TYPE.REPEAT_WEEKLY || nkmmissionTemplet.m_ResetInterval == NKM_MISSION_RESET_INTERVAL.WEEKLY))
				{
					if (missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.REPEAT_DAILY && nkmmissionTemplet.m_MissionCond.mission_cond == NKM_MISSION_COND.HAVE_DAILY_POINT)
					{
						this.m_lstDailyRepeatMissionTemplet.Add(nkmmissionTemplet);
					}
					else if (missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.REPEAT_WEEKLY && nkmmissionTemplet.m_MissionCond.mission_cond == NKM_MISSION_COND.HAVE_WEEKLY_POINT)
					{
						this.m_lstWeeklyRepeatMissionTemplet.Add(nkmmissionTemplet);
					}
					else
					{
						if (nkmmissionTemplet.m_MissionRequire != 0)
						{
							NKMMissionData nkmmissionData = myUserData.m_MissionData.GetMissionData(nkmmissionTemplet);
							if (nkmmissionData == null)
							{
								goto IL_18E;
							}
							if (nkmmissionData.mission_id == nkmmissionTemplet.m_MissionID)
							{
								this.m_lstCurrentList.Add(nkmmissionTemplet);
								goto IL_18E;
							}
							nkmmissionData = myUserData.m_MissionData.GetMissionDataByMissionId(nkmmissionTemplet.m_MissionRequire);
							if (nkmmissionData == null)
							{
								goto IL_18E;
							}
							if (nkmmissionData.isComplete && nkmmissionData.mission_id == nkmmissionTemplet.m_MissionRequire)
							{
								this.m_lstCurrentList.Add(nkmmissionTemplet);
								goto IL_18E;
							}
							if (nkmmissionData.mission_id <= nkmmissionTemplet.m_MissionRequire)
							{
								goto IL_18E;
							}
						}
						this.m_lstCurrentList.Add(nkmmissionTemplet);
					}
				}
				IL_18E:;
			}
			this.m_lstCurrentList.Sort(new Comparison<NKMMissionTemplet>(NKMMissionManager.Comparer));
			this.m_iDailyIndex = -1;
			this.m_iWeeklyIndex = -1;
			this.m_iMonthlyIndex = -1;
			for (int j = 0; j < this.m_lstCurrentList.Count; j++)
			{
				NKMMissionManager.MissionStateData missionStateData = NKMMissionManager.GetMissionStateData(this.m_lstCurrentList[j]);
				if (!missionStateData.IsMissionCompleted && missionStateData.progressCount < this.m_lstCurrentList[j].m_Times)
				{
					if (this.m_lstCurrentList[j].m_ResetInterval == NKM_MISSION_RESET_INTERVAL.DAILY && this.m_iDailyIndex < 0)
					{
						this.m_iDailyIndex = j;
					}
					else if (this.m_lstCurrentList[j].m_ResetInterval == NKM_MISSION_RESET_INTERVAL.WEEKLY && this.m_iWeeklyIndex < 0)
					{
						this.m_iWeeklyIndex = j;
					}
					else if (this.m_lstCurrentList[j].m_ResetInterval == NKM_MISSION_RESET_INTERVAL.MONTHLY && this.m_iMonthlyIndex < 0)
					{
						this.m_iMonthlyIndex = j;
					}
				}
			}
			this.m_lstDailyRepeatMissionTemplet.Sort(new Comparison<NKMMissionTemplet>(this.CompRepeatSort));
			this.m_lstWeeklyRepeatMissionTemplet.Sort(new Comparison<NKMMissionTemplet>(this.CompRepeatSort));
		}

		// Token: 0x060069CB RID: 27083 RVA: 0x0022422A File Offset: 0x0022242A
		private int CompRepeatSort(NKMMissionTemplet x, NKMMissionTemplet y)
		{
			return x.m_Times.CompareTo(y.m_Times);
		}

		// Token: 0x060069CC RID: 27084 RVA: 0x00224240 File Offset: 0x00222440
		public void OnClickMove(NKCUIMissionAchieveSlot cNKCUIMissionAchieveSlot)
		{
			if (cNKCUIMissionAchieveSlot == null)
			{
				return;
			}
			NKMMissionTemplet nkmmissionTemplet = cNKCUIMissionAchieveSlot.GetNKMMissionTemplet();
			if (nkmmissionTemplet == null)
			{
				return;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(nkmmissionTemplet.m_MissionTabId);
			if (missionTabTemplet == null)
			{
				return;
			}
			if (NKMMissionManager.IsMissionTabExpired(missionTabTemplet, NKCScenManager.CurrentUserData()))
			{
				this.m_NKM_MISSION_TAB_ID = 2;
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_MISSION_EXPIRED, new NKCPopupOKCancel.OnButton(this.RefreshMissionUI), NKCUtilString.GET_STRING_CONFIRM);
				return;
			}
			NKCContentManager.MoveToShortCut(nkmmissionTemplet.m_ShortCutType, nkmmissionTemplet.m_ShortCut, false);
		}

		// Token: 0x060069CD RID: 27085 RVA: 0x002242BC File Offset: 0x002224BC
		public void OnClickComplete(NKCUIMissionAchieveSlot cNKCUIMissionAchieveSlot)
		{
			if (cNKCUIMissionAchieveSlot == null)
			{
				return;
			}
			NKMMissionTemplet nkmmissionTemplet = cNKCUIMissionAchieveSlot.GetNKMMissionTemplet();
			if (nkmmissionTemplet == null)
			{
				return;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(nkmmissionTemplet.m_MissionTabId);
			if (missionTabTemplet == null)
			{
				return;
			}
			if (NKMMissionManager.IsMissionTabExpired(missionTabTemplet, NKCScenManager.CurrentUserData()))
			{
				this.m_NKM_MISSION_TAB_ID = 2;
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_MISSION_EXPIRED, new NKCPopupOKCancel.OnButton(this.RefreshMissionUI), NKCUtilString.GET_STRING_CONFIRM);
				return;
			}
			NKCPacketSender.Send_NKMPacket_MISSION_COMPLETE_REQ(cNKCUIMissionAchieveSlot.GetNKMMissionTemplet().m_MissionTabId, cNKCUIMissionAchieveSlot.GetNKMMissionTemplet().m_GroupId, cNKCUIMissionAchieveSlot.GetNKMMissionTemplet().m_MissionID);
		}

		// Token: 0x060069CE RID: 27086 RVA: 0x0022434C File Offset: 0x0022254C
		public RectTransform GetGrowthMissionSlot(int index)
		{
			NKCUIMissionAchieveSlotGrowth newInstance = NKCUIMissionAchieveSlotGrowth.GetNewInstance(null, new NKCUIMissionAchieveSlotGrowth.OnClickMASlot(this.OnClickMove), new NKCUIMissionAchieveSlotGrowth.OnClickMASlot(this.OnClickComplete));
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x060069CF RID: 27087 RVA: 0x0022438C File Offset: 0x0022258C
		public void ReturnGrowthMissionSlot(Transform tr)
		{
			NKCUIMissionAchieveSlotGrowth component = tr.GetComponent<NKCUIMissionAchieveSlotGrowth>();
			tr.SetParent(base.transform);
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x060069D0 RID: 27088 RVA: 0x002243C8 File Offset: 0x002225C8
		public void ProvideGrowthData(Transform tr, int index)
		{
			NKCUIMissionAchieveSlotGrowth component = tr.GetComponent<NKCUIMissionAchieveSlotGrowth>();
			if (component != null)
			{
				NKMMissionTemplet data = this.m_lstGrowthMissionList[index];
				component.SetData(data);
			}
		}

		// Token: 0x060069D1 RID: 27089 RVA: 0x002243F9 File Offset: 0x002225F9
		private void BuildGrowthMissionTempletByTab(int tabID)
		{
			this.m_lstGrowthMissionList = this.m_dicNKMMissionTemplet[tabID];
			this.m_lstGrowthMissionList.Sort(new Comparison<NKMMissionTemplet>(this.CompareByID));
		}

		// Token: 0x060069D2 RID: 27090 RVA: 0x00224424 File Offset: 0x00222624
		private int CompareByID(NKMMissionTemplet x, NKMMissionTemplet y)
		{
			return x.m_MissionID.CompareTo(y.m_MissionID);
		}

		// Token: 0x060069D3 RID: 27091 RVA: 0x00224438 File Offset: 0x00222638
		public void OnClickMove(NKCUIMissionAchieveSlotGrowth cNKCUIMissionAchieveSlot)
		{
			if (cNKCUIMissionAchieveSlot == null)
			{
				return;
			}
			NKMMissionTemplet nkmmissionTemplet = cNKCUIMissionAchieveSlot.GetNKMMissionTemplet();
			if (nkmmissionTemplet == null)
			{
				return;
			}
			NKCContentManager.MoveToShortCut(nkmmissionTemplet.m_ShortCutType, nkmmissionTemplet.m_ShortCut, false);
		}

		// Token: 0x060069D4 RID: 27092 RVA: 0x0022446C File Offset: 0x0022266C
		public void OnClickComplete(NKCUIMissionAchieveSlotGrowth cNKCUIMissionAchieveSlot)
		{
			if (cNKCUIMissionAchieveSlot == null)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_MISSION_COMPLETE_REQ(cNKCUIMissionAchieveSlot.GetNKMMissionTemplet().m_MissionTabId, cNKCUIMissionAchieveSlot.GetNKMMissionTemplet().m_GroupId, cNKCUIMissionAchieveSlot.GetNKMMissionTemplet().m_MissionID);
		}

		// Token: 0x060069D5 RID: 27093 RVA: 0x002244A0 File Offset: 0x002226A0
		public void Open(int reservedTabID = 0)
		{
			this.m_bRefreshReserved = false;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (this.m_bFirstOpen)
			{
				this.m_LoopScrollRect.PrepareCells(0);
				this.m_LoopScrollRectGrowth.PrepareCells(0);
				this.m_LoopScrollRectTab.PrepareCells(0);
				this.m_bFirstOpen = false;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(reservedTabID);
			if (missionTabTemplet != null && missionTabTemplet.EnableByTag && missionTabTemplet.m_Visible)
			{
				this.m_NKM_MISSION_TAB_ID = reservedTabID;
			}
			else
			{
				this.m_NKM_MISSION_TAB_ID = 2;
			}
			this.RefreshTabTempletList();
			this.m_dicMissionTab[this.m_NKM_MISSION_TAB_ID].GetToggle().Select(true, false, false);
			this.SetUIByTab(this.m_NKM_MISSION_TAB_ID);
			this.SetCompletableMissionAlarm();
			base.UIOpened(true);
			this.CheckTutorial();
		}

		// Token: 0x060069D6 RID: 27094 RVA: 0x00224561 File Offset: 0x00222761
		public override void UnHide()
		{
			base.UnHide();
			if (this.m_bRefreshReserved)
			{
				this.SetUIByCurrTab();
				this.m_bRefreshReserved = false;
			}
		}

		// Token: 0x060069D7 RID: 27095 RVA: 0x00224580 File Offset: 0x00222780
		public void SetCompletableMissionAlarm()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			foreach (KeyValuePair<int, NKCUIMissionAchieveTab> keyValuePair in this.m_dicMissionTab)
			{
				keyValuePair.Value.SetNewObject(!keyValuePair.Value.GetLocked() && !keyValuePair.Value.GetCompleted() && myUserData.m_MissionData.CheckCompletableMission(myUserData, keyValuePair.Key, false));
			}
		}

		// Token: 0x060069D8 RID: 27096 RVA: 0x0022461C File Offset: 0x0022281C
		public void SelectNextTab()
		{
			this.RefreshTabTempletList();
			NKMMissionTabTemplet nextMissionTabTemplet = NKMMissionManager.GetNextMissionTabTemplet(this.m_NKM_MISSION_TAB_ID);
			if (nextMissionTabTemplet == null)
			{
				this.m_dicMissionTab[2].GetToggle().Select(true, false, true);
				this.m_NKM_MISSION_TAB_ID = 2;
				return;
			}
			if (this.m_dicMissionTab.ContainsKey(nextMissionTabTemplet.m_tabID))
			{
				this.m_dicMissionTab[nextMissionTabTemplet.m_tabID].GetToggle().Select(true, false, true);
				this.m_NKM_MISSION_TAB_ID = nextMissionTabTemplet.m_tabID;
				return;
			}
		}

		// Token: 0x060069D9 RID: 27097 RVA: 0x002246A0 File Offset: 0x002228A0
		private void OnClickDailyBox(NKMMissionTemplet missionTemplet)
		{
			if (missionTemplet == null)
			{
				return;
			}
			if (this.m_bBlockRepeatBox)
			{
				return;
			}
			NKMMissionData missionData = NKCScenManager.CurrentUserData().m_MissionData.GetMissionData(missionTemplet);
			if (missionData != null && !NKMMissionManager.CheckCanReset(missionTemplet.m_ResetInterval, missionData))
			{
				NKMMissionTemplet missionTemplet2 = NKMMissionManager.GetMissionTemplet(missionData.mission_id);
				if (missionTemplet2 != null && !missionData.isComplete && missionData.times >= missionTemplet.m_Times && missionData.mission_id <= missionTemplet.m_MissionID)
				{
					this.m_bBlockRepeatBox = true;
					NKCPacketSender.Send_NKMPacket_MISSION_COMPLETE_REQ(missionTemplet.m_MissionTabId, missionTemplet2.m_GroupId, missionTemplet2.m_MissionID);
					return;
				}
			}
			if (missionTemplet.m_MissionReward.Count > 0)
			{
				NKCUISlotListViewer.Instance.OpenMissionRewardList(missionTemplet.m_MissionReward);
			}
		}

		// Token: 0x060069DA RID: 27098 RVA: 0x0022474C File Offset: 0x0022294C
		public void SetUIByCurrTab()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				this.m_bRefreshReserved = true;
				return;
			}
			this.SetUIByTab(this.m_NKM_MISSION_TAB_ID);
			this.SetCompletableMissionAlarm();
		}

		// Token: 0x060069DB RID: 27099 RVA: 0x00224778 File Offset: 0x00222978
		private void SetUIByTab(int _NKM_MISSION_TAB_ID)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(_NKM_MISSION_TAB_ID);
			if (missionTabTemplet == null)
			{
				return;
			}
			this.m_bBlockRepeatBox = false;
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_ACHIEVE, missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.ACHIEVE || missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.REPEAT_DAILY || missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.REPEAT_WEEKLY || missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.EMBLEM);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_ACHIEVE_POINT, missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.ACHIEVE);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_REPEAT_POINT, missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.REPEAT_DAILY || missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.REPEAT_WEEKLY);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_GROWTH_BANNER, missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.GROWTH_UNIT);
			if (!this.m_dicNKMMissionTemplet.ContainsKey(_NKM_MISSION_TAB_ID))
			{
				this.m_dicNKMMissionTemplet.Add(_NKM_MISSION_TAB_ID, NKMMissionManager.GetMissionTempletListByType(_NKM_MISSION_TAB_ID));
			}
			switch (missionTabTemplet.m_MissionType)
			{
			default:
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_ACHIEVE, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_GROWTH, false);
				this.BuildMissionTempletListByTab(_NKM_MISSION_TAB_ID);
				this.m_LoopScrollRect.TotalCount = this.m_lstCurrentList.Count;
				this.m_LoopScrollRect.StopMovement();
				this.m_LoopScrollRect.SetIndexPosition(0);
				if (missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.REPEAT_DAILY || missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.REPEAT_WEEKLY)
				{
					this.m_LoopScrollRect.GetComponent<RectTransform>().offsetMax = new Vector2(0f, -144f);
					this.m_LoopScrollRect.GetComponent<RectTransform>().offsetMin = new Vector2(0f, -33f);
				}
				else
				{
					this.m_LoopScrollRect.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
					this.m_LoopScrollRect.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 4.95f);
				}
				break;
			case NKM_MISSION_TYPE.GROWTH:
			case NKM_MISSION_TYPE.GROWTH_UNIT:
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_LIST_ACHIEVE, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_MISSION_GROWTH, true);
				this.BuildGrowthMissionTempletByTab(_NKM_MISSION_TAB_ID);
				int indexPosition = 0;
				bool flag;
				NKMMissionTemplet templet = NKMMissionManager.GetGrowthMissionIngTempletByTab(this.m_NKM_MISSION_TAB_ID, out flag);
				if (this.m_lstGrowthMissionList.Contains(templet))
				{
					indexPosition = this.m_lstGrowthMissionList.FindIndex((NKMMissionTemplet x) => x == templet);
				}
				this.m_LoopScrollRectGrowth.TotalCount = this.m_lstGrowthMissionList.Count;
				this.m_LoopScrollRectGrowth.StopMovement();
				this.m_LoopScrollRectGrowth.SetIndexPosition(indexPosition);
				if (missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.GROWTH_UNIT)
				{
					this.m_LoopScrollRectGrowth.GetComponent<RectTransform>().offsetMin = new Vector2(420f, 159f);
				}
				else
				{
					this.m_LoopScrollRectGrowth.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 159f);
				}
				break;
			}
			}
			if (missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.REPEAT_DAILY || missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.REPEAT_WEEKLY)
			{
				List<NKMMissionTemplet> repeatMissionTempletByTab = this.GetRepeatMissionTempletByTab(missionTabTemplet.m_MissionType);
				List<float> list = this.CalcRepeatBoxPosition(repeatMissionTempletByTab);
				long num = 0L;
				Sprite orLoadAssetResource;
				if (missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.REPEAT_DAILY)
				{
					NKCUtil.SetLabelText(this.m_MISSION_REPEAT_TYPE_TITLE, NKMItemManager.GetItemMiscTempletByID(203).GetItemName());
					orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_mission_sprite", "AB_UI_NKM_UI_MISSION_REPEAT_TOP_BG_DAILY", false);
				}
				else
				{
					NKCUtil.SetLabelText(this.m_MISSION_REPEAT_TYPE_TITLE, NKMItemManager.GetItemMiscTempletByID(204).GetItemName());
					orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_mission_sprite", "AB_UI_NKM_UI_MISSION_REPEAT_TOP_BG_WEEKLY", false);
				}
				if (repeatMissionTempletByTab.Count > 0)
				{
					NKMMissionData missionData = myUserData.m_MissionData.GetMissionData(repeatMissionTempletByTab[0]);
					if (missionData != null && !NKMMissionManager.CheckCanReset(repeatMissionTempletByTab[0].m_ResetInterval, missionData))
					{
						num = missionData.times;
					}
				}
				NKCUtil.SetLabelText(this.m_MISSION_REPEAT_SCORE, num.ToString());
				NKCUtil.SetImageSprite(this.m_MISSION_REPEAT_BG, orLoadAssetResource, false);
				for (int i = 0; i < this.m_lstRepeatBox.Count; i++)
				{
					if (i < repeatMissionTempletByTab.Count)
					{
						NKCUtil.SetGameobjectActive(this.m_lstRepeatBox[i], true);
						this.m_lstRepeatBox[i].SetData(repeatMissionTempletByTab[i], new NKCUIMissionAchieveRepeatBox.OnButton(this.OnClickDailyBox));
						this.m_lstRepeatBox[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(list[i], this.m_lstRepeatBox[i].GetComponent<RectTransform>().anchoredPosition.y);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_lstRepeatBox[i], false);
					}
				}
				if (repeatMissionTempletByTab.Count > 0)
				{
					float num2 = (float)num / (float)repeatMissionTempletByTab[repeatMissionTempletByTab.Count - 1].m_Times;
					if (num2 <= 0f)
					{
						num2 = 0f;
					}
					if (num2 >= 1f)
					{
						num2 = 1f;
					}
					this.m_NKM_UI_MISSION_REPEAT_POINT_SLIDER.value = num2;
				}
				bool flag2 = myUserData.m_MissionData.CheckCompletableMission(myUserData, _NKM_MISSION_TAB_ID, false);
				NKCUtil.SetGameobjectActive(this.m_REPEAT_BOTTOM_BUTTON_ALL, flag2);
				NKCUtil.SetGameobjectActive(this.m_REPEAT_BOTTOM_BUTTON_ALL_DISABLE, !flag2);
				NKMTime.TimePeriod timePeriod = (missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.REPEAT_DAILY) ? NKMTime.TimePeriod.Day : NKMTime.TimePeriod.Week;
				NKCUtil.SetLabelText(this.m_MISSION_REPEAT_TIME_TEXT, NKCUtilString.GetResetTimeString(NKCSynchronizedTime.GetServerUTCTime(0.0), timePeriod, 3));
			}
			else if (missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.GROWTH_UNIT)
			{
				if (!string.IsNullOrEmpty(missionTabTemplet.m_MainUnitStrID))
				{
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(missionTabTemplet.m_MainUnitStrID);
					if (unitTempletBase != null)
					{
						NKCUtil.SetLabelText(this.m_lbGrowthBannerTitle, unitTempletBase.GetUnitName());
						this.m_unitIllust = this.AddSpineIllustration(unitTempletBase.m_UnitStrID);
					}
					if (this.m_unitIllust != null && this.m_NKCUINPCSpineIllust != null)
					{
						this.m_NKCUINPCSpineIllust.m_spUnitIllust = this.m_unitIllust.m_SpineIllustInstant_SkeletonGraphic;
						this.m_unitIllust.SetParent(this.m_NKCUINPCSpineIllust.transform, false);
						this.m_unitIllust.SetAnchoredPosition(this.DEFAULT_CHAR_POS);
					}
				}
			}
			else
			{
				this.m_NKM_UI_MISSION_ACHIEVE_POINT.text = myUserData.GetMissionAchievePoint().ToString();
				bool flag3 = myUserData.m_MissionData.CheckCompletableMission(myUserData, _NKM_MISSION_TAB_ID, false);
				NKCUIComStateButton csbtnCompleteAll = this.m_csbtnCompleteAll;
				if (csbtnCompleteAll != null)
				{
					csbtnCompleteAll.SetLock(!flag3, false);
				}
				NKCUIComStateButton nkm_UI_ALLCLEAR_MISSION_BOTTOM_BUTTON_ALL = this.m_NKM_UI_ALLCLEAR_MISSION_BOTTOM_BUTTON_ALL;
				if (nkm_UI_ALLCLEAR_MISSION_BOTTOM_BUTTON_ALL != null)
				{
					nkm_UI_ALLCLEAR_MISSION_BOTTOM_BUTTON_ALL.SetLock(!flag3, false);
				}
			}
			this.SetAllClearMissionBottomUI();
		}

		// Token: 0x060069DC RID: 27100 RVA: 0x00224DAC File Offset: 0x00222FAC
		private bool IsShowCompleteAllButton(NKM_MISSION_TYPE tabType)
		{
			switch (tabType)
			{
			case NKM_MISSION_TYPE.TUTORIAL:
			case NKM_MISSION_TYPE.REPEAT_DAILY:
			case NKM_MISSION_TYPE.REPEAT_WEEKLY:
			case NKM_MISSION_TYPE.GROWTH:
			case NKM_MISSION_TYPE.GROWTH_COMPLETE:
			case NKM_MISSION_TYPE.GROWTH_UNIT:
			case NKM_MISSION_TYPE.MAX:
				return false;
			default:
				return true;
			}
		}

		// Token: 0x060069DD RID: 27101 RVA: 0x00224E0C File Offset: 0x0022300C
		private NKCASUISpineIllust AddSpineIllustration(string prefabStrID)
		{
			return (NKCASUISpineIllust)NKCResourceUtility.OpenSpineIllustWithManualNaming(prefabStrID, false);
		}

		// Token: 0x060069DE RID: 27102 RVA: 0x00224E1C File Offset: 0x0022301C
		private void SetAllClearMissionBottomUI()
		{
			List<NKMMissionTemplet> missionTempletListByType = NKMMissionManager.GetMissionTempletListByType(this.m_NKM_MISSION_TAB_ID);
			NKMMissionTemplet lastCompletedMissionTemplet = NKMMissionManager.GetLastCompletedMissionTempletByTab(this.m_NKM_MISSION_TAB_ID);
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(this.m_NKM_MISSION_TAB_ID);
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (missionTabTemplet == null || nkmuserData == null)
			{
				return;
			}
			NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(missionTabTemplet.m_completeMissionID);
			int count = missionTempletListByType.Count;
			int num = 0;
			if (lastCompletedMissionTemplet != null)
			{
				num = missionTempletListByType.FindIndex((NKMMissionTemplet x) => x == lastCompletedMissionTemplet) + 1;
			}
			if (missionTemplet != null)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_BOTTOM_ALLCLEAR_MISSION, true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_ALLCLEAR_MISSION_BOTTOM_BUTTON_ALL, this.IsShowCompleteAllButton(missionTabTemplet.m_MissionType));
				NKCUtil.SetGameobjectActive(this.m_objCompleteAll, false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_BOTTOM_ALLCLEAR_MISSION, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_ALLCLEAR_MISSION_BOTTOM_BUTTON_ALL, false);
				NKCUtil.SetGameobjectActive(this.m_objCompleteAll, this.IsShowCompleteAllButton(missionTabTemplet.m_MissionType));
			}
			NKM_MISSION_TYPE missionType = missionTabTemplet.m_MissionType;
			bool flag = missionType - NKM_MISSION_TYPE.GROWTH <= 1 || missionType == NKM_MISSION_TYPE.GROWTH_UNIT;
			NKCUtil.SetGameobjectActive(this.m_GROWTH_BOTTOM_BUTTON_QUICK, flag);
			if (missionTemplet != null)
			{
				if (flag)
				{
					this.m_GROWTH_BOTTOM_SLIDER_TEXT.text = string.Format("{0} {1} / {2}", NKCStringTable.GetString(missionTemplet.m_MissionDesc, false), num, missionTempletListByType.Count);
					this.m_GROWTH_BOTTOM_SLIDER.value = (float)num / (float)missionTempletListByType.Count;
				}
				else
				{
					NKMMissionManager.MissionStateData missionStateData = NKMMissionManager.GetMissionStateData(missionTemplet);
					this.m_GROWTH_BOTTOM_SLIDER_TEXT.text = string.Format("{0} {1} / {2}", NKCStringTable.GetString(missionTemplet.m_MissionDesc, false), missionStateData.progressCount, missionTemplet.m_Times);
					this.m_GROWTH_BOTTOM_SLIDER.value = (float)missionStateData.progressCount / (float)missionTemplet.m_Times;
				}
				for (int i = 0; i < missionTemplet.m_MissionReward.Count; i++)
				{
					MissionReward missionReward = missionTemplet.m_MissionReward[i];
					this.m_lstRewardSlot[i].SetData(NKCUISlot.SlotData.MakeRewardTypeData(missionReward.reward_type, missionReward.reward_id, missionReward.reward_value, 0), true, null);
					this.m_lstRewardSlot[i].SetActive(true);
				}
				for (int j = missionTemplet.m_MissionReward.Count; j < this.m_lstRewardSlot.Count; j++)
				{
					this.m_lstRewardSlot[j].SetActive(false);
				}
				NKMMissionManager.MissionStateData missionStateData2 = NKMMissionManager.GetMissionStateData(missionTemplet.m_MissionID);
				NKCUIComStateButton growth_BOTTOM_BUTTON_RECEIVE = this.m_GROWTH_BOTTOM_BUTTON_RECEIVE;
				if (growth_BOTTOM_BUTTON_RECEIVE == null)
				{
					return;
				}
				growth_BOTTOM_BUTTON_RECEIVE.SetLock(!missionStateData2.IsMissionCanClear, false);
			}
		}

		// Token: 0x060069DF RID: 27103 RVA: 0x002250B2 File Offset: 0x002232B2
		public void RefreshMissionUI()
		{
			this.m_NKM_MISSION_TAB_ID = 2;
			this.Open(0);
		}

		// Token: 0x060069E0 RID: 27104 RVA: 0x002250C4 File Offset: 0x002232C4
		public void OnClickTab(int tabID, bool bSet)
		{
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(tabID);
			if (missionTabTemplet != null && NKMMissionManager.IsMissionTabExpired(missionTabTemplet, NKCScenManager.CurrentUserData()))
			{
				this.m_NKM_MISSION_TAB_ID = 2;
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_MISSION_EXPIRED, new NKCPopupOKCancel.OnButton(this.RefreshMissionUI), NKCUtilString.GET_STRING_CONFIRM);
				return;
			}
			if (bSet)
			{
				this.m_NKM_MISSION_TAB_ID = tabID;
				this.SetUIByTab(this.m_NKM_MISSION_TAB_ID);
			}
		}

		// Token: 0x060069E1 RID: 27105 RVA: 0x00225128 File Offset: 0x00223328
		public void OnClickAchievementBundleFinish()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			NKMUserMissionData missionData = NKCScenManager.GetScenManager().GetMyUserData().m_MissionData;
			if (missionData == null)
			{
				return;
			}
			if (!missionData.CheckCompletableMission(myUserData, this.m_NKM_MISSION_TAB_ID, false))
			{
				return;
			}
			NKMPacket_MISSION_COMPLETE_ALL_REQ nkmpacket_MISSION_COMPLETE_ALL_REQ = new NKMPacket_MISSION_COMPLETE_ALL_REQ();
			nkmpacket_MISSION_COMPLETE_ALL_REQ.tabId = this.m_NKM_MISSION_TAB_ID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_MISSION_COMPLETE_ALL_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060069E2 RID: 27106 RVA: 0x00225190 File Offset: 0x00223390
		public void OnClickGrowthComplete()
		{
			NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(NKMMissionManager.GetMissionTabTemplet(this.m_NKM_MISSION_TAB_ID).m_completeMissionID);
			if (missionTemplet == null)
			{
				return;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(missionTemplet.m_MissionTabId);
			if (missionTabTemplet == null)
			{
				return;
			}
			NKMMissionManager.MissionStateData missionStateData = NKMMissionManager.GetMissionStateData(missionTemplet);
			if (missionStateData.IsMissionCompleted)
			{
				return;
			}
			if (!missionStateData.IsMissionCanClear)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_MISSION_NEED_GROWTH_ALL_COMPLETE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			NKCPacketSender.Send_NKMPacket_MISSION_COMPLETE_REQ(missionTabTemplet.m_tabID, missionTemplet.m_GroupId, missionTemplet.m_MissionID);
		}

		// Token: 0x060069E3 RID: 27107 RVA: 0x0022520C File Offset: 0x0022340C
		public void OnClickGrowthMoveToCenter()
		{
			int indexPosition = this.m_LoopScrollRectGrowth.TotalCount - 1;
			bool flag;
			NKMMissionTemplet growthMissionIngTempletByTab = NKMMissionManager.GetGrowthMissionIngTempletByTab(this.m_NKM_MISSION_TAB_ID, out flag);
			for (int i = 0; i < this.m_lstGrowthMissionList.Count; i++)
			{
				if (this.m_lstGrowthMissionList[i] == growthMissionIngTempletByTab)
				{
					indexPosition = i;
					break;
				}
			}
			this.m_LoopScrollRectGrowth.StopMovement();
			this.m_LoopScrollRectGrowth.SetIndexPosition(indexPosition);
		}

		// Token: 0x060069E4 RID: 27108 RVA: 0x00225275 File Offset: 0x00223475
		public void OnClickMoveToDaily()
		{
			if (this.m_iDailyIndex >= 0)
			{
				this.m_LoopScrollRect.SetIndexPosition(this.m_iDailyIndex);
			}
		}

		// Token: 0x060069E5 RID: 27109 RVA: 0x00225291 File Offset: 0x00223491
		public void OnClickMoveToWeekly()
		{
			if (this.m_iWeeklyIndex >= 0)
			{
				this.m_LoopScrollRect.SetIndexPosition(this.m_iWeeklyIndex);
			}
		}

		// Token: 0x060069E6 RID: 27110 RVA: 0x002252AD File Offset: 0x002234AD
		public void OnClickMoveToMonthly()
		{
			if (this.m_iMonthlyIndex >= 0)
			{
				this.m_LoopScrollRect.SetIndexPosition(this.m_iMonthlyIndex);
			}
		}

		// Token: 0x060069E7 RID: 27111 RVA: 0x002252CC File Offset: 0x002234CC
		private void Update()
		{
			if (base.gameObject.activeSelf && this.m_fLastUIUpdateTime + 1f < Time.time)
			{
				this.m_fLastUIUpdateTime = Time.time;
				NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
				if (myUserData != null)
				{
					NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(this.m_NKM_MISSION_TAB_ID);
					if (missionTabTemplet == null)
					{
						return;
					}
					if (missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.REPEAT_DAILY)
					{
						NKCUtil.SetLabelText(this.m_MISSION_REPEAT_TIME_TEXT, NKCUtilString.GetResetTimeString(NKCSynchronizedTime.GetServerUTCTime(0.0), NKMTime.TimePeriod.Day, 3));
					}
					else if (missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.REPEAT_WEEKLY)
					{
						NKCUtil.SetLabelText(this.m_MISSION_REPEAT_TIME_TEXT, NKCUtilString.GetResetTimeString(NKCSynchronizedTime.GetServerUTCTime(0.0), NKMTime.TimePeriod.Week, 3));
					}
					if (myUserData.m_MissionData.HasAlreadyCompleteMission(missionTabTemplet.m_tabID))
					{
						bool flag = false;
						foreach (KeyValuePair<int, NKMMissionData> keyValuePair in myUserData.m_MissionData.GetAlreadyCompleteMission(missionTabTemplet.m_tabID))
						{
							NKMMissionData value = keyValuePair.Value;
							if (value != null)
							{
								NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(value.mission_id);
								if (missionTemplet != null && (missionTemplet.m_ResetInterval == NKM_MISSION_RESET_INTERVAL.DAILY || missionTemplet.m_ResetInterval == NKM_MISSION_RESET_INTERVAL.MONTHLY || missionTemplet.m_ResetInterval == NKM_MISSION_RESET_INTERVAL.WEEKLY) && NKMMissionManager.CheckCanReset(missionTemplet.m_ResetInterval, value))
								{
									flag = true;
								}
							}
						}
						if (flag)
						{
							this.SetUIByCurrTab();
						}
					}
				}
			}
		}

		// Token: 0x060069E8 RID: 27112 RVA: 0x0022543C File Offset: 0x0022363C
		private void OnDestroyImpl()
		{
		}

		// Token: 0x060069E9 RID: 27113 RVA: 0x0022543E File Offset: 0x0022363E
		private void OnDestroy()
		{
			this.OnDestroyImpl();
			NKCUIMissionAchievement.m_Instance = null;
		}

		// Token: 0x060069EA RID: 27114 RVA: 0x0022544C File Offset: 0x0022364C
		public override void CloseInternal()
		{
			foreach (KeyValuePair<int, NKCUIMissionAchieveTab> keyValuePair in this.m_dicMissionTab)
			{
				keyValuePair.Value.GetToggle().Select(false, false, false);
			}
			this.OnDestroyImpl();
			this.m_bRefreshReserved = false;
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060069EB RID: 27115 RVA: 0x002254C8 File Offset: 0x002236C8
		public void OnRecv(NKMPacket_MISSION_COMPLETE_ACK cNKMPacket_MISSION_COMPLETE_ACK)
		{
			this.m_bBlockRepeatBox = false;
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(cNKMPacket_MISSION_COMPLETE_ACK.missionID);
			if (missionTemplet == null)
			{
				return;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(missionTemplet.m_MissionTabId);
			if (missionTabTemplet == null)
			{
				return;
			}
			if (missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.GROWTH_COMPLETE)
			{
				this.SelectNextTab();
			}
			NKCUIResult.OnClose onClose = null;
			if (NKCGameEventManager.IsWaiting())
			{
				onClose = new NKCUIResult.OnClose(NKCGameEventManager.WaitFinished);
			}
			onClose = (NKCUIResult.OnClose)Delegate.Combine(onClose, new NKCUIResult.OnClose(this.SetUIByCurrTab));
			NKCUIResult.Instance.OpenRewardGain(myUserData.m_ArmyData, cNKMPacket_MISSION_COMPLETE_ACK.rewardDate, cNKMPacket_MISSION_COMPLETE_ACK.additionalReward, NKCUtilString.GET_STRING_RESULT_MISSION, "", onClose);
		}

		// Token: 0x060069EC RID: 27116 RVA: 0x0022556C File Offset: 0x0022376C
		public void OnRecv(NKMPacket_MISSION_COMPLETE_ALL_ACK cNKMPacket_MISSION_COMPLETE_ALL_ACK)
		{
			this.m_bBlockRepeatBox = false;
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKCUIResult.OnClose onClose = null;
			if (NKCGameEventManager.IsWaiting())
			{
				onClose = new NKCUIResult.OnClose(NKCGameEventManager.WaitFinished);
			}
			onClose = (NKCUIResult.OnClose)Delegate.Combine(onClose, new NKCUIResult.OnClose(this.SetUIByCurrTab));
			NKCUIResult.Instance.OpenRewardGain(myUserData.m_ArmyData, cNKMPacket_MISSION_COMPLETE_ALL_ACK.rewardDate, cNKMPacket_MISSION_COMPLETE_ALL_ACK.additionalReward, NKCUtilString.GET_STRING_RESULT_MISSION, "", onClose);
		}

		// Token: 0x060069ED RID: 27117 RVA: 0x002255E0 File Offset: 0x002237E0
		public void ReservedRefresh(NKMPacket_MISSION_UPDATE_NOT cNKMPacket_MISSION_UPDATE_NOT)
		{
			foreach (NKMMissionData nkmmissionData in cNKMPacket_MISSION_UPDATE_NOT.missionDataList)
			{
				NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(nkmmissionData.mission_id);
				int? num = (missionTemplet != null) ? new int?(missionTemplet.m_MissionTabId) : null;
				int nkm_MISSION_TAB_ID = this.m_NKM_MISSION_TAB_ID;
				if (num.GetValueOrDefault() == nkm_MISSION_TAB_ID & num != null)
				{
					this.m_bRefreshReserved = true;
				}
			}
		}

		// Token: 0x060069EE RID: 27118 RVA: 0x00225674 File Offset: 0x00223874
		private List<NKMMissionTemplet> GetRepeatMissionTempletByTab(NKM_MISSION_TYPE missionType)
		{
			if (missionType == NKM_MISSION_TYPE.REPEAT_DAILY)
			{
				return this.m_lstDailyRepeatMissionTemplet;
			}
			if (missionType != NKM_MISSION_TYPE.REPEAT_WEEKLY)
			{
				return new List<NKMMissionTemplet>();
			}
			return this.m_lstWeeklyRepeatMissionTemplet;
		}

		// Token: 0x060069EF RID: 27119 RVA: 0x00225694 File Offset: 0x00223894
		private List<float> CalcRepeatBoxPosition(List<NKMMissionTemplet> lstRepeatTemplet)
		{
			List<float> list = new List<float>();
			if (lstRepeatTemplet.Count == 0)
			{
				return list;
			}
			long times = lstRepeatTemplet[lstRepeatTemplet.Count - 1].m_Times;
			for (int i = 0; i < lstRepeatTemplet.Count; i++)
			{
				list.Add((float)lstRepeatTemplet[i].m_Times / (float)times * 1000f);
			}
			return list;
		}

		// Token: 0x060069F0 RID: 27120 RVA: 0x002256F3 File Offset: 0x002238F3
		public void CheckTutorial()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.Achieventment, true);
		}

		// Token: 0x060069F1 RID: 27121 RVA: 0x00225700 File Offset: 0x00223900
		public RectTransform GetRectTransformSlot(int missionID)
		{
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(this.m_NKM_MISSION_TAB_ID);
			if (missionTabTemplet == null)
			{
				return null;
			}
			switch (missionTabTemplet.m_MissionType)
			{
			case NKM_MISSION_TYPE.ACHIEVE:
			case NKM_MISSION_TYPE.REPEAT_DAILY:
			case NKM_MISSION_TYPE.REPEAT_WEEKLY:
			{
				int num = this.m_lstCurrentList.FindIndex((NKMMissionTemplet v) => v.Key == missionID);
				if (num < 0)
				{
					return null;
				}
				this.m_LoopScrollRectGrowth.StopMovement();
				this.m_LoopScrollRect.SetIndexPosition(num);
				NKCUIMissionAchieveSlot[] componentsInChildren = this.m_LoopScrollRect.content.GetComponentsInChildren<NKCUIMissionAchieveSlot>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					NKMMissionTemplet nkmmissionTemplet = componentsInChildren[i].GetNKMMissionTemplet();
					if (nkmmissionTemplet != null && nkmmissionTemplet.Key == missionID)
					{
						return componentsInChildren[i].GetComponent<RectTransform>();
					}
				}
				break;
			}
			case NKM_MISSION_TYPE.GROWTH:
			case NKM_MISSION_TYPE.GROWTH_UNIT:
			{
				int num2 = this.m_lstGrowthMissionList.FindIndex((NKMMissionTemplet v) => v.Key == missionID);
				if (num2 < 0)
				{
					return null;
				}
				this.m_LoopScrollRectGrowth.StopMovement();
				this.m_LoopScrollRectGrowth.SetIndexPosition(num2);
				NKCUIMissionAchieveSlotGrowth[] componentsInChildren2 = this.m_LoopScrollRectGrowth.content.GetComponentsInChildren<NKCUIMissionAchieveSlotGrowth>();
				for (int j = 0; j < componentsInChildren2.Length; j++)
				{
					NKMMissionTemplet nkmmissionTemplet2 = componentsInChildren2[j].GetNKMMissionTemplet();
					if (nkmmissionTemplet2 != null && nkmmissionTemplet2.Key == missionID)
					{
						return componentsInChildren2[j].GetComponent<RectTransform>();
					}
				}
				break;
			}
			}
			return null;
		}

		// Token: 0x04005581 RID: 21889
		public const string UI_ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_mission";

		// Token: 0x04005582 RID: 21890
		public const string UI_ASSET_NAME = "NKM_UI_MISSION";

		// Token: 0x04005583 RID: 21891
		private static NKCUIMissionAchievement m_Instance;

		// Token: 0x04005584 RID: 21892
		private const string REPEAT_MISSION_BG_ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_mission_sprite";

		// Token: 0x04005585 RID: 21893
		private const string DAILY_REPEAT_MISSION_BG_NAME = "AB_UI_NKM_UI_MISSION_REPEAT_TOP_BG_DAILY";

		// Token: 0x04005586 RID: 21894
		private const string WEEKLY_REPEAT_MISSION_BG_NAME = "AB_UI_NKM_UI_MISSION_REPEAT_TOP_BG_WEEKLY";

		// Token: 0x04005587 RID: 21895
		public Vector2 DEFAULT_CHAR_POS = new Vector2(-9.97f, -104.7f);

		// Token: 0x04005588 RID: 21896
		public LoopScrollRect m_LoopScrollRectTab;

		// Token: 0x04005589 RID: 21897
		public Transform m_trTabParent;

		// Token: 0x0400558A RID: 21898
		public NKCUIComToggleGroup m_tglGroup;

		// Token: 0x0400558B RID: 21899
		public GameObject m_NKM_UI_MISSION_LIST_ACHIEVE;

		// Token: 0x0400558C RID: 21900
		public GameObject m_NKM_UI_MISSION_LIST_ACHIEVE_POINT;

		// Token: 0x0400558D RID: 21901
		public GameObject m_NKM_UI_MISSION_LIST_REPEAT_POINT;

		// Token: 0x0400558E RID: 21902
		public LoopScrollRect m_LoopScrollRect;

		// Token: 0x0400558F RID: 21903
		public Text m_NKM_UI_MISSION_ACHIEVE_POINT;

		// Token: 0x04005590 RID: 21904
		[Header("반복미션")]
		public Text m_MISSION_REPEAT_TYPE_TITLE;

		// Token: 0x04005591 RID: 21905
		public Text m_MISSION_REPEAT_SCORE;

		// Token: 0x04005592 RID: 21906
		public Image m_MISSION_REPEAT_BG;

		// Token: 0x04005593 RID: 21907
		public Slider m_NKM_UI_MISSION_REPEAT_POINT_SLIDER;

		// Token: 0x04005594 RID: 21908
		public List<NKCUIMissionAchieveRepeatBox> m_lstRepeatBox = new List<NKCUIMissionAchieveRepeatBox>();

		// Token: 0x04005595 RID: 21909
		public Text m_MISSION_REPEAT_TIME_TEXT;

		// Token: 0x04005596 RID: 21910
		[Header("하단 올클리어 미션")]
		public GameObject m_NKM_UI_BOTTOM_ALLCLEAR_MISSION;

		// Token: 0x04005597 RID: 21911
		public NKCUIComStateButton m_GROWTH_BOTTOM_BUTTON_QUICK;

		// Token: 0x04005598 RID: 21912
		public NKCUIComStateButton m_GROWTH_BOTTOM_BUTTON_RECEIVE;

		// Token: 0x04005599 RID: 21913
		public Slider m_GROWTH_BOTTOM_SLIDER;

		// Token: 0x0400559A RID: 21914
		public Text m_GROWTH_BOTTOM_SLIDER_TEXT;

		// Token: 0x0400559B RID: 21915
		public NKCUIComStateButton m_NKM_UI_ALLCLEAR_MISSION_BOTTOM_BUTTON_ALL;

		// Token: 0x0400559C RID: 21916
		[Header("하단 전체받기만 있는 버전")]
		public GameObject m_objCompleteAll;

		// Token: 0x0400559D RID: 21917
		public NKCUIComStateButton m_csbtnCompleteAll;

		// Token: 0x0400559E RID: 21918
		[Header("반복미션 전체받기 버튼")]
		public NKCUIComStateButton m_REPEAT_BOTTOM_BUTTON_ALL;

		// Token: 0x0400559F RID: 21919
		public NKCUIComStateButton m_REPEAT_BOTTOM_BUTTON_ALL_DISABLE;

		// Token: 0x040055A0 RID: 21920
		[Header("성장미션")]
		public GameObject m_NKM_UI_MISSION_GROWTH;

		// Token: 0x040055A1 RID: 21921
		public GameObject m_NKM_UI_MISSION_GROWTH_BANNER;

		// Token: 0x040055A2 RID: 21922
		public Text m_lbGrowthBannerTitle;

		// Token: 0x040055A3 RID: 21923
		public NKCUINPCSpineIllust m_NKCUINPCSpineIllust;

		// Token: 0x040055A4 RID: 21924
		public LoopScrollRect m_LoopScrollRectGrowth;

		// Token: 0x040055A5 RID: 21925
		public List<NKCUISlot> m_lstRewardSlot = new List<NKCUISlot>();

		// Token: 0x040055A6 RID: 21926
		private bool m_bFirstOpen = true;

		// Token: 0x040055A7 RID: 21927
		private int m_NKM_MISSION_TAB_ID;

		// Token: 0x040055A8 RID: 21928
		private List<NKMMissionTabTemplet> m_lstMissionTabTemplet = new List<NKMMissionTabTemplet>();

		// Token: 0x040055A9 RID: 21929
		private Dictionary<int, List<NKMMissionTemplet>> m_dicNKMMissionTemplet = new Dictionary<int, List<NKMMissionTemplet>>();

		// Token: 0x040055AA RID: 21930
		private List<NKMMissionTemplet> m_lstDailyRepeatMissionTemplet = new List<NKMMissionTemplet>();

		// Token: 0x040055AB RID: 21931
		private List<NKMMissionTemplet> m_lstWeeklyRepeatMissionTemplet = new List<NKMMissionTemplet>();

		// Token: 0x040055AC RID: 21932
		private int m_iDailyIndex;

		// Token: 0x040055AD RID: 21933
		private int m_iWeeklyIndex;

		// Token: 0x040055AE RID: 21934
		private int m_iMonthlyIndex;

		// Token: 0x040055AF RID: 21935
		private float m_fLastUIUpdateTime;

		// Token: 0x040055B0 RID: 21936
		private NKCASUISpineIllust m_unitIllust;

		// Token: 0x040055B1 RID: 21937
		private bool m_bRefreshReserved;

		// Token: 0x040055B2 RID: 21938
		private bool m_bBlockRepeatBox;

		// Token: 0x040055B3 RID: 21939
		private const int DEFAULT_MISSION_TAB_ID = 2;

		// Token: 0x040055B4 RID: 21940
		private Dictionary<int, NKCUIMissionAchieveTab> m_dicMissionTab = new Dictionary<int, NKCUIMissionAchieveTab>();

		// Token: 0x040055B5 RID: 21941
		private List<NKMMissionTemplet> m_lstCurrentList = new List<NKMMissionTemplet>();

		// Token: 0x040055B6 RID: 21942
		private List<NKMMissionTemplet> m_lstGrowthMissionList = new List<NKMMissionTemplet>();
	}
}
