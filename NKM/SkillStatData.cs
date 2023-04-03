using System;

namespace NKM
{
	// Token: 0x020004B4 RID: 1204
	public class SkillStatData
	{
		// Token: 0x06002184 RID: 8580 RVA: 0x000AB830 File Offset: 0x000A9A30
		public SkillStatData(NKM_STAT_TYPE eType, float value, float rate)
		{
			this.m_NKM_STAT_TYPE = eType;
			this.m_fStatValue = value;
			this.m_fStatRate = rate;
		}

		// Token: 0x0400224D RID: 8781
		public NKM_STAT_TYPE m_NKM_STAT_TYPE;

		// Token: 0x0400224E RID: 8782
		public float m_fStatValue;

		// Token: 0x0400224F RID: 8783
		public float m_fStatRate;
	}
}
