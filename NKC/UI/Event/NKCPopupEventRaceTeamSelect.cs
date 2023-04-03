using System;
using ClientPacket.Event;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BCE RID: 3022
	public class NKCPopupEventRaceTeamSelect : NKCUIBase
	{
		// Token: 0x17001665 RID: 5733
		// (get) Token: 0x06008BF4 RID: 35828 RVA: 0x002F9C60 File Offset: 0x002F7E60
		public static NKCPopupEventRaceTeamSelect Instance
		{
			get
			{
				if (NKCPopupEventRaceTeamSelect.m_Instance == null)
				{
					NKCPopupEventRaceTeamSelect.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupEventRaceTeamSelect>("AB_UI_NKM_UI_EVENT_PF_RACE", "NKM_UI_POPUP_EVENT_RACE_TEAM_SELECT", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupEventRaceTeamSelect.CleanupInstance)).GetInstance<NKCPopupEventRaceTeamSelect>();
					NKCPopupEventRaceTeamSelect.m_Instance.InitUI();
				}
				return NKCPopupEventRaceTeamSelect.m_Instance;
			}
		}

		// Token: 0x17001666 RID: 5734
		// (get) Token: 0x06008BF5 RID: 35829 RVA: 0x002F9CAF File Offset: 0x002F7EAF
		public static bool HasInstance
		{
			get
			{
				return NKCPopupEventRaceTeamSelect.m_Instance != null;
			}
		}

		// Token: 0x17001667 RID: 5735
		// (get) Token: 0x06008BF6 RID: 35830 RVA: 0x002F9CBC File Offset: 0x002F7EBC
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupEventRaceTeamSelect.m_Instance != null && NKCPopupEventRaceTeamSelect.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008BF7 RID: 35831 RVA: 0x002F9CD7 File Offset: 0x002F7ED7
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupEventRaceTeamSelect.m_Instance != null && NKCPopupEventRaceTeamSelect.m_Instance.IsOpen)
			{
				NKCPopupEventRaceTeamSelect.m_Instance.Close();
			}
		}

		// Token: 0x06008BF8 RID: 35832 RVA: 0x002F9CFC File Offset: 0x002F7EFC
		private static void CleanupInstance()
		{
			NKCPopupEventRaceTeamSelect.m_Instance = null;
		}

		// Token: 0x17001668 RID: 5736
		// (get) Token: 0x06008BF9 RID: 35833 RVA: 0x002F9D04 File Offset: 0x002F7F04
		public override string MenuName
		{
			get
			{
				return "RACE TEAM SELECT";
			}
		}

		// Token: 0x17001669 RID: 5737
		// (get) Token: 0x06008BFA RID: 35834 RVA: 0x002F9D0B File Offset: 0x002F7F0B
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x06008BFB RID: 35835 RVA: 0x002F9D10 File Offset: 0x002F7F10
		public void InitUI()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnRedTeamSelect, new UnityAction(this.OnClickRedTeamSelect));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnBlueTeamSelect, new UnityAction(this.OnClickBlueTeamSelect));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnOK, new UnityAction(this.OnClickOK));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnCancel, new UnityAction(base.Close));
			if (this.m_eventTriggerBg != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(delegate(BaseEventData eventData)
				{
					NKCPopupEventRaceTeamSelect.CheckInstanceAndClose();
				});
				this.m_eventTriggerBg.triggers.Add(entry);
			}
			NKCUtil.SetHotkey(this.m_csbtnOK, HotkeyEventType.Confirm, null, false);
			NKCUtil.SetHotkey(this.m_csbtnRedTeamSelect, HotkeyEventType.Left, null, false);
			NKCUtil.SetHotkey(this.m_csbtnBlueTeamSelect, HotkeyEventType.Right, null, false);
		}

		// Token: 0x06008BFC RID: 35836 RVA: 0x002F9DFC File Offset: 0x002F7FFC
		public void Open(int eventId)
		{
			this.m_EventId = eventId;
			NKCUtil.SetGameobjectActive(this.m_objRedSelected, false);
			NKCUtil.SetGameobjectActive(this.m_objBlueSelected, false);
			this.m_csbtnOK.SetLock(true, false);
			this.SetCharacter(eventId);
			base.gameObject.SetActive(true);
			base.UIOpened(true);
		}

		// Token: 0x06008BFD RID: 35837 RVA: 0x002F9E4F File Offset: 0x002F804F
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06008BFE RID: 35838 RVA: 0x002F9E60 File Offset: 0x002F8060
		private void SetCharacter(int eventId)
		{
			NKMEventRaceTemplet nkmeventRaceTemplet = NKMEventRaceTemplet.Find(eventId);
			if (nkmeventRaceTemplet == null)
			{
				return;
			}
			this.SetCharacterImage(nkmeventRaceTemplet.TeamAUnitImageType, nkmeventRaceTemplet.TeamAUnitId, this.m_imgRedTeamUnit);
			this.SetCharacterImage(nkmeventRaceTemplet.TeamBUnitImageType, nkmeventRaceTemplet.TeamBUnitId, this.m_imgBlueTeamUnit);
		}

		// Token: 0x06008BFF RID: 35839 RVA: 0x002F9EA8 File Offset: 0x002F80A8
		private void SetCharacterImage(string type, int id, Image characterImage)
		{
			if (type != null && type == "SKIN")
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(id);
				if (skinTemplet != null)
				{
					NKCUtil.SetImageSprite(characterImage, NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, skinTemplet), true);
					return;
				}
			}
			else
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(id);
				if (unitTempletBase != null)
				{
					NKCUtil.SetImageSprite(characterImage, NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitTempletBase), true);
				}
			}
		}

		// Token: 0x06008C00 RID: 35840 RVA: 0x002F9EF6 File Offset: 0x002F80F6
		private void OnClickRedTeamSelect()
		{
			this.m_selectedTeam = RaceTeam.TeamA;
			NKCUtil.SetGameobjectActive(this.m_objRedSelected, true);
			NKCUtil.SetGameobjectActive(this.m_objBlueSelected, false);
			this.m_csbtnOK.SetLock(false, false);
		}

		// Token: 0x06008C01 RID: 35841 RVA: 0x002F9F24 File Offset: 0x002F8124
		private void OnClickBlueTeamSelect()
		{
			this.m_selectedTeam = RaceTeam.TeamB;
			NKCUtil.SetGameobjectActive(this.m_objRedSelected, false);
			NKCUtil.SetGameobjectActive(this.m_objBlueSelected, true);
			this.m_csbtnOK.SetLock(false, false);
		}

		// Token: 0x06008C02 RID: 35842 RVA: 0x002F9F52 File Offset: 0x002F8152
		private void OnClickOK()
		{
			base.Close();
			NKCPacketSender.Send_NKMPACKET_RACE_TEAM_SELECT_REQ(this.m_selectedTeam);
		}

		// Token: 0x06008C03 RID: 35843 RVA: 0x002F9F65 File Offset: 0x002F8165
		private void OnDestroy()
		{
			this.m_imgRedTeamUnit = null;
			this.m_imgBlueTeamUnit = null;
			this.m_objRedSelected = null;
			this.m_objBlueSelected = null;
			this.m_csbtnRedTeamSelect = null;
			this.m_csbtnBlueTeamSelect = null;
			this.m_csbtnOK = null;
			this.m_csbtnCancel = null;
		}

		// Token: 0x040078D5 RID: 30933
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_EVENT_PF_RACE";

		// Token: 0x040078D6 RID: 30934
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_EVENT_RACE_TEAM_SELECT";

		// Token: 0x040078D7 RID: 30935
		private static NKCPopupEventRaceTeamSelect m_Instance;

		// Token: 0x040078D8 RID: 30936
		public Image m_imgRedTeamUnit;

		// Token: 0x040078D9 RID: 30937
		public Image m_imgBlueTeamUnit;

		// Token: 0x040078DA RID: 30938
		public GameObject m_objRedSelected;

		// Token: 0x040078DB RID: 30939
		public GameObject m_objBlueSelected;

		// Token: 0x040078DC RID: 30940
		public EventTrigger m_eventTriggerBg;

		// Token: 0x040078DD RID: 30941
		public NKCUIComStateButton m_csbtnRedTeamSelect;

		// Token: 0x040078DE RID: 30942
		public NKCUIComStateButton m_csbtnBlueTeamSelect;

		// Token: 0x040078DF RID: 30943
		public NKCUIComStateButton m_csbtnOK;

		// Token: 0x040078E0 RID: 30944
		public NKCUIComStateButton m_csbtnCancel;

		// Token: 0x040078E1 RID: 30945
		private RaceTeam m_selectedTeam;

		// Token: 0x040078E2 RID: 30946
		private int m_EventId;
	}
}
