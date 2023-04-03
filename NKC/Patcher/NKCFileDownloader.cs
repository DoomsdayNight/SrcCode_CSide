using System;
using System.Collections;

namespace NKC.Patcher
{
	// Token: 0x0200086D RID: 2157
	public abstract class NKCFileDownloader : IDisposable
	{
		// Token: 0x060055C4 RID: 21956 RVA: 0x0019F680 File Offset: 0x0019D880
		public NKCFileDownloader(string ServerBaseAddress, string localDownloadBaseAddress)
		{
			this.Initialize(ServerBaseAddress, localDownloadBaseAddress);
		}

		// Token: 0x060055C5 RID: 21957 RVA: 0x0019F690 File Offset: 0x0019D890
		public void Initialize(string svrBaseAddress, string localBaseAddr)
		{
			if (svrBaseAddress.EndsWith("/"))
			{
				this.m_strServerBaseAddress = svrBaseAddress;
			}
			else
			{
				this.m_strServerBaseAddress = svrBaseAddress + "/";
			}
			if (localBaseAddr.EndsWith("/"))
			{
				this.m_strLocalBaseAddress = localBaseAddr;
				return;
			}
			this.m_strLocalBaseAddress = localBaseAddr + "/";
		}

		// Token: 0x060055C6 RID: 21958 RVA: 0x0019F6EA File Offset: 0x0019D8EA
		protected void OnProgress(long currentBytes, long totalBytes)
		{
			if (this.dOnDownloadProgressUpdated != null)
			{
				this.dOnDownloadProgressUpdated(currentBytes, totalBytes);
			}
		}

		// Token: 0x060055C7 RID: 21959 RVA: 0x0019F701 File Offset: 0x0019D901
		protected void OnComplete(bool bSucceed)
		{
			if (this.dOnDownloadCompleted != null)
			{
				this.dOnDownloadCompleted(bSucceed);
			}
		}

		// Token: 0x060055C8 RID: 21960 RVA: 0x0019F717 File Offset: 0x0019D917
		public string GetLocalFullPath(string relativeTargetPath)
		{
			return this.m_strLocalBaseAddress + relativeTargetPath;
		}

		// Token: 0x060055C9 RID: 21961
		public abstract IEnumerator DownloadFile(string relativeDownloadPath, string relativeTargetPath, long targetFileSize);

		// Token: 0x060055CA RID: 21962
		protected abstract void Dispose(bool disposing);

		// Token: 0x060055CB RID: 21963 RVA: 0x0019F725 File Offset: 0x0019D925
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x04004479 RID: 17529
		public NKCFileDownloader.OnDownloadProgressUpdated dOnDownloadProgressUpdated;

		// Token: 0x0400447A RID: 17530
		public NKCFileDownloader.OnDownloadCompleted dOnDownloadCompleted;

		// Token: 0x0400447B RID: 17531
		protected string m_strServerBaseAddress;

		// Token: 0x0400447C RID: 17532
		protected string m_strLocalBaseAddress;

		// Token: 0x0400447D RID: 17533
		protected bool disposedValue;

		// Token: 0x02001523 RID: 5411
		// (Invoke) Token: 0x0600ABCF RID: 43983
		public delegate void OnDownloadProgressUpdated(long currentBytes, long totalBytes);

		// Token: 0x02001524 RID: 5412
		// (Invoke) Token: 0x0600ABD3 RID: 43987
		public delegate void OnDownloadCompleted(bool bSucceed);
	}
}
