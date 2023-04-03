using System;

namespace NKC
{
	// Token: 0x02000731 RID: 1841
	public class NKC_SCEN_VOICE_LIST_UI_DATA
	{
		// Token: 0x06004952 RID: 18770 RVA: 0x00160F47 File Offset: 0x0015F147
		public NKC_SCEN_VOICE_LIST_UI_DATA()
		{
			this.Init();
		}

		// Token: 0x06004953 RID: 18771 RVA: 0x00160F55 File Offset: 0x0015F155
		public void Init()
		{
			if (this.m_NUF_VOICE_LIST_PREFAB != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_NUF_VOICE_LIST_PREFAB);
			}
			this.m_NUF_VOICE_LIST_PREFAB = null;
		}

		// Token: 0x04003897 RID: 14487
		public NKCAssetInstanceData m_NUF_VOICE_LIST_PREFAB;
	}
}
