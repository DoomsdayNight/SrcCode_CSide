using System;

namespace NKC
{
	// Token: 0x02000724 RID: 1828
	public class NKC_SCEN_OPERATION_UI_DATA
	{
		// Token: 0x060048A6 RID: 18598 RVA: 0x0015EDA1 File Offset: 0x0015CFA1
		public NKC_SCEN_OPERATION_UI_DATA()
		{
			this.Init();
		}

		// Token: 0x060048A7 RID: 18599 RVA: 0x0015EDAF File Offset: 0x0015CFAF
		public void Init()
		{
			if (this.m_NUM_OPERATION_PREFAB != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_NUM_OPERATION_PREFAB);
			}
			if (this.m_NUF_OPERATION_PREFAB != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_NUF_OPERATION_PREFAB);
			}
			this.m_NUM_OPERATION_PREFAB = null;
			this.m_NUF_OPERATION_PREFAB = null;
		}

		// Token: 0x04003854 RID: 14420
		public NKCAssetInstanceData m_NUM_OPERATION_PREFAB;

		// Token: 0x04003855 RID: 14421
		public NKCAssetInstanceData m_NUF_OPERATION_PREFAB;
	}
}
