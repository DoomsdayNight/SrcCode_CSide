using System;
using Cs.Math;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004C8 RID: 1224
	public class NKMEventSpeedY : IEventConditionOwner, INKMUnitStateEventRollback, INKMUnitStateEvent
	{
		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06002260 RID: 8800 RVA: 0x000B1AF4 File Offset: 0x000AFCF4
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06002261 RID: 8801 RVA: 0x000B1AFC File Offset: 0x000AFCFC
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTimeMin;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06002262 RID: 8802 RVA: 0x000B1B04 File Offset: 0x000AFD04
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

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06002263 RID: 8803 RVA: 0x000B1B30 File Offset: 0x000AFD30
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06002264 RID: 8804 RVA: 0x000B1B38 File Offset: 0x000AFD38
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x000B1B5C File Offset: 0x000AFD5C
		public void DeepCopyFromSource(NKMEventSpeedY source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bFade = source.m_bFade;
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTimeMin = source.m_fEventTimeMin;
			this.m_fEventTimeMax = source.m_fEventTimeMax;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_bAdd = source.m_bAdd;
			this.m_bMultiply = source.m_bMultiply;
			this.m_SpeedY = source.m_SpeedY;
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x000B1BDC File Offset: 0x000AFDDC
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
			cNKMLua.GetData("m_SpeedY", ref this.m_SpeedY);
			return true;
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x000B1C9C File Offset: 0x000AFE9C
		public float GetSpeed(float fTime, float fSpeedNow)
		{
			if (!this.m_bFade || this.m_fEventTimeMin.IsNearlyEqual(this.m_fEventTimeMax, 1E-05f))
			{
				return this.m_SpeedY;
			}
			float num = (fTime - this.m_fEventTimeMin) / (this.m_fEventTimeMax - this.m_fEventTimeMin);
			return fSpeedNow + (this.m_SpeedY - fSpeedNow) * num;
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x000B1CF4 File Offset: 0x000AFEF4
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
			float num3 = this.m_SpeedY * (num2 - num);
			cNKMUnit.GetUnitFrameData().m_fSpeedY = this.m_SpeedY;
			cNKMUnit.GetUnitFrameData().m_JumpYPosCalc += num3;
		}

		// Token: 0x040023A4 RID: 9124
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x040023A5 RID: 9125
		public bool m_bFade;

		// Token: 0x040023A6 RID: 9126
		public bool m_bAnimTime = true;

		// Token: 0x040023A7 RID: 9127
		public float m_fEventTimeMin;

		// Token: 0x040023A8 RID: 9128
		public float m_fEventTimeMax;

		// Token: 0x040023A9 RID: 9129
		public bool m_bStateEndTime;

		// Token: 0x040023AA RID: 9130
		public bool m_bAdd;

		// Token: 0x040023AB RID: 9131
		public bool m_bMultiply;

		// Token: 0x040023AC RID: 9132
		public float m_SpeedY;
	}
}
