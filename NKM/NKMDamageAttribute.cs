using System;

namespace NKM
{
	// Token: 0x020003D0 RID: 976
	public class NKMDamageAttribute
	{
		// Token: 0x060019C4 RID: 6596 RVA: 0x0006DF5E File Offset: 0x0006C15E
		public void LoadFromLua(NKMLua cNKMLua)
		{
			cNKMLua.GetData("m_bTrueDamage", ref this.m_bTrueDamage);
			cNKMLua.GetData("m_bForceCritical", ref this.m_bForceCritical);
			cNKMLua.GetData("m_bNoCritical", ref this.m_bNoCritical);
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x0006DF96 File Offset: 0x0006C196
		public static NKMDamageAttribute LoadFromLua(NKMLua cNKMLua, string tableName)
		{
			if (cNKMLua.OpenTable(tableName))
			{
				NKMDamageAttribute nkmdamageAttribute = new NKMDamageAttribute();
				nkmdamageAttribute.LoadFromLua(cNKMLua);
				cNKMLua.CloseTable();
				return nkmdamageAttribute;
			}
			return null;
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x0006DFB6 File Offset: 0x0006C1B6
		public void DeepCopyFromSource(NKMDamageAttribute source)
		{
			this.m_bTrueDamage = source.m_bTrueDamage;
			this.m_bForceCritical = source.m_bForceCritical;
			this.m_bNoCritical = source.m_bNoCritical;
		}

		// Token: 0x04001289 RID: 4745
		public bool m_bTrueDamage;

		// Token: 0x0400128A RID: 4746
		public bool m_bForceCritical;

		// Token: 0x0400128B RID: 4747
		public bool m_bNoCritical;
	}
}
