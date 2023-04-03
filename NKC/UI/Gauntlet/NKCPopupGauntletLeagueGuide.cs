using System;
using System.Collections.Generic;
using System.Linq;
using Cs.Core.Util;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B64 RID: 2916
	public class NKCPopupGauntletLeagueGuide : NKCUIBase
	{
		// Token: 0x06008522 RID: 34082 RVA: 0x002D0098 File Offset: 0x002CE298
		public static NKCPopupGauntletLeagueGuide OpenInstance()
		{
			NKCPopupGauntletLeagueGuide instance = NKCUIManager.OpenNewInstance<NKCPopupGauntletLeagueGuide>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_POPUP_LEAGUE_GUIDE", NKCUIManager.eUIBaseRect.UIFrontCommon, null).GetInstance<NKCPopupGauntletLeagueGuide>();
			instance.InitUI();
			return instance;
		}

		// Token: 0x06008523 RID: 34083 RVA: 0x002D00B6 File Offset: 0x002CE2B6
		public override void OnCloseInstance()
		{
			if (this.m_lvsrSeason != null)
			{
				this.m_lvsrSeason.ClearCells();
			}
			if (this.m_lvsrWeekly != null)
			{
				this.m_lvsrWeekly.ClearCells();
			}
		}

		// Token: 0x170015A2 RID: 5538
		// (get) Token: 0x06008524 RID: 34084 RVA: 0x002D00EA File Offset: 0x002CE2EA
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170015A3 RID: 5539
		// (get) Token: 0x06008525 RID: 34085 RVA: 0x002D00ED File Offset: 0x002CE2ED
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x170015A4 RID: 5540
		// (get) Token: 0x06008526 RID: 34086 RVA: 0x002D00F0 File Offset: 0x002CE2F0
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return new List<int>
				{
					5,
					101
				};
			}
		}

		// Token: 0x170015A5 RID: 5541
		// (get) Token: 0x06008527 RID: 34087 RVA: 0x002D0106 File Offset: 0x002CE306
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_GAUNTLET_LEAUGE_GUIDE;
			}
		}

		// Token: 0x06008528 RID: 34088 RVA: 0x002D0110 File Offset: 0x002CE310
		public void InitUI()
		{
			this.m_ctglSeason.OnValueChanged.RemoveAllListeners();
			this.m_ctglSeason.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedSeason));
			this.m_ctglWeek.OnValueChanged.RemoveAllListeners();
			this.m_ctglWeek.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedWeek));
			this.m_ctglHelp.OnValueChanged.RemoveAllListeners();
			this.m_ctglHelp.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedHelp));
			this.m_csbtnClose.PointerClick.RemoveAllListeners();
			this.m_csbtnClose.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_lvsrSeason.dOnGetObject += this.GetSlotSeason;
			this.m_lvsrSeason.dOnReturnObject += this.ReturnSlot;
			this.m_lvsrSeason.dOnProvideData += this.ProvideDataSeason;
			this.m_lvsrSeason.ContentConstraintCount = 1;
			NKCUtil.SetScrollHotKey(this.m_lvsrSeason, null);
			this.m_lvsrWeekly.dOnGetObject += this.GetSlotWeek;
			this.m_lvsrWeekly.dOnReturnObject += this.ReturnSlot;
			this.m_lvsrWeekly.dOnProvideData += this.ProvideDataWeek;
			this.m_lvsrWeekly.ContentConstraintCount = 1;
			NKCUtil.SetScrollHotKey(this.m_lvsrWeekly, null);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06008529 RID: 34089 RVA: 0x002D0293 File Offset: 0x002CE493
		public RectTransform GetSlotSeason(int index)
		{
			return NKCPopupGauntletLGSlot.GetNewInstance(this.m_trContentSeason).GetComponent<RectTransform>();
		}

		// Token: 0x0600852A RID: 34090 RVA: 0x002D02A8 File Offset: 0x002CE4A8
		public void ReturnSlot(Transform tr)
		{
			NKCPopupGauntletLGSlot component = tr.GetComponent<NKCPopupGauntletLGSlot>();
			tr.SetParent(base.transform);
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x0600852B RID: 34091 RVA: 0x002D02E4 File Offset: 0x002CE4E4
		public void ProvideDataSeason(Transform tr, int index)
		{
			if (index < 0)
			{
				NKCUtil.SetGameobjectActive(tr, false);
				return;
			}
			if (this.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_LEAGUE)
			{
				if (index >= this.m_lstNKMLeaguePvpRankTempletSorted.Count)
				{
					NKCUtil.SetGameobjectActive(tr, false);
					return;
				}
				NKCPopupGauntletLGSlot component = tr.GetComponent<NKCPopupGauntletLGSlot>();
				if (component != null)
				{
					component.SetUI(true, this.m_lstNKMLeaguePvpRankTempletSorted[index], this.m_MyLeagueRankTemplet == this.m_lstNKMLeaguePvpRankTempletSorted[index]);
					return;
				}
			}
			else
			{
				if (index >= this.m_lstNKMPvpRankTempletSorted.Count)
				{
					NKCUtil.SetGameobjectActive(tr, false);
					return;
				}
				NKCPopupGauntletLGSlot component2 = tr.GetComponent<NKCPopupGauntletLGSlot>();
				if (component2 != null)
				{
					component2.SetUI(true, this.m_lstNKMPvpRankTempletSorted[index], this.m_MyRankTemplet == this.m_lstNKMPvpRankTempletSorted[index]);
				}
			}
		}

		// Token: 0x0600852C RID: 34092 RVA: 0x002D03A2 File Offset: 0x002CE5A2
		public RectTransform GetSlotWeek(int index)
		{
			return NKCPopupGauntletLGSlot.GetNewInstance(this.m_trContentWeekly).GetComponent<RectTransform>();
		}

		// Token: 0x0600852D RID: 34093 RVA: 0x002D03B4 File Offset: 0x002CE5B4
		public void ProvideDataWeek(Transform tr, int index)
		{
			NKCPopupGauntletLGSlot component = tr.GetComponent<NKCPopupGauntletLGSlot>();
			if (component != null)
			{
				component.SetUI(false, this.m_lstNKMPvpRankTempletSorted[index], this.m_MyRankTemplet == this.m_lstNKMPvpRankTempletSorted[index]);
			}
		}

		// Token: 0x0600852E RID: 34094 RVA: 0x002D03F8 File Offset: 0x002CE5F8
		private void OnValueChangedSeason(bool bSet)
		{
			if (bSet)
			{
				this.m_NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB = NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_SEASON;
				this.SetUIByTab();
			}
		}

		// Token: 0x0600852F RID: 34095 RVA: 0x002D040A File Offset: 0x002CE60A
		private void OnValueChangedWeek(bool bSet)
		{
			if (bSet)
			{
				this.m_NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB = NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_WEEK;
				this.SetUIByTab();
			}
		}

		// Token: 0x06008530 RID: 34096 RVA: 0x002D041C File Offset: 0x002CE61C
		private void OnValueChangedHelp(bool bSet)
		{
			if (bSet)
			{
				this.m_NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB = NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_HELP;
				this.SetUIByTab();
			}
		}

		// Token: 0x06008531 RID: 34097 RVA: 0x002D042E File Offset: 0x002CE62E
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06008532 RID: 34098 RVA: 0x002D043C File Offset: 0x002CE63C
		private void SetUIByTab()
		{
			NKCUtil.SetGameobjectActive(this.m_objSeasonWeekPage, this.m_NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB == NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_SEASON || this.m_NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB == NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_WEEK);
			NKCUtil.SetGameobjectActive(this.m_objHelpPage, this.m_NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB == NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_HELP);
			NKCUtil.SetGameobjectActive(this.m_lvsrSeason.gameObject, this.m_NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB == NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_SEASON);
			NKCUtil.SetGameobjectActive(this.m_lvsrWeekly.gameObject, this.m_NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB == NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_WEEK);
			if (this.m_NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB == NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_SEASON)
			{
				NKCUtil.SetGameobjectActive(this.m_objWeeklyRewardLock, false);
				this.m_lvsrSeason.TotalCount = this.GetListCount();
				this.m_lvsrSeason.velocity = new Vector2(0f, 0f);
				if (!this.m_bOpenSeason)
				{
					int myRankIndex = this.GetMyRankIndex();
					this.m_lvsrSeason.SetIndexPosition(myRankIndex);
					this.m_bOpenSeason = true;
				}
				else
				{
					this.m_lvsrSeason.RefreshCells(false);
				}
				this.m_lbLeftBottomTitle.text = string.Format(NKCUtilString.GET_STRING_GAUNTLET_SEASON_TITLE_ONE_PARAM, this.GetSeasonStrID());
				this.m_lbLeftBottomPeriod.text = NKMTime.UTCtoLocal(this.GetSeasonStartDate(), 0).ToString("yyyy. MM. dd") + " ~ " + NKMTime.UTCtoLocal(this.GetSeasonEndDate(), 0).ToString("yyyy. MM. dd");
				this.m_lbLeftBottomDesc.text = this.GetDesc(this.m_NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB);
				return;
			}
			if (this.m_NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB == NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_WEEK)
			{
				this.m_lvsrWeekly.TotalCount = this.m_lstNKMPvpRankTempletSorted.Count;
				this.m_lvsrWeekly.velocity = new Vector2(0f, 0f);
				if (!this.m_bOpenWeek)
				{
					this.m_lvsrWeekly.SetIndexPosition(0);
					this.m_bOpenWeek = true;
				}
				else
				{
					this.m_lvsrWeekly.RefreshCells(false);
				}
				this.m_lbLeftBottomTitle.text = NKCUtilString.GET_STRING_GAUNTLET_WEEK_LEAGUE;
				this.UpdateWeekPeriod();
				this.m_lbLeftBottomDesc.text = this.GetDesc(this.m_NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB);
				return;
			}
			if (this.m_NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB == NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_HELP)
			{
				this.m_lbLeftBottomDesc.text = this.GetDesc(this.m_NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB);
			}
		}

		// Token: 0x06008533 RID: 34099 RVA: 0x002D064D File Offset: 0x002CE84D
		private int GetListCount()
		{
			if (this.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_LEAGUE)
			{
				return this.m_lstNKMLeaguePvpRankTempletSorted.Count;
			}
			return this.m_lstNKMPvpRankTempletSorted.Count;
		}

		// Token: 0x06008534 RID: 34100 RVA: 0x002D0670 File Offset: 0x002CE870
		private int GetMyRankIndex()
		{
			if (this.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_LEAGUE)
			{
				return this.m_lstNKMLeaguePvpRankTempletSorted.FindIndex((NKMLeaguePvpRankTemplet v) => v == this.m_MyLeagueRankTemplet);
			}
			return this.m_lstNKMPvpRankTempletSorted.FindIndex((NKMPvpRankTemplet v) => v == this.m_MyRankTemplet);
		}

		// Token: 0x06008535 RID: 34101 RVA: 0x002D06AC File Offset: 0x002CE8AC
		private string GetSeasonStrID()
		{
			if (this.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_LEAGUE)
			{
				NKMLeaguePvpRankSeasonTemplet nkmleaguePvpRankSeasonTemplet = NKMLeaguePvpRankSeasonTemplet.Find(ServiceTime.Recent);
				if (nkmleaguePvpRankSeasonTemplet != null)
				{
					return nkmleaguePvpRankSeasonTemplet.GetSeasonStrId();
				}
			}
			else
			{
				int seasonID = this.FindPvpSeasonID(NKCSynchronizedTime.GetServerUTCTime(0.0));
				NKMPvpRankSeasonTemplet seasonTemplet = this.GetSeasonTemplet(seasonID);
				if (seasonTemplet != null)
				{
					return seasonTemplet.GetSeasonStrID();
				}
			}
			return string.Empty;
		}

		// Token: 0x06008536 RID: 34102 RVA: 0x002D0704 File Offset: 0x002CE904
		private DateTime GetSeasonStartDate()
		{
			if (this.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_LEAGUE)
			{
				NKMLeaguePvpRankSeasonTemplet nkmleaguePvpRankSeasonTemplet = NKMLeaguePvpRankSeasonTemplet.Find(ServiceTime.Recent);
				if (nkmleaguePvpRankSeasonTemplet != null)
				{
					return nkmleaguePvpRankSeasonTemplet.StartDateUTC;
				}
			}
			else
			{
				int seasonID = this.FindPvpSeasonID(NKCSynchronizedTime.GetServerUTCTime(0.0));
				NKMPvpRankSeasonTemplet seasonTemplet = this.GetSeasonTemplet(seasonID);
				if (seasonTemplet != null)
				{
					return seasonTemplet.StartDate;
				}
			}
			return default(DateTime);
		}

		// Token: 0x06008537 RID: 34103 RVA: 0x002D0760 File Offset: 0x002CE960
		private DateTime GetSeasonEndDate()
		{
			if (this.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_LEAGUE)
			{
				NKMLeaguePvpRankSeasonTemplet nkmleaguePvpRankSeasonTemplet = NKMLeaguePvpRankSeasonTemplet.Find(ServiceTime.Recent);
				if (nkmleaguePvpRankSeasonTemplet != null)
				{
					return nkmleaguePvpRankSeasonTemplet.EndDateUTC;
				}
			}
			else
			{
				int seasonID = this.FindPvpSeasonID(NKCSynchronizedTime.GetServerUTCTime(0.0));
				NKMPvpRankSeasonTemplet seasonTemplet = this.GetSeasonTemplet(seasonID);
				if (seasonTemplet != null)
				{
					return seasonTemplet.EndDate;
				}
			}
			return default(DateTime);
		}

		// Token: 0x06008538 RID: 34104 RVA: 0x002D07BC File Offset: 0x002CE9BC
		private void UpdateWeekPeriod()
		{
			int seasonID = this.FindPvpSeasonID(NKCSynchronizedTime.GetServerUTCTime(0.0));
			NKMPvpRankSeasonTemplet seasonTemplet = this.GetSeasonTemplet(seasonID);
			if (seasonTemplet != null)
			{
				if (!NKCPVPManager.IsRewardWeek(seasonTemplet, NKCPVPManager.WeekCalcStartDateUtc))
				{
					this.m_lbLeftBottomPeriod.text = "-";
					NKCUtil.SetGameobjectActive(this.m_objWeeklyRewardLock, true);
					return;
				}
				NKCUtil.SetGameobjectActive(this.m_objWeeklyRewardLock, false);
				this.m_lbLeftBottomPeriod.text = NKCUtilString.GetRemainTimeStringForGauntletWeekly();
			}
		}

		// Token: 0x06008539 RID: 34105 RVA: 0x002D082F File Offset: 0x002CEA2F
		private void Update()
		{
			if (this.m_fPrevUpdateTime + 1f < Time.time)
			{
				if (this.m_NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB == NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_WEEK)
				{
					this.UpdateWeekPeriod();
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objWeeklyRewardLock, false);
				}
				this.m_fPrevUpdateTime = Time.time;
			}
		}

		// Token: 0x0600853A RID: 34106 RVA: 0x002D086C File Offset: 0x002CEA6C
		public void Open(NKC_GAUNTLET_LOBBY_TAB lobbyTab, PvpState pvpData, NKMPvpRankSeasonTemplet curSeasonTemplet)
		{
			if (curSeasonTemplet == null)
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				return;
			}
			this.m_bOpenSeason = false;
			this.m_bOpenWeek = false;
			this.m_NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB = NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_SEASON;
			this.m_NKC_GAUNTLET_LOBBY_TAB = lobbyTab;
			int seasonID = curSeasonTemplet.SeasonID;
			int rankGroup = curSeasonTemplet.RankGroup;
			if (!NKCPVPManager.dicPvpRankTemplet.ContainsKey(rankGroup))
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				return;
			}
			this.m_lstNKMPvpRankTempletSorted = (from e in NKCPVPManager.dicPvpRankTemplet[rankGroup].Values
			orderby e.LeagueTier descending
			select e).ToList<NKMPvpRankTemplet>();
			NKCUtil.SetGameobjectActive(this.m_ctglWeek, true);
			if (lobbyTab != NKC_GAUNTLET_LOBBY_TAB.NGLT_RANK)
			{
				if (lobbyTab != NKC_GAUNTLET_LOBBY_TAB.NGLT_ASYNC)
				{
					Debug.LogError(string.Format("Gauntlet Info Lobby Tab ?? : {0}", lobbyTab));
					NKCUtil.SetLabelText(this.m_lbLeftCenter, "");
					this.m_imgTierBG.sprite = null;
				}
				else
				{
					this.m_NKM_GAME_TYPE = NKM_GAME_TYPE.NGT_ASYNC_PVP;
					NKCUtil.SetLabelText(this.m_lbLeftCenter, NKCUtilString.GET_STRING_GAUNTLET_ASYNC_GAME);
					this.m_imgTierBG.sprite = this.SpriteTierBG_Async;
					this.m_imgTierBG_Help.sprite = this.SpriteTierBG_Async;
					NKCUtil.SetLabelText(this.m_lbRightHelpDesc, this.GetDesc(NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_HELP));
				}
			}
			else
			{
				this.m_NKM_GAME_TYPE = NKM_GAME_TYPE.NGT_PVP_RANK;
				NKCUtil.SetLabelText(this.m_lbLeftCenter, NKCUtilString.GET_STRING_GAUNTLET_RANK_GAME);
				this.m_imgTierBG.sprite = this.SpriteTierBG_Rank;
				this.m_imgTierBG_Help.sprite = this.SpriteTierBG_Rank;
				NKCUtil.SetLabelText(this.m_lbRightHelpDesc, this.GetDesc(NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_HELP));
			}
			if (seasonID != pvpData.SeasonID)
			{
				this.m_MyRankTemplet = NKCPVPManager.GetRankTempletByRankGroupScore(rankGroup, NKCUtil.GetScoreBySeason(seasonID, pvpData.SeasonID, pvpData.Score, this.m_NKM_GAME_TYPE));
			}
			else
			{
				this.m_MyRankTemplet = NKCPVPManager.GetRankTempletByRankGroupTier(rankGroup, pvpData.LeagueTierID);
			}
			base.UIOpened(true);
			if (!this.m_bLoopScrollInit)
			{
				this.m_lvsrSeason.PrepareCells(0);
				this.m_lvsrWeekly.PrepareCells(0);
				this.m_bLoopScrollInit = true;
			}
			this.m_ctglSeason.Select(false, true, false);
			this.m_ctglSeason.Select(true, false, false);
		}

		// Token: 0x0600853B RID: 34107 RVA: 0x002D0A80 File Offset: 0x002CEC80
		public void Open(NKC_GAUNTLET_LOBBY_TAB lobbyTab, PvpState pvpData, NKMLeaguePvpRankSeasonTemplet curSeasonTemplet)
		{
			if (curSeasonTemplet == null)
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				return;
			}
			this.m_bOpenSeason = false;
			this.m_bOpenWeek = false;
			this.m_NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB = NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_SEASON;
			this.m_NKC_GAUNTLET_LOBBY_TAB = lobbyTab;
			int seasonId = curSeasonTemplet.SeasonId;
			int key = curSeasonTemplet.RankGroup.Key;
			if (curSeasonTemplet.RankGroup.List.Count <= 0)
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				return;
			}
			this.m_lstNKMLeaguePvpRankTempletSorted = (from e in curSeasonTemplet.RankGroup.List
			orderby e.LeagueTier descending
			select e).ToList<NKMLeaguePvpRankTemplet>();
			NKCUtil.SetGameobjectActive(this.m_ctglWeek, false);
			if (lobbyTab == NKC_GAUNTLET_LOBBY_TAB.NGLT_LEAGUE)
			{
				this.m_NKM_GAME_TYPE = NKM_GAME_TYPE.NGT_PVP_LEAGUE;
				NKCUtil.SetLabelText(this.m_lbLeftCenter, NKCUtilString.GET_STRING_GAUNTLET_LEAGUE_GAME);
				this.m_imgTierBG.sprite = this.SpriteTierBG_League;
				this.m_imgTierBG_Help.sprite = this.SpriteTierBG_League;
				NKCUtil.SetLabelText(this.m_lbRightHelpDesc, this.GetDesc(NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_HELP));
			}
			else
			{
				Debug.LogError(string.Format("Gauntlet Info Lobby Tab ?? : {0}", lobbyTab));
				NKCUtil.SetLabelText(this.m_lbLeftCenter, "");
				this.m_imgTierBG.sprite = null;
			}
			if (seasonId != pvpData.SeasonID)
			{
				this.m_MyLeagueRankTemplet = curSeasonTemplet.RankGroup.GetByScore(NKCPVPManager.GetResetScore(pvpData.SeasonID, pvpData.Score, this.m_NKM_GAME_TYPE));
			}
			else
			{
				this.m_MyLeagueRankTemplet = curSeasonTemplet.RankGroup.GetByTier(pvpData.LeagueTierID);
			}
			base.UIOpened(true);
			if (!this.m_bLoopScrollInit)
			{
				this.m_lvsrSeason.PrepareCells(0);
				this.m_lvsrWeekly.PrepareCells(0);
				this.m_bLoopScrollInit = true;
			}
			this.m_ctglSeason.Select(false, true, false);
			this.m_ctglSeason.Select(true, false, false);
		}

		// Token: 0x0600853C RID: 34108 RVA: 0x002D0C4C File Offset: 0x002CEE4C
		private int FindPvpSeasonID(DateTime now)
		{
			NKC_GAUNTLET_LOBBY_TAB nkc_GAUNTLET_LOBBY_TAB = this.m_NKC_GAUNTLET_LOBBY_TAB;
			if (nkc_GAUNTLET_LOBBY_TAB == NKC_GAUNTLET_LOBBY_TAB.NGLT_RANK)
			{
				return NKCUtil.FindPVPSeasonIDForRank(now);
			}
			if (nkc_GAUNTLET_LOBBY_TAB != NKC_GAUNTLET_LOBBY_TAB.NGLT_ASYNC)
			{
				return 0;
			}
			return NKCUtil.FindPVPSeasonIDForAsync(now);
		}

		// Token: 0x0600853D RID: 34109 RVA: 0x002D0C78 File Offset: 0x002CEE78
		private NKMPvpRankSeasonTemplet GetSeasonTemplet(int seasonID)
		{
			NKC_GAUNTLET_LOBBY_TAB nkc_GAUNTLET_LOBBY_TAB = this.m_NKC_GAUNTLET_LOBBY_TAB;
			if (nkc_GAUNTLET_LOBBY_TAB == NKC_GAUNTLET_LOBBY_TAB.NGLT_RANK)
			{
				return NKCPVPManager.GetPvpRankSeasonTemplet(seasonID);
			}
			if (nkc_GAUNTLET_LOBBY_TAB != NKC_GAUNTLET_LOBBY_TAB.NGLT_ASYNC)
			{
				return null;
			}
			return NKCPVPManager.GetPvpAsyncSeasonTemplet(seasonID);
		}

		// Token: 0x0600853E RID: 34110 RVA: 0x002D0CA4 File Offset: 0x002CEEA4
		private string GetDesc(NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB gauideTab)
		{
			NKC_GAUNTLET_LOBBY_TAB nkc_GAUNTLET_LOBBY_TAB = this.m_NKC_GAUNTLET_LOBBY_TAB;
			if (nkc_GAUNTLET_LOBBY_TAB != NKC_GAUNTLET_LOBBY_TAB.NGLT_RANK)
			{
				if (nkc_GAUNTLET_LOBBY_TAB != NKC_GAUNTLET_LOBBY_TAB.NGLT_ASYNC)
				{
					if (nkc_GAUNTLET_LOBBY_TAB == NKC_GAUNTLET_LOBBY_TAB.NGLT_LEAGUE)
					{
						switch (gauideTab)
						{
						case NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_SEASON:
							return NKCUtilString.GET_STRING_GAUNTLET_LEAGUE_SEASON_LEAGUE_DESC;
						case NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_WEEK:
							return "";
						case NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_HELP:
							return NKCUtilString.GET_STRING_GAUNTLET_LEAGUE_HELP_DESC;
						}
					}
				}
				else
				{
					switch (gauideTab)
					{
					case NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_SEASON:
						return NKCUtilString.GET_STRING_GAUNTLET_ASYNC_SEASON_LEAGUE_DESC;
					case NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_WEEK:
						return NKCUtilString.GET_STRING_GAUNTLET_ASYNC_WEEK_LEAGUE_DESC;
					case NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_HELP:
						return NKCUtilString.GET_STRING_GAUNTLET_ASYNC_HELP_DESC;
					}
				}
			}
			else
			{
				switch (gauideTab)
				{
				case NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_SEASON:
					return NKCUtilString.GET_STRING_GAUNTLET_SEASON_LEAUGE_DESC;
				case NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_WEEK:
					return NKCUtilString.GET_STRING_GAUNTLET_WEEK_LEAGUE_DESC;
				case NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB.NPGLGT_HELP:
					return NKCUtilString.GET_STRING_GAUNTLET_RANK_HELP_DESC;
				}
			}
			return string.Empty;
		}

		// Token: 0x04007190 RID: 29072
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_GAUNTLET";

		// Token: 0x04007191 RID: 29073
		private const string UI_ASSET_NAME = "NKM_UI_GAUNTLET_POPUP_LEAGUE_GUIDE";

		// Token: 0x04007192 RID: 29074
		public GameObject m_objSeasonWeekPage;

		// Token: 0x04007193 RID: 29075
		public GameObject m_objHelpPage;

		// Token: 0x04007194 RID: 29076
		public Image m_imgTierBG;

		// Token: 0x04007195 RID: 29077
		public Image m_imgTierBG_Help;

		// Token: 0x04007196 RID: 29078
		public Text m_lbLeftCenter;

		// Token: 0x04007197 RID: 29079
		public Text m_lbLeftBottomTitle;

		// Token: 0x04007198 RID: 29080
		public Text m_lbLeftBottomPeriod;

		// Token: 0x04007199 RID: 29081
		public Text m_lbLeftBottomDesc;

		// Token: 0x0400719A RID: 29082
		public Text m_lbRightHelpDesc;

		// Token: 0x0400719B RID: 29083
		public NKCUIComToggle m_ctglSeason;

		// Token: 0x0400719C RID: 29084
		public NKCUIComToggle m_ctglWeek;

		// Token: 0x0400719D RID: 29085
		public NKCUIComToggle m_ctglHelp;

		// Token: 0x0400719E RID: 29086
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x0400719F RID: 29087
		public LoopVerticalScrollRect m_lvsrSeason;

		// Token: 0x040071A0 RID: 29088
		public Transform m_trContentSeason;

		// Token: 0x040071A1 RID: 29089
		public LoopVerticalScrollRect m_lvsrWeekly;

		// Token: 0x040071A2 RID: 29090
		public Transform m_trContentWeekly;

		// Token: 0x040071A3 RID: 29091
		public GameObject m_objWeeklyRewardLock;

		// Token: 0x040071A4 RID: 29092
		[Header("Sprite")]
		public Sprite SpriteTierBG_League;

		// Token: 0x040071A5 RID: 29093
		public Sprite SpriteTierBG_Rank;

		// Token: 0x040071A6 RID: 29094
		public Sprite SpriteTierBG_Async;

		// Token: 0x040071A7 RID: 29095
		private NKCPopupGauntletLeagueGuide.NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB m_NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB;

		// Token: 0x040071A8 RID: 29096
		private NKC_GAUNTLET_LOBBY_TAB m_NKC_GAUNTLET_LOBBY_TAB;

		// Token: 0x040071A9 RID: 29097
		private List<NKMPvpRankTemplet> m_lstNKMPvpRankTempletSorted;

		// Token: 0x040071AA RID: 29098
		private NKMPvpRankTemplet m_MyRankTemplet;

		// Token: 0x040071AB RID: 29099
		private List<NKMLeaguePvpRankTemplet> m_lstNKMLeaguePvpRankTempletSorted;

		// Token: 0x040071AC RID: 29100
		private NKMLeaguePvpRankTemplet m_MyLeagueRankTemplet;

		// Token: 0x040071AD RID: 29101
		private bool m_bLoopScrollInit;

		// Token: 0x040071AE RID: 29102
		private bool m_bOpenSeason;

		// Token: 0x040071AF RID: 29103
		private bool m_bOpenWeek;

		// Token: 0x040071B0 RID: 29104
		private float m_fPrevUpdateTime;

		// Token: 0x040071B1 RID: 29105
		private NKM_GAME_TYPE m_NKM_GAME_TYPE;

		// Token: 0x02001905 RID: 6405
		public enum NKC_POPUP_GAUNTLET_LEAUGE_GUIDE_TAB
		{
			// Token: 0x0400AA54 RID: 43604
			NPGLGT_SEASON,
			// Token: 0x0400AA55 RID: 43605
			NPGLGT_WEEK,
			// Token: 0x0400AA56 RID: 43606
			NPGLGT_HELP
		}
	}
}
