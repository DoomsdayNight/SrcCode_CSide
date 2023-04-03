using System;
using Cs.Math;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004C7 RID: 1223
	public class NKMEventSpeedX : IEventConditionOwner, INKMUnitStateEventRollback, INKMUnitStateEvent
	{
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06002256 RID: 8790 RVA: 0x000B181E File Offset: 0x000AFA1E
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06002257 RID: 8791 RVA: 0x000B1826 File Offset: 0x000AFA26
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTimeMin;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06002258 RID: 8792 RVA: 0x000B182E File Offset: 0x000AFA2E
		public EventRollbackType RollbackType
		{
			get
			{
				if (this.m_bAdd || this.m_bMultiply || this.m_bFade)
				{
					return EventRollbackType.Prohibited;
				}
				if (this.m_fEventTimeMax < NKMCommonConst.SUMMON_UNIT_NOEVENT_TIME)
				{
					return EventRollbackType.Warning;
				}
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06002259 RID: 8793 RVA: 0x000B185A File Offset: 0x000AFA5A
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x0600225A RID: 8794 RVA: 0x000B1862 File Offset: 0x000AFA62
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x000B1884 File Offset: 0x000AFA84
		public void DeepCopyFromSource(NKMEventSpeedX source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bFade = source.m_bFade;
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTimeMin = source.m_fEventTimeMin;
			this.m_fEventTimeMax = source.m_fEventTimeMax;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_bAdd = source.m_bAdd;
			this.m_bMultiply = source.m_bMultiply;
			this.m_SpeedX = source.m_SpeedX;
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x000B1904 File Offset: 0x000AFB04
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
			cNKMLua.GetData("m_bAdd", ref this.m_bAdd);
			cNKMLua.GetData("m_bMultiply", ref this.m_bMultiply);
			cNKMLua.GetData("m_SpeedX", ref this.m_SpeedX);
			return true;
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x000B19C4 File Offset: 0x000AFBC4
		public float GetSpeed(float fTime, float fSpeedNow)
		{
			if (!this.m_bFade || this.m_fEventTimeMin.IsNearlyEqual(this.m_fEventTimeMax, 1E-05f))
			{
				return this.m_SpeedX;
			}
			float num = (fTime - this.m_fEventTimeMin) / (this.m_fEventTimeMax - this.m_fEventTimeMin);
			return fSpeedNow + (this.m_SpeedX - fSpeedNow) * num;
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x000B1A1C File Offset: 0x000AFC1C
		public void ProcessEventRollback(NKMGame cNKMGame, NKMUnit cNKMUnit, float rollbackTime)
		{
			if (this.m_bAnimTime && cNKMUnit.GetUnitFrameData().m_fAnimSpeed == 0f)
			{
				return;
			}
			float num;
			if (this.m_bAnimTime)
			{
				num = this.m_fEventTimeMin / cNKMUnit.GetUnitFrameData().m_fAnimSpeed;
			}
			else
			{
				num = this.m_fEventTimeMin;
			}
			float num2;
			if (cNKMUnit.RollbackEventTimer(this.m_bAnimTime, this.m_fEventTimeMax))
			{
				if (this.m_bAnimTime)
				{
					num2 = this.m_fEventTimeMax / cNKMUnit.GetUnitFrameData().m_fAnimSpeed;
				}
				else
				{
					num2 = this.m_fEventTimeMax;
				}
			}
			else
			{
				num2 = rollbackTime;
			}
			float num3 = this.m_SpeedX * (num2 - num);
			cNKMUnit.GetUnitFrameData().m_fSpeedX = this.m_SpeedX;
			if (cNKMUnit.GetUnitSyncData().m_bRight)
			{
				cNKMUnit.GetUnitFrameData().m_PosXCalc += num3;
				return;
			}
			cNKMUnit.GetUnitFrameData().m_PosXCalc -= num3;
		}

		// Token: 0x0400239B RID: 9115
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x0400239C RID: 9116
		public bool m_bFade;

		// Token: 0x0400239D RID: 9117
		public bool m_bAnimTime = true;

		// Token: 0x0400239E RID: 9118
		public float m_fEventTimeMin;

		// Token: 0x0400239F RID: 9119
		public float m_fEventTimeMax;

		// Token: 0x040023A0 RID: 9120
		public bool m_bStateEndTime;

		// Token: 0x040023A1 RID: 9121
		public bool m_bAdd;

		// Token: 0x040023A2 RID: 9122
		public bool m_bMultiply;

		// Token: 0x040023A3 RID: 9123
		public float m_SpeedX;
	}
}
