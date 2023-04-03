using System;
using UnityEngine;

namespace AssetBundles
{
	// Token: 0x02000054 RID: 84
	public class ResourceLoadAssetOperation : AssetBundleLoadAssetOperation
	{
		// Token: 0x06000276 RID: 630 RVA: 0x0000A94F File Offset: 0x00008B4F
		public ResourceLoadAssetOperation(string resourcePath)
		{
			this.m_Asset = Resources.Load(resourcePath);
			if (this.m_Asset == null)
			{
				Debug.LogError(resourcePath + " not found from internal resources");
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000A981 File Offset: 0x00008B81
		public override T GetAsset<T>()
		{
			return this.m_Asset as T;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000A993 File Offset: 0x00008B93
		public override bool Update()
		{
			return false;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000A996 File Offset: 0x00008B96
		public override bool IsDone()
		{
			return true;
		}

		// Token: 0x040001CC RID: 460
		private UnityEngine.Object m_Asset;
	}
}
