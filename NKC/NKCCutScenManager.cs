using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000665 RID: 1637
	public class NKCCutScenManager
	{
		// Token: 0x0600336A RID: 13162 RVA: 0x0010207A File Offset: 0x0010027A
		public static void Init()
		{
			NKCCutScenManager.ClearCacheData();
			NKCCutScenManager.m_dicNKCCutScenCharTempletByStrID.Clear();
			NKCCutScenManager.m_dicCutscenTypeByStrID.Clear();
			NKCCutScenManager.LoadFromLUA_CutscenFileList("LUA_CUTSCENE_FILE_LIST");
			NKCCutScenManager.LoadFromLUA_CutSceneChar("LUA_CUTSCENE_CHAR_TEMPLET");
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x001020AC File Offset: 0x001002AC
		public static NKCCutScenTemplet GetCutScenTemple(string _CutScenStrID)
		{
			if (_CutScenStrID == "")
			{
				return null;
			}
			if (NKCCutScenManager.m_dicNKCCutScenTempletByStrID.ContainsKey(_CutScenStrID))
			{
				return NKCCutScenManager.m_dicNKCCutScenTempletByStrID[_CutScenStrID];
			}
			if (NKCCutScenManager.LoadFromLUA_CutScene(_CutScenStrID))
			{
				if (!NKCCutScenManager.m_dicNKCCutScenTempletByStrID.ContainsKey(_CutScenStrID))
				{
					Log.Error("[" + _CutScenStrID + "] Cutscene LUA Compile Error!!!!", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCCutScenManager.cs", 742);
				}
				return NKCCutScenManager.m_dicNKCCutScenTempletByStrID[_CutScenStrID];
			}
			Log.Error("m_dicNKCCutScenTempletByStrID Cannot find Key or not loaded : " + _CutScenStrID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCCutScenManager.cs", 749);
			return null;
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x0010213C File Offset: 0x0010033C
		public static void ClearCacheData()
		{
			NKCCutScenManager.m_dicNKCCutScenTempletByID.Clear();
			NKCCutScenManager.m_dicNKCCutScenTempletByStrID.Clear();
		}

		// Token: 0x0600336D RID: 13165 RVA: 0x00102152 File Offset: 0x00100352
		public static NKCCutScenCharTemplet GetCutScenCharTempletByStrID(string _CharStrID)
		{
			if (NKCCutScenManager.m_dicNKCCutScenCharTempletByStrID.ContainsKey(_CharStrID))
			{
				return NKCCutScenManager.m_dicNKCCutScenCharTempletByStrID[_CharStrID];
			}
			Log.Error("m_dicNKCCutScenCharTempletByStrID Cannot find Key: " + _CharStrID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCCutScenManager.cs", 787);
			return null;
		}

		// Token: 0x0600336E RID: 13166 RVA: 0x00102188 File Offset: 0x00100388
		public static List<string> GetCutScenTempletStrIDList(HashSet<NKC_CUTSCEN_TYPE> setExceptNKC_CUTSCEN_TYPE)
		{
			List<string> list = new List<string>(NKCCutScenManager.m_dicCutscenTypeByStrID.Keys);
			if (setExceptNKC_CUTSCEN_TYPE != null && setExceptNKC_CUTSCEN_TYPE.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (setExceptNKC_CUTSCEN_TYPE.Contains(NKCCutScenManager.m_dicCutscenTypeByStrID[list[i]]))
					{
						list.RemoveAt(i);
						i--;
					}
				}
			}
			return list;
		}

		// Token: 0x0600336F RID: 13167 RVA: 0x001021E8 File Offset: 0x001003E8
		public static void GetCutScenTempletStrIDListFilteringByStr(List<string> _listCutScenTempletStrID, string strFilter)
		{
			for (int i = 0; i < _listCutScenTempletStrID.Count; i++)
			{
				if (!string.IsNullOrEmpty(strFilter) && !_listCutScenTempletStrID[i].Contains(strFilter))
				{
					_listCutScenTempletStrID.RemoveAt(i);
					i--;
				}
			}
		}

		// Token: 0x06003370 RID: 13168 RVA: 0x00102228 File Offset: 0x00100428
		public static List<Tuple<string, string>> GetSelectionRoutes(int id, int index)
		{
			if (NKCCutScenManager.m_dicNKCCutScenTempletByID.ContainsKey(id))
			{
				NKCCutScenTemplet nkccutScenTemplet = NKCCutScenManager.m_dicNKCCutScenTempletByID[id];
				if (nkccutScenTemplet.m_listCutTemplet[index].m_Action != NKCCutTemplet.eCutsceneAction.SELECT)
				{
					Debug.LogError("로직 오류 : 선택지가 아닌 곳에서 선택지 목록을 요청");
					return null;
				}
				List<Tuple<string, string>> list = new List<Tuple<string, string>>();
				for (int i = index; i < nkccutScenTemplet.m_listCutTemplet.Count; i++)
				{
					NKCCutTemplet nkccutTemplet = nkccutScenTemplet.m_listCutTemplet[i];
					if (nkccutTemplet.m_Action != NKCCutTemplet.eCutsceneAction.SELECT)
					{
						return list;
					}
					list.Add(new Tuple<string, string>(nkccutTemplet.m_ActionStrKey, nkccutTemplet.m_Talk));
				}
			}
			return null;
		}

		// Token: 0x06003371 RID: 13169 RVA: 0x001022BC File Offset: 0x001004BC
		public static bool LoadFromLUA_CutSceneChar(string fileName)
		{
			NKMLua nkmlua = new NKMLua();
			if (nkmlua.LoadCommonPath("AB_SCRIPT", fileName, true) && nkmlua.OpenTable("m_dicCutsceneCharTempletStrID"))
			{
				int num = 1;
				while (nkmlua.OpenTable(num))
				{
					NKCCutScenCharTemplet nkccutScenCharTemplet = new NKCCutScenCharTemplet();
					if (nkccutScenCharTemplet.LoadFromLUA(nkmlua))
					{
						if (NKCCutScenManager.m_dicNKCCutScenCharTempletByStrID.ContainsKey(nkccutScenCharTemplet.m_CharStrID))
						{
							Log.Error("m_dicCutsceneCharTempletStrID Duplicate Key: " + nkccutScenCharTemplet.m_CharStrID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCCutScenManager.cs", 881);
							return false;
						}
						NKCCutScenManager.m_dicNKCCutScenCharTempletByStrID.Add(nkccutScenCharTemplet.m_CharStrID, nkccutScenCharTemplet);
					}
					num++;
					nkmlua.CloseTable();
				}
				nkmlua.CloseTable();
			}
			nkmlua.LuaClose();
			return true;
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x0010236C File Offset: 0x0010056C
		public static bool LoadFromLUA_CutscenFileList(string fileName)
		{
			NKCCutScenManager.m_dicCutscenTypeByStrID.Clear();
			NKMLua nkmlua = new NKMLua();
			if (nkmlua.LoadCommonPath("AB_SCRIPT", fileName, true) && nkmlua.OpenTable("m_hsCutscenFileName"))
			{
				int num = 1;
				while (nkmlua.OpenTable(num))
				{
					string text = "";
					NKC_CUTSCEN_TYPE value = NKC_CUTSCEN_TYPE.NCT_ETC;
					nkmlua.GetData("m_CutScenFile", ref text);
					nkmlua.GetData<NKC_CUTSCEN_TYPE>("m_CutScenType", ref value);
					if (NKCCutScenManager.m_dicCutscenTypeByStrID.ContainsKey(text))
					{
						Log.Error("Duplicate CutscenFileList File Name: " + text, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCCutScenManager.cs", 919);
					}
					else
					{
						NKCCutScenManager.m_dicCutscenTypeByStrID.Add(text, value);
					}
					num++;
					nkmlua.CloseTable();
				}
			}
			nkmlua.LuaClose();
			return true;
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x00102420 File Offset: 0x00100620
		public static bool LoadFromLUA_CutScene(string fileName)
		{
			if (!NKCCutScenManager.m_dicCutscenTypeByStrID.ContainsKey(fileName))
			{
				return false;
			}
			if (NKCCutScenManager.m_dicNKCCutScenTempletByStrID.ContainsKey(fileName))
			{
				return true;
			}
			bool result = false;
			NKMLua nkmlua = new NKMLua();
			if (nkmlua.LoadCommonPath("AB_SCRIPT_CUTSCENE", fileName, true))
			{
				if (nkmlua.OpenTable("m_dicNKCCutScenTempletByID"))
				{
					int num = 1;
					while (nkmlua.OpenTable(num))
					{
						NKCCutScenTemplet nkccutScenTemplet = new NKCCutScenTemplet();
						if (nkccutScenTemplet.LoadFromLUA(nkmlua, num))
						{
							if (!NKCCutScenManager.m_dicNKCCutScenTempletByID.ContainsKey(nkccutScenTemplet.m_CutScenID))
							{
								NKCCutScenManager.m_dicNKCCutScenTempletByID.Add(nkccutScenTemplet.m_CutScenID, nkccutScenTemplet);
								if (!NKCCutScenManager.m_dicNKCCutScenTempletByStrID.ContainsKey(nkccutScenTemplet.m_CutScenStrID))
								{
									result = true;
									NKCCutScenManager.m_dicNKCCutScenTempletByStrID.Add(nkccutScenTemplet.m_CutScenStrID, nkccutScenTemplet);
								}
							}
							else
							{
								NKCCutScenManager.m_dicNKCCutScenTempletByID[nkccutScenTemplet.m_CutScenID].AddCutTemplet(nkccutScenTemplet);
							}
						}
						num++;
						nkmlua.CloseTable();
					}
					nkmlua.CloseTable();
				}
				else
				{
					result = false;
				}
			}
			else
			{
				result = false;
			}
			nkmlua.LuaClose();
			return result;
		}

		// Token: 0x0400323C RID: 12860
		public const string USER_NICKNAME = "<usernickname>";

		// Token: 0x0400323D RID: 12861
		private static Dictionary<int, NKCCutScenTemplet> m_dicNKCCutScenTempletByID = new Dictionary<int, NKCCutScenTemplet>();

		// Token: 0x0400323E RID: 12862
		private static Dictionary<string, NKCCutScenTemplet> m_dicNKCCutScenTempletByStrID = new Dictionary<string, NKCCutScenTemplet>();

		// Token: 0x0400323F RID: 12863
		private static Dictionary<string, NKCCutScenCharTemplet> m_dicNKCCutScenCharTempletByStrID = new Dictionary<string, NKCCutScenCharTemplet>();

		// Token: 0x04003240 RID: 12864
		private static Dictionary<string, NKC_CUTSCEN_TYPE> m_dicCutscenTypeByStrID = new Dictionary<string, NKC_CUTSCEN_TYPE>();
	}
}
