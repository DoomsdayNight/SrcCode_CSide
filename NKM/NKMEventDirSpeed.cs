using System;
using Cs.Math;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004C5 RID: 1221
	public class NKMEventDirSpeed : IEventConditionOwner, INKMUnitStateEvent
	{
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06002242 RID: 8770 RVA: 0x000B12C2 File Offset: 0x000AF4C2
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06002243 RID: 8771 RVA: 0x000B12CA File Offset: 0x000AF4CA
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTimeMin;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06002244 RID: 8772 RVA: 0x000B12D2 File Offset: 0x000AF4D2
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.DEOnly;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06002245 RID: 8773 RVA: 0x000B12D5 File Offset: 0x000AF4D5
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06002246 RID: 8774 RVA: 0x000B12DD File Offset: 0x000AF4DD
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x000B1300 File Offset: 0x000AF500
		public void DeepCopyFromSource(NKMEventDirSpeed source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bFade = source.m_bFade;
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTimeMin = source.m_fEventTimeMin;
			this.m_fEventTimeMax = source.m_fEventTimeMax;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_fDirSpeed = source.m_fDirSpeed;
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x000B1368 File Offset: 0x000AF568
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			if (cNKMLua.OpenTable("m_Condition"))
			{
				this.m_Condition.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_bFade", ref this.m_bFade);
			cNKMLua.GetData("m_bAnimTime", ref this.m_bAnimTime);
			cNKMLua.GetData("m_fEventTimeMin", ref this.m_fEventTimeMin);
			cNKMLua.GetData("m_fEventTimeMax", ref this.m_fEventTimeMax);
			cNKMLua.GetData("m_bStateEndTime", ref this.m_bStateEndTime);
			cNKMLua.GetData("m_fDirSpeed", ref this.m_fDirSpeed);
			return true;
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x000B1404 File Offset: 0x000AF604
		public float GetSpeed(float fTime, float fSpeedNow)
		{
			if (!this.m_bFade || this.m_fEventTimeMin.IsNearlyEqual(this.m_fEventTimeMax, 1E-05f))
			{
				return this.m_fDirSpeed;
			}
			float num = (fTime - this.m_fEventTimeMin) / (this.m_fEventTimeMax - this.m_fEventTimeMin);
			return fSpeedNow + (this.m_fDirSpeed - fSpeedNow) * num;
		}

		// Token: 0x0400238A RID: 9098
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x0400238B RID: 9099
		public bool m_bFade;

		// Token: 0x0400238C RID: 9100
		public bool m_bAnimTime = true;

		// Token: 0x0400238D RID: 9101
		public float m_fEventTimeMin;

		// Token: 0x0400238E RID: 9102
		public float m_fEventTimeMax;

		// Token: 0x0400238F RID: 9103
		public bool m_bStateEndTime;

		// Token: 0x04002390 RID: 9104
		public float m_fDirSpeed;
	}
}
