using System;

namespace NKC
{
	// Token: 0x02000703 RID: 1795
	public class NKC_SCEN_DIVE_UI_DATA
	{
		// Token: 0x0600466E RID: 18030 RVA: 0x001563CA File Offset: 0x001545CA
		public NKC_SCEN_DIVE_UI_DATA()
		{
			this.Init();
		}

		// Token: 0x0600466F RID: 18031 RVA: 0x001563D8 File Offset: 0x001545D8
		public void Init()
		{
			if (this.m_NUM_DIVE_PREFAB != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_NUM_DIVE_PREFAB);
			}
			if (this.m_NUF_DIVE_PREFAB != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_NUF_DIVE_PREFAB);
			}
			this.m_NUM_DIVE_PREFAB = null;
			this.m_NUF_DIVE_PREFAB = null;
		}

		// Token: 0x0400377F RID: 14207
		public NKCAssetInstanceData m_NUM_DIVE_PREFAB;

		// Token: 0x04003780 RID: 14208
		public NKCAssetInstanceData m_NUF_DIVE_PREFAB;
	}
}
