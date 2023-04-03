using System;
using System.Collections.Generic;
using System.Text;
using ClientPacket.Game;
using ClientPacket.User;
using Cs.Logging;
using Cs.Protocol;
using NKC.Loading;
using NKC.PacketHandler;
using NKC.UI;
using NKC.UI.HUD;
using NKC.UI.Option;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x0200067D RID: 1661
	public class NKCGameClient : NKMGame
	{
		// Token: 0x060034FD RID: 13565 RVA: 0x0010B899 File Offset: 0x00109A99
		public bool GetIntrude()
		{
			return this.m_bIntrude;
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x0010B8A1 File Offset: 0x00109AA1
		public bool GetLoadComplete()
		{
			return this.m_bLoadComplete;
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x0010B8A9 File Offset: 0x00109AA9
		public NKMGameData GetGameDataDummy()
		{
			return this.m_NKMGameDataDummy;
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x0010B8B1 File Offset: 0x00109AB1
		public NKCEffectManager GetNKCEffectManager()
		{
			return this.m_NKCEffectManager;
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x0010B8B9 File Offset: 0x00109AB9
		public void SetCameraNormalTackingWaitTime(float fCameraNormalTackingWaitTime)
		{
			this.m_fCameraNormalTackingWaitTime = fCameraNormalTackingWaitTime;
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x0010B8C2 File Offset: 0x00109AC2
		public bool GetShipSkillDrag()
		{
			return this.m_bShipSkillDrag;
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x0010B8CA File Offset: 0x00109ACA
		public float GetShipSkillDragPosX()
		{
			return this.m_fShipSkillDragPosX;
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x0010B8D2 File Offset: 0x00109AD2
		public int GetSelectShipSkillID()
		{
			return this.m_SelectShipSkillID;
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x0010B8DA File Offset: 0x00109ADA
		public NKCGameHud GetGameHud()
		{
			return this.m_NKCGameHud;
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x0010B8E2 File Offset: 0x00109AE2
		public void ClearGameHud()
		{
			if (this.m_NKCGameHud != null)
			{
				this.m_NKCGameHud.Clear();
			}
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x0010B8FD File Offset: 0x00109AFD
		public NKM_GAME_CAMERA_MODE GetCameraMode()
		{
			return this.m_NKM_GAME_CAMERA_MODE;
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x0010B905 File Offset: 0x00109B05
		public void SetCameraMode(NKM_GAME_CAMERA_MODE eNKM_GAME_CAMERA_MODE)
		{
			if (eNKM_GAME_CAMERA_MODE != NKM_GAME_CAMERA_MODE.NGCM_FOCUS_UNIT)
			{
				this.m_CameraFocusGameUnitUID = 0;
			}
			this.m_NKM_GAME_CAMERA_MODE = eNKM_GAME_CAMERA_MODE;
			if (eNKM_GAME_CAMERA_MODE != NKM_GAME_CAMERA_MODE.NGCM_DRAG)
			{
				this.m_bCameraDrag = false;
			}
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x0010B924 File Offset: 0x00109B24
		public void SetCameraFocusUnit(short gameUnitUID)
		{
			this.m_CameraFocusGameUnitUID = gameUnitUID;
		}

		// Token: 0x0600350A RID: 13578 RVA: 0x0010B92D File Offset: 0x00109B2D
		public void SetCameraFocusUnitOut(short gameUnitUID)
		{
			if (gameUnitUID == this.m_CameraFocusGameUnitUID)
			{
				this.m_CameraFocusGameUnitUID = 0;
			}
		}

		// Token: 0x0600350B RID: 13579 RVA: 0x0010B93F File Offset: 0x00109B3F
		public bool GetDeckDrag()
		{
			return this.m_bDeckDrag;
		}

		// Token: 0x0600350C RID: 13580 RVA: 0x0010B947 File Offset: 0x00109B47
		public bool GetDeckTouchDown()
		{
			return this.m_bDeckTouchDown;
		}

		// Token: 0x0600350D RID: 13581 RVA: 0x0010B94F File Offset: 0x00109B4F
		public bool GetShipSkillDeckTouchDown()
		{
			return this.m_bShipSkillDeckTouchDown;
		}

		// Token: 0x0600350E RID: 13582 RVA: 0x0010B957 File Offset: 0x00109B57
		public bool GetCameraTouchDown()
		{
			return this.m_bCameraTouchDown;
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x0010B95F File Offset: 0x00109B5F
		public bool IsShowUI()
		{
			return this.m_bShowUI;
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06003510 RID: 13584 RVA: 0x0010B967 File Offset: 0x00109B67
		// (set) Token: 0x06003511 RID: 13585 RVA: 0x0010B96F File Offset: 0x00109B6F
		public float LatencyLevel
		{
			get
			{
				return this.m_fLatencyLevel;
			}
			set
			{
				this.m_fLatencyLevel = value;
			}
		}

		// Token: 0x06003512 RID: 13586 RVA: 0x0010B978 File Offset: 0x00109B78
		public void SetSortUnitZDirty(bool bSortUnitZDirty)
		{
			this.m_bSortUnitZDirty = bSortUnitZDirty;
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06003513 RID: 13587 RVA: 0x0010B981 File Offset: 0x00109B81
		// (set) Token: 0x06003514 RID: 13588 RVA: 0x0010B989 File Offset: 0x00109B89
		public int MultiplyReward { get; private set; }

		// Token: 0x06003515 RID: 13589 RVA: 0x0010B994 File Offset: 0x00109B94
		public NKCGameClient()
		{
			this.m_NKM_GAME_CLASS_TYPE = NKM_GAME_CLASS_TYPE.NGCT_GAME_CLIENT;
			this.m_ObjectPool = new NKCObjectPool();
			this.Init();
		}

		// Token: 0x06003516 RID: 13590 RVA: 0x0010BAF8 File Offset: 0x00109CF8
		public void InitUI(NKCGameHud gameHUD)
		{
			if (gameHUD == null)
			{
				throw new Exception("HUD Prefab error!");
			}
			this.m_NKCGameHud = gameHUD;
			this.m_NKCGameHud.SetGameClient(this);
			if (NKCReplayMgr.IsPlayingReplay())
			{
				this.m_NKCGameHud.InitUI(NKCGameHud.HUDMode.Replay);
				return;
			}
			if (this.IsObserver(NKCScenManager.CurrentUserData()))
			{
				this.m_NKCGameHud.InitUI(NKCGameHud.HUDMode.Observer);
				return;
			}
			this.m_NKCGameHud.InitUI(NKCGameHud.HUDMode.Normal);
		}

		// Token: 0x06003517 RID: 13591 RVA: 0x0010BB68 File Offset: 0x00109D68
		public override void Init()
		{
			this.ObjectParentRestore();
			base.Init();
			this.m_bLoadComplete = false;
			this.SetCameraMode(NKM_GAME_CAMERA_MODE.NGCM_NORMAL_TRACKING);
			this.m_fCameraNormalTackingWaitTime = 3f;
			this.m_fCameraStopDragTime = 0f;
			this.m_CameraFocusGameUnitUID = 0;
			this.m_ShipSkillMark.Init();
			this.m_dicUnitViewer.Clear();
			this.m_NKCEffectManager.Init();
			this.m_DEManager.Init(this);
			this.m_Map.Init();
			this.m_CameraDrag.SetNowValue(0f);
			this.m_BattleCondition.Init();
			this.m_StartEffectUID = 0;
			this.m_fRemainGameTimeBeforeSync = 0f;
			this.m_linklistGameSyncData.Clear();
			this.m_CameraDragTime = 0f;
			this.m_CameraDragDist = 0f;
			this.m_CameraDragPositive = true;
			this.m_bDeckTouchDown = false;
			this.m_bShipSkillDeckTouchDown = false;
			this.m_bCameraTouchDown = false;
			this.m_bDeckDrag = false;
			this.m_bDeckSelectUnitUID = 0L;
			this.m_DeckSelectUnitTemplet = null;
			this.m_bDeckSelectPosX = 0f;
			this.m_dicRespawnCostHolder.Clear();
			this.m_dicRespawnCostHolderAssist.Clear();
			this.m_dicRespawnCostHolderTC.Clear();
			this.m_DeckDragIndex = -1;
			this.m_ShipSkillDeckDragIndex = -1;
			this.m_fLocalGameTime = 0f;
			this.m_fLastRecvTime = 0f;
			this.m_fLatencyLevel = 1f;
			this.m_fNoSyncDataTime = 0f;
			this.m_fWinLoseWaitTime = 3f;
			if (this.m_NKMGameRuntimeData != null)
			{
				this.m_NKMGameRuntimeData.m_NKM_GAME_STATE = NKM_GAME_STATE.NGS_INVALID;
			}
			this.m_NKCSkillCutInSideLeft = null;
			this.m_NKCSkillCutInSideRight = null;
			this.m_NKCSkillCutInSideRedLeft = null;
			this.m_NKCSkillCutInSideRedRight = null;
			this.m_linklistHitEffect.Clear();
			this.m_bRespawnUnit = false;
			this.m_NKC_OPEN_POPUP_TYPE_AFTER_PAUSE = NKC_OPEN_POPUP_TYPE_AFTER_PAUSE.NOPTAP_GAME_OPTION_POPUP;
			this.m_bCostSpeedVoicePlayed = false;
			this.m_lastMyShipHpRate = 0f;
			this.m_lastEnemyShipHpRate = 0f;
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x0010BD3C File Offset: 0x00109F3C
		public override void InitUnit()
		{
			foreach (KeyValuePair<short, NKMUnit> keyValuePair in this.m_dicNKMUnitPool)
			{
				NKCUnitClient nkcunitClient = (NKCUnitClient)keyValuePair.Value;
				if (nkcunitClient != null)
				{
					base.GetObjectPool().CloseObj(nkcunitClient);
				}
			}
			this.m_dicNKMUnitPool.Clear();
			for (int i = 0; i < this.m_listNKMUnit.Count; i++)
			{
				NKCUnitClient nkcunitClient2 = (NKCUnitClient)this.m_listNKMUnit[i];
				if (nkcunitClient2 != null)
				{
					base.GetObjectPool().CloseObj(nkcunitClient2);
				}
			}
			this.m_listNKMUnit.Clear();
			this.m_dicNKMUnit.Clear();
		}

		// Token: 0x06003519 RID: 13593 RVA: 0x0010BDDF File Offset: 0x00109FDF
		public NKMGameTeamData GetMyTeamData()
		{
			return base.GetGameData().GetTeamData(this.m_MyTeam);
		}

		// Token: 0x0600351A RID: 13594 RVA: 0x0010BDF2 File Offset: 0x00109FF2
		public NKMGameRuntimeTeamData GetMyRunTimeTeamData()
		{
			return base.GetGameRuntimeData().GetMyRuntimeTeamData(this.m_MyTeam);
		}

		// Token: 0x0600351B RID: 13595 RVA: 0x0010BE08 File Offset: 0x0010A008
		public void SetGameDataDummy(NKMGameData cNKMGameData, bool bIntrude = false)
		{
			this.m_NKMGameDataDummy = cNKMGameData;
			this.m_bIntrudeDummy = bIntrude;
			if (this.m_NKMGameDataDummy.m_DungeonID > 0)
			{
				NKMDungeonTemplet dungeonTemplet = NKMDungeonManager.GetDungeonTemplet(this.m_NKMGameDataDummy.m_DungeonID);
				if (dungeonTemplet != null && dungeonTemplet.m_DungeonTempletBase.m_DungeonType != NKM_DUNGEON_TYPE.NDT_WAVE)
				{
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(dungeonTemplet.m_BossUnitStrID);
					if (unitTempletBase != null)
					{
						cNKMGameData.m_NKMGameTeamDataB.m_UserNickname = unitTempletBase.GetUnitName();
					}
				}
			}
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x0010BE74 File Offset: 0x0010A074
		private void SetGameData(NKMGameData cNKMGameData, bool bIntrude = false)
		{
			if (cNKMGameData == null)
			{
				Debug.LogError("FATAL : GameData Null!!");
				return;
			}
			Debug.Log("<color=#FFFF00>NKCGameClient.SetGameData()</color>");
			this.Init();
			this.m_bIntrude = bIntrude;
			this.m_NKMGameData = cNKMGameData;
			this.m_NKMGameData.SetDungeonRespawnUnitTemplet();
			if (this.m_NKMGameData != null)
			{
				NKCAdjustManager.OnEnterDungeon(this.m_NKMGameData.m_DungeonID);
			}
			if (NKCScenManager.CurrentUserData() != null && this.m_NKMGameData.GetGameType() != NKM_GAME_TYPE.NGT_PRACTICE && !NKCReplayMgr.IsPlayingReplay() && !this.IsObserver(NKCScenManager.CurrentUserData()))
			{
				this.m_MyTeam = this.m_NKMGameData.GetTeamType(NKCScenManager.CurrentUserData().m_UserUID);
			}
			else if (NKCReplayMgr.IsPlayingReplay())
			{
				this.m_MyTeam = this.m_NKMGameData.GetTeamType(NKCScenManager.CurrentUserData().m_UserUID);
				if (this.m_MyTeam == NKM_TEAM_TYPE.NTT_INVALID)
				{
					this.m_MyTeam = NKM_TEAM_TYPE.NTT_A1;
				}
			}
			else
			{
				this.m_MyTeam = NKM_TEAM_TYPE.NTT_A1;
			}
			this.GetGameHud().CurrentViewTeamType = this.m_MyTeam;
			this.SetDefaultTacticalCommand();
			if (this.m_NKMGameData.m_DungeonID > 0)
			{
				this.m_NKMDungeonTemplet = NKMDungeonManager.GetDungeonTemplet(this.m_NKMGameData.m_DungeonID);
			}
			else
			{
				this.m_NKMDungeonTemplet = null;
			}
			base.InitDungeonEventData();
			this.ResetWinLoseWaitTime();
			Log.Info("NKCGameClient.SetGameData End", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCGameClient.cs", 533);
		}

		// Token: 0x0600351D RID: 13597 RVA: 0x0010BFB4 File Offset: 0x0010A1B4
		public bool IsObserver(NKMUserData userData)
		{
			if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_OBSERVE_MODE))
			{
				return false;
			}
			if (this.m_NKMGameDataDummy == null)
			{
				return false;
			}
			if (this.m_NKMGameDataDummy.GetGameType() != NKM_GAME_TYPE.NGT_PVP_PRIVATE)
			{
				return false;
			}
			if (userData == null)
			{
				return false;
			}
			long userUID = userData.m_UserUID;
			return userUID != this.m_NKMGameDataDummy.m_NKMGameTeamDataA.m_user_uid && userUID != this.m_NKMGameDataDummy.m_NKMGameTeamDataB.m_user_uid;
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x0010C01E File Offset: 0x0010A21E
		private void ResetWinLoseWaitTime()
		{
			if (base.GetDungeonType() == NKM_DUNGEON_TYPE.NDT_WAVE)
			{
				this.m_fWinLoseWaitTime = 0.01f;
				return;
			}
			this.m_fWinLoseWaitTime = 3f;
		}

		// Token: 0x0600351F RID: 13599 RVA: 0x0010C040 File Offset: 0x0010A240
		public void SetGameRuntimeData(NKMGameRuntimeData cNKMGameRuntimeData)
		{
			this.m_NKMGameRuntimeData = cNKMGameRuntimeData;
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x0010C04C File Offset: 0x0010A24C
		public void LoadGame()
		{
			Debug.Log("<color=#FFFF00>NKCGameClient.LoadGame()</color>");
			NKCUIManager.m_NUF_GAME_TOUCH_OBJECT.SetActive(true);
			this.SetGameData(this.m_NKMGameDataDummy, this.m_bIntrudeDummy);
			Log.Info("NKCGameClient.LoadGame SetGameData", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCGameClient.cs", 587);
			this.LoadGameMap();
			Log.Info("NKCGameClient.LoadGame LoadGameMap", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCGameClient.cs", 590);
			this.LoadGameUnit();
			Log.Info("NKCGameClient.LoadGame LoadGameUnit", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCGameClient.cs", 593);
			this.LoadGameUI();
			Log.Info("NKCGameClient.LoadGame LoadGameUI", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCGameClient.cs", 596);
			this.LoadGameEffectInst();
			Log.Info("NKCGameClient.LoadGame LoadGameEffectInst", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCGameClient.cs", 599);
			this.LoadGameBattleCondition();
			Log.Info("NKCGameClient.LoadGame LoadGameBattleCondition", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCGameClient.cs", 602);
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x0010C118 File Offset: 0x0010A318
		private bool LoadGameMap()
		{
			NKMMapTemplet mapTempletByID = NKMMapManager.GetMapTempletByID(this.m_NKMGameData.m_MapID);
			if (mapTempletByID == null)
			{
				return false;
			}
			this.m_NKMMapTemplet = mapTempletByID;
			this.m_Map.Load(this.m_NKMGameData.m_MapID, true);
			return true;
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x0010C15A File Offset: 0x0010A35A
		private void LoadGameUnit()
		{
			this.LoadOperator(this.m_NKMGameData.m_NKMGameTeamDataA);
			this.LoadOperator(this.m_NKMGameData.m_NKMGameTeamDataB);
			base.CreatePoolUnit(true);
			base.CreateDynaminRespawnPoolUnit(true);
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x0010C18C File Offset: 0x0010A38C
		private void LoadOperator(NKMGameTeamData cNKMGameTeamData)
		{
			if (cNKMGameTeamData == null)
			{
				return;
			}
			if (cNKMGameTeamData.m_Operator == null)
			{
				return;
			}
			string finalOperatorCutinEffectName = this.GetFinalOperatorCutinEffectName(cNKMGameTeamData.m_Operator.id);
			NKCASEffect item = (NKCASEffect)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASEffect, finalOperatorCutinEffectName, finalOperatorCutinEffectName, true);
			this.m_listEffectLoadTemp.Add(item);
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x0010C1E0 File Offset: 0x0010A3E0
		private string GetFinalOperatorCutinEffectName(int operatorID)
		{
			string unitStrID = NKMUnitManager.GetUnitStrID(operatorID);
			return "AB_FX_SKILL_CUTIN_" + unitStrID;
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x0010C200 File Offset: 0x0010A400
		private void LoadGameEffectInst()
		{
			NKCASEffect item;
			for (int i = 0; i < 20; i++)
			{
				item = (NKCASEffect)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASEffect, "AB_FX_DAMAGE_TEXT", "AB_FX_DAMAGE_TEXT", true);
				this.m_listEffectLoadTemp.Add(item);
			}
			item = (NKCASEffect)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASEffect, "AB_FX_COOLTIME", "AB_FX_COOLTIME", true);
			this.m_listEffectLoadTemp.Add(item);
			item = (NKCASEffect)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASEffect, "AB_FX_COST", "AB_FX_COST", true);
			this.m_listEffectLoadTemp.Add(item);
			item = (NKCASEffect)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASEffect, "AB_FX_EXCLAMATION_MARK", "AB_FX_EXCLAMATION_MARK", true);
			this.m_listEffectLoadTemp.Add(item);
			item = (NKCASEffect)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASEffect, "AB_FX_CMN_INSTANTKILL", "AB_FX_CMN_INSTANTKILL", true);
			this.m_listEffectLoadTemp.Add(item);
			item = (NKCASEffect)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASEffect, "AB_FX_CMN_DISPEL", "AB_FX_CMN_DISPEL", true);
			this.m_listEffectLoadTemp.Add(item);
			foreach (NKMUnitStatusTemplet nkmunitStatusTemplet in NKMTempletContainer<NKMUnitStatusTemplet>.Values)
			{
				if (!string.IsNullOrEmpty(nkmunitStatusTemplet.m_StatusEffectName))
				{
					NKMAssetName nkmassetName = NKMAssetName.ParseBundleName(nkmunitStatusTemplet.m_StatusEffectName, nkmunitStatusTemplet.m_StatusEffectName);
					item = (NKCASEffect)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASEffect, nkmassetName.m_BundleName, nkmassetName.m_AssetName, true);
					this.m_listEffectLoadTemp.Add(item);
				}
			}
			this.m_NKCSkillCutInSideLeft = new NKCSkillCutInSide();
			this.m_NKCSkillCutInSideLeft.Load(this.m_listEffectLoadTemp, "AB_FX_SKILL_CUTIN_SIDE_RENDER_BLUE_OTHER", "AB_FX_SKILL_CUTIN_SIDE_BLUE_OTHER");
			this.m_NKCSkillCutInSideRight = new NKCSkillCutInSide();
			this.m_NKCSkillCutInSideRight.Load(this.m_listEffectLoadTemp, "AB_FX_SKILL_CUTIN_SIDE_RENDER_BLUE", "AB_FX_SKILL_CUTIN_SIDE_BLUE");
			this.m_NKCSkillCutInSideRedLeft = new NKCSkillCutInSide();
			this.m_NKCSkillCutInSideRedLeft.Load(this.m_listEffectLoadTemp, "AB_FX_SKILL_CUTIN_SIDE_RENDER_RED_OTHER", "AB_FX_SKILL_CUTIN_SIDE_RED_OTHER");
			this.m_NKCSkillCutInSideRedRight = new NKCSkillCutInSide();
			this.m_NKCSkillCutInSideRedRight.Load(this.m_listEffectLoadTemp, "AB_FX_SKILL_CUTIN_SIDE_RENDER_RED", "AB_FX_SKILL_CUTIN_SIDE_RED");
		}

		// Token: 0x06003526 RID: 13606 RVA: 0x0010C454 File Offset: 0x0010A654
		private void LoadGameBattleCondition()
		{
			List<int> list = new List<int>();
			foreach (int num in base.GetGameData().m_BattleConditionIDs.Keys)
			{
				NKMBattleConditionTemplet templetByID = NKMBattleConditionManager.GetTempletByID(num);
				if (templetByID != null && templetByID.UseContentsType == NKMBattleConditionTemplet.USE_CONTENT_TYPE.UCT_BATTLE_CONDITION)
				{
					list.Add(num);
				}
			}
			this.m_BattleCondition.Load(list);
			this.m_StartEffectUID = 0;
		}

		// Token: 0x06003527 RID: 13607 RVA: 0x0010C4DC File Offset: 0x0010A6DC
		private void LoadGameCompleteEffectInst()
		{
			for (int i = 0; i < this.m_listEffectLoadTemp.Count; i++)
			{
				NKCASEffect nkcaseffect = this.m_listEffectLoadTemp[i];
				if (nkcaseffect != null)
				{
					NKCScenManager.GetScenManager().GetObjectPool().CloseObj(nkcaseffect);
				}
			}
			this.m_listEffectLoadTemp.Clear();
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x0010C52C File Offset: 0x0010A72C
		private bool LoadGameUI()
		{
			this.GetGameHud().LoadUI(this.m_NKMGameData);
			this.m_listLoadAssetResourceDataTemp.Add(NKCAssetResourceManager.OpenResource<GameObject>("AB_FX_UI_START_WIN_LOSE", "AB_FX_UI_START_WIN_LOSE", true, null));
			this.m_listLoadAssetResourceDataTemp.Add(NKCAssetResourceManager.OpenResource<GameObject>("AB_FX_UI_WARNING", "AB_FX_UI_WARNING", true, null));
			this.m_listLoadAssetResourceDataTemp.Add(NKCAssetResourceManager.OpenResource<GameObject>("AB_FX_UI_RESOURCE_DOUBLE", "AB_FX_UI_RESOURCE_DOUBLE", true, null));
			NKMAssetName introName = NKCLoadingScreenManager.GetIntroName(this.m_NKMGameData);
			if (introName != null)
			{
				this.m_listLoadAssetResourceDataTemp.Add(NKCAssetResourceManager.OpenResource<GameObject>(introName, true));
			}
			NKMAssetName outroName = NKCLoadingScreenManager.GetOutroName(this.m_NKMGameData);
			if (outroName != null)
			{
				this.m_listLoadAssetResourceDataTemp.Add(NKCAssetResourceManager.OpenResource<GameObject>(outroName, true));
			}
			return true;
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x0010C5E4 File Offset: 0x0010A7E4
		protected override NKMUnit CreateNewUnitObj()
		{
			NKMUnit nkmunit = base.CreateNewUnitObj();
			if (nkmunit == null && this.m_NKM_GAME_CLASS_TYPE == NKM_GAME_CLASS_TYPE.NGCT_GAME_CLIENT)
			{
				nkmunit = (NKMUnit)base.GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCUnitClient, "", "", false);
			}
			return nkmunit;
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x0010C624 File Offset: 0x0010A824
		public void ClearResource()
		{
			for (int i = 0; i < this.m_listLoadAssetResourceData.Count; i++)
			{
				NKCAssetResourceManager.CloseResource(this.m_listLoadAssetResourceData[i]);
			}
			this.m_listLoadAssetResourceData.Clear();
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x0010C664 File Offset: 0x0010A864
		public void LoadGameComplete()
		{
			this.ClearResource();
			for (int i = 0; i < this.m_listLoadAssetResourceDataTemp.Count; i++)
			{
				this.m_listLoadAssetResourceData.Add(this.m_listLoadAssetResourceDataTemp[i]);
			}
			this.m_listLoadAssetResourceDataTemp.Clear();
			if (NKCScenManager.GetScenManager().m_NKCMemoryCleaner != null)
			{
				NKCScenManager.GetScenManager().m_NKCMemoryCleaner.Clean(null, NKCUIManager.eUIUnloadFlag.ON_PLAY_GAME, true);
			}
			base.LoadCompleteUnit();
			this.LoadGameCompleteEffectInst();
			this.GetGameHud().LoadComplete(this.m_NKMGameData);
			this.m_DEManager.Init(this);
			this.m_Map.LoadComplete();
			this.m_BattleCondition.LoadComplete();
			this.SetCamera();
			NKCScenManager.GetScenManager().PreloadSprite("ab_fx_tx_cmn_atlas", "CMN_GLOW_00");
			NKCScenManager.GetScenManager().PreloadTexture("ab_fx_tx_mask", "MASK_RECT_05");
			NKCScenManager.GetScenManager().PreloadTexture("ab_fx_tx_ptcs", "PTCS_TRIANGLE_00");
			NKCScenManager.GetScenManager().PreloadSprite("ab_fx_tx_sprite", "NK_CYBER_DECO_B");
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x0010C76C File Offset: 0x0010A96C
		private string FindMusic()
		{
			if (base.GetGameRuntimeData().m_lstPermanentDungeonEvent != null)
			{
				for (int i = base.GetGameRuntimeData().m_lstPermanentDungeonEvent.Count - 1; i >= 0; i--)
				{
					NKMGameSyncData_DungeonEvent nkmgameSyncData_DungeonEvent = base.GetGameRuntimeData().m_lstPermanentDungeonEvent[i];
					if (nkmgameSyncData_DungeonEvent.m_eEventActionType == NKM_EVENT_ACTION_TYPE.CHANGE_BGM || nkmgameSyncData_DungeonEvent.m_eEventActionType == NKM_EVENT_ACTION_TYPE.CHANGE_BGM_TRACK)
					{
						return nkmgameSyncData_DungeonEvent.m_strEventActionValue;
					}
				}
			}
			if (NKMGame.IsPVP(this.m_NKMGameData.GetGameType()))
			{
				return this.m_NKMMapTemplet.m_PVPMusicName;
			}
			if (this.m_NKMDungeonTemplet != null && this.m_NKMDungeonTemplet.m_DungeonTempletBase != null)
			{
				if (this.m_NKMDungeonTemplet.m_DungeonTempletBase.m_MusicName.Length >= 1)
				{
					return this.m_NKMDungeonTemplet.m_DungeonTempletBase.m_MusicName;
				}
				string cutScenStrIDBefore = this.m_NKMDungeonTemplet.m_DungeonTempletBase.m_CutScenStrIDBefore;
				if (!string.IsNullOrEmpty(cutScenStrIDBefore))
				{
					NKCCutScenTemplet cutScenTemple = NKCCutScenManager.GetCutScenTemple(cutScenStrIDBefore);
					if (cutScenTemple != null)
					{
						string lastMusicAssetName = cutScenTemple.GetLastMusicAssetName();
						if (!string.IsNullOrEmpty(lastMusicAssetName) && lastMusicAssetName.Length > 0)
						{
							return lastMusicAssetName;
						}
					}
				}
			}
			if (this.m_NKMGameData.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_WARFARE)
			{
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_NKMGameData.m_WarfareID);
				if (nkmwarfareTemplet != null)
				{
					if (nkmwarfareTemplet.m_WarfareBGM.Length > 0)
					{
						return nkmwarfareTemplet.m_WarfareBGM;
					}
					string cutScenStrIDBefore = nkmwarfareTemplet.m_CutScenStrIDBefore;
					if (!string.IsNullOrEmpty(cutScenStrIDBefore))
					{
						NKCCutScenTemplet cutScenTemple = NKCCutScenManager.GetCutScenTemple(cutScenStrIDBefore);
						if (cutScenTemple != null)
						{
							string lastMusicAssetName2 = cutScenTemple.GetLastMusicAssetName();
							if (!string.IsNullOrEmpty(lastMusicAssetName2) && lastMusicAssetName2.Length > 0)
							{
								return lastMusicAssetName2;
							}
						}
					}
				}
			}
			return this.m_NKMMapTemplet.m_MusicName;
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x0010C8F4 File Offset: 0x0010AAF4
		public void PlayMusic()
		{
			string text = this.FindMusic();
			if (text.Length < 1)
			{
				return;
			}
			if (!NKCSoundManager.IsSameMusic(text))
			{
				NKCSoundManager.FadeOutMusic();
			}
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x0010C920 File Offset: 0x0010AB20
		public override void StartGame(bool bIntrude)
		{
			base.StartGame(bIntrude);
			if (!base.CanUseAutoRespawn(NKCScenManager.GetScenManager().GetMyUserData()))
			{
				NKMGameRuntimeTeamData myRunTimeTeamData = this.GetMyRunTimeTeamData();
				if (myRunTimeTeamData != null)
				{
					myRunTimeTeamData.m_bAutoRespawn = false;
				}
			}
			this.GetGameHud().StartGame(this.m_NKMGameRuntimeData);
			this.PlayMusic();
			this.SetShowUI(true, true);
			if (this.m_NKMGameRuntimeData.m_bPause)
			{
				if (NKCGameEventManager.IsPauseEventPlaying())
				{
					NKCGameEventManager.ResumeEvent();
				}
				else
				{
					if (NKCGameEventManager.IsEventPlaying())
					{
						NKCGameEventManager.ResumeEvent();
					}
					if (!this.m_NKMGameData.m_bLocal && this.m_NKMGameData.IsPVE())
					{
						if (NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing())
						{
							this.CancelPause();
						}
						else
						{
							this.GetGameHud().OpenPause(new NKCGameHudPause.dOnClickContinue(this.CancelPause));
						}
					}
				}
			}
			if (!this.GetIntrude())
			{
				this.PlayGameIntro();
				if (this.GetMyTeamData() != null)
				{
					NKCUIVoiceManager.PlayOperatorVoice(VOICE_TYPE.VT_FIGHT_START, this.GetMyTeamData().m_Operator, false);
				}
			}
			else
			{
				NKCLoadingScreenManager.CleanupIntroObject();
			}
			Debug.LogFormat("StartGame: {0}", new object[]
			{
				this.m_NKMGameData.m_NKM_GAME_TYPE
			});
			if (this.m_NKMGameData.m_WarfareID > 0)
			{
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_NKMGameData.m_WarfareID);
				if (nkmwarfareTemplet != null)
				{
					Debug.LogFormat("m_WarfareID: {0}", new object[]
					{
						nkmwarfareTemplet.m_WarfareStrID
					});
				}
			}
			if (this.m_NKMGameData.m_DungeonID > 0)
			{
				NKMDungeonTemplet dungeonTemplet = NKMDungeonManager.GetDungeonTemplet(this.m_NKMGameData.m_DungeonID);
				if (dungeonTemplet != null)
				{
					Debug.LogFormat("m_DungeonID: {0}", new object[]
					{
						dungeonTemplet.m_DungeonTempletBase.m_DungeonStrID
					});
				}
			}
			if (base.IsATeam(this.m_MyTeam))
			{
				this.m_Map.SetRespawnValidLandFactor(this.m_fRespawnValidLandTeamA, false);
				return;
			}
			if (base.IsBTeam(this.m_MyTeam))
			{
				this.m_Map.SetRespawnValidLandFactor(this.m_fRespawnValidLandTeamB, false);
			}
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x0010CAF8 File Offset: 0x0010ACF8
		public override void EndGame()
		{
			NKCCamera.BattleEnd();
			base.EndGame();
			if (NKCUIManager.m_NUF_GAME_TOUCH_OBJECT != null)
			{
				NKCUIManager.m_NUF_GAME_TOUCH_OBJECT.SetActive(false);
			}
			if (this.GetGameHud() != null)
			{
				this.GetGameHud().EndGame();
			}
			if (this.m_Map != null)
			{
				this.m_Map.Init();
			}
			this.m_linklistHitEffect.Clear();
			if (this.m_NKMGameRuntimeData != null)
			{
				this.m_NKMGameRuntimeData.m_NKM_GAME_STATE = NKM_GAME_STATE.NGS_INVALID;
				this.m_NKMGameRuntimeData.m_bPause = false;
			}
			this.m_BattleCondition.Close();
			this.UnloadUI();
			NKCSoundManager.StopAllSound();
			if (NKCScenManager.GetScenManager().m_NKCMemoryCleaner != null)
			{
				NKCScenManager.GetScenManager().m_NKCMemoryCleaner.Clean(null, NKCUIManager.eUIUnloadFlag.ON_PLAY_GAME, true);
			}
			NKCGameEventManager.ClearEvent();
			this.m_bTutorialGameReRespawnAllowed = false;
		}

		// Token: 0x06003530 RID: 13616 RVA: 0x0010CBC8 File Offset: 0x0010ADC8
		private void SetNetworkLatencyLevel(float fLatencyLevel)
		{
			this.m_fLatencyLevel = fLatencyLevel;
			if (this.m_fLatencyLevel < 1f)
			{
				this.m_fLatencyLevel = 1f;
			}
			if (this.m_fLatencyLevel > 10f)
			{
				this.m_fLatencyLevel = 10f;
			}
			this.GetGameHud().SetNetworkLatencyLevel((int)(this.m_fLatencyLevel - 2f));
		}

		// Token: 0x06003531 RID: 13617 RVA: 0x0010CC24 File Offset: 0x0010AE24
		public void UnloadUI()
		{
			this.ObjectParentWait();
			this.ClearGameHud();
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x0010CC34 File Offset: 0x0010AE34
		public void ObjectParentWait()
		{
			foreach (KeyValuePair<short, NKMUnit> keyValuePair in this.m_dicNKMUnitPool)
			{
				NKCUnitClient nkcunitClient = (NKCUnitClient)keyValuePair.Value;
				if (nkcunitClient != null)
				{
					nkcunitClient.ObjectParentWait();
				}
			}
			for (int i = 0; i < this.m_listNKMUnit.Count; i++)
			{
				NKCUnitClient nkcunitClient2 = (NKCUnitClient)this.m_listNKMUnit[i];
				if (nkcunitClient2 != null)
				{
					nkcunitClient2.ObjectParentWait();
				}
			}
			this.GetNKCEffectManager().ObjectParentWait();
		}

		// Token: 0x06003533 RID: 13619 RVA: 0x0010CCB8 File Offset: 0x0010AEB8
		public void ObjectParentRestore()
		{
			foreach (KeyValuePair<short, NKMUnit> keyValuePair in this.m_dicNKMUnitPool)
			{
				NKCUnitClient nkcunitClient = (NKCUnitClient)keyValuePair.Value;
				if (nkcunitClient != null)
				{
					nkcunitClient.ObjectParentRestore();
				}
			}
			for (int i = 0; i < this.m_listNKMUnit.Count; i++)
			{
				NKCUnitClient nkcunitClient2 = (NKCUnitClient)this.m_listNKMUnit[i];
				if (nkcunitClient2 != null)
				{
					nkcunitClient2.ObjectParentRestore();
				}
			}
			this.GetNKCEffectManager().ObjectParentRestore();
		}

		// Token: 0x06003534 RID: 13620 RVA: 0x0010CD3C File Offset: 0x0010AF3C
		public void SetCamera()
		{
			NKCCamera.InitBattle(this.m_NKMMapTemplet.m_fCamMinX, this.m_NKMMapTemplet.m_fCamMaxX, this.m_NKMMapTemplet.m_fCamMinY, this.m_NKMMapTemplet.m_fCamMaxY, this.m_NKMMapTemplet.m_fCamSize, this.m_NKMMapTemplet.m_fCamSizeMax);
			NKCCamera.EnableBloom(false);
			NKCCamera.SetPos(this.m_NKMMapTemplet.m_fCamMinX, 0f, -1000f, true, true);
			this.SetCameraMode(NKM_GAME_CAMERA_MODE.NGCM_NORMAL_TRACKING);
			this.m_fCameraNormalTackingWaitTime = 3f;
			this.m_fCameraStopDragTime = 0f;
			this.m_CameraDrag.SetNowValue(0f);
		}

		// Token: 0x06003535 RID: 13621 RVA: 0x0010CDE0 File Offset: 0x0010AFE0
		public void KeyDownF6()
		{
			List<string> list = new List<string>();
			NKMUnitManager.ReloadFromLUA();
			list.Clear();
			list.Add("LUA_DAMAGE_TEMPLET");
			list.Add("LUA_DAMAGE_TEMPLET2");
			list.Add("LUA_DAMAGE_TEMPLET3");
			list.Add("LUA_DAMAGE_TEMPLET4");
			list.Add("LUA_DAMAGE_TEMPLET5");
			list.Add("LUA_DAMAGE_TEMPLET6");
			NKMDamageManager.LoadFromLUA(new string[]
			{
				"LUA_DAMAGE_TEMPLET_BASE",
				"LUA_DAMAGE_TEMPLET_BASE2",
				"LUA_DAMAGE_TEMPLET_BASE3",
				"LUA_DAMAGE_TEMPLET_BASE4",
				"LUA_DAMAGE_TEMPLET_BASE5",
				"LUA_DAMAGE_TEMPLET_BASE6"
			}, list);
			NKMBuffManager.LoadFromLUA();
			NKMBuffTemplet.ParseAllSkinDic();
			list.Clear();
			list.Add("LUA_DAMAGE_EFFECT_TEMPLET");
			list.Add("LUA_DAMAGE_EFFECT_TEMPLET2");
			list.Add("LUA_DAMAGE_EFFECT_TEMPLET3");
			list.Add("LUA_DAMAGE_EFFECT_TEMPLET4");
			list.Add("LUA_DAMAGE_EFFECT_TEMPLET5");
			list.Add("LUA_DAMAGE_EFFECT_TEMPLET6");
			NKMDETempletManager.LoadFromLUA(list, true);
			NKMCommonUnitEvent.LoadFromLUA("LUA_COMMON_UNIT_EVENT_HEAL", true);
			NKCLocalServerManager.GetGameServerLocal().GameEndFlush();
			NKMMapManager.LoadFromLUA("LUA_MAP_TEMPLET", false);
		}

		// Token: 0x06003536 RID: 13622 RVA: 0x0010CEFC File Offset: 0x0010B0FC
		public void Update(float deltaTime)
		{
			if (this.m_NKMGameRuntimeData == null)
			{
				return;
			}
			switch (this.m_NKMGameRuntimeData.m_NKM_GAME_SPEED_TYPE)
			{
			case NKM_GAME_SPEED_TYPE.NGST_1:
				this.UpdateInner(deltaTime * 1.1f);
				return;
			case NKM_GAME_SPEED_TYPE.NGST_2:
				this.UpdateInner(deltaTime * 1.5f);
				return;
			case NKM_GAME_SPEED_TYPE.NGST_3:
				this.UpdateInner(deltaTime * 1.1f);
				this.UpdateInner(deltaTime * 1.1f);
				return;
			case NKM_GAME_SPEED_TYPE.NGST_05:
				this.UpdateInner(deltaTime * 0.6f);
				return;
			case NKM_GAME_SPEED_TYPE.NGST_10:
				this.UpdateInner(deltaTime * 1.1f);
				this.UpdateInner(deltaTime * 1.1f);
				this.UpdateInner(deltaTime * 1.1f);
				this.UpdateInner(deltaTime * 1.1f);
				this.UpdateInner(deltaTime * 1.1f);
				this.UpdateInner(deltaTime * 1.1f);
				this.UpdateInner(deltaTime * 1.1f);
				this.UpdateInner(deltaTime * 1.1f);
				this.UpdateInner(deltaTime * 1.1f);
				this.UpdateInner(deltaTime * 1.1f);
				return;
			default:
				this.UpdateInner(deltaTime);
				return;
			}
		}

		// Token: 0x06003537 RID: 13623 RVA: 0x0010D00C File Offset: 0x0010B20C
		private void PlayOperatorVoiceMyTeam(VOICE_TYPE eVOICE_TYPE)
		{
			if (this.GetMyTeamData() != null)
			{
				NKCUIVoiceManager.PlayOperatorVoice(eVOICE_TYPE, this.GetMyTeamData().m_Operator, false);
			}
		}

		// Token: 0x06003538 RID: 13624 RVA: 0x0010D02C File Offset: 0x0010B22C
		public void UpdateInner(float deltaTime)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData != null && !gameOptionData.UseVideoTexture && this.m_fGCTime > 0f)
			{
				this.m_fGCTime -= deltaTime;
				if (this.m_fGCTime <= 0f)
				{
					this.m_fGCTime = 60f;
					GC.Collect();
				}
			}
			if (!NKCDefineManager.DEFINE_SERVICE())
			{
				if (Input.GetKeyDown(KeyCode.F2))
				{
					this.SetShowUI(!this.m_bShowUI, true);
				}
				if (base.GetGameData().m_bLocal)
				{
					if (Input.GetKeyDown(KeyCode.F5))
					{
						List<string> list = new List<string>();
						NKMUnitManager.ReloadFromLUA();
						list.Clear();
						list.Add("LUA_DAMAGE_TEMPLET");
						list.Add("LUA_DAMAGE_TEMPLET2");
						list.Add("LUA_DAMAGE_TEMPLET3");
						list.Add("LUA_DAMAGE_TEMPLET4");
						list.Add("LUA_DAMAGE_TEMPLET5");
						list.Add("LUA_DAMAGE_TEMPLET6");
						NKMDamageManager.LoadFromLUA(new string[]
						{
							"LUA_DAMAGE_TEMPLET_BASE",
							"LUA_DAMAGE_TEMPLET_BASE2",
							"LUA_DAMAGE_TEMPLET_BASE3",
							"LUA_DAMAGE_TEMPLET_BASE4",
							"LUA_DAMAGE_TEMPLET_BASE5",
							"LUA_DAMAGE_TEMPLET_BASE6"
						}, list);
						NKMBuffManager.LoadFromLUA();
						NKMBuffTemplet.ParseAllSkinDic();
						list.Clear();
						list.Add("LUA_DAMAGE_EFFECT_TEMPLET");
						list.Add("LUA_DAMAGE_EFFECT_TEMPLET2");
						list.Add("LUA_DAMAGE_EFFECT_TEMPLET3");
						list.Add("LUA_DAMAGE_EFFECT_TEMPLET4");
						list.Add("LUA_DAMAGE_EFFECT_TEMPLET5");
						list.Add("LUA_DAMAGE_EFFECT_TEMPLET6");
						NKMDETempletManager.LoadFromLUA(list, true);
						NKMCommonUnitEvent.LoadFromLUA("LUA_COMMON_UNIT_EVENT_HEAL", true);
						NKMMapManager.LoadFromLUA("LUA_MAP_TEMPLET", false);
					}
					if (Input.GetKeyDown(KeyCode.F6))
					{
						this.KeyDownF6();
					}
				}
			}
			if (this.m_NKMGameRuntimeData.m_bPause)
			{
				if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE < NKM_GAME_STATE.NGS_FINISH || NKCGameEventManager.IsPauseEventPlaying())
				{
					this.UpdatePause(deltaTime);
					return;
				}
				this.m_NKMGameRuntimeData.m_bPause = false;
			}
			this.m_fDeltaTime = deltaTime * this.m_GameSpeed.GetNowValue();
			this.m_GameSpeed.Update(deltaTime);
			this.m_NKCEffectManager.Update(this.m_fDeltaTime);
			NKM_GAME_STATE nkm_GAME_STATE = this.m_NKMGameRuntimeData.m_NKM_GAME_STATE;
			if (nkm_GAME_STATE != NKM_GAME_STATE.NGS_PLAY)
			{
				if (nkm_GAME_STATE == NKM_GAME_STATE.NGS_FINISH)
				{
					if (this.m_fFinishWaitTime > 0f)
					{
						this.m_fFinishWaitTime -= deltaTime;
						if (this.m_fFinishWaitTime < 0f)
						{
							this.GameStateChange(NKM_GAME_STATE.NGS_END, this.m_NKMGameRuntimeData.m_WinTeam, this.m_NKMGameRuntimeData.m_WaveID);
						}
					}
					if (this.m_fWinLoseWaitTime > 0f)
					{
						this.m_fWinLoseWaitTime -= deltaTime;
						if (this.m_fWinLoseWaitTime < 0f)
						{
							this.GetGameHud().SetTimeOver(false);
							if (this.m_NKMGameRuntimeData.m_WinTeam == NKM_TEAM_TYPE.NTT_DRAW)
							{
								NKCASEffect nkcaseffect = this.m_NKCEffectManager.UseEffect(0, "AB_FX_UI_START_WIN_LOSE", "AB_FX_UI_START_WIN_LOSE", NKM_EFFECT_PARENT_TYPE.NEPT_NUF_BEFORE_HUD_EFFECT, 0f, 0f, 0f, true, 1f, 0f, 0f, 0f, false, 0f, false, "", false, true, "DRAW", 1f, false, false, 0f, -1f, false);
								this.m_EndEffectUID = ((nkcaseffect != null) ? nkcaseffect.m_EffectUID : 0);
							}
							else if (!base.IsEnemy(this.m_NKMGameRuntimeData.m_WinTeam, this.m_MyTeam))
							{
								NKCASEffect nkcaseffect2 = this.m_NKCEffectManager.UseEffect(0, "AB_FX_UI_START_WIN_LOSE", "AB_FX_UI_START_WIN_LOSE", NKM_EFFECT_PARENT_TYPE.NEPT_NUF_BEFORE_HUD_EFFECT, 0f, 0f, 0f, true, 1f, 0f, 0f, 0f, false, 0f, false, "", false, true, "WIN", 1f, false, false, 0f, -1f, false);
								this.PlayOperatorVoiceMyTeam(VOICE_TYPE.VT_SHIP_FIGHT_VICTORY);
								this.m_EndEffectUID = ((nkcaseffect2 != null) ? nkcaseffect2.m_EffectUID : 0);
							}
							else
							{
								NKCASEffect nkcaseffect3 = this.m_NKCEffectManager.UseEffect(0, "AB_FX_UI_START_WIN_LOSE", "AB_FX_UI_START_WIN_LOSE", NKM_EFFECT_PARENT_TYPE.NEPT_NUF_BEFORE_HUD_EFFECT, 0f, 0f, 0f, true, 1f, 0f, 0f, 0f, false, 0f, false, "", false, true, "LOSE", 1f, false, false, 0f, -1f, false);
								this.PlayOperatorVoiceMyTeam(VOICE_TYPE.VT_SHIP_FIGHT_LOSE);
								this.m_EndEffectUID = ((nkcaseffect3 != null) ? nkcaseffect3.m_EffectUID : 0);
							}
						}
					}
					if (this.m_EndEffectUID != 0 && !this.GetNKCEffectManager().IsLiveEffect(this.m_EndEffectUID))
					{
						this.m_EndEffectUID = 0;
						this.PlayGameOutro();
					}
				}
			}
			else
			{
				this.m_fLocalGameTime += this.m_fDeltaTime;
				if (this.m_fDeltaTime < 0.016666668f)
				{
					this.m_fLastRecvTime += this.m_fDeltaTime;
				}
				else
				{
					this.m_fLastRecvTime += 0.016666668f;
				}
				if (this.m_fLastRecvTime > 10f)
				{
					Debug.LogError("Play State에서 10초간 패킷전달 없음, 재접속 시도");
					this.m_fLastRecvTime = 0f;
					NKCScenManager.GetScenManager().SetAppEnableConnectCheckTime(1f, false);
				}
				if (this.m_StartEffectUID != 0 && !this.m_NKCEffectManager.IsLiveEffect(this.m_StartEffectUID))
				{
					this.m_StartEffectUID = 0;
				}
			}
			nkm_GAME_STATE = this.m_NKMGameRuntimeData.m_NKM_GAME_STATE;
			if (nkm_GAME_STATE - NKM_GAME_STATE.NGS_START <= 2)
			{
				this.m_AbsoluteGameTime += this.m_fDeltaTime;
				if (this.m_fWorldStopTime <= 0f)
				{
					this.m_NKMGameRuntimeData.m_GameTime += this.m_fDeltaTime;
					this.ProcecssGameTime();
				}
				else
				{
					this.m_fWorldStopTime -= this.m_fDeltaTime;
					if (this.m_fWorldStopTime < 0f)
					{
						this.m_fWorldStopTime = 0f;
					}
				}
				base.ProcessStopTime();
				if (NKCReplayMgr.IsPlayingReplay())
				{
					this.m_fLastRecvTime = 0f;
					NKCReplayMgr nkcreplaMgr = NKCReplayMgr.GetNKCReplaMgr();
					if (nkcreplaMgr != null)
					{
						nkcreplaMgr.Update(this.m_NKMGameRuntimeData);
					}
				}
				this.ProcessSyncData();
				this.ProcessTacticalCommand();
				this.ProcessCamera();
				this.ProcessMap();
				this.ProcessUnit();
				if (this.m_fWorldStopTime <= 0f)
				{
					base.ProcessReAttack();
				}
				this.m_DEManager.Update(this.m_fDeltaTime);
				this.ProcessBloomPoint();
				this.ProcessUI();
			}
			NKMUnit nkmunit = null;
			NKMUnit nkmunit2 = null;
			short gameUnitUID = 0;
			short gameUnitUID2 = 0;
			if (base.IsATeam(this.m_MyTeam))
			{
				if (this.m_NKMGameData.m_NKMGameTeamDataA.m_MainShip != null)
				{
					gameUnitUID = this.m_NKMGameData.m_NKMGameTeamDataA.m_MainShip.m_listGameUnitUID[0];
				}
				nkmunit = this.GetUnit(gameUnitUID, true, false);
				if (this.m_NKMGameData.m_NKMGameTeamDataB.m_MainShip != null)
				{
					gameUnitUID2 = this.m_NKMGameData.m_NKMGameTeamDataB.m_MainShip.m_listGameUnitUID[0];
				}
				nkmunit2 = this.GetUnit(gameUnitUID2, true, false);
			}
			else if (base.IsBTeam(this.m_MyTeam))
			{
				if (this.m_NKMGameData.m_NKMGameTeamDataA.m_MainShip != null)
				{
					gameUnitUID2 = this.m_NKMGameData.m_NKMGameTeamDataA.m_MainShip.m_listGameUnitUID[0];
				}
				nkmunit2 = this.GetUnit(gameUnitUID2, true, false);
				if (this.m_NKMGameData.m_NKMGameTeamDataB.m_MainShip != null)
				{
					gameUnitUID = this.m_NKMGameData.m_NKMGameTeamDataB.m_MainShip.m_listGameUnitUID[0];
				}
				nkmunit = this.GetUnit(gameUnitUID, true, false);
			}
			if (nkmunit != null)
			{
				if (base.GetGameRuntimeData().m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_PLAY)
				{
					if (this.m_lastMyShipHpRate > 0.6f && nkmunit.GetHPRate() <= 0.6f)
					{
						if (this.GetMyTeamData() != null)
						{
							NKCUIVoiceManager.PlayOperatorVoice(VOICE_TYPE.VT_SHIP_ENEMY_AREA_2ND, this.GetMyTeamData().m_Operator, false);
						}
					}
					else if (this.m_lastMyShipHpRate > 0.3f && nkmunit.GetHPRate() <= 0.3f && this.GetMyTeamData() != null)
					{
						NKCUIVoiceManager.PlayOperatorVoice(VOICE_TYPE.VT_SHIP_ENEMY_AREA_3RD, this.GetMyTeamData().m_Operator, false);
					}
				}
				this.m_lastMyShipHpRate = nkmunit.GetHPRate();
			}
			if (nkmunit2 != null)
			{
				if (base.GetGameRuntimeData().m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_PLAY)
				{
					if (this.m_lastEnemyShipHpRate > 0.6f && nkmunit2.GetHPRate() <= 0.6f)
					{
						if (this.GetMyTeamData() != null)
						{
							NKCUIVoiceManager.PlayOperatorVoice(VOICE_TYPE.VT_SHIP_OUR_AREA_2ND, this.GetMyTeamData().m_Operator, false);
						}
					}
					else if (this.m_lastEnemyShipHpRate > 0.3f && nkmunit2.GetHPRate() <= 0.3f && this.GetMyTeamData() != null)
					{
						NKCUIVoiceManager.PlayOperatorVoice(VOICE_TYPE.VT_SHIP_OUR_AREA_3RD, this.GetMyTeamData().m_Operator, false);
					}
				}
				this.m_lastEnemyShipHpRate = nkmunit2.GetHPRate();
			}
		}

		// Token: 0x06003539 RID: 13625 RVA: 0x0010D890 File Offset: 0x0010BA90
		public void UpdatePause(float deltaTime)
		{
			if (NKCGameEventManager.IsGameCameraStopRequired())
			{
				foreach (KeyValuePair<short, NKCUnitViewer> keyValuePair in this.m_dicUnitViewer)
				{
					keyValuePair.Value.Update(this.m_fDeltaTime);
				}
				this.ProcessMap();
				if (NKCUIOverlayTutorialGuide.IsInstanceOpen && NKCUIOverlayTutorialGuide.Instance.IsShowingInvalidMap)
				{
					this.m_Map.SetRespawnValidLandAlpha(1f, 0f);
				}
			}
			else
			{
				this.ProcessCamera();
				this.ProcessMap();
			}
			this.m_fDeltaTime = deltaTime;
		}

		// Token: 0x0600353A RID: 13626 RVA: 0x0010D918 File Offset: 0x0010BB18
		public void GameStateChange(NKM_GAME_STATE newState, NKM_TEAM_TYPE eWinTeam, int waveID = 0)
		{
			if (newState < this.m_NKMGameRuntimeData.m_NKM_GAME_STATE)
			{
				Debug.LogErrorFormat("GameStateChange Back : {0} to {1} (waveID {2})", new object[]
				{
					this.m_NKMGameRuntimeData.m_NKM_GAME_STATE,
					newState,
					waveID
				});
			}
			Debug.LogFormat("Game State Change : {0} to {1} (waveID {2})", new object[]
			{
				this.m_NKMGameRuntimeData.m_NKM_GAME_STATE,
				newState,
				waveID
			});
			this.m_NKMGameRuntimeData.m_WaveID = waveID;
			if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE != NKM_GAME_STATE.NGS_PLAY && newState == NKM_GAME_STATE.NGS_PLAY)
			{
				string text = this.FindMusic();
				if (text.Length > 1)
				{
					NKCSoundManager.PlayMusic(text, true, 1f, false, 0f, 0f);
				}
				if (base.GetDungeonType() == NKM_DUNGEON_TYPE.NDT_WAVE)
				{
					this.GetGameHud().SetCurrentWaveUI(waveID);
					NKCASEffect nkcaseffect = this.PlayWaveAlarmUI();
					this.m_StartEffectUID = ((nkcaseffect != null) ? nkcaseffect.m_EffectUID : 0);
				}
				else if (base.GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_PRACTICE)
				{
					NKCASEffect nkcaseffect2 = this.m_NKCEffectManager.UseEffect(0, "AB_FX_UI_START_WIN_LOSE", "AB_FX_UI_START_WIN_LOSE", NKM_EFFECT_PARENT_TYPE.NEPT_NUF_BEFORE_HUD_EFFECT, 0f, 0f, 0f, true, 1f, 0f, 0f, 0f, false, 0f, false, "", false, true, "START", 1f, false, false, 0f, -1f, false);
					this.m_StartEffectUID = ((nkcaseffect2 != null) ? nkcaseffect2.m_EffectUID : 0);
				}
			}
			else if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE != NKM_GAME_STATE.NGS_END && newState == NKM_GAME_STATE.NGS_END)
			{
				if (NKCReplayMgr.IsRecording())
				{
					NKCScenManager.GetScenManager().GetNKCReplayMgr().StopRecording(true);
				}
				NKCScenManager.GetScenManager().Get_SCEN_GAME().EndGameWithReservedGameData();
			}
			else if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE != NKM_GAME_STATE.NGS_FINISH && newState == NKM_GAME_STATE.NGS_FINISH)
			{
				if (this.m_NKMGameData.GetGameType() == NKM_GAME_TYPE.NGT_TUTORIAL)
				{
					NKMGameRuntimeTeamData myRuntimeTeamData = this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(this.m_MyTeam);
					if (myRuntimeTeamData != null && myRuntimeTeamData.m_bAutoRespawn)
					{
						this.Send_Packet_GAME_AUTO_RESPAWN_REQ(false, false);
					}
				}
			}
			else if (newState == NKM_GAME_STATE.NGS_PLAY && base.GetDungeonType() == NKM_DUNGEON_TYPE.NDT_WAVE)
			{
				this.GetGameHud().SetCurrentWaveUI(waveID);
				this.PlayWaveAlarmUI();
				if (waveID > 1 && this.GetMyTeamData() != null)
				{
					NKCUIVoiceManager.PlayOperatorVoice(VOICE_TYPE.VT_WAVE_START, this.GetMyTeamData().m_Operator, false);
				}
			}
			this.m_NKMGameRuntimeData.m_NKM_GAME_STATE = newState;
			this.m_NKMGameRuntimeData.m_WinTeam = eWinTeam;
		}

		// Token: 0x0600353B RID: 13627 RVA: 0x0010DB80 File Offset: 0x0010BD80
		protected override void ProcessUnit()
		{
			base.ProcessUnit();
			foreach (KeyValuePair<short, NKCUnitViewer> keyValuePair in this.m_dicUnitViewer)
			{
				keyValuePair.Value.Update(this.m_fDeltaTime);
			}
			this.GetSortUnitListByZ();
		}

		// Token: 0x0600353C RID: 13628 RVA: 0x0010DBCC File Offset: 0x0010BDCC
		protected NKCASEffect PlayWaveAlarmUI()
		{
			NKCASEffect nkcaseffect = this.m_NKCEffectManager.UseEffect(0, "AB_FX_UI_START_WIN_LOSE", "AB_FX_UI_START_WIN_LOSE", NKM_EFFECT_PARENT_TYPE.NEPT_NUF_BEFORE_HUD_EFFECT, 0f, 0f, 0f, true, 1f, 0f, 0f, 0f, false, 0f, false, "", false, true, "WAVE", 1f, false, false, 0f, -1f, false);
			if (nkcaseffect != null && nkcaseffect.m_EffectInstant != null && nkcaseffect.m_EffectInstant.m_Instant != null)
			{
				Transform transform = nkcaseffect.m_EffectInstant.m_Instant.transform.Find("WAVE/WAVE_TEXT2");
				if (transform != null)
				{
					GameObject gameObject = transform.gameObject;
					if (gameObject != null)
					{
						Text component = gameObject.GetComponent<Text>();
						if (component != null)
						{
							component.text = this.m_NKMGameRuntimeData.m_WaveID.ToString() + " / " + this.GetGameHud().GetWaveCount().ToString();
						}
					}
				}
			}
			return nkcaseffect;
		}

		// Token: 0x0600353D RID: 13629 RVA: 0x0010DCD8 File Offset: 0x0010BED8
		public void ProcessSyncData()
		{
			if (this.m_linklistGameSyncData.Count == 0 && this.m_fLastRecvTime > 0.4f)
			{
				if (this.m_fDeltaTime < 0.016666668f)
				{
					this.m_fNoSyncDataTime += this.m_fDeltaTime;
				}
				else
				{
					this.m_fNoSyncDataTime += 0.016666668f;
				}
			}
			else
			{
				this.m_fNoSyncDataTime = 0f;
				this.GetGameHud().SetNetworkWeak(false);
			}
			if (this.m_NKMGameRuntimeData.m_GameTime > 5f && this.m_fNoSyncDataTime > 0.4f && !base.GetGameData().m_bLocal)
			{
				this.SetNetworkLatencyLevel(this.m_fLatencyLevel + 0.5f);
				if (this.m_fLatencyLevel >= 10f)
				{
					this.SetNetworkLatencyLevel(10f);
				}
				if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_PLAY)
				{
					this.GetGameHud().SetNetworkWeak(true);
				}
				Debug.LogWarningFormat("레이턴시 레벨 증가 m_fNoSyncDataTime: {0:F2}, m_fLatencyLevel: {1:F2}", new object[]
				{
					this.m_fNoSyncDataTime,
					this.m_fLatencyLevel
				});
				this.m_fNoSyncDataTime = -0.4f * this.m_fLatencyLevel;
			}
			LinkedListNode<NKMGameSyncData_Base> linkedListNode = this.m_linklistGameSyncData.First;
			while (linkedListNode != null)
			{
				NKMGameSyncData_Base value = linkedListNode.Value;
				if (value != null && this.SyncGameTimer(value.m_fGameTime))
				{
					for (int i = 0; i < value.m_NKMGameSyncData_DieUnit.Count; i++)
					{
						NKMGameSyncData_DieUnit nkmgameSyncData_DieUnit = value.m_NKMGameSyncData_DieUnit[i];
						foreach (short gameUnitUID in nkmgameSyncData_DieUnit.m_DieGameUnitUID)
						{
							NKCUnitClient nkcunitClient = (NKCUnitClient)this.GetUnit(gameUnitUID, true, false);
							if (nkcunitClient != null)
							{
								nkcunitClient.SetDie(true);
							}
						}
						nkmgameSyncData_DieUnit.m_DieGameUnitUID.Clear();
					}
					for (int j = 0; j < value.m_NKMGameSyncData_Unit.Count; j++)
					{
						NKMGameSyncData_Unit nkmgameSyncData_Unit = value.m_NKMGameSyncData_Unit[j];
						if (nkmgameSyncData_Unit != null && nkmgameSyncData_Unit.m_NKMGameUnitSyncData != null)
						{
							if (base.IsReversePosTeam(this.m_MyTeam))
							{
								this.ReverseSyncData(nkmgameSyncData_Unit);
							}
							nkmgameSyncData_Unit.m_NKMGameUnitSyncData.Encrypt();
							NKCUnitClient nkcunitClient2 = (NKCUnitClient)this.GetUnit(nkmgameSyncData_Unit.m_NKMGameUnitSyncData.m_GameUnitUID, true, false);
							if (!nkmgameSyncData_Unit.m_NKMGameUnitSyncData.m_bRespawnThisFrame)
							{
								if (nkcunitClient2 == null && nkmgameSyncData_Unit.m_NKMGameUnitSyncData.GetHP() > 0f)
								{
									Log.Error("Processing unit sync packet without unit spawn!!", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCGameClient.cs", 1750);
									nkcunitClient2 = this.RespawnUnit(nkmgameSyncData_Unit.m_NKMGameUnitSyncData);
								}
								if (nkcunitClient2 != null)
								{
									nkcunitClient2.OnRecv(nkmgameSyncData_Unit.m_NKMGameUnitSyncData);
								}
							}
							else
							{
								if (nkcunitClient2 != null && nkmgameSyncData_Unit.m_NKMGameUnitSyncData.m_bRespawnThisFrame)
								{
									this.DieBeforeRespawnSync(nkmgameSyncData_Unit.m_NKMGameUnitSyncData.m_GameUnitUID, nkcunitClient2);
								}
								this.RespawnUnit(nkmgameSyncData_Unit.m_NKMGameUnitSyncData);
							}
						}
					}
					for (int k = 0; k < value.m_NKMGameSyncDataSimple_Unit.Count; k++)
					{
						NKMGameSyncDataSimple_Unit nkmgameSyncDataSimple_Unit = value.m_NKMGameSyncDataSimple_Unit[k];
						if (base.IsReversePosTeam(this.m_MyTeam))
						{
							this.ReverseSyncDataSimple(nkmgameSyncDataSimple_Unit);
						}
						NKCUnitClient nkcunitClient3 = (NKCUnitClient)this.GetUnit(nkmgameSyncDataSimple_Unit.m_GameUnitUID, true, false);
						if (nkcunitClient3 != null)
						{
							nkcunitClient3.OnRecv(nkmgameSyncDataSimple_Unit);
						}
					}
					for (int l = 0; l < value.m_NKMGameSyncData_ShipSkill.Count; l++)
					{
						NKMGameSyncData_ShipSkill nkmgameSyncData_ShipSkill = value.m_NKMGameSyncData_ShipSkill[l];
						if (nkmgameSyncData_ShipSkill.m_NKMGameUnitSyncData != null)
						{
							if (base.IsReversePosTeam(this.m_MyTeam))
							{
								this.ReverseSyncData(nkmgameSyncData_ShipSkill);
							}
							NKCUnitClient nkcunitClient4 = (NKCUnitClient)this.GetUnit(nkmgameSyncData_ShipSkill.m_NKMGameUnitSyncData.m_GameUnitUID, true, false);
							if (nkcunitClient4 != null && !nkmgameSyncData_ShipSkill.m_NKMGameUnitSyncData.m_bRespawnThisFrame)
							{
								nkcunitClient4.OnRecv(nkmgameSyncData_ShipSkill.m_NKMGameUnitSyncData);
							}
							else
							{
								if (nkcunitClient4 != null && nkmgameSyncData_ShipSkill.m_NKMGameUnitSyncData.m_bRespawnThisFrame)
								{
									this.DieBeforeRespawnSync(nkmgameSyncData_ShipSkill.m_NKMGameUnitSyncData.m_GameUnitUID, nkcunitClient4);
								}
								nkcunitClient4 = this.RespawnUnit(nkmgameSyncData_ShipSkill.m_NKMGameUnitSyncData);
							}
							NKMShipSkillTemplet shipSkillTempletByID = NKMShipSkillManager.GetShipSkillTempletByID(nkmgameSyncData_ShipSkill.m_ShipSkillID);
							if (shipSkillTempletByID != null)
							{
								nkcunitClient4.GetUnitFrameData().m_ShipSkillTemplet = shipSkillTempletByID;
								nkcunitClient4.GetUnitFrameData().m_fShipSkillPosX = nkmgameSyncData_ShipSkill.m_SkillPosX;
							}
							this.m_ShipSkillMark.SetShow(false);
							this.GetGameHud().ReturnDeckByShipSkillID(shipSkillTempletByID.m_ShipSkillID);
						}
					}
					NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
					for (int m = 0; m < value.m_NKMGameSyncData_Deck.Count; m++)
					{
						NKMGameSyncData_Deck nkmgameSyncData_Deck = value.m_NKMGameSyncData_Deck[m];
						NKMGameTeamData nkmgameTeamData = this.GetMyTeamData();
						if (this.IsObserver(NKCScenManager.CurrentUserData()))
						{
							nkmgameTeamData = base.GetGameData().GetTeamData(value.m_NKMGameSyncData_Deck[m].m_NKM_TEAM_TYPE);
						}
						if (nkmgameTeamData != null && nkmgameSyncData_Deck.m_UnitDeckIndex >= 0)
						{
							nkmgameTeamData.m_DeckData.SetListUnitDeck((int)nkmgameSyncData_Deck.m_UnitDeckIndex, nkmgameSyncData_Deck.m_UnitDeckUID);
							if (nkmgameSyncData_Deck.m_DeckUsedAddUnitUID != -1L)
							{
								NKMUnitData unitFromUID = myUserData.m_ArmyData.GetUnitFromUID(nkmgameSyncData_Deck.m_DeckUsedAddUnitUID);
								if (unitFromUID != null)
								{
									NKMUnitTemplet unitTemplet = NKMUnitManager.GetUnitTemplet(unitFromUID.m_UnitID);
									if (unitTemplet != null)
									{
										if (nkmgameSyncData_Deck.m_DeckUsedAddUnitUID == nkmgameTeamData.m_LeaderUnitUID)
										{
											base.GetRespawnCost(unitTemplet.m_StatTemplet, true, nkmgameTeamData.m_eNKM_TEAM_TYPE);
										}
										else
										{
											base.GetRespawnCost(unitTemplet.m_StatTemplet, false, nkmgameTeamData.m_eNKM_TEAM_TYPE);
										}
									}
								}
								nkmgameTeamData.m_DeckData.AddListUnitDeckUsed(nkmgameSyncData_Deck.m_DeckUsedAddUnitUID);
							}
							if (nkmgameSyncData_Deck.m_DeckUsedRemoveIndex != -1)
							{
								nkmgameTeamData.m_DeckData.RemoveAtListUnitDeckUsed((int)nkmgameSyncData_Deck.m_DeckUsedRemoveIndex);
							}
							if (nkmgameSyncData_Deck.m_DeckTombAddUnitUID != -1L)
							{
								nkmgameTeamData.m_DeckData.AddListUnitDeckTomb(nkmgameSyncData_Deck.m_DeckTombAddUnitUID);
								this.GetGameHud().UpdateRemainUnitCount(nkmgameTeamData.m_listUnitData.Count - nkmgameTeamData.m_DeckData.GetListUnitDeckTombCount());
							}
							if (nkmgameSyncData_Deck.m_AutoRespawnIndex != -1)
							{
								nkmgameTeamData.m_DeckData.SetAutoRespawnIndex((int)nkmgameSyncData_Deck.m_AutoRespawnIndex);
							}
							if (nkmgameSyncData_Deck.m_NextDeckUnitUID != -1L)
							{
								nkmgameTeamData.m_DeckData.SetNextDeck(nkmgameSyncData_Deck.m_NextDeckUnitUID);
							}
							this.GetGameHud().SetSyncDeck(nkmgameSyncData_Deck);
						}
					}
					for (int n = 0; n < value.m_NKMGameSyncData_DeckAssist.Count; n++)
					{
						NKMGameSyncData_DeckAssist nkmgameSyncData_DeckAssist = value.m_NKMGameSyncData_DeckAssist[n];
						NKMGameTeamData myTeamData = this.GetMyTeamData();
						if (myTeamData != null)
						{
							myTeamData.m_DeckData.SetAutoRespawnIndexAssist((int)nkmgameSyncData_DeckAssist.m_AutoRespawnIndexAssist);
							if (this.m_MyTeam == nkmgameSyncData_DeckAssist.m_NKM_TEAM_TYPE)
							{
								this.GetGameHud().SetAssistDeck(myTeamData);
							}
						}
					}
					for (int num = 0; num < value.m_NKMGameSyncData_GameState.Count; num++)
					{
						NKMGameSyncData_GameState nkmgameSyncData_GameState = value.m_NKMGameSyncData_GameState[num];
						if (nkmgameSyncData_GameState.m_NKM_GAME_STATE != NKM_GAME_STATE.NGS_INVALID)
						{
							this.GameStateChange(nkmgameSyncData_GameState.m_NKM_GAME_STATE, nkmgameSyncData_GameState.m_WinTeam, nkmgameSyncData_GameState.m_WaveID);
						}
					}
					for (int num2 = 0; num2 < value.m_NKMGameSyncData_GameEvent.Count; num2++)
					{
						NKMGameSyncData_GameEvent cNKMGameSyncData_GameEvent = value.m_NKMGameSyncData_GameEvent[num2];
						this.ProcessGameEventSync(cNKMGameSyncData_GameEvent);
					}
					if (base.GetDungeonType() != NKM_DUNGEON_TYPE.NDT_WAVE && base.GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_PRACTICE && this.m_fRemainGameTimeBeforeSync > base.GetGameData().m_fDoubleCostTime && value.m_fRemainGameTime <= base.GetGameData().m_fDoubleCostTime)
					{
						this.m_NKCEffectManager.UseEffect(0, "AB_FX_UI_RESOURCE_DOUBLE", "AB_FX_UI_RESOURCE_DOUBLE", NKM_EFFECT_PARENT_TYPE.NEPT_NUF_BEFORE_HUD_EFFECT, 0f, 0f, 0f, true, 1f, 0f, 0f, 0f, false, 0f, false, "", false, true, "AB_FX_UI_RESOURCE_DOUBLE", 1f, false, false, 0f, -1f, false);
						this.GetGameHud().SetTimeWarningFX(true);
						if (!this.m_bCostSpeedVoicePlayed && base.GetGameRuntimeData().m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_PLAY)
						{
							this.m_bCostSpeedVoicePlayed = true;
							if (this.GetMyTeamData() != null)
							{
								NKCUIVoiceManager.PlayOperatorVoice(VOICE_TYPE.VT_COST_SPEED, this.GetMyTeamData().m_Operator, false);
							}
						}
					}
					if (this.HasTimeLimit() && base.GetDungeonType() != NKM_DUNGEON_TYPE.NDT_WAVE && this.m_fRemainGameTimeBeforeSync > 0f && value.m_fRemainGameTime <= 0f)
					{
						this.GetGameHud().SetTimeOver(true);
					}
					this.m_NKMGameRuntimeData.m_fRemainGameTime = value.m_fRemainGameTime;
					this.m_fRemainGameTimeBeforeSync = value.m_fRemainGameTime;
					NKM_TEAM_TYPE myTeam = this.m_MyTeam;
					if (myTeam != NKM_TEAM_TYPE.NTT_A1)
					{
						if (myTeam == NKM_TEAM_TYPE.NTT_B1)
						{
							this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(NKM_TEAM_TYPE.NTT_B1).m_fRespawnCost = value.m_fRespawnCostB1;
							this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(NKM_TEAM_TYPE.NTT_B1).m_fRespawnCostAssist = value.m_fRespawnCostAssistB1;
							this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(NKM_TEAM_TYPE.NTT_B1).m_fUsedRespawnCost = value.m_fUsedRespawnCostB1;
							this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(NKM_TEAM_TYPE.NTT_A1).m_fRespawnCost = value.m_fRespawnCostA1;
							this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(NKM_TEAM_TYPE.NTT_A1).m_fRespawnCostAssist = value.m_fRespawnCostAssistA1;
							this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(NKM_TEAM_TYPE.NTT_B1).m_NKM_GAME_AUTO_SKILL_TYPE = value.m_NKM_GAME_AUTO_SKILL_TYPE_B;
						}
					}
					else
					{
						this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(NKM_TEAM_TYPE.NTT_A1).m_fRespawnCost = value.m_fRespawnCostA1;
						this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(NKM_TEAM_TYPE.NTT_A1).m_fRespawnCostAssist = value.m_fRespawnCostAssistA1;
						this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(NKM_TEAM_TYPE.NTT_A1).m_fUsedRespawnCost = value.m_fUsedRespawnCostA1;
						this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(NKM_TEAM_TYPE.NTT_B1).m_fRespawnCost = value.m_fRespawnCostB1;
						this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(NKM_TEAM_TYPE.NTT_B1).m_fRespawnCostAssist = value.m_fRespawnCostAssistB1;
						this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(NKM_TEAM_TYPE.NTT_A1).m_NKM_GAME_AUTO_SKILL_TYPE = value.m_NKM_GAME_AUTO_SKILL_TYPE_A;
					}
					this.GetGameHud().SetRespawnCost();
					this.GetGameHud().SetRespawnCostAssist();
					this.m_NKMGameRuntimeData.m_fShipDamage = value.m_fShipDamage;
					this.m_NKMGameRuntimeData.m_NKM_GAME_SPEED_TYPE = value.m_NKM_GAME_SPEED_TYPE;
					if (NKCReplayMgr.IsPlayingReplay())
					{
						this.m_NKMGameRuntimeData.m_NKM_GAME_SPEED_TYPE = NKCReplayMgr.GetNKCReplaMgr().GetPlayingGameSpeedType();
					}
					for (int num3 = 0; num3 < value.m_NKMGameSyncData_DungeonEvent.Count; num3++)
					{
						this.ProcessDungeonEventSync(value.m_NKMGameSyncData_DungeonEvent[num3]);
					}
					if (base.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_FIERCE && value.m_NKMGameSyncData_Fierce != null)
					{
						this.GetGameHud().SetFierceBattleScore(value.m_NKMGameSyncData_Fierce.m_fFiercePoint);
						Debug.Log(string.Format("<color=green>fierce point : {0}</color>", value.m_NKMGameSyncData_Fierce.m_fFiercePoint));
					}
					if (base.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_TRIM && value.m_NKMGameSyncData_Trim != null)
					{
						this.GetGameHud().SetTrimBattleScore(value.m_NKMGameSyncData_Trim.m_fTrimPoint);
						Debug.Log(string.Format("<color=green>trim point : {0}</color>", value.m_NKMGameSyncData_Trim.m_fTrimPoint));
					}
					NKCPacketObjectPool.CloseObject(value);
					LinkedListNode<NKMGameSyncData_Base> next = linkedListNode.Next;
					this.m_linklistGameSyncData.Remove(linkedListNode);
					linkedListNode = next;
				}
				else if (linkedListNode != null)
				{
					linkedListNode = linkedListNode.Next;
				}
			}
		}

		// Token: 0x0600353E RID: 13630 RVA: 0x0010E7AC File Offset: 0x0010C9AC
		private void DieBeforeRespawnSync(short gameUnitUID, NKCUnitClient cNKCUnitClient)
		{
			if (!this.m_dicNKMUnitPool.ContainsKey(gameUnitUID))
			{
				this.m_dicNKMUnitPool.Add(gameUnitUID, cNKCUnitClient);
			}
			if (this.m_dicNKMUnit.ContainsKey(gameUnitUID))
			{
				this.m_dicNKMUnit.Remove(gameUnitUID);
			}
			if (this.m_listNKMUnit.Contains(cNKCUnitClient))
			{
				this.m_listNKMUnit.Remove(cNKCUnitClient);
			}
			cNKCUnitClient.SetDie(true);
		}

		// Token: 0x0600353F RID: 13631 RVA: 0x0010E814 File Offset: 0x0010CA14
		public float GetRespawnCostClient(NKM_TEAM_TYPE teamType)
		{
			float num = 0f;
			foreach (KeyValuePair<long, float> keyValuePair in this.m_dicRespawnCostHolder)
			{
				num += keyValuePair.Value;
			}
			foreach (KeyValuePair<long, float> keyValuePair2 in this.m_dicRespawnCostHolderTC)
			{
				num += keyValuePair2.Value;
			}
			float num2 = this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(teamType).m_fRespawnCost - num;
			if (num2 < 0f)
			{
				num2 = 0f;
			}
			return num2;
		}

		// Token: 0x06003540 RID: 13632 RVA: 0x0010E8DC File Offset: 0x0010CADC
		public float GetRespawnCostAssistClient(NKM_TEAM_TYPE teamType)
		{
			float num = 0f;
			foreach (KeyValuePair<long, float> keyValuePair in this.m_dicRespawnCostHolderAssist)
			{
				num += keyValuePair.Value;
			}
			float num2 = this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(teamType).m_fRespawnCostAssist - num;
			if (num2 < 0f)
			{
				num2 = 0f;
			}
			return num2;
		}

		// Token: 0x06003541 RID: 13633 RVA: 0x0010E95C File Offset: 0x0010CB5C
		public float GetMyRespawnCostClient()
		{
			float num = 0f;
			foreach (KeyValuePair<long, float> keyValuePair in this.m_dicRespawnCostHolder)
			{
				num += keyValuePair.Value;
			}
			foreach (KeyValuePair<long, float> keyValuePair2 in this.m_dicRespawnCostHolderTC)
			{
				num += keyValuePair2.Value;
			}
			float num2 = this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(this.m_MyTeam).m_fRespawnCost - num;
			if (num2 < 0f)
			{
				num2 = 0f;
			}
			return num2;
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x0010EA28 File Offset: 0x0010CC28
		private float GetMyRespawnCostAssistClient()
		{
			float num = 0f;
			foreach (KeyValuePair<long, float> keyValuePair in this.m_dicRespawnCostHolderAssist)
			{
				num += keyValuePair.Value;
			}
			float num2 = this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(this.m_MyTeam).m_fRespawnCostAssist - num;
			if (num2 < 0f)
			{
				num2 = 0f;
			}
			return num2;
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x0010EAAC File Offset: 0x0010CCAC
		private void ReverseSyncData(NKMGameSyncData_Unit cNKMGameSyncData_Unit)
		{
			cNKMGameSyncData_Unit.m_NKMGameUnitSyncData.m_PosX = base.GetMapTemplet().ReversePosX(cNKMGameSyncData_Unit.m_NKMGameUnitSyncData.m_PosX);
			cNKMGameSyncData_Unit.m_NKMGameUnitSyncData.m_bRight = !cNKMGameSyncData_Unit.m_NKMGameUnitSyncData.m_bRight;
			cNKMGameSyncData_Unit.m_NKMGameUnitSyncData.m_bDamageSpeedXNegative = !cNKMGameSyncData_Unit.m_NKMGameUnitSyncData.m_bDamageSpeedXNegative;
		}

		// Token: 0x06003544 RID: 13636 RVA: 0x0010EB0C File Offset: 0x0010CD0C
		private void ReverseSyncDataSimple(NKMGameSyncDataSimple_Unit cNKMGameSyncDataSimple_Unit)
		{
			cNKMGameSyncDataSimple_Unit.m_bRight = !cNKMGameSyncDataSimple_Unit.m_bRight;
		}

		// Token: 0x06003545 RID: 13637 RVA: 0x0010EB20 File Offset: 0x0010CD20
		private void ReverseSyncData(NKMGameSyncData_ShipSkill cNKMGameSyncData_ShipSkill)
		{
			cNKMGameSyncData_ShipSkill.m_NKMGameUnitSyncData.m_PosX = base.GetMapTemplet().ReversePosX(cNKMGameSyncData_ShipSkill.m_NKMGameUnitSyncData.m_PosX);
			cNKMGameSyncData_ShipSkill.m_NKMGameUnitSyncData.m_bRight = !cNKMGameSyncData_ShipSkill.m_NKMGameUnitSyncData.m_bRight;
			cNKMGameSyncData_ShipSkill.m_SkillPosX = base.GetMapTemplet().ReversePosX(cNKMGameSyncData_ShipSkill.m_SkillPosX);
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x0010EB80 File Offset: 0x0010CD80
		public void ProcessCamera()
		{
			if (NKCGameEventManager.IsGameCameraStopRequired())
			{
				return;
			}
			if (this.m_NKM_GAME_CAMERA_MODE == NKM_GAME_CAMERA_MODE.NGCM_FOCUS_UNIT)
			{
				NKCUnitClient nkcunitClient = (NKCUnitClient)this.GetUnit(this.m_CameraFocusGameUnitUID, true, false);
				if (nkcunitClient != null && nkcunitClient.ProcessCamera())
				{
					this.m_fCameraNormalTackingWaitTime = 0.1f;
				}
			}
			this.ProcessCameraShortcut();
			bool flag = false;
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData != null)
			{
				flag = gameOptionData.TrackCamera;
				if (base.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_DEV || this.IsObserver(NKCScenManager.CurrentUserData()) || this.IsPause())
				{
					flag = false;
				}
			}
			if (this.m_NKM_GAME_CAMERA_MODE == NKM_GAME_CAMERA_MODE.NGCM_NORMAL_TRACKING && this.m_NKMGameRuntimeData.m_NKM_GAME_STATE != NKM_GAME_STATE.NGS_FINISH && flag)
			{
				float trackingCamPosX = this.GetTrackingCamPosX(true);
				NKCCamera.TrackingPos(3f, trackingCamPosX, 0f, -1f);
				NKCCamera.TrackingZoom(0.2f, NKCCamera.GetCameraSizeOrg());
				this.m_fCameraNormalTackingWaitTime = 3f;
			}
			else
			{
				if (this.m_NKM_GAME_CAMERA_MODE == NKM_GAME_CAMERA_MODE.NGCM_STOP)
				{
					NKCCamera.SetPos(-1f, 0f, -1f, true, false);
				}
				this.m_fCameraNormalTackingWaitTime -= this.m_fDeltaTime;
				if (this.m_fCameraNormalTackingWaitTime <= 0f)
				{
					this.SetCameraMode(NKM_GAME_CAMERA_MODE.NGCM_NORMAL_TRACKING);
					NKCCamera.TrackingZoom(0.2f, NKCCamera.GetCameraSizeOrg());
					NKCCamera.TrackingPos(3f, -1f, 0f, -1f);
				}
			}
			if (this.m_NKM_GAME_CAMERA_MODE == NKM_GAME_CAMERA_MODE.NGCM_DRAG)
			{
				this.m_fCameraStopDragTime += this.m_fDeltaTime;
				this.m_CameraDrag.Update(this.m_fDeltaTime);
				float num = NKCCamera.GetCameraSizeNow() * NKCCamera.GetCameraAspect() * 0.5f;
				if (this.m_CameraDrag.GetNowValue() < this.m_NKMMapTemplet.m_fCamMinX + num)
				{
					this.m_CameraDrag.SetNowValue(this.m_NKMMapTemplet.m_fCamMinX + num);
				}
				if (this.m_CameraDrag.GetNowValue() > this.m_NKMMapTemplet.m_fCamMaxX - num)
				{
					this.m_CameraDrag.SetNowValue(this.m_NKMMapTemplet.m_fCamMaxX - num);
				}
				NKCCamera.SetPos(this.m_CameraDrag.GetNowValue(), 0f, -1f, true, false);
				NKCCamera.TrackingZoom(0.2f, NKCCamera.GetCameraSizeOrg());
				this.m_CameraDragTime += this.m_fDeltaTime;
				if (!this.m_CameraDrag.IsTracking() && this.m_fCameraStopDragTime > 0.1f)
				{
					this.m_CameraDrag.StopTracking();
					this.m_CameraDrag.SetNowValue(NKCCamera.GetPosNowX(false));
					this.m_CameraDragTime = 0f;
					this.m_CameraDragDist = 0f;
				}
			}
		}

		// Token: 0x06003547 RID: 13639 RVA: 0x0010EE00 File Offset: 0x0010D000
		private void ProcessCameraShortcut()
		{
			if (NKCInputManager.IsHotkeyPressed(HotkeyEventType.Right))
			{
				this.m_CameraDrag.SetNowValue(NKCCamera.GetPosNowX(false) + 2500f * Time.deltaTime);
				this.SetCameraMode(NKM_GAME_CAMERA_MODE.NGCM_DRAG);
				this.m_fCameraNormalTackingWaitTime = 3f;
			}
			else if (NKCInputManager.IsHotkeyPressed(HotkeyEventType.Left))
			{
				this.m_CameraDrag.SetNowValue(NKCCamera.GetPosNowX(false) - 2500f * Time.deltaTime);
				this.SetCameraMode(NKM_GAME_CAMERA_MODE.NGCM_DRAG);
				this.m_fCameraNormalTackingWaitTime = 3f;
			}
			if (Input.mouseScrollDelta != Vector2.zero)
			{
				this.m_CameraDrag.SetNowValue(NKCCamera.GetPosNowX(false) + (Input.mouseScrollDelta.x - Input.mouseScrollDelta.y) * 2f * NKCInputManager.ScrollSensibility);
				this.SetCameraMode(NKM_GAME_CAMERA_MODE.NGCM_DRAG);
				this.m_fCameraNormalTackingWaitTime = 3f;
			}
		}

		// Token: 0x06003548 RID: 13640 RVA: 0x0010EED4 File Offset: 0x0010D0D4
		private float GetTrackingCamPosX(bool bFront = true)
		{
			float num = base.GetMapTemplet().m_fCamMinX;
			for (int i = 0; i < this.m_listNKMUnit.Count; i++)
			{
				NKMUnit nkmunit = this.m_listNKMUnit[i];
				if (nkmunit != null && nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_PLAY && !base.IsEnemy(this.m_MyTeam, nkmunit.GetUnitDataGame().m_NKM_TEAM_TYPE))
				{
					if (bFront)
					{
						if (num == base.GetMapTemplet().m_fCamMinX || num < nkmunit.GetUnitSyncData().m_PosX)
						{
							num = nkmunit.GetUnitSyncData().m_PosX;
						}
					}
					else if (nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL && (num == base.GetMapTemplet().m_fCamMinX || num > nkmunit.GetUnitSyncData().m_PosX))
					{
						num = nkmunit.GetUnitSyncData().m_PosX;
					}
				}
			}
			return num;
		}

		// Token: 0x06003549 RID: 13641 RVA: 0x0010EFB0 File Offset: 0x0010D1B0
		public void ProcessMap()
		{
			if (this.ProcecssValidLand(this.m_MyTeam))
			{
				if (base.IsATeam(this.m_MyTeam))
				{
					this.m_Map.SetRespawnValidLandFactor(this.m_fRespawnValidLandTeamA, true);
				}
				else if (base.IsBTeam(this.m_MyTeam))
				{
					this.m_Map.SetRespawnValidLandFactor(this.m_fRespawnValidLandTeamB, true);
				}
			}
			if (this.m_bDeckDrag)
			{
				bool flag = false;
				if (this.m_DeckSelectUnitTemplet != null && this.m_DeckSelectUnitTemplet.m_UnitTempletBase.m_bRespawnFreePos)
				{
					flag = true;
				}
				if (base.GetDungeonTemplet() != null && base.GetDungeonTemplet().m_bRespawnFreePos)
				{
					flag = true;
				}
				if (flag)
				{
					this.m_Map.SetRespawnValidLandAlpha(1f, 0.2f);
				}
				else
				{
					this.m_Map.SetRespawnValidLandAlpha(1f, 0f);
				}
			}
			this.m_Map.Update(this.m_fDeltaTime);
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x0010F08C File Offset: 0x0010D28C
		public void ProcessBloomPoint()
		{
			for (int i = 0; i < this.m_NKMMapTemplet.m_listBloomPoint.Count; i++)
			{
				NKMBloomPoint nkmbloomPoint = this.m_NKMMapTemplet.m_listBloomPoint[i];
				if (nkmbloomPoint != null)
				{
					float num = Mathf.Abs(NKCCamera.GetPosNowX(false) - nkmbloomPoint.m_fBloomPointX) / nkmbloomPoint.m_fBloomDistance;
					num = Mathf.Clamp(num, 0f, 1f);
					num = 1f - num;
					NKCCamera.SetBloomIntensity(NKCCamera.GetBloomIntensityOrg() + NKCCamera.GetBloomIntensityOrg() * num * nkmbloomPoint.m_fBloomAddIntensity);
					NKCCamera.SetBloomThreshHold(NKCCamera.GetBloomThreshHoldOrg() + NKCCamera.GetBloomThreshHoldOrg() * num * nkmbloomPoint.m_fBloomAddThreshHold);
					if (nkmbloomPoint.m_LensFlareName.m_AssetName.Length > 1)
					{
						NKCASLensFlare lensFlare = this.m_Map.GetLensFlare(i);
						if (lensFlare != null)
						{
							lensFlare.SetPos(nkmbloomPoint.m_fBloomPointX, nkmbloomPoint.m_fBloomPointY, -1f);
							lensFlare.SetLensFlareBright(lensFlare.GetLensFlareBrightOrg() * num * this.m_Map.GetMapBright());
						}
					}
				}
			}
		}

		// Token: 0x0600354B RID: 13643 RVA: 0x0010F18C File Offset: 0x0010D38C
		public void ProcessUI()
		{
			short gameUnitUID = -1;
			NKMUnit nkmunit = null;
			short gameUnitUID2 = -1;
			NKMUnit nkmunit2 = null;
			if (base.IsATeam(this.m_MyTeam))
			{
				if (this.m_NKMGameData.m_NKMGameTeamDataA.m_MainShip != null)
				{
					gameUnitUID = this.m_NKMGameData.m_NKMGameTeamDataA.m_MainShip.m_listGameUnitUID[0];
				}
				nkmunit = this.GetUnit(gameUnitUID, true, false);
				if (this.m_NKMGameData.m_NKMGameTeamDataB.m_MainShip != null)
				{
					gameUnitUID2 = this.m_NKMGameData.m_NKMGameTeamDataB.m_MainShip.m_listGameUnitUID[0];
				}
				nkmunit2 = this.GetUnit(gameUnitUID2, true, false);
			}
			else if (base.IsBTeam(this.m_MyTeam))
			{
				if (this.m_NKMGameData.m_NKMGameTeamDataA.m_MainShip != null)
				{
					gameUnitUID2 = this.m_NKMGameData.m_NKMGameTeamDataA.m_MainShip.m_listGameUnitUID[0];
				}
				nkmunit2 = this.GetUnit(gameUnitUID2, true, false);
				if (this.m_NKMGameData.m_NKMGameTeamDataB.m_MainShip != null)
				{
					gameUnitUID = this.m_NKMGameData.m_NKMGameTeamDataB.m_MainShip.m_listGameUnitUID[0];
				}
				nkmunit = this.GetUnit(gameUnitUID, true, false);
			}
			if (nkmunit != null)
			{
				this.GetGameHud().SetMainGage(true, nkmunit.GetUnitSyncData().GetHP() / nkmunit.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP), base.GetDungeonType() == NKM_DUNGEON_TYPE.NDT_WAVE);
			}
			if (base.GetDungeonType() == NKM_DUNGEON_TYPE.NDT_WAVE)
			{
				if (base.GetGameRuntimeData().m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_FINISH && base.IsATeam(base.GetGameRuntimeData().m_WinTeam))
				{
					this.GetGameHud().SetMainGage(false, 0f, false);
				}
				else if (this.GetGameHud().GetWaveCount() > 0)
				{
					float num = (float)(this.GetGameHud().GetWaveCount() - (this.m_NKMGameRuntimeData.m_WaveID - 1));
					if (num <= 0f)
					{
						num = 0f;
					}
					if (num >= (float)this.GetGameHud().GetWaveCount())
					{
						num = (float)this.GetGameHud().GetWaveCount();
					}
					this.GetGameHud().SetMainGage(false, num / (float)this.GetGameHud().GetWaveCount(), false);
				}
			}
			else if (nkmunit2 != null)
			{
				this.GetGameHud().SetMainGage(false, nkmunit2.GetUnitSyncData().GetHP() / nkmunit2.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP), base.GetDungeonType() == NKM_DUNGEON_TYPE.NDT_DAMAGE_ACCRUE);
				if (base.GetDungeonType() == NKM_DUNGEON_TYPE.NDT_DAMAGE_ACCRUE || base.GetDungeonType() == NKM_DUNGEON_TYPE.NDT_RAID || base.GetDungeonType() == NKM_DUNGEON_TYPE.NDT_SOLO_RAID)
				{
					this.GetGameHud().SetAttackPoint(true, (int)nkmunit2.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP) - (int)nkmunit2.GetUnitSyncData().GetHP());
					this.GetGameHud().SetAttackPoint(false, (int)nkmunit2.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP));
				}
			}
			if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_PLAY)
			{
				if (!this.m_bRespawnUnit && this.m_fLocalGameTime > 5f)
				{
					if (!this.GetGameHud().IsShowDangerMsg())
					{
						this.GetGameHud().PlayDangerMsg(NKCUtilString.GET_STRING_DANGER_MSG_UNIT_RESPAWN);
					}
				}
				else if (this.m_bRespawnUnit && this.GetGameHud().IsShowDangerMsg())
				{
					this.GetGameHud().StopDangerMsg();
				}
			}
			else if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_FINISH && this.GetGameHud().IsShowDangerMsg())
			{
				this.GetGameHud().StopDangerMsg();
			}
			this.GetGameHud().UpdateHud(this.m_fDeltaTime);
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x0010F4CB File Offset: 0x0010D6CB
		public void FadeColor(NKCUnitClient callerUnit, float fR, float fG, float fB, float fMapColorKeepTime, float fMapColorReturnTime)
		{
			this.m_Map.FadeColor(fR, fG, fB, fMapColorKeepTime, fMapColorReturnTime);
		}

		// Token: 0x0600354D RID: 13645 RVA: 0x0010F4E0 File Offset: 0x0010D6E0
		public void SetSkillFadeUnit(NKCUnitClient callerUnit, float fTime)
		{
			List<NKMUnit> unitChain = this.GetUnitChain();
			for (int i = 0; i < unitChain.Count; i++)
			{
				NKCUnitClient nkcunitClient = (NKCUnitClient)unitChain[i];
				if (nkcunitClient != null)
				{
					if (nkcunitClient == callerUnit)
					{
						nkcunitClient.SetUnitSkillColorFade(0f);
					}
					else
					{
						nkcunitClient.SetUnitSkillColorFade(fTime);
					}
				}
			}
		}

		// Token: 0x0600354E RID: 13646 RVA: 0x0010F52D File Offset: 0x0010D72D
		public bool SyncGameTimer(float fGameTime)
		{
			return (this.m_NKMGameRuntimeData.m_GameTime == 0f && fGameTime == 0f) || this.m_NKMGameRuntimeData.m_GameTime > fGameTime;
		}

		// Token: 0x0600354F RID: 13647 RVA: 0x0010F55C File Offset: 0x0010D75C
		public void RespawnPreviewBegin(int deckIndex, Vector2 pos)
		{
			long deckUnitUID = this.GetGameHud().GetDeckUnitUID(deckIndex);
			NKMUnitData unitDataByUnitUID = this.m_NKMGameData.GetUnitDataByUnitUID(deckUnitUID);
			if (unitDataByUnitUID != null)
			{
				for (int i = 0; i < unitDataByUnitUID.m_listGameUnitUID.Count; i++)
				{
					short gameUnitUID = unitDataByUnitUID.m_listGameUnitUID[i];
					NKCUnitClient nkcunitClient = (NKCUnitClient)this.GetUnit(gameUnitUID, true, true);
					if (nkcunitClient != null)
					{
						NKCUnitViewer unitViewer = nkcunitClient.GetUnitViewer();
						if (unitViewer != null)
						{
							unitViewer.SetActiveSprite(true);
							unitViewer.SetActiveShadow(true);
							unitViewer.StopTimer();
							unitViewer.Play("ASTAND", true, 0f);
							NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitDataByUnitUID.m_UnitID);
							int respawnCost;
							if (deckUnitUID == this.GetMyTeamData().m_LeaderUnitUID)
							{
								respawnCost = base.GetRespawnCost(unitStatTemplet, true, this.GetMyTeamData().m_eNKM_TEAM_TYPE);
							}
							else
							{
								respawnCost = base.GetRespawnCost(unitStatTemplet, false, this.GetMyTeamData().m_eNKM_TEAM_TYPE);
							}
							bool flag = this.GetMyTeamData().IsAssistUnit(deckUnitUID);
							if (!flag && this.GetMyRespawnCostClient() < (float)respawnCost)
							{
								unitViewer.SetColor(1f, 0f, 0f, 0.5f);
							}
							else if (flag && this.GetMyRespawnCostAssistClient() < (float)respawnCost)
							{
								unitViewer.SetColor(1f, 0f, 0f, 0.5f);
							}
							else if (this.GetGameHud().GetGameClient().IsGameUnitAllInBattle(deckUnitUID) != NKM_ERROR_CODE.NEC_OK)
							{
								unitViewer.SetColor(1f, 0f, 0f, 0.5f);
							}
							else
							{
								unitViewer.SetColor(1f, 1f, 1f, 0.5f);
							}
							unitViewer.SetLayer("UNIT_C");
							NKCCamera.GetScreenPosToWorldPos(out this.m_Vec3Temp, pos.x, pos.y);
							float fFactor = this.m_fRespawnValidLandTeamA;
							if (this.m_MyTeam == NKM_TEAM_TYPE.NTT_A1 || this.m_MyTeam == NKM_TEAM_TYPE.NTT_A2)
							{
								fFactor = this.m_fRespawnValidLandTeamA;
							}
							else
							{
								fFactor = this.m_fRespawnValidLandTeamB;
							}
							bool flag2 = false;
							if (unitViewer.GetUnitTemplet().m_UnitTempletBase.m_bRespawnFreePos)
							{
								flag2 = true;
							}
							if (base.GetDungeonTemplet() != null && base.GetDungeonTemplet().m_bRespawnFreePos)
							{
								flag2 = true;
							}
							if (flag2)
							{
								fFactor = 0.8f;
							}
							float minOffset = base.GetGameData().IsPVP() ? NKMCommonConst.PVP_SUMMON_MIN_POS : NKMCommonConst.PVE_SUMMON_MIN_POS;
							this.m_Vec3Temp.x = this.m_NKMMapTemplet.GetNearLandX(this.m_Vec3Temp.x, true, fFactor, minOffset);
							if (this.m_Vec3Temp.y - Screen.safeArea.y < this.m_NKMMapTemplet.m_fMinZ - 70f && pos.x < (float)Screen.width * 0.5f)
							{
								unitViewer.SetActiveSprite(false);
								unitViewer.SetActiveShadow(false);
								unitViewer.StopTimer();
								this.m_bDeckSelectUnitUID = 0L;
							}
							else
							{
								unitViewer.SetActiveSprite(true);
								unitViewer.SetActiveShadow(true);
								unitViewer.StopTimer();
								this.m_bDeckSelectUnitUID = deckUnitUID;
								this.m_DeckSelectUnitTemplet = unitViewer.GetUnitTemplet();
								this.m_bDeckSelectPosX = this.m_Vec3Temp.x;
								this.m_Vec3Temp.y = this.m_NKMMapTemplet.GetNearLandZ(this.m_Vec3Temp.y);
							}
							unitViewer.SetPos(this.m_Vec3Temp.x, 0f, this.m_Vec3Temp.y + 100f * (float)i, true);
							unitViewer.SetRight(true);
							if (!this.m_dicUnitViewer.ContainsKey(nkcunitClient.GetUnitDataGame().m_GameUnitUID))
							{
								this.m_dicUnitViewer.Add(nkcunitClient.GetUnitDataGame().m_GameUnitUID, unitViewer);
							}
						}
					}
				}
			}
		}

		// Token: 0x06003550 RID: 13648 RVA: 0x0010F8F0 File Offset: 0x0010DAF0
		public void RespawnPreviewDrag(int deckIndex, Vector2 pos)
		{
			long deckUnitUID = this.GetGameHud().GetDeckUnitUID(deckIndex);
			float fFactor = this.m_fRespawnValidLandTeamA;
			if (base.IsATeam(this.m_MyTeam))
			{
				fFactor = this.m_fRespawnValidLandTeamA;
			}
			else if (base.IsBTeam(this.m_MyTeam))
			{
				fFactor = this.m_fRespawnValidLandTeamB;
			}
			int num = 0;
			NKMUnitData unitDataByUnitUID = this.m_NKMGameData.GetUnitDataByUnitUID(deckUnitUID);
			if (unitDataByUnitUID != null)
			{
				for (int i = 0; i < unitDataByUnitUID.m_listGameUnitUID.Count; i++)
				{
					short key = unitDataByUnitUID.m_listGameUnitUID[i];
					if (this.m_dicUnitViewer.ContainsKey(key))
					{
						NKCUnitViewer nkcunitViewer = this.m_dicUnitViewer[key];
						if (nkcunitViewer != null)
						{
							NKCCamera.GetScreenPosToWorldPos(out this.m_Vec3Temp, pos.x, pos.y);
							bool flag = false;
							if (nkcunitViewer.GetUnitTemplet().m_UnitTempletBase.m_bRespawnFreePos)
							{
								flag = true;
							}
							if (base.GetDungeonTemplet() != null && base.GetDungeonTemplet().m_bRespawnFreePos)
							{
								flag = true;
							}
							if (flag)
							{
								fFactor = 0.8f;
							}
							float minOffset = base.GetGameData().IsPVP() ? NKMCommonConst.PVP_SUMMON_MIN_POS : NKMCommonConst.PVE_SUMMON_MIN_POS;
							this.m_Vec3Temp.x = this.m_NKMMapTemplet.GetNearLandX(this.m_Vec3Temp.x, true, fFactor, minOffset);
							if (this.m_Vec3Temp.y - Screen.safeArea.y < this.m_NKMMapTemplet.m_fMinZ - 70f && pos.x < (float)Screen.width * 0.7f)
							{
								nkcunitViewer.SetActiveSprite(false);
								nkcunitViewer.SetActiveShadow(false);
								nkcunitViewer.StopTimer();
								this.m_bDeckSelectUnitUID = 0L;
							}
							else
							{
								nkcunitViewer.SetActiveSprite(true);
								nkcunitViewer.SetActiveShadow(true);
								nkcunitViewer.StopTimer();
								this.m_bDeckSelectUnitUID = deckUnitUID;
								this.m_DeckSelectUnitTemplet = nkcunitViewer.GetUnitTemplet();
								this.m_bDeckSelectPosX = this.m_Vec3Temp.x;
								this.m_Vec3Temp.y = this.m_NKMMapTemplet.GetNearLandZ(this.m_Vec3Temp.y);
							}
							nkcunitViewer.SetPos(this.m_Vec3Temp.x, 0f, this.m_Vec3Temp.y + 100f * (float)num, true);
							nkcunitViewer.SetRight(true);
							NKMUnitData unitDataByUnitUID2 = this.m_NKMGameData.GetUnitDataByUnitUID(deckUnitUID);
							if (unitDataByUnitUID2 != null)
							{
								NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitDataByUnitUID2.m_UnitID);
								if (unitStatTemplet != null)
								{
									int respawnCost;
									if (deckUnitUID == this.GetMyTeamData().m_LeaderUnitUID)
									{
										respawnCost = base.GetRespawnCost(unitStatTemplet, true, this.GetMyTeamData().m_eNKM_TEAM_TYPE);
									}
									else
									{
										respawnCost = base.GetRespawnCost(unitStatTemplet, false, this.GetMyTeamData().m_eNKM_TEAM_TYPE);
									}
									bool flag2 = this.GetMyTeamData().IsAssistUnit(deckUnitUID);
									if (!flag2 && this.GetMyRespawnCostClient() < (float)respawnCost)
									{
										nkcunitViewer.SetColor(1f, 0f, 0f, 0.5f);
									}
									else if (flag2 && this.GetMyRespawnCostAssistClient() < (float)respawnCost)
									{
										nkcunitViewer.SetColor(1f, 0f, 0f, 0.5f);
									}
									else if (this.GetGameHud().GetGameClient().IsGameUnitAllInBattle(deckUnitUID) != NKM_ERROR_CODE.NEC_OK)
									{
										nkcunitViewer.SetColor(1f, 0f, 0f, 0.5f);
									}
									else
									{
										nkcunitViewer.SetColor(1f, 1f, 1f, 0.5f);
									}
								}
							}
							num++;
						}
					}
				}
			}
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x0010FC4C File Offset: 0x0010DE4C
		public void RespawnPreviewShipSkillBegin(int deckIndex, Vector2 pos)
		{
			NKCUIHudShipSkillDeck shipSkillDeck = this.GetGameHud().GetShipSkillDeck(deckIndex);
			if (shipSkillDeck.m_NKMShipSkillTemplet == null)
			{
				return;
			}
			float num = Mathf.Abs(shipSkillDeck.m_NKMShipSkillTemplet.m_fRange);
			float fY = Mathf.Max(Mathf.Abs(this.m_NKMMapTemplet.m_fMaxZ - this.m_NKMMapTemplet.m_fMinZ), 100f);
			if (shipSkillDeck.m_NKMShipSkillTemplet.m_bFullMap)
			{
				num = this.m_NKMMapTemplet.m_fMaxX - this.m_NKMMapTemplet.m_fMinX;
			}
			this.m_ShipSkillMark.SetScale(num, fY);
			NKCCamera.GetScreenPosToWorldPos(out this.m_Vec3Temp, pos.x, pos.y);
			if (this.m_NKMMapTemplet.m_fMinX > this.m_Vec3Temp.x - num * 0.5f)
			{
				this.m_Vec3Temp.x = this.m_NKMMapTemplet.m_fMinX + num * 0.5f;
			}
			if (this.m_NKMMapTemplet.m_fMaxX < this.m_Vec3Temp.x + num * 0.5f)
			{
				this.m_Vec3Temp.x = this.m_NKMMapTemplet.m_fMaxX - num * 0.5f;
			}
			this.m_ShipSkillMark.SetPos(this.m_Vec3Temp.x, this.m_NKMMapTemplet.m_fMinZ + Mathf.Abs(this.m_NKMMapTemplet.m_fMaxZ - this.m_NKMMapTemplet.m_fMinZ) * 0.5f);
			this.m_ShipSkillMark.SetShow(true);
			this.m_bShipSkillDrag = true;
			this.m_fShipSkillDragPosX = this.m_Vec3Temp.x;
			this.m_SelectShipSkillID = shipSkillDeck.m_NKMShipSkillTemplet.m_ShipSkillID;
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x0010FDE8 File Offset: 0x0010DFE8
		public void RespawnPreviewShipSkillDrag(int deckIndex, Vector2 pos)
		{
			NKCUIHudShipSkillDeck shipSkillDeck = this.GetGameHud().GetShipSkillDeck(deckIndex);
			if (shipSkillDeck.m_NKMShipSkillTemplet == null)
			{
				return;
			}
			float num = Mathf.Abs(shipSkillDeck.m_NKMShipSkillTemplet.m_fRange);
			float fY = Mathf.Max(Mathf.Abs(this.m_NKMMapTemplet.m_fMaxZ - this.m_NKMMapTemplet.m_fMinZ), 100f);
			if (shipSkillDeck.m_NKMShipSkillTemplet.m_bFullMap)
			{
				num = this.m_NKMMapTemplet.m_fMaxX - this.m_NKMMapTemplet.m_fMinX;
			}
			this.m_ShipSkillMark.SetScale(num, fY);
			NKCCamera.GetScreenPosToWorldPos(out this.m_Vec3Temp, pos.x, pos.y);
			if (this.m_Vec3Temp.y - Screen.safeArea.y < this.m_NKMMapTemplet.m_fMinZ - 70f && pos.x > (float)Screen.width * 0.5f)
			{
				this.m_ShipSkillMark.SetShow(false);
				this.m_SelectShipSkillID = 0;
				return;
			}
			if (this.m_NKMMapTemplet.m_fMinX > this.m_Vec3Temp.x - num * 0.5f)
			{
				this.m_Vec3Temp.x = this.m_NKMMapTemplet.m_fMinX + num * 0.5f;
			}
			if (this.m_NKMMapTemplet.m_fMaxX < this.m_Vec3Temp.x + num * 0.5f)
			{
				this.m_Vec3Temp.x = this.m_NKMMapTemplet.m_fMaxX - num * 0.5f;
			}
			this.m_ShipSkillMark.SetPos(this.m_Vec3Temp.x, this.m_NKMMapTemplet.m_fMinZ + Mathf.Abs(this.m_NKMMapTemplet.m_fMaxZ - this.m_NKMMapTemplet.m_fMinZ) * 0.5f);
			this.m_ShipSkillMark.SetShow(true);
			this.m_bShipSkillDrag = true;
			this.m_fShipSkillDragPosX = this.m_Vec3Temp.x;
			this.m_SelectShipSkillID = shipSkillDeck.m_NKMShipSkillTemplet.m_ShipSkillID;
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x0010FFD5 File Offset: 0x0010E1D5
		public void UI_GAME_CAMERA_TOUCH_DOWN()
		{
			this.m_bCameraTouchDown = true;
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x0010FFDE File Offset: 0x0010E1DE
		public void UI_GAME_CAMERA_TOUCH_UP()
		{
			this.m_bCameraTouchDown = false;
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x0010FFE8 File Offset: 0x0010E1E8
		public void OnUnitDeckHotkey(int unitIndex)
		{
			if (!this.m_bCameraDrag && !this.m_bDeckDrag && !this.m_bShipSkillDrag)
			{
				return;
			}
			if (this.m_bDeckDrag || this.m_bShipSkillDrag)
			{
				this.DeckDragCancel();
				this.ShipSkillDeckDragCancel();
				this.ClearUnitViewer();
				this.GetGameHud().UnSelectShipSkillDeck();
			}
			this.OnDragBeginDeck(this.m_vLastDragPos);
			this.OnDragDeck(this.m_vLastDragPos);
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x00110054 File Offset: 0x0010E254
		public void OnShipSkillhotkey(int index)
		{
			if (!this.m_bCameraDrag && !this.m_bDeckDrag && !this.m_bShipSkillDrag)
			{
				return;
			}
			if (this.m_bDeckDrag || this.m_bShipSkillDrag)
			{
				this.DeckDragCancel();
				this.ShipSkillDeckDragCancel();
				this.ClearUnitViewer();
				this.GetGameHud().UnSelectUnitDeck();
			}
			this.OnDragBeginShipSkill(this.m_vLastDragPos);
			this.OnDragShipSkill(this.m_vLastDragPos);
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x001100C0 File Offset: 0x0010E2C0
		public void UI_GAME_CAMERA_DRAG_BEGIN(Vector2 pos)
		{
			this.m_NKCEffectManager.StopCutInEffect();
			this.m_vLastDragPos = pos;
			this.m_bCameraDrag = true;
			if (this.GetGameHud().GetSelectUnitDeckIndex() < 0 && this.GetGameHud().GetSelectShipSkillDeckIndex() < 0)
			{
				if (NKCGameEventManager.IsGameCameraStopRequired())
				{
					return;
				}
				this.SetCameraMode(NKM_GAME_CAMERA_MODE.NGCM_DRAG);
				this.m_fCameraNormalTackingWaitTime = 999999f;
				if (NKCCamera.GetPosNowX(false) < this.m_NKMMapTemplet.m_fCamMinX)
				{
					float num = NKCCamera.GetCameraSizeNow() * NKCCamera.GetCameraAspect() * 0.5f;
					NKCCamera.SetPos(this.m_NKMMapTemplet.m_fCamMinX + num, 0f, -1f, true, false);
				}
				if (NKCCamera.GetPosNowX(false) > this.m_NKMMapTemplet.m_fCamMaxX)
				{
					float num2 = NKCCamera.GetCameraSizeNow() * NKCCamera.GetCameraAspect() * 0.5f;
					NKCCamera.SetPos(this.m_NKMMapTemplet.m_fCamMaxX - num2, 0f, -1f, true, false);
				}
				NKCCamera.StopTrackingCamera();
				this.m_CameraDrag.SetNowValue(NKCCamera.GetPosNowX(false));
				this.m_CameraDragTime = 0f;
				this.m_CameraDragDist = 0f;
				return;
			}
			else
			{
				if (this.GetGameHud().GetSelectUnitDeckIndex() >= 0)
				{
					this.OnDragBeginDeck(pos);
					return;
				}
				if (this.GetGameHud().GetSelectShipSkillDeckIndex() >= 0)
				{
					this.OnDragBeginShipSkill(pos);
				}
				return;
			}
		}

		// Token: 0x06003558 RID: 13656 RVA: 0x00110200 File Offset: 0x0010E400
		private void OnDragBeginDeck(Vector2 pos)
		{
			this.DeckDragCancel();
			this.ShipSkillDeckDragCancel();
			NKCCamera.StopTrackingCamera();
			this.SetCameraMode(NKM_GAME_CAMERA_MODE.NGCM_STOP);
			this.m_fCameraNormalTackingWaitTime = 3f;
			this.ClearUnitViewer();
			long deckUnitUID = this.GetGameHud().GetDeckUnitUID(this.GetGameHud().GetSelectUnitDeckIndex());
			NKMUnitData unitDataByUnitUID = this.m_NKMGameData.GetUnitDataByUnitUID(deckUnitUID);
			if (unitDataByUnitUID != null)
			{
				this.GetGameHud().MoveDeck(this.GetGameHud().GetSelectUnitDeckIndex(), pos.x, pos.y);
				this.m_bDeckDrag = true;
				this.m_DeckSelectUnitTemplet = NKMUnitManager.GetUnitTemplet(unitDataByUnitUID.m_UnitID);
				this.RespawnPreviewBegin(this.GetGameHud().GetSelectUnitDeckIndex(), pos);
			}
		}

		// Token: 0x06003559 RID: 13657 RVA: 0x001102AC File Offset: 0x0010E4AC
		private void OnDragBeginShipSkill(Vector2 pos)
		{
			this.DeckDragCancel();
			this.ShipSkillDeckDragCancel();
			NKCCamera.StopTrackingCamera();
			this.SetCameraMode(NKM_GAME_CAMERA_MODE.NGCM_STOP);
			this.m_fCameraNormalTackingWaitTime = 3f;
			this.m_ShipSkillDeckDragIndex = this.GetGameHud().GetSelectShipSkillDeckIndex();
			if (this.GetGameHud().GetShipSkillDeck(this.m_ShipSkillDeckDragIndex).m_NKMShipSkillTemplet == null)
			{
				return;
			}
			this.GetGameHud().MoveShipSkillDeck(this.m_ShipSkillDeckDragIndex, pos.x, pos.y);
			this.RespawnPreviewShipSkillBegin(this.GetGameHud().GetSelectShipSkillDeckIndex(), pos);
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x00110338 File Offset: 0x0010E538
		public void UI_GAME_CAMERA_DRAG(Vector2 delta, Vector2 pos)
		{
			this.m_vLastDragPos = pos;
			if (this.m_NKM_GAME_CAMERA_MODE == NKM_GAME_CAMERA_MODE.NGCM_DRAG && this.GetGameHud().GetSelectUnitDeckIndex() < 0 && this.GetGameHud().GetSelectShipSkillDeckIndex() < 0)
			{
				if (NKCGameEventManager.IsGameCameraStopRequired())
				{
					return;
				}
				this.SetCameraMode(NKM_GAME_CAMERA_MODE.NGCM_DRAG);
				this.m_fCameraNormalTackingWaitTime = 99999f;
				this.m_fCameraStopDragTime = 0f;
				NKCCamera.SetPosRel(-delta.x, 0f, 0f, true);
				if (NKCCamera.GetPosNowX(false) < this.m_NKMMapTemplet.m_fCamMinX)
				{
					NKCCamera.SetPos(this.m_NKMMapTemplet.m_fCamMinX, 0f, -1f, true, false);
				}
				if (NKCCamera.GetPosNowX(false) > this.m_NKMMapTemplet.m_fCamMaxX)
				{
					NKCCamera.SetPos(this.m_NKMMapTemplet.m_fCamMaxX, 0f, -1f, true, false);
				}
				this.m_CameraDrag.StopTracking();
				this.m_CameraDrag.SetNowValue(NKCCamera.GetPosNowX(false));
				if (this.m_CameraDragPositive && delta.x < 0f)
				{
					this.m_CameraDragTime = 0f;
					this.m_CameraDragDist = 0f;
					this.m_CameraDragPositive = false;
				}
				else if (!this.m_CameraDragPositive && delta.x > 0f)
				{
					this.m_CameraDragTime = 0f;
					this.m_CameraDragDist = 0f;
					this.m_CameraDragPositive = true;
				}
				else if (Mathf.Abs(delta.x) < 1f)
				{
					this.m_CameraDragTime = 0f;
					this.m_CameraDragDist = 0f;
					this.m_CameraDragPositive = true;
				}
				this.m_CameraDragDist += delta.x;
				return;
			}
			else
			{
				if (this.GetGameHud().GetSelectUnitDeckIndex() >= 0)
				{
					this.OnDragDeck(pos);
					return;
				}
				if (this.GetGameHud().GetSelectShipSkillDeckIndex() >= 0)
				{
					this.OnDragShipSkill(pos);
				}
				return;
			}
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x00110508 File Offset: 0x0010E708
		private void OnDragDeck(Vector2 pos)
		{
			NKCCamera.StopTrackingCamera();
			this.SetCameraMode(NKM_GAME_CAMERA_MODE.NGCM_STOP);
			this.m_fCameraNormalTackingWaitTime = 3f;
			this.GetGameHud().MoveDeck(this.GetGameHud().GetSelectUnitDeckIndex(), pos.x, pos.y);
			this.m_bDeckDrag = true;
			this.RespawnPreviewDrag(this.GetGameHud().GetSelectUnitDeckIndex(), pos);
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x00110568 File Offset: 0x0010E768
		private void OnDragShipSkill(Vector2 pos)
		{
			NKCCamera.StopTrackingCamera();
			this.SetCameraMode(NKM_GAME_CAMERA_MODE.NGCM_STOP);
			this.m_fCameraNormalTackingWaitTime = 3f;
			int selectShipSkillDeckIndex = this.GetGameHud().GetSelectShipSkillDeckIndex();
			if (this.m_ShipSkillDeckDragIndex != selectShipSkillDeckIndex)
			{
				return;
			}
			if (this.GetGameHud().GetShipSkillDeck(selectShipSkillDeckIndex).m_NKMShipSkillTemplet == null)
			{
				return;
			}
			this.GetGameHud().MoveShipSkillDeck(selectShipSkillDeckIndex, pos.x, pos.y);
			this.RespawnPreviewShipSkillDrag(this.GetGameHud().GetSelectShipSkillDeckIndex(), pos);
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x001105E0 File Offset: 0x0010E7E0
		public void UI_GAME_CAMERA_DRAG_END(Vector2 delta, Vector2 pos)
		{
			this.m_bCameraDrag = false;
			if (this.m_NKM_GAME_CAMERA_MODE == NKM_GAME_CAMERA_MODE.NGCM_DRAG && this.GetGameHud().GetSelectUnitDeckIndex() < 0 && this.GetGameHud().GetSelectShipSkillDeckIndex() < 0)
			{
				if (NKCGameEventManager.IsGameCameraStopRequired())
				{
					return;
				}
				this.SetCameraMode(NKM_GAME_CAMERA_MODE.NGCM_DRAG);
				this.m_fCameraNormalTackingWaitTime = 3f;
				if (this.m_CameraDragTime > 0f)
				{
					float num = this.m_CameraDragDist / this.m_CameraDragTime;
					this.m_CameraDrag.SetTracking(this.m_CameraDrag.GetNowValue() - num * 0.5f, 1f, TRACKING_DATA_TYPE.TDT_SLOWER);
					return;
				}
			}
			else
			{
				if (this.GetGameHud().GetSelectUnitDeckIndex() >= 0)
				{
					this.DeckDragEnd(this.GetGameHud().GetSelectUnitDeckIndex());
					this.DeckDragCancel();
					this.ShipSkillDeckDragCancel();
					this.ClearUnitViewer();
					this.GetGameHud().UnSelectUnitDeck();
					this.GetGameHud().UnSelectShipSkillDeck();
					return;
				}
				if (this.GetGameHud().GetSelectShipSkillDeckIndex() >= 0)
				{
					this.ShipSkillDeckDragEnd(this.GetGameHud().GetSelectShipSkillDeckIndex());
					this.GetGameHud().UnSelectUnitDeck();
					this.GetGameHud().UnSelectShipSkillDeck();
				}
			}
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x001106F8 File Offset: 0x0010E8F8
		public void QuickCamMove(NKM_GAME_QUICK_MOVE eNKM_GAME_QUICK_MOVE)
		{
			if (NKCGameEventManager.IsGameCameraStopRequired())
			{
				return;
			}
			if (this.GetGameHud().GetSelectUnitDeckIndex() < 0 && this.GetGameHud().GetSelectShipSkillDeckIndex() < 0)
			{
				this.m_NKCEffectManager.StopCutInEffect();
				this.SetCameraMode(NKM_GAME_CAMERA_MODE.NGCM_DRAG);
				this.m_fCameraNormalTackingWaitTime = 3f;
				float num = NKCCamera.GetCameraSizeNow() * NKCCamera.GetCameraAspect() * 0.5f;
				switch (eNKM_GAME_QUICK_MOVE)
				{
				case NKM_GAME_QUICK_MOVE.NGCQM_LEFT_END:
					NKCCamera.SetPos(this.m_NKMMapTemplet.m_fCamMinX + num, 0f, -1f, true, false);
					break;
				case NKM_GAME_QUICK_MOVE.NGCQM_LEFT:
					NKCCamera.SetPos(this.GetTrackingCamPosX(false), 0f, -1f, true, false);
					break;
				case NKM_GAME_QUICK_MOVE.NGCQM_RIGHT:
					NKCCamera.SetPos(this.GetTrackingCamPosX(true), 0f, -1f, true, false);
					break;
				case NKM_GAME_QUICK_MOVE.NGCQM_RIGHT_END:
					NKCCamera.SetPos(this.m_NKMMapTemplet.m_fCamMaxX - num, 0f, -1f, true, false);
					break;
				}
				NKCCamera.StopTrackingCamera();
				this.m_CameraDrag.SetNowValue(NKCCamera.GetPosNowX(false));
				this.m_CameraDragTime = 0f;
				this.m_CameraDragDist = 0f;
			}
		}

		// Token: 0x0600355F RID: 13663 RVA: 0x00110814 File Offset: 0x0010EA14
		public void UnlockTutorialReRespawn()
		{
			this.m_bTutorialGameReRespawnAllowed = true;
		}

		// Token: 0x06003560 RID: 13664 RVA: 0x00110820 File Offset: 0x0010EA20
		public void TutorialForceCamMove(float mapPositionRatio)
		{
			if (this.GetGameHud().GetSelectUnitDeckIndex() < 0 && this.GetGameHud().GetSelectShipSkillDeckIndex() < 0)
			{
				mapPositionRatio = Mathf.Clamp01(mapPositionRatio);
				this.m_NKCEffectManager.StopCutInEffect();
				this.SetCameraMode(NKM_GAME_CAMERA_MODE.NGCM_DRAG);
				this.m_fCameraNormalTackingWaitTime = 3f;
				float num = NKCCamera.GetCameraSizeNow() * NKCCamera.GetCameraAspect() * 0.5f;
				float a = this.m_NKMMapTemplet.m_fCamMinX + num;
				float b = this.m_NKMMapTemplet.m_fCamMaxX - num;
				NKCCamera.SetPos(Mathf.Lerp(a, b, mapPositionRatio), 0f, -1f, true, false);
				NKCCamera.StopTrackingCamera();
				this.m_CameraDrag.SetNowValue(NKCCamera.GetPosNowX(false));
				this.m_CameraDragTime = 0f;
				this.m_CameraDragDist = 0f;
			}
		}

		// Token: 0x06003561 RID: 13665 RVA: 0x001108E5 File Offset: 0x0010EAE5
		public void UI_HUD_DECK_DOWN(int index)
		{
			this.DeckDragCancel();
			this.ShipSkillDeckDragCancel();
			this.ClearUnitViewer();
			this.GetGameHud().TouchDownDeck(index);
			this.m_bDeckTouchDown = true;
		}

		// Token: 0x06003562 RID: 13666 RVA: 0x0011090C File Offset: 0x0010EB0C
		public void UI_HUD_DECK_UP(int index)
		{
			this.GetGameHud().TouchUpDeck(index, true);
			if (this.GetGameHud().GetSelectUnitDeckIndex() < 0)
			{
				this.GetGameHud().ReturnDeck(index);
				this.ClearUnitViewer();
			}
			this.m_bDeckTouchDown = false;
		}

		// Token: 0x06003563 RID: 13667 RVA: 0x00110944 File Offset: 0x0010EB44
		public void UI_HUD_DECK_DRAG_BEGIN(GameObject deckObject, Vector2 pos)
		{
			this.DeckDragCancel();
			this.ShipSkillDeckDragCancel();
			this.m_NKCEffectManager.StopCutInEffect();
			NKCCamera.StopTrackingCamera();
			this.SetCameraMode(NKM_GAME_CAMERA_MODE.NGCM_STOP);
			this.m_fCameraNormalTackingWaitTime = 3f;
			this.ClearUnitViewer();
			this.m_DeckDragIndex = this.GetGameHud().GetDeckIndex(deckObject);
			if (!this.EnableControlByGameType(NKM_ERROR_CODE.NEC_FAIL_ASYNC_PVP_MANUAL_PLAY_DISABLE))
			{
				return;
			}
			long deckUnitUID = this.GetGameHud().GetDeckUnitUID(this.m_DeckDragIndex);
			NKMUnitData unitDataByUnitUID = this.m_NKMGameData.GetUnitDataByUnitUID(deckUnitUID);
			if (unitDataByUnitUID != null)
			{
				this.GetGameHud().MoveDeck(this.m_DeckDragIndex, pos.x, pos.y);
				this.m_bDeckDrag = true;
				this.m_DeckSelectUnitTemplet = NKMUnitManager.GetUnitTemplet(unitDataByUnitUID.m_UnitID);
			}
			this.RespawnPreviewBegin(this.m_DeckDragIndex, pos);
		}

		// Token: 0x06003564 RID: 13668 RVA: 0x00110A0C File Offset: 0x0010EC0C
		public void UI_HUD_DECK_DRAG(GameObject deckObject, Vector2 deckPos, Vector2 charPos)
		{
			NKCCamera.StopTrackingCamera();
			this.SetCameraMode(NKM_GAME_CAMERA_MODE.NGCM_STOP);
			this.m_fCameraNormalTackingWaitTime = 3f;
			int deckIndex = this.GetGameHud().GetDeckIndex(deckObject);
			if (this.m_DeckDragIndex != deckIndex)
			{
				return;
			}
			if (!this.EnableControlByGameType(NKM_ERROR_CODE.NEC_OK))
			{
				return;
			}
			this.GetGameHud().MoveDeck(deckIndex, deckPos.x, deckPos.y);
			this.m_bDeckDrag = true;
			this.RespawnPreviewDrag(deckIndex, charPos);
		}

		// Token: 0x06003565 RID: 13669 RVA: 0x00110A78 File Offset: 0x0010EC78
		public void UI_HUD_DECK_DRAG_END(GameObject deckObject, Vector2 pos)
		{
			int deckIndex = this.GetGameHud().GetDeckIndex(deckObject);
			if (this.m_DeckDragIndex != deckIndex)
			{
				return;
			}
			this.DeckDragEnd(deckIndex);
			this.DeckDragCancel();
			this.ShipSkillDeckDragCancel();
			this.ClearUnitViewer();
			this.GetGameHud().UnSelectUnitDeck();
			this.GetGameHud().UnSelectShipSkillDeck();
		}

		// Token: 0x06003566 RID: 13670 RVA: 0x00110ACC File Offset: 0x0010ECCC
		private void DeckDragCancel()
		{
			if (this.GetGameHud().GetSelectUnitDeckIndex() >= 0)
			{
				this.GetGameHud().ReturnDeck(this.GetGameHud().GetSelectUnitDeckIndex());
			}
			if (this.GetGameHud().GetSelectShipSkillDeckIndex() >= 0)
			{
				this.GetGameHud().ReturnShipSkillDeck(this.GetGameHud().GetSelectShipSkillDeckIndex());
			}
			if (this.m_DeckDragIndex >= 0)
			{
				this.GetGameHud().ReturnDeck(this.m_DeckDragIndex);
			}
			this.ClearUnitViewer();
			this.m_bDeckSelectUnitUID = 0L;
			this.m_DeckSelectUnitTemplet = null;
			this.m_DeckDragIndex = -1;
			this.m_bDeckDrag = false;
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x00110B60 File Offset: 0x0010ED60
		private bool DeckDragEnd(int deckIndex)
		{
			bool result = false;
			this.GetGameHud().ReturnDeck(deckIndex);
			if (!this.EnableControlByGameType(NKM_ERROR_CODE.NEC_OK))
			{
				this.ClearUnitViewer();
				this.m_bDeckDrag = false;
				return result;
			}
			if (this.m_bDeckSelectUnitUID > 0L)
			{
				if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE != NKM_GAME_STATE.NGS_PLAY)
				{
					this.GetGameHud().SetMessage(NKCUtilString.GET_STRING_INGAME_RESPAWN_FAIL_STATE, -1f);
					this.ClearUnitViewer();
					this.m_bDeckDrag = false;
					return result;
				}
				if (!base.IsGameUnitAllDie(this.m_bDeckSelectUnitUID) && !this.CanReRespawn())
				{
					this.GetGameHud().SetMessage(NKCUtilString.GET_STRING_INGAME_RESPAWN_FAIL_ALREADY_SPAWN, -1f);
					this.ClearUnitViewer();
					this.m_bDeckDrag = false;
					return result;
				}
				NKMUnitData unitDataByUnitUID = this.GetMyTeamData().GetUnitDataByUnitUID(this.m_bDeckSelectUnitUID);
				if (unitDataByUnitUID == null)
				{
					this.GetGameHud().SetMessage(NKCUtilString.GET_STRING_INGAME_RESPAWN_FAIL_UNIT_DATA, -1f);
					this.ClearUnitViewer();
					this.m_bDeckDrag = false;
					return result;
				}
				NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitDataByUnitUID.m_UnitID);
				if (unitStatTemplet == null)
				{
					this.GetGameHud().SetMessage(NKCUtilString.GET_STRING_INGAME_RESPAWN_FAIL_UNIT_DATA, -1f);
					this.ClearUnitViewer();
					this.m_bDeckDrag = false;
					return result;
				}
				bool flag = this.GetMyTeamData().IsAssistUnit(unitDataByUnitUID.m_UnitUID);
				if (flag && this.GetMyTeamData().m_DeckData.GetAutoRespawnIndexAssist() == -1)
				{
					this.ClearUnitViewer();
					this.m_bDeckDrag = false;
					return result;
				}
				int respawnCost;
				if (this.m_bDeckSelectUnitUID == this.GetMyTeamData().m_LeaderUnitUID && !flag)
				{
					respawnCost = base.GetRespawnCost(unitStatTemplet, true, this.GetMyTeamData().m_eNKM_TEAM_TYPE);
				}
				else
				{
					respawnCost = base.GetRespawnCost(unitStatTemplet, false, this.GetMyTeamData().m_eNKM_TEAM_TYPE);
				}
				float num;
				if (!flag)
				{
					num = this.GetMyRespawnCostClient();
				}
				else
				{
					num = this.GetMyRespawnCostAssistClient();
				}
				if (num < (float)respawnCost)
				{
					this.GetGameHud().SetMessage(NKCUtilString.GET_STRING_INGAME_RESPAWN_FAIL_COST, -1f);
					this.ClearUnitViewer();
					this.m_bDeckDrag = false;
					this.PlayOperatorVoiceMyTeam(VOICE_TYPE.VT_COST_LACK);
					return result;
				}
				float fFactor = this.m_fRespawnValidLandTeamA;
				if (base.IsATeam(this.m_MyTeam))
				{
					fFactor = this.m_fRespawnValidLandTeamA;
				}
				else if (base.IsBTeam(this.m_MyTeam))
				{
					fFactor = this.m_fRespawnValidLandTeamB;
				}
				bool flag2 = false;
				if (this.m_DeckSelectUnitTemplet != null && this.m_DeckSelectUnitTemplet.m_UnitTempletBase.m_bRespawnFreePos)
				{
					flag2 = true;
				}
				if (base.GetDungeonTemplet() != null && base.GetDungeonTemplet().m_bRespawnFreePos)
				{
					flag2 = true;
				}
				if (flag2)
				{
					fFactor = 0.8f;
				}
				if (!this.m_NKMMapTemplet.IsValidLandX(this.m_bDeckSelectPosX, true, fFactor))
				{
					this.GetGameHud().SetMessage(NKCUtilString.GET_STRING_INGAME_RESPAWN_FAIL_MAP, -1f);
					this.ClearUnitViewer();
					this.m_bDeckDrag = false;
					return result;
				}
				if (!flag)
				{
					this.AddCostHolder(unitDataByUnitUID.m_UnitUID, (float)respawnCost);
					this.GetGameHud().SetRespawnCost();
				}
				else
				{
					this.AddCostHolderAssist(unitDataByUnitUID.m_UnitUID, 10f);
					this.GetGameHud().SetRespawnCostAssist();
				}
				NKMUnitData unitDataByUnitUID2 = this.m_NKMGameData.GetUnitDataByUnitUID(this.m_bDeckSelectUnitUID);
				if (unitDataByUnitUID2 != null)
				{
					for (int i = 0; i < unitDataByUnitUID2.m_listGameUnitUID.Count; i++)
					{
						short key = unitDataByUnitUID2.m_listGameUnitUID[i];
						if (this.m_dicUnitViewer.ContainsKey(key))
						{
							NKCUnitViewer nkcunitViewer = this.m_dicUnitViewer[key];
							if (nkcunitViewer != null)
							{
								nkcunitViewer.SetRespawnReady(true);
								if (i == 0)
								{
									float num2 = this.m_fGameTimeDifference;
									if (NKMCommonConst.USE_ROLLBACK && base.IsGameUnitAllDie(this.m_bDeckSelectUnitUID))
									{
										NKMUnitTemplet unitTemplet = NKMUnitManager.GetUnitTemplet(unitDataByUnitUID2.m_UnitID);
										num2 -= base.GetRollbackTime(unitTemplet);
									}
									nkcunitViewer.PlayTimer(num2);
								}
							}
						}
					}
				}
				this.GetGameHud().UseDeck(deckIndex, false);
				if (!flag)
				{
					this.Send_Packet_GAME_RESPAWN_REQ(this.m_bDeckSelectUnitUID, false, this.m_bDeckSelectPosX, this.m_NKMGameRuntimeData.m_GameTime);
				}
				else
				{
					this.Send_Packet_GAME_RESPAWN_REQ(-1L, true, this.m_bDeckSelectPosX, this.m_NKMGameRuntimeData.m_GameTime);
				}
				result = true;
			}
			this.m_bDeckDrag = false;
			return result;
		}

		// Token: 0x06003568 RID: 13672 RVA: 0x00110F40 File Offset: 0x0010F140
		private void AddCostHolder(long unitUID, float fCost)
		{
			if (!this.m_dicRespawnCostHolder.ContainsKey(unitUID))
			{
				this.m_dicRespawnCostHolder.Add(unitUID, fCost);
				return;
			}
			float num = this.m_dicRespawnCostHolder[unitUID];
			this.m_dicRespawnCostHolder[unitUID] = num + fCost;
		}

		// Token: 0x06003569 RID: 13673 RVA: 0x00110F88 File Offset: 0x0010F188
		public void AddCostHolderTC(short TCID, float fCost)
		{
			if (!this.m_dicRespawnCostHolderTC.ContainsKey((long)TCID))
			{
				this.m_dicRespawnCostHolderTC.Add((long)TCID, fCost);
				return;
			}
			float num = this.m_dicRespawnCostHolderTC[(long)TCID];
			this.m_dicRespawnCostHolderTC[(long)TCID] = num + fCost;
		}

		// Token: 0x0600356A RID: 13674 RVA: 0x00110FD4 File Offset: 0x0010F1D4
		private void RemoveCostHolder(long unitUID, float fCost)
		{
			if (this.m_dicRespawnCostHolder.ContainsKey(unitUID))
			{
				float num = this.m_dicRespawnCostHolder[unitUID] - fCost;
				if (num < 0f)
				{
					num = 0f;
				}
				this.m_dicRespawnCostHolder[unitUID] = num;
			}
		}

		// Token: 0x0600356B RID: 13675 RVA: 0x00111019 File Offset: 0x0010F219
		private void RemoveCostHolderTC(short TCID)
		{
			if (this.m_dicRespawnCostHolderTC.ContainsKey((long)TCID))
			{
				this.m_dicRespawnCostHolderTC[(long)TCID] = 0f;
			}
		}

		// Token: 0x0600356C RID: 13676 RVA: 0x0011103C File Offset: 0x0010F23C
		private void AddCostHolderAssist(long unitUID, float fCost)
		{
			if (!this.m_dicRespawnCostHolderAssist.ContainsKey(unitUID))
			{
				this.m_dicRespawnCostHolderAssist.Add(unitUID, fCost);
				return;
			}
			float num = this.m_dicRespawnCostHolderAssist[unitUID];
			this.m_dicRespawnCostHolderAssist[unitUID] = num + fCost;
		}

		// Token: 0x0600356D RID: 13677 RVA: 0x00111084 File Offset: 0x0010F284
		private void RemoveCostHolderAssist(long unitUID, float fCost)
		{
			if (this.m_dicRespawnCostHolderAssist.ContainsKey(unitUID))
			{
				float num = this.m_dicRespawnCostHolderAssist[unitUID] - fCost;
				if (num < 0f)
				{
					num = 0f;
				}
				this.m_dicRespawnCostHolderAssist[unitUID] = num;
			}
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x001110CC File Offset: 0x0010F2CC
		private void ClearUnitViewer()
		{
			this.m_listUnitViewerRemove.Clear();
			foreach (KeyValuePair<short, NKCUnitViewer> keyValuePair in this.m_dicUnitViewer)
			{
				NKCUnitViewer value = keyValuePair.Value;
				if (value != null && !value.GetRespawnReady())
				{
					value.SetActiveSprite(false);
					value.SetActiveShadow(false);
					value.StopTimer();
					List<short> listUnitViewerRemove = this.m_listUnitViewerRemove;
					Dictionary<short, NKCUnitViewer>.Enumerator enumerator;
					keyValuePair = enumerator.Current;
					listUnitViewerRemove.Add(keyValuePair.Key);
				}
			}
			for (int i = 0; i < this.m_listUnitViewerRemove.Count; i++)
			{
				this.m_dicUnitViewer.Remove(this.m_listUnitViewerRemove[i]);
			}
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x00111174 File Offset: 0x0010F374
		private void RespawnCompleteUnitViewer(long unitUID)
		{
			NKMUnitData cNKMUnitData;
			if (unitUID == -1L)
			{
				for (int i = 0; i < this.GetMyTeamData().m_listAssistUnitData.Count; i++)
				{
					cNKMUnitData = this.GetMyTeamData().m_listAssistUnitData[i];
					this.RespawnCompleteUnitViewer(cNKMUnitData);
				}
				return;
			}
			cNKMUnitData = this.m_NKMGameData.GetUnitDataByUnitUID(unitUID);
			this.RespawnCompleteUnitViewer(cNKMUnitData);
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x001111D4 File Offset: 0x0010F3D4
		private void RespawnCompleteUnitViewer(NKMUnitData cNKMUnitData)
		{
			if (cNKMUnitData != null)
			{
				for (int i = 0; i < cNKMUnitData.m_listGameUnitUID.Count; i++)
				{
					short key = cNKMUnitData.m_listGameUnitUID[i];
					if (this.m_dicUnitViewer.ContainsKey(key))
					{
						NKCUnitViewer nkcunitViewer = this.m_dicUnitViewer[key];
						nkcunitViewer.SetRespawnReady(false);
						nkcunitViewer.SetActiveSprite(false);
						nkcunitViewer.SetActiveShadow(false);
						nkcunitViewer.StopTimer();
						this.m_dicUnitViewer.Remove(key);
					}
				}
			}
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x00111248 File Offset: 0x0010F448
		public void SetShowUI(bool bShowUI, bool bDev)
		{
			if (this.m_bShowUI == bShowUI)
			{
				return;
			}
			this.m_bShowUI = bShowUI;
			this.GetGameHud().SetShowUI(this.m_bShowUI, bDev);
			this.GetGameHud().SetLeftMenu(this.m_NKMGameRuntimeData);
			foreach (KeyValuePair<short, NKMUnit> keyValuePair in this.m_dicNKMUnit)
			{
				((NKCUnitClient)keyValuePair.Value).SetShowUI();
			}
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x001112B9 File Offset: 0x0010F4B9
		public void UI_HUD_SHIP_SKILL_DECK_DOWN(int index, Vector2 touchPos)
		{
			if (this.GetGameHud().GetShipSkillDeck(index).m_NKMShipSkillTemplet == null)
			{
				return;
			}
			this.GetGameHud().ShowTooltip(index, touchPos);
			this.GetGameHud().TouchDownShipSkillDeck(index);
			this.m_bShipSkillDeckTouchDown = true;
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x001112F0 File Offset: 0x0010F4F0
		public void UI_HUD_SHIP_SKILL_DECK_UP(int index)
		{
			this.GetGameHud().GetShipSkillDeck(index);
			this.GetGameHud().TouchUpShipSkillDeck(index);
			if (this.GetGameHud().GetSelectShipSkillDeckIndex() < 0)
			{
				this.GetGameHud().ReturnShipSkillDeck(index);
				this.ShipSkillDeckDragCancel();
			}
			this.m_bShipSkillDeckTouchDown = false;
		}

		// Token: 0x06003574 RID: 13684 RVA: 0x00111340 File Offset: 0x0010F540
		public void UI_HUD_SHIP_SKILL_DECK_DRAG_BEGIN(GameObject deckObject, Vector2 pos)
		{
			this.DeckDragCancel();
			this.ShipSkillDeckDragCancel();
			this.m_NKCEffectManager.StopCutInEffect();
			NKCCamera.StopTrackingCamera();
			this.SetCameraMode(NKM_GAME_CAMERA_MODE.NGCM_STOP);
			this.m_fCameraNormalTackingWaitTime = 3f;
			this.GetGameHud().CloseTooltip();
			this.m_ShipSkillDeckDragIndex = this.GetGameHud().GetShipSkillDeckIndex(deckObject);
			NKCUIHudShipSkillDeck shipSkillDeck = this.GetGameHud().GetShipSkillDeck(this.m_ShipSkillDeckDragIndex);
			if (shipSkillDeck.m_NKMShipSkillTemplet == null || shipSkillDeck.m_NKMShipSkillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_PASSIVE)
			{
				return;
			}
			if (!this.EnableControlByGameType(NKM_ERROR_CODE.NEC_FAIL_ASYNC_PVP_MANUAL_PLAY_DISABLE))
			{
				return;
			}
			this.GetGameHud().MoveShipSkillDeck(this.m_ShipSkillDeckDragIndex, pos.x, pos.y);
			this.RespawnPreviewShipSkillBegin(this.m_ShipSkillDeckDragIndex, pos);
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x001113FC File Offset: 0x0010F5FC
		public void UI_HUD_SHIP_SKILL_DECK_DRAG(GameObject deckObject, Vector2 pos)
		{
			NKCCamera.StopTrackingCamera();
			this.SetCameraMode(NKM_GAME_CAMERA_MODE.NGCM_STOP);
			this.m_fCameraNormalTackingWaitTime = 3f;
			int shipSkillDeckIndex = this.GetGameHud().GetShipSkillDeckIndex(deckObject);
			if (this.m_ShipSkillDeckDragIndex != shipSkillDeckIndex)
			{
				return;
			}
			NKCUIHudShipSkillDeck shipSkillDeck = this.GetGameHud().GetShipSkillDeck(shipSkillDeckIndex);
			if (shipSkillDeck.m_NKMShipSkillTemplet == null || shipSkillDeck.m_NKMShipSkillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_PASSIVE || !this.EnableControlByGameType(NKM_ERROR_CODE.NEC_OK))
			{
				return;
			}
			this.GetGameHud().MoveShipSkillDeck(shipSkillDeckIndex, pos.x, pos.y);
			this.RespawnPreviewShipSkillDrag(shipSkillDeckIndex, pos);
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x00111484 File Offset: 0x0010F684
		public void UI_HUD_SHIP_SKILL_DECK_DRAG_END(GameObject deckObject, Vector2 pos)
		{
			int shipSkillDeckIndex = this.GetGameHud().GetShipSkillDeckIndex(deckObject);
			if (this.m_ShipSkillDeckDragIndex != shipSkillDeckIndex)
			{
				return;
			}
			this.ShipSkillDeckDragEnd(shipSkillDeckIndex);
			this.GetGameHud().UnSelectUnitDeck();
			this.GetGameHud().UnSelectShipSkillDeck();
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x001114C8 File Offset: 0x0010F6C8
		private bool ShipSkillDeckDragEnd(int deckIndex)
		{
			bool result = false;
			NKCUIHudShipSkillDeck shipSkillDeck = this.GetGameHud().GetShipSkillDeck(deckIndex);
			if (shipSkillDeck.m_NKMShipSkillTemplet == null || shipSkillDeck.m_NKMShipSkillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_PASSIVE || !this.EnableControlByGameType(NKM_ERROR_CODE.NEC_OK))
			{
				return false;
			}
			this.GetGameHud().ReturnShipSkillDeck(deckIndex);
			if (this.m_SelectShipSkillID > 0)
			{
				if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE != NKM_GAME_STATE.NGS_PLAY)
				{
					this.GetGameHud().SetMessage(NKCUtilString.GET_STRING_INGAME_SHIP_SKILL_FAIL_STATE, -1f);
					this.m_ShipSkillMark.SetShow(false);
					this.m_bShipSkillDrag = false;
					return false;
				}
				NKMUnit liveBossUnit = base.GetLiveBossUnit(this.m_MyTeam);
				if (liveBossUnit == null)
				{
					this.GetGameHud().SetMessage(NKCUtilString.GET_STRING_INGAME_SHIP_SKILL_FAIL_DIE, -1f);
					this.m_ShipSkillMark.SetShow(false);
					this.m_bShipSkillDrag = false;
					return false;
				}
				NKMUnitState unitStateNow = liveBossUnit.GetUnitStateNow();
				if (unitStateNow == null)
				{
					this.GetGameHud().SetMessage(NKCUtilString.GET_STRING_INGAME_SHIP_SKILL_FAIL_STATE, -1f);
					this.m_ShipSkillMark.SetShow(false);
					this.m_bShipSkillDrag = false;
					return false;
				}
				if (unitStateNow.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_HYPER || unitStateNow.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_SKILL)
				{
					this.GetGameHud().SetMessage(NKCUtilString.GET_STRING_INGAME_SHIP_SKILL_FAIL_USE_OTHER_SKILL, -1f);
					this.m_ShipSkillMark.SetShow(false);
					this.m_bShipSkillDrag = false;
					return false;
				}
				NKMUnit nkmunit = null;
				NKMGameTeamData myTeamData = this.GetMyTeamData();
				if (myTeamData != null && myTeamData.m_MainShip != null)
				{
					nkmunit = this.GetUnit(myTeamData.m_MainShip.m_listGameUnitUID[0], true, false);
				}
				if (nkmunit == null)
				{
					this.m_ShipSkillMark.SetShow(false);
					this.m_bShipSkillDrag = false;
					return false;
				}
				if (nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_PLAY || nkmunit.GetUnitSyncData().GetHP() <= 0f)
				{
					this.m_ShipSkillMark.SetShow(false);
					this.m_bShipSkillDrag = false;
					return false;
				}
				if (nkmunit.GetStateCoolTime(shipSkillDeck.m_NKMShipSkillTemplet.m_UnitStateName) > 0f)
				{
					this.GetGameHud().SetMessage(NKCUtilString.GET_STRING_INGAME_SHIP_SKILL_FAIL_COOLTIME, -1f);
					this.m_ShipSkillMark.SetShow(false);
					this.m_bShipSkillDrag = false;
					return false;
				}
				if (nkmunit.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_SILENCE))
				{
					this.GetGameHud().SetMessage(NKCUtilString.GET_STRING_INGAME_SHIP_SKILL_FAIL_SILENCE, -1f);
					this.m_ShipSkillMark.SetShow(false);
					this.m_bShipSkillDrag = false;
					return false;
				}
				if (nkmunit.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_SLEEP))
				{
					this.GetGameHud().SetMessage(NKCUtilString.GET_STRING_INGAME_SHIP_SKILL_FAIL_SLEEP, -1f);
					this.m_ShipSkillMark.SetShow(false);
					this.m_bShipSkillDrag = false;
					return false;
				}
				if (nkmunit.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_FEAR) || nkmunit.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_FREEZE) || nkmunit.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_HOLD))
				{
					this.GetGameHud().SetMessage(NKCUtilString.GET_STRING_INGAME_SHIP_SKILL_FAIL_STATE, -1f);
					this.m_ShipSkillMark.SetShow(false);
					this.m_bShipSkillDrag = false;
					return false;
				}
				this.GetGameHud().UseShipSkillDeck(deckIndex);
				this.Send_Packet_SHIP_SKILL_REQ(this.m_SelectShipSkillID, this.m_fShipSkillDragPosX);
				result = true;
			}
			this.m_bShipSkillDrag = false;
			return result;
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x001117A1 File Offset: 0x0010F9A1
		private void ShipSkillDeckDragCancel()
		{
			if (this.m_ShipSkillDeckDragIndex < 0)
			{
				return;
			}
			this.GetGameHud().ReturnShipSkillDeck(this.m_ShipSkillDeckDragIndex);
			this.m_ShipSkillMark.SetShow(false);
			this.m_bShipSkillDrag = false;
			this.m_DeckDragIndex = -1;
			this.m_SelectShipSkillID = 0;
		}

		// Token: 0x06003579 RID: 13689 RVA: 0x001117DF File Offset: 0x0010F9DF
		public SpriteRenderer GetMapInvalidLandRenderer()
		{
			return this.m_Map.GetInvalidLandRenderer();
		}

		// Token: 0x0600357A RID: 13690 RVA: 0x001117EC File Offset: 0x0010F9EC
		public void UI_HUD_AUTO_RESPAWN_TOGGLE()
		{
			NKMGameRuntimeTeamData myRuntimeTeamData = this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(this.m_MyTeam);
			if (myRuntimeTeamData != null)
			{
				this.Send_Packet_GAME_AUTO_RESPAWN_REQ(!myRuntimeTeamData.m_bAutoRespawn, true);
			}
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x00111820 File Offset: 0x0010FA20
		public void UI_HUD_ACTION_CAMERA_TOGGLE()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			this.Send_Packet_GAME_OPTION_CHANGE_REQ(!gameOptionData.ActionCamera, gameOptionData.TrackCamera, gameOptionData.ViewSkillCutIn);
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x00111858 File Offset: 0x0010FA58
		public void UI_HUD_TRACK_CAMERA_TOGGLE()
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			this.Send_Packet_GAME_OPTION_CHANGE_REQ(gameOptionData.ActionCamera, !gameOptionData.TrackCamera, gameOptionData.ViewSkillCutIn);
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x00111890 File Offset: 0x0010FA90
		public void UI_GAME_PAUSE()
		{
			if (NKCReplayMgr.IsPlayingReplay())
			{
				bool flag = false;
				if (this.GetGameHud().IsOpenPause())
				{
					flag = true;
				}
				NKMPacket_GAME_PAUSE_ACK nkmpacket_GAME_PAUSE_ACK = new NKMPacket_GAME_PAUSE_ACK();
				nkmpacket_GAME_PAUSE_ACK.errorCode = NKM_ERROR_CODE.NEC_OK;
				nkmpacket_GAME_PAUSE_ACK.isPause = !this.m_NKMGameRuntimeData.m_bPause;
				nkmpacket_GAME_PAUSE_ACK.isPauseEvent = false;
				this.m_NKC_OPEN_POPUP_TYPE_AFTER_PAUSE = NKC_OPEN_POPUP_TYPE_AFTER_PAUSE.NOPTAP_GAME_OPTION_POPUP;
				this.OnRecv(nkmpacket_GAME_PAUSE_ACK);
				if (flag)
				{
					this.GetGameHud().RefreshClassGuide();
				}
				return;
			}
			if (this.m_NKMGameData.m_bLocal || (this.m_NKMGameData.IsPVE() && !this.m_NKMGameData.IsGuildDungeon()))
			{
				if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE != NKM_GAME_STATE.NGS_START && this.m_NKMGameRuntimeData.m_NKM_GAME_STATE != NKM_GAME_STATE.NGS_PLAY)
				{
					return;
				}
				if (NKCGameEventManager.IsEventPlaying())
				{
					return;
				}
				if (this.GetGameHud().IsOpenPause())
				{
					this.GetGameHud().OnClickContinueOnPause();
					return;
				}
				NKMPopUpBox.OpenWaitBox(0f, "");
				bool bPause = !this.m_NKMGameRuntimeData.m_bPause;
				this.Send_Packet_GAME_PAUSE_REQ(bPause, false, NKC_OPEN_POPUP_TYPE_AFTER_PAUSE.NOPTAP_GAME_OPTION_POPUP);
				return;
			}
			else
			{
				if (this.IsObserver(NKCScenManager.CurrentUserData()))
				{
					NKCUIGameOption.Instance.Open(NKC_GAME_OPTION_MENU_TYPE.OBSERVE, delegate
					{
						this.GetGameHud().RefreshClassGuide();
					});
					return;
				}
				NKCUIGameOption.Instance.Open(NKC_GAME_OPTION_MENU_TYPE.NORMAL, delegate
				{
					this.GetGameHud().RefreshClassGuide();
				});
				return;
			}
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x001119C3 File Offset: 0x0010FBC3
		public void UI_GAME_SKILL_NORMAL_COOL_RESET(bool bEnemy = false)
		{
			if (!this.m_NKMGameData.m_bLocal)
			{
				return;
			}
			if (!bEnemy)
			{
				this.Send_Packet_GAME_DEV_COOL_TIME_RESET_REQ(true, NKM_TEAM_TYPE.NTT_A1);
				return;
			}
			this.Send_Packet_GAME_DEV_COOL_TIME_RESET_REQ(true, NKM_TEAM_TYPE.NTT_B1);
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x001119E7 File Offset: 0x0010FBE7
		public void UI_GAME_SKILL_HYPER_COOL_RESET(bool bEnemy = false)
		{
			if (!this.m_NKMGameData.m_bLocal)
			{
				return;
			}
			if (!bEnemy)
			{
				this.Send_Packet_GAME_DEV_COOL_TIME_RESET_REQ(false, NKM_TEAM_TYPE.NTT_A1);
				return;
			}
			this.Send_Packet_GAME_DEV_COOL_TIME_RESET_REQ(false, NKM_TEAM_TYPE.NTT_B1);
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x00111A0C File Offset: 0x0010FC0C
		public NKCUnitClient RespawnUnit(NKMUnitSyncData cNKMUnitSyncData)
		{
			NKCUnitClient nkcunitClient = (NKCUnitClient)this.GetUnit(cNKMUnitSyncData.m_GameUnitUID, true, true);
			if (nkcunitClient != null)
			{
				if (this.m_dicNKMUnitPool.ContainsKey(cNKMUnitSyncData.m_GameUnitUID))
				{
					this.m_dicNKMUnitPool.Remove(cNKMUnitSyncData.m_GameUnitUID);
				}
				if (!this.m_dicNKMUnit.ContainsKey(cNKMUnitSyncData.m_GameUnitUID))
				{
					this.m_dicNKMUnit.Add(cNKMUnitSyncData.m_GameUnitUID, nkcunitClient);
				}
				if (!this.m_listNKMUnit.Contains(nkcunitClient))
				{
					this.m_listNKMUnit.Add(nkcunitClient);
				}
				nkcunitClient.RespawnUnit(cNKMUnitSyncData.m_PosX, cNKMUnitSyncData.m_PosZ, cNKMUnitSyncData.m_JumpYPos, false, true, 0f, false, 0f);
				bool flag = this.GetMyTeamData().IsAssistUnit(nkcunitClient.GetUnitData().m_UnitUID);
				bool bReturnDeckActive = true;
				if (flag && this.GetMyTeamData().m_DeckData.GetAutoRespawnIndexAssist() < 0)
				{
					bReturnDeckActive = false;
				}
				this.GetGameHud().UseCompleteDeckByUnitUID(nkcunitClient.GetUnitData().m_UnitUID, bReturnDeckActive);
				if (flag)
				{
					this.GetGameHud().UseCompleteDeckAssist(bReturnDeckActive);
				}
				if (this.m_dicUnitViewer.ContainsKey(nkcunitClient.GetUnitDataGame().m_GameUnitUID))
				{
					this.m_dicUnitViewer[nkcunitClient.GetUnitDataGame().m_GameUnitUID].SetRespawnReady(false);
					this.m_dicUnitViewer.Remove(nkcunitClient.GetUnitDataGame().m_GameUnitUID);
				}
				NKCUnitClient nkcunitClient2 = nkcunitClient;
				nkcunitClient2.OnRecv(cNKMUnitSyncData);
				NKMGameTeamData myTeamData = this.GetMyTeamData();
				int respawnCost;
				if (myTeamData != null && nkcunitClient.GetUnitData().m_UnitUID == myTeamData.m_LeaderUnitUID)
				{
					respawnCost = base.GetRespawnCost(nkcunitClient2.GetUnitTemplet().m_StatTemplet, true, myTeamData.m_eNKM_TEAM_TYPE);
				}
				else
				{
					respawnCost = base.GetRespawnCost(nkcunitClient2.GetUnitTemplet().m_StatTemplet, false, myTeamData.m_eNKM_TEAM_TYPE);
				}
				if (this.m_MyTeam == nkcunitClient2.GetUnitDataGame().m_NKM_TEAM_TYPE_ORG)
				{
					if (!flag)
					{
						this.RemoveCostHolder(nkcunitClient.GetUnitData().m_UnitUID, (float)respawnCost);
						this.GetGameHud().SetRespawnCost();
					}
					else
					{
						this.RemoveCostHolderAssist(nkcunitClient.GetUnitData().m_UnitUID, 10f);
						this.GetGameHud().SetRespawnCostAssist();
					}
					if (!this.m_bRespawnUnit && nkcunitClient2.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL)
					{
						this.m_bRespawnUnit = true;
					}
				}
				if (base.IsEnemy(this.m_MyTeam, nkcunitClient2.GetUnitDataGame().m_NKM_TEAM_TYPE_ORG))
				{
					nkcunitClient2.MiniMapFaceWarrning();
					if (base.IsPVP() && nkcunitClient2.GetUnitTemplet().m_UnitTempletBase.m_bAwaken && !nkcunitClient2.IsSummonUnit())
					{
						this.GetGameHud().SetUnitIndicator(nkcunitClient2);
					}
				}
				Debug.LogFormat("RespawnUnit: {0}", new object[]
				{
					nkcunitClient.GetUnitTemplet().m_UnitTempletBase.m_UnitStrID
				});
				this.SetSortUnitZDirty(true);
				return nkcunitClient2;
			}
			return null;
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x00111CB6 File Offset: 0x0010FEB6
		public override NKMDamageEffectManager GetDEManager()
		{
			return this.m_DEManager;
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x00111CBE File Offset: 0x0010FEBE
		public override NKMDamageEffect GetDamageEffect(short DEUID)
		{
			return this.m_DEManager.GetDamageEffect(DEUID);
		}

		// Token: 0x06003583 RID: 13699 RVA: 0x00111CCC File Offset: 0x0010FECC
		public void TrySendGamePauseEnableREQ()
		{
			if (this.m_NKMGameRuntimeData == null || base.GetGameData() == null)
			{
				return;
			}
			if (!this.m_NKMGameRuntimeData.m_bPause && base.GetGameData().IsPVE())
			{
				this.Send_Packet_GAME_PAUSE_REQ(true, false, NKC_OPEN_POPUP_TYPE_AFTER_PAUSE.NOPTAP_GAME_OPTION_POPUP);
			}
		}

		// Token: 0x06003584 RID: 13700 RVA: 0x00111D04 File Offset: 0x0010FF04
		public void Send_Packet_GAME_PAUSE_REQ(bool bPause, bool bPauseEvent = false, NKC_OPEN_POPUP_TYPE_AFTER_PAUSE eNKC_OPEN_POPUP_TYPE_AFTER_PAUSE = NKC_OPEN_POPUP_TYPE_AFTER_PAUSE.NOPTAP_GAME_OPTION_POPUP)
		{
			this.m_NKC_OPEN_POPUP_TYPE_AFTER_PAUSE = eNKC_OPEN_POPUP_TYPE_AFTER_PAUSE;
			NKMPacket_GAME_PAUSE_REQ nkmpacket_GAME_PAUSE_REQ = new NKMPacket_GAME_PAUSE_REQ();
			nkmpacket_GAME_PAUSE_REQ.isPause = bPause;
			nkmpacket_GAME_PAUSE_REQ.isPauseEvent = bPauseEvent;
			if (base.GetGameData().m_bLocal)
			{
				NKCLocalPacketHandler.SendPacketToLocalServer(nkmpacket_GAME_PAUSE_REQ);
				return;
			}
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GAME_PAUSE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x00111D54 File Offset: 0x0010FF54
		public void Send_Packet_GAME_SPEED_2X_REQ(NKM_GAME_SPEED_TYPE eNKM_GAME_SPEED_TYPE)
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.BATTLE_2X, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.BATTLE_2X, 0);
				return;
			}
			NKMPacket_GAME_SPEED_2X_REQ nkmpacket_GAME_SPEED_2X_REQ = new NKMPacket_GAME_SPEED_2X_REQ();
			nkmpacket_GAME_SPEED_2X_REQ.gameSpeedType = eNKM_GAME_SPEED_TYPE;
			if (base.GetGameData().m_bLocal)
			{
				NKCLocalPacketHandler.SendPacketToLocalServer(nkmpacket_GAME_SPEED_2X_REQ);
				return;
			}
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GAME_SPEED_2X_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x00111DAC File Offset: 0x0010FFAC
		public void Send_Packet_GAME_AUTO_SKILL_CHANGE_REQ(NKM_GAME_AUTO_SKILL_TYPE eNKM_GAME_AUTO_SKILL_TYPE, bool bMsg = true)
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.BATTLE_AUTO_SKILL, 0, 0) && eNKM_GAME_AUTO_SKILL_TYPE == NKM_GAME_AUTO_SKILL_TYPE.NGST_AUTO)
			{
				if (bMsg)
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.BATTLE_AUTO_SKILL, 0);
				}
				return;
			}
			if (!this.EnableControlByGameType(bMsg ? NKM_ERROR_CODE.NEC_FAIL_ASYNC_PVP_MANUAL_PLAY_DISABLE : NKM_ERROR_CODE.NEC_OK))
			{
				return;
			}
			NKMPacket_GAME_AUTO_SKILL_CHANGE_REQ nkmpacket_GAME_AUTO_SKILL_CHANGE_REQ = new NKMPacket_GAME_AUTO_SKILL_CHANGE_REQ();
			nkmpacket_GAME_AUTO_SKILL_CHANGE_REQ.gameAutoSkillType = eNKM_GAME_AUTO_SKILL_TYPE;
			if (base.GetGameData().m_bLocal)
			{
				NKCLocalPacketHandler.SendPacketToLocalServer(nkmpacket_GAME_AUTO_SKILL_CHANGE_REQ);
				return;
			}
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GAME_AUTO_SKILL_CHANGE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x00111E1C File Offset: 0x0011001C
		public void Send_Packet_GAME_USE_UNIT_SKILL_REQ(short gameUnitUID)
		{
			NKMPacket_GAME_USE_UNIT_SKILL_REQ nkmpacket_GAME_USE_UNIT_SKILL_REQ = new NKMPacket_GAME_USE_UNIT_SKILL_REQ();
			nkmpacket_GAME_USE_UNIT_SKILL_REQ.gameUnitUID = gameUnitUID;
			if (base.GetGameData().m_bLocal)
			{
				NKCLocalPacketHandler.SendPacketToLocalServer(nkmpacket_GAME_USE_UNIT_SKILL_REQ);
				return;
			}
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GAME_USE_UNIT_SKILL_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x00111E60 File Offset: 0x00110060
		public void Send_Packet_GAME_CHECK_DIE_UNIT_REQ()
		{
			NKMPacket_GAME_CHECK_DIE_UNIT_REQ packet = new NKMPacket_GAME_CHECK_DIE_UNIT_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003589 RID: 13705 RVA: 0x00111E88 File Offset: 0x00110088
		public void Send_Packet_GAME_RESPAWN_REQ(long unitUID, bool bAssist, float fX, float gameTime)
		{
			if (base.IsReversePosTeam(this.m_MyTeam))
			{
				fX = base.GetMapTemplet().ReversePosX(fX);
			}
			NKMPacket_GAME_RESPAWN_REQ nkmpacket_GAME_RESPAWN_REQ = new NKMPacket_GAME_RESPAWN_REQ();
			nkmpacket_GAME_RESPAWN_REQ.unitUID = unitUID;
			nkmpacket_GAME_RESPAWN_REQ.assistUnit = bAssist;
			nkmpacket_GAME_RESPAWN_REQ.respawnPosX = fX;
			nkmpacket_GAME_RESPAWN_REQ.gameTime = gameTime - 0.4f * this.m_fLatencyLevel;
			if (!base.GetGameData().m_bLocal)
			{
				NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GAME_RESPAWN_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
				return;
			}
			NKCLocalPacketHandler.SendPacketToLocalServer(nkmpacket_GAME_RESPAWN_REQ);
		}

		// Token: 0x0600358A RID: 13706 RVA: 0x00111F08 File Offset: 0x00110108
		public void Send_Packet_SHIP_SKILL_REQ(int shipSkillID, float fX)
		{
			if (this.GetMyTeamData() == null || this.GetMyTeamData().m_MainShip == null)
			{
				return;
			}
			if (base.IsReversePosTeam(this.m_MyTeam))
			{
				fX = base.GetMapTemplet().ReversePosX(fX);
			}
			NKMPacket_GAME_SHIP_SKILL_REQ nkmpacket_GAME_SHIP_SKILL_REQ = new NKMPacket_GAME_SHIP_SKILL_REQ();
			nkmpacket_GAME_SHIP_SKILL_REQ.gameUnitUID = this.GetMyTeamData().m_MainShip.m_listGameUnitUID[0];
			nkmpacket_GAME_SHIP_SKILL_REQ.shipSkillID = shipSkillID;
			nkmpacket_GAME_SHIP_SKILL_REQ.skillPosX = fX;
			if (!base.GetGameData().m_bLocal)
			{
				NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GAME_SHIP_SKILL_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
				return;
			}
			NKCLocalPacketHandler.SendPacketToLocalServer(nkmpacket_GAME_SHIP_SKILL_REQ);
		}

		// Token: 0x0600358B RID: 13707 RVA: 0x00111FA0 File Offset: 0x001101A0
		public void Send_Packet_GAME_TACTICAL_COMMAND_REQ(short TCID)
		{
			NKMPacket_GAME_TACTICAL_COMMAND_REQ nkmpacket_GAME_TACTICAL_COMMAND_REQ = new NKMPacket_GAME_TACTICAL_COMMAND_REQ();
			nkmpacket_GAME_TACTICAL_COMMAND_REQ.TCID = TCID;
			if (!base.GetGameData().m_bLocal)
			{
				NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GAME_TACTICAL_COMMAND_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
				return;
			}
			NKCLocalPacketHandler.SendPacketToLocalServer(nkmpacket_GAME_TACTICAL_COMMAND_REQ);
		}

		// Token: 0x0600358C RID: 13708 RVA: 0x00111FE4 File Offset: 0x001101E4
		public void Send_Packet_GAME_AUTO_RESPAWN_REQ(bool bAutoRespawn, bool bMsg = true)
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.BATTLE_AUTO_RESPAWN, 0, 0) && bAutoRespawn)
			{
				if (bMsg)
				{
					NKCContentManager.ShowLockedMessagePopup(ContentsType.BATTLE_AUTO_RESPAWN, 0);
				}
				return;
			}
			if (!base.CanUseAutoRespawn(NKCScenManager.GetScenManager().GetMyUserData()) && bAutoRespawn)
			{
				if (bMsg)
				{
					NKCPopupMessageManager.AddPopupMessage(NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_AUTO_CAN_NOT_USE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				}
				return;
			}
			if (!this.EnableControlByGameType(bMsg ? NKM_ERROR_CODE.NEC_FAIL_ASYNC_PVP_MANUAL_PLAY_DISABLE : NKM_ERROR_CODE.NEC_OK))
			{
				return;
			}
			NKMPacket_GAME_AUTO_RESPAWN_REQ nkmpacket_GAME_AUTO_RESPAWN_REQ = new NKMPacket_GAME_AUTO_RESPAWN_REQ();
			nkmpacket_GAME_AUTO_RESPAWN_REQ.isAutoRespawn = bAutoRespawn;
			if (!base.GetGameData().m_bLocal)
			{
				NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GAME_AUTO_RESPAWN_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
				return;
			}
			NKCLocalPacketHandler.SendPacketToLocalServer(nkmpacket_GAME_AUTO_RESPAWN_REQ);
		}

		// Token: 0x0600358D RID: 13709 RVA: 0x00112080 File Offset: 0x00110280
		public void Send_Packet_GAME_OPTION_CHANGE_REQ(bool bActionCamera, bool bTrackCamera, bool bViewSkillCutIn)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData == null)
			{
				return;
			}
			bool bLocal = false;
			if (base.GetGameData() != null)
			{
				bLocal = base.GetGameData().m_bLocal;
			}
			NKCPacketSender.Send_NKMPacket_GAME_OPTION_CHANGE_REQ(bActionCamera, bTrackCamera, bViewSkillCutIn, gameOptionData.PvPAutoRespawn, gameOptionData.AutSyncFriendDeck, bLocal);
		}

		// Token: 0x0600358E RID: 13710 RVA: 0x001120C7 File Offset: 0x001102C7
		public void Send_Packet_GAME_DEV_COOL_TIME_RESET_REQ(bool bSkill, NKM_TEAM_TYPE eNKM_TEAM_TYPE)
		{
			if (base.GetGameData().m_bLocal)
			{
				NKCLocalPacketHandler.SendPacketToLocalServer(new NKMPacket_GAME_DEV_COOL_TIME_RESET_REQ
				{
					isSkill = bSkill,
					teamType = eNKM_TEAM_TYPE
				});
			}
		}

		// Token: 0x0600358F RID: 13711 RVA: 0x001120F0 File Offset: 0x001102F0
		public void Send_Packet_GAME_UNIT_RETREAT_REQ(long unitUID)
		{
			NKMPacket_GAME_UNIT_RETREAT_REQ nkmpacket_GAME_UNIT_RETREAT_REQ = new NKMPacket_GAME_UNIT_RETREAT_REQ();
			nkmpacket_GAME_UNIT_RETREAT_REQ.unitUID = unitUID;
			if (!base.GetGameData().m_bLocal)
			{
				NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GAME_UNIT_RETREAT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
				return;
			}
			NKCLocalPacketHandler.SendPacketToLocalServer(nkmpacket_GAME_UNIT_RETREAT_REQ);
		}

		// Token: 0x06003590 RID: 13712 RVA: 0x00112131 File Offset: 0x00110331
		public void OnRecv(NKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT cPacket_NPT_GAME_SYNC_DATA_PACK_NOT)
		{
			this.OnRecvSyncDataPack(cPacket_NPT_GAME_SYNC_DATA_PACK_NOT.gameTime, cPacket_NPT_GAME_SYNC_DATA_PACK_NOT.absoluteGameTime, cPacket_NPT_GAME_SYNC_DATA_PACK_NOT.gameSyncDataPack);
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x0011214C File Offset: 0x0011034C
		public void OnRecv(NKMPacket_GAME_INTRUDE_START_NOT cNKMPacket_GAME_INTRUDE_START_NOT)
		{
			if (cNKMPacket_GAME_INTRUDE_START_NOT.gameSyncDataPack != null)
			{
				this.OnRecvSyncDataPack(cNKMPacket_GAME_INTRUDE_START_NOT.gameTime, cNKMPacket_GAME_INTRUDE_START_NOT.absoluteGameTime, cNKMPacket_GAME_INTRUDE_START_NOT.gameSyncDataPack);
				base.GetGameData().m_NKMGameTeamDataA.m_DeckData.DeepCopyFrom(cNKMPacket_GAME_INTRUDE_START_NOT.gameTeamDeckDataA);
				base.GetGameData().m_NKMGameTeamDataB.m_DeckData.DeepCopyFrom(cNKMPacket_GAME_INTRUDE_START_NOT.gameTeamDeckDataB);
				this.GetGameHud().SetDeck(base.GetGameData());
			}
			if (base.GetGameData().m_NKMGameTeamDataA.m_MainShip != null)
			{
				if (base.GetGameData().m_NKMGameTeamDataA.m_MainShip.m_listGameUnitUID.Count == 1)
				{
					NKMUnit unit = this.GetUnit(base.GetGameData().m_NKMGameTeamDataA.m_MainShip.m_listGameUnitUID[0], true, true);
					if (unit != null)
					{
						unit.SetStateCoolTime(cNKMPacket_GAME_INTRUDE_START_NOT.mainShipAStateCoolTimeMap);
					}
				}
				else
				{
					Log.Error("GAME_INTRUDE_START_NOT, Team A flag ship is not one unit", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCGameClient.cs", 4239);
				}
			}
			if (base.GetGameData().m_NKMGameTeamDataB.m_MainShip != null)
			{
				if (base.GetGameData().m_NKMGameTeamDataB.m_MainShip.m_listGameUnitUID.Count == 1)
				{
					NKMUnit unit2 = this.GetUnit(base.GetGameData().m_NKMGameTeamDataB.m_MainShip.m_listGameUnitUID[0], true, true);
					if (unit2 != null)
					{
						unit2.SetStateCoolTime(cNKMPacket_GAME_INTRUDE_START_NOT.mainShipBStateCoolTimeMap);
					}
				}
				else
				{
					Log.Error("GAME_INTRUDE_START_NOT, Team B flag ship is not one unit", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCGameClient.cs", 4255);
				}
			}
			this.PlayMusic();
		}

		// Token: 0x06003592 RID: 13714 RVA: 0x001122B8 File Offset: 0x001104B8
		public void OnRecvSyncDataPack(float fGameTime, float fAbsoluteTime, NKMGameSyncDataPack cNKMGameSyncDataPack)
		{
			this.m_fLastRecvTime = 0f;
			if (cNKMGameSyncDataPack == null)
			{
				return;
			}
			if (fGameTime >= this.m_NKMGameRuntimeData.m_GameTime)
			{
				this.m_NKMGameRuntimeData.m_GameTime = fGameTime;
			}
			float num = 0f;
			for (int i = 0; i < cNKMGameSyncDataPack.m_listGameSyncData.Count; i++)
			{
				NKMGameSyncData_Base nkmgameSyncData_Base = cNKMGameSyncDataPack.m_listGameSyncData[i];
				if (nkmgameSyncData_Base != null)
				{
					nkmgameSyncData_Base.m_fGameTime += 0.4f * this.m_fLatencyLevel;
					nkmgameSyncData_Base.m_fAbsoluteGameTime += 0.4f * this.m_fLatencyLevel;
					num = Mathf.Max(num, nkmgameSyncData_Base.m_fGameTime);
					this.m_linklistGameSyncData.AddLast(nkmgameSyncData_Base);
				}
			}
			this.m_fGameTimeDifference = num - this.m_NKMGameRuntimeData.m_GameTime;
			cNKMGameSyncDataPack.m_listGameSyncData.Clear();
		}

		// Token: 0x06003593 RID: 13715 RVA: 0x00112384 File Offset: 0x00110584
		private void OpenReservedPopupAfterPause()
		{
			if (this.m_NKC_OPEN_POPUP_TYPE_AFTER_PAUSE == NKC_OPEN_POPUP_TYPE_AFTER_PAUSE.NOPTATP_REPEAT_OPERATION_POPUP)
			{
				NKCPopupRepeatOperation.Instance.Open(delegate
				{
					this.CancelPause();
				});
				return;
			}
			if (base.GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_DEV)
			{
				this.GetGameHud().OpenPause(new NKCGameHudPause.dOnClickContinue(this.CancelPause));
			}
		}

		// Token: 0x06003594 RID: 13716 RVA: 0x001123D8 File Offset: 0x001105D8
		public void OnRecv(NKMPacket_GAME_PAUSE_ACK cNKMPacket_GAME_PAUSE_ACK)
		{
			NKMPopUpBox.CloseWaitBox();
			if (cNKMPacket_GAME_PAUSE_ACK.errorCode == NKM_ERROR_CODE.NEC_OK)
			{
				if (this.GetGameHud() != null && base.GetGameData() != null)
				{
					this.GetGameHud().TogglePause(cNKMPacket_GAME_PAUSE_ACK.isPause, base.GetGameData().m_bLocal);
				}
				this.m_NKMGameRuntimeData.m_bPause = cNKMPacket_GAME_PAUSE_ACK.isPause;
				if (cNKMPacket_GAME_PAUSE_ACK.isPauseEvent)
				{
					return;
				}
				if (this.m_NKMGameRuntimeData.m_bPause)
				{
					this.OpenReservedPopupAfterPause();
				}
				else
				{
					this.GetGameHud().ClosePause();
				}
				string text = this.FindMusic();
				if (text.Length > 1)
				{
					NKCSoundManager.PlayMusic(text, true, 1f, false, 0f, 0f);
					return;
				}
			}
			else if (cNKMPacket_GAME_PAUSE_ACK.errorCode == NKM_ERROR_CODE.NEC_FAIL_GAME_NOT_IN_PLAY)
			{
				if (NKCGameEventManager.IsPauseEventPlaying())
				{
					Debug.LogWarning("Game not in play. will try pause!");
					if (this.GetGameHud() != null && base.GetGameData() != null)
					{
						this.GetGameHud().TogglePause(true, base.GetGameData().m_bLocal);
					}
					this.m_NKMGameRuntimeData.m_bPause = true;
					return;
				}
				Debug.LogWarning("Game not in play. will try unpause!");
				if (this.GetGameHud() != null && base.GetGameData() != null)
				{
					this.GetGameHud().TogglePause(false, base.GetGameData().m_bLocal);
				}
				this.m_NKMGameRuntimeData.m_bPause = false;
				this.GetGameHud().ClosePause();
			}
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x00112535 File Offset: 0x00110735
		public void OnRecv(NKMPacket_GAME_SPEED_2X_ACK cNKMPacket_GAME_SPEED_2X_ACK)
		{
			if (cNKMPacket_GAME_SPEED_2X_ACK.errorCode == NKM_ERROR_CODE.NEC_OK)
			{
				this.GetGameHud().ChangeGameSpeedTypeUI(cNKMPacket_GAME_SPEED_2X_ACK.gameSpeedType);
			}
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x00112550 File Offset: 0x00110750
		public void OnRecv(NKMPacket_GAME_AUTO_SKILL_CHANGE_ACK cNKMPacket_GAME_AUTO_SKILL_CHANGE_ACK)
		{
			if (cNKMPacket_GAME_AUTO_SKILL_CHANGE_ACK.errorCode == NKM_ERROR_CODE.NEC_OK)
			{
				this.GetGameHud().ChangeGameAutoSkillTypeUI(cNKMPacket_GAME_AUTO_SKILL_CHANGE_ACK.gameAutoSkillType);
			}
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x0011256C File Offset: 0x0011076C
		public void OnRecv(NKMPacket_GAME_USE_UNIT_SKILL_ACK cNKMPacket_GAME_USE_UNIT_SKILL_ACK)
		{
			NKCUnitClient nkcunitClient = (NKCUnitClient)this.GetUnit(cNKMPacket_GAME_USE_UNIT_SKILL_ACK.gameUnitUID, true, false);
			if (nkcunitClient != null)
			{
				nkcunitClient.OnRecv(cNKMPacket_GAME_USE_UNIT_SKILL_ACK);
			}
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x00112597 File Offset: 0x00110797
		public bool IsPause()
		{
			return this.m_NKMGameRuntimeData != null && this.m_NKMGameRuntimeData.m_bPause;
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x001125AE File Offset: 0x001107AE
		private void CancelPause()
		{
			if (NKCReplayMgr.IsPlayingReplay())
			{
				this.UI_GAME_PAUSE();
				return;
			}
			this.Send_Packet_GAME_PAUSE_REQ(false, false, NKC_OPEN_POPUP_TYPE_AFTER_PAUSE.NOPTAP_GAME_OPTION_POPUP);
			this.GetGameHud().RefreshClassGuide();
		}

		// Token: 0x0600359A RID: 13722 RVA: 0x001125D4 File Offset: 0x001107D4
		public int GetRemainSupplyOfTeamA()
		{
			NKMGameData gameData = base.GetGameData();
			if (gameData == null)
			{
				return 0;
			}
			return gameData.m_TeamASupply;
		}

		// Token: 0x0600359B RID: 13723 RVA: 0x001125F4 File Offset: 0x001107F4
		public void OnRecv(NKMPacket_GAME_RESPAWN_ACK cPacket_GAME_RESPAWN_ACK)
		{
			if (cPacket_GAME_RESPAWN_ACK.errorCode != NKM_ERROR_CODE.NEC_OK)
			{
				NKM_ERROR_CODE errorCode = cPacket_GAME_RESPAWN_ACK.errorCode;
				if (errorCode - NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_RESPAWN_ACK_UNIT_LIVE <= 4 || errorCode - NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_RESPAWN_ACK_MAX_UNIT_COUNT_SAME_TIME <= 3)
				{
					this.GetGameHud().SetMessage(NKCStringTable.GetString(cPacket_GAME_RESPAWN_ACK.errorCode), -1f);
				}
				bool flag = cPacket_GAME_RESPAWN_ACK.assistUnit;
				if (flag && cPacket_GAME_RESPAWN_ACK.unitUID == -1L)
				{
					NKMGameTeamData myTeamData = this.GetMyTeamData();
					NKMUnitData assistUnitDataByIndex = myTeamData.GetAssistUnitDataByIndex(myTeamData.m_DeckData.GetAutoRespawnIndexAssist());
					if (assistUnitDataByIndex != null)
					{
						cPacket_GAME_RESPAWN_ACK.unitUID = assistUnitDataByIndex.m_UnitUID;
					}
				}
				NKMGameTeamData myTeamData2 = this.GetMyTeamData();
				if (myTeamData2 != null)
				{
					NKMUnitData unitDataByUnitUID = myTeamData2.GetUnitDataByUnitUID(cPacket_GAME_RESPAWN_ACK.unitUID);
					if (unitDataByUnitUID != null)
					{
						NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitDataByUnitUID.m_UnitID);
						if (unitStatTemplet != null)
						{
							flag = this.GetMyTeamData().IsAssistUnit(unitDataByUnitUID.m_UnitUID);
							int respawnCost;
							if (unitDataByUnitUID.m_UnitUID == this.GetMyTeamData().m_LeaderUnitUID && !flag)
							{
								respawnCost = base.GetRespawnCost(unitStatTemplet, true, this.GetMyTeamData().m_eNKM_TEAM_TYPE);
							}
							else
							{
								respawnCost = base.GetRespawnCost(unitStatTemplet, false, this.GetMyTeamData().m_eNKM_TEAM_TYPE);
							}
							if (!flag)
							{
								this.RemoveCostHolder(unitDataByUnitUID.m_UnitUID, (float)respawnCost);
								this.GetGameHud().SetRespawnCost();
							}
							else
							{
								this.RemoveCostHolderAssist(unitDataByUnitUID.m_UnitUID, 10f);
								this.GetGameHud().SetRespawnCostAssist();
							}
						}
					}
				}
				bool bReturnDeckActive = true;
				if (flag && this.GetMyTeamData().m_DeckData.GetAutoRespawnIndexAssist() < 0)
				{
					bReturnDeckActive = false;
				}
				this.GetGameHud().UseCompleteDeckByUnitUID(cPacket_GAME_RESPAWN_ACK.unitUID, bReturnDeckActive);
				if (flag)
				{
					this.GetGameHud().UseCompleteDeckAssist(bReturnDeckActive);
				}
				this.RespawnCompleteUnitViewer(cPacket_GAME_RESPAWN_ACK.unitUID);
			}
		}

		// Token: 0x0600359C RID: 13724 RVA: 0x00112790 File Offset: 0x00110990
		public void OnRecv(NKMPacket_GAME_UNIT_RETREAT_ACK cPacket_GAME_UNIT_RETREAT_ACK)
		{
			if (cPacket_GAME_UNIT_RETREAT_ACK.errorCode != NKM_ERROR_CODE.NEC_OK)
			{
				NKM_ERROR_CODE errorCode = cPacket_GAME_UNIT_RETREAT_ACK.errorCode;
				if (errorCode - NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_RESPAWN_ACK_UNIT_LIVE <= 4 || errorCode - NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_RE_RESPAWN_UNIT_START <= 2)
				{
					this.GetGameHud().SetMessage(NKCStringTable.GetString(cPacket_GAME_UNIT_RETREAT_ACK.errorCode), -1f);
				}
				this.GetGameHud().UseCompleteDeckByUnitUID(cPacket_GAME_UNIT_RETREAT_ACK.unitUID, true);
			}
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x001127E8 File Offset: 0x001109E8
		public void OnRecv(NKMPacket_GAME_SHIP_SKILL_ACK cPacket_GAME_SHIP_SKILL_ACK)
		{
			if (cPacket_GAME_SHIP_SKILL_ACK.errorCode != NKM_ERROR_CODE.NEC_OK)
			{
				NKM_ERROR_CODE errorCode = cPacket_GAME_SHIP_SKILL_ACK.errorCode;
				if (errorCode - NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_SHIP_SKILL_ACK_NO_UNIT <= 1 || errorCode - NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_SHIP_SKILL_ACK_NO_RESPAWN_COST <= 2)
				{
					this.GetGameHud().SetMessage(NKCStringTable.GetString(cPacket_GAME_SHIP_SKILL_ACK.errorCode), -1f);
				}
				this.m_ShipSkillMark.SetShow(false);
				this.GetGameHud().ReturnDeckByShipSkillID(cPacket_GAME_SHIP_SKILL_ACK.shipSkillID);
			}
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x0011284C File Offset: 0x00110A4C
		public void OnRecv(NKMPacket_GAME_TACTICAL_COMMAND_ACK cPacket_GAME_TACTICAL_COMMAND_ACK)
		{
			if (cPacket_GAME_TACTICAL_COMMAND_ACK.errorCode != NKM_ERROR_CODE.NEC_OK)
			{
				NKM_ERROR_CODE errorCode = cPacket_GAME_TACTICAL_COMMAND_ACK.errorCode;
				if (errorCode - NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_TACTICAL_COMMAND_INVALID_TC <= 1)
				{
					this.GetGameHud().SetMessage(NKCStringTable.GetString(cPacket_GAME_TACTICAL_COMMAND_ACK.errorCode), -1f);
				}
				if (cPacket_GAME_TACTICAL_COMMAND_ACK.cTacticalCommandData.m_TCID > 0)
				{
					this.GetGameHud().ReturnDeckByTacticalCommandID((int)cPacket_GAME_TACTICAL_COMMAND_ACK.cTacticalCommandData.m_TCID);
					this.RemoveCostHolderTC(cPacket_GAME_TACTICAL_COMMAND_ACK.cTacticalCommandData.m_TCID);
					this.GetGameHud().SetRespawnCost();
				}
			}
			if (cPacket_GAME_TACTICAL_COMMAND_ACK.cTacticalCommandData.m_TCID > 0)
			{
				NKMTacticalCommandData tacticalCommandDataByID = this.GetMyTeamData().GetTacticalCommandDataByID(cPacket_GAME_TACTICAL_COMMAND_ACK.cTacticalCommandData.m_TCID);
				if (tacticalCommandDataByID != null)
				{
					tacticalCommandDataByID.DeepCopyFromSource(cPacket_GAME_TACTICAL_COMMAND_ACK.cTacticalCommandData);
				}
			}
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x00112900 File Offset: 0x00110B00
		public void OnRecv(NKMPacket_GAME_AUTO_RESPAWN_ACK cPacket_GAME_AUTO_RESPAWN_ACK)
		{
			if (cPacket_GAME_AUTO_RESPAWN_ACK.errorCode == NKM_ERROR_CODE.NEC_OK)
			{
				NKMGameRuntimeTeamData myRuntimeTeamData = this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(this.m_MyTeam);
				if (myRuntimeTeamData != null)
				{
					myRuntimeTeamData.m_bAutoRespawn = cPacket_GAME_AUTO_RESPAWN_ACK.isAutoRespawn;
				}
				this.GetGameHud().ToggleAutoRespawn(cPacket_GAME_AUTO_RESPAWN_ACK.isAutoRespawn);
			}
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x00112948 File Offset: 0x00110B48
		public void OnRecv(NKMPacket_GAME_OPTION_CHANGE_ACK cNKMPacket_GAME_OPTION_CHANGE_ACK)
		{
			NKMPopUpBox.CloseWaitBox();
			if (cNKMPacket_GAME_OPTION_CHANGE_ACK.errorCode == NKM_ERROR_CODE.NEC_OK)
			{
				NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
				if (gameOptionData == null)
				{
					return;
				}
				if (gameOptionData.ActionCamera != cNKMPacket_GAME_OPTION_CHANGE_ACK.isActionCamera)
				{
					gameOptionData.SetUseActionCamera(cNKMPacket_GAME_OPTION_CHANGE_ACK.isActionCamera, true);
				}
				if (gameOptionData.TrackCamera != cNKMPacket_GAME_OPTION_CHANGE_ACK.isTrackCamera)
				{
					gameOptionData.SetUseTrackCamera(cNKMPacket_GAME_OPTION_CHANGE_ACK.isTrackCamera, true);
				}
				if (gameOptionData.ViewSkillCutIn != cNKMPacket_GAME_OPTION_CHANGE_ACK.isViewSkillCutIn)
				{
					gameOptionData.SetViewSkillCutIn(cNKMPacket_GAME_OPTION_CHANGE_ACK.isViewSkillCutIn, true);
				}
				if (gameOptionData.PvPAutoRespawn != cNKMPacket_GAME_OPTION_CHANGE_ACK.defaultPvpAutoRespawn)
				{
					gameOptionData.SetPvPAutoRespawn(cNKMPacket_GAME_OPTION_CHANGE_ACK.defaultPvpAutoRespawn, true);
				}
				if (gameOptionData.AutSyncFriendDeck != cNKMPacket_GAME_OPTION_CHANGE_ACK.autoSyncFriendDeck)
				{
					gameOptionData.SetAutoSyncFriendDeck(cNKMPacket_GAME_OPTION_CHANGE_ACK.autoSyncFriendDeck, true);
				}
				NKCUIGameOption.CheckInstanceAndClose();
			}
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x00112A00 File Offset: 0x00110C00
		public void OnRecv(NKMPacket_GAME_LOAD_COMPLETE_ACK cNKMPacket_GAME_LOAD_COMPLETE_ACK)
		{
			this.SetGameRuntimeData(cNKMPacket_GAME_LOAD_COMPLETE_ACK.gameRuntimeData);
			this.GetGameHud().SetUIByRuntimeData(base.GetGameData(), base.GetGameRuntimeData());
			this.MultiplyReward = cNKMPacket_GAME_LOAD_COMPLETE_ACK.rewardMultiply;
			this.GetGameHud().SetMultiply(this.MultiplyReward);
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x00112A4D File Offset: 0x00110C4D
		public void OnRecv(NKMPacket_GAME_DEV_COOL_TIME_RESET_ACK cNKMPacket_GAME_DEV_COOL_TIME_RESET_ACK)
		{
			if (cNKMPacket_GAME_DEV_COOL_TIME_RESET_ACK.errorCode == NKM_ERROR_CODE.NEC_OK)
			{
				if (cNKMPacket_GAME_DEV_COOL_TIME_RESET_ACK.isSkill)
				{
					base.DEV_SkillCoolTimeReset(cNKMPacket_GAME_DEV_COOL_TIME_RESET_ACK.teamType);
					return;
				}
				base.DEV_HyperSkillCoolTimeReset(cNKMPacket_GAME_DEV_COOL_TIME_RESET_ACK.teamType);
			}
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x00112A78 File Offset: 0x00110C78
		public void OnRecv(NKMPacket_GAME_DEV_RESPAWN_ACK cNKMPacket_GAME_DEV_RESPAWN_ACK)
		{
			NKMPopUpBox.CloseWaitBox();
			if (cNKMPacket_GAME_DEV_RESPAWN_ACK.errorCode == NKM_ERROR_CODE.NEC_OK)
			{
				base.GetGameData().m_NKMGameTeamDataA.m_listDynamicRespawnUnitData = cNKMPacket_GAME_DEV_RESPAWN_ACK.dynamicRespawnUnitDataTeamA;
				base.GetGameData().m_NKMGameTeamDataB.m_listDynamicRespawnUnitData = cNKMPacket_GAME_DEV_RESPAWN_ACK.dynamicRespawnUnitDataTeamB;
				base.CreatePoolUnit(null, cNKMPacket_GAME_DEV_RESPAWN_ACK.unitData, 0, cNKMPacket_GAME_DEV_RESPAWN_ACK.teamType, false);
				base.CreateDynaminRespawnPoolUnit(false);
				return;
			}
			switch (cNKMPacket_GAME_DEV_RESPAWN_ACK.errorCode)
			{
			case NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_RESPAWN_ACK_UNIT_LIVE:
				this.GetGameHud().SetMessage("이미 출격중인 유닛입니다.", -1f);
				return;
			case NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_RESPAWN_ACK_INVALID_POS:
				this.GetGameHud().SetMessage("출격이 불가능한 지역입니다.", -1f);
				return;
			case NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_RESPAWN_ACK_NO_RESPAWN_COST:
				this.GetGameHud().SetMessage("출격 비용이 부족합니다.", -1f);
				return;
			case NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_RESPAWN_ACK_NO_DECK:
				this.GetGameHud().SetMessage("존재하지 않는 유닛입니다.", -1f);
				return;
			case NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_RESPAWN_ACK_NO_GAME_STATE:
				this.GetGameHud().SetMessage("지금은 출격할 수 없습니다.", -1f);
				return;
			default:
				return;
			}
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x00112B74 File Offset: 0x00110D74
		private void ProcessDungeonEventSync(NKMGameSyncData_DungeonEvent cNKMGameSyncData_DungeonEvent)
		{
			Debug.Log(string.Format("Got GameEvent! {0} {1} pause : {2}", cNKMGameSyncData_DungeonEvent.m_eEventActionType, cNKMGameSyncData_DungeonEvent.m_EventID, cNKMGameSyncData_DungeonEvent.m_bPause));
			if (cNKMGameSyncData_DungeonEvent.m_bPause)
			{
				this.Send_Packet_GAME_PAUSE_REQ(cNKMGameSyncData_DungeonEvent.m_bPause, true, NKC_OPEN_POPUP_TYPE_AFTER_PAUSE.NOPTAP_GAME_OPTION_POPUP);
				this.GetGameHud().TogglePause(cNKMGameSyncData_DungeonEvent.m_bPause, base.GetGameData().m_bLocal);
				this.m_NKMGameRuntimeData.m_bPause = cNKMGameSyncData_DungeonEvent.m_bPause;
				if (NKCUIGameOption.IsInstanceOpen)
				{
					Debug.LogWarning("Pause was open, forcing event");
					NKCUIGameOption.Instance.RemoveCloseCallBack();
					NKCUIGameOption.Instance.Close();
				}
			}
			if (NKMDungeonEventTemplet.IsPermanent(cNKMGameSyncData_DungeonEvent.m_eEventActionType))
			{
				if (this.m_NKMGameRuntimeData.m_lstPermanentDungeonEvent == null)
				{
					this.m_NKMGameRuntimeData.m_lstPermanentDungeonEvent = new List<NKMGameSyncData_DungeonEvent>();
				}
				this.m_NKMGameRuntimeData.m_lstPermanentDungeonEvent.Add(cNKMGameSyncData_DungeonEvent);
			}
			switch (cNKMGameSyncData_DungeonEvent.m_eEventActionType)
			{
			case NKM_EVENT_ACTION_TYPE.GAME_EVENT:
				NKCGameEventManager.PlayGameEvent(cNKMGameSyncData_DungeonEvent.m_EventID, cNKMGameSyncData_DungeonEvent.m_bPause, new NKCGameEventManager.OnEventFinish(this.EventFinished));
				return;
			case NKM_EVENT_ACTION_TYPE.SET_ENEMY_BOSS_HP_RATE:
			case NKM_EVENT_ACTION_TYPE.KILL_ALL_TAGGED_UNIT:
			case NKM_EVENT_ACTION_TYPE.ADD_EVENTTAG:
			case NKM_EVENT_ACTION_TYPE.SET_EVENTTAG:
			case NKM_EVENT_ACTION_TYPE.FORCE_WIN:
			case NKM_EVENT_ACTION_TYPE.FORCE_LOSE:
			case NKM_EVENT_ACTION_TYPE.SET_UNIT_STATE:
			case NKM_EVENT_ACTION_TYPE.SET_ENEMY_UNIT_STATE:
				return;
			case NKM_EVENT_ACTION_TYPE.SET_UNIT_HYPER_FULL:
				if (cNKMGameSyncData_DungeonEvent.m_iEventActionValue != 0)
				{
					NKMUnit unitByUnitID = this.GetUnitByUnitID(cNKMGameSyncData_DungeonEvent.m_iEventActionValue, true, true);
					if (unitByUnitID != null)
					{
						List<NKMAttackStateData> listHyperSkillStateData = unitByUnitID.GetUnitTemplet().m_listHyperSkillStateData;
						for (int i = 0; i < listHyperSkillStateData.Count; i++)
						{
							unitByUnitID.SetStateCoolTime(listHyperSkillStateData[i].m_StateName, false, 0f);
						}
						return;
					}
					return;
				}
				else
				{
					foreach (KeyValuePair<short, NKMUnit> keyValuePair in this.m_dicNKMUnit)
					{
						NKMUnit value = keyValuePair.Value;
						List<NKMAttackStateData> listHyperSkillStateData2 = value.GetUnitTemplet().m_listHyperSkillStateData;
						for (int j = 0; j < listHyperSkillStateData2.Count; j++)
						{
							value.SetStateCoolTime(listHyperSkillStateData2[j].m_StateName, false, 0f);
						}
					}
					using (Dictionary<short, NKMUnit>.Enumerator enumerator = this.m_dicNKMUnitPool.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<short, NKMUnit> keyValuePair2 = enumerator.Current;
							NKMUnit value2 = keyValuePair2.Value;
							List<NKMAttackStateData> listHyperSkillStateData3 = value2.GetUnitTemplet().m_listHyperSkillStateData;
							for (int k = 0; k < listHyperSkillStateData3.Count; k++)
							{
								value2.SetStateCoolTime(listHyperSkillStateData3[k].m_StateName, false, 0f);
							}
						}
						return;
					}
				}
				break;
			case NKM_EVENT_ACTION_TYPE.NEAT_RESPAWN_COST_A_TEAM:
				break;
			case NKM_EVENT_ACTION_TYPE.NEAT_RESPAWN_COST_B_TEAM:
				this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(NKM_TEAM_TYPE.NTT_B1).m_fRespawnCost = (float)cNKMGameSyncData_DungeonEvent.m_iEventActionValue;
				return;
			case NKM_EVENT_ACTION_TYPE.ADD_TEAM_A_EXTRA_RESPAWN_COST:
			{
				float num = (float)cNKMGameSyncData_DungeonEvent.m_iEventActionValue * 0.01f;
				Debug.Log(string.Format("Gameevent : A팀 리스폰코스트 초당 {0} 추가", num));
				this.m_NKMGameData.fExtraRespawnCostAddForA += num;
				return;
			}
			case NKM_EVENT_ACTION_TYPE.ADD_TEAM_B_EXTRA_RESPAWN_COST:
			{
				float num2 = (float)cNKMGameSyncData_DungeonEvent.m_iEventActionValue * 0.01f;
				Debug.Log(string.Format("Gameevent : B팀 리스폰코스트 초당 {0} 추가", num2));
				this.m_NKMGameData.fExtraRespawnCostAddForB += num2;
				return;
			}
			case NKM_EVENT_ACTION_TYPE.UNLOCK_TUTORIAL_GAME_RE_RESPAWN:
				Debug.Log("Gameevent : 튜토리얼 재출격 활성화");
				this.m_bTutorialGameReRespawnAllowed = true;
				return;
			case NKM_EVENT_ACTION_TYPE.CHANGE_BGM:
				if (string.IsNullOrEmpty(cNKMGameSyncData_DungeonEvent.m_strEventActionValue))
				{
					NKCSoundManager.FadeOutMusic();
					return;
				}
				if (cNKMGameSyncData_DungeonEvent.m_iEventActionValue != 0)
				{
					float fStartTime = (float)cNKMGameSyncData_DungeonEvent.m_iEventActionValue / 100f;
					NKCSoundManager.PlayMusic(cNKMGameSyncData_DungeonEvent.m_strEventActionValue, true, 1f, true, fStartTime, 0f);
					return;
				}
				if (!NKCSoundManager.IsSameMusic(cNKMGameSyncData_DungeonEvent.m_strEventActionValue))
				{
					NKCSoundManager.PlayMusic(cNKMGameSyncData_DungeonEvent.m_strEventActionValue, true, 1f, false, 0f, 0f);
					return;
				}
				return;
			case NKM_EVENT_ACTION_TYPE.CHANGE_BGM_TRACK:
			{
				if (string.IsNullOrEmpty(cNKMGameSyncData_DungeonEvent.m_strEventActionValue))
				{
					NKCSoundManager.FadeOutMusic();
					return;
				}
				float musicTime = NKCSoundManager.GetMusicTime();
				if (!NKCSoundManager.IsSameMusic(cNKMGameSyncData_DungeonEvent.m_strEventActionValue))
				{
					NKCSoundManager.PlayMusic(cNKMGameSyncData_DungeonEvent.m_strEventActionValue, true, 1f, true, musicTime, 0f);
					return;
				}
				return;
			}
			case NKM_EVENT_ACTION_TYPE.HUD_ALERT:
				this.GetGameHud().SetMessage(NKCStringTable.GetString(cNKMGameSyncData_DungeonEvent.m_strEventActionValue, false), -1f);
				return;
			case NKM_EVENT_ACTION_TYPE.POPUP_MESSAGE:
				if (NKCScenManager.GetScenManager().GetGameClient().IsShowUI())
				{
					NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString(cNKMGameSyncData_DungeonEvent.m_strEventActionValue, false), NKCPopupMessage.eMessagePosition.TopIngame, false, true, 0f, false);
					return;
				}
				return;
			default:
				return;
			}
			this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(NKM_TEAM_TYPE.NTT_A1).m_fRespawnCost = (float)cNKMGameSyncData_DungeonEvent.m_iEventActionValue;
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x00112FE4 File Offset: 0x001111E4
		private void ProcessGameEventSync(NKMGameSyncData_GameEvent cNKMGameSyncData_GameEvent)
		{
			switch (cNKMGameSyncData_GameEvent.m_NKM_GAME_EVENT_TYPE)
			{
			case NKM_GAME_EVENT_TYPE.NGET_TACTICAL_COMMAND:
				if (cNKMGameSyncData_GameEvent.m_NKM_TEAM_TYPE != NKM_TEAM_TYPE.NTT_INVALID && this.GetMyTeamData().m_eNKM_TEAM_TYPE != cNKMGameSyncData_GameEvent.m_NKM_TEAM_TYPE)
				{
					return;
				}
				this.ProcessGameEventTCSync(cNKMGameSyncData_GameEvent);
				return;
			case NKM_GAME_EVENT_TYPE.NGET_TC_COMBO_SUCCESS:
			case NKM_GAME_EVENT_TYPE.NGET_TC_COMBO_FAIL:
			case NKM_GAME_EVENT_TYPE.NGET_TC_COMBO_SKILL_SUCCESS:
			case NKM_GAME_EVENT_TYPE.NGET_TC_COMBO_SKILL_REAL_APPLY_AFTER_SUCCESS:
			case NKM_GAME_EVENT_TYPE.NGET_TC_COMBO_COOL_TIME_ON:
				this.ProcessGameEventTC_Combo(cNKMGameSyncData_GameEvent);
				return;
			case NKM_GAME_EVENT_TYPE.NGET_KILL_COUNT:
				if (cNKMGameSyncData_GameEvent.m_NKM_TEAM_TYPE != NKM_TEAM_TYPE.NTT_INVALID && this.GetMyTeamData().m_eNKM_TEAM_TYPE != cNKMGameSyncData_GameEvent.m_NKM_TEAM_TYPE)
				{
					return;
				}
				NKCKillCountManager.CurrentStageKillCount = (long)Mathf.Round(cNKMGameSyncData_GameEvent.m_fValue);
				this.GetGameHud().SetKillCount(NKCKillCountManager.CurrentStageKillCount);
				return;
			case NKM_GAME_EVENT_TYPE.NGET_AUTO_RESPAWN_WARNING:
				if (this.GetMyTeamData().m_eNKM_TEAM_TYPE != cNKMGameSyncData_GameEvent.m_NKM_TEAM_TYPE)
				{
					return;
				}
				this.GetGameHud().SetMessage(NKCStringTable.GetString("SI_PF_GAUNTLET_HAVE_NO_ACTION_TEXT", false), -1f);
				return;
			case NKM_GAME_EVENT_TYPE.NGET_AUTO_RESPAWN_SET:
			{
				if (this.GetMyTeamData().m_eNKM_TEAM_TYPE != cNKMGameSyncData_GameEvent.m_NKM_TEAM_TYPE)
				{
					return;
				}
				bool flag = cNKMGameSyncData_GameEvent.m_EventID != 0;
				NKMGameRuntimeTeamData myRunTimeTeamData = this.GetMyRunTimeTeamData();
				if (myRunTimeTeamData != null)
				{
					myRunTimeTeamData.m_bAutoRespawn = flag;
					this.GetGameHud().ToggleAutoRespawn(flag);
					return;
				}
				break;
			}
			case NKM_GAME_EVENT_TYPE.NGET_COST_RETURN:
				if (this.GetMyTeamData().m_eNKM_TEAM_TYPE != cNKMGameSyncData_GameEvent.m_NKM_TEAM_TYPE)
				{
					return;
				}
				this.GetGameHud().PlayRespawnAddEvent(cNKMGameSyncData_GameEvent.m_fValue);
				break;
			default:
				return;
			}
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x00113124 File Offset: 0x00111324
		private void ProcessGameEventTCSync(NKMGameSyncData_GameEvent cNKMGameSyncData_GameEvent)
		{
			this.GetGameHud().ReturnDeckByTacticalCommandID(cNKMGameSyncData_GameEvent.m_EventID);
			this.RemoveCostHolderTC((short)cNKMGameSyncData_GameEvent.m_EventID);
			this.GetGameHud().SetRespawnCost();
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x00113150 File Offset: 0x00111350
		private void PlayOperatorSkillCutinEffect(bool bRight, float fDurationTime, string operatorName, string skillName, string cutinBG_EffectName, string cutinEffectName, string cutInEffectAnimName)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData != null && !gameOptionData.ViewSkillCutIn)
			{
				return;
			}
			this.GetNKCEffectManager().StopCutInEffect();
			NKM_GAME_SPEED_TYPE nkm_GAME_SPEED_TYPE = base.GetGameRuntimeData().m_NKM_GAME_SPEED_TYPE;
			float fAnimSpeed;
			if (nkm_GAME_SPEED_TYPE != NKM_GAME_SPEED_TYPE.NGST_2)
			{
				if (nkm_GAME_SPEED_TYPE != NKM_GAME_SPEED_TYPE.NGST_05)
				{
					fAnimSpeed = 1.1f;
				}
				else
				{
					fAnimSpeed = 0.6f;
				}
			}
			else
			{
				fAnimSpeed = 1.5f;
			}
			NKCASEffect nkcaseffect = this.GetNKCEffectManager().UseEffect(0, cutinBG_EffectName, cutinBG_EffectName, NKM_EFFECT_PARENT_TYPE.NEPT_NUM_GAME_BATTLE_EFFECT, 0f, 0f, 0f, bRight, 1f, 0f, 0f, 0f, false, 0f, false, "", false, true, "BASE", fAnimSpeed, false, true, 0f, fDurationTime, false);
			if (nkcaseffect != null && nkcaseffect.m_Animator != null)
			{
				nkcaseffect.m_Animator.SetInteger("ColorIndex", 1);
			}
			this.GetNKCEffectManager().UseEffect(0, cutinEffectName, cutinEffectName, NKM_EFFECT_PARENT_TYPE.NEPT_NUF_BEFORE_HUD_CONTROL_EFFECT, 0f, 0f, 0f, bRight, 1f, 0f, 0f, 0f, false, 0f, false, "", false, true, cutInEffectAnimName, fAnimSpeed, false, true, 0f, -1f, false);
			NKCASEffect nkcaseffect2 = this.GetNKCEffectManager().UseEffect(0, "AB_FX_SKILL_CUTIN_COMMON_DESC", "AB_FX_SKILL_CUTIN_COMMON_DESC", NKM_EFFECT_PARENT_TYPE.NEPT_NUF_BEFORE_HUD_CONTROL_EFFECT, 0f, 0f, 0f, bRight, 1f, 0f, 0f, 0f, false, 0f, false, "", false, true, "BASE", fAnimSpeed, false, true, 0f, fDurationTime, false);
			if (nkcaseffect2 != null)
			{
				nkcaseffect2.Init_AB_FX_SKILL_CUTIN_COMMON_DESC();
				NKCUtil.SetLabelText(nkcaseffect2.m_AB_FX_SKILL_CUTIN_COMMON_DESC_UNIT_NAME, operatorName);
				if (bRight)
				{
					NKCUtil.SetRectTransformLocalRotate(nkcaseffect2.m_AB_FX_SKILL_CUTIN_COMMON_DESC_UNIT_NAME_RectTransform, 0f, 0f, 0f);
				}
				else
				{
					NKCUtil.SetRectTransformLocalRotate(nkcaseffect2.m_AB_FX_SKILL_CUTIN_COMMON_DESC_UNIT_NAME_RectTransform, 0f, 180f, 0f);
				}
				NKCUtil.SetLabelText(nkcaseffect2.m_AB_FX_SKILL_CUTIN_COMMON_DESC_SKILL_NAME, skillName);
				if (bRight)
				{
					NKCUtil.SetRectTransformLocalRotate(nkcaseffect2.m_AB_FX_SKILL_CUTIN_COMMON_DESC_SKILL_NAME_RectTransform, 0f, 0f, 0f);
					return;
				}
				NKCUtil.SetRectTransformLocalRotate(nkcaseffect2.m_AB_FX_SKILL_CUTIN_COMMON_DESC_SKILL_NAME_RectTransform, 0f, 180f, 0f);
			}
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x0011336C File Offset: 0x0011156C
		private void ProcessGameEventTC_Combo(NKMGameSyncData_GameEvent cNKMGameSyncData_GameEvent)
		{
			if (cNKMGameSyncData_GameEvent == null)
			{
				return;
			}
			if (cNKMGameSyncData_GameEvent.m_NKM_GAME_EVENT_TYPE == NKM_GAME_EVENT_TYPE.NGET_TC_COMBO_SKILL_REAL_APPLY_AFTER_SUCCESS)
			{
				base.SetStopTime(NKMCommonConst.OPERATOR_SKILL_STOP_TIME, NKM_STOP_TIME_INDEX.NSTI_OPERATOR_SKILL);
				if (base.GetGameData() != null)
				{
					NKMTacticalCommandTemplet tacticalCommandTempletByID = NKMTacticalCommandManager.GetTacticalCommandTempletByID(cNKMGameSyncData_GameEvent.m_EventID);
					if (tacticalCommandTempletByID != null)
					{
						bool flag = base.IsSameTeam(this.GetMyTeamData().m_eNKM_TEAM_TYPE, cNKMGameSyncData_GameEvent.m_NKM_TEAM_TYPE);
						float posZ = (this.m_NKMMapTemplet.m_fMinZ + this.m_NKMMapTemplet.m_fMaxZ) / 2f;
						float posX;
						if (flag)
						{
							posX = this.m_NKMMapTemplet.m_fMinX;
						}
						else
						{
							posX = this.m_NKMMapTemplet.m_fMaxX;
						}
						if (!string.IsNullOrWhiteSpace(tacticalCommandTempletByID.m_TCEffectSound))
						{
							NKCSoundManager.PlaySound(tacticalCommandTempletByID.m_TCEffectSound, 1f, 0f, 0f, false, 1f, false, 0f);
						}
						this.GetNKCEffectManager().UseEffect(0, tacticalCommandTempletByID.m_TCEffectName, tacticalCommandTempletByID.m_TCEffectName, NKM_EFFECT_PARENT_TYPE.NEPT_NUM_GAME_BATTLE_EFFECT, posX, -200f, posZ, flag, 1f, 0f, 0f, 0f, false, 0f, false, "", false, true, "BASE", 1f, false, false, 1f, -1f, false);
					}
					NKMGameTeamData teamData = base.GetGameData().GetTeamData(cNKMGameSyncData_GameEvent.m_NKM_TEAM_TYPE);
					if (teamData != null && teamData.m_Operator != null)
					{
						NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_COMBO_COMPLETE, teamData.m_Operator, false, true);
						NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(teamData.m_Operator.id);
						if (unitTempletBase != null)
						{
							string skillName = "";
							if (tacticalCommandTempletByID != null)
							{
								skillName = tacticalCommandTempletByID.GetTCName();
							}
							this.PlayOperatorSkillCutinEffect(base.IsSameTeam(this.GetMyTeamData().m_eNKM_TEAM_TYPE, cNKMGameSyncData_GameEvent.m_NKM_TEAM_TYPE), 1f, unitTempletBase.GetUnitName(), skillName, "AB_FX_SKILL_CUTIN_COMMON_A", this.GetFinalOperatorCutinEffectName(teamData.m_Operator.id), "BASE");
						}
					}
				}
				return;
			}
			if (cNKMGameSyncData_GameEvent.m_NKM_TEAM_TYPE != NKM_TEAM_TYPE.NTT_INVALID && this.GetMyTeamData().m_eNKM_TEAM_TYPE != cNKMGameSyncData_GameEvent.m_NKM_TEAM_TYPE && !this.IsObserver(NKCScenManager.CurrentUserData()))
			{
				return;
			}
			if (cNKMGameSyncData_GameEvent.m_EventID > 0)
			{
				NKMTacticalCommandData tacticalCommandDataByID = this.GetMyTeamData().GetTacticalCommandDataByID((short)cNKMGameSyncData_GameEvent.m_EventID);
				if (this.IsObserver(NKCScenManager.CurrentUserData()))
				{
					tacticalCommandDataByID = base.GetGameData().GetTeamData(cNKMGameSyncData_GameEvent.m_NKM_TEAM_TYPE).GetTacticalCommandDataByID((short)cNKMGameSyncData_GameEvent.m_EventID);
				}
				if (tacticalCommandDataByID != null)
				{
					byte comboCount = tacticalCommandDataByID.m_ComboCount;
					NKMTacticalCommandTemplet tacticalCommandTempletByID2 = NKMTacticalCommandManager.GetTacticalCommandTempletByID(cNKMGameSyncData_GameEvent.m_EventID);
					switch (cNKMGameSyncData_GameEvent.m_NKM_GAME_EVENT_TYPE)
					{
					case NKM_GAME_EVENT_TYPE.NGET_TC_COMBO_SUCCESS:
						tacticalCommandDataByID.m_ComboCount = (byte)cNKMGameSyncData_GameEvent.m_fValue;
						tacticalCommandDataByID.m_fComboResetCoolTimeNow = tacticalCommandTempletByID2.m_fComboResetCoolTime;
						return;
					case NKM_GAME_EVENT_TYPE.NGET_TC_COMBO_FAIL:
						tacticalCommandDataByID.m_ComboCount = (byte)cNKMGameSyncData_GameEvent.m_fValue;
						return;
					case NKM_GAME_EVENT_TYPE.NGET_TC_COMBO_SKILL_SUCCESS:
						tacticalCommandDataByID.m_ComboCount = (byte)cNKMGameSyncData_GameEvent.m_fValue;
						tacticalCommandDataByID.m_fCoolTimeNow = tacticalCommandTempletByID2.m_fCoolTime;
						tacticalCommandDataByID.m_bCoolTimeOn = false;
						return;
					case NKM_GAME_EVENT_TYPE.NGET_TC_COMBO_SKILL_REAL_APPLY_AFTER_SUCCESS:
						break;
					case NKM_GAME_EVENT_TYPE.NGET_TC_COMBO_COOL_TIME_ON:
						if (cNKMGameSyncData_GameEvent.m_fValue == 1f)
						{
							tacticalCommandDataByID.m_bCoolTimeOn = true;
							return;
						}
						tacticalCommandDataByID.m_bCoolTimeOn = false;
						break;
					default:
						return;
					}
				}
			}
		}

		// Token: 0x060035A9 RID: 13737 RVA: 0x00113661 File Offset: 0x00111861
		public void EventFinished(bool bUnpause)
		{
			if (bUnpause)
			{
				this.Send_Packet_GAME_PAUSE_REQ(false, false, NKC_OPEN_POPUP_TYPE_AFTER_PAUSE.NOPTAP_GAME_OPTION_POPUP);
			}
		}

		// Token: 0x060035AA RID: 13738 RVA: 0x00113670 File Offset: 0x00111870
		public void PlaySkillCutIn(NKCUnitClient caller, bool bHyper, bool bRight, Sprite faceSprite, string unitName, string skillName)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData != null && !gameOptionData.ViewSkillCutIn)
			{
				return;
			}
			if (!bHyper)
			{
				if (bRight)
				{
					this.m_NKCSkillCutInSideRedRight.Stop();
					this.m_NKCSkillCutInSideRight.Play(this.GetNKCEffectManager(), faceSprite, unitName, skillName);
					return;
				}
				this.m_NKCSkillCutInSideRedLeft.Stop();
				this.m_NKCSkillCutInSideLeft.Play(this.GetNKCEffectManager(), faceSprite, unitName, skillName);
				return;
			}
			else
			{
				if (bRight)
				{
					this.m_NKCSkillCutInSideRight.Stop();
					this.m_NKCSkillCutInSideRedRight.Play(this.GetNKCEffectManager(), faceSprite, unitName, skillName);
					return;
				}
				this.m_NKCSkillCutInSideLeft.Stop();
				this.m_NKCSkillCutInSideRedLeft.Play(this.GetNKCEffectManager(), faceSprite, unitName, skillName);
				return;
			}
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x00113728 File Offset: 0x00111928
		private bool HasTimeLimit()
		{
			NKM_GAME_TYPE gameType = base.GetGameData().GetGameType();
			return gameType > NKM_GAME_TYPE.NGT_PRACTICE && gameType != NKM_GAME_TYPE.NGT_TUTORIAL;
		}

		// Token: 0x060035AC RID: 13740 RVA: 0x0011374C File Offset: 0x0011194C
		public bool EnableControlByGameType(NKM_ERROR_CODE failErrorCode = NKM_ERROR_CODE.NEC_OK)
		{
			if (this.IsObserver(NKCScenManager.CurrentUserData()))
			{
				return false;
			}
			if (NKCReplayMgr.IsPlayingReplay())
			{
				return false;
			}
			if (base.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_ASYNC_PVP)
			{
				if (failErrorCode != NKM_ERROR_CODE.NEC_OK)
				{
					NKCPopupMessageManager.AddPopupMessage(failErrorCode, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				}
				return false;
			}
			return true;
		}

		// Token: 0x060035AD RID: 13741 RVA: 0x0011378A File Offset: 0x0011198A
		public bool CanReRespawn()
		{
			return base.GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_TUTORIAL || this.m_bTutorialGameReRespawnAllowed;
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x001137A4 File Offset: 0x001119A4
		public void HitEffect(NKCUnitClient cUnit, NKMDamageInst cNKMDamageInst, string hitEffectBundleName, string hitEffect, string hitEffectAnimName, float fHitEffectRange, float fHitEffectOffsetZ, bool bHitEffectLand)
		{
			if (cUnit == null || cNKMDamageInst == null)
			{
				return;
			}
			float num = cUnit.GetUnitSyncData().m_PosZ + fHitEffectOffsetZ;
			this.m_TempMinMaxVec3.m_MinX = cUnit.GetUnitSyncData().m_PosX - fHitEffectRange;
			this.m_TempMinMaxVec3.m_MinY = num + cUnit.GetUnitSyncData().m_JumpYPos + cUnit.GetUnitTemplet().m_UnitSizeY * 0.5f - fHitEffectRange;
			this.m_TempMinMaxVec3.m_MinZ = num - fHitEffectRange;
			this.m_TempMinMaxVec3.m_MaxX = cUnit.GetUnitSyncData().m_PosX + fHitEffectRange;
			this.m_TempMinMaxVec3.m_MaxY = num + cUnit.GetUnitSyncData().m_JumpYPos + cUnit.GetUnitTemplet().m_UnitSizeY * 0.5f + fHitEffectRange;
			this.m_TempMinMaxVec3.m_MaxZ = num + fHitEffectRange;
			if (cUnit.GetUnitFrameData().m_BarrierBuffData != null && cUnit.GetUnitFrameData().m_BarrierBuffData.m_fBarrierHP > 0f && cUnit.GetUnitFrameData().m_BarrierBuffData.m_NKMBuffTemplet.m_BarrierDamageEffectName.Length > 1)
			{
				this.GetNKCEffectManager().UseEffect(0, cUnit.GetUnitFrameData().m_BarrierBuffData.m_NKMBuffTemplet.m_BarrierDamageEffectName, cUnit.GetUnitFrameData().m_BarrierBuffData.m_NKMBuffTemplet.m_BarrierDamageEffectName, NKM_EFFECT_PARENT_TYPE.NEPT_NUM_GAME_BATTLE_EFFECT, this.m_TempMinMaxVec3.GetRandomX(), this.m_TempMinMaxVec3.GetRandomY(), num, cUnit.GetUnitSyncData().m_bRight, 1f, 0f, 0f, 0f, false, 0f, true, "", false, true, hitEffectAnimName, 1f, false, false, 0.05f * (float)cNKMDamageInst.m_listHitUnit.Count, -1f, false);
				if (cUnit.GetBuffEffectDic().ContainsKey(cUnit.GetUnitFrameData().m_BarrierBuffData.m_NKMBuffTemplet.m_BuffID))
				{
					cUnit.GetBuffEffectDic()[cUnit.GetUnitFrameData().m_BarrierBuffData.m_NKMBuffTemplet.m_BuffID].PlayAnim("DAMAGED", false, 1f);
				}
				return;
			}
			if (bHitEffectLand)
			{
				this.m_TempMinMaxVec3.m_MinY = num;
				this.m_TempMinMaxVec3.m_MaxY = num;
			}
			LinkedListNode<NKCASEffect> linkedListNode = this.m_linklistHitEffect.First;
			NKCASEffect nkcaseffect;
			while (linkedListNode != null)
			{
				nkcaseffect = linkedListNode.Value;
				if (nkcaseffect != null && !this.GetNKCEffectManager().IsLiveEffect(nkcaseffect.m_EffectUID))
				{
					LinkedListNode<NKCASEffect> next = linkedListNode.Next;
					this.m_linklistHitEffect.Remove(linkedListNode);
					linkedListNode = next;
				}
				else if (linkedListNode != null)
				{
					linkedListNode = linkedListNode.Next;
				}
			}
			bool flag = true;
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData != null && !gameOptionData.UseHitEffect)
			{
				flag = false;
			}
			if (this.m_bSimpleHitEffectFrame && this.m_linklistHitEffect.Count > 2)
			{
				flag = false;
				this.m_bSimpleHitEffectFrame = false;
			}
			else
			{
				this.m_bSimpleHitEffectFrame = true;
			}
			if (!flag)
			{
				hitEffectBundleName = "AB_FX_HIT_SIMPLE";
				hitEffect = "AB_FX_HIT_SIMPLE";
			}
			nkcaseffect = this.GetNKCEffectManager().UseEffect(0, hitEffectBundleName, hitEffect, NKM_EFFECT_PARENT_TYPE.NEPT_NUM_GAME_BATTLE_EFFECT, this.m_TempMinMaxVec3.GetRandomX(), this.m_TempMinMaxVec3.GetRandomY(), num, cUnit.GetUnitSyncData().m_bRight, 1f, 0f, 0f, 0f, false, 0f, true, "", false, true, hitEffectAnimName, 1f, false, false, 0.05f * (float)cNKMDamageInst.m_listHitUnit.Count, -1f, false);
			if (flag && nkcaseffect != null)
			{
				this.m_linklistHitEffect.AddLast(nkcaseffect);
			}
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x00113AF8 File Offset: 0x00111CF8
		private void GetSortUnitListByZ()
		{
			if (this.m_bSortUnitZDirty)
			{
				this.m_listSortUnitZ.Clear();
				List<NKMUnit> unitChain = this.GetUnitChain();
				for (int i = 0; i < unitChain.Count; i++)
				{
					NKCUnitClient nkcunitClient = (NKCUnitClient)unitChain[i];
					if (nkcunitClient.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_ENV)
					{
						this.m_listSortUnitZ.Add(nkcunitClient);
					}
				}
				this.m_listSortUnitZ.Sort((NKCUnitClient b, NKCUnitClient a) => a.GetUnitSyncData().m_PosZ.CompareTo(b.GetUnitSyncData().m_PosZ));
				this.m_bSortUnitZDirty = false;
				this.SortUnitSkillTouchObject(this.m_listSortUnitZ);
			}
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x00113BA0 File Offset: 0x00111DA0
		private void SortUnitSkillTouchObject(List<NKCUnitClient> unitList)
		{
			for (int i = 0; i < unitList.Count; i++)
			{
				NKCUnitClient nkcunitClient = unitList[i];
				if (nkcunitClient != null)
				{
					nkcunitClient.MoveToLastTouchObject();
				}
			}
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x00113BCF File Offset: 0x00111DCF
		private void PlayGameIntro()
		{
			NKCLoadingScreenManager.PlayDungeonIntro(base.GetGameData());
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x00113BDC File Offset: 0x00111DDC
		private void PlayGameOutro()
		{
			NKCLoadingScreenManager.PlayDungeonOutro(base.GetGameData());
		}

		// Token: 0x04003304 RID: 13060
		private bool m_bIntrude;

		// Token: 0x04003305 RID: 13061
		private bool m_bLoadComplete;

		// Token: 0x04003306 RID: 13062
		private NKMGameData m_NKMGameDataDummy;

		// Token: 0x04003307 RID: 13063
		private bool m_bIntrudeDummy;

		// Token: 0x04003308 RID: 13064
		protected NKCEffectManager m_NKCEffectManager = new NKCEffectManager();

		// Token: 0x04003309 RID: 13065
		private NKM_GAME_CAMERA_MODE m_NKM_GAME_CAMERA_MODE;

		// Token: 0x0400330A RID: 13066
		private float m_fCameraNormalTackingWaitTime;

		// Token: 0x0400330B RID: 13067
		private bool m_bCameraDrag;

		// Token: 0x0400330C RID: 13068
		private Vector2 m_vLastDragPos = Vector2.zero;

		// Token: 0x0400330D RID: 13069
		private float m_fCameraStopDragTime;

		// Token: 0x0400330E RID: 13070
		private short m_CameraFocusGameUnitUID;

		// Token: 0x0400330F RID: 13071
		private NKCShipSkillMark m_ShipSkillMark = new NKCShipSkillMark();

		// Token: 0x04003310 RID: 13072
		private bool m_bShipSkillDrag;

		// Token: 0x04003311 RID: 13073
		private float m_fShipSkillDragPosX;

		// Token: 0x04003312 RID: 13074
		private int m_SelectShipSkillID;

		// Token: 0x04003313 RID: 13075
		private List<NKCAssetResourceData> m_listLoadAssetResourceData = new List<NKCAssetResourceData>();

		// Token: 0x04003314 RID: 13076
		private List<NKCAssetResourceData> m_listLoadAssetResourceDataTemp = new List<NKCAssetResourceData>();

		// Token: 0x04003315 RID: 13077
		private List<NKCASEffect> m_listEffectLoadTemp = new List<NKCASEffect>();

		// Token: 0x04003316 RID: 13078
		private LinkedList<NKCASEffect> m_linklistHitEffect = new LinkedList<NKCASEffect>();

		// Token: 0x04003317 RID: 13079
		private bool m_bSimpleHitEffectFrame;

		// Token: 0x04003318 RID: 13080
		private NKCSkillCutInSide m_NKCSkillCutInSideLeft;

		// Token: 0x04003319 RID: 13081
		private NKCSkillCutInSide m_NKCSkillCutInSideRight;

		// Token: 0x0400331A RID: 13082
		private NKCSkillCutInSide m_NKCSkillCutInSideRedLeft;

		// Token: 0x0400331B RID: 13083
		private NKCSkillCutInSide m_NKCSkillCutInSideRedRight;

		// Token: 0x0400331C RID: 13084
		private NKCDamageEffectManager m_DEManager = new NKCDamageEffectManager();

		// Token: 0x0400331D RID: 13085
		private NKCMap m_Map = new NKCMap();

		// Token: 0x0400331E RID: 13086
		private NKCBattleCondition m_BattleCondition = new NKCBattleCondition();

		// Token: 0x0400331F RID: 13087
		private NKMTrackingFloat m_CameraDrag = new NKMTrackingFloat();

		// Token: 0x04003320 RID: 13088
		private float m_fRemainGameTimeBeforeSync;

		// Token: 0x04003321 RID: 13089
		private LinkedList<NKMGameSyncData_Base> m_linklistGameSyncData = new LinkedList<NKMGameSyncData_Base>();

		// Token: 0x04003322 RID: 13090
		private Dictionary<short, NKCUnitViewer> m_dicUnitViewer = new Dictionary<short, NKCUnitViewer>();

		// Token: 0x04003323 RID: 13091
		private List<short> m_listUnitViewerRemove = new List<short>();

		// Token: 0x04003324 RID: 13092
		private float m_CameraDragTime;

		// Token: 0x04003325 RID: 13093
		private float m_CameraDragDist;

		// Token: 0x04003326 RID: 13094
		private bool m_CameraDragPositive;

		// Token: 0x04003327 RID: 13095
		private bool m_bTutorialGameReRespawnAllowed;

		// Token: 0x04003328 RID: 13096
		private NKCGameHud m_NKCGameHud;

		// Token: 0x04003329 RID: 13097
		private bool m_bDeckDrag;

		// Token: 0x0400332A RID: 13098
		private bool m_bDeckTouchDown;

		// Token: 0x0400332B RID: 13099
		private bool m_bShipSkillDeckTouchDown;

		// Token: 0x0400332C RID: 13100
		private bool m_bCameraTouchDown;

		// Token: 0x0400332D RID: 13101
		private long m_bDeckSelectUnitUID;

		// Token: 0x0400332E RID: 13102
		private NKMUnitTemplet m_DeckSelectUnitTemplet;

		// Token: 0x0400332F RID: 13103
		private float m_bDeckSelectPosX;

		// Token: 0x04003330 RID: 13104
		private Dictionary<long, float> m_dicRespawnCostHolder = new Dictionary<long, float>();

		// Token: 0x04003331 RID: 13105
		private Dictionary<long, float> m_dicRespawnCostHolderTC = new Dictionary<long, float>();

		// Token: 0x04003332 RID: 13106
		private Dictionary<long, float> m_dicRespawnCostHolderAssist = new Dictionary<long, float>();

		// Token: 0x04003333 RID: 13107
		private int m_DeckDragIndex = -1;

		// Token: 0x04003334 RID: 13108
		private int m_ShipSkillDeckDragIndex = -1;

		// Token: 0x04003335 RID: 13109
		private bool m_bShowUI = true;

		// Token: 0x04003336 RID: 13110
		private Vector3 m_Vec3Temp;

		// Token: 0x04003337 RID: 13111
		private StringBuilder m_StringBuilder = new StringBuilder();

		// Token: 0x04003338 RID: 13112
		private int m_StartEffectUID;

		// Token: 0x04003339 RID: 13113
		private int m_EndEffectUID;

		// Token: 0x0400333A RID: 13114
		private float m_fLocalGameTime;

		// Token: 0x0400333B RID: 13115
		private float m_fLastRecvTime;

		// Token: 0x0400333C RID: 13116
		private float m_fLatencyLevel;

		// Token: 0x0400333D RID: 13117
		private float m_fNoSyncDataTime;

		// Token: 0x0400333E RID: 13118
		private float m_fGameTimeDifference = 3f;

		// Token: 0x0400333F RID: 13119
		private const float DEFAULT_WIN_LOSE_WAIT_TIME = 3f;

		// Token: 0x04003340 RID: 13120
		private float m_fWinLoseWaitTime = 3f;

		// Token: 0x04003341 RID: 13121
		private const float m_DECK_DRAG_INVALID_GAP_SIZE_Z = 70f;

		// Token: 0x04003342 RID: 13122
		public NKM_TEAM_TYPE m_MyTeam;

		// Token: 0x04003343 RID: 13123
		private NKMMinMaxVec3 m_TempMinMaxVec3 = new NKMMinMaxVec3(0f, 0f, 0f, 0f, 0f, 0f);

		// Token: 0x04003344 RID: 13124
		protected bool m_bSortUnitZDirty = true;

		// Token: 0x04003345 RID: 13125
		protected List<NKCUnitClient> m_listSortUnitZ = new List<NKCUnitClient>();

		// Token: 0x04003346 RID: 13126
		private float m_fGCTime = 60f;

		// Token: 0x04003347 RID: 13127
		private NKC_OPEN_POPUP_TYPE_AFTER_PAUSE m_NKC_OPEN_POPUP_TYPE_AFTER_PAUSE;

		// Token: 0x04003348 RID: 13128
		private bool m_bRespawnUnit;

		// Token: 0x04003349 RID: 13129
		private const float DANGER_MSG_UNIT_REPWAN_TIME = 5f;

		// Token: 0x0400334B RID: 13131
		private bool m_bCostSpeedVoicePlayed;

		// Token: 0x0400334C RID: 13132
		private float m_lastMyShipHpRate;

		// Token: 0x0400334D RID: 13133
		private float m_lastEnemyShipHpRate;
	}
}
