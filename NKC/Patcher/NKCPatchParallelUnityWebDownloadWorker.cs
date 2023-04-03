using System;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace NKC.Patcher
{
	// Token: 0x02000875 RID: 2165
	public class NKCPatchParallelUnityWebDownloadWorker : NKCPatchParallelDownloadWorker
	{
		// Token: 0x06005647 RID: 22087 RVA: 0x001A0E4B File Offset: 0x0019F04B
		public NKCPatchParallelUnityWebDownloadWorker(string serverBaseAddress, string localBaseAddress) : base(serverBaseAddress, localBaseAddress)
		{
		}

		// Token: 0x06005648 RID: 22088 RVA: 0x001A0E58 File Offset: 0x0019F058
		public override void StartDownloadFile(string relativeSourcePath, string relativeTargetPath)
		{
			if (base.State == NKCPatchParallelDownloadWorker.eState.Busy)
			{
				Debug.LogError("Logic Error : Already working!");
				return;
			}
			this.m_strRelativeSourcePath = relativeSourcePath;
			this.m_strRelativeTargetPath = relativeTargetPath;
			base.State = NKCPatchParallelDownloadWorker.eState.Busy;
			string url = this.m_strServerBaseAddress + relativeSourcePath;
			string path = this.m_strLocalBaseAddress + relativeTargetPath;
			if (this.webRequest != null)
			{
				this.webRequest.Dispose();
				this.webRequest = null;
			}
			this.webRequest = new UnityWebRequest(url);
			this.m_DownloadedByte = 0UL;
			this.m_Timeout = 0f;
			string directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			this.webRequest.downloadHandler = new DownloadHandlerFile(path);
			this.webRequest.SendWebRequest();
		}

		// Token: 0x06005649 RID: 22089 RVA: 0x001A0F14 File Offset: 0x0019F114
		public override long Update()
		{
			if (base.State == NKCPatchParallelDownloadWorker.eState.Retry)
			{
				this.m_retryWaitTime += Time.unscaledDeltaTime;
				if (this.m_retryWaitTime > 1f)
				{
					this.m_retryWaitTime = 0f;
					this.RetryDownloadFile();
					return 0L;
				}
			}
			if (this.webRequest == null)
			{
				return 0L;
			}
			if (!this.webRequest.isDone)
			{
				if (this.m_DownloadedByte == this.webRequest.downloadedBytes)
				{
					this.m_Timeout += Time.unscaledDeltaTime;
					if (this.m_Timeout > 30f)
					{
						this.webRequest.Abort();
						this.webRequest.Dispose();
						this.webRequest = null;
						base.WaitAndRetry("Request timeout");
						return 0L;
					}
				}
				else
				{
					this.m_DownloadedByte = this.webRequest.downloadedBytes;
					this.m_Timeout = 0f;
				}
				return (long)this.webRequest.downloadedBytes;
			}
			if (this.webRequest.result == UnityWebRequest.Result.ConnectionError || this.webRequest.result == UnityWebRequest.Result.ProtocolError)
			{
				base.WaitAndRetry(this.webRequest.error);
			}
			else
			{
				this.OnDownloadFinished();
			}
			this.webRequest.Dispose();
			this.webRequest = null;
			return 0L;
		}

		// Token: 0x0600564A RID: 22090 RVA: 0x001A104C File Offset: 0x0019F24C
		private void OnDownloadFinished()
		{
			if (base.PatchFileInfo == null)
			{
				this.m_retryCount = 0;
				base.State = NKCPatchParallelDownloadWorker.eState.Complete;
				return;
			}
			string fullPath = this.m_strLocalBaseAddress + this.m_strRelativeTargetPath;
			switch (base.PatchFileInfo.CheckFileIntegrity(fullPath))
			{
			case NKCPatchInfo.eFileIntergityStatus.OK:
				base.State = NKCPatchParallelDownloadWorker.eState.Complete;
				this.m_retryCount = 0;
				return;
			case NKCPatchInfo.eFileIntergityStatus.ERROR_SIZE:
				base.WaitAndRetry(string.Format("size check failed : {0}", this.m_strRelativeTargetPath));
				return;
			case NKCPatchInfo.eFileIntergityStatus.ERROR_HASH:
				base.WaitAndRetry(string.Format("Integrity check failed : {0}", this.m_strRelativeTargetPath));
				return;
			default:
				base.WaitAndRetry("Integrity Check error : behaivor undefined");
				return;
			}
		}

		// Token: 0x0600564B RID: 22091 RVA: 0x001A10EB File Offset: 0x0019F2EB
		public override void Cleanup()
		{
			if (this.webRequest != null)
			{
				this.webRequest.Dispose();
				this.webRequest = null;
			}
			base.State = NKCPatchParallelDownloadWorker.eState.Idle;
		}

		// Token: 0x0600564C RID: 22092 RVA: 0x001A110E File Offset: 0x0019F30E
		protected override void Dispose(bool disposing)
		{
			if (!this.disposedValue)
			{
				if (disposing && this.webRequest != null)
				{
					this.webRequest.Dispose();
					this.webRequest = null;
				}
				this.disposedValue = true;
			}
		}

		// Token: 0x040044BC RID: 17596
		private UnityWebRequest webRequest;

		// Token: 0x040044BD RID: 17597
		private ulong m_DownloadedByte;

		// Token: 0x040044BE RID: 17598
		private float m_Timeout;

		// Token: 0x040044BF RID: 17599
		private const float TIMEOUT_SECOND = 30f;
	}
}
