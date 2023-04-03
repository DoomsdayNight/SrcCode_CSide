using System;

namespace NKM
{
	// Token: 0x020004BB RID: 1211
	public class NKMGameStatRate
	{
		// Token: 0x060021F6 RID: 8694 RVA: 0x000ADD28 File Offset: 0x000ABF28
		public float GetStatValueRate(NKM_STAT_TYPE eStat)
		{
			switch (eStat)
			{
			case NKM_STAT_TYPE.NST_HP:
				return this.m_HPRate;
			case NKM_STAT_TYPE.NST_ATK:
				return this.m_ATKRate;
			case NKM_STAT_TYPE.NST_DEF:
				return this.m_DEFRate;
			case NKM_STAT_TYPE.NST_CRITICAL:
				return this.m_CRITRate;
			case NKM_STAT_TYPE.NST_HIT:
				return this.m_HITRate;
			case NKM_STAT_TYPE.NST_EVADE:
				return this.m_EvadeRate;
			default:
				return this.m_SubStatValueRate;
			}
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x000ADD85 File Offset: 0x000ABF85
		public float GetStatFactorRate(NKM_STAT_TYPE eStat)
		{
			if (NKMUnitStatManager.IsMainStat(eStat))
			{
				return this.m_MainStatFactorRate;
			}
			return 1f;
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x000ADD9B File Offset: 0x000ABF9B
		public float GetEquipStatRate()
		{
			return this.m_EquipStatRate;
		}

		// Token: 0x040022E9 RID: 8937
		public float m_MainStatFactorRate = 1f;

		// Token: 0x040022EA RID: 8938
		public float m_SubStatValueRate = 1f;

		// Token: 0x040022EB RID: 8939
		public float m_EquipStatRate = 1f;

		// Token: 0x040022EC RID: 8940
		public float m_HPRate = 1f;

		// Token: 0x040022ED RID: 8941
		public float m_ATKRate = 1f;

		// Token: 0x040022EE RID: 8942
		public float m_DEFRate = 1f;

		// Token: 0x040022EF RID: 8943
		public float m_CRITRate = 1f;

		// Token: 0x040022F0 RID: 8944
		public float m_HITRate = 1f;

		// Token: 0x040022F1 RID: 8945
		public float m_EvadeRate = 1f;
	}
}
