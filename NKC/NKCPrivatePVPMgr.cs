using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Pvp;
using ClientPacket.User;
using Cs.Logging;
using NKC.PacketHandler;
using NKC.UI;
using NKC.UI.Gauntlet;
using NKC.UI.Option;
using NKC.UI.Shop;
using NKM;

namespace NKC
{
	// Token: 0x020006BB RID: 1723
	public static class NKCPrivatePVPMgr
	{
		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06003A75 RID: 14965 RVA: 0x0012D0A4 File Offset: 0x0012B2A4
		public static NKMPrivateGameConfig PrivateGameConfig
		{
			get
			{
				if (NKCPrivatePVPMgr.m_privateGameConfig == null)
				{
					NKCPrivatePVPMgr.m_privateGameConfig = new NKMPrivateGameConfig();
					NKCPrivatePVPMgr.m_privateGameConfig.applyEquipStat = true;
				}
				return NKCPrivatePVPMgr.m_privateGameConfig;
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06003A76 RID: 14966 RVA: 0x0012D0C7 File Offset: 0x0012B2C7
		public static bool CancelNotPopupOpened
		{
			get
			{
				return NKCPrivatePVPMgr.isCancelNotPopupOpened;
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06003A77 RID: 14967 RVA: 0x0012D0CE File Offset: 0x0012B2CE
		public static bool PrivatePVPLobbyBanUpState
		{
			get
			{
				return NKCPrivatePVPMgr.m_pvpGameLobbyState != null && NKCPrivatePVPMgr.m_pvpGameLobbyState.config.applyBanUpSystem;
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06003A78 RID: 14968 RVA: 0x0012D0E8 File Offset: 0x0012B2E8
		public static List<FriendListData> SearchResult
		{
			get
			{
				return NKCPrivatePVPMgr.m_searchResult;
			}
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x0012D0EF File Offset: 0x0012B2EF
		public static void ResetData()
		{
			NKCPrivatePVPMgr.m_searchResult.Clear();
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x0012D0FB File Offset: 0x0012B2FB
		public static bool IsInProgress()
		{
			return NKCPrivatePVPMgr.isInviteSender || NKCPrivatePVPMgr.m_targetProfile != null || NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_PRIVATE_READY;
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x0012D120 File Offset: 0x0012B320
		public static void CancelAllProcess()
		{
			NKCPrivatePVPMgr.isCancelNotPopupOpened = false;
			NKCPrivatePVPMgr.isInviteSender = false;
			NKCPrivatePVPMgr.m_targetProfile = null;
			NKCPrivatePVPMgr.m_targetFriendListData = null;
			NKCPrivatePVPMgr.m_pvpGameLobbyState = null;
			NKCPrivatePVPMgr.m_myPrivatePVPState = NKCPrivatePVPMgr.PrivatePVPState.PPS_None;
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_PRIVATE_READY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_PRIVATE_READY().OnCancelAllProcess();
			}
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x0012D170 File Offset: 0x0012B370
		public static void SendInvite(FriendListData friendListData)
		{
			NKCPrivatePVPMgr.m_myPrivatePVPState = NKCPrivatePVPMgr.PrivatePVPState.PPS_InviteSent;
			NKCPrivatePVPMgr.isInviteSender = true;
			NKCPrivatePVPMgr.m_targetProfile = null;
			NKCPrivatePVPMgr.m_targetFriendListData = friendListData;
			NKCPrivatePVPMgr.m_pvpGameLobbyState = null;
			NKCPrivatePVPMgr.m_targetUserUID = friendListData.commonProfile.userUid;
			NKMPacket_PRIVATE_PVP_INVITE_REQ nkmpacket_PRIVATE_PVP_INVITE_REQ = new NKMPacket_PRIVATE_PVP_INVITE_REQ();
			nkmpacket_PRIVATE_PVP_INVITE_REQ.friendCode = friendListData.commonProfile.friendCode;
			nkmpacket_PRIVATE_PVP_INVITE_REQ.config = NKCPrivatePVPMgr.m_privateGameConfig;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_PRIVATE_PVP_INVITE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x0012D1E0 File Offset: 0x0012B3E0
		public static void OnRecv(NKMPacket_PRIVATE_PVP_INVITE_NOT sPacket)
		{
			if (NKCPrivatePVPMgr.isInviteSender || NKCPrivatePVPMgr.m_targetProfile != null)
			{
				NKCPrivatePVPMgr.SendInviteAccept(false, sPacket.senderProfile.commonProfile.userUid);
				return;
			}
			if (NKCUIShop.IsInstanceOpen)
			{
				NKCPrivatePVPMgr.SendInviteAccept(false, sPacket.senderProfile.commonProfile.userUid);
				return;
			}
			NKM_SCEN_ID nowScenID = NKCScenManager.GetScenManager().GetNowScenID();
			if (nowScenID <= NKM_SCEN_ID.NSI_COLLECTION)
			{
				if (nowScenID == NKM_SCEN_ID.NSI_HOME || nowScenID == NKM_SCEN_ID.NSI_COLLECTION)
				{
					goto IL_86;
				}
			}
			else if (nowScenID == NKM_SCEN_ID.NSI_FRIEND || nowScenID - NKM_SCEN_ID.NSI_GAUNTLET_INTRO <= 1 || nowScenID == NKM_SCEN_ID.NSI_GUILD_LOBBY)
			{
				goto IL_86;
			}
			NKCPrivatePVPMgr.SendInviteAccept(false, sPacket.senderProfile.commonProfile.userUid);
			return;
			IL_86:
			NKCPrivatePVPMgr.m_myPrivatePVPState = NKCPrivatePVPMgr.PrivatePVPState.PPS_InviteReceived;
			NKCPrivatePVPMgr.isInviteSender = false;
			NKCPrivatePVPMgr.m_targetProfile = sPacket.senderProfile;
			NKCPrivatePVPMgr.m_targetUserUID = NKCPrivatePVPMgr.m_targetProfile.commonProfile.userUid;
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_HOME)
			{
				NKC_SCEN_HOME scen_HOME = NKCScenManager.GetScenManager().Get_SCEN_HOME();
				if (scen_HOME != null)
				{
					scen_HOME.UnhideLobbyUI();
				}
			}
			NKCSoundManager.SetMute(NKCScenManager.GetScenManager().GetGameOptionData().SoundMute, true);
			NKCPopupGauntletInvite.OpenOKCancelTimerBox(NKCUtilString.GET_STRING_FRIEND_PVP, NKCUtilString.GET_STRING_PRIVATE_PVP_INVITE_NOT, new NKCPopupGauntletInvite.OnButton(NKCPrivatePVPMgr.OnClickAcceptInvite), new NKCPopupGauntletInvite.OnButton(NKCPrivatePVPMgr.OnClickRefuseInvite), (float)sPacket.timeoutDurationSec, NKCUtilString.GET_STRING_PRIVATE_PVP_AUTO_CANCEL_ID, NKCUtilString.GET_STRING_ACCEPT, NKCUtilString.GET_STRING_REFUSE, NKCPrivatePVPMgr.m_targetProfile, sPacket.config);
		}

		// Token: 0x06003A7E RID: 14974 RVA: 0x0012D31C File Offset: 0x0012B51C
		public static void OnRecv(NKMPacket_PRIVATE_PVP_INVITE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				NKCPrivatePVPMgr.CancelAllProcess();
				return;
			}
			NKCPrivatePVPMgr.isInviteSender = true;
			NKCPopupGauntletInvite.ClosePopupBox();
			NKCPopupGauntletInvite.OpenOKTimerBox(NKCUtilString.GET_STRING_FRIEND_PVP, NKCUtilString.GET_STRING_PRIVATE_PVP_INVITE_REQ, new NKCPopupGauntletInvite.OnButton(NKCPrivatePVPMgr.SendInviteCancel), NKCPrivatePVPMgr.WaitTimeSenderInviteReq, NKCUtilString.GET_STRING_CANCEL, NKCUtilString.GET_STRING_PRIVATE_PVP_AUTO_CANCEL_ID, NKCPrivatePVPMgr.m_targetFriendListData, NKCPrivatePVPMgr.m_privateGameConfig);
		}

		// Token: 0x06003A7F RID: 14975 RVA: 0x0012D384 File Offset: 0x0012B584
		public static void SendInviteCancel()
		{
			NKMPacket_PRIVATE_PVP_CANCEL_REQ nkmpacket_PRIVATE_PVP_CANCEL_REQ = new NKMPacket_PRIVATE_PVP_CANCEL_REQ();
			nkmpacket_PRIVATE_PVP_CANCEL_REQ.targetUserUid = NKCPrivatePVPMgr.GetTargetUserUID();
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_PRIVATE_PVP_CANCEL_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A80 RID: 14976 RVA: 0x0012D3B8 File Offset: 0x0012B5B8
		public static void OnRecv(NKMPacket_PRIVATE_PVP_ACCEPT_ACK sPacket)
		{
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
		}

		// Token: 0x06003A81 RID: 14977 RVA: 0x0012D411 File Offset: 0x0012B611
		public static void OnRecv(NKMPacket_PRIVATE_PVP_CANCEL_ACK sPacket)
		{
			NKCPopupGauntletInvite.ClosePopupBox();
			NKCPopupOKCancel.ClosePopupBox();
			NKMPopUpBox.CloseWaitBox();
			NKCPrivatePVPMgr.CancelAllProcess();
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x0012D428 File Offset: 0x0012B628
		public static void OnRecv(NKMPacket_PRIVATE_PVP_CANCEL_NOT sPacket)
		{
			if (NKCPrivatePVPMgr.m_targetUserUID != 0L && sPacket.targetUserUid != NKCPrivatePVPMgr.m_targetUserUID)
			{
				Log.Debug(string.Format("[PrivatePvp] TargetUser[{0}] CancelType[{1}]", sPacket.targetUserUid, sPacket.cancelType.ToString()), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCPrivatePVPMgr.cs", 240);
				return;
			}
			NKCPopupGauntletInvite.ClosePopupBox();
			NKCPopupOKCancel.ClosePopupBox();
			NKCPrivatePVPMgr.ShowCancelNoticePopup(sPacket.cancelType);
		}

		// Token: 0x06003A83 RID: 14979 RVA: 0x0012D494 File Offset: 0x0012B694
		public static void ShowCancelNoticePopup(PrivatePvpCancelType cancelType)
		{
			if (cancelType == PrivatePvpCancelType.IRejectInvitation)
			{
				return;
			}
			string str = "SI_DP_PRIVATE_PVP_CANCEL_NOT_";
			int num = (int)cancelType;
			string @string = NKCStringTable.GetString(str + num.ToString(), false);
			NKCPrivatePVPMgr.isCancelNotPopupOpened = true;
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_WARNING, @string, new NKCPopupOKCancel.OnButton(NKCPrivatePVPMgr.CancelAllProcess), "");
		}

		// Token: 0x06003A84 RID: 14980 RVA: 0x0012D4E2 File Offset: 0x0012B6E2
		public static void OnRecv(NKMPacket_PRIVATE_PVP_ACCEPT_NOT sPacket)
		{
			NKCPopupGauntletInvite.ClosePopupBox();
			NKCPrivatePVPMgr.m_targetProfile = null;
			NKCPrivatePVPMgr.m_targetUserUID = 0L;
			NKCPrivatePVPMgr.m_pvpGameLobbyState = sPacket.lobbyState;
			NKCPrivatePVPMgr.ChangeScene();
		}

		// Token: 0x06003A85 RID: 14981 RVA: 0x0012D506 File Offset: 0x0012B706
		public static void ChangeScene()
		{
			NKCPrivatePVPMgr.m_myPrivatePVPState = NKCPrivatePVPMgr.PrivatePVPState.PPS_MatchReadyDeckSelect;
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_PRIVATE_READY, true);
		}

		// Token: 0x06003A86 RID: 14982 RVA: 0x0012D51C File Offset: 0x0012B71C
		public static void SendInviteAccept(bool bAccept, long targetUserUID)
		{
			NKMPacket_PRIVATE_PVP_ACCEPT_REQ nkmpacket_PRIVATE_PVP_ACCEPT_REQ = new NKMPacket_PRIVATE_PVP_ACCEPT_REQ();
			nkmpacket_PRIVATE_PVP_ACCEPT_REQ.targetUserUid = targetUserUID;
			nkmpacket_PRIVATE_PVP_ACCEPT_REQ.accept = bAccept;
			if (!bAccept && targetUserUID != NKCPrivatePVPMgr.GetTargetUserUID())
			{
				NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_PRIVATE_PVP_ACCEPT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, bAccept);
				return;
			}
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_PRIVATE_PVP_ACCEPT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, bAccept);
		}

		// Token: 0x06003A87 RID: 14983 RVA: 0x0012D56F File Offset: 0x0012B76F
		public static void OnClickAcceptInvite()
		{
			NKCPrivatePVPMgr.SendInviteAccept(true, NKCPrivatePVPMgr.GetTargetUserUID());
		}

		// Token: 0x06003A88 RID: 14984 RVA: 0x0012D57C File Offset: 0x0012B77C
		public static void OnClickRefuseInvite()
		{
			NKCPrivatePVPMgr.SendInviteAccept(false, NKCPrivatePVPMgr.GetTargetUserUID());
			NKCPrivatePVPMgr.CancelAllProcess();
		}

		// Token: 0x06003A89 RID: 14985 RVA: 0x0012D590 File Offset: 0x0012B790
		public static void SendDeckSelectComplete(NKMDeckIndex selectedDeckIndex)
		{
			NKMPacket_PRIVATE_PVP_READY_REQ nkmpacket_PRIVATE_PVP_READY_REQ = new NKMPacket_PRIVATE_PVP_READY_REQ();
			nkmpacket_PRIVATE_PVP_READY_REQ.deckIndex = selectedDeckIndex;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_PRIVATE_PVP_READY_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003A8A RID: 14986 RVA: 0x0012D5BD File Offset: 0x0012B7BD
		public static void OnRecv(NKMPacket_PRIVATE_PVP_READY_ACK packet)
		{
			NKCPrivatePVPMgr.SetReady();
			if (NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_PRIVATE_READY() != null)
			{
				NKCUIGauntletPrivateReady privateReadyUI = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_PRIVATE_READY().m_PrivateReadyUI;
				if (privateReadyUI == null)
				{
					return;
				}
				privateReadyUI.RefreshUI();
			}
		}

		// Token: 0x06003A8B RID: 14987 RVA: 0x0012D5EC File Offset: 0x0012B7EC
		public static void SetReady()
		{
			NKMPvpGameLobbyUserState myPvpGameLobbyUserState = NKCPrivatePVPMgr.GetMyPvpGameLobbyUserState();
			if (myPvpGameLobbyUserState != null)
			{
				myPvpGameLobbyUserState.isReady = true;
			}
			NKCPrivatePVPMgr.m_myPrivatePVPState = NKCPrivatePVPMgr.PrivatePVPState.PPS_MatchReadyDeckComplete;
		}

		// Token: 0x06003A8C RID: 14988 RVA: 0x0012D60F File Offset: 0x0012B80F
		public static bool CanEditDeck()
		{
			return NKCPrivatePVPMgr.m_myPrivatePVPState == NKCPrivatePVPMgr.PrivatePVPState.PPS_MatchReadyDeckSelect;
		}

		// Token: 0x06003A8D RID: 14989 RVA: 0x0012D61C File Offset: 0x0012B81C
		public static long GetTargetUserUID()
		{
			return NKCPrivatePVPMgr.m_targetUserUID;
		}

		// Token: 0x06003A8E RID: 14990 RVA: 0x0012D624 File Offset: 0x0012B824
		public static void ProcessReLogin(NKMGameData gameData, NKMPvpGameLobbyState pvpGameLobbyState)
		{
			NKCPrivatePVPMgr.m_pvpGameLobbyState = pvpGameLobbyState;
			NKCPrivatePVPMgr.m_myPrivatePVPState = NKCPrivatePVPMgr.PrivatePVPState.PPS_MatchReadyDeckSelect;
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAUNTLET_PRIVATE_READY)
			{
				NKCPrivatePVPMgr.ChangeScene();
				return;
			}
			if (NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_PRIVATE_READY() != null)
			{
				NKCUIGauntletPrivateReady privateReadyUI = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_PRIVATE_READY().m_PrivateReadyUI;
				if (privateReadyUI == null)
				{
					return;
				}
				privateReadyUI.RefreshUI();
			}
		}

		// Token: 0x06003A8F RID: 14991 RVA: 0x0012D676 File Offset: 0x0012B876
		public static void OnRecv(NKMPacket_PRIVATE_PVP_LOBBY_STATE_NOT cNKMPacket_PRIVATE_PVP_LOBBY_STATE_NOT)
		{
			NKCPrivatePVPMgr.m_pvpGameLobbyState = cNKMPacket_PRIVATE_PVP_LOBBY_STATE_NOT.lobbyState;
			if (NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_PRIVATE_READY() != null)
			{
				NKCUIGauntletPrivateReady privateReadyUI = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_PRIVATE_READY().m_PrivateReadyUI;
				if (privateReadyUI == null)
				{
					return;
				}
				privateReadyUI.RefreshUI();
			}
		}

		// Token: 0x06003A90 RID: 14992 RVA: 0x0012D6A8 File Offset: 0x0012B8A8
		public static NKMPvpGameLobbyUserState GetMyPvpGameLobbyUserState()
		{
			if (NKCPrivatePVPMgr.m_pvpGameLobbyState == null)
			{
				return null;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			foreach (NKMPvpGameLobbyUserState nkmpvpGameLobbyUserState in NKCPrivatePVPMgr.m_pvpGameLobbyState.users)
			{
				if (nkmpvpGameLobbyUserState.profileData.commonProfile.userUid == myUserData.m_UserUID)
				{
					return nkmpvpGameLobbyUserState;
				}
			}
			return null;
		}

		// Token: 0x06003A91 RID: 14993 RVA: 0x0012D72C File Offset: 0x0012B92C
		public static NKMPvpGameLobbyUserState GetTargetPvpGameLobbyUserState()
		{
			if (NKCPrivatePVPMgr.m_pvpGameLobbyState == null)
			{
				return null;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			foreach (NKMPvpGameLobbyUserState nkmpvpGameLobbyUserState in NKCPrivatePVPMgr.m_pvpGameLobbyState.users)
			{
				if (nkmpvpGameLobbyUserState.profileData.commonProfile.userUid != myUserData.m_UserUID)
				{
					return nkmpvpGameLobbyUserState;
				}
			}
			return null;
		}

		// Token: 0x06003A92 RID: 14994 RVA: 0x0012D7B0 File Offset: 0x0012B9B0
		public static void OnRecv(NKMPacket_PVP_GAME_MATCH_COMPLETE_NOT cNKMPacket_PVP_GAME_MATCH_COMPLETE_NOT)
		{
			NKCPrivatePVPMgr.isInviteSender = false;
			NKCPrivatePVPMgr.m_targetProfile = null;
			NKCPrivatePVPMgr.m_pvpGameLobbyState = null;
			NKCPrivatePVPMgr.m_myPrivatePVPState = NKCPrivatePVPMgr.PrivatePVPState.PPS_None;
		}

		// Token: 0x06003A93 RID: 14995 RVA: 0x0012D7CA File Offset: 0x0012B9CA
		public static void OnRecv(NKMPacket_LEAGUE_PVP_ACCEPT_NOT cNKMPacket_LEAGUE_PVP_ACCEPT_NOT)
		{
			NKCPrivatePVPMgr.isInviteSender = false;
			NKCPrivatePVPMgr.m_targetProfile = null;
			NKCPrivatePVPMgr.m_targetUserUID = 0L;
			NKCPrivatePVPMgr.m_pvpGameLobbyState = null;
			NKCPrivatePVPMgr.m_myPrivatePVPState = NKCPrivatePVPMgr.PrivatePVPState.PPS_None;
		}

		// Token: 0x06003A94 RID: 14996 RVA: 0x0012D7EC File Offset: 0x0012B9EC
		public static void SendReadyCancel()
		{
			NKMPacket_PRIVATE_PVP_EXIT_REQ packet = new NKMPacket_PRIVATE_PVP_EXIT_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A95 RID: 14997 RVA: 0x0012D812 File Offset: 0x0012BA12
		public static void OnRecv(NKMPacket_PRIVATE_PVP_EXIT_ACK cNKMPacket_PRIVATE_PVP_EXIT_ACK)
		{
			NKCPopupGauntletInvite.ClosePopupBox();
			NKCPopupOKCancel.ClosePopupBox();
			NKCPrivatePVPMgr.CancelAllProcess();
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x0012D824 File Offset: 0x0012BA24
		public static void Send_NKMPacket_UPDATE_PVP_INVITATION_OPTION_REQ(PrivatePvpInvitation invitationOption)
		{
			NKMPacket_UPDATE_PVP_INVITATION_OPTION_REQ nkmpacket_UPDATE_PVP_INVITATION_OPTION_REQ = new NKMPacket_UPDATE_PVP_INVITATION_OPTION_REQ();
			nkmpacket_UPDATE_PVP_INVITATION_OPTION_REQ.value = invitationOption;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_UPDATE_PVP_INVITATION_OPTION_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A97 RID: 14999 RVA: 0x0012D854 File Offset: 0x0012BA54
		public static void OnRecv(NKMPacket_UPDATE_PVP_INVITATION_OPTION_ACK sPacket)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			if (gameOptionData.ePrivatePvpInviteOption != sPacket.value)
			{
				gameOptionData.ePrivatePvpInviteOption = sPacket.value;
			}
			gameOptionData.Save();
			NKCUIGameOption.CheckInstanceAndClose();
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME)
			{
				NKCScenManager.GetScenManager().GetGameClient().GetGameHud();
			}
		}

		// Token: 0x06003A98 RID: 15000 RVA: 0x0012D8B4 File Offset: 0x0012BAB4
		public static void Send_NKMPacket_PRIVATE_PVP_SYNC_DECK_INDEX_REQ(NKMDeckIndex selectedDeckIndex)
		{
			NKCPrivatePVPMgr.GetMyPvpGameLobbyUserState().deckIndex = selectedDeckIndex;
			NKMPacket_PRIVATE_PVP_SYNC_DECK_INDEX_REQ nkmpacket_PRIVATE_PVP_SYNC_DECK_INDEX_REQ = new NKMPacket_PRIVATE_PVP_SYNC_DECK_INDEX_REQ();
			nkmpacket_PRIVATE_PVP_SYNC_DECK_INDEX_REQ.deckIndex = selectedDeckIndex;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_PRIVATE_PVP_SYNC_DECK_INDEX_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A99 RID: 15001 RVA: 0x0012D8EC File Offset: 0x0012BAEC
		public static void OnRecv(NKMPacket_PRIVATE_PVP_SYNC_DECK_INDEX_ACK sPacket)
		{
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x0012D8F0 File Offset: 0x0012BAF0
		public static void Send_NKMPacket_PRIVATE_PVP_SEARCH_USER_REQ(string searchKeyworkd)
		{
			NKMPacket_PRIVATE_PVP_SEARCH_USER_REQ nkmpacket_PRIVATE_PVP_SEARCH_USER_REQ = new NKMPacket_PRIVATE_PVP_SEARCH_USER_REQ();
			nkmpacket_PRIVATE_PVP_SEARCH_USER_REQ.searchKeyword = searchKeyworkd;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_PRIVATE_PVP_SEARCH_USER_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x0012D920 File Offset: 0x0012BB20
		public static void OnRecv(NKMPacket_PRIVATE_PVP_SEARCH_USER_ACK sPacket)
		{
			NKCPrivatePVPMgr.m_searchResult = sPacket.list;
			if (NKCPrivatePVPMgr.m_searchResult.Count == 0)
			{
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_FRIEND_SEARCH_RESULT_EMPTY, null, "");
			}
			NKC_SCEN_GAUNTLET_LOBBY nkc_SCEN_GAUNTLET_LOBBY = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY();
			if (nkc_SCEN_GAUNTLET_LOBBY != null)
			{
				NKCUIGauntletLobbyCustom gauntletLobbyCustom = nkc_SCEN_GAUNTLET_LOBBY.GauntletLobbyCustom;
				if (gauntletLobbyCustom != null)
				{
					gauntletLobbyCustom.SetUI();
				}
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

		// Token: 0x06003A9C RID: 15004 RVA: 0x0012D99B File Offset: 0x0012BB9B
		public static void SetApplyEquipSet(bool value)
		{
			NKCPrivatePVPMgr.m_privateGameConfig.applyEquipStat = value;
		}

		// Token: 0x06003A9D RID: 15005 RVA: 0x0012D9A8 File Offset: 0x0012BBA8
		public static void SetApplyAllUnitMaxLevel(bool value)
		{
			NKCPrivatePVPMgr.m_privateGameConfig.applyAllUnitMaxLevel = value;
		}

		// Token: 0x06003A9E RID: 15006 RVA: 0x0012D9B5 File Offset: 0x0012BBB5
		public static void SetApplyBanUp(bool value)
		{
			NKCPrivatePVPMgr.m_privateGameConfig.applyBanUpSystem = value;
		}

		// Token: 0x06003A9F RID: 15007 RVA: 0x0012D9C2 File Offset: 0x0012BBC2
		public static void SetDraftBanMode(bool value)
		{
			NKCPrivatePVPMgr.m_privateGameConfig.draftBanMode = value;
		}

		// Token: 0x0400351D RID: 13597
		private static bool isInviteSender = false;

		// Token: 0x0400351E RID: 13598
		private static NKMUserProfileData m_targetProfile;

		// Token: 0x0400351F RID: 13599
		private static FriendListData m_targetFriendListData;

		// Token: 0x04003520 RID: 13600
		private static long m_targetUserUID = 0L;

		// Token: 0x04003521 RID: 13601
		private static bool isCancelNotPopupOpened = false;

		// Token: 0x04003522 RID: 13602
		private static float WaitTimeSenderInviteReq = 10f;

		// Token: 0x04003523 RID: 13603
		private static NKCPrivatePVPMgr.PrivatePVPState m_myPrivatePVPState = NKCPrivatePVPMgr.PrivatePVPState.PPS_None;

		// Token: 0x04003524 RID: 13604
		private static NKMPvpGameLobbyState m_pvpGameLobbyState = null;

		// Token: 0x04003525 RID: 13605
		private static NKMPrivateGameConfig m_privateGameConfig = null;

		// Token: 0x04003526 RID: 13606
		private static List<FriendListData> m_searchResult = new List<FriendListData>();

		// Token: 0x02001386 RID: 4998
		private enum PrivatePVPState
		{
			// Token: 0x04009A6D RID: 39533
			PPS_None,
			// Token: 0x04009A6E RID: 39534
			PPS_InviteSent,
			// Token: 0x04009A6F RID: 39535
			PPS_InviteReceived,
			// Token: 0x04009A70 RID: 39536
			PPS_MatchReadyDeckSelect,
			// Token: 0x04009A71 RID: 39537
			PPS_MatchReadyDeckComplete,
			// Token: 0x04009A72 RID: 39538
			PPS_MatchStarting
		}
	}
}
