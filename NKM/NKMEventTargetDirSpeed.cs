using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004C4 RID: 1220
	public class NKMEventTargetDirSpeed : INKMUnitStateEvent, IEventConditionOwner
	{
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x0600223A RID: 8762 RVA: 0x000B1210 File Offset: 0x000AF410
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x0600223B RID: 8763 RVA: 0x000B1218 File Offset: 0x000AF418
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.DEOnly;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x0600223C RID: 8764 RVA: 0x000B121B File Offset: 0x000AF41B
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x0600223D RID: 8765 RVA: 0x000B1223 File Offset: 0x000AF423
		public bool bStateEnd
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x0600223E RID: 8766 RVA: 0x000B1226 File Offset: 0x000AF426
		public NKMEventCondition Condition
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x000B1238 File Offset: 0x000AF438
		public void DeepCopyFromSource(NKMEventTargetDirSpeed source)
		{
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_fChangeTime = source.m_fChangeTime;
			this.m_fTargetDirSpeed = source.m_fTargetDirSpeed;
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x000B126C File Offset: 0x000AF46C
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			cNKMLua.GetData("m_bAnimTime", ref this.m_bAnimTime);
			cNKMLua.GetData("m_fEventTime", ref this.m_fEventTime);
			cNKMLua.GetData("m_fChangeTime", ref this.m_fChangeTime);
			cNKMLua.GetData("m_fTargetDirSpeed", ref this.m_fTargetDirSpeed);
			return true;
		}

		// Token: 0x04002386 RID: 9094
		public bool m_bAnimTime = true;

		// Token: 0x04002387 RID: 9095
		public float m_fEventTime;

		// Token: 0x04002388 RID: 9096
		public float m_fChangeTime;

		// Token: 0x04002389 RID: 9097
		public float m_fTargetDirSpeed;
	}
}
