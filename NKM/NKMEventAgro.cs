using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004D0 RID: 1232
	public class NKMEventAgro : IEventConditionOwner, INKMUnitStateEventRollback, INKMUnitStateEvent
	{
		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060022A8 RID: 8872 RVA: 0x000B3F01 File Offset: 0x000B2101
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060022A9 RID: 8873 RVA: 0x000B3F09 File Offset: 0x000B2109
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060022AA RID: 8874 RVA: 0x000B3F11 File Offset: 0x000B2111
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x060022AB RID: 8875 RVA: 0x000B3F14 File Offset: 0x000B2114
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x060022AC RID: 8876 RVA: 0x000B3F1C File Offset: 0x000B211C
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x000B3F50 File Offset: 0x000B2150
		public void DeepCopyFromSource(NKMEventAgro source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_bGetAgro = source.m_bGetAgro;
			this.m_fRange = source.m_fRange;
			this.m_fDurationTime = source.m_fDurationTime;
			this.m_MaxCount = source.m_MaxCount;
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x000B3FC4 File Offset: 0x000B21C4
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
			cNKMLua.GetData("m_bGetAgro", ref this.m_bGetAgro);
			cNKMLua.GetData("m_fRange", ref this.m_fRange);
			cNKMLua.GetData("m_bStateEndTime", ref this.m_bStateEndTime);
			cNKMLua.GetData("m_fDurationTime", ref this.m_fDurationTime);
			return true;
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x000B4071 File Offset: 0x000B2271
		public void ProcessEventRollback(NKMGame cNKMGame, NKMUnit cNKMUnit, float rollbackTime)
		{
			cNKMUnit.SetAgro(this.m_bGetAgro, this.m_fRange, this.m_fDurationTime, this.m_MaxCount);
		}

		// Token: 0x04002411 RID: 9233
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x04002412 RID: 9234
		public bool m_bAnimTime = true;

		// Token: 0x04002413 RID: 9235
		public float m_fEventTime;

		// Token: 0x04002414 RID: 9236
		public bool m_bStateEndTime;

		// Token: 0x04002415 RID: 9237
		public bool m_bGetAgro = true;

		// Token: 0x04002416 RID: 9238
		public float m_fRange;

		// Token: 0x04002417 RID: 9239
		public float m_fDurationTime = 999999f;

		// Token: 0x04002418 RID: 9240
		public int m_MaxCount;
	}
}
