using System;
using Cs.Math;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004C6 RID: 1222
	public class NKMEventSpeed : IEventConditionOwner, INKMUnitStateEventRollback, INKMUnitStateEvent
	{
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x0600224B RID: 8779 RVA: 0x000B145B File Offset: 0x000AF65B
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x0600224C RID: 8780 RVA: 0x000B1463 File Offset: 0x000AF663
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTimeMin;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x0600224D RID: 8781 RVA: 0x000B146B File Offset: 0x000AF66B
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

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x000B1497 File Offset: 0x000AF697
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x0600224F RID: 8783 RVA: 0x000B149F File Offset: 0x000AF69F
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x000B14D8 File Offset: 0x000AF6D8
		public void DeepCopyFromSource(NKMEventSpeed source)
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
			this.m_SpeedY = source.m_SpeedY;
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x000B1564 File Offset: 0x000AF764
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
			cNKMLua.GetData("m_SpeedY", ref this.m_SpeedY);
			return true;
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x000B1638 File Offset: 0x000AF838
		public float GetSpeedX(float fTime, float fSpeedNow)
		{
			if (!this.m_bFade || this.m_fEventTimeMin.IsNearlyEqual(this.m_fEventTimeMax, 1E-05f))
			{
				return this.m_SpeedX;
			}
			float num = (fTime - this.m_fEventTimeMin) / (this.m_fEventTimeMax - this.m_fEventTimeMin);
			return fSpeedNow + (this.m_SpeedX - fSpeedNow) * num;
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x000B1690 File Offset: 0x000AF890
		public float GetSpeedY(float fTime, float fSpeedNow)
		{
			if (!this.m_bFade || this.m_fEventTimeMin.IsNearlyEqual(this.m_fEventTimeMax, 1E-05f))
			{
				return this.m_SpeedY;
			}
			float num = (fTime - this.m_fEventTimeMin) / (this.m_fEventTimeMax - this.m_fEventTimeMin);
			return fSpeedNow + (this.m_SpeedY - fSpeedNow) * num;
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x000B16E8 File Offset: 0x000AF8E8
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
			if (!this.m_SpeedX.IsNearlyEqual(-1f, 1E-05f))
			{
				float num3 = this.m_SpeedX * (num2 - num);
				cNKMUnit.GetUnitFrameData().m_fSpeedX = this.m_SpeedX;
				if (cNKMUnit.GetUnitSyncData().m_bRight)
				{
					cNKMUnit.GetUnitFrameData().m_PosXCalc += num3;
				}
				else
				{
					cNKMUnit.GetUnitFrameData().m_PosXCalc -= num3;
				}
			}
			if (!this.m_SpeedY.IsNearlyEqual(-1f, 1E-05f))
			{
				float num4 = this.m_SpeedY * (num2 - num);
				cNKMUnit.GetUnitFrameData().m_fSpeedY = this.m_SpeedY;
				cNKMUnit.GetUnitFrameData().m_JumpYPosCalc += num4;
			}
		}

		// Token: 0x04002391 RID: 9105
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x04002392 RID: 9106
		public bool m_bFade;

		// Token: 0x04002393 RID: 9107
		public bool m_bAnimTime = true;

		// Token: 0x04002394 RID: 9108
		public float m_fEventTimeMin;

		// Token: 0x04002395 RID: 9109
		public float m_fEventTimeMax;

		// Token: 0x04002396 RID: 9110
		public bool m_bStateEndTime;

		// Token: 0x04002397 RID: 9111
		public bool m_bAdd;

		// Token: 0x04002398 RID: 9112
		public bool m_bMultiply;

		// Token: 0x04002399 RID: 9113
		public float m_SpeedX = -1f;

		// Token: 0x0400239A RID: 9114
		public float m_SpeedY = -1f;
	}
}
