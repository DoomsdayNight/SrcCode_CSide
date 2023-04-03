using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClientPacket.Common;
using ClientPacket.Game;
using ClientPacket.Mode;
using ClientPacket.Office;
using ClientPacket.WorldMap;
using Cs.Logging;
using Cs.Protocol;
using NKC.Publisher;
using NKC.Util;
using NKM;
using NKM.Guild;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Result
{
	// Token: 0x02000B9B RID: 2971
	public class NKCUIResult : NKCUIBase
	{
		// Token: 0x17001604 RID: 5636
		// (get) Token: 0x0600891C RID: 35100 RVA: 0x002E5CB8 File Offset: 0x002E3EB8
		public static NKCUIResult Instance
		{
			get
			{
				if (NKCUIResult.m_Instance == null)
				{
					NKCUIResult.m_Instance = NKCUIManager.OpenNewInstance<NKCUIResult>("ab_ui_nkm_ui_result", "NKM_UI_RESULT_COMMON", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIResult.CleanupInstance)).GetInstance<NKCUIResult>();
					NKCUIResult.m_Instance.Init();
				}
				return NKCUIResult.m_Instance;
			}
		}

		// Token: 0x17001605 RID: 5637
		// (get) Token: 0x0600891D RID: 35101 RVA: 0x002E5D07 File Offset: 0x002E3F07
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIResult.m_Instance != null && NKCUIResult.m_Instance.IsOpen;
			}
		}

		// Token: 0x0600891E RID: 35102 RVA: 0x002E5D22 File Offset: 0x002E3F22
		public static void CheckInstanceAndClose()
		{
			if (NKCUIResult.m_Instance != null && NKCUIResult.m_Instance.IsOpen)
			{
				NKCUIResult.m_Instance.Close();
			}
		}

		// Token: 0x0600891F RID: 35103 RVA: 0x002E5D47 File Offset: 0x002E3F47
		private static void CleanupInstance()
		{
			NKCUIResult.m_Instance = null;
		}

		// Token: 0x17001606 RID: 5638
		// (get) Token: 0x06008920 RID: 35104 RVA: 0x002E5D4F File Offset: 0x002E3F4F
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.ON_PLAY_GAME;
			}
		}

		// Token: 0x17001607 RID: 5639
		// (get) Token: 0x06008921 RID: 35105 RVA: 0x002E5D52 File Offset: 0x002E3F52
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_RESULT;
			}
		}

		// Token: 0x17001608 RID: 5640
		// (get) Token: 0x06008922 RID: 35106 RVA: 0x002E5D59 File Offset: 0x002E3F59
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001609 RID: 5641
		// (get) Token: 0x06008923 RID: 35107 RVA: 0x002E5D5C File Offset: 0x002E3F5C
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x1700160A RID: 5642
		// (get) Token: 0x06008924 RID: 35108 RVA: 0x002E5D5F File Offset: 0x002E3F5F
		public override bool WillCloseUnderPopupOnOpen
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06008925 RID: 35109 RVA: 0x002E5D62 File Offset: 0x002E3F62
		private IEnumerable<NKCUIResultSubUIBase> GetSubUIEnumerator()
		{
			yield return this.m_uiSubUIMiddle;
			yield return this.m_uiReward;
			yield return this.m_uiTip;
			yield return this.m_uiWorldmap;
			yield return this.m_uiShadowTime;
			yield return this.m_uiShadowLife;
			yield return this.m_ui_FierceBattle;
			yield return this.m_ui_RearmExtract;
			yield return this.m_uiKillCount;
			yield return this.m_uiMiscContract;
			yield return this.m_uiTrim;
			yield break;
		}

		// Token: 0x06008926 RID: 35110 RVA: 0x002E5D74 File Offset: 0x002E3F74
		private void SelectTitle(NKCUIResult.eTitleType type)
		{
			if (NKCReplayMgr.IsPlayingReplay())
			{
				type = NKCUIResult.eTitleType.Replay;
			}
			this.m_eCurrentTitleType = type;
			switch (this.m_eCurrentTitleType)
			{
			case NKCUIResult.eTitleType.Win:
				this.m_uiCurrentPlayingTitleAni = this.m_aniTitleWin;
				return;
			case NKCUIResult.eTitleType.Lose:
				this.m_uiCurrentPlayingTitleAni = this.m_aniTitleLose;
				return;
			case NKCUIResult.eTitleType.Get:
				this.m_uiCurrentPlayingTitleAni = this.m_aniTitleGet;
				return;
			case NKCUIResult.eTitleType.Replay:
				this.m_uiCurrentPlayingTitleAni = this.m_aniTitleReplay;
				return;
			case NKCUIResult.eTitleType.WinPrivate:
				this.m_uiCurrentPlayingTitleAni = this.m_aniTitleWinPrivate;
				return;
			case NKCUIResult.eTitleType.LosePrivate:
				this.m_uiCurrentPlayingTitleAni = this.m_aniTitleLosePrivate;
				return;
			case NKCUIResult.eTitleType.DrawPrivate:
				this.m_uiCurrentPlayingTitleAni = this.m_aniTitleDrawPrivate;
				return;
			default:
				return;
			}
		}

		// Token: 0x06008927 RID: 35111 RVA: 0x002E5E18 File Offset: 0x002E4018
		public void Init()
		{
			this.m_rtRoot = base.GetComponent<RectTransform>();
			this.m_aniTitleWin = this.m_rtRoot.Find("Result_WIN").GetComponent<Animator>();
			this.m_aniTitleLose = this.m_rtRoot.Find("Result_LOSE").GetComponent<Animator>();
			this.m_aniTitleGet = this.m_rtRoot.Find("Result_GET").GetComponent<Animator>();
			if (this.m_rtRoot.Find("Result_REPLAY") != null)
			{
				this.m_aniTitleReplay = this.m_rtRoot.Find("Result_REPLAY").GetComponent<Animator>();
			}
			if (this.m_rtRoot.Find("Result_PRIVATE_WIN") != null)
			{
				this.m_aniTitleWinPrivate = this.m_rtRoot.Find("Result_PRIVATE_WIN").GetComponent<Animator>();
			}
			if (this.m_rtRoot.Find("Result_PRIVATE_LOSE") != null)
			{
				this.m_aniTitleLosePrivate = this.m_rtRoot.Find("Result_PRIVATE_LOSE").GetComponent<Animator>();
			}
			if (this.m_rtRoot.Find("Result_PRIVATE_DRAW") != null)
			{
				this.m_aniTitleDrawPrivate = this.m_rtRoot.Find("Result_PRIVATE_DRAW").GetComponent<Animator>();
			}
			this.m_lbGetTitle = this.m_rtRoot.Find("Result_GET/SUB_TITLE/TEXT").GetComponent<Text>();
			this.m_objBottomButton = this.m_rtRoot.Find("Continue_button").gameObject;
			this.m_rtTitleRoot = this.m_rtRoot.Find("Result_GET/SUB_TITLE").GetComponent<RectTransform>();
			this.m_rtBackgroundOpen = this.m_rtRoot.Find("NKM_UI_WARFARE_RESULT_BG").GetComponent<RectTransform>();
			this.m_btnContinue = this.m_rtRoot.Find("Continue_button").GetComponent<NKCUIComStateButton>();
			this.m_btnContinue.PointerDown.RemoveAllListeners();
			this.m_btnContinue.PointerDown.AddListener(delegate(PointerEventData eventData)
			{
				this.OnClickContinue();
			});
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerDown;
			entry.callback = new EventTrigger.TriggerEvent();
			entry.callback.AddListener(new UnityAction<BaseEventData>(this.OnUserInputEvent));
			this.m_eventTrigger = this.m_rtRoot.gameObject.GetComponent<EventTrigger>();
			this.m_eventTrigger.triggers.Add(entry);
			this.m_btnBattleStatistics = this.m_rtRoot.Find("Battle_Statistics_button").GetComponent<NKCUIComStateButton>();
			NKCUtil.SetBindFunction(this.m_btnBattleStatistics, delegate()
			{
				NKCUIResult.OnTouchGameRecord onTouchGameRecord = this.dOnTouchGameRecord;
				if (onTouchGameRecord == null)
				{
					return;
				}
				onTouchGameRecord();
			});
			this.m_btnReplayBattleStatistics = this.m_rtRoot.Find("REPLAY_Battle_Statistics_button").GetComponent<NKCUIComStateButton>();
			NKCUtil.SetBindFunction(this.m_btnReplayBattleStatistics, delegate()
			{
				NKCUIResult.OnTouchGameRecord onTouchGameRecord = this.dOnTouchGameRecord;
				if (onTouchGameRecord == null)
				{
					return;
				}
				onTouchGameRecord();
			});
			NKCUtil.SetBindFunction(this.m_csbtnRepeatOperation, new UnityAction(this.OnClickRepeatOperation));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnTrimRetry, new UnityAction(this.OnTrimRetry));
			if (this.m_lstUnitGainRewardData != null && this.m_lstUnitGainRewardData.Count > 0)
			{
				Log.Error(string.Format("NKCUIResult - m_lstUnitGainRewardData is not null! Count[{0}]", this.m_lstUnitGainRewardData.Count), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUIResult.cs", 303);
			}
			this.m_lstUnitGainRewardData = null;
			this.m_uiSubUIMiddle.Init();
			this.m_uiReward.Init(this.m_lbDoubleTokenCount, this.m_amtorDoubleToken);
			NKCUIResultMiscContract uiMiscContract = this.m_uiMiscContract;
			if (uiMiscContract != null)
			{
				uiMiscContract.Init();
			}
			NKCUtil.SetBindFunction(this.m_csbtnShare, new UnityAction(this.OnClickShareBtn));
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06008928 RID: 35112 RVA: 0x002E6185 File Offset: 0x002E4385
		private void OnClickShareBtn()
		{
			NKCPopupSnsShareMenu.Instance.Open(new NKCPopupSnsShareMenu.OnClickSnsIcon(this.OnClickSnsShareIcon));
		}

		// Token: 0x06008929 RID: 35113 RVA: 0x002E619D File Offset: 0x002E439D
		private void OnClickSnsShareIcon(NKCPublisherModule.SNS_SHARE_TYPE eSNS_SHARE_TYPE)
		{
			this.m_crtShare = base.StartCoroutine(this.ProcessShare(eSNS_SHARE_TYPE));
		}

		// Token: 0x1700160B RID: 5643
		// (get) Token: 0x0600892A RID: 35114 RVA: 0x002E61B2 File Offset: 0x002E43B2
		private string CapturePath
		{
			get
			{
				return Path.Combine(Application.persistentDataPath, "ScreenCapture.png");
			}
		}

		// Token: 0x1700160C RID: 5644
		// (get) Token: 0x0600892B RID: 35115 RVA: 0x002E61C3 File Offset: 0x002E43C3
		private string ThumbnailPath
		{
			get
			{
				return Path.Combine(Application.persistentDataPath, "Thumbnail.png");
			}
		}

		// Token: 0x0600892C RID: 35116 RVA: 0x002E61D4 File Offset: 0x002E43D4
		private IEnumerator ProcessShare(NKCPublisherModule.SNS_SHARE_TYPE eSNS_SHARE_TYPE)
		{
			yield return null;
			NKCUtil.SetGameobjectActive(this.m_objShareBtn, false);
			NKCUtil.SetGameobjectActive(this.m_objShare, true);
			yield return new WaitForEndOfFrame();
			if (!NKCScreenCaptureUtility.CaptureScreenWithThumbnail(this.CapturePath, this.ThumbnailPath))
			{
				this.OnShareFinished(NKC_PUBLISHER_RESULT_CODE.NPRC_MARKETING_SNS_SHARE_FAIL, null);
				yield break;
			}
			yield return null;
			NKCPublisherModule.Marketing.TrySnsShare(eSNS_SHARE_TYPE, this.CapturePath, this.ThumbnailPath, new NKCPublisherModule.OnComplete(this.OnShareFinished));
			yield break;
		}

		// Token: 0x0600892D RID: 35117 RVA: 0x002E61EA File Offset: 0x002E43EA
		private void _OnShareFinished()
		{
			NKCUtil.SetGameobjectActive(this.m_objShareBtn, true);
			NKCUtil.SetGameobjectActive(this.m_objShare, false);
		}

		// Token: 0x0600892E RID: 35118 RVA: 0x002E6204 File Offset: 0x002E4404
		private void OnShareFinished(NKC_PUBLISHER_RESULT_CODE result, string additionalError)
		{
			if (NKMContentsVersionManager.HasCountryTag(CountryTagType.CHN))
			{
				this._OnShareFinished();
				return;
			}
			if (!NKCPublisherModule.CheckError(result, additionalError, true, new NKCPopupOKCancel.OnButton(this._OnShareFinished), false))
			{
				return;
			}
			this._OnShareFinished();
		}

		// Token: 0x0600892F RID: 35119 RVA: 0x002E6234 File Offset: 0x002E4434
		private void OnClickContinue()
		{
			this.m_bHadUserInput = true;
			foreach (NKCUIResultSubUIBase nkcuiresultSubUIBase in this.GetSubUIEnumerator())
			{
				if (nkcuiresultSubUIBase.gameObject.activeInHierarchy)
				{
					nkcuiresultSubUIBase.OnUserInput();
				}
			}
		}

		// Token: 0x06008930 RID: 35120 RVA: 0x002E6294 File Offset: 0x002E4494
		private void OnUserInputEvent(BaseEventData eventData)
		{
			this.OnClickContinue();
		}

		// Token: 0x06008931 RID: 35121 RVA: 0x002E629C File Offset: 0x002E449C
		private void CloseAllSubUI()
		{
			foreach (NKCUIResultSubUIBase nkcuiresultSubUIBase in this.GetSubUIEnumerator())
			{
				nkcuiresultSubUIBase.Close();
				nkcuiresultSubUIBase.ProcessRequired = false;
			}
		}

		// Token: 0x06008932 RID: 35122 RVA: 0x002E62F0 File Offset: 0x002E44F0
		public override void Hide()
		{
			if (this.m_uiReward.gameObject.activeInHierarchy)
			{
				this.m_uiReward.SetActiveLoopScrollList(false);
			}
			if (this.m_uiMiscContract.gameObject.activeInHierarchy)
			{
				this.m_uiMiscContract.SetActiveLoopScrollList(false);
			}
			this.m_bHide = true;
			this.m_rtRoot.localScale = Vector3.zero;
		}

		// Token: 0x06008933 RID: 35123 RVA: 0x002E6350 File Offset: 0x002E4550
		public override void UnHide()
		{
			this.m_bHide = false;
			this.m_rtRoot.localScale = Vector3.one;
			if (this.m_uiReward.gameObject.activeInHierarchy)
			{
				this.m_uiReward.SetActiveLoopScrollList(true);
			}
			if (this.m_uiMiscContract.gameObject.activeInHierarchy)
			{
				this.m_uiMiscContract.SetActiveLoopScrollList(true);
			}
		}

		// Token: 0x06008934 RID: 35124 RVA: 0x002E63B0 File Offset: 0x002E45B0
		private void OnClickBattleStatistics(NKCUIBattleStatistics.BattleData battleData)
		{
			if (battleData == null)
			{
				return;
			}
			NKCUIBattleStatistics.Instance.Open(battleData, delegate
			{
				this.SetPause(false);
			});
			this.SetPause(true);
		}

		// Token: 0x06008935 RID: 35125 RVA: 0x002E63D4 File Offset: 0x002E45D4
		private void OnClickRepeatOperation()
		{
			NKCPopupRepeatOperation.Instance.Open(delegate
			{
				this.SetPause(false);
				if (!NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing())
				{
					NKCUIBase.SetGameObjectActive(this.m_objRepeatOperation, false);
				}
			});
			this.SetPause(true);
		}

		// Token: 0x06008936 RID: 35126 RVA: 0x002E63F4 File Offset: 0x002E45F4
		private void HideCommonObjects()
		{
			NKCUtil.SetGameobjectActive(this.m_objBottomButton, false);
			NKCUtil.SetGameobjectActive(this.m_btnBattleStatistics, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnTrimRetry, false);
			NKCUtil.SetGameobjectActive(this.m_btnReplayBattleStatistics, false);
			NKCUtil.SetGameobjectActive(this.m_objDoubleToken, false);
			NKCUtil.SetGameobjectActive(this.m_objRepeatOperation, false);
		}

		// Token: 0x06008937 RID: 35127 RVA: 0x002E644C File Offset: 0x002E464C
		public static NKCUIResult.BattleResultData MakePvEBattleResultData(NKM_GAME_TYPE gameType, NKMGame nkmGame, NKMPacket_GAME_END_NOT cPacket_GAME_END_NOT, int dungeonID, int stageID)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKCUIResult.BattleResultData battleResultData;
			switch (gameType)
			{
			case NKM_GAME_TYPE.NGT_DIVE:
				battleResultData = NKCUIResult.MakeMissionResultData(nkmuserData.m_ArmyData, dungeonID, stageID, cPacket_GAME_END_NOT.win, cPacket_GAME_END_NOT.dungeonClearData, cPacket_GAME_END_NOT.deckIndex, NKCUIBattleStatistics.MakeBattleData(nkmGame, cPacket_GAME_END_NOT), cPacket_GAME_END_NOT.updatedUnits, NKCScenManager.GetScenManager().GetGameClient().MultiplyReward);
				battleResultData.m_NKM_GAME_TYPE = NKM_GAME_TYPE.NGT_DIVE;
				goto IL_21A;
			case NKM_GAME_TYPE.NGT_PVP_RANK:
			case NKM_GAME_TYPE.NGT_TUTORIAL:
			case NKM_GAME_TYPE.NGT_CUTSCENE:
			case NKM_GAME_TYPE.NGT_WORLDMAP:
			case NKM_GAME_TYPE.NGT_ASYNC_PVP:
				break;
			case NKM_GAME_TYPE.NGT_RAID:
			case NKM_GAME_TYPE.NGT_RAID_SOLO:
				battleResultData = NKCUIResult.MakeRaidResultData(nkmuserData.m_ArmyData, dungeonID, cPacket_GAME_END_NOT.raidBossResultData, NKCUIBattleStatistics.MakeBattleData(nkmGame, cPacket_GAME_END_NOT));
				goto IL_21A;
			case NKM_GAME_TYPE.NGT_SHADOW_PALACE:
				battleResultData = NKCUIResult.MakeShadowResultData(nkmuserData.m_ArmyData, cPacket_GAME_END_NOT.deckIndex, dungeonID, cPacket_GAME_END_NOT.win, cPacket_GAME_END_NOT.dungeonClearData, cPacket_GAME_END_NOT.shadowGameResult, nkmuserData.m_ShadowPalace, NKCUIBattleStatistics.MakeBattleData(nkmGame, cPacket_GAME_END_NOT), cPacket_GAME_END_NOT.updatedUnits);
				goto IL_21A;
			case NKM_GAME_TYPE.NGT_FIERCE:
				battleResultData = NKCUIResult.MakeFierceResultData(dungeonID, cPacket_GAME_END_NOT.win, cPacket_GAME_END_NOT.dungeonClearData, cPacket_GAME_END_NOT.fierceResultData, NKCUIBattleStatistics.MakeBattleData(nkmGame, cPacket_GAME_END_NOT));
				goto IL_21A;
			case NKM_GAME_TYPE.NGT_PHASE:
				battleResultData = NKCUIResult.MakePhaseResultData(stageID, nkmuserData.m_ArmyData, cPacket_GAME_END_NOT, NKCUIBattleStatistics.MakeBattleData(nkmGame, cPacket_GAME_END_NOT), 1);
				goto IL_21A;
			case NKM_GAME_TYPE.NGT_GUILD_DUNGEON_ARENA:
				battleResultData = NKCUIResult.MakeMissionResultData(nkmuserData.m_ArmyData, dungeonID, stageID, cPacket_GAME_END_NOT.win, cPacket_GAME_END_NOT.dungeonClearData, cPacket_GAME_END_NOT.deckIndex, NKCUIBattleStatistics.MakeBattleData(nkmGame, cPacket_GAME_END_NOT), cPacket_GAME_END_NOT.updatedUnits, NKCScenManager.GetScenManager().GetGameClient().MultiplyReward);
				goto IL_21A;
			case NKM_GAME_TYPE.NGT_GUILD_DUNGEON_BOSS:
				battleResultData = NKCUIResult.MakeRaidResultData(nkmuserData.m_ArmyData, dungeonID, cPacket_GAME_END_NOT.raidBossResultData, NKCUIBattleStatistics.MakeBattleData(nkmGame, cPacket_GAME_END_NOT));
				goto IL_21A;
			default:
				if (gameType == NKM_GAME_TYPE.NGT_TRIM)
				{
					battleResultData = NKCUIResult.MakeMissionResultData(nkmuserData.m_ArmyData, dungeonID, stageID, cPacket_GAME_END_NOT.win, cPacket_GAME_END_NOT.dungeonClearData, cPacket_GAME_END_NOT.deckIndex, NKCUIBattleStatistics.MakeBattleData(nkmGame, cPacket_GAME_END_NOT), cPacket_GAME_END_NOT.updatedUnits, NKCScenManager.GetScenManager().GetGameClient().MultiplyReward);
					battleResultData.m_NKM_GAME_TYPE = NKM_GAME_TYPE.NGT_TRIM;
					goto IL_21A;
				}
				break;
			}
			battleResultData = NKCUIResult.MakeMissionResultData(nkmuserData.m_ArmyData, dungeonID, stageID, cPacket_GAME_END_NOT.win, cPacket_GAME_END_NOT.dungeonClearData, cPacket_GAME_END_NOT.deckIndex, NKCUIBattleStatistics.MakeBattleData(nkmGame, cPacket_GAME_END_NOT), cPacket_GAME_END_NOT.updatedUnits, NKCScenManager.GetScenManager().GetGameClient().MultiplyReward);
			IL_21A:
			NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(stageID);
			if (nkmstageTempletV != null)
			{
				if (nkmstageTempletV.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_KILLCOUNT && cPacket_GAME_END_NOT.killCountData != null && cPacket_GAME_END_NOT.win)
				{
					battleResultData.m_KillCountGain = cPacket_GAME_END_NOT.killCountDelta;
					battleResultData.m_KillCountTotal = cPacket_GAME_END_NOT.killCountData.killCount;
					battleResultData.m_KillCountStageRecord = NKCScenManager.CurrentUserData().GetStageKillCountBest(stageID);
				}
				if (nkmstageTempletV.m_STAGE_SUB_TYPE == STAGE_SUB_TYPE.SST_TIMEATTACK)
				{
					battleResultData.m_ShadowCurrClearTime = (int)cPacket_GAME_END_NOT.totalPlayTime;
					battleResultData.m_ShadowBestClearTime = NKCScenManager.CurrentUserData().GetStageBestClearSec(stageID);
				}
			}
			return battleResultData;
		}

		// Token: 0x06008938 RID: 35128 RVA: 0x002E66F0 File Offset: 0x002E48F0
		public static NKCUIResult.BattleResultData MakePvPResultData(BATTLE_RESULT_TYPE battleResultType, NKMItemMiscData cNKMItemMiscData, NKCUIBattleStatistics.BattleData battleData, NKM_GAME_TYPE gameType)
		{
			NKCUIResult.BattleResultData battleResultData = new NKCUIResult.BattleResultData();
			battleResultData.m_stageID = 0;
			battleResultData.m_bShowMedal = true;
			battleResultData.m_bShowBonus = false;
			battleResultData.m_BATTLE_RESULT_TYPE = battleResultType;
			battleResultData.m_lstMissionData = new List<NKCUIResultSubUIDungeon.MissionData>();
			battleResultData.m_NKM_GAME_TYPE = gameType;
			NKCUIResultSubUIDungeon.MissionData missionData = new NKCUIResultSubUIDungeon.MissionData();
			missionData.bSuccess = (battleResultType == BATTLE_RESULT_TYPE.BRT_WIN);
			missionData.eMissionType = DUNGEON_GAME_MISSION_TYPE.DGMT_CLEAR;
			battleResultData.m_lstMissionData.Add(missionData);
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			PvpState pvpState;
			if (gameType != NKM_GAME_TYPE.NGT_PVP_RANK)
			{
				if (gameType != NKM_GAME_TYPE.NGT_ASYNC_PVP)
				{
					switch (gameType)
					{
					case NKM_GAME_TYPE.NGT_PVP_PRIVATE:
						pvpState = myUserData.m_PvpData;
						goto IL_CE;
					case NKM_GAME_TYPE.NGT_PVP_LEAGUE:
						pvpState = myUserData.m_LeagueData;
						goto IL_CE;
					case NKM_GAME_TYPE.NGT_PVP_STRATEGY:
					case NKM_GAME_TYPE.NGT_PVP_STRATEGY_REVENGE:
					case NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC:
						break;
					default:
						Debug.LogError("[NKCUIResult.MakePvPResultData] 모르는 게임타입이 들어옴 - " + gameType.ToString());
						return null;
					}
				}
				pvpState = myUserData.m_AsyncData;
			}
			else
			{
				pvpState = myUserData.m_PvpData;
			}
			IL_CE:
			int num = NKCPVPManager.FindPvPSeasonID(gameType, NKCSynchronizedTime.GetServerUTCTime(0.0));
			if (num == pvpState.SeasonID)
			{
				battleResultData.m_OrgPVPScore = pvpState.Score;
				battleResultData.m_OrgPVPTier = pvpState.LeagueTierID;
			}
			else
			{
				battleResultData.m_OrgPVPScore = NKCPVPManager.GetResetScore(pvpState.SeasonID, pvpState.Score, gameType);
				NKMPvpRankTemplet rankTempletByScore = NKCPVPManager.GetRankTempletByScore(gameType, num, battleResultData.m_OrgPVPScore);
				if (rankTempletByScore != null)
				{
					battleResultData.m_OrgPVPTier = rankTempletByScore.LeagueTier;
				}
			}
			battleResultData.m_OrgDoubleToken = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetCountMiscItem(301);
			if (cNKMItemMiscData != null)
			{
				battleResultData.m_RewardData = new NKMRewardData();
				battleResultData.m_RewardData.MiscItemDataList.Add(cNKMItemMiscData);
			}
			battleResultData.m_battleData = battleData;
			return battleResultData;
		}

		// Token: 0x06008939 RID: 35129 RVA: 0x002E6884 File Offset: 0x002E4A84
		public static NKCUIResult.BattleResultData MakeMissionResultData(NKMArmyData armyData, int dungeonID, int stageID, bool bWin, NKMDungeonClearData dungeonClearData, NKMDeckIndex deckIndex, NKCUIBattleStatistics.BattleData battleData, List<UnitLoyaltyUpdateData> lstUnitUpdateData = null, int multiplyReward = 1)
		{
			NKCUIResult.BattleResultData battleResultData = new NKCUIResult.BattleResultData();
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(dungeonID);
			if (dungeonTempletBase == null)
			{
				Debug.LogError("DungeonTemplet Not Found!!");
				return battleResultData;
			}
			battleResultData.m_stageID = stageID;
			battleResultData.m_lstMissionData = new List<NKCUIResultSubUIDungeon.MissionData>();
			bool flag = true;
			bool flag2 = deckIndex.m_eDeckType == NKM_DECK_TYPE.NDT_FRIEND;
			if (flag2)
			{
				flag = false;
			}
			if (deckIndex.m_eDeckType == NKM_DECK_TYPE.NDT_DIVE)
			{
				flag = false;
			}
			if (dungeonTempletBase.IsUsingEventDeck())
			{
				deckIndex = NKMDeckIndex.None;
			}
			NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(dungeonTempletBase.m_DungeonStrID);
			battleResultData.m_firstRewardData = null;
			battleResultData.m_firstAllClearData = null;
			if (bWin)
			{
				battleResultData.m_BATTLE_RESULT_TYPE = BATTLE_RESULT_TYPE.BRT_WIN;
			}
			else
			{
				battleResultData.m_BATTLE_RESULT_TYPE = BATTLE_RESULT_TYPE.BRT_LOSE;
			}
			battleResultData.m_bShowMedal = (nkmstageTempletV != null);
			battleResultData.m_bShowBonus = (dungeonTempletBase.BonusResult && bWin);
			bool flag3 = false;
			FirstRewardData firstRewardData = (nkmstageTempletV != null) ? nkmstageTempletV.GetFirstRewardData() : null;
			NKMRewardData nkmrewardData = null;
			if (nkmstageTempletV != null && firstRewardData != null && bWin && firstRewardData.RewardId > 0 && !NKCScenManager.CurrentUserData().CheckDungeonClear(nkmstageTempletV.DungeonTempletBase.m_DungeonID))
			{
				flag3 = true;
			}
			bool flag4;
			if (!(flag4 = !NKCScenManager.GetScenManager().GetMyUserData().CheckDungeonClear(dungeonID)) && dungeonClearData != null && (!dungeonClearData.missionResult1 || !dungeonClearData.missionResult2 || !dungeonClearData.missionRewardResult || (dungeonClearData != null && dungeonClearData.missionReward != null)))
			{
				flag4 = true;
			}
			if (flag)
			{
				NKCUIResultSubUIDungeon.MissionData missionData = new NKCUIResultSubUIDungeon.MissionData();
				missionData.bSuccess = bWin;
				missionData.eMissionType = DUNGEON_GAME_MISSION_TYPE.DGMT_CLEAR;
				battleResultData.m_lstMissionData.Add(missionData);
				NKCUIResultSubUIDungeon.MissionData missionData2 = new NKCUIResultSubUIDungeon.MissionData();
				missionData2.bSuccess = (dungeonClearData != null && dungeonClearData.missionResult1);
				missionData2.eMissionType = dungeonTempletBase.m_DGMissionType_1;
				missionData2.iMissionValue = dungeonTempletBase.m_DGMissionValue_1;
				battleResultData.m_lstMissionData.Add(missionData2);
				NKCUIResultSubUIDungeon.MissionData missionData3 = new NKCUIResultSubUIDungeon.MissionData();
				missionData3.bSuccess = (dungeonClearData != null && dungeonClearData.missionResult2);
				missionData3.eMissionType = dungeonTempletBase.m_DGMissionType_2;
				missionData3.iMissionValue = dungeonTempletBase.m_DGMissionValue_2;
				battleResultData.m_lstMissionData.Add(missionData3);
			}
			if (dungeonClearData != null)
			{
				NKMRewardData nkmrewardData2;
				if (dungeonClearData.rewardData != null)
				{
					nkmrewardData2 = dungeonClearData.rewardData.DeepCopy<NKMRewardData>();
				}
				else
				{
					nkmrewardData2 = new NKMRewardData();
					Debug.LogWarning("Dungeon Reward Data is null !!");
				}
				battleResultData.m_lstUnitLevelupData = NKCUIResult.MakeUnitLevelupExpData(armyData, nkmrewardData2.UnitExpDataList, deckIndex, lstUnitUpdateData);
				battleResultData.m_iUnitExpBonusRate = 0;
				if (nkmrewardData2.UnitExpDataList != null && nkmrewardData2.UnitExpDataList.Count > 0)
				{
					battleResultData.m_iUnitExpBonusRate = nkmrewardData2.UnitExpDataList[0].m_BonusRatio;
				}
				battleResultData.m_RewardData = nkmrewardData2;
				if (dungeonClearData.missionReward != null)
				{
					nkmrewardData = dungeonClearData.missionReward.DeepCopy<NKMRewardData>();
				}
				if (dungeonClearData.oneTimeRewards != null)
				{
					battleResultData.m_OnetimeRewardData = dungeonClearData.oneTimeRewards.DeepCopy<NKMRewardData>();
				}
			}
			else if (!flag2)
			{
				battleResultData.m_lstUnitLevelupData = NKCUIResult.MakeUnitLevelupExpData(armyData, null, deckIndex, lstUnitUpdateData);
				battleResultData.m_iUnitExpBonusRate = 0;
			}
			if (flag3 && firstRewardData != null)
			{
				battleResultData.m_firstRewardData = NKCUIResult.GetRewardItemAfterFilter(ref battleResultData.m_RewardData, firstRewardData.Type, firstRewardData.RewardId, firstRewardData.RewardQuantity);
			}
			if (flag4 && nkmrewardData != null)
			{
				battleResultData.m_firstAllClearData = NKCUIResult.GetRewardItemAfterFilter(ref nkmrewardData, nkmstageTempletV.MissionReward.rewardType, nkmstageTempletV.MissionReward.ID, nkmstageTempletV.MissionReward.Count);
			}
			battleResultData.m_battleData = battleData;
			battleResultData.m_multiply = multiplyReward;
			return battleResultData;
		}

		// Token: 0x0600893A RID: 35130 RVA: 0x002E6BC0 File Offset: 0x002E4DC0
		private static NKMRewardData GetRewardItemAfterFilter(ref NKMRewardData rewardData, NKM_REWARD_TYPE rewardType, int rewardID, int rewardCount)
		{
			NKMRewardData nkmrewardData = new NKMRewardData();
			if (rewardData == null)
			{
				return nkmrewardData;
			}
			if (!NKMRewardTemplet.IsValidReward(rewardType, rewardID))
			{
				return nkmrewardData;
			}
			if (!NKMRewardTemplet.IsOpenedReward(rewardType, rewardID, false))
			{
				return nkmrewardData;
			}
			switch (rewardType)
			{
			case NKM_REWARD_TYPE.RT_UNIT:
			case NKM_REWARD_TYPE.RT_SHIP:
			{
				NKMUnitData nkmunitData = rewardData.UnitDataList.Find((NKMUnitData x) => x.m_UnitID == rewardID);
				if (nkmunitData != null)
				{
					rewardData.UnitDataList.Remove(nkmunitData);
					nkmrewardData.UnitDataList.Add(nkmunitData);
					return nkmrewardData;
				}
				return nkmrewardData;
			}
			case NKM_REWARD_TYPE.RT_MISC:
			{
				NKMItemMiscData nkmitemMiscData = rewardData.MiscItemDataList.Find((NKMItemMiscData x) => x.ItemID == rewardID && x.TotalCount == (long)rewardCount);
				if (nkmitemMiscData != null)
				{
					rewardData.MiscItemDataList.Remove(nkmitemMiscData);
					nkmrewardData.MiscItemDataList.Add(nkmitemMiscData);
					return nkmrewardData;
				}
				NKMInteriorData nkminteriorData = rewardData.Interiors.Find((NKMInteriorData x) => x.itemId == rewardID && x.count == (long)rewardCount);
				if (nkminteriorData != null)
				{
					rewardData.Interiors.Remove(nkminteriorData);
					nkmrewardData.Interiors.Add(nkminteriorData);
					return nkmrewardData;
				}
				return nkmrewardData;
			}
			case NKM_REWARD_TYPE.RT_EQUIP:
			{
				NKMEquipItemData nkmequipItemData = rewardData.EquipItemDataList.Find((NKMEquipItemData x) => x.m_ItemEquipID == rewardID);
				if (nkmequipItemData != null)
				{
					rewardData.EquipItemDataList.Remove(nkmequipItemData);
					nkmrewardData.EquipItemDataList.Add(nkmequipItemData);
					return nkmrewardData;
				}
				return nkmrewardData;
			}
			case NKM_REWARD_TYPE.RT_MOLD:
			{
				NKMMoldItemData nkmmoldItemData = rewardData.MoldItemDataList.Find((NKMMoldItemData x) => x.m_MoldID == rewardID && x.m_Count == (long)rewardCount);
				if (nkmmoldItemData != null)
				{
					rewardData.MoldItemDataList.Remove(nkmmoldItemData);
					nkmrewardData.MoldItemDataList.Add(nkmmoldItemData);
					return nkmrewardData;
				}
				return nkmrewardData;
			}
			case NKM_REWARD_TYPE.RT_SKIN:
				if (rewardData.SkinIdList.Contains(rewardID))
				{
					rewardData.SkinIdList.Remove(rewardID);
					nkmrewardData.SkinIdList.Add(rewardID);
					return nkmrewardData;
				}
				return nkmrewardData;
			case NKM_REWARD_TYPE.RT_OPERATOR:
			{
				NKMOperator nkmoperator = rewardData.OperatorList.Find((NKMOperator x) => x.id == rewardID);
				if (nkmoperator != null)
				{
					rewardData.OperatorList.Remove(nkmoperator);
					nkmrewardData.OperatorList.Add(nkmoperator);
					return nkmrewardData;
				}
				return nkmrewardData;
			}
			}
			Debug.LogError(string.Format("해당 타입 GetRewardItemAfterFilter 추가 필요 - {0}", rewardType));
			return nkmrewardData;
		}

		// Token: 0x0600893B RID: 35131 RVA: 0x002E6E1C File Offset: 0x002E501C
		public static NKCUIResult.BattleResultData MakeRaidResultData(NKMArmyData armyData, int dungeonID, NKMRaidBossResultData cNKMRaidBossResultData, NKCUIBattleStatistics.BattleData battleData)
		{
			NKCUIResult.BattleResultData battleResultData = new NKCUIResult.BattleResultData();
			if (cNKMRaidBossResultData == null)
			{
				Debug.LogError("BossResultData is null!! When make Raid Result Data");
				return battleResultData;
			}
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(dungeonID);
			if (dungeonTempletBase == null)
			{
				Debug.LogError("DungeonTemplet Not Found!!");
				return battleResultData;
			}
			BATTLE_RESULT_TYPE battle_RESULT_TYPE = BATTLE_RESULT_TYPE.BRT_LOSE;
			battleResultData.m_RaidBossResultData = cNKMRaidBossResultData;
			if (cNKMRaidBossResultData.curHP <= 0f)
			{
				battle_RESULT_TYPE = BATTLE_RESULT_TYPE.BRT_WIN;
			}
			battleResultData.m_bShowMedal = true;
			battleResultData.m_bShowBonus = dungeonTempletBase.BonusResult;
			battleResultData.m_BATTLE_RESULT_TYPE = battle_RESULT_TYPE;
			battleResultData.m_battleData = battleData;
			return battleResultData;
		}

		// Token: 0x0600893C RID: 35132 RVA: 0x002E6E90 File Offset: 0x002E5090
		public static NKCUIResult.BattleResultData MakePhaseResultData(int stageID, NKMArmyData armyData, NKMPacket_GAME_END_NOT sPacket, NKCUIBattleStatistics.BattleData battleData, int multiplyReward = 1)
		{
			NKCUIResult.BattleResultData battleResultData = new NKCUIResult.BattleResultData();
			NKMDeckIndex nkmdeckIndex = sPacket.deckIndex;
			bool win = sPacket.win;
			battleResultData.m_stageID = stageID;
			NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(stageID);
			if (nkmstageTempletV == null)
			{
				Debug.LogError("phase clear : StageTemplet Not Found!!");
				return battleResultData;
			}
			NKMPhaseTemplet phaseTemplet = nkmstageTempletV.PhaseTemplet;
			if (phaseTemplet == null)
			{
				Debug.LogError("phase clear : PhaseTemplet  Not Found!!");
				return battleResultData;
			}
			battleResultData.m_NKM_GAME_TYPE = NKM_GAME_TYPE.NGT_PHASE;
			battleResultData.m_lstMissionData = new List<NKCUIResultSubUIDungeon.MissionData>();
			bool flag = true;
			bool flag2 = nkmdeckIndex.m_eDeckType == NKM_DECK_TYPE.NDT_FRIEND;
			if (flag2)
			{
				flag = false;
			}
			if (phaseTemplet.IsUsingEventDeck())
			{
				nkmdeckIndex = NKMDeckIndex.None;
			}
			battleResultData.m_firstRewardData = null;
			battleResultData.m_firstAllClearData = null;
			if (win)
			{
				battleResultData.m_BATTLE_RESULT_TYPE = BATTLE_RESULT_TYPE.BRT_WIN;
			}
			else
			{
				battleResultData.m_BATTLE_RESULT_TYPE = BATTLE_RESULT_TYPE.BRT_LOSE;
			}
			battleResultData.m_bShowMedal = (nkmstageTempletV != null);
			battleResultData.m_bShowBonus = (phaseTemplet.BonusResult && win);
			bool flag3 = !NKCScenManager.GetScenManager().GetMyUserData().CheckStageCleared(nkmstageTempletV);
			bool flag4 = flag3;
			bool flag5 = false;
			FirstRewardData firstRewardData = nkmstageTempletV.GetFirstRewardData();
			NKMRewardData nkmrewardData = null;
			if (nkmstageTempletV != null)
			{
				NKMEpisodeTempletV2 episodeTemplet = nkmstageTempletV.EpisodeTemplet;
				if (episodeTemplet != null && episodeTemplet.m_EPCategory == EPISODE_CATEGORY.EC_DAILY)
				{
					flag = false;
				}
				if (firstRewardData != null && (win && firstRewardData.RewardId > 0 && flag3))
				{
					flag5 = true;
				}
			}
			NKMPhaseClearData phaseClearData = sPacket.phaseClearData;
			if (!flag3 && phaseClearData != null && (!phaseClearData.missionResult1 || !phaseClearData.missionResult2 || !phaseClearData.missionRewardResult || (phaseClearData != null && phaseClearData.missionReward != null)))
			{
				flag4 = true;
			}
			if (flag)
			{
				NKCUIResultSubUIDungeon.MissionData missionData = new NKCUIResultSubUIDungeon.MissionData();
				missionData.bSuccess = win;
				missionData.eMissionType = DUNGEON_GAME_MISSION_TYPE.DGMT_CLEAR;
				battleResultData.m_lstMissionData.Add(missionData);
				NKCUIResultSubUIDungeon.MissionData missionData2 = new NKCUIResultSubUIDungeon.MissionData();
				missionData2.bSuccess = (phaseClearData != null && phaseClearData.missionResult1);
				missionData2.eMissionType = phaseTemplet.m_DGMissionType_1;
				missionData2.iMissionValue = phaseTemplet.m_DGMissionValue_1;
				battleResultData.m_lstMissionData.Add(missionData2);
				NKCUIResultSubUIDungeon.MissionData missionData3 = new NKCUIResultSubUIDungeon.MissionData();
				missionData3.bSuccess = (phaseClearData != null && phaseClearData.missionResult2);
				missionData3.eMissionType = phaseTemplet.m_DGMissionType_2;
				missionData3.iMissionValue = phaseTemplet.m_DGMissionValue_2;
				battleResultData.m_lstMissionData.Add(missionData3);
			}
			if (phaseClearData != null)
			{
				NKMRewardData nkmrewardData2;
				if (phaseClearData.rewardData != null)
				{
					nkmrewardData2 = phaseClearData.rewardData.DeepCopy<NKMRewardData>();
				}
				else
				{
					nkmrewardData2 = new NKMRewardData();
					Debug.LogWarning("Dungeon Reward Data is null !!");
				}
				battleResultData.m_lstUnitLevelupData = NKCUIResult.MakeUnitLevelupExpData(armyData, nkmrewardData2.UnitExpDataList, nkmdeckIndex, sPacket.updatedUnits);
				battleResultData.m_iUnitExpBonusRate = 0;
				if (nkmrewardData2.UnitExpDataList != null && nkmrewardData2.UnitExpDataList.Count > 0)
				{
					battleResultData.m_iUnitExpBonusRate = nkmrewardData2.UnitExpDataList[0].m_BonusRatio;
				}
				battleResultData.m_RewardData = nkmrewardData2;
				if (phaseClearData.missionReward != null)
				{
					nkmrewardData = phaseClearData.missionReward.DeepCopy<NKMRewardData>();
				}
				if (phaseClearData.oneTimeRewards != null)
				{
					battleResultData.m_OnetimeRewardData = phaseClearData.oneTimeRewards.DeepCopy<NKMRewardData>();
				}
			}
			else if (!flag2)
			{
				battleResultData.m_lstUnitLevelupData = NKCUIResult.MakeUnitLevelupExpData(armyData, null, nkmdeckIndex, sPacket.updatedUnits);
				battleResultData.m_iUnitExpBonusRate = 0;
			}
			NKMDungeonClearData dungeonClearData = sPacket.dungeonClearData;
			if (dungeonClearData != null)
			{
				if (dungeonClearData.rewardData != null)
				{
					if (battleResultData.m_RewardData == null)
					{
						battleResultData.m_RewardData = dungeonClearData.rewardData.DeepCopy<NKMRewardData>();
					}
					else
					{
						battleResultData.m_RewardData.AddRewardDataForRepeatOperation(dungeonClearData.rewardData);
					}
				}
				if (dungeonClearData.missionReward != null)
				{
					if (nkmrewardData == null)
					{
						nkmrewardData = dungeonClearData.missionReward.DeepCopy<NKMRewardData>();
					}
					else
					{
						nkmrewardData.AddRewardDataForRepeatOperation(dungeonClearData.missionReward);
					}
				}
				if (dungeonClearData.oneTimeRewards != null)
				{
					if (battleResultData.m_OnetimeRewardData == null)
					{
						battleResultData.m_OnetimeRewardData = dungeonClearData.oneTimeRewards.DeepCopy<NKMRewardData>();
					}
					else
					{
						battleResultData.m_OnetimeRewardData.AddRewardDataForRepeatOperation(dungeonClearData.oneTimeRewards);
					}
				}
			}
			if (flag5 && firstRewardData != null)
			{
				battleResultData.m_firstRewardData = NKCUIResult.GetRewardItemAfterFilter(ref battleResultData.m_RewardData, firstRewardData.Type, firstRewardData.RewardId, firstRewardData.RewardQuantity);
			}
			if (flag4 && nkmrewardData != null)
			{
				battleResultData.m_firstAllClearData = NKCUIResult.GetRewardItemAfterFilter(ref nkmrewardData, nkmstageTempletV.MissionReward.rewardType, nkmstageTempletV.MissionReward.ID, nkmstageTempletV.MissionReward.Count);
			}
			battleResultData.m_battleData = battleData;
			battleResultData.m_multiply = multiplyReward;
			return battleResultData;
		}

		// Token: 0x0600893D RID: 35133 RVA: 0x002E72A0 File Offset: 0x002E54A0
		public static NKCUIResult.BattleResultData MakeShadowResultData(NKMArmyData armyData, NKMDeckIndex deckIndex, int dungeonID, bool bWin, NKMDungeonClearData dungeonClearData, NKMShadowGameResult shadowResult, NKMShadowPalace shadowPalaceData, NKCUIBattleStatistics.BattleData battleData, List<UnitLoyaltyUpdateData> lstUnitUpdateData = null)
		{
			NKCUIResult.BattleResultData battleResultData = new NKCUIResult.BattleResultData();
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(dungeonID);
			if (dungeonTempletBase == null)
			{
				Debug.LogError("DungeonTemplet Not Found!!");
				return battleResultData;
			}
			battleResultData.m_NKM_GAME_TYPE = NKM_GAME_TYPE.NGT_SHADOW_PALACE;
			battleResultData.m_lstMissionData = new List<NKCUIResultSubUIDungeon.MissionData>();
			bool flag = true;
			if (dungeonTempletBase.IsUsingEventDeck())
			{
				deckIndex = NKMDeckIndex.None;
			}
			battleResultData.m_firstRewardData = null;
			battleResultData.m_firstAllClearData = null;
			if (bWin)
			{
				battleResultData.m_BATTLE_RESULT_TYPE = BATTLE_RESULT_TYPE.BRT_WIN;
			}
			else
			{
				battleResultData.m_BATTLE_RESULT_TYPE = BATTLE_RESULT_TYPE.BRT_LOSE;
			}
			battleResultData.m_bShadowAllClear = (shadowResult.rewardData != null);
			battleResultData.m_ShadowPrevLife = shadowPalaceData.life;
			battleResultData.m_ShadowCurrLife = shadowResult.life;
			battleResultData.m_ShadowBestClearTime = 0;
			battleResultData.m_ShadowCurrClearTime = ((shadowResult.dungeonData != null) ? shadowResult.dungeonData.recentTime : 0);
			NKMPalaceData nkmpalaceData = shadowPalaceData.palaceDataList.Find((NKMPalaceData v) => v.palaceId == shadowPalaceData.currentPalaceId);
			if (nkmpalaceData != null)
			{
				NKMPalaceDungeonData nkmpalaceDungeonData = nkmpalaceData.dungeonDataList.Find((NKMPalaceDungeonData v) => v.dungeonId == dungeonID);
				if (nkmpalaceDungeonData != null)
				{
					battleResultData.m_ShadowBestClearTime = nkmpalaceDungeonData.bestTime;
				}
			}
			battleResultData.m_bShowMedal = false;
			battleResultData.m_bShowBonus = (dungeonTempletBase.BonusResult && bWin);
			if (flag)
			{
				NKCUIResultSubUIDungeon.MissionData missionData = new NKCUIResultSubUIDungeon.MissionData();
				missionData.bSuccess = bWin;
				missionData.eMissionType = DUNGEON_GAME_MISSION_TYPE.DGMT_CLEAR;
				battleResultData.m_lstMissionData.Add(missionData);
				NKCUIResultSubUIDungeon.MissionData missionData2 = new NKCUIResultSubUIDungeon.MissionData();
				missionData2.bSuccess = (dungeonClearData != null && dungeonClearData.missionResult1);
				missionData2.eMissionType = dungeonTempletBase.m_DGMissionType_1;
				missionData2.iMissionValue = dungeonTempletBase.m_DGMissionValue_1;
				battleResultData.m_lstMissionData.Add(missionData2);
				NKCUIResultSubUIDungeon.MissionData missionData3 = new NKCUIResultSubUIDungeon.MissionData();
				missionData3.bSuccess = (dungeonClearData != null && dungeonClearData.missionResult2);
				missionData3.eMissionType = dungeonTempletBase.m_DGMissionType_2;
				missionData3.iMissionValue = dungeonTempletBase.m_DGMissionValue_2;
				battleResultData.m_lstMissionData.Add(missionData3);
			}
			if (dungeonClearData != null)
			{
				NKMRewardData nkmrewardData;
				if (dungeonClearData.rewardData != null)
				{
					nkmrewardData = dungeonClearData.rewardData.DeepCopy<NKMRewardData>();
				}
				else
				{
					nkmrewardData = new NKMRewardData();
					Debug.LogWarning("Dungeon Reward Data is null !!");
				}
				battleResultData.m_lstUnitLevelupData = NKCUIResult.MakeUnitLevelupExpData(armyData, nkmrewardData.UnitExpDataList, deckIndex, lstUnitUpdateData);
				battleResultData.m_iUnitExpBonusRate = 0;
				if (nkmrewardData.UnitExpDataList != null && nkmrewardData.UnitExpDataList.Count > 0)
				{
					battleResultData.m_iUnitExpBonusRate = nkmrewardData.UnitExpDataList[0].m_BonusRatio;
				}
				battleResultData.m_RewardData = nkmrewardData;
				if (dungeonClearData.oneTimeRewards != null)
				{
					battleResultData.m_OnetimeRewardData = dungeonClearData.oneTimeRewards.DeepCopy<NKMRewardData>();
				}
			}
			else
			{
				battleResultData.m_lstUnitLevelupData = NKCUIResult.MakeUnitLevelupExpData(armyData, null, deckIndex, lstUnitUpdateData);
				battleResultData.m_iUnitExpBonusRate = 0;
			}
			battleResultData.m_battleData = battleData;
			battleResultData.m_multiply = shadowPalaceData.rewardMultiply;
			return battleResultData;
		}

		// Token: 0x0600893E RID: 35134 RVA: 0x002E755C File Offset: 0x002E575C
		public void OpenBattleResult(NKCUIResult.BattleResultData data, NKCUIResult.OnClose onClose, bool bAutoSkip = false)
		{
			if (data == null)
			{
				if (onClose != null)
				{
					onClose();
				}
				return;
			}
			this.UnHide();
			this.dOnClose = onClose;
			base.gameObject.SetActive(true);
			this.ShowBonusType();
			this.CloseAllSubUI();
			NKCUtil.SetGameobjectActive(this.m_objShareBtn, false);
			NKCUtil.SetGameobjectActive(this.m_objShare, false);
			if (data.m_BATTLE_RESULT_TYPE == BATTLE_RESULT_TYPE.BRT_WIN)
			{
				this.SelectTitle(NKCUIResult.eTitleType.Win);
			}
			else
			{
				this.SelectTitle(NKCUIResult.eTitleType.Lose);
			}
			NKCUtil.SetGameobjectActive(this.m_objUserBuff, NKCScenManager.CurrentUserData().m_companyBuffDataList.Count > 0);
			NKCUtil.SetGameobjectActive(this.m_objBottomButton, false);
			NKCUtil.SetGameobjectActive(this.m_btnBattleStatistics, data.m_battleData != null);
			NKCUtil.SetGameobjectActive(this.m_btnReplayBattleStatistics, false);
			NKCUtil.SetGameobjectActive(this.m_objDoubleToken, false);
			this.SetTrimRetryButton(data.m_NKM_GAME_TYPE);
			this.SetOperationMultiply(data.m_multiply, this.IsDisplayMultiplyUI());
			this.SetContractPoint(0);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME_RESULT)
			{
				if (NKCRepeatOperaion.CheckVisible(NKCScenManager.GetScenManager().Get_NKC_SCEN_GAME_RESULT().GetStageID()))
				{
					NKCRepeatOperaion nkcrepeatOperaion = NKCScenManager.GetScenManager().GetNKCRepeatOperaion();
					if (nkcrepeatOperaion != null)
					{
						NKCUtil.SetGameobjectActive(this.m_objRepeatOperation, nkcrepeatOperaion.GetIsOnGoing());
						NKCUtil.SetGameobjectActive(this.m_objRepeatOperationCountDown, false);
						if (NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing())
						{
							NKCUtil.SetLabelText(this.m_lbRepeatOperation, string.Format("({0}/{1})", nkcrepeatOperaion.GetCurrRepeatCount(), nkcrepeatOperaion.GetMaxRepeatCount()));
						}
					}
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objRepeatOperation, false);
			}
			if (data.m_battleData != null)
			{
				this.dOnTouchGameRecord = delegate()
				{
					this.OnClickBattleStatistics(data.m_battleData);
				};
			}
			base.UIOpened(true);
			this.m_uiSubUIMiddle.SetDataBattleResult(data, (float)(bAutoSkip ? 2 : 0), false);
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			this.SetUnitGetOpenData(new List<NKMRewardData>
			{
				data.m_RewardData,
				data.m_OnetimeRewardData
			}, armyData, true, true);
			NKCUIResultSubUIReward.Data data2 = new NKCUIResultSubUIReward.Data();
			data2.rewardData = data.m_RewardData;
			data2.armyData = armyData;
			data2.bAutoSkipUnitGain = bAutoSkip;
			data2.firstRewardData = data.m_firstRewardData;
			data2.firstAllClearRewardData = data.m_firstAllClearData;
			data2.bIgnoreAutoClose = false;
			data2.selectRewardData = null;
			data2.selectSlotText = "";
			data2.bAllowRewardDataNull = false;
			data2.onetimeRewardData = data.m_OnetimeRewardData;
			data2.additionalReward = data.m_additionalReward;
			data2.battleResultType = data.m_BATTLE_RESULT_TYPE;
			this.m_uiReward.SetData(data2);
			this.m_uiKillCount.SetData(data, false);
			NKCUIResultMiscContract uiMiscContract = this.m_uiMiscContract;
			NKMRewardData rewardData = data.m_RewardData;
			uiMiscContract.SetData((rewardData != null) ? rewardData.ContractList : null);
			if (data.m_NKM_GAME_TYPE != NKM_GAME_TYPE.NGT_FIERCE)
			{
				this.m_uiTip.SetData(data.m_BATTLE_RESULT_TYPE, false);
			}
			this.m_uiWorldmap.SetData(false, null);
			this.m_uiShadowTime.SetData(data.m_BATTLE_RESULT_TYPE, data.m_ShadowCurrClearTime, data.m_ShadowBestClearTime, false);
			this.m_uiShadowLife.SetData(data.m_BATTLE_RESULT_TYPE, data.m_ShadowPrevLife, data.m_ShadowCurrLife, false);
			NKCUtil.SetGameobjectActive(this.m_WIN, data.m_NKM_GAME_TYPE != NKM_GAME_TYPE.NGT_FIERCE && data.m_NKM_GAME_TYPE != NKM_GAME_TYPE.NGT_GUILD_DUNGEON_BOSS);
			NKCUtil.SetGameobjectActive(this.m_LOSE, data.m_NKM_GAME_TYPE != NKM_GAME_TYPE.NGT_FIERCE && data.m_NKM_GAME_TYPE != NKM_GAME_TYPE.NGT_GUILD_DUNGEON_BOSS);
			NKCUtil.SetGameobjectActive(this.m_WIN_BATTLE_REPORT, data.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_FIERCE);
			NKCUtil.SetGameobjectActive(this.m_LOSE_BATTLE_REPORT, data.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_FIERCE);
			NKCUtil.SetGameobjectActive(this.m_ASSIST, data.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_GUILD_DUNGEON_BOSS);
			if (data.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_FIERCE)
			{
				this.m_uiSubUIMiddle.SetDataNull();
				this.m_ui_FierceBattle.SetData(data, false);
			}
			this.m_uiTrim.SetData(data);
			this.m_LastCoroutine = base.StartCoroutine(this.ProcessResultUI(bAutoSkip));
		}

		// Token: 0x0600893F RID: 35135 RVA: 0x002E79F8 File Offset: 0x002E5BF8
		public static NKCUIResult.BattleResultData MakeFierceResultData(int dungeonID, bool bWin, NKMDungeonClearData dungeonClearData, NKMFierceResultData fierceResult, NKCUIBattleStatistics.BattleData battleData)
		{
			NKCUIResult.BattleResultData battleResultData = new NKCUIResult.BattleResultData();
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(dungeonID);
			if (dungeonTempletBase == null)
			{
				Debug.LogError("DungeonTemplet Not Found!!");
				return battleResultData;
			}
			battleResultData.m_NKM_GAME_TYPE = NKM_GAME_TYPE.NGT_FIERCE;
			battleResultData.m_lstMissionData = new List<NKCUIResultSubUIDungeon.MissionData>();
			battleResultData.m_firstRewardData = null;
			battleResultData.m_firstAllClearData = null;
			battleResultData.m_BATTLE_RESULT_TYPE = (bWin ? BATTLE_RESULT_TYPE.BRT_WIN : BATTLE_RESULT_TYPE.BRT_LOSE);
			battleResultData.m_iFierceBestScore = fierceResult.bestPoint;
			battleResultData.m_iFierceScore = fierceResult.accquirePoint;
			battleResultData.m_fFierceLastBossHPPercent = (float)fierceResult.hpPercent;
			battleResultData.m_fFierceRestTime = fierceResult.restTime;
			battleResultData.m_battleData = battleData;
			if (true)
			{
				NKCUIResultSubUIDungeon.MissionData missionData = new NKCUIResultSubUIDungeon.MissionData();
				missionData.bSuccess = bWin;
				missionData.eMissionType = DUNGEON_GAME_MISSION_TYPE.DGMT_CLEAR;
				battleResultData.m_lstMissionData.Add(missionData);
				NKCUIResultSubUIDungeon.MissionData missionData2 = new NKCUIResultSubUIDungeon.MissionData();
				missionData2.bSuccess = (dungeonClearData != null && dungeonClearData.missionResult1);
				missionData2.eMissionType = dungeonTempletBase.m_DGMissionType_1;
				missionData2.iMissionValue = dungeonTempletBase.m_DGMissionValue_1;
				battleResultData.m_lstMissionData.Add(missionData2);
				NKCUIResultSubUIDungeon.MissionData missionData3 = new NKCUIResultSubUIDungeon.MissionData();
				missionData3.bSuccess = (dungeonClearData != null && dungeonClearData.missionResult2);
				missionData3.eMissionType = dungeonTempletBase.m_DGMissionType_2;
				missionData3.iMissionValue = dungeonTempletBase.m_DGMissionValue_2;
				battleResultData.m_lstMissionData.Add(missionData3);
			}
			return battleResultData;
		}

		// Token: 0x06008940 RID: 35136 RVA: 0x002E7B2C File Offset: 0x002E5D2C
		public static NKCUIResult.BattleResultData MakeGuildCoopArenaResultData(NKMArmyData armyData, int dungeonID, bool bWin, NKMDungeonClearData dungeonClearData, NKMDeckIndex deckIndex, NKCUIBattleStatistics.BattleData battleData)
		{
			NKCUIResult.BattleResultData battleResultData = new NKCUIResult.BattleResultData();
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(dungeonID);
			if (dungeonTempletBase == null)
			{
				Debug.LogError("DungeonTemplet Not Found!!");
				return battleResultData;
			}
			battleResultData.m_lstMissionData = new List<NKCUIResultSubUIDungeon.MissionData>();
			if (dungeonTempletBase.IsUsingEventDeck())
			{
				deckIndex = NKMDeckIndex.None;
			}
			if (bWin)
			{
				battleResultData.m_BATTLE_RESULT_TYPE = BATTLE_RESULT_TYPE.BRT_WIN;
			}
			else
			{
				battleResultData.m_BATTLE_RESULT_TYPE = BATTLE_RESULT_TYPE.BRT_LOSE;
			}
			battleResultData.m_bShowMedal = true;
			battleResultData.m_bShowBonus = (dungeonTempletBase.BonusResult && bWin);
			NKCUIResultSubUIDungeon.MissionData missionData = new NKCUIResultSubUIDungeon.MissionData();
			missionData.bSuccess = bWin;
			missionData.eMissionType = DUNGEON_GAME_MISSION_TYPE.DGMT_CLEAR;
			battleResultData.m_lstMissionData.Add(missionData);
			NKCUIResultSubUIDungeon.MissionData missionData2 = new NKCUIResultSubUIDungeon.MissionData();
			missionData2.bSuccess = (dungeonClearData != null && dungeonClearData.missionResult1);
			missionData2.eMissionType = dungeonTempletBase.m_DGMissionType_1;
			missionData2.iMissionValue = dungeonTempletBase.m_DGMissionValue_1;
			battleResultData.m_lstMissionData.Add(missionData2);
			NKCUIResultSubUIDungeon.MissionData missionData3 = new NKCUIResultSubUIDungeon.MissionData();
			missionData3.bSuccess = (dungeonClearData != null && dungeonClearData.missionResult2);
			missionData3.eMissionType = dungeonTempletBase.m_DGMissionType_2;
			missionData3.iMissionValue = dungeonTempletBase.m_DGMissionValue_2;
			battleResultData.m_lstMissionData.Add(missionData3);
			if (dungeonClearData != null)
			{
				NKMRewardData rewardData;
				if (dungeonClearData.rewardData != null)
				{
					rewardData = dungeonClearData.rewardData.DeepCopy<NKMRewardData>();
				}
				else
				{
					rewardData = new NKMRewardData();
					Debug.LogWarning("Dungeon Reward Data is null !!");
				}
				battleResultData.m_RewardData = rewardData;
			}
			else
			{
				battleResultData.m_lstUnitLevelupData = NKCUIResult.MakeUnitLevelupExpData(armyData, null, deckIndex, null);
				battleResultData.m_iUnitExpBonusRate = 0;
			}
			battleResultData.m_firstRewardData = null;
			battleResultData.m_firstAllClearData = null;
			battleResultData.m_battleData = battleData;
			battleResultData.m_multiply = 1;
			battleResultData.m_bShowClearPoint = true;
			int num = 0;
			foreach (KeyValuePair<int, GuildDungeonInfoTemplet> keyValuePair in NKCGuildCoopManager.m_dicGuildDungeonInfoTemplet)
			{
				if (keyValuePair.Value.GetSeasonDungeonId() == dungeonTempletBase.Key)
				{
					num = keyValuePair.Key;
					break;
				}
			}
			if (num == 0)
			{
				return battleResultData;
			}
			battleResultData.m_fArenaClearPoint = NKCGuildCoopManager.GetClearPointPercentage(num);
			return battleResultData;
		}

		// Token: 0x06008941 RID: 35137 RVA: 0x002E7D1C File Offset: 0x002E5F1C
		private bool IsDisplayMultiplyUI()
		{
			bool result = false;
			NKCGameClient gameClient = NKCScenManager.GetScenManager().GetGameClient();
			if (gameClient != null && gameClient.GetGameData() != null)
			{
				NKMStageTempletV2 nkmstageTempletV = null;
				NKM_GAME_TYPE gameType = gameClient.GetGameData().GetGameType();
				if (gameType <= NKM_GAME_TYPE.NGT_WARFARE)
				{
					if (gameType != NKM_GAME_TYPE.NGT_DUNGEON)
					{
						if (gameType == NKM_GAME_TYPE.NGT_WARFARE)
						{
							NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(gameClient.GetGameData().m_WarfareID);
							if (nkmwarfareTemplet != null)
							{
								nkmstageTempletV = nkmwarfareTemplet.StageTemplet;
							}
						}
					}
					else
					{
						NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(gameClient.GetGameData().m_DungeonID);
						if (dungeonTempletBase != null)
						{
							nkmstageTempletV = dungeonTempletBase.StageTemplet;
						}
					}
				}
				else if (gameType != NKM_GAME_TYPE.NGT_SHADOW_PALACE)
				{
					if (gameType == NKM_GAME_TYPE.NGT_PHASE || gameType == NKM_GAME_TYPE.NGT_TRIM)
					{
						return false;
					}
				}
				else
				{
					result = NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.SHADOW_PALACE_MULTIPLY);
				}
				if (nkmstageTempletV != null)
				{
					EPISODE_CATEGORY episodeCategory = nkmstageTempletV.EpisodeCategory;
					if (episodeCategory <= EPISODE_CATEGORY.EC_DAILY || episodeCategory - EPISODE_CATEGORY.EC_SIDESTORY <= 4)
					{
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06008942 RID: 35138 RVA: 0x002E7DDC File Offset: 0x002E5FDC
		private void ShowBonusType()
		{
			NKCGameClient gameClient = NKCScenManager.GetScenManager().GetGameClient();
			if (gameClient == null)
			{
				return;
			}
			NKMGameData gameData = gameClient.GetGameData();
			if (gameData == null)
			{
				return;
			}
			RewardTuningType type = RewardTuningType.None;
			NKM_GAME_TYPE gameType = gameData.GetGameType();
			if (gameType != NKM_GAME_TYPE.NGT_DUNGEON)
			{
				if (gameType != NKM_GAME_TYPE.NGT_WARFARE)
				{
					if (gameType == NKM_GAME_TYPE.NGT_PHASE)
					{
						NKMStageTempletV2 stageTemplet = NKCPhaseManager.GetStageTemplet();
						if (stageTemplet != null)
						{
							type = stageTemplet.m_BuffType;
						}
					}
				}
				else
				{
					NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(gameData.m_WarfareID);
					if (nkmwarfareTemplet != null)
					{
						type = nkmwarfareTemplet.StageTemplet.m_BuffType;
					}
				}
			}
			else
			{
				NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(gameData.m_DungeonID);
				if (dungeonTempletBase != null)
				{
					type = dungeonTempletBase.StageTemplet.m_BuffType;
				}
			}
			NKCUtil.SetGameobjectActive(this.m_RESULT_WIN_Bonus_Type, !type.Equals(RewardTuningType.None));
			NKCUtil.SetImageSprite(this.m_RESULT_WIN_Bonus_Type_icon, NKCUtil.GetBounsTypeIcon(type, false), false);
		}

		// Token: 0x06008943 RID: 35139 RVA: 0x002E7EA4 File Offset: 0x002E60A4
		public static NKCUIResult.CityMissionResultData MakeCityMissionCompleteUIData(NKMArmyData armyData, NKMWorldMapCityData cityData, NKMRewardData rewardData, int cityNewExp, int cityNewLevel, bool bGreatSuccess)
		{
			NKCUIResult.CityMissionResultData cityMissionResultData = new NKCUIResult.CityMissionResultData();
			cityMissionResultData.m_CityID = cityData.cityID;
			cityMissionResultData.m_CityLevelOld = cityData.level;
			cityMissionResultData.m_CityLevelNew = cityNewLevel;
			cityMissionResultData.m_CityExpOld = cityData.exp;
			cityMissionResultData.m_CityExpNew = cityNewExp;
			cityMissionResultData.m_bGreatSuccess = bGreatSuccess;
			cityMissionResultData.m_LeaderUnitData = armyData.GetUnitFromUID(cityData.leaderUnitUID);
			List<NKMRewardUnitExpData> list = new List<NKMRewardUnitExpData>(rewardData.UnitExpDataList);
			int num = list.FindIndex((NKMRewardUnitExpData x) => x.m_UnitUid == cityData.leaderUnitUID);
			if (num >= 0)
			{
				NKMUnitData unitFromUID = armyData.GetUnitFromUID(cityData.leaderUnitUID);
				NKMRewardUnitExpData unitExpData = list[num];
				cityMissionResultData.LeaderUnitLevelupData = NKCUIResult.MakeLevelupData(unitFromUID, unitExpData, -1);
			}
			cityMissionResultData.m_lstUnitLevelupData = NKCUIResult.MakeUnitLevelupExpData(armyData, list, NKMDeckIndex.None, null);
			cityMissionResultData.m_RewardData = rewardData.DeepCopy<NKMRewardData>();
			if (bGreatSuccess)
			{
				NKMWorldMapMissionTemplet missionTemplet = NKMWorldMapManager.GetMissionTemplet(cityData.worldMapMission.currentMissionID);
				if (missionTemplet != null && missionTemplet.m_CompleteRewardQuantity > 0 && NKMRewardTemplet.IsValidReward(missionTemplet.m_CompleteRewardType, missionTemplet.m_CompleteRewardID))
				{
					NKMRewardData nkmrewardData = new NKMRewardData();
					switch (missionTemplet.m_CompleteRewardType)
					{
					case NKM_REWARD_TYPE.RT_UNIT:
					case NKM_REWARD_TYPE.RT_SHIP:
					{
						NKMUnitData nkmunitData = cityMissionResultData.m_RewardData.UnitDataList.Find((NKMUnitData v) => v.m_UnitID == missionTemplet.m_CompleteRewardID);
						if (nkmunitData != null)
						{
							cityMissionResultData.m_RewardData.UnitDataList.Remove(nkmunitData);
							nkmrewardData.UnitDataList.Add(nkmunitData);
							goto IL_3A0;
						}
						goto IL_3A0;
					}
					case NKM_REWARD_TYPE.RT_MISC:
					{
						NKMItemMiscData nkmitemMiscData = cityMissionResultData.m_RewardData.MiscItemDataList.Find((NKMItemMiscData v) => v.ItemID == missionTemplet.m_CompleteRewardID);
						if (nkmitemMiscData != null)
						{
							cityMissionResultData.m_RewardData.MiscItemDataList.Remove(nkmitemMiscData);
							nkmrewardData.MiscItemDataList.Add(nkmitemMiscData);
							goto IL_3A0;
						}
						goto IL_3A0;
					}
					case NKM_REWARD_TYPE.RT_EQUIP:
					{
						NKMEquipItemData nkmequipItemData = cityMissionResultData.m_RewardData.EquipItemDataList.Find((NKMEquipItemData v) => v.m_ItemEquipID == missionTemplet.m_CompleteRewardID);
						if (nkmequipItemData != null)
						{
							cityMissionResultData.m_RewardData.EquipItemDataList.Remove(nkmequipItemData);
							nkmrewardData.EquipItemDataList.Add(nkmequipItemData);
							goto IL_3A0;
						}
						goto IL_3A0;
					}
					case NKM_REWARD_TYPE.RT_MOLD:
					{
						NKMMoldItemData nkmmoldItemData = cityMissionResultData.m_RewardData.MoldItemDataList.Find((NKMMoldItemData v) => v.m_MoldID == missionTemplet.m_CompleteRewardID);
						if (nkmmoldItemData != null)
						{
							cityMissionResultData.m_RewardData.MoldItemDataList.Remove(nkmmoldItemData);
							nkmrewardData.MoldItemDataList.Add(nkmmoldItemData);
							goto IL_3A0;
						}
						goto IL_3A0;
					}
					case NKM_REWARD_TYPE.RT_SKIN:
						if (cityMissionResultData.m_RewardData.SkinIdList.Contains(missionTemplet.m_CompleteRewardID))
						{
							cityMissionResultData.m_RewardData.SkinIdList.Remove(missionTemplet.m_CompleteRewardID);
							nkmrewardData.SkinIdList.Add(missionTemplet.m_CompleteRewardID);
							goto IL_3A0;
						}
						goto IL_3A0;
					case NKM_REWARD_TYPE.RT_OPERATOR:
					{
						NKMOperator nkmoperator = cityMissionResultData.m_RewardData.OperatorList.Find((NKMOperator v) => v.id == missionTemplet.m_CompleteRewardID);
						if (nkmoperator != null)
						{
							cityMissionResultData.m_RewardData.OperatorList.Remove(nkmoperator);
							nkmrewardData.OperatorList.Add(nkmoperator);
							goto IL_3A0;
						}
						goto IL_3A0;
					}
					}
					Debug.LogError("NKMWorldMapMissionTemplet.m_CompleteRewardType : " + missionTemplet.m_CompleteRewardType.ToString() + " ???");
					IL_3A0:
					cityMissionResultData.m_SuccessRewardData = nkmrewardData;
					cityMissionResultData.m_SuccessSlotText = NKCUtilString.GET_STRING_WORLDMAP_CITY_MISSION_REWARD_ADD_TEXT;
				}
			}
			return cityMissionResultData;
		}

		// Token: 0x06008944 RID: 35140 RVA: 0x002E8268 File Offset: 0x002E6468
		public void OpenCityMissionResult(NKCUIResult.CityMissionResultData resultData, NKCUIResult.OnClose onClose)
		{
			if (resultData == null)
			{
				if (onClose != null)
				{
					onClose();
				}
				return;
			}
			this.UnHide();
			this.dOnClose = onClose;
			this.CloseAllSubUI();
			NKCUtil.SetGameobjectActive(this.m_objShareBtn, false);
			NKCUtil.SetGameobjectActive(this.m_objShare, false);
			this.SelectTitle(NKCUIResult.eTitleType.Get);
			base.gameObject.SetActive(true);
			NKCUtil.SetGameobjectActive(this.m_objUserBuff, NKCScenManager.CurrentUserData().m_companyBuffDataList.Count > 0);
			this.SetOperationMultiply(0, false);
			this.SetContractPoint(0);
			this.HideCommonObjects();
			NKCUtil.SetLabelText(this.m_lbGetTitle, NKCUtilString.GET_STRING_RESULT_CITY_MISSION);
			NKCUtil.SetGameobjectActive(this.m_objTitle, !string.IsNullOrEmpty(this.m_lbGetTitle.text));
			base.UIOpened(true);
			this.m_uiSubUIMiddle.SetDataWorldMapMissionResult(resultData, 2f, false);
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			this.SetUnitGetOpenData(resultData.m_RewardData, armyData, true);
			NKCUIResultSubUIReward.Data data = new NKCUIResultSubUIReward.Data();
			data.rewardData = resultData.m_RewardData;
			data.armyData = armyData;
			data.bAutoSkipUnitGain = false;
			data.firstRewardData = null;
			data.bIgnoreAutoClose = false;
			data.selectRewardData = resultData.m_SuccessRewardData;
			data.selectSlotText = resultData.m_SuccessSlotText;
			this.m_uiReward.SetData(data);
			this.m_uiKillCount.SetData(null, false);
			NKCUIResultMiscContract uiMiscContract = this.m_uiMiscContract;
			NKMRewardData rewardData = resultData.m_RewardData;
			uiMiscContract.SetData((rewardData != null) ? rewardData.ContractList : null);
			this.m_uiTip.SetData(BATTLE_RESULT_TYPE.BRT_WIN, false);
			this.m_uiWorldmap.SetData(resultData.m_bGreatSuccess, resultData.m_LeaderUnitData);
			this.m_uiShadowTime.SetData(BATTLE_RESULT_TYPE.BRT_WIN, 0, 0, false);
			this.m_uiShadowLife.SetData(BATTLE_RESULT_TYPE.BRT_WIN, 0, 0, false);
			NKCUIResultSubUIFierceBattle ui_FierceBattle = this.m_ui_FierceBattle;
			if (ui_FierceBattle != null)
			{
				ui_FierceBattle.SetData(null, false);
			}
			NKCUIResultSubUIRearmament ui_RearmExtract = this.m_ui_RearmExtract;
			if (ui_RearmExtract != null)
			{
				ui_RearmExtract.SetData(null, null, false);
			}
			this.m_LastCoroutine = base.StartCoroutine(this.ProcessResultUI(false));
		}

		// Token: 0x06008945 RID: 35141 RVA: 0x002E8450 File Offset: 0x002E6650
		public void OpenMailResult(NKMArmyData armyData, NKMRewardData rewardData, NKCUIResult.OnClose onClose = null)
		{
			this.dOnClose = onClose;
			base.gameObject.SetActive(true);
			this.UnHide();
			this.CloseAllSubUI();
			NKCUtil.SetGameobjectActive(this.m_objShareBtn, false);
			NKCUtil.SetGameobjectActive(this.m_objShare, false);
			this.SelectTitle(NKCUIResult.eTitleType.Get);
			this.m_uiSubUIMiddle.SetDataNull();
			this.m_ui_FierceBattle.SetData(null, false);
			NKCUIResultSubUIRearmament ui_RearmExtract = this.m_ui_RearmExtract;
			if (ui_RearmExtract != null)
			{
				ui_RearmExtract.SetData(null, null, false);
			}
			this.SetUnitGetOpenData(rewardData, armyData, true);
			NKCUIResultSubUIReward.Data data = new NKCUIResultSubUIReward.Data();
			data.rewardData = rewardData;
			data.armyData = armyData;
			data.bAutoSkipUnitGain = false;
			this.m_uiReward.SetData(data);
			this.m_uiKillCount.SetData(null, false);
			this.m_uiMiscContract.SetData((rewardData != null) ? rewardData.ContractList : null);
			this.m_uiTip.SetData(BATTLE_RESULT_TYPE.BRT_WIN, false);
			this.m_uiWorldmap.SetData(false, null);
			this.m_uiShadowTime.SetData(BATTLE_RESULT_TYPE.BRT_WIN, 0, 0, false);
			this.m_uiShadowLife.SetData(BATTLE_RESULT_TYPE.BRT_WIN, 0, 0, false);
			if (!this.m_uiReward.ProcessRequired && !this.m_uiMiscContract.ProcessRequired)
			{
				base.gameObject.SetActive(false);
				Debug.Log("Mail uiReward Skip");
				if (onClose != null)
				{
					onClose();
				}
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objUserBuff, NKCScenManager.CurrentUserData().m_companyBuffDataList.Count > 0);
			this.SetOperationMultiply(0, false);
			this.SetContractPoint(0);
			this.HideCommonObjects();
			NKCUtil.SetLabelText(this.m_lbGetTitle, NKCUtilString.GET_STRING_MAIL);
			NKCUtil.SetGameobjectActive(this.m_objTitle, !string.IsNullOrEmpty(this.m_lbGetTitle.text));
			base.UIOpened(true);
			this.m_LastCoroutine = base.StartCoroutine(this.ProcessResultUI(false));
		}

		// Token: 0x06008946 RID: 35142 RVA: 0x002E8608 File Offset: 0x002E6808
		public void OpenPrivatePvpResult(NKMArmyData armyData, NKCUIResult.BattleResultData battleResultData, NKCUIResult.OnClose onClose, NKCUIBattleStatistics.BattleData battleData)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_objShareBtn, false);
			this.dOnClose = onClose;
			this.UnHide();
			this.CloseAllSubUI();
			NKCUtil.SetGameobjectActive(this.m_objShareBtn, false);
			NKCUtil.SetGameobjectActive(this.m_objShare, false);
			this.SelectTitle(NKCUIResult.eTitleType.Get);
			base.gameObject.SetActive(true);
			this.m_uiSubUIMiddle.SetDataNull();
			this.m_ui_FierceBattle.SetData(null, false);
			NKCUIResultSubUIRearmament ui_RearmExtract = this.m_ui_RearmExtract;
			if (ui_RearmExtract != null)
			{
				ui_RearmExtract.SetData(null, null, false);
			}
			this.SetUnitGetOpenData(battleResultData.m_RewardData, armyData, true);
			NKCUIResultSubUIReward.Data data = new NKCUIResultSubUIReward.Data();
			data.rewardData = battleResultData.m_RewardData;
			data.armyData = armyData;
			data.bAutoSkipUnitGain = false;
			data.firstRewardData = null;
			data.bIgnoreAutoClose = true;
			data.selectRewardData = null;
			data.selectSlotText = "";
			data.bAllowRewardDataNull = true;
			data.additionalReward = null;
			this.m_uiReward.SetData(data);
			this.m_uiKillCount.SetData(null, false);
			NKCUIResultMiscContract uiMiscContract = this.m_uiMiscContract;
			NKMRewardData rewardData = battleResultData.m_RewardData;
			uiMiscContract.SetData((rewardData != null) ? rewardData.ContractList : null);
			switch (battleResultData.m_BATTLE_RESULT_TYPE)
			{
			case BATTLE_RESULT_TYPE.BRT_WIN:
				this.SelectTitle(NKCUIResult.eTitleType.WinPrivate);
				break;
			case BATTLE_RESULT_TYPE.BRT_LOSE:
				this.SelectTitle(NKCUIResult.eTitleType.LosePrivate);
				break;
			case BATTLE_RESULT_TYPE.BRT_DRAW:
				this.SelectTitle(NKCUIResult.eTitleType.DrawPrivate);
				break;
			}
			NKCUtil.SetGameobjectActive(this.m_objUserBuff, NKCScenManager.CurrentUserData().m_companyBuffDataList.Count > 0);
			this.SetOperationMultiply(0, false);
			this.SetContractPoint(0);
			NKCUtil.SetGameobjectActive(this.m_objBottomButton, false);
			NKCUtil.SetGameobjectActive(this.m_objDoubleToken, false);
			NKCUtil.SetGameobjectActive(this.m_objRepeatOperation, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnTrimRetry, false);
			NKCUtil.SetLabelText(this.m_lbDoubleTokenCount, "");
			NKCUtil.SetLabelText(this.m_lbGetTitle, NKCUtilString.GET_STRING_RESULT_MISSION);
			NKCUtil.SetGameobjectActive(this.m_objTitle, !string.IsNullOrEmpty(this.m_lbGetTitle.text));
			if (NKCReplayMgr.IsPlayingReplay())
			{
				NKCReplayMgr nkcreplaMgr = NKCReplayMgr.GetNKCReplaMgr();
				if (nkcreplaMgr != null)
				{
					nkcreplaMgr.StopPlaying();
				}
			}
			if (battleData != null)
			{
				NKCUtil.SetGameobjectActive(this.m_btnBattleStatistics, false);
				NKCUtil.SetGameobjectActive(this.m_btnReplayBattleStatistics, true);
				this.dOnTouchGameRecord = delegate()
				{
					this.OnClickBattleStatistics(battleData);
				};
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_btnBattleStatistics, false);
			}
			base.UIOpened(true);
			this.m_LastCoroutine = base.StartCoroutine(this.ProcessResultUI(false));
		}

		// Token: 0x06008947 RID: 35143 RVA: 0x002E8880 File Offset: 0x002E6A80
		public void OpenComplexResult(NKMArmyData armyData, NKMRewardData rewardData, NKCUIResult.OnClose onClose = null, long orgDoubleTokenCount = 0L, NKCUIBattleStatistics.BattleData battleData = null, bool bIgnoreAutoClose = false, bool bAllowRewardDataNull = false)
		{
			this.OpenComplexResultFull(armyData, rewardData, null, onClose, orgDoubleTokenCount, battleData, bIgnoreAutoClose, bAllowRewardDataNull);
		}

		// Token: 0x06008948 RID: 35144 RVA: 0x002E88A0 File Offset: 0x002E6AA0
		public void OpenComplexResultFull(NKMArmyData armyData, NKMRewardData rewardData, NKMAdditionalReward additionalReward, NKCUIResult.OnClose onClose = null, long orgDoubleTokenCount = 0L, NKCUIBattleStatistics.BattleData battleData = null, bool bIgnoreAutoClose = false, bool bAllowRewardDataNull = false)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_objShareBtn, false);
			this.dOnClose = onClose;
			this.UnHide();
			this.CloseAllSubUI();
			NKCUtil.SetGameobjectActive(this.m_objShareBtn, false);
			NKCUtil.SetGameobjectActive(this.m_objShare, false);
			this.SelectTitle(NKCUIResult.eTitleType.Get);
			base.gameObject.SetActive(true);
			this.m_uiSubUIMiddle.SetDataNull();
			this.m_ui_FierceBattle.SetData(null, false);
			this.m_ui_RearmExtract.SetData(null, null, false);
			this.SetUnitGetOpenData(rewardData, armyData, true);
			NKCUIResultSubUIReward.Data data = new NKCUIResultSubUIReward.Data();
			data.rewardData = rewardData;
			data.armyData = armyData;
			data.bAutoSkipUnitGain = false;
			data.firstRewardData = null;
			data.bIgnoreAutoClose = bIgnoreAutoClose;
			data.selectRewardData = null;
			data.selectSlotText = "";
			data.bAllowRewardDataNull = bAllowRewardDataNull;
			data.additionalReward = additionalReward;
			this.m_uiReward.SetData(data);
			this.m_uiKillCount.SetData(null, false);
			this.m_uiMiscContract.SetData((rewardData != null) ? rewardData.ContractList : null);
			if (orgDoubleTokenCount > 0L)
			{
				this.m_uiReward.SetReservedDoubleTokenAddCount(orgDoubleTokenCount - NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(301));
			}
			this.m_uiTip.SetData(BATTLE_RESULT_TYPE.BRT_WIN, false);
			this.m_uiWorldmap.SetData(false, null);
			this.m_uiShadowTime.SetData(BATTLE_RESULT_TYPE.BRT_WIN, 0, 0, false);
			this.m_uiShadowLife.SetData(BATTLE_RESULT_TYPE.BRT_WIN, 0, 0, false);
			if (!this.m_uiReward.ProcessRequired && !this.m_uiMiscContract.ProcessRequired)
			{
				base.gameObject.SetActive(false);
				Debug.Log("PVPResult uiReward Skip");
				if (onClose != null)
				{
					onClose();
				}
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objUserBuff, NKCScenManager.CurrentUserData().m_companyBuffDataList.Count > 0);
			this.SetOperationMultiply(0, false);
			this.SetContractPoint(0);
			this.HideCommonObjects();
			if (battleData != null)
			{
				NKCUtil.SetGameobjectActive(this.m_btnBattleStatistics, !NKCReplayMgr.IsPlayingReplay());
				NKCUtil.SetGameobjectActive(this.m_btnReplayBattleStatistics, NKCReplayMgr.IsPlayingReplay());
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_btnBattleStatistics, false);
			}
			NKCUtil.SetLabelText(this.m_lbDoubleTokenCount, orgDoubleTokenCount.ToString());
			NKCUtil.SetLabelText(this.m_lbGetTitle, NKCUtilString.GET_STRING_RESULT_MISSION);
			NKCUtil.SetGameobjectActive(this.m_objTitle, !string.IsNullOrEmpty(this.m_lbGetTitle.text));
			if (NKCReplayMgr.IsPlayingReplay())
			{
				NKCReplayMgr nkcreplaMgr = NKCReplayMgr.GetNKCReplaMgr();
				if (nkcreplaMgr != null)
				{
					nkcreplaMgr.StopPlaying();
				}
			}
			if (battleData != null)
			{
				this.dOnTouchGameRecord = delegate()
				{
					this.OnClickBattleStatistics(battleData);
				};
			}
			base.UIOpened(true);
			this.m_LastCoroutine = base.StartCoroutine(this.ProcessResultUI(false));
		}

		// Token: 0x06008949 RID: 35145 RVA: 0x002E8B58 File Offset: 0x002E6D58
		public void OpenItemGain(List<NKMItemMiscData> lstItemData, string Title, string subTitle, NKCUIResult.OnClose onClose = null)
		{
			this.dOnClose = onClose;
			NKMRewardData nkmrewardData = new NKMRewardData();
			nkmrewardData.SetMiscItemData(lstItemData);
			this.UnHide();
			this.CloseAllSubUI();
			NKCUtil.SetGameobjectActive(this.m_objShareBtn, false);
			NKCUtil.SetGameobjectActive(this.m_objShare, false);
			this.SelectTitle(NKCUIResult.eTitleType.Get);
			base.gameObject.SetActive(true);
			this.m_uiSubUIMiddle.SetDataNull();
			this.m_ui_FierceBattle.SetData(null, false);
			NKCUIResultSubUIRearmament ui_RearmExtract = this.m_ui_RearmExtract;
			if (ui_RearmExtract != null)
			{
				ui_RearmExtract.SetData(null, null, false);
			}
			this.SetUnitGetOpenData(nkmrewardData, NKCScenManager.CurrentUserData().m_ArmyData, true);
			NKCUIResultSubUIReward.Data data = new NKCUIResultSubUIReward.Data();
			data.rewardData = nkmrewardData;
			data.armyData = null;
			this.m_uiReward.SetData(data);
			this.m_uiKillCount.SetData(null, false);
			this.m_uiMiscContract.SetData((nkmrewardData != null) ? nkmrewardData.ContractList : null);
			this.m_uiTip.SetData(BATTLE_RESULT_TYPE.BRT_WIN, false);
			this.m_uiWorldmap.SetData(false, null);
			this.m_uiShadowTime.SetData(BATTLE_RESULT_TYPE.BRT_WIN, 0, 0, false);
			this.m_uiShadowLife.SetData(BATTLE_RESULT_TYPE.BRT_WIN, 0, 0, false);
			if (!this.m_uiReward.ProcessRequired && !this.m_uiMiscContract.ProcessRequired)
			{
				if (onClose != null)
				{
					onClose();
				}
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objUserBuff, NKCScenManager.CurrentUserData().m_companyBuffDataList.Count > 0);
			this.HideCommonObjects();
			this.SetOperationMultiply(0, false);
			this.SetContractPoint(0);
			NKCUtil.SetLabelText(this.m_lbGetTitle, Title);
			NKCUtil.SetGameobjectActive(this.m_objTitle, !string.IsNullOrEmpty(this.m_lbGetTitle.text));
			base.UIOpened(true);
			this.m_LastCoroutine = base.StartCoroutine(this.ProcessResultUI(false));
		}

		// Token: 0x0600894A RID: 35146 RVA: 0x002E8D07 File Offset: 0x002E6F07
		public void OpenBoxGain(NKMArmyData armyData, NKMRewardData rewardData, int boxItemID, NKCUIResult.OnClose onClose = null)
		{
			this.OpenBoxGain(armyData, new List<NKMRewardData>
			{
				rewardData
			}, boxItemID, onClose);
		}

		// Token: 0x0600894B RID: 35147 RVA: 0x002E8D20 File Offset: 0x002E6F20
		public void OpenBoxGain(NKMArmyData armyData, List<NKMRewardData> lstRewardData, int boxItemID, NKCUIResult.OnClose onClose = null)
		{
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(boxItemID);
			string title = (itemMiscTempletByID != null) ? itemMiscTempletByID.GetItemName() : "";
			this.OpenBoxGain(armyData, lstRewardData, title, "", onClose, true, null, 1, true);
		}

		// Token: 0x0600894C RID: 35148 RVA: 0x002E8D5C File Offset: 0x002E6F5C
		public void OpenBoxGain(NKMArmyData armyData, List<NKMRewardData> lstRewardData, string Title, NKCUIResult.OnClose onClose = null)
		{
			this.OpenBoxGain(armyData, lstRewardData, Title, "", onClose, true, null, 1, true);
		}

		// Token: 0x0600894D RID: 35149 RVA: 0x002E8D80 File Offset: 0x002E6F80
		public void OpenBoxGain(NKMArmyData armyData, NKMRewardData rewardData, string Title, NKCUIResult.OnClose onClose = null, bool bDisplayUnitGet = true, int requestCount = 1, bool bDefaultSort = true)
		{
			this.OpenBoxGain(armyData, new List<NKMRewardData>
			{
				rewardData
			}, Title, "", onClose, bDisplayUnitGet, null, requestCount, bDefaultSort);
		}

		// Token: 0x0600894E RID: 35150 RVA: 0x002E8DB0 File Offset: 0x002E6FB0
		public void OpenBoxGain(NKMArmyData armyData, NKMRewardData rewardData, string Title, string subTitle = "", NKCUIResult.OnClose onClose = null)
		{
			this.OpenBoxGain(armyData, new List<NKMRewardData>
			{
				rewardData
			}, Title, subTitle, onClose, true, null, 1, true);
		}

		// Token: 0x0600894F RID: 35151 RVA: 0x002E8DDC File Offset: 0x002E6FDC
		public void OpenBoxGain(NKMArmyData armyData, List<NKMRewardData> lstRewardData, string Title, string subTitle, NKCUIResult.OnClose onClose = null, bool bDisplayUnitGet = true, List<NKMAdditionalReward> lstAdditionalRewards = null, int requestCount = 1, bool bDefaultSort = true)
		{
			this.dOnClose = onClose;
			this.UnHide();
			this.CloseAllSubUI();
			this.SelectTitle(NKCUIResult.eTitleType.Get);
			NKCUtil.SetGameobjectActive(this.m_objContractPoint, false);
			base.gameObject.SetActive(true);
			int contractPoint = 0;
			if (bDisplayUnitGet)
			{
				foreach (NKMRewardData nkmrewardData in lstRewardData)
				{
					if (nkmrewardData.MiscItemDataList != null && nkmrewardData.MiscItemDataList.Count > 0)
					{
						for (int i = 0; i < nkmrewardData.MiscItemDataList.Count; i++)
						{
							if (nkmrewardData.MiscItemDataList[i].ItemID == 401)
							{
								contractPoint = (int)nkmrewardData.MiscItemDataList[i].TotalCount;
								nkmrewardData.MiscItemDataList.RemoveAt(i);
								break;
							}
						}
					}
				}
			}
			this.SetContractPoint(contractPoint);
			List<NKCUISlot.SlotData> list = new List<NKCUISlot.SlotData>();
			HashSet<int> hashSet = new HashSet<int>();
			foreach (NKMRewardData nkmrewardData2 in lstRewardData)
			{
				list.AddRange(NKCUISlot.MakeSlotDataListFromReward(nkmrewardData2, false, false));
				if (nkmrewardData2.SkinIdList != null)
				{
					hashSet.UnionWith(nkmrewardData2.SkinIdList);
				}
			}
			if (lstAdditionalRewards != null)
			{
				foreach (NKMAdditionalReward reward in lstAdditionalRewards)
				{
					list.AddRange(NKCUISlot.MakeSlotDataListFromReward(reward));
				}
			}
			this.m_ui_FierceBattle.SetData(null, false);
			NKCUIResultSubUIRearmament ui_RearmExtract = this.m_ui_RearmExtract;
			if (ui_RearmExtract != null)
			{
				ui_RearmExtract.SetData(null, null, false);
			}
			this.m_uiSubUIMiddle.SetDataNull();
			if (bDisplayUnitGet)
			{
				this.SetUnitGetOpenData(lstRewardData, armyData, false, bDefaultSort);
			}
			else
			{
				this.m_lstUnitGainRewardData = null;
			}
			this.m_uiReward.SetBoxGainData(list);
			this.m_uiMiscContract.SetData(lstRewardData);
			this.m_uiTip.SetData(BATTLE_RESULT_TYPE.BRT_WIN, false);
			this.m_uiWorldmap.SetData(false, null);
			this.m_uiShadowTime.SetData(BATTLE_RESULT_TYPE.BRT_WIN, 0, 0, false);
			this.m_uiShadowLife.SetData(BATTLE_RESULT_TYPE.BRT_WIN, 0, 0, false);
			if (!this.m_uiReward.ProcessRequired && !this.m_uiMiscContract.ProcessRequired)
			{
				base.gameObject.SetActive(false);
				if (onClose != null)
				{
					onClose();
				}
				return;
			}
			NKCUtil.SetLabelText(this.m_lbGetTitle, Title);
			NKCUtil.SetGameobjectActive(this.m_objTitle, !string.IsNullOrEmpty(this.m_lbGetTitle.text));
			NKCUtil.SetGameobjectActive(this.m_objUserBuff, NKCScenManager.CurrentUserData().m_companyBuffDataList.Count > 0);
			this.HideCommonObjects();
			this.SetOperationMultiply(0, false);
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKCUtil.SetLabelText(this.m_lbLevel, NKCStringTable.GetString("SI_DP_LEVEL_ONE_PARAM", false), new object[]
			{
				nkmuserData.m_UserLevel
			});
			NKCUtil.SetLabelText(this.m_lbUserName, nkmuserData.m_UserNickName);
			NKCUtil.SetLabelText(this.m_lbUserUID, NKCUtilString.GetFriendCode(nkmuserData.m_FriendCode));
			bool bValue = NKCPublisherModule.Marketing.IsUseSnsShareOn10SeqContract();
			NKCUtil.SetGameobjectActive(this.m_objShareBtn, bValue);
			NKCUtil.SetGameobjectActive(this.m_objShare, false);
			base.UIOpened(true);
			this.m_LastCoroutine = base.StartCoroutine(this.ProcessResultUI(false));
		}

		// Token: 0x06008950 RID: 35152 RVA: 0x002E9144 File Offset: 0x002E7344
		public void OpenRewardGain(NKMArmyData armyData, NKMRewardData rewardData, string Title, string subTitle = "", NKCUIResult.OnClose onClose = null)
		{
			this.OpenRewardGain(armyData, rewardData, null, Title, subTitle, onClose);
		}

		// Token: 0x06008951 RID: 35153 RVA: 0x002E9154 File Offset: 0x002E7354
		public void OpenRewardGain(NKMArmyData armyData, NKMRewardData rewardData, NKMAdditionalReward additionalReward, string Title, string subTitle = "", NKCUIResult.OnClose onClose = null)
		{
			this.dOnClose = onClose;
			this.UnHide();
			this.CloseAllSubUI();
			NKCUtil.SetGameobjectActive(this.m_objShareBtn, false);
			NKCUtil.SetGameobjectActive(this.m_objShare, false);
			this.SelectTitle(NKCUIResult.eTitleType.Get);
			base.gameObject.SetActive(true);
			this.m_ui_FierceBattle.SetData(null, false);
			NKCUIResultSubUIRearmament ui_RearmExtract = this.m_ui_RearmExtract;
			if (ui_RearmExtract != null)
			{
				ui_RearmExtract.SetData(null, null, false);
			}
			this.m_uiSubUIMiddle.SetDataNull();
			this.SetUnitGetOpenData(rewardData, armyData, true);
			NKCUIResultSubUIReward.Data data = new NKCUIResultSubUIReward.Data();
			data.rewardData = rewardData;
			data.armyData = armyData;
			data.additionalReward = additionalReward;
			this.m_uiReward.SetData(data);
			this.m_uiKillCount.SetData(null, false);
			this.m_uiMiscContract.SetData((rewardData != null) ? rewardData.ContractList : null);
			this.m_uiTip.SetData(BATTLE_RESULT_TYPE.BRT_WIN, false);
			this.m_uiWorldmap.SetData(false, null);
			this.m_uiShadowTime.SetData(BATTLE_RESULT_TYPE.BRT_WIN, 0, 0, false);
			this.m_uiShadowLife.SetData(BATTLE_RESULT_TYPE.BRT_WIN, 0, 0, false);
			if (!this.m_uiReward.ProcessRequired && !this.m_uiMiscContract.ProcessRequired)
			{
				base.gameObject.SetActive(false);
				if (onClose != null)
				{
					onClose();
				}
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objUserBuff, NKCScenManager.CurrentUserData().m_companyBuffDataList.Count > 0);
			this.HideCommonObjects();
			this.SetOperationMultiply(0, false);
			this.SetContractPoint(0);
			NKCUtil.SetLabelText(this.m_lbGetTitle, Title);
			NKCUtil.SetGameobjectActive(this.m_objTitle, !string.IsNullOrEmpty(this.m_lbGetTitle.text));
			base.UIOpened(true);
			this.m_LastCoroutine = base.StartCoroutine(this.ProcessResultUI(false));
		}

		// Token: 0x06008952 RID: 35154 RVA: 0x002E9304 File Offset: 0x002E7504
		public void OpenRewardRearmExtract(NKMRewardData rewardData, NKMRewardData synergyRewardData, string Title, string subTitle = "", NKCUIResult.OnClose onClose = null)
		{
			this.dOnClose = onClose;
			this.UnHide();
			this.CloseAllSubUI();
			NKCUtil.SetGameobjectActive(this.m_objShareBtn, false);
			NKCUtil.SetGameobjectActive(this.m_objShare, false);
			this.SelectTitle(NKCUIResult.eTitleType.Get);
			base.gameObject.SetActive(true);
			NKCUIResultSubUIFierceBattle ui_FierceBattle = this.m_ui_FierceBattle;
			if (ui_FierceBattle != null)
			{
				ui_FierceBattle.SetData(null, false);
			}
			this.m_ui_RearmExtract.SetData(rewardData, synergyRewardData, false);
			this.m_uiSubUIMiddle.SetDataNull();
			this.SetUnitGetOpenData(rewardData, NKCScenManager.CurrentUserData().m_ArmyData, true);
			this.m_uiReward.SetData(null);
			this.m_uiKillCount.SetData(null, false);
			this.m_uiMiscContract.SetData((rewardData != null) ? rewardData.ContractList : null);
			this.m_uiTip.SetData(BATTLE_RESULT_TYPE.BRT_WIN, false);
			this.m_uiWorldmap.SetData(false, null);
			this.m_uiShadowTime.SetData(BATTLE_RESULT_TYPE.BRT_WIN, 0, 0, false);
			this.m_uiShadowLife.SetData(BATTLE_RESULT_TYPE.BRT_WIN, 0, 0, false);
			if (!this.m_ui_RearmExtract.ProcessRequired && !this.m_uiMiscContract.ProcessRequired)
			{
				base.gameObject.SetActive(false);
				if (onClose != null)
				{
					onClose();
				}
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objUserBuff, false);
			this.HideCommonObjects();
			this.SetOperationMultiply(0, false);
			this.SetContractPoint(0);
			NKCUtil.SetLabelText(this.m_lbGetTitle, Title);
			NKCUtil.SetGameobjectActive(this.m_objTitle, !string.IsNullOrEmpty(this.m_lbGetTitle.text));
			base.UIOpened(true);
			this.m_LastCoroutine = base.StartCoroutine(this.ProcessResultUI(false));
		}

		// Token: 0x06008953 RID: 35155 RVA: 0x002E9490 File Offset: 0x002E7690
		public void OpenRewardGainWithUnitSD(NKMArmyData armyData, NKMRewardData rewardData, NKMAdditionalReward additionalReward, int unitID, string Title, string subTitle = "", NKCUIResult.OnClose onClose = null)
		{
			this.dOnClose = onClose;
			this.UnHide();
			this.CloseAllSubUI();
			NKCUtil.SetGameobjectActive(this.m_objShareBtn, false);
			NKCUtil.SetGameobjectActive(this.m_objShare, false);
			this.SelectTitle(NKCUIResult.eTitleType.Get);
			base.gameObject.SetActive(true);
			this.m_ui_FierceBattle.SetData(null, false);
			NKCUIResultSubUIRearmament ui_RearmExtract = this.m_ui_RearmExtract;
			if (ui_RearmExtract != null)
			{
				ui_RearmExtract.SetData(null, null, false);
			}
			this.m_uiSubUIMiddle.SetDataNull();
			this.SetUnitGetOpenData(rewardData, armyData, true);
			NKCUIResultSubUIReward.Data data = new NKCUIResultSubUIReward.Data();
			data.rewardData = rewardData;
			data.armyData = armyData;
			data.additionalReward = additionalReward;
			this.m_uiReward.SetData(data);
			this.m_uiKillCount.SetData(null, false);
			this.m_uiMiscContract.SetData((rewardData != null) ? rewardData.ContractList : null);
			this.m_uiTip.SetData(BATTLE_RESULT_TYPE.BRT_WIN, false);
			this.m_uiWorldmap.SetData(true, unitID);
			this.m_uiShadowTime.SetData(BATTLE_RESULT_TYPE.BRT_WIN, 0, 0, false);
			this.m_uiShadowLife.SetData(BATTLE_RESULT_TYPE.BRT_WIN, 0, 0, false);
			if (!this.m_uiReward.ProcessRequired && !this.m_uiMiscContract.ProcessRequired)
			{
				base.gameObject.SetActive(false);
				if (onClose != null)
				{
					onClose();
				}
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objUserBuff, NKCScenManager.CurrentUserData().m_companyBuffDataList.Count > 0);
			this.HideCommonObjects();
			this.SetOperationMultiply(0, false);
			this.SetContractPoint(0);
			NKCUtil.SetLabelText(this.m_lbGetTitle, Title);
			NKCUtil.SetGameobjectActive(this.m_objTitle, !string.IsNullOrEmpty(this.m_lbGetTitle.text));
			base.UIOpened(true);
			this.m_LastCoroutine = base.StartCoroutine(this.ProcessResultUI(false));
		}

		// Token: 0x06008954 RID: 35156 RVA: 0x002E9640 File Offset: 0x002E7840
		public static List<NKCUIResultSubUIUnitExp.UnitLevelupUIData> MakeUnitLevelupExpData(NKMArmyData armyData, IReadOnlyList<NKMRewardUnitExpData> lstRewardUnitExpData, NKMDeckIndex deckIndex, List<UnitLoyaltyUpdateData> lstUnitUpdateData = null)
		{
			if (deckIndex.m_eDeckType == NKM_DECK_TYPE.NDT_TRIM)
			{
				return new List<NKCUIResultSubUIUnitExp.UnitLevelupUIData>();
			}
			List<NKCUIResultSubUIUnitExp.UnitLevelupUIData> list = new List<NKCUIResultSubUIUnitExp.UnitLevelupUIData>();
			Dictionary<long, UnitLoyaltyUpdateData> dictionary = new Dictionary<long, UnitLoyaltyUpdateData>();
			if (lstUnitUpdateData != null)
			{
				foreach (UnitLoyaltyUpdateData unitLoyaltyUpdateData in lstUnitUpdateData)
				{
					dictionary.Add(unitLoyaltyUpdateData.unitUid, unitLoyaltyUpdateData);
				}
			}
			NKMDeckData deckData = armyData.GetDeckData(deckIndex);
			if (deckData != null)
			{
				using (List<long>.Enumerator enumerator2 = deckData.m_listDeckUnitUID.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						long UID = enumerator2.Current;
						NKMUnitData unitFromUID = armyData.GetUnitFromUID(UID);
						if (unitFromUID != null)
						{
							NKMRewardUnitExpData nkmrewardUnitExpData = null;
							if (lstRewardUnitExpData != null)
							{
								nkmrewardUnitExpData = lstRewardUnitExpData.FirstOrDefault((NKMRewardUnitExpData x) => x.m_UnitUid == UID);
							}
							if (nkmrewardUnitExpData == null)
							{
								nkmrewardUnitExpData = new NKMRewardUnitExpData
								{
									m_UnitUid = UID,
									m_Exp = 0,
									m_BonusExp = 0
								};
							}
							int newLoyalty = -1;
							UnitLoyaltyUpdateData unitLoyaltyUpdateData2;
							if (dictionary.TryGetValue(UID, out unitLoyaltyUpdateData2))
							{
								newLoyalty = unitLoyaltyUpdateData2.loyalty;
							}
							NKCUIResultSubUIUnitExp.UnitLevelupUIData item = NKCUIResult.MakeLevelupData(unitFromUID, nkmrewardUnitExpData, newLoyalty);
							list.Add(item);
						}
					}
					return list;
				}
			}
			if (lstRewardUnitExpData != null)
			{
				using (IEnumerator<NKMRewardUnitExpData> enumerator3 = lstRewardUnitExpData.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						NKMRewardUnitExpData nkmrewardUnitExpData2 = enumerator3.Current;
						NKMUnitData unitFromUID2 = armyData.GetUnitFromUID(nkmrewardUnitExpData2.m_UnitUid);
						if (unitFromUID2 != null)
						{
							int newLoyalty2 = -1;
							UnitLoyaltyUpdateData unitLoyaltyUpdateData3;
							if (dictionary.TryGetValue(unitFromUID2.m_UnitUID, out unitLoyaltyUpdateData3))
							{
								newLoyalty2 = unitLoyaltyUpdateData3.loyalty;
							}
							NKCUIResultSubUIUnitExp.UnitLevelupUIData item2 = NKCUIResult.MakeLevelupData(unitFromUID2, nkmrewardUnitExpData2, newLoyalty2);
							list.Add(item2);
						}
					}
					return list;
				}
			}
			foreach (KeyValuePair<long, UnitLoyaltyUpdateData> keyValuePair in dictionary)
			{
				NKMUnitData unitFromUID3 = armyData.GetUnitFromUID(keyValuePair.Key);
				if (unitFromUID3 != null)
				{
					int loyalty = keyValuePair.Value.loyalty;
					NKMRewardUnitExpData unitExpData = new NKMRewardUnitExpData
					{
						m_UnitUid = keyValuePair.Key,
						m_Exp = 0,
						m_BonusExp = 0
					};
					NKCUIResultSubUIUnitExp.UnitLevelupUIData item3 = NKCUIResult.MakeLevelupData(unitFromUID3, unitExpData, loyalty);
					list.Add(item3);
				}
			}
			return list;
		}

		// Token: 0x06008955 RID: 35157 RVA: 0x002E98B0 File Offset: 0x002E7AB0
		private static NKCUIResultSubUIUnitExp.UnitLevelupUIData MakeLevelupData(NKMUnitData unitData, NKMRewardUnitExpData unitExpData, int newLoyalty = -1)
		{
			NKCUIResultSubUIUnitExp.UnitLevelupUIData unitLevelupUIData = new NKCUIResultSubUIUnitExp.UnitLevelupUIData();
			unitLevelupUIData.m_bIsLeader = false;
			unitLevelupUIData.m_UnitData = unitData;
			unitLevelupUIData.m_iExpOld = unitData.m_iUnitLevelEXP;
			unitLevelupUIData.m_iLevelOld = unitData.m_UnitLevel;
			NKCExpManager.CalculateFutureUnitExpAndLevel(unitData, unitExpData.m_Exp + unitExpData.m_BonusExp, out unitLevelupUIData.m_iLevelNew, out unitLevelupUIData.m_iExpNew);
			unitLevelupUIData.m_iTotalExpGain = unitExpData.m_Exp + unitExpData.m_BonusExp;
			NKCUIResultSubUIUnitExp.UNIT_LOYALTY loyalty = NKCUIResultSubUIUnitExp.UNIT_LOYALTY.None;
			if (newLoyalty >= 0)
			{
				if (unitData.loyalty < newLoyalty)
				{
					loyalty = NKCUIResultSubUIUnitExp.UNIT_LOYALTY.Up;
				}
				else if (unitData.loyalty > newLoyalty)
				{
					loyalty = NKCUIResultSubUIUnitExp.UNIT_LOYALTY.Down;
				}
			}
			unitLevelupUIData.m_loyalty = loyalty;
			return unitLevelupUIData;
		}

		// Token: 0x06008956 RID: 35158 RVA: 0x002E9941 File Offset: 0x002E7B41
		private void GetUnitProcessEnd()
		{
			this.m_bWaitForUnitGain = false;
		}

		// Token: 0x06008957 RID: 35159 RVA: 0x002E994A File Offset: 0x002E7B4A
		private IEnumerator ProcessResultUI(bool bAutoSkip = false)
		{
			if (NKCScenManager.GetScenManager().GetNKCPowerSaveMode().GetEnable() && NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing())
			{
				base.Close();
				yield break;
			}
			this.m_bPause = false;
			this.m_bUserLevelUpPopupOpened = false;
			this.OpenTitleAndBackground();
			yield return this.WaitTimeOrUserInput(this.UI_TITLE_ANI_END_DELAY);
			NKCUtil.SetGameobjectActive(this.m_objBottomButton, true);
			if (this.m_uiMiscContract.ProcessRequired)
			{
				while (this.m_uiMiscContract.WillProcess())
				{
					yield return this.ProcessSubUI(this.m_uiMiscContract, bAutoSkip);
					while (this.m_bPause)
					{
						yield return null;
					}
					while (!this.m_uiMiscContract.IsProcessFinished())
					{
						yield return null;
					}
					this.m_uiMiscContract.CleanUpData();
				}
				this.m_uiMiscContract.Close();
			}
			List<NKCUIResultSubUIBase> list = new List<NKCUIResultSubUIBase>
			{
				this.m_uiWorldmap,
				this.m_uiSubUIMiddle
			};
			foreach (NKCUIResultSubUIBase subUI in list)
			{
				NKCUIResultSubUIBase subUI;
				if (subUI.ProcessRequired)
				{
					yield return this.ProcessSubUI(subUI, bAutoSkip);
					while (this.m_bPause)
					{
						yield return null;
					}
					while (!subUI.IsProcessFinished())
					{
						yield return null;
					}
					subUI.Close();
				}
				subUI = null;
			}
			List<NKCUIResultSubUIBase>.Enumerator enumerator = default(List<NKCUIResultSubUIBase>.Enumerator);
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_OPERATION && NKCContentManager.CheckLevelChanged() && !this.m_bUserLevelUpPopupOpened)
			{
				NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
				NKCPopupUserLevelUp.instance.Open(myUserData, new Action(this.OnCloseUserLevelUpPopup));
				this.m_bUserLevelUpPopupOpened = true;
				while (this.m_bUserLevelUpPopupOpened)
				{
					yield return null;
				}
			}
			if (this.m_lstUnitGainRewardData != null && this.m_lstUnitGainRewardData.Count > 0)
			{
				this.m_bWaitForUnitGain = true;
				yield return null;
				if (NKCUIGameResultGetUnit.Instance == null)
				{
					Log.Error("NKCUIResult - NKCUIGameResultGetUnit.Instance is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUIResult.cs", 2752);
					yield break;
				}
				NKCUIGameResultGetUnit.ShowNewUnitGetUI(this.m_lstUnitGainRewardData, new NKCUIGameResultGetUnit.NKCUIGRGetUnitCallBack(this.GetUnitProcessEnd), bAutoSkip, this.m_bDefaultSort, this.m_bSkipDuplicateUnitGain);
			}
			while (this.m_bWaitForUnitGain)
			{
				yield return null;
			}
			List<NKCUIResultSubUIBase> list2 = new List<NKCUIResultSubUIBase>();
			list2.Add(this.m_uiReward);
			list2.Add(this.m_uiTip);
			list2.Add(this.m_uiShadowTime);
			list2.Add(this.m_uiShadowLife);
			list2.Add(this.m_uiTrim);
			list2.Add(this.m_ui_FierceBattle);
			list2.Add(this.m_uiKillCount);
			list2.Add(this.m_ui_RearmExtract);
			List<NKCUIResultSubUIBase> lstProcessedSet = new List<NKCUIResultSubUIBase>();
			foreach (NKCUIResultSubUIBase nkcuiresultSubUIBase in list2)
			{
				if (nkcuiresultSubUIBase.ProcessRequired)
				{
					lstProcessedSet.Add(nkcuiresultSubUIBase);
				}
			}
			int num;
			for (int i = 0; i < lstProcessedSet.Count; i = num + 1)
			{
				bool bCountdownRequired = bAutoSkip && i == lstProcessedSet.Count - 1;
				NKCUIResultSubUIBase subUI = lstProcessedSet[i];
				subUI.SetReserveCountdown(bCountdownRequired);
				yield return this.ProcessSubUI(subUI, bAutoSkip);
				while (this.m_bPause)
				{
					yield return null;
				}
				while (!subUI.IsProcessFinished())
				{
					yield return null;
				}
				if (bCountdownRequired)
				{
					yield return this.ProcessCountDown();
				}
				subUI.Close();
				subUI = null;
				num = i;
			}
			bool flag = false;
			using (IEnumerator<NKCUIResultSubUIBase> enumerator3 = this.GetSubUIEnumerator().GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					if (enumerator3.Current.ProcessRequired)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				while (this.m_bPause)
				{
					yield return null;
				}
				if (!bAutoSkip)
				{
					yield return this.WaitTimeOrUserInput(5f);
				}
			}
			yield return new WaitForSeconds(this.NoSkipSecond);
			while (NKCPopupSnsShareMenu.Instance.IsOpen)
			{
				yield return new WaitForSeconds(3f);
			}
			yield return this.CloseTitleAndBackground();
			base.Close();
			yield break;
			yield break;
		}

		// Token: 0x06008958 RID: 35160 RVA: 0x002E9960 File Offset: 0x002E7B60
		private IEnumerator ProcessCountDown()
		{
			float m_fWaitTimeForCloseAnimation = 1f;
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME_RESULT && NKCRepeatOperaion.CheckVisible(NKCScenManager.GetScenManager().Get_NKC_SCEN_GAME_RESULT().GetStageID()) && NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing())
			{
				m_fWaitTimeForCloseAnimation = 5f;
				NKCUtil.SetGameobjectActive(this.m_objRepeatOperationCountDown, true);
				NKCUtil.SetLabelText(this.m_lbRepeatOperationCountDown, Mathf.CeilToInt(m_fWaitTimeForCloseAnimation).ToString());
				this.m_imgRepeatOperationCountDown.fillAmount = 0f;
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objRepeatOperationCountDown, false);
			}
			float currentTime = 0f;
			this.m_bProcessingCountDown = true;
			while (m_fWaitTimeForCloseAnimation > currentTime)
			{
				if (this.m_bHadUserInput)
				{
					currentTime += 1f;
					this.m_bHadUserInput = false;
				}
				if (!this.m_bPause)
				{
					currentTime += Time.deltaTime;
				}
				float num = m_fWaitTimeForCloseAnimation - currentTime;
				if (this.m_objRepeatOperationCountDown.activeSelf)
				{
					int num2 = Mathf.CeilToInt(num);
					NKCUtil.SetLabelText(this.m_lbRepeatOperationCountDown, num2.ToString());
					this.m_imgRepeatOperationCountDown.fillAmount = (float)num2 - num;
				}
				yield return null;
			}
			this.m_bProcessingCountDown = false;
			if (this.m_amtorRepeatOperation.gameObject.activeSelf)
			{
				this.m_amtorRepeatOperation.Play("NKM_UI_RESULT_REPEAT_OUTRO");
			}
			yield break;
		}

		// Token: 0x06008959 RID: 35161 RVA: 0x002E9970 File Offset: 0x002E7B70
		private void OpenTitleAndBackground()
		{
			NKCUtil.SetGameobjectActive(this.m_aniTitleWin.gameObject, false);
			NKCUtil.SetGameobjectActive(this.m_aniTitleLose.gameObject, false);
			NKCUtil.SetGameobjectActive(this.m_aniTitleGet.gameObject, false);
			NKCUtil.SetGameobjectActive(this.m_aniTitleWin.gameObject, this.m_eCurrentTitleType == NKCUIResult.eTitleType.Win);
			NKCUtil.SetGameobjectActive(this.m_aniTitleLose.gameObject, this.m_eCurrentTitleType == NKCUIResult.eTitleType.Lose);
			NKCUtil.SetGameobjectActive(this.m_aniTitleGet.gameObject, this.m_eCurrentTitleType == NKCUIResult.eTitleType.Get);
			if (this.m_aniTitleReplay != null)
			{
				NKCUtil.SetGameobjectActive(this.m_aniTitleReplay.gameObject, this.m_eCurrentTitleType == NKCUIResult.eTitleType.Replay);
			}
			if (this.m_aniTitleWinPrivate != null)
			{
				NKCUtil.SetGameobjectActive(this.m_aniTitleWinPrivate.gameObject, this.m_eCurrentTitleType == NKCUIResult.eTitleType.WinPrivate);
			}
			if (this.m_aniTitleLosePrivate != null)
			{
				NKCUtil.SetGameobjectActive(this.m_aniTitleLosePrivate.gameObject, this.m_eCurrentTitleType == NKCUIResult.eTitleType.LosePrivate);
			}
			if (this.m_aniTitleDrawPrivate != null)
			{
				NKCUtil.SetGameobjectActive(this.m_aniTitleDrawPrivate.gameObject, this.m_eCurrentTitleType == NKCUIResult.eTitleType.DrawPrivate);
			}
		}

		// Token: 0x0600895A RID: 35162 RVA: 0x002E9A97 File Offset: 0x002E7C97
		private IEnumerator CloseTitleAndBackground()
		{
			NKCUtil.SetGameobjectActive(this.m_objBottomButton, false);
			NKCUtil.SetGameobjectActive(this.m_btnBattleStatistics, false);
			NKCUtil.SetGameobjectActive(this.m_btnReplayBattleStatistics, false);
			NKCUtil.SetGameobjectActive(this.m_RESULT_WIN_Bonus_Type, false);
			NKCUtil.SetGameobjectActive(this.m_csbtnTrimRetry, false);
			if (null != this.m_uiCurrentPlayingTitleAni)
			{
				this.m_uiCurrentPlayingTitleAni.Play("OUTRO");
			}
			yield return null;
			yield return this.WaitTimeOrUserInput(this.m_uiCurrentPlayingTitleAni.GetCurrentAnimatorStateInfo(0).length / this.m_uiCurrentPlayingTitleAni.GetCurrentAnimatorStateInfo(0).speedMultiplier);
			yield break;
		}

		// Token: 0x0600895B RID: 35163 RVA: 0x002E9AA6 File Offset: 0x002E7CA6
		private IEnumerator ProcessSubUI(NKCUIResultSubUIBase subUI, bool bAutoSkip)
		{
			if (subUI == null)
			{
				yield break;
			}
			this.m_bHadUserInput = false;
			NKCUtil.SetGameobjectActive(subUI.gameObject, true);
			yield return subUI.Process(bAutoSkip);
			yield return null;
			yield break;
		}

		// Token: 0x0600895C RID: 35164 RVA: 0x002E9AC3 File Offset: 0x002E7CC3
		private IEnumerator WaitTimeOrUserInput(float waitTime = 5f)
		{
			float currentTime = 0f;
			this.m_bHadUserInput = false;
			if (waitTime == 0f)
			{
				yield break;
			}
			if (waitTime < 0f)
			{
				while (!this.m_bHadUserInput)
				{
					yield return null;
				}
			}
			else
			{
				while (currentTime < waitTime)
				{
					currentTime += Time.deltaTime;
					if (this.m_bHadUserInput)
					{
						break;
					}
					yield return null;
				}
			}
			yield break;
		}

		// Token: 0x0600895D RID: 35165 RVA: 0x002E9AD9 File Offset: 0x002E7CD9
		public override void OnBackButton()
		{
			this.OnClickContinue();
		}

		// Token: 0x0600895E RID: 35166 RVA: 0x002E9AE4 File Offset: 0x002E7CE4
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
			if (this.m_LastCoroutine != null)
			{
				base.StopCoroutine(this.m_LastCoroutine);
				this.m_LastCoroutine = null;
			}
			NKCUIGameResultGetUnit.CheckInstanceAndClose();
			NKCUIBattleStatistics.CheckInstanceAndClose();
			this.dOnTouchGameRecord = null;
			this.m_lstUnitGainRewardData = null;
			if (this.m_crtShare != null)
			{
				base.StopCoroutine(this.m_crtShare);
				this.m_crtShare = null;
			}
			if (this.dOnClose != null)
			{
				this.dOnClose();
			}
		}

		// Token: 0x0600895F RID: 35167 RVA: 0x002E9B5E File Offset: 0x002E7D5E
		public void OnCloseUserLevelUpPopup()
		{
			this.m_bUserLevelUpPopupOpened = false;
			NKCContentManager.SetLevelChanged(false);
		}

		// Token: 0x06008960 RID: 35168 RVA: 0x002E9B70 File Offset: 0x002E7D70
		public void SetPause(bool bSet)
		{
			this.m_bPause = bSet;
			foreach (NKCUIResultSubUIBase nkcuiresultSubUIBase in this.GetSubUIEnumerator())
			{
				nkcuiresultSubUIBase.SetPause(bSet);
			}
		}

		// Token: 0x06008961 RID: 35169 RVA: 0x002E9BC4 File Offset: 0x002E7DC4
		private void SetUnitGetOpenData(NKMRewardData rewardData, NKMArmyData armyData, bool bSkipDuplicateUnitGain = true)
		{
			this.SetUnitGetOpenData(new List<NKMRewardData>
			{
				rewardData
			}, armyData, bSkipDuplicateUnitGain, true);
		}

		// Token: 0x06008962 RID: 35170 RVA: 0x002E9BE8 File Offset: 0x002E7DE8
		private void SetUnitGetOpenData(List<NKMRewardData> lstRewardData, NKMArmyData armyData, bool bSkipDuplicateUnitGain = true, bool bDefaultSort = true)
		{
			this.m_lstUnitGainRewardData = lstRewardData;
			this.m_bSkipDuplicateUnitGain = bSkipDuplicateUnitGain;
			this.m_bDefaultSort = bDefaultSort;
			if (armyData == null)
			{
				return;
			}
			foreach (NKMRewardData nkmrewardData in lstRewardData)
			{
				if (nkmrewardData != null)
				{
					if (nkmrewardData.UnitDataList != null && nkmrewardData.UnitDataList.Count > 0)
					{
						foreach (NKMUnitData nkmunitData in nkmrewardData.UnitDataList)
						{
							if (armyData.IsFirstGetUnit(nkmunitData.m_UnitID))
							{
								NKCUIGameResultGetUnit.AddFirstGetUnit(nkmunitData.m_UnitID);
							}
						}
					}
					if (nkmrewardData.OperatorList != null && nkmrewardData.OperatorList.Count > 0)
					{
						foreach (NKMOperator nkmoperator in nkmrewardData.OperatorList)
						{
							if (armyData.IsFirstGetUnit(nkmoperator.id))
							{
								NKCUIGameResultGetUnit.AddFirstGetUnit(nkmoperator.id);
							}
						}
					}
				}
			}
		}

		// Token: 0x06008963 RID: 35171 RVA: 0x002E9D30 File Offset: 0x002E7F30
		private void SetOperationMultiply(int multiply, bool bShow = false)
		{
			if (bShow)
			{
				NKCUtil.SetGameobjectActive(this.m_objOperationMultiply, multiply > 1);
				NKCUtil.SetLabelText(this.m_txtOperationMultiply, NKCUtilString.GET_MULTIPLY_REWARD_RESULT_ONE_PARAM, new object[]
				{
					multiply
				});
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objOperationMultiply, false);
		}

		// Token: 0x06008964 RID: 35172 RVA: 0x002E9D70 File Offset: 0x002E7F70
		private void SetContractPoint(int iCnt = 0)
		{
			if (iCnt == 0)
			{
				NKCUtil.SetGameobjectActive(this.m_objContractPoint, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objContractPoint, true);
			NKCUtil.SetLabelText(this.m_lbContractPoint, string.Format(NKCUtilString.GET_STRING_CONTRACT_POINT_DESC_01, iCnt));
		}

		// Token: 0x06008965 RID: 35173 RVA: 0x002E9DAC File Offset: 0x002E7FAC
		private void SetTrimRetryButton(NKM_GAME_TYPE gameType)
		{
			bool flag = false;
			if (gameType == NKM_GAME_TYPE.NGT_TRIM)
			{
				NKMTrimIntervalTemplet nkmtrimIntervalTemplet = NKMTrimIntervalTemplet.IntervalList.FirstOrDefault(delegate(NKMTrimIntervalTemplet e)
				{
					NKMIntervalTemplet nkmintervalTemplet = NKMIntervalTemplet.Find(e.DateStrId);
					return nkmintervalTemplet != null && nkmintervalTemplet.IsValidTime(NKCSynchronizedTime.ServiceTime);
				});
				flag = (nkmtrimIntervalTemplet != null && !nkmtrimIntervalTemplet.IsResetUnLimit);
			}
			NKCUtil.SetGameobjectActive(this.m_csbtnTrimRetry, gameType == NKM_GAME_TYPE.NGT_TRIM && flag);
			if (this.m_csbtnTrimRetry != null)
			{
				this.m_csbtnTrimRetry.UnLock(false);
			}
		}

		// Token: 0x06008966 RID: 35174 RVA: 0x002E9E28 File Offset: 0x002E8028
		public override bool OnHotkey(HotkeyEventType hotkey)
		{
			if (hotkey != HotkeyEventType.Confirm)
			{
				if (hotkey == HotkeyEventType.ShowHotkey)
				{
					if (this.m_btnContinue != null)
					{
						NKCUIComHotkeyDisplay.OpenInstance(this.m_btnContinue.transform, new HotkeyEventType[]
						{
							HotkeyEventType.Confirm,
							HotkeyEventType.Skip
						});
					}
				}
				return false;
			}
			this.OnClickContinue();
			return true;
		}

		// Token: 0x06008967 RID: 35175 RVA: 0x002E9E78 File Offset: 0x002E8078
		private void Update()
		{
			if (Input.touchCount > 0 || Input.GetMouseButton(0))
			{
				this.m_fCurrentHoldTime += Time.unscaledDeltaTime;
				if (this.m_fCurrentHoldTime > 0.3f)
				{
					this.OnClickContinue();
				}
			}
			else
			{
				this.m_fCurrentHoldTime = 0f;
			}
			if (this.m_bProcessingCountDown && Input.anyKeyDown)
			{
				this.m_bHadUserInput = true;
			}
		}

		// Token: 0x06008968 RID: 35176 RVA: 0x002E9EDD File Offset: 0x002E80DD
		public override void OnHotkeyHold(HotkeyEventType hotkey)
		{
			if (hotkey == HotkeyEventType.Skip)
			{
				this.OnClickContinue();
			}
		}

		// Token: 0x06008969 RID: 35177 RVA: 0x002E9EEC File Offset: 0x002E80EC
		private void OnTrimRetry()
		{
			this.SetPause(true);
			if (NKCScenManager.CurrentUserData().TrimData.TrimIntervalData.trimRetryCount > 0)
			{
				string @string = NKCStringTable.GetString("SI_PF_TRIM_DUNGEON_RESULT_RESET_COUNT_TEXT", new object[]
				{
					NKCScenManager.CurrentUserData().TrimData.TrimIntervalData.trimRetryCount
				});
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, @string, new NKCPopupOKCancel.OnButton(this.OnTrimRetryConfirm), new NKCPopupOKCancel.OnButton(this.OnTrimRetryCancel), false);
				return;
			}
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString("SI_PF_TRIM_DUNGEON_RESULT_RESET_NO_COUNT", false), new NKCPopupOKCancel.OnButton(this.OnTrimRetryCancel), "");
		}

		// Token: 0x0600896A RID: 35178 RVA: 0x002E9F8F File Offset: 0x002E818F
		private void OnTrimRetryConfirm()
		{
			if (NKMTrimIntervalTemplet.Find(NKCSynchronizedTime.ServiceTime) == null)
			{
				this.SetPause(false);
				return;
			}
			this.m_csbtnTrimRetry.Lock(false);
			NKCPacketSender.Send_NKMPacket_TRIM_RETRY_REQ();
		}

		// Token: 0x0600896B RID: 35179 RVA: 0x002E9FB6 File Offset: 0x002E81B6
		public void OnTrimRetryAck()
		{
			this.SetPause(false);
		}

		// Token: 0x0600896C RID: 35180 RVA: 0x002E9FBF File Offset: 0x002E81BF
		private void OnTrimRetryCancel()
		{
			this.SetPause(false);
		}

		// Token: 0x04007593 RID: 30099
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_result";

		// Token: 0x04007594 RID: 30100
		private const string UI_ASSET_NAME = "NKM_UI_RESULT_COMMON";

		// Token: 0x04007595 RID: 30101
		private static NKCUIResult m_Instance;

		// Token: 0x04007596 RID: 30102
		private RectTransform m_rtRoot;

		// Token: 0x04007597 RID: 30103
		[Header("Basic Information")]
		private Animator m_aniTitleWin;

		// Token: 0x04007598 RID: 30104
		private Animator m_aniTitleLose;

		// Token: 0x04007599 RID: 30105
		private Animator m_aniTitleGet;

		// Token: 0x0400759A RID: 30106
		private Animator m_aniTitleReplay;

		// Token: 0x0400759B RID: 30107
		private Animator m_aniTitleWinPrivate;

		// Token: 0x0400759C RID: 30108
		private Animator m_aniTitleLosePrivate;

		// Token: 0x0400759D RID: 30109
		private Animator m_aniTitleDrawPrivate;

		// Token: 0x0400759E RID: 30110
		public GameObject m_objTitle;

		// Token: 0x0400759F RID: 30111
		private Text m_lbGetTitle;

		// Token: 0x040075A0 RID: 30112
		private GameObject m_objBottomButton;

		// Token: 0x040075A1 RID: 30113
		[Header("SubPages")]
		public NKCUIResultSubUIMiddle m_uiSubUIMiddle;

		// Token: 0x040075A2 RID: 30114
		public NKCUIResultSubUIReward m_uiReward;

		// Token: 0x040075A3 RID: 30115
		public NKCUIResultSubUITip m_uiTip;

		// Token: 0x040075A4 RID: 30116
		public NKCUIResultSubUIWorldmap m_uiWorldmap;

		// Token: 0x040075A5 RID: 30117
		public NKCUIResultSubUIShadowTime m_uiShadowTime;

		// Token: 0x040075A6 RID: 30118
		public NKCUIResultSubUIShadowLife m_uiShadowLife;

		// Token: 0x040075A7 RID: 30119
		public NKCUIResultSubUIFierceBattle m_ui_FierceBattle;

		// Token: 0x040075A8 RID: 30120
		public NKCUIResultSubUIRearmament m_ui_RearmExtract;

		// Token: 0x040075A9 RID: 30121
		public NKCUIResultSubUIKillCount m_uiKillCount;

		// Token: 0x040075AA RID: 30122
		public NKCUIResultMiscContract m_uiMiscContract;

		// Token: 0x040075AB RID: 30123
		public NKCUIResultSubUITrim m_uiTrim;

		// Token: 0x040075AC RID: 30124
		[Header("더블토큰")]
		public Animator m_amtorDoubleToken;

		// Token: 0x040075AD RID: 30125
		public GameObject m_objDoubleToken;

		// Token: 0x040075AE RID: 30126
		public Text m_lbDoubleTokenCount;

		// Token: 0x040075AF RID: 30127
		[Header("반복작전")]
		public GameObject m_objRepeatOperation;

		// Token: 0x040075B0 RID: 30128
		public Animator m_amtorRepeatOperation;

		// Token: 0x040075B1 RID: 30129
		public Text m_lbRepeatOperation;

		// Token: 0x040075B2 RID: 30130
		public NKCUIComStateButton m_csbtnRepeatOperation;

		// Token: 0x040075B3 RID: 30131
		public GameObject m_objRepeatOperationCountDown;

		// Token: 0x040075B4 RID: 30132
		public Text m_lbRepeatOperationCountDown;

		// Token: 0x040075B5 RID: 30133
		public Image m_imgRepeatOperationCountDown;

		// Token: 0x040075B6 RID: 30134
		[Header("Etc")]
		public GameObject m_objUserBuff;

		// Token: 0x040075B7 RID: 30135
		public GameObject m_objOperationMultiply;

		// Token: 0x040075B8 RID: 30136
		public Text m_txtOperationMultiply;

		// Token: 0x040075B9 RID: 30137
		public GameObject m_objContractPoint;

		// Token: 0x040075BA RID: 30138
		public Text m_lbContractPoint;

		// Token: 0x040075BB RID: 30139
		public GameObject m_WIN;

		// Token: 0x040075BC RID: 30140
		public GameObject m_LOSE;

		// Token: 0x040075BD RID: 30141
		public GameObject m_WIN_BATTLE_REPORT;

		// Token: 0x040075BE RID: 30142
		public GameObject m_LOSE_BATTLE_REPORT;

		// Token: 0x040075BF RID: 30143
		public GameObject m_ASSIST;

		// Token: 0x040075C0 RID: 30144
		[Header("Share")]
		public GameObject m_objShareBtn;

		// Token: 0x040075C1 RID: 30145
		public NKCUIComStateButton m_csbtnShare;

		// Token: 0x040075C2 RID: 30146
		public GameObject m_objShare;

		// Token: 0x040075C3 RID: 30147
		public Text m_lbLevel;

		// Token: 0x040075C4 RID: 30148
		public Text m_lbUserName;

		// Token: 0x040075C5 RID: 30149
		public Text m_lbUserUID;

		// Token: 0x040075C6 RID: 30150
		private Coroutine m_crtShare;

		// Token: 0x040075C7 RID: 30151
		private RectTransform m_rtTitleRoot;

		// Token: 0x040075C8 RID: 30152
		private RectTransform m_rtBackgroundOpen;

		// Token: 0x040075C9 RID: 30153
		private NKCUIComStateButton m_btnContinue;

		// Token: 0x040075CA RID: 30154
		private NKCUIComStateButton m_btnBattleStatistics;

		// Token: 0x040075CB RID: 30155
		private NKCUIComStateButton m_btnReplayBattleStatistics;

		// Token: 0x040075CC RID: 30156
		[Header("Trim Retry")]
		public NKCUIComStateButton m_csbtnTrimRetry;

		// Token: 0x040075CD RID: 30157
		private EventTrigger m_eventTrigger;

		// Token: 0x040075CE RID: 30158
		private NKCUIResult.OnClose dOnClose;

		// Token: 0x040075CF RID: 30159
		private NKCUIResult.OnTouchGameRecord dOnTouchGameRecord;

		// Token: 0x040075D0 RID: 30160
		public float UI_TITLE_ANI_END_DELAY = 0.5f;

		// Token: 0x040075D1 RID: 30161
		public float NoSkipSecond = 0.1f;

		// Token: 0x040075D2 RID: 30162
		protected bool m_bHadUserInput;

		// Token: 0x040075D3 RID: 30163
		private Animator m_uiCurrentPlayingTitleAni;

		// Token: 0x040075D4 RID: 30164
		private NKCUIResult.eTitleType m_eCurrentTitleType;

		// Token: 0x040075D5 RID: 30165
		private Coroutine m_LastCoroutine;

		// Token: 0x040075D6 RID: 30166
		private bool m_bPause;

		// Token: 0x040075D7 RID: 30167
		private bool m_bUserLevelUpPopupOpened;

		// Token: 0x040075D8 RID: 30168
		private List<NKMRewardData> m_lstUnitGainRewardData;

		// Token: 0x040075D9 RID: 30169
		public GameObject m_RESULT_WIN_Bonus_Type;

		// Token: 0x040075DA RID: 30170
		public Image m_RESULT_WIN_Bonus_Type_icon;

		// Token: 0x040075DB RID: 30171
		private const string CAPTURE_FILE_NAME = "ScreenCapture.png";

		// Token: 0x040075DC RID: 30172
		private const string THUMBNAIL_FILE_NAME = "Thumbnail.png";

		// Token: 0x040075DD RID: 30173
		private bool m_bWaitForUnitGain;

		// Token: 0x040075DE RID: 30174
		private bool m_bSkipDuplicateUnitGain = true;

		// Token: 0x040075DF RID: 30175
		private bool m_bDefaultSort = true;

		// Token: 0x040075E0 RID: 30176
		private bool m_bProcessingCountDown;

		// Token: 0x040075E1 RID: 30177
		private const float HOLD_SKIP_TIME = 0.3f;

		// Token: 0x040075E2 RID: 30178
		private float m_fCurrentHoldTime;

		// Token: 0x02001944 RID: 6468
		// (Invoke) Token: 0x0600B81F RID: 47135
		public delegate void OnClose();

		// Token: 0x02001945 RID: 6469
		// (Invoke) Token: 0x0600B823 RID: 47139
		public delegate void OnTouchGameRecord();

		// Token: 0x02001946 RID: 6470
		private enum eTitleType
		{
			// Token: 0x0400AB14 RID: 43796
			None,
			// Token: 0x0400AB15 RID: 43797
			Win,
			// Token: 0x0400AB16 RID: 43798
			Lose,
			// Token: 0x0400AB17 RID: 43799
			Get,
			// Token: 0x0400AB18 RID: 43800
			Replay,
			// Token: 0x0400AB19 RID: 43801
			WinPrivate,
			// Token: 0x0400AB1A RID: 43802
			LosePrivate,
			// Token: 0x0400AB1B RID: 43803
			DrawPrivate
		}

		// Token: 0x02001947 RID: 6471
		public class BattleResultData
		{
			// Token: 0x17001980 RID: 6528
			// (get) Token: 0x0600B826 RID: 47142 RVA: 0x00368B5F File Offset: 0x00366D5F
			public bool IsWin
			{
				get
				{
					return this.m_BATTLE_RESULT_TYPE == BATTLE_RESULT_TYPE.BRT_WIN;
				}
			}

			// Token: 0x0600B827 RID: 47143 RVA: 0x00368B6C File Offset: 0x00366D6C
			public List<NKCUISlot.SlotData> GetAllListRewardSlotData()
			{
				List<NKCUISlot.SlotData> list = new List<NKCUISlot.SlotData>();
				list.AddRange(NKCUISlot.MakeSlotDataListFromReward(this.m_firstRewardData, false, false));
				list.AddRange(NKCUISlot.MakeSlotDataListFromReward(this.m_RewardData, false, false));
				list.AddRange(NKCUISlot.MakeSlotDataListFromReward(this.m_OnetimeRewardData, false, false));
				list.AddRange(NKCUISlot.MakeSlotDataListFromReward(this.m_firstAllClearData, false, false));
				list.AddRange(NKCUISlot.MakeSlotDataListFromReward(this.m_additionalReward));
				return list;
			}

			// Token: 0x0400AB1C RID: 43804
			public int m_stageID;

			// Token: 0x0400AB1D RID: 43805
			public BATTLE_RESULT_TYPE m_BATTLE_RESULT_TYPE = BATTLE_RESULT_TYPE.BRT_LOSE;

			// Token: 0x0400AB1E RID: 43806
			public NKM_GAME_TYPE m_NKM_GAME_TYPE = NKM_GAME_TYPE.NGT_PVP_RANK;

			// Token: 0x0400AB1F RID: 43807
			public List<NKCUIResultSubUIDungeon.MissionData> m_lstMissionData;

			// Token: 0x0400AB20 RID: 43808
			public bool m_bShowMedal;

			// Token: 0x0400AB21 RID: 43809
			public bool m_bShowBonus;

			// Token: 0x0400AB22 RID: 43810
			public List<NKCUIResultSubUIUnitExp.UnitLevelupUIData> m_lstUnitLevelupData;

			// Token: 0x0400AB23 RID: 43811
			public int m_iUnitExpBonusRate;

			// Token: 0x0400AB24 RID: 43812
			public NKMRewardData m_firstRewardData;

			// Token: 0x0400AB25 RID: 43813
			public NKMRewardData m_RewardData;

			// Token: 0x0400AB26 RID: 43814
			public NKMRewardData m_OnetimeRewardData;

			// Token: 0x0400AB27 RID: 43815
			public NKMRewardData m_firstAllClearData;

			// Token: 0x0400AB28 RID: 43816
			public NKMAdditionalReward m_additionalReward;

			// Token: 0x0400AB29 RID: 43817
			public int m_OrgPVPScore;

			// Token: 0x0400AB2A RID: 43818
			public int m_OrgPVPTier;

			// Token: 0x0400AB2B RID: 43819
			public long m_OrgDoubleToken;

			// Token: 0x0400AB2C RID: 43820
			public NKMRaidBossResultData m_RaidBossResultData;

			// Token: 0x0400AB2D RID: 43821
			public bool m_bShadowAllClear;

			// Token: 0x0400AB2E RID: 43822
			public int m_ShadowPrevLife;

			// Token: 0x0400AB2F RID: 43823
			public int m_ShadowCurrLife;

			// Token: 0x0400AB30 RID: 43824
			public int m_ShadowBestClearTime;

			// Token: 0x0400AB31 RID: 43825
			public int m_ShadowCurrClearTime;

			// Token: 0x0400AB32 RID: 43826
			public int m_iFierceScore;

			// Token: 0x0400AB33 RID: 43827
			public int m_iFierceBestScore;

			// Token: 0x0400AB34 RID: 43828
			public float m_fFierceLastBossHPPercent;

			// Token: 0x0400AB35 RID: 43829
			public float m_fFierceRestTime;

			// Token: 0x0400AB36 RID: 43830
			public bool m_bShowClearPoint;

			// Token: 0x0400AB37 RID: 43831
			public float m_fArenaClearPoint;

			// Token: 0x0400AB38 RID: 43832
			public long m_KillCountGain;

			// Token: 0x0400AB39 RID: 43833
			public long m_KillCountTotal;

			// Token: 0x0400AB3A RID: 43834
			public long m_KillCountStageRecord;

			// Token: 0x0400AB3B RID: 43835
			public NKCUIBattleStatistics.BattleData m_battleData;

			// Token: 0x0400AB3C RID: 43836
			public int m_multiply = 1;
		}

		// Token: 0x02001948 RID: 6472
		public class CityMissionResultData
		{
			// Token: 0x0400AB3D RID: 43837
			public int m_CityID;

			// Token: 0x0400AB3E RID: 43838
			public int m_MissionID;

			// Token: 0x0400AB3F RID: 43839
			public NKMUnitData m_LeaderUnitData;

			// Token: 0x0400AB40 RID: 43840
			public int m_CityLevelOld;

			// Token: 0x0400AB41 RID: 43841
			public int m_CityLevelNew;

			// Token: 0x0400AB42 RID: 43842
			public int m_CityExpOld;

			// Token: 0x0400AB43 RID: 43843
			public int m_CityExpNew;

			// Token: 0x0400AB44 RID: 43844
			public bool m_bGreatSuccess;

			// Token: 0x0400AB45 RID: 43845
			public List<NKCUIResultSubUIUnitExp.UnitLevelupUIData> m_lstUnitLevelupData;

			// Token: 0x0400AB46 RID: 43846
			public NKCUIResultSubUIUnitExp.UnitLevelupUIData LeaderUnitLevelupData;

			// Token: 0x0400AB47 RID: 43847
			public NKMRewardData m_RewardData;

			// Token: 0x0400AB48 RID: 43848
			public NKMRewardData m_SuccessRewardData;

			// Token: 0x0400AB49 RID: 43849
			public string m_SuccessSlotText;
		}
	}
}
