using System;
using System.IO;
using UnityEngine;

namespace AssetBundles
{
	// Token: 0x02000053 RID: 83
	public class AssetBundleLoadFromFileOperation : AssetBundleDownloadOperation
	{
		// Token: 0x06000272 RID: 626 RVA: 0x0000A89F File Offset: 0x00008A9F
		public AssetBundleLoadFromFileOperation(string assetBundleName, string path) : base(assetBundleName)
		{
			this.m_Stream = AssetBundleManager.OpenCryptoBundleFileStream(path);
			this.m_Request = AssetBundle.LoadFromStreamAsync(this.m_Stream);
			this.m_Path = path;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000A8CC File Offset: 0x00008ACC
		protected override bool downloadIsDone
		{
			get
			{
				return this.m_Request == null || this.m_Request.isDone;
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000A8E4 File Offset: 0x00008AE4
		protected override void FinishDownload()
		{
			if (this.m_Request != null)
			{
				if (this.m_Request.assetBundle == null)
				{
					base.error = string.Format("{0} is not a valid asset bundle.", base.assetBundleName);
				}
				else
				{
					base.assetBundle = new LoadedAssetBundle(this.m_Request.assetBundle, this.m_Stream);
				}
			}
			this.m_Request = null;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000A947 File Offset: 0x00008B47
		public override string GetSourceURL()
		{
			return this.m_Path;
		}

		// Token: 0x040001C9 RID: 457
		private string m_Path;

		// Token: 0x040001CA RID: 458
		private AssetBundleCreateRequest m_Request;

		// Token: 0x040001CB RID: 459
		private Stream m_Stream;
	}
}
