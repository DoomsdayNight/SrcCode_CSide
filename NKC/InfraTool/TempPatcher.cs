using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using AssetBundles;
using Cs.Logging;
using NKC.Patcher;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

namespace NKC.InfraTool
{
	// Token: 0x02000897 RID: 2199
	public class TempPatcher
	{
		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x060057B0 RID: 22448 RVA: 0x001A4EA8 File Offset: 0x001A30A8
		// (set) Token: 0x060057AF RID: 22447 RVA: 0x001A4E9F File Offset: 0x001A309F
		public string BaseFileServerAddress { get; set; } = "http://FileServer.bside.com/PatchFiles/CSConfigDev.txt";

		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x060057B2 RID: 22450 RVA: 0x001A4EB9 File Offset: 0x001A30B9
		// (set) Token: 0x060057B1 RID: 22449 RVA: 0x001A4EB0 File Offset: 0x001A30B0
		public string AssetBundleVersion { get; private set; }

		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x060057B3 RID: 22451 RVA: 0x001A4EC4 File Offset: 0x001A30C4
		public string DownLoadAddress
		{
			get
			{
				if (string.IsNullOrEmpty(this.AssetBundleVersion))
				{
					return string.Empty;
				}
				if (this.UseExtraDownload)
				{
					return string.Concat(new string[]
					{
						this.PatchFileAddress,
						this.m_sExtraPath,
						"/",
						this.AssetBundleVersion,
						"/"
					});
				}
				return string.Concat(new string[]
				{
					this.PatchFileAddress,
					Utility.GetPlatformName(),
					"/",
					this.AssetBundleVersion,
					"/"
				});
			}
		}

