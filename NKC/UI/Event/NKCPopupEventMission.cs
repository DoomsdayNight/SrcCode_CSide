using System;
using System.Collections.Generic;
using ClientPacket.User;
using NKM;
using NKM.Event;
using UnityEngine.Events;

namespace NKC.UI.Event
{
	// Token: 0x02000BC6 RID: 3014
	public class NKCPopupEventMission : NKCUIBase
	{
		// Token: 0x17001649 RID: 5705
		// (get) Token: 0x06008B75 RID: 35701 RVA: 0x002F69B4 File Offset: 0x002F4BB4
		public static NKCPopupEventMission Instance
		{
			get
			{
				if (NKCPopupEventMission.m_Instance == null)
				{
					NKCPopupEventMission.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupEventMission>("AB_UI_NKM_UI_EVENT", "NKM_UI_POPUP_EVENT_BINGO_SPECIALMISSION", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupEventMission.CleanupInstance)).GetInstance<NKCPopupEventMission>();
					NKCPopupEventMission.m_Instance.Init();
				}
				return NKCPopupEventMission.m_Instance;
			}
		}

		// Token: 0x06008B76 RID: 35702 RVA: 0x002F6A03 File Offset: 0x002F4C03
		private static void CleanupInstance()
		{
			NKCPopupEventMission.m_Instance = null;
		}

		// Token: 0x1700164A RID: 5706
		// (get) Token: 0x06008B77 RID: 35703 RVA: 0x002F6A0B File Offset: 0x002F4C0B
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupEventMission.m_Instance != null && NKCPopupEventMission.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008B78 RID: 35704 RVA: 0x002F6A26 File Offset: 0x002F4C26
		public static void PreloadInstance()
		{
			if (NKCPopupEventMission.m_Instance == null)
			{
				NKCUtil.SetGameobjectActive(NKCPopupEventMission.Instance, false);
			}
		}

