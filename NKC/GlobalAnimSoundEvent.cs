using System;
using System.Collections.Generic;
using NKM;

namespace NKC
{
	// Token: 0x020006CE RID: 1742
	public class GlobalAnimSoundEvent
	{
		// Token: 0x06003CBF RID: 15551 RVA: 0x001386CC File Offset: 0x001368CC
		public void DeepCopyFromSource(GlobalAnimSoundEvent source)
		{
			this.m_AssetName = source.m_AssetName;
			this.m_AniName = source.m_AniName;
			this.m_fTime = source.m_fTime;
			this.m_listSoundName.Clear();
			for (int i = 0; i < source.m_listSoundName.Count; i++)
			{
				this.m_listSoundName.Add(source.m_listSoundName[i]);
			}
		}

		// Token: 0x06003CC0 RID: 15552 RVA: 0x00138738 File Offset: 0x00136938
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			cNKMLua.GetData("m_AssetName", ref this.m_AssetName);
			cNKMLua.GetData("m_AniName", ref this.m_AniName);
			cNKMLua.GetData("m_fTime", ref this.m_fTime);
			if (cNKMLua.OpenTable("m_listSoundName"))
			{
				this.m_listSoundName.Clear();
				int num = 1;
				string item = "";
				while (cNKMLua.GetData(num, ref item))
				{
					this.m_listSoundName.Add(item);
					num++;
				}
				cNKMLua.CloseTable();
			}
			return true;
		}

		// Token: 0x040035FA RID: 13818
		public string m_AssetName = "";

		// Token: 0x040035FB RID: 13819
		public string m_AniName = "";

		// Token: 0x040035FC RID: 13820
		public float m_fTime;

		// Token: 0x040035FD RID: 13821
		public List<string> m_listSoundName = new List<string>();
	}
}
