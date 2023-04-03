using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004EB RID: 1259
	public class NKMEventCatchEnd : IEventConditionOwner, INKMUnitStateEvent
	{
		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06002384 RID: 9092 RVA: 0x000B7143 File Offset: 0x000B5343
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06002385 RID: 9093 RVA: 0x000B714B File Offset: 0x000B534B
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06002386 RID: 9094 RVA: 0x000B7153 File Offset: 0x000B5353
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Prohibited;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06002387 RID: 9095 RVA: 0x000B7156 File Offset: 0x000B5356
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06002388 RID: 9096 RVA: 0x000B715E File Offset: 0x000B535E
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x000B7180 File Offset: 0x000B5380
		public void DeepCopyFromSource(NKMEventCatchEnd source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_bStateEndTime = source.m_bStateEndTime;
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x000B71B8 File Offset: 0x000B53B8
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
			return true;
		}

		// Token: 0x0400250F RID: 9487
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x04002510 RID: 9488
		public bool m_bAnimTime = true;

		// Token: 0x04002511 RID: 9489
		public float m_fEventTime;

		// Token: 0x04002512 RID: 9490
		public bool m_bStateEndTime;
	}
}
