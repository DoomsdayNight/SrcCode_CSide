using System;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x020009A8 RID: 2472
	public class NKCUIInstantiatable : MonoBehaviour
	{
		// Token: 0x06006701 RID: 26369 RVA: 0x002105B8 File Offset: 0x0020E7B8
		protected static T OpenInstance<T>(string BundleName, string AssetName, Transform parent) where T : NKCUIInstantiatable
		{
			GameObject asset = NKCAssetResourceManager.OpenResource<GameObject>(BundleName, AssetName, false, null).GetAsset<GameObject>();
			if (asset != null)
			{
				T component = UnityEngine.Object.Instantiate<GameObject>(asset, parent).GetComponent<T>();
				if (component != null)
				{
					NKCUIManager.OpenUI(component.gameObject);
				}
				return component;
			}
			return default(T);
		}

		// Token: 0x06006702 RID: 26370 RVA: 0x00210614 File Offset: 0x0020E814
		protected void CloseInstance(string BundleName, string AssetName)
		{
			NKCAssetResourceManager.CloseResource(BundleName, AssetName);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
