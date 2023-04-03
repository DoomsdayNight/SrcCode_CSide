using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004E8 RID: 1256
	public class NKMEventAnimSpeed : INKMUnitStateEvent, IEventConditionOwner
	{
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06002370 RID: 9072 RVA: 0x000B6E69 File Offset: 0x000B5069
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06002371 RID: 9073 RVA: 0x000B6E71 File Offset: 0x000B5071
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06002372 RID: 9074 RVA: 0x000B6E79 File Offset: 0x000B5079
		public EventRollbackType RollbackType
		{
			get
			{
				if (this.m_fEventTime != 0f)
				{
					return EventRollbackType.Prohibited;
				}
				return EventRollbackType.Warning;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06002373 RID: 9075 RVA: 0x000B6E8B File Offset: 0x000B508B
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06002374 RID: 9076 RVA: 0x000B6E93 File Offset: 0x000B5093
		public bool bStateEnd
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x000B6EBB File Offset: 0x000B50BB
		public void DeepCopyFromSource(NKMEventAnimSpeed source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_fAnimSpeed = source.m_fAnimSpeed;
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x000B6EF2 File Offset: 0x000B50F2
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			cNKMLua.GetData("m_bAnimTime", ref this.m_bAnimTime);
			cNKMLua.GetData("m_fEventTime", ref this.m_fEventTime);
			cNKMLua.GetData("m_fAnimSpeed", ref this.m_fAnimSpeed);
			return true;
		}

		// Token: 0x04002500 RID: 9472
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x04002501 RID: 9473
		public bool m_bAnimTime = true;

		// Token: 0x04002502 RID: 9474
		public float m_fEventTime;

		// Token: 0x04002503 RID: 9475
		public float m_fAnimSpeed = 1f;
	}
}
