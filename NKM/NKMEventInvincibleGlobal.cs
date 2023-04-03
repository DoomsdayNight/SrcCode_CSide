using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004D8 RID: 1240
	public class NKMEventInvincibleGlobal : IEventConditionOwner, INKMUnitStateEventRollback, INKMUnitStateEvent
	{
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060022F3 RID: 8947 RVA: 0x000B5069 File Offset: 0x000B3269
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060022F4 RID: 8948 RVA: 0x000B5071 File Offset: 0x000B3271
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060022F5 RID: 8949 RVA: 0x000B5079 File Offset: 0x000B3279
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060022F6 RID: 8950 RVA: 0x000B507C File Offset: 0x000B327C
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060022F7 RID: 8951 RVA: 0x000B5084 File Offset: 0x000B3284
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x000B50A8 File Offset: 0x000B32A8
		public void DeepCopyFromSource(NKMEventInvincibleGlobal source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_InvincibleTime = source.m_InvincibleTime;
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x000B50F8 File Offset: 0x000B32F8
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
			cNKMLua.GetData("m_InvincibleTime", ref this.m_InvincibleTime);
			return true;
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x000B5170 File Offset: 0x000B3370
		public void ProcessEventRollback(NKMGame cNKMGame, NKMUnit cNKMUnit, float rollbackTime)
		{
			float eventStateTime = cNKMUnit.GetEventStateTime(this.m_bAnimTime, this.m_fEventTime);
			float time = this.m_InvincibleTime - (rollbackTime - eventStateTime);
			cNKMUnit.ApplyStatusTime(NKM_UNIT_STATUS_EFFECT.NUSE_INVINCIBLE, time, cNKMUnit, false, false, true);
		}

		// Token: 0x0400245C RID: 9308
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x0400245D RID: 9309
		public bool m_bAnimTime = true;

		// Token: 0x0400245E RID: 9310
		public float m_fEventTime;

		// Token: 0x0400245F RID: 9311
		public bool m_bStateEndTime;

		// Token: 0x04002460 RID: 9312
		public float m_InvincibleTime;
	}
}
