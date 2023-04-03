using System;
using System.Collections.Generic;
using AssetBundles;
using Cs.Logging;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006D2 RID: 1746
	public class NKCStringTable
	{
		// Token: 0x06003CF1 RID: 15601 RVA: 0x00139D14 File Offset: 0x00137F14
		static NKCStringTable()
		{
			NKCStringTable.Clear();
		}

		// Token: 0x06003CF2 RID: 15602 RVA: 0x00139D1B File Offset: 0x00137F1B
		public static void Clear()
		{
			NKCStringTable.m_NationalCode = NKM_NATIONAL_CODE.NNC_KOREA;
			NKCStringTable.m_dicString = new Dictionary<string, StringData>(35000, StringComparer.OrdinalIgnoreCase);
			NKCStringTable.m_dicStringCustom = new Dictionary<string, StringData>(100, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06003CF3 RID: 15603 RVA: 0x00139D48 File Offset: 0x00137F48
		public static string GetNationalPostfix(NKM_NATIONAL_CODE eNKM_NATIONAL_CODE)
		{
			switch (eNKM_NATIONAL_CODE)
			{
			case NKM_NATIONAL_CODE.NNC_KOREA:
				return "_KOREA";
			case NKM_NATIONAL_CODE.NNC_ENG:
				return "_ENG";
			case NKM_NATIONAL_CODE.NNC_JAPAN:
				return "_JPN";
			case NKM_NATIONAL_CODE.NNC_CENSORED_CHINESE:
				return "_CHN";
			case NKM_NATIONAL_CODE.NNC_SIMPLIFIED_CHINESE:
				return "_SCN";
			case NKM_NATIONAL_CODE.NNC_TRADITIONAL_CHINESE:
				return "_TWN";
			case NKM_NATIONAL_CODE.NNC_THAILAND:
				return "_THA";
			case NKM_NATIONAL_CODE.NNC_VIETNAM:
				return "_VTN";
			case NKM_NATIONAL_CODE.NNC_DEUTSCH:
				return "_DEU";
			case NKM_NATIONAL_CODE.NNC_FRENCH:
				return "_FRA";
			default:
				return "";
			}
		}

		// Token: 0x06003CF4 RID: 15604 RVA: 0x00139DC6 File Offset: 0x00137FC6
		public static string GetCurrLanguageCode()
		{
			return NKCStringTable.GetLanguageCode(NKCStringTable.GetNationalCode(), false);
		}

		// Token: 0x06003CF5 RID: 15605 RVA: 0x00139DD4 File Offset: 0x00137FD4
		public static string GetLanguageCode(NKM_NATIONAL_CODE eNKM_NATIONAL_CODE, bool bForTranslation = false)
		{
			switch (eNKM_NATIONAL_CODE)
			{
			case NKM_NATIONAL_CODE.NNC_KOREA:
				return "ko";
			case NKM_NATIONAL_CODE.NNC_ENG:
				return "en";
			case NKM_NATIONAL_CODE.NNC_JAPAN:
				return "ja";
			case NKM_NATIONAL_CODE.NNC_CENSORED_CHINESE:
			case NKM_NATIONAL_CODE.NNC_SIMPLIFIED_CHINESE:
				if (bForTranslation)
				{
					return "zh-CN";
				}
				return "zh-hans";
			case NKM_NATIONAL_CODE.NNC_TRADITIONAL_CHINESE:
				if (bForTranslation)
				{
					return "zh-TW";
				}
				return "zh-hant";
			case NKM_NATIONAL_CODE.NNC_THAILAND:
				return "th";
			case NKM_NATIONAL_CODE.NNC_VIETNAM:
				return "vi";
			case NKM_NATIONAL_CODE.NNC_DEUTSCH:
				return "de";
			case NKM_NATIONAL_CODE.NNC_FRENCH:
				return "fr";
			default:
				return "null";
			}
		}

		// Token: 0x06003CF6 RID: 15606 RVA: 0x00139E5E File Offset: 0x0013805E
		public static NKM_NATIONAL_CODE GetNationalCode()
		{
			return NKCStringTable.m_NationalCode;
		}

		// Token: 0x06003CF7 RID: 15607 RVA: 0x00139E68 File Offset: 0x00138068
		public static bool LoadFromLUA(NKM_NATIONAL_CODE eNationalCode)
		{
			NKCStringTable.m_NationalCode = eNationalCode;
			HashSet<string> hashSet = new HashSet<string>();
			AssetBundleManager.LoadAssetBundle("ab_script_string_table", false);
			string[] allAssetNameInBundle = AssetBundleManager.GetAllAssetNameInBundle("ab_script_string_table");
			if (allAssetNameInBundle == null)
			{
				Log.Error("load fail, String Table Asset Bundle", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCStringTable.cs", 153);
				return false;
			}
			string str = "_C";
			foreach (string text in allAssetNameInBundle)
			{
				string text2 = text;
				if (NKCDefineManager.DEFINE_USE_CONVERTED_FILENAME() && text.Contains("_C"))
				{
					string decryptedFileName = NKMLua.GetDecryptedFileName(text.Substring(0, text.Length - 2));
					text2 = decryptedFileName + "_C";
					text = decryptedFileName;
				}
				if (!text.ToUpper().EndsWith("_DEV"))
				{
					string value = NKCStringTable.GetNationalPostfix(NKCStringTable.m_NationalCode) + str;
					if (text2.ToUpper().EndsWith(value))
					{
						if (hashSet.Contains(text))
						{
							Log.Error(string.Format("Duplicate String Table File Name: {0}, {1}", NKCStringTable.m_NationalCode, text), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCStringTable.cs", 196);
						}
						else
						{
							hashSet.Add(text);
						}
					}
				}
			}
			foreach (string text3 in hashSet)
			{
				if (NKCDefineManager.DEFINE_USE_CONVERTED_FILENAME())
				{
					Debug.Log("[stringTableFile] => " + text3);
				}
				if (!NKCStringTable.LoadFromLUA(text3, "AB_SCRIPT_STRING_TABLE"))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003CF8 RID: 15608 RVA: 0x00139FF0 File Offset: 0x001381F0
		public static void AddString(NKM_NATIONAL_CODE nationalCode, string strID, string value, bool bOverwriteDuplicate)
		{
			if (NKCStringTable.m_NationalCode != nationalCode)
			{
				return;
			}
			if (!NKCStringTable.m_dicString.ContainsKey(strID))
			{
				StringData stringData = new StringData();
				stringData.m_StringID = strID;
				stringData.m_StringValue = value;
				NKCStringTable.m_dicString.Add(stringData.m_StringID, stringData);
				return;
			}
			StringData stringData2 = NKCStringTable.m_dicString[strID];
			if (bOverwriteDuplicate)
			{
				stringData2.m_StringValue = value;
				return;
			}
			Log.Error(string.Format("Duplicate String National[{0}] StrID[{1}]  Prev[{2}] New[{3}]", new object[]
			{
				NKCStringTable.m_NationalCode,
				strID,
				stringData2.m_StringValue,
				value
			}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCStringTable.cs", 262);
		}

		// Token: 0x06003CF9 RID: 15609 RVA: 0x0013A090 File Offset: 0x00138290
		public static bool LoadFromLUA(string fileName, string bundleName = "AB_SCRIPT_STRING_TABLE")
		{
			NKMLua nkmlua = new NKMLua();
			bool bAddCompiledLuaPostFix = NKCDefineManager.DEFINE_USE_CONVERTED_FILENAME();
			if (nkmlua.LoadCommonPath(bundleName, fileName, bAddCompiledLuaPostFix))
			{
				if (nkmlua.OpenTable("m_dicString"))
				{
					int num = 1;
					while (nkmlua.OpenTable(num))
					{
						string text = "";
						nkmlua.GetData(1, ref text);
						StringData stringData;
						if (NKCStringTable.m_dicString.ContainsKey(text))
						{
							stringData = NKCStringTable.m_dicString[text];
							Log.Error(string.Format("Duplicate String: {0}, {1} - {2}", NKCStringTable.m_NationalCode, fileName, text), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCStringTable.cs", 294);
						}
						else
						{
							stringData = new StringData();
							stringData.m_StringID = text;
							NKCStringTable.m_dicString.Add(stringData.m_StringID, stringData);
						}
						string stringValue = "";
						nkmlua.GetData(2, ref stringValue);
						stringData.m_StringValue = stringValue;
						num++;
						nkmlua.CloseTable();
					}
				}
				else
				{
					Log.Error("StringTable can't find m_dicString table, fileName : " + fileName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCStringTable.cs", 314);
				}
			}
			nkmlua.LuaClose();
			return true;
		}

		// Token: 0x06003CFA RID: 15610 RVA: 0x0013A198 File Offset: 0x00138398
		public static void SetNationalCode(NKM_NATIONAL_CODE eNKM_NATIONAL_CODE)
		{
			NKCStringTable.m_NationalCode = eNKM_NATIONAL_CODE;
		}

		// Token: 0x06003CFB RID: 15611 RVA: 0x0013A1A0 File Offset: 0x001383A0
		public static bool CheckExistString(string strID)
		{
			StringData stringData;
			return NKCStringTable.m_dicStringCustom.TryGetValue(strID, out stringData) || NKCStringTable.m_dicString.TryGetValue(strID, out stringData);
		}

		// Token: 0x06003CFC RID: 15612 RVA: 0x0013A1D0 File Offset: 0x001383D0
		private static string ProcessParameteredString(string stringValue, object[] param)
		{
			string text;
			if (param != null)
			{
				text = string.Format(stringValue, param);
			}
			else
			{
				text = stringValue;
			}
			if (NKCScenManager.CurrentUserData() != null)
			{
				text = text.Replace("<usernickname>", NKCScenManager.CurrentUserData().m_UserNickName);
			}
			if (NKCStringTable.m_NationalCode == NKM_NATIONAL_CODE.NNC_KOREA)
			{
				return Korean.ReplaceJosa(text);
			}
			return text;
		}

		// Token: 0x06003CFD RID: 15613 RVA: 0x0013A218 File Offset: 0x00138418
		public static string GetString(string strID, bool bSkipErrorCheck = false)
		{
			if (string.IsNullOrEmpty(strID))
			{
				return "";
			}
			string[] array = null;
			if (strID.Contains("@@"))
			{
				string[] array2 = strID.Split(NKCServerStringFormatter.Seperators, StringSplitOptions.None);
				if (array2.Length > 1)
				{
					strID = array2[0];
					array = new string[array2.Length - 1];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = NKCStringTable.GetString(array2[i + 1], true, null);
					}
				}
			}
			string strID2 = strID;
			object[] param = array;
			return NKCStringTable.GetString(strID2, bSkipErrorCheck, param);
		}

		// Token: 0x06003CFE RID: 15614 RVA: 0x0013A28C File Offset: 0x0013848C
		public static string GetString(string strID, bool bSkipErrorCheck = false, params object[] param)
		{
			if (NKCScenManager.GetScenManager() != null)
			{
				NKCScenManager.GetScenManager().SetLanguage();
			}
			if (string.IsNullOrEmpty(strID))
			{
				return "";
			}
			bool flag = bSkipErrorCheck;
			bSkipErrorCheck = true;
			StringData stringData;
			if (NKCStringTable.m_dicStringCustom.TryGetValue(strID, out stringData))
			{
				if (param != null && param.Length != 0)
				{
					return NKCStringTable.ProcessParameteredString(stringData.m_StringValue, param);
				}
				return stringData.m_StringValue;
			}
			else if (NKCStringTable.m_dicString.TryGetValue(strID, out stringData))
			{
				if (param != null && param.Length != 0)
				{
					return NKCStringTable.ProcessParameteredString(stringData.m_StringValue, param);
				}
				return stringData.m_StringValue;
			}
			else
			{
				if (bSkipErrorCheck)
				{
					if (!flag && bSkipErrorCheck)
					{
						Log.Debug("No Define String: " + strID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCStringTable.cs", 450);
					}
					return strID;
				}
				Log.Error("No Define String: " + strID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCStringTable.cs", 460);
				return "";
			}
		}

		// Token: 0x06003CFF RID: 15615 RVA: 0x0013A35B File Offset: 0x0013855B
		public static string GetString(string strID, params object[] param)
		{
			return NKCStringTable.GetString(strID, false, param);
		}

		// Token: 0x06003D00 RID: 15616 RVA: 0x0013A365 File Offset: 0x00138565
		public static string GetString(NKM_ERROR_CODE error_code)
		{
			return NKCStringTable.GetString(error_code.ToString(), false);
		}

		// Token: 0x06003D01 RID: 15617 RVA: 0x0013A37C File Offset: 0x0013857C
		public static void ChangeString(string strID, string newData)
		{
			if (NKCStringTable.m_dicString.ContainsKey(strID))
			{
				StringData stringData = new StringData();
				stringData.m_StringID = strID;
				stringData.m_StringValue = newData;
				NKCStringTable.m_dicString[strID] = stringData;
			}
		}

		// Token: 0x04003621 RID: 13857
		private static NKM_NATIONAL_CODE m_NationalCode;

		// Token: 0x04003622 RID: 13858
		private static Dictionary<string, StringData> m_dicString;

		// Token: 0x04003623 RID: 13859
		private static Dictionary<string, StringData> m_dicStringCustom;

		// Token: 0x04003624 RID: 13860
		private const int DIC_STRING_SIZE = 35000;

		// Token: 0x04003625 RID: 13861
		private const int DIC_STRING_CUSTOM_SIZE = 100;

		// Token: 0x04003626 RID: 13862
		private const string USER_NICK_NAME = "<usernickname>";
	}
}
