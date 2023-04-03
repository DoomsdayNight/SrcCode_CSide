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
	// Token: 0x02000B6C RID: 2924
	public class NKCUIGauntletIntro : NKCUIBase
	{
		// Token: 0x170015B0 RID: 5552
		// (get) Token: 0x060085AB RID: 34219 RVA: 0x002D3D66 File Offset: 0x002D1F66
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_GAUNTLET;
			}
		}

		// Token: 0x170015B1 RID: 5553
		// (get) Token: 0x060085AC RID: 34220 RVA: 0x002D3D6D File Offset: 0x002D1F6D
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170015B2 RID: 5554
		// (get) Token: 0x060085AD RID: 34221 RVA: 0x002D3D70 File Offset: 0x002D1F70
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x170015B3 RID: 5555
		// (get) Token: 0x060085AE RID: 34222 RVA: 0x002D3D73 File Offset: 0x002D1F73
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_PVP_RANK";
			}
		}

		// Token: 0x170015B4 RID: 5556
		// (get) Token: 0x060085AF RID: 34223 RVA: 0x002D3D7A File Offset: 0x002D1F7A
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

		// Token: 0x060085B0 RID: 34224 RVA: 0x002D3D90 File Offset: 0x002D1F90
		public static NKCAssetResourceData OpenInstanceAsync()
		{
			return NKCUIBase.OpenInstanceAsync<NKCUIBaseSceneMenu>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_INTRO");
		}

		// Token: 0x060085B1 RID: 34225 RVA: 0x002D3DA1 File Offset: 0x002D1FA1
		public static bool CheckInstanceLoaded(NKCAssetResourceData loadResourceData, out NKCUIGauntletIntro retVal)
		{
			return NKCUIBase.CheckInstanceLoaded<NKCUIGauntletIntro>(loadResourceData, NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontCommon), out retVal);
		}

		// Token: 0x060085B2 RID: 34226 RVA: 0x002D3DB0 File Offset: 0x002D1FB0
		public void CloseInstance()
		{
			int num = NKCAssetResourceManager.CloseResource("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_INTRO");
			Debug.Log(string.Format("gauntlet intro close resource retval is {0}", num));
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x060085B3 RID: 34227 RVA: 0x002D3DF0 File Offset: 0x002D1FF0
		public void InitUI()
		{
			if (this.m_bInit)
			{
				return;
			}
			this.m_csbtnRank.PointerClick.RemoveAllListeners();
			this.m_csbtnRank.PointerClick.AddListener(new UnityAction(this.OnClickRank));
			this.m_csbtnRank.m_bGetCallbackWhileLocked = true;
			NKCUtil.SetBindFunction(this.m_csbtnAsync, new UnityAction(this.OnClickAsync));
			NKCUtil.SetBindFunction(this.m_csbtnAsyncV2, new UnityAction(this.OnClickAsync));
			this.m_bOpenAsyncNewMode = NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_ASYNC_NEW_MODE);
			NKCUtil.SetGameobjectActive(this.m_csbtnAsync, !this.m_bOpenAsyncNewMode);
			NKCUtil.SetGameobjectActive(this.m_csbtnAsyncV2, this.m_bOpenAsyncNewMode);
			this.m_csbtnLeague.PointerClick.RemoveAllListeners();
			this.m_csbtnLeague.PointerClick.AddListener(new UnityAction(this.OnClickLeague));
			this.m_csbtnLeague.m_bGetCallbackWhileLocked = true;
			if (this.m_csbtnPrivatePvp != null)
			{
				this.m_csbtnPrivatePvp.PointerClick.RemoveAllListeners();
				this.m_csbtnPrivatePvp.PointerClick.AddListener(new UnityAction(this.OnClickPrivatePvp));
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_bInit = true;
		}

		// Token: 0x060085B4 RID: 34228 RVA: 0x002D3F24 File Offset: 0x002D2124
		public void Open()
		{
			base.UIOpened(true);
			NKCScenManager.CurrentUserData();
			NKCUtil.SetGameobjectActive(this.m_csbtnPrivatePvp, NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_FRIENDLY_MODE));
			NKMPvpRankSeasonTemplet pvpRankSeasonTemplet = NKCPVPManager.GetPvpRankSeasonTemplet(NKCUtil.FindPVPSeasonIDForRank(NKCSynchronizedTime.GetServerUTCTime(0.0)));
			bool bValue = false;
			if (pvpRankSeasonTemplet != null)
			{
				if (pvpRankSeasonTemplet.IsSeasonLobbyPrefab)
				{
					NKCUtil.SetLabelText(this.m_lbRankSeason, NKCStringTable.GetString(pvpRankSeasonTemplet.SeasonStrID, false));
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbRankSeason, string.Format(NKCUtilString.GET_STRING_GAUNTLET_SEASON_NUMBERING_NAME, pvpRankSeasonTemplet.SeasonLobbyName));
				}
				bValue = !string.IsNullOrEmpty(pvpRankSeasonTemplet.SeasonIcon);
				NKCUtil.SetImageSprite(this.m_imgRankSeasonIcon, NKCUtil.GetSpriteBattleConditionICon(pvpRankSeasonTemplet.SeasonIcon), false);
			}
			this.SetBattleCondition(pvpRankSeasonTemplet, this.m_objRankBattleCond, this.m_imgRankBattleCond, this.m_lbRankBattleCondTitle, this.m_lbRankBattleCondDesc);
			NKCUtil.SetGameobjectActive(this.m_imgRankSeasonIcon, bValue);
			if (NKCPVPManager.IsPvpRankUnlocked())
			{
				this.m_csbtnRank.UnLock(false);
				NKCUtil.SetGameobjectActive(this.m_objRankLocked, false);
			}
			else
			{
				this.m_csbtnRank.Lock(false);
				NKCUtil.SetGameobjectActive(this.m_objRankLocked, true);
				NKCUtil.SetLabelText(this.m_lbRankLocked, string.Format(NKCUtilString.GET_STRING_GAUNTLET_OPEN_RANK_MODE, NKMPvpCommonConst.Instance.RANK_PVP_OPEN_POINT));
			}
			if (!this.m_bOpenAsyncNewMode)
			{
				bool bValue2 = false;
				NKMPvpRankSeasonTemplet pvpAsyncSeasonTemplet = NKCPVPManager.GetPvpAsyncSeasonTemplet(NKCUtil.FindPVPSeasonIDForAsync(NKCSynchronizedTime.GetServerUTCTime(0.0)));
				if (pvpAsyncSeasonTemplet != null)
				{
					if (pvpAsyncSeasonTemplet.IsSeasonLobbyPrefab)
					{
						NKCUtil.SetLabelText(this.m_lbAsyncSeason, NKCStringTable.GetString(pvpAsyncSeasonTemplet.SeasonStrID, false));
					}
					else
					{
						NKCUtil.SetLabelText(this.m_lbAsyncSeason, NKCUtilString.GET_STRING_GAUNTLET_SEASON_NUMBERING_NAME, new object[]
						{
							pvpAsyncSeasonTemplet.SeasonLobbyName
						});
					}
					bValue2 = !string.IsNullOrEmpty(pvpAsyncSeasonTemplet.SeasonIcon);
					NKCUtil.SetImageSprite(this.m_imgAsyncSeasonIcon, NKCUtil.GetSpriteBattleConditionICon(pvpAsyncSeasonTemplet.SeasonIcon), false);
				}
				this.SetBattleCondition(pvpAsyncSeasonTemplet, this.m_objAsyncBattleCond, this.m_imgAsyncBattleCond, this.m_lbAsyncBattleCondTitle, this.m_lbAsyncBattleCondDesc);
				NKCUtil.SetGameobjectActive(this.m_imgAsyncSeasonIcon, bValue2);
			}
			else
			{
				bool bValue3 = false;
				NKMPvpRankSeasonTemplet pvpAsyncSeasonTemplet2 = NKCPVPManager.GetPvpAsyncSeasonTemplet(NKCUtil.FindPVPSeasonIDForAsync(NKCSynchronizedTime.GetServerUTCTime(0.0)));
				if (pvpAsyncSeasonTemplet2 != null)
				{
					if (pvpAsyncSeasonTemplet2.IsSeasonLobbyPrefab)
					{
						NKCUtil.SetLabelText(this.m_lbAsyncSeasonV2, NKCStringTable.GetString(pvpAsyncSeasonTemplet2.SeasonStrID, false));
					}
					else
					{
						NKCUtil.SetLabelText(this.m_lbAsyncSeasonV2, NKCUtilString.GET_STRING_GAUNTLET_SEASON_NUMBERING_NAME, new object[]
						{
							pvpAsyncSeasonTemplet2.SeasonLobbyName
						});
					}
					bValue3 = !string.IsNullOrEmpty(pvpAsyncSeasonTemplet2.SeasonIcon);
					NKCUtil.SetImageSprite(this.m_imgAsyncSeasonIconV2, NKCUtil.GetSpriteBattleConditionICon(pvpAsyncSeasonTemplet2.SeasonIcon), false);
				}
				this.SetBattleCondition(pvpAsyncSeasonTemplet2, this.m_objAsyncBattleCondV2, this.m_imgAsyncBattleCondV2, this.m_lbAsyncBattleCondTitleV2, this.m_lbAsyncBattleCondDescV2);
				NKCUtil.SetGameobjectActive(this.m_imgAsyncSeasonIconV2, bValue3);
			}
			NKCUtil.SetGameobjectActive(this.m_csbtnLeague, NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_LEAGUE_MODE));
			if (this.m_csbtnLeague != null && this.m_csbtnLeague.gameObject.activeSelf)
			{
				if (NKCPVPManager.IsPvpLeagueUnlocked())
				{
					NKCUtil.SetGameobjectActive(this.m_objLeagueLocked, false);
					NKCUtil.SetGameobjectActive(this.m_objLeagueOpenCond, true);
					NKCUtil.SetLabelText(this.m_lbLeagueOpenDaysOfWeek, NKCPVPManager.GetLeagueOpenDaysString());
					if (!NKMPvpCommonConst.Instance.LeaguePvp.OpenDaysOfWeek.Any((DayOfWeek e) => e == ServiceTime.Recent.DayOfWeek))
					{
						NKCUtil.SetLabelTextColor(this.m_lbLeagueOpenDaysOfWeek, NKCUtil.GetColor("#FF2626"));
					}
					else
					{
						NKCUtil.SetLabelTextColor(this.m_lbLeagueOpenDaysOfWeek, Color.white);
					}
					NKCUtil.SetLabelText(this.m_lbLeagueOpenTime, NKCPVPManager.GetLeagueOpenTimeString());
					TimeSpan timeOfDay = ServiceTime.Recent.TimeOfDay;
					if (NKMPvpCommonConst.Instance.LeaguePvp.OpenTimeStart > timeOfDay || NKMPvpCommonConst.Instance.LeaguePvp.OpenTimeEnd <= timeOfDay)
					{
						NKCUtil.SetLabelTextColor(this.m_lbLeagueOpenTime, NKCUtil.GetColor("#FF2626"));
					}
					else
					{
						NKCUtil.SetLabelTextColor(this.m_lbLeagueOpenTime, Color.white);
					}
					this.m_csbtnLeague.PointerClick.RemoveAllListeners();
					this.m_csbtnLeague.PointerClick.AddListener(new UnityAction(this.OnClickLeague));
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objLeagueLocked, true);
					NKCUtil.SetGameobjectActive(this.m_objLeagueOpenCond, false);
					if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_LEAGUE_MODE))
					{
						NKCUtil.SetLabelText(this.m_lbLeagueLocked, NKCUtilString.GET_STRING_GAUNTLET_LEAGUE_TAG_CLOSE);
					}
					else
					{
						NKCUtil.SetLabelText(this.m_lbLeagueLocked, string.Format(NKCUtilString.GET_STRING_GAUNTLET_OPEN_LEAGUE_MODE, NKMPvpCommonConst.Instance.LEAGUE_PVP_OPEN_POINT));
					}
					this.m_csbtnLeague.PointerClick.RemoveAllListeners();
					this.m_csbtnLeague.PointerClick.AddListener(new UnityAction(this.OnClickLeagueLocked));
				}
				NKMLeaguePvpRankSeasonTemplet nkmleaguePvpRankSeasonTemplet = NKMLeaguePvpRankSeasonTemplet.Find(NKCUtil.FindPVPSeasonIDForLeague(NKCSynchronizedTime.GetServerUTCTime(0.0)));
				if (nkmleaguePvpRankSeasonTemplet != null)
				{
					if (nkmleaguePvpRankSeasonTemplet.IsSeasonLobbyPrefab)
					{
						NKCUtil.SetLabelText(this.m_lbLeagueSeason, NKCStringTable.GetString(nkmleaguePvpRankSeasonTemplet.SeasonStrId, false));
					}
					else
					{
						NKCUtil.SetLabelText(this.m_lbLeagueSeason, NKCUtilString.GET_STRING_GAUNTLET_SEASON_NUMBERING_NAME, new object[]
						{
							nkmleaguePvpRankSeasonTemplet.SeasonLobbyName
						});
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objLeagueBattleCond, false);
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			bool flag = gameOptionData != null && gameOptionData.UseVideoTexture;
			NKCUtil.SetGameobjectActive(this.m_objBGFallBack, !flag);
			this.TutorialCheck();
		}

		// Token: 0x060085B5 RID: 34229 RVA: 0x002D4468 File Offset: 0x002D2668
		private void SetBattleCondition(NKMPvpRankSeasonTemplet templet, GameObject objBattleCond, Image imgBattleCond, Text lbBattleCondTitle, Text lbBattleCondDesc)
		{
			if (templet == null)
			{
				NKCUtil.SetGameobjectActive(objBattleCond, false);
				return;
			}
			if (string.IsNullOrEmpty(templet.SeasonBattleCondition))
			{
				NKCUtil.SetGameobjectActive(objBattleCond, false);
				return;
			}
			NKMBattleConditionTemplet templetByStrID = NKMBattleConditionManager.GetTempletByStrID(templet.SeasonBattleCondition);
			if (templetByStrID != null)
			{
				NKCUtil.SetGameobjectActive(objBattleCond, true);
				NKCUtil.SetImageSprite(imgBattleCond, NKCUtil.GetSpriteBattleConditionICon(templetByStrID), false);
				NKCUtil.SetLabelText(lbBattleCondTitle, templetByStrID.BattleCondName_Translated);
				NKCUtil.SetLabelText(lbBattleCondDesc, templetByStrID.BattleCondDesc_Translated);
				return;
			}
			NKCUtil.SetGameobjectActive(objBattleCond, false);
		}

		// Token: 0x060085B6 RID: 34230 RVA: 0x002D44DC File Offset: 0x002D26DC
		private void OnClickRank()
		{
			if (this.m_csbtnRank.m_bLock)
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(string.Format(NKCUtilString.GET_STRING_GAUNTLET_NOT_OPEN_RANK_MODE, NKMPvpCommonConst.Instance.RANK_PVP_OPEN_POINT), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetReservedLobbyTab(NKC_GAUNTLET_LOBBY_TAB.NGLT_RANK);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LOBBY, true);
		}

		// Token: 0x060085B7 RID: 34231 RVA: 0x002D4545 File Offset: 0x002D2745
		private void OnClickAsync()
		{
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetReservedLobbyTab(NKC_GAUNTLET_LOBBY_TAB.NGLT_ASYNC);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LOBBY, true);
		}

		// Token: 0x060085B8 RID: 34232 RVA: 0x002D4564 File Offset: 0x002D2764
		private void OnClickLeague()
		{
			if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_LEAGUE_MODE))
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_GAUNTLET_LEAGUE_TAG_CLOSE_MESSAGE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			if (!NKCPVPManager.IsPvpLeagueUnlocked())
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(string.Format(NKCUtilString.GET_STRING_GAUNTLET_NOT_OPEN_LEAGUE_MODE, NKMPvpCommonConst.Instance.LEAGUE_PVP_OPEN_POINT), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetReservedLobbyTab(NKC_GAUNTLET_LOBBY_TAB.NGLT_LEAGUE);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LOBBY, true);
		}

		// Token: 0x060085B9 RID: 34233 RVA: 0x002D45E4 File Offset: 0x002D27E4
		private void OnClickLeagueLocked()
		{
			if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_LEAGUE_MODE))
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_GAUNTLET_LEAGUE_TAG_CLOSE_MESSAGE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				return;
			}
			if (!NKCPVPManager.IsPvpLeagueUnlocked())
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(string.Format(NKCUtilString.GET_STRING_GAUNTLET_NOT_OPEN_LEAGUE_MODE, NKMPvpCommonConst.Instance.LEAGUE_PVP_OPEN_POINT), NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
		}

		// Token: 0x060085BA RID: 34234 RVA: 0x002D4647 File Offset: 0x002D2847
		private void OnClickPrivatePvp()
		{
			if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_FRIENDLY_MODE))
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetReservedLobbyTab(NKC_GAUNTLET_LOBBY_TAB.NGLT_PRIVATE);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LOBBY, true);
		}

		// Token: 0x060085BB RID: 34235 RVA: 0x002D4670 File Offset: 0x002D2870
		public override void CloseInternal()
		{
			NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
			if (subUICameraVideoPlayer != null)
			{
				subUICameraVideoPlayer.CleanUp();
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060085BC RID: 34236 RVA: 0x002D469E File Offset: 0x002D289E
		public override void OnBackButton()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
		}

		// Token: 0x060085BD RID: 34237 RVA: 0x002D46AC File Offset: 0x002D28AC
		public void TutorialCheck()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.Gauntlet, true);
		}

		// Token: 0x04007239 RID: 29241
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_GAUNTLET";

		// Token: 0x0400723A RID: 29242
		private const string UI_ASSET_NAME = "NKM_UI_GAUNTLET_INTRO";

		// Token: 0x0400723B RID: 29243
		[Header("랭크전")]
		public NKCUIComStateButton m_csbtnRank;

		// Token: 0x0400723C RID: 29244
		public Text m_lbRankSeason;

		// Token: 0x0400723D RID: 29245
		public GameObject m_objRankBattleCond;

		// Token: 0x0400723E RID: 29246
		public Image m_imgRankBattleCond;

		// Token: 0x0400723F RID: 29247
		public Text m_lbRankBattleCondTitle;

		// Token: 0x04007240 RID: 29248
		public Text m_lbRankBattleCondDesc;

		// Token: 0x04007241 RID: 29249
		public GameObject m_objRankLocked;

		// Token: 0x04007242 RID: 29250
		public Text m_lbRankLocked;

		// Token: 0x04007243 RID: 29251
		public Image m_imgRankSeasonIcon;

		// Token: 0x04007244 RID: 29252
		[Header("전략전")]
		public NKCUIComStateButton m_csbtnAsync;

		// Token: 0x04007245 RID: 29253
		public Text m_lbAsyncSeason;

		// Token: 0x04007246 RID: 29254
		public GameObject m_objAsyncBattleCond;

		// Token: 0x04007247 RID: 29255
		public Image m_imgAsyncBattleCond;

		// Token: 0x04007248 RID: 29256
		public Text m_lbAsyncBattleCondTitle;

		// Token: 0x04007249 RID: 29257
		public Text m_lbAsyncBattleCondDesc;

		// Token: 0x0400724A RID: 29258
		public Image m_imgAsyncSeasonIcon;

		// Token: 0x0400724B RID: 29259
		[Header("전략전V2")]
		public NKCUIComStateButton m_csbtnAsyncV2;

		// Token: 0x0400724C RID: 29260
		public Text m_lbAsyncSeasonV2;

		// Token: 0x0400724D RID: 29261
		public GameObject m_objAsyncBattleCondV2;

		// Token: 0x0400724E RID: 29262
		public Image m_imgAsyncBattleCondV2;

		// Token: 0x0400724F RID: 29263
		public Text m_lbAsyncBattleCondTitleV2;

		// Token: 0x04007250 RID: 29264
		public Text m_lbAsyncBattleCondDescV2;

		// Token: 0x04007251 RID: 29265
		public Image m_imgAsyncSeasonIconV2;

		// Token: 0x04007252 RID: 29266
		[Header("리그전")]
		public NKCUIComStateButton m_csbtnLeague;

		// Token: 0x04007253 RID: 29267
		public Text m_lbLeagueSeason;

		// Token: 0x04007254 RID: 29268
		public GameObject m_objLeagueBattleCond;

		// Token: 0x04007255 RID: 29269
		public Image m_imgLeagueBattleCond;

		// Token: 0x04007256 RID: 29270
		public Text m_lbLeagueBattleCondTitle;

		// Token: 0x04007257 RID: 29271
		public Text m_lbLeagueBattleCondDesc;

		// Token: 0x04007258 RID: 29272
		public GameObject m_objLeagueLocked;

		// Token: 0x04007259 RID: 29273
		public Text m_lbLeagueLocked;

		// Token: 0x0400725A RID: 29274
		public GameObject m_objLeagueOpenCond;

		// Token: 0x0400725B RID: 29275
		public Text m_lbLeagueOpenDaysOfWeek;

		// Token: 0x0400725C RID: 29276
		public Text m_lbLeagueOpenTime;

		// Token: 0x0400725D RID: 29277
		[Header("친선전")]
		public NKCUIComStateButton m_csbtnPrivatePvp;

		// Token: 0x0400725E RID: 29278
		[Header("Fallback BG")]
		public GameObject m_objBGFallBack;

		// Token: 0x0400725F RID: 29279
		private bool m_bInit;

		// Token: 0x04007260 RID: 29280
		private bool m_bOpenAsyncNewMode;
	}
}
