using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004CF RID: 1231
	public class NKMEventChangeState : IEventConditionOwner, INKMUnitStateEvent
	{
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060022A0 RID: 8864 RVA: 0x000B3DD3 File Offset: 0x000B1FD3
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060022A1 RID: 8865 RVA: 0x000B3DDB File Offset: 0x000B1FDB
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060022A2 RID: 8866 RVA: 0x000B3DE3 File Offset: 0x000B1FE3
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Prohibited;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060022A3 RID: 8867 RVA: 0x000B3DE6 File Offset: 0x000B1FE6
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060022A4 RID: 8868 RVA: 0x000B3DEE File Offset: 0x000B1FEE
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x000B3E1C File Offset: 0x000B201C
		public void DeepCopyFromSource(NKMEventChangeState source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_TargetUnitID = source.m_TargetUnitID;
			this.m_ChangeState = source.m_ChangeState;
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x000B3E78 File Offset: 0x000B2078
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
			cNKMLua.GetData("m_TargetUnitID", ref this.m_TargetUnitID);
			cNKMLua.GetData("m_ChangeState", ref this.m_ChangeState);
			return true;
		}

		// Token: 0x0400240B RID: 9227
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x0400240C RID: 9228
		public bool m_bAnimTime = true;

		// Token: 0x0400240D RID: 9229
		public float m_fEventTime;

		// Token: 0x0400240E RID: 9230
		public bool m_bStateEndTime;

		// Token: 0x0400240F RID: 9231
		public int m_TargetUnitID;

		// Token: 0x04002410 RID: 9232
		public string m_ChangeState = "";
	}
}
