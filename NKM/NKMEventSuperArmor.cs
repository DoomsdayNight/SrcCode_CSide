using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004DA RID: 1242
	public class NKMEventSuperArmor : IEventConditionOwner, INKMUnitStateEvent
	{
		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060022FC RID: 8956 RVA: 0x000B51A7 File Offset: 0x000B33A7
		public NKMEventCondition Condition
		{
			get
			{
				return this.m_Condition;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060022FD RID: 8957 RVA: 0x000B51AF File Offset: 0x000B33AF
		public float EventStartTime
		{
			get
			{
				return this.m_fEventTimeMin;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060022FE RID: 8958 RVA: 0x000B51B7 File Offset: 0x000B33B7
		public EventRollbackType RollbackType
		{
			get
			{
				return EventRollbackType.Allowed;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060022FF RID: 8959 RVA: 0x000B51BA File Offset: 0x000B33BA
		public bool bAnimTime
		{
			get
			{
				return this.m_bAnimTime;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06002300 RID: 8960 RVA: 0x000B51C2 File Offset: 0x000B33C2
		public bool bStateEnd
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x000B51E8 File Offset: 0x000B33E8
		public void DeepCopyFromSource(NKMEventSuperArmor source)
		{
			this.m_Condition.DeepCopyFromSource(source.m_Condition);
			this.m_bAnimTime = source.m_bAnimTime;
			this.m_fEventTimeMin = source.m_fEventTimeMin;
			this.m_fEventTimeMax = source.m_fEventTimeMax;
			this.m_SuperArmorLevel = source.m_SuperArmorLevel;
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x000B5238 File Offset: 0x000B3438
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
			cNKMLua.GetData<NKM_SUPER_ARMOR_LEVEL>("m_SuperArmorLevel", ref this.m_SuperArmorLevel);
			return true;
		}

		// Token: 0x04002467 RID: 9319
		public NKMEventCondition m_Condition = new NKMEventCondition();

		// Token: 0x04002468 RID: 9320
		public bool m_bAnimTime = true;

		// Token: 0x04002469 RID: 9321
		public float m_fEventTimeMin;

		// Token: 0x0400246A RID: 9322
		public float m_fEventTimeMax;

		// Token: 0x0400246B RID: 9323
		public NKM_SUPER_ARMOR_LEVEL m_SuperArmorLevel = NKM_SUPER_ARMOR_LEVEL.NSAL_NO;
	}
}
