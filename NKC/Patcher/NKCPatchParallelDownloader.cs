using System;
using System.Collections;
using System.Collections.Generic;
using Cs.Logging;
using NKC.UI;
using UnityEngine;

namespace NKC.Patcher
{
	// Token: 0x02000876 RID: 2166
	public class NKCPatchParallelDownloader : NKCPatchDownloader
	{
		// Token: 0x0600564D RID: 22093 RVA: 0x001A113C File Offset: 0x0019F33C
		public static NKCPatchDownloader InitInstance(NKCPatchDownloader.OnError onErrorDelegate)
		{
			Log.Debug("Init NKCPatchParallelDownloader", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchParallelDownloader.cs", 19);
			GameObject gameObject = new GameObject("PatchDownloader");
			if (string.IsNullOrEmpty(NKCConnectionInfo.DownloadServerAddress))
			{
				Debug.LogError("DownloadServerAddress not initialized!!");
			}
			(NKCPatchDownloader.Instance = gameObject.AddComponent<NKCPatchParallelDownloader>()).Init(onErrorDelegate);
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			return NKCPatchDownloader.Instance;
		}

		// Token: 0x0600564E RID: 22094 RVA: 0x001A1196 File Offset: 0x0019F396
		private void Init(NKCPatchDownloader.OnError onErrorDelegate)
		{
			this.onError = onErrorDelegate;
			base.SetLocalBasePath();
			this.m_bInit = true;
		}

		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x0600564F RID: 22095 RVA: 0x001A11AC File Offset: 0x0019F3AC
		public override bool IsInit
		{
			get
			{
				return this.m_bInit;
			}
		}

		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x06005650 RID: 22096 RVA: 0x001A11B4 File Offset: 0x0019F3B4
		public override long TotalSize
		{
			get
			{
				return this._totalBytesToDownload;
			}
		}

		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x06005651 RID: 22097 RVA: 0x001A11BC File Offset: 0x0019F3BC
		public override long CurrentSize
		{
			get
			{
				return this._currentBytesToDownload;
			}
		}

		// Token: 0x06005652 RID: 22098 RVA: 0x001A11C4 File Offset: 0x0019F3C4
		public override bool IsFileWillDownloaded(string filePath)
		{
			return base.DownloadList != null && base.DownloadList.Exists((NKCPatchInfo.PatchFileInfo x) => x.FileName == filePath);
		}

		// Token: 0x06005653 RID: 22099 RVA: 0x001A1200 File Offset: 0x0019F400
		public override void StartFileDownload()
		{
			this._downloadHistoryController.SetDownloadHistoryPatchInfo(PatchManifestManager.BasePatchInfoController.GetDefaultDownloadHistoryPatchInfo());
			this.SetDownloadState();
			this.SetOnEndAction(new Action<bool, string>(this.OnEndDownload));
			base.StartCoroutine(this.DownloadProcess(base.DownloadList));
		}

		// Token: 0x06005654 RID: 22100 RVA: 0x001A1250 File Offset: 0x0019F450
		public override void StartBackgroundDownload()
		{
			if (base.IsBackGroundDownload())
			{
				this._downloadHistoryController.SetDownloadHistoryPatchInfo(PatchManifestManager.OptimizationPatchInfoController.GetBackgroundDownloadHistoryPatchInfo());
				List<NKCPatchInfo.PatchFileInfo> finalBackGroundDownList = base.GetFinalBackGroundDownList();
				this._totalBytesToDownload = 0L;
				foreach (NKCPatchInfo.PatchFileInfo patchFileInfo in finalBackGroundDownList)
				{
					this._totalBytesToDownload += patchFileInfo.Size;
				}
				this.SetOnEndAction(new Action<bool, string>(this.OnEndNonEssentialDownload));
				this._backGroundCoroutine = base.StartCoroutine(this.DownloadProcess(finalBackGroundDownList));
			}
		}

		// Token: 0x06005655 RID: 22101 RVA: 0x001A1300 File Offset: 0x0019F500
		public override void StopBackgroundDownload()
		{
			if (this._backGroundCoroutine != null)
			{
				base.StopCoroutine(this._backGroundCoroutine);
				this._backGroundCoroutine = null;
			}
		}

		// Token: 0x06005656 RID: 22102 RVA: 0x001A131D File Offset: 0x0019F51D
		private void SetOnEndAction(Action<bool, string> onEnd)
		{
			this._onEndAction = onEnd;
		}

		// Token: 0x06005657 RID: 22103 RVA: 0x001A1328 File Offset: 0x0019F528
		public void OnEndDownload(bool errorOccurred, string errorStr)
		{
			if (errorOccurred)
			{
				Log.Error(string.Format("[OnEndDownload][errorOccurred:{0}][errorStr:{1}]", true, errorStr), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchParallelDownloader.cs", 110);
			}
			else
			{
				Log.Debug(string.Format("[OnEndDownload][errorOccurred:{0}][errorStr:{1}]", false, errorStr), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchParallelDownloader.cs", 114);
			}
			if (errorOccurred)
			{
				this.EventDownloadFinished(NKCPatchDownloader.PatchDownloadStatus.Error, errorStr);
			}
			else
			{
				this.EventDownloadFinished(NKCPatchDownloader.PatchDownloadStatus.Finished, "");
				PatchManifestManager.BasePatchInfoController.AppendFilteredManifestToCurrentManifest();
				PatchManifestManager.RemoveManifestFile(PatchManifestPath.PatchType.TempManifest);
			}
			this.StartBackgroundDownload();
		}

		// Token: 0x06005658 RID: 22104 RVA: 0x001A13A4 File Offset: 0x0019F5A4
		public void OnEndNonEssentialDownload(bool errorOccurred, string errorStr)
		{
			if (errorOccurred)
			{
				Log.Error(string.Format("[OnEndDownload][errorOccurred:{0}][errorStr:{1}]", true, errorStr), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchParallelDownloader.cs", 138);
			}
			else
			{
				Log.Debug(string.Format("[OnEndDownload][errorOccurred:{0}][errorStr:{1}]", false, errorStr), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchParallelDownloader.cs", 142);
			}
			NKCUIOverlayPatchProgress.CheckInstanceAndClose();
			PatchManifestManager.BasePatchInfoController.AppendFilteredManifestToCurrentManifest();
			PatchManifestManager.RemoveManifestFile(PatchManifestPath.PatchType.BackgroundDownloadHistoryManifest);
		}

		// Token: 0x06005659 RID: 22105 RVA: 0x001A140B File Offset: 0x0019F60B
		private void SetDownloadState()
		{
			base.DownloadStatus = NKCPatchDownloader.PatchDownloadStatus.Downloading;
			base.VersionCheckStatus = NKCPatchDownloader.VersionStatus.Downloading;
		}

		// Token: 0x0600565A RID: 22106 RVA: 0x001A141B File Offset: 0x0019F61B
		public IEnumerator DownloadProcess(IReadOnlyList<NKCPatchInfo.PatchFileInfo> downloadList)
		{
			this._currentBytesToDownload = 0L;
			if (downloadList == null)
			{
				this.EventDownloadFinished(NKCPatchDownloader.PatchDownloadStatus.Error, "No  file to download!");
				yield break;
			}
			if (string.IsNullOrEmpty(base.ServerBaseAddress))
			{
				this.EventDownloadFinished(NKCPatchDownloader.PatchDownloadStatus.Error, "PatchManifestPath.ServerBaseAddress Is Empty!");
				yield break;
			}
			this.DownloadQueue = new Queue<NKCPatchInfo.PatchFileInfo>(downloadList);
			this.lstWorker = new List<NKCPatchParallelDownloadWorker>(16);
			for (int i = 0; i < 16; i++)
			{
				this.lstWorker.Add(new NKCPatchParallelUnityWebDownloadWorker(base.ServerBaseAddress, this.LocalDownloadPath));
			}
			this._downloadHistoryController.CleanUp();
			string lastError = null;
			bool bErrorStop = false;
			bool bFinished;
			do
			{
				bFinished = true;
				long num = 0L;
				foreach (NKCPatchParallelDownloadWorker nkcpatchParallelDownloadWorker in this.lstWorker)
				{
					num += nkcpatchParallelDownloadWorker.Update();
					switch (nkcpatchParallelDownloadWorker.State)
					{
					case NKCPatchParallelDownloadWorker.eState.Idle:
					case NKCPatchParallelDownloadWorker.eState.Complete:
						if (nkcpatchParallelDownloadWorker.State == NKCPatchParallelDownloadWorker.eState.Complete)
						{
							if (nkcpatchParallelDownloadWorker.PatchFileInfo != null)
							{
								this._downloadHistoryController.AddTo(nkcpatchParallelDownloadWorker.PatchFileInfo);
							}
							nkcpatchParallelDownloadWorker.Cleanup();
						}
						if (this.DownloadQueue.Count != 0 && !bErrorStop)
						{
							NKCPatchInfo.PatchFileInfo fileInfo = this.DownloadQueue.Dequeue();
							nkcpatchParallelDownloadWorker.StartDownloadFile(fileInfo);
							bFinished = false;
						}
						break;
					case NKCPatchParallelDownloadWorker.eState.Busy:
					case NKCPatchParallelDownloadWorker.eState.Retry:
						bFinished = false;
						break;
					case NKCPatchParallelDownloadWorker.eState.Error:
						bErrorStop = true;
						lastError = nkcpatchParallelDownloadWorker.lastError;
						break;
					}
				}
				this._downloadHistoryController.UpdateDownloadHistoryPatchInfo();
				this._currentBytesToDownload = this._downloadHistoryController.CurrentDownloadedCompletedSize + num;
				NKCPatchDownloader.OnDownloadProgress onDownloadProgress = this.onDownloadProgress;
				if (onDownloadProgress != null)
				{
					onDownloadProgress(this._currentBytesToDownload, this._totalBytesToDownload);
				}
				yield return null;
			}
			while (!bFinished);
			Debug.Log("Download Finished. Disposing");
			this.DisposeDownloadWorker();
			Action<bool, string> onEndAction = this._onEndAction;
			if (onEndAction != null)
			{
				onEndAction(bErrorStop, lastError);
			}
			yield break;
		}

		// Token: 0x0600565B RID: 22107 RVA: 0x001A1434 File Offset: 0x0019F634
		private void DisposeDownloadWorker()
		{
			if (this.lstWorker == null)
			{
				return;
			}
			foreach (NKCPatchParallelDownloadWorker nkcpatchParallelDownloadWorker in this.lstWorker)
			{
				nkcpatchParallelDownloadWorker.Dispose();
			}
			this.lstWorker.Clear();
		}

		// Token: 0x0600565C RID: 22108 RVA: 0x001A1498 File Offset: 0x0019F698
		public void EventDownloadFinished(NKCPatchDownloader.PatchDownloadStatus downloadStatus, string errorString)
		{
			Log.Debug(string.Format("EventDownloadFinished : status {0}, ErrorCode {1}", downloadStatus, errorString), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchParallelDownloader.cs", 285);
			base.DownloadStatus = downloadStatus;
			base.ErrorString = errorString;
			base.DownloadFinished(downloadStatus, errorString);
		}

		// Token: 0x0600565D RID: 22109 RVA: 0x001A14D0 File Offset: 0x0019F6D0
		public override void Unload()
		{
			if (base.gameObject != null)
			{
				base.StopAllCoroutines();
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x0600565E RID: 22110 RVA: 0x001A14F4 File Offset: 0x0019F6F4
		protected override void CheckVersionImpl(List<string> lstVariants)
		{
			if (!this.m_bInit)
			{
				base.CheckVersionImplAfterProcess(NKCPatchDownloader.BuildStatus.Error, NKCPatchDownloader.VersionStatus.Error, "Patcher not initialized");
				return;
			}
			Debug.Log("downloader Version Check Start");
			base.BeginCoroutine(this.StartCoroutine(this.ProcessCheckVersion(lstVariants)), delegate(string s)
			{
				base.CheckVersionImplAfterProcess(NKCPatchDownloader.BuildStatus.Error, NKCPatchDownloader.VersionStatus.Error, s);
			});
		}

		// Token: 0x0600565F RID: 22111 RVA: 0x001A1541 File Offset: 0x0019F741
		private IEnumerator ProcessCheckVersion(List<string> lstVariants)
		{
			base.ClearFileDownloadContainer();
			base.FullBuildCheck();
			yield return base.DownloadListCheck(lstVariants, false);
			base.DownloadList.ForEach(delegate(NKCPatchInfo.PatchFileInfo x)
			{
				this._totalBytesToDownload += x.Size;
			});
			base.OnEndVersionCheck();
			Log.Debug("[ProcessCheckVersion] DownLoader end of version  check", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchParallelDownloader.cs", 332);
			yield break;
		}

		// Token: 0x06005660 RID: 22112 RVA: 0x001A1557 File Offset: 0x0019F757
		private void OnDestroy()
		{
			this.DisposeDownloadWorker();
		}

		// Token: 0x040044C0 RID: 17600
		private Queue<NKCPatchInfo.PatchFileInfo> DownloadQueue;

		// Token: 0x040044C1 RID: 17601
		private List<NKCPatchParallelDownloadWorker> lstWorker;

		// Token: 0x040044C2 RID: 17602
		private readonly DownloadHistoryController _downloadHistoryController = new DownloadHistoryController();

		// Token: 0x040044C3 RID: 17603
		private bool m_bInit;

		// Token: 0x040044C4 RID: 17604
		private Action<bool, string> _onEndAction;

		// Token: 0x040044C5 RID: 17605
		private const int PARALLEL_DOWNLOAD_COUNT = 16;

		// Token: 0x040044C6 RID: 17606
		private Coroutine _backGroundCoroutine;
	}
}
