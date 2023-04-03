using System;
using NKM;

namespace NKC
{
	// Token: 0x020006CC RID: 1740
	public class NKCScenMusicData
	{
		// Token: 0x06003CBA RID: 15546 RVA: 0x001384D3 File Offset: 0x001366D3
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			cNKMLua.GetData<NKM_SCEN_ID>("m_NKM_SCEN_ID", ref this.m_NKM_SCEN_ID);
			cNKMLua.GetData("m_MusicName", ref this.m_MusicName);
			return true;
		}

		// Token: 0x06003CBB RID: 15547 RVA: 0x001384FA File Offset: 0x001366FA
		public void DeepCopyFromSource(NKCScenMusicData source)
		{
			this.m_NKM_SCEN_ID = source.m_NKM_SCEN_ID;
			this.m_MusicName = source.m_MusicName;
		}

		// Token: 0x040035EC RID: 13804
		public NKM_SCEN_ID m_NKM_SCEN_ID;

		// Token: 0x040035ED RID: 13805
		public string m_MusicName = "";
	}
}
