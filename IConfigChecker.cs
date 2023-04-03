using System;
using System.Collections;
using NKC.Patcher;

// Token: 0x02000009 RID: 9
public interface IConfigChecker
{
	// Token: 0x17000008 RID: 8
	// (get) Token: 0x06000027 RID: 39
	string PatchFileAddress { get; }

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000028 RID: 40
	NKCPatchDownloader.VersionStatus vs { get; }

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000029 RID: 41
	string VersionInfoStr { get; }

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x0600002A RID: 42
	string DownLoadAddress { get; }

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x0600002B RID: 43
	string DefaultVersionJson { get; }

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x0600002C RID: 44
	string AssetBundleVersion { get; }

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x0600002D RID: 45
	string logStr { get; }

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x0600002E RID: 46
	string ErrorSolutionStr { get; }

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x06000030 RID: 48
	// (set) Token: 0x0600002F RID: 47
	string BaseFileServerAddress { get; set; }

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x06000032 RID: 50
	// (set) Token: 0x06000031 RID: 49
	string ProtocolVersion { get; set; }

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x06000034 RID: 52
	// (set) Token: 0x06000033 RID: 51
	bool OpenTag { get; set; }

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x06000036 RID: 54
	// (set) Token: 0x06000035 RID: 53
	bool ContentTag { get; set; }

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x06000037 RID: 55
	bool IsSaveToTag { get; }

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x06000038 RID: 56
	NKCPatchInfo PatchInfo { get; }

	// Token: 0x06000039 RID: 57
	void Init();

	// Token: 0x0600003A RID: 58
	IEnumerator StartDownLoad();

	// Token: 0x0600003B RID: 59
	IEnumerator ConfigRequest();

	// Token: 0x0600003C RID: 60
	IEnumerator UpdateServerInfo();

	// Token: 0x0600003D RID: 61
	IEnumerator RunAll();

	// Token: 0x0600003E RID: 62
	IEnumerator ExtraAssetRunAll();

	// Token: 0x0600003F RID: 63
	void RequestLoginConnection();

	// Token: 0x06000040 RID: 64
	void Update();

	// Token: 0x06000041 RID: 65
	void SaveTagListToText();

	// Token: 0x06000042 RID: 66
	void SaveLogToText();
}
