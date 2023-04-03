using System;
using UnityEngine;

namespace AssetBundles
{
	// Token: 0x02000051 RID: 81
	public class AssetBundleLoadAssetOperationFull : AssetBundleLoadAssetOperation
	{
		// Token: 0x0600026C RID: 620 RVA: 0x0000A786 File Offset: 0x00008986
		public AssetBundleLoadAssetOperationFull(string bundleName, string assetName, Type type)
		{
			this.m_AssetBundleName = bundleName;
			this.m_AssetName = assetName;
			this.m_Type = type;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000A7A4 File Offset: 0x000089A4
		public override T GetAsset<T>()
		{
			if (this.m_Request != null && this.m_Request.isDone)
			{
				return this.m_Request.asset as T;
			}
			return default(T);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000A7E8 File Offset: 0x000089E8
		public override bool Update()
		{
			if (this.m_Request != null)
			{
				return false;
			}
			LoadedAssetBundle loadedAssetBundle = AssetBundleManager.GetLoadedAssetBundle(this.m_AssetBundleName, out this.m_DownloadingError);
			if (loadedAssetBundle != null)
			{
				this.m_Request = loadedAssetBundle.m_AssetBundle.LoadAssetAsync(this.m_AssetName, this.m_Type);
				return false;
			}
			return true;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000A834 File Offset: 0x00008A34
		public override bool IsDone()
		{
			if (this.m_Request == null && this.m_DownloadingError != null)
			{
				Debug.LogError(this.m_DownloadingError);
				return true;
			}
			return this.m_Request != null && this.m_Request.isDone;
		}

		// Token: 0x040001C4 RID: 452
		protected string m_AssetBundleName;

		// Token: 0x040001C5 RID: 453
		protected string m_AssetName;

		// Token: 0x040001C6 RID: 454
		protected string m_DownloadingError;

		// Token: 0x040001C7 RID: 455
		protected Type m_Type;

		// Token: 0x040001C8 RID: 456
		protected AssetBundleRequest m_Request;
	}
}
