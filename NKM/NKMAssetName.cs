using System;

namespace NKM
{
	// Token: 0x0200039C RID: 924
	public class NKMAssetName : IEquatable<NKMAssetName>
	{
		// Token: 0x06001825 RID: 6181 RVA: 0x000619C0 File Offset: 0x0005FBC0
		public NKMAssetName()
		{
			this.Init();
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x000619E4 File Offset: 0x0005FBE4
		public static NKMAssetName ParseBundleName(string defaultBundleName, string assetName)
		{
			if (string.IsNullOrEmpty(assetName))
			{
				return new NKMAssetName(defaultBundleName, assetName);
			}
			if (!assetName.Contains("@"))
			{
				return new NKMAssetName(defaultBundleName, assetName);
			}
			char[] separator = new char[]
			{
				'@'
			};
			string[] array = assetName.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length > 1)
			{
				return new NKMAssetName(array[0], array[1]);
			}
			return new NKMAssetName(defaultBundleName, assetName);
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x00061A44 File Offset: 0x0005FC44
		public static NKMAssetName ParseBundleName(string defaultBundleName, string assetName, string assetNamePrefix)
		{
			if (string.IsNullOrEmpty(assetName))
			{
				return new NKMAssetName(defaultBundleName, assetName);
			}
			char[] separator = new char[]
			{
				'@'
			};
			string[] array = assetName.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length > 1)
			{
				return new NKMAssetName(array[0], assetNamePrefix + array[1]);
			}
			return new NKMAssetName(defaultBundleName, assetNamePrefix + assetName);
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x00061A9C File Offset: 0x0005FC9C
		public NKMAssetName(string bundleName, string assetName)
		{
			if (string.IsNullOrEmpty(assetName) || !assetName.Contains("@"))
			{
				this.m_BundleName = bundleName;
				this.m_AssetName = assetName;
				return;
			}
			char[] separator = new char[]
			{
				'@'
			};
			string[] array = assetName.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length > 1)
			{
				this.m_BundleName = array[0];
				this.m_AssetName = array[1];
				return;
			}
			this.m_BundleName = bundleName;
			this.m_AssetName = assetName;
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00061B25 File Offset: 0x0005FD25
		public void Init()
		{
			this.m_BundleName = string.Empty;
			this.m_AssetName = string.Empty;
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x00061B3D File Offset: 0x0005FD3D
		public void DeepCopyFromSource(NKMAssetName source)
		{
			this.m_BundleName = source.m_BundleName;
			this.m_AssetName = source.m_AssetName;
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00061B57 File Offset: 0x0005FD57
		public void LoadFromLua(NKMLua cNKMLua)
		{
			cNKMLua.GetData(1, ref this.m_BundleName);
			cNKMLua.GetData(2, ref this.m_AssetName);
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x00061B75 File Offset: 0x0005FD75
		public bool LoadFromLua(NKMLua cNKMLua, string pKey)
		{
			if (cNKMLua.OpenTable(pKey))
			{
				this.LoadFromLua(cNKMLua);
				cNKMLua.CloseTable();
				return true;
			}
			return false;
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x00061B91 File Offset: 0x0005FD91
		public bool LoadFromLua(NKMLua cNKMLua, int index)
		{
			if (cNKMLua.OpenTable(index))
			{
				this.LoadFromLua(cNKMLua);
				cNKMLua.CloseTable();
				return true;
			}
			return false;
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x00061BAD File Offset: 0x0005FDAD
		public override int GetHashCode()
		{
			return this.m_BundleName.GetHashCode() * 31 + this.m_AssetName.GetHashCode();
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x00061BC9 File Offset: 0x0005FDC9
		public override string ToString()
		{
			return string.Format("{0}@{1}", this.m_BundleName, this.m_AssetName);
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x00061BE1 File Offset: 0x0005FDE1
		public bool Equals(NKMAssetName other)
		{
			return this.m_BundleName == other.m_BundleName && this.m_AssetName == other.m_AssetName;
		}

		// Token: 0x0400100F RID: 4111
		public string m_BundleName = string.Empty;

		// Token: 0x04001010 RID: 4112
		public string m_AssetName = string.Empty;
	}
}
