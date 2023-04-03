using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using AssetBundles;
using Cs.Logging;
using NKM;
using UnityEngine;

namespace NKC.Patcher
{
	// Token: 0x02000883 RID: 2179
	public static class NKCPatchUtility
	{
		// Token: 0x060056A0 RID: 22176 RVA: 0x001A1844 File Offset: 0x0019FA44
		public static string GetHash<T>(this Stream stream) where T : HashAlgorithm, new()
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (T t = Activator.CreateInstance<T>())
			{
				foreach (byte b in t.ComputeHash(stream))
				{
					stringBuilder.Append(b.ToString("x2"));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060056A1 RID: 22177 RVA: 0x001A18BC File Offset: 0x0019FABC
		public static long FileSize(string FullPath)
		{
			if (FullPath.Contains("jar:"))
			{
				return BetterStreamingAssets.FileSize(NKCAssetbundleInnerStream.GetJarRelativePath(FullPath));
			}
			if (NKCObbUtil.IsOBBPath(FullPath))
			{
				return (long)NKCObbUtil.GetObbEntrySize(NKCObbUtil.GetRelativePathFromOBB(FullPath));
			}
			return new FileInfo(FullPath).Length;
		}

		// Token: 0x060056A2 RID: 22178 RVA: 0x001A18F8 File Offset: 0x0019FAF8
		public static string CalculateMD5(string fullPath)
		{
			if (fullPath.Contains("jar:"))
			{
				using (Stream stream = BetterStreamingAssets.OpenRead(NKCAssetbundleInnerStream.GetJarRelativePath(fullPath)))
				{
					return NKCPatchUtility.CalculateMD5(stream);
				}
			}
			if (NKCObbUtil.IsOBBPath(fullPath))
			{
				return NKCPatchUtility.CalculateMD5(NKCObbUtil.GetEntryBufferByFullPath(fullPath));
			}
			string result;
			using (FileStream fileStream = File.OpenRead(fullPath))
			{
				result = NKCPatchUtility.CalculateMD5(fileStream);
			}
			return result;
		}

		// Token: 0x060056A3 RID: 22179 RVA: 0x001A197C File Offset: 0x0019FB7C
		public static string CalculateMD5(Stream stream)
		{
			string result;
			using (MD5 md = MD5.Create())
			{
				result = NKCPatchUtility.ConvertByteCode(md.ComputeHash(stream));
			}
			return result;
		}

		// Token: 0x060056A4 RID: 22180 RVA: 0x001A19BC File Offset: 0x0019FBBC
		public static string CalculateMD5(byte[] buffer)
		{
			string result;
			using (MD5 md = MD5.Create())
			{
				byte[] hashBytes = md.ComputeHash(buffer);
				buffer = null;
				result = NKCPatchUtility.ConvertByteCode(hashBytes);
			}
			return result;
		}

		// Token: 0x060056A5 RID: 22181 RVA: 0x001A19FC File Offset: 0x0019FBFC
		private static string ConvertByteCode(byte[] hashBytes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in hashBytes)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060056A6 RID: 22182 RVA: 0x001A1A3C File Offset: 0x0019FC3C
		public static void CopyFile(string sourcePath, string targetPath)
		{
			if (!File.Exists(sourcePath))
			{
				Debug.LogError("File " + sourcePath + " Not Found");
				return;
			}
			if (!Directory.Exists(Path.GetDirectoryName(targetPath)))
			{
				Directory.CreateDirectory(targetPath);
			}
			File.Copy(sourcePath, targetPath, true);
		}

		// Token: 0x060056A7 RID: 22183 RVA: 0x001A1A78 File Offset: 0x0019FC78
		public static void CleanupDirectory(string basePath, NKCPatchInfo lastedPatchInfo, List<string> ignoreList, NKCPatchInfo innerPatchInfo = null, NKCPatchInfo currentPatchInfo = null)
		{
			NKCPatchUtility._CleanupDirectory(basePath, new Uri(basePath), lastedPatchInfo, ignoreList, innerPatchInfo, currentPatchInfo);
		}

		// Token: 0x060056A8 RID: 22184 RVA: 0x001A1A8C File Offset: 0x0019FC8C
		private static void _CleanupDirectory(string currentPath, Uri basePathUri, NKCPatchInfo lastedPatchInfo, List<string> ignoreList, NKCPatchInfo innerPatchInfo = null, NKCPatchInfo currentPatchInfo = null)
		{
			foreach (string text in Directory.GetDirectories(currentPath))
			{
				NKCPatchUtility._CleanupDirectory(text, basePathUri, lastedPatchInfo, ignoreList, innerPatchInfo, currentPatchInfo);
				if (NKCPatchUtility.IsDirectoryEmpty(text))
				{
					Directory.Delete(text);
				}
			}
			char[] trimChars = new char[]
			{
				Path.AltDirectorySeparatorChar,
				Path.DirectorySeparatorChar,
				Path.PathSeparator
			};
			string[] array = Directory.GetFiles(currentPath);
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i];
				Uri uri = new Uri(text2);
				Uri uri2 = basePathUri.MakeRelativeUri(uri);
				string relativePath = Uri.UnescapeDataString(uri2.ToString());
				relativePath = relativePath.TrimStart(trimChars);
				bool flag = ignoreList.Exists((string x) => string.Equals(x, relativePath, StringComparison.OrdinalIgnoreCase));
				if (!lastedPatchInfo.PatchInfoExists(relativePath) && !flag)
				{
					if (string.IsNullOrEmpty(Path.GetExtension(relativePath)))
					{
						string name = relativePath + ".asset";
						string text3 = text2 + ".asset";
						string name2 = relativePath + ".vkor";
						string text4 = text2 + ".vkor";
						if (lastedPatchInfo.PatchInfoExists(name) && !File.Exists(text3))
						{
							File.Move(text2, text3);
						}
						else if (lastedPatchInfo.PatchInfoExists(name2) && !File.Exists(text4))
						{
							File.Move(text2, text4);
						}
						else
						{
							File.Delete(text2);
						}
					}
					else
					{
						File.Delete(text2);
					}
				}
				else if (innerPatchInfo != null && lastedPatchInfo.PatchInfoExists(relativePath))
				{
					NKCPatchInfo.PatchFileInfo patchInfo = lastedPatchInfo.GetPatchInfo(relativePath);
					NKCPatchInfo.PatchFileInfo patchInfo2 = innerPatchInfo.GetPatchInfo(relativePath);
					if (patchInfo != null && patchInfo2 != null)
					{
						string innerAssetPath = NKCPatchUtility.GetInnerAssetPath(relativePath, false);
						if (!patchInfo.FileUpdated(patchInfo2) && NKCPatchUtility.IsFileExists(innerAssetPath))
						{
							if (currentPatchInfo != null)
							{
								NKCPatchInfo.PatchFileInfo patchInfo3 = currentPatchInfo.GetPatchInfo(relativePath);
								if (patchInfo3 != null)
								{
									patchInfo3.Size = patchInfo2.Size;
									patchInfo3.Hash = patchInfo2.Hash;
								}
								else
								{
									NKCPatchInfo.PatchFileInfo value = new NKCPatchInfo.PatchFileInfo(patchInfo2.FileName, patchInfo2.Hash, patchInfo2.Size);
									currentPatchInfo.m_dicPatchInfo.Add(relativePath, value);
								}
							}
							else
							{
								Debug.LogWarning(string.Format("CurrentPatchInfo is null when CleanupDirectory", Array.Empty<object>()));
							}
							File.Delete(text2);
						}
					}
				}
			}
		}

