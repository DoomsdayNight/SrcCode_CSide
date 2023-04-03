using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Account;
using ClientPacket.Chat;
using ClientPacket.Common;
using ClientPacket.Community;
using ClientPacket.Contract;
using ClientPacket.Event;
using ClientPacket.Game;
using ClientPacket.Guild;
using ClientPacket.Item;
using ClientPacket.LeaderBoard;
using ClientPacket.Mode;
using ClientPacket.Negotiation;
using ClientPacket.Office;
using ClientPacket.Pvp;
using ClientPacket.Raid;
using ClientPacket.Service;
using ClientPacket.Shop;
using ClientPacket.Unit;
using ClientPacket.User;
using ClientPacket.Warfare;
using ClientPacket.WorldMap;
using Cs.Logging;
using Cs.Protocol;
using NKC.Office;
using NKC.Patcher;
using NKC.Publisher;
using NKC.Trim;
using NKC.UI;
using NKC.UI.Collection;
using NKC.UI.Event;
using NKC.UI.Fierce;
using NKC.UI.Friend;
using NKC.UI.Gauntlet;
using NKC.UI.Guild;
using NKC.UI.Lobby;
using NKC.UI.Module;
using NKC.UI.NPC;
using NKC.UI.Office;
using NKC.UI.Option;
using NKC.UI.Result;
using NKC.UI.Shop;
using NKC.UI.Trim;
using NKC.UI.Worldmap;
using NKC.Util;
using NKM;
using NKM.Contract2;
using NKM.Event;
using NKM.Guild;
using NKM.Shop;
using NKM.Templet;
using NKM.Templet.Base;
using NKM.Templet.Office;
using UnityEngine;

namespace NKC.PacketHandler
{
	// Token: 0x0200089E RID: 2206
	public static class NKCPacketHandlersLobby
	{
		// Token: 0x06005812 RID: 22546 RVA: 0x001A642A File Offset: 0x001A462A
		public static void OnRecv(NKMPacket_HEART_BIT_ACK res)
		{
			NKCScenManager.GetScenManager().GetConnectGame().ResetHeartbitTimeout(16f);
			NKCScenManager.GetScenManager().GetConnectGame().SetPingTime(res.time);
			NKCPacketObjectPool.CloseObject(res);
		}

		// Token: 0x06005813 RID: 22547 RVA: 0x001A645B File Offset: 0x001A465B
		public static void OnRecv(NKMPacket_CONNECT_CHECK_ACK res)
		{
			NKMPopUpBox.CloseWaitBox();
			NKCScenManager.GetScenManager().SetAppEnableConnectCheckTime(-1f, false);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_HOME)
			{
				NKCScenManager.GetScenManager().Get_SCEN_HOME().OnReturnApp();
			}
		}

		// Token: 0x06005814 RID: 22548 RVA: 0x001A6490 File Offset: 0x001A4690
		public static void OnRecv(NKMPacket_JOIN_LOBBY_ACK res)
		{
			NKMPopUpBox.CloseWaitBox();
			NKCPopupOKCancel.OnButton onOK_Button = null;
			if (NKCPublisherModule.PublisherType == NKCPublisherModule.ePublisherType.NexonPC && (res.errorCode == NKM_ERROR_CODE.NEC_FAIL_NEXON_PC_FORCE_SHUTDOWN || res.errorCode == NKM_ERROR_CODE.NEC_FAIL_NEXON_PC_INVALID_AGE || res.errorCode == NKM_ERROR_CODE.NEC_FAIL_NEXON_PC_OPTIONAL_SHUTDOWN || res.errorCode == NKM_ERROR_CODE.NEC_FAIL_NEXON_PC_INVALID_AUTH_LEVEL || res.errorCode == NKM_ERROR_CODE.NEC_FAIL_UNDER_MAINTENANCE))
			{
				NKCScenManager.GetScenManager().GetConnectLogin().SetEnable(false);
				NKCScenManager.GetScenManager().GetConnectGame().SetEnable(false);
				onOK_Button = delegate()
				{
					NKCMain.QuitGame();
				};
			}
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(res.errorCode, true, onOK_Button, -2147483648))
			{
				Debug.LogWarningFormat("Login failed. result:{0}", new object[]
				{
					res.errorCode
				});
				if (res.errorCode == NKM_ERROR_CODE.kLoginFailure_LoggedInJustBefore)
				{
					NKCScenManager.GetScenManager().SetAppEnableConnectCheckTime(4f, true);
				}
				return;
			}
			NKCSynchronizedTime.OnRecv(res.utcTime, res.utcOffset);
			NKCStringTable.AddString(NKCStringTable.GetNationalCode(), "SI_SYSTEM_UTC_OFFSET", NKMTime.INTERVAL_FROM_UTC.ToString("+0;-#"), true);
			NKMTempletContainer<NKMIntervalTemplet>.Load(new List<NKMIntervalTemplet>(from e in res.intervalData
			select e), (NKMIntervalTemplet e) => e.StrKey);
			try
			{
				NKCTempletUtility.PostJoin();
				foreach (ContractTempletV2 contractTempletV in ContractTempletV2.Values)
				{
					contractTempletV.Validate();
				}
				foreach (MiscContractTemplet miscContractTemplet in MiscContractTemplet.Values)
				{
					miscContractTemplet.ValidateMiscContract();
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message + "\n" + ex.StackTrace, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCPacketHandlersLobby.cs", 136);
				if (!NKCPatchDownloader.Instance.ProloguePlay)
				{
					NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_INTERVAL_JOIN_FAILED, new NKCPopupOKCancel.OnButton(Application.Quit), "");
				}
			}
			NKCScenManager.GetScenManager().SetAppEnableConnectCheckTime(-1f, true);
			NKCScenManager.GetScenManager().GetConnectGame().SetReconnectKey(res.reconnectKey);
			NKCScenManager.GetScenManager().GetConnectGame().LoginComplete();
			Debug.Log("Login succeed.");
			NKCScenManager.GetScenManager().SetMyUserData(res.userData);
			NKCScenManager.GetScenManager().SetWarfareGameData(res.warfareGameData);
			NKCScenManager.GetScenManager().GetMyUserData().m_AsyncData = res.asyncPvpState;
			NKCScenManager.GetScenManager().GetMyUserData().m_LeagueData = res.leaguePvpState;
			NKCScenManager.GetScenManager().GetMyUserData().LastPvpPointChargeTimeUTC = NKCSynchronizedTime.ToUtcTime(res.pvpPointChargeTime);
			NKCScenManager.GetScenManager().GetMyUserData().m_LeaguePvpHistory = new PvpHistoryList();
			NKCScenManager.GetScenManager().GetMyUserData().m_LeaguePvpHistory.Add(res.leaguePvpHistories);
			NKCScenManager.GetScenManager().GetMyUserData().m_PrivatePvpHistory = new PvpHistoryList();
			NKCScenManager.GetScenManager().GetMyUserData().m_PrivatePvpHistory.Add(res.privatePvpHistories);
			NKCScenManager.GetScenManager().GetMyUserData().m_RankOpen = res.rankPvpOpen;
			NKCScenManager.GetScenManager().GetMyUserData().m_LeagueOpen = res.leaguePvpOpen;
			NKCScenManager.GetScenManager().GetMyUserData().m_LastPlayInfo = res.lastPlayInfo;
			NKCScenManager.GetScenManager().GetMyUserData().m_unitTacticReturnCount = res.unitTacticReturnCount;
			NKCScenManager.GetScenManager().GetMyUserData().UpdateConsumerPackageData(res.consumerPackages);
			NKCScenManager.GetScenManager().GetMyUserData().m_NpcData = res.npcPvpData;
			NKCScenManager.GetScenManager().GetMyUserData().m_enableAccountLink = res.enableAccountLink;
			Log.Debug(string.Format("[SteamLink] JoinLobbyAck - enableAccountLink[{0}]", res.enableAccountLink), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCPacketHandlersLobby.cs", 173);
			NKCScenManager.CurrentUserData().m_ShopData.SetTotalPayment(res.totalPaidAmount);
			NKCScenManager.CurrentUserData().m_ShopData.SetChainTabResetData(res.shopChainTabNestResetList);
			NKCScenManager.CurrentUserData().SetReturningUserStates(res.ReturningUserStates);
			NKCScenManager.CurrentUserData().SetShipCandidateData(res.shipSlotCandidate);
			NKCScenManager.CurrentUserData().OfficeData.SetData(res.officeState);
			NKCScenManager.CurrentUserData().kakaoMissionData = res.kakaoMissionData;
			NKCScenManager.CurrentUserData().TrimData.SetTrimIntervalData(res.trimIntervalData);
			NKCScenManager.CurrentUserData().TrimData.SetTrimClearList(res.trimClearList);
			NKCUIManager.NKCUIUpsideMenu.UpdateTimeContents();
			NKCScenManager.GetScenManager().GetNKCContractDataMgr().SetContractState(res.contractState);
			NKCScenManager.GetScenManager().GetNKCContractDataMgr().SetContractBonusState(res.contractBonusState);
			NKCScenManager.GetScenManager().GetNKCContractDataMgr().SetSelectableContractState(res.selectableContractState);
			NKCScenManager.CurrentUserData().SetMyUserProfileInfo(res.userProfileData);
			NKMEventManager.SetEventInfo(res.eventInfo);
			NKCScenManager.CurrentUserData().SetStagePlayData(res.stagePlayDataList);
			if (res.marketReviewCompletion)
			{
				NKCPublisherModule.Marketing.SetMarketReviewCompleted();
			}
			NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr().SetDailyRewardReceived(res.fierceDailyRewardReceived);
			NKCPublisherModule.InAppPurchase.RequestBillingProductList(null);
			NKCBanManager.UpdatePVPBanData(res.pvpBanResult);
			NKCPVPManager.Init(NKCSynchronizedTime.GetServerUTCTime(0.0));
			NKCContentManager.AddUnlockableContents();
			NKCContentManager.AddUnlockableCounterCase();
			NKCScenManager.CurrentUserData().SetEquipTuningData(res.equipTuningCandidate);
			NKCGuildManager.SetMyData(res.privateGuildData);
			NKCGuildCoopManager.SetMyData(res.guildDungeonRewardInfo);
			NKCChatManager.SetMuteEndDate(res.blockMuteEndDate);
			NKMEpisodeMgr.SetUnlockedStage(res.unlockedStageIds);
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData != null)
			{
				gameOptionData.LoadAccountLocal(res.userData.m_UserOption);
			}
			NKMMissionManager.SetDefaultTrackingMissionToGrowthMission();
			NKCScenManager.GetScenManager().Get_SCEN_LOGIN().OnLoginSuccess(res);
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_FRIENDLY_MODE) && NKCPrivatePVPMgr.IsInProgress() && res.gameData == null && res.pvpGameLobby == null)
			{
				NKCPrivatePVPMgr.CancelAllProcess();
			}
			NKCPhaseManager.SetPhaseModeState(res.phaseModeState);
			NKCPhaseManager.SetPhaseClearDataList(res.phaseClearDataList);
			NKCTrimManager.SetTrimModeState(res.trimModeState);
			NKCKillCountManager.SetServerKillCountData(res.serverKillCountDataList);
			NKCKillCountManager.SetKillCountData(res.killCountDataList);
			NKCUnitMissionManager.SetCompletedUnitMissionData(res.completedUnitMissions);
			NKCUnitMissionManager.SetRewardUnitMissionData(res.rewardEnableUnitMissions);
			NKCBanManager.UpdatePVPCastingVoteData(res.pvpCastingVoteData);
			NKCScenManager.CurrentUserData().EventCollectionInfo = res.eventCollectionInfo;
			if (!NKCPublisherModule.Auth.OnLoginSuccessToCS())
			{
				return;
			}
			if (!NKCPatchUtility.BackgroundPatchEnabled())
			{
				NKCPatchUtility.SaveTutorialClearedStatus();
			}
			if (!NKCTutorialManager.CheckTutoGameCondAtLogin(res.userData) || res.userData.CheckDungeonClear(1007))
			{
				NKCPatchUtility.SaveTutorialClearedStatus();
				if (NKCPatchDownloader.Instance != null && (NKCPatchDownloader.Instance.VersionCheckStatus != NKCPatchDownloader.VersionStatus.UpToDate || NKCPatchDownloader.Instance.ProloguePlay))
				{
					NKCScenManager.GetScenManager().ShowBundleUpdate(false);
					return;
				}
			}
			Debug.Log(string.Format("[NKMPacket_JOIN_LOBBY_ACK] Is gameData null : {0}", res.gameData == null));
			Debug.Log(string.Format("[NKMPacket_JOIN_LOBBY_ACK] Is leaguePvpRoomData null : {0}", res.leaguePvpRoomData == null));
			Debug.Log(string.Format("[NKMPacket_JOIN_LOBBY_ACK] Is pvpGameLobby null : {0}", res.pvpGameLobby == null));
			if (res.gameData == null)
			{
				if (NKCTutorialManager.CheckTutoGameCondAtLogin(res.userData))
				{
					if (!res.userData.CheckDungeonClear(1004))
					{
						NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ(new NKMEventDeckData(), 11211, 0, 1004, 0, false, 1, 0);
						return;
					}
					if (!res.userData.CheckDungeonClear(1005))
					{
						NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ(new NKMEventDeckData(), 11212, 0, 1005, 0, false, 1, 0);
						return;
					}
					if (!res.userData.CheckDungeonClear(1006))
					{
						NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ(new NKMEventDeckData(), 11213, 0, 1006, 0, false, 1, 0);
						return;
					}
					if (!res.userData.CheckDungeonClear(1007))
					{
						NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ(new NKMEventDeckData(), 11214, 0, 1007, 0, false, 1, 0);
						return;
					}
				}
				if (NKCPhaseManager.PlayNextPhase())
				{
					return;
				}
				if (NKCTrimManager.ProcessTrim())
				{
					return;
				}
				NKM_SCEN_ID nowScenID = NKCScenManager.GetScenManager().GetNowScenID();
				switch (nowScenID)
				{
				case NKM_SCEN_ID.NSI_LOGIN:
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
					return;
				case NKM_SCEN_ID.NSI_HOME:
				case NKM_SCEN_ID.NSI_TEAM:
					break;
				case NKM_SCEN_ID.NSI_GAME:
					if (NKCScenManager.GetScenManager().GetGameClient() != null && NKCScenManager.GetScenManager().GetGameClient().GetGameRuntimeData() != null && (NKCScenManager.GetScenManager().GetGameClient().GetGameRuntimeData().m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_FINISH || NKCScenManager.GetScenManager().GetGameClient().GetGameRuntimeData().m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_END))
					{
						if (NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_BattleResultData() != null)
						{
							NKCScenManager.GetScenManager().Get_SCEN_GAME().EndGameWithReservedGameData();
							return;
						}
						if (NKCScenManager.GetScenManager().GetGameClient().GetGameData() != null)
						{
							NKCScenManager.GetScenManager().Get_SCEN_GAME().DoAfterGiveUp();
							return;
						}
						NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
						return;
					}
					else
					{
						if (NKCScenManager.GetScenManager().GetGameClient() != null && NKCScenManager.GetScenManager().GetGameClient().GetGameData() != null && NKCScenManager.GetScenManager().GetGameClient().GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_PRACTICE)
						{
							return;
						}
						if (NKCScenManager.GetScenManager().GetGameClient() != null && NKCScenManager.GetScenManager().GetGameClient().GetGameData() != null)
						{
							NKCScenManager.GetScenManager().Get_SCEN_GAME().DoAfterGiveUp();
							return;
						}
						NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
						return;
					}
					break;
				case NKM_SCEN_ID.NSI_BASE:
					if (NKCUIUnitInfo.IsInstanceOpen)
					{
						NKCUIUnitInfo.Instance.RefreshUIForReconnect();
						return;
					}
					return;
				default:
					switch (nowScenID)
					{
					case NKM_SCEN_ID.NSI_OPERATION:
						NKCScenManager.GetScenManager().Get_SCEN_OPERATION().ProcessRelogin();
						return;
					case NKM_SCEN_ID.NSI_EPISODE:
					case NKM_SCEN_ID.NSI_DUNGEON_ATK_READY:
					case NKM_SCEN_ID.NSI_UNIT_LIST:
					case NKM_SCEN_ID.NSI_SHOP:
					case NKM_SCEN_ID.NSI_FRIEND:
						break;
					case NKM_SCEN_ID.NSI_COLLECTION:
					case NKM_SCEN_ID.NSI_CUTSCENE_DUNGEON:
					case NKM_SCEN_ID.NSI_GAME_RESULT:
						return;
					case NKM_SCEN_ID.NSI_WARFARE_GAME:
						NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().ProcessReLogin();
						return;
					case NKM_SCEN_ID.NSI_WORLDMAP:
						NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().ProcessReLogin();
						return;
					default:
						if (nowScenID == NKM_SCEN_ID.NSI_GAUNTLET_MATCH)
						{
							NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_MATCH().ProcessReLogin();
							return;
						}
						break;
					}
					break;
				}
				NKCScenManager.GetScenManager().ScenChangeFade(NKCScenManager.GetScenManager().GetNowScenID(), true);
				return;
			}
			else
			{
				if (res.leaguePvpRoomData != null)
				{
					NKCLeaguePVPMgr.ProcessReLogin(res.gameData, res.leaguePvpRoomData);
					return;
				}
				if (res.pvpGameLobby != null)
				{
					if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
					{
						NKCPrivatePVPRoomMgr.ProcessReLogin(res.gameData, res.pvpGameLobby);
						return;
					}
					NKCPrivatePVPMgr.ProcessReLogin(res.gameData, res.pvpGameLobby);
					return;
				}
				else
				{
					Debug.Log(string.Format("[NKMPacket_JOIN_LOBBY_ACK] {0} ProcessReLogin", NKCScenManager.GetScenManager().GetNowScenID()));
					NKM_SCEN_ID nowScenID = NKCScenManager.GetScenManager().GetNowScenID();
					if (nowScenID != NKM_SCEN_ID.NSI_CUTSCENE_DUNGEON)
					{
						if (nowScenID == NKM_SCEN_ID.NSI_GAUNTLET_MATCH)
						{
							NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_MATCH().ProcessReLogin(res.gameData);
							return;
						}
						NKCPacketHandlersLobby.NormalProcessReLoginWhenGameExist(res.gameData);
					}
				}
			}
		}

		// Token: 0x06005815 RID: 22549 RVA: 0x001A6F38 File Offset: 0x001A5138
		public static void NormalProcessReLoginWhenGameExist(NKMGameData _NKMGameData)
		{
			if (_NKMGameData == null)
			{
				return;
			}
			NKCScenManager.GetScenManager().GetGameClient().SetGameDataDummy(_NKMGameData, true);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAME, true);
		}

		// Token: 0x06005816 RID: 22550 RVA: 0x001A6F5C File Offset: 0x001A515C
		public static void OnRecv(NKMPacket_GAME_LAW_SHUTDOWN_NOT cNKMPacket_GAME_LAW_SHUTDOWN_NOT)
		{
			if (cNKMPacket_GAME_LAW_SHUTDOWN_NOT.remainSpan <= TimeSpan.Zero)
			{
				NKCScenManager.GetScenManager().GetConnectLogin().SetEnable(false);
				NKCScenManager.GetScenManager().GetConnectGame().SetEnable(false);
				NKCScenManager.GetScenManager().GetConnectLogin().ResetConnection();
				NKCScenManager.GetScenManager().GetConnectGame().ResetConnection();
				NKCScenManager.GetScenManager().Get_SCEN_LOGIN().SetShutdownPopup();
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_LOGIN, true);
				return;
			}
			NKCUIPopupMessageServer.Instance.Open(NKCUIPopupMessageServer.eMessageStyle.Slide, NKCUtilString.GET_STRING_SHUTDOWN_ALARM, 1);
		}

		// Token: 0x06005817 RID: 22551 RVA: 0x001A6FE6 File Offset: 0x001A51E6
		public static void OnRecv(NKMPacket_DUPLICATED_CONNECTED_NOT cNKMPacket_DUPLICATED_CONNECTED_NOT)
		{
			NKCScenManager.GetScenManager().GetConnectLogin().ResetConnection();
			NKCScenManager.GetScenManager().GetConnectGame().ResetConnection();
			NKCScenManager.GetScenManager().Get_SCEN_LOGIN().SetDuplicateConnectPopup();
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_LOGIN, true);
		}

		// Token: 0x06005818 RID: 22552 RVA: 0x001A7024 File Offset: 0x001A5224
		public static void OnRecv(NKMPacket_WEEKLY_REFRESH_NOT cNKMPacket_WEEKLY_REFRESH_NOT)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				nkmuserData.m_InventoryData.UpdateItemInfo(cNKMPacket_WEEKLY_REFRESH_NOT.refreshItemDataList);
			}
		}

		// Token: 0x06005819 RID: 22553 RVA: 0x001A704C File Offset: 0x001A524C
		public static void OnRecv(NKMPacket_CHANGE_NICKNAME_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.m_UserNickName = sPacket.nickname;
			myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItemData);
			NKCPopupNickname.CheckInstanceAndClose();
			if (NKCUIUserInfo.IsInstanceOpen)
			{
				NKCUIUserInfo.Instance.RefreshNickname();
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_HOME)
			{
				NKCScenManager.GetScenManager().Get_SCEN_HOME().RefreshNickname();
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_FRIEND)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().RefreshNickname();
			}
			NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.PlayerNameChanged, 0, null);
		}

		// Token: 0x0600581A RID: 22554 RVA: 0x001A70F4 File Offset: 0x001A52F4
		public static void OnRecv(NKMPacket_DECK_UNLOCK_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.m_ArmyData.UnlockDeck(sPacket.deckType, (int)sPacket.unlockedDeckSize);
			myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItemData);
			if (NKCUIDeckViewer.IsInstanceOpen)
			{
				NKCUIDeckViewer.Instance.OnRecv(sPacket);
			}
		}

		// Token: 0x0600581B RID: 22555 RVA: 0x001A715C File Offset: 0x001A535C
		public static void OnRecv(NKMPacket_DECK_UNIT_SET_ACK cPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			if (cPacket.oldDeckIndex.m_eDeckType != NKM_DECK_TYPE.NDT_NONE)
			{
				armyData.SetDeckUnitByIndex(cPacket.oldDeckIndex, (byte)cPacket.oldSlotIndex, 0L);
				armyData.SetDeckLeader(cPacket.oldDeckIndex, cPacket.oldLeaderSlotIndex);
			}
			long num = 0L;
			NKMUnitData deckUnitByIndex = armyData.GetDeckUnitByIndex(cPacket.deckIndex, (int)cPacket.slotIndex);
			if (deckUnitByIndex != null)
			{
				num = deckUnitByIndex.m_UnitUID;
			}
			armyData.SetDeckUnitByIndex(cPacket.deckIndex, cPacket.slotIndex, cPacket.slotUnitUID);
			NKMDeckData deckData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetDeckData(cPacket.deckIndex);
			if (deckData != null)
			{
				deckData.m_LeaderIndex = cPacket.leaderSlotIndex;
			}
			if (NKCUIDeckViewer.IsInstanceOpen)
			{
				NKCUIDeckViewer.Instance.OnRecv(cPacket, deckUnitByIndex == null);
			}
			if (NKCUIUnitSelectList.IsInstanceOpen)
			{
				if (num != 0L)
				{
					NKCUIUnitSelectList.Instance.ChangeUnitDeckIndex(num, new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE));
				}
				NKCUIUnitSelectList.Instance.ChangeUnitDeckIndex(cPacket.slotUnitUID, cPacket.deckIndex);
			}
		}

		// Token: 0x0600581C RID: 22556 RVA: 0x001A726C File Offset: 0x001A546C
		public static void OnRecv(NKMPacket_DECK_SHIP_SET_ACK cPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			NKMDeckData deckData = armyData.GetDeckData(cPacket.deckIndex);
			long num = 0L;
			NKMUnitData deckShip = armyData.GetDeckShip(cPacket.deckIndex);
			if (deckShip != null)
			{
				num = deckShip.m_UnitUID;
			}
			if (cPacket.oldDeckIndex.m_eDeckType != NKM_DECK_TYPE.NDT_NONE)
			{
				NKMDeckData deckData2 = armyData.GetDeckData(cPacket.oldDeckIndex);
				if (deckData2 != null)
				{
					deckData2.m_ShipUID = 0L;
				}
			}
			if (deckData != null)
			{
				deckData.m_ShipUID = cPacket.shipUID;
			}
			if (NKCUIDeckViewer.IsInstanceOpen)
			{
				NKCUIDeckViewer.Instance.OnRecv(cPacket);
			}
			if (NKCUIShipInfo.IsInstanceOpen)
			{
				NKCUIShipInfo.Instance.OnRecv(cPacket);
			}
			if (NKCUIUnitSelectList.IsInstanceOpen)
			{
				if (num != 0L)
				{
					NKCUIUnitSelectList.Instance.ChangeUnitDeckIndex(num, new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE));
				}
				NKCUIUnitSelectList.Instance.ChangeUnitDeckIndex(cPacket.shipUID, cPacket.deckIndex);
			}
		}

		// Token: 0x0600581D RID: 22557 RVA: 0x001A7354 File Offset: 0x001A5554
		public static void OnRecv(NKMPacket_DECK_UNIT_SWAP_ACK cPacket_DECK_UNIT_SWAP_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cPacket_DECK_UNIT_SWAP_ACK.errorCode, true, null, -2147483648))
			{
				if (NKCUIDeckViewer.IsInstanceOpen)
				{
					NKCUIDeckViewer.Instance.SelectCurrentDeck();
				}
				return;
			}
			NKMDeckData deckData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetDeckData(cPacket_DECK_UNIT_SWAP_ACK.deckIndex);
			if (deckData != null)
			{
				if (cPacket_DECK_UNIT_SWAP_ACK.leaderSlotIndex != -1)
				{
					deckData.m_LeaderIndex = cPacket_DECK_UNIT_SWAP_ACK.leaderSlotIndex;
				}
				deckData.m_listDeckUnitUID[(int)cPacket_DECK_UNIT_SWAP_ACK.slotIndexFrom] = cPacket_DECK_UNIT_SWAP_ACK.slotUnitUIDFrom;
				deckData.m_listDeckUnitUID[(int)cPacket_DECK_UNIT_SWAP_ACK.slotIndexTo] = cPacket_DECK_UNIT_SWAP_ACK.slotUnitUIDTo;
			}
			if (NKCUIDeckViewer.IsInstanceOpen)
			{
				NKCUIDeckViewer.Instance.OnRecv(cPacket_DECK_UNIT_SWAP_ACK);
			}
		}

		// Token: 0x0600581E RID: 22558 RVA: 0x001A73FC File Offset: 0x001A55FC
		public static void OnRecv(NKMPacket_DECK_UNIT_SET_LEADER_ACK cPacket_DECK_UNIT_SET_LEADER_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cPacket_DECK_UNIT_SET_LEADER_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMDeckData deckData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetDeckData(cPacket_DECK_UNIT_SET_LEADER_ACK.deckIndex);
			if (deckData != null)
			{
				deckData.m_LeaderIndex = cPacket_DECK_UNIT_SET_LEADER_ACK.leaderSlotIndex;
			}
			if (NKCUIDeckViewer.IsInstanceOpen)
			{
				NKCUIDeckViewer.Instance.OnRecv(cPacket_DECK_UNIT_SET_LEADER_ACK);
			}
		}

		// Token: 0x0600581F RID: 22559 RVA: 0x001A745C File Offset: 0x001A565C
		public static void OnRecv(NKMPacket_DECK_UNIT_AUTO_SET_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMDeckData nkmdeckData = NKCScenManager.CurrentUserData().m_ArmyData.GetDeckData(sPacket.deckIndex);
			if (nkmdeckData == null)
			{
				nkmdeckData = new NKMDeckData();
			}
			HashSet<int> hashSet = new HashSet<int>();
			for (int i = 0; i < nkmdeckData.m_listDeckUnitUID.Count; i++)
			{
				if (i < sPacket.deckData.m_listDeckUnitUID.Count && sPacket.deckData.m_listDeckUnitUID[i] != nkmdeckData.m_listDeckUnitUID[i])
				{
					hashSet.Add(i);
				}
			}
			nkmdeckData.DeepCopyFrom(sPacket.deckData);
			if (NKCUIDeckViewer.IsInstanceOpen)
			{
				NKCUIDeckViewer.Instance.OnRecv(sPacket, hashSet);
			}
		}

		// Token: 0x06005820 RID: 22560 RVA: 0x001A7514 File Offset: 0x001A5714
		public static void OnRecv(NKMPacket_DECK_NAME_UPDATE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				if (NKCUIDeckViewer.IsInstanceOpen)
				{
					NKCUIDeckViewer.Instance.ResetDeckName();
				}
				return;
			}
			NKMDeckData deckData = NKCScenManager.CurrentUserData().m_ArmyData.GetDeckData(sPacket.deckIndex);
			if (deckData != null)
			{
				deckData.m_DeckName = sPacket.name;
			}
			if (NKCUIDeckViewer.IsInstanceOpen)
			{
				NKCUIDeckViewer.Instance.UpdateDeckName(sPacket.deckIndex, sPacket.name);
			}
		}

		// Token: 0x06005821 RID: 22561 RVA: 0x001A758C File Offset: 0x001A578C
		public static void OnRecv(NKMPacket_PVP_GAME_MATCH_ACK cNKMPacket_PVP_GAME_MATCH_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_PVP_GAME_MATCH_ACK.errorCode, true, null, -2147483648))
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_MATCH_READY, true);
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_HOME || NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME)
			{
				NKMPopUpBox.OpenWaitBox(0f, "");
			}
		}

		// Token: 0x06005822 RID: 22562 RVA: 0x001A75E4 File Offset: 0x001A57E4
		public static void OnRecv(NKMPacket_PVP_GAME_MATCH_CANCEL_ACK cNKMPacket_PVP_GAME_MATCH_CANCEL_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_PVP_GAME_MATCH_CANCEL_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_MATCH)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_MATCH().NKCUIGuantletMatch.OnRecv(cNKMPacket_PVP_GAME_MATCH_CANCEL_ACK);
			}
		}

		// Token: 0x06005823 RID: 22563 RVA: 0x001A7620 File Offset: 0x001A5820
		public static void OnRecv(NKMPacket_PVP_GAME_MATCH_COMPLETE_NOT cNKMPacket_PVP_GAME_MATCH_COMPLETE_NOT)
		{
			NKCSoundManager.SetMute(NKCScenManager.GetScenManager().GetGameOptionData().SoundMute, true);
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
			{
				NKCPrivatePVPRoomMgr.OnRecv(cNKMPacket_PVP_GAME_MATCH_COMPLETE_NOT);
			}
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_FRIENDLY_MODE))
			{
				NKCPrivatePVPMgr.OnRecv(cNKMPacket_PVP_GAME_MATCH_COMPLETE_NOT);
			}
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_LEAGUE_MODE))
			{
				NKCLeaguePVPMgr.OnRecv(cNKMPacket_PVP_GAME_MATCH_COMPLETE_NOT);
			}
			NKCScenManager.GetScenManager().GetGameClient().SetGameDataDummy(cNKMPacket_PVP_GAME_MATCH_COMPLETE_NOT.gameData, false);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME)
			{
				NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_PVP_GAME_MATCH_COMPLETE_NOT);
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_MATCH)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_MATCH().OnRecv(cNKMPacket_PVP_GAME_MATCH_COMPLETE_NOT);
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAUNTLET_ASYNC_READY)
			{
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_MATCH_READY)
				{
					NKCScenManager.GetScenManager().Get_SCEN_GAME().ReserveGameEndData(null);
					if (cNKMPacket_PVP_GAME_MATCH_COMPLETE_NOT.gameData == null)
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_ERROR_SERVER_GAME_DATA, delegate()
						{
							NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
						}, "");
						return;
					}
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAME, true);
					return;
				}
				else
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAME, true);
				}
			}
		}

		// Token: 0x06005824 RID: 22564 RVA: 0x001A7742 File Offset: 0x001A5942
		public static void OnRecv(NKMPacket_PVP_GAME_MATCH_FAIL_NOT cNKMPacket_PVP_GAME_MATCH_FAIL_NOT)
		{
			NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_GAUNTLET_MATCHING_FAIL_ALARM, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetReservedLobbyTab(NKC_GAUNTLET_LOBBY_TAB.NGLT_RANK);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LOBBY, true);
		}

		// Token: 0x06005825 RID: 22565 RVA: 0x001A7774 File Offset: 0x001A5974
		public static void OnRecv(NKMPacket_CONTRACT_ACK sNKMPacket_CONTRACT_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sNKMPacket_CONTRACT_ACK.errorCode, true, null, -2147483648))
			{
				if ((sNKMPacket_CONTRACT_ACK.errorCode == NKM_ERROR_CODE.NEC_FAIL_CANNOT_USE_TICKET_WHEN_FREE_CHANCE_REMAINED || sNKMPacket_CONTRACT_ACK.errorCode == NKM_ERROR_CODE.NEC_FAIL_CANNOT_USE_MONEY_WHEN_FREE_CHANCE_REMAINED || sNKMPacket_CONTRACT_ACK.errorCode == NKM_ERROR_CODE.NEC_FAIL_CANNOT_USE_MONEY_WHEN_TICKET_REMAINED) && NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_CONTRACT)
				{
					NKCScenManager.GetScenManager().GET_SCEN_CONTRACT().OnUIForceRefresh(false);
				}
				return;
			}
			NKMInventoryData inventoryData = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData;
			if (inventoryData != null)
			{
				inventoryData.UpdateItemInfo(sNKMPacket_CONTRACT_ACK.costItems);
				if (sNKMPacket_CONTRACT_ACK.rewardData != null && sNKMPacket_CONTRACT_ACK.rewardData.MiscItemDataList != null)
				{
					inventoryData.AddItemMisc(sNKMPacket_CONTRACT_ACK.rewardData.MiscItemDataList);
				}
			}
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			for (int i = 0; i < sNKMPacket_CONTRACT_ACK.units.Count; i++)
			{
				NKMUnitData nkmunitData = sNKMPacket_CONTRACT_ACK.units[i];
				if (armyData.IsFirstGetUnit(nkmunitData.m_UnitID))
				{
					NKCUIGameResultGetUnit.AddFirstGetUnit(nkmunitData.m_UnitID);
				}
				armyData.AddNewUnit(nkmunitData);
			}
			foreach (NKMOperator nkmoperator in sNKMPacket_CONTRACT_ACK.operators)
			{
				if (armyData.IsFirstGetUnit(nkmoperator.id))
				{
					NKCUIGameResultGetUnit.AddFirstGetUnit(nkmoperator.id);
				}
				armyData.AddNewOperator(nkmoperator);
			}
			NKCContractDataMgr nkccontractDataMgr = NKCScenManager.GetScenManager().GetNKCContractDataMgr();
			if (nkccontractDataMgr != null)
			{
				nkccontractDataMgr.UpdateContractBonusState(sNKMPacket_CONTRACT_ACK.contractBonusState);
				nkccontractDataMgr.UpdateContractState(sNKMPacket_CONTRACT_ACK.contractState);
			}
			NKCScenManager.GetScenManager().GET_SCEN_CONTRACT().OnRecv(sNKMPacket_CONTRACT_ACK);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_HOME)
			{
				foreach (NKCUIModuleHome nkcuimoduleHome in NKCUIManager.GetOpenedUIsByType<NKCUIModuleHome>())
				{
					if (nkcuimoduleHome.IsOpen)
					{
						nkcuimoduleHome.UpdateUI();
					}
				}
			}
		}

		// Token: 0x06005826 RID: 22566 RVA: 0x001A796C File Offset: 0x001A5B6C
		public static void OnRecv(NKMPacket_SELECTABLE_CONTRACT_CHANGE_POOL_ACK sNKMPacket_SELECTABLE_CONTRACT_CHANGE_POOL_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sNKMPacket_SELECTABLE_CONTRACT_CHANGE_POOL_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.GetScenManager().GetNKCContractDataMgr().SetSelectableContractState(sNKMPacket_SELECTABLE_CONTRACT_CHANGE_POOL_ACK.selectableContractState);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_CONTRACT)
			{
				NKCScenManager.GetScenManager().GET_SCEN_CONTRACT().OnRecv(sNKMPacket_SELECTABLE_CONTRACT_CHANGE_POOL_ACK);
			}
		}

		// Token: 0x06005827 RID: 22567 RVA: 0x001A79C0 File Offset: 0x001A5BC0
		public static void OnRecv(NKMPacket_SELECTABLE_CONTRACT_CONFIRM_ACK sNKMPacket_SELECTABLE_CONTRACT_CONFIRM_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sNKMPacket_SELECTABLE_CONTRACT_CONFIRM_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.UpdateItemInfo(sNKMPacket_SELECTABLE_CONTRACT_CONFIRM_ACK.costItems);
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			List<NKMUnitData> units = sNKMPacket_SELECTABLE_CONTRACT_CONFIRM_ACK.units;
			for (int i = 0; i < units.Count; i++)
			{
				if (armyData.IsFirstGetUnit(units[i].m_UnitID))
				{
					NKCUIGameResultGetUnit.AddFirstGetUnit(units[i].m_UnitID);
				}
				armyData.AddNewUnit(units[i]);
			}
			NKCScenManager.GetScenManager().GetNKCContractDataMgr().SetSelectableContractState(sNKMPacket_SELECTABLE_CONTRACT_CONFIRM_ACK.selectableContractState);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_CONTRACT)
			{
				NKCScenManager.GetScenManager().GET_SCEN_CONTRACT().OnRecv(sNKMPacket_SELECTABLE_CONTRACT_CONFIRM_ACK);
			}
		}

		// Token: 0x06005828 RID: 22568 RVA: 0x001A7A88 File Offset: 0x001A5C88
		public static void OnRecv(NKMPacket_CONTRACT_STATE_LIST_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (sPacket.contractState != null)
			{
				NKCScenManager.GetScenManager().GetNKCContractDataMgr().SetContractState(sPacket.contractState);
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_CONTRACT)
			{
				NKCScenManager.GetScenManager().GET_SCEN_CONTRACT().OnUIForceRefresh(false);
			}
		}

		// Token: 0x06005829 RID: 22569 RVA: 0x001A7AE4 File Offset: 0x001A5CE4
		public static void OnRecv(NKMPacket_INSTANT_CONTRACT_LIST_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.GetScenManager().GetNKCContractDataMgr().UpdateInstantContract(sPacket.InstantContract);
			if (sPacket.InstantContract != null && sPacket.InstantContract.Count > 0 && NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_CONTRACT)
			{
				NKCScenManager.GetScenManager().GET_SCEN_CONTRACT().OnUIForceRefresh(true);
			}
		}

		// Token: 0x0600582A RID: 22570 RVA: 0x001A7B4E File Offset: 0x001A5D4E
		public static void OnRecv(NKMPacket_BACKGROUND_CHANGE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.GetScenManager().GetMyUserData().backGroundInfo = sPacket.backgroundInfo;
			NKCUIChangeLobby.CheckInstanceAndClose();
			NKCUIUserInfo.CheckInstanceAndClose();
		}

		// Token: 0x0600582B RID: 22571 RVA: 0x001A7B84 File Offset: 0x001A5D84
		public static void OnRecv(NKMPacket_SERVER_TIME_ACK sPacket)
		{
			NKCSynchronizedTime.OnRecv(sPacket);
			NKCPacketObjectPool.CloseObject(sPacket);
		}

		// Token: 0x0600582C RID: 22572 RVA: 0x001A7B94 File Offset: 0x001A5D94
		public static void OnRecv(NKMPacket_ENHANCE_UNIT_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMArmyData armyData = myUserData.m_ArmyData;
			for (int i = 0; i < sPacket.consumeUnitUIDList.Count; i++)
			{
				armyData.RemoveUnit(sPacket.consumeUnitUIDList[i]);
			}
			myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItemData);
			NKMUnitData unitFromUID = armyData.GetUnitFromUID(sPacket.unitUID);
			if (unitFromUID != null)
			{
				for (int j = 0; j < sPacket.statExpList.Count; j++)
				{
					unitFromUID.m_listStatEXP[j] = sPacket.statExpList[j];
				}
			}
			armyData.UpdateUnitData(unitFromUID);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_BASE)
			{
				NKCScenManager.GetScenManager().Get_SCEN_BASE().OnRecv(sPacket);
			}
		}

		// Token: 0x0600582D RID: 22573 RVA: 0x001A7C6C File Offset: 0x001A5E6C
		public static void OnRecv(NKMPacket_LOCK_UNIT_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			NKMUnitData unitOrShipFromUID = armyData.GetUnitOrShipFromUID(sPacket.unitUID);
			if (unitOrShipFromUID != null)
			{
				unitOrShipFromUID.m_bLock = sPacket.isLock;
			}
			armyData.UpdateData(unitOrShipFromUID);
		}

		// Token: 0x0600582E RID: 22574 RVA: 0x001A7CC0 File Offset: 0x001A5EC0
		public static void OnRecv(NKMPacket_FAVORITE_UNIT_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			NKMUnitData unitOrShipFromUID = armyData.GetUnitOrShipFromUID(sPacket.unitUid);
			if (unitOrShipFromUID != null)
			{
				unitOrShipFromUID.isFavorite = sPacket.isFavorite;
			}
			armyData.UpdateData(unitOrShipFromUID);
		}

		// Token: 0x0600582F RID: 22575 RVA: 0x001A7D14 File Offset: 0x001A5F14
		public static void OnRecv(NKMPacket_INVENTORY_EXPAND_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMInventoryManager.UpdateInventoryCount(sPacket.inventoryExpandType, sPacket.expandedCount, myUserData);
			myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItemDataList);
			if (NKCUIUnitSelectList.IsInstanceOpen)
			{
				NKCUIUnitSelectList.Instance.UpdateUnitCount();
				NKCUIUnitSelectList.Instance.OnExpandInventory();
			}
			if (NKCUIInventory.IsInstanceOpen)
			{
				NKCUIInventory.Instance.SetCurrentEquipCountUI();
				NKCUIInventory.Instance.OnInventoryAdd();
			}
			if (NKCUIForge.IsInstanceOpen && NKCUIForge.Instance.IsInventoryInstanceOpen())
			{
				NKCUIForge.Instance.Inventory.SetCurrentEquipCountUI();
				NKCUIForge.Instance.Inventory.OnInventoryAdd();
			}
			if (NKCUIDeckViewer.IsInstanceOpen)
			{
				NKCUIDeckViewer.Instance.UpdateUnitCount();
			}
		}

		// Token: 0x06005830 RID: 22576 RVA: 0x001A7DDC File Offset: 0x001A5FDC
		public static void OnRecv(NKMPacket_REMOVE_UNIT_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMArmyData armyData = myUserData.m_ArmyData;
			myUserData.m_InventoryData.AddItemMisc(sPacket.rewardItemDataList);
			armyData.RemoveUnitList(sPacket.removeUnitUIDList);
			armyData.AddUnitDeleteRewardList(sPacket.rewardItemDataList);
			if (!armyData.IsEmptyUnitDeleteList)
			{
				NKCPacketSender.Send_NKMPacket_REMOVE_UNIT_REQ();
				return;
			}
			if (NKCUIUnitSelectList.IsInstanceOpen)
			{
				NKCUIUnitSelectList.Instance.CloseRemoveMode();
				NKCUIUnitSelectList.Instance.ClearMultipleSelect();
				NKCUINPCMachineGap.PlayVoice(NPC_TYPE.MACHINE_GAP, NPC_ACTION_TYPE.DISMISSAL_RESULT, true);
			}
			if (armyData.GetUnitDeleteReward().Count > 0)
			{
				NKCUIResult.Instance.OpenItemGain(armyData.GetUnitDeleteReward(), NKCUtilString.GET_STRING_ITEM_GAIN, NKCUtilString.GET_STRING_REMOVE_UNIT, null);
			}
		}

		// Token: 0x06005831 RID: 22577 RVA: 0x001A7E94 File Offset: 0x001A6094
		public static void OnRecv(NKMPacket_LIMIT_BREAK_UNIT_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMArmyData armyData = myUserData.m_ArmyData;
			myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItemDataList);
			armyData.UpdateUnitData(sPacket.unitData);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_BASE)
			{
				NKCScenManager.GetScenManager().Get_SCEN_BASE().OnRecv(sPacket);
			}
			if (NKCUIUnitInfo.IsInstanceOpen)
			{
				NKCUIUnitInfo.Instance.OnRecv(sPacket);
			}
		}

		// Token: 0x06005832 RID: 22578 RVA: 0x001A7F14 File Offset: 0x001A6114
		public static void OnRecv(NKMPacket_UNIT_SKILL_UPGRADE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMUnitData unitFromUID = myUserData.m_ArmyData.GetUnitFromUID(sPacket.unitUID);
			myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItemDataList);
			int unitSkillIndex = unitFromUID.GetUnitSkillIndex(sPacket.skillID);
			if (unitSkillIndex >= 0)
			{
				unitFromUID.m_aUnitSkillLevel[unitSkillIndex] = sPacket.skillLevel;
				myUserData.m_ArmyData.UpdateUnitData(unitFromUID);
			}
			else
			{
				Debug.LogError(string.Format("Can't Find Skill {0} from Unit {1}(Uid {2})", sPacket.skillID, unitFromUID.m_UnitID, sPacket.unitUID));
			}
			if (NKCUIUnitInfo.IsInstanceOpen)
			{
				NKCUIUnitInfo.Instance.OnRecv(sPacket);
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_BASE)
			{
				NKCScenManager.GetScenManager().Get_SCEN_BASE().OnRecv(sPacket);
			}
		}

		// Token: 0x06005833 RID: 22579 RVA: 0x001A7FF0 File Offset: 0x001A61F0
		public static void OnRecv(NKMPacket_CONTENTS_DAILY_REFRESH_NOT cNKMPacket_CONTENTS_DAILY_REFRESH_NOT)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_CONTENTS_DAILY_REFRESH_NOT.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				myUserData.m_InventoryData.UpdateItemInfo(cNKMPacket_CONTENTS_DAILY_REFRESH_NOT.refreshItemDataList);
				myUserData.m_InventoryData.RefreshDailyContens();
			}
		}

		// Token: 0x06005834 RID: 22580 RVA: 0x001A803C File Offset: 0x001A623C
		public static void OnRecv(NKMPacket_COUNTERCASE_UNLOCK_ACK cNKMPacket_COUNTERCASE_UNLOCK_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_COUNTERCASE_UNLOCK_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				myUserData.m_InventoryData.UpdateItemInfo(cNKMPacket_COUNTERCASE_UNLOCK_ACK.costItemData);
				myUserData.AddCounterCaseData(cNKMPacket_COUNTERCASE_UNLOCK_ACK.dungeonID, true);
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_OPERATION)
			{
				NKCScenManager.GetScenManager().Get_SCEN_OPERATION().OnRecv(cNKMPacket_COUNTERCASE_UNLOCK_ACK);
			}
		}

		// Token: 0x06005835 RID: 22581 RVA: 0x001A80A8 File Offset: 0x001A62A8
		public static void OnRecv(NKMPacket_CUTSCENE_DUNGEON_CLEAR_ACK cNKMPacket_CUTSCENE_DUNGEON_CLEAR_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_CUTSCENE_DUNGEON_CLEAR_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null && cNKMPacket_CUTSCENE_DUNGEON_CLEAR_ACK.dungeonClearData != null && cNKMPacket_CUTSCENE_DUNGEON_CLEAR_ACK.dungeonClearData.dungeonId != 0)
			{
				myUserData.SetDungeonClearData(cNKMPacket_CUTSCENE_DUNGEON_CLEAR_ACK.dungeonClearData);
				myUserData.GetReward(cNKMPacket_CUTSCENE_DUNGEON_CLEAR_ACK.dungeonClearData.rewardData);
				myUserData.UpdateEpisodeCompleteData(cNKMPacket_CUTSCENE_DUNGEON_CLEAR_ACK.episodeCompleteData);
				NKCContentManager.SetUnlockedContent(STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DUNGEON, cNKMPacket_CUTSCENE_DUNGEON_CLEAR_ACK.dungeonClearData.dungeonId);
			}
			NKCScenManager.GetScenManager().Get_SCEN_OPERATION().OnRecv(cNKMPacket_CUTSCENE_DUNGEON_CLEAR_ACK);
		}

		// Token: 0x06005836 RID: 22582 RVA: 0x001A8138 File Offset: 0x001A6338
		public static void OnRecv(NKMPacket_EPISODE_COMPLETE_REWARD_ACK cNKMPacket_EPISODE_COMPLETE_REWARD_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_EPISODE_COMPLETE_REWARD_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				myUserData.UpdateEpisodeCompleteData(cNKMPacket_EPISODE_COMPLETE_REWARD_ACK.episodeCompleteData);
				myUserData.GetReward(cNKMPacket_EPISODE_COMPLETE_REWARD_ACK.rewardData);
			}
			if (myUserData != null)
			{
				NKCUIResult.Instance.OpenComplexResult(myUserData.m_ArmyData, cNKMPacket_EPISODE_COMPLETE_REWARD_ACK.rewardData, null, 0L, null, false, false);
			}
			if (NKCUIOperationNodeViewer.isOpen())
			{
				NKCUIOperationNodeViewer.Instance.Refresh();
			}
		}

		// Token: 0x06005837 RID: 22583 RVA: 0x001A81B0 File Offset: 0x001A63B0
		public static void OnRecv(NKMPacket_EPISODE_COMPLETE_REWARD_ALL_ACK cNKMPacket_EPISODE_COMPLETE_REWARD_ALL_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_EPISODE_COMPLETE_REWARD_ALL_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				foreach (NKMEpisodeCompleteData episodeCompleteData in cNKMPacket_EPISODE_COMPLETE_REWARD_ALL_ACK.episodeCompleteData)
				{
					myUserData.UpdateEpisodeCompleteData(episodeCompleteData);
				}
				myUserData.GetReward(cNKMPacket_EPISODE_COMPLETE_REWARD_ALL_ACK.rewardDate);
			}
			if (myUserData != null)
			{
				NKCUIResult.Instance.OpenComplexResult(myUserData.m_ArmyData, cNKMPacket_EPISODE_COMPLETE_REWARD_ALL_ACK.rewardDate, null, 0L, null, false, false);
			}
			if (NKCUIOperationNodeViewer.isOpen())
			{
				NKCUIOperationNodeViewer.Instance.Refresh();
			}
		}

		// Token: 0x06005838 RID: 22584 RVA: 0x001A8264 File Offset: 0x001A6464
		public static void OnRecv(NKMPacket_REFRESH_COMPANY_BUFF_ACK cNKMPacket_REFRESH_COMPANY_BUFF_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_REFRESH_COMPANY_BUFF_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				myUserData.m_companyBuffDataList = cNKMPacket_REFRESH_COMPANY_BUFF_ACK.companyBuffDataList;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_HOME)
			{
				NKCScenManager.GetScenManager().Get_SCEN_HOME().RefreshBuff();
			}
		}

		// Token: 0x06005839 RID: 22585 RVA: 0x001A82BC File Offset: 0x001A64BC
		public static void OnRecv(NKMPacket_SHOP_SUBSCRIPTION_NOT sPacket)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			bool flag;
			if (nkmuserData == null)
			{
				flag = (null != null);
			}
			else
			{
				NKMShopData shopData = nkmuserData.m_ShopData;
				flag = (((shopData != null) ? shopData.subscriptions : null) != null);
			}
			if (!flag)
			{
				Debug.LogWarning("[NKMPacket_SHOP_SUBSCRIPTION_NOT] Invalid Current User ShopData");
				return;
			}
			if (NKCScenManager.CurrentUserData().m_ShopData.subscriptions.ContainsKey(sPacket.productId))
			{
				NKCScenManager.CurrentUserData().m_ShopData.subscriptions[sPacket.productId].lastUpdateDate = sPacket.lastUpdateDate;
			}
		}

		// Token: 0x0600583A RID: 22586 RVA: 0x001A8334 File Offset: 0x001A6534
		public static void OnRecv(NKMPacket_GAME_OPTION_PLAY_CUTSCENE_ACK cNKMPacket_GAME_OPTION_PLAY_CUTSCENE_ACK)
		{
			NKMPopUpBox.CloseWaitBox();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GAME_OPTION_PLAY_CUTSCENE_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.GetScenManager().GetMyUserData().m_UserOption.m_bPlayCutscene = cNKMPacket_GAME_OPTION_PLAY_CUTSCENE_ACK.isPlayCutscene;
			if (NKCPopupDungeonInfo.IsInstanceOpen)
			{
				NKCPopupDungeonInfo.Instance.OnRecv(cNKMPacket_GAME_OPTION_PLAY_CUTSCENE_ACK);
			}
		}

		// Token: 0x0600583B RID: 22587 RVA: 0x001A8388 File Offset: 0x001A6588
		public static void OnRecv(NKMPacket_PVP_RANK_WEEK_REWARD_ACK cNKMPacket_PVP_RANK_WEEK_REWARD_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_PVP_RANK_WEEK_REWARD_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			if (cNKMPacket_PVP_RANK_WEEK_REWARD_ACK.pvpData != null)
			{
				myUserData.m_PvpData.WeekID = cNKMPacket_PVP_RANK_WEEK_REWARD_ACK.pvpData.WeekID;
			}
			if (cNKMPacket_PVP_RANK_WEEK_REWARD_ACK.rewardData != null)
			{
				myUserData.GetReward(cNKMPacket_PVP_RANK_WEEK_REWARD_ACK.rewardData);
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_LOBBY)
				{
					if (cNKMPacket_PVP_RANK_WEEK_REWARD_ACK.isScoreChanged)
					{
						NKCPopupGauntletOutgameReward.SetNKMPVPData(cNKMPacket_PVP_RANK_WEEK_REWARD_ACK.pvpData);
						myUserData.m_PvpData = cNKMPacket_PVP_RANK_WEEK_REWARD_ACK.pvpData;
					}
					else
					{
						NKCPopupGauntletOutgameReward.SetNKMPVPData(myUserData.m_PvpData);
					}
					NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().OnRecv(cNKMPacket_PVP_RANK_WEEK_REWARD_ACK);
				}
			}
		}

		// Token: 0x0600583C RID: 22588 RVA: 0x001A8438 File Offset: 0x001A6638
		public static void OnRecv(NKMPacket_PVP_RANK_SEASON_REWARD_ACK cNKMPacket_PVP_RANK_SEASON_REWARD_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_PVP_RANK_SEASON_REWARD_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			if (cNKMPacket_PVP_RANK_SEASON_REWARD_ACK.isScoreChanged)
			{
				NKCPopupGauntletNewSeasonAlarm.SetPrevNKMPVPData(cNKMPacket_PVP_RANK_SEASON_REWARD_ACK.reducedPvpData);
				NKCPopupGauntletOutgameReward.SetNKMPVPData(cNKMPacket_PVP_RANK_SEASON_REWARD_ACK.reducedPvpData);
				myUserData.m_PvpData = cNKMPacket_PVP_RANK_SEASON_REWARD_ACK.pvpData;
			}
			else
			{
				NKCPopupGauntletNewSeasonAlarm.SetPrevNKMPVPData(myUserData.m_PvpData);
				NKCPopupGauntletOutgameReward.SetNKMPVPData(myUserData.m_PvpData);
				myUserData.m_PvpData = cNKMPacket_PVP_RANK_SEASON_REWARD_ACK.pvpData;
			}
			if (cNKMPacket_PVP_RANK_SEASON_REWARD_ACK.rewardData != null)
			{
				myUserData.GetReward(cNKMPacket_PVP_RANK_SEASON_REWARD_ACK.rewardData);
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().OnRecv(cNKMPacket_PVP_RANK_SEASON_REWARD_ACK);
			}
		}

		// Token: 0x0600583D RID: 22589 RVA: 0x001A84EC File Offset: 0x001A66EC
		public static void OnRecv(NKMPacket_ASYNC_PVP_RANK_WEEK_REWARD_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			myUserData.m_AsyncData.WeekID = sPacket.weekID;
			if (sPacket.rewardData != null)
			{
				myUserData.GetReward(sPacket.rewardData);
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_LOBBY)
				{
					NKCPopupGauntletOutgameReward.SetNKMPVPData(myUserData.m_AsyncData);
					NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().NKCPopupGauntletOutgameReward.Open(true, sPacket.rewardData, false, false);
				}
			}
		}

		// Token: 0x0600583E RID: 22590 RVA: 0x001A8578 File Offset: 0x001A6778
		public static void OnRecv(NKMPacket_ASYNC_PVP_RANK_SEASON_REWARD_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			NKCPopupGauntletNewSeasonAlarm.SetPrevNKMPVPData(myUserData.m_AsyncData);
			NKCPopupGauntletOutgameReward.SetNKMPVPData(myUserData.m_AsyncData);
			myUserData.m_AsyncData = sPacket.pvpState;
			myUserData.m_NpcData = sPacket.npcPvpData;
			if (sPacket.rewardData != null)
			{
				myUserData.GetReward(sPacket.rewardData);
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().OnRecv(sPacket);
			}
		}

		// Token: 0x0600583F RID: 22591 RVA: 0x001A860C File Offset: 0x001A680C
		public static void OnRecv(NKMPacket_PVP_BAN_LIST_UPDATED_NOT not)
		{
			if (NKCScenManager.CurrentUserData() == null || NKCScenManager.GetScenManager() == null)
			{
				return;
			}
			NKCBanManager.UpdatePVPBanData(not.pvpBanResult);
			NKM_SCEN_ID nowScenID = NKCScenManager.GetScenManager().GetNowScenID();
			bool flag = false;
			if (nowScenID == NKM_SCEN_ID.NSI_GAUNTLET_LOBBY)
			{
				if (NKCPopupGauntletBanList.IsInstanceOpen)
				{
					flag = true;
					NKCPopupGauntletBanList.Instance.OnChangedBanList();
				}
			}
			else if (nowScenID == NKM_SCEN_ID.NSI_GAUNTLET_MATCH_READY && NKCUIDeckViewer.IsInstanceOpen && NKCUIDeckViewer.IsPVPSyncMode(NKCUIDeckViewer.Instance.GetDeckViewerMode()))
			{
				flag = true;
				NKCScenManager.GetScenManager().ScenChangeFade(nowScenID, true);
			}
			if (flag)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString("SI_TOAST_PVP_BANLIST_RENEWED", false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
			}
		}

		// Token: 0x06005840 RID: 22592 RVA: 0x001A86A7 File Offset: 0x001A68A7
		public static void OnRecv(NKMPacket_ASYNC_PVP_TARGET_LIST_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().OnRecv(sPacket);
			}
		}

		// Token: 0x06005841 RID: 22593 RVA: 0x001A86DC File Offset: 0x001A68DC
		public static void OnRecv(NKMPacket_ASYNC_PVP_RANK_LIST_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().OnRecv(sPacket);
			}
		}

		// Token: 0x06005842 RID: 22594 RVA: 0x001A8711 File Offset: 0x001A6911
		public static void OnRecv(NKMPacket_REVENGE_PVP_TARGET_LIST_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().OnRecv(sPacket);
			}
		}

		// Token: 0x06005843 RID: 22595 RVA: 0x001A8746 File Offset: 0x001A6946
		public static void OnRecv(NKMPacket_NPC_PVP_TARGET_LIST_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().OnRecv(sPacket);
			}
		}

		// Token: 0x06005844 RID: 22596 RVA: 0x001A877B File Offset: 0x001A697B
		public static void OnRecv(NKMPacket_ASYNC_PVP_START_GAME_ACK sPacket)
		{
			NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, int.MinValue);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_ASYNC_READY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_ASYNC_READY().OnRecv(sPacket);
				return;
			}
			NKMPopUpBox.CloseWaitBox();
		}

		// Token: 0x06005845 RID: 22597 RVA: 0x001A87B4 File Offset: 0x001A69B4
		public static void OnRecv(NKMPacket_UPDATE_DEFENCE_DECK_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMDeckData deckData = NKCScenManager.CurrentUserData().m_ArmyData.GetDeckData(new NKMDeckIndex(NKM_DECK_TYPE.NDT_PVP_DEFENCE, 0));
			if (deckData != null)
			{
				deckData.DeepCopyFrom(sPacket.deckData);
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().OnRecv(sPacket);
			}
		}

		// Token: 0x06005846 RID: 22598 RVA: 0x001A881A File Offset: 0x001A6A1A
		public static void OnRecv(NKMPacket_PRIVATE_PVP_INVITE_ACK sPacket)
		{
			NKCPrivatePVPMgr.OnRecv(sPacket);
		}

		// Token: 0x06005847 RID: 22599 RVA: 0x001A8822 File Offset: 0x001A6A22
		public static void OnRecv(NKMPacket_PRIVATE_PVP_INVITE_NOT sPacket)
		{
			NKCPrivatePVPMgr.OnRecv(sPacket);
		}

		// Token: 0x06005848 RID: 22600 RVA: 0x001A882C File Offset: 0x001A6A2C
		public static void OnRecv(NKMPacket_PRIVATE_PVP_CANCEL_ACK sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			NKM_ERROR_CODE errorCode = sPacket.errorCode;
			if (errorCode == NKM_ERROR_CODE.NEC_FAIL_PRIVATE_PVP_INVALID_TARGET_USER_UID)
			{
				if (sPacket.targetUserUid == NKCPrivatePVPMgr.GetTargetUserUID())
				{
					NKCPrivatePVPMgr.OnRecv(sPacket);
				}
				return;
			}
			if (errorCode == NKM_ERROR_CODE.NEC_FAIL_PRIVATE_PVP_STATE_NOT_JOINED)
			{
				NKCPrivatePVPMgr.OnRecv(sPacket);
				return;
			}
			if (errorCode == NKM_ERROR_CODE.NEC_FAIL_PRIVATE_PVP_GAME_ALREADY_JOINED)
			{
				return;
			}
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPrivatePVPMgr.OnRecv(sPacket);
		}

		// Token: 0x06005849 RID: 22601 RVA: 0x001A8896 File Offset: 0x001A6A96
		public static void OnRecv(NKMPacket_PRIVATE_PVP_CANCEL_NOT sPacket)
		{
			if (NKCLeaguePVPMgr.OnRecv(sPacket))
			{
				return;
			}
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
			{
				NKCPrivatePVPRoomMgr.OnRecv(sPacket);
				return;
			}
			NKCPrivatePVPMgr.OnRecv(sPacket);
		}

		// Token: 0x0600584A RID: 22602 RVA: 0x001A88B7 File Offset: 0x001A6AB7
		public static void OnRecv(NKMPacket_PRIVATE_PVP_ACCEPT_ACK sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			if (sPacket.errorCode == NKM_ERROR_CODE.NEC_FAIL_PRIVATE_PVP_INVALID_TARGET_USER_UID && NKCPrivatePVPMgr.CancelNotPopupOpened)
			{
				return;
			}
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPrivatePVPMgr.OnRecv(sPacket);
		}

		// Token: 0x0600584B RID: 22603 RVA: 0x001A88EE File Offset: 0x001A6AEE
		public static void OnRecv(NKMPacket_PRIVATE_PVP_ACCEPT_NOT sPacket)
		{
			NKCPrivatePVPMgr.OnRecv(sPacket);
		}

		// Token: 0x0600584C RID: 22604 RVA: 0x001A88F6 File Offset: 0x001A6AF6
		public static void OnRecv(NKMPacket_PRIVATE_PVP_READY_ACK sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
			{
				NKCPrivatePVPRoomMgr.OnRecv(sPacket);
				return;
			}
			NKCPrivatePVPMgr.OnRecv(sPacket);
		}

		// Token: 0x0600584D RID: 22605 RVA: 0x001A8928 File Offset: 0x001A6B28
		public static void OnRecv(NKMPacket_PRIVATE_PVP_LOBBY_STATE_NOT sPacket)
		{
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
			{
				NKCPrivatePVPRoomMgr.OnRecv(sPacket);
				return;
			}
			NKCPrivatePVPMgr.OnRecv(sPacket);
		}

		// Token: 0x0600584E RID: 22606 RVA: 0x001A8940 File Offset: 0x001A6B40
		public static void OnRecv(NKMPacket_PRIVATE_PVP_EXIT_ACK sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCLeaguePVPMgr.OnRecv(sPacket))
			{
				return;
			}
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
			{
				NKCPrivatePVPRoomMgr.OnRecv(sPacket);
				return;
			}
			NKCPrivatePVPMgr.OnRecv(sPacket);
		}

		// Token: 0x0600584F RID: 22607 RVA: 0x001A897B File Offset: 0x001A6B7B
		public static void OnRecv(NKMPacket_UPDATE_PVP_INVITATION_OPTION_ACK sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPrivatePVPMgr.OnRecv(sPacket);
		}

		// Token: 0x06005850 RID: 22608 RVA: 0x001A899D File Offset: 0x001A6B9D
		public static void OnRecv(NKMPacket_PRIVATE_PVP_SYNC_DECK_INDEX_ACK sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPrivatePVPMgr.OnRecv(sPacket);
		}

		// Token: 0x06005851 RID: 22609 RVA: 0x001A89BF File Offset: 0x001A6BBF
		public static void OnRecv(NKMPacket_PRIVATE_PVP_SEARCH_USER_ACK sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
			{
				NKCPrivatePVPRoomMgr.OnRecv(sPacket);
				return;
			}
			NKCPrivatePVPMgr.OnRecv(sPacket);
		}

		// Token: 0x06005852 RID: 22610 RVA: 0x001A89F1 File Offset: 0x001A6BF1
		public static void OnRecv(NKMPacket_PRIVATE_PVP_CREATE_ROOM_ACK sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPrivatePVPRoomMgr.OnRecv(sPacket);
		}

		// Token: 0x06005853 RID: 22611 RVA: 0x001A8A13 File Offset: 0x001A6C13
		public static void OnRecv(NKMPacket_PVP_ROOM_CHANGE_ROLE_ACK sPacket)
		{
			NKCPrivatePVPRoomMgr.OnRecv(sPacket);
		}

		// Token: 0x06005854 RID: 22612 RVA: 0x001A8A1B File Offset: 0x001A6C1B
		public static void OnRecv(NKMPacket_PVP_ROOM_INVITE_ACK sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPrivatePVPRoomMgr.OnRecv(sPacket);
		}

		// Token: 0x06005855 RID: 22613 RVA: 0x001A8A3D File Offset: 0x001A6C3D
		public static void OnRecv(NKMPacket_PVP_ROOM_INVITE_NOT sPacket)
		{
			NKCPrivatePVPRoomMgr.OnRecv(sPacket);
		}

		// Token: 0x06005856 RID: 22614 RVA: 0x001A8A45 File Offset: 0x001A6C45
		public static void OnRecv(NKMPacket_PVP_ROOM_CANCEL_INVITE_ACK sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPrivatePVPRoomMgr.OnRecv(sPacket);
		}

		// Token: 0x06005857 RID: 22615 RVA: 0x001A8A67 File Offset: 0x001A6C67
		public static void OnRecv(NKMPacket_PVP_ROOM_CANCEL_INVITE_NOT sPacket)
		{
			NKCPrivatePVPRoomMgr.OnRecv(sPacket);
		}

		// Token: 0x06005858 RID: 22616 RVA: 0x001A8A6F File Offset: 0x001A6C6F
		public static void OnRecv(NKMPacket_PVP_ROOM_ACCEPT_INVITE_ACK sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPrivatePVPRoomMgr.OnRecv(sPacket);
		}

		// Token: 0x06005859 RID: 22617 RVA: 0x001A8A91 File Offset: 0x001A6C91
		public static void OnRecv(NKMPacket_PVP_ROOM_ACCEPT_INVITE_NOT sPacket)
		{
			NKCPrivatePVPRoomMgr.OnRecv(sPacket);
		}

		// Token: 0x0600585A RID: 22618 RVA: 0x001A8A99 File Offset: 0x001A6C99
		public static void OnRecv(NKMPacket_PVP_ROOM_START_GAME_SETTING_ACK sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPrivatePVPRoomMgr.OnRecv(sPacket);
		}

		// Token: 0x0600585B RID: 22619 RVA: 0x001A8ABC File Offset: 0x001A6CBC
		public static void OnRecv(NKMPacket_LEAGUE_PVP_MATCH_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetReservedLobbyTab(NKC_GAUNTLET_LOBBY_TAB.NGLT_LEAGUE);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LOBBY, true);
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_HOME || NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME)
			{
				NKMPopUpBox.OpenWaitBox(0f, "");
			}
		}

		// Token: 0x0600585C RID: 22620 RVA: 0x001A8B24 File Offset: 0x001A6D24
		public static void OnRecv(NKMPacket_LEAGUE_PVP_MATCH_CANCEL_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_MATCH)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_MATCH().NKCUIGuantletMatch.OnRecv(sPacket);
			}
		}

		// Token: 0x0600585D RID: 22621 RVA: 0x001A8B5E File Offset: 0x001A6D5E
		public static void OnRecv(NKMPacket_LEAGUE_PVP_MATCH_FAIL_NOT sPacket)
		{
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetReservedLobbyTab(NKC_GAUNTLET_LOBBY_TAB.NGLT_LEAGUE);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LOBBY, true);
		}

		// Token: 0x0600585E RID: 22622 RVA: 0x001A8B7D File Offset: 0x001A6D7D
		public static void OnRecv(NKMPacket_LEAGUE_PVP_ACCEPT_NOT sPacket)
		{
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM))
			{
				NKCPrivatePVPRoomMgr.OnRecv(sPacket);
			}
			else
			{
				NKCPrivatePVPMgr.OnRecv(sPacket);
			}
			NKCLeaguePVPMgr.OnRecv(sPacket);
		}

		// Token: 0x0600585F RID: 22623 RVA: 0x001A8B9C File Offset: 0x001A6D9C
		public static void OnRecv(NKMPacket_LEAGUE_PVP_UPDATED_NOT sPacket)
		{
			NKCLeaguePVPMgr.OnRecv(sPacket);
		}

		// Token: 0x06005860 RID: 22624 RVA: 0x001A8BA4 File Offset: 0x001A6DA4
		public static void OnRecv(NKMPacket_DRAFT_PVP_GLOBAL_BAN_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCLeaguePVPMgr.OnRecv(sPacket);
		}

		// Token: 0x06005861 RID: 22625 RVA: 0x001A8BC1 File Offset: 0x001A6DC1
		public static void OnRecv(NKMPacket_LEAGUE_PVP_GIVEUP_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCLeaguePVPMgr.OnRecv(sPacket);
		}

		// Token: 0x06005862 RID: 22626 RVA: 0x001A8BDE File Offset: 0x001A6DDE
		public static void OnRecv(NKMPacket_DRAFT_PVP_PICK_UNIT_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCLeaguePVPMgr.OnRecv(sPacket);
		}

		// Token: 0x06005863 RID: 22627 RVA: 0x001A8BFB File Offset: 0x001A6DFB
		public static void OnRecv(NKMPacket_DRAFT_PVP_OPPONENT_BAN_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCLeaguePVPMgr.OnRecv(sPacket);
		}

		// Token: 0x06005864 RID: 22628 RVA: 0x001A8C18 File Offset: 0x001A6E18
		public static void OnRecv(NKMPacket_DRAFT_PVP_PICK_SHIP_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCLeaguePVPMgr.OnRecv(sPacket);
		}

		// Token: 0x06005865 RID: 22629 RVA: 0x001A8C35 File Offset: 0x001A6E35
		public static void OnRecv(NKMPacket_DRAFT_PVP_PICK_OPERATOR_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCLeaguePVPMgr.OnRecv(sPacket);
		}

		// Token: 0x06005866 RID: 22630 RVA: 0x001A8C52 File Offset: 0x001A6E52
		public static void OnRecv(NKMPacket_DRAFT_PVP_PICK_LEADER_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCLeaguePVPMgr.OnRecv(sPacket);
		}

		// Token: 0x06005867 RID: 22631 RVA: 0x001A8C6F File Offset: 0x001A6E6F
		public static void OnRecv(NKMPacket_DRAFT_PVP_SELECT_UNIT_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCLeaguePVPMgr.OnRecv(sPacket);
		}

		// Token: 0x06005868 RID: 22632 RVA: 0x001A8C8C File Offset: 0x001A6E8C
		public static void OnRecv(NKMPacket_LEAGUE_PVP_WEEKLY_RANKER_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_HOME)
			{
				NKCScenManager.GetScenManager().Get_SCEN_HOME().OnRecv(sPacket);
			}
		}

		// Token: 0x06005869 RID: 22633 RVA: 0x001A8CC0 File Offset: 0x001A6EC0
		public static void OnRecv(NKMPacket_LEAGUE_PVP_WEEKLY_REWARD_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().OnRecv(sPacket);
			}
		}

		// Token: 0x0600586A RID: 22634 RVA: 0x001A8CF8 File Offset: 0x001A6EF8
		public static void OnRecv(NKMPacket_LEAGUE_PVP_SEASON_REWARD_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			NKCPopupGauntletNewSeasonAlarm.SetPrevNKMPVPData(myUserData.m_LeagueData);
			NKCPopupGauntletOutgameReward.SetNKMPVPData(myUserData.m_LeagueData);
			myUserData.m_LeagueData = sPacket.pvpData;
			if (sPacket.rewardData != null)
			{
				myUserData.GetReward(sPacket.rewardData);
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().OnRecv(sPacket);
			}
		}

		// Token: 0x0600586B RID: 22635 RVA: 0x001A8D7D File Offset: 0x001A6F7D
		public static void OnRecv(NKMPacket_PHASE_START_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPhaseManager.SetPhaseModeState(sPacket.state);
			NKCPhaseManager.PlayNextPhase();
		}

		// Token: 0x0600586C RID: 22636 RVA: 0x001A8DA8 File Offset: 0x001A6FA8
		public static void OnRecv(NKMPacket_GAME_LOAD_ACK cNKMPacket_GAME_LOAD_ACK)
		{
			NKCRepeatOperaion nkcrepeatOperaion = NKCScenManager.GetScenManager().GetNKCRepeatOperaion();
			if (NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GAME_LOAD_ACK.errorCode, true, null, -2147483648))
			{
				NKCScenManager.GetScenManager().Get_SCEN_GAME().ReserveGameEndData(null);
				if (cNKMPacket_GAME_LOAD_ACK.gameData == null)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_ERROR_SERVER_GAME_DATA, delegate()
					{
						NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
					}, "");
				}
				if (nkcrepeatOperaion != null && nkcrepeatOperaion.GetIsOnGoing() && cNKMPacket_GAME_LOAD_ACK.costItemDataList != null && cNKMPacket_GAME_LOAD_ACK.costItemDataList.Count > 0)
				{
					nkcrepeatOperaion.SetCostIncreaseCount(nkcrepeatOperaion.GetCostIncreaseCount() + 1L);
				}
				NKM_SCEN_ID nowScenID = NKCScenManager.GetScenManager().GetNowScenID();
				if (nowScenID <= NKM_SCEN_ID.NSI_GAME)
				{
					if (nowScenID - NKM_SCEN_ID.NSI_LOGIN > 1)
					{
						if (nowScenID != NKM_SCEN_ID.NSI_GAME)
						{
							return;
						}
						if (cNKMPacket_GAME_LOAD_ACK.gameData != null && cNKMPacket_GAME_LOAD_ACK.gameData.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_TUTORIAL)
						{
							NKCScenManager.GetScenManager().Get_SCEN_HOME().OnRecv(cNKMPacket_GAME_LOAD_ACK, 1);
							return;
						}
						Debug.LogError("Dungeon Loaded from Game Scene!");
						return;
					}
				}
				else
				{
					switch (nowScenID)
					{
					case NKM_SCEN_ID.NSI_DUNGEON_ATK_READY:
					case NKM_SCEN_ID.NSI_CUTSCENE_DUNGEON:
					{
						int multiply = 1;
						if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_DUNGEON_ATK_READY)
						{
							multiply = NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY().GetLastMultiplyRewardCount();
						}
						NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(cNKMPacket_GAME_LOAD_ACK.gameData.m_DungeonID);
						if (dungeonTempletBase != null && dungeonTempletBase.StageTemplet != null)
						{
							NKCScenManager.CurrentUserData().SetLastPlayInfo(NKM_GAME_TYPE.NGT_DUNGEON, dungeonTempletBase.StageTemplet.Key);
						}
						NKCScenManager.GetScenManager().Get_NKC_SCEN_CUTSCEN_DUNGEON().OnRecv(cNKMPacket_GAME_LOAD_ACK, multiply);
						return;
					}
					case NKM_SCEN_ID.NSI_UNIT_LIST:
					case NKM_SCEN_ID.NSI_COLLECTION:
					case NKM_SCEN_ID.NSI_SHOP:
					case NKM_SCEN_ID.NSI_FRIEND:
					case NKM_SCEN_ID.NSI_DIVE_READY:
						return;
					case NKM_SCEN_ID.NSI_WARFARE_GAME:
					case NKM_SCEN_ID.NSI_WORLDMAP:
					case NKM_SCEN_ID.NSI_GAME_RESULT:
					case NKM_SCEN_ID.NSI_DIVE:
						break;
					default:
						if (nowScenID != NKM_SCEN_ID.NSI_RAID_READY && nowScenID != NKM_SCEN_ID.NSI_TRIM)
						{
							return;
						}
						break;
					}
				}
				int multiply2 = 1;
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME)
				{
					multiply2 = NKCScenManager.GetScenManager().WarfareGameData.rewardMultiply;
				}
				NKCScenManager.GetScenManager().Get_SCEN_HOME().OnRecv(cNKMPacket_GAME_LOAD_ACK, multiply2);
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_TRIM)
				{
					NKCScenManager.CurrentUserData().SetLastPlayInfo(NKM_GAME_TYPE.NGT_TRIM, 0);
				}
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME)
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().OpenWaitBox();
					return;
				}
				return;
			}
			if (nkcrepeatOperaion != null && nkcrepeatOperaion.GetIsOnGoing())
			{
				NKCPopupOKCancel.ClosePopupBox();
				nkcrepeatOperaion.Init();
				nkcrepeatOperaion.SetStopReason(NKCStringTable.GetString(cNKMPacket_GAME_LOAD_ACK.errorCode.ToString(), false));
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME_RESULT)
				{
					NKCPopupRepeatOperation.Instance.OpenForResult(delegate
					{
						NKMDungeonTempletBase dungeonTempletBase2 = NKMDungeonManager.GetDungeonTempletBase(cNKMPacket_GAME_LOAD_ACK.gameData.m_DungeonID);
						if (dungeonTempletBase2 != null && dungeonTempletBase2.StageTemplet != null)
						{
							if (!NKCScenManager.GetScenManager().Get_SCEN_OPERATION().PlayByFavorite)
							{
								NKCScenManager.GetScenManager().Get_SCEN_OPERATION().SetReservedEpisodeTemplet(dungeonTempletBase2.StageTemplet.EpisodeTemplet);
							}
							NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OPERATION, true);
						}
					});
				}
				else
				{
					NKCPopupRepeatOperation.Instance.OpenForResult(null);
				}
			}
			if (nkcrepeatOperaion != null)
			{
				nkcrepeatOperaion.Init();
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_LOGIN || NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_DUNGEON_ATK_READY || NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_CUTSCENE_DUNGEON)
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				NKCPopupMessageManager.AddPopupMessage(cNKMPacket_GAME_LOAD_ACK.errorCode, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME)
			{
				bool activeRepeatOperationOnOff = false;
				if (nkcrepeatOperaion != null)
				{
					activeRepeatOperationOnOff = nkcrepeatOperaion.GetIsOnGoing();
				}
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareGame().m_NKCWarfareGameHUD.SetActiveRepeatOperationOnOff(activeRepeatOperationOnOff);
			}
		}

		// Token: 0x0600586D RID: 22637 RVA: 0x001A90F1 File Offset: 0x001A72F1
		public static void OnRecv(NKMPacket_GAME_GIVEUP_ACK cNKMPacket_GAME_GIVEUP_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GAME_GIVEUP_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCTrimManager.TrimModeState != null)
			{
				NKCTrimManager.SetGiveUpState(true);
			}
		}

		// Token: 0x0600586E RID: 22638 RVA: 0x001A9118 File Offset: 0x001A7318
		public static void UpdateLeagueGiveupUserData(NKMPVPResultDataForClient _NKMPVPResultData)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (_NKMPVPResultData.myInfo != null)
			{
				PvpState.SetPrevScore(myUserData.m_LeagueData.Score);
				myUserData.m_LeagueData = _NKMPVPResultData.myInfo;
				myUserData.LastPvpPointChargeTimeUTC = NKCSynchronizedTime.ToUtcTime(_NKMPVPResultData.pvpPointChargeTime);
			}
			myUserData.m_InventoryData.AddItemMisc(_NKMPVPResultData.pvpPoint);
			myUserData.m_InventoryData.UpdateItemInfo(_NKMPVPResultData.pvpChargePoint);
			if (_NKMPVPResultData.history != null)
			{
				myUserData.m_LeaguePvpHistory.Add(_NKMPVPResultData.history);
			}
			NKCUIGauntletLobbyLeague.SetAlertDemotion(NKCUtil.IsPVPDemotionAlert(NKM_GAME_TYPE.NGT_PVP_LEAGUE, myUserData.m_PvpData));
			NKCPublisherModule.Push.UpdateLocalPush(NKC_GAME_OPTION_ALARM_GROUP.PVP_POINT_COMPLETE, true);
		}

		// Token: 0x0600586F RID: 22639 RVA: 0x001A91C0 File Offset: 0x001A73C0
		public static void UpdateUserData(bool bWin, NKMDungeonClearData _NKMDungeonClearData, NKMEpisodeCompleteData _NKMEpisodeCompleteData = null, WarfareSyncData _NKMWarfareGameSyncDataPack = null, NKMPVPResultDataForClient _NKMPVPResultData = null, NKMDiveSyncData _NKMDiveSyncData = null, NKMRaidBossResultData _NKMRaidBossResultData = null, List<UnitLoyaltyUpdateData> lstUnitUpdateData = null, NKMShadowGameResult _NKMShadowGameResult = null, NKMFierceResultData _NKMFierceResultData = null, NKMPhaseClearData _NKMPhaseClearData = null)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (_NKMDungeonClearData != null)
			{
				if (bWin)
				{
					if (!myUserData.CheckDungeonClear(_NKMDungeonClearData.dungeonId))
					{
						if (myUserData.m_dicNKMDungeonClearData.Count == 0)
						{
							NKCPublisherModule.Statistics.OnFirstEpisodeClear();
						}
						NKCUtil.m_sHsFirstClearDungeon.Add(_NKMDungeonClearData.dungeonId);
					}
					myUserData.SetDungeonClearDataOnlyTrue(_NKMDungeonClearData);
				}
				myUserData.GetReward(_NKMDungeonClearData.rewardData);
				myUserData.GetReward(_NKMDungeonClearData.missionReward);
				myUserData.GetReward(_NKMDungeonClearData.oneTimeRewards);
				NKCRepeatOperaion nkcrepeatOperaion = NKCScenManager.GetScenManager().GetNKCRepeatOperaion();
				if (nkcrepeatOperaion != null && nkcrepeatOperaion.GetIsOnGoing())
				{
					if (_NKMDungeonClearData != null)
					{
						nkcrepeatOperaion.AddReward(_NKMDungeonClearData.rewardData);
						nkcrepeatOperaion.AddReward(_NKMDungeonClearData.missionReward);
						nkcrepeatOperaion.AddReward(_NKMDungeonClearData.oneTimeRewards);
					}
					if (_NKMPhaseClearData != null)
					{
						nkcrepeatOperaion.AddReward(_NKMPhaseClearData.rewardData);
						nkcrepeatOperaion.AddReward(_NKMPhaseClearData.missionReward);
						nkcrepeatOperaion.AddReward(_NKMPhaseClearData.oneTimeRewards);
					}
				}
			}
			NKMGameData gameData = NKCScenManager.GetScenManager().GetGameClient().GetGameData();
			if (gameData != null)
			{
				switch (gameData.m_NKM_GAME_TYPE)
				{
				case NKM_GAME_TYPE.NGT_DUNGEON:
				{
					NKCRepeatOperaion nkcrepeatOperaion2 = NKCScenManager.GetScenManager().GetNKCRepeatOperaion();
					if (_NKMDungeonClearData == null || !bWin)
					{
						if (nkcrepeatOperaion2.GetIsOnGoing())
						{
							NKCScenManager.GetScenManager().GetNKCRepeatOperaion().SetCurrRepeatCount(nkcrepeatOperaion2.GetCurrRepeatCount() + 1L);
							NKCScenManager.GetScenManager().GetNKCRepeatOperaion().SetAlarmRepeatOperationQuitByDefeat(true);
						}
						nkcrepeatOperaion2.Init();
						if (NKCScenManager.GetScenManager().GetGameClient() != null && NKCScenManager.GetScenManager().GetGameClient().GetGameHud() != null)
						{
							NKCScenManager.GetScenManager().GetGameClient().GetGameHud().GetNKCGameHUDRepeatOperation().ResetBtnOnOffUI();
						}
					}
					else if (nkcrepeatOperaion2.GetIsOnGoing())
					{
						nkcrepeatOperaion2.SetCurrRepeatCount(nkcrepeatOperaion2.GetCurrRepeatCount() + 1L);
						if (nkcrepeatOperaion2.GetCurrRepeatCount() >= nkcrepeatOperaion2.GetMaxRepeatCount())
						{
							nkcrepeatOperaion2.Init();
							nkcrepeatOperaion2.SetStopReason(NKCUtilString.GET_STRING_REPEAT_OPERATION_IS_TERMINATED);
							nkcrepeatOperaion2.SetAlarmRepeatOperationSuccess(true);
						}
					}
					break;
				}
				case NKM_GAME_TYPE.NGT_DIVE:
					if (_NKMDungeonClearData != null)
					{
					}
					break;
				case NKM_GAME_TYPE.NGT_RAID:
				case NKM_GAME_TYPE.NGT_RAID_SOLO:
					if (_NKMRaidBossResultData != null)
					{
						NKMRaidDetailData nkmraidDetailData = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Find(gameData.m_RaidUID);
						if (nkmraidDetailData != null)
						{
							nkmraidDetailData.curHP = _NKMRaidBossResultData.curHP;
							NKMRaidJoinData nkmraidJoinData = nkmraidDetailData.FindJoinData(NKCScenManager.CurrentUserData().m_UserUID);
							if (nkmraidJoinData != null)
							{
								NKMRaidJoinData nkmraidJoinData2 = nkmraidJoinData;
								nkmraidJoinData2.tryCount += 1;
							}
						}
					}
					break;
				case NKM_GAME_TYPE.NGT_SHADOW_PALACE:
					if (_NKMShadowGameResult != null)
					{
						NKMShadowPalace shadowPalace = myUserData.m_ShadowPalace;
						shadowPalace.life = _NKMShadowGameResult.life;
						NKMPalaceData nkmpalaceData = shadowPalace.palaceDataList.Find((NKMPalaceData v) => v.palaceId == _NKMShadowGameResult.palaceId);
						if (nkmpalaceData == null)
						{
							nkmpalaceData = new NKMPalaceData();
							nkmpalaceData.palaceId = _NKMShadowGameResult.palaceId;
							shadowPalace.palaceDataList.Add(nkmpalaceData);
						}
						nkmpalaceData.currentDungeonId = _NKMShadowGameResult.currentDungeonId;
						if (_NKMShadowGameResult.dungeonData != null)
						{
							NKMPalaceDungeonData nkmpalaceDungeonData = nkmpalaceData.dungeonDataList.Find((NKMPalaceDungeonData v) => v.dungeonId == _NKMShadowGameResult.dungeonData.dungeonId);
							if (nkmpalaceDungeonData == null)
							{
								nkmpalaceDungeonData = new NKMPalaceDungeonData();
								nkmpalaceDungeonData.dungeonId = _NKMShadowGameResult.dungeonData.dungeonId;
								nkmpalaceData.dungeonDataList.Add(nkmpalaceDungeonData);
							}
							nkmpalaceDungeonData.recentTime = _NKMShadowGameResult.dungeonData.recentTime;
						}
						if (_NKMShadowGameResult.rewardData != null || _NKMShadowGameResult.life == 0)
						{
							shadowPalace.currentPalaceId = 0;
							if (_NKMShadowGameResult.rewardData != null)
							{
								NKMShadowPalaceManager.SaveLastClearedPalace(_NKMShadowGameResult.palaceId);
								myUserData.GetReward(_NKMShadowGameResult.rewardData);
							}
							List<int> list = new List<int>();
							for (int i = 0; i < nkmpalaceData.dungeonDataList.Count; i++)
							{
								NKMPalaceDungeonData nkmpalaceDungeonData2 = nkmpalaceData.dungeonDataList[i];
								list.Add(nkmpalaceDungeonData2.bestTime);
								if (_NKMShadowGameResult.newRecord)
								{
									nkmpalaceDungeonData2.bestTime = nkmpalaceDungeonData2.recentTime;
								}
							}
							NKCScenManager.GetScenManager().Get_NKC_SCEN_SHADOW_RESULT().SetData(_NKMShadowGameResult, list);
						}
					}
					break;
				case NKM_GAME_TYPE.NGT_PHASE:
				{
					NKCRepeatOperaion nkcrepeatOperaion3 = NKCScenManager.GetScenManager().GetNKCRepeatOperaion();
					if (!bWin)
					{
						if (nkcrepeatOperaion3.GetIsOnGoing())
						{
							NKCScenManager.GetScenManager().GetNKCRepeatOperaion().SetCurrRepeatCount(nkcrepeatOperaion3.GetCurrRepeatCount() + 1L);
							NKCScenManager.GetScenManager().GetNKCRepeatOperaion().SetAlarmRepeatOperationQuitByDefeat(true);
						}
						nkcrepeatOperaion3.Init();
						if (NKCScenManager.GetScenManager().GetGameClient() != null && NKCScenManager.GetScenManager().GetGameClient().GetGameHud() != null)
						{
							NKCScenManager.GetScenManager().GetGameClient().GetGameHud().GetNKCGameHUDRepeatOperation().ResetBtnOnOffUI();
						}
					}
					if (_NKMPhaseClearData != null && bWin && nkcrepeatOperaion3.GetIsOnGoing())
					{
						nkcrepeatOperaion3.SetCurrRepeatCount(nkcrepeatOperaion3.GetCurrRepeatCount() + 1L);
						if (nkcrepeatOperaion3.GetCurrRepeatCount() >= nkcrepeatOperaion3.GetMaxRepeatCount())
						{
							nkcrepeatOperaion3.Init();
							nkcrepeatOperaion3.SetStopReason(NKCUtilString.GET_STRING_REPEAT_OPERATION_IS_TERMINATED);
							nkcrepeatOperaion3.SetAlarmRepeatOperationSuccess(true);
						}
					}
					break;
				}
				}
			}
			NKMDiveGameData diveGameData = myUserData.m_DiveGameData;
			if (diveGameData != null && _NKMDiveSyncData != null)
			{
				diveGameData.UpdateData(_NKMDungeonClearData != null && bWin, _NKMDiveSyncData);
				if (NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_BattleResultData() == null || NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_BattleResultData().m_BATTLE_RESULT_TYPE != BATTLE_RESULT_TYPE.BRT_WIN)
				{
					NKCDiveGame.SetReservedUnitDieShow(true, diveGameData.Player.PlayerBase.ReservedDeckIndex, NKC_DIVE_GAME_UNIT_DIE_TYPE.NDGUDT_EXPLOSION);
				}
				if (_NKMDiveSyncData.AddedSlotSets.Count > 0)
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_DIVE().SetSectorAddEvent(true);
				}
				if (diveGameData.Player.PlayerBase.State == NKMDivePlayerState.Clear || diveGameData.Player.PlayerBase.State == NKMDivePlayerState.Annihilation)
				{
					if (diveGameData.Player.PlayerBase.State == NKMDivePlayerState.Clear)
					{
						if (myUserData.m_DiveClearData == null)
						{
							myUserData.m_DiveClearData = new HashSet<int>();
						}
						if (!diveGameData.Floor.Templet.IsEventDive && !myUserData.m_DiveClearData.Contains(diveGameData.Floor.Templet.StageID))
						{
							myUserData.m_DiveClearData.Add(diveGameData.Floor.Templet.StageID);
						}
						if (_NKMDiveSyncData.RewardData != null)
						{
							myUserData.GetReward(_NKMDiveSyncData.RewardData);
						}
						if (_NKMDiveSyncData.ArtifactRewardData != null)
						{
							myUserData.GetReward(_NKMDiveSyncData.ArtifactRewardData);
						}
						if (_NKMDiveSyncData.StormMiscReward != null)
						{
							NKMRewardData nkmrewardData = new NKMRewardData();
							nkmrewardData.SetMiscItemData(new List<NKMItemMiscData>
							{
								_NKMDiveSyncData.StormMiscReward
							});
							myUserData.GetReward(nkmrewardData);
						}
						if (!myUserData.CheckDiveHistory(diveGameData.Floor.Templet.StageID))
						{
							myUserData.m_LastDiveHistoryData = new HashSet<int>(myUserData.m_DiveHistoryData);
							myUserData.m_DiveHistoryData.Add(diveGameData.Floor.Templet.StageID);
						}
						else if (!myUserData.m_LastDiveHistoryData.Contains(diveGameData.Floor.Templet.StageID))
						{
							myUserData.m_LastDiveHistoryData.Add(diveGameData.Floor.Templet.StageID);
						}
					}
					NKC_SCEN_DIVE_RESULT nkc_SCEN_DIVE_RESULT = NKCScenManager.GetScenManager().Get_NKC_SCEN_DIVE_RESULT();
					if (nkc_SCEN_DIVE_RESULT != null)
					{
						nkc_SCEN_DIVE_RESULT.SetData(diveGameData.Player.PlayerBase.State == NKMDivePlayerState.Clear, diveGameData.Floor.Templet.IsEventDive, _NKMDiveSyncData.RewardData, _NKMDiveSyncData.ArtifactRewardData, _NKMDiveSyncData.StormMiscReward, diveGameData.Player.PlayerBase.Artifacts, new NKMDeckIndex(NKM_DECK_TYPE.NDT_DIVE, diveGameData.Player.PlayerBase.LeaderDeckIndex), diveGameData.Floor.Templet);
					}
					NKCPacketHandlersLobby.ProcessWorldmapContentsAfterDiveEnd();
					myUserData.ClearDiveGameData();
				}
			}
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData != null && _NKMWarfareGameSyncDataPack != null)
			{
				if (NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME() != null)
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().SetBattleInfo(warfareGameData, _NKMWarfareGameSyncDataPack);
				}
				warfareGameData.UpdateData(_NKMWarfareGameSyncDataPack);
				Debug.Log("Warfare Game State : " + warfareGameData.warfareGameState.ToString());
				if (warfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_RESULT && warfareGameData.isWinTeamA && !NKCScenManager.GetScenManager().GetMyUserData().CheckWarfareClear(warfareGameData.warfareTempletID))
				{
					NKCUtil.m_sHsFirstClearWarfare.Add(warfareGameData.warfareTempletID);
				}
			}
			myUserData.UpdateEpisodeCompleteData(_NKMEpisodeCompleteData);
			if (_NKMPhaseClearData != null)
			{
				NKCPhaseManager.UpdateClearData(_NKMPhaseClearData);
				myUserData.GetReward(_NKMPhaseClearData.rewardData);
				myUserData.GetReward(_NKMPhaseClearData.missionReward);
				myUserData.GetReward(_NKMPhaseClearData.oneTimeRewards);
			}
			if (_NKMPVPResultData != null && gameData != null)
			{
				if (gameData.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_LEAGUE)
				{
					if (_NKMPVPResultData.myInfo != null)
					{
						PvpState.SetPrevScore(myUserData.m_LeagueData.Score);
						myUserData.m_LeagueData = _NKMPVPResultData.myInfo;
						myUserData.LastPvpPointChargeTimeUTC = NKCSynchronizedTime.ToUtcTime(_NKMPVPResultData.pvpPointChargeTime);
					}
					myUserData.m_InventoryData.AddItemMisc(_NKMPVPResultData.pvpPoint);
					myUserData.m_InventoryData.UpdateItemInfo(_NKMPVPResultData.pvpChargePoint);
					if (_NKMPVPResultData.history != null)
					{
						myUserData.m_LeaguePvpHistory.Add(_NKMPVPResultData.history);
					}
					NKCUIGauntletLobbyLeague.SetAlertDemotion(NKCUtil.IsPVPDemotionAlert(gameData.m_NKM_GAME_TYPE, myUserData.m_LeagueData));
				}
				else if (gameData.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_PRIVATE)
				{
					if (_NKMPVPResultData.history != null)
					{
						myUserData.m_PrivatePvpHistory.Add(_NKMPVPResultData.history);
					}
				}
				else if (gameData.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_RANK)
				{
					if (_NKMPVPResultData.myInfo != null)
					{
						PvpState.SetPrevScore(myUserData.m_PvpData.Score);
						myUserData.m_PvpData = _NKMPVPResultData.myInfo;
						myUserData.LastPvpPointChargeTimeUTC = NKCSynchronizedTime.ToUtcTime(_NKMPVPResultData.pvpPointChargeTime);
						myUserData.m_LeagueOpen = _NKMPVPResultData.leaguePvpOpen;
					}
					myUserData.m_InventoryData.AddItemMisc(_NKMPVPResultData.pvpPoint);
					myUserData.m_InventoryData.UpdateItemInfo(_NKMPVPResultData.pvpChargePoint);
					if (_NKMPVPResultData.history != null)
					{
						myUserData.m_SyncPvpHistory.Add(_NKMPVPResultData.history);
					}
					NKCUIGauntletLobbyRank.SetAlertDemotion(NKCUtil.IsPVPDemotionAlert(gameData.m_NKM_GAME_TYPE, myUserData.m_PvpData));
				}
				NKCPublisherModule.Push.UpdateLocalPush(NKC_GAME_OPTION_ALARM_GROUP.PVP_POINT_COMPLETE, true);
			}
			if (lstUnitUpdateData != null)
			{
				foreach (UnitLoyaltyUpdateData unitLoyaltyUpdateData in lstUnitUpdateData)
				{
					NKMUnitData unitFromUID = myUserData.m_ArmyData.GetUnitFromUID(unitLoyaltyUpdateData.unitUid);
					if (unitFromUID != null)
					{
						unitFromUID.loyalty = unitLoyaltyUpdateData.loyalty;
						unitFromUID.SetOfficeRoomId(unitLoyaltyUpdateData.officeRoomId, unitLoyaltyUpdateData.officeGrade, unitLoyaltyUpdateData.heartGaugeStartTime);
					}
				}
			}
		}

		// Token: 0x06005870 RID: 22640 RVA: 0x001A9C6C File Offset: 0x001A7E6C
		public static void OnRecv(NKMPacket_GAME_END_NOT cPacket_GAME_END_NOT)
		{
			if (NKCReplayMgr.IsRecording())
			{
				NKCScenManager.GetScenManager().GetNKCReplayMgr().FillReplayData(cPacket_GAME_END_NOT);
			}
			if (NKCLeaguePVPMgr.OnRecv(cPacket_GAME_END_NOT))
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAME_RESULT, true);
				return;
			}
			if (NKCScenManager.GetScenManager().GetGameClient().IsObserver(NKCScenManager.CurrentUserData()))
			{
				NKCUIResult.BattleResultData resultData = new NKCUIResult.BattleResultData();
				NKCScenManager.GetScenManager().Get_SCEN_GAME().ReserveGameEndData(resultData);
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME && !cPacket_GAME_END_NOT.giveup)
			{
				Debug.Log("NKMPacket_GAME_END_NOT making resultUIData");
				NKMGame gameClient = NKCScenManager.GetScenManager().GetGameClient();
				NKMGameData gameData = NKCScenManager.GetScenManager().GetGameClient().GetGameData();
				if (gameData != null)
				{
					NKCUIResult.BattleResultData battleResultData;
					if (gameData.IsPVE())
					{
						int num = (cPacket_GAME_END_NOT.dungeonClearData != null) ? cPacket_GAME_END_NOT.dungeonClearData.dungeonId : gameData.m_DungeonID;
						NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(num);
						if (num == 1007)
						{
							Debug.Log("Tutorial final stage cleared!");
							NKCPatchUtility.SaveTutorialClearedStatus();
						}
						NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.DungeonGameClear, num, null);
						NKM_GAME_TYPE nkm_GAME_TYPE = gameData.m_NKM_GAME_TYPE;
						int num2;
						if (nkm_GAME_TYPE != NKM_GAME_TYPE.NGT_WARFARE)
						{
							if (nkm_GAME_TYPE == NKM_GAME_TYPE.NGT_PHASE)
							{
								num2 = NKCPhaseManager.PhaseModeState.stageId;
							}
							else if (dungeonTempletBase != null && dungeonTempletBase.StageTemplet != null)
							{
								num2 = dungeonTempletBase.StageTemplet.Key;
							}
							else
							{
								num2 = 0;
							}
						}
						else
						{
							NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(gameData.m_WarfareID);
							if (nkmwarfareTemplet != null && nkmwarfareTemplet.StageTemplet != null)
							{
								num2 = nkmwarfareTemplet.StageTemplet.Key;
							}
							else
							{
								num2 = 0;
							}
						}
						battleResultData = NKCUIResult.MakePvEBattleResultData(gameData.m_NKM_GAME_TYPE, gameClient, cPacket_GAME_END_NOT, num, num2);
						if (gameData.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_FIERCE)
						{
							NKCScenManager.CurrentUserData().SetLastPlayInfo(NKM_GAME_TYPE.NGT_FIERCE, 0);
						}
						else if (gameData.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_TRIM)
						{
							NKCScenManager.CurrentUserData().SetLastPlayInfo(NKM_GAME_TYPE.NGT_TRIM, 0);
						}
						else if (gameData.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_SHADOW_PALACE)
						{
							NKCScenManager.CurrentUserData().SetLastPlayInfo(NKM_GAME_TYPE.NGT_SHADOW_PALACE, 0);
						}
						else if (num2 > 0)
						{
							NKCScenManager.CurrentUserData().SetLastPlayInfo(NKM_GAME_TYPE.NGT_DUNGEON, num2);
						}
						if (dungeonTempletBase != null)
						{
							string key = string.Format("{0}_{1}", myUserData.m_UserUID, dungeonTempletBase.m_DungeonStrID);
							if (battleResultData != null && battleResultData.IsWin && PlayerPrefs.HasKey(key))
							{
								PlayerPrefs.DeleteKey(key);
							}
						}
						myUserData.UpdateStagePlayData(cPacket_GAME_END_NOT.stagePlayData);
					}
					else if (gameData.IsPVP())
					{
						BATTLE_RESULT_TYPE battle_RESULT_TYPE = BATTLE_RESULT_TYPE.BRT_LOSE;
						if (cPacket_GAME_END_NOT.pvpResultData != null)
						{
							if (cPacket_GAME_END_NOT.pvpResultData.result == PVP_RESULT.WIN)
							{
								battle_RESULT_TYPE = BATTLE_RESULT_TYPE.BRT_WIN;
							}
							else if (cPacket_GAME_END_NOT.pvpResultData.result == PVP_RESULT.LOSE)
							{
								battle_RESULT_TYPE = BATTLE_RESULT_TYPE.BRT_LOSE;
							}
							else
							{
								battle_RESULT_TYPE = BATTLE_RESULT_TYPE.BRT_DRAW;
							}
						}
						NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.PvPGameFinished, (int)battle_RESULT_TYPE, null);
						NKMItemMiscData cNKMItemMiscData = null;
						if (cPacket_GAME_END_NOT.pvpResultData != null)
						{
							cNKMItemMiscData = cPacket_GAME_END_NOT.pvpResultData.pvpPoint;
						}
						battleResultData = NKCUIResult.MakePvPResultData(battle_RESULT_TYPE, cNKMItemMiscData, NKCUIBattleStatistics.MakeBattleData(gameClient, cPacket_GAME_END_NOT), gameData.GetGameType());
					}
					else
					{
						Debug.LogError("Undefined GameType");
						int dungeonID = (cPacket_GAME_END_NOT.dungeonClearData != null) ? cPacket_GAME_END_NOT.dungeonClearData.dungeonId : gameData.m_DungeonID;
						battleResultData = NKCUIResult.MakeMissionResultData(myUserData.m_ArmyData, dungeonID, 0, cPacket_GAME_END_NOT.win, cPacket_GAME_END_NOT.dungeonClearData, cPacket_GAME_END_NOT.deckIndex, NKCUIBattleStatistics.MakeBattleData(gameClient, cPacket_GAME_END_NOT), cPacket_GAME_END_NOT.updatedUnits, 1);
					}
					NKCScenManager.GetScenManager().Get_SCEN_GAME().ReserveGameEndData(battleResultData);
				}
				else
				{
					Debug.LogError("FATAL : GAMEDATA NULL");
				}
			}
			NKCRepeatOperaion nkcrepeatOperaion = NKCScenManager.GetScenManager().GetNKCRepeatOperaion();
			if (nkcrepeatOperaion != null && nkcrepeatOperaion.GetIsOnGoing() && cPacket_GAME_END_NOT.costItemDataList != null && cPacket_GAME_END_NOT.costItemDataList.Count > 0)
			{
				nkcrepeatOperaion.SetCostIncreaseCount(nkcrepeatOperaion.GetCostIncreaseCount() + 1L);
			}
			NKCPacketHandlersLobby.UpdateUserData(cPacket_GAME_END_NOT.win, cPacket_GAME_END_NOT.dungeonClearData, cPacket_GAME_END_NOT.episodeCompleteData, cPacket_GAME_END_NOT.warfareSyncData, cPacket_GAME_END_NOT.pvpResultData, cPacket_GAME_END_NOT.diveSyncData, cPacket_GAME_END_NOT.raidBossResultData, cPacket_GAME_END_NOT.updatedUnits, cPacket_GAME_END_NOT.shadowGameResult, cPacket_GAME_END_NOT.fierceResultData, cPacket_GAME_END_NOT.phaseClearData);
			myUserData.m_InventoryData.UpdateItemInfo(cPacket_GAME_END_NOT.costItemDataList);
			NKCPhaseManager.SetPhaseModeState(cPacket_GAME_END_NOT.phaseModeState);
			NKC_SCEN_TRIM nkc_SCEN_TRIM = NKCScenManager.GetScenManager().Get_NKC_SCEN_TRIM();
			if (nkc_SCEN_TRIM != null)
			{
				nkc_SCEN_TRIM.SetReservedTrim(NKCTrimManager.TrimModeState);
			}
			if (NKCTrimManager.GiveUpState)
			{
				NKCTrimManager.ClearTrimModeState();
			}
			else
			{
				NKCTrimManager.SetTrimModeState(cPacket_GAME_END_NOT.trimModeState);
			}
			if (!NKCPhaseManager.IsPhaseOnGoing())
			{
				NKCKillCountManager.CurrentStageKillCount = 0L;
				NKCPhaseManager.ClearTempUnitData();
			}
			else
			{
				NKCPhaseManager.SaveTempUnitData(NKCScenManager.GetScenManager().GetGameClient(), cPacket_GAME_END_NOT.gameRecord);
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME && cPacket_GAME_END_NOT.giveup)
			{
				NKCScenManager.GetScenManager().GetNKCRepeatOperaion().SetAlarmRepeatOperationQuitByDefeat(false);
				NKCScenManager.GetScenManager().Get_SCEN_GAME().DoAfterGiveUp();
			}
			NKCKillCountManager.UpdateKillCountData(cPacket_GAME_END_NOT.killCountData);
		}

		// Token: 0x06005871 RID: 22641 RVA: 0x001AA0E4 File Offset: 0x001A82E4
		public static void OnRecv(NKMPacket_ASYNC_PVP_GAME_END_NOT sPacket)
		{
			if (NKCReplayMgr.IsRecording())
			{
				NKCScenManager.GetScenManager().GetNKCReplayMgr().FillReplayData(sPacket);
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME)
			{
				NKMGame gameClient = NKCScenManager.GetScenManager().GetGameClient();
				NKMGameData gameData = NKCScenManager.GetScenManager().GetGameClient().GetGameData();
				if ((gameData != null && gameData.GetGameType() == NKM_GAME_TYPE.NGT_ASYNC_PVP) || gameData.GetGameType() == NKM_GAME_TYPE.NGT_PVP_STRATEGY || gameData.GetGameType() == NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC || gameData.GetGameType() == NKM_GAME_TYPE.NGT_PVP_STRATEGY_REVENGE)
				{
					Debug.Log("[AsyncPvpGameEndNot] CorrectGameType");
					BATTLE_RESULT_TYPE battleResultType = BATTLE_RESULT_TYPE.BRT_LOSE;
					Debug.Log(string.Format("[AsyncPvpGameEndNot] Result : {0}", sPacket.result));
					switch (sPacket.result)
					{
					case PVP_RESULT.WIN:
						battleResultType = BATTLE_RESULT_TYPE.BRT_WIN;
						break;
					case PVP_RESULT.LOSE:
						battleResultType = BATTLE_RESULT_TYPE.BRT_LOSE;
						break;
					case PVP_RESULT.DRAW:
						battleResultType = BATTLE_RESULT_TYPE.BRT_DRAW;
						break;
					}
					NKCUIResult.BattleResultData resultData = NKCUIResult.MakePvPResultData(battleResultType, sPacket.gainPointItem, NKCUIBattleStatistics.MakeBattleData(gameClient, sPacket.gameRecord, gameData.GetGameType()), gameData.GetGameType());
					NKCScenManager.GetScenManager().Get_SCEN_GAME().ReserveGameEndData(resultData);
					int maxScore = myUserData.m_AsyncData.MaxScore;
					myUserData.m_AsyncData = sPacket.pvpState;
					Debug.Log(string.Format("[AsyncPvpGameEndNot] PvpState is null? : {0}", sPacket.pvpState == null));
					myUserData.m_AsyncPvpHistory.Add(sPacket.history);
					Debug.Log(string.Format("[AsyncPvpGameEndNot] History is null? : {0}", sPacket.history == null));
					myUserData.LastPvpPointChargeTimeUTC = NKCSynchronizedTime.ToUtcTime(sPacket.pointChargeTime);
					myUserData.m_RankOpen = sPacket.rankPvpOpen;
					NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetAsyncTargetList(sPacket.targetList);
					Debug.Log(string.Format("[AsyncPvpGameEndNot] targetList is null? : {0}", sPacket.targetList == null));
					if (gameData.GetGameType() == NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC && NKCScenManager.CurrentUserData().m_NpcData.MaxOpenedTier < sPacket.npcMaxOpenedTier)
					{
						NKCScenManager.CurrentUserData().m_NpcData.MaxOpenedTier = sPacket.npcMaxOpenedTier;
						NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetReserveOpenNpcBotTier(sPacket.npcMaxOpenedTier);
					}
				}
			}
			myUserData.m_InventoryData.AddItemMisc(sPacket.gainPointItem);
			myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItem);
		}

		// Token: 0x06005872 RID: 22642 RVA: 0x001AA30F File Offset: 0x001A850F
		public static void OnRecv(NKMPacket_STRATEGY_PVP_REFRESH_NOT sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.CurrentUserData().m_AsyncData = sPacket.data;
		}

		// Token: 0x06005873 RID: 22643 RVA: 0x001AA336 File Offset: 0x001A8536
		public static void OnRecv(NKMPacket_PVP_RANK_LIST_ACK cNKMPacket_PVP_RANK_LIST_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_PVP_RANK_LIST_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().OnRecv(cNKMPacket_PVP_RANK_LIST_ACK);
			}
		}

		// Token: 0x06005874 RID: 22644 RVA: 0x001AA36B File Offset: 0x001A856B
		public static void OnRecv(NKMPacket_LEAGUE_PVP_RANK_LIST_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().OnRecv(sPacket);
			}
		}

		// Token: 0x06005875 RID: 22645 RVA: 0x001AA3A0 File Offset: 0x001A85A0
		public static void OnRecv(NKMPacket_PVP_CHARGE_POINT_REFRESH_ACK cNKMPacket_PVP_CHARGE_POINT_REFRESH_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_PVP_CHARGE_POINT_REFRESH_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.m_InventoryData.UpdateItemInfo(cNKMPacket_PVP_CHARGE_POINT_REFRESH_ACK.itemData);
			if (cNKMPacket_PVP_CHARGE_POINT_REFRESH_ACK.itemData != null)
			{
				if (cNKMPacket_PVP_CHARGE_POINT_REFRESH_ACK.itemData.ItemID == 9)
				{
					myUserData.LastPvpPointChargeTimeUTC = NKCSynchronizedTime.ToUtcTime(cNKMPacket_PVP_CHARGE_POINT_REFRESH_ACK.chrageTime);
				}
				else if (cNKMPacket_PVP_CHARGE_POINT_REFRESH_ACK.itemData.ItemID == 6)
				{
					myUserData.LastPvpPointChargeTimeUTC = NKCSynchronizedTime.ToUtcTime(cNKMPacket_PVP_CHARGE_POINT_REFRESH_ACK.chrageTime);
				}
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().OnRecv(cNKMPacket_PVP_CHARGE_POINT_REFRESH_ACK);
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_HOME)
			{
				NKCScenManager.GetScenManager().Get_SCEN_HOME().OnRecv(cNKMPacket_PVP_CHARGE_POINT_REFRESH_ACK);
			}
		}

		// Token: 0x06005876 RID: 22646 RVA: 0x001AA464 File Offset: 0x001A8664
		public static void OnRecv(NKMPacket_WARFARE_GAME_START_ACK cNKMPacket_WARFARE_GAME_START_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_WARFARE_GAME_START_ACK.errorCode, true, null, -2147483648))
			{
				if (NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing())
				{
					NKCPopupOKCancel.ClosePopupBox();
					NKCScenManager.GetScenManager().GetNKCRepeatOperaion().Init();
					NKCScenManager.GetScenManager().GetNKCRepeatOperaion().SetStopReason(NKCStringTable.GetString(cNKMPacket_WARFARE_GAME_START_ACK.errorCode.ToString(), false));
					NKCPopupRepeatOperation.Instance.OpenForResult(null);
				}
				NKCScenManager.GetScenManager().GetNKCRepeatOperaion().Init();
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME)
				{
					bool isOnGoing = NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing();
					NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareGame().m_NKCWarfareGameHUD.SetActiveRepeatOperationOnOff(isOnGoing);
					NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareGame().SetUserUnitDeckWarfareState();
				}
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.m_InventoryData.UpdateItemInfo(cNKMPacket_WARFARE_GAME_START_ACK.costItemDataList);
			NKCScenManager.GetScenManager().SetWarfareGameData(cNKMPacket_WARFARE_GAME_START_ACK.warfareGameData);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().OnRecv(cNKMPacket_WARFARE_GAME_START_ACK);
				if (cNKMPacket_WARFARE_GAME_START_ACK.warfareGameData != null)
				{
					NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(cNKMPacket_WARFARE_GAME_START_ACK.warfareGameData.warfareTempletID);
					string key = string.Format("{0}_{1}", myUserData.m_UserUID, nkmwarfareTemplet.m_WarfareStrID);
					if (!PlayerPrefs.HasKey(key) && !NKCScenManager.CurrentUserData().CheckWarfareClear(nkmwarfareTemplet.m_WarfareStrID))
					{
						PlayerPrefs.SetInt(key, 0);
					}
				}
			}
		}

		// Token: 0x06005877 RID: 22647 RVA: 0x001AA5DC File Offset: 0x001A87DC
		public static void OnRecv(NKMPacket_WARFARE_GAME_MOVE_ACK cNKMPacket_WARFARE_GAME_MOVE_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME && NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareGame() != null)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareGame().SetPause(false);
			}
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_WARFARE_GAME_MOVE_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().OnRecv(cNKMPacket_WARFARE_GAME_MOVE_ACK);
			}
		}

		// Token: 0x06005878 RID: 22648 RVA: 0x001AA658 File Offset: 0x001A8858
		public static void OnRecv(NKMPacket_WARFARE_GAME_TURN_FINISH_ACK cNKMPacket_WARFARE_GAME_TURN_FINISH_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME && NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareGame() != null)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareGame().SetPause(false);
			}
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_WARFARE_GAME_TURN_FINISH_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().OnRecv(cNKMPacket_WARFARE_GAME_TURN_FINISH_ACK);
			}
		}

		// Token: 0x06005879 RID: 22649 RVA: 0x001AA6D4 File Offset: 0x001A88D4
		public static void OnRecv(NKMPacket_WARFARE_GAME_NEXT_ORDER_ACK cNKMPacket_WARFARE_GAME_NEXT_ORDER_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().InitWaitNextOrder();
				if (NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareGame() != null)
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareGame().SetPause(false);
				}
			}
			Debug.Log("NKMPacket_WARFARE_GAME_NEXT_ORDER_ACK - CurrentScenID : " + NKCScenManager.GetScenManager().GetNowScenID().ToString() + ", errorCode : " + cNKMPacket_WARFARE_GAME_NEXT_ORDER_ACK.errorCode.ToString());
			if (cNKMPacket_WARFARE_GAME_NEXT_ORDER_ACK.errorCode == NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_GET_NEXT_ORDER_AT_TURN_A || cNKMPacket_WARFARE_GAME_NEXT_ORDER_ACK.errorCode == NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_NEXT_ORDER_WARFARE_STATE_INGAME_PLAYING)
			{
				Debug.LogWarning("NKMPacket_WARFARE_GAME_NEXT_ORDER_ACK - errorCode : " + cNKMPacket_WARFARE_GAME_NEXT_ORDER_ACK.errorCode.ToString());
				NKMPopUpBox.CloseWaitBox();
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME && cNKMPacket_WARFARE_GAME_NEXT_ORDER_ACK.errorCode == NKM_ERROR_CODE.NEC_FAIL_WARFARE_NOT_ENOUGH_SUPPLEMENT && NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing())
			{
				NKMPopUpBox.CloseWaitBox();
				if (NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareGame() != null)
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareGame().SetPause(true);
				}
				NKCScenManager.GetScenManager().GetNKCRepeatOperaion().Init();
				NKCScenManager.GetScenManager().GetNKCRepeatOperaion().SetStopReason(NKCStringTable.GetString(cNKMPacket_WARFARE_GAME_NEXT_ORDER_ACK.errorCode.ToString(), false));
				NKCPopupRepeatOperation.Instance.OpenForResult(delegate
				{
					if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME && NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareGame() != null)
					{
						NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareGame().SetPause(false);
					}
				});
				return;
			}
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_WARFARE_GAME_NEXT_ORDER_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCUIGameOption.CheckInstanceAndClose();
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().OnRecv(cNKMPacket_WARFARE_GAME_NEXT_ORDER_ACK);
			}
		}

		// Token: 0x0600587A RID: 22650 RVA: 0x001AA8A4 File Offset: 0x001A8AA4
		public static void OnRecv(NKMPacket_WARFARE_GAME_GIVE_UP_ACK cNKMPacket_WARFARE_GAME_GIVE_UP_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_WARFARE_GAME_GIVE_UP_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				NKCScenManager.GetScenManager().WarfareGameData.warfareGameState = NKM_WARFARE_GAME_STATE.NWGS_STOP;
				myUserData.m_ArmyData.ResetDeckStateOf(NKM_DECK_STATE.DECK_STATE_WARFARE);
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().OnRecv(cNKMPacket_WARFARE_GAME_GIVE_UP_ACK);
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_OPERATION && NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_SHADOW_BATTLE)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_SHADOW_BATTLE().OnRecv(cNKMPacket_WARFARE_GAME_GIVE_UP_ACK);
			}
		}

		// Token: 0x0600587B RID: 22651 RVA: 0x001AA93C File Offset: 0x001A8B3C
		public static void OnRecv(NKMPacket_WARFARE_GAME_AUTO_ACK cNKMPacket_WARFARE_GAME_AUTO_ACK)
		{
			if (NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().WaitAutoPacekt)
			{
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME)
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareGame().SetPause(false);
				}
				if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_WARFARE_GAME_AUTO_ACK.errorCode, true, null, -2147483648))
				{
					return;
				}
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().WaitAutoPacekt = false;
			}
			else if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_WARFARE_GAME_AUTO_ACK.errorCode, false, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				myUserData.m_UserOption.m_bAutoWarfare = cNKMPacket_WARFARE_GAME_AUTO_ACK.isAuto;
				myUserData.m_UserOption.m_bAutoWarfareRepair = cNKMPacket_WARFARE_GAME_AUTO_ACK.isAutoRepair;
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME)
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().OnRecv(cNKMPacket_WARFARE_GAME_AUTO_ACK);
				}
			}
		}

		// Token: 0x0600587C RID: 22652 RVA: 0x001AAA08 File Offset: 0x001A8C08
		public static void OnRecv(NKMPacket_WARFARE_GAME_USE_SERVICE_ACK cNKMPacket_WARFARE_GAME_USE_SERVICE_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareGame().SetPause(false);
			}
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_WARFARE_GAME_USE_SERVICE_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				NKCWarfareManager.UseService(myUserData, NKCScenManager.GetScenManager().WarfareGameData, cNKMPacket_WARFARE_GAME_USE_SERVICE_ACK);
				myUserData.m_InventoryData.UpdateItemInfo(cNKMPacket_WARFARE_GAME_USE_SERVICE_ACK.costItemData);
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().OnRecv(cNKMPacket_WARFARE_GAME_USE_SERVICE_ACK);
			}
		}

		// Token: 0x0600587D RID: 22653 RVA: 0x001AAA9C File Offset: 0x001A8C9C
		public static void OnRecv(NKMPacket_WARFARE_EXPIRED_NOT cNKMPacket_WARFARE_EXPIRED_NOT)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				NKCScenManager.GetScenManager().WarfareGameData.warfareGameState = NKM_WARFARE_GAME_STATE.NWGS_STOP;
				myUserData.m_ArmyData.ResetDeckStateOf(NKM_DECK_STATE.DECK_STATE_WARFARE);
				NKCUtil.ProcessWFExpireTime();
			}
			NKM_SCEN_ID nowScenID = NKCScenManager.GetScenManager().GetNowScenID();
			if (nowScenID != NKM_SCEN_ID.NSI_HOME)
			{
				if (nowScenID != NKM_SCEN_ID.NSI_OPERATION)
				{
					if (nowScenID != NKM_SCEN_ID.NSI_WARFARE_GAME)
					{
						return;
					}
					NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().OnRecv(cNKMPacket_WARFARE_EXPIRED_NOT);
				}
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_HOME().OnRecv(cNKMPacket_WARFARE_EXPIRED_NOT);
		}

		// Token: 0x0600587E RID: 22654 RVA: 0x001AAB14 File Offset: 0x001A8D14
		public static void OnRecv(NKMPacket_WARFARE_FRIEND_LIST_ACK cNKMPacket_WARFARE_FRIEND_LIST_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_WARFARE_FRIEND_LIST_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCWarfareManager.SetSupportList(cNKMPacket_WARFARE_FRIEND_LIST_ACK.friends, cNKMPacket_WARFARE_FRIEND_LIST_ACK.guests);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().OnRecv(cNKMPacket_WARFARE_FRIEND_LIST_ACK);
			}
		}

		// Token: 0x0600587F RID: 22655 RVA: 0x001AAB68 File Offset: 0x001A8D68
		public static void OnRecv(NKMPacket_WARFARE_RECOVER_ACK cNKMPacket_WARFARE_RECOVER_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareGame().SetPause(false);
			}
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_WARFARE_RECOVER_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				myUserData.m_InventoryData.UpdateItemInfo(cNKMPacket_WARFARE_RECOVER_ACK.costItemDataList);
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().OnRecv(cNKMPacket_WARFARE_RECOVER_ACK);
			}
		}

		// Token: 0x06005880 RID: 22656 RVA: 0x001AABEC File Offset: 0x001A8DEC
		public static void OnRecv(NKMPacket_STAGE_UNLOCK_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(sPacket.stageId);
			NKMDungeonTempletBase dungeonTempletBase = nkmstageTempletV.DungeonTempletBase;
			string message;
			if (dungeonTempletBase != null && dungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_CUTSCENE)
			{
				message = string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_CUTSCENE_COMPLETE_EC_SIDESTORY", false), nkmstageTempletV.EpisodeTemplet.GetEpisodeTitle(), nkmstageTempletV.GetDungeonName());
			}
			else
			{
				message = string.Format(NKCStringTable.GetString("SI_DP_UNLOCK_COMPLETE_EC_SIDESTORY", false), nkmstageTempletV.EpisodeTemplet.GetEpisodeTitle(), nkmstageTempletV.ActId, nkmstageTempletV.m_StageUINum);
			}
			NKCUIManager.NKCPopupMessage.Open(new PopupMessage(message, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
			NKCScenManager.CurrentUserData().m_InventoryData.UpdateItemInfo(sPacket.costItemDataList);
			NKMEpisodeMgr.SetUnlockedStage(sPacket.stageId);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_OPERATION)
			{
				NKCScenManager.GetScenManager().Get_SCEN_OPERATION().OnRecv(sPacket);
			}
		}

		// Token: 0x06005881 RID: 22657 RVA: 0x001AACE4 File Offset: 0x001A8EE4
		public static void OnRecv(NKMPacket_FAVORITES_STAGE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMEpisodeMgr.SetFavoriteStage(sPacket.favoritesStage);
		}

		// Token: 0x06005882 RID: 22658 RVA: 0x001AAD06 File Offset: 0x001A8F06
		public static void OnRecv(NKMPacket_FAVORITES_STAGE_ADD_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMEpisodeMgr.SetFavoriteStage(sPacket.favoritesStage);
		}

		// Token: 0x06005883 RID: 22659 RVA: 0x001AAD28 File Offset: 0x001A8F28
		public static void OnRecv(NKMPacket_FAVORITE_STAGE_DELETE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMEpisodeMgr.SetFavoriteStage(sPacket.favoritesStage);
		}

		// Token: 0x06005884 RID: 22660 RVA: 0x001AAD4C File Offset: 0x001A8F4C
		public static void OnRecv(NKMPacket_SET_EMBLEM_ACK cNKMPacket_SET_EMBLEM_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_SET_EMBLEM_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserProfileData userProfileData = NKCScenManager.CurrentUserData().UserProfileData;
			if (userProfileData != null && cNKMPacket_SET_EMBLEM_ACK.index >= 0 && (int)cNKMPacket_SET_EMBLEM_ACK.index < userProfileData.emblems.Count)
			{
				int index = (int)cNKMPacket_SET_EMBLEM_ACK.index;
				userProfileData.emblems[index].id = cNKMPacket_SET_EMBLEM_ACK.itemId;
				userProfileData.emblems[index].count = cNKMPacket_SET_EMBLEM_ACK.count;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_FRIEND)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().OnRecv(cNKMPacket_SET_EMBLEM_ACK);
			}
		}

		// Token: 0x06005885 RID: 22661 RVA: 0x001AADEB File Offset: 0x001A8FEB
		public static void OnRecv(NKMPacket_MY_USER_PROFILE_INFO_ACK cNKMPacket_MY_USER_PROFILE_INFO_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_MY_USER_PROFILE_INFO_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.CurrentUserData().SetMyUserProfileInfo(cNKMPacket_MY_USER_PROFILE_INFO_ACK.userProfileData);
			if (NKCUIPopupOfficeInteract.IsInstanceOpen)
			{
				NKCUIPopupOfficeInteract.Instance.UpdateMyBizCard();
			}
		}

		// Token: 0x06005886 RID: 22662 RVA: 0x001AAE24 File Offset: 0x001A9024
		public static void OnRecv(NKMPacket_FRIEND_LIST_ACK cNKMPacket_FRIEND_LIST_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_FRIEND_LIST_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (cNKMPacket_FRIEND_LIST_ACK.friendListType == NKM_FRIEND_LIST_TYPE.FRIEND)
			{
				NKCFriendManager.SetFriendList(cNKMPacket_FRIEND_LIST_ACK.list);
			}
			else if (cNKMPacket_FRIEND_LIST_ACK.friendListType == NKM_FRIEND_LIST_TYPE.BLOCKER)
			{
				NKCFriendManager.SetBlockList(cNKMPacket_FRIEND_LIST_ACK.list);
			}
			else if (cNKMPacket_FRIEND_LIST_ACK.friendListType == NKM_FRIEND_LIST_TYPE.RECEIVE_REQUEST)
			{
				NKCFriendManager.SetReceivedREQList(cNKMPacket_FRIEND_LIST_ACK.list);
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_FRIEND)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().OnRecv(cNKMPacket_FRIEND_LIST_ACK);
			}
		}

		// Token: 0x06005887 RID: 22663 RVA: 0x001AAEA4 File Offset: 0x001A90A4
		public static void OnRecv(NKMPacket_FRIEND_DELETE_NOT not)
		{
			NKCFriendManager.DeleteFriend(not.friendCode);
			NKCChatManager.RemoveFriendByFriendCode(not.friendCode);
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_HOME)
			{
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_FRIEND)
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().OnRecv(not);
				}
				return;
			}
			NKC_SCEN_HOME scen_HOME = NKCScenManager.GetScenManager().Get_SCEN_HOME();
			if (scen_HOME == null)
			{
				return;
			}
			scen_HOME.UpdateRightSide3DButton(NKCUILobbyV2.eUIMenu.Friends);
		}

		// Token: 0x06005888 RID: 22664 RVA: 0x001AAF0C File Offset: 0x001A910C
		public static void OnRecv(NKMPacket_FRIEND_CANCEL_REQUEST_NOT not)
		{
			NKCFriendManager.RemoveReceivedREQ(not.friendCode);
			NKC_SCEN_HOME scen_HOME = NKCScenManager.GetScenManager().Get_SCEN_HOME();
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_HOME && scen_HOME != null)
			{
				scen_HOME.UpdateRightSide3DButton(NKCUILobbyV2.eUIMenu.Friends);
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_FRIEND)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().OnRecv(not);
			}
		}

		// Token: 0x06005889 RID: 22665 RVA: 0x001AAF68 File Offset: 0x001A9168
		public static void OnRecv(NKMPacket_FRIEND_ACCEPT_NOT cNKMPacket_FRIEND_ACCEPT_NOT)
		{
			if (cNKMPacket_FRIEND_ACCEPT_NOT.isAllow)
			{
				NKCFriendManager.AddFriend(cNKMPacket_FRIEND_ACCEPT_NOT.friendProfileData);
				NKCChatManager.AddFriend(cNKMPacket_FRIEND_ACCEPT_NOT.friendProfileData.commonProfile);
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_FRIEND)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().OnRecv(cNKMPacket_FRIEND_ACCEPT_NOT);
			}
		}

		// Token: 0x0600588A RID: 22666 RVA: 0x001AAFB8 File Offset: 0x001A91B8
		public static void OnRecv(NKMPacket_FRIEND_SEARCH_ACK cNKMPacket_FRIEND_SEARCH_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_FRIEND_SEARCH_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_FRIEND)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().OnRecv(cNKMPacket_FRIEND_SEARCH_ACK);
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GUILD_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_LOBBY().OnRecv(cNKMPacket_FRIEND_SEARCH_ACK.list);
			}
		}

		// Token: 0x0600588B RID: 22667 RVA: 0x001AB01C File Offset: 0x001A921C
		public static void OnRecv(NKMPacket_FRIEND_RECOMMEND_ACK cNKMPacket_FRIEND_RECOMMEND_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_FRIEND_RECOMMEND_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_FRIEND)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().OnRecv(cNKMPacket_FRIEND_RECOMMEND_ACK);
			}
		}

		// Token: 0x0600588C RID: 22668 RVA: 0x001AB054 File Offset: 0x001A9254
		public static void OnRecv(NKMPacket_FRIEND_REQUEST_ACK cNKMPacket_FRIEND_ADD_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_FRIEND_ADD_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPopupFriendInfo.CheckInstanceAndClose();
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_FRIEND_ADD_REQ_COMPLETE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
			}
			else if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_OFFICE)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_FRIEND_ADD_REQ_COMPLETE, delegate()
				{
					NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString("SI_OFFICE_BIZ_CARD_SENT", false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				}, "");
			}
			else
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_FRIEND_ADD_REQ_COMPLETE, null, "");
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_FRIEND)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().OnRecv(cNKMPacket_FRIEND_ADD_ACK);
			}
		}

		// Token: 0x0600588D RID: 22669 RVA: 0x001AB114 File Offset: 0x001A9314
		public static void OnRecv(NKMPacket_FRIEND_REQUEST_NOT cNKMPacket_FRIEND_REQUEST_NOT)
		{
			NKCFriendManager.AddReceivedREQ(cNKMPacket_FRIEND_REQUEST_NOT.friendProfileData.commonProfile.friendCode);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_HOME)
			{
				NKC_SCEN_HOME scen_HOME = NKCScenManager.GetScenManager().Get_SCEN_HOME();
				if (scen_HOME != null)
				{
					scen_HOME.UpdateRightSide3DButton(NKCUILobbyV2.eUIMenu.Friends);
				}
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_FRIEND)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().OnRecv(cNKMPacket_FRIEND_REQUEST_NOT);
			}
		}

		// Token: 0x0600588E RID: 22670 RVA: 0x001AB178 File Offset: 0x001A9378
		public static void OnRecv(NKMPacket_FRIEND_PROFILE_MODIFY_MAIN_CHAR_ACK cNKMPacket_FRIEND_PROFILE_MODIFY_MAIN_CHAR_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_FRIEND_PROFILE_MODIFY_MAIN_CHAR_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().CloseImageChange();
			NKMUserProfileData userProfileData = NKCScenManager.CurrentUserData().UserProfileData;
			if (userProfileData != null)
			{
				userProfileData.commonProfile.mainUnitId = cNKMPacket_FRIEND_PROFILE_MODIFY_MAIN_CHAR_ACK.mainCharId;
				userProfileData.commonProfile.mainUnitSkinId = cNKMPacket_FRIEND_PROFILE_MODIFY_MAIN_CHAR_ACK.mainCharSkinId;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_FRIEND)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().OnRecv(cNKMPacket_FRIEND_PROFILE_MODIFY_MAIN_CHAR_ACK);
			}
		}

		// Token: 0x0600588F RID: 22671 RVA: 0x001AB1F8 File Offset: 0x001A93F8
		public static void OnRecv(NKMPacket_FRIEND_PROFILE_MODIFY_DECK_ACK cNKMPacket_FRIEND_PROFILE_MODIFY_DECK_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_FRIEND_PROFILE_MODIFY_DECK_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCUIDeckViewer.CheckInstanceAndClose();
			NKMUserProfileData userProfileData = NKCScenManager.CurrentUserData().UserProfileData;
			if (userProfileData != null)
			{
				userProfileData.profileDeck = cNKMPacket_FRIEND_PROFILE_MODIFY_DECK_ACK.deckData;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_FRIEND)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().OnRecv(cNKMPacket_FRIEND_PROFILE_MODIFY_DECK_ACK);
			}
		}

		// Token: 0x06005890 RID: 22672 RVA: 0x001AB258 File Offset: 0x001A9458
		public static void OnRecv(NKMPacket_FRIEND_PROFILE_MODIFY_INTRO_ACK cNKMPacket_FRIEND_PROFILE_MODIFY_INTRO_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_FRIEND_PROFILE_MODIFY_INTRO_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			cNKMPacket_FRIEND_PROFILE_MODIFY_INTRO_ACK.intro = NKCFilterManager.CheckBadChat(cNKMPacket_FRIEND_PROFILE_MODIFY_INTRO_ACK.intro);
			NKMUserProfileData userProfileData = NKCScenManager.CurrentUserData().UserProfileData;
			if (userProfileData != null)
			{
				userProfileData.friendIntro = cNKMPacket_FRIEND_PROFILE_MODIFY_INTRO_ACK.intro;
			}
			NKCUIUserInfo.SetComment(cNKMPacket_FRIEND_PROFILE_MODIFY_INTRO_ACK.intro);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_FRIEND)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().OnRecv(cNKMPacket_FRIEND_PROFILE_MODIFY_INTRO_ACK);
			}
		}

		// Token: 0x06005891 RID: 22673 RVA: 0x001AB2CE File Offset: 0x001A94CE
		public static void OnRecv(NKMPacket_GREETING_MESSAGE_ACK cNKMPacket_GREETING_MESSAGE_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GREETING_MESSAGE_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCUIUserInfo.SetComment(cNKMPacket_GREETING_MESSAGE_ACK.message);
		}

		// Token: 0x06005892 RID: 22674 RVA: 0x001AB2F0 File Offset: 0x001A94F0
		public static void OnRecv(NKMPacket_FRIEND_ACCEPT_ACK cNKMPacket_FRIEND_ACCEPT_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_FRIEND_ACCEPT_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPopupFriendInfo.CheckInstanceAndClose();
			if (cNKMPacket_FRIEND_ACCEPT_ACK.isAllow)
			{
				Log.Info("<color=#ffffff>AddFriend</color>", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCPacketHandlersLobby.cs", 3598);
				NKCFriendManager.AddFriend(cNKMPacket_FRIEND_ACCEPT_ACK.friendCode);
			}
			NKCFriendManager.RemoveReceivedREQ(cNKMPacket_FRIEND_ACCEPT_ACK.friendCode);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_FRIEND)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().OnRecv(cNKMPacket_FRIEND_ACCEPT_ACK);
			}
		}

		// Token: 0x06005893 RID: 22675 RVA: 0x001AB367 File Offset: 0x001A9567
		public static void OnRecv(NKMPacket_FRIEND_CANCEL_REQUEST_ACK cNKMPacket_FRIEND_ADD_CANCEL_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_FRIEND_ADD_CANCEL_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPopupFriendInfo.CheckInstanceAndClose();
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_FRIEND)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().OnRecv(cNKMPacket_FRIEND_ADD_CANCEL_ACK);
			}
		}

		// Token: 0x06005894 RID: 22676 RVA: 0x001AB3A4 File Offset: 0x001A95A4
		public static void OnRecv(NKMPacket_FRIEND_BLOCK_ACK cNKMPacket_FRIEND_BLOCK_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_FRIEND_BLOCK_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPopupFriendInfo.CheckInstanceAndClose();
			if (cNKMPacket_FRIEND_BLOCK_ACK.isCancel)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_FRIEND_BLOCK_CANCEL_NOTICE, null, "");
				NKCFriendManager.RemoveBlockUser(cNKMPacket_FRIEND_BLOCK_ACK.friendCode);
			}
			else if (NKCFriendManager.IsFriend(cNKMPacket_FRIEND_BLOCK_ACK.friendCode))
			{
				NKCFriendManager.DeleteFriend(cNKMPacket_FRIEND_BLOCK_ACK.friendCode);
				NKCFriendManager.AddBlockUser(cNKMPacket_FRIEND_BLOCK_ACK.friendCode);
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_FRIEND)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().OnRecv(cNKMPacket_FRIEND_BLOCK_ACK);
			}
			if (NKCPopupPrivateChatLobby.IsInstanceOpen)
			{
				NKCPopupPrivateChatLobby.Instance.OnRecvFriendBlock(cNKMPacket_FRIEND_BLOCK_ACK.friendCode);
			}
		}

		// Token: 0x06005895 RID: 22677 RVA: 0x001AB450 File Offset: 0x001A9650
		public static void OnRecv(NKMPacket_FRIEND_DELETE_ACK cNKMPacket_FRIEND_DEL_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_FRIEND_DEL_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPopupFriendInfo.CheckInstanceAndClose();
			NKCFriendManager.DeleteFriend(cNKMPacket_FRIEND_DEL_ACK.friendCode);
			NKCChatManager.RemoveFriendByFriendCode(cNKMPacket_FRIEND_DEL_ACK.friendCode);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_FRIEND)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().OnRecv(cNKMPacket_FRIEND_DEL_ACK);
			}
		}

		// Token: 0x06005896 RID: 22678 RVA: 0x001AB4AC File Offset: 0x001A96AC
		public static void OnRecv(NKMPacket_USER_PROFILE_CHANGE_FRAME_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserProfileData userProfileData = NKCScenManager.CurrentUserData().UserProfileData;
			if (userProfileData != null)
			{
				userProfileData.commonProfile.frameId = sPacket.selfiFrameId;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_FRIEND)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_FRIEND().OnRecv(sPacket);
			}
		}

		// Token: 0x06005897 RID: 22679 RVA: 0x001AB50C File Offset: 0x001A970C
		public static void OnRecv(NKMPacket_RANDOM_MISSION_REFRESH_NOT cNKMPacket_RANDOM_MISSION_REFRESH_NOT)
		{
			if (NKCScenManager.CurrentUserData() == null)
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			nkmuserData.m_MissionData.RemoveAllRandomMissionInTab(cNKMPacket_RANDOM_MISSION_REFRESH_NOT.tabId);
			foreach (NKMMissionData missionData in cNKMPacket_RANDOM_MISSION_REFRESH_NOT.missionDataList)
			{
				nkmuserData.m_MissionData.AddMission(missionData);
			}
			nkmuserData.m_MissionData.OnRandomMissionRefresh();
		}

		// Token: 0x06005898 RID: 22680 RVA: 0x001AB594 File Offset: 0x001A9794
		public static void OnRecv(NKMPacket_RANDOM_MISSION_CHANGE_ACK cNKMPacket_RANDOM_MISSION_CHANGE_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_RANDOM_MISSION_CHANGE_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				nkmuserData.m_InventoryData.UpdateItemInfo(cNKMPacket_RANDOM_MISSION_CHANGE_ACK.costItemData);
				nkmuserData.m_MissionData.RemoveMission(cNKMPacket_RANDOM_MISSION_CHANGE_ACK.beforeGroupId);
				nkmuserData.m_MissionData.AddMission(cNKMPacket_RANDOM_MISSION_CHANGE_ACK.afterMissionData);
				NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(cNKMPacket_RANDOM_MISSION_CHANGE_ACK.afterMissionData.mission_id);
				nkmuserData.m_MissionData.SetRandomMissionRefreshCount(missionTemplet.m_MissionTabId, cNKMPacket_RANDOM_MISSION_CHANGE_ACK.remainRefreshCount);
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GUILD_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_LOBBY().RefreshUI();
			}
		}

		// Token: 0x06005899 RID: 22681 RVA: 0x001AB637 File Offset: 0x001A9837
		public static void OnRecv(NKMPacket_MISSION_GIVE_ITEM_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.UpdateItemInfo(sPacket.costItems);
		}

		// Token: 0x0600589A RID: 22682 RVA: 0x001AB668 File Offset: 0x001A9868
		public static void OnRecv(NKMPacket_MISSION_COMPLETE_ALL_ACK cNKMPacket_MISSION_COMPLETE_ALL_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_MISSION_COMPLETE_ALL_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.GetReward(cNKMPacket_MISSION_COMPLETE_ALL_ACK.rewardDate);
			for (int i = 0; i < cNKMPacket_MISSION_COMPLETE_ALL_ACK.missionIDList.Count; i++)
			{
				myUserData.m_MissionData.SetCompleteMissionData(cNKMPacket_MISSION_COMPLETE_ALL_ACK.missionIDList[i]);
			}
			NKMMissionManager.SetHaveClearedMission(myUserData.m_MissionData.CheckCompletableMission(myUserData, true), true);
			NKMMissionManager.SetHaveClearedMissionGuide(myUserData.m_MissionData.CheckCompletableGuideMission(myUserData, true), true);
			bool flag = false;
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_HOME && NKCUIMissionAchievement.IsInstanceOpen)
			{
				flag = true;
				NKCUIMissionAchievement.Instance.OnRecv(cNKMPacket_MISSION_COMPLETE_ALL_ACK);
			}
			else if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_HOME && NKCUIMissionGuide.IsInstanceOpen)
			{
				flag = true;
				NKCUIMissionGuide.Instance.OnRecv(cNKMPacket_MISSION_COMPLETE_ALL_ACK);
			}
			else if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GUILD_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_LOBBY().RefreshUI();
			}
			if (NKCUIEvent.IsInstanceOpen)
			{
				NKCUIEvent.Instance.RefreshUI(0);
			}
			if (NKCUIEventPass.IsInstanceOpen)
			{
				flag = true;
				if (cNKMPacket_MISSION_COMPLETE_ALL_ACK.additionalReward != null)
				{
					NKCUIEventPass.Instance.RefreshPassAdditionalExpRelatedInfo(cNKMPacket_MISSION_COMPLETE_ALL_ACK.additionalReward.eventPassExpDelta);
				}
				NKCPopupMessageToastSimple.Instance.Open(cNKMPacket_MISSION_COMPLETE_ALL_ACK.rewardDate, cNKMPacket_MISSION_COMPLETE_ALL_ACK.additionalReward, delegate
				{
					NKCUIEventPass.Instance.PlayExpDoTween();
				});
			}
			NKMEventCollectionIndexTemplet nkmeventCollectionIndexTemplet = null;
			foreach (NKCUIModuleHome nkcuimoduleHome in NKCUIManager.GetOpenedUIsByType<NKCUIModuleHome>())
			{
				if (nkcuimoduleHome.IsOpen)
				{
					nkcuimoduleHome.UpdateUI();
					nkmeventCollectionIndexTemplet = nkcuimoduleHome.EventCollectionIndexTemplet;
					if (nkmeventCollectionIndexTemplet != null && !string.IsNullOrEmpty(nkmeventCollectionIndexTemplet.EventMergeResultPrefabID))
					{
						flag = true;
					}
				}
			}
			if (nkmeventCollectionIndexTemplet != null && flag)
			{
				NKCUIPopupModuleResult popupResultUIData = NKCUIPopupModuleResult.MakeInstance(nkmeventCollectionIndexTemplet.EventResultPrefabID, nkmeventCollectionIndexTemplet.EventResultPrefabID);
				if (null != popupResultUIData)
				{
					popupResultUIData.Init();
					popupResultUIData.Open(cNKMPacket_MISSION_COMPLETE_ALL_ACK.rewardDate, cNKMPacket_MISSION_COMPLETE_ALL_ACK.additionalReward, delegate()
					{
						if (popupResultUIData.IsOpen)
						{
							popupResultUIData.Close();
							NKCUIPopupModuleResult.CheckInstanceAndClose();
							popupResultUIData = null;
						}
					});
				}
			}
			if (NKCPopupEventPayReward.IsInstanceOpen)
			{
				NKCPopupEventPayReward.Instance.Refresh();
			}
			if (!flag)
			{
				NKCUIResult.Instance.OpenRewardGain(myUserData.m_ArmyData, cNKMPacket_MISSION_COMPLETE_ALL_ACK.rewardDate, cNKMPacket_MISSION_COMPLETE_ALL_ACK.additionalReward, NKCUtilString.GET_STRING_RESULT_MISSION, "", null);
			}
		}

		// Token: 0x0600589B RID: 22683 RVA: 0x001AB8D4 File Offset: 0x001A9AD4
		public static void OnRecv(NKMPacket_MISSION_COMPLETE_ACK cNKMPacket_MISSION_COMPLETE_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_MISSION_COMPLETE_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.GetReward(cNKMPacket_MISSION_COMPLETE_ACK.rewardDate);
			myUserData.m_MissionData.SetCompleteMissionData(cNKMPacket_MISSION_COMPLETE_ACK.missionID);
			NKMMissionManager.SetHaveClearedMission(myUserData.m_MissionData.CheckCompletableMission(myUserData, true), true);
			NKMMissionManager.SetHaveClearedMissionGuide(myUserData.m_MissionData.CheckCompletableGuideMission(myUserData, true), true);
			NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(cNKMPacket_MISSION_COMPLETE_ACK.missionID);
			if (missionTemplet == null)
			{
				return;
			}
			if (missionTemplet.m_TrackingEvent != string.Empty)
			{
				NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.MissionClear, 0, missionTemplet.m_TrackingEvent);
			}
			if (missionTemplet.m_AdjustEvent != string.Empty)
			{
				NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.MissionClearAdjust, 0, missionTemplet.m_AdjustEvent);
			}
			NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(missionTemplet.m_MissionTabId);
			if (missionTabTemplet != null)
			{
				if (missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.TUTORIAL)
				{
					Debug.Log(string.Format("Tutorial mission {0} Completed!", cNKMPacket_MISSION_COMPLETE_ACK.missionID));
					NKCGameEventManager.TutorialCompletePacketSent(cNKMPacket_MISSION_COMPLETE_ACK.missionID);
					return;
				}
				if (missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.BINGO)
				{
					if (NKCPopupEventMission.IsInstanceOpen)
					{
						NKCPopupEventMission.Instance.OnRecv(cNKMPacket_MISSION_COMPLETE_ACK);
					}
					return;
				}
			}
			bool flag = false;
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_HOME && NKCUIMissionAchievement.IsInstanceOpen)
			{
				flag = true;
				NKCUIMissionAchievement.Instance.OnRecv(cNKMPacket_MISSION_COMPLETE_ACK);
			}
			else if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_HOME && NKCUIMissionGuide.IsInstanceOpen)
			{
				flag = true;
				NKCUIMissionGuide.Instance.OnRecv(cNKMPacket_MISSION_COMPLETE_ACK);
			}
			else if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GUILD_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_LOBBY().RefreshUI();
			}
			if (NKCPopupHamburgerMenu.IsInstanceOpen)
			{
				if (!flag)
				{
					flag = true;
					NKCPopupHamburgerMenu.instance.OnRecv(cNKMPacket_MISSION_COMPLETE_ACK);
				}
				NKCPopupHamburgerMenu.instance.Refresh();
			}
			if (NKCUIEvent.IsInstanceOpen)
			{
				NKCUIEvent.Instance.RefreshUI(0);
			}
			if (NKCUIEventPass.IsInstanceOpen)
			{
				flag = true;
				NKCUIEventPass.Instance.RefreshPassAdditionalExpRelatedInfo(cNKMPacket_MISSION_COMPLETE_ACK.additionalReward.eventPassExpDelta);
				NKCPopupMessageToastSimple.Instance.Open(cNKMPacket_MISSION_COMPLETE_ACK.rewardDate, cNKMPacket_MISSION_COMPLETE_ACK.additionalReward, delegate
				{
					NKCUIEventPass.Instance.PlayExpDoTween();
				});
			}
			NKMEventCollectionIndexTemplet nkmeventCollectionIndexTemplet = null;
			foreach (NKCUIModuleHome nkcuimoduleHome in NKCUIManager.GetOpenedUIsByType<NKCUIModuleHome>())
			{
				if (nkcuimoduleHome.IsOpen)
				{
					nkcuimoduleHome.UpdateUI();
					nkmeventCollectionIndexTemplet = nkcuimoduleHome.EventCollectionIndexTemplet;
					if (nkmeventCollectionIndexTemplet != null && !string.IsNullOrEmpty(nkmeventCollectionIndexTemplet.EventMergeResultPrefabID))
					{
						flag = true;
					}
				}
			}
			if (nkmeventCollectionIndexTemplet != null && flag)
			{
				NKCUIPopupModuleResult popupResult = NKCUIPopupModuleResult.MakeInstance(nkmeventCollectionIndexTemplet.EventResultPrefabID, nkmeventCollectionIndexTemplet.EventResultPrefabID);
				if (null != popupResult)
				{
					popupResult.Init();
					popupResult.Open(cNKMPacket_MISSION_COMPLETE_ACK.rewardDate, cNKMPacket_MISSION_COMPLETE_ACK.additionalReward, delegate()
					{
						if (popupResult.IsOpen)
						{
							popupResult.Close();
							NKCUIPopupModuleResult.CheckInstanceAndClose();
							popupResult = null;
						}
					});
				}
			}
			if (!flag)
			{
				NKCUIResult.Instance.OpenRewardGain(myUserData.m_ArmyData, cNKMPacket_MISSION_COMPLETE_ACK.rewardDate, cNKMPacket_MISSION_COMPLETE_ACK.additionalReward, NKCUtilString.GET_STRING_RESULT_MISSION, "", null);
			}
		}

		// Token: 0x0600589C RID: 22684 RVA: 0x001ABBE8 File Offset: 0x001A9DE8
		public static void OnRecv(NKMPacket_MISSION_UPDATE_NOT cNKMPacket_MISSION_UPDATE_NOT)
		{
			if (cNKMPacket_MISSION_UPDATE_NOT.missionDataList == null)
			{
				return;
			}
			foreach (NKMMissionData nkmmissionData in cNKMPacket_MISSION_UPDATE_NOT.missionDataList)
			{
				if (nkmmissionData != null)
				{
					NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(nkmmissionData.mission_id);
					if (missionTemplet != null)
					{
						NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(missionTemplet.m_MissionTabId);
						if (missionTabTemplet != null && missionTabTemplet.m_MissionType != NKM_MISSION_TYPE.TUTORIAL)
						{
							NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
							bool flag = false;
							bool flag2 = false;
							if (myUserData != null)
							{
								if (NKMMissionManager.CanComplete(missionTemplet, myUserData, nkmmissionData) == NKM_ERROR_CODE.NEC_OK)
								{
									flag = true;
									if (NKMMissionManager.IsCumulativeCondition(missionTemplet.m_MissionCond.mission_cond))
									{
										bool flag3 = false;
										NKMMissionData missionDataByMissionId = myUserData.m_MissionData.GetMissionDataByMissionId(nkmmissionData.mission_id);
										if (missionDataByMissionId != null && !NKMMissionManager.CheckCanReset(missionTemplet.m_ResetInterval, missionDataByMissionId))
										{
											flag3 = (NKMMissionManager.CanComplete(missionTemplet, myUserData, missionDataByMissionId) == NKM_ERROR_CODE.NEC_OK);
										}
										if (!flag3)
										{
											flag2 = true;
										}
									}
									else
									{
										flag2 = true;
									}
								}
								myUserData.m_MissionData.AddOrUpdateMission(nkmmissionData);
							}
							if (flag)
							{
								if (flag2)
								{
									NKCPopupMessageManager.AddPopupMessage(string.Format(NKCUtilString.GET_STRING_MISSION_COMPLETE_ONE_PARAM, missionTemplet.GetTitle()), NKCPopupMessage.eMessagePosition.Top, true, false, 0f, true);
									NKCSoundManager.PlaySound("FX_UI_DECK_SLOT_OPEN", 1f, 0f, 0f, false, 0f, false, 0f);
								}
								if (missionTabTemplet.EnableByTag)
								{
									if (missionTabTemplet.m_MissionType != NKM_MISSION_TYPE.COMBINE_GUIDE_MISSION)
									{
										NKMMissionManager.SetHaveClearedMission(true, missionTabTemplet.m_Visible);
									}
									if (missionTabTemplet.m_MissionType == NKM_MISSION_TYPE.COMBINE_GUIDE_MISSION)
									{
										NKMMissionManager.SetHaveClearedMissionGuide(true, missionTabTemplet.m_Visible);
									}
								}
							}
							if (NKCUIMissionAchievement.IsInstanceOpen)
							{
								if (NKCUIMissionAchievement.Instance.gameObject.activeInHierarchy)
								{
									NKCUIMissionAchievement.Instance.SetUIByCurrTab();
								}
								else
								{
									NKCUIMissionAchievement.Instance.ReservedRefresh(cNKMPacket_MISSION_UPDATE_NOT);
								}
								NKCUIMissionAchievement.Instance.SetCompletableMissionAlarm();
							}
							if (NKCUIMissionGuide.IsInstanceOpen)
							{
								if (NKCUIMissionGuide.Instance.gameObject.activeInHierarchy)
								{
									NKCUIMissionGuide.Instance.SetUIByCurrTab();
								}
								else
								{
									NKCUIMissionGuide.Instance.ReservedRefresh(cNKMPacket_MISSION_UPDATE_NOT);
								}
								NKCUIMissionGuide.Instance.SetCompletableMissionAlarm();
							}
						}
					}
				}
			}
			if (cNKMPacket_MISSION_UPDATE_NOT.missionDataList.Count > 0)
			{
				if (NKCUIEvent.IsInstanceOpen)
				{
					NKCUIEvent.Instance.RefreshUI(0);
				}
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GUILD_LOBBY)
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_LOBBY().RefreshUI();
				}
				if (NKCUIEventPass.IsEventTime(false))
				{
					NKCUIEventPass.RefreshMissionState(cNKMPacket_MISSION_UPDATE_NOT.missionDataList);
				}
			}
		}

		// Token: 0x0600589D RID: 22685 RVA: 0x001ABE50 File Offset: 0x001AA050
		public static void OnRecv(NKMPacket_SURVEY_UPSERT_NOT cNKMPacket_SURVEY_UPSERT_NOT)
		{
			if (cNKMPacket_SURVEY_UPSERT_NOT.surveyInfos != null)
			{
				for (int i = 0; i < cNKMPacket_SURVEY_UPSERT_NOT.surveyInfos.Count; i++)
				{
					NKCScenManager.GetScenManager().GetNKCSurveyMgr().UpdaterOrAdd(cNKMPacket_SURVEY_UPSERT_NOT.surveyInfos[i]);
				}
			}
		}

		// Token: 0x0600589E RID: 22686 RVA: 0x001ABE96 File Offset: 0x001AA096
		public static void OnRecv(NKMPacket_SURVEY_RESET_NOT cNKMPacket_SURVEY_RESET_NOT)
		{
			NKCScenManager.GetScenManager().GetNKCSurveyMgr().Clear();
		}

		// Token: 0x0600589F RID: 22687 RVA: 0x001ABEA7 File Offset: 0x001AA0A7
		public static void OnRecv(NKMPacket_SURVEY_COMPLETE_ACK cNKMPacket_SURVEY_COMPLETE_ACK)
		{
			NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_SURVEY_COMPLETE_ACK.errorCode, true, null, int.MinValue);
		}

		// Token: 0x060058A0 RID: 22688 RVA: 0x001ABEBC File Offset: 0x001AA0BC
		public static void OnRecv(NKMPacket_WORLDMAP_EVENT_CANCEL_ACK cNKMPacket_WORLDMAP_EVENT_CANCEL_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_WORLDMAP_EVENT_CANCEL_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			NKMWorldMapCityData cityData = nkmuserData.m_WorldmapData.GetCityData(cNKMPacket_WORLDMAP_EVENT_CANCEL_ACK.cityID);
			if (cityData != null)
			{
				NKMWorldMapEventTemplet nkmworldMapEventTemplet = NKMWorldMapEventTemplet.Find(cityData.worldMapEventGroup.worldmapEventID);
				if (nkmworldMapEventTemplet != null)
				{
					if (nkmworldMapEventTemplet.eventType == NKM_WORLDMAP_EVENT_TYPE.WET_DIVE)
					{
						NKMUserData nkmuserData2 = NKCScenManager.CurrentUserData();
						if (nkmuserData2 != null && nkmuserData2.m_DiveGameData != null && nkmuserData2.m_DiveGameData.DiveUid == cityData.worldMapEventGroup.eventUid)
						{
							nkmuserData2.ClearDiveGameData();
						}
					}
					else if (nkmworldMapEventTemplet.eventType == NKM_WORLDMAP_EVENT_TYPE.WET_RAID)
					{
						NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Remove(cityData.worldMapEventGroup.eventUid);
					}
				}
				cityData.worldMapEventGroup.Clear();
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WORLDMAP)
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().OnRecv(cNKMPacket_WORLDMAP_EVENT_CANCEL_ACK);
				}
			}
		}

		// Token: 0x060058A1 RID: 22689 RVA: 0x001ABF9C File Offset: 0x001AA19C
		public static void OnRecv(NKMPacket_WORLDMAP_SET_CITY_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItemData);
			if (myUserData.m_WorldmapData.IsCityUnlocked(sPacket.worldMapCityData.cityID))
			{
				Debug.LogError("FATAL : City already opened, Client-server data sync off");
				return;
			}
			myUserData.m_WorldmapData.worldMapCityDataMap.Add(sPacket.worldMapCityData.cityID, sPacket.worldMapCityData);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WORLDMAP)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().OnWorldManCitySet(sPacket.worldMapCityData.cityID, sPacket.worldMapCityData);
			}
		}

		// Token: 0x060058A2 RID: 22690 RVA: 0x001AC04C File Offset: 0x001AA24C
		public static void OnRecv(NKMPacket_WORLDMAP_SET_LEADER_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMWorldMapCityData cityData = myUserData.m_WorldmapData.GetCityData(sPacket.cityID);
			if (cityData == null)
			{
				Debug.LogError("FATAL : City/Area Does not exist, Cilent-Server Templet info sync off");
				return;
			}
			foreach (KeyValuePair<int, NKMWorldMapCityData> keyValuePair in myUserData.m_WorldmapData.worldMapCityDataMap)
			{
				if (keyValuePair.Value.leaderUnitUID != 0L && keyValuePair.Value.leaderUnitUID == sPacket.leaderUID)
				{
					keyValuePair.Value.leaderUnitUID = 0L;
					break;
				}
			}
			cityData.leaderUnitUID = sPacket.leaderUID;
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WORLDMAP)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().OnCityLeaderChanged(cityData);
			}
			NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(cityData.leaderUnitUID);
			if (unitFromUID != null)
			{
				NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_WORLDMAP_CHECK, unitFromUID, false, false);
			}
		}

		// Token: 0x060058A3 RID: 22691 RVA: 0x001AC160 File Offset: 0x001AA360
		public static void OnRecv(NKMPacket_WORLDMAP_CITY_MISSION_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMWorldMapCityData cityData = NKCScenManager.GetScenManager().GetMyUserData().m_WorldmapData.GetCityData(sPacket.cityID);
			if (cityData == null)
			{
				Debug.LogError("FATAL : City/Area Does not exist, Cilent-Server Templet info sync off");
				return;
			}
			cityData.worldMapMission.completeTime = sPacket.completeTime;
			cityData.worldMapMission.currentMissionID = sPacket.missionID;
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WORLDMAP)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().CityDataUpdated(cityData);
			}
			long leaderUnitUID = cityData.leaderUnitUID;
			NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(leaderUnitUID);
			if (unitFromUID != null)
			{
				NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_WORLDMAP_MISSION_START, unitFromUID, false, false);
			}
		}

		// Token: 0x060058A4 RID: 22692 RVA: 0x001AC210 File Offset: 0x001AA410
		public static void OnRecv(NKMPacket_WORLDMAP_CITY_MISSION_CANCEL_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMWorldMapCityData cityData = NKCScenManager.GetScenManager().GetMyUserData().m_WorldmapData.GetCityData(sPacket.cityID);
			if (cityData == null)
			{
				Debug.LogError("FATAL : City/Area Does not exist, Cilent-Server Templet info sync off");
				return;
			}
			cityData.worldMapMission.completeTime = 0L;
			cityData.worldMapMission.currentMissionID = 0;
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WORLDMAP)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().CityDataUpdated(cityData);
			}
			NKCPublisherModule.Push.UpdateLocalPush(NKC_GAME_OPTION_ALARM_GROUP.WORLD_MAP_MISSION_COMPLETE, true);
		}

		// Token: 0x060058A5 RID: 22693 RVA: 0x001AC2A0 File Offset: 0x001AA4A0
		public static void OnRecv(NKMPacket_WORLDMAP_MISSION_REFRESH_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMWorldMapCityData cityData = myUserData.m_WorldmapData.GetCityData(sPacket.cityID);
			if (cityData == null)
			{
				Debug.LogError("FATAL : City/Area Does not exist, Cilent-Server Templet info sync off");
				return;
			}
			myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItemData);
			cityData.worldMapMission.stMissionIDList = sPacket.stMissionIDList;
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WORLDMAP)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().CityDataUpdated(cityData);
			}
		}

		// Token: 0x060058A6 RID: 22694 RVA: 0x001AC330 File Offset: 0x001AA530
		public static void OnRecv(NKMPacket_WORLDMAP_MISSION_COMPLETE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMWorldMapCityData cityData = myUserData.m_WorldmapData.GetCityData(sPacket.cityID);
			if (cityData == null)
			{
				Debug.LogError("FATAL : City/Area Does not exist, Cilent-Server Templet info sync off");
				return;
			}
			NKCUIResult.CityMissionResultData resultData = NKCUIResult.MakeCityMissionCompleteUIData(myUserData.m_ArmyData, cityData, sPacket.rewardData, sPacket.exp, sPacket.level, sPacket.isSuccess);
			int level = sPacket.level;
			int level2 = cityData.level;
			cityData.worldMapMission.stMissionIDList = sPacket.stMissionIDList;
			cityData.worldMapMission.completeTime = 0L;
			cityData.worldMapMission.currentMissionID = 0;
			cityData.exp = sPacket.exp;
			cityData.level = sPacket.level;
			bool flag = false;
			if (cityData.worldMapEventGroup.worldmapEventID == 0 && sPacket.worldMapEventGroup.worldmapEventID > 0)
			{
				flag = true;
			}
			cityData.worldMapEventGroup = sPacket.worldMapEventGroup;
			myUserData.GetReward(sPacket.rewardData);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WORLDMAP)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().CityDataUpdated(cityData);
				if (flag)
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().CityEventSpawned(cityData.cityID);
				}
			}
			long leaderUnitUID = cityData.leaderUnitUID;
			NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(leaderUnitUID);
			if (unitFromUID != null)
			{
				NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_WORLDMAP_MISSION_COMPLETE, unitFromUID, false, false);
			}
			if (NKCPopupOKCancel.isOpen())
			{
				NKCPopupOKCancel.ClosePopupBox();
			}
			if (flag)
			{
				NKMWorldMapEventTemplet nkmworldMapEventTemplet = NKMWorldMapEventTemplet.Find(sPacket.worldMapEventGroup.worldmapEventID);
				if (nkmworldMapEventTemplet != null)
				{
					if (nkmworldMapEventTemplet.eventType == NKM_WORLDMAP_EVENT_TYPE.WET_RAID)
					{
						NKCPacketSender.Send_NKMPacket_MY_RAID_LIST_REQ();
						NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().SetReservedWarning(NKC_WORLD_MAP_WARNING_TYPE.NWMWT_RAID, cityData.cityID);
					}
					else if (nkmworldMapEventTemplet.eventType == NKM_WORLDMAP_EVENT_TYPE.WET_DIVE)
					{
						NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().SetReservedWarning(NKC_WORLD_MAP_WARNING_TYPE.NWMWT_DIVE, cityData.cityID);
					}
					else
					{
						NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().SetReservedWarning(NKC_WORLD_MAP_WARNING_TYPE.NWMWT_NONE, -1);
					}
					NKCUIResult.Instance.OpenCityMissionResult(resultData, delegate
					{
						NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().CloseCityManageUI();
						NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().ShowReservedWarningUI();
						NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().OnWorldmapMissionCompleteAck();
					});
					return;
				}
			}
			else
			{
				NKCUIResult.Instance.OpenCityMissionResult(resultData, delegate
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().OnWorldmapMissionCompleteAck();
				});
			}
		}

		// Token: 0x060058A7 RID: 22695 RVA: 0x001AC564 File Offset: 0x001AA764
		public static void OnRecv(NKMPacket_WORLDMAP_REMOVE_EVENT_DUNGEON_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			NKMWorldMapCityData cityData = nkmuserData.m_WorldmapData.GetCityData(sPacket.cityID);
			if (cityData != null)
			{
				cityData.worldMapEventGroup.Clear();
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WORLDMAP)
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().OnRecv(sPacket);
				}
			}
		}

		// Token: 0x060058A8 RID: 22696 RVA: 0x001AC5CE File Offset: 0x001AA7CE
		public static void OnRecv(NKMPacket_WORLDMAP_COLLECT_ACK sPacket)
		{
			NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, int.MinValue);
		}

		// Token: 0x060058A9 RID: 22697 RVA: 0x001AC5E4 File Offset: 0x001AA7E4
		public static void OnRecv(NKMPacket_WORLDMAP_BUILD_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			NKMWorldMapCityData cityData = nkmuserData.m_WorldmapData.GetCityData(sPacket.cityID);
			if (cityData == null)
			{
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_CITY_ID, null, "");
				Debug.LogError("FATAL : City not exist. Client-Server data sync off");
				return;
			}
			nkmuserData.m_InventoryData.UpdateItemInfo(sPacket.costItemDataList);
			NKMWorldmapCityBuildingData nkmworldmapCityBuildingData = new NKMWorldmapCityBuildingData();
			nkmworldmapCityBuildingData.id = sPacket.buildID;
			nkmworldmapCityBuildingData.level = 1;
			cityData.AddBuild(nkmworldmapCityBuildingData);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WORLDMAP)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().CityBuildingChanged(cityData, nkmworldmapCityBuildingData.id);
			}
		}

		// Token: 0x060058AA RID: 22698 RVA: 0x001AC698 File Offset: 0x001AA898
		public static void OnRecv(NKMPacket_WORLDMAP_BUILD_LEVELUP_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			NKMWorldMapCityData cityData = nkmuserData.m_WorldmapData.GetCityData(sPacket.cityID);
			if (cityData == null)
			{
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_CITY_ID, null, "");
				Debug.LogError("FATAL : City not exist. Client-Server data sync off");
				return;
			}
			cityData.UpdateBuildingData(sPacket.worldMapCityBuildingData);
			nkmuserData.m_InventoryData.UpdateItemInfo(sPacket.costItemDataList);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WORLDMAP)
			{
				NKMWorldmapCityBuildingData worldMapCityBuildingData = sPacket.worldMapCityBuildingData;
				int changedBuildingID = (worldMapCityBuildingData != null) ? worldMapCityBuildingData.id : 0;
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().CityBuildingChanged(cityData, changedBuildingID);
			}
		}

		// Token: 0x060058AB RID: 22699 RVA: 0x001AC744 File Offset: 0x001AA944
		public static void OnRecv(NKMPacket_WORLDMAP_BUILD_EXPIRE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			NKMWorldMapCityData cityData = nkmuserData.m_WorldmapData.GetCityData(sPacket.cityID);
			if (cityData == null)
			{
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_CITY_ID, null, "");
				Debug.LogError("FATAL : City not exist. Client-Server data sync off");
				return;
			}
			cityData.RemoveBuild(sPacket.buildID);
			nkmuserData.m_InventoryData.UpdateItemInfo(sPacket.itemData);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WORLDMAP)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().CityBuildingChanged(cityData, 0);
			}
		}

		// Token: 0x060058AC RID: 22700 RVA: 0x001AC7DC File Offset: 0x001AA9DC
		public static void OnRecv(NKMPacket_POST_LIST_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCMailManager.AddMail(sPacket.postDataList, sPacket.postCount);
		}

		// Token: 0x060058AD RID: 22701 RVA: 0x001AC804 File Offset: 0x001AAA04
		public static void OnRecv(NKMPacket_POST_LIST_NOT sPacket)
		{
			NKCMailManager.AddMail(sPacket.postDataList, sPacket.postCount);
		}

		// Token: 0x060058AE RID: 22702 RVA: 0x001AC818 File Offset: 0x001AAA18
		public static void OnRecv(NKMPacket_POST_RECEIVE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				if (sPacket.postIndex != 0L)
				{
					return;
				}
				if (sPacket.errorCode != NKM_ERROR_CODE.NEC_FAIL_POST_RECV_ITEM_FULL)
				{
					return;
				}
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.GetReward(sPacket.rewardDate);
			NKCMailManager.OnPostReceive(sPacket);
			NKCUIResult.Instance.OpenMailResult(myUserData.m_ArmyData, sPacket.rewardDate, null);
		}

		// Token: 0x060058AF RID: 22703 RVA: 0x001AC884 File Offset: 0x001AAA84
		public static void OnRecv(NKMPacket_POST_ARRIVE_NOT sPacket)
		{
			NKCMailManager.OnNewMailNotify(sPacket.count);
		}

		// Token: 0x060058B0 RID: 22704 RVA: 0x001AC894 File Offset: 0x001AAA94
		public static void OnRecv(NKMPacket_SHIP_BUILD_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.UpdateItemInfo(sPacket.costItemList);
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			if (armyData.IsFirstGetUnit(sPacket.shipData.m_UnitID))
			{
				NKCUIGameResultGetUnit.AddFirstGetUnit(sPacket.shipData.m_UnitID);
			}
			armyData.AddNewShip(sPacket.shipData);
			if (NKCUIHangarBuild.IsInstanceOpen)
			{
				NKCUIHangarBuild.Instance.UpdateUI(true);
			}
			NKMRewardData nkmrewardData = new NKMRewardData();
			nkmrewardData.UnitDataList.Add(sPacket.shipData);
			if (NKCGameEventManager.IsWaiting())
			{
				NKCUIResult.Instance.OpenRewardGain(NKCScenManager.CurrentUserData().m_ArmyData, nkmrewardData, NKCUtilString.GET_STRING_HANGAR_BUILD, "", new NKCUIResult.OnClose(NKCGameEventManager.WaitFinished));
			}
			else
			{
				NKCUIResult.Instance.OpenRewardGain(NKCScenManager.CurrentUserData().m_ArmyData, nkmrewardData, NKCUtilString.GET_STRING_HANGAR_BUILD, "", null);
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(sPacket.shipData.m_UnitID);
			if (unitTempletBase != null)
			{
				switch (unitTempletBase.m_NKM_UNIT_STYLE_TYPE)
				{
				case NKM_UNIT_STYLE_TYPE.NUST_SHIP_ASSAULT:
					NKCUINPCHangarNaHeeRin.PlayVoice(NPC_TYPE.HANGAR_NAHEERIN, NPC_ACTION_TYPE.SHIP_GET_ASSAULT, true);
					return;
				case NKM_UNIT_STYLE_TYPE.NUST_SHIP_HEAVY:
					NKCUINPCHangarNaHeeRin.PlayVoice(NPC_TYPE.HANGAR_NAHEERIN, NPC_ACTION_TYPE.SHIP_GET_HEAVY, true);
					return;
				case NKM_UNIT_STYLE_TYPE.NUST_SHIP_CRUISER:
					NKCUINPCHangarNaHeeRin.PlayVoice(NPC_TYPE.HANGAR_NAHEERIN, NPC_ACTION_TYPE.SHIP_GET_CRUISER, true);
					return;
				case NKM_UNIT_STYLE_TYPE.NUST_SHIP_SPECIAL:
					NKCUINPCHangarNaHeeRin.PlayVoice(NPC_TYPE.HANGAR_NAHEERIN, NPC_ACTION_TYPE.SHIP_GET_SPECIAL, true);
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x060058B1 RID: 22705 RVA: 0x001AC9E8 File Offset: 0x001AABE8
		public static void OnRecv(NKMPacket_SHIP_LEVELUP_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.m_ArmyData.UpdateShipData(sPacket.shipUnitData);
			myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItemDataList);
		}

		// Token: 0x060058B2 RID: 22706 RVA: 0x001ACA38 File Offset: 0x001AAC38
		public static void OnRecv(NKMPacket_SHIP_DIVISION_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMArmyData armyData = myUserData.m_ArmyData;
			myUserData.m_InventoryData.AddItemMisc(sPacket.rewardItemDataList);
			armyData.RemoveShip(sPacket.removeShipUIDList);
			if (sPacket.rewardItemDataList.Count > 0)
			{
				NKCUIResult.Instance.OpenItemGain(sPacket.rewardItemDataList, NKCUtilString.GET_STRING_ITEM_GAIN, NKCUtilString.GET_STRING_REMOVE_SHIP, null);
			}
			if (NKCUIUnitSelectList.IsInstanceOpen)
			{
				NKCUIUnitSelectList.Instance.CloseRemoveMode();
				NKCUIUnitSelectList.Instance.ClearMultipleSelect();
			}
			NKCUINPCHangarNaHeeRin.PlayVoice(NPC_TYPE.HANGAR_NAHEERIN, NPC_ACTION_TYPE.SHIP_DIVISION, true);
		}

		// Token: 0x060058B3 RID: 22707 RVA: 0x001ACAD4 File Offset: 0x001AACD4
		public static void OnRecv(NKMPacket_SHIP_UPGRADE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItemDataList);
			NKMArmyData armyData = myUserData.m_ArmyData;
			armyData.UpdateShipData(sPacket.shipUnitData);
			armyData.TryCollectUnit(sPacket.shipUnitData.m_UnitID);
			if (NKCUIShipInfo.IsInstanceOpen)
			{
				NKCUIShipInfo.Instance.OnRecv(sPacket);
			}
		}

		// Token: 0x060058B4 RID: 22708 RVA: 0x001ACB48 File Offset: 0x001AAD48
		public static void OnRecv(NKMPacket_LIMIT_BREAK_SHIP_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCUIGameResultGetUnit.Instance.Open(new NKCUIGameResultGetUnit.GetUnitResultData(sPacket.shipData), null, false, false, false, NKCUIGameResultGetUnit.Type.Ship);
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItemDataList);
			myUserData.m_ArmyData.RemoveShip(sPacket.consumeUnitUid);
			myUserData.m_ArmyData.UpdateShipData(sPacket.shipData);
		}

		// Token: 0x060058B5 RID: 22709 RVA: 0x001ACBC0 File Offset: 0x001AADC0
		public static void OnRecv(NKMPacket_SHIP_SLOT_FIRST_OPTION_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.UpdateShipData(sPacket.shipData);
			if (NKCPopupShipCommandModule.IsInstanceOpen)
			{
				NKCPopupShipCommandModule.Instance.ShowModuleOpenFx();
			}
		}

		// Token: 0x060058B6 RID: 22710 RVA: 0x001ACC10 File Offset: 0x001AAE10
		public static void OnRecv(NKMPacket_SHIP_SLOT_LOCK_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItemDataList);
			myUserData.m_ArmyData.UpdateShipData(sPacket.shipData);
		}

		// Token: 0x060058B7 RID: 22711 RVA: 0x001ACC60 File Offset: 0x001AAE60
		public static void OnRecv(NKMPacket_SHIP_SLOT_OPTION_CHANGE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItemDataList);
			myUserData.SetShipCandidateData(sPacket.candidateOption);
			if (NKCPopupShipCommandModule.IsInstanceOpen)
			{
				NKCPopupShipCommandModule.Instance.CandidateChanged();
			}
			myUserData.m_ArmyData.UpdateShipData(sPacket.shipData);
		}

		// Token: 0x060058B8 RID: 22712 RVA: 0x001ACCCC File Offset: 0x001AAECC
		public static void OnRecv(NKMPacket_SHIP_SLOT_OPTION_CONFIRM_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.SetShipCandidateData(new NKMShipModuleCandidate());
			myUserData.m_ArmyData.UpdateShipData(sPacket.shipData);
			if (NKCPopupShipCommandModule.IsInstanceOpen)
			{
				NKCPopupShipCommandModule.Instance.CandidateChanged();
			}
		}

		// Token: 0x060058B9 RID: 22713 RVA: 0x001ACD24 File Offset: 0x001AAF24
		public static void OnRecv(NKMPacket_SHIP_SLOT_OPTION_CANCEL_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.GetScenManager().GetMyUserData().SetShipCandidateData(new NKMShipModuleCandidate());
			if (NKCPopupShipCommandModule.IsInstanceOpen)
			{
				NKCPopupShipCommandModule.Instance.OnCandidateRemoved();
			}
		}

		// Token: 0x060058BA RID: 22714 RVA: 0x001ACD60 File Offset: 0x001AAF60
		public static void OnRecv(NKMPacket_CRAFT_UNLOCK_SLOT_ACK cNKMPacket_CREATION_UNLOCK_SLOT_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_CREATION_UNLOCK_SLOT_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.m_InventoryData.UpdateItemInfo(cNKMPacket_CREATION_UNLOCK_SLOT_ACK.costItemDataList);
			myUserData.m_CraftData.AddSlotData(cNKMPacket_CREATION_UNLOCK_SLOT_ACK.craftSlotData);
			if (NKCUIForgeCraft.IsInstanceOpen)
			{
				NKCUIForgeCraft.Instance.ResetUI();
				NKCUIForgeCraft.Instance.OnRecvSlotOpen((int)cNKMPacket_CREATION_UNLOCK_SLOT_ACK.craftSlotData.Index);
			}
			if (NKCUIOffice.IsInstanceOpen)
			{
				NKCUIOffice.GetInstance().UpdateForgeSlot((int)cNKMPacket_CREATION_UNLOCK_SLOT_ACK.craftSlotData.Index);
			}
		}

		// Token: 0x060058BB RID: 22715 RVA: 0x001ACDF0 File Offset: 0x001AAFF0
		public static void OnRecv(NKMPacket_EQUIP_TUNING_REFINE_ACK cNKMPacket_EQUIP_TUNING_REFINE_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_EQUIP_TUNING_REFINE_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMEquipItemData nkmequipItemData = new NKMEquipItemData();
			int changedSlotNum = -1;
			NKMEquipItemData itemEquip = myUserData.m_InventoryData.GetItemEquip(cNKMPacket_EQUIP_TUNING_REFINE_ACK.equipItemData.m_ItemUid);
			if (itemEquip != null)
			{
				int num = 1;
				while (num < itemEquip.m_Stat.Count && cNKMPacket_EQUIP_TUNING_REFINE_ACK.equipItemData.m_Stat.Count > num)
				{
					if (itemEquip.m_Stat[num].stat_value != cNKMPacket_EQUIP_TUNING_REFINE_ACK.equipItemData.m_Stat[num].stat_value && ((num == 1 && cNKMPacket_EQUIP_TUNING_REFINE_ACK.equipItemData.m_Precision >= 100) || (num == 2 && cNKMPacket_EQUIP_TUNING_REFINE_ACK.equipItemData.m_Precision2 >= 100)))
					{
						changedSlotNum = num;
					}
					num++;
				}
				nkmequipItemData.DeepCopyFrom(itemEquip);
				itemEquip.DeepCopyFrom(cNKMPacket_EQUIP_TUNING_REFINE_ACK.equipItemData);
				myUserData.m_InventoryData.UpdateItemEquip(itemEquip);
			}
			myUserData.m_InventoryData.UpdateItemInfo(cNKMPacket_EQUIP_TUNING_REFINE_ACK.costItemDataList);
			if (NKCUIForge.IsInstanceOpen)
			{
				NKCUIForge.Instance.DoAfterRefine(nkmequipItemData, changedSlotNum);
			}
			NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GetRefineResultMsg(cNKMPacket_EQUIP_TUNING_REFINE_ACK.equipRefineResult), NKCPopupMessage.eMessagePosition.Top, true, true, 0f, false);
			NKCUINPCFactoryAnastasia.PlayVoice(NPC_TYPE.FACTORY_ANASTASIA, NPC_ACTION_TYPE.ITEM_TUNING, true);
		}

		// Token: 0x060058BC RID: 22716 RVA: 0x001ACF2C File Offset: 0x001AB12C
		public static void OnRecv(NKMPacket_EQUIP_TUNING_STAT_CHANGE_ACK cNKMPacket_EQUIP_TUNING_STAT_CHANGE_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_EQUIP_TUNING_STAT_CHANGE_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMEquipItemData itemEquip = myUserData.m_InventoryData.GetItemEquip(cNKMPacket_EQUIP_TUNING_STAT_CHANGE_ACK.equipItemData.m_ItemUid);
			if (itemEquip != null)
			{
				itemEquip.DeepCopyFrom(cNKMPacket_EQUIP_TUNING_STAT_CHANGE_ACK.equipItemData);
				myUserData.m_InventoryData.UpdateItemEquip(itemEquip);
			}
			myUserData.m_InventoryData.UpdateItemInfo(cNKMPacket_EQUIP_TUNING_STAT_CHANGE_ACK.costItemDataList);
			NKCScenManager.CurrentUserData().SetEquipTuningData(cNKMPacket_EQUIP_TUNING_STAT_CHANGE_ACK.equipTuningCandidate);
			if (NKCUIForge.IsInstanceOpen)
			{
				NKCUIForge.Instance.ResetUI();
				NKCUIForge.Instance.DoAfterOptionChanged(cNKMPacket_EQUIP_TUNING_STAT_CHANGE_ACK.equipOptionID);
			}
		}

		// Token: 0x060058BD RID: 22717 RVA: 0x001ACFD0 File Offset: 0x001AB1D0
		public static void OnRecv(NKMPacket_EQUIP_TUNING_STAT_CHANGE_CONFIRM_ACK cNKMPacket_EQUIP_TUNING_STAT_CHANGE_CONFIRM_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_EQUIP_TUNING_STAT_CHANGE_CONFIRM_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMEquipItemData itemEquip = myUserData.m_InventoryData.GetItemEquip(cNKMPacket_EQUIP_TUNING_STAT_CHANGE_CONFIRM_ACK.equipItemData.m_ItemUid);
			if (itemEquip != null)
			{
				itemEquip.DeepCopyFrom(cNKMPacket_EQUIP_TUNING_STAT_CHANGE_CONFIRM_ACK.equipItemData);
				myUserData.m_InventoryData.UpdateItemEquip(itemEquip);
			}
			NKCScenManager.CurrentUserData().SetEquipTuningData(cNKMPacket_EQUIP_TUNING_STAT_CHANGE_CONFIRM_ACK.equipTuningCandidate);
			if (NKCUIForge.IsInstanceOpen)
			{
				NKCUIForge.Instance.ResetUI();
				NKCUIForge.Instance.DoAfterOptionChangedConfirm();
			}
			NKCUINPCFactoryAnastasia.PlayVoice(NPC_TYPE.FACTORY_ANASTASIA, NPC_ACTION_TYPE.ITEM_TUNING, true);
		}

		// Token: 0x060058BE RID: 22718 RVA: 0x001AD064 File Offset: 0x001AB264
		public static void OnRecv(NKMPacket_CRAFT_START_ACK cNKMPacket_CREATION_START_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_CREATION_START_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCUIForgeCraftMold.CheckInstanceAndClose();
			NKCScenManager.GetScenManager().GetMyUserData().m_CraftData.UpdateSlotData(cNKMPacket_CREATION_START_ACK.craftSlotData);
			NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(cNKMPacket_CREATION_START_ACK.craftSlotData.MoldID);
			if (itemMoldTempletByID != null && !itemMoldTempletByID.m_bPermanent)
			{
				NKCScenManager.GetScenManager().GetMyUserData().m_CraftData.DecMoldItem(cNKMPacket_CREATION_START_ACK.craftSlotData.MoldID, (long)cNKMPacket_CREATION_START_ACK.craftSlotData.Count);
			}
			NKCPublisherModule.Push.UpdateLocalPush(NKC_GAME_OPTION_ALARM_GROUP.CRAFT_COMPLETE, false);
			NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.UpdateItemInfo(cNKMPacket_CREATION_START_ACK.materialItemDataList);
			NKCUINPCFactoryAnastasia.PlayVoice(NPC_TYPE.FACTORY_ANASTASIA, NPC_ACTION_TYPE.ITEM_CRAFT_START, true);
			if (NKCUIForgeCraft.IsInstanceOpen)
			{
				NKCUIForgeCraft.Instance.ResetUI();
			}
			if (NKCUIOffice.IsInstanceOpen)
			{
				NKCUIOffice.GetInstance().UpdateForgeSlot((int)cNKMPacket_CREATION_START_ACK.craftSlotData.Index);
			}
			if (NKCUIOfficeMapFront.IsInstanceOpen)
			{
				NKCUIOfficeMapFront.GetInstance().UpdateFactoryState();
			}
			NKCUINPCFactoryAnastasia.PlayVoice(NPC_TYPE.FACTORY_ANASTASIA, NPC_ACTION_TYPE.THANKS, true);
		}

		// Token: 0x060058BF RID: 22719 RVA: 0x001AD164 File Offset: 0x001AB364
		public static void OnRecv(NKMPacket_CRAFT_COMPLETE_ACK cNKMPacket_CREATION_COMPLETE_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_CREATION_COMPLETE_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.GetScenManager().GetMyUserData().m_CraftData.UpdateSlotData(cNKMPacket_CREATION_COMPLETE_ACK.craftSlotData);
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.GetReward(cNKMPacket_CREATION_COMPLETE_ACK.createdRewardData);
			NKCUINPCFactoryAnastasia.PlayVoice(NPC_TYPE.FACTORY_ANASTASIA, NPC_ACTION_TYPE.ITEM_CRAFT_COMPLETE, true);
			if (NKCUIForgeCraft.IsInstanceOpen)
			{
				NKCUIForgeCraft.Instance.ResetUI();
			}
			if (NKCUIOffice.IsInstanceOpen)
			{
				NKCUIOffice.GetInstance().UpdateForgeSlot((int)cNKMPacket_CREATION_COMPLETE_ACK.craftSlotData.Index);
			}
			if (NKCUIOfficeMapFront.IsInstanceOpen)
			{
				NKCUIOfficeMapFront.GetInstance().UpdateFactoryState();
			}
			NKCUIResult.Instance.OpenComplexResult(myUserData.m_ArmyData, cNKMPacket_CREATION_COMPLETE_ACK.createdRewardData, null, 0L, null, true, false);
		}

		// Token: 0x060058C0 RID: 22720 RVA: 0x001AD21C File Offset: 0x001AB41C
		public static void OnRecv(NKMPacket_CRAFT_INSTANT_COMPLETE_ACK cNKMPacket_CREATION_INSTANT_COMPLETE_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_CREATION_INSTANT_COMPLETE_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.GetScenManager().GetMyUserData().m_CraftData.UpdateSlotData(cNKMPacket_CREATION_INSTANT_COMPLETE_ACK.craftSlotData);
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.GetReward(cNKMPacket_CREATION_INSTANT_COMPLETE_ACK.createdRewardData);
			myUserData.m_InventoryData.UpdateItemInfo(cNKMPacket_CREATION_INSTANT_COMPLETE_ACK.extraCostItemData);
			NKCUINPCFactoryAnastasia.PlayVoice(NPC_TYPE.FACTORY_ANASTASIA, NPC_ACTION_TYPE.ITEM_CRAFT_COMPLETE, true);
			if (NKCUIForgeCraft.IsInstanceOpen)
			{
				NKCUIForgeCraft.Instance.ResetUI();
			}
			if (NKCUIOffice.IsInstanceOpen)
			{
				NKCUIOffice.GetInstance().UpdateForgeSlot((int)cNKMPacket_CREATION_INSTANT_COMPLETE_ACK.craftSlotData.Index);
			}
			if (NKCUIOfficeMapFront.IsInstanceOpen)
			{
				NKCUIOfficeMapFront.GetInstance().UpdateFactoryState();
			}
			NKCUIResult.Instance.OpenComplexResult(myUserData.m_ArmyData, cNKMPacket_CREATION_INSTANT_COMPLETE_ACK.createdRewardData, null, 0L, null, true, false);
			NKCPublisherModule.Push.UpdateLocalPush(NKC_GAME_OPTION_ALARM_GROUP.CRAFT_COMPLETE, true);
		}

		// Token: 0x060058C1 RID: 22721 RVA: 0x001AD2F0 File Offset: 0x001AB4F0
		public static void OnRecv(NKMPacket_REMOVE_EQUIP_ITEM_ACK cNKMPacket_REMOVE_EQUIP_ITEM_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_REMOVE_EQUIP_ITEM_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.m_InventoryData.RemoveItemEquip(cNKMPacket_REMOVE_EQUIP_ITEM_ACK.removeEquipItemUIDList);
			NKCUINPCFactoryAnastasia.PlayVoice(NPC_TYPE.FACTORY_ANASTASIA, NPC_ACTION_TYPE.ITEM_RECYCLE, false);
			if (cNKMPacket_REMOVE_EQUIP_ITEM_ACK.rewardItemDataList.Count > 0)
			{
				myUserData.m_InventoryData.AddItemMisc(cNKMPacket_REMOVE_EQUIP_ITEM_ACK.rewardItemDataList);
				Log.Info("OpenItemGain", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCPacketHandlersLobby.cs", 6152);
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_INVENTORY || NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_OFFICE)
				{
					NKCScenManager.GetScenManager().Get_SCEN_INVENTORY().OnRemoveEquipItemAck();
				}
				else
				{
					NKCUIInventory.CheckInstanceAndClose();
				}
				NKCUIResult.Instance.OpenItemGain(cNKMPacket_REMOVE_EQUIP_ITEM_ACK.rewardItemDataList, NKCUtilString.GET_STRING_ITEM_GAIN, NKCUtilString.GET_STRING_EQUIP_BREAK_UP, null);
			}
		}

		// Token: 0x060058C2 RID: 22722 RVA: 0x001AD3B8 File Offset: 0x001AB5B8
		public static void OnRecv(NKMPAcket_EQUIP_ITEM_ENCHANT_ACK cNKMPAcket_EQUIP_ITEM_ENCHANT_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPAcket_EQUIP_ITEM_ENCHANT_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMEquipItemData itemEquip = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(cNKMPAcket_EQUIP_ITEM_ENCHANT_ACK.equipItemUID);
			itemEquip.m_EnchantLevel = cNKMPAcket_EQUIP_ITEM_ENCHANT_ACK.enchantLevel;
			itemEquip.m_EnchantExp = cNKMPAcket_EQUIP_ITEM_ENCHANT_ACK.enchantExp;
			NKCScenManager.CurrentUserData().m_InventoryData.UpdateItemEquip(itemEquip);
			for (int i = 0; i < cNKMPAcket_EQUIP_ITEM_ENCHANT_ACK.consumeEquipItemUIDList.Count; i++)
			{
				NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.RemoveItemEquip(cNKMPAcket_EQUIP_ITEM_ENCHANT_ACK.consumeEquipItemUIDList[i]);
			}
			NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.UpdateItemInfo(cNKMPAcket_EQUIP_ITEM_ENCHANT_ACK.costItemDataList);
			if (NKCUIForge.IsInstanceOpen)
			{
				NKCUIForge.Instance.PlayEnhanceEffect();
			}
			if (NKCUIInventory.IsInstanceOpen)
			{
				NKCUIInventory.Instance.ResetEquipSlotList();
			}
			NKCUINPCFactoryAnastasia.PlayVoice(NPC_TYPE.FACTORY_ANASTASIA, NPC_ACTION_TYPE.ITEM_ENHANCE, false);
		}

		// Token: 0x060058C3 RID: 22723 RVA: 0x001AD498 File Offset: 0x001AB698
		public static void OnRecv(NKMPacket_EQUIP_ITEM_EQUIP_ACK cNKMPacket_EQUIP_ITEM_EQUIP_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_EQUIP_ITEM_EQUIP_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMUnitData unitFromUID = myUserData.m_ArmyData.GetUnitFromUID(cNKMPacket_EQUIP_ITEM_EQUIP_ACK.unitUID);
			NKMUnitData nkmunitData = null;
			if (unitFromUID != null)
			{
				if (cNKMPacket_EQUIP_ITEM_EQUIP_ACK.unequipItemUID > 0L)
				{
					NKMEquipItemData itemEquip = myUserData.m_InventoryData.GetItemEquip(cNKMPacket_EQUIP_ITEM_EQUIP_ACK.unequipItemUID);
					if (itemEquip != null && unitFromUID.m_UnitUID != itemEquip.m_OwnerUnitUID)
					{
						nkmunitData = myUserData.m_ArmyData.GetUnitFromUID(itemEquip.m_OwnerUnitUID);
					}
					if (!unitFromUID.UnEquipItem(myUserData.m_InventoryData, cNKMPacket_EQUIP_ITEM_EQUIP_ACK.unequipItemUID, cNKMPacket_EQUIP_ITEM_EQUIP_ACK.equipPosition))
					{
						Debug.LogError("UnitData.UnEquipItem failed");
					}
					NKMEquipItemData itemEquip2 = myUserData.m_InventoryData.GetItemEquip(cNKMPacket_EQUIP_ITEM_EQUIP_ACK.unequipItemUID);
					myUserData.m_InventoryData.UpdateItemEquip(itemEquip2);
					if (NKCUIInventory.IsInstanceOpen)
					{
						if (NKCUIInventory.Instance.GetNKCUIInventoryOption().m_dOnClickEmptySlot != null)
						{
							NKCUIInventory.Instance.Close();
						}
						else
						{
							NKCUIInventory.Instance.UpdateEquipSlot(cNKMPacket_EQUIP_ITEM_EQUIP_ACK.unequipItemUID);
						}
					}
				}
				if (cNKMPacket_EQUIP_ITEM_EQUIP_ACK.equipItemUID > 0L)
				{
					long num = 0L;
					if (!unitFromUID.EquipItem(myUserData.m_InventoryData, cNKMPacket_EQUIP_ITEM_EQUIP_ACK.equipItemUID, out num, cNKMPacket_EQUIP_ITEM_EQUIP_ACK.equipPosition))
					{
						Debug.LogError("UnitData.EquipItem failed");
					}
					NKMEquipItemData itemEquip3 = myUserData.m_InventoryData.GetItemEquip(cNKMPacket_EQUIP_ITEM_EQUIP_ACK.equipItemUID);
					myUserData.m_InventoryData.UpdateItemEquip(itemEquip3);
					if (NKCUIUnitSelectList.IsInstanceOpen && !NKCUIUnitInfo.IsInstanceOpen)
					{
						NKCUIUnitSelectList.Instance.Close();
						if (NKCUIInventory.IsInstanceOpen)
						{
							NKCUIInventory.Instance.UpdateEquipSlot(cNKMPacket_EQUIP_ITEM_EQUIP_ACK.equipItemUID);
						}
					}
					else if (NKCUIInventory.IsInstanceOpen)
					{
						NKCUIInventory.Instance.Close();
					}
					NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_GROWTH_EQUIP, unitFromUID, false, false);
				}
			}
			if (nkmunitData != null)
			{
				myUserData.m_ArmyData.UpdateUnitData(nkmunitData);
			}
			myUserData.m_ArmyData.UpdateUnitData(unitFromUID);
			NKCPopupItemEquipBox.CloseItemBox();
			if (NKCUIUnitInfo.IsInstanceOpen)
			{
				NKCUIEquipPreset equipPreset = NKCUIUnitInfo.Instance.EquipPreset;
				if (equipPreset != null)
				{
					equipPreset.UpdatePresetData(null, false, 0, false);
				}
			}
			if (NKCRecallManager.m_bWaitingRecallProcess)
			{
				NKCRecallManager.OnUnequipComplete();
			}
			if (NKCUITacticUpdate.IsInstanceOpen)
			{
				NKCUITacticUpdate.Instance.OnUnEquipComplete();
			}
		}

		// Token: 0x060058C4 RID: 22724 RVA: 0x001AD69C File Offset: 0x001AB89C
		public static void OnRecv(NKMPacket_LOCK_EQUIP_ITEM_ACK cNKMPacket_LOCK_EQUIP_ITEM_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_LOCK_EQUIP_ITEM_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMEquipItemData itemEquip = myUserData.m_InventoryData.GetItemEquip(cNKMPacket_LOCK_EQUIP_ITEM_ACK.equipItemUID);
			itemEquip.m_bLock = cNKMPacket_LOCK_EQUIP_ITEM_ACK.isLock;
			if (NKCUIInventory.IsInstanceOpen)
			{
				NKCUIInventory.Instance.UpdateEquipSlot(cNKMPacket_LOCK_EQUIP_ITEM_ACK.equipItemUID);
			}
			myUserData.m_InventoryData.UpdateItemEquip(itemEquip);
		}

		// Token: 0x060058C5 RID: 22725 RVA: 0x001AD708 File Offset: 0x001AB908
		public static void OnRecv(NKMPacket_EQUIP_UPGRADE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItemDataList);
			myUserData.m_InventoryData.RemoveItemEquip(sPacket.consumeEquipItemUidList);
			myUserData.m_InventoryData.UpdateItemEquip(sPacket.equipItemData);
			if (NKCUIForgeUpgrade.IsInstanceOpen)
			{
				NKCUIForgeUpgrade.Instance.UpgradeFinished(sPacket.equipItemData);
			}
		}

		// Token: 0x060058C6 RID: 22726 RVA: 0x001AD780 File Offset: 0x001AB980
		public static void OnRecv(NKMPacket_EQUIP_OPEN_SOCKET_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItemDataList);
				myUserData.m_InventoryData.UpdateItemEquip(sPacket.equipItemData);
			}
			if (NKCUIForge.IsInstanceOpen && NKCUIForge.Instance.m_NKCUIForgeHiddenOption != null)
			{
				NKCUIForge.Instance.m_NKCUIForgeHiddenOption.SetUI();
				if (sPacket.equipItemData.potentialOption != null)
				{
					int socketIndex = -1;
					int num = sPacket.equipItemData.potentialOption.sockets.Length;
					for (int i = 0; i < num; i++)
					{
						if (sPacket.equipItemData.potentialOption.sockets[i] != null)
						{
							socketIndex = i;
						}
					}
					NKCUIForge.Instance.m_NKCUIForgeHiddenOption.UnlockingSocket = true;
					NKCUIForge.Instance.m_NKCUIForgeHiddenOption.ActivateUnlockFx(socketIndex, delegate
					{
						NKCUIForge.Instance.ResetUI();
					});
				}
			}
		}

		// Token: 0x060058C7 RID: 22727 RVA: 0x001AD884 File Offset: 0x001ABA84
		public static void OnRecv(NKMPacket_RANDOM_ITEM_BOX_OPEN_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				if (NKCGameEventManager.IsEventPlaying())
				{
					NKCGameEventManager.CollectResultData(null);
				}
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			NKCUIGameResultGetUnit.AddFirstGetUnit(sPacket.rewardData);
			myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItemData);
			myUserData.GetReward(sPacket.rewardData);
			if (NKCGameEventManager.RandomBoxDataCollecting)
			{
				NKCGameEventManager.CollectResultData(sPacket.rewardData);
				return;
			}
			NKCUIResult.Instance.OpenBoxGain(myUserData.m_ArmyData, sPacket.rewardData, sPacket.costItemData.ItemID, null);
		}

		// Token: 0x060058C8 RID: 22728 RVA: 0x001AD920 File Offset: 0x001ABB20
		public static void OnRecv(NKMPacket_CHOICE_ITEM_USE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				if (NKCGameEventManager.IsEventPlaying())
				{
					NKCGameEventManager.CollectResultData(null);
				}
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItemData);
			myUserData.GetReward(sPacket.rewardData);
			if (NKCUISelection.IsInstanceOpen)
			{
				NKCUISelection.Instance.Close();
			}
			if (NKCUISelectionEquip.IsInstanceOpen)
			{
				NKCUISelectionEquip.Instance.Close();
			}
			if (NKCUISelectionMisc.IsInstanceOpen)
			{
				NKCUISelectionMisc.Instance.Close();
			}
			if (NKCUISelectionSkin.IsInstanceOpen)
			{
				NKCUISelectionSkin.Instance.Close();
			}
			NKCUISelectionOperator.CheckInstanceAndClose();
			NKCUIResult.Instance.OpenComplexResult(NKCScenManager.CurrentUserData().m_ArmyData, sPacket.rewardData, null, 0L, null, false, false);
		}

		// Token: 0x060058C9 RID: 22729 RVA: 0x001AD9E0 File Offset: 0x001ABBE0
		public static void OnRecv(NKMPacket_MISC_CONTRACT_OPEN_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			if (sPacket.result == null)
			{
				return;
			}
			int boxItemID = 0;
			foreach (MiscContractResult miscContractResult in sPacket.result)
			{
				if (miscContractResult != null)
				{
					boxItemID = miscContractResult.miscItemId;
					foreach (NKMUnitData nkmunitData in miscContractResult.units)
					{
						if (nkmunitData != null)
						{
							NKCUIGameResultGetUnit.AddFirstGetUnit(nkmunitData.m_UnitID);
						}
					}
				}
			}
			myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItems);
			NKMRewardData nkmrewardData = new NKMRewardData();
			nkmrewardData.contractList = new List<MiscContractResult>(sPacket.result);
			myUserData.GetReward(nkmrewardData);
			NKCUIResult.Instance.OpenBoxGain(myUserData.m_ArmyData, nkmrewardData, boxItemID, null);
		}

		// Token: 0x060058CA RID: 22730 RVA: 0x001ADAF8 File Offset: 0x001ABCF8
		public static void OnRecv(NKMPacket_ZLONG_PAYMENT_NOTIFY sPacket)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_HOME)
			{
				NKC_SCEN_HOME scen_HOME = NKCScenManager.GetScenManager().Get_SCEN_HOME();
				if (scen_HOME != null)
				{
					scen_HOME.UnhideLobbyUI();
				}
			}
			NKCPacketHandlersLobby.CommonProcessBuy(sPacket.rewardData, sPacket.productID, sPacket.history, sPacket.subScriptionData);
			if (sPacket.totalPaidAmount > 0.0)
			{
				NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
				if (myUserData != null && myUserData.m_ShopData != null)
				{
					myUserData.m_ShopData.SetTotalPayment(sPacket.totalPaidAmount);
				}
			}
			if (NKCUIEvent.IsInstanceOpen)
			{
				NKCUIEvent.Instance.RefreshUI(0);
			}
		}

		// Token: 0x060058CB RID: 22731 RVA: 0x001ADB8D File Offset: 0x001ABD8D
		public static void OnRecv(NKMPacket_SHOP_FIXED_LIST_ACK sPacket)
		{
			if (sPacket.errorCode == NKM_ERROR_CODE.NEC_OK)
			{
				NKCShopManager.SetShopItemList(sPacket.shopList, sPacket.InstantProductList);
				return;
			}
			Debug.LogError("Server Error Code : " + sPacket.errorCode.ToString());
		}

		// Token: 0x060058CC RID: 22732 RVA: 0x001ADBCC File Offset: 0x001ABDCC
		private static void CommonProcessBuy(NKMRewardData rewardData, int productID, NKMShopPurchaseHistory history, NKMShopSubscriptionData subScriptionData)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			myUserData.GetReward(rewardData);
			if (subScriptionData != null)
			{
				if (myUserData.m_ShopData.subscriptions.ContainsKey(subScriptionData.productId))
				{
					myUserData.m_ShopData.subscriptions[subScriptionData.productId].endDate = subScriptionData.endDate;
				}
				else
				{
					subScriptionData.startDate = subScriptionData.startDate.AddMinutes(-1.0);
					myUserData.m_ShopData.subscriptions.Add(subScriptionData.productId, subScriptionData);
				}
			}
			if (history != null)
			{
				myUserData.m_ShopData.histories[history.shopId] = history;
			}
			ShopItemTemplet productTemplet = ShopItemTemplet.Find(productID);
			if (productTemplet.m_PriceItemID == 0)
			{
				NKCPublisherModule.Statistics.TrackPurchase(productTemplet.m_ProductID);
			}
			NKCAdjustManager.OnTrackPurchase(productTemplet);
			foreach (NKCUIShop nkcuishop in NKCUIManager.GetOpenedUIsByType<NKCUIShop>())
			{
				if (nkcuishop != null)
				{
					nkcuishop.RefreshShopItem(productID);
				}
				if (history != null && nkcuishop != null)
				{
					nkcuishop.RefreshShopItem(history.shopId);
				}
				if (nkcuishop != null)
				{
					nkcuishop.RefreshShopRedDot();
				}
				if (nkcuishop != null)
				{
					nkcuishop.OnProductBuy(productTemplet);
				}
			}
			if (rewardData != null)
			{
				if (rewardData.CompanyBuffDataList.Count > 0)
				{
					for (int i = 0; i < rewardData.CompanyBuffDataList.Count; i++)
					{
						NKCCompanyBuff.UpsertCompanyBuffData(myUserData.m_companyBuffDataList, rewardData.CompanyBuffDataList[i]);
					}
				}
				if (NKCShopManager.GetBundleCount() > 0)
				{
					NKCShopManager.RemoveBundleItemId(productID, rewardData);
				}
				else if (productTemplet.m_ItemType == NKM_REWARD_TYPE.RT_UNIT || productTemplet.m_ItemType == NKM_REWARD_TYPE.RT_OPERATOR)
				{
					NKCUIGameResultGetUnit.ShowNewUnitGetUI(rewardData, null, false, true, false);
				}
				else if (NKCShopManager.IsPackageItem(productID) || NKCShopManager.IsCustomPackageItem(productID))
				{
					NKCUIResult.Instance.OpenBoxGain(myUserData.m_ArmyData, new List<NKMRewardData>
					{
						rewardData
					}, productTemplet.GetItemName(), "", null, true, null, 1, true);
				}
				else
				{
					NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GetShopItemBuyMessage(productTemplet, false), NKCPopupMessage.eMessagePosition.Top, true, true, 0f, false);
				}
			}
			else
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GetShopItemBuyMessage(productTemplet, true), NKCPopupMessage.eMessagePosition.Top, true, true, 0f, false);
			}
			NKCUIShopSkinPopup.CheckInstanceAndClose();
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_EPISODE)
			{
				NKCScenManager.GetScenManager().Get_SCEN_EPISODE().UpdateTicketCountUI();
			}
			if (productTemplet.m_ItemType == NKM_REWARD_TYPE.RT_SKIN)
			{
				if (NKCUIShopSkinPopup.IsInstanceOpen)
				{
					NKCUIShopSkinPopup.Instance.OnSkinBuy(productTemplet.m_ItemID);
				}
				if (rewardData != null)
				{
					NKCUIGameResultGetUnit.ShowNewSkinGetUI(rewardData.SkinIdList, delegate
					{
						NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(productTemplet.m_ItemID);
						if (skinTemplet != null && !string.IsNullOrEmpty(skinTemplet.m_CutscenePurchase))
						{
							NKCUICutScenPlayer.Instance.LoadAndPlay(skinTemplet.m_CutscenePurchase, 0, null, true);
						}
					}, false, false);
				}
			}
		}

		// Token: 0x060058CD RID: 22733 RVA: 0x001ADE90 File Offset: 0x001AC090
		public static void OnRecv(NKMPacket_SHOP_FIX_SHOP_BUY_ACK sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			Log.Debug(string.Format("[Inapp] NKMPacket_SHOP_FIX_SHOP_BUY_ACK - productID[{0}]", sPacket.productID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCPacketHandlersLobby.cs", 6630);
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCPopupShopBuyShortcut.IsInstanceOpen)
			{
				NKCPopupShopBuyShortcut.Instance.Close();
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKCPacketHandlersLobby.CommonProcessBuy(sPacket.rewardData, sPacket.productID, sPacket.histroy, sPacket.subScriptionData);
			myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItemData);
			if (sPacket.totalPaidAmount > 0.0)
			{
				myUserData.m_ShopData.SetTotalPayment(sPacket.totalPaidAmount);
			}
			if (NKCPopupPointExchange.IsInstanceOpen)
			{
				NKCPopupPointExchange.Instance.RefreshProduct();
			}
			if (NKCUIPopupTrimDungeon.IsInstanceOpen)
			{
				NKCUIPopupTrimDungeon.Instance.RefreshUI(false);
			}
			List<NKCPopupItemBox> openedUIsByType = NKCUIManager.GetOpenedUIsByType<NKCPopupItemBox>();
			int count = openedUIsByType.Count;
			for (int i = 0; i < count; i++)
			{
				if (openedUIsByType[i].IsOpen)
				{
					openedUIsByType[i].RefreshDropInfo(false);
				}
			}
		}

		// Token: 0x060058CE RID: 22734 RVA: 0x001ADFA0 File Offset: 0x001AC1A0
		public static void OnRecv(NKMPacket_GAMEBASE_BUY_ACK sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCPopupShopBuyShortcut.IsInstanceOpen)
			{
				NKCPopupShopBuyShortcut.Instance.Close();
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKCPacketHandlersLobby.CommonProcessBuy(sPacket.rewardData, sPacket.productId, sPacket.histroy, sPacket.subScriptionData);
			myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItemData);
			if (sPacket.totalPaidAmount > 0.0)
			{
				myUserData.m_ShopData.SetTotalPayment(sPacket.totalPaidAmount);
			}
		}

		// Token: 0x060058CF RID: 22735 RVA: 0x001AE034 File Offset: 0x001AC234
		public static void OnRecv(NKMPacket_SHOP_REFRESH_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.m_ShopData.randomShop = sPacket.randomShopData;
			myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItemData);
			foreach (NKCUIShop nkcuishop in NKCUIManager.GetOpenedUIsByType<NKCUIShop>())
			{
				if (nkcuishop != null)
				{
					nkcuishop.RandomShopItemUpdateComplete();
				}
			}
		}

		// Token: 0x060058D0 RID: 22736 RVA: 0x001AE0CC File Offset: 0x001AC2CC
		public static void OnRecv(NKMPacket_SHOP_CHAIN_TAB_RESET_TIME_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.GetScenManager().GetMyUserData().m_ShopData.SetChainTabResetData(sPacket.list);
			foreach (NKCUIShop nkcuishop in NKCUIManager.GetOpenedUIsByType<NKCUIShop>())
			{
				if (nkcuishop != null)
				{
					nkcuishop.ChainRefreshComplete(sPacket.list);
				}
			}
		}

		// Token: 0x060058D1 RID: 22737 RVA: 0x001AE158 File Offset: 0x001AC358
		public static void OnRecv(NKMPacket_SHOP_RANDOM_SHOP_BUY_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMShopRandomListData nkmshopRandomListData;
			if (!myUserData.m_ShopData.randomShop.datas.TryGetValue(sPacket.slotIndex, out nkmshopRandomListData))
			{
				Debug.LogError("Bad random shop index from server");
			}
			nkmshopRandomListData.isBuy = true;
			myUserData.GetReward(sPacket.rewardData);
			myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItemData);
			if (nkmshopRandomListData.itemType == NKM_REWARD_TYPE.RT_UNIT || nkmshopRandomListData.itemType == NKM_REWARD_TYPE.RT_OPERATOR)
			{
				NKCUIGameResultGetUnit.ShowNewUnitGetUI(sPacket.rewardData, null, false, true, false);
			}
			else
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GetShopItemBuyMessage(nkmshopRandomListData), NKCPopupMessage.eMessagePosition.Top, true, true, 0f, false);
			}
			foreach (NKCUIShop nkcuishop in NKCUIManager.GetOpenedUIsByType<NKCUIShop>())
			{
				nkcuishop.RefreshRandomShopItem(sPacket.slotIndex);
				nkcuishop.OnProductBuy(null);
			}
		}

		// Token: 0x060058D2 RID: 22738 RVA: 0x001AE258 File Offset: 0x001AC458
		public static void OnRecv(NKMPacket_FIRST_CASH_PURCHASE_NOT sPacket)
		{
			NKCAdjustManager.OnCustomEvent("00_first_purchase");
		}

		// Token: 0x060058D3 RID: 22739 RVA: 0x001AE264 File Offset: 0x001AC464
		public static void OnRecv(NKMPacket_SHOP_FIX_SHOP_CASH_BUY_POSSIBLE_ACK sPacket)
		{
			NKMShopData shopData = NKCScenManager.CurrentUserData().m_ShopData;
			if (sPacket != null)
			{
				if (sPacket.histroy != null)
				{
					if (shopData.histories.ContainsKey(sPacket.histroy.shopId))
					{
						shopData.histories[sPacket.histroy.shopId] = sPacket.histroy;
					}
					else
					{
						shopData.histories.Add(sPacket.histroy.shopId, sPacket.histroy);
					}
				}
				if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
				{
					return;
				}
				NKCScenManager.GetScenManager().Get_NKC_SCEN_SHOP().OnRecvProductBuyCheck(sPacket.productMarketID, sPacket.selectIndices);
			}
		}

		// Token: 0x060058D4 RID: 22740 RVA: 0x001AE30C File Offset: 0x001AC50C
		public static void OnRecv(NKMPacket_CONSUMER_PACKAGE_UPDATED_NOT sPacket)
		{
			NKCScenManager.CurrentUserData().UpdateConsumerPackageData(sPacket.list);
		}

		// Token: 0x060058D5 RID: 22741 RVA: 0x001AE31E File Offset: 0x001AC51E
		public static void OnRecv(NKMPacket_CONSUMER_PACKAGE_REMOVED_NOT sPacket)
		{
			NKCScenManager.CurrentUserData().RemoveConsumerPackageData(sPacket.productIds);
		}

		// Token: 0x060058D6 RID: 22742 RVA: 0x001AE330 File Offset: 0x001AC530
		private static void ProcessWorldmapContentsAfterDiveEnd()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null || myUserData.m_DiveGameData == null)
			{
				return;
			}
			if (myUserData.m_DiveGameData.Floor.Templet.IsEventDive)
			{
				if (myUserData.m_DiveGameData.Player.PlayerBase.State == NKMDivePlayerState.Clear)
				{
					int num;
					myUserData.m_WorldmapData.RemoveEvent(NKM_WORLDMAP_EVENT_TYPE.WET_DIVE, myUserData.m_DiveGameData.DiveUid, out num);
					return;
				}
				if (myUserData.m_DiveGameData.Player.PlayerBase.State == NKMDivePlayerState.Annihilation)
				{
					int cityID = myUserData.m_WorldmapData.GetCityID(myUserData.m_DiveGameData.DiveUid);
					NKMWorldMapCityData cityData = myUserData.m_WorldmapData.GetCityData(cityID);
					if (cityData != null)
					{
						cityData.worldMapEventGroup.eventUid = 0L;
					}
				}
			}
		}

		// Token: 0x060058D7 RID: 22743 RVA: 0x001AE3EC File Offset: 0x001AC5EC
		public static void OnRecv(NKMPacket_DIVE_SUICIDE_ACK cNKMPacket_DIVE_SUICIDE_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_DIVE_SUICIDE_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null || myUserData.m_DiveGameData == null)
			{
				return;
			}
			NKCDiveGame.SetReservedUnitDieShow(true, myUserData.m_DiveGameData.Player.PlayerBase.ReservedDeckIndex, NKC_DIVE_GAME_UNIT_DIE_TYPE.NDGUDT_WARP);
			myUserData.m_DiveGameData.UpdateData(false, cNKMPacket_DIVE_SUICIDE_ACK.diveSyncData);
			if (myUserData.m_DiveGameData.Player.PlayerBase.State == NKMDivePlayerState.Annihilation)
			{
				NKCDiveGame.SetReservedUnitDieShow(false, -1, NKC_DIVE_GAME_UNIT_DIE_TYPE.NDGUDT_EXPLOSION);
				NKC_SCEN_DIVE_RESULT nkc_SCEN_DIVE_RESULT = NKCScenManager.GetScenManager().Get_NKC_SCEN_DIVE_RESULT();
				if (nkc_SCEN_DIVE_RESULT != null)
				{
					nkc_SCEN_DIVE_RESULT.SetData(false, myUserData.m_DiveGameData.Floor.Templet.IsEventDive, cNKMPacket_DIVE_SUICIDE_ACK.diveSyncData.RewardData, cNKMPacket_DIVE_SUICIDE_ACK.diveSyncData.ArtifactRewardData, cNKMPacket_DIVE_SUICIDE_ACK.diveSyncData.StormMiscReward, null, new NKMDeckIndex(NKM_DECK_TYPE.NDT_DIVE, myUserData.m_DiveGameData.Player.PlayerBase.LeaderDeckIndex), null);
				}
				NKCPacketHandlersLobby.ProcessWorldmapContentsAfterDiveEnd();
				myUserData.ClearDiveGameData();
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_DIVE_RESULT, true);
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_DIVE)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_DIVE().OnRecv(cNKMPacket_DIVE_SUICIDE_ACK);
			}
		}

		// Token: 0x060058D8 RID: 22744 RVA: 0x001AE518 File Offset: 0x001AC718
		public static void OnRecv(NKMPacket_DIVE_SELECT_ARTIFACT_ACK cNKMPacket_DIVE_SELECT_ARTIFACT_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_DIVE_SELECT_ARTIFACT_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null || myUserData.m_DiveGameData == null)
			{
				return;
			}
			myUserData.m_DiveGameData.UpdateData(false, cNKMPacket_DIVE_SELECT_ARTIFACT_ACK.diveSyncData);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_DIVE)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_DIVE().OnRecv(cNKMPacket_DIVE_SELECT_ARTIFACT_ACK);
			}
		}

		// Token: 0x060058D9 RID: 22745 RVA: 0x001AE584 File Offset: 0x001AC784
		public static void OnRecv(NKMPacket_DIVE_START_ACK cNKMPacket_DIVE_START_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_DIVE_START_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.m_DiveGameData = cNKMPacket_DIVE_START_ACK.diveGameData;
			myUserData.m_InventoryData.UpdateItemInfo(cNKMPacket_DIVE_START_ACK.costItemDataList);
			if (cNKMPacket_DIVE_START_ACK.cityID != 0)
			{
				NKMWorldMapCityData cityData = myUserData.m_WorldmapData.GetCityData(cNKMPacket_DIVE_START_ACK.cityID);
				if (cityData != null)
				{
					cityData.worldMapEventGroup.eventUid = cNKMPacket_DIVE_START_ACK.diveGameData.DiveUid;
					NKMWorldMapEventTemplet nkmworldMapEventTemplet = NKMWorldMapEventTemplet.Find(cityData.worldMapEventGroup.worldmapEventID);
					if (nkmworldMapEventTemplet.eventType != NKM_WORLDMAP_EVENT_TYPE.WET_DIVE)
					{
						Debug.LogError("FATAL : Dive event가 없는 도시의 dive에 진입 성공. 서버 로직 에러");
					}
					if (nkmworldMapEventTemplet.stageID != cNKMPacket_DIVE_START_ACK.diveGameData.Floor.Templet.StageID)
					{
						Debug.LogError("FATAL : Dive event가 지정하는 dive stage와 다른 dive에 진입. 서버 로직 에러");
					}
				}
			}
			NKMDiveGameManager.UpdateAllDiveDeckState(NKM_DECK_STATE.DECK_STATE_DIVE, myUserData);
			NKCDiveGame.SetReservedUnitDieShow(false, -1, NKC_DIVE_GAME_UNIT_DIE_TYPE.NDGUDT_EXPLOSION);
			NKCScenManager.GetScenManager().Get_NKC_SCEN_DIVE().SetIntro(true);
			NKCScenManager.GetScenManager().Get_NKC_SCEN_DIVE().SetSectorAddEventWhenStart(true);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_DIVE, true);
		}

		// Token: 0x060058DA RID: 22746 RVA: 0x001AE688 File Offset: 0x001AC888
		public static void OnRecv(NKMPacket_DIVE_MOVE_FORWARD_ACK cNKMPacket_DIVE_MOVE_FORWARD_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_DIVE_MOVE_FORWARD_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (cNKMPacket_DIVE_MOVE_FORWARD_ACK.diveSyncData != null && cNKMPacket_DIVE_MOVE_FORWARD_ACK.diveSyncData.RewardData != null)
			{
				NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
				if (myUserData != null)
				{
					myUserData.GetReward(cNKMPacket_DIVE_MOVE_FORWARD_ACK.diveSyncData.RewardData);
				}
			}
			if (cNKMPacket_DIVE_MOVE_FORWARD_ACK.diveSyncData == null)
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKMDiveGameData nkmdiveGameData = (nkmuserData != null) ? nkmuserData.m_DiveGameData : null;
			if (nkmdiveGameData == null)
			{
				return;
			}
			NKMDiveSyncData diveSyncData = cNKMPacket_DIVE_MOVE_FORWARD_ACK.diveSyncData;
			for (int i = 0; i < diveSyncData.UpdatedSlots.Count; i++)
			{
				NKMDiveSlotWithIndexes nkmdiveSlotWithIndexes = diveSyncData.UpdatedSlots[i];
				if (nkmdiveSlotWithIndexes != null)
				{
					NKMDiveSlot slot = nkmdiveGameData.Floor.GetSlot(nkmdiveSlotWithIndexes.SlotSetIndex, nkmdiveSlotWithIndexes.SlotIndex);
					if (slot != null)
					{
						slot.DeepCopyFrom(nkmdiveSlotWithIndexes.Slot);
					}
				}
			}
			if (diveSyncData.UpdatedPlayer != null && (diveSyncData.UpdatedPlayer.State == NKMDivePlayerState.Exploring || diveSyncData.UpdatedPlayer.State == NKMDivePlayerState.SelectArtifact))
			{
				nkmdiveGameData.Floor.Rebuild(nkmdiveGameData.Player.PlayerBase.Distance, nkmdiveGameData.Player.GetNextSlotSetIndex(), diveSyncData.UpdatedPlayer.SlotIndex);
			}
			if (diveSyncData.UpdatedPlayer != null)
			{
				nkmdiveGameData.Player.PlayerBase.DeepCopyFromSource(diveSyncData.UpdatedPlayer);
			}
			if (diveSyncData.UpdatedSquads.Count > 0)
			{
				for (int j = 0; j < diveSyncData.UpdatedSquads.Count; j++)
				{
					NKMDiveSquad nkmdiveSquad = diveSyncData.UpdatedSquads[j];
					if (nkmdiveSquad != null)
					{
						NKMDiveSquad nkmdiveSquad2 = null;
						nkmdiveGameData.Player.Squads.TryGetValue(nkmdiveSquad.DeckIndex, out nkmdiveSquad2);
						if (nkmdiveSquad2 != null)
						{
							nkmdiveSquad2.DeepCopyFromSource(nkmdiveSquad);
						}
					}
				}
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_DIVE)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_DIVE().OnRecv(cNKMPacket_DIVE_MOVE_FORWARD_ACK);
			}
		}

		// Token: 0x060058DB RID: 22747 RVA: 0x001AE850 File Offset: 0x001ACA50
		public static void OnRecv(NKMPacket_DIVE_GIVE_UP_ACK cNKMPacket_DIVE_GIVE_UP_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_DIVE_GIVE_UP_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			bool bEventDive = false;
			if (myUserData != null)
			{
				if (myUserData.m_DiveGameData != null && myUserData.m_DiveGameData.Floor.Templet.IsEventDive)
				{
					bEventDive = true;
					int cityID = myUserData.m_WorldmapData.GetCityID(myUserData.m_DiveGameData.DiveUid);
					NKMWorldMapCityData cityData = myUserData.m_WorldmapData.GetCityData(cityID);
					if (cityData != null)
					{
						cityData.worldMapEventGroup.eventUid = 0L;
					}
				}
				myUserData.ClearDiveGameData();
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_DIVE)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_DIVE().OnRecv(cNKMPacket_DIVE_GIVE_UP_ACK, bEventDive);
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_DIVE_READY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_DIVE_READY().OnRecv(cNKMPacket_DIVE_GIVE_UP_ACK);
			}
		}

		// Token: 0x060058DC RID: 22748 RVA: 0x001AE91C File Offset: 0x001ACB1C
		public static void OnRecv(NKMPacket_DIVE_AUTO_ACK cNKMPacket_DIVE_AUTO_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_DIVE_AUTO_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null && myUserData.m_UserOption != null)
			{
				myUserData.m_UserOption.m_bAutoDive = cNKMPacket_DIVE_AUTO_ACK.isAuto;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_DIVE)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_DIVE().OnRecv(cNKMPacket_DIVE_AUTO_ACK);
			}
		}

		// Token: 0x060058DD RID: 22749 RVA: 0x001AE984 File Offset: 0x001ACB84
		public static void OnRecv(NKMPacket_DIVE_EXPIRE_NOT cNKMPacket_DIVE_EXPIRE_NOT)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				NKMDiveGameData diveGameData = myUserData.m_DiveGameData;
				if (diveGameData != null && diveGameData.Floor.Templet.StageID == cNKMPacket_DIVE_EXPIRE_NOT.stageID)
				{
					myUserData.ClearDiveGameData();
					NKCUtil.ProcessDiveExpireTime();
					NKM_SCEN_ID nowScenID = NKCScenManager.GetScenManager().GetNowScenID();
					if (nowScenID == NKM_SCEN_ID.NSI_HOME)
					{
						NKCScenManager.GetScenManager().Get_SCEN_HOME().OnRecv(cNKMPacket_DIVE_EXPIRE_NOT);
						return;
					}
					switch (nowScenID)
					{
					case NKM_SCEN_ID.NSI_WORLDMAP:
						NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().OnRecv(cNKMPacket_DIVE_EXPIRE_NOT);
						break;
					case NKM_SCEN_ID.NSI_CUTSCENE_DUNGEON:
					case NKM_SCEN_ID.NSI_GAME_RESULT:
						break;
					case NKM_SCEN_ID.NSI_DIVE_READY:
						NKCScenManager.GetScenManager().Get_NKC_SCEN_DIVE_READY().OnRecv(cNKMPacket_DIVE_EXPIRE_NOT);
						return;
					case NKM_SCEN_ID.NSI_DIVE:
						NKCScenManager.GetScenManager().Get_NKC_SCEN_DIVE().OnRecv(cNKMPacket_DIVE_EXPIRE_NOT);
						return;
					default:
						return;
					}
				}
			}
		}

		// Token: 0x060058DE RID: 22750 RVA: 0x001AEA44 File Offset: 0x001ACC44
		public static void OnRecv(NKMPacket_DIVE_SKIP_ACK cNKMPacket_DIVE_SKIP_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_DIVE_SKIP_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				if (cNKMPacket_DIVE_SKIP_ACK.rewardDatas != null)
				{
					foreach (NKMRewardData rewardData in cNKMPacket_DIVE_SKIP_ACK.rewardDatas)
					{
						myUserData.GetReward(rewardData);
					}
				}
				myUserData.m_InventoryData.UpdateItemInfo(cNKMPacket_DIVE_SKIP_ACK.costItems);
			}
			NKCPopupOpSkipProcess.Instance.Open(cNKMPacket_DIVE_SKIP_ACK.rewardDatas, null, NKCUtilString.GET_STRING_DIVE_SAFE_MINING_RESULT);
		}

		// Token: 0x060058DF RID: 22751 RVA: 0x001AEAEC File Offset: 0x001ACCEC
		public static void OnRecv(NKMPacket_RAID_RESULT_LIST_ACK cNKMPacket_RAID_RESULT_LIST_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_RAID_RESULT_LIST_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.GetScenManager().GetNKCRaidDataMgr().SetDataList(cNKMPacket_RAID_RESULT_LIST_ACK.raidResultDataList);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WORLDMAP)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().OnRecv(cNKMPacket_RAID_RESULT_LIST_ACK);
			}
		}

		// Token: 0x060058E0 RID: 22752 RVA: 0x001AEB41 File Offset: 0x001ACD41
		public static void OnRecv(NKMPacket_RAID_COOP_LIST_ACK cNKMPacket_RAID_COOP_LIST_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_RAID_COOP_LIST_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WORLDMAP)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().OnRecv(cNKMPacket_RAID_COOP_LIST_ACK);
			}
		}

		// Token: 0x060058E1 RID: 22753 RVA: 0x001AEB78 File Offset: 0x001ACD78
		public static void OnRecv(NKMPacket_RAID_SET_COOP_ACK cNKMPacket_RAID_SET_COOP_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_RAID_SET_COOP_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.GetScenManager().GetNKCRaidDataMgr().SetRaidCoopOn(cNKMPacket_RAID_SET_COOP_ACK.raidUID, cNKMPacket_RAID_SET_COOP_ACK.raidJoinDataList);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_RAID)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_RAID().ResetUI();
			}
		}

		// Token: 0x060058E2 RID: 22754 RVA: 0x001AEBD4 File Offset: 0x001ACDD4
		public static void OnRecv(NKMPacket_RAID_RESULT_ACCEPT_ACK cNKMPacket_RAID_RESULT_ACCEPT_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_RAID_RESULT_ACCEPT_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			int cityID = -1;
			if (nkmuserData != null)
			{
				nkmuserData.m_WorldmapData.RemoveEvent(NKM_WORLDMAP_EVENT_TYPE.WET_RAID, cNKMPacket_RAID_RESULT_ACCEPT_ACK.raidUID, out cityID);
			}
			if (nkmuserData != null && cNKMPacket_RAID_RESULT_ACCEPT_ACK.rewardData != null)
			{
				nkmuserData.GetReward(cNKMPacket_RAID_RESULT_ACCEPT_ACK.rewardData);
				NKCUIResult.Instance.OpenComplexResult(nkmuserData.m_ArmyData, cNKMPacket_RAID_RESULT_ACCEPT_ACK.rewardData, delegate
				{
					NKCPacketHandlersLobby.OnRaidReward(cNKMPacket_RAID_RESULT_ACCEPT_ACK, cityID);
				}, 0L, null, false, false);
				return;
			}
			NKCPacketHandlersLobby.OnRaidReward(cNKMPacket_RAID_RESULT_ACCEPT_ACK, cityID);
		}

		// Token: 0x060058E3 RID: 22755 RVA: 0x001AEC98 File Offset: 0x001ACE98
		private static void OnRaidReward(NKMPacket_RAID_RESULT_ACCEPT_ACK cNKMPacket_RAID_RESULT_ACCEPT_ACK, int cityID)
		{
			NKCScenManager.CurrentUserData();
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WORLDMAP)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().OnRecv(cNKMPacket_RAID_RESULT_ACCEPT_ACK, cityID);
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_RAID || NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_RAID_READY)
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_WORLDMAP, true);
			}
		}

		// Token: 0x060058E4 RID: 22756 RVA: 0x001AECF4 File Offset: 0x001ACEF4
		public static void OnRecv(NKMPacket_RAID_RESULT_ACCEPT_ALL_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			List<int> list = new List<int>();
			foreach (long eventUID in sPacket.raidUids)
			{
				int item = -1;
				if (nkmuserData != null)
				{
					nkmuserData.m_WorldmapData.RemoveEvent(NKM_WORLDMAP_EVENT_TYPE.WET_RAID, eventUID, out item);
					list.Add(item);
				}
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WORLDMAP)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().OnRecv(sPacket, list);
			}
			if (nkmuserData != null && sPacket.rewardData != null)
			{
				nkmuserData.GetReward(sPacket.rewardData);
				NKCUIResult.Instance.OpenComplexResult(nkmuserData.m_ArmyData, sPacket.rewardData, null, 0L, null, false, false);
			}
		}

		// Token: 0x060058E5 RID: 22757 RVA: 0x001AEDD4 File Offset: 0x001ACFD4
		public static void OnRecv(NKMPacket_MY_RAID_LIST_ACK cNKMPacket_MY_RAID_LIST_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_MY_RAID_LIST_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.GetScenManager().GetNKCRaidDataMgr().SetDataList(cNKMPacket_MY_RAID_LIST_ACK.myRaidDataList);
			for (int i = 0; i < cNKMPacket_MY_RAID_LIST_ACK.myRaidDataList.Count; i++)
			{
				NKMRaidTemplet nkmraidTemplet = NKMRaidTemplet.Find(cNKMPacket_MY_RAID_LIST_ACK.myRaidDataList[i].stageID);
				if (nkmraidTemplet != null && nkmraidTemplet.DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_SOLO_RAID)
				{
					NKCPacketSender.Send_NKMPacket_RAID_DETAIL_INFO_REQ(cNKMPacket_MY_RAID_LIST_ACK.myRaidDataList[i].raidUID);
				}
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WORLDMAP)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().OnRecv(cNKMPacket_MY_RAID_LIST_ACK);
			}
		}

		// Token: 0x060058E6 RID: 22758 RVA: 0x001AEE80 File Offset: 0x001AD080
		public static void OnRecv(NKMPacket_RAID_DETAIL_INFO_ACK cNKMPacket_RAID_DETAIL_INFO_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_RAID_DETAIL_INFO_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.GetScenManager().GetNKCRaidDataMgr().SetData(cNKMPacket_RAID_DETAIL_INFO_ACK.raidDetailData);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_RAID)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_RAID().OnRecv(cNKMPacket_RAID_DETAIL_INFO_ACK);
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WORLDMAP)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().OnRecv(cNKMPacket_RAID_DETAIL_INFO_ACK);
			}
		}

		// Token: 0x060058E7 RID: 22759 RVA: 0x001AEEF4 File Offset: 0x001AD0F4
		public static void OnRecv(NKMPacket_RAID_POINT_REWARD_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.CurrentUserData().GetReward(sPacket.rewardData);
			NKCUIResult.Instance.OpenRewardGain(NKCScenManager.CurrentUserData().m_ArmyData, sPacket.rewardData, "", "", null);
		}

		// Token: 0x060058E8 RID: 22760 RVA: 0x001AEF4C File Offset: 0x001AD14C
		public static void OnRecv(NKMPacket_RAID_SEASON_NOT sPacket)
		{
			if (sPacket.raidSeason == null)
			{
				return;
			}
			NKCRaidSeasonManager.RaidSeason = sPacket.raidSeason;
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WORLDMAP)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().OnRecv(sPacket);
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_RAID)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_RAID().ResetUI();
			}
		}

		// Token: 0x060058E9 RID: 22761 RVA: 0x001AEFAC File Offset: 0x001AD1AC
		public static void OnRecv(NKMPacket_SET_UNIT_SKIN_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			NKMUnitData unitFromUID = armyData.GetUnitFromUID(sPacket.unitUID);
			if (unitFromUID != null)
			{
				unitFromUID.m_SkinID = sPacket.skinID;
			}
			armyData.UpdateData(unitFromUID);
		}

		// Token: 0x060058EA RID: 22762 RVA: 0x001AF000 File Offset: 0x001AD200
		public static void OnRecv(NKMPacket_CONTRACT_PERMANENTLY_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItemData);
			NKMArmyData armyData = myUserData.m_ArmyData;
			NKMUnitData unitFromUID = armyData.GetUnitFromUID(sPacket.unitUID);
			if (unitFromUID != null)
			{
				unitFromUID.SetPermanentContract();
				armyData.UpdateData(unitFromUID);
			}
			if (NKCUILifetime.IsInstanceOpen)
			{
				NKCUILifetime.Instance.PlayAni();
			}
		}

		// Token: 0x060058EB RID: 22763 RVA: 0x001AF074 File Offset: 0x001AD274
		public static void OnRecv(NKMPacket_EXCHANGE_PIECE_TO_UNIT_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItemData);
			NKMRewardData nkmrewardData = new NKMRewardData();
			nkmrewardData.UnitDataList.AddRange(sPacket.unitDataList);
			myUserData.GetReward(nkmrewardData);
			NKCUIResult.Instance.OpenRewardGain(NKCScenManager.CurrentUserData().m_ArmyData, nkmrewardData, NKCStringTable.GetString("SI_PF_PERSONNEL_SCOUT_TEXT", false), "", null);
			if (NKCUIScout.IsInstanceOpen)
			{
				NKCUIScout.Instance.Refresh();
			}
			NKMPieceTemplet nkmpieceTemplet = NKMTempletContainer<NKMPieceTemplet>.Find(sPacket.costItemData.ItemID);
			if (nkmpieceTemplet != null)
			{
				long num = (long)(myUserData.m_ArmyData.IsCollectedUnit(nkmpieceTemplet.m_PieceGetUintId) ? nkmpieceTemplet.m_PieceReq : nkmpieceTemplet.m_PieceReqFirst);
				if (myUserData.m_InventoryData.GetCountMiscItem(nkmpieceTemplet.m_PieceId) < num)
				{
					NKCUIScout.UnregisgerAlarmOff(nkmpieceTemplet.Key);
				}
			}
		}

		// Token: 0x060058EC RID: 22764 RVA: 0x001AF164 File Offset: 0x001AD364
		public static void OnRecv(NKMPacket_UNIT_REVIEW_COMMENT_AND_SCORE_ACK sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (sPacket.bestUnitReviewCommentDataList != null)
			{
				for (int i = 0; i < sPacket.bestUnitReviewCommentDataList.Count; i++)
				{
					sPacket.bestUnitReviewCommentDataList[i].content = NKCFilterManager.CheckBadChat(sPacket.bestUnitReviewCommentDataList[i].content);
				}
			}
			if (sPacket.unitReviewCommentDataList != null)
			{
				for (int j = 0; j < sPacket.unitReviewCommentDataList.Count; j++)
				{
					sPacket.unitReviewCommentDataList[j].content = NKCFilterManager.CheckBadChat(sPacket.unitReviewCommentDataList[j].content);
				}
			}
			if (sPacket.myUnitReviewCommentData != null && !string.IsNullOrEmpty(sPacket.myUnitReviewCommentData.content))
			{
				sPacket.myUnitReviewCommentData.content = NKCFilterManager.CheckBadChat(sPacket.myUnitReviewCommentData.content);
			}
			if (NKCUIUnitReview.IsInstanceOpen)
			{
				NKCUIUnitReview.Instance.RecvReviewData(sPacket);
			}
		}

		// Token: 0x060058ED RID: 22765 RVA: 0x001AF25C File Offset: 0x001AD45C
		public static void OnRecv(NKMPacket_UNIT_REVIEW_COMMENT_LIST_ACK sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (sPacket.unitReviewCommentDataList != null)
			{
				for (int i = 0; i < sPacket.unitReviewCommentDataList.Count; i++)
				{
					sPacket.unitReviewCommentDataList[i].content = NKCFilterManager.CheckBadChat(sPacket.unitReviewCommentDataList[i].content);
				}
			}
			if (NKCUIUnitReview.IsInstanceOpen)
			{
				NKCUIUnitReview.Instance.RecvCommentList(sPacket.unitReviewCommentDataList);
			}
		}

		// Token: 0x060058EE RID: 22766 RVA: 0x001AF2E0 File Offset: 0x001AD4E0
		public static void OnRecv(NKMPacket_UNIT_REVIEW_COMMENT_WRITE_ACK sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (sPacket.myUnitReviewCommentData != null && !string.IsNullOrEmpty(sPacket.myUnitReviewCommentData.content))
			{
				sPacket.myUnitReviewCommentData.content = NKCFilterManager.CheckBadChat(sPacket.myUnitReviewCommentData.content);
			}
			if (NKCUIUnitReview.IsInstanceOpen)
			{
				NKCUIUnitReview.Instance.RecvMyCommentChanged(sPacket.myUnitReviewCommentData);
			}
		}

		// Token: 0x060058EF RID: 22767 RVA: 0x001AF353 File Offset: 0x001AD553
		public static void OnRecv(NKMPacket_UNIT_REVIEW_COMMENT_DELETE_ACK sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCUIUnitReview.IsInstanceOpen)
			{
				NKCUIUnitReview.Instance.RecvMyCommentChanged(null);
			}
		}

		// Token: 0x060058F0 RID: 22768 RVA: 0x001AF384 File Offset: 0x001AD584
		public static void OnRecv(NKMPacket_UNIT_REVIEW_COMMENT_VOTE_ACK sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			if (sPacket.errorCode == NKM_ERROR_CODE.NEC_FAIL_UNIT_REVIEW_COMMENT_NOT_EXIST)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_REVIEW_IS_ALREADY_DELETE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (sPacket.unitReviewCommentData != null && !string.IsNullOrEmpty(sPacket.unitReviewCommentData.content))
			{
				sPacket.unitReviewCommentData.content = NKCFilterManager.CheckBadChat(sPacket.unitReviewCommentData.content);
			}
			if (NKCUIUnitReview.IsInstanceOpen)
			{
				NKCUIUnitReview.Instance.RecvCommentVote(sPacket.unitID, sPacket.unitReviewCommentData, true);
			}
		}

		// Token: 0x060058F1 RID: 22769 RVA: 0x001AF420 File Offset: 0x001AD620
		public static void OnRecv(NKMPacket_UNIT_REVIEW_COMMENT_VOTE_CANCEL_ACK sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			if (sPacket.errorCode == NKM_ERROR_CODE.NEC_FAIL_UNIT_REVIEW_COMMENT_NOT_EXIST)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_REVIEW_IS_ALREADY_DELETE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (sPacket.unitReviewCommentData != null && !string.IsNullOrEmpty(sPacket.unitReviewCommentData.content))
			{
				sPacket.unitReviewCommentData.content = NKCFilterManager.CheckBadChat(sPacket.unitReviewCommentData.content);
			}
			if (NKCUIUnitReview.IsInstanceOpen)
			{
				NKCUIUnitReview.Instance.RecvCommentVote(sPacket.unitID, sPacket.unitReviewCommentData, false);
			}
		}

		// Token: 0x060058F2 RID: 22770 RVA: 0x001AF4BC File Offset: 0x001AD6BC
		public static void OnRecv(NKMPacket_UNIT_REVIEW_SCORE_VOTE_ACK sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			if (sPacket.errorCode == NKM_ERROR_CODE.NEC_FAIL_UNIT_REVIEW_SCORE_INTERVAL_HAS_NOT_ELAPSED)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString(sPacket.errorCode), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCUIUnitReview.IsInstanceOpen)
			{
				NKCUIUnitReview.Instance.RecvScoreVoteAck(sPacket.unitID, sPacket.unitReviewScoreData);
			}
		}

		// Token: 0x060058F3 RID: 22771 RVA: 0x001AF527 File Offset: 0x001AD727
		public static void OnRecv(NKMPacket_UNIT_REVIEW_USER_BAN_ACK sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCUnitReviewManager.AddBanList(sPacket.targetUserUid);
			if (NKCUIUnitReview.IsInstanceOpen)
			{
				NKCUIUnitReview.Instance.RefreshUI();
			}
		}

		// Token: 0x060058F4 RID: 22772 RVA: 0x001AF55F File Offset: 0x001AD75F
		public static void OnRecv(NKMPacket_UNIT_REVIEW_USER_BAN_CANCEL_ACK sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCUnitReviewManager.RemoveBanList(sPacket.targetUserUid);
			if (NKCUIUnitReview.IsInstanceOpen)
			{
				NKCUIUnitReview.Instance.RefreshUI();
			}
		}

		// Token: 0x060058F5 RID: 22773 RVA: 0x001AF597 File Offset: 0x001AD797
		public static void OnRecv(NKMPacket_UNIT_REVIEW_USER_BAN_LIST_ACK sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCUnitReviewManager.m_bReceivedUnitReviewBanList = true;
			NKCUnitReviewManager.SetBanList(sPacket.banishList);
			if (NKCUIUnitReview.IsInstanceOpen)
			{
				NKCUIUnitReview.Instance.OnRecvBanList();
			}
		}

		// Token: 0x060058F6 RID: 22774 RVA: 0x001AF5D8 File Offset: 0x001AD7D8
		public static void OnRecv(NKMPacket_NEGOTIATE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			nkmuserData.m_InventoryData.UpdateItemInfo(sPacket.costItemDataList);
			NKMUnitData unitFromUID = nkmuserData.m_ArmyData.GetUnitFromUID(sPacket.targetUnitUid);
			NKCNegotiateManager.NegotiateResultUIData uiData = NKCNegotiateManager.MakeResultUIData(nkmuserData, sPacket);
			if (unitFromUID != null)
			{
				unitFromUID.m_UnitLevel = sPacket.targetUnitLevel;
				unitFromUID.m_iUnitLevelEXP = sPacket.targetUnitExp;
				unitFromUID.loyalty = sPacket.targetUnitLoyalty;
			}
			if (NKCUIUnitInfo.IsInstanceOpen)
			{
				NKCUIUnitInfo.Instance.ReserveLevelUpFx(uiData);
			}
			nkmuserData.m_ArmyData.UpdateUnitData(unitFromUID);
		}

		// Token: 0x060058F7 RID: 22775 RVA: 0x001AF674 File Offset: 0x001AD874
		public static void OnRecv(NKMPacket_ATTENDANCE_NOT sPacket)
		{
			NKMPopUpBox.CloseWaitBox();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				Debug.LogWarning(string.Format("NKMPacket_ATTENDANCE_NOT ERROR - {0}", sPacket.errorCode));
				if (NKCUIAttendance.IsInstanceOpen)
				{
					NKCUIAttendance.Instance.Close();
				}
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCPacketHandlers.GetErrorMessage(sPacket.errorCode), null, "");
				if (sPacket.errorCode == NKM_ERROR_CODE.NEC_FAIL_SYSTEM_CONTENTS_BLOCK)
				{
					NKMAttendanceManager.SetContentBlock();
				}
				return;
			}
			NKC_SCEN_HOME scen_HOME = NKCScenManager.GetScenManager().Get_SCEN_HOME();
			if (scen_HOME != null)
			{
				scen_HOME.ReserveAttendanceData(sPacket.attendanceData, sPacket.lastUpdateDate);
			}
		}

		// Token: 0x060058F8 RID: 22776 RVA: 0x001AF713 File Offset: 0x001AD913
		public static void OnRecv(NKMPacket_USER_PROFILE_INFO_ACK cNKMPacket_USER_PROFILE_INFO_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_USER_PROFILE_INFO_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPopupFriendInfo.Instance.Open(cNKMPacket_USER_PROFILE_INFO_ACK.userProfileData, true);
		}

		// Token: 0x060058F9 RID: 22777 RVA: 0x001AF73C File Offset: 0x001AD93C
		public static void OnRecv(NKMPacket_GAME_OPTION_CHANGE_ACK cNKMPacket_GAME_OPTION_CHANGE_ACK)
		{
			NKMPopUpBox.CloseWaitBox();
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GAME_OPTION_CHANGE_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			NKMUserOption userOption = myUserData.m_UserOption;
			if (userOption == null)
			{
				return;
			}
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			if (userOption.m_bActionCamera != cNKMPacket_GAME_OPTION_CHANGE_ACK.isActionCamera)
			{
				userOption.m_bActionCamera = cNKMPacket_GAME_OPTION_CHANGE_ACK.isActionCamera;
				gameOptionData.SetUseActionCamera(cNKMPacket_GAME_OPTION_CHANGE_ACK.isActionCamera, true);
			}
			if (userOption.m_bTrackCamera != cNKMPacket_GAME_OPTION_CHANGE_ACK.isTrackCamera)
			{
				userOption.m_bTrackCamera = cNKMPacket_GAME_OPTION_CHANGE_ACK.isTrackCamera;
				gameOptionData.SetUseTrackCamera(cNKMPacket_GAME_OPTION_CHANGE_ACK.isTrackCamera, true);
			}
			if (userOption.m_bViewSkillCutIn != cNKMPacket_GAME_OPTION_CHANGE_ACK.isViewSkillCutIn)
			{
				userOption.m_bViewSkillCutIn = cNKMPacket_GAME_OPTION_CHANGE_ACK.isViewSkillCutIn;
				gameOptionData.SetViewSkillCutIn(cNKMPacket_GAME_OPTION_CHANGE_ACK.isViewSkillCutIn, true);
			}
			if (userOption.m_bDefaultPvpAutoRespawn != cNKMPacket_GAME_OPTION_CHANGE_ACK.defaultPvpAutoRespawn)
			{
				userOption.m_bDefaultPvpAutoRespawn = cNKMPacket_GAME_OPTION_CHANGE_ACK.defaultPvpAutoRespawn;
				gameOptionData.SetPvPAutoRespawn(cNKMPacket_GAME_OPTION_CHANGE_ACK.defaultPvpAutoRespawn, true);
			}
			if (userOption.m_bAutoSyncFriendDeck != cNKMPacket_GAME_OPTION_CHANGE_ACK.autoSyncFriendDeck)
			{
				userOption.m_bAutoSyncFriendDeck = cNKMPacket_GAME_OPTION_CHANGE_ACK.autoSyncFriendDeck;
				gameOptionData.SetAutoSyncFriendDeck(cNKMPacket_GAME_OPTION_CHANGE_ACK.autoSyncFriendDeck, true);
			}
			gameOptionData.Save();
			NKCUIGameOption.CheckInstanceAndClose();
		}

		// Token: 0x060058FA RID: 22778 RVA: 0x001AF85C File Offset: 0x001ADA5C
		public static void OnRecv(NKMPacket_CUTSCENE_DUNGEON_START_ACK sPacket)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				nkmuserData.UpdateStagePlayData(sPacket.stagePlayData);
			}
		}

		// Token: 0x060058FB RID: 22779 RVA: 0x001AF880 File Offset: 0x001ADA80
		public static void OnRecv(NKMPacket_UNIT_REVIEW_TAG_VOTE_CANCEL_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCCollectionManager.SetVote(sPacket.unitID, sPacket.unitReviewTagData.tagType, sPacket.unitReviewTagData.votedCount, sPacket.unitReviewTagData.isVoted);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_COLLECTION)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_COLLECTION().OnRecvReviewTagVoteCancelAck(sPacket);
				return;
			}
			if (NKCUICollectionUnitInfo.IsInstanceOpen)
			{
				NKCUICollectionUnitInfo.Instance.OnRecvReviewTagVoteCancelAck(sPacket);
			}
		}

		// Token: 0x060058FC RID: 22780 RVA: 0x001AF900 File Offset: 0x001ADB00
		public static void OnRecv(NKMPacket_UNIT_REVIEW_TAG_VOTE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCCollectionManager.SetVote(sPacket.unitID, sPacket.unitReviewTagData.tagType, sPacket.unitReviewTagData.votedCount, sPacket.unitReviewTagData.isVoted);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_COLLECTION)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_COLLECTION().OnRecvReviewTagVoteAck(sPacket);
				return;
			}
			if (NKCUICollectionUnitInfo.IsInstanceOpen)
			{
				NKCUICollectionUnitInfo.Instance.OnRecvReviewTagVoteAck(sPacket);
			}
		}

		// Token: 0x060058FD RID: 22781 RVA: 0x001AF980 File Offset: 0x001ADB80
		public static void OnRecv(NKMPacket_UNIT_REVIEW_TAG_LIST_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_COLLECTION)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_COLLECTION().OnRecvReviewTagListAck(sPacket);
				return;
			}
			if (NKCUICollectionUnitInfo.IsInstanceOpen)
			{
				NKCUICollectionUnitInfo.Instance.OnRecvReviewTagListAck(sPacket);
			}
		}

		// Token: 0x060058FE RID: 22782 RVA: 0x001AF9D3 File Offset: 0x001ADBD3
		public static void OnRecv(NKMPacket_TEAM_COLLECTION_REWARD_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_COLLECTION)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_COLLECTION().OnRecvTeamCollectionRewardAck(sPacket);
			}
		}

		// Token: 0x060058FF RID: 22783 RVA: 0x001AFA08 File Offset: 0x001ADC08
		private static void CloseIngameEmoticonListPopup()
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME)
			{
				NKCGameClient gameClient = NKCScenManager.GetScenManager().GetGameClient();
				if (gameClient != null && gameClient.GetGameHud() != null && gameClient.GetGameHud().GetNKCGameHudEmoticon() != null && gameClient.GetGameHud().GetNKCGameHudEmoticon().IsNKCPopupInGameEmoticonOpen)
				{
					NKCPopupInGameEmoticon nkcpopupInGameEmoticon = gameClient.GetGameHud().GetNKCGameHudEmoticon().NKCPopupInGameEmoticon;
					if (nkcpopupInGameEmoticon == null)
					{
						return;
					}
					nkcpopupInGameEmoticon.Close();
				}
			}
		}

		// Token: 0x06005900 RID: 22784 RVA: 0x001AFA7D File Offset: 0x001ADC7D
		public static void OnRecv(NKMPacket_GAME_EMOTICON_ACK cNKMPacket_GAME_EMOTICON_ACK)
		{
			NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GAME_EMOTICON_ACK.errorCode, true, null, int.MinValue);
			NKCPacketHandlersLobby.CloseIngameEmoticonListPopup();
		}

		// Token: 0x06005901 RID: 22785 RVA: 0x001AFA98 File Offset: 0x001ADC98
		public static void OnRecv(NKMPacket_GAME_EMOTICON_NOT cNKMPacket_GAME_EMOTICON_NOT)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData != null && gameOptionData.UseEmoticonBlock)
			{
				return;
			}
			if (NKCReplayMgr.IsRecording())
			{
				NKCScenManager.GetScenManager().GetNKCReplayMgr().FillReplayData(cNKMPacket_GAME_EMOTICON_NOT);
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME)
			{
				NKCGameClient gameClient = NKCScenManager.GetScenManager().GetGameClient();
				if (gameClient != null && gameClient.GetGameHud() != null && gameClient.GetGameHud().GetNKCGameHudEmoticon() != null)
				{
					gameClient.GetGameHud().GetNKCGameHudEmoticon().OnRecv(cNKMPacket_GAME_EMOTICON_NOT);
				}
			}
		}

		// Token: 0x06005902 RID: 22786 RVA: 0x001AFB20 File Offset: 0x001ADD20
		public static void OnRecv(NKMPacket_EMOTICON_DATA_ACK cNKMPacket_EMOTICON_DATA_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_EMOTICON_DATA_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCEmoticonManager.m_bReceivedEmoticonData = true;
			NKCEmoticonManager.m_lstAniPreset = cNKMPacket_EMOTICON_DATA_ACK.presetData.animationList;
			NKCEmoticonManager.m_lstTextPreset = cNKMPacket_EMOTICON_DATA_ACK.presetData.textList;
			NKCEmoticonManager.m_hsEmoticonCollection = cNKMPacket_EMOTICON_DATA_ACK.collections;
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_LOBBY && NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().IsWaitForEmoticon())
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetWaitForEmoticon(false);
				NKCPopupEmoticonSetting.Instance.Open();
			}
		}

		// Token: 0x06005903 RID: 22787 RVA: 0x001AFBAC File Offset: 0x001ADDAC
		public static void OnRecv(NKMPacket_EMOTICON_ANI_CHANGE_ACK cNKMPacket_EMOTICON_ANI_CHANGE_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_EMOTICON_ANI_CHANGE_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (!NKCEmoticonManager.m_bReceivedEmoticonData)
			{
				return;
			}
			int presetIndex = cNKMPacket_EMOTICON_ANI_CHANGE_ACK.presetIndex;
			if (NKCEmoticonManager.m_lstAniPreset != null && NKCEmoticonManager.m_lstAniPreset.Count > presetIndex && presetIndex >= 0)
			{
				NKCEmoticonManager.m_lstAniPreset[presetIndex] = cNKMPacket_EMOTICON_ANI_CHANGE_ACK.emoticonId;
				if (NKCPopupEmoticonSetting.IsInstanceOpen)
				{
					NKCPopupEmoticonSetting.Instance.OnRecv(cNKMPacket_EMOTICON_ANI_CHANGE_ACK);
				}
			}
		}

		// Token: 0x06005904 RID: 22788 RVA: 0x001AFC18 File Offset: 0x001ADE18
		public static void OnRecv(NKMPacket_EMOTICON_TEXT_CHANGE_ACK cNKMPacket_EMOTICON_TEXT_CHANGE_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_EMOTICON_TEXT_CHANGE_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (!NKCEmoticonManager.m_bReceivedEmoticonData)
			{
				return;
			}
			int presetIndex = cNKMPacket_EMOTICON_TEXT_CHANGE_ACK.presetIndex;
			if (NKCEmoticonManager.m_lstTextPreset != null && NKCEmoticonManager.m_lstTextPreset.Count > presetIndex && presetIndex >= 0)
			{
				NKCEmoticonManager.m_lstTextPreset[presetIndex] = cNKMPacket_EMOTICON_TEXT_CHANGE_ACK.emoticonId;
				if (NKCPopupEmoticonSetting.IsInstanceOpen)
				{
					NKCPopupEmoticonSetting.Instance.OnRecv(cNKMPacket_EMOTICON_TEXT_CHANGE_ACK);
				}
			}
		}

		// Token: 0x06005905 RID: 22789 RVA: 0x001AFC84 File Offset: 0x001ADE84
		public static void OnRecv(NKMPacket_GAME_LOAD_COMPLETE_ACK cNKMPacket_GAME_LOAD_COMPLETE_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GAME_LOAD_COMPLETE_ACK.errorCode, true, null, -2147483648))
			{
				if (cNKMPacket_GAME_LOAD_COMPLETE_ACK.errorCode == NKM_ERROR_CODE.NEC_FAIL_GAME_LOAD_INVALID_STATE)
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				}
				return;
			}
			Debug.Log(string.Format("[NKMPacket_GAME_LOAD_COMPLETE_ACK] isIntrude : {0}", cNKMPacket_GAME_LOAD_COMPLETE_ACK.isIntrude));
			if (NKCReplayMgr.IsReplayRecordingOpened())
			{
				NKMGameData gameData = NKCScenManager.GetScenManager().GetGameClient().GetGameData();
				if (gameData.IsPVP() && !cNKMPacket_GAME_LOAD_COMPLETE_ACK.isIntrude)
				{
					Debug.Log("[NKMPacket_GAME_LOAD_COMPLETE_ACK] CreateNewReplayData");
					NKCScenManager.GetScenManager().GetNKCReplayMgr().CreateNewReplayData(gameData, cNKMPacket_GAME_LOAD_COMPLETE_ACK.gameRuntimeData);
				}
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME)
			{
				NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_GAME_LOAD_COMPLETE_ACK);
			}
		}

		// Token: 0x06005906 RID: 22790 RVA: 0x001AFD40 File Offset: 0x001ADF40
		public static void OnRecv(NKMPacket_GAME_START_NOT cNKMPacket_GAME_START_NOT)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME)
			{
				if (NKCScenManager.GetScenManager().GetGameClient().GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_WARFARE)
				{
					NKCScenManager.GetScenManager().WarfareGameData.warfareGameState = NKM_WARFARE_GAME_STATE.NWGS_INGAME_PLAYING;
				}
				NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_GAME_START_NOT);
			}
		}

		// Token: 0x06005907 RID: 22791 RVA: 0x001AFD94 File Offset: 0x001ADF94
		public static void OnRecv(NKMPacket_GAME_INTRUDE_START_NOT cNKMPacket_GAME_INTRUDE_START_NOT)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME)
			{
				if (NKCScenManager.GetScenManager().GetGameClient().GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_WARFARE)
				{
					NKCScenManager.GetScenManager().WarfareGameData.warfareGameState = NKM_WARFARE_GAME_STATE.NWGS_INGAME_PLAYING;
				}
				NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_GAME_INTRUDE_START_NOT);
			}
		}

		// Token: 0x06005908 RID: 22792 RVA: 0x001AFDE5 File Offset: 0x001ADFE5
		public static void OnRecv(NKMPacket_GAME_PAUSE_ACK cNKMPacket_GAME_PAUSE_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_GAME_PAUSE_ACK);
			NKCPacketObjectPool.CloseObject(cNKMPacket_GAME_PAUSE_ACK);
		}

		// Token: 0x06005909 RID: 22793 RVA: 0x001AFE0B File Offset: 0x001AE00B
		public static void OnRecv(NKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT cPacket_NPT_GAME_SYNC_DATA_PACK_NOT)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				NKCPacketObjectPool.CloseObject(cPacket_NPT_GAME_SYNC_DATA_PACK_NOT);
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cPacket_NPT_GAME_SYNC_DATA_PACK_NOT);
			NKCPacketObjectPool.CloseObject(cPacket_NPT_GAME_SYNC_DATA_PACK_NOT);
		}

		// Token: 0x0600590A RID: 22794 RVA: 0x001AFE37 File Offset: 0x001AE037
		public static void OnRecv(NKMPacket_GAME_DEV_RESPAWN_ACK cNKMPacket_GAME_DEV_RESPAWN_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_GAME_DEV_RESPAWN_ACK);
		}

		// Token: 0x0600590B RID: 22795 RVA: 0x001AFE57 File Offset: 0x001AE057
		public static void OnRecv(NKMPacket_GAME_CHECK_DIE_UNIT_ACK cNKMPacket_GAME_CHECK_DIE_UNIT_ACK)
		{
			NKCScenManager.GetScenManager().GetNowScenID();
		}

		// Token: 0x0600590C RID: 22796 RVA: 0x001AFE66 File Offset: 0x001AE066
		public static void OnRecv(NKMPacket_GAME_RESPAWN_ACK cPacket_GAME_RESPAWN_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cPacket_GAME_RESPAWN_ACK);
		}

		// Token: 0x0600590D RID: 22797 RVA: 0x001AFE86 File Offset: 0x001AE086
		public static void OnRecv(NKMPacket_GAME_SHIP_SKILL_ACK cPacket_GAME_SHIP_SKILL_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cPacket_GAME_SHIP_SKILL_ACK);
		}

		// Token: 0x0600590E RID: 22798 RVA: 0x001AFEA6 File Offset: 0x001AE0A6
		public static void OnRecv(NKMPacket_GAME_TACTICAL_COMMAND_ACK cNKMPacket_GAME_TACTICAL_COMMAND_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_GAME_TACTICAL_COMMAND_ACK);
		}

		// Token: 0x0600590F RID: 22799 RVA: 0x001AFEC6 File Offset: 0x001AE0C6
		public static void OnRecv(NKMPacket_GAME_AUTO_RESPAWN_ACK cPacket_GAME_AUTO_RESPAWN_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cPacket_GAME_AUTO_RESPAWN_ACK);
		}

		// Token: 0x06005910 RID: 22800 RVA: 0x001AFEE6 File Offset: 0x001AE0E6
		public static void OnRecv(NKMPacket_GAME_UNIT_RETREAT_ACK cNKMPacket_GAME_UNIT_RETREAT_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_GAME_UNIT_RETREAT_ACK);
		}

		// Token: 0x06005911 RID: 22801 RVA: 0x001AFF06 File Offset: 0x001AE106
		public static void OnRecv(NKMPacket_GAME_SPEED_2X_ACK cNKMPacket_GAME_SPEED_2X_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_GAME_SPEED_2X_ACK);
			NKCPacketObjectPool.CloseObject(cNKMPacket_GAME_SPEED_2X_ACK);
		}

		// Token: 0x06005912 RID: 22802 RVA: 0x001AFF2C File Offset: 0x001AE12C
		public static void OnRecv(NKMPacket_GAME_AUTO_SKILL_CHANGE_ACK cNKMPacket_GAME_AUTO_SKILL_CHANGE_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_GAME_AUTO_SKILL_CHANGE_ACK);
			NKCPacketObjectPool.CloseObject(cNKMPacket_GAME_AUTO_SKILL_CHANGE_ACK);
		}

		// Token: 0x06005913 RID: 22803 RVA: 0x001AFF52 File Offset: 0x001AE152
		public static void OnRecv(NKMPacket_GAME_USE_UNIT_SKILL_ACK cNKMPacket_GAME_USE_UNIT_SKILL_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_GAME_USE_UNIT_SKILL_ACK);
			NKCPacketObjectPool.CloseObject(cNKMPacket_GAME_USE_UNIT_SKILL_ACK);
		}

		// Token: 0x06005914 RID: 22804 RVA: 0x001AFF78 File Offset: 0x001AE178
		public static void OnRecv(NKMPacket_RECONNECT_SERVER_INFO_NOT not)
		{
			NKCConnectGame connectGame = NKCScenManager.GetScenManager().GetConnectGame();
			connectGame.SetRemoteAddress(not.serverIp, not.port);
			connectGame.SetAccessToken(not.accessToken);
			connectGame.ResetConnection();
			connectGame.ConnectToLobbyServer();
		}

		// Token: 0x06005915 RID: 22805 RVA: 0x001AFFAD File Offset: 0x001AE1AD
		public static void OnRecv(NKMPacket_COMMON_FAIL_ACK ack)
		{
			NKCPacketHandlers.Check_NKM_ERROR_CODE(ack.errorCode, true, null, int.MinValue);
		}

		// Token: 0x06005916 RID: 22806 RVA: 0x001AFFC4 File Offset: 0x001AE1C4
		public static void OnRecv(NKMPacket_UPDATE_NICKNAME_NOT sPacket)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				nkmuserData.m_UserNickName = sPacket.nickname;
			}
		}

		// Token: 0x06005917 RID: 22807 RVA: 0x001AFFE8 File Offset: 0x001AE1E8
		public static void OnRecv(NKMPacket_EXIT_APP_NOT sPacket)
		{
			if (NKCScenManager.GetScenManager() == null)
			{
				return;
			}
			NKCScenManager.GetScenManager().GetConnectLogin().SetEnable(false);
			NKCScenManager.GetScenManager().GetConnectGame().SetEnable(false);
			NKCScenManager.GetScenManager().GetConnectLogin().ResetConnection();
			NKCScenManager.GetScenManager().GetConnectGame().ResetConnection();
			NKCScenManager.GetScenManager().Get_SCEN_LOGIN().SetErrorCodeForNGS(sPacket.errorCode);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_LOGIN, true);
		}

		// Token: 0x06005918 RID: 22808 RVA: 0x001B0062 File Offset: 0x001AE262
		public static void OnRecv(NKMPacket_NEXON_NGS_DATA_NOT cNKMPacket_NEXON_NGS_DATA_NOT)
		{
			NKCPMNexonNGS.OnRecv(cNKMPacket_NEXON_NGS_DATA_NOT);
		}

		// Token: 0x06005919 RID: 22809 RVA: 0x001B006A File Offset: 0x001AE26A
		public static void OnRecv(NKMPacket_NEXON_PC_DATA_NOT cNKMPacket_NEXON_PC_DATA_NOT)
		{
			NKCPMNexonNGS.SetNpaCode(cNKMPacket_NEXON_PC_DATA_NOT.npacode);
		}

		// Token: 0x0600591A RID: 22810 RVA: 0x001B0077 File Offset: 0x001AE277
		public static void OnRecv(NKMPacket_ACCOUNT_LINK_ACK cNKMPacket_ACCOUNT_LINK_ACK)
		{
			NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_ACCOUNT_LINK_ACK.errorCode, true, null, int.MinValue);
		}

		// Token: 0x0600591B RID: 22811 RVA: 0x001B008C File Offset: 0x001AE28C
		public static void OnRecv(NKMPacket_UPDATE_RECONNECT_KEY_NOT cNKMPacket_UPDATE_RECONNECT_KEY_NOT)
		{
			NKCScenManager.GetScenManager().GetConnectGame().SetReconnectKey(cNKMPacket_UPDATE_RECONNECT_KEY_NOT.reconnectKey);
		}

		// Token: 0x0600591C RID: 22812 RVA: 0x001B00A3 File Offset: 0x001AE2A3
		public static void OnRecv(NKMPacket_INQUIRY_RESPONDED_NOT sPacket)
		{
			NKCUIPopupMessageServer.Instance.Open(NKCUIPopupMessageServer.eMessageStyle.Slide, NKCUtilString.GET_STRING_TOY_CUSTOMER_CENTER_RESPOND, 1);
		}

		// Token: 0x0600591D RID: 22813 RVA: 0x001B00B8 File Offset: 0x001AE2B8
		public static void OnRecv(NKMPacket_ACCOUNT_UNLINK_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPublisherModule.Auth.ResetConnection();
			NKCScenManager.GetScenManager().GetConnectLogin().ResetConnection();
			NKCScenManager.GetScenManager().GetConnectGame().ResetConnection();
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_LOGIN, true);
		}

		// Token: 0x0600591E RID: 22814 RVA: 0x001B0110 File Offset: 0x001AE310
		public static void OnRecv(NKMPacket_ACCOUNT_LEAVE_STATE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (sPacket.leave && NKCDefineManager.DEFINE_SB_GB())
			{
				if (NKCPublisherModule.Auth.IsGuest())
				{
					NKCPublisherModule.Auth.Withdraw(delegate(NKC_PUBLISHER_RESULT_CODE result, string additionalError)
					{
						NKCScenManager.GetScenManager().GetConnectLogin().ResetConnection();
						NKCScenManager.GetScenManager().GetConnectGame().ResetConnection();
						NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_LOGIN, true);
					});
					return;
				}
				NKCPublisherModule.Auth.TemporaryWithdrawal(delegate(NKC_PUBLISHER_RESULT_CODE result, string additionalError)
				{
					NKCScenManager.GetScenManager().GetConnectLogin().ResetConnection();
					NKCScenManager.GetScenManager().GetConnectGame().ResetConnection();
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_LOGIN, true);
				});
			}
		}

		// Token: 0x0600591F RID: 22815 RVA: 0x001B01A0 File Offset: 0x001AE3A0
		public static void OnRecv(NKMPacket_ACCOUNT_KICK_NOT sPacket)
		{
			NKMPopUpBox.OpenWaitBox(0f, "");
			NKCPublisherModule.Auth.Logout(new NKCPublisherModule.OnComplete(NKCPacketHandlersLobby.OnLogoutComplete));
		}

		// Token: 0x06005920 RID: 22816 RVA: 0x001B01C7 File Offset: 0x001AE3C7
		public static void OnLogoutComplete(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError)
		{
			if (!NKCPublisherModule.CheckError(resultCode, additionalError, true, null, false))
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_HOME().ResetFirstLobby();
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_TOY_LOGOUT_SUCCESS, new NKCPopupOKCancel.OnButton(NKCPacketHandlersLobby.MoveToLogin), "");
		}

		// Token: 0x06005921 RID: 22817 RVA: 0x001B0208 File Offset: 0x001AE408
		public static void MoveToLogin()
		{
			Log.Debug("[PacketHandlersLobby] MoveToLogin", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCPacketHandlersLobby.cs", 8299);
			NKCScenManager.GetScenManager().GetConnectLogin().ResetConnection();
			NKCScenManager.GetScenManager().GetConnectGame().ResetConnection();
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_LOGIN && NKCPatchDownloader.Instance != null)
			{
				NKCPatchDownloader.Instance.InitCheckTime();
			}
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_LOGIN, true);
		}

		// Token: 0x06005922 RID: 22818 RVA: 0x001B0274 File Offset: 0x001AE474
		public static void OnRecv(NKMPacket_MARQUEE_MESSAGE_NOT sPacket)
		{
			if (sPacket.message == null)
			{
				return;
			}
			foreach (string origin in sPacket.message)
			{
				NKCUIPopupMessageServer.Instance.Open(NKCUIPopupMessageServer.eMessageStyle.Slide, NKCPublisherModule.Localization.GetTranslationIfJson(origin), 1);
			}
		}

		// Token: 0x06005923 RID: 22819 RVA: 0x001B02E0 File Offset: 0x001AE4E0
		public static void OnRecv(NKMPacket_MESSAGE_NOT sPacket)
		{
			if (sPacket.message == null)
			{
				return;
			}
			foreach (string strID in sPacket.message)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString(strID, false), NKCPopupMessage.eMessagePosition.Top, false, false, 0f, false);
			}
		}

		// Token: 0x06005924 RID: 22820 RVA: 0x001B0348 File Offset: 0x001AE548
		public static void OnRecv(NKMPacket_EVENT_BINGO_RANDOM_MARK_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItemData);
			myUserData.GetReward(sPacket.rewardData);
			EventBingo bingoData = NKMEventManager.GetBingoData(sPacket.eventId);
			if (bingoData != null)
			{
				bingoData.SetMileage(sPacket.mileage);
			}
			if (NKCUIEvent.IsInstanceOpen && sPacket.rewardData != null)
			{
				NKCUIEvent.Instance.MarkBingo(sPacket.eventId, sPacket.rewardData.BingoTileList, true);
			}
		}

		// Token: 0x06005925 RID: 22821 RVA: 0x001B03D8 File Offset: 0x001AE5D8
		public static void OnRecv(NKMPacket_EVENT_BINGO_INDEX_MARK_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.GetScenManager().GetMyUserData().GetReward(sPacket.rewardData);
			EventBingo bingoData = NKMEventManager.GetBingoData(sPacket.eventId);
			if (bingoData != null)
			{
				bingoData.SetMileage(sPacket.mileage);
			}
			if (NKCUIEvent.IsInstanceOpen && sPacket.rewardData != null)
			{
				NKCUIEvent.Instance.MarkBingo(sPacket.eventId, sPacket.rewardData.BingoTileList, false);
			}
		}

		// Token: 0x06005926 RID: 22822 RVA: 0x001B0458 File Offset: 0x001AE658
		public static void OnRecv(NKMPacket_EVENT_BINGO_REWARD_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			myUserData.GetReward(sPacket.rewardData);
			EventBingo bingoData = NKMEventManager.GetBingoData(sPacket.eventId);
			if (bingoData != null)
			{
				bingoData.RecvReward(sPacket.rewardIndex);
			}
			if (NKCUIEvent.IsInstanceOpen)
			{
				NKCUIEvent.Instance.RefreshUI(sPacket.eventId);
			}
			if (NKCPopupEventBingoReward.IsInstanceOpen)
			{
				NKCPopupEventBingoReward.Instance.Refresh();
			}
			NKCUIResult.Instance.OpenComplexResult(myUserData.m_ArmyData, sPacket.rewardData, null, 0L, null, false, false);
		}

		// Token: 0x06005927 RID: 22823 RVA: 0x001B04F0 File Offset: 0x001AE6F0
		public static void OnRecv(NKMPacket_RESET_STAGE_PLAY_COUNT_ACK cNKMPacket_RESET_STAGE_PLAY_COUNT_ACK)
		{
			Debug.Log("OnRecv - NKMPacket_RESET_STAGE_PLAY_COUNT_ACK - NKCPacketHandlersLobby");
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_RESET_STAGE_PLAY_COUNT_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				nkmuserData.UpdateStagePlayData(cNKMPacket_RESET_STAGE_PLAY_COUNT_ACK.stagePlayData);
				nkmuserData.m_InventoryData.UpdateItemInfo(cNKMPacket_RESET_STAGE_PLAY_COUNT_ACK.costItemData);
			}
		}

		// Token: 0x06005928 RID: 22824 RVA: 0x001B0544 File Offset: 0x001AE744
		public static void OnRecv(NKMPacket_ZLONG_USE_COUPON_ACK cNKMPacket_ZLONG_USE_COUPON_ACK)
		{
			Debug.Log("OnRecv - cNKMPacket_ZLONG_USE_COUPON_ACK - NKCPacketHandlersLobby");
			if (cNKMPacket_ZLONG_USE_COUPON_ACK.errorCode == NKM_ERROR_CODE.NEC_FAIL_ZLONG_COUPON_API_RETURN_ERROR)
			{
				NKMPopUpBox.CloseWaitBox();
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_ZLONG_COUPON_API_RETURN_ERROR.ToString() + "_" + cNKMPacket_ZLONG_USE_COUPON_ACK.zlongInfoCode.ToString(), false), null, "");
				return;
			}
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_ZLONG_USE_COUPON_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPopupCoupon.CheckInstanceAndClose();
			if (NKCScenManager.CurrentUserData() != null)
			{
				NKCScenManager.CurrentUserData().GetReward(cNKMPacket_ZLONG_USE_COUPON_ACK.rewardData);
			}
			NKCUIResult.Instance.OpenComplexResult(NKCScenManager.CurrentUserData().m_ArmyData, cNKMPacket_ZLONG_USE_COUPON_ACK.rewardData, null, 0L, null, false, false);
		}

		// Token: 0x06005929 RID: 22825 RVA: 0x001B0600 File Offset: 0x001AE800
		public static void OnRecv(NKMPacket_EQUIP_ITEM_CHANGE_SET_OPTION_ACK sNKMPacket_EQUIP_ITEM_CHANGE_SET_OPTION_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sNKMPacket_EQUIP_ITEM_CHANGE_SET_OPTION_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMInventoryData inventoryData = NKCScenManager.CurrentUserData().m_InventoryData;
			if (inventoryData == null)
			{
				return;
			}
			inventoryData.UpdateItemInfo(sNKMPacket_EQUIP_ITEM_CHANGE_SET_OPTION_ACK.costItemData);
			NKCScenManager.CurrentUserData().SetEquipTuningData(sNKMPacket_EQUIP_ITEM_CHANGE_SET_OPTION_ACK.equipTuningCandidate);
			if (NKCUIForge.IsInstanceOpen)
			{
				NKCUIForge.Instance.m_NKCUIForgeTuning.ResetUI(true, false);
				NKCUIForge.Instance.DoAfterSetOptionChanged();
			}
		}

		// Token: 0x0600592A RID: 22826 RVA: 0x001B0670 File Offset: 0x001AE870
		public static void OnRecv(NKMPacket_EQUIP_ITEM_CONFIRM_SET_OPTION_ACK sNKMPacket_EQUIP_ITEM_CONFIRM_SET_OPTION_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sNKMPacket_EQUIP_ITEM_CONFIRM_SET_OPTION_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMInventoryData inventoryData = NKCScenManager.CurrentUserData().m_InventoryData;
			if (inventoryData == null)
			{
				return;
			}
			NKMEquipItemData itemEquip = inventoryData.GetItemEquip(sNKMPacket_EQUIP_ITEM_CONFIRM_SET_OPTION_ACK.equipUID);
			if (itemEquip != null)
			{
				itemEquip.m_SetOptionId = sNKMPacket_EQUIP_ITEM_CONFIRM_SET_OPTION_ACK.setOptionId;
			}
			inventoryData.UpdateItemEquip(itemEquip);
			NKCScenManager.CurrentUserData().SetEquipTuningData(sNKMPacket_EQUIP_ITEM_CONFIRM_SET_OPTION_ACK.equipTuningCandidate);
			if (NKCUIForge.IsInstanceOpen)
			{
				NKCUIForge.Instance.ResetUI();
				NKCUIForge.Instance.DoAfterSetOptionChangeConfirm();
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_UNIT_LIST && NKCUIUnitInfo.IsInstanceOpen)
			{
				NKCUIUnitInfo.Instance.UpdateEquipSlots();
			}
			if (NKCUIInventory.IsInstanceOpen)
			{
				NKCUIInventory.Instance.ResetEquipSlotList();
			}
		}

		// Token: 0x0600592B RID: 22827 RVA: 0x001B0720 File Offset: 0x001AE920
		public static void OnRecv(NKMPacket_EQUIP_ITEM_FIRST_SET_OPTION_ACK sNKMPacket_EQUIP_ITEM_FIRST_SET_OPTION_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sNKMPacket_EQUIP_ITEM_FIRST_SET_OPTION_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMInventoryData inventoryData = NKCScenManager.CurrentUserData().m_InventoryData;
			if (inventoryData == null)
			{
				return;
			}
			NKMEquipItemData itemEquip = inventoryData.GetItemEquip(sNKMPacket_EQUIP_ITEM_FIRST_SET_OPTION_ACK.equipUID);
			if (itemEquip != null)
			{
				itemEquip.m_SetOptionId = sNKMPacket_EQUIP_ITEM_FIRST_SET_OPTION_ACK.setOptionId;
			}
			if (NKCUIForge.IsInstanceOpen)
			{
				NKCUIForge.Instance.ResetUI();
			}
		}

		// Token: 0x0600592C RID: 22828 RVA: 0x001B077E File Offset: 0x001AE97E
		public static void OnRecv(NKMPacket_EQUIP_TUNING_CANCEL_ACK sNKMPacket_Equip_Tuning_Cancel_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sNKMPacket_Equip_Tuning_Cancel_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.CurrentUserData().SetEquipTuningData(sNKMPacket_Equip_Tuning_Cancel_ACK.equipTuningCandidate);
		}

		// Token: 0x0600592D RID: 22829 RVA: 0x001B07A5 File Offset: 0x001AE9A5
		public static void OnRecv(NKMPacket_EQUIP_TUNING_NOT sNKMPacket_Equip_Tuning_NOT)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sNKMPacket_Equip_Tuning_NOT.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.CurrentUserData().SetEquipTuningData(sNKMPacket_Equip_Tuning_NOT.equipTuningCandidate);
		}

		// Token: 0x0600592E RID: 22830 RVA: 0x001B07CC File Offset: 0x001AE9CC
		public static void OnRecv(NKMPacket_GUILD_CHAT_TRANSLATE_ACK cNKMPacket_GUILD_CHAT_TRANSLATE_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_CHAT_TRANSLATE_ACK.errorCode, true, null, -2147483648))
			{
				NKCPublisherModule.Localization.OnTranslateCompleteFromCS_Server(cNKMPacket_GUILD_CHAT_TRANSLATE_ACK.messageUid, "");
				return;
			}
			NKCPublisherModule.Localization.OnTranslateCompleteFromCS_Server(cNKMPacket_GUILD_CHAT_TRANSLATE_ACK.messageUid, cNKMPacket_GUILD_CHAT_TRANSLATE_ACK.textTranslated);
		}

		// Token: 0x0600592F RID: 22831 RVA: 0x001B0819 File Offset: 0x001AEA19
		public static void OnRecv(NKMPacket_GUILD_DATA_UPDATED_NOT cNKMPacket_GUILD_DATA_UPDATED_NOT)
		{
			NKCGuildManager.SetMyGuildData(cNKMPacket_GUILD_DATA_UPDATED_NOT.guildData);
		}

		// Token: 0x06005930 RID: 22832 RVA: 0x001B0826 File Offset: 0x001AEA26
		public static void OnRecv(NKMPacket_GUILD_CANCEL_REQUEST_NOT cNKMPacket_GUILD_CANCEL_REQUEST_NOT)
		{
			if (cNKMPacket_GUILD_CANCEL_REQUEST_NOT.isRequest)
			{
				NKCGuildManager.RemoveRequestedData(cNKMPacket_GUILD_CANCEL_REQUEST_NOT.guildUid);
				return;
			}
			NKCGuildManager.RemoveInvitedData(cNKMPacket_GUILD_CANCEL_REQUEST_NOT.guildUid);
		}

		// Token: 0x06005931 RID: 22833 RVA: 0x001B0847 File Offset: 0x001AEA47
		public static void OnRecv(NKMPacket_GUILD_BAN_NOT cNKMPacket_GUILD_BAN_NOT)
		{
			NKCGuildManager.SetMyData(new PrivateGuildData());
			NKCGuildManager.SetMyGuildData(null);
			NKCGuildCoopManager.ResetGuildCoopState();
			NKCChatManager.ResetGuildMemberChatList();
			NKCPopupMessageGuild.Instance.Open(NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_FORCE_EXIT_TOAST_MESSAGE_TITLE_DESC, NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_FORCE_EXIT_INFORMATION_POPUP_BODY_DESC, false);
		}

		// Token: 0x06005932 RID: 22834 RVA: 0x001B0878 File Offset: 0x001AEA78
		public static void OnRecv(NKMPacket_GUILD_ACCEPT_JOIN_NOT cNKMPacket_GUILD_ACCEPT_JOIN_NOT)
		{
			if (cNKMPacket_GUILD_ACCEPT_JOIN_NOT.isAllow)
			{
				NKCPopupMessageGuild.Instance.Open(NKCUtilString.GET_STRING_CONSORTIUM_OVERLAY_MESSAGE_HEAD_DESC, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_OVERLAY_MESSAGE_BODY_JOIN, cNKMPacket_GUILD_ACCEPT_JOIN_NOT.guildName), true);
			}
			NKCGuildManager.SetMyData(cNKMPacket_GUILD_ACCEPT_JOIN_NOT.privateGuildData);
		}

		// Token: 0x06005933 RID: 22835 RVA: 0x001B08AD File Offset: 0x001AEAAD
		public static void OnRecv(NKMPacket_GUILD_LEVEL_UP_NOT cNKMPacket_GUILD_LEVEL_UP_NOT)
		{
			NKCPopupMessageGuild.Instance.Open(NKCUtilString.GET_STRING_CONSORTIUM_OVERLAY_MESSAGE_HEAD_DESC, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_OVERLAY_MESSAGE_BODY_LEVEL_UP, NKCGuildManager.MyGuildData.name, cNKMPacket_GUILD_LEVEL_UP_NOT.guildLevel), true);
		}

		// Token: 0x06005934 RID: 22836 RVA: 0x001B08DE File Offset: 0x001AEADE
		public static void OnRecv(NKMPacket_GUILD_INVITE_NOT cNKMPacket_GUILD_INVITE_NOT)
		{
		}

		// Token: 0x06005935 RID: 22837 RVA: 0x001B08E0 File Offset: 0x001AEAE0
		public static void OnRecv(NKMPacket_GUILD_DELETED_NOT cNKMPacket_GUILD_DELETED_NOT)
		{
			NKCGuildManager.SetMyData(new PrivateGuildData());
			NKCGuildManager.SetMyGuildData(null);
			NKCGuildCoopManager.ResetGuildCoopState();
		}

		// Token: 0x06005936 RID: 22838 RVA: 0x001B08F8 File Offset: 0x001AEAF8
		public static void OnRecv(NKMPacket_GUILD_MEMBER_GRADE_UPDATED_NOT cNKMPacket_GUILD_MEMBER_GRADE_UPDATED_NOT)
		{
			if (cNKMPacket_GUILD_MEMBER_GRADE_UPDATED_NOT.gradeBefore > cNKMPacket_GUILD_MEMBER_GRADE_UPDATED_NOT.gradeAfter)
			{
				NKCPopupMessageGuild.Instance.Open(NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_CHANGE_PERMISSION_TOAST_MESSAGE_TITLE_DESC, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_GRADE_UP_INFORMATION_POPUP_BODY_DESC, NKCGuildManager.MyGuildData.name), true);
				return;
			}
			NKCPopupMessageGuild.Instance.Open(NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_CHANGE_PERMISSION_TOAST_MESSAGE_TITLE_DESC, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_FRADE_DOWN_INFORMATION_POPUP_BODY_DESC, NKCGuildManager.MyGuildData.name), false);
		}

		// Token: 0x06005937 RID: 22839 RVA: 0x001B095C File Offset: 0x001AEB5C
		public static void OnRecv(NKMPacket_GUILD_MASTER_SPECIFIED_MIGRATION_NOT cNKMPacket_GUILD_MASTER_SPECIFIED_MIGRATION_NOT)
		{
			NKCPopupMessageGuild.Instance.Open(NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_CHANGE_MASTER_TOAST_MESSAGE_TITLE_DESC, string.Format(NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_GRADE_HANDOVER_INFORMATION_POPUP_BODY_DESC, NKCGuildManager.MyGuildData.name), true);
		}

		// Token: 0x06005938 RID: 22840 RVA: 0x001B0982 File Offset: 0x001AEB82
		public static void OnRecv(NKMPacket_GUILD_USER_PROFILE_UPDATED_NOT cNKMPacket_GUILD_USER_PROFILE_UPDATED_NOT)
		{
			NKCGuildManager.ChangeGuildMemberData(cNKMPacket_GUILD_USER_PROFILE_UPDATED_NOT.commonProfile, cNKMPacket_GUILD_USER_PROFILE_UPDATED_NOT.lastOnlineTime);
		}

		// Token: 0x06005939 RID: 22841 RVA: 0x001B0995 File Offset: 0x001AEB95
		public static void OnRecv(NKMPacket_GUILD_JOIN_DISABLETIME_UPDATED_NOT cNKMPacket_GUILD_JOIN_DISABLETIME_UPDATED_NOT)
		{
			NKCGuildManager.SetGuildJoinDisableTime(cNKMPacket_GUILD_JOIN_DISABLETIME_UPDATED_NOT.joinDisableTime);
		}

		// Token: 0x0600593A RID: 22842 RVA: 0x001B09A2 File Offset: 0x001AEBA2
		public static void OnRecv(NKMPacket_GUILD_UPDATE_NOTICE_NOT cNKMPacket_GUILD_UPDATE_NOTICE_NOT)
		{
			NKCPopupMessageGuild.Instance.Open(NKCUtilString.GET_STRING_CONSORTIUM_LOBBY_INFORMATION_CHANGE_OVERLAY_TITLE_TEXT, NKCUtilString.GET_STRING_CONSORTIUM_LOBBY_INFORMATION_CHANGE_OVERLAY_BODY_TEXT, true);
		}

		// Token: 0x0600593B RID: 22843 RVA: 0x001B09BC File Offset: 0x001AEBBC
		public static void OnRecv(NKMPacket_COMPANY_BUFF_ADD_NOT cNKMPacket_COMPANY_BUFF_ADD_NOT)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			NKCCompanyBuff.UpsertCompanyBuffData(nkmuserData.m_companyBuffDataList, cNKMPacket_COMPANY_BUFF_ADD_NOT.companyBuffData);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GUILD_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_LOBBY().RefreshUI();
			}
		}

		// Token: 0x0600593C RID: 22844 RVA: 0x001B0A04 File Offset: 0x001AEC04
		public static void OnRecv(NKMPacket_GUILD_CREATE_ACK cNKMPacket_GUILD_CREATE_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_CREATE_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCGuildManager.SetMyData(cNKMPacket_GUILD_CREATE_ACK.privateGuildData);
			NKCGuildManager.SetMyGuildData(cNKMPacket_GUILD_CREATE_ACK.guildData);
			NKCScenManager.CurrentUserData().m_InventoryData.UpdateItemInfo(cNKMPacket_GUILD_CREATE_ACK.costItemDataList);
		}

		// Token: 0x0600593D RID: 22845 RVA: 0x001B0A51 File Offset: 0x001AEC51
		public static void OnRecv(NKMPacket_GUILD_CLOSE_ACK cNKMPacket_GUILD_CLOSE_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_CLOSE_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_CONSORTIUM_OPTION_DISMANTLE_POPUP_TITLE_DESC, NKCUtilString.GET_STRING_CONSORTIUM_OPTION_DISMANTLE_POPUP_CONFIRM_DESC, null, "");
		}

		// Token: 0x0600593E RID: 22846 RVA: 0x001B0A7D File Offset: 0x001AEC7D
		public static void OnRecv(NKMPacket_GUILD_CLOSE_CANCEL_ACK cNKMPacket_GUILD_CLOSE_CANCEL_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_CLOSE_CANCEL_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_CONSORTIUM_OPTION_DISMANTLE_CANCEL_POPUP_TITLE_DESC, NKCUtilString.GET_STRING_CONSORTIUM_OPTION_DISMANTLE_CANCEL_POPUP_CONFIRM_DESC, null, "");
		}

		// Token: 0x0600593F RID: 22847 RVA: 0x001B0AA9 File Offset: 0x001AECA9
		public static void OnRecv(NKMPacket_GUILD_SEARCH_ACK cNKMPacket_GUILD_SEARCH_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_SEARCH_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCGuildManager.OnRecv(cNKMPacket_GUILD_SEARCH_ACK);
		}

		// Token: 0x06005940 RID: 22848 RVA: 0x001B0AC6 File Offset: 0x001AECC6
		public static void OnRecv(NKMPacket_GUILD_LIST_ACK cNKMPacket_GUILD_LIST_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_LIST_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCGuildManager.OnRecv(cNKMPacket_GUILD_LIST_ACK);
		}

		// Token: 0x06005941 RID: 22849 RVA: 0x001B0AE3 File Offset: 0x001AECE3
		public static void OnRecv(NKMPacket_GUILD_DATA_ACK cNKMPacket_GUILD_DATA_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_DATA_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPopupGuildInfo.Instance.Open(cNKMPacket_GUILD_DATA_ACK.guildData);
		}

		// Token: 0x06005942 RID: 22850 RVA: 0x001B0B0A File Offset: 0x001AED0A
		public static void OnRecv(NKMPacket_GUILD_JOIN_ACK cNKMPacket_GUILD_JOIN_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_JOIN_ACK.errorCode, true, null, -2147483648))
			{
				NKCGuildManager.RemoveLastJoinRequestedGuildData();
				if (NKCUIGuildJoin.IsInstanceOpen)
				{
					NKCUIGuildJoin.Instance.RefreshUI();
				}
				return;
			}
			NKCGuildManager.SetMyData(cNKMPacket_GUILD_JOIN_ACK.privateGuildData);
			NKCGuildManager.OnRecv(cNKMPacket_GUILD_JOIN_ACK);
		}

		// Token: 0x06005943 RID: 22851 RVA: 0x001B0B48 File Offset: 0x001AED48
		public static void OnRecv(NKMPacket_GUILD_CANCEL_JOIN_ACK cNKMPacket_GUILD_CANCEL_JOIN_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_CANCEL_JOIN_ACK.errorCode, true, null, -2147483648))
			{
				NKCPacketSender.Send_NKMPacket_GUILD_LIST_REQ(GuildListType.SendRequest);
				return;
			}
			NKCGuildManager.OnRecv(cNKMPacket_GUILD_CANCEL_JOIN_ACK);
		}

		// Token: 0x06005944 RID: 22852 RVA: 0x001B0B6B File Offset: 0x001AED6B
		public static void OnRecv(NKMPacket_GUILD_ACCEPT_JOIN_ACK cNKMPacket_GUILD_ACCEPT_JOIN_ACK)
		{
			NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_ACCEPT_JOIN_ACK.errorCode, true, null, int.MinValue);
		}

		// Token: 0x06005945 RID: 22853 RVA: 0x001B0B80 File Offset: 0x001AED80
		public static void OnRecv(NKMPacket_GUILD_INVITE_ACK cNKMPacket_GUILD_INVITE_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_INVITE_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_CONSORTIUM_POPUP_INVITE_TITLE, NKCUtilString.GET_STRING_CONSORTIUM_INVITE_SEND_SUCCESS_BODY_DESC, null, "");
		}

		// Token: 0x06005946 RID: 22854 RVA: 0x001B0BAC File Offset: 0x001AEDAC
		public static void OnRecv(NKMPacket_GUILD_CANCEL_INVITE_ACK cNKMPacket_GUILD_CANCEL_INVITE_ACK)
		{
			NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_CANCEL_INVITE_ACK.errorCode, true, null, int.MinValue);
		}

		// Token: 0x06005947 RID: 22855 RVA: 0x001B0BC4 File Offset: 0x001AEDC4
		public static void OnRecv(NKMPacket_GUILD_ACCEPT_INVITE_ACK cNKMPacket_GUILD_ACCEPT_INVITE_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_ACCEPT_INVITE_ACK.errorCode, true, null, -2147483648))
			{
				NKCPacketSender.Send_NKMPacket_GUILD_LIST_REQ(GuildListType.ReceiveInvite);
				return;
			}
			NKCGuildManager.RemoveInvitedData(cNKMPacket_GUILD_ACCEPT_INVITE_ACK.guildUid);
			NKCGuildManager.SetMyData(cNKMPacket_GUILD_ACCEPT_INVITE_ACK.privateGuildData);
			if (NKCUIGuildJoin.IsInstanceOpen)
			{
				NKCUIGuildJoin.Instance.RefreshUI();
			}
		}

		// Token: 0x06005948 RID: 22856 RVA: 0x001B0C13 File Offset: 0x001AEE13
		public static void OnRecv(NKMPacket_GUILD_EXIT_ACK cNKMPacket_GUILD_EXIT_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_EXIT_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCGuildManager.SetMyData(new PrivateGuildData
			{
				guildJoinDisableTime = cNKMPacket_GUILD_EXIT_ACK.joinDisableTime
			});
			NKCGuildManager.SetMyGuildData(null);
			NKCGuildCoopManager.ResetGuildCoopState();
			NKCChatManager.ResetGuildMemberChatList();
		}

		// Token: 0x06005949 RID: 22857 RVA: 0x001B0C50 File Offset: 0x001AEE50
		public static void OnRecv(NKMPacket_GUILD_SET_MEMBER_GRADE_ACK cNKMPacket_GUILD_SET_MEMBER_GRADE_ACK)
		{
			if (cNKMPacket_GUILD_SET_MEMBER_GRADE_ACK.errorCode != NKM_ERROR_CODE.NEC_OK && NKCPopupFriendInfo.IsInstanceOpen)
			{
				NKCPopupFriendInfo.Instance.Close();
			}
			NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_SET_MEMBER_GRADE_ACK.errorCode, true, null, int.MinValue);
		}

		// Token: 0x0600594A RID: 22858 RVA: 0x001B0C7E File Offset: 0x001AEE7E
		public static void OnRecv(NKMPacket_GUILD_BAN_ACK cNKMPacket_GUILD_BAN_ACK)
		{
			if (cNKMPacket_GUILD_BAN_ACK.errorCode != NKM_ERROR_CODE.NEC_OK && NKCPopupFriendInfo.IsInstanceOpen)
			{
				NKCPopupFriendInfo.Instance.Close();
			}
			NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_BAN_ACK.errorCode, true, null, int.MinValue);
		}

		// Token: 0x0600594B RID: 22859 RVA: 0x001B0CAC File Offset: 0x001AEEAC
		public static void OnRecv(NKMPacket_GUILD_MASTER_SPECIFIED_MIGRATION_ACK cNKMPacket_GUILD_MASTER_SPECIFIED_MIGRATION_ACK)
		{
			if (cNKMPacket_GUILD_MASTER_SPECIFIED_MIGRATION_ACK.errorCode != NKM_ERROR_CODE.NEC_OK && NKCPopupFriendInfo.IsInstanceOpen)
			{
				NKCPopupFriendInfo.Instance.Close();
			}
			NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_MASTER_SPECIFIED_MIGRATION_ACK.errorCode, true, null, int.MinValue);
		}

		// Token: 0x0600594C RID: 22860 RVA: 0x001B0CDA File Offset: 0x001AEEDA
		public static void OnRecv(NKMPacket_GUILD_MASTER_MIGRATION_ACK cNKMPacket_GUILD_MASTER_MIGRATION_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_MASTER_MIGRATION_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_CONSORTIUM_OPTION_DISMANTLE_HANDOVER_CONFIRM_POPUP_TITLE_DESC, NKCUtilString.GET_STRING_CONSORTIUM_OPTION_DISMANTLE_HANDOVER_CONFIRM_POPUP_BODY_DESC, null, "");
		}

		// Token: 0x0600594D RID: 22861 RVA: 0x001B0D06 File Offset: 0x001AEF06
		public static void OnRecv(NKMPacket_GUILD_UPDATE_DATA_ACK cNKMPacket_GUILD_UPDATE_DATA_ACK)
		{
			NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_UPDATE_DATA_ACK.errorCode, true, null, int.MinValue);
		}

		// Token: 0x0600594E RID: 22862 RVA: 0x001B0D1B File Offset: 0x001AEF1B
		public static void OnRecv(NKMPacket_GUILD_UPDATE_NOTICE_ACK cNKMPacket_GUILD_UPDATE_NOTICE_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_UPDATE_NOTICE_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCGuildManager.SetLastNoticeChangedTimeUTC(NKCSynchronizedTime.GetServerUTCTime(0.0));
		}

		// Token: 0x0600594F RID: 22863 RVA: 0x001B0D45 File Offset: 0x001AEF45
		public static void OnRecv(NKMPacket_GUILD_UPDATE_MEMBER_GREETING_ACK cNKMPacket_GUILD_UPDATE_MEMBER_GREETING_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_UPDATE_MEMBER_GREETING_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_CONSORTIUM_MEMBER_INTRODUCE_WRITE_SUCCESS_TOAST_MESSAGE_TEXT, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
		}

		// Token: 0x06005950 RID: 22864 RVA: 0x001B0D7C File Offset: 0x001AEF7C
		public static void OnRecv(NKMPacket_GUILD_ATTENDANCE_ACK cNKMPacket_GUILD_ATTENDANCE_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_ATTENDANCE_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData userData = NKCScenManager.CurrentUserData();
			if (userData != null)
			{
				userData.GetReward(cNKMPacket_GUILD_ATTENDANCE_ACK.rewardData, cNKMPacket_GUILD_ATTENDANCE_ACK.additionalReward);
				NKMGuildMemberData nkmguildMemberData = NKCGuildManager.MyGuildData.members.Find((NKMGuildMemberData x) => x.commonProfile.userUid == userData.m_UserUID);
				if (nkmguildMemberData != null)
				{
					nkmguildMemberData.lastAttendanceDate = cNKMPacket_GUILD_ATTENDANCE_ACK.lastAttendanceDate;
				}
				if (cNKMPacket_GUILD_ATTENDANCE_ACK.rewardData != null)
				{
					NKCUIResult.Instance.OpenRewardGain(userData.m_ArmyData, cNKMPacket_GUILD_ATTENDANCE_ACK.rewardData, cNKMPacket_GUILD_ATTENDANCE_ACK.additionalReward, NKCUtilString.GET_STRING_CONSORTIUM_POPUP_ATTENDANCE_REWARD_TITLE, "", null);
				}
			}
			NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_CONSORTIUM_ATTENDANCE_SUCCESS_TOAST_MESSAGE_TEXT, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
			NKCUIManager.OnGuildDataChanged();
		}

		// Token: 0x06005951 RID: 22865 RVA: 0x001B0E4F File Offset: 0x001AF04F
		public static void OnRecv(NKMPacket_GUILD_RECOMMEND_INVITE_LIST_ACK cNKMPacket_GUILD_RECOMMEND_INVITE_LIST_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_RECOMMEND_INVITE_LIST_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GUILD_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_LOBBY().OnRecv(cNKMPacket_GUILD_RECOMMEND_INVITE_LIST_ACK.list);
			}
		}

		// Token: 0x06005952 RID: 22866 RVA: 0x001B0E8C File Offset: 0x001AF08C
		public static void OnRecv(NKMPacket_GUILD_DONATION_ACK cNKMPacket_GUILD_DONATION_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_DONATION_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			GuildDonationTemplet guildDonationTemplet = GuildDonationTemplet.Find(cNKMPacket_GUILD_DONATION_ACK.donationId);
			if (guildDonationTemplet != null)
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(string.Format(NKCUtilString.GET_STRING_CONSORTIUM_DONATION_SUCCESS_TOAST_TEXT, NKCStringTable.GetString(guildDonationTemplet.DonateText, false)), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
			}
			NKCGuildManager.MyData.donationCount = cNKMPacket_GUILD_DONATION_ACK.donationCount;
			NKCGuildManager.MyData.lastDailyResetDate = cNKMPacket_GUILD_DONATION_ACK.lastDailyResetDate;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				nkmuserData.m_InventoryData.UpdateItemInfo(cNKMPacket_GUILD_DONATION_ACK.costItemDataList);
				nkmuserData.GetReward(cNKMPacket_GUILD_DONATION_ACK.rewardData);
				NKCUIResult.Instance.OpenComplexResultFull(nkmuserData.m_ArmyData, cNKMPacket_GUILD_DONATION_ACK.rewardData, cNKMPacket_GUILD_DONATION_ACK.additionalReward, null, 0L, null, false, false);
			}
		}

		// Token: 0x06005953 RID: 22867 RVA: 0x001B0F54 File Offset: 0x001AF154
		public static void OnRecv(NKMPacket_GUILD_BUY_BUFF_ACK cNKMPacket_GUILD_BUY_BUFF_ACK)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (cNKMPacket_GUILD_BUY_BUFF_ACK.errorCode == NKM_ERROR_CODE.NEC_FAIL_GUILD_NOT_ENOUGH_UNION_POINT)
			{
				if (nkmuserData != null)
				{
					NKCGuildManager.MyGuildData.unionPoint = cNKMPacket_GUILD_BUY_BUFF_ACK.unionPoint;
				}
				NKCUIManager.OnGuildDataChanged();
			}
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_BUY_BUFF_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (nkmuserData != null)
			{
				nkmuserData.m_InventoryData.UpdateItemInfo(cNKMPacket_GUILD_BUY_BUFF_ACK.costItemDataList);
				nkmuserData.GetReward(cNKMPacket_GUILD_BUY_BUFF_ACK.rewardData);
				NKCGuildManager.MyGuildData.unionPoint = cNKMPacket_GUILD_BUY_BUFF_ACK.unionPoint;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GUILD_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_LOBBY().RefreshUI();
			}
		}

		// Token: 0x06005954 RID: 22868 RVA: 0x001B0FF0 File Offset: 0x001AF1F0
		public static void OnRecv(NKMPacket_GUILD_BUY_WELFARE_POINT_ACK cNKMPacket_GUILD_BUY_WELFARE_POINT_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_BUY_WELFARE_POINT_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				nkmuserData.m_InventoryData.UpdateItemInfo(cNKMPacket_GUILD_BUY_WELFARE_POINT_ACK.costItemDataList);
				nkmuserData.GetReward(cNKMPacket_GUILD_BUY_WELFARE_POINT_ACK.rewardData);
			}
			NKCUIManager.NKCPopupMessage.Open(new PopupMessage(string.Format(NKCStringTable.GetString("SI_DP_SHOP_ITEM_BUY_MESSAGE_ELSE", false), NKMItemManager.GetItemMiscTempletByID(23).GetItemName()), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GUILD_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_LOBBY().RefreshUI();
			}
		}

		// Token: 0x06005955 RID: 22869 RVA: 0x001B108C File Offset: 0x001AF28C
		public static void OnRecv(NKMPacket_GUILD_DUNGEON_INFO_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCGuildCoopManager.OnRecv(sPacket);
			NKCPublisherModule.Push.UpdateLocalPush(NKC_GAME_OPTION_ALARM_GROUP.GUILD_DUNGEON_NOTIFY, true);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GUILD_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_LOBBY().SetCoopDataRecved(true);
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GUILD_COOP)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_COOP().Refresh();
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_RAID_READY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_RAID_READY().OnRecv(sPacket);
			}
		}

		// Token: 0x06005956 RID: 22870 RVA: 0x001B111B File Offset: 0x001AF31B
		public static void OnRecv(NKMPacket_GUILD_DUNGEON_MEMBER_INFO_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCGuildCoopManager.OnRecv(sPacket);
		}

		// Token: 0x06005957 RID: 22871 RVA: 0x001B1138 File Offset: 0x001AF338
		public static void OnRecv(NKMPacket_GUILD_DUNGEON_SEASON_REWARD_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			nkmuserData.GetReward(sPacket.rewardData);
			NKCGuildCoopManager.OnRecv(sPacket);
			NKCUIResult.Instance.OpenComplexResult(nkmuserData.m_ArmyData, sPacket.rewardData, delegate
			{
				NKCPopupGuildCoopSeasonReward.Instance.RefreshUI();
			}, 0L, null, false, false);
		}

		// Token: 0x06005958 RID: 22872 RVA: 0x001B11AC File Offset: 0x001AF3AC
		public static void OnRecv(NKMPacket_GUILD_DUNGEON_TICKET_BUY_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				nkmuserData.m_InventoryData.UpdateItemInfo(sPacket.costItemData);
			}
			NKCGuildCoopManager.SetArenaTicketBuyCount(sPacket.currentTicketBuyCount);
			NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_CONSORTIUM_DUNGEON_PLAY_COUNT_BUY_SUCCESS_TEXT, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
			if (NKCUIPrepareEventDeck.IsInstanceOpen)
			{
				NKCUIPrepareEventDeck.Instance.RefreshUIByContents();
			}
		}

		// Token: 0x06005959 RID: 22873 RVA: 0x001B1224 File Offset: 0x001AF424
		public static void OnRecv(NKMPacket_GUILD_DUNGEON_SESSION_REWARD_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			NKMRewardData nkmrewardData = new NKMRewardData();
			if (sPacket.rewardList != null)
			{
				nkmrewardData.SetMiscItemData(sPacket.rewardList);
			}
			NKMRewardData nkmrewardData2 = new NKMRewardData();
			if (sPacket.artifactReward != null)
			{
				for (int i = 0; i < sPacket.artifactReward.Count; i++)
				{
					nkmrewardData2.Upsert(sPacket.artifactReward[i]);
				}
			}
			nkmuserData.GetReward(nkmrewardData);
			nkmuserData.GetReward(nkmrewardData2);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GUILD_COOP)
			{
				NKCGuildCoopManager.SetRewarded();
				NKCGuildCoopManager.AddMyPoint(GuildDungeonRewardCategory.RANK, sPacket.clearPoint);
				NKCPopupGuildCoopSessionResult.Instance.Open(sPacket);
			}
		}

		// Token: 0x0600595A RID: 22874 RVA: 0x001B12D8 File Offset: 0x001AF4D8
		public static void OnRecv(NKMPacket_GUILD_DUNGEON_ARENA_PLAY_NOT sPacket)
		{
			NKCGuildCoopManager.SetArenaPlayStart(sPacket.arenaId, sPacket.userUid);
		}

		// Token: 0x0600595B RID: 22875 RVA: 0x001B12EB File Offset: 0x001AF4EB
		public static void OnRecv(NKMPacket_GUILD_DUNGEON_BOSS_PLAY_NOT sPacket)
		{
			NKCGuildCoopManager.SetBossPlayStart(sPacket.userUid);
		}

		// Token: 0x0600595C RID: 22876 RVA: 0x001B12F8 File Offset: 0x001AF4F8
		public static void OnRecv(NKMPacket_GUILD_DUNGEON_ARENA_PLAY_END_NOT sPacket)
		{
			NKCGuildCoopManager.SetArenaPlayEnd(sPacket);
		}

		// Token: 0x0600595D RID: 22877 RVA: 0x001B1300 File Offset: 0x001AF500
		public static void OnRecv(NKMPacket_GUILD_DUNGEON_ARENA_PLAY_CANCEL_NOT sPacket)
		{
			NKCGuildCoopManager.SetArenaPlayCancel(sPacket);
		}

		// Token: 0x0600595E RID: 22878 RVA: 0x001B1308 File Offset: 0x001AF508
		public static void OnRecv(NKMPacket_GUILD_DUNGEON_BOSS_PLAY_END_NOT sPacket)
		{
			NKCGuildCoopManager.SetBossPlayEnd(sPacket);
		}

		// Token: 0x0600595F RID: 22879 RVA: 0x001B1310 File Offset: 0x001AF510
		public static void OnRecv(NKMPacket_GUILD_DUNGEON_BOSS_PLAY_CANCEL_NOT sPacket)
		{
			NKCGuildCoopManager.SetBossPlayCancel(sPacket);
		}

		// Token: 0x06005960 RID: 22880 RVA: 0x001B1318 File Offset: 0x001AF518
		public static void OnRecv(NKMPacket_GUILD_CHAT_NOT cNKMPacket_GUILD_CHAT_NOT)
		{
			NKCChatManager.OnRecv(cNKMPacket_GUILD_CHAT_NOT);
		}

		// Token: 0x06005961 RID: 22881 RVA: 0x001B1320 File Offset: 0x001AF520
		public static void OnRecv(NKMPacket_GUILD_CHAT_LIST_NOT cNKMPacket_GUILD_CHAT_LIST_NOT)
		{
			NKCChatManager.OnRecvGuildChatList(cNKMPacket_GUILD_CHAT_LIST_NOT.guildUid, cNKMPacket_GUILD_CHAT_LIST_NOT.messages, true);
		}

		// Token: 0x06005962 RID: 22882 RVA: 0x001B1334 File Offset: 0x001AF534
		public static void OnRecv(NKMPacket_BLOCK_MUTE_NOT cNKMPacket_BLOCK_MUTE_NOT)
		{
			if (NKCScenManager.CurrentUserData().m_UserUID == cNKMPacket_BLOCK_MUTE_NOT.userUid)
			{
				NKCChatManager.SetMuteEndDate(cNKMPacket_BLOCK_MUTE_NOT.endDate);
			}
		}

		// Token: 0x06005963 RID: 22883 RVA: 0x001B1353 File Offset: 0x001AF553
		public static void OnRecv(NKMPacket_GUILD_CHAT_ACK cNKMPacket_GUILD_CHAT_ACK)
		{
			NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_CHAT_ACK.errorCode, true, null, int.MinValue);
		}

		// Token: 0x06005964 RID: 22884 RVA: 0x001B1368 File Offset: 0x001AF568
		public static void OnRecv(NKMPacket_GUILD_CHAT_LIST_ACK cNKMPacket_GUILD_CHAT_LIST_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_CHAT_LIST_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GUILD_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GUILD_LOBBY().SetChatDataRecved(true);
			}
			NKCChatManager.OnRecvGuildChatList(cNKMPacket_GUILD_CHAT_LIST_ACK.guildUid, cNKMPacket_GUILD_CHAT_LIST_ACK.messages, false);
		}

		// Token: 0x06005965 RID: 22885 RVA: 0x001B13BA File Offset: 0x001AF5BA
		public static void OnRecv(NKMPacket_GUILD_CHAT_COMPLAIN_ACK cNKMPacket_GUILD_CHAT_COMPLAIN_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GUILD_CHAT_COMPLAIN_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_CONSORTIUM_CHAT_REPORT_CONFIRM_POPUP_TITLE_DESC, NKCUtilString.GET_STRING_CONSORTIUM_CHAT_REPORT_CONFIRM_POPUP_BODY_DESC, null, "");
		}

		// Token: 0x06005966 RID: 22886 RVA: 0x001B13E6 File Offset: 0x001AF5E6
		public static void OnRecv(NKMPacket_PRIVATE_CHAT_NOT sPacket)
		{
			NKCChatManager.OnRecv(sPacket);
		}

		// Token: 0x06005967 RID: 22887 RVA: 0x001B13EE File Offset: 0x001AF5EE
		public static void OnRecv(NKMPacket_PRIVATE_CHAT_LIST_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCChatManager.OnRecv(sPacket);
		}

		// Token: 0x06005968 RID: 22888 RVA: 0x001B140B File Offset: 0x001AF60B
		public static void OnRecv(NKMPacket_PRIVATE_CHAT_ACK sPacket)
		{
			NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, int.MinValue);
		}

		// Token: 0x06005969 RID: 22889 RVA: 0x001B1420 File Offset: 0x001AF620
		public static void OnRecv(NKMPacket_PRIVATE_CHAT_ALL_LIST_ACK sPacket)
		{
			if (sPacket.errorCode != NKM_ERROR_CODE.NEC_OK)
			{
				return;
			}
			NKCChatManager.OnRecvAllChat(sPacket);
		}

		// Token: 0x0600596A RID: 22890 RVA: 0x001B1431 File Offset: 0x001AF631
		public static void OnRecv(NKMPacket_LEADERBOARD_ACHIEVE_LIST_ACK cNKMPacket_LEADERBOARD_ACHIEVE_LIST_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_LEADERBOARD_ACHIEVE_LIST_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCLeaderBoardManager.OnRecv(cNKMPacket_LEADERBOARD_ACHIEVE_LIST_ACK);
		}

		// Token: 0x0600596B RID: 22891 RVA: 0x001B144E File Offset: 0x001AF64E
		public static void OnRecv(NKMPacket_LEADERBOARD_SHADOWPALACE_LIST_ACK cNKMPacket_LEADERBOARD_SHADOWPALACE_LIST_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_LEADERBOARD_SHADOWPALACE_LIST_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCLeaderBoardManager.OnRecv(cNKMPacket_LEADERBOARD_SHADOWPALACE_LIST_ACK);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_SHADOW_PALACE && NKCUIShadowPalace.IsInstanceOpen)
			{
				NKCUIShadowPalace.GetInstance().OpenRank();
			}
		}

		// Token: 0x0600596C RID: 22892 RVA: 0x001B148A File Offset: 0x001AF68A
		public static void OnRecv(NKMPacket_LEADERBOARD_FIERCE_LIST_ACK cNKMPacket_LEADERBOARD_FIERCE_LIST_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_LEADERBOARD_FIERCE_LIST_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCLeaderBoardManager.OnRecv(cNKMPacket_LEADERBOARD_FIERCE_LIST_ACK);
		}

		// Token: 0x0600596D RID: 22893 RVA: 0x001B14A7 File Offset: 0x001AF6A7
		public static void OnRecv(NKMPacket_LEADERBOARD_GUILD_LEVEL_RANK_LIST_ACK sPacket)
		{
			NKCLeaderBoardManager.OnRecv(sPacket);
		}

		// Token: 0x0600596E RID: 22894 RVA: 0x001B14AF File Offset: 0x001AF6AF
		public static void OnRecv(NKMPacket_LEADERBOARD_GUILD_UNION_RANK_LIST_ACK sPacket)
		{
			NKCLeaderBoardManager.OnRecv(sPacket);
		}

		// Token: 0x0600596F RID: 22895 RVA: 0x001B14B7 File Offset: 0x001AF6B7
		public static void OnRecv(NKMPacket_LEADERBOARD_TIMEATTACK_LIST_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCLeaderBoardManager.OnRecv(sPacket);
		}

		// Token: 0x06005970 RID: 22896 RVA: 0x001B14D4 File Offset: 0x001AF6D4
		public static void OnRecv(NKMPacket_SHADOW_PALACE_START_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				nkmuserData.m_InventoryData.UpdateItemInfo(sPacket.costItemDataList);
				nkmuserData.m_ShadowPalace.currentPalaceId = sPacket.currentPalaceId;
				nkmuserData.m_ShadowPalace.life = 3;
				nkmuserData.m_ShadowPalace.rewardMultiply = sPacket.rewardMultiply;
				NKMPalaceData nkmpalaceData = nkmuserData.m_ShadowPalace.palaceDataList.Find((NKMPalaceData v) => v.palaceId == sPacket.currentPalaceId);
				if (nkmpalaceData == null)
				{
					nkmpalaceData = new NKMPalaceData();
					nkmpalaceData.palaceId = sPacket.currentPalaceId;
					nkmuserData.m_ShadowPalace.palaceDataList.Add(nkmpalaceData);
				}
				nkmpalaceData.currentDungeonId = 0;
				for (int i = 0; i < nkmpalaceData.dungeonDataList.Count; i++)
				{
					nkmpalaceData.dungeonDataList[i].recentTime = 0;
				}
			}
			NKCScenManager.GetScenManager().Get_NKC_SCEN_SHADOW_BATTLE().SetShadowPalaceID(sPacket.currentPalaceId);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_SHADOW_BATTLE, true);
		}

		// Token: 0x06005971 RID: 22897 RVA: 0x001B1600 File Offset: 0x001AF800
		public static void OnRecv(NKMPacket_SHADOW_PALACE_GIVEUP_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				nkmuserData.m_ShadowPalace.currentPalaceId = 0;
			}
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_SHADOW_PALACE, true);
		}

		// Token: 0x06005972 RID: 22898 RVA: 0x001B1644 File Offset: 0x001AF844
		public static void OnRecv(NKMPacket_SHADOW_PALACE_SKIP_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItems);
			}
			if (sPacket.rewardDatas != null)
			{
				foreach (NKMRewardData rewardData in sPacket.rewardDatas)
				{
					myUserData.GetReward(rewardData);
				}
			}
			NKCPopupOpSkipProcess.Instance.Open(sPacket.rewardDatas, new List<UnitLoyaltyUpdateData>(), null);
		}

		// Token: 0x06005973 RID: 22899 RVA: 0x001B16EC File Offset: 0x001AF8EC
		public static void OnRecv(NKMPacket_FIERCE_DATA_ACK sPacket)
		{
			if (NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
				if (nkcfierceBattleSupportDataMgr != null)
				{
					nkcfierceBattleSupportDataMgr.UpdateFierceData(sPacket);
				}
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_HOME)
			{
				NKC_SCEN_HOME scen_HOME = NKCScenManager.GetScenManager().Get_SCEN_HOME();
				if (scen_HOME != null)
				{
					scen_HOME.UpdateRightSide3DButton(NKCUILobbyV2.eUIMenu.Worldmap);
				}
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_FIERCE_BATTLE_SUPPORT)
			{
				NKC_SCEN_FIERCE_BATTLE_SUPPORT nkc_SCEN_FIERCE_BATTLE_SUPPORT = NKCScenManager.GetScenManager().Get_NKC_SCEN_FIERCE_BATTLE_SUPPORT();
				if (nkc_SCEN_FIERCE_BATTLE_SUPPORT == null)
				{
					return;
				}
				if (nkc_SCEN_FIERCE_BATTLE_SUPPORT.Get_NKC_SCEN_STATE() == NKC_SCEN_STATE.NSS_DATA_REQ_WAIT)
				{
					nkc_SCEN_FIERCE_BATTLE_SUPPORT.ScenDataReqWaitUpdate();
					nkc_SCEN_FIERCE_BATTLE_SUPPORT.SetDataReq(true);
					return;
				}
				nkc_SCEN_FIERCE_BATTLE_SUPPORT.ScenUpdate();
			}
		}

		// Token: 0x06005974 RID: 22900 RVA: 0x001B1780 File Offset: 0x001AF980
		public static void OnRecv(NKMPacket_FIERCE_SEASON_NOT sPacket)
		{
			NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
			if (nkcfierceBattleSupportDataMgr != null)
			{
				nkcfierceBattleSupportDataMgr.Init(sPacket.fierceId);
			}
		}

		// Token: 0x06005975 RID: 22901 RVA: 0x001B17A8 File Offset: 0x001AF9A8
		public static void OnRecv(NKMPacket_FIERCE_PROFILE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKM_SCEN_ID nowScenID = NKCScenManager.GetScenManager().GetNowScenID();
			if (nowScenID == NKM_SCEN_ID.NSI_FIERCE_BATTLE_SUPPORT || nowScenID == NKM_SCEN_ID.NSI_HOME)
			{
				NKCPopupFierceUserInfo.Instance.Open(sPacket);
			}
		}

		// Token: 0x06005976 RID: 22902 RVA: 0x001B17EC File Offset: 0x001AF9EC
		public static void OnRecv(NKMPacket_LEADERBOARD_FIERCE_BOSSGROUP_LIST_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
			if (nkcfierceBattleSupportDataMgr != null)
			{
				nkcfierceBattleSupportDataMgr.UpdateFierceData(sPacket);
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_FIERCE_BATTLE_SUPPORT)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_FIERCE_BATTLE_SUPPORT().OnRecv(sPacket);
			}
		}

		// Token: 0x06005977 RID: 22903 RVA: 0x001B1844 File Offset: 0x001AFA44
		public static void OnRecv(NKMPacket_FIERCE_COMPLETE_RANK_REWARD_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (sPacket.rewardData != null && myUserData != null)
			{
				myUserData.GetReward(sPacket.rewardData);
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_FIERCE_BATTLE_SUPPORT)
			{
				NKCUIPopupFierceBattleRankReward.Instance.Open(sPacket.rewardData);
			}
			NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
			if (nkcfierceBattleSupportDataMgr != null)
			{
				nkcfierceBattleSupportDataMgr.SetReceivedRankReward();
			}
		}

		// Token: 0x06005978 RID: 22904 RVA: 0x001B18BC File Offset: 0x001AFABC
		public static void OnRecv(NKMPacket_FIERCE_COMPLETE_POINT_REWARD_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				if (myUserData.m_InventoryData != null)
				{
					if (sPacket.rewardData != null && sPacket.rewardData.MiscItemDataList != null)
					{
						myUserData.m_InventoryData.AddItemMisc(sPacket.rewardData.MiscItemDataList);
					}
					if (sPacket.rewardData != null && sPacket.rewardData.EquipItemDataList != null)
					{
						myUserData.m_InventoryData.AddItemEquip(sPacket.rewardData.EquipItemDataList);
					}
				}
				if (sPacket.rewardData != null && sPacket.rewardData.MoldItemDataList != null)
				{
					foreach (NKMMoldItemData nkmmoldItemData in sPacket.rewardData.MoldItemDataList)
					{
						if (NKMItemManager.GetItemMoldTempletByID(nkmmoldItemData.m_MoldID) != null)
						{
							myUserData.m_CraftData.UpdateMoldItem(nkmmoldItemData.m_MoldID, nkmmoldItemData.m_Count);
						}
					}
				}
			}
			NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
			if (nkcfierceBattleSupportDataMgr != null)
			{
				nkcfierceBattleSupportDataMgr.UpdateRecevePointRewardID(sPacket.pointRewardId);
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_FIERCE_BATTLE_SUPPORT)
			{
				if (NKCUIPopupFierceBattleScoreReward.IsInstanceOpen)
				{
					NKCUIPopupFierceBattleScoreReward.Instance.Open();
				}
				NKCScenManager.GetScenManager().Get_NKC_SCEN_FIERCE_BATTLE_SUPPORT().OnRecv(sPacket);
			}
			NKCUIResult.Instance.OpenRewardGain(NKCScenManager.CurrentUserData().m_ArmyData, sPacket.rewardData, NKCUtilString.GET_FIERCE_BATTLE_POINT_REWARD_TITLE, "", null);
		}

		// Token: 0x06005979 RID: 22905 RVA: 0x001B1A38 File Offset: 0x001AFC38
		public static void OnRecv(NKMPacket_FIERCE_COMPLETE_POINT_REWARD_ALL_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				if (myUserData.m_InventoryData != null)
				{
					if (sPacket.rewardData != null && sPacket.rewardData.MiscItemDataList != null)
					{
						myUserData.m_InventoryData.AddItemMisc(sPacket.rewardData.MiscItemDataList);
					}
					if (sPacket.rewardData != null && sPacket.rewardData.EquipItemDataList != null)
					{
						myUserData.m_InventoryData.AddItemEquip(sPacket.rewardData.EquipItemDataList);
					}
				}
				if (sPacket.rewardData != null && sPacket.rewardData.MoldItemDataList != null)
				{
					foreach (NKMMoldItemData nkmmoldItemData in sPacket.rewardData.MoldItemDataList)
					{
						if (NKMItemManager.GetItemMoldTempletByID(nkmmoldItemData.m_MoldID) != null)
						{
							myUserData.m_CraftData.UpdateMoldItem(nkmmoldItemData.m_MoldID, nkmmoldItemData.m_Count);
						}
					}
				}
			}
			NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
			if (nkcfierceBattleSupportDataMgr != null)
			{
				foreach (int receivedPointRewardID in sPacket.pointRewardIds)
				{
					nkcfierceBattleSupportDataMgr.UpdateRecevePointRewardID(receivedPointRewardID);
				}
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_FIERCE_BATTLE_SUPPORT)
			{
				if (NKCUIPopupFierceBattleScoreReward.IsInstanceOpen)
				{
					NKCUIPopupFierceBattleScoreReward.Instance.Open();
				}
				NKCScenManager.GetScenManager().Get_NKC_SCEN_FIERCE_BATTLE_SUPPORT().OnRecv(sPacket);
			}
			NKCUIResult.Instance.OpenRewardGain(NKCScenManager.CurrentUserData().m_ArmyData, sPacket.rewardData, NKCUtilString.GET_FIERCE_BATTLE_POINT_REWARD_TITLE, "", null);
		}

		// Token: 0x0600597A RID: 22906 RVA: 0x001B1BF0 File Offset: 0x001AFDF0
		public static void OnRecv(NKMPacket_FIERCE_PENALTY_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
			if (nkcfierceBattleSupportDataMgr != null)
			{
				nkcfierceBattleSupportDataMgr.SetSelfPenalty(sPacket.penaltyIds);
			}
		}

		// Token: 0x0600597B RID: 22907 RVA: 0x001B1C2C File Offset: 0x001AFE2C
		public static void OnRecv(NKMPacket_FIERCE_DAILY_REWARD_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
			if (nkcfierceBattleSupportDataMgr != null)
			{
				nkcfierceBattleSupportDataMgr.SetDailyRewardReceived(sPacket.fierceDailyRewardReceived);
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				myUserData.GetReward(sPacket.rewardData);
			}
			NKCPopupMessageToastSimple.Instance.Open(sPacket.rewardData, null, null);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_FIERCE_BATTLE_SUPPORT)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_FIERCE_BATTLE_SUPPORT().OnRecv(sPacket);
			}
		}

		// Token: 0x0600597C RID: 22908 RVA: 0x001B1CB2 File Offset: 0x001AFEB2
		public static void OnRecv(NKMPacket_UPDATE_MARKET_REVIEW_ACK sPacket)
		{
			if (sPacket.errorCode != NKM_ERROR_CODE.NEC_OK)
			{
				return;
			}
			NKCPublisherModule.Marketing.SetMarketReviewCompleted();
		}

		// Token: 0x0600597D RID: 22909 RVA: 0x001B1CC8 File Offset: 0x001AFEC8
		public static void OnRecv(NKMPacket_EVENT_PASS_NOT sPacket)
		{
			NKCEventPassDataManager eventPassDataManager = NKCScenManager.GetScenManager().GetEventPassDataManager();
			if (eventPassDataManager != null)
			{
				eventPassDataManager.EventPassId = sPacket.eventPassId;
				eventPassDataManager.EventPassDataReceived = false;
			}
			NKCUIEventPass.RewardRedDot = false;
			NKCUIEventPass.DailyMissionRedDot = false;
			NKCUIEventPass.WeeklyMissionRedDot = false;
			NKCUIEventPass.EventPassDataManager = null;
			NKCPacketSender.Send_NKMPacket_EVENT_PASS_REQ(NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID);
		}

		// Token: 0x0600597E RID: 22910 RVA: 0x001B1D14 File Offset: 0x001AFF14
		public static void OnRecv(NKMPacket_EVENT_PASS_ACK cNKMPacket_EVENT_PASS_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_EVENT_PASS_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCEventPassDataManager eventPassDataManager = NKCScenManager.GetScenManager().GetEventPassDataManager();
			if (eventPassDataManager != null)
			{
				eventPassDataManager.SetEventPassData(cNKMPacket_EVENT_PASS_ACK);
			}
			if (NKCUIEventPass.OpenUIStandby)
			{
				NKCUIEventPass.Instance.Open(eventPassDataManager);
				NKCUIEventPass.OpenUIStandby = false;
			}
		}

		// Token: 0x0600597F RID: 22911 RVA: 0x001B1D64 File Offset: 0x001AFF64
		public static void OnRecv(NKMPacket_EVENT_PASS_LEVEL_COMPLETE_ACK cNKMPacket_EVENT_PASS_LEVEL_COMPLETE_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_EVENT_PASS_LEVEL_COMPLETE_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				myUserData.GetReward(cNKMPacket_EVENT_PASS_LEVEL_COMPLETE_ACK.rewardData);
			}
			if (NKCUIEventPass.IsInstanceOpen)
			{
				NKCUIEventPass.Instance.OnRecv(cNKMPacket_EVENT_PASS_LEVEL_COMPLETE_ACK);
			}
		}

		// Token: 0x06005980 RID: 22912 RVA: 0x001B1DB2 File Offset: 0x001AFFB2
		public static void OnRecv(NKMPacket_EVENT_PASS_MISSION_ACK cNKMPacket_EVENT_PASS_MISSION_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_EVENT_PASS_MISSION_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCUIEventPass.HasInstance)
			{
				NKCUIEventPass.Instance.OnRecv(cNKMPacket_EVENT_PASS_MISSION_ACK);
			}
		}

		// Token: 0x06005981 RID: 22913 RVA: 0x001B1DDB File Offset: 0x001AFFDB
		public static void OnRecv(NKMPacket_EVENT_PASS_FINAL_MISSION_COMPLETE_ACK cNKMPacket_EVENT_PASS_FINAL_MISSION_COMPLETE_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_EVENT_PASS_FINAL_MISSION_COMPLETE_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCUIEventPass.IsInstanceOpen)
			{
				NKCUIEventPass.Instance.RefreshFinalMissionCompleted(cNKMPacket_EVENT_PASS_FINAL_MISSION_COMPLETE_ACK);
			}
		}

		// Token: 0x06005982 RID: 22914 RVA: 0x001B1E04 File Offset: 0x001B0004
		public static void OnRecv(NKMPacket_EVENT_PASS_DAILY_MISSION_RETRY_ACK cNKMPacket_EVENT_PASS_DAILY_MISSION_RETRY_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_EVENT_PASS_DAILY_MISSION_RETRY_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				myUserData.m_InventoryData.UpdateItemInfo(cNKMPacket_EVENT_PASS_DAILY_MISSION_RETRY_ACK.costItems);
			}
			if (NKCUIEventPass.IsInstanceOpen)
			{
				NKCUIEventPass.Instance.RefreshSelectedDailyMissionSlot(cNKMPacket_EVENT_PASS_DAILY_MISSION_RETRY_ACK.missionInfo);
			}
		}

		// Token: 0x06005983 RID: 22915 RVA: 0x001B1E5C File Offset: 0x001B005C
		public static void OnRecv(NKMPacket_EVENT_PASS_PURCHASE_CORE_PASS_ACK cNKMPacket_EVENT_PASS_PURCHASE_CORE_PASS_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_EVENT_PASS_PURCHASE_CORE_PASS_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				myUserData.m_InventoryData.UpdateItemInfo(cNKMPacket_EVENT_PASS_PURCHASE_CORE_PASS_ACK.costItemList);
			}
			if (NKCPopupEventPassPurchase.IsInstanceOpen)
			{
				NKCPopupEventPassPurchase.Instance.Close();
			}
			NKCEventPassDataManager eventPassDataManager = NKCScenManager.GetScenManager().GetEventPassDataManager();
			if (eventPassDataManager != null)
			{
				eventPassDataManager.CorePassPurchased = true;
			}
			NKCAdjustManager.OnCustomEvent("38_counterpass_sp");
			if (NKCUIEventPass.IsInstanceOpen)
			{
				NKCUIEventPass.Instance.RefreshPurchaseCorePass(true, -1);
			}
		}

		// Token: 0x06005984 RID: 22916 RVA: 0x001B1EE0 File Offset: 0x001B00E0
		public static void OnRecv(NKMPacket_EVENT_PASS_PURCHASE_CORE_PASS_PLUS_ACK cNKMPacket_EVENT_PASS_PURCHASE_CORE_PASS_PLUS_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_EVENT_PASS_PURCHASE_CORE_PASS_PLUS_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				myUserData.m_InventoryData.UpdateItemInfo(cNKMPacket_EVENT_PASS_PURCHASE_CORE_PASS_PLUS_ACK.costItemList);
			}
			if (NKCPopupEventPassPurchase.IsInstanceOpen)
			{
				NKCPopupEventPassPurchase.Instance.Close();
			}
			NKCEventPassDataManager eventPassDataManager = NKCScenManager.GetScenManager().GetEventPassDataManager();
			if (eventPassDataManager != null)
			{
				eventPassDataManager.CorePassPurchased = true;
			}
			NKCAdjustManager.OnCustomEvent("38_counterpass_spplus");
			if (NKCUIEventPass.IsInstanceOpen)
			{
				NKCUIEventPass.Instance.RefreshPurchaseCorePass(true, cNKMPacket_EVENT_PASS_PURCHASE_CORE_PASS_PLUS_ACK.totalExp);
			}
		}

		// Token: 0x06005985 RID: 22917 RVA: 0x001B1F69 File Offset: 0x001B0169
		public static void OnRecv(NKMPacket_EVENT_PASS_DOT_NOT cNKMPacket_EVENT_PASS_LOBBY_DOT_NOT)
		{
			NKCUIEventPass.RewardRedDot = cNKMPacket_EVENT_PASS_LOBBY_DOT_NOT.passLevelDot;
			NKCUIEventPass.DailyMissionRedDot = cNKMPacket_EVENT_PASS_LOBBY_DOT_NOT.dailyMissionDot;
			NKCUIEventPass.WeeklyMissionRedDot = cNKMPacket_EVENT_PASS_LOBBY_DOT_NOT.weeklyMissionDot;
		}

		// Token: 0x06005986 RID: 22918 RVA: 0x001B1F8C File Offset: 0x001B018C
		public static void OnRecv(NKMPacket_EVENT_PASS_LEVEL_UP_ACK cNKMPacket_EVENT_PASS_LEVEL_UP_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_EVENT_PASS_LEVEL_UP_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				myUserData.m_InventoryData.UpdateItemInfo(cNKMPacket_EVENT_PASS_LEVEL_UP_ACK.costItemList);
			}
			if (NKCUIEventPass.IsInstanceOpen)
			{
				NKCUIEventPass.Instance.RefreshPassTotalExpRelatedInfo(cNKMPacket_EVENT_PASS_LEVEL_UP_ACK.totalExp, false, NKCUIEventPass.ExpFXType.DIRECT);
			}
			NKCAdjustManager.OnCustomEvent("38_counterpass_lvup");
		}

		// Token: 0x06005987 RID: 22919 RVA: 0x001B1FF0 File Offset: 0x001B01F0
		public static void OnRecv(NKMPacket_OPERATOR_LEVELUP_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.CurrentUserData().m_InventoryData.UpdateItemInfo(sPacket.costItemData);
			NKCScenManager.CurrentUserData().m_ArmyData.UpdateOperatorData(sPacket.operatorUnit);
		}

		// Token: 0x06005988 RID: 22920 RVA: 0x001B2040 File Offset: 0x001B0240
		public static void OnRecv(NKMPacket_OPERATOR_ENHANCE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.CurrentUserData().m_InventoryData.UpdateItemInfo(sPacket.costItemData);
			NKMOperator operatorData = NKCOperatorUtil.GetOperatorData(sPacket.operatorUnit.uid);
			NKMOperator operatorData2 = NKCOperatorUtil.GetOperatorData(sPacket.sourceUnitUid);
			bool bTryMainSkill = false;
			bool bMainSkillLvUp = false;
			bool bTrySubskill = false;
			bool bSubskillLvUp = false;
			bool bImplantSubskill = false;
			if (operatorData2.mainSkill.id == sPacket.operatorUnit.mainSkill.id && !NKCOperatorUtil.IsMaximumSkillLevel(operatorData.mainSkill.id, (int)operatorData.mainSkill.level))
			{
				bTryMainSkill = true;
				bMainSkillLvUp = (operatorData.mainSkill.level < sPacket.operatorUnit.mainSkill.level);
			}
			if (operatorData.subSkill.id == operatorData2.subSkill.id && !NKCOperatorUtil.IsMaximumSkillLevel(operatorData.subSkill.id, (int)operatorData.subSkill.level))
			{
				bTrySubskill = true;
				bSubskillLvUp = (operatorData.subSkill.level < sPacket.operatorUnit.subSkill.level);
			}
			if (sPacket.transSkill)
			{
				bImplantSubskill = (operatorData.subSkill.id != sPacket.operatorUnit.subSkill.id);
			}
			int id = operatorData.subSkill.id;
			int level = (int)operatorData.subSkill.level;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				nkmuserData.m_ArmyData.RemoveOperatorEx(sPacket.sourceUnitUid);
			}
			NKCScenManager.CurrentUserData().m_ArmyData.UpdateOperatorData(sPacket.operatorUnit);
			if (NKCUIOperatorInfoPopupSkill.Instance.IsOpen)
			{
				NKCUIOperatorInfoPopupSkill.Instance.OnRecv(bTryMainSkill, bMainSkillLvUp, bTrySubskill, bSubskillLvUp, sPacket.transSkill, bImplantSubskill, id, level);
			}
		}

		// Token: 0x06005989 RID: 22921 RVA: 0x001B21F4 File Offset: 0x001B03F4
		public static void OnRecv(NKMPacket_OPERATOR_LOCK_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCOperatorUtil.UpdateLockState(sPacket.unitUID, sPacket.locked);
			NKMOperator operatorData = NKCOperatorUtil.GetOperatorData(sPacket.unitUID);
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			if (armyData != null)
			{
				armyData.UpdateOperatorData(operatorData);
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_UNIT_LIST && NKCUIOperatorInfo.IsInstanceOpen)
			{
				NKCUIOperatorInfo.Instance.UpdateLockState(sPacket.unitUID);
			}
		}

		// Token: 0x0600598A RID: 22922 RVA: 0x001B2274 File Offset: 0x001B0474
		public static void OnRecv(NKMPacket_OPERATOR_REMOVE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			nkmuserData.m_InventoryData.AddItemMisc(sPacket.rewardItemDataList);
			NKMArmyData armyData = nkmuserData.m_ArmyData;
			armyData.RemoveOperator(sPacket.removeUnitUIDList);
			armyData.AddUnitDeleteRewardList(sPacket.rewardItemDataList);
			if (!armyData.IsEmptyUnitDeleteList)
			{
				NKCPacketSender.Send_NKMPacket_OPERATOR_REMOVE_REQ();
				return;
			}
			if (NKCUIUnitSelectList.IsInstanceOpen)
			{
				NKCUIUnitSelectList.Instance.CloseRemoveMode();
				NKCUIUnitSelectList.Instance.ClearMultipleSelect();
				NKCUINPCMachineGap.PlayVoice(NPC_TYPE.MACHINE_GAP, NPC_ACTION_TYPE.DISMISSAL_RESULT, true);
			}
			if (armyData.GetUnitDeleteReward().Count > 0)
			{
				NKCUIResult.Instance.OpenItemGain(armyData.GetUnitDeleteReward(), NKCUtilString.GET_STRING_ITEM_GAIN, NKCUtilString.GET_STRING_REMOVE_UNIT, null);
			}
		}

		// Token: 0x0600598B RID: 22923 RVA: 0x001B2328 File Offset: 0x001B0528
		public static void OnRecv(NKMPacket_DECK_OPERATOR_SET_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			long num = 0L;
			NKMOperator deckOperatorByIndex = armyData.GetDeckOperatorByIndex(sPacket.deckIndex);
			if (deckOperatorByIndex != null)
			{
				num = deckOperatorByIndex.uid;
			}
			armyData.SetDeckOperatorByIndex(sPacket.oldDeckIndex.m_eDeckType, (int)sPacket.oldDeckIndex.m_iIndex, 0L);
			armyData.SetDeckOperatorByIndex(sPacket.deckIndex.m_eDeckType, (int)sPacket.deckIndex.m_iIndex, sPacket.operatorUid);
			if (NKCUIDeckViewer.IsInstanceOpen)
			{
				NKCUIDeckViewer.Instance.OnRecv(sPacket);
			}
			if (NKCUIUnitSelectList.IsInstanceOpen && num != 0L)
			{
				NKCUIUnitSelectList.Instance.ChangeUnitDeckIndex(num, new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE));
			}
		}

		// Token: 0x0600598C RID: 22924 RVA: 0x001B23E0 File Offset: 0x001B05E0
		public static void OnRecv(NKMPacket_EQUIP_PRESET_LIST_ACK cPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetMyUserData() == null)
			{
				return;
			}
			NKCEquipPresetDataManager.ListEquipPresetData = cPacket.presetDatas;
			NKCEquipPresetDataManager.RefreshEquipUidHash();
			if (NKCEquipPresetDataManager.OpenUI)
			{
				NKCUIUnitInfo.Instance.OnRecv(cPacket);
			}
		}

		// Token: 0x0600598D RID: 22925 RVA: 0x001B2434 File Offset: 0x001B0634
		public static void OnRecv(NKMPacket_EQUIP_PRESET_ADD_ACK cPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				myUserData.m_InventoryData.UpdateItemInfo(cPacket.costItemDataList);
			}
			NKCUIEquipPreset equipPreset = NKCUIUnitInfo.Instance.EquipPreset;
			if (equipPreset == null)
			{
				return;
			}
			equipPreset.AddPresetSlot(cPacket.totalPresetCount);
		}

		// Token: 0x0600598E RID: 22926 RVA: 0x001B248F File Offset: 0x001B068F
		public static void OnRecv(NKMPacket_EQUIP_PRESET_CHANGE_NAME_ACK cPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCUIEquipPreset equipPreset = NKCUIUnitInfo.Instance.EquipPreset;
			if (equipPreset == null)
			{
				return;
			}
			equipPreset.UpdatePresetName(cPacket.presetIndex, cPacket.newPresetName);
		}

		// Token: 0x0600598F RID: 22927 RVA: 0x001B24C6 File Offset: 0x001B06C6
		public static void OnRecv(NKMPacket_EQUIP_PRESET_REGISTER_ALL_ACK cPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCUIEquipPreset equipPreset = NKCUIUnitInfo.Instance.EquipPreset;
			if (equipPreset == null)
			{
				return;
			}
			equipPreset.UpdatePresetSlot(cPacket.presetData, true);
		}

		// Token: 0x06005990 RID: 22928 RVA: 0x001B24F8 File Offset: 0x001B06F8
		public static void OnRecv(NKMPacket_EQUIP_PRESET_REGISTER_ACK cPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCUIInventory.IsInstanceOpen)
			{
				NKCUIInventory.Instance.Close();
			}
			NKCUIEquipPreset equipPreset = NKCUIUnitInfo.Instance.EquipPreset;
			if (equipPreset != null)
			{
				equipPreset.UpdatePresetSlot(cPacket.presetData, false);
			}
			NKCPopupItemEquipBox.CloseItemBox();
		}

		// Token: 0x06005991 RID: 22929 RVA: 0x001B254C File Offset: 0x001B074C
		public static void OnRecv(NKMPacket_EQUIP_PRESET_APPLY_ACK cPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			bool flag = false;
			int count = cPacket.updateUnitDatas.Count;
			for (int i = 0; i < count; i++)
			{
				if (cPacket.updateUnitDatas[i] == null)
				{
					Debug.LogWarning(string.Format("updateUnitDatas {0} index is null", i));
				}
				else
				{
					NKMPacket_EQUIP_PRESET_APPLY_ACK.UnitEquipUidSet unitEquipUidSet = cPacket.updateUnitDatas[i];
					NKMUnitData unitFromUID = myUserData.m_ArmyData.GetUnitFromUID(unitEquipUidSet.unitUid);
					if (unitFromUID != null)
					{
						List<NKMUnitData> list = new List<NKMUnitData>();
						int count2 = unitEquipUidSet.equipUids.Count;
						for (int j = 0; j < count2; j++)
						{
							ITEM_EQUIP_POSITION item_EQUIP_POSITION = (ITEM_EQUIP_POSITION)j;
							long equipUid = unitFromUID.GetEquipUid(item_EQUIP_POSITION);
							if (unitEquipUidSet.equipUids[j] <= 0L)
							{
								if (equipUid > 0L && !unitFromUID.UnEquipItem(myUserData.m_InventoryData, equipUid, item_EQUIP_POSITION))
								{
									flag = true;
								}
							}
							else if (unitEquipUidSet.equipUids[j] != equipUid)
							{
								NKMEquipItemData itemEquip = myUserData.m_InventoryData.GetItemEquip(unitEquipUidSet.equipUids[j]);
								if (itemEquip.m_OwnerUnitUID > 0L)
								{
									NKMUnitData removeItemUnitData = myUserData.m_ArmyData.GetUnitFromUID(itemEquip.m_OwnerUnitUID);
									if (!removeItemUnitData.UnEquipItem(myUserData.m_InventoryData, unitEquipUidSet.equipUids[j]))
									{
										flag = true;
									}
									if (list.Find((NKMUnitData e) => e.m_UnitUID == removeItemUnitData.m_UnitUID) == null)
									{
										list.Add(removeItemUnitData);
									}
								}
								long num;
								if (!unitFromUID.EquipItem(myUserData.m_InventoryData, unitEquipUidSet.equipUids[j], out num, item_EQUIP_POSITION))
								{
									flag = true;
								}
							}
						}
						int count3 = list.Count;
						for (int k = 0; k < count3; k++)
						{
							if (list[k].m_UnitUID != unitFromUID.m_UnitUID)
							{
								myUserData.m_ArmyData.UpdateUnitData(list[k]);
							}
						}
						myUserData.m_ArmyData.UpdateUnitData(unitFromUID);
					}
				}
			}
			NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_GROWTH_EQUIP, NKCUIUnitInfo.Instance.GetNKMUnitData(), false, false);
			if (flag)
			{
				Debug.LogWarning("Some of EquipPreset apply failed");
			}
			NKCUIEquipPreset equipPreset = NKCUIUnitInfo.Instance.EquipPreset;
			if (equipPreset == null)
			{
				return;
			}
			equipPreset.UpdatePresetData(null, false, 0, false);
		}

		// Token: 0x06005992 RID: 22930 RVA: 0x001B27B4 File Offset: 0x001B09B4
		public static void OnRecv(NKMPacket_EQUIP_PRESET_NOT cPacket)
		{
			if (!NKCUIUnitInfo.IsInstanceOpen || !NKCUIUnitInfo.Instance.EquipPresetOpened())
			{
				if (cPacket.presetDatas != null)
				{
					NKCEquipPresetDataManager.ListEquipPresetData = cPacket.presetDatas;
				}
				NKCEquipPresetDataManager.RefreshEquipUidHash();
				return;
			}
			NKCUIEquipPreset equipPreset = NKCUIUnitInfo.Instance.EquipPreset;
			if (equipPreset == null)
			{
				return;
			}
			equipPreset.UpdatePresetData(cPacket.presetDatas, false, 0, true);
		}

		// Token: 0x06005993 RID: 22931 RVA: 0x001B280C File Offset: 0x001B0A0C
		public static void OnRecv(NKMPacket_EQUIP_PRESET_CHANGE_INDEX_ACK cPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (!NKCUIUnitInfo.IsInstanceOpen || !NKCUIUnitInfo.Instance.EquipPresetOpened())
			{
				if (cPacket.presetDatas != null)
				{
					NKCEquipPresetDataManager.ListEquipPresetData = cPacket.presetDatas;
				}
				return;
			}
			NKCUIEquipPreset equipPreset = NKCUIUnitInfo.Instance.EquipPreset;
			if (equipPreset == null)
			{
				return;
			}
			equipPreset.UpdatePresetData(cPacket.presetDatas, false, 0, true);
		}

		// Token: 0x06005994 RID: 22932 RVA: 0x001B2874 File Offset: 0x001B0A74
		public static void OnRecv(NKMPacket_CHARGE_ITEM_NOT sPacket)
		{
			if (sPacket.itemData == null || sPacket.itemData.ItemID == 0)
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				nkmuserData.SetUpdateDate(sPacket);
				nkmuserData.m_InventoryData.UpdateItemInfo(sPacket.itemData);
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_HOME)
				{
					NKCScenManager.GetScenManager().Get_SCEN_HOME().RefreshRechargeEternium();
				}
			}
		}

		// Token: 0x06005995 RID: 22933 RVA: 0x001B28D4 File Offset: 0x001B0AD4
		public static void OnRecv(NKMPACKET_RACE_TEAM_SELECT_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCUIEventSubUIRace.RaceSummary != null)
			{
				NKCUIEventSubUIRace.RaceSummary.racePrivate = sPacket.racePrivate;
				if (NKCUIEvent.IsInstanceOpen)
				{
					NKCUIEvent.Instance.RefreshUI(0);
				}
			}
			NKCUIEventSubUIRace.OpenRace();
		}

		// Token: 0x06005996 RID: 22934 RVA: 0x001B2924 File Offset: 0x001B0B24
		public static void OnRecv(NKMPACKET_RACE_START_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				myUserData.m_InventoryData.UpdateItemInfo(sPacket.costItemList);
				myUserData.GetReward(sPacket.rewardData);
			}
			if (NKCUIEventSubUIRace.RaceSummary != null)
			{
				NKCUIEventSubUIRace.RaceSummary.racePrivate = sPacket.racePrivate;
				if (NKCUIEvent.IsInstanceOpen)
				{
					NKCUIEvent.Instance.RefreshUI(0);
				}
			}
			if (NKCPopupEventRace.IsInstanceOpen)
			{
				NKCPopupEventRace.Instance.OnRecv(sPacket);
			}
		}

		// Token: 0x06005997 RID: 22935 RVA: 0x001B29AC File Offset: 0x001B0BAC
		public static void OnRecv(NKMPACKET_RACE_RESET_NOT sPacket)
		{
			NKCUIEventSubUIRace.RaceDay = sPacket.currentRaceIndex;
			NKCUIEventSubUIRace.RaceSummary = sPacket.summary;
			if (NKCUIEvent.IsInstanceOpen)
			{
				NKCUIEvent.Instance.RefreshUI(0);
			}
		}

		// Token: 0x06005998 RID: 22936 RVA: 0x001B29D8 File Offset: 0x001B0BD8
		public static void OnRecv(NKMPacket_DUNGEON_SKIP_ACK cPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				myUserData.m_InventoryData.UpdateItemInfo(cPacket.costItems);
				if (cPacket.rewardDatas != null)
				{
					int count = cPacket.rewardDatas.Count;
					for (int i = 0; i < count; i++)
					{
						NKCPacketHandlersLobby.UpdateUserData(true, cPacket.rewardDatas[i].dungeonClearData, cPacket.rewardDatas[i].episodeCompleteData, null, null, null, null, null, null, null, null);
					}
				}
				if (cPacket.updatedUnits != null)
				{
					foreach (UnitLoyaltyUpdateData unitLoyaltyUpdateData in cPacket.updatedUnits)
					{
						NKMUnitData unitFromUID = myUserData.m_ArmyData.GetUnitFromUID(unitLoyaltyUpdateData.unitUid);
						if (unitFromUID != null)
						{
							unitFromUID.loyalty = unitLoyaltyUpdateData.loyalty;
							unitFromUID.SetOfficeRoomId(unitLoyaltyUpdateData.officeRoomId, unitLoyaltyUpdateData.officeGrade, unitLoyaltyUpdateData.heartGaugeStartTime);
						}
					}
				}
				if (cPacket.stagePlayData != null)
				{
					myUserData.UpdateStagePlayData(cPacket.stagePlayData);
				}
			}
			NKCPopupOpSkipProcess.Instance.Open(cPacket.rewardDatas, cPacket.updatedUnits, null);
			if (NKCUIOperationNodeViewer.isOpen())
			{
				NKCUIOperationNodeViewer.Instance.Refresh();
			}
		}

		// Token: 0x06005999 RID: 22937 RVA: 0x001B2B34 File Offset: 0x001B0D34
		public static void OnRecv(NKMPacket_OFFICE_OPEN_SECTION_ACK cPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				myUserData.m_InventoryData.UpdateItemInfo(cPacket.costItems);
				myUserData.OfficeData.UpdateSectionData(cPacket.sectionId, cPacket.newRooms);
			}
			NKCUIOfficeMapFront instance = NKCUIOfficeMapFront.GetInstance();
			if (instance != null)
			{
				instance.UpdateSectionLockState(cPacket.sectionId);
				instance.GetCurrentMinimap().UpdateRoomStateInSection(cPacket.sectionId);
			}
		}

		// Token: 0x0600599A RID: 22938 RVA: 0x001B2BB8 File Offset: 0x001B0DB8
		public static void OnRecv(NKMPacket_OFFICE_OPEN_ROOM_ACK cPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				myUserData.m_InventoryData.UpdateItemInfo(cPacket.costItems);
				myUserData.OfficeData.UpdateRoomData(cPacket.room);
			}
			NKCUIOfficeMapFront instance = NKCUIOfficeMapFront.GetInstance();
			if (instance == null)
			{
				return;
			}
			IOfficeMinimap currentMinimap = instance.GetCurrentMinimap();
			if (currentMinimap == null)
			{
				return;
			}
			currentMinimap.UpdateRoomStateAll();
		}

		// Token: 0x0600599B RID: 22939 RVA: 0x001B2C24 File Offset: 0x001B0E24
		public static void OnRecv(NKMPacket_OFFICE_SET_ROOM_NAME_ACK cPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				nkmuserData.OfficeData.UpdateRoomData(cPacket.room);
			}
			NKCUIOfficeMapFront instance = NKCUIOfficeMapFront.GetInstance();
			if (instance != null)
			{
				IOfficeMinimap currentMinimap = instance.GetCurrentMinimap();
				if (currentMinimap != null)
				{
					currentMinimap.UpdateRoomInfo(cPacket.room);
				}
			}
			if (NKCUIPopupOfficeMemberEdit.IsInstanceOpen)
			{
				NKCUIPopupOfficeMemberEdit.Instance.UpdateRoomName(cPacket.room.name);
			}
		}

		// Token: 0x0600599C RID: 22940 RVA: 0x001B2CA0 File Offset: 0x001B0EA0
		public static void OnRecv(NKMPacket_OFFICE_SET_ROOM_UNIT_ACK cPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			int count = cPacket.rooms.Count;
			if (nkmuserData != null)
			{
				int count2 = cPacket.units.Count;
				for (int i = 0; i < count2; i++)
				{
					nkmuserData.m_ArmyData.UpdateUnitData(cPacket.units[i]);
				}
				for (int j = 0; j < count; j++)
				{
					nkmuserData.OfficeData.UpdateRoomData(cPacket.rooms[j]);
				}
			}
			if (NKCUIOfficeMapFront.IsInstanceOpen)
			{
				IOfficeMinimap currentMinimap = NKCUIOfficeMapFront.GetInstance().GetCurrentMinimap();
				if (currentMinimap != null)
				{
					for (int k = 0; k < count; k++)
					{
						currentMinimap.UpdateRoomInfo(cPacket.rooms[k]);
					}
				}
			}
			if (NKCUIOffice.IsInstanceOpen)
			{
				NKCUIOffice instance = NKCUIOffice.GetInstance();
				if (instance != null)
				{
					instance.OnRoomUnitUpdated();
				}
			}
			NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_OFFICE_ASSIGN_COMPLETE, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
		}

		// Token: 0x0600599D RID: 22941 RVA: 0x001B2DA8 File Offset: 0x001B0FA8
		public static void OnRecv(NKMPacket_OFFICE_SET_ROOM_WALL_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.CurrentUserData().OfficeData.UpdateRoomData(sPacket.room);
			NKCScenManager.CurrentUserData().m_ArmyData.UpdateUnitData(sPacket.updatedUnits);
			if (NKCUIOffice.IsInstanceOpen)
			{
				NKCUIOffice instance = NKCUIOffice.GetInstance();
				if (instance != null)
				{
					instance.OnApplyDecoration(sPacket.room.wallInteriorId);
				}
			}
		}

		// Token: 0x0600599E RID: 22942 RVA: 0x001B2E1C File Offset: 0x001B101C
		public static void OnRecv(NKMPacket_OFFICE_SET_ROOM_FLOOR_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.CurrentUserData().OfficeData.UpdateRoomData(sPacket.room);
			NKCScenManager.CurrentUserData().m_ArmyData.UpdateUnitData(sPacket.updatedUnits);
			if (NKCUIOffice.IsInstanceOpen)
			{
				NKCUIOffice instance = NKCUIOffice.GetInstance();
				if (instance != null)
				{
					instance.OnApplyDecoration(sPacket.room.floorInteriorId);
				}
			}
		}

		// Token: 0x0600599F RID: 22943 RVA: 0x001B2E90 File Offset: 0x001B1090
		public static void OnRecv(NKMPacket_OFFICE_SET_ROOM_BACKGROUND_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.CurrentUserData().OfficeData.UpdateRoomData(sPacket.room);
			NKCScenManager.CurrentUserData().m_ArmyData.UpdateUnitData(sPacket.updatedUnits);
			if (NKCUIOffice.IsInstanceOpen)
			{
				NKCUIOffice instance = NKCUIOffice.GetInstance();
				if (instance != null)
				{
					instance.OnApplyDecoration(sPacket.room.backgroundId);
				}
			}
		}

		// Token: 0x060059A0 RID: 22944 RVA: 0x001B2F04 File Offset: 0x001B1104
		public static void OnRecv(NKMPacket_OFFICE_ADD_FURNITURE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.CurrentUserData().OfficeData.UpdateRoomData(sPacket.room);
			NKCScenManager.CurrentUserData().OfficeData.UpdateInteriorData(sPacket.changedInterior);
			NKCScenManager.CurrentUserData().m_ArmyData.UpdateUnitData(sPacket.updatedUnits);
			if (NKCUIOffice.IsInstanceOpen)
			{
				NKCUIOffice instance = NKCUIOffice.GetInstance();
				if (instance != null)
				{
					instance.OnAddFurniture(sPacket.room.id, sPacket.furniture);
				}
			}
		}

		// Token: 0x060059A1 RID: 22945 RVA: 0x001B2F94 File Offset: 0x001B1194
		public static void OnRecv(NKMPacket_OFFICE_UPDATE_FURNITURE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.CurrentUserData().OfficeData.UpdateRoomData(sPacket.room);
			if (NKCUIOffice.IsInstanceOpen)
			{
				NKCUIOffice instance = NKCUIOffice.GetInstance();
				if (instance != null)
				{
					instance.OnFurnitureMove(sPacket.room.id, sPacket.furniture);
				}
			}
		}

		// Token: 0x060059A2 RID: 22946 RVA: 0x001B2FF8 File Offset: 0x001B11F8
		public static void OnRecv(NKMPacket_OFFICE_REMOVE_FURNITURE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.CurrentUserData().OfficeData.UpdateRoomData(sPacket.room);
			NKCScenManager.CurrentUserData().OfficeData.UpdateInteriorData(sPacket.changedInterior);
			NKCScenManager.CurrentUserData().m_ArmyData.UpdateUnitData(sPacket.updatedUnits);
			if (NKCUIOffice.IsInstanceOpen)
			{
				NKCUIOffice instance = NKCUIOffice.GetInstance();
				if (instance != null)
				{
					instance.OnRemoveFurniture(sPacket.room.id, sPacket.furnitureUid);
				}
			}
		}

		// Token: 0x060059A3 RID: 22947 RVA: 0x001B3088 File Offset: 0x001B1288
		public static void OnRecv(NKMPacket_OFFICE_CLEAR_ALL_FURNITURE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.CurrentUserData().OfficeData.UpdateRoomData(sPacket.room);
			NKCScenManager.CurrentUserData().OfficeData.UpdateInteriorData(sPacket.changedInteriors);
			NKCScenManager.CurrentUserData().m_ArmyData.UpdateUnitData(sPacket.updatedUnits);
			if (NKCUIOffice.IsInstanceOpen)
			{
				NKCUIOffice instance = NKCUIOffice.GetInstance();
				if (instance != null)
				{
					instance.OnRemoveAllFurnitures(sPacket.room.id);
				}
			}
		}

		// Token: 0x060059A4 RID: 22948 RVA: 0x001B3110 File Offset: 0x001B1310
		public static void OnRecv(NKMPacket_OFFICE_TAKE_HEART_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, false, null, -2147483648))
			{
				return;
			}
			NKCScenManager.CurrentUserData().m_ArmyData.UpdateData(sPacket.unit);
			if (NKCUIOffice.IsInstanceOpen)
			{
				NKCUIOffice instance = NKCUIOffice.GetInstance();
				if (instance != null)
				{
					instance.OnUnitTakeHeart(sPacket.unit);
				}
			}
		}

		// Token: 0x060059A5 RID: 22949 RVA: 0x001B316C File Offset: 0x001B136C
		public static void OnRecv(NKMPacket_OFFICE_STATE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_OFFICE)
			{
				NKCUIOfficeMapFront.ReserveScenID = NKCScenManager.GetScenManager().GetNowScenID();
			}
			NKCScenManager.CurrentUserData().OfficeData.ResetFriendUId();
			NKCScenManager.CurrentUserData().OfficeData.SetFriendData(sPacket.userUid, sPacket.officeState);
			NKCScenManager.GetScenManager().Get_NKC_SCEN_OFFICE().ReserveShortcut(NKM_SHORTCUT_TYPE.SHORTCUT_OFFICE, NKCUIOfficeMapFront.SectionType.Room.ToString());
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OFFICE, false);
		}

		// Token: 0x060059A6 RID: 22950 RVA: 0x001B3204 File Offset: 0x001B1404
		public static void OnRecv(NKMPacket_OFFICE_RANDOM_VISIT_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_OFFICE)
			{
				NKCUIOfficeMapFront.ReserveScenID = NKCScenManager.GetScenManager().GetNowScenID();
			}
			NKCScenManager.CurrentUserData().OfficeData.ResetFriendUId();
			NKCScenManager.CurrentUserData().OfficeData.SetFriendData(sPacket.officeState.commonProfile.userUid, sPacket.officeState);
			NKCScenManager.GetScenManager().Get_NKC_SCEN_OFFICE().ReserveShortcut(NKM_SHORTCUT_TYPE.SHORTCUT_OFFICE, NKCUIOfficeMapFront.SectionType.Room.ToString());
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OFFICE, false);
		}

		// Token: 0x060059A7 RID: 22951 RVA: 0x001B32A4 File Offset: 0x001B14A4
		public static void OnRecv(NKMPacket_OFFICE_POST_SEND_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			nkmuserData.OfficeData.UpdatePostState(sPacket.postState);
			if (NKCUIOffice.IsInstanceOpen)
			{
				NKCUIOffice instance = NKCUIOffice.GetInstance();
				if (instance != null)
				{
					instance.UpdatePostState();
				}
			}
			NKMCommonProfile profile = nkmuserData.OfficeData.GetFriendProfile();
			if (profile == null)
			{
				return;
			}
			if (!NKCFriendManager.IsFriend(profile.friendCode) && NKCFriendManager.GetFriendCount() < 60)
			{
				string content = string.Format(NKCUtilString.GET_STRING_OFFICE_REQUEST_FRIEND, profile.nickname);
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, content, delegate()
				{
					NKMPacket_FRIEND_REQUEST_REQ nkmpacket_FRIEND_REQUEST_REQ = new NKMPacket_FRIEND_REQUEST_REQ();
					nkmpacket_FRIEND_REQUEST_REQ.friendCode = profile.friendCode;
					NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FRIEND_REQUEST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
				}, delegate()
				{
					NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString("SI_OFFICE_BIZ_CARD_SENT", false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				}, false);
				return;
			}
			NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString("SI_OFFICE_BIZ_CARD_SENT", false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
		}

		// Token: 0x060059A8 RID: 22952 RVA: 0x001B339F File Offset: 0x001B159F
		public static void OnRecv(NKMPacket_OFFICE_POST_LIST_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.CurrentUserData().OfficeData.UpdatePostList(sPacket.postList);
			if (NKCUIPopupOfficeInteract.IsInstanceOpen)
			{
				NKCUIPopupOfficeInteract.Instance.UpdateBizCardList();
			}
		}

		// Token: 0x060059A9 RID: 22953 RVA: 0x001B33DC File Offset: 0x001B15DC
		public static void OnRecv(NKMPacket_OFFICE_POST_RECV_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.CurrentUserData().OfficeData.UpdatePostState(sPacket.postState);
			NKCScenManager.CurrentUserData().OfficeData.UpdatePostList(sPacket.postList);
			NKCScenManager.CurrentUserData().GetReward(sPacket.rewardData);
			if (NKCUIPopupOfficeInteract.IsInstanceOpen)
			{
				NKCUIPopupOfficeInteract.Instance.UpdateBizCardList();
			}
			NKCUIResult.Instance.OpenRewardGain(NKCScenManager.CurrentUserData().m_ArmyData, sPacket.rewardData, NKCUtilString.GET_STRING_ITEM_GAIN, "", null);
		}

		// Token: 0x060059AA RID: 22954 RVA: 0x001B3470 File Offset: 0x001B1670
		public static void OnRecv(NKMPacket_OFFICE_POST_BROADCAST_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.CurrentUserData().OfficeData.UpdatePostState(sPacket.postState);
			NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_OFFICE_BIZCARD_SENDED_ALL, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
			if (NKCUIOffice.IsInstanceOpen)
			{
				NKCUIOffice instance = NKCUIOffice.GetInstance();
				if (instance != null)
				{
					instance.UpdatePostState();
				}
			}
			if (NKCUIPopupOfficeInteract.IsInstanceOpen)
			{
				NKCUIPopupOfficeInteract.Instance.UpdateSendBizCardAllState();
			}
		}

		// Token: 0x060059AB RID: 22955 RVA: 0x001B34E7 File Offset: 0x001B16E7
		public static void OnRecv(NKMPacket_OFFICE_GUEST_LIST_NOT sPacket)
		{
			NKCScenManager.CurrentUserData().OfficeData.UpdateRandomVisitor(sPacket.guestList);
		}

		// Token: 0x060059AC RID: 22956 RVA: 0x001B3500 File Offset: 0x001B1700
		public static void OnRecv(NKMPacket_OFFICE_PARTY_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			nkmuserData.m_ArmyData.UpdateUnitData(sPacket.units);
			nkmuserData.m_InventoryData.UpdateItemInfo(sPacket.costItems);
			nkmuserData.GetReward(sPacket.rewardData);
			if (NKCUIOffice.IsInstanceOpen)
			{
				NKCUIPopupOfficePartyStart.Instance.Open(sPacket.rewardData, new NKCUIPopupOfficePartyStart.OnClose(NKCUIOffice.GetInstance().OnPartyFinished));
				return;
			}
			NKCUIPopupOfficePartyStart.Instance.Open(sPacket.rewardData, null);
		}

		// Token: 0x060059AD RID: 22957 RVA: 0x001B3590 File Offset: 0x001B1790
		public static void OnRecv(NKMPacket_OFFICE_PRESET_APPLY_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.CurrentUserData().OfficeData.UpdateRoomData(sPacket.room);
			NKCScenManager.CurrentUserData().OfficeData.UpdateInteriorData(sPacket.changedInteriors);
			NKCScenManager.CurrentUserData().m_ArmyData.UpdateUnitData(sPacket.updatedUnits);
			if (NKCUIOffice.IsInstanceOpen)
			{
				NKCUIOffice instance = NKCUIOffice.GetInstance();
				if (instance != null)
				{
					instance.OnApplyPreset(sPacket.room);
				}
			}
			if (NKCOfficeManager.IsAllFurniturePlaced(NKCScenManager.CurrentUserData().OfficeData.GetPreset(sPacket.presetId), sPacket.room))
			{
				NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString("SI_PF_OFFICE_DECO_MODE_PRESET_LOAD_COMPLETE", false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString("SI_PF_OFFICE_DECO_MODE_PRESET_LOAD_ERROR", false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
		}

		// Token: 0x060059AE RID: 22958 RVA: 0x001B3668 File Offset: 0x001B1868
		public static void OnRecv(NKMPacket_OFFICE_PRESET_ADD_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			int unlockCount = sPacket.totalPresetCount - NKCScenManager.CurrentUserData().OfficeData.GetPresetCount();
			NKCScenManager.CurrentUserData().OfficeData.SetPresetCount(sPacket.totalPresetCount);
			NKCScenManager.CurrentUserData().m_InventoryData.UpdateItemInfo(sPacket.costItemDatas);
			if (NKCUIPopupOfficePresetList.IsInstanceOpen)
			{
				NKCUIPopupOfficePresetList.Instance.Refresh(-1);
				NKCUIPopupOfficePresetList.Instance.PlayUnlockEffect(unlockCount);
			}
		}

		// Token: 0x060059AF RID: 22959 RVA: 0x001B36E8 File Offset: 0x001B18E8
		public static void OnRecv(NKMPacket_OFFICE_PRESET_RESET_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMOfficePreset nkmofficePreset = new NKMOfficePreset();
			NKMOfficePreset preset = NKCScenManager.CurrentUserData().OfficeData.GetPreset(sPacket.presetId);
			nkmofficePreset.presetId = sPacket.presetId;
			if (preset != null)
			{
				nkmofficePreset.name = preset.name;
			}
			NKCScenManager.CurrentUserData().OfficeData.SetPreset(nkmofficePreset);
			if (NKCUIPopupOfficePresetList.IsInstanceOpen)
			{
				NKCUIPopupOfficePresetList.Instance.Refresh(sPacket.presetId);
			}
		}

		// Token: 0x060059B0 RID: 22960 RVA: 0x001B3768 File Offset: 0x001B1968
		public static void OnRecv(NKMPacket_OFFICE_PRESET_CHANGE_NAME_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.CurrentUserData().OfficeData.ChangePresetName(sPacket.presetId, sPacket.newPresetName);
			if (NKCUIPopupOfficePresetList.IsInstanceOpen)
			{
				NKCUIPopupOfficePresetList.Instance.Refresh(sPacket.presetId);
			}
		}

		// Token: 0x060059B1 RID: 22961 RVA: 0x001B37BC File Offset: 0x001B19BC
		public static void OnRecv(NKMPacket_OFFICE_PRESET_REGISTER_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (sPacket.preset != null)
			{
				NKCScenManager.CurrentUserData().OfficeData.SetPreset(sPacket.preset);
				if (NKCUIPopupOfficePresetList.IsInstanceOpen)
				{
					NKCUIPopupOfficePresetList.Instance.Refresh(sPacket.preset.presetId);
					return;
				}
			}
			else if (NKCUIPopupOfficePresetList.IsInstanceOpen)
			{
				NKCUIPopupOfficePresetList.Instance.Refresh(-1);
			}
		}

		// Token: 0x060059B2 RID: 22962 RVA: 0x001B382C File Offset: 0x001B1A2C
		public static void OnRecv(NKMPacket_OFFICE_PRESET_APPLY_THEMA_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.CurrentUserData().OfficeData.UpdateRoomData(sPacket.room);
			NKCScenManager.CurrentUserData().OfficeData.UpdateInteriorData(sPacket.changedInteriors);
			NKCScenManager.CurrentUserData().m_ArmyData.UpdateUnitData(sPacket.updatedUnits);
			if (NKCUIOffice.IsInstanceOpen)
			{
				NKCUIOffice instance = NKCUIOffice.GetInstance();
				if (instance != null)
				{
					instance.OnApplyPreset(sPacket.room);
				}
			}
			NKMOfficeThemePresetTemplet nkmofficeThemePresetTemplet = NKMOfficeThemePresetTemplet.Find(sPacket.themaIndex);
			if (nkmofficeThemePresetTemplet != null)
			{
				if (NKCOfficeManager.IsAllFurniturePlaced(nkmofficeThemePresetTemplet.OfficePreset, sPacket.room))
				{
					NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString("SI_PF_OFFICE_DECO_MODE_PRESET_LOAD_COMPLETE", false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
				NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString("SI_PF_OFFICE_DECO_MODE_PRESET_LOAD_ERROR", false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
			}
		}

		// Token: 0x060059B3 RID: 22963 RVA: 0x001B3904 File Offset: 0x001B1B04
		public static void OnRecv(NKMPacket_RECALL_UNIT_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (!nkmuserData.m_RecallHistoryData.ContainsKey(sPacket.historyInfo.unitId))
			{
				nkmuserData.m_RecallHistoryData.Add(sPacket.historyInfo.unitId, sPacket.historyInfo);
			}
			else
			{
				nkmuserData.m_RecallHistoryData[sPacket.historyInfo.unitId] = sPacket.historyInfo;
			}
			NKMRewardData nkmrewardData = new NKMRewardData();
			if (sPacket.exchangeUnitData != null)
			{
				nkmrewardData.SetUnitData(new List<NKMUnitData>
				{
					sPacket.exchangeUnitData
				});
			}
			if (sPacket.rewardList.Count > 0)
			{
				nkmrewardData.SetMiscItemData(sPacket.rewardList);
			}
			nkmuserData.m_ArmyData.RemoveUnitOrShip(sPacket.removeUnitUid);
			nkmuserData.GetReward(nkmrewardData);
			if (nkmuserData != null)
			{
				NKCUIResult.Instance.OpenComplexResult(nkmuserData.m_ArmyData, nkmrewardData, new NKCUIResult.OnClose(NKCPacketHandlersLobby.OnCloseRecallReward), 0L, null, false, false);
			}
		}

		// Token: 0x060059B4 RID: 22964 RVA: 0x001B39FA File Offset: 0x001B1BFA
		private static void OnCloseRecallReward()
		{
			if (NKCPopupRecall.IsInstanceOpen)
			{
				NKCPopupRecall.Instance.Close();
			}
			if (NKCUIUnitInfo.IsInstanceOpen)
			{
				NKCUIUnitInfo.Instance.Close();
			}
			if (NKCUIShipInfo.IsInstanceOpen)
			{
				NKCUIShipInfo.Instance.Close();
			}
		}

		// Token: 0x060059B5 RID: 22965 RVA: 0x001B3A2F File Offset: 0x001B1C2F
		public static void OnRecv(NKMPacket_KAKAO_MISSION_REFRESH_STATE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCScenManager.CurrentUserData().kakaoMissionData = sPacket.missionData;
			if (NKCUIEvent.IsInstanceOpen)
			{
				NKCUIEvent.Instance.RefreshUI(0);
			}
		}

		// Token: 0x060059B6 RID: 22966 RVA: 0x001B3A68 File Offset: 0x001B1C68
		public static void OnRecv(NKMPacket_WECHAT_COUPON_CHECK_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, sPacket.zlongInfoCode))
			{
				return;
			}
			NKCUIEventSubUIWechatFollow.SetWechatCouponData(sPacket.data);
			NKCUIEventSubUIWechatFollow.SetSendPacketAfterRefresh(false);
			if (NKCUIEvent.IsInstanceOpen)
			{
				NKCUIEvent.Instance.RefreshUI(0);
			}
		}

		// Token: 0x060059B7 RID: 22967 RVA: 0x001B3AA4 File Offset: 0x001B1CA4
		public static void OnRecv(NKMPacket_WECHAT_COUPON_REWARD_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCUIEventSubUIWechatFollow.SetWechatCouponData(sPacket.data);
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			nkmuserData.GetReward(sPacket.rewardData);
			if (NKCUIEvent.IsInstanceOpen)
			{
				NKCUIEvent.Instance.RefreshUI(0);
			}
			NKCUIResult.Instance.OpenComplexResult(nkmuserData.m_ArmyData, sPacket.rewardData, null, 0L, null, false, false);
		}

		// Token: 0x060059B8 RID: 22968 RVA: 0x001B3B14 File Offset: 0x001B1D14
		public static void OnRecv(NKMPacket_EXTRACT_UNIT_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			nkmuserData.GetReward(sPacket.rewardItems);
			nkmuserData.GetReward(sPacket.synergyItems);
			nkmuserData.m_ArmyData.RemoveUnit(sPacket.extractUnitUidList);
			if (NKCUIPopupRearmamentExtractConfirm.IsInstanceOpen)
			{
				NKCUIPopupRearmamentExtractConfirm.Instance.Close();
			}
			NKCUIRearmament.Instance.OnRecv(sPacket);
			if (NKCGameEventManager.IsWaiting())
			{
				NKCUIResult.Instance.OpenRewardRearmExtract(sPacket.rewardItems, sPacket.synergyItems, NKCUtilString.GET_STRING_REARM_EXTRACT_RESULT_TITLE, "", new NKCUIResult.OnClose(NKCGameEventManager.WaitFinished));
				return;
			}
			NKCUIResult.Instance.OpenRewardRearmExtract(sPacket.rewardItems, sPacket.synergyItems, NKCUtilString.GET_STRING_REARM_EXTRACT_RESULT_TITLE, "", null);
		}

		// Token: 0x060059B9 RID: 22969 RVA: 0x001B3BDC File Offset: 0x001B1DDC
		public static void OnRecv(NKMPacket_REARMAMENT_UNIT_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKMArmyData armyData = nkmuserData.m_ArmyData;
			armyData.UpdateUnitData(sPacket.rearmamentUnitData);
			armyData.TryCollectUnit(sPacket.rearmamentUnitData.m_UnitID);
			nkmuserData.m_InventoryData.UpdateItemInfo(sPacket.costItems);
			if (NKCUIPopupRearmamentConfirm.IsInstanceOpen)
			{
				NKCUIPopupRearmamentConfirm.Instance.Close();
			}
			if (NKCUIPopupRearmamentConfirmBox.IsInstanceOpen)
			{
				NKCUIPopupRearmamentConfirmBox.Instance.Close();
			}
			NKCUIPopupRearmamentResult.Instance.Open(sPacket.rearmamentUnitData);
		}

		// Token: 0x060059BA RID: 22970 RVA: 0x001B3C68 File Offset: 0x001B1E68
		public static void OnRecv(NKMPacket_SERVER_KILL_COUNT_NOT sPacket)
		{
			NKCKillCountManager.SetServerKillCountData(sPacket.serverKillCountDataList);
		}

		// Token: 0x060059BB RID: 22971 RVA: 0x001B3C78 File Offset: 0x001B1E78
		public static void OnRecv(NKMPacket_KILL_COUNT_USER_REWARD_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			NKCKillCountManager.SetKillCountData(sPacket.killCountData);
			nkmuserData.GetReward(sPacket.rewardData);
			if (NKCUIEvent.IsInstanceOpen)
			{
				NKCUIEvent.Instance.RefreshUI(0);
			}
			NKCUIResult.Instance.OpenRewardGain(nkmuserData.m_ArmyData, sPacket.rewardData, NKCUtilString.GET_STRING_RESULT_MISSION, "", null);
		}

		// Token: 0x060059BC RID: 22972 RVA: 0x001B3CF0 File Offset: 0x001B1EF0
		public static void OnRecv(NKMPacket_KILL_COUNT_SERVER_REWARD_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			NKCKillCountManager.SetKillCountData(sPacket.killCountData);
			nkmuserData.GetReward(sPacket.rewardData);
			NKCUIResult.Instance.OpenRewardGain(nkmuserData.m_ArmyData, sPacket.rewardData, NKCUtilString.GET_STRING_RESULT_MISSION, "", delegate()
			{
				if (NKCPopupEventKillCountReward.IsInstanceOpen)
				{
					NKCPopupEventKillCountReward.Instance.SetData();
				}
				if (NKCUIEvent.IsInstanceOpen)
				{
					NKCUIEventSubUIHorizon.RewardGet = true;
					NKCUIEvent.Instance.RefreshUI(0);
				}
			});
		}

		// Token: 0x060059BD RID: 22973 RVA: 0x001B3D74 File Offset: 0x001B1F74
		public static void OnRecv(NKMPacket_UNIT_MISSION_REWARD_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (sPacket.missionData == null)
			{
				return;
			}
			NKCUnitMissionManager.UpdateCompletedUnitMissionData(sPacket.missionData);
			if (NKCUIPopupCollectionAchievement.IsInstanceOpen)
			{
				NKCUIPopupCollectionAchievement.Instance.Refresh();
			}
			if (NKCUICollectionUnitInfo.IsInstanceOpen)
			{
				NKCUICollectionUnitInfo.Instance.UpdateUnitMissionRedDot();
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_COLLECTION)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_COLLECTION().OnRecvUnitMissionReward(sPacket.missionData.unitId);
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			nkmuserData.GetReward(sPacket.rewardData);
			NKCPopupMessageToastSimple.Instance.Open(sPacket.rewardData, null, null);
		}

		// Token: 0x060059BE RID: 22974 RVA: 0x001B3E1C File Offset: 0x001B201C
		public static void OnRecv(NKMPacket_UNIT_MISSION_REWARD_ALL_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (sPacket.missionData.Count <= 0)
			{
				return;
			}
			NKCUnitMissionManager.UpdateCompletedUnitMissionData(sPacket.missionData);
			if (NKCUIPopupCollectionAchievement.IsInstanceOpen)
			{
				NKCUIPopupCollectionAchievement.Instance.Refresh();
			}
			if (NKCUICollectionUnitInfo.IsInstanceOpen)
			{
				NKCUICollectionUnitInfo.Instance.UpdateUnitMissionRedDot();
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_COLLECTION)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_COLLECTION().OnRecvUnitMissionReward(sPacket.missionData[0].unitId);
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			nkmuserData.GetReward(sPacket.rewardData);
			NKCPopupMessageToastSimple.Instance.Open(sPacket.rewardData, null, null);
		}

		// Token: 0x060059BF RID: 22975 RVA: 0x001B3ED0 File Offset: 0x001B20D0
		public static void OnRecv(NKMPacket_UNIT_MISSION_UPDATED_NOT sPacket)
		{
			NKCUnitMissionManager.UpdateRewardEnableMissionData(sPacket.rewardEnableMissions);
			if (NKCUICollectionUnitInfo.IsInstanceOpen)
			{
				NKCUICollectionUnitInfo.Instance.UpdateUnitMissionRedDot();
			}
			if (NKCUIPopupCollectionAchievement.IsInstanceOpen)
			{
				NKCUIPopupCollectionAchievement.Instance.Refresh();
			}
		}

		// Token: 0x060059C0 RID: 22976 RVA: 0x001B3F00 File Offset: 0x001B2100
		public static void OnRecv(NKMPacket_PVP_CASTING_VOTE_UNIT_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCBanManager.UpdatePVPCastingVoteData(sPacket.pvpCastingVoteData);
			if (NKCPopupGauntletCastingBan.IsInstanceOpen)
			{
				NKCPopupGauntletCastingBan.Instance.UpdateUI();
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().OnRecv(sPacket);
			}
		}

		// Token: 0x060059C1 RID: 22977 RVA: 0x001B3F5C File Offset: 0x001B215C
		public static void OnRecv(NKMPacket_PVP_CASTING_VOTE_SHIP_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCBanManager.UpdatePVPCastingVoteData(sPacket.pvpCastingVoteData);
			if (NKCPopupGauntletCastingBan.IsInstanceOpen)
			{
				NKCPopupGauntletCastingBan.Instance.UpdateUI();
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().OnRecv(sPacket);
			}
		}

		// Token: 0x060059C2 RID: 22978 RVA: 0x001B3FB8 File Offset: 0x001B21B8
		public static void OnRecv(NKMPacket_PVP_CASTING_VOTE_OPERATOR_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCBanManager.UpdatePVPCastingVoteData(sPacket.pvpCastingVoteData);
			if (NKCPopupGauntletCastingBan.IsInstanceOpen)
			{
				NKCPopupGauntletCastingBan.Instance.UpdateUI();
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().OnRecv(sPacket);
			}
		}

		// Token: 0x060059C3 RID: 22979 RVA: 0x001B4014 File Offset: 0x001B2214
		public static void OnRecv(NKMPacket_EVENT_BAR_DAILY_INFO_NOT sPacket)
		{
			NKCEventBarManager.DailyCocktailItemID = sPacket.dailyCocktailItemId;
			NKCEventBarManager.RemainDeliveryLimitValue = sPacket.remainDeliveryLimitValue;
			if (NKCUIEvent.IsInstanceOpen)
			{
				NKCUIEventSubUIBar.RefreshUI = true;
				NKCUIEvent.Instance.RefreshUI(0);
			}
		}

		// Token: 0x060059C4 RID: 22980 RVA: 0x001B4044 File Offset: 0x001B2244
		public static void OnRecv(NKMPacket_EVENT_BAR_CREATE_COCKTAIL_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData cNKMUserData = NKCScenManager.CurrentUserData();
			if (cNKMUserData == null)
			{
				return;
			}
			cNKMUserData.m_InventoryData.UpdateItemInfo(sPacket.costItems);
			cNKMUserData.GetReward(sPacket.rewardData);
			if (NKCUIEvent.IsInstanceOpen)
			{
				if (sPacket.rewardData.MiscItemDataList.Count > 0)
				{
					NKCUIEventBarResult instance = NKCUIEventBarResult.Instance;
					instance.m_onClose = (NKCUIEventBarResult.OnClose)Delegate.Combine(instance.m_onClose, new NKCUIEventBarResult.OnClose(delegate()
					{
						NKCUIResult.Instance.OpenRewardGain(cNKMUserData.m_ArmyData, sPacket.rewardData, NKCUtilString.GET_STRING_GREMORY_CREATE_RESULT, "", delegate()
						{
							NKCUIEventSubUI everOpenedEventSubUI = NKCUIEvent.Instance.GetEverOpenedEventSubUI(NKCUIEventSubUIBar.EventID);
							if (everOpenedEventSubUI != null)
							{
								NKCUIEventSubUIBar component = everOpenedEventSubUI.GetComponent<NKCUIEventSubUIBar>();
								if (component != null)
								{
									component.ActivateCreateFx();
								}
							}
						});
					}));
					NKCUIEventBarResult.Instance.Open(sPacket.rewardData.MiscItemDataList[0].ItemID);
				}
				NKCUIEventSubUIBar.RefreshUI = true;
				NKCUIEvent.Instance.RefreshUI(0);
			}
		}

		// Token: 0x060059C5 RID: 22981 RVA: 0x001B4138 File Offset: 0x001B2338
		public static void OnRecv(NKMPacket_EVENT_BAR_GET_REWARD_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			nkmuserData.m_InventoryData.UpdateItemInfo(sPacket.costItems);
			nkmuserData.GetReward(sPacket.rewardData);
			NKCEventBarManager.RemainDeliveryLimitValue = sPacket.remainDeliveryLimitValue;
			if (NKCUIEvent.IsInstanceOpen)
			{
				NKCUIEventSubUIBar.RefreshUI = true;
				NKCUIEvent.Instance.RefreshUI(0);
			}
			NKCUIResult.Instance.OpenRewardGainWithUnitSD(nkmuserData.m_ArmyData, sPacket.rewardData, null, NKCEventBarManager.RewardPopupUnitID, NKCUtilString.GET_STRING_GREMORY_DAILY_REWARD, "", null);
		}

		// Token: 0x060059C6 RID: 22982 RVA: 0x001B41CB File Offset: 0x001B23CB
		public static void OnRecv(NKMPacket_AD_INFO_NOT sPacket)
		{
			NKCAdManager.SetItemRewardInfo(sPacket.itemRewardInfos);
			NKCAdManager.SetInventoryExpandRewardInfo(sPacket.inventoryExpandRewardInfos);
		}

		// Token: 0x060059C7 RID: 22983 RVA: 0x001B41E4 File Offset: 0x001B23E4
		public static void OnRecv(NKMPacket_AD_ITEM_REWARD_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCAdjustManager.OnCustomEvent("34_AD_eternium");
			NKCAdManager.UpdateItemRewardInfo(sPacket.itemRewardInfo);
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			nkmuserData.GetReward(sPacket.rewardData);
		}

		// Token: 0x060059C8 RID: 22984 RVA: 0x001B4234 File Offset: 0x001B2434
		public static void OnRecv(NKMPacket_AD_INVENTORY_EXPAND_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCAdjustManager.OnCustomEvent("35_AD_ch_inventory");
			NKCAdManager.UpdateInventoryRewardInfo(sPacket.inventoryExpandType);
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			NKMInventoryManager.UpdateInventoryCount(sPacket.inventoryExpandType, sPacket.expandedCount, nkmuserData);
			if (NKCUIUnitSelectList.IsInstanceOpen)
			{
				NKCUIUnitSelectList.Instance.UpdateUnitCount();
				NKCUIUnitSelectList.Instance.OnExpandInventory();
			}
			if (NKCUIInventory.IsInstanceOpen)
			{
				NKCUIInventory.Instance.SetCurrentEquipCountUI();
				NKCUIInventory.Instance.OnInventoryAdd();
			}
			if (NKCUIForge.IsInstanceOpen && NKCUIForge.Instance.IsInventoryInstanceOpen())
			{
				NKCUIForge.Instance.Inventory.SetCurrentEquipCountUI();
				NKCUIForge.Instance.Inventory.OnInventoryAdd();
			}
			if (NKCUIDeckViewer.IsInstanceOpen)
			{
				NKCUIDeckViewer.Instance.UpdateUnitCount();
			}
		}

		// Token: 0x060059C9 RID: 22985 RVA: 0x001B4300 File Offset: 0x001B2500
		public static void OnRecv(NKMPacket_BSIDE_COUPON_USE_ACK sPacket)
		{
			Debug.Log("OnRecv - NKMPacket_BSIDE_COUPON_USE_ACK");
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			string @string = NKCStringTable.GetString("SI_PF_OPTION_ACCOUNT_COUPON_SUCCESS", false);
			NKCUIManager.NKCPopupMessage.Open(new PopupMessage(@string, NKCPopupMessage.eMessagePosition.Middle, 0f, true, false, false));
		}

		// Token: 0x060059CA RID: 22986 RVA: 0x001B4351 File Offset: 0x001B2551
		public static void OnRecv(NKMPacket_BSIDE_ACCOUNT_LINK_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCAccountLinkMgr.OnRecv(sPacket);
		}

		// Token: 0x060059CB RID: 22987 RVA: 0x001B436E File Offset: 0x001B256E
		public static void OnRecv(NKMPacket_BSIDE_ACCOUNT_LINK_NOT sPacket)
		{
			NKCAccountLinkMgr.OnRecv(sPacket);
		}

		// Token: 0x060059CC RID: 22988 RVA: 0x001B4376 File Offset: 0x001B2576
		public static void OnRecv(NKMPacket_BSIDE_ACCOUNT_LINK_CODE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCAccountLinkMgr.OnRecv(sPacket);
		}

		// Token: 0x060059CD RID: 22989 RVA: 0x001B4393 File Offset: 0x001B2593
		public static void OnRecv(NKMPacket_BSIDE_ACCOUNT_LINK_CODE_NOT sPacket)
		{
			NKCAccountLinkMgr.OnRecv(sPacket);
		}

		// Token: 0x060059CE RID: 22990 RVA: 0x001B439B File Offset: 0x001B259B
		public static void OnRecv(NKMPacket_BSIDE_ACCOUNT_LINK_SELECT_USERDATA_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCAccountLinkMgr.OnRecv(sPacket);
		}

		// Token: 0x060059CF RID: 22991 RVA: 0x001B43B8 File Offset: 0x001B25B8
		public static void OnRecv(NKMPacket_BSIDE_ACCOUNT_LINK_SUCCESS_NOT sPacket)
		{
			NKCAccountLinkMgr.OnRecv(sPacket);
		}

		// Token: 0x060059D0 RID: 22992 RVA: 0x001B43C0 File Offset: 0x001B25C0
		public static void OnRecv(NKMPacket_BSIDE_ACCOUNT_LINK_CANCEL_ACK sPacket)
		{
			NKCAccountLinkMgr.OnRecv(sPacket);
		}

		// Token: 0x060059D1 RID: 22993 RVA: 0x001B43C8 File Offset: 0x001B25C8
		public static void OnRecv(NKMPacket_BSIDE_ACCOUNT_LINK_CANCEL_NOT sPacket)
		{
			NKCAccountLinkMgr.OnRecv(sPacket);
		}

		// Token: 0x060059D2 RID: 22994 RVA: 0x001B43D0 File Offset: 0x001B25D0
		public static void OnRecv(NKMPacket_STEAM_BUY_INIT_ACK sPacket)
		{
			((NKCPMSteamPC.InAppSteam)NKCPublisherModule.InAppPurchase).OnRecv(sPacket);
		}

		// Token: 0x060059D3 RID: 22995 RVA: 0x001B43E4 File Offset: 0x001B25E4
		public static void OnRecv(NKMPacket_EVENT_POINT_NOT sPacket)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				nkmuserData.GetReward(sPacket.additionalReward);
			}
			if (NKCPopupPointExchange.IsInstanceOpen)
			{
				NKCPopupPointExchange.Instance.RefreshPoint();
				NKCPopupPointExchange.Instance.RefreshMission();
			}
			NKCPopupMessageToastSimple.Instance.Open(sPacket.additionalReward, null, null);
		}

		// Token: 0x060059D4 RID: 22996 RVA: 0x001B4434 File Offset: 0x001B2634
		public static void OnRecv(NKMPacket_TRIM_START_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				nkmuserData.m_InventoryData.UpdateItemInfo(sPacket.costItemDataList);
			}
			NKCTrimManager.SetTrimModeState(sPacket.trimModeState);
			NKCTrimManager.ProcessTrim();
		}

		// Token: 0x060059D5 RID: 22997 RVA: 0x001B4482 File Offset: 0x001B2682
		public static void OnRecv(NKMPacket_TRIM_RETRY_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCTrimManager.ClearTrimModeState();
			if (NKCUIResult.IsInstanceOpen)
			{
				NKCUIResult.Instance.OnTrimRetryAck();
			}
		}

		// Token: 0x060059D6 RID: 22998 RVA: 0x001B44AF File Offset: 0x001B26AF
		public static void OnRecv(NKMPacket_TRIM_RESTORE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			nkmuserData.m_InventoryData.UpdateItemInfo(sPacket.costItemData);
		}

		// Token: 0x060059D7 RID: 22999 RVA: 0x001B44E0 File Offset: 0x001B26E0
		public static void OnRecv(NKMPacket_TRIM_END_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKCTrimManager.ClearTrimModeState();
			NKCScenManager.GetScenManager().Get_NKC_SCEN_TRIM_RESULT().SetData(sPacket.trimClearData, sPacket.trimModeState, sPacket.bestScore, sPacket.isFirst);
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (sPacket.trimClearData != null && nkmuserData != null)
			{
				nkmuserData.GetReward(sPacket.trimClearData.rewardData);
			}
			if (nkmuserData != null)
			{
				nkmuserData.m_InventoryData.UpdateItemInfo(sPacket.costItemDataList);
			}
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_TRIM_RESULT, true);
		}

		// Token: 0x060059D8 RID: 23000 RVA: 0x001B4574 File Offset: 0x001B2774
		public static void OnRecv(NKMPacket_TRIM_INTERVAL_INFO_NOT sPacket)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				nkmuserData.TrimData.SetTrimClearList(sPacket.trimClearList);
			}
			NKMUserData nkmuserData2 = NKCScenManager.CurrentUserData();
			if (nkmuserData2 != null)
			{
				nkmuserData2.TrimData.SetTrimIntervalData(sPacket.trimIntervalData);
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_TRIM && NKCScenManager.GetScenManager().Get_NKC_SCEN_TRIM().TrimIntervalId != sPacket.trimIntervalId)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_TRIM_INTERVAL_END, delegate()
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_WORLDMAP, true);
				}, "");
				return;
			}
			if (NKCUITrimMain.IsInstanceOpen)
			{
				NKCUITrimMain.GetInstance().RefreshUI();
			}
			if (NKCUIPopupTrimDungeon.IsInstanceOpen)
			{
				NKCUIPopupTrimDungeon.Instance.RefreshUI(false);
			}
		}

		// Token: 0x060059D9 RID: 23001 RVA: 0x001B4634 File Offset: 0x001B2834
		public static void OnRecv(NKMPacket_TRIM_DUNGEON_SKIP_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				nkmuserData.TrimData.SetTrimClearData(sPacket.trimClearData);
				nkmuserData.m_InventoryData.UpdateItemInfo(sPacket.costItems);
				if (sPacket.rewardDatas != null)
				{
					foreach (NKMRewardData rewardData in sPacket.rewardDatas)
					{
						nkmuserData.GetReward(rewardData);
					}
				}
				if (sPacket.updatedUnits != null)
				{
					foreach (UnitLoyaltyUpdateData unitLoyaltyUpdateData in sPacket.updatedUnits)
					{
						NKMUnitData unitFromUID = nkmuserData.m_ArmyData.GetUnitFromUID(unitLoyaltyUpdateData.unitUid);
						if (unitFromUID != null)
						{
							unitFromUID.loyalty = unitLoyaltyUpdateData.loyalty;
							unitFromUID.SetOfficeRoomId(unitLoyaltyUpdateData.officeRoomId, unitLoyaltyUpdateData.officeGrade, unitLoyaltyUpdateData.heartGaugeStartTime);
						}
					}
				}
			}
			NKCPopupOpSkipProcess.Instance.Open(sPacket.rewardDatas, sPacket.updatedUnits, null);
		}

		// Token: 0x060059DA RID: 23002 RVA: 0x001B4770 File Offset: 0x001B2970
		public static void OnRecv(NKMPacket_EVENT_COLLECTION_NOT sPacket)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				nkmuserData.EventCollectionInfo = sPacket.eventCollectionInfo;
			}
		}

		// Token: 0x060059DB RID: 23003 RVA: 0x001B4794 File Offset: 0x001B2994
		public static void OnRecv(NKMPacket_EVENT_COLLECTION_MERGE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			foreach (NKMUnitData newUnit in sPacket.rewardData.UnitDataList)
			{
				armyData.AddNewUnit(newUnit);
			}
			armyData.RemoveUnit(sPacket.consumeTrophyUids);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_HOME)
			{
				foreach (NKCUIModuleHome nkcuimoduleHome in NKCUIManager.GetOpenedUIsByType<NKCUIModuleHome>())
				{
					if (nkcuimoduleHome.IsOpen)
					{
						nkcuimoduleHome.UpdateUI();
						using (IEnumerator<NKCUIModuleSubUIMerge> enumerator3 = nkcuimoduleHome.GetSubUIs<NKCUIModuleSubUIMerge>().GetEnumerator())
						{
							if (enumerator3.MoveNext())
							{
								NKCUIModuleSubUIMerge nkcuimoduleSubUIMerge = enumerator3.Current;
								if (nkcuimoduleSubUIMerge != null)
								{
									nkcuimoduleSubUIMerge.OnCompleteMerge(sPacket.collectionMergeId, sPacket.rewardData.UnitDataList);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060059DC RID: 23004 RVA: 0x001B48CC File Offset: 0x001B2ACC
		public static void OnRecv(NKMPacket_UNIT_TACTIC_UPDATE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			nkmuserData.m_ArmyData.RemoveUnit(sPacket.consumeUnitUid);
			nkmuserData.m_ArmyData.UpdateUnitData(sPacket.unitData);
			nkmuserData.GetReward(sPacket.rewardItems);
			nkmuserData.m_unitTacticReturnCount = sPacket.unitTacticReturnCount;
			NKCPopupMessageToastSimple.Instance.Open(sPacket.rewardItems, null, null);
			if (NKCUITacticUpdate.IsInstanceOpen)
			{
				NKCUITacticUpdate.Instance.OnRecv(sPacket);
			}
		}

		// Token: 0x060059DD RID: 23005 RVA: 0x001B4956 File Offset: 0x001B2B56
		public static void OnRecv(NKMPacket_SERVICE_TRANSFER_REGIST_CODE_ACK sPacket)
		{
			NKCServiceTransferMgr.OnRecv(sPacket);
		}

		// Token: 0x060059DE RID: 23006 RVA: 0x001B495E File Offset: 0x001B2B5E
		public static void OnRecv(NKMPacket_SERVICE_TRANSFER_CODE_COPY_REWARD_ACK sPacket)
		{
			NKCServiceTransferMgr.OnRecv(sPacket);
		}

		// Token: 0x060059DF RID: 23007 RVA: 0x001B4966 File Offset: 0x001B2B66
		public static void OnRecv(NKMPacket_SERVICE_TRANSFER_USER_VALIDATION_ACK sPacket)
		{
			NKCServiceTransferMgr.OnRecv(sPacket);
		}

		// Token: 0x060059E0 RID: 23008 RVA: 0x001B496E File Offset: 0x001B2B6E
		public static void OnRecv(NKMPacket_SERVICE_TRANSFER_CODE_VALIDATION_ACK sPacket)
		{
			NKCServiceTransferMgr.OnRecv(sPacket);
		}

		// Token: 0x060059E1 RID: 23009 RVA: 0x001B4976 File Offset: 0x001B2B76
		public static void OnRecv(NKMPacket_SERVICE_TRANSFER_CONFIRM_ACK sPacket)
		{
			NKCServiceTransferMgr.OnRecv(sPacket);
		}
	}
}
