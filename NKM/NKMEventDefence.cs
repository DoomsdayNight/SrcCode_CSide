using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004D4 RID: 1236
	public class NKMEventDefence : IEventConditionOwner, INKMUnitStateEvent
	{
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x060022D1 RID: 8913 RVA: 0x000B4A3C File Offset: 0x000B2C3C
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x060022D2 RID: 8914 RVA: 0x000B4A44 File Offset: 0x000B2C44
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTimeMin;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x060022D3 RID: 8915 RVA: 0x000B4A4C File Offset: 0x000B2C4C
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x060022D4 RID: 8916 RVA: 0x000B4A4F File Offset: 0x000B2C4F
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x060022D5 RID: 8917 RVA: 0x000B4A57 File Offset: 0x000B2C57
		public bool bStateEnd
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x000B4A84 File Offset: 0x000B2C84
		public void DeepCopyFromSource(NKMEventDefence source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTimeMin = source.m_fEventTimeMin;
			this.m_fEventTimeMax = source.m_fEventTimeMax;
			this.m_bDefenceFront = source.m_bDefenceFront;
			this.m_bDefenceBack = source.m_bDefenceBack;
			this.m_fDamageReduceRate = source.m_fDamageReduceRate;
			this.m_fDamageReducePerSkillLevel = source.m_fDamageReducePerSkillLevel;
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x000B4AF8 File Offset: 0x000B2CF8
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			if (cNKMLua.OpenTable("m_Condition"))
			{
				this.m_Condition.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_bAnimTime", ref this.m_bAnimTime);
			cNKMLua.GetData("m_fEventTimeMin", ref this.m_fEventTimeMin);
			cNKMLua.GetData("m_fEventTimeMax", ref this.m_fEventTimeMax);
			cNKMLua.GetData("m_bDefenceFront", ref this.m_bDefenceFront);
			cNKMLua.GetData("m_bDefenceBack", ref this.m_bDefenceBack);
			cNKMLua.GetData("m_fDamageReduceRate", ref this.m_fDamageReduceRate);
			cNKMLua.GetData("m_fDamageReducePerSkillLevel", ref this.m_fDamageReducePerSkillLevel);
			return true;
		}

		// Token: 0x0400243C RID: 9276
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x0400243D RID: 9277
		public bool m_bAnimTime = true;

		// Token: 0x0400243E RID: 9278
		public float m_fEventTimeMin;

		// Token: 0x0400243F RID: 9279
		public float m_fEventTimeMax;

		// Token: 0x04002440 RID: 9280
		public bool m_bDefenceFront = true;

		// Token: 0x04002441 RID: 9281
		public bool m_bDefenceBack = true;

		// Token: 0x04002442 RID: 9282
		public float m_fDamageReduceRate;

		// Token: 0x04002443 RID: 9283
		public float m_fDamageReducePerSkillLevel;
	}
}
