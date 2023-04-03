using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004E7 RID: 1255
	public class NKMEventGameSpeed : IEventConditionOwner, INKMUnitStateEvent
	{
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06002368 RID: 9064 RVA: 0x000B6D3C File Offset: 0x000B4F3C
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06002369 RID: 9065 RVA: 0x000B6D44 File Offset: 0x000B4F44
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600236A RID: 9066 RVA: 0x000B6D4C File Offset: 0x000B4F4C
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Prohibited;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x0600236B RID: 9067 RVA: 0x000B6D4F File Offset: 0x000B4F4F
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600236C RID: 9068 RVA: 0x000B6D57 File Offset: 0x000B4F57
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x000B6D84 File Offset: 0x000B4F84
		public void DeepCopyFromSource(NKMEventGameSpeed source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_fGameSpeed = source.m_fGameSpeed;
			this.m_fTrackingTime = source.m_fTrackingTime;
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x000B6DE0 File Offset: 0x000B4FE0
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
			cNKMLua.GetData("m_fGameSpeed", ref this.m_fGameSpeed);
			cNKMLua.GetData("m_fTrackingTime", ref this.m_fTrackingTime);
			return true;
		}

		// Token: 0x040024FA RID: 9466
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x040024FB RID: 9467
		public bool m_bAnimTime = true;

		// Token: 0x040024FC RID: 9468
		public float m_fEventTime;

		// Token: 0x040024FD RID: 9469
		public bool m_bStateEndTime;

		// Token: 0x040024FE RID: 9470
		public float m_fGameSpeed = 1f;

		// Token: 0x040024FF RID: 9471
		public float m_fTrackingTime;
	}
}
