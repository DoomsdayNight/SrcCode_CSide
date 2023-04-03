using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using AssetBundles;
using Cs.Logging;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000809 RID: 2057
	public class NKCObbUtil
	{
		// Token: 0x0600519F RID: 20895 RVA: 0x0018C736 File Offset: 0x0018A936
		public static bool IsOBBPath(string path)
		{
			return NKCObbUtil.s_bLoadedOBB && path.Contains("CS_OBB_FILE/");
		}

		// Token: 0x060051A0 RID: 20896 RVA: 0x0018C74C File Offset: 0x0018A94C
		private static void InitOBB_EntryIndex_(NKCObbUtil.OBB_TYPE obbType)
		{
			NKCObbUtil.DebugLog("Start", "InitOBB_EntryIndex_");
			string.IsNullOrWhiteSpace(NKCObbUtil.GetOBBFullPath(obbType.ToString()));
		}

		// Token: 0x060051A1 RID: 20897 RVA: 0x0018C775 File Offset: 0x0018A975
		public static void Init()
		{
			Log.Debug("[NKCObbUtil] Init", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Utility/NKCObbUtil.cs", 100);
			NKCObbUtil.InitOBB_EntryIndex();
		}

		// Token: 0x060051A2 RID: 20898 RVA: 0x0018C790 File Offset: 0x0018A990
		public static void ExtractFile(string relativePath)
		{
			string localDownloadPath = AssetBundleManager.GetLocalDownloadPath();
			for (int i = 0; i < 2; i++)
			{
				NKCObbUtil.OBB_EntryInfo obb_EntryInfo;
				if (NKCObbUtil.s_lst_dicOBB[i].dicObb_EntryInfo.TryGetValue(relativePath, out obb_EntryInfo))
				{
					string text = Path.Combine(localDownloadPath, relativePath);
					string directoryName = Path.GetDirectoryName(text);
					NKCObbUtil.DebugLog(string.Concat(new string[]
					{
						"[fullPath:",
						text,
						"][relativePath:",
						relativePath,
						"]"
					}), "ExtractFile");
					if (!Directory.Exists(directoryName))
					{
						Directory.CreateDirectory(directoryName);
					}
					try
					{
						File.WriteAllBytes(text, NKCObbUtil.GetEntryBufferByIndex((NKCObbUtil.OBB_TYPE)i, obb_EntryInfo.index));
					}
					catch (Exception ex)
					{
						NKCObbUtil.ErrorLog(string.Concat(new string[]
						{
							"[fullPath:",
							text,
							"][ExceptionMessage:",
							ex.Message,
							"] obb extract error exception"
						}), "ExtractFile");
						if (File.Exists(text))
						{
							File.Delete(text);
						}
					}
					return;
				}
			}
		}

		// Token: 0x060051A3 RID: 20899 RVA: 0x0018C89C File Offset: 0x0018AA9C
		public static void InitOBB_EntryIndex()
		{
			if (NKCObbUtil.s_lst_dicOBB.Count > 0)
			{
				return;
			}
			for (int i = 0; i < 2; i++)
			{
				NKCObbUtil.s_lst_dicOBB.Add(new NKCObbUtil.OBB_Info());
			}
			NKCObbUtil.s_bLoadedOBB = false;
			for (int j = 0; j < 2; j++)
			{
				NKCObbUtil.InitOBB_EntryIndex_((NKCObbUtil.OBB_TYPE)j);
			}
		}

		// Token: 0x060051A4 RID: 20900 RVA: 0x0018C8EC File Offset: 0x0018AAEC
		public static NKCObbUtil.OBB_TYPE GetObbType(string relativePath)
		{
			for (int i = 0; i < 2; i++)
			{
				if (NKCObbUtil.s_lst_dicOBB[i].dicObb_EntryInfo.ContainsKey(relativePath))
				{
					return (NKCObbUtil.OBB_TYPE)i;
				}
			}
			return NKCObbUtil.OBB_TYPE.num;
		}

		// Token: 0x060051A5 RID: 20901 RVA: 0x0018C920 File Offset: 0x0018AB20
		public static int GetObbEntryIndex(string relativePath)
		{
			return NKCObbUtil.GetObbEntryIndex(NKCObbUtil.GetObbType(relativePath), relativePath);
		}

		// Token: 0x060051A6 RID: 20902 RVA: 0x0018C930 File Offset: 0x0018AB30
		public static int GetObbEntryIndex(NKCObbUtil.OBB_TYPE obbType, string relativePath)
		{
			if (obbType == NKCObbUtil.OBB_TYPE.num)
			{
				return -1;
			}
			for (int i = 0; i < 2; i++)
			{
				NKCObbUtil.OBB_EntryInfo obb_EntryInfo;
				if (NKCObbUtil.s_lst_dicOBB[i].dicObb_EntryInfo.TryGetValue(relativePath, out obb_EntryInfo))
				{
					return obb_EntryInfo.index;
				}
			}
			return -1;
		}

		// Token: 0x060051A7 RID: 20903 RVA: 0x0018C971 File Offset: 0x0018AB71
		public static ulong GetObbEntrySize(string relativePath)
		{
			return NKCObbUtil.GetObbEntrySize(NKCObbUtil.GetObbType(relativePath), relativePath);
		}

		// Token: 0x060051A8 RID: 20904 RVA: 0x0018C980 File Offset: 0x0018AB80
		public static ulong GetObbEntrySize(NKCObbUtil.OBB_TYPE obbType, string relativePath)
		{
			if (obbType == NKCObbUtil.OBB_TYPE.num)
			{
				return 0UL;
			}
			NKCObbUtil.OBB_EntryInfo obb_EntryInfo;
			if (!NKCObbUtil.s_lst_dicOBB[(int)obbType].dicObb_EntryInfo.TryGetValue(relativePath, out obb_EntryInfo))
			{
				return 0UL;
			}
			return obb_EntryInfo.size;
		}

		// Token: 0x060051A9 RID: 20905 RVA: 0x0018C9B7 File Offset: 0x0018ABB7
		public static string GetRelativePathFromOBB(string path)
		{
			if (string.IsNullOrWhiteSpace(path))
			{
				return "";
			}
			return path.Remove(0, 12);
		}

		// Token: 0x060051AA RID: 20906 RVA: 0x0018C9D0 File Offset: 0x0018ABD0
		public static byte[] GetEntryBufferByFullPath(string fullPath)
		{
			string relativePathFromOBB = NKCObbUtil.GetRelativePathFromOBB(fullPath);
			NKCObbUtil.OBB_TYPE obbType = NKCObbUtil.GetObbType(relativePathFromOBB);
			int obbEntryIndex = NKCObbUtil.GetObbEntryIndex(obbType, relativePathFromOBB);
			return NKCObbUtil.GetEntryBufferByIndex(obbType, obbEntryIndex);
		}

		// Token: 0x060051AB RID: 20907 RVA: 0x0018C9F8 File Offset: 0x0018ABF8
		private static byte[] GetEntryBufferByIndex(NKCObbUtil.OBB_TYPE obbType, int index)
		{
			if (obbType == NKCObbUtil.OBB_TYPE.num)
			{
				return null;
			}
			if (index < 0)
			{
				return null;
			}
			NKCObbUtil.OBB_Info obb_Info = NKCObbUtil.s_lst_dicOBB[(int)obbType];
			if (obb_Info != null)
			{
				BinaryReader br = obb_Info.br;
			}
			return null;
		}

		// Token: 0x060051AC RID: 20908 RVA: 0x0018CA28 File Offset: 0x0018AC28
		private static int GetAppBundleVersion()
		{
			if (NKCObbUtil.s_OBB_Version > 0)
			{
				return NKCObbUtil.s_OBB_Version;
			}
			if (Application.platform == RuntimePlatform.Android)
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
					NKCObbUtil.s_OBB_Version = @static.Call<AndroidJavaObject>("getPackageManager", Array.Empty<object>()).Call<AndroidJavaObject>("getPackageInfo", new object[]
					{
						@static.Call<string>("getPackageName", Array.Empty<object>()),
						0
					}).Get<int>("versionCode");
				}
			}
			return NKCObbUtil.s_OBB_Version;
		}

		// Token: 0x060051AD RID: 20909 RVA: 0x0018CAD0 File Offset: 0x0018ACD0
		private static string GetOBBFullPath(string obbType)
		{
			return NKCObbUtil.GetAndroidInternalFileFullPath(string.Concat(new string[]
			{
				"Android/obb/",
				Application.identifier,
				"/",
				obbType,
				".",
				NKCObbUtil.GetAppBundleVersion().ToString(),
				".",
				Application.identifier,
				".obb"
			}));
		}

		// Token: 0x060051AE RID: 20910 RVA: 0x0018CB3C File Offset: 0x0018AD3C
		public static string GetAndroidInternalFileFullPath(string relativePath)
		{
			string[] array = new string[]
			{
				"/storage",
				"/sdcard",
				"/storage/emulated/0",
				"/mnt/sdcard",
				"/storage/sdcard0",
				"/storage/sdcard1"
			};
			if (Application.platform == RuntimePlatform.Android)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (Directory.Exists(array[i]))
					{
						string text = array[i] + "/" + relativePath;
						if (File.Exists(text))
						{
							NKCObbUtil.DebugLog("[fullPath:" + text + "] Exist Path!", "GetAndroidInternalFileFullPath");
							return text;
						}
					}
				}
			}
			NKCObbUtil.DebugLog("[relativePath:" + relativePath + "] not found full obb path", "GetAndroidInternalFileFullPath");
			return "";
		}

		// Token: 0x060051AF RID: 20911 RVA: 0x0018CBF3 File Offset: 0x0018ADF3
		private static void DebugLog(string log, [CallerMemberName] string caller = "")
		{
			Log.Debug("[NKCObbUtil][" + caller + "] _ " + log, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Utility/NKCObbUtil.cs", 381);
		}

		// Token: 0x060051B0 RID: 20912 RVA: 0x0018CC15 File Offset: 0x0018AE15
		private static void ErrorLog(string log, [CallerMemberName] string caller = "")
		{
			Log.Error("[NKCObbUtil][" + caller + "] _ " + log, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Utility/NKCObbUtil.cs", 387);
		}

		// Token: 0x04004208 RID: 16904
		public static bool s_bLoadedOBB = false;

		// Token: 0x04004209 RID: 16905
		public static string s_OBBFullPath = "";

		// Token: 0x0400420A RID: 16906
		public const string CS_OBB_FILE = "CS_OBB_FILE/";

		// Token: 0x0400420B RID: 16907
		public const int CS_OBB_FILE_COUNT = 12;

		// Token: 0x0400420C RID: 16908
		private const string _logHeader = "NKCObbUtil";

		// Token: 0x0400420D RID: 16909
		private static List<NKCObbUtil.OBB_Info> s_lst_dicOBB = new List<NKCObbUtil.OBB_Info>();

		// Token: 0x0400420E RID: 16910
		private static int s_OBB_Version = 0;

		// Token: 0x020014C5 RID: 5317
		public class OBB_Info
		{
			// Token: 0x04009F05 RID: 40709
			public Dictionary<string, NKCObbUtil.OBB_EntryInfo> dicObb_EntryInfo = new Dictionary<string, NKCObbUtil.OBB_EntryInfo>();

			// Token: 0x04009F06 RID: 40710
			public List<string> lstFileName = new List<string>();

			// Token: 0x04009F07 RID: 40711
			public List<ulong> lstUncompressedFileSize = new List<ulong>();

			// Token: 0x04009F08 RID: 40712
			public List<ulong> lstLocalOffset = new List<ulong>();

			// Token: 0x04009F09 RID: 40713
			public BinaryReader br;
		}

		// Token: 0x020014C6 RID: 5318
		public class OBB_EntryInfo
		{
			// Token: 0x04009F0A RID: 40714
			public int index;

			// Token: 0x04009F0B RID: 40715
			public ulong size;
		}

		// Token: 0x020014C7 RID: 5319
		public enum OBB_TYPE
		{
			// Token: 0x04009F0D RID: 40717
			main,
			// Token: 0x04009F0E RID: 40718
			patch,
			// Token: 0x04009F0F RID: 40719
			num
		}
	}
}
