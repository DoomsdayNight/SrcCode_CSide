using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using AssetBundles;
using Cs.Logging;

namespace NKC.Patcher
{
	// Token: 0x0200088C RID: 2188
	public static class NKCPatchManifestManager
	{
		// Token: 0x06005712 RID: 22290 RVA: 0x001A3100 File Offset: 0x001A1300
		public static IEnumerator MakeDownloadListForTutorialAsset()
		{
			yield return null;
			yield break;
		}

		// Token: 0x06005713 RID: 22291 RVA: 0x001A3108 File Offset: 0x001A1308
		public static string GetLatestManifestPath()
		{
			return Path.Combine(AssetBundleManager.GetLocalDownloadPath(), "LatestPatchInfo.json");
		}

		// Token: 0x06005714 RID: 22292 RVA: 0x001A311C File Offset: 0x001A131C
		public static string GetCurrentManifestPath()
		{
			string text = Path.Combine(AssetBundleManager.GetLocalDownloadPath(), "PatchInfo.json");
			if (NKCPatchUtility.IsFileExists(text))
			{
				return text;
			}
			string innerAssetPath = NKCPatchUtility.GetInnerAssetPath("PatchInfo.json", false);
			if (NKCPatchUtility.IsFileExists(innerAssetPath))
			{
				return innerAssetPath;
			}
			return text;
		}

		// Token: 0x06005715 RID: 22293 RVA: 0x001A315A File Offset: 0x001A135A
		public static string GetInnerManifestPath()
		{
			return NKCPatchUtility.GetInnerAssetPath("PatchInfo.json", false);
		}

		// Token: 0x06005716 RID: 22294 RVA: 0x001A3167 File Offset: 0x001A1367
		public static string GetTempManifestPath()
		{
			return Path.Combine(AssetBundleManager.GetLocalDownloadPath(), "TempPatchInfo.json");
		}

		// Token: 0x06005717 RID: 22295 RVA: 0x001A3178 File Offset: 0x001A1378
		public static string GetNonEssentialManifestPath()
		{
			return Path.Combine(AssetBundleManager.GetLocalDownloadPath(), "NonEssentialTempPatchInfo.json");
		}

		// Token: 0x06005718 RID: 22296 RVA: 0x001A3189 File Offset: 0x001A1389
		public static string GetCurrentExtraAssetManifestPath()
		{
			return Path.Combine(NKCUtil.GetExtraDownloadPath(), "PatchInfo.json");
		}

		// Token: 0x06005719 RID: 22297 RVA: 0x001A319A File Offset: 0x001A139A
		public static string GetLatestExtraAssetManifestPath()
		{
			return Path.Combine(NKCUtil.GetExtraDownloadPath(), "LatestExtraPatchInfo.json");
		}

		// Token: 0x0600571A RID: 22298 RVA: 0x001A31AB File Offset: 0x001A13AB
		public static string GetDownloadedExtraAssetManifestPath()
		{
			return Path.Combine(NKCUtil.GetExtraDownloadPath(), "TempPatchInfo.json");
		}

		// Token: 0x0600571B RID: 22299 RVA: 0x001A31BC File Offset: 0x001A13BC
		public static int GetBackgroundRequiredDownloadCount()
		{
			if (NKCPatchManifestManager.m_requiredBackgroundDownloadList == null)
			{
				return 0;
			}
			return NKCPatchManifestManager.m_requiredBackgroundDownloadList.Count;
		}

