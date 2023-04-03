using System;
using System.Collections.Generic;
using Cs.Logging;
using NKC.Templet;
using NKC.Templet.Base;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000654 RID: 1620
	public class NKCCollectionManager
	{
		// Token: 0x060032A7 RID: 12967 RVA: 0x000FBE17 File Offset: 0x000FA017
		public static Dictionary<int, NKCCollectionIllustTemplet> GetIllustData()
		{
			return NKCCollectionManager.m_dicNKCCollectionIllustTemplet;
		}

		// Token: 0x060032A8 RID: 12968 RVA: 0x000FBE1E File Offset: 0x000FA01E
		public static Dictionary<int, NKCCollectionUnitTemplet> GetUnitData()
		{
			return NKCCollectionManager.m_dicNKCCollectionUnitTemplet;
		}

		// Token: 0x060032A9 RID: 12969 RVA: 0x000FBE25 File Offset: 0x000FA025
		public static Dictionary<int, NKCCollectionTagTemplet> GetTagData()
		{
			return NKCCollectionManager.m_dicNKCCollectionTagTemplet;
		}

		// Token: 0x060032AA RID: 12970 RVA: 0x000FBE2C File Offset: 0x000FA02C
		public static Dictionary<int, storyUnlockData> GetStoryData()
		{
			return NKCCollectionManager.m_dicNKCCollectionStoryTemplet;
		}

		// Token: 0x060032AB RID: 12971 RVA: 0x000FBE33 File Offset: 0x000FA033
		public static Dictionary<int, List<int>> GetEpiSodeStageIdData()
		{
			return NKCCollectionManager.m_dicEpisodeStageIdData;
		}

		// Token: 0x060032AC RID: 12972 RVA: 0x000FBE3A File Offset: 0x000FA03A
		public static NKCCollectionManager.COLLECTION_STORY_CATEGORY GetCollectionStoryCategory(EPISODE_CATEGORY category)
		{
			if (category == EPISODE_CATEGORY.EC_MAINSTREAM)
			{
				return NKCCollectionManager.COLLECTION_STORY_CATEGORY.MAINSTREAM;
			}
			if (category != EPISODE_CATEGORY.EC_SIDESTORY)
			{
				if (category != EPISODE_CATEGORY.EC_EVENT)
				{
				}
				return NKCCollectionManager.COLLECTION_STORY_CATEGORY.EVENT;
			}
			return NKCCollectionManager.COLLECTION_STORY_CATEGORY.SIDESTORY;
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x000FBE50 File Offset: 0x000FA050
		public static string GetTagTitle(short tagType)
		{
			NKCCollectionTagTemplet nkccollectionTagTemplet = NKCCollectionManager.Find<NKCCollectionTagTemplet>((int)tagType, ref NKCCollectionManager.m_dicNKCCollectionTagTemplet);
			if (nkccollectionTagTemplet != null)
			{
				return nkccollectionTagTemplet.GetTagName();
			}
			return "";
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x000FBE78 File Offset: 0x000FA078
		public static void SetVote(int unitID, short tagType, int tagCount, bool voted)
		{
			if (!NKCCollectionManager.m_dic_UnitTagData.ContainsKey(unitID))
			{
				Debug.Log(string.Format("NKCCollectionManager.SetVote() - invaild unit id {0}", unitID));
				return;
			}
			NKCUnitTagData nkcunitTagData = NKCCollectionManager.m_dic_UnitTagData[unitID].Find((NKCUnitTagData x) => x.TagType == tagType);
			nkcunitTagData.VoteCount = tagCount;
			nkcunitTagData.Voted = voted;
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x000FBEE0 File Offset: 0x000FA0E0
		public static bool IsVoted(int unitID, short tagType)
		{
			if (!NKCCollectionManager.m_dic_UnitTagData.ContainsKey(unitID))
			{
				Debug.Log(string.Format("NKCCollectionManager.IsVoted() - invaild unit id {0}", unitID));
				return false;
			}
			return NKCCollectionManager.m_dic_UnitTagData[unitID].Find((NKCUnitTagData x) => x.TagType == tagType).Voted;
		}

		// Token: 0x060032B0 RID: 12976 RVA: 0x000FBF3F File Offset: 0x000FA13F
		public static List<NKCUnitTagData> GetUnitTagData(int unitID)
		{
			if (NKCCollectionManager.m_dic_UnitTagData.ContainsKey(unitID))
			{
				return NKCCollectionManager.m_dic_UnitTagData[unitID];
			}
			return null;
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x000FBF5C File Offset: 0x000FA15C
		public static int GetUnitVoteCount(int unitID, short type)
		{
			if (NKCCollectionManager.m_dic_UnitTagData.ContainsKey(unitID))
			{
				return NKCCollectionManager.m_dic_UnitTagData[unitID].Find((NKCUnitTagData x) => x.TagType == type).VoteCount;
			}
			return 0;
		}

		// Token: 0x060032B2 RID: 12978 RVA: 0x000FBFA8 File Offset: 0x000FA1A8
		public static void SetUnitTagData(int unitID, List<NKCUnitTagData> lst)
		{
			if (lst.Count > 1)
			{
				lst.Sort((NKCUnitTagData a, NKCUnitTagData b) => a.VoteCount.CompareTo(b.VoteCount));
			}
			List<NKCUnitTagData> list = new List<NKCUnitTagData>();
			using (Dictionary<int, NKCCollectionTagTemplet>.Enumerator enumerator = NKCCollectionManager.m_dicNKCCollectionTagTemplet.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, NKCCollectionTagTemplet> tag = enumerator.Current;
					NKCUnitTagData nkcunitTagData = lst.Find((NKCUnitTagData x) => x.TagType == tag.Value.m_TagOrder);
					if (nkcunitTagData != null)
					{
						list.Add(nkcunitTagData);
					}
					else
					{
						list.Add(new NKCUnitTagData(tag.Value.m_TagOrder, false, 0, false));
					}
				}
			}
			if (NKCCollectionManager.m_dic_UnitTagData.ContainsKey(unitID))
			{
				NKCCollectionManager.m_dic_UnitTagData[unitID] = list;
				return;
			}
			NKCCollectionManager.m_dic_UnitTagData.Add(unitID, list);
		}

		// Token: 0x060032B3 RID: 12979 RVA: 0x000FC098 File Offset: 0x000FA298
		public static List<int> GetUnitList(NKM_UNIT_TYPE type)
		{
			switch (type)
			{
			case NKM_UNIT_TYPE.NUT_NORMAL:
				return NKCCollectionManager.m_lstCollectionUnit;
			case NKM_UNIT_TYPE.NUT_SHIP:
				return NKCCollectionManager.m_lstCollectionShip;
			case NKM_UNIT_TYPE.NUT_OPERATOR:
				return NKCCollectionManager.m_lstCollectionOperator;
			default:
				return new List<int>();
			}
		}

		// Token: 0x060032B5 RID: 12981 RVA: 0x000FC0D0 File Offset: 0x000FA2D0
		public static T Find<T>(int key, ref Dictionary<int, T> data)
		{
			T result;
			data.TryGetValue(key, out result);
			return result;
		}

		// Token: 0x060032B6 RID: 12982 RVA: 0x000FC0EC File Offset: 0x000FA2EC
		public static void Init()
		{
			if (NKCCollectionManager.m_bPreLoading)
			{
				return;
			}
			NKCCollectionManager.m_dicNKCCollectionUnitTemplet.Clear();
			NKCCollectionManager.m_dicNKCCollectionTagTemplet.Clear();
			NKCCollectionManager.m_dicNKCCollectionIllustTemplet.Clear();
			NKCCollectionManager.m_dicNKCCollectionStoryTemplet.Clear();
			NKCCollectionManager.m_dicEpisodeStageIdData.Clear();
			NKCCollectionManager.m_lstSkinTemplet.Clear();
			NKCCollectionManager.DivideCollectionUnitData();
			NKCCollectionManager.Load<NKCCollectionTagTemplet>("AB_SCRIPT", "LUA_COLLECTION_TAG_TEMPLET", "COLLECTION_TAG_TEMPLET", new Func<NKMLua, NKCCollectionTagTemplet>(NKCCollectionTagTemplet.LoadFromLUA), ref NKCCollectionManager.m_dicNKCCollectionTagTemplet, false);
			NKCCollectionManager.Load<TempNKCCollectionIllustTemplet>("AB_SCRIPT", "LUA_COLLECTION_ILLUST_TEMPLET", "COLLECTION_ILLUST_TEMPLET", new Func<NKMLua, TempNKCCollectionIllustTemplet>(TempNKCCollectionIllustTemplet.LoadFromLUA), ref NKCCollectionManager.m_dicNKCCollectionIllustTemplet);
			NKCCollectionManager.Load<NKMCollectionStoryTemplet>("AB_SCRIPT", "LUA_COLLECTION_CUTSCENE_TEMPLET", "COLLECTION_CUTSCENE_TEMPLET", new Func<NKMLua, NKMCollectionStoryTemplet>(NKMCollectionStoryTemplet.LoadFromLUA), ref NKCCollectionManager.m_dicNKCCollectionStoryTemplet, ref NKCCollectionManager.m_lstSkinTemplet, true);
			NKCCollectionManager.m_bPreLoading = true;
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x000FC1C0 File Offset: 0x000FA3C0
		private static void DivideCollectionUnitData()
		{
			NKCCollectionManager.Load<NKCCollectionUnitTemplet>("AB_SCRIPT", "LUA_COLLECTION_UNIT_TEMPLET", "COLLECTION_UNIT_TEMPLET", new Func<NKMLua, NKCCollectionUnitTemplet>(NKCCollectionUnitTemplet.LoadFromLUA), ref NKCCollectionManager.m_dicNKCCollectionUnitTemplet, true);
			NKCCollectionManager.m_lstCollectionUnit.Clear();
			NKCCollectionManager.m_lstCollectionShip.Clear();
			NKCCollectionManager.m_lstCollectionOperator.Clear();
			foreach (KeyValuePair<int, NKCCollectionUnitTemplet> keyValuePair in NKCCollectionManager.m_dicNKCCollectionUnitTemplet)
			{
				NKCCollectionUnitTemplet value = keyValuePair.Value;
				switch (value.m_NKM_UNIT_TYPE)
				{
				case NKM_UNIT_TYPE.NUT_NORMAL:
					NKCCollectionManager.m_lstCollectionUnit.Add(value.m_UnitID);
					break;
				case NKM_UNIT_TYPE.NUT_SHIP:
					NKCCollectionManager.m_lstCollectionShip.Add(value.m_UnitID);
					break;
				case NKM_UNIT_TYPE.NUT_OPERATOR:
					NKCCollectionManager.m_lstCollectionOperator.Add(value.m_UnitID);
					break;
				}
			}
		}

		// Token: 0x060032B8 RID: 12984 RVA: 0x000FC2A8 File Offset: 0x000FA4A8
		public static NKCCollectionIllustTemplet GetIllustTemplet(int key)
		{
			return NKCCollectionManager.Find<NKCCollectionIllustTemplet>(key, ref NKCCollectionManager.m_dicNKCCollectionIllustTemplet);
		}

		// Token: 0x060032B9 RID: 12985 RVA: 0x000FC2B5 File Offset: 0x000FA4B5
		public static NKCCollectionUnitTemplet GetUnitTemplet(int key)
		{
			return NKCCollectionManager.Find<NKCCollectionUnitTemplet>(key, ref NKCCollectionManager.m_dicNKCCollectionUnitTemplet);
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x000FC2C4 File Offset: 0x000FA4C4
		public static string GetEmployeeNumber(int key)
		{
			string result = "";
			NKCCollectionUnitTemplet nkccollectionUnitTemplet = NKCCollectionManager.Find<NKCCollectionUnitTemplet>(key, ref NKCCollectionManager.m_dicNKCCollectionUnitTemplet);
			if (nkccollectionUnitTemplet != null)
			{
				if (nkccollectionUnitTemplet.Idx >= 100)
				{
					result = nkccollectionUnitTemplet.Idx.ToString();
				}
				else if (nkccollectionUnitTemplet.Idx >= 10)
				{
					result = "0" + nkccollectionUnitTemplet.Idx.ToString();
				}
				else
				{
					result = "00" + nkccollectionUnitTemplet.Idx.ToString();
				}
			}
			return result;
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x000FC337 File Offset: 0x000FA537
		public static NKCCollectionTagTemplet GetTagTemplet(int idx)
		{
			return NKCCollectionManager.Find<NKCCollectionTagTemplet>(idx, ref NKCCollectionManager.m_dicNKCCollectionTagTemplet);
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x000FC344 File Offset: 0x000FA544
		public static List<storyUnlockData> GetSkinStoryData()
		{
			return NKCCollectionManager.m_lstSkinTemplet;
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x000FC34B File Offset: 0x000FA54B
		public static string GetVoiceActorName(int unitId)
		{
			return NKCVoiceActorNameTemplet.FindActorName(NKCCollectionManager.GetUnitTemplet(unitId));
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x000FC358 File Offset: 0x000FA558
		private static void Load<T>(string assetName, string fileName, string tableName, Func<NKMLua, T> factory, ref Dictionary<int, T> data, bool bTempletNullAllowed = false) where T : INKCTemplet
		{
			string name = typeof(T).Name;
			NKMLua nkmlua = new NKMLua();
			if (!nkmlua.LoadCommonPath(assetName, fileName, true))
			{
				Log.ErrorAndExit(string.Concat(new string[]
				{
					"[",
					name,
					"] lua file loading fail. assetName:",
					assetName,
					" fileName:",
					fileName
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCCollectionManager.cs", 527);
			}
			if (!nkmlua.OpenTable(tableName))
			{
				Log.ErrorAndExit(string.Concat(new string[]
				{
					"[",
					name,
					"] lua table open fail. fileName:",
					fileName,
					" tableName:",
					tableName
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCCollectionManager.cs", 532);
			}
			int num = 1;
			while (nkmlua.OpenTable(num))
			{
				T t = factory(nkmlua);
				if (!bTempletNullAllowed)
				{
					if (t == null)
					{
						Log.ErrorAndExit(string.Format("[{0}] data load fail. fileName:{1} tableName:{2} index:{3}", new object[]
						{
							name,
							fileName,
							tableName,
							num
						}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCCollectionManager.cs", 544);
						break;
					}
				}
				else if (t == null)
				{
					num++;
					nkmlua.CloseTable();
					continue;
				}
				if (data.ContainsKey(t.Key))
				{
					Log.ErrorAndExit(string.Format("[{0}] Table contains duplicate key. tableName:{1} key:{2}", name, tableName, t.Key), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCCollectionManager.cs", 563);
					break;
				}
				data.Add(t.Key, t);
				num++;
				nkmlua.CloseTable();
			}
			nkmlua.CloseTable();
			nkmlua.LuaClose();
		}

		// Token: 0x060032BF RID: 12991 RVA: 0x000FC4F4 File Offset: 0x000FA6F4
		private static void Load<T>(string assetName, string fileName, string tableName, Func<NKMLua, T> factory, ref Dictionary<int, NKCCollectionIllustTemplet> data) where T : INKCTemplet
		{
			string name = typeof(T).Name;
			NKMLua nkmlua = new NKMLua();
			if (!nkmlua.LoadCommonPath(assetName, fileName, true))
			{
				Log.ErrorAndExit(string.Concat(new string[]
				{
					"[",
					name,
					"] lua file loading fail. assetName:",
					assetName,
					" fileName:",
					fileName
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCCollectionManager.cs", 583);
			}
			if (!nkmlua.OpenTable(tableName))
			{
				Log.ErrorAndExit(string.Concat(new string[]
				{
					"[",
					name,
					"] lua table open fail. fileName:",
					fileName,
					" tableName:",
					tableName
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCCollectionManager.cs", 588);
			}
			int num = 1;
			while (nkmlua.OpenTable(num))
			{
				T t = factory(nkmlua);
				if (t == null)
				{
					Log.ErrorAndExit(string.Format("[{0}] data load fail. fileName:{1} tableName:{2} index:{3}", new object[]
					{
						name,
						fileName,
						tableName,
						num
					}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCCollectionManager.cs", 597);
					break;
				}
				if (data.ContainsKey(t.Key))
				{
					NKCCollectionIllustTemplet nkccollectionIllustTemplet = data[t.Key];
					TempNKCCollectionIllustTemplet tempNKCCollectionIllustTemplet = t as TempNKCCollectionIllustTemplet;
					if (tempNKCCollectionIllustTemplet != null)
					{
						Dictionary<int, NKCCollectionIllustData> dicIllustData = nkccollectionIllustTemplet.m_dicIllustData;
						if (dicIllustData.ContainsKey(tempNKCCollectionIllustTemplet.m_BGGroupID))
						{
							NKCCollectionIllustData nkccollectionIllustData = dicIllustData[tempNKCCollectionIllustTemplet.m_BGGroupID];
							if (nkccollectionIllustData != null)
							{
								NKCIllustFileData item = new NKCIllustFileData(tempNKCCollectionIllustTemplet.m_BGThumbnailFileName, tempNKCCollectionIllustTemplet.m_BGFileName, tempNKCCollectionIllustTemplet.m_GameObjectBGAniName);
								nkccollectionIllustData.m_FileData.Add(item);
							}
						}
						else
						{
							NKCCollectionIllustData value = new NKCCollectionIllustData(tempNKCCollectionIllustTemplet.GetBGGroupTitle(), tempNKCCollectionIllustTemplet.GetBGGroupText(), tempNKCCollectionIllustTemplet.m_BGThumbnailFileName, tempNKCCollectionIllustTemplet.m_BGFileName, tempNKCCollectionIllustTemplet.m_GameObjectBGAniName, tempNKCCollectionIllustTemplet.m_UnlockReqType, tempNKCCollectionIllustTemplet.m_UnlockReqValue);
							dicIllustData.Add(tempNKCCollectionIllustTemplet.m_BGGroupID, value);
						}
					}
				}
				else
				{
					TempNKCCollectionIllustTemplet tempNKCCollectionIllustTemplet2 = t as TempNKCCollectionIllustTemplet;
					if (tempNKCCollectionIllustTemplet2 == null)
					{
						Log.ErrorAndExit(string.Format("[{0}] Load Faile - Table Data. tableName:{1} key:{2}", name, tableName, t.Key), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCCollectionManager.cs", 630);
						break;
					}
					NKCCollectionIllustData illustData = new NKCCollectionIllustData(tempNKCCollectionIllustTemplet2.GetBGGroupTitle(), tempNKCCollectionIllustTemplet2.GetBGGroupText(), tempNKCCollectionIllustTemplet2.m_BGThumbnailFileName, tempNKCCollectionIllustTemplet2.m_BGFileName, tempNKCCollectionIllustTemplet2.m_GameObjectBGAniName, tempNKCCollectionIllustTemplet2.m_UnlockReqType, tempNKCCollectionIllustTemplet2.m_UnlockReqValue);
					NKCCollectionIllustTemplet value2 = new NKCCollectionIllustTemplet(tempNKCCollectionIllustTemplet2.m_CategoryID, tempNKCCollectionIllustTemplet2.m_CategoryTitle, tempNKCCollectionIllustTemplet2.m_CategorySubTitle, tempNKCCollectionIllustTemplet2.m_BGGroupID, illustData);
					data.Add(tempNKCCollectionIllustTemplet2.m_CategoryID, value2);
				}
				num++;
				nkmlua.CloseTable();
			}
			nkmlua.CloseTable();
			nkmlua.LuaClose();
		}

		// Token: 0x060032C0 RID: 12992 RVA: 0x000FC7C0 File Offset: 0x000FA9C0
		private static void Load<T>(string assetName, string fileName, string tableName, Func<NKMLua, T> factory, ref Dictionary<int, storyUnlockData> data, ref List<storyUnlockData> lstShopCutScene, bool bTempletNullAllowed = false) where T : INKMTemplet
		{
			string name = typeof(T).Name;
			NKMLua nkmlua = new NKMLua();
			if (!nkmlua.LoadCommonPath(assetName, fileName, true))
			{
				Log.ErrorAndExit(string.Concat(new string[]
				{
					"[",
					name,
					"] lua file loading fail. assetName:",
					assetName,
					" fileName:",
					fileName
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCCollectionManager.cs", 651);
			}
			if (!nkmlua.OpenTable(tableName))
			{
				Log.ErrorAndExit(string.Concat(new string[]
				{
					"[",
					name,
					"] lua table open fail. fileName:",
					fileName,
					" tableName:",
					tableName
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCCollectionManager.cs", 656);
			}
			int num = 1;
			while (nkmlua.OpenTable(num))
			{
				T t = factory(nkmlua);
				if (!bTempletNullAllowed)
				{
					if (t == null)
					{
						Log.ErrorAndExit(string.Format("[{0}] data load fail. fileName:{1} tableName:{2} index:{3}", new object[]
						{
							name,
							fileName,
							tableName,
							num
						}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCCollectionManager.cs", 668);
						break;
					}
				}
				else if (t == null)
				{
					num++;
					nkmlua.CloseTable();
					continue;
				}
				NKMCollectionStoryTemplet nkmcollectionStoryTemplet = t as NKMCollectionStoryTemplet;
				if (nkmcollectionStoryTemplet == null)
				{
					Log.ErrorAndExit(string.Format("[{0}] Load Faile - Table Data. tableName:{1} key:{2}", name, tableName, t.Key), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCCollectionManager.cs", 685);
				}
				else if (nkmcollectionStoryTemplet.m_StageID == 0)
				{
					lstShopCutScene.Add(new storyUnlockData(nkmcollectionStoryTemplet.m_UnlockReqList, nkmcollectionStoryTemplet.m_EPCategory, nkmcollectionStoryTemplet.m_EpisodeID, nkmcollectionStoryTemplet.m_ActID, nkmcollectionStoryTemplet.m_StageID));
				}
				else
				{
					data.Add(nkmcollectionStoryTemplet.m_StageID, new storyUnlockData(nkmcollectionStoryTemplet.m_UnlockReqList, nkmcollectionStoryTemplet.m_EPCategory, nkmcollectionStoryTemplet.m_EpisodeID, nkmcollectionStoryTemplet.m_ActID, nkmcollectionStoryTemplet.m_StageID));
					if (!NKCCollectionManager.m_dicEpisodeStageIdData.ContainsKey(nkmcollectionStoryTemplet.m_EpisodeID))
					{
						NKCCollectionManager.m_dicEpisodeStageIdData.Add(nkmcollectionStoryTemplet.m_EpisodeID, new List<int>());
					}
					NKCCollectionManager.m_dicEpisodeStageIdData[nkmcollectionStoryTemplet.m_EpisodeID].Add(nkmcollectionStoryTemplet.m_StageID);
				}
				num++;
				nkmlua.CloseTable();
			}
			nkmlua.CloseTable();
			nkmlua.LuaClose();
		}

		// Token: 0x0400318C RID: 12684
		private static Dictionary<int, NKCCollectionIllustTemplet> m_dicNKCCollectionIllustTemplet = new Dictionary<int, NKCCollectionIllustTemplet>(100);

		// Token: 0x0400318D RID: 12685
		private static Dictionary<int, NKCCollectionUnitTemplet> m_dicNKCCollectionUnitTemplet = new Dictionary<int, NKCCollectionUnitTemplet>(300);

		// Token: 0x0400318E RID: 12686
		private static Dictionary<int, NKCCollectionTagTemplet> m_dicNKCCollectionTagTemplet = new Dictionary<int, NKCCollectionTagTemplet>(100);

		// Token: 0x0400318F RID: 12687
		private static Dictionary<int, storyUnlockData> m_dicNKCCollectionStoryTemplet = new Dictionary<int, storyUnlockData>(300);

		// Token: 0x04003190 RID: 12688
		private static List<storyUnlockData> m_lstSkinTemplet = new List<storyUnlockData>();

		// Token: 0x04003191 RID: 12689
		private static Dictionary<int, NKCollectionStoryTemplet> m_dicCollectionStoryData = new Dictionary<int, NKCollectionStoryTemplet>(300);

		// Token: 0x04003192 RID: 12690
		private static Dictionary<int, List<int>> m_dicEpisodeStageIdData = new Dictionary<int, List<int>>(300);

		// Token: 0x04003193 RID: 12691
		public static Dictionary<int, List<NKCUnitTagData>> m_dic_UnitTagData = new Dictionary<int, List<NKCUnitTagData>>(100);

		// Token: 0x04003194 RID: 12692
		private static List<int> m_lstCollectionUnit = new List<int>();

		// Token: 0x04003195 RID: 12693
		private static List<int> m_lstCollectionShip = new List<int>();

		// Token: 0x04003196 RID: 12694
		private static List<int> m_lstCollectionOperator = new List<int>();

		// Token: 0x04003197 RID: 12695
		private static bool m_bPreLoading = false;

		// Token: 0x020012F9 RID: 4857
		public enum COLLECTION_STORY_CATEGORY
		{
			// Token: 0x040097A4 RID: 38820
			MAINSTREAM,
			// Token: 0x040097A5 RID: 38821
			SIDESTORY,
			// Token: 0x040097A6 RID: 38822
			EVENT,
			// Token: 0x040097A7 RID: 38823
			WORLDMAP,
			// Token: 0x040097A8 RID: 38824
			ETC,
			// Token: 0x040097A9 RID: 38825
			Count
		}
	}
}
