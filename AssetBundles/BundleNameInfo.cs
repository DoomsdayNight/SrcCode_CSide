using System;

namespace AssetBundles
{
	// Token: 0x02000055 RID: 85
	public struct BundleNameInfo
	{
		// Token: 0x0600027A RID: 634 RVA: 0x0000A999 File Offset: 0x00008B99
		public BundleNameInfo(string _bundlename, string _variant)
		{
			this.bundleName = _bundlename;
			this.variant = _variant;
		}

		// Token: 0x040001CD RID: 461
		public string bundleName;

		// Token: 0x040001CE RID: 462
		public string variant;
	}
}