		// Token: 0x0600571C RID: 22300 RVA: 0x001A31D1 File Offset: 0x001A13D1
		public static IEnumerator MakeDownloadListForExtraAsset()
		{
			Log.Debug("[MakeDownloadListForExtraAsset] Start", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/PatchManager/NKCPatchManifestManager.cs", 110);
			NKCPatchManifestManager.m_latestExtraAssetManifest = NKCPatchInfo.LoadFromJSON(NKCPatchManifestManager.GetLatestExtraAssetManifestPath());
			NKCPatchManifestManager.m_currentExtraAssetManifest = NKCPatchInfo.LoadFromJSON(NKCPatchManifestManager.GetCurrentExtraAssetManifestPath());
			NKCPatchManifestManager.m_prevDownloadedExtraAssetManifest = NKCPatchInfo.LoadFromJSON(NKCPatchManifestManager.GetDownloadedExtraAssetManifestPath());
			if (!NKCPatchManifestManager.m_prevDownloadedExtraAssetManifest.IsEmpty())
			{
				NKCPatchManifestManager.m_currentExtraAssetManifest.Append(NKCPatchManifestManager.m_prevDownloadedExtraAssetManifest);
			}
			bool calculateMD = false;
			foreach (KeyValuePair<string, NKCPatchInfo.PatchFileInfo> keyValuePair in NKCPatchManifestManager.m_latestExtraAssetManifest.m_dicPatchInfo)
			{
				NKCPatchInfo.PatchFileInfo value = keyValuePair.Value;
				NKCPatchInfo.PatchFileInfo patchInfo = NKCPatchManifestManager.m_currentExtraAssetManifest.GetPatchInfo(keyValuePair.Key);
				if (value == null)
				{
					Log.Error("[MakeDownloadList] Invalid Latest PatchInfo key[" + keyValuePair.Key + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/PatchManager/NKCPatchManifestManager.cs", 137);
				}
				else
				{
					string filePath = Path.Combine(NKCUtil.GetExtraDownloadPath(), value.FileName);
					if (patchInfo != null && patchInfo.FileUpdated(value) && !NKCPatchManifestManager.CompareFileInInPath(patchInfo, filePath, calculateMD))
					{
						NKCPatchManifestManager.AddRequiredDownloadList(NKCPatchManifestManager.m_requiredExtraAssetDownloadList, value);
					}
				}
			}
			yield return null;
			yield break;
		}

		// Token: 0x0600571D RID: 22301 RVA: 0x001A31D9 File Offset: 0x001A13D9
		public static IEnumerator MakeDownloadList()
		{
			Log.Debug("[MakeDownloadList] Start", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/PatchManager/NKCPatchManifestManager.cs", 162);
			List<string> lstVariants = new List<string>(AssetBundleManager.ActiveVariants);
			NKCPatchManifestManager.m_latestManifest = NKCPatchInfo.LoadFromJSON(NKCPatchManifestManager.GetLatestManifestPath());
			NKCPatchManifestManager.m_currentManifest = NKCPatchInfo.LoadFromJSON(NKCPatchManifestManager.GetCurrentManifestPath());
			NKCPatchManifestManager.m_prevDownloadedManifest = NKCPatchInfo.LoadFromJSON(NKCPatchManifestManager.GetTempManifestPath());
			NKCPatchManifestManager.m_filteredLatestManifest = NKCPatchManifestManager.m_latestManifest.FilterByVariants(lstVariants);
			if (!NKCPatchManifestManager.m_prevDownloadedManifest.IsEmpty())
			{
				NKCPatchManifestManager.m_currentManifest.Append(NKCPatchManifestManager.m_prevDownloadedManifest);
			}
			NKCPatchManifestManager.m_currentManifest.IsEmpty();
			if (NKCDefineManager.DEFINE_OBB())
			{
				bool s_bLoadedOBB = NKCObbUtil.s_bLoadedOBB;
			}
			bool bFullIntegrityCheck = false;
			int count = NKCPatchManifestManager.m_filteredLatestManifest.m_dicPatchInfo.Count;
			int currentCount = 0;
			foreach (KeyValuePair<string, NKCPatchInfo.PatchFileInfo> kvPair in NKCPatchManifestManager.m_filteredLatestManifest.m_dicPatchInfo)
			{
				int num = currentCount;
				currentCount = num + 1;
				if (bFullIntegrityCheck)
				{
					yield return null;
				}
				NKCPatchInfo.PatchFileInfo value = kvPair.Value;
				NKCPatchInfo.PatchFileInfo patchInfo = NKCPatchManifestManager.m_currentManifest.GetPatchInfo(kvPair.Key);
				if (value == null)
				{
					Log.Error("[MakeDownloadList] Invalid Latest PatchInfo key[" + kvPair.Key + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/PatchManager/NKCPatchManifestManager.cs", 212);
				}
				else
				{
					string innerAssetPath = NKCPatchUtility.GetInnerAssetPath(value.FileName, false);
					string filePath = Path.Combine(AssetBundleManager.GetLocalDownloadPath(), value.FileName);
					if (patchInfo != null && patchInfo.FileUpdated(value))
					{
						if (!NKCPatchManifestManager.CompareFileInInPath(patchInfo, filePath, bFullIntegrityCheck))
						{
							NKCPatchManifestManager.AddRequiredDownloadList(NKCPatchManifestManager.m_requiredDownloadList, value);
						}
					}
					else if (!NKCPatchManifestManager.CompareFileInInPath(patchInfo, innerAssetPath, bFullIntegrityCheck))
					{
						NKCPatchManifestManager.AddRequiredDownloadList(NKCPatchManifestManager.m_requiredDownloadList, value);
					}
					else
					{
						kvPair = default(KeyValuePair<string, NKCPatchInfo.PatchFileInfo>);
					}
				}
			}
			Dictionary<string, NKCPatchInfo.PatchFileInfo>.Enumerator enumerator = default(Dictionary<string, NKCPatchInfo.PatchFileInfo>.Enumerator);
			yield return null;
			yield break;
			yield break;
		}

		// Token: 0x0600571E RID: 22302 RVA: 0x001A31E1 File Offset: 0x001A13E1
		private static bool CompareFileInInPath(NKCPatchInfo.PatchFileInfo patchFileInfo, string filePath, bool calculateMD5)
		{
			if (!NKCPatchUtility.IsFileExists(filePath))
			{
				return false;
			}
			if (patchFileInfo == null)
			{
				return false;
			}
			if (calculateMD5)
			{
				return NKCPatchUtility.CheckIntegrity(filePath, patchFileInfo.Hash);
			}
			return NKCPatchUtility.CheckSize(filePath, patchFileInfo.Size);
		}

		// Token: 0x0600571F RID: 22303 RVA: 0x001A3213 File Offset: 0x001A1413
		private static void AddRequiredDownloadList(List<NKCPatchInfo.PatchFileInfo> retVal, NKCPatchInfo.PatchFileInfo newInfo)
		{
			if (retVal.Contains(newInfo))
			{
				Log.Debug("[" + newInfo.FileName + "] already exist", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/PatchManager/NKCPatchManifestManager.cs", 275);
				return;
			}
			retVal.Add(newInfo);
		}

		// Token: 0x04004507 RID: 17671
		public const string manifestFileName = "PatchInfo.json";

		// Token: 0x04004508 RID: 17672
		public const string LatestManifestFileName = "LatestPatchInfo.json";

		// Token: 0x04004509 RID: 17673
		public const string LatestExtraManifestFileName = "LatestExtraPatchInfo.json";

		// Token: 0x0400450A RID: 17674
		public const string tempManifestFileName = "TempPatchInfo.json";

		// Token: 0x0400450B RID: 17675
		public const string filteredManifestFileName = "FilteredPatchInfo.json";

		// Token: 0x0400450C RID: 17676
		public const string NonEssentialTempManifestFileName = "NonEssentialTempPatchInfo.json";

		// Token: 0x0400450D RID: 17677
		public static NKCPatchInfo m_latestManifest;

		// Token: 0x0400450E RID: 17678
		public static NKCPatchInfo m_currentManifest;

		// Token: 0x0400450F RID: 17679
		public static NKCPatchInfo m_prevDownloadedManifest;

		// Token: 0x04004510 RID: 17680
		public static NKCPatchInfo m_filteredLatestManifest;

		// Token: 0x04004511 RID: 17681
		public static NKCPatchInfo m_latestExtraAssetManifest;

		// Token: 0x04004512 RID: 17682
		public static NKCPatchInfo m_currentExtraAssetManifest;

		// Token: 0x04004513 RID: 17683
		public static NKCPatchInfo m_prevDownloadedExtraAssetManifest;

		// Token: 0x04004514 RID: 17684
		public static List<NKCPatchInfo.PatchFileInfo> m_requiredDownloadList = new List<NKCPatchInfo.PatchFileInfo>();

		// Token: 0x04004515 RID: 17685
		public static List<NKCPatchInfo.PatchFileInfo> m_requiredExtraAssetDownloadList = new List<NKCPatchInfo.PatchFileInfo>();

		// Token: 0x04004516 RID: 17686
		public static List<NKCPatchInfo.PatchFileInfo> m_requiredForegroundDownloadList = new List<NKCPatchInfo.PatchFileInfo>();

		// Token: 0x04004517 RID: 17687
		public static List<NKCPatchInfo.PatchFileInfo> m_requiredBackgroundDownloadList = new List<NKCPatchInfo.PatchFileInfo>();
	}
}