		// Token: 0x060056A9 RID: 22185 RVA: 0x001A1D1C File Offset: 0x0019FF1C
		private static bool IsDirectoryEmpty(string path)
		{
			return Directory.GetFileSystemEntries(path).Length == 0;
		}

		// Token: 0x060056AA RID: 22186 RVA: 0x001A1D28 File Offset: 0x0019FF28
		public static bool CheckIntegrity(string path, string targetHash)
		{
			return string.IsNullOrEmpty(targetHash) || (NKCPatchUtility.IsFileExists(path) && NKCPatchUtility.CalculateMD5(path).Equals(targetHash));
		}

		// Token: 0x060056AB RID: 22187 RVA: 0x001A1D4C File Offset: 0x0019FF4C
		public static bool CheckSize(string path, long targetSize)
		{
			return NKCPatchUtility.IsFileExists(path) && (targetSize == 0L || NKCPatchUtility.FileSize(path).Equals(targetSize));
		}

		// Token: 0x060056AC RID: 22188 RVA: 0x001A1D77 File Offset: 0x0019FF77
		public static string GetInnerAssetPath(string relativePath, bool isLBDefined = false)
		{
			if (NKCDefineManager.DEFINE_LB() || isLBDefined)
			{
				return Application.streamingAssetsPath + "/Assetbundles/" + relativePath;
			}
			return Application.streamingAssetsPath + "/" + relativePath;
		}

