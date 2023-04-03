using System;
using System.Collections.Generic;
using NKC.Templet;
using NKM;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BCB RID: 3019
	public class NKCPopupEventRaceMission : NKCUIBase
	{
		// Token: 0x17001657 RID: 5719
		// (get) Token: 0x06008BCC RID: 35788 RVA: 0x002F91E4 File Offset: 0x002F73E4
		public static NKCPopupEventRaceMission Instance
		{
			get
			{
				if (NKCPopupEventRaceMission.m_Instance == null)
				{
					NKCPopupEventRaceMission.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupEventRaceMission>("AB_UI_NKM_UI_EVENT_PF_RACE", "NKM_UI_POPUP_EVENT_RACE_MISSION", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupEventRaceMission.CleanupInstance)).GetInstance<NKCPopupEventRaceMission>();
					NKCPopupEventRaceMission.m_Instance.InitUI();
				}
				return NKCPopupEventRaceMission.m_Instance;
			}
		}

		// Token: 0x17001658 RID: 5720
		// (get) Token: 0x06008BCD RID: 35789 RVA: 0x002F9233 File Offset: 0x002F7433
		public static bool HasInstance
		{
			get
			{
				return NKCPopupEventRaceMission.m_Instance != null;
			}
		}

		// Token: 0x17001659 RID: 5721
		// (get) Token: 0x06008BCE RID: 35790 RVA: 0x002F9240 File Offset: 0x002F7440
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupEventRaceMission.m_Instance != null && NKCPopupEventRaceMission.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008BCF RID: 35791 RVA: 0x002F925B File Offset: 0x002F745B
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupEventRaceMission.m_Instance != null && NKCPopupEventRaceMission.m_Instance.IsOpen)
			{
				NKCPopupEventRaceMission.m_Instance.Close();
			}
		}

		// Token: 0x06008BD0 RID: 35792 RVA: 0x002F9280 File Offset: 0x002F7480
		private static void CleanupInstance()
		{
			NKCPopupEventRaceMission.m_Instance = null;
		}

		// Token: 0x1700165A RID: 5722
		// (get) Token: 0x06008BD1 RID: 35793 RVA: 0x002F9288 File Offset: 0x002F7488
		public override string MenuName
		{
			get
			{
				return "RACE MISSION";
			}
		}

		// Token: 0x1700165B RID: 5723
		// (get) Token: 0x06008BD2 RID: 35794 RVA: 0x002F928F File Offset: 0x002F748F
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x06008BD3 RID: 35795 RVA: 0x002F9294 File Offset: 0x002F7494
		public void InitUI()
		{
			if (this.m_eventTriggerBg != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(delegate(BaseEventData eventData)
				{
					NKCPopupEventRaceMission.CheckInstanceAndClose();
				});
				this.m_eventTriggerBg.triggers.Add(entry);
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnClose, new UnityAction(base.Close));
			NKCUtil.SetScrollHotKey(this.m_scrollRect, null);
			this.m_missionSlotList.Clear();
			if (this.m_scrollRect != null)
			{
				int childCount = this.m_scrollRect.content.childCount;
				for (int i = 0; i < childCount; i++)
				{
					NKCUIMissionAchieveSlot component = this.m_scrollRect.content.GetChild(i).GetComponent<NKCUIMissionAchieveSlot>();
					component.Init();
					this.m_missionSlotList.Add(component);
				}
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x06008BD4 RID: 35796 RVA: 0x002F9385 File Offset: 0x002F7585
		public void Open(int eventId)
		{
			base.gameObject.SetActive(true);
			base.UIOpened(true);
		}

		// Token: 0x06008BD5 RID: 35797 RVA: 0x002F939C File Offset: 0x002F759C
		public bool GetMissionRedDotState(int eventId)
		{
			this.SetMissionData(eventId);
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			int count = this.m_missionSlotList.Count;
			int num = 0;
			while (num < count && this.m_missionSlotList[num].IsActive())
			{
				NKMMissionTemplet nkmmissionTemplet = this.m_missionSlotList[num].GetNKMMissionTemplet();
				NKMMissionData nkmmissionData = this.m_missionSlotList[num].GetNKMMissionData();
				if (NKMMissionManager.CanComplete(nkmmissionTemplet, myUserData, nkmmissionData) == NKM_ERROR_CODE.NEC_OK)
				{
					return true;
				}
				num++;
			}
			return false;
		}

		// Token: 0x06008BD6 RID: 35798 RVA: 0x002F9418 File Offset: 0x002F7618
		private void SetMissionData(int eventId)
		{
			NKCEventMissionTemplet nkceventMissionTemplet = NKCEventMissionTemplet.Find(eventId);
			if (nkceventMissionTemplet == null)
			{
				return;
			}
			int count = nkceventMissionTemplet.m_lstMissionTab.Count;
			for (int i = 0; i < count; i++)
			{
				List<NKMMissionTemplet> missionTempletListByType = NKMMissionManager.GetMissionTempletListByType(nkceventMissionTemplet.m_lstMissionTab[i]);
				int count2 = this.m_missionSlotList.Count;
				int count3 = missionTempletListByType.Count;
				for (int j = 0; j < count2; j++)
				{
					if (count3 <= j)
					{
						this.m_missionSlotList[j].SetActive(false);
					}
					else
					{
						this.m_missionSlotList[j].SetData(missionTempletListByType[j], new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickMissionMove), new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickMissionComplete), null, null);
						this.m_missionSlotList[j].SetActive(true);
					}
				}
				if (count2 < count3)
				{
					for (int k = count2; k < count3; k++)
					{
						NKCUIMissionAchieveSlot newInstance = NKCUIMissionAchieveSlot.GetNewInstance(this.m_scrollRect.content, "AB_UI_NKM_UI_EVENT_PF_RACE", "NKM_UI_POPUP_EVENT_RACE_MISSION_SLOT");
						if (newInstance != null)
						{
							this.m_missionSlotList.Add(newInstance);
							newInstance.Init();
							this.m_missionSlotList[k].SetData(missionTempletListByType[k], new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickMissionMove), new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickMissionComplete), null, null);
							newInstance.SetData(missionTempletListByType[k], new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickMissionMove), new NKCUIMissionAchieveSlot.OnClickMASlot(this.OnClickMissionComplete), null, null);
						}
					}
				}
			}
		}

		// Token: 0x06008BD7 RID: 35799 RVA: 0x002F95A0 File Offset: 0x002F77A0
		private void OnClickMissionMove(NKCUIMissionAchieveSlot cNKCUIMissionAchieveSlot)
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

		// Token: 0x06008BD8 RID: 35800 RVA: 0x002F95D4 File Offset: 0x002F77D4
		private void OnClickMissionComplete(NKCUIMissionAchieveSlot cNKCUIMissionAchieveSlot)
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
			NKCPacketSender.Send_NKMPacket_MISSION_COMPLETE_REQ(nkmmissionTemplet.m_MissionTabId, nkmmissionTemplet.m_GroupId, nkmmissionTemplet.m_MissionID);
		}

		// Token: 0x06008BD9 RID: 35801 RVA: 0x002F960D File Offset: 0x002F780D
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06008BDA RID: 35802 RVA: 0x002F961B File Offset: 0x002F781B
		private void OnDestroy()
		{
			this.m_scrollRect = null;
			this.m_eventTriggerBg = null;
			this.m_csbtnClose = null;
			if (this.m_missionSlotList != null)
			{
				this.m_missionSlotList.Clear();
				this.m_missionSlotList = null;
			}
		}

		// Token: 0x040078B4 RID: 30900
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_EVENT_PF_RACE";

		// Token: 0x040078B5 RID: 30901
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_EVENT_RACE_MISSION";

		// Token: 0x040078B6 RID: 30902
		private static NKCPopupEventRaceMission m_Instance;

		// Token: 0x040078B7 RID: 30903
		public ScrollRect m_scrollRect;

		// Token: 0x040078B8 RID: 30904
		public EventTrigger m_eventTriggerBg;

		// Token: 0x040078B9 RID: 30905
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x040078BA RID: 30906
		private List<NKCUIMissionAchieveSlot> m_missionSlotList = new List<NKCUIMissionAchieveSlot>();
	}
}
