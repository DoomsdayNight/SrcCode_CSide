using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using AssetBundles;
using Cs.Logging;

namespace NKC.Patcher
{
	// Token: 0x0200088F RID: 2191
	public class PatchManifestManager
	{
		// Token: 0x06005725 RID: 22309 RVA: 0x001A3288 File Offset: 0x001A1488
		public static NKCPatchInfo LoadManifest(PatchManifestPath.PatchType type)
		{
			return NKCPatchInfo.LoadFromJSON(PatchManifestPath.GetLocalPathBy(type));
		}

		// Token: 0x06005726 RID: 22310 RVA: 0x001A3295 File Offset: 0x001A1495
		public static NKCPatchInfo GetDownloadHistoryPatchInfoFor(bool extra)
		{
			if (!extra)
			{
				return PatchManifestManager.BasePatchInfoController.LoadDownloadHistoryManifest();
			}
			return PatchManifestManager.ExtraPatchInfoController.LoadDownloadHistoryExtraManifest();
		}

		// Token: 0x06005727 RID: 22311 RVA: 0x001A32AF File Offset: 0x001A14AF
		public static NKCPatchInfo GetCurrentPatchInfoFor(bool extra)
		{
			if (!extra)
			{
				return PatchManifestManager.BasePatchInfoController.GetCurPatchInfo();
			}
			return PatchManifestManager.ExtraPatchInfoController.GetCurrentExtraPatchInfo();
		}

		// Token: 0x06005728 RID: 22312 RVA: 0x001A32C9 File Offset: 0x001A14C9
		public static NKCPatchInfo GetLatestPatchInfoFor(bool extra)
		{
			if (!extra)
			{
				return PatchManifestManager.BasePatchInfoController.GetLatestPatchInfo();
			}
			return PatchManifestManager.ExtraPatchInfoController.GetLatestExtraPatchInfo();
		}

		// Token: 0x06005729 RID: 22313 RVA: 0x001A32E3 File Offset: 0x001A14E3
		public static bool IsFileExist(string assetBundleName)
		{
			return File.Exists(AssetBundleManager.GetLocalDownloadPath() + "/" + assetBundleName);
		}

		// Token: 0x0600572A RID: 22314 RVA: 0x001A3300 File Offset: 0x001A1500
		public static void RemoveManifestFile(PatchManifestPath.PatchType type)
		{
			string localPathBy = PatchManifestPath.GetLocalPathBy(type);
			if (localPathBy == string.Empty)
			{
				return;
			}
			if (File.Exists(localPathBy))
			{
				File.Delete(localPathBy);
				return;
			}
			PatchManifestManager.WarnLog("[RemoveManifestFile][Not exist file:" + localPathBy + "]", "RemoveManifestFile", 64);
		}