		// Token: 0x060056AD RID: 22189 RVA: 0x001A1DA4 File Offset: 0x0019FFA4
		public static bool IsFileExists(string path)
		{
			if (path.Contains("jar:"))
			{
				string jarRelativePath = NKCAssetbundleInnerStream.GetJarRelativePath(path);
				return !string.IsNullOrEmpty(jarRelativePath) && BetterStreamingAssets.FileExists(jarRelativePath);
			}
			if (NKCObbUtil.IsOBBPath(path))
			{
				return NKCObbUtil.GetObbEntryIndex(NKCObbUtil.GetRelativePathFromOBB(path)) != -1;
			}
			return File.Exists(path);
		}

		// Token: 0x060056AE RID: 22190 RVA: 0x001A1DF8 File Offset: 0x0019FFF8
		public static Tuple<string, string> GetVariantFromFilename(string path, List<string> lstVariants)
		{
			string text = path;
			int num = text.LastIndexOf('.');
			string str;
			if (num > 0)
			{
				if (text.EndsWith("."))
				{
					str = "";
				}
				else
				{
					str = text.Substring(num + 1);
				}
				text = text.Substring(0, num);
			}
			else
			{
				str = "";
			}
			int num2 = text.LastIndexOf('_');
			if (num2 > 0 && !text.EndsWith("_"))
			{
				string item = text.Substring(0, num2) + "." + str;
				string value = text.Substring(num2 + 1);
				foreach (string text2 in lstVariants)
				{
					if (text2.Equals(value, StringComparison.InvariantCultureIgnoreCase))
					{
						return new Tuple<string, string>(item, text2);
					}
				}
			}
			return new Tuple<string, string>(path, "asset");
		}

		// Token: 0x060056AF RID: 22191 RVA: 0x001A1EE4 File Offset: 0x001A00E4
		public static bool IsPatchSkip()
		{
			if (NKCPatchDownloader.Instance == null)
			{
				Debug.LogError("PatchDownloader not initialized!");
				return false;
			}
			return NKCDefineManager.DEFINE_PATCH_SKIP() && !NKCPatchUtility.GetTutorialClearedStatus() && NKCPatchUtility.IsFileExists(NKCPatchUtility.GetInnerAssetPath("PatchInfo.json", false));
		}

		// Token: 0x060056B0 RID: 22192 RVA: 0x001A1F31 File Offset: 0x001A0131
		public static bool SaveTutorialClearedStatus()
		{
			NKCPatchUtility.GetTutorialClearedStatus();
			PlayerPrefs.SetInt("PKTutorialCleared", 1);
			PlayerPrefs.Save();
			return false;
		}

		// Token: 0x060056B1 RID: 22193 RVA: 0x001A1F4A File Offset: 0x001A014A
		public static bool GetTutorialClearedStatus()
		{
			return PlayerPrefs.GetInt("PKTutorialCleared", 0) != 0;
		}

		// Token: 0x060056B2 RID: 22194 RVA: 0x001A1F5A File Offset: 0x001A015A
		public static void DeleteTutorialClearedStatus()
		{
			PlayerPrefs.DeleteKey("PKTutorialCleared");
		}

		// Token: 0x060056B3 RID: 22195 RVA: 0x001A1F68 File Offset: 0x001A0168
		public static void ClearAllInnerAssetsFromDownloaded(NKCPatchInfo innerPatchInfo, string innerPath)
		{
			foreach (KeyValuePair<string, NKCPatchInfo.PatchFileInfo> keyValuePair in innerPatchInfo.m_dicPatchInfo)
			{
				string key = keyValuePair.Key;
				FileInfo fileInfo = new FileInfo(Path.Combine(innerPath, key));
				if (fileInfo.Exists)
				{
					File.SetAttributes(fileInfo.FullName, FileAttributes.Normal);
					fileInfo.Delete();
				}
			}
		}

		// Token: 0x060056B4 RID: 22196 RVA: 0x001A1FE8 File Offset: 0x001A01E8
		public static void ReservePatchSkipTest()
		{
			PlayerPrefs.SetInt("PKSkipTestReserve", 1);
			PlayerPrefs.Save();
		}

		// Token: 0x060056B5 RID: 22197 RVA: 0x001A1FFA File Offset: 0x001A01FA
		public static void ProcessPatchSkipTest(string localDownloadPath)
		{
			if (PlayerPrefs.GetInt("PKSkipTestReserve", 0) != 0)
			{
				PlayerPrefs.DeleteKey("PKSkipTestReserve");
				NKCPatchUtility.DeleteTutorialClearedStatus();
				PlayerPrefs.Save();
				if (Directory.Exists(localDownloadPath))
				{
					Directory.Delete(localDownloadPath, true);
				}
			}
		}

		// Token: 0x060056B6 RID: 22198 RVA: 0x001A202C File Offset: 0x001A022C
		public static long GetDownloadFileSize(List<NKCPatchInfo.PatchFileInfo> patchFiles)
		{
			long num = 0L;
			foreach (NKCPatchInfo.PatchFileInfo patchFileInfo in patchFiles)
			{
				num += patchFileInfo.Size;
			}
			return num;
		}

