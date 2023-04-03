using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B66 RID: 2918
	public class NKCPopupGauntletOutgameReward : NKCUIBase
	{
		// Token: 0x170015A8 RID: 5544
		// (get) Token: 0x06008550 RID: 34128 RVA: 0x002D10F9 File Offset: 0x002CF2F9
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170015A9 RID: 5545
		// (get) Token: 0x06008551 RID: 34129 RVA: 0x002D10FC File Offset: 0x002CF2FC
		public override string MenuName
		{
			get
			{
				return "PopupGauntletOutgameReward";
			}
		}

		// Token: 0x06008552 RID: 34130 RVA: 0x002D1103 File Offset: 0x002CF303
		public static void SetNKMPVPData(PvpState cNKMPVPData)
		{
			NKCPopupGauntletOutgameReward.m_NKMPVPData = cNKMPVPData;
		}

		// Token: 0x06008553 RID: 34131 RVA: 0x002D110C File Offset: 0x002CF30C
		public void InitUI()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			NKCUtil.SetBindFunction(this.m_csbtnOK, new UnityAction(this.OnClickOK));
			NKCUtil.SetHotkey(this.m_csbtnOK, HotkeyEventType.Confirm, null, false);
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
		}

		// Token: 0x06008554 RID: 34132 RVA: 0x002D115B File Offset: 0x002CF35B
		private void OnClickOK()
		{
			base.Close();
		}

		// Token: 0x06008555 RID: 34133 RVA: 0x002D1164 File Offset: 0x002CF364
		public void Open(bool bWeeklyReward, NKMRewardData cNKMRewardData, bool bRank, bool bChangeScore = false)
		{
			this.m_bSeason = !bWeeklyReward;
			this.m_bRank = bRank;
			this.m_bLeague = false;
			if (NKCPopupGauntletOutgameReward.m_NKMPVPData != null)
			{
				int score = NKCPopupGauntletOutgameReward.m_NKMPVPData.Score;
				NKMPvpRankTemplet nkmpvpRankTemplet;
				if (bRank)
				{
					nkmpvpRankTemplet = NKCPVPManager.GetPvpRankTempletByTier(NKCPopupGauntletOutgameReward.m_NKMPVPData.SeasonID, NKCPopupGauntletOutgameReward.m_NKMPVPData.LeagueTierID);
				}
				else
				{
					nkmpvpRankTemplet = NKCPVPManager.GetAsyncPvpRankTempletByTier(NKCPopupGauntletOutgameReward.m_NKMPVPData.SeasonID, NKCPopupGauntletOutgameReward.m_NKMPVPData.LeagueTierID);
				}
				if (nkmpvpRankTemplet != null)
				{
					this.m_NKCUILeagueTier.SetUI(nkmpvpRankTemplet);
					this.m_lbTierText.text = nkmpvpRankTemplet.GetLeagueName();
				}
				this.m_lbScore.text = score.ToString();
			}
			if (bWeeklyReward)
			{
				NKCUtil.SetLabelText(this.m_lbTitle, NKCUtilString.GET_STRING_GAUNTLET_WEEKLY_REWARD);
				NKMPvpRankSeasonTemplet nkmpvpRankSeasonTemplet;
				if (bRank)
				{
					nkmpvpRankSeasonTemplet = NKCPVPManager.GetPvpRankSeasonTemplet(NKCUtil.FindPVPSeasonIDForRank(NKCSynchronizedTime.GetServerUTCTime(0.0)));
				}
				else
				{
					nkmpvpRankSeasonTemplet = NKCPVPManager.GetPvpAsyncSeasonTemplet(NKCUtil.FindPVPSeasonIDForAsync(NKCSynchronizedTime.GetServerUTCTime(0.0)));
				}
				if (nkmpvpRankSeasonTemplet != null)
				{
					if (!NKCSynchronizedTime.IsFinished(nkmpvpRankSeasonTemplet.EndDate))
					{
						this.m_lbSeasonRemainTime.text = string.Format(NKCStringTable.GetString("SI_DP_SEASON_TIME_UP_TO_END_FULL_TEXT_ONE_PARAM", false), NKCUtilString.GetTimeString(nkmpvpRankSeasonTemplet.EndDate, true));
					}
					else
					{
						this.m_lbSeasonRemainTime.text = NKCStringTable.GetString("SI_DP_SEASON_TIME_CLOSING_FULL_TEXT", false);
					}
				}
			}
			else
			{
				NKCSoundManager.PlaySound("FX_UI_PVP_RESULT_PROMOTE", 1f, 0f, 0f, false, 0f, false, 0f);
				NKCUtil.SetLabelText(this.m_lbTitle, NKCUtilString.GET_STRING_GAUNTLET_SEASON_REWARD);
				NKCUtil.SetLabelText(this.m_lbSeasonRemainTime, "");
			}
			List<NKCUISlot.SlotData> list = null;
			if (cNKMRewardData != null)
			{
				list = NKCUISlot.MakeSlotDataListFromReward(cNKMRewardData, false, false);
			}
			if (list != null)
			{
				int num = list.Count - this.m_lstNKCUISlot.Count;
				for (int i = 0; i < num; i++)
				{
					NKCUISlot newInstance = NKCUISlot.GetNewInstance(this.m_ParentOfSlots);
					newInstance.transform.localScale = Vector3.one;
					this.m_lstNKCUISlot.Add(newInstance);
				}
				for (int j = 0; j < list.Count; j++)
				{
					NKCUtil.SetGameobjectActive(this.m_lstNKCUISlot[j], true);
					this.m_lstNKCUISlot[j].SetData(list[j], true, null);
				}
				for (int k = list.Count; k < this.m_lstNKCUISlot.Count; k++)
				{
					NKCUtil.SetGameobjectActive(this.m_lstNKCUISlot[k], false);
				}
			}
			else
			{
				for (int l = 0; l < this.m_lstNKCUISlot.Count; l++)
				{
					NKCUtil.SetGameobjectActive(this.m_lstNKCUISlot[l], false);
				}
			}
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			NKCUtil.SetGameobjectActive(this.m_objChangeScoreMessage, bChangeScore);
			base.UIOpened(true);
		}

		// Token: 0x06008556 RID: 34134 RVA: 0x002D1418 File Offset: 0x002CF618
		public void OpenForLeague(NKMRewardData cNKMRewardData)
		{
			this.m_bSeason = true;
			this.m_bRank = false;
			this.m_bLeague = true;
			if (NKCPopupGauntletOutgameReward.m_NKMPVPData != null)
			{
				int score = NKCPopupGauntletOutgameReward.m_NKMPVPData.Score;
				this.m_NKCUILeagueTier.SetUI(NKCPVPManager.GetTierIconByTier(NKM_GAME_TYPE.NGT_PVP_LEAGUE, NKCPopupGauntletOutgameReward.m_NKMPVPData.SeasonID, NKCPopupGauntletOutgameReward.m_NKMPVPData.LeagueTierID), NKCPVPManager.GetTierNumberByTier(NKM_GAME_TYPE.NGT_PVP_LEAGUE, NKCPopupGauntletOutgameReward.m_NKMPVPData.SeasonID, NKCPopupGauntletOutgameReward.m_NKMPVPData.LeagueTierID));
				this.m_lbTierText.text = NKCPVPManager.GetLeagueNameByTier(NKM_GAME_TYPE.NGT_PVP_LEAGUE, NKCPopupGauntletOutgameReward.m_NKMPVPData.SeasonID, NKCPopupGauntletOutgameReward.m_NKMPVPData.LeagueTierID);
				this.m_lbScore.text = score.ToString();
			}
			NKCSoundManager.PlaySound("FX_UI_PVP_RESULT_PROMOTE", 1f, 0f, 0f, false, 0f, false, 0f);
			NKCUtil.SetLabelText(this.m_lbTitle, NKCUtilString.GET_STRING_GAUNTLET_SEASON_REWARD);
			NKCUtil.SetLabelText(this.m_lbSeasonRemainTime, "");
			List<NKCUISlot.SlotData> list = null;
			if (cNKMRewardData != null)
			{
				list = NKCUISlot.MakeSlotDataListFromReward(cNKMRewardData, false, false);
			}
			if (list != null)
			{
				int num = list.Count - this.m_lstNKCUISlot.Count;
				for (int i = 0; i < num; i++)
				{
					NKCUISlot newInstance = NKCUISlot.GetNewInstance(this.m_ParentOfSlots);
					newInstance.transform.localScale = Vector3.one;
					this.m_lstNKCUISlot.Add(newInstance);
				}
				for (int j = 0; j < list.Count; j++)
				{
					NKCUtil.SetGameobjectActive(this.m_lstNKCUISlot[j], true);
					this.m_lstNKCUISlot[j].SetData(list[j], true, null);
				}
				for (int k = list.Count; k < this.m_lstNKCUISlot.Count; k++)
				{
					NKCUtil.SetGameobjectActive(this.m_lstNKCUISlot[k], false);
				}
			}
			else
			{
				for (int l = 0; l < this.m_lstNKCUISlot.Count; l++)
				{
					NKCUtil.SetGameobjectActive(this.m_lstNKCUISlot[l], false);
				}
			}
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
		}

		// Token: 0x06008557 RID: 34135 RVA: 0x002D1623 File Offset: 0x002CF823
		private void Update()
		{
			this.m_NKCUIOpenAnimator.Update();
		}

		// Token: 0x06008558 RID: 34136 RVA: 0x002D1630 File Offset: 0x002CF830
		public void CloseGauntletOutgameRewardPopup()
		{
			base.Close();
		}

		// Token: 0x06008559 RID: 34137 RVA: 0x002D1638 File Offset: 0x002CF838
		public void OnCloseBtn()
		{
			base.Close();
		}

		// Token: 0x0600855A RID: 34138 RVA: 0x002D1640 File Offset: 0x002CF840
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			if (this.m_bSeason && NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_LOBBY)
			{
				if (this.m_bLeague)
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().NKCPopupGauntletNewSeasonAlarm.OpenForLeague();
					return;
				}
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().NKCPopupGauntletNewSeasonAlarm.Open(this.m_bRank);
			}
		}

		// Token: 0x040071BF RID: 29119
		public const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_GAUNTLET";

		// Token: 0x040071C0 RID: 29120
		public const string UI_ASSET_NAME = "NKM_UI_GAUNTLET_RANK_REWARD_POPUP";

		// Token: 0x040071C1 RID: 29121
		public Text m_lbTitle;

		// Token: 0x040071C2 RID: 29122
		public NKCUILeagueTier m_NKCUILeagueTier;

		// Token: 0x040071C3 RID: 29123
		public Text m_lbScore;

		// Token: 0x040071C4 RID: 29124
		public Text m_lbTierText;

		// Token: 0x040071C5 RID: 29125
		public Text m_lbSeasonRemainTime;

		// Token: 0x040071C6 RID: 29126
		public Transform m_ParentOfSlots;

		// Token: 0x040071C7 RID: 29127
		public NKCUIComStateButton m_csbtnOK;

		// Token: 0x040071C8 RID: 29128
		public GameObject m_objChangeScoreMessage;

		// Token: 0x040071C9 RID: 29129
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x040071CA RID: 29130
		private List<NKCUISlot> m_lstNKCUISlot = new List<NKCUISlot>();

		// Token: 0x040071CB RID: 29131
		private bool m_bSeason;

		// Token: 0x040071CC RID: 29132
		private bool m_bRank;

		// Token: 0x040071CD RID: 29133
		private bool m_bLeague;

		// Token: 0x040071CE RID: 29134
		private static PvpState m_NKMPVPData = new PvpState();
	}
}
