using System;
using NKM;
using NKM.Templet;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B65 RID: 2917
	public class NKCPopupGauntletNewSeasonAlarm : NKCUIBase
	{
		// Token: 0x170015A6 RID: 5542
		// (get) Token: 0x06008542 RID: 34114 RVA: 0x002D0D5A File Offset: 0x002CEF5A
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170015A7 RID: 5543
		// (get) Token: 0x06008543 RID: 34115 RVA: 0x002D0D5D File Offset: 0x002CEF5D
		public override string MenuName
		{
			get
			{
				return "PopupGauntletNewSeasonAlarm";
			}
		}

		// Token: 0x06008544 RID: 34116 RVA: 0x002D0D64 File Offset: 0x002CEF64
		public static void SetPrevNKMPVPData(PvpState cNKMPVPData)
		{
			NKCPopupGauntletNewSeasonAlarm.m_PrevNKMPVPData = cNKMPVPData;
		}

		// Token: 0x06008545 RID: 34117 RVA: 0x002D0D6C File Offset: 0x002CEF6C
		public void InitUI()
		{
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData e)
			{
				this.OnCloseBtn();
			});
			this.m_etBG.triggers.Add(entry);
			NKCUtil.SetBindFunction(this.m_csbtnOK, new UnityAction(this.OnClickOK));
			NKCUtil.SetHotkey(this.m_csbtnOK, HotkeyEventType.Confirm, null, false);
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06008546 RID: 34118 RVA: 0x002D0DF0 File Offset: 0x002CEFF0
		private void OnClickOK()
		{
			base.Close();
		}

		// Token: 0x06008547 RID: 34119 RVA: 0x002D0DF8 File Offset: 0x002CEFF8
		public void Open(bool bRank)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			PvpState pvpState;
			NKMPvpRankSeasonTemplet nkmpvpRankSeasonTemplet;
			NKMPvpRankSeasonTemplet nkmpvpRankSeasonTemplet2;
			if (bRank)
			{
				pvpState = nkmuserData.m_PvpData;
				nkmpvpRankSeasonTemplet = NKCPVPManager.GetPvpRankSeasonTemplet(NKCUtil.FindPVPSeasonIDForRank(NKCSynchronizedTime.GetServerUTCTime(0.0)));
				nkmpvpRankSeasonTemplet2 = NKCPVPManager.GetPvpRankSeasonTemplet(NKCPopupGauntletNewSeasonAlarm.m_PrevNKMPVPData.SeasonID);
			}
			else
			{
				pvpState = nkmuserData.m_AsyncData;
				nkmpvpRankSeasonTemplet = NKCPVPManager.GetPvpAsyncSeasonTemplet(NKCUtil.FindPVPSeasonIDForAsync(NKCSynchronizedTime.GetServerUTCTime(0.0)));
				nkmpvpRankSeasonTemplet2 = NKCPVPManager.GetPvpAsyncSeasonTemplet(NKCPopupGauntletNewSeasonAlarm.m_PrevNKMPVPData.SeasonID);
			}
			if (nkmpvpRankSeasonTemplet != null)
			{
				NKCUtil.SetLabelText(this.m_lbTitle, string.Format(NKCStringTable.GetString("SI_DP_SEASON_POPUP_NEW_SEASON_TITLE", false), nkmpvpRankSeasonTemplet.GetSeasonStrID()));
			}
			if (pvpState != null)
			{
				int score = NKCPopupGauntletNewSeasonAlarm.m_PrevNKMPVPData.Score;
				NKMPvpRankTemplet rankTempletByRankGroupTier = NKCPVPManager.GetRankTempletByRankGroupTier(nkmpvpRankSeasonTemplet2.RankGroup, NKCPopupGauntletNewSeasonAlarm.m_PrevNKMPVPData.LeagueTierID);
				if (rankTempletByRankGroupTier != null)
				{
					this.m_NKCUILeagueTier.SetUI(rankTempletByRankGroupTier);
					this.m_lbTierText.text = rankTempletByRankGroupTier.GetLeagueName();
				}
				this.m_lbScore.text = score.ToString();
				int score2 = pvpState.Score;
				NKMPvpRankTemplet rankTempletByRankGroupTier2 = NKCPVPManager.GetRankTempletByRankGroupTier(nkmpvpRankSeasonTemplet.RankGroup, pvpState.LeagueTierID);
				if (rankTempletByRankGroupTier2 != null)
				{
					this.m_NKCUILeagueTierNew.SetUI(rankTempletByRankGroupTier2);
					this.m_lbTierTextNew.text = rankTempletByRankGroupTier2.GetLeagueName();
				}
				this.m_lbScoreNew.text = score2.ToString();
			}
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
		}

		// Token: 0x06008548 RID: 34120 RVA: 0x002D0F54 File Offset: 0x002CF154
		public void OpenForLeague()
		{
			PvpState leagueData = NKCScenManager.CurrentUserData().m_LeagueData;
			NKMLeaguePvpRankSeasonTemplet nkmleaguePvpRankSeasonTemplet = NKMLeaguePvpRankSeasonTemplet.Find(NKCUtil.FindPVPSeasonIDForLeague(NKCSynchronizedTime.GetServerUTCTime(0.0)));
			NKMLeaguePvpRankSeasonTemplet nkmleaguePvpRankSeasonTemplet2 = NKMLeaguePvpRankSeasonTemplet.Find(NKCPopupGauntletNewSeasonAlarm.m_PrevNKMPVPData.SeasonID);
			if (nkmleaguePvpRankSeasonTemplet != null)
			{
				NKCUtil.SetLabelText(this.m_lbTitle, string.Format(NKCStringTable.GetString("SI_DP_SEASON_POPUP_NEW_SEASON_TITLE", false), nkmleaguePvpRankSeasonTemplet.GetSeasonStrId()));
			}
			if (leagueData != null)
			{
				int score = NKCPopupGauntletNewSeasonAlarm.m_PrevNKMPVPData.Score;
				this.m_NKCUILeagueTier.SetUI(NKCPVPManager.GetTierIconByTier(NKM_GAME_TYPE.NGT_PVP_LEAGUE, nkmleaguePvpRankSeasonTemplet2.SeasonId, NKCPopupGauntletNewSeasonAlarm.m_PrevNKMPVPData.LeagueTierID), NKCPVPManager.GetTierNumberByTier(NKM_GAME_TYPE.NGT_PVP_LEAGUE, nkmleaguePvpRankSeasonTemplet2.SeasonId, NKCPopupGauntletNewSeasonAlarm.m_PrevNKMPVPData.LeagueTierID));
				this.m_lbTierText.text = NKCPVPManager.GetLeagueNameByTier(NKM_GAME_TYPE.NGT_PVP_LEAGUE, nkmleaguePvpRankSeasonTemplet2.SeasonId, NKCPopupGauntletNewSeasonAlarm.m_PrevNKMPVPData.LeagueTierID);
				this.m_lbScore.text = score.ToString();
				int score2 = leagueData.Score;
				this.m_NKCUILeagueTierNew.SetUI(NKCPVPManager.GetTierIconByTier(NKM_GAME_TYPE.NGT_PVP_LEAGUE, nkmleaguePvpRankSeasonTemplet.SeasonId, leagueData.LeagueTierID), NKCPVPManager.GetTierNumberByTier(NKM_GAME_TYPE.NGT_PVP_LEAGUE, nkmleaguePvpRankSeasonTemplet.SeasonId, leagueData.LeagueTierID));
				this.m_lbTierTextNew.text = NKCPVPManager.GetLeagueNameByTier(NKM_GAME_TYPE.NGT_PVP_LEAGUE, leagueData.SeasonID, leagueData.LeagueTierID);
				this.m_lbScoreNew.text = score2.ToString();
			}
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
		}

		// Token: 0x06008549 RID: 34121 RVA: 0x002D10B2 File Offset: 0x002CF2B2
		private void Update()
		{
			this.m_NKCUIOpenAnimator.Update();
		}

		// Token: 0x0600854A RID: 34122 RVA: 0x002D10BF File Offset: 0x002CF2BF
		public void CloseGauntletNewSeasonAlarm()
		{
			base.Close();
		}

		// Token: 0x0600854B RID: 34123 RVA: 0x002D10C7 File Offset: 0x002CF2C7
		public void OnCloseBtn()
		{
			base.Close();
		}

		// Token: 0x0600854C RID: 34124 RVA: 0x002D10CF File Offset: 0x002CF2CF
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x040071B2 RID: 29106
		public const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_GAUNTLET";

		// Token: 0x040071B3 RID: 29107
		public const string UI_ASSET_NAME = "NKM_UI_GAUNTLET_RANK_NEWSEASON_POPUP";

		// Token: 0x040071B4 RID: 29108
		public EventTrigger m_etBG;

		// Token: 0x040071B5 RID: 29109
		public Text m_lbTitle;

		// Token: 0x040071B6 RID: 29110
		public NKCUILeagueTier m_NKCUILeagueTier;

		// Token: 0x040071B7 RID: 29111
		public Text m_lbScore;

		// Token: 0x040071B8 RID: 29112
		public Text m_lbTierText;

		// Token: 0x040071B9 RID: 29113
		public NKCUILeagueTier m_NKCUILeagueTierNew;

		// Token: 0x040071BA RID: 29114
		public Text m_lbScoreNew;

		// Token: 0x040071BB RID: 29115
		public Text m_lbTierTextNew;

		// Token: 0x040071BC RID: 29116
		public NKCUIComStateButton m_csbtnOK;

		// Token: 0x040071BD RID: 29117
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x040071BE RID: 29118
		private static PvpState m_PrevNKMPVPData = new PvpState();
	}
}
