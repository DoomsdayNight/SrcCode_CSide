using System;
using NKM.Templet;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC.UI.Event
{
	// Token: 0x02000BCD RID: 3021
	public class NKCPopupEventRaceReward : NKCUIBase
	{
		// Token: 0x17001660 RID: 5728
		// (get) Token: 0x06008BE8 RID: 35816 RVA: 0x002F99CC File Offset: 0x002F7BCC
		public static NKCPopupEventRaceReward Instance
		{
			get
			{
				if (NKCPopupEventRaceReward.m_Instance == null)
				{
					NKCPopupEventRaceReward.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupEventRaceReward>("AB_UI_NKM_UI_EVENT_PF_RACE", "NKM_UI_POPUP_EVENT_RACE_REWARD", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupEventRaceReward.CleanupInstance)).GetInstance<NKCPopupEventRaceReward>();
					NKCPopupEventRaceReward.m_Instance.InitUI();
				}
				return NKCPopupEventRaceReward.m_Instance;
			}
		}

		// Token: 0x17001661 RID: 5729
		// (get) Token: 0x06008BE9 RID: 35817 RVA: 0x002F9A1B File Offset: 0x002F7C1B
		public static bool HasInstance
		{
			get
			{
				return NKCPopupEventRaceReward.m_Instance != null;
			}
		}

		// Token: 0x17001662 RID: 5730
		// (get) Token: 0x06008BEA RID: 35818 RVA: 0x002F9A28 File Offset: 0x002F7C28
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupEventRaceReward.m_Instance != null && NKCPopupEventRaceReward.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008BEB RID: 35819 RVA: 0x002F9A43 File Offset: 0x002F7C43
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupEventRaceReward.m_Instance != null && NKCPopupEventRaceReward.m_Instance.IsOpen)
			{
				NKCPopupEventRaceReward.m_Instance.Close();
			}
		}

		// Token: 0x06008BEC RID: 35820 RVA: 0x002F9A68 File Offset: 0x002F7C68
		private static void CleanupInstance()
		{
			NKCPopupEventRaceReward.m_Instance = null;
		}

		// Token: 0x17001663 RID: 5731
		// (get) Token: 0x06008BED RID: 35821 RVA: 0x002F9A70 File Offset: 0x002F7C70
		public override string MenuName
		{
			get
			{
				return "RACE REWARD";
			}
		}

		// Token: 0x17001664 RID: 5732
		// (get) Token: 0x06008BEE RID: 35822 RVA: 0x002F9A77 File Offset: 0x002F7C77
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x06008BEF RID: 35823 RVA: 0x002F9A7C File Offset: 0x002F7C7C
		public void InitUI()
		{
			this.m_raceWinRewardIcon.Init();
			this.m_raceLoseRewardIcon.Init();
			this.m_teamWinRewardIcon.Init();
			this.m_teamLoseRewardIcon.Init();
			NKCUtil.SetButtonClickDelegate(this.m_csbtnClose, new UnityAction(base.Close));
			if (this.m_eventTriggerBg != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(delegate(BaseEventData eventData)
				{
					NKCPopupEventRaceReward.CheckInstanceAndClose();
				});
				this.m_eventTriggerBg.triggers.Add(entry);
			}
		}

		// Token: 0x06008BF0 RID: 35824 RVA: 0x002F9B24 File Offset: 0x002F7D24
		public void Open(int eventId)
		{
			NKMEventRaceTemplet nkmeventRaceTemplet = NKMEventRaceTemplet.Find(eventId);
			if (!this.dataSet && nkmeventRaceTemplet != null)
			{
				NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(nkmeventRaceTemplet.RaceWinRewardType, nkmeventRaceTemplet.RaceWinRewardItemId, (int)nkmeventRaceTemplet.RaceWinRewardItemValue, 0);
				NKCUISlot raceWinRewardIcon = this.m_raceWinRewardIcon;
				if (raceWinRewardIcon != null)
				{
					raceWinRewardIcon.SetData(data, true, null);
				}
				NKCUISlot.SlotData data2 = NKCUISlot.SlotData.MakeRewardTypeData(nkmeventRaceTemplet.RaceLoseRewardType, nkmeventRaceTemplet.RaceLoseRewardItemId, (int)nkmeventRaceTemplet.RaceLoseRewardItemValue, 0);
				NKCUISlot raceLoseRewardIcon = this.m_raceLoseRewardIcon;
				if (raceLoseRewardIcon != null)
				{
					raceLoseRewardIcon.SetData(data2, true, null);
				}
				NKCUISlot.SlotData data3 = NKCUISlot.SlotData.MakeRewardTypeData(nkmeventRaceTemplet.TeamWinRewardType, nkmeventRaceTemplet.TeamWinRewardItemId, (int)nkmeventRaceTemplet.TeamWinRewardItemValue, 0);
				NKCUISlot teamWinRewardIcon = this.m_teamWinRewardIcon;
				if (teamWinRewardIcon != null)
				{
					teamWinRewardIcon.SetData(data3, true, null);
				}
				NKCUISlot.SlotData data4 = NKCUISlot.SlotData.MakeRewardTypeData(nkmeventRaceTemplet.TeamLoseRewardType, nkmeventRaceTemplet.TeamLoseRewardItemId, (int)nkmeventRaceTemplet.TeamLoseRewardItemValue, 0);
				NKCUISlot teamLoseRewardIcon = this.m_teamLoseRewardIcon;
				if (teamLoseRewardIcon != null)
				{
					teamLoseRewardIcon.SetData(data4, true, null);
				}
				this.dataSet = true;
			}
			base.gameObject.SetActive(true);
			base.UIOpened(true);
		}

		// Token: 0x06008BF1 RID: 35825 RVA: 0x002F9C1D File Offset: 0x002F7E1D
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06008BF2 RID: 35826 RVA: 0x002F9C2B File Offset: 0x002F7E2B
		private void OnDestroy()
		{
			this.m_raceWinRewardIcon = null;
			this.m_raceLoseRewardIcon = null;
			this.m_teamWinRewardIcon = null;
			this.m_teamLoseRewardIcon = null;
			this.m_eventTriggerBg = null;
			this.m_csbtnClose = null;
		}

		// Token: 0x040078CB RID: 30923
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_EVENT_PF_RACE";

		// Token: 0x040078CC RID: 30924
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_EVENT_RACE_REWARD";

		// Token: 0x040078CD RID: 30925
		private static NKCPopupEventRaceReward m_Instance;

		// Token: 0x040078CE RID: 30926
		public NKCUISlot m_raceWinRewardIcon;

		// Token: 0x040078CF RID: 30927
		public NKCUISlot m_raceLoseRewardIcon;

		// Token: 0x040078D0 RID: 30928
		public NKCUISlot m_teamWinRewardIcon;

		// Token: 0x040078D1 RID: 30929
		public NKCUISlot m_teamLoseRewardIcon;

		// Token: 0x040078D2 RID: 30930
		public EventTrigger m_eventTriggerBg;

		// Token: 0x040078D3 RID: 30931
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x040078D4 RID: 30932
		private bool dataSet;
	}
}
