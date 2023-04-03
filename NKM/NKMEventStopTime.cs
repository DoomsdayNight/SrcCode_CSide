using System;
using Cs.Math;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004D6 RID: 1238
	public class NKMEventStopTime : IEventConditionOwner, INKMUnitStateEventRollback, INKMUnitStateEvent
	{
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060022E2 RID: 8930 RVA: 0x000B4D99 File Offset: 0x000B2F99
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060022E3 RID: 8931 RVA: 0x000B4DA1 File Offset: 0x000B2FA1
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060022E4 RID: 8932 RVA: 0x000B4DA9 File Offset: 0x000B2FA9
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060022E5 RID: 8933 RVA: 0x000B4DAC File Offset: 0x000B2FAC
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060022E6 RID: 8934 RVA: 0x000B4DB4 File Offset: 0x000B2FB4
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x000B4DFC File Offset: 0x000B2FFC
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
			bool data = cNKMLua.GetData<NKM_STOP_TIME_INDEX>("m_StopTimeIndex", ref this.m_StopTimeIndex);
			cNKMLua.GetData("m_fStopTime", ref this.m_fStopTime);
			cNKMLua.GetData("m_fStopReserveTime", ref this.m_fStopReserveTime);
			cNKMLua.GetData("m_bMyStop", ref this.m_bStopSelf);
			cNKMLua.GetData("m_bSummoneeStop", ref this.m_bStopSummonee);
			return data;
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x000B4EBC File Offset: 0x000B30BC
		public void DeepCopyFromSource(NKMEventStopTime source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_StopTimeIndex = source.m_StopTimeIndex;
			this.m_fStopTime = source.m_fStopTime;
			this.m_fStopReserveTime = source.m_fStopReserveTime;
			this.m_bStopSelf = source.m_bStopSelf;
			this.m_bStopSummonee = source.m_bStopSummonee;
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x000B4F3C File Offset: 0x000B313C
		public void ProcessEventRollback(NKMGame cNKMGame, NKMUnit cNKMUnit, float rollbackTime)
		{
			if (!this.m_fStopReserveTime.IsNearlyEqual(-1f, 1E-05f))
			{
				cNKMUnit.SetStopReserveTime(this.m_fStopReserveTime);
			}
			cNKMGame.SetStopTime(cNKMUnit.GetUnitDataGame().m_GameUnitUID, this.m_fStopTime, this.m_bStopSelf, this.m_bStopSummonee, this.m_StopTimeIndex);
		}

		// Token: 0x0400244F RID: 9295
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x04002450 RID: 9296
		public bool m_bAnimTime = true;

		// Token: 0x04002451 RID: 9297
		public float m_fEventTime;

		// Token: 0x04002452 RID: 9298
		public bool m_bStateEndTime;

		// Token: 0x04002453 RID: 9299
		public NKM_STOP_TIME_INDEX m_StopTimeIndex;

		// Token: 0x04002454 RID: 9300
		public float m_fStopTime = -1f;

		// Token: 0x04002455 RID: 9301
		public float m_fStopReserveTime = -1f;

		// Token: 0x04002456 RID: 9302
		public bool m_bStopSelf = true;

		// Token: 0x04002457 RID: 9303
		public bool m_bStopSummonee = true;
	}
}
