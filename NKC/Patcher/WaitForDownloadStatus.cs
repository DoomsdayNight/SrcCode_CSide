using System;
using System.Collections;
using NKC.Publisher;
using UnityEngine;

namespace NKC.Patcher
{
	// Token: 0x0200087F RID: 2175
	public class WaitForDownloadStatus : IPatchProcessStrategy, IEnumerable
	{
		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x06005687 RID: 22151 RVA: 0x001A1705 File Offset: 0x0019F905
		// (set) Token: 0x06005686 RID: 22150 RVA: 0x001A16FC File Offset: 0x0019F8FC
		public IPatchProcessStrategy.ExecutionStatus Status { get; private set; }

		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x06005689 RID: 22153 RVA: 0x001A1716 File Offset: 0x0019F916
		// (set) Token: 0x06005688 RID: 22152 RVA: 0x001A170D File Offset: 0x0019F90D
		public string ReasonOfFailure { get; private set; } = string.Empty;

		// Token: 0x0600568A RID: 22154 RVA: 0x001A171E File Offset: 0x0019F91E
		public IEnumerator GetEnumerator()
		{
			this.Status = IPatchProcessStrategy.ExecutionStatus.Success;
			this.ReasonOfFailure = string.Empty;
			yield return null;
			if (this.IsDownloading())
			{
				NKCPatcherUI patcherUI = NKCPatchChecker.PatcherUI;
				if (patcherUI != null)
				{
					patcherUI.SetProgressText(NKCUtilString.GET_STRING_PATCHER_DOWNLOADING);
				}
				NKCPatcherUI patcherUI2 = NKCPatchChecker.PatcherUI;
				if (patcherUI2 != null)
				{
					patcherUI2.SetActiveBackGround(NKCPatchDownloader.Instance.BackgroundDownloadAvailble);
				}
				NKCPatcherUI patcherUI3 = NKCPatchChecker.PatcherUI;
				if (patcherUI3 != null)
				{
					patcherUI3.Set_lbCanDownloadBackground(NKCUtilString.GET_STRING_PATCHER_CAN_BACKGROUND_DOWNLOAD);
				}
				while (NKCPatchDownloader.Instance.DownloadStatus == NKCPatchDownloader.PatchDownloadStatus.Downloading)
				{
					NKCPatcherUI patcherUI4 = NKCPatchChecker.PatcherUI;
					if (patcherUI4 != null)
					{
						patcherUI4.OnFileDownloadProgressTotal(NKCPatchDownloader.Instance.CurrentSize, NKCPatchDownloader.Instance.TotalSize);
					}
					yield return null;
				}
				Debug.Log("PatchLoop finished, patcherStatus " + NKCPatchDownloader.Instance.DownloadStatus.ToString());
				switch (NKCPatchDownloader.Instance.DownloadStatus)
				{
				case NKCPatchDownloader.PatchDownloadStatus.Idle:
					NKCPatcherManager.GetPatcherManager().ShowError(NKCStringTable.GetString("SI_DP_PATCHER_DOWNLOAD_ERROR", new object[]
					{
						""
					}));
					break;
				case NKCPatchDownloader.PatchDownloadStatus.UserCancel:
					NKCPatcherManager.GetPatcherManager().ShowError("User Canceled");
					break;
				case NKCPatchDownloader.PatchDownloadStatus.Finished:
				{
					Debug.Log("Download finished");
					NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.Patch_DownloadComplete, 0, null);
					NKCPatcherUI patcherUI5 = NKCPatchChecker.PatcherUI;
					if (patcherUI5 != null)
					{
						patcherUI5.SetProgressText(NKCUtilString.GET_STRING_PATCHER_DOWNLOADING);
					}
					break;
				}
				case NKCPatchDownloader.PatchDownloadStatus.Error:
					NKCPatcherManager.GetPatcherManager().ShowError(NKCStringTable.GetString("SI_DP_PATCHER_DOWNLOAD_ERROR", new object[]
					{
						NKCPatchDownloader.Instance.ErrorString
					}));
					break;
				case NKCPatchDownloader.PatchDownloadStatus.UpdateRequired:
					NKCPatcherManager.GetPatcherManager().ShowUpdate();
					break;
				}
			}
			NKCPatcherUI patcherUI6 = NKCPatchChecker.PatcherUI;
			if (patcherUI6 != null)
			{
				patcherUI6.Progress();
			}
			NKCAdjustManager.OnCustomEvent("03_downLoad_complete");
			NKCPatcherUI patcherUI7 = NKCPatchChecker.PatcherUI;
			if (patcherUI7 != null)
			{
				patcherUI7.Progress();
			}
			NKCPatcherUI patcherUI8 = NKCPatchChecker.PatcherUI;
			if (patcherUI8 != null)
			{
				patcherUI8.OnFileDownloadProgressTotal(1L, 1L);
			}
			yield break;
		}

		// Token: 0x0600568B RID: 22155 RVA: 0x001A172D File Offset: 0x0019F92D
		private bool IsDownloading()
		{
			return !NKCPatchDownloader.Instance.ProloguePlay && NKCPatchDownloader.Instance.DownloadStatus == NKCPatchDownloader.PatchDownloadStatus.Downloading;
		}
	}
}
