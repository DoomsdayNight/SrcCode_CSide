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
	// Token: 0x02000B73 RID: 2931
	public class NKCUIGauntletLobbyAsync : MonoBehaviour
	{
		// Token: 0x06008649 RID: 34377 RVA: 0x002D7304 File Offset: 0x002D5504
		public RANK_TYPE GetCurrRankType()
		{
			return this.m_RANK_TYPE;
		}

		// Token: 0x0600864A RID: 34378 RVA: 0x002D730C File Offset: 0x002D550C
		public void Init()
		{
			this.m_csbtnAsyncRefresh.PointerClick.RemoveAllListeners();
			this.m_csbtnAsyncRefresh.PointerClick.AddListener(new UnityAction(this.OnClickAsyncRefresh));
			this.m_ctglAsyncTab.OnValueChanged.RemoveAllListeners();
			this.m_ctglAsyncTab.OnValueChanged.AddListener(new UnityAction<bool>(this.OnTabChangedToAsyncTarget));
			this.m_ctglAllTab.OnValueChanged.RemoveAllListeners();
			this.m_ctglAllTab.OnValueChanged.AddListener(new UnityAction<bool>(this.OnTabChangedToAll));
			this.m_ctglFriendTab.OnValueChanged.RemoveAllListeners();
			this.m_ctglFriendTab.OnValueChanged.AddListener(new UnityAction<bool>(this.OnTabChangedToFriend));
			this.m_lvsrAsyncTartget.dOnGetObject += this.GetSlotAsync;
			this.m_lvsrAsyncTartget.dOnReturnObject += this.ReturnAsyncSlot;
			this.m_lvsrAsyncTartget.dOnProvideData += this.ProvideDataAsync;
			this.m_lvsrAsyncTartget.ContentConstraintCount = 1;
			this.m_lvsrRankAll.dOnGetObject += this.GetSlotAll;
			this.m_lvsrRankAll.dOnReturnObject += this.ReturnSlot;
			this.m_lvsrRankAll.dOnProvideData += this.ProvideDataAll;
			this.m_lvsrRankAll.ContentConstraintCount = 1;
			NKCUtil.SetScrollHotKey(this.m_lvsrRankAll, null);
			this.m_lvsrRankFriend.dOnGetObject += this.GetSlotFriend;
			this.m_lvsrRankFriend.dOnReturnObject += this.ReturnSlot;
			this.m_lvsrRankFriend.dOnProvideData += this.ProvideDataFriend;
			this.m_lvsrRankFriend.ContentConstraintCount = 1;
			NKCUtil.SetScrollHotKey(this.m_lvsrRankFriend, null);
			this.m_RightSide.InitUI();
			this.m_RightSide.SetCallback(new NKCUIDeckViewer.DeckViewerOption.OnBackButton(this.CloseDefenseDeck));
			bool flag = NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_ASYNC_NEW_MODE);
			NKCUtil.SetGameobjectActive(this.m_objNKM_UI_GAUNTLET_RANK_UPSIDEMENU, !flag);
			NKCUtil.SetGameobjectActive(this.m_objNKM_UI_GAUNTLET_RANK_UPSIDEMENU_NEW, flag);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAUNTLET_ASYNC_LIST_SCROLL, flag);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAUNTLET_ASYNC_NPC_SCROLL, flag);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAUNTLET_ASYNC_REVENGE_SCROLL, flag);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_GAUNTLET_ASYNC_RANK_SCROLL, flag);
		}

		// Token: 0x0600864B RID: 34379 RVA: 0x002D754C File Offset: 0x002D574C
		private void UpdateRemainTimeUI()
		{
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

		// Token: 0x0600864C RID: 34380 RVA: 0x002D75D7 File Offset: 0x002D57D7
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

		// Token: 0x0600864D RID: 34381 RVA: 0x002D7610 File Offset: 0x002D5810
		private void UpdateAsyncRefreshTime()
		{
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

		// Token: 0x0600864E RID: 34382 RVA: 0x002D7681 File Offset: 0x002D5881
		private void TrySendAllRankUserListREQ(RANK_TYPE rt)
		{
			if (this.m_arAllRankREQ[(int)rt])
			{
				return;
			}
			this.m_arAllRankREQ[(int)rt] = true;
			this.SendPVPListREQ(rt, true);
		}

		// Token: 0x0600864F RID: 34383 RVA: 0x002D769F File Offset: 0x002D589F
		private void OnEventPanelBeginDragMyLeague()
		{
			this.TrySendAllRankUserListREQ(RANK_TYPE.MY_LEAGUE);
		}

		// Token: 0x06008650 RID: 34384 RVA: 0x002D76A8 File Offset: 0x002D58A8
		private void OnEventPanelBeginDragAll()
		{
			this.TrySendAllRankUserListREQ(RANK_TYPE.ALL);
		}

		// Token: 0x06008651 RID: 34385 RVA: 0x002D76B1 File Offset: 0x002D58B1
		private void OnEventPanelBeginDragFriend()
		{
			this.TrySendAllRankUserListREQ(RANK_TYPE.FRIEND);
		}

		// Token: 0x06008652 RID: 34386 RVA: 0x002D76BA File Offset: 0x002D58BA
		public RectTransform GetSlotAsync(int index)
		{
			return NKCUIGauntletAsyncSlot.GetNewInstance(this.m_trAsyncTartget, new NKCUIGauntletAsyncSlot.OnTouchBattle(this.OnClickAsyncBattle)).GetComponent<RectTransform>();
		}

		// Token: 0x06008653 RID: 34387 RVA: 0x002D76D8 File Offset: 0x002D58D8
		public void ReturnSlot(Transform tr)
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

		// Token: 0x06008654 RID: 34388 RVA: 0x002D7714 File Offset: 0x002D5914
		public void ReturnAsyncSlot(Transform tr)
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

		// Token: 0x06008655 RID: 34389 RVA: 0x002D7750 File Offset: 0x002D5950
		public void ProvideDataAsync(Transform tr, int index)
		{
			NKCUIGauntletAsyncSlot component = tr.GetComponent<NKCUIGauntletAsyncSlot>();
			if (component != null)
			{
				if (this.m_listAsyncTarget.Count <= index)
				{
					Debug.LogError(string.Format("Async PVP data 이상함. target : {0} <= {1}", this.m_listAsyncTarget.Count, index));
				}
				component.SetUI(this.m_listAsyncTarget[index]);
			}
		}

		// Token: 0x06008656 RID: 34390 RVA: 0x002D77B2 File Offset: 0x002D59B2
		public RectTransform GetSlotAll(int index)
		{
			return NKCUIGauntletLRSlot.GetNewInstance(this.m_trRankAll, new NKCUIGauntletLRSlot.OnDragBegin(this.OnEventPanelBeginDragAll)).GetComponent<RectTransform>();
		}

		// Token: 0x06008657 RID: 34391 RVA: 0x002D77D0 File Offset: 0x002D59D0
		public void ProvideDataAll(Transform tr, int index)
		{
			NKCUIGauntletLRSlot component = tr.GetComponent<NKCUIGauntletLRSlot>();
			if (component != null)
			{
				component.SetUI(this.GetUserSimpleData(RANK_TYPE.ALL, index), index + 1, NKM_GAME_TYPE.NGT_ASYNC_PVP);
			}
		}

		// Token: 0x06008658 RID: 34392 RVA: 0x002D7800 File Offset: 0x002D5A00
		public RectTransform GetSlotFriend(int index)
		{
			return NKCUIGauntletLRSlot.GetNewInstance(this.m_trRankFriend, new NKCUIGauntletLRSlot.OnDragBegin(this.OnEventPanelBeginDragFriend)).GetComponent<RectTransform>();
		}

		// Token: 0x06008659 RID: 34393 RVA: 0x002D7820 File Offset: 0x002D5A20
		public void ProvideDataFriend(Transform tr, int index)
		{
			NKCUIGauntletLRSlot component = tr.GetComponent<NKCUIGauntletLRSlot>();
			if (component != null)
			{
				component.SetUI(this.GetUserSimpleData(RANK_TYPE.FRIEND, index), index + 1, NKM_GAME_TYPE.NGT_ASYNC_PVP);
			}
		}

		// Token: 0x0600865A RID: 34394 RVA: 0x002D7850 File Offset: 0x002D5A50
		private void RefreshRankTabCells()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			if (this.m_RANK_TYPE == RANK_TYPE.MY_LEAGUE)
			{
				this.RefreshScrollRect(this.m_lvsrAsyncTartget, this.m_RANK_TYPE, this.m_listAsyncTarget.Count);
				return;
			}
			if (this.m_RANK_TYPE == RANK_TYPE.ALL)
			{
				this.RefreshScrollRect(this.m_lvsrRankAll, this.m_RANK_TYPE, this.GetUserSimpleList(this.m_RANK_TYPE).Count);
				return;
			}
			if (this.m_RANK_TYPE == RANK_TYPE.FRIEND)
			{
				this.RefreshScrollRect(this.m_lvsrRankFriend, this.m_RANK_TYPE, this.GetUserSimpleList(this.m_RANK_TYPE).Count);
				this.m_lbEmptyMessage.text = NKCUtilString.GET_STRING_FRIEND_LIST_IS_EMPTY;
				NKCUtil.SetGameobjectActive(this.m_objEmptyList, this.m_lvsrRankFriend.TotalCount == 0);
			}
		}

		// Token: 0x0600865B RID: 34395 RVA: 0x002D7913 File Offset: 0x002D5B13
		private void RefreshScrollRect(LoopVerticalScrollRect scrollRect, RANK_TYPE type, int count)
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

		// Token: 0x0600865C RID: 34396 RVA: 0x002D794C File Offset: 0x002D5B4C
		public void OnRecv(NKMPacket_ASYNC_PVP_TARGET_LIST_ACK spacket)
		{
			if (NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().AsyncTargetList.Count > 0)
			{
				this.m_fAsyncRefreshTimer = Time.time;
				NKC_SCEN_GAUNTLET_LOBBY.AsyncRefreshCooltime = this.m_fAsyncRefreshTimer;
			}
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetAsyncTargetList(spacket.targetList);
			this.SetTartgetList(NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().AsyncTargetList);
			this.RefreshAsyncButton();
			this.UpdateAsyncRefreshTime();
			if (this.m_RANK_TYPE == RANK_TYPE.MY_LEAGUE)
			{
				this.RefreshRankTabCells();
			}
		}

		// Token: 0x0600865D RID: 34397 RVA: 0x002D79CA File Offset: 0x002D5BCA
		public void SetTartgetList(List<AsyncPvpTarget> targetList)
		{
			this.m_listAsyncTarget.Clear();
			this.m_listAsyncTarget.AddRange(targetList);
		}

		// Token: 0x0600865E RID: 34398 RVA: 0x002D79E4 File Offset: 0x002D5BE4
		public void OnRecv(NKMPacket_ASYNC_PVP_RANK_LIST_ACK spacket)
		{
			this.AddUserSimpleList(spacket.rankType, spacket.userProfileDataList);
			if (spacket.rankType == RANK_TYPE.FRIEND)
			{
				this.GetUserSimpleList(spacket.rankType).Sort((NKMUserSimpleProfileData a, NKMUserSimpleProfileData b) => b.pvpScore.CompareTo(a.pvpScore));
			}
			if (spacket.userProfileDataList.Count < NKMPvpCommonConst.Instance.RANK_SIMPLE_COUNT)
			{
				this.m_arAllRankREQ[(int)spacket.rankType] = true;
			}
			if (this.m_RANK_TYPE == spacket.rankType)
			{
				this.RefreshRankTabCells();
			}
		}

		// Token: 0x0600865F RID: 34399 RVA: 0x002D7A75 File Offset: 0x002D5C75
		public void OnRecv(NKMPacket_UPDATE_DEFENCE_DECK_ACK packet)
		{
			this.RefreshAsyncLock();
		}

		// Token: 0x06008660 RID: 34400 RVA: 0x002D7A7D File Offset: 0x002D5C7D
		public void OnRecv(NKMPacket_ASYNC_PVP_RANK_SEASON_REWARD_ACK packet)
		{
			this.m_RightSide.UpdateNowSeasonPVPInfoUI(NKM_GAME_TYPE.NGT_ASYNC_PVP);
			if (this.m_RANK_TYPE != RANK_TYPE.MY_LEAGUE)
			{
				this.SendPVPListREQ(this.m_RANK_TYPE, false);
			}
		}

		// Token: 0x06008661 RID: 34401 RVA: 0x002D7AA1 File Offset: 0x002D5CA1
		public void OnRecv(NKMPacket_PVP_CHARGE_POINT_REFRESH_ACK cNKMPacket_PVP_CHARGE_POINT_REFRESH_ACK)
		{
			this.m_RightSide.UpdateRankPVPPointUI();
		}

		// Token: 0x06008662 RID: 34402 RVA: 0x002D7AB0 File Offset: 0x002D5CB0
		public void SetUI()
		{
			this.m_bPlayIntro = true;
			this.m_fAsyncRefreshTimer = NKC_SCEN_GAUNTLET_LOBBY.AsyncRefreshCooltime;
			this.RefreshAsyncLock();
			if (!this.m_bPrepareLoopScrollCells)
			{
				NKCUtil.SetGameobjectActive(this.m_objAsyncTarget, true);
				NKCUtil.SetGameobjectActive(this.m_objRankAll, true);
				NKCUtil.SetGameobjectActive(this.m_objRankFriend, true);
				this.m_lvsrAsyncTartget.PrepareCells(0);
				this.m_lvsrRankAll.PrepareCells(0);
				this.m_lvsrRankFriend.PrepareCells(0);
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
				this.m_bFirstOpen = false;
			}
			if (this.m_RANK_TYPE == RANK_TYPE.MY_LEAGUE)
			{
				this.m_ctglAsyncTab.Select(false, true, false);
				this.m_ctglAsyncTab.Select(true, false, false);
			}
			else if (this.m_RANK_TYPE == RANK_TYPE.ALL)
			{
				this.m_ctglAllTab.Select(false, true, false);
				this.m_ctglAllTab.Select(true, false, false);
			}
			else if (this.m_RANK_TYPE == RANK_TYPE.FRIEND)
			{
				this.m_ctglFriendTab.Select(false, true, false);
				this.m_ctglFriendTab.Select(true, false, false);
			}
			else
			{
				this.m_ctglAsyncTab.Select(false, true, false);
				this.m_ctglAsyncTab.Select(true, false, false);
			}
			this.UpdateRemainTimeUI();
			this.m_RightSide.UpdateRankPVPPointUI();
			this.SetRankTabUI();
			this.RefreshAsyncButton();
		}

		// Token: 0x06008663 RID: 34403 RVA: 0x002D7C30 File Offset: 0x002D5E30
		private void SetRankTabUI()
		{
			NKCUtil.SetGameobjectActive(this.m_objAsyncTarget, this.m_RANK_TYPE == RANK_TYPE.MY_LEAGUE);
			NKCUtil.SetGameobjectActive(this.m_objRankAll, this.m_RANK_TYPE == RANK_TYPE.ALL);
			NKCUtil.SetGameobjectActive(this.m_objRankFriend, this.m_RANK_TYPE == RANK_TYPE.FRIEND);
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetLatestRANK_TYPE(this.m_RANK_TYPE);
			this.RefreshRankTabCells();
			if (!this.m_bPlayIntro)
			{
				this.m_amtorRankCenter.Play("NKM_UI_GAUNTLET_LOBBY_CONTENT_INTRO_CENTER_FADEIN");
				return;
			}
			this.m_bPlayIntro = false;
		}

		// Token: 0x06008664 RID: 34404 RVA: 0x002D7CB4 File Offset: 0x002D5EB4
		private void SendPVPListREQ(RANK_TYPE type, bool all)
		{
			if (type != RANK_TYPE.MY_LEAGUE)
			{
				NKMPacket_ASYNC_PVP_RANK_LIST_REQ nkmpacket_ASYNC_PVP_RANK_LIST_REQ = new NKMPacket_ASYNC_PVP_RANK_LIST_REQ();
				nkmpacket_ASYNC_PVP_RANK_LIST_REQ.rankType = type;
				nkmpacket_ASYNC_PVP_RANK_LIST_REQ.isAll = all;
				NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_ASYNC_PVP_RANK_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
				return;
			}
			List<AsyncPvpTarget> asyncTargetList = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().AsyncTargetList;
			if (asyncTargetList.Count > 0)
			{
				this.SetTartgetList(asyncTargetList);
				this.RefreshAsyncButton();
				this.UpdateAsyncRefreshTime();
				this.RefreshRankTabCells();
				return;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = this.CheckCanPlayPVPGame();
			if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_END_WEEK || nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_END_SEASON)
			{
				this.SetTartgetList(asyncTargetList);
				this.RefreshAsyncLock();
				return;
			}
			NKCPacketSender.Send_NKMPacket_ASYNC_PVP_TARGET_LIST_REQ();
		}

		// Token: 0x06008665 RID: 34405 RVA: 0x002D7D49 File Offset: 0x002D5F49
		private void IfCanSendRankListREQByCurrRankType()
		{
			if (!this.m_arRankREQ[(int)this.m_RANK_TYPE])
			{
				this.SendPVPListREQ(this.m_RANK_TYPE, false);
				this.m_arRankREQ[(int)this.m_RANK_TYPE] = true;
			}
		}

		// Token: 0x06008666 RID: 34406 RVA: 0x002D7D75 File Offset: 0x002D5F75
		private void OnTabChangedToAsyncTarget(bool bSet)
		{
			if (bSet)
			{
				this.m_RANK_TYPE = RANK_TYPE.MY_LEAGUE;
			}
			this.IfCanSendRankListREQByCurrRankType();
			this.SetRankTabUI();
		}

		// Token: 0x06008667 RID: 34407 RVA: 0x002D7D8D File Offset: 0x002D5F8D
		private void OnTabChangedToAll(bool bSet)
		{
			if (bSet)
			{
				this.m_RANK_TYPE = RANK_TYPE.ALL;
			}
			this.IfCanSendRankListREQByCurrRankType();
			this.SetRankTabUI();
		}

		// Token: 0x06008668 RID: 34408 RVA: 0x002D7DA5 File Offset: 0x002D5FA5
		private void OnTabChangedToFriend(bool bSet)
		{
			if (bSet)
			{
				this.m_RANK_TYPE = RANK_TYPE.FRIEND;
			}
			this.IfCanSendRankListREQByCurrRankType();
			this.SetRankTabUI();
		}

		// Token: 0x06008669 RID: 34409 RVA: 0x002D7DC0 File Offset: 0x002D5FC0
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
			this.m_lvsrRankAll.ClearCells();
			this.m_lvsrRankFriend.ClearCells();
			this.m_lvsrAsyncTartget.ClearCells();
		}

		// Token: 0x0600866A RID: 34410 RVA: 0x002D7E1B File Offset: 0x002D601B
		public void Close()
		{
			if (this.m_NKCPopupGauntletLeagueGuide != null && this.m_NKCPopupGauntletLeagueGuide.IsOpen)
			{
				this.m_NKCPopupGauntletLeagueGuide.Close();
			}
			this.m_bFirstOpen = true;
			NKCPopupGauntletBanList.CheckInstanceAndClose();
		}

		// Token: 0x0600866B RID: 34411 RVA: 0x002D7E50 File Offset: 0x002D6050
		private void OnClickAsyncBattle(long friendCode)
		{
			if (this.CheckDefenseDeck() != NKM_ERROR_CODE.NEC_OK)
			{
				return;
			}
			if (this.CheckCanPlayPVPGame() != NKM_ERROR_CODE.NEC_OK)
			{
				return;
			}
			AsyncPvpTarget asyncPvpTarget = this.m_listAsyncTarget.Find((AsyncPvpTarget v) => v.userFriendCode == friendCode);
			if (asyncPvpTarget != null)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_ASYNC_READY().SetReserveData(asyncPvpTarget, NKM_GAME_TYPE.NGT_ASYNC_PVP);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_ASYNC_READY, true);
			}
		}

		// Token: 0x0600866C RID: 34412 RVA: 0x002D7EB6 File Offset: 0x002D60B6
		private void CloseDefenseDeck()
		{
			NKCUIDeckViewer.Instance.Close();
			this.RefreshAsyncLock();
		}

		// Token: 0x0600866D RID: 34413 RVA: 0x002D7EC8 File Offset: 0x002D60C8
		private NKM_ERROR_CODE CheckDefenseDeck()
		{
			NKMDeckIndex nkmdeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_PVP_DEFENCE, 0);
			return NKMMain.IsValidDeck(NKCScenManager.CurrentUserData().m_ArmyData, nkmdeckIndex.m_eDeckType, nkmdeckIndex.m_iIndex, NKM_GAME_TYPE.NGT_ASYNC_PVP);
		}

		// Token: 0x0600866E RID: 34414 RVA: 0x002D7EFC File Offset: 0x002D60FC
		public void RefreshAsyncLock()
		{
			bool bValue = false;
			NKM_ERROR_CODE nkm_ERROR_CODE = this.CheckCanPlayPVPGame();
			if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_END_SEASON || nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_END_WEEK)
			{
				bValue = true;
				NKCUtil.SetLabelText(this.m_txtAsyncLock, NKCUtilString.GET_STRING_GAUNTLET_ASYNC_LOCK_CLOSING);
			}
			else if (this.CheckDefenseDeck() != NKM_ERROR_CODE.NEC_OK)
			{
				bValue = true;
				NKCUtil.SetLabelText(this.m_txtAsyncLock, NKCUtilString.GET_STRING_GAUNTLET_ASYNC_LOCK_DEFENSE_DECK);
			}
			NKCUtil.SetGameobjectActive(this.m_objAsyncLock, bValue);
		}

		// Token: 0x0600866F RID: 34415 RVA: 0x002D7F5C File Offset: 0x002D615C
		private void OnClickAsyncRefresh()
		{
			if (!this.CheckAsyncRfreshTime())
			{
				return;
			}
			if (this.CheckDefenseDeck() != NKM_ERROR_CODE.NEC_OK)
			{
				return;
			}
			if (this.CheckCanPlayPVPGame() != NKM_ERROR_CODE.NEC_OK)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_ASYNC_PVP_TARGET_LIST_REQ();
		}

		// Token: 0x06008670 RID: 34416 RVA: 0x002D7F7E File Offset: 0x002D617E
		private bool CheckAsyncRfreshTime()
		{
			return this.m_fAsyncRefreshTimer <= 0f || this.m_fAsyncRefreshTimer + 60f < Time.time;
		}

		// Token: 0x06008671 RID: 34417 RVA: 0x002D7FA8 File Offset: 0x002D61A8
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

		// Token: 0x06008672 RID: 34418 RVA: 0x002D7FEC File Offset: 0x002D61EC
		private List<NKMUserSimpleProfileData> GetUserSimpleList(RANK_TYPE type)
		{
			List<NKMUserSimpleProfileData> result;
			if (this.m_dicUserSimpleData.TryGetValue(type, out result))
			{
				return result;
			}
			return new List<NKMUserSimpleProfileData>();
		}

		// Token: 0x06008673 RID: 34419 RVA: 0x002D8010 File Offset: 0x002D6210
		private NKMUserSimpleProfileData GetUserSimpleData(RANK_TYPE type, int index)
		{
			List<NKMUserSimpleProfileData> userSimpleList = this.GetUserSimpleList(type);
			if (userSimpleList.Count > index)
			{
				return userSimpleList[index];
			}
			return null;
		}

		// Token: 0x06008674 RID: 34420 RVA: 0x002D8037 File Offset: 0x002D6237
		private void AddUserSimpleList(RANK_TYPE type, List<NKMUserSimpleProfileData> list)
		{
			if (!this.m_dicUserSimpleData.ContainsKey(type))
			{
				this.m_dicUserSimpleData.Add(type, new List<NKMUserSimpleProfileData>());
			}
			this.m_dicUserSimpleData[type].Clear();
			this.m_dicUserSimpleData[type] = list;
		}

		// Token: 0x06008675 RID: 34421 RVA: 0x002D8076 File Offset: 0x002D6276
		private int GetCurrentSeasonID()
		{
			return NKCUtil.FindPVPSeasonIDForAsync(NKCSynchronizedTime.GetServerUTCTime(0.0));
		}

		// Token: 0x06008676 RID: 34422 RVA: 0x002D808C File Offset: 0x002D628C
		private NKM_ERROR_CODE CheckCanPlayPVPGame()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			int seasonID = NKCUtil.FindPVPSeasonIDForAsync(NKCSynchronizedTime.GetServerUTCTime(0.0));
			int weekIDForAsync = NKCPVPManager.GetWeekIDForAsync(NKCSynchronizedTime.GetServerUTCTime(0.0), seasonID);
			return NKCPVPManager.CanPlayPVPAsyncGame(myUserData, seasonID, weekIDForAsync, NKCSynchronizedTime.GetServerUTCTime(0.0));
		}

		// Token: 0x040072D4 RID: 29396
		public Animator m_amtorRankCenter;

		// Token: 0x040072D5 RID: 29397
		[Header("상단 탭")]
		public NKCUIComToggle m_ctglAsyncTab;

		// Token: 0x040072D6 RID: 29398
		public NKCUIComToggle m_ctglAllTab;

		// Token: 0x040072D7 RID: 29399
		public NKCUIComToggle m_ctglFriendTab;

		// Token: 0x040072D8 RID: 29400
		[Header("스크롤 관련")]
		public GameObject m_objAsyncTarget;

		// Token: 0x040072D9 RID: 29401
		public GameObject m_objRankAll;

		// Token: 0x040072DA RID: 29402
		public GameObject m_objRankFriend;

		// Token: 0x040072DB RID: 29403
		public LoopVerticalScrollRect m_lvsrAsyncTartget;

		// Token: 0x040072DC RID: 29404
		public LoopVerticalScrollRect m_lvsrRankAll;

		// Token: 0x040072DD RID: 29405
		public LoopVerticalScrollRect m_lvsrRankFriend;

		// Token: 0x040072DE RID: 29406
		public Transform m_trAsyncTartget;

		// Token: 0x040072DF RID: 29407
		public Transform m_trRankAll;

		// Token: 0x040072E0 RID: 29408
		public Transform m_trRankFriend;

		// Token: 0x040072E1 RID: 29409
		public GameObject m_objAsyncLock;

		// Token: 0x040072E2 RID: 29410
		public Text m_txtAsyncLock;

		// Token: 0x040072E3 RID: 29411
		public GameObject m_objEmptyList;

		// Token: 0x040072E4 RID: 29412
		public Text m_lbEmptyMessage;

		// Token: 0x040072E5 RID: 29413
		[Header("중앙 하단")]
		public Text m_lbRemainRankTime;

		// Token: 0x040072E6 RID: 29414
		public NKCUIComStateButton m_csbtnAsyncRefresh;

		// Token: 0x040072E7 RID: 29415
		public GameObject m_objAsyncRefreshOn;

		// Token: 0x040072E8 RID: 29416
		public GameObject m_objAsyncRefreshOff;

		// Token: 0x040072E9 RID: 29417
		public Text m_txtAsyncRefreshTimer;

		// Token: 0x040072EA RID: 29418
		[Header("V2 대응 처리")]
		public GameObject m_objNKM_UI_GAUNTLET_RANK_UPSIDEMENU;

		// Token: 0x040072EB RID: 29419
		public GameObject m_objNKM_UI_GAUNTLET_RANK_UPSIDEMENU_NEW;

		// Token: 0x040072EC RID: 29420
		public GameObject m_NKM_UI_GAUNTLET_ASYNC_LIST_SCROLL;

		// Token: 0x040072ED RID: 29421
		public GameObject m_NKM_UI_GAUNTLET_ASYNC_NPC_SCROLL;

		// Token: 0x040072EE RID: 29422
		public GameObject m_NKM_UI_GAUNTLET_ASYNC_REVENGE_SCROLL;

		// Token: 0x040072EF RID: 29423
		public GameObject m_NKM_UI_GAUNTLET_ASYNC_RANK_SCROLL;

		// Token: 0x040072F0 RID: 29424
		[Header("정보")]
		public NKCUIGauntletLobbyRightSideAsync m_RightSide;

		// Token: 0x040072F1 RID: 29425
		private NKCPopupGauntletLeagueGuide m_NKCPopupGauntletLeagueGuide;

		// Token: 0x040072F2 RID: 29426
		private RANK_TYPE m_RANK_TYPE;

		// Token: 0x040072F3 RID: 29427
		private bool m_bFirstOpen = true;

		// Token: 0x040072F4 RID: 29428
		private bool m_bPrepareLoopScrollCells;

		// Token: 0x040072F5 RID: 29429
		private bool[] m_arRankREQ = new bool[3];

		// Token: 0x040072F6 RID: 29430
		private bool[] m_arAllRankREQ = new bool[3];

		// Token: 0x040072F7 RID: 29431
		private bool[] m_arOpened = new bool[3];

		// Token: 0x040072F8 RID: 29432
		private Dictionary<RANK_TYPE, List<NKMUserSimpleProfileData>> m_dicUserSimpleData = new Dictionary<RANK_TYPE, List<NKMUserSimpleProfileData>>();

		// Token: 0x040072F9 RID: 29433
		private List<AsyncPvpTarget> m_listAsyncTarget = new List<AsyncPvpTarget>();

		// Token: 0x040072FA RID: 29434
		private float m_fPrevUpdateTime;

		// Token: 0x040072FB RID: 29435
		private float m_fAsyncRefreshTimer;

		// Token: 0x040072FC RID: 29436
		private bool m_bPlayIntro = true;

		// Token: 0x040072FD RID: 29437
		private const int ASYNC_REFRESH_TIMER = 60;
	}
}
