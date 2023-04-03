using System;
using NKC.UI.Result;
using NKM;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B86 RID: 2950
	public class NKCUIGauntletResult : NKCUIBase
	{
		// Token: 0x170015E2 RID: 5602
		// (get) Token: 0x0600880E RID: 34830 RVA: 0x002E099F File Offset: 0x002DEB9F
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_GAUNTLET;
			}
		}

		// Token: 0x170015E3 RID: 5603
		// (get) Token: 0x0600880F RID: 34831 RVA: 0x002E09A6 File Offset: 0x002DEBA6
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170015E4 RID: 5604
		// (get) Token: 0x06008810 RID: 34832 RVA: 0x002E09A9 File Offset: 0x002DEBA9
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x06008811 RID: 34833 RVA: 0x002E09AC File Offset: 0x002DEBAC
		public static void SetResultData(NKCUIResult.BattleResultData _BattleResultData)
		{
			NKCUIGauntletResult.m_BattleResultData = _BattleResultData;
		}

		// Token: 0x06008812 RID: 34834 RVA: 0x002E09B4 File Offset: 0x002DEBB4
		public static NKCAssetResourceData OpenInstanceAsync()
		{
			return NKCUIBase.OpenInstanceAsync<NKCUIBaseSceneMenu>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_RESULT");
		}

		// Token: 0x06008813 RID: 34835 RVA: 0x002E09C5 File Offset: 0x002DEBC5
		public static bool CheckInstanceLoaded(NKCAssetResourceData loadResourceData, out NKCUIGauntletResult retVal)
		{
			return NKCUIBase.CheckInstanceLoaded<NKCUIGauntletResult>(loadResourceData, NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontCommon), out retVal);
		}

		// Token: 0x06008814 RID: 34836 RVA: 0x002E09D4 File Offset: 0x002DEBD4
		public void CloseInstance()
		{
			NKCAssetResourceManager.CloseResource("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_RESULT");
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06008815 RID: 34837 RVA: 0x002E09F1 File Offset: 0x002DEBF1
		public void InitUI()
		{
			if (this.m_bInit)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_bInit = true;
		}

		// Token: 0x06008816 RID: 34838 RVA: 0x002E0A0F File Offset: 0x002DEC0F
		public void Open(NKCUIGauntletResult.OnClose _dOnClose)
		{
			if (NKCUIGauntletResult.m_BattleResultData == null)
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				return;
			}
			this.dOnClose = _dOnClose;
			base.UIOpened(true);
			if (!this.SetUI())
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				return;
			}
			NKCCamera.EnableBloom(false);
		}

		// Token: 0x06008817 RID: 34839 RVA: 0x002E0A50 File Offset: 0x002DEC50
		private bool CheckTierDiff()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return false;
			}
			PvpState pvPData = this.GetPvPData(myUserData, NKCUIGauntletResult.m_BattleResultData.m_NKM_GAME_TYPE);
			return pvPData != null && NKCPVPManager.GetFinalTier(pvPData.LeagueTierID) != NKCPVPManager.GetFinalTier(NKCUIGauntletResult.m_BattleResultData.m_OrgPVPTier);
		}

		// Token: 0x06008818 RID: 34840 RVA: 0x002E0AA4 File Offset: 0x002DECA4
		private bool SetUI()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return false;
			}
			if (NKCUIGauntletResult.m_BattleResultData != null)
			{
				PvpState pvPData = this.GetPvPData(myUserData, NKCUIGauntletResult.m_BattleResultData.m_NKM_GAME_TYPE);
				NKCUtil.SetLabelText(this.m_lbScore, pvPData.Score.ToString());
				int num = pvPData.Score - NKCUIGauntletResult.m_BattleResultData.m_OrgPVPScore;
				if (num > 0)
				{
					NKCUtil.SetLabelText(this.m_lbAddScore, "+" + num.ToString());
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbAddScore, num.ToString());
				}
				int seasonID = NKCPVPManager.FindPvPSeasonID(NKCUIGauntletResult.m_BattleResultData.m_NKM_GAME_TYPE, NKCSynchronizedTime.GetServerUTCTime(0.0));
				this.m_NKCUILeagueTier_After.SetUI(NKCPVPManager.GetTierIconByTier(NKCUIGauntletResult.m_BattleResultData.m_NKM_GAME_TYPE, seasonID, pvPData.LeagueTierID), NKCPVPManager.GetTierNumberByTier(NKCUIGauntletResult.m_BattleResultData.m_NKM_GAME_TYPE, seasonID, pvPData.LeagueTierID));
				NKCUtil.SetLabelText(this.m_lbPromoteTier, NKCPVPManager.GetLeagueNameByTier(NKCUIGauntletResult.m_BattleResultData.m_NKM_GAME_TYPE, seasonID, pvPData.LeagueTierID));
				this.m_NKCUILeagueTier.SetUI(NKCPVPManager.GetTierIconByTier(NKCUIGauntletResult.m_BattleResultData.m_NKM_GAME_TYPE, seasonID, NKCUIGauntletResult.m_BattleResultData.m_OrgPVPTier), NKCPVPManager.GetTierNumberByTier(NKCUIGauntletResult.m_BattleResultData.m_NKM_GAME_TYPE, seasonID, NKCUIGauntletResult.m_BattleResultData.m_OrgPVPTier));
				string text = "";
				this.m_fDefaultAniTime = 8f;
				if (NKCUIGauntletResult.m_BattleResultData.m_BATTLE_RESULT_TYPE == BATTLE_RESULT_TYPE.BRT_WIN)
				{
					if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_REPLAY) && NKCReplayMgr.IsPlayingReplay())
					{
						text = "RESULT_REPLAY_WIN";
					}
					else if (NKCUIGauntletResult.m_BattleResultData.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_PRIVATE)
					{
						text = "RESULT_REPLAY_WIN";
					}
					else if (this.CheckTierDiff())
					{
						text = "RESULT_PROMOTION";
					}
					else
					{
						text = "RESULT_WIN";
					}
					this.SetBG();
				}
				else if (NKCUIGauntletResult.m_BattleResultData.m_BATTLE_RESULT_TYPE == BATTLE_RESULT_TYPE.BRT_LOSE)
				{
					if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_REPLAY) && NKCReplayMgr.IsPlayingReplay())
					{
						text = "RESULT_REPLAY_LOSE";
					}
					else if (NKCUIGauntletResult.m_BattleResultData.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_PRIVATE)
					{
						text = "RESULT_REPLAY_LOSE";
					}
					else if (this.CheckTierDiff())
					{
						text = "RESULT_DEMOTE";
					}
					else
					{
						text = "RESULT_LOSE";
						NKCUtil.SetGameobjectActive(this.m_objDemoteAlert, this.IsPVPDemotionAlert(pvPData, NKCUIGauntletResult.m_BattleResultData.m_NKM_GAME_TYPE));
					}
				}
				else if (NKCUIGauntletResult.m_BattleResultData.m_BATTLE_RESULT_TYPE == BATTLE_RESULT_TYPE.BRT_DRAW)
				{
					if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_REPLAY) && NKCReplayMgr.IsPlayingReplay())
					{
						text = "RESULT_REPLAY_DRAW";
					}
					else if (NKCUIGauntletResult.m_BattleResultData.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_PRIVATE)
					{
						text = "RESULT_REPLAY_DRAW";
					}
					else
					{
						text = "RESULT_DRAW";
					}
				}
				else
				{
					Debug.LogWarning("Gauntlet Unknown Result !!");
				}
				if (this.m_Animator != null && !string.IsNullOrEmpty(text))
				{
					this.m_Animator.SetTrigger(text);
				}
			}
			return true;
		}

		// Token: 0x06008819 RID: 34841 RVA: 0x002E0D4C File Offset: 0x002DEF4C
		private void SetBG()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			bool flag = gameOptionData != null && gameOptionData.UseVideoTexture;
			NKCUtil.SetGameobjectActive(this.m_objBGFallBack, !flag);
			if (flag)
			{
				NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
				if (subUICameraVideoPlayer != null)
				{
					subUICameraVideoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
					subUICameraVideoPlayer.m_fMoviePlaySpeed = 1f;
					subUICameraVideoPlayer.Play("Gauntlet_BG.mp4", true, false, null, false);
				}
			}
		}

		// Token: 0x0600881A RID: 34842 RVA: 0x002E0DB4 File Offset: 0x002DEFB4
		private void Update()
		{
			if (!base.IsOpen)
			{
				return;
			}
			if (this.m_Animator == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				this.dOnClose = null;
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_ERROR_SERVER_GAME_DATA_AND_GO_LOBBY, delegate()
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				}, "");
				Debug.Log("NKCUIGauntletResult.Update : m_Animator is null");
				return;
			}
			if (this.m_fDefaultAniTime > 0f)
			{
				this.m_fDefaultAniTime -= Time.deltaTime;
			}
			if (this.m_fDefaultAniTime <= 0f)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				this.dOnClose = null;
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_ERROR_SERVER_GAME_DATA_AND_GO_LOBBY, delegate()
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				}, "");
				Debug.Log("NKCUIGauntletResult.Update : Default Ani Time played");
				return;
			}
			if (this.m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
			{
				return;
			}
			base.Close();
		}

		// Token: 0x0600881B RID: 34843 RVA: 0x002E0EC8 File Offset: 0x002DF0C8
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
			if (subUICameraVideoPlayer != null)
			{
				subUICameraVideoPlayer.CleanUp();
			}
			if (this.dOnClose != null)
			{
				this.dOnClose();
			}
			NKCUIGauntletResult.m_BattleResultData = null;
		}

		// Token: 0x0600881C RID: 34844 RVA: 0x002E0F0F File Offset: 0x002DF10F
		public override void OnBackButton()
		{
		}

		// Token: 0x0600881D RID: 34845 RVA: 0x002E0F11 File Offset: 0x002DF111
		private PvpState GetPvPData(NKMUserData userData, NKM_GAME_TYPE gameType)
		{
			if (gameType <= NKM_GAME_TYPE.NGT_ASYNC_PVP)
			{
				if (gameType == NKM_GAME_TYPE.NGT_PVP_RANK)
				{
					goto IL_2C;
				}
				if (gameType != NKM_GAME_TYPE.NGT_ASYNC_PVP)
				{
					goto IL_2C;
				}
			}
			else
			{
				if (gameType == NKM_GAME_TYPE.NGT_PVP_LEAGUE)
				{
					return userData.m_LeagueData;
				}
				if (gameType - NKM_GAME_TYPE.NGT_PVP_STRATEGY > 2)
				{
					goto IL_2C;
				}
			}
			return userData.m_AsyncData;
			IL_2C:
			return userData.m_PvpData;
		}

		// Token: 0x0600881E RID: 34846 RVA: 0x002E0F45 File Offset: 0x002DF145
		private bool IsPVPDemotionAlert(PvpState pvpData, NKM_GAME_TYPE gameType)
		{
			if (gameType != NKM_GAME_TYPE.NGT_PVP_RANK)
			{
				return gameType != NKM_GAME_TYPE.NGT_ASYNC_PVP && gameType == NKM_GAME_TYPE.NGT_PVP_LEAGUE && NKCUtil.IsPVPDemotionAlert(gameType, pvpData);
			}
			return NKCUtil.IsPVPDemotionAlert(gameType, pvpData);
		}

		// Token: 0x0400747C RID: 29820
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_GAUNTLET";

		// Token: 0x0400747D RID: 29821
		private const string UI_ASSET_NAME = "NKM_UI_GAUNTLET_RESULT";

		// Token: 0x0400747E RID: 29822
		private NKCUIGauntletResult.OnClose dOnClose;

		// Token: 0x0400747F RID: 29823
		private bool m_bInit;

		// Token: 0x04007480 RID: 29824
		public Animator m_Animator;

		// Token: 0x04007481 RID: 29825
		public Text m_lbScore;

		// Token: 0x04007482 RID: 29826
		public Text m_lbAddScore;

		// Token: 0x04007483 RID: 29827
		public NKCUILeagueTier m_NKCUILeagueTier;

		// Token: 0x04007484 RID: 29828
		public NKCUILeagueTier m_NKCUILeagueTier_After;

		// Token: 0x04007485 RID: 29829
		public Text m_lbPromoteTier;

		// Token: 0x04007486 RID: 29830
		public GameObject m_objDemoteAlert;

		// Token: 0x04007487 RID: 29831
		[Header("Fallback BG")]
		public GameObject m_objBGFallBack;

		// Token: 0x04007488 RID: 29832
		private static NKCUIResult.BattleResultData m_BattleResultData;

		// Token: 0x04007489 RID: 29833
		private const float DEFAULT_ANI_WAIT_TIME = 8f;

		// Token: 0x0400748A RID: 29834
		private float m_fDefaultAniTime;

		// Token: 0x0200192C RID: 6444
		// (Invoke) Token: 0x0600B7D9 RID: 47065
		public delegate void OnClose();
	}
}
