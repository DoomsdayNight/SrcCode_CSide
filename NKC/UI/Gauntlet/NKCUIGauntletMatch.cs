using System;
using ClientPacket.Common;
using ClientPacket.Pvp;
using NKC.UI.Guild;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B7E RID: 2942
	public class NKCUIGauntletMatch : NKCUIBase
	{
		// Token: 0x170015CB RID: 5579
		// (get) Token: 0x06008778 RID: 34680 RVA: 0x002DD4AD File Offset: 0x002DB6AD
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_GAUNTLET;
			}
		}

		// Token: 0x06008779 RID: 34681 RVA: 0x002DD4B4 File Offset: 0x002DB6B4
		public NKCUIGauntletMatch.NKC_GAUNTLET_MATCH_STATE Get_NKC_GAUNTLET_MATCH_STATE()
		{
			return this.m_NKC_GAUNTLET_MATCH_STATE;
		}

		// Token: 0x0600877A RID: 34682 RVA: 0x002DD4BC File Offset: 0x002DB6BC
		public static void SetDeckIndex(byte index)
		{
			NKCUIGauntletMatch.m_sSelectDeckIndex = index;
		}

		// Token: 0x170015CC RID: 5580
		// (get) Token: 0x0600877B RID: 34683 RVA: 0x002DD4C4 File Offset: 0x002DB6C4
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170015CD RID: 5581
		// (get) Token: 0x0600877C RID: 34684 RVA: 0x002DD4C7 File Offset: 0x002DB6C7
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x0600877D RID: 34685 RVA: 0x002DD4CA File Offset: 0x002DB6CA
		public static NKCAssetResourceData OpenInstanceAsync()
		{
			return NKCUIBase.OpenInstanceAsync<NKCUIGauntletMatch>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_MATCH");
		}

		// Token: 0x0600877E RID: 34686 RVA: 0x002DD4DB File Offset: 0x002DB6DB
		public static bool CheckInstanceLoaded(NKCAssetResourceData loadResourceData, out NKCUIGauntletMatch retVal)
		{
			return NKCUIBase.CheckInstanceLoaded<NKCUIGauntletMatch>(loadResourceData, NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontCommon), out retVal);
		}

		// Token: 0x0600877F RID: 34687 RVA: 0x002DD4EA File Offset: 0x002DB6EA
		public void CloseInstance()
		{
			NKCAssetResourceManager.CloseResource("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_MATCH");
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06008780 RID: 34688 RVA: 0x002DD508 File Offset: 0x002DB708
		public void InitUI()
		{
			if (this.m_bInit)
			{
				return;
			}
			this.m_csbtn_2P_MatchCancel.PointerClick.RemoveAllListeners();
			this.m_csbtn_2P_MatchCancel.PointerClick.AddListener(new UnityAction(this.SendMatchCancelREQ));
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_bInit = true;
		}

		// Token: 0x06008781 RID: 34689 RVA: 0x002DD560 File Offset: 0x002DB760
		private void SendMatchCancelREQ()
		{
			if (this.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_LEAGUE)
			{
				NKMPacket_LEAGUE_PVP_MATCH_CANCEL_REQ packet = new NKMPacket_LEAGUE_PVP_MATCH_CANCEL_REQ();
				NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
				return;
			}
			NKMPacket_PVP_GAME_MATCH_CANCEL_REQ packet2 = new NKMPacket_PVP_GAME_MATCH_CANCEL_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet2, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06008782 RID: 34690 RVA: 0x002DD5AC File Offset: 0x002DB7AC
		public void Open(NKM_GAME_TYPE eNKM_GAME_TYPE)
		{
			this.m_NKM_GAME_TYPE = eNKM_GAME_TYPE;
			this.m_bVSShowTime = false;
			this.m_fVSShowElapsedTime = 0f;
			if (eNKM_GAME_TYPE == NKM_GAME_TYPE.NGT_ASYNC_PVP || eNKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_STRATEGY || eNKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC || eNKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_STRATEGY_REVENGE)
			{
				this.m_NKC_GAUNTLET_MATCH_STATE = NKCUIGauntletMatch.NKC_GAUNTLET_MATCH_STATE.NGMS_SEARCH_COMPLETE;
			}
			else if (eNKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_LEAGUE)
			{
				this.m_NKC_GAUNTLET_MATCH_STATE = NKCUIGauntletMatch.NKC_GAUNTLET_MATCH_STATE.NGMS_SEARCHING;
				NKMPacket_LEAGUE_PVP_MATCH_REQ packet = new NKMPacket_LEAGUE_PVP_MATCH_REQ();
				NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
			}
			else
			{
				this.m_NKC_GAUNTLET_MATCH_STATE = NKCUIGauntletMatch.NKC_GAUNTLET_MATCH_STATE.NGMS_SEARCHING;
				NKMPacket_PVP_GAME_MATCH_REQ nkmpacket_PVP_GAME_MATCH_REQ = new NKMPacket_PVP_GAME_MATCH_REQ();
				nkmpacket_PVP_GAME_MATCH_REQ.selectDeckIndex = NKCUIGauntletMatch.m_sSelectDeckIndex;
				nkmpacket_PVP_GAME_MATCH_REQ.gameType = eNKM_GAME_TYPE;
				NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_PVP_GAME_MATCH_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
			}
			NKCUtil.SetGameobjectActive(this.m_obj_1P_DemotionAlert, false);
			NKCUtil.SetGameobjectActive(this.m_obj_2P_DemotionAlert, false);
			this.SetBasicUI();
			this.Set_1P_UI();
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			bool flag = gameOptionData != null && gameOptionData.UseVideoTexture;
			NKCUtil.SetGameobjectActive(this.m_objBGFallBack, !flag);
			base.UIOpened(true);
		}

		// Token: 0x06008783 RID: 34691 RVA: 0x002DD69C File Offset: 0x002DB89C
		public void SetTarget(AsyncPvpTarget target)
		{
			NKM_GAME_TYPE nkm_GAME_TYPE = this.m_NKM_GAME_TYPE;
			if (nkm_GAME_TYPE != NKM_GAME_TYPE.NGT_ASYNC_PVP && nkm_GAME_TYPE - NKM_GAME_TYPE.NGT_PVP_STRATEGY > 2)
			{
				return;
			}
			if (this.m_NKC_GAUNTLET_MATCH_STATE != NKCUIGauntletMatch.NKC_GAUNTLET_MATCH_STATE.NGMS_SEARCH_COMPLETE)
			{
				return;
			}
			this.Set_2P_UI(target);
			this.m_bVSShowTime = true;
			this.m_fVSShowElapsedTime = 0f;
		}

		// Token: 0x06008784 RID: 34692 RVA: 0x002DD6E0 File Offset: 0x002DB8E0
		public void SetTarget(NpcPvpTarget target)
		{
			NKM_GAME_TYPE nkm_GAME_TYPE = this.m_NKM_GAME_TYPE;
			if (nkm_GAME_TYPE != NKM_GAME_TYPE.NGT_ASYNC_PVP && nkm_GAME_TYPE - NKM_GAME_TYPE.NGT_PVP_STRATEGY > 2)
			{
				return;
			}
			if (this.m_NKC_GAUNTLET_MATCH_STATE != NKCUIGauntletMatch.NKC_GAUNTLET_MATCH_STATE.NGMS_SEARCH_COMPLETE)
			{
				return;
			}
			this.Set_2P_UI(target);
			this.m_bVSShowTime = true;
			this.m_fVSShowElapsedTime = 0f;
		}

		// Token: 0x06008785 RID: 34693 RVA: 0x002DD724 File Offset: 0x002DB924
		private void Update()
		{
			if (this.m_bVSShowTime)
			{
				this.m_fVSShowElapsedTime += Time.deltaTime;
				if (this.m_fVSShowElapsedTime >= 3f)
				{
					this.m_fVSShowElapsedTime = 0f;
					this.m_bVSShowTime = false;
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAME, true);
				}
			}
		}

		// Token: 0x06008786 RID: 34694 RVA: 0x002DD778 File Offset: 0x002DB978
		private void Set_1P_UI()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			NKMDeckIndex deckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_PVP, (int)NKCUIGauntletMatch.m_sSelectDeckIndex);
			NKMDeckData deckData = myUserData.m_ArmyData.GetDeckData(deckIndex);
			if (deckData != null)
			{
				NKMUnitData deckUnitByIndex = myUserData.m_ArmyData.GetDeckUnitByIndex(deckIndex, (int)deckData.m_LeaderIndex);
				if (deckUnitByIndex != null)
				{
					this.m_NKCUICharacterView_1P.SetCharacterIllust(deckUnitByIndex, false, false, true, 0);
					this.m_NKCUICharacterView_1P.PlayEffect(NKCUICharacterView.EffectType.VersusMaskL);
				}
			}
			this.m_lb_1P_Name.text = NKCUtilString.GetUserNickname(myUserData.m_UserNickName, false);
			this.m_lb_1P_LV.text = string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, myUserData.UserLevel);
			if (NKCGuildManager.HasGuild())
			{
				NKCUtil.SetGameobjectActive(this.m_Guild_1P, true);
				NKCUIGuildBadge guildBadge_1P = this.m_GuildBadge_1P;
				if (guildBadge_1P != null)
				{
					guildBadge_1P.SetData(NKCGuildManager.MyGuildData.badgeId, false);
				}
				NKCUtil.SetLabelText(this.m_GuildName_1P, NKCUtilString.GetUserGuildName(NKCGuildManager.MyGuildData.name, false));
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_Guild_1P, false);
			}
			PvpState pvPDataByGameType = this.GetPvPDataByGameType(this.m_NKM_GAME_TYPE);
			int num = NKCPVPManager.FindPvPSeasonID(this.m_NKM_GAME_TYPE, NKCSynchronizedTime.GetServerUTCTime(0.0));
			if (pvPDataByGameType != null)
			{
				int score;
				if (pvPDataByGameType.SeasonID != num)
				{
					score = NKCPVPManager.GetResetScore(pvPDataByGameType.SeasonID, pvPDataByGameType.Score, this.m_NKM_GAME_TYPE);
					this.m_NKCUILeagueTier_1P.SetUI(NKCPVPManager.GetTierIconByScore(this.m_NKM_GAME_TYPE, num, score), NKCPVPManager.GetTierNumberByScore(this.m_NKM_GAME_TYPE, num, score));
				}
				else
				{
					score = pvPDataByGameType.Score;
					this.m_NKCUILeagueTier_1P.SetUI(NKCPVPManager.GetTierIconByTier(this.m_NKM_GAME_TYPE, num, pvPDataByGameType.LeagueTierID), NKCPVPManager.GetTierNumberByTier(this.m_NKM_GAME_TYPE, num, pvPDataByGameType.LeagueTierID));
				}
				int winStreak = pvPDataByGameType.WinStreak;
				this.m_lb_1P_Score.text = score.ToString();
				bool flag = this.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC || this.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_STRATEGY_REVENGE;
				NKCUtil.SetGameobjectActive(this.m_lb_1P_Score, !flag);
				NKCUtil.SetGameobjectActive(this.m_obj_1P_WinStreak, winStreak >= 2 && !flag);
				if (winStreak >= 2)
				{
					this.m_lb_1P_WinStreak.text = string.Format(NKCUtilString.GET_STRING_GAUNTLET_WIN_STREAK_ONE_PARAM, winStreak);
					return;
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_lb_1P_Score, false);
				NKCUtil.SetGameobjectActive(this.m_obj_1P_WinStreak, false);
			}
		}

		// Token: 0x06008787 RID: 34695 RVA: 0x002DD9C4 File Offset: 0x002DBBC4
		private void Set_2P_UI(NKMGameData gameData)
		{
			NKM_TEAM_TYPE nkm_TEAM_TYPE;
			if (NKCScenManager.CurrentUserData() != null)
			{
				nkm_TEAM_TYPE = gameData.GetTeamType(NKCScenManager.CurrentUserData().m_UserUID);
			}
			else
			{
				nkm_TEAM_TYPE = NKM_TEAM_TYPE.NTT_A1;
			}
			NKMGameTeamData nkmgameTeamData;
			if (nkm_TEAM_TYPE == NKM_TEAM_TYPE.NTT_A1)
			{
				nkmgameTeamData = gameData.m_NKMGameTeamDataB;
			}
			else
			{
				nkmgameTeamData = gameData.m_NKMGameTeamDataA;
			}
			this.m_NKCUICharacterView_2P.SetCharacterIllust(nkmgameTeamData.GetLeaderUnitData(), false, false, true, 0);
			this.m_NKCUICharacterView_2P.PlayEffect(NKCUICharacterView.EffectType.VersusMaskR);
			this.m_lb_2P_Name.text = NKCUtilString.GetUserNickname(nkmgameTeamData.m_UserNickname, true);
			this.m_lb_2P_LV.text = string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, nkmgameTeamData.m_UserLevel);
			if (nkmgameTeamData.guildSimpleData != null && nkmgameTeamData.guildSimpleData.guildUid > 0L)
			{
				NKCUtil.SetGameobjectActive(this.m_Guild_2P, true);
				NKCUIGuildBadge guildBadge_2P = this.m_GuildBadge_2P;
				if (guildBadge_2P != null)
				{
					guildBadge_2P.SetData(nkmgameTeamData.guildSimpleData.badgeId, true);
				}
				NKCUtil.SetLabelText(this.m_GuildName_2P, NKCUtilString.GetUserGuildName(nkmgameTeamData.guildSimpleData.guildName, true));
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_Guild_2P, false);
			}
			if (this.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_RANK || this.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_LEAGUE)
			{
				this.m_lb_2P_Score.text = nkmgameTeamData.m_Score.ToString();
				NKCUtil.SetGameobjectActive(this.m_lb_2P_Score, true);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_lb_2P_Score, false);
			}
			int num = NKCPVPManager.FindPvPSeasonID(this.m_NKM_GAME_TYPE, NKCSynchronizedTime.GetServerUTCTime(0.0));
			this.m_NKCUILeagueTier_2P.SetUI(NKCPVPManager.GetTierIconByTier(this.m_NKM_GAME_TYPE, num, nkmgameTeamData.m_Tier), NKCPVPManager.GetTierNumberByTier(this.m_NKM_GAME_TYPE, num, nkmgameTeamData.m_Tier));
			NKCUtil.SetGameobjectActive(this.m_obj_2P_WinStreak, nkmgameTeamData.m_WinStreak >= 2);
			this.m_lb_2P_WinStreak.text = string.Format(NKCUtilString.GET_STRING_GAUNTLET_WIN_STREAK_ONE_PARAM, nkmgameTeamData.m_WinStreak);
			PvpState pvpState = null;
			if (this.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_RANK)
			{
				pvpState = NKCScenManager.GetScenManager().GetMyUserData().m_PvpData;
			}
			else if (this.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_LEAGUE)
			{
				pvpState = NKCScenManager.GetScenManager().GetMyUserData().m_LeagueData;
			}
			if (pvpState != null)
			{
				int finalPVPScore = NKCUtil.GetFinalPVPScore(pvpState, this.m_NKM_GAME_TYPE);
				if (this.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_RANK)
				{
					NKMPvpRankTemplet rankTempletByScore = NKCPVPManager.GetRankTempletByScore(this.m_NKM_GAME_TYPE, num, finalPVPScore);
					NKMPvpRankTemplet rankTempletByTier = NKCPVPManager.GetRankTempletByTier(this.m_NKM_GAME_TYPE, num, pvpState.LeagueTierID);
					if (NKCUtil.IsPVPDemotionAlert(rankTempletByScore, rankTempletByTier, finalPVPScore))
					{
						NKCUtil.SetGameobjectActive(this.m_obj_1P_DemotionAlert, true);
					}
				}
				else if (this.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_LEAGUE)
				{
					NKMLeaguePvpRankSeasonTemplet nkmleaguePvpRankSeasonTemplet = NKMLeaguePvpRankSeasonTemplet.Find(num);
					NKMLeaguePvpRankTemplet byScore = nkmleaguePvpRankSeasonTemplet.RankGroup.GetByScore(finalPVPScore);
					NKMLeaguePvpRankTemplet byTier = nkmleaguePvpRankSeasonTemplet.RankGroup.GetByTier(pvpState.LeagueTierID);
					if (NKCUtil.IsPVPDemotionAlert(byScore, byTier, finalPVPScore))
					{
						NKCUtil.SetGameobjectActive(this.m_obj_1P_DemotionAlert, true);
					}
				}
			}
			int score = nkmgameTeamData.m_Score;
			if (this.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_RANK)
			{
				NKMPvpRankTemplet rankTempletByScore2 = NKCPVPManager.GetRankTempletByScore(this.m_NKM_GAME_TYPE, num, score);
				NKMPvpRankTemplet rankTempletByTier2 = NKCPVPManager.GetRankTempletByTier(this.m_NKM_GAME_TYPE, num, nkmgameTeamData.m_Tier);
				if (NKCUtil.IsPVPDemotionAlert(rankTempletByScore2, rankTempletByTier2, score))
				{
					NKCUtil.SetGameobjectActive(this.m_obj_2P_DemotionAlert, true);
				}
			}
			else if (this.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_LEAGUE)
			{
				NKMLeaguePvpRankSeasonTemplet nkmleaguePvpRankSeasonTemplet2 = NKMLeaguePvpRankSeasonTemplet.Find(num);
				NKMLeaguePvpRankTemplet byScore2 = nkmleaguePvpRankSeasonTemplet2.RankGroup.GetByScore(score);
				NKMLeaguePvpRankTemplet byTier2 = nkmleaguePvpRankSeasonTemplet2.RankGroup.GetByTier(nkmgameTeamData.m_Tier);
				if (NKCUtil.IsPVPDemotionAlert(byScore2, byTier2, score))
				{
					NKCUtil.SetGameobjectActive(this.m_obj_2P_DemotionAlert, true);
				}
			}
			this.m_amtor2PChar.Play("NKM_UI_GAUNTLET_VERSUS_2P_UNIT_INTRO");
		}

		// Token: 0x06008788 RID: 34696 RVA: 0x002DDD07 File Offset: 0x002DBF07
		private void Set_2P_UI(AsyncPvpTarget target)
		{
			if (target == null)
			{
				return;
			}
			this.Set_2P_UI(target.asyncDeck, target.userNickName, target.userLevel, target.guildData, target.score, target.tier);
		}

		// Token: 0x06008789 RID: 34697 RVA: 0x002DDD37 File Offset: 0x002DBF37
		private void Set_2P_UI(NpcPvpTarget target)
		{
			if (target == null)
			{
				return;
			}
			this.Set_2P_UI(target.asyncDeck, target.userNickName, target.userLevel, null, target.score, target.tier);
		}

		// Token: 0x0600878A RID: 34698 RVA: 0x002DDD64 File Offset: 0x002DBF64
		private void Set_2P_UI(NKMAsyncDeckData asyncDeck, string userNickName, int userLv, NKMGuildSimpleData guildData, int score, int tier)
		{
			NKMAsyncUnitData nkmasyncUnitData = null;
			for (int i = 0; i < asyncDeck.units.Count; i++)
			{
				if (asyncDeck.units[i] != null && asyncDeck.units[i].unitId > 0)
				{
					nkmasyncUnitData = asyncDeck.units[i];
					break;
				}
			}
			NKMUnitData unitData = NKMDungeonManager.MakeUnitDataFromID(nkmasyncUnitData.unitId, -1L, nkmasyncUnitData.unitLevel, nkmasyncUnitData.limitBreakLevel, nkmasyncUnitData.skinId, nkmasyncUnitData.tacticLevel);
			this.m_NKCUICharacterView_2P.SetCharacterIllust(unitData, false, false, true, 0);
			this.m_NKCUICharacterView_2P.PlayEffect(NKCUICharacterView.EffectType.VersusMaskR);
			this.m_lb_2P_Name.text = NKCUtilString.GetUserNickname(userNickName, true);
			this.m_lb_2P_LV.text = string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, userLv);
			if (guildData != null && guildData.guildUid > 0L)
			{
				NKCUtil.SetGameobjectActive(this.m_Guild_2P, true);
				NKCUIGuildBadge guildBadge_2P = this.m_GuildBadge_2P;
				if (guildBadge_2P != null)
				{
					guildBadge_2P.SetData(guildData.badgeId, true);
				}
				NKCUtil.SetLabelText(this.m_GuildName_2P, NKCUtilString.GetUserGuildName(guildData.guildName, true));
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_Guild_2P, false);
			}
			NKCUtil.SetLabelText(this.m_lb_2P_Score, score.ToString());
			NKCUtil.SetGameobjectActive(this.m_lb_2P_Score, true);
			int seasonID = NKCPVPManager.FindPvPSeasonID(this.m_NKM_GAME_TYPE, NKCSynchronizedTime.GetServerUTCTime(0.0));
			this.m_NKCUILeagueTier_2P.SetUI(NKCPVPManager.GetTierIconByTier(this.m_NKM_GAME_TYPE, seasonID, tier), NKCPVPManager.GetTierNumberByTier(this.m_NKM_GAME_TYPE, seasonID, tier));
			NKCUtil.SetGameobjectActive(this.m_obj_2P_WinStreak, false);
			this.m_amtor2PChar.Play("NKM_UI_GAUNTLET_VERSUS_2P_UNIT_INTRO");
		}

		// Token: 0x0600878B RID: 34699 RVA: 0x002DDF00 File Offset: 0x002DC100
		private void SetBasicUI()
		{
			NKCUtil.SetGameobjectActive(this.m_objCenterReady, this.m_NKC_GAUNTLET_MATCH_STATE == NKCUIGauntletMatch.NKC_GAUNTLET_MATCH_STATE.NGMS_SEARCHING);
			NKCUtil.SetGameobjectActive(this.m_objCenterVS, this.m_NKC_GAUNTLET_MATCH_STATE == NKCUIGauntletMatch.NKC_GAUNTLET_MATCH_STATE.NGMS_SEARCH_COMPLETE);
			NKCUtil.SetGameobjectActive(this.m_obj_2P_Searching, this.m_NKC_GAUNTLET_MATCH_STATE == NKCUIGauntletMatch.NKC_GAUNTLET_MATCH_STATE.NGMS_SEARCHING);
			NKCUtil.SetGameobjectActive(this.m_obj_2P_Info, this.m_NKC_GAUNTLET_MATCH_STATE == NKCUIGauntletMatch.NKC_GAUNTLET_MATCH_STATE.NGMS_SEARCH_COMPLETE);
			NKCUtil.SetGameobjectActive(this.m_obj_2P_TierIcon, this.m_NKC_GAUNTLET_MATCH_STATE == NKCUIGauntletMatch.NKC_GAUNTLET_MATCH_STATE.NGMS_SEARCH_COMPLETE);
		}

		// Token: 0x0600878C RID: 34700 RVA: 0x002DDF74 File Offset: 0x002DC174
		public void OnRecv(NKMPacket_PVP_GAME_MATCH_CANCEL_ACK cPacket)
		{
			this.m_NKC_GAUNTLET_MATCH_STATE = NKCUIGauntletMatch.NKC_GAUNTLET_MATCH_STATE.NGMS_NONE;
			NKM_GAME_TYPE nkm_GAME_TYPE = this.m_NKM_GAME_TYPE;
			if (nkm_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_RANK)
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_MATCH_READY, true);
				return;
			}
			if (nkm_GAME_TYPE != NKM_GAME_TYPE.NGT_ASYNC_PVP && nkm_GAME_TYPE - NKM_GAME_TYPE.NGT_PVP_STRATEGY > 2)
			{
				return;
			}
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_ASYNC_READY, true);
		}

		// Token: 0x0600878D RID: 34701 RVA: 0x002DDFBB File Offset: 0x002DC1BB
		public void OnRecv(NKMPacket_LEAGUE_PVP_MATCH_CANCEL_ACK cPacket)
		{
			this.m_NKC_GAUNTLET_MATCH_STATE = NKCUIGauntletMatch.NKC_GAUNTLET_MATCH_STATE.NGMS_NONE;
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetReservedLobbyTab(NKC_GAUNTLET_LOBBY_TAB.NGLT_LEAGUE);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LOBBY, true);
		}

		// Token: 0x0600878E RID: 34702 RVA: 0x002DDFE1 File Offset: 0x002DC1E1
		public void OnRecv(NKMPacket_PRIVATE_PVP_ACCEPT_NOT cPacket)
		{
			this.m_NKC_GAUNTLET_MATCH_STATE = NKCUIGauntletMatch.NKC_GAUNTLET_MATCH_STATE.NGMS_SEARCH_COMPLETE;
			this.SetBasicUI();
			this.m_bVSShowTime = false;
			this.m_fVSShowElapsedTime = 0f;
		}

		// Token: 0x0600878F RID: 34703 RVA: 0x002DE002 File Offset: 0x002DC202
		public void OnRecv(NKMPacket_LEAGUE_PVP_ACCEPT_NOT cPacket)
		{
			this.m_NKC_GAUNTLET_MATCH_STATE = NKCUIGauntletMatch.NKC_GAUNTLET_MATCH_STATE.NGMS_SEARCH_COMPLETE;
			this.m_bVSShowTime = false;
			this.m_fVSShowElapsedTime = 0f;
		}

		// Token: 0x06008790 RID: 34704 RVA: 0x002DE020 File Offset: 0x002DC220
		public void OnRecv(NKMPacket_PVP_GAME_MATCH_COMPLETE_NOT cNKMPacket_PVP_GAME_MATCH_COMPLETE_NOT)
		{
			this.m_NKC_GAUNTLET_MATCH_STATE = NKCUIGauntletMatch.NKC_GAUNTLET_MATCH_STATE.NGMS_SEARCH_COMPLETE;
			NKM_GAME_TYPE nkm_GAME_TYPE = this.m_NKM_GAME_TYPE;
			if (nkm_GAME_TYPE != NKM_GAME_TYPE.NGT_ASYNC_PVP && nkm_GAME_TYPE - NKM_GAME_TYPE.NGT_PVP_STRATEGY > 2)
			{
				this.SetBasicUI();
				this.Set_2P_UI(cNKMPacket_PVP_GAME_MATCH_COMPLETE_NOT.gameData);
			}
			this.m_bVSShowTime = true;
			this.m_fVSShowElapsedTime = 0f;
		}

		// Token: 0x06008791 RID: 34705 RVA: 0x002DE06C File Offset: 0x002DC26C
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
			if (subUICameraVideoPlayer != null)
			{
				subUICameraVideoPlayer.CleanUp();
			}
			if (this.m_NKC_GAUNTLET_MATCH_STATE == NKCUIGauntletMatch.NKC_GAUNTLET_MATCH_STATE.NGMS_SEARCHING)
			{
				this.SendMatchCancelREQ();
			}
		}

		// Token: 0x06008792 RID: 34706 RVA: 0x002DE0A9 File Offset: 0x002DC2A9
		public override void OnBackButton()
		{
			if (this.m_NKC_GAUNTLET_MATCH_STATE == NKCUIGauntletMatch.NKC_GAUNTLET_MATCH_STATE.NGMS_SEARCHING)
			{
				if (NKMPopUpBox.IsOpenedWaitBox())
				{
					return;
				}
				this.SendMatchCancelREQ();
			}
		}

		// Token: 0x06008793 RID: 34707 RVA: 0x002DE0C4 File Offset: 0x002DC2C4
		private PvpState GetPvPDataByGameType(NKM_GAME_TYPE gameType)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return null;
			}
			if (gameType <= NKM_GAME_TYPE.NGT_ASYNC_PVP)
			{
				if (gameType == NKM_GAME_TYPE.NGT_PVP_RANK)
				{
					return myUserData.m_PvpData;
				}
				if (gameType != NKM_GAME_TYPE.NGT_ASYNC_PVP)
				{
					goto IL_43;
				}
			}
			else
			{
				if (gameType == NKM_GAME_TYPE.NGT_PVP_LEAGUE)
				{
					return myUserData.m_LeagueData;
				}
				if (gameType - NKM_GAME_TYPE.NGT_PVP_STRATEGY > 2)
				{
					goto IL_43;
				}
			}
			return myUserData.m_AsyncData;
			IL_43:
			return null;
		}

		// Token: 0x06008794 RID: 34708 RVA: 0x002DE115 File Offset: 0x002DC315
		public bool GetVSShowTime()
		{
			return this.m_bVSShowTime;
		}

		// Token: 0x040073E3 RID: 29667
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_GAUNTLET";

		// Token: 0x040073E4 RID: 29668
		private const string UI_ASSET_NAME = "NKM_UI_GAUNTLET_MATCH";

		// Token: 0x040073E5 RID: 29669
		private bool m_bInit;

		// Token: 0x040073E6 RID: 29670
		public NKCUICharacterView m_NKCUICharacterView_1P;

		// Token: 0x040073E7 RID: 29671
		public NKCUILeagueTier m_NKCUILeagueTier_1P;

		// Token: 0x040073E8 RID: 29672
		public Text m_lb_1P_Score;

		// Token: 0x040073E9 RID: 29673
		public Text m_lb_1P_LV;

		// Token: 0x040073EA RID: 29674
		public Text m_lb_1P_Name;

		// Token: 0x040073EB RID: 29675
		public GameObject m_obj_1P_WinStreak;

		// Token: 0x040073EC RID: 29676
		public Text m_lb_1P_WinStreak;

		// Token: 0x040073ED RID: 29677
		public GameObject m_obj_1P_DemotionAlert;

		// Token: 0x040073EE RID: 29678
		public GameObject m_Guild_1P;

		// Token: 0x040073EF RID: 29679
		public NKCUIGuildBadge m_GuildBadge_1P;

		// Token: 0x040073F0 RID: 29680
		public Text m_GuildName_1P;

		// Token: 0x040073F1 RID: 29681
		public GameObject m_obj_2P_Searching;

		// Token: 0x040073F2 RID: 29682
		public GameObject m_obj_2P_Info;

		// Token: 0x040073F3 RID: 29683
		public GameObject m_obj_2P_TierIcon;

		// Token: 0x040073F4 RID: 29684
		public NKCUIComStateButton m_csbtn_2P_MatchCancel;

		// Token: 0x040073F5 RID: 29685
		public NKCUICharacterView m_NKCUICharacterView_2P;

		// Token: 0x040073F6 RID: 29686
		public NKCUILeagueTier m_NKCUILeagueTier_2P;

		// Token: 0x040073F7 RID: 29687
		public Text m_lb_2P_Score;

		// Token: 0x040073F8 RID: 29688
		public Text m_lb_2P_LV;

		// Token: 0x040073F9 RID: 29689
		public Text m_lb_2P_Name;

		// Token: 0x040073FA RID: 29690
		public GameObject m_obj_2P_WinStreak;

		// Token: 0x040073FB RID: 29691
		public Text m_lb_2P_WinStreak;

		// Token: 0x040073FC RID: 29692
		public GameObject m_obj_2P_DemotionAlert;

		// Token: 0x040073FD RID: 29693
		public Animator m_amtor2PChar;

		// Token: 0x040073FE RID: 29694
		public GameObject m_Guild_2P;

		// Token: 0x040073FF RID: 29695
		public NKCUIGuildBadge m_GuildBadge_2P;

		// Token: 0x04007400 RID: 29696
		public Text m_GuildName_2P;

		// Token: 0x04007401 RID: 29697
		public GameObject m_objCenterReady;

		// Token: 0x04007402 RID: 29698
		public GameObject m_objCenterVS;

		// Token: 0x04007403 RID: 29699
		[Header("Fallback BG")]
		public GameObject m_objBGFallBack;

		// Token: 0x04007404 RID: 29700
		private NKCUIGauntletMatch.NKC_GAUNTLET_MATCH_STATE m_NKC_GAUNTLET_MATCH_STATE;

		// Token: 0x04007405 RID: 29701
		private static byte m_sSelectDeckIndex;

		// Token: 0x04007406 RID: 29702
		private const float VS_SHOW_TIME = 3f;

		// Token: 0x04007407 RID: 29703
		private float m_fVSShowElapsedTime;

		// Token: 0x04007408 RID: 29704
		private bool m_bVSShowTime;

		// Token: 0x04007409 RID: 29705
		private NKM_GAME_TYPE m_NKM_GAME_TYPE = NKM_GAME_TYPE.NGT_PVP_RANK;

		// Token: 0x0400740A RID: 29706
		private const int WIN_STREAK_SHOW_COUNT = 2;

		// Token: 0x02001923 RID: 6435
		public enum NKC_GAUNTLET_MATCH_STATE
		{
			// Token: 0x0400AAB6 RID: 43702
			NGMS_NONE,
			// Token: 0x0400AAB7 RID: 43703
			NGMS_SEARCHING,
			// Token: 0x0400AAB8 RID: 43704
			NGMS_SEARCH_COMPLETE
		}
	}
}
