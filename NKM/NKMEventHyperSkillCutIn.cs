using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004E4 RID: 1252
	public class NKMEventHyperSkillCutIn : IEventConditionOwner, INKMUnitStateEvent
	{
		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x0600234F RID: 9039 RVA: 0x000B668B File Offset: 0x000B488B
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06002350 RID: 9040 RVA: 0x000B6693 File Offset: 0x000B4893
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTime;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06002351 RID: 9041 RVA: 0x000B669B File Offset: 0x000B489B
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06002352 RID: 9042 RVA: 0x000B669E File Offset: 0x000B489E
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06002353 RID: 9043 RVA: 0x000B66A6 File Offset: 0x000B48A6
		public bool bStateEnd
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x000B6700 File Offset: 0x000B4900
		public void DeepCopyFromSource(NKMEventHyperSkillCutIn source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTime = source.m_fEventTime;
			this.m_fDurationTime = source.m_fDurationTime;
			this.m_BGEffectName = source.m_BGEffectName;
			this.m_CutInEffectName = source.m_CutInEffectName;
			this.m_CutInEffectAnimName = source.m_CutInEffectAnimName;
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x000B6768 File Offset: 0x000B4968
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			if (cNKMLua.OpenTable("m_Condition"))
			{
				this.m_Condition.LoadFromLUA(cNKMLua);
				cNKMLua.CloseTable();
			}
			cNKMLua.GetData("m_bAnimTime", ref this.m_bAnimTime);
			cNKMLua.GetData("m_fEventTime", ref this.m_fEventTime);
			cNKMLua.GetData("m_fDurationTime", ref this.m_fDurationTime);
			cNKMLua.GetData("m_BGEffectName", ref this.m_BGEffectName);
			cNKMLua.GetData("m_CutInEffectName", ref this.m_CutInEffectName);
			cNKMLua.GetData("m_CutInEffectAnimName", ref this.m_CutInEffectAnimName);
			return true;
		}

		// Token: 0x040024D4 RID: 9428
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x040024D5 RID: 9429
		public bool m_bAnimTime = true;

		// Token: 0x040024D6 RID: 9430
		public float m_fEventTime;

		// Token: 0x040024D7 RID: 9431
		public float m_fDurationTime = 1f;

		// Token: 0x040024D8 RID: 9432
		public string m_BGEffectName = "";

		// Token: 0x040024D9 RID: 9433
		public string m_CutInEffectName = "";

		// Token: 0x040024DA RID: 9434
		public string m_CutInEffectAnimName = "BASE";
	}
}
