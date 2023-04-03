using System;

namespace NKC
{
	// Token: 0x02000705 RID: 1797
	public class NKC_SCEN_DIVE_READY_UI_DATA
	{
		// Token: 0x06004686 RID: 18054 RVA: 0x001566A3 File Offset: 0x001548A3
		public NKC_SCEN_DIVE_READY_UI_DATA()
		{
			this.Init();
		}

		// Token: 0x06004687 RID: 18055 RVA: 0x001566B1 File Offset: 0x001548B1
		public void Init()
		{
			if (this.m_NUF_LOGIN_PREFAB != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_NUF_LOGIN_PREFAB);
			}
			this.m_NUF_LOGIN_PREFAB = null;
		}

		// Token: 0x04003785 RID: 14213
		public NKCAssetInstanceData m_NUF_LOGIN_PREFAB;
	}
}
