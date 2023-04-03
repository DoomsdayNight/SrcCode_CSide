using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200065B RID: 1627
	public static class NKCContentsVersionManager
	{
		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06003330 RID: 13104 RVA: 0x0010013A File Offset: 0x000FE33A
		private static HashSet<string> CurrentContentsTag
		{
			get
			{
				return NKMContentsVersionManager.CurrentContentsTag;
			}
		}

		// Token: 0x06003331 RID: 13105 RVA: 0x00100144 File Offset: 0x000FE344
		public static void TryRecoverTag()
		{
			if (NKCContentsVersionManager.CurrentContentsTag.Count > 0)
			{
				Log.Debug("[ContentsVersion] CurrentContentsTag exist. Skip recovery.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCContentsVersionManager.cs", 22);
				return;
			}
			string @string = PlayerPrefs.GetString("LOCAL_SAVE_CONTENTS_TAG_KEY");
			if (!string.IsNullOrEmpty(@string))
			{
				Log.Debug("[ContentsVersion] Local CurrentContentsTag exist. Get tag from local data.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCContentsVersionManager.cs", 30);
				NKCContentsVersionManager.SetTagList(@string);
				return;
			}
			TextAsset textAsset = Resources.Load<TextAsset>("LUA_DEFAULT_CONTENTS_TAG");
			if (textAsset == null)
			{
				Log.ErrorAndExit("[ContentsVersion] Cannot find file :LUA_DEFAULT_CONTENTS_TAG", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCContentsVersionManager.cs", 39);
				return;
			}
			string str = textAsset.ToString();
			using (NKMLua nkmlua = new NKMLua())
			{
				if (!nkmlua.DoString(str))
				{
					Log.ErrorAndExit("[ContentsVersion] loading file failed:LUA_DEFAULT_CONTENTS_TAG", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCContentsVersionManager.cs", 50);
				}
				Log.Debug("TagVariableName : " + NKCContentsVersionManager.TagVariableName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCContentsVersionManager.cs", 53);
				List<string> list = new List<string>();
				if (!nkmlua.GetData(NKCContentsVersionManager.TagVariableName, list))
				{
					Log.ErrorAndExit("[ContentsVersion] loading default tag failed:" + NKCContentsVersionManager.TagVariableName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCContentsVersionManager.cs", 58);
				}
				foreach (string text in list)
				{
					NKMContentsVersionManager.AddTag(text);
					Log.Debug("NKMContentsVersionManager AddTag : " + text, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCContentsVersionManager.cs", 64);
					NKCContentsVersionManager.s_DefaultTagLoaded = true;
				}
			}
		}

		// Token: 0x06003332 RID: 13106 RVA: 0x001002AC File Offset: 0x000FE4AC
		public static void SaveTagToLocal()
		{
			string value = string.Join(";", NKCContentsVersionManager.CurrentContentsTag);
			PlayerPrefs.SetString("LOCAL_SAVE_CONTENTS_TAG_KEY", value);
			PlayerPrefs.SetString("LOCAL_SAVE_CONTENTS_TAG_LAST_SERVER_IP", NKCConnectionInfo.ServiceIP);
			PlayerPrefs.SetInt("LOCAL_SAVE_CONTENTS_TAG_LAST_SERVER_PORT", NKCConnectionInfo.ServicePort);
			Log.Info(string.Format("SaveTagInfo IP[{0}] PORT[{1}]", NKCConnectionInfo.ServiceIP, NKCConnectionInfo.ServicePort), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCContentsVersionManager.cs", 77);
		}

		// Token: 0x06003333 RID: 13107 RVA: 0x00100318 File Offset: 0x000FE518
		public static void SetTagList(IReadOnlyList<string> tagList)
		{
			NKCContentsVersionManager.CurrentContentsTag.Clear();
			foreach (string tag in tagList)
			{
				NKMContentsVersionManager.AddTag(tag);
			}
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x00100368 File Offset: 0x000FE568
		public static bool CheckSameTagList(IReadOnlyList<string> tagList)
		{
			if (NKCContentsVersionManager.CurrentContentsTag.Count != tagList.Count)
			{
				Log.Warn("client tagList:" + string.Join(", ", NKCContentsVersionManager.CurrentContentsTag), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCContentsVersionManager.cs", 93);
				Log.Warn("server tagList:" + string.Join(", ", tagList), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCContentsVersionManager.cs", 94);
				return false;
			}
			using (IEnumerator<string> enumerator = tagList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!NKMContentsVersionManager.HasTag(enumerator.Current))
					{
						Log.Warn("client tagList:" + string.Join(", ", NKCContentsVersionManager.CurrentContentsTag), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCContentsVersionManager.cs", 102);
						Log.Warn("server tagList:" + string.Join(", ", tagList), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCContentsVersionManager.cs", 103);
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x00100458 File Offset: 0x000FE658
		private static void SetTagList(string tagList)
		{
			NKCContentsVersionManager.CurrentContentsTag.Clear();
			string[] array = tagList.Split(new char[]
			{
				';'
			});
			for (int i = 0; i < array.Length; i++)
			{
				NKMContentsVersionManager.AddTag(array[i]);
			}
		}

		// Token: 0x040031B6 RID: 12726
		public const string LUA_DEFAULT_CONTENTS_TAG = "LUA_DEFAULT_CONTENTS_TAG";

		// Token: 0x040031B7 RID: 12727
		public static string TagVariableName = "KOR";

		// Token: 0x040031B8 RID: 12728
		public static bool s_DefaultTagLoaded = false;
	}
}
