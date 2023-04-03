using System;
using System.Collections.Generic;
using ClientPacket.User;
using NKC.UI.Result;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009BE RID: 2494
	public class NKCUIMissionGuide : NKCUIBase
	{
		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x060069F3 RID: 27123 RVA: 0x00225904 File Offset: 0x00223B04
		public static NKCUIMissionGuide Instance
		{
			get
			{
				if (NKCUIMissionGuide.m_Instance == null)
				{
					NKCUIMissionGuide.m_Instance = NKCUIManager.OpenNewInstance<NKCUIMissionGuide>("ab_ui_nkm_ui_mission_guide", "NKM_UI_MISSION_GUIDE", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIMissionGuide.CleanupInstance)).GetInstance<NKCUIMissionGuide>();
					NKCUIMissionGuide.m_Instance.InitUI();
				}
				return NKCUIMissionGuide.m_Instance;
			}
		}

		// Token: 0x060069F4 RID: 27124 RVA: 0x00225953 File Offset: 0x00223B53
		private static void CleanupInstance()
		{
			NKCUIMissionGuide.m_Instance = null;
		}

		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x060069F5 RID: 27125 RVA: 0x0022595B File Offset: 0x00223B5B
		public static bool HasInstance
		{
			get
			{
				return NKCUIMissionGuide.m_Instance != null;
			}
		}

		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x060069F6 RID: 27126 RVA: 0x00225968 File Offset: 0x00223B68
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIMissionGuide.m_Instance != null && NKCUIMissionGuide.m_Instance.IsOpen;
			}
		}

		// Token: 0x060069F7 RID: 27127 RVA: 0x00225983 File Offset: 0x00223B83
		public static void CheckInstanceAndClose()
		{
			if (NKCUIMissionGuide.m_Instance != null && NKCUIMissionGuide.m_Instance.IsOpen)
			{
				NKCUIMissionGuide.m_Instance.Close();
			}
		}

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x060069F8 RID: 27128 RVA: 0x002259A8 File Offset: 0x00223BA8
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_MISSION;
			}
		}

		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x060069F9 RID: 27129 RVA: 0x002259AF File Offset: 0x00223BAF
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x060069FA RID: 27130 RVA: 0x002259B2 File Offset: 0x00223BB2
		public override void UnHide()
		{
			base.UnHide();
			if (this.m_bRefreshReserved)
			{
				this.SetUIByCurrTab();
				this.m_bRefreshReserved = false;
			}
		}

		// Token: 0x060069FB RID: 27131 RVA: 0x002259D0 File Offset: 0x00223BD0
		public override void CloseInternal()
		{
			foreach (KeyValuePair<int, NKCUIMissionAchieveTab> keyValuePair in this.m_dicMissionTab)
			{
				keyValuePair.Value.GetToggle().Select(false, false, false);
			}
			this.m_bRefreshReserved = false;
			this.m_bFirstOpen = true;
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060069FC RID: 27132 RVA: 0x00225A4C File Offset: 0x00223C4C
		public void InitUI()
		{
			NKCUtil.SetBindFunction(this.m_csbtnClose, new UnityAction(base.Close));
			NKCUtil.SetBindFunction(this.m_csbtnQuick, new UnityAction(this.OnClickGrowthMoveToCenter));
			NKCUtil.SetBindFunction(this.m_csbtnReceive, new UnityAction(this.OnClickReceive));
			if (null != this.m_lvsrTab)
			{
				this.m_lvsrTab.dOnGetObject += this.GetMissionTab;
				this.m_lvsrTab.dOnReturnObject += this.ReturnMissionTab;
				this.m_lvsrTab.dOnProvideData += this.ProvideTabData;
				this.m_lvsrTab.ContentConstraintCount = 1;
				NKCUtil.SetScrollHotKey(this.m_lvsrTab, null);
			}
			if (null != this.m_lvsrMission)
			{
				this.m_lvsrMission.dOnGetObject += this.GetGrowthMissionSlot;
				this.m_lvsrMission.dOnReturnObject += this.ReturnGrowthMissionSlot;
				this.m_lvsrMission.dOnProvideData += this.ProvideGrowthData;
				this.m_lvsrMission.ContentConstraintCount = 1;
				NKCUtil.SetScrollHotKey(this.m_lvsrMission, null);
			}
			foreach (NKCUISlot nkcuislot in this.m_lstReceiveSlots)
			{
				if (nkcuislot != null)
				{
					nkcuislot.Init();
				}
			}
		}

		// Token: 0x060069FD RID: 27133 RVA: 0x00225B98 File Offset: 0x00223D98
		public void Open(int missionTabID = 0)
		{
			this.m_bRefreshReserved = false;
			this.m_NKM_MISSION_TAB_ID = missionTabID;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (this.m_bFirstOpen)
			{
				this.ResetMissionTabData();
				this.m_lvsrTab.PrepareCells(0);
				this.m_lvsrMission.PrepareCells(0);
				this.m_bFirstOpen = false;
			}
			this.ResetTabUI();
			this.OnClickTab(this.m_NKM_MISSION_TAB_ID, true);
			this.SetCompletableMissionAlarm();
			base.UIOpened(true);
		}

		// Token: 0x060069FE RID: 27134 RVA: 0x00225C0C File Offset: 0x00223E0C
		private void ResetMissionTabData()
		{
			this.m_lstMissionTabTemplet.Clear();
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			foreach (NKMMissionTabTemplet nkmmissionTabTemplet in NKMMissionManager.DicMissionTab.Values)
			{
				if (nkmmissionTabTemplet != null && nkmmissionTabTemplet.EnableByTag && nkmmissionTabTemplet.m_MissionType == NKM_MISSION_TYPE.COMBINE_GUIDE_MISSION)
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
						this.m_lstMissionTabTemplet.Add(nkmmissionTabTemplet);
					}
				}
			}
			this.m_lstMissionTabTemplet.Sort(new Comparison<NKMMissionTabTemplet>(this.CompTabSort));
			NKMMissionTabTemplet nkmmissionTabTemplet2 = this.m_lstMissionTabTemplet.Find((NKMMissionTabTemplet e) => e.m_tabID == this.m_NKM_MISSION_TAB_ID);
			if (nkmmissionTabTemplet2 == null)
			{
				foreach (NKMMissionTabTemplet nkmmissionTabTemplet3 in this.m_lstMissionTabTemplet)
				{
					NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(nkmmissionTabTemplet3.m_completeMissionID);
					if (missionTemplet != null && NKMMissionManager.GetMissionStateData(missionTemplet.m_MissionID).IsMissionCanClear)
					{
						this.m_NKM_MISSION_TAB_ID = nkmmissionTabTemplet3.m_tabID;
						return;
					}
				}
				foreach (NKMMissionTabTemplet nkmmissionTabTemplet4 in this.m_lstMissionTabTemplet)
				{
					NKMMissionTemplet missionTemplet2 = NKMMissionManager.GetMissionTemplet(nkmmissionTabTemplet4.m_completeMissionID);
					if (missionTemplet2 != null && !NKMMissionManager.GetMissionStateData(missionTemplet2.m_MissionID).IsMissionCompleted)
					{
						this.m_NKM_MISSION_TAB_ID = nkmmissionTabTemplet4.m_tabID;
						return;
					}
				}
				this.m_NKM_MISSION_TAB_ID = this.m_lstMissionTabTemplet[0].m_tabID;
				return;
			}
		}

		// Token: 0x060069FF RID: 27135 RVA: 0x00225E4C File Offset: 0x0022404C
		private void ResetTabUI()
		{
			if (this.m_lstMissionTabTemplet.Count <= 0)
			{
				Debug.LogError("NKCUIMissionGuide::ResetMissionTabData - tab data is zero");
				return;
			}
			this.m_lvsrTab.TotalCount = this.m_lstMissionTabTemplet.Count;
			this.m_lvsrTab.RefreshCells(false);
			this.m_dicMissionTab[this.m_NKM_MISSION_TAB_ID].GetToggle().Select(true, false, false);
		}

		// Token: 0x06006A00 RID: 27136 RVA: 0x00225EB3 File Offset: 0x002240B3
		private int CompTabSort(NKMMissionTabTemplet lItem, NKMMissionTabTemplet rItem)
		{
			return lItem.m_tabID.CompareTo(rItem.m_tabID);
		}

		// Token: 0x06006A01 RID: 27137 RVA: 0x00225EC8 File Offset: 0x002240C8
		private void OnClickGrowthMoveToCenter()
		{
			int indexPosition = this.m_lvsrMission.TotalCount - 1;
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
			this.m_lvsrMission.StopMovement();
			this.m_lvsrMission.SetIndexPosition(indexPosition);
		}

		// Token: 0x06006A02 RID: 27138 RVA: 0x00225F34 File Offset: 0x00224134
		private void OnClickReceive()
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

		// Token: 0x06006A03 RID: 27139 RVA: 0x00225FB0 File Offset: 0x002241B0
		public void SetUIByCurrTab()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				this.m_bRefreshReserved = true;
				return;
			}
			this.ResetTabUI();
			this.SetUIByTab(this.m_NKM_MISSION_TAB_ID);
			this.SetCompletableMissionAlarm();
		}

		// Token: 0x06006A04 RID: 27140 RVA: 0x00225FE0 File Offset: 0x002241E0
		private void SetUIByTab(int _NKM_MISSION_TAB_ID)
		{
			if (NKCScenManager.GetScenManager().GetMyUserData() == null)
			{
				return;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(_NKM_MISSION_TAB_ID);
			if (missionTabTemplet == null)
			{
				return;
			}
			if (!string.IsNullOrEmpty(missionTabTemplet.m_MissionBannerImage))
			{
				NKCUtil.SetImageSprite(this.m_BannerImg, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("UI_MISSION_GUIDE_BANNER", missionTabTemplet.m_MissionBannerImage, false), false);
			}
			if (!this.m_dicNKMMissionTemplet.ContainsKey(_NKM_MISSION_TAB_ID))
			{
				this.m_dicNKMMissionTemplet.Add(_NKM_MISSION_TAB_ID, NKMMissionManager.GetMissionTempletListByType(_NKM_MISSION_TAB_ID));
			}
			NKM_MISSION_TYPE missionType = missionTabTemplet.m_MissionType;
			switch (missionType)
			{
			case NKM_MISSION_TYPE.ACHIEVE:
			case NKM_MISSION_TYPE.REPEAT_DAILY:
			case NKM_MISSION_TYPE.REPEAT_WEEKLY:
			case NKM_MISSION_TYPE.EVENT:
			case NKM_MISSION_TYPE.GROWTH_COMPLETE:
			case NKM_MISSION_TYPE.EMBLEM:
				break;
			case NKM_MISSION_TYPE.GROWTH:
			case NKM_MISSION_TYPE.GROWTH_UNIT:
				goto IL_9F;
			default:
				if (missionType == NKM_MISSION_TYPE.COMBINE_GUIDE_MISSION)
				{
					goto IL_9F;
				}
				break;
			}
			this.BuildMissionTempletListByTab(_NKM_MISSION_TAB_ID);
			goto IL_119;
			IL_9F:
			this.BuildGrowthMissionTempletByTab(_NKM_MISSION_TAB_ID);
			int indexPosition = 0;
			bool flag;
			NKMMissionTemplet templet = NKMMissionManager.GetGrowthMissionIngTempletByTab(this.m_NKM_MISSION_TAB_ID, out flag);
			if (this.m_lstGrowthMissionList.Contains(templet))
			{
				indexPosition = this.m_lstGrowthMissionList.FindIndex((NKMMissionTemplet x) => x == templet);
			}
			this.m_lvsrMission.TotalCount = this.m_lstGrowthMissionList.Count;
			this.m_lvsrMission.StopMovement();
			this.m_lvsrMission.SetIndexPosition(indexPosition);
			IL_119:
			this.SetAllClearMissionBottomUI();
		}

		// Token: 0x06006A05 RID: 27141 RVA: 0x0022610C File Offset: 0x0022430C
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

		// Token: 0x06006A06 RID: 27142 RVA: 0x002261A8 File Offset: 0x002243A8
		private void BuildMissionTempletListByTab(int tabID)
		{
			this.m_lstCurrentList.Clear();
			List<NKMMissionTemplet> list = this.m_dicNKMMissionTemplet[tabID];
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			if (NKMMissionManager.GetMissionTabTemplet(tabID) == null)
			{
				return;
			}
			for (int i = 0; i < list.Count; i++)
			{
				NKMMissionTemplet nkmmissionTemplet = list[i];
				if (nkmmissionTemplet != null)
				{
					if (nkmmissionTemplet.m_MissionRequire != 0)
					{
						NKMMissionData nkmmissionData = myUserData.m_MissionData.GetMissionData(nkmmissionTemplet);
						if (nkmmissionData == null)
						{
							goto IL_D4;
						}
						if (nkmmissionData.mission_id == nkmmissionTemplet.m_MissionID)
						{
							this.m_lstCurrentList.Add(nkmmissionTemplet);
							goto IL_D4;
						}
						nkmmissionData = myUserData.m_MissionData.GetMissionDataByMissionId(nkmmissionTemplet.m_MissionRequire);
						if (nkmmissionData == null)
						{
							goto IL_D4;
						}
						if (nkmmissionData.isComplete && nkmmissionData.mission_id == nkmmissionTemplet.m_MissionRequire)
						{
							this.m_lstCurrentList.Add(nkmmissionTemplet);
							goto IL_D4;
						}
						if (nkmmissionData.mission_id <= nkmmissionTemplet.m_MissionRequire)
						{
							goto IL_D4;
						}
					}
					this.m_lstCurrentList.Add(nkmmissionTemplet);
				}
				IL_D4:;
			}
			this.m_lstCurrentList.Sort(new Comparison<NKMMissionTemplet>(NKMMissionManager.Comparer));
		}

		// Token: 0x06006A07 RID: 27143 RVA: 0x002262B0 File Offset: 0x002244B0
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
			NKM_MISSION_TYPE missionType = missionTabTemplet.m_MissionType;
			bool flag = missionType - NKM_MISSION_TYPE.GROWTH <= 1 || missionType == NKM_MISSION_TYPE.GROWTH_UNIT || missionType == NKM_MISSION_TYPE.COMBINE_GUIDE_MISSION;
			NKCUtil.SetGameobjectActive(this.m_objBottom, flag);
			NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(missionTabTemplet.m_completeMissionID);
			if (missionTemplet != null)
			{
				int count = missionTempletListByType.Count;
				int num = 0;
				if (lastCompletedMissionTemplet != null)
				{
					num = missionTempletListByType.FindIndex((NKMMissionTemplet x) => x == lastCompletedMissionTemplet) + 1;
				}
				if (flag)
				{
					this.m_lbReceiveGauge.text = string.Format("{0} {1} / {2}", NKCStringTable.GetString(missionTemplet.m_MissionDesc, false), num, missionTempletListByType.Count);
					this.m_imgReceiveGauge.value = (float)num / (float)missionTempletListByType.Count;
				}
				else
				{
					NKMMissionManager.MissionStateData missionStateData = NKMMissionManager.GetMissionStateData(missionTemplet);
					this.m_lbReceiveGauge.text = string.Format("{0} {1} / {2}", NKCStringTable.GetString(missionTemplet.m_MissionDesc, false), missionStateData.progressCount, missionTemplet.m_Times);
					this.m_imgReceiveGauge.value = (float)missionStateData.progressCount / (float)missionTemplet.m_Times;
				}
				for (int i = 0; i < missionTemplet.m_MissionReward.Count; i++)
				{
					MissionReward missionReward = missionTemplet.m_MissionReward[i];
					this.m_lstReceiveSlots[i].SetData(NKCUISlot.SlotData.MakeRewardTypeData(missionReward.reward_type, missionReward.reward_id, missionReward.reward_value, 0), true, null);
					this.m_lstReceiveSlots[i].SetActive(true);
				}
				for (int j = missionTemplet.m_MissionReward.Count; j < this.m_lstReceiveSlots.Length; j++)
				{
					this.m_lstReceiveSlots[j].SetActive(false);
				}
				NKMMissionManager.MissionStateData missionStateData2 = NKMMissionManager.GetMissionStateData(missionTemplet.m_MissionID);
				NKCUIComStateButton csbtnReceive = this.m_csbtnReceive;
				if (csbtnReceive == null)
				{
					return;
				}
				csbtnReceive.SetLock(!missionStateData2.IsMissionCanClear, false);
			}
		}

		// Token: 0x06006A08 RID: 27144 RVA: 0x002264D8 File Offset: 0x002246D8
		public RectTransform GetGrowthMissionSlot(int index)
		{
			NKCUIMissionAchieveSlotGrowth newInstance = NKCUIMissionAchieveSlotGrowth.GetNewInstance(this.m_lvsrMission.content, "ab_ui_nkm_ui_mission_guide", "NKM_UI_MISSION_GUIDE_SLOT", new NKCUIMissionAchieveSlotGrowth.OnClickMASlot(this.OnClickMove), new NKCUIMissionAchieveSlotGrowth.OnClickMASlot(this.OnClickComplete));
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x06006A09 RID: 27145 RVA: 0x0022652C File Offset: 0x0022472C
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

		// Token: 0x06006A0A RID: 27146 RVA: 0x00226568 File Offset: 0x00224768
		public void ProvideGrowthData(Transform tr, int index)
		{
			NKCUIMissionAchieveSlotGrowth component = tr.GetComponent<NKCUIMissionAchieveSlotGrowth>();
			if (component != null)
			{
				NKMMissionTemplet data = this.m_lstGrowthMissionList[index];
				component.SetData(data);
			}
		}

		// Token: 0x06006A0B RID: 27147 RVA: 0x00226599 File Offset: 0x00224799
		private void BuildGrowthMissionTempletByTab(int tabID)
		{
			this.m_lstGrowthMissionList = this.m_dicNKMMissionTemplet[tabID];
			this.m_lstGrowthMissionList.Sort(new Comparison<NKMMissionTemplet>(this.CompareByID));
		}

		// Token: 0x06006A0C RID: 27148 RVA: 0x002265C4 File Offset: 0x002247C4
		private int CompareByID(NKMMissionTemplet x, NKMMissionTemplet y)
		{
			return x.m_MissionID.CompareTo(y.m_MissionID);
		}

		// Token: 0x06006A0D RID: 27149 RVA: 0x002265D8 File Offset: 0x002247D8
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

		// Token: 0x06006A0E RID: 27150 RVA: 0x0022660C File Offset: 0x0022480C
		public void OnClickComplete(NKCUIMissionAchieveSlotGrowth cNKCUIMissionAchieveSlot)
		{
			if (cNKCUIMissionAchieveSlot == null)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_MISSION_COMPLETE_REQ(cNKCUIMissionAchieveSlot.GetNKMMissionTemplet().m_MissionTabId, cNKCUIMissionAchieveSlot.GetNKMMissionTemplet().m_GroupId, cNKCUIMissionAchieveSlot.GetNKMMissionTemplet().m_MissionID);
		}

		// Token: 0x06006A0F RID: 27151 RVA: 0x00226640 File Offset: 0x00224840
		public RectTransform GetMissionTab(int index)
		{
			NKCUIMissionAchieveTab newInstance = NKCUIMissionAchieveTab.GetNewInstance(this.m_lvsrTab.content.transform, "ab_ui_nkm_ui_mission_guide", "NKM_UI_MISSION_GUIDE_TAB");
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x06006A10 RID: 27152 RVA: 0x00226680 File Offset: 0x00224880
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

		// Token: 0x06006A11 RID: 27153 RVA: 0x002266BC File Offset: 0x002248BC
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
			component.SetData(this.m_lstMissionTabTemplet[index], this.m_ctgTab, new NKCUIMissionAchieveTab.OnClickTab(this.OnClickTab));
			component.SetCompleteObject(component.GetCompleted());
			component.SetLockObject(true);
			component.gameObject.name = this.m_lstMissionTabTemplet[index].m_tabID.ToString("D3");
			if (!this.m_dicMissionTab.ContainsKey(this.m_lstMissionTabTemplet[index].m_tabID))
			{
				this.m_dicMissionTab.Add(this.m_lstMissionTabTemplet[index].m_tabID, component);
				return;
			}
			this.m_dicMissionTab[this.m_lstMissionTabTemplet[index].m_tabID] = component;
		}

		// Token: 0x06006A12 RID: 27154 RVA: 0x002267C3 File Offset: 0x002249C3
		public void OnClickTab(int tabID, bool bSet)
		{
			if (bSet)
			{
				this.m_NKM_MISSION_TAB_ID = tabID;
				this.SetUIByTab(this.m_NKM_MISSION_TAB_ID);
			}
		}

		// Token: 0x06006A13 RID: 27155 RVA: 0x002267DC File Offset: 0x002249DC
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

		// Token: 0x06006A14 RID: 27156 RVA: 0x00226870 File Offset: 0x00224A70
		public void OnRecv(NKMPacket_MISSION_COMPLETE_ALL_ACK cNKMPacket_MISSION_COMPLETE_ALL_ACK)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKCUIResult.OnClose onClose = null;
			if (NKCGameEventManager.IsWaiting())
			{
				onClose = new NKCUIResult.OnClose(NKCGameEventManager.WaitFinished);
			}
			onClose = (NKCUIResult.OnClose)Delegate.Combine(onClose, new NKCUIResult.OnClose(this.SetUIByCurrTab));
			NKCUIResult.Instance.OpenRewardGain(myUserData.m_ArmyData, cNKMPacket_MISSION_COMPLETE_ALL_ACK.rewardDate, cNKMPacket_MISSION_COMPLETE_ALL_ACK.additionalReward, NKCUtilString.GET_STRING_RESULT_MISSION, "", onClose);
		}

		// Token: 0x06006A15 RID: 27157 RVA: 0x002268E0 File Offset: 0x00224AE0
		public void OnRecv(NKMPacket_MISSION_COMPLETE_ACK cNKMPacket_MISSION_COMPLETE_ACK)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(cNKMPacket_MISSION_COMPLETE_ACK.missionID);
			if (missionTemplet == null)
			{
				return;
			}
			if (NKMMissionManager.GetMissionTabTemplet(missionTemplet.m_MissionTabId) == null)
			{
				return;
			}
			NKCUIResult.OnClose onClose = null;
			if (NKCGameEventManager.IsWaiting())
			{
				onClose = new NKCUIResult.OnClose(NKCGameEventManager.WaitFinished);
			}
			this.ResetMissionTabData();
			onClose = (NKCUIResult.OnClose)Delegate.Combine(onClose, new NKCUIResult.OnClose(this.SetUIByCurrTab));
			NKCUIResult.Instance.OpenRewardGain(myUserData.m_ArmyData, cNKMPacket_MISSION_COMPLETE_ACK.rewardDate, cNKMPacket_MISSION_COMPLETE_ACK.additionalReward, NKCUtilString.GET_STRING_RESULT_MISSION, "", onClose);
		}

		// Token: 0x040055B7 RID: 21943
		public const string UI_ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_mission_guide";

		// Token: 0x040055B8 RID: 21944
		public const string UI_ASSET_NAME = "NKM_UI_MISSION_GUIDE";

		// Token: 0x040055B9 RID: 21945
		private static NKCUIMissionGuide m_Instance;

		// Token: 0x040055BA RID: 21946
		public Image m_BannerImg;

		// Token: 0x040055BB RID: 21947
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x040055BC RID: 21948
		public NKCUIComToggleGroup m_ctgTab;

		// Token: 0x040055BD RID: 21949
		public LoopVerticalScrollRect m_lvsrTab;

		// Token: 0x040055BE RID: 21950
		public LoopVerticalScrollRect m_lvsrMission;

		// Token: 0x040055BF RID: 21951
		public GameObject m_objReceive;

		// Token: 0x040055C0 RID: 21952
		public NKCUISlot[] m_lstReceiveSlots;

		// Token: 0x040055C1 RID: 21953
		public NKCUIComStateButton m_csbtnQuick;

		// Token: 0x040055C2 RID: 21954
		public NKCUIComStateButton m_csbtnReceive;

		// Token: 0x040055C3 RID: 21955
		[Header("All Clear")]
		public GameObject m_objBottom;

		// Token: 0x040055C4 RID: 21956
		public Slider m_imgReceiveGauge;

		// Token: 0x040055C5 RID: 21957
		public Text m_lbReceiveGauge;

		// Token: 0x040055C6 RID: 21958
		private bool m_bRefreshReserved;

		// Token: 0x040055C7 RID: 21959
		private bool m_bFirstOpen = true;

		// Token: 0x040055C8 RID: 21960
		private int m_NKM_MISSION_TAB_ID;

		// Token: 0x040055C9 RID: 21961
		private List<NKMMissionTabTemplet> m_lstMissionTabTemplet = new List<NKMMissionTabTemplet>();

		// Token: 0x040055CA RID: 21962
		private Dictionary<int, List<NKMMissionTemplet>> m_dicNKMMissionTemplet = new Dictionary<int, List<NKMMissionTemplet>>();

		// Token: 0x040055CB RID: 21963
		private List<NKMMissionTemplet> m_lstGrowthMissionList = new List<NKMMissionTemplet>();

		// Token: 0x040055CC RID: 21964
		private Dictionary<int, NKCUIMissionAchieveTab> m_dicMissionTab = new Dictionary<int, NKCUIMissionAchieveTab>();

		// Token: 0x040055CD RID: 21965
		private List<NKMMissionTemplet> m_lstCurrentList = new List<NKMMissionTemplet>();
	}
}
