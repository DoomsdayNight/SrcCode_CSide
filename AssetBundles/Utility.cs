using System;
using UnityEngine;

namespace AssetBundles
{
	// Token: 0x0200005B RID: 91
	public class Utility
	{
		// Token: 0x060002C7 RID: 711 RVA: 0x0000C291 File Offset: 0x0000A491
		public static string GetPlatformName()
		{
			return Utility.GetPlatformForAssetBundles(Application.platform);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000C2A0 File Offset: 0x0000A4A0
		private static string GetPlatformForAssetBundles(RuntimePlatform platform)
		{
			if (platform <= RuntimePlatform.WindowsPlayer)
			{
				if (platform == RuntimePlatform.OSXPlayer)
				{
					return "OSX";
				}
				if (platform == RuntimePlatform.WindowsPlayer)
				{
					if (IntPtr.Size == 8)
					{
						return "StandaloneWindows64";
					}
					return "StandaloneWindows";
				}
			}
			else
			{
				if (platform == RuntimePlatform.IPhonePlayer)
				{
					return "iOS";
				}
				if (platform == RuntimePlatform.Android)
				{
					return "Android";
				}
				if (platform == RuntimePlatform.WebGLPlayer)
				{
					return "WebGL";
				}
			}
			return null;
		}

		// Token: 0x040001F0 RID: 496
		public const string AssetBundlesOutputPath = "AssetBundles";
	}
}
