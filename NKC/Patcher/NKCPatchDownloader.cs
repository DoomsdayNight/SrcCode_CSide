using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using AssetBundles;
using Cs.Logging;
using NKC.Publisher;
using NKM;
using SimpleJSON;
using UnityEngine;

namespace NKC.Patcher
{
	// Token: 0x02000872 RID: 2162
	public abstract class NKCPatchDownloader : MonoBehaviour
	{
		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x060055DC RID: 21980
		public abstract bool IsInit { get; }

		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x060055DD RID: 21981 RVA: 0x0019FA03 File Offset: 0x0019DC03
		// (set) Token: 0x060055DE RID: 21982 RVA: 0x0019FA0B File Offset: 0x0019DC0B
		public NKCPatchDownloader.BuildStatus BuildCheckStatus { get; set; }

		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x060055DF RID: 21983 RVA: 0x0019FA14 File Offset: 0x0019DC14
		// (set) Token: 0x060055E0 RID: 21984 RVA: 0x0019FA1C File Offset: 0x0019DC1C
		public NKCPatchDownloader.VersionStatus VersionCheckStatus { get; set; }

		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x060055E1 RID: 21985 RVA: 0x0019FA25 File Offset: 0x0019DC25
		// (set) Token: 0x060055E2 RID: 21986 RVA: 0x0019FA2D File Offset: 0x0019DC2D
		public NKCPatchDownloader.PatchDownloadStatus DownloadStatus { get; protected set; }

		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x060055E3 RID: 21987 RVA: 0x0019FA36 File Offset: 0x0019DC36
		// (set) Token: 0x060055E4 RID: 21988 RVA: 0x0019FA3E File Offset: 0x0019DC3E
		public bool ConnectionInfoUpdated { get; protected set; }

		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x060055E5 RID: 21989 RVA: 0x0019FA47 File Offset: 0x0019DC47
		// (set) Token: 0x060055E6 RID: 21990 RVA: 0x0019FA4F File Offset: 0x0019DC4F
		public string ErrorString { get; protected set; }

		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x060055E7 RID: 21991
		public abstract long TotalSize { get; }

		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x060055E8 RID: 21992
		public abstract long CurrentSize { get; }

		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x060055E9 RID: 21993 RVA: 0x0019FA58 File Offset: 0x0019DC58
		public float DownloadPercent
		{
			get
			{
				if (this.CurrentSize == this.TotalSize)
				{
					return 1f;
				}
				if (this.TotalSize == 0L)
				{
					return 1f;
				}
				return (float)this.CurrentSize / (float)this.TotalSize;
			}
		}

		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x060055EA RID: 21994 RVA: 0x0019FA8B File Offset: 0x0019DC8B
		public virtual bool BackgroundDownloadAvailble
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060055EB RID: 21995 RVA: 0x0019FA8E File Offset: 0x0019DC8E
		public void InitCheckTime()
		{
			Log.Debug("[PatcherManager] Init skip CheckTime", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchDownloader.cs", 149);
			this.m_dtNextVersionCheckTime = DateTime.UtcNow;
		}

		// Token: 0x060055EC RID: 21996 RVA: 0x0019FAB0 File Offset: 0x0019DCB0
		public void CheckVersion(List<string> lstVariants, bool bIntegrityCheck = false)
		{
			Log.Debug(string.Format("[CheckVersion] BuildCheckStatus:{0} _ DownloadStatus:{1}", this.BuildCheckStatus, this.VersionCheckStatus), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchDownloader.cs", 163);
			if (this.BuildCheckStatus != NKCPatchDownloader.BuildStatus.Unchecked && this.VersionCheckStatus != NKCPatchDownloader.VersionStatus.Unchecked)
			{
				if (this.VersionCheckStatus == NKCPatchDownloader.VersionStatus.RequireDownload && this.DownloadStatus == NKCPatchDownloader.PatchDownloadStatus.Downloading)
				{
					this.VersionCheckStatus = NKCPatchDownloader.VersionStatus.Downloading;
				}
				if (this.VersionCheckStatus == NKCPatchDownloader.VersionStatus.Downloading)
				{
					Debug.LogWarning("Skip VersionCheck : already downloading");
					return;
				}
				if (DateTime.UtcNow < this.m_dtNextVersionCheckTime)
				{
					Debug.LogWarning("Skip VersionCheck");
					return;
				}
			}
			Debug.Log("Version Check 1");
			this.m_dtNextVersionCheckTime = DateTime.UtcNow.AddMinutes(5.0);
			this.m_bIntegrityCheck = bIntegrityCheck;
			this.BuildCheckStatus = NKCPatchDownloader.BuildStatus.Unchecked;
			this.VersionCheckStatus = NKCPatchDownloader.VersionStatus.Unchecked;
			Debug.Log("Version Check Imple");
			this.CheckVersionImpl(lstVariants);
		}

		// Token: 0x060055ED RID: 21997
		protected abstract void CheckVersionImpl(List<string> lstVariants);

		// Token: 0x060055EE RID: 21998 RVA: 0x0019FB92 File Offset: 0x0019DD92
		protected void CheckVersionImplAfterProcess(NKCPatchDownloader.BuildStatus buildStatus, NKCPatchDownloader.VersionStatus versionStatus, string ErrorCode)
		{
			this.BuildCheckStatus = buildStatus;
			this.VersionCheckStatus = versionStatus;
			this.ErrorString = ErrorCode;
			NKCPatchDownloader.OnVersionCheckResult onVersionCheckResult = this.onVersionCheckResult;
			if (onVersionCheckResult == null)
			{
				return;
			}
			onVersionCheckResult(buildStatus, versionStatus, ErrorCode);
		}

		// Token: 0x060055EF RID: 21999 RVA: 0x0019FBBC File Offset: 0x0019DDBC
		protected virtual void UpdateDataVersion()
		{
			NKCConnectionInfo.UpdateDataVersionOnly();
			Log.Debug(string.Format("DataVersion Updated : {0}", NKMDataVersion.DataVersion), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchDownloader.cs", 231);
		}

		// Token: 0x060055F0 RID: 22000
		public abstract void StartFileDownload();

		// Token: 0x060055F1 RID: 22001
		public abstract void StartBackgroundDownload();

		// Token: 0x060055F2 RID: 22002
		public abstract void StopBackgroundDownload();

		// Token: 0x060055F3 RID: 22003 RVA: 0x0019FBE6 File Offset: 0x0019DDE6
		public virtual void DoWhenEndDownload()
		{
		}

		// Token: 0x060055F4 RID: 22004 RVA: 0x0019FBE8 File Offset: 0x0019DDE8
		protected List<NKCPatchInfo.PatchFileInfo> GetFinalBackGroundDownList()
		{
			List<NKCPatchInfo.PatchFileInfo> list = new List<NKCPatchInfo.PatchFileInfo>();
			foreach (NKCPatchInfo.PatchFileInfo patchFileInfo in this._tutorialBackGroundDownloadFiles)
			{
				if (PatchManifestManager.BasePatchInfoController.NeedToBeUpdated(patchFileInfo.FileName))
				{
					NKCPatchInfo.PatchFileInfo item = new NKCPatchInfo.PatchFileInfo(patchFileInfo.FileName, patchFileInfo.Hash, patchFileInfo.Size);
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x060055F5 RID: 22005 RVA: 0x0019FC6C File Offset: 0x0019DE6C
		protected void DownloadFinished(NKCPatchDownloader.PatchDownloadStatus status, string errorStr)
		{
			this.DownloadStatus = status;
			if (status == NKCPatchDownloader.PatchDownloadStatus.Finished)
			{
				this.DoWhenEndDownload();
				this.VersionCheckStatus = NKCPatchDownloader.VersionStatus.UpToDate;
				if (NKCPublisherModule.ServerInfo.IsUsePatchConnectionInfo())
				{
					this.UpdateDataVersion();
				}
			}
			NKCPatchDownloader.OnDownloadFinished onDownloadFinished = this.onDownloadFinished;
			if (onDownloadFinished == null)
			{
				return;
			}
			onDownloadFinished(status, errorStr);
		}

		// Token: 0x060055F6 RID: 22006 RVA: 0x0019FCAC File Offset: 0x0019DEAC
		public virtual string GetFileFullPath(string filePath)
		{
			if (false || (this.VersionCheckStatus == NKCPatchDownloader.VersionStatus.UpToDate && !this.ProloguePlay))
			{
				string text = this.LocalDownloadPath;
				if (!text.EndsWith("/"))
				{
					text += "/";
				}
				string text2 = text + filePath;
				if (NKCPatchUtility.IsFileExists(text2))
				{
					return text2;
				}
			}
			string innerAssetPath = NKCPatchUtility.GetInnerAssetPath(filePath, false);
			if (NKCPatchUtility.IsFileExists(innerAssetPath))
			{
				return innerAssetPath;
			}
			return "";
		}

		// Token: 0x060055F7 RID: 22007 RVA: 0x0019FD18 File Offset: 0x0019DF18
		public virtual string GetLocalDownloadedPath(string filePath)
		{
			string text = this.LocalDownloadPath;
			if (!text.EndsWith("/"))
			{
				text += "/";
			}
			string text2 = text + filePath;
			if (NKCPatchUtility.IsFileExists(text2))
			{
				return text2;
			}
			return "";
		}

		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x060055F9 RID: 22009 RVA: 0x0019FD65 File Offset: 0x0019DF65
		// (set) Token: 0x060055F8 RID: 22008 RVA: 0x0019FD5C File Offset: 0x0019DF5C
		public string ServerBaseAddress { get; private set; }

		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x060055FB RID: 22011 RVA: 0x0019FD76 File Offset: 0x0019DF76
		// (set) Token: 0x060055FA RID: 22010 RVA: 0x0019FD6D File Offset: 0x0019DF6D
		public virtual string LocalDownloadPath { get; private set; }

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x060055FD RID: 22013 RVA: 0x0019FD87 File Offset: 0x0019DF87
		// (set) Token: 0x060055FC RID: 22012 RVA: 0x0019FD7E File Offset: 0x0019DF7E
		public string ExtraServerBaseAddress { get; private set; }

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x060055FF RID: 22015 RVA: 0x0019FD98 File Offset: 0x0019DF98
		// (set) Token: 0x060055FE RID: 22014 RVA: 0x0019FD8F File Offset: 0x0019DF8F
		public virtual string ExtraLocalDownloadPath { get; private set; }

		// Token: 0x06005600 RID: 22016
		public abstract void Unload();

		// Token: 0x06005601 RID: 22017
		public abstract bool IsFileWillDownloaded(string filePath);

		// Token: 0x06005602 RID: 22018 RVA: 0x0019FDA0 File Offset: 0x0019DFA0
		protected void NotifyError(string msg)
		{
			this.ErrorString = msg;
			NKCPatchDownloader.OnError onError = this.onError;
			if (onError == null)
			{
				return;
			}
			onError(this.ErrorString);
		}

		// Token: 0x06005603 RID: 22019 RVA: 0x0019FDC0 File Offset: 0x0019DFC0
		public virtual bool HasNoDownloadedFiles()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(this.LocalDownloadPath);
			return !directoryInfo.Exists || directoryInfo.GetFiles().Length <= 1;
		}

		// Token: 0x06005604 RID: 22020 RVA: 0x0019FDF1 File Offset: 0x0019DFF1
		public void SetUpdatedForSkipPatch()
		{
			this.VersionCheckStatus = NKCPatchDownloader.VersionStatus.UpToDate;
			this.BuildCheckStatus = NKCPatchDownloader.BuildStatus.UpToDate;
		}

		// Token: 0x06005605 RID: 22021 RVA: 0x0019FE04 File Offset: 0x0019E004
		public void SetBaseDownloadPath(string downloadServerAddress, string versionString, bool useExtra)
		{
			if (useExtra)
			{
				this.ExtraServerBaseAddress = downloadServerAddress + "ExtraAsset/" + versionString + "/";
				NKCPatchDownloader.DebugLog(string.Format("[Extra:{0}] ExtraServerBaseAddress : {1}", useExtra, this.ExtraServerBaseAddress), "SetBaseDownloadPath");
				return;
			}
			this.ServerBaseAddress = string.Concat(new string[]
			{
				downloadServerAddress,
				Utility.GetPlatformName(),
				"/",
				versionString,
				"/"
			});
			NKCPatchDownloader.DebugLog(string.Format("[Extra:{0}] ServerBaseAddress : {1}", useExtra, this.ServerBaseAddress), "SetBaseDownloadPath");
		}

		// Token: 0x06005606 RID: 22022 RVA: 0x0019FE9D File Offset: 0x0019E09D
		public void SetLocalBasePath()
		{
			this.LocalDownloadPath = AssetBundleManager.GetLocalDownloadPath();
			if (!this.LocalDownloadPath.EndsWith("/"))
			{
				this.LocalDownloadPath += "/";
			}
		}

		// Token: 0x06005607 RID: 22023 RVA: 0x0019FED2 File Offset: 0x0019E0D2
		public void SetExtraLocalBasePath()
		{
			this.ExtraLocalDownloadPath = NKCUtil.GetExtraDownloadPath();
			if (!this.ExtraLocalDownloadPath.EndsWith("/"))
			{
				this.ExtraLocalDownloadPath += "/";
			}
		}

		// Token: 0x06005608 RID: 22024 RVA: 0x0019FF08 File Offset: 0x0019E108
		protected void CleanUpPcExtraPath()
		{
			if (string.IsNullOrEmpty(this.ExtraLocalDownloadPath))
			{
				NKCPatchDownloader.DebugLog("ExtraLocalDownloadPath is null or empty", "CleanUpPcExtraPath");
				return;
			}
			if (NKCDefineManager.DEFINE_PC_EXTRA_DOWNLOAD_IN_EXE_FOLDER())
			{
				string text = Application.persistentDataPath + "/Assetbundles/";
				if (!text.EndsWith("/"))
				{
					text = this.ExtraLocalDownloadPath + "/";
				}
				if (Directory.Exists(text))
				{
					Directory.Delete(text, true);
				}
				text = Application.persistentDataPath + "/Replay/";
				if (!text.EndsWith("/"))
				{
					text = this.ExtraLocalDownloadPath + "/";
				}
				if (Directory.Exists(text))
				{
					NKCPatchDownloader.DebugLog("Delete PC ExtraPath : " + text, "CleanUpPcExtraPath");
					Directory.Delete(text, true);
				}
			}
		}

		// Token: 0x06005609 RID: 22025 RVA: 0x0019FFCC File Offset: 0x0019E1CC
		public void FullBuildCheck()
		{
			if (NKCDefineManager.DEFINE_FULL_BUILD())
			{
				string @string = PlayerPrefs.GetString("NKC_FULL_BUILD_VERSION", "");
				string version = Application.version;
				NKCPatchDownloader.DebugLog(string.Concat(new string[]
				{
					"[NKC_FULL_BUILD_VERSION:",
					@string,
					"][ApplicationVersion:",
					version,
					"]"
				}), "FullBuildCheck");
				if (@string != version)
				{
					NKCPatchDownloader.DebugLog("Change FullBuildVersion", "FullBuildCheck");
					if (Directory.Exists(this.LocalDownloadPath))
					{
						NKCPatchDownloader.DebugLog("Delete _ " + this.LocalDownloadPath, "FullBuildCheck");
						Directory.Delete(this.LocalDownloadPath, true);
					}
					PlayerPrefs.SetString("NKC_FULL_BUILD_VERSION", Application.version);
				}
			}
		}

		// Token: 0x0600560A RID: 22026 RVA: 0x001A0088 File Offset: 0x0019E288
		private IEnumerator VersionFileDownload(string versionFilePath)
		{
			NKCPatchDownloader.<VersionFileDownload>d__90 <VersionFileDownload>d__ = new NKCPatchDownloader.<VersionFileDownload>d__90(0);
			<VersionFileDownload>d__.<>4__this = this;
			<VersionFileDownload>d__.versionFilePath = versionFilePath;
			return <VersionFileDownload>d__;
		}

		// Token: 0x0600560B RID: 22027 RVA: 0x001A00A0 File Offset: 0x0019E2A0
		private static string GetVersionFileDownLoadPath(string downloadServerAddress, bool useExtraPath)
		{
			string text = UnityEngine.Random.Range(1000000, 8000000).ToString();
			text += UnityEngine.Random.Range(1000000, 8000000).ToString();
			string str = "?p=" + text;
			string versionJson = NKCConnectionInfo.VersionJson;
			string result = downloadServerAddress + Utility.GetPlatformName() + versionJson + str;
			if (useExtraPath)
			{
				result = downloadServerAddress + "ExtraAsset" + versionJson + str;
			}
			return result;
		}

		// Token: 0x0600560C RID: 22028 RVA: 0x001A0117 File Offset: 0x0019E317
		protected IEnumerator DownloadAppVersion(bool extra)
		{
			Log.Debug("[DownloadAppVersion] DownLoader version file getting...", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchDownloader.cs", 504);
			string targetVersion = null;
			string versionFilePath = NKCPatchDownloader.GetVersionFileDownLoadPath(NKCConnectionInfo.DownloadServerAddress, extra);
			yield return this.VersionFileDownload(versionFilePath);
			string downloadServerAddress;
			if (this._versionJson == null)
			{
				Log.Debug(string.Concat(new string[]
				{
					"[DownloadAppVersion] Fail VersionFileDownload [Error:",
					this.m_webRequestError,
					"][Path:",
					versionFilePath,
					"]"
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchDownloader.cs", 516);
				versionFilePath = NKCPatchDownloader.GetVersionFileDownLoadPath(NKCConnectionInfo.DownloadServerAddress2, extra);
				yield return this.VersionFileDownload(versionFilePath);
				if (this._versionJson == null)
				{
					Log.Error(string.Concat(new string[]
					{
						"[DownloadAppVersion] Fail VersionFileDownload2 [Error:",
						this.m_webRequestError,
						"][Path:",
						versionFilePath,
						"]"
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchDownloader.cs", 525);
					this.CheckVersionImplAfterProcess(NKCPatchDownloader.BuildStatus.Error, NKCPatchDownloader.VersionStatus.Error, NKCUtilString.GET_STRING_FAIL_VERSION);
					yield break;
				}
				downloadServerAddress = NKCConnectionInfo.DownloadServerAddress2;
			}
			else
			{
				downloadServerAddress = NKCConnectionInfo.DownloadServerAddress;
			}
			this.m_webRequestError = string.Empty;
			bool flag = false;
			JSONNode jsonnode = this._versionJson["versionList"];
			JSONArray jsonarray = (jsonnode != null) ? jsonnode.AsArray : null;
			if (jsonarray != null)
			{
				targetVersion = jsonarray[0]["version"];
				flag = true;
				if (!extra)
				{
					NKCUtil.PatchVersion = targetVersion;
				}
				else
				{
					NKCUtil.PatchVersionEA = targetVersion;
				}
				this.CheckVersionImplAfterProcess(NKCPatchDownloader.BuildStatus.UpToDate, NKCPatchDownloader.VersionStatus.Unchecked, NKCUtilString.GET_STRING_FAIL_VERSION);
				Log.Debug("[DownloadAppVersion] Found downLoader version", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchDownloader.cs", 560);
			}
			if (!flag)
			{
				Log.Error(string.Format("[DownloadAppVersion] Not found downLoader version _ {0}", this._versionJson), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchDownloader.cs", 565);
				this.CheckVersionImplAfterProcess(NKCPatchDownloader.BuildStatus.RequireAppUpdate, NKCPatchDownloader.VersionStatus.Unchecked, "");
			}
			this.SetBaseDownloadPath(downloadServerAddress, targetVersion, extra);
			yield break;
		}

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x0600560D RID: 22029 RVA: 0x001A0130 File Offset: 0x0019E330
		public List<NKCPatchInfo.PatchFileInfo> DownloadList
		{
			get
			{
				if (!NKCPatchUtility.BackgroundPatchEnabled())
				{
					NKCPatchUtility.SaveDownloadType(NKCPatchDownloader.DownType.FullDownload);
					return this._downloadfiles;
				}
				NKCPatchDownloader.DownType downloadType = NKCPatchUtility.GetDownloadType();
				if (downloadType == NKCPatchDownloader.DownType.FullDownload)
				{
					return this._downloadfiles;
				}
				if (downloadType != NKCPatchDownloader.DownType.TutorialWithBackground)
				{
					throw new ArgumentOutOfRangeException();
				}
				return this._tutorialDownloadFiles;
			}
		}

		// Token: 0x0600560E RID: 22030 RVA: 0x001A0173 File Offset: 0x0019E373
		public bool IsBackGroundDownload()
		{
			return NKCPatchUtility.GetDownloadType() == NKCPatchDownloader.DownType.TutorialWithBackground && this._tutorialBackGroundDownloadFiles != null && this._tutorialBackGroundDownloadFiles.Count != 0;
		}

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x06005610 RID: 22032 RVA: 0x001A01A0 File Offset: 0x0019E3A0
		// (set) Token: 0x0600560F RID: 22031 RVA: 0x001A0197 File Offset: 0x0019E397
		private protected bool _succeedDownloadlatestManifest { protected get; private set; }

		// Token: 0x06005611 RID: 22033 RVA: 0x001A01A8 File Offset: 0x0019E3A8
		public IEnumerator DownloadLatestManifest(bool extra)
		{
			this._succeedDownloadlatestManifest = false;
			yield return PatchManifestManager.DownloadLatestManifest(extra);
			if (!PatchManifestManager.SuccessLatestManifest)
			{
				NKCPatchDownloader.ErrorLog(string.Format("[extra:{0}] LatestManifest download fail", extra), "DownloadLatestManifest");
				this.CheckVersionImplAfterProcess(NKCPatchDownloader.BuildStatus.Error, NKCPatchDownloader.VersionStatus.Error, NKCUtilString.GET_STRING_FAIL_PATCHDATA);
				yield break;
			}
			if (PatchManifestManager.GetLatestPatchInfoFor(extra) == null)
			{
				NKCPatchDownloader.ErrorLog(string.Format("[extra:{0}] Not found latestManifest", extra), "DownloadLatestManifest");
				this.CheckVersionImplAfterProcess(NKCPatchDownloader.BuildStatus.Error, NKCPatchDownloader.VersionStatus.Error, NKCUtilString.GET_STRING_FAIL_PATCHDATA);
				yield break;
			}
			this._succeedDownloadlatestManifest = true;
			yield break;
		}

		// Token: 0x06005612 RID: 22034 RVA: 0x001A01BE File Offset: 0x0019E3BE
		public IEnumerator DownloadTutorialPatchData()
		{
			if (!NKCPatchUtility.BackgroundPatchEnabled())
			{
				yield break;
			}
			string text = Path.Combine(PatchManifestPath.GetLocalDownloadPath(false), PatchManifestPath.TutorialPatchFileName);
			if (File.Exists(text))
			{
				File.Delete(text);
			}
			UnityWebRequestDownloader unityWebRequestDownloader = new UnityWebRequestDownloader(this.ServerBaseAddress, this.LocalDownloadPath);
			UnityWebRequestDownloader unityWebRequestDownloader2 = unityWebRequestDownloader;
			unityWebRequestDownloader2.dOnDownloadCompleted = (NKCFileDownloader.OnDownloadCompleted)Delegate.Combine(unityWebRequestDownloader2.dOnDownloadCompleted, new NKCFileDownloader.OnDownloadCompleted(delegate(bool result)
			{
				Log.Debug(string.Format("[DownloadTutorialPatchData] Download Result[{0}]", result), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchDownloader.cs", 675);
			}));
			Log.Debug("[DownloadTutorialPatchData] FilePath[" + text + "] Download Start", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchDownloader.cs", 677);
			yield return unityWebRequestDownloader.DownloadFile(PatchManifestPath.TutorialPatchFileName, PatchManifestPath.TutorialPatchFileName, 0L);
			yield break;
		}

		// Token: 0x06005613 RID: 22035 RVA: 0x001A01CD File Offset: 0x0019E3CD
		public IEnumerator GetDownloadFiles(List<string> lstVariants, bool extra)
		{
			NKCPatchInfo currentPatchInfo = PatchManifestManager.GetCurrentPatchInfoFor(extra);
			if (currentPatchInfo == null)
			{
				NKCPatchDownloader.ErrorLog(string.Format("[extra:{0}] CurrentPatchInfo is null", extra), "GetDownloadFiles");
				this.CheckVersionImplAfterProcess(NKCPatchDownloader.BuildStatus.Error, NKCPatchDownloader.VersionStatus.Error, NKCUtilString.GET_STRING_FAIL_PATCHDATA);
				yield break;
			}
			NKCPatchInfo downloadHistoryPatchInfoFor = PatchManifestManager.GetDownloadHistoryPatchInfoFor(extra);
			if (downloadHistoryPatchInfoFor != null)
			{
				currentPatchInfo = currentPatchInfo.Append(downloadHistoryPatchInfoFor);
			}
			NKCPatchInfo backgroundDownloadHistoryPatchInfo = PatchManifestManager.OptimizationPatchInfoController.GetBackgroundDownloadHistoryPatchInfo();
			if (backgroundDownloadHistoryPatchInfo != null)
			{
				currentPatchInfo = currentPatchInfo.Append(backgroundDownloadHistoryPatchInfo);
			}
			bool bCheckPreExistCurPatchInfo = currentPatchInfo.m_dicPatchInfo.Count > 0;
			NKCPatchInfo latestPatchInfoFor = PatchManifestManager.GetLatestPatchInfoFor(extra);
			if (latestPatchInfoFor == null)
			{
				NKCPatchDownloader.ErrorLog(string.Format("[extra:{0}] latestPatchInfo is null", extra), "GetDownloadFiles");
				this.CheckVersionImplAfterProcess(NKCPatchDownloader.BuildStatus.Error, NKCPatchDownloader.VersionStatus.Error, NKCUtilString.GET_STRING_FAIL_PATCHDATA);
				yield break;
			}
			NKCPatchInfo filteredPatchInfo = extra ? latestPatchInfoFor : PatchManifestManager.BasePatchInfoController.CreateFilteredManifestInfo(latestPatchInfoFor, lstVariants);
			List<NKCPatchInfo.PatchFileInfo> downloadContainer = extra ? this._extraDownloadFiles : this._downloadfiles;
			string localBasePath = extra ? this.ExtraLocalDownloadPath : this.LocalDownloadPath;
			yield return base.StartCoroutine(PatchManifestManager.GetDownloadList(downloadContainer, bCheckPreExistCurPatchInfo, currentPatchInfo, filteredPatchInfo, localBasePath, this.m_bIntegrityCheck, this.onIntegrityCheckProgress));
			NKCPatchDownloader.DebugLog(string.Format("[extra:{0}][curVer:{1}][latestVer:{2}][downLoadListCount:{3}] Download list created ", new object[]
			{
				extra,
				currentPatchInfo.VersionString,
				filteredPatchInfo.VersionString,
				downloadContainer.Count
			}), "GetDownloadFiles");
			if (this.m_bIntegrityCheck || NKCPatchUtility.GetTutorialClearedStatus() || NKCPatchUtility.GetDownloadType() == NKCPatchDownloader.DownType.FullDownload)
			{
				NKCPatchUtility.SaveDownloadType(NKCPatchDownloader.DownType.FullDownload);
			}
			else if (!extra && NKCPatchUtility.BackgroundPatchEnabled())
			{
				NKCPatchInfo tutorialPatchInfo = PatchManifestManager.OptimizationPatchInfoController.CreateTutorialOnlyManifestInfo(filteredPatchInfo, lstVariants);
				if (tutorialPatchInfo == null)
				{
					NKCPatchUtility.SaveDownloadType(NKCPatchDownloader.DownType.FullDownload);
				}
				else
				{
					yield return base.StartCoroutine(PatchManifestManager.GetDownloadList(this._tutorialDownloadFiles, bCheckPreExistCurPatchInfo, currentPatchInfo, tutorialPatchInfo, localBasePath, this.m_bIntegrityCheck, this.onIntegrityCheckProgress));
					NKCPatchInfo latestManifest = filteredPatchInfo.DifferenceOfSetBy(tutorialPatchInfo);
					yield return base.StartCoroutine(PatchManifestManager.GetDownloadList(this._tutorialBackGroundDownloadFiles, bCheckPreExistCurPatchInfo, currentPatchInfo, latestManifest, localBasePath, this.m_bIntegrityCheck, this.onIntegrityCheckProgress));
					NKCPatchUtility.SaveDownloadType(NKCPatchDownloader.DownType.TutorialWithBackground);
				}
				tutorialPatchInfo = null;
			}
			yield return null;
			yield break;
		}

		// Token: 0x06005614 RID: 22036 RVA: 0x001A01EC File Offset: 0x0019E3EC
		protected void ClearFileDownloadContainer()
		{
			List<NKCPatchInfo.PatchFileInfo> downloadfiles = this._downloadfiles;
			if (downloadfiles != null)
			{
				downloadfiles.Clear();
			}
			List<NKCPatchInfo.PatchFileInfo> extraDownloadFiles = this._extraDownloadFiles;
			if (extraDownloadFiles != null)
			{
				extraDownloadFiles.Clear();
			}
			List<NKCPatchInfo.PatchFileInfo> tutorialDownloadFiles = this._tutorialDownloadFiles;
			if (tutorialDownloadFiles != null)
			{
				tutorialDownloadFiles.Clear();
			}
			List<NKCPatchInfo.PatchFileInfo> tutorialBackGroundDownloadFiles = this._tutorialBackGroundDownloadFiles;
			if (tutorialBackGroundDownloadFiles != null)
			{
				tutorialBackGroundDownloadFiles.Clear();
			}
			this._totalBytesToDownload = 0L;
		}

		// Token: 0x06005615 RID: 22037 RVA: 0x001A0248 File Offset: 0x0019E448
		public void OnEndVersionCheck()
		{
			if (this.DownloadList.Count > 0 || this._extraDownloadFiles.Count > 0)
			{
				this.CheckVersionImplAfterProcess(this.BuildCheckStatus, NKCPatchDownloader.VersionStatus.RequireDownload, "");
				return;
			}
			if (this.IsBackGroundDownload())
			{
				Log.Debug("[OnEndVersionChec ] Need to background down", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchDownloader.cs", 772);
				this.StartBackgroundDownload();
			}
			this.CheckVersionImplAfterProcess(this.BuildCheckStatus, NKCPatchDownloader.VersionStatus.UpToDate, "");
		}

		// Token: 0x06005616 RID: 22038 RVA: 0x001A02B8 File Offset: 0x0019E4B8
		public IEnumerator DownloadListCheck(List<string> variantList, bool extra)
		{
			yield return this.DownloadAppVersion(extra);
			if (this.BuildCheckStatus == NKCPatchDownloader.BuildStatus.Error)
			{
				NKCPatchDownloader.ErrorLog(string.Format("[extra:{0}] Fail DownloadAppVersion", extra), "DownloadListCheck");
				yield break;
			}
			if (this.BuildCheckStatus == NKCPatchDownloader.BuildStatus.RequireAppUpdate)
			{
				NKCPatchDownloader.ErrorLog(string.Format("[extra:{0}] Fail DownloadAppVersion", extra), "DownloadListCheck");
				yield break;
			}
			NKCPatchDownloader.DebugLog(string.Format("[extra:{0}][LocalDownloadPath:{1}] DownLoader Ini t Finished, Starting download patchInfo", extra, this.LocalDownloadPath), "DownloadListCheck");
			yield return this.DownloadLatestManifest(extra);
			if (this.BuildCheckStatus == NKCPatchDownloader.BuildStatus.Error)
			{
				yield break;
			}
			if (!extra)
			{
				yield return this.DownloadTutorialPatchData();
			}
			yield return this.GetDownloadFiles(variantList, extra);
			if (this.BuildCheckStatus == NKCPatchDownloader.BuildStatus.Error)
			{
				NKCPatchDownloader.ErrorLog(string.Format("[extra:{0}] Fail GetDownloadFiles", extra), "DownloadListCheck");
				yield break;
			}
			if (!PatchManifestManager.CleanUpFiles(extra))
			{
				NKCPatchDownloader.ErrorLog(string.Format("[extra:{0}] Fail CleanUpFiles ", extra), "DownloadListCheck");
				this.CheckVersionImplAfterProcess(NKCPatchDownloader.BuildStatus.Error, NKCPatchDownloader.VersionStatus.Error, NKCUtilString.GET_STRING_FAIL_PATCHDATA);
			}
			yield break;
		}

		// Token: 0x06005617 RID: 22039 RVA: 0x001A02D8 File Offset: 0x0019E4D8
		public virtual void MoveToMarket()
		{
			RuntimePlatform platform = Application.platform;
			if (platform <= RuntimePlatform.IPhonePlayer)
			{
				if (platform > RuntimePlatform.OSXPlayer && platform != RuntimePlatform.IPhonePlayer)
				{
					goto IL_40;
				}
			}
			else
			{
				if (platform == RuntimePlatform.Android)
				{
					Application.OpenURL("market://details?id=" + Application.identifier);
					Application.Quit();
					return;
				}
				if (platform != RuntimePlatform.tvOS)
				{
					goto IL_40;
				}
			}
			Application.Quit();
			return;
			IL_40:
			Application.Quit();
		}

		// Token: 0x06005618 RID: 22040 RVA: 0x001A032A File Offset: 0x0019E52A
		protected IEnumerator BeginCoroutine(Coroutine<string> routine, NKCPatchDownloader.OnError onError)
		{
			yield return routine.coroutine;
			try
			{
				NKCPatchDownloader.DebugLog(routine.Value, "BeginCoroutine");
				yield break;
			}
			catch (WebException ex)
			{
				if (ex.Status == WebExceptionStatus.ProtocolError)
				{
					string text = string.Format("{0} : {1}", ((HttpWebResponse)ex.Response).StatusCode, ((HttpWebResponse)ex.Response).StatusDescription);
					NKCPatchDownloader.ErrorLog(text, "BeginCoroutine");
					if (onError != null)
					{
						onError(text);
					}
				}
				else
				{
					NKCPatchDownloader.ErrorLog(ex.Message, "BeginCoroutine");
					if (onError != null)
					{
						onError(ex.Message);
					}
				}
				yield break;
			}
			catch (Exception ex2)
			{
				NKCPatchDownloader.ErrorLog(ex2.Message, "BeginCoroutine");
				if (onError != null)
				{
					onError(ex2.Message);
				}
				yield break;
			}
			yield break;
		}

		// Token: 0x06005619 RID: 22041 RVA: 0x001A0340 File Offset: 0x0019E540
		private static void DebugLog(string log, [CallerMemberName] string caller = "")
		{
			Log.Debug(string.Concat(new string[]
			{
				"[",
				NKCPatchDownloader._logHeader,
				"][",
				caller,
				"] _ ",
				log
			}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchDownloader.cs", 920);
		}

		// Token: 0x0600561A RID: 22042 RVA: 0x001A0390 File Offset: 0x0019E590
		private static void WarnLog(string log, [CallerMemberName] string caller = "")
		{
			Log.Warn(string.Concat(new string[]
			{
				"[",
				NKCPatchDownloader._logHeader,
				"][",
				caller,
				"] _ ",
				log
			}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchDownloader.cs", 925);
		}

		// Token: 0x0600561B RID: 22043 RVA: 0x001A03E0 File Offset: 0x0019E5E0
		private static void ErrorLog(string log, [CallerMemberName] string caller = "")
		{
			Log.Error(string.Concat(new string[]
			{
				"[",
				NKCPatchDownloader._logHeader,
				"][",
				caller,
				"] _ ",
				log
			}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchDownloader.cs", 930);
		}

		// Token: 0x0400448B RID: 17547
		public static NKCPatchDownloader Instance;

		// Token: 0x0400448C RID: 17548
		public const int MAX_ERROR_COUNT = 10;

		// Token: 0x0400448D RID: 17549
		public NKCPatchDownloader.OnError onError;

		// Token: 0x0400448E RID: 17550
		public NKCPatchDownloader.OnVersionCheckResult onVersionCheckResult;

		// Token: 0x0400448F RID: 17551
		public NKCPatchDownloader.OnDownloadProgress onDownloadProgress;

		// Token: 0x04004490 RID: 17552
		public NKCPatchDownloader.OnDownloadFinished onDownloadFinished;

		// Token: 0x04004491 RID: 17553
		public NKCPatchDownloader.OnIntegrityCheckProgress onIntegrityCheckProgress;

		// Token: 0x04004496 RID: 17558
		public bool ProloguePlay;

		// Token: 0x04004498 RID: 17560
		private DateTime m_dtNextVersionCheckTime;

		// Token: 0x04004499 RID: 17561
		private bool m_bIntegrityCheck;

		// Token: 0x0400449E RID: 17566
		private const string _extraAssetPath = "ExtraAsset";

		// Token: 0x0400449F RID: 17567
		private JSONNode _versionJson;

		// Token: 0x040044A0 RID: 17568
		private string m_webRequestError;

		// Token: 0x040044A1 RID: 17569
		protected readonly List<NKCPatchInfo.PatchFileInfo> _downloadfiles = new List<NKCPatchInfo.PatchFileInfo>();

		// Token: 0x040044A2 RID: 17570
		protected readonly List<NKCPatchInfo.PatchFileInfo> _extraDownloadFiles = new List<NKCPatchInfo.PatchFileInfo>();

		// Token: 0x040044A3 RID: 17571
		protected readonly List<NKCPatchInfo.PatchFileInfo> _tutorialDownloadFiles = new List<NKCPatchInfo.PatchFileInfo>();

		// Token: 0x040044A4 RID: 17572
		protected readonly List<NKCPatchInfo.PatchFileInfo> _tutorialBackGroundDownloadFiles = new List<NKCPatchInfo.PatchFileInfo>();

		// Token: 0x040044A5 RID: 17573
		protected long _totalBytesToDownload;

		// Token: 0x040044A6 RID: 17574
		protected long _currentBytesToDownload;

		// Token: 0x040044A8 RID: 17576
		private static string _logHeader = "NKCPatchDownloader";

		// Token: 0x0200152C RID: 5420
		public enum BuildStatus
		{
			// Token: 0x0400A00E RID: 40974
			Unchecked,
			// Token: 0x0400A00F RID: 40975
			UpToDate,
			// Token: 0x0400A010 RID: 40976
			UpdateAvailable,
			// Token: 0x0400A011 RID: 40977
			RequireAppUpdate,
			// Token: 0x0400A012 RID: 40978
			Error
		}

		// Token: 0x0200152D RID: 5421
		public enum VersionStatus
		{
			// Token: 0x0400A014 RID: 40980
			Unchecked,
			// Token: 0x0400A015 RID: 40981
			UpToDate,
			// Token: 0x0400A016 RID: 40982
			RequireDownload,
			// Token: 0x0400A017 RID: 40983
			Downloading,
			// Token: 0x0400A018 RID: 40984
			Error
		}

		// Token: 0x0200152E RID: 5422
		public enum PatchDownloadStatus
		{
			// Token: 0x0400A01A RID: 40986
			Idle,
			// Token: 0x0400A01B RID: 40987
			Downloading,
			// Token: 0x0400A01C RID: 40988
			UserCancel,
			// Token: 0x0400A01D RID: 40989
			Finished,
			// Token: 0x0400A01E RID: 40990
			Error,
			// Token: 0x0400A01F RID: 40991
			UpdateRequired
		}

		// Token: 0x0200152F RID: 5423
		// (Invoke) Token: 0x0600ABFA RID: 44026
		public delegate void OnError(string Error);

		// Token: 0x02001530 RID: 5424
		// (Invoke) Token: 0x0600ABFE RID: 44030
		public delegate void OnVersionCheckResult(NKCPatchDownloader.BuildStatus buildStatus, NKCPatchDownloader.VersionStatus versionStatus, string ErrorCode);

		// Token: 0x02001531 RID: 5425
		// (Invoke) Token: 0x0600AC02 RID: 44034
		public delegate void OnDownloadProgress(long currentByte, long totalByte);

		// Token: 0x02001532 RID: 5426
		// (Invoke) Token: 0x0600AC06 RID: 44038
		public delegate void OnDownloadFinished(NKCPatchDownloader.PatchDownloadStatus downloadStatus, string ErrorCode);

		// Token: 0x02001533 RID: 5427
		// (Invoke) Token: 0x0600AC0A RID: 44042
		public delegate void OnIntegrityCheckProgress(int currentCount, int totalCount);

		// Token: 0x02001534 RID: 5428
		public enum DownType
		{
			// Token: 0x0400A021 RID: 40993
			FullDownload,
			// Token: 0x0400A022 RID: 40994
			TutorialWithBackground,
			// Token: 0x0400A023 RID: 40995
			Count
		}
	}
}
