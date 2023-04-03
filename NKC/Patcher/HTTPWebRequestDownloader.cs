using System;
using System.Collections;
using System.IO;
using System.Net;

namespace NKC.Patcher
{
	// Token: 0x0200086F RID: 2159
	public class HTTPWebRequestDownloader : NKCFileDownloader
	{
		// Token: 0x060055CF RID: 21967 RVA: 0x0019F76D File Offset: 0x0019D96D
		public HTTPWebRequestDownloader(string serverBaseAddress, string localBaseAddress, string versionCode) : base(serverBaseAddress, localBaseAddress)
		{
			this.ResumeCode = versionCode;
			this.downBuffer = new byte[1048576];
		}

		// Token: 0x060055D0 RID: 21968 RVA: 0x0019F78E File Offset: 0x0019D98E
		public override IEnumerator DownloadFile(string relativeDownloadPath, string relativeTargetPath, long targetFileSize)
		{
			string requestUriString = this.m_strServerBaseAddress + relativeDownloadPath;
			string targetPath = this.m_strLocalBaseAddress + relativeTargetPath;
			string targetTempPath = targetPath + "." + this.ResumeCode;
			string directoryName = Path.GetDirectoryName(targetPath);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			bool flag = !string.IsNullOrEmpty(this.ResumeCode);
			FileInfo fileInfo = new FileInfo(targetTempPath);
			if (fileInfo.Exists && fileInfo.Length >= targetFileSize)
			{
				File.Delete(targetTempPath);
			}
			using (FileStream fileStream = new FileStream(targetTempPath, flag ? FileMode.Append : FileMode.Create))
			{
				int currentFileLength = (int)fileStream.Length;
				HttpWebRequest httpWebRequest = WebRequest.Create(requestUriString) as HttpWebRequest;
				if (flag && 0 < currentFileLength)
				{
					httpWebRequest.AddRange(currentFileLength);
				}
				httpWebRequest.Timeout = 10000;
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				Stream smRespStream = httpWebResponse.GetResponseStream();
				long contentLength = httpWebResponse.ContentLength;
				long maxSize = httpWebResponse.ContentLength;
				int num;
				while ((num = smRespStream.Read(this.downBuffer, 0, this.downBuffer.Length)) > 0)
				{
					fileStream.Write(this.downBuffer, 0, num);
					currentFileLength += num;
					if (maxSize > 0L)
					{
						base.OnProgress((long)currentFileLength, maxSize);
					}
					if (fileStream.Length == targetFileSize)
					{
						break;
					}
					yield return null;
				}
				smRespStream = null;
			}
			FileStream fileStream = null;
			File.Copy(targetTempPath, targetPath, true);
			File.Delete(targetTempPath);
			base.OnComplete(true);
			yield break;
			yield break;
		}

		// Token: 0x060055D1 RID: 21969 RVA: 0x0019F7B2 File Offset: 0x0019D9B2
		protected override void Dispose(bool disposing)
		{
			if (!this.disposedValue)
			{
				this.disposedValue = true;
			}
		}

		// Token: 0x0400447F RID: 17535
		private string ResumeCode;

		// Token: 0x04004480 RID: 17536
		private byte[] downBuffer;

		// Token: 0x04004481 RID: 17537
		private const int iBufferSize = 1048576;
	}
}