		// Token: 0x060057B4 RID: 22452 RVA: 0x001A4F58 File Offset: 0x001A3158
		private void SetLog(string str, TempPatcher.LogType type = TempPatcher.LogType.Debug)
		{
			string text = "[Patcher] " + str;
			switch (type)
			{
			case TempPatcher.LogType.Debug:
				Debug.Log(text);
				return;
			case TempPatcher.LogType.Warn:
				Log.Warn("[Warn] : " + text, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/PatchTool/TempPatcher.cs", 95);
				return;
			case TempPatcher.LogType.Error:
				Log.Error("[Error] : " + text, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/PatchTool/TempPatcher.cs", 98);
				return;
			default:
				throw new ArgumentOutOfRangeException("type", type, null);
			}
		}

		// Token: 0x060057B5 RID: 22453 RVA: 0x001A4FCD File Offset: 0x001A31CD
		public TempPatcher(Action<TempPatcher.VersionJsonErrorType, TempPatcher> versionJsonDownError, Action<TempPatcher.DownloadErrorType, TempPatcher> downloadError)
		{
			this._versionJsonError = versionJsonDownError;
			this._downloadErrorType = downloadError;
		}

		// Token: 0x060057B6 RID: 22454 RVA: 0x001A5004 File Offset: 0x001A3204
		public void Init()
		{
			this.FilesToDownload = null;
			this.ConfigAddress = this.BaseFileServerAddress;
		}

		// Token: 0x060057B7 RID: 22455 RVA: 0x001A5019 File Offset: 0x001A3219
		public IEnumerator GetAddressOrLoginServer()
		{
			string text = UnityEngine.Random.Range(1000000, 8000000).ToString();
			text += UnityEngine.Random.Range(1000000, 8000000).ToString();
			string str = "?p=" + text;
			string uri = this.ConfigAddress + str;
			using (UnityWebRequest www = UnityWebRequest.Get(uri))
			{
				yield return www.SendWebRequest();
				if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
				{
					this.SetLog(string.Format("connect fail => {0}", www.result), TempPatcher.LogType.Error);
					Action<TempPatcher.VersionJsonErrorType, TempPatcher> versionJsonError = this._versionJsonError;
					if (versionJsonError != null)
					{
						versionJsonError(TempPatcher.VersionJsonErrorType.ConfigFilePathError, this);
					}
				}
				else
				{
					JSONNode jsonnode = JSONNode.Parse(www.downloadHandler.text);
					JSONNode jsonnode2 = jsonnode["PatchServerAddress1"];
					if (jsonnode2 == null)
					{
						JSONNode d = jsonnode["PatchServerAddress2"];
						this.PatchFileAddress = d;
					}
					else
					{
						this.PatchFileAddress = jsonnode2;
					}
					if (this.UseExtraDownload)
					{
						this.VersionAddress = this.PatchFileAddress + this.m_sExtraPath + this.DefaultVersionJson;
					}
					else
					{
						this.VersionAddress = this.PatchFileAddress + Utility.GetPlatformName() + this.DefaultVersionJson;
					}
					this.ServiceIP = jsonnode["CSLoginServerIP"];
					this.ServicePort = jsonnode["CSLoginServerPort"].AsInt;
					if (this.ServiceIP == null)
					{
						this.ServiceIP = jsonnode["ip"];
						this.ServicePort = jsonnode["port"];
						JSONNode jsonnode3 = jsonnode["type"];
						this._versionJson = jsonnode["versionJson"];
						this.PatchFileAddress = jsonnode["cdn"];
						this.VersionAddress = this.PatchFileAddress + Utility.GetPlatformName() + this._versionJson;
					}
				}
			}
			UnityWebRequest www = null;
			yield break;
			yield break;
		}

		// Token: 0x060057B8 RID: 22456 RVA: 0x001A5028 File Offset: 0x001A3228
		public IEnumerator GetVersionJson()
		{
			string text = UnityEngine.Random.Range(1000000, 8000000).ToString();
			text += UnityEngine.Random.Range(1000000, 8000000).ToString();
			string str = "?p=" + text;
			string uri = this.VersionAddress + str;
			this._versionJson = null;
			using (UnityWebRequest www = UnityWebRequest.Get(uri))
			{
				yield return www.SendWebRequest();
				if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
				{
					this.SetLog(string.Format("connect fail => {0}", www.result), TempPatcher.LogType.Error);
					Action<TempPatcher.VersionJsonErrorType, TempPatcher> versionJsonError = this._versionJsonError;
					if (versionJsonError != null)
					{
						versionJsonError(TempPatcher.VersionJsonErrorType.DownServerAddressFail, this);
					}
					yield break;
				}
				this._versionJson = JSONNode.Parse(www.downloadHandler.text);
			}
			UnityWebRequest www = null;
			if (this._versionJson == null)
			{
				Action<TempPatcher.VersionJsonErrorType, TempPatcher> versionJsonError2 = this._versionJsonError;
				if (versionJsonError2 != null)
				{
					versionJsonError2(TempPatcher.VersionJsonErrorType.VersionJsonParseFail, this);
				}
				this.SetLog("version json null", TempPatcher.LogType.Error);
				yield break;
			}
			JSONNode jsonnode = this._versionJson["versionList"];
			JSONArray jsonarray = (jsonnode != null) ? jsonnode.AsArray : null;
			if (jsonarray == null)
			{
				Action<TempPatcher.VersionJsonErrorType, TempPatcher> versionJsonError3 = this._versionJsonError;
				if (versionJsonError3 != null)
				{
					versionJsonError3(TempPatcher.VersionJsonErrorType.VersionListNodeIsNull, this);
				}
				this.SetLog("versionList is null", TempPatcher.LogType.Error);
				yield break;
			}
			this.AssetBundleVersion = jsonarray[0]["version"];
			this.SetLog("version   => " + this.AssetBundleVersion, TempPatcher.LogType.Debug);
			yield break;
			yield break;
		}

		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x060057B9 RID: 22457 RVA: 0x001A5037 File Offset: 0x001A3237
		private string LocalDownloadBasePath
		{
			get
			{
				return AssetBundleManager.GetLocalDownloadPath() + "/";
			}
		}

		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x060057BA RID: 22458 RVA: 0x001A5048 File Offset: 0x001A3248
		private string CurrentManifestPath
		{
			get
			{
				return this.LocalDownloadBasePath + "PatchInfo.json";
			}
		}

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x060057BB RID: 22459 RVA: 0x001A505A File Offset: 0x001A325A
		private string InnerManifestPath
		{
			get
			{
				return NKCPatchUtility.GetInnerAssetPath("PatchInfo.json", false);
			}
		}

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x060057BC RID: 22460 RVA: 0x001A5067 File Offset: 0x001A3267
		private string TempManifestPath
		{
			get
			{
				return this.LocalDownloadBasePath + "TempPatchInfo.json";
			}
		}

		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x060057BD RID: 22461 RVA: 0x001A5079 File Offset: 0x001A3279
		private string NewManifestPath
		{
			get
			{
				return this.LocalDownloadBasePath + "LatestPatchInfo.json";
			}
		}

		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x060057BF RID: 22463 RVA: 0x001A5094 File Offset: 0x001A3294
		// (set) Token: 0x060057BE RID: 22462 RVA: 0x001A508B File Offset: 0x001A328B
		public NKCPatchInfo FilteredPatchInfo { get; private set; }

		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x060057C1 RID: 22465 RVA: 0x001A50A5 File Offset: 0x001A32A5
		// (set) Token: 0x060057C0 RID: 22464 RVA: 0x001A509C File Offset: 0x001A329C
		public NKCPatchInfo CurrentPatchInfo { get; private set; }

		// Token: 0x060057C2 RID: 22466 RVA: 0x001A50AD File Offset: 0x001A32AD
		public IEnumerator GetPatchInfo()
		{
			string localDownloadPath = AssetBundleManager.GetLocalDownloadPath();
			if (string.IsNullOrEmpty(this.DownLoadAddress))
			{
				this.SetLog("Invalid DownLoadAddress", TempPatcher.LogType.Error);
				yield break;
			}
			using (TempPatchDownLoader downLoader = new TempPatchDownLoader(this.DownLoadAddress, localDownloadPath))
			{
				this.SetLog("[Start Download]", TempPatcher.LogType.Debug);
				yield return this.DownloadFile(downLoader, "PatchInfo.json", "LatestPatchInfo.json", null);
				NKCPatchInfo nkcpatchInfo = NKCPatchInfo.LoadFromJSON(this.NewManifestPath);
				if (nkcpatchInfo == null)
				{
					Action<TempPatcher.VersionJsonErrorType, TempPatcher> versionJsonError = this._versionJsonError;
					if (versionJsonError != null)
					{
						versionJsonError(TempPatcher.VersionJsonErrorType.PatchInfoFail, this);
					}
					yield break;
				}
				string[] activeVariants = AssetBundleManager.ActiveVariants;
				foreach (string str in activeVariants)
				{
					this.SetLog("[variants] : " + str, TempPatcher.LogType.Debug);
				}
				this.FilteredPatchInfo = null;
				if (this.UseExtraDownload)
				{
					this.FilteredPatchInfo = nkcpatchInfo;
				}
				else
				{
					this.FilteredPatchInfo = nkcpatchInfo.FilterByVariants(new List<string>(activeVariants));
				}
				this.SetLog("[VersionString] : " + this.FilteredPatchInfo.VersionString, TempPatcher.LogType.Debug);
				this.DownloadedFiles = NKCPatchInfo.LoadFromJSON(this.TempManifestPath);
				this.CurrentPatchInfo = null;
				if (NKCPatchUtility.IsFileExists(this.CurrentManifestPath))
				{
					this.CurrentPatchInfo = NKCPatchInfo.LoadFromJSON(this.CurrentManifestPath);
				}
				else
				{
					this.CurrentPatchInfo = NKCPatchInfo.LoadFromJSON(this.InnerManifestPath);
				}
				bool bCheckPreExistOldPatchInfo = this.CurrentPatchInfo.m_dicPatchInfo.Count > 0;
				this.CurrentPatchInfo = this.CurrentPatchInfo.Append(this.DownloadedFiles);
				this.SetLog("[Get DownLoadList]", TempPatcher.LogType.Debug);
				this.FilesToDownload = new List<NKCPatchInfo.PatchFileInfo>();
				yield return PatchManifestManager.GetDownloadList(this.FilesToDownload, bCheckPreExistOldPatchInfo, this.CurrentPatchInfo, this.FilteredPatchInfo, this.LocalDownloadBasePath, false, null);
				if (!this.UseExtraDownload)
				{
					this.FilesToDownload = new List<NKCPatchInfo.PatchFileInfo>(this.FilesToDownload.FindAll((NKCPatchInfo.PatchFileInfo x) => x.FileName == "ab_script" || x.FileName.Contains("ab_font") || x.FileName == "ab_script_string_table"));
				}
				foreach (NKCPatchInfo.PatchFileInfo patchFileInfo in this.FilesToDownload)
				{
					this.SetLog(string.Format("[DownLoad Target Name] : {0}, Size : {1} MB", patchFileInfo.FileName, (float)patchFileInfo.Size / 1048576f), TempPatcher.LogType.Debug);
				}
			}
			TempPatchDownLoader downLoader = null;
			yield break;
			yield break;
		}

		// Token: 0x060057C3 RID: 22467 RVA: 0x001A50BC File Offset: 0x001A32BC
		private IEnumerator DownloadFile(TempPatchDownLoader downLoader, string relativeDownloadPath, string relativeTargetPath, NKCPatchInfo.PatchFileInfo fileInfo)
		{
			TempPatcher.<>c__DisplayClass51_0 CS$<>8__locals1 = new TempPatcher.<>c__DisplayClass51_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.fileDownError = false;
			Exception ex2;
			for (;;)
			{
				yield return downLoader.DownloadFile1(relativeDownloadPath, relativeTargetPath, (fileInfo != null) ? fileInfo.Size : 0L, new Action<string>(CS$<>8__locals1.<DownloadFile>g__DownloadError|0));
				if (CS$<>8__locals1.fileDownError)
				{
					break;
				}
				try
				{
					if (fileInfo != null)
					{
						string localFullPath = downLoader.GetLocalFullPath(relativeTargetPath);
						switch (fileInfo.CheckFileIntegrity(localFullPath))
						{
						case NKCPatchInfo.eFileIntergityStatus.OK:
							break;
						case NKCPatchInfo.eFileIntergityStatus.ERROR_SIZE:
							throw new Exception("size check failed : " + relativeDownloadPath);
						case NKCPatchInfo.eFileIntergityStatus.ERROR_HASH:
							throw new Exception("Integrity check failed : " + relativeDownloadPath);
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
						string text = string.Format("{0} : {1}", ((HttpWebResponse)ex.Response).StatusCode, ((HttpWebResponse)ex.Response).StatusDescription);
						this.SetLog(text ?? "", TempPatcher.LogType.Error);
						ex2 = new Exception(text, ex);
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
					goto IL_200;
				}
				this.SetLog("download retry", TempPatcher.LogType.Error);
				this.retryCount++;
				yield return new WaitForSecondsRealtime(1f);
			}
			this.SetLog("download error occurred : " + relativeTargetPath, TempPatcher.LogType.Error);
			yield break;
			IL_200:
			this.retryCount = 0;
			this.SetLog(string.Format("{0}", ex2), TempPatcher.LogType.Error);
			Debug.LogError(ex2);
			yield break;
		}

		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x060057C4 RID: 22468 RVA: 0x001A50E8 File Offset: 0x001A32E8
		// (set) Token: 0x060057C5 RID: 22469 RVA: 0x001A50F0 File Offset: 0x001A32F0
		public long CurrentBytesToDownload { get; protected set; }

		// Token: 0x060057C6 RID: 22470 RVA: 0x001A50F9 File Offset: 0x001A32F9
		public IEnumerator DownloadProcess()
		{
			this.CurrentBytesToDownload = 0L;
			if (this.FilesToDownload == null)
			{
				Action<TempPatcher.DownloadErrorType, TempPatcher> downloadErrorType = this._downloadErrorType;
				if (downloadErrorType != null)
				{
					downloadErrorType(TempPatcher.DownloadErrorType.FilesToDownloadIsNull, this);
				}
				yield break;
			}
			if (this.FilesToDownload.Count == 0)
			{
				Action<TempPatcher.DownloadErrorType, TempPatcher> downloadErrorType2 = this._downloadErrorType;
				if (downloadErrorType2 != null)
				{
					downloadErrorType2(TempPatcher.DownloadErrorType.NotExistDownloadFile, this);
				}
				yield break;
			}
			this._downloadQueue = new Queue<NKCPatchInfo.PatchFileInfo>(this.FilesToDownload);
			this.lstWorker = new List<NKCPatchParallelDownloadWorker>(16);
			for (int i = 0; i < 16; i++)
			{
				this.lstWorker.Add(new NKCPatchParallelUnityWebDownloadWorker(this.DownLoadAddress, this.LocalDownloadBasePath));
			}
			bool bErrorStop = false;
			bool bFinished;
			do
			{
				bFinished = true;
				long num = 0L;
				List<NKCPatchInfo.PatchFileInfo> list = new List<NKCPatchInfo.PatchFileInfo>();
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
								list.Add(nkcpatchParallelDownloadWorker.PatchFileInfo);
							}
							nkcpatchParallelDownloadWorker.Cleanup();
						}
						if (this._downloadQueue.Count != 0 && !bErrorStop)
						{
							NKCPatchInfo.PatchFileInfo fileInfo = this._downloadQueue.Dequeue();
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
						this.SetLog(nkcpatchParallelDownloadWorker.lastError, TempPatcher.LogType.Error);
						break;
					}
				}
				if (list.Count > 0)
				{
					for (int j = 0; j < list.Count; j++)
					{
						NKCPatchInfo.PatchFileInfo patchFileInfo = list[j];
						this.DownloadedFiles.m_dicPatchInfo[patchFileInfo.FileName] = patchFileInfo;
					}
					this.DownloadedFiles.SaveAsJSON(this.LocalDownloadBasePath, "TempPatchInfo.json");
				}
				list.Clear();
				yield return null;
			}
			while (!bFinished);
			Debug.Log("Download Finished. Disposing");
			for (int k = 0; k < this.lstWorker.Count; k++)
			{
				this.lstWorker[k].Dispose();
			}
			this.lstWorker.Clear();
			if (bErrorStop)
			{
				Action<TempPatcher.DownloadErrorType, TempPatcher> downloadErrorType3 = this._downloadErrorType;
				if (downloadErrorType3 != null)
				{
					downloadErrorType3(TempPatcher.DownloadErrorType.ErrorStop, this);
				}
			}
			else
			{
				Action<TempPatcher.DownloadErrorType, TempPatcher> downloadErrorType4 = this._downloadErrorType;
				if (downloadErrorType4 != null)
				{
					downloadErrorType4(TempPatcher.DownloadErrorType.Ok, this);
				}
				this.SetLog("DownLoad Ok!", TempPatcher.LogType.Debug);
			}
			yield break;
		}

		// Token: 0x0400454D RID: 17741
		public string DefaultVersionJson = "/version.json";

		// Token: 0x0400454E RID: 17742
		public string PatchFileAddress;

		// Token: 0x0400454F RID: 17743
		public string VersionAddress;

		// Token: 0x04004550 RID: 17744
		public string ConfigAddress;

		// Token: 0x04004551 RID: 17745
		public string ServiceIP;

		// Token: 0x04004552 RID: 17746
		public int ServicePort;

		// Token: 0x04004553 RID: 17747
		public List<NKCPatchInfo.PatchFileInfo> FilesToDownload;

		// Token: 0x04004554 RID: 17748
		public bool UseExtraDownload;

		// Token: 0x04004555 RID: 17749
		private string m_sExtraPath = "ExtraAsset";

		// Token: 0x04004556 RID: 17750
		private JSONNode _versionJson;

		// Token: 0x04004557 RID: 17751
		private readonly Action<TempPatcher.VersionJsonErrorType, TempPatcher> _versionJsonError;

		// Token: 0x04004558 RID: 17752
		private readonly Action<TempPatcher.DownloadErrorType, TempPatcher> _downloadErrorType;

		// Token: 0x0400455C RID: 17756
		private int retryCount;

		// Token: 0x0400455D RID: 17757
		private const int MAX_RETRY_COUNT = 10;

		// Token: 0x0400455F RID: 17759
		private Queue<NKCPatchInfo.PatchFileInfo> _downloadQueue;

		// Token: 0x04004560 RID: 17760
		private List<NKCPatchParallelDownloadWorker> lstWorker;

		// Token: 0x04004561 RID: 17761
		private const int PARALLEL_DOWNLOAD_COUNT = 16;

		// Token: 0x04004562 RID: 17762
		private NKCPatchInfo DownloadedFiles;

		// Token: 0x0200156E RID: 5486
		public enum VersionJsonErrorType
		{
			// Token: 0x0400A10B RID: 41227
			Ok,
			// Token: 0x0400A10C RID: 41228
			ConfigFilePathError,
			// Token: 0x0400A10D RID: 41229
			DownServerAddressFail,
			// Token: 0x0400A10E RID: 41230
			VersionListNodeIsNull,
			// Token: 0x0400A10F RID: 41231
			VersionJsonParseFail,
			// Token: 0x0400A110 RID: 41232
			PatchInfoFail
		}

		// Token: 0x0200156F RID: 5487
		public enum DownloadErrorType
		{
			// Token: 0x0400A112 RID: 41234
			Ok,
			// Token: 0x0400A113 RID: 41235
			FilesToDownloadIsNull,
			// Token: 0x0400A114 RID: 41236
			NotExistDownloadFile,
			// Token: 0x0400A115 RID: 41237
			ErrorStop,
			// Token: 0x0400A116 RID: 41238
			DownLoadError
		}

		// Token: 0x02001570 RID: 5488
		public enum LogType
		{
			// Token: 0x0400A118 RID: 41240
			Debug,
			// Token: 0x0400A119 RID: 41241
			Warn,
			// Token: 0x0400A11A RID: 41242
			Error
		}
	}
}
