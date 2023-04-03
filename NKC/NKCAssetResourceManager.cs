using System;
using System.Collections;
using System.Collections.Generic;
using AssetBundles;
using Cs.Logging;
using NKC.UI;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200063F RID: 1599
	public class NKCAssetResourceManager : MonoBehaviour
	{
		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x060031BF RID: 12735 RVA: 0x000F70D6 File Offset: 0x000F52D6
		public static IReadOnlyDictionary<string, string> dicFileList
		{
			get
			{
				return NKCAssetResourceManager.m_dicFileList;
			}
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x000F70E8 File Offset: 0x000F52E8
		public static void Init()
		{
			NKCAssetResourceManager.ReserveInstanceData(200);
			NKCAssetResourceManager.ReserveResourceData(200);
			NKCAssetResourceManager.m_dicFileList.Clear();
			NKCAssetResourceManager.m_setLocFileList.Clear();
			NKCAssetResourceManager.LoadAssetBundleNamingFromLua("AB_SCRIPT", "LUA_ASSET_BUNDLE_FILE_LIST");
			Log.Info("AssetNaming Loaded", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCAssetResourceManager.cs", 312);
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x000F7140 File Offset: 0x000F5340
		public static void LoadAssetBundleNamingFromLua(string bundlePath, string luaFilePath)
		{
			Log.Info("LoadAssetBundleNamingFromLua - [" + luaFilePath + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCAssetResourceManager.cs", 318);
			NKMLua nkmlua = new NKMLua();
			if (nkmlua.LoadCommonPath(bundlePath, luaFilePath, true))
			{
				if (nkmlua.OpenTable("AssetBundleFileList"))
				{
					int num = 1;
					while (nkmlua.OpenTable(num))
					{
						string text = "";
						nkmlua.GetData(1, ref text);
						string text2 = "";
						nkmlua.GetData(2, ref text2);
						if (!NKCAssetResourceManager.m_dicFileList.ContainsKey(text))
						{
							NKCAssetResourceManager.m_dicFileList.Add(text, text2);
						}
						else
						{
							Log.Error("m_dicFileList Duplicate fileName: " + text2 + ", " + text, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCAssetResourceManager.cs", 340);
						}
						num++;
						nkmlua.CloseTable();
					}
					nkmlua.CloseTable();
				}
				if (nkmlua.OpenTable("LocAssetBundleFileList"))
				{
					int num2 = 1;
					string item = "";
					while (nkmlua.GetData(num2, ref item))
					{
						NKCAssetResourceManager.m_setLocFileList.Add(item);
						num2++;
					}
				}
			}
			nkmlua.LuaClose();
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x000F7248 File Offset: 0x000F5448
		public static bool IsAssetInLocBundle(string bundleName, string assetName)
		{
			string item = bundleName.ToLower() + "_loc@" + assetName;
			return NKCAssetResourceManager.m_setLocFileList.Contains(item);
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x000F7272 File Offset: 0x000F5472
		public static void SetLocFileCheck(bool bOn)
		{
			if (bOn)
			{
				NKCAssetResourceManager.lstLocFileList.Clear();
			}
			NKCAssetResourceManager.m_bChecked = bOn;
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x000F7288 File Offset: 0x000F5488
		public static List<string> GetMissedLocFileList()
		{
			List<string> list = new List<string>();
			foreach (string item in NKCAssetResourceManager.m_setLocFileList)
			{
				if (!NKCAssetResourceManager.lstLocFileList.Contains(item))
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x000F72F0 File Offset: 0x000F54F0
		public static bool IsAssetInLocBundleCheckAll(string bundleName, string assetName)
		{
			string item = bundleName.ToLower() + "@" + assetName;
			if (NKCAssetResourceManager.m_bChecked)
			{
				NKCAssetResourceManager.lstLocFileList.Add(item);
			}
			if (NKCAssetResourceManager.m_setLocFileList.Contains(item))
			{
				return true;
			}
			string item2 = bundleName.ToLower() + "_loc@" + assetName;
			return NKCAssetResourceManager.m_setLocFileList.Contains(item2);
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x000F734D File Offset: 0x000F554D
		public static string GetBundleName(string fileName)
		{
			if (NKCAssetResourceManager.m_dicFileList.ContainsKey(fileName))
			{
				return NKCAssetResourceManager.m_dicFileList[fileName];
			}
			Log.Error("GetBundleName Null: " + fileName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCAssetResourceManager.cs", 415);
			return "";
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x000F7388 File Offset: 0x000F5588
		private static void ReserveResourceData(int count)
		{
			for (int i = 0; i < count; i++)
			{
				NKCAssetResourceData item = new NKCAssetResourceData("", "", false);
				NKCAssetResourceManager.m_qResourceDataPool.Enqueue(item);
			}
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x000F73C0 File Offset: 0x000F55C0
		private static void ReserveInstanceData(int count)
		{
			for (int i = 0; i < count; i++)
			{
				NKCAssetInstanceData item = new NKCAssetInstanceData();
				NKCAssetResourceManager.m_qInstanceDataPool.Enqueue(item);
			}
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x000F73EA File Offset: 0x000F55EA
		private static NKCAssetResourceData GetResourceDataFromPool(string bundleName, string assetName, bool bAsync)
		{
			if (NKCAssetResourceManager.m_qResourceDataPool.Count <= 0)
			{
				NKCAssetResourceManager.ReserveResourceData(200);
			}
			NKCAssetResourceData nkcassetResourceData = NKCAssetResourceManager.m_qResourceDataPool.Dequeue();
			nkcassetResourceData.Init(bundleName, assetName, bAsync);
			return nkcassetResourceData;
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x000F7416 File Offset: 0x000F5616
		private static NKCAssetInstanceData GetInstanceDataFromPool()
		{
			if (NKCAssetResourceManager.m_qInstanceDataPool.Count <= 0)
			{
				NKCAssetResourceManager.ReserveInstanceData(200);
			}
			return NKCAssetResourceManager.m_qInstanceDataPool.Dequeue();
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x000F7439 File Offset: 0x000F5639
		private static void ReturnResourceDataToPool(NKCAssetResourceData cNKCResourceData)
		{
			cNKCResourceData.Unload();
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x000F7441 File Offset: 0x000F5641
		private static void ReturnInstanceDataToPool(NKCAssetInstanceData cNKCInstantData)
		{
			cNKCInstantData.Unload();
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x000F7449 File Offset: 0x000F5649
		public static bool IsLoadEnd()
		{
			if (!NKCAssetResourceManager.IsLoadEndResource())
			{
				return false;
			}
			if (!NKCAssetResourceManager.IsLoadEndInstant())
			{
				return false;
			}
			NKCAssetResourceManager.m_ResourceDataLoadCountMax = 0;
			NKCAssetResourceManager.m_InstantDataLoadCountMax = 0;
			return true;
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x000F746A File Offset: 0x000F566A
		public static bool IsLoadEndResource()
		{
			return NKCAssetResourceManager.m_linklistNKCResourceDataAsync.Count <= 0;
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x000F747C File Offset: 0x000F567C
		public static bool IsLoadEndInstant()
		{
			return NKCAssetResourceManager.m_linklistNKCInstanceDataAsync.Count <= 0;
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x000F7490 File Offset: 0x000F5690
		public static float GetLoadProgress()
		{
			float num = 0f;
			if (NKCAssetResourceManager.m_ResourceDataLoadCountMax == 0)
			{
				num += 0.5f;
			}
			else
			{
				num += (1f - (float)NKCAssetResourceManager.m_linklistNKCResourceDataAsync.Count / (float)NKCAssetResourceManager.m_ResourceDataLoadCountMax) * 0.5f;
			}
			if (NKCAssetResourceManager.m_InstantDataLoadCountMax == 0)
			{
				num += 0.5f;
			}
			else
			{
				num += (1f - (float)NKCAssetResourceManager.m_linklistNKCInstanceDataAsync.Count / (float)NKCAssetResourceManager.m_InstantDataLoadCountMax) * 0.5f;
			}
			return num;
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x000F7508 File Offset: 0x000F5708
		public static void Update()
		{
			LinkedListNode<NKCAssetResourceData> linkedListNode = NKCAssetResourceManager.m_linklistNKCResourceDataAsync.First;
			while (linkedListNode != null)
			{
				NKCAssetResourceData value = linkedListNode.Value;
				if (value.IsDone())
				{
					if (value.callBack != null)
					{
						value.callBack(value);
					}
					NKCAssetResourceManager.m_AsyncResourceLoadingCount--;
					LinkedListNode<NKCAssetResourceData> next = linkedListNode.Next;
					NKCAssetResourceManager.m_linklistNKCResourceDataAsync.Remove(linkedListNode);
					linkedListNode = next;
				}
				else
				{
					linkedListNode = linkedListNode.Next;
				}
			}
			if (NKCAssetResourceManager.m_linklistNKCResourceDataAsync.Count == 0)
			{
				while (NKCAssetResourceManager.m_linklistNKCInstanceDataAsyncReady.Count > 0 && NKCAssetResourceManager.m_AsyncInstantLoadingCount <= 100)
				{
					NKCAssetInstanceData value2 = NKCAssetResourceManager.m_linklistNKCInstanceDataAsyncReady.First.Value;
					NKCAssetResourceManager.m_linklistNKCInstanceDataAsyncReady.RemoveFirst();
					NKCAssetResourceManager.m_AsyncInstantLoadingCount++;
					value2.m_fTime = Time.time;
					NKCScenManager.GetScenManager().StartCoroutine(NKCAssetResourceManager.LoadInstanceAsync(value2));
				}
				LinkedListNode<NKCAssetInstanceData> linkedListNode2 = NKCAssetResourceManager.m_linklistNKCInstanceDataAsync.First;
				while (linkedListNode2 != null)
				{
					NKCAssetInstanceData value3 = linkedListNode2.Value;
					if (value3.m_bLoad || value3.GetLoadFail())
					{
						NKCAssetResourceManager.m_AsyncInstantLoadingCount--;
						LinkedListNode<NKCAssetInstanceData> next2 = linkedListNode2.Next;
						NKCAssetResourceManager.m_linklistNKCInstanceDataAsync.Remove(linkedListNode2);
						linkedListNode2 = next2;
					}
					else
					{
						linkedListNode2 = linkedListNode2.Next;
					}
				}
			}
			if (NKCAssetResourceManager.m_linklistNKCResourceDataAsync.Count == 0 && NKCAssetResourceManager.m_linklistNKCInstanceDataAsync.Count == 0)
			{
				foreach (NKCAssetInstanceData nkcassetInstanceData in NKCAssetResourceManager.m_setNKCAssetInstanceDataToReservedClose)
				{
					Debug.LogWarning("m_qNKCAssetInstanceDataToReservedClose Processing, assetName : " + nkcassetInstanceData.m_InstantOrg.m_NKMAssetName.m_AssetName);
					NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				}
				NKCAssetResourceManager.m_setNKCAssetInstanceDataToReservedClose.Clear();
				foreach (NKCAssetResourceData nkcassetResourceData in NKCAssetResourceManager.m_setNKCAssetResourceDataToReservedClose)
				{
					if (nkcassetResourceData.m_RefCount <= 0)
					{
						NKCAssetResourceManager.DeleteResource(nkcassetResourceData);
					}
				}
				NKCAssetResourceManager.m_setNKCAssetResourceDataToReservedClose.Clear();
			}
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x000F7710 File Offset: 0x000F5910
		public static NKCAssetResourceData OpenResource<T>(string assetName, bool bAsync = false) where T : UnityEngine.Object
		{
			return NKCAssetResourceManager.OpenResource<T>(NKCAssetResourceManager.GetBundleName(assetName), assetName, bAsync, null);
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x000F7720 File Offset: 0x000F5920
		public static NKCAssetResourceData OpenResource<T>(NKMAssetName cNKMAssetName, bool bAsync = false) where T : UnityEngine.Object
		{
			return NKCAssetResourceManager.OpenResource<T>(cNKMAssetName.m_BundleName, cNKMAssetName.m_AssetName, bAsync, null);
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x000F7738 File Offset: 0x000F5938
		public static NKCAssetResourceData OpenResource<T>(string bundleName, string assetName, bool bAsync = false, CallBackHandler callBackFunc = null) where T : UnityEngine.Object
		{
			bundleName = bundleName.ToLower();
			assetName = assetName.ToUpper();
			string text = bundleName + "_loc";
			if (NKCAssetResourceManager.IsAssetInLocBundle(bundleName, assetName))
			{
				if (AssetBundleManager.IsBundleExists(text))
				{
					bundleName = text;
				}
				else
				{
					if (NKCDefineManager.DEFINE_USE_CHEAT() || !NKCDefineManager.DEFINE_SERVICE())
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, "Loc Bundle not found : " + text + "\nNationTag: " + NKMContentsVersionManager.GetCountryTag(), null, "");
						if (!NKCDefineManager.DEFINE_SERVICE())
						{
							NKMContentsVersionManager.DeleteLocalTag();
						}
					}
					Debug.LogError("Loc Bundle not found : " + text);
				}
			}
			NKCAssetResourceBundle assetResourceBundle = NKCAssetResourceManager.GetAssetResourceBundle(bundleName);
			NKCAssetResourceData nkcassetResourceData;
			if (assetResourceBundle.m_dicNKCResourceData.ContainsKey(assetName))
			{
				nkcassetResourceData = assetResourceBundle.m_dicNKCResourceData[assetName];
				if (nkcassetResourceData != null)
				{
					nkcassetResourceData.m_RefCount++;
					if (!bAsync && !nkcassetResourceData.IsDone())
					{
						Debug.LogWarning(string.Format("Trying sync load while processing async load {0}, {1} Forcing Sync load..", bundleName, assetName));
						nkcassetResourceData.ForceSyncLoad();
					}
				}
				else
				{
					Debug.LogWarning(string.Format("Found Resource Data is null, bundleName : {0}, assetName {1}", bundleName, assetName));
				}
			}
			else
			{
				nkcassetResourceData = NKCAssetResourceManager.GetResourceDataFromPool(bundleName, assetName, bAsync);
				nkcassetResourceData.BeginLoad<T>();
				nkcassetResourceData.callBack = callBackFunc;
				if (bAsync)
				{
					NKCAssetResourceManager.m_linklistNKCResourceDataAsync.AddLast(nkcassetResourceData);
					NKCAssetResourceManager.m_ResourceDataLoadCountMax = NKCAssetResourceManager.m_linklistNKCResourceDataAsync.Count;
				}
				assetResourceBundle.m_dicNKCResourceData.Add(assetName, nkcassetResourceData);
			}
			return nkcassetResourceData;
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x000F7874 File Offset: 0x000F5A74
		public static string RemapLocBundle(string bundleName, string assetName)
		{
			if (NKCAssetResourceManager.IsAssetInLocBundle(bundleName, assetName))
			{
				string text = bundleName.ToLower() + "_loc";
				if (AssetBundleManager.IsBundleExists(text))
				{
					return text;
				}
				Debug.LogError("Loc Bundle not found : " + text);
			}
			return bundleName.ToLower();
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x000F78BB File Offset: 0x000F5ABB
		public static bool IsBundleExists(NKMAssetName assetName)
		{
			return NKCAssetResourceManager.IsBundleExists(assetName.m_BundleName, assetName.m_AssetName);
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x000F78CE File Offset: 0x000F5ACE
		public static bool IsBundleExists(string bundleName, string assetName)
		{
			bundleName = bundleName.ToLower();
			if (NKCAssetResourceManager.IsAssetInLocBundle(bundleName, assetName))
			{
				return AssetBundleManager.IsBundleExists(bundleName + "_loc");
			}
			return AssetBundleManager.IsBundleExists(bundleName);
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x000F78F8 File Offset: 0x000F5AF8
		public static bool IsAssetExists(string bundleName, string assetName, bool loadUnloadedBundle)
		{
			bundleName = bundleName.ToLower();
			if (NKCAssetResourceManager.IsAssetInLocBundle(bundleName, assetName))
			{
				bundleName += "_loc";
			}
			if (!AssetBundleManager.IsBundleExists(bundleName))
			{
				return false;
			}
			if (!AssetBundleManager.IsAssetBundleLoaded(bundleName))
			{
				if (!loadUnloadedBundle)
				{
					return false;
				}
				AssetBundleManager.LoadAssetBundle(bundleName, false);
			}
			return AssetBundleManager.IsAssetExists(bundleName, assetName);
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x000F794C File Offset: 0x000F5B4C
		public static int CloseResource(string bundleName, string assetName)
		{
			assetName = assetName.ToUpper();
			bundleName = bundleName.ToLower();
			NKCAssetResourceBundle assetResourceBundle = NKCAssetResourceManager.GetAssetResourceBundle(bundleName);
			if (assetResourceBundle.m_dicNKCResourceData.ContainsKey(assetName))
			{
				return NKCAssetResourceManager.CloseResource(assetResourceBundle.m_dicNKCResourceData[assetName]);
			}
			return -1;
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x000F7994 File Offset: 0x000F5B94
		public static int CloseResource(NKCAssetResourceData cNKCResourceData)
		{
			int result = -1;
			if (cNKCResourceData == null)
			{
				Debug.LogWarning("AssetResourceMgr.CloseResource Fail Because Data is null ");
				return result;
			}
			if (cNKCResourceData.m_RefCount <= 0)
			{
				Debug.LogWarning("AssetResourceMgr.CloseResource Fail Because Ref Count <= 0, AssetName : " + cNKCResourceData.m_NKMAssetName.m_AssetName + " ");
				return result;
			}
			cNKCResourceData.m_RefCount--;
			result = cNKCResourceData.m_RefCount;
			if (cNKCResourceData.m_RefCount == 0)
			{
				if (NKCAssetResourceManager.m_linklistNKCResourceDataAsync.Count == 0 && NKCAssetResourceManager.m_linklistNKCInstanceDataAsync.Count == 0 && cNKCResourceData.IsDone())
				{
					NKCAssetResourceManager.DeleteResource(cNKCResourceData);
				}
				else if (!NKCAssetResourceManager.m_setNKCAssetResourceDataToReservedClose.Contains(cNKCResourceData))
				{
					NKCAssetResourceManager.m_setNKCAssetResourceDataToReservedClose.Add(cNKCResourceData);
				}
			}
			return result;
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x000F7A3C File Offset: 0x000F5C3C
		private static void DeleteResource(NKCAssetResourceData cNKCResourceData)
		{
			NKCAssetResourceBundle assetResourceBundle = NKCAssetResourceManager.GetAssetResourceBundle(cNKCResourceData.m_NKMAssetName.m_BundleName);
			if (assetResourceBundle.m_dicNKCResourceData.ContainsKey(cNKCResourceData.m_NKMAssetName.m_AssetName))
			{
				assetResourceBundle.m_dicNKCResourceData.Remove(cNKCResourceData.m_NKMAssetName.m_AssetName);
				NKCAssetResourceManager.ReturnResourceDataToPool(cNKCResourceData);
				return;
			}
			Debug.LogWarning("AssetResourceMgr.DeleteResource Fail, AssetName : " + cNKCResourceData.m_NKMAssetName.m_AssetName + " ");
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x000F7AB0 File Offset: 0x000F5CB0
		public static void UnloadAllResources()
		{
			foreach (NKCAssetResourceBundle nkcassetResourceBundle in NKCAssetResourceManager.m_dicNKCResourceBundle.Values)
			{
				foreach (NKCAssetResourceData cNKCResourceData in nkcassetResourceBundle.m_dicNKCResourceData.Values)
				{
					NKCAssetResourceManager.ReturnResourceDataToPool(cNKCResourceData);
				}
				nkcassetResourceBundle.m_dicNKCResourceData.Clear();
			}
			NKCAssetResourceManager.m_dicNKCResourceBundle.Clear();
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x000F7B5C File Offset: 0x000F5D5C
		private static NKCAssetResourceBundle GetAssetResourceBundle(string bundleName)
		{
			NKCAssetResourceBundle nkcassetResourceBundle;
			if (!NKCAssetResourceManager.m_dicNKCResourceBundle.ContainsKey(bundleName))
			{
				nkcassetResourceBundle = new NKCAssetResourceBundle();
				NKCAssetResourceManager.m_dicNKCResourceBundle.Add(bundleName, nkcassetResourceBundle);
			}
			else
			{
				nkcassetResourceBundle = NKCAssetResourceManager.m_dicNKCResourceBundle[bundleName];
			}
			return nkcassetResourceBundle;
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x000F7B99 File Offset: 0x000F5D99
		private static IEnumerator LoadInstanceAsync(NKCAssetInstanceData cNKCInstantData)
		{
			while (cNKCInstantData.m_InstantOrg != null && cNKCInstantData.m_InstantOrg.GetAsset<GameObject>() == null && !cNKCInstantData.GetLoadFail())
			{
				yield return null;
			}
			if (cNKCInstantData.m_InstantOrg != null && !cNKCInstantData.GetLoadFail())
			{
				cNKCInstantData.m_Instant = UnityEngine.Object.Instantiate<GameObject>(cNKCInstantData.m_InstantOrg.GetAsset<GameObject>());
				cNKCInstantData.m_bLoad = true;
				cNKCInstantData.m_Instant.transform.SetParent(NKCScenManager.GetScenManager().Get_NKM_NEW_INSTANT().transform);
			}
			yield return cNKCInstantData;
			yield break;
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x000F7BA8 File Offset: 0x000F5DA8
		public static NKCAssetInstanceData OpenInstance<T>(NKMAssetName cNKMAssetName, bool bAsync = false, Transform parent = null) where T : UnityEngine.Object
		{
			return NKCAssetResourceManager.OpenInstance<T>(cNKMAssetName.m_BundleName, cNKMAssetName.m_AssetName, bAsync, parent);
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x000F7BC0 File Offset: 0x000F5DC0
		public static NKCAssetInstanceData OpenInstance<T>(string bundleName, string assetName, bool bAsync = false, Transform parent = null) where T : UnityEngine.Object
		{
			NKCAssetResourceData nkcassetResourceData = NKCAssetResourceManager.OpenResource<T>(bundleName, assetName, bAsync, null);
			NKCAssetInstanceData instanceDataFromPool = NKCAssetResourceManager.GetInstanceDataFromPool();
			instanceDataFromPool.m_BundleName = bundleName;
			instanceDataFromPool.m_AssetName = assetName;
			instanceDataFromPool.m_InstantOrg = nkcassetResourceData;
			instanceDataFromPool.m_bLoadTypeAsync = bAsync;
			if (bAsync)
			{
				NKCAssetResourceManager.m_linklistNKCInstanceDataAsyncReady.AddLast(instanceDataFromPool);
				NKCAssetResourceManager.m_linklistNKCInstanceDataAsync.AddLast(instanceDataFromPool);
				NKCAssetResourceManager.m_InstantDataLoadCountMax = NKCAssetResourceManager.m_linklistNKCInstanceDataAsync.Count;
				return instanceDataFromPool;
			}
			if (nkcassetResourceData != null && nkcassetResourceData.GetAsset<GameObject>() != null)
			{
				if (parent != null)
				{
					instanceDataFromPool.m_Instant = UnityEngine.Object.Instantiate<GameObject>(nkcassetResourceData.GetAsset<GameObject>(), parent);
				}
				else
				{
					instanceDataFromPool.m_Instant = UnityEngine.Object.Instantiate<GameObject>(nkcassetResourceData.GetAsset<GameObject>());
				}
				instanceDataFromPool.m_bLoad = true;
				if (parent == null)
				{
					if (NKCScenManager.GetScenManager() != null && NKCScenManager.GetScenManager().Get_NKM_NEW_INSTANT() != null)
					{
						instanceDataFromPool.m_Instant.transform.SetParent(NKCScenManager.GetScenManager().Get_NKM_NEW_INSTANT().transform);
					}
					else
					{
						instanceDataFromPool.m_Instant.transform.SetParent(null);
					}
				}
				return instanceDataFromPool;
			}
			return null;
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x000F7CD4 File Offset: 0x000F5ED4
		public static void CloseInstance(NKCAssetInstanceData cInstantData)
		{
			if (cInstantData == null)
			{
				return;
			}
			if (cInstantData.m_bLoadTypeAsync && !cInstantData.m_bLoad && NKCAssetResourceManager.m_linklistNKCInstanceDataAsync.Count > 0)
			{
				LinkedListNode<NKCAssetInstanceData> linkedListNode = NKCAssetResourceManager.m_linklistNKCInstanceDataAsync.First;
				while (linkedListNode != null)
				{
					NKCAssetInstanceData value = linkedListNode.Value;
					if (value == cInstantData)
					{
						if (!NKCAssetResourceManager.m_setNKCAssetInstanceDataToReservedClose.Contains(value))
						{
							NKCAssetResourceManager.m_setNKCAssetInstanceDataToReservedClose.Add(value);
							return;
						}
						Debug.LogWarning("AssetResourceMgr.CloseInstance fail because this instance is already reserved to close, assetName : " + value.m_AssetName);
						return;
					}
					else if (linkedListNode != null)
					{
						linkedListNode = linkedListNode.Next;
					}
				}
			}
			NKCAssetResourceManager.ReturnInstanceDataToPool(cInstantData);
		}

		// Token: 0x040030E7 RID: 12519
		private static Dictionary<string, NKCAssetResourceBundle> m_dicNKCResourceBundle = new Dictionary<string, NKCAssetResourceBundle>(500);

		// Token: 0x040030E8 RID: 12520
		private static LinkedList<NKCAssetResourceData> m_linklistNKCResourceDataAsync = new LinkedList<NKCAssetResourceData>();

		// Token: 0x040030E9 RID: 12521
		private static int m_AsyncResourceLoadingCount = 0;

		// Token: 0x040030EA RID: 12522
		private static LinkedList<NKCAssetInstanceData> m_linklistNKCInstanceDataAsyncReady = new LinkedList<NKCAssetInstanceData>();

		// Token: 0x040030EB RID: 12523
		private static LinkedList<NKCAssetInstanceData> m_linklistNKCInstanceDataAsync = new LinkedList<NKCAssetInstanceData>();

		// Token: 0x040030EC RID: 12524
		private static int m_AsyncInstantLoadingCount = 0;

		// Token: 0x040030ED RID: 12525
		private static HashSet<NKCAssetInstanceData> m_setNKCAssetInstanceDataToReservedClose = new HashSet<NKCAssetInstanceData>();

		// Token: 0x040030EE RID: 12526
		private static HashSet<NKCAssetResourceData> m_setNKCAssetResourceDataToReservedClose = new HashSet<NKCAssetResourceData>();

		// Token: 0x040030EF RID: 12527
		private static Queue<NKCAssetResourceData> m_qResourceDataPool = new Queue<NKCAssetResourceData>(500);

		// Token: 0x040030F0 RID: 12528
		private static Queue<NKCAssetInstanceData> m_qInstanceDataPool = new Queue<NKCAssetInstanceData>(500);

		// Token: 0x040030F1 RID: 12529
		private static int m_ResourceDataLoadCountMax = 0;

		// Token: 0x040030F2 RID: 12530
		private static int m_InstantDataLoadCountMax = 0;

		// Token: 0x040030F3 RID: 12531
		private static Dictionary<string, string> m_dicFileList = new Dictionary<string, string>(50000);

		// Token: 0x040030F4 RID: 12532
		private static HashSet<string> m_setLocFileList = new HashSet<string>();

		// Token: 0x040030F5 RID: 12533
		private static List<string> lstLocFileList = new List<string>();

		// Token: 0x040030F6 RID: 12534
		private static bool m_bChecked = false;
	}
}
