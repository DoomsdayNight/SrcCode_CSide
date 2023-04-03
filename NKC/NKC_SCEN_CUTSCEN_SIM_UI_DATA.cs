using System;

namespace NKC
{
	// Token: 0x02000701 RID: 1793
	public class NKC_SCEN_CUTSCEN_SIM_UI_DATA
	{
		// Token: 0x06004664 RID: 18020 RVA: 0x00156275 File Offset: 0x00154475
		public NKC_SCEN_CUTSCEN_SIM_UI_DATA()
		{
			this.Init();
		}

		// Token: 0x06004665 RID: 18021 RVA: 0x00156283 File Offset: 0x00154483
		public void Init()
		{
			if (this.m_NUF_CUTSCEN_SIM_PREFAB != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_NUF_CUTSCEN_SIM_PREFAB);
			}
			this.m_NUF_CUTSCEN_SIM_PREFAB = null;
		}

		// Token: 0x0400377B RID: 14203
		public NKCAssetInstanceData m_NUF_CUTSCEN_SIM_PREFAB;
	}
}
