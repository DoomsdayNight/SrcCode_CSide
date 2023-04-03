using System;
using System.Collections.Generic;
using System.Text;
using ClientPacket.Common;
using ClientPacket.Event;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKC.UI.Result;
using NKM;
using NKM.EventPass;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200098C RID: 2444
	public class NKCUIEventPass : NKCUIBase
	{
		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x060064AE RID: 25774 RVA: 0x001FF548 File Offset: 0x001FD748
		public static NKCUIEventPass Instance
		{
			get
			{
				if (NKCUIEventPass.m_Instance == null)
				{
					NKCUIEventPass.m_Instance = NKCUIManager.OpenNewInstance<NKCUIEventPass>("AB_UI_NKM_UI_EVENT_PASS", "NKM_UI_EVENT_PASS", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIEventPass.CleanUpInstance)).GetInstance<NKCUIEventPass>();
					NKCUIEventPass.m_Instance.InitUI();
				}
				return NKCUIEventPass.m_Instance;
			}
		}

		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x060064AF RID: 25775 RVA: 0x001FF597 File Offset: 0x001FD797
		public static bool HasInstance
		{
			get
			{
				return NKCUIEventPass.m_Instance != null;
			}
		}

		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x060064B0 RID: 25776 RVA: 0x001FF5A4 File Offset: 0x001FD7A4
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIEventPass.m_Instance != null && NKCUIEventPass.m_Instance.IsOpen;
			}
		}

		// Token: 0x060064B1 RID: 25777 RVA: 0x001FF5BF File Offset: 0x001FD7BF
		public static void CheckInstanceAndClose()
		{
			if (NKCUIEventPass.m_Instance != null && NKCUIEventPass.m_Instance.IsOpen)
			{
				NKCUIEventPass.m_Instance.Close();
			}
		}

		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x060064B2 RID: 25778 RVA: 0x001FF5E4 File Offset: 0x001FD7E4
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_EVENTPASS_EVENT_PASS_MENU_TITLE;
			}
		}

		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x060064B3 RID: 25779 RVA: 0x001FF5EB File Offset: 0x001FD7EB
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x060064B4 RID: 25780 RVA: 0x001FF5EE File Offset: 0x001FD7EE
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return this.m_listUpsideMenuResource;
			}
		}

		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x060064B5 RID: 25781 RVA: 0x001FF5F6 File Offset: 0x001FD7F6
		// (set) Token: 0x060064B6 RID: 25782 RVA: 0x001FF5FD File Offset: 0x001FD7FD
		public static bool OpenUIStandby
		{
			get
			{
				return NKCUIEventPass.m_bOpenUIStandby;
			}
			set
			{
				NKCUIEventPass.m_bOpenUIStandby = value;
			}
		}

		// Token: 0x170011AD RID: 4525
		// (set) Token: 0x060064B7 RID: 25783 RVA: 0x001FF605 File Offset: 0x001FD805
		public static NKCEventPassDataManager EventPassDataManager
		{
			set
			{
				NKCUIEventPass.m_NKCEventPassDataManager = value;
			}
		}

		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x060064B8 RID: 25784 RVA: 0x001FF60D File Offset: 0x001FD80D
		public int UserPassLevel
		{
			get
			{
				return this.m_iUserPassLevel;
			}
		}

		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x060064BA RID: 25786 RVA: 0x001FF62F File Offset: 0x001FD82F
		// (set) Token: 0x060064B9 RID: 25785 RVA: 0x001FF615 File Offset: 0x001FD815
		public static bool RewardRedDot
		{
			get
			{
				return NKCUIEventPass.m_bRewardDot;
			}
			set
			{
				NKCUIEventPass.m_bRewardDot = value;
				if (NKCUIEventPass.IsInstanceOpen)
				{
					NKCUIEventPass.m_Instance.SetRewardRedDot(value);
				}
			}
		}

		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x060064BC RID: 25788 RVA: 0x001FF650 File Offset: 0x001FD850
		// (set) Token: 0x060064BB RID: 25787 RVA: 0x001FF636 File Offset: 0x001FD836
		public static bool DailyMissionRedDot
		{
			get
			{
				return NKCUIEventPass.m_bDailyMissionDot;
			}
			set
			{
				NKCUIEventPass.m_bDailyMissionDot = value;
				if (NKCUIEventPass.IsInstanceOpen)
				{
					NKCUIEventPass.m_Instance.SetDailyMissionRedDot(value);
				}
			}
		}

		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x060064BE RID: 25790 RVA: 0x001FF671 File Offset: 0x001FD871
		// (set) Token: 0x060064BD RID: 25789 RVA: 0x001FF657 File Offset: 0x001FD857
		public static bool WeeklyMissionRedDot
		{
			get
			{
				return NKCUIEventPass.m_bWeeklyMissionDot;
			}
			set
			{
				NKCUIEventPass.m_bWeeklyMissionDot = value;
				if (NKCUIEventPass.IsInstanceOpen)
				{
					NKCUIEventPass.m_Instance.SetWeeklyMissionRedDot(value);
				}
			}
		}

		// Token: 0x060064BF RID: 25791 RVA: 0x001FF678 File Offset: 0x001FD878
		public void InitUI()
		{
			this.m_LoopRewardScrollRect.dOnGetObject += this.GetRewardSlot;
			this.m_LoopRewardScrollRect.dOnReturnObject += this.ReturnRewardSlot;
			this.m_LoopRewardScrollRect.dOnProvideData += this.ProvideRewardData;
			this.m_LoopRewardScrollRect.ContentConstraintCount = 1;
			NKCUtil.SetScrollHotKey(this.m_LoopRewardScrollRect, null);
			this.m_LoopMissionScrollRect.dOnGetObject += this.GetMissionSlot;
			this.m_LoopMissionScrollRect.dOnReturnObject += this.ReturnMissionSlot;
			this.m_LoopMissionScrollRect.dOnProvideData += this.ProvideMissionData;
			this.m_LoopMissionScrollRect.ContentConstraintCount = 1;
			NKCUtil.SetScrollHotKey(this.m_LoopMissionScrollRect, null);
			NKCUtil.SetGameobjectActive(this.m_objRewardListPanel, true);
			NKCUtil.SetGameobjectActive(this.m_objMissionListPanel, true);
			this.m_LoopRewardScrollRect.PrepareCells(0);
			this.m_LoopRewardScrollRect.TotalCount = 0;
			this.m_LoopRewardScrollRect.RefreshCells(false);
			this.m_LoopMissionScrollRect.PrepareCells(0);
			this.m_LoopMissionScrollRect.TotalCount = 0;
			this.m_LoopMissionScrollRect.RefreshCells(false);
			base.gameObject.SetActive(false);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnPassLevelUp, new UnityAction(this.OnClickPassLevelUp));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnPurchaseCorePass, new UnityAction(this.OnClickPurchaseCorePass));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglRewardSubMenu, new UnityAction<bool>(this.OnToggleRewardPanel));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglMissionSubMenu, new UnityAction<bool>(this.OnToggleMissionPanel));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnDailyMission, delegate()
			{
				this.OnClickDailyMission(true);
			});
			NKCUtil.SetButtonClickDelegate(this.m_csbtnWeeklyMission, delegate()
			{
				this.OnClickWeeklyMission(true);
			});
			NKCUtil.SetButtonClickDelegate(this.m_csbtnCompleteAllMission, new UnityAction(this.OnClickMissionCompleteAll));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnEventStage, new UnityAction(this.OnClickEventStage));
			if (this.m_csbtnEventStage != null)
			{
				this.m_csbtnEventStage.m_bGetCallbackWhileLocked = true;
			}
			this.m_LoopRewardScrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this.OnRewardScrollValueChanged));
			this.m_listMissionInfo.Clear();
			this.m_listMissionInfo.Add(EventPassMissionType.Daily, null);
			this.m_listMissionInfo.Add(EventPassMissionType.Weekly, null);
			this.m_missionTempletInfo.Clear();
			this.m_missionTempletInfo.Add(EventPassMissionType.Daily, new List<NKMMissionTemplet>());
			this.m_missionTempletInfo.Add(EventPassMissionType.Weekly, new List<NKMMissionTemplet>());
			this.m_resetMissionTime.Clear();
			this.m_resetMissionTime.Add(EventPassMissionType.Daily, NKCUIEventPass.GetMissionResetTime(EventPassMissionType.Daily));
			this.m_resetMissionTime.Add(EventPassMissionType.Weekly, NKCUIEventPass.GetMissionResetTime(EventPassMissionType.Weekly));
			this.m_finalMissionCompleted.Clear();
			this.m_finalMissionCompleted.Add(EventPassMissionType.Daily, false);
			this.m_finalMissionCompleted.Add(EventPassMissionType.Weekly, false);
			this.m_dSendMissionRequest = null;
			this.m_currentMissionType = EventPassMissionType.Daily;
			this.m_finalCoreRewardSlot.Init();
			this.m_finalNormalRewardSlot.Init();
			this.m_achieveSlot.Init();
			this.m_characterView.Init(null, null);
		}

		// Token: 0x060064C0 RID: 25792 RVA: 0x001FF97B File Offset: 0x001FDB7B
		public override void CloseInternal()
		{
			this.m_aniEventPass.keepAnimatorControllerStateOnDisable = false;
			base.gameObject.SetActive(false);
			this.m_characterView.CleanUp();
		}

		// Token: 0x060064C1 RID: 25793 RVA: 0x001FF9A0 File Offset: 0x001FDBA0
		public void Open(NKCEventPassDataManager eventPassDataManager)
		{
			if (NKCUIEventPass.m_NKCEventPassDataManager == null)
			{
				if (eventPassDataManager == null)
				{
					return;
				}
				NKCUIEventPass.m_NKCEventPassDataManager = eventPassDataManager;
				this.m_iEventPassId = eventPassDataManager.EventPassId;
				if (this.m_listMissionInfo[EventPassMissionType.Daily] != null)
				{
					this.m_listMissionInfo[EventPassMissionType.Daily].Clear();
					this.m_listMissionInfo[EventPassMissionType.Daily] = null;
				}
				if (this.m_listMissionInfo[EventPassMissionType.Weekly] != null)
				{
					this.m_listMissionInfo[EventPassMissionType.Weekly].Clear();
					this.m_listMissionInfo[EventPassMissionType.Weekly] = null;
				}
				this.m_missionTempletInfo[EventPassMissionType.Daily].Clear();
				this.m_missionTempletInfo[EventPassMissionType.Weekly].Clear();
				this.m_dSendMissionRequest = null;
			}
			base.gameObject.SetActive(true);
			this.m_fUpdateTime = 1f;
			this.SetCorePassPurchaseState(NKCUIEventPass.m_NKCEventPassDataManager.CorePassPurchased);
			NKCUtil.SetGameobjectActive(this.m_objPassMissionComplete, false);
			NKCUtil.SetGameobjectActive(this.m_objLevelFullMissionDisable, false);
			NKCUtil.SetGameobjectActive(this.m_objAchieveSlotEffect, false);
			NKMEventPassTemplet nkmeventPassTemplet = NKMTempletContainer<NKMEventPassTemplet>.Find(this.m_iEventPassId);
			if (nkmeventPassTemplet == null)
			{
				return;
			}
			NKCUtil.SetLabelText(this.m_lbPassTitle, NKCStringTable.GetString(nkmeventPassTemplet.EventPassTitleStrId, false));
			NKCUtil.SetLabelText(this.m_lbPassTime, NKCUtilString.GetTimeIntervalString(nkmeventPassTemplet.EventPassStartDate, nkmeventPassTemplet.EventPassEndDate, NKMTime.INTERVAL_FROM_UTC, false));
			this.SetPassLevelExp(NKCUIEventPass.m_NKCEventPassDataManager.TotalExp, nkmeventPassTemplet.PassLevelUpExp, nkmeventPassTemplet.PassMaxLevel, NKCUIEventPass.ExpFXType.NONE);
			this.SetMaxLevelRewardFloatingUI(nkmeventPassTemplet);
			NKCUIEventPass.SetMaxLevelMainRewardImage(nkmeventPassTemplet, this.m_characterView, this.m_eventPassEquip, this.m_objEquipRoot);
			this.SetEndNotice(nkmeventPassTemplet);
			this.SetRewardRedDot(NKCUIEventPass.m_bRewardDot);
			this.SetMissionRedDot(NKCUIEventPass.m_bDailyMissionDot, NKCUIEventPass.m_bWeeklyMissionDot);
			this.SetEventMissionButtonState(nkmeventPassTemplet);
			NKCUIComToggle tglRewardSubMenu = this.m_tglRewardSubMenu;
			if (tglRewardSubMenu != null)
			{
				tglRewardSubMenu.Select(false, true, false);
			}
			NKCUIComToggle tglRewardSubMenu2 = this.m_tglRewardSubMenu;
			if (tglRewardSubMenu2 != null)
			{
				tglRewardSubMenu2.Select(true, false, false);
			}
			this.m_aniEventPass.keepAnimatorControllerStateOnDisable = true;
			base.UIOpened(true);
		}

		// Token: 0x060064C2 RID: 25794 RVA: 0x001FFB84 File Offset: 0x001FDD84
		public void OnRecv(NKMPacket_EVENT_PASS_LEVEL_COMPLETE_ACK cNKMPacket_EVENT_PASS_LEVEL_COMPLETE_ACK)
		{
			NKCUIEventPass.m_NKCEventPassDataManager.NormalRewardLevel = cNKMPacket_EVENT_PASS_LEVEL_COMPLETE_ACK.rewardNormalLevel;
			NKCUIEventPass.m_NKCEventPassDataManager.CoreRewardLevel = cNKMPacket_EVENT_PASS_LEVEL_COMPLETE_ACK.rewardCoreLevel;
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			this.RefreshScrollRect(false);
			NKCUIResult.Instance.OpenRewardGain(myUserData.m_ArmyData, cNKMPacket_EVENT_PASS_LEVEL_COMPLETE_ACK.rewardData, null, NKCUtilString.GET_STRING_RESULT_MISSION, "", null);
		}

		// Token: 0x060064C3 RID: 25795 RVA: 0x001FFBEC File Offset: 0x001FDDEC
		public void OnRecv(NKMPacket_EVENT_PASS_MISSION_ACK cNKMPacket_EVENT_PASS_MISSION_ACK)
		{
			EventPassMissionType missionType = cNKMPacket_EVENT_PASS_MISSION_ACK.missionType;
			if (this.m_listMissionInfo[missionType] != null)
			{
				this.m_listMissionInfo[missionType].Clear();
				this.m_listMissionInfo[missionType] = null;
			}
			this.m_listMissionInfo[missionType] = cNKMPacket_EVENT_PASS_MISSION_ACK.missionInfoList;
			this.m_listMissionInfo[missionType].Sort(delegate(NKMEventPassMissionInfo e1, NKMEventPassMissionInfo e2)
			{
				if (e1.slotIndex > e2.slotIndex)
				{
					return 1;
				}
				if (e1.slotIndex < e2.slotIndex)
				{
					return -1;
				}
				return 0;
			});
			this.m_resetMissionTime[missionType] = NKCSynchronizedTime.ToUtcTime(cNKMPacket_EVENT_PASS_MISSION_ACK.nextResetDate);
			this.m_finalMissionCompleted[missionType] = cNKMPacket_EVENT_PASS_MISSION_ACK.isFinalMissionCompleted;
			this.m_dSendMissionRequest = new NKCUIEventPass.SendMissionRequest(NKCPacketSender.Send_NKMPacket_EVENT_PASS_MISSION_REQ);
			bool flag = true;
			if (this.m_bMissionTabClick)
			{
				this.m_bMissionTabClick = false;
				if (missionType == EventPassMissionType.Daily && this.IsDailyMissionCompleted(this.m_listMissionInfo[missionType]))
				{
					this.OnClickWeeklyMission(false);
					flag = false;
				}
			}
			if (missionType == EventPassMissionType.Daily && this.m_currentMissionType == EventPassMissionType.Weekly && this.m_objMissionListPanel.activeSelf && (this.m_listMissionInfo[EventPassMissionType.Weekly] == null || NKCSynchronizedTime.IsFinished(this.m_resetMissionTime[EventPassMissionType.Weekly])))
			{
				this.m_dSendMissionRequest = null;
				NKCPacketSender.Send_NKMPacket_EVENT_PASS_MISSION_REQ(EventPassMissionType.Weekly);
				flag = false;
			}
			if (NKCUIEventPass.IsInstanceOpen && flag)
			{
				this.RefreshScrollRect(true);
			}
		}

		// Token: 0x060064C4 RID: 25796 RVA: 0x001FFD34 File Offset: 0x001FDF34
		public void RefreshPurchaseCorePass(bool corePassPurchased, int totalExp = -1)
		{
			this.SetCorePassPurchaseState(corePassPurchased);
			NKMEventPassTemplet nkmeventPassTemplet = NKMTempletContainer<NKMEventPassTemplet>.Find(this.m_iEventPassId);
			if (totalExp >= 0 && nkmeventPassTemplet != null)
			{
				this.SetPassLevelExp(totalExp, nkmeventPassTemplet.PassLevelUpExp, nkmeventPassTemplet.PassMaxLevel, NKCUIEventPass.ExpFXType.DIRECT);
			}
			if (this.m_objRewardListPanel.activeSelf)
			{
				this.RefreshScrollRectCounterPassReward(true);
			}
			else
			{
				this.UpdateRewardRedDot();
				this.SetMissionRedDot(NKCUIEventPass.m_bDailyMissionDot, NKCUIEventPass.m_bWeeklyMissionDot);
			}
			NKCPopupEventPassUnlock.Instance.Open();
		}

		// Token: 0x060064C5 RID: 25797 RVA: 0x001FFDA8 File Offset: 0x001FDFA8
		public void RefreshFinalMissionCompleted(NKMPacket_EVENT_PASS_FINAL_MISSION_COMPLETE_ACK cPacket)
		{
			this.m_finalMissionCompleted[cPacket.missionType] = true;
			int num = cPacket.totalExp - NKCUIEventPass.m_NKCEventPassDataManager.TotalExp;
			this.RefreshPassTotalExpRelatedInfo(cPacket.totalExp, true, NKCUIEventPass.ExpFXType.TRANS);
			if (NKCScenManager.GetScenManager().GetMyUserData() == null)
			{
				return;
			}
			Tweener tweenerExpGauge = this.m_tweenerExpGauge;
			if (tweenerExpGauge != null)
			{
				tweenerExpGauge.Pause<Tweener>();
			}
			NKMAdditionalReward nkmadditionalReward = new NKMAdditionalReward();
			nkmadditionalReward.eventPassExpDelta = (long)num;
			NKCPopupMessageToastSimple.Instance.Open(new NKMRewardData(), nkmadditionalReward, delegate
			{
				this.PlayExpDoTween();
			});
		}

		// Token: 0x060064C6 RID: 25798 RVA: 0x001FFE30 File Offset: 0x001FE030
		public void RefreshPassTotalExpRelatedInfo(int totalExp, bool initScrollPosition = true, NKCUIEventPass.ExpFXType fxType = NKCUIEventPass.ExpFXType.DIRECT)
		{
			if (totalExp >= 0)
			{
				NKMEventPassTemplet nkmeventPassTemplet = NKMTempletContainer<NKMEventPassTemplet>.Find(this.m_iEventPassId);
				if (nkmeventPassTemplet != null)
				{
					this.SetPassLevelExp(totalExp, nkmeventPassTemplet.PassLevelUpExp, nkmeventPassTemplet.PassMaxLevel, fxType);
				}
			}
			this.RefreshScrollRect(initScrollPosition);
		}

		// Token: 0x060064C7 RID: 25799 RVA: 0x001FFE6C File Offset: 0x001FE06C
		public void RefreshPassAdditionalExpRelatedInfo(long addExp)
		{
			if (addExp > 0L)
			{
				NKMEventPassTemplet nkmeventPassTemplet = NKMTempletContainer<NKMEventPassTemplet>.Find(this.m_iEventPassId);
				if (nkmeventPassTemplet != null)
				{
					this.SetPassLevelExp(NKCUIEventPass.m_NKCEventPassDataManager.TotalExp + (int)addExp, nkmeventPassTemplet.PassLevelUpExp, nkmeventPassTemplet.PassMaxLevel, NKCUIEventPass.ExpFXType.DIRECT);
				}
			}
			Tweener tweenerExpGauge = this.m_tweenerExpGauge;
			if (tweenerExpGauge != null)
			{
				tweenerExpGauge.Pause<Tweener>();
			}
			this.RefreshScrollRect(true);
		}

		// Token: 0x060064C8 RID: 25800 RVA: 0x001FFEC8 File Offset: 0x001FE0C8
		public void RefreshSelectedDailyMissionSlot(NKMEventPassMissionInfo missionInfo)
		{
			int num = missionInfo.slotIndex - 1;
			if (this.m_listMissionInfo[this.m_currentMissionType].Count > num)
			{
				this.m_listMissionInfo[this.m_currentMissionType][num] = missionInfo;
			}
			this.RefreshScrollRect(false);
		}

		// Token: 0x060064C9 RID: 25801 RVA: 0x001FFF18 File Offset: 0x001FE118
		public static void RefreshMissionState(HashSet<NKMMissionData> missionUpdateList)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			bool flag = false;
			bool flag2 = false;
			foreach (NKMMissionData nkmmissionData in missionUpdateList)
			{
				NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(nkmmissionData.tabId);
				if (missionTabTemplet != null && missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.EVENT_PASS)
				{
					NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(nkmmissionData.mission_id);
					if (missionTemplet != null)
					{
						if (NKMMissionManager.CanComplete(missionTemplet, myUserData, nkmmissionData) == NKM_ERROR_CODE.NEC_OK)
						{
							if (missionTemplet.m_ResetInterval == NKM_MISSION_RESET_INTERVAL.DAILY)
							{
								flag = true;
							}
							if (missionTemplet.m_ResetInterval == NKM_MISSION_RESET_INTERVAL.WEEKLY)
							{
								flag2 = true;
							}
						}
						if (flag && flag2)
						{
							break;
						}
					}
				}
			}
			if (NKCUIEventPass.m_dOnMissionUpdate != null)
			{
				NKCUIEventPass.m_dOnMissionUpdate();
			}
			if (flag)
			{
				NKCUIEventPass.DailyMissionRedDot = true;
			}
			if (flag2)
			{
				NKCUIEventPass.WeeklyMissionRedDot = true;
			}
			NKCEventPassDataManager eventPassDataManager = NKCScenManager.GetScenManager().GetEventPassDataManager();
			if (eventPassDataManager != null)
			{
				int passLevel = eventPassDataManager.GetPassLevel();
				NKMEventPassTemplet nkmeventPassTemplet = NKMEventPassTemplet.Find(eventPassDataManager.EventPassId);
				if (nkmeventPassTemplet != null && passLevel >= nkmeventPassTemplet.PassMaxLevel)
				{
					NKCUIEventPass.DailyMissionRedDot = false;
					NKCUIEventPass.WeeklyMissionRedDot = false;
				}
			}
			if (NKCUIEventPass.IsInstanceOpen)
			{
				NKCUIEventPass.Instance.RefreshScrollRect(false);
			}
		}

		// Token: 0x060064CA RID: 25802 RVA: 0x00200040 File Offset: 0x001FE240
		public void SetRewardRedDot(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_objRewardRedDot, value);
		}

		// Token: 0x060064CB RID: 25803 RVA: 0x0020004E File Offset: 0x001FE24E
		public void SetDailyMissionRedDot(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_objDailyMissionRedDot, value);
		}

		// Token: 0x060064CC RID: 25804 RVA: 0x0020005C File Offset: 0x001FE25C
		public void SetWeeklyMissionRedDot(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_objWeeklyMissionRedDot, value);
		}

		// Token: 0x060064CD RID: 25805 RVA: 0x0020006C File Offset: 0x001FE26C
		public void SetPassMissionCompleteText(EventPassMissionType missionType)
		{
			if (missionType == EventPassMissionType.Daily)
			{
				NKCUtil.SetLabelText(this.m_lbPassMissionCompleteText, NKCUtilString.GET_STRING_EVENTPASS_MISSION_COMPLETE_DAILY_ALL);
				NKCUtil.SetLabelText(this.m_lbPassMissionCompleteExText, NKCUtilString.GET_STRING_EVENTPASS_MISSION_COMPLETE_DAILY_ALL_EX);
				return;
			}
			if (missionType == EventPassMissionType.Weekly)
			{
				NKCUtil.SetLabelText(this.m_lbPassMissionCompleteText, NKCUtilString.GET_STRING_EVENTPASS_MISSION_COMPLETE_WEEKLY_ALL);
				NKCUtil.SetLabelText(this.m_lbPassMissionCompleteExText, NKCUtilString.GET_STRING_EVENTPASS_MISSION_COMPLETE_WEEKLY_ALL_EX);
			}
		}

		// Token: 0x060064CE RID: 25806 RVA: 0x002000C4 File Offset: 0x001FE2C4
		public static void SetMaxLevelMainRewardImage(NKMEventPassTemplet eventPassTemplet, NKCUICharacterView characterView, NKCUIComEventPassEquip eventPassEquip, GameObject equipRoot)
		{
			if (eventPassTemplet == null)
			{
				return;
			}
			switch (eventPassTemplet.EventPassMainRewardType)
			{
			case NKM_REWARD_TYPE.RT_UNIT:
			case NKM_REWARD_TYPE.RT_SHIP:
			case NKM_REWARD_TYPE.RT_OPERATOR:
				NKCUtil.SetGameobjectActive(characterView, true);
				NKCUtil.SetGameobjectActive(equipRoot, false);
				characterView.SetCharacterIllust(eventPassTemplet.EventPassMainReward, 0, false, true, 0);
				return;
			case NKM_REWARD_TYPE.RT_MISC:
			case NKM_REWARD_TYPE.RT_EQUIP:
			case NKM_REWARD_TYPE.RT_MOLD:
				NKCUtil.SetGameobjectActive(characterView, false);
				NKCUtil.SetGameobjectActive(equipRoot, true);
				eventPassEquip.SetData(eventPassTemplet);
				break;
			case NKM_REWARD_TYPE.RT_USER_EXP:
			case NKM_REWARD_TYPE.RT_BUFF:
			case NKM_REWARD_TYPE.RT_EMOTICON:
			case NKM_REWARD_TYPE.RT_MISSION_POINT:
			case NKM_REWARD_TYPE.RT_BINGO_TILE:
			case NKM_REWARD_TYPE.RT_PASS_EXP:
				break;
			case NKM_REWARD_TYPE.RT_SKIN:
			{
				NKCUtil.SetGameobjectActive(characterView, true);
				NKCUtil.SetGameobjectActive(equipRoot, false);
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(eventPassTemplet.EventPassMainReward);
				characterView.SetCharacterIllust(skinTemplet, false, true, 0);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x060064CF RID: 25807 RVA: 0x00200172 File Offset: 0x001FE372
		public bool IsExpOverflowed(NKMEventPassTemplet eventPassTemplet, int addExp)
		{
			return NKCUIEventPass.m_NKCEventPassDataManager.TotalExp + addExp > eventPassTemplet.PassMaxExp;
		}

		// Token: 0x060064D0 RID: 25808 RVA: 0x00200188 File Offset: 0x001FE388
		public void PlayExpDoTween()
		{
			Tweener tweenerExpGauge = this.m_tweenerExpGauge;
			if (tweenerExpGauge == null)
			{
				return;
			}
			tweenerExpGauge.Play<Tweener>();
		}

		// Token: 0x060064D1 RID: 25809 RVA: 0x0020019C File Offset: 0x001FE39C
		public static bool IsEventTime(bool activateAlarm = true)
		{
			NKCEventPassDataManager eventPassDataManager = NKCScenManager.GetScenManager().GetEventPassDataManager();
			if (eventPassDataManager == null)
			{
				return false;
			}
			NKMEventPassTemplet nkmeventPassTemplet = NKMTempletContainer<NKMEventPassTemplet>.Find(eventPassDataManager.EventPassId);
			if (nkmeventPassTemplet == null)
			{
				return false;
			}
			if (NKMContentsVersionManager.HasDFChangeTagType(DataFormatChangeTagType.OPEN_TAG_EVENTPASS) && !nkmeventPassTemplet.EnableByTag)
			{
				return false;
			}
			DateTime startTimeUTC = NKCSynchronizedTime.ToUtcTime(nkmeventPassTemplet.EventPassStartDate);
			DateTime finishTimeUTC = NKCSynchronizedTime.ToUtcTime(nkmeventPassTemplet.EventPassEndDate);
			bool flag = NKCSynchronizedTime.IsEventTime(startTimeUTC, finishTimeUTC);
			if (!flag && activateAlarm)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_EVENTPASS_END, new NKCPopupOKCancel.OnButton(NKCUIEventPass.CheckInstanceAndClose), "");
			}
			if (!flag)
			{
				eventPassDataManager.EventPassDataReceived = false;
			}
			return flag;
		}

		// Token: 0x060064D2 RID: 25810 RVA: 0x00200230 File Offset: 0x001FE430
		public static DateTime GetMissionResetTime(EventPassMissionType missionType)
		{
			DateTime serverUTCTime = NKCSynchronizedTime.GetServerUTCTime(0.0);
			if (missionType == EventPassMissionType.Daily)
			{
				return NKMTime.GetNextResetTime(serverUTCTime, NKMTime.TimePeriod.Day);
			}
			if (missionType == EventPassMissionType.Weekly)
			{
				return NKMTime.GetNextResetTime(serverUTCTime, NKMTime.TimePeriod.Week);
			}
			return default(DateTime);
		}

		// Token: 0x060064D3 RID: 25811 RVA: 0x0020026C File Offset: 0x001FE46C
		private void Update()
		{
			this.m_fUpdateTime -= Time.deltaTime;
			if (this.m_fUpdateTime < 0f)
			{
				if (this.m_objMissionListPanel.activeSelf)
				{
					this.UpdateMissionResetTime(this.m_resetMissionTime[this.m_currentMissionType]);
				}
				if (this.m_objMissionResetting.activeSelf)
				{
					this.OnOffMissionResettingPanel(this.m_currentMissionType);
				}
				if (this.m_dSendMissionRequest != null)
				{
					if (!NKCUIEventPass.IsEventTime(false))
					{
						this.m_dSendMissionRequest = null;
						return;
					}
					if (NKCSynchronizedTime.IsFinished(this.m_resetMissionTime[EventPassMissionType.Daily]))
					{
						this.m_dSendMissionRequest(EventPassMissionType.Daily);
						this.m_dSendMissionRequest = null;
						if (NKCUIEventPass.m_dOnMissionUpdate != null)
						{
							NKCUIEventPass.m_dOnMissionUpdate();
						}
					}
				}
				this.m_fUpdateTime = 1f;
			}
		}

		// Token: 0x060064D4 RID: 25812 RVA: 0x00200332 File Offset: 0x001FE532
		private void SetCorePassPurchaseState(bool corePassPurchased)
		{
			this.m_csbtnPurchaseCorePass.SetLock(corePassPurchased, false);
		}

		// Token: 0x060064D5 RID: 25813 RVA: 0x00200344 File Offset: 0x001FE544
		private void SetPassLevelExp(int totalExp, int passLevelUpExp, int passMaxLevel, NKCUIEventPass.ExpFXType fxType)
		{
			int totalExp2 = NKCUIEventPass.m_NKCEventPassDataManager.TotalExp;
			NKCUIEventPass.m_NKCEventPassDataManager.TotalExp = totalExp;
			int iUserPassLevel = this.m_iUserPassLevel;
			this.m_iUserPassLevel = Mathf.Min(passMaxLevel, totalExp / passLevelUpExp + 1);
			int currentExp = totalExp % passLevelUpExp;
			string expTextColor = ColorUtility.ToHtmlStringRGB(this.m_passExpColor);
			int num = Mathf.Min(totalExp, (passMaxLevel - 1) * passLevelUpExp);
			int startExp = Mathf.Max(0, ((num - totalExp2) / passLevelUpExp < 10) ? totalExp2 : (num - passLevelUpExp * 10));
			float duration = ((float)Mathf.Max(0, num - startExp) / (float)passLevelUpExp + 1f) * this.m_fExpGaugeTimeMultiplier;
			if (this.m_tweenerExpGauge != null && this.m_tweenerExpGauge.IsActive())
			{
				this.m_tweenerExpGauge.Kill(true);
			}
			this.m_tweenerExpGauge = DOTween.To(() => startExp, delegate(int x)
			{
				int num2 = Mathf.Min(passMaxLevel, x / passLevelUpExp + 1);
				int num3 = x % passLevelUpExp;
				if (num2 >= passMaxLevel)
				{
					num3 = passLevelUpExp;
				}
				NKCUtil.SetLabelText(this.m_lbPassExp, string.Format(NKCUtilString.GET_STRING_EVENTPASS_EXP, new object[]
				{
					this.m_iExpFontSize,
					expTextColor,
					num3,
					passLevelUpExp
				}));
				NKCUtil.SetLabelText(this.m_lbPassLevel, num2.ToString());
				NKCUtil.SetImageFillAmount(this.m_imgPassExpGauge, (float)num3 / (float)passLevelUpExp);
			}, num, duration).SetEase(Ease.OutCubic).OnComplete(delegate
			{
				this.CheckPassLevelAchievedMax(currentExp, passLevelUpExp, passMaxLevel);
			});
			NKCUtil.SetGameobjectActive(this.m_csbtnPassLevelUp, this.m_iUserPassLevel < passMaxLevel);
			NKCUIEventPass.ExpGainType gainType = NKCUIEventPass.ExpGainType.Gain;
			if (iUserPassLevel < this.m_iUserPassLevel)
			{
				gainType = NKCUIEventPass.ExpGainType.LevelUp;
			}
			else if (totalExp2 < totalExp)
			{
				gainType = NKCUIEventPass.ExpGainType.Gain;
			}
			this.TriggerExpFx(gainType, fxType);
		}

		// Token: 0x060064D6 RID: 25814 RVA: 0x002004C0 File Offset: 0x001FE6C0
		private void CheckPassLevelAchievedMax(int currentExp, int passLevelUpExp, int passMaxLevel)
		{
			if (this.m_iUserPassLevel >= passMaxLevel)
			{
				currentExp = passLevelUpExp;
				NKCUtil.SetGameobjectActive(this.m_objExpRoot, false);
				NKCUtil.SetGameobjectActive(this.m_objExpMaxLevel, true);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objExpRoot, true);
				NKCUtil.SetGameobjectActive(this.m_objExpMaxLevel, false);
			}
			string text = ColorUtility.ToHtmlStringRGB(this.m_passExpColor);
			NKCUtil.SetLabelText(this.m_lbPassExp, string.Format(NKCUtilString.GET_STRING_EVENTPASS_EXP, new object[]
			{
				this.m_iExpFontSize,
				text,
				currentExp,
				passLevelUpExp
			}));
			NKCUtil.SetLabelText(this.m_lbPassLevel, this.m_iUserPassLevel.ToString());
			NKCUtil.SetImageFillAmount(this.m_imgPassExpGauge, (float)currentExp / (float)passLevelUpExp);
		}

		// Token: 0x060064D7 RID: 25815 RVA: 0x0020057C File Offset: 0x001FE77C
		private void SetMaxLevelRewardFloatingUI(NKMEventPassTemplet eventPassTemplet)
		{
			NKMEventPassRewardTemplet rewardTemplet = NKMEventPassRewardTemplet.GetRewardTemplet(eventPassTemplet.PassRewardGroupId, eventPassTemplet.PassMaxLevel);
			if (rewardTemplet == null)
			{
				return;
			}
			NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(rewardTemplet.NormalRewardItemType, rewardTemplet.NormalRewardItemId, rewardTemplet.NormalRewardItemCount, 0);
			NKCUISlot finalNormalRewardSlot = this.m_finalNormalRewardSlot;
			if (finalNormalRewardSlot != null)
			{
				finalNormalRewardSlot.SetData(data, true, null);
			}
			NKCUISlot.SlotData data2 = NKCUISlot.SlotData.MakeRewardTypeData(rewardTemplet.CoreRewardItemType, rewardTemplet.CoreRewardItemId, rewardTemplet.CoreRewardItemCount, 0);
			NKCUISlot finalCoreRewardSlot = this.m_finalCoreRewardSlot;
			if (finalCoreRewardSlot != null)
			{
				finalCoreRewardSlot.SetData(data2, true, null);
			}
			string arg = ColorUtility.ToHtmlStringRGB(this.m_colFinalRewardFontColor);
			NKCUtil.SetLabelText(this.m_lbFinalRewardLevel, string.Format(NKCUtilString.GET_STRING_EVENTPASS_MAX_PASS_LEVEL_FINAL_REWARD, this.m_iFinalRewardFontSize, arg, eventPassTemplet.PassMaxLevel));
		}

		// Token: 0x060064D8 RID: 25816 RVA: 0x00200634 File Offset: 0x001FE834
		private void SetEndNotice(NKMEventPassTemplet eventPassTemplet)
		{
			if (eventPassTemplet == null)
			{
				return;
			}
			TimeSpan timeLeft = NKCSynchronizedTime.GetTimeLeft(NKCSynchronizedTime.ToUtcTime(eventPassTemplet.EventPassEndDate));
			string arg = string.Empty;
			if (timeLeft.Days > 0)
			{
				arg = string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_DAYS", false), timeLeft.Days);
			}
			else if (timeLeft.Hours > 0)
			{
				arg = string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_HOURS", false), timeLeft.Hours);
			}
			else if (timeLeft.Minutes > 0)
			{
				arg = string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_MINUTES", false), timeLeft.Minutes);
			}
			else
			{
				arg = string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_MINUTES", false), 1);
			}
			if (timeLeft.Days > this.m_iEndWarningRemainDays)
			{
				NKCUtil.SetGameobjectActive(this.m_objEndNotice, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objEndNotice, true);
			string arg2 = ColorUtility.ToHtmlStringRGB(this.m_colEndTimeText);
			if (timeLeft.Days > 0)
			{
				NKCUtil.SetLabelText(this.m_lbEndTimeRemain, string.Format(NKCUtilString.GET_STRING_EVENTPASS_END_TIME_REMAIN, arg2, arg));
				return;
			}
			NKCUtil.SetLabelText(this.m_lbEndTimeRemain, string.Format(NKCUtilString.GET_STRING_EVENTPASS_END_TIME_ALMOST_END, arg2, arg));
		}

		// Token: 0x060064D9 RID: 25817 RVA: 0x0020075C File Offset: 0x001FE95C
		private void TriggerExpFx(NKCUIEventPass.ExpGainType gainType, NKCUIEventPass.ExpFXType fxType)
		{
			if (fxType != NKCUIEventPass.ExpFXType.DIRECT)
			{
				if (fxType != NKCUIEventPass.ExpFXType.TRANS)
				{
					return;
				}
				if (gainType == NKCUIEventPass.ExpGainType.LevelUp)
				{
					this.m_aniEventPass.ResetTrigger("TRANS_LEVEL_UP");
					this.m_aniEventPass.SetTrigger("TRANS_LEVEL_UP");
					return;
				}
				if (gainType == NKCUIEventPass.ExpGainType.Gain)
				{
					this.m_aniEventPass.ResetTrigger("TRANS_GAIN");
					this.m_aniEventPass.SetTrigger("TRANS_GAIN");
				}
			}
			else
			{
				if (gainType == NKCUIEventPass.ExpGainType.LevelUp)
				{
					this.m_aniEventPass.ResetTrigger("DIRECT_LEVEL_UP");
					this.m_aniEventPass.SetTrigger("DIRECT_LEVEL_UP");
					return;
				}
				if (gainType == NKCUIEventPass.ExpGainType.Gain)
				{
					this.m_aniEventPass.ResetTrigger("DIRECT_GAIN");
					this.m_aniEventPass.SetTrigger("DIRECT_GAIN");
					return;
				}
			}
		}

		// Token: 0x060064DA RID: 25818 RVA: 0x00200804 File Offset: 0x001FEA04
		private void FindMissionTemplet(EventPassMissionType missionType)
		{
			if (this.m_listMissionInfo[missionType] == null)
			{
				return;
			}
			this.m_missionTempletInfo[missionType].Clear();
			int count = this.m_listMissionInfo[missionType].Count;
			for (int i = 0; i < count; i++)
			{
				NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(this.m_listMissionInfo[missionType][i].missionId);
				this.m_missionTempletInfo[missionType].Add(missionTemplet);
			}
			this.m_missionTempletInfo[missionType].Sort(new Comparison<NKMMissionTemplet>(this.Comparer));
		}

		// Token: 0x060064DB RID: 25819 RVA: 0x0020089C File Offset: 0x001FEA9C
		private int Comparer(NKMMissionTemplet x, NKMMissionTemplet y)
		{
			NKMMissionManager.MissionStateData missionStateData = NKMMissionManager.GetMissionStateData(x);
			NKMMissionManager.MissionStateData missionStateData2 = NKMMissionManager.GetMissionStateData(y);
			if (missionStateData.state != missionStateData2.state)
			{
				return missionStateData.state.CompareTo(missionStateData2.state);
			}
			return 0;
		}

		// Token: 0x060064DC RID: 25820 RVA: 0x002008E4 File Offset: 0x001FEAE4
		private void RefreshScrollRect(bool initScrollPosition = true)
		{
			if (this.m_objRewardListPanel.activeSelf)
			{
				this.RefreshScrollRectCounterPassReward(initScrollPosition);
			}
			if (this.m_objMissionListPanel.activeSelf)
			{
				NKMEventPassTemplet nkmeventPassTemplet = NKMTempletContainer<NKMEventPassTemplet>.Find(this.m_iEventPassId);
				if (nkmeventPassTemplet != null)
				{
					if (this.m_iUserPassLevel >= nkmeventPassTemplet.PassMaxLevel)
					{
						NKCUtil.SetGameobjectActive(this.m_objLevelFullMissionDisable, true);
						NKCUtil.SetGameobjectActive(this.m_objMissionListPanel, false);
						NKCUtil.SetGameobjectActive(this.m_objMissionResetting, false);
						this.SetMissionRedDot(false, false);
						this.UpdateRewardRedDot();
						return;
					}
					int weekSinceEventStart = nkmeventPassTemplet.GetWeekSinceEventStart(NKCSynchronizedTime.ServiceTime);
					this.FindMissionTemplet(this.m_currentMissionType);
					bool flag = this.m_finalMissionCompleted[this.m_currentMissionType];
					NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
					this.m_achieveSlot.Init();
					long completedMissionCount = this.GetCompletedMissionCount(myUserData, EventPassMissionType.Daily);
					long completedMissionCount2 = this.GetCompletedMissionCount(myUserData, EventPassMissionType.Weekly);
					bool flag2 = completedMissionCount >= (long)nkmeventPassTemplet.DailyMissionClearCount;
					bool flag3 = completedMissionCount2 >= (long)nkmeventPassTemplet.WeeklyMissionClearCount;
					bool flag4 = flag2 && !this.m_finalMissionCompleted[EventPassMissionType.Daily];
					bool flag5 = flag3 && !this.m_finalMissionCompleted[EventPassMissionType.Weekly];
					bool flag6 = NKCUIEventPass.DailyMissionRedDot;
					bool flag7 = NKCUIEventPass.WeeklyMissionRedDot;
					bool flag8;
					bool bValue;
					NKCUISlot.SlotData slotData;
					if (this.m_currentMissionType == EventPassMissionType.Daily)
					{
						NKCUtil.SetGameobjectActive(this.m_objDailyAchieveBG, true);
						NKCUtil.SetGameobjectActive(this.m_objWeeklyAchieveBG, false);
						NKCUtil.SetLabelText(this.m_lbAchieveTitle, NKCUtilString.GET_STRING_EVENTPASS_DAILY_MISSION_ACHIEVE);
						string arg = ColorUtility.ToHtmlStringRGB(this.m_colDailyCountColor);
						NKCUtil.SetLabelText(this.m_lbWeekCount, string.Format(NKCUtilString.GET_STRING_EVENTPASS_ELAPSED_WEEK, arg, weekSinceEventStart));
						flag6 = (completedMissionCount < (long)nkmeventPassTemplet.DailyMissionClearCount && this.ExistCompletableMission(myUserData, this.m_missionTempletInfo[EventPassMissionType.Daily]));
						flag8 = flag4;
						bValue = flag2;
						this.m_csbtnCompleteAllMission.SetLock(!flag6, false);
						this.m_bDailyMissionCompleteAchieved = flag2;
						NKCUtil.SetLabelText(this.m_lbAchieveCount, string.Format("{0}/{1}", Mathf.Min((float)completedMissionCount, (float)nkmeventPassTemplet.DailyMissionClearCount), nkmeventPassTemplet.DailyMissionClearCount));
						NKCUtil.SetImageFillAmount(this.m_imgAchieveGauge, (float)completedMissionCount / (float)nkmeventPassTemplet.DailyMissionClearCount);
						slotData = NKCUISlot.SlotData.MakeRewardTypeData(NKM_REWARD_TYPE.RT_PASS_EXP, 504, nkmeventPassTemplet.DailyMissionClearRewardExp, 0);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_objWeeklyAchieveBG, true);
						NKCUtil.SetGameobjectActive(this.m_objDailyAchieveBG, false);
						NKCUtil.SetLabelText(this.m_lbAchieveTitle, NKCUtilString.GET_STRING_EVENTPASS_WEEKLY_MISSION_ACHIEVE);
						string arg2 = ColorUtility.ToHtmlStringRGB(this.m_colWeeklyCountColor);
						NKCUtil.SetLabelText(this.m_lbWeekCount, string.Format(NKCUtilString.GET_STRING_EVENTPASS_ELAPSED_WEEK, arg2, weekSinceEventStart));
						flag7 = (completedMissionCount2 < (long)nkmeventPassTemplet.WeeklyMissionClearCount && this.ExistCompletableMission(myUserData, this.m_missionTempletInfo[EventPassMissionType.Weekly]));
						flag8 = flag5;
						bValue = flag3;
						this.m_csbtnCompleteAllMission.SetLock(!flag7, false);
						NKCUtil.SetLabelText(this.m_lbAchieveCount, string.Format("{0}/{1}", completedMissionCount2, nkmeventPassTemplet.WeeklyMissionClearCount));
						NKCUtil.SetImageFillAmount(this.m_imgAchieveGauge, (float)completedMissionCount2 / (float)nkmeventPassTemplet.WeeklyMissionClearCount);
						slotData = NKCUISlot.SlotData.MakeRewardTypeData(NKM_REWARD_TYPE.RT_PASS_EXP, 504, nkmeventPassTemplet.WeeklyMissionClearRewardExp, 0);
					}
					if (!flag8)
					{
						if (slotData != null)
						{
							this.m_achieveSlot.SetData(slotData, true, null);
						}
						NKCUtil.SetGameobjectActive(this.m_objAchieveSlotEffect, false);
					}
					else
					{
						if (slotData != null)
						{
							this.m_achieveSlot.SetData(slotData, true, new NKCUISlot.OnClick(this.OnClickCompleteFinalMission));
						}
						NKCUtil.SetGameobjectActive(this.m_objAchieveSlotEffect, true);
					}
					NKCUtil.SetGameobjectActive(this.m_objPassMissionComplete, bValue);
					this.SetPassMissionCompleteText(this.m_currentMissionType);
					this.UpdateMissionResetTime(this.m_resetMissionTime[this.m_currentMissionType]);
					this.m_achieveSlot.SetDisable(flag, "");
					this.m_achieveSlot.SetCompleteMark(flag);
					this.SetMissionRedDot(flag6 || flag4, flag7 || flag5);
					this.UpdateRewardRedDot();
					this.OnOffMissionResettingPanel(this.m_currentMissionType);
					if (this.m_missionTempletInfo[this.m_currentMissionType] != null)
					{
						this.m_LoopMissionScrollRect.TotalCount = this.m_missionTempletInfo[this.m_currentMissionType].Count;
						this.m_LoopMissionScrollRect.StopMovement();
						if (initScrollPosition)
						{
							this.m_LoopMissionScrollRect.SetIndexPosition(0);
							return;
						}
						this.m_LoopMissionScrollRect.RefreshCells(false);
					}
				}
			}
		}

		// Token: 0x060064DD RID: 25821 RVA: 0x00200D20 File Offset: 0x001FEF20
		private void RefreshScrollRectCounterPassReward(bool initScrollPosition)
		{
			NKCUtil.SetGameobjectActive(this.m_objLevelFullMissionDisable, false);
			NKMEventPassTemplet nkmeventPassTemplet = NKMTempletContainer<NKMEventPassTemplet>.Find(this.m_iEventPassId);
			if (nkmeventPassTemplet == null)
			{
				return;
			}
			List<NKMEventPassRewardTemplet> rewardGroupTemplet = NKMEventPassRewardTemplet.GetRewardGroupTemplet(nkmeventPassTemplet.PassRewardGroupId);
			if (rewardGroupTemplet == null)
			{
				return;
			}
			this.m_LoopRewardScrollRect.TotalCount = rewardGroupTemplet.Count;
			this.m_LoopRewardScrollRect.StopMovement();
			if (initScrollPosition)
			{
				int num = Mathf.Max(new int[]
				{
					NKCUIEventPass.m_NKCEventPassDataManager.NormalRewardLevel - 1,
					NKCUIEventPass.m_NKCEventPassDataManager.CoreRewardLevel - 1,
					this.m_iUserPassLevel - 1
				});
				num = Mathf.Clamp(num, 0, rewardGroupTemplet.Count - 1);
				this.m_LoopRewardScrollRect.SetIndexPosition(num);
			}
			else
			{
				this.m_LoopRewardScrollRect.RefreshCells(false);
			}
			this.UpdateRewardRedDot();
			this.SetMissionRedDot(NKCUIEventPass.m_bDailyMissionDot, NKCUIEventPass.m_bWeeklyMissionDot);
		}

		// Token: 0x060064DE RID: 25822 RVA: 0x00200DF0 File Offset: 0x001FEFF0
		private void UpdateRewardRedDot()
		{
			bool flag = NKCUIEventPass.m_NKCEventPassDataManager.NormalRewardLevel < this.m_iUserPassLevel;
			bool flag2 = NKCUIEventPass.m_NKCEventPassDataManager.CorePassPurchased && NKCUIEventPass.m_NKCEventPassDataManager.CoreRewardLevel < this.m_iUserPassLevel;
			NKCUIEventPass.RewardRedDot = (flag || flag2);
		}

		// Token: 0x060064DF RID: 25823 RVA: 0x00200E38 File Offset: 0x001FF038
		private void SetMissionRedDot(bool existCompletableDailyMission, bool existCompletableWeeklyMission)
		{
			NKMEventPassTemplet nkmeventPassTemplet = NKMEventPassTemplet.Find(this.m_iEventPassId);
			if (nkmeventPassTemplet != null && this.m_iUserPassLevel >= nkmeventPassTemplet.PassMaxLevel)
			{
				existCompletableDailyMission = false;
				existCompletableWeeklyMission = false;
			}
			NKCUtil.SetGameobjectActive(this.m_objMissionRedDot, existCompletableDailyMission || existCompletableWeeklyMission);
			NKCUIEventPass.DailyMissionRedDot = existCompletableDailyMission;
			NKCUIEventPass.WeeklyMissionRedDot = existCompletableWeeklyMission;
		}

		// Token: 0x060064E0 RID: 25824 RVA: 0x00200E84 File Offset: 0x001FF084
		private long GetCompletedMissionCount(NKMUserData cNKMUserData, EventPassMissionType missionType)
		{
			int num = 0;
			if (this.m_listMissionInfo.ContainsKey(missionType) && this.m_listMissionInfo[missionType] != null)
			{
				int count = this.m_listMissionInfo[missionType].Count;
				for (int i = 0; i < count; i++)
				{
					int missionId = this.m_listMissionInfo[missionType][i].missionId;
					NKMMissionData missionDataByMissionId = cNKMUserData.m_MissionData.GetMissionDataByMissionId(missionId);
					if (missionDataByMissionId != null && missionDataByMissionId.isComplete)
					{
						num++;
					}
				}
			}
			return (long)num;
		}

		// Token: 0x060064E1 RID: 25825 RVA: 0x00200F08 File Offset: 0x001FF108
		private bool ExistCompletableMission(NKMUserData cNKMUserData, List<NKMMissionTemplet> listMissionTemplet)
		{
			if (listMissionTemplet == null)
			{
				return false;
			}
			int count = listMissionTemplet.Count;
			for (int i = 0; i < count; i++)
			{
				NKMMissionTemplet nkmmissionTemplet = listMissionTemplet[i];
				NKMMissionData missionData = NKMMissionManager.GetMissionData(nkmmissionTemplet);
				if (missionData != null && !missionData.isComplete && NKMMissionManager.CanComplete(nkmmissionTemplet, cNKMUserData, missionData) == NKM_ERROR_CODE.NEC_OK)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060064E2 RID: 25826 RVA: 0x00200F54 File Offset: 0x001FF154
		private bool CheckMissionRetryEnable(EventPassMissionType missionType, int destMissionID)
		{
			NKMEventPassTemplet nkmeventPassTemplet = NKMTempletContainer<NKMEventPassTemplet>.Find(this.m_iEventPassId);
			if (nkmeventPassTemplet == null)
			{
				return false;
			}
			List<NKMEventPassMissionGroupTemplet> missionGroupList;
			if (missionType == EventPassMissionType.Daily)
			{
				missionGroupList = NKMEventPassMissionGroupTemplet.GetMissionGroupList(missionType, nkmeventPassTemplet.DailyMissionGroupId, nkmeventPassTemplet.GetWeekSinceEventStart(NKCSynchronizedTime.ServiceTime));
			}
			else
			{
				missionGroupList = NKMEventPassMissionGroupTemplet.GetMissionGroupList(missionType, nkmeventPassTemplet.WeeklyMissionGroupId, nkmeventPassTemplet.GetWeekSinceEventStart(NKCSynchronizedTime.ServiceTime));
			}
			int count = missionGroupList.Count;
			Predicate<int> <>9__0;
			for (int i = 0; i < count; i++)
			{
				List<int> missionIds = missionGroupList[i].MissionIds;
				Predicate<int> match;
				if ((match = <>9__0) == null)
				{
					match = (<>9__0 = ((int e) => e == destMissionID));
				}
				if (missionIds.Find(match) > 0)
				{
					return missionGroupList[i].IsRetryEnable;
				}
			}
			return false;
		}

		// Token: 0x060064E3 RID: 25827 RVA: 0x00201014 File Offset: 0x001FF214
		private void UpdateMissionResetTime(DateTime resetTime)
		{
			string remainTimeStringEx = NKCUtilString.GetRemainTimeStringEx(resetTime);
			string remainTimeString = NKCUtilString.GetRemainTimeString(resetTime, 2);
			NKCUtil.SetLabelText(this.m_lbAchieveTimeRemain, remainTimeStringEx);
			NKCUtil.SetLabelText(this.m_lbRefreshTimeRemain, string.Format(NKCUtilString.GET_STRING_EVENTPASS_UPDATE_TIME_LEFT, remainTimeString));
		}

		// Token: 0x060064E4 RID: 25828 RVA: 0x00201054 File Offset: 0x001FF254
		private void OnOffMissionResettingPanel(EventPassMissionType missionType)
		{
			DateTime d = this.m_resetMissionTime[missionType];
			string msg = string.Empty;
			if (missionType == EventPassMissionType.Daily)
			{
				d = d.AddDays(-1.0);
				msg = NKCUtilString.GET_STRING_EVENTPASS_MISSION_UPDATING_DAILY;
			}
			else if (missionType == EventPassMissionType.Weekly)
			{
				d = d.AddDays(-7.0);
				msg = NKCUtilString.GET_STRING_EVENTPASS_MISSION_UPDATING_WEEKLY;
			}
			TimeSpan timeSpan = NKCSynchronizedTime.GetServerUTCTime(0.0) - d;
			if (timeSpan.Ticks >= 0L && timeSpan.TotalMinutes < 5.0)
			{
				NKCUtil.SetLabelText(this.m_lbMissionResetMsg, msg);
				NKCUtil.SetGameobjectActive(this.m_objMissionResetting, true);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objMissionResetting, false);
		}

		// Token: 0x060064E5 RID: 25829 RVA: 0x00201104 File Offset: 0x001FF304
		private void SetEventMissionButtonState(NKMEventPassTemplet eventPassTemplet)
		{
			if (eventPassTemplet == null)
			{
				return;
			}
			bool flag = true;
			if (eventPassTemplet.m_ShortCutType == NKM_SHORTCUT_TYPE.SHORTCUT_NONE)
			{
				flag = false;
			}
			else if (string.IsNullOrEmpty(eventPassTemplet.m_ShortCut))
			{
				flag = false;
			}
			else
			{
				string[] array = eventPassTemplet.m_ShortCut.Split(new char[]
				{
					'@'
				});
				EPISODE_CATEGORY category;
				if (array == null || array.Length <= 1)
				{
					flag = false;
				}
				else if (Enum.TryParse<EPISODE_CATEGORY>(array[0], out category))
				{
					if (!NKCContentManager.IsContentsUnlocked(NKCContentManager.GetContentsType(category), 0, 0))
					{
						flag = false;
					}
					else
					{
						int episodeID;
						if (int.TryParse(array[1], out episodeID))
						{
							if (NKMEpisodeMgr.GetListNKMEpisodeTempletByCategory(category, true, EPISODE_DIFFICULTY.NORMAL).Find((NKMEpisodeTempletV2 e) => e.m_EpisodeID == episodeID) == null)
							{
								flag = false;
							}
						}
						else
						{
							flag = false;
						}
					}
				}
				else
				{
					flag = false;
				}
			}
			NKCUIComStateButton csbtnEventStage = this.m_csbtnEventStage;
			if (csbtnEventStage == null)
			{
				return;
			}
			csbtnEventStage.SetLock(!flag, false);
		}

		// Token: 0x060064E6 RID: 25830 RVA: 0x002011CA File Offset: 0x001FF3CA
		private RectTransform GetRewardSlot(int index)
		{
			NKCUIEventPassRewardSlot newInstance = NKCUIEventPassRewardSlot.GetNewInstance(null, false);
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x060064E7 RID: 25831 RVA: 0x002011E0 File Offset: 0x001FF3E0
		private void ReturnRewardSlot(Transform tr)
		{
			NKCUIEventPassRewardSlot component = tr.GetComponent<NKCUIEventPassRewardSlot>();
			tr.SetParent(null);
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x060064E8 RID: 25832 RVA: 0x00201218 File Offset: 0x001FF418
		private void ProvideRewardData(Transform tr, int index)
		{
			NKCUIEventPassRewardSlot component = tr.GetComponent<NKCUIEventPassRewardSlot>();
			if (component != null)
			{
				int passLevel = index + 1;
				NKMEventPassTemplet nkmeventPassTemplet = NKMTempletContainer<NKMEventPassTemplet>.Find(this.m_iEventPassId);
				if (nkmeventPassTemplet == null)
				{
					return;
				}
				NKMEventPassRewardTemplet rewardTemplet = NKMEventPassRewardTemplet.GetRewardTemplet(nkmeventPassTemplet.PassRewardGroupId, passLevel);
				int passMaxLevel = nkmeventPassTemplet.PassMaxLevel;
				component.SetData(rewardTemplet, this.m_iUserPassLevel, passMaxLevel, NKCUIEventPass.m_NKCEventPassDataManager.CorePassPurchased, NKCUIEventPass.m_NKCEventPassDataManager.NormalRewardLevel, NKCUIEventPass.m_NKCEventPassDataManager.CoreRewardLevel, new NKCUIEventPassRewardSlot.dOnClickGetReward(this.OnClickGetPassLevelReward));
			}
		}

		// Token: 0x060064E9 RID: 25833 RVA: 0x00201297 File Offset: 0x001FF497
		private RectTransform GetMissionSlot(int index)
		{
			NKCUIMissionAchieveSlot newInstance = NKCUIMissionAchieveSlot.GetNewInstance(null, "AB_UI_NKM_UI_EVENT_PASS", "NKM_UI_EVENT_PASS_MISSION_SLOT");
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x060064EA RID: 25834 RVA: 0x002012B4 File Offset: 0x001FF4B4
		private void ReturnMissionSlot(Transform tr)
		{
			NKCUIMissionAchieveSlot component = tr.GetComponent<NKCUIMissionAchieveSlot>();
			tr.SetParent(null);
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x060064EB RID: 25835 RVA: 0x002012EC File Offset: 0x001FF4EC
		private void ProvideMissionData(Transform tr, int index)
		{
			NKCUIMissionAchieveSlot component = tr.GetComponent<NKCUIMissionAchieveSlot>();
			if (component == null)
			{
				return;
			}
			if (this.m_missionTempletInfo[this.m_currentMissionType] != null && this.m_missionTempletInfo[this.m_currentMissionType].Count > index)
			{
				NKMMissionTemplet nkmmissionTemplet = this.m_missionTempletInfo[this.m_currentMissionType][index];
				if (this.m_currentMissionType == EventPassMissionType.Daily)
				{
					component.SetData(nkmmissionTemplet, new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickMissionMove), new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickMissionComplete), new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickRefreshDailyMission), null);
					if (this.m_bDailyMissionCompleteAchieved)
					{
						component.SetForceMissionDisabled();
					}
				}
				else
				{
					component.SetData(nkmmissionTemplet, new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickMissionMove), new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickMissionComplete), null, null);
				}
				if (this.CheckMissionRetryEnable(this.m_currentMissionType, nkmmissionTemplet.m_MissionID))
				{
					component.SetForceActivateEventPassRefreshButton();
				}
			}
		}

		// Token: 0x060064EC RID: 25836 RVA: 0x002013D4 File Offset: 0x001FF5D4
		private int GetMissionTabId(EventPassMissionType missionType)
		{
			if (!this.m_missionTempletInfo.ContainsKey(missionType) || this.m_missionTempletInfo[missionType] == null || this.m_missionTempletInfo[missionType].Count <= 0)
			{
				return -1;
			}
			return this.m_missionTempletInfo[missionType][0].m_MissionTabId;
		}

		// Token: 0x060064ED RID: 25837 RVA: 0x0020142C File Offset: 0x001FF62C
		private void OnRewardScrollValueChanged(Vector2 value)
		{
			float num = 1f - 1f / (float)(this.m_LoopRewardScrollRect.TotalCount - this.m_LoopRewardScrollRect.content.transform.childCount);
			if (value.y > num)
			{
				Animator aniFinalReward = this.m_aniFinalReward;
				if (aniFinalReward != null)
				{
					aniFinalReward.ResetTrigger("in");
				}
				Animator aniFinalReward2 = this.m_aniFinalReward;
				if (aniFinalReward2 == null)
				{
					return;
				}
				aniFinalReward2.SetTrigger("out");
				return;
			}
			else
			{
				Animator aniFinalReward3 = this.m_aniFinalReward;
				if (aniFinalReward3 != null)
				{
					aniFinalReward3.ResetTrigger("out");
				}
				Animator aniFinalReward4 = this.m_aniFinalReward;
				if (aniFinalReward4 == null)
				{
					return;
				}
				aniFinalReward4.SetTrigger("in");
				return;
			}
		}

		// Token: 0x060064EE RID: 25838 RVA: 0x002014C8 File Offset: 0x001FF6C8
		private bool IsDailyMissionCompleted(List<NKMEventPassMissionInfo> dailyMissionInfoList)
		{
			if (dailyMissionInfoList == null)
			{
				return false;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return false;
			}
			NKMEventPassTemplet nkmeventPassTemplet = NKMEventPassTemplet.Find(this.m_iEventPassId);
			return nkmeventPassTemplet != null && this.GetCompletedMissionCount(myUserData, EventPassMissionType.Daily) >= (long)nkmeventPassTemplet.DailyMissionClearCount && this.m_finalMissionCompleted[EventPassMissionType.Daily];
		}

		// Token: 0x060064EF RID: 25839 RVA: 0x00201520 File Offset: 0x001FF720
		private void Release()
		{
			NKCUIEventPass.m_NKCEventPassDataManager = null;
			if (this.m_listMissionInfo != null)
			{
				if (this.m_listMissionInfo.ContainsKey(EventPassMissionType.Daily) && this.m_listMissionInfo[EventPassMissionType.Daily] != null)
				{
					this.m_listMissionInfo[EventPassMissionType.Daily].Clear();
					this.m_listMissionInfo[EventPassMissionType.Daily] = null;
				}
				if (this.m_listMissionInfo.ContainsKey(EventPassMissionType.Weekly) && this.m_listMissionInfo[EventPassMissionType.Weekly] != null)
				{
					this.m_listMissionInfo[EventPassMissionType.Weekly].Clear();
					this.m_listMissionInfo[EventPassMissionType.Weekly] = null;
				}
				this.m_listMissionInfo.Clear();
			}
			if (this.m_missionTempletInfo != null)
			{
				foreach (KeyValuePair<EventPassMissionType, List<NKMMissionTemplet>> keyValuePair in this.m_missionTempletInfo)
				{
					if (this.m_missionTempletInfo.Values != null)
					{
						keyValuePair.Value.Clear();
					}
				}
				this.m_missionTempletInfo.Clear();
			}
			if (this.m_tweenerExpGauge != null)
			{
				this.m_tweenerExpGauge.Kill(false);
				this.m_tweenerExpGauge = null;
			}
		}

		// Token: 0x060064F0 RID: 25840 RVA: 0x00201640 File Offset: 0x001FF840
		private static void CleanUpInstance()
		{
			NKCUIEventPass.m_Instance.Release();
			NKCUIEventPass.m_Instance = null;
		}

		// Token: 0x060064F1 RID: 25841 RVA: 0x00201654 File Offset: 0x001FF854
		private int GetTempletDataCount<T>(IEnumerable<T> container)
		{
			int num = 0;
			foreach (T t in container)
			{
				num++;
			}
			return num;
		}

		// Token: 0x060064F2 RID: 25842 RVA: 0x0020169C File Offset: 0x001FF89C
		private void OnClickMissionMove(NKCUIMissionAchieveSlot cNKCUIMissionAchieveSlot)
		{
			if (!NKCUIEventPass.IsEventTime(true))
			{
				return;
			}
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

		// Token: 0x060064F3 RID: 25843 RVA: 0x002016DC File Offset: 0x001FF8DC
		private void OnClickMissionComplete(NKCUIMissionAchieveSlot cNKCUIMissionAchieveSlot)
		{
			if (!NKCUIEventPass.IsEventTime(true))
			{
				return;
			}
			if (cNKCUIMissionAchieveSlot == null)
			{
				return;
			}
			if (this.m_objMissionResetting.activeSelf)
			{
				return;
			}
			NKMMissionTemplet nkmmissionTemplet = cNKCUIMissionAchieveSlot.GetNKMMissionTemplet();
			if (nkmmissionTemplet == null)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_MISSION_COMPLETE_REQ(nkmmissionTemplet.m_MissionTabId, nkmmissionTemplet.m_GroupId, nkmmissionTemplet.m_MissionID);
		}

		// Token: 0x060064F4 RID: 25844 RVA: 0x0020172C File Offset: 0x001FF92C
		private void OnClickMissionCompleteAll()
		{
			if (!NKCUIEventPass.IsEventTime(true))
			{
				return;
			}
			if (this.m_objMissionResetting.activeSelf)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_MISSION_COMPLETE_ALL_REQ(this.GetMissionTabId(this.m_currentMissionType));
		}

		// Token: 0x060064F5 RID: 25845 RVA: 0x00201758 File Offset: 0x001FF958
		private void OnClickRefreshDailyMission(NKCUIMissionAchieveSlot cNKCUIMissionAchieveSlot)
		{
			if (!NKCUIEventPass.IsEventTime(true))
			{
				return;
			}
			if (this.m_objMissionResetting.activeSelf)
			{
				return;
			}
			NKMMissionTemplet missionTemplet = cNKCUIMissionAchieveSlot.GetNKMMissionTemplet();
			if (missionTemplet == null)
			{
				return;
			}
			NKMEventPassMissionInfo nkmeventPassMissionInfo = this.m_listMissionInfo[this.m_currentMissionType].Find((NKMEventPassMissionInfo e) => e.missionId == missionTemplet.m_MissionID);
			if (nkmeventPassMissionInfo == null)
			{
				return;
			}
			int totalMissionRerollCount = NKMEventPassConst.TotalMissionRerollCount;
			StringBuilder stringBuilder = new StringBuilder();
			if (nkmeventPassMissionInfo.retryCount >= totalMissionRerollCount)
			{
				stringBuilder.Append(NKCUtilString.GET_STRING_EVENTPASS_MISSION_REFRESH_MAX_DESC);
				stringBuilder.Append("\n");
				stringBuilder.AppendFormat(NKCUtilString.GET_STRING_EVENTPASS_MISSION_REFRESH_COUNT, nkmeventPassMissionInfo.retryCount, totalMissionRerollCount);
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_EVENTPASS_MISSION_REFRESH, stringBuilder.ToString(), null, "");
				return;
			}
			stringBuilder.Append(NKCUtilString.GET_STRING_EVENTPASS_MISSION_REFRESH_DESC);
			if (NKMMissionManager.GetMissionStateData(missionTemplet).progressCount > 0L)
			{
				stringBuilder.Append("\n");
				stringBuilder.Append(NKCUtilString.GET_STRING_EVENTPASS_MISSION_REFRESH_WARNING_DESC);
			}
			if (nkmeventPassMissionInfo.retryCount < NKMEventPassConst.FreeMissionRerollCount)
			{
				stringBuilder.Append("\n");
				stringBuilder.AppendFormat(NKCUtilString.GET_STRING_EVENTPASS_MISSION_REFRESH_FREECOUNT, nkmeventPassMissionInfo.retryCount, NKMEventPassConst.FreeMissionRerollCount);
				NKCPopupResourceConfirmBox.Instance.OpenForConfirm(NKCUtilString.GET_STRING_EVENTPASS_MISSION_REFRESH, stringBuilder.ToString(), delegate
				{
					NKCPacketSender.Send_cNKMPacket_EVENT_PASS_DAILY_MISSION_RETRY_REQ(missionTemplet.m_MissionID);
				}, null, false);
				return;
			}
			stringBuilder.Append("\n");
			stringBuilder.AppendFormat(NKCUtilString.GET_STRING_EVENTPASS_MISSION_REFRESH_COUNT, nkmeventPassMissionInfo.retryCount - NKMEventPassConst.FreeMissionRerollCount, NKMEventPassConst.PayMissionRerollCount);
			NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_EVENTPASS_MISSION_REFRESH, stringBuilder.ToString(), 1, 20000, delegate()
			{
				NKCPacketSender.Send_cNKMPacket_EVENT_PASS_DAILY_MISSION_RETRY_REQ(missionTemplet.m_MissionID);
			}, null, true);
		}

		// Token: 0x060064F6 RID: 25846 RVA: 0x00201914 File Offset: 0x001FFB14
		private void OnClickGetPassLevelReward()
		{
			if (!NKCUIEventPass.IsEventTime(true))
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_EVENT_PASS_LEVEL_COMPLETE_REQ();
		}

		// Token: 0x060064F7 RID: 25847 RVA: 0x00201924 File Offset: 0x001FFB24
		private void OnClickPassLevelUp()
		{
			if (!NKCUIEventPass.IsEventTime(true))
			{
				return;
			}
			NKMEventPassTemplet nkmeventPassTemplet = NKMTempletContainer<NKMEventPassTemplet>.Find(this.m_iEventPassId);
			if (nkmeventPassTemplet == null)
			{
				return;
			}
			string contentText = NKCUtilString.GET_STRING_EVENTPASS_PASS_LEVEL_UP_DESC.Split(new char[]
			{
				'\n'
			})[0];
			NKCPopupInventoryAdd.SliderInfo sliderInfo = default(NKCPopupInventoryAdd.SliderInfo);
			sliderInfo.increaseCount = 1;
			sliderInfo.maxCount = nkmeventPassTemplet.PassMaxLevel;
			sliderInfo.currentCount = this.m_iUserPassLevel;
			sliderInfo.inventoryType = NKM_INVENTORY_EXPAND_TYPE.NIET_NONE;
			NKCPopupInventoryAdd.Instance.Open(NKCUtilString.GET_STRING_EVENTPASS_PASS_LEVEL_UP_NOTICE, contentText, sliderInfo, nkmeventPassTemplet.PassLevelUpMiscCount, nkmeventPassTemplet.PassLevelUpMiscId, delegate(int value)
			{
				if (!NKCUIEventPass.IsEventTime(true))
				{
					return;
				}
				NKCPacketSender.Send_NKMPacket_EVENT_PASS_LEVEL_UP_REQ(value);
			}, false);
		}

		// Token: 0x060064F8 RID: 25848 RVA: 0x002019D4 File Offset: 0x001FFBD4
		private void OnClickPurchaseCorePass()
		{
			if (!NKCUIEventPass.IsEventTime(true))
			{
				return;
			}
			NKMEventPassTemplet nkmeventPassTemplet = NKMEventPassTemplet.Find(this.m_iEventPassId);
			if (nkmeventPassTemplet == null)
			{
				return;
			}
			bool endTimeNotice = NKCSynchronizedTime.GetTimeLeft(NKCSynchronizedTime.ToUtcTime(nkmeventPassTemplet.EventPassEndDate)).Days <= this.m_iEndWarningRemainDays;
			NKCPopupEventPassPurchase.Instance.Open(endTimeNotice, new NKCPopupEventPassPurchase.EventTimeCheck(NKCUIEventPass.IsEventTime));
		}

		// Token: 0x060064F9 RID: 25849 RVA: 0x00201A35 File Offset: 0x001FFC35
		private void OnToggleRewardPanel(bool value)
		{
			if (value)
			{
				NKCUtil.SetGameobjectActive(this.m_objRewardListPanel, value);
				NKCUtil.SetGameobjectActive(this.m_objMissionListPanel, !value);
				if (!NKCUIEventPass.IsEventTime(true))
				{
					return;
				}
				this.RefreshScrollRect(true);
			}
		}

		// Token: 0x060064FA RID: 25850 RVA: 0x00201A65 File Offset: 0x001FFC65
		private void OnToggleMissionPanel(bool value)
		{
			if (value)
			{
				NKCUtil.SetGameobjectActive(this.m_objRewardListPanel, !value);
				NKCUtil.SetGameobjectActive(this.m_objMissionListPanel, value);
				if (!NKCUIEventPass.IsEventTime(true))
				{
					return;
				}
				this.m_bMissionTabClick = true;
				this.OnClickDailyMission(false);
			}
		}

		// Token: 0x060064FB RID: 25851 RVA: 0x00201A9C File Offset: 0x001FFC9C
		private void OnClickDailyMission(bool bAnim)
		{
			if (bAnim)
			{
				RectTransform rtToggleBar = this.m_rtToggleBar;
				if (rtToggleBar != null)
				{
					rtToggleBar.DOAnchorPos(this.m_vToggleDailyMissionPos, this.m_fToggleMissionTime, false).SetEase(Ease.OutCubic);
				}
			}
			else if (this.m_rtToggleBar != null)
			{
				this.m_rtToggleBar.anchoredPosition = this.m_vToggleDailyMissionPos;
			}
			this.m_currentMissionType = EventPassMissionType.Daily;
			NKCUtil.SetLabelTextColor(this.m_lbDailyTabText, this.m_colActivatedTabColor);
			NKCUtil.SetLabelTextColor(this.m_lbWeeklyTabText, this.m_colDeactivatedTabColor);
			if (!NKCUIEventPass.IsEventTime(true))
			{
				return;
			}
			if (this.m_listMissionInfo[this.m_currentMissionType] == null || NKCSynchronizedTime.IsFinished(this.m_resetMissionTime[this.m_currentMissionType]))
			{
				this.m_dSendMissionRequest = null;
				NKCPacketSender.Send_NKMPacket_EVENT_PASS_MISSION_REQ(EventPassMissionType.Daily);
				return;
			}
			if (this.m_bMissionTabClick)
			{
				this.m_bMissionTabClick = false;
				if (this.IsDailyMissionCompleted(this.m_listMissionInfo[EventPassMissionType.Daily]))
				{
					this.OnClickWeeklyMission(false);
					return;
				}
			}
			this.RefreshScrollRect(true);
		}

		// Token: 0x060064FC RID: 25852 RVA: 0x00201B90 File Offset: 0x001FFD90
		private void OnClickWeeklyMission(bool bAnim)
		{
			if (bAnim)
			{
				RectTransform rtToggleBar = this.m_rtToggleBar;
				if (rtToggleBar != null)
				{
					rtToggleBar.DOAnchorPos(this.m_vToggleWeeklyMissionPos, this.m_fToggleMissionTime, false).SetEase(Ease.OutCubic);
				}
			}
			else if (this.m_rtToggleBar != null)
			{
				this.m_rtToggleBar.anchoredPosition = this.m_vToggleWeeklyMissionPos;
			}
			this.m_currentMissionType = EventPassMissionType.Weekly;
			NKCUtil.SetLabelTextColor(this.m_lbDailyTabText, this.m_colDeactivatedTabColor);
			NKCUtil.SetLabelTextColor(this.m_lbWeeklyTabText, this.m_colActivatedTabColor);
			if (!NKCUIEventPass.IsEventTime(true))
			{
				return;
			}
			if (this.m_listMissionInfo[this.m_currentMissionType] == null || NKCSynchronizedTime.IsFinished(this.m_resetMissionTime[this.m_currentMissionType]))
			{
				this.m_dSendMissionRequest = null;
				NKCPacketSender.Send_NKMPacket_EVENT_PASS_MISSION_REQ(EventPassMissionType.Weekly);
				return;
			}
			this.RefreshScrollRect(true);
		}

		// Token: 0x060064FD RID: 25853 RVA: 0x00201C59 File Offset: 0x001FFE59
		private void OnClickCompleteFinalMission(NKCUISlot.SlotData slotData, bool bLocked)
		{
			if (!NKCUIEventPass.IsEventTime(true))
			{
				return;
			}
			if (this.m_objMissionResetting.activeSelf)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_EVENT_PASS_FINAL_MISSION_COMPLETE_REQ(this.m_currentMissionType);
		}

		// Token: 0x060064FE RID: 25854 RVA: 0x00201C80 File Offset: 0x001FFE80
		private void OnClickEventStage()
		{
			if (this.m_csbtnEventStage != null && this.m_csbtnEventStage.m_bLock)
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_NO_EVENT, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			if (!NKCUIEventPass.IsEventTime(true))
			{
				return;
			}
			NKMEventPassTemplet nkmeventPassTemplet = NKMEventPassTemplet.Find(this.m_iEventPassId);
			if (nkmeventPassTemplet == null)
			{
				return;
			}
			NKCContentManager.MoveToShortCut(nkmeventPassTemplet.m_ShortCutType, nkmeventPassTemplet.m_ShortCut, false);
		}

		// Token: 0x04005052 RID: 20562
		public const string UI_ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_EVENT_PASS";

		// Token: 0x04005053 RID: 20563
		public const string UI_ASSET_NAME = "NKM_UI_EVENT_PASS";

		// Token: 0x04005054 RID: 20564
		private static NKCUIEventPass m_Instance;

		// Token: 0x04005055 RID: 20565
		public Animator m_aniEventPass;

		// Token: 0x04005056 RID: 20566
		[Header("레벨, 경험치 정보")]
		public Text m_lbPassLevel;

		// Token: 0x04005057 RID: 20567
		public Text m_lbPassExp;

		// Token: 0x04005058 RID: 20568
		public Color m_passExpColor;

		// Token: 0x04005059 RID: 20569
		public Image m_imgPassExpGauge;

		// Token: 0x0400505A RID: 20570
		public float m_fExpGaugeTimeMultiplier;

		// Token: 0x0400505B RID: 20571
		public int m_iExpFontSize;

		// Token: 0x0400505C RID: 20572
		public GameObject m_objExpRoot;

		// Token: 0x0400505D RID: 20573
		public GameObject m_objExpMaxLevel;

		// Token: 0x0400505E RID: 20574
		[Header("공통 오브젝트")]
		public Text m_lbPassTime;

		// Token: 0x0400505F RID: 20575
		public Text m_lbPassTitle;

		// Token: 0x04005060 RID: 20576
		public NKCUICharacterView m_characterView;

		// Token: 0x04005061 RID: 20577
		public GameObject m_objEndNotice;

		// Token: 0x04005062 RID: 20578
		public int m_iEndWarningRemainDays;

		// Token: 0x04005063 RID: 20579
		public Text m_lbEndTimeRemain;

		// Token: 0x04005064 RID: 20580
		public Color m_colEndTimeText;

		// Token: 0x04005065 RID: 20581
		public GameObject m_RewardSubMenuRedDot;

		// Token: 0x04005066 RID: 20582
		public GameObject m_MissionSubMenuRedDot;

		// Token: 0x04005067 RID: 20583
		public NKCUIComToggle m_tglRewardSubMenu;

		// Token: 0x04005068 RID: 20584
		public NKCUIComToggle m_tglMissionSubMenu;

		// Token: 0x04005069 RID: 20585
		public NKCUIComStateButton m_csbtnPassLevelUp;

		// Token: 0x0400506A RID: 20586
		public NKCUIComStateButton m_csbtnPurchaseCorePass;

		// Token: 0x0400506B RID: 20587
		public NKCUIComStateButton m_csbtnEventStage;

		// Token: 0x0400506C RID: 20588
		[Header("장비 패스 정보")]
		public NKCUIComEventPassEquip m_eventPassEquip;

		// Token: 0x0400506D RID: 20589
		public GameObject m_objEquipRoot;

		// Token: 0x0400506E RID: 20590
		[Header("보상 패널")]
		public GameObject m_objRewardListPanel;

		// Token: 0x0400506F RID: 20591
		public GameObject m_objRewardRedDot;

		// Token: 0x04005070 RID: 20592
		public LoopScrollRect m_LoopRewardScrollRect;

		// Token: 0x04005071 RID: 20593
		[Header("최대 레벨 보상")]
		public Animator m_aniFinalReward;

		// Token: 0x04005072 RID: 20594
		public NKCUISlot m_finalNormalRewardSlot;

		// Token: 0x04005073 RID: 20595
		public NKCUISlot m_finalCoreRewardSlot;

		// Token: 0x04005074 RID: 20596
		public Text m_lbFinalRewardLevel;

		// Token: 0x04005075 RID: 20597
		public Color m_colFinalRewardFontColor;

		// Token: 0x04005076 RID: 20598
		public int m_iFinalRewardFontSize;

		// Token: 0x04005077 RID: 20599
		[Header("미션 패널")]
		public GameObject m_objMissionListPanel;

		// Token: 0x04005078 RID: 20600
		public LoopScrollRect m_LoopMissionScrollRect;

		// Token: 0x04005079 RID: 20601
		public RectTransform m_rtToggleBar;

		// Token: 0x0400507A RID: 20602
		public Vector2 m_vToggleDailyMissionPos;

		// Token: 0x0400507B RID: 20603
		public Vector2 m_vToggleWeeklyMissionPos;

		// Token: 0x0400507C RID: 20604
		public float m_fToggleMissionTime = 0.4f;

		// Token: 0x0400507D RID: 20605
		public NKCUIComStateButton m_csbtnDailyMission;

		// Token: 0x0400507E RID: 20606
		public NKCUIComStateButton m_csbtnWeeklyMission;

		// Token: 0x0400507F RID: 20607
		public NKCUIComStateButton m_csbtnCompleteAllMission;

		// Token: 0x04005080 RID: 20608
		public GameObject m_objMissionRedDot;

		// Token: 0x04005081 RID: 20609
		public GameObject m_objDailyMissionRedDot;

		// Token: 0x04005082 RID: 20610
		public GameObject m_objWeeklyMissionRedDot;

		// Token: 0x04005083 RID: 20611
		public GameObject m_objPassMissionComplete;

		// Token: 0x04005084 RID: 20612
		public Text m_lbPassMissionCompleteText;

		// Token: 0x04005085 RID: 20613
		public Text m_lbPassMissionCompleteExText;

		// Token: 0x04005086 RID: 20614
		public GameObject m_objLevelFullMissionDisable;

		// Token: 0x04005087 RID: 20615
		public Text m_lbWeekCount;

		// Token: 0x04005088 RID: 20616
		public Color m_colDailyCountColor;

		// Token: 0x04005089 RID: 20617
		public Color m_colWeeklyCountColor;

		// Token: 0x0400508A RID: 20618
		public Text m_lbRefreshTimeRemain;

		// Token: 0x0400508B RID: 20619
		public Text m_lbDailyTabText;

		// Token: 0x0400508C RID: 20620
		public Text m_lbWeeklyTabText;

		// Token: 0x0400508D RID: 20621
		public Color m_colActivatedTabColor;

		// Token: 0x0400508E RID: 20622
		public Color m_colDeactivatedTabColor;

		// Token: 0x0400508F RID: 20623
		public GameObject m_objMissionResetting;

		// Token: 0x04005090 RID: 20624
		public Text m_lbMissionResetMsg;

		// Token: 0x04005091 RID: 20625
		[Header("미션 달성도")]
		public GameObject m_objDailyAchieveBG;

		// Token: 0x04005092 RID: 20626
		public GameObject m_objWeeklyAchieveBG;

		// Token: 0x04005093 RID: 20627
		public GameObject m_objAchieveSlotEffect;

		// Token: 0x04005094 RID: 20628
		public NKCUISlot m_achieveSlot;

		// Token: 0x04005095 RID: 20629
		public Text m_lbAchieveTitle;

		// Token: 0x04005096 RID: 20630
		public Text m_lbAchieveTimeRemain;

		// Token: 0x04005097 RID: 20631
		public Image m_imgAchieveGauge;

		// Token: 0x04005098 RID: 20632
		public Text m_lbAchieveCount;

		// Token: 0x04005099 RID: 20633
		private static bool m_bOpenUIStandby;

		// Token: 0x0400509A RID: 20634
		private static bool m_bRewardDot;

		// Token: 0x0400509B RID: 20635
		private static bool m_bDailyMissionDot;

		// Token: 0x0400509C RID: 20636
		private static bool m_bWeeklyMissionDot;

		// Token: 0x0400509D RID: 20637
		private List<int> m_listUpsideMenuResource = new List<int>
		{
			1,
			2,
			101,
			102
		};

		// Token: 0x0400509E RID: 20638
		private Dictionary<EventPassMissionType, List<NKMEventPassMissionInfo>> m_listMissionInfo = new Dictionary<EventPassMissionType, List<NKMEventPassMissionInfo>>();

		// Token: 0x0400509F RID: 20639
		private Dictionary<EventPassMissionType, List<NKMMissionTemplet>> m_missionTempletInfo = new Dictionary<EventPassMissionType, List<NKMMissionTemplet>>();

		// Token: 0x040050A0 RID: 20640
		private Dictionary<EventPassMissionType, DateTime> m_resetMissionTime = new Dictionary<EventPassMissionType, DateTime>();

		// Token: 0x040050A1 RID: 20641
		private Dictionary<EventPassMissionType, bool> m_finalMissionCompleted = new Dictionary<EventPassMissionType, bool>();

		// Token: 0x040050A2 RID: 20642
		private EventPassMissionType m_currentMissionType;

		// Token: 0x040050A3 RID: 20643
		private Tweener m_tweenerExpGauge;

		// Token: 0x040050A4 RID: 20644
		private static NKCEventPassDataManager m_NKCEventPassDataManager;

		// Token: 0x040050A5 RID: 20645
		private int m_iUserPassLevel;

		// Token: 0x040050A6 RID: 20646
		private int m_iEventPassId;

		// Token: 0x040050A7 RID: 20647
		private const int MissionResetTime = 5;

		// Token: 0x040050A8 RID: 20648
		private float m_fUpdateTime;

		// Token: 0x040050A9 RID: 20649
		private const float UpdateInterval = 1f;

		// Token: 0x040050AA RID: 20650
		private bool m_bDailyMissionCompleteAchieved;

		// Token: 0x040050AB RID: 20651
		private bool m_bMissionTabClick;

		// Token: 0x040050AC RID: 20652
		private NKCUIEventPass.SendMissionRequest m_dSendMissionRequest;

		// Token: 0x040050AD RID: 20653
		public static NKCUIEventPass.OnResetMissionTime m_dOnMissionUpdate;

		// Token: 0x02001649 RID: 5705
		public enum ExpFXType
		{
			// Token: 0x0400A3FB RID: 41979
			NONE,
			// Token: 0x0400A3FC RID: 41980
			DIRECT,
			// Token: 0x0400A3FD RID: 41981
			TRANS
		}

		// Token: 0x0200164A RID: 5706
		public enum ExpGainType
		{
			// Token: 0x0400A3FF RID: 41983
			LevelUp,
			// Token: 0x0400A400 RID: 41984
			Gain
		}

		// Token: 0x0200164B RID: 5707
		// (Invoke) Token: 0x0600AFDB RID: 45019
		private delegate void SendMissionRequest(EventPassMissionType missionType);

		// Token: 0x0200164C RID: 5708
		// (Invoke) Token: 0x0600AFDF RID: 45023
		public delegate void OnResetMissionTime();
	}
}
