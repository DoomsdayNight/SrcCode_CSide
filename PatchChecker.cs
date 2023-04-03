using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AssetBundles;
using NKC;
using NKC.Patcher;
using NKC.Publisher;
using NKM.Templet.Base;
using SimpleJSON;
using UnityEngine;

// Token: 0x0200000A RID: 10
public class PatchChecker : IConfigChecker
{
	// Token: 0x17000016 RID: 22
	// (get) Token: 0x06000043 RID: 67 RVA: 0x000028BB File Offset: 0x00000ABB
	public string PatchFileAddress
	{
		get
		{
			if (NKCPublisherModule.ServerInfo == null)
			{
				return string.Empty;
			}
			if (string.IsNullOrEmpty(NKCConnectionInfo.DownloadServerAddress2))
			{
				return NKCConnectionInfo.DownloadServerAddress;
			}
			return NKCConnectionInfo.DownloadServerAddress2;
		}
	}

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x06000044 RID: 68 RVA: 0x000028E4 File Offset: 0x00000AE4
	public string DownLoadAddress
	{
		get
		{
			if (string.IsNullOrEmpty(this.AssetBundleVersion))
			{
				return string.Empty;
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

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x06000045 RID: 69 RVA: 0x00002939 File Offset: 0x00000B39
	public NKCPatchDownloader.VersionStatus vs
	{
		get
		{
			if (NKCPatchDownloader.Instance == null)
			{
				return NKCPatchDownloader.VersionStatus.Unchecked;
			}
			return NKCPatchDownloader.Instance.VersionCheckStatus;
		}
	}

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x06000046 RID: 70 RVA: 0x00002954 File Offset: 0x00000B54
	public string VersionInfoStr
	{
		get
		{
			this._tempSb.Clear();
			if (NKCPatchDownloader.Instance == null)
			{
				return "Not Set";
			}
			return this._tempSb.Append("Address              : ").Append(this.PatchFileAddress).AppendLine().Append("Build Check Status   : ").Append(NKCPatchDownloader.Instance.BuildCheckStatus).AppendLine().Append("Version Check Status : ").Append(NKCPatchDownloader.Instance.VersionCheckStatus).AppendLine().Append("Download Status      : ").Append(NKCPatchDownloader.Instance.DownloadStatus).AppendLine().Append("Total Size           : ").Append((float)NKCPatchDownloader.Instance.TotalSize / 1048576f).Append(" [MB]").AppendLine().Append("Current Size         : ").Append(NKCPatchDownloader.Instance.CurrentSize).ToString();
		}
	}

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x06000047 RID: 71 RVA: 0x00002A56 File Offset: 0x00000C56
	public string DefaultVersionJson
	{
		get
		{
			return "/version.json";
		}
	}

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x06000049 RID: 73 RVA: 0x00002A66 File Offset: 0x00000C66
	// (set) Token: 0x06000048 RID: 72 RVA: 0x00002A5D File Offset: 0x00000C5D
	public string AssetBundleVersion { get; private set; }

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x0600004A RID: 74 RVA: 0x00002A70 File Offset: 0x00000C70
	public string logStr
	{
		get
		{
			if (this._sb.Length > 0)
			{
				string text = this._sb.ToString();
				if (!string.IsNullOrEmpty(text))
				{
					this._sb.Clear();
					if (!string.Equals(this._lastLog, text, StringComparison.Ordinal))
					{
						this._lastLog = text;
					}
				}
			}
			return this._lastLog;
		}
	}

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x0600004B RID: 75 RVA: 0x00002AC7 File Offset: 0x00000CC7
	public string ErrorSolutionStr
	{
		get
		{
			if (this._str == string.Empty)
			{
				return "-";
			}
			return this._str;
		}
	}

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x0600004C RID: 76 RVA: 0x00002AE7 File Offset: 0x00000CE7
	// (set) Token: 0x0600004D RID: 77 RVA: 0x00002AF4 File Offset: 0x00000CF4
	public string BaseFileServerAddress
	{
		get
		{
			return this._configCheckerImplementation.BaseFileServerAddress;
		}
		set
		{
			this._configCheckerImplementation.BaseFileServerAddress = value;
		}
	}

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x0600004E RID: 78 RVA: 0x00002B02 File Offset: 0x00000D02
	// (set) Token: 0x0600004F RID: 79 RVA: 0x00002B0A File Offset: 0x00000D0A
	public string ProtocolVersion { get; set; }

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x06000050 RID: 80 RVA: 0x00002B13 File Offset: 0x00000D13
	// (set) Token: 0x06000051 RID: 81 RVA: 0x00002B1B File Offset: 0x00000D1B
	public bool OpenTag { get; set; }

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x06000052 RID: 82 RVA: 0x00002B24 File Offset: 0x00000D24
	// (set) Token: 0x06000053 RID: 83 RVA: 0x00002B2C File Offset: 0x00000D2C
	public bool ContentTag { get; set; }

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x06000054 RID: 84 RVA: 0x00002B35 File Offset: 0x00000D35
	public bool IsSaveToTag { get; }

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x06000056 RID: 86 RVA: 0x00002B46 File Offset: 0x00000D46
	// (set) Token: 0x06000055 RID: 85 RVA: 0x00002B3D File Offset: 0x00000D3D
	public bool UseExtraAsset { get; set; }

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x06000057 RID: 87 RVA: 0x00002B4E File Offset: 0x00000D4E
	public NKCPatchInfo PatchInfo
	{
		get
		{
			return this._patchInfo;
		}
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x06000058 RID: 88 RVA: 0x00002B56 File Offset: 0x00000D56
	private string key
	{
		get
		{
			return "[ PatchChecker ]";
		}
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00002B5D File Offset: 0x00000D5D
	public void Log(string log)
	{
		Debug.Log(this.key + "   " + log);
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00002B75 File Offset: 0x00000D75
	public void LogClear(bool sbClear = true, bool lastLogClear = true)
	{
		this._str = string.Empty;
		if (sbClear)
		{
			this._sb.Clear();
		}
		if (lastLogClear)
		{
			this._lastLog = string.Empty;
		}
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00002B9F File Offset: 0x00000D9F
	public IEnumerator StartDownLoad()
	{
		yield break;
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00002BA7 File Offset: 0x00000DA7
	public IEnumerator ConfigRequest()
	{
		NKCPublisherModule.InitInstance(new NKCPublisherModule.OnComplete(this.InitInstance));
		if (NKCDefineManager.DEFINE_EXTRA_ASSET() || NKCDefineManager.DEFINE_ZLONG_CHN())
		{
			LegacyPatchDownloader.InitInstance(new NKCPatchDownloader.OnError(this.DownLoaderInitError));
		}
		else
		{
			NKCPatchParallelDownloader.InitInstance(new NKCPatchDownloader.OnError(this.DownLoaderInitError));
		}
		NKCPatchDownloader.Instance.InitCheckTime();
		NKCPatchDownloader.Instance.CheckVersion(new List<string>(AssetBundleManager.ActiveVariants), false);
		NKCPatchDownloader instance = NKCPatchDownloader.Instance;
		instance.onVersionCheckResult = (NKCPatchDownloader.OnVersionCheckResult)Delegate.Remove(instance.onVersionCheckResult, new NKCPatchDownloader.OnVersionCheckResult(this.OnVersionCheckResult));
		NKCPatchDownloader instance2 = NKCPatchDownloader.Instance;
		instance2.onVersionCheckResult = (NKCPatchDownloader.OnVersionCheckResult)Delegate.Combine(instance2.onVersionCheckResult, new NKCPatchDownloader.OnVersionCheckResult(this.OnVersionCheckResult));
		NKCPatchDownloader instance3 = NKCPatchDownloader.Instance;
		instance3.onDownloadProgress = (NKCPatchDownloader.OnDownloadProgress)Delegate.Remove(instance3.onDownloadProgress, new NKCPatchDownloader.OnDownloadProgress(this.OnDownLoadProgress));
		NKCPatchDownloader instance4 = NKCPatchDownloader.Instance;
		instance4.onDownloadProgress = (NKCPatchDownloader.OnDownloadProgress)Delegate.Combine(instance4.onDownloadProgress, new NKCPatchDownloader.OnDownloadProgress(this.OnDownLoadProgress));
		NKCPatchDownloader instance5 = NKCPatchDownloader.Instance;
		instance5.onDownloadFinished = (NKCPatchDownloader.OnDownloadFinished)Delegate.Remove(instance5.onDownloadFinished, new NKCPatchDownloader.OnDownloadFinished(this.OnDownLoadFinished));
		NKCPatchDownloader instance6 = NKCPatchDownloader.Instance;
		instance6.onDownloadFinished = (NKCPatchDownloader.OnDownloadFinished)Delegate.Combine(instance6.onDownloadFinished, new NKCPatchDownloader.OnDownloadFinished(this.OnDownLoadFinished));
		yield return null;
		yield break;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00002BB6 File Offset: 0x00000DB6
	public IEnumerator UpdateServerInfo()
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00002BBD File Offset: 0x00000DBD
	public IEnumerator RunAll()
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00002BC4 File Offset: 0x00000DC4
	public IEnumerator ExtraAssetRunAll()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00002BCB File Offset: 0x00000DCB
	public void RequestLoginConnection()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00002BD2 File Offset: 0x00000DD2
	public void Update()
	{
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00002BD4 File Offset: 0x00000DD4
	public void SaveTagListToText()
	{
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00002BD6 File Offset: 0x00000DD6
	public void SaveLogToText()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00002BDD File Offset: 0x00000DDD
	public void Init()
	{
		Application.logMessageReceived -= this.Handling;
		Application.logMessageReceived += this.Handling;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00002C01 File Offset: 0x00000E01
	public void DownLoaderInitError(string error)
	{
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00002C04 File Offset: 0x00000E04
	public void OnVersionCheckResult(NKCPatchDownloader.BuildStatus buildStatus, NKCPatchDownloader.VersionStatus versionStatus, string errorStr)
	{
		if (string.IsNullOrEmpty(errorStr))
		{
			return;
		}
		if (string.Equals(errorStr, "Patcher not initialzed"))
		{
			this._str = "Patcher가 초기화되지 않았습니다. Patcher 코드를 확인해야 합니다.";
			return;
		}
		if (string.Equals(errorStr, NKCUtilString.GET_STRING_FAIL_VERSION))
		{
			this._str = "버전 파일 다운로드 실패. Config 파일을 확인해 주세요.";
			return;
		}
		if (buildStatus == NKCPatchDownloader.BuildStatus.RequireAppUpdate && versionStatus == NKCPatchDownloader.VersionStatus.Unchecked)
		{
			this._str = this.PatchFileAddress + " 파일에서 version 정보 찾지 못함";
			return;
		}
		if (string.Equals(errorStr, NKCUtilString.GET_STRING_FAIL_PATCHDATA))
		{
			this._str = "패치파일 다운로드 실패, 파일의 다운 버전을 확인해주세요.";
			return;
		}
		this._str = errorStr;
		Debug.Log(errorStr);
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00002C90 File Offset: 0x00000E90
	public void OnDownLoadProgress(long currentByte, long totalByte)
	{
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00002C92 File Offset: 0x00000E92
	public void OnDownLoadFinished(NKCPatchDownloader.PatchDownloadStatus downloadStatus, string errorCode)
	{
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00002C94 File Offset: 0x00000E94
	public void InitInstance(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError = null)
	{
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00002C96 File Offset: 0x00000E96
	public void Handling(string log, string stackTrace, LogType type)
	{
		if (string.IsNullOrEmpty(stackTrace))
		{
			return;
		}
		this._sb.Append(string.Format("[{0}] ", type)).Append(log).AppendLine();
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00002CC8 File Offset: 0x00000EC8
	public static IEnumerable GetEnumerableOfType1<T>(params object[] constructorArgs)
	{
		List<T> result = new List<T>();
		Type typeFromHandle = typeof(T);
		foreach (Type type in Assembly.Load("Assembly-CSharp").GetTypes())
		{
			if (type.Name == "NKMFierceBossGroupTemplet")
			{
				bool flag = typeFromHandle.IsSubclassOf(type);
				type.IsSubclassOf(typeof(INKMTemplet));
				type.IsAssignableFrom(typeof(INKMTemplet));
				bool isInterface = type.IsInterface;
				typeof(INKMTemplet).IsSubclassOf(type);
				typeof(INKMTemplet).IsAssignableFrom(type);
				bool isInterface2 = typeof(INKMTemplet).IsInterface;
				Debug.Log(string.Format("equal name => {0}", flag));
			}
			if (typeFromHandle.IsAssignableFrom(type))
			{
				Debug.Log(type);
			}
		}
		return result;
	}

	// Token: 0x04000019 RID: 25
	private string _lastLog;

	// Token: 0x0400001F RID: 31
	private NKCPatchInfo _patchInfo;

	// Token: 0x04000020 RID: 32
	private JSONNode _versionJson;

	// Token: 0x04000021 RID: 33
	private readonly StringBuilder _sb = new StringBuilder();

	// Token: 0x04000022 RID: 34
	private readonly StringBuilder _tempSb = new StringBuilder();

	// Token: 0x04000023 RID: 35
	private string _str;

	// Token: 0x04000024 RID: 36
	private IConfigChecker _configCheckerImplementation;
}
