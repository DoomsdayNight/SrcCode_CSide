using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Game;
using Cs.Logging;
using Cs.Math;
using Cs.Protocol;
using NKM.Templet;

namespace NKM
{
	// Token: 0x0200041F RID: 1055
	public abstract class NKMGameServerHost : NKMGame
	{
		// Token: 0x06001C2B RID: 7211 RVA: 0x0007F120 File Offset: 0x0007D320
		public NKMGameServerHost()
		{
			this.m_NKM_GAME_CLASS_TYPE = NKM_GAME_CLASS_TYPE.NGCT_GAME_SERVER;
			this.m_ObjectPool = new NKMObjectPool();
			this.Init();
			this.m_DEManager.Init(this);
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x0007F190 File Offset: 0x0007D390
		public override void Init()
		{
			base.Init();
			this.m_listNKMGameUnitDynamicRespawnData.Clear();
			this.m_NKMGameSyncDataPack = new NKMGameSyncDataPack();
			this.m_SyncFlushTime = 0f;
			this.m_DEManager.Init(this);
			if (this.m_dicRespawnUnitUID_ByDeckUsed != null)
			{
				this.m_dicRespawnUnitUID_ByDeckUsed.Clear();
			}
			if (this.m_NKMReservedTacticalCommandTeamA != null)
			{
				this.m_NKMReservedTacticalCommandTeamA.Invalidate();
			}
			if (this.m_NKMReservedTacticalCommandTeamB != null)
			{
				this.m_NKMReservedTacticalCommandTeamB.Invalidate();
			}
			this.m_bTeamAFirstRespawn = false;
			this.m_bTeamBFirstRespawn = false;
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x0007F218 File Offset: 0x0007D418
		public void SetOperatorBanSkillLevel()
		{
			if (base.IsPVP())
			{
				if (this.m_NKMGameData.m_NKMGameTeamDataA.m_Operator != null)
				{
					NKMOperator @operator = this.m_NKMGameData.m_NKMGameTeamDataA.m_Operator;
					if (this.m_NKMGameData.IsBanOperator(@operator.id))
					{
						int banOperatorLevel = this.m_NKMGameData.GetBanOperatorLevel(@operator.id);
						byte level = @operator.mainSkill.level;
						@operator.mainSkill.level = (byte)Math.Max((int)level - banOperatorLevel * (int)NKMUnitStatManager.m_OperatorSkillLevelPerBanLevel, (int)NKMUnitStatManager.m_MinOperatorSkillLevelPerBanLevel);
						level = @operator.subSkill.level;
						@operator.subSkill.level = (byte)Math.Max((int)level - banOperatorLevel * (int)NKMUnitStatManager.m_OperatorSkillLevelPerBanLevel, (int)NKMUnitStatManager.m_MinOperatorSkillLevelPerBanLevel);
					}
				}
				if (this.m_NKMGameData.m_NKMGameTeamDataB.m_Operator != null)
				{
					NKMOperator operator2 = this.m_NKMGameData.m_NKMGameTeamDataB.m_Operator;
					if (this.m_NKMGameData.IsBanOperator(operator2.id))
					{
						int banOperatorLevel2 = this.m_NKMGameData.GetBanOperatorLevel(operator2.id);
						byte level2 = operator2.mainSkill.level;
						operator2.mainSkill.level = (byte)Math.Max((int)level2 - banOperatorLevel2 * (int)NKMUnitStatManager.m_OperatorSkillLevelPerBanLevel, (int)NKMUnitStatManager.m_MinOperatorSkillLevelPerBanLevel);
						level2 = operator2.subSkill.level;
						operator2.subSkill.level = (byte)Math.Max((int)level2 - banOperatorLevel2 * (int)NKMUnitStatManager.m_OperatorSkillLevelPerBanLevel, (int)NKMUnitStatManager.m_MinOperatorSkillLevelPerBanLevel);
					}
				}
			}
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x0007F37C File Offset: 0x0007D57C
		public virtual void SetGameData(NKMGameData cNKMGameData)
		{
			this.Init();
			this.m_NKMGameData = cNKMGameData;
			if (this.m_NKMGameData.m_DungeonID > 0)
			{
				this.m_NKMDungeonTemplet = NKMDungeonManager.GetDungeonTemplet(this.m_NKMGameData.m_DungeonID);
			}
			else
			{
				this.m_NKMDungeonTemplet = null;
			}
			this.SetOperatorBanSkillLevel();
			this.SetDefaultTacticalCommand();
			base.InitDungeonEventData();
			this.m_bUseStateChangeEvent = false;
			for (int i = 0; i < this.m_listDungeonEventDataTeamA.Count; i++)
			{
				NKMDungeonEventData nkmdungeonEventData = this.m_listDungeonEventDataTeamA[i];
				if (nkmdungeonEventData.m_DungeonEventTemplet.m_EventCondition == NKM_EVENT_START_CONDITION_TYPE.UNIT_STATE_START || nkmdungeonEventData.m_DungeonEventTemplet.m_EventCondition == NKM_EVENT_START_CONDITION_TYPE.UNIT_STATE_END)
				{
					this.m_bUseStateChangeEvent = true;
					break;
				}
			}
			if (!this.m_bUseStateChangeEvent)
			{
				for (int j = 0; j < this.m_listDungeonEventDataTeamB.Count; j++)
				{
					NKMDungeonEventData nkmdungeonEventData2 = this.m_listDungeonEventDataTeamB[j];
					if (nkmdungeonEventData2.m_DungeonEventTemplet.m_EventCondition == NKM_EVENT_START_CONDITION_TYPE.UNIT_STATE_START || nkmdungeonEventData2.m_DungeonEventTemplet.m_EventCondition == NKM_EVENT_START_CONDITION_TYPE.UNIT_STATE_END)
					{
						this.m_bUseStateChangeEvent = true;
						break;
					}
				}
			}
			this.SetMap();
			this.SetRemainGameTime();
			this.SetUnit();
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PREVENTION_TURTLING) && NKMCommonConst.PVP_AFK_APPLY_MODE.Contains(cNKMGameData.m_NKM_GAME_TYPE))
			{
				this.m_bUseTurtlingPrevent = true;
			}
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x0007F4A8 File Offset: 0x0007D6A8
		protected override void SetDefaultTacticalCommand()
		{
			base.SetDefaultTacticalCommand();
			if (base.GetGameData() == null)
			{
				return;
			}
			base.AddTacticalCommand(base.GetGameData().m_NKMGameTeamDataA);
			base.AddTacticalCommand(base.GetGameData().m_NKMGameTeamDataB);
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x0007F4DB File Offset: 0x0007D6DB
		public virtual void SetGameRuntimeData(NKMGameRuntimeData cNKMRuntimeGameData)
		{
			this.m_NKMGameRuntimeData = cNKMRuntimeGameData;
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x0007F4E4 File Offset: 0x0007D6E4
		public virtual float GetInitialGameTimeSec()
		{
			return 180f;
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x0007F4EC File Offset: 0x0007D6EC
		private void SetRemainGameTime()
		{
			if (this.m_NKMGameData == null)
			{
				return;
			}
			if (this.m_NKMGameRuntimeData == null)
			{
				return;
			}
			float initialGameTimeSec = this.GetInitialGameTimeSec();
			if (!this.m_NKMGameData.IsPVE())
			{
				this.m_NKMGameRuntimeData.m_fRemainGameTime = initialGameTimeSec;
				return;
			}
			if (base.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_PRACTICE)
			{
				this.m_NKMGameRuntimeData.m_fRemainGameTime = 999999f;
				return;
			}
			NKM_DUNGEON_TYPE dungeonType = base.GetDungeonType();
			if (dungeonType == NKM_DUNGEON_TYPE.NDT_DAMAGE_ACCRUE)
			{
				NKMDungeonTemplet dungeonTemplet = NKMDungeonManager.GetDungeonTemplet(base.GetGameData().m_DungeonID);
				this.m_NKMGameRuntimeData.m_fRemainGameTime = ((dungeonTemplet != null) ? dungeonTemplet.m_DungeonTempletBase.m_fGameTime : 60f);
				return;
			}
			if (dungeonType == NKM_DUNGEON_TYPE.NDT_WAVE)
			{
				NKMDungeonTemplet dungeonTemplet2 = NKMDungeonManager.GetDungeonTemplet(base.GetGameData().m_DungeonID);
				NKMDungeonWaveTemplet nkmdungeonWaveTemplet = (dungeonTemplet2 != null) ? dungeonTemplet2.GetWaveTemplet(1) : null;
				this.m_NKMGameRuntimeData.m_fRemainGameTime = ((dungeonTemplet2 != null && nkmdungeonWaveTemplet != null) ? nkmdungeonWaveTemplet.m_fNextWavetime : initialGameTimeSec);
				return;
			}
			NKMDungeonTemplet dungeonTemplet3 = NKMDungeonManager.GetDungeonTemplet(base.GetGameData().m_DungeonID);
			this.m_NKMGameRuntimeData.m_fRemainGameTime = ((dungeonTemplet3 != null) ? dungeonTemplet3.m_DungeonTempletBase.m_fGameTime : initialGameTimeSec);
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x0007F5FF File Offset: 0x0007D7FF
		public void SetMap()
		{
			this.m_NKMMapTemplet = NKMMapManager.GetMapTempletByID(this.m_NKMGameData.m_MapID);
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x0007F618 File Offset: 0x0007D818
		public void SetUnit()
		{
			base.CreateGameUnitUID();
			base.CreatePoolUnit(true);
			base.CreateDynaminRespawnPoolUnit(true);
			float num = 1f;
			float num2 = 1f;
			if (base.IsPVP())
			{
				if (this.m_NKMGameData.m_NKMGameTeamDataA.m_MainShip != null && NKMGame.ApplyUpBanByGameType(this.m_NKMGameData, NKM_TEAM_TYPE.NTT_A1))
				{
					NKMUnitTemplet unitTemplet = this.m_NKMGameData.m_NKMGameTeamDataA.m_MainShip.GetUnitTemplet();
					if (unitTemplet == null)
					{
						Log.Error(string.Format("Invalid Ship Templet Data ShipId:{0}", this.m_NKMGameData.m_NKMGameTeamDataA.m_MainShip.m_UnitID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGameServerHost.cs", 335);
						return;
					}
					if (this.m_NKMGameData.IsBanShip(unitTemplet.m_UnitTempletBase.m_ShipGroupID))
					{
						int banShipLevel = this.m_NKMGameData.GetBanShipLevel(unitTemplet.m_UnitTempletBase.m_ShipGroupID);
						num = 1f - NKMUnitStatManager.m_fPercentPerBanLevel * (float)banShipLevel;
						if (num < 1f - NKMUnitStatManager.m_fMaxPercentPerBanLevel)
						{
							num = 1f - NKMUnitStatManager.m_fMaxPercentPerBanLevel;
						}
					}
				}
				if (this.m_NKMGameData.m_NKMGameTeamDataB.m_MainShip != null && NKMGame.ApplyUpBanByGameType(this.m_NKMGameData, NKM_TEAM_TYPE.NTT_B1))
				{
					NKMUnitTemplet unitTemplet2 = this.m_NKMGameData.m_NKMGameTeamDataB.m_MainShip.GetUnitTemplet();
					if (unitTemplet2 == null)
					{
						Log.Error(string.Format("Invalid Ship Templet Data ShipId:{0}", this.m_NKMGameData.m_NKMGameTeamDataB.m_MainShip.m_UnitID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGameServerHost.cs", 354);
						return;
					}
					if (this.m_NKMGameData.IsBanShip(unitTemplet2.m_UnitTempletBase.m_ShipGroupID))
					{
						int banShipLevel2 = this.m_NKMGameData.GetBanShipLevel(unitTemplet2.m_UnitTempletBase.m_ShipGroupID);
						num2 = 1f - NKMUnitStatManager.m_fPercentPerBanLevel * (float)banShipLevel2;
						if (num2 < 1f - NKMUnitStatManager.m_fMaxPercentPerBanLevel)
						{
							num2 = 1f - NKMUnitStatManager.m_fMaxPercentPerBanLevel;
						}
					}
				}
			}
			if (base.IsPVP())
			{
				if (this.m_NKMGameData.m_NKMGameTeamDataA.m_MainShip != null)
				{
					this.RespawnUnit(this.m_NKMGameData.m_NKMGameTeamDataA.m_MainShip, this.GetShipRespawnPosX(true), this.GetShipRespawnPosZ(1f), num, true, 0f);
				}
				if (this.m_NKMGameData.m_NKMGameTeamDataB.m_MainShip != null)
				{
					this.RespawnUnit(this.m_NKMGameData.m_NKMGameTeamDataB.m_MainShip, this.GetShipRespawnPosX(false), this.GetShipRespawnPosZ(1f), num2, true, 0f);
				}
			}
			else
			{
				if (this.m_NKMGameData.m_NKMGameTeamDataA.m_MainShip != null)
				{
					this.RespawnUnit(this.m_NKMGameData.m_NKMGameTeamDataA.m_MainShip, this.GetShipRespawnPosX(true), this.GetShipRespawnPosZ(1f), this.m_NKMGameData.m_NKMGameTeamDataA.m_fInitHP, false, 0f);
				}
				if (this.m_NKMGameData.m_NKMGameTeamDataB.m_MainShip != null)
				{
					if (this.m_NKMDungeonTemplet != null)
					{
						this.RespawnUnit(this.m_NKMGameData.m_NKMGameTeamDataB.m_MainShip, this.GetShipRespawnPosX(false), this.GetShipRespawnPosZ(this.m_NKMDungeonTemplet.m_fBossPosZ), this.m_NKMGameData.m_NKMGameTeamDataB.m_fInitHP, false, 0f);
					}
					else
					{
						this.RespawnUnit(this.m_NKMGameData.m_NKMGameTeamDataB.m_MainShip, this.GetShipRespawnPosX(false), this.GetShipRespawnPosZ(1f), 0f, false, 0f);
					}
				}
			}
			foreach (NKMUnitData cNKMUnitData in this.m_NKMGameData.m_NKMGameTeamDataA.m_listEnvUnitData)
			{
				this.RespawnUnit(cNKMUnitData, this.GetShipRespawnPosX(true), this.GetShipRespawnPosZ(1f), 0f, false, 0f);
			}
			foreach (NKMUnitData cNKMUnitData2 in this.m_NKMGameData.m_NKMGameTeamDataB.m_listEnvUnitData)
			{
				this.RespawnUnit(cNKMUnitData2, this.GetShipRespawnPosX(false), this.GetShipRespawnPosZ(1f), 0f, false, 0f);
			}
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x0007FA4C File Offset: 0x0007DC4C
		public NKMUnit DynamicRespawnUnitReserve(bool bMaxCountReRespawn, short gameUnitUID, float x, float z, float fJumpYPos = 0f, bool bUseRight = false, bool bRight = true, float fHPRate = 0f, string respawnState = null, float rollbackTime = 0f)
		{
			if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE != NKM_GAME_STATE.NGS_PLAY)
			{
				return null;
			}
			if (this.m_dicNKMUnitPool.ContainsKey(gameUnitUID))
			{
				NKMUnit nkmunit = this.m_dicNKMUnitPool[gameUnitUID];
				if (nkmunit != null)
				{
					NKMDynamicRespawnUnitReserve nkmdynamicRespawnUnitReserve = new NKMDynamicRespawnUnitReserve();
					nkmdynamicRespawnUnitReserve.m_GameUnitUID = gameUnitUID;
					nkmdynamicRespawnUnitReserve.m_PosX = x;
					nkmdynamicRespawnUnitReserve.m_PosZ = z;
					nkmdynamicRespawnUnitReserve.m_fJumpYPos = fJumpYPos;
					nkmdynamicRespawnUnitReserve.m_bUseRight = bUseRight;
					nkmdynamicRespawnUnitReserve.m_bRight = bRight;
					nkmdynamicRespawnUnitReserve.m_fHPRate = fHPRate;
					nkmdynamicRespawnUnitReserve.m_RespawnState = respawnState;
					nkmdynamicRespawnUnitReserve.m_fRollbackTime = rollbackTime;
					this.m_listNKMGameUnitDynamicRespawnData.Add(nkmdynamicRespawnUnitReserve);
				}
				return nkmunit;
			}
			if (bMaxCountReRespawn)
			{
				NKMUnit unit = this.GetUnit(gameUnitUID, true, false);
				if (unit != null)
				{
					unit.ProcessUnitDyingBuff();
					unit.RespawnUnit(x, z, fJumpYPos, bUseRight, bRight, fHPRate, true, rollbackTime);
					this.AddStaticBuff(unit);
					unit.SetConserveHPRate();
					this.ProcessRespawnCostReturn(unit);
					if (!string.IsNullOrEmpty(respawnState) && unit.GetUnitState(respawnState, true) != null)
					{
						unit.StateChange(respawnState, true, true);
					}
				}
				return unit;
			}
			return null;
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x0007FB40 File Offset: 0x0007DD40
		public bool DynamicRespawnUnit(short gameUnitUID, float x, float z, float fJumpYPos = 0f, bool bUseRight = false, bool bRight = true, float fHPRate = 0f, string respawnState = "", float rollbackTime = 0f)
		{
			if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE != NKM_GAME_STATE.NGS_PLAY)
			{
				return false;
			}
			if (this.m_dicNKMUnitPool.ContainsKey(gameUnitUID))
			{
				NKMUnit nkmunit = this.m_dicNKMUnitPool[gameUnitUID];
				if (nkmunit != null)
				{
					this.m_dicNKMUnitPool.Remove(gameUnitUID);
					this.m_dicNKMUnit.Add(nkmunit.GetUnitDataGame().m_GameUnitUID, nkmunit);
					this.m_listNKMUnit.Add(nkmunit);
					nkmunit.RespawnUnit(x, z, fJumpYPos, bUseRight, bRight, fHPRate, true, rollbackTime);
					this.AddStaticBuff(nkmunit);
					nkmunit.SetConserveHPRate();
					this.ProcessRespawnCostReturn(nkmunit);
					if (!string.IsNullOrEmpty(respawnState) && nkmunit.GetUnitState(respawnState, true) != null)
					{
						nkmunit.StateChange(respawnState, true, true);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x0007FBF8 File Offset: 0x0007DDF8
		public bool RespawnUnit(NKMUnitData cNKMUnitData, float x, float z, float fInitHP = 0f, bool bInitHPRate = false, float rollbackTime = 0f)
		{
			for (int i = 0; i < cNKMUnitData.m_listGameUnitUID.Count; i++)
			{
				short key = cNKMUnitData.m_listGameUnitUID[i];
				if (this.m_dicNKMUnitPool.ContainsKey(key))
				{
					NKMUnit nkmunit = this.m_dicNKMUnitPool[key];
					if (nkmunit != null)
					{
						NKMUnitTemplet unitTemplet = nkmunit.GetUnitTemplet();
						if (unitTemplet != null)
						{
							this.m_dicNKMUnitPool.Remove(key);
							this.m_dicNKMUnit.Add(nkmunit.GetUnitDataGame().m_GameUnitUID, nkmunit);
							this.m_listNKMUnit.Add(nkmunit);
							if (!unitTemplet.m_fForceRespawnXpos.IsNearlyEqual(-1f, 1E-05f))
							{
								x = this.GetRespawnPosX(base.IsATeam(nkmunit.GetUnitDataGame().m_NKM_TEAM_TYPE), unitTemplet.m_fForceRespawnXpos);
							}
							if (!unitTemplet.m_fForceRespawnZposMin.IsNearlyEqual(-1f, 1E-05f) || !unitTemplet.m_fForceRespawnZposMax.IsNearlyEqual(-1f, 1E-05f))
							{
								z = this.GetRespawnPosZ(unitTemplet.m_fForceRespawnZposMin, unitTemplet.m_fForceRespawnZposMax);
							}
							nkmunit.RespawnUnit(x, z, 0f, false, true, fInitHP, bInitHPRate, rollbackTime);
							this.AddStaticBuff(nkmunit);
							this.ProcessRespawnCostReturn(nkmunit);
						}
						if (this.m_bUseStateChangeEvent)
						{
							nkmunit.ClearAllStateChangeEvent();
							nkmunit.AddStateChangeEvent(new NKMUnit.StateChangeEvent(this.OnUnitStateChangeEvent));
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x0007FD50 File Offset: 0x0007DF50
		private void ProcessRespawnCostReturn(NKMUnit cNKMUnit)
		{
			if (cNKMUnit == null)
			{
				return;
			}
			if (cNKMUnit.m_usedRespawnCost <= 0)
			{
				return;
			}
			NKM_TEAM_TYPE nkm_TEAM_TYPE_ORG = cNKMUnit.GetUnitDataGame().m_NKM_TEAM_TYPE_ORG;
			NKMGameRuntimeTeamData myRuntimeTeamData = base.GetGameRuntimeData().GetMyRuntimeTeamData(nkm_TEAM_TYPE_ORG);
			if (base.GetGameData().GetTeamData(nkm_TEAM_TYPE_ORG).IsAssistUnit(cNKMUnit.GetUnitData().m_UnitUID))
			{
				return;
			}
			float statFinal = cNKMUnit.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_COST_RETURN_RATE);
			if (statFinal <= 0f)
			{
				return;
			}
			float num = (float)cNKMUnit.m_usedRespawnCost * statFinal;
			myRuntimeTeamData.m_fRespawnCost += num;
			cNKMUnit.m_usedRespawnCost = 0;
			this.SyncGameEvent(NKM_GAME_EVENT_TYPE.NGET_COST_RETURN, nkm_TEAM_TYPE_ORG, 0, num);
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x0007FDEC File Offset: 0x0007DFEC
		private void AddStaticBuff(NKMUnit cNKMUnit)
		{
			cNKMUnit.AddStaticBuffUnit();
			NKMUnitData unitData = cNKMUnit.GetUnitData();
			if (unitData.IsDungeonUnit())
			{
				for (int i = 0; i < unitData.m_DungeonRespawnUnitTemplet.m_listStaticBuffData.Count; i++)
				{
					NKMStaticBuffData nkmstaticBuffData = unitData.m_DungeonRespawnUnitTemplet.m_listStaticBuffData[i];
					if (cNKMUnit.CheckEventCondition(nkmstaticBuffData.m_Condition))
					{
						cNKMUnit.AddBuffByStrID(nkmstaticBuffData.m_BuffStrID, nkmstaticBuffData.m_BuffStatLevel, nkmstaticBuffData.m_BuffTimeLevel, cNKMUnit.GetUnitDataGame().m_GameUnitUID, false, false, false, 1);
					}
				}
			}
			foreach (KeyValuePair<int, int> keyValuePair in this.m_NKMGameData.m_BattleConditionIDs)
			{
				int key = keyValuePair.Key;
				int value = keyValuePair.Value;
				NKMBattleConditionTemplet templetByID = NKMBattleConditionManager.GetTempletByID(key);
				if (templetByID != null)
				{
					if (this.m_NKMGameData.IsPVP() && this.m_NKMGameData.m_NKM_GAME_TYPE != NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC)
					{
						this.AddBuffByBattleCondition(templetByID, cNKMUnit, 1, cNKMUnit.GetTeam());
					}
					else
					{
						this.AddBuffByBattleCondition(templetByID, cNKMUnit, value, NKM_TEAM_TYPE.NTT_A1);
					}
				}
			}
			this.AddStaticBuffByOperator(cNKMUnit, this.m_NKMGameData.m_NKMGameTeamDataA);
			this.AddStaticBuffByOperator(cNKMUnit, this.m_NKMGameData.m_NKMGameTeamDataB);
			NKM_DUNGEON_TYPE dungeonType = base.GetDungeonType();
			if ((dungeonType == NKM_DUNGEON_TYPE.NDT_RAID || dungeonType == NKM_DUNGEON_TYPE.NDT_SOLO_RAID) && cNKMUnit.IsATeam())
			{
				for (int j = 0; j < this.m_NKMGameData.m_lstTeamABuffStrIDListForRaid.Count; j++)
				{
					string buffStrID = this.m_NKMGameData.m_lstTeamABuffStrIDListForRaid[j];
					cNKMUnit.AddBuffByStrID(buffStrID, 1, 1, cNKMUnit.GetUnitDataGame().m_GameUnitUID, false, false, false, 1);
				}
			}
			this.AddStaticBuffByBanUnit(cNKMUnit);
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x0007FF9C File Offset: 0x0007E19C
		private void AddStaticBuffByOperator(NKMUnit cNKMUnitTarget, NKMGameTeamData cNKMGameTeamData)
		{
			if (cNKMGameTeamData == null)
			{
				return;
			}
			if (cNKMGameTeamData != null && cNKMGameTeamData.m_Operator != null && cNKMGameTeamData.m_Operator.subSkill != null)
			{
				NKMBattleConditionTemplet battleCondTemplet = cNKMGameTeamData.m_Operator.subSkill.GetBattleCondTemplet();
				this.AddBuffByBattleCondition(battleCondTemplet, cNKMUnitTarget, (int)cNKMGameTeamData.m_Operator.subSkill.level, cNKMGameTeamData.m_eNKM_TEAM_TYPE);
			}
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x0007FFF4 File Offset: 0x0007E1F4
		private void AddStaticBuffByBanUnit(NKMUnit cNKMUnitTarget)
		{
			NKMUnitData unitData = cNKMUnitTarget.GetUnitData();
			if (!base.IsPVP())
			{
				return;
			}
			NKM_GAME_TYPE nkm_GAME_TYPE = base.GetGameData().m_NKM_GAME_TYPE;
			if (nkm_GAME_TYPE != NKM_GAME_TYPE.NGT_ASYNC_PVP && nkm_GAME_TYPE - NKM_GAME_TYPE.NGT_PVP_STRATEGY > 1)
			{
				if (nkm_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC)
				{
					return;
				}
			}
			else if (cNKMUnitTarget.IsATeam())
			{
				return;
			}
			if (base.IsBanUnit(unitData.m_UnitID))
			{
				int banUnitLevel = this.m_NKMGameData.GetBanUnitLevel(unitData.m_UnitID);
				foreach (string buffStrID in this.m_NKMGameData.m_BanUnitBuffStrIDs)
				{
					cNKMUnitTarget.AddBuffByStrID(buffStrID, (byte)banUnitLevel, 1, cNKMUnitTarget.GetUnitDataGame().m_GameUnitUID, false, false, false, 1);
				}
			}
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x000800BC File Offset: 0x0007E2BC
		private void AddBuffByBattleCondition(NKMBattleConditionTemplet cNKMBattleConditionTemplet, NKMUnit cNKMUnit, int buffLevel = 1, NKM_TEAM_TYPE usingTeamType = NKM_TEAM_TYPE.NTT_A1)
		{
			if (this.IsCorrectStaticBuffTarget(cNKMBattleConditionTemplet, cNKMUnit))
			{
				NKMUnitDataGame unitDataGame = cNKMUnit.GetUnitDataGame();
				HashSet<string> buffStrIDList = cNKMBattleConditionTemplet.GetBuffStrIDList(unitDataGame.m_NKM_TEAM_TYPE, usingTeamType);
				if (buffStrIDList == null)
				{
					return;
				}
				foreach (string buffStrID in buffStrIDList)
				{
					cNKMUnit.AddBuffByStrID(buffStrID, (byte)buffLevel, (byte)buffLevel, cNKMUnit.GetUnitDataGame().m_GameUnitUID, false, false, false, 1);
				}
			}
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x00080144 File Offset: 0x0007E344
		private bool IsCorrectStaticBuffTarget(NKMBattleConditionTemplet cNKMBattleConditionTemplet, NKMUnit cNKMUnit)
		{
			if (cNKMBattleConditionTemplet == null)
			{
				return false;
			}
			NKMUnitTempletBase unitTempletBase = cNKMUnit.GetUnitTemplet().m_UnitTempletBase;
			NKM_UNIT_TYPE nkm_UNIT_TYPE = unitTempletBase.m_NKM_UNIT_TYPE;
			if (nkm_UNIT_TYPE - NKM_UNIT_TYPE.NUT_SYSTEM > 1)
			{
				return nkm_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP && (cNKMBattleConditionTemplet.AffectTeamUpID == null || cNKMBattleConditionTemplet.AffectTeamUpID.Count <= 0 || cNKMBattleConditionTemplet.AffectTeamUpID.Contains(unitTempletBase.TeamUp)) && cNKMBattleConditionTemplet.AffectSHIP;
			}
			bool flag = false | (cNKMBattleConditionTemplet.AffectCOUNTER && unitTempletBase.HasUnitStyleType(NKM_UNIT_STYLE_TYPE.NUST_COUNTER)) | (cNKMBattleConditionTemplet.AffectSOLDIER && unitTempletBase.HasUnitStyleType(NKM_UNIT_STYLE_TYPE.NUST_SOLDIER)) | (cNKMBattleConditionTemplet.AffectMECHANIC && unitTempletBase.HasUnitStyleType(NKM_UNIT_STYLE_TYPE.NUST_MECHANIC)) | (cNKMBattleConditionTemplet.AffectCORRUPT && unitTempletBase.HasUnitStyleType(NKM_UNIT_STYLE_TYPE.NUST_CORRUPTED));
			bool flag2 = cNKMBattleConditionTemplet.AffectUnitRole == NKM_UNIT_ROLE_TYPE.NURT_INVALID || unitTempletBase.m_NKM_UNIT_ROLE_TYPE == cNKMBattleConditionTemplet.AffectUnitRole;
			bool flag3 = cNKMUnit.IsAirUnit() ? cNKMBattleConditionTemplet.HitAir : cNKMBattleConditionTemplet.HitLand;
			bool result = flag && flag2 && flag3;
			if (cNKMBattleConditionTemplet.hashAffectUnitID.Contains(unitTempletBase.m_UnitID))
			{
				result = true;
			}
			if (cNKMBattleConditionTemplet.hashIgnoreUnitID.Contains(unitTempletBase.m_UnitID))
			{
				result = false;
			}
			if (cNKMBattleConditionTemplet.AffectTeamUpID.Contains(unitTempletBase.TeamUp))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x00080278 File Offset: 0x0007E478
		public virtual void Update(float deltaTime)
		{
			if (this.m_NKMGameRuntimeData.m_bPause)
			{
				return;
			}
			this.m_fDeltaTime = deltaTime * this.m_GameSpeed.GetNowValue();
			this.m_GameSpeed.Update(deltaTime);
			this.m_ObjectPool.Update();
			switch (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE)
			{
			case NKM_GAME_STATE.NGS_START:
			case NKM_GAME_STATE.NGS_FINISH:
				this.m_AbsoluteGameTime += this.m_fDeltaTime;
				this.m_NKMGameRuntimeData.m_GameTime += this.m_fDeltaTime;
				this.ProcessGameState();
				this.ProcessUnit();
				this.SyncDataPackFlush();
				return;
			case NKM_GAME_STATE.NGS_PLAY:
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
						this.m_fLastWorldStopFinishedATime = this.m_AbsoluteGameTime;
					}
				}
				base.ProcessStopTime();
				this.ProcecssWin();
				this.ProcecssValidLand(NKM_TEAM_TYPE.NTT_A1);
				this.ProcecssRespawnCost();
				this.ProcessTacticalCommand();
				this.ProcessDungeonEvent();
				this.ProcessUnit();
				if (this.m_fWorldStopTime <= 0f)
				{
					base.ProcessReAttack();
				}
				this.m_DEManager.Update(this.m_fDeltaTime);
				this.ProcessRespawn();
				this.ProcessDynamicRespawn();
				this.SyncDataPackFlush();
				return;
			default:
				return;
			}
		}

		// Token: 0x06001C3F RID: 7231
		public abstract void ProcessGameState();

		// Token: 0x06001C40 RID: 7232 RVA: 0x000803F8 File Offset: 0x0007E5F8
		protected void ProcessTacticalCommandTeam(NKMTacticalCommandData cNKMTacticalCommandData, NKMGameTeamData cNKMGameTeamData)
		{
			if (cNKMTacticalCommandData == null || cNKMGameTeamData == null)
			{
				return;
			}
			if (cNKMTacticalCommandData.m_ComboCount > 0 && cNKMTacticalCommandData.m_fComboResetCoolTimeNow <= 0f)
			{
				cNKMTacticalCommandData.m_ComboCount = 0;
				this.SyncGameEvent(NKM_GAME_EVENT_TYPE.NGET_TC_COMBO_FAIL, cNKMGameTeamData.m_eNKM_TEAM_TYPE, (int)cNKMTacticalCommandData.m_TCID, (float)cNKMTacticalCommandData.m_ComboCount);
			}
			if (!cNKMTacticalCommandData.m_bCoolTimeOn && cNKMTacticalCommandData.m_fCoolTimeNow <= 0f)
			{
				cNKMTacticalCommandData.m_bCoolTimeOn = true;
				this.SyncGameEvent(NKM_GAME_EVENT_TYPE.NGET_TC_COMBO_COOL_TIME_ON, cNKMGameTeamData.m_eNKM_TEAM_TYPE, (int)cNKMTacticalCommandData.m_TCID, 1f);
			}
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x00080478 File Offset: 0x0007E678
		protected override void ProcessTacticalCommand()
		{
			base.ProcessTacticalCommand();
			if (base.GetGameData().m_NKMGameTeamDataA != null)
			{
				for (int i = 0; i < base.GetGameData().m_NKMGameTeamDataA.m_listTacticalCommandData.Count; i++)
				{
					NKMTacticalCommandData nkmtacticalCommandData = base.GetGameData().m_NKMGameTeamDataA.m_listTacticalCommandData[i];
					if (nkmtacticalCommandData != null)
					{
						this.ProcessTacticalCommandTeam(nkmtacticalCommandData, base.GetGameData().m_NKMGameTeamDataA);
					}
				}
				if (base.GetWorldStopTime() <= 0f)
				{
					this.m_NKMReservedTacticalCommandTeamA.Update(this.m_fDeltaTime);
					if (this.m_NKMReservedTacticalCommandTeamA.CheckApplyTiming())
					{
						this.UseReservedTacticalCommand(this.m_NKMReservedTacticalCommandTeamA);
					}
				}
			}
			if (base.GetGameData().m_NKMGameTeamDataB != null)
			{
				for (int j = 0; j < base.GetGameData().m_NKMGameTeamDataB.m_listTacticalCommandData.Count; j++)
				{
					NKMTacticalCommandData nkmtacticalCommandData2 = base.GetGameData().m_NKMGameTeamDataB.m_listTacticalCommandData[j];
					if (nkmtacticalCommandData2 != null)
					{
						this.ProcessTacticalCommandTeam(nkmtacticalCommandData2, base.GetGameData().m_NKMGameTeamDataB);
					}
				}
				if (base.GetWorldStopTime() <= 0f)
				{
					this.m_NKMReservedTacticalCommandTeamB.Update(this.m_fDeltaTime);
					if (this.m_NKMReservedTacticalCommandTeamB.CheckApplyTiming())
					{
						this.UseReservedTacticalCommand(this.m_NKMReservedTacticalCommandTeamB);
					}
				}
			}
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x000805B4 File Offset: 0x0007E7B4
		private void UseReservedTacticalCommand(NKMReservedTacticalCommand cNKMReservedTacticalCommand)
		{
			if (cNKMReservedTacticalCommand == null)
			{
				return;
			}
			this.UseTacticalCommand_RealAffect(cNKMReservedTacticalCommand.GetNKMTacticalCommandTemplet(), cNKMReservedTacticalCommand.GetNKMTacticalCommandData(), cNKMReservedTacticalCommand.Get_NKM_TEAM_TYPE());
			if (cNKMReservedTacticalCommand.GetNKMTacticalCommandTemplet() != null && cNKMReservedTacticalCommand.GetNKMTacticalCommandData() != null)
			{
				this.SyncGameEvent(NKM_GAME_EVENT_TYPE.NGET_TC_COMBO_SKILL_REAL_APPLY_AFTER_SUCCESS, cNKMReservedTacticalCommand.Get_NKM_TEAM_TYPE(), (int)cNKMReservedTacticalCommand.GetNKMTacticalCommandTemplet().m_TCID, (float)cNKMReservedTacticalCommand.GetNKMTacticalCommandData().m_ComboCount);
			}
			cNKMReservedTacticalCommand.Invalidate();
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x00080618 File Offset: 0x0007E818
		protected void ProcessWinForCommon()
		{
			if (this.ProcecssWin(this.m_NKMGameData.m_NKMGameTeamDataA, this.m_NKMGameData.m_NKMGameTeamDataB) || this.ProcessTimeOut())
			{
				this.SyncGameStateChange(this.m_NKMGameRuntimeData.m_NKM_GAME_STATE, this.m_NKMGameRuntimeData.m_WinTeam, 0);
			}
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x00080668 File Offset: 0x0007E868
		protected void ProcessWinForWave()
		{
			if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_PLAY && this.CheckPossibleNextWave(this.m_NKMGameData.m_NKMGameTeamDataB))
			{
				NKMDungeonTemplet dungeonTemplet = NKMDungeonManager.GetDungeonTemplet(base.GetGameData().m_DungeonID);
				if (dungeonTemplet != null)
				{
					int nextWave = dungeonTemplet.GetNextWave(this.m_NKMGameRuntimeData.m_WaveID);
					if (!dungeonTemplet.CheckValidWave(nextWave))
					{
						this.SetGameState(NKM_GAME_STATE.NGS_FINISH);
						this.m_NKMGameRuntimeData.m_WinTeam = NKM_TEAM_TYPE.NTT_A1;
						this.SyncGameStateChange(this.m_NKMGameRuntimeData.m_NKM_GAME_STATE, this.m_NKMGameRuntimeData.m_WinTeam, this.m_NKMGameRuntimeData.m_WaveID);
						return;
					}
				}
			}
			else if (this.ProcecssWin(this.m_NKMGameData.m_NKMGameTeamDataA, this.m_NKMGameData.m_NKMGameTeamDataB))
			{
				this.SyncGameStateChange(this.m_NKMGameRuntimeData.m_NKM_GAME_STATE, this.m_NKMGameRuntimeData.m_WinTeam, this.m_NKMGameRuntimeData.m_WaveID);
			}
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x00080750 File Offset: 0x0007E950
		protected void ProcessWinForRaid()
		{
			if (this.ProcecssWin(this.m_NKMGameData.m_NKMGameTeamDataA, this.m_NKMGameData.m_NKMGameTeamDataB))
			{
				this.SyncGameStateChange(this.m_NKMGameRuntimeData.m_NKM_GAME_STATE, this.m_NKMGameRuntimeData.m_WinTeam, 0);
				return;
			}
			bool flag = true;
			if (this.m_NKMGameData.m_NKMGameTeamDataA.m_DeckData.GetListUnitDeckTombCount() < this.m_NKMGameData.m_NKMGameTeamDataA.m_listUnitData.Count)
			{
				flag = false;
			}
			if (flag)
			{
				for (int i = 0; i < this.m_listNKMGameUnitRespawnData.Count; i++)
				{
					NKMGameUnitRespawnData nkmgameUnitRespawnData = this.m_listNKMGameUnitRespawnData[i];
					if (this.m_NKMGameData.m_NKMGameTeamDataA.GetUnitDataByUnitUID(nkmgameUnitRespawnData.m_UnitUID) != null)
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				for (int j = 0; j < this.m_listNKMGameUnitDynamicRespawnData.Count; j++)
				{
					NKMDynamicRespawnUnitReserve nkmdynamicRespawnUnitReserve = this.m_listNKMGameUnitDynamicRespawnData[j];
					NKMUnit nkmunit = this.GetUnit(nkmdynamicRespawnUnitReserve.m_GameUnitUID, true, true);
					if (nkmunit != null && nkmunit.IsATeam())
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				List<NKMUnit> unitChain = this.GetUnitChain();
				for (int k = 0; k < unitChain.Count; k++)
				{
					NKMUnit nkmunit = unitChain[k];
					if (nkmunit != null && nkmunit.IsATeam() && !nkmunit.IsBoss())
					{
						flag = false;
						break;
					}
				}
			}
			if (flag || this.ProcessTimeOut())
			{
				this.SetGameState(NKM_GAME_STATE.NGS_FINISH);
				this.m_NKMGameRuntimeData.m_WinTeam = NKM_TEAM_TYPE.NTT_B1;
				this.SyncGameStateChange(this.m_NKMGameRuntimeData.m_NKM_GAME_STATE, this.m_NKMGameRuntimeData.m_WinTeam, 0);
			}
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x000808D8 File Offset: 0x0007EAD8
		protected void ProcecssWin()
		{
			NKM_DUNGEON_TYPE dungeonType = base.GetDungeonType();
			if (dungeonType == NKM_DUNGEON_TYPE.NDT_WAVE)
			{
				this.ProcessWinForWave();
				return;
			}
			if (dungeonType - NKM_DUNGEON_TYPE.NDT_RAID > 1)
			{
				this.ProcessWinForCommon();
				return;
			}
			this.ProcessWinForRaid();
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x0008090C File Offset: 0x0007EB0C
		protected virtual bool ProcessTimeOut()
		{
			if (base.GetGameData() == null)
			{
				return false;
			}
			if (base.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_DEV)
			{
				return false;
			}
			if (base.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_PRACTICE)
			{
				return false;
			}
			if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE != NKM_GAME_STATE.NGS_PLAY)
			{
				return false;
			}
			if (this.m_NKMGameData.IsPVE() && !this.m_NKMGameData.m_bLocal && this.m_NKMGameRuntimeData.m_fRemainGameTime <= 0f)
			{
				this.SetGameState(NKM_GAME_STATE.NGS_FINISH);
				this.m_NKMGameRuntimeData.m_WinTeam = NKM_TEAM_TYPE.NTT_B1;
				return true;
			}
			return false;
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x00080998 File Offset: 0x0007EB98
		public int GetUnitDieCount(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return -1;
			}
			int num = 0;
			for (int i = 0; i < unitData.m_listGameUnitUID.Count; i++)
			{
				short gameUnitUID = unitData.m_listGameUnitUID[i];
				NKMUnit unit = this.GetUnit(gameUnitUID, true, false);
				if (unit != null && (unit.IsDie() || unit.IsDying()))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x000809F4 File Offset: 0x0007EBF4
		protected bool ProcecssWin(NKMGameTeamData cNKMGameTeamDataA, NKMGameTeamData cNKMGameTeamDataB)
		{
			if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_END || this.m_NKMGameRuntimeData.m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_FINISH)
			{
				return false;
			}
			int unitDieCount = this.GetUnitDieCount(cNKMGameTeamDataA.m_MainShip);
			int unitDieCount2 = this.GetUnitDieCount(cNKMGameTeamDataB.m_MainShip);
			bool flag = false;
			bool flag2 = false;
			if (unitDieCount != -1 && unitDieCount == cNKMGameTeamDataA.m_MainShip.m_listGameUnitUID.Count)
			{
				flag = true;
			}
			if (unitDieCount2 != -1 && unitDieCount2 == cNKMGameTeamDataB.m_MainShip.m_listGameUnitUID.Count)
			{
				flag2 = true;
			}
			if (!flag && !flag2 && this.m_NKMGameData.IsPVP() && !this.m_NKMGameData.m_bLocal && this.m_NKMGameRuntimeData.m_fRemainGameTime <= 0f)
			{
				NKMUnit liveBossUnit = base.GetLiveBossUnit(true);
				NKMUnit liveBossUnit2 = base.GetLiveBossUnit(false);
				if (liveBossUnit != null && liveBossUnit2 != null)
				{
					if (liveBossUnit.GetHPRate() > liveBossUnit2.GetHPRate())
					{
						flag2 = true;
					}
					else if (liveBossUnit.GetHPRate() < liveBossUnit2.GetHPRate())
					{
						flag = true;
					}
					else
					{
						flag = true;
						flag2 = true;
					}
				}
			}
			if (flag || flag2)
			{
				this.SetGameState(NKM_GAME_STATE.NGS_FINISH);
				if (flag && flag2)
				{
					this.m_NKMGameRuntimeData.m_WinTeam = NKM_TEAM_TYPE.NTT_DRAW;
				}
				else
				{
					if (flag)
					{
						this.m_NKMGameRuntimeData.m_WinTeam = NKM_TEAM_TYPE.NTT_B1;
					}
					else if (flag2)
					{
						this.m_NKMGameRuntimeData.m_WinTeam = NKM_TEAM_TYPE.NTT_A1;
					}
					foreach (KeyValuePair<short, NKMUnit> keyValuePair in this.m_dicNKMUnit)
					{
						NKMUnit value = keyValuePair.Value;
						if (base.IsEnemy(this.m_NKMGameRuntimeData.m_WinTeam, value.GetUnitDataGame().m_NKM_TEAM_TYPE))
						{
							value.GetUnitSyncData().SetHP(0f);
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x00080B8C File Offset: 0x0007ED8C
		protected override void ProcecssRespawnCost()
		{
			base.ProcecssRespawnCost();
			if (this.m_NKMDungeonTemplet != null && !this.m_NKMDungeonTemplet.m_bCanUseAuto)
			{
				this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_bAutoRespawn = false;
			}
			if (this.m_bUseTurtlingPrevent)
			{
				float gamePlayTime = this.m_NKMGameRuntimeData.GetGamePlayTime();
				if (!this.m_bTeamAFirstRespawn && this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCost >= 10f)
				{
					if (!this.m_bTurtleWarningSentA && gamePlayTime >= NKMCommonConst.PVP_AFK_WARNING_TIME)
					{
						this.SyncGameEvent(NKM_GAME_EVENT_TYPE.NGET_AUTO_RESPAWN_WARNING, NKM_TEAM_TYPE.NTT_A1, 0, 0f);
						this.m_bTurtleWarningSentA = true;
					}
					if (gamePlayTime >= NKMCommonConst.PVP_AFK_AUTO_TIME)
					{
						this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_bAutoRespawn = true;
						this.SyncGameEvent(NKM_GAME_EVENT_TYPE.NGET_AUTO_RESPAWN_SET, NKM_TEAM_TYPE.NTT_A1, 1, 0f);
						this.m_bTeamAFirstRespawn = true;
					}
				}
				if (!this.m_bTeamBFirstRespawn && this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fRespawnCost >= 10f)
				{
					if (!this.m_bTurtleWarningSentB && gamePlayTime >= NKMCommonConst.PVP_AFK_WARNING_TIME)
					{
						this.SyncGameEvent(NKM_GAME_EVENT_TYPE.NGET_AUTO_RESPAWN_WARNING, NKM_TEAM_TYPE.NTT_B1, 0, 0f);
						this.m_bTurtleWarningSentB = true;
					}
					if (gamePlayTime >= NKMCommonConst.PVP_AFK_AUTO_TIME)
					{
						this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_bAutoRespawn = true;
						this.SyncGameEvent(NKM_GAME_EVENT_TYPE.NGET_AUTO_RESPAWN_SET, NKM_TEAM_TYPE.NTT_B1, 1, 0f);
						this.m_bTeamBFirstRespawn = true;
					}
				}
			}
			if (this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_bAutoRespawn)
			{
				this.ProcessAutoRespawn(this.m_NKMGameData.m_NKMGameTeamDataA, this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA);
				this.ProcessAutoRespawnAssist(this.m_NKMGameData.m_NKMGameTeamDataA, this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA);
			}
			if (!NKMGame.IsPVP(this.m_NKMGameData.m_NKM_GAME_TYPE))
			{
				this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_bAutoRespawn = true;
			}
			if (this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_bAutoRespawn)
			{
				this.ProcessAutoRespawn(this.m_NKMGameData.m_NKMGameTeamDataB, this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB);
				this.ProcessAutoRespawnAssist(this.m_NKMGameData.m_NKMGameTeamDataB, this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB);
			}
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x00080D7C File Offset: 0x0007EF7C
		private void GetNextAutoRespawnIndex(NKMGameTeamData cNKMGameTeamData)
		{
			this.IsThreadSafe();
			float gamePlayTime = this.m_NKMGameRuntimeData.GetGamePlayTime();
			for (int i = 0; i < cNKMGameTeamData.m_DeckData.GetListUnitDeckCount(); i++)
			{
				cNKMGameTeamData.m_DeckData.SetAutoRespawnIndex((cNKMGameTeamData.m_DeckData.GetAutoRespawnIndex() + 1) % cNKMGameTeamData.m_DeckData.GetListUnitDeckCount());
				long listUnitDeck = cNKMGameTeamData.m_DeckData.GetListUnitDeck(cNKMGameTeamData.m_DeckData.GetAutoRespawnIndex());
				NKMUnitData unitDataByUnitUID = cNKMGameTeamData.GetUnitDataByUnitUID(listUnitDeck);
				if (unitDataByUnitUID != null && base.IsGameUnitAllDie(listUnitDeck) && this.CheckRespawnable(unitDataByUnitUID, cNKMGameTeamData.m_eNKM_TEAM_TYPE, gamePlayTime))
				{
					return;
				}
			}
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x00080E14 File Offset: 0x0007F014
		private void GetNextAutoRespawnIndexAssist(NKMGameTeamData cNKMGameTeamData)
		{
			this.IsThreadSafe();
			if (cNKMGameTeamData.m_listAssistUnitData.Count == 0 || cNKMGameTeamData.m_DeckData.GetAutoRespawnIndexAssist() == -1)
			{
				return;
			}
			for (int i = 0; i < cNKMGameTeamData.m_listAssistUnitData.Count; i++)
			{
				int num = cNKMGameTeamData.m_DeckData.GetAutoRespawnIndexAssist() + 1;
				if (cNKMGameTeamData.m_listAssistUnitData.Count <= num)
				{
					num = -1;
				}
				cNKMGameTeamData.m_DeckData.SetAutoRespawnIndexAssist(num);
				if (num == -1)
				{
					break;
				}
				NKMUnitData assistUnitDataByIndex = cNKMGameTeamData.GetAssistUnitDataByIndex(cNKMGameTeamData.m_DeckData.GetAutoRespawnIndexAssist());
				if (assistUnitDataByIndex != null && assistUnitDataByIndex.m_UnitUID != 0L)
				{
					break;
				}
			}
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x00080EA4 File Offset: 0x0007F0A4
		private void ProcessAutoRespawn(NKMGameTeamData cNKMGameTeamData, NKMGameRuntimeTeamData cNKMGameRuntimeTeamData)
		{
			this.IsThreadSafe();
			if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE != NKM_GAME_STATE.NGS_PLAY)
			{
				return;
			}
			if (base.IsATeam(cNKMGameTeamData.m_eNKM_TEAM_TYPE) && base.GetDungeonTemplet() != null && base.GetDungeonTemplet().m_DungeonTempletBase.m_RespawnCountMaxSameTime > 0 && base.GetLiveUnitCountTeamA() >= base.GetDungeonTemplet().m_DungeonTempletBase.m_RespawnCountMaxSameTime)
			{
				return;
			}
			long listUnitDeck = cNKMGameTeamData.m_DeckData.GetListUnitDeck(cNKMGameTeamData.m_DeckData.GetAutoRespawnIndex());
			NKMUnitData unitDataByUnitUID = cNKMGameTeamData.GetUnitDataByUnitUID(listUnitDeck);
			if (unitDataByUnitUID != null && base.IsGameUnitAllDie(listUnitDeck) && this.CheckRespawnable(unitDataByUnitUID, cNKMGameTeamData.m_eNKM_TEAM_TYPE, this.m_NKMGameRuntimeData.GetGamePlayTime()))
			{
				this.RespawnUnit(false, 0f, 0f, cNKMGameTeamData, cNKMGameRuntimeTeamData, listUnitDeck, -1f, true);
				return;
			}
			this.GetNextAutoRespawnIndex(cNKMGameTeamData);
			this.RespawnUnit(false, 0f, 0f, cNKMGameTeamData, cNKMGameRuntimeTeamData, listUnitDeck, -1f, true);
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x00080F8C File Offset: 0x0007F18C
		private void ProcessAutoRespawnAssist(NKMGameTeamData cNKMGameTeamData, NKMGameRuntimeTeamData cNKMGameRuntimeTeamData)
		{
			this.IsThreadSafe();
			if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE != NKM_GAME_STATE.NGS_PLAY)
			{
				return;
			}
			if (cNKMGameTeamData.m_DeckData.GetAutoRespawnIndexAssist() < 0)
			{
				return;
			}
			NKMUnitData assistUnitDataByIndex = cNKMGameTeamData.GetAssistUnitDataByIndex(cNKMGameTeamData.m_DeckData.GetAutoRespawnIndexAssist());
			if (assistUnitDataByIndex != null && assistUnitDataByIndex.m_UnitUID != 0L && base.IsGameUnitAllDie(assistUnitDataByIndex.m_UnitUID))
			{
				this.RespawnUnit(false, 0f, 0f, cNKMGameTeamData, cNKMGameRuntimeTeamData, assistUnitDataByIndex.m_UnitUID, -1f, true);
			}
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x00081008 File Offset: 0x0007F208
		private void ProcessDungeonEvent()
		{
			this.ProcessDungeonEvent(this.m_listDungeonEventDataTeamA, NKM_TEAM_TYPE.NTT_A1);
			this.ProcessDungeonEvent(this.m_listDungeonEventDataTeamB, NKM_TEAM_TYPE.NTT_B1);
			this.ProcessEventRespawn();
			this.ProcessDungeonWaveEvent(this.m_NKMGameData.m_NKMGameTeamDataB);
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x0008103C File Offset: 0x0007F23C
		private bool CheckPossibleNextWave(NKMGameTeamData cNKMGameTeamData)
		{
			if (this.m_NKMGameRuntimeData.m_fRemainGameTime <= 0f)
			{
				return true;
			}
			for (int i = 0; i < cNKMGameTeamData.m_listEvevtUnitData.Count; i++)
			{
				NKMUnitData cNKMUnitData = cNKMGameTeamData.m_listEvevtUnitData[i];
				if (cNKMUnitData != null && cNKMUnitData.m_DungeonRespawnUnitTemplet != null && (cNKMUnitData.m_DungeonRespawnUnitTemplet.m_WaveID == 0 || cNKMUnitData.m_DungeonRespawnUnitTemplet.m_WaveID == this.m_NKMGameRuntimeData.m_WaveID))
				{
					if (cNKMUnitData.m_fLastRespawnTime <= 0f)
					{
						return false;
					}
					if (this.m_listNKMGameUnitRespawnData.Any((NKMGameUnitRespawnData e) => e.m_UnitUID == cNKMUnitData.m_UnitUID))
					{
						return false;
					}
					for (int j = 0; j < cNKMUnitData.m_listGameUnitUID.Count; j++)
					{
						short gameUnitUID = cNKMUnitData.m_listGameUnitUID[j];
						NKMUnit nkmunit = this.GetUnit(gameUnitUID, true, false);
						if (nkmunit != null && !nkmunit.IsDie() && !nkmunit.IsDying())
						{
							return false;
						}
					}
				}
			}
			List<NKMUnit> unitChain = this.GetUnitChain();
			for (int k = 0; k < unitChain.Count; k++)
			{
				NKMUnit nkmunit = unitChain[k];
				if (nkmunit != null && nkmunit.IsBTeam() && !nkmunit.IsDyingOrDie())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x000811A0 File Offset: 0x0007F3A0
		private void ProcessDungeonWaveEvent(NKMGameTeamData cNKMGameTeamData)
		{
			if (base.GetDungeonType() != NKM_DUNGEON_TYPE.NDT_WAVE)
			{
				return;
			}
			if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE != NKM_GAME_STATE.NGS_PLAY)
			{
				return;
			}
			if (this.CheckPossibleNextWave(cNKMGameTeamData))
			{
				NKMDungeonTemplet dungeonTemplet = NKMDungeonManager.GetDungeonTemplet(base.GetGameData().m_DungeonID);
				if (dungeonTemplet != null)
				{
					int nextWave = dungeonTemplet.GetNextWave(this.m_NKMGameRuntimeData.m_WaveID);
					if (dungeonTemplet.CheckValidWave(nextWave))
					{
						this.m_NKMGameRuntimeData.m_WaveID = nextWave;
						this.m_NKMGameRuntimeData.m_PrevWaveEndTime = this.m_NKMGameRuntimeData.m_GameTime;
						NKMDungeonWaveTemplet waveTemplet = dungeonTemplet.GetWaveTemplet(nextWave);
						if (waveTemplet != null)
						{
							foreach (NKMUnit nkmunit in this.m_listNKMUnit)
							{
								this.m_GameRecord.AddPlayTime(nkmunit, nkmunit.m_RespawnTime - this.m_NKMGameRuntimeData.m_fRemainGameTime);
								nkmunit.m_RespawnTime = waveTemplet.m_fNextWavetime;
							}
							this.m_NKMGameRuntimeData.m_fRemainGameTime = waveTemplet.m_fNextWavetime;
						}
						this.SyncGameStateChange(this.m_NKMGameRuntimeData.m_NKM_GAME_STATE, this.m_NKMGameRuntimeData.m_WinTeam, this.m_NKMGameRuntimeData.m_WaveID);
					}
				}
			}
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x000812DC File Offset: 0x0007F4DC
		private void ProcessDungeonEvent(List<NKMDungeonEventData> listNKMDungeonEventData, NKM_TEAM_TYPE eNKM_TEAM_TYPE)
		{
			for (int i = 0; i < listNKMDungeonEventData.Count; i++)
			{
				NKMDungeonEventData nkmdungeonEventData = listNKMDungeonEventData[i];
				if (nkmdungeonEventData != null)
				{
					if (nkmdungeonEventData.m_bEvokeReserved)
					{
						if (nkmdungeonEventData.m_fEventExecuteReserveTime > 0f)
						{
							if (nkmdungeonEventData.m_fEventExecuteReserveTime <= this.m_NKMGameRuntimeData.m_GameTime)
							{
								this.ExcuteDungeonEvent(nkmdungeonEventData, eNKM_TEAM_TYPE);
								nkmdungeonEventData.m_bEvokeReserved = false;
							}
						}
						else
						{
							this.ExcuteDungeonEvent(nkmdungeonEventData, eNKM_TEAM_TYPE);
							nkmdungeonEventData.m_bEvokeReserved = false;
						}
					}
					else if (nkmdungeonEventData.m_DungeonEventTemplet != null && this.CheckDungeonEventCondition(nkmdungeonEventData, eNKM_TEAM_TYPE) && this.DungeonEventTimer(nkmdungeonEventData.m_DungeonEventTemplet.m_NKMDungeonEventTiming, eNKM_TEAM_TYPE, nkmdungeonEventData.m_fEventLastStartTime, nkmdungeonEventData.m_fEventLastEndTime))
					{
						if (nkmdungeonEventData.m_DungeonEventTemplet.m_fEventDelay > 0f)
						{
							nkmdungeonEventData.m_fEventExecuteReserveTime = this.m_NKMGameRuntimeData.m_GameTime + nkmdungeonEventData.m_DungeonEventTemplet.m_fEventDelay;
							nkmdungeonEventData.m_bEvokeReserved = true;
						}
						else
						{
							this.ExcuteDungeonEvent(nkmdungeonEventData, eNKM_TEAM_TYPE);
						}
					}
				}
			}
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x000813D0 File Offset: 0x0007F5D0
		private bool CheckDungeonEventCondition(NKMDungeonEventData cNKMDungeonEventData, NKM_TEAM_TYPE eNKM_TEAM_TYPE)
		{
			switch (cNKMDungeonEventData.m_DungeonEventTemplet.m_EventCondition)
			{
			case NKM_EVENT_START_CONDITION_TYPE.NONE:
				return true;
			case NKM_EVENT_START_CONDITION_TYPE.UNIT_STATE_START:
			case NKM_EVENT_START_CONDITION_TYPE.UNIT_STATE_END:
				return false;
			case NKM_EVENT_START_CONDITION_TYPE.ENEMY_BOSS_HP_RATE_LESS:
			{
				float liveShipHPRate;
				if (base.IsATeam(eNKM_TEAM_TYPE))
				{
					liveShipHPRate = base.GetLiveShipHPRate(NKM_TEAM_TYPE.NTT_B1);
				}
				else
				{
					liveShipHPRate = base.GetLiveShipHPRate(NKM_TEAM_TYPE.NTT_A1);
				}
				return liveShipHPRate * 100f < (float)cNKMDungeonEventData.m_DungeonEventTemplet.m_EventConditionNumValue;
			}
			case NKM_EVENT_START_CONDITION_TYPE.TARGET_ALLY_UNIT_HP_RATE_LESS:
				if (base.IsSameTeam(eNKM_TEAM_TYPE, NKM_TEAM_TYPE.NTT_A1))
				{
					return this.CheckLiveUnitHPRate(cNKMDungeonEventData.EventConditionCache1, cNKMDungeonEventData.m_DungeonEventTemplet.m_EventConditionNumValue, NKM_TEAM_TYPE.NTT_A1);
				}
				return base.IsSameTeam(eNKM_TEAM_TYPE, NKM_TEAM_TYPE.NTT_B1) && this.CheckLiveUnitHPRate(cNKMDungeonEventData.EventConditionCache1, cNKMDungeonEventData.m_DungeonEventTemplet.m_EventConditionNumValue, NKM_TEAM_TYPE.NTT_B1);
			case NKM_EVENT_START_CONDITION_TYPE.TARGET_ENEMY_UNIT_HP_RATE_LESS:
				if (base.IsSameTeam(eNKM_TEAM_TYPE, NKM_TEAM_TYPE.NTT_A1))
				{
					return this.CheckLiveUnitHPRate(cNKMDungeonEventData.EventConditionCache1, cNKMDungeonEventData.m_DungeonEventTemplet.m_EventConditionNumValue, NKM_TEAM_TYPE.NTT_B1);
				}
				return base.IsSameTeam(eNKM_TEAM_TYPE, NKM_TEAM_TYPE.NTT_B1) && this.CheckLiveUnitHPRate(cNKMDungeonEventData.EventConditionCache1, cNKMDungeonEventData.m_DungeonEventTemplet.m_EventConditionNumValue, NKM_TEAM_TYPE.NTT_A1);
			case NKM_EVENT_START_CONDITION_TYPE.HAVE_SUMMON_COST:
			{
				float fRespawnCost;
				if (base.IsATeam(eNKM_TEAM_TYPE))
				{
					fRespawnCost = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCost;
				}
				else
				{
					fRespawnCost = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fRespawnCost;
				}
				return fRespawnCost >= (float)cNKMDungeonEventData.m_DungeonEventTemplet.m_EventConditionNumValue;
			}
			default:
				return false;
			}
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x00081518 File Offset: 0x0007F718
		private bool CheckLiveUnitHPRate(int unitID, int intHpRate, NKM_TEAM_TYPE team)
		{
			float num = (float)intHpRate / 100f;
			foreach (NKMUnit nkmunit in this.m_listNKMUnit)
			{
				if (nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_PLAY && nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_ENV && base.IsSameTeam(nkmunit.GetUnitDataGame().m_NKM_TEAM_TYPE, team) && nkmunit.GetUnitData().m_UnitID == unitID && nkmunit.GetHPRate() < num)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x000815C4 File Offset: 0x0007F7C4
		private void OnUnitStateChangeEvent(NKM_UNIT_STATE_CHANGE_TYPE stateChangeType, NKMUnit unit, NKMUnitState unitState)
		{
			if (unit == null || unitState == null)
			{
				return;
			}
			NKM_TEAM_TYPE nkm_TEAM_TYPE = unit.GetUnitDataGame().m_NKM_TEAM_TYPE;
			List<NKMDungeonEventData> list = null;
			if (base.IsATeam(nkm_TEAM_TYPE))
			{
				list = this.m_listDungeonEventDataTeamA;
			}
			else if (base.IsBTeam(nkm_TEAM_TYPE))
			{
				list = this.m_listDungeonEventDataTeamB;
			}
			if (list == null)
			{
				return;
			}
			for (int i = 0; i < list.Count; i++)
			{
				NKMDungeonEventData nkmdungeonEventData = list[i];
				if (!nkmdungeonEventData.m_bEvokeReserved && nkmdungeonEventData.EventConditionCache1 == unit.GetUnitData().m_UnitID)
				{
					NKMDungeonEventTemplet dungeonEventTemplet = nkmdungeonEventData.m_DungeonEventTemplet;
					if (dungeonEventTemplet != null)
					{
						if (stateChangeType != NKM_UNIT_STATE_CHANGE_TYPE.NUSCT_START)
						{
							if (stateChangeType != NKM_UNIT_STATE_CHANGE_TYPE.NUSCT_END)
							{
								goto IL_FD;
							}
							if (dungeonEventTemplet.m_EventCondition != NKM_EVENT_START_CONDITION_TYPE.UNIT_STATE_END)
							{
								goto IL_FD;
							}
						}
						else if (dungeonEventTemplet.m_EventCondition != NKM_EVENT_START_CONDITION_TYPE.UNIT_STATE_START)
						{
							goto IL_FD;
						}
						if (!(unitState.m_StateName != dungeonEventTemplet.m_EventConditionValue2) && this.DungeonEventTimer(dungeonEventTemplet.m_NKMDungeonEventTiming, nkm_TEAM_TYPE, nkmdungeonEventData.m_fEventLastStartTime, nkmdungeonEventData.m_fEventLastEndTime))
						{
							nkmdungeonEventData.m_bEvokeReserved = true;
							if (nkmdungeonEventData.m_DungeonEventTemplet.m_fEventDelay > 0f)
							{
								nkmdungeonEventData.m_fEventExecuteReserveTime = this.m_NKMGameRuntimeData.m_GameTime + nkmdungeonEventData.m_DungeonEventTemplet.m_fEventDelay;
							}
						}
					}
				}
				IL_FD:;
			}
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x000816E0 File Offset: 0x0007F8E0
		protected virtual void ExcuteDungeonEvent(NKMDungeonEventData cNKMDungeonEventData, NKM_TEAM_TYPE eNKM_TEAM_TYPE)
		{
			cNKMDungeonEventData.m_fEventLastStartTime = this.m_NKMGameRuntimeData.m_GameTime;
			cNKMDungeonEventData.m_fEventLastEndTime = this.m_NKMGameRuntimeData.m_GameTime;
			cNKMDungeonEventData.m_fEventExecuteReserveTime = 0f;
			bool flag = false;
			bool bRecordToGameData = NKMDungeonEventTemplet.IsPermanent(cNKMDungeonEventData.m_DungeonEventTemplet.m_dungeonEventType);
			switch (cNKMDungeonEventData.m_DungeonEventTemplet.m_dungeonEventType)
			{
			case NKM_EVENT_ACTION_TYPE.GAME_EVENT:
			case NKM_EVENT_ACTION_TYPE.UNLOCK_TUTORIAL_GAME_RE_RESPAWN:
			case NKM_EVENT_ACTION_TYPE.CHANGE_BGM:
			case NKM_EVENT_ACTION_TYPE.CHANGE_BGM_TRACK:
			case NKM_EVENT_ACTION_TYPE.HUD_ALERT:
			case NKM_EVENT_ACTION_TYPE.POPUP_MESSAGE:
				flag = true;
				goto IL_5CA;
			case NKM_EVENT_ACTION_TYPE.SET_ENEMY_BOSS_HP_RATE:
			{
				NKMUnit liveBossUnit;
				if (base.IsATeam(eNKM_TEAM_TYPE))
				{
					liveBossUnit = base.GetLiveBossUnit(false);
				}
				else
				{
					liveBossUnit = base.GetLiveBossUnit(true);
				}
				if (liveBossUnit != null)
				{
					liveBossUnit.SetHPRate((float)cNKMDungeonEventData.m_DungeonEventTemplet.m_EventActionValue / 100f);
					goto IL_5CA;
				}
				goto IL_5CA;
			}
			case NKM_EVENT_ACTION_TYPE.SET_UNIT_HYPER_FULL:
				flag = true;
				if (cNKMDungeonEventData.m_DungeonEventTemplet.m_EventActionValue != 0)
				{
					NKMUnit unitByUnitID = this.GetUnitByUnitID(cNKMDungeonEventData.m_DungeonEventTemplet.m_EventActionValue, true, true);
					if (unitByUnitID != null)
					{
						List<NKMAttackStateData> listHyperSkillStateData = unitByUnitID.GetUnitTemplet().m_listHyperSkillStateData;
						for (int i = 0; i < listHyperSkillStateData.Count; i++)
						{
							unitByUnitID.SetStateCoolTime(listHyperSkillStateData[i].m_StateName, false, 0f);
						}
						goto IL_5CA;
					}
					goto IL_5CA;
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
						goto IL_5CA;
					}
				}
				break;
			case NKM_EVENT_ACTION_TYPE.KILL_ALL_TAGGED_UNIT:
				goto IL_376;
			case NKM_EVENT_ACTION_TYPE.ADD_EVENTTAG:
				base.AddEventTag(cNKMDungeonEventData.m_DungeonEventTemplet.m_EventActionStrValue, cNKMDungeonEventData.m_DungeonEventTemplet.m_EventActionValue);
				goto IL_5CA;
			case NKM_EVENT_ACTION_TYPE.SET_EVENTTAG:
				base.SetEventTag(cNKMDungeonEventData.m_DungeonEventTemplet.m_EventActionStrValue, cNKMDungeonEventData.m_DungeonEventTemplet.m_EventActionValue);
				goto IL_5CA;
			case NKM_EVENT_ACTION_TYPE.NEAT_RESPAWN_COST_A_TEAM:
				flag = true;
				this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCost = (float)cNKMDungeonEventData.m_DungeonEventTemplet.m_EventActionValue;
				if (this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCost > 10f)
				{
					this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCost = 10f;
				}
				if (this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCost < 0f)
				{
					this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCost = 0f;
					goto IL_5CA;
				}
				goto IL_5CA;
			case NKM_EVENT_ACTION_TYPE.NEAT_RESPAWN_COST_B_TEAM:
				flag = true;
				this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fRespawnCost = (float)cNKMDungeonEventData.m_DungeonEventTemplet.m_EventActionValue;
				if (this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fRespawnCost > 10f)
				{
					this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fRespawnCost = 10f;
				}
				if (this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fRespawnCost < 0f)
				{
					this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fRespawnCost = 0f;
					goto IL_5CA;
				}
				goto IL_5CA;
			case NKM_EVENT_ACTION_TYPE.ADD_TEAM_A_EXTRA_RESPAWN_COST:
				this.m_NKMGameData.fExtraRespawnCostAddForA += (float)cNKMDungeonEventData.m_DungeonEventTemplet.m_EventActionValue * 0.01f;
				flag = true;
				goto IL_5CA;
			case NKM_EVENT_ACTION_TYPE.ADD_TEAM_B_EXTRA_RESPAWN_COST:
				this.m_NKMGameData.fExtraRespawnCostAddForB += (float)cNKMDungeonEventData.m_DungeonEventTemplet.m_EventActionValue * 0.01f;
				flag = true;
				goto IL_5CA;
			case NKM_EVENT_ACTION_TYPE.FORCE_WIN:
				this.ForceWin(eNKM_TEAM_TYPE, cNKMDungeonEventData.m_DungeonEventTemplet.m_EventActionValue == 0);
				goto IL_5CA;
			case NKM_EVENT_ACTION_TYPE.FORCE_LOSE:
				if (base.IsATeam(eNKM_TEAM_TYPE))
				{
					this.ForceWin(NKM_TEAM_TYPE.NTT_B1, cNKMDungeonEventData.m_DungeonEventTemplet.m_EventActionValue == 0);
					goto IL_5CA;
				}
				this.ForceWin(NKM_TEAM_TYPE.NTT_A1, cNKMDungeonEventData.m_DungeonEventTemplet.m_EventActionValue == 0);
				goto IL_5CA;
			case NKM_EVENT_ACTION_TYPE.SET_UNIT_STATE:
			case NKM_EVENT_ACTION_TYPE.SET_ENEMY_UNIT_STATE:
				break;
			default:
				goto IL_5CA;
			}
			NKM_TEAM_TYPE nkm_TEAM_TYPE = eNKM_TEAM_TYPE;
			if (cNKMDungeonEventData.m_DungeonEventTemplet.m_dungeonEventType == NKM_EVENT_ACTION_TYPE.SET_ENEMY_UNIT_STATE)
			{
				if (base.IsSameTeam(eNKM_TEAM_TYPE, NKM_TEAM_TYPE.NTT_A1))
				{
					nkm_TEAM_TYPE = NKM_TEAM_TYPE.NTT_B1;
				}
				else
				{
					nkm_TEAM_TYPE = NKM_TEAM_TYPE.NTT_A1;
				}
			}
			if (cNKMDungeonEventData.m_DungeonEventTemplet.m_EventActionValue != 0)
			{
				NKMUnit unitByUnitID2 = this.GetUnitByUnitID(cNKMDungeonEventData.m_DungeonEventTemplet.m_EventActionValue, nkm_TEAM_TYPE, true, false);
				if (unitByUnitID2 != null && !unitByUnitID2.IsDying() && !unitByUnitID2.IsDie() && unitByUnitID2.GetUnitState(cNKMDungeonEventData.m_DungeonEventTemplet.m_EventActionStrValue, true) != null)
				{
					unitByUnitID2.StateChange(cNKMDungeonEventData.m_DungeonEventTemplet.m_EventActionStrValue, true, true);
					goto IL_5CA;
				}
				goto IL_5CA;
			}
			else
			{
				using (Dictionary<short, NKMUnit>.Enumerator enumerator = this.m_dicNKMUnit.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<short, NKMUnit> keyValuePair3 = enumerator.Current;
						NKMUnit value3 = keyValuePair3.Value;
						if (value3.IsAlly(nkm_TEAM_TYPE) && !value3.IsBoss() && value3 != null && !value3.IsDying() && !value3.IsDie() && value3.GetUnitState(cNKMDungeonEventData.m_DungeonEventTemplet.m_EventActionStrValue, true) != null)
						{
							value3.StateChange(cNKMDungeonEventData.m_DungeonEventTemplet.m_EventActionStrValue, true, true);
						}
					}
					goto IL_5CA;
				}
			}
			IL_376:
			foreach (KeyValuePair<short, NKMUnit> keyValuePair4 in this.m_dicNKMUnit)
			{
				NKMUnit value4 = keyValuePair4.Value;
				if (value4.GetUnitData().m_DungeonRespawnUnitTemplet != null && value4.GetUnitData().m_DungeonRespawnUnitTemplet.m_EventRespawnUnitTag == cNKMDungeonEventData.m_DungeonEventTemplet.m_EventActionStrValue)
				{
					value4.GetUnitSyncData().SetHP(0f);
					value4.SetDying(true, false);
				}
			}
			IL_5CA:
			if (flag)
			{
				if (cNKMDungeonEventData.m_DungeonEventTemplet.m_bPause)
				{
					this.ForceSyncDataPackFlushThisFrame();
				}
				this.SyncDungeonEvent(cNKMDungeonEventData.m_DungeonEventTemplet, eNKM_TEAM_TYPE, bRecordToGameData);
			}
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x00081D04 File Offset: 0x0007FF04
		protected void ForceWin(NKM_TEAM_TYPE winTeam, bool bKillBoss)
		{
			if (base.GetDungeonType() != NKM_DUNGEON_TYPE.NDT_WAVE)
			{
				if (bKillBoss)
				{
					NKMUnit liveBossUnit = base.GetLiveBossUnit(winTeam != NKM_TEAM_TYPE.NTT_A1);
					if (liveBossUnit != null)
					{
						liveBossUnit.SetHPRate(0f);
						return;
					}
				}
				else
				{
					this.SetGameState(NKM_GAME_STATE.NGS_FINISH);
					this.m_NKMGameRuntimeData.m_WinTeam = winTeam;
					this.SyncGameStateChange(this.m_NKMGameRuntimeData.m_NKM_GAME_STATE, this.m_NKMGameRuntimeData.m_WinTeam, 0);
				}
				return;
			}
			if (winTeam == NKM_TEAM_TYPE.NTT_A1)
			{
				this.m_NKMGameRuntimeData.m_WaveID = 0;
				base.AllKill(NKM_TEAM_TYPE.NTT_B1);
				return;
			}
			this.m_NKMGameRuntimeData.m_WaveID = 0;
			this.m_NKMGameRuntimeData.m_fRemainGameTime = 0f;
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x00081DA0 File Offset: 0x0007FFA0
		private void ProcessEventRespawn()
		{
			this.ProcessEventRespawn(this.m_NKMGameData.m_NKMGameTeamDataA);
			this.ProcessEventRespawn(this.m_NKMGameData.m_NKMGameTeamDataB);
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x00081DC4 File Offset: 0x0007FFC4
		protected bool CheckRespawnable(NKMUnitData cNKMUnitData, NKM_TEAM_TYPE teamType, float gamePlayTime)
		{
			NKMDungeonRespawnUnitTemplet dungeonRespawnUnitTemplet = cNKMUnitData.m_DungeonRespawnUnitTemplet;
			return dungeonRespawnUnitTemplet == null || this.DungeonEventTimer(dungeonRespawnUnitTemplet.m_NKMDungeonEventTiming, teamType, cNKMUnitData.m_fLastRespawnTime, cNKMUnitData.m_fLastDieTime);
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x00081DF8 File Offset: 0x0007FFF8
		protected bool DungeonEventTimer(NKMDungeonEventTiming cNKMDungeonEventTiming, NKM_TEAM_TYPE eNKM_TEAM_TYPE, float fLastEventTime, float fLastEventEndTime)
		{
			if (this.m_NKMGameRuntimeData.m_PrevWaveEndTime > 0f)
			{
				if (!cNKMDungeonEventTiming.EventTimeCheck(this.m_NKMGameRuntimeData.m_GameTime - this.m_NKMGameRuntimeData.m_PrevWaveEndTime))
				{
					return false;
				}
			}
			else if (!cNKMDungeonEventTiming.EventTimeCheck(this.m_NKMGameRuntimeData.GetGamePlayTime()))
			{
				return false;
			}
			if (cNKMDungeonEventTiming.m_fEventBossHPLess > 0f && base.GetLiveShipHPRate(eNKM_TEAM_TYPE) >= cNKMDungeonEventTiming.m_fEventBossHPLess)
			{
				return false;
			}
			if (cNKMDungeonEventTiming.m_fEventBossHPUpper > 0f && base.GetLiveShipHPRate(eNKM_TEAM_TYPE) <= cNKMDungeonEventTiming.m_fEventBossHPUpper)
			{
				return false;
			}
			NKMUnit liveBossUnit = base.GetLiveBossUnit(eNKM_TEAM_TYPE);
			if (liveBossUnit != null && cNKMDungeonEventTiming.m_fEventBossHPLess > 0f && cNKMDungeonEventTiming.m_fEventIgnoreBossInitHPLess && cNKMDungeonEventTiming.m_NKM_DUNGEON_EVENT_TYPE == NKM_DUNGEON_EVENT_TYPE.NDET_ONE_TIME && liveBossUnit.GetUnitFrameData().m_fInitHP <= liveBossUnit.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP) * cNKMDungeonEventTiming.m_fEventBossHPLess)
			{
				return false;
			}
			if (cNKMDungeonEventTiming.m_EventDieUnitTagCount > 0 && cNKMDungeonEventTiming.m_EventDieUnitTagCount > base.GetDieUnitRespawnTag(cNKMDungeonEventTiming.m_EventDieUnitTag))
			{
				return false;
			}
			if (cNKMDungeonEventTiming.m_EventDieDeckTagCount > 0 && cNKMDungeonEventTiming.m_EventDieDeckTagCount > base.GetDieDeckRespawnTag(cNKMDungeonEventTiming.m_EventDieDeckTag))
			{
				return false;
			}
			if (cNKMDungeonEventTiming.m_EventTagCount > 0 && cNKMDungeonEventTiming.m_EventTagCount > base.GetEventTag(cNKMDungeonEventTiming.m_EventTag))
			{
				return false;
			}
			if (cNKMDungeonEventTiming.m_EventLiveDeckTag.Length > 0 && base.GetDieDeckRespawnTag(cNKMDungeonEventTiming.m_EventLiveDeckTag) > 0)
			{
				return false;
			}
			if (this.m_NKMGameData.IsPVE())
			{
				if (cNKMDungeonEventTiming.m_EventDieWarfareDungeonTag.Length > 1 && this.CheckWarfareDungeonExist(cNKMDungeonEventTiming.m_EventDieWarfareDungeonTag))
				{
					return false;
				}
				if (cNKMDungeonEventTiming.m_EventLiveWarfareDungeonTag.Length > 1 && !this.CheckWarfareDungeonExist(cNKMDungeonEventTiming.m_EventLiveWarfareDungeonTag))
				{
					return false;
				}
			}
			switch (cNKMDungeonEventTiming.m_NKM_DUNGEON_EVENT_TYPE)
			{
			case NKM_DUNGEON_EVENT_TYPE.NDET_ONE_TIME:
				if (fLastEventTime >= 0f)
				{
					return false;
				}
				break;
			case NKM_DUNGEON_EVENT_TYPE.NDET_TIME:
				if (this.m_NKMGameRuntimeData.m_GameTime - fLastEventTime < cNKMDungeonEventTiming.m_fEventTimeGap && fLastEventTime > 0f)
				{
					return false;
				}
				break;
			case NKM_DUNGEON_EVENT_TYPE.NDET_DIE_AFTER_TIME:
				if (this.m_NKMGameRuntimeData.m_GameTime - fLastEventEndTime < cNKMDungeonEventTiming.m_fEventTimeGap && fLastEventTime > 0f)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x00082009 File Offset: 0x00080209
		protected virtual bool CheckWarfareDungeonExist(string dungeonTag)
		{
			return false;
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x0008200C File Offset: 0x0008020C
		private void ProcessEventRespawn(NKMGameTeamData cNKMGameTeamData)
		{
			if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE != NKM_GAME_STATE.NGS_PLAY)
			{
				return;
			}
			for (int i = 0; i < cNKMGameTeamData.m_listEvevtUnitData.Count; i++)
			{
				NKMUnitData nkmunitData = cNKMGameTeamData.m_listEvevtUnitData[i];
				if (nkmunitData != null && nkmunitData.m_DungeonRespawnUnitTemplet != null)
				{
					bool flag = false;
					for (int j = 0; j < this.m_listNKMGameUnitRespawnData.Count; j++)
					{
						if (this.m_listNKMGameUnitRespawnData[j].m_UnitUID == nkmunitData.m_UnitUID)
						{
							flag = true;
							break;
						}
					}
					if (!flag && (nkmunitData.m_DungeonRespawnUnitTemplet.m_WaveID == 0 || nkmunitData.m_DungeonRespawnUnitTemplet.m_WaveID == this.m_NKMGameRuntimeData.m_WaveID) && base.IsGameUnitAllDie(nkmunitData.m_UnitUID) && this.DungeonEventTimer(nkmunitData.m_DungeonRespawnUnitTemplet.m_NKMDungeonEventTiming, cNKMGameTeamData.m_eNKM_TEAM_TYPE, nkmunitData.m_fLastRespawnTime, nkmunitData.m_fLastDieTime))
					{
						NKMUnitTemplet unitTemplet = NKMUnitManager.GetUnitTemplet(nkmunitData.m_UnitID);
						if (unitTemplet != null)
						{
							for (int k = 0; k < unitTemplet.m_StatTemplet.m_RespawnCount; k++)
							{
								NKMGameUnitRespawnData nkmgameUnitRespawnData = (NKMGameUnitRespawnData)this.m_ObjectPool.OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKMGameUnitRespawnData, "", "", false);
								nkmgameUnitRespawnData.m_UnitUID = nkmunitData.m_UnitUID;
								nkmgameUnitRespawnData.m_fRespawnCoolTime = nkmunitData.m_DungeonRespawnUnitTemplet.m_fRespawnCoolTime + unitTemplet.m_fRespawnCoolTime * (float)k;
								nkmgameUnitRespawnData.m_fRollbackTime = 0f;
								if (base.IsATeam(cNKMGameTeamData.m_eNKM_TEAM_TYPE))
								{
									nkmgameUnitRespawnData.m_fRespawnPosX = this.m_NKMMapTemplet.m_fMinX + (this.m_NKMMapTemplet.m_fMaxX - this.m_NKMMapTemplet.m_fMinX) * nkmunitData.m_DungeonRespawnUnitTemplet.m_NKMDungeonEventTiming.m_fEventPos;
								}
								else
								{
									nkmgameUnitRespawnData.m_fRespawnPosX = this.m_NKMMapTemplet.m_fMaxX - (this.m_NKMMapTemplet.m_fMaxX - this.m_NKMMapTemplet.m_fMinX) * nkmunitData.m_DungeonRespawnUnitTemplet.m_NKMDungeonEventTiming.m_fEventPos;
								}
								this.m_listNKMGameUnitRespawnData.Add(nkmgameUnitRespawnData);
							}
							nkmunitData.m_fLastRespawnTime = this.m_NKMGameRuntimeData.m_GameTime;
						}
					}
				}
			}
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x00082230 File Offset: 0x00080430
		private bool RespawnUnit(bool bCheckOnly, float fBaseCoolTime, float rollbackTime, NKMGameTeamData cNKMGameTeamData, NKMGameRuntimeTeamData cNKMGameRuntimeTeamData, long unitUID, float fRespawnPosX = -1f, bool bAutoRespawn = false)
		{
			if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE != NKM_GAME_STATE.NGS_PLAY)
			{
				return false;
			}
			NKMUnitData unitDataByUnitUID = cNKMGameTeamData.GetUnitDataByUnitUID(unitUID);
			if (unitDataByUnitUID == null)
			{
				return false;
			}
			NKMUnitTemplet unitTemplet = NKMUnitManager.GetUnitTemplet(unitDataByUnitUID.m_UnitID);
			if (unitTemplet == null)
			{
				return false;
			}
			if (!this.CheckRespawnable(unitDataByUnitUID, cNKMGameTeamData.m_eNKM_TEAM_TYPE, this.m_NKMGameRuntimeData.GetGamePlayTime()))
			{
				return false;
			}
			for (int i = 0; i < this.m_listNKMGameUnitRespawnData.Count; i++)
			{
				if (this.m_listNKMGameUnitRespawnData[i].m_UnitUID == unitUID)
				{
					return false;
				}
			}
			int num;
			if (cNKMGameTeamData.m_LeaderUnitUID == unitUID)
			{
				num = Math.Max(0, base.GetRespawnCost(unitTemplet.m_StatTemplet, true, cNKMGameTeamData.m_eNKM_TEAM_TYPE));
			}
			else
			{
				num = base.GetRespawnCost(unitTemplet.m_StatTemplet, false, cNKMGameTeamData.m_eNKM_TEAM_TYPE);
			}
			if (!bCheckOnly && !base.IsGameUnitAllDie(unitUID))
			{
				return false;
			}
			if (cNKMGameTeamData.IsAssistUnit(unitUID))
			{
				if (cNKMGameRuntimeTeamData.m_fRespawnCostAssist < (float)num)
				{
					return false;
				}
				if (bCheckOnly)
				{
					return true;
				}
				for (int j = 0; j < unitTemplet.m_StatTemplet.m_RespawnCount; j++)
				{
					NKMGameUnitRespawnData nkmgameUnitRespawnData = (NKMGameUnitRespawnData)this.m_ObjectPool.OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKMGameUnitRespawnData, "", "", false);
					nkmgameUnitRespawnData.m_UnitUID = unitUID;
					float num2 = unitTemplet.m_fRespawnCoolTime * (float)j - rollbackTime;
					if (num2 >= 0f)
					{
						nkmgameUnitRespawnData.m_fRespawnCoolTime = fBaseCoolTime + num2;
						nkmgameUnitRespawnData.m_fRollbackTime = 0f;
					}
					else
					{
						nkmgameUnitRespawnData.m_fRespawnCoolTime = fBaseCoolTime;
						nkmgameUnitRespawnData.m_fRollbackTime = -num2;
					}
					if (fRespawnPosX.IsNearlyEqual(-1f, 1E-05f) && unitDataByUnitUID.m_DungeonRespawnUnitTemplet != null)
					{
						if (base.IsATeam(cNKMGameTeamData.m_eNKM_TEAM_TYPE))
						{
							nkmgameUnitRespawnData.m_fRespawnPosX = this.m_NKMMapTemplet.m_fMinX + (this.m_NKMMapTemplet.m_fMaxX - this.m_NKMMapTemplet.m_fMinX) * unitDataByUnitUID.m_DungeonRespawnUnitTemplet.m_NKMDungeonEventTiming.m_fEventPos;
						}
						else
						{
							nkmgameUnitRespawnData.m_fRespawnPosX = this.m_NKMMapTemplet.m_fMaxX - (this.m_NKMMapTemplet.m_fMaxX - this.m_NKMMapTemplet.m_fMinX) * unitDataByUnitUID.m_DungeonRespawnUnitTemplet.m_NKMDungeonEventTiming.m_fEventPos;
						}
					}
					else
					{
						nkmgameUnitRespawnData.m_fRespawnPosX = fRespawnPosX;
					}
					this.m_listNKMGameUnitRespawnData.Add(nkmgameUnitRespawnData);
				}
				if (bAutoRespawn)
				{
					this.GetNextAutoRespawnIndexAssist(cNKMGameTeamData);
				}
				cNKMGameRuntimeTeamData.m_fRespawnCostAssist = 0f;
				this.SyncDeckChangeAssist(cNKMGameTeamData.m_eNKM_TEAM_TYPE, rollbackTime, cNKMGameTeamData.m_DeckData.GetAutoRespawnIndexAssist());
				if (this.m_dicRespawnUnitUID_ByDeckUsed != null && !this.m_dicRespawnUnitUID_ByDeckUsed.ContainsKey(unitUID))
				{
					this.m_dicRespawnUnitUID_ByDeckUsed.Add(unitUID, num);
				}
				return true;
			}
			else
			{
				if (cNKMGameRuntimeTeamData.m_fRespawnCost < (float)num)
				{
					return false;
				}
				if (bCheckOnly)
				{
					return true;
				}
				for (int k = 0; k < unitTemplet.m_StatTemplet.m_RespawnCount; k++)
				{
					NKMGameUnitRespawnData nkmgameUnitRespawnData2 = (NKMGameUnitRespawnData)this.m_ObjectPool.OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKMGameUnitRespawnData, "", "", false);
					nkmgameUnitRespawnData2.m_UnitUID = unitUID;
					nkmgameUnitRespawnData2.m_fRespawnPosX = fRespawnPosX;
					nkmgameUnitRespawnData2.m_eNKM_TEAM_TYPE = cNKMGameTeamData.m_eNKM_TEAM_TYPE;
					float num3 = unitTemplet.m_fRespawnCoolTime * (float)k - rollbackTime;
					if (num3 >= 0f)
					{
						nkmgameUnitRespawnData2.m_fRespawnCoolTime = fBaseCoolTime + num3;
						nkmgameUnitRespawnData2.m_fRollbackTime = 0f;
					}
					else
					{
						nkmgameUnitRespawnData2.m_fRespawnCoolTime = fBaseCoolTime;
						nkmgameUnitRespawnData2.m_fRollbackTime = -num3;
					}
					if (fRespawnPosX.IsNearlyEqual(-1f, 1E-05f) && unitDataByUnitUID.m_DungeonRespawnUnitTemplet != null)
					{
						if (base.IsATeam(cNKMGameTeamData.m_eNKM_TEAM_TYPE))
						{
							nkmgameUnitRespawnData2.m_fRespawnPosX = this.m_NKMMapTemplet.m_fMinX + (this.m_NKMMapTemplet.m_fMaxX - this.m_NKMMapTemplet.m_fMinX) * unitDataByUnitUID.m_DungeonRespawnUnitTemplet.m_NKMDungeonEventTiming.m_fEventPos;
						}
						else
						{
							nkmgameUnitRespawnData2.m_fRespawnPosX = this.m_NKMMapTemplet.m_fMaxX - (this.m_NKMMapTemplet.m_fMaxX - this.m_NKMMapTemplet.m_fMinX) * unitDataByUnitUID.m_DungeonRespawnUnitTemplet.m_NKMDungeonEventTiming.m_fEventPos;
						}
					}
					else
					{
						nkmgameUnitRespawnData2.m_fRespawnPosX = fRespawnPosX;
					}
					this.m_listNKMGameUnitRespawnData.Add(nkmgameUnitRespawnData2);
				}
				if (bAutoRespawn)
				{
					this.GetNextAutoRespawnIndex(cNKMGameTeamData);
				}
				if (base.IsATeam(cNKMGameTeamData.m_eNKM_TEAM_TYPE))
				{
					for (int l = 0; l < this.m_listNKMGameUnitRespawnData.Count; l++)
					{
						NKMGameUnitRespawnData nkmgameUnitRespawnData3 = this.m_listNKMGameUnitRespawnData[l];
						if (nkmgameUnitRespawnData3 != null && !this.m_liveUnitUID.Contains(nkmgameUnitRespawnData3.m_UnitUID))
						{
							this.m_liveUnitUID.Add(nkmgameUnitRespawnData3.m_UnitUID);
						}
					}
				}
				this.UseDeck(cNKMGameTeamData, unitUID, rollbackTime);
				cNKMGameRuntimeTeamData.m_fRespawnCost -= (float)num;
				cNKMGameRuntimeTeamData.m_fUsedRespawnCost += (float)num;
				cNKMGameRuntimeTeamData.m_respawn_count++;
				if (unitDataByUnitUID.m_listGameUnitUID.Count > 0)
				{
					short gameUnitUID = unitDataByUnitUID.m_listGameUnitUID[0];
					NKMUnit unit = this.GetUnit(gameUnitUID, true, true);
					if (unit != null)
					{
						unit.m_usedRespawnCost = num;
					}
				}
				if (this.m_dicRespawnUnitUID_ByDeckUsed != null && !this.m_dicRespawnUnitUID_ByDeckUsed.ContainsKey(unitUID))
				{
					this.m_dicRespawnUnitUID_ByDeckUsed.Add(unitUID, num);
				}
				return true;
			}
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x0008273C File Offset: 0x0008093C
		protected void AddComboByDeckUnitRespawn(NKMGameTeamData cNKMGameTeamData, NKMGameRuntimeTeamData cNKMGameRuntimeTeamData, int unitID, int respawnCost)
		{
			NKMUnitTemplet unitTemplet = NKMUnitManager.GetUnitTemplet(unitID);
			if (cNKMGameTeamData == null || unitTemplet == null || unitTemplet.m_UnitTempletBase == null)
			{
				return;
			}
			NKMTacticalCommandData tc_Combo = cNKMGameTeamData.GetTC_Combo();
			if (tc_Combo == null)
			{
				return;
			}
			if (tc_Combo.m_fCoolTimeNow > 0f)
			{
				return;
			}
			NKMTacticalCommandTemplet tacticalCommandTempletByID = NKMTacticalCommandManager.GetTacticalCommandTempletByID((int)tc_Combo.m_TCID);
			if (tacticalCommandTempletByID != null && (int)tc_Combo.m_ComboCount < tacticalCommandTempletByID.m_listComboType.Count)
			{
				NKMTacticalCombo nkmtacticalCombo = tacticalCommandTempletByID.m_listComboType[(int)tc_Combo.m_ComboCount];
				if (nkmtacticalCombo != null && nkmtacticalCombo.CheckCond(unitTemplet.m_UnitTempletBase, respawnCost))
				{
					tc_Combo.AddComboCount();
					if (tacticalCommandTempletByID.m_listComboType.Count <= (int)tc_Combo.m_ComboCount)
					{
						this.UseTacticalCommand(tacticalCommandTempletByID, tc_Combo, cNKMGameRuntimeTeamData, cNKMGameTeamData.m_eNKM_TEAM_TYPE, NKMCommonConst.OPERATOR_SKILL_DELAY_START_TIME);
						return;
					}
					tc_Combo.m_fComboResetCoolTimeNow = tacticalCommandTempletByID.m_fComboResetCoolTime;
					this.SyncGameEvent(NKM_GAME_EVENT_TYPE.NGET_TC_COMBO_SUCCESS, cNKMGameTeamData.m_eNKM_TEAM_TYPE, (int)tacticalCommandTempletByID.m_TCID, (float)tc_Combo.m_ComboCount);
				}
			}
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x00082818 File Offset: 0x00080A18
		protected long UseDeck(NKMGameTeamData cNKMGameTeamData, long unitUID, float rollbackTime)
		{
			long deckTombAddUnitUID = -1L;
			bool flag = true;
			if (base.GetDungeonTemplet() != null && !base.GetDungeonTemplet().m_DungeonTempletBase.m_bDeckReuse)
			{
				flag = false;
			}
			if (base.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_RAID || base.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_RAID_SOLO || !flag)
			{
				cNKMGameTeamData.m_DeckData.AddListUnitDeckTomb(unitUID);
				deckTombAddUnitUID = unitUID;
			}
			else
			{
				cNKMGameTeamData.m_DeckData.AddListUnitDeckUsed(unitUID);
			}
			int i;
			for (i = 0; i < cNKMGameTeamData.m_DeckData.GetListUnitDeckCount(); i++)
			{
				if (cNKMGameTeamData.m_DeckData.GetListUnitDeck(i) == unitUID)
				{
					cNKMGameTeamData.m_DeckData.SetListUnitDeck(i, 0L);
					break;
				}
			}
			long num = 0L;
			if (cNKMGameTeamData.m_DeckData.GetNextDeck() != 0L)
			{
				num = cNKMGameTeamData.m_DeckData.GetNextDeck();
				cNKMGameTeamData.m_DeckData.SetListUnitDeck(i, num);
				cNKMGameTeamData.m_DeckData.SetNextDeck(0L);
			}
			int num2 = -1;
			int j;
			for (j = 0; j < cNKMGameTeamData.m_DeckData.GetListUnitDeckUsedCount(); j++)
			{
				long listUnitDeckUsed = cNKMGameTeamData.m_DeckData.GetListUnitDeckUsed(j);
				if (listUnitDeckUsed > 0L && base.IsGameUnitAllDie(listUnitDeckUsed))
				{
					num2 = j;
					break;
				}
				if (listUnitDeckUsed > 0L)
				{
					num2 = j;
					break;
				}
			}
			if (num2 >= 0)
			{
				long listUnitDeckUsed2 = cNKMGameTeamData.m_DeckData.GetListUnitDeckUsed(num2);
				if (cNKMGameTeamData.m_DeckData.GetListUnitDeck(i) == 0L)
				{
					cNKMGameTeamData.m_DeckData.SetListUnitDeck(i, listUnitDeckUsed2);
					num = listUnitDeckUsed2;
				}
				else
				{
					cNKMGameTeamData.m_DeckData.SetNextDeck(listUnitDeckUsed2);
				}
				cNKMGameTeamData.m_DeckData.RemoveAtListUnitDeckUsed(num2);
			}
			if (num == unitUID)
			{
				unitUID = -1L;
				j = -1;
			}
			this.SyncDeckChange(cNKMGameTeamData.m_eNKM_TEAM_TYPE, rollbackTime, i, num, unitUID, j, deckTombAddUnitUID, cNKMGameTeamData.m_DeckData.GetAutoRespawnIndex(), cNKMGameTeamData.m_DeckData.GetNextDeck());
			return num;
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x000829C4 File Offset: 0x00080BC4
		private void ProcessRespawn()
		{
			this.ProcessRespawn(this.m_NKMGameData.m_NKMGameTeamDataA, true);
			this.ProcessRespawn(this.m_NKMGameData.m_NKMGameTeamDataB, false);
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x000829EC File Offset: 0x00080BEC
		private void ProcessRespawn(NKMGameTeamData cNKMGameTeamData, bool bTeamA)
		{
			if (base.GetWorldStopTime() > 0f)
			{
				return;
			}
			if (base.GetDungeonTemplet() != null && base.GetDungeonTemplet().m_bNoEnemyRespawnBeforeUserFirstRespawn && !bTeamA && !this.m_bTeamAFirstRespawn)
			{
				return;
			}
			for (int i = 0; i < this.m_listNKMGameUnitRespawnData.Count; i++)
			{
				NKMGameUnitRespawnData nkmgameUnitRespawnData = this.m_listNKMGameUnitRespawnData[i];
				if (nkmgameUnitRespawnData != null)
				{
					nkmgameUnitRespawnData.m_fRespawnCoolTime -= this.m_fDeltaTime;
					if (nkmgameUnitRespawnData.m_fRespawnCoolTime < 0f)
					{
						nkmgameUnitRespawnData.m_fRespawnCoolTime = 0f;
						NKMUnitData unitDataByUnitUID = cNKMGameTeamData.GetUnitDataByUnitUID(nkmgameUnitRespawnData.m_UnitUID);
						if (unitDataByUnitUID != null)
						{
							float num = nkmgameUnitRespawnData.m_fRespawnPosX;
							if (num.IsNearlyEqual(-1f, 1E-05f))
							{
								num = this.GetRespawnPosX(bTeamA, -1f);
							}
							if (this.RespawnUnit(unitDataByUnitUID, num, this.GetRespawnPosZ(-1f, -1f), 0f, false, nkmgameUnitRespawnData.m_fRollbackTime))
							{
								if (bTeamA)
								{
									this.m_bTeamAFirstRespawn = true;
								}
								else
								{
									this.m_bTeamBFirstRespawn = true;
								}
								this.ProcessRespawnCombo(unitDataByUnitUID, cNKMGameTeamData);
								this.m_ObjectPool.CloseObj(nkmgameUnitRespawnData);
								this.m_listNKMGameUnitRespawnData.RemoveAt(i);
								return;
							}
						}
					}
				}
			}
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x00082B18 File Offset: 0x00080D18
		protected void ProcessRespawnCombo(NKMUnitData cNewUnitData, NKMGameTeamData cNKMGameTeamData)
		{
			if (cNewUnitData == null)
			{
				return;
			}
			if (cNKMGameTeamData == null)
			{
				return;
			}
			if (this.m_dicRespawnUnitUID_ByDeckUsed == null)
			{
				return;
			}
			NKMGameRuntimeTeamData myRuntimeTeamData = this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(cNKMGameTeamData.m_eNKM_TEAM_TYPE);
			int respawnCost;
			if (myRuntimeTeamData != null && this.m_dicRespawnUnitUID_ByDeckUsed.TryGetValue(cNewUnitData.m_UnitUID, out respawnCost))
			{
				this.AddComboByDeckUnitRespawn(cNKMGameTeamData, myRuntimeTeamData, cNewUnitData.m_UnitID, respawnCost);
				this.m_dicRespawnUnitUID_ByDeckUsed.Remove(cNewUnitData.m_UnitUID);
			}
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x00082B84 File Offset: 0x00080D84
		private void ProcessDynamicRespawn()
		{
			for (int i = 0; i < this.m_listNKMGameUnitDynamicRespawnData.Count; i++)
			{
				NKMDynamicRespawnUnitReserve nkmdynamicRespawnUnitReserve = this.m_listNKMGameUnitDynamicRespawnData[i];
				this.DynamicRespawnUnit(nkmdynamicRespawnUnitReserve.m_GameUnitUID, nkmdynamicRespawnUnitReserve.m_PosX, nkmdynamicRespawnUnitReserve.m_PosZ, nkmdynamicRespawnUnitReserve.m_fJumpYPos, nkmdynamicRespawnUnitReserve.m_bUseRight, nkmdynamicRespawnUnitReserve.m_bRight, nkmdynamicRespawnUnitReserve.m_fHPRate, nkmdynamicRespawnUnitReserve.m_RespawnState, nkmdynamicRespawnUnitReserve.m_fRollbackTime);
			}
			this.m_listNKMGameUnitDynamicRespawnData.Clear();
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x00082BFC File Offset: 0x00080DFC
		private float GetShipRespawnPosX(bool bTeamA)
		{
			if (bTeamA)
			{
				return this.m_NKMMapTemplet.m_fMinX;
			}
			return this.m_NKMMapTemplet.m_fMaxX;
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x00082C18 File Offset: 0x00080E18
		private float GetShipRespawnPosZ(float fRate = 1f)
		{
			return this.m_NKMMapTemplet.m_fMinZ + (this.m_NKMMapTemplet.m_fMaxZ - this.m_NKMMapTemplet.m_fMinZ) * fRate;
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x00082C40 File Offset: 0x00080E40
		private float GetRespawnPosX(bool bTeamA, float fFactor = -1f)
		{
			float num = base.GetGameData().IsPVP() ? NKMCommonConst.PVP_SUMMON_MIN_POS : NKMCommonConst.PVE_SUMMON_MIN_POS;
			if (bTeamA)
			{
				if (fFactor.IsNearlyEqual(-1f, 1E-05f))
				{
					return this.m_NKMMapTemplet.m_fMinX + num;
				}
				float num2 = this.m_NKMMapTemplet.m_fMaxX - this.m_NKMMapTemplet.m_fMinX;
				return NKMMathf.Max(this.m_NKMMapTemplet.m_fMinX + num2 * fFactor, this.m_NKMMapTemplet.m_fMinX + num);
			}
			else
			{
				if (fFactor.IsNearlyEqual(-1f, 1E-05f))
				{
					return this.m_NKMMapTemplet.m_fMaxX - num;
				}
				float num3 = this.m_NKMMapTemplet.m_fMaxX - this.m_NKMMapTemplet.m_fMinX;
				return NKMMathf.Min(this.m_NKMMapTemplet.m_fMaxX - num3 * fFactor, this.m_NKMMapTemplet.m_fMaxX - num);
			}
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x00082D20 File Offset: 0x00080F20
		public float GetRespawnPosZ(float fFactorMin = -1f, float fFactorMax = -1f)
		{
			if (fFactorMin.IsNearlyEqual(-1f, 1E-05f) && fFactorMax.IsNearlyEqual(-1f, 1E-05f))
			{
				return NKMRandom.Range(this.m_NKMMapTemplet.m_fMinZ, this.m_NKMMapTemplet.m_fMaxZ);
			}
			if (fFactorMin.IsNearlyEqual(-1f, 1E-05f))
			{
				fFactorMin = 0f;
			}
			else if (fFactorMin < 0f)
			{
				fFactorMin = 0f;
			}
			else if (fFactorMin > 1f)
			{
				fFactorMin = 1f;
			}
			if (fFactorMax.IsNearlyEqual(-1f, 1E-05f))
			{
				fFactorMax = 1f;
			}
			else if (fFactorMax < 0f)
			{
				fFactorMax = 0f;
			}
			else if (fFactorMax > 1f)
			{
				fFactorMax = 1f;
			}
			if (fFactorMin > fFactorMax)
			{
				fFactorMin = fFactorMax;
				Log.Error("GetRespawnPosZ fFactorMin > fFactorMax ", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGameServerHost.cs", 2617);
			}
			float num = NKMRandom.Range(fFactorMin, fFactorMax);
			float num2 = this.m_NKMMapTemplet.m_fMaxZ - this.m_NKMMapTemplet.m_fMinZ;
			return this.m_NKMMapTemplet.m_fMinZ + num2 * num;
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x00082E30 File Offset: 0x00081030
		private NKMGameSyncData_Base GetCurrentSyncData(float rollbackTime = 0f)
		{
			bool bWillUseRollback = rollbackTime > 0f;
			NKMGameSyncData_Base nkmgameSyncData_Base = this.m_NKMGameSyncDataPack.m_listGameSyncData.FindLast((NKMGameSyncData_Base x) => x.IsRollbackPacket == bWillUseRollback);
			if (nkmgameSyncData_Base != null && nkmgameSyncData_Base.m_fAbsoluteGameTime < this.m_AbsoluteGameTime - rollbackTime)
			{
				nkmgameSyncData_Base = null;
			}
			if (nkmgameSyncData_Base == null)
			{
				nkmgameSyncData_Base = new NKMGameSyncData_Base();
				this.m_NKMGameSyncDataPack.m_listGameSyncData.Add(nkmgameSyncData_Base);
			}
			nkmgameSyncData_Base.m_fGameTime = this.m_NKMGameRuntimeData.m_GameTime - rollbackTime;
			nkmgameSyncData_Base.m_fAbsoluteGameTime = this.m_AbsoluteGameTime - rollbackTime;
			nkmgameSyncData_Base.m_fRemainGameTime = this.m_NKMGameRuntimeData.m_fRemainGameTime;
			nkmgameSyncData_Base.m_fRespawnCostA1 = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCost;
			nkmgameSyncData_Base.m_fRespawnCostB1 = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fRespawnCost;
			nkmgameSyncData_Base.m_fRespawnCostAssistA1 = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCostAssist;
			nkmgameSyncData_Base.m_fRespawnCostAssistB1 = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fRespawnCostAssist;
			nkmgameSyncData_Base.m_fUsedRespawnCostA1 = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fUsedRespawnCost;
			nkmgameSyncData_Base.m_fUsedRespawnCostB1 = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fUsedRespawnCost;
			nkmgameSyncData_Base.m_fShipDamage = this.m_NKMGameRuntimeData.m_fShipDamage;
			nkmgameSyncData_Base.m_NKM_GAME_SPEED_TYPE = this.m_NKMGameRuntimeData.m_NKM_GAME_SPEED_TYPE;
			nkmgameSyncData_Base.m_NKM_GAME_AUTO_SKILL_TYPE_A = this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(NKM_TEAM_TYPE.NTT_A1).m_NKM_GAME_AUTO_SKILL_TYPE;
			nkmgameSyncData_Base.m_NKM_GAME_AUTO_SKILL_TYPE_B = this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(NKM_TEAM_TYPE.NTT_B1).m_NKM_GAME_AUTO_SKILL_TYPE;
			if (base.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_FIERCE)
			{
				if (nkmgameSyncData_Base.m_NKMGameSyncData_Fierce == null)
				{
					nkmgameSyncData_Base.m_NKMGameSyncData_Fierce = new NKMGameSyncData_Fierce();
				}
				nkmgameSyncData_Base.m_NKMGameSyncData_Fierce.m_fFiercePoint = (int)this.m_GameRecord.TotalFiercePoint;
			}
			if (base.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_TRIM)
			{
				if (nkmgameSyncData_Base.m_NKMGameSyncData_Trim == null)
				{
					nkmgameSyncData_Base.m_NKMGameSyncData_Trim = new NKMGameSyncData_Trim();
				}
				nkmgameSyncData_Base.m_NKMGameSyncData_Trim.m_fTrimPoint = (int)this.m_GameRecord.TotalTrimPoint;
			}
			return nkmgameSyncData_Base;
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x00083018 File Offset: 0x00081218
		private void SyncDeckChange(NKM_TEAM_TYPE eNKM_TEAM_TYPE, float rollbackTime, int deckIndex, long unitUID, long deckUsedAddUnitUID, int deckUsedRemoveIndex, long deckTombAddUnitUID, int autoRespawnIndex, long nextDeckUnitUID)
		{
			NKMGameSyncData_Base currentSyncData = this.GetCurrentSyncData(rollbackTime);
			NKMGameSyncData_Deck nkmgameSyncData_Deck = new NKMGameSyncData_Deck();
			nkmgameSyncData_Deck.m_NKM_TEAM_TYPE = eNKM_TEAM_TYPE;
			nkmgameSyncData_Deck.m_UnitDeckIndex = (sbyte)deckIndex;
			nkmgameSyncData_Deck.m_UnitDeckUID = unitUID;
			nkmgameSyncData_Deck.m_DeckUsedAddUnitUID = deckUsedAddUnitUID;
			nkmgameSyncData_Deck.m_DeckUsedRemoveIndex = (sbyte)deckUsedRemoveIndex;
			nkmgameSyncData_Deck.m_DeckTombAddUnitUID = deckTombAddUnitUID;
			nkmgameSyncData_Deck.m_AutoRespawnIndex = (sbyte)autoRespawnIndex;
			nkmgameSyncData_Deck.m_NextDeckUnitUID = nextDeckUnitUID;
			currentSyncData.m_NKMGameSyncData_Deck.Add(nkmgameSyncData_Deck);
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x00083080 File Offset: 0x00081280
		private void SyncDeckChangeAssist(NKM_TEAM_TYPE eNKM_TEAM_TYPE, float rollbackTime, int autoRespawnIndexAssist)
		{
			NKMGameSyncData_Base currentSyncData = this.GetCurrentSyncData(rollbackTime);
			NKMGameSyncData_DeckAssist nkmgameSyncData_DeckAssist = new NKMGameSyncData_DeckAssist();
			nkmgameSyncData_DeckAssist.m_NKM_TEAM_TYPE = eNKM_TEAM_TYPE;
			nkmgameSyncData_DeckAssist.m_AutoRespawnIndexAssist = (sbyte)autoRespawnIndexAssist;
			currentSyncData.m_NKMGameSyncData_DeckAssist.Add(nkmgameSyncData_DeckAssist);
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x000830B4 File Offset: 0x000812B4
		public void SyncDieUnit(short gameUnitUID = -1)
		{
			NKMGameSyncData_Base currentSyncData = this.GetCurrentSyncData(0f);
			NKMGameSyncData_DieUnit nkmgameSyncData_DieUnit = null;
			if (gameUnitUID == -1)
			{
				nkmgameSyncData_DieUnit = new NKMGameSyncData_DieUnit();
				using (Dictionary<short, NKMUnit>.Enumerator enumerator = this.m_dicNKMUnitPool.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<short, NKMUnit> keyValuePair = enumerator.Current;
						if (!nkmgameSyncData_DieUnit.m_DieGameUnitUID.Contains(keyValuePair.Key))
						{
							nkmgameSyncData_DieUnit.m_DieGameUnitUID.Add(keyValuePair.Key);
						}
					}
					goto IL_B8;
				}
			}
			if (currentSyncData.m_NKMGameSyncData_DieUnit.Count > 0)
			{
				nkmgameSyncData_DieUnit = currentSyncData.m_NKMGameSyncData_DieUnit[currentSyncData.m_NKMGameSyncData_DieUnit.Count - 1];
			}
			else
			{
				nkmgameSyncData_DieUnit = new NKMGameSyncData_DieUnit();
			}
			if (!nkmgameSyncData_DieUnit.m_DieGameUnitUID.Contains(gameUnitUID))
			{
				nkmgameSyncData_DieUnit.m_DieGameUnitUID.Add(gameUnitUID);
			}
			IL_B8:
			currentSyncData.m_NKMGameSyncData_DieUnit.Add(nkmgameSyncData_DieUnit);
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x00083198 File Offset: 0x00081398
		public void SyncUnitSyncHalfData(NKMUnit cUnit, NKMUnitSyncData cNKMUnitSyncData)
		{
			cNKMUnitSyncData.m_usSpeedX = NKMUtil.FloatToHalf(cUnit.GetUnitFrameData().m_fSpeedX);
			cNKMUnitSyncData.m_usSpeedY = NKMUtil.FloatToHalf(cUnit.GetUnitFrameData().m_fSpeedY);
			cNKMUnitSyncData.m_usSpeedZ = NKMUtil.FloatToHalf(cUnit.GetUnitFrameData().m_fSpeedZ);
			cNKMUnitSyncData.m_bDamageSpeedXNegative = (cUnit.GetUnitFrameData().m_fDamageSpeedX < 0f);
			cNKMUnitSyncData.m_usDamageSpeedX = NKMUtil.FloatToHalf(Math.Abs(cUnit.GetUnitFrameData().m_fDamageSpeedX));
			cNKMUnitSyncData.m_usDamageSpeedZ = NKMUtil.FloatToHalf(cUnit.GetUnitFrameData().m_fDamageSpeedZ);
			cNKMUnitSyncData.m_usDamageSpeedJumpY = NKMUtil.FloatToHalf(cUnit.GetUnitFrameData().m_fDamageSpeedJumpY);
			cNKMUnitSyncData.m_usDamageSpeedKeepTimeX = NKMUtil.FloatToHalf(cUnit.GetUnitFrameData().m_fDamageSpeedKeepTimeX);
			cNKMUnitSyncData.m_usDamageSpeedKeepTimeZ = NKMUtil.FloatToHalf(cUnit.GetUnitFrameData().m_fDamageSpeedKeepTimeZ);
			cNKMUnitSyncData.m_usDamageSpeedKeepTimeJumpY = NKMUtil.FloatToHalf(cUnit.GetUnitFrameData().m_fDamageSpeedKeepTimeJumpY);
			cNKMUnitSyncData.m_usSkillCoolTime = 0;
			cNKMUnitSyncData.m_usHyperSkillCoolTime = 0;
			if (cUnit.GetUnitTemplet().m_listSkillStateData.Count > 0 && cUnit.GetUnitTemplet().m_listSkillStateData[0] != null)
			{
				float stateCoolTime = cUnit.GetStateCoolTime(cUnit.GetUnitTemplet().m_listSkillStateData[0].m_StateName);
				cNKMUnitSyncData.m_usSkillCoolTime = NKMUtil.FloatToHalf(stateCoolTime);
			}
			if (cUnit.GetUnitTemplet().m_listHyperSkillStateData.Count > 0 && cUnit.GetUnitTemplet().m_listHyperSkillStateData[0] != null)
			{
				float stateCoolTime2 = cUnit.GetStateCoolTime(cUnit.GetUnitTemplet().m_listHyperSkillStateData[0].m_StateName);
				cNKMUnitSyncData.m_usHyperSkillCoolTime = NKMUtil.FloatToHalf(stateCoolTime2);
			}
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x00083334 File Offset: 0x00081534
		public void SyncUnitSync(NKMUnit cUnit, NKMGameSyncDataPack cNKMGameSyncDataPack = null)
		{
			if (cNKMGameSyncDataPack == null)
			{
				cNKMGameSyncDataPack = this.m_NKMGameSyncDataPack;
			}
			float syncRollbackTime = cUnit.GetSyncRollbackTime();
			bool bWillUseRollback = syncRollbackTime > 0f;
			NKMGameSyncData_Base nkmgameSyncData_Base = cNKMGameSyncDataPack.m_listGameSyncData.FindLast((NKMGameSyncData_Base x) => x.IsRollbackPacket == bWillUseRollback);
			if (nkmgameSyncData_Base != null && nkmgameSyncData_Base.m_fAbsoluteGameTime < this.m_AbsoluteGameTime - cUnit.GetSyncRollbackTime())
			{
				nkmgameSyncData_Base = null;
			}
			if (nkmgameSyncData_Base == null)
			{
				nkmgameSyncData_Base = new NKMGameSyncData_Base();
				this.m_NKMGameSyncDataPack.m_listGameSyncData.Add(nkmgameSyncData_Base);
			}
			nkmgameSyncData_Base.IsRollbackPacket = bWillUseRollback;
			nkmgameSyncData_Base.m_fGameTime = this.m_NKMGameRuntimeData.m_GameTime - syncRollbackTime;
			nkmgameSyncData_Base.m_fAbsoluteGameTime = this.m_AbsoluteGameTime - syncRollbackTime;
			nkmgameSyncData_Base.m_fRemainGameTime = this.m_NKMGameRuntimeData.m_fRemainGameTime;
			nkmgameSyncData_Base.m_fRespawnCostA1 = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCost;
			nkmgameSyncData_Base.m_fRespawnCostB1 = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fRespawnCost;
			nkmgameSyncData_Base.m_fRespawnCostAssistA1 = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCostAssist;
			nkmgameSyncData_Base.m_fRespawnCostAssistB1 = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fRespawnCostAssist;
			nkmgameSyncData_Base.m_fUsedRespawnCostA1 = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fUsedRespawnCost;
			nkmgameSyncData_Base.m_fUsedRespawnCostB1 = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fUsedRespawnCost;
			nkmgameSyncData_Base.m_fShipDamage = this.m_NKMGameRuntimeData.m_fShipDamage;
			nkmgameSyncData_Base.m_NKM_GAME_SPEED_TYPE = this.m_NKMGameRuntimeData.m_NKM_GAME_SPEED_TYPE;
			nkmgameSyncData_Base.m_NKM_GAME_AUTO_SKILL_TYPE_A = this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(NKM_TEAM_TYPE.NTT_A1).m_NKM_GAME_AUTO_SKILL_TYPE;
			nkmgameSyncData_Base.m_NKM_GAME_AUTO_SKILL_TYPE_B = this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(NKM_TEAM_TYPE.NTT_B1).m_NKM_GAME_AUTO_SKILL_TYPE;
			NKMGameSyncData_Unit nkmgameSyncData_Unit = new NKMGameSyncData_Unit();
			nkmgameSyncData_Unit.m_NKMGameUnitSyncData = new NKMUnitSyncData();
			nkmgameSyncData_Unit.m_NKMGameUnitSyncData.DeepCopyFrom(cUnit.GetUnitSyncData());
			this.SyncUnitSyncHalfData(cUnit, nkmgameSyncData_Unit.m_NKMGameUnitSyncData);
			nkmgameSyncData_Base.m_NKMGameSyncData_Unit.Add(nkmgameSyncData_Unit);
			cUnit.GetUnitSyncData().m_bRespawnThisFrame = false;
			foreach (KeyValuePair<short, NKMBuffSyncData> keyValuePair in cUnit.GetUnitSyncData().m_dicBuffData)
			{
				keyValuePair.Value.m_bNew = false;
			}
			cUnit.GetUnitSyncData().m_listNKM_UNIT_EVENT_MARK.Clear();
			cUnit.GetUnitSyncData().m_listStatusTimeData.Clear();
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x00083580 File Offset: 0x00081780
		public void SyncUnitSimpleSync(NKMUnit cUnit)
		{
			NKMGameSyncData_Base currentSyncData = this.GetCurrentSyncData(0f);
			NKMGameSyncDataSimple_Unit nkmgameSyncDataSimple_Unit = new NKMGameSyncDataSimple_Unit();
			nkmgameSyncDataSimple_Unit.m_GameUnitUID = cUnit.GetUnitSyncData().m_GameUnitUID;
			nkmgameSyncDataSimple_Unit.m_bRight = cUnit.GetUnitSyncData().m_bRight;
			nkmgameSyncDataSimple_Unit.m_TargetUID = cUnit.GetUnitSyncData().m_TargetUID;
			nkmgameSyncDataSimple_Unit.m_SubTargetUID = cUnit.GetUnitSyncData().m_SubTargetUID;
			nkmgameSyncDataSimple_Unit.m_listNKM_UNIT_EVENT_MARK.Clear();
			for (int i = 0; i < cUnit.GetUnitSyncData().m_listNKM_UNIT_EVENT_MARK.Count; i++)
			{
				nkmgameSyncDataSimple_Unit.m_listNKM_UNIT_EVENT_MARK.Add(cUnit.GetUnitSyncData().m_listNKM_UNIT_EVENT_MARK[i]);
			}
			cUnit.GetUnitSyncData().m_bRespawnThisFrame = false;
			nkmgameSyncDataSimple_Unit.m_dicBuffData.Clear();
			foreach (KeyValuePair<short, NKMBuffSyncData> keyValuePair in cUnit.GetUnitSyncData().m_dicBuffData)
			{
				NKMBuffSyncData nkmbuffSyncData = new NKMBuffSyncData();
				nkmbuffSyncData.DeepCopyFrom(keyValuePair.Value);
				keyValuePair.Value.m_bNew = false;
				nkmgameSyncDataSimple_Unit.m_dicBuffData.Add(nkmbuffSyncData.m_BuffID, nkmbuffSyncData);
			}
			nkmgameSyncDataSimple_Unit.m_listStatusTimeData.Clear();
			foreach (NKMUnitStatusTimeSyncData item in cUnit.GetUnitSyncData().m_listStatusTimeData)
			{
				nkmgameSyncDataSimple_Unit.m_listStatusTimeData.Add(item);
			}
			cUnit.GetUnitSyncData().m_listStatusTimeData.Clear();
			currentSyncData.m_NKMGameSyncDataSimple_Unit.Add(nkmgameSyncDataSimple_Unit);
			cUnit.GetUnitSyncData().m_listNKM_UNIT_EVENT_MARK.Clear();
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x00083740 File Offset: 0x00081940
		public void SyncShipSkillSync(NKMUnit cUnit)
		{
			NKMGameSyncData_Base currentSyncData = this.GetCurrentSyncData(0f);
			NKMGameSyncData_ShipSkill nkmgameSyncData_ShipSkill = new NKMGameSyncData_ShipSkill();
			nkmgameSyncData_ShipSkill.m_NKMGameUnitSyncData = new NKMUnitSyncData();
			nkmgameSyncData_ShipSkill.m_NKMGameUnitSyncData.DeepCopyFrom(cUnit.GetUnitSyncData());
			nkmgameSyncData_ShipSkill.m_ShipSkillID = cUnit.GetUnitFrameData().m_ShipSkillTemplet.m_ShipSkillID;
			nkmgameSyncData_ShipSkill.m_SkillPosX = cUnit.GetUnitFrameData().m_fShipSkillPosX;
			currentSyncData.m_NKMGameSyncData_ShipSkill.Add(nkmgameSyncData_ShipSkill);
			cUnit.GetUnitSyncData().m_bRespawnThisFrame = false;
			foreach (KeyValuePair<short, NKMBuffSyncData> keyValuePair in cUnit.GetUnitSyncData().m_dicBuffData)
			{
				keyValuePair.Value.m_bNew = false;
			}
			cUnit.GetUnitSyncData().m_listNKM_UNIT_EVENT_MARK.Clear();
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x0008381C File Offset: 0x00081A1C
		protected void SyncGameStateChange(NKM_GAME_STATE eNKM_GAME_STATE, NKM_TEAM_TYPE eWinTeam, int waveID = 0)
		{
			NKMGameSyncData_Base currentSyncData = this.GetCurrentSyncData(0f);
			NKMGameSyncData_GameState nkmgameSyncData_GameState = new NKMGameSyncData_GameState();
			nkmgameSyncData_GameState.m_NKM_GAME_STATE = eNKM_GAME_STATE;
			nkmgameSyncData_GameState.m_WinTeam = eWinTeam;
			nkmgameSyncData_GameState.m_WaveID = waveID;
			currentSyncData.m_NKMGameSyncData_GameState.Add(nkmgameSyncData_GameState);
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x0008385C File Offset: 0x00081A5C
		protected void SyncDungeonEvent(NKMDungeonEventTemplet dungeonEventTemplet, NKM_TEAM_TYPE eTeam, bool bRecordToGameData)
		{
			NKMGameSyncData_Base currentSyncData = this.GetCurrentSyncData(0f);
			NKMGameSyncData_DungeonEvent nkmgameSyncData_DungeonEvent = new NKMGameSyncData_DungeonEvent();
			nkmgameSyncData_DungeonEvent.m_eEventActionType = dungeonEventTemplet.m_dungeonEventType;
			nkmgameSyncData_DungeonEvent.m_EventID = dungeonEventTemplet.m_EventID;
			nkmgameSyncData_DungeonEvent.m_iEventActionValue = dungeonEventTemplet.m_EventActionValue;
			nkmgameSyncData_DungeonEvent.m_strEventActionValue = dungeonEventTemplet.m_EventActionStrValue;
			nkmgameSyncData_DungeonEvent.m_bPause = dungeonEventTemplet.m_bPause;
			nkmgameSyncData_DungeonEvent.m_eTeam = eTeam;
			currentSyncData.m_NKMGameSyncData_DungeonEvent.Add(nkmgameSyncData_DungeonEvent);
			if (bRecordToGameData)
			{
				if (base.GetGameRuntimeData().m_lstPermanentDungeonEvent == null)
				{
					base.GetGameRuntimeData().m_lstPermanentDungeonEvent = new List<NKMGameSyncData_DungeonEvent>();
				}
				base.GetGameRuntimeData().m_lstPermanentDungeonEvent.Add(nkmgameSyncData_DungeonEvent);
			}
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x000838FC File Offset: 0x00081AFC
		protected void SyncGameEvent(NKM_GAME_EVENT_TYPE eNKM_GAME_EVENT_TYPE, NKM_TEAM_TYPE eNKM_TEAM_TYPE, int eventID, float fValue = 0f)
		{
			NKMGameSyncData_Base currentSyncData = this.GetCurrentSyncData(0f);
			NKMGameSyncData_GameEvent nkmgameSyncData_GameEvent = new NKMGameSyncData_GameEvent();
			nkmgameSyncData_GameEvent.m_NKM_GAME_EVENT_TYPE = eNKM_GAME_EVENT_TYPE;
			nkmgameSyncData_GameEvent.m_NKM_TEAM_TYPE = eNKM_TEAM_TYPE;
			nkmgameSyncData_GameEvent.m_EventID = eventID;
			nkmgameSyncData_GameEvent.m_fValue = fValue;
			currentSyncData.m_NKMGameSyncData_GameEvent.Add(nkmgameSyncData_GameEvent);
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x00083942 File Offset: 0x00081B42
		protected void ForceSyncDataPackFlushThisFrame()
		{
			this.m_SyncFlushTime = 0f;
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x00083950 File Offset: 0x00081B50
		private void SyncDataPackFlush()
		{
			this.m_SyncFlushTime -= this.m_fDeltaTime;
			if (this.m_SyncFlushTime <= 0f)
			{
				this.m_SyncFlushTime = 0.4f;
				if (this.m_NKMGameSyncDataPack.m_listGameSyncData.Count == 0)
				{
					NKMGameSyncData_Base nkmgameSyncData_Base = new NKMGameSyncData_Base();
					nkmgameSyncData_Base.m_fGameTime = this.m_NKMGameRuntimeData.m_GameTime;
					nkmgameSyncData_Base.m_fAbsoluteGameTime = this.m_AbsoluteGameTime;
					nkmgameSyncData_Base.m_fRemainGameTime = this.m_NKMGameRuntimeData.m_fRemainGameTime;
					nkmgameSyncData_Base.m_fShipDamage = this.m_NKMGameRuntimeData.m_fShipDamage;
					nkmgameSyncData_Base.m_fRespawnCostA1 = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCost;
					nkmgameSyncData_Base.m_fRespawnCostB1 = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fRespawnCost;
					nkmgameSyncData_Base.m_fRespawnCostAssistA1 = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCostAssist;
					nkmgameSyncData_Base.m_fRespawnCostAssistB1 = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fRespawnCostAssist;
					nkmgameSyncData_Base.m_fUsedRespawnCostA1 = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fUsedRespawnCost;
					nkmgameSyncData_Base.m_fUsedRespawnCostB1 = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fUsedRespawnCost;
					nkmgameSyncData_Base.m_NKM_GAME_SPEED_TYPE = this.m_NKMGameRuntimeData.m_NKM_GAME_SPEED_TYPE;
					nkmgameSyncData_Base.m_NKM_GAME_AUTO_SKILL_TYPE_A = this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(NKM_TEAM_TYPE.NTT_A1).m_NKM_GAME_AUTO_SKILL_TYPE;
					nkmgameSyncData_Base.m_NKM_GAME_AUTO_SKILL_TYPE_B = this.m_NKMGameRuntimeData.GetMyRuntimeTeamData(NKM_TEAM_TYPE.NTT_B1).m_NKM_GAME_AUTO_SKILL_TYPE;
					this.m_NKMGameSyncDataPack.m_listGameSyncData.Add(nkmgameSyncData_Base);
				}
				this.SendSyncDataPackFlush(new NKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT
				{
					absoluteGameTime = this.m_AbsoluteGameTime,
					gameSyncDataPack = this.m_NKMGameSyncDataPack
				});
				this.m_NKMGameSyncDataPack = new NKMGameSyncDataPack();
			}
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x00083AE9 File Offset: 0x00081CE9
		public virtual void SendSyncDataPackFlush(NKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT cPacket_NPT_GAME_SYNC_DATA_PACK_NOT)
		{
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x00083AEC File Offset: 0x00081CEC
		public NKMPacket_GAME_INTRUDE_START_NOT MakeFullSyncData()
		{
			NKMPacket_GAME_INTRUDE_START_NOT nkmpacket_GAME_INTRUDE_START_NOT = new NKMPacket_GAME_INTRUDE_START_NOT();
			nkmpacket_GAME_INTRUDE_START_NOT.gameTime = this.m_NKMGameRuntimeData.m_GameTime;
			nkmpacket_GAME_INTRUDE_START_NOT.absoluteGameTime = this.m_AbsoluteGameTime;
			nkmpacket_GAME_INTRUDE_START_NOT.gameSyncDataPack = new NKMGameSyncDataPack();
			nkmpacket_GAME_INTRUDE_START_NOT.gameTeamDeckDataA = new NKMGameTeamDeckData();
			nkmpacket_GAME_INTRUDE_START_NOT.gameTeamDeckDataA.DeepCopyFrom(base.GetGameData().m_NKMGameTeamDataA.m_DeckData);
			nkmpacket_GAME_INTRUDE_START_NOT.gameTeamDeckDataB = new NKMGameTeamDeckData();
			nkmpacket_GAME_INTRUDE_START_NOT.gameTeamDeckDataB.DeepCopyFrom(base.GetGameData().m_NKMGameTeamDataB.m_DeckData);
			nkmpacket_GAME_INTRUDE_START_NOT.usedRespawnCost = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fUsedRespawnCost;
			nkmpacket_GAME_INTRUDE_START_NOT.respawnCount = (float)this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_respawn_count;
			if (this.m_NKMGameData.m_NKMGameTeamDataA != null && this.m_NKMGameData.m_NKMGameTeamDataA.m_MainShip != null && this.m_NKMGameData.m_NKMGameTeamDataA.m_MainShip.m_listGameUnitUID.Count == 1)
			{
				nkmpacket_GAME_INTRUDE_START_NOT.mainShipAStateCoolTimeMap = this.GetUnit(this.m_NKMGameData.m_NKMGameTeamDataA.m_MainShip.m_listGameUnitUID[0], true, false).GetDicStateCoolTime();
			}
			if (this.m_NKMGameData.m_NKMGameTeamDataB != null && this.m_NKMGameData.m_NKMGameTeamDataB.m_MainShip != null && this.m_NKMGameData.m_NKMGameTeamDataB.m_MainShip.m_listGameUnitUID.Count == 1)
			{
				nkmpacket_GAME_INTRUDE_START_NOT.mainShipBStateCoolTimeMap = this.GetUnit(this.m_NKMGameData.m_NKMGameTeamDataB.m_MainShip.m_listGameUnitUID[0], true, false).GetDicStateCoolTime();
			}
			foreach (KeyValuePair<short, NKMUnit> keyValuePair in this.m_dicNKMUnit)
			{
				NKMUnit value = keyValuePair.Value;
				this.SyncUnitSync(value, nkmpacket_GAME_INTRUDE_START_NOT.gameSyncDataPack);
			}
			return nkmpacket_GAME_INTRUDE_START_NOT;
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x00083CA9 File Offset: 0x00081EA9
		public override NKMDamageEffectManager GetDEManager()
		{
			return this.m_DEManager;
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x00083CB1 File Offset: 0x00081EB1
		public override NKMDamageEffect GetDamageEffect(short DEUID)
		{
			return this.m_DEManager.GetDamageEffect(DEUID);
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x00083CBF File Offset: 0x00081EBF
		public virtual NKM_ERROR_CODE OnRecv(NKMPacket_GAME_PAUSE_REQ cNKMPacket_GAME_PAUSE_REQ)
		{
			if ((this.m_NKMGameRuntimeData.m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_FINISH || this.m_NKMGameRuntimeData.m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_END) && cNKMPacket_GAME_PAUSE_REQ.isPause)
			{
				return NKM_ERROR_CODE.NEC_FAIL_GAME_IS_PAUSE;
			}
			this.m_NKMGameRuntimeData.m_bPause = cNKMPacket_GAME_PAUSE_REQ.isPause;
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x00083D00 File Offset: 0x00081F00
		public virtual NKM_ERROR_CODE OnRecv(NKMPacket_GAME_RESPAWN_REQ cPacket_GAME_RESPAWN_REQ, NKM_TEAM_TYPE eNKM_TEAM_TYPE, ref long respawnUnitUID)
		{
			respawnUnitUID = cPacket_GAME_RESPAWN_REQ.unitUID;
			NKMGameTeamData nkmgameTeamData = null;
			if (base.IsATeam(eNKM_TEAM_TYPE))
			{
				nkmgameTeamData = this.m_NKMGameData.m_NKMGameTeamDataA;
			}
			if (base.GetGameData().IsPVP() && base.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_ASYNC_PVP)
			{
				return NKM_ERROR_CODE.NEC_FAIL_ASYNC_PVP_MANUAL_PLAY_DISABLE;
			}
			if (base.IsBTeam(eNKM_TEAM_TYPE))
			{
				nkmgameTeamData = this.m_NKMGameData.m_NKMGameTeamDataB;
			}
			if (nkmgameTeamData == null)
			{
				return NKMError.Build(NKM_ERROR_CODE.NEC_FAIL_ACCOUNT_INVALID_ID, -1L, this.m_NKMGameData.m_GameUID, string.Format("teamType:{0}", eNKM_TEAM_TYPE), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGameServerHost.cs", 3128);
			}
			if (nkmgameTeamData.m_DeckData == null)
			{
				return NKMError.Build(NKM_ERROR_CODE.NEC_FAIL_DECK_DATA_INVALID, -1L, this.m_NKMGameData.m_GameUID, string.Format("teamType:{0}", eNKM_TEAM_TYPE), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGameServerHost.cs", 3133);
			}
			NKMGameRuntimeTeamData nkmgameRuntimeTeamData = null;
			if (base.IsATeam(eNKM_TEAM_TYPE))
			{
				nkmgameRuntimeTeamData = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA;
			}
			if (base.IsBTeam(eNKM_TEAM_TYPE))
			{
				nkmgameRuntimeTeamData = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB;
			}
			NKMUnitData nkmunitData = null;
			bool assistUnit = cPacket_GAME_RESPAWN_REQ.assistUnit;
			if (assistUnit)
			{
				nkmunitData = nkmgameTeamData.GetAssistUnitDataByIndex(nkmgameTeamData.m_DeckData.GetAutoRespawnIndexAssist());
				if (nkmunitData != null)
				{
					this.GetNextAutoRespawnIndexAssist(nkmgameTeamData);
				}
			}
			else
			{
				for (int i = 0; i < nkmgameTeamData.m_DeckData.GetListUnitDeckCount(); i++)
				{
					long listUnitDeck = nkmgameTeamData.m_DeckData.GetListUnitDeck(i);
					if (cPacket_GAME_RESPAWN_REQ.unitUID == listUnitDeck)
					{
						nkmunitData = nkmgameTeamData.GetUnitDataByUnitUID(listUnitDeck);
						if (nkmunitData != null)
						{
							break;
						}
					}
				}
			}
			if (nkmunitData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_RESPAWN_ACK_UNIT_NULL;
			}
			respawnUnitUID = nkmunitData.m_UnitUID;
			NKMUnitTemplet unitTemplet = NKMUnitManager.GetUnitTemplet(nkmunitData.m_UnitID);
			if (unitTemplet == null)
			{
				Log.Error(string.Format("Can not found unittemplet. unitId:{0}, unitUid:{1}", nkmunitData.m_UnitID, nkmunitData.m_UnitUID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGameServerHost.cs", 3180);
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_RESPAWN_ACK_UNIT_TEMPLET_NULL;
			}
			NKMUnitStatTemplet statTemplet = unitTemplet.m_StatTemplet;
			float fFactor = this.m_fRespawnValidLandTeamA;
			bool flag = false;
			if (unitTemplet.m_UnitTempletBase.m_bRespawnFreePos)
			{
				flag = true;
			}
			if (base.GetDungeonTemplet() != null && base.GetDungeonTemplet().m_bRespawnFreePos)
			{
				flag = true;
			}
			if (!flag)
			{
				if (base.IsATeam(eNKM_TEAM_TYPE))
				{
					fFactor = this.m_fRespawnValidLandTeamA;
				}
				else
				{
					fFactor = this.m_fRespawnValidLandTeamB;
				}
			}
			else
			{
				fFactor = 0.8f;
			}
			int num;
			if (nkmgameTeamData.m_LeaderUnitUID == nkmunitData.m_UnitUID && !assistUnit)
			{
				num = Math.Max(0, base.GetRespawnCost(statTemplet, true, nkmgameTeamData.m_eNKM_TEAM_TYPE));
			}
			else
			{
				num = base.GetRespawnCost(statTemplet, false, nkmgameTeamData.m_eNKM_TEAM_TYPE);
			}
			float minOffset = base.GetGameData().IsPVP() ? NKMCommonConst.PVP_SUMMON_MIN_POS : NKMCommonConst.PVE_SUMMON_MIN_POS;
			cPacket_GAME_RESPAWN_REQ.respawnPosX = this.m_NKMMapTemplet.GetNearLandX(cPacket_GAME_RESPAWN_REQ.respawnPosX, base.IsATeam(eNKM_TEAM_TYPE), fFactor, minOffset);
			if (!assistUnit)
			{
				if ((float)num > nkmgameRuntimeTeamData.m_fRespawnCost)
				{
					return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_RESPAWN_ACK_NO_RESPAWN_COST;
				}
			}
			else if ((float)num > nkmgameRuntimeTeamData.m_fRespawnCostAssist)
			{
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_RESPAWN_ACK_NO_RESPAWN_COST;
			}
			if (this.IsRespawnUnitWait(nkmunitData.m_UnitUID))
			{
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_RESPAWN_ACK_UNIT_LIVE;
			}
			if (!base.IsGameUnitAllDie(nkmunitData.m_UnitUID))
			{
				NKM_ERROR_CODE nkm_ERROR_CODE = base.IsGameUnitAllInBattle(nkmunitData.m_UnitUID);
				if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
				{
					return nkm_ERROR_CODE;
				}
				if (!this.RespawnUnit(true, 0f, 0f, nkmgameTeamData, nkmgameRuntimeTeamData, nkmunitData.m_UnitUID, cPacket_GAME_RESPAWN_REQ.respawnPosX, false))
				{
					return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_RESPAWN_ACK_NO_GAME_STATE;
				}
				base.EventDieForReRespawn(nkmunitData.m_UnitUID);
				base.IsPVE();
				if (!this.RespawnUnit(false, 0.01f, 0f, nkmgameTeamData, nkmgameRuntimeTeamData, nkmunitData.m_UnitUID, cPacket_GAME_RESPAWN_REQ.respawnPosX, false))
				{
					return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_RESPAWN_ACK_NO_GAME_STATE;
				}
			}
			else
			{
				float rollbackTime = base.GetRollbackTime(unitTemplet);
				if (base.IsATeam(eNKM_TEAM_TYPE) && base.GetDungeonTemplet() != null && base.GetDungeonTemplet().m_DungeonTempletBase.m_RespawnCountMaxSameTime > 0 && base.GetLiveUnitCountTeamA() >= base.GetDungeonTemplet().m_DungeonTempletBase.m_RespawnCountMaxSameTime)
				{
					return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_RESPAWN_ACK_MAX_UNIT_COUNT_SAME_TIME;
				}
				if (!this.RespawnUnit(false, 0f, rollbackTime, nkmgameTeamData, nkmgameRuntimeTeamData, nkmunitData.m_UnitUID, cPacket_GAME_RESPAWN_REQ.respawnPosX, false))
				{
					return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_RESPAWN_ACK_NO_GAME_STATE;
				}
			}
			respawnUnitUID = nkmunitData.m_UnitUID;
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x000840B4 File Offset: 0x000822B4
		public virtual NKM_ERROR_CODE OnRecv(NKMPacket_GAME_UNIT_RETREAT_REQ cNKMPacket_GAME_UNIT_RETREAT_REQ, NKM_TEAM_TYPE eNKM_TEAM_TYPE)
		{
			NKMGameTeamData nkmgameTeamData = null;
			if (base.IsATeam(eNKM_TEAM_TYPE))
			{
				nkmgameTeamData = this.m_NKMGameData.m_NKMGameTeamDataA;
			}
			if (base.GetGameData().IsPVP() && base.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_ASYNC_PVP)
			{
				return NKM_ERROR_CODE.NEC_FAIL_ASYNC_PVP_MANUAL_PLAY_DISABLE;
			}
			if (base.IsBTeam(eNKM_TEAM_TYPE))
			{
				nkmgameTeamData = this.m_NKMGameData.m_NKMGameTeamDataB;
			}
			if (nkmgameTeamData == null)
			{
				return NKMError.Build(NKM_ERROR_CODE.NEC_FAIL_ACCOUNT_INVALID_ID, -1L, this.m_NKMGameData.m_GameUID, string.Format("teamType:{0}", eNKM_TEAM_TYPE), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGameServerHost.cs", 3310);
			}
			if (nkmgameTeamData.m_DeckData == null)
			{
				return NKMError.Build(NKM_ERROR_CODE.NEC_FAIL_DECK_DATA_INVALID, -1L, this.m_NKMGameData.m_GameUID, string.Format("teamType:{0}", eNKM_TEAM_TYPE), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGameServerHost.cs", 3313);
			}
			if (base.IsATeam(eNKM_TEAM_TYPE))
			{
				NKMGameRuntimeTeamData nkmgameRuntimeTeamDataA = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA;
			}
			if (base.IsBTeam(eNKM_TEAM_TYPE))
			{
				NKMGameRuntimeTeamData nkmgameRuntimeTeamDataB = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB;
			}
			NKMUnitData unitDataByUnitUID = nkmgameTeamData.GetUnitDataByUnitUID(cNKMPacket_GAME_UNIT_RETREAT_REQ.unitUID);
			if (unitDataByUnitUID == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_RESPAWN_ACK_NO_GAME_STATE;
			}
			if (base.IsGameUnitAllDie(unitDataByUnitUID.m_UnitUID))
			{
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_RESPAWN_ACK_NO_GAME_STATE;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = base.IsGameUnitAllInBattle(unitDataByUnitUID.m_UnitUID);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				return nkm_ERROR_CODE;
			}
			base.EventDieForReRespawn(unitDataByUnitUID.m_UnitUID);
			if (base.IsPVE() && NKMUnitManager.GetUnitTemplet(unitDataByUnitUID.m_UnitID) == null)
			{
				Log.Error(string.Format("Can not found unittemplet. unitId:{0}, unitUid:{1}", unitDataByUnitUID.m_UnitID, unitDataByUnitUID.m_UnitUID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGameServerHost.cs", 3341);
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_RESPAWN_ACK_UNIT_TEMPLET_NULL;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x0008422C File Offset: 0x0008242C
		protected bool IsRespawnUnitWait(long unitUID)
		{
			for (int i = 0; i < this.m_listNKMGameUnitRespawnData.Count; i++)
			{
				NKMGameUnitRespawnData nkmgameUnitRespawnData = this.m_listNKMGameUnitRespawnData[i];
				if (nkmgameUnitRespawnData != null && nkmgameUnitRespawnData.m_UnitUID == unitUID)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x0008426B File Offset: 0x0008246B
		public bool CanUseTacticalCommand(NKMGameRuntimeTeamData cNKMGameRuntimeTeamData, NKMTacticalCommandTemplet cNKMTacticalCommandTemplet, NKMTacticalCommandData cNKMTacticalCommandData)
		{
			return cNKMTacticalCommandData.m_fCoolTimeNow <= 0f && cNKMGameRuntimeTeamData.m_fRespawnCost >= cNKMTacticalCommandTemplet.GetNeedCost(cNKMTacticalCommandData);
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x00084290 File Offset: 0x00082490
		public NKM_ERROR_CODE OnRecv(NKMPacket_GAME_TACTICAL_COMMAND_REQ cNKMPacket_GAME_TACTICAL_COMMAND_REQ, NKMPacket_GAME_TACTICAL_COMMAND_ACK cNKMPacket_GAME_TACTICAL_COMMAND_ACK, NKM_TEAM_TYPE eNKM_TEAM_TYPE)
		{
			NKMGameTeamData nkmgameTeamData = null;
			NKMGameRuntimeTeamData nkmgameRuntimeTeamData = null;
			if (base.IsATeam(eNKM_TEAM_TYPE))
			{
				nkmgameTeamData = base.GetGameData().m_NKMGameTeamDataA;
				nkmgameRuntimeTeamData = base.GetGameRuntimeData().m_NKMGameRuntimeTeamDataA;
			}
			if (base.IsBTeam(eNKM_TEAM_TYPE))
			{
				nkmgameTeamData = base.GetGameData().m_NKMGameTeamDataB;
				nkmgameRuntimeTeamData = base.GetGameRuntimeData().m_NKMGameRuntimeTeamDataB;
			}
			if (nkmgameTeamData == null || nkmgameRuntimeTeamData == null)
			{
				Log.Error(string.Format("Invalid TeamType. {0}", eNKM_TEAM_TYPE), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGameServerHost.cs", 3409);
				return NKM_ERROR_CODE.NED_FAIL_INVALID_TEAM_TYPE;
			}
			NKMTacticalCommandData nkmtacticalCommandData = null;
			for (int i = 0; i < nkmgameTeamData.m_listTacticalCommandData.Count; i++)
			{
				NKMTacticalCommandData nkmtacticalCommandData2 = nkmgameTeamData.m_listTacticalCommandData[i];
				if (nkmtacticalCommandData2 != null && nkmtacticalCommandData2.m_TCID == cNKMPacket_GAME_TACTICAL_COMMAND_REQ.TCID)
				{
					nkmtacticalCommandData = nkmtacticalCommandData2;
					break;
				}
			}
			if (nkmtacticalCommandData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_TACTICAL_COMMAND_INVALID_TC;
			}
			cNKMPacket_GAME_TACTICAL_COMMAND_ACK.cTacticalCommandData.DeepCopyFromSource(nkmtacticalCommandData);
			NKMTacticalCommandTemplet tacticalCommandTempletByID = NKMTacticalCommandManager.GetTacticalCommandTempletByID((int)nkmtacticalCommandData.m_TCID);
			if (tacticalCommandTempletByID == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_TACTICAL_COMMAND_INVALID_TC;
			}
			if (!this.CanUseTacticalCommand(nkmgameRuntimeTeamData, tacticalCommandTempletByID, nkmtacticalCommandData))
			{
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_TACTICAL_COMMAND_NO_COST;
			}
			this.UseTacticalCommand(tacticalCommandTempletByID, nkmtacticalCommandData, nkmgameRuntimeTeamData, eNKM_TEAM_TYPE, 0f);
			cNKMPacket_GAME_TACTICAL_COMMAND_ACK.cTacticalCommandData.DeepCopyFromSource(nkmtacticalCommandData);
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x000843A0 File Offset: 0x000825A0
		protected void AddCost(NKM_TEAM_TYPE eNKM_TEAM_TYPE, float fAddCost)
		{
			NKMGameRuntimeTeamData nkmgameRuntimeTeamData = null;
			if (base.IsATeam(eNKM_TEAM_TYPE))
			{
				nkmgameRuntimeTeamData = base.GetGameRuntimeData().m_NKMGameRuntimeTeamDataA;
			}
			else if (base.IsBTeam(eNKM_TEAM_TYPE))
			{
				nkmgameRuntimeTeamData = base.GetGameRuntimeData().m_NKMGameRuntimeTeamDataB;
			}
			nkmgameRuntimeTeamData.m_fRespawnCost += fAddCost;
			if (nkmgameRuntimeTeamData.m_fRespawnCost > 10f)
			{
				nkmgameRuntimeTeamData.m_fRespawnCost = 10f;
			}
			if (nkmgameRuntimeTeamData.m_fRespawnCost < 0f)
			{
				nkmgameRuntimeTeamData.m_fRespawnCost = 0f;
			}
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x0008441C File Offset: 0x0008261C
		private void UseTacticalCommand_RealAffect(NKMTacticalCommandTemplet cNKMTacticalCommandTemplet, NKMTacticalCommandData cNKMTacticalCommandData, NKM_TEAM_TYPE eNKM_TEAM_TYPE)
		{
			if (cNKMTacticalCommandData == null || cNKMTacticalCommandTemplet == null)
			{
				return;
			}
			if (cNKMTacticalCommandTemplet.m_NKM_TACTICAL_COMMAND_TYPE == NKM_TACTICAL_COMMAND_TYPE.NTCT_COMBO)
			{
				base.SetStopTime(NKMCommonConst.OPERATOR_SKILL_STOP_TIME, NKM_STOP_TIME_INDEX.NSTI_OPERATOR_SKILL);
			}
			foreach (KeyValuePair<short, NKMUnit> keyValuePair in this.m_dicNKMUnit)
			{
				NKMUnit value = keyValuePair.Value;
				if (base.IsEnemy(eNKM_TEAM_TYPE, value.GetUnitDataGame().m_NKM_TEAM_TYPE))
				{
					if (!cNKMTacticalCommandTemplet.CheckEnemyTargetBuffExist() || (base.IsBoss(value.GetUnitDataGame().m_GameUnitUID) && !cNKMTacticalCommandTemplet.m_bTargetBossEnemy))
					{
						continue;
					}
					if (cNKMTacticalCommandTemplet.m_lstBuffStrID_Enemy != null)
					{
						for (int i = 0; i < cNKMTacticalCommandTemplet.m_lstBuffStrID_Enemy.Count; i++)
						{
							value.AddBuffByStrID(cNKMTacticalCommandTemplet.m_lstBuffStrID_Enemy[i], cNKMTacticalCommandData.m_Level, cNKMTacticalCommandData.m_Level, value.GetUnitDataGame().m_GameUnitUID, false, false, false, 1);
						}
					}
				}
				if (base.IsSameTeam(eNKM_TEAM_TYPE, value.GetUnitDataGame().m_NKM_TEAM_TYPE))
				{
					if (!cNKMTacticalCommandTemplet.CheckMyTeamTargetBuffExist() || (base.IsBoss(value.GetUnitDataGame().m_GameUnitUID) && !cNKMTacticalCommandTemplet.m_bTargetBossMyTeam))
					{
						continue;
					}
					if (cNKMTacticalCommandTemplet.m_lstBuffStrID_MyTeam != null)
					{
						for (int j = 0; j < cNKMTacticalCommandTemplet.m_lstBuffStrID_MyTeam.Count; j++)
						{
							value.AddBuffByStrID(cNKMTacticalCommandTemplet.m_lstBuffStrID_MyTeam[j], cNKMTacticalCommandData.m_Level, cNKMTacticalCommandData.m_Level, value.GetUnitDataGame().m_GameUnitUID, false, false, false, 1);
						}
					}
				}
				if (base.IsEnemy(eNKM_TEAM_TYPE, value.GetUnitDataGame().m_NKM_TEAM_TYPE))
				{
					value.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_TACTICAL_COMMAND_ENEMY_EFFECT);
				}
				else
				{
					value.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_TACTICAL_COMMAND_MYTEAM_EFFECT);
				}
			}
			if (cNKMTacticalCommandTemplet.m_fCostPump > 0f)
			{
				float fAddCost = cNKMTacticalCommandTemplet.m_fCostPump + (float)(cNKMTacticalCommandData.m_Level - 1) * cNKMTacticalCommandTemplet.m_fCostPumpPerLevel;
				this.AddCost(eNKM_TEAM_TYPE, fAddCost);
			}
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x000845DC File Offset: 0x000827DC
		private void UseTacticalCommand(NKMTacticalCommandTemplet cNKMTacticalCommandTemplet, NKMTacticalCommandData cNKMTacticalCommandData, NKMGameRuntimeTeamData cNKMGameRuntimeTeamData, NKM_TEAM_TYPE eNKM_TEAM_TYPE, float fReservedAffectTime = 0f)
		{
			if (cNKMTacticalCommandData == null || cNKMTacticalCommandTemplet == null || cNKMGameRuntimeTeamData == null)
			{
				return;
			}
			if (fReservedAffectTime <= 0f)
			{
				this.UseTacticalCommand_RealAffect(cNKMTacticalCommandTemplet, cNKMTacticalCommandData, eNKM_TEAM_TYPE);
			}
			else if (eNKM_TEAM_TYPE == NKM_TEAM_TYPE.NTT_A1)
			{
				this.m_NKMReservedTacticalCommandTeamA.SetNewData(fReservedAffectTime, cNKMTacticalCommandTemplet, cNKMTacticalCommandData, eNKM_TEAM_TYPE);
			}
			else if (eNKM_TEAM_TYPE == NKM_TEAM_TYPE.NTT_B1)
			{
				this.m_NKMReservedTacticalCommandTeamB.SetNewData(fReservedAffectTime, cNKMTacticalCommandTemplet, cNKMTacticalCommandData, eNKM_TEAM_TYPE);
			}
			else
			{
				Log.Error("UseTacticalCommand, Not support team type found, teamType " + eNKM_TEAM_TYPE.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGameServerHost.cs", 3577);
			}
			float needCost = cNKMTacticalCommandTemplet.GetNeedCost(cNKMTacticalCommandData);
			cNKMGameRuntimeTeamData.m_fRespawnCost -= needCost;
			cNKMGameRuntimeTeamData.m_fUsedRespawnCost += needCost;
			float num = cNKMTacticalCommandTemplet.m_fCoolTime;
			if (this.m_NKMGameData.IsPVP())
			{
				int operatorId = 0;
				if (eNKM_TEAM_TYPE == NKM_TEAM_TYPE.NTT_A1)
				{
					operatorId = this.m_NKMGameData.m_NKMGameTeamDataA.m_Operator.id;
				}
				else if (eNKM_TEAM_TYPE == NKM_TEAM_TYPE.NTT_B1)
				{
					operatorId = this.m_NKMGameData.m_NKMGameTeamDataB.m_Operator.id;
				}
				if (this.m_NKMGameData.IsBanOperator(operatorId))
				{
					int banOperatorLevel = this.m_NKMGameData.GetBanOperatorLevel(operatorId);
					float num2 = Math.Min(NKMUnitStatManager.m_fPercentPerBanLevel * (float)banOperatorLevel, NKMUnitStatManager.m_fMaxPercentPerBanLevel);
					num += num * num2 + Math.Min(NKMUnitStatManager.m_OperatorTacticalCommandPerBanLevel * (float)banOperatorLevel, NKMUnitStatManager.m_MaxOperatorTacticalCommandPerBanLevel);
				}
			}
			cNKMTacticalCommandData.m_fCoolTimeNow = num;
			cNKMTacticalCommandData.m_bCoolTimeOn = false;
			cNKMTacticalCommandData.m_UseCount += 1;
			cNKMTacticalCommandData.m_fComboResetCoolTimeNow = 0f;
			cNKMTacticalCommandData.m_ComboCount = 0;
			if (cNKMTacticalCommandTemplet.m_NKM_TACTICAL_COMMAND_TYPE == NKM_TACTICAL_COMMAND_TYPE.NTCT_ACTIVE)
			{
				this.SyncGameEvent(NKM_GAME_EVENT_TYPE.NGET_TACTICAL_COMMAND, eNKM_TEAM_TYPE, (int)cNKMTacticalCommandTemplet.m_TCID, 0f);
				return;
			}
			if (cNKMTacticalCommandTemplet.m_NKM_TACTICAL_COMMAND_TYPE == NKM_TACTICAL_COMMAND_TYPE.NTCT_COMBO)
			{
				this.SyncGameEvent(NKM_GAME_EVENT_TYPE.NGET_TC_COMBO_SKILL_SUCCESS, eNKM_TEAM_TYPE, (int)cNKMTacticalCommandTemplet.m_TCID, (float)cNKMTacticalCommandData.m_ComboCount);
			}
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x00084788 File Offset: 0x00082988
		public NKM_ERROR_CODE OnRecv(NKMPacket_GAME_SHIP_SKILL_REQ cPacket_GAME_SHIP_SKILL_REQ, NKM_TEAM_TYPE eNKM_TEAM_TYPE)
		{
			if (base.IsATeam(eNKM_TEAM_TYPE))
			{
				NKMGameRuntimeTeamData nkmgameRuntimeTeamDataA = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA;
			}
			if (base.IsBTeam(eNKM_TEAM_TYPE))
			{
				NKMGameRuntimeTeamData nkmgameRuntimeTeamDataB = this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB;
			}
			NKMUnit unit = this.GetUnit(cPacket_GAME_SHIP_SKILL_REQ.gameUnitUID, true, false);
			if (unit == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_SHIP_SKILL_ACK_NO_UNIT;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = unit.CanUseShipSkill(cPacket_GAME_SHIP_SKILL_REQ.shipSkillID);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				return nkm_ERROR_CODE;
			}
			if (NKMShipSkillManager.GetShipSkillTempletByID(cPacket_GAME_SHIP_SKILL_REQ.shipSkillID) == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_SHIP_SKILL_ACK_NO_SHIP_SKILL;
			}
			unit.UseShipSkill(cPacket_GAME_SHIP_SKILL_REQ.shipSkillID, cPacket_GAME_SHIP_SKILL_REQ.skillPosX);
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x0008480A File Offset: 0x00082A0A
		public NKM_ERROR_CODE OnRecv(NKMPacket_GAME_CHECK_DIE_UNIT_REQ cNKMPacket_GAME_CHECK_DIE_UNIT_REQ)
		{
			this.SyncDieUnit(-1);
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x00084814 File Offset: 0x00082A14
		public NKM_ERROR_CODE OnRecv(NKMPacket_GAME_AUTO_RESPAWN_REQ cPacket_GAME_AUTO_RESPAWN_REQ, NKM_TEAM_TYPE eNKM_TEAM_TYPE, NKMUserData cSenderUserData)
		{
			if (this.m_NKMDungeonTemplet != null && !this.m_NKMDungeonTemplet.m_bCanUseAuto && cPacket_GAME_AUTO_RESPAWN_REQ.isAutoRespawn)
			{
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_AUTO_CAN_NOT_USE;
			}
			if (!base.CanUseAutoRespawn(cSenderUserData) && cPacket_GAME_AUTO_RESPAWN_REQ.isAutoRespawn)
			{
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_AUTO_CAN_NOT_USE;
			}
			if (base.IsATeam(eNKM_TEAM_TYPE))
			{
				this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_bAutoRespawn = cPacket_GAME_AUTO_RESPAWN_REQ.isAutoRespawn;
			}
			else
			{
				if (!base.IsBTeam(eNKM_TEAM_TYPE))
				{
					Log.Error(string.Format("Invalid TeamData. {0}", eNKM_TEAM_TYPE), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGameServerHost.cs", 3691);
					return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_AUTO_CAN_NOT_USE;
				}
				this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_bAutoRespawn = cPacket_GAME_AUTO_RESPAWN_REQ.isAutoRespawn;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x000848BC File Offset: 0x00082ABC
		public NKM_ERROR_CODE OnRecv(NKMPacket_GAME_SPEED_2X_REQ cNKMPacket_GAME_SPEED_2X_REQ, NKMUserData cSenderUserData)
		{
			if (NKMGame.IsPVPSync(base.GetGameData().GetGameType()))
			{
				return NKMError.Build(NKM_ERROR_CODE.NEC_FAIL_GAME_SPEED_2X_NOT_SUPPORT_IN_PVP, -1L, base.GetGameData().m_GameUID, "", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGameServerHost.cs", 3703);
			}
			base.GetGameRuntimeData().m_NKM_GAME_SPEED_TYPE = cNKMPacket_GAME_SPEED_2X_REQ.gameSpeedType;
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x00084914 File Offset: 0x00082B14
		public NKM_ERROR_CODE OnRecv(NKMPacket_GAME_AUTO_SKILL_CHANGE_REQ cNKMPacket_GAME_AUTO_SKILL_CHANGE_REQ, NKM_TEAM_TYPE eNKM_TEAM_TYPE, NKMUserData cSenderUserData)
		{
			NKMGameRuntimeData gameRuntimeData = base.GetGameRuntimeData();
			if (gameRuntimeData == null)
			{
				Log.Error(string.Format("GameRuntimeData is null. UserUid:{0}", cSenderUserData.m_UserUID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGameServerHost.cs", 3716);
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_AUTO_CAN_NOT_USE;
			}
			NKMGameRuntimeTeamData myRuntimeTeamData = gameRuntimeData.GetMyRuntimeTeamData(eNKM_TEAM_TYPE);
			if (myRuntimeTeamData == null)
			{
				Log.Error(string.Format("MyRunTimeTeamData is null. UserUid:{0}", cSenderUserData.m_UserUID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGameServerHost.cs", 3724);
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_AUTO_CAN_NOT_USE;
			}
			myRuntimeTeamData.m_NKM_GAME_AUTO_SKILL_TYPE = cNKMPacket_GAME_AUTO_SKILL_CHANGE_REQ.gameAutoSkillType;
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x00084994 File Offset: 0x00082B94
		public NKM_ERROR_CODE OnRecv(NKMPacket_GAME_USE_UNIT_SKILL_REQ cNKMPacket_GAME_USE_UNIT_SKILL_REQ, NKM_TEAM_TYPE eNKM_TEAM_TYPE, out byte skillStateID, NKMUserData cSenderUserData)
		{
			bool flag = false;
			skillStateID = 0;
			NKMUnit unit = this.GetUnit(cNKMPacket_GAME_USE_UNIT_SKILL_REQ.gameUnitUID, true, false);
			if (unit == null || unit.IsDyingOrDie())
			{
				return NKM_ERROR_CODE.NEC_FAIL_USE_UNIT_SKILL_CANT_FIND_UNIT;
			}
			if (unit.GetUnitDataGame().m_NKM_TEAM_TYPE != eNKM_TEAM_TYPE)
			{
				return NKMError.Build(NKM_ERROR_CODE.NEC_FAIL_USE_UNIT_SKILL_CANT_USE_SKILL, -1L, base.GetGameData().m_GameUID, string.Format("m_GameUnitUID: {0}", cNKMPacket_GAME_USE_UNIT_SKILL_REQ.gameUnitUID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGameServerHost.cs", 3749);
			}
			if (unit.CanUseManualSkill(true, out flag, out skillStateID))
			{
				return NKM_ERROR_CODE.NEC_OK;
			}
			return NKM_ERROR_CODE.NEC_FAIL_USE_UNIT_SKILL_CANT_USE_SKILL;
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x00084A20 File Offset: 0x00082C20
		public virtual NKM_ERROR_CODE OnRecv(NKMPacket_GAME_DEV_RESPAWN_REQ cNKMPacket_GAME_DEV_RESPAWN_REQ, ref NKMPacket_GAME_DEV_RESPAWN_ACK cNKMPacket_GAME_DEV_RESPAWN_ACK, NKM_TEAM_TYPE eNKM_TEAM_TYPE)
		{
			NKMUnitData nkmunitData;
			if (cNKMPacket_GAME_DEV_RESPAWN_REQ.unitLevel == 1)
			{
				nkmunitData = new NKMUnitData();
				nkmunitData.m_UnitUID = NpcUid.Get();
				nkmunitData.m_UnitID = cNKMPacket_GAME_DEV_RESPAWN_REQ.unitID;
				nkmunitData.m_UnitLevel = cNKMPacket_GAME_DEV_RESPAWN_REQ.unitLevel;
				int unitSkillCount = nkmunitData.GetUnitSkillCount();
				for (int i = 0; i < 5; i++)
				{
					if (i < unitSkillCount)
					{
						nkmunitData.m_aUnitSkillLevel[i] = 1;
					}
					else
					{
						nkmunitData.m_aUnitSkillLevel[i] = 0;
					}
				}
				nkmunitData.m_LimitBreakLevel = 0;
			}
			else
			{
				nkmunitData = NKMDungeonManager.MakeUnitDataFromID(cNKMPacket_GAME_DEV_RESPAWN_REQ.unitID, NpcUid.Get(), cNKMPacket_GAME_DEV_RESPAWN_REQ.unitLevel, -1, 0, 0);
			}
			float fFactor = this.m_fRespawnValidLandTeamA;
			if (base.IsATeam(eNKM_TEAM_TYPE))
			{
				fFactor = this.m_fRespawnValidLandTeamA;
			}
			else
			{
				fFactor = this.m_fRespawnValidLandTeamB;
			}
			float num = cNKMPacket_GAME_DEV_RESPAWN_REQ.respawnPosX;
			if (num.IsNearlyEqual(-1f, 1E-05f))
			{
				num = this.GetRespawnPosX(base.IsATeam(eNKM_TEAM_TYPE), -1f);
			}
			float minOffset = base.GetGameData().IsPVP() ? NKMCommonConst.PVP_SUMMON_MIN_POS : NKMCommonConst.PVE_SUMMON_MIN_POS;
			num = this.m_NKMMapTemplet.GetNearLandX(num, base.IsATeam(eNKM_TEAM_TYPE), fFactor, minOffset);
			if (!this.DevRespawnUnit(ref cNKMPacket_GAME_DEV_RESPAWN_ACK, nkmunitData, num, this.GetRespawnPosZ(-1f, -1f), base.IsATeam(eNKM_TEAM_TYPE)))
			{
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_RESPAWN_ACK_NO_GAME_STATE;
			}
			cNKMPacket_GAME_DEV_RESPAWN_ACK.unitData = new NKMUnitData();
			cNKMPacket_GAME_DEV_RESPAWN_ACK.unitData.DeepCopyFrom(nkmunitData);
			cNKMPacket_GAME_DEV_RESPAWN_ACK.teamType = eNKM_TEAM_TYPE;
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x00084B7C File Offset: 0x00082D7C
		public bool DevRespawnUnit(ref NKMPacket_GAME_DEV_RESPAWN_ACK cNKMPacket_GAME_DEV_RESPAWN_ACK, NKMUnitData cNKMUnitData, float x, float z, bool bTeamA)
		{
			if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE != NKM_GAME_STATE.NGS_PLAY && this.m_NKMGameData.GetGameType() != NKM_GAME_TYPE.NGT_DEV)
			{
				return false;
			}
			base.CreateGameUnitUID(cNKMUnitData);
			if (bTeamA)
			{
				base.CreatePoolUnit(this.m_NKMGameData.m_NKMGameTeamDataA, cNKMUnitData, 0, NKM_TEAM_TYPE.NTT_A1, false);
			}
			else
			{
				base.CreatePoolUnit(this.m_NKMGameData.m_NKMGameTeamDataB, cNKMUnitData, 0, NKM_TEAM_TYPE.NTT_B1, false);
			}
			base.CreateDynaminRespawnPoolUnit(false);
			cNKMPacket_GAME_DEV_RESPAWN_ACK.dynamicRespawnUnitDataTeamA = this.m_NKMGameData.m_NKMGameTeamDataA.m_listDynamicRespawnUnitData;
			cNKMPacket_GAME_DEV_RESPAWN_ACK.dynamicRespawnUnitDataTeamB = this.m_NKMGameData.m_NKMGameTeamDataB.m_listDynamicRespawnUnitData;
			return this.RespawnUnit(cNKMUnitData, x, z, 0f, false, 0f);
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x00084C2B File Offset: 0x00082E2B
		public virtual NKM_ERROR_CODE OnRecv(NKMPacket_GAME_DEV_COOL_TIME_RESET_REQ cNKMPacket_GAME_DEV_COOL_TIME_RESET_REQ)
		{
			if (!base.GetGameData().m_bLocal)
			{
				return NKM_ERROR_CODE.NEC_FAIL_GAME_IS_NOT_LOCAL;
			}
			if (cNKMPacket_GAME_DEV_COOL_TIME_RESET_REQ.isSkill)
			{
				base.DEV_SkillCoolTimeReset(cNKMPacket_GAME_DEV_COOL_TIME_RESET_REQ.teamType);
			}
			else
			{
				base.DEV_HyperSkillCoolTimeReset(cNKMPacket_GAME_DEV_COOL_TIME_RESET_REQ.teamType);
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x04001BB1 RID: 7089
		private NKMGameSyncDataPack m_NKMGameSyncDataPack = new NKMGameSyncDataPack();

		// Token: 0x04001BB2 RID: 7090
		private float m_SyncFlushTime;

		// Token: 0x04001BB3 RID: 7091
		private NKMDamageEffectManager m_DEManager = new NKMDamageEffectManager();

		// Token: 0x04001BB4 RID: 7092
		protected bool m_bTeamAFirstRespawn;

		// Token: 0x04001BB5 RID: 7093
		protected bool m_bTeamBFirstRespawn;

		// Token: 0x04001BB6 RID: 7094
		private bool m_bUseStateChangeEvent;

		// Token: 0x04001BB7 RID: 7095
		private Dictionary<long, int> m_dicRespawnUnitUID_ByDeckUsed = new Dictionary<long, int>();

		// Token: 0x04001BB8 RID: 7096
		protected NKMReservedTacticalCommand m_NKMReservedTacticalCommandTeamA = new NKMReservedTacticalCommand();

		// Token: 0x04001BB9 RID: 7097
		protected NKMReservedTacticalCommand m_NKMReservedTacticalCommandTeamB = new NKMReservedTacticalCommand();

		// Token: 0x04001BBA RID: 7098
		protected bool m_bUseTurtlingPrevent;

		// Token: 0x04001BBB RID: 7099
		protected bool m_bTurtleWarningSentA;

		// Token: 0x04001BBC RID: 7100
		protected bool m_bTurtleWarningSentB;
	}
}
