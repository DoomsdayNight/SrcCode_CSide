using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004C3 RID: 1219
	public class NKMEventText : INKMUnitStateEvent, IEventConditionOwner
	{
		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06002232 RID: 8754 RVA: 0x000B1104 File Offset: 0x000AF304
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06002233 RID: 8755 RVA: 0x000B110C File Offset: 0x000AF30C
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06002234 RID: 8756 RVA: 0x000B1114 File Offset: 0x000AF314
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06002235 RID: 8757 RVA: 0x000B1117 File Offset: 0x000AF317
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06002236 RID: 8758 RVA: 0x000B111F File Offset: 0x000AF31F
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x000B114C File Offset: 0x000AF34C
		public void DeepCopyFromSource(NKMEventText source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_Text = source.m_Text;
			this.m_fTime = source.m_fTime;
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x000B11A8 File Offset: 0x000AF3A8
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			cNKMLua.GetData("m_bAnimTime", ref this.m_bAnimTime);
			cNKMLua.GetData("m_fEventTime", ref this.m_fEventTime);
			cNKMLua.GetData("m_bStateEndTime", ref this.m_bStateEndTime);
			cNKMLua.GetData("m_Text", ref this.m_Text);
			cNKMLua.GetData("m_fTime", ref this.m_fTime);
			return true;
		}

		// Token: 0x04002380 RID: 9088
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x04002381 RID: 9089
		public bool m_bAnimTime = true;

		// Token: 0x04002382 RID: 9090
		public float m_fEventTime;

		// Token: 0x04002383 RID: 9091
		public bool m_bStateEndTime;

		// Token: 0x04002384 RID: 9092
		public string m_Text = "";

		// Token: 0x04002385 RID: 9093
		public float m_fTime;
	}
}