		// Token: 0x060056B7 RID: 22199 RVA: 0x001A2080 File Offset: 0x001A0280
		public static NKCPatchDownloader.DownType GetDownloadType()
		{
			int @int = PlayerPrefs.GetInt("DownloadTypeKey", -1);
			if (@int == -1)
			{
				NKCPatchDownloader.DownType downType = NKCPatchDownloader.DownType.TutorialWithBackground;
				if (NKMContentsVersionManager.HasTag("STOP_BACKGROUND_PATCH"))
				{
					downType = NKCPatchDownloader.DownType.FullDownload;
				}
				NKCPatchUtility.SaveDownloadType(downType);
				return downType;
			}
			string str = "[PatcherManager][GetDownloadType] DownloadType[";
			NKCPatchDownloader.DownType downType2 = (NKCPatchDownloader.DownType)@int;
			Log.Debug(str + downType2.ToString() + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchUtility.cs", 538);
			return (NKCPatchDownloader.DownType)@int;
		}

		// Token: 0x060056B8 RID: 22200 RVA: 0x001A20E3 File Offset: 0x001A02E3
		public static void SaveDownloadType(NKCPatchDownloader.DownType type)
		{
			Log.Debug("[PatcherManager][SaveDownloadType] DownloadType[" + type.ToString() + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchUtility.cs", 545);
			PlayerPrefs.SetInt("DownloadTypeKey", (int)type);
			PlayerPrefs.Save();
		}

		// Token: 0x060056B9 RID: 22201 RVA: 0x001A2120 File Offset: 0x001A0320
		public static void RemoveDownloadType()
		{
			Log.Debug("[PatcherManager][RemoveDownloadType]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchUtility.cs", 553);
			PlayerPrefs.DeleteKey("DownloadTypeKey");
			PlayerPrefs.Save();
		}

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x060056BA RID: 22202 RVA: 0x001A2148 File Offset: 0x001A0348
		// (set) Token: 0x060056BB RID: 22203 RVA: 0x001A2188 File Offset: 0x001A0388
		public static bool _enablePatchOptimizationInEditor
		{
			get
			{
				int @int = PlayerPrefs.GetInt("ENABLE_PATCH_OPTIMIZATION", 0);
				Log.Debug(string.Format("[PATCH_OPTIMIZATION] get _enablePatchOptimizationInEditor[{0}]", @int), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchUtility.cs", 564);
				return @int == 1;
			}
			set
			{
				int num = value ? 1 : 0;
				Log.Debug(string.Format("[PATCH_OPTIMIZATION] set _enablePatchOptimizationInEditor[{0}]", num), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchUtility.cs", 571);
				PlayerPrefs.SetInt("ENABLE_PATCH_OPTIMIZATION", num);
			}
		}

		// Token: 0x060056BC RID: 22204 RVA: 0x001A21C7 File Offset: 0x001A03C7
		public static bool BackgroundPatchEnabled()
		{
			return NKCPatchUtility._enablePatchOptimizationInEditor || (NKCDefineManager.DEFINE_PATCH_OPTIMIZATION() && !NKMContentsVersionManager.HasTag("STOP_BACKGROUND_PATCH"));
		}

		// Token: 0x040044D8 RID: 17624
		public const string PREF_KEY_TUTORIAL_CLEARED = "PKTutorialCleared";

		// Token: 0x040044D9 RID: 17625
		private const string PREF_KEY_RESERVE_SKIP_TEST = "PKSkipTestReserve";

		// Token: 0x0200154E RID: 5454
		public struct FileFilterInfo
		{
			// Token: 0x0600AC87 RID: 44167 RVA: 0x00354E3F File Offset: 0x0035303F
			public FileFilterInfo(string _name, string _variant)
			{
				this.originalName = _name;
				this.variant = _variant;
			}

			// Token: 0x0400A088 RID: 41096
			public string originalName;

			// Token: 0x0400A089 RID: 41097
			public string variant;
		}

		// Token: 0x0200154F RID: 5455
		public class VariantSetInfo
		{
			// Token: 0x0600AC88 RID: 44168 RVA: 0x00354E4F File Offset: 0x0035304F
			public VariantSetInfo()
			{
				this.lstUseVariant = new List<string>();
			}

			// Token: 0x0600AC89 RID: 44169 RVA: 0x00354E62 File Offset: 0x00353062
			public VariantSetInfo(List<string> lstVariant)
			{
				this.lstUseVariant = lstVariant;
			}

			// Token: 0x0400A08A RID: 41098
			public List<string> lstUseVariant;
		}
	}
}
