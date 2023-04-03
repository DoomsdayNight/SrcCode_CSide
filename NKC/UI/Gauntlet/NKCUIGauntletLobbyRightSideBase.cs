using System;
using Cs.Core.Util;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B7B RID: 2939
	public abstract class NKCUIGauntletLobbyRightSideBase : MonoBehaviour
	{
		// Token: 0x06008754 RID: 34644 RVA: 0x002DC554 File Offset: 0x002DA754
		public virtual void InitUI()
		{
			if (this.m_csbtnLeagueGuide != null)
			{
				this.m_csbtnLeagueGuide.PointerClick.RemoveAllListeners();
				this.m_csbtnLeagueGuide.PointerClick.AddListener(new UnityAction(this.OnClickLeagueGuide));
			}
			if (this.m_csbtnBattleHistory != null)
			{
				this.m_csbtnBattleHistory.PointerClick.RemoveAllListeners();
				this.m_csbtnBattleHistory.PointerClick.AddListener(new UnityAction(this.OnClickBattleRecord));
			}
			if (this.m_csbtnEmoticonSetting != null)
			{
				this.m_csbtnEmoticonSetting.PointerClick.RemoveAllListeners();
				this.m_csbtnEmoticonSetting.PointerClick.AddListener(new UnityAction(this.OnClickEmoticonSetting));
			}
		}

		// Token: 0x06008755 RID: 34645 RVA: 0x002DC610 File Offset: 0x002DA810
		public virtual void UpdateNowSeasonPVPInfoUI(NKM_GAME_TYPE gameType)
		{
			if (NKCScenManager.GetScenManager().GetMyUserData() == null)
			{
				return;
			}
			int num = 0;
			this.m_NKM_GAME_TYPE = gameType;
			if (gameType <= NKM_GAME_TYPE.NGT_ASYNC_PVP)
			{
				if (gameType == NKM_GAME_TYPE.NGT_PVP_RANK)
				{
					num = NKCUtil.FindPVPSeasonIDForRank(NKCSynchronizedTime.GetServerUTCTime(0.0));
					goto IL_72;
				}
				if (gameType != NKM_GAME_TYPE.NGT_ASYNC_PVP)
				{
					goto IL_72;
				}
			}
			else
			{
				if (gameType == NKM_GAME_TYPE.NGT_PVP_LEAGUE)
				{
					num = NKCUtil.FindPVPSeasonIDForLeague(NKCSynchronizedTime.GetServerUTCTime(0.0));
					goto IL_72;
				}
				if (gameType - NKM_GAME_TYPE.NGT_PVP_STRATEGY > 2)
				{
					goto IL_72;
				}
			}
			num = NKCUtil.FindPVPSeasonIDForAsync(NKCSynchronizedTime.GetServerUTCTime(0.0));
			IL_72:
			PvpState pvpData = this.GetPvpData();
			if (pvpData == null)
			{
				return;
			}
			if (pvpData.SeasonID != num)
			{
				this.m_cgMyInfo.alpha = 0f;
				NKCUtil.SetGameobjectActive(this.m_objNoRecord, true);
				NKCUtil.SetLabelText(this.m_lbLeagueRank, NKCUtilString.GET_STRING_GAUNTLET_RANK_NO_JOIN);
				NKCUtil.SetLabelText(this.m_lbWinCount, "-");
				NKCUtil.SetLabelText(this.m_lbMaxWinStreak, "-");
				NKCUtil.SetLabelText(this.m_lbMaxScore, "-");
				NKCUtil.SetLabelText(this.m_lbMaxLeagueTierId, "-");
				int resetScore = NKCPVPManager.GetResetScore(pvpData.SeasonID, pvpData.Score, gameType);
				NKCUtil.SetLabelText(this.m_lbScore, resetScore.ToString());
				this.SetMyLeagueTier(NKCPVPManager.GetTierIconByScore(gameType, num, resetScore), NKCPVPManager.GetTierNumberByScore(gameType, num, resetScore));
				NKCUtil.SetLabelText(this.m_lbTier, this.GetLeagueNameByScore(num, pvpData));
				return;
			}
			this.SetMyLeagueTier(NKCPVPManager.GetTierIconByTier(gameType, num, pvpData.LeagueTierID), NKCPVPManager.GetTierNumberByTier(gameType, num, pvpData.LeagueTierID));
			NKCUtil.SetLabelText(this.m_lbTier, this.GetLeagueNameByTier(num, pvpData));
			this.m_cgMyInfo.alpha = 1f;
			NKCUtil.SetLabelText(this.m_lbScore, pvpData.Score.ToString());
			NKCUtil.SetGameobjectActive(this.m_objNoRecord, false);
			NKCUtil.SetLabelText(this.m_lbLeagueRank, string.Format(NKCUtilString.GET_STRING_TOTAL_RANK_ONE_PARAM, pvpData.Rank));
			NKCUtil.SetLabelText(this.m_lbWinCount, pvpData.WinCount.ToString());
			NKCUtil.SetLabelText(this.m_lbMaxWinStreak, pvpData.MaxWinStreak.ToString());
			NKCUtil.SetLabelText(this.m_lbMaxScore, pvpData.MaxScore.ToString());
			NKCUtil.SetLabelText(this.m_lbMaxLeagueTierId, NKCPVPManager.GetLeagueNameByTier(gameType, num, pvpData.LeagueTierID));
		}

		// Token: 0x06008756 RID: 34646 RVA: 0x002DC840 File Offset: 0x002DAA40
		private void SetMyLeagueTier(LEAGUE_TIER_ICON leagueTierIcon, int leagueTierNum)
		{
			this.m_NKCUILeagueTierMy.SetUI(leagueTierIcon, leagueTierNum);
		}

		// Token: 0x06008757 RID: 34647 RVA: 0x002DC850 File Offset: 0x002DAA50
		public void UpdateBattleCondition()
		{
			NKMPvpRankSeasonTemplet seasonTemplet = this.GetSeasonTemplet();
			if (seasonTemplet == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objBattleCond, false);
				return;
			}
			if (string.IsNullOrEmpty(seasonTemplet.SeasonBattleCondition))
			{
				NKCUtil.SetGameobjectActive(this.m_objBattleCond, false);
				return;
			}
			NKMBattleConditionTemplet templetByStrID = NKMBattleConditionManager.GetTempletByStrID(seasonTemplet.SeasonBattleCondition);
			if (templetByStrID != null)
			{
				NKCUtil.SetGameobjectActive(this.m_objBattleCond, true);
				NKCUtil.SetImageSprite(this.m_imgBattleCond, NKCUtil.GetSpriteBattleConditionICon(templetByStrID), false);
				NKCUtil.SetLabelText(this.m_lbBattleCondTitle, templetByStrID.BattleCondName_Translated);
				NKCUtil.SetLabelText(this.m_lbBattleCondDesc, templetByStrID.BattleCondDesc_Translated);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objBattleCond, false);
		}

		// Token: 0x06008758 RID: 34648 RVA: 0x002DC8EC File Offset: 0x002DAAEC
		private void OnClickLeagueGuide()
		{
			if (this.m_NKCPopupGauntletLeagueGuide == null)
			{
				this.m_NKCPopupGauntletLeagueGuide = NKCPopupGauntletLeagueGuide.OpenInstance();
			}
			PvpState pvpData = this.GetPvpData();
			if (this.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_ASYNC_PVP || this.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_RANK)
			{
				NKMPvpRankSeasonTemplet seasonTemplet = this.GetSeasonTemplet();
				this.m_NKCPopupGauntletLeagueGuide.Open(NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().GetCurrentLobbyTab(), pvpData, seasonTemplet);
				return;
			}
			if (this.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_LEAGUE)
			{
				NKMLeaguePvpRankSeasonTemplet curSeasonTemplet = NKMLeaguePvpRankSeasonTemplet.Find(ServiceTime.Recent);
				this.m_NKCPopupGauntletLeagueGuide.Open(NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().GetCurrentLobbyTab(), pvpData, curSeasonTemplet);
			}
		}

		// Token: 0x06008759 RID: 34649 RVA: 0x002DC984 File Offset: 0x002DAB84
		private void OnClickBattleRecord()
		{
			NKC_SCEN_GAUNTLET_LOBBY nkc_SCEN_GAUNTLET_LOBBY = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY();
			if (nkc_SCEN_GAUNTLET_LOBBY != null)
			{
				nkc_SCEN_GAUNTLET_LOBBY.OpenBattleRecord(this.m_NKM_GAME_TYPE);
			}
		}

		// Token: 0x0600875A RID: 34650 RVA: 0x002DC9AB File Offset: 0x002DABAB
		private void OnClickEmoticonSetting()
		{
			if (NKCEmoticonManager.m_bReceivedEmoticonData)
			{
				NKCPopupEmoticonSetting.Instance.Open();
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_LOBBY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetWaitForEmoticon(true);
				NKCPacketSender.Send_NKMPacket_EMOTICON_DATA_REQ(NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL);
			}
		}

		// Token: 0x0600875B RID: 34651
		protected abstract PvpState GetPvpData();

		// Token: 0x0600875C RID: 34652
		protected abstract NKMPvpRankSeasonTemplet GetSeasonTemplet();

		// Token: 0x0600875D RID: 34653
		protected abstract string GetLeagueNameByScore(int seasonID, PvpState pvpData);

		// Token: 0x0600875E RID: 34654
		protected abstract string GetLeagueNameByTier(int seasonID, PvpState pvpData);

		// Token: 0x040073B5 RID: 29621
		public CanvasGroup m_cgMyInfo;

		// Token: 0x040073B6 RID: 29622
		public NKCUILeagueTier m_NKCUILeagueTierMy;

		// Token: 0x040073B7 RID: 29623
		public Text m_lbScore;

		// Token: 0x040073B8 RID: 29624
		public Text m_lbTier;

		// Token: 0x040073B9 RID: 29625
		public Text m_lbLeagueRank;

		// Token: 0x040073BA RID: 29626
		public GameObject m_objNoRecord;

		// Token: 0x040073BB RID: 29627
		public Text m_lbWinCount;

		// Token: 0x040073BC RID: 29628
		public Text m_lbMaxWinStreak;

		// Token: 0x040073BD RID: 29629
		public Text m_lbMaxScore;

		// Token: 0x040073BE RID: 29630
		public Text m_lbMaxLeagueTierId;

		// Token: 0x040073BF RID: 29631
		public NKCUIComStateButton m_csbtnLeagueGuide;

		// Token: 0x040073C0 RID: 29632
		public NKCUIComStateButton m_csbtnBattleHistory;

		// Token: 0x040073C1 RID: 29633
		public NKCUIComStateButton m_csbtnEmoticonSetting;

		// Token: 0x040073C2 RID: 29634
		[Header("전투 환경")]
		public GameObject m_objBattleCond;

		// Token: 0x040073C3 RID: 29635
		public Image m_imgBattleCond;

		// Token: 0x040073C4 RID: 29636
		public Text m_lbBattleCondTitle;

		// Token: 0x040073C5 RID: 29637
		public Text m_lbBattleCondDesc;

		// Token: 0x040073C6 RID: 29638
		private NKCPopupGauntletLeagueGuide m_NKCPopupGauntletLeagueGuide;

		// Token: 0x040073C7 RID: 29639
		private NKM_GAME_TYPE m_NKM_GAME_TYPE;
	}
}
