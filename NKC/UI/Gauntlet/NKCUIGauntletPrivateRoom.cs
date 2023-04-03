using System;
using System.Collections.Generic;
using ClientPacket.Pvp;
using NKM;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B80 RID: 2944
	public class NKCUIGauntletPrivateRoom : NKCUIBase
	{
		// Token: 0x170015D5 RID: 5589
		// (get) Token: 0x060087B6 RID: 34742 RVA: 0x002DEDB0 File Offset: 0x002DCFB0
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170015D6 RID: 5590
		// (get) Token: 0x060087B7 RID: 34743 RVA: 0x002DEDB3 File Offset: 0x002DCFB3
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x170015D7 RID: 5591
		// (get) Token: 0x060087B8 RID: 34744 RVA: 0x002DEDB6 File Offset: 0x002DCFB6
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return new List<int>();
			}
		}

		// Token: 0x170015D8 RID: 5592
		// (get) Token: 0x060087B9 RID: 34745 RVA: 0x002DEDBD File Offset: 0x002DCFBD
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_GAUNTLET;
			}
		}

		// Token: 0x060087BA RID: 34746 RVA: 0x002DEDC4 File Offset: 0x002DCFC4
		public override void CloseInternal()
		{
		}

		// Token: 0x060087BB RID: 34747 RVA: 0x002DEDC6 File Offset: 0x002DCFC6
		public static NKCUIManager.LoadedUIData OpenNewInstanceAsync()
		{
			if (!NKCUIManager.IsValid(NKCUIGauntletPrivateRoom.s_LoadedUIData))
			{
				NKCUIGauntletPrivateRoom.s_LoadedUIData = NKCUIManager.OpenNewInstanceAsync<NKCUIGauntletPrivateRoom>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_PRIVATE_ROOM", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIGauntletPrivateRoom.CleanupInstance));
			}
			return NKCUIGauntletPrivateRoom.s_LoadedUIData;
		}

		// Token: 0x170015D9 RID: 5593
		// (get) Token: 0x060087BC RID: 34748 RVA: 0x002DEDFA File Offset: 0x002DCFFA
		public static bool IsInstanceLoaded
		{
			get
			{
				return NKCUIGauntletPrivateRoom.s_LoadedUIData != null && NKCUIGauntletPrivateRoom.s_LoadedUIData.IsLoadComplete;
			}
		}

		// Token: 0x060087BD RID: 34749 RVA: 0x002DEE0F File Offset: 0x002DD00F
		public static NKCUIGauntletPrivateRoom GetInstance()
		{
			if (NKCUIGauntletPrivateRoom.s_LoadedUIData != null && NKCUIGauntletPrivateRoom.s_LoadedUIData.IsLoadComplete)
			{
				return NKCUIGauntletPrivateRoom.s_LoadedUIData.GetInstance<NKCUIGauntletPrivateRoom>();
			}
			return null;
		}

		// Token: 0x060087BE RID: 34750 RVA: 0x002DEE30 File Offset: 0x002DD030
		public static void CleanupInstance()
		{
			NKCUIGauntletPrivateRoom.s_LoadedUIData = null;
		}

		// Token: 0x170015DA RID: 5594
		// (get) Token: 0x060087BF RID: 34751 RVA: 0x002DEE38 File Offset: 0x002DD038
		public NKCUIGauntletPrivateRoomInvite UIGauntletPrivateRoomInvite
		{
			get
			{
				if (this.m_NKCUIGauntletPrivateRoomInvite == null)
				{
					NKCUIManager.LoadedUIData loadedUIData = NKCUIManager.OpenNewInstance<NKCUIGauntletPrivateRoomInvite>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_PRIVATE_ROOM_INVITE_POPUP", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), null);
					this.m_NKCUIGauntletPrivateRoomInvite = loadedUIData.GetInstance<NKCUIGauntletPrivateRoomInvite>();
					this.m_NKCUIGauntletPrivateRoomInvite.Init();
					NKCUtil.SetGameobjectActive(this.m_NKCUIGauntletPrivateRoomInvite, false);
				}
				return this.m_NKCUIGauntletPrivateRoomInvite;
			}
		}

		// Token: 0x060087C0 RID: 34752 RVA: 0x002DEE94 File Offset: 0x002DD094
		public void Init()
		{
			this.m_btnLeaveRoom.PointerClick.RemoveAllListeners();
			this.m_btnLeaveRoom.PointerClick.AddListener(new UnityAction(this.OnTouchLeaveRoom));
			this.m_btnStartDeckSelect.PointerClick.RemoveAllListeners();
			this.m_btnStartDeckSelect.PointerClick.AddListener(new UnityAction(this.OnTouchStartDeckSelect));
			NKCUIGauntletPrivateRoomUserSlot[] array = this.m_slotPlayers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Init();
			}
			foreach (NKCUIGauntletPrivateRoomUserSlot nkcuigauntletPrivateRoomUserSlot in this.m_slotObservers)
			{
				if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_OBSERVE_MODE))
				{
					NKCUtil.SetGameobjectActive(nkcuigauntletPrivateRoomUserSlot, false);
				}
				else
				{
					nkcuigauntletPrivateRoomUserSlot.Init();
				}
			}
			NKCUIGauntletPrivateRoomCustomOption customOption = this.m_customOption;
			if (customOption == null)
			{
				return;
			}
			customOption.Init();
		}

		// Token: 0x060087C1 RID: 34753 RVA: 0x002DEF57 File Offset: 0x002DD157
		public void Open()
		{
			this.RefreshUI();
			if (!base.IsOpen)
			{
				base.UIOpened(true);
			}
		}

		// Token: 0x060087C2 RID: 34754 RVA: 0x002DEF6E File Offset: 0x002DD16E
		public void SetUI()
		{
			this.RefreshUI();
		}

		// Token: 0x060087C3 RID: 34755 RVA: 0x002DEF76 File Offset: 0x002DD176
		public void RefreshUI()
		{
			NKCUtil.SetGameobjectActive(this.m_btnStartDeckSelect, NKCPrivatePVPRoomMgr.IsHost(NKCPrivatePVPRoomMgr.GetMyPvpGameLobbyUserState()));
			this.RefreshPlayer();
			this.RefreshObserver();
			this.RefreshCustomOption();
		}

		// Token: 0x060087C4 RID: 34756 RVA: 0x002DEFA0 File Offset: 0x002DD1A0
		private void RefreshPlayer()
		{
			List<NKMPvpGameLobbyUserState> users = NKCPrivatePVPRoomMgr.PvpGameLobbyState.users;
			int num = 0;
			while (num < users.Count && num < 2)
			{
				if (users[num] == null)
				{
					this.m_slotPlayers[num].SetEmptyUI();
				}
				else
				{
					this.m_slotPlayers[num].SetUI(users[num]);
				}
				num++;
			}
		}

		// Token: 0x060087C5 RID: 34757 RVA: 0x002DEFFC File Offset: 0x002DD1FC
		private void RefreshObserver()
		{
			if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_OBSERVE_MODE))
			{
				return;
			}
			List<NKMPvpGameLobbyUserState> observers = NKCPrivatePVPRoomMgr.PvpGameLobbyState.observers;
			int num = 0;
			while (num < observers.Count && num < 4)
			{
				this.m_slotObservers[num].SetUI(observers[num]);
				num++;
			}
			for (int i = observers.Count; i < 4; i++)
			{
				this.m_slotObservers[i].SetEmptyUI();
			}
		}

		// Token: 0x060087C6 RID: 34758 RVA: 0x002DF065 File Offset: 0x002DD265
		private void RefreshCustomOption()
		{
			NKCUIGauntletPrivateRoomCustomOption customOption = this.m_customOption;
			if (customOption == null)
			{
				return;
			}
			customOption.SetOption(NKCPrivatePVPRoomMgr.PvpGameLobbyState.config);
		}

		// Token: 0x060087C7 RID: 34759 RVA: 0x002DF081 File Offset: 0x002DD281
		private void OnTouchLeaveRoom()
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_PRIVATE_PVP_READY_CANCEL_TITLE, NKCUtilString.GET_STRING_PRIVATE_PVP_READY_CANCEL, new NKCPopupOKCancel.OnButton(NKCPrivatePVPRoomMgr.Send_NKMPacket_PRIVATE_PVP_EXIT_REQ), null, false);
		}

		// Token: 0x060087C8 RID: 34760 RVA: 0x002DF0A0 File Offset: 0x002DD2A0
		private void OnTouchStartDeckSelect()
		{
			NKCPopupOKCancel.OpenOKCancelBox("대전 시작", "덱 구성 단계로 넘어가시겠습니까?", new NKCPopupOKCancel.OnButton(NKCPrivatePVPRoomMgr.Send_NKMPacket_PVP_ROOM_START_GAME_SETTING_REQ), null, false);
		}

		// Token: 0x060087C9 RID: 34761 RVA: 0x002DF0BF File Offset: 0x002DD2BF
		public override void OnBackButton()
		{
		}

		// Token: 0x060087CA RID: 34762 RVA: 0x002DF0C1 File Offset: 0x002DD2C1
		public void ProcessBackButton()
		{
			base.OnBackButton();
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetReservedLobbyTab(NKC_GAUNTLET_LOBBY_TAB.NGLT_PRIVATE);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LOBBY, true);
		}

		// Token: 0x0400741A RID: 29722
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_GAUNTLET";

		// Token: 0x0400741B RID: 29723
		private const string UI_ASSET_NAME = "NKM_UI_GAUNTLET_PRIVATE_ROOM";

		// Token: 0x0400741C RID: 29724
		private static NKCUIManager.LoadedUIData s_LoadedUIData;

		// Token: 0x0400741D RID: 29725
		[Header("버튼")]
		public NKCUIComButton m_btnLeaveRoom;

		// Token: 0x0400741E RID: 29726
		public NKCUIComButton m_btnStartDeckSelect;

		// Token: 0x0400741F RID: 29727
		[Header("유저 슬롯")]
		private const int PLAYER_COUNT_MAX = 2;

		// Token: 0x04007420 RID: 29728
		public NKCUIGauntletPrivateRoomUserSlot[] m_slotPlayers = new NKCUIGauntletPrivateRoomUserSlot[2];

		// Token: 0x04007421 RID: 29729
		private const int OBSERVER_COUNT_MAX = 4;

		// Token: 0x04007422 RID: 29730
		public NKCUIGauntletPrivateRoomUserSlot[] m_slotObservers = new NKCUIGauntletPrivateRoomUserSlot[4];

		// Token: 0x04007423 RID: 29731
		[Header("방 옵션")]
		public NKCUIGauntletPrivateRoomCustomOption m_customOption;

		// Token: 0x04007424 RID: 29732
		private NKCUIGauntletPrivateRoomInvite m_NKCUIGauntletPrivateRoomInvite;
	}
}
