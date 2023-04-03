using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004D5 RID: 1237
	public class NKMEventDispel : IEventConditionOwner, INKMUnitStateEventRollback, INKMUnitStateEvent
	{
		// Token: 0x170003CD RID: 973
		// (get) Token: 0x060022D9 RID: 8921 RVA: 0x000B4BA5 File Offset: 0x000B2DA5
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x060022DA RID: 8922 RVA: 0x000B4BAD File Offset: 0x000B2DAD
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x060022DB RID: 8923 RVA: 0x000B4BB5 File Offset: 0x000B2DB5
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060022DC RID: 8924 RVA: 0x000B4BB8 File Offset: 0x000B2DB8
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060022DD RID: 8925 RVA: 0x000B4BC0 File Offset: 0x000B2DC0
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x000B4BEC File Offset: 0x000B2DEC
		public void DeepCopyFromSource(NKMEventDispel source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_bDebuff = source.m_bDebuff;
			this.m_bDeleteInfinity = source.m_bDeleteInfinity;
			this.m_fRangeMin = source.m_fRangeMin;
			this.m_fRangeMax = source.m_fRangeMax;
			this.m_MaxCount = source.m_MaxCount;
			this.m_bTargetSelf = source.m_bTargetSelf;
			this.m_DispelCountPerSkillLevel = source.m_DispelCountPerSkillLevel;
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x000B4C84 File Offset: 0x000B2E84
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
			cNKMLua.GetData("m_bDebuff", ref this.m_bDebuff);
			cNKMLua.GetData("m_bDeleteInfinity", ref this.m_bDeleteInfinity);
			cNKMLua.GetData("m_fRangeMin", ref this.m_fRangeMin);
			cNKMLua.GetData("m_fRangeMax", ref this.m_fRangeMax);
			cNKMLua.GetData("m_MaxCount", ref this.m_MaxCount);
			cNKMLua.GetData("m_bTargetSelf", ref this.m_bTargetSelf);
			cNKMLua.GetData("m_DispelCountPerSkillLevel", ref this.m_DispelCountPerSkillLevel);
			return true;
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x000B4D67 File Offset: 0x000B2F67
		public void ProcessEventRollback(NKMGame cNKMGame, NKMUnit cNKMUnit, float rollbackTime)
		{
			cNKMUnit.SetDispel(this.m_bDebuff, this.m_fRangeMin, this.m_fRangeMax, this.m_MaxCount, this.m_bTargetSelf, this.m_DispelCountPerSkillLevel, this.m_bDeleteInfinity);
		}

		// Token: 0x04002444 RID: 9284
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x04002445 RID: 9285
		public bool m_bAnimTime = true;

		// Token: 0x04002446 RID: 9286
		public float m_fEventTime;

		// Token: 0x04002447 RID: 9287
		public bool m_bStateEndTime;

		// Token: 0x04002448 RID: 9288
		public bool m_bDebuff = true;

		// Token: 0x04002449 RID: 9289
		public bool m_bDeleteInfinity;

		// Token: 0x0400244A RID: 9290
		public float m_fRangeMin;

		// Token: 0x0400244B RID: 9291
		public float m_fRangeMax;

		// Token: 0x0400244C RID: 9292
		public int m_MaxCount;

		// Token: 0x0400244D RID: 9293
		public bool m_bTargetSelf;

		// Token: 0x0400244E RID: 9294
		public int m_DispelCountPerSkillLevel;
	}
}
