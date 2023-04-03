using System;
using NKM;

namespace NKC
{
	// Token: 0x02000662 RID: 1634
	public class NKCCutScenCharTemplet
	{
		// Token: 0x0600335D RID: 13149 RVA: 0x00101134 File Offset: 0x000FF334
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			string nationalPostfix = NKCStringTable.GetNationalPostfix(NKCStringTable.GetNationalCode());
			cNKMLua.GetData("m_CharStrID", ref this.m_CharStrID);
			cNKMLua.GetData("m_CharStr" + nationalPostfix, ref this.m_CharStr);
			cNKMLua.GetData("m_PrefabStr", ref this.m_PrefabStr);
			cNKMLua.GetData("m_Background", ref this.m_Background);
			return true;
		}

		// Token: 0x040031DE RID: 12766
		public string m_CharStrID = "";

		// Token: 0x040031DF RID: 12767
		public string m_CharStr = "";

		// Token: 0x040031E0 RID: 12768
		public string m_PrefabStr = "";

		// Token: 0x040031E1 RID: 12769
		public bool m_Background;
	}
}
