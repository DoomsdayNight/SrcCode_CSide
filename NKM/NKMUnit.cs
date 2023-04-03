using System;
using System.Collections.Generic;
using Cs.Logging;
using Cs.Math;
using NKM.Templet;

namespace NKM
{
	// Token: 0x02000494 RID: 1172
	public class NKMUnit : NKMObjectPoolData
	{
		// Token: 0x06001F8F RID: 8079 RVA: 0x00095AEA File Offset: 0x00093CEA
		public NKM_UNIT_CLASS_TYPE Get_NKM_UNIT_CLASS_TYPE()
		{
			return this.m_NKM_UNIT_CLASS_TYPE;
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x00095AF2 File Offset: 0x00093CF2
		public NKMUnitState GetUnitStateBefore()
		{
			return this.m_UnitStateBefore;
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x00095AFA File Offset: 0x00093CFA
		public NKMUnitState GetUnitStateNow()
		{
			return this.m_UnitStateNow;
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001F92 RID: 8082 RVA: 0x00095B02 File Offset: 0x00093D02
		public LinkedList<NKMDamageEffect> llDamageEffect
		{
			get
			{
				return this.m_linklistDamageEffect;
			}
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x00095B0A File Offset: 0x00093D0A
		public NKMUnitData GetUnitData()
		{
			return this.m_UnitData;
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x00095B12 File Offset: 0x00093D12
		public NKMUnitDataGame GetUnitDataGame()
		{
			return this.m_UnitDataGame;
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x00095B1A File Offset: 0x00093D1A
		public NKMUnitTemplet GetUnitTemplet()
		{
			return this.m_UnitTemplet;
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x00095B22 File Offset: 0x00093D22
		public NKMUnitSyncData GetUnitSyncData()
		{
			return this.m_UnitSyncData;
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x00095B2A File Offset: 0x00093D2A
		public NKMUnitFrameData GetUnitFrameData()
		{
			return this.m_UnitFrameData;
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x00095B32 File Offset: 0x00093D32
		public void SetPushSync()
		{
			this.m_PushSyncData = true;
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x00095B3B File Offset: 0x00093D3B
		public void SetPushSimpleSync()
		{
			this.m_bPushSimpleSyncData = true;
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x00095B44 File Offset: 0x00093D44
		public void SetConserveHPRate()
		{
			this.m_bBuffHPRateConserveRequired = true;
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x00095B4D File Offset: 0x00093D4D
		public void SetSortUnitDirty()
		{
			this.m_fSortUnitDirtyCheckTime = 0.1f;
			this.m_bSortUnitDirty = true;
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x00095B61 File Offset: 0x00093D61
		public float GetTempSortDist()
		{
			return this.m_TempSortDist;
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x00095B69 File Offset: 0x00093D69
		public void SetBoss(bool bSet)
		{
			this.m_bBoss = bSet;
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x00095B72 File Offset: 0x00093D72
		public bool IsBoss()
		{
			return this.m_bBoss;
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x00095B7A File Offset: 0x00093D7A
		public void AddStateChangeEvent(NKMUnit.StateChangeEvent eventfunc)
		{
			this.dStateChangeEvent = (NKMUnit.StateChangeEvent)Delegate.Combine(this.dStateChangeEvent, eventfunc);
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x00095B93 File Offset: 0x00093D93
		public void RemoveStateChangeEvent(NKMUnit.StateChangeEvent eventfunc)
		{
			this.dStateChangeEvent = (NKMUnit.StateChangeEvent)Delegate.Remove(this.dStateChangeEvent, eventfunc);
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x00095BAC File Offset: 0x00093DAC
		public void ClearAllStateChangeEvent()
		{
			this.dStateChangeEvent = null;
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x00095BB8 File Offset: 0x00093DB8
		public NKMUnit()
		{
			this.m_NKM_UNIT_CLASS_TYPE = NKM_UNIT_CLASS_TYPE.NCT_UNIT;
			this.m_bUnloadable = true;
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x00095D65 File Offset: 0x00093F65
		public override bool LoadComplete()
		{
			return true;
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x00095D68 File Offset: 0x00093F68
		public override void Open()
		{
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x00095D6C File Offset: 0x00093F6C
		public override void Close()
		{
			this.m_TargetUnit = null;
			this.m_SubTargetUnit = null;
			this.InitLInkedDamageEffect();
			foreach (KeyValuePair<int, NKMStateCoolTime> keyValuePair in this.m_dicStateCoolTime)
			{
				NKMStateCoolTime value = keyValuePair.Value;
				this.m_NKMGame.GetObjectPool().CloseObj(value);
			}
			this.m_dicStateCoolTime.Clear();
			this.m_dicDynamicRespawnPool.Clear();
			this.m_dicUnitChangeRespawnPool.Clear();
			this.m_dicStateMaxCoolTime.Clear();
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x00095DF1 File Offset: 0x00093FF1
		public override void Unload()
		{
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x00095DF4 File Offset: 0x00093FF4
		public virtual bool LoadUnit(NKMGame cNKMGame, NKMUnitData cNKMUnitData, short masterGameUnitUID, short gameUnitUID, float fNearTargetRange, NKM_TEAM_TYPE eNKM_TEAM_TYPE, bool bSub, bool bAsync)
		{
			this.m_NKMGame = cNKMGame;
			this.m_UnitStateNow = null;
			this.m_UnitStateBefore = null;
			this.m_StateNameNow = "";
			this.m_StateNameNext = "";
			this.m_StateNameNextChange = "";
			this.m_UnitDataGame.m_MasterGameUnitUID = masterGameUnitUID;
			this.m_UnitDataGame.m_GameUnitUID = gameUnitUID;
			this.m_UnitDataGame.m_fTargetNearRange = fNearTargetRange;
			this.m_UnitDataGame.m_NKM_TEAM_TYPE_ORG = eNKM_TEAM_TYPE;
			this.m_UnitDataGame.m_NKM_TEAM_TYPE = eNKM_TEAM_TYPE;
			this.m_UnitData = cNKMUnitData;
			this.m_UnitData.m_UnitUID = cNKMUnitData.m_UnitUID;
			this.m_UnitDataGame.m_UnitUID = cNKMUnitData.m_UnitUID;
			this.m_UnitSyncData.m_GameUnitUID = this.m_UnitDataGame.m_GameUnitUID;
			this.m_UnitTemplet = NKMUnitManager.GetUnitTemplet(this.m_UnitData.m_UnitID);
			if (this.m_UnitTemplet == null)
			{
				return false;
			}
			this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE = NKM_UNIT_PLAY_STATE.NUPS_DIE;
			if (this.m_UnitTemplet.m_listHyperSkillStateData.Count > 0 && this.m_UnitTemplet.m_listHyperSkillStateData[0] != null && this.m_UnitTemplet.m_listHyperSkillStateData[0].m_fStartCool > 0f && this.IsStateUnlocked(this.m_UnitTemplet.m_listHyperSkillStateData[0]))
			{
				float num = 1f;
				if (this.m_NKMGame != null)
				{
					num = this.m_NKMGame.GetHyperBeginRatio(eNKM_TEAM_TYPE);
					if (num.IsNearlyEqual(-1f, 1E-05f))
					{
						num = this.m_UnitTemplet.m_listHyperSkillStateData[0].m_fStartCool;
					}
				}
				if (this.m_NKMGame != null && this.m_NKMGame.GetGameData().IsPVP())
				{
					num = this.m_UnitTemplet.m_listHyperSkillStateData[0].m_fStartCoolPVP;
				}
				for (int i = 0; i < this.m_UnitTemplet.m_listHyperSkillStateData.Count; i++)
				{
					this.SetStateCoolTime(this.m_UnitTemplet.m_listHyperSkillStateData[i].m_StateName, true, num);
				}
			}
			if (this.m_UnitTemplet.m_listSkillStateData.Count > 0 && this.m_UnitTemplet.m_listSkillStateData[0] != null && this.m_UnitTemplet.m_listSkillStateData[0].m_fStartCool > 0f && this.IsStateUnlocked(this.m_UnitTemplet.m_listSkillStateData[0]))
			{
				for (int j = 0; j < this.m_UnitTemplet.m_listSkillStateData.Count; j++)
				{
					this.SetStateCoolTime(this.m_UnitTemplet.m_listSkillStateData[0].m_StateName, true, this.m_UnitTemplet.m_listSkillStateData[0].m_fStartCool);
				}
			}
			for (int k = 0; k < this.GetUnitTemplet().m_listHitFeedBack.Count; k++)
			{
				this.GetUnitFrameData().m_listHitFeedBackCount.Add(0);
			}
			for (int l = 0; l < this.GetUnitTemplet().m_listHitCriticalFeedBack.Count; l++)
			{
				this.GetUnitFrameData().m_listHitCriticalFeedBackCount.Add(0);
			}
			for (int m = 0; m < this.GetUnitTemplet().m_listHitEvadeFeedBack.Count; m++)
			{
				this.GetUnitFrameData().m_listHitEvadeFeedBackCount.Add(0);
			}
			for (int n = 0; n < this.GetUnitTemplet().m_listAccumStateChangePack.Count; n++)
			{
				this.GetUnitFrameData().m_listUnitAccumStateData.Add(new NKMUnitAccumStateData());
			}
			this.m_listShipSkillTemplet.Clear();
			this.m_listShipSkillStateID.Clear();
			for (int num2 = 0; num2 < this.m_UnitTemplet.m_UnitTempletBase.GetSkillCount(); num2++)
			{
				NKMShipSkillTemplet shipSkillTempletByIndex = NKMShipSkillManager.GetShipSkillTempletByIndex(this.m_UnitTemplet.m_UnitTempletBase, num2);
				if (shipSkillTempletByIndex != null)
				{
					this.m_listShipSkillTemplet.Add(shipSkillTempletByIndex);
					if (shipSkillTempletByIndex.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_SHIP_ACTIVE)
					{
						if (shipSkillTempletByIndex.m_UnitStateName.Length > 1)
						{
							NKMUnitState unitState = this.GetUnitState(shipSkillTempletByIndex.m_UnitStateName, true);
							if (unitState != null)
							{
								this.m_listShipSkillStateID.Add((int)unitState.m_StateID);
							}
						}
						if (this.m_NKMGame != null && this.m_NKMGame.GetDungeonTemplet() != null && NKMDungeonManager.IsTutorialDungeon(this.m_NKMGame.GetDungeonTemplet().m_DungeonTempletBase.m_DungeonID))
						{
							this.SetStateCoolTime(shipSkillTempletByIndex.m_UnitStateName, true, 0f);
						}
						else
						{
							this.SetStateCoolTime(shipSkillTempletByIndex.m_UnitStateName, true, 0.3f);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x00096268 File Offset: 0x00094468
		public virtual void LoadUnitComplete()
		{
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x0009626C File Offset: 0x0009446C
		public virtual void InitLInkedDamageEffect()
		{
			foreach (NKMDamageEffect nkmdamageEffect in this.m_linklistDamageEffect)
			{
				nkmdamageEffect.SetDie(false, false);
			}
			this.m_linklistDamageEffect.Clear();
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x000962CC File Offset: 0x000944CC
		public virtual void RespawnUnit(float fPosX, float fPosZ, float fJumpYPos, bool bUseRight = false, bool bRight = true, float fInitHP = 0f, bool bInitHPRate = false, float rollbackTime = 0f)
		{
			this.m_fSyncRollbackTime = rollbackTime;
			this.m_bConsumeRollback = false;
			this.m_TargetUnit = null;
			this.m_SubTargetUnit = null;
			this.m_UnitStateNow = null;
			this.m_UnitStateBefore = null;
			this.m_UnitSyncData.RespawnInit(this.m_fSyncRollbackTime > 0f);
			this.m_UnitFrameData.RespawnInit();
			this.InitLInkedDamageEffect();
			this.SetSortUnitDirty();
			if (this.m_fSyncRollbackTime > 0f)
			{
				this.m_UnitFrameData.m_fAnimTime = this.m_fSyncRollbackTime;
				this.m_UnitFrameData.m_fStateTime = this.m_fSyncRollbackTime;
			}
			this.m_listDamageResistUnit.Clear();
			this.m_UnitFrameData.m_fFindTargetTime = 0f;
			this.m_UnitFrameData.m_fFindSubTargetTime = 0f;
			this.m_UnitDataGame.m_NKM_TEAM_TYPE = this.m_UnitDataGame.m_NKM_TEAM_TYPE_ORG;
			this.m_UnitDataGame.m_RespawnPosX = fPosX;
			this.m_UnitDataGame.m_RespawnPosZ = fPosZ;
			this.m_UnitDataGame.m_RespawnJumpYPos = fJumpYPos;
			this.m_EventMovePosX.Init();
			this.m_EventMovePosZ.Init();
			this.m_EventMovePosJumpY.Init();
			this.m_UnitFrameData.m_PosXCalc = this.m_UnitDataGame.m_RespawnPosX;
			this.m_UnitFrameData.m_PosZCalc = this.m_UnitDataGame.m_RespawnPosZ;
			this.m_UnitFrameData.m_JumpYPosCalc = this.m_UnitDataGame.m_RespawnJumpYPos;
			this.m_UnitSyncData.m_PosX = this.m_UnitDataGame.m_RespawnPosX;
			this.m_UnitSyncData.m_PosZ = this.m_UnitDataGame.m_RespawnPosZ;
			this.m_UnitSyncData.m_JumpYPos = this.m_UnitDataGame.m_RespawnJumpYPos;
			if (bUseRight)
			{
				this.m_UnitSyncData.m_bRight = bRight;
			}
			else if (this.m_NKMGame.IsATeam(this.m_UnitDataGame.m_NKM_TEAM_TYPE))
			{
				this.m_UnitSyncData.m_bRight = true;
			}
			else
			{
				this.m_UnitSyncData.m_bRight = false;
			}
			this.m_UnitData.m_UserUID = this.m_NKMGame.GetGameData().GetTeamData(this.m_UnitDataGame.m_NKM_TEAM_TYPE).m_user_uid;
			this.m_UnitFrameData.m_StatData = NKMUnitStatManager.MakeFinalStat(this.m_NKMGame, this.m_UnitData, this.m_UnitDataGame.m_NKM_TEAM_TYPE, this, this.GetOperatorForStat());
			if (!bInitHPRate)
			{
				this.m_UnitFrameData.m_fInitHP = fInitHP;
			}
			else
			{
				this.m_UnitFrameData.m_fInitHP = this.GetMaxHP(fInitHP);
			}
			if (!this.m_UnitFrameData.m_fInitHP.IsNearlyZero(1E-05f))
			{
				this.m_UnitSyncData.SetHP(this.m_UnitFrameData.m_fInitHP);
			}
			else
			{
				this.m_UnitFrameData.m_fInitHP = this.m_UnitFrameData.m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP);
				this.m_UnitSyncData.SetHP(this.m_UnitFrameData.m_fInitHP);
			}
			this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE = NKM_UNIT_PLAY_STATE.NUPS_PLAY;
			this.StateEvent_Phase(true);
			this.SaveEventMoveXPosition(fPosX);
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x000965AC File Offset: 0x000947AC
		protected float GetCoolTimeReducedByOperator(float fInputTime)
		{
			float num = fInputTime;
			if (this.m_UnitTemplet == null || this.m_UnitTemplet.m_UnitTempletBase == null)
			{
				return num;
			}
			if (this.m_UnitTemplet.m_UnitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_SHIP)
			{
				return num;
			}
			NKMGameData gameData = this.m_NKMGame.GetGameData();
			if (gameData == null)
			{
				return num;
			}
			NKMGameTeamData teamData = gameData.GetTeamData(this.GetTeam());
			if (teamData == null)
			{
				return num;
			}
			NKMOperator @operator = teamData.m_Operator;
			if (@operator == null)
			{
				return num;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(@operator.id);
			if (unitTempletBase == null || unitTempletBase.StatTemplet == null || unitTempletBase.StatTemplet.m_StatData == null)
			{
				return num;
			}
			float num2 = unitTempletBase.StatTemplet.m_StatData.GetStatBase(NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE);
			if (num2 <= 0f)
			{
				return num;
			}
			num2 /= 10000f;
			float num3 = 1f;
			if (this.m_UnitTemplet.m_UnitTempletBase.IsShip() && gameData.IsPVP() && gameData.IsBanOperator(@operator.id))
			{
				int banOperatorLevel = gameData.GetBanOperatorLevel(@operator.id);
				float num4 = NKMUnitStatManager.m_fPercentPerBanLevel * (float)banOperatorLevel;
				if (num4 > NKMUnitStatManager.m_fMaxPercentPerUpLevel)
				{
					num4 = NKMUnitStatManager.m_fMaxPercentPerBanLevel;
				}
				num3 -= num4;
			}
			num = fInputTime - fInputTime * num2 * num3;
			if (num <= 0f)
			{
				num = 0f;
			}
			Log.Debug("GetCoolTimeReducedByOperator success input : " + fInputTime.ToString() + ", output : " + num.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnit.cs", 1152);
			return num;
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x00096710 File Offset: 0x00094910
		public virtual void Update(float deltaTime)
		{
			this.m_DeltaTime = deltaTime;
			this.m_fSortUnitDirtyCheckTime -= deltaTime;
			if (this.m_fSortUnitDirtyCheckTime < 0f)
			{
				this.SetSortUnitDirty();
			}
			if (this.m_UnitDataGame == null)
			{
				return;
			}
			if (this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DIE)
			{
				return;
			}
			this.m_TargetUIDOrg = (int)this.m_UnitSyncData.m_TargetUID;
			this.m_SubTargetUIDOrg = (int)this.m_UnitSyncData.m_SubTargetUID;
			this.m_bRightOrg = this.m_UnitSyncData.m_bRight;
			if (this.GetUnitFrameData().m_PosXBefore.IsNearlyEqual(-1f, 1E-05f))
			{
				this.GetUnitFrameData().m_PosXBefore = this.GetUnitSyncData().m_PosX;
			}
			if (this.GetUnitFrameData().m_PosZBefore.IsNearlyEqual(-1f, 1E-05f))
			{
				this.GetUnitFrameData().m_PosZBefore = this.GetUnitSyncData().m_PosZ;
			}
			if (this.GetUnitFrameData().m_JumpYPosBefore.IsNearlyEqual(-1f, 1E-05f))
			{
				this.GetUnitFrameData().m_JumpYPosBefore = this.GetUnitSyncData().m_JumpYPos;
			}
			if (!this.IsStopTime())
			{
				this.m_UnitFrameData.m_fStopReserveTime -= this.m_DeltaTime;
				if (this.m_UnitFrameData.m_fStopReserveTime < 0f)
				{
					this.m_UnitFrameData.m_fStopReserveTime = 0f;
				}
				this.m_UnitFrameData.m_PosXCalc = this.m_UnitSyncData.m_PosX;
				this.m_UnitFrameData.m_PosZCalc = this.m_UnitSyncData.m_PosZ;
				this.m_UnitFrameData.m_JumpYPosCalc = this.m_UnitSyncData.m_JumpYPos;
				this.StateTimeUpdate();
				this.AnimTimeUpdate();
				this.DoStateEndStart();
				this.StateUpdate();
				if (this.m_NKM_UNIT_CLASS_TYPE == NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
				{
					this.StateEvent();
					return;
				}
			}
			else
			{
				for (int i = 0; i < 3; i++)
				{
					this.m_UnitFrameData.m_StopTime[i] -= this.m_DeltaTime;
					if (this.m_UnitFrameData.m_StopTime[i] < 0f)
					{
						this.m_UnitFrameData.m_StopTime[i] = 0f;
					}
				}
			}
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x00096924 File Offset: 0x00094B24
		public virtual void Update2()
		{
			if (this.m_UnitDataGame == null)
			{
				return;
			}
			if (this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DIE && !this.IsStopTime())
			{
				this.m_UnitFrameData.m_fDamageBeforeFrame = this.m_UnitFrameData.m_fDamageThisFrame;
				if (this.m_UnitFrameData.m_bInvincible || this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_INVINCIBLE))
				{
					this.m_UnitFrameData.m_fDamageThisFrame = 0f;
				}
				if (this.m_UnitStateNow != null && this.m_UnitStateNow.m_bInvincibleState)
				{
					this.m_UnitFrameData.m_fDamageThisFrame = 0f;
				}
				this.GetUnitFrameData().m_PosXBefore = this.m_UnitSyncData.m_PosX;
				this.GetUnitFrameData().m_PosZBefore = this.m_UnitSyncData.m_PosZ;
				this.GetUnitFrameData().m_JumpYPosBefore = this.m_UnitSyncData.m_JumpYPos;
				this.m_UnitSyncData.m_PosX = this.m_UnitFrameData.m_PosXCalc;
				this.m_UnitSyncData.m_PosZ = this.m_UnitFrameData.m_PosZCalc;
				this.m_UnitSyncData.m_JumpYPos = this.m_UnitFrameData.m_JumpYPosCalc;
				if (this.m_UnitFrameData.m_bFindTargetThisFrame)
				{
					this.AITarget();
					this.m_UnitFrameData.m_bFindTargetThisFrame = false;
					this.m_UnitFrameData.m_fFindTargetTime = this.GetUnitTemplet().m_FindTargetTime;
				}
				if (this.m_UnitFrameData.m_bFindSubTargetThisFrame && this.GetUnitTemplet().m_SubTargetFindData != null)
				{
					this.AISubTarget();
					this.m_UnitFrameData.m_bFindSubTargetThisFrame = false;
					this.m_UnitFrameData.m_fFindSubTargetTime = this.GetUnitTemplet().m_SubTargetFindData.m_fFindTargetTime;
				}
				if (this.m_NKMGame.GetGameDevModeData().m_bNoHPDamageModeTeamA && this.m_NKMGame.IsATeam(this.GetUnitDataGame().m_NKM_TEAM_TYPE))
				{
					this.m_UnitFrameData.m_fDamageThisFrame = 0f;
				}
				if (this.m_NKMGame.GetGameDevModeData().m_bNoHPDamageModeTeamB && this.m_NKMGame.IsBTeam(this.GetUnitDataGame().m_NKM_TEAM_TYPE))
				{
					this.m_UnitFrameData.m_fDamageThisFrame = 0f;
				}
				if (this.m_UnitFrameData.m_fDamageThisFrame > 0f)
				{
					if (this.m_UnitFrameData.m_BarrierBuffData != null && this.m_UnitFrameData.m_BarrierBuffData.m_fBarrierHP > 0f)
					{
						this.m_UnitFrameData.m_BarrierBuffData.m_fBarrierHP -= this.m_UnitFrameData.m_fDamageThisFrame;
						if (this.m_UnitFrameData.m_BarrierBuffData.m_fBarrierHP < 0f)
						{
							this.m_UnitFrameData.m_BarrierBuffData.m_fBarrierHP = 0f;
						}
					}
					else
					{
						if (this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_IMMORTAL))
						{
							if (this.m_UnitFrameData.m_fDamageThisFrame >= this.m_UnitSyncData.GetHP())
							{
								this.m_UnitFrameData.m_bImmortalStart = true;
								this.m_UnitSyncData.SetHP(1f);
								this.GetUnitFrameData().m_fDangerChargeDamage += this.m_UnitFrameData.m_fDamageThisFrame;
							}
							else
							{
								this.m_UnitSyncData.SetHP(this.m_UnitSyncData.GetHP() - this.m_UnitFrameData.m_fDamageThisFrame);
								this.GetUnitFrameData().m_fDangerChargeDamage += this.m_UnitFrameData.m_fDamageThisFrame;
							}
						}
						else
						{
							this.m_UnitSyncData.SetHP(this.m_UnitSyncData.GetHP() - this.m_UnitFrameData.m_fDamageThisFrame);
							this.GetUnitFrameData().m_fDangerChargeDamage += this.m_UnitFrameData.m_fDamageThisFrame;
						}
						bool flag;
						float phaseDamageLimit = this.GetPhaseDamageLimit(out flag);
						if (phaseDamageLimit > 0f && this.GetNowHP() < phaseDamageLimit)
						{
							if (flag)
							{
								this.m_UnitSyncData.SetHP(phaseDamageLimit - 1f);
							}
							if (!this.GetUnitTemplet().m_UnitTempletBase.m_bMonster && this.GetNowHP() <= 0f)
							{
								this.m_UnitSyncData.SetHP(1f);
							}
						}
					}
					this.m_UnitFrameData.m_fDamageThisFrame = 0f;
				}
				if (this.m_UnitSyncData.GetHP() <= 0f)
				{
					if (this.m_NKMGame.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_PRACTICE)
					{
						this.m_UnitSyncData.SetHP(1f);
					}
					else
					{
						this.m_UnitSyncData.SetHP(0f);
						this.SetDying(false, false);
					}
				}
			}
			if (!this.IsStopTime())
			{
				if (this.m_bRightOrg != this.m_UnitSyncData.m_bRight)
				{
					this.m_bPushSimpleSyncData = true;
				}
				if (this.m_TargetUIDOrg != 0 && this.m_UnitSyncData.m_TargetUID != 0 && this.m_TargetUIDOrg != (int)this.m_UnitSyncData.m_TargetUID)
				{
					this.m_bPushSimpleSyncData = true;
				}
				if (this.m_SubTargetUIDOrg != 0 && this.m_UnitSyncData.m_SubTargetUID != 0 && this.m_SubTargetUIDOrg != (int)this.m_UnitSyncData.m_SubTargetUID)
				{
					this.m_bPushSimpleSyncData = true;
				}
				bool flag2 = false;
				if (this.m_StateNameNext.Length > 1)
				{
					this.m_StateNameNextChange = this.m_StateNameNext;
					this.m_StateNameNext = "";
					flag2 = true;
				}
				if (!flag2 && this.m_PushSyncData)
				{
					this.PushSyncData();
				}
				else if (!flag2 && this.m_bPushSimpleSyncData)
				{
					this.PushSimpleSyncData();
				}
				this.m_bPushSimpleSyncData = false;
			}
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x00096E2F File Offset: 0x0009502F
		protected void StateTimeUpdate()
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			this.m_UnitFrameData.m_fStateTimeBack = this.m_UnitFrameData.m_fStateTime;
			this.m_UnitFrameData.m_fStateTime += this.m_DeltaTime;
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x00096E68 File Offset: 0x00095068
		protected void AnimTimeUpdate()
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			if (this.m_UnitFrameData.m_fAnimTime >= this.m_UnitFrameData.m_fAnimTimeMax && this.m_UnitStateNow.m_bAnimLoop)
			{
				this.m_UnitFrameData.m_fAnimTimeBack = 0f;
				this.m_UnitFrameData.m_fAnimTime = 0f;
				foreach (KeyValuePair<float, NKMTimeStamp> keyValuePair in this.m_EventTimeStampAnim)
				{
					NKMTimeStamp value = keyValuePair.Value;
					this.m_NKMGame.GetObjectPool().CloseObj(value);
				}
				this.m_EventTimeStampAnim.Clear();
				this.ChangeAnimSpeed(0f);
			}
			this.m_UnitFrameData.m_fAnimTimeBack = this.m_UnitFrameData.m_fAnimTime;
			this.m_UnitFrameData.m_fAnimTime += this.m_DeltaTime * this.m_UnitFrameData.m_fAnimSpeed;
			this.m_UnitFrameData.m_bAnimPlayCountAddThisFrame = false;
			if (this.m_UnitFrameData.m_fAnimTime >= this.m_UnitFrameData.m_fAnimTimeMax)
			{
				if (this.m_UnitStateNow.m_bAnimLoop)
				{
					this.m_UnitFrameData.m_AnimPlayCount++;
					this.m_UnitFrameData.m_bAnimPlayCountAddThisFrame = true;
					return;
				}
				this.m_UnitFrameData.m_AnimPlayCount = 1;
			}
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x00096FA8 File Offset: 0x000951A8
		public NKMUnitState GetUnitState(short StateID)
		{
			return this.m_UnitTemplet.GetUnitState(StateID);
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x00096FB6 File Offset: 0x000951B6
		public NKMUnitState GetUnitState(string StateName, bool bLog = true)
		{
			return this.m_UnitTemplet.GetUnitState(StateName, bLog);
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x00096FC8 File Offset: 0x000951C8
		protected void DoStateEndStart()
		{
			if (this.m_StateNameNextChange.Length > 1)
			{
				this.StateEnd();
				this.beforeStateName3 = this.beforeStateName2;
				this.beforeStateName2 = this.beforeStateName1;
				this.beforeStateName1 = this.m_StateNameNow;
				this.m_StateNameNow = this.m_StateNameNextChange;
				this.m_StateNameNextChange = "";
				this.m_UnitStateBefore = this.m_UnitStateNow;
				this.m_UnitStateNow = this.GetUnitState(this.m_StateNameNow, true);
				if (this.m_UnitStateNow == null)
				{
					Log.Error("UnitState is null. " + this.m_StateNameNow, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnit.cs", 1470);
					return;
				}
				this.m_UnitSyncData.m_StateID = this.m_UnitStateNow.m_StateID;
				if (this.m_NKM_UNIT_CLASS_TYPE == NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
				{
					NKMUnitSyncData unitSyncData = this.m_UnitSyncData;
					unitSyncData.m_StateChangeCount += 1;
					this.m_PushSyncData = true;
					if (this.m_UnitFrameData.m_ShipSkillTemplet != null && this.m_UnitFrameData.m_ShipSkillTemplet.m_UnitStateName == this.m_StateNameNow)
					{
						this.m_UnitFrameData.m_bSyncShipSkill = true;
					}
				}
				this.StateStart();
			}
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x000970E4 File Offset: 0x000952E4
		public void SetSyncRollbackTime(float rollbackTime, bool bWillConsumeRollback)
		{
			this.m_bConsumeRollback = bWillConsumeRollback;
			this.m_fSyncRollbackTime = rollbackTime;
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x000970F4 File Offset: 0x000952F4
		public float GetSyncRollbackTime()
		{
			return this.m_fSyncRollbackTime;
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x000970FC File Offset: 0x000952FC
		public virtual void PushSyncData()
		{
			this.m_PushSyncData = false;
			this.m_bPushSimpleSyncData = false;
			if (this.m_bConsumeRollback)
			{
				this.m_fSyncRollbackTime = 0f;
			}
			this.m_UnitSyncData.m_listDamageData.Clear();
			this.m_UnitSyncData.m_listStatusTimeData.Clear();
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x0009714A File Offset: 0x0009534A
		protected virtual void PushSimpleSyncData()
		{
			this.m_bPushSimpleSyncData = false;
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x00097154 File Offset: 0x00095354
		protected virtual void StateEnd()
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			if (this.m_UnitStateNow != null)
			{
				this.ProcessStateEvent<NKMEventFindTarget>(this.m_UnitStateNow.m_listNKMEventFindTarget, true);
			}
			this.ProcessEventText(true);
			this.ProcessEventSpeed(true);
			this.ProcessEventSpeedX(true);
			this.ProcessEventSpeedY(true);
			this.ProcessEventMove(true);
			this.ProcessEventStopTime(true);
			this.ProcessEventInvincibleGlobal(true);
			this.ProcessEventSound(true);
			this.ProcessEventColor(true);
			this.ProcessEventCameraCrash(true);
			this.ProcessEventCameraMove(true);
			this.ProcessEventDissolve(true);
			this.ProcessEventEffect(true);
			this.ProcessEventDEStateChange(true);
			this.ProcessEventDamageEffect(true);
			this.ProcessEventGameSpeed(true);
			this.ProcessEventBuff(true);
			this.ProcessEventStatus(true);
			this.ProcessEventRespawn(true);
			this.ProcessEventUnitChange(true);
			this.ProcessEventDie(true);
			this.ProcessEventChangeState(true);
			this.ProcessEventAgro(true);
			this.ProcessEventHeal(true);
			this.ProcessEventStun(true);
			this.ProcessEventCost(true);
			this.ProcessEventDispel(true);
			if (this.m_UnitSyncData.m_TargetUID > 0 && (this.m_TargetUnit == null || this.m_TargetUnit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DIE || this.m_TargetUnit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DYING))
			{
				this.m_UnitSyncData.m_TargetUID = 0;
			}
			if (this.m_UnitSyncData.m_SubTargetUID > 0 && (this.m_SubTargetUnit == null || this.m_SubTargetUnit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DIE || this.m_SubTargetUnit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DYING))
			{
				this.m_UnitSyncData.m_SubTargetUID = 0;
			}
			NKMUnit.StateChangeEvent stateChangeEvent = this.dStateChangeEvent;
			if (stateChangeEvent == null)
			{
				return;
			}
			stateChangeEvent(NKM_UNIT_STATE_CHANGE_TYPE.NUSCT_END, this, this.m_UnitStateNow);
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x000972E8 File Offset: 0x000954E8
		private float FindZeroFrameAnimSpeedEvent(NKMUnitState unitState)
		{
			if (unitState == null)
			{
				return 1f;
			}
			foreach (NKMEventAnimSpeed nkmeventAnimSpeed in unitState.m_listNKMEventAnimSpeed)
			{
				if (nkmeventAnimSpeed.m_fEventTime == 0f && this.CheckEventCondition(nkmeventAnimSpeed.m_Condition))
				{
					return nkmeventAnimSpeed.m_fAnimSpeed;
				}
			}
			return unitState.m_fAnimSpeed;
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x0009736C File Offset: 0x0009556C
		protected virtual void StateStart()
		{
			this.m_bConsumeRollback = true;
			float num = this.FindZeroFrameAnimSpeedEvent(this.m_UnitStateNow);
			if (this.m_UnitStateNow.m_AnimName.Length > 1)
			{
				this.m_UnitFrameData.m_fAnimTimeBack = this.m_fSyncRollbackTime * num;
				if (this.m_UnitStateNow.m_fAnimStartTime > 0f)
				{
					this.m_UnitFrameData.m_fAnimTimeBack = this.m_UnitStateNow.m_fAnimStartTime + this.m_fSyncRollbackTime * num;
				}
				this.m_UnitFrameData.m_fAnimTime = this.m_UnitStateNow.m_fAnimStartTime + this.m_fSyncRollbackTime * num;
				this.m_UnitFrameData.m_fAnimTimeMax = NKMAnimDataManager.GetAnimTimeMax(this.m_UnitTemplet.m_UnitTempletBase.m_SpriteBundleName, this.m_UnitTemplet.m_UnitTempletBase.m_SpriteName, this.m_UnitStateNow.m_AnimName);
				if (this.m_UnitFrameData.m_fAnimTimeMax.IsNearlyZero(1E-05f))
				{
					Log.Error(string.Concat(new string[]
					{
						"NKMUnit NoExistAnim: ",
						this.GetUnitTemplet().m_UnitTempletBase.m_UnitStrID,
						" : ",
						this.m_UnitStateNow.m_StateName,
						" : ",
						this.m_UnitStateNow.m_AnimName
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnit.cs", 1626);
					return;
				}
				this.m_UnitFrameData.m_AnimPlayCount = 0;
			}
			if (!this.m_UnitStateNow.m_fAirHigh.IsNearlyEqual(-1f, 1E-05f))
			{
				this.m_UnitFrameData.m_fAirHigh = this.m_UnitStateNow.m_fAirHigh;
			}
			else
			{
				this.m_UnitFrameData.m_fAirHigh = this.m_UnitTemplet.m_fAirHigh;
			}
			this.ChangeAnimSpeed(num);
			if (this.m_UnitStateNow.m_bAutoCoolTime)
			{
				this.m_UnitStateNow.m_StateCoolTime.m_Min = this.m_UnitFrameData.m_fAnimTimeMax - 0.2f;
				this.m_UnitStateNow.m_StateCoolTime.m_Max = this.m_UnitFrameData.m_fAnimTimeMax + 0.2f;
				if (this.m_UnitFrameData.m_fAnimSpeed > 0f)
				{
					this.m_UnitStateNow.m_StateCoolTime.m_Min /= this.m_UnitFrameData.m_fAnimSpeed;
					this.m_UnitStateNow.m_StateCoolTime.m_Max /= this.m_UnitFrameData.m_fAnimSpeed;
				}
				else
				{
					this.m_UnitStateNow.m_StateCoolTime.m_Min = 0f;
					this.m_UnitStateNow.m_StateCoolTime.m_Max = 0f;
				}
			}
			this.m_UnitFrameData.m_fStateTime = this.m_fSyncRollbackTime;
			this.m_UnitFrameData.m_fStateTimeBack = this.m_fSyncRollbackTime;
			foreach (KeyValuePair<float, NKMTimeStamp> keyValuePair in this.m_EventTimeStampAnim)
			{
				NKMTimeStamp value = keyValuePair.Value;
				this.m_NKMGame.GetObjectPool().CloseObj(value);
			}
			this.m_EventTimeStampAnim.Clear();
			foreach (KeyValuePair<float, NKMTimeStamp> keyValuePair in this.m_EventTimeStampState)
			{
				NKMTimeStamp value2 = keyValuePair.Value;
				this.m_NKMGame.GetObjectPool().CloseObj(value2);
			}
			this.m_EventTimeStampState.Clear();
			this.SetStateCoolTime(this.m_UnitStateNow, false, 1f);
			if (this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE != NKM_UNIT_STATE_TYPE.NUST_DAMAGE && this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE != NKM_UNIT_STATE_TYPE.NUST_START)
			{
				this.SeeTarget();
			}
			if (this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE != NKM_UNIT_STATE_TYPE.NUST_DAMAGE && this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE != NKM_UNIT_STATE_TYPE.NUST_START && this.GetUnitTemplet().m_bSeeMoreEnemy)
			{
				this.SeeMoreEnemy();
			}
			if (this.m_NKM_UNIT_CLASS_TYPE == NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
			{
				this.AccumStateProcess();
			}
			NKMUnit.StateChangeEvent stateChangeEvent = this.dStateChangeEvent;
			if (stateChangeEvent != null)
			{
				stateChangeEvent(NKM_UNIT_STATE_CHANGE_TYPE.NUSCT_START, this, this.m_UnitStateNow);
			}
			if (this.m_UnitStateNow.m_bForceRight)
			{
				if (!this.m_UnitStateNow.m_bForceRightLeftDependTeam)
				{
					this.m_UnitSyncData.m_bRight = true;
				}
				else if (this.IsATeam())
				{
					this.m_UnitSyncData.m_bRight = true;
				}
				else
				{
					this.m_UnitSyncData.m_bRight = false;
				}
			}
			if (this.m_UnitStateNow.m_bForceLeft)
			{
				if (!this.m_UnitStateNow.m_bForceRightLeftDependTeam)
				{
					this.m_UnitSyncData.m_bRight = false;
				}
				else if (this.IsATeam())
				{
					this.m_UnitSyncData.m_bRight = false;
				}
				else
				{
					this.m_UnitSyncData.m_bRight = true;
				}
			}
			this.GetUnitFrameData().m_fDangerChargeTime = this.m_UnitStateNow.m_DangerCharge.m_fChargeTime;
			this.GetUnitFrameData().m_fDangerChargeDamage = 0f;
			this.GetUnitFrameData().m_DangerChargeHitCount = 0;
			if (this.m_UnitStateNow.m_bInvincibleState || this.GetUnitFrameData().m_bInvincible || this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_INVINCIBLE))
			{
				this.RemoveStatus(NKM_UNIT_STATUS_EFFECT.NUSE_STUN);
				this.RemoveStatus(NKM_UNIT_STATUS_EFFECT.NUSE_SLEEP);
			}
			if (this.m_UnitStateNow.m_StatusEffectType == NKM_UNIT_STATUS_EFFECT.NUSE_STUN)
			{
				this.m_UnitFrameData.m_fSpeedX = 0f;
				this.m_UnitFrameData.m_fDamageSpeedX = 0f;
				this.m_UnitFrameData.m_fDamageSpeedKeepTimeX = 0f;
			}
			for (int i = 0; i < this.m_UnitTemplet.m_listHyperSkillStateData.Count; i++)
			{
				if (this.m_UnitStateNow.m_StateName == this.m_UnitTemplet.m_listHyperSkillStateData[i].m_StateName)
				{
					this.m_StateNameNext = "";
					this.m_StateNameNextChange = "";
					return;
				}
			}
			for (int j = 0; j < this.m_UnitTemplet.m_listAirHyperSkillStateData.Count; j++)
			{
				if (this.m_UnitStateNow.m_StateName == this.m_UnitTemplet.m_listAirHyperSkillStateData[j].m_StateName)
				{
					this.m_StateNameNext = "";
					this.m_StateNameNextChange = "";
					return;
				}
			}
			for (int k = 0; k < this.m_UnitTemplet.m_listPhaseChangeData.Count; k++)
			{
				if (this.m_UnitStateNow.m_StateName == this.m_UnitTemplet.m_listPhaseChangeData[k].m_ChangeStateName)
				{
					this.m_StateNameNext = "";
					this.m_StateNameNextChange = "";
					return;
				}
			}
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x0009797C File Offset: 0x00095B7C
		protected void AccumStateProcessUpdate()
		{
			for (int i = 0; i < this.GetUnitFrameData().m_listUnitAccumStateData.Count; i++)
			{
				foreach (KeyValuePair<string, NKMUnitAccumStateDataCount> keyValuePair in this.GetUnitFrameData().m_listUnitAccumStateData[i].m_dicAccumStateChange)
				{
					if (keyValuePair.Value.m_fCountCoolTimeNow > 0f)
					{
						keyValuePair.Value.m_fCountCoolTimeNow -= this.m_DeltaTime;
						if (keyValuePair.Value.m_fCountCoolTimeNow < 0f)
						{
							keyValuePair.Value.m_fCountCoolTimeNow = 0f;
						}
					}
					if (keyValuePair.Value.m_fMainCoolTimeNow > 0f)
					{
						keyValuePair.Value.m_fMainCoolTimeNow -= this.m_DeltaTime;
						if (keyValuePair.Value.m_fMainCoolTimeNow < 0f)
						{
							keyValuePair.Value.m_fMainCoolTimeNow = 0f;
						}
					}
				}
			}
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x00097AA0 File Offset: 0x00095CA0
		protected void AccumStateProcess()
		{
			for (int i = 0; i < this.GetUnitTemplet().m_listAccumStateChangePack.Count; i++)
			{
				NKMAccumStateChangePack nkmaccumStateChangePack = this.GetUnitTemplet().m_listAccumStateChangePack[i];
				if (nkmaccumStateChangePack != null && this.CheckEventCondition(nkmaccumStateChangePack.m_Condition))
				{
					for (int j = 0; j < nkmaccumStateChangePack.m_listAccumStateChange.Count; j++)
					{
						bool flag = false;
						NKMAccumStateChange nkmaccumStateChange = nkmaccumStateChangePack.m_listAccumStateChange[j];
						for (int k = 0; k < nkmaccumStateChange.m_listAccumStateName.Count; k++)
						{
							if (nkmaccumStateChange.m_listAccumStateName[k].CompareTo(this.m_UnitStateNow.m_StateName) == 0)
							{
								flag = true;
								break;
							}
						}
						if (flag && nkmaccumStateChange.m_listAccumStateName.Count > 0)
						{
							string key = nkmaccumStateChange.m_listAccumStateName[0];
							NKMUnitAccumStateData nkmunitAccumStateData = this.GetUnitFrameData().m_listUnitAccumStateData[i];
							if (!nkmunitAccumStateData.m_dicAccumStateChange.ContainsKey(key))
							{
								NKMUnitAccumStateDataCount nkmunitAccumStateDataCount = new NKMUnitAccumStateDataCount();
								nkmunitAccumStateDataCount.m_StateCount = 1;
								nkmunitAccumStateDataCount.m_fCountCoolTimeMax = nkmaccumStateChange.m_fAccumCountCoolTime;
								nkmunitAccumStateDataCount.m_fCountCoolTimeNow = nkmunitAccumStateDataCount.m_fCountCoolTimeMax;
								nkmunitAccumStateDataCount.m_fMainCoolTimeMax = nkmaccumStateChange.m_fAccumMainCoolTime;
								nkmunitAccumStateData.m_dicAccumStateChange.Add(key, nkmunitAccumStateDataCount);
							}
							else
							{
								NKMUnitAccumStateDataCount nkmunitAccumStateDataCount2 = nkmunitAccumStateData.m_dicAccumStateChange[key];
								if (nkmunitAccumStateDataCount2.m_fCountCoolTimeNow <= 0f)
								{
									NKMUnitAccumStateDataCount nkmunitAccumStateDataCount3 = nkmunitAccumStateDataCount2;
									nkmunitAccumStateDataCount3.m_StateCount += 1;
									nkmunitAccumStateDataCount2.m_fCountCoolTimeNow = nkmunitAccumStateDataCount2.m_fCountCoolTimeMax;
								}
							}
						}
					}
					if (this.GetUnitStateNow().m_NKM_SKILL_TYPE >= NKM_SKILL_TYPE.NST_SKILL)
					{
						return;
					}
					for (int l = 0; l < nkmaccumStateChangePack.m_listAccumStateChange.Count; l++)
					{
						NKMAccumStateChange nkmaccumStateChange2 = nkmaccumStateChangePack.m_listAccumStateChange[l];
						NKMUnitAccumStateDataCount nkmunitAccumStateDataCount4 = null;
						if (nkmaccumStateChange2.m_listAccumStateName.Count > 0)
						{
							string key2 = nkmaccumStateChange2.m_listAccumStateName[0];
							if (this.GetUnitFrameData().m_listUnitAccumStateData[i].m_dicAccumStateChange.TryGetValue(key2, out nkmunitAccumStateDataCount4) && nkmunitAccumStateDataCount4.m_fMainCoolTimeNow <= 0f && nkmunitAccumStateDataCount4.m_StateCount > nkmaccumStateChange2.m_AccumCount)
							{
								if (nkmaccumStateChange2.m_fRange.m_Min >= 0f || nkmaccumStateChange2.m_fRange.m_Max >= 0f)
								{
									if (this.m_TargetUnit == null)
									{
										goto IL_347;
									}
									float num = Math.Abs(this.m_TargetUnit.GetUnitSyncData().m_PosX - this.m_UnitSyncData.m_PosX) - (this.m_TargetUnit.GetUnitTemplet().m_UnitSizeX * 0.5f + this.m_UnitTemplet.m_UnitSizeX * 0.5f);
									if (num < 0f)
									{
										num = 0f;
									}
									if ((nkmaccumStateChange2.m_fRange.m_Min >= 0f && num < nkmaccumStateChange2.m_fRange.m_Min) || (nkmaccumStateChange2.m_fRange.m_Max >= 0f && num > nkmaccumStateChange2.m_fRange.m_Max))
									{
										goto IL_347;
									}
								}
								nkmunitAccumStateDataCount4.m_StateCount = 0;
								nkmunitAccumStateDataCount4.m_fCountCoolTimeNow = nkmunitAccumStateDataCount4.m_fCountCoolTimeMax;
								nkmunitAccumStateDataCount4.m_fMainCoolTimeNow = nkmunitAccumStateDataCount4.m_fMainCoolTimeMax;
								if (!this.IsDyingOrDie())
								{
									if (this.m_TargetUnit != null && this.m_TargetUnit.IsAirUnit() && nkmaccumStateChange2.m_AirTargetStateName.Length > 1)
									{
										this.StateChange(nkmaccumStateChange2.m_AirTargetStateName, true, false);
										return;
									}
									this.StateChange(nkmaccumStateChange2.m_TargetStateName, true, false);
								}
								return;
							}
						}
						IL_347:;
					}
				}
			}
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x00097E28 File Offset: 0x00096028
		protected virtual void ChangeAnimSpeed(float fAnimSpeed = 0f)
		{
			if (fAnimSpeed <= 0f)
			{
				fAnimSpeed = this.m_UnitFrameData.m_fAnimSpeedOrg;
			}
			else
			{
				this.m_UnitFrameData.m_fAnimSpeedOrg = fAnimSpeed;
			}
			if (this.m_UnitStateNow.m_bAnimSpeedFix)
			{
				return;
			}
			if (this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE == NKM_UNIT_STATE_TYPE.NUST_ATTACK || this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE == NKM_UNIT_STATE_TYPE.NUST_SKILL)
			{
				if (!this.m_UnitStateNow.m_bNotUseAttackSpeedStat)
				{
					fAnimSpeed += fAnimSpeed * this.m_UnitFrameData.m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_ATTACK_SPEED_RATE);
				}
			}
			else if (this.m_UnitStateNow.m_bRun)
			{
				fAnimSpeed += fAnimSpeed * this.m_UnitFrameData.m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_MOVE_SPEED_RATE);
			}
			this.m_UnitFrameData.m_fAnimSpeed = fAnimSpeed;
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x00097EDC File Offset: 0x000960DC
		public void SetStateCoolTime(Dictionary<int, float> dicStateCoolTime)
		{
			if (dicStateCoolTime == null)
			{
				return;
			}
			foreach (KeyValuePair<int, NKMStateCoolTime> keyValuePair in this.m_dicStateCoolTime)
			{
				NKMStateCoolTime value = keyValuePair.Value;
				this.m_NKMGame.GetObjectPool().CloseObj(value);
			}
			this.m_dicStateCoolTime.Clear();
			foreach (KeyValuePair<int, float> keyValuePair2 in dicStateCoolTime)
			{
				if (keyValuePair2.Value > 0f)
				{
					NKMStateCoolTime nkmstateCoolTime = (NKMStateCoolTime)this.m_NKMGame.GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKMStateCoolTime, "", "", false);
					nkmstateCoolTime.m_CoolTime = keyValuePair2.Value;
					this.m_dicStateCoolTime.Add(keyValuePair2.Key, nkmstateCoolTime);
				}
			}
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x00097FC0 File Offset: 0x000961C0
		public void SetStateCoolTime(string stateName, bool bMax, float ratio = 1f)
		{
			NKMUnitState unitState = this.GetUnitState(stateName, true);
			this.SetStateCoolTime(unitState, bMax, ratio);
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x00097FE0 File Offset: 0x000961E0
		protected void SetStateCoolTime(NKMUnitState cNKMUnitState, bool bMax, float ratio = 1f)
		{
			NKMUnitSkillTemplet stateSkill = this.GetStateSkill(cNKMUnitState);
			if (cNKMUnitState == null)
			{
				return;
			}
			if (stateSkill != null || cNKMUnitState.m_StateCoolTime.m_Max > 0f)
			{
				float num;
				if (stateSkill != null && stateSkill.m_fCooltimeSecond > 0f)
				{
					num = stateSkill.m_fCooltimeSecond;
				}
				else if (bMax)
				{
					num = cNKMUnitState.m_StateCoolTime.m_Max;
				}
				else
				{
					num = cNKMUnitState.m_StateCoolTime.GetRandom();
				}
				num = this.GetCoolTimeReducedByOperator(num);
				num *= ratio;
				this.SetStateCoolTime(cNKMUnitState, num);
			}
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x00098058 File Offset: 0x00096258
		public void SetStateCoolTimeAdd(string stateName, float fAddCool)
		{
			NKMUnitState unitState = this.GetUnitState(stateName, true);
			if (unitState != null)
			{
				this.SetStateCoolTimeAdd(unitState, fAddCool);
			}
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x0009807C File Offset: 0x0009627C
		protected void SetStateCoolTimeAdd(NKMUnitState cNKMUnitState, float fAddCool)
		{
			float stateMaxCoolTime = this.GetStateMaxCoolTime(cNKMUnitState.m_StateName);
			float num = this.GetStateCoolTime(cNKMUnitState.m_StateName);
			num += fAddCool;
			if (num > stateMaxCoolTime)
			{
				num = stateMaxCoolTime;
			}
			if (num < 0f)
			{
				num = 0f;
			}
			this.SetStateCoolTime(cNKMUnitState, num);
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x000980C4 File Offset: 0x000962C4
		protected void SetStateCoolTime(NKMUnitState cNKMUnitState, float fCoolTime)
		{
			if (!this.m_dicStateCoolTime.ContainsKey((int)cNKMUnitState.m_StateID))
			{
				if (fCoolTime > 0f)
				{
					NKMStateCoolTime nkmstateCoolTime = (NKMStateCoolTime)this.m_NKMGame.GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKMStateCoolTime, "", "", false);
					nkmstateCoolTime.m_CoolTime = fCoolTime;
					this.m_dicStateCoolTime.Add((int)cNKMUnitState.m_StateID, nkmstateCoolTime);
				}
			}
			else
			{
				this.m_dicStateCoolTime[(int)cNKMUnitState.m_StateID].m_CoolTime = fCoolTime;
			}
			for (int i = 0; i < cNKMUnitState.m_listCoolTimeLink.Count; i++)
			{
				NKMUnitState unitState = this.GetUnitState(cNKMUnitState.m_listCoolTimeLink[i], true);
				if (unitState != null)
				{
					if (!this.m_dicStateCoolTime.ContainsKey((int)unitState.m_StateID))
					{
						if (fCoolTime > 0f)
						{
							NKMStateCoolTime nkmstateCoolTime2 = (NKMStateCoolTime)this.m_NKMGame.GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKMStateCoolTime, "", "", false);
							nkmstateCoolTime2.m_CoolTime = fCoolTime;
							this.m_dicStateCoolTime.Add((int)unitState.m_StateID, nkmstateCoolTime2);
						}
					}
					else
					{
						this.m_dicStateCoolTime[(int)unitState.m_StateID].m_CoolTime = fCoolTime;
					}
				}
			}
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x000981E4 File Offset: 0x000963E4
		public float GetStateMaxCoolTime(string stateName)
		{
			NKMUnitState unitState = this.GetUnitState(stateName, true);
			if (unitState == null)
			{
				return 0f;
			}
			if (this.m_dicStateMaxCoolTime.ContainsKey((int)unitState.m_StateID))
			{
				return this.m_dicStateMaxCoolTime[(int)unitState.m_StateID];
			}
			NKMUnitSkillTemplet stateSkill = this.GetStateSkill(unitState);
			float num;
			if (stateSkill != null)
			{
				num = stateSkill.m_fCooltimeSecond;
				if (0f < unitState.m_StateCoolTime.m_Max && unitState.m_StateCoolTime.m_Max < stateSkill.m_fCooltimeSecond)
				{
					Log.Warn(string.Concat(new string[]
					{
						"unitID: ",
						this.GetUnitTemplet().m_UnitTempletBase.m_UnitStrID,
						", 스테이트 ",
						unitState.m_StateName,
						"의 쿨타임보다 해당 스테이트 스킬 템플릿 ",
						stateSkill.m_strID,
						"의 쿨타임이 깁니다. 스킬 템플릿의 쿨타임을 사용합니다. 혹시 쿨타임 설정을 빠트리셨나요?"
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnit.cs", 2101);
				}
				else if (stateSkill.m_fCooltimeSecond.IsNearlyZero(1E-05f))
				{
					Log.Warn(string.Concat(new string[]
					{
						"unitID: ",
						this.GetUnitTemplet().m_UnitTempletBase.m_UnitStrID,
						", 스테이트 ",
						unitState.m_StateName,
						"에 지정된 스킬 ",
						stateSkill.m_strID,
						"의 쿨타임이 0입니다.혹시 쿨타임 설정을 빠트리셨나요?"
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnit.cs", 2105);
				}
			}
			else
			{
				num = unitState.m_StateCoolTime.m_Max;
			}
			num = this.GetCoolTimeReducedByOperator(num);
			this.m_dicStateMaxCoolTime.Add((int)unitState.m_StateID, num);
			for (int i = 0; i < unitState.m_listCoolTimeLink.Count; i++)
			{
				NKMUnitState unitState2 = this.GetUnitState(unitState.m_listCoolTimeLink[i], true);
				if (unitState2 != null && !this.m_dicStateMaxCoolTime.ContainsKey((int)unitState2.m_StateID))
				{
					this.m_dicStateMaxCoolTime.Add((int)unitState2.m_StateID, num);
				}
			}
			return num;
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x000983B4 File Offset: 0x000965B4
		protected void ProcessCoolTime()
		{
			if (this.IsATeam() && this.m_NKMGame.GetGameData().m_TeamASupply <= 0)
			{
				return;
			}
			foreach (KeyValuePair<int, NKMStateCoolTime> keyValuePair in this.m_dicStateCoolTime)
			{
				NKMStateCoolTime value = keyValuePair.Value;
				if (value.m_CoolTime > 0f)
				{
					float num = 0f;
					if (this.m_NKMGame.IsATeam(this.m_UnitDataGame.m_NKM_TEAM_TYPE))
					{
						num = this.m_NKMGame.GetCoolTimeReduceTeamA();
					}
					else if (this.m_NKMGame.IsBTeam(this.m_UnitDataGame.m_NKM_TEAM_TYPE))
					{
						num = this.m_NKMGame.GetCoolTimeReduceTeamB();
					}
					bool flag = false;
					NKMUnitTemplet unitTemplet = this.GetUnitTemplet();
					Dictionary<int, NKMStateCoolTime>.Enumerator enumerator;
					keyValuePair = enumerator.Current;
					float num2;
					switch (unitTemplet.GetAttackStateType(keyValuePair.Key))
					{
					case NKM_UNIT_ATTACK_STATE_TYPE.INVALID:
					case NKM_UNIT_ATTACK_STATE_TYPE.PASSIVE:
					case NKM_UNIT_ATTACK_STATE_TYPE.AIR_PASSIVE:
						goto IL_15E;
					case NKM_UNIT_ATTACK_STATE_TYPE.ATTACK:
					case NKM_UNIT_ATTACK_STATE_TYPE.AIR_ATTACK:
						if (!this.GetUnitTemplet().m_UnitTempletBase.m_bMonster)
						{
							num2 = this.m_UnitFrameData.m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_ATTACK_SPEED_RATE);
						}
						else
						{
							num2 = 0f;
						}
						break;
					case NKM_UNIT_ATTACK_STATE_TYPE.SKILL:
					case NKM_UNIT_ATTACK_STATE_TYPE.AIR_SKILL:
					case NKM_UNIT_ATTACK_STATE_TYPE.HYPER:
					case NKM_UNIT_ATTACK_STATE_TYPE.AIR_HYPER:
						num2 = this.m_UnitFrameData.m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE);
						if (this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_SILENCE))
						{
							flag = true;
						}
						if (this.GetUnitTemplet().m_UnitTempletBase != null && this.GetUnitTemplet().m_UnitTempletBase.StopDefaultCoolTime)
						{
							continue;
						}
						break;
					default:
						goto IL_15E;
					}
					IL_165:
					for (int i = 0; i < this.m_listShipSkillStateID.Count; i++)
					{
						int num3 = this.m_listShipSkillStateID[i];
						keyValuePair = enumerator.Current;
						if (num3 == keyValuePair.Key)
						{
							num2 = this.m_UnitFrameData.m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_SKILL_COOL_TIME_REDUCE_RATE);
							if (this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_SILENCE))
							{
								flag = true;
							}
						}
					}
					if (flag)
					{
						continue;
					}
					float num4 = 1f + num + num2;
					if (num4 < 0f)
					{
						num4 = 0f;
					}
					value.m_CoolTime -= this.m_DeltaTime * num4;
					if (value.m_CoolTime <= 0f)
					{
						value.m_CoolTime = 0f;
						continue;
					}
					continue;
					IL_15E:
					num2 = 0f;
					goto IL_165;
				}
			}
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x000985D8 File Offset: 0x000967D8
		protected void DecreaseSkillStateCoolTime(float decreaseValue)
		{
			if (decreaseValue <= 0f)
			{
				return;
			}
			if (this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_SILENCE))
			{
				return;
			}
			foreach (KeyValuePair<int, NKMStateCoolTime> keyValuePair in this.m_dicStateCoolTime)
			{
				NKMStateCoolTime value = keyValuePair.Value;
				if (value.m_CoolTime > 0f)
				{
					NKMUnitTemplet unitTemplet = this.GetUnitTemplet();
					Dictionary<int, NKMStateCoolTime>.Enumerator enumerator;
					keyValuePair = enumerator.Current;
					NKM_UNIT_ATTACK_STATE_TYPE attackStateType = unitTemplet.GetAttackStateType(keyValuePair.Key);
					if (attackStateType - NKM_UNIT_ATTACK_STATE_TYPE.SKILL <= 3)
					{
						value.m_CoolTime -= decreaseValue;
					}
					for (int i = 0; i < this.m_listShipSkillStateID.Count; i++)
					{
						int num = this.m_listShipSkillStateID[i];
						keyValuePair = enumerator.Current;
						if (num == keyValuePair.Key)
						{
							value.m_CoolTime -= decreaseValue;
						}
					}
					if (value.m_CoolTime <= 0f)
					{
						value.m_CoolTime = 0f;
					}
				}
			}
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x000986C4 File Offset: 0x000968C4
		protected float RegenHPThisFrame(float fRegenRate)
		{
			float statFinal = this.m_UnitFrameData.m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP);
			float num = statFinal * fRegenRate * this.m_DeltaTime;
			if (this.m_UnitSyncData.GetHP() >= statFinal && num > 0f)
			{
				num = 0f;
			}
			if (this.m_UnitSyncData.GetHP() <= 0f && num < 0f)
			{
				num = 0f;
			}
			bool flag = false;
			if (this.GetUnitFrameData().m_bInvincible || this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_INVINCIBLE))
			{
				flag = true;
			}
			if (this.m_UnitStateNow != null && this.m_UnitStateNow.m_bInvincibleState)
			{
				flag = true;
			}
			if (num < 0f && flag)
			{
				num = 0f;
			}
			if (this.GetUnitFrameData().m_fHealFeedback > 0f)
			{
				num = 0f;
			}
			if (this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_IRONWALL))
			{
				num = 0f;
			}
			if (num > 0f && this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_NOHEAL))
			{
				num = 0f;
			}
			return num;
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x000987B0 File Offset: 0x000969B0
		protected virtual void StateUpdate()
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			bool flag = this.m_NKMGame.GetWorldStopTime() > 0f;
			this.m_TargetUnit = this.GetTargetUnit(false);
			if (this.m_TargetUnit != null && this.m_TargetUnit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DYING && this.m_TargetUnit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DIE)
			{
				this.GetUnitFrameData().m_LastTargetPosX = this.m_TargetUnit.GetUnitSyncData().m_PosX;
				this.GetUnitFrameData().m_LastTargetPosZ = this.m_TargetUnit.GetUnitSyncData().m_PosZ;
				this.GetUnitFrameData().m_LastTargetJumpYPos = this.m_TargetUnit.GetUnitSyncData().m_JumpYPos;
			}
			this.m_SubTargetUnit = this.GetTargetUnit(this.m_UnitSyncData.m_SubTargetUID, this.m_UnitTemplet.m_SubTargetFindData);
			this.m_dicKillFeedBackGameUnitUID.Clear();
			if (this.m_UnitStateNow.m_bRun && this.m_UnitFrameData.m_fSpeedX < this.m_UnitTemplet.m_SpeedRun * this.m_UnitStateNow.m_fRunSpeedRate)
			{
				float num = this.m_UnitTemplet.m_SpeedRun * this.m_UnitStateNow.m_fRunSpeedRate;
				num += num * this.m_UnitFrameData.m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_MOVE_SPEED_RATE);
				this.m_UnitFrameData.m_fSpeedX = num;
			}
			if (this.m_NKM_UNIT_CLASS_TYPE == NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
			{
				if (!flag)
				{
					this.HPProcess();
				}
				if (this.m_TargetUnit == null || this.m_TargetUnit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DYING || this.m_TargetUnit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DIE)
				{
					this.m_UnitFrameData.m_fTargetLostDurationTime += this.m_DeltaTime;
				}
				else
				{
					this.m_UnitFrameData.m_fTargetLostDurationTime = 0f;
				}
				if (this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE != NKM_UNIT_STATE_TYPE.NUST_START && this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE != NKM_UNIT_STATE_TYPE.NUST_DAMAGE && !this.m_UnitStateNow.m_bNoAI)
				{
					this.ProcessAI();
				}
			}
			else if (!flag && this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_PLAY)
			{
				if (this.m_UnitSyncData.GetHP() > 1f)
				{
					float statFinal = this.m_UnitFrameData.m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP);
					float val = this.RegenHPThisFrame(this.m_UnitFrameData.m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP_REGEN_RATE)) + this.m_UnitSyncData.GetHP();
					this.m_UnitSyncData.SetHP(Math.Min(val, statFinal));
				}
				if (this.m_UnitSyncData.GetHP() < 1f)
				{
					this.m_UnitSyncData.SetHP(1f);
				}
			}
			if (this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE != NKM_UNIT_STATE_TYPE.NUST_DAMAGE && this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE != NKM_UNIT_STATE_TYPE.NUST_START && this.m_UnitStateNow.m_bSeeTarget)
			{
				this.SeeTarget();
			}
			if (this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE != NKM_UNIT_STATE_TYPE.NUST_DAMAGE && this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE != NKM_UNIT_STATE_TYPE.NUST_START && this.m_UnitStateNow.m_bSeeMoreEnemy)
			{
				this.SeeMoreEnemy();
			}
			this.GetUnitFrameData().m_fLiveTime += this.m_DeltaTime;
			if (!flag)
			{
				this.ProcessCoolTime();
			}
			if (this.m_UnitStateNow != null)
			{
				this.ProcessStateEvent<NKMEventFindTarget>(this.m_UnitStateNow.m_listNKMEventFindTarget, false);
			}
			this.ProcessEventText(false);
			this.ProcessEventSpeed(false);
			this.ProcessEventSpeedX(false);
			this.ProcessEventSpeedY(false);
			this.ProcessEventMove(false);
			this.ProcessEventAttack();
			this.ProcessEventStopTime(false);
			this.ProcessEventInvincible();
			this.ProcessEventInvincibleGlobal(false);
			this.ProcessEventSuperArmor();
			this.ProcessEventSound(false);
			this.ProcessEventColor(false);
			this.ProcessEventCameraCrash(false);
			this.ProcessEventCameraMove(false);
			this.ProcessEventFadeWorld();
			this.ProcessEventDissolve(false);
			this.ProcessEventMotionBlur();
			this.ProcessEventEffect(false);
			this.ProcessEventHyperSkillCutIn();
			this.ProcessEventDamageEffect(false);
			this.ProcessEventDEStateChange(false);
			this.ProcessEventGameSpeed(false);
			this.ProcessEventAnimSpeed();
			this.ProcessEventBuff(false);
			this.ProcessEventStatus(false);
			this.ProcessEventRespawn(false);
			this.ProcessEventUnitChange(false);
			this.ProcessEventDie(false);
			this.ProcessEventChangeState(false);
			this.ProcessEventAgro(false);
			this.ProcessEventHeal(false);
			this.ProcessEventCost(false);
			this.ProcessEventDispel(false);
			this.ProcessEventStun(false);
			this.ProcessEventCatch(false);
			this.ProcessEventChangeCooltime();
			this.ProcessAutoShipSkill();
			this.ProcessDangerCharge();
			this.AccumStateProcessUpdate();
			if (!flag)
			{
				this.ProcessStatusApply();
				this.ProcessBuff();
				this.ProcessStatusAffect();
			}
			this.ProcessStaticBuff();
			this.ProcessLeaguePvpRageBuff();
			this.ProcessLeaguePvpDeadlineBuff();
			this.PhysicProcess();
			this.MapEdgeProcess();
			foreach (KeyValuePair<float, NKMTimeStamp> keyValuePair in this.m_EventTimeStampAnim)
			{
				keyValuePair.Value.m_FramePass = true;
			}
			foreach (KeyValuePair<float, NKMTimeStamp> keyValuePair in this.m_EventTimeStampState)
			{
				keyValuePair.Value.m_FramePass = true;
			}
			this.m_UnitFrameData.m_fHitLightTime -= this.m_DeltaTime;
			if (this.m_UnitFrameData.m_fHitLightTime < 0f)
			{
				this.m_UnitFrameData.m_fHitLightTime = 0f;
			}
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x00098CA4 File Offset: 0x00096EA4
		protected virtual void StateEvent()
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			if (this.m_NKMGame.GetGameRuntimeData().m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_FINISH && this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_PLAY && this.m_UnitFrameData.m_fAnimTime >= this.m_UnitFrameData.m_fAnimTimeMax)
			{
				this.StateChangeToASTAND(false, false);
				return;
			}
			if (this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE == NKM_UNIT_STATE_TYPE.NUST_DIE && this.m_UnitFrameData.m_fStateTime >= this.GetUnitTemplet().m_fDieCompleteTime)
			{
				if (this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DIE)
				{
					this.m_PushSyncData = true;
					this.SetDie(true);
				}
				return;
			}
			if (this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DYING && this.m_UnitStateNow.m_AnimEndDyingState.Length > 1 && this.m_UnitFrameData.m_fAnimTime >= this.m_UnitFrameData.m_fAnimTimeMax)
			{
				this.StateChange(this.m_UnitStateNow.m_AnimEndDyingState, true, false);
				return;
			}
			if (this.m_UnitFrameData.m_bFootOnLand && this.m_UnitSyncData.GetHP().IsNearlyZero(1E-05f) && this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE != NKM_UNIT_STATE_TYPE.NUST_DIE && this.m_UnitFrameData.m_fAnimTime >= this.m_UnitFrameData.m_fAnimTimeMax)
			{
				this.StateChange(this.m_UnitTemplet.m_DyingState, true, false);
				return;
			}
			if (this.StateEvent_Phase(false))
			{
				return;
			}
			if (this.m_UnitStateNow.m_AnimTimeChangeStateTime >= 0f && this.m_UnitFrameData.m_fAnimTime >= this.m_UnitStateNow.m_AnimTimeChangeStateTime)
			{
				this.StateChange(this.m_UnitStateNow.m_AnimTimeChangeState, true, false);
				return;
			}
			if (this.m_UnitStateNow.m_AnimTimeRateChangeStateTime >= 0f && this.m_UnitFrameData.m_fAnimTimeMax > 0f && this.m_UnitFrameData.m_fAnimTime / this.m_UnitFrameData.m_fAnimTimeMax >= this.m_UnitStateNow.m_AnimTimeRateChangeStateTime)
			{
				this.StateChange(this.m_UnitStateNow.m_AnimTimeRateChangeState, true, false);
				return;
			}
			if (this.m_UnitStateNow.m_StateTimeChangeStateTime >= 0f && this.m_UnitFrameData.m_fStateTime >= this.m_UnitStateNow.m_StateTimeChangeStateTime)
			{
				this.StateChange(this.m_UnitStateNow.m_StateTimeChangeState, true, false);
				return;
			}
			if (this.m_UnitStateNow.m_TargetDistOverChangeState.Length > 1 && this.m_TargetUnit != null && Math.Abs(this.m_TargetUnit.GetUnitSyncData().m_PosX - this.m_UnitSyncData.m_PosX) - (this.m_TargetUnit.GetUnitTemplet().m_UnitSizeX * 0.5f + this.m_UnitTemplet.m_UnitSizeX * 0.5f) > this.m_UnitStateNow.m_TargetDistOverChangeStateDist)
			{
				this.StateChange(this.m_UnitStateNow.m_TargetDistOverChangeState, true, false);
				return;
			}
			if (this.m_UnitStateNow.m_TargetDistLessChangeState.Length > 1 && this.m_TargetUnit != null && Math.Abs(this.m_TargetUnit.GetUnitSyncData().m_PosX - this.m_UnitSyncData.m_PosX) - (this.m_TargetUnit.GetUnitTemplet().m_UnitSizeX * 0.5f + this.m_UnitTemplet.m_UnitSizeX * 0.5f) < this.m_UnitStateNow.m_TargetDistLessChangeStateDist)
			{
				this.StateChange(this.m_UnitStateNow.m_TargetDistLessChangeState, true, false);
				return;
			}
			if (this.m_UnitStateNow.m_TargetLostOrDieState.Length > 1 && (this.m_TargetUnit == null || this.m_TargetUnit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DYING || this.m_TargetUnit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DIE))
			{
				if (this.m_UnitFrameData.m_fTargetLostDurationTime >= this.m_UnitStateNow.m_TargetLostOrDieStateDurationTime)
				{
					this.StateChange(this.m_UnitStateNow.m_TargetLostOrDieState, true, false);
					return;
				}
				this.m_UnitFrameData.m_fTargetLostDurationTime += this.m_DeltaTime;
			}
			if (this.m_UnitStateNow.m_AnimEndChangeState.Length > 1 && !this.m_UnitStateNow.m_bAnimLoop && this.m_UnitStateNow.m_AnimEndChangeStatePlayCount <= this.m_UnitFrameData.m_AnimPlayCount)
			{
				this.StateChange(this.m_UnitStateNow.m_AnimEndChangeState, true, false);
				return;
			}
			if (this.m_UnitStateNow.m_ChangeRightTrueState.Length > 1 && this.m_bRightOrg != this.m_UnitSyncData.m_bRight && this.m_UnitSyncData.m_bRight)
			{
				this.StateChange(this.m_UnitStateNow.m_ChangeRightTrueState, true, false);
				return;
			}
			if (this.m_UnitStateNow.m_ChangeRightFalseState.Length > 1 && this.m_bRightOrg != this.m_UnitSyncData.m_bRight && !this.m_UnitSyncData.m_bRight)
			{
				this.StateChange(this.m_UnitStateNow.m_ChangeRightFalseState, true, false);
				return;
			}
			if (this.m_UnitStateNow.m_AirTargetThisFrameChangeState.Length > 1 && this.GetUnitFrameData().m_bTargetChangeThisFrame && this.m_TargetUnit != null && !this.m_TargetUnit.IsDyingOrDie() && this.m_TargetUnit.IsAirUnit())
			{
				this.StateChange(this.m_UnitStateNow.m_AirTargetThisFrameChangeState, true, false);
				return;
			}
			if (this.m_UnitStateNow.m_LandTargetThisFrameChangeState.Length > 1 && this.GetUnitFrameData().m_bTargetChangeThisFrame && this.m_TargetUnit != null && !this.m_TargetUnit.IsDyingOrDie() && !this.m_TargetUnit.IsAirUnit())
			{
				this.StateChange(this.m_UnitStateNow.m_LandTargetThisFrameChangeState, true, false);
				return;
			}
			if (this.m_UnitStateNow.m_ChangeRightState.Length > 1 && this.m_bRightOrg != this.m_UnitSyncData.m_bRight)
			{
				this.StateChange(this.m_UnitStateNow.m_ChangeRightState, true, false);
				return;
			}
			if (this.m_UnitStateNow.m_FootOnLandChangeState.Length > 1 && this.m_UnitFrameData.m_bFootOnLand)
			{
				this.StateChange(this.m_UnitStateNow.m_FootOnLandChangeState, true, false);
				return;
			}
			if (this.m_UnitStateNow.m_FootOffLandChangeState.Length > 1 && !this.m_UnitFrameData.m_bFootOnLand)
			{
				this.StateChange(this.m_UnitStateNow.m_FootOffLandChangeState, true, false);
				return;
			}
			if (this.m_UnitStateNow.m_AnimEndFootOnLandChangeState.Length > 1 && this.m_UnitFrameData.m_fAnimTime >= this.m_UnitFrameData.m_fAnimTimeMax && this.m_UnitFrameData.m_bFootOnLand)
			{
				this.StateChange(this.m_UnitStateNow.m_AnimEndFootOnLandChangeState, true, false);
				return;
			}
			if (this.m_UnitStateNow.m_AnimEndFootOffLandChangeState.Length > 1 && this.m_UnitFrameData.m_fAnimTime >= this.m_UnitFrameData.m_fAnimTimeMax && !this.m_UnitFrameData.m_bFootOnLand)
			{
				this.StateChange(this.m_UnitStateNow.m_AnimEndFootOffLandChangeState, true, false);
				return;
			}
			if (this.m_UnitStateNow.m_SpeedYPositiveChangeState.Length > 1 && this.m_UnitFrameData.m_fSpeedY + this.m_UnitFrameData.m_fDamageSpeedJumpY > 0f)
			{
				this.StateChange(this.m_UnitStateNow.m_SpeedYPositiveChangeState, true, false);
				return;
			}
			if (this.m_UnitStateNow.m_SpeedY0NegativeChangeState.Length > 1 && this.m_UnitFrameData.m_fSpeedY + this.m_UnitFrameData.m_fDamageSpeedJumpY <= 0f)
			{
				this.StateChange(this.m_UnitStateNow.m_SpeedY0NegativeChangeState, true, false);
				return;
			}
			if (this.m_UnitStateNow.m_DamagedChangeState.Length > 1 && this.m_UnitFrameData.m_fDamageBeforeFrame > 0f && (this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE != NKM_UNIT_STATE_TYPE.NUST_START || this.m_UnitFrameData.m_fStateTime > 0.5f) && this.m_StateNameNext.Length <= 1 && this.m_StateNameNextChange.Length <= 1)
			{
				this.StateChange(this.m_UnitStateNow.m_DamagedChangeState, true, false);
				return;
			}
			if (this.m_UnitStateNow.m_MapPosOverState.Length > 1 && this.m_UnitStateNow.m_MapPosOverStatePos <= this.m_NKMGame.GetMapTemplet().GetMapFactor(this.GetUnitSyncData().m_PosX, this.m_NKMGame.IsATeam(this.GetUnitDataGame().m_NKM_TEAM_TYPE)))
			{
				this.StateChange(this.m_UnitStateNow.m_MapPosOverState, true, false);
				return;
			}
			if (this.GetUnitTemplet().m_MapPosOverState.Length > 1 && this.GetUnitTemplet().m_MapPosOverStatePos <= this.m_NKMGame.GetMapTemplet().GetMapFactor(this.GetUnitSyncData().m_PosX, this.m_NKMGame.IsATeam(this.GetUnitDataGame().m_NKM_TEAM_TYPE)))
			{
				this.StateChange(this.GetUnitTemplet().m_MapPosOverState, true, false);
				return;
			}
			if (this.m_UnitStateNow.IsBadStatusState && this.m_UnitStateNow.m_StatusEffectType != NKM_UNIT_STATUS_EFFECT.NUSE_NONE && !this.HasStatus(this.m_UnitStateNow.m_StatusEffectType))
			{
				this.StateChangeToASTAND(false, false);
				return;
			}
			bool flag = this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE != NKM_UNIT_STATE_TYPE.NUST_DAMAGE && this.m_UnitFrameData.m_bFootOnLand;
			bool flag2 = this.m_UnitSyncData.GetHP() > 0f && this.m_UnitStateNow.m_NKMEventUnitChange == null;
			if (flag2)
			{
				if (this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_FREEZE))
				{
					this.StateChange("USN_DAMAGE_FREEZE", false, false);
					return;
				}
				if (this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_HOLD))
				{
					this.StateChange("USN_DAMAGE_HOLD", false, false);
					return;
				}
			}
			if (flag2 && flag)
			{
				if (this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_STUN))
				{
					this.StateChange("USN_DAMAGE_STUN", false, false);
					return;
				}
				if (this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_SLEEP))
				{
					this.StateChange("USN_DAMAGE_SLEEP", false, false);
					return;
				}
				if (this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_FEAR))
				{
					if (this.GetUnitTemplet().m_bNoMove || this.GetUnitTemplet().m_bNoRun)
					{
						this.StateChange("USN_DAMAGE_FEAR_NOMOVE", false, false);
						return;
					}
					this.StateChange("USN_DAMAGE_FEAR", false, false);
					return;
				}
			}
			if (!this.m_UnitStateNow.m_bNoStateTypeEvent)
			{
				if (this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE == NKM_UNIT_STATE_TYPE.NUST_ASTAND && this.StateEvent_NUST_ASTAND())
				{
					return;
				}
				if (this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE == NKM_UNIT_STATE_TYPE.NUST_MOVE && this.StateEvent_NUST_MOVE())
				{
					return;
				}
				if ((this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE == NKM_UNIT_STATE_TYPE.NUST_ATTACK || this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE == NKM_UNIT_STATE_TYPE.NUST_SKILL) && this.StateEvent_NUST_ATTACK())
				{
					return;
				}
				if (this.m_UnitStateNow.m_StatusEffectType == NKM_UNIT_STATUS_EFFECT.NUSE_FEAR)
				{
					this.StateEvent_NUSE_FEAR();
					return;
				}
			}
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x0009967C File Offset: 0x0009787C
		protected virtual bool StateEvent_Phase(bool bRespawnTime = false)
		{
			for (int i = 0; i < this.GetUnitTemplet().m_listPhaseChangeData.Count; i++)
			{
				NKMPhaseChangeData nkmphaseChangeData = this.GetUnitTemplet().m_listPhaseChangeData[i];
				if (nkmphaseChangeData != null && (bRespawnTime || (this.m_UnitStateNow != null && this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE != NKM_UNIT_STATE_TYPE.NUST_START && this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE != NKM_UNIT_STATE_TYPE.NUST_SKILL && this.m_UnitStateNow.m_NKM_SKILL_TYPE != NKM_SKILL_TYPE.NST_SKILL && this.m_UnitStateNow.m_NKM_SKILL_TYPE != NKM_SKILL_TYPE.NST_HYPER)) && this.GetHPRate() > 0f && !this.IsDyingOrDie() && this.CheckEventCondition(nkmphaseChangeData.m_Condition) && nkmphaseChangeData.m_TargetPhase > this.GetUnitFrameData().m_PhaseNow && (nkmphaseChangeData.m_fChangeConditionHPRate <= 0f || nkmphaseChangeData.m_fChangeConditionHPRate >= this.GetHPRate()) && (nkmphaseChangeData.m_fChangeConditionTime <= 0f || nkmphaseChangeData.m_fChangeConditionTime <= this.GetUnitFrameData().m_fLiveTime) && (nkmphaseChangeData.m_ChangeConditionMyKill <= 0 || nkmphaseChangeData.m_ChangeConditionMyKill <= this.GetUnitFrameData().m_KillCount) && (!nkmphaseChangeData.m_bChangeConditionImmortalStart || this.GetUnitFrameData().m_bImmortalStart))
				{
					for (int j = 0; j < nkmphaseChangeData.m_listChangeCoolTime.Count; j++)
					{
						NKMPhaseChangeCoolTime nkmphaseChangeCoolTime = nkmphaseChangeData.m_listChangeCoolTime[j];
						this.SetStateCoolTime(nkmphaseChangeCoolTime.m_StateName, false, nkmphaseChangeCoolTime.m_fCoolTime);
					}
					if (nkmphaseChangeData.m_ChangeStateName.Length > 1 && !bRespawnTime)
					{
						this.StateChange(nkmphaseChangeData.m_ChangeStateName, true, false);
					}
					this.GetUnitFrameData().m_PhaseNow = nkmphaseChangeData.m_TargetPhase;
					if (!bRespawnTime)
					{
						this.ApplyStatusTime(NKM_UNIT_STATUS_EFFECT.NUSE_INVINCIBLE, 0.1f, this, false, true, true);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x00099848 File Offset: 0x00097A48
		public float GetPhaseDamageLimit(out bool cutDamage)
		{
			cutDamage = false;
			float num = 0f;
			for (int i = 0; i < this.GetUnitTemplet().m_listPhaseChangeData.Count; i++)
			{
				NKMPhaseChangeData nkmphaseChangeData = this.GetUnitTemplet().m_listPhaseChangeData[i];
				if (nkmphaseChangeData != null && this.m_UnitStateNow != null && !this.IsDyingOrDie() && this.CheckEventCondition(nkmphaseChangeData.m_Condition) && nkmphaseChangeData.m_TargetPhase > this.GetUnitFrameData().m_PhaseNow && nkmphaseChangeData.m_fChangeConditionHPRate > 0f && num < this.GetHPRateToValue(nkmphaseChangeData.m_fChangeConditionHPRate))
				{
					num = this.GetHPRateToValue(nkmphaseChangeData.m_fChangeConditionHPRate);
					cutDamage = nkmphaseChangeData.m_bCutHpDamage;
				}
			}
			return num;
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x000998F4 File Offset: 0x00097AF4
		protected virtual bool StateEvent_NUST_ASTAND()
		{
			if (this.m_NKMGame.GetGameRuntimeData().m_NKM_GAME_STATE < NKM_GAME_STATE.NGS_PLAY)
			{
				return false;
			}
			if (this.m_UnitTemplet.m_UnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL && this.m_UnitSyncData.m_TargetUID <= 0)
			{
				float num = 99999f;
				bool bRight = this.m_UnitSyncData.m_bRight;
				bool idleDirRight = this.GetIdleDirRight(ref bRight, ref num) != null;
				if (this.m_UnitStateNow != null && !this.m_UnitStateNow.m_bNoChangeRight)
				{
					this.m_UnitSyncData.m_bRight = bRight;
				}
				if (!idleDirRight && this.GetUnitTemplet().m_UnitTempletBase.m_NKM_FIND_TARGET_TYPE != NKM_FIND_TARGET_TYPE.NFTT_NO)
				{
					this.StateChangeToASTAND(false, false);
					return true;
				}
				if (num < this.GetUnitDataGame().m_fTargetNearRange)
				{
					this.StateChangeToASTAND(false, false);
					return true;
				}
				if (this.m_NKMGame.GetGameRuntimeData().m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_FINISH && this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_PLAY)
				{
					this.StateChangeToASTAND(false, false);
					return true;
				}
				if ((this.m_NKMGame.GetGameRuntimeData().m_NKMGameRuntimeTeamDataA.m_bAIDisable && this.m_NKMGame.IsATeam(this.GetUnitDataGame().m_NKM_TEAM_TYPE)) || (this.m_NKMGame.GetGameRuntimeData().m_NKMGameRuntimeTeamDataB.m_bAIDisable && this.m_NKMGame.IsBTeam(this.GetUnitDataGame().m_NKM_TEAM_TYPE)))
				{
					this.StateChangeToASTAND(false, false);
					return true;
				}
				if (!this.m_UnitTemplet.m_bNoMove && !this.m_UnitTemplet.m_bNoRun)
				{
					this.StateChangeToRUN(true, false);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x00099A69 File Offset: 0x00097C69
		public NKMUnitSkillTemplet GetStateSkill(NKMUnitState cNKMUnitState)
		{
			if (this.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_NORMAL)
			{
				return null;
			}
			if (cNKMUnitState == null)
			{
				return null;
			}
			return this.m_UnitData.GetUnitSkillTempletByType(cNKMUnitState.m_NKM_SKILL_TYPE);
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x00099A98 File Offset: 0x00097C98
		protected NKMUnit GetIdleDirRight(ref bool bRight, ref float fDist)
		{
			if (this.GetUnitTemplet().m_UnitTempletBase.m_NKM_FIND_TARGET_TYPE == NKM_FIND_TARGET_TYPE.NFTT_NO)
			{
				return null;
			}
			NKMUnit nkmunit = this.m_NKMGame.GetLiveBossUnit(!this.m_NKMGame.IsATeam(this.m_UnitDataGame.m_NKM_TEAM_TYPE));
			if (nkmunit == null)
			{
				NKM_FIND_TARGET_TYPE nkm_FIND_TARGET_TYPE = this.GetUnitTemplet().m_UnitTempletBase.m_NKM_FIND_TARGET_TYPE;
				if (nkm_FIND_TARGET_TYPE - NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_LAND <= 1)
				{
					nkmunit = this.m_NKMGame.GetLiveUnit(!this.m_NKMGame.IsATeam(this.m_UnitDataGame.m_NKM_TEAM_TYPE), this.IsAirUnit());
				}
				if (nkmunit == null)
				{
					nkmunit = this.m_NKMGame.GetLiveUnit(!this.m_NKMGame.IsATeam(this.m_UnitDataGame.m_NKM_TEAM_TYPE));
				}
			}
			if (nkmunit == null)
			{
				return null;
			}
			fDist = Math.Abs(nkmunit.GetUnitSyncData().m_PosX - this.GetUnitSyncData().m_PosX);
			if (this.GetUnitSyncData().m_PosX < nkmunit.GetUnitSyncData().m_PosX)
			{
				bRight = true;
				return nkmunit;
			}
			bRight = false;
			return nkmunit;
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x00099B90 File Offset: 0x00097D90
		protected virtual bool StateEvent_NUST_MOVE()
		{
			if (this.m_NKMGame.GetGameRuntimeData().m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_FINISH && this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_PLAY)
			{
				this.StateChangeToASTAND(false, false);
				return true;
			}
			if ((this.m_NKMGame.GetGameRuntimeData().m_NKMGameRuntimeTeamDataA.m_bAIDisable && this.m_NKMGame.IsATeam(this.GetUnitDataGame().m_NKM_TEAM_TYPE)) || (this.m_NKMGame.GetGameRuntimeData().m_NKMGameRuntimeTeamDataB.m_bAIDisable && this.m_NKMGame.IsBTeam(this.GetUnitDataGame().m_NKM_TEAM_TYPE)))
			{
				this.StateChangeToASTAND(false, false);
				return true;
			}
			if (this.m_UnitTemplet.m_UnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL && this.GetUnitTemplet().m_fPatrolRange <= 0f)
			{
				if (this.m_UnitSyncData.m_TargetUID <= 0)
				{
					float num = 99999f;
					bool bRight = this.m_UnitSyncData.m_bRight;
					bool idleDirRight = this.GetIdleDirRight(ref bRight, ref num) != null;
					if (this.m_UnitStateNow != null && !this.m_UnitStateNow.m_bNoChangeRight)
					{
						this.m_UnitSyncData.m_bRight = bRight;
					}
					if (!idleDirRight && this.GetUnitTemplet().m_UnitTempletBase.m_NKM_FIND_TARGET_TYPE != NKM_FIND_TARGET_TYPE.NFTT_NO)
					{
						this.StateChangeToASTAND(false, false);
						return true;
					}
					if (num < this.GetUnitDataGame().m_fTargetNearRange)
					{
						this.StateChangeToASTAND(false, false);
						return true;
					}
				}
				else
				{
					this.SeeTarget();
				}
			}
			return false;
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x00099CE4 File Offset: 0x00097EE4
		protected virtual bool StateEvent_NUSE_FEAR()
		{
			if (this.m_UnitTemplet.m_UnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL)
			{
				if (this.m_UnitTemplet.m_bNoMove || this.m_UnitTemplet.m_bNoRun || !this.m_UnitStateNow.m_bRun)
				{
					return true;
				}
				bool bRight = this.m_UnitSyncData.m_bRight;
				NKMUnit liveBossUnit = this.m_NKMGame.GetLiveBossUnit(this.m_UnitDataGame.m_NKM_TEAM_TYPE);
				if (liveBossUnit != null)
				{
					bRight = (this.GetUnitSyncData().m_PosX < liveBossUnit.GetUnitSyncData().m_PosX);
				}
				else
				{
					NKMUnit liveBossUnit2 = this.m_NKMGame.GetLiveBossUnit(!this.m_NKMGame.IsATeam(this.m_UnitDataGame.m_NKM_TEAM_TYPE));
					if (liveBossUnit2 != null)
					{
						bRight = (this.GetUnitSyncData().m_PosX > liveBossUnit2.GetUnitSyncData().m_PosX);
					}
				}
				if (this.m_UnitStateNow != null && !this.m_UnitStateNow.m_bNoChangeRight)
				{
					this.m_UnitSyncData.m_bRight = bRight;
				}
				if (this.GetUnitTemplet().m_fPatrolRange > 0f && (Math.Abs(this.m_UnitDataGame.m_RespawnPosX - this.GetUnitSyncData().m_PosX) > this.GetUnitTemplet().m_fPatrolRange || this.GetUnitSyncData().m_PosX <= this.m_NKMGame.GetMapTemplet().GetUnitMinX() || this.GetUnitSyncData().m_PosX >= this.m_NKMGame.GetMapTemplet().GetUnitMaxX()))
				{
					this.StateChange("USN_DAMAGE_FEAR_NOMOVE", true, false);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x00099E58 File Offset: 0x00098058
		protected virtual bool StateEvent_NUST_ATTACK()
		{
			if ((this.m_NKMGame.GetGameRuntimeData().m_NKMGameRuntimeTeamDataA.m_bAIDisable && this.m_NKMGame.IsATeam(this.GetUnitDataGame().m_NKM_TEAM_TYPE)) || (this.m_NKMGame.GetGameRuntimeData().m_NKMGameRuntimeTeamDataB.m_bAIDisable && this.m_NKMGame.IsBTeam(this.GetUnitDataGame().m_NKM_TEAM_TYPE)))
			{
				this.StateChangeToASTAND(false, false);
				return true;
			}
			if (this.m_UnitFrameData.m_fAnimTime >= this.m_UnitFrameData.m_fAnimTimeMax && !this.m_UnitStateNow.m_bAnimLoop && !this.AIAttack(false))
			{
				if (this.IsArriveTarget())
				{
					this.StateChangeToASTAND(false, false);
				}
				else
				{
					this.AIMove();
				}
			}
			return false;
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x00099F18 File Offset: 0x00098118
		public void StateChange(string stateName, bool bForceChange = true, bool bImmediate = false)
		{
			if (!bForceChange && this.m_UnitStateNow != null && stateName.CompareTo(this.m_UnitStateNow.m_StateName) == 0)
			{
				return;
			}
			if (this.GetUnitState(stateName, true) == null)
			{
				return;
			}
			if (bImmediate)
			{
				this.m_StateNameNext = "";
				this.m_StateNameNextChange = stateName;
				return;
			}
			this.m_StateNameNext = stateName;
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x00099F6C File Offset: 0x0009816C
		public void StateChange(short stateID, bool bForceChange = true, bool bImmediate = false)
		{
			NKMUnitState unitState = this.GetUnitState(stateID);
			if (unitState == null)
			{
				Log.Error(string.Format("StateChange GetUnitState NULL, unitID: {0}, stateID: {1}", this.GetUnitTemplet().m_UnitTempletBase.m_UnitStrID, stateID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnit.cs", 3249);
				return;
			}
			if (!bForceChange && stateID == (short)this.m_UnitStateNow.m_StateID)
			{
				return;
			}
			if (bImmediate)
			{
				this.m_StateNameNext = "";
				this.m_StateNameNextChange = unitState.m_StateName;
				return;
			}
			this.m_StateNameNext = unitState.m_StateName;
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x00099FF0 File Offset: 0x000981F0
		public bool UseShipSkill(int shipSkillID, float fPosX)
		{
			NKMShipSkillTemplet shipSkillTempletByID = NKMShipSkillManager.GetShipSkillTempletByID(shipSkillID);
			if (shipSkillTempletByID == null)
			{
				return false;
			}
			this.m_UnitFrameData.m_ShipSkillTemplet = shipSkillTempletByID;
			this.m_UnitFrameData.m_fShipSkillPosX = fPosX;
			this.StateChange(shipSkillTempletByID.m_UnitStateName, true, false);
			this.m_fCheckUseShipSkillAuto = 1f;
			return true;
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x0009A03B File Offset: 0x0009823B
		public virtual bool ProcessCamera()
		{
			return false;
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x0009A040 File Offset: 0x00098240
		protected void ProcessAI()
		{
			if ((this.m_NKMGame.GetGameRuntimeData().m_NKMGameRuntimeTeamDataA.m_bAIDisable && this.m_NKMGame.IsATeam(this.GetUnitDataGame().m_NKM_TEAM_TYPE)) || (this.m_NKMGame.GetGameRuntimeData().m_NKMGameRuntimeTeamDataB.m_bAIDisable && this.m_NKMGame.IsBTeam(this.GetUnitDataGame().m_NKM_TEAM_TYPE)))
			{
				return;
			}
			if (this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_PLAY)
			{
				return;
			}
			if (this.m_NKMGame.GetGameRuntimeData().m_NKM_GAME_STATE < NKM_GAME_STATE.NGS_PLAY)
			{
				return;
			}
			if (this.m_NKMGame.GetGameRuntimeData().m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_FINISH)
			{
				return;
			}
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			if (this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE != NKM_UNIT_STATE_TYPE.NUST_ATTACK && this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE != NKM_UNIT_STATE_TYPE.NUST_SKILL)
			{
				if (!this.AIAttack(false))
				{
					if (this.IsArriveTarget() && this.GetUnitTemplet().m_fPatrolRange <= 0f)
					{
						this.StateChangeToASTAND(false, false);
					}
					else if (this.m_UnitSyncData.m_TargetUID <= 0)
					{
						if (this.GetUnitTemplet().m_fPatrolRange <= 0f)
						{
							float num = 99999f;
							bool bRight = this.m_UnitSyncData.m_bRight;
							bool idleDirRight = this.GetIdleDirRight(ref bRight, ref num) != null;
							if (!this.m_UnitStateNow.m_bNoChangeRight)
							{
								this.m_UnitSyncData.m_bRight = bRight;
							}
							if (idleDirRight && num > this.GetUnitDataGame().m_fTargetNearRange)
							{
								this.AIMove();
							}
						}
						else
						{
							this.AIMove();
						}
					}
					else
					{
						this.AIMove();
					}
				}
			}
			else if (this.m_UnitStateNow.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_ATTACK && this.m_UnitStateNow.m_bAnimLoop)
			{
				this.AIAttack(true);
			}
			if (this.GetUnitTemplet().m_fPatrolRange > 0f && !this.m_UnitStateNow.m_bNoChangeRight && (Math.Abs(this.m_UnitDataGame.m_RespawnPosX - this.GetUnitSyncData().m_PosX) > this.GetUnitTemplet().m_fPatrolRange || this.GetUnitSyncData().m_PosX <= this.m_NKMGame.GetMapTemplet().GetUnitMinX() || this.GetUnitSyncData().m_PosX >= this.m_NKMGame.GetMapTemplet().GetUnitMaxX()))
			{
				bool bRight2 = this.GetUnitSyncData().m_bRight;
				if (this.m_UnitDataGame.m_RespawnPosX < this.GetUnitSyncData().m_PosX)
				{
					this.GetUnitSyncData().m_bRight = false;
				}
				else
				{
					this.GetUnitSyncData().m_bRight = true;
				}
				if (this.GetUnitSyncData().m_PosX <= this.m_NKMGame.GetMapTemplet().GetUnitMinX())
				{
					this.GetUnitSyncData().m_bRight = true;
				}
				if (this.GetUnitSyncData().m_PosX >= this.m_NKMGame.GetMapTemplet().GetUnitMaxX())
				{
					this.GetUnitSyncData().m_bRight = false;
				}
				if (bRight2 != this.m_UnitSyncData.m_bRight)
				{
					this.m_UnitFrameData.m_fSpeedX = -this.m_UnitFrameData.m_fSpeedX;
				}
			}
			this.m_UnitFrameData.m_fFindTargetTime -= this.m_DeltaTime;
			if (this.m_UnitFrameData.m_fFindTargetTime < 0f)
			{
				this.m_UnitFrameData.m_fFindTargetTime = 0f;
			}
			if (this.m_UnitSyncData.m_TargetUID <= 0 && this.m_UnitFrameData.m_fFindTargetTime > this.GetUnitTemplet().m_FindTargetTime)
			{
				this.m_UnitFrameData.m_fFindTargetTime = this.GetUnitTemplet().m_FindTargetTime;
			}
			if ((this.m_UnitSyncData.m_TargetUID <= 0 || !this.m_UnitTemplet.m_bTargetNoChange) && this.m_UnitFrameData.m_fFindTargetTime <= 0f)
			{
				this.m_UnitFrameData.m_bFindTargetThisFrame = true;
			}
			if (this.GetUnitTemplet().m_SubTargetFindData != null)
			{
				this.m_UnitFrameData.m_fFindSubTargetTime -= this.m_DeltaTime;
				if (this.m_UnitFrameData.m_fFindSubTargetTime < 0f)
				{
					this.m_UnitFrameData.m_fFindSubTargetTime = 0f;
				}
				if ((this.m_UnitSyncData.m_SubTargetUID <= 0 || !this.m_UnitTemplet.m_SubTargetFindData.m_bTargetNoChange) && this.m_UnitFrameData.m_fFindSubTargetTime <= 0f)
				{
					this.m_UnitFrameData.m_bFindSubTargetThisFrame = true;
				}
			}
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x0009A458 File Offset: 0x00098658
		protected virtual void AITarget()
		{
			NKMUnit nkmunit = this.m_NKMGame.FindTarget(this, this.GetSortUnitListByNearDist(), this.m_UnitTemplet.m_UnitTempletBase.m_NKM_FIND_TARGET_TYPE, this.GetUnitDataGame().m_NKM_TEAM_TYPE, this.GetUnitSyncData().m_PosX, this.m_UnitTemplet.m_SeeRange, this.GetUnitTemplet().m_bNoBackTarget, false, this.GetUnitSyncData().m_bRight, true, this.m_UnitTemplet.m_UnitTempletBase.m_hsFindTargetRolePriority, false);
			if (nkmunit != null)
			{
				this.m_UnitSyncData.m_TargetUID = nkmunit.GetUnitDataGame().m_GameUnitUID;
				this.m_TargetUnit = nkmunit;
				if (this.m_TargetUIDOrg != (int)this.m_UnitSyncData.m_TargetUID)
				{
					this.GetUnitFrameData().m_bTargetChangeThisFrame = true;
				}
			}
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x0009A514 File Offset: 0x00098714
		protected virtual void AISubTarget()
		{
			NKMUnit nkmunit = this.m_NKMGame.FindTarget(this, this.GetSortUnitListByNearDist(), this.m_UnitTemplet.m_SubTargetFindData, this.GetUnitDataGame().m_NKM_TEAM_TYPE, this.GetUnitSyncData().m_PosX, this.GetUnitSyncData().m_bRight);
			if (nkmunit != null)
			{
				this.m_UnitSyncData.m_SubTargetUID = nkmunit.GetUnitDataGame().m_GameUnitUID;
				this.m_SubTargetUnit = nkmunit;
				if (this.m_SubTargetUIDOrg != (int)this.m_UnitSyncData.m_SubTargetUID)
				{
					this.GetUnitFrameData().m_bTargetChangeThisFrame = true;
				}
			}
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x0009A59F File Offset: 0x0009879F
		protected bool AIMove()
		{
			if (!this.m_UnitTemplet.m_bNoMove && !this.m_UnitTemplet.m_bNoRun)
			{
				this.StateChangeToRUN(false, false);
				return true;
			}
			return false;
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x0009A5C8 File Offset: 0x000987C8
		protected bool AIAttack(bool bSkillCheckOnly = false)
		{
			if (this.m_NKMGame.GetGameRuntimeData().m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_FINISH && this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_PLAY)
			{
				return false;
			}
			if (this.IsATeam() && this.m_NKMGame.GetGameData().m_TeamASupply <= 0)
			{
				return false;
			}
			int index = 0;
			NKM_GAME_AUTO_SKILL_TYPE nkm_GAME_AUTO_SKILL_TYPE;
			if (this.IsATeam())
			{
				nkm_GAME_AUTO_SKILL_TYPE = this.m_NKMGame.GetGameRuntimeData().GetMyRuntimeTeamData(NKM_TEAM_TYPE.NTT_A1).m_NKM_GAME_AUTO_SKILL_TYPE;
			}
			else
			{
				nkm_GAME_AUTO_SKILL_TYPE = this.m_NKMGame.GetGameRuntimeData().GetMyRuntimeTeamData(NKM_TEAM_TYPE.NTT_B1).m_NKM_GAME_AUTO_SKILL_TYPE;
			}
			if (nkm_GAME_AUTO_SKILL_TYPE == NKM_GAME_AUTO_SKILL_TYPE.NGST_AUTO && this.CanUseAttack(this.m_UnitTemplet.m_listHyperSkillStateData, NKM_ATTACK_STATE_DATA_TYPE.NASDT_NORMAL, ref index) && !this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_SILENCE))
			{
				this.StateChange(this.m_UnitTemplet.m_listHyperSkillStateData[index].m_StateName, true, false);
				return true;
			}
			if ((nkm_GAME_AUTO_SKILL_TYPE == NKM_GAME_AUTO_SKILL_TYPE.NGST_AUTO || nkm_GAME_AUTO_SKILL_TYPE == NKM_GAME_AUTO_SKILL_TYPE.NGST_OFF_HYPER) && this.CanUseAttack(this.m_UnitTemplet.m_listSkillStateData, NKM_ATTACK_STATE_DATA_TYPE.NASDT_NORMAL, ref index) && !this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_SILENCE))
			{
				this.StateChange(this.m_UnitTemplet.m_listSkillStateData[index].m_StateName, true, false);
				return true;
			}
			if (!bSkillCheckOnly && this.CanUseAttack(this.m_UnitTemplet.m_listAttackStateData, NKM_ATTACK_STATE_DATA_TYPE.NASDT_NORMAL, ref index))
			{
				this.StateChange(this.m_UnitTemplet.m_listAttackStateData[index].m_StateName, true, false);
				return true;
			}
			if (this.m_TargetUnit == null)
			{
				return false;
			}
			if (this.m_TargetUnit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_PLAY)
			{
				this.m_UnitSyncData.m_TargetUID = 0;
				return false;
			}
			float num = Math.Abs(this.m_TargetUnit.GetUnitSyncData().m_PosX - this.m_UnitSyncData.m_PosX) - (this.m_TargetUnit.GetUnitTemplet().m_UnitSizeX * 0.5f + this.m_UnitTemplet.m_UnitSizeX * 0.5f);
			if (num < 0f)
			{
				num = 0f;
			}
			if (nkm_GAME_AUTO_SKILL_TYPE == NKM_GAME_AUTO_SKILL_TYPE.NGST_AUTO)
			{
				if (this.CanUseAttack(this.m_UnitTemplet.m_listAirHyperSkillStateData, NKM_ATTACK_STATE_DATA_TYPE.NASDT_NORMAL, this.m_TargetUnit.IsAirUnit(), num, ref index) && !this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_SILENCE) && this.m_TargetUnit.IsAirUnit())
				{
					this.StateChange(this.m_UnitTemplet.m_listAirHyperSkillStateData[index].m_StateName, true, false);
					return true;
				}
				if (this.CanUseAttack(this.m_UnitTemplet.m_listHyperSkillStateData, NKM_ATTACK_STATE_DATA_TYPE.NASDT_NORMAL, this.m_TargetUnit.IsAirUnit(), num, ref index) && !this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_SILENCE))
				{
					this.StateChange(this.m_UnitTemplet.m_listHyperSkillStateData[index].m_StateName, true, false);
					return true;
				}
			}
			if (nkm_GAME_AUTO_SKILL_TYPE == NKM_GAME_AUTO_SKILL_TYPE.NGST_AUTO || nkm_GAME_AUTO_SKILL_TYPE == NKM_GAME_AUTO_SKILL_TYPE.NGST_OFF_HYPER)
			{
				if (this.CanUseAttack(this.m_UnitTemplet.m_listAirSkillStateData, NKM_ATTACK_STATE_DATA_TYPE.NASDT_NORMAL, this.m_TargetUnit.IsAirUnit(), num, ref index) && !this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_SILENCE) && this.m_TargetUnit.IsAirUnit())
				{
					this.StateChange(this.m_UnitTemplet.m_listAirSkillStateData[index].m_StateName, true, false);
					return true;
				}
				if (this.CanUseAttack(this.m_UnitTemplet.m_listSkillStateData, NKM_ATTACK_STATE_DATA_TYPE.NASDT_NORMAL, this.m_TargetUnit.IsAirUnit(), num, ref index) && !this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_SILENCE))
				{
					this.StateChange(this.m_UnitTemplet.m_listSkillStateData[index].m_StateName, true, false);
					return true;
				}
			}
			if (!bSkillCheckOnly && this.CanUseAttack(this.m_UnitTemplet.m_listAirAttackStateData, NKM_ATTACK_STATE_DATA_TYPE.NASDT_NORMAL, this.m_TargetUnit.IsAirUnit(), num, ref index) && this.m_TargetUnit.IsAirUnit())
			{
				this.StateChange(this.m_UnitTemplet.m_listAirAttackStateData[index].m_StateName, true, false);
				return true;
			}
			if (!bSkillCheckOnly && this.CanUseAttack(this.m_UnitTemplet.m_listAttackStateData, NKM_ATTACK_STATE_DATA_TYPE.NASDT_NORMAL, this.m_TargetUnit.IsAirUnit(), num, ref index))
			{
				this.StateChange(this.m_UnitTemplet.m_listAttackStateData[index].m_StateName, true, false);
				return true;
			}
			if (this.m_TargetUnit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_PLAY)
			{
				this.m_UnitSyncData.m_TargetUID = 0;
				return false;
			}
			return false;
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x0009A9A4 File Offset: 0x00098BA4
		public bool CanUseManualSkill(bool bUse, out bool bHyper, out byte skillStateID)
		{
			bHyper = false;
			skillStateID = 0;
			if (this.m_UnitStateNow == null)
			{
				return false;
			}
			if (this.m_NKMGame.GetGameRuntimeData().m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_FINISH && this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_PLAY)
			{
				return false;
			}
			if (this.IsATeam() && this.m_NKMGame.GetGameData().m_TeamASupply <= 0)
			{
				return false;
			}
			int index = 0;
			if (this.CanUseAttack(this.m_UnitTemplet.m_listHyperSkillStateData, NKM_ATTACK_STATE_DATA_TYPE.NASDT_NORMAL, ref index) && !this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_SILENCE) && this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE != NKM_UNIT_STATE_TYPE.NUST_DAMAGE && this.m_UnitStateNow.m_NKM_SKILL_TYPE < NKM_SKILL_TYPE.NST_HYPER)
			{
				if (bUse)
				{
					this.StateChange(this.m_UnitTemplet.m_listHyperSkillStateData[index].m_StateName, true, false);
				}
				bHyper = true;
				NKMUnitState unitState = this.GetUnitState(this.m_UnitTemplet.m_listHyperSkillStateData[index].m_StateName, true);
				if (unitState != null)
				{
					skillStateID = unitState.m_StateID;
				}
				return true;
			}
			bool bAirUnit = false;
			if (this.m_TargetUnit != null && this.m_TargetUnit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_PLAY)
			{
				bAirUnit = this.m_TargetUnit.IsAirUnit();
			}
			if (this.CanUseAttack(this.m_UnitTemplet.m_listHyperSkillStateData, NKM_ATTACK_STATE_DATA_TYPE.NASDT_NORMAL, bAirUnit, 0f, ref index) && !this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_SILENCE) && this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE != NKM_UNIT_STATE_TYPE.NUST_DAMAGE && this.m_UnitStateNow.m_NKM_SKILL_TYPE < NKM_SKILL_TYPE.NST_HYPER)
			{
				if (bUse)
				{
					this.StateChange(this.m_UnitTemplet.m_listHyperSkillStateData[index].m_StateName, true, false);
				}
				bHyper = true;
				NKMUnitState unitState2 = this.GetUnitState(this.m_UnitTemplet.m_listHyperSkillStateData[index].m_StateName, true);
				if (unitState2 != null)
				{
					skillStateID = unitState2.m_StateID;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x0009AB47 File Offset: 0x00098D47
		public float GetNowHP()
		{
			return this.GetUnitSyncData().GetHP();
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x0009AB54 File Offset: 0x00098D54
		public float GetHPRate()
		{
			float statFinal = this.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP);
			if (statFinal.IsNearlyZero(1E-05f))
			{
				return 0f;
			}
			return this.GetUnitSyncData().GetHP() / statFinal;
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x0009AB94 File Offset: 0x00098D94
		public float GetHPRateToValue(float fHPRate)
		{
			float statFinal = this.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP);
			if (statFinal.IsNearlyZero(1E-05f))
			{
				return 0f;
			}
			return statFinal * fHPRate;
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x0009ABCC File Offset: 0x00098DCC
		protected bool IsStateUnlocked(NKMAttackStateData attackStateData)
		{
			NKMUnitState unitState = this.m_UnitTemplet.GetUnitState(attackStateData.m_StateName, true);
			return this.IsStateUnlocked(unitState);
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x0009ABF3 File Offset: 0x00098DF3
		protected bool IsStateUnlocked(NKMUnitState unitState)
		{
			return unitState != null && this.GetUnitData().IsUnitSkillUnlockedByType(unitState.m_NKM_SKILL_TYPE);
		}

		// Token: 0x06001FE0 RID: 8160 RVA: 0x0009AC0C File Offset: 0x00098E0C
		public void StateChangeToSTART(bool bForceChange = true, bool bImmediate = false)
		{
			string text = this.GetUsableCommonState(this.m_UnitTemplet.m_listStartStateData, null);
			if (string.IsNullOrEmpty(text))
			{
				if (this.m_NKMGame.CheckBossDungeon() && this.GetUnitState("USN_START_BOSS", false) != null)
				{
					text = "USN_START_BOSS";
				}
				else
				{
					text = "USN_START";
				}
			}
			this.StateChange(text, bForceChange, bImmediate);
		}

		// Token: 0x06001FE1 RID: 8161 RVA: 0x0009AC68 File Offset: 0x00098E68
		public void StateChangeToASTAND(bool bForceChange = true, bool bImmediate = false)
		{
			string usableCommonState = this.GetUsableCommonState(this.m_UnitTemplet.m_listStandStateData, "USN_ASTAND");
			this.StateChange(usableCommonState, bForceChange, bImmediate);
		}

		// Token: 0x06001FE2 RID: 8162 RVA: 0x0009AC98 File Offset: 0x00098E98
		public void StateChangeToRUN(bool bForceChange = true, bool bImmediate = false)
		{
			string usableCommonState = this.GetUsableCommonState(this.m_UnitTemplet.m_listRunStateData, "USN_RUN");
			this.StateChange(usableCommonState, bForceChange, bImmediate);
		}

		// Token: 0x06001FE3 RID: 8163 RVA: 0x0009ACC8 File Offset: 0x00098EC8
		protected string GetUsableCommonState(List<NKMCommonStateData> lstCommonStateData, string defaultState)
		{
			if (lstCommonStateData == null || lstCommonStateData.Count == 0)
			{
				return defaultState;
			}
			this.m_listAttackSelectTemp.Clear();
			for (int i = 0; i < lstCommonStateData.Count; i++)
			{
				if (this.CanUseState(lstCommonStateData[i]))
				{
					for (int j = 0; j < lstCommonStateData[i].m_Ratio; j++)
					{
						this.m_listAttackSelectTemp.Add(i);
					}
				}
			}
			if (this.m_listAttackSelectTemp.Count > 0)
			{
				int index = NKMRandom.Range(0, this.m_listAttackSelectTemp.Count);
				int index2 = this.m_listAttackSelectTemp[index];
				return lstCommonStateData[index2].m_StateName;
			}
			return defaultState;
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x0009AD6C File Offset: 0x00098F6C
		protected bool CanUseState(NKMCommonStateData stateData)
		{
			if (!this.CheckEventCondition(stateData.m_Condition))
			{
				return false;
			}
			float num = this.GetUnitDataGame().m_fTargetNearRange / this.GetUnitTemplet().m_TargetNearRange;
			if (stateData.CanUseState(this.GetHPRate(), this.GetUnitFrameData().m_PhaseNow))
			{
				if (!this.CheckStateCoolTime(stateData.m_StateName))
				{
					return false;
				}
				NKMUnitState unitState = this.m_UnitTemplet.GetUnitState(stateData.m_StateName, true);
				if (this.IsStateUnlocked(unitState))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001FE5 RID: 8165 RVA: 0x0009ADEC File Offset: 0x00098FEC
		protected bool CanUseAttack(List<NKMAttackStateData> m_listAttackStateData, NKM_ATTACK_STATE_DATA_TYPE eNKM_ATTACK_STATE_DATA_TYPE, bool bAirUnit, float fDistToTarget, ref int attackIndex)
		{
			this.m_listAttackSelectTemp.Clear();
			for (int i = 0; i < m_listAttackStateData.Count; i++)
			{
				if (this.CanUseAttack(m_listAttackStateData[i], eNKM_ATTACK_STATE_DATA_TYPE, bAirUnit, fDistToTarget))
				{
					for (int j = 0; j < m_listAttackStateData[i].m_Ratio; j++)
					{
						this.m_listAttackSelectTemp.Add(i);
					}
				}
			}
			if (this.m_listAttackSelectTemp.Count > 0)
			{
				int index = NKMRandom.Range(0, this.m_listAttackSelectTemp.Count);
				attackIndex = this.m_listAttackSelectTemp[index];
				return true;
			}
			return false;
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x0009AE80 File Offset: 0x00099080
		protected bool CanUseAttack(NKMAttackStateData m_AttackStateData, NKM_ATTACK_STATE_DATA_TYPE eNKM_ATTACK_STATE_DATA_TYPE, bool bAirUnit, float fDistToTarget)
		{
			if (!this.CheckEventCondition(m_AttackStateData.m_Condition))
			{
				return false;
			}
			float fRangeFactor = this.GetUnitDataGame().m_fTargetNearRange / this.GetUnitTemplet().m_TargetNearRange;
			if (m_AttackStateData.CanUseAttack(eNKM_ATTACK_STATE_DATA_TYPE, this.GetHPRate(), bAirUnit, fDistToTarget, this.GetUnitFrameData().m_PhaseNow, fRangeFactor))
			{
				if (!this.CheckStateCoolTime(m_AttackStateData.m_StateName))
				{
					return false;
				}
				if (this.IsStateUnlocked(m_AttackStateData))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x0009AEF0 File Offset: 0x000990F0
		protected bool CanUseAttack(List<NKMAttackStateData> m_listAttackStateData, NKM_ATTACK_STATE_DATA_TYPE eNKM_ATTACK_STATE_DATA_TYPE, ref int attackIndex)
		{
			this.m_listAttackSelectTemp.Clear();
			for (int i = 0; i < m_listAttackStateData.Count; i++)
			{
				if (this.CanUseAttack(m_listAttackStateData[i], eNKM_ATTACK_STATE_DATA_TYPE))
				{
					for (int j = 0; j < m_listAttackStateData[i].m_Ratio; j++)
					{
						this.m_listAttackSelectTemp.Add(i);
					}
				}
			}
			if (this.m_listAttackSelectTemp.Count > 0)
			{
				int index = NKMRandom.Range(0, this.m_listAttackSelectTemp.Count);
				attackIndex = this.m_listAttackSelectTemp[index];
				return true;
			}
			return false;
		}

		// Token: 0x06001FE8 RID: 8168 RVA: 0x0009AF80 File Offset: 0x00099180
		protected bool CanUseAttack(NKMAttackStateData m_AttackStateData, NKM_ATTACK_STATE_DATA_TYPE eNKM_ATTACK_STATE_DATA_TYPE)
		{
			if (!this.CheckEventCondition(m_AttackStateData.m_Condition))
			{
				return false;
			}
			float fRangeFactor = this.GetUnitDataGame().m_fTargetNearRange / this.GetUnitTemplet().m_TargetNearRange;
			if (m_AttackStateData.CanUseAttack(eNKM_ATTACK_STATE_DATA_TYPE, this.GetHPRate(), this.GetUnitFrameData().m_PhaseNow, fRangeFactor))
			{
				if (!this.CheckStateCoolTime(m_AttackStateData.m_StateName))
				{
					return false;
				}
				if (this.IsStateUnlocked(m_AttackStateData))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x0009AFEC File Offset: 0x000991EC
		protected bool CheckStateCoolTime(string stateName)
		{
			return this.GetStateCoolTime(stateName) <= 0f;
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x0009B000 File Offset: 0x00099200
		public float GetStateCoolTime(string stateName)
		{
			NKMUnitState unitState = this.GetUnitState(stateName, true);
			return this.GetStateCoolTime(unitState);
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x0009B01D File Offset: 0x0009921D
		public float GetStateCoolTime(NKMUnitState cNKMUnitState)
		{
			if (cNKMUnitState != null && this.m_dicStateCoolTime.ContainsKey((int)cNKMUnitState.m_StateID))
			{
				return this.m_dicStateCoolTime[(int)cNKMUnitState.m_StateID].m_CoolTime;
			}
			return 0f;
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x0009B054 File Offset: 0x00099254
		public Dictionary<int, float> GetDicStateCoolTime()
		{
			Dictionary<int, float> dictionary = new Dictionary<int, float>();
			foreach (KeyValuePair<int, NKMStateCoolTime> keyValuePair in this.m_dicStateCoolTime)
			{
				dictionary.Add(keyValuePair.Key, keyValuePair.Value.m_CoolTime);
			}
			return dictionary;
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x0009B0C0 File Offset: 0x000992C0
		protected void SeeTarget()
		{
			if (this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_PLAY)
			{
				return;
			}
			if (this.m_NKM_UNIT_CLASS_TYPE != NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
			{
				return;
			}
			if (this.m_TargetUnit != null)
			{
				bool bRight = this.m_UnitSyncData.m_bRight;
				if (this.m_UnitTemplet.m_SeeTarget && this.m_UnitStateNow != null && !this.m_UnitStateNow.m_bNoChangeRight)
				{
					if (this.m_UnitSyncData.m_PosX < this.m_TargetUnit.GetUnitSyncData().m_PosX)
					{
						this.m_UnitSyncData.m_bRight = true;
					}
					else
					{
						this.m_UnitSyncData.m_bRight = false;
					}
				}
				if (bRight != this.m_UnitSyncData.m_bRight)
				{
					this.m_UnitFrameData.m_fSpeedX = -this.m_UnitFrameData.m_fSpeedX;
				}
			}
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x0009B17C File Offset: 0x0009937C
		protected void SeeMoreEnemy()
		{
			if (this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_PLAY)
			{
				return;
			}
			if (this.m_NKM_UNIT_CLASS_TYPE != NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
			{
				return;
			}
			int num = 0;
			int num2 = 0;
			List<NKMUnit> sortUnitListByNearDist = this.GetSortUnitListByNearDist();
			for (int i = 0; i < sortUnitListByNearDist.Count; i++)
			{
				NKMUnit nkmunit = sortUnitListByNearDist[i];
				if (nkmunit.GetUnitSyncData().m_GameUnitUID != this.GetUnitSyncData().m_GameUnitUID && nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_ENV && nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DYING && nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DIE && !this.IsAlly(nkmunit) && Math.Abs(nkmunit.GetUnitSyncData().m_PosX - this.GetUnitSyncData().m_PosX) <= this.GetUnitTemplet().m_SeeRange)
				{
					if (this.GetUnitSyncData().m_PosX <= nkmunit.GetUnitSyncData().m_PosX)
					{
						num++;
					}
					else
					{
						num2++;
					}
				}
			}
			bool bRight = this.m_UnitSyncData.m_bRight;
			if (num == num2)
			{
				return;
			}
			if (this.m_UnitStateNow != null && !this.m_UnitStateNow.m_bNoChangeRight)
			{
				if (num >= num2)
				{
					this.m_UnitSyncData.m_bRight = true;
				}
				else
				{
					this.m_UnitSyncData.m_bRight = false;
				}
			}
			if (bRight != this.m_UnitSyncData.m_bRight)
			{
				this.m_UnitFrameData.m_fSpeedX = -this.m_UnitFrameData.m_fSpeedX;
			}
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x0009B2E0 File Offset: 0x000994E0
		protected void HPProcess()
		{
			if (this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_PLAY)
			{
				float num = this.m_UnitFrameData.m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP) * this.m_UnitFrameData.m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP_REGEN_RATE) * this.m_DeltaTime;
				bool flag = false;
				if (this.GetUnitFrameData().m_bInvincible || this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_INVINCIBLE))
				{
					flag = true;
				}
				if (this.m_UnitStateNow != null && this.m_UnitStateNow.m_bInvincibleState)
				{
					flag = true;
				}
				if (num < 0f && flag)
				{
					num = 0f;
				}
				if (num > 0f && this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_NOHEAL))
				{
					num = 0f;
				}
				if (this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_IRONWALL))
				{
					num = 0f;
				}
				if (num > 0f && this.GetUnitFrameData().m_fHealFeedback > 0f)
				{
					NKM_TEAM_TYPE teamType = NKM_TEAM_TYPE.NTT_INVALID;
					NKMUnit unit = this.m_NKMGame.GetUnit(this.GetUnitFrameData().m_fHealFeedbackMasterGameUnitUID, true, true);
					if (unit != null)
					{
						teamType = unit.GetUnitDataGame().m_NKM_TEAM_TYPE;
					}
					if (!flag)
					{
						this.AddDamage(false, num * this.GetUnitFrameData().m_fHealFeedback, NKM_DAMAGE_RESULT_TYPE.NDRT_NO_MARK, this.GetUnitFrameData().m_fHealFeedbackMasterGameUnitUID, teamType, false, true, false);
					}
					num = 0f;
				}
				num += this.m_UnitSyncData.GetHP();
				bool flag2;
				float phaseDamageLimit = this.GetPhaseDamageLimit(out flag2);
				if (phaseDamageLimit > 0f && num < phaseDamageLimit)
				{
					if (flag2)
					{
						num = phaseDamageLimit - 1f;
					}
					if (!this.GetUnitTemplet().m_UnitTempletBase.m_bMonster && num <= 0f)
					{
						num = 1f;
					}
				}
				if (num <= 0f && this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_IMMORTAL))
				{
					this.m_UnitFrameData.m_bImmortalStart = true;
					num = 1f;
				}
				this.m_UnitSyncData.SetHP(num);
			}
			if (this.m_UnitSyncData.GetHP() > this.m_UnitFrameData.m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP))
			{
				this.m_UnitSyncData.SetHP(this.m_UnitFrameData.m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP));
			}
			if (this.m_NKMGame.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_PRACTICE && this.m_NKMGame.GetGameRuntimeData().m_bPracticeHeal)
			{
				this.m_fPracticeHPReset -= this.m_DeltaTime;
				if (this.m_fPracticeHPReset < 0f)
				{
					this.m_fPracticeHPReset = 3f;
					this.m_UnitSyncData.SetHP(this.m_UnitFrameData.m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP));
					this.m_PushSyncData = true;
				}
			}
			if (this.m_UnitSyncData.GetHP() <= 0f)
			{
				if (this.m_NKMGame.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_PRACTICE && this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_PLAY)
				{
					this.m_UnitSyncData.SetHP(1f);
					return;
				}
				this.m_UnitSyncData.SetHP(0f);
				this.SetDying(false, false);
			}
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x0009B590 File Offset: 0x00099790
		public virtual void SetDying(bool bForce = false, bool bUnitChange = false)
		{
			if (!bForce && (this.m_NKMGame.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_DEV || this.m_NKMGame.GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_PRACTICE))
			{
				bool flag = false;
				if (this.m_NKMGame.GetGameData().m_NKMGameTeamDataA.m_MainShip != null)
				{
					for (int i = 0; i < this.m_NKMGame.GetGameData().m_NKMGameTeamDataA.m_MainShip.m_listGameUnitUID.Count; i++)
					{
						if (this.m_UnitSyncData.m_GameUnitUID == this.m_NKMGame.GetGameData().m_NKMGameTeamDataA.m_MainShip.m_listGameUnitUID[i])
						{
							flag = true;
							break;
						}
					}
				}
				if (this.m_NKMGame.GetGameData().m_NKMGameTeamDataB.m_MainShip != null)
				{
					for (int j = 0; j < this.m_NKMGame.GetGameData().m_NKMGameTeamDataB.m_MainShip.m_listGameUnitUID.Count; j++)
					{
						if (this.m_UnitSyncData.m_GameUnitUID == this.m_NKMGame.GetGameData().m_NKMGameTeamDataB.m_MainShip.m_listGameUnitUID[j])
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					this.m_UnitSyncData.SetHP(1f);
					return;
				}
			}
			if (this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DIE && this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DYING)
			{
				NKMGameRuntimeTeamData nkmgameRuntimeTeamData = null;
				if (this.IsATeam())
				{
					nkmgameRuntimeTeamData = this.m_NKMGame.GetGameRuntimeData().m_NKMGameRuntimeTeamDataA;
				}
				else if (this.IsBTeam())
				{
					nkmgameRuntimeTeamData = this.m_NKMGame.GetGameRuntimeData().m_NKMGameRuntimeTeamDataB;
				}
				if (nkmgameRuntimeTeamData != null && nkmgameRuntimeTeamData.m_bGodMode)
				{
					this.m_UnitSyncData.SetHP(1f);
					return;
				}
				this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE = NKM_UNIT_PLAY_STATE.NUPS_DYING;
				this.StateChange(this.m_UnitTemplet.m_DyingState, false, false);
				this.ProcessUnitDyingBuff();
				if (this.GetUnitData().m_DungeonRespawnUnitTemplet != null && !bUnitChange && this.GetUnitData().m_DungeonRespawnUnitTemplet.m_EventRespawnUnitTag.Length > 0)
				{
					this.m_NKMGame.AddDieUnitRespawnTag(this.GetUnitData().m_DungeonRespawnUnitTemplet.m_EventRespawnUnitTag);
				}
				this.m_EventMovePosX.StopTracking();
				this.m_EventMovePosZ.StopTracking();
				this.m_EventMovePosJumpY.StopTracking();
			}
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x0009B7C4 File Offset: 0x000999C4
		public void ProcessUnitDyingBuff()
		{
			this.m_listBuffDieEvent.Clear();
			this.m_listBuffDelete.Clear();
			foreach (KeyValuePair<short, NKMBuffData> keyValuePair in this.m_UnitFrameData.m_dicBuffData)
			{
				NKMBuffData value = keyValuePair.Value;
				if (!value.m_NKMBuffTemplet.m_bInfinity)
				{
					this.m_listBuffDelete.Add(value.m_BuffSyncData.m_BuffID);
				}
				if (value.m_NKMBuffTemplet.m_bUnitDieEvent && (value.m_NKMBuffTemplet.m_AffectMe || value.m_BuffSyncData.m_MasterGameUnitUID != this.GetUnitDataGame().m_GameUnitUID))
				{
					this.m_listBuffDieEvent.Add(value);
				}
			}
			foreach (NKMBuffData nkmbuffData in this.m_listBuffDieEvent)
			{
				NKMUnit unit = this.m_NKMGame.GetUnit(nkmbuffData.m_BuffSyncData.m_MasterGameUnitUID, true, true);
				if (unit != null)
				{
					unit.ProcessEventBuffUnitDie(nkmbuffData, this.GetUnitSyncData().m_PosX);
				}
			}
			this.m_listBuffDieEvent.Clear();
			foreach (short buffID in this.m_listBuffDelete)
			{
				this.DeleteBuff(buffID, NKMBuffTemplet.BuffEndDTType.NoUse);
			}
			this.m_listBuffDelete.Clear();
			this.m_bPushSimpleSyncData = true;
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x0009B968 File Offset: 0x00099B68
		public virtual bool SetDie(bool bCheckAllDie = true)
		{
			if (this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DIE)
			{
				this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE = NKM_UNIT_PLAY_STATE.NUPS_DIE;
				this.m_UnitDataGame.m_NKM_TEAM_TYPE = this.m_UnitDataGame.m_NKM_TEAM_TYPE_ORG;
				this.InitLInkedDamageEffect();
				if (this.m_NKMGame.IsGameUnitAllDie(this.GetUnitData(), this.m_UnitSyncData.m_GameUnitUID))
				{
					if (this.GetUnitData().m_DungeonRespawnUnitTemplet != null && this.GetUnitData().m_DungeonRespawnUnitTemplet.m_EventRespawnUnitTag.Length > 0)
					{
						this.m_NKMGame.AddDieDeckRespawnTag(this.GetUnitData().m_DungeonRespawnUnitTemplet.m_EventRespawnUnitTag);
					}
					this.GetUnitData().m_fLastDieTime = this.m_NKMGame.GetGameRuntimeData().m_GameTime;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x0009BA2C File Offset: 0x00099C2C
		protected virtual void PhysicProcess()
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			NKMUnit unit = this.m_NKMGame.GetUnit(this.GetUnitSyncData().m_CatcherGameUnitUID, true, false);
			if (this.m_UnitStateNow.m_bNoMove || this.m_UnitTemplet.m_bNoMove || unit != null)
			{
				this.m_UnitFrameData.m_fSpeedX = 0f;
				this.m_UnitFrameData.m_fSpeedY = 0f;
				this.m_UnitFrameData.m_fSpeedZ = 0f;
				this.m_UnitFrameData.m_fDamageSpeedX = 0f;
				this.m_UnitFrameData.m_fDamageSpeedZ = 0f;
				this.m_UnitFrameData.m_fDamageSpeedJumpY = 0f;
			}
			if (this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_FREEZE) || this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_HOLD))
			{
				this.m_UnitFrameData.m_fSpeedX = 0f;
				this.m_UnitFrameData.m_fSpeedZ = 0f;
				this.m_UnitFrameData.m_fDamageSpeedX = 0f;
				this.m_UnitFrameData.m_fDamageSpeedZ = 0f;
				this.m_UnitFrameData.m_fDamageSpeedJumpY = 0f;
			}
			float fGAccel = this.m_UnitTemplet.m_fGAccel;
			if (!this.m_UnitStateNow.m_fGAccel.IsNearlyEqual(-1f, 1E-05f))
			{
				fGAccel = this.m_UnitStateNow.m_fGAccel;
			}
			if (this.m_UnitSyncData.m_bRight)
			{
				this.m_UnitFrameData.m_PosXCalc += this.m_UnitFrameData.m_fSpeedX * this.m_DeltaTime;
			}
			else
			{
				this.m_UnitFrameData.m_PosXCalc -= this.m_UnitFrameData.m_fSpeedX * this.m_DeltaTime;
			}
			this.m_UnitFrameData.m_PosZCalc += this.m_UnitFrameData.m_fSpeedZ * this.m_DeltaTime;
			this.m_UnitFrameData.m_JumpYPosCalc += this.m_UnitFrameData.m_fSpeedY * this.m_DeltaTime;
			this.m_UnitFrameData.m_PosXCalc += this.m_UnitFrameData.m_fDamageSpeedX * this.m_DeltaTime;
			if (this.m_UnitSyncData.m_bAttackerZUp)
			{
				this.m_UnitFrameData.m_PosZCalc -= this.m_UnitFrameData.m_fDamageSpeedZ * this.m_DeltaTime;
			}
			else
			{
				this.m_UnitFrameData.m_PosZCalc += this.m_UnitFrameData.m_fDamageSpeedZ * this.m_DeltaTime;
			}
			this.m_UnitFrameData.m_JumpYPosCalc += this.m_UnitFrameData.m_fDamageSpeedJumpY * this.m_DeltaTime;
			bool flag = this.m_UnitFrameData.m_fSpeedX >= 0f;
			if (flag)
			{
				this.m_UnitFrameData.m_fSpeedX -= this.m_UnitTemplet.m_fReloadAccel * this.m_DeltaTime;
				if (this.m_UnitFrameData.m_fSpeedX <= 0f)
				{
					this.m_UnitFrameData.m_fSpeedX = 0f;
				}
			}
			else
			{
				this.m_UnitFrameData.m_fSpeedX += this.m_UnitTemplet.m_fReloadAccel * this.m_DeltaTime;
				if (this.m_UnitFrameData.m_fSpeedX > 0f)
				{
					this.m_UnitFrameData.m_fSpeedX = 0f;
				}
			}
			bool flag2 = this.m_UnitFrameData.m_fSpeedZ >= 0f;
			if (flag2)
			{
				this.m_UnitFrameData.m_fSpeedZ -= this.m_UnitTemplet.m_fReloadAccel * this.m_DeltaTime;
				if (this.m_UnitFrameData.m_fSpeedZ <= 0f)
				{
					this.m_UnitFrameData.m_fSpeedZ = 0f;
				}
			}
			else
			{
				this.m_UnitFrameData.m_fSpeedZ += this.m_UnitTemplet.m_fReloadAccel * this.m_DeltaTime;
				if (this.m_UnitFrameData.m_fSpeedZ > 0f)
				{
					this.m_UnitFrameData.m_fSpeedZ = 0f;
				}
			}
			this.m_UnitFrameData.m_fSpeedY -= fGAccel * this.m_DeltaTime;
			if (this.m_UnitFrameData.m_fSpeedY <= this.m_UnitTemplet.m_fMaxGSpeed)
			{
				this.m_UnitFrameData.m_fSpeedY = this.m_UnitTemplet.m_fMaxGSpeed;
			}
			this.m_UnitFrameData.m_fDamageSpeedKeepTimeX -= this.m_DeltaTime;
			this.m_UnitFrameData.m_fDamageSpeedKeepTimeZ -= this.m_DeltaTime;
			this.m_UnitFrameData.m_fDamageSpeedKeepTimeJumpY -= this.m_DeltaTime;
			if (this.m_UnitFrameData.m_fDamageSpeedKeepTimeX <= 0f)
			{
				this.m_UnitFrameData.m_fDamageSpeedKeepTimeX = 0f;
				flag = (this.m_UnitFrameData.m_fDamageSpeedX >= 0f);
				if (flag)
				{
					this.m_UnitFrameData.m_fDamageSpeedX -= this.m_UnitTemplet.m_fReloadAccel * this.m_DeltaTime;
					if (this.m_UnitFrameData.m_fDamageSpeedX <= 0f)
					{
						this.m_UnitFrameData.m_fDamageSpeedX = 0f;
					}
				}
				else
				{
					this.m_UnitFrameData.m_fDamageSpeedX += this.m_UnitTemplet.m_fReloadAccel * this.m_DeltaTime;
					if (this.m_UnitFrameData.m_fDamageSpeedX > 0f)
					{
						this.m_UnitFrameData.m_fDamageSpeedX = 0f;
					}
				}
			}
			if (this.m_UnitFrameData.m_fDamageSpeedKeepTimeZ <= 0f)
			{
				this.m_UnitFrameData.m_fDamageSpeedKeepTimeZ = 0f;
				flag2 = (this.m_UnitFrameData.m_fDamageSpeedZ >= 0f);
				if (flag2)
				{
					this.m_UnitFrameData.m_fDamageSpeedZ -= this.m_UnitTemplet.m_fReloadAccel * this.m_DeltaTime;
					if (this.m_UnitFrameData.m_fDamageSpeedZ <= 0f)
					{
						this.m_UnitFrameData.m_fDamageSpeedZ = 0f;
					}
				}
				else
				{
					this.m_UnitFrameData.m_fDamageSpeedZ += this.m_UnitTemplet.m_fReloadAccel * this.m_DeltaTime;
					if (this.m_UnitFrameData.m_fDamageSpeedZ > 0f)
					{
						this.m_UnitFrameData.m_fDamageSpeedZ = 0f;
					}
				}
			}
			if (this.m_UnitFrameData.m_fDamageSpeedKeepTimeJumpY <= 0f)
			{
				this.m_UnitFrameData.m_fDamageSpeedKeepTimeJumpY = 0f;
				bool flag3 = this.m_UnitFrameData.m_fDamageSpeedJumpY >= 0f;
				if (flag3)
				{
					this.m_UnitFrameData.m_fDamageSpeedJumpY -= fGAccel * this.m_DeltaTime;
					if (this.m_UnitFrameData.m_fDamageSpeedJumpY <= 0f)
					{
						this.m_UnitFrameData.m_fDamageSpeedJumpY = 0f;
					}
				}
				else
				{
					this.m_UnitFrameData.m_fDamageSpeedJumpY += fGAccel * this.m_DeltaTime;
					if (this.m_UnitFrameData.m_fDamageSpeedJumpY > 0f)
					{
						this.m_UnitFrameData.m_fDamageSpeedJumpY = 0f;
					}
				}
			}
			else
			{
				this.m_UnitFrameData.m_fSpeedY = 0f;
			}
			if (this.m_UnitStateNow.m_bNoMove || this.m_UnitTemplet.m_bNoMove)
			{
				this.m_EventMovePosX.StopTracking();
				this.m_EventMovePosZ.StopTracking();
				this.m_EventMovePosJumpY.StopTracking();
			}
			this.m_EventMovePosX.Update(this.m_DeltaTime);
			this.m_EventMovePosZ.Update(this.m_DeltaTime);
			this.m_EventMovePosJumpY.Update(this.m_DeltaTime);
			if (this.m_EventMovePosX.IsTracking())
			{
				this.m_UnitFrameData.m_fSpeedX = 0f;
				this.m_UnitFrameData.m_PosXCalc = this.m_EventMovePosX.GetNowValue();
			}
			if (this.m_EventMovePosZ.IsTracking())
			{
				this.m_UnitFrameData.m_fSpeedZ = 0f;
				this.m_UnitFrameData.m_PosZCalc = this.m_EventMovePosZ.GetNowValue();
			}
			if (this.m_EventMovePosJumpY.IsTracking())
			{
				this.m_UnitFrameData.m_fSpeedY = 0f;
				this.m_UnitFrameData.m_JumpYPosCalc = this.m_EventMovePosJumpY.GetNowValue();
			}
			if (unit != null)
			{
				if (unit.GetUnitSyncData().m_bRight)
				{
					this.m_UnitFrameData.m_PosXCalc = unit.GetUnitSyncData().m_PosX + unit.GetUnitTemplet().m_UnitSizeX;
				}
				else
				{
					this.m_UnitFrameData.m_PosXCalc = unit.GetUnitSyncData().m_PosX - unit.GetUnitTemplet().m_UnitSizeX;
				}
				this.m_UnitFrameData.m_JumpYPosCalc = unit.GetUnitSyncData().m_JumpYPos;
			}
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x0009C270 File Offset: 0x0009A470
		private float IsCollisionUnit(NKMUnit cNKMUnit)
		{
			float num = Math.Abs(cNKMUnit.GetUnitSyncData().m_PosX - this.m_UnitFrameData.m_PosXCalc);
			float num2 = cNKMUnit.GetUnitTemplet().m_UnitSizeX * 0.5f + this.m_UnitTemplet.m_UnitSizeX * 0.5f;
			return num - num2;
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x0009C2C0 File Offset: 0x0009A4C0
		protected void MapEdgeProcess()
		{
			NKMMapTemplet mapTemplet = this.m_NKMGame.GetMapTemplet();
			if (!this.GetUnitTemplet().m_bNoMapLimit)
			{
				if (this.m_UnitFrameData.m_PosXCalc < mapTemplet.m_fMinX)
				{
					if (this.IsBoss())
					{
						this.m_UnitFrameData.m_PosXCalc = mapTemplet.m_fMinX;
					}
					else
					{
						this.m_UnitFrameData.m_PosXCalc = mapTemplet.GetUnitMinX();
					}
				}
				if (this.m_UnitFrameData.m_PosXCalc > mapTemplet.m_fMaxX)
				{
					if (this.IsBoss())
					{
						this.m_UnitFrameData.m_PosXCalc = mapTemplet.m_fMaxX;
					}
					else
					{
						this.m_UnitFrameData.m_PosXCalc = mapTemplet.GetUnitMaxX();
					}
				}
			}
			if (this.m_UnitFrameData.m_PosZCalc < mapTemplet.m_fMinZ)
			{
				this.m_UnitFrameData.m_PosZCalc = mapTemplet.m_fMinZ;
			}
			if (this.m_UnitFrameData.m_PosZCalc > mapTemplet.m_fMaxZ)
			{
				this.m_UnitFrameData.m_PosZCalc = mapTemplet.m_fMaxZ;
			}
			float num = 0f;
			if (this.IsAirUnit())
			{
				num = this.m_UnitFrameData.m_fAirHigh;
			}
			if (this.m_UnitFrameData.m_JumpYPosCalc <= num)
			{
				this.m_UnitFrameData.m_JumpYPosCalc = num;
				this.m_UnitFrameData.m_fSpeedY = 0f;
				this.m_UnitFrameData.m_bFootOnLand = true;
				return;
			}
			this.m_UnitFrameData.m_bFootOnLand = false;
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x0009C40C File Offset: 0x0009A60C
		public bool CheckEventCondition(NKMEventCondition cCondition)
		{
			cCondition.CheckSkillID();
			if (this.m_NKMGame.GetGameData().IsPVP() && !cCondition.m_bUsePVP)
			{
				return false;
			}
			if (this.m_NKMGame.GetGameData().IsPVE() && !cCondition.m_bUsePVE)
			{
				return false;
			}
			if (!cCondition.CanUsePhase(this.m_UnitFrameData.m_PhaseNow))
			{
				return false;
			}
			if (!cCondition.CanUseBuff(this.GetUnitSyncData().m_dicBuffData))
			{
				return false;
			}
			if (!cCondition.CanUseStatus(this))
			{
				return false;
			}
			if (cCondition.m_bLeaderUnit)
			{
				NKMGameTeamData teamData = this.GetTeamData();
				if (teamData == null)
				{
					return false;
				}
				if (teamData.GetLeaderUnitData() == null)
				{
					return false;
				}
				if (this.IsSummonUnit())
				{
					NKMUnit masterUnit = this.GetMasterUnit();
					if (masterUnit == null)
					{
						return false;
					}
					if (teamData.GetLeaderUnitData().m_UnitUID != masterUnit.GetUnitData().m_UnitUID)
					{
						return false;
					}
				}
				else if (teamData.GetLeaderUnitData().m_UnitUID != this.GetUnitData().m_UnitUID)
				{
					return false;
				}
			}
			if (cCondition.m_SkillID != -1)
			{
				int skillLevel = this.GetUnitData().GetSkillLevel(cCondition.m_SkillStrID);
				if (!cCondition.CanUseSkill(skillLevel))
				{
					return false;
				}
			}
			if (cCondition.m_MasterSkillID != -1)
			{
				int masterSkillLevel = -1;
				NKMUnit masterUnit2 = this.GetMasterUnit();
				if (masterUnit2 != null)
				{
					masterSkillLevel = masterUnit2.GetUnitData().GetSkillLevel(cCondition.m_MasterSkillStrID);
				}
				if (!cCondition.CanUseMasterSkill(masterSkillLevel))
				{
					return false;
				}
			}
			return cCondition.CheckHPRate(this) && cCondition.CanUseMapPosition(this.m_NKMGame.GetMapTemplet().GetMapFactor(this.GetUnitSyncData().m_PosX, this.m_NKMGame.IsATeam(this.GetUnitDataGame().m_NKM_TEAM_TYPE))) && cCondition.CanUseLevelRange(this) && cCondition.CanUseUnitExist(this.m_NKMGame, this.GetTeam());
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x0009C5B8 File Offset: 0x0009A7B8
		public void AddStaticBuffUnit(NKMStaticBuffData cNKMStaticBuffData)
		{
			if (this.CheckEventCondition(cNKMStaticBuffData.m_Condition))
			{
				short buffID = this.AddBuffByStrID(cNKMStaticBuffData.m_BuffStrID, cNKMStaticBuffData.m_BuffStatLevel, cNKMStaticBuffData.m_BuffTimeLevel, this.GetUnitDataGame().m_GameUnitUID, true, false, false, 1);
				NKMBuffData buff = this.GetBuff(buffID, false);
				if (buff != null && buff.m_NKMBuffTemplet.m_Range > 0f)
				{
					this.ProcessBuffRange(true, this, buff);
				}
				if (!cNKMStaticBuffData.m_fRange.IsNearlyZero(1E-05f))
				{
					List<NKMUnit> sortUnitListByNearDist = this.GetSortUnitListByNearDist();
					for (int i = 0; i < sortUnitListByNearDist.Count; i++)
					{
						NKMUnit nkmunit = sortUnitListByNearDist[i];
						if (nkmunit.GetUnitSyncData().m_GameUnitUID != this.GetUnitSyncData().m_GameUnitUID && nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_ENV && nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DYING && nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DIE && (cNKMStaticBuffData.m_bMyTeam || this.m_NKMGame.IsEnemy(this.GetUnitDataGame().m_NKM_TEAM_TYPE, nkmunit.GetUnitDataGame().m_NKM_TEAM_TYPE)) && (cNKMStaticBuffData.m_bEnemy || !this.m_NKMGame.IsEnemy(this.GetUnitDataGame().m_NKM_TEAM_TYPE, nkmunit.GetUnitDataGame().m_NKM_TEAM_TYPE)))
						{
							if (cNKMStaticBuffData.m_fRange < Math.Abs(this.GetUnitSyncData().m_PosX - nkmunit.GetUnitSyncData().m_PosX))
							{
								break;
							}
							nkmunit.AddBuffByStrID(cNKMStaticBuffData.m_BuffStrID, cNKMStaticBuffData.m_BuffStatLevel, cNKMStaticBuffData.m_BuffTimeLevel, this.GetUnitDataGame().m_GameUnitUID, true, false, false, 1);
						}
					}
				}
			}
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x0009C760 File Offset: 0x0009A960
		public void AddStaticBuffUnit()
		{
			this.m_listNKMStaticBuffDataRuntime.Clear();
			for (int i = 0; i < this.GetUnitTemplet().m_listStaticBuffData.Count; i++)
			{
				NKMStaticBuffData nkmstaticBuffData = this.GetUnitTemplet().m_listStaticBuffData[i];
				this.AddStaticBuffUnit(nkmstaticBuffData);
				if (nkmstaticBuffData.m_fRebuffTime > 0f)
				{
					NKMStaticBuffDataRuntime nkmstaticBuffDataRuntime = new NKMStaticBuffDataRuntime();
					nkmstaticBuffDataRuntime.m_NKMStaticBuffData = nkmstaticBuffData;
					nkmstaticBuffDataRuntime.m_fReBuffTimeNow = nkmstaticBuffData.m_fRebuffTime;
					this.m_listNKMStaticBuffDataRuntime.Add(nkmstaticBuffDataRuntime);
				}
			}
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x0009C7DE File Offset: 0x0009A9DE
		protected virtual void ProcessEventText(bool bStateEnd = false)
		{
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x0009C7E0 File Offset: 0x0009A9E0
		protected void ProcessEventSpeed(bool bStateEnd = false)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			if (this.m_UnitStateNow.m_bNoMove || this.m_UnitTemplet.m_bNoMove)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventSpeed.Count; i++)
			{
				NKMEventSpeed nkmeventSpeed = this.m_UnitStateNow.m_listNKMEventSpeed[i];
				if (nkmeventSpeed != null && this.CheckEventCondition(nkmeventSpeed.m_Condition))
				{
					bool flag = false;
					if (nkmeventSpeed.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (this.EventTimer(nkmeventSpeed.m_bAnimTime, nkmeventSpeed.m_fEventTimeMin, nkmeventSpeed.m_fEventTimeMax) && !nkmeventSpeed.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag)
					{
						if (!nkmeventSpeed.m_SpeedX.IsNearlyEqual(-1f, 1E-05f))
						{
							float speedX;
							if (nkmeventSpeed.m_bAnimTime)
							{
								speedX = nkmeventSpeed.GetSpeedX(this.m_UnitFrameData.m_fAnimTime, this.m_UnitFrameData.m_fSpeedX);
							}
							else
							{
								speedX = nkmeventSpeed.GetSpeedX(this.m_UnitFrameData.m_fStateTime, this.m_UnitFrameData.m_fSpeedX);
							}
							if (nkmeventSpeed.m_bAdd)
							{
								this.m_UnitFrameData.m_fSpeedX += speedX;
							}
							else if (nkmeventSpeed.m_bMultiply)
							{
								this.m_UnitFrameData.m_fSpeedX *= speedX;
							}
							else
							{
								this.m_UnitFrameData.m_fSpeedX = speedX;
							}
						}
						if (!nkmeventSpeed.m_SpeedY.IsNearlyEqual(-1f, 1E-05f))
						{
							float speedY;
							if (nkmeventSpeed.m_bAnimTime)
							{
								speedY = nkmeventSpeed.GetSpeedY(this.m_UnitFrameData.m_fAnimTime, this.m_UnitFrameData.m_fSpeedY);
							}
							else
							{
								speedY = nkmeventSpeed.GetSpeedY(this.m_UnitFrameData.m_fStateTime, this.m_UnitFrameData.m_fSpeedY);
							}
							if (nkmeventSpeed.m_bAdd)
							{
								this.m_UnitFrameData.m_fSpeedY += speedY;
							}
							else if (nkmeventSpeed.m_bMultiply)
							{
								this.m_UnitFrameData.m_fSpeedY *= speedY;
							}
							else
							{
								this.m_UnitFrameData.m_fSpeedY = speedY;
							}
						}
					}
				}
			}
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x0009C9F0 File Offset: 0x0009ABF0
		protected void ProcessEventSpeedX(bool bStateEnd = false)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			if (this.m_UnitStateNow.m_bNoMove || this.m_UnitTemplet.m_bNoMove)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventSpeedX.Count; i++)
			{
				NKMEventSpeedX nkmeventSpeedX = this.m_UnitStateNow.m_listNKMEventSpeedX[i];
				if (nkmeventSpeedX != null && this.CheckEventCondition(nkmeventSpeedX.m_Condition))
				{
					bool flag = false;
					if (nkmeventSpeedX.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (this.EventTimer(nkmeventSpeedX.m_bAnimTime, nkmeventSpeedX.m_fEventTimeMin, nkmeventSpeedX.m_fEventTimeMax) && !nkmeventSpeedX.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag)
					{
						float speed;
						if (nkmeventSpeedX.m_bAnimTime)
						{
							speed = nkmeventSpeedX.GetSpeed(this.m_UnitFrameData.m_fAnimTime, this.m_UnitFrameData.m_fSpeedX);
						}
						else
						{
							speed = nkmeventSpeedX.GetSpeed(this.m_UnitFrameData.m_fStateTime, this.m_UnitFrameData.m_fSpeedX);
						}
						if (nkmeventSpeedX.m_bAdd)
						{
							this.m_UnitFrameData.m_fSpeedX += speed;
						}
						else if (nkmeventSpeedX.m_bMultiply)
						{
							this.m_UnitFrameData.m_fSpeedX *= speed;
						}
						else
						{
							this.m_UnitFrameData.m_fSpeedX = speed;
						}
					}
				}
			}
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x0009CB38 File Offset: 0x0009AD38
		protected void ProcessEventSpeedY(bool bStateEnd = false)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			if (this.m_UnitStateNow.m_bNoMove || this.m_UnitTemplet.m_bNoMove)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventSpeedY.Count; i++)
			{
				NKMEventSpeedY nkmeventSpeedY = this.m_UnitStateNow.m_listNKMEventSpeedY[i];
				if (nkmeventSpeedY != null && this.CheckEventCondition(nkmeventSpeedY.m_Condition))
				{
					bool flag = false;
					if (nkmeventSpeedY.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (this.EventTimer(nkmeventSpeedY.m_bAnimTime, nkmeventSpeedY.m_fEventTimeMin, nkmeventSpeedY.m_fEventTimeMax) && !nkmeventSpeedY.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag)
					{
						float speed;
						if (nkmeventSpeedY.m_bAnimTime)
						{
							speed = nkmeventSpeedY.GetSpeed(this.m_UnitFrameData.m_fAnimTime, this.m_UnitFrameData.m_fSpeedY);
						}
						else
						{
							speed = nkmeventSpeedY.GetSpeed(this.m_UnitFrameData.m_fStateTime, this.m_UnitFrameData.m_fSpeedY);
						}
						if (nkmeventSpeedY.m_bAdd)
						{
							this.m_UnitFrameData.m_fSpeedY += speed;
						}
						else if (nkmeventSpeedY.m_bMultiply)
						{
							this.m_UnitFrameData.m_fSpeedY *= speed;
						}
						else
						{
							this.m_UnitFrameData.m_fSpeedY = speed;
						}
					}
				}
			}
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x0009CC80 File Offset: 0x0009AE80
		protected void ProcessEventMove(bool bStateEnd = false)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			if (this.m_UnitStateNow.m_bNoMove || this.m_UnitTemplet.m_bNoMove)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventMove.Count; i++)
			{
				NKMEventMove cNKMEventMove = this.m_UnitStateNow.m_listNKMEventMove[i];
				this.ApplyEventMove(cNKMEventMove, true, bStateEnd, false);
			}
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x0009CCE8 File Offset: 0x0009AEE8
		public void ApplyEventMove(NKMEventMove cNKMEventMove, bool bUseEventTimer, bool bStateEnd = false, bool bFromDE = false)
		{
			if (cNKMEventMove == null)
			{
				return;
			}
			if (!this.CheckEventCondition(cNKMEventMove.m_Condition))
			{
				return;
			}
			if (bUseEventTimer)
			{
				bool flag = false;
				if (cNKMEventMove.m_bStateEndTime && bStateEnd)
				{
					flag = true;
				}
				else if (this.EventTimer(cNKMEventMove.m_bAnimTime, cNKMEventMove.m_fEventTime, true) && !cNKMEventMove.m_bStateEndTime)
				{
					flag = true;
				}
				if (!flag)
				{
					return;
				}
			}
			if (cNKMEventMove.m_bSavePosition || cNKMEventMove.m_MoveBase == NKMEventMove.MoveBase.SAVE_ONLY)
			{
				if (cNKMEventMove.m_fEventTime == 0f && this.m_NKM_UNIT_CLASS_TYPE == NKM_UNIT_CLASS_TYPE.NCT_UNIT_CLIENT)
				{
					this.SaveEventMoveXPosition(this.m_UnitFrameData.m_PosXBefore);
				}
				else
				{
					this.SaveEventMoveXPosition(this.m_UnitFrameData.m_PosXCalc);
				}
				if (cNKMEventMove.m_MoveBase == NKMEventMove.MoveBase.SAVE_ONLY)
				{
					return;
				}
			}
			this.m_EventMovePosX.StopTracking();
			this.m_EventMovePosJumpY.StopTracking();
			this.m_EventMovePosZ.StopTracking();
			float eventMovePosX = this.GetEventMovePosX(cNKMEventMove, this.IsATeam());
			float eventMovePosY = this.GetEventMovePosY(cNKMEventMove);
			if (cNKMEventMove.m_fSpeed > 0f)
			{
				float fTime = Math.Abs(this.m_UnitFrameData.m_PosXCalc - eventMovePosX) / cNKMEventMove.m_fSpeed;
				this.m_EventMovePosX.SetNowValue(this.m_UnitFrameData.m_PosXCalc);
				this.m_EventMovePosX.SetTracking(eventMovePosX, fTime, cNKMEventMove.m_MoveTrackingType);
				if (!cNKMEventMove.m_bLandMove)
				{
					this.m_EventMovePosJumpY.SetNowValue(this.m_UnitFrameData.m_JumpYPosCalc);
					this.m_EventMovePosJumpY.SetTracking(eventMovePosY, fTime, cNKMEventMove.m_MoveTrackingType);
				}
			}
			else if (cNKMEventMove.m_MoveTime > 0f)
			{
				this.m_EventMovePosX.SetNowValue(this.m_UnitFrameData.m_PosXCalc);
				this.m_EventMovePosX.SetTracking(eventMovePosX, cNKMEventMove.m_MoveTime, cNKMEventMove.m_MoveTrackingType);
				if (!cNKMEventMove.m_bLandMove)
				{
					this.m_EventMovePosJumpY.SetNowValue(this.m_UnitFrameData.m_JumpYPosCalc);
					this.m_EventMovePosJumpY.SetTracking(eventMovePosY, cNKMEventMove.m_MoveTime, cNKMEventMove.m_MoveTrackingType);
				}
			}
			else
			{
				this.m_UnitFrameData.m_PosXCalc = eventMovePosX;
				if (!cNKMEventMove.m_bLandMove)
				{
					this.m_UnitFrameData.m_JumpYPosCalc = eventMovePosY;
				}
			}
			if (bFromDE && cNKMEventMove.m_fSpeed <= 0f && cNKMEventMove.m_MoveTime <= 0f)
			{
				this.m_UnitSyncData.m_PosX = this.m_UnitFrameData.m_PosXCalc;
				if (!cNKMEventMove.m_bLandMove)
				{
					this.m_UnitSyncData.m_JumpYPos = this.m_UnitFrameData.m_JumpYPosCalc;
				}
			}
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x0009CF39 File Offset: 0x0009B139
		public void SaveEventMoveXPosition(float posX)
		{
			this.m_fEventMoveSavedPositionX = posX;
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x0009CF44 File Offset: 0x0009B144
		public virtual float GetEventMovePosX(NKMEventMove cNKMEventMove, bool isATeam)
		{
			float eventMoveBasePosX = this.GetEventMoveBasePosX(cNKMEventMove, isATeam);
			if (this.GetEventMoveOffsetRight(cNKMEventMove, eventMoveBasePosX, isATeam))
			{
				return eventMoveBasePosX + cNKMEventMove.m_OffsetX;
			}
			return eventMoveBasePosX - cNKMEventMove.m_OffsetX;
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x0009CF78 File Offset: 0x0009B178
		public float GetEventMoveBasePosX(NKMEventMove cNKMEventMove, bool isATeam)
		{
			switch (cNKMEventMove.m_MoveBase)
			{
			case NKMEventMove.MoveBase.TARGET_UNIT:
				if (this.m_UnitSyncData.m_TargetUID > 0 && this.m_TargetUnit != null)
				{
					return this.m_TargetUnit.GetUnitSyncData().m_PosX;
				}
				return this.GetUnitFrameData().m_LastTargetPosX;
			case NKMEventMove.MoveBase.SUB_TARGET_UNIT:
				if (this.m_UnitSyncData.m_SubTargetUID != 0 && this.m_SubTargetUnit != null)
				{
					return this.m_SubTargetUnit.GetUnitSyncData().m_PosX;
				}
				return this.GetUnitFrameData().m_PosXCalc;
			case NKMEventMove.MoveBase.MAP_RATE:
				return this.m_NKMGame.GetMapTemplet().GetMapRatePos(cNKMEventMove.m_fMapPosFactor, isATeam);
			case NKMEventMove.MoveBase.MY_SHIP:
			{
				NKMUnit myBoss = this.GetMyBoss();
				if (myBoss != null)
				{
					return myBoss.GetUnitSyncData().m_PosX;
				}
				return this.m_NKMGame.GetMapTemplet().GetMapRatePos(0f, isATeam);
			}
			case NKMEventMove.MoveBase.ENEMY_SHIP:
			{
				NKMUnit liveEnemyBossUnit = this.m_NKMGame.GetLiveEnemyBossUnit(this.GetTeam());
				if (liveEnemyBossUnit != null)
				{
					return liveEnemyBossUnit.GetUnitSyncData().m_PosX;
				}
				return this.m_NKMGame.GetMapTemplet().GetMapRatePos(1f, isATeam);
			}
			case NKMEventMove.MoveBase.SAVED_POS:
				return this.m_fEventMoveSavedPositionX;
			}
			return this.GetUnitFrameData().m_PosXCalc;
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x0009D0AC File Offset: 0x0009B2AC
		public bool GetEventMoveOffsetRight(NKMEventMove cNKMEventMove, float basePos, bool isATeam)
		{
			bool flag;
			switch (cNKMEventMove.m_MoveOffset)
			{
			case NKMEventMove.MoveOffset.ME:
			case NKMEventMove.MoveOffset.MASTER_UNIT:
				return this.GetUnitFrameData().m_PosXCalc > basePos;
			case NKMEventMove.MoveOffset.ME_INV:
				return this.GetUnitFrameData().m_PosXCalc <= basePos;
			case NKMEventMove.MoveOffset.TARGET_UNIT:
			{
				float num;
				if (this.m_UnitSyncData.m_TargetUID > 0 && this.m_TargetUnit != null)
				{
					num = this.m_TargetUnit.GetUnitSyncData().m_PosX;
				}
				else
				{
					num = this.GetUnitFrameData().m_LastTargetPosX;
				}
				return basePos < num;
			}
			case NKMEventMove.MoveOffset.TARGET_UNIT_INV:
			{
				float num2;
				if (this.m_UnitSyncData.m_TargetUID > 0 && this.m_TargetUnit != null)
				{
					num2 = this.m_TargetUnit.GetUnitSyncData().m_PosX;
				}
				else
				{
					num2 = this.GetUnitFrameData().m_LastTargetPosX;
				}
				return basePos >= num2;
			}
			case NKMEventMove.MoveOffset.SUB_TARGET_UNIT:
			{
				float num3;
				if (this.m_UnitSyncData.m_SubTargetUID != 0 && this.m_SubTargetUnit != null)
				{
					num3 = this.m_SubTargetUnit.GetUnitSyncData().m_PosX;
				}
				else
				{
					num3 = this.GetUnitFrameData().m_PosXCalc;
				}
				return basePos < num3;
			}
			case NKMEventMove.MoveOffset.MY_SHIP:
			case NKMEventMove.MoveOffset.MY_SHIP_INV:
			{
				NKMUnit myBoss = this.GetMyBoss();
				if (myBoss != null)
				{
					flag = (this.GetUnitFrameData().m_PosXCalc < myBoss.GetUnitSyncData().m_PosX);
				}
				else
				{
					flag = !isATeam;
				}
				if (cNKMEventMove.m_MoveOffset == NKMEventMove.MoveOffset.MY_SHIP_INV)
				{
					return !flag;
				}
				return flag;
			}
			case NKMEventMove.MoveOffset.ENEMY_SHIP:
			case NKMEventMove.MoveOffset.ENEMY_SHIP_INV:
			{
				NKMUnit liveEnemyBossUnit = this.m_NKMGame.GetLiveEnemyBossUnit(this.GetTeam());
				if (liveEnemyBossUnit != null)
				{
					flag = (this.GetUnitFrameData().m_PosXCalc < liveEnemyBossUnit.GetUnitSyncData().m_PosX);
				}
				else
				{
					flag = !isATeam;
				}
				if (cNKMEventMove.m_MoveOffset == NKMEventMove.MoveOffset.ENEMY_SHIP_INV)
				{
					return !flag;
				}
				return flag;
			}
			case NKMEventMove.MoveOffset.TEAM_DIR:
				return isATeam;
			case NKMEventMove.MoveOffset.MAP_RATE:
			{
				float mapRatePos = this.m_NKMGame.GetMapTemplet().GetMapRatePos(cNKMEventMove.m_fMapPosFactor, isATeam);
				return basePos < mapRatePos;
			}
			case NKMEventMove.MoveOffset.SAVED_POS:
				return basePos < this.m_fEventMoveSavedPositionX;
			}
			flag = this.GetUnitSyncData().m_bRight;
			return flag;
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x0009D2C8 File Offset: 0x0009B4C8
		public float GetEventMovePosY(NKMEventMove cNKMEventMove)
		{
			switch (cNKMEventMove.m_MoveBase)
			{
			case NKMEventMove.MoveBase.TARGET_UNIT:
			{
				float num;
				if (this.m_UnitSyncData.m_TargetUID > 0 && this.m_TargetUnit != null)
				{
					num = this.m_TargetUnit.GetUnitSyncData().m_JumpYPos;
				}
				else
				{
					num = this.GetUnitFrameData().m_LastTargetJumpYPos;
				}
				return num + cNKMEventMove.m_OffsetJumpYPos;
			}
			case NKMEventMove.MoveBase.SUB_TARGET_UNIT:
				if (this.m_UnitSyncData.m_SubTargetUID != 0 && this.m_SubTargetUnit != null)
				{
					return this.m_SubTargetUnit.GetUnitSyncData().m_JumpYPos + cNKMEventMove.m_OffsetJumpYPos;
				}
				return this.m_UnitFrameData.m_JumpYPosCalc + cNKMEventMove.m_OffsetJumpYPos;
			}
			return this.m_UnitFrameData.m_JumpYPosCalc + cNKMEventMove.m_OffsetJumpYPos;
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x0009D384 File Offset: 0x0009B584
		public NKMUnit GetMyBoss()
		{
			return this.m_NKMGame.GetLiveMyBossUnit(this.GetTeam());
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x0009D398 File Offset: 0x0009B598
		protected NKMDamageInst GetDamageInstAtk(int index)
		{
			if (this.m_listDamageInstAtk.Count <= index)
			{
				int num = index - this.m_listDamageInstAtk.Count;
				for (int i = 0; i <= num; i++)
				{
					NKMDamageInst item = new NKMDamageInst();
					this.m_listDamageInstAtk.Add(item);
				}
			}
			return this.m_listDamageInstAtk[index];
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x0009D3EC File Offset: 0x0009B5EC
		protected virtual void ProcessEventAttack()
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventAttack.Count; i++)
			{
				NKMEventAttack nkmeventAttack = this.m_UnitStateNow.m_listNKMEventAttack[i];
				if (nkmeventAttack != null && this.CheckEventCondition(nkmeventAttack.m_Condition))
				{
					bool flag = false;
					if (this.EventTimer(nkmeventAttack.m_bAnimTime, nkmeventAttack.m_fEventTimeMin, true))
					{
						flag = true;
					}
					if (flag)
					{
						NKMDamageInst damageInstAtk = this.GetDamageInstAtk(i);
						if (damageInstAtk != null)
						{
							damageInstAtk.Init();
						}
					}
					flag = false;
					if (this.EventTimer(nkmeventAttack.m_bAnimTime, nkmeventAttack.m_fEventTimeMin, nkmeventAttack.m_fEventTimeMax))
					{
						flag = true;
					}
					if (nkmeventAttack.m_fEventTimeMin.IsNearlyEqual(nkmeventAttack.m_fEventTimeMax, 1E-05f) && this.EventTimer(nkmeventAttack.m_bAnimTime, nkmeventAttack.m_fEventTimeMin, true))
					{
						flag = true;
					}
					if (flag)
					{
						NKMDamageInst damageInstAtk2 = this.GetDamageInstAtk(i);
						if (damageInstAtk2.m_Templet == null)
						{
							damageInstAtk2.m_Templet = NKMDamageManager.GetTempletByStrID(nkmeventAttack.m_DamageTempletName);
							damageInstAtk2.m_AttackerType = NKM_REACTOR_TYPE.NRT_GAME_UNIT;
							damageInstAtk2.m_AttackerGameUnitUID = this.m_UnitDataGame.m_GameUnitUID;
							damageInstAtk2.m_AttackerUnitSkillTemplet = this.GetStateSkill(this.m_UnitStateNow);
							damageInstAtk2.m_AttackerTeamType = this.GetUnitDataGame().m_NKM_TEAM_TYPE;
						}
						if (this.m_NKMGame.DamageCheck(damageInstAtk2, nkmeventAttack, false))
						{
							if (nkmeventAttack.m_EffectName.Length > 1)
							{
								this.ProcessAttackHitEffect(nkmeventAttack);
							}
							if (nkmeventAttack.m_HitStateChange.Length > 1)
							{
								this.StateChange(nkmeventAttack.m_HitStateChange, false, false);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x0009D571 File Offset: 0x0009B771
		protected virtual void ProcessAttackHitEffect(NKMEventAttack cNKMEventAttack)
		{
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x0009D574 File Offset: 0x0009B774
		protected void ProcessEventStopTime(bool bStateEnd = false)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			if (this.m_NKMGame.GetDungeonTemplet() != null && this.m_NKMGame.GetDungeonTemplet().m_bNoTimeStop)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventStopTime.Count; i++)
			{
				NKMEventStopTime nkmeventStopTime = this.m_UnitStateNow.m_listNKMEventStopTime[i];
				if (nkmeventStopTime != null && this.CheckEventCondition(nkmeventStopTime.m_Condition))
				{
					bool flag = false;
					if (nkmeventStopTime.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (this.EventTimer(nkmeventStopTime.m_bAnimTime, nkmeventStopTime.m_fEventTime, true) && !nkmeventStopTime.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag)
					{
						if (!nkmeventStopTime.m_fStopReserveTime.IsNearlyEqual(-1f, 1E-05f))
						{
							this.SetStopReserveTime(nkmeventStopTime.m_fStopReserveTime);
						}
						this.m_NKMGame.SetStopTime(this.m_UnitDataGame.m_GameUnitUID, nkmeventStopTime.m_fStopTime, nkmeventStopTime.m_bStopSelf, nkmeventStopTime.m_bStopSummonee, nkmeventStopTime.m_StopTimeIndex);
					}
				}
			}
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x0009D678 File Offset: 0x0009B878
		protected void ProcessEventInvincible()
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			this.m_UnitFrameData.m_bInvincible = this.m_UnitTemplet.m_Invincible;
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventInvincible.Count; i++)
			{
				NKMEventInvincible nkmeventInvincible = this.m_UnitStateNow.m_listNKMEventInvincible[i];
				if (nkmeventInvincible != null && this.CheckEventCondition(nkmeventInvincible.m_Condition) && this.EventTimer(nkmeventInvincible.m_bAnimTime, nkmeventInvincible.m_fEventTimeMin, nkmeventInvincible.m_fEventTimeMax))
				{
					this.m_UnitFrameData.m_bInvincible = true;
				}
			}
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x0009D708 File Offset: 0x0009B908
		protected void ProcessEventInvincibleGlobal(bool bStateEnd = false)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			if (this.m_NKM_UNIT_CLASS_TYPE == NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER && this.m_NKMGame.GetDungeonType() == NKM_DUNGEON_TYPE.NDT_DAMAGE_ACCRUE && this.m_bBoss && this.m_NKMGame.IsATeam(this.GetUnitDataGame().m_NKM_TEAM_TYPE) && !this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_INVINCIBLE))
			{
				this.ApplyStatusTime(NKM_UNIT_STATUS_EFFECT.NUSE_INVINCIBLE, 600f, this, false, false, false);
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventInvincibleGlobal.Count; i++)
			{
				NKMEventInvincibleGlobal nkmeventInvincibleGlobal = this.m_UnitStateNow.m_listNKMEventInvincibleGlobal[i];
				if (nkmeventInvincibleGlobal != null && this.CheckEventCondition(nkmeventInvincibleGlobal.m_Condition))
				{
					bool flag = false;
					if (nkmeventInvincibleGlobal.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (this.EventTimer(nkmeventInvincibleGlobal.m_bAnimTime, nkmeventInvincibleGlobal.m_fEventTime, true) && !nkmeventInvincibleGlobal.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag)
					{
						this.ApplyStatusTime(NKM_UNIT_STATUS_EFFECT.NUSE_INVINCIBLE, nkmeventInvincibleGlobal.m_InvincibleTime, this, false, false, true);
					}
				}
			}
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x0009D7F0 File Offset: 0x0009B9F0
		protected void ProcessEventSuperArmor()
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			this.m_UnitFrameData.m_SuperArmorLevel = NKM_SUPER_ARMOR_LEVEL.NSAL_NO;
			if (this.m_UnitTemplet.m_SuperArmorLevel != NKM_SUPER_ARMOR_LEVEL.NSAL_INVALID)
			{
				this.m_UnitFrameData.m_SuperArmorLevel = this.m_UnitTemplet.m_SuperArmorLevel;
			}
			if (this.m_UnitStateNow.m_SuperArmorLevel != NKM_SUPER_ARMOR_LEVEL.NSAL_INVALID)
			{
				this.m_UnitFrameData.m_SuperArmorLevel = this.m_UnitStateNow.m_SuperArmorLevel;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventSuperArmor.Count; i++)
			{
				NKMEventSuperArmor nkmeventSuperArmor = this.m_UnitStateNow.m_listNKMEventSuperArmor[i];
				if (nkmeventSuperArmor != null && this.EventTimer(nkmeventSuperArmor.m_bAnimTime, nkmeventSuperArmor.m_fEventTimeMin, nkmeventSuperArmor.m_fEventTimeMax))
				{
					this.m_UnitFrameData.m_SuperArmorLevel = nkmeventSuperArmor.m_SuperArmorLevel;
				}
			}
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x0009D8B3 File Offset: 0x0009BAB3
		protected virtual void ProcessEventSound(bool bStateEnd = false)
		{
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x0009D8B5 File Offset: 0x0009BAB5
		protected virtual void ProcessEventColor(bool bStateEnd = false)
		{
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x0009D8B7 File Offset: 0x0009BAB7
		protected virtual void ProcessEventCameraCrash(bool bStateEnd = false)
		{
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x0009D8B9 File Offset: 0x0009BAB9
		protected virtual void ProcessEventCameraMove(bool bStateEnd = false)
		{
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x0009D8BB File Offset: 0x0009BABB
		protected virtual void ProcessEventFadeWorld()
		{
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x0009D8BD File Offset: 0x0009BABD
		protected virtual void ProcessEventDissolve(bool bStateEnd = false)
		{
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x0009D8BF File Offset: 0x0009BABF
		protected virtual void ProcessEventMotionBlur()
		{
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x0009D8C1 File Offset: 0x0009BAC1
		protected virtual void ProcessEventEffect(bool bStateEnd = false)
		{
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x0009D8C3 File Offset: 0x0009BAC3
		protected virtual void ProcessEventHyperSkillCutIn()
		{
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x0009D8C8 File Offset: 0x0009BAC8
		protected void ProcessEventDamageEffect(bool bStateEnd = false)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			if (bStateEnd)
			{
				for (LinkedListNode<NKMDamageEffect> linkedListNode = this.m_linklistDamageEffect.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					NKMDamageEffect value = linkedListNode.Value;
					if (value.GetStateEndDie())
					{
						value.SetDie(false, true);
					}
				}
			}
			else
			{
				LinkedListNode<NKMDamageEffect> linkedListNode2 = this.m_linklistDamageEffect.First;
				while (linkedListNode2 != null)
				{
					NKMDamageEffect value2 = linkedListNode2.Value;
					if (!this.m_NKMGame.GetDEManager().IsLiveEffect(value2.GetDEUID()))
					{
						LinkedListNode<NKMDamageEffect> next = linkedListNode2.Next;
						this.m_linklistDamageEffect.Remove(linkedListNode2);
						linkedListNode2 = next;
					}
					else
					{
						value2.SetRight(this.m_UnitSyncData.m_bRight);
						value2.SetFollowPos(this.m_UnitSyncData.m_PosX, this.m_UnitSyncData.m_JumpYPos, this.m_UnitSyncData.m_PosZ, true);
						linkedListNode2 = linkedListNode2.Next;
					}
				}
			}
			NKMUnitSkillTemplet stateSkill = this.GetStateSkill(this.m_UnitStateNow);
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventDamageEffect.Count; i++)
			{
				NKMEventDamageEffect nkmeventDamageEffect = this.m_UnitStateNow.m_listNKMEventDamageEffect[i];
				if (nkmeventDamageEffect != null && this.CheckEventCondition(nkmeventDamageEffect.m_Condition))
				{
					bool flag = false;
					if (nkmeventDamageEffect.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (this.EventTimer(nkmeventDamageEffect.m_bAnimTime, nkmeventDamageEffect.m_fEventTime, true) && !nkmeventDamageEffect.m_bStateEndTime)
					{
						flag = true;
					}
					if (nkmeventDamageEffect.m_bIgnoreNoTarget && this.m_TargetUnit == null)
					{
						flag = false;
					}
					if (flag)
					{
						this.ProcessEventDamageEffect(nkmeventDamageEffect, stateSkill, this.m_UnitSyncData.m_PosX, this.m_UnitSyncData.m_JumpYPos, this.m_UnitSyncData.m_PosZ);
					}
				}
			}
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x0009DA74 File Offset: 0x0009BC74
		protected void ProcessEventDamageEffect(NKMEventDamageEffect cNKMEventDamageEffect, NKMUnitSkillTemplet cUnitStateSkillTemplet, float fPosX, float fJumpYPos, float fPosZ)
		{
			if (cNKMEventDamageEffect.m_bIgnoreNoTarget && this.m_TargetUnit == null)
			{
				return;
			}
			this.m_NKMVector3Temp.x = this.m_UnitSyncData.m_PosX;
			this.m_NKMVector3Temp.y = this.m_UnitSyncData.m_JumpYPos;
			this.m_NKMVector3Temp.z = this.m_UnitSyncData.m_PosZ;
			if (cNKMEventDamageEffect.m_bTargetPos)
			{
				if (this.m_TargetUnit != null)
				{
					this.m_NKMVector3Temp.x = this.m_TargetUnit.GetUnitSyncData().m_PosX;
					this.m_NKMVector3Temp.y = this.m_TargetUnit.GetUnitSyncData().m_JumpYPos;
				}
				else
				{
					this.m_NKMVector3Temp.x = this.GetUnitFrameData().m_LastTargetPosX;
					this.m_NKMVector3Temp.y = this.GetUnitFrameData().m_LastTargetJumpYPos;
				}
			}
			if (cNKMEventDamageEffect.m_bUseMapPos)
			{
				if (this.m_UnitSyncData.m_bRight)
				{
					this.m_NKMVector3Temp.x = this.m_NKMGame.GetMapTemplet().GetMapRatePos(cNKMEventDamageEffect.m_fMapPosRate, true);
				}
				else
				{
					this.m_NKMVector3Temp.x = this.m_NKMGame.GetMapTemplet().GetMapRatePos(cNKMEventDamageEffect.m_fMapPosRate, false);
				}
			}
			if (cNKMEventDamageEffect.m_bShipSkillPos)
			{
				this.m_NKMVector3Temp.x = this.m_UnitFrameData.m_fShipSkillPosX;
				this.m_NKMVector3Temp.y = 0f;
				this.m_NKMVector3Temp.z = this.m_NKMGame.GetMapTemplet().m_fMinZ + (this.m_NKMGame.GetMapTemplet().m_fMaxZ - this.m_NKMGame.GetMapTemplet().m_fMinZ) / 2f;
			}
			float zscaleFactor = this.m_NKMGame.GetZScaleFactor(this.GetUnitSyncData().m_PosZ);
			short targetGameUID = this.m_UnitSyncData.m_TargetUID;
			if (cNKMEventDamageEffect.m_bIgnoreTarget)
			{
				targetGameUID = 0;
			}
			string templetID = cNKMEventDamageEffect.m_DEName;
			if (this.m_NKMGame.GetGameData().IsPVP() && cNKMEventDamageEffect.m_DENamePVP.Length > 1)
			{
				templetID = cNKMEventDamageEffect.m_DENamePVP;
			}
			NKMDamageEffect nkmdamageEffect = this.m_NKMGame.GetDEManager().UseDamageEffect(templetID, this.m_UnitSyncData.m_GameUnitUID, targetGameUID, cUnitStateSkillTemplet, this.GetUnitFrameData().m_PhaseNow, this.m_NKMVector3Temp.x, this.m_NKMVector3Temp.y, this.m_NKMVector3Temp.z, this.m_UnitSyncData.m_bRight, cNKMEventDamageEffect.m_OffsetX * zscaleFactor, cNKMEventDamageEffect.m_OffsetY * zscaleFactor, cNKMEventDamageEffect.m_OffsetZ, cNKMEventDamageEffect.m_fAddRotate, cNKMEventDamageEffect.m_bUseZScale, cNKMEventDamageEffect.m_fSpeedFactorX, cNKMEventDamageEffect.m_fSpeedFactorY, cNKMEventDamageEffect.m_fReserveTime, false);
			if (nkmdamageEffect != null && (cNKMEventDamageEffect.m_bHold || cNKMEventDamageEffect.m_bStateEndStop))
			{
				nkmdamageEffect.SetHoldFollowData(cNKMEventDamageEffect.m_FollowType, cNKMEventDamageEffect.m_FollowTime, cNKMEventDamageEffect.m_FollowUpdateTime);
				nkmdamageEffect.SetStateEndDie(cNKMEventDamageEffect.m_bStateEndStop);
				this.m_linklistDamageEffect.AddLast(nkmdamageEffect);
			}
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x0009DD40 File Offset: 0x0009BF40
		protected void ProcessEventDEStateChange(bool bStateEnd = false)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			if (this.m_linklistDamageEffect == null || this.m_linklistDamageEffect.First == null)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventDEStateChange.Count; i++)
			{
				NKMEventDEStateChange nkmeventDEStateChange = this.m_UnitStateNow.m_listNKMEventDEStateChange[i];
				if (nkmeventDEStateChange != null && this.CheckEventCondition(nkmeventDEStateChange.m_Condition))
				{
					bool flag = false;
					if (nkmeventDEStateChange.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (this.EventTimer(nkmeventDEStateChange.m_bAnimTime, nkmeventDEStateChange.m_fEventTime, true) && !nkmeventDEStateChange.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag)
					{
						foreach (NKMDamageEffect nkmdamageEffect in this.m_linklistDamageEffect)
						{
							if (nkmdamageEffect != null && nkmdamageEffect.GetTemplet() != null && (!bStateEnd || !nkmdamageEffect.GetStateEndDie()) && nkmdamageEffect.GetTemplet().m_DamageEffectID.Equals(nkmeventDEStateChange.m_DamageEffectID))
							{
								nkmdamageEffect.StateChangeByUnitState(nkmeventDEStateChange.m_ChangeState, true);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x0009DE6C File Offset: 0x0009C06C
		protected void ProcessEventGameSpeed(bool bStateEnd = false)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventGameSpeed.Count; i++)
			{
				NKMEventGameSpeed nkmeventGameSpeed = this.m_UnitStateNow.m_listNKMEventGameSpeed[i];
				if (nkmeventGameSpeed != null && this.CheckEventCondition(nkmeventGameSpeed.m_Condition))
				{
					bool flag = false;
					if (nkmeventGameSpeed.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (this.EventTimer(nkmeventGameSpeed.m_bAnimTime, nkmeventGameSpeed.m_fEventTime, true) && !nkmeventGameSpeed.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag)
					{
						this.m_NKMGame.SetGameSpeed(nkmeventGameSpeed.m_fGameSpeed, nkmeventGameSpeed.m_fTrackingTime);
					}
				}
			}
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x0009DF10 File Offset: 0x0009C110
		protected void ProcessEventAnimSpeed()
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventAnimSpeed.Count; i++)
			{
				NKMEventAnimSpeed nkmeventAnimSpeed = this.m_UnitStateNow.m_listNKMEventAnimSpeed[i];
				if (nkmeventAnimSpeed != null && this.CheckEventCondition(nkmeventAnimSpeed.m_Condition))
				{
					bool flag = false;
					if (this.EventTimer(nkmeventAnimSpeed.m_bAnimTime, nkmeventAnimSpeed.m_fEventTime, true))
					{
						flag = true;
					}
					if (flag)
					{
						this.ChangeAnimSpeed(nkmeventAnimSpeed.m_fAnimSpeed);
					}
				}
			}
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x0009DF8C File Offset: 0x0009C18C
		protected void ProcessEventBuff(bool bStateEnd = false)
		{
			if (this.m_NKM_UNIT_CLASS_TYPE != NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
			{
				return;
			}
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			if (bStateEnd)
			{
				foreach (KeyValuePair<short, NKMBuffData> keyValuePair in this.m_UnitFrameData.m_dicBuffData)
				{
					NKMBuffData value = keyValuePair.Value;
					if (value != null && value.m_BuffSyncData.m_MasterGameUnitUID == this.GetUnitSyncData().m_GameUnitUID && value.m_StateEndRemove)
					{
						value.m_StateEndCheck = true;
					}
				}
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventBuff.Count; i++)
			{
				NKMEventBuff nkmeventBuff = this.m_UnitStateNow.m_listNKMEventBuff[i];
				if (nkmeventBuff != null && !nkmeventBuff.m_bReflection && this.CheckEventCondition(nkmeventBuff.m_Condition))
				{
					bool flag = false;
					if (nkmeventBuff.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (this.EventTimer(nkmeventBuff.m_bAnimTime, nkmeventBuff.m_fEventTime, true) && !nkmeventBuff.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag)
					{
						nkmeventBuff.Process(this.m_NKMGame, this);
					}
				}
			}
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x0009E0BC File Offset: 0x0009C2BC
		protected void ProcessEventStatus(bool bStateEnd = false)
		{
			if (this.m_NKM_UNIT_CLASS_TYPE != NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
			{
				return;
			}
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventStatus.Count; i++)
			{
				NKMEventStatus nkmeventStatus = this.m_UnitStateNow.m_listNKMEventStatus[i];
				if (nkmeventStatus != null)
				{
					nkmeventStatus.ProcessEvent(this.m_NKMGame, this, bStateEnd);
				}
			}
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x0009E11A File Offset: 0x0009C31A
		protected virtual void ProcessEventRespawn(bool bStateEnd = false)
		{
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x0009E11C File Offset: 0x0009C31C
		protected virtual bool ProcessEventRespawn(NKMEventRespawn cNKMEventRespawn, float fRespawnPosX, float rollbackTime = 0f)
		{
			return true;
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x0009E120 File Offset: 0x0009C320
		protected virtual void ProcessEventUnitChange(bool bStateEnd = false)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			if (this.m_UnitStateNow.m_NKMEventUnitChange != null)
			{
				if (!this.CheckEventCondition(this.m_UnitStateNow.m_NKMEventUnitChange.m_Condition))
				{
					return;
				}
				bool flag = false;
				if (this.m_UnitStateNow.m_NKMEventUnitChange.m_bStateEndTime && bStateEnd)
				{
					flag = true;
				}
				else if (this.EventTimer(this.m_UnitStateNow.m_NKMEventUnitChange.m_bAnimTime, this.m_UnitStateNow.m_NKMEventUnitChange.m_fEventTime, true) && !this.m_UnitStateNow.m_NKMEventUnitChange.m_bStateEndTime)
				{
					flag = true;
				}
				if (flag)
				{
					this.EventDie(true, false, true);
				}
			}
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x0009E1C0 File Offset: 0x0009C3C0
		protected void ProcessEventDie(bool bStateEnd = false)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventDie.Count; i++)
			{
				NKMEventDie nkmeventDie = this.m_UnitStateNow.m_listNKMEventDie[i];
				if (nkmeventDie != null && this.CheckEventCondition(nkmeventDie.m_Condition))
				{
					bool flag = false;
					if (nkmeventDie.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (this.EventTimer(nkmeventDie.m_bAnimTime, nkmeventDie.m_fEventTime, true) && !nkmeventDie.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag)
					{
						this.EventDie(nkmeventDie.m_bImmediateDie, true, false);
					}
				}
			}
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x0009E258 File Offset: 0x0009C458
		protected void ProcessEventChangeState(bool bStateEnd = false)
		{
			if (this.m_NKM_UNIT_CLASS_TYPE != NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
			{
				return;
			}
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventChangeState.Count; i++)
			{
				NKMEventChangeState nkmeventChangeState = this.m_UnitStateNow.m_listNKMEventChangeState[i];
				if (nkmeventChangeState != null && this.CheckEventCondition(nkmeventChangeState.m_Condition))
				{
					bool flag = false;
					if (nkmeventChangeState.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (this.EventTimer(nkmeventChangeState.m_bAnimTime, nkmeventChangeState.m_fEventTime, true) && !nkmeventChangeState.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag)
					{
						if (nkmeventChangeState.m_TargetUnitID <= 0)
						{
							this.StateChange(nkmeventChangeState.m_ChangeState, true, false);
						}
						else
						{
							List<NKMUnit> list = new List<NKMUnit>();
							this.m_NKMGame.GetUnitByUnitID(list, nkmeventChangeState.m_TargetUnitID, true, false);
							for (int j = 0; j < list.Count; j++)
							{
								NKMUnit nkmunit = list[j];
								if (nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DYING && nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DIE && !this.m_NKMGame.IsEnemy(this.GetUnitDataGame().m_NKM_TEAM_TYPE, nkmunit.GetUnitDataGame().m_NKM_TEAM_TYPE))
								{
									nkmunit.StateChange(nkmeventChangeState.m_ChangeState, true, false);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x0009E3A0 File Offset: 0x0009C5A0
		protected void ProcessEventBuffUnitDie(NKMBuffData cNKMBuffData, float posX)
		{
			for (int i = 0; i < this.GetUnitTemplet().m_listBuffUnitDieEvent.Count; i++)
			{
				NKMBuffUnitDieEvent nkmbuffUnitDieEvent = this.GetUnitTemplet().m_listBuffUnitDieEvent[i];
				if (nkmbuffUnitDieEvent != null && this.CheckEventCondition(nkmbuffUnitDieEvent.m_Condition) && nkmbuffUnitDieEvent.m_BuffStrID.CompareTo(cNKMBuffData.m_NKMBuffTemplet.m_BuffStrID) == 0 && this.m_NKM_UNIT_CLASS_TYPE == NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
				{
					for (int j = 0; j < nkmbuffUnitDieEvent.m_listNKMEventRespawn.Count; j++)
					{
						NKMEventRespawn nkmeventRespawn = nkmbuffUnitDieEvent.m_listNKMEventRespawn[j];
						if (nkmeventRespawn != null && this.CheckEventCondition(nkmeventRespawn.m_Condition))
						{
							this.ProcessEventRespawn(nkmeventRespawn, posX, 0f);
						}
					}
					for (int k = 0; k < nkmbuffUnitDieEvent.m_listNKMEventCost.Count; k++)
					{
						NKMEventCost nkmeventCost = nkmbuffUnitDieEvent.m_listNKMEventCost[k];
						if (nkmeventCost != null && this.CheckEventCondition(nkmeventCost.m_Condition))
						{
							this.SetCost(nkmeventCost.m_AddCost, nkmeventCost.m_RemoveCost, nkmeventCost.m_CostPerSkillLevel);
						}
					}
					if (nkmbuffUnitDieEvent.m_fSkillCoolTime > 0f && this.GetUnitTemplet().m_listSkillStateData.Count > 0)
					{
						NKMAttackStateData nkmattackStateData = this.GetUnitTemplet().m_listSkillStateData[0];
						if (nkmattackStateData != null)
						{
							NKMUnitState unitState = this.GetUnitState(nkmattackStateData.m_StateName, true);
							if (unitState != null && this.GetStateCoolTime(unitState) > nkmbuffUnitDieEvent.m_fSkillCoolTime)
							{
								this.SetStateCoolTime(unitState, nkmbuffUnitDieEvent.m_fSkillCoolTime);
							}
						}
					}
					if (nkmbuffUnitDieEvent.m_fHyperSkillCoolTime > 0f && this.GetUnitTemplet().m_listHyperSkillStateData.Count > 0)
					{
						NKMAttackStateData nkmattackStateData2 = this.GetUnitTemplet().m_listHyperSkillStateData[0];
						if (nkmattackStateData2 != null)
						{
							NKMUnitState unitState2 = this.GetUnitState(nkmattackStateData2.m_StateName, true);
							if (unitState2 != null && this.GetStateCoolTime(unitState2) > nkmbuffUnitDieEvent.m_fHyperSkillCoolTime)
							{
								this.SetStateCoolTime(unitState2, nkmbuffUnitDieEvent.m_fHyperSkillCoolTime);
							}
						}
					}
					if (!nkmbuffUnitDieEvent.m_fSkillCoolTimeAdd.IsNearlyZero(1E-05f) && this.GetUnitTemplet().m_listSkillStateData.Count > 0)
					{
						NKMAttackStateData nkmattackStateData3 = this.GetUnitTemplet().m_listSkillStateData[0];
						if (nkmattackStateData3 != null)
						{
							NKMUnitState unitState3 = this.GetUnitState(nkmattackStateData3.m_StateName, true);
							if (unitState3 != null)
							{
								this.SetStateCoolTimeAdd(unitState3, nkmbuffUnitDieEvent.m_fSkillCoolTimeAdd);
							}
						}
					}
					if (!nkmbuffUnitDieEvent.m_fHyperSkillCoolTimeAdd.IsNearlyZero(1E-05f) && this.GetUnitTemplet().m_listHyperSkillStateData.Count > 0)
					{
						NKMAttackStateData nkmattackStateData4 = this.GetUnitTemplet().m_listHyperSkillStateData[0];
						if (nkmattackStateData4 != null)
						{
							NKMUnitState unitState4 = this.GetUnitState(nkmattackStateData4.m_StateName, true);
							if (unitState4 != null)
							{
								this.SetStateCoolTimeAdd(unitState4, nkmbuffUnitDieEvent.m_fHyperSkillCoolTimeAdd);
							}
						}
					}
					if (nkmbuffUnitDieEvent.m_fHPRate > 0f)
					{
						float fHeal = this.GetMaxHP(1f) * nkmbuffUnitDieEvent.m_fHPRate;
						this.SetHeal(fHeal, this.GetUnitSyncData().m_GameUnitUID);
					}
					if (nkmbuffUnitDieEvent.m_OutBuffStrID.Length > 1)
					{
						this.AddBuffByStrID(nkmbuffUnitDieEvent.m_OutBuffStrID, nkmbuffUnitDieEvent.m_OutBuffStatLevel, nkmbuffUnitDieEvent.m_OutBuffTimeLevel, this.m_UnitDataGame.m_GameUnitUID, true, false, false, (byte)nkmbuffUnitDieEvent.m_Overlap);
					}
				}
			}
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x0009E6B5 File Offset: 0x0009C8B5
		public void EventDie(bool bImmediate, bool bCheckAllDie = true, bool bUnitChange = false)
		{
			this.GetUnitSyncData().SetHP(0f);
			if (bImmediate)
			{
				this.SetDying(false, bUnitChange);
				this.SetDie(bCheckAllDie);
			}
			this.m_PushSyncData = true;
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x0009E6E4 File Offset: 0x0009C8E4
		protected void ProcessEventAgro(bool bStateEnd = false)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventAgro.Count; i++)
			{
				NKMEventAgro nkmeventAgro = this.m_UnitStateNow.m_listNKMEventAgro[i];
				if (nkmeventAgro != null && this.CheckEventCondition(nkmeventAgro.m_Condition))
				{
					bool flag = false;
					if (nkmeventAgro.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (this.EventTimer(nkmeventAgro.m_bAnimTime, nkmeventAgro.m_fEventTime, true) && !nkmeventAgro.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag)
					{
						this.SetAgro(nkmeventAgro.m_bGetAgro, nkmeventAgro.m_fRange, nkmeventAgro.m_fDurationTime, nkmeventAgro.m_MaxCount);
					}
				}
			}
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x0009E78C File Offset: 0x0009C98C
		protected void ProcessEventHeal(bool bStateEnd = false)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventHeal.Count; i++)
			{
				NKMEventHeal nkmeventHeal = this.m_UnitStateNow.m_listNKMEventHeal[i];
				if (nkmeventHeal != null && this.CheckEventCondition(nkmeventHeal.m_Condition) && (nkmeventHeal.m_bStateEndTime ? bStateEnd : this.EventTimer(nkmeventHeal.m_bAnimTime, nkmeventHeal.m_fEventTime, true)))
				{
					this.SetEventHeal(nkmeventHeal, this.GetUnitSyncData().m_PosX);
				}
			}
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x0009E814 File Offset: 0x0009CA14
		protected void ProcessEventCost(bool bStateEnd = false)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventCost.Count; i++)
			{
				NKMEventCost nkmeventCost = this.m_UnitStateNow.m_listNKMEventCost[i];
				if (nkmeventCost != null && this.CheckEventCondition(nkmeventCost.m_Condition))
				{
					bool flag = false;
					if (nkmeventCost.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (this.EventTimer(nkmeventCost.m_bAnimTime, nkmeventCost.m_fEventTime, true) && !nkmeventCost.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag)
					{
						this.SetCost(nkmeventCost.m_AddCost, nkmeventCost.m_RemoveCost, nkmeventCost.m_CostPerSkillLevel);
					}
				}
			}
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x0009E8B8 File Offset: 0x0009CAB8
		protected void ProcessEventDispel(bool bStateEnd = false)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventDispel.Count; i++)
			{
				NKMEventDispel nkmeventDispel = this.m_UnitStateNow.m_listNKMEventDispel[i];
				if (nkmeventDispel != null && this.CheckEventCondition(nkmeventDispel.m_Condition))
				{
					bool flag = false;
					if (nkmeventDispel.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (this.EventTimer(nkmeventDispel.m_bAnimTime, nkmeventDispel.m_fEventTime, true) && !nkmeventDispel.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag)
					{
						this.SetDispel(nkmeventDispel.m_bDebuff, nkmeventDispel.m_fRangeMin, nkmeventDispel.m_fRangeMax, nkmeventDispel.m_MaxCount, nkmeventDispel.m_bTargetSelf, nkmeventDispel.m_DispelCountPerSkillLevel, nkmeventDispel.m_bDeleteInfinity);
					}
				}
			}
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x0009E978 File Offset: 0x0009CB78
		public float GetModifiedDMGAfterEventDEF(float fAttackPosX, float fOrgDmg)
		{
			if (this.m_UnitStateNow == null)
			{
				return fOrgDmg;
			}
			float num = fOrgDmg;
			float num2 = 0f;
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventDefence.Count; i++)
			{
				NKMEventDefence nkmeventDefence = this.m_UnitStateNow.m_listNKMEventDefence[i];
				if (nkmeventDefence != null && this.CheckEventCondition(nkmeventDefence.m_Condition))
				{
					bool flag = false;
					if (this.EventTimer(nkmeventDefence.m_bAnimTime, nkmeventDefence.m_fEventTimeMin, nkmeventDefence.m_fEventTimeMax))
					{
						if (nkmeventDefence.m_bDefenceBack && nkmeventDefence.m_bDefenceFront)
						{
							flag = true;
						}
						else if (nkmeventDefence.m_bDefenceFront)
						{
							if (this.GetUnitSyncData().m_bRight && this.GetUnitSyncData().m_PosX <= fAttackPosX)
							{
								flag = true;
							}
							else if (!this.GetUnitSyncData().m_bRight && this.GetUnitSyncData().m_PosX >= fAttackPosX)
							{
								flag = true;
							}
						}
						else if (nkmeventDefence.m_bDefenceBack)
						{
							if (this.GetUnitSyncData().m_bRight && this.GetUnitSyncData().m_PosX >= fAttackPosX)
							{
								flag = true;
							}
							else if (!this.GetUnitSyncData().m_bRight && this.GetUnitSyncData().m_PosX <= fAttackPosX)
							{
								flag = true;
							}
						}
					}
					if (flag)
					{
						int skillLevelIfNowSkillState = this.GetSkillLevelIfNowSkillState();
						if (skillLevelIfNowSkillState > 0)
						{
							num2 = (float)(skillLevelIfNowSkillState - 1) * nkmeventDefence.m_fDamageReducePerSkillLevel;
						}
						num -= num * (nkmeventDefence.m_fDamageReduceRate + nkmeventDefence.m_fDamageReduceRate * num2);
					}
				}
			}
			return num;
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x0009EADC File Offset: 0x0009CCDC
		protected void ProcessEventStun(bool bStateEnd = false)
		{
			if (this.m_NKM_UNIT_CLASS_TYPE != NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
			{
				return;
			}
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventStun.Count; i++)
			{
				NKMEventStun nkmeventStun = this.m_UnitStateNow.m_listNKMEventStun[i];
				if (nkmeventStun != null && this.CheckEventCondition(nkmeventStun.m_Condition))
				{
					bool flag = false;
					if (nkmeventStun.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (this.EventTimer(nkmeventStun.m_bAnimTime, nkmeventStun.m_fEventTime, true) && !nkmeventStun.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag)
					{
						this.SetStun(this, nkmeventStun.m_fStunTime, nkmeventStun.m_fRange, nkmeventStun.m_MaxCount, nkmeventStun.m_fStunTimePerSkillLevel, nkmeventStun.m_StunCountPerSkillLevel, nkmeventStun.m_IgnoreStyleType);
					}
				}
			}
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x0009EBA0 File Offset: 0x0009CDA0
		protected void ProcessEventCatch(bool bStateEnd = false)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			if (this.IsDyingOrDie() && this.GetUnitSyncData().m_CatcherGameUnitUID != 0)
			{
				this.GetUnitSyncData().m_CatcherGameUnitUID = 0;
			}
			NKMUnit unit = this.m_NKMGame.GetUnit(this.GetUnitSyncData().m_CatcherGameUnitUID, true, false);
			if ((unit == null || unit.IsDyingOrDie() || unit.GetUnitStateNow().m_NKM_UNIT_STATE_TYPE == NKM_UNIT_STATE_TYPE.NUST_DAMAGE || unit.GetUnitStateNow().m_NKM_UNIT_STATE_TYPE == NKM_UNIT_STATE_TYPE.NUST_DIE || unit.GetUnitStateNow().m_NKM_UNIT_STATE_TYPE == NKM_UNIT_STATE_TYPE.NUST_ASTAND) && this.GetUnitSyncData().m_CatcherGameUnitUID != 0)
			{
				this.GetUnitSyncData().m_CatcherGameUnitUID = 0;
			}
			if (this.m_NKM_UNIT_CLASS_TYPE == NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
			{
				for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventCatchEnd.Count; i++)
				{
					NKMEventCatchEnd nkmeventCatchEnd = this.m_UnitStateNow.m_listNKMEventCatchEnd[i];
					if (nkmeventCatchEnd != null && this.CheckEventCondition(nkmeventCatchEnd.m_Condition))
					{
						bool flag = false;
						if (nkmeventCatchEnd.m_bStateEndTime && bStateEnd)
						{
							flag = true;
						}
						else if (this.EventTimer(nkmeventCatchEnd.m_bAnimTime, nkmeventCatchEnd.m_fEventTime, true) && !nkmeventCatchEnd.m_bStateEndTime)
						{
							flag = true;
						}
						if (flag)
						{
							for (int j = 0; j < this.m_NKMGame.GetUnitChain().Count; j++)
							{
								NKMUnit nkmunit = this.m_NKMGame.GetUnitChain()[j];
								if (nkmunit != null && nkmunit.GetUnitSyncData().m_CatcherGameUnitUID == this.GetUnitSyncData().m_GameUnitUID)
								{
									nkmunit.GetUnitSyncData().m_CatcherGameUnitUID = 0;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x0009ED24 File Offset: 0x0009CF24
		protected void ProcessEventChangeCooltime()
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			if (this.m_NKM_UNIT_CLASS_TYPE != NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventChangeCooltime.Count; i++)
			{
				NKMEventChangeCooltime nkmeventChangeCooltime = this.m_UnitStateNow.m_listNKMEventChangeCooltime[i];
				if (nkmeventChangeCooltime != null && this.EventTimer(nkmeventChangeCooltime.m_bAnimTime, nkmeventChangeCooltime.m_fEventAnimTime, true) && this.CheckEventCondition(nkmeventChangeCooltime.m_Condition))
				{
					NKMEventChangeCooltime.ChangeType eChangeType = nkmeventChangeCooltime.m_eChangeType;
					if (eChangeType != NKMEventChangeCooltime.ChangeType.SET_RATIO)
					{
						if (eChangeType == NKMEventChangeCooltime.ChangeType.ADD_SECONDS)
						{
							this.SetStateCoolTimeAdd(nkmeventChangeCooltime.m_TargetStateName, nkmeventChangeCooltime.m_fChangeValue);
						}
					}
					else
					{
						this.SetStateCoolTime(nkmeventChangeCooltime.m_TargetStateName, true, nkmeventChangeCooltime.m_fChangeValue);
					}
					this.m_PushSyncData = true;
				}
			}
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x0009EDD8 File Offset: 0x0009CFD8
		protected void ProcessStateEvent<T>(List<T> lstEvent, bool bStateEnd = false) where T : INKMUnitStateEventOneTime
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			for (int i = 0; i < lstEvent.Count; i++)
			{
				INKMUnitStateEventOneTime inkmunitStateEventOneTime = lstEvent[i];
				if (inkmunitStateEventOneTime != null && inkmunitStateEventOneTime.bStateEnd == bStateEnd && (bStateEnd || this.EventTimer(inkmunitStateEventOneTime.bAnimTime, inkmunitStateEventOneTime.EventStartTime, true)))
				{
					inkmunitStateEventOneTime.ProcessEvent(this.m_NKMGame, this);
				}
			}
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x0009EE40 File Offset: 0x0009D040
		protected void ProcessAutoShipSkill()
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			if (this.m_NKM_UNIT_CLASS_TYPE != NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
			{
				return;
			}
			if (this.m_NKMGame.GetGameRuntimeData().m_NKM_GAME_STATE != NKM_GAME_STATE.NGS_PLAY)
			{
				return;
			}
			if (this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE != NKM_UNIT_STATE_TYPE.NUST_ASTAND && this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE != NKM_UNIT_STATE_TYPE.NUST_ATTACK && this.m_UnitStateNow.m_NKM_UNIT_STATE_TYPE != NKM_UNIT_STATE_TYPE.NUST_DAMAGE)
			{
				return;
			}
			if (this.m_listShipSkillTemplet.Count <= 0)
			{
				return;
			}
			NKMGameRuntimeTeamData myRuntimeTeamData = this.m_NKMGame.GetGameRuntimeData().GetMyRuntimeTeamData(this.GetUnitDataGame().m_NKM_TEAM_TYPE);
			if (myRuntimeTeamData == null)
			{
				return;
			}
			if (!myRuntimeTeamData.m_bAutoRespawn)
			{
				return;
			}
			this.m_fCheckUseShipSkillAuto -= this.m_DeltaTime;
			if (this.m_fCheckUseShipSkillAuto <= 0f)
			{
				this.m_fCheckUseShipSkillAuto = 0f;
			}
			if (this.m_fCheckUseShipSkillAuto > 0f)
			{
				return;
			}
			for (int i = 0; i < this.m_listShipSkillTemplet.Count; i++)
			{
				NKMShipSkillTemplet nkmshipSkillTemplet = this.m_listShipSkillTemplet[i];
				if (nkmshipSkillTemplet != null && this.CanUseShipSkill(nkmshipSkillTemplet.m_ShipSkillID) == NKM_ERROR_CODE.NEC_OK)
				{
					switch (nkmshipSkillTemplet.m_NKM_SHIP_SKILL_USE_TYPE)
					{
					case NKM_SHIP_SKILL_USE_TYPE.NSSUT_ANY:
						if (this.UseShipSkill(nkmshipSkillTemplet.m_ShipSkillID, this.GetUnitSyncData().m_PosX))
						{
							return;
						}
						break;
					case NKM_SHIP_SKILL_USE_TYPE.NSSUT_ENEMY:
					{
						NKMUnit sortUnit = this.GetSortUnit(true, true, false, 0f, 0f, true, true, true);
						if (sortUnit != null && this.UseShipSkill(nkmshipSkillTemplet.m_ShipSkillID, sortUnit.GetUnitSyncData().m_PosX))
						{
							return;
						}
						break;
					}
					case NKM_SHIP_SKILL_USE_TYPE.NSSUT_MY_TEAM:
					{
						NKMUnit sortUnit2 = this.GetSortUnit(false, false, true, 0f, 0f, true, true, true);
						if (sortUnit2 != null && this.UseShipSkill(nkmshipSkillTemplet.m_ShipSkillID, sortUnit2.GetUnitSyncData().m_PosX))
						{
							return;
						}
						break;
					}
					case NKM_SHIP_SKILL_USE_TYPE.NSSUT_SHIP_ATTACKED:
						if (this.m_UnitFrameData.m_fDamageBeforeFrame > 0f && this.UseShipSkill(nkmshipSkillTemplet.m_ShipSkillID, this.GetUnitSyncData().m_PosX))
						{
							return;
						}
						break;
					}
				}
			}
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x0009F02C File Offset: 0x0009D22C
		protected virtual bool ProcessDangerCharge()
		{
			if (this.m_UnitStateNow == null)
			{
				return false;
			}
			if (this.GetUnitFrameData().m_fDangerChargeTime <= 0f)
			{
				return false;
			}
			if (this.m_UnitStateNow.m_DangerCharge.m_fChargeTime <= 0f)
			{
				return false;
			}
			if (this.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DYING || this.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DIE)
			{
				return false;
			}
			if (this.m_NKMGame.GetGameRuntimeData().m_NKM_GAME_STATE != NKM_GAME_STATE.NGS_PLAY)
			{
				return false;
			}
			if (this.GetUnitFrameData().m_fDangerChargeTime > 0f)
			{
				if (this.m_UnitStateNow.m_DangerCharge.m_fCancelDamageRate > 0f && this.GetUnitFrameData().m_fDangerChargeDamage >= this.GetMaxHP(this.m_UnitStateNow.m_DangerCharge.m_fCancelDamageRate))
				{
					this.StateChange(this.m_UnitStateNow.m_DangerCharge.m_CancelState, true, false);
					return true;
				}
				if (this.m_UnitStateNow.m_DangerCharge.m_CancelHitCount > 0 && this.GetUnitFrameData().m_DangerChargeHitCount >= this.m_UnitStateNow.m_DangerCharge.m_CancelHitCount)
				{
					this.StateChange(this.m_UnitStateNow.m_DangerCharge.m_CancelState, true, false);
					return true;
				}
				if (this.m_NKMGame.GetWorldStopTime() <= 0f)
				{
					this.GetUnitFrameData().m_fDangerChargeTime -= this.m_DeltaTime;
				}
				if (this.GetUnitFrameData().m_fDangerChargeTime < 0f)
				{
					this.GetUnitFrameData().m_fDangerChargeTime = 0f;
					if (this.m_NKM_UNIT_CLASS_TYPE == NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
					{
						this.StateChange(this.m_UnitStateNow.m_DangerCharge.m_SuccessState, true, false);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x0009F1C8 File Offset: 0x0009D3C8
		public NKM_ERROR_CODE CanUseShipSkill(int m_ShipSkillID)
		{
			if (this.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_PLAY || this.GetUnitSyncData().GetHP() <= 0f)
			{
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_SHIP_SKILL_ACK_NO_UNIT;
			}
			NKMShipSkillTemplet shipSkillTempletByID = NKMShipSkillManager.GetShipSkillTempletByID(m_ShipSkillID);
			if (shipSkillTempletByID == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_SHIP_SKILL_ACK_NO_SHIP_SKILL;
			}
			if (shipSkillTempletByID.m_NKM_SKILL_TYPE != NKM_SKILL_TYPE.NST_SHIP_ACTIVE)
			{
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_SHIP_SKILL_ACK_NO_SHIP_ACTIVE_TYPE;
			}
			if (this.GetStateCoolTime(shipSkillTempletByID.m_UnitStateName) > 0f)
			{
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_SHIP_SKILL_ACK_GAME_STATE_COOL_TIME;
			}
			NKMUnitState unitStateNow = this.GetUnitStateNow();
			if (unitStateNow == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_SHIP_SKILL_ACK_NO_UNIT;
			}
			if (unitStateNow.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_HYPER || unitStateNow.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_SKILL)
			{
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_SHIP_SKILL_ACK_NO_ALREADY_USE_SKILL;
			}
			if (this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_SILENCE))
			{
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_SHIP_SKILL_ACK_NO_SILENCE;
			}
			if (this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_SLEEP))
			{
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_SHIP_SKILL_ACK_NO_SLEEP;
			}
			if (this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_FEAR) || this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_FREEZE) || this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_HOLD))
			{
				return NKM_ERROR_CODE.NEC_FAIL_NPT_GAME_SHIP_SKILL_ACK_NO_SILENCE;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x0009F286 File Offset: 0x0009D486
		public float GetEventStateTime(bool bAnim, float eventTime)
		{
			if (!bAnim)
			{
				return eventTime;
			}
			if (this.m_UnitFrameData.m_fAnimSpeed == 0f)
			{
				return 0f;
			}
			return eventTime / this.m_UnitFrameData.m_fAnimSpeed;
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x0009F2B4 File Offset: 0x0009D4B4
		public bool EventTimer(bool bAnim, float fTime, bool bOneTime)
		{
			if (bAnim)
			{
				return this.EventTimer(fTime, bOneTime, this.m_UnitFrameData.m_fAnimTimeBack, this.m_UnitFrameData.m_fAnimTime, this.m_EventTimeStampAnim);
			}
			return this.EventTimer(fTime, bOneTime, this.m_UnitFrameData.m_fStateTimeBack, this.m_UnitFrameData.m_fStateTime, this.m_EventTimeStampState);
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x0009F310 File Offset: 0x0009D510
		private bool EventTimer(float fTimeTarget, bool bOneTime, float fTimeBack, float fTimeNow, Dictionary<float, NKMTimeStamp> dicTimeStamp)
		{
			if ((fTimeTarget > fTimeBack && fTimeTarget <= fTimeNow) || (fTimeTarget.IsNearlyZero(1E-05f) && fTimeNow.IsNearlyZero(1E-05f)))
			{
				if (!bOneTime)
				{
					return true;
				}
				if (!dicTimeStamp.ContainsKey(fTimeTarget))
				{
					NKMTimeStamp nkmtimeStamp = (NKMTimeStamp)this.m_NKMGame.GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKMTimeStamp, "", "", false);
					nkmtimeStamp.m_FramePass = false;
					dicTimeStamp.Add(fTimeTarget, nkmtimeStamp);
					return true;
				}
				if (!dicTimeStamp[fTimeTarget].m_FramePass)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x0009F398 File Offset: 0x0009D598
		public bool EventTimer(bool bAnim, float fTimeMin, float fTimeMax)
		{
			bool flag = false;
			if (bAnim)
			{
				if (this.m_UnitFrameData.m_fAnimTime >= fTimeMin && this.m_UnitFrameData.m_fAnimTime <= fTimeMax)
				{
					flag = true;
				}
			}
			else if (this.m_UnitFrameData.m_fStateTime >= fTimeMin && this.m_UnitFrameData.m_fStateTime <= fTimeMax)
			{
				flag = true;
			}
			if (!flag && this.EventTimer(bAnim, fTimeMin, true))
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x0009F3F9 File Offset: 0x0009D5F9
		public bool RollbackEventTimer(bool bAnim, float eventTime)
		{
			if (bAnim)
			{
				return eventTime < this.m_UnitFrameData.m_fAnimTime;
			}
			return eventTime < this.m_UnitFrameData.m_fStateTime;
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x0009F41C File Offset: 0x0009D61C
		protected void ProcessBuffRange(bool bMasterOK, NKMUnit masterUnit, NKMBuffData cNKMBuffData)
		{
			int num = 0;
			if (bMasterOK && masterUnit != null && masterUnit.GetUnitSyncData().m_GameUnitUID == this.GetUnitSyncData().m_GameUnitUID && cNKMBuffData.m_NKMBuffTemplet.m_Range > 0f && !cNKMBuffData.m_NKMBuffTemplet.m_RangeOverlap)
			{
				List<NKMUnit> unitChain = this.m_NKMGame.GetUnitChain();
				if (unitChain != null)
				{
					for (int i = 0; i < unitChain.Count; i++)
					{
						NKMUnit nkmunit = unitChain[i];
						if (nkmunit != null && this.IsUnitAffectedByBuffRange(nkmunit, bMasterOK, masterUnit, cNKMBuffData))
						{
							num++;
							if (num > cNKMBuffData.m_NKMBuffTemplet.m_RangeSonCount)
							{
								break;
							}
							short num2 = cNKMBuffData.m_BuffSyncData.m_BuffID;
							bool flag = nkmunit.IsAlly(cNKMBuffData.m_BuffSyncData.m_MasterGameUnitUID);
							if (!flag && num2 > 0)
							{
								num2 = -num2;
							}
							if (nkmunit.IsBuffLive(num2))
							{
								NKMBuffData buff = nkmunit.GetBuff(num2, !flag);
								if (cNKMBuffData.m_BuffSyncData.m_OverlapCount == buff.m_BuffSyncData.m_OverlapCount && cNKMBuffData.m_BuffSyncData.m_BuffStatLevel == buff.m_BuffSyncData.m_BuffStatLevel && cNKMBuffData.m_BuffSyncData.m_BuffTimeLevel == buff.m_BuffSyncData.m_BuffTimeLevel)
								{
									goto IL_16D;
								}
							}
							nkmunit.AddBuffByID(cNKMBuffData.m_BuffSyncData.m_BuffID, cNKMBuffData.m_BuffSyncData.m_BuffStatLevel, cNKMBuffData.m_BuffSyncData.m_BuffTimeLevel, cNKMBuffData.m_BuffSyncData.m_MasterGameUnitUID, cNKMBuffData.m_BuffSyncData.m_bUseMasterStat, true, false, 1);
						}
						IL_16D:;
					}
				}
			}
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x0009F5A8 File Offset: 0x0009D7A8
		private bool IsUnitAffectedByBuffRange(NKMUnit cNKMUnit, bool bMasterOK, NKMUnit masterUnit, NKMBuffData cNKMBuffData)
		{
			if (cNKMUnit == null)
			{
				return false;
			}
			if (cNKMUnit.GetUnitSyncData().m_GameUnitUID == this.GetUnitSyncData().m_GameUnitUID)
			{
				return false;
			}
			if (cNKMUnit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_ENV)
			{
				return false;
			}
			if (cNKMUnit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DYING || cNKMUnit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DIE)
			{
				return false;
			}
			if (cNKMBuffData.m_NKMBuffTemplet.m_bRangeSonOnlyTarget && masterUnit.GetUnitSyncData().m_TargetUID != cNKMUnit.GetUnitDataGame().m_GameUnitUID)
			{
				return false;
			}
			if (cNKMBuffData.m_NKMBuffTemplet.m_bRangeSonOnlySubTarget && masterUnit.GetUnitSyncData().m_SubTargetUID != cNKMUnit.GetUnitDataGame().m_GameUnitUID)
			{
				return false;
			}
			bool flag = false;
			if (cNKMBuffData.m_NKMBuffTemplet.m_AffectMasterTeam && !this.m_NKMGame.IsEnemy(this.GetUnitDataGame().m_NKM_TEAM_TYPE, cNKMUnit.GetUnitDataGame().m_NKM_TEAM_TYPE))
			{
				flag = true;
			}
			if (cNKMBuffData.m_NKMBuffTemplet.m_AffectMasterEnemyTeam && this.m_NKMGame.IsEnemy(this.GetUnitDataGame().m_NKM_TEAM_TYPE, cNKMUnit.GetUnitDataGame().m_NKM_TEAM_TYPE))
			{
				flag = true;
			}
			if (!flag)
			{
				return false;
			}
			float num;
			if (!cNKMBuffData.m_NKMBuffTemplet.IsFixedPosBuff())
			{
				num = Math.Abs(cNKMUnit.GetUnitSyncData().m_PosX - this.m_UnitSyncData.m_PosX);
			}
			else
			{
				num = Math.Abs(cNKMUnit.GetUnitSyncData().m_PosX - cNKMBuffData.m_fBuffPosX);
			}
			return cNKMBuffData.m_NKMBuffTemplet.m_Range >= num;
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x0009F724 File Offset: 0x0009D924
		protected void ProcessBuffRangeOverlap(bool bMasterOK, NKMUnit masterUnit, NKMBuffData cNKMBuffData)
		{
			if (!bMasterOK)
			{
				return;
			}
			if (masterUnit == null)
			{
				return;
			}
			if (masterUnit.GetUnitSyncData().m_GameUnitUID != this.GetUnitSyncData().m_GameUnitUID)
			{
				return;
			}
			if (cNKMBuffData.m_NKMBuffTemplet.m_Range <= 0f)
			{
				return;
			}
			if (!cNKMBuffData.m_NKMBuffTemplet.m_RangeOverlap)
			{
				return;
			}
			List<NKMUnit> unitChain = this.m_NKMGame.GetUnitChain();
			if (unitChain == null)
			{
				return;
			}
			int num = 0;
			foreach (NKMUnit nkmunit in unitChain)
			{
				if (this.IsUnitAffectedByBuffRange(nkmunit, bMasterOK, masterUnit, cNKMBuffData) && nkmunit.IsBuffAllowed(cNKMBuffData, true))
				{
					num++;
					if (num >= 255)
					{
						break;
					}
					if (num >= cNKMBuffData.m_NKMBuffTemplet.m_RangeSonCount)
					{
						num = cNKMBuffData.m_NKMBuffTemplet.m_RangeSonCount;
						break;
					}
				}
			}
			if (this.IsBuffLive(cNKMBuffData.m_BuffSyncData.m_BuffID))
			{
				this.SetBuffLevel(cNKMBuffData.m_BuffSyncData.m_BuffID, Convert.ToByte(num + 1), cNKMBuffData.m_BuffSyncData.m_BuffTimeLevel);
			}
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x0009F838 File Offset: 0x0009DA38
		protected bool WillAffectedByBuff(NKMBuffData cNKMBuffData)
		{
			NKMUnit unit = this.m_NKMGame.GetUnit(cNKMBuffData.m_BuffSyncData.m_MasterGameUnitUID, true, true);
			bool bMasterOK = unit != null && unit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_PLAY;
			return this.WillAffectedByBuff(bMasterOK, unit, cNKMBuffData);
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x0009F87C File Offset: 0x0009DA7C
		protected bool WillAffectedByBuff(bool bMasterOK, NKMUnit masterUnit, NKMBuffData cNKMBuffData)
		{
			return cNKMBuffData.m_NKMBuffTemplet.m_AffectMe || (cNKMBuffData.m_NKMBuffTemplet.m_AffectMasterTeam && bMasterOK && masterUnit != null && masterUnit.GetUnitSyncData().m_GameUnitUID != this.GetUnitSyncData().m_GameUnitUID && !this.m_NKMGame.IsEnemy(masterUnit.GetUnitDataGame().m_NKM_TEAM_TYPE, this.GetUnitDataGame().m_NKM_TEAM_TYPE)) || (cNKMBuffData.m_NKMBuffTemplet.m_AffectMasterEnemyTeam && bMasterOK && masterUnit != null && masterUnit.GetUnitSyncData().m_GameUnitUID != this.GetUnitSyncData().m_GameUnitUID && this.m_NKMGame.IsEnemy(masterUnit.GetUnitDataGame().m_NKM_TEAM_TYPE, this.GetUnitDataGame().m_NKM_TEAM_TYPE));
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x0009F93C File Offset: 0x0009DB3C
		protected void ProcessBuffAffect(bool bMasterOK, NKMUnit masterUnit, NKMBuffData cNKMBuffData, bool bBuffDamageOK)
		{
			bool flag = this.WillAffectedByBuff(bMasterOK, masterUnit, cNKMBuffData);
			bool bAffect = cNKMBuffData.m_BuffSyncData.m_bAffect;
			if (flag)
			{
				cNKMBuffData.m_BuffSyncData.m_bAffect = true;
				if (cNKMBuffData.m_NKMBuffTemplet.m_AddAttackUnitCount > 0)
				{
					NKMUnitFrameData unitFrameData = this.GetUnitFrameData();
					unitFrameData.m_AddAttackUnitCount += cNKMBuffData.m_NKMBuffTemplet.m_AddAttackUnitCount;
				}
				if (cNKMBuffData.m_NKMBuffTemplet.m_fAddAttackRange > 0f)
				{
					this.GetUnitFrameData().m_fAddAttackRange += cNKMBuffData.m_NKMBuffTemplet.m_fAddAttackRange;
				}
				if (cNKMBuffData.m_NKMBuffTemplet.m_bNotCastSummon && !this.IsBoss())
				{
					this.GetUnitFrameData().m_bNotCastSummon = cNKMBuffData.m_NKMBuffTemplet.m_bNotCastSummon;
				}
				if (cNKMBuffData.m_NKMBuffTemplet.m_SuperArmorLevel > this.GetUnitFrameData().m_BuffSuperArmorLevel)
				{
					this.GetUnitFrameData().m_BuffSuperArmorLevel = cNKMBuffData.m_NKMBuffTemplet.m_SuperArmorLevel;
				}
				if (cNKMBuffData.m_NKMBuffTemplet.m_fBarrierHP > 0f)
				{
					this.GetUnitFrameData().m_BarrierBuffData = cNKMBuffData;
				}
				if (cNKMBuffData.m_NKMBuffTemplet.m_fDamageTransfer > 0f && cNKMBuffData.m_BuffSyncData.m_bRangeSon)
				{
					this.GetUnitFrameData().m_fDamageTransferGameUnitUID = cNKMBuffData.m_BuffSyncData.m_MasterGameUnitUID;
					this.GetUnitFrameData().m_fDamageTransfer = cNKMBuffData.m_NKMBuffTemplet.m_fDamageTransfer;
				}
				if (cNKMBuffData.m_NKMBuffTemplet.m_bGuard)
				{
					this.GetUnitFrameData().m_GuardGameUnitUID = cNKMBuffData.m_BuffSyncData.m_MasterGameUnitUID;
				}
				if (cNKMBuffData.m_NKMBuffTemplet.m_fDamageReflection > 0f)
				{
					this.GetUnitFrameData().m_fDamageReflection = cNKMBuffData.m_NKMBuffTemplet.m_fDamageReflection;
				}
				if (cNKMBuffData.m_NKMBuffTemplet.m_fHealFeedback > 0f)
				{
					this.GetUnitFrameData().m_fHealFeedback = cNKMBuffData.m_NKMBuffTemplet.m_fHealFeedback + cNKMBuffData.m_NKMBuffTemplet.m_fHealFeedbackPerLevel * (float)(cNKMBuffData.m_BuffSyncData.m_BuffStatLevel - 1);
					this.GetUnitFrameData().m_fHealFeedbackMasterGameUnitUID = cNKMBuffData.m_BuffSyncData.m_MasterGameUnitUID;
				}
				if (cNKMBuffData.m_NKMBuffTemplet.m_UnitLevel != 0)
				{
					this.m_bBuffUnitLevelChangedThisFrame = true;
					this.GetUnitFrameData().m_BuffUnitLevel += cNKMBuffData.m_NKMBuffTemplet.m_UnitLevel;
				}
				if (bBuffDamageOK)
				{
					if (bMasterOK && !this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_BUFF_DAMAGE_IMMUNE) && cNKMBuffData.m_NKMBuffTemplet.m_NKMDamageTemplet != null)
					{
						cNKMBuffData.m_DamageInstBuff.m_DefenderUID = this.GetUnitDataGame().m_GameUnitUID;
						cNKMBuffData.m_DamageInstBuff.m_ReActResult = cNKMBuffData.m_DamageInstBuff.m_Templet.m_ReActType;
						if (!cNKMBuffData.m_NKMBuffTemplet.IsFixedPosBuff())
						{
							cNKMBuffData.m_DamageInstBuff.m_AttackerPosX = this.GetUnitSyncData().m_PosX;
						}
						else
						{
							cNKMBuffData.m_DamageInstBuff.m_AttackerPosX = cNKMBuffData.m_fBuffPosX;
						}
						cNKMBuffData.m_DamageInstBuff.m_AttackerPosZ = this.GetUnitSyncData().m_PosZ;
						cNKMBuffData.m_DamageInstBuff.m_AttackerPosJumpY = this.GetUnitSyncData().m_JumpYPos;
						cNKMBuffData.m_DamageInstBuff.m_bAttackerRight = this.GetUnitSyncData().m_bRight;
						cNKMBuffData.m_DamageInstBuff.m_bAttackerAwaken = false;
						cNKMBuffData.m_DamageInstBuff.m_AttackerAddAttackUnitCount = 0;
						cNKMBuffData.m_DamageInstBuff.m_bEvade = false;
						this.DamageReact(cNKMBuffData.m_DamageInstBuff, true);
						if (cNKMBuffData.m_DamageInstBuff.m_ReActResult == NKM_REACT_TYPE.NRT_NO || cNKMBuffData.m_DamageInstBuff.m_ReActResult == NKM_REACT_TYPE.NRT_INVINCIBLE)
						{
							return;
						}
						if (this.m_NKM_UNIT_CLASS_TYPE == NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER && masterUnit != null)
						{
							NKMStatData statAtk = masterUnit.GetUnitFrameData().m_StatData;
							NKMUnitData unitdataAtk = masterUnit.GetUnitData();
							if (!cNKMBuffData.m_BuffSyncData.m_bUseMasterStat)
							{
								statAtk = null;
								unitdataAtk = null;
							}
							NKM_DAMAGE_RESULT_TYPE eNKM_DAMAGE_RESULT_TYPE = NKM_DAMAGE_RESULT_TYPE.NDRT_NORMAL;
							bool bInstaKill = false;
							float num;
							if (this.m_NKMGame.GetGameData().m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PRACTICE && this.m_NKMGame.GetGameRuntimeData().m_bPracticeFixedDamage)
							{
								num = NKMUnitStatManager.GetAttackFactorDamage(cNKMBuffData.m_NKMBuffTemplet.m_NKMDamageTemplet.m_DamageTempletBase, cNKMBuffData.m_DamageInstBuff.m_AttackerUnitSkillTemplet, this.IsBoss());
							}
							else if (this.WillInstaKilled(cNKMBuffData.m_NKMBuffTemplet.m_NKMDamageTemplet))
							{
								num = this.GetNowHP() * 2f;
								this.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_INSTA_KILL);
								bInstaKill = true;
							}
							else
							{
								num = NKMUnitStatManager.GetFinalDamage(this.m_NKMGame.GetGameData().IsPVP(), statAtk, this.GetUnitFrameData().m_StatData, unitdataAtk, null, this, cNKMBuffData.m_NKMBuffTemplet.m_NKMDamageTemplet, cNKMBuffData.m_DamageInstBuff.m_AttackerUnitSkillTemplet, false, true, false, out eNKM_DAMAGE_RESULT_TYPE, 0f, false, this.IsBoss(), 1f, false, false, false, false);
							}
							num = this.GetModifiedDMGAfterEventDEF(cNKMBuffData.m_DamageInstBuff.m_AttackerPosX, num);
							this.AddDamage(false, num, eNKM_DAMAGE_RESULT_TYPE, masterUnit.GetUnitDataGame().m_GameUnitUID, masterUnit.GetUnitDataGame().m_NKM_TEAM_TYPE, false, false, bInstaKill);
							if (cNKMBuffData.m_NKMBuffTemplet.m_NKMDamageTemplet.m_fCoolTimeDamage > 0f)
							{
								this.AddDamage(false, cNKMBuffData.m_NKMBuffTemplet.m_NKMDamageTemplet.m_fCoolTimeDamage, NKM_DAMAGE_RESULT_TYPE.NDRT_COOL_TIME, masterUnit.GetUnitDataGame().m_GameUnitUID, masterUnit.GetUnitDataGame().m_NKM_TEAM_TYPE, false, false, false);
							}
							this.m_PushSyncData = true;
						}
					}
					if (bMasterOK && cNKMBuffData.m_NKMBuffTemplet.m_NKMEventHeal != null && this.m_NKM_UNIT_CLASS_TYPE == NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER && this.CheckEventCondition(cNKMBuffData.m_NKMBuffTemplet.m_NKMEventHeal.m_Condition) && (!cNKMBuffData.m_BuffSyncData.m_bRangeSon || cNKMBuffData.m_NKMBuffTemplet.m_NKMEventHeal.m_fRangeMin.IsNearlyZero(1E-05f) || cNKMBuffData.m_NKMBuffTemplet.m_NKMEventHeal.m_fRangeMax.IsNearlyZero(1E-05f)))
					{
						if (!cNKMBuffData.m_fBuffPosX.IsNearlyZero(1E-05f))
						{
							this.SetEventHeal(cNKMBuffData.m_NKMBuffTemplet.m_NKMEventHeal, cNKMBuffData.m_fBuffPosX);
						}
						else
						{
							this.SetEventHeal(cNKMBuffData.m_NKMBuffTemplet.m_NKMEventHeal, this.GetUnitSyncData().m_PosX);
						}
					}
				}
				if (this.m_NKM_UNIT_CLASS_TYPE == NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
				{
					float num2 = 0f;
					if (cNKMBuffData.m_NKMBuffTemplet.m_StatType1 == NKM_STAT_TYPE.NST_HP_REGEN_RATE)
					{
						num2 += NKMStatData.GetBuffStatVal(cNKMBuffData.m_NKMBuffTemplet.m_StatType1, cNKMBuffData.m_NKMBuffTemplet.m_StatValue1, cNKMBuffData.m_NKMBuffTemplet.m_StatFactor1, cNKMBuffData.m_NKMBuffTemplet.m_StatAddPerLevel1, cNKMBuffData.m_BuffSyncData.m_BuffStatLevel, cNKMBuffData.m_BuffSyncData.m_OverlapCount);
					}
					if (cNKMBuffData.m_NKMBuffTemplet.m_StatType2 == NKM_STAT_TYPE.NST_HP_REGEN_RATE)
					{
						num2 += NKMStatData.GetBuffStatVal(cNKMBuffData.m_NKMBuffTemplet.m_StatType2, cNKMBuffData.m_NKMBuffTemplet.m_StatValue2, cNKMBuffData.m_NKMBuffTemplet.m_StatFactor2, cNKMBuffData.m_NKMBuffTemplet.m_StatAddPerLevel2, cNKMBuffData.m_BuffSyncData.m_BuffStatLevel, cNKMBuffData.m_BuffSyncData.m_OverlapCount);
					}
					if (cNKMBuffData.m_NKMBuffTemplet.m_StatType3 == NKM_STAT_TYPE.NST_HP_REGEN_RATE)
					{
						num2 += NKMStatData.GetBuffStatVal(cNKMBuffData.m_NKMBuffTemplet.m_StatType3, cNKMBuffData.m_NKMBuffTemplet.m_StatValue3, cNKMBuffData.m_NKMBuffTemplet.m_StatFactor3, cNKMBuffData.m_NKMBuffTemplet.m_StatAddPerLevel3, cNKMBuffData.m_BuffSyncData.m_BuffStatLevel, cNKMBuffData.m_BuffSyncData.m_OverlapCount);
					}
					if (num2 != 0f)
					{
						float num3 = this.RegenHPThisFrame(num2);
						NKMUnit unit = this.m_NKMGame.GetUnit(cNKMBuffData.m_BuffSyncData.m_MasterGameUnitUID, true, true);
						if (num3 < 0f)
						{
							if (unit != null && !unit.IsAlly(this.GetTeam()))
							{
								this.m_NKMGame.m_GameRecord.AddDamage(cNKMBuffData.m_BuffSyncData.m_MasterGameUnitUID, unit.GetTeam(), this, -num3);
							}
						}
						else if (num3 > 0f)
						{
							float num4 = 0f;
							if (unit != null)
							{
								num4 = unit.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HEAL_RATE);
							}
							float num5 = 1f + num4 - this.m_UnitFrameData.m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HEAL_REDUCE_RATE);
							if (num5 < 0f)
							{
								num5 = 0f;
							}
							if (this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_NOHEAL))
							{
								num5 = 0f;
							}
							num3 *= num5;
							this.m_NKMGame.m_GameRecord.AddHeal(cNKMBuffData.m_BuffSyncData.m_MasterGameUnitUID, num3);
						}
					}
				}
			}
			else
			{
				cNKMBuffData.m_BuffSyncData.m_bAffect = false;
			}
			if (bAffect != cNKMBuffData.m_BuffSyncData.m_bAffect)
			{
				this.m_bBuffChangedThisFrame = true;
				if (cNKMBuffData.m_BuffSyncData.m_bAffect)
				{
					this.BuffAffectEffect(cNKMBuffData);
				}
			}
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x000A0148 File Offset: 0x0009E348
		public bool WillInstaKilled(NKMDamageTemplet damageTemplet)
		{
			return damageTemplet != null && this.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_SHIP && !this.IsBoss() && !damageTemplet.m_fInstantKillHPRate.IsNearlyZero(1E-05f) && ((damageTemplet.m_fInstantKillAwaken || !this.GetUnitTemplet().m_UnitTempletBase.m_bAwaken) && damageTemplet.m_fInstantKillHPRate > this.GetHPRate());
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x000A01B8 File Offset: 0x0009E3B8
		protected virtual void BuffAffectEffect(NKMBuffData cNKMBuffData)
		{
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x000A01BC File Offset: 0x0009E3BC
		protected void ProcessBuffDelete(bool bMasterOK, NKMUnit masterUnit, NKMBuffData cNKMBuffData)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (!cNKMBuffData.m_fLifeTime.IsNearlyEqual(-1f, 1E-05f) && !cNKMBuffData.m_BuffSyncData.m_bRangeSon)
			{
				if (cNKMBuffData.m_fLifeTime <= 0f)
				{
					flag = true;
					flag2 = true;
				}
			}
			else if (cNKMBuffData.m_BuffSyncData.m_MasterGameUnitUID != this.m_UnitSyncData.m_GameUnitUID)
			{
				if (bMasterOK && masterUnit != null)
				{
					float num = 0f;
					if (!cNKMBuffData.m_NKMBuffTemplet.IsFixedPosBuff())
					{
						num = Math.Abs(masterUnit.GetUnitSyncData().m_PosX - this.m_UnitSyncData.m_PosX);
					}
					else
					{
						NKMBuffData buff = masterUnit.GetBuff(cNKMBuffData.m_NKMBuffTemplet.m_BuffID, false);
						if (buff != null)
						{
							num = Math.Abs(buff.m_fBuffPosX - this.m_UnitSyncData.m_PosX);
						}
						else
						{
							flag = true;
						}
					}
					if (cNKMBuffData.m_NKMBuffTemplet.m_Range > 0f && cNKMBuffData.m_NKMBuffTemplet.m_Range < num && !cNKMBuffData.m_NKMBuffTemplet.m_RangeOverlap)
					{
						flag = true;
					}
					if (cNKMBuffData.m_NKMBuffTemplet.m_bRangeSonOnlyTarget && masterUnit.GetUnitSyncData().m_TargetUID != this.GetUnitDataGame().m_GameUnitUID)
					{
						flag = true;
					}
					if (cNKMBuffData.m_NKMBuffTemplet.m_bRangeSonOnlySubTarget && masterUnit.GetUnitSyncData().m_SubTargetUID != this.GetUnitDataGame().m_GameUnitUID)
					{
						flag = true;
					}
					bool flag4 = false;
					if (cNKMBuffData.m_NKMBuffTemplet.m_AffectMasterTeam && masterUnit.IsAlly(this.GetTeam()))
					{
						flag4 = true;
					}
					if (cNKMBuffData.m_NKMBuffTemplet.m_AffectMasterEnemyTeam && !masterUnit.IsAlly(this.GetTeam()))
					{
						flag4 = true;
					}
					if (!flag4)
					{
						flag = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			foreach (NKM_UNIT_STATUS_EFFECT status in cNKMBuffData.m_NKMBuffTemplet.m_ApplyStatus)
			{
				if (this.IsImmuneStatus(status) && this.WillAffectedByBuff(cNKMBuffData))
				{
					flag = true;
					break;
				}
			}
			if (!cNKMBuffData.m_fBarrierHP.IsNearlyEqual(-1f, 1E-05f) && cNKMBuffData.m_fBarrierHP <= 0f)
			{
				flag = true;
				flag2 = true;
				if (cNKMBuffData.m_fBarrierHP.IsNearlyEqual(-2f, 1E-05f))
				{
					flag3 = true;
				}
			}
			if (flag)
			{
				this.m_listBuffDelete.Add(cNKMBuffData.m_BuffSyncData.m_BuffID);
			}
			if (flag2)
			{
				if (cNKMBuffData.m_NKMBuffTemplet.m_FinalUnitStateChange.Length > 1)
				{
					this.StateChange(cNKMBuffData.m_NKMBuffTemplet.m_FinalUnitStateChange, true, false);
				}
				if (!flag3 && cNKMBuffData.m_NKMBuffTemplet.m_FinalBuffStrID.Length > 1)
				{
					this.m_listBuffAdd.Add(new NKMBuffCreateData(cNKMBuffData.m_NKMBuffTemplet.m_FinalBuffStrID, cNKMBuffData.m_BuffSyncData.m_BuffStatLevel, cNKMBuffData.m_BuffSyncData.m_BuffTimeLevel, cNKMBuffData.m_BuffSyncData.m_MasterGameUnitUID, true, false, false, 1));
				}
			}
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x000A0498 File Offset: 0x0009E698
		protected virtual void ProcessBuff()
		{
			float statFinal = this.m_UnitFrameData.m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_ATTACK_SPEED_RATE);
			float statFinal2 = this.m_UnitFrameData.m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_MOVE_SPEED_RATE);
			this.GetUnitFrameData().m_bNotCastSummon = false;
			this.GetUnitFrameData().m_fDamageTransferGameUnitUID = 0;
			this.GetUnitFrameData().m_fDamageTransfer = 0f;
			this.GetUnitFrameData().m_GuardGameUnitUID = 0;
			this.GetUnitFrameData().m_fDamageReflection = 0f;
			this.GetUnitFrameData().m_fHealFeedback = 0f;
			this.GetUnitFrameData().m_fHealFeedbackMasterGameUnitUID = 0;
			this.GetUnitFrameData().m_BuffUnitLevel = 0;
			this.GetUnitFrameData().m_AddAttackUnitCount = 0;
			this.GetUnitFrameData().m_fAddAttackRange = 0f;
			this.m_listBuffDelete.Clear();
			this.m_listBuffAdd.Clear();
			bool flag = false;
			this.m_BuffProcessTime -= this.m_DeltaTime;
			if (this.m_BuffProcessTime <= 0f)
			{
				flag = true;
				this.m_BuffProcessTime = 0.3f;
			}
			bool bBuffDamageOK = false;
			this.m_BuffDamageTime -= this.m_DeltaTime;
			if (this.m_BuffDamageTime <= 0f)
			{
				bBuffDamageOK = true;
				this.m_BuffDamageTime = 1f;
			}
			this.m_UnitFrameData.m_BuffSuperArmorLevel = NKM_SUPER_ARMOR_LEVEL.NSAL_NO;
			foreach (KeyValuePair<short, NKMBuffData> keyValuePair in this.m_UnitFrameData.m_dicBuffData)
			{
				NKMBuffData value = keyValuePair.Value;
				if (value != null)
				{
					NKMUnit unit = this.m_NKMGame.GetUnit(value.m_BuffSyncData.m_MasterGameUnitUID, true, false);
					bool bMasterOK = false;
					if (unit != null && unit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_PLAY && unit.IsBuffLive(Math.Abs(value.m_BuffSyncData.m_BuffID)))
					{
						bMasterOK = true;
					}
					if (value.m_NKMBuffTemplet == null || value.m_BuffSyncData.m_BuffID != value.m_NKMBuffTemplet.m_BuffID)
					{
						value.m_NKMBuffTemplet = NKMBuffManager.GetBuffTempletByID(value.m_BuffSyncData.m_BuffID);
					}
					if (flag && this.m_NKM_UNIT_CLASS_TYPE == NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
					{
						this.ProcessBuffRange(bMasterOK, unit, value);
						this.ProcessBuffRangeOverlap(bMasterOK, unit, value);
					}
					this.ProcessBuffAffect(bMasterOK, unit, value, bBuffDamageOK);
					if (!value.m_fLifeTime.IsNearlyEqual(-1f, 1E-05f) && !value.m_NKMBuffTemplet.m_bInfinity && !value.m_BuffSyncData.m_bRangeSon)
					{
						bool flag2 = value.m_NKMBuffTemplet.m_bDebuff;
						if (value.m_BuffSyncData.m_bRangeSon)
						{
							flag2 = value.m_NKMBuffTemplet.m_bDebuffSon;
						}
						if (flag2)
						{
							float num = 1f / (1f - this.m_UnitFrameData.m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_CC_RESIST_RATE));
							value.m_fLifeTime -= this.m_DeltaTime * num;
						}
						else
						{
							value.m_fLifeTime -= this.m_DeltaTime;
						}
						if (value.m_fLifeTime < 0f)
						{
							value.m_fLifeTime = 0f;
						}
					}
					if (this.m_NKM_UNIT_CLASS_TYPE == NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
					{
						this.ProcessBuffDelete(bMasterOK, unit, value);
						if (value.m_BuffSyncData.m_MasterGameUnitUID == this.GetUnitSyncData().m_GameUnitUID && value.m_StateEndRemove && value.m_StateEndCheck)
						{
							this.m_listBuffDelete.Add(value.m_BuffSyncData.m_BuffID);
						}
					}
				}
			}
			if (this.m_listBuffDelete.Count > 0)
			{
				this.m_bBuffChangedThisFrame = true;
			}
			for (int i = 0; i < this.m_listBuffDelete.Count; i++)
			{
				this.DeleteBuff(this.m_listBuffDelete[i], NKMBuffTemplet.BuffEndDTType.End);
			}
			this.m_listBuffDelete.Clear();
			if (this.m_listBuffAdd.Count > 0)
			{
				this.m_bBuffChangedThisFrame = true;
				for (int j = 0; j < this.m_listBuffAdd.Count; j++)
				{
					this.AddBuffByStrID(this.m_listBuffAdd[j].m_buffID, this.m_listBuffAdd[j].m_buffStatLevel, this.m_listBuffAdd[j].m_buffTimeLevel, this.m_listBuffAdd[j].m_masterGameUnitUID, this.m_listBuffAdd[j].m_bUseMasterStat, this.m_listBuffAdd[j].m_bRangeSon, this.m_listBuffAdd[j].m_stateEndRemove, this.m_listBuffAdd[j].m_overlapCount);
				}
				this.m_listBuffAdd.Clear();
			}
			if (this.m_bBuffChangedThisFrame)
			{
				this.CheckAndCalculateBuffStat();
			}
			this.m_bBuffHPRateConserveRequired = false;
			float statFinal3 = this.m_UnitFrameData.m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_ATTACK_SPEED_RATE);
			float statFinal4 = this.m_UnitFrameData.m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_MOVE_SPEED_RATE);
			if (Math.Abs(statFinal3 - statFinal) > 0.01f || Math.Abs(statFinal4 - statFinal2) > 0.01f)
			{
				this.ChangeAnimSpeed(0f);
			}
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x000A09AC File Offset: 0x0009EBAC
		protected void ProcessStatusApply()
		{
			this.GetUnitFrameData().m_hsStatus.Clear();
			this.GetUnitFrameData().m_hsImmuneStatus.Clear();
			this.m_lstTempStatus.Clear();
			this.m_lstTempStatus.AddRange(this.m_UnitFrameData.m_dicStatusTime.Keys);
			foreach (NKM_UNIT_STATUS_EFFECT nkm_UNIT_STATUS_EFFECT in this.m_lstTempStatus)
			{
				if (this.IsImmuneStatus(nkm_UNIT_STATUS_EFFECT))
				{
					this.m_UnitFrameData.m_dicStatusTime.Remove(nkm_UNIT_STATUS_EFFECT);
				}
				else
				{
					float num = this.m_UnitFrameData.m_dicStatusTime[nkm_UNIT_STATUS_EFFECT];
					if (NKMUnitStatusTemplet.IsDebuff(nkm_UNIT_STATUS_EFFECT))
					{
						float num2 = 1f / (1f - this.m_UnitFrameData.m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_CC_RESIST_RATE));
						num -= this.m_DeltaTime * num2;
					}
					else
					{
						num -= this.m_DeltaTime;
					}
					this.m_UnitFrameData.m_dicStatusTime[nkm_UNIT_STATUS_EFFECT] = num;
					if (num <= 0f)
					{
						this.m_UnitFrameData.m_dicStatusTime.Remove(nkm_UNIT_STATUS_EFFECT);
					}
					else
					{
						this.ApplyStatus(nkm_UNIT_STATUS_EFFECT);
					}
				}
			}
			foreach (KeyValuePair<short, NKMBuffData> keyValuePair in this.m_UnitFrameData.m_dicBuffData)
			{
				NKMBuffData value = keyValuePair.Value;
				if (value != null)
				{
					NKMUnit unit = this.m_NKMGame.GetUnit(value.m_BuffSyncData.m_MasterGameUnitUID, true, false);
					bool bMasterOK = false;
					if (unit != null && unit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_PLAY && unit.IsBuffLive(Math.Abs(value.m_BuffSyncData.m_BuffID)))
					{
						bMasterOK = true;
					}
					if (this.WillAffectedByBuff(bMasterOK, unit, value))
					{
						this.ApplyImmuneStatus(value.m_NKMBuffTemplet.m_ImmuneStatus);
						this.ApplyStatus(value.m_NKMBuffTemplet.m_ApplyStatus);
					}
				}
			}
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x000A0BC0 File Offset: 0x0009EDC0
		protected void ProcessStatusAffect()
		{
			if (this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_NULLIFY_BARRIER) && this.GetUnitFrameData().m_BarrierBuffData != null && !this.GetUnitFrameData().m_BarrierBuffData.m_NKMBuffTemplet.m_bNotDispel)
			{
				this.GetUnitFrameData().m_BarrierBuffData.m_fBarrierHP = -2f;
			}
			if (!this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_CONFUSE))
			{
				this.m_UnitDataGame.m_NKM_TEAM_TYPE = this.m_UnitDataGame.m_NKM_TEAM_TYPE_ORG;
			}
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x000A0C30 File Offset: 0x0009EE30
		private void ProcessStaticBuff()
		{
			for (int i = 0; i < this.m_listNKMStaticBuffDataRuntime.Count; i++)
			{
				NKMStaticBuffDataRuntime nkmstaticBuffDataRuntime = this.m_listNKMStaticBuffDataRuntime[i];
				if (nkmstaticBuffDataRuntime != null)
				{
					nkmstaticBuffDataRuntime.m_fReBuffTimeNow -= this.m_DeltaTime;
					if (nkmstaticBuffDataRuntime.m_fReBuffTimeNow <= 0f)
					{
						this.AddStaticBuffUnit(nkmstaticBuffDataRuntime.m_NKMStaticBuffData);
						nkmstaticBuffDataRuntime.m_fReBuffTimeNow = nkmstaticBuffDataRuntime.m_NKMStaticBuffData.m_fRebuffTime;
					}
				}
			}
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x000A0CA0 File Offset: 0x0009EEA0
		private void ProcessLeaguePvpRageBuff()
		{
			if (this.m_NKMGame.GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_PVP_LEAGUE)
			{
				return;
			}
			bool isUnit = this.m_UnitTemplet.m_UnitTempletBase.IsUnitStyleType();
			NKMPvpCommonConst.LeaguePvpConst leaguePvp = NKMPvpCommonConst.Instance.LeaguePvp;
			NKMBuffTemplet rageBuff = leaguePvp.GetRageBuff(isUnit);
			if (rageBuff == null)
			{
				return;
			}
			if (this.GetBuff(rageBuff.m_BuffID, false) != null)
			{
				return;
			}
			NKMUnitData mainShip = this.m_NKMGame.GetGameData().GetTeamData(this.GetTeam()).m_MainShip;
			if (mainShip == null)
			{
				return;
			}
			NKMUnit unit = this.m_NKMGame.GetUnit(mainShip.m_listGameUnitUID[0], true, false);
			float maxHP = unit.GetMaxHP(1f);
			float num = maxHP * leaguePvp.RageBuffShipHpRate;
			float hp = unit.GetUnitSyncData().GetHP();
			if (hp > num)
			{
				return;
			}
			float num2 = hp * 100f / maxHP;
			long gameUID = this.m_NKMGame.GetGameData().m_GameUID;
			Log.Debug(string.Format("[NKMUnit] start rage buff. gameUid:{0} buffId:{1} shipHp:{2}({3:0.00}%) teamType:{4}", new object[]
			{
				gameUID,
				rageBuff.m_BuffStrID,
				hp,
				num2,
				this.GetTeam()
			}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnit.cs", 7892);
			this.AddBuffByID(rageBuff.m_BuffID, 1, 1, this.GetUnitDataGame().m_GameUnitUID, true, false, false, 1);
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x000A0DF0 File Offset: 0x0009EFF0
		private void ProcessLeaguePvpDeadlineBuff()
		{
			if (this.m_NKMGame.GetGameData().GetGameType() != NKM_GAME_TYPE.NGT_PVP_LEAGUE)
			{
				return;
			}
			bool isUnit = this.m_UnitTemplet.m_UnitTempletBase.IsUnitStyleType();
			NKMPvpCommonConst.LeaguePvpConst leaguePvp = NKMPvpCommonConst.Instance.LeaguePvp;
			NKMBuffTemplet deadlineBuff = leaguePvp.GetDeadlineBuff(isUnit);
			if (deadlineBuff == null)
			{
				return;
			}
			float fRemainGameTime = this.m_NKMGame.GetGameRuntimeData().m_fRemainGameTime;
			float deadlineBuffConditionTimeMax = leaguePvp.GetDeadlineBuffConditionTimeMax();
			if (fRemainGameTime >= deadlineBuffConditionTimeMax)
			{
				return;
			}
			NKMPvpCommonConst.LeaguePvpConst.DeadlineBuffCondition deadlineBuffCondition;
			if (!leaguePvp.GetDeadlineBuffCondition(fRemainGameTime, out deadlineBuffCondition))
			{
				Log.Warn(string.Format("[NKMUnit] leaguePvp.deadlineBuff condition access failed. remainTime:{0}", fRemainGameTime), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnit.cs", 7929);
				return;
			}
			NKMBuffData buff = this.GetBuff(deadlineBuff.m_BuffID, false);
			if (buff != null && (int)buff.m_BuffSyncData.m_BuffStatLevel == deadlineBuffCondition.BuffLevel)
			{
				return;
			}
			long gameUID = this.m_NKMGame.GetGameData().m_GameUID;
			short gameUnitUID = this.GetUnitDataGame().m_GameUnitUID;
			Log.Debug(string.Format("[NKMUnit] start deadline buff. gameUid:{0} gameUnitUid:{1} buffId:{2} remainTime:{3} buffLevel:{4}", new object[]
			{
				gameUID,
				gameUnitUID,
				deadlineBuff.m_BuffStrID,
				fRemainGameTime,
				deadlineBuffCondition.BuffLevel
			}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnit.cs", 7941);
			this.AddBuffByID(deadlineBuff.m_BuffID, (byte)deadlineBuffCondition.BuffLevel, 1, gameUnitUID, true, false, false, 1);
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x000A0F3C File Offset: 0x0009F13C
		public short AddBuffByStrID(string buffStrID, byte buffLevel, byte buffTimeLevel, short masterGameUnitUID, bool bUseMasterStat, bool bRangeSon, bool bStateEndRemove = false, byte overlapCount = 1)
		{
			NKMBuffTemplet buffTempletByStrID = NKMBuffManager.GetBuffTempletByStrID(buffStrID);
			if (buffTempletByStrID == null)
			{
				return 0;
			}
			return this.AddBuffByID(buffTempletByStrID.m_BuffID, buffLevel, buffTimeLevel, masterGameUnitUID, bUseMasterStat, bRangeSon, bStateEndRemove, overlapCount);
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x000A0F70 File Offset: 0x0009F170
		public bool ExistDispelBuff(bool bDebuff = true)
		{
			foreach (KeyValuePair<short, NKMBuffData> keyValuePair in this.m_UnitFrameData.m_dicBuffData)
			{
				if (keyValuePair.Value != null && keyValuePair.Value.m_NKMBuffTemplet != null)
				{
					if (!bDebuff && keyValuePair.Value.m_NKMBuffTemplet.m_bDispelBuff)
					{
						return true;
					}
					if (!bDebuff && keyValuePair.Value.m_NKMBuffTemplet.m_bRangeSonDispelBuff && keyValuePair.Value.m_BuffSyncData.m_bRangeSon)
					{
						return true;
					}
					if (bDebuff && keyValuePair.Value.m_NKMBuffTemplet.m_bDispelDebuff)
					{
						return true;
					}
					if (bDebuff && keyValuePair.Value.m_NKMBuffTemplet.m_bRangeSonDispelDebuff && keyValuePair.Value.m_BuffSyncData.m_bRangeSon)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x000A1078 File Offset: 0x0009F278
		public virtual short AddBuffByID(short buffID, byte buffLevel, byte buffTimeLevel, short masterGameUnitUID, bool bUseMasterStat, bool bRangeSon, bool bStateEndRemove, byte overlapCount)
		{
			if (this.m_UnitDataGame.m_NKM_TEAM_TYPE != this.m_UnitDataGame.m_NKM_TEAM_TYPE_ORG)
			{
				bool flag = true;
				NKMBuffTemplet buffTempletByID = NKMBuffManager.GetBuffTempletByID(buffID);
				if (buffTempletByID != null && buffTempletByID.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_CONFUSE))
				{
					flag = false;
				}
				if (flag)
				{
					return 0;
				}
			}
			NKMBuffData nkmbuffData = (NKMBuffData)this.m_NKMGame.GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKMBuffData, "", "", false);
			if (nkmbuffData == null)
			{
				return 0;
			}
			NKMUnit unit = this.m_NKMGame.GetUnit(masterGameUnitUID, true, true);
			if (unit != null && !this.IsAlly(unit) && buffID > 0)
			{
				buffID = -buffID;
			}
			if (this.GetUnitFrameData().IsNoReuseBuff(buffID))
			{
				this.m_NKMGame.GetObjectPool().CloseObj(nkmbuffData);
				return 0;
			}
			NKMBuffData buff = this.GetBuff(buffID, false);
			if (buff != null && bRangeSon && buff.m_BuffSyncData.m_bRangeSon && buff.m_BuffSyncData.m_BuffStatLevel == nkmbuffData.m_BuffSyncData.m_BuffStatLevel && buff.m_BuffSyncData.m_BuffTimeLevel == nkmbuffData.m_BuffSyncData.m_BuffTimeLevel)
			{
				this.m_NKMGame.GetObjectPool().CloseObj(nkmbuffData);
				return 0;
			}
			nkmbuffData.m_BuffSyncData.m_BuffID = buffID;
			nkmbuffData.m_BuffSyncData.m_BuffStatLevel = buffLevel;
			nkmbuffData.m_BuffSyncData.m_BuffTimeLevel = buffTimeLevel;
			nkmbuffData.m_BuffSyncData.m_bNew = true;
			nkmbuffData.m_BuffSyncData.m_bRangeSon = bRangeSon;
			nkmbuffData.m_BuffSyncData.m_MasterGameUnitUID = masterGameUnitUID;
			nkmbuffData.m_BuffSyncData.m_bUseMasterStat = bUseMasterStat;
			nkmbuffData.m_NKMBuffTemplet = NKMBuffManager.GetBuffTempletByID(buffID);
			if (nkmbuffData.m_NKMBuffTemplet == null)
			{
				this.m_NKMGame.GetObjectPool().CloseObj(nkmbuffData);
				return 0;
			}
			NKMBuffTemplet nkmbuffTemplet = nkmbuffData.m_NKMBuffTemplet;
			if (nkmbuffTemplet.m_RangeOverlap)
			{
				nkmbuffData.m_BuffSyncData.m_BuffStatLevel = 1;
				nkmbuffData.m_BuffSyncData.m_BuffTimeLevel = 1;
			}
			bool flag2 = nkmbuffTemplet.m_bDebuff;
			if (nkmbuffData.m_BuffSyncData.m_bRangeSon)
			{
				flag2 = nkmbuffTemplet.m_bDebuffSon;
			}
			nkmbuffData.m_StateEndRemove = bStateEndRemove;
			bool bMasterOK = unit != null && unit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_PLAY;
			bool flag3 = this.WillAffectedByBuff(bMasterOK, unit, nkmbuffData);
			if (flag3)
			{
				if (this.ExistDispelBuff(flag2) && !nkmbuffTemplet.m_bInfinity && !nkmbuffTemplet.m_bNotDispel)
				{
					this.m_NKMGame.GetObjectPool().CloseObj(nkmbuffData);
					return 0;
				}
				foreach (NKM_UNIT_STATUS_EFFECT status in nkmbuffTemplet.m_ApplyStatus)
				{
					if (this.IsImmuneStatus(status))
					{
						this.m_NKMGame.GetObjectPool().CloseObj(nkmbuffData);
						return 0;
					}
				}
				if (nkmbuffTemplet.m_bIgnoreBlock || (!this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_BLOCK_BUFF) && !this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_BLOCK_DEBUFF)))
				{
					goto IL_2DA;
				}
				if (!flag2 && this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_BLOCK_BUFF))
				{
					this.m_NKMGame.GetObjectPool().CloseObj(nkmbuffData);
					return 0;
				}
				if (flag2 && this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_BLOCK_DEBUFF))
				{
					this.m_NKMGame.GetObjectPool().CloseObj(nkmbuffData);
					return 0;
				}
			}
			IL_2DA:
			if (this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_NULLIFY_BARRIER) && nkmbuffTemplet.IsBarrierBuff && !nkmbuffTemplet.m_bNotDispel)
			{
				this.m_NKMGame.GetObjectPool().CloseObj(nkmbuffData);
				return 0;
			}
			if (buff != null && nkmbuffTemplet.m_bNoRefresh)
			{
				this.m_NKMGame.GetObjectPool().CloseObj(nkmbuffData);
				return 0;
			}
			if (buff != null)
			{
				nkmbuffData.m_BuffSyncData.m_OverlapCount = buff.m_BuffSyncData.m_OverlapCount;
				NKMBuffSyncData buffSyncData = nkmbuffData.m_BuffSyncData;
				buffSyncData.m_OverlapCount += overlapCount;
				if (nkmbuffData.m_BuffSyncData.m_OverlapCount > nkmbuffTemplet.m_MaxOverlapCount)
				{
					nkmbuffData.m_BuffSyncData.m_OverlapCount = nkmbuffTemplet.m_MaxOverlapCount;
				}
				if (nkmbuffData.m_BuffSyncData.m_OverlapCount <= 0)
				{
					this.DeleteBuff(buffID, NKMBuffTemplet.BuffEndDTType.NoUse);
					this.m_NKMGame.GetObjectPool().CloseObj(nkmbuffData);
					return 0;
				}
			}
			else
			{
				if (overlapCount <= 0)
				{
					this.m_NKMGame.GetObjectPool().CloseObj(nkmbuffData);
					return 0;
				}
				nkmbuffData.m_BuffSyncData.m_OverlapCount = overlapCount;
			}
			if (nkmbuffData.m_BuffSyncData.m_OverlapCount >= nkmbuffTemplet.m_MaxOverlapCount)
			{
				nkmbuffData.m_BuffSyncData.m_OverlapCount = nkmbuffTemplet.m_MaxOverlapCount;
				if (!string.IsNullOrEmpty(nkmbuffTemplet.m_MaxOverlapBuffStrID))
				{
					short num = this.AddBuffByStrID(nkmbuffTemplet.m_MaxOverlapBuffStrID, buffLevel, buffTimeLevel, masterGameUnitUID, bUseMasterStat, false, false, 1);
					if (num != 0)
					{
						this.DeleteBuff(buffID, NKMBuffTemplet.BuffEndDTType.NoUse);
						this.m_NKMGame.GetObjectPool().CloseObj(nkmbuffData);
						return num;
					}
				}
			}
			if (!this.IsBuffAllowed(nkmbuffData, bRangeSon))
			{
				this.m_NKMGame.GetObjectPool().CloseObj(nkmbuffData);
				return 0;
			}
			if (!this.IsDyingOrDie())
			{
				string badStatusStateChange = this.GetBadStatusStateChange(nkmbuffTemplet.m_ApplyStatus);
				if (!string.IsNullOrEmpty(badStatusStateChange))
				{
					this.StateChange(badStatusStateChange, true, false);
					this.m_NKMGame.GetObjectPool().CloseObj(nkmbuffData);
					return 0;
				}
			}
			if (!this.OnApplyStatus(nkmbuffTemplet.m_ApplyStatus, unit))
			{
				this.m_NKMGame.GetObjectPool().CloseObj(nkmbuffData);
				return 0;
			}
			if (nkmbuffTemplet.IsFixedPosBuff() && masterGameUnitUID == this.GetUnitDataGame().m_GameUnitUID)
			{
				float num2;
				if (nkmbuffTemplet.m_bShipSkillPos)
				{
					num2 = this.m_UnitFrameData.m_fShipSkillPosX;
				}
				else
				{
					num2 = this.GetUnitSyncData().m_PosX;
				}
				if (this.GetUnitSyncData().m_bRight)
				{
					num2 += nkmbuffTemplet.m_fOffsetX;
				}
				else
				{
					num2 -= nkmbuffTemplet.m_fOffsetX;
				}
				nkmbuffData.m_fBuffPosX = num2;
			}
			if (nkmbuffTemplet.m_NKMDamageTemplet != null)
			{
				nkmbuffData.m_DamageInstBuff.m_Templet = nkmbuffTemplet.m_NKMDamageTemplet;
				nkmbuffData.m_DamageInstBuff.m_AttackerType = NKM_REACTOR_TYPE.NRT_GAME_UNIT;
				nkmbuffData.m_DamageInstBuff.m_AttackerGameUnitUID = nkmbuffData.m_BuffSyncData.m_MasterGameUnitUID;
				if (unit != null)
				{
					nkmbuffData.m_DamageInstBuff.m_AttackerTeamType = unit.GetUnitDataGame().m_NKM_TEAM_TYPE;
				}
				nkmbuffData.m_DamageInstBuff.m_AttackerUnitSkillTemplet = null;
			}
			nkmbuffData.m_fLifeTime = nkmbuffData.GetLifeTimeMax();
			nkmbuffData.m_fBarrierHP = nkmbuffTemplet.GetBarrierHPMax(this.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP), this.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_BARRIER_REINFORCE_RATE), (int)nkmbuffData.m_BuffSyncData.m_BuffStatLevel);
			if (nkmbuffTemplet.m_fOneTimeHPDamageRate > 0f)
			{
				float fDamage = this.m_UnitSyncData.GetHP() * nkmbuffTemplet.m_fOneTimeHPDamageRate;
				NKM_TEAM_TYPE teamType = (unit != null) ? unit.GetUnitDataGame().m_NKM_TEAM_TYPE : NKM_TEAM_TYPE.NTT_INVALID;
				this.AddDamage(false, fDamage, NKM_DAMAGE_RESULT_TYPE.NDRT_NORMAL, nkmbuffData.m_BuffSyncData.m_MasterGameUnitUID, teamType, true, false, false);
			}
			if (flag3 && nkmbuffTemplet.m_DTStart != null)
			{
				this.m_NKMGame.ProcessDamageTemplet(nkmbuffTemplet.m_DTStart, unit, this, true, true, nkmbuffData.m_DamageInstBuff.m_AttackerUnitSkillTemplet, null);
			}
			this.DeleteBuff(buffID, NKMBuffTemplet.BuffEndDTType.NoUse);
			this.m_UnitFrameData.m_dicBuffData.Add(nkmbuffData.m_BuffSyncData.m_BuffID, nkmbuffData);
			this.m_UnitSyncData.m_dicBuffData.Add(nkmbuffData.m_BuffSyncData.m_BuffID, nkmbuffData.m_BuffSyncData);
			if (nkmbuffTemplet != null)
			{
				bool flag4 = false;
				if (nkmbuffTemplet.m_bDispelBuff)
				{
					this.DispelBuff(false, false);
					this.DispelStatusTime(false, 999);
					flag4 = true;
				}
				if (nkmbuffTemplet.m_bRangeSonDispelBuff && nkmbuffData.m_BuffSyncData.m_bRangeSon)
				{
					this.DispelBuff(false, false);
					this.DispelStatusTime(false, 999);
					flag4 = true;
				}
				if (nkmbuffTemplet.m_bDispelDebuff)
				{
					this.DispelBuff(true, false);
					this.DispelStatusTime(true, 999);
					flag4 = true;
				}
				if (nkmbuffTemplet.m_bRangeSonDispelDebuff && nkmbuffData.m_BuffSyncData.m_bRangeSon)
				{
					this.DispelBuff(true, false);
					this.DispelStatusTime(true, 999);
					flag4 = true;
				}
				if (flag4)
				{
					this.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_DISPEL);
				}
			}
			foreach (NKM_UNIT_STATUS_EFFECT status2 in nkmbuffTemplet.m_ImmuneStatus)
			{
				this.ApplyImmuneStatus(status2);
			}
			this.m_bBuffChangedThisFrame = true;
			this.m_bPushSimpleSyncData = true;
			if (nkmbuffTemplet != null)
			{
				if (nkmbuffTemplet.m_UnitLevel != 0)
				{
					this.m_bBuffUnitLevelChangedThisFrame = true;
				}
				if (nkmbuffTemplet.m_bNoReuse)
				{
					this.GetUnitFrameData().AddNoReuseBuff(buffID);
				}
			}
			return buffID;
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x000A1844 File Offset: 0x0009FA44
		private bool IsBuffAllowed(NKMBuffData cNKMBuffData, bool bRangeSon)
		{
			if (cNKMBuffData.m_NKMBuffTemplet.m_AffectCostRange.m_Min >= 0 || cNKMBuffData.m_NKMBuffTemplet.m_AffectCostRange.m_Max >= 0)
			{
				int respawnCost = this.m_NKMGame.GetRespawnCost(this.GetUnitTemplet().m_UnitTempletBase.StatTemplet, false, this.GetUnitDataGame().m_NKM_TEAM_TYPE);
				if (respawnCost < cNKMBuffData.m_NKMBuffTemplet.m_AffectCostRange.m_Min)
				{
					return false;
				}
				if (respawnCost > cNKMBuffData.m_NKMBuffTemplet.m_AffectCostRange.m_Max)
				{
					return false;
				}
			}
			switch (cNKMBuffData.m_NKMBuffTemplet.m_eAffectSummonType)
			{
			case NKMBuffTemplet.AffectSummonType.SummonOnly:
				if (!this.IsSummonUnit())
				{
					return false;
				}
				break;
			case NKMBuffTemplet.AffectSummonType.SummonNo:
				if (this.IsSummonUnit())
				{
					return false;
				}
				break;
			}
			if (!bRangeSon)
			{
				if (!cNKMBuffData.m_NKMBuffTemplet.m_bAllowBoss && this.IsBoss())
				{
					return false;
				}
				if (cNKMBuffData.m_NKMBuffTemplet.m_listAllowUnitID.Count > 0)
				{
					bool flag = false;
					for (int i = 0; i < cNKMBuffData.m_NKMBuffTemplet.m_listAllowUnitID.Count; i++)
					{
						if (cNKMBuffData.m_NKMBuffTemplet.m_listAllowUnitID[i] == this.GetUnitTemplet().m_UnitTempletBase.m_UnitID)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return false;
					}
				}
				if (cNKMBuffData.m_NKMBuffTemplet.m_listIgnoreUnitID.Count > 0)
				{
					for (int j = 0; j < cNKMBuffData.m_NKMBuffTemplet.m_listIgnoreUnitID.Count; j++)
					{
						if (cNKMBuffData.m_NKMBuffTemplet.m_listIgnoreUnitID[j] == this.GetUnitTemplet().m_UnitTempletBase.m_UnitID)
						{
							return false;
						}
					}
				}
				if (cNKMBuffData.m_NKMBuffTemplet.m_listAllowStyleType.Count > 0)
				{
					bool flag2 = false;
					for (int k = 0; k < cNKMBuffData.m_NKMBuffTemplet.m_listAllowStyleType.Count; k++)
					{
						if (this.GetUnitTemplet().m_UnitTempletBase.HasUnitStyleType(cNKMBuffData.m_NKMBuffTemplet.m_listAllowStyleType[k]))
						{
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						return false;
					}
				}
				if (cNKMBuffData.m_NKMBuffTemplet.m_listAllowRoleType.Count > 0)
				{
					bool flag3 = false;
					for (int l = 0; l < cNKMBuffData.m_NKMBuffTemplet.m_listAllowRoleType.Count; l++)
					{
						if (cNKMBuffData.m_NKMBuffTemplet.m_listAllowRoleType[l] == this.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE)
						{
							flag3 = true;
							break;
						}
					}
					if (!flag3)
					{
						return false;
					}
				}
				if (cNKMBuffData.m_NKMBuffTemplet.m_listIgnoreStyleType.Count > 0)
				{
					bool flag4 = false;
					for (int m = 0; m < cNKMBuffData.m_NKMBuffTemplet.m_listIgnoreStyleType.Count; m++)
					{
						if (this.GetUnitTemplet().m_UnitTempletBase.HasUnitStyleType(cNKMBuffData.m_NKMBuffTemplet.m_listIgnoreStyleType[m]))
						{
							flag4 = true;
							break;
						}
					}
					if (flag4)
					{
						return false;
					}
				}
				if (cNKMBuffData.m_NKMBuffTemplet.m_listIgnoreRoleType.Count > 0)
				{
					bool flag5 = false;
					for (int n = 0; n < cNKMBuffData.m_NKMBuffTemplet.m_listIgnoreRoleType.Count; n++)
					{
						if (cNKMBuffData.m_NKMBuffTemplet.m_listIgnoreRoleType[n] == this.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE)
						{
							flag5 = true;
							break;
						}
					}
					if (flag5)
					{
						return false;
					}
				}
				if (!cNKMBuffData.m_NKMBuffTemplet.m_bAllowAirUnit && this.IsAirUnit())
				{
					return false;
				}
				if (cNKMBuffData.m_NKMBuffTemplet.m_AffectMultiRespawnMinCount > 0 && this.GetUnitTemplet().m_UnitTempletBase.StatTemplet.m_RespawnCount < cNKMBuffData.m_NKMBuffTemplet.m_AffectMultiRespawnMinCount)
				{
					return false;
				}
				if (!cNKMBuffData.m_NKMBuffTemplet.m_bAllowLandUnit && !this.IsAirUnit())
				{
					return false;
				}
				if (!cNKMBuffData.m_NKMBuffTemplet.m_bAllowAwaken && this.GetUnitTemplet().m_UnitTempletBase.m_bAwaken)
				{
					return false;
				}
				if (!cNKMBuffData.m_NKMBuffTemplet.m_bAllowNormal && !this.GetUnitTemplet().m_UnitTempletBase.m_bAwaken)
				{
					return false;
				}
			}
			else
			{
				if (!cNKMBuffData.m_NKMBuffTemplet.m_bRangeSonAllowBoss && this.IsBoss())
				{
					return false;
				}
				if (cNKMBuffData.m_NKMBuffTemplet.m_bRangeSonOnlyTarget || cNKMBuffData.m_NKMBuffTemplet.m_bRangeSonOnlySubTarget)
				{
					NKMUnit unit = this.m_NKMGame.GetUnit(cNKMBuffData.m_BuffSyncData.m_MasterGameUnitUID, true, true);
					if (unit == null)
					{
						return false;
					}
					if (cNKMBuffData.m_NKMBuffTemplet.m_bRangeSonOnlyTarget && unit.GetUnitSyncData().m_TargetUID != this.GetUnitDataGame().m_GameUnitUID)
					{
						return false;
					}
					if (cNKMBuffData.m_NKMBuffTemplet.m_bRangeSonOnlySubTarget && unit.GetUnitSyncData().m_SubTargetUID != this.GetUnitDataGame().m_GameUnitUID)
					{
						return false;
					}
				}
				if (cNKMBuffData.m_NKMBuffTemplet.m_listRangeSonAllowUnitID.Count > 0)
				{
					bool flag6 = false;
					for (int num = 0; num < cNKMBuffData.m_NKMBuffTemplet.m_listRangeSonAllowUnitID.Count; num++)
					{
						if (cNKMBuffData.m_NKMBuffTemplet.m_listRangeSonAllowUnitID[num] == this.GetUnitTemplet().m_UnitTempletBase.m_UnitID)
						{
							flag6 = true;
							break;
						}
					}
					if (!flag6)
					{
						return false;
					}
				}
				if (cNKMBuffData.m_NKMBuffTemplet.m_listRangeSonIgnoreUnitID.Count > 0)
				{
					for (int num2 = 0; num2 < cNKMBuffData.m_NKMBuffTemplet.m_listRangeSonIgnoreUnitID.Count; num2++)
					{
						if (cNKMBuffData.m_NKMBuffTemplet.m_listRangeSonIgnoreUnitID[num2] == this.GetUnitTemplet().m_UnitTempletBase.m_UnitID)
						{
							return false;
						}
					}
				}
				if (cNKMBuffData.m_NKMBuffTemplet.m_listRangeSonAllowStyleType.Count > 0)
				{
					bool flag7 = false;
					for (int num3 = 0; num3 < cNKMBuffData.m_NKMBuffTemplet.m_listRangeSonAllowStyleType.Count; num3++)
					{
						if (this.GetUnitTemplet().m_UnitTempletBase.HasUnitStyleType(cNKMBuffData.m_NKMBuffTemplet.m_listRangeSonAllowStyleType[num3]))
						{
							flag7 = true;
							break;
						}
					}
					if (!flag7)
					{
						return false;
					}
				}
				if (cNKMBuffData.m_NKMBuffTemplet.m_listRangeSonAllowRoleType.Count > 0)
				{
					bool flag8 = false;
					for (int num4 = 0; num4 < cNKMBuffData.m_NKMBuffTemplet.m_listRangeSonAllowRoleType.Count; num4++)
					{
						if (cNKMBuffData.m_NKMBuffTemplet.m_listRangeSonAllowRoleType[num4] == this.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE)
						{
							flag8 = true;
							break;
						}
					}
					if (!flag8)
					{
						return false;
					}
				}
				if (cNKMBuffData.m_NKMBuffTemplet.m_listRangeSonIgnoreStyleType.Count > 0)
				{
					bool flag9 = false;
					for (int num5 = 0; num5 < cNKMBuffData.m_NKMBuffTemplet.m_listRangeSonIgnoreStyleType.Count; num5++)
					{
						if (this.GetUnitTemplet().m_UnitTempletBase.HasUnitStyleType(cNKMBuffData.m_NKMBuffTemplet.m_listRangeSonIgnoreStyleType[num5]))
						{
							flag9 = true;
							break;
						}
					}
					if (flag9)
					{
						return false;
					}
				}
				if (cNKMBuffData.m_NKMBuffTemplet.m_listRangeSonIgnoreRoleType.Count > 0)
				{
					bool flag10 = false;
					for (int num6 = 0; num6 < cNKMBuffData.m_NKMBuffTemplet.m_listRangeSonIgnoreRoleType.Count; num6++)
					{
						if (cNKMBuffData.m_NKMBuffTemplet.m_listRangeSonIgnoreRoleType[num6] == this.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE)
						{
							flag10 = true;
							break;
						}
					}
					if (flag10)
					{
						return false;
					}
				}
				if (cNKMBuffData.m_NKMBuffTemplet.m_AffectSonMultiRespawnMinCount > 0 && this.GetUnitTemplet().m_UnitTempletBase.StatTemplet.m_RespawnCount < cNKMBuffData.m_NKMBuffTemplet.m_AffectSonMultiRespawnMinCount)
				{
					return false;
				}
				if (!cNKMBuffData.m_NKMBuffTemplet.m_bRangeSonAllowAirUnit && this.IsAirUnit())
				{
					return false;
				}
				if (!cNKMBuffData.m_NKMBuffTemplet.m_bRangeSonAllowLandUnit && !this.IsAirUnit())
				{
					return false;
				}
				if (!cNKMBuffData.m_NKMBuffTemplet.m_bRangeSonAllowAwaken && this.GetUnitTemplet().m_UnitTempletBase.m_bAwaken)
				{
					return false;
				}
				if (!cNKMBuffData.m_NKMBuffTemplet.m_bRangeSonAllowNormal && this.GetUnitTemplet().m_UnitTempletBase.m_bAwaken)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x000A1F98 File Offset: 0x000A0198
		public NKMBuffData GetBuff(short buffID, bool bNegativeBuff = false)
		{
			if (this.m_UnitFrameData.m_dicBuffData.ContainsKey(buffID))
			{
				return this.m_UnitFrameData.m_dicBuffData[buffID];
			}
			if (bNegativeBuff && this.m_UnitFrameData.m_dicBuffData.ContainsKey(-buffID))
			{
				return this.m_UnitFrameData.m_dicBuffData[buffID];
			}
			return null;
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x000A1FF8 File Offset: 0x000A01F8
		public void DispelBuff(bool bDebuff, bool bDeleteInfinity)
		{
			this.m_listBuffToDelete.Clear();
			foreach (KeyValuePair<short, NKMBuffData> keyValuePair in this.m_UnitFrameData.m_dicBuffData)
			{
				NKMBuffData value = keyValuePair.Value;
				if (value != null && (!value.m_NKMBuffTemplet.m_bInfinity || bDeleteInfinity) && !value.m_NKMBuffTemplet.m_bNotDispel && this.WillAffectedByBuff(value))
				{
					bool flag;
					if (value.m_BuffSyncData.m_bRangeSon)
					{
						flag = value.m_NKMBuffTemplet.m_bDebuffSon;
					}
					else
					{
						flag = value.m_NKMBuffTemplet.m_bDebuff;
					}
					if (flag == bDebuff && this.IsBuffLive(keyValuePair.Key))
					{
						this.m_listBuffToDelete.Add(keyValuePair.Key);
					}
				}
			}
			for (int i = 0; i < this.m_listBuffToDelete.Count; i++)
			{
				short buffID = this.m_listBuffToDelete[i];
				this.DeleteBuff(buffID, NKMBuffTemplet.BuffEndDTType.Dispel);
			}
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x000A210C File Offset: 0x000A030C
		public void DispelRandomBuff(int count, bool bDispelDebuff)
		{
			if (count == 0)
			{
				return;
			}
			this.m_listBuffToDelete.Clear();
			foreach (KeyValuePair<short, NKMBuffData> keyValuePair in this.m_UnitFrameData.m_dicBuffData)
			{
				NKMBuffData value = keyValuePair.Value;
				bool flag = value.m_NKMBuffTemplet.m_bDebuff;
				if (value.m_BuffSyncData.m_bRangeSon)
				{
					flag = value.m_NKMBuffTemplet.m_bDebuffSon;
				}
				if (bDispelDebuff == flag && !value.m_NKMBuffTemplet.m_bInfinity && !value.m_NKMBuffTemplet.m_bNotDispel && this.WillAffectedByBuff(value))
				{
					this.m_listBuffToDelete.Add(value.m_NKMBuffTemplet.m_BuffID);
				}
			}
			for (int i = 0; i < count; i++)
			{
				if (this.m_listBuffToDelete.Count <= 0)
				{
					return;
				}
				int index = NKMRandom.Range(0, this.m_listBuffToDelete.Count);
				this.DeleteBuff(this.m_listBuffToDelete[index], NKMBuffTemplet.BuffEndDTType.Dispel);
				this.m_listBuffToDelete.RemoveAt(index);
			}
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x000A222C File Offset: 0x000A042C
		public void DeleteStatusBuff(NKM_UNIT_STATUS_EFFECT status, bool bForceRemove, bool bAffectOnly)
		{
			if (status == NKM_UNIT_STATUS_EFFECT.NUSE_NONE)
			{
				return;
			}
			this.m_listBuffToDelete.Clear();
			foreach (KeyValuePair<short, NKMBuffData> keyValuePair in this.m_UnitFrameData.m_dicBuffData)
			{
				NKMBuffData value = keyValuePair.Value;
				if (value.m_NKMBuffTemplet.HasStatus(status) && (bForceRemove || (!value.m_NKMBuffTemplet.m_bInfinity && !value.m_NKMBuffTemplet.m_bNotDispel)) && (!bAffectOnly || this.WillAffectedByBuff(value)))
				{
					this.m_listBuffToDelete.Add(keyValuePair.Key);
				}
			}
			for (int i = 0; i < this.m_listBuffToDelete.Count; i++)
			{
				this.DeleteBuff(this.m_listBuffToDelete[i], NKMBuffTemplet.BuffEndDTType.Dispel);
			}
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x000A2308 File Offset: 0x000A0508
		public virtual bool DeleteBuff(string buffStrID, bool bFromEnemy = false)
		{
			NKMBuffTemplet buffTempletByStrID = NKMBuffManager.GetBuffTempletByStrID(buffStrID);
			if (buffTempletByStrID != null)
			{
				short buffID = bFromEnemy ? (-buffTempletByStrID.m_BuffID) : buffTempletByStrID.m_BuffID;
				return this.DeleteBuff(buffID, NKMBuffTemplet.BuffEndDTType.NoUse);
			}
			return false;
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x000A2340 File Offset: 0x000A0540
		public virtual bool DeleteBuff(short buffID, NKMBuffTemplet.BuffEndDTType eEndDTType)
		{
			if (this.m_UnitFrameData.m_dicBuffData.ContainsKey(buffID))
			{
				NKMBuffData nkmbuffData = this.m_UnitFrameData.m_dicBuffData[buffID];
				if (nkmbuffData.m_NKMBuffTemplet.m_UnitLevel != 0)
				{
					this.m_bBuffUnitLevelChangedThisFrame = true;
				}
				this.m_UnitSyncData.m_dicBuffData.Remove(buffID);
				this.m_UnitFrameData.m_dicBuffData.Remove(buffID);
				if (this.WillAffectedByBuff(nkmbuffData))
				{
					if (eEndDTType != NKMBuffTemplet.BuffEndDTType.End)
					{
						if (eEndDTType == NKMBuffTemplet.BuffEndDTType.Dispel)
						{
							NKMUnit unit = this.m_NKMGame.GetUnit(nkmbuffData.m_BuffSyncData.m_MasterGameUnitUID, true, true);
							this.m_NKMGame.ProcessDamageTemplet(nkmbuffData.m_NKMBuffTemplet.m_DTDispel, unit, this, true, true, nkmbuffData.m_DamageInstBuff.m_AttackerUnitSkillTemplet, null);
						}
					}
					else
					{
						NKMUnit unit2 = this.m_NKMGame.GetUnit(nkmbuffData.m_BuffSyncData.m_MasterGameUnitUID, true, true);
						this.m_NKMGame.ProcessDamageTemplet(nkmbuffData.m_NKMBuffTemplet.m_DTEnd, unit2, this, true, true, nkmbuffData.m_DamageInstBuff.m_AttackerUnitSkillTemplet, null);
					}
				}
				if (this.GetUnitFrameData().m_BarrierBuffData != null && this.GetUnitFrameData().m_BarrierBuffData.m_BuffSyncData.m_BuffID == buffID)
				{
					this.GetUnitFrameData().m_BarrierBuffData = null;
				}
				this.m_NKMGame.GetObjectPool().CloseObj(nkmbuffData);
				this.m_bBuffChangedThisFrame = true;
				this.m_bPushSimpleSyncData = true;
				return true;
			}
			return false;
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x000A249B File Offset: 0x000A069B
		public bool IsBuffLive(short buffID)
		{
			return this.m_UnitFrameData.m_dicBuffData.ContainsKey(buffID);
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x000A24B4 File Offset: 0x000A06B4
		protected void SetBuffLevel(short buffID, byte level, byte timeLevel)
		{
			NKMBuffData buff = this.GetBuff(buffID, false);
			if (buff != null && (buff.m_BuffSyncData.m_BuffStatLevel != level || buff.m_BuffSyncData.m_BuffTimeLevel != timeLevel))
			{
				buff.m_BuffSyncData.m_BuffStatLevel = level;
				buff.m_BuffSyncData.m_BuffTimeLevel = timeLevel;
				this.m_bBuffChangedThisFrame = true;
				this.m_bPushSimpleSyncData = true;
			}
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x000A2510 File Offset: 0x000A0710
		protected NKMOperator GetOperatorForStat()
		{
			NKMOperator result = null;
			if (this.m_UnitTemplet != null && this.m_UnitTemplet.m_UnitTempletBase != null && this.m_UnitTemplet.m_UnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP && this.m_NKMGame.GetGameData() != null && this.m_NKMGame.GetGameData().GetTeamData(this.m_UnitDataGame.m_NKM_TEAM_TYPE) != null)
			{
				result = this.m_NKMGame.GetGameData().GetTeamData(this.m_UnitDataGame.m_NKM_TEAM_TYPE).m_Operator;
			}
			return result;
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x000A2594 File Offset: 0x000A0794
		protected void CheckAndCalculateBuffStat()
		{
			if (this.m_bBuffUnitLevelChangedThisFrame)
			{
				this.m_UnitFrameData.m_StatData.MakeBaseStat(this.m_NKMGame.GetGameData(), this.m_NKMGame.GetGameData().IsPVP(), this.GetUnitData(), this.GetUnitTemplet().m_UnitTempletBase.StatTemplet.m_StatData, false, this.GetUnitFrameData().m_BuffUnitLevel, this.GetOperatorForStat());
				this.m_bBuffUnitLevelChangedThisFrame = false;
			}
			if (this.m_bBuffChangedThisFrame)
			{
				this.m_UnitFrameData.m_StatData.UpdateFinalStat(this.m_NKMGame, this.m_NKMGame.GetGameData().GameStatRate, this, this.m_bBuffHPRateConserveRequired);
				this.m_bBuffChangedThisFrame = false;
			}
			if (this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_IMMUNE_MOVE_SLOW) && this.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_MOVE_SPEED_RATE) < 0f)
			{
				this.GetUnitFrameData().m_StatData.SetStatFinal(NKM_STAT_TYPE.NST_MOVE_SPEED_RATE, 0f);
			}
			if (this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_IMMUNE_ATTACK_SLOW) && this.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_ATTACK_SPEED_RATE) < 0f)
			{
				this.GetUnitFrameData().m_StatData.SetStatFinal(NKM_STAT_TYPE.NST_ATTACK_SPEED_RATE, 0f);
			}
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x000A26BC File Offset: 0x000A08BC
		public void SetAgro(bool bGetAgro, float fRange, float fDurationTime, int maxCount)
		{
			if (bGetAgro)
			{
				int num = 0;
				List<NKMUnit> sortUnitListByNearDist = this.GetSortUnitListByNearDist();
				for (int i = 0; i < sortUnitListByNearDist.Count; i++)
				{
					NKMUnit nkmunit = sortUnitListByNearDist[i];
					if (nkmunit != null && nkmunit != this && this.m_NKMGame.IsEnemy(this.GetUnitDataGame().m_NKM_TEAM_TYPE, nkmunit.GetUnitDataGame().m_NKM_TEAM_TYPE) && nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DYING && nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DIE)
					{
						NKM_FIND_TARGET_TYPE nkm_FIND_TARGET_TYPE = nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_FIND_TARGET_TYPE;
						switch (nkm_FIND_TARGET_TYPE)
						{
						case NKM_FIND_TARGET_TYPE.NFTT_NO:
							goto IL_161;
						case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY:
						case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_AIR_FIRST:
							break;
						case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_LAND:
							if (this.IsAirUnit())
							{
								goto IL_161;
							}
							break;
						case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_AIR:
							if (!this.IsAirUnit())
							{
								goto IL_161;
							}
							break;
						default:
							switch (nkm_FIND_TARGET_TYPE)
							{
							case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_ONLY:
								goto IL_161;
							case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS:
								if (this.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE != NKM_UNIT_ROLE_TYPE.NURT_TOWER)
								{
									goto IL_161;
								}
								break;
							case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_LAND:
								if (this.IsAirUnit())
								{
									goto IL_161;
								}
								if (this.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE != NKM_UNIT_ROLE_TYPE.NURT_TOWER)
								{
									goto IL_161;
								}
								break;
							case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_AIR:
								if (!this.IsAirUnit())
								{
									goto IL_161;
								}
								if (this.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE != NKM_UNIT_ROLE_TYPE.NURT_TOWER)
								{
									goto IL_161;
								}
								break;
							}
							break;
						}
						if (fRange > 0f && Math.Abs(nkmunit.GetUnitSyncData().m_PosX - this.GetUnitSyncData().m_PosX) > fRange)
						{
							return;
						}
						this.SetAgroData(nkmunit, fDurationTime);
						num++;
						if (maxCount > 0 && maxCount <= num)
						{
							return;
						}
					}
					IL_161:;
				}
				return;
			}
			List<NKMUnit> targetingUnitList = this.m_NKMGame.GetTargetingUnitList(this.GetUnitDataGame().m_GameUnitUID);
			if (targetingUnitList != null)
			{
				for (int j = 0; j < targetingUnitList.Count; j++)
				{
					NKMUnit nkmunit2 = targetingUnitList[j];
					if (nkmunit2 != null)
					{
						nkmunit2.GetUnitSyncData().m_TargetUID = 0;
					}
				}
			}
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x000A288C File Offset: 0x000A0A8C
		public bool SetAgro(NKMUnit targetUnit, float fDurationTime)
		{
			if (targetUnit == null)
			{
				return false;
			}
			if (targetUnit == this)
			{
				return false;
			}
			if (!this.m_NKMGame.IsEnemy(this.GetUnitDataGame().m_NKM_TEAM_TYPE, targetUnit.GetUnitDataGame().m_NKM_TEAM_TYPE))
			{
				return false;
			}
			if (targetUnit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DYING || targetUnit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DIE)
			{
				return false;
			}
			NKM_FIND_TARGET_TYPE nkm_FIND_TARGET_TYPE = targetUnit.GetUnitTemplet().m_UnitTempletBase.m_NKM_FIND_TARGET_TYPE;
			switch (nkm_FIND_TARGET_TYPE)
			{
			case NKM_FIND_TARGET_TYPE.NFTT_NO:
				return false;
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY:
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_AIR_FIRST:
				break;
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_LAND:
				if (this.IsAirUnit())
				{
					return false;
				}
				break;
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_AIR:
				if (!this.IsAirUnit())
				{
					return false;
				}
				break;
			default:
				switch (nkm_FIND_TARGET_TYPE)
				{
				case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_ONLY:
					return false;
				case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS:
					if (this.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE != NKM_UNIT_ROLE_TYPE.NURT_TOWER)
					{
						return false;
					}
					break;
				case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_LAND:
					if (this.IsAirUnit() || this.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE != NKM_UNIT_ROLE_TYPE.NURT_TOWER)
					{
						return false;
					}
					break;
				case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_AIR:
					if (!this.IsAirUnit() || this.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE != NKM_UNIT_ROLE_TYPE.NURT_TOWER)
					{
						return false;
					}
					break;
				}
				break;
			}
			this.SetAgroData(targetUnit, fDurationTime);
			return true;
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x000A29A0 File Offset: 0x000A0BA0
		public bool SetTargetUnit(NKMUnit targetUnit, float duration, bool bSubTarget)
		{
			if (targetUnit == null)
			{
				if (bSubTarget)
				{
					this.m_UnitSyncData.m_SubTargetUID = 0;
					this.m_SubTargetUnit = null;
					if (this.m_SubTargetUIDOrg != (int)this.m_UnitSyncData.m_SubTargetUID)
					{
						this.GetUnitFrameData().m_bTargetChangeThisFrame = true;
					}
					this.m_UnitFrameData.m_bFindSubTargetThisFrame = false;
					this.m_UnitFrameData.m_fFindSubTargetTime = duration;
					return true;
				}
				return false;
			}
			else
			{
				if (targetUnit == this)
				{
					return false;
				}
				if (targetUnit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DYING || targetUnit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DIE)
				{
					return false;
				}
				bool flag = this.m_NKMGame.IsEnemy(this.GetUnitDataGame().m_NKM_TEAM_TYPE, targetUnit.GetUnitDataGame().m_NKM_TEAM_TYPE);
				if (!bSubTarget)
				{
					if (this.GetTargetTypeToEnemy() != flag)
					{
						return false;
					}
					NKM_FIND_TARGET_TYPE nkm_FIND_TARGET_TYPE = targetUnit.GetUnitTemplet().m_UnitTempletBase.m_NKM_FIND_TARGET_TYPE;
					switch (nkm_FIND_TARGET_TYPE)
					{
					case NKM_FIND_TARGET_TYPE.NFTT_NO:
						return false;
					case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY:
					case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_AIR_FIRST:
						break;
					case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_LAND:
						if (this.IsAirUnit())
						{
							return false;
						}
						break;
					case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_AIR:
						if (!this.IsAirUnit())
						{
							return false;
						}
						break;
					default:
						switch (nkm_FIND_TARGET_TYPE)
						{
						case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_ONLY:
							return false;
						case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS:
							if (this.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE != NKM_UNIT_ROLE_TYPE.NURT_TOWER)
							{
								return false;
							}
							break;
						case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_LAND:
							if (this.IsAirUnit() || this.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE != NKM_UNIT_ROLE_TYPE.NURT_TOWER)
							{
								return false;
							}
							break;
						case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_AIR:
							if (!this.IsAirUnit() || this.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_ROLE_TYPE != NKM_UNIT_ROLE_TYPE.NURT_TOWER)
							{
								return false;
							}
							break;
						}
						break;
					}
				}
				if (bSubTarget)
				{
					this.m_UnitSyncData.m_SubTargetUID = targetUnit.GetUnitDataGame().m_GameUnitUID;
					this.m_SubTargetUnit = targetUnit;
					if (this.m_SubTargetUIDOrg != (int)this.m_UnitSyncData.m_SubTargetUID)
					{
						this.GetUnitFrameData().m_bTargetChangeThisFrame = true;
					}
					this.m_UnitFrameData.m_bFindSubTargetThisFrame = false;
					this.m_UnitFrameData.m_fFindSubTargetTime = duration;
				}
				else
				{
					this.m_UnitSyncData.m_TargetUID = targetUnit.GetUnitDataGame().m_GameUnitUID;
					this.m_TargetUnit = targetUnit;
					if (this.m_TargetUIDOrg != (int)this.m_UnitSyncData.m_TargetUID)
					{
						this.GetUnitFrameData().m_bTargetChangeThisFrame = true;
					}
					this.m_UnitFrameData.m_bFindTargetThisFrame = false;
					this.m_UnitFrameData.m_fFindTargetTime = duration;
				}
				return true;
			}
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x000A2BB4 File Offset: 0x000A0DB4
		private void SetAgroData(NKMUnit targetUnit, float fDurationTime)
		{
			if (targetUnit == null)
			{
				return;
			}
			if (targetUnit.GetUnitSyncData().m_TargetUID != this.GetUnitDataGame().m_GameUnitUID)
			{
				targetUnit.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_GET_AGRO);
			}
			targetUnit.GetUnitSyncData().m_TargetUID = this.GetUnitDataGame().m_GameUnitUID;
			targetUnit.GetUnitFrameData().m_fFindTargetTime = fDurationTime;
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x000A2C08 File Offset: 0x000A0E08
		protected int GetSkillLevelIfNowSkillState()
		{
			NKMUnitSkillTemplet skillTempletNowState = this.GetSkillTempletNowState();
			if (skillTempletNowState != null)
			{
				return skillTempletNowState.m_Level;
			}
			return 0;
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x000A2C28 File Offset: 0x000A0E28
		protected NKMUnitSkillTemplet GetSkillTempletNowState()
		{
			NKMUnitState unitState = this.GetUnitState((short)this.GetUnitSyncData().m_StateID);
			NKMUnitData unitData = this.GetUnitData();
			if (unitState != null && unitData != null && (unitState.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_HYPER || unitState.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_SKILL))
			{
				return unitData.GetUnitSkillTempletByType(unitState.m_NKM_SKILL_TYPE);
			}
			return null;
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x000A2C74 File Offset: 0x000A0E74
		public float GetMaxHP(float fHPRate = 1f)
		{
			return this.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP) * fHPRate;
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x000A2C89 File Offset: 0x000A0E89
		public void SetHPRate(float rate)
		{
			this.GetMaxHP(1f);
			this.GetUnitSyncData().SetHP(this.GetMaxHP(1f) * rate);
			this.m_PushSyncData = true;
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x000A2CB8 File Offset: 0x000A0EB8
		public void SetDispel(bool bDebuff, float fRangeMin, float fRangeMax, int maxCount, bool bTargetSelf, int dispelCountPerSkillLevel, bool bDeleteInfinity)
		{
			int num = 0;
			int skillLevelIfNowSkillState = this.GetSkillLevelIfNowSkillState();
			if (skillLevelIfNowSkillState > 0 && maxCount > 0)
			{
				num = (skillLevelIfNowSkillState - 1) * dispelCountPerSkillLevel;
			}
			int num2 = 0;
			List<NKMUnit> sortUnitListByNearDist = this.GetSortUnitListByNearDist();
			for (int i = 0; i < sortUnitListByNearDist.Count; i++)
			{
				NKMUnit nkmunit = sortUnitListByNearDist[i];
				if (nkmunit != null && (!bDebuff || !this.m_NKMGame.IsEnemy(this.GetUnitDataGame().m_NKM_TEAM_TYPE, nkmunit.GetUnitDataGame().m_NKM_TEAM_TYPE)) && (bDebuff || this.m_NKMGame.IsEnemy(this.GetUnitDataGame().m_NKM_TEAM_TYPE, nkmunit.GetUnitDataGame().m_NKM_TEAM_TYPE)) && nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DYING && nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DIE && nkmunit.GetUnitSyncData().GetHP() > 0f && (!bTargetSelf || nkmunit == this))
				{
					if (!fRangeMin.IsNearlyZero(1E-05f) || !fRangeMax.IsNearlyZero(1E-05f))
					{
						float num3 = this.GetUnitSyncData().m_PosX;
						float num4 = this.GetUnitSyncData().m_PosX;
						if (this.GetUnitSyncData().m_bRight)
						{
							num3 += fRangeMin;
							num4 += fRangeMax;
						}
						else
						{
							num3 -= fRangeMax;
							num4 -= fRangeMin;
						}
						if (nkmunit.GetUnitSyncData().m_PosX < num3 || nkmunit.GetUnitSyncData().m_PosX > num4)
						{
							goto IL_182;
						}
					}
					nkmunit.DispelStatusTime(bDebuff, 999);
					nkmunit.DispelBuff(bDebuff, bDeleteInfinity);
					nkmunit.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_DISPEL);
					num2++;
					if (maxCount + num > 0 && maxCount + num <= num2)
					{
						break;
					}
				}
				IL_182:;
			}
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x000A2E5C File Offset: 0x000A105C
		public void SetEventHeal(NKMEventHeal eventHeal, float fPosX)
		{
			if (this.m_NKM_UNIT_CLASS_TYPE != NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
			{
				return;
			}
			int num = eventHeal.m_MaxCount;
			int skillLevelIfNowSkillState = this.GetSkillLevelIfNowSkillState();
			if (skillLevelIfNowSkillState > 0 && eventHeal.m_MaxCount > 0)
			{
				num += (skillLevelIfNowSkillState - 1) * eventHeal.m_HealCountPerSkillLevel;
			}
			int num2 = 0;
			List<NKMUnit> list = new List<NKMUnit>();
			NKMUnit nkmunit = this.GetTargetUnit(false);
			if (nkmunit != null && this.IsAlly(nkmunit))
			{
				list.Add(nkmunit);
			}
			if (list.Count == 0 && eventHeal.m_bSelfTargetingOnly)
			{
				list.Add(this);
			}
			if (list.Count == 0 || num != 1)
			{
				if (nkmunit != null && eventHeal.m_bSplashNearTarget)
				{
					list.AddRange(nkmunit.GetSortUnitListByNearDist());
				}
				else
				{
					list.AddRange(this.GetSortUnitListByNearDist());
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				nkmunit = list[i];
				if (nkmunit != null && (eventHeal.m_bEnableSelfHeal || nkmunit != this) && (!eventHeal.m_bIgnoreShip || !NKMUnitManager.IsShipType(nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE)) && !eventHeal.m_IgnoreStyleType.Contains(nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE) && !eventHeal.m_IgnoreStyleType.Contains(nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE_SUB) && (eventHeal.m_AllowStyleType.Count <= 0 || eventHeal.m_AllowStyleType.Contains(nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE) || eventHeal.m_AllowStyleType.Contains(nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE_SUB)) && !this.m_NKMGame.IsEnemy(this.GetUnitDataGame().m_NKM_TEAM_TYPE, nkmunit.GetUnitDataGame().m_NKM_TEAM_TYPE) && nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DYING && nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DIE && nkmunit.GetUnitSyncData().GetHP() > 0f)
				{
					if (!eventHeal.m_fRangeMin.IsNearlyZero(1E-05f) || !eventHeal.m_fRangeMax.IsNearlyZero(1E-05f))
					{
						float num3;
						float num4;
						if (this.GetUnitSyncData().m_bRight)
						{
							num3 = fPosX + eventHeal.m_fRangeMin;
							num4 = fPosX + eventHeal.m_fRangeMax;
						}
						else
						{
							num3 = fPosX - eventHeal.m_fRangeMax;
							num4 = fPosX - eventHeal.m_fRangeMin;
						}
						if (nkmunit.GetUnitSyncData().m_PosX < num3 || nkmunit.GetUnitSyncData().m_PosX > num4)
						{
							goto IL_314;
						}
					}
					num2++;
					float num5 = eventHeal.CalcHealAmount(skillLevelIfNowSkillState, nkmunit.GetMaxHP(1f));
					nkmunit.SetHeal(num5, this.GetUnitDataGame().m_GameUnitUID);
					Log.Debug(string.Format("unitID: {0}, [EventHeal] caster:{1} target:{2} amount:{3} count:{4}/{5}", new object[]
					{
						this.GetUnitTemplet().m_UnitTempletBase.m_UnitStrID,
						this.GetUnitData().m_UnitUID,
						nkmunit.GetUnitData().m_UnitUID,
						num5,
						num2,
						num
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnit.cs", 9435);
					if (num > 0 && num2 >= num)
					{
						return;
					}
				}
				IL_314:;
			}
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x000A3190 File Offset: 0x000A1390
		protected void SetHeal(float fHeal, short UIDCaster)
		{
			if (this.m_NKM_UNIT_CLASS_TYPE != NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
			{
				return;
			}
			if (fHeal > 0f)
			{
				if (this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_NOHEAL))
				{
					return;
				}
				float num = 0f;
				NKMUnit unit = this.m_NKMGame.GetUnit(UIDCaster, true, false);
				if (unit != null)
				{
					num = unit.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HEAL_RATE);
				}
				fHeal *= 1f + num - this.m_UnitFrameData.m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HEAL_REDUCE_RATE);
				if (fHeal <= 0f)
				{
					return;
				}
			}
			if (fHeal > 0f && this.GetUnitFrameData().m_fHealFeedback > 0f)
			{
				NKM_TEAM_TYPE teamType = NKM_TEAM_TYPE.NTT_INVALID;
				NKMUnit unit2 = this.m_NKMGame.GetUnit(this.GetUnitFrameData().m_fHealFeedbackMasterGameUnitUID, true, true);
				if (unit2 != null)
				{
					teamType = unit2.GetUnitDataGame().m_NKM_TEAM_TYPE;
				}
				bool flag = false;
				if (this.GetUnitFrameData().m_bInvincible || this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_INVINCIBLE))
				{
					flag = true;
				}
				if (this.m_UnitStateNow != null && this.m_UnitStateNow.m_bInvincibleState)
				{
					flag = true;
				}
				if (!flag)
				{
					this.AddDamage(false, fHeal * this.GetUnitFrameData().m_fHealFeedback, NKM_DAMAGE_RESULT_TYPE.NDRT_NO_MARK, this.GetUnitFrameData().m_fHealFeedbackMasterGameUnitUID, teamType, false, true, false);
				}
				return;
			}
			if (this.GetUnitSyncData().GetHP() + fHeal > this.GetMaxHP(1f))
			{
				fHeal = this.GetMaxHP(1f) - this.GetUnitSyncData().GetHP();
			}
			this.GetUnitSyncData().SetHP(this.GetUnitSyncData().GetHP() + fHeal);
			if (this.GetUnitSyncData().GetHP() > this.GetMaxHP(1f))
			{
				this.GetUnitSyncData().SetHP(this.GetMaxHP(1f));
			}
			if (fHeal > 0f)
			{
				NKMDamageData nkmdamageData = new NKMDamageData();
				nkmdamageData.m_FinalDamage = (int)fHeal;
				nkmdamageData.m_NKM_DAMAGE_RESULT_TYPE = NKM_DAMAGE_RESULT_TYPE.NDRT_HEAL;
				nkmdamageData.m_GameUnitUIDAttacker = (long)UIDCaster;
				this.m_UnitSyncData.m_listDamageData.Add(nkmdamageData);
				this.m_PushSyncData = true;
				this.m_NKMGame.m_GameRecord.AddHeal(UIDCaster, fHeal);
			}
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x000A3380 File Offset: 0x000A1580
		public void SetCost(float fAddCost, float fRemoveCost, float fCostPerSkillLevel)
		{
			NKMGameRuntimeTeamData nkmgameRuntimeTeamData;
			NKMGameRuntimeTeamData nkmgameRuntimeTeamData2;
			if (this.m_NKMGame.IsATeam(this.GetUnitDataGame().m_NKM_TEAM_TYPE))
			{
				nkmgameRuntimeTeamData = this.m_NKMGame.GetGameRuntimeData().m_NKMGameRuntimeTeamDataA;
				nkmgameRuntimeTeamData2 = this.m_NKMGame.GetGameRuntimeData().m_NKMGameRuntimeTeamDataB;
			}
			else
			{
				if (!this.m_NKMGame.IsBTeam(this.GetUnitDataGame().m_NKM_TEAM_TYPE))
				{
					Log.Warn("unitID: " + this.GetUnitTemplet().m_UnitTempletBase.m_UnitStrID + ", invalid team type:" + this.GetUnitDataGame().m_NKM_TEAM_TYPE.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnit.cs", 9550);
					return;
				}
				nkmgameRuntimeTeamData = this.m_NKMGame.GetGameRuntimeData().m_NKMGameRuntimeTeamDataB;
				nkmgameRuntimeTeamData2 = this.m_NKMGame.GetGameRuntimeData().m_NKMGameRuntimeTeamDataA;
			}
			int skillLevelIfNowSkillState = this.GetSkillLevelIfNowSkillState();
			float num = 0f;
			if (skillLevelIfNowSkillState > 0)
			{
				num = fCostPerSkillLevel * (float)(skillLevelIfNowSkillState - 1);
			}
			float num2 = fAddCost + fAddCost * num;
			nkmgameRuntimeTeamData.m_fRespawnCost += num2;
			if (Math.Abs(num2) > 0.01f)
			{
				switch ((byte)((int)(num2 + 2f)))
				{
				case 2:
					this.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_ADD_RESPAWN_COST0);
					break;
				case 3:
					this.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_ADD_RESPAWN_COST1);
					break;
				case 4:
					this.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_ADD_RESPAWN_COST2);
					break;
				case 5:
					this.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_ADD_RESPAWN_COST3);
					break;
				case 6:
					this.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_ADD_RESPAWN_COST4);
					break;
				case 7:
					this.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_ADD_RESPAWN_COST5);
					break;
				default:
					this.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_ADD_RESPAWN_COST);
					break;
				}
			}
			float num3 = fRemoveCost + fRemoveCost * num;
			nkmgameRuntimeTeamData2.m_fRespawnCost -= num3;
			if (Math.Abs(num3) > 0.01f)
			{
				switch ((byte)((int)(num3 + 17f)))
				{
				case 12:
					this.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_REMOVE_RESPAWN_COST_MINUS5);
					break;
				case 13:
					this.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_REMOVE_RESPAWN_COST_MINUS4);
					break;
				case 14:
					this.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_REMOVE_RESPAWN_COST_MINUS3);
					break;
				case 15:
					this.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_REMOVE_RESPAWN_COST_MINUS2);
					break;
				case 16:
					this.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_REMOVE_RESPAWN_COST_MINUS1);
					break;
				case 17:
					this.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_REMOVE_RESPAWN_COST0);
					break;
				case 18:
					this.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_REMOVE_RESPAWN_COST1);
					break;
				case 19:
					this.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_REMOVE_RESPAWN_COST2);
					break;
				case 20:
					this.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_REMOVE_RESPAWN_COST3);
					break;
				case 21:
					this.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_REMOVE_RESPAWN_COST4);
					break;
				case 22:
					this.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_REMOVE_RESPAWN_COST5);
					break;
				default:
					this.AddEventMark(NKM_UNIT_EVENT_MARK.NUEM_REMOVE_RESPAWN_COST);
					break;
				}
			}
			if (nkmgameRuntimeTeamData.m_fRespawnCost > 10f)
			{
				nkmgameRuntimeTeamData.m_fRespawnCost = 10f;
			}
			if (nkmgameRuntimeTeamData2.m_fRespawnCost < 0f)
			{
				nkmgameRuntimeTeamData2.m_fRespawnCost = 0f;
			}
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x000A360C File Offset: 0x000A180C
		public void SetStun(NKMUnit Applier, float fStunTime, float fRange, int maxCount, float fStunTimePerSkillLevel, int StunCountPerSkillLevel, HashSet<NKM_UNIT_STYLE_TYPE> m_IgnoreStyleType)
		{
			float num = 0f;
			int num2 = 0;
			int skillLevelIfNowSkillState = this.GetSkillLevelIfNowSkillState();
			if (skillLevelIfNowSkillState > 0)
			{
				num = fStunTimePerSkillLevel * (float)(skillLevelIfNowSkillState - 1);
				if (maxCount > 0)
				{
					num2 = (skillLevelIfNowSkillState - 1) * StunCountPerSkillLevel;
				}
			}
			int num3 = 0;
			List<NKMUnit> sortUnitListByNearDist = this.GetSortUnitListByNearDist();
			for (int i = 0; i < sortUnitListByNearDist.Count; i++)
			{
				NKMUnit nkmunit = sortUnitListByNearDist[i];
				if (nkmunit != null && nkmunit != this && !m_IgnoreStyleType.Contains(nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE) && !m_IgnoreStyleType.Contains(nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE_SUB) && this.m_NKMGame.IsEnemy(this.GetUnitDataGame().m_NKM_TEAM_TYPE, nkmunit.GetUnitDataGame().m_NKM_TEAM_TYPE) && nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DYING && nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DIE && nkmunit.GetUnitSyncData().GetHP() > 0f)
				{
					if (fRange > 0f && Math.Abs(nkmunit.GetUnitSyncData().m_PosX - this.GetUnitSyncData().m_PosX) > fRange)
					{
						break;
					}
					float time = fStunTime + num;
					nkmunit.ApplyStatusTime(NKM_UNIT_STATUS_EFFECT.NUSE_STUN, time, Applier, false, false, false);
					num3++;
					if (maxCount + num2 > 0 && maxCount + num2 <= num3)
					{
						break;
					}
				}
			}
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x000A375B File Offset: 0x000A195B
		public bool GetTargetTypeToEnemy()
		{
			return this.GetTargetTypeToEnemy(this.GetUnitTemplet().m_UnitTempletBase.m_NKM_FIND_TARGET_TYPE);
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x000A3773 File Offset: 0x000A1973
		private bool GetTargetTypeToEnemy(NKM_FIND_TARGET_TYPE type)
		{
			return type - NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM > 8;
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x000A3780 File Offset: 0x000A1980
		public bool CanAttackTargetDependingAirUnit(bool isTargetAirUnit)
		{
			switch (this.GetUnitTemplet().m_UnitTempletBase.m_NKM_FIND_TARGET_TYPE)
			{
			case NKM_FIND_TARGET_TYPE.NFTT_NO:
				return false;
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_LAND:
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_LAND_RANGER_SUPPORTER_SNIPER_FIRST:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_LAND:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_LAND_BOSS_LAST:
			case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_LAND:
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LAND:
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LOW_HP_LAND:
				return !isTargetAirUnit;
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY_AIR:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_AIR:
			case NKM_FIND_TARGET_TYPE.NFTT_FAR_ENEMY_AIR_BOSS_LAST:
			case NKM_FIND_TARGET_TYPE.NFTT_ENEMY_BOSS_AIR:
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_AIR:
			case NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LOW_HP_AIR:
				return isTargetAirUnit;
			}
			return true;
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x000A380C File Offset: 0x000A1A0C
		public NKMUnit GetTargetUnit(bool bDying = false)
		{
			if (this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DIE)
			{
				return null;
			}
			if (this.m_UnitSyncData.m_TargetUID <= 0)
			{
				return null;
			}
			NKMUnit unit = this.m_NKMGame.GetUnit(this.m_UnitSyncData.m_TargetUID, true, false);
			if (unit == null)
			{
				this.m_UnitSyncData.m_TargetUID = 0;
				return null;
			}
			if (!bDying && (unit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DYING || unit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DIE))
			{
				this.m_UnitSyncData.m_TargetUID = 0;
				return null;
			}
			if (unit.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_CLOCKING) && !this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_DETECTER))
			{
				this.m_UnitSyncData.m_TargetUID = 0;
				return null;
			}
			if (unit.GetUnitStateNow() != null && unit.GetUnitStateNow().m_bForceNoTargeted)
			{
				this.m_UnitSyncData.m_TargetUID = 0;
				return null;
			}
			if (!this.CanAttackTargetDependingAirUnit(unit.IsAirUnit()) && !unit.IsBoss())
			{
				this.m_UnitSyncData.m_TargetUID = 0;
				return null;
			}
			if (this.GetUnitTemplet().m_bNoBackTarget)
			{
				if (this.GetUnitSyncData().m_bRight && unit.GetUnitSyncData().m_PosX < this.m_UnitSyncData.m_PosX)
				{
					this.m_UnitSyncData.m_TargetUID = 0;
					return null;
				}
				if (!this.GetUnitSyncData().m_bRight && unit.GetUnitSyncData().m_PosX > this.m_UnitSyncData.m_PosX)
				{
					this.m_UnitSyncData.m_TargetUID = 0;
					return null;
				}
				return unit;
			}
			else
			{
				if (this.GetTargetTypeToEnemy() && this.IsAlly(unit))
				{
					this.m_UnitSyncData.m_TargetUID = 0;
					return null;
				}
				if (!this.GetTargetTypeToEnemy() && !this.IsAlly(unit))
				{
					this.m_UnitSyncData.m_TargetUID = 0;
					return null;
				}
				if ((this.GetUnitTemplet().m_UnitTempletBase.m_NKM_FIND_TARGET_TYPE == NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LOW_HP || this.GetUnitTemplet().m_UnitTempletBase.m_NKM_FIND_TARGET_TYPE == NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LOW_HP_AIR || this.GetUnitTemplet().m_UnitTempletBase.m_NKM_FIND_TARGET_TYPE == NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LOW_HP_LAND) && unit.GetUnitSyncData().GetHP() >= unit.GetMaxHP(1f))
				{
					this.m_UnitSyncData.m_TargetUID = 0;
					return null;
				}
				return unit;
			}
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x000A3A0C File Offset: 0x000A1C0C
		public NKMUnit GetTargetUnit(short targetUID, NKMFindTargetData cNKMFindTargetData)
		{
			if (this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DIE)
			{
				return null;
			}
			if (targetUID <= 0)
			{
				return null;
			}
			if (cNKMFindTargetData == null)
			{
				return null;
			}
			NKMUnit unit = this.m_NKMGame.GetUnit(targetUID, true, false);
			if (unit == null)
			{
				return null;
			}
			if (unit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DYING || unit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DIE)
			{
				return null;
			}
			if (Math.Abs(unit.GetUnitSyncData().m_PosX - this.m_UnitSyncData.m_PosX) > cNKMFindTargetData.m_fSeeRange)
			{
				return null;
			}
			if (!this.IsAlly(unit) && unit.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_CLOCKING) && !this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_DETECTER))
			{
				return null;
			}
			if (unit.GetUnitStateNow() != null && unit.GetUnitStateNow().m_bForceNoTargeted)
			{
				return null;
			}
			if (!cNKMFindTargetData.m_bCanTargetBoss && unit.IsBoss())
			{
				return null;
			}
			if (cNKMFindTargetData.m_bNoBackTarget)
			{
				if (this.GetUnitSyncData().m_bRight && unit.GetUnitSyncData().m_PosX < this.m_UnitSyncData.m_PosX)
				{
					return null;
				}
				if (!this.GetUnitSyncData().m_bRight && unit.GetUnitSyncData().m_PosX > this.m_UnitSyncData.m_PosX)
				{
					return null;
				}
				return unit;
			}
			else if (cNKMFindTargetData.m_bNoFrontTarget)
			{
				if (this.GetUnitSyncData().m_bRight && unit.GetUnitSyncData().m_PosX > this.m_UnitSyncData.m_PosX)
				{
					return null;
				}
				if (!this.GetUnitSyncData().m_bRight && unit.GetUnitSyncData().m_PosX < this.m_UnitSyncData.m_PosX)
				{
					return null;
				}
				return unit;
			}
			else
			{
				if (this.GetTargetTypeToEnemy(cNKMFindTargetData.m_FindTargetType) && this.IsAlly(unit))
				{
					return null;
				}
				if (!this.GetTargetTypeToEnemy(cNKMFindTargetData.m_FindTargetType) && !this.IsAlly(unit))
				{
					return null;
				}
				NKM_FIND_TARGET_TYPE findTargetType = cNKMFindTargetData.m_FindTargetType;
				if (findTargetType - NKM_FIND_TARGET_TYPE.NFTT_NEAR_MY_TEAM_LOW_HP <= 2)
				{
					return null;
				}
				return unit;
			}
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x000A3BC4 File Offset: 0x000A1DC4
		public bool IsArriveTarget()
		{
			if (this.m_TargetUnit == null)
			{
				return false;
			}
			float num = Math.Abs(this.m_TargetUnit.GetUnitSyncData().m_PosX - this.m_UnitSyncData.m_PosX);
			if (num > this.m_UnitTemplet.m_SeeRangeMax)
			{
				this.m_UnitSyncData.m_TargetUID = 0;
				return false;
			}
			return num - (this.m_TargetUnit.GetUnitTemplet().m_UnitSizeX * 0.5f + this.m_UnitTemplet.m_UnitSizeX * 0.5f) < this.GetUnitDataGame().m_fTargetNearRange;
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x000A3C53 File Offset: 0x000A1E53
		public float GetDist(NKMUnit unit)
		{
			return Math.Abs(this.m_UnitSyncData.m_PosX - unit.GetUnitSyncData().m_PosX);
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x000A3C74 File Offset: 0x000A1E74
		public bool IsStopTime()
		{
			if (this.m_UnitFrameData.m_fStopReserveTime > 0f)
			{
				return false;
			}
			for (int i = 0; i < 3; i++)
			{
				if (this.m_UnitFrameData.m_StopTime[i] > 0f)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x000A3CB8 File Offset: 0x000A1EB8
		public void SetStopTime(float fStopTime, NKM_STOP_TIME_INDEX stopTimeIndex)
		{
			this.m_UnitFrameData.m_StopTime[(int)stopTimeIndex] = fStopTime;
			this.SetStopReserveTime(0.08f);
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x000A3CD3 File Offset: 0x000A1ED3
		public void SetStopReserveTime(float fStopReserveTime)
		{
			this.m_UnitFrameData.m_fStopReserveTime = fStopReserveTime;
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x000A3CE1 File Offset: 0x000A1EE1
		public int GetDamageResistCount(short gameUnitUID)
		{
			if (this.m_listDamageResistUnit.ContainsKey(gameUnitUID))
			{
				return this.m_listDamageResistUnit[gameUnitUID];
			}
			return 0;
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x000A3D00 File Offset: 0x000A1F00
		public void AddDamageResistCount(short gameUnitUID)
		{
			if (this.m_UnitFrameData.m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_DAMAGE_RESIST) > 0f && gameUnitUID > 0)
			{
				if (!this.m_listDamageResistUnit.ContainsKey(gameUnitUID))
				{
					this.m_listDamageResistUnit.Add(gameUnitUID, 1);
					return;
				}
				this.m_listDamageResistUnit[gameUnitUID] = this.m_listDamageResistUnit[gameUnitUID] + 1;
			}
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x000A3D60 File Offset: 0x000A1F60
		public virtual void DamageReact(NKMDamageInst cNKMDamageInst, bool bBuffDamage)
		{
			if (cNKMDamageInst == null)
			{
				return;
			}
			if (cNKMDamageInst.m_Templet == null)
			{
				return;
			}
			if (this.m_UnitStateNow == null)
			{
				cNKMDamageInst.m_ReActResult = NKM_REACT_TYPE.NRT_NO;
				return;
			}
			bool flag = false;
			if (this.m_UnitFrameData.m_bInvincible || this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_INVINCIBLE))
			{
				flag = true;
			}
			if (this.m_UnitStateNow.m_bInvincibleState)
			{
				flag = true;
			}
			if (flag && (cNKMDamageInst.m_EventAttack == null || !cNKMDamageInst.m_EventAttack.m_bForceHit))
			{
				cNKMDamageInst.m_ReActResult = NKM_REACT_TYPE.NRT_INVINCIBLE;
				return;
			}
			this.m_UnitFrameData.m_fHitLightTime = 0.15f;
			if (this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_PLAY)
			{
				if (this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_SLEEP))
				{
					this.RemoveStatus(NKM_UNIT_STATUS_EFFECT.NUSE_SLEEP);
				}
				bool flag2 = false;
				if (cNKMDamageInst.m_EventAttack != null && cNKMDamageInst.m_EventAttack.m_AttackUnitCount + (int)cNKMDamageInst.m_AttackerAddAttackUnitCount <= cNKMDamageInst.m_AttackCount)
				{
					flag2 = true;
				}
				bool flag3;
				if (cNKMDamageInst.m_EventAttack != null && cNKMDamageInst.m_EventAttack.m_bDamageSpeedDependRight)
				{
					flag3 = !cNKMDamageInst.m_bAttackerRight;
				}
				else
				{
					flag3 = (this.m_UnitSyncData.m_PosX < cNKMDamageInst.m_AttackerPosX);
				}
				if (this.m_UnitSyncData.m_PosZ < cNKMDamageInst.m_AttackerPosZ)
				{
					this.m_UnitSyncData.m_bAttackerZUp = true;
				}
				else
				{
					this.m_UnitSyncData.m_bAttackerZUp = false;
				}
				if (this.m_NKM_UNIT_CLASS_TYPE == NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER && cNKMDamageInst.m_Templet.m_bCrashBarrier && this.GetUnitFrameData().m_BarrierBuffData != null && !this.GetUnitFrameData().m_BarrierBuffData.m_NKMBuffTemplet.m_bNotDispel)
				{
					if (cNKMDamageInst.m_Templet.m_fFeedbackBarrier > 0f && this.GetUnitFrameData().m_BarrierBuffData.m_fBarrierHP > 0f && !this.IsBoss())
					{
						this.AddDamage(false, this.GetUnitFrameData().m_BarrierBuffData.m_fBarrierHP * cNKMDamageInst.m_Templet.m_fFeedbackBarrier, NKM_DAMAGE_RESULT_TYPE.NDRT_NORMAL, cNKMDamageInst.m_AttackerGameUnitUID, cNKMDamageInst.m_AttackerTeamType, false, false, false);
					}
					this.GetUnitFrameData().m_BarrierBuffData.m_fBarrierHP = -2f;
				}
				NKM_SUPER_ARMOR_LEVEL currentSuperArmorLevel = this.m_UnitFrameData.CurrentSuperArmorLevel;
				bool flag4 = true;
				if (currentSuperArmorLevel == NKM_SUPER_ARMOR_LEVEL.NSAL_NO || currentSuperArmorLevel < cNKMDamageInst.m_Templet.m_CrashSuperArmorLevel)
				{
					flag4 = false;
				}
				if (cNKMDamageInst.m_bEvade)
				{
					flag4 = true;
				}
				if (!flag4)
				{
					if ((!this.m_UnitStateNow.m_bNoMove || this.m_UnitTemplet.m_bNoMove) && this.m_NKM_UNIT_CLASS_TYPE == NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER && !this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_NO_DAMAGE_BACK_SPEED))
					{
						if (!cNKMDamageInst.m_Templet.m_BackSpeedX.IsNearlyEqual(-1f, 1E-05f))
						{
							float num = (float)Math.Sqrt((double)Math.Max(0f, 1f - this.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_DAMAGE_BACK_RESIST)));
							float num2;
							if (flag3)
							{
								num2 = -cNKMDamageInst.m_Templet.m_BackSpeedX * this.m_UnitTemplet.m_fDamageBackFactor * num;
							}
							else
							{
								num2 = cNKMDamageInst.m_Templet.m_BackSpeedX * this.m_UnitTemplet.m_fDamageBackFactor * num;
							}
							if (num2 * this.m_UnitFrameData.m_fDamageSpeedX >= 0f)
							{
								if (Math.Abs(num2) >= Math.Abs(this.m_UnitFrameData.m_fDamageSpeedX))
								{
									this.m_UnitFrameData.m_fDamageSpeedX = num2;
									this.m_UnitFrameData.m_fSpeedX = 0f;
									this.m_UnitFrameData.m_fDamageSpeedKeepTimeX = cNKMDamageInst.m_Templet.m_BackSpeedKeepTimeX;
								}
							}
							else
							{
								this.m_UnitFrameData.m_fDamageSpeedX += num2;
								this.m_UnitFrameData.m_fSpeedX = 0f;
								if (Math.Abs(num2) >= Math.Abs(this.m_UnitFrameData.m_fDamageSpeedX))
								{
									this.m_UnitFrameData.m_fDamageSpeedKeepTimeX = cNKMDamageInst.m_Templet.m_BackSpeedKeepTimeX;
								}
							}
						}
						if (!cNKMDamageInst.m_Templet.m_BackSpeedZ.IsNearlyEqual(-1f, 1E-05f) && !flag2)
						{
							this.m_UnitFrameData.m_fSpeedZ = 0f;
							this.m_UnitFrameData.m_fDamageSpeedZ = cNKMDamageInst.m_Templet.m_BackSpeedZ;
							this.m_UnitFrameData.m_fDamageSpeedKeepTimeZ = cNKMDamageInst.m_Templet.m_BackSpeedKeepTimeZ;
						}
						if (!cNKMDamageInst.m_Templet.m_BackSpeedJumpY.IsNearlyEqual(-1f, 1E-05f))
						{
							this.m_UnitFrameData.m_fSpeedY = 0f;
							this.m_UnitFrameData.m_fDamageSpeedJumpY = cNKMDamageInst.m_Templet.m_BackSpeedJumpY * this.m_UnitTemplet.m_fDamageUpFactor;
							this.m_UnitFrameData.m_fDamageSpeedKeepTimeJumpY = cNKMDamageInst.m_Templet.m_BackSpeedKeepTimeJumpY;
						}
						if (this.IsAirUnit() || this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_IMMUNE_AIRBORNE))
						{
							this.m_UnitFrameData.m_fDamageSpeedJumpY = 0f;
							this.m_UnitFrameData.m_fDamageSpeedKeepTimeJumpY = 0f;
						}
					}
					if (!this.GetUnitTemplet().m_bNoDamageStopTime)
					{
						this.SetStopTime(cNKMDamageInst.m_Templet.m_fStopTimeDef, NKM_STOP_TIME_INDEX.NSTI_DAMAGE);
						this.SetStopReserveTime(cNKMDamageInst.m_Templet.m_fStopReserveTimeDef);
					}
				}
				else
				{
					cNKMDamageInst.m_ReActResult = NKM_REACT_TYPE.NRT_NO_ACTION;
				}
				if (this.m_NKM_UNIT_CLASS_TYPE == NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
				{
					if (this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_PLAY)
					{
						bool flag5 = false;
						if (!bBuffDamage && this.m_UnitFrameData.m_GuardGameUnitUID > 0)
						{
							NKMUnit unit = this.m_NKMGame.GetUnit(this.GetUnitFrameData().m_GuardGameUnitUID, true, false);
							if (unit != null)
							{
								string text;
								flag5 = unit.CanGuard(cNKMDamageInst, bBuffDamage, this, out text);
								if (!string.IsNullOrEmpty(text))
								{
									unit.StateChange(text, true, false);
								}
							}
						}
						if (!flag5)
						{
							string text2;
							flag5 = this.CanRevenge(cNKMDamageInst, bBuffDamage, out text2);
							if (!string.IsNullOrEmpty(text2))
							{
								this.StateChange(text2, true, false);
							}
						}
						if (flag5)
						{
							cNKMDamageInst.m_ReActResult = NKM_REACT_TYPE.NRT_REVENGE;
							this.m_UnitFrameData.m_fSpeedX = 0f;
							this.m_UnitFrameData.m_fSpeedZ = 0f;
							this.m_UnitFrameData.m_fSpeedY = 0f;
							this.m_UnitFrameData.m_fDamageSpeedX = 0f;
							this.m_UnitFrameData.m_fDamageSpeedZ = 0f;
							this.m_UnitFrameData.m_fDamageSpeedJumpY = 0f;
							this.m_UnitFrameData.m_fDamageSpeedKeepTimeX = 0f;
							this.m_UnitFrameData.m_fDamageSpeedKeepTimeZ = 0f;
							this.m_UnitFrameData.m_fDamageSpeedKeepTimeJumpY = 0f;
							if (this.m_UnitStateNow.m_RevengeChangeState.Length > 1)
							{
								this.StateChange(this.m_UnitStateNow.m_RevengeChangeState, true, false);
							}
						}
						if (cNKMDamageInst.m_ReActResult == NKM_REACT_TYPE.NRT_DAMAGE_CATCH && !this.m_UnitStateNow.m_bNoMove && !this.m_UnitTemplet.m_bNoMove && !this.IsBoss() && !flag4 && this.GetUnitSyncData().m_CatcherGameUnitUID != cNKMDamageInst.m_AttackerGameUnitUID)
						{
							this.GetUnitSyncData().m_CatcherGameUnitUID = cNKMDamageInst.m_AttackerGameUnitUID;
							this.m_PushSyncData = true;
						}
						if (!this.m_UnitTemplet.m_bNoDamageState && !flag4 && !flag5)
						{
							switch (cNKMDamageInst.m_ReActResult)
							{
							case NKM_REACT_TYPE.NRT_DAMAGE_CATCH:
							case NKM_REACT_TYPE.NRT_DAMAGE_A:
								if (!this.IsAirUnit())
								{
									if (this.m_UnitFrameData.m_bFootOnLand)
									{
										this.StateChange("USN_DAMAGE_A", true, false);
									}
									else
									{
										this.StateChange("USN_DAMAGE_AIR_DOWN", true, false);
									}
								}
								else
								{
									this.StateChange("USN_DAMAGE_A", true, false);
								}
								break;
							case NKM_REACT_TYPE.NRT_DAMAGE_B:
								if (!this.IsAirUnit())
								{
									if (this.m_UnitFrameData.m_bFootOnLand)
									{
										this.StateChange("USN_DAMAGE_B", true, false);
									}
									else
									{
										this.StateChange("USN_DAMAGE_AIR_DOWN", true, false);
									}
								}
								else
								{
									this.StateChange("USN_DAMAGE_B", true, false);
								}
								break;
							case NKM_REACT_TYPE.NRT_DAMAGE_DOWN:
								if (!this.IsAirUnit())
								{
									if (!this.m_UnitTemplet.m_bNoDamageDownState)
									{
										if (this.m_UnitFrameData.m_bFootOnLand)
										{
											this.StateChange("USN_DAMAGE_DOWN", true, false);
										}
										else
										{
											this.StateChange("USN_DAMAGE_AIR_DOWN", true, false);
										}
									}
									else
									{
										this.StateChange("USN_DAMAGE_A", true, false);
									}
								}
								else
								{
									this.StateChange("USN_DAMAGE_A", true, false);
								}
								break;
							case NKM_REACT_TYPE.NRT_DAMAGE_UP:
								if (!this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_IMMUNE_AIRBORNE))
								{
									if (!this.IsAirUnit())
									{
										if (this.m_UnitTemplet.m_fDamageUpFactor > 0f)
										{
											this.StateChange("USN_DAMAGE_AIR_UP", true, false);
										}
										else
										{
											this.StateChange("USN_DAMAGE_A", true, false);
										}
									}
									else
									{
										this.StateChange("USN_DAMAGE_B", true, false);
									}
								}
								break;
							}
						}
					}
					if (this.m_StateNameNext.Length <= 1)
					{
						this.m_PushSyncData = true;
					}
				}
			}
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x000A457B File Offset: 0x000A277B
		public bool HasBarrierBuff()
		{
			return this.GetUnitFrameData().m_BarrierBuffData != null && this.GetUnitFrameData().m_BarrierBuffData.m_fBarrierHP > 0f;
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x000A45A8 File Offset: 0x000A27A8
		public void SetUnitSkillColorFade(float fTime)
		{
			this.m_UnitFrameData.m_fColorEventTime = fTime;
			if (fTime > 0f)
			{
				this.m_UnitFrameData.m_ColorR.SetTracking(0.3f, 0.2f, TRACKING_DATA_TYPE.TDT_SLOWER);
				this.m_UnitFrameData.m_ColorG.SetTracking(0.3f, 0.2f, TRACKING_DATA_TYPE.TDT_SLOWER);
				this.m_UnitFrameData.m_ColorB.SetTracking(0.3f, 0.2f, TRACKING_DATA_TYPE.TDT_SLOWER);
				return;
			}
			this.m_UnitFrameData.m_ColorR.SetNowValue(this.m_UnitTemplet.m_ColorR);
			this.m_UnitFrameData.m_ColorG.SetNowValue(this.m_UnitTemplet.m_ColorG);
			this.m_UnitFrameData.m_ColorB.SetNowValue(this.m_UnitTemplet.m_ColorB);
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x000A466C File Offset: 0x000A286C
		private bool CanRevenge(NKMDamageInst cNKMDamageInst, bool bBuffDamage, out string revengeState)
		{
			revengeState = null;
			if (cNKMDamageInst.m_bAttackerAwaken && !this.GetUnitTemplet().m_UnitTempletBase.m_bAwaken)
			{
				return false;
			}
			if (this.m_UnitStateNow.m_NKM_SKILL_TYPE >= NKM_SKILL_TYPE.NST_HYPER)
			{
				return false;
			}
			if (this.m_UnitFrameData.m_bFootOnLand)
			{
				if (this.m_UnitStateNow.m_bNormalRevengeState && cNKMDamageInst.m_Templet.m_CrashSuperArmorLevel < NKM_SUPER_ARMOR_LEVEL.NSAL_SKILL && !bBuffDamage)
				{
					return true;
				}
				if (cNKMDamageInst.m_Templet.m_ReActType >= NKM_REACT_TYPE.NRT_DAMAGE_A || cNKMDamageInst.m_Templet.m_fStunTime > 0f || NKMUnitStatusTemplet.IsCrowdControlStatus(cNKMDamageInst.m_Templet.m_ApplyStatusEffect))
				{
					int index = 0;
					if (this.CanUseAttack(this.m_UnitTemplet.m_listPassiveAttackStateData, NKM_ATTACK_STATE_DATA_TYPE.NASDT_REVENGE, false, 0f, ref index) && !this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_SILENCE))
					{
						revengeState = this.m_UnitTemplet.m_listHyperSkillStateData[index].m_StateName;
						return true;
					}
					if (this.CanUseAttack(this.m_UnitTemplet.m_listHyperSkillStateData, NKM_ATTACK_STATE_DATA_TYPE.NASDT_REVENGE, false, 0f, ref index) && !this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_SILENCE))
					{
						revengeState = this.m_UnitTemplet.m_listHyperSkillStateData[index].m_StateName;
						return true;
					}
					if (this.CanUseAttack(this.m_UnitTemplet.m_listSkillStateData, NKM_ATTACK_STATE_DATA_TYPE.NASDT_REVENGE, false, 0f, ref index) && !this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_SILENCE) && (cNKMDamageInst.m_Templet.m_CrashSuperArmorLevel <= NKM_SUPER_ARMOR_LEVEL.NSAL_SKILL || cNKMDamageInst.m_Templet.m_bCanRevenge))
					{
						revengeState = this.m_UnitTemplet.m_listSkillStateData[index].m_StateName;
						return true;
					}
					if (this.m_UnitStateNow.m_bRevengeState && cNKMDamageInst.m_Templet.m_CrashSuperArmorLevel <= NKM_SUPER_ARMOR_LEVEL.NSAL_SKILL)
					{
						return true;
					}
					if (this.m_UnitStateNow.m_bSuperRevengeState)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x000A4814 File Offset: 0x000A2A14
		private bool CanGuard(NKMDamageInst cNKMDamageInst, bool bBuffDamage, NKMUnit defender, out string guardState)
		{
			guardState = null;
			if (bBuffDamage)
			{
				return false;
			}
			if (defender == null)
			{
				return false;
			}
			if (this.IsDyingOrDie())
			{
				return false;
			}
			if (cNKMDamageInst.m_bAttackerAwaken && !this.GetUnitTemplet().m_UnitTempletBase.m_bAwaken)
			{
				return false;
			}
			if (this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_SILENCE))
			{
				return false;
			}
			if (this.HasCrowdControlStatus())
			{
				return false;
			}
			if (this.m_UnitStateNow.m_NKM_SKILL_TYPE >= NKM_SKILL_TYPE.NST_HYPER)
			{
				return false;
			}
			if (defender.GetUnitDataGame().m_GameUnitUID != this.GetUnitDataGame().m_GameUnitUID && cNKMDamageInst.m_listHitUnit.Contains(this.GetUnitDataGame().m_GameUnitUID))
			{
				return false;
			}
			if (this.m_UnitFrameData.m_bFootOnLand && (cNKMDamageInst.m_Templet.m_ReActType >= NKM_REACT_TYPE.NRT_DAMAGE_A || cNKMDamageInst.m_Templet.m_fStunTime > 0f || NKMUnitStatusTemplet.IsCrowdControlStatus(cNKMDamageInst.m_Templet.m_ApplyStatusEffect)))
			{
				int index = 0;
				if (defender.IsAirUnit())
				{
					if (this.CanUseAttack(this.m_UnitTemplet.m_listAirHyperSkillStateData, NKM_ATTACK_STATE_DATA_TYPE.NASDT_GUARD, false, 0f, ref index))
					{
						guardState = this.m_UnitTemplet.m_listAirHyperSkillStateData[index].m_StateName;
						return true;
					}
					if (this.CanUseAttack(this.m_UnitTemplet.m_listAirSkillStateData, NKM_ATTACK_STATE_DATA_TYPE.NASDT_GUARD, false, 0f, ref index) && (cNKMDamageInst.m_Templet.m_CrashSuperArmorLevel <= NKM_SUPER_ARMOR_LEVEL.NSAL_SKILL || cNKMDamageInst.m_Templet.m_bCanRevenge))
					{
						guardState = this.m_UnitTemplet.m_listAirSkillStateData[index].m_StateName;
						return true;
					}
					if (this.CanUseAttack(this.m_UnitTemplet.m_listAirPassiveAttackStateData, NKM_ATTACK_STATE_DATA_TYPE.NASDT_GUARD, false, 0f, ref index) && (cNKMDamageInst.m_Templet.m_CrashSuperArmorLevel <= NKM_SUPER_ARMOR_LEVEL.NSAL_SKILL || cNKMDamageInst.m_Templet.m_bCanRevenge))
					{
						guardState = this.m_UnitTemplet.m_listAirPassiveAttackStateData[index].m_StateName;
						return true;
					}
				}
				else
				{
					if (this.CanUseAttack(this.m_UnitTemplet.m_listHyperSkillStateData, NKM_ATTACK_STATE_DATA_TYPE.NASDT_GUARD, false, 0f, ref index))
					{
						guardState = this.m_UnitTemplet.m_listHyperSkillStateData[index].m_StateName;
						return true;
					}
					if (this.CanUseAttack(this.m_UnitTemplet.m_listSkillStateData, NKM_ATTACK_STATE_DATA_TYPE.NASDT_GUARD, false, 0f, ref index) && (cNKMDamageInst.m_Templet.m_CrashSuperArmorLevel <= NKM_SUPER_ARMOR_LEVEL.NSAL_SKILL || cNKMDamageInst.m_Templet.m_bCanRevenge))
					{
						guardState = this.m_UnitTemplet.m_listSkillStateData[index].m_StateName;
						return true;
					}
					if (this.CanUseAttack(this.m_UnitTemplet.m_listPassiveAttackStateData, NKM_ATTACK_STATE_DATA_TYPE.NASDT_GUARD, false, 0f, ref index) && (cNKMDamageInst.m_Templet.m_CrashSuperArmorLevel <= NKM_SUPER_ARMOR_LEVEL.NSAL_SKILL || cNKMDamageInst.m_Templet.m_bCanRevenge))
					{
						guardState = this.m_UnitTemplet.m_listPassiveAttackStateData[index].m_StateName;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x000A4ABC File Offset: 0x000A2CBC
		public virtual void AttackResult(NKMDamageInst cNKMDamageInst)
		{
			if (this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_PLAY)
			{
				return;
			}
			if (cNKMDamageInst.m_ReActResult == NKM_REACT_TYPE.NRT_NO)
			{
				return;
			}
			this.SetStopTime(cNKMDamageInst.m_Templet.m_fStopTimeAtk, NKM_STOP_TIME_INDEX.NSTI_DAMAGE);
			this.SetStopReserveTime(cNKMDamageInst.m_Templet.m_fStopReserveTimeAtk);
			if (this.m_NKM_UNIT_CLASS_TYPE == NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
			{
				if (cNKMDamageInst.m_ReActResult == NKM_REACT_TYPE.NRT_REVENGE)
				{
					if (!this.GetUnitTemplet().m_bNoDamageState)
					{
						if (!this.IsAirUnit())
						{
							if (this.m_UnitFrameData.m_bFootOnLand)
							{
								this.StateChange("USN_DAMAGE_A", true, false);
							}
							else if (!this.GetUnitTemplet().m_bNoDamageDownState)
							{
								this.StateChange("USN_DAMAGE_AIR_DOWN", true, false);
							}
							else
							{
								this.StateChange("USN_DAMAGE_A", true, false);
							}
						}
						else
						{
							this.StateChange("USN_DAMAGE_A", true, false);
						}
					}
					else
					{
						this.StateChangeToASTAND(true, false);
					}
					this.SetStopTime(0.5f, NKM_STOP_TIME_INDEX.NSTI_DAMAGE);
					this.SetStopReserveTime(0.1f);
					return;
				}
				if (cNKMDamageInst.m_Templet.m_AttackerStateChange.Length > 1)
				{
					this.StateChange(cNKMDamageInst.m_Templet.m_AttackerStateChange, true, false);
				}
			}
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x000A4BCE File Offset: 0x000A2DCE
		public bool IsDie()
		{
			return this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DIE;
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x000A4BE0 File Offset: 0x000A2DE0
		public bool IsDying()
		{
			return this.m_UnitSyncData.m_NKM_UNIT_PLAY_STATE == NKM_UNIT_PLAY_STATE.NUPS_DYING;
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x000A4BF3 File Offset: 0x000A2DF3
		public bool IsDyingOrDie()
		{
			return this.IsDying() || this.IsDie();
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x000A4C0C File Offset: 0x000A2E0C
		public void AddDamage(bool bAttackCountOver, float fDamage, NKM_DAMAGE_RESULT_TYPE eNKM_DAMAGE_RESULT_TYPE, short attackUnitGameUid, NKM_TEAM_TYPE teamType, bool bPushSyncData = false, bool bNoRedirect = false, bool bInstaKill = false)
		{
			if (eNKM_DAMAGE_RESULT_TYPE == NKM_DAMAGE_RESULT_TYPE.NDRT_COOL_TIME)
			{
				this.AddCoolTimeDamage(fDamage);
			}
			else
			{
				if (!bInstaKill && this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_IRONWALL) && fDamage > 0f)
				{
					if (eNKM_DAMAGE_RESULT_TYPE == NKM_DAMAGE_RESULT_TYPE.NDRT_MISS)
					{
						fDamage = 0f;
					}
					else
					{
						fDamage = 1f;
					}
				}
				if (!bNoRedirect && !bInstaKill)
				{
					NKMUnit nkmunit = null;
					if (this.GetUnitFrameData().m_fDamageTransferGameUnitUID > 0)
					{
						nkmunit = this.m_NKMGame.GetUnit(this.GetUnitFrameData().m_fDamageTransferGameUnitUID, true, false);
					}
					if (this.GetUnitFrameData().m_fDamageTransfer > 0f && nkmunit != null && !nkmunit.IsDyingOrDie())
					{
						nkmunit.AddDamage(bAttackCountOver, fDamage * this.GetUnitFrameData().m_fDamageTransfer, eNKM_DAMAGE_RESULT_TYPE, attackUnitGameUid, teamType, true, true, false);
						if (this.GetUnitFrameData().m_fDamageTransfer > 0.99f)
						{
							return;
						}
						fDamage *= 1f - this.GetUnitFrameData().m_fDamageTransfer;
						this.m_UnitFrameData.m_fDamageThisFrame += fDamage;
					}
					else
					{
						this.m_UnitFrameData.m_fDamageThisFrame += fDamage;
					}
					if (fDamage > 0f && this.GetUnitFrameData().m_fDamageReflection > 0f)
					{
						NKMUnit unit = this.m_NKMGame.GetUnit(attackUnitGameUid, true, false);
						if (unit != null && !unit.IsDyingOrDie())
						{
							unit.AddDamage(bAttackCountOver, fDamage * this.GetUnitFrameData().m_fDamageReflection, eNKM_DAMAGE_RESULT_TYPE, attackUnitGameUid, teamType, true, true, false);
						}
					}
				}
				else
				{
					this.m_UnitFrameData.m_fDamageThisFrame += fDamage;
				}
			}
			NKMDamageData nkmdamageData = new NKMDamageData();
			nkmdamageData.m_bAttackCountOver = bAttackCountOver;
			nkmdamageData.m_FinalDamage = (int)fDamage;
			nkmdamageData.m_NKM_DAMAGE_RESULT_TYPE = eNKM_DAMAGE_RESULT_TYPE;
			nkmdamageData.m_GameUnitUIDAttacker = (long)attackUnitGameUid;
			this.m_UnitSyncData.m_listDamageData.Add(nkmdamageData);
			if (bPushSyncData)
			{
				this.m_PushSyncData = true;
			}
			if (eNKM_DAMAGE_RESULT_TYPE == NKM_DAMAGE_RESULT_TYPE.NDRT_NORMAL || eNKM_DAMAGE_RESULT_TYPE == NKM_DAMAGE_RESULT_TYPE.NDRT_NO_MARK || eNKM_DAMAGE_RESULT_TYPE == NKM_DAMAGE_RESULT_TYPE.NDRT_CRITICAL || eNKM_DAMAGE_RESULT_TYPE == NKM_DAMAGE_RESULT_TYPE.NDRT_WEAK || eNKM_DAMAGE_RESULT_TYPE == NKM_DAMAGE_RESULT_TYPE.NDRT_PROTECT || eNKM_DAMAGE_RESULT_TYPE == NKM_DAMAGE_RESULT_TYPE.NDRT_MISS)
			{
				this.GetUnitFrameData().m_DangerChargeHitCount++;
			}
			if (eNKM_DAMAGE_RESULT_TYPE == NKM_DAMAGE_RESULT_TYPE.NDRT_NO_MARK)
			{
				return;
			}
			if (this.m_NKM_UNIT_CLASS_TYPE == NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
			{
				this.m_NKMGame.m_GameRecord.AddDamage(attackUnitGameUid, teamType, this, fDamage);
			}
			if (this.m_NKMGame.GetDungeonType() == NKM_DUNGEON_TYPE.NDT_FIERCE && this.IsBTeam() && this.IsBoss())
			{
				float num = this.m_NKMGame.CaculateFiercePointByDamage(this);
				if (num > this.m_NKMGame.m_GameRecord.TotalFiercePoint)
				{
					this.m_NKMGame.m_GameRecord.SetTotalFiercePoint(num, 0f);
				}
			}
			if (this.m_NKMGame.GetDungeonType() == NKM_DUNGEON_TYPE.NDT_TRIM && this.IsBTeam() && this.IsBoss())
			{
				float num2 = this.m_NKMGame.CaculateTrimPointByDamage(this);
				if (num2 > this.m_NKMGame.m_GameRecord.TotalTrimPoint)
				{
					this.m_NKMGame.m_GameRecord.SetTotalTrimPoint(num2);
				}
			}
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x000A4EB0 File Offset: 0x000A30B0
		public void AddCoolTimeDamage(float fDamage)
		{
			if (this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_IMMUNE_SKILL_COOLTIME_DAMAGE))
			{
				return;
			}
			if (this.GetUnitTemplet().m_UnitTempletBase.StopDefaultCoolTime)
			{
				return;
			}
			if (this.GetUnitTemplet().m_listSkillStateData.Count > 0)
			{
				NKMAttackStateData nkmattackStateData = this.GetUnitTemplet().m_listSkillStateData[0];
				NKMUnitState unitState = this.GetUnitState(nkmattackStateData.m_StateName, true);
				if (unitState != null)
				{
					this.SetStateCoolTimeAdd(unitState, fDamage);
				}
			}
			if (this.GetUnitTemplet().m_listHyperSkillStateData.Count > 0)
			{
				NKMAttackStateData nkmattackStateData = this.GetUnitTemplet().m_listHyperSkillStateData[0];
				NKMUnitState unitState2 = this.GetUnitState(nkmattackStateData.m_StateName, true);
				if (unitState2 != null)
				{
					this.SetStateCoolTimeAdd(unitState2, fDamage);
				}
			}
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x000A4F58 File Offset: 0x000A3158
		public void Kill(NKMKillFeedBack cNKMKillFeedBack, short killGameUnitUID)
		{
			for (int i = 0; i < this.GetUnitTemplet().m_listKillFeedBack.Count; i++)
			{
				this.KillFeedBack(this.GetUnitTemplet().m_listKillFeedBack[i], killGameUnitUID);
			}
			this.KillFeedBack(cNKMKillFeedBack, killGameUnitUID);
			this.GetUnitFrameData().m_KillCount++;
			this.m_NKMGame.AddKillCount(this.m_UnitDataGame);
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x000A4FC4 File Offset: 0x000A31C4
		protected void KillFeedBack(NKMKillFeedBack cNKMKillFeedBack, short killGameUnitUID)
		{
			if (cNKMKillFeedBack == null)
			{
				return;
			}
			if (!this.CheckEventCondition(cNKMKillFeedBack.m_Condition))
			{
				return;
			}
			if (this.m_dicKillFeedBackGameUnitUID.ContainsKey(killGameUnitUID))
			{
				List<NKMKillFeedBack> list = this.m_dicKillFeedBackGameUnitUID[killGameUnitUID];
				for (int i = 0; i < list.Count; i++)
				{
					NKMKillFeedBack nkmkillFeedBack = list[i];
					if (cNKMKillFeedBack == nkmkillFeedBack)
					{
						return;
					}
				}
				list.Add(cNKMKillFeedBack);
			}
			else
			{
				List<NKMKillFeedBack> list = new List<NKMKillFeedBack>();
				list.Add(cNKMKillFeedBack);
				this.m_dicKillFeedBackGameUnitUID.Add(killGameUnitUID, list);
			}
			if (cNKMKillFeedBack.m_fSkillCoolTime > 0f && this.GetUnitTemplet().m_listSkillStateData.Count > 0)
			{
				NKMAttackStateData nkmattackStateData = this.GetUnitTemplet().m_listSkillStateData[0];
				if (nkmattackStateData != null)
				{
					NKMUnitState unitState = this.GetUnitState(nkmattackStateData.m_StateName, true);
					if (unitState != null && this.GetStateCoolTime(unitState) > cNKMKillFeedBack.m_fSkillCoolTime)
					{
						this.SetStateCoolTime(unitState, cNKMKillFeedBack.m_fSkillCoolTime);
					}
				}
			}
			if (cNKMKillFeedBack.m_fHyperSkillCoolTime > 0f && this.GetUnitTemplet().m_listHyperSkillStateData.Count > 0)
			{
				NKMAttackStateData nkmattackStateData2 = this.GetUnitTemplet().m_listHyperSkillStateData[0];
				if (nkmattackStateData2 != null)
				{
					NKMUnitState unitState2 = this.GetUnitState(nkmattackStateData2.m_StateName, true);
					if (unitState2 != null && this.GetStateCoolTime(unitState2) > cNKMKillFeedBack.m_fHyperSkillCoolTime)
					{
						this.SetStateCoolTime(unitState2, cNKMKillFeedBack.m_fHyperSkillCoolTime);
					}
				}
			}
			if (!cNKMKillFeedBack.m_fSkillCoolTimeAdd.IsNearlyZero(1E-05f) && this.GetUnitTemplet().m_listSkillStateData.Count > 0)
			{
				NKMAttackStateData nkmattackStateData3 = this.GetUnitTemplet().m_listSkillStateData[0];
				if (nkmattackStateData3 != null)
				{
					NKMUnitState unitState3 = this.GetUnitState(nkmattackStateData3.m_StateName, true);
					if (unitState3 != null)
					{
						this.SetStateCoolTimeAdd(unitState3, cNKMKillFeedBack.m_fSkillCoolTimeAdd);
					}
				}
			}
			if (!cNKMKillFeedBack.m_fHyperSkillCoolTimeAdd.IsNearlyZero(1E-05f) && this.GetUnitTemplet().m_listHyperSkillStateData.Count > 0)
			{
				NKMAttackStateData nkmattackStateData4 = this.GetUnitTemplet().m_listHyperSkillStateData[0];
				if (nkmattackStateData4 != null)
				{
					NKMUnitState unitState4 = this.GetUnitState(nkmattackStateData4.m_StateName, true);
					if (unitState4 != null)
					{
						this.SetStateCoolTimeAdd(unitState4, cNKMKillFeedBack.m_fHyperSkillCoolTimeAdd);
					}
				}
			}
			if (cNKMKillFeedBack.m_fHPRate > 0f)
			{
				float fHeal = this.GetMaxHP(1f) * cNKMKillFeedBack.m_fHPRate;
				this.SetHeal(fHeal, this.GetUnitSyncData().m_GameUnitUID);
			}
			if (cNKMKillFeedBack.m_BuffName.Length > 1)
			{
				this.AddBuffByStrID(cNKMKillFeedBack.m_BuffName, cNKMKillFeedBack.m_BuffStatLevel, cNKMKillFeedBack.m_BuffTimeLevel, this.m_UnitDataGame.m_GameUnitUID, true, false, false, 1);
			}
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x000A5238 File Offset: 0x000A3438
		public void StateCoolTimeReset(string stateName)
		{
			NKMUnitState unitState = this.GetUnitState(stateName, true);
			if (unitState != null)
			{
				this.StateCoolTimeReset((int)unitState.m_StateID);
			}
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x000A525D File Offset: 0x000A345D
		public void StateCoolTimeReset(int stateID)
		{
			if (this.m_dicStateCoolTime.ContainsKey(stateID))
			{
				this.m_dicStateCoolTime[stateID].m_CoolTime = 0f;
				this.m_PushSyncData = true;
			}
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x000A528A File Offset: 0x000A348A
		public void AddDynamicRespawnPool(NKMEventRespawn cNKMEventRespawn, short gameUnitUID)
		{
			if (!this.m_dicDynamicRespawnPool.ContainsKey(cNKMEventRespawn))
			{
				this.m_dicDynamicRespawnPool.Add(cNKMEventRespawn, new List<short>());
			}
			this.m_dicDynamicRespawnPool[cNKMEventRespawn].Add(gameUnitUID);
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x000A52C0 File Offset: 0x000A34C0
		public short GetDynamicRespawnPool(NKMEventRespawn cNKMEventRespawn)
		{
			if (!this.m_dicDynamicRespawnPool.ContainsKey(cNKMEventRespawn))
			{
				return 0;
			}
			List<short> list = this.m_dicDynamicRespawnPool[cNKMEventRespawn];
			for (int i = 0; i < list.Count; i++)
			{
				short num = list[i];
				if (this.m_NKMGame.GetUnit(num, false, true) != null && !this.m_NKMGame.IsInDynamicRespawnUnitReserve(num))
				{
					return num;
				}
			}
			return 0;
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x000A5324 File Offset: 0x000A3524
		public short GetDynamicRespawnPoolReRespawn(NKMEventRespawn cNKMEventRespawn)
		{
			if (!this.m_dicDynamicRespawnPool.ContainsKey(cNKMEventRespawn))
			{
				return 0;
			}
			List<short> list = this.m_dicDynamicRespawnPool[cNKMEventRespawn];
			for (int i = 0; i < list.Count; i++)
			{
				short num = list[i];
				if (this.m_NKMGame.GetUnit(num, true, false) != null && !this.m_NKMGame.IsInDynamicRespawnUnitReserve(num))
				{
					return num;
				}
			}
			return 0;
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x000A5388 File Offset: 0x000A3588
		public void AddUnitChangeRespawnPool(string unitStrID, short gameUnitUID)
		{
			if (!this.m_dicUnitChangeRespawnPool.ContainsKey(unitStrID))
			{
				this.m_dicUnitChangeRespawnPool.Add(unitStrID, new List<short>());
			}
			this.m_dicUnitChangeRespawnPool[unitStrID].Add(gameUnitUID);
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x000A53BC File Offset: 0x000A35BC
		public short GetUnitChangeRespawnPool(string unitStrID)
		{
			if (!this.m_dicUnitChangeRespawnPool.ContainsKey(unitStrID))
			{
				return 0;
			}
			List<short> list = this.m_dicUnitChangeRespawnPool[unitStrID];
			for (int i = 0; i < list.Count; i++)
			{
				short num = list[i];
				if (this.m_NKMGame.GetUnit(num, false, true) != null)
				{
					return num;
				}
			}
			return 0;
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x000A5414 File Offset: 0x000A3614
		public void SetHitFeedBack()
		{
			for (int i = 0; i < this.GetUnitTemplet().m_listHitFeedBack.Count; i++)
			{
				if (this.CheckEventCondition(this.GetUnitTemplet().m_listHitFeedBack[i].m_Condition))
				{
					this.GetUnitFrameData().m_listHitFeedBackCount[i] = this.GetUnitFrameData().m_listHitFeedBackCount[i] + 1;
					if (this.GetUnitFrameData().m_listHitFeedBackCount[i] >= this.GetUnitTemplet().m_listHitFeedBack[i].m_HitCount)
					{
						bool flag = false;
						if (this.GetUnitTemplet().m_listHitFeedBack[i].m_bStartAnyTime)
						{
							flag = true;
						}
						NKM_UNIT_STATE_TYPE nkm_UNIT_STATE_TYPE = this.GetUnitStateNow().m_NKM_UNIT_STATE_TYPE;
						if ((nkm_UNIT_STATE_TYPE == NKM_UNIT_STATE_TYPE.NUST_DAMAGE || nkm_UNIT_STATE_TYPE == NKM_UNIT_STATE_TYPE.NUST_ASTAND || nkm_UNIT_STATE_TYPE == NKM_UNIT_STATE_TYPE.NUST_MOVE || nkm_UNIT_STATE_TYPE == NKM_UNIT_STATE_TYPE.NUST_ATTACK) && this.GetUnitStateNow().m_NKM_SKILL_TYPE <= NKM_SKILL_TYPE.NST_ATTACK)
						{
							flag = true;
						}
						if (flag)
						{
							this.GetUnitFrameData().m_listHitFeedBackCount[i] = 0;
							if (this.GetUnitTemplet().m_listHitFeedBack[i].m_BuffStrID.Length > 1)
							{
								this.AddBuffByStrID(this.GetUnitTemplet().m_listHitFeedBack[i].m_BuffStrID, this.GetUnitTemplet().m_listHitFeedBack[i].m_BuffStatLevel, this.GetUnitTemplet().m_listHitFeedBack[i].m_BuffTimeLevel, this.GetUnitSyncData().m_GameUnitUID, true, false, false, 1);
							}
							if (this.GetUnitTemplet().m_listHitFeedBack[i].m_StateName.Length > 1 && this.CheckStateCoolTime(this.GetUnitTemplet().m_listHitFeedBack[i].m_StateName))
							{
								this.StateChange(this.GetUnitTemplet().m_listHitFeedBack[i].m_StateName, false, false);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x000A55E4 File Offset: 0x000A37E4
		public void SetHitCriticalFeedBack()
		{
			for (int i = 0; i < this.GetUnitTemplet().m_listHitCriticalFeedBack.Count; i++)
			{
				if (this.CheckEventCondition(this.GetUnitTemplet().m_listHitCriticalFeedBack[i].m_Condition))
				{
					this.GetUnitFrameData().m_listHitCriticalFeedBackCount[i] = this.GetUnitFrameData().m_listHitCriticalFeedBackCount[i] + 1;
					if (this.GetUnitFrameData().m_listHitCriticalFeedBackCount[i] >= this.GetUnitTemplet().m_listHitCriticalFeedBack[i].m_Count)
					{
						bool flag = false;
						if (this.GetUnitTemplet().m_listHitCriticalFeedBack[i].m_bStartAnyTime)
						{
							flag = true;
						}
						NKM_UNIT_STATE_TYPE nkm_UNIT_STATE_TYPE = this.GetUnitStateNow().m_NKM_UNIT_STATE_TYPE;
						if ((nkm_UNIT_STATE_TYPE == NKM_UNIT_STATE_TYPE.NUST_DAMAGE || nkm_UNIT_STATE_TYPE == NKM_UNIT_STATE_TYPE.NUST_ASTAND || nkm_UNIT_STATE_TYPE == NKM_UNIT_STATE_TYPE.NUST_MOVE || nkm_UNIT_STATE_TYPE == NKM_UNIT_STATE_TYPE.NUST_ATTACK) && this.GetUnitStateNow().m_NKM_SKILL_TYPE <= NKM_SKILL_TYPE.NST_ATTACK)
						{
							flag = true;
						}
						if (flag)
						{
							this.GetUnitFrameData().m_listHitCriticalFeedBackCount[i] = 0;
							if (this.GetUnitTemplet().m_listHitCriticalFeedBack[i].m_BuffStrID.Length > 1)
							{
								this.AddBuffByStrID(this.GetUnitTemplet().m_listHitCriticalFeedBack[i].m_BuffStrID, this.GetUnitTemplet().m_listHitCriticalFeedBack[i].m_BuffStatLevel, this.GetUnitTemplet().m_listHitCriticalFeedBack[i].m_BuffTimeLevel, this.GetUnitSyncData().m_GameUnitUID, true, false, false, 1);
							}
							if (this.GetUnitTemplet().m_listHitCriticalFeedBack[i].m_StateName.Length > 1 && this.CheckStateCoolTime(this.GetUnitTemplet().m_listHitCriticalFeedBack[i].m_StateName))
							{
								this.StateChange(this.GetUnitTemplet().m_listHitCriticalFeedBack[i].m_StateName, false, false);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x000A57B4 File Offset: 0x000A39B4
		public void SetHitEvadeFeedBack()
		{
			for (int i = 0; i < this.GetUnitTemplet().m_listHitEvadeFeedBack.Count; i++)
			{
				if (this.CheckEventCondition(this.GetUnitTemplet().m_listHitEvadeFeedBack[i].m_Condition))
				{
					this.GetUnitFrameData().m_listHitEvadeFeedBackCount[i] = this.GetUnitFrameData().m_listHitEvadeFeedBackCount[i] + 1;
					if (this.GetUnitFrameData().m_listHitEvadeFeedBackCount[i] >= this.GetUnitTemplet().m_listHitEvadeFeedBack[i].m_Count)
					{
						bool flag = false;
						if (this.GetUnitTemplet().m_listHitEvadeFeedBack[i].m_bStartAnyTime)
						{
							flag = true;
						}
						NKM_UNIT_STATE_TYPE nkm_UNIT_STATE_TYPE = this.GetUnitStateNow().m_NKM_UNIT_STATE_TYPE;
						if ((nkm_UNIT_STATE_TYPE == NKM_UNIT_STATE_TYPE.NUST_DAMAGE || nkm_UNIT_STATE_TYPE == NKM_UNIT_STATE_TYPE.NUST_ASTAND || nkm_UNIT_STATE_TYPE == NKM_UNIT_STATE_TYPE.NUST_MOVE || nkm_UNIT_STATE_TYPE == NKM_UNIT_STATE_TYPE.NUST_ATTACK) && this.GetUnitStateNow().m_NKM_SKILL_TYPE <= NKM_SKILL_TYPE.NST_ATTACK)
						{
							flag = true;
						}
						if (flag)
						{
							this.GetUnitFrameData().m_listHitEvadeFeedBackCount[i] = 0;
							if (this.GetUnitTemplet().m_listHitEvadeFeedBack[i].m_BuffStrID.Length > 1)
							{
								this.AddBuffByStrID(this.GetUnitTemplet().m_listHitEvadeFeedBack[i].m_BuffStrID, this.GetUnitTemplet().m_listHitEvadeFeedBack[i].m_BuffStatLevel, this.GetUnitTemplet().m_listHitEvadeFeedBack[i].m_BuffTimeLevel, this.GetUnitSyncData().m_GameUnitUID, true, false, false, 1);
							}
							if (this.GetUnitTemplet().m_listHitEvadeFeedBack[i].m_StateName.Length > 1 && this.CheckStateCoolTime(this.GetUnitTemplet().m_listHitEvadeFeedBack[i].m_StateName))
							{
								this.StateChange(this.GetUnitTemplet().m_listHitEvadeFeedBack[i].m_StateName, false, false);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x000A5984 File Offset: 0x000A3B84
		public NKMUnit GetMasterUnit()
		{
			if (this.GetUnitDataGame().m_MasterGameUnitUID == 0)
			{
				return null;
			}
			return this.m_NKMGame.GetUnit(this.GetUnitDataGame().m_MasterGameUnitUID, true, false);
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x000A59AD File Offset: 0x000A3BAD
		public short GetMasterUnitGameUID()
		{
			return this.GetUnitDataGame().m_MasterGameUnitUID;
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x000A59BA File Offset: 0x000A3BBA
		public bool IsSummonUnit()
		{
			return this.GetUnitData().m_bSummonUnit;
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x000A59C7 File Offset: 0x000A3BC7
		public bool IsChangeUnit()
		{
			return this.GetUnitDataGame().m_bChangeUnit;
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x000A59D4 File Offset: 0x000A3BD4
		public void CalcSortDist(float originPosX)
		{
			this.m_TempSortDist = Math.Abs(this.GetUnitSyncData().m_PosX - originPosX);
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x000A59F0 File Offset: 0x000A3BF0
		public List<NKMUnit> GetSortUnitListByNearDist()
		{
			if (this.m_bSortUnitDirty)
			{
				this.m_listSortUnit.Clear();
				List<NKMUnit> unitChain = this.m_NKMGame.GetUnitChain();
				for (int i = 0; i < unitChain.Count; i++)
				{
					NKMUnit nkmunit = unitChain[i];
					if (nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_ENV)
					{
						nkmunit.CalcSortDist(this.GetUnitSyncData().m_PosX);
						this.m_listSortUnit.Add(nkmunit);
					}
				}
				this.m_listSortUnit.Sort((NKMUnit a, NKMUnit b) => a.GetTempSortDist().CompareTo(b.GetTempSortDist()));
				this.m_bSortUnitDirty = false;
			}
			return this.m_listSortUnit;
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x000A5AA0 File Offset: 0x000A3CA0
		public NKMUnit GetSortUnit(bool bNearFirst, bool bExceptMyTeam = true, bool bExceptEnemyTeam = true, float fUseNearDist = 0f, float fUseFarDist = 0f, bool bExceptMe = true, bool bExceptEnv = true, bool bExceptDie = true)
		{
			List<NKMUnit> sortUnitListByNearDist = this.GetSortUnitListByNearDist();
			for (int i = 0; i < sortUnitListByNearDist.Count; i++)
			{
				NKMUnit nkmunit;
				if (bNearFirst)
				{
					nkmunit = sortUnitListByNearDist[i];
				}
				else
				{
					nkmunit = sortUnitListByNearDist[sortUnitListByNearDist.Count - 1 - i];
				}
				if (nkmunit != null && (!bExceptMe || nkmunit.GetUnitSyncData().m_GameUnitUID != this.GetUnitSyncData().m_GameUnitUID) && (!bExceptEnv || nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_ENV) && (!bExceptDie || (nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DYING && nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DIE)) && (!bExceptMyTeam || this.m_NKMGame.IsEnemy(this.GetUnitDataGame().m_NKM_TEAM_TYPE, nkmunit.GetUnitDataGame().m_NKM_TEAM_TYPE)) && (!bExceptEnemyTeam || !this.m_NKMGame.IsEnemy(this.GetUnitDataGame().m_NKM_TEAM_TYPE, nkmunit.GetUnitDataGame().m_NKM_TEAM_TYPE)) && (fUseNearDist.IsNearlyZero(1E-05f) || fUseNearDist >= Math.Abs(this.GetUnitSyncData().m_PosX - nkmunit.GetUnitSyncData().m_PosX)) && (fUseFarDist.IsNearlyZero(1E-05f) || fUseFarDist <= Math.Abs(this.GetUnitSyncData().m_PosX - nkmunit.GetUnitSyncData().m_PosX)))
				{
					return nkmunit;
				}
			}
			return null;
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x000A5BFE File Offset: 0x000A3DFE
		public void AddEventMark(NKM_UNIT_EVENT_MARK eNKM_UNIT_EVENT_MARK)
		{
			this.GetUnitSyncData().m_listNKM_UNIT_EVENT_MARK.Add((byte)eNKM_UNIT_EVENT_MARK);
			this.m_bPushSimpleSyncData = true;
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x000A5C18 File Offset: 0x000A3E18
		public virtual void OnGameEnd()
		{
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x000A5C1A File Offset: 0x000A3E1A
		public bool IsATeam()
		{
			return this.m_NKMGame.IsATeam(this.GetUnitDataGame().m_NKM_TEAM_TYPE);
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x000A5C32 File Offset: 0x000A3E32
		public bool IsBTeam()
		{
			return this.m_NKMGame.IsBTeam(this.GetUnitDataGame().m_NKM_TEAM_TYPE);
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x000A5C4A File Offset: 0x000A3E4A
		public NKM_TEAM_TYPE GetTeam()
		{
			return this.GetUnitDataGame().m_NKM_TEAM_TYPE;
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x000A5C57 File Offset: 0x000A3E57
		public NKMGameTeamData GetTeamData()
		{
			if (this.m_NKMGame == null)
			{
				return null;
			}
			if (this.m_NKMGame.GetGameData() == null)
			{
				return null;
			}
			return this.m_NKMGame.GetGameData().GetTeamData(this.GetTeam());
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x000A5C88 File Offset: 0x000A3E88
		public bool IsAlly(short gameUnitUID)
		{
			NKMUnit unit = this.m_NKMGame.GetUnit(gameUnitUID, true, true);
			return this.IsAlly(unit);
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x000A5CAC File Offset: 0x000A3EAC
		public bool IsAlly(NKMUnit other)
		{
			NKM_TEAM_TYPE nkm_TEAM_TYPE = other.m_UnitDataGame.m_NKM_TEAM_TYPE;
			return this.IsAlly(nkm_TEAM_TYPE);
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x000A5CCC File Offset: 0x000A3ECC
		public bool IsAlly(NKM_TEAM_TYPE otherTeam)
		{
			NKM_TEAM_TYPE nkm_TEAM_TYPE = this.m_UnitDataGame.m_NKM_TEAM_TYPE;
			if (nkm_TEAM_TYPE - NKM_TEAM_TYPE.NTT_A1 > 1)
			{
				return nkm_TEAM_TYPE - NKM_TEAM_TYPE.NTT_B1 <= 1 && (otherTeam == NKM_TEAM_TYPE.NTT_B1 || otherTeam == NKM_TEAM_TYPE.NTT_B2);
			}
			return otherTeam == NKM_TEAM_TYPE.NTT_A1 || otherTeam == NKM_TEAM_TYPE.NTT_A2;
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x000A5D0C File Offset: 0x000A3F0C
		public bool IsAirUnit()
		{
			if (this.GetUnitTemplet() == null)
			{
				return false;
			}
			if (this.GetUnitTemplet().m_UnitTempletBase == null)
			{
				return false;
			}
			if (this.m_UnitStateNow != null && this.m_UnitStateNow.m_bChangeIsAirUnit)
			{
				return !this.GetUnitTemplet().m_UnitTempletBase.m_bAirUnit;
			}
			return this.GetUnitTemplet().m_UnitTempletBase.m_bAirUnit;
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x000A5D6B File Offset: 0x000A3F6B
		public bool CanApplyStatus(NKM_UNIT_STATUS_EFFECT status)
		{
			return status != NKM_UNIT_STATUS_EFFECT.NUSE_NONE && !this.IsImmuneStatus(status);
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x000A5D80 File Offset: 0x000A3F80
		public bool IsImmuneStatus(NKM_UNIT_STATUS_EFFECT status)
		{
			if (status == NKM_UNIT_STATUS_EFFECT.NUSE_NONE)
			{
				return false;
			}
			if (this.m_UnitTemplet.m_listFixedStatusImmune.Contains(status))
			{
				return true;
			}
			if (this.m_UnitStateNow != null && this.m_UnitStateNow.m_listFixedStatusImmune.Contains(status))
			{
				return true;
			}
			NKMUnitStatusTemplet nkmunitStatusTemplet = NKMUnitStatusTemplet.Find(status);
			if (nkmunitStatusTemplet == null)
			{
				if (this.IsBoss())
				{
					return true;
				}
				if (this.GetUnitTemplet().m_UnitTempletBase.IsShip())
				{
					return true;
				}
			}
			else
			{
				if (!nkmunitStatusTemplet.m_bAllowBoss && this.IsBoss())
				{
					return true;
				}
				if (!nkmunitStatusTemplet.m_bAllowShip && this.GetUnitTemplet().m_UnitTempletBase.IsShip())
				{
					return true;
				}
				if (nkmunitStatusTemplet.m_bDebuff && this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_IMMUNE_NEGATIVE_STATUS))
				{
					return true;
				}
				if (nkmunitStatusTemplet.m_bCrowdControl && this.HasStatus(NKM_UNIT_STATUS_EFFECT.NUSE_IMMUNE_CC))
				{
					return true;
				}
			}
			return (status == NKM_UNIT_STATUS_EFFECT.NUSE_SLEEP && this.GetUnitTemplet().m_UnitTempletBase.HasUnitStyleType(NKM_UNIT_STYLE_TYPE.NUST_MECHANIC)) || this.m_UnitFrameData.m_hsImmuneStatus.Contains(status);
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x000A5E6C File Offset: 0x000A406C
		public bool HasStatus(NKM_UNIT_STATUS_EFFECT status)
		{
			if (status == NKM_UNIT_STATUS_EFFECT.NUSE_NONE)
			{
				return false;
			}
			if (this.m_UnitStateNow != null)
			{
				if (this.m_UnitStateNow.m_listFixedStatusImmune.Contains(status))
				{
					return false;
				}
				if (this.m_UnitStateNow.m_listFixedStatusEffect.Contains(status))
				{
					return true;
				}
			}
			return !this.m_UnitTemplet.m_listFixedStatusImmune.Contains(status) && (this.m_UnitTemplet.m_listFixedStatusEffect.Contains(status) || (!this.GetUnitFrameData().m_hsImmuneStatus.Contains(status) && this.GetUnitFrameData().m_hsStatus.Contains(status)));
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x000A5F00 File Offset: 0x000A4100
		public void ApplyStatus(NKM_UNIT_STATUS_EFFECT status)
		{
			if (!this.CanApplyStatus(status))
			{
				return;
			}
			this.GetUnitFrameData().m_hsStatus.Add(status);
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x000A5F20 File Offset: 0x000A4120
		public void ApplyStatus(HashSet<NKM_UNIT_STATUS_EFFECT> hsStatus)
		{
			if (hsStatus.Count == 0)
			{
				return;
			}
			foreach (NKM_UNIT_STATUS_EFFECT status in hsStatus)
			{
				this.ApplyStatus(status);
			}
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x000A5F78 File Offset: 0x000A4178
		public void RemoveStatus(NKM_UNIT_STATUS_EFFECT status)
		{
			this.GetUnitFrameData().m_hsStatus.Remove(status);
			bool flag = this.GetUnitFrameData().m_dicStatusTime.Remove(status);
			this.DeleteStatusBuff(status, true, true);
			if (flag && this.m_NKM_UNIT_CLASS_TYPE == NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
			{
				this.m_UnitSyncData.m_listStatusTimeData.Add(new NKMUnitStatusTimeSyncData(status, 0f));
				this.m_bPushSimpleSyncData = true;
			}
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x000A5FDE File Offset: 0x000A41DE
		public void ApplyImmuneStatus(NKM_UNIT_STATUS_EFFECT status)
		{
			this.GetUnitFrameData().m_hsImmuneStatus.Add(status);
			this.RemoveStatus(status);
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x000A5FFC File Offset: 0x000A41FC
		public void ApplyImmuneStatus(HashSet<NKM_UNIT_STATUS_EFFECT> hsStatus)
		{
			if (hsStatus.Count == 0)
			{
				return;
			}
			foreach (NKM_UNIT_STATUS_EFFECT status in hsStatus)
			{
				this.ApplyImmuneStatus(status);
			}
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x000A6054 File Offset: 0x000A4254
		public void ApplyStatusTime(NKM_UNIT_STATUS_EFFECT type, float time, NKMUnit applierUnit, bool bForceOverwrite = false, bool bServerOnly = false, bool bImmediate = false)
		{
			if (this.m_NKM_UNIT_CLASS_TYPE != NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
			{
				Log.Error("Client Apply status time!", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnit.cs", 11587);
				return;
			}
			if (this.IsImmuneStatus(type))
			{
				return;
			}
			string badStatusStateChange = this.GetBadStatusStateChange(type);
			if (!string.IsNullOrEmpty(badStatusStateChange))
			{
				this.StateChange(badStatusStateChange, true, false);
				return;
			}
			if (!this.OnApplyStatus(type, applierUnit))
			{
				return;
			}
			float num;
			if (this.m_UnitFrameData.m_dicStatusTime.TryGetValue(type, out num))
			{
				if (!bForceOverwrite && time <= num)
				{
					return;
				}
				this.m_UnitFrameData.m_dicStatusTime[type] = time;
			}
			else
			{
				this.m_UnitFrameData.m_dicStatusTime.Add(type, time);
			}
			if (bImmediate)
			{
				this.ApplyStatus(type);
			}
			if (!bServerOnly)
			{
				this.m_UnitSyncData.m_listStatusTimeData.Add(new NKMUnitStatusTimeSyncData(type, time));
				this.m_bPushSimpleSyncData = true;
			}
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x000A6120 File Offset: 0x000A4320
		public int DispelStatusTime(bool bDebuff, int count = 999)
		{
			if (this.m_NKM_UNIT_CLASS_TYPE != NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
			{
				return 0;
			}
			this.m_lstTempStatus.Clear();
			int num = 0;
			foreach (NKM_UNIT_STATUS_EFFECT nkm_UNIT_STATUS_EFFECT in this.m_UnitFrameData.m_dicStatusTime.Keys)
			{
				if (count - num <= 0)
				{
					break;
				}
				NKMUnitStatusTemplet nkmunitStatusTemplet = NKMUnitStatusTemplet.Find(nkm_UNIT_STATUS_EFFECT);
				if (nkmunitStatusTemplet.m_bDispel && nkmunitStatusTemplet.m_bDebuff == bDebuff)
				{
					this.m_lstTempStatus.Add(nkm_UNIT_STATUS_EFFECT);
					num++;
				}
			}
			if (num > 0)
			{
				foreach (NKM_UNIT_STATUS_EFFECT nkm_UNIT_STATUS_EFFECT2 in this.m_lstTempStatus)
				{
					this.m_UnitFrameData.m_dicStatusTime[nkm_UNIT_STATUS_EFFECT2] = 0f;
					this.m_UnitSyncData.m_listStatusTimeData.Add(new NKMUnitStatusTimeSyncData(nkm_UNIT_STATUS_EFFECT2, 0f));
				}
				this.m_bPushSimpleSyncData = true;
			}
			return num;
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x000A623C File Offset: 0x000A443C
		private bool OnApplyStatus(HashSet<NKM_UNIT_STATUS_EFFECT> hsType, NKMUnit applierUnit)
		{
			bool flag = true;
			foreach (NKM_UNIT_STATUS_EFFECT type in hsType)
			{
				flag &= this.OnApplyStatus(type, applierUnit);
			}
			return flag;
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x000A6294 File Offset: 0x000A4494
		private bool OnApplyStatus(NKM_UNIT_STATUS_EFFECT type, NKMUnit applierUnit)
		{
			if (type == NKM_UNIT_STATUS_EFFECT.NUSE_CONFUSE)
			{
				if (applierUnit == null)
				{
					return false;
				}
				if (this.IsImmuneStatus(NKM_UNIT_STATUS_EFFECT.NUSE_CONFUSE))
				{
					return false;
				}
				if (this.IsBoss())
				{
					return false;
				}
				if (this.m_UnitDataGame.m_NKM_TEAM_TYPE != this.m_UnitDataGame.m_NKM_TEAM_TYPE_ORG && this.m_NKMGame.IsSameTeam(this.m_UnitDataGame.m_NKM_TEAM_TYPE_ORG, applierUnit.GetTeam()))
				{
					this.RemoveStatus(NKM_UNIT_STATUS_EFFECT.NUSE_CONFUSE);
					this.m_UnitDataGame.m_NKM_TEAM_TYPE = this.m_UnitDataGame.m_NKM_TEAM_TYPE_ORG;
					return false;
				}
				this.m_UnitDataGame.m_NKM_TEAM_TYPE = applierUnit.GetTeam();
			}
			return true;
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x000A632C File Offset: 0x000A452C
		public string GetBadStatusStateChange(HashSet<NKM_UNIT_STATUS_EFFECT> hsStateType)
		{
			foreach (NKM_UNIT_STATUS_EFFECT stateType in hsStateType)
			{
				string badStatusStateChange = this.GetBadStatusStateChange(stateType);
				if (badStatusStateChange != null)
				{
					return badStatusStateChange;
				}
			}
			return null;
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x000A6388 File Offset: 0x000A4588
		public string GetBadStatusStateChange(NKM_UNIT_STATUS_EFFECT stateType)
		{
			string text;
			switch (stateType)
			{
			case NKM_UNIT_STATUS_EFFECT.NUSE_STUN:
				text = this.GetUnitTemplet().m_StateChangeStun;
				break;
			case NKM_UNIT_STATUS_EFFECT.NUSE_SLEEP:
				text = this.GetUnitTemplet().m_StateChangeSleep;
				break;
			case NKM_UNIT_STATUS_EFFECT.NUSE_SILENCE:
				text = this.GetUnitTemplet().m_StateChangeSilence;
				break;
			default:
				if (stateType != NKM_UNIT_STATUS_EFFECT.NUSE_CONFUSE)
				{
					return null;
				}
				text = this.GetUnitTemplet().m_StateChangeConfuse;
				break;
			}
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x000A63F8 File Offset: 0x000A45F8
		public bool HasCrowdControlStatus()
		{
			using (HashSet<NKM_UNIT_STATUS_EFFECT>.Enumerator enumerator = this.GetUnitFrameData().m_hsStatus.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (NKMUnitStatusTemplet.IsCrowdControlStatus(enumerator.Current))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0400210B RID: 8459
		public float m_RespawnTime;

		// Token: 0x0400210C RID: 8460
		protected NKM_UNIT_CLASS_TYPE m_NKM_UNIT_CLASS_TYPE;

		// Token: 0x0400210D RID: 8461
		protected NKMGame m_NKMGame;

		// Token: 0x0400210E RID: 8462
		protected NKMUnitData m_UnitData;

		// Token: 0x0400210F RID: 8463
		protected NKMUnitDataGame m_UnitDataGame = new NKMUnitDataGame();

		// Token: 0x04002110 RID: 8464
		protected NKMUnitTemplet m_UnitTemplet;

		// Token: 0x04002111 RID: 8465
		protected NKMUnitSyncData m_UnitSyncData = new NKMUnitSyncData();

		// Token: 0x04002112 RID: 8466
		protected NKMUnitFrameData m_UnitFrameData = new NKMUnitFrameData();

		// Token: 0x04002113 RID: 8467
		protected NKMUnitState m_UnitStateBefore;

		// Token: 0x04002114 RID: 8468
		protected NKMUnitState m_UnitStateNow;

		// Token: 0x04002115 RID: 8469
		protected NKMUnit m_TargetUnit;

		// Token: 0x04002116 RID: 8470
		protected NKMUnit m_SubTargetUnit;

		// Token: 0x04002117 RID: 8471
		protected string m_StateNameNow = "";

		// Token: 0x04002118 RID: 8472
		protected string m_StateNameNext = "";

		// Token: 0x04002119 RID: 8473
		protected string m_StateNameNextChange = "";

		// Token: 0x0400211A RID: 8474
		protected List<NKMDamageInst> m_listDamageInstAtk = new List<NKMDamageInst>();

		// Token: 0x0400211B RID: 8475
		protected Dictionary<float, NKMTimeStamp> m_EventTimeStampAnim = new Dictionary<float, NKMTimeStamp>();

		// Token: 0x0400211C RID: 8476
		protected Dictionary<float, NKMTimeStamp> m_EventTimeStampState = new Dictionary<float, NKMTimeStamp>();

		// Token: 0x0400211D RID: 8477
		protected Dictionary<int, NKMStateCoolTime> m_dicStateCoolTime = new Dictionary<int, NKMStateCoolTime>();

		// Token: 0x0400211E RID: 8478
		protected Dictionary<int, float> m_dicStateMaxCoolTime = new Dictionary<int, float>();

		// Token: 0x0400211F RID: 8479
		protected LinkedList<NKMDamageEffect> m_linklistDamageEffect = new LinkedList<NKMDamageEffect>();

		// Token: 0x04002120 RID: 8480
		protected List<NKMShipSkillTemplet> m_listShipSkillTemplet = new List<NKMShipSkillTemplet>();

		// Token: 0x04002121 RID: 8481
		protected List<int> m_listShipSkillStateID = new List<int>();

		// Token: 0x04002122 RID: 8482
		protected float m_fUnitCollisonCheckTime;

		// Token: 0x04002123 RID: 8483
		public NKMTrackingFloat m_EventMovePosX = new NKMTrackingFloat();

		// Token: 0x04002124 RID: 8484
		public NKMTrackingFloat m_EventMovePosZ = new NKMTrackingFloat();

		// Token: 0x04002125 RID: 8485
		public NKMTrackingFloat m_EventMovePosJumpY = new NKMTrackingFloat();

		// Token: 0x04002126 RID: 8486
		public float m_fEventMoveSavedPositionX;

		// Token: 0x04002127 RID: 8487
		protected float m_DeltaTime;

		// Token: 0x04002128 RID: 8488
		protected float m_fPracticeHPReset = 3f;

		// Token: 0x04002129 RID: 8489
		protected float m_fCheckUseShipSkillAuto;

		// Token: 0x0400212A RID: 8490
		protected int m_TargetUIDOrg;

		// Token: 0x0400212B RID: 8491
		protected int m_SubTargetUIDOrg;

		// Token: 0x0400212C RID: 8492
		protected bool m_bRightOrg;

		// Token: 0x0400212D RID: 8493
		protected bool m_PushSyncData;

		// Token: 0x0400212E RID: 8494
		protected bool m_bPushSimpleSyncData;

		// Token: 0x0400212F RID: 8495
		protected float m_fSyncRollbackTime;

		// Token: 0x04002130 RID: 8496
		protected bool m_bConsumeRollback;

		// Token: 0x04002131 RID: 8497
		protected NKMVector3 m_NKMVector3Temp = new NKMVector3(0f, 0f, 0f);

		// Token: 0x04002132 RID: 8498
		protected float m_BuffProcessTime;

		// Token: 0x04002133 RID: 8499
		protected float m_BuffDamageTime;

		// Token: 0x04002134 RID: 8500
		protected List<short> m_listBuffDelete = new List<short>();

		// Token: 0x04002135 RID: 8501
		protected List<NKMBuffCreateData> m_listBuffAdd = new List<NKMBuffCreateData>();

		// Token: 0x04002136 RID: 8502
		protected List<NKMBuffData> m_listBuffDieEvent = new List<NKMBuffData>();

		// Token: 0x04002137 RID: 8503
		protected bool m_bBuffChangedThisFrame;

		// Token: 0x04002138 RID: 8504
		protected bool m_bBuffUnitLevelChangedThisFrame;

		// Token: 0x04002139 RID: 8505
		protected bool m_bBuffHPRateConserveRequired;

		// Token: 0x0400213A RID: 8506
		protected Dictionary<NKMEventRespawn, List<short>> m_dicDynamicRespawnPool = new Dictionary<NKMEventRespawn, List<short>>();

		// Token: 0x0400213B RID: 8507
		protected Dictionary<string, List<short>> m_dicUnitChangeRespawnPool = new Dictionary<string, List<short>>();

		// Token: 0x0400213C RID: 8508
		protected Dictionary<short, List<NKMKillFeedBack>> m_dicKillFeedBackGameUnitUID = new Dictionary<short, List<NKMKillFeedBack>>();

		// Token: 0x0400213D RID: 8509
		protected List<NKMStaticBuffDataRuntime> m_listNKMStaticBuffDataRuntime = new List<NKMStaticBuffDataRuntime>();

		// Token: 0x0400213E RID: 8510
		protected Dictionary<short, int> m_listDamageResistUnit = new Dictionary<short, int>();

		// Token: 0x0400213F RID: 8511
		protected float m_fSortUnitDirtyCheckTime;

		// Token: 0x04002140 RID: 8512
		protected bool m_bSortUnitDirty = true;

		// Token: 0x04002141 RID: 8513
		public int m_usedRespawnCost;

		// Token: 0x04002142 RID: 8514
		protected float m_TempSortDist;

		// Token: 0x04002143 RID: 8515
		protected List<NKMUnit> m_listSortUnit = new List<NKMUnit>();

		// Token: 0x04002144 RID: 8516
		protected List<int> m_listAttackSelectTemp = new List<int>();

		// Token: 0x04002145 RID: 8517
		protected string beforeStateName1 = "";

		// Token: 0x04002146 RID: 8518
		protected string beforeStateName2 = "";

		// Token: 0x04002147 RID: 8519
		protected string beforeStateName3 = "";

		// Token: 0x04002148 RID: 8520
		protected List<short> m_listBuffToDelete = new List<short>();

		// Token: 0x04002149 RID: 8521
		protected List<NKM_UNIT_STATUS_EFFECT> m_lstTempStatus = new List<NKM_UNIT_STATUS_EFFECT>();

		// Token: 0x0400214A RID: 8522
		protected bool m_bBoss;

		// Token: 0x0400214B RID: 8523
		private NKMUnit.StateChangeEvent dStateChangeEvent;

		// Token: 0x0200120F RID: 4623
		// (Invoke) Token: 0x0600A19D RID: 41373
		public delegate void StateChangeEvent(NKM_UNIT_STATE_CHANGE_TYPE stateChangeType, NKMUnit unit, NKMUnitState unitState);
	}
}
