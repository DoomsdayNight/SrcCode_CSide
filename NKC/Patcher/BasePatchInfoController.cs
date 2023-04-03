using System;
using System.Collections.Generic;
using Cs.Logging;

namespace NKC.Patcher
{
	// Token: 0x02000888 RID: 2184
	public class BasePatchInfoController
	{
		// Token: 0x060056F3 RID: 22259 RVA: 0x001A2B0D File Offset: 0x001A0D0D
		private void LoadCurrentManifest()
		{
			this._curPatchInfo = PatchManifestManager.LoadManifest(PatchManifestPath.PatchType.CurrentManifest);
		}

		// Token: 0x060056F4 RID: 22260 RVA: 0x001A2B1B File Offset: 0x001A0D1B
		private void LoadLatestManifest()
		{
			this._latestPatchInfo = PatchManifestManager.LoadManifest(PatchManifestPath.PatchType.LatestManifest);
		}

		// Token: 0x060056F5 RID: 22261 RVA: 0x001A2B29 File Offset: 0x001A0D29
		public NKCPatchInfo LoadDownloadHistoryManifest()
		{
			this._downloadHistoryPatchInfo = PatchManifestManager.LoadManifest(PatchManifestPath.PatchType.TempManifest);
			return this._downloadHistoryPatchInfo;
		}

		// Token: 0x060056F6 RID: 22262 RVA: 0x001A2B3D File Offset: 0x001A0D3D
		public NKCPatchInfo LoadInnerManifest()
		{
			return PatchManifestManager.LoadManifest(PatchManifestPath.PatchType.InnerManifest);
		}

		// Token: 0x060056F7 RID: 22263 RVA: 0x001A2B45 File Offset: 0x001A0D45
		public NKCPatchInfo GetCurPatchInfo()
		{
			if (this._curPatchInfo == null)
			{
				this.LoadCurrentManifest();
			}
			return this._curPatchInfo;
		}

		// Token: 0x060056F8 RID: 22264 RVA: 0x001A2B5B File Offset: 0x001A0D5B
		public NKCPatchInfo GetLatestPatchInfo()
		{
			if (this._latestPatchInfo == null)
			{
				this.LoadLatestManifest();
			}
			return this._latestPatchInfo;
		}

		// Token: 0x060056F9 RID: 22265 RVA: 0x001A2B71 File Offset: 0x001A0D71
		public NKCPatchInfo GetDefaultDownloadHistoryPatchInfo()
		{
			if (this._downloadHistoryPatchInfo == null)
			{
				this.LoadDownloadHistoryManifest();
			}
			return this._downloadHistoryPatchInfo;
		}

		// Token: 0x060056FA RID: 22266 RVA: 0x001A2B88 File Offset: 0x001A0D88
		public NKCPatchInfo CreateFilteredManifestInfo(NKCPatchInfo patchInfo, List<string> lstVariants)
		{
			this._filteredPatchInfo = patchInfo.FilterByVariants(lstVariants);
			return this._filteredPatchInfo;
		}

		// Token: 0x060056FB RID: 22267 RVA: 0x001A2BA0 File Offset: 0x001A0DA0
		public void AppendFilteredManifestToCurrentManifest()
		{
			if (this._curPatchInfo == null)
			{
				Log.Error("[OverwriteLatestManifestToCurrentManifest] curPatchInfo is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/PatchInfoController/BasePatchInfoController.cs", 74);
				return;
			}
			if (this._filteredPatchInfo == null)
			{
				Log.Error("[OverwriteLatestManifestToCurrentManifest] filteredPatchInfo is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/PatchInfoController/BasePatchInfoController.cs", 80);
				return;
			}
			this._curPatchInfo = this._curPatchInfo.Append(this._filteredPatchInfo);
			this._curPatchInfo.SaveAsJSON(PatchManifestPath.LocalDownloadPath, "PatchInfo.json");
		}

		// Token: 0x060056FC RID: 22268 RVA: 0x001A2C10 File Offset: 0x001A0E10
		public bool NeedToBeUpdated(string assetBundleName)
		{
			NKCPatchInfo.PatchFileInfo latestFileInfo = this.GetLatestFileInfo(assetBundleName);
			if (latestFileInfo == null)
			{
				return false;
			}
			NKCPatchInfo.PatchFileInfo patchInfo = this._curPatchInfo.GetPatchInfo(assetBundleName);
			if (patchInfo == null)
			{
				return true;
			}
			if (!PatchManifestManager.IsFileExist(assetBundleName))
			{
				this._curPatchInfo.RemovePatchFileInfo(assetBundleName);
				return true;
			}
			return latestFileInfo.FileUpdated(patchInfo);
		}

		// Token: 0x060056FD RID: 22269 RVA: 0x001A2C59 File Offset: 0x001A0E59
		public NKCPatchInfo.PatchFileInfo GetLatestFileInfo(string assetBundleName)
		{
			return this._latestPatchInfo.GetPatchInfo(assetBundleName);
		}

		// Token: 0x060056FE RID: 22270 RVA: 0x001A2C67 File Offset: 0x001A0E67
		public void CleanUp()
		{
			this._curPatchInfo = null;
			this._latestPatchInfo = null;
			this._filteredPatchInfo = null;
			this._downloadHistoryPatchInfo = null;
		}

		// Token: 0x040044F4 RID: 17652
		private NKCPatchInfo _curPatchInfo;

		// Token: 0x040044F5 RID: 17653
		private NKCPatchInfo _latestPatchInfo;

		// Token: 0x040044F6 RID: 17654
		private NKCPatchInfo _filteredPatchInfo;

		// Token: 0x040044F7 RID: 17655
		private NKCPatchInfo _downloadHistoryPatchInfo;
	}
}
