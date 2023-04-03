using System;
using Cs.Logging;

namespace NKC.Patcher
{
	// Token: 0x02000889 RID: 2185
	public class ExtraPatchInfoController
	{
		// Token: 0x06005700 RID: 22272 RVA: 0x001A2C8D File Offset: 0x001A0E8D
		public NKCPatchInfo LoadCurrentExtraManifest()
		{
			this._curExtraPatchInfo = PatchManifestManager.LoadManifest(PatchManifestPath.PatchType.CurrentExtraManifestPath);
			return this._curExtraPatchInfo;
		}

		// Token: 0x06005701 RID: 22273 RVA: 0x001A2CA1 File Offset: 0x001A0EA1
		public NKCPatchInfo LoadExtraLatestManifest()
		{
			this._latestExtraPatchInfo = PatchManifestManager.LoadManifest(PatchManifestPath.PatchType.LatestExtraManifest);
			return this._latestExtraPatchInfo;
		}

		// Token: 0x06005702 RID: 22274 RVA: 0x001A2CB5 File Offset: 0x001A0EB5
		public NKCPatchInfo GetCurrentExtraPatchInfo()
		{
			if (this._curExtraPatchInfo == null)
			{
				this.LoadCurrentExtraManifest();
			}
			return this._curExtraPatchInfo;
		}

		// Token: 0x06005703 RID: 22275 RVA: 0x001A2CCC File Offset: 0x001A0ECC
		public NKCPatchInfo GetLatestExtraPatchInfo()
		{
			if (this._latestExtraPatchInfo == null)
			{
				this.LoadExtraLatestManifest();
			}
			return this._latestExtraPatchInfo;
		}

		// Token: 0x06005704 RID: 22276 RVA: 0x001A2CE3 File Offset: 0x001A0EE3
		public NKCPatchInfo LoadDownloadHistoryExtraManifest()
		{
			this._downloadHistoryExtraPatchInfo = PatchManifestManager.LoadManifest(PatchManifestPath.PatchType.TempExtraManifest);
			return this._downloadHistoryExtraPatchInfo;
		}

		// Token: 0x06005705 RID: 22277 RVA: 0x001A2CF7 File Offset: 0x001A0EF7
		public NKCPatchInfo GetDownloadHistoryExtraPatchInfo()
		{
			if (this._downloadHistoryExtraPatchInfo == null)
			{
				this.LoadDownloadHistoryExtraManifest();
			}
			return this._downloadHistoryExtraPatchInfo;
		}

		// Token: 0x06005706 RID: 22278 RVA: 0x001A2D10 File Offset: 0x001A0F10
		public void AppendFilteredManifestToCurrentManifest()
		{
			if (this._curExtraPatchInfo == null)
			{
				Log.Debug("[OverwriteLatestManifestToCurrentManifest] curPatchInfo is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/PatchInfoController/ExtraPatchInfoController.cs", 62);
				return;
			}
			if (this._latestExtraPatchInfo == null)
			{
				Log.Debug("[OverwriteLatestManifestToCurrentManifest] latestExtraPatchInfo is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/PatchInfoController/ExtraPatchInfoController.cs", 68);
				return;
			}
			this._curExtraPatchInfo = this._curExtraPatchInfo.Append(this._latestExtraPatchInfo);
			this._curExtraPatchInfo.SaveAsJSON(PatchManifestPath.ExtraLocalDownloadPath, "PatchInfo.json");
		}

		// Token: 0x06005707 RID: 22279 RVA: 0x001A2D7D File Offset: 0x001A0F7D
		public void CleanUp()
		{
			this._curExtraPatchInfo = null;
			this._latestExtraPatchInfo = null;
			this._downloadHistoryExtraPatchInfo = null;
		}

		// Token: 0x040044F8 RID: 17656
		private NKCPatchInfo _curExtraPatchInfo;

		// Token: 0x040044F9 RID: 17657
		private NKCPatchInfo _latestExtraPatchInfo;

		// Token: 0x040044FA RID: 17658
		private NKCPatchInfo _downloadHistoryExtraPatchInfo;
	}
}
