using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Pvp;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B74 RID: 2932
	public class NKCUIGauntletLobbyAsyncV2 : MonoBehaviour
	{
		// Token: 0x06008678 RID: 34424 RVA: 0x002D8140 File Offset: 0x002D6340
		public void Init()
		{
			NKCUtil.SetBindFunction(this.m_csbtnAsyncRefresh, new UnityAction(this.OnClickAsyncRefresh));
			if (null != this.m_lvsrAsyncBattle)
			{
				this.m_lvsrAsyncBattle.dOnGetObject += this.GetSlotAsyncBattle;
				this.m_lvsrAsyncBattle.dOnReturnObject += this.ReturnSlotAsyncBattle;
				this.m_lvsrAsyncBattle.dOnProvideData += this.ProvideDataAsyncBattle;
				this.m_lvsrAsyncBattle.ContentConstraintCount = 1;
				NKCUtil.SetScrollHotKey(this.m_lvsrAsyncBattle, null);
			}
			if (null != this.m_lvsrAsyncNPC)
			{
				this.m_lvsrAsyncNPC.dOnGetObject += this.GetSlotAsyncNPC;
				this.m_lvsrAsyncNPC.dOnReturnObject += this.ReturnSlotAsyncNPC;
				this.m_lvsrAsyncNPC.dOnProvideData += this.ProvideDataAsyncNPC;
				this.m_lvsrAsyncNPC.ContentConstraintCount = 1;
				NKCUtil.SetScrollHotKey(this.m_lvsrAsyncNPC, null);
			}
			if (null != this.m_lvsrAsyncRevenge)
			{
				this.m_lvsrAsyncRevenge.dOnGetObject += this.GetSlotAsyncRevenge;
				this.m_lvsrAsyncRevenge.dOnReturnObject += this.ReturnSlotAsyncRevenge;
				this.m_lvsrAsyncRevenge.dOnProvideData += this.ProvideDataAsyncRevenge;
				this.m_lvsrAsyncRevenge.ContentConstraintCount = 1;
				NKCUtil.SetScrollHotKey(this.m_lvsrAsyncRevenge, null);
			}
			if (null != this.m_lvsrAsyncRank)
			{
				this.m_lvsrAsyncRank.dOnGetObject += this.GetSlotAsyncRank;
				this.m_lvsrAsyncRank.dOnReturnObject += this.ReturnSlotAsyncRank;
				this.m_lvsrAsyncRank.dOnProvideData += this.ProvideDataAsyncRank;
				this.m_lvsrAsyncRank.ContentConstraintCount = 1;
				NKCUtil.SetScrollHotKey(this.m_lvsrAsyncRank, null);
			}
			if (null != this.m_lhsrNPCSub)
			{
				this.m_lhsrNPCSub.dOnGetObject += this.GetSlotAsyncNPCSub;
				this.m_lhsrNPCSub.dOnReturnObject += this.ReturnSlotAsyncNPCSub;
				this.m_lhsrNPCSub.dOnProvideData += this.ProvideDataAsyncNPCSub;
				this.m_lhsrNPCSub.ContentConstraintCount = 1;
			}
			this.m_RightSide.InitUI();
			this.m_RightSide.SetCallback(new NKCUIDeckViewer.DeckViewerOption.OnBackButton(this.CloseDefenseDeck));
			NKCUtil.SetToggleValueChangedDelegate(this.m_ctglAsyncBattle, delegate(bool _b)
			{
				this.OnToggleAsyncTab(_b, NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_BATTLE, false);
			});
			NKCUtil.SetToggleValueChangedDelegate(this.m_ctglAsyncNPC, delegate(bool _b)
			{
				this.OnToggleAsyncTab(_b, NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_NPC, false);
			});
			NKCUtil.SetToggleValueChangedDelegate(this.m_ctglAsyncRevenge, delegate(bool _b)
			{
				this.OnToggleAsyncTab(_b, NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_REVENGE, false);
			});
			NKCUtil.SetToggleValueChangedDelegate(this.m_ctglAsyncRank, delegate(bool _b)
			{
				this.OnToggleAsyncTab(_b, NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_RANK, false);
			});
			NKCUtil.SetToggleValueChangedDelegate(this.m_ctglAll, delegate(bool _b)
			{
				this.OnToggleRankTab(_b, RANK_TYPE.ALL);
			});
			NKCUtil.SetToggleValueChangedDelegate(this.m_ctglMyLeague, delegate(bool _b)
			{
				this.OnToggleRankTab(_b, RANK_TYPE.MY_LEAGUE);
			});
			NKCUtil.SetToggleValueChangedDelegate(this.m_ctglFriend, delegate(bool _b)
			{
				this.OnToggleRankTab(_b, RANK_TYPE.FRIEND);
			});
			NKCUtil.SetGameobjectActive(this.m_ctglAsyncRevenge.gameObject, NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_ASYNC_REVENGE_MODE));
			NKCUtil.SetGameobjectActive(this.m_ctglAsyncNPC.gameObject, NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_ASYNC_REVENGE_MODE));
			this.m_CurAsyncType = NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_BATTLE;
			this.m_CurRankType = RANK_TYPE.ALL;
		}

		// Token: 0x06008679 RID: 34425 RVA: 0x002D846E File Offset: 0x002D666E
		private void Update()
		{
			if (this.m_fPrevUpdateTime + 1f < Time.time)
			{
				this.m_fPrevUpdateTime = Time.time;
				this.UpdateRemainTimeUI();
				this.UpdateAsyncRefreshTime();
				this.m_RightSide.UpdateRankPVPPointUI();
			}
		}

		// Token: 0x0600867A RID: 34426 RVA: 0x002D84A8 File Offset: 0x002D66A8
		private void UpdateRemainTimeUI()
		{
			if (this.m_CurAsyncType == NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_REVENGE)
			{
				return;
			}
			NKMPvpRankSeasonTemplet pvpAsyncSeasonTemplet = NKCPVPManager.GetPvpAsyncSeasonTemplet(this.GetCurrentSeasonID());
			if (pvpAsyncSeasonTemplet != null)
			{
				if (!NKCPVPManager.IsRewardWeek(pvpAsyncSeasonTemplet, NKCPVPManager.WeekCalcStartDateUtc))
				{
					if (!pvpAsyncSeasonTemplet.CheckMySeason(NKCSynchronizedTime.GetServerUTCTime(0.0)))
					{
						this.m_lbRemainRankTime.text = NKCUtilString.GET_STRING_GAUNTLET_THIS_SEASON_LEAGUE_BEING_EVALUATED;
						return;
					}
					this.m_lbRemainRankTime.text = string.Format(NKCUtilString.GET_STRING_GAUNTLET_THIS_SEASON_LEAGUE_REMAIN_TIME_ONE_PARAM, NKCUtilString.GetRemainTimeStringEx(pvpAsyncSeasonTemplet.EndDate));
					return;
				}
				else
				{
					this.m_lbRemainRankTime.text = string.Format(NKCUtilString.GET_STRING_GAUNTLET_THIS_WEEK_LEAGUE_ONE_PARAM, NKCUtilString.GetRemainTimeStringForGauntletWeekly());
				}
			}
		}

		// Token: 0x0600867B RID: 34427 RVA: 0x002D8540 File Offset: 0x002D6740
		private void UpdateAsyncRefreshTime()
		{
			if (this.m_CurAsyncType != NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_BATTLE)
			{
				return;
			}
			if (this.m_fAsyncRefreshTimer <= 0f)
			{
				return;
			}
			if (this.m_fAsyncRefreshTimer + 60f < Time.time)
			{
				this.RefreshAsyncButton();
				return;
			}
			float num = 60f - (Time.time - this.m_fAsyncRefreshTimer);
			NKCUtil.SetLabelText(this.m_txtAsyncRefreshTimer, string.Format("00:{0:00}", num));
			if (num <= 0f)
			{
				this.RefreshAsyncButton();
			}
		}

		// Token: 0x0600867C RID: 34428 RVA: 0x002D85BC File Offset: 0x002D67BC
		private bool TrySendRankUserListREQ(RANK_TYPE rt, bool all)
		{
			if (all)
			{
				if (this.m_arAllRankREQ[(int)rt])
				{
					return false;
				}
				this.m_arAllRankREQ[(int)rt] = true;
			}
			else
			{
				if (this.m_arRankREQ[(int)rt])
				{
					return false;
				}
				this.m_arRankREQ[(int)rt] = true;
			}
			NKCPacketSender.Send_NKMPacket_ASYNC_PVP_RANK_LIST_REQ(rt, all);
			NKCUtil.SetGameobjectActive(this.m_lvsrAsyncRank.gameObject, false);
			return true;
		}

		// Token: 0x0600867D RID: 34429 RVA: 0x002D8611 File Offset: 0x002D6811
		private void OnEventPanelBeginDragAll()
		{
			this.TrySendRankUserListREQ(this.m_CurRankType, true);
		}

		// Token: 0x0600867E RID: 34430 RVA: 0x002D8621 File Offset: 0x002D6821
		private void RefreshScrollRect(LoopVerticalScrollRect scrollRect, NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE type, int count)
		{
			scrollRect.TotalCount = count;
			if (!this.m_arOpened[(int)type])
			{
				this.m_arOpened[(int)type] = true;
				scrollRect.velocity = Vector2.zero;
				scrollRect.SetIndexPosition(0);
				return;
			}
			scrollRect.RefreshCells(false);
		}

		// Token: 0x0600867F RID: 34431 RVA: 0x002D8658 File Offset: 0x002D6858
		public void OnRecv(NKMPacket_ASYNC_PVP_TARGET_LIST_ACK sPacket)
		{
			if (NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().AsyncTargetList.Count > 0)
			{
				this.m_fAsyncRefreshTimer = Time.time;
				NKC_SCEN_GAUNTLET_LOBBY.AsyncRefreshCooltime = this.m_fAsyncRefreshTimer;
			}
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetAsyncTargetList(sPacket.targetList);
			this.SetTartgetList(NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().AsyncTargetList);
			this.RefreshAsyncButton();
			this.UpdateAsyncRefreshTime();
			if (this.m_CurAsyncType == NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_BATTLE)
			{
				this.UpdateScroll();
			}
		}

		// Token: 0x06008680 RID: 34432 RVA: 0x002D86D6 File Offset: 0x002D68D6
		public void OnRecv(NKMPacket_NPC_PVP_TARGET_LIST_ACK sPacket)
		{
			this.m_fAsyncNpcBotTimer = Time.time;
			this.m_lstCurNpcBot = sPacket.targetList;
			NKCUtil.SetGameobjectActive(this.m_lvsrAsyncNPC.gameObject, true);
			this.UpdateScroll();
			this.OnNewOpenTierSlotEffect();
		}

		// Token: 0x06008681 RID: 34433 RVA: 0x002D870C File Offset: 0x002D690C
		public void OnRecv(NKMPacket_REVENGE_PVP_TARGET_LIST_ACK sPacket)
		{
			this.SetRevengeList(sPacket.targetList);
			NKCUtil.SetGameobjectActive(this.m_lvsrAsyncRevenge.gameObject, true);
			if (this.m_CurAsyncType == NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_REVENGE)
			{
				this.UpdateScroll();
			}
		}

		// Token: 0x06008682 RID: 34434 RVA: 0x002D873A File Offset: 0x002D693A
		public void SetTartgetList(List<AsyncPvpTarget> targetList)
		{
			this.m_listAsyncTarget.Clear();
			this.m_listAsyncTarget.AddRange(targetList);
		}

		// Token: 0x06008683 RID: 34435 RVA: 0x002D8753 File Offset: 0x002D6953
		public void SetRevengeList(List<RevengePvpTarget> targetList)
		{
			this.m_listRevengeTarget.Clear();
			this.m_listRevengeTarget.AddRange(targetList.FindAll((RevengePvpTarget v) => this.InvalidTarget(v)));
		}

		// Token: 0x06008684 RID: 34436 RVA: 0x002D8780 File Offset: 0x002D6980
		private bool InvalidTarget(RevengePvpTarget target)
		{
			return target != null && target.asyncDeck != null && target.asyncDeck.ship != null && target.asyncDeck.units != null && target.asyncDeck.units.Count == 8;
		}

		// Token: 0x06008685 RID: 34437 RVA: 0x002D87D0 File Offset: 0x002D69D0
		public void OnRecv(NKMPacket_ASYNC_PVP_RANK_LIST_ACK spacket)
		{
			NKCUtil.SetGameobjectActive(this.m_lvsrAsyncRank.gameObject, true);
			this.AddUserSimpleList(spacket.rankType, spacket.userProfileDataList);
			if (spacket.rankType == RANK_TYPE.FRIEND)
			{
				this.GetUserSimpleData(spacket.rankType).Sort((NKMUserSimpleProfileData a, NKMUserSimpleProfileData b) => b.pvpScore.CompareTo(a.pvpScore));
			}
			if (spacket.userProfileDataList.Count < NKMPvpCommonConst.Instance.RANK_SIMPLE_COUNT)
			{
				this.m_arAllRankREQ[(int)spacket.rankType] = true;
			}
			if (this.m_CurRankType == spacket.rankType)
			{
				this.UpdateScroll();
			}
		}

		// Token: 0x06008686 RID: 34438 RVA: 0x002D8872 File Offset: 0x002D6A72
		public void OnRecv(NKMPacket_UPDATE_DEFENCE_DECK_ACK packet)
		{
			if (!this.RefreshAsyncLock() && this.m_bNotSetDefenceDeck)
			{
				this.m_bNotSetDefenceDeck = false;
				this.OnToggleAsyncTab(true, this.m_CurAsyncType, true);
			}
		}

		// Token: 0x06008687 RID: 34439 RVA: 0x002D8899 File Offset: 0x002D6A99
		public void OnRecv(NKMPacket_ASYNC_PVP_RANK_SEASON_REWARD_ACK packet)
		{
			this.m_iCurNPCBotTier = 0;
			this.RefreshNpcSubTabUI();
			this.UpdateTabUI();
			this.m_RightSide.UpdateNowSeasonPVPInfoUI(NKM_GAME_TYPE.NGT_ASYNC_PVP);
		}

		// Token: 0x06008688 RID: 34440 RVA: 0x002D88BB File Offset: 0x002D6ABB
		public void OnRecv(NKMPacket_PVP_CHARGE_POINT_REFRESH_ACK cNKMPacket_PVP_CHARGE_POINT_REFRESH_ACK)
		{
			this.m_RightSide.UpdateRankPVPPointUI();
		}

		// Token: 0x06008689 RID: 34441 RVA: 0x002D88C8 File Offset: 0x002D6AC8
		public void SetUI(NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE reservedTab = NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.MAX)
		{
			this.m_bPlayIntro = true;
			this.m_fAsyncRefreshTimer = NKC_SCEN_GAUNTLET_LOBBY.AsyncRefreshCooltime;
			if (reservedTab - NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_NPC <= 1)
			{
				this.m_CurAsyncType = reservedTab;
			}
			this.RefreshAsyncLock();
			if (!this.m_bPrepareLoopScrollCells)
			{
				NKCUtil.SetGameobjectActive(this.m_objAsyncBattle, true);
				NKCUtil.SetGameobjectActive(this.m_objAsyncNPC, true);
				NKCUtil.SetGameobjectActive(this.m_objAsyncRevenge, true);
				NKCUtil.SetGameobjectActive(this.m_objAsyncRank, true);
				LoopVerticalScrollRect lvsrAsyncBattle = this.m_lvsrAsyncBattle;
				if (lvsrAsyncBattle != null)
				{
					lvsrAsyncBattle.PrepareCells(0);
				}
				LoopVerticalScrollRect lvsrAsyncNPC = this.m_lvsrAsyncNPC;
				if (lvsrAsyncNPC != null)
				{
					lvsrAsyncNPC.PrepareCells(0);
				}
				LoopHorizontalScrollRect lhsrNPCSub = this.m_lhsrNPCSub;
				if (lhsrNPCSub != null)
				{
					lhsrNPCSub.PrepareCells(0);
				}
				LoopVerticalScrollRect lvsrAsyncRevenge = this.m_lvsrAsyncRevenge;
				if (lvsrAsyncRevenge != null)
				{
					lvsrAsyncRevenge.PrepareCells(0);
				}
				LoopVerticalScrollRect lvsrAsyncRank = this.m_lvsrAsyncRank;
				if (lvsrAsyncRank != null)
				{
					lvsrAsyncRank.PrepareCells(0);
				}
				this.m_bPrepareLoopScrollCells = true;
			}
			this.m_RightSide.UpdateNowSeasonPVPInfoUI(NKM_GAME_TYPE.NGT_ASYNC_PVP);
			this.m_RightSide.UpdateBattleCondition();
			if (this.m_bFirstOpen)
			{
				for (int i = 0; i < 3; i++)
				{
					this.m_arRankREQ[i] = false;
					this.m_arAllRankREQ[i] = false;
					this.m_arOpened[i] = false;
				}
				this.RefreshNpcSubTabUI();
				this.OnToggleAsyncTab(true, this.m_CurAsyncType, true);
				this.OnSelectTierSlot(this.m_iCurNPCBotTier);
				this.m_bFirstOpen = false;
			}
			this.UpdateRemainTimeUI();
			this.m_RightSide.UpdateRankPVPPointUI();
			this.UpdateTabUI();
			this.RefreshAsyncButton();
		}

		// Token: 0x0600868A RID: 34442 RVA: 0x002D8A24 File Offset: 0x002D6C24
		public void ClearCacheData()
		{
			if (this.m_NKCPopupGauntletLeagueGuide != null)
			{
				if (this.m_NKCPopupGauntletLeagueGuide.IsOpen)
				{
					this.m_NKCPopupGauntletLeagueGuide.Close();
				}
				this.m_NKCPopupGauntletLeagueGuide = null;
			}
			this.m_lvsrAsyncBattle.ClearCells();
			this.m_lvsrAsyncNPC.ClearCells();
			this.m_lvsrAsyncRevenge.ClearCells();
			this.m_lvsrAsyncRank.ClearCells();
			this.m_lhsrNPCSub.ClearCells();
		}

		// Token: 0x0600868B RID: 34443 RVA: 0x002D8A98 File Offset: 0x002D6C98
		public void Close()
		{
			if (this.m_NKCPopupGauntletLeagueGuide != null && this.m_NKCPopupGauntletLeagueGuide.IsOpen)
			{
				this.m_NKCPopupGauntletLeagueGuide.Close();
			}
			this.m_bFirstOpen = true;
			this.m_iNewOpendNpcTier = 0;
			this.m_bPrepareLoopScrollCells = false;
			NKCPopupGauntletBanList.CheckInstanceAndClose();
		}

		// Token: 0x0600868C RID: 34444 RVA: 0x002D8AE8 File Offset: 0x002D6CE8
		private void RefreshNpcSubTabUI()
		{
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_ASYNC_NPC_MODE))
			{
				NKCUtil.SetGameobjectActive(this.m_objAsyncNPC, true);
				this.m_iNPCBotMaxOpendTier = NKCScenManager.GetScenManager().GetMyUserData().m_NpcData.MaxOpenedTier;
				this.m_iCurNPCBotTier = ((this.m_iCurNPCBotTier != 0) ? this.m_iCurNPCBotTier : this.m_iNPCBotMaxOpendTier);
				this.m_lhsrNPCSub.TotalCount = Math.Max(1, NKCScenManager.GetScenManager().GetMyUserData().m_NpcData.MaxTierCount);
				this.m_lhsrNPCSub.SetIndexPosition(this.m_iCurNPCBotTier - 1);
			}
		}

		// Token: 0x0600868D RID: 34445 RVA: 0x002D8B78 File Offset: 0x002D6D78
		private void OnClickFriendProfile(long friendCode)
		{
			if (friendCode <= 0L)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_USER_PROFILE_BY_FRIEND_CODE_REQ(friendCode);
		}

		// Token: 0x0600868E RID: 34446 RVA: 0x002D8B88 File Offset: 0x002D6D88
		private void OnClickAsyncBattle(long friendCode, NKM_GAME_TYPE gameType)
		{
			if (!this.IsEnableAsync())
			{
				return;
			}
			AsyncPvpTarget asyncPvpTarget = this.m_listAsyncTarget.Find((AsyncPvpTarget v) => v.userFriendCode == friendCode);
			if (asyncPvpTarget != null)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_ASYNC_READY().SetReserveData(asyncPvpTarget, gameType);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_ASYNC_READY, true);
			}
		}

		// Token: 0x0600868F RID: 34447 RVA: 0x002D8BE4 File Offset: 0x002D6DE4
		private void OnClickAsyncRevenge(long friendCode, NKM_GAME_TYPE gameType)
		{
			if (!this.IsEnableAsync())
			{
				return;
			}
			RevengePvpTarget revengePvpTarget = this.m_listRevengeTarget.Find((RevengePvpTarget v) => v.userFriendCode == friendCode);
			if (revengePvpTarget != null)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_ASYNC_READY().SetReserveData(NKCUIGauntletLobbyAsyncV2.ConventToAsyncPvpTarget(revengePvpTarget), gameType);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_ASYNC_READY, true);
			}
		}

		// Token: 0x06008690 RID: 34448 RVA: 0x002D8C45 File Offset: 0x002D6E45
		private bool IsEnableAsync()
		{
			return this.CheckDefenseDeck() == NKM_ERROR_CODE.NEC_OK && this.CheckCanPlayPVPGame() == NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06008691 RID: 34449 RVA: 0x002D8C5C File Offset: 0x002D6E5C
		public static AsyncPvpTarget ConventToAsyncPvpTarget(RevengePvpTarget _revengeTarget)
		{
			AsyncPvpTarget asyncPvpTarget = new AsyncPvpTarget();
			if (_revengeTarget != null)
			{
				asyncPvpTarget.userLevel = _revengeTarget.userLevel;
				asyncPvpTarget.userNickName = _revengeTarget.userNickName;
				asyncPvpTarget.userFriendCode = _revengeTarget.userFriendCode;
				asyncPvpTarget.rank = _revengeTarget.rank;
				asyncPvpTarget.score = _revengeTarget.score;
				asyncPvpTarget.tier = _revengeTarget.tier;
				asyncPvpTarget.mainUnitId = _revengeTarget.mainUnitId;
				asyncPvpTarget.mainUnitSkinId = _revengeTarget.mainUnitSkinId;
				asyncPvpTarget.selfieFrameId = _revengeTarget.selfieFrameId;
				asyncPvpTarget.asyncDeck = _revengeTarget.asyncDeck;
				asyncPvpTarget.guildData = _revengeTarget.guildData;
			}
			return asyncPvpTarget;
		}

		// Token: 0x06008692 RID: 34450 RVA: 0x002D8CFA File Offset: 0x002D6EFA
		private void CloseDefenseDeck()
		{
			NKCUIDeckViewer.Instance.Close();
			this.RefreshAsyncLock();
		}

		// Token: 0x06008693 RID: 34451 RVA: 0x002D8D10 File Offset: 0x002D6F10
		private NKM_ERROR_CODE CheckDefenseDeck()
		{
			NKMDeckIndex nkmdeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_PVP_DEFENCE, 0);
			return NKMMain.IsValidDeck(NKCScenManager.CurrentUserData().m_ArmyData, nkmdeckIndex.m_eDeckType, nkmdeckIndex.m_iIndex, NKM_GAME_TYPE.NGT_ASYNC_PVP);
		}

		// Token: 0x06008694 RID: 34452 RVA: 0x002D8D44 File Offset: 0x002D6F44
		public bool RefreshAsyncLock()
		{
			bool flag = false;
			NKM_ERROR_CODE nkm_ERROR_CODE = this.CheckCanPlayPVPGame();
			if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_END_SEASON || nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_END_WEEK)
			{
				flag = true;
				NKCUtil.SetLabelText(this.m_txtAsyncLock, NKCUtilString.GET_STRING_GAUNTLET_ASYNC_LOCK_CLOSING);
			}
			else if (this.CheckDefenseDeck() != NKM_ERROR_CODE.NEC_OK)
			{
				flag = true;
				NKCUtil.SetLabelText(this.m_txtAsyncLock, NKCUtilString.GET_STRING_GAUNTLET_ASYNC_LOCK_DEFENSE_DECK);
			}
			NKCUtil.SetGameobjectActive(this.m_objAsyncLock, flag && this.m_CurAsyncType != NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_RANK);
			return flag;
		}

		// Token: 0x06008695 RID: 34453 RVA: 0x002D8DB6 File Offset: 0x002D6FB6
		private void OnClickAsyncRefresh()
		{
			if (!this.CheckAsyncRfreshTime())
			{
				return;
			}
			if (!this.IsEnableAsync())
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_ASYNC_PVP_TARGET_LIST_REQ();
		}

		// Token: 0x06008696 RID: 34454 RVA: 0x002D8DCF File Offset: 0x002D6FCF
		private bool CheckAsyncRfreshTime()
		{
			return this.m_fAsyncRefreshTimer <= 0f || this.m_fAsyncRefreshTimer + 60f < Time.time;
		}

		// Token: 0x06008697 RID: 34455 RVA: 0x002D8DF6 File Offset: 0x002D6FF6
		private bool CheckNpcRfreshTime()
		{
			return this.m_fAsyncNpcBotTimer <= 0f || this.m_fAsyncNpcBotTimer + 60f < Time.time;
		}

		// Token: 0x06008698 RID: 34456 RVA: 0x002D8E20 File Offset: 0x002D7020
		private void RefreshAsyncButton()
		{
			bool flag = this.CheckAsyncRfreshTime();
			NKCUtil.SetGameobjectActive(this.m_objAsyncRefreshOn, flag);
			NKCUtil.SetGameobjectActive(this.m_objAsyncRefreshOff, !flag);
			if (flag)
			{
				NKCUtil.SetLabelText(this.m_txtAsyncRefreshTimer, "");
			}
		}

		// Token: 0x06008699 RID: 34457 RVA: 0x002D8E64 File Offset: 0x002D7064
		private NKMUserSimpleProfileData GetUserSimpleData(RANK_TYPE type, int index)
		{
			List<NKMUserSimpleProfileData> userSimpleData = this.GetUserSimpleData(type);
			if (userSimpleData.Count > index)
			{
				return userSimpleData[index];
			}
			return null;
		}

		// Token: 0x0600869A RID: 34458 RVA: 0x002D8E8C File Offset: 0x002D708C
		private List<NKMUserSimpleProfileData> GetUserSimpleData(RANK_TYPE _type)
		{
			List<NKMUserSimpleProfileData> result;
			if (this.m_dicUserSimpleData.TryGetValue(_type, out result))
			{
				return result;
			}
			return new List<NKMUserSimpleProfileData>();
		}

		// Token: 0x0600869B RID: 34459 RVA: 0x002D8EB0 File Offset: 0x002D70B0
		private void AddUserSimpleList(RANK_TYPE type, List<NKMUserSimpleProfileData> list)
		{
			if (!this.m_dicUserSimpleData.ContainsKey(type))
			{
				this.m_dicUserSimpleData.Add(type, new List<NKMUserSimpleProfileData>());
			}
			this.m_dicUserSimpleData[type].Clear();
			this.m_dicUserSimpleData[type] = list;
		}

		// Token: 0x0600869C RID: 34460 RVA: 0x002D8EEF File Offset: 0x002D70EF
		private int GetCurrentSeasonID()
		{
			return NKCUtil.FindPVPSeasonIDForAsync(NKCSynchronizedTime.GetServerUTCTime(0.0));
		}

		// Token: 0x0600869D RID: 34461 RVA: 0x002D8F04 File Offset: 0x002D7104
		private NKM_ERROR_CODE CheckCanPlayPVPGame()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			int seasonID = NKCUtil.FindPVPSeasonIDForAsync(NKCSynchronizedTime.GetServerUTCTime(0.0));
			int weekIDForAsync = NKCPVPManager.GetWeekIDForAsync(NKCSynchronizedTime.GetServerUTCTime(0.0), seasonID);
			return NKCPVPManager.CanPlayPVPAsyncGame(myUserData, seasonID, weekIDForAsync, NKCSynchronizedTime.GetServerUTCTime(0.0));
		}

		// Token: 0x0600869E RID: 34462 RVA: 0x002D8F59 File Offset: 0x002D7159
		private void OnToggleRankTab(bool _bSet, RANK_TYPE _newType)
		{
			if (!_bSet)
			{
				return;
			}
			if (this.m_CurRankType == _newType)
			{
				return;
			}
			this.UpdateRankData(_newType);
		}

		// Token: 0x0600869F RID: 34463 RVA: 0x002D8F70 File Offset: 0x002D7170
		private void UpdateRankData(RANK_TYPE _newType)
		{
			this.m_CurRankType = _newType;
			if (!this.TrySendRankUserListREQ(_newType, false))
			{
				this.UpdateScroll();
			}
		}

		// Token: 0x060086A0 RID: 34464 RVA: 0x002D8F8C File Offset: 0x002D718C
		public void OnToggleAsyncTab(bool _bSet, NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE _newTab, bool bForce = false)
		{
			if (!_bSet)
			{
				return;
			}
			if (this.m_CurAsyncType == _newTab && !bForce)
			{
				return;
			}
			if ((_newTab == NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_NPC && NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_ASYNC_NPC_MODE)) || (_newTab == NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_REVENGE && NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_ASYNC_REVENGE_MODE)))
			{
				switch (_newTab)
				{
				case NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_BATTLE:
					this.m_ctglAsyncBattle.Select(true, true, false);
					break;
				case NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_NPC:
					this.m_ctglAsyncNPC.Select(true, true, false);
					break;
				case NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_REVENGE:
					this.m_ctglAsyncRevenge.Select(true, true, false);
					break;
				case NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_RANK:
					this.m_ctglAsyncRank.Select(true, true, false);
					break;
				}
			}
			this.m_CurAsyncType = _newTab;
			this.UpdateTabUI();
			this.TrySendAllRankUserListREQ();
		}

		// Token: 0x060086A1 RID: 34465 RVA: 0x002D9034 File Offset: 0x002D7234
		private void TrySendAllRankUserListREQ()
		{
			if ((this.m_CurAsyncType == NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_BATTLE && this.m_bSendBattleListReq) || (this.m_CurAsyncType == NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_RANK && this.m_bSendRankListReq) || (this.m_CurAsyncType == NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_NPC && this.m_bSendNPCListReq))
			{
				this.UpdateScroll();
				return;
			}
			if (this.UpdateAsyncLockUI())
			{
				this.m_bNotSetDefenceDeck = (this.CheckDefenseDeck() > NKM_ERROR_CODE.NEC_OK);
				return;
			}
			if (this.m_CurAsyncType == NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_BATTLE)
			{
				this.m_bSendBattleListReq = true;
			}
			if (this.m_CurAsyncType == NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_RANK)
			{
				this.m_bSendRankListReq = true;
			}
			if (this.m_CurAsyncType == NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_NPC)
			{
				this.m_bSendNPCListReq = true;
			}
			this.SendPVPListREQ();
		}

		// Token: 0x060086A2 RID: 34466 RVA: 0x002D90C8 File Offset: 0x002D72C8
		private void SendPVPListREQ()
		{
			switch (this.m_CurAsyncType)
			{
			case NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_BATTLE:
			{
				List<AsyncPvpTarget> asyncTargetList = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().AsyncTargetList;
				if (asyncTargetList.Count > 0)
				{
					this.SetTartgetList(asyncTargetList);
					this.RefreshAsyncButton();
					this.UpdateAsyncRefreshTime();
					this.UpdateScroll();
					return;
				}
				NKCPacketSender.Send_NKMPacket_ASYNC_PVP_TARGET_LIST_REQ();
				return;
			}
			case NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_NPC:
				if (this.CheckNpcRfreshTime())
				{
					NKCPacketSender.Send_NKMPacket_NPC_PVP_TARGET_LIST_REQ(this.m_iCurNPCBotTier);
					NKCUtil.SetGameobjectActive(this.m_lvsrAsyncNPC.gameObject, false);
				}
				else
				{
					this.UpdateScroll();
				}
				this.OnSelectTierSlot(this.m_iCurNPCBotTier);
				return;
			case NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_REVENGE:
				NKCUtil.SetGameobjectActive(this.m_lvsrAsyncRevenge.gameObject, false);
				NKCPacketSender.Send_NKMPacket_REVENGE_PVP_TARGET_LIST_REQ();
				return;
			case NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_RANK:
				switch (this.m_CurRankType)
				{
				case RANK_TYPE.MY_LEAGUE:
					this.m_ctglMyLeague.Select(true, true, false);
					break;
				case RANK_TYPE.ALL:
					this.m_ctglAll.Select(true, true, false);
					break;
				case RANK_TYPE.FRIEND:
					this.m_ctglFriend.Select(true, true, false);
					break;
				}
				this.UpdateRankData(this.m_CurRankType);
				return;
			default:
				return;
			}
		}

		// Token: 0x060086A3 RID: 34467 RVA: 0x002D91D4 File Offset: 0x002D73D4
		private bool UpdateAsyncLockUI()
		{
			if (this.m_CurAsyncType == NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_RANK)
			{
				NKCUtil.SetGameobjectActive(this.m_objAsyncLock, false);
				return false;
			}
			return this.RefreshAsyncLock();
		}

		// Token: 0x060086A4 RID: 34468 RVA: 0x002D91F4 File Offset: 0x002D73F4
		private void UpdateTabUI()
		{
			bool flag = false;
			if (this.m_CurAsyncType == NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_RANK)
			{
				NKCUtil.SetGameobjectActive(this.m_objAsyncLock, false);
			}
			else
			{
				flag = this.RefreshAsyncLock();
			}
			NKCUtil.SetGameobjectActive(this.m_objAsyncBattle, this.m_CurAsyncType == NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_BATTLE && !flag);
			NKCUtil.SetGameobjectActive(this.m_objAsyncNPC, this.m_CurAsyncType == NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_NPC && !flag);
			NKCUtil.SetGameobjectActive(this.m_objAsyncRevenge, this.m_CurAsyncType == NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_REVENGE && !flag);
			NKCUtil.SetGameobjectActive(this.m_objAsyncRank, this.m_CurAsyncType == NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_RANK);
			NKCUtil.SetGameobjectActive(this.m_csbtnAsyncRefresh, this.m_CurAsyncType == NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_BATTLE);
			NKCUtil.SetGameobjectActive(this.m_lbRemainRankTime.gameObject, this.m_CurAsyncType != NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_REVENGE);
			if (!this.m_bPlayIntro)
			{
				this.m_amtorRankCenter.Play("NKM_UI_GAUNTLET_LOBBY_CONTENT_INTRO_CENTER_FADEIN");
				return;
			}
			this.m_bPlayIntro = false;
		}

		// Token: 0x060086A5 RID: 34469 RVA: 0x002D92D8 File Offset: 0x002D74D8
		private void UpdateScroll()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			switch (this.m_CurAsyncType)
			{
			case NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_BATTLE:
				this.RefreshScrollRect(this.m_lvsrAsyncBattle, this.m_CurAsyncType, this.m_listAsyncTarget.Count);
				return;
			case NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_NPC:
				this.RefreshScrollRect(this.m_lvsrAsyncNPC, this.m_CurAsyncType, this.m_lstCurNpcBot.Count);
				return;
			case NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_REVENGE:
				this.RefreshScrollRect(this.m_lvsrAsyncRevenge, this.m_CurAsyncType, this.m_listRevengeTarget.Count);
				return;
			case NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_RANK:
				this.RefreshScrollRect(this.m_lvsrAsyncRank, this.m_CurAsyncType, this.GetUserSimpleData(this.m_CurRankType).Count);
				return;
			default:
				return;
			}
		}

		// Token: 0x060086A6 RID: 34470 RVA: 0x002D938E File Offset: 0x002D758E
		public RectTransform GetSlotAsyncBattle(int index)
		{
			return NKCUIGauntletAsyncSlot.GetNewInstance(this.m_lvsrAsyncRevenge.content, new NKCUIGauntletAsyncSlot.OnTouchBattleAsync(this.OnClickAsyncBattle), new NKCUIGauntletAsyncSlot.OnTouchProfile(this.OnClickFriendProfile)).GetComponent<RectTransform>();
		}

		// Token: 0x060086A7 RID: 34471 RVA: 0x002D93C0 File Offset: 0x002D75C0
		public void ReturnSlotAsyncBattle(Transform tr)
		{
			tr.SetParent(base.transform);
			NKCUIGauntletAsyncSlot component = tr.GetComponent<NKCUIGauntletAsyncSlot>();
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x060086A8 RID: 34472 RVA: 0x002D93FC File Offset: 0x002D75FC
		public void ProvideDataAsyncBattle(Transform tr, int index)
		{
			NKCUIGauntletAsyncSlot component = tr.GetComponent<NKCUIGauntletAsyncSlot>();
			if (component != null)
			{
				if (this.m_listAsyncTarget.Count <= index)
				{
					Debug.LogError(string.Format("Async PVP data �̻���. target : {0} <= {1}", this.m_listAsyncTarget.Count, index));
				}
				component.SetUI(this.m_listAsyncTarget[index], NKM_GAME_TYPE.NGT_PVP_STRATEGY);
			}
		}

		// Token: 0x060086A9 RID: 34473 RVA: 0x002D9460 File Offset: 0x002D7660
		public RectTransform GetSlotAsyncNPC(int index)
		{
			return NKCUIGauntletAsyncSlot.GetNewInstance(this.m_lvsrAsyncNPC.content, new NKCUIGauntletAsyncSlot.OnTouchBattle(this.OnClickAsyncNPC)).GetComponent<RectTransform>();
		}

		// Token: 0x060086AA RID: 34474 RVA: 0x002D9484 File Offset: 0x002D7684
		public void ReturnSlotAsyncNPC(Transform tr)
		{
			tr.SetParent(base.transform);
			NKCUIGauntletAsyncSlot component = tr.GetComponent<NKCUIGauntletAsyncSlot>();
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x060086AB RID: 34475 RVA: 0x002D94C0 File Offset: 0x002D76C0
		public void ProvideDataAsyncNPC(Transform tr, int index)
		{
			NKCUIGauntletAsyncSlot component = tr.GetComponent<NKCUIGauntletAsyncSlot>();
			if (this.m_lstCurNpcBot.Count > index)
			{
				component.SetUI(this.m_lstCurNpcBot[index]);
				return;
			}
			Debug.LogError(string.Format("ProvideDataAsyncNPC target : {0} <= {1}", this.m_lstCurNpcBot.Count, index));
		}

		// Token: 0x060086AC RID: 34476 RVA: 0x002D951A File Offset: 0x002D771A
		public RectTransform GetSlotAsyncRevenge(int index)
		{
			return NKCUIGauntletAsyncSlotNew.GetNewInstance(this.m_lvsrAsyncRevenge.content, new NKCUIGauntletAsyncSlotNew.OnTouchBattle(this.OnClickAsyncRevenge), new NKCUIGauntletAsyncSlotNew.OnTouchProfile(this.OnClickFriendProfile)).GetComponent<RectTransform>();
		}

		// Token: 0x060086AD RID: 34477 RVA: 0x002D954C File Offset: 0x002D774C
		public void ReturnSlotAsyncRevenge(Transform tr)
		{
			tr.SetParent(base.transform);
			NKCUIGauntletAsyncSlotNew component = tr.GetComponent<NKCUIGauntletAsyncSlotNew>();
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x060086AE RID: 34478 RVA: 0x002D9588 File Offset: 0x002D7788
		public void ProvideDataAsyncRevenge(Transform tr, int index)
		{
			NKCUIGauntletAsyncSlotNew component = tr.GetComponent<NKCUIGauntletAsyncSlotNew>();
			if (component != null)
			{
				if (this.m_listRevengeTarget.Count <= index)
				{
					Debug.LogError(string.Format("Revenge data �̻���. target : {0} <= {1}", this.m_listRevengeTarget.Count, index));
				}
				component.SetUI(this.m_listRevengeTarget[index], NKM_GAME_TYPE.NGT_PVP_STRATEGY_REVENGE);
			}
		}

		// Token: 0x060086AF RID: 34479 RVA: 0x002D95EC File Offset: 0x002D77EC
		public RectTransform GetSlotAsyncRank(int index)
		{
			return NKCUIGauntletLRSlot.GetNewInstance(this.m_lvsrAsyncRank.content, new NKCUIGauntletLRSlot.OnDragBegin(this.OnEventPanelBeginDragAll)).GetComponent<RectTransform>();
		}

		// Token: 0x060086B0 RID: 34480 RVA: 0x002D9610 File Offset: 0x002D7810
		public void ReturnSlotAsyncRank(Transform tr)
		{
			tr.SetParent(base.transform);
			NKCUIGauntletLRSlot component = tr.GetComponent<NKCUIGauntletLRSlot>();
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x060086B1 RID: 34481 RVA: 0x002D964C File Offset: 0x002D784C
		public void ProvideDataAsyncRank(Transform tr, int index)
		{
			NKCUIGauntletLRSlot component = tr.GetComponent<NKCUIGauntletLRSlot>();
			if (component != null)
			{
				component.SetUI(this.GetUserSimpleData(this.m_CurRankType, index), index + 1, NKM_GAME_TYPE.NGT_PVP_STRATEGY);
			}
		}

		// Token: 0x060086B2 RID: 34482 RVA: 0x002D9684 File Offset: 0x002D7884
		public RectTransform GetSlotAsyncNPCSub(int idx)
		{
			NKCUIGauntletLobbyAsyncSubTab newInstance = NKCUIGauntletLobbyAsyncSubTab.GetNewInstance(this.m_lhsrNPCSub.transform);
			if (newInstance != null)
			{
				RectTransform component = newInstance.GetComponent<RectTransform>();
				if (null != component)
				{
					return component;
				}
			}
			return null;
		}

		// Token: 0x060086B3 RID: 34483 RVA: 0x002D96C0 File Offset: 0x002D78C0
		public void ReturnSlotAsyncNPCSub(Transform tr)
		{
			if (tr == null)
			{
				return;
			}
			NKCUIGauntletLobbyAsyncSubTab component = tr.GetComponent<NKCUIGauntletLobbyAsyncSubTab>();
			if (null != component)
			{
				this.m_lstSubTalSlots.Remove(component);
			}
			NKCUtil.SetGameobjectActive(tr, false);
		}

		// Token: 0x060086B4 RID: 34484 RVA: 0x002D96FC File Offset: 0x002D78FC
		public void ProvideDataAsyncNPCSub(Transform tr, int index)
		{
			NKCUIGauntletLobbyAsyncSubTab component = tr.GetComponent<NKCUIGauntletLobbyAsyncSubTab>();
			if (component != null)
			{
				component.SetData(index + 1, new NKCUIGauntletLobbyAsyncSubTab.OnClickSubTab(this.OnClickAsyncNPCSubTab), index + 1 == this.m_iCurNPCBotTier);
			}
			if (null != component)
			{
				this.m_lstSubTalSlots.Add(component);
			}
		}

		// Token: 0x060086B5 RID: 34485 RVA: 0x002D9748 File Offset: 0x002D7948
		public void OnClickAsyncNPCSubTab(int _iSelectedTier)
		{
			if (this.m_iCurNPCBotTier == _iSelectedTier)
			{
				return;
			}
			this.m_iCurNPCBotTier = _iSelectedTier;
			this.OnSelectTierSlot(this.m_iCurNPCBotTier);
			NKCPacketSender.Send_NKMPacket_NPC_PVP_TARGET_LIST_REQ(this.m_iCurNPCBotTier);
		}

		// Token: 0x060086B6 RID: 34486 RVA: 0x002D9774 File Offset: 0x002D7974
		private void OnClickAsyncNPC(long _targetNpcFriendCode)
		{
			foreach (NpcPvpTarget npcPvpTarget in this.m_lstCurNpcBot)
			{
				if (npcPvpTarget.userFriendCode == _targetNpcFriendCode)
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_ASYNC_READY().SetReserveData(npcPvpTarget);
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_ASYNC_READY, true);
					break;
				}
			}
		}

		// Token: 0x060086B7 RID: 34487 RVA: 0x002D97E8 File Offset: 0x002D79E8
		private void OnSelectTierSlot(int iKey)
		{
			if (this.m_CurAsyncType != NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_NPC)
			{
				return;
			}
			foreach (NKCUIGauntletLobbyAsyncSubTab nkcuigauntletLobbyAsyncSubTab in this.m_lstSubTalSlots)
			{
				nkcuigauntletLobbyAsyncSubTab.OnSelect(nkcuigauntletLobbyAsyncSubTab.Tier == iKey);
			}
		}

		// Token: 0x060086B8 RID: 34488 RVA: 0x002D984C File Offset: 0x002D7A4C
		public static AsyncPvpTarget ConventToAsyncPvpTarget(NpcPvpTarget _npcTarget)
		{
			AsyncPvpTarget asyncPvpTarget = new AsyncPvpTarget();
			if (_npcTarget != null)
			{
				asyncPvpTarget.userLevel = _npcTarget.userLevel;
				asyncPvpTarget.userNickName = _npcTarget.userNickName;
				asyncPvpTarget.userFriendCode = _npcTarget.userFriendCode;
				asyncPvpTarget.rank = 0;
				asyncPvpTarget.score = _npcTarget.score;
				asyncPvpTarget.tier = _npcTarget.tier;
				asyncPvpTarget.mainUnitId = 0;
				asyncPvpTarget.mainUnitSkinId = 0;
				asyncPvpTarget.selfieFrameId = 0;
				asyncPvpTarget.asyncDeck = _npcTarget.asyncDeck;
				asyncPvpTarget.guildData = null;
			}
			return asyncPvpTarget;
		}

		// Token: 0x060086B9 RID: 34489 RVA: 0x002D98CE File Offset: 0x002D7ACE
		public void SetReserveOpenNpcBotTier(int newOpenTier)
		{
			this.m_iNewOpendNpcTier = newOpenTier;
		}

		// Token: 0x060086BA RID: 34490 RVA: 0x002D98D8 File Offset: 0x002D7AD8
		private void OnNewOpenTierSlotEffect()
		{
			if (this.m_iNewOpendNpcTier == 0)
			{
				return;
			}
			foreach (NKCUIGauntletLobbyAsyncSubTab nkcuigauntletLobbyAsyncSubTab in this.m_lstSubTalSlots)
			{
				if (nkcuigauntletLobbyAsyncSubTab.Tier == this.m_iNewOpendNpcTier)
				{
					nkcuigauntletLobbyAsyncSubTab.OnActiveEffect();
					break;
				}
			}
			this.m_iNewOpendNpcTier = 0;
		}

		// Token: 0x040072FE RID: 29438
		public Animator m_amtorRankCenter;

		// Token: 0x040072FF RID: 29439
		[Header("������v1(off)")]
		public GameObject m_objNKM_UI_GAUNTLET_RANK_UPSIDEMENU;

		// Token: 0x04007300 RID: 29440
		public GameObject m_objNKM_UI_GAUNTLET_RANK_ASYNC_SCROLL;

		// Token: 0x04007301 RID: 29441
		public GameObject m_objNKM_UI_GAUNTLET_RANK_ALL_SCROLL;

		// Token: 0x04007302 RID: 29442
		public GameObject m_objNKM_UI_GAUNTLET_RANK_FRIEND_SCROLL;

		// Token: 0x04007303 RID: 29443
		[Header("������v2")]
		public GameObject m_objNKM_UI_GAUNTLET_RANK_UPSIDEMENU_NEW;

		// Token: 0x04007304 RID: 29444
		[Space]
		public NKCUIComToggle m_ctglAsyncBattle;

		// Token: 0x04007305 RID: 29445
		public NKCUIComToggle m_ctglAsyncNPC;

		// Token: 0x04007306 RID: 29446
		public NKCUIComToggle m_ctglAsyncRevenge;

		// Token: 0x04007307 RID: 29447
		public NKCUIComToggle m_ctglAsyncRank;

		// Token: 0x04007308 RID: 29448
		[Space]
		public LoopVerticalScrollRect m_lvsrAsyncBattle;

		// Token: 0x04007309 RID: 29449
		public LoopVerticalScrollRect m_lvsrAsyncNPC;

		// Token: 0x0400730A RID: 29450
		public LoopVerticalScrollRect m_lvsrAsyncRevenge;

		// Token: 0x0400730B RID: 29451
		public LoopVerticalScrollRect m_lvsrAsyncRank;

		// Token: 0x0400730C RID: 29452
		[Space]
		public LoopHorizontalScrollRect m_lhsrNPCSub;

		// Token: 0x0400730D RID: 29453
		[Header("��ũ�� ������Ʈ")]
		public GameObject m_objAsyncBattle;

		// Token: 0x0400730E RID: 29454
		public GameObject m_objAsyncNPC;

		// Token: 0x0400730F RID: 29455
		public GameObject m_objAsyncRevenge;

		// Token: 0x04007310 RID: 29456
		public GameObject m_objAsyncRank;

		// Token: 0x04007311 RID: 29457
		[Header("��ŷ ���� ��")]
		public NKCUIComToggle m_ctglAll;

		// Token: 0x04007312 RID: 29458
		public NKCUIComToggle m_ctglMyLeague;

		// Token: 0x04007313 RID: 29459
		public NKCUIComToggle m_ctglFriend;

		// Token: 0x04007314 RID: 29460
		[Header("�߾� �ϴ�")]
		public Text m_lbRemainRankTime;

		// Token: 0x04007315 RID: 29461
		public NKCUIComStateButton m_csbtnAsyncRefresh;

		// Token: 0x04007316 RID: 29462
		public GameObject m_objAsyncRefreshOn;

		// Token: 0x04007317 RID: 29463
		public GameObject m_objAsyncRefreshOff;

		// Token: 0x04007318 RID: 29464
		public Text m_txtAsyncRefreshTimer;

		// Token: 0x04007319 RID: 29465
		[Header("etc")]
		public GameObject m_objAsyncLock;

		// Token: 0x0400731A RID: 29466
		public Text m_txtAsyncLock;

		// Token: 0x0400731B RID: 29467
		[Header("����")]
		public NKCUIGauntletLobbyRightSideAsync m_RightSide;

		// Token: 0x0400731C RID: 29468
		private NKCPopupGauntletLeagueGuide m_NKCPopupGauntletLeagueGuide;

		// Token: 0x0400731D RID: 29469
		private bool m_bFirstOpen = true;

		// Token: 0x0400731E RID: 29470
		private bool m_bPrepareLoopScrollCells;

		// Token: 0x0400731F RID: 29471
		private List<AsyncPvpTarget> m_listAsyncTarget = new List<AsyncPvpTarget>();

		// Token: 0x04007320 RID: 29472
		private List<RevengePvpTarget> m_listRevengeTarget = new List<RevengePvpTarget>();

		// Token: 0x04007321 RID: 29473
		private float m_fPrevUpdateTime;

		// Token: 0x04007322 RID: 29474
		private float m_fAsyncRefreshTimer;

		// Token: 0x04007323 RID: 29475
		private float m_fAsyncNpcBotTimer;

		// Token: 0x04007324 RID: 29476
		private bool m_bPlayIntro = true;

		// Token: 0x04007325 RID: 29477
		private const int ASYNC_REFRESH_TIMER = 60;

		// Token: 0x04007326 RID: 29478
		private bool[] m_arRankREQ = new bool[4];

		// Token: 0x04007327 RID: 29479
		private bool[] m_arAllRankREQ = new bool[4];

		// Token: 0x04007328 RID: 29480
		private bool[] m_arOpened = new bool[4];

		// Token: 0x04007329 RID: 29481
		private NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE m_CurAsyncType;

		// Token: 0x0400732A RID: 29482
		private RANK_TYPE m_CurRankType;

		// Token: 0x0400732B RID: 29483
		private int m_iAlreadySelectedNPCSubTier;

		// Token: 0x0400732C RID: 29484
		private Dictionary<RANK_TYPE, List<NKMUserSimpleProfileData>> m_dicUserSimpleData = new Dictionary<RANK_TYPE, List<NKMUserSimpleProfileData>>();

		// Token: 0x0400732D RID: 29485
		private int m_iCurNPCBotTier;

		// Token: 0x0400732E RID: 29486
		private List<NpcPvpTarget> m_lstCurNpcBot = new List<NpcPvpTarget>();

		// Token: 0x0400732F RID: 29487
		private int m_iNPCBotMaxOpendTier;

		// Token: 0x04007330 RID: 29488
		private bool m_bSendBattleListReq;

		// Token: 0x04007331 RID: 29489
		private bool m_bSendRankListReq;

		// Token: 0x04007332 RID: 29490
		private bool m_bSendNPCListReq;

		// Token: 0x04007333 RID: 29491
		private bool m_bNotSetDefenceDeck;

		// Token: 0x04007334 RID: 29492
		private List<NKCUIGauntletLobbyAsyncSubTab> m_lstSubTalSlots = new List<NKCUIGauntletLobbyAsyncSubTab>();

		// Token: 0x04007335 RID: 29493
		private int m_iNewOpendNpcTier;

		// Token: 0x02001919 RID: 6425
		public enum PVP_ASYNC_TYPE
		{
			// Token: 0x0400AA9B RID: 43675
			PAT_BATTLE,
			// Token: 0x0400AA9C RID: 43676
			PAT_NPC,
			// Token: 0x0400AA9D RID: 43677
			PAT_REVENGE,
			// Token: 0x0400AA9E RID: 43678
			PAT_RANK,
			// Token: 0x0400AA9F RID: 43679
			MAX
		}
	}
}
