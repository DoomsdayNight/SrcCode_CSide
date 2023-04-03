using System;
using AssetBundles;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200063C RID: 1596
	public class NKCAssetResourceData
	{
		// Token: 0x060031AC RID: 12716 RVA: 0x000F6B9F File Offset: 0x000F4D9F
		public bool GetLoadFail()
		{
			return this.m_bLoadFail;
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x060031AD RID: 12717 RVA: 0x000F6BA7 File Offset: 0x000F4DA7
		// (set) Token: 0x060031AE RID: 12718 RVA: 0x000F6BAF File Offset: 0x000F4DAF
		public AssetBundleLoadAssetOperation m_Operation { get; protected set; }

		// Token: 0x060031AF RID: 12719 RVA: 0x000F6BB8 File Offset: 0x000F4DB8
		public NKCAssetResourceData(string bundleName, string assetName, bool bAsync)
		{
			this.m_NKMAssetName = new NKMAssetName();
			this.Init(bundleName, assetName, bAsync);
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x000F6BD4 File Offset: 0x000F4DD4
		public void Init(string bundleName, string assetName, bool bAsync)
		{
			this.Unload();
			this.m_NKMAssetName.m_BundleName = bundleName;
			this.m_NKMAssetName.m_AssetName = assetName;
			this.m_RefCount = 1;
			this.m_bAsync = bAsync;
			this.m_bLoadFail = false;
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x000F6C0C File Offset: 0x000F4E0C
		public void BeginLoad<T>() where T : UnityEngine.Object
		{
			this.m_resType = typeof(T);
			this.m_fTime = Time.time;
			if (this.m_bAsync)
			{
				this.m_Operation = AssetBundleManager.LoadAssetAsync(this.m_NKMAssetName.m_BundleName, this.m_NKMAssetName.m_AssetName, typeof(T));
				return;
			}
			this.m_Asset = AssetBundleManager.LoadAsset<T>(this.m_NKMAssetName.m_BundleName, this.m_NKMAssetName.m_AssetName);
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x000F6C90 File Offset: 0x000F4E90
		public T GetAsset<T>() where T : UnityEngine.Object
		{
			if (this.m_bAsync)
			{
				if (this.m_Operation == null)
				{
					Debug.LogError("Asset Load Operation Null! " + this.m_NKMAssetName.m_BundleName + " " + this.m_NKMAssetName.m_AssetName);
					this.m_bLoadFail = true;
					return default(T);
				}
				if (!this.IsDone())
				{
					Debug.LogWarning("Tried loading too soon, wait for IsDone()");
					return default(T);
				}
				if (this.m_Asset == null)
				{
					this.m_Asset = this.m_Operation.GetAsset<T>();
				}
			}
			return this.m_Asset as T;
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x000F6D3B File Offset: 0x000F4F3B
		public bool IsDone()
		{
			return !this.m_bAsync || this.m_Operation == null || this.m_Operation.IsDone();
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x000F6D5C File Offset: 0x000F4F5C
		public void Unload()
		{
			if (this.m_NKMAssetName.m_BundleName.Length > 1)
			{
				AssetBundleManager.UnloadAssetBundle(this.m_NKMAssetName.m_BundleName);
			}
			this.m_resType = null;
			this.m_NKMAssetName.m_AssetName = "";
			this.m_NKMAssetName.m_BundleName = "";
			this.m_Asset = null;
			this.m_RefCount = 0;
			this.callBack = null;
			this.m_Operation = null;
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x000F6DD0 File Offset: 0x000F4FD0
		public void ForceSyncLoad()
		{
			if (this.m_bAsync && this.m_Operation != null && !this.IsDone())
			{
				this.m_Operation = null;
				this.m_bAsync = false;
				this.m_Asset = AssetBundleManager.LoadAsset<UnityEngine.Object>(this.m_NKMAssetName.m_BundleName, this.m_NKMAssetName.m_AssetName);
			}
		}

		// Token: 0x040030D4 RID: 12500
		public Type m_resType;

		// Token: 0x040030D5 RID: 12501
		public NKMAssetName m_NKMAssetName;

		// Token: 0x040030D6 RID: 12502
		protected UnityEngine.Object m_Asset;

		// Token: 0x040030D7 RID: 12503
		public int m_RefCount;

		// Token: 0x040030D8 RID: 12504
		public bool m_bAsync;

		// Token: 0x040030D9 RID: 12505
		public CallBackHandler callBack;

		// Token: 0x040030DA RID: 12506
		public bool m_bLoadFail;

		// Token: 0x040030DB RID: 12507
		public float m_fTime;
	}
}
