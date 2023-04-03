using System;
using System.Collections.Generic;
using NKM.Templet;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004D3 RID: 1235
	public class NKMEventStun : IEventConditionOwner, INKMUnitStateEventRollback, INKMUnitStateEvent
	{
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x060022C8 RID: 8904 RVA: 0x000B47E5 File Offset: 0x000B29E5
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x060022C9 RID: 8905 RVA: 0x000B47ED File Offset: 0x000B29ED
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x060022CA RID: 8906 RVA: 0x000B47F5 File Offset: 0x000B29F5
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x060022CB RID: 8907 RVA: 0x000B47F8 File Offset: 0x000B29F8
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x060022CC RID: 8908 RVA: 0x000B4800 File Offset: 0x000B2A00
		public bool bStateEnd
		{
			get
			{
				return this.m_bStateEndTime;
			}
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x000B4830 File Offset: 0x000B2A30
		public void DeepCopyFromSource(NKMEventStun source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_bStateEndTime = source.m_bStateEndTime;
			this.m_fStunTime = source.m_fStunTime;
			this.m_fRange = source.m_fRange;
			this.m_MaxCount = source.m_MaxCount;
			this.m_fStunTimePerSkillLevel = source.m_fStunTimePerSkillLevel;
			this.m_StunCountPerSkillLevel = source.m_StunCountPerSkillLevel;
			this.m_IgnoreStyleType.Clear();
			foreach (NKM_UNIT_STYLE_TYPE item in source.m_IgnoreStyleType)
			{
				this.m_IgnoreStyleType.Add(item);
			}
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x000B4908 File Offset: 0x000B2B08
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
			cNKMLua.GetData("m_fStunTime", ref this.m_fStunTime);
			cNKMLua.GetData("m_fRange", ref this.m_fRange);
			cNKMLua.GetData("m_MaxCount", ref this.m_MaxCount);
			cNKMLua.GetData("m_fStunTimePerSkillLevel", ref this.m_fStunTimePerSkillLevel);
			cNKMLua.GetData("m_StunCountPerSkillLevel", ref this.m_StunCountPerSkillLevel);
			this.m_IgnoreStyleType.Clear();
			if (cNKMLua.OpenTable("m_IgnoreStyleType"))
			{
				bool flag = true;
				int num = 1;
				NKM_UNIT_STYLE_TYPE item = NKM_UNIT_STYLE_TYPE.NUST_INVALID;
				while (flag)
				{
					flag = cNKMLua.GetData<NKM_UNIT_STYLE_TYPE>(num, ref item);
					if (flag)
					{
						this.m_IgnoreStyleType.Add(item);
					}
					num++;
				}
				cNKMLua.CloseTable();
			}
			return true;
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x000B4A0F File Offset: 0x000B2C0F
		public void ProcessEventRollback(NKMGame cNKMGame, NKMUnit cNKMUnit, float rollbackTime)
		{
			cNKMUnit.SetStun(cNKMUnit, this.m_fStunTime, this.m_fRange, this.m_MaxCount, this.m_fStunTimePerSkillLevel, this.m_StunCountPerSkillLevel, this.m_IgnoreStyleType);
		}

		// Token: 0x04002432 RID: 9266
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x04002433 RID: 9267
		public bool m_bAnimTime = true;

		// Token: 0x04002434 RID: 9268
		public float m_fEventTime;

		// Token: 0x04002435 RID: 9269
		public bool m_bStateEndTime;

		// Token: 0x04002436 RID: 9270
		public float m_fStunTime;

		// Token: 0x04002437 RID: 9271
		public float m_fRange;

		// Token: 0x04002438 RID: 9272
		public int m_MaxCount;

		// Token: 0x04002439 RID: 9273
		public float m_fStunTimePerSkillLevel;

		// Token: 0x0400243A RID: 9274
		public int m_StunCountPerSkillLevel;

		// Token: 0x0400243B RID: 9275
		public HashSet<NKM_UNIT_STYLE_TYPE> m_IgnoreStyleType = new HashSet<NKM_UNIT_STYLE_TYPE>();
	}
}
