using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004DC RID: 1244
	public class NKMEventColor : IEventConditionOwner, INKMUnitStateEvent
	{
		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06002313 RID: 8979 RVA: 0x000B5774 File Offset: 0x000B3974
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06002314 RID: 8980 RVA: 0x000B577C File Offset: 0x000B397C
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06002315 RID: 8981 RVA: 0x000B5784 File Offset: 0x000B3984
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06002316 RID: 8982 RVA: 0x000B5787 File Offset: 0x000B3987
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06002317 RID: 8983 RVA: 0x000B578F File Offset: 0x000B398F
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x000B57D4 File Offset: 0x000B39D4
		public void DeepCopyFromSource(NKMEventColor source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_fColorR = source.m_fColorR;
			this.m_fColorG = source.m_fColorG;
			this.m_fColorB = source.m_fColorB;
			this.m_fTrackTime = source.m_fTrackTime;
			this.m_fColorTime = source.m_fColorTime;
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x000B5854 File Offset: 0x000B3A54
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
			cNKMLua.GetData("m_fColorR", ref this.m_fColorR);
			cNKMLua.GetData("m_fColorG", ref this.m_fColorG);
			cNKMLua.GetData("m_fColorB", ref this.m_fColorB);
			cNKMLua.GetData("m_fTrackTime", ref this.m_fTrackTime);
			cNKMLua.GetData("m_fColorTime", ref this.m_fColorTime);
			return true;
		}

		// Token: 0x0400247A RID: 9338
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x0400247B RID: 9339
		public bool m_bAnimTime = true;

		// Token: 0x0400247C RID: 9340
		public float m_fEventTime;

		// Token: 0x0400247D RID: 9341
		public bool m_bStateEndTime;

		// Token: 0x0400247E RID: 9342
		public float m_fColorR = 1f;

		// Token: 0x0400247F RID: 9343
		public float m_fColorG = 1f;

		// Token: 0x04002480 RID: 9344
		public float m_fColorB = 1f;

		// Token: 0x04002481 RID: 9345
		public float m_fTrackTime;

		// Token: 0x04002482 RID: 9346
		public float m_fColorTime;
	}
}
