using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B7A RID: 2938
	public class NKCUIGauntletLobbyRightSideAsync : NKCUIGauntletLobbyRightSideBase
	{
		// Token: 0x06008749 RID: 34633 RVA: 0x002DC110 File Offset: 0x002DA310
		public override void InitUI()
		{
			base.InitUI();
			this.m_csbtnAsyncDefenseDeck.PointerClick.RemoveAllListeners();
			this.m_csbtnAsyncDefenseDeck.PointerClick.AddListener(new UnityAction(this.OnClickAsyncDefenseDeck));
		}

		// Token: 0x0600874A RID: 34634 RVA: 0x002DC144 File Offset: 0x002DA344
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
			if (this.GetPvpData() == null)
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

		// Token: 0x0600874B RID: 34635 RVA: 0x002DC322 File Offset: 0x002DA522
		public void SetCallback(NKCUIDeckViewer.DeckViewerOption.OnBackButton dCallback)
		{
			this.m_dOnBackButton = dCallback;
		}

		// Token: 0x0600874C RID: 34636 RVA: 0x002DC32C File Offset: 0x002DA52C
		private void OnClickAsyncDefenseDeck()
		{
			NKCUIDeckViewer.DeckViewerOption options = default(NKCUIDeckViewer.DeckViewerOption);
			options.MenuName = NKCUtilString.GET_STRING_GAUNTLET;
			options.eDeckviewerMode = NKCUIDeckViewer.DeckViewerMode.AsyncPvpDefenseDeck;
			options.dOnDeckSideButtonConfirmForAsync = new NKCUIDeckViewer.DeckViewerOption.OnDeckSideButtonConfirmForAsync(this.SelectDefenseDeck);
			options.DeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_PVP_DEFENCE, 0);
			options.SelectLeaderUnitOnOpen = true;
			options.bEnableDefaultBackground = false;
			options.bUpsideMenuHomeButton = false;
			options.upsideMenuShowResourceList = new List<int>
			{
				13,
				5,
				101
			};
			options.StageBattleStrID = string.Empty;
			options.dOnBackButton = this.m_dOnBackButton;
			options.bUseAsyncDeckSetting = true;
			NKCUIDeckViewer.Instance.Open(options, true);
		}

		// Token: 0x0600874D RID: 34637 RVA: 0x002DC3E0 File Offset: 0x002DA5E0
		private void SelectDefenseDeck(NKMDeckIndex deckIndex, NKMDeckData originalDeck)
		{
			NKMDeckData deckData = NKCScenManager.CurrentUserData().m_ArmyData.GetDeckData(deckIndex);
			if (this.HasDeckChanged(deckData, originalDeck))
			{
				NKCPacketSender.Send_NKMPacket_UPDATE_DEFENCE_DECK_REQ(deckData);
			}
			NKCUIDeckViewer.DeckViewerOption.OnBackButton dOnBackButton = this.m_dOnBackButton;
			if (dOnBackButton == null)
			{
				return;
			}
			dOnBackButton();
		}

		// Token: 0x0600874E RID: 34638 RVA: 0x002DC420 File Offset: 0x002DA620
		private bool HasDeckChanged(NKMDeckData newDeck, NKMDeckData originalDeck)
		{
			if (originalDeck == null)
			{
				return false;
			}
			if (newDeck == null)
			{
				return false;
			}
			if (originalDeck.m_ShipUID != newDeck.m_ShipUID)
			{
				return true;
			}
			if (originalDeck.m_LeaderIndex != newDeck.m_LeaderIndex)
			{
				return true;
			}
			if (originalDeck.m_OperatorUID != newDeck.m_OperatorUID)
			{
				return true;
			}
			for (int i = 0; i < newDeck.m_listDeckUnitUID.Count; i++)
			{
				if (i < originalDeck.m_listDeckUnitUID.Count && newDeck.m_listDeckUnitUID[i] != originalDeck.m_listDeckUnitUID[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600874F RID: 34639 RVA: 0x002DC4A8 File Offset: 0x002DA6A8
		protected override PvpState GetPvpData()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return null;
			}
			return myUserData.m_AsyncData;
		}

		// Token: 0x06008750 RID: 34640 RVA: 0x002DC4CB File Offset: 0x002DA6CB
		protected override NKMPvpRankSeasonTemplet GetSeasonTemplet()
		{
			return NKCPVPManager.GetPvpAsyncSeasonTemplet(NKCUtil.FindPVPSeasonIDForAsync(NKCSynchronizedTime.GetServerUTCTime(0.0)));
		}

		// Token: 0x06008751 RID: 34641 RVA: 0x002DC4E8 File Offset: 0x002DA6E8
		protected override string GetLeagueNameByScore(int seasonID, PvpState pvpData)
		{
			NKMPvpRankTemplet asyncPvpRankTempletByScore = NKCPVPManager.GetAsyncPvpRankTempletByScore(seasonID, NKCUtil.GetScoreBySeason(seasonID, pvpData.SeasonID, pvpData.Score, NKM_GAME_TYPE.NGT_ASYNC_PVP));
			if (asyncPvpRankTempletByScore != null)
			{
				return asyncPvpRankTempletByScore.GetLeagueName();
			}
			return "";
		}

		// Token: 0x06008752 RID: 34642 RVA: 0x002DC520 File Offset: 0x002DA720
		protected override string GetLeagueNameByTier(int seasonID, PvpState pvpData)
		{
			NKMPvpRankTemplet asyncPvpRankTempletByTier = NKCPVPManager.GetAsyncPvpRankTempletByTier(seasonID, pvpData.LeagueTierID);
			if (asyncPvpRankTempletByTier != null)
			{
				return asyncPvpRankTempletByTier.GetLeagueName();
			}
			return "";
		}

		// Token: 0x040073AC RID: 29612
		[Header("포인트")]
		public GameObject m_objPVPDoublePoint;

		// Token: 0x040073AD RID: 29613
		public Text m_lbPVPDoublePoint;

		// Token: 0x040073AE RID: 29614
		public GameObject m_objPVPPoint;

		// Token: 0x040073AF RID: 29615
		public Text m_lbRemainPVPPoint;

		// Token: 0x040073B0 RID: 29616
		public GameObject m_objRemainPVPPointPlusTime;

		// Token: 0x040073B1 RID: 29617
		public Text m_lbPlusPVPPoint;

		// Token: 0x040073B2 RID: 29618
		public Text m_lbRemainPVPPointPlusTime;

		// Token: 0x040073B3 RID: 29619
		[Header("방어덱")]
		public NKCUIComStateButton m_csbtnAsyncDefenseDeck;

		// Token: 0x040073B4 RID: 29620
		private NKCUIDeckViewer.DeckViewerOption.OnBackButton m_dOnBackButton;
	}
}
