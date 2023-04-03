using System;
using System.Collections.Generic;
using AssetBundles;

namespace NKC.Patcher
{
	// Token: 0x0200088A RID: 2186
	public class OptimizationPatchInfoController
	{
		// Token: 0x06005709 RID: 22281 RVA: 0x001A2D9C File Offset: 0x001A0F9C
		public NKCPatchInfo CreateTutorialOnlyManifestInfo(NKCPatchInfo patchInfo, List<string> lstVariants)
		{
			this._tutorialPatchInfo = patchInfo.FilterByVariants(lstVariants);
			List<string> list = AssetBundleManager.LoadSavedAssetBundleLists(PatchManifestPath.TutorialPatchFileName);
			if (list.Count == 0)
			{
				this._tutorialPatchInfo = null;
				return null;
			}
			list.Add(Utility.GetPlatformName().ToLower());
			foreach (string text in this._tutorialPatchInfo.m_dicPatchInfo.Keys)
			{
				string text2 = text.Split(new char[]
				{
					'.'
				})[0].ToLower();
				if (text2.Contains("ab_script"))
				{
					list.Add(text2);
				}
				else if (text2.Contains("login"))
				{
					list.Add(text2);
				}
				else if (text2.Contains("ab_music/cutscen"))
				{
					list.Add(text2);
				}
				else if (text2.Contains("tutorial"))
				{
					list.Add(text2);
				}
				else if (text2.Contains("ab_sound_fx"))
				{
					list.Add(text2);
				}
			}
			this._tutorialPatchInfo = this._tutorialPatchInfo.MakePatchinfoSubset(list);
			return this._tutorialPatchInfo;
		}

		// Token: 0x0600570A RID: 22282 RVA: 0x001A2ECC File Offset: 0x001A10CC
		public NKCPatchInfo LoadBackgroundDownloadHistoryPatchInfo()
		{
			this._backgroundDownloadHistoryPatchInfo = PatchManifestManager.LoadManifest(PatchManifestPath.PatchType.BackgroundDownloadHistoryManifest);
			return this._backgroundDownloadHistoryPatchInfo;
		}

		// Token: 0x0600570B RID: 22283 RVA: 0x001A2EE0 File Offset: 0x001A10E0
		public NKCPatchInfo GetBackgroundDownloadHistoryPatchInfo()
		{
			if (this._backgroundDownloadHistoryPatchInfo == null)
			{
				this.LoadBackgroundDownloadHistoryPatchInfo();
			}
			return this._backgroundDownloadHistoryPatchInfo;
		}

		// Token: 0x0600570C RID: 22284 RVA: 0x001A2EF7 File Offset: 0x001A10F7
		public void CleanUp()
		{
			this._tutorialPatchInfo = null;
			this._backgroundDownloadHistoryPatchInfo = null;
		}

		// Token: 0x040044FB RID: 17659
		private NKCPatchInfo _tutorialPatchInfo;

		// Token: 0x040044FC RID: 17660
		private NKCPatchInfo _nonEssentialPatchInfo;

		// Token: 0x040044FD RID: 17661
		private NKCPatchInfo _backgroundDownloadHistoryPatchInfo;
	}
}
