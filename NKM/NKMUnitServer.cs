using System;
using System.Collections.Generic;
using Cs.Logging;
using Cs.Math;

namespace NKM
{
	// Token: 0x020004B2 RID: 1202
	public class NKMUnitServer : NKMUnit
	{
		// Token: 0x06002172 RID: 8562 RVA: 0x000AAE94 File Offset: 0x000A9094
		public NKMUnitServer()
		{
			this.m_NKM_UNIT_CLASS_TYPE = NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER;
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x000AAEA4 File Offset: 0x000A90A4
		public override bool LoadUnit(NKMGame cNKMGame, NKMUnitData cNKMUnitData, short masterGameUnitUID, short gameUnitUID, float fNearTargetRange, NKM_TEAM_TYPE eNKM_TEAM_TYPE, bool bSub, bool bAsync)
		{
			if (!base.LoadUnit(cNKMGame, cNKMUnitData, masterGameUnitUID, gameUnitUID, fNearTargetRange, eNKM_TEAM_TYPE, bSub, bAsync))
			{
				return false;
			}
			this.m_NKMGameServerHost = (NKMGameServerHost)cNKMGame;
			return true;
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x000AAED5 File Offset: 0x000A90D5
		public override bool SetDie(bool bCheckAllDie = true)
		{
			bool flag = base.SetDie(bCheckAllDie);
			if (flag && this.m_NKM_UNIT_CLASS_TYPE == NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
			{
				this.m_NKMGameServerHost.SyncDieUnit(base.GetUnitDataGame().m_GameUnitUID);
			}
			return flag;
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x000AAF00 File Offset: 0x000A9100
		public override void SetDying(bool bForce = false, bool bUnitChange = false)
		{
			if (!base.IsDyingOrDie())
			{
				this.m_NKMGame.m_GameRecord.AddPlayTime(this, this.m_RespawnTime - this.m_NKMGame.GetGameRuntimeData().m_fRemainGameTime);
				this.m_NKMGame.m_GameRecord.AddDieCount(this);
			}
			base.SetDying(bForce, bUnitChange);
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x000AAF58 File Offset: 0x000A9158
		public override void RespawnUnit(float fPosX, float fPosZ, float fJumpYPos, bool bUseRight = false, bool bRight = true, float fInitHP = 0f, bool bInitHPRate = false, float rollbackTime = 0f)
		{
			base.RespawnUnit(fPosX, fPosZ, fJumpYPos, bUseRight, bRight, fInitHP, bInitHPRate, rollbackTime);
			base.StateChangeToSTART(true, true);
			this.m_NKMGame.m_GameRecord.AddSummonCount(this);
			this.m_RespawnTime = this.m_NKMGame.GetGameRuntimeData().m_fRemainGameTime;
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x000AAFA7 File Offset: 0x000A91A7
		protected override void StateStart()
		{
			base.StateStart();
			if (this.m_fSyncRollbackTime > 0f)
			{
				this.ProcessRollbackEvents();
			}
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x000AAFC2 File Offset: 0x000A91C2
		public override void OnGameEnd()
		{
			if (base.IsDyingOrDie())
			{
				return;
			}
			this.m_NKMGame.m_GameRecord.AddPlayTime(this, this.m_RespawnTime - this.m_NKMGame.GetGameRuntimeData().m_fRemainGameTime);
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x000AAFF5 File Offset: 0x000A91F5
		public override void PushSyncData()
		{
			if (this.m_UnitFrameData.m_bSyncShipSkill)
			{
				this.m_NKMGameServerHost.SyncShipSkillSync(this);
				this.m_UnitFrameData.m_bSyncShipSkill = false;
			}
			else
			{
				this.m_NKMGameServerHost.SyncUnitSync(this, null);
			}
			base.PushSyncData();
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x000AB031 File Offset: 0x000A9231
		protected override void PushSimpleSyncData()
		{
			this.m_NKMGameServerHost.SyncUnitSimpleSync(this);
			base.PushSyncData();
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x000AB048 File Offset: 0x000A9248
		protected void ProcessRollbackEvents()
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			this.ProcessEventSpeedAndEventMoveRollback(this.m_fSyncRollbackTime);
			if (this.m_NKMGame.GetDungeonTemplet() == null || !this.m_NKMGame.GetDungeonTemplet().m_bNoTimeStop)
			{
				this.ProcessEventRollback<NKMEventStopTime>(this.m_UnitStateNow.m_listNKMEventStopTime);
			}
			this.ProcessEventRollback<NKMEventInvincibleGlobal>(this.m_UnitStateNow.m_listNKMEventInvincibleGlobal);
			this.ProcessEventDamageEffectRollback();
			if (this.m_linklistDamageEffect != null && this.m_linklistDamageEffect.First != null)
			{
				this.ProcessEventRollback<NKMEventDEStateChange>(this.m_UnitStateNow.m_listNKMEventDEStateChange);
			}
			this.ProcessEventBuffRollBack(this.m_fSyncRollbackTime);
			this.ProcessEventRollback<NKMEventStatus>(this.m_UnitStateNow.m_listNKMEventStatus);
			this.ProcessEventRespawnRollback(this.m_fSyncRollbackTime);
			this.ProcessEventRollback<NKMEventAgro>(this.m_UnitStateNow.m_listNKMEventAgro);
			this.ProcessEventRollback<NKMEventHeal>(this.m_UnitStateNow.m_listNKMEventHeal);
			this.ProcessEventRollback<NKMEventCost>(this.m_UnitStateNow.m_listNKMEventCost);
			this.ProcessEventRollback<NKMEventDispel>(this.m_UnitStateNow.m_listNKMEventDispel);
			this.ProcessEventRollback<NKMEventStun>(this.m_UnitStateNow.m_listNKMEventStun);
			this.ProcessEventRollback<NKMEventChangeCooltime>(this.m_UnitStateNow.m_listNKMEventChangeCooltime);
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x000AB168 File Offset: 0x000A9368
		private void ProcessEventRespawnRollback(float rollbackTIme)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventRespawn.Count; i++)
			{
				NKMEventRespawn nkmeventRespawn = this.m_UnitStateNow.m_listNKMEventRespawn[i];
				if (nkmeventRespawn != null && !nkmeventRespawn.m_bStateEndTime && base.RollbackEventTimer(nkmeventRespawn.m_bAnimTime, nkmeventRespawn.m_fEventTime) && base.CheckEventCondition(nkmeventRespawn.m_Condition))
				{
					float eventStateTime = base.GetEventStateTime(nkmeventRespawn.m_bAnimTime, nkmeventRespawn.m_fEventTime);
					float rollbackTime = rollbackTIme - eventStateTime;
					if (this.m_NKM_UNIT_CLASS_TYPE == NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
					{
						this.ProcessEventRespawn(nkmeventRespawn, base.GetUnitSyncData().m_PosX, rollbackTime);
					}
				}
			}
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x000AB210 File Offset: 0x000A9410
		protected void ProcessEventSpeedAndEventMoveRollback(float rollbackTime)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			if (this.m_UnitStateNow.m_bNoMove || this.m_UnitTemplet.m_bNoMove)
			{
				return;
			}
			int num = this.m_UnitStateNow.m_listNKMEventSpeed.Count + this.m_UnitStateNow.m_listNKMEventSpeedX.Count + this.m_UnitStateNow.m_listNKMEventSpeedY.Count + this.m_UnitStateNow.m_listNKMEventMove.Count;
			if (num == 0)
			{
				return;
			}
			List<INKMUnitStateEventRollback> list = new List<INKMUnitStateEventRollback>(num);
			list.AddRange(this.m_UnitStateNow.m_listNKMEventSpeed);
			list.AddRange(this.m_UnitStateNow.m_listNKMEventSpeedX);
			list.AddRange(this.m_UnitStateNow.m_listNKMEventSpeedY);
			list.AddRange(this.m_UnitStateNow.m_listNKMEventMove);
			list.Sort(delegate(INKMUnitStateEventRollback x, INKMUnitStateEventRollback y)
			{
				int num2 = x.EventStartTime.CompareTo(y.EventStartTime);
				if (num2 != 0)
				{
					return num2;
				}
				bool value = x is NKMEventMove;
				return (y is NKMEventMove).CompareTo(value);
			});
			for (int i = 0; i < list.Count; i++)
			{
				INKMUnitStateEventRollback inkmunitStateEventRollback = list[i];
				if (!inkmunitStateEventRollback.bStateEnd && base.RollbackEventTimer(inkmunitStateEventRollback.bAnimTime, inkmunitStateEventRollback.EventStartTime) && base.CheckEventCondition(inkmunitStateEventRollback.Condition))
				{
					inkmunitStateEventRollback.ProcessEventRollback(this.m_NKMGame, this, rollbackTime);
				}
			}
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x000AB34C File Offset: 0x000A954C
		private void ProcessEventDamageEffectRollback()
		{
			if (this.m_UnitStateNow.m_listNKMEventDamageEffect.Count == 0)
			{
				return;
			}
			NKMUnitSkillTemplet stateSkill = base.GetStateSkill(this.m_UnitStateNow);
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventDamageEffect.Count; i++)
			{
				NKMEventDamageEffect nkmeventDamageEffect = this.m_UnitStateNow.m_listNKMEventDamageEffect[i];
				if (nkmeventDamageEffect != null && !nkmeventDamageEffect.m_bStateEndTime && base.RollbackEventTimer(nkmeventDamageEffect.m_bAnimTime, nkmeventDamageEffect.m_fEventTime) && base.CheckEventCondition(nkmeventDamageEffect.m_Condition) && (!nkmeventDamageEffect.m_bIgnoreNoTarget || this.m_TargetUnit != null))
				{
					base.ProcessEventDamageEffect(nkmeventDamageEffect, stateSkill, this.m_UnitSyncData.m_PosX, this.m_UnitSyncData.m_JumpYPos, this.m_UnitSyncData.m_PosZ);
				}
			}
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x000AB410 File Offset: 0x000A9610
		private void ProcessEventBuffRollBack(float rollbackTime)
		{
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventBuff.Count; i++)
			{
				NKMEventBuff nkmeventBuff = this.m_UnitStateNow.m_listNKMEventBuff[i];
				if (nkmeventBuff != null && !nkmeventBuff.m_bReflection && !nkmeventBuff.m_bStateEndTime && base.RollbackEventTimer(nkmeventBuff.m_bAnimTime, nkmeventBuff.m_fEventTime) && base.CheckEventCondition(nkmeventBuff.m_Condition))
				{
					nkmeventBuff.Process(this.m_NKMGame, this);
				}
			}
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x000AB48C File Offset: 0x000A968C
		private void ProcessEventRollback<T>(List<T> lstEvent) where T : INKMUnitStateEventRollback
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			for (int i = 0; i < lstEvent.Count; i++)
			{
				INKMUnitStateEventRollback inkmunitStateEventRollback = lstEvent[i];
				if (inkmunitStateEventRollback != null && !inkmunitStateEventRollback.bStateEnd && base.RollbackEventTimer(inkmunitStateEventRollback.bAnimTime, inkmunitStateEventRollback.EventStartTime) && base.CheckEventCondition(inkmunitStateEventRollback.Condition))
				{
					inkmunitStateEventRollback.ProcessEventRollback(this.m_NKMGame, this, this.m_fSyncRollbackTime);
				}
			}
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x000AB500 File Offset: 0x000A9700
		protected override void ProcessEventRespawn(bool bStateEnd = false)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			for (int i = 0; i < this.m_UnitStateNow.m_listNKMEventRespawn.Count; i++)
			{
				NKMEventRespawn nkmeventRespawn = this.m_UnitStateNow.m_listNKMEventRespawn[i];
				if (nkmeventRespawn != null && base.CheckEventCondition(nkmeventRespawn.m_Condition))
				{
					bool flag = false;
					if (nkmeventRespawn.m_bStateEndTime && bStateEnd)
					{
						flag = true;
					}
					else if (base.EventTimer(nkmeventRespawn.m_bAnimTime, nkmeventRespawn.m_fEventTime, true) && !nkmeventRespawn.m_bStateEndTime)
					{
						flag = true;
					}
					if (flag && this.m_NKM_UNIT_CLASS_TYPE == NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
					{
						this.ProcessEventRespawn(nkmeventRespawn, base.GetUnitSyncData().m_PosX, 0f);
					}
				}
			}
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x000AB5AC File Offset: 0x000A97AC
		protected override bool ProcessEventRespawn(NKMEventRespawn cNKMEventRespawn, float fRespawnPosX, float rollbackTime = 0f)
		{
			if (base.GetUnitFrameData().m_bNotCastSummon && !base.IsBoss())
			{
				return false;
			}
			short num = base.GetDynamicRespawnPool(cNKMEventRespawn);
			bool bMaxCountReRespawn = false;
			if (num <= 0 && cNKMEventRespawn.m_bMaxCountReRespawn)
			{
				bMaxCountReRespawn = true;
				num = base.GetDynamicRespawnPoolReRespawn(cNKMEventRespawn);
			}
			if (cNKMEventRespawn.m_bShipSkillPos)
			{
				fRespawnPosX = this.m_UnitFrameData.m_fShipSkillPosX;
			}
			if (base.GetUnitSyncData().m_bRight)
			{
				fRespawnPosX += cNKMEventRespawn.m_fOffsetX;
			}
			else
			{
				fRespawnPosX -= cNKMEventRespawn.m_fOffsetX;
			}
			float respawnPosZ = this.m_NKMGameServerHost.GetRespawnPosZ(-1f, -1f);
			NKMUnitTemplet unitTemplet = NKMUnitManager.GetUnitTemplet(cNKMEventRespawn.m_UnitStrID);
			if (unitTemplet == null)
			{
				Log.Error("Can not found UnitTemplet. UnitStrId:" + cNKMEventRespawn.m_UnitStrID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitServer.cs", 462);
				return false;
			}
			if (!unitTemplet.m_fForceRespawnZposMin.IsNearlyEqual(-1f, 1E-05f) || !unitTemplet.m_fForceRespawnZposMax.IsNearlyEqual(-1f, 1E-05f))
			{
				respawnPosZ = this.m_NKMGameServerHost.GetRespawnPosZ(unitTemplet.m_fForceRespawnZposMin, unitTemplet.m_fForceRespawnZposMax);
			}
			if (this.m_NKMGameServerHost.DynamicRespawnUnitReserve(bMaxCountReRespawn, num, fRespawnPosX, respawnPosZ, 0f, false, true, 0f, cNKMEventRespawn.m_RespawnState, rollbackTime) != null)
			{
				base.AddDamage(false, base.GetMaxHP(1f) * cNKMEventRespawn.m_ReduceHPRate, NKM_DAMAGE_RESULT_TYPE.NDRT_NO_MARK, base.GetUnitDataGame().m_GameUnitUID, base.GetUnitDataGame().m_NKM_TEAM_TYPE, false, false, false);
			}
			return true;
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x000AB70C File Offset: 0x000A990C
		protected override void ProcessEventUnitChange(bool bStateEnd = false)
		{
			if (this.m_UnitStateNow == null)
			{
				return;
			}
			if (this.m_UnitStateNow.m_NKMEventUnitChange != null)
			{
				if (!base.CheckEventCondition(this.m_UnitStateNow.m_NKMEventUnitChange.m_Condition))
				{
					return;
				}
				bool flag = false;
				if (this.m_UnitStateNow.m_NKMEventUnitChange.m_bStateEndTime && bStateEnd)
				{
					flag = true;
				}
				else if (base.EventTimer(this.m_UnitStateNow.m_NKMEventUnitChange.m_bAnimTime, this.m_UnitStateNow.m_NKMEventUnitChange.m_fEventTime, true) && !this.m_UnitStateNow.m_NKMEventUnitChange.m_bStateEndTime)
				{
					flag = true;
				}
				if (flag && this.m_NKM_UNIT_CLASS_TYPE == NKM_UNIT_CLASS_TYPE.NCT_UNIT_SERVER)
				{
					short unitChangeRespawnPool = base.GetUnitChangeRespawnPool(this.m_UnitStateNow.m_NKMEventUnitChange.m_UnitStrID);
					NKMUnit nkmunit = this.m_NKMGameServerHost.DynamicRespawnUnitReserve(false, unitChangeRespawnPool, base.GetUnitSyncData().m_PosX, base.GetUnitSyncData().m_PosZ, base.GetUnitSyncData().m_JumpYPos, true, base.GetUnitSyncData().m_bRight, base.GetHPRate(), null, 0f);
					if (nkmunit != null)
					{
						nkmunit.GetUnitData().SetDungeonRespawnUnitTemplet(base.GetUnitData().m_DungeonRespawnUnitTemplet);
					}
				}
			}
			base.ProcessEventUnitChange(bStateEnd);
		}

		// Token: 0x04002244 RID: 8772
		private NKMGameServerHost m_NKMGameServerHost;
	}
}
