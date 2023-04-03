using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004E0 RID: 1248
	public class NKMEventDissolve : IEventConditionOwner, INKMUnitStateEvent
	{
		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06002333 RID: 9011 RVA: 0x000B5E2B File Offset: 0x000B402B
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06002334 RID: 9012 RVA: 0x000B5E33 File Offset: 0x000B4033
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06002335 RID: 9013 RVA: 0x000B5E3B File Offset: 0x000B403B
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06002336 RID: 9014 RVA: 0x000B5E3E File Offset: 0x000B403E
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06002337 RID: 9015 RVA: 0x000B5E46 File Offset: 0x000B4046
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x000B5E8C File Offset: 0x000B408C
		public void DeepCopyFromSource(NKMEventDissolve source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_fColorR = source.m_fColorR;
			this.m_fColorG = source.m_fColorG;
			this.m_fColorB = source.m_fColorB;
			this.m_fDissolve = source.m_fDissolve;
			this.m_fTrackTime = source.m_fTrackTime;
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x000B5F0C File Offset: 0x000B410C
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
			cNKMLua.GetData("m_fDissolve", ref this.m_fDissolve);
			cNKMLua.GetData("m_fTrackTime", ref this.m_fTrackTime);
			return true;
		}

		// Token: 0x040024A2 RID: 9378
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x040024A3 RID: 9379
		public bool m_bAnimTime = true;

		// Token: 0x040024A4 RID: 9380
		public float m_fEventTime;

		// Token: 0x040024A5 RID: 9381
		public bool m_bStateEndTime;

		// Token: 0x040024A6 RID: 9382
		public float m_fColorR = -1f;

		// Token: 0x040024A7 RID: 9383
		public float m_fColorG = -1f;

		// Token: 0x040024A8 RID: 9384
		public float m_fColorB = -1f;

		// Token: 0x040024A9 RID: 9385
		public float m_fDissolve;

		// Token: 0x040024AA RID: 9386
		public float m_fTrackTime;
	}
}
