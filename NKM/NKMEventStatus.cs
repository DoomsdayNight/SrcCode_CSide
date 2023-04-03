using System;
using System.Collections.Generic;
using Cs.Math;
using NKM.Templet;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004ED RID: 1261
	public class NKMEventStatus : IEventConditionOwner, INKMUnitStateEventRollback, INKMUnitStateEvent
	{
		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06002390 RID: 9104 RVA: 0x000B755E File Offset: 0x000B575E
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06002391 RID: 9105 RVA: 0x000B7566 File Offset: 0x000B5766
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06002392 RID: 9106 RVA: 0x000B756E File Offset: 0x000B576E
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06002393 RID: 9107 RVA: 0x000B7571 File Offset: 0x000B5771
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06002394 RID: 9108 RVA: 0x000B7579 File Offset: 0x000B5779
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x000B759B File Offset: 0x000B579B
		public bool Validate()
		{
			return this.m_StatusType != NKM_UNIT_STATUS_EFFECT.NUSE_NONE && this.m_fStatusTime > 0f;
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x000B75B8 File Offset: 0x000B57B8
		public void DeepCopyFromSource(NKMEventStatus source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_StatusType = source.m_StatusType;
			this.m_bRemove = source.m_bRemove;
			this.m_fStatusTime = source.m_fStatusTime;
			this.m_fRange = source.m_fRange;
			this.m_bMyTeam = source.m_bMyTeam;
			this.m_bEnemy = source.m_bEnemy;
			this.m_ApplyCount = source.m_ApplyCount;
			this.m_fMinTargetHP = source.m_fMinTargetHP;
			this.m_fMaxTargetHP = source.m_fMaxTargetHP;
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x000B7668 File Offset: 0x000B5868
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			if (cNKMLua.OpenTable("m_Condition"))
			{
				this.m_Condition.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_bAnimTime", ref this.m_bAnimTime);
			cNKMLua.GetData("m_fEventTime", ref this.m_fEventTime);
			cNKMLua.GetData("m_bStateEndTime", ref this.m_bStateEndTime);
			cNKMLua.GetData<NKM_UNIT_STATUS_EFFECT>("m_StatusType", ref this.m_StatusType);
			cNKMLua.GetData("m_bRemove", ref this.m_bRemove);
			cNKMLua.GetData("m_fStatusTime", ref this.m_fStatusTime);
			cNKMLua.GetData("m_fRange", ref this.m_fRange);
			cNKMLua.GetData("m_bMyTeam", ref this.m_bMyTeam);
			cNKMLua.GetData("m_bEnemy", ref this.m_bEnemy);
			cNKMLua.GetData("m_ApplyCount", ref this.m_ApplyCount);
			cNKMLua.GetData("m_fMinTargetHP", ref this.m_fMinTargetHP);
			cNKMLua.GetData("m_fMaxTargetHP", ref this.m_fMaxTargetHP);
			return true;
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x000B7770 File Offset: 0x000B5970
		public void ProcessEvent(NKMGame cNKMGame, NKMUnit cNKMUnit, bool bStateEnd)
		{
			if (!cNKMUnit.CheckEventCondition(this.m_Condition))
			{
				return;
			}
			if (bStateEnd)
			{
				if (!this.m_bStateEndTime)
				{
					return;
				}
			}
			else
			{
				if (this.m_bStateEndTime)
				{
					return;
				}
				if (!cNKMUnit.EventTimer(this.m_bAnimTime, this.m_fEventTime, true))
				{
					return;
				}
			}
			if (!this.m_fRange.IsNearlyZero(1E-05f))
			{
				List<NKMUnit> sortUnitListByNearDist = cNKMUnit.GetSortUnitListByNearDist();
				this.ProcessRangeStatus(cNKMGame, cNKMUnit, cNKMUnit.GetUnitSyncData().m_PosX, sortUnitListByNearDist);
				return;
			}
			if (this.m_fMinTargetHP > cNKMUnit.GetHPRate())
			{
				return;
			}
			if (this.m_fMaxTargetHP > 0f && this.m_fMaxTargetHP <= cNKMUnit.GetHPRate())
			{
				return;
			}
			if (this.m_bRemove)
			{
				cNKMUnit.RemoveStatus(this.m_StatusType);
				return;
			}
			cNKMUnit.ApplyStatusTime(this.m_StatusType, this.m_fStatusTime, cNKMUnit, false, false, false);
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x000B783C File Offset: 0x000B5A3C
		public void ProcessEvent(NKMGame cNKMGame, NKMDamageEffect cNKMDamageEffect, bool bStateEnd)
		{
			if (!cNKMDamageEffect.CheckEventCondition(this.m_Condition))
			{
				return;
			}
			if (bStateEnd)
			{
				if (!this.m_bStateEndTime)
				{
					return;
				}
			}
			else
			{
				if (this.m_bStateEndTime)
				{
					return;
				}
				if (!cNKMDamageEffect.EventTimer(this.m_bAnimTime, this.m_fEventTime, true))
				{
					return;
				}
			}
			List<NKMUnit> sortUnitListByNearDist = cNKMDamageEffect.GetSortUnitListByNearDist();
			NKMUnit masterUnit = cNKMDamageEffect.GetMasterUnit();
			this.ProcessRangeStatus(cNKMGame, masterUnit, cNKMDamageEffect.GetDEData().m_PosX, sortUnitListByNearDist);
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x000B78A8 File Offset: 0x000B5AA8
		private void ProcessRangeStatus(NKMGame cNKMGame, NKMUnit cNKMUnit, float posX, List<NKMUnit> cSortedUnitList)
		{
			int num = 0;
			for (int i = 0; i < cSortedUnitList.Count; i++)
			{
				NKMUnit nkmunit = cSortedUnitList[i];
				if (nkmunit.GetUnitSyncData().m_GameUnitUID != cNKMUnit.GetUnitSyncData().m_GameUnitUID && nkmunit.GetUnitTemplet().m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_ENV && nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DYING && nkmunit.GetUnitSyncData().m_NKM_UNIT_PLAY_STATE != NKM_UNIT_PLAY_STATE.NUPS_DIE && (this.m_bMyTeam || cNKMGame.IsEnemy(cNKMUnit.GetUnitDataGame().m_NKM_TEAM_TYPE, nkmunit.GetUnitDataGame().m_NKM_TEAM_TYPE)) && (this.m_bEnemy || !cNKMGame.IsEnemy(cNKMUnit.GetUnitDataGame().m_NKM_TEAM_TYPE, nkmunit.GetUnitDataGame().m_NKM_TEAM_TYPE)))
				{
					if (this.m_fRange < Math.Abs(posX - nkmunit.GetUnitSyncData().m_PosX) || this.m_fMinTargetHP > nkmunit.GetHPRate() || (this.m_fMaxTargetHP > 0f && this.m_fMaxTargetHP <= nkmunit.GetHPRate()))
					{
						break;
					}
					num++;
					if (this.m_bRemove)
					{
						nkmunit.RemoveStatus(this.m_StatusType);
					}
					else
					{
						nkmunit.ApplyStatusTime(this.m_StatusType, this.m_fStatusTime, cNKMUnit, false, false, false);
					}
					if (this.m_ApplyCount > 0 && num >= this.m_ApplyCount)
					{
						return;
					}
				}
			}
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x000B7A04 File Offset: 0x000B5C04
		public void ProcessEventRollback(NKMGame cNKMGame, NKMUnit cNKMUnit, float rollbackTime)
		{
			if (!this.m_fRange.IsNearlyZero(1E-05f))
			{
				List<NKMUnit> sortUnitListByNearDist = cNKMUnit.GetSortUnitListByNearDist();
				this.ProcessRangeStatus(cNKMGame, cNKMUnit, cNKMUnit.GetUnitSyncData().m_PosX, sortUnitListByNearDist);
				return;
			}
			if (this.m_fMinTargetHP > cNKMUnit.GetHPRate())
			{
				return;
			}
			if (this.m_fMaxTargetHP > 0f && this.m_fMaxTargetHP <= cNKMUnit.GetHPRate())
			{
				return;
			}
			if (this.m_bRemove)
			{
				cNKMUnit.RemoveStatus(this.m_StatusType);
				return;
			}
			cNKMUnit.ApplyStatusTime(this.m_StatusType, this.m_fStatusTime, cNKMUnit, false, false, false);
		}

		// Token: 0x04002521 RID: 9505
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x04002522 RID: 9506
		public bool m_bAnimTime = true;

		// Token: 0x04002523 RID: 9507
		public float m_fEventTime;

		// Token: 0x04002524 RID: 9508
		public bool m_bStateEndTime;

		// Token: 0x04002525 RID: 9509
		public NKM_UNIT_STATUS_EFFECT m_StatusType;

		// Token: 0x04002526 RID: 9510
		public bool m_bRemove;

		// Token: 0x04002527 RID: 9511
		public float m_fStatusTime;

		// Token: 0x04002528 RID: 9512
		public float m_fRange;

		// Token: 0x04002529 RID: 9513
		public bool m_bMyTeam;

		// Token: 0x0400252A RID: 9514
		public bool m_bEnemy;

		// Token: 0x0400252B RID: 9515
		public int m_ApplyCount;

		// Token: 0x0400252C RID: 9516
		public float m_fMinTargetHP;

		// Token: 0x0400252D RID: 9517
		public float m_fMaxTargetHP;
	}
}
