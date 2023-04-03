using System;
using System.IO;
using UnityEngine;

namespace AssetBundles
{
	// Token: 0x02000056 RID: 86
	public class LoadedAssetBundle
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600027B RID: 635 RVA: 0x0000A9AC File Offset: 0x00008BAC
		// (remove) Token: 0x0600027C RID: 636 RVA: 0x0000A9E4 File Offset: 0x00008BE4
		internal event Action unload;

		// Token: 0x0600027D RID: 637 RVA: 0x0000AA1C File Offset: 0x00008C1C
		internal void OnUnload()
		{
			if (this.m_AssetBundle != null)
			{
				this.m_AssetBundle.Unload(false);
			}
			if (this.unload != null)
			{
				this.unload();
			}
			Stream stream = this.m_Stream;
			if (stream != null)
			{
				stream.Dispose();
			}
			this.m_AssetBundle = null;
			this.m_Stream = null;
			this.unload = null;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000AA7C File Offset: 0x00008C7C
		public LoadedAssetBundle(AssetBundle assetBundle, Stream stream)
		{
			this.m_AssetBundle = assetBundle;
			this.m_Stream = stream;
			this.m_ReferencedCount = 1;
		}

		// Token: 0x040001CF RID: 463
		public AssetBundle m_AssetBundle;

		// Token: 0x040001D0 RID: 464
		public int m_ReferencedCount;

		// Token: 0x040001D1 RID: 465
		public Stream m_Stream;
	}
}
