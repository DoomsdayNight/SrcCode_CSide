using System;
using NKM;

namespace NKC
{
	// Token: 0x0200066E RID: 1646
	public class NKCEffectTemplet
	{
		// Token: 0x060033FE RID: 13310 RVA: 0x00104D43 File Offset: 0x00102F43
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			cNKMLua.GetData(1, ref this.m_Name);
			cNKMLua.GetData(2, ref this.m_PoolCount);
			return true;
		}

		// Token: 0x04003259 RID: 12889
		public string m_Name = "";

		// Token: 0x0400325A RID: 12890
		public int m_PoolCount;
	}
}
