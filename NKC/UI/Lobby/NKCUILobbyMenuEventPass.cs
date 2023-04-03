using System;
using NKM;
using NKM.EventPass;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Lobby
{
	// Token: 0x02000C10 RID: 3088
	public class NKCUILobbyMenuEventPass : NKCUILobbyMenuButtonBase
	{
		// Token: 0x06008EE8 RID: 36584 RVA: 0x00309C78 File Offset: 0x00307E78
		public void Init(ContentsType contentsType = ContentsType.None)
		{
			if (this.m_csbtnMenu != null)
			{
				this.m_csbtnMenu.PointerClick.RemoveAllListeners();
				this.m_csbtnMenu.PointerClick.AddListener(new UnityAction(this.OnButton));
			}
			this.m_ContentsType = contentsType;
			this.InitTime();
		}

		// Token: 0x06008EE9 RID: 36585 RVA: 0x00309CCC File Offset: 0x00307ECC
		private void InitTime()
		{
			if (!NKCUILobbyMenuEventPass.initDateTime)
			{
				NKCUILobbyMenuEventPass.initUTCTime = NKCSynchronizedTime.GetServerUTCTime(0.0);
				NKCUILobbyMenuEventPass.initDateTime = true;
			}
			NKCUIEventPass.m_dOnMissionUpdate = new NKCUIEventPass.OnResetMissionTime(this.CheckResetMissionTime);
		}

		// Token: 0x06008EEA RID: 36586 RVA: 0x00309D00 File Offset: 0x00307F00
		private void CheckResetMissionTime()
		{
			if (NKCSynchronizedTime.IsFinished(NKMTime.GetNextResetTime(NKCUILobbyMenuEventPass.initUTCTime, NKMTime.TimePeriod.Day)))
			{
				NKCUIEventPass.DailyMissionRedDot = false;
				NKCUILobbyMenuEventPass.initUTCTime = NKCSynchronizedTime.GetServerUTCTime(0.0);
			}
			if (NKCSynchronizedTime.IsFinished(NKMTime.GetNextResetTime(NKCUILobbyMenuEventPass.initUTCTime, NKMTime.TimePeriod.Week)))
			{
				NKCUIEventPass.WeeklyMissionRedDot = false;
				NKCUILobbyMenuEventPass.initUTCTime = NKCSynchronizedTime.GetServerUTCTime(0.0);
			}
		}

		// Token: 0x06008EEB RID: 36587 RVA: 0x00309D63 File Offset: 0x00307F63
		public void Update()
		{
			this.UpdateEventPassEnabled();
			this.UpdateLeftTime();
		}

		// Token: 0x06008EEC RID: 36588 RVA: 0x00309D74 File Offset: 0x00307F74
		public void UpdateEventPassEnabled()
		{
			this.updateTimer -= Time.deltaTime;
			if (this.updateTimer <= 0f)
			{
				this.CheckButtonEnable();
				this.CheckResetMissionTime();
				if (base.gameObject.activeSelf && this.m_EventPassTemplet == null)
				{
					this.CheckPassInfo();
				}
				this.updateTimer = 1f;
			}
		}

		// Token: 0x06008EED RID: 36589 RVA: 0x00309DD2 File Offset: 0x00307FD2
		protected override void ContentsUpdate(NKMUserData userData)
		{
			this.UpdateContents(userData);
		}

		// Token: 0x06008EEE RID: 36590 RVA: 0x00309DDB File Offset: 0x00307FDB
		public void UpdateContents(NKMUserData userData)
		{
			this.CheckButtonEnable();
			this.CheckResetMissionTime();
			this.CheckPassInfo();
		}

		// Token: 0x06008EEF RID: 36591 RVA: 0x00309DF0 File Offset: 0x00307FF0
		private void CheckPassInfo()
		{
			NKCEventPassDataManager eventPassDataManager = NKCScenManager.GetScenManager().GetEventPassDataManager();
			if (eventPassDataManager == null)
			{
				return;
			}
			NKMEventPassTemplet nkmeventPassTemplet = NKMTempletContainer<NKMEventPassTemplet>.Find(eventPassDataManager.EventPassId);
			if (nkmeventPassTemplet == null)
			{
				return;
			}
			this.m_EventPassTemplet = nkmeventPassTemplet;
			this.UpdatePassExpAndLv();
			this.UpdatePassIcon();
			this.UpdateLeftTime();
		}

		// Token: 0x06008EF0 RID: 36592 RVA: 0x00309E35 File Offset: 0x00308035
		protected override void UpdateLock()
		{
			this.m_bLocked = !NKCContentManager.IsContentsUnlocked(this.m_ContentsType, 0, 0);
			this.UpdateLock(this.m_bLocked);
		}

		// Token: 0x06008EF1 RID: 36593 RVA: 0x00309E59 File Offset: 0x00308059
		public void UpdateLock(bool bLock)
		{
			NKCUtil.SetLabelText(this.m_lbLock, NKCContentManager.GetLockedMessage(this.m_ContentsType, 0));
			NKCUtil.SetGameobjectActive(this.m_objLock, bLock);
			this.m_bLocked = bLock;
			if (!this.m_bLocked)
			{
				this.UpdateContents(NKCScenManager.CurrentUserData());
			}
		}

		// Token: 0x06008EF2 RID: 36594 RVA: 0x00309E98 File Offset: 0x00308098
		private void CheckButtonEnable()
		{
			bool flag = NKCUIEventPass.IsEventTime(false);
			NKCUtil.SetGameobjectActive(this.m_objRoot, flag);
			NKCUtil.SetGameobjectActive(this.m_objEmpty, !flag);
			if (flag)
			{
				bool bValue = NKCUIEventPass.RewardRedDot || NKCUIEventPass.DailyMissionRedDot || NKCUIEventPass.WeeklyMissionRedDot;
				NKCUtil.SetGameobjectActive(this.m_objNotify, bValue);
			}
		}

		// Token: 0x06008EF3 RID: 36595 RVA: 0x00309EF0 File Offset: 0x003080F0
		private void OnButton()
		{
			if (this.m_bLocked)
			{
				NKCContentManager.ShowLockedMessagePopup(this.m_ContentsType, 0);
				return;
			}
			if (!NKCUIEventPass.IsEventTime(true))
			{
				return;
			}
			NKCEventPassDataManager eventPassDataManager = NKCScenManager.GetScenManager().GetEventPassDataManager();
			if (eventPassDataManager == null)
			{
				return;
			}
			if (!eventPassDataManager.EventPassDataReceived)
			{
				NKCUIEventPass.OpenUIStandby = true;
				NKCUIEventPass.EventPassDataManager = null;
				NKCPacketSender.Send_NKMPacket_EVENT_PASS_REQ(NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL);
				return;
			}
			NKCUIEventPass.Instance.Open(eventPassDataManager);
		}

		// Token: 0x06008EF4 RID: 36596 RVA: 0x00309F54 File Offset: 0x00308154
		private void UpdatePassIcon()
		{
			if (this.m_EventPassTemplet == null)
			{
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_EventPassTemplet.EventPassMainReward);
			if (unitTempletBase != null)
			{
				NKCUtil.SetImageSprite(this.m_imgCounterPass, NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, unitTempletBase), true);
			}
		}

		// Token: 0x06008EF5 RID: 36597 RVA: 0x00309F94 File Offset: 0x00308194
		private void UpdatePassExpAndLv()
		{
			NKCEventPassDataManager eventPassDataManager = NKCScenManager.GetScenManager().GetEventPassDataManager();
			if (eventPassDataManager == null)
			{
				return;
			}
			if (this.m_EventPassTemplet == null)
			{
				return;
			}
			int totalExp = eventPassDataManager.TotalExp;
			int passLevelUpExp = this.m_EventPassTemplet.PassLevelUpExp;
			int passMaxLevel = this.m_EventPassTemplet.PassMaxLevel;
			int num = totalExp % passLevelUpExp;
			int num2 = Mathf.Min(passMaxLevel, totalExp / passLevelUpExp + 1);
			NKCUtil.SetLabelText(this.m_lbCounterPassLv, num2.ToString());
			if (num2 >= passMaxLevel)
			{
				num = passLevelUpExp;
			}
			NKCUtil.SetImageFillAmount(this.m_imgCounterGauge, (float)num / (float)passLevelUpExp);
		}

		// Token: 0x06008EF6 RID: 36598 RVA: 0x0030A018 File Offset: 0x00308218
		public void UpdateLeftTime()
		{
			if (this.m_EventPassTemplet == null)
			{
				return;
			}
			if (NKCSynchronizedTime.IsFinished(this.m_EventPassTemplet.EventPassEndDate))
			{
				this.m_EventPassTemplet = null;
				return;
			}
			TimeSpan timeLeft = NKCSynchronizedTime.GetTimeLeft(NKCSynchronizedTime.ToUtcTime(this.m_EventPassTemplet.EventPassEndDate));
			if (timeLeft.Ticks <= 0L)
			{
				this.m_EventPassTemplet = null;
				return;
			}
			string remainTimeString = NKCUtilString.GetRemainTimeString(timeLeft, 1, true);
			NKCUtil.SetLabelText(this.m_lbLeftTime, remainTimeString);
		}

		// Token: 0x04007BF0 RID: 31728
		public NKCUIComStateButton m_csbtnMenu;

		// Token: 0x04007BF1 RID: 31729
		public GameObject m_objRoot;

		// Token: 0x04007BF2 RID: 31730
		public GameObject m_objNotify;

		// Token: 0x04007BF3 RID: 31731
		public Image m_imgCounterPass;

		// Token: 0x04007BF4 RID: 31732
		public Image m_imgCounterGauge;

		// Token: 0x04007BF5 RID: 31733
		public Text m_lbLeftTime;

		// Token: 0x04007BF6 RID: 31734
		public Text m_lbCounterPassLv;

		// Token: 0x04007BF7 RID: 31735
		public GameObject m_objEmpty;

		// Token: 0x04007BF8 RID: 31736
		private NKMEventPassTemplet m_EventPassTemplet;

		// Token: 0x04007BF9 RID: 31737
		private float updateTimer = -1f;

		// Token: 0x04007BFA RID: 31738
		private static DateTime initUTCTime;

		// Token: 0x04007BFB RID: 31739
		private static bool initDateTime;
	}
}
