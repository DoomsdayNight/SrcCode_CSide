using System;
using System.Collections.Generic;

namespace NKM
{
	// Token: 0x02000526 RID: 1318
	public class NKCEnemyData
	{
		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x0600259D RID: 9629 RVA: 0x000C1D57 File Offset: 0x000BFF57
		public string Key
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_ChangeUnitName))
				{
					return this.m_UnitStrID;
				}
				return string.Format("{0}@{1}", this.m_UnitStrID, this.m_ChangeUnitName);
			}
		}

		// Token: 0x0400271C RID: 10012
		public string m_UnitStrID = "";

		// Token: 0x0400271D RID: 10013
		public string m_ChangeUnitName = "";

		// Token: 0x0400271E RID: 10014
		public int m_SkinID;

		// Token: 0x0400271F RID: 10015
		public NKM_BOSS_TYPE m_NKM_BOSS_TYPE;

		// Token: 0x04002720 RID: 10016
		public int m_Level;

		// Token: 0x02001248 RID: 4680
		public class CompNED : IComparer<NKCEnemyData>
		{
			// Token: 0x0600A295 RID: 41621 RVA: 0x00341404 File Offset: 0x0033F604
			public int Compare(NKCEnemyData x, NKCEnemyData y)
			{
				if (x == null)
				{
					return 1;
				}
				if (y == null)
				{
					return -1;
				}
				if (y.m_NKM_BOSS_TYPE > x.m_NKM_BOSS_TYPE)
				{
					return 1;
				}
				if (y.m_NKM_BOSS_TYPE < x.m_NKM_BOSS_TYPE)
				{
					return -1;
				}
				if (y.m_Level >= x.m_Level)
				{
					return 1;
				}
				return -1;
			}
		}
	}
}
