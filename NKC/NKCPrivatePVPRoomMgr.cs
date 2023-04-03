using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Pvp;
using Cs.Logging;
using NKC.UI;
using NKC.UI.Gauntlet;
using NKC.UI.Shop;
using NKM;

namespace NKC
{
	// Token: 0x020006BC RID: 1724
	public static class NKCPrivatePVPRoomMgr
	{
		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06003AA1 RID: 15009 RVA: 0x0012DA0A File Offset: 0x0012BC0A
		public static NKMPvpGameLobbyState PvpGameLobbyState
		{
			get
			{
				return NKCPrivatePVPRoomMgr.m_pvpGameLobbyState;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06003AA2 RID: 15010 RVA: 0x0012DA11 File Offset: 0x0012BC11
		public static List<FriendListData> SearchResult
		{
			get
			{
				return NKCPrivatePVPRoomMgr.m_searchResult;
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06003AA3 RID: 15011 RVA: 0x0012DA18 File Offset: 0x0012BC18
		public static bool PrivatePVPLobbyBanUpState
		{
			get
			{
				return NKCPrivatePVPRoomMgr.m_pvpGameLobbyState != null && NKCPrivatePVPRoomMgr.m_pvpGameLobbyState.config.applyBanUpSystem;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06003AA4 RID: 15012 RVA: 0x0012DA32 File Offset: 0x0012BC32
		public static NKMPrivateGameConfig PrivateGameConfig
		{
			get
			{
				if (NKCPrivatePVPRoomMgr.m_privateGameConfig == null)
				{
					NKCPrivatePVPRoomMgr.m_privateGameConfig = new NKMPrivateGameConfig();
					NKCPrivatePVPRoomMgr.m_privateGameConfig.applyEquipStat = true;
				}
				return NKCPrivatePVPRoomMgr.m_privateGameConfig;
			}
		}

		// Token: 0x06003AA5 RID: 15013 RVA: 0x0012DA55 File Offset: 0x0012BC55
		public static void SetRoomData(NKMPvpGameLobbyState lobbyState)
		{
			NKCPrivatePVPRoomMgr.m_pvpGameLobbyState = lobbyState;
		}

		// Token: 0x06003AA6 RID: 15014 RVA: 0x0012DA5D File Offset: 0x0012BC5D
		public static void ResetData()
		{
			NKCPrivatePVPRoomMgr.m_pvpGameLobbyState = null;
			NKCPrivatePVPRoomMgr.ResetInviteData();
			NKCPrivatePVPRoomMgr.ResetSearchData();
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x0012DA6F File Offset: 0x0012BC6F
		public static void ResetSearchData()
		{
			NKCPrivatePVPRoomMgr.m_searchResult.Clear();
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x0012DA7B File Offset: 0x0012BC7B
		public static void ResetInviteData()
		{
			NKCPrivatePVPRoomMgr.isInviteSender = false;
			NKCPrivatePVPRoomMgr.m_targetProfile = null;
			NKCPrivatePVPRoomMgr.m_targetFriendListData = null;
		}

		// Token: 0x06003AA9 RID: 15017 RVA: 0x0012DA8F File Offset: 0x0012BC8F
		private static void ChangeScene(NKM_SCEN_ID scenID)
		{
			NKCScenManager.GetScenManager().ScenChangeFade(scenID, true);
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x0012DA9D File Offset: 0x0012BC9D
		public static void CancelInviteProcess()
		{
			NKCPrivatePVPRoomMgr.ResetInviteData();
		}

		// Token: 0x06003AAB RID: 15019 RVA: 0x0012DAA4 File Offset: 0x0012BCA4
		public static void CancelAllProcess()
		{
			NKCPrivatePVPRoomMgr.ResetData();
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_PRIVATE_ROOM)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_PRIVATE_ROOM().OnCancelAllProcess();
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_PRIVATE_ROOM_DECK_SELECT)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_PRIVATE_ROOM_DECK_SELECT().OnCancelAllProcess();
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_LEAGUE_ROOM || NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetReservedLobbyTab(NKC_GAUNTLET_LOBBY_TAB.NGLT_PRIVATE);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LOBBY, true);
			}
		}

		// Token: 0x06003AAC RID: 15020 RVA: 0x0012DB2A File Offset: 0x0012BD2A
		public static void OnClickAcceptInvite()
		{
			NKCPrivatePVPRoomMgr.Send_NKMPacket_PVP_ROOM_ACCEPT_INVITE_REQ(true, NKCPrivatePVPRoomMgr.m_currentInviteUserUID);
		}

		// Token: 0x06003AAD RID: 15021 RVA: 0x0012DB37 File Offset: 0x0012BD37
		public static void OnClickRefuseInvite()
		{
			NKCPrivatePVPRoomMgr.Send_NKMPacket_PVP_ROOM_ACCEPT_INVITE_REQ(false, NKCPrivatePVPRoomMgr.m_currentInviteUserUID);
			NKCPrivatePVPRoomMgr.CancelAllProcess();
		}

		// Token: 0x06003AAE RID: 15022 RVA: 0x0012DB4C File Offset: 0x0012BD4C
		public static void ShowInvitePopup()
		{
			NKC_SCEN_GAUNTLET_PRIVATE_ROOM nkc_SCEN_GAUNTLET_PRIVATE_ROOM = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_PRIVATE_ROOM();
			if (nkc_SCEN_GAUNTLET_PRIVATE_ROOM != null)
			{
				NKCUIGauntletPrivateRoom gauntletPrivateRoom = nkc_SCEN_GAUNTLET_PRIVATE_ROOM.GauntletPrivateRoom;
				if (gauntletPrivateRoom == null)
				{
					return;
				}
				gauntletPrivateRoom.UIGauntletPrivateRoomInvite.Open();
			}
		}

		// Token: 0x06003AAF RID: 15023 RVA: 0x0012DB7C File Offset: 0x0012BD7C
		public static NKMPvpGameLobbyUserState GetMyPvpGameLobbyUserState()
		{
			if (NKCPrivatePVPRoomMgr.m_pvpGameLobbyState == null)
			{
				return null;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			foreach (NKMPvpGameLobbyUserState nkmpvpGameLobbyUserState in NKCPrivatePVPRoomMgr.m_pvpGameLobbyState.users)
			{
				if (nkmpvpGameLobbyUserState != null && nkmpvpGameLobbyUserState.profileData.commonProfile.userUid == myUserData.m_UserUID)
				{
					return nkmpvpGameLobbyUserState;
				}
			}
			foreach (NKMPvpGameLobbyUserState nkmpvpGameLobbyUserState2 in NKCPrivatePVPRoomMgr.m_pvpGameLobbyState.observers)
			{
				if (nkmpvpGameLobbyUserState2.profileData.commonProfile.userUid == myUserData.m_UserUID)
				{
					return nkmpvpGameLobbyUserState2;
				}
			}
			return null;
		}

		// Token: 0x06003AB0 RID: 15024 RVA: 0x0012DC60 File Offset: 0x0012BE60
		public static NKMPvpGameLobbyUserState GetTargetPvpGameLobbyUserState()
		{
			if (NKCPrivatePVPRoomMgr.m_pvpGameLobbyState == null)
			{
				return null;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			foreach (NKMPvpGameLobbyUserState nkmpvpGameLobbyUserState in NKCPrivatePVPRoomMgr.m_pvpGameLobbyState.users)
			{
				if (nkmpvpGameLobbyUserState != null && nkmpvpGameLobbyUserState.profileData.commonProfile.userUid != myUserData.m_UserUID)
				{
					return nkmpvpGameLobbyUserState;
				}
			}
			return null;
		}

		// Token: 0x06003AB1 RID: 15025 RVA: 0x0012DCE8 File Offset: 0x0012BEE8
		public static NKMPvpGameLobbyUserState GetLeftPvpGameLobbyUserState()
		{
			if (NKCPrivatePVPRoomMgr.IsPlayer(NKCPrivatePVPRoomMgr.GetMyPvpGameLobbyUserState()))
			{
				return NKCPrivatePVPRoomMgr.GetMyPvpGameLobbyUserState();
			}
			return NKCPrivatePVPRoomMgr.m_pvpGameLobbyState.users[0];
		}

		// Token: 0x06003AB2 RID: 15026 RVA: 0x0012DD0C File Offset: 0x0012BF0C
		public static NKMPvpGameLobbyUserState GetRightPvpGameLobbyUserState()
		{
			if (NKCPrivatePVPRoomMgr.IsPlayer(NKCPrivatePVPRoomMgr.GetMyPvpGameLobbyUserState()))
			{
				return NKCPrivatePVPRoomMgr.GetTargetPvpGameLobbyUserState();
			}
			return NKCPrivatePVPRoomMgr.m_pvpGameLobbyState.users[1];
		}

		// Token: 0x06003AB3 RID: 15027 RVA: 0x0012DD30 File Offset: 0x0012BF30
		public static bool CanEditDeck()
		{
			return NKCPrivatePVPRoomMgr.IsPlayer(NKCPrivatePVPRoomMgr.GetMyPvpGameLobbyUserState()) && NKCPrivatePVPRoomMgr.m_pvpGameLobbyState.gameState == NKM_GAME_STATE.NGS_LOBBY_GAME_SETTING;
		}

		// Token: 0x06003AB4 RID: 15028 RVA: 0x0012DD50 File Offset: 0x0012BF50
		public static bool IsHost(NKMPvpGameLobbyUserState userState)
		{
			return userState != null && userState.isHost;
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x0012DD64 File Offset: 0x0012BF64
		public static bool IsPlayer(long userUid)
		{
			if (NKCPrivatePVPRoomMgr.m_pvpGameLobbyState == null)
			{
				return false;
			}
			foreach (NKMPvpGameLobbyUserState nkmpvpGameLobbyUserState in NKCPrivatePVPRoomMgr.m_pvpGameLobbyState.users)
			{
				if (nkmpvpGameLobbyUserState != null && nkmpvpGameLobbyUserState.profileData.commonProfile.userUid == userUid)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003AB6 RID: 15030 RVA: 0x0012DDDC File Offset: 0x0012BFDC
		public static bool IsPlayer(NKMPvpGameLobbyUserState userState)
		{
			if (userState == null)
			{
				return false;
			}
			if (NKCPrivatePVPRoomMgr.m_pvpGameLobbyState == null)
			{
				return false;
			}
			foreach (NKMPvpGameLobbyUserState nkmpvpGameLobbyUserState in NKCPrivatePVPRoomMgr.m_pvpGameLobbyState.users)
			{
				if (nkmpvpGameLobbyUserState != null && nkmpvpGameLobbyUserState.profileData.commonProfile.userUid == userState.profileData.commonProfile.userUid)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003AB7 RID: 15031 RVA: 0x0012DE68 File Offset: 0x0012C068
		public static bool IsObserver(NKMPvpGameLobbyUserState userState)
		{
			if (userState == null)
			{
				return false;
			}
			if (NKCPrivatePVPRoomMgr.m_pvpGameLobbyState == null)
			{
				return false;
			}
			using (List<NKMPvpGameLobbyUserState>.Enumerator enumerator = NKCPrivatePVPRoomMgr.m_pvpGameLobbyState.observers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.profileData.commonProfile.userUid == userState.profileData.commonProfile.userUid)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003AB8 RID: 15032 RVA: 0x0012DEF0 File Offset: 0x0012C0F0
		public static void Send_NKMPacket_PRIVATE_PVP_CREATE_ROOM_REQ()
		{
			NKCPrivatePVPRoomMgr.ResetData();
			NKMPacket_PRIVATE_PVP_CREATE_ROOM_REQ nkmpacket_PRIVATE_PVP_CREATE_ROOM_REQ = new NKMPacket_PRIVATE_PVP_CREATE_ROOM_REQ();
			nkmpacket_PRIVATE_PVP_CREATE_ROOM_REQ.config = NKCPrivatePVPRoomMgr.PrivateGameConfig;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_PRIVATE_PVP_CREATE_ROOM_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003AB9 RID: 15033 RVA: 0x0012DF26 File Offset: 0x0012C126
		public static void OnRecv(NKMPacket_PRIVATE_PVP_CREATE_ROOM_ACK sPacket)
		{
			if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
			{
				return;
			}
			NKCPrivatePVPRoomMgr.SetRoomData(sPacket.lobbyState);
			NKCPrivatePVPRoomMgr.ChangeScene(NKM_SCEN_ID.NSI_GAUNTLET_PRIVATE_ROOM);
		}

		// Token: 0x06003ABA RID: 15034 RVA: 0x0012DF44 File Offset: 0x0012C144
		public static void SetApplyEquipSet(bool value)
		{
			NKCPrivatePVPRoomMgr.m_privateGameConfig.applyEquipStat = value;
		}

		// Token: 0x06003ABB RID: 15035 RVA: 0x0012DF51 File Offset: 0x0012C151
		public static void SetApplyAllUnitMaxLevel(bool value)
		{
			NKCPrivatePVPRoomMgr.m_privateGameConfig.applyAllUnitMaxLevel = value;
		}

		// Token: 0x06003ABC RID: 15036 RVA: 0x0012DF5E File Offset: 0x0012C15E
		public static void SetApplyBanUp(bool value)
		{
			NKCPrivatePVPRoomMgr.m_privateGameConfig.applyBanUpSystem = value;
		}

		// Token: 0x06003ABD RID: 15037 RVA: 0x0012DF6B File Offset: 0x0012C16B
		public static void SetDraftBanMode(bool value)
		{
			if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
			{
				return;
			}
			NKCPrivatePVPRoomMgr.m_privateGameConfig.draftBanMode = value;
		}

		// Token: 0x06003ABE RID: 15038 RVA: 0x0012DF84 File Offset: 0x0012C184
		public static void Send_NKMPacket_PVP_ROOM_INVITE_REQ(FriendListData friendListData)
		{
			NKCPrivatePVPRoomMgr.isInviteSender = true;
			NKCPrivatePVPRoomMgr.m_targetProfile = null;
			NKCPrivatePVPRoomMgr.m_targetFriendListData = friendListData;
			NKCPrivatePVPRoomMgr.m_currentInviteUserUID = friendListData.commonProfile.userUid;
			NKMPacket_PVP_ROOM_INVITE_REQ nkmpacket_PVP_ROOM_INVITE_REQ = new NKMPacket_PVP_ROOM_INVITE_REQ();
			nkmpacket_PVP_ROOM_INVITE_REQ.friendCode = friendListData.commonProfile.friendCode;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_PVP_ROOM_INVITE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003ABF RID: 15039 RVA: 0x0012DFE0 File Offset: 0x0012C1E0
		public static void OnRecv(NKMPacket_PVP_ROOM_INVITE_ACK sPacket)
		{
			if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
			{
				return;
			}
			NKCPrivatePVPRoomMgr.isInviteSender = true;
			NKCPopupGauntletInvite.ClosePopupBox();
			NKCPopupGauntletInvite.OpenOKTimerBox(NKCUtilString.GET_STRING_FRIEND_PVP, NKCUtilString.GET_STRING_PRIVATE_PVP_INVITE_REQ, new NKCPopupGauntletInvite.OnButton(NKCPrivatePVPRoomMgr.Send_NKMPacket_PVP_ROOM_CANCEL_INVITE_REQ), NKCPrivatePVPRoomMgr.WaitTimeSenderInviteReq, NKCUtilString.GET_STRING_CANCEL, NKCUtilString.GET_STRING_PRIVATE_PVP_AUTO_CANCEL_ID, NKCPrivatePVPRoomMgr.m_targetFriendListData, NKCPrivatePVPMgr.PrivateGameConfig);
		}

		// Token: 0x06003AC0 RID: 15040 RVA: 0x0012E038 File Offset: 0x0012C238
		public static void OnRecv(NKMPacket_PVP_ROOM_INVITE_NOT sPacket)
		{
			if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
			{
				return;
			}
			if (NKCPrivatePVPRoomMgr.isInviteSender)
			{
				NKCPrivatePVPRoomMgr.Send_NKMPacket_PVP_ROOM_ACCEPT_INVITE_REQ(false, sPacket.senderProfile.commonProfile.userUid);
				return;
			}
			if (NKCUIShop.IsInstanceOpen)
			{
				NKCPrivatePVPRoomMgr.Send_NKMPacket_PVP_ROOM_ACCEPT_INVITE_REQ(false, sPacket.senderProfile.commonProfile.userUid);
				return;
			}
			NKM_SCEN_ID nowScenID = NKCScenManager.GetScenManager().GetNowScenID();
			if (nowScenID <= NKM_SCEN_ID.NSI_COLLECTION)
			{
				if (nowScenID == NKM_SCEN_ID.NSI_HOME || nowScenID == NKM_SCEN_ID.NSI_COLLECTION)
				{
					goto IL_89;
				}
			}
			else if (nowScenID == NKM_SCEN_ID.NSI_FRIEND || nowScenID - NKM_SCEN_ID.NSI_GAUNTLET_INTRO <= 1 || nowScenID == NKM_SCEN_ID.NSI_GUILD_LOBBY)
			{
				goto IL_89;
			}
			NKCPrivatePVPRoomMgr.Send_NKMPacket_PVP_ROOM_ACCEPT_INVITE_REQ(false, sPacket.senderProfile.commonProfile.userUid);
			return;
			IL_89:
			NKCPrivatePVPRoomMgr.isInviteSender = false;
			NKCPrivatePVPRoomMgr.m_targetProfile = sPacket.senderProfile;
			NKCPrivatePVPRoomMgr.m_currentInviteUserUID = NKCPrivatePVPRoomMgr.m_targetProfile.commonProfile.userUid;
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_HOME)
			{
				NKC_SCEN_HOME scen_HOME = NKCScenManager.GetScenManager().Get_SCEN_HOME();
				if (scen_HOME != null)
				{
					scen_HOME.UnhideLobbyUI();
				}
			}
			NKCPopupGauntletInvite.OpenOKCancelTimerBox(NKCUtilString.GET_STRING_FRIEND_PVP, NKCUtilString.GET_STRING_PRIVATE_PVP_INVITE_NOT, new NKCPopupGauntletInvite.OnButton(NKCPrivatePVPRoomMgr.OnClickAcceptInvite), new NKCPopupGauntletInvite.OnButton(NKCPrivatePVPRoomMgr.OnClickRefuseInvite), (float)sPacket.timeoutDurationSec, NKCUtilString.GET_STRING_PRIVATE_PVP_AUTO_CANCEL_ID, NKCUtilString.GET_STRING_ACCEPT, NKCUtilString.GET_STRING_REFUSE, NKCPrivatePVPRoomMgr.m_targetProfile, sPacket.config);
		}

		// Token: 0x06003AC1 RID: 15041 RVA: 0x0012E15C File Offset: 0x0012C35C
		public static void Send_NKMPacket_PVP_ROOM_CANCEL_INVITE_REQ()
		{
			NKMPacket_PVP_ROOM_CANCEL_INVITE_REQ nkmpacket_PVP_ROOM_CANCEL_INVITE_REQ = new NKMPacket_PVP_ROOM_CANCEL_INVITE_REQ();
			nkmpacket_PVP_ROOM_CANCEL_INVITE_REQ.targetUserUid = NKCPrivatePVPRoomMgr.m_currentInviteUserUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_PVP_ROOM_CANCEL_INVITE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003AC2 RID: 15042 RVA: 0x0012E18D File Offset: 0x0012C38D
		public static void OnRecv(NKMPacket_PVP_ROOM_CANCEL_INVITE_ACK sPacket)
		{
			if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
			{
				return;
			}
			NKCPopupOKCancel.ClosePopupBox();
			NKMPopUpBox.CloseWaitBox();
			if (NKCPrivatePVPRoomMgr.isInviteSender)
			{
				NKCPrivatePVPRoomMgr.CancelInviteProcess();
				return;
			}
			NKCPrivatePVPRoomMgr.CancelAllProcess();
		}

		// Token: 0x06003AC3 RID: 15043 RVA: 0x0012E1B8 File Offset: 0x0012C3B8
		public static void OnRecv(NKMPacket_PVP_ROOM_CANCEL_INVITE_NOT sPacket)
		{
			if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
			{
				return;
			}
			if (NKCPrivatePVPRoomMgr.m_currentInviteUserUID != 0L && sPacket.targetUserUid != NKCPrivatePVPRoomMgr.m_currentInviteUserUID)
			{
				Log.Debug(string.Format("[PrivatePvp] TargetUser[{0}] CancelType[{1}]", sPacket.targetUserUid, sPacket.cancelType.ToString()), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCPrivatePVPRoomMgr.cs", 459);
				return;
			}
			NKCPopupGauntletInvite.ClosePopupBox();
			NKCPopupOKCancel.ClosePopupBox();
			NKCPrivatePVPRoomMgr.ShowCancelNoticePopup(sPacket.cancelType);
		}

		// Token: 0x06003AC4 RID: 15044 RVA: 0x0012E230 File Offset: 0x0012C430
		public static void ShowCancelNoticePopup(PrivatePvpCancelType cancelType)
		{
			if (cancelType == PrivatePvpCancelType.IRejectInvitation)
			{
				return;
			}
			string str = "SI_DP_PRIVATE_PVP_CANCEL_NOT_";
			int num = (int)cancelType;
			string @string = NKCStringTable.GetString(str + num.ToString(), false);
			if (NKCPrivatePVPRoomMgr.isInviteSender)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_WARNING, @string, new NKCPopupOKCancel.OnButton(NKCPrivatePVPRoomMgr.CancelInviteProcess), "");
				return;
			}
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_WARNING, @string, new NKCPopupOKCancel.OnButton(NKCPrivatePVPRoomMgr.CancelAllProcess), "");
		}

		// Token: 0x06003AC5 RID: 15045 RVA: 0x0012E29C File Offset: 0x0012C49C
		public static void Send_NKMPacket_PVP_ROOM_ACCEPT_INVITE_REQ(bool bAccept, long targetUserUID)
		{
			NKMPacket_PVP_ROOM_ACCEPT_INVITE_REQ nkmpacket_PVP_ROOM_ACCEPT_INVITE_REQ = new NKMPacket_PVP_ROOM_ACCEPT_INVITE_REQ();
			nkmpacket_PVP_ROOM_ACCEPT_INVITE_REQ.targetUserUid = targetUserUID;
			nkmpacket_PVP_ROOM_ACCEPT_INVITE_REQ.accept = bAccept;
			if (!bAccept && targetUserUID != NKCPrivatePVPRoomMgr.m_currentInviteUserUID)
			{
				NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_PVP_ROOM_ACCEPT_INVITE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, bAccept);
				return;
			}
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_PVP_ROOM_ACCEPT_INVITE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, bAccept);
		}

		// Token: 0x06003AC6 RID: 15046 RVA: 0x0012E2F0 File Offset: 0x0012C4F0
		public static void OnRecv(NKMPacket_PVP_ROOM_ACCEPT_INVITE_ACK sPacket)
		{
			if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
			{
				return;
			}
			if (sPacket.cancelType == PrivatePvpCancelType.IRejectInvitation)
			{
				return;
			}
			NKCConnectGame connectGame = NKCScenManager.GetScenManager().GetConnectGame();
			if (!string.IsNullOrEmpty(sPacket.serverIp))
			{
				connectGame.SetRemoteAddress(sPacket.serverIp, sPacket.port);
				connectGame.SetAccessToken(sPacket.accessToken);
				connectGame.ResetConnection();
				connectGame.ConnectToLobbyServer();
			}
			NKCPrivatePVPRoomMgr.ChangeScene(NKM_SCEN_ID.NSI_GAUNTLET_PRIVATE_ROOM);
		}

		// Token: 0x06003AC7 RID: 15047 RVA: 0x0012E35C File Offset: 0x0012C55C
		public static void OnRecv(NKMPacket_PVP_ROOM_ACCEPT_INVITE_NOT sPacket)
		{
			if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
			{
				return;
			}
			NKCPopupGauntletInvite.ClosePopupBox();
			NKCUIGauntletPrivateRoom instance = NKCUIGauntletPrivateRoom.GetInstance();
			if (instance != null)
			{
				NKCUIGauntletPrivateRoomInvite uigauntletPrivateRoomInvite = instance.UIGauntletPrivateRoomInvite;
				if (uigauntletPrivateRoomInvite != null)
				{
					uigauntletPrivateRoomInvite.Close();
				}
			}
			NKCPrivatePVPRoomMgr.SetRoomData(sPacket.lobbyState);
			NKC_SCEN_GAUNTLET_PRIVATE_ROOM nkc_SCEN_GAUNTLET_PRIVATE_ROOM = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_PRIVATE_ROOM();
			if (nkc_SCEN_GAUNTLET_PRIVATE_ROOM != null)
			{
				NKCUIGauntletPrivateRoom gauntletPrivateRoom = nkc_SCEN_GAUNTLET_PRIVATE_ROOM.GauntletPrivateRoom;
				if (gauntletPrivateRoom == null)
				{
					return;
				}
				gauntletPrivateRoom.SetUI();
			}
		}

		// Token: 0x06003AC8 RID: 15048 RVA: 0x0012E3BC File Offset: 0x0012C5BC
		public static void Send_NKMPacket_PRIVATE_PVP_SEARCH_USER_REQ(string searchKeyworkd)
		{
			NKMPacket_PRIVATE_PVP_SEARCH_USER_REQ nkmpacket_PRIVATE_PVP_SEARCH_USER_REQ = new NKMPacket_PRIVATE_PVP_SEARCH_USER_REQ();
			nkmpacket_PRIVATE_PVP_SEARCH_USER_REQ.searchKeyword = searchKeyworkd;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_PRIVATE_PVP_SEARCH_USER_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003AC9 RID: 15049 RVA: 0x0012E3EC File Offset: 0x0012C5EC
		public static void OnRecv(NKMPacket_PRIVATE_PVP_SEARCH_USER_ACK sPacket)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAUNTLET_PRIVATE_ROOM)
			{
				return;
			}
			if (NKCPrivatePVPRoomMgr.m_pvpGameLobbyState == null)
			{
				return;
			}
			if (NKCPrivatePVPRoomMgr.m_pvpGameLobbyState.gameState != NKM_GAME_STATE.NGS_LOBBY_MATCHING)
			{
				return;
			}
			NKCPrivatePVPRoomMgr.m_searchResult = sPacket.list;
			if (NKCPrivatePVPRoomMgr.m_searchResult.Count == 0)
			{
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_FRIEND_SEARCH_RESULT_EMPTY, null, "");
			}
			NKC_SCEN_GAUNTLET_PRIVATE_ROOM nkc_SCEN_GAUNTLET_PRIVATE_ROOM = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_PRIVATE_ROOM();
			if (nkc_SCEN_GAUNTLET_PRIVATE_ROOM != null)
			{
				NKCUIGauntletPrivateRoom gauntletPrivateRoom = nkc_SCEN_GAUNTLET_PRIVATE_ROOM.GauntletPrivateRoom;
				if (gauntletPrivateRoom == null)
				{
					return;
				}
				NKCUIGauntletPrivateRoomInvite uigauntletPrivateRoomInvite = gauntletPrivateRoom.UIGauntletPrivateRoomInvite;
				if (uigauntletPrivateRoomInvite == null)
				{
					return;
				}
				uigauntletPrivateRoomInvite.SetUI();
			}
		}

		// Token: 0x06003ACA RID: 15050 RVA: 0x0012E46D File Offset: 0x0012C66D
		public static void Send_NKMPacket_PVP_ROOM_CHANGE_ROLE_REQ()
		{
			new NKMPacket_PVP_ROOM_CHANGE_ROLE_REQ();
		}

		// Token: 0x06003ACB RID: 15051 RVA: 0x0012E475 File Offset: 0x0012C675
		public static void OnRecv(NKMPacket_PVP_ROOM_CHANGE_ROLE_ACK sPacket)
		{
			NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM);
		}

		// Token: 0x06003ACC RID: 15052 RVA: 0x0012E480 File Offset: 0x0012C680
		public static void Send_NKMPacket_PRIVATE_PVP_EXIT_REQ()
		{
			NKMPacket_PRIVATE_PVP_EXIT_REQ packet = new NKMPacket_PRIVATE_PVP_EXIT_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003ACD RID: 15053 RVA: 0x0012E4A6 File Offset: 0x0012C6A6
		public static void OnRecv(NKMPacket_PRIVATE_PVP_EXIT_ACK cNKMPacket_PRIVATE_PVP_EXIT_ACK)
		{
			NKCPopupGauntletInvite.ClosePopupBox();
			NKCPopupOKCancel.ClosePopupBox();
			NKCPrivatePVPRoomMgr.CancelAllProcess();
		}

		// Token: 0x06003ACE RID: 15054 RVA: 0x0012E4B8 File Offset: 0x0012C6B8
		public static void OnRecv(NKMPacket_PRIVATE_PVP_CANCEL_NOT sPacket)
		{
			if (NKCPrivatePVPRoomMgr.m_pvpGameLobbyState == null)
			{
				return;
			}
			if (!NKCPrivatePVPRoomMgr.IsPlayer(sPacket.targetUserUid))
			{
				Log.Debug(string.Format("[PrivatePvpRoom] TargetUser[{0}] CancelType[{1}]", sPacket.targetUserUid, sPacket.cancelType.ToString()), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCPrivatePVPRoomMgr.cs", 616);
				return;
			}
			NKCPopupGauntletInvite.ClosePopupBox();
			NKCPopupOKCancel.ClosePopupBox();
			NKCPrivatePVPRoomMgr.ShowCancelNoticePopup(sPacket.cancelType);
		}

		// Token: 0x06003ACF RID: 15055 RVA: 0x0012E528 File Offset: 0x0012C728
		public static void OnRecv(NKMPacket_PRIVATE_PVP_LOBBY_STATE_NOT cNKMPacket_PRIVATE_PVP_LOBBY_STATE_NOT)
		{
			if (NKCPrivatePVPRoomMgr.m_pvpGameLobbyState == null)
			{
				return;
			}
			NKCPrivatePVPRoomMgr.SetRoomData(cNKMPacket_PRIVATE_PVP_LOBBY_STATE_NOT.lobbyState);
			NKM_GAME_STATE gameState = NKCPrivatePVPRoomMgr.m_pvpGameLobbyState.gameState;
			if (gameState != NKM_GAME_STATE.NGS_LOBBY_MATCHING)
			{
				if (gameState != NKM_GAME_STATE.NGS_LOBBY_GAME_SETTING)
				{
					return;
				}
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAUNTLET_PRIVATE_ROOM_DECK_SELECT)
				{
					NKCPrivatePVPRoomMgr.CancelInviteProcess();
					if (!NKCPrivatePVPRoomMgr.m_pvpGameLobbyState.config.draftBanMode)
					{
						NKCPrivatePVPRoomMgr.ChangeScene(NKM_SCEN_ID.NSI_GAUNTLET_PRIVATE_ROOM_DECK_SELECT);
						return;
					}
				}
				else if (NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_PRIVATE_ROOM() != null && !NKCPrivatePVPRoomMgr.m_pvpGameLobbyState.config.draftBanMode)
				{
					NKCUIGauntletPrivateRoomDeckSelect privateRoomDeckSelectUI = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_PRIVATE_ROOM_DECK_SELECT().m_PrivateRoomDeckSelectUI;
					if (privateRoomDeckSelectUI == null)
					{
						return;
					}
					privateRoomDeckSelectUI.RefreshUI();
				}
			}
			else
			{
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAUNTLET_PRIVATE_ROOM)
				{
					NKCPrivatePVPRoomMgr.ChangeScene(NKM_SCEN_ID.NSI_GAUNTLET_PRIVATE_ROOM);
					return;
				}
				if (NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_PRIVATE_ROOM() != null)
				{
					NKCUIGauntletPrivateRoom gauntletPrivateRoom = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_PRIVATE_ROOM().GauntletPrivateRoom;
					if (gauntletPrivateRoom == null)
					{
						return;
					}
					gauntletPrivateRoom.RefreshUI();
					return;
				}
			}
		}

		// Token: 0x06003AD0 RID: 15056 RVA: 0x0012E5FC File Offset: 0x0012C7FC
		public static void Send_NKMPacket_PVP_ROOM_START_GAME_SETTING_REQ()
		{
			NKMPacket_PVP_ROOM_START_GAME_SETTING_REQ packet = new NKMPacket_PVP_ROOM_START_GAME_SETTING_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003AD1 RID: 15057 RVA: 0x0012E622 File Offset: 0x0012C822
		public static void OnRecv(NKMPacket_PVP_ROOM_START_GAME_SETTING_ACK sPacket)
		{
			NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM);
		}

		// Token: 0x06003AD2 RID: 15058 RVA: 0x0012E62C File Offset: 0x0012C82C
		public static void Send_NKMPacket_PRIVATE_PVP_SYNC_DECK_INDEX_REQ(NKMDeckIndex selectedDeckIndex)
		{
			NKCPrivatePVPRoomMgr.GetMyPvpGameLobbyUserState().deckIndex = selectedDeckIndex;
			NKMPacket_PRIVATE_PVP_SYNC_DECK_INDEX_REQ nkmpacket_PRIVATE_PVP_SYNC_DECK_INDEX_REQ = new NKMPacket_PRIVATE_PVP_SYNC_DECK_INDEX_REQ();
			nkmpacket_PRIVATE_PVP_SYNC_DECK_INDEX_REQ.deckIndex = selectedDeckIndex;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_PRIVATE_PVP_SYNC_DECK_INDEX_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003AD3 RID: 15059 RVA: 0x0012E664 File Offset: 0x0012C864
		public static void Send_NKMPacket_PRIVATE_PVP_READY_REQ(NKMDeckIndex selectedDeckIndex)
		{
			NKMPacket_PRIVATE_PVP_READY_REQ nkmpacket_PRIVATE_PVP_READY_REQ = new NKMPacket_PRIVATE_PVP_READY_REQ();
			nkmpacket_PRIVATE_PVP_READY_REQ.deckIndex = selectedDeckIndex;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_PRIVATE_PVP_READY_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003AD4 RID: 15060 RVA: 0x0012E691 File Offset: 0x0012C891
		public static void OnRecv(NKMPacket_PRIVATE_PVP_READY_ACK packet)
		{
			NKMPvpGameLobbyState pvpGameLobbyState = NKCPrivatePVPRoomMgr.m_pvpGameLobbyState;
		}

		// Token: 0x06003AD5 RID: 15061 RVA: 0x0012E699 File Offset: 0x0012C899
		public static void OnRecv(NKMPacket_LEAGUE_PVP_ACCEPT_NOT sPacket)
		{
			if (NKCPrivatePVPRoomMgr.m_pvpGameLobbyState == null)
			{
				return;
			}
			NKCPrivatePVPRoomMgr.ResetData();
		}

		// Token: 0x06003AD6 RID: 15062 RVA: 0x0012E6A8 File Offset: 0x0012C8A8
		public static void OnRecv(NKMPacket_PVP_GAME_MATCH_COMPLETE_NOT cNKMPacket_PVP_GAME_MATCH_COMPLETE_NOT)
		{
			NKCPrivatePVPRoomMgr.ResetData();
		}

		// Token: 0x06003AD7 RID: 15063 RVA: 0x0012E6B0 File Offset: 0x0012C8B0
		public static void ProcessReLogin(NKMGameData gameData, NKMPvpGameLobbyState pvpGameLobbyState)
		{
			if (pvpGameLobbyState.gameState != NKM_GAME_STATE.NGS_LOBBY_MATCHING || pvpGameLobbyState.gameState != NKM_GAME_STATE.NGS_LOBBY_GAME_SETTING)
			{
				return;
			}
			NKCPrivatePVPRoomMgr.SetRoomData(pvpGameLobbyState);
			NKM_GAME_STATE gameState = NKCPrivatePVPRoomMgr.m_pvpGameLobbyState.gameState;
			if (gameState != NKM_GAME_STATE.NGS_LOBBY_MATCHING)
			{
				if (gameState != NKM_GAME_STATE.NGS_LOBBY_GAME_SETTING)
				{
					return;
				}
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAUNTLET_PRIVATE_ROOM_DECK_SELECT)
				{
					NKCPrivatePVPRoomMgr.ChangeScene(NKM_SCEN_ID.NSI_GAUNTLET_PRIVATE_ROOM_DECK_SELECT);
					return;
				}
				if (NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_PRIVATE_ROOM() != null)
				{
					NKCUIGauntletPrivateRoomDeckSelect privateRoomDeckSelectUI = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_PRIVATE_ROOM_DECK_SELECT().m_PrivateRoomDeckSelectUI;
					if (privateRoomDeckSelectUI == null)
					{
						return;
					}
					privateRoomDeckSelectUI.RefreshUI();
				}
			}
			else
			{
				if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAUNTLET_PRIVATE_ROOM)
				{
					NKCPrivatePVPRoomMgr.ChangeScene(NKM_SCEN_ID.NSI_GAUNTLET_PRIVATE_ROOM);
					return;
				}
				if (NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_PRIVATE_ROOM() != null)
				{
					NKCUIGauntletPrivateRoom gauntletPrivateRoom = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_PRIVATE_ROOM().GauntletPrivateRoom;
					if (gauntletPrivateRoom == null)
					{
						return;
					}
					gauntletPrivateRoom.RefreshUI();
					return;
				}
			}
		}

		// Token: 0x04003527 RID: 13607
		private static NKMPvpGameLobbyState m_pvpGameLobbyState = null;

		// Token: 0x04003528 RID: 13608
		private static bool isInviteSender = false;

		// Token: 0x04003529 RID: 13609
		private static NKMUserProfileData m_targetProfile;

		// Token: 0x0400352A RID: 13610
		private static FriendListData m_targetFriendListData;

		// Token: 0x0400352B RID: 13611
		private static long m_currentInviteUserUID = 0L;

		// Token: 0x0400352C RID: 13612
		private static float WaitTimeSenderInviteReq = 10f;

		// Token: 0x0400352D RID: 13613
		private static List<FriendListData> m_searchResult = new List<FriendListData>();

		// Token: 0x0400352E RID: 13614
		private static NKMPrivateGameConfig m_privateGameConfig = null;
	}
}
