using System;
using System.Collections;
using System.IO;
using NKC.Patcher;
using UnityEngine;
using UnityEngine.Networking;

namespace NKC.InfraTool
{
	// Token: 0x02000896 RID: 2198
	public class TempPatchDownLoader : NKCFileDownloader
	{
		// Token: 0x060057AB RID: 22443 RVA: 0x001A4E34 File Offset: 0x001A3034
		public TempPatchDownLoader(string serverBaseAddress, string localBaseAddress) : base(serverBaseAddress, localBaseAddress)
		{
		}

		// Token: 0x060057AC RID: 22444 RVA: 0x001A4E3E File Offset: 0x001A303E
		public override IEnumerator DownloadFile(string relativeDownloadPath, string relativeTargetPath, long targetFileSize)
		{
			string url = this.m_strServerBaseAddress + relativeDownloadPath;
			string path = this.m_strLocalBaseAddress + relativeTargetPath;
			using (UnityWebRequest webRequest = new UnityWebRequest(url))
			{
				string directoryName = Path.GetDirectoryName(path);
				if (!Directory.Exists(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				webRequest.downloadHandler = new DownloadHandlerFile(path);
				webRequest.SendWebRequest();
				while (!webRequest.isDone)
				{
					base.OnProgress((long)webRequest.downloadedBytes, targetFileSize);
					yield return null;
				}
				if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
				{
					Debug.LogError(webRequest.error);
				}
			}
			UnityWebRequest webRequest = null;
			yield break;
			yield break;
		}

		// Token: 0x060057AD RID: 22445 RVA: 0x001A4E62 File Offset: 0x001A3062
		public IEnumerator DownloadFile1(string relativeDownloadPath, string relativeTargetPath, long targetFileSize, Action<string> onError)
		{
			string downloadPath = this.m_strServerBaseAddress + relativeDownloadPath;
			string targetPath = this.m_strLocalBaseAddress + relativeTargetPath;
			using (UnityWebRequest webRequest = new UnityWebRequest(downloadPath))
			{
				string directoryName = Path.GetDirectoryName(targetPath);
				if (!Directory.Exists(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				webRequest.downloadHandler = new DownloadHandlerFile(targetPath);
				webRequest.SendWebRequest();
				while (!webRequest.isDone)
				{
					base.OnProgress((long)webRequest.downloadedBytes, targetFileSize);
					yield return null;
				}
				if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
				{
					Debug.LogError(webRequest.error);
					if (onError != null)
					{
						onError(downloadPath + " \n " + targetPath);
					}
				}
			}
			UnityWebRequest webRequest = null;
			yield break;
			yield break;
		}

		// Token: 0x060057AE RID: 22446 RVA: 0x001A4E8E File Offset: 0x001A308E
		protected override void Dispose(bool disposing)
		{
			if (!this.disposedValue)
			{
				this.disposedValue = true;
			}
		}
	}
}
