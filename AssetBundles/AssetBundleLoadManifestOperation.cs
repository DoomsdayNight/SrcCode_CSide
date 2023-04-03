using System;
using UnityEngine;

namespace AssetBundles
{
	// Token: 0x02000052 RID: 82
	public class AssetBundleLoadManifestOperation : AssetBundleLoadAssetOperationFull
	{
		// Token: 0x06000270 RID: 624 RVA: 0x0000A868 File Offset: 0x00008A68
		public AssetBundleLoadManifestOperation(string bundleName, string assetName, Type type) : base(bundleName, assetName, type)
		{
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000A873 File Offset: 0x00008A73
		public override bool Update()
		{
			base.Update();
			if (this.m_Request != null && this.m_Request.isDone)
			{
				AssetBundleManager.AssetBundleManifestObject = this.GetAsset<AssetBundleManifest>();
				return false;
			}
			return true;
		}
	}
}