		// Token: 0x0600572B RID: 22315 RVA: 0x001A3350 File Offset: 0x001A1550
		public static bool CleanUpFiles(bool extra)
		{
			PatchManifestManager.DebugLog("Start", "CleanUpFiles", 73);
			bool flag = false;
			NKCPatchInfo nkcpatchInfo = null;
			if (NKCDefineManager.DEFINE_SEMI_FULL_BUILD())
			{
				nkcpatchInfo = PatchManifestManager.BasePatchInfoController.LoadInnerManifest();
				if (nkcpatchInfo == null)
				{
					Log.Debug("[CleanUpFiles] Not found innerManifest", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/PatchManifestManager.cs", 83);
				}
				flag = true;
			}
			NKCPatchInfo latestPatchInfoFor = PatchManifestManager.GetLatestPatchInfoFor(extra);
			if (latestPatchInfoFor == null)
			{
				PatchManifestManager.DebugLog("latestPatchInfo is null", "CleanUpFiles", 91);
				return false;
			}
			NKCPatchInfo currentPatchInfoFor = PatchManifestManager.GetCurrentPatchInfoFor(extra);
			if (currentPatchInfoFor == null)
			{
				PatchManifestManager.DebugLog("CurrentPatchInfo is null", "CleanUpFiles", 98);
				return false;
			}
			List<string> ignoreList = new List<string>
			{
				"PatchInfo.json",
				"LatestPatchInfo.json",
				"TempPatchInfo.json",
				"LatestExtraPatchInfo.json",
				"ConnectionInfo.json",
				Utility.GetPlatformName()
			};
			NKCPatchUtility.CleanupDirectory(extra ? PatchManifestPath.ExtraLocalDownloadPath : PatchManifestPath.LocalDownloadPath, latestPatchInfoFor, ignoreList, flag ? nkcpatchInfo : null, flag ? currentPatchInfoFor : null);
			return true;
		}

		// Token: 0x0600572C RID: 22316 RVA: 0x001A3443 File Offset: 0x001A1643
		public static IEnumerator DownloadLatestManifest(bool extra)
		{
			if (File.Exists(PatchManifestPath.GetManifestPath(extra)))
			{
				File.Delete(PatchManifestPath.GetManifestPath(extra));
			}
			UnityWebRequestDownloader unityWebRequestDownloader = new UnityWebRequestDownloader(PatchManifestPath.GetServerBasePath(extra), PatchManifestPath.GetLocalDownloadPath(extra));
			UnityWebRequestDownloader unityWebRequestDownloader2 = unityWebRequestDownloader;
			unityWebRequestDownloader2.dOnDownloadCompleted = (NKCFileDownloader.OnDownloadCompleted)Delegate.Combine(unityWebRequestDownloader2.dOnDownloadCompleted, new NKCFileDownloader.OnDownloadCompleted(PatchManifestManager.<DownloadLatestManifest>g__OnComplete|11_0));
			PatchManifestManager.DebugLog(string.Format("[serverDownloadPath:{0}][localDownloadPath:{1}][extra:{2}] Start PatchFile Down ", PatchManifestPath.GetServerBasePath(extra), PatchManifestPath.GetLocalDownloadPath(extra), extra), "DownloadLatestManifest", 141);
			yield return unityWebRequestDownloader.DownloadFile("PatchInfo.json", PatchManifestPath.GetManifestFileName(extra), 0L);
			yield break;
		}

		// Token: 0x0600572D RID: 22317 RVA: 0x001A3452 File Offset: 0x001A1652
		public static IEnumerator GetDownloadList(List<NKCPatchInfo.PatchFileInfo> retVal, bool bCheckPreExistOldPatchInfo, NKCPatchInfo currentManifest, NKCPatchInfo latestManifest, string downloadBasePath, bool fullIntegrityCheck = false, NKCPatchDownloader.OnIntegrityCheckProgress onIntegrityProgress = null)
		{
			retVal.Clear();
			if (latestManifest == null)
			{
				PatchManifestManager.ErrorLog("NewManifest Null!", "GetDownloadList", 161);
				yield break;
			}
			if (currentManifest == null)
			{
				PatchManifestManager.ErrorLog("All new Download", "GetDownloadList", 168);
				retVal.AddRange(latestManifest.m_dicPatchInfo.Values);
				yield break;
			}
			if (fullIntegrityCheck)
			{
				PatchManifestManager.DebugLog("Doing full integrity check", "GetDownloadList", 176);
			}
			else
			{
				PatchManifestManager.DebugLog("Integrity check skip", "GetDownloadList", 180);
			}
			bool bSkipNotExistCheck = NKCDefineManager.DEFINE_LB();
			if (latestManifest.m_dicPatchInfo == null)
			{
				PatchManifestManager.ErrorLog("latestManifest patchInfo dic is null!!", "GetDownloadList", 187);
				yield break;
			}
			int totalCount = latestManifest.m_dicPatchInfo.Count;
			int currentCount = 0;
			foreach (KeyValuePair<string, NKCPatchInfo.PatchFileInfo> kvPair in latestManifest.m_dicPatchInfo)
			{
				if (fullIntegrityCheck)
				{
					int num = currentCount;
					currentCount = num + 1;
					if (currentCount % 10 == 0)
					{
						if (onIntegrityProgress != null)
						{
							onIntegrityProgress(currentCount, totalCount);
						}
						yield return null;
					}
				}
				NKCPatchInfo.PatchFileInfo value = kvPair.Value;
				NKCPatchInfo.PatchFileInfo patchInfo = currentManifest.GetPatchInfo(kvPair.Key);
				if (value == null)
				{
					PatchManifestManager.ErrorLog("NewInfo is null!!", "GetDownloadList", 209);
				}
				else if (patchInfo == null)
				{
					if (NKCDefineManager.DEFINE_SEMI_FULL_BUILD())
					{
						PatchManifestManager.ProcessInnerAsset(retVal, value, fullIntegrityCheck);
					}
					else
					{
						PatchManifestManager.AddTo(retVal, value);
						PatchLogContainer.AddToLog("Not exist in currentManifest", value, value.FileName);
					}
				}
				else
				{
					string path = Path.Combine(downloadBasePath, value.FileName);
					if (patchInfo.FileUpdated(value))
					{
						if (NKCDefineManager.DEFINE_SEMI_FULL_BUILD())
						{
							PatchManifestManager.ProcessInnerAsset(retVal, value, fullIntegrityCheck);
						}
						else
						{
							PatchManifestManager.AddTo(retVal, value);
							PatchLogContainer.AddToLog("Is old file", value, path);
						}
					}
					else
					{
						if (fullIntegrityCheck || !bSkipNotExistCheck)
						{
							if (!NKCPatchUtility.IsFileExists(path))
							{
								PatchManifestManager.ProcessInnerAsset(retVal, value, fullIntegrityCheck);
								continue;
							}
							if (!NKCPatchUtility.CheckSize(path, value.Size))
							{
								PatchManifestManager.AddTo(retVal, value);
								PatchLogContainer.AddToLog("Different size", value, path);
								continue;
							}
						}
						if (fullIntegrityCheck && !NKCPatchUtility.CheckIntegrity(path, value.Hash))
						{
							PatchManifestManager.AddTo(retVal, value);
							PatchLogContainer.AddToLog("Check integrity", value, path);
						}
						else
						{
							kvPair = default(KeyValuePair<string, NKCPatchInfo.PatchFileInfo>);
						}
					}
				}
			}
			Dictionary<string, NKCPatchInfo.PatchFileInfo>.Enumerator enumerator = default(Dictionary<string, NKCPatchInfo.PatchFileInfo>.Enumerator);
			PatchLogContainer.DownloadListLogOutPut();
			yield break;
			yield break;
		}

		// Token: 0x0600572E RID: 22318 RVA: 0x001A3487 File Offset: 0x001A1687
		private static void AddTo(List<NKCPatchInfo.PatchFileInfo> retVal, NKCPatchInfo.PatchFileInfo newInfo)
		{
			if (retVal.Contains(newInfo))
			{
				return;
			}
			retVal.Add(newInfo);
		}

		// Token: 0x0600572F RID: 22319 RVA: 0x001A349C File Offset: 0x001A169C
		private static void ProcessInnerAsset(List<NKCPatchInfo.PatchFileInfo> retVal, NKCPatchInfo.PatchFileInfo newInfo, bool FullIntegrityCheck)
		{
			string innerAssetPath = NKCPatchUtility.GetInnerAssetPath(newInfo.FileName, false);
			if (!NKCPatchUtility.IsFileExists(innerAssetPath))
			{
				PatchManifestManager.AddTo(retVal, newInfo);
				PatchLogContainer.AddToLog("[Inner] Not exist", newInfo, innerAssetPath);
				return;
			}
			if (!NKCPatchUtility.CheckSize(innerAssetPath, newInfo.Size))
			{
				PatchManifestManager.AddTo(retVal, newInfo);
				PatchLogContainer.AddToLog("[Inner] Deferent size", newInfo, innerAssetPath);
				return;
			}
			if (FullIntegrityCheck && !NKCPatchUtility.CheckIntegrity(innerAssetPath, newInfo.Hash))
			{
				PatchManifestManager.AddTo(retVal, newInfo);
				PatchLogContainer.AddToLog("[Inner] Check integrity", newInfo, innerAssetPath);
			}
		}

		// Token: 0x06005730 RID: 22320 RVA: 0x001A3518 File Offset: 0x001A1718
		private static void DebugLog(string log, [CallerMemberName] string caller = "", [CallerLineNumber] int l = 0)
		{
			Log.Debug(string.Concat(new string[]
			{
				"[",
				PatchManifestManager._logHeader,
				"][",
				caller,
				"] ",
				log
			}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/PatchManifestManager.cs", l);
		}

		// Token: 0x06005731 RID: 22321 RVA: 0x001A3558 File Offset: 0x001A1758
		private static void WarnLog(string log, [CallerMemberName] string caller = "", [CallerLineNumber] int l = 0)
		{
			Log.Warn(string.Concat(new string[]
			{
				"[",
				PatchManifestManager._logHeader,
				"][",
				caller,
				"] ",
				log
			}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/PatchManifestManager.cs", l);
		}

		// Token: 0x06005732 RID: 22322 RVA: 0x001A3598 File Offset: 0x001A1798
		private static void ErrorLog(string log, [CallerMemberName] string caller = "", [CallerLineNumber] int l = 0)
		{
			Log.Error(string.Concat(new string[]
			{
				"[",
				PatchManifestManager._logHeader,
				"][",
				caller,
				"] ",
				log
			}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/PatchManifestManager.cs", l);
		}

		// Token: 0x06005735 RID: 22325 RVA: 0x001A360A File Offset: 0x001A180A
		[CompilerGenerated]
		internal static void <DownloadLatestManifest>g__OnComplete|11_0(bool success)
		{
			PatchManifestManager.SuccessLatestManifest = success;
		}

		// Token: 0x04004518 RID: 17688
		public static readonly BasePatchInfoController BasePatchInfoController = new BasePatchInfoController();

		// Token: 0x04004519 RID: 17689
		public static readonly ExtraPatchInfoController ExtraPatchInfoController = new ExtraPatchInfoController();

		// Token: 0x0400451A RID: 17690
		public static readonly OptimizationPatchInfoController OptimizationPatchInfoController = new OptimizationPatchInfoController();

		// Token: 0x0400451B RID: 17691
		public static bool SuccessLatestManifest;

		// Token: 0x0400451C RID: 17692
		private static string _logHeader = "PatchManifestManager";
	}
}
