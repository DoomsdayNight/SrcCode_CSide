using System;

namespace NKC.Patcher
{
	// Token: 0x02000890 RID: 2192
	public static class PatchManifestPath
	{
		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x06005736 RID: 22326 RVA: 0x001A3612 File Offset: 0x001A1812
		public static string ServerBaseAddress
		{
			get
			{
				return NKCPatchDownloader.Instance.ServerBaseAddress;
			}
		}

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x06005737 RID: 22327 RVA: 0x001A361E File Offset: 0x001A181E
		public static string LocalDownloadPath
		{
			get
			{
				return NKCPatchDownloader.Instance.LocalDownloadPath ?? "";
			}
		}

		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x06005738 RID: 22328 RVA: 0x001A3633 File Offset: 0x001A1833
		public static string ExtraServerBaseAddress
		{
			get
			{
				return NKCPatchDownloader.Instance.ExtraServerBaseAddress;
			}
		}

		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x06005739 RID: 22329 RVA: 0x001A363F File Offset: 0x001A183F
		public static string ExtraLocalDownloadPath
		{
			get
			{
				return NKCPatchDownloader.Instance.ExtraLocalDownloadPath ?? "";
			}
		}

		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x0600573A RID: 22330 RVA: 0x001A3654 File Offset: 0x001A1854
		public static string CurrentManifestPath
		{
			get
			{
				return PatchManifestPath.LocalDownloadPath + "PatchInfo.json";
			}
		}

		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x0600573B RID: 22331 RVA: 0x001A3665 File Offset: 0x001A1865
		public static string InnerManifestPath
		{
			get
			{
				return NKCPatchUtility.GetInnerAssetPath("PatchInfo.json", false);
			}
		}

		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x0600573C RID: 22332 RVA: 0x001A3672 File Offset: 0x001A1872
		public static string LatestManifestPath
		{
			get
			{
				return PatchManifestPath.LocalDownloadPath + "LatestPatchInfo.json";
			}
		}

		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x0600573D RID: 22333 RVA: 0x001A3683 File Offset: 0x001A1883
		public static string TempManifestPath
		{
			get
			{
				return PatchManifestPath.LocalDownloadPath + "TempPatchInfo.json";
			}
		}

		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x0600573E RID: 22334 RVA: 0x001A3694 File Offset: 0x001A1894
		public static string BackgroundDownloadHistoryManifestPath
		{
			get
			{
				return PatchManifestPath.LocalDownloadPath + "BackgroundDownloadHistoryPatchInfo.json";
			}
		}

		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x0600573F RID: 22335 RVA: 0x001A36A5 File Offset: 0x001A18A5
		public static string FilteredManifestPath
		{
			get
			{
				return PatchManifestPath.LocalDownloadPath + "FilteredPatchInfo.json";
			}
		}

		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x06005740 RID: 22336 RVA: 0x001A36B6 File Offset: 0x001A18B6
		public static string LatestExtraManifestPath
		{
			get
			{
				return PatchManifestPath.ExtraLocalDownloadPath + "LatestExtraPatchInfo.json";
			}
		}

		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x06005741 RID: 22337 RVA: 0x001A36C7 File Offset: 0x001A18C7
		public static string CurrentExtraManifestPath
		{
			get
			{
				return PatchManifestPath.ExtraLocalDownloadPath + "PatchInfo.json";
			}
		}

		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x06005742 RID: 22338 RVA: 0x001A36D8 File Offset: 0x001A18D8
		public static string TempExtraManifestPath
		{
			get
			{
				return PatchManifestPath.ExtraLocalDownloadPath + "TempPatchInfo.json";
			}
		}

