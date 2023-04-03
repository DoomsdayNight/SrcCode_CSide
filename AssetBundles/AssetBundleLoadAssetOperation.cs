using System;
using UnityEngine;

namespace AssetBundles
{
	// Token: 0x0200004F RID: 79
	public abstract class AssetBundleLoadAssetOperation : AssetBundleLoadOperation
	{
		// Token: 0x06000266 RID: 614
		public abstract T GetAsset<T>() where T : UnityEngine.Object;
	}
}
