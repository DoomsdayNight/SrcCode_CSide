using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004CE RID: 1230
	public class NKMEventDie : IEventConditionOwner, INKMUnitStateEvent
	{
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06002298 RID: 8856 RVA: 0x000B3CCF File Offset: 0x000B1ECF
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06002299 RID: 8857 RVA: 0x000B3CD7 File Offset: 0x000B1ED7
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x0600229A RID: 8858 RVA: 0x000B3CDF File Offset: 0x000B1EDF
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Prohibited;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x0600229B RID: 8859 RVA: 0x000B3CE2 File Offset: 0x000B1EE2
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x0600229C RID: 8860 RVA: 0x000B3CEA File Offset: 0x000B1EEA
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x000B3D0C File Offset: 0x000B1F0C
		public void DeepCopyFromSource(NKMEventDie source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_bImmediateDie = source.m_bImmediateDie;
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x000B3D5C File Offset: 0x000B1F5C
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
			cNKMLua.GetData("m_bImmediateDie", ref this.m_bImmediateDie);
			return true;
		}

		// Token: 0x04002406 RID: 9222
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x04002407 RID: 9223
		public bool m_bAnimTime = true;

		// Token: 0x04002408 RID: 9224
		public float m_fEventTime;

		// Token: 0x04002409 RID: 9225
		public bool m_bStateEndTime;

		// Token: 0x0400240A RID: 9226
		public bool m_bImmediateDie;
	}
}
