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
	// Token: 0x02000B78 RID: 2936
	public class NKCUIGauntletLobbyRank : MonoBehaviour
	{
		// Token: 0x06008715 RID: 34581 RVA: 0x002DB3D1 File Offset: 0x002D95D1
		public static void SetAlertDemotion(bool bSet)
		{
			NKCUIGauntletLobbyRank.m_bAlertDemotion = bSet;
		}

		// Token: 0x06008716 RID: 34582 RVA: 0x002DB3D9 File Offset: 0x002D95D9
		public RANK_TYPE GetCurrRankType()
		{
			return this.m_RANK_TYPE;
		}

		// Token: 0x06008717 RID: 34583 RVA: 0x002DB3E1 File Offset: 0x002D95E1
		public void SetCurrRankType(RANK_TYPE eRANK_TYPE)
		{
			this.m_RANK_TYPE = eRANK_TYPE;
		}

		// Token: 0x06008718 RID: 34584 RVA: 0x002DB3EC File Offset: 0x002D95EC
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
			if (this.m_RightSide != null)
			{
				this.m_RightSide.InitUI();
			}
		}

		// Token: 0x06008719 RID: 34585 RVA: 0x002DB5B0 File Offset: 0x002D97B0
		private void UpdateRemainTimeUI()
		{
			NKMPvpRankSeasonTemplet pvpRankSeasonTemplet = NKCPVPManager.GetPvpRankSeasonTemplet(NKCUtil.FindPVPSeasonIDForRank(NKCSynchronizedTime.GetServerUTCTime(0.0)));
			if (pvpRankSeasonTemplet != null)
			{
				if (!NKCPVPManager.IsRewardWeek(pvpRankSeasonTemplet, NKCPVPManager.WeekCalcStartDateUtc))
				{
					if (!pvpRankSeasonTemplet.CheckMySeason(NKCSynchronizedTime.GetServerUTCTime(0.0)))
					{
						this.m_lbRemainRankTime.text = NKCUtilString.GET_STRING_GAUNTLET_THIS_SEASON_LEAGUE_BEING_EVALUATED;
						return;
					}
					this.m_lbRemainRankTime.text = string.Format(NKCUtilString.GET_STRING_GAUNTLET_THIS_SEASON_LEAGUE_REMAIN_TIME_ONE_PARAM, NKCUtilString.GetRemainTimeStringEx(pvpRankSeasonTemplet.EndDate));
					return;
				}
				else
				{
					this.m_lbRemainRankTime.text = string.Format(NKCUtilString.GET_STRING_GAUNTLET_THIS_WEEK_LEAGUE_ONE_PARAM, NKCUtilString.GetRemainTimeStringForGauntletWeekly());
				}
			}
		}

		// Token: 0x0600871A RID: 34586 RVA: 0x002DB648 File Offset: 0x002D9848
		private void Update()
		{
			if (this.m_fPrevUpdateTime + 1f < Time.time)
			{
				this.m_fPrevUpdateTime = Time.time;
				this.UpdateRemainTimeUI();
				this.m_RightSide.UpdateReadyRankButtonUI();
				this.m_RightSide.UpdateRankPVPPointUI();
			}
		}

		// Token: 0x0600871B RID: 34587 RVA: 0x002DB684 File Offset: 0x002D9884
		private void TrySendAllRankUserListREQ(RANK_TYPE rt)
		{
			if (this.m_arAllRankREQ[(int)rt])
			{
				return;
			}
			this.m_arAllRankREQ[(int)rt] = true;
			this.SendPVPListREQ(rt, true);
		}

		// Token: 0x0600871C RID: 34588 RVA: 0x002DB6A2 File Offset: 0x002D98A2
		private void OnEventPanelBeginDragMyLeague()
		{
			this.TrySendAllRankUserListREQ(RANK_TYPE.MY_LEAGUE);
		}

		// Token: 0x0600871D RID: 34589 RVA: 0x002DB6AB File Offset: 0x002D98AB
		private void OnEventPanelBeginDragAll()
		{
			this.TrySendAllRankUserListREQ(RANK_TYPE.ALL);
		}

		// Token: 0x0600871E RID: 34590 RVA: 0x002DB6B4 File Offset: 0x002D98B4
		private void OnEventPanelBeginDragFriend()
		{
			this.TrySendAllRankUserListREQ(RANK_TYPE.FRIEND);
		}

		// Token: 0x0600871F RID: 34591 RVA: 0x002DB6BD File Offset: 0x002D98BD
		public RectTransform GetSlotMyLeague(int index)
		{
			NKCUIGauntletLRSlot newInstance = NKCUIGauntletLRSlot.GetNewInstance(this.m_trRankMyLeague, new NKCUIGauntletLRSlot.OnDragBegin(this.OnEventPanelBeginDragMyLeague));
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x06008720 RID: 34592 RVA: 0x002DB6E4 File Offset: 0x002D98E4
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

		// Token: 0x06008721 RID: 34593 RVA: 0x002DB720 File Offset: 0x002D9920
		public void ProvideDataMyLeague(Transform tr, int index)
		{
			NKCUIGauntletLRSlot component = tr.GetComponent<NKCUIGauntletLRSlot>();
			if (component != null)
			{
				component.SetUI(this.GetUserSimpleData(RANK_TYPE.MY_LEAGUE, index), index + 1, NKM_GAME_TYPE.NGT_PVP_RANK);
			}
		}

		// Token: 0x06008722 RID: 34594 RVA: 0x002DB74F File Offset: 0x002D994F
		public RectTransform GetSlotAll(int index)
		{
			NKCUIGauntletLRSlot newInstance = NKCUIGauntletLRSlot.GetNewInstance(this.m_trRankAll, new NKCUIGauntletLRSlot.OnDragBegin(this.OnEventPanelBeginDragAll));
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x06008723 RID: 34595 RVA: 0x002DB774 File Offset: 0x002D9974
		public void ProvideDataAll(Transform tr, int index)
		{
			NKCUIGauntletLRSlot component = tr.GetComponent<NKCUIGauntletLRSlot>();
			if (component != null)
			{
				component.SetUI(this.GetUserSimpleData(RANK_TYPE.ALL, index), index + 1, NKM_GAME_TYPE.NGT_PVP_RANK);
			}
		}

		// Token: 0x06008724 RID: 34596 RVA: 0x002DB7A3 File Offset: 0x002D99A3
		public RectTransform GetSlotFriend(int index)
		{
			NKCUIGauntletLRSlot newInstance = NKCUIGauntletLRSlot.GetNewInstance(this.m_trRankFriend, new NKCUIGauntletLRSlot.OnDragBegin(this.OnEventPanelBeginDragFriend));
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x06008725 RID: 34597 RVA: 0x002DB7C8 File Offset: 0x002D99C8
		public void ProvideDataFriend(Transform tr, int index)
		{
			NKCUIGauntletLRSlot component = tr.GetComponent<NKCUIGauntletLRSlot>();
			if (component != null)
			{
				component.SetUI(this.GetUserSimpleData(RANK_TYPE.FRIEND, index), index + 1, NKM_GAME_TYPE.NGT_PVP_RANK);
			}
		}

		// Token: 0x06008726 RID: 34598 RVA: 0x002DB7F8 File Offset: 0x002D99F8
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

		// Token: 0x06008727 RID: 34599 RVA: 0x002DB8C1 File Offset: 0x002D9AC1
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

		// Token: 0x06008728 RID: 34600 RVA: 0x002DB8F7 File Offset: 0x002D9AF7
		public void OnRecv(NKMPacket_PVP_CHARGE_POINT_REFRESH_ACK cNKMPacket_PVP_CHARGE_POINT_REFRESH_ACK)
		{
			this.m_RightSide.UpdateRankPVPPointUI();
		}

		// Token: 0x06008729 RID: 34601 RVA: 0x002DB904 File Offset: 0x002D9B04
		public void OnRecv(NKMPacket_PVP_RANK_WEEK_REWARD_ACK sPacket)
		{
			this.m_RightSide.UpdateNowSeasonPVPInfoUI(NKM_GAME_TYPE.NGT_PVP_RANK);
			this.SendPVPListREQ(this.m_RANK_TYPE, false);
		}

		// Token: 0x0600872A RID: 34602 RVA: 0x002DB91F File Offset: 0x002D9B1F
		public void OnRecv(NKMPacket_PVP_RANK_SEASON_REWARD_ACK cNKMPacket_PVP_RANK_SEASON_REWARD_ACK)
		{
			this.m_RightSide.UpdateNowSeasonPVPInfoUI(NKM_GAME_TYPE.NGT_PVP_RANK);
			this.SendPVPListREQ(this.m_RANK_TYPE, false);
		}

		// Token: 0x0600872B RID: 34603 RVA: 0x002DB93C File Offset: 0x002D9B3C
		public void OnRecv(NKMPacket_PVP_RANK_LIST_ACK cNKMPacket_PVP_RANK_LIST_ACK)
		{
			this.AddUserSimpleList(cNKMPacket_PVP_RANK_LIST_ACK.rankType, cNKMPacket_PVP_RANK_LIST_ACK.userProfileDataList);
			if (cNKMPacket_PVP_RANK_LIST_ACK.rankType == RANK_TYPE.FRIEND)
			{
				this.GetUserSimpleList(cNKMPacket_PVP_RANK_LIST_ACK.rankType).Sort((NKMUserSimpleProfileData a, NKMUserSimpleProfileData b) => b.pvpScore.CompareTo(a.pvpScore));
			}
			if (cNKMPacket_PVP_RANK_LIST_ACK.userProfileDataList.Count < NKMPvpCommonConst.Instance.RANK_SIMPLE_COUNT)
			{
				this.m_arAllRankREQ[(int)cNKMPacket_PVP_RANK_LIST_ACK.rankType] = true;
			}
			if (this.m_RANK_TYPE == cNKMPacket_PVP_RANK_LIST_ACK.rankType)
			{
				this.RefreshRankTabCells();
			}
		}

		// Token: 0x0600872C RID: 34604 RVA: 0x002DB9CD File Offset: 0x002D9BCD
		public void OnRecv(NKMPacket_PVP_CASTING_VOTE_UNIT_ACK sPacket)
		{
			this.m_RightSide.UpdateCastingBanVoteState();
		}

		// Token: 0x0600872D RID: 34605 RVA: 0x002DB9DA File Offset: 0x002D9BDA
		public void OnRecv(NKMPacket_PVP_CASTING_VOTE_SHIP_ACK sPacket)
		{
			this.m_RightSide.UpdateCastingBanVoteState();
		}

		// Token: 0x0600872E RID: 34606 RVA: 0x002DB9E7 File Offset: 0x002D9BE7
		public void OnRecv(NKMPacket_PVP_CASTING_VOTE_OPERATOR_ACK sPacket)
		{
			this.m_RightSide.UpdateCastingBanVoteState();
		}

		// Token: 0x0600872F RID: 34607 RVA: 0x002DB9F4 File Offset: 0x002D9BF4
		public void SetUI()
		{
			this.m_bPlayIntro = true;
			if (NKCUIGauntletLobbyRank.m_bAlertDemotion)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_GAUNTLET_DEMOTE_WARNING, null, "");
				NKCUIGauntletLobbyRank.m_bAlertDemotion = false;
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
			this.m_RightSide.UpdateNowSeasonPVPInfoUI(NKM_GAME_TYPE.NGT_PVP_RANK);
			this.m_RightSide.UpdateBattleCondition();
			this.m_RightSide.UpdateCastingBanVoteState();
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
			this.m_RightSide.UpdateReadyRankButtonUI();
			this.m_RightSide.UpdateRankPVPPointUI();
			this.SetRankTabUI();
			this.TutorialCheck();
		}

		// Token: 0x06008730 RID: 34608 RVA: 0x002DBB9C File Offset: 0x002D9D9C
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

		// Token: 0x06008731 RID: 34609 RVA: 0x002DBC20 File Offset: 0x002D9E20
		private void SendPVPListREQ(RANK_TYPE type, bool all)
		{
			NKMPacket_PVP_RANK_LIST_REQ nkmpacket_PVP_RANK_LIST_REQ = new NKMPacket_PVP_RANK_LIST_REQ();
			nkmpacket_PVP_RANK_LIST_REQ.rankType = this.m_RANK_TYPE;
			nkmpacket_PVP_RANK_LIST_REQ.isAll = all;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_PVP_RANK_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06008732 RID: 34610 RVA: 0x002DBC59 File Offset: 0x002D9E59
		private void IfCanSendRankListREQByCurrRankType()
		{
			if (!this.m_arRankREQ[(int)this.m_RANK_TYPE])
			{
				this.SendPVPListREQ(this.m_RANK_TYPE, false);
				this.m_arRankREQ[(int)this.m_RANK_TYPE] = true;
			}
		}

		// Token: 0x06008733 RID: 34611 RVA: 0x002DBC85 File Offset: 0x002D9E85
		private void OnRankTabChangedToMyLeague(bool bSet)
		{
			if (bSet)
			{
				this.m_RANK_TYPE = RANK_TYPE.MY_LEAGUE;
			}
			this.IfCanSendRankListREQByCurrRankType();
			this.SetRankTabUI();
		}

		// Token: 0x06008734 RID: 34612 RVA: 0x002DBC9D File Offset: 0x002D9E9D
		private void OnRankTabChangedToAll(bool bSet)
		{
			if (bSet)
			{
				this.m_RANK_TYPE = RANK_TYPE.ALL;
			}
			this.IfCanSendRankListREQByCurrRankType();
			this.SetRankTabUI();
		}

		// Token: 0x06008735 RID: 34613 RVA: 0x002DBCB5 File Offset: 0x002D9EB5
		private void OnRankTabChangedToFriend(bool bSet)
		{
			if (bSet)
			{
				this.m_RANK_TYPE = RANK_TYPE.FRIEND;
			}
			this.IfCanSendRankListREQByCurrRankType();
			this.SetRankTabUI();
		}

		// Token: 0x06008736 RID: 34614 RVA: 0x002DBCD0 File Offset: 0x002D9ED0
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

		// Token: 0x06008737 RID: 34615 RVA: 0x002DBD2B File Offset: 0x002D9F2B
		public void Close()
		{
			if (this.m_NKCPopupGauntletLeagueGuide != null && this.m_NKCPopupGauntletLeagueGuide.IsOpen)
			{
				this.m_NKCPopupGauntletLeagueGuide.Close();
			}
			this.m_bFirstOpen = true;
			NKCPopupGauntletBanList.CheckInstanceAndClose();
		}

		// Token: 0x06008738 RID: 34616 RVA: 0x002DBD60 File Offset: 0x002D9F60
		private List<NKMUserSimpleProfileData> GetUserSimpleList(RANK_TYPE type)
		{
			List<NKMUserSimpleProfileData> result;
			if (this.m_dicUserSimpleData.TryGetValue(type, out result))
			{
				return result;
			}
			return new List<NKMUserSimpleProfileData>();
		}

		// Token: 0x06008739 RID: 34617 RVA: 0x002DBD84 File Offset: 0x002D9F84
		private NKMUserSimpleProfileData GetUserSimpleData(RANK_TYPE type, int index)
		{
			List<NKMUserSimpleProfileData> userSimpleList = this.GetUserSimpleList(type);
			if (userSimpleList.Count > index)
			{
				return userSimpleList[index];
			}
			return null;
		}

		// Token: 0x0600873A RID: 34618 RVA: 0x002DBDAB File Offset: 0x002D9FAB
		private void AddUserSimpleList(RANK_TYPE type, List<NKMUserSimpleProfileData> list)
		{
			if (!this.m_dicUserSimpleData.ContainsKey(type))
			{
				this.m_dicUserSimpleData.Add(type, new List<NKMUserSimpleProfileData>());
			}
			this.m_dicUserSimpleData[type].Clear();
			this.m_dicUserSimpleData[type] = list;
		}

		// Token: 0x0600873B RID: 34619 RVA: 0x002DBDEA File Offset: 0x002D9FEA
		private void TutorialCheck()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.GauntletLobbyRank, true);
		}

		// Token: 0x04007387 RID: 29575
		public Animator m_amtorRankCenter;

		// Token: 0x04007388 RID: 29576
		[Header("상단 탭")]
		public NKCUIComToggle m_ctglRankMyLeagueTab;

		// Token: 0x04007389 RID: 29577
		public NKCUIComToggle m_ctglRankAllTab;

		// Token: 0x0400738A RID: 29578
		public NKCUIComToggle m_ctglRankFriendTab;

		// Token: 0x0400738B RID: 29579
		[Header("스크롤 관련")]
		public GameObject m_objRankMyLeague;

		// Token: 0x0400738C RID: 29580
		public GameObject m_objRankAll;

		// Token: 0x0400738D RID: 29581
		public GameObject m_objRankFriend;

		// Token: 0x0400738E RID: 29582
		public LoopVerticalScrollRect m_lvsrRankMyLeague;

		// Token: 0x0400738F RID: 29583
		public LoopVerticalScrollRect m_lvsrRankAll;

		// Token: 0x04007390 RID: 29584
		public LoopVerticalScrollRect m_lvsrRankFriend;

		// Token: 0x04007391 RID: 29585
		public Transform m_trRankMyLeague;

		// Token: 0x04007392 RID: 29586
		public Transform m_trRankAll;

		// Token: 0x04007393 RID: 29587
		public Transform m_trRankFriend;

		// Token: 0x04007394 RID: 29588
		public GameObject m_objEmptyList;

		// Token: 0x04007395 RID: 29589
		public Text m_lbEmptyMessage;

		// Token: 0x04007396 RID: 29590
		[Header("남은 시간")]
		public Text m_lbRemainRankTime;

		// Token: 0x04007397 RID: 29591
		[Header("정보")]
		public NKCUIGauntletLobbyRightSideRank m_RightSide;

		// Token: 0x04007398 RID: 29592
		private NKCPopupGauntletLeagueGuide m_NKCPopupGauntletLeagueGuide;

		// Token: 0x04007399 RID: 29593
		private RANK_TYPE m_RANK_TYPE;

		// Token: 0x0400739A RID: 29594
		private bool m_bFirstOpen = true;

		// Token: 0x0400739B RID: 29595
		private bool m_bPrepareLoopScrollCells;

		// Token: 0x0400739C RID: 29596
		private bool[] m_arRankREQ = new bool[3];

		// Token: 0x0400739D RID: 29597
		private bool[] m_arAllRankREQ = new bool[3];

		// Token: 0x0400739E RID: 29598
		private bool[] m_arOpened = new bool[3];

		// Token: 0x0400739F RID: 29599
		private Dictionary<RANK_TYPE, List<NKMUserSimpleProfileData>> m_dicUserSimpleData = new Dictionary<RANK_TYPE, List<NKMUserSimpleProfileData>>();

		// Token: 0x040073A0 RID: 29600
		private float m_fPrevUpdateTime;

		// Token: 0x040073A1 RID: 29601
		private static bool m_bAlertDemotion;

		// Token: 0x040073A2 RID: 29602
		private bool m_bPlayIntro = true;
	}
}
