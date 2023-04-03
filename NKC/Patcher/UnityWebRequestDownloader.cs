using System;
using System.Collections;
using System.IO;
using Cs.Logging;
using UnityEngine.Networking;

namespace NKC.Patcher
{
	// Token: 0x0200086E RID: 2158
	public class UnityWebRequestDownloader : NKCFileDownloader
	{
		// Token: 0x060055CC RID: 21964 RVA: 0x0019F72E File Offset: 0x0019D92E
		public UnityWebRequestDownloader(string serverBaseAddress, string localBaseAddress) : base(serverBaseAddress, localBaseAddress)
		{
		}

		// Token: 0x060055CD RID: 21965 RVA: 0x0019F738 File Offset: 0x0019D938
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
				bool bSucceed = true;
				if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
				{
					bSucceed = false;
					Log.Error("[UnityWebRequestDownloader][DownloadFile] DownloadFail : " + webRequest.error, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCFileDownloader.cs", 118);
				}
				base.OnComplete(bSucceed);
			}
			UnityWebRequest webRequest = null;
			yield break;
			yield break;
		}

		// Token: 0x060055CE RID: 21966 RVA: 0x0019F75C File Offset: 0x0019D95C
		protected override void Dispose(bool disposing)
		{
			if (!this.disposedValue)
			{
				this.disposedValue = true;
			}
		}

		// Token: 0x0400447E RID: 17534
		private const string _className = "UnityWebRequestDownloader";
	}
}
