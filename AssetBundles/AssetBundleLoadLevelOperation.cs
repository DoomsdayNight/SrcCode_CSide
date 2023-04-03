using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AssetBundles
{
	// Token: 0x0200004E RID: 78
	public class AssetBundleLoadLevelOperation : AssetBundleLoadOperation
	{
		// Token: 0x06000262 RID: 610 RVA: 0x0000A664 File Offset: 0x00008864
		public AssetBundleLoadLevelOperation(string assetbundleName, string levelName, bool isAdditive, AssetBundleLoadLevelOperation.OnComplete dOnComplete)
		{
			this.m_AssetBundleName = assetbundleName;
			this.m_LevelName = levelName;
			this.m_IsAdditive = isAdditive;
			this.m_OnComplete = dOnComplete;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000A68C File Offset: 0x0000888C
		public override bool Update()
		{
			if (this.m_Request != null)
			{
				return false;
			}
			if (AssetBundleManager.GetLoadedAssetBundle(this.m_AssetBundleName, out this.m_DownloadingError) != null)
			{
				if (this.m_IsAdditive)
				{
					this.m_Request = SceneManager.LoadSceneAsync(this.m_LevelName, LoadSceneMode.Additive);
				}
				else
				{
					this.m_Request = SceneManager.LoadSceneAsync(this.m_LevelName, LoadSceneMode.Single);
				}
				if (this.m_Request == null)
				{
					this.m_DownloadingError = "Asset Request failed";
				}
				this.m_Request.completed += delegate(AsyncOperation handle)
				{
					AssetBundleLoadLevelOperation.OnComplete onComplete = this.m_OnComplete;
					if (onComplete == null)
					{
						return;
					}
					onComplete();
				};
				return false;
			}
			return true;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000A711 File Offset: 0x00008911
		public override bool IsDone()
		{
			if (this.m_Request == null && this.m_DownloadingError != null)
			{
				Debug.LogError(this.m_DownloadingError);
				return true;
			}
			return this.m_Request != null && this.m_Request.isDone;
		}

		// Token: 0x040001BD RID: 445
		protected string m_AssetBundleName;

		// Token: 0x040001BE RID: 446
		protected string m_LevelName;

		// Token: 0x040001BF RID: 447
		protected bool m_IsAdditive;

		// Token: 0x040001C0 RID: 448
		protected string m_DownloadingError;

		// Token: 0x040001C1 RID: 449
		protected AsyncOperation m_Request;

		// Token: 0x040001C2 RID: 450
		protected AssetBundleLoadLevelOperation.OnComplete m_OnComplete;

		// Token: 0x020010FF RID: 4351
		// (Invoke) Token: 0x06009EE0 RID: 40672
		public delegate void OnComplete();
	}
}
