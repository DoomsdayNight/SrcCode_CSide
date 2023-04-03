using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004DF RID: 1247
	public class NKMEventFadeWorld : IEventConditionOwner, INKMUnitStateEvent
	{
		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x0600232B RID: 9003 RVA: 0x000B5C91 File Offset: 0x000B3E91
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x0600232C RID: 9004 RVA: 0x000B5C99 File Offset: 0x000B3E99
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTimeMin;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x0600232D RID: 9005 RVA: 0x000B5CA1 File Offset: 0x000B3EA1
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x0600232E RID: 9006 RVA: 0x000B5CA4 File Offset: 0x000B3EA4
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x0600232F RID: 9007 RVA: 0x000B5CAC File Offset: 0x000B3EAC
		public bool bStateEnd
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x000B5CEC File Offset: 0x000B3EEC
		public void DeepCopyFromSource(NKMEventFadeWorld source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTimeMin = source.m_fEventTimeMin;
			this.m_fEventTimeMax = source.m_fEventTimeMax;
			this.m_fColorR = source.m_fColorR;
			this.m_fColorG = source.m_fColorG;
			this.m_fColorB = source.m_fColorB;
			this.m_fMapColorKeepTime = source.m_fMapColorKeepTime;
			this.m_fMapColorReturnTime = source.m_fMapColorReturnTime;
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x000B5D6C File Offset: 0x000B3F6C
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
			cNKMLua.GetData("m_fColorR", ref this.m_fColorR);
			cNKMLua.GetData("m_fColorG", ref this.m_fColorG);
			cNKMLua.GetData("m_fColorB", ref this.m_fColorB);
			cNKMLua.GetData("m_fMapColorKeepTime", ref this.m_fMapColorKeepTime);
			cNKMLua.GetData("m_fMapColorReturnTime", ref this.m_fMapColorReturnTime);
			return true;
		}

		// Token: 0x04002499 RID: 9369
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x0400249A RID: 9370
		public bool m_bAnimTime = true;

		// Token: 0x0400249B RID: 9371
		public float m_fEventTimeMin;

		// Token: 0x0400249C RID: 9372
		public float m_fEventTimeMax;

		// Token: 0x0400249D RID: 9373
		public float m_fColorR = 1f;

		// Token: 0x0400249E RID: 9374
		public float m_fColorG = 1f;

		// Token: 0x0400249F RID: 9375
		public float m_fColorB = 1f;

		// Token: 0x040024A0 RID: 9376
		public float m_fMapColorKeepTime;

		// Token: 0x040024A1 RID: 9377
		public float m_fMapColorReturnTime;
	}
}
