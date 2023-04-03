using System;
using System.IO;
using UnityEngine;

namespace NKC.Patcher
{
	// Token: 0x02000874 RID: 2164
	public abstract class NKCPatchParallelDownloadWorker : IDisposable
	{
		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x06005638 RID: 22072 RVA: 0x001A0D37 File Offset: 0x0019EF37
		// (set) Token: 0x06005639 RID: 22073 RVA: 0x001A0D3F File Offset: 0x0019EF3F
		public string lastError { get; protected set; }

		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x0600563A RID: 22074 RVA: 0x001A0D48 File Offset: 0x0019EF48
		// (set) Token: 0x0600563B RID: 22075 RVA: 0x001A0D50 File Offset: 0x0019EF50
		public NKCPatchInfo.PatchFileInfo PatchFileInfo { get; protected set; }

		// Token: 0x0600563C RID: 22076 RVA: 0x001A0D59 File Offset: 0x0019EF59
		public NKCPatchParallelDownloadWorker(string serverBaseAddress, string localBaseAddress)
		{
			this.m_strServerBaseAddress = serverBaseAddress;
			this.m_strLocalBaseAddress = localBaseAddress;
		}

		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x0600563D RID: 22077 RVA: 0x001A0D6F File Offset: 0x0019EF6F
		// (set) Token: 0x0600563E RID: 22078 RVA: 0x001A0D77 File Offset: 0x0019EF77
		public NKCPatchParallelDownloadWorker.eState State { get; protected set; }

		// Token: 0x0600563F RID: 22079 RVA: 0x001A0D80 File Offset: 0x0019EF80
		public void StartDownloadFile(NKCPatchInfo.PatchFileInfo fileInfo)
		{
			this.PatchFileInfo = fileInfo;
			this.StartDownloadFile(fileInfo.FileName, fileInfo.FileName);
		}

		// Token: 0x06005640 RID: 22080
		public abstract void StartDownloadFile(string relativeSourcePath, string relativeTargetPath);

		// Token: 0x06005641 RID: 22081
		public abstract long Update();

		// Token: 0x06005642 RID: 22082 RVA: 0x001A0D9B File Offset: 0x0019EF9B
		public virtual void RetryDownloadFile()
		{
			this.StartDownloadFile(this.m_strRelativeSourcePath, this.m_strRelativeTargetPath);
		}

		// Token: 0x06005643 RID: 22083
		public abstract void Cleanup();

		// Token: 0x06005644 RID: 22084 RVA: 0x001A0DB0 File Offset: 0x0019EFB0
		protected void WaitAndRetry(string error)
		{
			this.lastError = error;
			Debug.LogError(this.m_strRelativeTargetPath + " : " + error);
			FileInfo fileInfo = new FileInfo(this.m_strLocalBaseAddress + this.m_strRelativeTargetPath);
			if (fileInfo.Exists)
			{
				fileInfo.Attributes = FileAttributes.Normal;
				fileInfo.Delete();
			}
			if (this.m_retryCount > 10)
			{
				this.State = NKCPatchParallelDownloadWorker.eState.Error;
				return;
			}
			this.State = NKCPatchParallelDownloadWorker.eState.Retry;
			this.m_retryWaitTime = 0f;
			this.m_retryCount++;
		}

		// Token: 0x06005645 RID: 22085
		protected abstract void Dispose(bool disposing);

		// Token: 0x06005646 RID: 22086 RVA: 0x001A0E3C File Offset: 0x0019F03C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x040044B1 RID: 17585
		protected string m_strRelativeSourcePath;

		// Token: 0x040044B2 RID: 17586
		protected string m_strRelativeTargetPath;

		// Token: 0x040044B3 RID: 17587
		protected string m_strServerBaseAddress;

		// Token: 0x040044B4 RID: 17588
		protected string m_strLocalBaseAddress;

		// Token: 0x040044B8 RID: 17592
		protected const float RETRY_WAIT_SECOND = 1f;

		// Token: 0x040044B9 RID: 17593
		protected float m_retryWaitTime;

		// Token: 0x040044BA RID: 17594
		protected int m_retryCount;

		// Token: 0x040044BB RID: 17595
		protected bool disposedValue;

		// Token: 0x0200153F RID: 5439
		public enum eState
		{
			// Token: 0x0400A054 RID: 41044
			Idle,
			// Token: 0x0400A055 RID: 41045
			Busy,
			// Token: 0x0400A056 RID: 41046
			Retry,
			// Token: 0x0400A057 RID: 41047
			Error,
			// Token: 0x0400A058 RID: 41048
			Complete
		}
	}
}
