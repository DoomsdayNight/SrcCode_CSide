using System;
using NKC.PacketHandler;
using NKC.UI.Guide;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B7C RID: 2940
	public class NKCUIGauntletLobbyRightSideLeague : NKCUIGauntletLobbyRightSideBase
	{
		// Token: 0x06008760 RID: 34656 RVA: 0x002DC9EC File Offset: 0x002DABEC
		public override void InitUI()
		{
			base.InitUI();
			this.m_csbtnRankMatchReady.PointerClick.RemoveAllListeners();
			this.m_csbtnRankMatchReady.PointerClick.AddListener(new UnityAction(this.OnClickLeagueMatchFind));
			this.m_csbtnRankMatchReadyDisable.PointerClick.RemoveAllListeners();
			this.m_csbtnRankMatchReadyDisable.PointerClick.AddListener(new UnityAction(this.OnClickLeagueMatchFind));
			this.m_csbtnRankPVPPoint.PointerClick.RemoveAllListeners();
			this.m_csbtnRankPVPPoint.PointerClick.AddListener(delegate()
			{
				NKCUIPopUpGuide.Instance.Open("ARTICLE_PVP_RANK", 1);
			});
		}

		// Token: 0x06008761 RID: 34657 RVA: 0x002DCA98 File Offset: 0x002DAC98
		public void UpdatePVPPointUI()
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
			if (NKCScenManager.GetScenManager().GetMyUserData().m_LeagueData == null)
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

		// Token: 0x06008762 RID: 34658 RVA: 0x002DCC80 File Offset: 0x002DAE80
		public override void UpdateNowSeasonPVPInfoUI(NKM_GAME_TYPE gameType)
		{
			base.UpdateNowSeasonPVPInfoUI(gameType);
			if (NKCScenManager.GetScenManager().GetMyUserData() == null)
			{
				return;
			}
			PvpState leagueData = NKCScenManager.GetScenManager().GetMyUserData().m_LeagueData;
			int num = NKCUtil.FindPVPSeasonIDForLeague(NKCSynchronizedTime.GetServerUTCTime(0.0));
			if (leagueData == null)
			{
				return;
			}
			if (leagueData.SeasonID != num)
			{
				NKCUtil.SetLabelText(this.m_lbLoseCount, "-");
				NKCUtil.SetLabelText(this.m_lbWinRate, "-");
				return;
			}
			NKCUtil.SetLabelText(this.m_lbLoseCount, leagueData.LoseCount.ToString());
			if (leagueData.WinCount + leagueData.LoseCount <= 0)
			{
				NKCUtil.SetLabelText(this.m_lbWinRate, "0%");
				return;
			}
			NKCUtil.SetLabelText(this.m_lbWinRate, string.Format("{0}%", leagueData.WinCount * 100 / (leagueData.WinCount + leagueData.LoseCount)));
		}

		// Token: 0x06008763 RID: 34659 RVA: 0x002DCD5C File Offset: 0x002DAF5C
		private bool CheckCanPlayPVPGame()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return false;
			}
			int seasonID = NKCUtil.FindPVPSeasonIDForLeague(NKCSynchronizedTime.GetServerUTCTime(0.0));
			return NKCPVPManager.CanPlayPVPLeagueGame(myUserData, seasonID, NKCSynchronizedTime.GetServerUTCTime(0.0)) == NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06008764 RID: 34660 RVA: 0x002DCDA8 File Offset: 0x002DAFA8
		public void UpdateReadyButtonUI()
		{
			bool flag = this.CheckCanPlayPVPGame();
			NKCUtil.SetGameobjectActive(this.m_csbtnRankMatchReady, flag);
			NKCUtil.SetGameobjectActive(this.m_csbtnRankMatchReadyDisable, !flag);
		}

		// Token: 0x06008765 RID: 34661 RVA: 0x002DCDD8 File Offset: 0x002DAFD8
		private void OnClickLeagueMatchFind()
		{
			if (this.m_csbtnRankMatchReady.gameObject.activeSelf)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_MATCH().SetReservedGameType(NKM_GAME_TYPE.NGT_PVP_LEAGUE);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_MATCH, true);
				return;
			}
			int seasonID = NKCUtil.FindPVPSeasonIDForLeague(NKCSynchronizedTime.GetServerUTCTime(0.0));
			NKM_ERROR_CODE nkm_ERROR_CODE = NKCPVPManager.CanPlayPVPLeagueGame(NKCScenManager.CurrentUserData(), seasonID, NKCSynchronizedTime.GetServerUTCTime(0.0));
			if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_DRAFT_PVP_NOT_ENOUGH_UNIT_COUNT || nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_DRAFT_PVP_NOT_ENOUGH_SHIP_COUNT)
			{
				NKCPopupGauntletLeagueEnterCondition.Instance.Open();
				return;
			}
			if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_DRAFT_PVP_INVALID_TIME)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, string.Format(NKCStringTable.GetString("SI_DP_GAUNTLET_LEAGUE_START_REQ_TIME_POPUP_DESC", false), NKCPVPManager.GetLeagueOpenDaysString(), NKCPVPManager.GetLeagueOpenTimeString()), null, "");
				return;
			}
			NKCPacketHandlers.Check_NKM_ERROR_CODE(nkm_ERROR_CODE, true, null, int.MinValue);
		}

		// Token: 0x06008766 RID: 34662 RVA: 0x002DCEA0 File Offset: 0x002DB0A0
		protected override PvpState GetPvpData()
		{
			return NKCScenManager.CurrentUserData().m_LeagueData;
		}

		// Token: 0x06008767 RID: 34663 RVA: 0x002DCEAC File Offset: 0x002DB0AC
		protected override NKMPvpRankSeasonTemplet GetSeasonTemplet()
		{
			return null;
		}

		// Token: 0x06008768 RID: 34664 RVA: 0x002DCEB0 File Offset: 0x002DB0B0
		protected override string GetLeagueNameByScore(int seasonID, PvpState pvpData)
		{
			NKMLeaguePvpRankGroupTemplet nkmleaguePvpRankGroupTemplet = NKMLeaguePvpRankGroupTemplet.Find(seasonID);
			if (nkmleaguePvpRankGroupTemplet != null)
			{
				return NKCStringTable.GetString(nkmleaguePvpRankGroupTemplet.GetByScore(pvpData.Score).LeagueName, false);
			}
			return "";
		}

		// Token: 0x06008769 RID: 34665 RVA: 0x002DCEE4 File Offset: 0x002DB0E4
		protected override string GetLeagueNameByTier(int seasonID, PvpState pvpData)
		{
			NKMLeaguePvpRankGroupTemplet nkmleaguePvpRankGroupTemplet = NKMLeaguePvpRankGroupTemplet.Find(seasonID);
			if (nkmleaguePvpRankGroupTemplet != null)
			{
				return NKCStringTable.GetString(nkmleaguePvpRankGroupTemplet.GetByTier(pvpData.LeagueTierID).LeagueName, false);
			}
			return "";
		}

		// Token: 0x040073C8 RID: 29640
		[Header("리그전 전용")]
		public NKCUIComStateButton m_csbtnRankMatchReady;

		// Token: 0x040073C9 RID: 29641
		public NKCUIComStateButton m_csbtnRankMatchReadyDisable;

		// Token: 0x040073CA RID: 29642
		public NKCUIComStateButton m_csbtnRankPVPPoint;

		// Token: 0x040073CB RID: 29643
		public GameObject m_objPVPDoublePoint;

		// Token: 0x040073CC RID: 29644
		public Text m_lbPVPDoublePoint;

		// Token: 0x040073CD RID: 29645
		public GameObject m_objPVPPoint;

		// Token: 0x040073CE RID: 29646
		public Text m_lbRemainPVPPoint;

		// Token: 0x040073CF RID: 29647
		public GameObject m_objRemainPVPPointPlusTime;

		// Token: 0x040073D0 RID: 29648
		public Text m_lbPlusPVPPoint;

		// Token: 0x040073D1 RID: 29649
		public Text m_lbRemainPVPPointPlusTime;

		// Token: 0x040073D2 RID: 29650
		public Text m_lbLoseCount;

		// Token: 0x040073D3 RID: 29651
		public Text m_lbWinRate;
	}
}
