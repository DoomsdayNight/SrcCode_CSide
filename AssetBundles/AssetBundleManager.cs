using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using NKC;
using NKC.Localization;
using NKC.Patcher;
using SimpleJSON;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AssetBundles
{
	// Token: 0x02000057 RID: 87
	public class AssetBundleManager : MonoBehaviour
	{
		// Token: 0x0600027F RID: 639 RVA: 0x0000AA9C File Offset: 0x00008C9C
		public static ulong[] GetMaskList(string filePath, bool bWriteLog = false)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath.ToLower());
			string text = NKCPatchUtility.CalculateMD5(Encoding.UTF8.GetBytes(fileNameWithoutExtension.ToCharArray()));
			text.GetHashCode();
			string s = text.Substring(0, 16);
			string s2 = text.Substring(16, 16);
			string s3 = text.Substring(0, 8) + text.Substring(16, 8);
			string s4 = text.Substring(8, 8) + text.Substring(24, 8);
			ulong item = ulong.Parse(s, NumberStyles.HexNumber);
			ulong item2 = ulong.Parse(s2, NumberStyles.HexNumber);
			ulong item3 = ulong.Parse(s3, NumberStyles.HexNumber);
			ulong item4 = ulong.Parse(s4, NumberStyles.HexNumber);
			if (bWriteLog)
			{
				Debug.Log(string.Concat(new string[]
				{
					"[ENCRYPT_TEST][",
					fileNameWithoutExtension,
					"] mask1[",
					item.ToString(),
					"] mask2[",
					item2.ToString(),
					"] mask3[",
					item3.ToString(),
					"] mask4[",
					item4.ToString(),
					"]"
				}));
			}
			AssetBundleManager.maskList.Clear();
			AssetBundleManager.maskList.Add(item);
			AssetBundleManager.maskList.Add(item2);
			AssetBundleManager.maskList.Add(item3);
			AssetBundleManager.maskList.Add(item4);
			return AssetBundleManager.maskList.ToArray();
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000AC01 File Offset: 0x00008E01
		// (set) Token: 0x06000281 RID: 641 RVA: 0x0000AC08 File Offset: 0x00008E08
		public static AssetBundleManager.LogMode logMode
		{
			get
			{
				return AssetBundleManager.m_LogMode;
			}
			set
			{
				AssetBundleManager.m_LogMode = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000AC10 File Offset: 0x00008E10
		// (set) Token: 0x06000283 RID: 643 RVA: 0x0000AC17 File Offset: 0x00008E17
		public static string BaseDownloadingURL
		{
			get
			{
				return AssetBundleManager.m_BaseDownloadingURL;
			}
			set
			{
				AssetBundleManager.m_BaseDownloadingURL = value;
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000284 RID: 644 RVA: 0x0000AC20 File Offset: 0x00008E20
		// (remove) Token: 0x06000285 RID: 645 RVA: 0x0000AC54 File Offset: 0x00008E54
		public static event AssetBundleManager.OverrideBaseDownloadingURLDelegate overrideBaseDownloadingURL;

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000AC87 File Offset: 0x00008E87
		// (set) Token: 0x06000287 RID: 647 RVA: 0x0000AC8E File Offset: 0x00008E8E
		public static string[] ActiveVariants
		{
			get
			{
				return AssetBundleManager.m_ActiveVariants;
			}
			set
			{
				AssetBundleManager.m_ActiveVariants = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (set) Token: 0x06000288 RID: 648 RVA: 0x0000AC96 File Offset: 0x00008E96
		public static AssetBundleManifest AssetBundleManifestObject
		{
			set
			{
				AssetBundleManager.m_AssetBundleManifest = value;
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000AC9E File Offset: 0x00008E9E
		public static bool IsAssetBundleManifestLoaded()
		{
			return AssetBundleManager.m_AssetBundleManifest != null;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000ACAB File Offset: 0x00008EAB
		private static void Log(AssetBundleManager.LogType logType, string text)
		{
			if (logType == AssetBundleManager.LogType.Error)
			{
				Debug.LogError("[AssetBundleManager] " + text);
				return;
			}
			if (AssetBundleManager.m_LogMode == AssetBundleManager.LogMode.All && logType == AssetBundleManager.LogType.Warning)
			{
				Debug.LogWarning("[AssetBundleManager] " + text);
				return;
			}
			AssetBundleManager.LogMode logMode = AssetBundleManager.m_LogMode;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000ACE4 File Offset: 0x00008EE4
		public static string GetLocalDownloadPath()
		{
			if (Application.isEditor)
			{
				return Environment.CurrentDirectory.Replace("\\", "/") + "/AssetBundles/" + Utility.GetPlatformName();
			}
			if (NKCDefineManager.DEFINE_PC_EXTRA_DOWNLOAD_IN_EXE_FOLDER())
			{
				return Application.dataPath + "/../Assetbundles/";
			}
			return Application.persistentDataPath + "/Assetbundles/";
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000AD42 File Offset: 0x00008F42
		public static void SetSourceAssetBundleDirectory(string relativePath)
		{
			AssetBundleManager.BaseDownloadingURL = AssetBundleManager.GetLocalDownloadPath() + relativePath;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000AD54 File Offset: 0x00008F54
		public static LoadedAssetBundle GetLoadedAssetBundle(string assetBundleName, out string error)
		{
			if (AssetBundleManager.m_DownloadingErrors.TryGetValue(assetBundleName, out error))
			{
				return null;
			}
			LoadedAssetBundle loadedAssetBundle = null;
			AssetBundleManager.m_LoadedAssetBundles.TryGetValue(assetBundleName, out loadedAssetBundle);
			if (loadedAssetBundle == null)
			{
				return null;
			}
			string[] array = null;
			if (!AssetBundleManager.m_Dependencies.TryGetValue(assetBundleName, out array))
			{
				return loadedAssetBundle;
			}
			foreach (string text in array)
			{
				if (!string.IsNullOrEmpty(text))
				{
					if (AssetBundleManager.m_DownloadingErrors.TryGetValue(text, out error))
					{
						return null;
					}
					LoadedAssetBundle loadedAssetBundle2;
					AssetBundleManager.m_LoadedAssetBundles.TryGetValue(text, out loadedAssetBundle2);
					if (loadedAssetBundle2 == null)
					{
						return null;
					}
				}
			}
			return loadedAssetBundle;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000ADDE File Offset: 0x00008FDE
		public static bool IsAssetBundleDownloaded(string assetBundleName)
		{
			return AssetBundleManager.m_LoadedAssetBundles.ContainsKey(assetBundleName);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000ADEB File Offset: 0x00008FEB
		public static AssetBundleLoadManifestOperation Initialize()
		{
			AssetBundleManager.SetSourceAssetBundleDirectory("");
			return AssetBundleManager.Initialize(Utility.GetPlatformName());
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000AE04 File Offset: 0x00009004
		public static AssetBundleLoadManifestOperation Initialize(string manifestAssetBundleName)
		{
			AssetBundleManager.m_dicVariants = null;
			if (AssetBundleManager.s_objAssetBundleManager == null)
			{
				AssetBundleManager.s_objAssetBundleManager = new GameObject("AssetBundleManager", new Type[]
				{
					typeof(AssetBundleManager)
				});
				UnityEngine.Object.DontDestroyOnLoad(AssetBundleManager.s_objAssetBundleManager);
			}
			AssetBundleManager.LoadAssetBundle(manifestAssetBundleName, false, true);
			AssetBundleLoadManifestOperation assetBundleLoadManifestOperation = new AssetBundleLoadManifestOperation(manifestAssetBundleName, "AssetBundleManifest", typeof(AssetBundleManifest));
			AssetBundleManager.m_InProgressOperations.Add(assetBundleLoadManifestOperation);
			return assetBundleLoadManifestOperation;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000AE7A File Offset: 0x0000907A
		public static void LoadAssetBundle(string assetBundleName, bool async)
		{
			assetBundleName = AssetBundleManager.RemapVariantName(assetBundleName);
			AssetBundleManager.LoadAssetBundle(assetBundleName, async, false);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000AE8C File Offset: 0x0000908C
		protected static void LoadAssetBundle(string assetBundleName, bool async, bool isLoadingAssetBundleManifest)
		{
			AssetBundleManager.Log(AssetBundleManager.LogType.Info, "Loading Asset Bundle " + (isLoadingAssetBundleManifest ? "Manifest: " : ": ") + assetBundleName);
			if (!isLoadingAssetBundleManifest && AssetBundleManager.m_AssetBundleManifest == null)
			{
				AssetBundleManager.Log(AssetBundleManager.LogType.Error, "Please initialize AssetBundleManifest by calling AssetBundleManager.Initialize()");
				return;
			}
			bool flag;
			if (async)
			{
				flag = AssetBundleManager.LoadAssetBundleInternalAsync(assetBundleName);
			}
			else
			{
				flag = AssetBundleManager.LoadAssetBundleInternal(assetBundleName);
			}
			if (!flag && !isLoadingAssetBundleManifest)
			{
				AssetBundleManager.LoadDependencies(assetBundleName, async);
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000AEF8 File Offset: 0x000090F8
		protected static string GetAssetBundleBaseDownloadingURL(string bundleName)
		{
			if (AssetBundleManager.overrideBaseDownloadingURL != null)
			{
				Delegate[] invocationList = AssetBundleManager.overrideBaseDownloadingURL.GetInvocationList();
				for (int i = 0; i < invocationList.Length; i++)
				{
					string text = ((AssetBundleManager.OverrideBaseDownloadingURLDelegate)invocationList[i])(bundleName);
					if (text != null)
					{
						return text;
					}
				}
			}
			return AssetBundleManager.m_BaseDownloadingURL;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000AF3E File Offset: 0x0000913E
		protected static bool UsesExternalBundleVariantResolutionMechanism(string baseAssetBundleName)
		{
			return false;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000AF44 File Offset: 0x00009144
		private static Dictionary<string, HashSet<string>> dicVariants
		{
			get
			{
				if (AssetBundleManager.m_dicVariants == null)
				{
					AssetBundleManager.m_bundleVariantCache.Clear();
					AssetBundleManager.m_bundlePathCache.Clear();
					AssetBundleManager.m_dicVariants = new Dictionary<string, HashSet<string>>();
					string[] allAssetBundlesWithVariant = AssetBundleManager.m_AssetBundleManifest.GetAllAssetBundlesWithVariant();
					for (int i = 0; i < allAssetBundlesWithVariant.Length; i++)
					{
						string[] array = allAssetBundlesWithVariant[i].Split(new char[]
						{
							'.'
						});
						string key = array[0];
						string item = array[1];
						if (!AssetBundleManager.m_dicVariants.ContainsKey(key))
						{
							AssetBundleManager.m_dicVariants[key] = new HashSet<string>();
						}
						AssetBundleManager.m_dicVariants[key].Add(item);
					}
				}
				return AssetBundleManager.m_dicVariants;
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000AFE0 File Offset: 0x000091E0
		public static string RemapVariantName(string assetBundleName)
		{
			string result;
			if (AssetBundleManager.m_bundleVariantCache.TryGetValue(assetBundleName, out result))
			{
				return result;
			}
			if (AssetBundleManager.m_AssetBundleManifest == null)
			{
				return assetBundleName;
			}
			assetBundleName = assetBundleName.ToLower();
			string text = assetBundleName.Split(new char[]
			{
				'.'
			})[0];
			if (AssetBundleManager.UsesExternalBundleVariantResolutionMechanism(text))
			{
				AssetBundleManager.m_bundleVariantCache[assetBundleName] = text;
				return text;
			}
			HashSet<string> hashSet;
			if (AssetBundleManager.dicVariants.TryGetValue(text, out hashSet))
			{
				for (int i = 0; i < AssetBundleManager.m_ActiveVariants.Length; i++)
				{
					if (hashSet.Contains(AssetBundleManager.m_ActiveVariants[i]))
					{
						string text2 = text + "." + AssetBundleManager.m_ActiveVariants[i];
						AssetBundleManager.m_bundleVariantCache[assetBundleName] = text2;
						return text2;
					}
				}
				if (hashSet.Contains("asset"))
				{
					string text3 = text + ".asset";
					AssetBundleManager.m_bundleVariantCache[assetBundleName] = text3;
					AssetBundleManager.Log(AssetBundleManager.LogType.Warning, "default asset bundle variant chosen because there was no matching active variant: " + text3);
					return text3;
				}
			}
			AssetBundleManager.m_bundleVariantCache[assetBundleName] = assetBundleName;
			return assetBundleName;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000B0E0 File Offset: 0x000092E0
		protected static bool LoadAssetBundleInternalAsync(string assetBundleName)
		{
			LoadedAssetBundle loadedAssetBundle = null;
			AssetBundleManager.m_LoadedAssetBundles.TryGetValue(assetBundleName, out loadedAssetBundle);
			if (loadedAssetBundle != null)
			{
				loadedAssetBundle.m_ReferencedCount++;
				return true;
			}
			if (AssetBundleManager.m_DownloadingBundles.ContainsKey(assetBundleName))
			{
				AssetBundleManager.m_DownloadingBundles[assetBundleName] = AssetBundleManager.m_DownloadingBundles[assetBundleName] + 1;
				return true;
			}
			string bundleFilePath = AssetBundleManager.GetBundleFilePath(assetBundleName);
			if (string.IsNullOrWhiteSpace(bundleFilePath))
			{
				Debug.LogWarning("path is invalid, assetBundleName : " + assetBundleName);
			}
			AssetBundleManager.AddToTotalLoadedAssetBundle(assetBundleName, bundleFilePath);
			try
			{
				AssetBundleManager.m_InProgressOperations.Add(new AssetBundleLoadFromFileOperation(assetBundleName, bundleFilePath));
			}
			catch
			{
				throw new InvalidOperationException("Could not load asset : " + assetBundleName);
			}
			AssetBundleManager.m_DownloadingBundles.Add(assetBundleName, 1);
			return false;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000B1A0 File Offset: 0x000093A0
		protected static void AddToTotalLoadedAssetBundle(string assetBundleName, string assetBundlePath)
		{
			if (!NKCDefineManager.DEFINE_ZLONG_CHN() || !NKCDefineManager.DEFINE_USE_CHEAT())
			{
				return;
			}
			if (AssetBundleManager.m_TotalLoadedAssetBundleNames.Contains(assetBundleName))
			{
				return;
			}
			Debug.Log("[ASSETBUNDLE_LOAD] NAME " + assetBundleName);
			Debug.Log("[ASSETBUNDLE_LOAD] PATH " + assetBundlePath);
			AssetBundleManager.m_TotalLoadedAssetBundleNames.Add(assetBundleName);
			if (!AssetBundleManager.m_TotalLoadedAssetBundlePath.Contains(assetBundlePath))
			{
				AssetBundleManager.m_TotalLoadedAssetBundlePath.Add(assetBundlePath);
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000B210 File Offset: 0x00009410
		protected static bool LoadAssetBundleInternal(string assetBundleName)
		{
			LoadedAssetBundle loadedAssetBundle = null;
			AssetBundleManager.m_LoadedAssetBundles.TryGetValue(assetBundleName, out loadedAssetBundle);
			if (loadedAssetBundle != null)
			{
				loadedAssetBundle.m_ReferencedCount++;
				return true;
			}
			int referencedCount = 1;
			if (AssetBundleManager.m_DownloadingBundles.ContainsKey(assetBundleName))
			{
				referencedCount = AssetBundleManager.m_DownloadingBundles[assetBundleName] + 1;
				foreach (AssetBundleLoadOperation assetBundleLoadOperation in AssetBundleManager.m_InProgressOperations)
				{
					AssetBundleDownloadOperation assetBundleDownloadOperation = assetBundleLoadOperation as AssetBundleDownloadOperation;
					if (assetBundleDownloadOperation != null && assetBundleDownloadOperation.assetBundleName == assetBundleName)
					{
						Debug.LogWarning("[LoadAssetBundleInternal][" + assetBundleDownloadOperation.assetBundleName + "] ForceFlush");
						assetBundleDownloadOperation.bForceFlush = true;
						break;
					}
				}
			}
			string text;
			if (AssetBundleManager.m_DownloadingErrors.TryGetValue(assetBundleName, out text))
			{
				AssetBundleManager.Log(AssetBundleManager.LogType.Error, text);
				return true;
			}
			if (!AssetBundleManager.IsBundleExists(assetBundleName))
			{
				AssetBundleManager.m_DownloadingErrors.Add(assetBundleName, assetBundleName + " Bundle Not Exists");
				AssetBundleManager.Log(AssetBundleManager.LogType.Error, "assetbundle " + assetBundleName + " does not exists!");
				return true;
			}
			string bundleFilePath = AssetBundleManager.GetBundleFilePath(assetBundleName);
			Stream stream = AssetBundleManager.OpenCryptoBundleFileStream(bundleFilePath);
			loadedAssetBundle = new LoadedAssetBundle(AssetBundle.LoadFromStream(stream), stream);
			loadedAssetBundle.m_ReferencedCount = referencedCount;
			AssetBundleManager.m_LoadedAssetBundles.Add(assetBundleName, loadedAssetBundle);
			AssetBundleManager.AddToTotalLoadedAssetBundle(assetBundleName, bundleFilePath);
			return false;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000B364 File Offset: 0x00009564
		protected static void LoadDependencies(string assetBundleName, bool async)
		{
			if (AssetBundleManager.m_AssetBundleManifest == null)
			{
				AssetBundleManager.Log(AssetBundleManager.LogType.Error, "Please initialize AssetBundleManifest by calling AssetBundleManager.Initialize()");
				return;
			}
			string[] allDependencies = AssetBundleManager.m_AssetBundleManifest.GetAllDependencies(assetBundleName);
			if (allDependencies.Length == 0)
			{
				return;
			}
			for (int i = 0; i < allDependencies.Length; i++)
			{
				allDependencies[i] = AssetBundleManager.RemapVariantName(allDependencies[i]);
			}
			AssetBundleManager.m_Dependencies[assetBundleName] = allDependencies;
			for (int j = 0; j < allDependencies.Length; j++)
			{
				if (string.IsNullOrEmpty(allDependencies[j]))
				{
					Debug.LogWarning("Assetbundle " + assetBundleName + " has empty dependancy. check for missing texture/reference");
				}
				else if (async)
				{
					AssetBundleManager.LoadAssetBundleInternalAsync(allDependencies[j]);
				}
				else
				{
					AssetBundleManager.LoadAssetBundleInternal(allDependencies[j]);
				}
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000B406 File Offset: 0x00009606
		public static void UnloadAssetBundle(string assetBundleName)
		{
			assetBundleName = AssetBundleManager.RemapVariantName(assetBundleName);
			if (AssetBundleManager.UnloadAssetBundleInternal(assetBundleName))
			{
				AssetBundleManager.UnloadDependencies(assetBundleName);
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000B420 File Offset: 0x00009620
		public static void UnloadAllAndCleanup()
		{
			foreach (LoadedAssetBundle loadedAssetBundle in AssetBundleManager.m_LoadedAssetBundles.Values)
			{
				loadedAssetBundle.OnUnload();
			}
			AssetBundleManager.m_dicVariants = null;
			AssetBundleManager.m_bundlePathCache.Clear();
			AssetBundleManager.m_bundleVariantCache.Clear();
			AssetBundleManager.m_LoadedAssetBundles.Clear();
			AssetBundleManager.m_Dependencies.Clear();
			AssetBundleManager.m_AssetBundleManifest = null;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000B4A8 File Offset: 0x000096A8
		protected static void UnloadDependencies(string assetBundleName)
		{
			string[] array = null;
			if (!AssetBundleManager.m_Dependencies.TryGetValue(assetBundleName, out array))
			{
				return;
			}
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				AssetBundleManager.UnloadAssetBundleInternal(array2[i]);
			}
			AssetBundleManager.m_Dependencies.Remove(assetBundleName);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000B4EC File Offset: 0x000096EC
		protected static bool UnloadAssetBundleInternal(string assetBundleName)
		{
			string text;
			LoadedAssetBundle loadedAssetBundle = AssetBundleManager.GetLoadedAssetBundle(assetBundleName, out text);
			if (loadedAssetBundle == null)
			{
				return false;
			}
			LoadedAssetBundle loadedAssetBundle2 = loadedAssetBundle;
			int num = loadedAssetBundle2.m_ReferencedCount - 1;
			loadedAssetBundle2.m_ReferencedCount = num;
			if (num == 0)
			{
				loadedAssetBundle.OnUnload();
				AssetBundleManager.m_LoadedAssetBundles.Remove(assetBundleName);
				AssetBundleManager.Log(AssetBundleManager.LogType.Info, assetBundleName + " has been unloaded successfully");
				return true;
			}
			return false;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000B540 File Offset: 0x00009740
		private void Update()
		{
			int i = 0;
			while (i < AssetBundleManager.m_InProgressOperations.Count)
			{
				AssetBundleLoadOperation assetBundleLoadOperation = AssetBundleManager.m_InProgressOperations[i];
				if (assetBundleLoadOperation.Update())
				{
					i++;
				}
				else
				{
					AssetBundleManager.m_InProgressOperations.RemoveAt(i);
					this.ProcessFinishedOperation(assetBundleLoadOperation);
				}
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000B58C File Offset: 0x0000978C
		private void ProcessFinishedOperation(AssetBundleLoadOperation operation)
		{
			AssetBundleDownloadOperation assetBundleDownloadOperation = operation as AssetBundleDownloadOperation;
			if (assetBundleDownloadOperation == null)
			{
				return;
			}
			if (assetBundleDownloadOperation.bForceFlush)
			{
				if (assetBundleDownloadOperation.assetBundle != null)
				{
					if (assetBundleDownloadOperation.assetBundle.m_AssetBundle != null)
					{
						Debug.LogWarning("[ProcessFinishedOperation][" + assetBundleDownloadOperation.assetBundle.m_AssetBundle.name + "] ForceFlush");
					}
					assetBundleDownloadOperation.assetBundle.OnUnload();
				}
				return;
			}
			if (string.IsNullOrEmpty(assetBundleDownloadOperation.error))
			{
				assetBundleDownloadOperation.assetBundle.m_ReferencedCount = AssetBundleManager.m_DownloadingBundles[assetBundleDownloadOperation.assetBundleName];
				AssetBundleManager.m_LoadedAssetBundles.Add(assetBundleDownloadOperation.assetBundleName, assetBundleDownloadOperation.assetBundle);
			}
			else if (!AssetBundleManager.m_DownloadingErrors.ContainsKey(assetBundleDownloadOperation.assetBundleName))
			{
				string value = string.Format("Failed downloading bundle {0} from {1}: {2}", assetBundleDownloadOperation.assetBundleName, assetBundleDownloadOperation.GetSourceURL(), assetBundleDownloadOperation.error);
				AssetBundleManager.m_DownloadingErrors.Add(assetBundleDownloadOperation.assetBundleName, value);
			}
			AssetBundleManager.m_DownloadingBundles.Remove(assetBundleDownloadOperation.assetBundleName);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000B688 File Offset: 0x00009888
		public static T LoadAsset<T>(string assetBundleName, string assetName) where T : UnityEngine.Object
		{
			AssetBundleManager.Log(AssetBundleManager.LogType.Info, string.Concat(new string[]
			{
				"Instant Loading ",
				assetName,
				" from ",
				assetBundleName,
				" bundle"
			}));
			assetBundleName = AssetBundleManager.RemapVariantName(assetBundleName);
			AssetBundleManager.LoadAssetBundle(assetBundleName, false, false);
			string text;
			LoadedAssetBundle loadedAssetBundle = AssetBundleManager.GetLoadedAssetBundle(assetBundleName, out text);
			if (!string.IsNullOrEmpty(text))
			{
				AssetBundleManager.Log(AssetBundleManager.LogType.Error, text);
				return default(T);
			}
			if (loadedAssetBundle == null || loadedAssetBundle.m_AssetBundle == null)
			{
				AssetBundleManager.Log(AssetBundleManager.LogType.Error, "Could not open target assetbundle " + assetBundleName);
				return default(T);
			}
			T t = loadedAssetBundle.m_AssetBundle.LoadAsset<T>(assetName);
			if (t == null)
			{
				AssetBundleManager.Log(AssetBundleManager.LogType.Error, "Could not open target asset " + assetName + " from assetbundle " + assetBundleName);
				return default(T);
			}
			return t;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000B764 File Offset: 0x00009964
		public static AssetBundleLoadAssetOperation LoadAssetAsync(string assetBundleName, string assetName, Type type)
		{
			AssetBundleManager.Log(AssetBundleManager.LogType.Info, string.Concat(new string[]
			{
				"Loading ",
				assetName,
				" from ",
				assetBundleName,
				" bundle"
			}));
			assetBundleName = AssetBundleManager.RemapVariantName(assetBundleName);
			string text;
			if (AssetBundleManager.m_DownloadingErrors.TryGetValue(assetBundleName, out text))
			{
				AssetBundleManager.Log(AssetBundleManager.LogType.Error, text);
				return null;
			}
			if (!AssetBundleManager.IsBundleExists(assetBundleName))
			{
				if (NKCPatchUtility.BackgroundPatchEnabled() && AssetBundleManager.IsNonEssentialAssetBundle(assetBundleName))
				{
					AssetBundleManager.Log(AssetBundleManager.LogType.Warning, "bundle " + assetBundleName + " Is NonEssential AssetBundle");
				}
				else
				{
					AssetBundleManager.m_DownloadingErrors.Add(assetBundleName, assetBundleName + " Bundle Not Exists");
					AssetBundleManager.Log(AssetBundleManager.LogType.Error, "bundle " + assetBundleName + " does not exist");
				}
				return null;
			}
			AssetBundleManager.LoadAssetBundle(assetBundleName, true, false);
			AssetBundleLoadAssetOperation assetBundleLoadAssetOperation = new AssetBundleLoadAssetOperationFull(assetBundleName, assetName, type);
			AssetBundleManager.m_InProgressOperations.Add(assetBundleLoadAssetOperation);
			return assetBundleLoadAssetOperation;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000B840 File Offset: 0x00009A40
		private static bool IsNonEssentialAssetBundle(string assetBundleName)
		{
			string[] array = assetBundleName.Split(new char[]
			{
				'.'
			});
			return array.Length == 2 && NKCLocalization.IsVoiceVariant(array[1]);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000B86F File Offset: 0x00009A6F
		public static AssetBundleLoadOperation LoadLevelAsync(string levelName, bool isAddictive, AssetBundleLoadLevelOperation.OnComplete dOnComplete)
		{
			return AssetBundleManager.LoadLevelAsync("scene/" + levelName, levelName, isAddictive, dOnComplete);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000B884 File Offset: 0x00009A84
		public static AssetBundleLoadOperation LoadLevelAsync(string assetBundleName, string levelName, bool isAddictive, AssetBundleLoadLevelOperation.OnComplete dOnComplete)
		{
			AssetBundleManager.Log(AssetBundleManager.LogType.Info, string.Concat(new string[]
			{
				"Loading ",
				levelName,
				" from ",
				assetBundleName,
				" bundle"
			}));
			assetBundleName = AssetBundleManager.RemapVariantName(assetBundleName);
			AssetBundleManager.LoadAssetBundle(assetBundleName, true, false);
			AssetBundleLoadOperation assetBundleLoadOperation = new AssetBundleLoadLevelOperation(assetBundleName, levelName, isAddictive, dOnComplete);
			AssetBundleManager.m_InProgressOperations.Add(assetBundleLoadOperation);
			return assetBundleLoadOperation;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000B8EA File Offset: 0x00009AEA
		public static void LoadLevel(string levelName, bool isAddictive)
		{
			AssetBundleManager.LoadLevel("scene/" + levelName, levelName, isAddictive);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000B900 File Offset: 0x00009B00
		public static void LoadLevel(string assetBundleName, string levelName, bool isAdditive)
		{
			assetBundleName = AssetBundleManager.RemapVariantName(assetBundleName);
			AssetBundleManager.Log(AssetBundleManager.LogType.Info, string.Concat(new string[]
			{
				"Loading ",
				levelName,
				" from ",
				assetBundleName,
				" bundle"
			}));
			AssetBundleManager.LoadAssetBundle(assetBundleName, false, false);
			string text;
			if (AssetBundleManager.GetLoadedAssetBundle(assetBundleName, out text) == null)
			{
				Debug.LogError("Scene bundle not found");
				return;
			}
			if (!string.IsNullOrEmpty(text))
			{
				Debug.LogError(text);
				return;
			}
			if (isAdditive)
			{
				SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
				return;
			}
			SceneManager.LoadScene(levelName, LoadSceneMode.Single);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000B984 File Offset: 0x00009B84
		public static string GetRawFilePath(string filePath)
		{
			foreach (string text in AssetBundleManager.m_ActiveVariants)
			{
				string text2 = "ASSET_RAW/" + filePath.Replace(".", "_" + text.ToUpper() + ".");
				if (AssetBundleManager.IsFileExists(text2))
				{
					return AssetBundleManager.GetBundleFilePath(text2);
				}
			}
			return AssetBundleManager.GetBundleFilePath("ASSET_RAW/" + filePath);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000B9F3 File Offset: 0x00009BF3
		public static IEnumerable<string> GetMergedVariantString(string path)
		{
			foreach (string text in AssetBundleManager.m_ActiveVariants)
			{
				yield return path.Replace(".", "_" + text.ToUpper() + ".");
			}
			string[] array = null;
			yield break;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000BA03 File Offset: 0x00009C03
		private static bool IsFileExists(string path)
		{
			return !string.IsNullOrEmpty(NKCPatchDownloader.Instance.GetFileFullPath(path));
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000BA18 File Offset: 0x00009C18
		public static string GetBundleFilePath(string bundleName)
		{
			string result;
			if (AssetBundleManager.m_bundlePathCache.TryGetValue(bundleName, out result))
			{
				return result;
			}
			if (NKCObbUtil.s_bLoadedOBB && NKCObbUtil.IsOBBPath(NKCPatchDownloader.Instance.GetFileFullPath(bundleName)))
			{
				NKCObbUtil.ExtractFile(bundleName);
				string fileFullPath = NKCPatchDownloader.Instance.GetFileFullPath(bundleName);
				AssetBundleManager.m_bundlePathCache[bundleName] = fileFullPath;
				return fileFullPath;
			}
			string fileFullPath2 = NKCPatchDownloader.Instance.GetFileFullPath(bundleName);
			AssetBundleManager.m_bundlePathCache[bundleName] = fileFullPath2;
			return fileFullPath2;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000BA88 File Offset: 0x00009C88
		public static bool IsBundleExists(string bundleName)
		{
			bundleName = AssetBundleManager.RemapVariantName(bundleName);
			return !string.IsNullOrEmpty(AssetBundleManager.GetBundleFilePath(bundleName));
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000BAA0 File Offset: 0x00009CA0
		public static bool IsAssetExists(string bundleName, string assetName)
		{
			bundleName = AssetBundleManager.RemapVariantName(bundleName);
			string text;
			LoadedAssetBundle loadedAssetBundle = AssetBundleManager.GetLoadedAssetBundle(bundleName, out text);
			return loadedAssetBundle != null && loadedAssetBundle.m_AssetBundle != null && loadedAssetBundle.m_AssetBundle.Contains(assetName);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000BAE0 File Offset: 0x00009CE0
		public static bool IsAssetBundleLoaded(string bundleName)
		{
			string text;
			return AssetBundleManager.GetLoadedAssetBundle(bundleName, out text) != null;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000BAF8 File Offset: 0x00009CF8
		public static string[] GetAllAssetNameInBundle(string bundleName)
		{
			bundleName = bundleName.ToLower();
			bundleName = AssetBundleManager.RemapVariantName(bundleName);
			string text;
			LoadedAssetBundle loadedAssetBundle = AssetBundleManager.GetLoadedAssetBundle(bundleName, out text);
			if (!string.IsNullOrEmpty(text))
			{
				Debug.LogError(text);
				return null;
			}
			if (loadedAssetBundle == null || loadedAssetBundle.m_AssetBundle == null)
			{
				Debug.Log("target bundle not loaded");
				return null;
			}
			string[] allAssetNames = loadedAssetBundle.m_AssetBundle.GetAllAssetNames();
			if (allAssetNames != null)
			{
				for (int i = 0; i < allAssetNames.Length; i++)
				{
					allAssetNames[i] = Path.GetFileNameWithoutExtension(allAssetNames[i]).ToUpper();
				}
			}
			return allAssetNames;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000BB79 File Offset: 0x00009D79
		public static Stream OpenCryptoBundleFileStream(string path)
		{
			if (path.Contains("jar:"))
			{
				return new NKCAssetbundleInnerStream(path);
			}
			if (NKCObbUtil.IsOBBPath(path))
			{
				return new NKCAssetbundleCryptoStreamMem(NKCObbUtil.GetEntryBufferByFullPath(path), path);
			}
			return new NKCAssetbundleCryptoStream(path, FileMode.Open, FileAccess.Read);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000BBAC File Offset: 0x00009DAC
		public static List<string> LoadSavedAssetBundleLists(string fileName)
		{
			List<string> list = new List<string>();
			list.Add(Path.Combine(AssetBundleManager.GetLocalDownloadPath(), fileName));
			List<string> list2 = new List<string>();
			foreach (string path in list)
			{
				if (File.Exists(path))
				{
					list2.Clear();
					using (StreamReader streamReader = new StreamReader(path))
					{
						JSONArray asArray = JSONNode.Parse(streamReader.ReadToEnd()).AsArray;
						if (!(asArray == null))
						{
							for (int i = 0; i < asArray.Count; i++)
							{
								string[] array = asArray[i].ToString().ToLower().Replace("\"", "").Split(new char[]
								{
									'.'
								});
								list2.Add(array[0]);
							}
							break;
						}
					}
				}
			}
			return list2;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000BCB8 File Offset: 0x00009EB8
		public static void SaveResourceListsToFile(string fileName, string path, HashSet<string> resourceList)
		{
			string path2 = Path.Combine(path, fileName);
			new JSONObject();
			JSONNode jsonnode = new JSONArray();
			int num = 0;
			foreach (string s in resourceList)
			{
				jsonnode[num] = s;
				num++;
			}
			using (StreamWriter streamWriter = new StreamWriter(path2, false, Encoding.UTF8))
			{
				streamWriter.WriteLine(jsonnode.ToString());
			}
			Debug.Log("[SaveResourceListsToFile] fileName[" + fileName + "]");
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000BD74 File Offset: 0x00009F74
		public static void SaveLoadedAssetBundleListToFile(string fileName)
		{
			string dataPath = Application.dataPath;
			if (!Directory.Exists(dataPath))
			{
				Directory.CreateDirectory(dataPath);
			}
			string path = Path.Combine(dataPath, fileName);
			new JSONObject();
			JSONNode jsonnode = new JSONArray();
			int num = 0;
			foreach (KeyValuePair<string, LoadedAssetBundle> keyValuePair in AssetBundleManager.m_LoadedAssetBundles)
			{
				new JSONObject()["filename"] = keyValuePair.Key;
				jsonnode[num] = keyValuePair.Key;
				num++;
			}
			using (StreamWriter streamWriter = new StreamWriter(path, false, Encoding.UTF8))
			{
				streamWriter.WriteLine(jsonnode.ToString());
			}
			Debug.Log("[SaveLoadedAssets] fileName[" + fileName + "]");
		}

		// Token: 0x040001D3 RID: 467
		public static readonly ulong[] MaskList = new ulong[]
		{
			6024583083643712183UL,
			9359322754833546465UL,
			920365553137762983UL,
			7040909297097279612UL
		};

		// Token: 0x040001D4 RID: 468
		public static List<ulong> maskList = new List<ulong>();

		// Token: 0x040001D5 RID: 469
		public const long AssetMaskLength = 212L;

		// Token: 0x040001D6 RID: 470
		private static AssetBundleManager.LogMode m_LogMode = AssetBundleManager.LogMode.All;

		// Token: 0x040001D7 RID: 471
		private static string m_BaseDownloadingURL = "";

		// Token: 0x040001D8 RID: 472
		private static string[] m_ActiveVariants = new string[]
		{
			"asset"
		};

		// Token: 0x040001D9 RID: 473
		private static AssetBundleManifest m_AssetBundleManifest = null;

		// Token: 0x040001DA RID: 474
		private static GameObject s_objAssetBundleManager;

		// Token: 0x040001DB RID: 475
		private static HashSet<string> m_TotalLoadedAssetBundleNames = new HashSet<string>();

		// Token: 0x040001DC RID: 476
		private static HashSet<string> m_TotalLoadedAssetBundlePath = new HashSet<string>();

		// Token: 0x040001DD RID: 477
		private static Dictionary<string, LoadedAssetBundle> m_LoadedAssetBundles = new Dictionary<string, LoadedAssetBundle>();

		// Token: 0x040001DE RID: 478
		private static Dictionary<string, string> m_DownloadingErrors = new Dictionary<string, string>();

		// Token: 0x040001DF RID: 479
		private static Dictionary<string, int> m_DownloadingBundles = new Dictionary<string, int>();

		// Token: 0x040001E0 RID: 480
		private static List<AssetBundleLoadOperation> m_InProgressOperations = new List<AssetBundleLoadOperation>();

		// Token: 0x040001E1 RID: 481
		private static Dictionary<string, string[]> m_Dependencies = new Dictionary<string, string[]>();

		// Token: 0x040001E2 RID: 482
		public const string DEFAULT_VARIENT = "asset";

		// Token: 0x040001E3 RID: 483
		public const string DEVELOPMENT_VARIANT = "dev";

		// Token: 0x040001E4 RID: 484
		public const string DEVELOPMENT_VARIANT_TAG = "DEV_VARIANT";

		// Token: 0x040001E6 RID: 486
		private static Dictionary<string, HashSet<string>> m_dicVariants = null;

		// Token: 0x040001E7 RID: 487
		private static Dictionary<string, string> m_bundleVariantCache = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040001E8 RID: 488
		private static Dictionary<string, string> m_bundlePathCache = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x02001100 RID: 4352
		public enum LogMode
		{
			// Token: 0x04009127 RID: 37159
			All,
			// Token: 0x04009128 RID: 37160
			JustErrors
		}

		// Token: 0x02001101 RID: 4353
		public enum LogType
		{
			// Token: 0x0400912A RID: 37162
			Info,
			// Token: 0x0400912B RID: 37163
			Warning,
			// Token: 0x0400912C RID: 37164
			Error
		}

		// Token: 0x02001102 RID: 4354
		// (Invoke) Token: 0x06009EE4 RID: 40676
		public delegate string OverrideBaseDownloadingURLDelegate(string bundleName);
	}
}
