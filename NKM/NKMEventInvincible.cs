using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004D7 RID: 1239
	public class NKMEventInvincible : IEventConditionOwner, INKMUnitStateEvent
	{
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060022EB RID: 8939 RVA: 0x000B4F95 File Offset: 0x000B3195
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060022EC RID: 8940 RVA: 0x000B4F9D File Offset: 0x000B319D
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTimeMin;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060022ED RID: 8941 RVA: 0x000B4FA5 File Offset: 0x000B31A5
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060022EE RID: 8942 RVA: 0x000B4FA8 File Offset: 0x000B31A8
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060022EF RID: 8943 RVA: 0x000B4FB0 File Offset: 0x000B31B0
		public bool bStateEnd
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x000B4FCD File Offset: 0x000B31CD
		public void DeepCopyFromSource(NKMEventInvincible source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTimeMin = source.m_fEventTimeMin;
			this.m_fEventTimeMax = source.m_fEventTimeMax;
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x000B5004 File Offset: 0x000B3204
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			if (cNKMLua.OpenTable("m_Condition"))
			{
				this.m_Condition.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_bAnimTime", ref this.m_bAnimTime);
			cNKMLua.GetData("m_fEventTimeMin", ref this.m_fEventTimeMin);
			cNKMLua.GetData("m_fEventTimeMax", ref this.m_fEventTimeMax);
			return true;
		}

		// Token: 0x04002458 RID: 9304
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x04002459 RID: 9305
		public bool m_bAnimTime = true;

		// Token: 0x0400245A RID: 9306
		public float m_fEventTimeMin;

		// Token: 0x0400245B RID: 9307
		public float m_fEventTimeMax;
	}
}
