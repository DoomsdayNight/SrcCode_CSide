using System;
using NKC.UI.Guide;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B7D RID: 2941
	public class NKCUIGauntletLobbyRightSideRank : NKCUIGauntletLobbyRightSideBase
	{
		// Token: 0x0600876B RID: 34667 RVA: 0x002DCF20 File Offset: 0x002DB120
		public override void InitUI()
		{
			base.InitUI();
			if (this.m_csbtnRankMatchReady != null)
			{
				this.m_csbtnRankMatchReady.PointerClick.RemoveAllListeners();
				this.m_csbtnRankMatchReady.PointerClick.AddListener(new UnityAction(this.OnClickRankMatchReady));
			}
			if (this.m_csbtnRankMatchReadyDisable != null)
			{
				this.m_csbtnRankMatchReadyDisable.PointerClick.RemoveAllListeners();
				this.m_csbtnRankMatchReadyDisable.PointerClick.AddListener(new UnityAction(this.OnClickRankMatchReady));
			}
			if (this.m_csbtnRankPVPPoint != null)
			{
				this.m_csbtnRankPVPPoint.PointerClick.RemoveAllListeners();
				this.m_csbtnRankPVPPoint.PointerClick.AddListener(delegate()
				{
					NKCUIPopUpGuide.Instance.Open("ARTICLE_PVP_RANK", 1);
				});
			}
			bool flag = NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_CASTING_BAN);
			NKCUtil.SetGameobjectActive(this.m_csbtnBanListOld, !flag);
			NKCUtil.SetGameobjectActive(this.m_csbtnRankCastingBan, flag);
			NKCUtil.SetGameobjectActive(this.m_csbtnBanList, flag);
			NKCUtil.SetBindFunction(this.m_csbtnBanListOld, new UnityAction(this.OnClickBanList));
			NKCUtil.SetBindFunction(this.m_csbtnBanList, new UnityAction(this.OnClickBanList));
			NKCUtil.SetBindFunction(this.m_csbtnRankCastingBan, new UnityAction(this.OnClickRankCastingBan));
		}

		// Token: 0x0600876C RID: 34668 RVA: 0x002DD068 File Offset: 0x002DB268
		private void OnClickRankMatchReady()
		{
			if (this.m_csbtnRankMatchReady.gameObject.activeSelf)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_MATCH_READY().SetReservedGameType(NKM_GAME_TYPE.NGT_PVP_RANK);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_MATCH_READY, true);
				return;
			}
			int seasonID = NKCUtil.FindPVPSeasonIDForRank(NKCSynchronizedTime.GetServerUTCTime(0.0));
			int weekIDForRank = NKCPVPManager.GetWeekIDForRank(NKCSynchronizedTime.GetServerUTCTime(0.0), seasonID);
			NKM_ERROR_CODE nkm_ERROR_CODE = NKCPVPManager.CanPlayPVPRankGame(NKCScenManager.CurrentUserData(), seasonID, weekIDForRank, NKCSynchronizedTime.GetServerUTCTime(0.0));
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(nkm_ERROR_CODE, null, "");
			}
		}

		// Token: 0x0600876D RID: 34669 RVA: 0x002DD0F8 File Offset: 0x002DB2F8
		private void OnClickRankCastingBan()
		{
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_CASTING_BAN) && NKCUIGauntletLobbyRightSideRank.CheckCanPlayPVPRankGame())
			{
				NKCPopupGauntletCastingBan.Instance.Open();
				return;
			}
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_GAUNTLET_SELECT_IMPOSSIBLE, null, "");
		}

		// Token: 0x0600876E RID: 34670 RVA: 0x002DD12C File Offset: 0x002DB32C
		public void UpdateRankPVPPointUI()
		{
			NKMInventoryData inventoryData = NKCScenManager.CurrentUserData().m_InventoryData;
			long countMiscItem = inventoryData.GetCountMiscItem(301);
			int charge_POINT_MAX_COUNT = NKMPvpCommonConst.Instance.CHARGE_POINT_MAX_COUNT;
			long countMiscItem2 = inventoryData.GetCountMiscItem(6);
			int charge_POINT_ONE_STEP = NKMPvpCommonConst.Instance.CHARGE_POINT_ONE_STEP;
			int num = 0;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				NKCCompanyBuff.IncreaseChargePointOfPvpWithBonusRatio(nkmuserData.m_companyBuffDataList, ref charge_POINT_ONE_STEP, out num);
			}
			if (countMiscItem > 0L)
			{
				NKCUtil.SetGameobjectActive(this.m_objPVPDoublePoint, true);
				NKCUtil.SetLabelText(this.m_lbPVPDoublePoint, countMiscItem.ToString());
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objPVPDoublePoint, false);
			}
			NKCUtil.SetLabelText(this.m_lbRemainPVPPoint, string.Format("{0}<color=#5d77a3>/{1}</color>", countMiscItem2, charge_POINT_MAX_COUNT));
			if (NKCScenManager.GetScenManager().GetMyUserData().m_PvpData == null)
			{
				return;
			}
			if (countMiscItem2 < (long)charge_POINT_MAX_COUNT)
			{
				NKCUtil.SetGameobjectActive(this.m_objRemainPVPPointPlusTime, true);
				if (num > 0)
				{
					NKCUtil.SetLabelText(this.m_lbPlusPVPPoint, string.Format("<color=#00baff>+{0}</color>", charge_POINT_ONE_STEP));
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbPlusPVPPoint, string.Format("+{0}", charge_POINT_ONE_STEP));
				}
				DateTime dateTime = new DateTime(NKCPVPManager.GetLastUpdateChargePointTicks());
				DateTime serverUTCTime = NKCSynchronizedTime.GetServerUTCTime(0.0);
				TimeSpan timeSpan = new DateTime(dateTime.Ticks + NKMPvpCommonConst.Instance.CHARGE_POINT_REFRESH_INTERVAL_TICKS) - serverUTCTime;
				if (timeSpan.TotalHours >= 1.0)
				{
					NKCUtil.SetLabelText(this.m_lbRemainPVPPointPlusTime, string.Format("{0}:{1:00}:{2:00}", (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds));
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbRemainPVPPointPlusTime, string.Format("{0}:{1:00}", timeSpan.Minutes, timeSpan.Seconds));
				}
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().ProcessPVPPointCharge(6);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objRemainPVPPointPlusTime, false);
		}

		// Token: 0x0600876F RID: 34671 RVA: 0x002DD314 File Offset: 0x002DB514
		public void UpdateReadyRankButtonUI()
		{
			bool flag = NKCUIGauntletLobbyRightSideRank.CheckCanPlayPVPRankGame();
			NKCUtil.SetGameobjectActive(this.m_csbtnRankMatchReady, flag);
			NKCUtil.SetGameobjectActive(this.m_csbtnRankMatchReadyDisable, !flag);
			if (this.m_csbtnRankCastingBan.gameObject.activeSelf)
			{
				NKCUtil.SetImageSprite(this.m_imgRankCastingBan, NKCUtil.GetButtonSprite(flag ? NKCUtil.ButtonColor.BC_BLUE : NKCUtil.ButtonColor.BC_GRAY), false);
			}
		}

		// Token: 0x06008770 RID: 34672 RVA: 0x002DD36C File Offset: 0x002DB56C
		public void UpdateCastingBanVoteState()
		{
			NKCUtil.SetGameobjectActive(this.m_objCastingBanRedDot, !NKCBanManager.IsCastingBanVoted());
		}

		// Token: 0x06008771 RID: 34673 RVA: 0x002DD381 File Offset: 0x002DB581
		private void OnClickBanList()
		{
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_CASTING_BAN))
			{
				NKCPopupGauntletBanListV2.Instance.Open();
				return;
			}
			NKCPopupGauntletBanList.Instance.Open();
		}

		// Token: 0x06008772 RID: 34674 RVA: 0x002DD3A0 File Offset: 0x002DB5A0
		public static bool CheckCanPlayPVPRankGame()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return false;
			}
			int seasonID = NKCUtil.FindPVPSeasonIDForRank(NKCSynchronizedTime.GetServerUTCTime(0.0));
			int weekIDForRank = NKCPVPManager.GetWeekIDForRank(NKCSynchronizedTime.GetServerUTCTime(0.0), seasonID);
			return NKCPVPManager.CanPlayPVPRankGame(myUserData, seasonID, weekIDForRank, NKCSynchronizedTime.GetServerUTCTime(0.0)) == NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06008773 RID: 34675 RVA: 0x002DD404 File Offset: 0x002DB604
		protected override PvpState GetPvpData()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return null;
			}
			return myUserData.m_PvpData;
		}

		// Token: 0x06008774 RID: 34676 RVA: 0x002DD427 File Offset: 0x002DB627
		protected override NKMPvpRankSeasonTemplet GetSeasonTemplet()
		{
			return NKCPVPManager.GetPvpRankSeasonTemplet(NKCUtil.FindPVPSeasonIDForRank(NKCSynchronizedTime.GetServerUTCTime(0.0)));
		}

		// Token: 0x06008775 RID: 34677 RVA: 0x002DD444 File Offset: 0x002DB644
		protected override string GetLeagueNameByScore(int seasonID, PvpState pvpData)
		{
			NKMPvpRankTemplet pvpRankTempletByScore = NKCPVPManager.GetPvpRankTempletByScore(seasonID, NKCUtil.GetScoreBySeason(seasonID, pvpData.SeasonID, pvpData.Score, NKM_GAME_TYPE.NGT_PVP_RANK));
			if (pvpRankTempletByScore != null)
			{
				return pvpRankTempletByScore.GetLeagueName();
			}
			return "";
		}

		// Token: 0x06008776 RID: 34678 RVA: 0x002DD47C File Offset: 0x002DB67C
		protected override string GetLeagueNameByTier(int seasonID, PvpState pvpData)
		{
			NKMPvpRankTemplet pvpRankTempletByTier = NKCPVPManager.GetPvpRankTempletByTier(seasonID, pvpData.LeagueTierID);
			if (pvpRankTempletByTier != null)
			{
				return pvpRankTempletByTier.GetLeagueName();
			}
			return "";
		}

		// Token: 0x040073D4 RID: 29652
		[Header("랭크전 전용")]
		public NKCUIComStateButton m_csbtnRankMatchReady;

		// Token: 0x040073D5 RID: 29653
		public NKCUIComStateButton m_csbtnRankMatchReadyDisable;

		// Token: 0x040073D6 RID: 29654
		public NKCUIComStateButton m_csbtnRankPVPPoint;

		// Token: 0x040073D7 RID: 29655
		public NKCUIComStateButton m_csbtnBanListOld;

		// Token: 0x040073D8 RID: 29656
		public NKCUIComStateButton m_csbtnBanList;

		// Token: 0x040073D9 RID: 29657
		public NKCUIComStateButton m_csbtnRankCastingBan;

		// Token: 0x040073DA RID: 29658
		public Image m_imgRankCastingBan;

		// Token: 0x040073DB RID: 29659
		[Header("포인트")]
		public GameObject m_objPVPDoublePoint;

		// Token: 0x040073DC RID: 29660
		public Text m_lbPVPDoublePoint;

		// Token: 0x040073DD RID: 29661
		public GameObject m_objPVPPoint;

		// Token: 0x040073DE RID: 29662
		public Text m_lbRemainPVPPoint;

		// Token: 0x040073DF RID: 29663
		public GameObject m_objRemainPVPPointPlusTime;

		// Token: 0x040073E0 RID: 29664
		public Text m_lbPlusPVPPoint;

		// Token: 0x040073E1 RID: 29665
		public Text m_lbRemainPVPPointPlusTime;

		// Token: 0x040073E2 RID: 29666
		public GameObject m_objCastingBanRedDot;
	}
}
