using System;
using System.Collections.Generic;
using Cs.Logging;
using Cs.Math;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200041D RID: 1053
	public abstract class NKMGame
	{
		// Token: 0x06001B88 RID: 7048 RVA: 0x00078AED File Offset: 0x00076CED
		public NKMObjectPool GetObjectPool()
		{
			return this.m_ObjectPool;
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x00078AF5 File Offset: 0x00076CF5
		public NKMDungeonTemplet GetDungeonTemplet()
		{
			return this.m_NKMDungeonTemplet;
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x00078AFD File Offset: 0x00076CFD
		public int GetLiveUnitCountTeamA()
		{
			return this.m_liveUnitUID.Count;
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x00078B0A File Offset: 0x00076D0A
		public float GetCoolTimeReduceTeamA()
		{
			return this.m_fCoolTimeReduceTeamA + this.m_fCoolTimeReduceFullDeckTeamA;
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x00078B19 File Offset: 0x00076D19
		public float GetCoolTimeReduceTeamB()
		{
			return this.m_fCoolTimeReduceTeamB + this.m_fCoolTimeReduceFullDeckTeamB;
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x00078B28 File Offset: 0x00076D28
		private void SetWorldStopTime(float fWorldStopTime)
		{
			this.m_fWorldStopTime = fWorldStopTime;
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x00078B31 File Offset: 0x00076D31
		public float GetWorldStopTime()
		{
			return this.m_fWorldStopTime;
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x00078B39 File Offset: 0x00076D39
		public NKMGameDevModeData GetGameDevModeData()
		{
			return this.m_GameDevModeData;
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x00078B41 File Offset: 0x00076D41
		public NKMGameData GetGameData()
		{
			return this.m_NKMGameData;
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x00078B49 File Offset: 0x00076D49
		public NKMGameRuntimeData GetGameRuntimeData()
		{
			return this.m_NKMGameRuntimeData;
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x00078B51 File Offset: 0x00076D51
		public NKMMapTemplet GetMapTemplet()
		{
			return this.m_NKMMapTemplet;
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x00078B59 File Offset: 0x00076D59
		public virtual void AddKillCount(NKMUnitDataGame unitDataGame)
		{
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x00078B5B File Offset: 0x00076D5B
		public void AddDieUnitRespawnTag(string tag)
		{
			this.AddEventTag("UNITDIE_" + tag, 1);
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x00078B6F File Offset: 0x00076D6F
		public int GetDieUnitRespawnTag(string tag)
		{
			return this.GetEventTag("UNITDIE_" + tag);
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x00078B82 File Offset: 0x00076D82
		public void AddDieDeckRespawnTag(string tag)
		{
			this.AddEventTag("DECKDIE_" + tag, 1);
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x00078B96 File Offset: 0x00076D96
		public int GetDieDeckRespawnTag(string tag)
		{
			return this.GetEventTag("DECKDIE_" + tag);
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x00078BA9 File Offset: 0x00076DA9
		public void SetEventTag(string tag, int value)
		{
			this.m_EventStatusTag[tag] = value;
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x00078BB8 File Offset: 0x00076DB8
		public void AddEventTag(string tag, int value)
		{
			if (!this.m_EventStatusTag.ContainsKey(tag))
			{
				this.m_EventStatusTag.Add(tag, value);
				return;
			}
			this.m_EventStatusTag[tag] = this.m_EventStatusTag[tag] + value;
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x00078BF0 File Offset: 0x00076DF0
		public int GetEventTag(string tag)
		{
			if (this.m_EventStatusTag.ContainsKey(tag))
			{
				return this.m_EventStatusTag[tag];
			}
			return 0;
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x00078C0E File Offset: 0x00076E0E
		protected virtual void IsThreadSafe()
		{
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x00078C10 File Offset: 0x00076E10
		public static bool IsPVE(NKM_GAME_TYPE type)
		{
			switch (type)
			{
			case NKM_GAME_TYPE.NGT_DEV:
			case NKM_GAME_TYPE.NGT_PRACTICE:
			case NKM_GAME_TYPE.NGT_DUNGEON:
			case NKM_GAME_TYPE.NGT_WARFARE:
			case NKM_GAME_TYPE.NGT_DIVE:
			case NKM_GAME_TYPE.NGT_TUTORIAL:
			case NKM_GAME_TYPE.NGT_RAID:
			case NKM_GAME_TYPE.NGT_RAID_SOLO:
			case NKM_GAME_TYPE.NGT_SHADOW_PALACE:
			case NKM_GAME_TYPE.NGT_FIERCE:
			case NKM_GAME_TYPE.NGT_PHASE:
			case NKM_GAME_TYPE.NGT_GUILD_DUNGEON_ARENA:
			case NKM_GAME_TYPE.NGT_GUILD_DUNGEON_BOSS:
			case NKM_GAME_TYPE.NGT_TRIM:
				return true;
			}
			return false;
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x00078C86 File Offset: 0x00076E86
		public static bool IsPVP(NKM_GAME_TYPE type)
		{
			return type == NKM_GAME_TYPE.NGT_PVP_RANK || type == NKM_GAME_TYPE.NGT_ASYNC_PVP || type - NKM_GAME_TYPE.NGT_PVP_PRIVATE <= 4;
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x00078C9C File Offset: 0x00076E9C
		public static bool ApplyUpBanByGameType(NKMGameData gameData, NKM_TEAM_TYPE teamType)
		{
			if (gameData == null)
			{
				return false;
			}
			NKM_GAME_TYPE gameType = gameData.GetGameType();
			if (gameType != NKM_GAME_TYPE.NGT_PVP_RANK && gameType - NKM_GAME_TYPE.NGT_PVP_PRIVATE > 1)
			{
				if (gameType - NKM_GAME_TYPE.NGT_PVP_STRATEGY <= 1)
				{
					if (NKMGame.IsBTeamStaticFunc(teamType))
					{
						return true;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x00078CD6 File Offset: 0x00076ED6
		public static bool IsGuildDungeon(NKM_GAME_TYPE type)
		{
			return type == NKM_GAME_TYPE.NGT_GUILD_DUNGEON_ARENA || type == NKM_GAME_TYPE.NGT_GUILD_DUNGEON_BOSS;
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x00078CE4 File Offset: 0x00076EE4
		public static bool IsPVPSync(NKM_GAME_TYPE type)
		{
			return type == NKM_GAME_TYPE.NGT_PVP_RANK || type == NKM_GAME_TYPE.NGT_PVP_PRIVATE || type == NKM_GAME_TYPE.NGT_PVP_LEAGUE;
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x00078CF8 File Offset: 0x00076EF8
		public NKMGame()
		{
			this.m_NKM_GAME_CLASS_TYPE = NKM_GAME_CLASS_TYPE.NGCT_GAME;
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x00078E24 File Offset: 0x00077024
		public virtual void Init()
		{
			this.m_NKMGameData = null;
			this.m_NKMMapTemplet = null;
			this.m_NKMDungeonTemplet = null;
			this.m_listDieGameUnitUID.Clear();
			this.InitUnit();
			foreach (NKMDamageInst nkmdamageInst in this.m_linklistReAttack)
			{
				if (nkmdamageInst != null)
				{
					this.m_ObjectPool.CloseObj(nkmdamageInst);
				}
			}
			this.m_linklistReAttack.Clear();
			this.m_fDeltaTime = 0f;
			this.m_GameSpeed.SetNowValue(1f);
			this.m_fPlayWaitTimeOrg = 4f;
			this.m_fPlayWaitTime = this.m_fPlayWaitTimeOrg;
			this.m_fFinishWaitTime = 8f;
			this.m_AbsoluteGameTime = 0f;
			this.m_fWorldStopTime = 0f;
			this.m_fRespawnValidLandTeamA = 0.4f;
			this.m_fRespawnValidLandTeamB = 0.4f;
			this.m_fRespawnCostAddTeamA = 0f;
			this.m_fRespawnCostAddTeamB = 0f;
			this.m_fRespawnCostFullDeckAddTeamA = 0f;
			this.m_fRespawnCostFullDeckAddTeamB = 0f;
			this.m_fCoolTimeReduceFullDeckTeamA = 0f;
			this.m_fCoolTimeReduceFullDeckTeamB = 0f;
			this.m_fCoolTimeReduceTeamA = 0f;
			this.m_fCoolTimeReduceTeamB = 0f;
			this.m_EventStatusTag.Clear();
			for (int i = 0; i < this.m_listNKMGameUnitRespawnData.Count; i++)
			{
				this.m_ObjectPool.CloseObj(this.m_listNKMGameUnitRespawnData[i]);
			}
			this.m_listNKMGameUnitRespawnData.Clear();
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x00078FB4 File Offset: 0x000771B4
		public virtual void InitUnit()
		{
			foreach (KeyValuePair<short, NKMUnit> keyValuePair in this.m_dicNKMUnitPool)
			{
				NKMUnit value = keyValuePair.Value;
				if (value != null)
				{
					this.GetObjectPool().CloseObj(value);
				}
			}
			this.m_dicNKMUnitPool.Clear();
			for (int i = 0; i < this.m_listNKMUnit.Count; i++)
			{
				NKMUnit nkmunit = this.m_listNKMUnit[i];
				if (nkmunit != null)
				{
					this.GetObjectPool().CloseObj(nkmunit);
				}
			}
			this.m_listNKMUnit.Clear();
			this.m_dicNKMUnit.Clear();
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x00079050 File Offset: 0x00077250
		protected void InitDungeonEventData()
		{
			if (this.m_NKMDungeonTemplet == null)
			{
				return;
			}
			this.m_listDungeonEventDataTeamA.Clear();
			this.m_listDungeonEventDataTeamB.Clear();
			for (int i = 0; i < this.m_NKMDungeonTemplet.m_listDungeonEventTempletTeamA.Count; i++)
			{
				NKMDungeonEventData nkmdungeonEventData = new NKMDungeonEventData();
				nkmdungeonEventData.m_DungeonEventTemplet = this.m_NKMDungeonTemplet.m_listDungeonEventTempletTeamA[i];
				this.ProcessEventDataCache(ref nkmdungeonEventData);
				this.m_listDungeonEventDataTeamA.Add(nkmdungeonEventData);
			}
			for (int j = 0; j < this.m_NKMDungeonTemplet.m_listDungeonEventTempletTeamB.Count; j++)
			{
				NKMDungeonEventData nkmdungeonEventData2 = new NKMDungeonEventData();
				nkmdungeonEventData2.m_DungeonEventTemplet = this.m_NKMDungeonTemplet.m_listDungeonEventTempletTeamB[j];
				this.ProcessEventDataCache(ref nkmdungeonEventData2);
				this.m_listDungeonEventDataTeamB.Add(nkmdungeonEventData2);
			}
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x00079114 File Offset: 0x00077314
		private void ProcessEventDataCache(ref NKMDungeonEventData cNKMDungeonEventData)
		{
			if (cNKMDungeonEventData.m_DungeonEventTemplet != null)
			{
				switch (cNKMDungeonEventData.m_DungeonEventTemplet.m_EventCondition)
				{
				case NKM_EVENT_START_CONDITION_TYPE.NONE:
				case NKM_EVENT_START_CONDITION_TYPE.ENEMY_BOSS_HP_RATE_LESS:
				case NKM_EVENT_START_CONDITION_TYPE.HAVE_SUMMON_COST:
					break;
				case NKM_EVENT_START_CONDITION_TYPE.UNIT_STATE_START:
				case NKM_EVENT_START_CONDITION_TYPE.UNIT_STATE_END:
				case NKM_EVENT_START_CONDITION_TYPE.TARGET_ALLY_UNIT_HP_RATE_LESS:
				case NKM_EVENT_START_CONDITION_TYPE.TARGET_ENEMY_UNIT_HP_RATE_LESS:
				{
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(cNKMDungeonEventData.m_DungeonEventTemplet.m_EventConditionValue1);
					if (unitTempletBase != null)
					{
						cNKMDungeonEventData.EventConditionCache1 = unitTempletBase.m_UnitID;
						return;
					}
					Log.ErrorAndExit(string.Concat(new string[]
					{
						"DungeonStrID ",
						this.m_NKMDungeonTemplet.m_DungeonTempletBase.m_DungeonStrID,
						", ",
						string.Format("EventCondition : {0}, ", cNKMDungeonEventData.m_DungeonEventTemplet.m_EventCondition),
						string.Format("EventID {0}, Unit Not Found", cNKMDungeonEventData.m_DungeonEventTemplet.m_EventID)
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGame.cs", 2068);
					break;
				}
				default:
					return;
				}
			}
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x000791F9 File Offset: 0x000773F9
		protected virtual void SetDefaultTacticalCommand()
		{
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x000791FC File Offset: 0x000773FC
		protected void AddTacticalCommand(NKMGameTeamData cNKMGameTeamData)
		{
			if (cNKMGameTeamData == null)
			{
				return;
			}
			if (cNKMGameTeamData.m_Operator != null)
			{
				NKMOperatorSkill mainSkill = cNKMGameTeamData.m_Operator.mainSkill;
				if (mainSkill != null)
				{
					NKMOperatorSkillTemplet nkmoperatorSkillTemplet = NKMTempletContainer<NKMOperatorSkillTemplet>.Find(mainSkill.id);
					if (nkmoperatorSkillTemplet != null && nkmoperatorSkillTemplet.m_OperSkillType == OperatorSkillType.m_Tactical)
					{
						NKMTacticalCommandTemplet tacticalCommandTempletByStrID = NKMTacticalCommandManager.GetTacticalCommandTempletByStrID(nkmoperatorSkillTemplet.m_OperSkillTarget);
						if (tacticalCommandTempletByStrID != null)
						{
							NKMTacticalCommandData nkmtacticalCommandData = new NKMTacticalCommandData();
							nkmtacticalCommandData.m_TCID = tacticalCommandTempletByStrID.m_TCID;
							nkmtacticalCommandData.m_Level = mainSkill.level;
							if (this.m_NKMGameData.IsPVP() && this.m_NKMGameData.IsBanOperator(cNKMGameTeamData.m_Operator.id))
							{
								int banOperatorLevel = this.m_NKMGameData.GetBanOperatorLevel(cNKMGameTeamData.m_Operator.id);
								nkmtacticalCommandData.m_fCoolTimeNow = Math.Min(NKMUnitStatManager.m_OperatorTacticalCommandPerBanLevel * (float)banOperatorLevel, NKMUnitStatManager.m_MaxOperatorTacticalCommandPerBanLevel);
							}
							cNKMGameTeamData.m_listTacticalCommandData.Add(nkmtacticalCommandData);
						}
					}
				}
			}
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x000792DC File Offset: 0x000774DC
		public virtual void StartGame(bool bIntrude)
		{
			NKM_GAME_STATE nkm_GAME_STATE = this.m_NKMGameRuntimeData.m_NKM_GAME_STATE;
			if (nkm_GAME_STATE == NKM_GAME_STATE.NGS_LOBBY_MATCHING)
			{
				Log.Error(string.Format("[NKMGame] invalid game state:{0} gameUid:{1}", nkm_GAME_STATE, this.m_NKMGameData.m_GameUID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGame.cs", 2165);
				return;
			}
			if (nkm_GAME_STATE == NKM_GAME_STATE.NGS_INVALID || nkm_GAME_STATE == NKM_GAME_STATE.NGS_STOP || nkm_GAME_STATE == NKM_GAME_STATE.NGS_LOBBY_GAME_SETTING)
			{
				this.SetGameState(NKM_GAME_STATE.NGS_START);
			}
			if (this.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_PRACTICE)
			{
				this.m_fPlayWaitTime = 0.3f;
				this.GetGameRuntimeData().m_NKMGameRuntimeTeamDataB.m_bAIDisable = true;
			}
			if (this.m_NKMDungeonTemplet != null && !this.m_NKMDungeonTemplet.m_bCanUseAuto)
			{
				this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_bAutoRespawn = false;
			}
			if (this.GetGameData().m_NKMGameTeamDataA.IsFullDeck())
			{
				this.m_fRespawnCostFullDeckAddTeamA = 0f;
				this.m_fCoolTimeReduceFullDeckTeamA = 0f;
			}
			NKM_GAME_TYPE gameType = this.GetGameData().GetGameType();
			if (gameType <= NKM_GAME_TYPE.NGT_TUTORIAL)
			{
				if (gameType == NKM_GAME_TYPE.NGT_DUNGEON || gameType == NKM_GAME_TYPE.NGT_TUTORIAL)
				{
					goto IL_117;
				}
			}
			else if (gameType == NKM_GAME_TYPE.NGT_PHASE || gameType == NKM_GAME_TYPE.NGT_TRIM)
			{
				goto IL_117;
			}
			if (this.GetGameData().m_NKMGameTeamDataB.IsFullDeck())
			{
				this.m_fRespawnCostFullDeckAddTeamB = 0f;
				this.m_fCoolTimeReduceFullDeckTeamB = 0f;
			}
			IL_117:
			if (this.m_NKMGameData.m_NKMGameTeamDataA.m_MainShip != null)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_NKMGameData.m_NKMGameTeamDataA.m_MainShip.m_UnitID);
				this.m_fRespawnValidLandTeamA = this.GetValidLandFactor(0, this.GetGameData().IsPVP());
				this.m_fRespawnCostAddTeamA = this.GetCostAddFactorByShip(0, unitTempletBase.m_NKM_UNIT_STYLE_TYPE);
				this.m_fCoolTimeReduceTeamA = this.GetCoolTimeReduceFactorByShip(0, unitTempletBase.m_NKM_UNIT_STYLE_TYPE);
			}
			if (this.m_NKMGameData.m_NKMGameTeamDataB.m_MainShip != null)
			{
				NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(this.m_NKMGameData.m_NKMGameTeamDataB.m_MainShip.m_UnitID);
				this.m_fRespawnValidLandTeamB = this.GetValidLandFactor(0, this.GetGameData().IsPVP());
				this.m_fRespawnCostAddTeamB = this.GetCostAddFactorByShip(0, unitTempletBase2.m_NKM_UNIT_STYLE_TYPE);
				this.m_fCoolTimeReduceTeamB = this.GetCoolTimeReduceFactorByShip(0, unitTempletBase2.m_NKM_UNIT_STYLE_TYPE);
			}
			if (NKMGame.IsPVE(this.m_NKMGameData.m_NKM_GAME_TYPE))
			{
				this.GetGameRuntimeData().m_NKMGameRuntimeTeamDataB.m_NKM_GAME_AUTO_SKILL_TYPE = NKM_GAME_AUTO_SKILL_TYPE.NGST_AUTO;
			}
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x000794F9 File Offset: 0x000776F9
		public virtual void EndGame()
		{
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x000794FB File Offset: 0x000776FB
		public void SetGameSpeed(float fSpeed, float fTrackingTime)
		{
			this.m_GameSpeed.SetNowValue(fSpeed);
			this.m_GameSpeed.SetTracking(1f, fTrackingTime, TRACKING_DATA_TYPE.TDT_FASTER);
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x0007951B File Offset: 0x0007771B
		protected void CreateGameUnitUID()
		{
			this.CreateGameUnitUID(this.m_NKMGameData.m_NKMGameTeamDataA);
			this.CreateGameUnitUID(this.m_NKMGameData.m_NKMGameTeamDataB);
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x00079540 File Offset: 0x00077740
		protected void CreateGameUnitUID(NKMGameTeamData cNKMGameTeamData)
		{
			this.CreateGameUnitUID(cNKMGameTeamData.m_MainShip);
			for (int i = 0; i < cNKMGameTeamData.m_listUnitData.Count; i++)
			{
				this.CreateGameUnitUID(cNKMGameTeamData.m_listUnitData[i]);
			}
			for (int j = 0; j < cNKMGameTeamData.m_listAssistUnitData.Count; j++)
			{
				this.CreateGameUnitUID(cNKMGameTeamData.m_listAssistUnitData[j]);
			}
			for (int k = 0; k < cNKMGameTeamData.m_listEvevtUnitData.Count; k++)
			{
				this.CreateGameUnitUID(cNKMGameTeamData.m_listEvevtUnitData[k]);
			}
			for (int l = 0; l < cNKMGameTeamData.m_listEnvUnitData.Count; l++)
			{
				this.CreateGameUnitUID(cNKMGameTeamData.m_listEnvUnitData[l]);
			}
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x00079600 File Offset: 0x00077800
		protected bool CreateGameUnitUID(NKMUnitData cNKMUnitData)
		{
			if (cNKMUnitData == null)
			{
				return false;
			}
			NKMUnitTemplet unitTemplet = NKMUnitManager.GetUnitTemplet(cNKMUnitData.m_UnitID);
			if (unitTemplet == null)
			{
				Log.Error(string.Format("Invalid Unit Data. unit id : {0}", cNKMUnitData.m_UnitID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGame.cs", 2277);
				return false;
			}
			if (cNKMUnitData.m_listGameUnitUID.Count > 0)
			{
				Log.Error("FATAL. GameUnitUid List Not Empty", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGame.cs", 2283);
				throw new Exception("GameUnitUid List Not Empty");
			}
			for (int i = 0; i < unitTemplet.m_StatTemplet.m_RespawnCount; i++)
			{
				cNKMUnitData.m_listGameUnitUID.Add(this.m_NKMGameData.GetGameUnitUID());
				cNKMUnitData.m_listNearTargetRange.Add(unitTemplet.GetNearTargetRandomRange());
			}
			return true;
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x000796B2 File Offset: 0x000778B2
		protected void CreateDynaminRespawnPoolUnit(bool bAsync)
		{
			this.CreateDynaminRespawnPoolUnit(this.m_NKMGameData.m_NKMGameTeamDataA, NKM_TEAM_TYPE.NTT_A1, bAsync);
			this.CreateDynaminRespawnPoolUnit(this.m_NKMGameData.m_NKMGameTeamDataB, NKM_TEAM_TYPE.NTT_B1, bAsync);
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x000796DC File Offset: 0x000778DC
		protected void CreateDynaminRespawnPoolUnit(NKMGameTeamData cNKMGameTeamData, NKM_TEAM_TYPE eNKM_TEAM_TYPE, bool bAsync)
		{
			for (int i = 0; i < cNKMGameTeamData.m_listDynamicRespawnUnitData.Count; i++)
			{
				NKMDynamicRespawnUnitData nkmdynamicRespawnUnitData = cNKMGameTeamData.m_listDynamicRespawnUnitData[i];
				if (this.m_NKM_GAME_CLASS_TYPE == NKM_GAME_CLASS_TYPE.NGCT_GAME_CLIENT)
				{
					if (nkmdynamicRespawnUnitData != null && !nkmdynamicRespawnUnitData.m_bLoadedClient)
					{
						this.CreatePoolUnit(cNKMGameTeamData, cNKMGameTeamData.m_listDynamicRespawnUnitData[i].m_NKMUnitData, cNKMGameTeamData.m_listDynamicRespawnUnitData[i].m_MasterGameUnitUID, eNKM_TEAM_TYPE, bAsync);
						nkmdynamicRespawnUnitData.m_bLoadedClient = true;
					}
				}
				else if (nkmdynamicRespawnUnitData != null && !nkmdynamicRespawnUnitData.m_bLoadedServer)
				{
					this.CreatePoolUnit(cNKMGameTeamData, cNKMGameTeamData.m_listDynamicRespawnUnitData[i].m_NKMUnitData, cNKMGameTeamData.m_listDynamicRespawnUnitData[i].m_MasterGameUnitUID, eNKM_TEAM_TYPE, bAsync);
					nkmdynamicRespawnUnitData.m_bLoadedServer = true;
				}
			}
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x00079799 File Offset: 0x00077999
		protected void CreatePoolUnit(bool bAsync)
		{
			this.CreatePoolUnit(this.m_NKMGameData.m_NKMGameTeamDataA, NKM_TEAM_TYPE.NTT_A1, bAsync);
			this.CreatePoolUnit(this.m_NKMGameData.m_NKMGameTeamDataB, NKM_TEAM_TYPE.NTT_B1, bAsync);
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x000797C4 File Offset: 0x000779C4
		protected void CreatePoolUnit(NKMGameTeamData cNKMGameTeamData, NKM_TEAM_TYPE eNKM_TEAM_TYPE, bool bAsync)
		{
			this.CreatePoolUnit(cNKMGameTeamData, cNKMGameTeamData.m_MainShip, 0, eNKM_TEAM_TYPE, bAsync);
			for (int i = 0; i < cNKMGameTeamData.m_listUnitData.Count; i++)
			{
				this.CreatePoolUnit(cNKMGameTeamData, cNKMGameTeamData.m_listUnitData[i], 0, eNKM_TEAM_TYPE, bAsync);
			}
			for (int j = 0; j < cNKMGameTeamData.m_listAssistUnitData.Count; j++)
			{
				this.CreatePoolUnit(cNKMGameTeamData, cNKMGameTeamData.m_listAssistUnitData[j], 0, eNKM_TEAM_TYPE, bAsync);
			}
			for (int k = 0; k < cNKMGameTeamData.m_listEvevtUnitData.Count; k++)
			{
				this.CreatePoolUnit(cNKMGameTeamData, cNKMGameTeamData.m_listEvevtUnitData[k], 0, eNKM_TEAM_TYPE, bAsync);
			}
			for (int l = 0; l < cNKMGameTeamData.m_listEnvUnitData.Count; l++)
			{
				this.CreatePoolUnit(cNKMGameTeamData, cNKMGameTeamData.m_listEnvUnitData[l], 0, eNKM_TEAM_TYPE, bAsync);
			}
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x00079898 File Offset: 0x00077A98
		protected bool CreatePoolUnit(NKMGameTeamData cNKMGameTeamData, NKMUnitData cNKMUnitData, short masterGameUnitUID, NKM_TEAM_TYPE eNKM_TEAM_TYPE, bool bAsync)
		{
			if (cNKMUnitData == null)
			{
				return false;
			}
			NKMUnitTemplet unitTemplet = NKMUnitManager.GetUnitTemplet(cNKMUnitData.m_UnitID);
			if (unitTemplet == null)
			{
				return false;
			}
			for (int i = 0; i < cNKMUnitData.m_listGameUnitUID.Count; i++)
			{
				NKMUnit nkmunit = this.CreateNewUnitObj();
				if (nkmunit == null)
				{
					return false;
				}
				if (this.m_dicNKMUnit.ContainsKey(cNKMUnitData.m_listGameUnitUID[i]))
				{
					return false;
				}
				if (this.m_dicNKMUnitPool.ContainsKey(cNKMUnitData.m_listGameUnitUID[i]))
				{
					return false;
				}
				nkmunit.SetBoss(this.IsBoss(cNKMUnitData.m_listGameUnitUID[i]));
				if (i == 0)
				{
					nkmunit.LoadUnit(this, cNKMUnitData, masterGameUnitUID, cNKMUnitData.m_listGameUnitUID[i], cNKMUnitData.m_listNearTargetRange[i], eNKM_TEAM_TYPE, false, bAsync);
				}
				else
				{
					nkmunit.LoadUnit(this, cNKMUnitData, masterGameUnitUID, cNKMUnitData.m_listGameUnitUID[i], cNKMUnitData.m_listNearTargetRange[i], eNKM_TEAM_TYPE, true, bAsync);
				}
				if (!bAsync)
				{
					nkmunit.LoadUnitComplete();
				}
				if (this.m_dicNKMUnitPool.ContainsKey(nkmunit.GetUnitDataGame().m_GameUnitUID))
				{
					return false;
				}
				this.m_dicNKMUnitPool.Add(nkmunit.GetUnitDataGame().m_GameUnitUID, nkmunit);
				if (cNKMGameTeamData != null && (this.m_NKM_GAME_CLASS_TYPE == NKM_GAME_CLASS_TYPE.NGCT_GAME_SERVER || this.m_NKM_GAME_CLASS_TYPE == NKM_GAME_CLASS_TYPE.NGCT_GAME_SERVER_LOCAL))
				{
					foreach (NKMBuffUnitDieEvent nkmbuffUnitDieEvent in unitTemplet.m_listBuffUnitDieEvent)
					{
						foreach (NKMEventRespawn cNKMEventRespawn in nkmbuffUnitDieEvent.m_listNKMEventRespawn)
						{
							this.CreatePoolUnitDynamicRespawn(cNKMGameTeamData, nkmunit, cNKMUnitData, cNKMEventRespawn, NKM_SKILL_TYPE.NST_INVALID);
						}
					}
					foreach (KeyValuePair<short, NKMUnitState> keyValuePair in unitTemplet.m_dicNKMUnitStateID)
					{
						foreach (NKMEventRespawn cNKMEventRespawn2 in keyValuePair.Value.m_listNKMEventRespawn)
						{
							this.CreatePoolUnitDynamicRespawn(cNKMGameTeamData, nkmunit, cNKMUnitData, cNKMEventRespawn2, keyValuePair.Value.m_NKM_SKILL_TYPE);
						}
						if (keyValuePair.Value.m_NKMEventUnitChange != null && nkmunit.GetUnitChangeRespawnPool(keyValuePair.Value.m_NKMEventUnitChange.m_UnitStrID) == 0)
						{
							NKMDynamicRespawnUnitData nkmdynamicRespawnUnitData = new NKMDynamicRespawnUnitData();
							if (keyValuePair.Value.m_NKMEventUnitChange.m_dicSummonSkin != null && !keyValuePair.Value.m_NKMEventUnitChange.m_dicSummonSkin.TryGetValue(cNKMUnitData.m_SkinID, out nkmdynamicRespawnUnitData.m_NKMUnitData.m_SkinID))
							{
								if (cNKMUnitData.m_SkinID == 0)
								{
									nkmdynamicRespawnUnitData.m_NKMUnitData.m_SkinID = 0;
								}
								else if (!keyValuePair.Value.m_NKMEventUnitChange.m_dicSummonSkin.TryGetValue(0, out nkmdynamicRespawnUnitData.m_NKMUnitData.m_SkinID))
								{
									nkmdynamicRespawnUnitData.m_NKMUnitData.m_SkinID = 0;
								}
							}
							if (nkmunit.GetMasterUnitGameUID() == 0)
							{
								nkmdynamicRespawnUnitData.m_MasterGameUnitUID = nkmunit.GetUnitDataGame().m_GameUnitUID;
							}
							else
							{
								nkmdynamicRespawnUnitData.m_MasterGameUnitUID = nkmunit.GetMasterUnitGameUID();
							}
							nkmunit.GetUnitDataGame().m_bChangeUnit = true;
							nkmdynamicRespawnUnitData.m_NKMUnitData.m_UnitID = NKMUnitManager.GetUnitID(keyValuePair.Value.m_NKMEventUnitChange.m_UnitStrID);
							nkmdynamicRespawnUnitData.m_NKMUnitData.FillDataFromRespawnOrigin(cNKMUnitData, false);
							short gameUnitUID = this.m_NKMGameData.GetGameUnitUID();
							nkmdynamicRespawnUnitData.m_NKMUnitData.m_listGameUnitUID.Add(gameUnitUID);
							NKMUnitTemplet unitTemplet2 = nkmdynamicRespawnUnitData.m_NKMUnitData.GetUnitTemplet();
							if (unitTemplet2 != null)
							{
								nkmdynamicRespawnUnitData.m_NKMUnitData.m_listNearTargetRange.Add(unitTemplet2.GetNearTargetRandomRange());
							}
							cNKMUnitData.m_listGameUnitUIDChange.Add(gameUnitUID);
							nkmunit.AddUnitChangeRespawnPool(keyValuePair.Value.m_NKMEventUnitChange.m_UnitStrID, gameUnitUID);
							cNKMGameTeamData.m_listDynamicRespawnUnitData.Add(nkmdynamicRespawnUnitData);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x00079CCC File Offset: 0x00077ECC
		protected bool CreatePoolUnitDynamicRespawn(NKMGameTeamData cNKMGameTeamData, NKMUnit cUnit, NKMUnitData cNKMUnitData, NKMEventRespawn cNKMEventRespawn, NKM_SKILL_TYPE eNKM_SKILL_TYPE)
		{
			NKMDynamicRespawnUnitData nkmdynamicRespawnUnitData = new NKMDynamicRespawnUnitData();
			if (cUnit.GetMasterUnitGameUID() == 0)
			{
				nkmdynamicRespawnUnitData.m_MasterGameUnitUID = cUnit.GetUnitDataGame().m_GameUnitUID;
			}
			else
			{
				nkmdynamicRespawnUnitData.m_MasterGameUnitUID = cUnit.GetMasterUnitGameUID();
			}
			nkmdynamicRespawnUnitData.m_NKMUnitData.m_UnitID = NKMUnitManager.GetUnitID(cNKMEventRespawn.m_UnitStrID);
			nkmdynamicRespawnUnitData.m_NKMUnitData.m_LimitBreakLevel = 0;
			nkmdynamicRespawnUnitData.m_NKMUnitData.m_UnitLevel = 1;
			nkmdynamicRespawnUnitData.m_NKMUnitData.m_bSummonUnit = true;
			if (cNKMEventRespawn.m_bUseMasterLevel)
			{
				nkmdynamicRespawnUnitData.m_NKMUnitData.m_UnitLevel = cNKMUnitData.m_UnitLevel;
			}
			if (cNKMEventRespawn.m_bUseMasterData)
			{
				nkmdynamicRespawnUnitData.m_NKMUnitData.FillDataFromRespawnOrigin(cNKMUnitData, false);
			}
			NKMUnitTemplet unitTemplet = cNKMUnitData.GetUnitTemplet();
			if (unitTemplet == null)
			{
				Log.Error(string.Format("UnitTemplet is null. UserUid:{0}, UnitId:{1}, UnitUid:{2}", cNKMUnitData.m_UserUID, cNKMUnitData.m_UnitID, cNKMUnitData.m_UnitUID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGame.cs", 2511);
				return false;
			}
			NKMUnitTempletBase unitTempletBase = unitTemplet.m_UnitTempletBase;
			if (unitTempletBase == null)
			{
				Log.Error(string.Format("UnitBaseTemplet is null. UserUid:{0}, UnitId:{1}, UnitUid:{2}", cNKMUnitData.m_UserUID, cNKMUnitData.m_UnitID, cNKMUnitData.m_UnitUID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGame.cs", 2518);
				return false;
			}
			if (unitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_SHIP)
			{
				NKMUnitSkillTemplet unitSkillTempletByType = cNKMUnitData.GetUnitSkillTempletByType(eNKM_SKILL_TYPE);
				if (unitSkillTempletByType != null && cNKMEventRespawn.m_bUsePerSkillLevel > 0)
				{
					nkmdynamicRespawnUnitData.m_NKMUnitData.m_UnitLevel += unitSkillTempletByType.m_Level * (int)cNKMEventRespawn.m_bUsePerSkillLevel;
				}
			}
			if (cNKMEventRespawn.m_dicSummonSkin != null && !cNKMEventRespawn.m_dicSummonSkin.TryGetValue(cNKMUnitData.m_SkinID, out nkmdynamicRespawnUnitData.m_NKMUnitData.m_SkinID))
			{
				if (cNKMUnitData.m_SkinID == 0)
				{
					nkmdynamicRespawnUnitData.m_NKMUnitData.m_SkinID = 0;
				}
				else if (!cNKMEventRespawn.m_dicSummonSkin.TryGetValue(0, out nkmdynamicRespawnUnitData.m_NKMUnitData.m_SkinID))
				{
					nkmdynamicRespawnUnitData.m_NKMUnitData.m_SkinID = 0;
				}
			}
			NKMUnitTemplet unitTemplet2 = nkmdynamicRespawnUnitData.m_NKMUnitData.GetUnitTemplet();
			for (int i = 0; i < (int)cNKMEventRespawn.m_MaxCount; i++)
			{
				short gameUnitUID = this.m_NKMGameData.GetGameUnitUID();
				nkmdynamicRespawnUnitData.m_NKMUnitData.m_listGameUnitUID.Add(gameUnitUID);
				if (unitTemplet2 != null)
				{
					nkmdynamicRespawnUnitData.m_NKMUnitData.m_listNearTargetRange.Add(unitTemplet2.GetNearTargetRandomRange());
				}
				cUnit.AddDynamicRespawnPool(cNKMEventRespawn, gameUnitUID);
			}
			cNKMGameTeamData.m_listDynamicRespawnUnitData.Add(nkmdynamicRespawnUnitData);
			return true;
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x00079F14 File Offset: 0x00078114
		protected virtual NKMUnit CreateNewUnitObj()
		{
			if (this.m_NKM_GAME_CLASS_TYPE == NKM_GAME_CLASS_TYPE.NGCT_GAME_SERVER || this.m_NKM_GAME_CLASS_TYPE == NKM_GAME_CLASS_TYPE.NGCT_GAME_SERVER_LOCAL)
			{
				return new NKMUnitServer();
			}
			return null;
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x00079F30 File Offset: 0x00078130
		public bool CheckBossDungeon()
		{
			NKMGameData gameData = this.GetGameData();
			return gameData != null && gameData.m_bBossDungeon;
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x00079F50 File Offset: 0x00078150
		protected void LoadCompleteUnit()
		{
			foreach (KeyValuePair<short, NKMUnit> keyValuePair in this.m_dicNKMUnitPool)
			{
				NKMUnit value = keyValuePair.Value;
				if (value != null)
				{
					value.LoadUnitComplete();
				}
			}
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x00079F8D File Offset: 0x0007818D
		public virtual List<NKMUnit> GetUnitChain()
		{
			return this.m_listNKMUnit;
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x00079F98 File Offset: 0x00078198
		public virtual NKMUnit GetUnitByUnitID(int unitID, bool bChain = true, bool bPool = false)
		{
			if (bChain)
			{
				foreach (KeyValuePair<short, NKMUnit> keyValuePair in this.m_dicNKMUnit)
				{
					NKMUnit value = keyValuePair.Value;
					if (value.GetUnitData().m_UnitID == unitID)
					{
						return value;
					}
					if (value.GetUnitTemplet().m_UnitTempletBase.IsSameBaseUnit(unitID))
					{
						return value;
					}
				}
			}
			if (bPool)
			{
				foreach (KeyValuePair<short, NKMUnit> keyValuePair2 in this.m_dicNKMUnitPool)
				{
					NKMUnit value2 = keyValuePair2.Value;
					if (value2.GetUnitData().m_UnitID == unitID)
					{
						return value2;
					}
					if (value2.GetUnitTemplet().m_UnitTempletBase.IsSameBaseUnit(unitID))
					{
						return value2;
					}
				}
			}
			return null;
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x0007A098 File Offset: 0x00078298
		public virtual NKMUnit GetUnitByUnitID(int unitID, NKM_TEAM_TYPE team, bool bChain = true, bool bPool = false)
		{
			if (bChain)
			{
				foreach (KeyValuePair<short, NKMUnit> keyValuePair in this.m_dicNKMUnit)
				{
					NKMUnit value = keyValuePair.Value;
					if (value.IsAlly(team))
					{
						if (value.GetUnitData().m_UnitID == unitID)
						{
							return value;
						}
						if (value.GetUnitTemplet().m_UnitTempletBase.IsSameBaseUnit(unitID))
						{
							return value;
						}
					}
				}
			}
			if (bPool)
			{
				foreach (KeyValuePair<short, NKMUnit> keyValuePair2 in this.m_dicNKMUnitPool)
				{
					NKMUnit value2 = keyValuePair2.Value;
					if (value2.IsAlly(team))
					{
						if (value2.GetUnitData().m_UnitID == unitID)
						{
							return value2;
						}
						if (value2.GetUnitTemplet().m_UnitTempletBase.IsSameBaseUnit(unitID))
						{
							return value2;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x0007A1AC File Offset: 0x000783AC
		public virtual NKMUnit GetEnemyUnitByUnitID(int unitID, NKM_TEAM_TYPE myTeam, bool bChain = true, bool bPool = false)
		{
			if (bChain)
			{
				foreach (KeyValuePair<short, NKMUnit> keyValuePair in this.m_dicNKMUnit)
				{
					NKMUnit value = keyValuePair.Value;
					if (this.IsEnemy(value.GetTeam(), myTeam))
					{
						if (value.GetUnitData().m_UnitID == unitID)
						{
							return value;
						}
						if (value.GetUnitTemplet().m_UnitTempletBase.IsSameBaseUnit(unitID))
						{
							return value;
						}
					}
				}
			}
			if (bPool)
			{
				foreach (KeyValuePair<short, NKMUnit> keyValuePair2 in this.m_dicNKMUnitPool)
				{
					NKMUnit value2 = keyValuePair2.Value;
					if (this.IsEnemy(value2.GetTeam(), myTeam))
					{
						if (value2.GetUnitData().m_UnitID == unitID)
						{
							return value2;
						}
						if (value2.GetUnitTemplet().m_UnitTempletBase.IsSameBaseUnit(unitID))
						{
							return value2;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x0007A2CC File Offset: 0x000784CC
		public void GetUnitByUnitID(List<NKMUnit> listUnit, int unitID, bool bChain = true, bool bPool = false)
		{
			if (bChain)
			{
				foreach (KeyValuePair<short, NKMUnit> keyValuePair in this.m_dicNKMUnit)
				{
					NKMUnit value = keyValuePair.Value;
					if (value.GetUnitData().m_UnitID == unitID)
					{
						listUnit.Add(value);
					}
				}
			}
			if (bPool)
			{
				foreach (KeyValuePair<short, NKMUnit> keyValuePair2 in this.m_dicNKMUnitPool)
				{
					NKMUnit value2 = keyValuePair2.Value;
					if (value2.GetUnitData().m_UnitID == unitID)
					{
						listUnit.Add(value2);
					}
				}
			}
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x0007A398 File Offset: 0x00078598
		public virtual NKMUnit GetUnit(short GameUnitUID, bool bChain = true, bool bPool = false)
		{
			if (bChain && this.m_dicNKMUnit.ContainsKey(GameUnitUID))
			{
				return this.m_dicNKMUnit[GameUnitUID];
			}
			if (bPool && this.m_dicNKMUnitPool.ContainsKey(GameUnitUID))
			{
				return this.m_dicNKMUnitPool[GameUnitUID];
			}
			return null;
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x0007A3D8 File Offset: 0x000785D8
		public virtual List<NKMUnit> GetTargetingUnitList(short targetedGameUnitUID)
		{
			List<NKMUnit> list = new List<NKMUnit>();
			NKMUnit unit = this.GetUnit(targetedGameUnitUID, true, false);
			if (unit == null)
			{
				return list;
			}
			foreach (KeyValuePair<short, NKMUnit> keyValuePair in this.m_dicNKMUnit)
			{
				NKMUnit value = keyValuePair.Value;
				if (value != unit && value.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DYING && value.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DIE && value.GetUnitSyncData().m_TargetUID == targetedGameUnitUID)
				{
					list.Add(value);
				}
			}
			return list;
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x0007A47C File Offset: 0x0007867C
		public virtual NKMDamageEffect GetDamageEffect(short DEUID)
		{
			return null;
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x0007A47F File Offset: 0x0007867F
		public virtual NKMDamageEffectManager GetDEManager()
		{
			return null;
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x0007A484 File Offset: 0x00078684
		public bool IsGameUnitAllDie(long unitUID)
		{
			NKMUnitData unitDataByUnitUID = this.m_NKMGameData.GetUnitDataByUnitUID(unitUID);
			return unitDataByUnitUID != null && this.IsGameUnitAllDie(unitDataByUnitUID, -1);
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x0007A4AC File Offset: 0x000786AC
		public NKM_ERROR_CODE IsGameUnitAllInBattle(long unitUID)
		{
			bool flag = false;
			int num = 0;
			NKMUnitData unitDataByUnitUID = this.m_NKMGameData.GetUnitDataByUnitUID(unitUID);
			if (unitDataByUnitUID != null)
			{
				for (int i = 0; i < unitDataByUnitUID.m_listGameUnitUID.Count; i++)
				{
					short gameUnitUID = unitDataByUnitUID.m_listGameUnitUID[i];
					NKMUnit unit = this.GetUnit(gameUnitUID, true, false);
					if (unit != null && unit.GetUnitStateNow() != null)
					{
						if (unit.GetUnitStateNow().m_NKM_UNIT_STATE_TYPE == NKM_UNIT_STATE_TYPE.NUST_START)
						{
							return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_RE_RESPAWN_UNIT_START;
						}
						if (unit.GetUnitStateNow().m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_SKILL || unit.GetUnitStateNow().m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_HYPER)
						{
							return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_RE_RESPAWN_UNIT_SKILL;
						}
						if (unit.IsDying())
						{
							flag = true;
						}
						else
						{
							num++;
						}
					}
				}
				for (int j = 0; j < unitDataByUnitUID.m_listGameUnitUIDChange.Count; j++)
				{
					short gameUnitUID2 = unitDataByUnitUID.m_listGameUnitUIDChange[j];
					NKMUnit unit2 = this.GetUnit(gameUnitUID2, true, false);
					if (unit2 != null && unit2.GetUnitStateNow() != null)
					{
						if (unit2.IsDying())
						{
							flag = true;
						}
						else
						{
							num++;
						}
					}
				}
				if (flag && num <= 1)
				{
					return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_RE_RESPAWN_UNIT_DYING;
				}
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x0007A5B0 File Offset: 0x000787B0
		public bool IsGameUnitAllDie(NKMUnitData cNKMUnitData, short dieCallerGameUnitUID = -1)
		{
			for (int i = 0; i < cNKMUnitData.m_listGameUnitUID.Count; i++)
			{
				short num = cNKMUnitData.m_listGameUnitUID[i];
				if (dieCallerGameUnitUID != num)
				{
					if (!this.m_dicNKMUnitPool.ContainsKey(num))
					{
						return false;
					}
					for (int j = 0; j < this.m_listNKMGameUnitDynamicRespawnData.Count; j++)
					{
						if (this.m_listNKMGameUnitDynamicRespawnData[j].m_GameUnitUID == num)
						{
							return false;
						}
					}
				}
			}
			for (int k = 0; k < cNKMUnitData.m_listGameUnitUIDChange.Count; k++)
			{
				short num2 = cNKMUnitData.m_listGameUnitUIDChange[k];
				if (dieCallerGameUnitUID != num2)
				{
					if (!this.m_dicNKMUnitPool.ContainsKey(num2))
					{
						return false;
					}
					for (int l = 0; l < this.m_listNKMGameUnitDynamicRespawnData.Count; l++)
					{
						if (this.m_listNKMGameUnitDynamicRespawnData[l].m_GameUnitUID == num2)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x0007A690 File Offset: 0x00078890
		protected virtual void ProcecssGameTime()
		{
			NKMProfiler.BeginSample("NKCGameClient.ProcecssGameTime");
			if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_PLAY)
			{
				float fRemainGameTime = this.m_NKMGameRuntimeData.m_fRemainGameTime;
				if (this.m_NKMGameRuntimeData.m_fRemainGameTime > 0f)
				{
					this.m_NKMGameRuntimeData.m_fRemainGameTime -= this.m_fDeltaTime;
					if (this.m_NKMGameRuntimeData.m_fRemainGameTime <= 0f)
					{
						this.m_NKMGameRuntimeData.m_fRemainGameTime = 0f;
					}
				}
			}
			NKMProfiler.EndSample();
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x0007A714 File Offset: 0x00078914
		protected virtual bool ProcecssValidLand(NKM_TEAM_TYPE eNKM_TEAM_TYPE)
		{
			bool result = false;
			if (this.m_NKMGameData.m_NKMGameTeamDataA.m_MainShip != null && this.m_NKMGameData.m_NKMGameTeamDataA.m_MainShip.m_listGameUnitUID.Count == 0)
			{
				Log.Error("Check the respawn count or unit id.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGame.cs", 2923);
				return false;
			}
			bool bPvP = this.m_NKMGameData.IsPVP();
			NKMUnit liveBossUnit = this.GetLiveBossUnit(NKM_TEAM_TYPE.NTT_A1);
			NKMUnit liveBossUnit2 = this.GetLiveBossUnit(NKM_TEAM_TYPE.NTT_B1);
			if (liveBossUnit != null && liveBossUnit2 != null)
			{
				if (0.6f > liveBossUnit.GetUnitSyncData().GetHP() / liveBossUnit.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP) && this.m_fRespawnValidLandTeamB < this.GetValidLandFactor(1, bPvP))
				{
					this.m_fRespawnValidLandTeamB = this.GetValidLandFactor(1, bPvP);
					this.m_fRespawnCostAddTeamB = this.GetCostAddFactorByShip(1, liveBossUnit2.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE);
					this.m_fCoolTimeReduceTeamB = this.GetCoolTimeReduceFactorByShip(1, liveBossUnit2.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE);
					if (this.IsBTeam(eNKM_TEAM_TYPE))
					{
						result = true;
					}
				}
				else if (0.3f > liveBossUnit.GetUnitSyncData().GetHP() / liveBossUnit.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP) && this.m_fRespawnValidLandTeamB < this.GetValidLandFactor(2, bPvP))
				{
					this.m_fRespawnValidLandTeamB = this.GetValidLandFactor(2, bPvP);
					this.m_fRespawnCostAddTeamB = this.GetCostAddFactorByShip(2, liveBossUnit2.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE);
					this.m_fCoolTimeReduceTeamB = this.GetCoolTimeReduceFactorByShip(2, liveBossUnit2.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE);
					if (this.IsBTeam(eNKM_TEAM_TYPE))
					{
						result = true;
					}
				}
				if (0.6f > liveBossUnit2.GetUnitSyncData().GetHP() / liveBossUnit2.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP) && this.m_fRespawnValidLandTeamA < this.GetValidLandFactor(1, bPvP))
				{
					this.m_fRespawnValidLandTeamA = this.GetValidLandFactor(1, bPvP);
					this.m_fRespawnCostAddTeamA = this.GetCostAddFactorByShip(1, liveBossUnit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE);
					this.m_fCoolTimeReduceTeamA = this.GetCoolTimeReduceFactorByShip(1, liveBossUnit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE);
					if (this.IsATeam(eNKM_TEAM_TYPE))
					{
						result = true;
					}
				}
				else if (0.3f > liveBossUnit2.GetUnitSyncData().GetHP() / liveBossUnit2.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP) && this.m_fRespawnValidLandTeamA < this.GetValidLandFactor(2, bPvP))
				{
					this.m_fRespawnValidLandTeamA = this.GetValidLandFactor(2, bPvP);
					this.m_fRespawnCostAddTeamA = this.GetCostAddFactorByShip(2, liveBossUnit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE);
					this.m_fCoolTimeReduceTeamA = this.GetCoolTimeReduceFactorByShip(2, liveBossUnit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE);
					if (this.IsATeam(eNKM_TEAM_TYPE))
					{
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x0007A9BC File Offset: 0x00078BBC
		protected float GetValidLandFactor(int level, bool bPvP)
		{
			if (this.GetDungeonType() == NKM_DUNGEON_TYPE.NDT_WAVE)
			{
				level = 2;
			}
			if (this.m_NKMDungeonTemplet != null && this.m_NKMDungeonTemplet.m_listValidLand != null)
			{
				return this.m_NKMDungeonTemplet.m_listValidLand[level];
			}
			if (bPvP)
			{
				return NKMCommonConst.VALID_LAND_PVP[level];
			}
			return NKMCommonConst.VALID_LAND_PVE[level];
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x0007AA10 File Offset: 0x00078C10
		protected float GetCostAddFactorByShip(int level, NKM_UNIT_STYLE_TYPE eNKM_UNIT_STYLE_TYPE)
		{
			switch (eNKM_UNIT_STYLE_TYPE)
			{
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_ASSAULT:
				return this.SHIP_ASSULT_RESPAWN_COST_ADD[level];
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_HEAVY:
				return this.SHIP_HEAVY_RESPAWN_COST_ADD[level];
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_CRUISER:
				return this.SHIP_CRUISER_RESPAWN_COST_ADD[level];
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_SPECIAL:
				return this.SHIP_SPECIAL_RESPAWN_COST_ADD[level];
			default:
				return 0f;
			}
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x0007AA60 File Offset: 0x00078C60
		protected float GetCoolTimeReduceFactorByShip(int level, NKM_UNIT_STYLE_TYPE eNKM_UNIT_STYLE_TYPE)
		{
			switch (eNKM_UNIT_STYLE_TYPE)
			{
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_ASSAULT:
				return this.SHIP_ASSULT_COOLTIME_REDUCE_ADD[level];
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_HEAVY:
				return this.SHIP_HEAVY_COOLTIME_REDUCE_ADD[level];
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_CRUISER:
				return this.SHIP_CRUISER_COOLTIME_REDUCE_ADD[level];
			case NKM_UNIT_STYLE_TYPE.NUST_SHIP_SPECIAL:
				return this.SHIP_SPECIAL_COOLTIME_REDUCE_ADD[level];
			default:
				return 0f;
			}
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x0007AAB0 File Offset: 0x00078CB0
		public NKMUnit GetLiveBossUnit(NKM_TEAM_TYPE eNKM_TEAM_TYPE)
		{
			NKMGameTeamData nkmgameTeamData = null;
			if (eNKM_TEAM_TYPE != NKM_TEAM_TYPE.NTT_A1)
			{
				if (eNKM_TEAM_TYPE == NKM_TEAM_TYPE.NTT_B1)
				{
					nkmgameTeamData = this.m_NKMGameData.m_NKMGameTeamDataB;
				}
			}
			else
			{
				nkmgameTeamData = this.m_NKMGameData.m_NKMGameTeamDataA;
			}
			if (nkmgameTeamData == null || nkmgameTeamData.m_MainShip == null)
			{
				return null;
			}
			for (int i = 0; i < nkmgameTeamData.m_MainShip.m_listGameUnitUID.Count; i++)
			{
				short gameUnitUID = nkmgameTeamData.m_MainShip.m_listGameUnitUID[i];
				NKMUnit unit = this.GetUnit(gameUnitUID, true, false);
				if (unit != null)
				{
					return unit;
				}
			}
			return null;
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x0007AB30 File Offset: 0x00078D30
		public bool IsLiveEnemyStructureUnit(NKM_TEAM_TYPE eMyTeam, List<NKMUnit> finderSortUnitList)
		{
			if (this.GetLiveBossUnit(!this.IsATeam(eMyTeam)) != null)
			{
				return true;
			}
			for (int i = 0; i < finderSortUnitList.Count; i++)
			{
				NKMUnit nkmunit = finderSortUnitList[i];
				if (nkmunit != null && nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DYING && nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DIE && nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_ENV && this.IsEnemy(eMyTeam, nkmunit.GetUnitDataGame().m_NKM_TEAM_TYPE) && nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_TOWER)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x0007ABC6 File Offset: 0x00078DC6
		public NKMUnit GetLiveMyBossUnit(NKM_TEAM_TYPE eMyTeam)
		{
			return this.GetLiveBossUnit(this.IsATeam(eMyTeam));
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x0007ABD5 File Offset: 0x00078DD5
		public NKMUnit GetLiveEnemyBossUnit(NKM_TEAM_TYPE eMyTeam)
		{
			return this.GetLiveBossUnit(!this.IsATeam(eMyTeam));
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x0007ABE8 File Offset: 0x00078DE8
		public NKMUnit GetLiveBossUnit(bool bTeamA)
		{
			NKMGameTeamData nkmgameTeamData;
			if (bTeamA)
			{
				nkmgameTeamData = this.m_NKMGameData.m_NKMGameTeamDataA;
			}
			else
			{
				nkmgameTeamData = this.m_NKMGameData.m_NKMGameTeamDataB;
			}
			if (nkmgameTeamData.m_MainShip == null)
			{
				return null;
			}
			for (int i = 0; i < nkmgameTeamData.m_MainShip.m_listGameUnitUID.Count; i++)
			{
				short gameUnitUID = nkmgameTeamData.m_MainShip.m_listGameUnitUID[i];
				NKMUnit unit = this.GetUnit(gameUnitUID, true, false);
				if (unit != null && unit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_PLAY)
				{
					return unit;
				}
			}
			return null;
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x0007AC6C File Offset: 0x00078E6C
		public NKMUnit GetLiveUnit(bool bTeamA)
		{
			foreach (NKMUnit nkmunit in this.m_listNKMUnit)
			{
				if (nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_PLAY && nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_ENV && this.IsATeam(nkmunit.GetUnitDataGame().m_NKM_TEAM_TYPE_ORG) == bTeamA)
				{
					return nkmunit;
				}
			}
			return null;
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x0007ACF8 File Offset: 0x00078EF8
		public NKMUnit GetLiveUnit(bool bTeamA, bool bAirUnit)
		{
			foreach (NKMUnit nkmunit in this.m_listNKMUnit)
			{
				if (nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_PLAY && nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_ENV && this.IsATeam(nkmunit.GetUnitDataGame().m_NKM_TEAM_TYPE_ORG) == bTeamA && nkmunit.IsAirUnit() == bAirUnit)
				{
					return nkmunit;
				}
			}
			return null;
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x0007AD8C File Offset: 0x00078F8C
		public float GetLiveShipHPRate(NKM_TEAM_TYPE eNKM_TEAM_TYPE)
		{
			NKMGameTeamData nkmgameTeamData = null;
			if (eNKM_TEAM_TYPE == NKM_TEAM_TYPE.NTT_A1)
			{
				nkmgameTeamData = this.m_NKMGameData.m_NKMGameTeamDataA;
			}
			else if (eNKM_TEAM_TYPE == NKM_TEAM_TYPE.NTT_B1)
			{
				nkmgameTeamData = this.m_NKMGameData.m_NKMGameTeamDataB;
			}
			if (nkmgameTeamData == null || nkmgameTeamData.m_MainShip == null)
			{
				return 0f;
			}
			float num = 0f;
			float num2 = 0f;
			for (int i = 0; i < nkmgameTeamData.m_MainShip.m_listGameUnitUID.Count; i++)
			{
				short gameUnitUID = nkmgameTeamData.m_MainShip.m_listGameUnitUID[i];
				NKMUnit unit = this.GetUnit(gameUnitUID, true, false);
				if (unit != null)
				{
					num += unit.GetUnitSyncData().GetHP();
					num2 += unit.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP);
				}
			}
			if (num2 <= 0f)
			{
				return 0f;
			}
			return num / num2;
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x0007AE4D File Offset: 0x0007904D
		public virtual float CaculateFiercePointByDamage(NKMUnit target)
		{
			return 0f;
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x0007AE54 File Offset: 0x00079054
		public virtual float CaculateTrimPointByDamage(NKMUnit target)
		{
			return 0f;
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x0007AE5C File Offset: 0x0007905C
		protected virtual void ProcecssRespawnCost()
		{
			if (this.GetWorldStopTime() <= 0f)
			{
				float num = this.m_fDeltaTime * 0.3f;
				float num2 = this.m_fDeltaTime * 0.3f;
				num -= num * this.m_NKMGameData.m_fRespawnCostMinusPercentForTeamA;
				if (this.m_NKMDungeonTemplet != null)
				{
					num *= this.m_NKMDungeonTemplet.m_fCostSpeedRateA;
					num2 *= this.m_NKMDungeonTemplet.m_fCostSpeedRateB;
				}
				num += Math.Max(this.m_fDeltaTime * this.m_NKMGameData.fExtraRespawnCostAddForA, -1f);
				num2 += Math.Max(this.m_fDeltaTime * this.m_NKMGameData.fExtraRespawnCostAddForB, -1f);
				switch (this.GetDungeonType())
				{
				case NKM_DUNGEON_TYPE.NDT_WAVE:
				case NKM_DUNGEON_TYPE.NDT_RAID:
				case NKM_DUNGEON_TYPE.NDT_SOLO_RAID:
					this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCost += num;
					this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fRespawnCost += num2;
					break;
				case NKM_DUNGEON_TYPE.NDT_DAMAGE_ACCRUE:
					this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCost += num * 2f;
					this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fRespawnCost += num2;
					break;
				default:
					if (this.m_NKMGameRuntimeData.m_fRemainGameTime <= this.GetGameData().m_fDoubleCostTime)
					{
						this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCost += num * 1.5f;
						this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fRespawnCost += num2 * 1.5f;
					}
					else
					{
						this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCost += num;
						this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fRespawnCost += num2;
					}
					break;
				}
				this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCost += num * (this.m_fRespawnCostAddTeamA + this.m_fRespawnCostFullDeckAddTeamA);
				this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fRespawnCost += num2 * (this.m_fRespawnCostAddTeamB + this.m_fRespawnCostFullDeckAddTeamB);
			}
			if (this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCost > 10f)
			{
				this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCost = 10f;
			}
			if (this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fRespawnCost > 10f)
			{
				this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fRespawnCost = 10f;
			}
			if (this.GetWorldStopTime() <= 0f)
			{
				this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCostAssist += this.m_fDeltaTime * 0.3f * 0.5f;
				this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fRespawnCostAssist += this.m_fDeltaTime * 0.3f * 0.5f;
			}
			if (this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCostAssist > 10f)
			{
				this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCostAssist = 10f;
			}
			if (this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fRespawnCostAssist > 10f)
			{
				this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fRespawnCostAssist = 10f;
			}
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x0007B178 File Offset: 0x00079378
		protected void CalcLiveUnitCountATeam()
		{
			this.m_liveUnitUID.Clear();
			for (int i = 0; i < this.GetGameData().m_NKMGameTeamDataA.m_listUnitData.Count; i++)
			{
				NKMUnitData nkmunitData = this.GetGameData().m_NKMGameTeamDataA.m_listUnitData[i];
				if (nkmunitData != null)
				{
					int j = 0;
					while (j < nkmunitData.m_listGameUnitUID.Count)
					{
						if (this.GetUnit(nkmunitData.m_listGameUnitUID[j], true, false) != null)
						{
							if (!this.m_liveUnitUID.Contains(nkmunitData.m_UnitUID))
							{
								this.m_liveUnitUID.Add(nkmunitData.m_UnitUID);
								break;
							}
							break;
						}
						else
						{
							j++;
						}
					}
				}
			}
			for (int k = 0; k < this.m_listNKMGameUnitRespawnData.Count; k++)
			{
				NKMGameUnitRespawnData nkmgameUnitRespawnData = this.m_listNKMGameUnitRespawnData[k];
				if (nkmgameUnitRespawnData != null && this.IsATeam(nkmgameUnitRespawnData.m_eNKM_TEAM_TYPE) && !this.m_liveUnitUID.Contains(nkmgameUnitRespawnData.m_UnitUID))
				{
					this.m_liveUnitUID.Add(nkmgameUnitRespawnData.m_UnitUID);
				}
			}
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x0007B280 File Offset: 0x00079480
		protected virtual void ProcessUnit()
		{
			this.m_listDieGameUnitUID.Clear();
			bool flag = false;
			foreach (KeyValuePair<short, NKMUnit> keyValuePair in this.m_dicNKMUnit)
			{
				NKMUnit value = keyValuePair.Value;
				value.Update(this.m_fDeltaTime);
				if (this.m_NKMGameRuntimeData.m_fShipDamage > 0f)
				{
					long unitUID = value.GetUnitData().m_UnitUID;
					if (this.m_NKMGameData.GetAnyTeamMainShipDataByUnitUID(unitUID) != null)
					{
						float num = value.GetUnitSyncData().GetHP();
						if (num > 0f)
						{
							num -= this.m_NKMGameRuntimeData.m_fShipDamage * this.m_fDeltaTime;
							if (num <= 0f)
							{
								if (this.m_NKM_GAME_CLASS_TYPE == NKM_GAME_CLASS_TYPE.NGCT_GAME_CLIENT)
								{
									num = 1f;
								}
								flag = true;
							}
							value.GetUnitSyncData().SetHP(num);
						}
					}
				}
			}
			if (flag)
			{
				this.m_NKMGameRuntimeData.m_fShipDamage = 0f;
			}
			foreach (KeyValuePair<short, NKMUnit> keyValuePair in this.m_dicNKMUnit)
			{
				NKMUnit value2 = keyValuePair.Value;
				value2.Update2();
				if (value2.IsDie() && this.m_NKMGameData.GetAnyTeamMainShipDataByUnitUID(value2.GetUnitData().m_UnitUID) == null)
				{
					this.m_listDieGameUnitUID.Add(value2.GetUnitSyncData().m_GameUnitUID);
				}
			}
			for (int i = 0; i < this.m_listDieGameUnitUID.Count; i++)
			{
				short key = this.m_listDieGameUnitUID[i];
				NKMUnit nkmunit = this.m_dicNKMUnit[key];
				this.m_dicNKMUnitPool.Add(key, nkmunit);
				this.m_dicNKMUnit.Remove(key);
				this.m_listNKMUnit.Remove(nkmunit);
				nkmunit.SetDie(true);
			}
			this.m_listDieGameUnitUID.Clear();
			this.CalcLiveUnitCountATeam();
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x0007B450 File Offset: 0x00079650
		protected virtual void ProcessTacticalCommand()
		{
			if (this.GetGameData().m_NKMGameTeamDataA != null)
			{
				for (int i = 0; i < this.GetGameData().m_NKMGameTeamDataA.m_listTacticalCommandData.Count; i++)
				{
					NKMTacticalCommandData nkmtacticalCommandData = this.GetGameData().m_NKMGameTeamDataA.m_listTacticalCommandData[i];
					if (nkmtacticalCommandData != null && this.GetWorldStopTime() <= 0f)
					{
						nkmtacticalCommandData.Update(this.m_fDeltaTime);
					}
				}
			}
			if (this.GetGameData().m_NKMGameTeamDataB != null)
			{
				for (int j = 0; j < this.GetGameData().m_NKMGameTeamDataB.m_listTacticalCommandData.Count; j++)
				{
					NKMTacticalCommandData nkmtacticalCommandData2 = this.GetGameData().m_NKMGameTeamDataB.m_listTacticalCommandData[j];
					if (nkmtacticalCommandData2 != null && this.GetWorldStopTime() <= 0f)
					{
						nkmtacticalCommandData2.Update(this.m_fDeltaTime);
					}
				}
			}
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x0007B520 File Offset: 0x00079720
		protected void ProcessReAttack()
		{
			NKMProfiler.BeginSample("NKMGame.ProcessReAttack");
			for (LinkedListNode<NKMDamageInst> linkedListNode = this.m_linklistReAttack.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				NKMDamageInst value = linkedListNode.Value;
				if (value != null && value.m_fReAttackGap > 0f)
				{
					value.m_fReAttackGap -= this.m_fDeltaTime;
					if (value.m_fReAttackGap <= 0f)
					{
						value.m_ReAttackCount++;
						value.m_fReAttackGap = value.m_Templet.m_fReAttackGap;
						NKMUnit unit = this.GetUnit(value.m_DefenderUID, true, false);
						if (unit != null)
						{
							unit.DamageReact(value, false);
							if (value.m_ReActResult == NKM_REACT_TYPE.NRT_NO)
							{
								this.m_ObjectPool.CloseObj(value);
								LinkedListNode<NKMDamageInst> next = linkedListNode.Next;
								this.m_linklistReAttack.Remove(linkedListNode);
								linkedListNode = next;
								continue;
							}
						}
						if (value.m_ReAttackCount >= value.m_Templet.m_ReAttackCount)
						{
							this.m_ObjectPool.CloseObj(value);
							LinkedListNode<NKMDamageInst> next2 = linkedListNode.Next;
							this.m_linklistReAttack.Remove(linkedListNode);
							linkedListNode = next2;
							continue;
						}
					}
				}
			}
			NKMProfiler.EndSample();
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x0007B634 File Offset: 0x00079834
		public NKMUnit FindTarget(NKMUnit finderUnit, List<NKMUnit> finderSortUnitList, NKMFindTargetData cNKMFindTargetData, NKM_TEAM_TYPE eFinderTeam, float fFinderPosX, bool bFinderRight)
		{
			if (cNKMFindTargetData == null)
			{
				return null;
			}
			return this.FindTarget(finderUnit, finderSortUnitList, cNKMFindTargetData.m_FindTargetType, eFinderTeam, fFinderPosX, cNKMFindTargetData.m_fSeeRange, cNKMFindTargetData.m_bNoBackTarget, cNKMFindTargetData.m_bNoFrontTarget, bFinderRight, cNKMFindTargetData.m_bCanTargetBoss, cNKMFindTargetData.m_hsFindTargetRolePriority, cNKMFindTargetData.m_bPriorityOnly);
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x0007B680 File Offset: 0x00079880
		public NKMUnit FindTarget(NKMUnit finderUnit, List<NKMUnit> finderSortUnitList, NKM_FIND_TARGET_TYPE NKM_FIND_TARGET_TYPE, NKM_TEAM_TYPE eFinderTeam, float fFinderPosX, float fSeeRange, bool bNoBackTarget, bool bNoFrontTarget, bool bFinderRight, bool bCanTargetBoss, HashSet<NKM_UNIT_ROLE_TYPE> hsPriorityRole, bool bPriorityOnly)
		{
			bool flag;
			switch (NKM_FIND_TARGET_TYPE)
			{
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY:
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_AIR_FIRST:
			{
				flag = true;
				bool bEnemy = true;
				bool bLand = true;
				bool bAir = true;
				bool bStructureOnly = false;
				goto IL_21F;
			}
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_LAND:
			{
				flag = true;
				bool bEnemy = true;
				bool bLand = true;
				bool bAir = false;
				bool bStructureOnly = false;
				goto IL_21F;
			}
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_AIR:
			{
				flag = true;
				bool bEnemy = true;
				bool bLand = false;
				bool bAir = true;
				bool bStructureOnly = false;
				goto IL_21F;
			}
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_LAND_RANGER_SUPPORTER_SNIPER_FIRST:
			{
				flag = true;
				bool bEnemy = true;
				bool bLand = true;
				bool bAir = false;
				bool bStructureOnly = false;
				goto IL_21F;
			}
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_BOSS_LAST:
			{
				flag = false;
				bool bEnemy = true;
				bool bLand = true;
				bool bAir = true;
				bool bStructureOnly = false;
				goto IL_21F;
			}
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_LAND:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_LAND_BOSS_LAST:
			{
				flag = false;
				bool bEnemy = true;
				bool bLand = true;
				bool bAir = false;
				bool bStructureOnly = false;
				goto IL_21F;
			}
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_AIR:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_AIR_BOSS_LAST:
			{
				flag = false;
				bool bEnemy = true;
				bool bLand = false;
				bool bAir = true;
				bool bStructureOnly = false;
				goto IL_21F;
			}
			case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_ONLY:
			{
				bool bEnemy;
				bool bLand;
				bool bAir;
				bool bStructureOnly;
				if (this.GetLiveEnemyBossUnit(eFinderTeam) != null)
				{
					flag = true;
					bEnemy = true;
					bLand = false;
					bAir = true;
					bStructureOnly = true;
					goto IL_21F;
				}
				flag = true;
				bEnemy = true;
				bLand = true;
				bAir = true;
				bStructureOnly = false;
				goto IL_21F;
			}
			case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS:
			{
				bool bEnemy;
				bool bLand;
				bool bAir;
				bool bStructureOnly;
				if (this.IsLiveEnemyStructureUnit(eFinderTeam, finderSortUnitList))
				{
					flag = true;
					bEnemy = true;
					bLand = true;
					bAir = true;
					bStructureOnly = true;
					goto IL_21F;
				}
				flag = true;
				bEnemy = true;
				bLand = true;
				bAir = true;
				bStructureOnly = false;
				goto IL_21F;
			}
			case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_LAND:
			{
				bool bEnemy;
				bool bLand;
				bool bAir;
				bool bStructureOnly;
				if (this.IsLiveEnemyStructureUnit(eFinderTeam, finderSortUnitList))
				{
					flag = true;
					bEnemy = true;
					bLand = true;
					bAir = false;
					bStructureOnly = true;
					goto IL_21F;
				}
				flag = true;
				bEnemy = true;
				bLand = true;
				bAir = false;
				bStructureOnly = false;
				goto IL_21F;
			}
			case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_AIR:
			{
				bool bEnemy;
				bool bLand;
				bool bAir;
				bool bStructureOnly;
				if (this.IsLiveEnemyStructureUnit(eFinderTeam, finderSortUnitList))
				{
					flag = true;
					bEnemy = true;
					bLand = false;
					bAir = true;
					bStructureOnly = true;
					goto IL_21F;
				}
				flag = true;
				bEnemy = true;
				bLand = false;
				bAir = true;
				bStructureOnly = false;
				goto IL_21F;
			}
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM:
			{
				flag = true;
				bool bEnemy = false;
				bool bLand = true;
				bool bAir = true;
				bool bStructureOnly = false;
				goto IL_21F;
			}
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LAND:
			{
				flag = true;
				bool bEnemy = false;
				bool bLand = true;
				bool bAir = false;
				bool bStructureOnly = false;
				goto IL_21F;
			}
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_AIR:
			{
				flag = true;
				bool bEnemy = false;
				bool bLand = false;
				bool bAir = true;
				bool bStructureOnly = false;
				goto IL_21F;
			}
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LOW_HP:
			{
				flag = true;
				bool bEnemy = false;
				bool bLand = true;
				bool bAir = true;
				bool bStructureOnly = false;
				goto IL_21F;
			}
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LOW_HP_LAND:
			{
				flag = true;
				bool bEnemy = false;
				bool bLand = true;
				bool bAir = false;
				bool bStructureOnly = false;
				goto IL_21F;
			}
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LOW_HP_AIR:
			{
				flag = true;
				bool bEnemy = false;
				bool bLand = false;
				bool bAir = true;
				bool bStructureOnly = false;
				goto IL_21F;
			}
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_MY_TEAM:
			{
				flag = false;
				bool bEnemy = false;
				bool bLand = true;
				bool bAir = true;
				bool bStructureOnly = false;
				goto IL_21F;
			}
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_MY_TEAM_LAND:
			{
				flag = false;
				bool bEnemy = false;
				bool bLand = true;
				bool bAir = false;
				bool bStructureOnly = false;
				goto IL_21F;
			}
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_MY_TEAM_AIR:
			{
				flag = false;
				bool bEnemy = false;
				bool bLand = false;
				bool bAir = true;
				bool bStructureOnly = false;
				goto IL_21F;
			}
			}
			return null;
			IL_21F:
			NKMUnit result;
			if (flag)
			{
				bool bEnemy;
				bool bLand;
				bool bAir;
				bool bStructureOnly;
				result = this.FindNearUnit(finderUnit, finderSortUnitList, eFinderTeam, fFinderPosX, bEnemy, fSeeRange, bLand, bAir, bStructureOnly, bNoBackTarget, bNoFrontTarget, bFinderRight, NKM_FIND_TARGET_TYPE, bCanTargetBoss, hsPriorityRole, bPriorityOnly);
			}
			else
			{
				bool bEnemy;
				bool bLand;
				bool bAir;
				bool bStructureOnly;
				result = this.FindFarUnit(finderUnit, finderSortUnitList, eFinderTeam, fFinderPosX, bEnemy, fSeeRange, bLand, bAir, bStructureOnly, bNoBackTarget, bNoFrontTarget, bFinderRight, NKM_FIND_TARGET_TYPE, bCanTargetBoss, hsPriorityRole, bPriorityOnly);
			}
			return result;
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x0007B8F8 File Offset: 0x00079AF8
		protected NKMUnit FindNearUnit(NKMUnit finderUnit, List<NKMUnit> finderSortUnitList, NKM_TEAM_TYPE eFinderTeam, float fFinderPosX, bool bEnemy, float seeRange, bool bLand, bool bAir, bool bStructureOnly, bool bNoBackTarget, bool bNoFrontTarget, bool bFinderRight, NKM_FIND_TARGET_TYPE NKM_FIND_TARGET_TYPE, bool bCanTargetBoss, HashSet<NKM_UNIT_ROLE_TYPE> hsPriorityRole, bool bPriorityOnly)
		{
			if (finderSortUnitList == null)
			{
				return null;
			}
			NKMUnit nkmunit = null;
			bool flag = false;
			for (int i = 0; i < finderSortUnitList.Count; i++)
			{
				NKMUnit nkmunit2 = finderSortUnitList[i];
				if (nkmunit2 != null && Math.Abs(nkmunit2.GetUnitSyncData().m_PosX - fFinderPosX) < seeRange && nkmunit2.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DYING && nkmunit2.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DIE && nkmunit2.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_ENV && bEnemy == this.IsEnemy(eFinderTeam, nkmunit2.GetUnitDataGame().m_NKM_TEAM_TYPE) && ((!flag && !bPriorityOnly) || hsPriorityRole.Contains(nkmunit2.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE)) && (!bEnemy || (finderUnit != null && finderUnit.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_DETECTER)) || !nkmunit2.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_CLOCKING)) && (nkmunit2.GetUnitStateNow() == null || !nkmunit2.GetUnitStateNow().m_bForceNoTargeted))
				{
					bool flag2 = false;
					if (this.IsBoss(nkmunit2.GetUnitDataGame().m_GameUnitUID))
					{
						flag2 = true;
					}
					if ((flag2 || ((bAir || !nkmunit2.IsAirUnit()) && (bLand || nkmunit2.IsAirUnit()) && (!bStructureOnly || nkmunit2.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_TOWER) && NKM_FIND_TARGET_TYPE != NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_ONLY)) && (!flag2 || bCanTargetBoss) && (!bNoBackTarget || ((!bFinderRight || nkmunit2.GetUnitSyncData().m_PosX >= fFinderPosX) && (bFinderRight || nkmunit2.GetUnitSyncData().m_PosX <= fFinderPosX))) && (!bNoFrontTarget || ((!bFinderRight || nkmunit2.GetUnitSyncData().m_PosX <= fFinderPosX) && (bFinderRight || nkmunit2.GetUnitSyncData().m_PosX >= fFinderPosX))))
					{
						bool flag3 = false;
						if (NKM_FIND_TARGET_TYPE != NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_AIR_FIRST)
						{
							if (NKM_FIND_TARGET_TYPE != NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_LAND_RANGER_SUPPORTER_SNIPER_FIRST)
							{
								if (NKM_FIND_TARGET_TYPE - NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LOW_HP <= 2)
								{
									if (nkmunit == null)
									{
										nkmunit = nkmunit2;
									}
									else if (nkmunit2.GetUnitSyncData().GetHP() < nkmunit.GetUnitSyncData().GetHP())
									{
										nkmunit = nkmunit2;
									}
								}
								else
								{
									nkmunit = nkmunit2;
									flag3 = true;
								}
							}
							else if (nkmunit == null)
							{
								nkmunit = nkmunit2;
							}
							else if (nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE != NKM_UNIT_ROLE_TYPE.NURT_RANGER && nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE != NKM_UNIT_ROLE_TYPE.NURT_SNIPER && nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE != NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER && (nkmunit2.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_RANGER || nkmunit2.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_SNIPER || nkmunit2.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER))
							{
								nkmunit = nkmunit2;
							}
						}
						else if (nkmunit == null)
						{
							nkmunit = nkmunit2;
						}
						else if (!nkmunit.IsAirUnit() && nkmunit2.IsAirUnit())
						{
							nkmunit = nkmunit2;
						}
						if (nkmunit != null && hsPriorityRole != null && hsPriorityRole.Count > 0)
						{
							flag = hsPriorityRole.Contains(nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE);
						}
						if (flag3 && (flag || hsPriorityRole == null || hsPriorityRole.Count == 0))
						{
							break;
						}
					}
				}
			}
			return nkmunit;
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x0007BBE8 File Offset: 0x00079DE8
		protected NKMUnit FindFarUnit(NKMUnit finderUnit, List<NKMUnit> finderSortUnitList, NKM_TEAM_TYPE eFinderTeam, float fFinderPosX, bool bEnemy, float seeRange, bool bLand, bool bAir, bool bStructureOnly, bool bNoBackTarget, bool bNoFrontTarget, bool bFinderRight, NKM_FIND_TARGET_TYPE NKM_FIND_TARGET_TYPE, bool bCanTargetBoss, HashSet<NKM_UNIT_ROLE_TYPE> hsPriorityRole, bool bPriorityOnly)
		{
			NKMUnit nkmunit = null;
			if (finderSortUnitList != null)
			{
				bool flag = false;
				for (int i = finderSortUnitList.Count - 1; i >= 0; i--)
				{
					NKMUnit nkmunit2 = finderSortUnitList[i];
					if (nkmunit2 != null && Math.Abs(nkmunit2.GetUnitSyncData().m_PosX - fFinderPosX) < seeRange && nkmunit2.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DYING && nkmunit2.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DIE && nkmunit2.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_ENV && bEnemy == this.IsEnemy(eFinderTeam, nkmunit2.GetUnitDataGame().m_NKM_TEAM_TYPE) && ((!flag && !bPriorityOnly) || hsPriorityRole.Contains(nkmunit2.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE)) && (!bEnemy || (finderUnit != null && finderUnit.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_DETECTER)) || !nkmunit2.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_CLOCKING)) && (nkmunit2.GetUnitStateNow() == null || !nkmunit2.GetUnitStateNow().m_bForceNoTargeted))
					{
						bool flag2 = false;
						if (this.IsBoss(nkmunit2.GetUnitDataGame().m_GameUnitUID))
						{
							flag2 = true;
						}
						if ((flag2 || ((bAir || !nkmunit2.IsAirUnit()) && (bLand || nkmunit2.IsAirUnit()) && (!bStructureOnly || nkmunit2.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_TOWER))) && (!flag2 || bCanTargetBoss) && (!bNoBackTarget || ((!bFinderRight || nkmunit2.GetUnitSyncData().m_PosX >= fFinderPosX) && (bFinderRight || nkmunit2.GetUnitSyncData().m_PosX <= fFinderPosX))) && (!bNoFrontTarget || ((!bFinderRight || nkmunit2.GetUnitSyncData().m_PosX <= fFinderPosX) && (bFinderRight || nkmunit2.GetUnitSyncData().m_PosX >= fFinderPosX))))
						{
							bool flag3 = false;
							if (NKM_FIND_TARGET_TYPE - NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_BOSS_LAST <= 2)
							{
								if (nkmunit == null)
								{
									nkmunit = nkmunit2;
								}
								else if (nkmunit.IsBoss() && !nkmunit2.IsBoss())
								{
									nkmunit = nkmunit2;
								}
							}
							else
							{
								nkmunit = nkmunit2;
								flag3 = true;
							}
							if (nkmunit != null && hsPriorityRole != null && hsPriorityRole.Count > 0)
							{
								flag = hsPriorityRole.Contains(nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE);
							}
							if (flag3 && (flag || hsPriorityRole == null || hsPriorityRole.Count == 0))
							{
								break;
							}
						}
					}
				}
			}
			return nkmunit;
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x0007BE0C File Offset: 0x0007A00C
		public bool DamageCheck(NKMDamageInst cNKMDamageInst, NKMEventAttack cNKMEventAttack, bool bDieAttack = false)
		{
			if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE != NKM_GAME_STATE.NGS_PLAY)
			{
				return false;
			}
			bool flag = false;
			if (cNKMDamageInst == null || cNKMDamageInst.m_Templet == null)
			{
				return false;
			}
			NKM_REACTOR_TYPE attackerType = cNKMDamageInst.m_AttackerType;
			if (attackerType != NKM_REACTOR_TYPE.NRT_GAME_UNIT)
			{
				if (attackerType == NKM_REACTOR_TYPE.NRT_DAMAGE_EFFECT)
				{
					flag = this.EffectToUnit(cNKMDamageInst, cNKMEventAttack, bDieAttack);
				}
			}
			else
			{
				flag = this.UnitToUnit(cNKMDamageInst, cNKMEventAttack);
			}
			if (flag)
			{
				this.ProcessExtraHit(cNKMDamageInst, cNKMEventAttack);
			}
			return flag;
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x0007BE6C File Offset: 0x0007A06C
		private void ProcessExtraHit(NKMDamageInst cNKMDamageInst, NKMEventAttack cNKMEventAttack)
		{
			if (cNKMDamageInst.m_Templet.m_ExtraHitDamageTemplet == null)
			{
				return;
			}
			NKMUnit unit = this.GetUnit(cNKMDamageInst.m_AttackerGameUnitUID, true, false);
			if (unit == null)
			{
				return;
			}
			int count = cNKMDamageInst.m_listHitUnit.Count;
			if (cNKMDamageInst.m_Templet.m_ExtraHitCountRange.m_Min <= count && count <= cNKMDamageInst.m_Templet.m_ExtraHitCountRange.m_Max)
			{
				for (int i = 0; i < count; i++)
				{
					NKMUnit unit2 = this.GetUnit(cNKMDamageInst.m_listHitUnit[i], true, false);
					if (unit2 != null)
					{
						this.ProcessDamageTemplet(cNKMDamageInst.m_Templet.m_ExtraHitDamageTemplet, unit, unit2, true, false, cNKMDamageInst.m_AttackerUnitSkillTemplet, cNKMDamageInst.m_Templet.m_ExtraHitAttribute);
					}
				}
			}
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x0007BF18 File Offset: 0x0007A118
		protected bool UnitToUnit(NKMDamageInst cNKMDamageInst, NKMEventAttack cNKMEventAttack)
		{
			bool flag = false;
			NKMUnit unit = this.GetUnit(cNKMDamageInst.m_AttackerGameUnitUID, true, false);
			if (unit == null)
			{
				return false;
			}
			cNKMDamageInst.m_EventAttack = cNKMEventAttack;
			float num = 0f;
			bool bSplashHit = cNKMEventAttack.m_AttackUnitCount > 1;
			if (cNKMEventAttack.m_AttackTargetUnit)
			{
				if (cNKMEventAttack.m_AttackUnitCount == 1 && cNKMDamageInst.m_AttackCount >= 1)
				{
					return flag;
				}
				if (cNKMEventAttack.m_AttackUnitCountOnly && cNKMDamageInst.m_AttackCount >= cNKMEventAttack.m_AttackUnitCount)
				{
					return flag;
				}
				NKMUnit nkmunit = unit.GetTargetUnit(true);
				flag = this.HitCheck(cNKMDamageInst, cNKMEventAttack, unit, nkmunit, num, bSplashHit);
				if (flag && nkmunit != null)
				{
					if (nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_DEFENDER)
					{
						num += NKMUnitStatManager.m_fDEFENDER_PROTECT_RATE;
						float statFinal = nkmunit.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_DEFENDER_PROTECT_RATE);
						num += NKMUnitStatManager.m_fDEFENDER_PROTECT_RATE * statFinal;
					}
					if (num > NKMUnitStatManager.m_fDEFENDER_PROTECT_RATE_MAX)
					{
						num = NKMUnitStatManager.m_fDEFENDER_PROTECT_RATE_MAX;
					}
				}
			}
			List<NKMUnit> sortUnitListByNearDist = unit.GetSortUnitListByNearDist();
			if (sortUnitListByNearDist.Count > 0)
			{
				int num2 = 0;
				while (num2 < sortUnitListByNearDist.Count && (cNKMEventAttack.m_AttackUnitCount != 1 || cNKMDamageInst.m_AttackCount < 1) && (!cNKMEventAttack.m_AttackUnitCountOnly || cNKMDamageInst.m_AttackCount < cNKMEventAttack.m_AttackUnitCount))
				{
					NKMUnit nkmunit = sortUnitListByNearDist[num2];
					if (nkmunit != null)
					{
						if (cNKMDamageInst.m_ReActResult == NKM_REACT_TYPE.NRT_REVENGE)
						{
							break;
						}
						if (this.HitCheck(cNKMDamageInst, cNKMEventAttack, unit, nkmunit, num, bSplashHit))
						{
							if (nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_DEFENDER)
							{
								num += NKMUnitStatManager.m_fDEFENDER_PROTECT_RATE;
								float statFinal2 = nkmunit.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_DEFENDER_PROTECT_RATE);
								num += NKMUnitStatManager.m_fDEFENDER_PROTECT_RATE * statFinal2;
							}
							if (num > NKMUnitStatManager.m_fDEFENDER_PROTECT_RATE_MAX)
							{
								num = NKMUnitStatManager.m_fDEFENDER_PROTECT_RATE_MAX;
							}
							flag = true;
						}
					}
					num2++;
				}
			}
			return flag;
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x0007C0C0 File Offset: 0x0007A2C0
		protected bool EffectToUnit(NKMDamageInst cNKMDamageInst, NKMEventAttack cNKMEventAttack, bool bDieAttack = false)
		{
			bool flag = false;
			NKMDamageEffect damageEffect = this.GetDamageEffect(cNKMDamageInst.m_AttackerEffectUID);
			if (damageEffect == null)
			{
				return false;
			}
			NKMUnit masterUnit = damageEffect.GetMasterUnit();
			if (damageEffect.IsEnd())
			{
				return false;
			}
			if (damageEffect.GetDEData() == null)
			{
				return false;
			}
			if (damageEffect.GetTemplet() == null)
			{
				return false;
			}
			if (masterUnit == null)
			{
				return false;
			}
			cNKMDamageInst.m_EventAttack = cNKMEventAttack;
			float num = 0f;
			bool bSplashHit = (cNKMEventAttack.m_AttackUnitCount != 1 || !cNKMEventAttack.m_AttackUnitCountOnly) && damageEffect.GetTemplet().m_DamageCountMax != 1;
			NKMUnit targetUnit = damageEffect.GetTargetUnit();
			if (cNKMEventAttack.m_AttackUnitCount == 1 && cNKMEventAttack.m_AttackTargetUnit && targetUnit != null && targetUnit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_PLAY)
			{
				NKMUnit nkmunit = targetUnit;
				bool flag2 = this.HitCheck(cNKMDamageInst, cNKMEventAttack, damageEffect, masterUnit, nkmunit, num, bSplashHit);
				if (flag2)
				{
					if (nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_DEFENDER)
					{
						num += NKMUnitStatManager.m_fDEFENDER_PROTECT_RATE;
						float statFinal = nkmunit.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_DEFENDER_PROTECT_RATE);
						num += NKMUnitStatManager.m_fDEFENDER_PROTECT_RATE * statFinal;
					}
					if (num > NKMUnitStatManager.m_fDEFENDER_PROTECT_RATE_MAX)
					{
						num = NKMUnitStatManager.m_fDEFENDER_PROTECT_RATE_MAX;
					}
				}
				return flag2;
			}
			if (cNKMEventAttack.m_AttackTargetUnit)
			{
				NKMUnit nkmunit = damageEffect.GetTargetUnit();
				if (nkmunit != null)
				{
					flag = this.HitCheck(cNKMDamageInst, cNKMEventAttack, damageEffect, masterUnit, nkmunit, num, bSplashHit);
					if (flag)
					{
						if (nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_DEFENDER)
						{
							num += NKMUnitStatManager.m_fDEFENDER_PROTECT_RATE;
							float statFinal2 = nkmunit.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_DEFENDER_PROTECT_RATE);
							num += NKMUnitStatManager.m_fDEFENDER_PROTECT_RATE * statFinal2;
						}
						if (num > NKMUnitStatManager.m_fDEFENDER_PROTECT_RATE_MAX)
						{
							num = NKMUnitStatManager.m_fDEFENDER_PROTECT_RATE_MAX;
						}
						flag = true;
					}
				}
			}
			if (!bDieAttack && damageEffect.GetDEData().m_DamageCountNow >= damageEffect.GetTemplet().m_DamageCountMax)
			{
				return flag;
			}
			List<NKMUnit> sortUnitListByNearDist = damageEffect.GetSortUnitListByNearDist();
			if (sortUnitListByNearDist.Count > 0)
			{
				int num2 = 0;
				while (num2 < sortUnitListByNearDist.Count && (cNKMEventAttack.m_AttackUnitCount != 1 || cNKMDamageInst.m_AttackCount < 1) && (!cNKMEventAttack.m_AttackUnitCountOnly || cNKMDamageInst.m_AttackCount < cNKMEventAttack.m_AttackUnitCount) && !damageEffect.IsEnd() && cNKMDamageInst.m_ReActResult != NKM_REACT_TYPE.NRT_REVENGE && (bDieAttack || damageEffect.GetDEData().m_DamageCountNow < damageEffect.GetTemplet().m_DamageCountMax))
				{
					NKMUnit nkmunit = sortUnitListByNearDist[num2];
					if (nkmunit != null && this.HitCheck(cNKMDamageInst, cNKMEventAttack, damageEffect, masterUnit, nkmunit, num, bSplashHit))
					{
						if (nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE == NKM_UNIT_ROLE_TYPE.NURT_DEFENDER)
						{
							num += NKMUnitStatManager.m_fDEFENDER_PROTECT_RATE;
							float statFinal3 = nkmunit.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_DEFENDER_PROTECT_RATE);
							num += NKMUnitStatManager.m_fDEFENDER_PROTECT_RATE * statFinal3;
						}
						if (num > NKMUnitStatManager.m_fDEFENDER_PROTECT_RATE_MAX)
						{
							num = NKMUnitStatManager.m_fDEFENDER_PROTECT_RATE_MAX;
						}
						flag = true;
					}
					num2++;
				}
			}
			return flag;
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x0007C368 File Offset: 0x0007A568
		protected bool CollisionCheck(float fAtkBeforeX, float fAtkNowX, float fAtkSizeX, bool bAtkRight, NKMEventAttack cNKMEventAttack, float fAddRange, float fDefNowX, float fDefSizeX)
		{
			if (fAtkBeforeX.IsNearlyEqual(-1f, 1E-05f))
			{
				fAtkBeforeX = fAtkNowX;
			}
			float num;
			float num2;
			if (fAtkBeforeX <= fAtkNowX)
			{
				num = fAtkBeforeX;
				num2 = fAtkNowX;
			}
			else
			{
				num = fAtkNowX;
				num2 = fAtkBeforeX;
			}
			if (bAtkRight)
			{
				if (cNKMEventAttack.m_fRangeMax + fAddRange > 0f)
				{
					num2 += fAtkSizeX * 0.5f;
				}
				if (cNKMEventAttack.m_fRangeMin < 0f)
				{
					num -= fAtkSizeX * 0.5f;
				}
				num += cNKMEventAttack.m_fRangeMin;
				num2 += cNKMEventAttack.m_fRangeMax + fAddRange;
			}
			else
			{
				if (cNKMEventAttack.m_fRangeMax + fAddRange > 0f)
				{
					num -= fAtkSizeX * 0.5f;
				}
				if (cNKMEventAttack.m_fRangeMin < 0f)
				{
					num2 += fAtkSizeX * 0.5f;
				}
				num -= cNKMEventAttack.m_fRangeMax + fAddRange;
				num2 -= cNKMEventAttack.m_fRangeMin;
			}
			if (fDefNowX >= num && fDefNowX <= num2)
			{
				return true;
			}
			float num3 = num - fDefNowX;
			if (num3 >= 0f && num3 < fDefSizeX * 0.5f)
			{
				return true;
			}
			float num4 = fDefNowX - num2;
			return num4 >= 0f && num4 < fDefSizeX * 0.5f;
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x0007C480 File Offset: 0x0007A680
		protected bool HitCheck(NKMDamageInst cNKMDamageInst, NKMEventAttack cNKMEventAttack, NKMUnit pAttacker, NKMUnit pDefender, float fDefenderDamageReduce, bool bSplashHit)
		{
			if (pAttacker == null || pDefender == null)
			{
				return false;
			}
			if (!pDefender.CheckEventCondition(cNKMEventAttack.m_ConditionTarget))
			{
				return false;
			}
			if (pDefender.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_PLAY)
			{
				return false;
			}
			switch (cNKMEventAttack.m_NKM_DAMAGE_TARGET_TYPE)
			{
			case NKM_DAMAGE_TARGET_TYPE.NDTT_ENEMY:
				if (!this.IsEnemy(pAttacker.GetUnitDataGame().m_NKM_TEAM_TYPE, pDefender.GetUnitDataGame().m_NKM_TEAM_TYPE))
				{
					return false;
				}
				break;
			case NKM_DAMAGE_TARGET_TYPE.NDTT_MY_TEAM:
				if (this.IsEnemy(pAttacker.GetUnitDataGame().m_NKM_TEAM_TYPE, pDefender.GetUnitDataGame().m_NKM_TEAM_TYPE))
				{
					return false;
				}
				break;
			case NKM_DAMAGE_TARGET_TYPE.NDTT_MY_TEAM_NOT_SELF:
				if (this.IsEnemy(pAttacker.GetUnitDataGame().m_NKM_TEAM_TYPE, pDefender.GetUnitDataGame().m_NKM_TEAM_TYPE))
				{
					return false;
				}
				if (pAttacker == pDefender)
				{
					return false;
				}
				break;
			case NKM_DAMAGE_TARGET_TYPE.NDTT_ALL_NOT_SELF:
				if (pAttacker == pDefender)
				{
					return false;
				}
				break;
			case NKM_DAMAGE_TARGET_TYPE.NDTT_SELF_ONLY:
				if (pAttacker != pDefender)
				{
					return false;
				}
				break;
			}
			for (int i = 0; i < cNKMDamageInst.m_listHitUnit.Count; i++)
			{
				if (cNKMDamageInst.m_listHitUnit[i] == pDefender.GetUnitDataGame().m_GameUnitUID)
				{
					return false;
				}
			}
			NKMUnitTempletBase unitTempletBase = pDefender.GetUnitTemplet().m_UnitTempletBase;
			if (!unitTempletBase.IsAllowUnitStyleType(cNKMEventAttack.m_listAllowStyle, cNKMEventAttack.m_listIgnoreStyle))
			{
				return false;
			}
			if (!unitTempletBase.IsAllowUnitRoleType(cNKMEventAttack.m_listAllowRole, cNKMEventAttack.m_listIgnoreRole))
			{
				return false;
			}
			int respawnCost = this.GetRespawnCost(pDefender.GetUnitTemplet().m_StatTemplet, false, pDefender.GetUnitDataGame().m_NKM_TEAM_TYPE);
			if (cNKMEventAttack.m_TargetCostLess != -1 && cNKMEventAttack.m_TargetCostLess <= respawnCost)
			{
				return false;
			}
			if (cNKMEventAttack.m_TargetCostOver != -1 && cNKMEventAttack.m_TargetCostOver >= respawnCost)
			{
				return false;
			}
			bool flag = false;
			if (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
			{
				flag = true;
			}
			if (pDefender.IsBoss())
			{
				flag = true;
			}
			if (!flag)
			{
				if (!cNKMEventAttack.m_bHitAir && pDefender.IsAirUnit())
				{
					return false;
				}
				if (!cNKMEventAttack.m_bHitLand && !pDefender.IsAirUnit())
				{
					return false;
				}
			}
			if (cNKMEventAttack.m_bHitSummonOnly && !pDefender.IsSummonUnit())
			{
				return false;
			}
			if (!cNKMEventAttack.m_bHitAwakenUnit && unitTempletBase.m_bAwaken)
			{
				return false;
			}
			if (!cNKMEventAttack.m_bHitNormalUnit && !unitTempletBase.m_bAwaken)
			{
				return false;
			}
			if (!cNKMEventAttack.m_bHitBossUnit && flag)
			{
				return false;
			}
			bool flag2 = false;
			if (cNKMDamageInst.m_EventAttack.m_AttackUnitCount + (int)pAttacker.GetUnitFrameData().m_AddAttackUnitCount <= cNKMDamageInst.m_AttackCount)
			{
				flag2 = true;
			}
			if (!this.CollisionCheck(pAttacker.GetUnitFrameData().m_PosXBefore, pAttacker.GetUnitSyncData().m_PosX, pAttacker.GetUnitTemplet().m_UnitSizeX, pAttacker.GetUnitSyncData().m_bRight, cNKMEventAttack, pAttacker.GetUnitFrameData().m_fAddAttackRange, pDefender.GetUnitSyncData().m_PosX, pDefender.GetUnitTemplet().m_UnitSizeX))
			{
				return false;
			}
			cNKMDamageInst.m_DefenderUID = pDefender.GetUnitDataGame().m_GameUnitUID;
			cNKMDamageInst.m_ReActResult = cNKMDamageInst.m_Templet.m_ReActType;
			cNKMDamageInst.m_AttackerPosX = pAttacker.GetUnitSyncData().m_PosX;
			cNKMDamageInst.m_AttackerPosZ = pAttacker.GetUnitSyncData().m_PosZ;
			cNKMDamageInst.m_AttackerPosJumpY = pAttacker.GetUnitSyncData().m_JumpYPos;
			cNKMDamageInst.m_bAttackerRight = pAttacker.GetUnitSyncData().m_bRight;
			cNKMDamageInst.m_bAttackerAwaken = pAttacker.GetUnitTemplet().m_UnitTempletBase.m_bAwaken;
			cNKMDamageInst.m_AttackerAddAttackUnitCount = pAttacker.GetUnitFrameData().m_AddAttackUnitCount;
			cNKMDamageInst.m_bEvade = NKMUnitStatManager.GetEvade(pAttacker, pDefender, false, pDefender.GetHPRate(), cNKMEventAttack);
			pDefender.DamageReact(cNKMDamageInst, false);
			if (cNKMDamageInst.m_ReActResult == NKM_REACT_TYPE.NRT_NO)
			{
				return false;
			}
			bool result = true;
			bool flag3 = true;
			for (int j = 0; j < cNKMDamageInst.m_listHitUnit.Count; j++)
			{
				if (cNKMDamageInst.m_listHitUnit[j] == cNKMDamageInst.m_DefenderUID)
				{
					flag3 = false;
					break;
				}
			}
			if (flag3)
			{
				cNKMDamageInst.m_listHitUnit.Add(cNKMDamageInst.m_DefenderUID);
				cNKMDamageInst.m_AttackCount++;
				cNKMDamageInst.m_AttackCount += (int)(pDefender.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_ATTACK_COUNT_REDUCE) + 0.1f);
			}
			pAttacker.AttackResult(cNKMDamageInst);
			if (cNKMDamageInst.m_Templet.m_ReAttackCount > 1)
			{
				cNKMDamageInst.m_bReAttackCountOver = flag2;
				cNKMDamageInst.m_ReAttackCount = 1;
				cNKMDamageInst.m_fReAttackGap = cNKMDamageInst.m_Templet.m_fReAttackGap;
				NKMDamageInst nkmdamageInst = (NKMDamageInst)this.m_ObjectPool.OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKMDamageInst, "", "", false);
				nkmdamageInst.DeepCopyFromSource(cNKMDamageInst);
				this.m_linklistReAttack.AddLast(nkmdamageInst);
			}
			if (cNKMDamageInst.m_ReActResult == NKM_REACT_TYPE.NRT_INVINCIBLE)
			{
				return result;
			}
			if (this.m_NKM_GAME_CLASS_TYPE == NKM_GAME_CLASS_TYPE.NGCT_GAME_SERVER || this.m_NKM_GAME_CLASS_TYPE == NKM_GAME_CLASS_TYPE.NGCT_GAME_SERVER_LOCAL)
			{
				bool bLongRange = Math.Abs(pDefender.GetUnitSyncData().m_PosX - pAttacker.GetUnitSyncData().m_PosX) > NKMUnitStatManager.m_fLONG_RANGE;
				NKM_DAMAGE_RESULT_TYPE nkm_DAMAGE_RESULT_TYPE = NKM_DAMAGE_RESULT_TYPE.NDRT_NORMAL;
				bool bInstaKill = false;
				float num;
				if (this.GetGameData().m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PRACTICE && this.GetGameRuntimeData().m_bPracticeFixedDamage)
				{
					num = NKMUnitStatManager.GetAttackFactorDamage(cNKMDamageInst.m_Templet.m_DamageTempletBase, cNKMDamageInst.m_AttackerUnitSkillTemplet, flag);
				}
				else if (!flag2 && pDefender.WillInstaKilled(cNKMDamageInst.m_Templet))
				{
					num = pDefender.GetNowHP() * 2f;
					pDefender.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_INSTA_KILL);
					bInstaKill = true;
				}
				else
				{
					num = NKMUnitStatManager.GetFinalDamage(this.GetGameData().IsPVP(), pAttacker.GetUnitFrameData().m_StatData, pDefender.GetUnitFrameData().m_StatData, pAttacker.GetUnitData(), pAttacker, pDefender, cNKMDamageInst.m_Templet, cNKMDamageInst.m_AttackerUnitSkillTemplet, flag2, false, cNKMDamageInst.m_bEvade, out nkm_DAMAGE_RESULT_TYPE, fDefenderDamageReduce, bLongRange, flag, pAttacker.GetHPRate(), cNKMEventAttack.m_bTrueDamage, bSplashHit, cNKMEventAttack.m_bForceCritical, cNKMEventAttack.m_bNoCritical);
				}
				if (cNKMDamageInst.m_ReActResult == NKM_REACT_TYPE.NRT_REVENGE)
				{
					num *= 0.1f;
				}
				num = pDefender.GetModifiedDMGAfterEventDEF(pAttacker.GetUnitSyncData().m_PosX, num);
				pDefender.SetHitFeedBack();
				if (nkm_DAMAGE_RESULT_TYPE == NKM_DAMAGE_RESULT_TYPE.NDRT_CRITICAL)
				{
					pAttacker.SetHitCriticalFeedBack();
				}
				if (cNKMDamageInst.m_Templet.m_DamageTempletBase != null && (!cNKMDamageInst.m_Templet.m_DamageTempletBase.m_fAtkFactor.IsNearlyZero(1E-05f) || !cNKMDamageInst.m_Templet.m_DamageTempletBase.m_fAtkFactorPVP.IsNearlyZero(1E-05f)) && cNKMDamageInst.m_bEvade)
				{
					pDefender.SetHitEvadeFeedBack();
				}
				bool flag4;
				if (cNKMDamageInst.m_ReActResult != NKM_REACT_TYPE.NRT_REVENGE && pDefender.GetUnitSyncData().GetHP() > pDefender.GetUnitFrameData().m_fDamageThisFrame && pDefender.GetUnitSyncData().GetHP() <= pDefender.GetUnitFrameData().m_fDamageThisFrame + num && pDefender.GetPhaseDamageLimit(out flag4) <= 0f && !pDefender.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_IMMORTAL))
				{
					pAttacker.Kill(cNKMDamageInst.m_Templet.m_NKMKillFeedBack, pDefender.GetUnitDataGame().m_GameUnitUID);
				}
				pDefender.AddDamage(flag2, num, nkm_DAMAGE_RESULT_TYPE, pAttacker.GetUnitDataGame().m_GameUnitUID, pAttacker.GetUnitDataGame().m_NKM_TEAM_TYPE, false, false, bInstaKill);
				if (cNKMDamageInst.m_ReActResult == NKM_REACT_TYPE.NRT_REVENGE)
				{
					if (pDefender.GetUnitFrameData().m_fDamageThisFrame >= pDefender.GetUnitSyncData().GetHP())
					{
						pDefender.GetUnitFrameData().m_fDamageThisFrame = pDefender.GetUnitSyncData().GetHP() - 1f;
					}
				}
				else
				{
					if (cNKMDamageInst.m_Templet.CanApplyStun(pDefender.GetUnitTemplet().m_UnitTempletBase))
					{
						pDefender.ApplyStatusTime(NKM_UNIT_STATUS_EFFECT.NUSE_STUN, cNKMDamageInst.m_Templet.m_fStunTime, pAttacker, false, false, false);
					}
					if (cNKMDamageInst.m_Templet.CanApplyCooltimeDamage(pDefender.GetUnitTemplet().m_UnitTempletBase))
					{
						pDefender.AddDamage(flag2, cNKMDamageInst.m_Templet.m_fCoolTimeDamage, NKM_DAMAGE_RESULT_TYPE.NDRT_COOL_TIME, pAttacker.GetUnitDataGame().m_GameUnitUID, pAttacker.GetUnitDataGame().m_NKM_TEAM_TYPE, false, false, false);
					}
					if (cNKMDamageInst.m_Templet.CanApplyStatus(pDefender.GetUnitTemplet().m_UnitTempletBase))
					{
						pDefender.ApplyStatusTime(cNKMDamageInst.m_Templet.m_ApplyStatusEffect, cNKMDamageInst.m_Templet.m_fApplyStatusTime, pAttacker, false, false, false);
					}
				}
				this.ProcessHitBuff(cNKMDamageInst, pAttacker, pDefender);
				if (cNKMEventAttack.m_fGetAgroTime > 0f)
				{
					pAttacker.SetAgro(pDefender, cNKMEventAttack.m_fGetAgroTime);
				}
			}
			this.ProcessHitEvent(cNKMDamageInst, pAttacker, pDefender, false);
			return result;
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x0007CC38 File Offset: 0x0007AE38
		protected bool HitCheck(NKMDamageInst cNKMDamageInst, NKMEventAttack cNKMEventAttack, NKMDamageEffect pAttackerEffect, NKMUnit pAttackerUnit, NKMUnit pDefender, float fDefenderDamageReduce, bool bSplashHit)
		{
			if (pAttackerEffect == null || pDefender == null)
			{
				return false;
			}
			if (pAttackerEffect.IsEnd())
			{
				return false;
			}
			if (pDefender.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_PLAY)
			{
				return false;
			}
			switch (cNKMEventAttack.m_NKM_DAMAGE_TARGET_TYPE)
			{
			case NKM_DAMAGE_TARGET_TYPE.NDTT_ENEMY:
				if (!this.IsEnemy(pAttackerEffect.GetDEData().m_NKM_TEAM_TYPE, pDefender.GetUnitDataGame().m_NKM_TEAM_TYPE))
				{
					return false;
				}
				break;
			case NKM_DAMAGE_TARGET_TYPE.NDTT_MY_TEAM:
				if (this.IsEnemy(pAttackerEffect.GetDEData().m_NKM_TEAM_TYPE, pDefender.GetUnitDataGame().m_NKM_TEAM_TYPE))
				{
					return false;
				}
				break;
			case NKM_DAMAGE_TARGET_TYPE.NDTT_MY_TEAM_NOT_SELF:
				if (this.IsEnemy(pAttackerEffect.GetDEData().m_NKM_TEAM_TYPE, pDefender.GetUnitDataGame().m_NKM_TEAM_TYPE))
				{
					return false;
				}
				if (pAttackerEffect.GetDEData().m_MasterGameUnitUID == pDefender.GetUnitDataGame().m_GameUnitUID)
				{
					return false;
				}
				break;
			case NKM_DAMAGE_TARGET_TYPE.NDTT_ALL_NOT_SELF:
				if (pAttackerEffect.GetDEData().m_MasterGameUnitUID == pDefender.GetUnitDataGame().m_GameUnitUID)
				{
					return false;
				}
				break;
			case NKM_DAMAGE_TARGET_TYPE.NDTT_SELF_ONLY:
				if (pAttackerEffect.GetDEData().m_MasterGameUnitUID != pDefender.GetUnitDataGame().m_GameUnitUID)
				{
					return false;
				}
				break;
			}
			if (pDefender.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DYING || pDefender.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DIE)
			{
				return false;
			}
			for (int i = 0; i < cNKMDamageInst.m_listHitUnit.Count; i++)
			{
				if (cNKMDamageInst.m_listHitUnit[i] == pDefender.GetUnitDataGame().m_GameUnitUID)
				{
					return false;
				}
			}
			NKMUnitTempletBase unitTempletBase = pDefender.GetUnitTemplet().m_UnitTempletBase;
			if (!unitTempletBase.IsAllowUnitStyleType(cNKMEventAttack.m_listAllowStyle, cNKMEventAttack.m_listIgnoreStyle))
			{
				return false;
			}
			if (!unitTempletBase.IsAllowUnitRoleType(cNKMEventAttack.m_listAllowRole, cNKMEventAttack.m_listIgnoreRole))
			{
				return false;
			}
			bool flag = false;
			if (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
			{
				flag = true;
			}
			if (pDefender.IsBoss())
			{
				flag = true;
			}
			if (!flag)
			{
				if (!cNKMEventAttack.m_bHitAir && pDefender.IsAirUnit())
				{
					return false;
				}
				if (!cNKMEventAttack.m_bHitLand && !pDefender.IsAirUnit())
				{
					return false;
				}
			}
			if (cNKMEventAttack.m_bHitSummonOnly && !pDefender.IsSummonUnit())
			{
				return false;
			}
			if (!cNKMEventAttack.m_bHitAwakenUnit && unitTempletBase.m_bAwaken)
			{
				return false;
			}
			if (!cNKMEventAttack.m_bHitNormalUnit && !unitTempletBase.m_bAwaken)
			{
				return false;
			}
			if (!cNKMEventAttack.m_bHitBossUnit && flag)
			{
				return false;
			}
			byte b;
			if (pAttackerEffect.GetMasterUnit() != null)
			{
				b = pAttackerEffect.GetMasterUnit().GetUnitFrameData().m_AddAttackUnitCount;
			}
			else
			{
				b = 0;
			}
			bool flag2 = false;
			if (cNKMDamageInst.m_EventAttack.m_AttackUnitCount + (int)b <= cNKMDamageInst.m_AttackCount)
			{
				flag2 = true;
			}
			if (!pDefender.CheckEventCondition(cNKMEventAttack.m_ConditionTarget))
			{
				return false;
			}
			if (!this.CollisionCheck(pAttackerEffect.GetDEData().m_PosXBefore, pAttackerEffect.GetDEData().m_PosX, pAttackerEffect.GetTemplet().m_fEffectSize, pAttackerEffect.GetDEData().m_bRight, cNKMEventAttack, 0f, pDefender.GetUnitSyncData().m_PosX, pDefender.GetUnitTemplet().m_UnitSizeX))
			{
				return false;
			}
			cNKMDamageInst.m_DefenderUID = pDefender.GetUnitDataGame().m_GameUnitUID;
			cNKMDamageInst.m_ReActResult = cNKMDamageInst.m_Templet.m_ReActType;
			if (!pAttackerEffect.GetTemplet().m_bDamageSpeedDependMaster || pAttackerUnit == null)
			{
				cNKMDamageInst.m_AttackerPosX = pAttackerEffect.GetDEData().m_PosX;
				cNKMDamageInst.m_AttackerPosZ = pAttackerEffect.GetDEData().m_PosZ;
				cNKMDamageInst.m_AttackerPosJumpY = pAttackerEffect.GetDEData().m_JumpYPos;
				cNKMDamageInst.m_bAttackerRight = pAttackerEffect.GetDEData().m_bRight;
				if (pAttackerEffect.GetMasterUnit() != null)
				{
					cNKMDamageInst.m_bAttackerAwaken = pAttackerEffect.GetMasterUnit().GetUnitTemplet().m_UnitTempletBase.m_bAwaken;
					cNKMDamageInst.m_AttackerAddAttackUnitCount = pAttackerEffect.GetMasterUnit().GetUnitFrameData().m_AddAttackUnitCount;
				}
				else
				{
					cNKMDamageInst.m_AttackerAddAttackUnitCount = 0;
				}
			}
			else
			{
				cNKMDamageInst.m_AttackerPosX = pAttackerUnit.GetUnitSyncData().m_PosX;
				cNKMDamageInst.m_AttackerPosZ = pAttackerUnit.GetUnitSyncData().m_PosZ;
				cNKMDamageInst.m_AttackerPosJumpY = pAttackerUnit.GetUnitSyncData().m_JumpYPos;
				cNKMDamageInst.m_bAttackerRight = pAttackerUnit.GetUnitSyncData().m_bRight;
				cNKMDamageInst.m_bAttackerAwaken = pAttackerUnit.GetUnitTemplet().m_UnitTempletBase.m_bAwaken;
				cNKMDamageInst.m_bEvade = NKMUnitStatManager.GetEvade(pAttackerUnit, pDefender, false, pDefender.GetHPRate(), cNKMEventAttack);
				cNKMDamageInst.m_AttackerAddAttackUnitCount = pAttackerUnit.GetUnitFrameData().m_AddAttackUnitCount;
			}
			if (pAttackerUnit == null)
			{
				cNKMDamageInst.m_bEvade = false;
			}
			else
			{
				cNKMDamageInst.m_bEvade = NKMUnitStatManager.GetEvade(pAttackerUnit, pDefender, false, pDefender.GetHPRate(), cNKMEventAttack);
			}
			pDefender.DamageReact(cNKMDamageInst, false);
			if (cNKMDamageInst.m_ReActResult == NKM_REACT_TYPE.NRT_NO)
			{
				return false;
			}
			bool result = true;
			bool flag3 = true;
			for (int j = 0; j < cNKMDamageInst.m_listHitUnit.Count; j++)
			{
				if (cNKMDamageInst.m_listHitUnit[j] == cNKMDamageInst.m_DefenderUID)
				{
					flag3 = false;
					break;
				}
			}
			if (flag3)
			{
				cNKMDamageInst.m_listHitUnit.Add(cNKMDamageInst.m_DefenderUID);
				cNKMDamageInst.m_AttackCount++;
				cNKMDamageInst.m_AttackCount += (int)(pDefender.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_ATTACK_COUNT_REDUCE) + 0.1f);
			}
			pAttackerEffect.AttackResult(cNKMDamageInst, pDefender);
			if (cNKMDamageInst.m_Templet.m_ReAttackCount > 1)
			{
				cNKMDamageInst.m_bReAttackCountOver = flag2;
				cNKMDamageInst.m_ReAttackCount = 1;
				cNKMDamageInst.m_fReAttackGap = cNKMDamageInst.m_Templet.m_fReAttackGap;
				NKMDamageInst nkmdamageInst = (NKMDamageInst)this.m_ObjectPool.OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKMDamageInst, "", "", false);
				nkmdamageInst.DeepCopyFromSource(cNKMDamageInst);
				this.m_linklistReAttack.AddLast(nkmdamageInst);
			}
			if (cNKMDamageInst.m_ReActResult == NKM_REACT_TYPE.NRT_INVINCIBLE)
			{
				return result;
			}
			if (this.m_NKM_GAME_CLASS_TYPE == NKM_GAME_CLASS_TYPE.NGCT_GAME_SERVER || this.m_NKM_GAME_CLASS_TYPE == NKM_GAME_CLASS_TYPE.NGCT_GAME_SERVER_LOCAL)
			{
				float fAtkHPRate = 1f;
				bool bLongRange = false;
				if (pAttackerUnit != null)
				{
					if (Math.Abs(pDefender.GetUnitSyncData().m_PosX - pAttackerUnit.GetUnitSyncData().m_PosX) > NKMUnitStatManager.m_fLONG_RANGE)
					{
						bLongRange = true;
					}
					fAtkHPRate = pAttackerUnit.GetHPRate();
				}
				NKM_DAMAGE_RESULT_TYPE nkm_DAMAGE_RESULT_TYPE = NKM_DAMAGE_RESULT_TYPE.NDRT_NORMAL;
				bool bInstaKill = false;
				float num;
				if (this.GetGameData().m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PRACTICE && this.GetGameRuntimeData().m_bPracticeFixedDamage)
				{
					num = NKMUnitStatManager.GetAttackFactorDamage(cNKMDamageInst.m_Templet.m_DamageTempletBase, cNKMDamageInst.m_AttackerUnitSkillTemplet, flag);
				}
				else if (!flag2 && pDefender.WillInstaKilled(cNKMDamageInst.m_Templet))
				{
					num = pDefender.GetNowHP() * 2f;
					pDefender.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_INSTA_KILL);
					bInstaKill = true;
				}
				else
				{
					num = NKMUnitStatManager.GetFinalDamage(this.GetGameData().IsPVP(), pAttackerEffect.GetDEData().m_StatData, pDefender.GetUnitFrameData().m_StatData, pAttackerEffect.GetDEData().m_UnitData, pAttackerEffect.GetMasterUnit(), pDefender, cNKMDamageInst.m_Templet, cNKMDamageInst.m_AttackerUnitSkillTemplet, flag2, false, cNKMDamageInst.m_bEvade, out nkm_DAMAGE_RESULT_TYPE, fDefenderDamageReduce, bLongRange, flag, fAtkHPRate, cNKMEventAttack.m_bTrueDamage, bSplashHit, cNKMEventAttack.m_bForceCritical, cNKMEventAttack.m_bNoCritical);
				}
				if (cNKMDamageInst.m_ReActResult == NKM_REACT_TYPE.NRT_REVENGE)
				{
					num *= 0.1f;
				}
				if (pAttackerEffect.GetTemplet().m_bDamageSpeedDependMaster && pAttackerUnit != null)
				{
					num = pDefender.GetModifiedDMGAfterEventDEF(pAttackerUnit.GetUnitSyncData().m_PosX, num);
				}
				else
				{
					num = pDefender.GetModifiedDMGAfterEventDEF(pAttackerEffect.GetDEData().m_PosX, num);
				}
				pDefender.SetHitFeedBack();
				if (nkm_DAMAGE_RESULT_TYPE == NKM_DAMAGE_RESULT_TYPE.NDRT_CRITICAL && pAttackerUnit != null)
				{
					pAttackerUnit.SetHitCriticalFeedBack();
				}
				if (cNKMDamageInst.m_Templet.m_DamageTempletBase != null && (!cNKMDamageInst.m_Templet.m_DamageTempletBase.m_fAtkFactor.IsNearlyZero(1E-05f) || !cNKMDamageInst.m_Templet.m_DamageTempletBase.m_fAtkFactorPVP.IsNearlyZero(1E-05f)) && cNKMDamageInst.m_bEvade)
				{
					pDefender.SetHitEvadeFeedBack();
				}
				bool flag4;
				if (cNKMDamageInst.m_ReActResult != NKM_REACT_TYPE.NRT_REVENGE && pDefender.GetUnitSyncData().GetHP() > pDefender.GetUnitFrameData().m_fDamageThisFrame && pDefender.GetUnitSyncData().GetHP() <= pDefender.GetUnitFrameData().m_fDamageThisFrame + num && pDefender.GetPhaseDamageLimit(out flag4) <= 0f && !pDefender.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_IMMORTAL) && pAttackerUnit != null)
				{
					pAttackerUnit.Kill(cNKMDamageInst.m_Templet.m_NKMKillFeedBack, pDefender.GetUnitDataGame().m_GameUnitUID);
				}
				pDefender.AddDamage(flag2, num, nkm_DAMAGE_RESULT_TYPE, pAttackerEffect.GetDEData().m_MasterGameUnitUID, pAttackerEffect.GetDEData().m_NKM_TEAM_TYPE, false, false, bInstaKill);
				if (cNKMDamageInst.m_ReActResult == NKM_REACT_TYPE.NRT_REVENGE)
				{
					if (pDefender.GetUnitFrameData().m_fDamageThisFrame >= pDefender.GetUnitSyncData().GetHP())
					{
						pDefender.GetUnitFrameData().m_fDamageThisFrame = pDefender.GetUnitSyncData().GetHP() - 1f;
					}
				}
				else
				{
					if (cNKMDamageInst.m_Templet.CanApplyStun(pDefender.GetUnitTemplet().m_UnitTempletBase))
					{
						pDefender.ApplyStatusTime(NKM_UNIT_STATUS_EFFECT.NUSE_STUN, cNKMDamageInst.m_Templet.m_fStunTime, pAttackerUnit, false, false, false);
					}
					if (cNKMDamageInst.m_Templet.CanApplyCooltimeDamage(pDefender.GetUnitTemplet().m_UnitTempletBase))
					{
						pDefender.AddDamage(flag2, cNKMDamageInst.m_Templet.m_fCoolTimeDamage, NKM_DAMAGE_RESULT_TYPE.NDRT_COOL_TIME, pAttackerEffect.GetDEData().m_MasterGameUnitUID, pAttackerEffect.GetDEData().m_NKM_TEAM_TYPE, false, false, false);
					}
					if (cNKMDamageInst.m_Templet.CanApplyStatus(pDefender.GetUnitTemplet().m_UnitTempletBase))
					{
						pDefender.ApplyStatusTime(cNKMDamageInst.m_Templet.m_ApplyStatusEffect, cNKMDamageInst.m_Templet.m_fApplyStatusTime, pAttackerUnit, false, false, false);
					}
				}
				if (pAttackerUnit != null)
				{
					this.ProcessHitBuff(cNKMDamageInst, pAttackerUnit, pDefender);
					if (cNKMEventAttack.m_fGetAgroTime > 0f)
					{
						pAttackerUnit.SetAgro(pDefender, cNKMEventAttack.m_fGetAgroTime);
					}
				}
			}
			this.ProcessHitEvent(cNKMDamageInst, pAttackerUnit, pDefender, true);
			return result;
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x0007D550 File Offset: 0x0007B750
		public bool ProcessDamageTemplet(NKMDamageTemplet cNKMDamageTemplet, NKMUnit pAttacker, NKMUnit pDefender, bool bUseAttackerStat = true, bool buffDamage = false, NKMUnitSkillTemplet attackerSkillTemplet = null, NKMDamageAttribute damageAttribute = null)
		{
			if (cNKMDamageTemplet == null)
			{
				return false;
			}
			if (pAttacker == null)
			{
				return false;
			}
			if (pDefender == null)
			{
				return false;
			}
			NKMDamageInst nkmdamageInst = (NKMDamageInst)this.m_ObjectPool.OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKMDamageInst, "", "", false);
			nkmdamageInst.m_Templet = cNKMDamageTemplet;
			nkmdamageInst.m_DefenderUID = pDefender.GetUnitDataGame().m_GameUnitUID;
			nkmdamageInst.m_ReActResult = nkmdamageInst.m_Templet.m_ReActType;
			nkmdamageInst.m_AttackerPosX = pAttacker.GetUnitSyncData().m_PosX;
			nkmdamageInst.m_AttackerPosZ = pAttacker.GetUnitSyncData().m_PosZ;
			nkmdamageInst.m_AttackerPosJumpY = pAttacker.GetUnitSyncData().m_JumpYPos;
			nkmdamageInst.m_bAttackerRight = pAttacker.GetUnitSyncData().m_bRight;
			nkmdamageInst.m_bAttackerAwaken = pAttacker.GetUnitTemplet().m_UnitTempletBase.m_bAwaken;
			nkmdamageInst.m_AttackerAddAttackUnitCount = pAttacker.GetUnitFrameData().m_AddAttackUnitCount;
			nkmdamageInst.m_AttackerUnitSkillTemplet = attackerSkillTemplet;
			nkmdamageInst.m_bEvade = false;
			pDefender.DamageReact(nkmdamageInst, false);
			if (nkmdamageInst.m_ReActResult == NKM_REACT_TYPE.NRT_NO)
			{
				this.m_ObjectPool.CloseObj(nkmdamageInst);
				return false;
			}
			bool flag = true;
			for (int i = 0; i < nkmdamageInst.m_listHitUnit.Count; i++)
			{
				if (nkmdamageInst.m_listHitUnit[i] == nkmdamageInst.m_DefenderUID)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				nkmdamageInst.m_listHitUnit.Add(nkmdamageInst.m_DefenderUID);
				nkmdamageInst.m_AttackCount++;
				nkmdamageInst.m_AttackCount += (int)(pDefender.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_ATTACK_COUNT_REDUCE) + 0.1f);
			}
			pAttacker.AttackResult(nkmdamageInst);
			if (nkmdamageInst.m_Templet.m_ReAttackCount > 1)
			{
				nkmdamageInst.m_bReAttackCountOver = false;
				nkmdamageInst.m_ReAttackCount = 1;
				nkmdamageInst.m_fReAttackGap = nkmdamageInst.m_Templet.m_fReAttackGap;
				NKMDamageInst nkmdamageInst2 = (NKMDamageInst)this.m_ObjectPool.OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKMDamageInst, "", "", false);
				nkmdamageInst2.DeepCopyFromSource(nkmdamageInst);
				this.m_linklistReAttack.AddLast(nkmdamageInst2);
			}
			if (nkmdamageInst.m_ReActResult == NKM_REACT_TYPE.NRT_INVINCIBLE && !pDefender.IsAlly(pAttacker))
			{
				this.m_ObjectPool.CloseObj(nkmdamageInst);
				return true;
			}
			if (this.m_NKM_GAME_CLASS_TYPE == NKM_GAME_CLASS_TYPE.NGCT_GAME_SERVER || this.m_NKM_GAME_CLASS_TYPE == NKM_GAME_CLASS_TYPE.NGCT_GAME_SERVER_LOCAL)
			{
				bool bLongRange = Math.Abs(pDefender.GetUnitSyncData().m_PosX - pAttacker.GetUnitSyncData().m_PosX) > NKMUnitStatManager.m_fLONG_RANGE;
				NKM_DAMAGE_RESULT_TYPE nkm_DAMAGE_RESULT_TYPE = NKM_DAMAGE_RESULT_TYPE.NDRT_NORMAL;
				bool bInstaKill = false;
				bool bBoss = false;
				if (pDefender.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
				{
					bBoss = true;
				}
				if (pDefender.IsBoss())
				{
					bBoss = true;
				}
				float num;
				if (this.GetGameData().m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PRACTICE && this.GetGameRuntimeData().m_bPracticeFixedDamage)
				{
					num = NKMUnitStatManager.GetAttackFactorDamage(nkmdamageInst.m_Templet.m_DamageTempletBase, nkmdamageInst.m_AttackerUnitSkillTemplet, bBoss);
				}
				else if (pDefender.WillInstaKilled(nkmdamageInst.m_Templet))
				{
					num = pDefender.GetNowHP() * 2f;
					pDefender.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_INSTA_KILL);
					bInstaKill = true;
				}
				else
				{
					num = NKMUnitStatManager.GetFinalDamage(this.GetGameData().IsPVP(), pAttacker.GetUnitFrameData().m_StatData, pDefender.GetUnitFrameData().m_StatData, pAttacker.GetUnitData(), pAttacker, pDefender, nkmdamageInst.m_Templet, attackerSkillTemplet, false, buffDamage, nkmdamageInst.m_bEvade, out nkm_DAMAGE_RESULT_TYPE, 0f, bLongRange, bBoss, pAttacker.GetHPRate(), false, damageAttribute);
				}
				if (nkmdamageInst.m_ReActResult == NKM_REACT_TYPE.NRT_REVENGE)
				{
					num *= 0.1f;
				}
				num = pDefender.GetModifiedDMGAfterEventDEF(pAttacker.GetUnitSyncData().m_PosX, num);
				pDefender.SetHitFeedBack();
				if (nkm_DAMAGE_RESULT_TYPE == NKM_DAMAGE_RESULT_TYPE.NDRT_CRITICAL)
				{
					pAttacker.SetHitCriticalFeedBack();
				}
				if (nkmdamageInst.m_Templet.m_DamageTempletBase != null && (!nkmdamageInst.m_Templet.m_DamageTempletBase.m_fAtkFactor.IsNearlyZero(1E-05f) || !nkmdamageInst.m_Templet.m_DamageTempletBase.m_fAtkFactorPVP.IsNearlyZero(1E-05f)) && nkmdamageInst.m_bEvade)
				{
					pDefender.SetHitEvadeFeedBack();
				}
				bool flag2;
				if (nkmdamageInst.m_ReActResult != NKM_REACT_TYPE.NRT_REVENGE && pDefender.GetUnitSyncData().GetHP() > pDefender.GetUnitFrameData().m_fDamageThisFrame && pDefender.GetUnitSyncData().GetHP() <= pDefender.GetUnitFrameData().m_fDamageThisFrame + num && pDefender.GetPhaseDamageLimit(out flag2) <= 0f && !pDefender.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_IMMORTAL))
				{
					pAttacker.Kill(nkmdamageInst.m_Templet.m_NKMKillFeedBack, pDefender.GetUnitDataGame().m_GameUnitUID);
				}
				pDefender.AddDamage(false, num, nkm_DAMAGE_RESULT_TYPE, pAttacker.GetUnitDataGame().m_GameUnitUID, pAttacker.GetUnitDataGame().m_NKM_TEAM_TYPE, false, false, bInstaKill);
				if (nkmdamageInst.m_ReActResult == NKM_REACT_TYPE.NRT_REVENGE)
				{
					if (pDefender.GetUnitFrameData().m_fDamageThisFrame >= pDefender.GetUnitSyncData().GetHP())
					{
						pDefender.GetUnitFrameData().m_fDamageThisFrame = pDefender.GetUnitSyncData().GetHP() - 1f;
					}
				}
				else
				{
					if (nkmdamageInst.m_Templet.CanApplyStun(pDefender.GetUnitTemplet().m_UnitTempletBase))
					{
						pDefender.ApplyStatusTime(NKM_UNIT_STATUS_EFFECT.NUSE_STUN, nkmdamageInst.m_Templet.m_fStunTime, pAttacker, false, false, false);
					}
					if (nkmdamageInst.m_Templet.CanApplyCooltimeDamage(pDefender.GetUnitTemplet().m_UnitTempletBase))
					{
						pDefender.AddDamage(false, nkmdamageInst.m_Templet.m_fCoolTimeDamage, NKM_DAMAGE_RESULT_TYPE.NDRT_COOL_TIME, pAttacker.GetUnitDataGame().m_GameUnitUID, pAttacker.GetUnitDataGame().m_NKM_TEAM_TYPE, false, false, false);
					}
					if (nkmdamageInst.m_Templet.CanApplyStatus(pDefender.GetUnitTemplet().m_UnitTempletBase))
					{
						pDefender.ApplyStatusTime(nkmdamageInst.m_Templet.m_ApplyStatusEffect, nkmdamageInst.m_Templet.m_fApplyStatusTime, pAttacker, false, false, false);
					}
				}
				this.ProcessHitBuff(nkmdamageInst, pAttacker, pDefender);
			}
			this.ProcessHitEvent(nkmdamageInst, pAttacker, pDefender, false);
			this.m_ObjectPool.CloseObj(nkmdamageInst);
			return true;
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x0007DA94 File Offset: 0x0007BC94
		public void ProcessHitBuff(NKMDamageInst cNKMDamageInst, NKMUnit pAttackerUnit, NKMUnit pDefender)
		{
			if (pAttackerUnit == null)
			{
				return;
			}
			if (cNKMDamageInst.m_Templet.m_DeleteBuffCount > 0)
			{
				int num = (int)cNKMDamageInst.m_Templet.m_DeleteBuffCount;
				num -= pDefender.DispelStatusTime(false, num);
				pDefender.DispelRandomBuff(num, false);
				pDefender.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_DISPEL);
			}
			if (cNKMDamageInst.m_Templet.m_DeleteConfuseBuff)
			{
				pDefender.DeleteStatusBuff(NKM_UNIT_STATUS_EFFECT.NUSE_CONFUSE, false, true);
			}
			if (!string.IsNullOrEmpty(cNKMDamageInst.m_Templet.m_AttackerBuff))
			{
				this.ProcessAttackerBuff(cNKMDamageInst, pAttackerUnit, cNKMDamageInst.m_Templet.m_AttackerBuff, cNKMDamageInst.m_Templet.m_AttackerBuffStatBaseLevel, cNKMDamageInst.m_Templet.m_AttackerBuffStatAddLVBySkillLV, cNKMDamageInst.m_Templet.m_AttackerBuffTimeBaseLevel, cNKMDamageInst.m_Templet.m_AttackerBuffTimeAddLVBySkillLV, false, 1);
			}
			for (int i = 0; i < cNKMDamageInst.m_Templet.m_listAttackerHitBuff.Count; i++)
			{
				if (pAttackerUnit.CheckEventCondition(cNKMDamageInst.m_Templet.m_listAttackerHitBuff[i].m_Condition))
				{
					this.ProcessAttackerBuff(cNKMDamageInst, pAttackerUnit, cNKMDamageInst.m_Templet.m_listAttackerHitBuff[i].m_HitBuff, cNKMDamageInst.m_Templet.m_listAttackerHitBuff[i].m_HitBuffStatBaseLevel, cNKMDamageInst.m_Templet.m_listAttackerHitBuff[i].m_HitBuffStatAddLVBySkillLV, cNKMDamageInst.m_Templet.m_listAttackerHitBuff[i].m_HitBuffTimeBaseLevel, cNKMDamageInst.m_Templet.m_listAttackerHitBuff[i].m_HitBuffTimeAddLVBySkillLV, cNKMDamageInst.m_Templet.m_listAttackerHitBuff[i].m_bRemove, cNKMDamageInst.m_Templet.m_listAttackerHitBuff[i].m_HitBuffOverlap);
				}
			}
			if (!string.IsNullOrEmpty(cNKMDamageInst.m_Templet.m_DefenderBuff))
			{
				this.ProcessDefenderBuff(cNKMDamageInst, pAttackerUnit, pDefender, cNKMDamageInst.m_Templet.m_DefenderBuff, cNKMDamageInst.m_Templet.m_DefenderBuffStatBaseLevel, cNKMDamageInst.m_Templet.m_DefenderBuffStatAddLVBySkillLV, cNKMDamageInst.m_Templet.m_DefenderBuffTimeBaseLevel, cNKMDamageInst.m_Templet.m_DefenderBuffTimeAddLVBySkillLV, false, 1);
			}
			for (int j = 0; j < cNKMDamageInst.m_Templet.m_listDefenderHitBuff.Count; j++)
			{
				if (pAttackerUnit.CheckEventCondition(cNKMDamageInst.m_Templet.m_listDefenderHitBuff[j].m_Condition))
				{
					this.ProcessDefenderBuff(cNKMDamageInst, pAttackerUnit, pDefender, cNKMDamageInst.m_Templet.m_listDefenderHitBuff[j].m_HitBuff, cNKMDamageInst.m_Templet.m_listDefenderHitBuff[j].m_HitBuffStatBaseLevel, cNKMDamageInst.m_Templet.m_listDefenderHitBuff[j].m_HitBuffStatAddLVBySkillLV, cNKMDamageInst.m_Templet.m_listDefenderHitBuff[j].m_HitBuffTimeBaseLevel, cNKMDamageInst.m_Templet.m_listDefenderHitBuff[j].m_HitBuffTimeAddLVBySkillLV, cNKMDamageInst.m_Templet.m_listDefenderHitBuff[j].m_bRemove, cNKMDamageInst.m_Templet.m_listDefenderHitBuff[j].m_HitBuffOverlap);
				}
			}
			if (pAttackerUnit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_ENV)
			{
				for (int k = 0; k < pDefender.GetUnitTemplet().m_listReflectionBuffData.Count; k++)
				{
					NKMStaticBuffData nkmstaticBuffData = pDefender.GetUnitTemplet().m_listReflectionBuffData[k];
					if (pDefender.CheckEventCondition(nkmstaticBuffData.m_Condition))
					{
						pAttackerUnit.AddBuffByStrID(nkmstaticBuffData.m_BuffStrID, nkmstaticBuffData.m_BuffStatLevel, nkmstaticBuffData.m_BuffTimeLevel, pDefender.GetUnitDataGame().m_GameUnitUID, true, false, false, 1);
					}
				}
				if (pDefender.GetUnitStateNow() != null)
				{
					for (int l = 0; l < pDefender.GetUnitStateNow().m_listNKMEventBuff.Count; l++)
					{
						NKMEventBuff nkmeventBuff = pDefender.GetUnitStateNow().m_listNKMEventBuff[l];
						if (nkmeventBuff != null && nkmeventBuff.m_bReflection && pDefender.CheckEventCondition(nkmeventBuff.m_Condition))
						{
							bool flag = false;
							if (pDefender.EventTimer(nkmeventBuff.m_bAnimTime, nkmeventBuff.m_fEventTime, true) && !nkmeventBuff.m_bStateEndTime)
							{
								flag = true;
							}
							if (flag)
							{
								int num2 = (int)nkmeventBuff.m_BuffStatLevel;
								int num3 = (int)nkmeventBuff.m_BuffTimeLevel;
								NKMUnitTemplet unitTemplet = pDefender.GetUnitData().GetUnitTemplet();
								if (unitTemplet != null && unitTemplet.m_UnitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_SHIP)
								{
									NKMUnitSkillTemplet unitSkillTempletByType = pDefender.GetUnitData().GetUnitSkillTempletByType(pDefender.GetUnitStateNow().m_NKM_SKILL_TYPE);
									if (unitSkillTempletByType != null && unitSkillTempletByType.m_Level > 0)
									{
										if (nkmeventBuff.m_BuffStatLevelPerSkillLevel > 0)
										{
											num2 += (unitSkillTempletByType.m_Level - 1) * (int)nkmeventBuff.m_BuffStatLevelPerSkillLevel;
										}
										if (nkmeventBuff.m_BuffTimeLevelPerSkillLevel > 0)
										{
											num3 += (unitSkillTempletByType.m_Level - 1) * (int)nkmeventBuff.m_BuffTimeLevelPerSkillLevel;
										}
									}
								}
								pAttackerUnit.AddBuffByStrID(nkmeventBuff.m_BuffStrID, (byte)num2, (byte)num3, pDefender.GetUnitDataGame().m_GameUnitUID, true, false, false, (byte)nkmeventBuff.m_Overlap);
							}
						}
					}
				}
			}
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x0007DF38 File Offset: 0x0007C138
		private void ProcessAttackerBuff(NKMDamageInst cNKMDamageInst, NKMUnit cAttackerUnit, string hitBuff, byte hitBuffBaseLevel, byte hitBuffAddLVBySkillLV, byte hitBuffTimeBaseLevel, byte hitBuffTimeAddLVBySkillLV, bool bRemove, byte overlap)
		{
			NKMBuffTemplet buffTempletByStrID = NKMBuffManager.GetBuffTempletByStrID(hitBuff);
			if (buffTempletByStrID != null)
			{
				if (bRemove)
				{
					if (cAttackerUnit != null)
					{
						cAttackerUnit.DeleteBuff(hitBuff, false);
						return;
					}
				}
				else if ((int)cNKMDamageInst.m_AtkBuffCount < buffTempletByStrID.m_RangeSonCount && cAttackerUnit != null)
				{
					byte b = 0;
					byte b2 = 0;
					NKMUnitState unitState = cAttackerUnit.GetUnitState((short)cAttackerUnit.GetUnitSyncData().m_StateID);
					if (unitState != null && (unitState.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_HYPER || unitState.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_SKILL))
					{
						NKMUnitSkillTemplet stateSkill = cAttackerUnit.GetStateSkill(unitState);
						if (stateSkill != null)
						{
							b = (byte)((int)hitBuffAddLVBySkillLV * (stateSkill.m_Level - 1));
							b2 = (byte)((int)hitBuffTimeAddLVBySkillLV * (stateSkill.m_Level - 1));
						}
					}
					cAttackerUnit.AddBuffByID(buffTempletByStrID.m_BuffID, hitBuffBaseLevel + b, hitBuffTimeBaseLevel + b2, cAttackerUnit.GetUnitDataGame().m_GameUnitUID, true, false, false, overlap);
					cNKMDamageInst.m_AtkBuffCount += 1;
				}
			}
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x0007E008 File Offset: 0x0007C208
		private void ProcessDefenderBuff(NKMDamageInst cNKMDamageInst, NKMUnit cAttackerUnit, NKMUnit cDefenderUnit, string hitBuff, byte hitBuffBaseLevel, byte hitBuffAddLVBySkillLV, byte hitBuffTimeBaseLevel, byte hitBuffTimeAddLVBySkillLV, bool bRemove, byte overlap)
		{
			NKMBuffTemplet buffTempletByStrID = NKMBuffManager.GetBuffTempletByStrID(hitBuff);
			if (buffTempletByStrID != null)
			{
				if (bRemove)
				{
					if (cDefenderUnit != null && cAttackerUnit != null)
					{
						bool bFromEnemy = !cDefenderUnit.IsAlly(cAttackerUnit);
						cDefenderUnit.DeleteBuff(hitBuff, bFromEnemy);
						return;
					}
				}
				else if ((int)cNKMDamageInst.m_DefBuffCount < buffTempletByStrID.m_RangeSonCount && cAttackerUnit != null)
				{
					byte b = 0;
					byte b2 = 0;
					NKMUnitState unitState = cAttackerUnit.GetUnitState((short)cAttackerUnit.GetUnitSyncData().m_StateID);
					if (unitState != null && (unitState.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_HYPER || unitState.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_SKILL))
					{
						NKMUnitSkillTemplet stateSkill = cAttackerUnit.GetStateSkill(unitState);
						if (stateSkill != null)
						{
							b = (byte)((int)hitBuffAddLVBySkillLV * (stateSkill.m_Level - 1));
							b2 = (byte)((int)hitBuffTimeAddLVBySkillLV * (stateSkill.m_Level - 1));
						}
					}
					cDefenderUnit.AddBuffByID(buffTempletByStrID.m_BuffID, hitBuffBaseLevel + b, hitBuffTimeBaseLevel + b2, cAttackerUnit.GetUnitDataGame().m_GameUnitUID, true, false, false, overlap);
					cNKMDamageInst.m_DefBuffCount += 1;
				}
			}
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x0007E0F0 File Offset: 0x0007C2F0
		public void ProcessHitEvent(NKMDamageInst cNKMDamageInst, NKMUnit pAttackerUnit, NKMUnit pDefender, bool bFromDE)
		{
			if (pAttackerUnit == null)
			{
				return;
			}
			if (cNKMDamageInst.m_Templet.m_EventMove != null)
			{
				bool flag = true;
				if (cNKMDamageInst.m_Templet.m_CrashSuperArmorLevel != NKM_SUPER_ARMOR_LEVEL.NSAL_INVALID && this.IsEnemy(pAttackerUnit.GetUnitDataGame().m_NKM_TEAM_TYPE, pDefender.GetUnitDataGame().m_NKM_TEAM_TYPE))
				{
					if (pDefender.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_NO_DAMAGE_BACK_SPEED))
					{
						flag = false;
					}
					NKM_SUPER_ARMOR_LEVEL currentSuperArmorLevel = pDefender.GetUnitFrameData().CurrentSuperArmorLevel;
					if (currentSuperArmorLevel != NKM_SUPER_ARMOR_LEVEL.NSAL_NO && currentSuperArmorLevel >= cNKMDamageInst.m_Templet.m_CrashSuperArmorLevel)
					{
						flag = false;
					}
				}
				if (pDefender.GetUnitTemplet().m_UnitTempletBase.IsShip())
				{
					flag = false;
				}
				if (pDefender.IsBoss())
				{
					flag = false;
				}
				if (flag)
				{
					pDefender.ApplyEventMove(cNKMDamageInst.m_Templet.m_EventMove, false, false, bFromDE);
				}
			}
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x0007E1A0 File Offset: 0x0007C3A0
		public void SetStopTime(float fStopTime, NKM_STOP_TIME_INDEX stopTimeIndex)
		{
			this.SetStopTime(-1, fStopTime, true, true, stopTimeIndex);
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x0007E1B0 File Offset: 0x0007C3B0
		public void SetStopTime(short callUnitUID, float fStopTime, bool bStopSelf, bool bStopSummonee, NKM_STOP_TIME_INDEX stopTimeIndex)
		{
			NKMGame.TimeStopEventInfo item = default(NKMGame.TimeStopEventInfo);
			item.fStopTime = fStopTime;
			item.stopTimeIndex = stopTimeIndex;
			item.bStopSelf = bStopSelf;
			item.callUnitUID = callUnitUID;
			item.bStopSummonee = bStopSummonee;
			this.m_qEventTimeStop.Enqueue(item);
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x0007E1FC File Offset: 0x0007C3FC
		protected void ProcessStopTime()
		{
			if (this.m_fWorldStopTime > 0f)
			{
				return;
			}
			if (this.m_qEventTimeStop.Count <= 0)
			{
				return;
			}
			NKMGame.TimeStopEventInfo timeStopEventInfo = this.m_qEventTimeStop.Dequeue();
			if (timeStopEventInfo.callUnitUID == -1)
			{
				this.ApplyStopTime(timeStopEventInfo.fStopTime, timeStopEventInfo.stopTimeIndex);
			}
			else
			{
				this.ApplyStopTime(timeStopEventInfo.callUnitUID, timeStopEventInfo.fStopTime, timeStopEventInfo.bStopSelf, timeStopEventInfo.bStopSummonee, timeStopEventInfo.stopTimeIndex);
			}
			this.SetWorldStopTime(timeStopEventInfo.fStopTime);
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x0007E280 File Offset: 0x0007C480
		private void ApplyStopTime(float fStopTime, NKM_STOP_TIME_INDEX stopTimeIndex)
		{
			for (int i = 0; i < this.m_listNKMUnit.Count; i++)
			{
				NKMUnit nkmunit = this.m_listNKMUnit[i];
				if (nkmunit != null)
				{
					nkmunit.SetStopTime(fStopTime, stopTimeIndex);
				}
			}
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x0007E2BC File Offset: 0x0007C4BC
		private void ApplyStopTime(short callUnitUID, float fStopTime, bool bMyStop, bool bSummoneeStop, NKM_STOP_TIME_INDEX stopTimeIndex)
		{
			NKMUnit unit = this.GetUnit(callUnitUID, true, false);
			if (unit != null)
			{
				for (int i = 0; i < this.m_listNKMUnit.Count; i++)
				{
					NKMUnit nkmunit = this.m_listNKMUnit[i];
					if (nkmunit != null && (bMyStop || unit != nkmunit) && (bSummoneeStop || nkmunit.GetMasterUnit() != unit))
					{
						nkmunit.SetStopTime(fStopTime, stopTimeIndex);
					}
				}
			}
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x0007E31C File Offset: 0x0007C51C
		public void SetStopReserveTime(short callUnitUID, float fStopReserveTime, bool bEnemyOnly)
		{
			NKMUnit unit = this.GetUnit(callUnitUID, true, false);
			if (unit != null)
			{
				for (int i = 0; i < this.m_listNKMUnit.Count; i++)
				{
					NKMUnit nkmunit = this.m_listNKMUnit[i];
					if (!bEnemyOnly || this.IsEnemy(unit.GetUnitDataGame().m_NKM_TEAM_TYPE, nkmunit.GetUnitDataGame().m_NKM_TEAM_TYPE))
					{
						nkmunit.SetStopReserveTime(fStopReserveTime);
					}
				}
			}
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x0007E381 File Offset: 0x0007C581
		public float GetZScaleFactor(float fPosZ)
		{
			return 1.05f - (fPosZ - this.GetMapTemplet().m_fMinZ) * 0.001f;
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x0007E39C File Offset: 0x0007C59C
		public void AllKill(NKM_TEAM_TYPE eNKM_TEAM_TYPE)
		{
			foreach (KeyValuePair<short, NKMUnit> keyValuePair in this.m_dicNKMUnit)
			{
				NKMUnit value = keyValuePair.Value;
				if (value.GetUnitDataGame().m_NKM_TEAM_TYPE == eNKM_TEAM_TYPE && !this.IsBoss(value.GetUnitSyncData().m_GameUnitUID))
				{
					value.GetUnitSyncData().SetHP(0f);
					value.SetDying(true, false);
				}
			}
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x0007E40C File Offset: 0x0007C60C
		public void PracticeBossStateChange(string stateName)
		{
			if (this.GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_PRACTICE)
			{
				return;
			}
			if (this.GetGameData().m_NKMGameTeamDataB.m_MainShip == null)
			{
				return;
			}
			for (int i = 0; i < this.GetGameData().m_NKMGameTeamDataB.m_MainShip.m_listGameUnitUID.Count; i++)
			{
				short gameUnitUID = this.GetGameData().m_NKMGameTeamDataB.m_MainShip.m_listGameUnitUID[i];
				NKMUnit unit = this.GetUnit(gameUnitUID, true, true);
				if (unit != null)
				{
					unit.StateChange(stateName, true, false);
					return;
				}
			}
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x0007E493 File Offset: 0x0007C693
		public void PracticeHealEnable(bool value)
		{
			if (this.GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_PRACTICE)
			{
				return;
			}
			this.GetGameRuntimeData().m_bPracticeHeal = value;
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x0007E4B0 File Offset: 0x0007C6B0
		public void PracticeFixedDamageEnable(bool value)
		{
			if (this.GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_PRACTICE)
			{
				return;
			}
			this.GetGameRuntimeData().m_bPracticeFixedDamage = value;
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x0007E4D0 File Offset: 0x0007C6D0
		public void DEV_HPReset(NKM_TEAM_TYPE eNKM_TEAM_TYPE)
		{
			foreach (KeyValuePair<short, NKMUnit> keyValuePair in this.m_dicNKMUnit)
			{
				NKMUnit value = keyValuePair.Value;
				if (value.GetUnitDataGame().m_NKM_TEAM_TYPE == eNKM_TEAM_TYPE)
				{
					value.GetUnitSyncData().SetHP(value.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP));
				}
			}
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x0007E530 File Offset: 0x0007C730
		public void DEV_SkillCoolTimeReset(NKM_TEAM_TYPE eNKM_TEAM_TYPE)
		{
			foreach (KeyValuePair<short, NKMUnit> keyValuePair in this.m_dicNKMUnit)
			{
				NKMUnit value = keyValuePair.Value;
				if (value.GetUnitDataGame().m_NKM_TEAM_TYPE == eNKM_TEAM_TYPE)
				{
					for (int i = 0; i < value.GetUnitTemplet().m_listSkillStateData.Count; i++)
					{
						if (value.GetUnitTemplet().m_listSkillStateData[i] != null)
						{
							value.StateCoolTimeReset(value.GetUnitTemplet().m_listSkillStateData[i].m_StateName);
						}
					}
					for (int j = 0; j < value.GetUnitTemplet().m_listAirSkillStateData.Count; j++)
					{
						if (value.GetUnitTemplet().m_listAirSkillStateData[j] != null)
						{
							value.StateCoolTimeReset(value.GetUnitTemplet().m_listAirSkillStateData[j].m_StateName);
						}
					}
					if (this.IsBoss(value.GetUnitSyncData().m_GameUnitUID))
					{
						for (int k = 0; k < value.GetUnitTemplet().m_UnitTempletBase.GetSkillCount(); k++)
						{
							NKMShipSkillTemplet shipSkillTempletByIndex = NKMShipSkillManager.GetShipSkillTempletByIndex(value.GetUnitTemplet().m_UnitTempletBase, k);
							if (shipSkillTempletByIndex != null && shipSkillTempletByIndex.m_UnitStateName.Length > 1)
							{
								value.StateCoolTimeReset(shipSkillTempletByIndex.m_UnitStateName);
							}
						}
					}
				}
			}
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x0007E67C File Offset: 0x0007C87C
		public void DEV_HyperSkillCoolTimeReset(NKM_TEAM_TYPE eNKM_TEAM_TYPE)
		{
			foreach (KeyValuePair<short, NKMUnit> keyValuePair in this.m_dicNKMUnit)
			{
				NKMUnit value = keyValuePair.Value;
				if (value.GetUnitDataGame().m_NKM_TEAM_TYPE == eNKM_TEAM_TYPE)
				{
					for (int i = 0; i < value.GetUnitTemplet().m_listHyperSkillStateData.Count; i++)
					{
						if (value.GetUnitTemplet().m_listHyperSkillStateData[i] != null)
						{
							value.StateCoolTimeReset(value.GetUnitTemplet().m_listHyperSkillStateData[i].m_StateName);
						}
					}
					for (int j = 0; j < value.GetUnitTemplet().m_listAirHyperSkillStateData.Count; j++)
					{
						if (value.GetUnitTemplet().m_listAirHyperSkillStateData[j] != null)
						{
							value.StateCoolTimeReset(value.GetUnitTemplet().m_listAirHyperSkillStateData[j].m_StateName);
						}
					}
					if (this.IsBoss(value.GetUnitSyncData().m_GameUnitUID))
					{
						for (int k = 0; k < value.GetUnitTemplet().m_UnitTempletBase.GetSkillCount(); k++)
						{
							NKMShipSkillTemplet shipSkillTempletByIndex = NKMShipSkillManager.GetShipSkillTempletByIndex(value.GetUnitTemplet().m_UnitTempletBase, k);
							if (shipSkillTempletByIndex != null && shipSkillTempletByIndex.m_UnitStateName.Length > 1)
							{
								value.StateCoolTimeReset(shipSkillTempletByIndex.m_UnitStateName);
							}
						}
					}
				}
			}
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x0007E7C8 File Offset: 0x0007C9C8
		public bool IsBoss(short gameUnitUID)
		{
			if (this.GetGameData().m_NKMGameTeamDataA.m_MainShip != null)
			{
				for (int i = 0; i < this.GetGameData().m_NKMGameTeamDataA.m_MainShip.m_listGameUnitUID.Count; i++)
				{
					if (gameUnitUID == this.GetGameData().m_NKMGameTeamDataA.m_MainShip.m_listGameUnitUID[i])
					{
						return true;
					}
				}
			}
			if (this.GetGameData().m_NKMGameTeamDataB.m_MainShip != null)
			{
				for (int j = 0; j < this.GetGameData().m_NKMGameTeamDataB.m_MainShip.m_listGameUnitUID.Count; j++)
				{
					if (gameUnitUID == this.GetGameData().m_NKMGameTeamDataB.m_MainShip.m_listGameUnitUID[j])
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x0007E884 File Offset: 0x0007CA84
		public NKM_DUNGEON_TYPE GetDungeonType()
		{
			if (this.m_NKMGameData != null && this.m_NKMGameData.IsPVE() && this.m_NKMDungeonTemplet != null && this.m_NKMDungeonTemplet.m_DungeonTempletBase != null)
			{
				return this.m_NKMDungeonTemplet.m_DungeonTempletBase.m_DungeonType;
			}
			return NKM_DUNGEON_TYPE.NDT_INVALID;
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x0007E8C4 File Offset: 0x0007CAC4
		public float GetHyperBeginRatio(NKM_TEAM_TYPE team)
		{
			float result = -1f;
			if (this.m_NKMDungeonTemplet != null)
			{
				switch (team)
				{
				case NKM_TEAM_TYPE.NTT_A1:
				case NKM_TEAM_TYPE.NTT_A2:
					result = this.m_NKMDungeonTemplet.m_fAllyHyperCooltimeStartRatio;
					break;
				case NKM_TEAM_TYPE.NTT_B1:
				case NKM_TEAM_TYPE.NTT_B2:
					result = this.m_NKMDungeonTemplet.m_fEnemyHyperCooltimeStartRatio;
					break;
				}
			}
			return result;
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x0007E91E File Offset: 0x0007CB1E
		public float GetShipSkillBeginRatio(NKM_TEAM_TYPE team)
		{
			if (this.IsATeam(team) && this.m_NKMGameData.m_TeamASupply > 0)
			{
				return 1f;
			}
			return 0f;
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x0007E944 File Offset: 0x0007CB44
		public bool IsEnemy(NKM_TEAM_TYPE eMyTeam, NKM_TEAM_TYPE eTargetTeam)
		{
			if (eMyTeam == eTargetTeam)
			{
				return false;
			}
			switch (eMyTeam)
			{
			case NKM_TEAM_TYPE.NTT_A1:
				if (eTargetTeam == NKM_TEAM_TYPE.NTT_A2)
				{
					return false;
				}
				break;
			case NKM_TEAM_TYPE.NTT_A2:
				if (eTargetTeam == NKM_TEAM_TYPE.NTT_A1)
				{
					return false;
				}
				break;
			case NKM_TEAM_TYPE.NTT_B1:
				if (eTargetTeam == NKM_TEAM_TYPE.NTT_B2)
				{
					return false;
				}
				break;
			case NKM_TEAM_TYPE.NTT_B2:
				if (eTargetTeam == NKM_TEAM_TYPE.NTT_B1)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x0007E994 File Offset: 0x0007CB94
		public bool IsAlly(NKM_TEAM_TYPE eMyTeam, NKM_TEAM_TYPE eTargetTeam)
		{
			switch (eMyTeam)
			{
			case NKM_TEAM_TYPE.NTT_A1:
				if (eTargetTeam == NKM_TEAM_TYPE.NTT_A2)
				{
					return true;
				}
				break;
			case NKM_TEAM_TYPE.NTT_A2:
				if (eTargetTeam == NKM_TEAM_TYPE.NTT_A1)
				{
					return true;
				}
				break;
			case NKM_TEAM_TYPE.NTT_B1:
				if (eTargetTeam == NKM_TEAM_TYPE.NTT_B2)
				{
					return true;
				}
				break;
			case NKM_TEAM_TYPE.NTT_B2:
				if (eTargetTeam == NKM_TEAM_TYPE.NTT_B1)
				{
					return true;
				}
				break;
			}
			return false;
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x0007E9D3 File Offset: 0x0007CBD3
		public bool IsSameTeam(NKM_TEAM_TYPE eMyTeam, NKM_TEAM_TYPE eTargetTeam)
		{
			return NKMGame.IsSameTeamStaticFunc(eMyTeam, eTargetTeam);
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x0007E9DC File Offset: 0x0007CBDC
		public static bool IsSameTeamStaticFunc(NKM_TEAM_TYPE eMyTeam, NKM_TEAM_TYPE eTargetTeam)
		{
			switch (eMyTeam)
			{
			case NKM_TEAM_TYPE.NTT_A1:
			case NKM_TEAM_TYPE.NTT_A2:
				return eTargetTeam == NKM_TEAM_TYPE.NTT_A1 || eTargetTeam == NKM_TEAM_TYPE.NTT_A2;
			case NKM_TEAM_TYPE.NTT_B1:
			case NKM_TEAM_TYPE.NTT_B2:
				return eTargetTeam == NKM_TEAM_TYPE.NTT_B1 || eTargetTeam == NKM_TEAM_TYPE.NTT_B2;
			case NKM_TEAM_TYPE.NTT_C:
				return eTargetTeam == NKM_TEAM_TYPE.NTT_C;
			}
			return false;
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x0007EA29 File Offset: 0x0007CC29
		public bool IsReversePosTeam(NKM_TEAM_TYPE eTeam)
		{
			return eTeam == NKM_TEAM_TYPE.NTT_B1 || eTeam == NKM_TEAM_TYPE.NTT_B2 || eTeam == NKM_TEAM_TYPE.NTT_C;
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x0007EA3A File Offset: 0x0007CC3A
		public bool IsATeam(NKM_TEAM_TYPE eTeam)
		{
			return NKMGame.IsATeamStaticFunc(eTeam);
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x0007EA42 File Offset: 0x0007CC42
		public static bool IsATeamStaticFunc(NKM_TEAM_TYPE eTeam)
		{
			return eTeam == NKM_TEAM_TYPE.NTT_A1 || eTeam == NKM_TEAM_TYPE.NTT_A2;
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x0007EA4F File Offset: 0x0007CC4F
		public bool IsBTeam(NKM_TEAM_TYPE eTeam)
		{
			return NKMGame.IsBTeamStaticFunc(eTeam);
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x0007EA57 File Offset: 0x0007CC57
		public static bool IsBTeamStaticFunc(NKM_TEAM_TYPE eTeam)
		{
			return eTeam == NKM_TEAM_TYPE.NTT_B1 || eTeam == NKM_TEAM_TYPE.NTT_B2;
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x0007EA64 File Offset: 0x0007CC64
		public void GiveUp()
		{
			this.m_NKMGameRuntimeData.m_bGiveUp = true;
			this.m_NKMGameRuntimeData.m_WinTeam = NKM_TEAM_TYPE.NTT_B1;
			this.SetGameState(NKM_GAME_STATE.NGS_FINISH);
			this.m_NKMGameRuntimeData.m_bPause = false;
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x0007EA92 File Offset: 0x0007CC92
		protected virtual bool SetGameState(NKM_GAME_STATE eNKM_GAME_STATE)
		{
			if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_END)
			{
				return false;
			}
			this.m_NKMGameRuntimeData.m_NKM_GAME_STATE = eNKM_GAME_STATE;
			return true;
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x0007EAB1 File Offset: 0x0007CCB1
		public static bool CanUseAutoRespawnOnlyDungeon(NKMUserData cNKMUserData, int dungeonID)
		{
			return dungeonID != 0 && dungeonID - 20001 > 4;
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x0007EAC5 File Offset: 0x0007CCC5
		public static bool CanUseAutoRespawnOnlyWarfare(NKMUserData userData, int warfareID)
		{
			return warfareID != 0;
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x0007EAD0 File Offset: 0x0007CCD0
		public bool CanUseAutoRespawn(NKMUserData cNKMUserData)
		{
			NKM_GAME_TYPE gameType = this.GetGameData().GetGameType();
			switch (gameType)
			{
			case NKM_GAME_TYPE.NGT_DUNGEON:
				break;
			case NKM_GAME_TYPE.NGT_WARFARE:
				return NKMGame.CanUseAutoRespawnOnlyWarfare(cNKMUserData, this.GetGameData().m_WarfareID);
			case NKM_GAME_TYPE.NGT_DIVE:
			case NKM_GAME_TYPE.NGT_PVP_RANK:
				return true;
			case NKM_GAME_TYPE.NGT_TUTORIAL:
				return false;
			default:
				if (gameType != NKM_GAME_TYPE.NGT_PHASE && gameType != NKM_GAME_TYPE.NGT_TRIM)
				{
					return true;
				}
				break;
			}
			return NKMGame.CanUseAutoRespawnOnlyDungeon(cNKMUserData, this.GetGameData().m_DungeonID);
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x0007EB38 File Offset: 0x0007CD38
		public bool IsPVP()
		{
			return this.GetGameData().IsPVP();
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x0007EB45 File Offset: 0x0007CD45
		public bool IsPVE()
		{
			return this.GetGameData().IsPVE();
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x0007EB54 File Offset: 0x0007CD54
		public int GetRespawnCost(NKMUnitStatTemplet cNKMUnitStatTemplet, bool bLeader, NKM_TEAM_TYPE teamType)
		{
			if (this.IsPVE() || this.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_ASYNC_PVP || (this.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_PVP_STRATEGY && this.IsATeam(teamType)) || (this.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_PVP_STRATEGY_REVENGE && this.IsATeam(teamType)) || this.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC)
			{
				return cNKMUnitStatTemplet.GetRespawnCost(false, bLeader, null, null);
			}
			return cNKMUnitStatTemplet.GetRespawnCost(this.GetGameData().IsPVP(), bLeader, this.GetGameData().m_dicNKMBanData, this.GetGameData().m_dicNKMUpData);
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x0007EBEA File Offset: 0x0007CDEA
		public bool IsBanUnit(int unitID)
		{
			return this.GetGameData().IsBanUnit(unitID);
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x0007EBF8 File Offset: 0x0007CDF8
		public bool IsUpUnit(int unitID)
		{
			return this.GetGameData().IsUpUnit(unitID);
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x0007EC06 File Offset: 0x0007CE06
		public bool IsBanShip(int shipGroupId)
		{
			return this.GetGameData().IsBanShip(shipGroupId);
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x0007EC14 File Offset: 0x0007CE14
		public int GetBanShipLevel(int shipGroupId)
		{
			return this.GetGameData().GetBanShipLevel(shipGroupId);
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x0007EC24 File Offset: 0x0007CE24
		public void EventDieForReRespawn(long unitUID)
		{
			NKMUnitData unitDataByUnitUID = this.GetGameData().GetUnitDataByUnitUID(unitUID);
			if (unitDataByUnitUID == null)
			{
				return;
			}
			for (int i = 0; i < unitDataByUnitUID.m_listGameUnitUID.Count; i++)
			{
				this.EventDieForReRespawnInner(unitDataByUnitUID.m_listGameUnitUID[i]);
			}
			for (int j = 0; j < unitDataByUnitUID.m_listGameUnitUIDChange.Count; j++)
			{
				this.EventDieForReRespawnInner(unitDataByUnitUID.m_listGameUnitUIDChange[j]);
			}
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x0007EC94 File Offset: 0x0007CE94
		public void EventDieForReRespawnInner(short gameUnitUID)
		{
			NKMUnit unit = this.GetUnit(gameUnitUID, true, false);
			if (unit != null)
			{
				unit.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_RE_RESPAWN_EFFECT);
				unit.EventDie(true, false, false);
				unit.PushSyncData();
				this.m_dicNKMUnitPool.Add(gameUnitUID, unit);
				this.m_dicNKMUnit.Remove(gameUnitUID);
				this.m_listNKMUnit.Remove(unit);
				unit.SetDie(true);
			}
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x0007ECF4 File Offset: 0x0007CEF4
		protected float GetRollbackTime(NKMUnitTemplet targetUnitTemplet)
		{
			if (!NKMCommonConst.USE_ROLLBACK)
			{
				return 0f;
			}
			if (targetUnitTemplet == null)
			{
				return 0f;
			}
			if (this.GetWorldStopTime() > 0f)
			{
				return 0f;
			}
			float num = Math.Min(targetUnitTemplet.m_fMaxRollbackTime, NKMCommonConst.SUMMON_UNIT_NOEVENT_TIME);
			float num2 = this.m_AbsoluteGameTime - num;
			if (this.m_fLastWorldStopFinishedATime > num2)
			{
				return this.m_AbsoluteGameTime - this.m_fLastWorldStopFinishedATime;
			}
			return num;
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x0007ED5C File Offset: 0x0007CF5C
		public bool IsInDynamicRespawnUnitReserve(short gameUnitUID)
		{
			for (int i = 0; i < this.m_listNKMGameUnitDynamicRespawnData.Count; i++)
			{
				NKMDynamicRespawnUnitReserve nkmdynamicRespawnUnitReserve = this.m_listNKMGameUnitDynamicRespawnData[i];
				if (nkmdynamicRespawnUnitReserve != null && nkmdynamicRespawnUnitReserve.m_GameUnitUID == gameUnitUID)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001B6C RID: 7020
		public const float DefaultInitialGameTime = 180f;

		// Token: 0x04001B6D RID: 7021
		public const byte MAX_RESPAWN_COST = 10;

		// Token: 0x04001B6E RID: 7022
		public const byte PVE_RESPAWN_COST = 4;

		// Token: 0x04001B6F RID: 7023
		public const byte PVP_RESPAWN_COST = 7;

		// Token: 0x04001B70 RID: 7024
		public const byte ASYNC_PVP_RESPAWN_COST = 10;

		// Token: 0x04001B71 RID: 7025
		public const float RESPAWN_COST_Add_ADJUST_VALUE = 0.3f;

		// Token: 0x04001B72 RID: 7026
		public const float GAME_PLAY_WAIT_TIME = 4f;

		// Token: 0x04001B73 RID: 7027
		public const float GAME_FINISH_WAIT_TIME = 8f;

		// Token: 0x04001B74 RID: 7028
		public const float GAME_DOUBLE_COST_TIME = 60f;

		// Token: 0x04001B75 RID: 7029
		public const float FULL_DECK_RESPAWN_COST_ADD = 0f;

		// Token: 0x04001B76 RID: 7030
		public const float FULL_DECK_COOL_TIME_REDUCE = 0f;

		// Token: 0x04001B77 RID: 7031
		public readonly float[] SHIP_ASSULT_RESPAWN_COST_ADD = new float[3];

		// Token: 0x04001B78 RID: 7032
		public readonly float[] SHIP_HEAVY_RESPAWN_COST_ADD = new float[3];

		// Token: 0x04001B79 RID: 7033
		public readonly float[] SHIP_CRUISER_RESPAWN_COST_ADD = new float[3];

		// Token: 0x04001B7A RID: 7034
		public readonly float[] SHIP_SPECIAL_RESPAWN_COST_ADD = new float[3];

		// Token: 0x04001B7B RID: 7035
		public readonly float[] SHIP_ASSULT_COOLTIME_REDUCE_ADD = new float[3];

		// Token: 0x04001B7C RID: 7036
		public readonly float[] SHIP_HEAVY_COOLTIME_REDUCE_ADD = new float[3];

		// Token: 0x04001B7D RID: 7037
		public readonly float[] SHIP_CRUISER_COOLTIME_REDUCE_ADD = new float[3];

		// Token: 0x04001B7E RID: 7038
		public readonly float[] SHIP_SPECIAL_COOLTIME_REDUCE_ADD = new float[3];

		// Token: 0x04001B7F RID: 7039
		public NKMGameRecord m_GameRecord = new NKMGameRecord();

		// Token: 0x04001B80 RID: 7040
		protected NKM_GAME_CLASS_TYPE m_NKM_GAME_CLASS_TYPE;

		// Token: 0x04001B81 RID: 7041
		protected NKMObjectPool m_ObjectPool;

		// Token: 0x04001B82 RID: 7042
		protected NKMGameData m_NKMGameData;

		// Token: 0x04001B83 RID: 7043
		protected NKMGameRuntimeData m_NKMGameRuntimeData = new NKMGameRuntimeData();

		// Token: 0x04001B84 RID: 7044
		protected NKMMapTemplet m_NKMMapTemplet;

		// Token: 0x04001B85 RID: 7045
		protected NKMDungeonTemplet m_NKMDungeonTemplet;

		// Token: 0x04001B86 RID: 7046
		protected HashSet<long> m_liveUnitUID = new HashSet<long>();

		// Token: 0x04001B87 RID: 7047
		protected List<short> m_listDieGameUnitUID = new List<short>();

		// Token: 0x04001B88 RID: 7048
		protected Dictionary<short, NKMUnit> m_dicNKMUnitPool = new Dictionary<short, NKMUnit>();

		// Token: 0x04001B89 RID: 7049
		protected Dictionary<short, NKMUnit> m_dicNKMUnit = new Dictionary<short, NKMUnit>();

		// Token: 0x04001B8A RID: 7050
		protected List<NKMUnit> m_listNKMUnit = new List<NKMUnit>();

		// Token: 0x04001B8B RID: 7051
		protected LinkedList<NKMDamageInst> m_linklistReAttack = new LinkedList<NKMDamageInst>();

		// Token: 0x04001B8C RID: 7052
		protected const float m_SyncFlushTimeMax = 0.4f;

		// Token: 0x04001B8D RID: 7053
		protected float m_fDeltaTime;

		// Token: 0x04001B8E RID: 7054
		protected NKMTrackingFloat m_GameSpeed = new NKMTrackingFloat();

		// Token: 0x04001B8F RID: 7055
		protected float m_fRespawnValidLandTeamA;

		// Token: 0x04001B90 RID: 7056
		protected float m_fRespawnValidLandTeamB;

		// Token: 0x04001B91 RID: 7057
		protected float m_fRespawnCostAddTeamA;

		// Token: 0x04001B92 RID: 7058
		protected float m_fRespawnCostAddTeamB;

		// Token: 0x04001B93 RID: 7059
		protected float m_fRespawnCostFullDeckAddTeamA;

		// Token: 0x04001B94 RID: 7060
		protected float m_fRespawnCostFullDeckAddTeamB;

		// Token: 0x04001B95 RID: 7061
		protected float m_fRespawnCostAsyncPVPAddTeamB;

		// Token: 0x04001B96 RID: 7062
		protected float m_fCoolTimeReduceFullDeckTeamA;

		// Token: 0x04001B97 RID: 7063
		protected float m_fCoolTimeReduceFullDeckTeamB;

		// Token: 0x04001B98 RID: 7064
		protected float m_fCoolTimeReduceTeamA;

		// Token: 0x04001B99 RID: 7065
		protected float m_fCoolTimeReduceTeamB;

		// Token: 0x04001B9A RID: 7066
		protected float m_fPlayWaitTimeOrg;

		// Token: 0x04001B9B RID: 7067
		protected float m_fPlayWaitTime;

		// Token: 0x04001B9C RID: 7068
		protected float m_fFinishWaitTime;

		// Token: 0x04001B9D RID: 7069
		public float m_AbsoluteGameTime;

		// Token: 0x04001B9E RID: 7070
		protected float m_fWorldStopTime;

		// Token: 0x04001B9F RID: 7071
		protected float m_fLastWorldStopFinishedATime;

		// Token: 0x04001BA0 RID: 7072
		protected NKMGameDevModeData m_GameDevModeData = new NKMGameDevModeData();

		// Token: 0x04001BA1 RID: 7073
		private const string UNIT_DIE_PREFIX = "UNITDIE_";

		// Token: 0x04001BA2 RID: 7074
		private const string DECK_DIE_PREFIX = "DECKDIE_";

		// Token: 0x04001BA3 RID: 7075
		protected Dictionary<string, int> m_EventStatusTag = new Dictionary<string, int>();

		// Token: 0x04001BA4 RID: 7076
		protected List<NKMDungeonEventData> m_listDungeonEventDataTeamA = new List<NKMDungeonEventData>();

		// Token: 0x04001BA5 RID: 7077
		protected List<NKMDungeonEventData> m_listDungeonEventDataTeamB = new List<NKMDungeonEventData>();

		// Token: 0x04001BA6 RID: 7078
		protected List<NKMDynamicRespawnUnitReserve> m_listNKMGameUnitDynamicRespawnData = new List<NKMDynamicRespawnUnitReserve>();

		// Token: 0x04001BA7 RID: 7079
		protected List<NKMGameUnitRespawnData> m_listNKMGameUnitRespawnData = new List<NKMGameUnitRespawnData>();

		// Token: 0x04001BA8 RID: 7080
		private Queue<NKMGame.TimeStopEventInfo> m_qEventTimeStop = new Queue<NKMGame.TimeStopEventInfo>();

		// Token: 0x020011E7 RID: 4583
		private struct TimeStopEventInfo
		{
			// Token: 0x040093CC RID: 37836
			public float fStopTime;

			// Token: 0x040093CD RID: 37837
			public NKM_STOP_TIME_INDEX stopTimeIndex;

			// Token: 0x040093CE RID: 37838
			public short callUnitUID;

			// Token: 0x040093CF RID: 37839
			public bool bStopSelf;

			// Token: 0x040093D0 RID: 37840
			public bool bStopSummonee;
		}
	}
}
