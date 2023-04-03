using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cs.Core.Util;
using Cs.Logging;
using NKM.Templet.Base;
using UnityEngine;

namespace NKM
{
	// Token: 0x020003BF RID: 959
	public static class NKMContentsVersionManager
	{
		// Token: 0x06001925 RID: 6437 RVA: 0x00067B2C File Offset: 0x00065D2C
		static NKMContentsVersionManager()
		{
			for (int i = 0; i < NKMContentsVersionManager.CountryTag.Length; i++)
			{
				string[] countryTag = NKMContentsVersionManager.CountryTag;
				int num = i;
				CountryTagType countryTagType = (CountryTagType)i;
				countryTag[num] = countryTagType.ToString();
			}
			for (int j = 0; j < NKMContentsVersionManager.DataFormatChangeTag.Length; j++)
			{
				string[] dataFormatChangeTag = NKMContentsVersionManager.DataFormatChangeTag;
				int num2 = j;
				DataFormatChangeTagType dataFormatChangeTagType = (DataFormatChangeTagType)j;
				dataFormatChangeTag[num2] = dataFormatChangeTagType.ToString();
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06001926 RID: 6438 RVA: 0x00067BB1 File Offset: 0x00065DB1
		// (set) Token: 0x06001927 RID: 6439 RVA: 0x00067BB8 File Offset: 0x00065DB8
		public static NKMContentsVersion CurrentVersion { get; private set; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06001928 RID: 6440 RVA: 0x00067BC0 File Offset: 0x00065DC0
		public static IEnumerable<string> CurrentTagList
		{
			get
			{
				return NKMContentsVersionManager.currentContentsTag;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06001929 RID: 6441 RVA: 0x00067BC7 File Offset: 0x00065DC7
		internal static HashSet<string> CurrentContentsTag
		{
			get
			{
				return NKMContentsVersionManager.currentContentsTag;
			}
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x00067BD0 File Offset: 0x00065DD0
		public static void LoadDefaultVersion()
		{
			using (NKMLua nkmlua = new NKMLua())
			{
				if (!nkmlua.LoadCommonPath("AB_SCRIPT", "LUA_CONTENTS_VERSION", true))
				{
					Log.ErrorAndExit("[ContentsVersion] loading file failed:LUA_CONTENTS_VERSION", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMContentsVersionManager.cs", 68);
				}
				string text = null;
				if (!nkmlua.GetData("ContentsVersion", ref text))
				{
					Log.ErrorAndExit("[ContentsVersion] loading default version failed:ContentsVersion", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMContentsVersionManager.cs", 74);
				}
				if (!NKMContentsVersionManager.SetCurrent(text))
				{
					Log.ErrorAndExit("[ContentsVersion] parsing version value failed:" + text, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMContentsVersionManager.cs", 79);
				}
			}
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x00067C64 File Offset: 0x00065E64
		public static bool HasTag(string tag)
		{
			if (string.IsNullOrWhiteSpace(tag))
			{
				throw new ArgumentException("[ContentsTag] param:" + tag);
			}
			return NKMContentsVersionManager.currentContentsTag.Contains(tag);
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x00067C8A File Offset: 0x00065E8A
		public static bool CheckIfValid(string tag)
		{
			return string.IsNullOrWhiteSpace(tag) || NKMContentsVersionManager.currentContentsTag.Contains(tag);
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00067CA1 File Offset: 0x00065EA1
		public static bool HasDFChangeTagType(DataFormatChangeTagType tagType)
		{
			return NKMContentsVersionManager.HasTag(NKMContentsVersionManager.DataFormatChangeTag[(int)tagType]);
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x00067CAF File Offset: 0x00065EAF
		public static bool HasCountryTag(CountryTagType countryTagType)
		{
			return NKMContentsVersionManager.HasTag(NKMContentsVersionManager.CountryTag[(int)countryTagType]);
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x00067CBD File Offset: 0x00065EBD
		public static bool SetCurrent(string literal)
		{
			NKMContentsVersionManager.CurrentVersion = NKMContentsVersion.Create(literal);
			if (NKMContentsVersionManager.CurrentVersion == null)
			{
				return false;
			}
			Log.Info(string.Format("[ContentsVersion] ContentsVersion:{0}", NKMContentsVersionManager.CurrentVersion), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMContentsVersionManager.cs", 119);
			return true;
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x00067CF5 File Offset: 0x00065EF5
		public static void AddTag(string tag)
		{
			if (string.IsNullOrEmpty(tag))
			{
				return;
			}
			if (!NKMContentsVersionManager.currentContentsTag.Contains(tag))
			{
				NKMContentsVersionManager.currentContentsTag.Add(tag);
			}
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x00067D1C File Offset: 0x00065F1C
		public static bool CheckContentsVersion(IList<string> listContentsTagIgnore, IList<string> listContentsTagAllow)
		{
			if (listContentsTagAllow.Count > 0)
			{
				if (listContentsTagAllow.All((string e) => !NKMContentsVersionManager.currentContentsTag.Contains(e)))
				{
					return false;
				}
			}
			return !listContentsTagIgnore.Any((string e) => NKMContentsVersionManager.currentContentsTag.Contains(e));
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x00067D88 File Offset: 0x00065F88
		public static bool CheckContentsVersion(NKMLua cNKMLua, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			string text = "listContentsTagAllow";
			string str;
			if (cNKMLua.OpenTable(text))
			{
				int num = 1;
				string item;
				while (cNKMLua.GetData(num, out item, string.Empty))
				{
					list.Add(item);
					num++;
				}
				cNKMLua.CloseTable();
			}
			else if (cNKMLua.GetData(text, out str, string.Empty))
			{
				NKMTempletError.Add("[ContentsVersion] 데이터 포맷 오류. 리스트 타입이어야 함. keyName:" + text + " value:" + str, file, line);
			}
			text = "listContentsTagIgnore";
			string str2;
			if (cNKMLua.OpenTable(text))
			{
				int num2 = 1;
				string item2;
				while (cNKMLua.GetData(num2, out item2, string.Empty))
				{
					list2.Add(item2);
					num2++;
				}
				cNKMLua.CloseTable();
			}
			else if (cNKMLua.GetData(text, out str2, string.Empty))
			{
				NKMTempletError.Add("[ContentsVersion] 데이터 포맷 오류. 리스트 타입이어야 함. keyName:" + text + " value:" + str2, file, line);
			}
			return NKMContentsVersionManager.CheckContentsVersion(list2, list);
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x00067E6E File Offset: 0x0006606E
		public static string Dump()
		{
			return string.Format("version:{0} #tags:{1}", NKMContentsVersionManager.CurrentVersion, NKMContentsVersionManager.currentContentsTag.Count);
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x00067E8E File Offset: 0x0006608E
		public static void PrintCurrentTagSet()
		{
			Log.Debug("[ContentsVersion] Print tag: " + string.Join(", ", NKMContentsVersionManager.currentContentsTag), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMContentsVersionManager.cs", 198);
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x00067EB8 File Offset: 0x000660B8
		public static void Drop()
		{
			NKMContentsVersionManager.currentContentsTag.Clear();
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x00067EC4 File Offset: 0x000660C4
		public static string GetCountryTag()
		{
			for (int i = 0; i < NKMContentsVersionManager.CountryTag.Length; i++)
			{
				string text = NKMContentsVersionManager.CountryTag[i];
				if (!string.IsNullOrWhiteSpace(text) && NKMContentsVersionManager.HasTag(text))
				{
					return text;
				}
			}
			return string.Empty;
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x00067F02 File Offset: 0x00066102
		public static void DeleteLocalTag()
		{
			Log.Info("DeleteLocalTags", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMContentsVersionManagerEx.cs", 27);
			PlayerPrefs.DeleteKey("LOCAL_SAVE_CONTENTS_TAG_KEY");
			PlayerPrefs.DeleteKey("LOCAL_SAVE_CONTENTS_TAG_LAST_SERVER_IP");
			PlayerPrefs.DeleteKey("LOCAL_SAVE_CONTENTS_TAG_LAST_SERVER_PORT");
		}

		// Token: 0x040011A0 RID: 4512
		public const string m_LOCAL_SAVE_CONTENTS_VERSION_KEY = "LOCAL_SAVE_CONTENTS_VERSION_KEY";

		// Token: 0x040011A1 RID: 4513
		public const string m_LOCAL_SAVE_CONTENTS_TAG_KEY = "LOCAL_SAVE_CONTENTS_TAG_KEY";

		// Token: 0x040011A2 RID: 4514
		public const string m_LOCAL_SAVE_CONTENTS_TAG_LAST_SERVER_IP = "LOCAL_SAVE_CONTENTS_TAG_LAST_SERVER_IP";

		// Token: 0x040011A3 RID: 4515
		public const string m_LOCAL_SAVE_CONTENTS_TAG_LAST_SERVER_PORT = "LOCAL_SAVE_CONTENTS_TAG_LAST_SERVER_PORT";

		// Token: 0x040011A4 RID: 4516
		public const string FileName = "LUA_CONTENTS_VERSION";

		// Token: 0x040011A5 RID: 4517
		public const string VersionVariableName = "ContentsVersion";

		// Token: 0x040011A6 RID: 4518
		private static readonly string[] CountryTag = new string[EnumUtil<CountryTagType>.Count];

		// Token: 0x040011A7 RID: 4519
		private static readonly string[] DataFormatChangeTag = new string[EnumUtil<DataFormatChangeTagType>.Count];

		// Token: 0x040011A8 RID: 4520
		private static HashSet<string> currentContentsTag = new HashSet<string>();
	}
}
