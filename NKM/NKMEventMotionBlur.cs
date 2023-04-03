using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004E1 RID: 1249
	public class NKMEventMotionBlur : IEventConditionOwner, INKMUnitStateEvent
	{
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x0600233B RID: 9019 RVA: 0x000B5FCB File Offset: 0x000B41CB
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x0600233C RID: 9020 RVA: 0x000B5FD3 File Offset: 0x000B41D3
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTimeMin;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x0600233D RID: 9021 RVA: 0x000B5FDB File Offset: 0x000B41DB
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x0600233E RID: 9022 RVA: 0x000B5FDE File Offset: 0x000B41DE
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x0600233F RID: 9023 RVA: 0x000B5FE6 File Offset: 0x000B41E6
		public bool bStateEnd
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x000B6024 File Offset: 0x000B4224
		public void DeepCopyFromSource(NKMEventMotionBlur source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTimeMin = source.m_fEventTimeMin;
			this.m_fEventTimeMax = source.m_fEventTimeMax;
			this.m_fColorR = source.m_fColorR;
			this.m_fColorG = source.m_fColorG;
			this.m_fColorB = source.m_fColorB;
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x000B608C File Offset: 0x000B428C
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
			cNKMLua.GetData("m_fColorR", ref this.m_fColorR);
			cNKMLua.GetData("m_fColorG", ref this.m_fColorG);
			cNKMLua.GetData("m_fColorB", ref this.m_fColorB);
			return true;
		}

		// Token: 0x040024AB RID: 9387
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x040024AC RID: 9388
		public bool m_bAnimTime = true;

		// Token: 0x040024AD RID: 9389
		public float m_fEventTimeMin;

		// Token: 0x040024AE RID: 9390
		public float m_fEventTimeMax;

		// Token: 0x040024AF RID: 9391
		public float m_fColorR = 0.5f;

		// Token: 0x040024B0 RID: 9392
		public float m_fColorG = 0.5f;

		// Token: 0x040024B1 RID: 9393
		public float m_fColorB = 1f;
	}
}
