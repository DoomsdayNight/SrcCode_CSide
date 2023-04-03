using System;
using System.Collections;
using System.Collections.Generic;
using Cs.Logging;
using NKC.UI;
using UnityEngine;

namespace NKC.Patcher
{
	// Token: 0x0200086B RID: 2155
	public class LegacyPatchDownloader : NKCPatchDownloader
	{
		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x060055A0 RID: 21920 RVA: 0x0019F224 File Offset: 0x0019D424
		public override long CurrentSize
		{
			get
			{
				long num = 0L;
				if (this.impl != null)
				{
					num += this.impl.CurrentBytesToDownload;
				}
				return num;
			}
		}

		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x060055A1 RID: 21921 RVA: 0x0019F251 File Offset: 0x0019D451
		public override long TotalSize
		{
			get
			{
				return this._totalBytesToDownload;
			}
		}

		// Token: 0x060055A2 RID: 21922 RVA: 0x0019F25C File Offset: 0x0019D45C
		public static NKCPatchDownloader InitInstance(NKCPatchDownloader.OnError onErrorDelegate)
		{
			Log.Debug("Init LegacyPatchDownloader", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/LegacyPatchDownloader.cs", 34);
			GameObject gameObject = new GameObject("PatchDownloader");
			if (string.IsNullOrEmpty(NKCConnectionInfo.DownloadServerAddress))
			{
				Debug.LogError("DownloadServerAddress not initialized!!");
			}
			NKCPatchDownloader nkcpatchDownloader = NKCPatchDownloader.Instance = gameObject.AddComponent<LegacyPatchDownloader>();
			nkcpatchDownloader.onError = onErrorDelegate;
			nkcpatchDownloader.impl = gameObject.AddComponent<LegacyPatchDownloaderImpl>();
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			return NKCPatchDownloader.Instance;
		}

		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x060055A3 RID: 21923 RVA: 0x0019F2C4 File Offset: 0x0019D4C4
		public override bool IsInit
		{
			get
			{
				return this.impl != null;
			}
		}

		// Token: 0x060055A4 RID: 21924 RVA: 0x0019F2D2 File Offset: 0x0019D4D2
		protected override void CheckVersionImpl(List<string> lstVariants)
		{
			base.BeginCoroutine(this.StartCoroutine(this.StartVersionCheck(lstVariants)), delegate(string s)
			{
				base.CheckVersionImplAfterProcess(NKCPatchDownloader.BuildStatus.Error, NKCPatchDownloader.VersionStatus.Error, s);
			});
		}

		// Token: 0x060055A5 RID: 21925 RVA: 0x0019F2F4 File Offset: 0x0019D4F4
		private IEnumerator StartVersionCheck(List<string> lstVariants)
		{
			base.ClearFileDownloadContainer();
			base.SetLocalBasePath();
			base.FullBuildCheck();
			yield return base.DownloadListCheck(lstVariants, false);
			base.DownloadList.ForEach(delegate(NKCPatchInfo.PatchFileInfo x)
			{
				this._totalBytesToDownload += x.Size;
			});
			if (NKCDefineManager.DEFINE_EXTRA_ASSET())
			{
				base.SetExtraLocalBasePath();
				base.CleanUpPcExtraPath();
				yield return base.DownloadListCheck(lstVariants, true);
				this._extraDownloadFiles.ForEach(delegate(NKCPatchInfo.PatchFileInfo x)
				{
					this._totalBytesToDownload += x.Size;
				});
			}
			base.OnEndVersionCheck();
			yield break;
		}

		// Token: 0x060055A6 RID: 21926 RVA: 0x0019F30A File Offset: 0x0019D50A
		private bool NeedToExtraDownload()
		{
			if (NKCDefineManager.DEFINE_EXTRA_ASSET())
			{
				List<NKCPatchInfo.PatchFileInfo> extraDownloadFiles = this._extraDownloadFiles;
				if (extraDownloadFiles != null && extraDownloadFiles.Count > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060055A7 RID: 21927 RVA: 0x0019F32D File Offset: 0x0019D52D
		public IEnumerator StartDownload()
		{
			this.impl.SetDownloadInfo(base.ServerBaseAddress, this.LocalDownloadPath, base.DownloadList, PatchManifestManager.BasePatchInfoController.GetDefaultDownloadHistoryPatchInfo());
			Log.Debug("[StartDownload] Start Download", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/LegacyPatchDownloader.cs", 106);
			yield return this.impl.ProcessFileDownload(new NKCPatchDownloader.OnDownloadProgress(this.EventProcessDownload), new NKCPatchDownloader.OnDownloadFinished(this.OnFinishedDownload));
			if (this.NeedToExtraDownload())
			{
				Log.Debug("[StartDownload] Start ExtraDownload", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/LegacyPatchDownloader.cs", 111);
				foreach (NKCPatchInfo.PatchFileInfo patchFileInfo in this._extraDownloadFiles)
				{
					Log.Debug("[StartDownload] Extra download target : " + patchFileInfo.FileName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/LegacyPatchDownloader.cs", 114);
				}
				this.impl.SetDownloadInfo(base.ExtraServerBaseAddress, this.ExtraLocalDownloadPath, this._extraDownloadFiles, PatchManifestManager.ExtraPatchInfoController.GetDownloadHistoryExtraPatchInfo());
				yield return this.impl.ProcessFileDownload(new NKCPatchDownloader.OnDownloadProgress(this.EventProcessDownload), new NKCPatchDownloader.OnDownloadFinished(this.OnFinishedExtraDownload));
			}
			base.DownloadFinished(NKCPatchDownloader.PatchDownloadStatus.Finished, "");
			this.StartBackgroundDownload();
			yield break;
		}

		// Token: 0x060055A8 RID: 21928 RVA: 0x0019F33C File Offset: 0x0019D53C
		private void OnFinishedDownload(NKCPatchDownloader.PatchDownloadStatus downloadStatus, string errorCode)
		{
			Log.Debug(string.Format("[OnFinishedDownload] : status {0}, errorCode {1}", downloadStatus, errorCode), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/LegacyPatchDownloader.cs", 128);
			if (this.NeedToExtraDownload())
			{
				base.DownloadStatus = NKCPatchDownloader.PatchDownloadStatus.Downloading;
			}
			else
			{
				base.DownloadStatus = downloadStatus;
			}
			base.ErrorString = errorCode;
			PatchManifestManager.BasePatchInfoController.AppendFilteredManifestToCurrentManifest();
			PatchManifestManager.RemoveManifestFile(PatchManifestPath.PatchType.TempManifest);
		}

		// Token: 0x060055A9 RID: 21929 RVA: 0x0019F398 File Offset: 0x0019D598
		private void OnFinishedExtraDownload(NKCPatchDownloader.PatchDownloadStatus downloadStatus, string errorCode)
		{
			Log.Debug(string.Format("[OnFinishedExtraDownload][status:{0}][errorCode:{1}]", downloadStatus, errorCode), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/LegacyPatchDownloader.cs", 148);
			base.DownloadStatus = downloadStatus;
			base.ErrorString = errorCode;
			PatchManifestManager.ExtraPatchInfoController.AppendFilteredManifestToCurrentManifest();
			PatchManifestManager.RemoveManifestFile(PatchManifestPath.PatchType.TempExtraManifest);
		}

		// Token: 0x060055AA RID: 21930 RVA: 0x0019F3D8 File Offset: 0x0019D5D8
		private void OnFinishedNonEssentialDownload(NKCPatchDownloader.PatchDownloadStatus downloadStatus, string errorCode)
		{
			Log.Debug(string.Format("[OnFinishedNonEssentialDownload][status:{0}][errorCode:{1}]", downloadStatus, errorCode), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/LegacyPatchDownloader.cs", 159);
			NKCUIOverlayPatchProgress.CheckInstanceAndClose();
			PatchManifestManager.BasePatchInfoController.AppendFilteredManifestToCurrentManifest();
			PatchManifestManager.RemoveManifestFile(PatchManifestPath.PatchType.TempManifest);
		}

		// Token: 0x060055AB RID: 21931 RVA: 0x0019F40F File Offset: 0x0019D60F
		public override void StartFileDownload()
		{
			base.DownloadStatus = NKCPatchDownloader.PatchDownloadStatus.Downloading;
			base.VersionCheckStatus = NKCPatchDownloader.VersionStatus.Downloading;
			this.StartCoroutine(this.StartDownload());
		}

		// Token: 0x060055AC RID: 21932 RVA: 0x0019F42C File Offset: 0x0019D62C
		public override void StartBackgroundDownload()
		{
			if (base.IsBackGroundDownload())
			{
				List<NKCPatchInfo.PatchFileInfo> finalBackGroundDownList = base.GetFinalBackGroundDownList();
				this._backGroundCoroutine = this.StartCoroutine(this.BackGroundDownload(finalBackGroundDownList));
			}
		}

		// Token: 0x060055AD RID: 21933 RVA: 0x0019F45B File Offset: 0x0019D65B
		public override void StopBackgroundDownload()
		{
			Coroutine<string> backGroundCoroutine = this._backGroundCoroutine;
			if (((backGroundCoroutine != null) ? backGroundCoroutine.coroutine : null) != null)
			{
				base.StopCoroutine(this._backGroundCoroutine.coroutine);
				this._backGroundCoroutine = null;
			}
		}

		// Token: 0x060055AE RID: 21934 RVA: 0x0019F489 File Offset: 0x0019D689
		public IEnumerator BackGroundDownload(IReadOnlyList<NKCPatchInfo.PatchFileInfo> patchFiles)
		{
			yield return null;
			this._totalBytesToDownload = 0L;
			foreach (NKCPatchInfo.PatchFileInfo patchFileInfo in patchFiles)
			{
				this._totalBytesToDownload += patchFileInfo.Size;
			}
			this.impl.SetDownloadInfo(base.ServerBaseAddress, this.LocalDownloadPath, patchFiles, PatchManifestManager.OptimizationPatchInfoController.GetBackgroundDownloadHistoryPatchInfo());
			yield return this.impl.ProcessFileDownload(new NKCPatchDownloader.OnDownloadProgress(this.EventProcessDownload), new NKCPatchDownloader.OnDownloadFinished(this.OnFinishedNonEssentialDownload));
			yield break;
		}

		// Token: 0x060055AF RID: 21935 RVA: 0x0019F49F File Offset: 0x0019D69F
		private void EventProcessDownload(long currentByte, long totalByte)
		{
			NKCPatchDownloader.OnDownloadProgress onDownloadProgress = this.onDownloadProgress;
			if (onDownloadProgress == null)
			{
				return;
			}
			onDownloadProgress(currentByte, totalByte);
		}

		// Token: 0x060055B0 RID: 21936 RVA: 0x0019F4B3 File Offset: 0x0019D6B3
		public override void Unload()
		{
			if (this.impl != null && this.impl.gameObject != null)
			{
				UnityEngine.Object.Destroy(this.impl.gameObject);
			}
		}

		// Token: 0x060055B1 RID: 21937 RVA: 0x0019F4E6 File Offset: 0x0019D6E6
		public override bool IsFileWillDownloaded(string filePath)
		{
			return this.impl.IsFileWillDownloaded(filePath);
		}

		// Token: 0x0400446C RID: 17516
		private LegacyPatchDownloaderImpl impl;

		// Token: 0x0400446D RID: 17517
		private Coroutine<string> _backGroundCoroutine;
	}
}
