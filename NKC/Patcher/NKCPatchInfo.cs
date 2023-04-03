using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AssetBundles;
using NKC.Localization;
using NKC.Publisher;
using SimpleJSON;
using UnityEngine;

namespace NKC.Patcher
{
	// Token: 0x02000873 RID: 2163
	public class NKCPatchInfo
	{
		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x0600561F RID: 22047 RVA: 0x001A0478 File Offset: 0x0019E678
		// (set) Token: 0x0600561E RID: 22046 RVA: 0x001A046F File Offset: 0x0019E66F
		public string VersionString { get; set; }

		// Token: 0x06005620 RID: 22048 RVA: 0x001A0480 File Offset: 0x0019E680
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("version : " + this.VersionString);
			foreach (KeyValuePair<string, NKCPatchInfo.PatchFileInfo> keyValuePair in this.m_dicPatchInfo)
			{
				stringBuilder.AppendLine(keyValuePair.Value.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005621 RID: 22049 RVA: 0x001A0504 File Offset: 0x0019E704
		public void AddFiles(IEnumerable<string> FilesToAdd, string basePath)
		{
			foreach (string text in FilesToAdd)
			{
				string text2 = Path.Combine(basePath, text);
				if (!File.Exists(text2))
				{
					throw new FileNotFoundException(string.Format("file {0} Not Found, Something Wrong", text2));
				}
				string hash = NKCPatchUtility.CalculateMD5(text2);
				long size = NKCPatchUtility.FileSize(text2);
				this.m_dicPatchInfo.Add(text, new NKCPatchInfo.PatchFileInfo(text, hash, size));
			}
		}

		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x06005623 RID: 22051 RVA: 0x001A0595 File Offset: 0x0019E795
		// (set) Token: 0x06005622 RID: 22050 RVA: 0x001A058C File Offset: 0x0019E78C
		public string FileFullPath { get; private set; }

		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x06005625 RID: 22053 RVA: 0x001A05A6 File Offset: 0x0019E7A6
		// (set) Token: 0x06005624 RID: 22052 RVA: 0x001A059D File Offset: 0x0019E79D
		public string FilePath { get; private set; }

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x06005627 RID: 22055 RVA: 0x001A05B7 File Offset: 0x0019E7B7
		// (set) Token: 0x06005626 RID: 22054 RVA: 0x001A05AE File Offset: 0x0019E7AE
		public string FileName { get; private set; }

		// Token: 0x06005628 RID: 22056 RVA: 0x001A05BF File Offset: 0x0019E7BF
		public void SetFilePath(string filePath)
		{
			this.FileFullPath = filePath;
			this.FileName = Path.GetFileName(this.FileFullPath);
			this.FilePath = this.FileFullPath.Replace(this.FileName, "");
		}

		// Token: 0x06005629 RID: 22057 RVA: 0x001A05F8 File Offset: 0x0019E7F8
		public static NKCPatchInfo LoadFromJSON(string path)
		{
			Debug.Log("[LoadFromJSON] patch info load : " + path);
			NKCPatchInfo nkcpatchInfo = new NKCPatchInfo();
			nkcpatchInfo.SetFilePath(path);
			if (NKCPatchUtility.IsFileExists(path))
			{
				Debug.Log("[LoadFromJSON] patch info load exist: " + path);
				JSONNode jsonnode;
				if (path.Contains("jar:"))
				{
					string jarRelativePath = NKCAssetbundleInnerStream.GetJarRelativePath(path);
					Debug.Log("[LoadFromJSON] open from jar : " + jarRelativePath);
					jsonnode = JSONNode.LoadFromStream(BetterStreamingAssets.OpenRead(jarRelativePath));
				}
				else if (NKCObbUtil.IsOBBPath(path))
				{
					MemoryStream memoryStream = new MemoryStream(NKCObbUtil.GetEntryBufferByFullPath(path));
					Debug.Log("[LoadFromJSON] Inner PatchInfo");
					jsonnode = JSONNode.LoadFromStream(memoryStream);
					memoryStream.Dispose();
				}
				else
				{
					jsonnode = JSONNode.LoadFromFile(path);
				}
				Debug.Log("[LoadFromJSON] patch info load from file end");
				nkcpatchInfo.VersionString = jsonnode["version"];
				Debug.Log("[LoadFromJSON] patch info load from file end version : " + nkcpatchInfo.VersionString);
				JSONNode jsonnode2 = jsonnode["data"];
				for (int i = 0; i < jsonnode2.Count; i++)
				{
					NKCPatchInfo.PatchFileInfo patchFileInfo = new NKCPatchInfo.PatchFileInfo(jsonnode2[i]);
					nkcpatchInfo.m_dicPatchInfo.Add(patchFileInfo.FileName, patchFileInfo);
				}
				Debug.Log("[LoadFromJSON] patch info load exist End");
			}
			else
			{
				Debug.Log("[LoadFromJSON] patch info load fail: " + path);
				nkcpatchInfo.VersionString = "";
			}
			return nkcpatchInfo;
		}

		// Token: 0x0600562A RID: 22058 RVA: 0x001A0744 File Offset: 0x0019E944
		private JSONNode GetJSONNode()
		{
			JSONObject jsonobject = new JSONObject();
			jsonobject["version"] = this.VersionString;
			int num = 0;
			JSONNode jsonnode = new JSONArray();
			jsonobject["data"] = jsonnode;
			foreach (KeyValuePair<string, NKCPatchInfo.PatchFileInfo> keyValuePair in this.m_dicPatchInfo)
			{
				jsonnode[num] = keyValuePair.Value.GetJSONNode();
				num++;
			}
			return jsonobject;
		}

		// Token: 0x0600562B RID: 22059 RVA: 0x001A07DC File Offset: 0x0019E9DC
		public void SaveAsJSON(string path, string fileName)
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			string text = Path.Combine(path, fileName);
			this.GetJSONNode().SaveToFile(text);
			this.FileFullPath = text;
		}

		// Token: 0x0600562C RID: 22060 RVA: 0x001A0814 File Offset: 0x0019EA14
		public void SaveAsJSON()
		{
			if (string.IsNullOrEmpty(this.FilePath))
			{
				Debug.LogWarning("[SaveAsJSON] FilePath is null or empty");
			}
			if (string.IsNullOrEmpty(this.FileName))
			{
				Debug.LogWarning("[SaveAsJSON] FilePath is null or empty");
			}
			this.SaveAsJSON(this.FilePath, this.FileName);
		}

		// Token: 0x0600562D RID: 22061 RVA: 0x001A0861 File Offset: 0x0019EA61
		public bool IsEmpty()
		{
			return this.m_dicPatchInfo == null || this.m_dicPatchInfo.Count == 0;
		}

		// Token: 0x0600562E RID: 22062 RVA: 0x001A0880 File Offset: 0x0019EA80
		public NKCPatchInfo Append(NKCPatchInfo targetPatchInfo)
		{
			NKCPatchInfo nkcpatchInfo = new NKCPatchInfo();
			nkcpatchInfo.VersionString = targetPatchInfo.VersionString;
			nkcpatchInfo.m_dicPatchInfo = new Dictionary<string, NKCPatchInfo.PatchFileInfo>(this.m_dicPatchInfo);
			foreach (KeyValuePair<string, NKCPatchInfo.PatchFileInfo> keyValuePair in targetPatchInfo.m_dicPatchInfo)
			{
				NKCPatchInfo.PatchFileInfo patchInfo = nkcpatchInfo.GetPatchInfo(keyValuePair.Key);
				if (patchInfo == null)
				{
					nkcpatchInfo.AddPatchFileInfo(keyValuePair.Value);
				}
				else if (patchInfo.FileUpdated(keyValuePair.Value))
				{
					nkcpatchInfo.AddPatchFileInfo(keyValuePair.Value);
				}
			}
			return nkcpatchInfo;
		}

		// Token: 0x0600562F RID: 22063 RVA: 0x001A092C File Offset: 0x0019EB2C
		public NKCPatchInfo.PatchFileInfo GetPatchInfo(string name)
		{
			if (this.m_dicPatchInfo.ContainsKey(name))
			{
				return this.m_dicPatchInfo[name];
			}
			return null;
		}

		// Token: 0x06005630 RID: 22064 RVA: 0x001A094A File Offset: 0x0019EB4A
		public bool PatchInfoExists(string name)
		{
			return this.m_dicPatchInfo.ContainsKey(name);
		}

		// Token: 0x06005631 RID: 22065 RVA: 0x001A0958 File Offset: 0x0019EB58
		public void RemovePatchFileInfo(string name)
		{
			if (this.PatchInfoExists(name))
			{
				this.m_dicPatchInfo.Remove(name);
			}
		}

		// Token: 0x06005632 RID: 22066 RVA: 0x001A0970 File Offset: 0x0019EB70
		public void AddPatchFileInfo(NKCPatchInfo.PatchFileInfo patchFileInfo)
		{
			if (this.m_dicPatchInfo.ContainsKey(patchFileInfo.FileName))
			{
				this.m_dicPatchInfo.Remove(patchFileInfo.FileName);
			}
			NKCPatchInfo.PatchFileInfo value = new NKCPatchInfo.PatchFileInfo(patchFileInfo.FileName, patchFileInfo.Hash, patchFileInfo.Size);
			this.m_dicPatchInfo[patchFileInfo.FileName] = value;
		}

		// Token: 0x06005633 RID: 22067 RVA: 0x001A09CC File Offset: 0x0019EBCC
		public IEnumerable<KeyValuePair<string, NKCPatchInfo.PatchFileInfo>> GetPatchFile(Func<KeyValuePair<string, NKCPatchInfo.PatchFileInfo>, bool> predicate)
		{
			return this.m_dicPatchInfo.Where(predicate);
		}

		// Token: 0x06005634 RID: 22068 RVA: 0x001A09DC File Offset: 0x0019EBDC
		public NKCPatchInfo DifferenceOfSetBy(NKCPatchInfo targetPatchInfo)
		{
			NKCPatchInfo nkcpatchInfo = new NKCPatchInfo();
			foreach (KeyValuePair<string, NKCPatchInfo.PatchFileInfo> keyValuePair in this.m_dicPatchInfo)
			{
				if (targetPatchInfo.GetPatchInfo(keyValuePair.Key) == null)
				{
					nkcpatchInfo.AddPatchFileInfo(keyValuePair.Value);
				}
			}
			return nkcpatchInfo;
		}

		// Token: 0x06005635 RID: 22069 RVA: 0x001A0A4C File Offset: 0x0019EC4C
		public NKCPatchInfo FilterByVariants(List<string> lstVariants)
		{
			NKCPatchInfo nkcpatchInfo = new NKCPatchInfo();
			nkcpatchInfo.VersionString = this.VersionString;
			Dictionary<string, NKCPatchUtility.FileFilterInfo> dictionary = new Dictionary<string, NKCPatchUtility.FileFilterInfo>();
			List<string> allVariants = NKCLocalization.GetAllVariants();
			foreach (KeyValuePair<string, NKCPatchInfo.PatchFileInfo> keyValuePair in this.m_dicPatchInfo)
			{
				string key = keyValuePair.Key;
				string key2;
				string text;
				if (key.Contains("ASSET_RAW"))
				{
					Tuple<string, string> variantFromFilename = NKCPatchUtility.GetVariantFromFilename(key, allVariants);
					key2 = variantFromFilename.Item1;
					text = variantFromFilename.Item2;
				}
				else
				{
					string[] array = key.Split(new char[]
					{
						'.'
					});
					if (array.Length == 1)
					{
						key2 = key;
						text = null;
					}
					else
					{
						key2 = array[0];
						text = array[1];
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					nkcpatchInfo.m_dicPatchInfo.Add(key, keyValuePair.Value);
				}
				else
				{
					int num = lstVariants.IndexOf(text);
					if (num >= 0)
					{
						NKCPatchUtility.FileFilterInfo fileFilterInfo;
						if (dictionary.TryGetValue(key2, out fileFilterInfo))
						{
							int num2 = lstVariants.IndexOf(fileFilterInfo.variant);
							if (num < num2)
							{
								dictionary[key2] = new NKCPatchUtility.FileFilterInfo(key, text);
							}
						}
						else if (!NKCConnectionInfo.IgnoreVariantList.Contains(text) && !NKCPublisherModule.CheckReviewServerSkipVariant(text))
						{
							dictionary.Add(key2, new NKCPatchUtility.FileFilterInfo(key, text));
						}
					}
				}
			}
			foreach (KeyValuePair<string, NKCPatchUtility.FileFilterInfo> keyValuePair2 in dictionary)
			{
				string originalName = keyValuePair2.Value.originalName;
				if (this.m_dicPatchInfo.ContainsKey(originalName))
				{
					nkcpatchInfo.m_dicPatchInfo.Add(originalName, this.m_dicPatchInfo[originalName]);
				}
				else
				{
					Debug.LogError("Logic Error!");
				}
			}
			return nkcpatchInfo;
		}

		// Token: 0x06005636 RID: 22070 RVA: 0x001A0C3C File Offset: 0x0019EE3C
		public NKCPatchInfo MakePatchinfoSubset(IEnumerable<string> bundleNames)
		{
			NKCPatchInfo nkcpatchInfo = new NKCPatchInfo();
			nkcpatchInfo.VersionString = this.VersionString;
			HashSet<string> hashSet = new HashSet<string>();
			foreach (string text in bundleNames)
			{
				hashSet.Add(text.ToLower());
			}
			foreach (KeyValuePair<string, NKCPatchInfo.PatchFileInfo> keyValuePair in this.m_dicPatchInfo)
			{
				string text2 = keyValuePair.Key.Split(new char[]
				{
					'.'
				})[0];
				if (hashSet.Contains(text2.ToLower()))
				{
					nkcpatchInfo.m_dicPatchInfo.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return nkcpatchInfo;
		}

		// Token: 0x040044A9 RID: 17577
		public Dictionary<string, NKCPatchInfo.PatchFileInfo> m_dicPatchInfo = new Dictionary<string, NKCPatchInfo.PatchFileInfo>();

		// Token: 0x040044AB RID: 17579
		public const bool bSaveAsBinary = true;

		// Token: 0x040044AC RID: 17580
		private const string VersionJSONName = "version";

		// Token: 0x040044AD RID: 17581
		private const string FileInfoListJsonName = "data";

		// Token: 0x0200153D RID: 5437
		public enum eFileIntergityStatus
		{
			// Token: 0x0400A04D RID: 41037
			OK,
			// Token: 0x0400A04E RID: 41038
			ERROR_SIZE,
			// Token: 0x0400A04F RID: 41039
			ERROR_HASH
		}

		// Token: 0x0200153E RID: 5438
		public class PatchFileInfo
		{
			// Token: 0x0600AC3B RID: 44091 RVA: 0x00353DCF File Offset: 0x00351FCF
			public PatchFileInfo(string bundleName, string hash, long size)
			{
				this.FileName = bundleName;
				this.Hash = hash;
				this.Size = size;
			}

			// Token: 0x0600AC3C RID: 44092 RVA: 0x00353DEC File Offset: 0x00351FEC
			public PatchFileInfo(JSONNode node)
			{
				this.FileName = node[0].Value;
				this.Hash = node[1].Value;
				this.Size = ((node.Count > 2) ? long.Parse(node[2]) : 0L);
			}

			// Token: 0x0600AC3D RID: 44093 RVA: 0x00353E48 File Offset: 0x00352048
			public JSONNode GetJSONNode()
			{
				JSONArray jsonarray = new JSONArray();
				jsonarray[0] = this.FileName;
				jsonarray[1] = this.Hash;
				jsonarray[2] = this.Size.ToString();
				return jsonarray;
			}

			// Token: 0x0600AC3E RID: 44094 RVA: 0x00353E95 File Offset: 0x00352095
			public bool FileUpdated(NKCPatchInfo.PatchFileInfo newFile)
			{
				if (this.FileName != newFile.FileName)
				{
					Debug.Log("Wrong Comparison");
					return false;
				}
				return !this.Hash.Equals(newFile.Hash);
			}

			// Token: 0x0600AC3F RID: 44095 RVA: 0x00353ECA File Offset: 0x003520CA
			public NKCPatchInfo.eFileIntergityStatus CheckFileIntegrity(string fullPath)
			{
				if (!NKCPatchUtility.CheckIntegrity(fullPath, this.Hash))
				{
					return NKCPatchInfo.eFileIntergityStatus.ERROR_HASH;
				}
				if (!NKCPatchUtility.CheckSize(fullPath, this.Size))
				{
					return NKCPatchInfo.eFileIntergityStatus.ERROR_SIZE;
				}
				return NKCPatchInfo.eFileIntergityStatus.OK;
			}

			// Token: 0x0600AC40 RID: 44096 RVA: 0x00353EED File Offset: 0x003520ED
			public override string ToString()
			{
				return string.Format("{0} : {1}", this.FileName, this.Hash);
			}

			// Token: 0x0400A050 RID: 41040
			public string FileName;

			// Token: 0x0400A051 RID: 41041
			public string Hash;

			// Token: 0x0400A052 RID: 41042
			public long Size;
		}
	}
}
