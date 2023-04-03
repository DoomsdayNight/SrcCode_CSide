using System;
using System.Collections;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Community;
using ClientPacket.Mode;
using ClientPacket.Warfare;
using Cs.Logging;
using NKC.Templet;
using NKC.UI.Friend;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

namespace NKC.UI.Result
{
	// Token: 0x02000BAE RID: 2990
	public class NKCUIWarfareResult : NKCUIBase
	{
		// Token: 0x17001610 RID: 5648
		// (get) Token: 0x06008A14 RID: 35348 RVA: 0x002ECD38 File Offset: 0x002EAF38
		public static NKCUIWarfareResult Instance
		{
			get
			{
				if (NKCUIWarfareResult.m_Instance == null)
				{
					NKCUIWarfareResult.m_Instance = NKCUIManager.OpenNewInstance<NKCUIWarfareResult>("ab_ui_nkm_ui_result", "NKM_UI_WARFARE_RESULT", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIWarfareResult.CleanupInstance)).GetInstance<NKCUIWarfareResult>();
					NKCUIWarfareResult.m_Instance.InitUI();
				}
				return NKCUIWarfareResult.m_Instance;
			}
		}

		// Token: 0x17001611 RID: 5649
		// (get) Token: 0x06008A15 RID: 35349 RVA: 0x002ECD87 File Offset: 0x002EAF87
		public static bool HasInstance
		{
			get
			{
				return NKCUIWarfareResult.m_Instance != null;
			}
		}

		// Token: 0x17001612 RID: 5650
		// (get) Token: 0x06008A16 RID: 35350 RVA: 0x002ECD94 File Offset: 0x002EAF94
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIWarfareResult.m_Instance != null && NKCUIWarfareResult.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008A17 RID: 35351 RVA: 0x002ECDAF File Offset: 0x002EAFAF
		public static void CheckInstanceAndClose()
		{
			if (NKCUIWarfareResult.m_Instance != null && NKCUIWarfareResult.m_Instance.IsOpen)
			{
				NKCUIWarfareResult.m_Instance.Close();
			}
		}

		// Token: 0x06008A18 RID: 35352 RVA: 0x002ECDD4 File Offset: 0x002EAFD4
		private static void CleanupInstance()
		{
			NKCUIWarfareResult.m_Instance = null;
		}

