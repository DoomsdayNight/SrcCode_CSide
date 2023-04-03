using System;
using System.Collections.Generic;
using NKM.Templet;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004EE RID: 1262
	public class NKMEventFindTarget : IEventConditionOwner, INKMUnitStateEventRollback, INKMUnitStateEvent, INKMUnitStateEventOneTime
	{
		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600239D RID: 9117 RVA: 0x000B7A95 File Offset: 0x000B5C95
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600239E RID: 9118 RVA: 0x000B7A9D File Offset: 0x000B5C9D
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x0600239F RID: 9119 RVA: 0x000B7AA5 File Offset: 0x000B5CA5
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060023A0 RID: 9120 RVA: 0x000B7AA8 File Offset: 0x000B5CA8
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060023A1 RID: 9121 RVA: 0x000B7AB0 File Offset: 0x000B5CB0
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x000B7AB8 File Offset: 0x000B5CB8
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
			cNKMLua.GetData("m_bSubTarget", ref this.m_bSubTarget);
			cNKMLua.GetData("m_fDuration", ref this.m_fDuration);
			cNKMLua.GetData<NKM_FIND_TARGET_TYPE>("m_FindTargetType", ref this.m_FindTargetType);
			cNKMLua.GetDataListEnum<NKM_UNIT_ROLE_TYPE>("m_hsFindTargetRolePriority", this.m_hsFindTargetRolePriority, true);
			cNKMLua.GetData("m_fSeeRange", ref this.m_fSeeRange);
			cNKMLua.GetData("m_bNoBackTarget", ref this.m_bNoBackTarget);
			cNKMLua.GetData("m_bNoFrontTarget", ref this.m_bNoFrontTarget);
			cNKMLua.GetData("m_bCanTargetBoss", ref this.m_bCanTargetBoss);
			cNKMLua.GetData("m_bPriorityOnly", ref this.m_bPriorityOnly);
			return true;
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x000B7BC0 File Offset: 0x000B5DC0
		public void ProcessEventRollback(NKMGame cNKMGame, NKMUnit cNKMUnit, float rollbackTime)
		{
			this.ProcessEvent(cNKMGame, cNKMUnit);
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x000B7BCC File Offset: 0x000B5DCC
		public void ProcessEvent(NKMGame cNKMGame, NKMUnit cNKMUnit)
		{
			NKMUnit targetUnit = cNKMGame.FindTarget(cNKMUnit, cNKMUnit.GetSortUnitListByNearDist(), this.m_FindTargetType, cNKMUnit.GetUnitDataGame().m_NKM_TEAM_TYPE, cNKMUnit.GetUnitSyncData().m_PosX, this.m_fSeeRange, this.m_bNoBackTarget, this.m_bNoFrontTarget, cNKMUnit.GetUnitSyncData().m_bRight, this.m_bCanTargetBoss, this.m_hsFindTargetRolePriority, this.m_bPriorityOnly);
			cNKMUnit.SetTargetUnit(targetUnit, this.m_fDuration, this.m_bSubTarget);
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x000B7C48 File Offset: 0x000B5E48
		public void DeepCopyFromSource(NKMEventFindTarget source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_hsFindTargetRolePriority.Clear();
			this.m_hsFindTargetRolePriority.UnionWith(source.m_hsFindTargetRolePriority);
			this.m_bSubTarget = source.m_bSubTarget;
			this.m_fDuration = source.m_fDuration;
			this.m_FindTargetType = source.m_FindTargetType;
			this.m_fSeeRange = source.m_fSeeRange;
			this.m_bNoBackTarget = source.m_bNoBackTarget;
			this.m_bNoFrontTarget = source.m_bNoFrontTarget;
			this.m_bPriorityOnly = source.m_bPriorityOnly;
		}

		// Token: 0x0400252E RID: 9518
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x0400252F RID: 9519
		public bool m_bAnimTime = true;

		// Token: 0x04002530 RID: 9520
		public float m_fEventTime;

		// Token: 0x04002531 RID: 9521
		public bool m_bStateEndTime;

		// Token: 0x04002532 RID: 9522
		public bool m_bSubTarget;

		// Token: 0x04002533 RID: 9523
		public float m_fDuration = 5f;

		// Token: 0x04002534 RID: 9524
		public NKM_FIND_TARGET_TYPE m_FindTargetType = NKM_FIND_TARGET_TYPE.NFTT_NEAR_ENEMY;

		// Token: 0x04002535 RID: 9525
		public HashSet<NKM_UNIT_ROLE_TYPE> m_hsFindTargetRolePriority = new HashSet<NKM_UNIT_ROLE_TYPE>();

		// Token: 0x04002536 RID: 9526
		public float m_fSeeRange;

		// Token: 0x04002537 RID: 9527
		public bool m_bNoBackTarget;

		// Token: 0x04002538 RID: 9528
		public bool m_bNoFrontTarget;

		// Token: 0x04002539 RID: 9529
		public bool m_bCanTargetBoss = true;

		// Token: 0x0400253A RID: 9530
		public bool m_bPriorityOnly;
	}
}
