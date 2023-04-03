using System;
using AssetBundles;
using Cs.Logging;
using NKC;
using NKC.Patcher;
using NKM;
using UnityEngine;

// Token: 0x02000007 RID: 7
public class NKCPatcherStringList
{
	// Token: 0x06000022 RID: 34 RVA: 0x0000272C File Offset: 0x0000092C
	public static void LoadStrings(string fileName, string tableName, bool bOverwriteDuplicate)
	{
		TextAsset textAsset = Resources.Load<TextAsset>(fileName);
		string str = "";
		if (textAsset == null)
		{
			string text = Application.streamingAssetsPath + "/" + fileName;
			if (NKCPatchUtility.IsFileExists(text))
			{
				Debug.Log("patcherString exist in SA");
				if (text.Contains("jar:"))
				{
					str = BetterStreamingAssets.ReadAllText(NKCAssetbundleInnerStream.GetJarRelativePath(text));
				}
			}
			else
			{
				Debug.Log("patcherString not exist in SA");
			}
		}
		else
		{
			str = textAsset.ToString();
		}
		NKMLua nkmlua = new NKMLua();
		if (!nkmlua.DoString(str))
		{
			Log.ErrorAndExit("[PatchString] lua file loading fail. fileName:" + fileName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCPatcherStringList.cs", 53);
		}
		if (!nkmlua.OpenTable(tableName))
		{
			Log.ErrorAndExit("[PatchString] lua table open fail. fileName:" + fileName + " tableName:" + tableName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCPatcherStringList.cs", 62);
		}
		int num = 1;
		while (nkmlua.OpenTable(num))
		{
			string strID = null;
			if (nkmlua.GetData("m_StringID", ref strID))
			{
				foreach (object obj in Enum.GetValues(typeof(NKM_NATIONAL_CODE)))
				{
					NKM_NATIONAL_CODE nationalCode = (NKM_NATIONAL_CODE)obj;
					string value = null;
					if (nkmlua.GetData(nationalCode.ToString(), ref value))
					{
						NKCStringTable.AddString(nationalCode, strID, value, bOverwriteDuplicate);
					}
				}
			}
			num++;
			nkmlua.CloseTable();
		}
		nkmlua.CloseTable();
		nkmlua.LuaClose();
	}
}
