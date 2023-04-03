using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Cs.Logging;
using UnityEngine;

namespace NKC.Patcher
{
	// Token: 0x0200086C RID: 2156
	public class LegacyPatchDownloaderImpl : MonoBehaviour
	{
		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x060055B6 RID: 21942 RVA: 0x0019F531 File Offset: 0x0019D731
		// (set) Token: 0x060055B7 RID: 21943 RVA: 0x0019F539 File Offset: 0x0019D739
		public long TotalBytesToDownload { get; private set; }

		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x060055B8 RID: 21944 RVA: 0x0019F542 File Offset: 0x0019D742
		// (set) Token: 0x060055B9 RID: 21945 RVA: 0x0019F54A File Offset: 0x0019D74A
		public long CurrentBytesToDownload { get; private set; }

		// Token: 0x060055BA RID: 21946 RVA: 0x0019F553 File Offset: 0x0019D753
		private void OnDestroy()
		{
			base.StopAllCoroutines();
		}

		// Token: 0x060055BB RID: 21947 RVA: 0x0019F55B File Offset: 0x0019D75B
		public void SetDownloadInfo(string serverPath, string localPath, IReadOnlyList<NKCPatchInfo.PatchFileInfo> downLoadList, NKCPatchInfo historyPatchInfo)
		{
			this.SetPath(serverPath, localPath);
			this.SetDownLoadList(downLoadList);
			this.SetDownloadHistoryPatchInfo(historyPatchInfo);
		}

		// Token: 0x060055BC RID: 21948 RVA: 0x0019F574 File Offset: 0x0019D774
		private void SetDownLoadList(IReadOnlyList<NKCPatchInfo.PatchFileInfo> downLoadList)
		{
			this._filesToDownload = null;
			this._filesToDownload = downLoadList;
		}

		// Token: 0x060055BD RID: 21949 RVA: 0x0019F584 File Offset: 0x0019D784
		private void SetDownloadHistoryPatchInfo(NKCPatchInfo historyPatchInfo)
		{
			this._downloadHistoryPatchInfo = historyPatchInfo;
		}

		// Token: 0x060055BE RID: 21950 RVA: 0x0019F58D File Offset: 0x0019D78D
		private void SetPath(string serverPath, string localPath)
		{
			this._sourceBaseAddress = serverPath;
			this._localDownloadBasePath = localPath;
		}

