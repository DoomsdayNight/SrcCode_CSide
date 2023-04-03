using System;
using ClientPacket.Game;
using ClientPacket.Pvp;
using ClientPacket.User;
using Cs.Logging;
using NKC.Loading;
using NKC.Patcher;
using NKC.Publisher;
using NKC.Trim;
using NKC.UI;
using NKC.UI.Gauntlet;
using NKC.UI.HUD;
using NKC.UI.Option;
using NKC.UI.Result;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000710 RID: 1808
	public class NKC_SCEN_GAME : NKC_SCEN_BASIC
	{
		// Token: 0x06004731 RID: 18225 RVA: 0x00158D94 File Offset: 0x00156F94
		public GameObject Get_NKM_LENS_FLARE_LIST()
		{
			return this.m_NKM_LENS_FLARE_LIST;
		}

		// Token: 0x06004732 RID: 18226 RVA: 0x00158D9C File Offset: 0x00156F9C
		public NKC_SCEN_GAME_UI_DATA Get_NKC_SCEN_GAME_UI_DATA()
		{
			return this.m_NKC_SCEN_GAME_UI_DATA;
		}

		// Token: 0x06004733 RID: 18227 RVA: 0x00158DA4 File Offset: 0x00156FA4
		public NKCUIResult.BattleResultData Get_BattleResultData()
		{
			return this.GameResultUIData;
		}

		// Token: 0x06004734 RID: 18228 RVA: 0x00158DAC File Offset: 0x00156FAC
		public NKC_SCEN_GAME()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_GAME;
			this.m_NUM_GAME = NKCUIManager.OpenUI("NUM_GAME");
			this.m_NUF_GAME_Panel = NKCUIManager.OpenUI("NUF_GAME_Panel");
			this.m_NUM_GAME_PREFAB = GameObject.Find("NUM_GAME_PREFAB");
			this.m_NKCUIGame = this.m_NUF_GAME_Panel.GetComponent<NKCUIGame>();
			this.m_NKM_LENS_FLARE_LIST = GameObject.Find("NKM_LENS_FLARE_LIST");
			this.m_NUM_GAME.SetActive(false);
			this.m_NUF_GAME_Panel.SetActive(false);
		}

		// Token: 0x06004735 RID: 18229 RVA: 0x00158E50 File Offset: 0x00157050
		public void SetDungeonStrIDForLocal(string strID)
		{
			this.m_DungeonStrIDForLocal = strID;
		}

		// Token: 0x06004736 RID: 18230 RVA: 0x00158E5C File Offset: 0x0015705C
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			NKCUtil.ClearGauntletCacheData(NKCScenManager.GetScenManager());
			if (!this.m_bLoadedUI && this.m_NKC_SCEN_GAME_UI_DATA.m_NUF_GAME_PREFAB == null)
			{
				this.m_NKC_SCEN_GAME_UI_DATA.m_NUF_GAME_PREFAB = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_HUD_RENEWAL", "NUF_GAME_PREFAB", true, null);
			}
			if (!NKCScenManager.GetScenManager().GetGameClient().GetGameDataDummy().m_bLocal)
			{
				NKCPacketSender.Send_NKMPacket_INFORM_MY_LOADING_PROGRESS_REQ((byte)(this.m_fLoadingProgress * 100f));
			}
		}

		// Token: 0x06004737 RID: 18231 RVA: 0x00158ED4 File Offset: 0x001570D4
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
			if (!this.m_bLoadedUI)
			{
				this.m_NKC_SCEN_GAME_UI_DATA.m_NUFGameObjects = this.m_NKC_SCEN_GAME_UI_DATA.m_NUF_GAME_PREFAB.m_Instant.GetComponent<NKCGameHudObjects>();
				this.m_NKC_SCEN_GAME_UI_DATA.m_NUF_GAME_PREFAB.m_Instant.transform.SetParent(this.m_NUF_GAME_Panel.transform, false);
				this.m_NKC_SCEN_GAME_UI_DATA.m_GAME_BATTLE_MAP = this.m_NUM_GAME_PREFAB.transform.Find("GAME_BATTLE_MAP").gameObject;
				this.m_NKC_SCEN_GAME_UI_DATA.m_GAME_BATTLE_UNIT = this.m_NUM_GAME_PREFAB.transform.Find("GAME_BATTLE_UNIT").gameObject;
				this.m_NKC_SCEN_GAME_UI_DATA.m_GAME_BATTLE_UNIT_SHADOW = this.m_NUM_GAME_PREFAB.transform.Find("GAME_BATTLE_UNIT_SHADOW").gameObject;
				this.m_NKC_SCEN_GAME_UI_DATA.m_GAME_BATTLE_UNIT_MOTION_BLUR = this.m_NUM_GAME_PREFAB.transform.Find("GAME_BATTLE_UNIT_MOTION_BLUR").gameObject;
				this.m_NKC_SCEN_GAME_UI_DATA.m_GAME_BATTLE_UNIT_VIEWER = this.m_NUM_GAME_PREFAB.transform.Find("GAME_BATTLE_UNIT_VIEWER").gameObject;
				this.m_NKC_SCEN_GAME_UI_DATA.m_NUF_GAME_HUD_MINI_MAP = this.m_NKC_SCEN_GAME_UI_DATA.m_NUF_GAME_PREFAB.m_Instant.transform.Find("AB_UI_GAME_HUD/HUD_Top/HUD_TOP_MINI_MAP").gameObject;
				this.m_NKC_SCEN_GAME_UI_DATA.m_NUM_GAME_BATTLE_EFFECT = this.m_NUM_GAME_PREFAB.transform.Find("NUM_GAME_BATTLE_EFFECT").gameObject;
				NKCScenManager.GetScenManager().GetGameClient().InitUI(this.m_NKC_SCEN_GAME_UI_DATA.m_NUFGameObjects.m_GameHud);
			}
			this.m_NKCUIGameToastMSG.Invalid();
			NKCCamera.GetCamera().orthographic = true;
			NKCScenManager.GetScenManager().GetGameClient().LoadGame();
			if (!NKCScenManager.GetScenManager().GetGameClient().GetGameData().m_bLocal)
			{
				NKCPacketSender.Send_NKMPacket_INFORM_MY_LOADING_PROGRESS_REQ((byte)(this.m_fLoadingProgress * 100f));
			}
		}

		// Token: 0x06004738 RID: 18232 RVA: 0x001590B1 File Offset: 0x001572B1
		public void OnBackButton()
		{
			if (base.Get_NKC_SCEN_STATE() == NKC_SCEN_STATE.NSS_START && NKCScenManager.GetScenManager().GetGameClient() != null && NKCScenManager.GetScenManager().GetGameClient().GetGameRuntimeData() != null)
			{
				NKCScenManager.GetScenManager().GetGameClient().UI_GAME_PAUSE();
			}
		}

		// Token: 0x06004739 RID: 18233 RVA: 0x001590EC File Offset: 0x001572EC
		public override void ScenLoadLastStart()
		{
			Log.Info("NKC_SCEN_GAME:ScenLoadLastStart", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKC_SCEN_GAME.cs", 209);
			base.ScenLoadLastStart();
			if (!NKCScenManager.GetScenManager().GetGameClient().GetGameData().m_bLocal)
			{
				NKCPacketSender.Send_NKMPacket_INFORM_MY_LOADING_PROGRESS_REQ((byte)(this.m_fLoadingProgress * 100f));
			}
			Log.Info("NKC_SCEN_GAME:ScenLoadLastStart End", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKC_SCEN_GAME.cs", 220);
		}

		// Token: 0x0600473A RID: 18234 RVA: 0x0015914F File Offset: 0x0015734F
		public override void ScenLoadCompleteWait()
		{
			Log.Info("NKC_SCEN_GAME:ScenLoadCompleteWait Start", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKC_SCEN_GAME.cs", 225);
			base.ScenLoadCompleteWait();
			Log.Info("NKC_SCEN_GAME:ScenLoadCompleteWait End", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKC_SCEN_GAME.cs", 228);
		}

		// Token: 0x0600473B RID: 18235 RVA: 0x00159180 File Offset: 0x00157380
		public override void ScenLoadComplete()
		{
			Log.Info("NKC_SCEN_GAME:ScenLoadComplete Start", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKC_SCEN_GAME.cs", 233);
			base.ScenLoadComplete();
			NKC2DMotionAfterImage.Init();
			NKCScenManager.GetScenManager().GetGameClient().LoadGameComplete();
			Shader.WarmupAllShaders();
			if (!NKCScenManager.GetScenManager().GetGameClient().GetGameData().m_bLocal)
			{
				NKCPacketSender.Send_NKMPacket_INFORM_MY_LOADING_PROGRESS_REQ((byte)(this.m_fLoadingProgress * 100f));
				Log.Info("NKC_SCEN_GAME:ScenLoadLastStart Send_NKMPacket_INFORM_MY_LOADING_PROGRESS_REQ", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKC_SCEN_GAME.cs", 257);
			}
		}

		// Token: 0x0600473C RID: 18236 RVA: 0x001591FC File Offset: 0x001573FC
		private void SetEpisodeIDOrWarfareID(bool bReservedDetail = false)
		{
			NKMGameData gameData = NKCScenManager.GetScenManager().GetGameClient().GetGameData();
			if (gameData == null)
			{
				return;
			}
			NKM_GAME_TYPE gameType = gameData.GetGameType();
			if (gameType != NKM_GAME_TYPE.NGT_WARFARE)
			{
				if (gameType != NKM_GAME_TYPE.NGT_PHASE)
				{
					string dungeonStrID = NKMDungeonManager.GetDungeonStrID(gameData.m_DungeonID);
					if (NKMDungeonManager.GetDungeonTempletBase(dungeonStrID) != null)
					{
						NKCScenManager.GetScenManager().Get_SCEN_OPERATION();
						NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(dungeonStrID);
						if (nkmstageTempletV != null)
						{
							if (!NKCScenManager.GetScenManager().Get_SCEN_OPERATION().PlayByFavorite)
							{
								NKCScenManager.GetScenManager().Get_SCEN_OPERATION().SetReservedEpisodeTemplet(nkmstageTempletV.EpisodeTemplet);
								return;
							}
							NKCScenManager.GetScenManager().Get_SCEN_OPERATION().SetReservedEpisodeCategory(nkmstageTempletV.EpisodeCategory);
						}
					}
				}
				else
				{
					NKMStageTempletV2 nkmstageTempletV2 = NKMStageTempletV2.Find(NKCPhaseManager.GetLastStageID());
					if (nkmstageTempletV2 != null)
					{
						NKCScenManager.GetScenManager().Get_SCEN_OPERATION().SetReservedEpisodeTemplet(nkmstageTempletV2.EpisodeTemplet);
						return;
					}
				}
				return;
			}
			NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().SetWarfareStrID(NKCWarfareManager.GetWarfareStrID(gameData.m_WarfareID));
		}

		// Token: 0x0600473D RID: 18237 RVA: 0x001592D8 File Offset: 0x001574D8
		public override void ScenStart()
		{
			base.ScenStart();
			NKCScenChangeOrder nkcscenChangeOrder = NKCScenManager.GetScenManager().PeekNextScenChangeOrder();
			if (nkcscenChangeOrder != null && nkcscenChangeOrder.m_NextScen == NKM_SCEN_ID.NSI_GAME && nkcscenChangeOrder.m_bForce)
			{
				Log.Info("Game Scene Reloading. dropping current game scene!", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKC_SCEN_GAME.cs", 347);
				return;
			}
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData != null)
			{
				float num = (float)gameOptionData.EffectOpacity / 100f;
				Shader.SetGlobalFloat("_FxGlobalTransparency", 1f - num * num);
			}
			this.m_NKCUIGame.Open();
			this.m_NKCUIGameToastMSG.Reset(NKCScenManager.GetScenManager().GetGameClient().GetGameData(), NKCScenManager.GetScenManager().GetGameClient().GetGameRuntimeData());
			if (NKCScenManager.GetScenManager().GetGameClient().GetGameData().IsPVP())
			{
				NKCScenManager.GetScenManager().SetActiveLoadingUI(NKCLoadingScreenManager.eGameContentsType.GAUNTLET, 0);
				NKCUIManager.LoadingUI.SetWaitOpponent();
				this.m_bWaitingEnemyGameLoading = true;
				NKCPacketSender.Send_Packet_GAME_LOAD_COMPLETE_REQ(NKCScenManager.GetScenManager().GetGameClient().GetIntrude());
			}
			else
			{
				this.DungeonScenStart();
			}
			this.SetEpisodeIDOrWarfareID(false);
			if (this.GameResultUIData != null)
			{
				this.EndGameWithReservedGameData();
			}
		}

		// Token: 0x0600473E RID: 18238 RVA: 0x001593E6 File Offset: 0x001575E6
		public void TrySendGamePauseEnableREQ()
		{
			NKCScenManager.GetScenManager().GetGameClient().TrySendGamePauseEnableREQ();
		}

		// Token: 0x0600473F RID: 18239 RVA: 0x001593F8 File Offset: 0x001575F8
		private void DungeonScenStart()
		{
			this.m_bWaitingEnemyGameLoading = false;
			this.SetShow(true);
			if (NKCScenManager.CurrentUserData() != null)
			{
				long userUID = NKCScenManager.CurrentUserData().m_UserUID;
			}
			NKMPopUpBox.OpenWaitBox(0f, "");
			this.m_UpdateTime = 0f;
			NKCPacketSender.Send_Packet_GAME_LOAD_COMPLETE_REQ(NKCScenManager.GetScenManager().GetGameClient().GetIntrude());
		}

		// Token: 0x06004740 RID: 18240 RVA: 0x00159454 File Offset: 0x00157654
		public override void ScenEnd()
		{
			Shader.SetGlobalFloat("_FxGlobalTransparency", 0f);
			this.ScenClear();
			this.m_NKCUIGame.Close();
			NKCUIOverlayCharMessage.CheckInstanceAndClose();
			NKCUIOverlayTutorialGuide.CheckInstanceAndClose();
			this.m_NKC_SCEN_GAME_UI_DATA.Init();
			NKC2DMotionAfterImage.CleanUp();
			NKCReplayMgr nkcreplaMgr = NKCReplayMgr.GetNKCReplaMgr();
			if (nkcreplaMgr != null)
			{
				nkcreplaMgr.OnGameScenEnd();
			}
			base.ScenEnd();
		}

		// Token: 0x06004741 RID: 18241 RVA: 0x001594B4 File Offset: 0x001576B4
		public void ScenClear()
		{
			if (this.m_NUM_GAME.activeSelf)
			{
				this.m_NUM_GAME.SetActive(false);
			}
			if (this.m_NUF_GAME_Panel.activeSelf)
			{
				this.m_NUF_GAME_Panel.SetActive(false);
			}
			NKCScenManager.GetScenManager().GetGameClient().EndGame();
			NKCSoundManager.Unload();
			this.UnloadUI();
		}

		// Token: 0x06004742 RID: 18242 RVA: 0x0015950D File Offset: 0x0015770D
		public override void UnloadUI()
		{
			base.UnloadUI();
			NKCScenManager.GetScenManager().GetGameClient().UnloadUI();
			if (this.m_NKC_SCEN_GAME_UI_DATA.m_NUF_GAME_PREFAB != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_NKC_SCEN_GAME_UI_DATA.m_NUF_GAME_PREFAB);
			}
			this.m_NKC_SCEN_GAME_UI_DATA.Init();
		}

		// Token: 0x06004743 RID: 18243 RVA: 0x0015954C File Offset: 0x0015774C
		public override void ScenUpdate()
		{
			if (!this.m_bWaitingEnemyGameLoading)
			{
				base.ScenUpdate();
				NKCScenManager.GetScenManager().GetGameClient().Update(Time.deltaTime);
				this.UpdateAtGamePlaying();
			}
		}

		// Token: 0x06004744 RID: 18244 RVA: 0x00159578 File Offset: 0x00157778
		private void UpdateAtGamePlaying()
		{
			if (base.Get_NKC_SCEN_STATE() == NKC_SCEN_STATE.NSS_START && NKCScenManager.GetScenManager().GetGameClient() != null && NKCScenManager.GetScenManager().GetGameClient().GetGameRuntimeData() != null && NKCScenManager.GetScenManager().GetGameClient().GetGameRuntimeData().m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_PLAY)
			{
				this.m_NKCUIGameToastMSG.Update();
				if (this.m_fNextCancelPauseCheckTime < Time.time)
				{
					this.m_fNextCancelPauseCheckTime = Time.time + 5f;
					if (NKCScenManager.GetScenManager().GetGameClient().GetGameData() != null && NKCScenManager.GetScenManager().GetGameClient().GetGameData().IsPVE() && NKCScenManager.GetScenManager().GetNKCPowerSaveMode().GetEnable() && NKCScenManager.GetScenManager().GetGameClient().GetGameRuntimeData().m_bPause && !NKMPopUpBox.IsOpenedWaitBox())
					{
						NKCScenManager.GetScenManager().GetGameClient().Send_Packet_GAME_PAUSE_REQ(false, false, NKC_OPEN_POPUP_TYPE_AFTER_PAUSE.NOPTAP_GAME_OPTION_POPUP);
					}
				}
			}
		}

		// Token: 0x06004745 RID: 18245 RVA: 0x0015965F File Offset: 0x0015785F
		public override bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x06004746 RID: 18246 RVA: 0x00159662 File Offset: 0x00157862
		public void OnRecv(NKMPacket_PVP_GAME_MATCH_COMPLETE_NOT cNKMPacket_PVP_GAME_MATCH_COMPLETE_NOT)
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAME, true);
		}

		// Token: 0x06004747 RID: 18247 RVA: 0x00159670 File Offset: 0x00157870
		public void OnRecv(NKMPacket_GAME_LOAD_COMPLETE_ACK cNKMPacket_GAME_LOAD_COMPLETE_ACK)
		{
			NKCScenManager.GetScenManager().GetGameClient().OnRecv(cNKMPacket_GAME_LOAD_COMPLETE_ACK);
		}

		// Token: 0x06004748 RID: 18248 RVA: 0x00159684 File Offset: 0x00157884
		public void OnRecv(NKMPacket_GAME_START_NOT cNKMPacket_GAME_START_NOT)
		{
			if (NKCScenManager.GetScenManager().GetGameClient().GetGameData().IsPVP() && this.m_bWaitingEnemyGameLoading)
			{
				this.m_NUM_GAME.SetActive(true);
				this.m_NUF_GAME_Panel.SetActive(true);
				this.m_UpdateTime = 0f;
				this.m_bWaitingEnemyGameLoading = false;
				NKCScenManager.GetScenManager().CloseLoadingUI();
			}
			NKCScenManager.GetScenManager().GetGameClient().StartGame(false);
			NKMPopUpBox.CloseWaitBox();
			this.SetAutoIfAutoGame();
		}

		// Token: 0x06004749 RID: 18249 RVA: 0x00159700 File Offset: 0x00157900
		private void SetAutoIfAutoGame()
		{
			NKCGameClient gameClient = NKCScenManager.GetScenManager().GetGameClient();
			if (gameClient == null)
			{
				return;
			}
			if (gameClient.GetGameData() == null)
			{
				return;
			}
			NKMGameRuntimeData gameRuntimeData = gameClient.GetGameRuntimeData();
			if (gameRuntimeData == null)
			{
				return;
			}
			NKMGameRuntimeTeamData myRuntimeTeamData = gameRuntimeData.GetMyRuntimeTeamData(NKCScenManager.GetScenManager().GetGameClient().m_MyTeam);
			if (myRuntimeTeamData != null && !myRuntimeTeamData.m_bAutoRespawn && this.CheckAutoGame())
			{
				gameClient.Send_Packet_GAME_AUTO_RESPAWN_REQ(true, true);
			}
		}

		// Token: 0x0600474A RID: 18250 RVA: 0x00159764 File Offset: 0x00157964
		public void OnRecv(NKMPacket_GAME_INTRUDE_START_NOT cNKMPacket_GAME_INTRUDE_START_NOT)
		{
			if (NKCScenManager.GetScenManager().GetGameClient().GetGameData().IsPVP() && this.m_bWaitingEnemyGameLoading)
			{
				if (!this.m_NUM_GAME.activeSelf)
				{
					this.m_NUM_GAME.SetActive(true);
				}
				if (!this.m_NUF_GAME_Panel.activeSelf)
				{
					this.m_NUF_GAME_Panel.SetActive(true);
				}
				this.m_UpdateTime = 0f;
				this.m_bWaitingEnemyGameLoading = false;
				NKCScenManager.GetScenManager().CloseLoadingUI();
			}
			this.m_NKCUIGameToastMSG.SetCost((int)cNKMPacket_GAME_INTRUDE_START_NOT.usedRespawnCost);
			NKCScenManager.GetScenManager().GetGameClient().OnRecv(cNKMPacket_GAME_INTRUDE_START_NOT);
			NKCScenManager.GetScenManager().GetGameClient().StartGame(false);
			NKMPopUpBox.CloseWaitBox();
		}

		// Token: 0x0600474B RID: 18251 RVA: 0x00159814 File Offset: 0x00157A14
		private void ExitToRaid()
		{
			if (NKCScenManager.GetScenManager().GetGameClient() == null)
			{
				this.ExitGame(NKM_SCEN_ID.NSI_WORLDMAP);
				return;
			}
			NKMGameData gameData = NKCScenManager.GetScenManager().GetGameClient().GetGameData();
			if (gameData == null || NKCScenManager.GetScenManager().GetNKCRaidDataMgr().CheckCompletableRaid(gameData.m_RaidUID))
			{
				this.ExitGame(NKM_SCEN_ID.NSI_WORLDMAP);
				return;
			}
			NKCScenManager.GetScenManager().Get_NKC_SCEN_RAID().SetRaidUID(gameData.m_RaidUID);
			this.ExitGame(NKM_SCEN_ID.NSI_RAID);
		}

		// Token: 0x0600474C RID: 18252 RVA: 0x00159888 File Offset: 0x00157A88
		private void TutorialExit(int dungeonID)
		{
			if (NKCPatchUtility.BackgroundPatchEnabled())
			{
				if (dungeonID == 1007 && NKCPatchUtility.SaveTutorialClearedStatus())
				{
					Debug.Log("Tutorial final stage cleared!");
					return;
				}
				this.ExitGame(NKM_SCEN_ID.NSI_OPERATION);
				return;
			}
			else
			{
				if (NKCPatchDownloader.Instance != null && NKCPatchDownloader.Instance.ProloguePlay)
				{
					NKCScenManager.GetScenManager().GetGameClient().EndGame();
					NKCScenManager.GetScenManager().ShowBundleUpdate(true);
					return;
				}
				this.ExitGame(NKM_SCEN_ID.NSI_OPERATION);
				return;
			}
		}

		// Token: 0x0600474D RID: 18253 RVA: 0x001598FC File Offset: 0x00157AFC
		private void ExitToDungeonResult(NKMStageTempletV2 stageTemplet, NKCUIResult.BattleResultData resultData)
		{
			long num = 0L;
			long leaderShipUID = 0L;
			int num2 = 0;
			int skinID = 0;
			bool flag = false;
			if (stageTemplet != null)
			{
				if (stageTemplet.DungeonTempletBase != null && stageTemplet.DungeonTempletBase.IsUsingEventDeck())
				{
					NKMEventDeckData lastEventDeck = NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY().GetLastEventDeck();
					if (lastEventDeck != null)
					{
						leaderShipUID = lastEventDeck.m_ShipUID;
						num = lastEventDeck.GetUnitUID(lastEventDeck.m_LeaderIndex);
						if (num == 0L)
						{
							NKMDungeonEventDeckTemplet.EventDeckSlot unitSlot = stageTemplet.DungeonTempletBase.EventDeckTemplet.GetUnitSlot(lastEventDeck.m_LeaderIndex);
							if (unitSlot.m_eType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_GUEST || unitSlot.m_eType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_NPC)
							{
								num2 = unitSlot.m_ID;
								skinID = unitSlot.m_SkinID;
							}
						}
						flag = true;
					}
				}
				else
				{
					NKMDeckIndex lastDeckIndex = NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY().GetLastDeckIndex();
					NKMDeckData deckData = NKCScenManager.CurrentUserData().m_ArmyData.GetDeckData(lastDeckIndex);
					if (deckData != null)
					{
						leaderShipUID = deckData.m_ShipUID;
						num = deckData.GetLeaderUnitUID();
						flag = true;
					}
				}
			}
			if (!flag)
			{
				NKMGameTeamData teamData = NKCScenManager.GetScenManager().GetGameClient().GetGameData().GetTeamData(NKCScenManager.CurrentUserData().m_UserUID);
				if (teamData != null)
				{
					NKMUnitData leaderUnitData = teamData.GetLeaderUnitData();
					if (leaderUnitData != null)
					{
						num2 = leaderUnitData.m_UnitID;
						skinID = leaderUnitData.m_SkinID;
						flag = true;
					}
				}
			}
			if (flag)
			{
				NKCScenManager.GetScenManager().GET_NKC_SCEN_DUNGEON_RESULT().SetData(resultData, num, leaderShipUID);
				if (num2 != 0)
				{
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(num2);
					if (unitTempletBase == null || !unitTempletBase.m_bContractable)
					{
						num2 = 999;
						skinID = 0;
					}
					NKCScenManager.GetScenManager().GET_NKC_SCEN_DUNGEON_RESULT().SetDummyLeader(num2, skinID);
				}
				this.ExitGame(NKM_SCEN_ID.NSI_DUNGEON_RESULT);
				return;
			}
			this.ExitGame(NKM_SCEN_ID.NSI_OPERATION);
		}

		// Token: 0x0600474E RID: 18254 RVA: 0x00159A84 File Offset: 0x00157C84
		private void ExitToDiveInstance()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData != null)
			{
				if (NKCScenManager.GetScenManager().Get_NKC_SCEN_DIVE_RESULT().GetExistNewData())
				{
					this.ExitGame(NKM_SCEN_ID.NSI_DIVE_RESULT);
					return;
				}
				if (myUserData.m_DiveGameData != null)
				{
					this.ExitGame(NKM_SCEN_ID.NSI_DIVE);
					return;
				}
			}
			this.ExitGame(NKM_SCEN_ID.NSI_WORLDMAP);
		}

		// Token: 0x0600474F RID: 18255 RVA: 0x00159AD4 File Offset: 0x00157CD4
		private void ExitToShadow(bool bGiveUp, bool bEnd)
		{
			if (bGiveUp && bEnd)
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_SHADOW_PALACE, true);
				return;
			}
			if (!bGiveUp && bEnd)
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_SHADOW_RESULT, true);
				return;
			}
			NKCScenManager.GetScenManager().Get_NKC_SCEN_SHADOW_BATTLE().SetShadowPalaceID(NKCScenManager.CurrentUserData().m_ShadowPalace.currentPalaceId);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_SHADOW_BATTLE, true);
		}

		// Token: 0x06004750 RID: 18256 RVA: 0x00159B35 File Offset: 0x00157D35
		private void ExitToGuildCoop()
		{
			this.ExitGame(NKM_SCEN_ID.NSI_GUILD_COOP);
		}

		// Token: 0x06004751 RID: 18257 RVA: 0x00159B40 File Offset: 0x00157D40
		private void ExitGame(NKM_SCEN_ID scenID)
		{
			NKCScenManager.GetScenManager().GetGameClient().EndGame();
			if (NKCScenManager.GetScenManager().GetGameClient().GetGameData() != null && NKCScenManager.GetScenManager().GetGameClient().GetGameData().IsPVP())
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LOBBY, true);
				return;
			}
			if (scenID == NKM_SCEN_ID.NSI_OPERATION)
			{
				this.SetEpisodeIDOrWarfareID(true);
			}
			NKCScenManager.GetScenManager().ScenChangeFade(scenID, true);
		}

		// Token: 0x06004752 RID: 18258 RVA: 0x00159BAC File Offset: 0x00157DAC
		public void DoAfterGiveUp()
		{
			NKCUIGameOption.CheckInstanceAndClose();
			NKMGameData gameData = NKCScenManager.GetScenManager().GetGameClient().GetGameData();
			switch (gameData.m_NKM_GAME_TYPE)
			{
			case NKM_GAME_TYPE.NGT_DUNGEON:
			case NKM_GAME_TYPE.NGT_PHASE:
			{
				NKCRepeatOperaion nkcrepeatOperaion = NKCScenManager.GetScenManager().GetNKCRepeatOperaion();
				if (nkcrepeatOperaion != null && nkcrepeatOperaion.GetIsOnGoing())
				{
					nkcrepeatOperaion.Init();
				}
				if (NKCScenManager.GetScenManager().Get_SCEN_OPERATION().GetReservedEpisodeTemplet() == null)
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
					return;
				}
				this.SetEpisodeIDOrWarfareID(true);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OPERATION, true);
				return;
			}
			case NKM_GAME_TYPE.NGT_WARFARE:
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().SetReservedShowBattleResult(true);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_WARFARE_GAME, true);
				return;
			case NKM_GAME_TYPE.NGT_DIVE:
				this.ExitToDiveInstance();
				return;
			case NKM_GAME_TYPE.NGT_TUTORIAL:
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				return;
			case NKM_GAME_TYPE.NGT_RAID:
			case NKM_GAME_TYPE.NGT_RAID_SOLO:
				this.ExitToRaid();
				return;
			case NKM_GAME_TYPE.NGT_SHADOW_PALACE:
				this.ExitToShadow(true, NKCScenManager.CurrentUserData().m_ShadowPalace.life == 0);
				return;
			case NKM_GAME_TYPE.NGT_FIERCE:
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_FIERCE_BATTLE_SUPPORT, true);
				return;
			case NKM_GAME_TYPE.NGT_GUILD_DUNGEON_ARENA:
			case NKM_GAME_TYPE.NGT_GUILD_DUNGEON_BOSS:
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GUILD_COOP, true);
				return;
			case NKM_GAME_TYPE.NGT_TRIM:
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_TRIM, true);
				return;
			}
			if (gameData.IsPVP())
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LOBBY, true);
				return;
			}
			this.SetEpisodeIDOrWarfareID(true);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_OPERATION, true);
		}

		// Token: 0x06004753 RID: 18259 RVA: 0x00159D2C File Offset: 0x00157F2C
		public void OnLocalGameEndRecv(NKMPacket_GAME_END_NOT cPacket_GAME_END_NOT)
		{
			NKCUIGameOption.CheckInstanceAndClose();
			if (NKCScenManager.GetScenManager().GetGameClient().GetGameData().m_bLocal)
			{
				NKCScenManager.GetScenManager().GetGameClient().EndGame();
				NKCPacketSender.Send_NKMPacket_DEV_GAME_LOAD_REQ(this.m_DungeonStrIDForLocal);
				return;
			}
			Debug.LogError("Unexpected non-local NKMPacket_GAME_END_NOT packet");
		}

		// Token: 0x06004754 RID: 18260 RVA: 0x00159D79 File Offset: 0x00157F79
		public void ReserveGameEndData(NKCUIResult.BattleResultData resultData)
		{
			this.GameResultUIData = resultData;
		}

		// Token: 0x06004755 RID: 18261 RVA: 0x00159D84 File Offset: 0x00157F84
		public void EndGameWithReservedGameData()
		{
			if (this.GameResultUIData != null)
			{
				this.OnEndGame(this.GameResultUIData);
				this.GameResultUIData = null;
				return;
			}
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_ERROR_SERVER_GAME_DATA_AND_GO_LOBBY, delegate()
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
			}, "");
		}

		// Token: 0x06004756 RID: 18262 RVA: 0x00159DE0 File Offset: 0x00157FE0
		public void DisconnectAll()
		{
			NKCScenManager.GetScenManager().GetConnectGame().Reconnect();
		}

		// Token: 0x06004757 RID: 18263 RVA: 0x00159DF4 File Offset: 0x00157FF4
		private bool CheckAutoGame()
		{
			NKMGameData gameData = NKCScenManager.GetScenManager().GetGameClient().GetGameData();
			if (gameData == null)
			{
				return false;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return false;
			}
			if (myUserData.m_UserOption == null)
			{
				return false;
			}
			NKM_GAME_TYPE gameType = gameData.GetGameType();
			if (gameType != NKM_GAME_TYPE.NGT_WARFARE)
			{
				if (gameType == NKM_GAME_TYPE.NGT_DIVE)
				{
					if (myUserData.m_UserOption.m_bAutoDive)
					{
						return true;
					}
				}
			}
			else if (myUserData.m_UserOption.m_bAutoWarfare)
			{
				return true;
			}
			return NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing();
		}

		// Token: 0x06004758 RID: 18264 RVA: 0x00159E74 File Offset: 0x00158074
		public void OnEndGame(NKCUIResult.BattleResultData resultData)
		{
			NKCUIGameOption.CheckInstanceAndClose();
			this.m_NUF_GAME_Panel.SetActive(false);
			NKMGameData gameData = NKCScenManager.GetScenManager().GetGameClient().GetGameData();
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GAME_RESULT().SetStageID(resultData.m_stageID);
			if (NKMGame.IsPVP(gameData.GetGameType()))
			{
				NKCUIGauntletResult.SetResultData(resultData);
				NKM_GAME_TYPE gameType = gameData.GetGameType();
				NKC_GAUNTLET_LOBBY_TAB reservedLobbyTab;
				if (gameType != NKM_GAME_TYPE.NGT_PVP_RANK)
				{
					if (gameType != NKM_GAME_TYPE.NGT_ASYNC_PVP)
					{
						switch (gameType)
						{
						case NKM_GAME_TYPE.NGT_PVP_PRIVATE:
							reservedLobbyTab = NKC_GAUNTLET_LOBBY_TAB.NGLT_PRIVATE;
							goto IL_AB;
						case NKM_GAME_TYPE.NGT_PVP_LEAGUE:
							reservedLobbyTab = NKC_GAUNTLET_LOBBY_TAB.NGLT_LEAGUE;
							goto IL_AB;
						case NKM_GAME_TYPE.NGT_PVP_STRATEGY:
						case NKM_GAME_TYPE.NGT_PVP_STRATEGY_REVENGE:
						case NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC:
							break;
						default:
							reservedLobbyTab = NKC_GAUNTLET_LOBBY_TAB.NGLT_RANK;
							goto IL_AB;
						}
					}
					reservedLobbyTab = NKC_GAUNTLET_LOBBY_TAB.NGLT_ASYNC;
				}
				else
				{
					reservedLobbyTab = NKC_GAUNTLET_LOBBY_TAB.NGLT_RANK;
				}
				IL_AB:
				gameType = gameData.GetGameType();
				if (gameType != NKM_GAME_TYPE.NGT_PVP_STRATEGY_REVENGE)
				{
					if (gameType == NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC)
					{
						NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetReservedAsyncTab(NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_NPC);
					}
					else
					{
						NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetReservedAsyncTab(NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.MAX);
					}
				}
				else
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetReservedAsyncTab(NKCUIGauntletLobbyAsyncV2.PVP_ASYNC_TYPE.PAT_REVENGE);
				}
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetReservedLobbyTab(reservedLobbyTab);
				if (NKCReplayMgr.IsPlayingReplay())
				{
					resultData.m_OrgDoubleToken = 0L;
					resultData.m_RewardData = new NKMRewardData();
				}
				if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_ROOM) && NKCScenManager.GetScenManager().GetGameClient().IsObserver(NKCScenManager.CurrentUserData()))
				{
					NKCPrivatePVPRoomMgr.CancelAllProcess();
					return;
				}
				if (gameData.GetGameType() == NKM_GAME_TYPE.NGT_PVP_PRIVATE)
				{
					NKCUIGauntletResult.OnClose <>9__4;
					NKCScenManager.GetScenManager().Get_NKC_SCEN_GAME_RESULT().SetDoAtScenStart(delegate
					{
						NKCUIGauntletResult nkcuigauntletResult = NKCUIManager.NKCUIGauntletResult;
						NKCUIGauntletResult.OnClose dOnClose;
						if ((dOnClose = <>9__4) == null)
						{
							dOnClose = (<>9__4 = delegate()
							{
								NKCUIResult.Instance.OpenPrivatePvpResult(NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData, resultData, delegate
								{
									NKCContentManager.ShowContentUnlockPopup(delegate
									{
										NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LOBBY, true);
									}, new STAGE_UNLOCK_REQ_TYPE[]
									{
										STAGE_UNLOCK_REQ_TYPE.SURT_PVP_RANK_SCORE,
										STAGE_UNLOCK_REQ_TYPE.SURT_PVP_RANK_SCORE_RECORD,
										STAGE_UNLOCK_REQ_TYPE.SURT_PVP_ASYNC_SCORE,
										STAGE_UNLOCK_REQ_TYPE.SURT_PVP_ASYNC_SCORE_RECORD
									});
								}, resultData.m_battleData);
							});
						}
						nkcuigauntletResult.Open(dOnClose);
					});
				}
				else if (gameData.IsPVP())
				{
					NKCUIGauntletResult.OnClose <>9__7;
					NKCScenManager.GetScenManager().Get_NKC_SCEN_GAME_RESULT().SetDoAtScenStart(delegate
					{
						NKCUIGauntletResult nkcuigauntletResult = NKCUIManager.NKCUIGauntletResult;
						NKCUIGauntletResult.OnClose dOnClose;
						if ((dOnClose = <>9__7) == null)
						{
							dOnClose = (<>9__7 = delegate()
							{
								NKCUIResult.Instance.OpenComplexResult(NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData, resultData.m_RewardData, delegate
								{
									NKCContentManager.ShowContentUnlockPopup(delegate
									{
										NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LOBBY, true);
									}, new STAGE_UNLOCK_REQ_TYPE[]
									{
										STAGE_UNLOCK_REQ_TYPE.SURT_PVP_RANK_SCORE,
										STAGE_UNLOCK_REQ_TYPE.SURT_PVP_RANK_SCORE_RECORD,
										STAGE_UNLOCK_REQ_TYPE.SURT_PVP_ASYNC_SCORE,
										STAGE_UNLOCK_REQ_TYPE.SURT_PVP_ASYNC_SCORE_RECORD
									});
								}, resultData.m_OrgDoubleToken, resultData.m_battleData, true, true);
							});
						}
						nkcuigauntletResult.Open(dOnClose);
					});
				}
				else
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_GAME_RESULT().SetDoAtScenStart(delegate
					{
						NKCUIResult.Instance.OpenComplexResult(NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData, resultData.m_RewardData, delegate
						{
							NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LOBBY, true);
						}, resultData.m_OrgDoubleToken, resultData.m_battleData, true, true);
					});
				}
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAME_RESULT, true);
				return;
			}
			else
			{
				if (!NKMGame.IsPVE(gameData.GetGameType()))
				{
					Debug.LogError("Reward UI Type Not Defined!");
					NKCUIResult.Instance.OpenBattleResult(resultData, delegate
					{
						this.ExitGame(NKM_SCEN_ID.NSI_HOME);
					}, false);
					return;
				}
				bool flag = true;
				int dungeonID = gameData.m_DungeonID;
				NKMDungeonTempletBase cNKMDungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(dungeonID);
				bool flag2 = true;
				NKCUIResult.OnClose ClosingDelegate;
				switch (gameData.GetGameType())
				{
				case NKM_GAME_TYPE.NGT_DUNGEON:
					flag = false;
					ClosingDelegate = delegate()
					{
						int stageID = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAME_RESULT().GetStageID();
						NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(stageID);
						if (NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing())
						{
							NKMDungeonTempletBase dungeonTempletBase = nkmstageTempletV.DungeonTempletBase;
							if (NKCRepeatOperaion.CheckVisible(stageID) && dungeonTempletBase != null)
							{
								if (dungeonTempletBase.IsUsingEventDeck())
								{
									NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ(NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY().GetLastEventDeck(), stageID, 0, dungeonTempletBase.m_DungeonID, 0, false, 1, 0);
									return;
								}
								NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ(NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY().GetLastDeckIndex().m_iIndex, stageID, 0, dungeonTempletBase.m_DungeonID, 0, false, 1, 0);
								return;
							}
						}
						this.ExitToDungeonResult(nkmstageTempletV, resultData);
					};
					goto IL_57E;
				case NKM_GAME_TYPE.NGT_WARFARE:
					ClosingDelegate = delegate()
					{
						NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().SetReservedShowBattleResult(true);
						this.ExitGame(NKM_SCEN_ID.NSI_WARFARE_GAME);
					};
					goto IL_57E;
				case NKM_GAME_TYPE.NGT_DIVE:
					ClosingDelegate = new NKCUIResult.OnClose(this.ExitToDiveInstance);
					flag = (resultData.GetAllListRewardSlotData().Count > 0);
					goto IL_57E;
				case NKM_GAME_TYPE.NGT_RAID:
				case NKM_GAME_TYPE.NGT_RAID_SOLO:
					ClosingDelegate = new NKCUIResult.OnClose(this.ExitToRaid);
					goto IL_57E;
				case NKM_GAME_TYPE.NGT_SHADOW_PALACE:
					if (resultData.m_ShadowCurrLife == 0 || resultData.m_bShadowAllClear)
					{
						ClosingDelegate = delegate()
						{
							this.ExitToShadow(false, true);
						};
						goto IL_57E;
					}
					ClosingDelegate = delegate()
					{
						this.ExitToShadow(false, false);
					};
					goto IL_57E;
				case NKM_GAME_TYPE.NGT_FIERCE:
					ClosingDelegate = delegate()
					{
						NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_FIERCE_BATTLE_SUPPORT, true);
					};
					goto IL_57E;
				case NKM_GAME_TYPE.NGT_PHASE:
				{
					if (resultData.m_BATTLE_RESULT_TYPE != BATTLE_RESULT_TYPE.BRT_WIN)
					{
						ClosingDelegate = delegate()
						{
							this.ExitGame(NKM_SCEN_ID.NSI_OPERATION);
						};
						goto IL_57E;
					}
					if (NKCPhaseManager.ShouldPlayNextPhase())
					{
						flag2 = false;
						flag = false;
						ClosingDelegate = delegate()
						{
							if (NKMDungeonManager.GetDungeonTempletBase(NKCPhaseManager.PhaseModeState.dungeonId) != null)
							{
								NKMStageTempletV2 stageTemplet = NKMStageTempletV2.Find(NKCPhaseManager.PhaseModeState.stageId);
								NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY().SetDungeonInfo(stageTemplet, DeckContents.PHASE);
								NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_DUNGEON_ATK_READY, true);
							}
						};
						goto IL_57E;
					}
					int currentStageID = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAME_RESULT().GetStageID();
					if (NKCRepeatOperaion.CheckVisible(currentStageID) && NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing())
					{
						NKMStageTempletV2 stageTemplet = NKMStageTempletV2.Find(currentStageID);
						ClosingDelegate = delegate()
						{
							if (stageTemplet == null)
							{
								this.ExitGame(NKM_SCEN_ID.NSI_OPERATION);
								return;
							}
							if (stageTemplet.IsUsingEventDeck())
							{
								NKCPacketSender.Send_NKMPacket_PHASE_START_REQ(currentStageID, NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY().GetLastEventDeck());
								return;
							}
							NKCPacketSender.Send_NKMPacket_PHASE_START_REQ(currentStageID, NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY().GetLastDeckIndex());
						};
						goto IL_57E;
					}
					if (this.CheckCutscen(gameData.m_NKM_GAME_TYPE, resultData.m_stageID, gameData.m_DungeonID))
					{
						NKMStageTempletV2 stageTemplet = NKMStageTempletV2.Find(resultData.m_stageID);
						NKCUICutScenPlayer.CutScenCallBack <>9__25;
						ClosingDelegate = delegate()
						{
							if (stageTemplet != null && stageTemplet.PhaseTemplet != null)
							{
								NKCCutScenTemplet cutScenTemple = NKCCutScenManager.GetCutScenTemple(stageTemplet.PhaseTemplet.m_CutScenStrIDAfter);
								if (cutScenTemple != null)
								{
									NKC_SCEN_CUTSCEN_DUNGEON nkc_SCEN_CUTSCEN_DUNGEON = NKCScenManager.GetScenManager().Get_NKC_SCEN_CUTSCEN_DUNGEON();
									string cutScenStrID = cutScenTemple.m_CutScenStrID;
									NKCUICutScenPlayer.CutScenCallBack callBack;
									if ((callBack = <>9__25) == null)
									{
										callBack = (<>9__25 = delegate()
										{
											this.ExitGame(NKM_SCEN_ID.NSI_OPERATION);
										});
									}
									nkc_SCEN_CUTSCEN_DUNGEON.SetReservedOneCutscenType(cutScenStrID, callBack, cNKMDungeonTempletBase.m_DungeonStrID);
									NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_CUTSCENE_DUNGEON, true);
									return;
								}
								this.ExitGame(NKM_SCEN_ID.NSI_OPERATION);
							}
						};
						goto IL_57E;
					}
					ClosingDelegate = delegate()
					{
						this.ExitGame(NKM_SCEN_ID.NSI_OPERATION);
					};
					goto IL_57E;
				}
				case NKM_GAME_TYPE.NGT_GUILD_DUNGEON_ARENA:
				case NKM_GAME_TYPE.NGT_GUILD_DUNGEON_BOSS:
					ClosingDelegate = delegate()
					{
						this.ExitToGuildCoop();
					};
					goto IL_57E;
				case NKM_GAME_TYPE.NGT_TRIM:
					ClosingDelegate = delegate()
					{
						if (!NKCTrimManager.ProcessTrim())
						{
							NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_TRIM, true);
						}
					};
					goto IL_57E;
				}
				ClosingDelegate = delegate()
				{
					this.TutorialExit(dungeonID);
				};
				IL_57E:
				if (flag2)
				{
					NKCLoadingScreenManager.CleanupIntroObject();
				}
				if (gameData.GetGameType() == NKM_GAME_TYPE.NGT_TUTORIAL && cNKMDungeonTempletBase != null && NKCUtil.m_sHsFirstClearDungeon.Contains(cNKMDungeonTempletBase.m_DungeonID) && NKCTutorialManager.IsPrologueDungeon(cNKMDungeonTempletBase.m_DungeonID))
				{
					if (resultData.m_BATTLE_RESULT_TYPE == BATTLE_RESULT_TYPE.BRT_WIN)
					{
						NKCUtil.m_sHsFirstClearDungeon.Remove(cNKMDungeonTempletBase.m_DungeonID);
						this.PlayTutorialNextDungeon(cNKMDungeonTempletBase.m_DungeonID);
						return;
					}
					this.PlayNextDungeon(cNKMDungeonTempletBase.m_DungeonID);
					return;
				}
				else
				{
					bool bResultAutoSkip = this.CheckAutoGame();
					if (gameData.GetGameType() == NKM_GAME_TYPE.NGT_TRIM && NKCScenManager.CurrentUserData().m_UserOption != null && NKCScenManager.CurrentUserData().m_UserOption.m_bAutoDive)
					{
						bResultAutoSkip = true;
					}
					if (resultData.m_BATTLE_RESULT_TYPE == BATTLE_RESULT_TYPE.BRT_WIN && cNKMDungeonTempletBase != null)
					{
						NKCCutScenTemplet cNKCCutScenTemplet = NKCCutScenManager.GetCutScenTemple(cNKMDungeonTempletBase.m_CutScenStrIDAfter);
						if (cNKCCutScenTemplet != null && this.CheckCutscen(gameData.m_NKM_GAME_TYPE, resultData.m_stageID, gameData.m_DungeonID))
						{
							if (flag)
							{
								NKCUICutScenPlayer.CutScenCallBack <>9__29;
								NKCUIResult.OnClose <>9__28;
								NKCScenManager.GetScenManager().Get_NKC_SCEN_GAME_RESULT().SetDoAtScenStart(delegate
								{
									NKCUIResult instance = NKCUIResult.Instance;
									NKCUIResult.BattleResultData resultData2 = resultData;
									NKCUIResult.OnClose onClose;
									if ((onClose = <>9__28) == null)
									{
										onClose = (<>9__28 = delegate()
										{
											NKC_SCEN_CUTSCEN_DUNGEON nkc_SCEN_CUTSCEN_DUNGEON = NKCScenManager.GetScenManager().Get_NKC_SCEN_CUTSCEN_DUNGEON();
											string cutScenStrID = cNKCCutScenTemplet.m_CutScenStrID;
											NKCUICutScenPlayer.CutScenCallBack callBack;
											if ((callBack = <>9__29) == null)
											{
												callBack = (<>9__29 = delegate()
												{
													ClosingDelegate();
												});
											}
											nkc_SCEN_CUTSCEN_DUNGEON.SetReservedOneCutscenType(cutScenStrID, callBack, cNKMDungeonTempletBase.m_DungeonStrID);
											NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_CUTSCENE_DUNGEON, true);
										});
									}
									instance.OpenBattleResult(resultData2, onClose, bResultAutoSkip);
								});
								NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAME_RESULT, true);
								return;
							}
							NKCScenManager.GetScenManager().Get_NKC_SCEN_CUTSCEN_DUNGEON().SetReservedOneCutscenType(cNKCCutScenTemplet.m_CutScenStrID, delegate()
							{
								ClosingDelegate();
							}, cNKMDungeonTempletBase.m_DungeonStrID);
							NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_CUTSCENE_DUNGEON, true);
							return;
						}
					}
					if (flag)
					{
						NKCScenManager.GetScenManager().Get_NKC_SCEN_GAME_RESULT().SetDoAtScenStart(delegate
						{
							NKCUIResult.Instance.OpenBattleResult(resultData, ClosingDelegate, bResultAutoSkip);
							if (resultData.m_stageID == 11214)
							{
								NKCPublisherModule.Notice.OpenPromotionalBanner(NKCPublisherModule.NKCPMNotice.eOptionalBannerPlaces.ep1act4clear, null);
							}
						});
						NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAME_RESULT, true);
						return;
					}
					ClosingDelegate();
					return;
				}
			}
		}

		// Token: 0x06004759 RID: 18265 RVA: 0x0015A638 File Offset: 0x00158838
		private bool CheckCutscen(NKM_GAME_TYPE gameType, int stageID, int dungeonID)
		{
			if (this.CheckAutoGame())
			{
				return false;
			}
			bool flag = true;
			if (NKCScenManager.CurrentUserData() != null)
			{
				flag = NKCScenManager.CurrentUserData().m_UserOption.m_bPlayCutscene;
			}
			if (gameType != NKM_GAME_TYPE.NGT_DIVE)
			{
				if (gameType != NKM_GAME_TYPE.NGT_PHASE)
				{
					if (gameType == NKM_GAME_TYPE.NGT_TRIM)
					{
						return NKCTrimManager.WillPlayTrimDungeonCutscene(NKCTrimManager.TrimModeState.trimId, dungeonID, NKCTrimManager.TrimModeState.trimLevel);
					}
					NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(dungeonID);
					if (dungeonTempletBase == null)
					{
						return false;
					}
					if (NKCUtil.m_sHsFirstClearDungeon.Contains(dungeonTempletBase.m_DungeonID) || flag)
					{
						NKCUtil.m_sHsFirstClearDungeon.Remove(dungeonTempletBase.m_DungeonID);
						return true;
					}
				}
				else if (flag || NKCPhaseManager.WasPhaseStageFirstClear(stageID))
				{
					return true;
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600475A RID: 18266 RVA: 0x0015A6D8 File Offset: 0x001588D8
		private bool GetIsWinGame()
		{
			return NKCScenManager.GetScenManager().GetGameClient().GetMyTeamData() != null && !NKCScenManager.GetScenManager().GetGameClient().IsEnemy(NKCScenManager.GetScenManager().GetGameClient().GetMyTeamData().m_eNKM_TEAM_TYPE, NKCScenManager.GetScenManager().GetGameClient().GetGameRuntimeData().m_WinTeam);
		}

		// Token: 0x0600475B RID: 18267 RVA: 0x0015A732 File Offset: 0x00158932
		private void ExitGameToHome()
		{
			this.ExitGame(NKM_SCEN_ID.NSI_HOME);
		}

		// Token: 0x0600475C RID: 18268 RVA: 0x0015A73C File Offset: 0x0015893C
		private void SetShow(bool bShow)
		{
			if (this.m_NUM_GAME.activeSelf == !bShow)
			{
				this.m_NUM_GAME.SetActive(bShow);
			}
			if (this.m_NUF_GAME_Panel.activeSelf == !bShow)
			{
				this.m_NUF_GAME_Panel.SetActive(bShow);
			}
			if (this.m_NKM_LENS_FLARE_LIST.activeSelf == !bShow)
			{
				this.m_NKM_LENS_FLARE_LIST.SetActive(bShow);
			}
		}

		// Token: 0x0600475D RID: 18269 RVA: 0x0015A7A0 File Offset: 0x001589A0
		private void PlayNextDungeon(int dungeonID)
		{
			NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY().SetDungeonInfo(NKMDungeonManager.GetDungeonTempletBase(dungeonID), DeckContents.NORMAL);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_DUNGEON_ATK_READY, true);
		}

		// Token: 0x0600475E RID: 18270 RVA: 0x0015A7C5 File Offset: 0x001589C5
		private void MoveToWarfare(string warfareStrID)
		{
			NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().SetWarfareStrID(warfareStrID);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_WARFARE_GAME, true);
		}

		// Token: 0x0600475F RID: 18271 RVA: 0x0015A7E4 File Offset: 0x001589E4
		public void PlayPracticeGame(NKMUnitData cNKMUnitData, NKM_SHORTCUT_TYPE returnUIShortcut = NKM_SHORTCUT_TYPE.SHORTCUT_NONE, string returnUIShortcutParam = "")
		{
			this.m_ePracticeReturnScenID = NKCScenManager.GetScenManager().GetNowScenID();
			this.m_ePractiveReturnShortcut = returnUIShortcut;
			this.m_strPractiveReturnShortcutParam = returnUIShortcutParam;
			NKCPacketSender.Send_NKMPacket_PRACTICE_GAME_LOAD_REQ(cNKMUnitData);
		}

		// Token: 0x06004760 RID: 18272 RVA: 0x0015A80C File Offset: 0x00158A0C
		public void EndPracticeGame()
		{
			NKCLocalServerManager.MakeNewLocalGame();
			if (this.m_ePractiveReturnShortcut == NKM_SHORTCUT_TYPE.SHORTCUT_NONE)
			{
				NKCScenManager.GetScenManager().ScenChangeFade(this.m_ePracticeReturnScenID, true);
			}
			else
			{
				NKCContentManager.MoveToShortCut(this.m_ePractiveReturnShortcut, this.m_strPractiveReturnShortcutParam, true);
			}
			this.m_ePractiveReturnShortcut = NKM_SHORTCUT_TYPE.SHORTCUT_NONE;
			this.m_strPractiveReturnShortcutParam = "";
		}

		// Token: 0x06004761 RID: 18273 RVA: 0x0015A860 File Offset: 0x00158A60
		private void PlayTutorialNextDungeon(int clearedDungeonID)
		{
			NKC_SCEN_GAME.<>c__DisplayClass64_0 CS$<>8__locals1 = new NKC_SCEN_GAME.<>c__DisplayClass64_0();
			CS$<>8__locals1.<>4__this = this;
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(clearedDungeonID);
			CS$<>8__locals1.cNKCCutScenTemplet = NKCCutScenManager.GetCutScenTemple(dungeonTempletBase.m_CutScenStrIDAfter);
			if (CS$<>8__locals1.cNKCCutScenTemplet != null)
			{
				switch (dungeonTempletBase.m_DungeonID)
				{
				case 1004:
					CS$<>8__locals1.<PlayTutorialNextDungeon>g__PlayDungeonAfterCutscene|0(1005);
					return;
				case 1005:
					CS$<>8__locals1.<PlayTutorialNextDungeon>g__PlayDungeonAfterCutscene|0(1006);
					return;
				}
				CS$<>8__locals1.<PlayTutorialNextDungeon>g__PlayDungeonAfterCutscene|0(1007);
				return;
			}
			switch (dungeonTempletBase.m_DungeonID)
			{
			case 1004:
				this.PlayNextDungeon(1005);
				return;
			case 1005:
				this.PlayNextDungeon(1006);
				return;
			}
			this.PlayNextDungeon(1007);
		}

		// Token: 0x06004762 RID: 18274 RVA: 0x0015A923 File Offset: 0x00158B23
		public void OnRecv(NKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT cPacket_NPT_GAME_SYNC_DATA_PACK_NOT)
		{
			if (NKCReplayMgr.IsRecording())
			{
				NKCScenManager.GetScenManager().GetNKCReplayMgr().FillReplayData(cPacket_NPT_GAME_SYNC_DATA_PACK_NOT);
			}
			NKCScenManager.GetScenManager().GetGameClient().OnRecv(cPacket_NPT_GAME_SYNC_DATA_PACK_NOT);
		}

		// Token: 0x06004763 RID: 18275 RVA: 0x0015A94C File Offset: 0x00158B4C
		public void OnRecv(NKMPacket_GAME_PAUSE_ACK cNKMPacket_GAME_PAUSE_ACK)
		{
			NKCScenManager.GetScenManager().GetGameClient().OnRecv(cNKMPacket_GAME_PAUSE_ACK);
		}

		// Token: 0x06004764 RID: 18276 RVA: 0x0015A95E File Offset: 0x00158B5E
		public void OnRecv(NKMPacket_GAME_SPEED_2X_ACK cNKMPacket_GAME_SPEED_2X_ACK)
		{
			NKCScenManager.GetScenManager().GetGameClient().OnRecv(cNKMPacket_GAME_SPEED_2X_ACK);
		}

		// Token: 0x06004765 RID: 18277 RVA: 0x0015A970 File Offset: 0x00158B70
		public void OnRecv(NKMPacket_GAME_AUTO_SKILL_CHANGE_ACK cNKMPacket_GAME_AUTO_SKILL_CHANGE_ACK)
		{
			NKCScenManager.GetScenManager().GetGameClient().OnRecv(cNKMPacket_GAME_AUTO_SKILL_CHANGE_ACK);
		}

		// Token: 0x06004766 RID: 18278 RVA: 0x0015A982 File Offset: 0x00158B82
		public void OnRecv(NKMPacket_GAME_USE_UNIT_SKILL_ACK cNKMPacket_GAME_USE_UNIT_SKILL_ACK)
		{
			NKCScenManager.GetScenManager().GetGameClient().OnRecv(cNKMPacket_GAME_USE_UNIT_SKILL_ACK);
		}

		// Token: 0x06004767 RID: 18279 RVA: 0x0015A994 File Offset: 0x00158B94
		public void OnRecv(NKMPacket_GAME_DEV_COOL_TIME_RESET_ACK cNKMPacket_GAME_DEV_COOL_TIME_RESET_ACK)
		{
		}

		// Token: 0x06004768 RID: 18280 RVA: 0x0015A996 File Offset: 0x00158B96
		public void OnRecv(NKMPacket_GAME_DEV_RESPAWN_ACK cNKMPacket_GAME_DEV_RESPAWN_ACK)
		{
		}

		// Token: 0x06004769 RID: 18281 RVA: 0x0015A998 File Offset: 0x00158B98
		public void OnRecv(NKMPacket_GAME_SHIP_SKILL_ACK cNKMPacket_GAME_SHIP_SKILL_ACK)
		{
			NKCScenManager.GetScenManager().GetGameClient().OnRecv(cNKMPacket_GAME_SHIP_SKILL_ACK);
		}

		// Token: 0x0600476A RID: 18282 RVA: 0x0015A9AA File Offset: 0x00158BAA
		public void OnRecv(NKMPacket_GAME_TACTICAL_COMMAND_ACK cNKMPacket_GAME_TACTICAL_COMMAND_ACK)
		{
			NKCScenManager.GetScenManager().GetGameClient().OnRecv(cNKMPacket_GAME_TACTICAL_COMMAND_ACK);
		}

		// Token: 0x0600476B RID: 18283 RVA: 0x0015A9BC File Offset: 0x00158BBC
		public void OnRecv(NKMPacket_GAME_RESPAWN_ACK cPacket_GAME_RESPAWN_ACK)
		{
			NKCScenManager.GetScenManager().GetGameClient().OnRecv(cPacket_GAME_RESPAWN_ACK);
		}

		// Token: 0x0600476C RID: 18284 RVA: 0x0015A9CE File Offset: 0x00158BCE
		public void OnRecv(NKMPacket_GAME_UNIT_RETREAT_ACK cPacket_GAME_UNIT_RETREAT_ACK)
		{
			NKCScenManager.GetScenManager().GetGameClient().OnRecv(cPacket_GAME_UNIT_RETREAT_ACK);
		}

		// Token: 0x0600476D RID: 18285 RVA: 0x0015A9E0 File Offset: 0x00158BE0
		public void OnRecv(NKMPacket_GAME_OPTION_CHANGE_ACK cNKMPacket_GAME_OPTION_CHANGE_ACK)
		{
			NKCScenManager.GetScenManager().GetGameClient().OnRecv(cNKMPacket_GAME_OPTION_CHANGE_ACK);
		}

		// Token: 0x0600476E RID: 18286 RVA: 0x0015A9F2 File Offset: 0x00158BF2
		public void OnRecv(NKMPacket_GAME_AUTO_RESPAWN_ACK cPacket_GAME_AUTO_RESPAWN_ACK)
		{
			NKCScenManager.GetScenManager().GetGameClient().OnRecv(cPacket_GAME_AUTO_RESPAWN_ACK);
		}

		// Token: 0x040037D1 RID: 14289
		private GameObject m_NUM_GAME;

		// Token: 0x040037D2 RID: 14290
		private GameObject m_NUF_GAME_Panel;

		// Token: 0x040037D3 RID: 14291
		private GameObject m_NUM_GAME_PREFAB;

		// Token: 0x040037D4 RID: 14292
		private GameObject m_NKM_LENS_FLARE_LIST;

		// Token: 0x040037D5 RID: 14293
		private NKC_SCEN_GAME_UI_DATA m_NKC_SCEN_GAME_UI_DATA = new NKC_SCEN_GAME_UI_DATA();

		// Token: 0x040037D6 RID: 14294
		private bool m_bWaitingEnemyGameLoading;

		// Token: 0x040037D7 RID: 14295
		private NKCUIResult.BattleResultData GameResultUIData;

		// Token: 0x040037D8 RID: 14296
		public float m_UpdateTime;

		// Token: 0x040037D9 RID: 14297
		private NKCUIGame m_NKCUIGame;

		// Token: 0x040037DA RID: 14298
		private string m_DungeonStrIDForLocal = "NKM_DUNGEON_TEST";

		// Token: 0x040037DB RID: 14299
		private NKCUIGameToastMSG m_NKCUIGameToastMSG = new NKCUIGameToastMSG();

		// Token: 0x040037DC RID: 14300
		private float m_fNextCancelPauseCheckTime;

		// Token: 0x040037DD RID: 14301
		private const float CANCEL_PAUSE_CHECK_REFRESH_TIME_ON_POWER_SAVE_MODE = 5f;

		// Token: 0x040037DE RID: 14302
		private NKM_SCEN_ID m_ePracticeReturnScenID;

		// Token: 0x040037DF RID: 14303
		private NKM_SHORTCUT_TYPE m_ePractiveReturnShortcut;

		// Token: 0x040037E0 RID: 14304
		private string m_strPractiveReturnShortcutParam;
	}
}
