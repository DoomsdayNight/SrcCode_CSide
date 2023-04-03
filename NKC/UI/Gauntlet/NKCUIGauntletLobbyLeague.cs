using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.LeaderBoard;
using ClientPacket.Pvp;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B76 RID: 2934
	public class NKCUIGauntletLobbyLeague : MonoBehaviour
	{
		// Token: 0x060086EA RID: 34538 RVA: 0x002DA8F5 File Offset: 0x002D8AF5
		public static void SetAlertDemotion(bool bSet)
		{
			NKCUIGauntletLobbyLeague.m_bAlertDemotion = bSet;
		}

		// Token: 0x060086EB RID: 34539 RVA: 0x002DA8FD File Offset: 0x002D8AFD
		public RANK_TYPE GetCurrRankType()
		{
			return this.m_RANK_TYPE;
		}

		// Token: 0x060086EC RID: 34540 RVA: 0x002DA905 File Offset: 0x002D8B05
		public void SetCurrRankType(RANK_TYPE eRANK_TYPE)
		{
			this.m_RANK_TYPE = eRANK_TYPE;
		}

		// Token: 0x060086ED RID: 34541 RVA: 0x002DA910 File Offset: 0x002D8B10
		public void Init()
		{
			this.m_ctglRankMyLeagueTab.OnValueChanged.RemoveAllListeners();
			this.m_ctglRankMyLeagueTab.OnValueChanged.AddListener(new UnityAction<bool>(this.OnRankTabChangedToMyLeague));
			this.m_ctglRankAllTab.OnValueChanged.RemoveAllListeners();
			this.m_ctglRankAllTab.OnValueChanged.AddListener(new UnityAction<bool>(this.OnRankTabChangedToAll));
			this.m_ctglRankFriendTab.OnValueChanged.RemoveAllListeners();
			this.m_ctglRankFriendTab.OnValueChanged.AddListener(new UnityAction<bool>(this.OnRankTabChangedToFriend));
			this.m_lvsrRankMyLeague.dOnGetObject += this.GetSlotMyLeague;
			this.m_lvsrRankMyLeague.dOnReturnObject += this.ReturnSlot;
			this.m_lvsrRankMyLeague.dOnProvideData += this.ProvideDataMyLeague;
			this.m_lvsrRankMyLeague.ContentConstraintCount = 1;
			NKCUtil.SetScrollHotKey(this.m_lvsrRankMyLeague, null);
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
		}

		// Token: 0x060086EE RID: 34542 RVA: 0x002DAAC4 File Offset: 0x002D8CC4
		private void UpdateRemainTimeUI()
		{
			NKMLeaguePvpRankSeasonTemplet nkmleaguePvpRankSeasonTemplet = NKMLeaguePvpRankSeasonTemplet.Find(NKCUtil.FindPVPSeasonIDForLeague(NKCSynchronizedTime.GetServerUTCTime(0.0)));
			if (nkmleaguePvpRankSeasonTemplet != null)
			{
				if (!nkmleaguePvpRankSeasonTemplet.CheckMySeason(NKCSynchronizedTime.GetServerUTCTime(0.0)))
				{
					this.m_lbRemainRankTime.text = NKCUtilString.GET_STRING_GAUNTLET_THIS_SEASON_LEAGUE_BEING_EVALUATED;
					return;
				}
				this.m_lbRemainRankTime.text = string.Format(NKCUtilString.GET_STRING_GAUNTLET_THIS_SEASON_LEAGUE_REMAIN_TIME_ONE_PARAM, NKCUtilString.GetRemainTimeStringEx(nkmleaguePvpRankSeasonTemplet.EndDateUTC));
			}
		}

		// Token: 0x060086EF RID: 34543 RVA: 0x002DAB34 File Offset: 0x002D8D34
		private void Update()
		{
			if (this.m_fPrevUpdateTime + 1f < Time.time)
			{
				this.m_fPrevUpdateTime = Time.time;
				this.UpdateRemainTimeUI();
				this.m_RightSide.UpdateReadyButtonUI();
				this.m_RightSide.UpdatePVPPointUI();
			}
		}

		// Token: 0x060086F0 RID: 34544 RVA: 0x002DAB70 File Offset: 0x002D8D70
		private void TrySendAllRankUserListREQ(RANK_TYPE rt)
		{
			if (this.m_arAllRankREQ[(int)rt])
			{
				return;
			}
			this.m_arAllRankREQ[(int)rt] = true;
			this.SendPVPListREQ(rt, true);
		}

		// Token: 0x060086F1 RID: 34545 RVA: 0x002DAB8E File Offset: 0x002D8D8E
		private void OnEventPanelBeginDragMyLeague()
		{
			this.TrySendAllRankUserListREQ(RANK_TYPE.MY_LEAGUE);
		}

		// Token: 0x060086F2 RID: 34546 RVA: 0x002DAB97 File Offset: 0x002D8D97
		private void OnEventPanelBeginDragAll()
		{
			this.TrySendAllRankUserListREQ(RANK_TYPE.ALL);
		}

		// Token: 0x060086F3 RID: 34547 RVA: 0x002DABA0 File Offset: 0x002D8DA0
		private void OnEventPanelBeginDragFriend()
		{
			this.TrySendAllRankUserListREQ(RANK_TYPE.FRIEND);
		}

		// Token: 0x060086F4 RID: 34548 RVA: 0x002DABA9 File Offset: 0x002D8DA9
		public RectTransform GetSlotMyLeague(int index)
		{
			NKCUIGauntletLRSlot newInstance = NKCUIGauntletLRSlot.GetNewInstance(this.m_trRankMyLeague, new NKCUIGauntletLRSlot.OnDragBegin(this.OnEventPanelBeginDragMyLeague));
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x060086F5 RID: 34549 RVA: 0x002DABD0 File Offset: 0x002D8DD0
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

		// Token: 0x060086F6 RID: 34550 RVA: 0x002DAC0C File Offset: 0x002D8E0C
		public void ProvideDataMyLeague(Transform tr, int index)
		{
			NKCUIGauntletLRSlot component = tr.GetComponent<NKCUIGauntletLRSlot>();
			if (component != null)
			{
				component.SetUI(this.GetUserSimpleData(RANK_TYPE.MY_LEAGUE, index), index + 1, NKM_GAME_TYPE.NGT_PVP_LEAGUE);
			}
		}

		// Token: 0x060086F7 RID: 34551 RVA: 0x002DAC3C File Offset: 0x002D8E3C
		public RectTransform GetSlotAll(int index)
		{
			NKCUIGauntletLRSlot newInstance = NKCUIGauntletLRSlot.GetNewInstance(this.m_trRankAll, new NKCUIGauntletLRSlot.OnDragBegin(this.OnEventPanelBeginDragAll));
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x060086F8 RID: 34552 RVA: 0x002DAC60 File Offset: 0x002D8E60
		public void ProvideDataAll(Transform tr, int index)
		{
			NKCUIGauntletLRSlot component = tr.GetComponent<NKCUIGauntletLRSlot>();
			if (component != null)
			{
				component.SetUI(this.GetUserSimpleData(RANK_TYPE.ALL, index), index + 1, NKM_GAME_TYPE.NGT_PVP_LEAGUE);
			}
		}

		// Token: 0x060086F9 RID: 34553 RVA: 0x002DAC90 File Offset: 0x002D8E90
		public RectTransform GetSlotFriend(int index)
		{
			NKCUIGauntletLRSlot newInstance = NKCUIGauntletLRSlot.GetNewInstance(this.m_trRankFriend, new NKCUIGauntletLRSlot.OnDragBegin(this.OnEventPanelBeginDragFriend));
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x060086FA RID: 34554 RVA: 0x002DACB4 File Offset: 0x002D8EB4
		public void ProvideDataFriend(Transform tr, int index)
		{
			NKCUIGauntletLRSlot component = tr.GetComponent<NKCUIGauntletLRSlot>();
			if (component != null)
			{
				component.SetUI(this.GetUserSimpleData(RANK_TYPE.FRIEND, index), index + 1, NKM_GAME_TYPE.NGT_PVP_LEAGUE);
			}
		}

		// Token: 0x060086FB RID: 34555 RVA: 0x002DACE4 File Offset: 0x002D8EE4
		private void RefreshRankTabCells()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			if (this.m_RANK_TYPE == RANK_TYPE.MY_LEAGUE)
			{
				this.RefreshScrollRect(this.m_lvsrRankMyLeague, this.m_RANK_TYPE, this.GetUserSimpleList(this.m_RANK_TYPE).Count);
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

		// Token: 0x060086FC RID: 34556 RVA: 0x002DADAD File Offset: 0x002D8FAD
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

		// Token: 0x060086FD RID: 34557 RVA: 0x002DADE3 File Offset: 0x002D8FE3
		public void OnRecv(NKMPacket_PVP_CHARGE_POINT_REFRESH_ACK cNKMPacket_PVP_CHARGE_POINT_REFRESH_ACK)
		{
			this.m_RightSide.UpdatePVPPointUI();
		}

		// Token: 0x060086FE RID: 34558 RVA: 0x002DADF0 File Offset: 0x002D8FF0
		public void OnRecv(NKMPacket_LEAGUE_PVP_WEEKLY_REWARD_ACK sPacket)
		{
			this.m_RightSide.UpdateNowSeasonPVPInfoUI(NKM_GAME_TYPE.NGT_PVP_LEAGUE);
			this.SendPVPListREQ(this.m_RANK_TYPE, false);
		}

		// Token: 0x060086FF RID: 34559 RVA: 0x002DAE0C File Offset: 0x002D900C
		public void OnRecv(NKMPacket_LEAGUE_PVP_SEASON_REWARD_ACK sPacket)
		{
			this.m_RightSide.UpdateNowSeasonPVPInfoUI(NKM_GAME_TYPE.NGT_PVP_LEAGUE);
			this.SendPVPListREQ(this.m_RANK_TYPE, false);
		}

		// Token: 0x06008700 RID: 34560 RVA: 0x002DAE28 File Offset: 0x002D9028
		public void OnRecv(NKMPacket_LEAGUE_PVP_RANK_LIST_ACK cNKMPacket_LEAGUE_PVP_RANK_LIST_ACK)
		{
			this.AddUserSimpleList(cNKMPacket_LEAGUE_PVP_RANK_LIST_ACK.rankType, cNKMPacket_LEAGUE_PVP_RANK_LIST_ACK.list);
			if (cNKMPacket_LEAGUE_PVP_RANK_LIST_ACK.rankType == RANK_TYPE.FRIEND)
			{
				this.GetUserSimpleList(cNKMPacket_LEAGUE_PVP_RANK_LIST_ACK.rankType).Sort((NKMUserSimpleProfileData a, NKMUserSimpleProfileData b) => b.pvpScore.CompareTo(a.pvpScore));
			}
			if (cNKMPacket_LEAGUE_PVP_RANK_LIST_ACK.list.Count < NKMPvpCommonConst.Instance.RANK_SIMPLE_COUNT)
			{
				this.m_arAllRankREQ[(int)cNKMPacket_LEAGUE_PVP_RANK_LIST_ACK.rankType] = true;
			}
			if (this.m_RANK_TYPE == cNKMPacket_LEAGUE_PVP_RANK_LIST_ACK.rankType)
			{
				this.RefreshRankTabCells();
			}
		}

		// Token: 0x06008701 RID: 34561 RVA: 0x002DAEBC File Offset: 0x002D90BC
		public void SetUI()
		{
			this.m_bPlayIntro = true;
			if (NKCUIGauntletLobbyLeague.m_bAlertDemotion)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_GAUNTLET_DEMOTE_WARNING, null, "");
				NKCUIGauntletLobbyLeague.m_bAlertDemotion = false;
			}
			if (!this.m_bPrepareLoopScrollCells)
			{
				NKCUtil.SetGameobjectActive(this.m_objRankMyLeague, true);
				NKCUtil.SetGameobjectActive(this.m_objRankAll, true);
				NKCUtil.SetGameobjectActive(this.m_objRankFriend, true);
				this.m_lvsrRankMyLeague.PrepareCells(0);
				this.m_lvsrRankAll.PrepareCells(0);
				this.m_lvsrRankFriend.PrepareCells(0);
				this.m_bPrepareLoopScrollCells = true;
			}
			this.m_RightSide.UpdateNowSeasonPVPInfoUI(NKM_GAME_TYPE.NGT_PVP_LEAGUE);
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
				this.m_ctglRankMyLeagueTab.Select(false, true, false);
				this.m_ctglRankMyLeagueTab.Select(true, false, false);
			}
			else if (this.m_RANK_TYPE == RANK_TYPE.ALL)
			{
				this.m_ctglRankAllTab.Select(false, true, false);
				this.m_ctglRankAllTab.Select(true, false, false);
			}
			else if (this.m_RANK_TYPE == RANK_TYPE.FRIEND)
			{
				this.m_ctglRankFriendTab.Select(false, true, false);
				this.m_ctglRankFriendTab.Select(true, false, false);
			}
			else
			{
				this.m_ctglRankMyLeagueTab.Select(false, true, false);
				this.m_ctglRankMyLeagueTab.Select(true, false, false);
			}
			this.UpdateRemainTimeUI();
			this.m_RightSide.UpdateReadyButtonUI();
			this.m_RightSide.UpdatePVPPointUI();
			this.m_RightSide.UpdateBattleCondition();
			this.SetRankTabUI();
			this.TutorialCheck();
		}

		// Token: 0x06008702 RID: 34562 RVA: 0x002DB058 File Offset: 0x002D9258
		private void SetRankTabUI()
		{
			NKCUtil.SetGameobjectActive(this.m_objRankMyLeague, this.m_RANK_TYPE == RANK_TYPE.MY_LEAGUE);
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

		// Token: 0x06008703 RID: 34563 RVA: 0x002DB0DC File Offset: 0x002D92DC
		private void SendPVPListREQ(RANK_TYPE type, bool all)
		{
			NKMPacket_LEAGUE_PVP_RANK_LIST_REQ nkmpacket_LEAGUE_PVP_RANK_LIST_REQ = new NKMPacket_LEAGUE_PVP_RANK_LIST_REQ();
			nkmpacket_LEAGUE_PVP_RANK_LIST_REQ.rankType = this.m_RANK_TYPE;
			nkmpacket_LEAGUE_PVP_RANK_LIST_REQ.range = (all ? LeaderBoardRangeType.ALL : LeaderBoardRangeType.TOP10);
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_LEAGUE_PVP_RANK_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06008704 RID: 34564 RVA: 0x002DB11B File Offset: 0x002D931B
		private void IfCanSendRankListREQByCurrRankType()
		{
			if (!this.m_arRankREQ[(int)this.m_RANK_TYPE])
			{
				this.SendPVPListREQ(this.m_RANK_TYPE, false);
				this.m_arRankREQ[(int)this.m_RANK_TYPE] = true;
			}
		}

		// Token: 0x06008705 RID: 34565 RVA: 0x002DB147 File Offset: 0x002D9347
		private void OnRankTabChangedToMyLeague(bool bSet)
		{
			if (bSet)
			{
				this.m_RANK_TYPE = RANK_TYPE.MY_LEAGUE;
			}
			this.IfCanSendRankListREQByCurrRankType();
			this.SetRankTabUI();
		}

		// Token: 0x06008706 RID: 34566 RVA: 0x002DB15F File Offset: 0x002D935F
		private void OnRankTabChangedToAll(bool bSet)
		{
			if (bSet)
			{
				this.m_RANK_TYPE = RANK_TYPE.ALL;
			}
			this.IfCanSendRankListREQByCurrRankType();
			this.SetRankTabUI();
		}

		// Token: 0x06008707 RID: 34567 RVA: 0x002DB177 File Offset: 0x002D9377
		private void OnRankTabChangedToFriend(bool bSet)
		{
			if (bSet)
			{
				this.m_RANK_TYPE = RANK_TYPE.FRIEND;
			}
			this.IfCanSendRankListREQByCurrRankType();
			this.SetRankTabUI();
		}

		// Token: 0x06008708 RID: 34568 RVA: 0x002DB190 File Offset: 0x002D9390
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
			this.m_lvsrRankMyLeague.ClearCells();
			this.m_lvsrRankFriend.ClearCells();
		}

		// Token: 0x06008709 RID: 34569 RVA: 0x002DB1EB File Offset: 0x002D93EB
		public void Close()
		{
			if (this.m_NKCPopupGauntletLeagueGuide != null && this.m_NKCPopupGauntletLeagueGuide.IsOpen)
			{
				this.m_NKCPopupGauntletLeagueGuide.Close();
			}
			this.m_bFirstOpen = true;
			NKCPopupGauntletBanList.CheckInstanceAndClose();
		}

		// Token: 0x0600870A RID: 34570 RVA: 0x002DB220 File Offset: 0x002D9420
		private List<NKMUserSimpleProfileData> GetUserSimpleList(RANK_TYPE type)
		{
			List<NKMUserSimpleProfileData> result;
			if (this.m_dicUserSimpleData.TryGetValue(type, out result))
			{
				return result;
			}
			return new List<NKMUserSimpleProfileData>();
		}

		// Token: 0x0600870B RID: 34571 RVA: 0x002DB244 File Offset: 0x002D9444
		private NKMUserSimpleProfileData GetUserSimpleData(RANK_TYPE type, int index)
		{
			List<NKMUserSimpleProfileData> userSimpleList = this.GetUserSimpleList(type);
			if (userSimpleList.Count > index)
			{
				return userSimpleList[index];
			}
			return null;
		}

		// Token: 0x0600870C RID: 34572 RVA: 0x002DB26B File Offset: 0x002D946B
		private void AddUserSimpleList(RANK_TYPE type, List<NKMUserSimpleProfileData> list)
		{
			if (!this.m_dicUserSimpleData.ContainsKey(type))
			{
				this.m_dicUserSimpleData.Add(type, new List<NKMUserSimpleProfileData>());
			}
			this.m_dicUserSimpleData[type].Clear();
			this.m_dicUserSimpleData[type] = list;
		}

		// Token: 0x0600870D RID: 34573 RVA: 0x002DB2AA File Offset: 0x002D94AA
		private void TutorialCheck()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.GauntletLobbyLeague, true);
		}

		// Token: 0x04007367 RID: 29543
		public Animator m_amtorRankCenter;

		// Token: 0x04007368 RID: 29544
		[Header("상단 탭")]
		public NKCUIComToggle m_ctglRankMyLeagueTab;

		// Token: 0x04007369 RID: 29545
		public NKCUIComToggle m_ctglRankAllTab;

		// Token: 0x0400736A RID: 29546
		public NKCUIComToggle m_ctglRankFriendTab;

		// Token: 0x0400736B RID: 29547
		[Header("스크롤 관련")]
		public GameObject m_objRankMyLeague;

		// Token: 0x0400736C RID: 29548
		public GameObject m_objRankAll;

		// Token: 0x0400736D RID: 29549
		public GameObject m_objRankFriend;

		// Token: 0x0400736E RID: 29550
		public LoopVerticalScrollRect m_lvsrRankMyLeague;

		// Token: 0x0400736F RID: 29551
		public LoopVerticalScrollRect m_lvsrRankAll;

		// Token: 0x04007370 RID: 29552
		public LoopVerticalScrollRect m_lvsrRankFriend;

		// Token: 0x04007371 RID: 29553
		public Transform m_trRankMyLeague;

		// Token: 0x04007372 RID: 29554
		public Transform m_trRankAll;

		// Token: 0x04007373 RID: 29555
		public Transform m_trRankFriend;

		// Token: 0x04007374 RID: 29556
		public GameObject m_objEmptyList;

		// Token: 0x04007375 RID: 29557
		public Text m_lbEmptyMessage;

		// Token: 0x04007376 RID: 29558
		[Header("남은 시간")]
		public Text m_lbRemainRankTime;

		// Token: 0x04007377 RID: 29559
		[Header("우측 정보")]
		public NKCUIGauntletLobbyRightSideLeague m_RightSide;

		// Token: 0x04007378 RID: 29560
		private NKCPopupGauntletLeagueGuide m_NKCPopupGauntletLeagueGuide;

		// Token: 0x04007379 RID: 29561
		private RANK_TYPE m_RANK_TYPE;

		// Token: 0x0400737A RID: 29562
		private bool m_bFirstOpen = true;

		// Token: 0x0400737B RID: 29563
		private bool m_bPrepareLoopScrollCells;

		// Token: 0x0400737C RID: 29564
		private bool[] m_arRankREQ = new bool[3];

		// Token: 0x0400737D RID: 29565
		private bool[] m_arAllRankREQ = new bool[3];

		// Token: 0x0400737E RID: 29566
		private bool[] m_arOpened = new bool[3];

		// Token: 0x0400737F RID: 29567
		private Dictionary<RANK_TYPE, List<NKMUserSimpleProfileData>> m_dicUserSimpleData = new Dictionary<RANK_TYPE, List<NKMUserSimpleProfileData>>();

		// Token: 0x04007380 RID: 29568
		private float m_fPrevUpdateTime;

		// Token: 0x04007381 RID: 29569
		private static bool m_bAlertDemotion;

		// Token: 0x04007382 RID: 29570
		private bool m_bPlayIntro = true;
	}
}