		// Token: 0x060055BF RID: 21951 RVA: 0x0019F59D File Offset: 0x0019D79D
		public IEnumerator ProcessFileDownload(NKCPatchDownloader.OnDownloadProgress onDownloadProgress, NKCPatchDownloader.OnDownloadFinished onDownloadFinished)
		{
			if (this._filesToDownload == null)
			{
				Log.Warn("[ProcessFileDownload] Download list is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/LegacyPatchDownloaderImpl.cs", 57);
				yield break;
			}
			this.m_CurrentDownloadedCompletedSize = 0L;
			this._onDownloadProgress = onDownloadProgress;
			using (NKCFileDownloader downloader = new UnityWebRequestDownloader(this._sourceBaseAddress, this._localDownloadBasePath))
			{
				NKCFileDownloader nkcfileDownloader = downloader;
				nkcfileDownloader.dOnDownloadProgressUpdated = (NKCFileDownloader.OnDownloadProgressUpdated)Delegate.Combine(nkcfileDownloader.dOnDownloadProgressUpdated, new NKCFileDownloader.OnDownloadProgressUpdated(this.OnFileDownloadProgress));
				foreach (NKCPatchInfo.PatchFileInfo targetFile in this._filesToDownload)
				{
					yield return this.DownloadFile(downloader, targetFile.FileName, targetFile.FileName, targetFile);
					if (!string.IsNullOrEmpty(this._errorString))
					{
						Log.Error("[ProcessFileDownload] Download fail _ [error:" + this._errorString + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/LegacyPatchDownloaderImpl.cs", 75);
						this._errorString = string.Empty;
					}
					else
					{
						this._downloadHistoryPatchInfo.AddPatchFileInfo(targetFile);
						this._downloadHistoryPatchInfo.SaveAsJSON();
						this.m_CurrentDownloadedCompletedSize += targetFile.Size;
						targetFile = null;
					}
				}
				IEnumerator<NKCPatchInfo.PatchFileInfo> enumerator = null;
			}
			NKCFileDownloader downloader = null;
			this.CurrentBytesToDownload = this.m_CurrentDownloadedCompletedSize;
			if (onDownloadFinished != null)
			{
				onDownloadFinished(NKCPatchDownloader.PatchDownloadStatus.Finished, "");
			}
			yield break;
			yield break;
		}

		// Token: 0x060055C0 RID: 21952 RVA: 0x0019F5BA File Offset: 0x0019D7BA
		private void OnFileDownloadProgress(long currentByte, long maxByte)
		{
			this.CurrentBytesToDownload = this.m_CurrentDownloadedCompletedSize + currentByte;
			NKCPatchDownloader.OnDownloadProgress onDownloadProgress = this._onDownloadProgress;
			if (onDownloadProgress == null)
			{
				return;
			}
			onDownloadProgress(this.CurrentBytesToDownload, this.TotalBytesToDownload);
		}

		// Token: 0x060055C1 RID: 21953 RVA: 0x0019F5E6 File Offset: 0x0019D7E6
		private IEnumerator DownloadFile(NKCFileDownloader downloader, string relativeDownloadPath, string relativeTargetPath, NKCPatchInfo.PatchFileInfo fileInfo)
		{
			Exception ex2;
			for (;;)
			{
				Coroutine<string> routine = this.StartCoroutine(downloader.DownloadFile(relativeDownloadPath, relativeTargetPath, (fileInfo != null) ? fileInfo.Size : 0L));
				yield return routine.coroutine;
				try
				{
					string value = routine.Value;
					if (fileInfo != null)
					{
						string localFullPath = downloader.GetLocalFullPath(relativeTargetPath);
						switch (fileInfo.CheckFileIntegrity(localFullPath))
						{
						case NKCPatchInfo.eFileIntergityStatus.OK:
							break;
						case NKCPatchInfo.eFileIntergityStatus.ERROR_SIZE:
							throw new Exception(string.Format("size check failed : {0}", relativeDownloadPath));
						case NKCPatchInfo.eFileIntergityStatus.ERROR_HASH:
							throw new Exception(string.Format("Integrity check failed : {0}", relativeDownloadPath));
						default:
							throw new NotImplementedException("Integrity Check behaivor undefined");
						}
					}
					yield break;
				}
				catch (WebException ex)
				{
					if (ex.Status == WebExceptionStatus.ProtocolError)
					{
						string message = string.Format("{0} : {1}", ((HttpWebResponse)ex.Response).StatusCode, ((HttpWebResponse)ex.Response).StatusDescription);
						Debug.LogError(message);
						ex2 = new Exception(message, ex);
					}
					else
					{
						ex2 = new Exception(ex.Message, ex);
					}
				}
				catch (Exception ex2)
				{
				}
				if (this.retryCount >= 10)
				{
					break;
				}
				Debug.LogError(ex2);
				this.retryCount++;
				yield return new WaitForSecondsRealtime(1f);
				routine = null;
			}
			this.retryCount = 0;
			Debug.LogError(ex2);
			this._errorString = string.Format(NKCUtilString.GET_STRING_ERROR_DOWNLOAD_ONE_PARAM, ex2.Message);
			yield break;
			yield break;
		}

		// Token: 0x060055C2 RID: 21954 RVA: 0x0019F614 File Offset: 0x0019D814
		public bool IsFileWillDownloaded(string filePath)
		{
			if (this._filesToDownload == null)
			{
				return false;
			}
			using (IEnumerator<NKCPatchInfo.PatchFileInfo> enumerator = this._filesToDownload.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.FileName == filePath)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0400446E RID: 17518
		private string _sourceBaseAddress;

		// Token: 0x0400446F RID: 17519
		private string _localDownloadBasePath;

		// Token: 0x04004470 RID: 17520
		private string _errorString;

		// Token: 0x04004471 RID: 17521
		private long m_CurrentDownloadedCompletedSize;

		// Token: 0x04004472 RID: 17522
		private IReadOnlyList<NKCPatchInfo.PatchFileInfo> _filesToDownload;

		// Token: 0x04004473 RID: 17523
		private NKCPatchInfo _downloadHistoryPatchInfo;

		// Token: 0x04004476 RID: 17526
		private NKCPatchDownloader.OnDownloadProgress _onDownloadProgress;

		// Token: 0x04004477 RID: 17527
		private int retryCount;

		// Token: 0x04004478 RID: 17528
		private const int MAX_RETRY_COUNT = 10;
	}
}