		// Token: 0x06008B79 RID: 35705 RVA: 0x002F6A40 File Offset: 0x002F4C40
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupEventMission.m_Instance != null && NKCPopupEventMission.m_Instance.IsOpen)
			{
				NKCPopupEventMission.m_Instance.Close();
			}
		}

		// Token: 0x06008B7A RID: 35706 RVA: 0x002F6A65 File Offset: 0x002F4C65
		private void OnDestroy()
		{
			NKCPopupEventMission.m_Instance = null;
		}

		// Token: 0x1700164B RID: 5707
		// (get) Token: 0x06008B7B RID: 35707 RVA: 0x002F6A6D File Offset: 0x002F4C6D
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700164C RID: 5708
		// (get) Token: 0x06008B7C RID: 35708 RVA: 0x002F6A70 File Offset: 0x002F4C70
		public override string MenuName
		{
			get
			{
				return "Spacial Mission";
			}
		}

		// Token: 0x06008B7D RID: 35709 RVA: 0x002F6A77 File Offset: 0x002F4C77
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06008B7E RID: 35710 RVA: 0x002F6A88 File Offset: 0x002F4C88
		private void Init()
		{
			if (this.m_btnBack != null)
			{
				this.m_btnBack.PointerClick.RemoveAllListeners();
				this.m_btnBack.PointerClick.AddListener(new UnityAction(base.Close));
			}
			if (this.m_btnClose != null)
			{
				this.m_btnClose.PointerClick.RemoveAllListeners();
				this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			}
			for (int i = 0; i < this.m_listSlot.Count; i++)
			{
				this.m_listSlot[i].Init(new NKCPopupEventMissionSlot.OnTouchProgress(this.OnTouchProgress), new NKCPopupEventMissionSlot.OnTouchComplete(this.OnTouchComplete));
			}
		}

		// Token: 0x06008B7F RID: 35711 RVA: 0x002F6B48 File Offset: 0x002F4D48
		public void Open(NKMEventBingoTemplet bingoTemplet, NKCPopupEventMission.CheckTime checkEventTime, NKCPopupEventMission.OnComplete onComplete)
		{
			this.m_bingoTemplet = bingoTemplet;
			this.dCheckTime = checkEventTime;
			this.dOnComplete = onComplete;
			this.SetData(bingoTemplet);
			base.UIOpened(true);
		}

		// Token: 0x06008B80 RID: 35712 RVA: 0x002F6B6D File Offset: 0x002F4D6D
		public void Refresh()
		{
			this.SetData(this.m_bingoTemplet);
		}

		// Token: 0x06008B81 RID: 35713 RVA: 0x002F6B7C File Offset: 0x002F4D7C
		private void SetData(NKMEventBingoTemplet bingoTemplet)
		{
			List<NKMMissionTemplet> missionTempletListByType = NKMMissionManager.GetMissionTempletListByType(bingoTemplet.m_BingoMissionTabId);
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			for (int i = 0; i < this.m_listSlot.Count; i++)
			{
				NKCPopupEventMissionSlot nkcpopupEventMissionSlot = this.m_listSlot[i];
				if (i < missionTempletListByType.Count)
				{
					nkcpopupEventMissionSlot.SetData(missionTempletListByType[i], nkmuserData.m_MissionData.GetMissionData(missionTempletListByType[i]));
					NKCUtil.SetGameobjectActive(nkcpopupEventMissionSlot, true);
				}
				else
				{
					NKCUtil.SetGameobjectActive(nkcpopupEventMissionSlot, false);
				}
			}
		}

		// Token: 0x06008B82 RID: 35714 RVA: 0x002F6BF6 File Offset: 0x002F4DF6
		private void OnTouchProgress(NKMMissionTemplet templet, NKMMissionData data)
		{
			if (templet == null)
			{
				return;
			}
			NKCContentManager.MoveToShortCut(templet.m_ShortCutType, templet.m_ShortCut, false);
		}

		// Token: 0x06008B83 RID: 35715 RVA: 0x002F6C0E File Offset: 0x002F4E0E
		private void OnTouchComplete(NKMMissionTemplet templet, NKMMissionData data)
		{
			if (templet == null)
			{
				return;
			}
			if (data == null)
			{
				return;
			}
			if (this.dCheckTime != null && !this.dCheckTime(true))
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_MISSION_COMPLETE_REQ(templet.m_MissionTabId, templet.m_GroupId, templet.m_MissionID);
		}

		// Token: 0x06008B84 RID: 35716 RVA: 0x002F6C48 File Offset: 0x002F4E48
		public void OnRecv(NKMPacket_MISSION_COMPLETE_ACK packet)
		{
			NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(packet.missionID);
			if (missionTemplet == null)
			{
				return;
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(missionTemplet.m_MissionTabId);
			if (missionTabTemplet == null)
			{
				return;
			}
			if (missionTabTemplet.m_MissionType != NKM_MISSION_TYPE.BINGO)
			{
				return;
			}
			int eventID = 0;
			int tileIndex = -1;
			if (missionTemplet.m_MissionReward.Count > 0)
			{
				eventID = missionTemplet.m_MissionReward[0].reward_id;
				tileIndex = missionTemplet.m_MissionReward[0].reward_value;
			}
			if (this.dOnComplete != null)
			{
				this.dOnComplete(eventID, tileIndex);
			}
			base.Close();
		}

		// Token: 0x04007849 RID: 30793
		public const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_EVENT";

		// Token: 0x0400784A RID: 30794
		public const string UI_ASSET_NAME = "NKM_UI_POPUP_EVENT_BINGO_SPECIALMISSION";

		// Token: 0x0400784B RID: 30795
		private static NKCPopupEventMission m_Instance;

		// Token: 0x0400784C RID: 30796
		public NKCUIComButton m_btnBack;

		// Token: 0x0400784D RID: 30797
		public NKCUIComStateButton m_btnClose;

		// Token: 0x0400784E RID: 30798
		public List<NKCPopupEventMissionSlot> m_listSlot;

		// Token: 0x0400784F RID: 30799
		private NKMEventBingoTemplet m_bingoTemplet;

		// Token: 0x04007850 RID: 30800
		private NKCPopupEventMission.CheckTime dCheckTime;

		// Token: 0x04007851 RID: 30801
		private NKCPopupEventMission.OnComplete dOnComplete;

		// Token: 0x02001995 RID: 6549
		// (Invoke) Token: 0x0600B95C RID: 47452
		public delegate bool CheckTime(bool bPopup);

		// Token: 0x02001996 RID: 6550
		// (Invoke) Token: 0x0600B960 RID: 47456
		public delegate void OnComplete(int eventID, int tileIndex);
	}
}