		// Token: 0x06005743 RID: 22339 RVA: 0x001A36EC File Offset: 0x001A18EC
		public static string GetLocalPathBy(PatchManifestPath.PatchType type)
		{
			switch (type)
			{
			case PatchManifestPath.PatchType.CurrentManifest:
				if (!NKCPatchUtility.IsFileExists(PatchManifestPath.CurrentManifestPath))
				{
					return PatchManifestPath.InnerManifestPath;
				}
				return PatchManifestPath.CurrentManifestPath;
			case PatchManifestPath.PatchType.InnerManifest:
				return PatchManifestPath.InnerManifestPath;
			case PatchManifestPath.PatchType.LatestManifest:
				return PatchManifestPath.LatestManifestPath;
			case PatchManifestPath.PatchType.TempManifest:
				return PatchManifestPath.TempManifestPath;
			case PatchManifestPath.PatchType.BackgroundDownloadHistoryManifest:
				return PatchManifestPath.BackgroundDownloadHistoryManifestPath;
			case PatchManifestPath.PatchType.FilteredManifest:
				return PatchManifestPath.FilteredManifestPath;
			case PatchManifestPath.PatchType.CurrentExtraManifestPath:
				return PatchManifestPath.CurrentExtraManifestPath;
			case PatchManifestPath.PatchType.LatestExtraManifest:
				return PatchManifestPath.LatestExtraManifestPath;
			case PatchManifestPath.PatchType.TempExtraManifest:
				return PatchManifestPath.TempExtraManifestPath;
			default:
				throw new ArgumentOutOfRangeException("type", type, null);
			}
		}

		// Token: 0x06005744 RID: 22340 RVA: 0x001A377E File Offset: 0x001A197E
		public static string GetServerBasePath(bool extra)
		{
			if (!extra)
			{
				return PatchManifestPath.ServerBaseAddress;
			}
			return PatchManifestPath.ExtraServerBaseAddress;
		}

		// Token: 0x06005745 RID: 22341 RVA: 0x001A378E File Offset: 0x001A198E
		public static string GetLocalDownloadPath(bool extra)
		{
			if (!extra)
			{
				return PatchManifestPath.LocalDownloadPath;
			}
			return PatchManifestPath.ExtraLocalDownloadPath;
		}

		// Token: 0x06005746 RID: 22342 RVA: 0x001A379E File Offset: 0x001A199E
		public static string GetManifestPath(bool extra)
		{
			if (!extra)
			{
				return PatchManifestPath.LatestManifestPath;
			}
			return PatchManifestPath.LatestExtraManifestPath;
		}

		// Token: 0x06005747 RID: 22343 RVA: 0x001A37AE File Offset: 0x001A19AE
		public static string GetManifestFileName(bool extra)
		{
			if (!extra)
			{
				return "LatestPatchInfo.json";
			}
			return "LatestExtraPatchInfo.json";
		}

		// Token: 0x0400451D RID: 17693
		public static string TutorialPatchFileName = "tutorialDungeonResources.json";

		// Token: 0x0400451E RID: 17694
		public const string manifestFileName = "PatchInfo.json";

		// Token: 0x0400451F RID: 17695
		public const string LatestManifestFileName = "LatestPatchInfo.json";

		// Token: 0x04004520 RID: 17696
		public const string LatestExtraManifestFileName = "LatestExtraPatchInfo.json";

		// Token: 0x04004521 RID: 17697
		public const string tempManifestFileName = "TempPatchInfo.json";

		// Token: 0x04004522 RID: 17698
		public const string filteredManifestFileName = "FilteredPatchInfo.json";

		// Token: 0x04004523 RID: 17699
		public const string BackgroundDownloadHistoryManifestFileName = "BackgroundDownloadHistoryPatchInfo.json";

		// Token: 0x02001560 RID: 5472
		public enum PatchType
		{
			// Token: 0x0400A0C8 RID: 41160
			CurrentManifest,
			// Token: 0x0400A0C9 RID: 41161
			InnerManifest,
			// Token: 0x0400A0CA RID: 41162
			LatestManifest,
			// Token: 0x0400A0CB RID: 41163
			TempManifest,
			// Token: 0x0400A0CC RID: 41164
			BackgroundDownloadHistoryManifest,
			// Token: 0x0400A0CD RID: 41165
			FilteredManifest,
			// Token: 0x0400A0CE RID: 41166
			CurrentExtraManifestPath,
			// Token: 0x0400A0CF RID: 41167
			LatestExtraManifest,
			// Token: 0x0400A0D0 RID: 41168
			TempExtraManifest
		}
	}
}
