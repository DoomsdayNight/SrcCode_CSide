using System;

namespace NKM
{
	// Token: 0x02000429 RID: 1065
	public class NKMMapLayer
	{
		// Token: 0x06001D03 RID: 7427 RVA: 0x000872DA File Offset: 0x000854DA
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			cNKMLua.GetData("m_LayerName", ref this.m_LayerName);
			cNKMLua.GetData("m_fMoveFactor", ref this.m_fMoveFactor);
			return true;
		}

		// Token: 0x04001C66 RID: 7270
		public string m_LayerName = "";

		// Token: 0x04001C67 RID: 7271
		public float m_fMoveFactor;
	}
}