		// Token: 0x17001613 RID: 5651
		// (get) Token: 0x06008A19 RID: 35353 RVA: 0x002ECDDC File Offset: 0x002EAFDC
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_WARFARE_RESULT;
			}
		}

		// Token: 0x17001614 RID: 5652
		// (get) Token: 0x06008A1A RID: 35354 RVA: 0x002ECDE3 File Offset: 0x002EAFE3
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001615 RID: 5653
		// (get) Token: 0x06008A1B RID: 35355 RVA: 0x002ECDE6 File Offset: 0x002EAFE6
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x17001616 RID: 5654
		// (get) Token: 0x06008A1C RID: 35356 RVA: 0x002ECDEC File Offset: 0x002EAFEC
		private NKCPopupArtifactExchange NKCPopupArtifactExchange
		{
			get
			{
				if (this.m_NKCPopupArtifactExchange == null)
				{
					NKCUIManager.LoadedUIData loadedUIData = NKCUIManager.OpenNewInstance<NKCPopupArtifactExchange>("AB_UI_NKM_UI_WORLD_MAP_DIVE", "NKM_UI_DIVE_ARTIFACT_EXCHANGE_POPUP", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), null);
					this.m_NKCPopupArtifactExchange = loadedUIData.GetInstance<NKCPopupArtifactExchange>();
					this.m_NKCPopupArtifactExchange.InitUI();
				}
				return this.m_NKCPopupArtifactExchange;
			}
		}

		// Token: 0x06008A1D RID: 35357 RVA: 0x002ECE3B File Offset: 0x002EB03B
		public void CheckNKCPopupArtifactExchangeAndClose()
		{
			if (this.m_NKCPopupArtifactExchange != null && this.m_NKCPopupArtifactExchange.IsOpen)
			{
				this.m_NKCPopupArtifactExchange.Close();
			}
		}

		// Token: 0x06008A1E RID: 35358 RVA: 0x002ECE64 File Offset: 0x002EB064
		private void InitUI()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnCloseToHome, new UnityAction(this.CloseToHome));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnCloseToList, new UnityAction(this.CloseToOK));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnRestart, new UnityAction(this.OnClickRetry));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnConfirm, new UnityAction(this.CloseToOK));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnNextMission, new UnityAction(this.PlayNextOperation));
			NKCUtil.SetButtonClickDelegate(this.m_NKM_UI_WARFARE_RESULT_BTN_REPEAT, new UnityAction(this.OnClickRepeatOperation));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnBattleStat, new UnityAction(this.OnClickBattleStat));
		}

		// Token: 0x06008A1F RID: 35359 RVA: 0x002ECF20 File Offset: 0x002EB120
		public void OpenForDive(bool bClear, NKMRewardData cNKMRewardData, NKMRewardData cNKMRewardDataArtifact, NKMRewardData cNKMRewardDataStrom, List<int> lstArtifact, NKMDeckIndex currentDeck, NKCUIWarfareResult.CallBackWhenClosed callBackWhenClosed)
		{
			this.m_bPause = false;
			this.m_eContents = NKCUIWarfareResult.USE_CONTENTS.DIVE;
			this.SetAutoProcessWaitTime();
			this.m_bClearDive = bClear;
			this.m_DiveRewardData = cNKMRewardData;
			this.m_DiveRewardDataArtifact = cNKMRewardDataArtifact;
			this.m_DiveRewardDataStorm = cNKMRewardDataStrom;
			this.m_lstArtifact.Clear();
			if (lstArtifact != null)
			{
				this.m_lstArtifact.AddRange(lstArtifact);
			}
			this.m_bArtifactReturnEvent = false;
			this.m_bWaitingMovie = false;
			this.m_bDoneMakeRewardSlot = false;
			this.m_bReservedShowGetUnit = false;
			this.m_bFinishedIntroMovie = false;
			this.m_bWaitContentUnlockPopup = false;
			this.m_bTriggeredAutoOK = false;
			if (bClear)
			{
				if (this.m_DiveRewardData != null && this.m_DiveRewardData.GetUnitCount() > 0)
				{
					this.m_bReservedShowGetUnit = true;
					List<NKCUISlot.SlotData> list = NKCUISlot.MakeSlotDataListFromReward(this.m_DiveRewardData, false, false);
					this.m_fTimeToShowGetUnit = (float)(list.Count - 1) * 0.13333334f + 2.1833334f + 0.8333333f;
				}
				if (this.m_DiveRewardDataStorm != null)
				{
					NKCUISlot.MakeSlotDataListFromReward(this.m_DiveRewardDataStorm, false, false);
				}
			}
			for (int i = 0; i < this.m_lstNKCUIWRRewardSlot.Count; i++)
			{
				NKCUIWRRewardSlot nkcuiwrrewardSlot = this.m_lstNKCUIWRRewardSlot[i];
				nkcuiwrrewardSlot.InvalidAni();
				NKCUtil.SetGameobjectActive(nkcuiwrrewardSlot.gameObject, false);
			}
			this.SetUIRewardAlert(false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_REWARD_BONUS_TYPE.gameObject, false);
			this.m_CallBackWhenClosed = callBackWhenClosed;
			this.m_CurrentDeck = currentDeck;
			this.m_dummyUnitID = 0;
			this.m_dummyShipID = 0;
			this.m_NextScenID = NKM_SCEN_ID.NSI_INVALID;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (bClear)
			{
				base.UIOpened(true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_ROOT, false);
				if (this.m_coIntro != null)
				{
					base.StopCoroutine(this.m_coIntro);
				}
				this.m_coIntro = base.StartCoroutine(this.WarfareResultUIOpenProcess());
				return;
			}
			this.m_bFinishedIntroMovie = true;
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_ROOT, true);
			this.SetUI();
			base.UIOpened(true);
		}

		// Token: 0x06008A20 RID: 35360 RVA: 0x002ED0E9 File Offset: 0x002EB2E9
		private void SetAutoProcessWaitTime()
		{
			if (this.CheckSpecialEventExist())
			{
				this.m_fFinalAutoProcessWaitTime = 10f;
				return;
			}
			this.m_fFinalAutoProcessWaitTime = 5f;
		}

		// Token: 0x06008A21 RID: 35361 RVA: 0x002ED10C File Offset: 0x002EB30C
		public void Open(NKMWarfareClearData _NKMWarfareClearData, NKMDeckIndex currentDeck, NKCUIWarfareResult.CallBackWhenClosed callBackWhenClosed, bool bNoClearBefore, bool bNoAllClearBefore, WarfareSupporterListData guestSptData = null)
		{
			this.m_bPause = false;
			this.m_eContents = NKCUIWarfareResult.USE_CONTENTS.WARFARE;
			this.SetAutoProcessWaitTime();
			this.m_bWaitingMovie = false;
			this.m_bDoneMakeRewardSlot = false;
			this.m_bReservedShowGetUnit = false;
			this.m_bFinishedIntroMovie = false;
			this.m_guestSptData = guestSptData;
			this.m_bWaitContentUnlockPopup = false;
			this.m_bTriggeredAutoOK = false;
			this.m_bfirstClear = (bNoClearBefore && _NKMWarfareClearData != null);
			this.m_bFirstAllClear = bNoAllClearBefore;
			this.m_NKMWarfareClearData = _NKMWarfareClearData;
			if (this.m_NKMWarfareClearData != null)
			{
				int num = 0;
				if (this.m_NKMWarfareClearData.m_RewardDataList != null && this.m_NKMWarfareClearData.m_RewardDataList.GetUnitCount() > 0)
				{
					this.m_bReservedShowGetUnit = true;
					List<NKCUISlot.SlotData> list = NKCUISlot.MakeSlotDataListFromReward(this.m_NKMWarfareClearData.m_RewardDataList, false, false);
					num += list.Count;
				}
				if (this.m_NKMWarfareClearData.m_OnetimeRewards != null && this.m_NKMWarfareClearData.m_OnetimeRewards.GetUnitCount() > 0)
				{
					this.m_bReservedShowGetUnit = true;
					List<NKCUISlot.SlotData> list2 = NKCUISlot.MakeSlotDataListFromReward(this.m_NKMWarfareClearData.m_OnetimeRewards, false, false);
					num += list2.Count;
				}
				this.m_fTimeToShowGetUnit = (float)(num - 1) * 0.13333334f + 2.1833334f + 0.8333333f;
			}
			for (int i = 0; i < this.m_lstNKCUIWRRewardSlot.Count; i++)
			{
				NKCUIWRRewardSlot nkcuiwrrewardSlot = this.m_lstNKCUIWRRewardSlot[i];
				nkcuiwrrewardSlot.InvalidAni();
				NKCUtil.SetGameobjectActive(nkcuiwrrewardSlot.gameObject, false);
			}
			this.SetUIRewardAlert(false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_REWARD_BONUS_TYPE.gameObject, false);
			this.m_CallBackWhenClosed = callBackWhenClosed;
			this.m_CurrentDeck = currentDeck;
			this.m_NextScenID = NKM_SCEN_ID.NSI_INVALID;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_csbtnCloseToList, true);
			NKCUtil.SetGameobjectActive(this.m_csbtnRestart, true);
			if (_NKMWarfareClearData != null)
			{
				base.UIOpened(true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_ROOT, false);
				if (this.m_coIntro != null)
				{
					base.StopCoroutine(this.m_coIntro);
				}
				this.m_coIntro = base.StartCoroutine(this.WarfareResultUIOpenProcess());
			}
			else
			{
				this.m_bFinishedIntroMovie = true;
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_ROOT, true);
				this.SetUI();
				base.UIOpened(true);
			}
			if (this.m_bfirstClear && _NKMWarfareClearData != null)
			{
				NKCAdjustManager.OnWarfareResult(_NKMWarfareClearData.m_WarfareID);
			}
			this.CheckTutorialRequired();
		}

		// Token: 0x06008A22 RID: 35362 RVA: 0x002ED32C File Offset: 0x002EB52C
		public void OpenForShadow(NKMShadowGameResult shadowResult, List<int> lstBestTime, int dummyUnitID, int dummyShipID, NKCUIWarfareResult.CallBackWhenClosed callBackWhenClosed)
		{
			if (shadowResult == null)
			{
				Debug.LogError("shadowResult == null");
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			this.m_bPause = false;
			this.m_eContents = NKCUIWarfareResult.USE_CONTENTS.SHADOW;
			this.SetAutoProcessWaitTime();
			this.m_ShadowResult = shadowResult;
			this.m_lstShadowBestTime = lstBestTime;
			this.m_bShadowRecordEvent = (shadowResult.life > 0);
			this.m_CurrentDeck = default(NKMDeckIndex);
			this.m_dummyUnitID = dummyUnitID;
			this.m_dummyShipID = dummyShipID;
			this.m_bWaitingMovie = false;
			this.m_bDoneMakeRewardSlot = false;
			this.m_bReservedShowGetUnit = false;
			this.m_bFinishedIntroMovie = false;
			this.m_bWaitContentUnlockPopup = false;
			this.m_bTriggeredAutoOK = false;
			if (shadowResult.rewardData != null)
			{
				this.m_bReservedShowGetUnit = (shadowResult.rewardData.GetUnitCount() > 0);
				this.m_fTimeToShowGetUnit = (float)(shadowResult.rewardData.GetUnitCount() - 1) * 0.13333334f + 2.1833334f + 0.8333333f;
			}
			for (int i = 0; i < this.m_lstNKCUIWRRewardSlot.Count; i++)
			{
				NKCUIWRRewardSlot nkcuiwrrewardSlot = this.m_lstNKCUIWRRewardSlot[i];
				nkcuiwrrewardSlot.InvalidAni();
				NKCUtil.SetGameobjectActive(nkcuiwrrewardSlot.gameObject, false);
			}
			this.SetUIRewardAlert(false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_REWARD_BONUS_TYPE.gameObject, false);
			this.m_CallBackWhenClosed = callBackWhenClosed;
			this.m_NextScenID = NKM_SCEN_ID.NSI_INVALID;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (shadowResult.life > 0)
			{
				base.UIOpened(true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_ROOT, false);
				if (this.m_coIntro != null)
				{
					base.StopCoroutine(this.m_coIntro);
				}
				this.m_coIntro = base.StartCoroutine(this.WarfareResultUIOpenProcess());
				return;
			}
			this.m_bFinishedIntroMovie = true;
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_ROOT, true);
			this.SetUI();
			base.UIOpened(true);
		}

		// Token: 0x06008A23 RID: 35363 RVA: 0x002ED4D4 File Offset: 0x002EB6D4
		public void OpenForTrim(NKMTrimClearData trimClearData, TrimModeState trimModeState, long unitUId, int dummyUnitId, bool firstClear, int bestScore, NKCUIWarfareResult.CallBackWhenClosed callBackWhenClosed)
		{
			if (trimClearData == null || trimModeState == null)
			{
				return;
			}
			this.m_bPause = false;
			this.m_eContents = NKCUIWarfareResult.USE_CONTENTS.TRIM;
			this.m_bTrimEvent = true;
			this.SetAutoProcessWaitTime();
			this.m_trimClearData = trimClearData;
			this.m_trimModeState = trimModeState;
			this.m_leaderUnitUID = unitUId;
			this.m_dummyUnitID = dummyUnitId;
			this.m_dummyShipID = 0;
			this.m_trimBestScore = bestScore;
			this.m_bTrimFirstClear = firstClear;
			this.m_bWaitingMovie = false;
			this.m_bDoneMakeRewardSlot = false;
			this.m_bReservedShowGetUnit = false;
			this.m_bFinishedIntroMovie = false;
			this.m_bWaitContentUnlockPopup = false;
			this.m_bTriggeredAutoOK = false;
			if (trimClearData.rewardData != null && trimClearData.rewardData.GetUnitCount() > 0)
			{
				int unitCount = trimClearData.rewardData.GetUnitCount();
				this.m_bReservedShowGetUnit = (unitCount > 0);
				this.m_fTimeToShowGetUnit = (float)(unitCount - 1) * 0.13333334f + 2.1833334f + 0.8333333f;
			}
			for (int i = 0; i < this.m_lstNKCUIWRRewardSlot.Count; i++)
			{
				NKCUIWRRewardSlot nkcuiwrrewardSlot = this.m_lstNKCUIWRRewardSlot[i];
				nkcuiwrrewardSlot.InvalidAni();
				NKCUtil.SetGameobjectActive(nkcuiwrrewardSlot.gameObject, false);
			}
			this.SetUIRewardAlert(false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_REWARD_BONUS_TYPE.gameObject, false);
			this.m_CallBackWhenClosed = callBackWhenClosed;
			this.m_CurrentDeck = new NKMDeckIndex(NKM_DECK_TYPE.NDT_NORMAL, 0);
			this.m_NextScenID = NKM_SCEN_ID.NSI_INVALID;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (trimClearData.isWin)
			{
				base.UIOpened(true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_ROOT, false);
				if (this.m_coIntro != null)
				{
					base.StopCoroutine(this.m_coIntro);
				}
				this.m_coIntro = base.StartCoroutine(this.WarfareResultUIOpenProcess());
				return;
			}
			this.m_bFinishedIntroMovie = true;
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_ROOT, true);
			this.SetUI();
			base.UIOpened(true);
		}

		// Token: 0x06008A24 RID: 35364 RVA: 0x002ED680 File Offset: 0x002EB880
		public void OpenForDungeon(NKCUIResult.BattleResultData battleResultData, long leaderUnitUID, long leaderShipUID, int dummyLeaderUnitID, int dummyLeaderSkinID, NKCUIWarfareResult.CallBackWhenClosed callBackWhenClosed)
		{
			this.m_bPause = false;
			this.m_eContents = NKCUIWarfareResult.USE_CONTENTS.DUNGEON;
			this.SetAutoProcessWaitTime();
			this.m_bWaitingMovie = false;
			this.m_bDoneMakeRewardSlot = false;
			this.m_bReservedShowGetUnit = false;
			this.m_bFinishedIntroMovie = false;
			this.m_bWaitContentUnlockPopup = false;
			this.m_bTriggeredAutoOK = false;
			this.m_bfirstClear = (battleResultData.m_firstRewardData != null);
			this.m_bFirstAllClear = (battleResultData.m_firstAllClearData != null);
			this.m_battleResultData = battleResultData;
			if (this.m_battleResultData != null)
			{
				int num = 0;
				List<NKCUISlot.SlotData> allListRewardSlotData = this.m_battleResultData.GetAllListRewardSlotData();
				if (allListRewardSlotData.Exists((NKCUISlot.SlotData x) => x.eType == NKCUISlot.eSlotMode.Unit || x.eType == NKCUISlot.eSlotMode.Skin))
				{
					this.m_bReservedShowGetUnit = true;
					num = allListRewardSlotData.Count;
				}
				this.m_fTimeToShowGetUnit = (float)(num - 1) * 0.13333334f + 2.1833334f + 0.8333333f;
			}
			for (int i = 0; i < this.m_lstNKCUIWRRewardSlot.Count; i++)
			{
				NKCUIWRRewardSlot nkcuiwrrewardSlot = this.m_lstNKCUIWRRewardSlot[i];
				nkcuiwrrewardSlot.InvalidAni();
				NKCUtil.SetGameobjectActive(nkcuiwrrewardSlot.gameObject, false);
			}
			this.SetUIRewardAlert(false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_REWARD_BONUS_TYPE.gameObject, false);
			this.m_CallBackWhenClosed = callBackWhenClosed;
			this.m_leaderUnitUID = leaderUnitUID;
			this.m_leaderShipUID = leaderShipUID;
			this.m_dummyUnitID = dummyLeaderUnitID;
			this.m_dummyUnitSkinID = dummyLeaderSkinID;
			this.m_NextScenID = NKM_SCEN_ID.NSI_INVALID;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_csbtnCloseToList, true);
			NKCUtil.SetGameobjectActive(this.m_csbtnRestart, true);
			if (battleResultData.m_BATTLE_RESULT_TYPE == BATTLE_RESULT_TYPE.BRT_WIN)
			{
				base.UIOpened(true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_ROOT, false);
				if (this.m_coIntro != null)
				{
					base.StopCoroutine(this.m_coIntro);
				}
				this.m_coIntro = base.StartCoroutine(this.WarfareResultUIOpenProcess());
			}
			else
			{
				this.m_bFinishedIntroMovie = true;
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_ROOT, true);
				this.SetUI();
				base.UIOpened(true);
			}
			if (this.m_bfirstClear && battleResultData.m_BATTLE_RESULT_TYPE == BATTLE_RESULT_TYPE.BRT_WIN)
			{
				NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(battleResultData.m_stageID);
				if (nkmstageTempletV != null && nkmstageTempletV.DungeonTempletBase != null)
				{
					NKCAdjustManager.OnClearDungeon(nkmstageTempletV.DungeonTempletBase.m_DungeonID);
				}
			}
			this.CheckTutorialRequired();
		}

		// Token: 0x06008A25 RID: 35365 RVA: 0x002ED893 File Offset: 0x002EBA93
		private IEnumerator WarfareResultUIOpenProcess()
		{
			NKCUIComVideoCamera videoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
			if (videoPlayer != null)
			{
				videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
				videoPlayer.m_fMoviePlaySpeed = this.MoviePlaySpeed;
				this.m_bWaitingMovie = true;
				videoPlayer.Play("WarfareResultIntro.mp4", false, false, new NKCUIComVideoPlayer.VideoPlayMessageCallback(this.VideoPlayMessageCallback), false);
				yield return null;
				NKCSoundManager.PlaySound("FX_UI_WARFARE_RESULT_START", 1f, 0f, 0f, false, 0f, false, 0f);
				while (this.m_bWaitingMovie)
				{
					yield return null;
					if (Input.anyKeyDown && PlayerPrefs.GetInt("WARFARE_RESULT_INTRO_SKIP", 0) == 1)
					{
						break;
					}
				}
				videoPlayer.Stop();
				if (PlayerPrefs.GetInt("WARFARE_RESULT_INTRO_SKIP", 0) == 0)
				{
					PlayerPrefs.SetInt("WARFARE_RESULT_INTRO_SKIP", 1);
				}
			}
			this.m_bWaitingMovie = false;
			this.SetUI();
			this.m_bFinishedIntroMovie = true;
			this.m_coIntro = null;
			yield break;
		}

		// Token: 0x06008A26 RID: 35366 RVA: 0x002ED8A2 File Offset: 0x002EBAA2
		private void VideoPlayMessageCallback(NKCUIComVideoPlayer.eVideoMessage message)
		{
			switch (message)
			{
			case NKCUIComVideoPlayer.eVideoMessage.PlayFailed:
			case NKCUIComVideoPlayer.eVideoMessage.PlayComplete:
				this.m_bWaitingMovie = false;
				break;
			case NKCUIComVideoPlayer.eVideoMessage.PlayBegin:
				break;
			default:
				return;
			}
		}

		// Token: 0x06008A27 RID: 35367 RVA: 0x002ED8C0 File Offset: 0x002EBAC0
		private void SetUI()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_ROOT, true);
			NKCUtil.SetGameobjectActive(this.m_objEntryFeeReturn, false);
			if (this.GetIsWinGame())
			{
				NKCUIWarfareResult.USE_CONTENTS eContents = this.m_eContents;
				string stateName;
				if (eContents == NKCUIWarfareResult.USE_CONTENTS.WARFARE || eContents == NKCUIWarfareResult.USE_CONTENTS.DUNGEON)
				{
					stateName = "NKM_UI_WARFARE_RESULT_INTRO";
				}
				else
				{
					stateName = "NKM_UI_DIVE_RESULT_INTRO";
				}
				this.m_Animtor.Play(stateName);
				NKCSoundManager.PlayMusic("UI_WARFARE_RESULT_WIN", true, 1f, false, 0f, 0f);
				NKCUtil.SetGameobjectActive(this.m_PARTICLEOBJECT, true);
			}
			else
			{
				NKCUIWarfareResult.USE_CONTENTS eContents = this.m_eContents;
				string stateName2;
				if (eContents == NKCUIWarfareResult.USE_CONTENTS.WARFARE || eContents == NKCUIWarfareResult.USE_CONTENTS.DUNGEON)
				{
					stateName2 = "NKM_UI_WARFARE_RESULT_INTRO_DEFEAT";
				}
				else
				{
					stateName2 = "NKM_UI_DIVE_RESULT_INTRO_FAILED";
				}
				this.m_Animtor.Play(stateName2);
				NKCSoundManager.PlayMusic("UI_WARFARE_RESULT_LOSE", true, 1f, false, 0f, 0f);
			}
			this.m_fElapsedTime = 0f;
			switch (this.m_eContents)
			{
			case NKCUIWarfareResult.USE_CONTENTS.WARFARE:
			case NKCUIWarfareResult.USE_CONTENTS.DUNGEON:
				this.SetUIByGrade(this.GetGrade());
				this.SetUIByLeader(this.m_CurrentDeck, this.m_dummyUnitID, this.m_dummyShipID);
				this.SetUIByMission();
				this.SetBtnUI();
				this.SetBG();
				this.SetUIGameTip();
				return;
			}
			this.SetUIByGrade(this.GetGrade());
			this.SetUIByLeader(this.m_CurrentDeck, this.m_dummyUnitID, this.m_dummyShipID);
			NKCUtil.SetGameobjectActive(this.m_csbtnCloseToHome, true);
			NKCUtil.SetGameobjectActive(this.m_csbtnCloseToList, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnRestart, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnConfirm, true);
			NKCUtil.SetGameobjectActive(this.m_csbtnNextMission, false);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_REPEAT, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnBattleStat, false);
			this.SetBG();
			this.SetUIGameTip();
		}

		// Token: 0x06008A28 RID: 35368 RVA: 0x002EDA70 File Offset: 0x002EBC70
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
					subUICameraVideoPlayer.m_fMoviePlaySpeed = this.MoviePlaySpeed;
					subUICameraVideoPlayer.Play("WarfareResultBG.mp4", true, false, null, false);
				}
			}
		}

		// Token: 0x06008A29 RID: 35369 RVA: 0x002EDAD8 File Offset: 0x002EBCD8
		public void SetBtnUI()
		{
			NKCRepeatOperaion nkcrepeatOperaion = NKCScenManager.GetScenManager().GetNKCRepeatOperaion();
			if (nkcrepeatOperaion.GetIsOnGoing())
			{
				NKCUtil.SetGameobjectActive(this.m_csbtnCloseToHome, false);
				NKCUtil.SetGameobjectActive(this.m_csbtnCloseToList, false);
				NKCUtil.SetGameobjectActive(this.m_csbtnConfirm, false);
				NKCUtil.SetGameobjectActive(this.m_csbtnNextMission, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_REPEAT, true);
				NKCUtil.SetGameobjectActive(this.m_csbtnRestart, false);
				NKCUtil.SetLabelText(this.m_NKM_UI_WARFARE_RESULT_REPEAT_COUNT, string.Format("({0}/{1})", nkcrepeatOperaion.GetCurrRepeatCount(), nkcrepeatOperaion.GetMaxRepeatCount()));
				NKCUtil.SetLabelText(this.m_NKM_UI_WARFARE_RESULT_BTN_REPEAT_COUNT_DOWN, ((int)this.m_fFinalAutoProcessWaitTime).ToString());
				this.m_NKM_UI_WARFARE_RESULT_BTN_REPEAT_COUNT_DOWN_Gauge.fillAmount = 0f;
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_csbtnCloseToHome, true);
			NKCUtil.SetGameobjectActive(this.m_csbtnCloseToList, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_REPEAT, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnRestart, true);
			if (this.GetPossibleNextOperation() != null)
			{
				NKCUtil.SetGameobjectActive(this.m_csbtnConfirm, false);
				NKCUtil.SetGameobjectActive(this.m_csbtnNextMission, true);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_csbtnConfirm, true);
				NKCUtil.SetGameobjectActive(this.m_csbtnNextMission, false);
			}
			NKMStageTempletV2 currentStageTemplet = this.GetCurrentStageTemplet();
			if (currentStageTemplet != null)
			{
				this.m_csbtnRestart.PointerClick.RemoveAllListeners();
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (currentStageTemplet.EnterLimit > 0 && nkmuserData.GetStatePlayCnt(currentStageTemplet.Key, false, false, false) >= currentStageTemplet.EnterLimit && nkmuserData.GetStageRestoreCnt(currentStageTemplet.Key) >= currentStageTemplet.RestoreLimit)
				{
					this.m_csbtnRestart.PointerClick.AddListener(delegate()
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_ENTER_LIMIT_OVER, null, "");
					});
				}
				else
				{
					this.m_csbtnRestart.PointerClick.AddListener(new UnityAction(this.OnClickRetry));
				}
			}
			NKCUtil.SetGameobjectActive(this.m_csbtnBattleStat, this.m_battleResultData != null && this.m_battleResultData.m_battleData != null);
		}

		// Token: 0x06008A2A RID: 35370 RVA: 0x002EDCCC File Offset: 0x002EBECC
		private NKMStageTempletV2 GetCurrentStageTemplet()
		{
			NKCUIWarfareResult.USE_CONTENTS eContents = this.m_eContents;
			if (eContents != NKCUIWarfareResult.USE_CONTENTS.WARFARE)
			{
				if (eContents != NKCUIWarfareResult.USE_CONTENTS.DUNGEON)
				{
					return null;
				}
				if (this.m_battleResultData == null)
				{
					return null;
				}
				return NKMStageTempletV2.Find(this.m_battleResultData.m_stageID);
			}
			else
			{
				if (this.m_NKMWarfareClearData == null)
				{
					return null;
				}
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_NKMWarfareClearData.m_WarfareID);
				return NKMEpisodeMgr.FindStageTempletByBattleStrID(((nkmwarfareTemplet != null) ? nkmwarfareTemplet.m_WarfareStrID : null) ?? string.Empty);
			}
		}

		// Token: 0x06008A2B RID: 35371 RVA: 0x002EDD38 File Offset: 0x002EBF38
		private NKMStageTempletV2 GetPossibleNextOperation()
		{
			NKMStageTempletV2 currentStageTemplet = this.GetCurrentStageTemplet();
			if (currentStageTemplet != null)
			{
				NKMEpisodeTempletV2 episodeTemplet = currentStageTemplet.EpisodeTemplet;
				if (episodeTemplet != null && episodeTemplet.m_DicStage.ContainsKey(currentStageTemplet.ActId))
				{
					bool flag = false;
					int i = 0;
					while (i < episodeTemplet.m_DicStage[currentStageTemplet.ActId].Count)
					{
						if (flag)
						{
							NKMStageTempletV2 nkmstageTempletV = episodeTemplet.m_DicStage[currentStageTemplet.ActId][i];
							NKMUserData cNKMUserData = NKCScenManager.CurrentUserData();
							bool flag2 = false;
							if (nkmstageTempletV.m_STAGE_TYPE == STAGE_TYPE.ST_DUNGEON)
							{
								NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(nkmstageTempletV.m_StageBattleStrID);
								if (dungeonTempletBase != null && dungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_CUTSCENE)
								{
									flag2 = true;
								}
							}
							if (!flag2 && NKMEpisodeMgr.CheckEpisodeMission(cNKMUserData, nkmstageTempletV))
							{
								return nkmstageTempletV;
							}
							return null;
						}
						else
						{
							if (episodeTemplet.m_DicStage[currentStageTemplet.ActId][i].Key == currentStageTemplet.Key)
							{
								flag = true;
							}
							i++;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06008A2C RID: 35372 RVA: 0x002EDE28 File Offset: 0x002EC028
		public void PlayNextOperation()
		{
			NKMStageTempletV2 possibleNextOperation = this.GetPossibleNextOperation();
			if (possibleNextOperation != null)
			{
				NKC_SCEN_OPERATION_V2 scen_OPERATION = NKCScenManager.GetScenManager().Get_SCEN_OPERATION();
				if (scen_OPERATION != null)
				{
					NKCScenManager.GetScenManager().GetNKCRepeatOperaion().Init();
					scen_OPERATION.SetReservedStage(possibleNextOperation);
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OPERATION, true);
				}
			}
		}

		// Token: 0x06008A2D RID: 35373 RVA: 0x002EDE70 File Offset: 0x002EC070
		private bool CheckSpecialEventExist()
		{
			return NKCContentManager.CheckLevelChanged() || NKCContentManager.HasUnlockedContent(new STAGE_UNLOCK_REQ_TYPE[]
			{
				this.GetReqType()
			}) || NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetAlarmRepeatOperationQuitByDefeat() || NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetAlarmRepeatOperationSuccess();
		}

		// Token: 0x06008A2E RID: 35374 RVA: 0x002EDEC8 File Offset: 0x002EC0C8
		private STAGE_UNLOCK_REQ_TYPE GetReqType()
		{
			switch (this.m_eContents)
			{
			case NKCUIWarfareResult.USE_CONTENTS.DIVE:
				return STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DIVE;
			case NKCUIWarfareResult.USE_CONTENTS.SHADOW:
				return STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_PALACE;
			case NKCUIWarfareResult.USE_CONTENTS.DUNGEON:
				return STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DUNGEON;
			}
			return STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_WARFARE;
		}

		// Token: 0x06008A2F RID: 35375 RVA: 0x002EDF04 File Offset: 0x002EC104
		private void Update()
		{
			if (this.m_bFinishedIntroMovie)
			{
				if (!this.m_bPause)
				{
					this.m_fElapsedTime += Time.deltaTime;
				}
				if (this.m_eContents == NKCUIWarfareResult.USE_CONTENTS.DIVE)
				{
					if (this.m_fElapsedTime > 2.1833334f && !this.m_bDoneMakeRewardSlot)
					{
						this.m_bDoneMakeRewardSlot = true;
						this.SetUIByReward();
						if (this.m_bReservedShowGetUnit)
						{
							this.m_Animtor.speed = 0f;
						}
					}
					if (this.m_bReservedShowGetUnit && this.m_fElapsedTime > this.m_fTimeToShowGetUnit)
					{
						this.m_bReservedShowGetUnit = false;
						NKCUIGameResultGetUnit.ShowNewUnitGetUI(this.m_DiveRewardData, new NKCUIGameResultGetUnit.NKCUIGRGetUnitCallBack(this.GetUnitCallback), NKCScenManager.GetScenManager().GetMyUserData().m_UserOption.m_bAutoDive, true, true);
						NKCUtil.SetGameobjectActive(this.m_PARTICLEOBJECT, false);
					}
					if (this.m_bArtifactReturnEvent)
					{
						if (!this.NKCPopupArtifactExchange.IsOpen && this.m_Animtor.speed == 1f && this.m_fElapsedTime > 2.466667f)
						{
							this.m_bPause = true;
							this.NKCPopupArtifactExchange.Open(this.m_lstArtifact, NKMCommonConst.DiveArtifactReturnItemId, delegate
							{
								this.m_bArtifactReturnEvent = false;
								this.m_bPause = false;
							});
						}
					}
					else if (NKCContentManager.CheckLevelChanged())
					{
						if (!this.m_bUserLevelUpPopupOpened && this.m_Animtor.speed == 1f && this.m_fElapsedTime > 2.466667f)
						{
							this.m_bUserLevelUpPopupOpened = true;
							NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
							if (nkmuserData != null)
							{
								NKCPopupUserLevelUp.instance.Open(nkmuserData, new Action(this.OnCloseUserLevelUpPopup));
							}
						}
					}
					else if (NKCContentManager.HasUnlockedContent(new STAGE_UNLOCK_REQ_TYPE[]
					{
						STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DIVE
					}) || this.m_bWaitContentUnlockPopup)
					{
						if (!this.m_bWaitContentUnlockPopup && this.m_Animtor.speed == 1f && this.m_fElapsedTime > 2.466667f)
						{
							this.m_bWaitContentUnlockPopup = true;
							NKCContentManager.ShowContentUnlockPopup(new NKCContentManager.OnClose(this.OnCloseContentUnlockPopup), new STAGE_UNLOCK_REQ_TYPE[]
							{
								STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DIVE
							});
						}
					}
					else if (NKCScenManager.GetScenManager().GetMyUserData().m_UserOption.m_bAutoDive && !this.m_bTriggeredAutoOK && this.m_fElapsedTime > 3.466667f + this.m_fFinalAutoProcessWaitTime)
					{
						this.m_bTriggeredAutoOK = true;
						this.CloseToOK();
					}
				}
				else if (this.m_eContents == NKCUIWarfareResult.USE_CONTENTS.SHADOW)
				{
					if (this.m_fElapsedTime > 2.1833334f && !this.m_bDoneMakeRewardSlot)
					{
						this.m_bDoneMakeRewardSlot = true;
						this.SetUIByReward();
						if (this.m_bReservedShowGetUnit)
						{
							this.m_Animtor.speed = 0f;
						}
					}
					if (this.m_bReservedShowGetUnit && this.m_fElapsedTime > this.m_fTimeToShowGetUnit)
					{
						this.m_bReservedShowGetUnit = false;
						NKCUIGameResultGetUnit.ShowNewUnitGetUI(this.m_ShadowResult.rewardData, new NKCUIGameResultGetUnit.NKCUIGRGetUnitCallBack(this.GetUnitCallback), false, true, true);
						NKCUtil.SetGameobjectActive(this.m_PARTICLEOBJECT, false);
					}
					if (this.m_bShadowRecordEvent)
					{
						if (!NKCPopupShadowRecord.IsInstanceOpen && this.m_Animtor.speed == 1f && this.m_fElapsedTime > 2.466667f)
						{
							this.m_bPause = true;
							NKCPopupShadowRecord.Instance.Open(this.m_ShadowResult, this.m_lstShadowBestTime, delegate
							{
								this.m_bShadowRecordEvent = false;
								this.m_bPause = false;
							});
						}
					}
					else if (NKCContentManager.CheckLevelChanged())
					{
						if (!this.m_bUserLevelUpPopupOpened && this.m_Animtor.speed == 1f && this.m_fElapsedTime > 2.466667f)
						{
							this.m_bUserLevelUpPopupOpened = true;
							NKMUserData nkmuserData2 = NKCScenManager.CurrentUserData();
							if (nkmuserData2 != null)
							{
								NKCPopupUserLevelUp.instance.Open(nkmuserData2, new Action(this.OnCloseUserLevelUpPopup));
							}
						}
					}
					else if ((NKCContentManager.HasUnlockedContent(new STAGE_UNLOCK_REQ_TYPE[]
					{
						STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_PALACE
					}) || this.m_bWaitContentUnlockPopup) && !this.m_bWaitContentUnlockPopup && this.m_Animtor.speed == 1f && this.m_fElapsedTime > 2.466667f)
					{
						this.m_bWaitContentUnlockPopup = true;
						NKCContentManager.ShowContentUnlockPopup(new NKCContentManager.OnClose(this.OnCloseContentUnlockPopup), new STAGE_UNLOCK_REQ_TYPE[]
						{
							STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_PALACE
						});
					}
				}
				else if (this.m_eContents == NKCUIWarfareResult.USE_CONTENTS.TRIM)
				{
					if (this.m_fElapsedTime > 2.1833334f && !this.m_bDoneMakeRewardSlot)
					{
						this.m_bDoneMakeRewardSlot = true;
						this.SetUIByReward();
						if (this.m_bReservedShowGetUnit)
						{
							this.m_Animtor.speed = 0f;
						}
					}
					if (this.m_bReservedShowGetUnit && this.m_fElapsedTime > this.m_fTimeToShowGetUnit)
					{
						this.m_bReservedShowGetUnit = false;
						NKCUIGameResultGetUnit.ShowNewUnitGetUI(this.m_ShadowResult.rewardData, new NKCUIGameResultGetUnit.NKCUIGRGetUnitCallBack(this.GetUnitCallback), false, true, true);
						NKCUtil.SetGameobjectActive(this.m_PARTICLEOBJECT, false);
					}
					if (this.m_bTrimEvent)
					{
						if (!NKCPopupTrimScoreResult.IsInstanceOpen && this.m_Animtor.speed == 1f && this.m_fElapsedTime > 2.466667f)
						{
							this.m_bPause = true;
							NKCPopupTrimScoreResult.Instance.Open(this.m_trimModeState, this.m_trimClearData.score, this.m_trimBestScore, delegate
							{
								this.m_bTrimEvent = false;
								this.m_bPause = false;
							});
						}
					}
					else if (NKCContentManager.CheckLevelChanged() && !this.m_bUserLevelUpPopupOpened && this.m_Animtor.speed == 1f && this.m_fElapsedTime > 2.466667f)
					{
						this.m_bUserLevelUpPopupOpened = true;
						NKMUserData nkmuserData3 = NKCScenManager.CurrentUserData();
						if (nkmuserData3 != null)
						{
							NKCPopupUserLevelUp.instance.Open(nkmuserData3, new Action(this.OnCloseUserLevelUpPopup));
						}
					}
				}
				else
				{
					if (this.m_fElapsedTime > 2.1833334f && !this.m_bDoneMakeRewardSlot)
					{
						this.m_bDoneMakeRewardSlot = true;
						this.SetUIByReward();
						if (this.m_bReservedShowGetUnit)
						{
							this.m_Animtor.speed = 0f;
						}
					}
					if (this.m_bReservedShowGetUnit && this.m_fElapsedTime > this.m_fTimeToShowGetUnit)
					{
						List<NKMRewardData> list = new List<NKMRewardData>();
						NKCUIWarfareResult.USE_CONTENTS eContents = this.m_eContents;
						if (eContents != NKCUIWarfareResult.USE_CONTENTS.WARFARE)
						{
							if (eContents == NKCUIWarfareResult.USE_CONTENTS.DUNGEON)
							{
								if (this.m_battleResultData != null)
								{
									if (this.m_battleResultData.m_firstRewardData != null)
									{
										list.Add(this.m_battleResultData.m_firstRewardData);
									}
									if (this.m_battleResultData.m_RewardData != null)
									{
										list.Add(this.m_battleResultData.m_RewardData);
									}
									if (this.m_battleResultData.m_OnetimeRewardData != null)
									{
										list.Add(this.m_battleResultData.m_OnetimeRewardData);
									}
									if (this.m_battleResultData.m_firstAllClearData != null)
									{
										list.Add(this.m_battleResultData.m_firstAllClearData);
									}
								}
							}
						}
						else
						{
							if (this.m_NKMWarfareClearData.m_RewardDataList != null)
							{
								list.Add(this.m_NKMWarfareClearData.m_RewardDataList);
							}
							if (this.m_NKMWarfareClearData.m_OnetimeRewards != null)
							{
								list.Add(this.m_NKMWarfareClearData.m_OnetimeRewards);
							}
						}
						this.m_bReservedShowGetUnit = false;
						NKCUIGameResultGetUnit.ShowNewUnitGetUI(list, new NKCUIGameResultGetUnit.NKCUIGRGetUnitCallBack(this.GetUnitCallback), NKCScenManager.GetScenManager().GetMyUserData().m_UserOption.m_bAutoWarfare, true, true);
						NKCUtil.SetGameobjectActive(this.m_PARTICLEOBJECT, false);
					}
					if (NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing())
					{
						float num = this.m_fFinalAutoProcessWaitTime - (this.m_fElapsedTime - 3.466667f);
						if (num < 0f)
						{
							num = 0f;
						}
						int num2 = Mathf.CeilToInt(num);
						NKCUtil.SetLabelText(this.m_NKM_UI_WARFARE_RESULT_BTN_REPEAT_COUNT_DOWN, num2.ToString());
						this.m_NKM_UI_WARFARE_RESULT_BTN_REPEAT_COUNT_DOWN_Gauge.fillAmount = (float)num2 - num;
					}
					if (NKCContentManager.CheckLevelChanged())
					{
						if (!this.m_bUserLevelUpPopupOpened && this.IsPopupTiming())
						{
							this.m_bUserLevelUpPopupOpened = true;
							NKMUserData nkmuserData4 = NKCScenManager.CurrentUserData();
							if (nkmuserData4 != null)
							{
								NKCPopupUserLevelUp.instance.Open(nkmuserData4, new Action(this.OnCloseUserLevelUpPopup));
							}
						}
					}
					else if (NKCContentManager.HasUnlockedContent(new STAGE_UNLOCK_REQ_TYPE[1]) || this.m_bWaitContentUnlockPopup)
					{
						if (!this.m_bWaitContentUnlockPopup && this.IsPopupTiming())
						{
							this.m_bWaitContentUnlockPopup = true;
							NKCContentManager.ShowContentUnlockPopup(new NKCContentManager.OnClose(this.OnCloseContentUnlockPopup), new STAGE_UNLOCK_REQ_TYPE[1]);
						}
					}
					else if (this.m_guestSptData != null && !NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing())
					{
						if (!this.m_bWaitFriendRequestPopup && this.IsPopupTiming())
						{
							this.m_bWaitFriendRequestPopup = true;
							NKCPopupFriendRequest.Instance.Open(this.m_guestSptData, new UnityAction(this.OnCloseFriendRequestPopup));
						}
					}
					else if (NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetAlarmRepeatOperationQuitByDefeat() && this.IsPopupTiming())
					{
						if (!NKCPopupRepeatOperation.IsInstanceOpen)
						{
							this.m_bPause = true;
							NKCScenManager.GetScenManager().GetNKCRepeatOperaion().Init();
							NKCScenManager.GetScenManager().GetNKCRepeatOperaion().SetStopReason(NKCStringTable.GetString("SI_POPUP_REPEAT_FAIL_DEFEAT", false));
							NKCPopupRepeatOperation.Instance.OpenForResult(delegate
							{
								this.m_bPause = false;
								NKCScenManager.GetScenManager().GetNKCRepeatOperaion().SetAlarmRepeatOperationQuitByDefeat(false);
							});
						}
					}
					else if (NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetAlarmRepeatOperationSuccess() && this.IsPopupTiming())
					{
						if (!NKCPopupRepeatOperation.IsInstanceOpen)
						{
							this.m_bPause = true;
							NKCScenManager.GetScenManager().GetNKCRepeatOperaion().Init();
							NKCScenManager.GetScenManager().GetNKCRepeatOperaion().SetStopReason(NKCUtilString.GET_STRING_REPEAT_OPERATION_IS_TERMINATED);
							NKCPopupRepeatOperation.Instance.OpenForResult(delegate
							{
								this.m_bPause = false;
								NKCScenManager.GetScenManager().GetNKCRepeatOperaion().SetAlarmRepeatOperationSuccess(false);
							});
						}
					}
					else if (NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing())
					{
						if (!this.m_bTriggeredAutoOK && this.m_fElapsedTime > 3.466667f + this.m_fFinalAutoProcessWaitTime)
						{
							this.m_bTriggeredAutoOK = true;
							this.Retry();
						}
					}
					else
					{
						if (!this.m_bRequiredTutorial && NKCScenManager.GetScenManager().GetMyUserData().m_UserOption.m_bAutoWarfare && !this.m_bTriggeredAutoOK && this.m_fElapsedTime > 3.466667f + this.m_fFinalAutoProcessWaitTime)
						{
							this.m_bTriggeredAutoOK = true;
							this.CloseToOK();
						}
						if (this.m_bRequiredTutorial && this.m_Animtor.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8701923f)
						{
							NKCUtil.SetGameobjectActive(this.m_csbtnCloseToHome, true);
							NKCTutorialManager.TutorialRequired(TutorialPoint.WarfareResult, true);
							this.m_bRequiredTutorial = false;
							this.m_bTriggeredAutoOK = true;
						}
					}
				}
				for (int i = 0; i < this.m_lstNKCUIWRRewardSlot.Count; i++)
				{
					if (this.m_lstNKCUIWRRewardSlot[i] != null)
					{
						this.m_lstNKCUIWRRewardSlot[i].ManualUpdate();
					}
				}
			}
		}

		// Token: 0x06008A30 RID: 35376 RVA: 0x002EE94C File Offset: 0x002ECB4C
		private void GetUnitCallback()
		{
			this.m_Animtor.speed = 1f;
		}

		// Token: 0x06008A31 RID: 35377 RVA: 0x002EE95E File Offset: 0x002ECB5E
		private void OnCloseUserLevelUpPopup()
		{
			this.m_bUserLevelUpPopupOpened = false;
			NKCContentManager.SetLevelChanged(false);
		}

		// Token: 0x06008A32 RID: 35378 RVA: 0x002EE96D File Offset: 0x002ECB6D
		private void OnCloseContentUnlockPopup()
		{
			this.m_bWaitContentUnlockPopup = false;
		}

		// Token: 0x06008A33 RID: 35379 RVA: 0x002EE976 File Offset: 0x002ECB76
		private void OnCloseFriendRequestPopup()
		{
			this.m_guestSptData = null;
			this.m_bWaitFriendRequestPopup = false;
		}

		// Token: 0x06008A34 RID: 35380 RVA: 0x002EE986 File Offset: 0x002ECB86
		public override void UnHide()
		{
			base.UnHide();
			this.m_Animtor.Play("NKM_UI_WARFARE_RESULT_INTRO", -1, 0.8173077f);
		}

		// Token: 0x06008A35 RID: 35381 RVA: 0x002EE9A4 File Offset: 0x002ECBA4
		private void UpdateRewardList(ref List<NKCUISlot.SlotData> lstReward, ref int globalIndex, NKCUIWarfareResult.eRewardType type)
		{
			List<NKCUISlot.SlotData> list = new List<NKCUISlot.SlotData>();
			if (type == NKCUIWarfareResult.eRewardType.FirstClear && !this.m_bfirstClear)
			{
				return;
			}
			if (type == NKCUIWarfareResult.eRewardType.FirstAllClear && !this.m_bFirstAllClear)
			{
				return;
			}
			if (this.m_NKMWarfareClearData != null)
			{
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_NKMWarfareClearData.m_WarfareID);
				if (nkmwarfareTemplet != null)
				{
					NKCUISlot.SlotData slotData = null;
					int num = 0;
					if (type == NKCUIWarfareResult.eRewardType.FirstClear)
					{
						FirstRewardData firstRewardData = nkmwarfareTemplet.GetFirstRewardData();
						if (firstRewardData.Type != NKM_REWARD_TYPE.RT_NONE)
						{
							slotData = NKCUISlot.SlotData.MakeRewardTypeData(firstRewardData.Type, firstRewardData.RewardId, firstRewardData.RewardQuantity, 0);
							num = firstRewardData.RewardQuantity;
						}
					}
					else if (type == NKCUIWarfareResult.eRewardType.FirstAllClear)
					{
						NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(nkmwarfareTemplet.m_WarfareStrID);
						if (nkmstageTempletV == null || nkmstageTempletV.MissionReward == null || nkmstageTempletV.MissionReward.rewardType == NKM_REWARD_TYPE.RT_NONE)
						{
							return;
						}
						if (!NKMRewardTemplet.IsOpenedReward(nkmstageTempletV.MissionReward.rewardType, nkmstageTempletV.MissionReward.ID, false))
						{
							return;
						}
						slotData = NKCUISlot.SlotData.MakeRewardTypeData(nkmstageTempletV.MissionReward.rewardType, nkmstageTempletV.MissionReward.ID, nkmstageTempletV.MissionReward.Count, 0);
						num = nkmstageTempletV.MissionReward.Count;
					}
					if (slotData != null)
					{
						List<NKCUISlot.SlotData> list2 = new List<NKCUISlot.SlotData>();
						for (int i = 0; i < lstReward.Count; i++)
						{
							NKCUISlot.SlotData slotData2 = lstReward[i];
							if (slotData2.eType == slotData.eType && slotData2.ID == slotData.ID)
							{
								list2.Add(slotData2);
							}
							else if (slotData2.eType == NKCUISlot.eSlotMode.Unit && slotData.eType == NKCUISlot.eSlotMode.UnitCount && slotData2.ID == slotData.ID)
							{
								list2.Add(slotData2);
							}
						}
						for (int j = 0; j < list2.Count; j++)
						{
							NKCUISlot.SlotData slotData3 = list2[j];
							if (slotData3.eType != NKCUISlot.eSlotMode.Unit && slotData3.eType != NKCUISlot.eSlotMode.UnitCount)
							{
								slotData3.Count -= (long)num;
								num = 0;
								if (slotData3.Count <= 0L)
								{
									lstReward.Remove(slotData3);
								}
								list.Add(slotData);
							}
							else
							{
								lstReward.Remove(slotData3);
								list.Add(slotData3);
								num--;
							}
							if (num == 0)
							{
								break;
							}
						}
					}
				}
			}
			if (list.Count > 0)
			{
				int count = this.m_lstNKCUIWRRewardSlot.Count;
				if (count + list.Count > this.m_lstNKCUIWRRewardSlot.Count)
				{
					int newCount = count + list.Count - this.m_lstNKCUIWRRewardSlot.Count;
					this.AddExtraRewardSlot(newCount);
				}
				for (int k = 0; k < list.Count; k++)
				{
					NKCUISlot.SlotData slotData4 = list[k];
					this.m_lstNKCUIWRRewardSlot[globalIndex].SetUI(slotData4, globalIndex);
					this.m_lstNKCUIWRRewardSlot[globalIndex].SetMultiplyMark(false);
					if (type == NKCUIWarfareResult.eRewardType.FirstClear)
					{
						this.m_lstNKCUIWRRewardSlot[globalIndex].SetFirstMark(true);
					}
					else if (type == NKCUIWarfareResult.eRewardType.FirstAllClear)
					{
						this.m_lstNKCUIWRRewardSlot[globalIndex].SetFirstAllClearMark(true);
					}
					globalIndex++;
				}
			}
		}

		// Token: 0x06008A36 RID: 35382 RVA: 0x002EEC8C File Offset: 0x002ECE8C
		private void ApplyRewardData(NKMRewardData rewardData, ref int globalIndex, bool bMultiply, NKCUIWarfareResult.eRewardType extraType)
		{
			List<NKCUISlot.SlotData> list = NKCUISlot.MakeSlotDataListFromReward(rewardData, false, false);
			int count = this.m_lstNKCUIWRRewardSlot.Count;
			int num = globalIndex + list.Count;
			if (num > count)
			{
				this.AddExtraRewardSlot(num - count);
			}
			foreach (NKCUISlot.SlotData slotData in list)
			{
				this.m_lstNKCUIWRRewardSlot[globalIndex].SetUI(slotData, globalIndex);
				this.m_lstNKCUIWRRewardSlot[globalIndex].SetMultiplyMark(bMultiply && extraType == NKCUIWarfareResult.eRewardType.None);
				if (extraType == NKCUIWarfareResult.eRewardType.FirstClear)
				{
					this.m_lstNKCUIWRRewardSlot[globalIndex].SetFirstMark(true);
				}
				else if (extraType == NKCUIWarfareResult.eRewardType.FirstAllClear)
				{
					this.m_lstNKCUIWRRewardSlot[globalIndex].SetFirstAllClearMark(true);
				}
				globalIndex++;
			}
		}

		// Token: 0x06008A37 RID: 35383 RVA: 0x002EED6C File Offset: 0x002ECF6C
		private void ApplyRewardData(NKMAdditionalReward rewardData, ref int globalIndex, bool bMultiply, NKCUIWarfareResult.eRewardType extraType)
		{
			List<NKCUISlot.SlotData> list = NKCUISlot.MakeSlotDataListFromReward(rewardData);
			int count = this.m_lstNKCUIWRRewardSlot.Count;
			int num = globalIndex + list.Count;
			if (num > count)
			{
				this.AddExtraRewardSlot(num - count);
			}
			foreach (NKCUISlot.SlotData slotData in list)
			{
				this.m_lstNKCUIWRRewardSlot[globalIndex].SetUI(slotData, globalIndex);
				this.m_lstNKCUIWRRewardSlot[globalIndex].SetMultiplyMark(bMultiply && extraType == NKCUIWarfareResult.eRewardType.None);
				if (extraType == NKCUIWarfareResult.eRewardType.FirstClear)
				{
					this.m_lstNKCUIWRRewardSlot[globalIndex].SetFirstMark(true);
				}
				else if (extraType == NKCUIWarfareResult.eRewardType.FirstAllClear)
				{
					this.m_lstNKCUIWRRewardSlot[globalIndex].SetFirstAllClearMark(true);
				}
				globalIndex++;
			}
		}

		// Token: 0x06008A38 RID: 35384 RVA: 0x002EEE4C File Offset: 0x002ED04C
		private void SetUIByReward()
		{
			if (this.m_eContents == NKCUIWarfareResult.USE_CONTENTS.DIVE)
			{
				int i = 0;
				int num = 0;
				int num2 = 0;
				if (this.m_DiveRewardDataArtifact != null)
				{
					List<NKCUISlot.SlotData> list = NKCUISlot.MakeSlotDataListFromReward(this.m_DiveRewardDataArtifact, false, false);
					num = list.Count;
					if (list.Count > this.m_lstNKCUIWRRewardSlot.Count)
					{
						int num3 = list.Count - this.m_lstNKCUIWRRewardSlot.Count;
						for (int j = 0; j < num3; j++)
						{
							NKCUIWRRewardSlot newInstance = NKCUIWRRewardSlot.GetNewInstance(this.m_NKM_UI_WARFARE_RESULT_REWARDS_Content);
							this.m_lstNKCUIWRRewardSlot.Add(newInstance);
						}
					}
					for (i = 0; i < list.Count; i++)
					{
						NKCUISlot.SlotData slotData = list[i];
						this.m_lstNKCUIWRRewardSlot[i].SetUI(slotData, i);
						this.m_lstNKCUIWRRewardSlot[i].SetArtifactMark(true);
						this.m_lstNKCUIWRRewardSlot[i].SetDiveStormMark(false);
					}
				}
				if (this.m_DiveRewardData != null)
				{
					List<NKCUISlot.SlotData> list2 = NKCUISlot.MakeSlotDataListFromReward(this.m_DiveRewardData, false, true);
					num2 = list2.Count;
					if (list2.Count > this.m_lstNKCUIWRRewardSlot.Count - num)
					{
						int num4 = list2.Count - (this.m_lstNKCUIWRRewardSlot.Count - num);
						for (int k = 0; k < num4; k++)
						{
							NKCUIWRRewardSlot newInstance2 = NKCUIWRRewardSlot.GetNewInstance(this.m_NKM_UI_WARFARE_RESULT_REWARDS_Content);
							this.m_lstNKCUIWRRewardSlot.Add(newInstance2);
						}
					}
					for (int l = 0; l < list2.Count; l++)
					{
						NKCUISlot.SlotData slotData2 = list2[l];
						this.m_lstNKCUIWRRewardSlot[i].SetUI(slotData2, i);
						this.m_lstNKCUIWRRewardSlot[i].SetArtifactMark(false);
						this.m_lstNKCUIWRRewardSlot[i].SetDiveStormMark(false);
						i++;
					}
				}
				if (this.m_DiveRewardDataStorm != null)
				{
					List<NKCUISlot.SlotData> list3 = NKCUISlot.MakeSlotDataListFromReward(this.m_DiveRewardDataStorm, false, false);
					if (list3.Count > this.m_lstNKCUIWRRewardSlot.Count - num - num2)
					{
						int num5 = list3.Count - (this.m_lstNKCUIWRRewardSlot.Count - num - num2);
						for (int m = 0; m < num5; m++)
						{
							NKCUIWRRewardSlot newInstance3 = NKCUIWRRewardSlot.GetNewInstance(this.m_NKM_UI_WARFARE_RESULT_REWARDS_Content);
							this.m_lstNKCUIWRRewardSlot.Add(newInstance3);
						}
					}
					for (int n = 0; n < list3.Count; n++)
					{
						NKCUISlot.SlotData slotData3 = list3[n];
						this.m_lstNKCUIWRRewardSlot[i].SetUI(slotData3, i);
						this.m_lstNKCUIWRRewardSlot[i].SetArtifactMark(false);
						this.m_lstNKCUIWRRewardSlot[i].SetDiveStormMark(true);
						i++;
					}
					return;
				}
			}
			else if (this.m_eContents == NKCUIWarfareResult.USE_CONTENTS.SHADOW)
			{
				if (this.m_ShadowResult != null && this.m_ShadowResult.rewardData != null)
				{
					List<NKCUISlot.SlotData> list4 = NKCUISlot.MakeSlotDataListFromReward(this.m_ShadowResult.rewardData, false, false);
					int count = list4.Count;
					int count2 = this.m_lstNKCUIWRRewardSlot.Count;
					this.AddExtraRewardSlot(count - count2);
					NKMShadowPalace shadowPalace = NKCScenManager.GetScenManager().GetMyUserData().m_ShadowPalace;
					bool flag = shadowPalace.rewardMultiply > 1;
					if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.SHADOW_PALACE_MULTIPLY))
					{
						flag = false;
					}
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_MULTIPLY_REWARD_TAG, flag);
					NKCUtil.SetLabelText(this.m_MultiplyReward_COUNT_TEXT, string.Format(NKCUtilString.GET_MULTIPLY_REWARD_COUNT_PARAM_02, shadowPalace.rewardMultiply));
					for (int num6 = 0; num6 < count; num6++)
					{
						this.m_lstNKCUIWRRewardSlot[num6].SetUI(list4[num6], num6);
						this.m_lstNKCUIWRRewardSlot[num6].SetMultiplyMark(flag);
					}
					return;
				}
			}
			else if (this.m_eContents == NKCUIWarfareResult.USE_CONTENTS.TRIM)
			{
				if (this.m_trimClearData.rewardData != null)
				{
					int num7 = 0;
					int num8 = 0;
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_MULTIPLY_REWARD_TAG, false);
					List<NKCUISlot.SlotData> list5 = NKCUISlot.MakeSlotDataListFromReward(this.m_trimClearData.rewardData, false, false);
					NKCUISlot.SlotData firstReward = null;
					NKCTrimRewardTemplet nkctrimRewardTemplet = NKCTrimRewardTemplet.Find(this.m_trimClearData.trimId, this.m_trimClearData.trimLevel);
					if (this.m_bTrimFirstClear && nkctrimRewardTemplet != null)
					{
						firstReward = NKCUISlot.SlotData.MakeRewardTypeData(nkctrimRewardTemplet.FirstClearRewardType, nkctrimRewardTemplet.FirstClearRewardID, nkctrimRewardTemplet.FirstClearValue, 0);
					}
					if (firstReward != null)
					{
						if (list5.Find((NKCUISlot.SlotData e) => e.ID == firstReward.ID && e.eType == firstReward.eType) != null)
						{
							int count3 = this.m_lstNKCUIWRRewardSlot.Count;
							num8 = 1;
							this.AddExtraRewardSlot(num8 - count3);
							if (this.m_lstNKCUIWRRewardSlot.Count > num7)
							{
								this.m_lstNKCUIWRRewardSlot[num7].SetUI(firstReward, 0);
								this.m_lstNKCUIWRRewardSlot[num7].SetFirstMark(true);
								num7++;
							}
						}
						else
						{
							firstReward = null;
						}
					}
					if (list5 != null)
					{
						int count4 = list5.Count;
						int count5 = this.m_lstNKCUIWRRewardSlot.Count;
						this.AddExtraRewardSlot(count4 + num8 - count5);
						for (int num9 = 0; num9 < count4; num9++)
						{
							if (this.m_lstNKCUIWRRewardSlot.Count <= num7)
							{
								Log.Error("Trim reward slot out of index", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUIWarfareResult.cs", 1808);
								return;
							}
							if (firstReward != null && firstReward.ID == list5[num9].ID && firstReward.eType == list5[num9].eType)
							{
								list5[num9].Count = Math.Max(1L, list5[num9].Count - firstReward.Count);
							}
							this.m_lstNKCUIWRRewardSlot[num7].SetUI(list5[num9], num7);
							num7++;
						}
						return;
					}
				}
			}
			else if (this.m_eContents == NKCUIWarfareResult.USE_CONTENTS.WARFARE)
			{
				if (this.m_NKMWarfareClearData != null)
				{
					NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_NKMWarfareClearData.m_WarfareID);
					WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
					bool flag2 = warfareGameData.rewardMultiply > 1;
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_MULTIPLY_REWARD_TAG, flag2);
					NKCUtil.SetLabelText(this.m_MultiplyReward_COUNT_TEXT, string.Format(NKCUtilString.GET_MULTIPLY_REWARD_COUNT_PARAM_02, warfareGameData.rewardMultiply));
					List<NKCUISlot.SlotData> list6 = new List<NKCUISlot.SlotData>();
					if (this.m_NKMWarfareClearData.m_RewardDataList != null)
					{
						list6 = NKCUISlot.MakeSlotDataListFromReward(this.m_NKMWarfareClearData.m_RewardDataList, false, false);
					}
					if (this.m_NKMWarfareClearData.m_MissionReward != null && this.m_bFirstAllClear)
					{
						list6.AddRange(NKCUISlot.MakeSlotDataListFromReward(this.m_NKMWarfareClearData.m_MissionReward, false, false));
					}
					int num10 = 0;
					this.UpdateRewardList(ref list6, ref num10, NKCUIWarfareResult.eRewardType.FirstClear);
					this.UpdateRewardList(ref list6, ref num10, NKCUIWarfareResult.eRewardType.FirstAllClear);
					if (this.m_NKMWarfareClearData.m_OnetimeRewards != null)
					{
						List<NKCUISlot.SlotData> list7 = NKCUISlot.MakeSlotDataListFromReward(this.m_NKMWarfareClearData.m_OnetimeRewards, false, false);
						int count6 = this.m_lstNKCUIWRRewardSlot.Count;
						if (count6 + list7.Count > this.m_lstNKCUIWRRewardSlot.Count)
						{
							int newCount = count6 + list7.Count - this.m_lstNKCUIWRRewardSlot.Count;
							this.AddExtraRewardSlot(newCount);
						}
						for (int num11 = 0; num11 < list7.Count; num11++)
						{
							NKCUISlot.SlotData slotData4 = list7[num11];
							this.m_lstNKCUIWRRewardSlot[num10].SetUI(slotData4, num10);
							this.m_lstNKCUIWRRewardSlot[num10].SetChanceUpMark(true);
							this.m_lstNKCUIWRRewardSlot[num10].SetMultiplyMark(flag2);
							num10++;
						}
					}
					if (this.m_NKMWarfareClearData.m_ContainerRewards != null)
					{
						List<NKCUISlot.SlotData> list8 = NKCUISlot.MakeSlotDataListFromReward(this.m_NKMWarfareClearData.m_ContainerRewards, false, false);
						int count7 = this.m_lstNKCUIWRRewardSlot.Count;
						if (count7 + list8.Count > this.m_lstNKCUIWRRewardSlot.Count)
						{
							int newCount2 = count7 + list8.Count - this.m_lstNKCUIWRRewardSlot.Count;
							this.AddExtraRewardSlot(newCount2);
						}
						for (int num12 = 0; num12 < list8.Count; num12++)
						{
							NKCUISlot.SlotData slotData5 = list8[num12];
							this.m_lstNKCUIWRRewardSlot[num10].SetUI(slotData5, num10);
							this.m_lstNKCUIWRRewardSlot[num10].SetContainerMark(true);
							this.m_lstNKCUIWRRewardSlot[num10].SetMultiplyMark(flag2);
							num10++;
						}
					}
					int count8 = this.m_lstNKCUIWRRewardSlot.Count;
					if (count8 + list6.Count > this.m_lstNKCUIWRRewardSlot.Count)
					{
						int newCount3 = count8 + list6.Count - this.m_lstNKCUIWRRewardSlot.Count;
						this.AddExtraRewardSlot(newCount3);
					}
					for (int num13 = 0; num13 < list6.Count; num13++)
					{
						NKCUISlot.SlotData slotData6 = list6[num13];
						this.m_lstNKCUIWRRewardSlot[num10].SetUI(slotData6, num10);
						this.m_lstNKCUIWRRewardSlot[num10].SetMultiplyMark(flag2);
						num10++;
					}
					this.SetUIRewardAlert(this.m_NKMWarfareClearData.m_enemiesKillCount == 0);
					if (this.m_NKMWarfareClearData.m_enemiesKillCount > 0)
					{
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_REWARD_BONUS_TYPE.gameObject, !nkmwarfareTemplet.StageTemplet.m_BuffType.Equals(RewardTuningType.None));
						NKCUtil.SetImageSprite(this.m_NKM_UI_WARFARE_RESULT_REWARD_BONUS_TYPE, NKCUtil.GetBounsTypeIcon(nkmwarfareTemplet.StageTemplet.m_BuffType, false), false);
						return;
					}
				}
			}
			else if (this.m_eContents == NKCUIWarfareResult.USE_CONTENTS.DUNGEON)
			{
				if (this.m_battleResultData == null)
				{
					return;
				}
				if (this.GetIsWinGame())
				{
					bool flag3 = this.m_battleResultData.m_multiply > 1;
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_MULTIPLY_REWARD_TAG, flag3);
					NKCUtil.SetLabelText(this.m_MultiplyReward_COUNT_TEXT, string.Format(NKCUtilString.GET_MULTIPLY_REWARD_COUNT_PARAM_02, this.m_battleResultData.m_multiply));
					int num14 = 0;
					this.ApplyRewardData(this.m_battleResultData.m_firstRewardData, ref num14, flag3, NKCUIWarfareResult.eRewardType.FirstClear);
					this.ApplyRewardData(this.m_battleResultData.m_firstAllClearData, ref num14, flag3, NKCUIWarfareResult.eRewardType.FirstAllClear);
					this.ApplyRewardData(this.m_battleResultData.m_OnetimeRewardData, ref num14, flag3, NKCUIWarfareResult.eRewardType.OneTime);
					this.ApplyRewardData(this.m_battleResultData.m_RewardData, ref num14, flag3, NKCUIWarfareResult.eRewardType.None);
					this.ApplyRewardData(this.m_battleResultData.m_additionalReward, ref num14, flag3, NKCUIWarfareResult.eRewardType.None);
					this.SetUIRewardAlert(false);
					NKMStageTempletV2 currentStageTemplet = this.GetCurrentStageTemplet();
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_REWARD_BONUS_TYPE.gameObject, !currentStageTemplet.m_BuffType.Equals(RewardTuningType.None));
					NKCUtil.SetImageSprite(this.m_NKM_UI_WARFARE_RESULT_REWARD_BONUS_TYPE, NKCUtil.GetBounsTypeIcon(currentStageTemplet.m_BuffType, false), false);
					return;
				}
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_MULTIPLY_REWARD_TAG, false);
				List<NKCUISlot.SlotData> allListRewardSlotData = this.m_battleResultData.GetAllListRewardSlotData();
				if (allListRewardSlotData.Count > 0)
				{
					if (this.m_slotEntryFeeReturn != null)
					{
						NKCUISlot.SlotData slotData7 = allListRewardSlotData[0];
						this.m_slotEntryFeeReturn.SetUI(slotData7, 0);
						this.m_slotEntryFeeReturn.SetMultiplyMark(false);
						this.m_slotEntryFeeReturn.SetFirstAllClearMark(false);
						this.m_slotEntryFeeReturn.SetFirstMark(false);
					}
					NKCUtil.SetGameobjectActive(this.m_objEntryFeeReturn, true);
					return;
				}
				NKCUtil.SetGameobjectActive(this.m_objEntryFeeReturn, false);
			}
		}

		// Token: 0x06008A39 RID: 35385 RVA: 0x002EF920 File Offset: 0x002EDB20
		private void AddExtraRewardSlot(int newCount)
		{
			for (int i = 0; i < newCount; i++)
			{
				NKCUIWRRewardSlot newInstance = NKCUIWRRewardSlot.GetNewInstance(this.m_NKM_UI_WARFARE_RESULT_REWARDS_Content);
				this.m_lstNKCUIWRRewardSlot.Add(newInstance);
			}
		}

		// Token: 0x06008A3A RID: 35386 RVA: 0x002EF954 File Offset: 0x002EDB54
		private void SetUIByMission()
		{
			if (this.m_eContents != NKCUIWarfareResult.USE_CONTENTS.WARFARE)
			{
				if (this.m_eContents == NKCUIWarfareResult.USE_CONTENTS.DUNGEON)
				{
					if (this.m_battleResultData == null)
					{
						return;
					}
					for (int i = 0; i < this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_ICON.Count; i++)
					{
						if (this.m_battleResultData.m_lstMissionData.Count > i)
						{
							NKCUIResultSubUIDungeon.MissionData missionData = this.m_battleResultData.m_lstMissionData[i];
							if (missionData.eMissionType == DUNGEON_GAME_MISSION_TYPE.DGMT_NONE)
							{
								NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_ICON[i], false);
								NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_ICON_SUCCESS[i], false);
								NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_ICON_FAIL[i], false);
								NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_TEXT[i], false);
							}
							else
							{
								bool flag = this.m_battleResultData.IsWin && missionData.bSuccess;
								NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_ICON[i], true);
								NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_TEXT[i], true);
								this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_TEXT[i].text = NKCUtilString.GetDGMissionText(missionData.eMissionType, missionData.iMissionValue);
								NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_ICON_SUCCESS[i], flag);
								NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_ICON_FAIL[i], !flag);
							}
						}
						else
						{
							NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_ICON[i], false);
							NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_ICON_SUCCESS[i], false);
							NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_ICON_FAIL[i], false);
							NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_TEXT[i], false);
						}
					}
				}
				return;
			}
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData == null)
			{
				return;
			}
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(warfareGameData.warfareTempletID);
			if (nkmwarfareTemplet == null)
			{
				return;
			}
			this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_TEXT[2].text = NKCUtilString.GetWFMissionText(nkmwarfareTemplet.m_WFMissionType_2, nkmwarfareTemplet.m_WFMissionValue_2);
			this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_TEXT[1].text = NKCUtilString.GetWFMissionText(nkmwarfareTemplet.m_WFMissionType_1, nkmwarfareTemplet.m_WFMissionValue_1);
			this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_TEXT[0].text = NKCUtilString.GetWFMissionText(WARFARE_GAME_MISSION_TYPE.WFMT_CLEAR, 0);
			foreach (GameObject targetObj in this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_ICON)
			{
				NKCUtil.SetGameobjectActive(targetObj, true);
			}
			if (this.GetIsWinGame())
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_ICON_SUCCESS[2], this.m_NKMWarfareClearData.m_mission_result_2);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_ICON_FAIL[2], !this.m_NKMWarfareClearData.m_mission_result_2);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_ICON_SUCCESS[1], this.m_NKMWarfareClearData.m_mission_result_1);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_ICON_FAIL[1], !this.m_NKMWarfareClearData.m_mission_result_1);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_ICON_SUCCESS[0], true);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_ICON_FAIL[0], false);
				return;
			}
			for (int j = 0; j < this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_ICON_SUCCESS.Count; j++)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_ICON_SUCCESS[j], false);
			}
			for (int k = 0; k < this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_ICON_FAIL.Count; k++)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_INFO_MISSION_ICON_FAIL[k], true);
			}
		}

		// Token: 0x06008A3B RID: 35387 RVA: 0x002EFCC0 File Offset: 0x002EDEC0
		private void SetUIByGrade(NKCUIWarfareResult.NKC_WARFARE_RESULT_GRADE grade)
		{
			for (int i = 0; i < 5; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_lst_NKM_UI_WARFARE_RESULT_BACKFX[i], grade == (NKCUIWarfareResult.NKC_WARFARE_RESULT_GRADE)i);
				NKCUtil.SetGameobjectActive(this.m_lst_NKM_UI_WARFARE_RESULT_INFO_GRADE[i], grade == (NKCUIWarfareResult.NKC_WARFARE_RESULT_GRADE)i);
			}
		}

		// Token: 0x06008A3C RID: 35388 RVA: 0x002EFD04 File Offset: 0x002EDF04
		private bool GetIsWinGame()
		{
			switch (this.m_eContents)
			{
			case NKCUIWarfareResult.USE_CONTENTS.WARFARE:
				return this.m_NKMWarfareClearData != null;
			case NKCUIWarfareResult.USE_CONTENTS.DIVE:
				return this.m_bClearDive;
			case NKCUIWarfareResult.USE_CONTENTS.SHADOW:
				return this.m_ShadowResult.life > 0;
			case NKCUIWarfareResult.USE_CONTENTS.TRIM:
				return this.m_trimClearData.isWin;
			case NKCUIWarfareResult.USE_CONTENTS.DUNGEON:
				return this.m_battleResultData != null && this.m_battleResultData.IsWin;
			default:
				return false;
			}
		}

		// Token: 0x06008A3D RID: 35389 RVA: 0x002EFD78 File Offset: 0x002EDF78
		private void SetUIByLeader(NKMDeckIndex deckIndex, int dummyUnitID = 0, int dummyShipID = 0)
		{
			if (this.m_NKCASUISpineIllust != null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_NKCASUISpineIllust);
			}
			this.m_NKCASUISpineIllust = null;
			if (this.m_NKCASUISpineIllustShip != null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_NKCASUISpineIllustShip);
			}
			this.m_NKCASUISpineIllustShip = null;
			this.m_AB_UI_TALK_BOX.SetText("", 0f, 0f);
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			NKMUnitData nkmunitData = null;
			int unitID;
			int skinID;
			if (dummyUnitID == 0)
			{
				if (this.m_leaderUnitUID != 0L)
				{
					nkmunitData = armyData.GetUnitFromUID(this.m_leaderUnitUID);
					if (nkmunitData == null)
					{
						return;
					}
				}
				else if (deckIndex.m_eDeckType != NKM_DECK_TYPE.NDT_NONE)
				{
					nkmunitData = armyData.GetDeckLeaderUnitData(deckIndex);
					if (nkmunitData == null)
					{
						return;
					}
				}
				else if (this.m_NKMWarfareClearData != null && this.m_NKMWarfareClearData.m_RewardDataList != null && this.m_NKMWarfareClearData.m_RewardDataList.UnitExpDataList.Count > 0)
				{
					nkmunitData = armyData.GetUnitFromUID(this.m_NKMWarfareClearData.m_RewardDataList.UnitExpDataList[0].m_UnitUid);
				}
				else
				{
					if (this.m_battleResultData == null || this.m_battleResultData.m_RewardData == null || this.m_battleResultData.m_RewardData.UnitExpDataList.Count <= 0)
					{
						return;
					}
					nkmunitData = armyData.GetUnitFromUID(this.m_battleResultData.m_RewardData.UnitExpDataList[0].m_UnitUid);
				}
				this.m_NKCASUISpineIllust = NKCResourceUtility.OpenSpineIllust(nkmunitData, false);
				unitID = nkmunitData.m_UnitID;
				skinID = nkmunitData.m_SkinID;
			}
			else
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(this.m_dummyUnitSkinID);
				if (skinTemplet != null && NKMSkinManager.IsSkinForCharacter(dummyUnitID, skinTemplet))
				{
					this.m_NKCASUISpineIllust = NKCResourceUtility.OpenSpineIllust(skinTemplet, false);
					unitID = dummyUnitID;
					skinID = this.m_dummyUnitSkinID;
				}
				else
				{
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(dummyUnitID);
					this.m_NKCASUISpineIllust = NKCResourceUtility.OpenSpineIllust(unitTempletBase, false);
					unitID = dummyUnitID;
					skinID = 0;
				}
			}
			if (this.m_NKCASUISpineIllust != null)
			{
				this.m_NKCASUISpineIllust.SetParent(this.m_UNITSPINEOBJECT, false);
				this.m_NKCASUISpineIllust.SetAnchoredPosition(Vector2.zero);
				this.m_NKCASUISpineIllust.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.UNIT_IDLE, true, false, 0f);
			}
			if (this.m_leaderShipUID != 0L)
			{
				NKMUnitData shipFromUID = armyData.GetShipFromUID(this.m_leaderShipUID);
				if (shipFromUID != null)
				{
					this.m_NKCASUISpineIllustShip = NKCResourceUtility.OpenSpineIllust(shipFromUID, false);
					if (this.m_NKCASUISpineIllustShip != null)
					{
						this.m_NKCASUISpineIllustShip.SetParent(this.m_SHIPSPINEOBJECT, false);
						this.m_NKCASUISpineIllustShip.SetAnchoredPosition(Vector2.zero);
						this.m_NKCASUISpineIllustShip.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SHIP_IDLE, true, false, 0f);
					}
				}
			}
			else if (deckIndex.m_eDeckType != NKM_DECK_TYPE.NDT_NONE)
			{
				NKMDeckData deckData = armyData.GetDeckData(deckIndex);
				if (deckData != null)
				{
					NKMUnitData shipFromUID2 = armyData.GetShipFromUID(deckData.m_ShipUID);
					if (shipFromUID2 != null)
					{
						this.m_NKCASUISpineIllustShip = NKCResourceUtility.OpenSpineIllust(shipFromUID2, false);
						if (this.m_NKCASUISpineIllustShip != null)
						{
							this.m_NKCASUISpineIllustShip.SetParent(this.m_SHIPSPINEOBJECT, false);
							this.m_NKCASUISpineIllustShip.SetAnchoredPosition(Vector2.zero);
							this.m_NKCASUISpineIllustShip.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SHIP_IDLE, true, false, 0f);
						}
					}
				}
			}
			else if (dummyShipID != 0)
			{
				NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(dummyShipID);
				this.m_NKCASUISpineIllustShip = NKCResourceUtility.OpenSpineIllust(unitTempletBase2, false);
				if (this.m_NKCASUISpineIllustShip != null)
				{
					this.m_NKCASUISpineIllustShip.SetParent(this.m_SHIPSPINEOBJECT, false);
					this.m_NKCASUISpineIllustShip.SetAnchoredPosition(Vector2.zero);
					this.m_NKCASUISpineIllustShip.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SHIP_IDLE, true, false, 0f);
				}
			}
			NKCDescTemplet descTemplet = NKCDescMgr.GetDescTemplet(unitID, skinID);
			if (descTemplet != null)
			{
				NKCDescTemplet.NKCDescData nkcdescData;
				if (this.GetIsWinGame())
				{
					if (nkmunitData != null && nkmunitData.IsPermanentContract)
					{
						nkcdescData = descTemplet.m_arrDescData[4];
					}
					else
					{
						nkcdescData = descTemplet.m_arrDescData[0];
					}
				}
				else if (nkmunitData != null && nkmunitData.IsPermanentContract)
				{
					nkcdescData = descTemplet.m_arrDescData[5];
				}
				else
				{
					nkcdescData = descTemplet.m_arrDescData[1];
				}
				this.m_AB_UI_TALK_BOX.SetText(nkcdescData.GetDesc(), 0f, 0f);
				if (this.m_NKCASUISpineIllust != null)
				{
					this.m_NKCASUISpineIllust.SetAnimation(nkcdescData.m_Ani, false, 0, true, 0f, true);
				}
			}
			else
			{
				this.m_AB_UI_TALK_BOX.SetText("", 0f, 0f);
				if (this.m_NKCASUISpineIllust != null)
				{
					this.m_NKCASUISpineIllust.SetAnimation(NKCASUIUnitIllust.eAnimation.UNIT_TOUCH, false, 0, true, 0f, true);
				}
			}
			if (nkmunitData == null)
			{
				if (this.GetIsWinGame())
				{
					NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_BATTLE_VICTORY, unitID, skinID, true, false);
					return;
				}
				NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_BATTLE_FAIL, unitID, skinID, true, false);
				return;
			}
			else
			{
				if (this.GetIsWinGame())
				{
					NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_BATTLE_VICTORY, nkmunitData, true, false);
					return;
				}
				NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_BATTLE_FAIL, nkmunitData, true, false);
				return;
			}
		}

		// Token: 0x06008A3E RID: 35390 RVA: 0x002F01EC File Offset: 0x002EE3EC
		private void SetUIGameTip()
		{
			bool isWinGame = this.GetIsWinGame();
			NKCUtil.SetGameobjectActive(this.m_TITLE_REWARD, isWinGame);
			NKCUtil.SetGameobjectActive(this.m_TITLE_GAMETIP, !isWinGame && this.m_eContents != NKCUIWarfareResult.USE_CONTENTS.TRIM);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_WARFARE_RESULT_GAMETIP, !isWinGame);
			switch (this.m_eContents)
			{
			case NKCUIWarfareResult.USE_CONTENTS.WARFARE:
				NKCUtil.SetLabelText(this.m_NKM_UI_WARFARE_RESULT_GAMETIP_text, NKCUtilString.GET_STRING_WARFARE_RESULT_GAME_TIP);
				return;
			case NKCUIWarfareResult.USE_CONTENTS.DIVE:
				NKCUtil.SetLabelText(this.m_NKM_UI_WARFARE_RESULT_GAMETIP_text, NKCUtilString.GET_STRING_DIVE_RESULT_GAME_TIP);
				return;
			case NKCUIWarfareResult.USE_CONTENTS.SHADOW:
				NKCUtil.SetLabelText(this.m_NKM_UI_WARFARE_RESULT_GAMETIP_text, NKCUtilString.GET_SHADOW_PALACE_RESULT_GAME_TIP);
				return;
			case NKCUIWarfareResult.USE_CONTENTS.DUNGEON:
				NKCUtil.SetLabelText(this.m_NKM_UI_WARFARE_RESULT_GAMETIP_text, NKCUtilString.GET_STRING_DUNGEON_RESULT_GAME_TIP);
				return;
			}
			NKCUtil.SetLabelText(this.m_NKM_UI_WARFARE_RESULT_GAMETIP_text, string.Empty);
		}

		// Token: 0x06008A3F RID: 35391 RVA: 0x002F02AF File Offset: 0x002EE4AF
		private void SetUIRewardAlert(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_Rewards_alert, bSet);
			if (bSet)
			{
				this.m_Rewards_alert.transform.SetAsLastSibling();
			}
		}

		// Token: 0x06008A40 RID: 35392 RVA: 0x002F02D0 File Offset: 0x002EE4D0
		private NKCUIWarfareResult.NKC_WARFARE_RESULT_GRADE GetGrade()
		{
			if (this.m_battleResultData != null)
			{
				if (!this.m_battleResultData.IsWin)
				{
					return NKCUIWarfareResult.NKC_WARFARE_RESULT_GRADE.NWRG_C;
				}
				if (NKMStageTempletV2.Find(this.m_battleResultData.m_stageID) == null)
				{
					return NKCUIWarfareResult.NKC_WARFARE_RESULT_GRADE.NWRG_S;
				}
				int num = 0;
				int num2 = 0;
				foreach (NKCUIResultSubUIDungeon.MissionData missionData in this.m_battleResultData.m_lstMissionData)
				{
					if (missionData.eMissionType != DUNGEON_GAME_MISSION_TYPE.DGMT_NONE)
					{
						num2++;
						if (missionData.bSuccess)
						{
							num++;
						}
					}
				}
				switch (num2 - num)
				{
				case 0:
					return NKCUIWarfareResult.NKC_WARFARE_RESULT_GRADE.NWRG_S;
				case 1:
					return NKCUIWarfareResult.NKC_WARFARE_RESULT_GRADE.NWRG_A;
				case 2:
					return NKCUIWarfareResult.NKC_WARFARE_RESULT_GRADE.NWRG_B;
				}
				return NKCUIWarfareResult.NKC_WARFARE_RESULT_GRADE.NWRG_C;
			}
			else if (this.m_NKMWarfareClearData != null)
			{
				int num3 = 1;
				if (this.m_NKMWarfareClearData.m_mission_result_1)
				{
					num3++;
				}
				if (this.m_NKMWarfareClearData.m_mission_result_2)
				{
					num3++;
				}
				if (num3 >= 3)
				{
					return NKCUIWarfareResult.NKC_WARFARE_RESULT_GRADE.NWRG_S;
				}
				if (num3 == 2)
				{
					return NKCUIWarfareResult.NKC_WARFARE_RESULT_GRADE.NWRG_A;
				}
				if (num3 == 1)
				{
					return NKCUIWarfareResult.NKC_WARFARE_RESULT_GRADE.NWRG_B;
				}
				return NKCUIWarfareResult.NKC_WARFARE_RESULT_GRADE.NWRG_C;
			}
			else if (this.m_eContents == NKCUIWarfareResult.USE_CONTENTS.DIVE)
			{
				if (this.m_bClearDive)
				{
					return NKCUIWarfareResult.NKC_WARFARE_RESULT_GRADE.NWRG_S;
				}
				return NKCUIWarfareResult.NKC_WARFARE_RESULT_GRADE.NWRG_C;
			}
			else if (this.m_eContents == NKCUIWarfareResult.USE_CONTENTS.SHADOW)
			{
				if (this.m_ShadowResult.life == 0)
				{
					return NKCUIWarfareResult.NKC_WARFARE_RESULT_GRADE.NWRG_C;
				}
				return NKCUIWarfareResult.NKC_WARFARE_RESULT_GRADE.NWRG_S;
			}
			else
			{
				if (this.m_eContents != NKCUIWarfareResult.USE_CONTENTS.TRIM)
				{
					return NKCUIWarfareResult.NKC_WARFARE_RESULT_GRADE.NWRG_C;
				}
				if (this.m_trimClearData.isWin)
				{
					return NKCUIWarfareResult.NKC_WARFARE_RESULT_GRADE.NWRG_S;
				}
				return NKCUIWarfareResult.NKC_WARFARE_RESULT_GRADE.NWRG_C;
			}
		}

		// Token: 0x06008A41 RID: 35393 RVA: 0x002F0424 File Offset: 0x002EE624
		public override void CloseInternal()
		{
			this.m_bFinishedIntroMovie = false;
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			if (this.m_CallBackWhenClosed != null && this.m_NextScenID != NKM_SCEN_ID.NSI_INVALID)
			{
				this.m_CallBackWhenClosed(this.m_NextScenID);
				this.m_NextScenID = NKM_SCEN_ID.NSI_INVALID;
			}
			if (this.m_NKCASUISpineIllust != null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_NKCASUISpineIllust);
			}
			this.m_NKCASUISpineIllust = null;
			if (this.m_NKCASUISpineIllustShip != null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().CloseObj(this.m_NKCASUISpineIllustShip);
			}
			this.m_NKCASUISpineIllustShip = null;
			if (this.m_coIntro != null)
			{
				base.StopCoroutine(this.m_coIntro);
				this.m_coIntro = null;
			}
			NKCUIComVideoCamera subUICameraVideoPlayer = NKCCamera.GetSubUICameraVideoPlayer();
			if (subUICameraVideoPlayer != null)
			{
				subUICameraVideoPlayer.CleanUp();
			}
			for (int i = 0; i < this.m_lstNKCUIWRRewardSlot.Count; i++)
			{
				this.m_lstNKCUIWRRewardSlot[i].Close();
			}
			this.m_lstNKCUIWRRewardSlot.Clear();
			NKCUIOperationIntro.CheckInstanceAndClose();
			this.m_trimClearData = null;
			this.m_trimModeState = null;
			this.m_leaderUnitUID = 0L;
			this.m_leaderShipUID = 0L;
			this.m_dummyShipID = 0;
			this.m_dummyUnitID = 0;
			this.m_dummyUnitSkinID = 0;
			this.m_bTrimEvent = false;
			this.m_battleResultData = null;
			this.CheckNKCPopupArtifactExchangeAndClose();
		}

		// Token: 0x06008A42 RID: 35394 RVA: 0x002F0562 File Offset: 0x002EE762
		public override void OnBackButton()
		{
			this.CloseToOK();
		}

		// Token: 0x06008A43 RID: 35395 RVA: 0x002F056A File Offset: 0x002EE76A
		public void CloseToHome()
		{
			this.m_NextScenID = NKM_SCEN_ID.NSI_HOME;
			this.m_bPause = true;
			NKCScenManager.GetScenManager().GetNKCRepeatOperaion().Init();
			base.Close();
		}

		// Token: 0x06008A44 RID: 35396 RVA: 0x002F058F File Offset: 0x002EE78F
		private void OnClickRepeatOperation()
		{
			this.m_bPause = true;
			NKCPopupRepeatOperation.Instance.Open(delegate
			{
				this.m_bPause = false;
				if (!NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing())
				{
					this.SetBtnUI();
				}
			});
		}

		// Token: 0x06008A45 RID: 35397 RVA: 0x002F05AE File Offset: 0x002EE7AE
		private void OnClickRetry()
		{
			this.m_bPause = true;
			NKCScenManager.GetScenManager().GetNKCRepeatOperaion().Init();
			this.Retry();
		}

		// Token: 0x06008A46 RID: 35398 RVA: 0x002F05CC File Offset: 0x002EE7CC
		public void Retry()
		{
			NKCUIWarfareResult.USE_CONTENTS eContents = this.m_eContents;
			if (eContents != NKCUIWarfareResult.USE_CONTENTS.WARFARE)
			{
				if (eContents != NKCUIWarfareResult.USE_CONTENTS.DUNGEON)
				{
					return;
				}
				NKMStageTempletV2 currentStageTemplet = this.GetCurrentStageTemplet();
				if (currentStageTemplet != null)
				{
					NKC_SCEN_OPERATION_V2 scen_OPERATION = NKCScenManager.GetScenManager().Get_SCEN_OPERATION();
					if (scen_OPERATION != null)
					{
						NKCScenManager.GetScenManager().GetNKCRepeatOperaion().Init();
						scen_OPERATION.SetReservedStage(currentStageTemplet);
						NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OPERATION, true);
					}
				}
			}
			else
			{
				NKC_SCEN_WARFARE_GAME nkc_SCEN_WARFARE_GAME = NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME();
				if (nkc_SCEN_WARFARE_GAME != null && NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WARFARE_GAME)
				{
					nkc_SCEN_WARFARE_GAME.SetRetry(true);
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_WARFARE_GAME, true);
					return;
				}
			}
		}

		// Token: 0x06008A47 RID: 35399 RVA: 0x002F0654 File Offset: 0x002EE854
		public void CloseToOK()
		{
			this.m_bPause = true;
			switch (this.m_eContents)
			{
			case NKCUIWarfareResult.USE_CONTENTS.WARFARE:
			case NKCUIWarfareResult.USE_CONTENTS.DUNGEON:
			{
				NKCScenManager.GetScenManager().GetNKCRepeatOperaion().Init();
				NKC_SCEN_OPERATION_V2 scen_OPERATION = NKCScenManager.GetScenManager().Get_SCEN_OPERATION();
				NKMStageTempletV2 possibleNextOperation = this.GetPossibleNextOperation();
				if (possibleNextOperation != null)
				{
					scen_OPERATION.SetReservedStage(possibleNextOperation);
					this.m_NextScenID = NKM_SCEN_ID.NSI_OPERATION;
				}
				else
				{
					NKMStageTempletV2 currentStageTemplet = this.GetCurrentStageTemplet();
					if (currentStageTemplet != null)
					{
						scen_OPERATION.SetReservedStage(currentStageTemplet);
						this.m_NextScenID = NKM_SCEN_ID.NSI_OPERATION;
					}
					else
					{
						this.m_NextScenID = NKM_SCEN_ID.NSI_HOME;
					}
				}
				break;
			}
			case NKCUIWarfareResult.USE_CONTENTS.DIVE:
				this.m_NextScenID = NKM_SCEN_ID.NSI_DIVE_READY;
				break;
			case NKCUIWarfareResult.USE_CONTENTS.SHADOW:
				this.m_NextScenID = NKM_SCEN_ID.NSI_SHADOW_PALACE;
				break;
			case NKCUIWarfareResult.USE_CONTENTS.TRIM:
				this.m_NextScenID = NKM_SCEN_ID.NSI_TRIM;
				break;
			}
			base.Close();
		}

		// Token: 0x06008A48 RID: 35400 RVA: 0x002F0706 File Offset: 0x002EE906
		private void OnClickBattleStat()
		{
			if (this.m_battleResultData != null)
			{
				NKCUIBattleStatistics.Instance.Open(this.m_battleResultData.m_battleData, null);
			}
		}

		// Token: 0x06008A49 RID: 35401 RVA: 0x002F0726 File Offset: 0x002EE926
		private bool IsPopupTiming()
		{
			return this.m_Animtor.speed == 1f && this.m_fElapsedTime > 2.466667f;
		}

		// Token: 0x06008A4A RID: 35402 RVA: 0x002F0749 File Offset: 0x002EE949
		private void CheckTutorialRequired()
		{
			this.m_bRequiredTutorial = (NKCTutorialManager.TutorialRequired(TutorialPoint.WarfareResult, false) > TutorialStep.None);
		}

		// Token: 0x040076A5 RID: 30373
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_result";

		// Token: 0x040076A6 RID: 30374
		private const string UI_ASSET_NAME = "NKM_UI_WARFARE_RESULT";

		// Token: 0x040076A7 RID: 30375
		private static NKCUIWarfareResult m_Instance;

		// Token: 0x040076A8 RID: 30376
		public GameObject m_NKM_UI_WARFARE_RESULT_ROOT;

		// Token: 0x040076A9 RID: 30377
		public Animator m_Animtor;

		// Token: 0x040076AA RID: 30378
		public List<GameObject> m_lst_NKM_UI_WARFARE_RESULT_BACKFX;

		// Token: 0x040076AB RID: 30379
		public List<GameObject> m_lst_NKM_UI_WARFARE_RESULT_INFO_GRADE;

		// Token: 0x040076AC RID: 30380
		public NKCComUITalkBox m_AB_UI_TALK_BOX;

		// Token: 0x040076AD RID: 30381
		public List<GameObject> m_NKM_UI_WARFARE_RESULT_INFO_MISSION_ICON;

		// Token: 0x040076AE RID: 30382
		public List<GameObject> m_NKM_UI_WARFARE_RESULT_INFO_MISSION_ICON_SUCCESS;

		// Token: 0x040076AF RID: 30383
		public List<GameObject> m_NKM_UI_WARFARE_RESULT_INFO_MISSION_ICON_FAIL;

		// Token: 0x040076B0 RID: 30384
		public List<Text> m_NKM_UI_WARFARE_RESULT_INFO_MISSION_TEXT;

		// Token: 0x040076B1 RID: 30385
		public Transform m_NKM_UI_WARFARE_RESULT_REWARDS_Content;

		// Token: 0x040076B2 RID: 30386
		public RectTransform m_UNITSPINEOBJECT;

		// Token: 0x040076B3 RID: 30387
		public RectTransform m_SHIPSPINEOBJECT;

		// Token: 0x040076B4 RID: 30388
		public GameObject m_PARTICLEOBJECT;

		// Token: 0x040076B5 RID: 30389
		[Header("우측")]
		public GameObject m_TITLE_REWARD;

		// Token: 0x040076B6 RID: 30390
		public GameObject m_Rewards_alert;

		// Token: 0x040076B7 RID: 30391
		public Image m_NKM_UI_WARFARE_RESULT_REWARD_BONUS_TYPE;

		// Token: 0x040076B8 RID: 30392
		public GameObject m_NKM_UI_WARFARE_RESULT_MULTIPLY_REWARD_TAG;

		// Token: 0x040076B9 RID: 30393
		public NKCComText m_MultiplyReward_COUNT_TEXT;

		// Token: 0x040076BA RID: 30394
		[Header("패배시 게임 팁 관련")]
		public GameObject m_TITLE_GAMETIP;

		// Token: 0x040076BB RID: 30395
		public GameObject m_NKM_UI_WARFARE_RESULT_GAMETIP;

		// Token: 0x040076BC RID: 30396
		public Text m_NKM_UI_WARFARE_RESULT_GAMETIP_text;

		// Token: 0x040076BD RID: 30397
		public GameObject m_objEntryFeeReturn;

		// Token: 0x040076BE RID: 30398
		public NKCUIWRRewardSlot m_slotEntryFeeReturn;

		// Token: 0x040076BF RID: 30399
		[Header("Button")]
		public NKCUIComStateButton m_csbtnCloseToHome;

		// Token: 0x040076C0 RID: 30400
		public NKCUIComStateButton m_csbtnCloseToList;

		// Token: 0x040076C1 RID: 30401
		public NKCUIComStateButton m_csbtnRestart;

		// Token: 0x040076C2 RID: 30402
		public NKCUIComStateButton m_csbtnConfirm;

		// Token: 0x040076C3 RID: 30403
		public NKCUIComStateButton m_csbtnNextMission;

		// Token: 0x040076C4 RID: 30404
		public NKCUIComStateButton m_csbtnBattleStat;

		// Token: 0x040076C5 RID: 30405
		[Header("반복작전")]
		public GameObject m_NKM_UI_WARFARE_RESULT_REPEAT;

		// Token: 0x040076C6 RID: 30406
		public NKCUIComStateButton m_NKM_UI_WARFARE_RESULT_BTN_REPEAT;

		// Token: 0x040076C7 RID: 30407
		public Text m_NKM_UI_WARFARE_RESULT_REPEAT_COUNT;

		// Token: 0x040076C8 RID: 30408
		public Image m_NKM_UI_WARFARE_RESULT_BTN_REPEAT_COUNT_DOWN_Gauge;

		// Token: 0x040076C9 RID: 30409
		public Text m_NKM_UI_WARFARE_RESULT_BTN_REPEAT_COUNT_DOWN;

		// Token: 0x040076CA RID: 30410
		[Header("배경 Fallback")]
		public GameObject m_objBGFallBack;

		// Token: 0x040076CB RID: 30411
		private NKCUIWarfareResult.CallBackWhenClosed m_CallBackWhenClosed;

		// Token: 0x040076CC RID: 30412
		private NKM_SCEN_ID m_NextScenID;

		// Token: 0x040076CD RID: 30413
		private NKMWarfareClearData m_NKMWarfareClearData;

		// Token: 0x040076CE RID: 30414
		private NKMDeckIndex m_CurrentDeck;

		// Token: 0x040076CF RID: 30415
		private List<NKCUIWRRewardSlot> m_lstNKCUIWRRewardSlot = new List<NKCUIWRRewardSlot>();

		// Token: 0x040076D0 RID: 30416
		private float m_fElapsedTime;

		// Token: 0x040076D1 RID: 30417
		private const int REWARD_SLOT_MAKE_START_FRAME = 131;

		// Token: 0x040076D2 RID: 30418
		private const float REWARD_SLOT_MAKE_START_TIME = 2.1833334f;

		// Token: 0x040076D3 RID: 30419
		private bool m_bDoneMakeRewardSlot;

		// Token: 0x040076D4 RID: 30420
		private bool m_bReservedShowGetUnit;

		// Token: 0x040076D5 RID: 30421
		private float m_fTimeToShowGetUnit;

		// Token: 0x040076D6 RID: 30422
		public const float REWARD_SLOT_ANI_INTERVAL_TIME = 0.13333334f;

		// Token: 0x040076D7 RID: 30423
		private const int ANI_FRAME = 208;

		// Token: 0x040076D8 RID: 30424
		private const int TUTORIAL_CHECK_ANIM_FRAME = 181;

		// Token: 0x040076D9 RID: 30425
		private const float TUTORIAL_CHECK_NORMALIZED_TIME = 0.8701923f;

		// Token: 0x040076DA RID: 30426
		private const float SECOND_PER_ANI_FRAME = 0.016666668f;

		// Token: 0x040076DB RID: 30427
		private bool m_bTriggeredAutoOK;

		// Token: 0x040076DC RID: 30428
		private NKCASUIUnitIllust m_NKCASUISpineIllust;

		// Token: 0x040076DD RID: 30429
		private NKCASUIUnitIllust m_NKCASUISpineIllustShip;

		// Token: 0x040076DE RID: 30430
		private Coroutine m_coIntro;

		// Token: 0x040076DF RID: 30431
		private bool m_bFinishedIntroMovie;

		// Token: 0x040076E0 RID: 30432
		private bool m_bfirstClear;

		// Token: 0x040076E1 RID: 30433
		private bool m_bFirstAllClear;

		// Token: 0x040076E2 RID: 30434
		private WarfareSupporterListData m_guestSptData;

		// Token: 0x040076E3 RID: 30435
		private NKCUIWarfareResult.USE_CONTENTS m_eContents;

		// Token: 0x040076E4 RID: 30436
		private bool m_bClearDive;

		// Token: 0x040076E5 RID: 30437
		private NKMRewardData m_DiveRewardData;

		// Token: 0x040076E6 RID: 30438
		private NKMRewardData m_DiveRewardDataArtifact;

		// Token: 0x040076E7 RID: 30439
		private NKMRewardData m_DiveRewardDataStorm;

		// Token: 0x040076E8 RID: 30440
		private List<int> m_lstArtifact = new List<int>();

		// Token: 0x040076E9 RID: 30441
		private bool m_bArtifactReturnEvent;

		// Token: 0x040076EA RID: 30442
		private NKMShadowGameResult m_ShadowResult;

		// Token: 0x040076EB RID: 30443
		private List<int> m_lstShadowBestTime;

		// Token: 0x040076EC RID: 30444
		private bool m_bShadowRecordEvent;

		// Token: 0x040076ED RID: 30445
		private NKMTrimClearData m_trimClearData;

		// Token: 0x040076EE RID: 30446
		private TrimModeState m_trimModeState;

		// Token: 0x040076EF RID: 30447
		private int m_trimBestScore;

		// Token: 0x040076F0 RID: 30448
		private bool m_bTrimFirstClear;

		// Token: 0x040076F1 RID: 30449
		private bool m_bTrimEvent;

		// Token: 0x040076F2 RID: 30450
		private bool m_bUserLevelUpPopupOpened;

		// Token: 0x040076F3 RID: 30451
		private bool m_bWaitContentUnlockPopup;

		// Token: 0x040076F4 RID: 30452
		private bool m_bWaitFriendRequestPopup;

		// Token: 0x040076F5 RID: 30453
		private int m_dummyUnitID;

		// Token: 0x040076F6 RID: 30454
		private int m_dummyUnitSkinID;

		// Token: 0x040076F7 RID: 30455
		private int m_dummyShipID;

		// Token: 0x040076F8 RID: 30456
		private long m_leaderUnitUID;

		// Token: 0x040076F9 RID: 30457
		private long m_leaderShipUID;

		// Token: 0x040076FA RID: 30458
		private bool m_bRequiredTutorial;

		// Token: 0x040076FB RID: 30459
		private float m_fFinalAutoProcessWaitTime = 5f;

		// Token: 0x040076FC RID: 30460
		private const float AUTO_PROCESS_WAIT_TIME = 5f;

		// Token: 0x040076FD RID: 30461
		private const float AUTO_PROCESS_WAIT_TIME_WITH_SPECIAL_EVENT = 10f;

		// Token: 0x040076FE RID: 30462
		private bool m_bPause;

		// Token: 0x040076FF RID: 30463
		private NKCUIResult.BattleResultData m_battleResultData;

		// Token: 0x04007700 RID: 30464
		private NKCPopupArtifactExchange m_NKCPopupArtifactExchange;

		// Token: 0x04007701 RID: 30465
		public float MoviePlaySpeed = 1f;

		// Token: 0x04007702 RID: 30466
		private bool m_bWaitingMovie;

		// Token: 0x02001978 RID: 6520
		// (Invoke) Token: 0x0600B905 RID: 47365
		public delegate void CallBackWhenClosed(NKM_SCEN_ID scenID);

		// Token: 0x02001979 RID: 6521
		public enum NKC_WARFARE_RESULT_GRADE
		{
			// Token: 0x0400AC18 RID: 44056
			NWRG_F,
			// Token: 0x0400AC19 RID: 44057
			NWRG_C,
			// Token: 0x0400AC1A RID: 44058
			NWRG_B,
			// Token: 0x0400AC1B RID: 44059
			NWRG_A,
			// Token: 0x0400AC1C RID: 44060
			NWRG_S,
			// Token: 0x0400AC1D RID: 44061
			NWRG_COUNT
		}

		// Token: 0x0200197A RID: 6522
		public enum USE_CONTENTS
		{
			// Token: 0x0400AC1F RID: 44063
			WARFARE,
			// Token: 0x0400AC20 RID: 44064
			DIVE,
			// Token: 0x0400AC21 RID: 44065
			SHADOW,
			// Token: 0x0400AC22 RID: 44066
			TRIM,
			// Token: 0x0400AC23 RID: 44067
			DUNGEON
		}

		// Token: 0x0200197B RID: 6523
		private enum eRewardType
		{
			// Token: 0x0400AC25 RID: 44069
			None,
			// Token: 0x0400AC26 RID: 44070
			FirstClear,
			// Token: 0x0400AC27 RID: 44071
			FirstAllClear,
			// Token: 0x0400AC28 RID: 44072
			OneTime
		}
	}
}
