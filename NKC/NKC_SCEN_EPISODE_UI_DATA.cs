using System;

namespace NKC
{
	// Token: 0x0200070B RID: 1803
	public class NKC_SCEN_EPISODE_UI_DATA
	{
		// Token: 0x060046CE RID: 18126 RVA: 0x00157DCC File Offset: 0x00155FCC
		public NKC_SCEN_EPISODE_UI_DATA()
		{
			this.Init();
		}

		// Token: 0x060046CF RID: 18127 RVA: 0x00157DDA File Offset: 0x00155FDA
		public void Init()
		{
			if (this.m_NUM_EPISODE_PREFAB != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_NUM_EPISODE_PREFAB);
			}
			if (this.m_NUF_EPISODE_PREFAB != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_NUF_EPISODE_PREFAB);
			}
			this.m_NUM_EPISODE_PREFAB = null;
			this.m_NUF_EPISODE_PREFAB = null;
		}

		// Token: 0x040037B1 RID: 14257
		public NKCAssetInstanceData m_NUM_EPISODE_PREFAB;

		// Token: 0x040037B2 RID: 14258
		public NKCAssetInstanceData m_NUF_EPISODE_PREFAB;
	}
}
