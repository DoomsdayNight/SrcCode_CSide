using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004D2 RID: 1234
	public class NKMEventCost : IEventConditionOwner, INKMUnitStateEventRollback, INKMUnitStateEvent
	{
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x060022BF RID: 8895 RVA: 0x000B4688 File Offset: 0x000B2888
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x060022C0 RID: 8896 RVA: 0x000B4690 File Offset: 0x000B2890
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x060022C1 RID: 8897 RVA: 0x000B4698 File Offset: 0x000B2898
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x060022C2 RID: 8898 RVA: 0x000B469B File Offset: 0x000B289B
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x000B46A3 File Offset: 0x000B28A3
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x000B46C8 File Offset: 0x000B28C8
		public void DeepCopyFromSource(NKMEventCost source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_AddCost = source.m_AddCost;
			this.m_RemoveCost = source.m_RemoveCost;
			this.m_CostPerSkillLevel = source.m_CostPerSkillLevel;
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x000B4730 File Offset: 0x000B2930
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
			cNKMLua.GetData("m_AddCost", ref this.m_AddCost);
			cNKMLua.GetData("m_RemoveCost", ref this.m_RemoveCost);
			cNKMLua.GetData("m_CostPerSkillLevel", ref this.m_CostPerSkillLevel);
			return true;
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x000B47CB File Offset: 0x000B29CB
		public void ProcessEventRollback(NKMGame cNKMGame, NKMUnit cNKMUnit, float rollbackTime)
		{
			cNKMUnit.SetCost(this.m_AddCost, this.m_RemoveCost, this.m_CostPerSkillLevel);
		}

		// Token: 0x0400242B RID: 9259
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x0400242C RID: 9260
		public bool m_bAnimTime = true;

		// Token: 0x0400242D RID: 9261
		public float m_fEventTime;

		// Token: 0x0400242E RID: 9262
		public bool m_bStateEndTime;

		// Token: 0x0400242F RID: 9263
		public float m_AddCost;

		// Token: 0x04002430 RID: 9264
		public float m_RemoveCost;

		// Token: 0x04002431 RID: 9265
		public float m_CostPerSkillLevel;
	}
}
