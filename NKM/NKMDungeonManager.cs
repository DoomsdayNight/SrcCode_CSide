using System;
using System.Collections.Generic;
using System.Text;
using Cs.Logging;
using Cs.Protocol;
using NKC;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;

namespace NKM
{
	// Token: 0x020003EF RID: 1007
	public sealed class NKMDungeonManager
	{
		// Token: 0x06001A5F RID: 6751 RVA: 0x000722C5 File Offset: 0x000704C5
		public static long CalculateDungeonRespawnUnitTempletUID(int dungeonID, DUNGEON_RESPAWN_UNIT_TEMPLET_TYPE dungeonRespawnUnitTempletType, int waveID, int respawnUnitCount)
		{
			return (long)dungeonID * 100000L + (long)dungeonRespawnUnitTempletType * 10000L + (long)(waveID * 100) + (long)respawnUnitCount;
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x000722E4 File Offset: 0x000704E4
		public static long AddNKMDungeonRespawnUnitTemplet(NKMDungeonRespawnUnitTemplet cNKMDungeonRespawnUnitTemplet, string dungeonName, int dungeonID, DUNGEON_RESPAWN_UNIT_TEMPLET_TYPE dungeonRespawnUnitTempletType, int waveID, int respawnUnitCount)
		{
			if (respawnUnitCount >= 100)
			{
				Log.ErrorAndExit(string.Format("[NKMDungeonRespawnUnitTemplet] RespawnUnitCount is Too Big - DungeonID[{0}:{1}] DungeonRespawnUnitTempletType[{2}] WaveID[{3}] RespawnUnitCount[{4}]", new object[]
				{
					dungeonID,
					dungeonName,
					dungeonRespawnUnitTempletType,
					waveID,
					respawnUnitCount
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonManager.cs", 829);
				return 0L;
			}
			if (waveID >= 100)
			{
				Log.ErrorAndExit(string.Format("[NKMDungeonRespawnUnitTemplet] WaveID is Too Big - DungeonID[{0}:{1}] DungeonRespawnUnitTempletType[{2}] WaveID[{3}] RespawnUnitCount[{4}]", new object[]
				{
					dungeonID,
					dungeonName,
					dungeonRespawnUnitTempletType,
					waveID,
					respawnUnitCount
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonManager.cs", 835);
				return 0L;
			}
			long num = NKMDungeonManager.CalculateDungeonRespawnUnitTempletUID(dungeonID, dungeonRespawnUnitTempletType, waveID, respawnUnitCount);
			if (NKMDungeonManager.m_dicNKMDungeonRespawnUnitTemplet.ContainsKey(num))
			{
				Log.ErrorAndExit(string.Format("[NKMDungeonRespawnUnitTemplet] Duplicate DungeonRespawnUnitTemplet UID[{0}]  DungeonID[{1}:{2}] dungeonRespawnUnitTempletType[{3}] respawnUnitCount[{4}]", new object[]
				{
					num,
					dungeonID,
					dungeonName,
					dungeonRespawnUnitTempletType,
					respawnUnitCount
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonManager.cs", 842);
				return 0L;
			}
			NKMDungeonManager.m_dicNKMDungeonRespawnUnitTemplet.Add(num, cNKMDungeonRespawnUnitTemplet);
			return num;
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00072406 File Offset: 0x00070606
		public static NKMDungeonRespawnUnitTemplet GetNKMDungeonRespawnUnitTemplet(long UID)
		{
			if (!NKMDungeonManager.m_dicNKMDungeonRespawnUnitTemplet.ContainsKey(UID))
			{
				return null;
			}
			return NKMDungeonManager.m_dicNKMDungeonRespawnUnitTemplet[UID];
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00072424 File Offset: 0x00070624
		public static bool LoadFromLUA(string dungeonTempletBaseFileName, string dungeonTempletFolderName, bool bFullLoad)
		{
			NKMDungeonManager.m_dicNKMDungeonTempletByID.Clear();
			NKMDungeonManager.m_dicNKMDungeonTempletByStrID.Clear();
			NKMDungeonManager.m_DungeonTempletFolderName = dungeonTempletFolderName;
			NKMDungeonManager.LoadFromLUA_LUA_DUNGEON_TEMPLET_BASE(dungeonTempletBaseFileName);
			foreach (NKMDungeonTempletBase dungeonTempletBase in NKMTempletContainer<NKMDungeonTempletBase>.Values)
			{
				NKMDungeonTemplet nkmdungeonTemplet = new NKMDungeonTemplet
				{
					m_DungeonTempletBase = dungeonTempletBase
				};
				if (bFullLoad)
				{
					NKMDungeonManager.LoadFromLUA_LUA_DUNGEON_TEMPLET(nkmdungeonTemplet);
				}
				IEnumerator<NKMDungeonTempletBase> enumerator;
				NKMDungeonManager.m_dicNKMDungeonTempletByID.Add(enumerator.Current.Key, nkmdungeonTemplet);
				NKMDungeonManager.m_dicNKMDungeonTempletByStrID.Add(enumerator.Current.m_DungeonStrID, nkmdungeonTemplet);
			}
			return true;
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x000724B0 File Offset: 0x000706B0
		private static void LoadFromLUA_LUA_DUNGEON_TEMPLET_BASE(string dungeonTempletBaseFileName)
		{
			NKMDungeonManager.m_DungeonTempletBaseFileName = dungeonTempletBaseFileName;
			NKMTempletContainer<NKMDungeonTempletBase>.Load("AB_SCRIPT_DUNGEON_TEMPLET", NKMDungeonManager.m_DungeonTempletBaseFileName, "m_dicNKMDungeonTempletByStrID", new Func<NKMLua, NKMDungeonTempletBase>(NKMDungeonTempletBase.LoadFromLUA), (NKMDungeonTempletBase e) => e.m_DungeonStrID);
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00072504 File Offset: 0x00070704
		private static void LoadFromLUA_LUA_DUNGEON_TEMPLET(NKMDungeonTemplet cNKMDungeonTemplet)
		{
			if (cNKMDungeonTemplet.m_DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_CUTSCENE)
			{
				return;
			}
			NKMLua nkmlua = new NKMLua();
			if (nkmlua.LoadCommonPath("AB_SCRIPT_DUNGEON_TEMPLET_ALL", NKMDungeonManager.m_DungeonTempletFolderName + cNKMDungeonTemplet.m_DungeonTempletBase.m_DungeonTempletFileName, true))
			{
				if (nkmlua.OpenTable("NKMDungeonTemplet"))
				{
					cNKMDungeonTemplet.LoadFromLUA(nkmlua, cNKMDungeonTemplet.m_DungeonTempletBase);
					nkmlua.CloseTable();
				}
				else
				{
					Log.ErrorAndExit(string.Format("[DungeonTemplet] 데이터 로드 실패 m_DungeonID : {0}, m_DungeonType : {1}, m_DungeonTempletFileName : {2}", cNKMDungeonTemplet.m_DungeonTempletBase.m_DungeonID, cNKMDungeonTemplet.m_DungeonTempletBase.m_DungeonType, cNKMDungeonTemplet.m_DungeonTempletBase.m_DungeonTempletFileName), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonManager.cs", 914);
				}
			}
			nkmlua.LuaClose();
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x000725B7 File Offset: 0x000707B7
		public static bool LoadFromLUA_EventDeckInfo()
		{
			NKMDungeonManager.m_dicNKMDungeonEventDeckTemplet = NKMTempletLoader.LoadDictionary<NKMDungeonEventDeckTemplet>("ab_script", "LUA_EVENTDECK_TEMPLET", "EVENTDECK_TEMPLET", new Func<NKMLua, NKMDungeonEventDeckTemplet>(NKMDungeonEventDeckTemplet.LoadFromLUA));
			return NKMDungeonManager.m_dicNKMDungeonEventDeckTemplet != null;
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x000725E8 File Offset: 0x000707E8
		public static NKMDungeonEventDeckTemplet GetEventDeckTemplet(int id)
		{
			NKMDungeonEventDeckTemplet result;
			if (NKMDungeonManager.m_dicNKMDungeonEventDeckTemplet.TryGetValue(id, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00072607 File Offset: 0x00070807
		public static bool IsEventDeckUnitStyleRight(NKMDungeonEventDeckTemplet.SLOT_TYPE slotType, NKM_UNIT_STYLE_TYPE styleType)
		{
			switch (slotType)
			{
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_CLOSED:
				return false;
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_COUNTER:
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_SOLDIER:
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_MECHANIC:
				return styleType == NKMDungeonManager.GetUnitStyleTypeFromEventDeckType(slotType);
			}
			return true;
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x0007263E File Offset: 0x0007083E
		public static NKM_UNIT_STYLE_TYPE GetUnitStyleTypeFromEventDeckType(NKMDungeonEventDeckTemplet.SLOT_TYPE slotType)
		{
			switch (slotType)
			{
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_COUNTER:
				return NKM_UNIT_STYLE_TYPE.NUST_COUNTER;
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_SOLDIER:
				return NKM_UNIT_STYLE_TYPE.NUST_SOLDIER;
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_MECHANIC:
				return NKM_UNIT_STYLE_TYPE.NUST_MECHANIC;
			default:
				return NKM_UNIT_STYLE_TYPE.NUST_INVALID;
			}
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x0007265C File Offset: 0x0007085C
		public static int GetDungeonID(string dungeonStrID)
		{
			if (dungeonStrID == null)
			{
				return 0;
			}
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(dungeonStrID);
			if (dungeonTempletBase != null)
			{
				return dungeonTempletBase.m_DungeonID;
			}
			return 0;
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x00072680 File Offset: 0x00070880
		public static List<string> GetTotalDungeonStrID()
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<int, NKMDungeonTemplet> keyValuePair in NKMDungeonManager.m_dicNKMDungeonTempletByID)
			{
				NKMDungeonTemplet value = keyValuePair.Value;
				list.Add(value.m_DungeonTempletBase.m_DungeonStrID);
			}
			return list;
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x000726CC File Offset: 0x000708CC
		public static List<string> GetTotalDungeonStrIDExpectCutscene()
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<int, NKMDungeonTemplet> keyValuePair in NKMDungeonManager.m_dicNKMDungeonTempletByID)
			{
				NKMDungeonTemplet value = keyValuePair.Value;
				if (value.m_DungeonTempletBase.m_DungeonType != NKM_DUNGEON_TYPE.NDT_CUTSCENE)
				{
					list.Add(value.m_DungeonTempletBase.m_DungeonStrID);
				}
			}
			return list;
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x00072728 File Offset: 0x00070928
		public static int GetDungeonLevel(int dungeonID)
		{
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(dungeonID);
			if (dungeonTempletBase != null)
			{
				return dungeonTempletBase.m_DungeonLevel;
			}
			return 0;
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x00072747 File Offset: 0x00070947
		public static NKMDungeonTempletBase GetDungeonTempletBase(int dungeonID)
		{
			if (NKMDungeonManager.m_dicNKMDungeonTempletByID.ContainsKey(dungeonID))
			{
				return NKMDungeonManager.m_dicNKMDungeonTempletByID[dungeonID].m_DungeonTempletBase;
			}
			return null;
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x00072768 File Offset: 0x00070968
		public static NKMDungeonTempletBase GetDungeonTempletBase(string dungeonStrID)
		{
			if (NKMDungeonManager.m_dicNKMDungeonTempletByStrID.ContainsKey(dungeonStrID))
			{
				return NKMDungeonManager.m_dicNKMDungeonTempletByStrID[dungeonStrID].m_DungeonTempletBase;
			}
			return null;
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x0007278C File Offset: 0x0007098C
		public static NKMDungeonTemplet GetDungeonTemplet(int dungeonID)
		{
			if (NKMDungeonManager.m_dicNKMDungeonTempletByID.ContainsKey(dungeonID))
			{
				NKMDungeonTemplet nkmdungeonTemplet = NKMDungeonManager.m_dicNKMDungeonTempletByID[dungeonID];
				if (!nkmdungeonTemplet.m_bLoaded)
				{
					NKMDungeonManager.LoadFromLUA_LUA_DUNGEON_TEMPLET(nkmdungeonTemplet);
				}
				return nkmdungeonTemplet;
			}
			return null;
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x000727C4 File Offset: 0x000709C4
		public static NKMDungeonTemplet GetDungeonTemplet(string dungeonStrID)
		{
			if (NKMDungeonManager.m_dicNKMDungeonTempletByStrID.ContainsKey(dungeonStrID))
			{
				NKMDungeonTemplet nkmdungeonTemplet = NKMDungeonManager.m_dicNKMDungeonTempletByStrID[dungeonStrID];
				if (!nkmdungeonTemplet.m_bLoaded)
				{
					NKMDungeonManager.LoadFromLUA_LUA_DUNGEON_TEMPLET(nkmdungeonTemplet);
				}
				return nkmdungeonTemplet;
			}
			return null;
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x000727FC File Offset: 0x000709FC
		public static bool MakeGameTeamData(NKMGameData cNKMGameData, NKMGameRuntimeData cNKMGameRuntimeData)
		{
			NKMDungeonTemplet dungeonTemplet = NKMDungeonManager.GetDungeonTemplet(cNKMGameData.m_DungeonID);
			if (dungeonTemplet == null)
			{
				Log.Error("No Exist NKMDungeonTemplet dungeonID: " + cNKMGameData.m_DungeonID.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonManager.cs", 1096);
				return false;
			}
			if (cNKMGameData.m_TeamASupply > 0)
			{
				cNKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCost = 4f;
				if (cNKMGameData.m_NKMGameTeamDataA.m_MainShip != null)
				{
					NKMUnitTemplet unitTemplet = cNKMGameData.m_NKMGameTeamDataA.m_MainShip.GetUnitTemplet();
					if (unitTemplet != null)
					{
						cNKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCost += (float)unitTemplet.m_UnitTempletBase.m_StarGradeMax;
					}
				}
			}
			else
			{
				cNKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fRespawnCost = 0f;
			}
			cNKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_fRespawnCost = dungeonTemplet.m_fStartCost;
			NKMMapTemplet mapTempletByStrID = NKMMapManager.GetMapTempletByStrID(dungeonTemplet.m_DungeonTempletBase.m_DungeonMapStrID);
			if (mapTempletByStrID == null)
			{
				Log.Error("No Exist cNKMMapTemplet m_DungeonMapStrID: " + dungeonTemplet.m_DungeonTempletBase.m_DungeonMapStrID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonManager.cs", 1122);
				return false;
			}
			cNKMGameData.m_MapID = mapTempletByStrID.m_MapID;
			cNKMGameData.m_fDoubleCostTime = dungeonTemplet.m_DungeonTempletBase.m_fDoubleCostTime;
			if (dungeonTemplet.m_DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_WAVE)
			{
				cNKMGameData.m_NKMGameTeamDataB.m_MainShip = null;
				cNKMGameData.m_NKMGameTeamDataB.m_LeaderUnitUID = 0L;
				cNKMGameData.m_NKMGameTeamDataB.m_UserLevel = NKMDungeonManager.GetFixedTeamBUnitLevel(dungeonTemplet.m_BossUnitLevel, cNKMGameData);
				cNKMGameData.m_NKMGameTeamDataB.m_UserNickname = "";
			}
			else
			{
				NKMUnitData nkmunitData = new NKMUnitData();
				nkmunitData.m_UnitUID = NpcUid.Get();
				NKMUnitTemplet unitTemplet2 = NKMUnitManager.GetUnitTemplet(dungeonTemplet.m_BossUnitStrID);
				if (unitTemplet2 != null)
				{
					nkmunitData.m_UnitID = unitTemplet2.m_UnitTempletBase.m_UnitID;
					cNKMGameData.m_NKMGameTeamDataB.m_UserNickname = "";
				}
				nkmunitData.m_UnitLevel = NKMDungeonManager.GetFixedTeamBUnitLevel(dungeonTemplet.m_BossUnitLevel, cNKMGameData);
				nkmunitData.SetDungeonRespawnUnitTemplet(dungeonTemplet.m_BossRespawnUnitTemplet);
				cNKMGameData.m_NKMGameTeamDataB.m_MainShip = nkmunitData;
				cNKMGameData.m_NKMGameTeamDataB.m_LeaderUnitUID = nkmunitData.m_UnitUID;
				cNKMGameData.m_NKMGameTeamDataB.m_UserLevel = NKMDungeonManager.GetFixedTeamBUnitLevel(dungeonTemplet.m_BossUnitLevel, cNKMGameData);
			}
			cNKMGameData.m_NKMGameTeamDataB.m_listUnitData.Clear();
			for (int i = 0; i < dungeonTemplet.m_listDungeonDeck.Count; i++)
			{
				if (i == 8)
				{
					Log.Error("cNKMDungeonTemplet.m_listDungeonDeck over 8 : " + dungeonTemplet.m_listDungeonDeck.Count.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonManager.cs", 1164);
					break;
				}
				NKMDungeonRespawnUnitTemplet nkmdungeonRespawnUnitTemplet = dungeonTemplet.m_listDungeonDeck[i];
				if (nkmdungeonRespawnUnitTemplet == null)
				{
					Log.Error("cNKMDungeonUnitTemplet null dungeonID: " + cNKMGameData.m_DungeonID.ToString() + " i: " + i.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonManager.cs", 1171);
				}
				else
				{
					NKMUnitData nkmunitData2 = new NKMUnitData();
					nkmunitData2.m_UnitUID = NpcUid.Get();
					nkmunitData2.m_UnitID = NKMUnitManager.GetUnitID(nkmdungeonRespawnUnitTemplet.m_UnitStrID);
					nkmunitData2.m_SkinID = nkmdungeonRespawnUnitTemplet.m_SkinID;
					nkmunitData2.m_UnitLevel = NKMDungeonManager.GetFixedTeamBUnitLevel(nkmdungeonRespawnUnitTemplet.m_UnitLevel, cNKMGameData);
					nkmunitData2.m_LimitBreakLevel = nkmdungeonRespawnUnitTemplet.m_UnitLimitBreakLevel;
					nkmunitData2.SetDungeonRespawnUnitTemplet(nkmdungeonRespawnUnitTemplet);
					cNKMGameData.m_NKMGameTeamDataB.m_listUnitData.Add(nkmunitData2);
				}
			}
			cNKMGameData.m_NKMGameTeamDataA.m_listEvevtUnitData.Clear();
			for (int j = 0; j < dungeonTemplet.m_listDungeonUnitRespawnA.Count; j++)
			{
				NKMDungeonRespawnUnitTemplet nkmdungeonRespawnUnitTemplet2 = dungeonTemplet.m_listDungeonUnitRespawnA[j];
				if (nkmdungeonRespawnUnitTemplet2 != null)
				{
					NKMUnitData nkmunitData3 = new NKMUnitData();
					nkmunitData3.m_UnitUID = NpcUid.Get();
					nkmunitData3.m_UnitID = NKMUnitManager.GetUnitID(nkmdungeonRespawnUnitTemplet2.m_UnitStrID);
					nkmunitData3.m_SkinID = nkmdungeonRespawnUnitTemplet2.m_SkinID;
					nkmunitData3.m_UnitLevel = NKMDungeonManager.GetFixedTeamBUnitLevel(nkmdungeonRespawnUnitTemplet2.m_UnitLevel, cNKMGameData);
					nkmunitData3.m_LimitBreakLevel = nkmdungeonRespawnUnitTemplet2.m_UnitLimitBreakLevel;
					nkmunitData3.SetDungeonRespawnUnitTemplet(nkmdungeonRespawnUnitTemplet2);
					cNKMGameData.m_NKMGameTeamDataA.m_listEvevtUnitData.Add(nkmunitData3);
				}
			}
			cNKMGameData.m_NKMGameTeamDataB.m_listEvevtUnitData.Clear();
			for (int k = 0; k < dungeonTemplet.m_listDungeonUnitRespawnB.Count; k++)
			{
				NKMDungeonRespawnUnitTemplet nkmdungeonRespawnUnitTemplet3 = dungeonTemplet.m_listDungeonUnitRespawnB[k];
				if (nkmdungeonRespawnUnitTemplet3 != null)
				{
					NKMUnitData nkmunitData4 = new NKMUnitData();
					nkmunitData4.m_UnitUID = NpcUid.Get();
					nkmunitData4.m_UnitID = NKMUnitManager.GetUnitID(nkmdungeonRespawnUnitTemplet3.m_UnitStrID);
					nkmunitData4.m_SkinID = nkmdungeonRespawnUnitTemplet3.m_SkinID;
					nkmunitData4.m_UnitLevel = NKMDungeonManager.GetFixedTeamBUnitLevel(nkmdungeonRespawnUnitTemplet3.m_UnitLevel, cNKMGameData);
					nkmunitData4.m_LimitBreakLevel = nkmdungeonRespawnUnitTemplet3.m_UnitLimitBreakLevel;
					nkmunitData4.SetDungeonRespawnUnitTemplet(nkmdungeonRespawnUnitTemplet3);
					cNKMGameData.m_NKMGameTeamDataB.m_listEvevtUnitData.Add(nkmunitData4);
				}
			}
			for (int l = 0; l < dungeonTemplet.m_listDungeonWave.Count; l++)
			{
				for (int m = 0; m < dungeonTemplet.m_listDungeonWave[l].m_listDungeonUnitRespawnA.Count; m++)
				{
					NKMDungeonRespawnUnitTemplet nkmdungeonRespawnUnitTemplet4 = dungeonTemplet.m_listDungeonWave[l].m_listDungeonUnitRespawnA[m];
					if (nkmdungeonRespawnUnitTemplet4 != null)
					{
						NKMUnitData nkmunitData5 = new NKMUnitData();
						nkmunitData5.m_UnitUID = NpcUid.Get();
						nkmunitData5.m_UnitID = NKMUnitManager.GetUnitID(nkmdungeonRespawnUnitTemplet4.m_UnitStrID);
						nkmunitData5.m_SkinID = nkmdungeonRespawnUnitTemplet4.m_SkinID;
						nkmunitData5.m_UnitLevel = NKMDungeonManager.GetFixedTeamBUnitLevel(nkmdungeonRespawnUnitTemplet4.m_UnitLevel, cNKMGameData);
						nkmunitData5.m_LimitBreakLevel = nkmdungeonRespawnUnitTemplet4.m_UnitLimitBreakLevel;
						nkmunitData5.SetDungeonRespawnUnitTemplet(nkmdungeonRespawnUnitTemplet4);
						cNKMGameData.m_NKMGameTeamDataA.m_listEvevtUnitData.Add(nkmunitData5);
					}
				}
				for (int n = 0; n < dungeonTemplet.m_listDungeonWave[l].m_listDungeonUnitRespawnB.Count; n++)
				{
					NKMDungeonRespawnUnitTemplet nkmdungeonRespawnUnitTemplet5 = dungeonTemplet.m_listDungeonWave[l].m_listDungeonUnitRespawnB[n];
					if (nkmdungeonRespawnUnitTemplet5 != null)
					{
						NKMUnitData nkmunitData6 = new NKMUnitData();
						nkmunitData6.m_UnitUID = NpcUid.Get();
						nkmunitData6.m_UnitID = NKMUnitManager.GetUnitID(nkmdungeonRespawnUnitTemplet5.m_UnitStrID);
						nkmunitData6.m_SkinID = nkmdungeonRespawnUnitTemplet5.m_SkinID;
						nkmunitData6.m_UnitLevel = NKMDungeonManager.GetFixedTeamBUnitLevel(nkmdungeonRespawnUnitTemplet5.m_UnitLevel, cNKMGameData);
						nkmunitData6.m_LimitBreakLevel = nkmdungeonRespawnUnitTemplet5.m_UnitLimitBreakLevel;
						nkmunitData6.SetDungeonRespawnUnitTemplet(nkmdungeonRespawnUnitTemplet5);
						cNKMGameData.m_NKMGameTeamDataB.m_listEvevtUnitData.Add(nkmunitData6);
					}
				}
			}
			foreach (int bCondID in cNKMGameData.m_BattleConditionIDs.Keys)
			{
				NKMBattleConditionTemplet templetByID = NKMBattleConditionManager.GetTempletByID(bCondID);
				if (templetByID != null)
				{
					NKMDungeonManager.MakeEnviromentUnitData(templetByID.AllyBCondUnitStrIDList, cNKMGameData, cNKMGameData.m_NKMGameTeamDataA);
					NKMDungeonManager.MakeEnviromentUnitData(templetByID.EnemyBCondUnitStrIDList, cNKMGameData, cNKMGameData.m_NKMGameTeamDataB);
				}
			}
			return true;
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x00072E8C File Offset: 0x0007108C
		private static void MakeEnviromentUnitData(ICollection<string> unitStrIDList, NKMGameData cNKMGameData, NKMGameTeamData cNKMGameTeamData)
		{
			if (cNKMGameData == null || cNKMGameTeamData == null)
			{
				return;
			}
			foreach (string unitStrID in unitStrIDList)
			{
				NKMUnitData nkmunitData = new NKMUnitData();
				nkmunitData.m_UnitUID = NpcUid.Get();
				nkmunitData.m_UnitID = NKMUnitManager.GetUnitID(unitStrID);
				cNKMGameTeamData.m_listEnvUnitData.Add(nkmunitData);
			}
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x00072F00 File Offset: 0x00071100
		public static NKM_ERROR_CODE CheckCommon(NKMUserData cNKMUserData, NKMDungeonTempletBase dungeonTempletBase)
		{
			if (cNKMUserData.m_UserLevel < dungeonTempletBase.m_DGLimitUserLevel)
			{
				return NKM_ERROR_CODE.NEC_FAIL_REQUIRE_MORE_USER_LEVEL;
			}
			if (!cNKMUserData.m_ArmyData.CanGetMoreUnit(1))
			{
				return NKM_ERROR_CODE.NEC_FAIL_ARMY_FULL;
			}
			if (!cNKMUserData.m_ArmyData.CanGetMoreShip(0))
			{
				return NKM_ERROR_CODE.NEC_FAIL_SHIP_FULL;
			}
			if (!cNKMUserData.m_InventoryData.CanGetMoreEquipItem(1))
			{
				return NKM_ERROR_CODE.NEC_FAIL_EQUIP_ITEM_FULL;
			}
			if (!cNKMUserData.m_ArmyData.CanGetMoreOperator(0))
			{
				return NKM_ERROR_CODE.NEC_FAIL_OPERATOR_FULL;
			}
			if (!cNKMUserData.m_ArmyData.CanGetMoreTrophy(1))
			{
				return NKM_ERROR_CODE.NEC_FAIL_TROPHY_FULL;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x00072F78 File Offset: 0x00071178
		public static NKM_ERROR_CODE CheckEventSlot(NKMArmyData armyData, NKMDungeonEventDeckTemplet eventDeckTemplet, NKMDungeonEventDeckTemplet.EventDeckSlot eventDeckSlotData, long targetUnitUID, NKM_UNIT_TYPE unitType)
		{
			NKMDungeonEventDeckTemplet.SLOT_TYPE eType = eventDeckSlotData.m_eType;
			if (eType != NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_CLOSED)
			{
				if (eType != NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_NPC)
				{
					if (targetUnitUID != 0L)
					{
						if (unitType == NKM_UNIT_TYPE.NUT_OPERATOR)
						{
							NKMOperator operatorFromUId = armyData.GetOperatorFromUId(targetUnitUID);
							if (operatorFromUId == null)
							{
								return NKM_ERROR_CODE.NEC_FAIL_OPERATOR_INVALID_UNIT_UID;
							}
							eType = eventDeckSlotData.m_eType;
							if (eType - NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FIXED <= 1 && eventDeckSlotData.m_ID != operatorFromUId.id)
							{
								return NKM_ERROR_CODE.NEC_FAIL_EVENTDECK_SLOT_DIFFRENT_UNIT;
							}
							if (NKMUnitManager.GetUnitTempletBase(operatorFromUId) == null)
							{
								return NKM_ERROR_CODE.NEC_FAIL_GET_UNIT_BASE_TEMPLET_NULL;
							}
						}
						else
						{
							NKMUnitData nkmunitData;
							if (unitType != NKM_UNIT_TYPE.NUT_NORMAL)
							{
								if (unitType != NKM_UNIT_TYPE.NUT_SHIP)
								{
									return NKM_ERROR_CODE.NEC_FAIL_EVENT_SLOT_UNIT_TYPE_INVALID;
								}
								nkmunitData = armyData.GetShipFromUID(targetUnitUID);
							}
							else
							{
								nkmunitData = armyData.GetUnitOrTrophyFromUID(targetUnitUID);
							}
							if (nkmunitData == null)
							{
								return NKM_ERROR_CODE.NEC_FAIL_DECK_UNIT_INVALID;
							}
							NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(nkmunitData);
							if (unitTempletBase == null)
							{
								return NKM_ERROR_CODE.NEC_FAIL_GET_UNIT_BASE_TEMPLET_NULL;
							}
							eType = eventDeckSlotData.m_eType;
							if (eType - NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FIXED <= 1 && !unitTempletBase.IsSameBaseUnit(eventDeckSlotData.m_ID))
							{
								return NKM_ERROR_CODE.NEC_FAIL_EVENTDECK_SLOT_DIFFRENT_UNIT;
							}
							if (!NKMDungeonManager.IsEventDeckUnitStyleRight(eventDeckSlotData.m_eType, unitTempletBase.m_NKM_UNIT_STYLE_TYPE))
							{
								return NKM_ERROR_CODE.NEC_FAIL_EVENTDECK_SLOT_DIFFRENT_UNIT;
							}
						}
						return NKM_ERROR_CODE.NEC_OK;
					}
					if (eventDeckSlotData.m_eType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_GUEST)
					{
						return NKM_ERROR_CODE.NEC_OK;
					}
					if (unitType == NKM_UNIT_TYPE.NUT_SHIP)
					{
						return NKM_ERROR_CODE.NEC_FAIL_DECK_NO_SHIP;
					}
					if (unitType != NKM_UNIT_TYPE.NUT_OPERATOR)
					{
						return NKM_ERROR_CODE.NEC_FAIL_DECK_UNIT_INVALID;
					}
					return NKM_ERROR_CODE.NEC_OK;
				}
				else
				{
					if (targetUnitUID != 0L)
					{
						return NKM_ERROR_CODE.NEC_FAIL_EVENTDECK_SLOT_UNREQUIRED_DATA;
					}
					return NKM_ERROR_CODE.NEC_OK;
				}
			}
			else
			{
				if (unitType == NKM_UNIT_TYPE.NUT_SHIP)
				{
					return NKM_ERROR_CODE.NEC_FAIL_DECK_NO_SHIP;
				}
				if (targetUnitUID != 0L)
				{
					return NKM_ERROR_CODE.NEC_FAIL_EVENTDECK_SLOT_UNREQUIRED_DATA;
				}
				return NKM_ERROR_CODE.NEC_OK;
			}
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x00073094 File Offset: 0x00071294
		public static NKM_ERROR_CODE IsValidEventDeck(NKMArmyData armyData, NKMDungeonEventDeckTemplet eventDeckTemplet, NKMEventDeckData eventDeckData, bool bOperatorEnabled)
		{
			if (eventDeckTemplet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_EVENT_DECK_TEMPLET_NULL;
			}
			if (eventDeckData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_EVENTDECK_NOT_EXIST;
			}
			Dictionary<int, long> dicUnit = eventDeckData.m_dicUnit;
			long shipUID = eventDeckData.m_ShipUID;
			HashSet<int> hashSet = new HashSet<int>();
			NKM_ERROR_CODE nkm_ERROR_CODE = NKMDungeonManager.CheckEventSlot(armyData, eventDeckTemplet, eventDeckTemplet.ShipSlot, shipUID, NKM_UNIT_TYPE.NUT_SHIP);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				return nkm_ERROR_CODE;
			}
			if (bOperatorEnabled)
			{
				NKM_ERROR_CODE nkm_ERROR_CODE2 = NKMDungeonManager.CheckEventSlot(armyData, eventDeckTemplet, eventDeckTemplet.OperatorSlot, eventDeckData.m_OperatorUID, NKM_UNIT_TYPE.NUT_OPERATOR);
				if (nkm_ERROR_CODE2 != NKM_ERROR_CODE.NEC_OK)
				{
					return nkm_ERROR_CODE2;
				}
			}
			for (int i = 0; i < 8; i++)
			{
				NKMDungeonEventDeckTemplet.EventDeckSlot unitSlot = eventDeckTemplet.GetUnitSlot(i);
				long num;
				if (!dicUnit.TryGetValue(i, out num))
				{
					num = 0L;
				}
				NKM_ERROR_CODE nkm_ERROR_CODE3 = NKMDungeonManager.CheckEventSlot(armyData, eventDeckTemplet, unitSlot, num, NKM_UNIT_TYPE.NUT_NORMAL);
				if (nkm_ERROR_CODE3 != NKM_ERROR_CODE.NEC_OK)
				{
					return nkm_ERROR_CODE3;
				}
				switch (unitSlot.m_eType)
				{
				case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE:
				case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_COUNTER:
				case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_SOLDIER:
				case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_MECHANIC:
				{
					NKMUnitData unitFromUID = armyData.GetUnitFromUID(num);
					if (NKMUnitManager.CheckContainsBaseUnit(hashSet, unitFromUID.m_UnitID))
					{
						return NKM_ERROR_CODE.NEC_FAIL_DECK_DUPLICATE_UNIT;
					}
					if (unitFromUID.IsSeized)
					{
						return NKM_ERROR_CODE.NEC_FAIL_UNIT_IS_SEIZED;
					}
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitFromUID);
					if (unitTempletBase == null)
					{
						return NKM_ERROR_CODE.NEC_FAIL_GET_UNIT_BASE_TEMPLET_NULL;
					}
					if (!NKMDungeonManager.IsEventDeckUnitStyleRight(unitSlot.m_eType, unitTempletBase.m_NKM_UNIT_STYLE_TYPE))
					{
						return NKM_ERROR_CODE.NEC_FAIL_EVENTDECK_SLOT_DIFFRENT_UNIT;
					}
					hashSet.Add(unitFromUID.m_UnitID);
					break;
				}
				case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FIXED:
				case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_GUEST:
				case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_NPC:
					if (NKMUnitManager.CheckContainsBaseUnit(hashSet, unitSlot.m_ID))
					{
						return NKM_ERROR_CODE.NEC_FAIL_DECK_DUPLICATE_UNIT;
					}
					hashSet.Add(unitSlot.m_ID);
					break;
				}
			}
			if (eventDeckTemplet.m_DeckCondition != null)
			{
				return eventDeckTemplet.m_DeckCondition.CheckEventDeckCondition(armyData, eventDeckTemplet, eventDeckData, bOperatorEnabled);
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x00073218 File Offset: 0x00071418
		public static NKMUnitData MakeUnitDataFromID(int unitID, long unitUid, int level, int limitBreakLevel, int skinID, int tacticLevel = 0)
		{
			NKMUnitData nkmunitData = new NKMUnitData();
			nkmunitData.m_UnitID = unitID;
			nkmunitData.m_UnitUID = unitUid;
			nkmunitData.m_UnitLevel = level;
			nkmunitData.m_SkinID = skinID;
			nkmunitData.tacticLevel = tacticLevel;
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
			if (limitBreakLevel == -1)
			{
				nkmunitData.m_LimitBreakLevel = (short)NKMUnitLimitBreakManager.GetMaxLimitBreakLevelByUnitLevel(level);
			}
			else
			{
				nkmunitData.m_LimitBreakLevel = (short)limitBreakLevel;
			}
			if (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL)
			{
				int skillCount = unitTempletBase.GetSkillCount();
				for (int i = 0; i < skillCount; i++)
				{
					NKMUnitSkillTemplet skillTemplet = NKMUnitSkillManager.GetSkillTemplet(unitTempletBase.GetSkillStrID(i), 1);
					if (skillTemplet != null)
					{
						int maxSkillLevelFromLimitBreakLevel = NKMUnitSkillManager.GetMaxSkillLevelFromLimitBreakLevel(skillTemplet.m_ID, (int)nkmunitData.m_LimitBreakLevel);
						if (NKMUnitSkillManager.GetSkillTemplet(skillTemplet.m_ID, maxSkillLevelFromLimitBreakLevel) != null)
						{
							nkmunitData.m_aUnitSkillLevel[i] = maxSkillLevelFromLimitBreakLevel;
						}
						else
						{
							nkmunitData.m_aUnitSkillLevel[i] = 1;
						}
					}
				}
				for (int j = 0; j < nkmunitData.m_listStatEXP.Count; j++)
				{
					nkmunitData.m_listStatEXP[j] = NKMEnhanceManager.CalculateMaxEXP(nkmunitData, (NKM_STAT_TYPE)j);
				}
			}
			return nkmunitData;
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x00073310 File Offset: 0x00071510
		public static NKMOperator MakeOperatorDataFromID(int unitID, long unitUid, int level, int subSkillID)
		{
			NKMOperator nkmoperator = new NKMOperator();
			nkmoperator.uid = unitUid;
			nkmoperator.id = unitID;
			nkmoperator.level = level;
			nkmoperator.mainSkill = new NKMOperatorSkill();
			nkmoperator.subSkill = new NKMOperatorSkill();
			NKMOperatorSkillTemplet nkmoperatorSkillTemplet = NKMTempletContainer<NKMOperatorSkillTemplet>.Find(NKMUnitManager.GetUnitTempletBase(unitID).m_lstSkillStrID[0]);
			if (nkmoperatorSkillTemplet != null)
			{
				nkmoperator.mainSkill.id = nkmoperatorSkillTemplet.m_OperSkillID;
				nkmoperator.mainSkill.level = (byte)nkmoperatorSkillTemplet.m_MaxSkillLevel;
			}
			NKMOperatorSkillTemplet nkmoperatorSkillTemplet2 = NKMTempletContainer<NKMOperatorSkillTemplet>.Find(subSkillID);
			if (nkmoperatorSkillTemplet2 != null)
			{
				nkmoperator.subSkill.id = subSkillID;
				nkmoperator.subSkill.level = (byte)nkmoperatorSkillTemplet2.m_MaxSkillLevel;
			}
			return nkmoperator;
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x000733B4 File Offset: 0x000715B4
		public static NKMUnitData MakeEventDeckUnit(NKMArmyData cNKMArmyData, NKMDungeonEventDeckTemplet eventDeckTemplet, NKMDungeonEventDeckTemplet.EventDeckSlot eventDeckSlot, long unitUid, NKM_UNIT_TYPE unitType)
		{
			switch (eventDeckSlot.m_eType)
			{
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_CLOSED:
				return null;
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE:
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FIXED:
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_GUEST:
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_COUNTER:
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_SOLDIER:
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_MECHANIC:
			{
				if (eventDeckSlot.m_eType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_GUEST && unitUid == 0L)
				{
					return NKMDungeonManager.MakeUnitDataFromID(eventDeckSlot.m_ID, NpcUid.Get(), eventDeckSlot.m_Level, -1, eventDeckSlot.m_SkinID, 0);
				}
				NKMUnitData nkmunitData;
				if (unitType == NKM_UNIT_TYPE.NUT_SHIP)
				{
					nkmunitData = cNKMArmyData.GetShipFromUID(unitUid);
				}
				else
				{
					nkmunitData = cNKMArmyData.GetUnitFromUID(unitUid);
				}
				if (nkmunitData != null)
				{
					NKMUnitData nkmunitData2 = new NKMUnitData();
					nkmunitData2.DeepCopyFrom(nkmunitData);
					if (eventDeckTemplet.m_DeckCondition != null)
					{
						NKMDeckCondition.GameCondition gameCondition = eventDeckTemplet.m_DeckCondition.GetGameCondition(NKMDeckCondition.GAME_CONDITION.LEVEL_CAP);
						if (gameCondition != null && nkmunitData2.m_UnitLevel > gameCondition.Value)
						{
							nkmunitData2.m_UnitLevel = gameCondition.Value;
						}
					}
					if (eventDeckSlot.m_eType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_GUEST && eventDeckSlot.m_SkinID != 0)
					{
						nkmunitData2.m_SkinID = eventDeckSlot.m_SkinID;
					}
					return nkmunitData2;
				}
				return null;
			}
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_NPC:
				return NKMDungeonManager.MakeUnitDataFromID(eventDeckSlot.m_ID, NpcUid.Get(), eventDeckSlot.m_Level, -1, eventDeckSlot.m_SkinID, 0);
			default:
				return null;
			}
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x000734BC File Offset: 0x000716BC
		public static NKMOperator MakeEventDeckOperator(NKMArmyData cNKMArmyData, NKMDungeonEventDeckTemplet eventDeckTemplet, NKMDungeonEventDeckTemplet.EventDeckSlot eventDeckSlot, long unitUid)
		{
			switch (eventDeckSlot.m_eType)
			{
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_CLOSED:
				return null;
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE:
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FIXED:
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_GUEST:
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_COUNTER:
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_SOLDIER:
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_MECHANIC:
			{
				if (eventDeckSlot.m_eType == NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_GUEST && unitUid == 0L)
				{
					return NKMDungeonManager.MakeOperatorDataFromID(eventDeckSlot.m_ID, NpcUid.Get(), eventDeckSlot.m_Level, eventDeckTemplet.OperatorSubSkillID);
				}
				NKMOperator operatorFromUId = cNKMArmyData.GetOperatorFromUId(unitUid);
				if (operatorFromUId != null)
				{
					NKMOperator nkmoperator = new NKMOperator();
					nkmoperator.DeepCopyFrom(operatorFromUId);
					if (eventDeckTemplet.m_DeckCondition != null)
					{
						NKMDeckCondition.GameCondition gameCondition = eventDeckTemplet.m_DeckCondition.GetGameCondition(NKMDeckCondition.GAME_CONDITION.LEVEL_CAP);
						if (gameCondition != null && nkmoperator.level > gameCondition.Value)
						{
							nkmoperator.level = gameCondition.Value;
						}
					}
					return nkmoperator;
				}
				return null;
			}
			case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_NPC:
				return NKMDungeonManager.MakeOperatorDataFromID(eventDeckSlot.m_ID, NpcUid.Get(), eventDeckSlot.m_Level, eventDeckTemplet.OperatorSubSkillID);
			default:
				return null;
			}
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x00073594 File Offset: 0x00071794
		public static bool IsEventDeckSelectRequired(NKMStageTempletV2 stageTemplet, bool bOperatorEnabled)
		{
			STAGE_TYPE stage_TYPE = stageTemplet.m_STAGE_TYPE;
			if (stage_TYPE != STAGE_TYPE.ST_DUNGEON && stage_TYPE == STAGE_TYPE.ST_PHASE)
			{
				if (stageTemplet.PhaseTemplet != null)
				{
					return NKMDungeonManager.IsEventDeckSelectRequired(stageTemplet.PhaseTemplet.EventDeckTemplet, bOperatorEnabled);
				}
			}
			else if (stageTemplet.DungeonTempletBase != null)
			{
				return NKMDungeonManager.IsEventDeckSelectRequired(stageTemplet.DungeonTempletBase.EventDeckTemplet, bOperatorEnabled);
			}
			return false;
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x000735E8 File Offset: 0x000717E8
		public static bool IsEventDeckSelectRequired(NKMDungeonEventDeckTemplet eventDeckTemplet, bool bOperatorEnabled)
		{
			NKMDungeonEventDeckTemplet.SLOT_TYPE eType;
			foreach (NKMDungeonEventDeckTemplet.EventDeckSlot eventDeckSlot in eventDeckTemplet.m_lstUnitSlot)
			{
				eType = eventDeckSlot.m_eType;
				if (eType != NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_CLOSED && eType != NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_NPC)
				{
					return true;
				}
			}
			eType = eventDeckTemplet.ShipSlot.m_eType;
			if (eType != NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_CLOSED && eType != NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_NPC)
			{
				return true;
			}
			if (bOperatorEnabled)
			{
				eType = eventDeckTemplet.OperatorSlot.m_eType;
				if (eType != NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_CLOSED && eType != NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_NPC)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x00073674 File Offset: 0x00071874
		public static NKMUnitData MakeDeckShipData(NKMArmyData armyData, NKMDeckData deckData, NKMGameData gameData)
		{
			NKMUnitData nkmunitData = null;
			NKMUnitData shipFromUID = armyData.GetShipFromUID(deckData.m_ShipUID);
			if (shipFromUID != null)
			{
				nkmunitData = new NKMUnitData();
				nkmunitData.DeepCopyFrom(shipFromUID);
			}
			return nkmunitData;
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x000736A4 File Offset: 0x000718A4
		public static NKMOperator MakeDeckOperatorData(NKMArmyData armyData, NKMDeckData deckData, NKMGameData gameData)
		{
			NKMOperator operatorFromUId = armyData.GetOperatorFromUId(deckData.m_OperatorUID);
			if (operatorFromUId == null)
			{
				return null;
			}
			NKMOperator nkmoperator = new NKMOperator();
			nkmoperator.DeepCopyFrom(operatorFromUId);
			return nkmoperator;
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x000736D0 File Offset: 0x000718D0
		public static List<GameUnitData> MakeDeckUnitDataList(NKMArmyData armyData, NKMDeckData deckData, NKMInventoryData inventoryData)
		{
			List<GameUnitData> list = new List<GameUnitData>();
			for (int i = 0; i < deckData.m_listDeckUnitUID.Count; i++)
			{
				if (deckData.m_listDeckUnitUID[i] != 0L)
				{
					GameUnitData gameUnitData = new GameUnitData();
					NKMUnitData unitFromUID = armyData.GetUnitFromUID(deckData.m_listDeckUnitUID[i]);
					if (unitFromUID != null)
					{
						NKMUnitData nkmunitData = new NKMUnitData();
						nkmunitData.DeepCopyFrom(unitFromUID);
						gameUnitData.unit = nkmunitData;
					}
					for (int j = 0; j < 4; j++)
					{
						long equipUid = gameUnitData.unit.GetEquipUid((ITEM_EQUIP_POSITION)j);
						if (equipUid > 0L)
						{
							NKMEquipItemData itemEquip = inventoryData.GetItemEquip(equipUid);
							if (itemEquip != null)
							{
								NKMEquipItemData nkmequipItemData = new NKMEquipItemData();
								nkmequipItemData.DeepCopyFrom(itemEquip);
								gameUnitData.equip_item_list.Add(nkmequipItemData);
							}
						}
					}
					list.Add(gameUnitData);
				}
			}
			return list;
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x0007379C File Offset: 0x0007199C
		public static NKMOperator MakeEventDeckOperatorData(NKMArmyData cNKMArmyData, NKMDungeonEventDeckTemplet eventDeckTemplet, NKMEventDeckData eventDeckData)
		{
			return NKMDungeonManager.MakeEventDeckOperator(cNKMArmyData, eventDeckTemplet, eventDeckTemplet.OperatorSlot, eventDeckData.m_OperatorUID);
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x000737B1 File Offset: 0x000719B1
		public static NKMUnitData MakeEventDeckShipData(NKMArmyData cNKMArmyData, NKMDungeonEventDeckTemplet eventDeckTemplet, NKMEventDeckData eventDeckData)
		{
			return NKMDungeonManager.MakeEventDeckUnit(cNKMArmyData, eventDeckTemplet, eventDeckTemplet.ShipSlot, eventDeckData.m_ShipUID, NKM_UNIT_TYPE.NUT_SHIP);
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x000737C8 File Offset: 0x000719C8
		public static List<GameUnitData> MakeEventDeckUnitDataList(NKMArmyData cNKMArmyData, NKMDungeonEventDeckTemplet eventDeckTemplet, NKMEventDeckData eventDeckData, NKMInventoryData inventoryData)
		{
			List<GameUnitData> list = new List<GameUnitData>();
			for (int i = 0; i < 8; i++)
			{
				NKMDungeonEventDeckTemplet.EventDeckSlot unitSlot = eventDeckTemplet.GetUnitSlot(i);
				long unitUID = eventDeckData.GetUnitUID(i);
				NKMUnitData nkmunitData = NKMDungeonManager.MakeEventDeckUnit(cNKMArmyData, eventDeckTemplet, unitSlot, unitUID, NKM_UNIT_TYPE.NUT_NORMAL);
				if (nkmunitData != null)
				{
					GameUnitData gameUnitData = new GameUnitData();
					gameUnitData.unit = nkmunitData;
					for (int j = 0; j < 4; j++)
					{
						long equipUid = gameUnitData.unit.GetEquipUid((ITEM_EQUIP_POSITION)j);
						if (equipUid > 0L)
						{
							NKMEquipItemData itemEquip = inventoryData.GetItemEquip(equipUid);
							if (itemEquip != null)
							{
								NKMEquipItemData nkmequipItemData = new NKMEquipItemData();
								nkmequipItemData.DeepCopyFrom(itemEquip);
								gameUnitData.equip_item_list.Add(nkmequipItemData);
							}
						}
					}
					list.Add(gameUnitData);
				}
			}
			return list;
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x00073879 File Offset: 0x00071A79
		public static bool IsTutorialDungeon(int dungeonID)
		{
			return dungeonID - 1004 <= 3 || dungeonID - 20001 <= 4;
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x00073894 File Offset: 0x00071A94
		public static float GetBossHp(int dungeonID, int fixedBossLevel)
		{
			NKMDungeonTemplet dungeonTemplet = NKMDungeonManager.GetDungeonTemplet(dungeonID);
			if (dungeonTemplet == null)
			{
				return 0f;
			}
			NKMUnitTemplet unitTemplet = NKMUnitManager.GetUnitTemplet(dungeonTemplet.m_BossUnitStrID);
			if (unitTemplet == null)
			{
				return 0f;
			}
			return NKMUnitStatManager.MakeFinalStat(new NKMUnitData
			{
				m_UnitID = unitTemplet.m_UnitTempletBase.m_UnitID,
				m_UnitLevel = ((fixedBossLevel > 0) ? fixedBossLevel : dungeonTemplet.m_BossUnitLevel)
			}, null, null).GetStatFinal(NKM_STAT_TYPE.NST_HP);
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x000738FC File Offset: 0x00071AFC
		private static int GetFixedTeamBUnitLevel(int baseLevel, NKMGameData gameData)
		{
			if (gameData.m_TeamBLevelFix > 0)
			{
				return gameData.m_TeamBLevelFix;
			}
			return baseLevel + gameData.m_TeamBLevelAdd;
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x00073918 File Offset: 0x00071B18
		public static void CheckValidCutScen()
		{
			foreach (KeyValuePair<int, NKMDungeonTemplet> keyValuePair in NKMDungeonManager.m_dicNKMDungeonTempletByID)
			{
				NKMDungeonTempletBase dungeonTempletBase = keyValuePair.Value.m_DungeonTempletBase;
				if (dungeonTempletBase.m_CutScenStrIDBefore != "" && NKCCutScenManager.GetCutScenTemple(dungeonTempletBase.m_CutScenStrIDBefore) == null)
				{
					Log.Error("NKMDungeonTempletBase can't find m_CutScenStrIDBefore : " + dungeonTempletBase.m_CutScenStrIDBefore, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMDungeonManagerEx.cs", 107);
				}
				if (dungeonTempletBase.m_CutScenStrIDAfter != "" && NKCCutScenManager.GetCutScenTemple(dungeonTempletBase.m_CutScenStrIDAfter) == null)
				{
					Log.Error("NKMDungeonTempletBase can't find m_CutScenStrIDAfter : " + dungeonTempletBase.m_CutScenStrIDAfter, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMDungeonManagerEx.cs", 117);
				}
			}
			NKCCutScenManager.ClearCacheData();
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x000739D4 File Offset: 0x00071BD4
		public static string GetDungeonStrID(int dungeonID)
		{
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(dungeonID);
			if (dungeonTempletBase != null)
			{
				return dungeonTempletBase.m_DungeonStrID;
			}
			return "";
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x000739F8 File Offset: 0x00071BF8
		public static int GetCurrentMissionValue(NKMGame game, DUNGEON_GAME_MISSION_TYPE missionType)
		{
			NKMGameData gameData = game.GetGameData();
			NKMGameRuntimeData gameRuntimeData = game.GetGameRuntimeData();
			if (gameData == null || gameRuntimeData == null)
			{
				return 0;
			}
			int num = 0;
			switch (missionType)
			{
			case DUNGEON_GAME_MISSION_TYPE.DGMT_CLEAR:
				if (gameRuntimeData.m_WinTeam == NKM_TEAM_TYPE.NTT_A1 || gameRuntimeData.m_WinTeam == NKM_TEAM_TYPE.NTT_A2)
				{
					num++;
				}
				break;
			case DUNGEON_GAME_MISSION_TYPE.DGMT_TIME:
				num = (int)gameRuntimeData.GetGamePlayTime();
				break;
			case DUNGEON_GAME_MISSION_TYPE.DGMT_COST:
				num = (int)gameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fUsedRespawnCost;
				break;
			case DUNGEON_GAME_MISSION_TYPE.DGMT_RESPAWN:
				num = gameRuntimeData.m_NKMGameRuntimeTeamDataA.m_respawn_count;
				break;
			case DUNGEON_GAME_MISSION_TYPE.DGMT_SHIP_HP_DAMAGE:
			{
				float statFinal = NKMUnitStatManager.MakeFinalStat(gameData.m_NKMGameTeamDataA.m_MainShip, null, null).GetStatFinal(NKM_STAT_TYPE.NST_HP);
				float num2;
				if (NKCPhaseManager.IsPhaseOnGoing() && NKCPhaseManager.IsCurrentPhaseDungeon(gameData.m_DungeonID))
				{
					num2 = statFinal;
				}
				else
				{
					num2 = gameData.m_NKMGameTeamDataA.m_fInitHP;
					if (num2 == 0f)
					{
						num2 = statFinal;
					}
				}
				float num3 = num2 / statFinal;
				float num4 = NKCScenManager.GetScenManager().GetGameClient().GetLiveShipHPRate(NKM_TEAM_TYPE.NTT_A1);
				if (num4 == 0f)
				{
					num4 = num3;
				}
				num = Mathf.FloorToInt((num3 - num4) * 100f);
				break;
			}
			case DUNGEON_GAME_MISSION_TYPE.DGMT_DECKCOUNT_SOLDIER:
				num = NKMDungeonManager.DeckUnitTypeCount(gameData, NKM_UNIT_STYLE_TYPE.NUST_SOLDIER);
				break;
			case DUNGEON_GAME_MISSION_TYPE.DGMT_DECKCOUNT_MECHANIC:
				num = NKMDungeonManager.DeckUnitTypeCount(gameData, NKM_UNIT_STYLE_TYPE.NUST_MECHANIC);
				break;
			case DUNGEON_GAME_MISSION_TYPE.DGMT_DECKCOUNT_COUNTER:
				num = NKMDungeonManager.DeckUnitTypeCount(gameData, NKM_UNIT_STYLE_TYPE.NUST_COUNTER);
				break;
			case DUNGEON_GAME_MISSION_TYPE.DGMT_DECKCOUNT_DEFENDER:
				num = NKMDungeonManager.DeckUnitTypeCount(gameData, NKM_UNIT_ROLE_TYPE.NURT_DEFENDER);
				break;
			case DUNGEON_GAME_MISSION_TYPE.DGMT_DECKCOUNT_STRIKER:
				num = NKMDungeonManager.DeckUnitTypeCount(gameData, NKM_UNIT_ROLE_TYPE.NURT_STRIKER);
				break;
			case DUNGEON_GAME_MISSION_TYPE.DGMT_DECKCOUNT_RANGER:
				num = NKMDungeonManager.DeckUnitTypeCount(gameData, NKM_UNIT_ROLE_TYPE.NURT_RANGER);
				break;
			case DUNGEON_GAME_MISSION_TYPE.DGMT_DECKCOUNT_SNIPER:
				num = NKMDungeonManager.DeckUnitTypeCount(gameData, NKM_UNIT_ROLE_TYPE.NURT_SNIPER);
				break;
			case DUNGEON_GAME_MISSION_TYPE.DGMT_DECKCOUNT_TOWER:
				num = NKMDungeonManager.DeckUnitTypeCount(gameData, NKM_UNIT_ROLE_TYPE.NURT_TOWER);
				break;
			case DUNGEON_GAME_MISSION_TYPE.DGMT_DECKCOUNT_SIEGE:
				num = NKMDungeonManager.DeckUnitTypeCount(gameData, NKM_UNIT_ROLE_TYPE.NURT_SIEGE);
				break;
			case DUNGEON_GAME_MISSION_TYPE.DGMT_DECKCOUNT_SUPPORTER:
				num = NKMDungeonManager.DeckUnitTypeCount(gameData, NKM_UNIT_ROLE_TYPE.NURT_SUPPORTER);
				break;
			}
			return num;
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x00073BB8 File Offset: 0x00071DB8
		private static int DeckUnitTypeCount(NKMGameData gameData, NKM_UNIT_STYLE_TYPE unitType)
		{
			if (gameData == null)
			{
				return 0;
			}
			int num = 0;
			using (List<NKMUnitData>.Enumerator enumerator = gameData.m_NKMGameTeamDataA.m_listUnitData.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (NKMUnitManager.GetUnitTempletBase(enumerator.Current.m_UnitID).m_NKM_UNIT_STYLE_TYPE == unitType)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x00073C28 File Offset: 0x00071E28
		private static int DeckUnitTypeCount(NKMGameData gameData, NKM_UNIT_ROLE_TYPE unitRole)
		{
			if (gameData == null)
			{
				return 0;
			}
			int num = 0;
			using (List<NKMUnitData>.Enumerator enumerator = gameData.m_NKMGameTeamDataA.m_listUnitData.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (NKMUnitManager.GetUnitTempletBase(enumerator.Current.m_UnitID).m_NKM_UNIT_ROLE_TYPE == unitRole)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x00073C98 File Offset: 0x00071E98
		public static NKM_ERROR_CODE CanCounterCaseUnlock(NKMUserData userData, NKMDungeonTemplet dungeonTemplet)
		{
			NKMStageTempletV2 stageTemplet = dungeonTemplet.m_DungeonTempletBase.StageTemplet;
			if (stageTemplet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_LOCKED_EPISODE;
			}
			if (!NKMEpisodeMgr.CheckEpisodeMission(userData, stageTemplet))
			{
				return NKM_ERROR_CODE.NEC_FAIL_LOCKED_EPISODE;
			}
			if (userData.CheckUnlockedCounterCase(dungeonTemplet.m_DungeonTempletBase.m_DungeonID))
			{
				return NKM_ERROR_CODE.NEC_FAIL_COUNTERCASE_ALREADY_UNLOCKED;
			}
			if (userData.GetInformation() < (long)stageTemplet.m_StageReqItemCount)
			{
				return NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_INFORMATION;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x00073CF0 File Offset: 0x00071EF0
		public static Dictionary<string, NKCEnemyData> GetEnemyUnits(NKMStageTempletV2 stageTemplet)
		{
			if (stageTemplet == null)
			{
				return new Dictionary<string, NKCEnemyData>();
			}
			switch (stageTemplet.m_STAGE_TYPE)
			{
			case STAGE_TYPE.ST_WARFARE:
				return NKMDungeonManager.GetEnemyUnits(stageTemplet.WarfareTemplet);
			case STAGE_TYPE.ST_DUNGEON:
				return NKMDungeonManager.GetEnemyUnits(stageTemplet.DungeonTempletBase);
			case STAGE_TYPE.ST_PHASE:
				return NKMDungeonManager.GetEnemyUnits(stageTemplet.PhaseTemplet);
			default:
				return new Dictionary<string, NKCEnemyData>();
			}
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x00073D4C File Offset: 0x00071F4C
		public static Dictionary<string, NKCEnemyData> GetEnemyUnits(NKMWarfareTemplet cNKMWarfareTemplet)
		{
			Dictionary<string, NKCEnemyData> result = new Dictionary<string, NKCEnemyData>();
			if (cNKMWarfareTemplet == null)
			{
				return result;
			}
			NKMWarfareMapTemplet mapTemplet = cNKMWarfareTemplet.MapTemplet;
			if (mapTemplet == null)
			{
				return result;
			}
			foreach (string dungeonStrID in mapTemplet.GetDungeonStrIDList())
			{
				NKMDungeonTemplet dungeonTemplet = NKMDungeonManager.GetDungeonTemplet(dungeonStrID);
				if (dungeonTemplet != null)
				{
					bool isWarfareBossDungeon = dungeonTemplet.m_DungeonTempletBase.m_DungeonStrID == mapTemplet.GetFlagDungeonStrID();
					NKMDungeonManager.AddEnemyUnits(dungeonTemplet, ref result, isWarfareBossDungeon);
				}
			}
			return result;
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x00073DD4 File Offset: 0x00071FD4
		public static Dictionary<string, NKCEnemyData> GetEnemyUnits(NKMDungeonTempletBase cNKMDungeonTempletBase)
		{
			Dictionary<string, NKCEnemyData> result = new Dictionary<string, NKCEnemyData>();
			if (cNKMDungeonTempletBase == null)
			{
				return result;
			}
			NKMDungeonTemplet dungeonTemplet = NKMDungeonManager.GetDungeonTemplet(cNKMDungeonTempletBase.m_DungeonStrID);
			if (dungeonTemplet == null)
			{
				return result;
			}
			NKMDungeonManager.AddEnemyUnits(dungeonTemplet, ref result, false);
			return result;
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x00073E08 File Offset: 0x00072008
		public static Dictionary<string, NKCEnemyData> GetEnemyUnits(NKMPhaseTemplet phaseTemplet)
		{
			Dictionary<string, NKCEnemyData> result = new Dictionary<string, NKCEnemyData>();
			if (phaseTemplet == null || phaseTemplet.PhaseList == null)
			{
				return result;
			}
			for (int i = 0; i < phaseTemplet.PhaseList.List.Count; i++)
			{
				NKMDungeonTemplet dungeonTemplet = NKMDungeonManager.GetDungeonTemplet(phaseTemplet.PhaseList.List[i].Dungeon.m_DungeonID);
				if (dungeonTemplet != null)
				{
					NKMDungeonManager.AddEnemyUnits(dungeonTemplet, ref result, false);
				}
			}
			return result;
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x00073E74 File Offset: 0x00072074
		private static void AddEnemyUnits(NKMDungeonTemplet dungeonTemplet, ref Dictionary<string, NKCEnemyData> dicEnemyUnitIDs, bool isWarfareBossDungeon)
		{
			if (dungeonTemplet == null)
			{
				return;
			}
			if (!string.IsNullOrEmpty(dungeonTemplet.m_BossUnitStrID))
			{
				string key = (!string.IsNullOrEmpty(dungeonTemplet.m_BossUnitChangeName)) ? dungeonTemplet.m_BossUnitChangeName : dungeonTemplet.m_BossUnitStrID;
				NKCEnemyData nkcenemyData = null;
				if (dicEnemyUnitIDs.TryGetValue(key, out nkcenemyData))
				{
					if (nkcenemyData.m_Level < dungeonTemplet.m_BossUnitLevel)
					{
						dicEnemyUnitIDs[key].m_Level = dungeonTemplet.m_BossUnitLevel;
					}
					nkcenemyData.m_NKM_BOSS_TYPE = (isWarfareBossDungeon ? NKM_BOSS_TYPE.NBT_WARFARE_BOSS : NKM_BOSS_TYPE.NBT_DUNGEON_BOSS);
				}
				else
				{
					nkcenemyData = new NKCEnemyData();
					nkcenemyData.m_UnitStrID = dungeonTemplet.m_BossUnitStrID;
					nkcenemyData.m_ChangeUnitName = dungeonTemplet.m_BossUnitChangeName;
					nkcenemyData.m_Level = dungeonTemplet.m_BossUnitLevel;
					nkcenemyData.m_NKM_BOSS_TYPE = (isWarfareBossDungeon ? NKM_BOSS_TYPE.NBT_WARFARE_BOSS : NKM_BOSS_TYPE.NBT_DUNGEON_BOSS);
					dicEnemyUnitIDs.Add(key, nkcenemyData);
				}
			}
			if (dungeonTemplet.m_BossRespawnUnitTemplet != null)
			{
				NKCEnemyData nkcenemyData2 = NKMDungeonManager.AddEnemyUnits(ref dicEnemyUnitIDs, dungeonTemplet.m_BossRespawnUnitTemplet);
				if (nkcenemyData2 != null)
				{
					nkcenemyData2.m_NKM_BOSS_TYPE = (isWarfareBossDungeon ? NKM_BOSS_TYPE.NBT_WARFARE_BOSS : NKM_BOSS_TYPE.NBT_DUNGEON_BOSS);
				}
			}
			if (dungeonTemplet.m_listDungeonDeck != null)
			{
				for (int i = 0; i < dungeonTemplet.m_listDungeonDeck.Count; i++)
				{
					NKMDungeonRespawnUnitTemplet cNKMDungeonRespawnUnitTemplet = dungeonTemplet.m_listDungeonDeck[i];
					NKMDungeonManager.AddEnemyUnits(ref dicEnemyUnitIDs, cNKMDungeonRespawnUnitTemplet);
				}
			}
			if (dungeonTemplet.m_listDungeonWave != null)
			{
				for (int j = 0; j < dungeonTemplet.m_listDungeonWave.Count; j++)
				{
					NKMDungeonWaveTemplet nkmdungeonWaveTemplet = dungeonTemplet.m_listDungeonWave[j];
					if (nkmdungeonWaveTemplet != null)
					{
						for (int k = 0; k < nkmdungeonWaveTemplet.m_listDungeonUnitRespawnB.Count; k++)
						{
							NKMDungeonRespawnUnitTemplet cNKMDungeonRespawnUnitTemplet2 = nkmdungeonWaveTemplet.m_listDungeonUnitRespawnB[k];
							NKMDungeonManager.AddEnemyUnits(ref dicEnemyUnitIDs, cNKMDungeonRespawnUnitTemplet2);
						}
					}
				}
			}
			if (dungeonTemplet.m_listDungeonUnitRespawnB != null)
			{
				for (int l = 0; l < dungeonTemplet.m_listDungeonUnitRespawnB.Count; l++)
				{
					NKMDungeonRespawnUnitTemplet cNKMDungeonRespawnUnitTemplet3 = dungeonTemplet.m_listDungeonUnitRespawnB[l];
					NKMDungeonManager.AddEnemyUnits(ref dicEnemyUnitIDs, cNKMDungeonRespawnUnitTemplet3);
				}
			}
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x0007402C File Offset: 0x0007222C
		private static NKCEnemyData AddEnemyUnits(ref Dictionary<string, NKCEnemyData> dicEnemyUnitIDs, NKMDungeonRespawnUnitTemplet cNKMDungeonRespawnUnitTemplet)
		{
			if (dicEnemyUnitIDs == null || cNKMDungeonRespawnUnitTemplet == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(cNKMDungeonRespawnUnitTemplet.m_UnitStrID))
			{
				return null;
			}
			NKCEnemyData nkcenemyData;
			if (dicEnemyUnitIDs.TryGetValue(cNKMDungeonRespawnUnitTemplet.StrKey, out nkcenemyData))
			{
				if (nkcenemyData.m_Level < cNKMDungeonRespawnUnitTemplet.m_UnitLevel)
				{
					dicEnemyUnitIDs[cNKMDungeonRespawnUnitTemplet.StrKey].m_Level = cNKMDungeonRespawnUnitTemplet.m_UnitLevel;
					dicEnemyUnitIDs[cNKMDungeonRespawnUnitTemplet.StrKey].m_SkinID = cNKMDungeonRespawnUnitTemplet.m_SkinID;
				}
			}
			else
			{
				nkcenemyData = new NKCEnemyData();
				nkcenemyData.m_UnitStrID = cNKMDungeonRespawnUnitTemplet.m_UnitStrID;
				nkcenemyData.m_Level = cNKMDungeonRespawnUnitTemplet.m_UnitLevel;
				nkcenemyData.m_SkinID = cNKMDungeonRespawnUnitTemplet.m_SkinID;
				nkcenemyData.m_ChangeUnitName = cNKMDungeonRespawnUnitTemplet.m_ChangeUnitName;
				dicEnemyUnitIDs.Add(cNKMDungeonRespawnUnitTemplet.StrKey, nkcenemyData);
			}
			return nkcenemyData;
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x000740E8 File Offset: 0x000722E8
		public static NKMEventDeckData LoadDungeonDeck(NKMStageTempletV2 stageTemplet)
		{
			string curEventDeckKey = NKMDungeonManager.GetCurEventDeckKey(stageTemplet.DungeonTempletBase, stageTemplet, DeckContents.NORMAL);
			return NKMDungeonManager.LoadDungeonDeck(stageTemplet.GetEventDeckTemplet(), curEventDeckKey);
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x00074110 File Offset: 0x00072310
		public static NKMEventDeckData LoadDungeonDeck(NKMDungeonEventDeckTemplet targetEventDeckTemplet, string eventDeckKey)
		{
			if (string.IsNullOrEmpty(eventDeckKey))
			{
				return null;
			}
			if (!PlayerPrefs.HasKey(eventDeckKey))
			{
				return null;
			}
			NKMEventDeckData nkmeventDeckData = new NKMEventDeckData();
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			string @string = PlayerPrefs.GetString(eventDeckKey);
			foreach (string text in @string.Split(new char[]
			{
				'&'
			}))
			{
				int num = text.IndexOf('/');
				if (num < 0)
				{
					break;
				}
				int num2;
				int.TryParse(text.Substring(0, num), out num2);
				long num3;
				long.TryParse(text.Substring(num + 1, text.Length - (num + 1)), out num3);
				NKMUnitData unitFromUID = armyData.GetUnitFromUID(num3);
				if (targetEventDeckTemplet.IsUnitFitInSlot(targetEventDeckTemplet.GetUnitSlot(num2), unitFromUID))
				{
					nkmeventDeckData.m_dicUnit.Add(num2, num3);
				}
			}
			int num4 = @string.IndexOf('_') + 1;
			if (num4 > 0)
			{
				int num5 = @string.IndexOf('o');
				int num6 = @string.IndexOf('l');
				int num7 = @string.Length - num4;
				if (num5 > 0)
				{
					num7 -= @string.Length - num5;
				}
				else if (num6 > 0)
				{
					num7 -= @string.Length - num6;
				}
				long num8;
				long.TryParse(@string.Substring(num4, num7), out num8);
				NKMUnitData shipFromUID = armyData.GetShipFromUID(num8);
				if (targetEventDeckTemplet.IsUnitFitInSlot(targetEventDeckTemplet.ShipSlot, shipFromUID))
				{
					nkmeventDeckData.m_ShipUID = num8;
				}
			}
			int num9 = @string.IndexOf('|') + 1;
			if (num9 > 0)
			{
				int num10 = @string.IndexOf('l');
				int num11 = @string.Length - num9;
				if (num10 > 0)
				{
					num11 -= @string.Length - num10;
				}
				long num12;
				long.TryParse(@string.Substring(num9, num11), out num12);
				NKMOperator operatorFromUId = armyData.GetOperatorFromUId(num12);
				if (targetEventDeckTemplet.IsOperatorFitInSlot(operatorFromUId))
				{
					nkmeventDeckData.m_OperatorUID = num12;
				}
			}
			int num13 = @string.IndexOf('^') + 1;
			if (num13 > 0)
			{
				int length = @string.Length - num13;
				string text2 = @string.Substring(num13, length);
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				long num14 = (nkmuserData != null) ? nkmuserData.m_UserUID : 0L;
				long num15 = 0L;
				if (text2.Length > 1)
				{
					long.TryParse(text2.Substring(1), out num15);
				}
				int leaderIndex;
				if (num15 == num14 && int.TryParse(text2.Substring(0, 1), out leaderIndex))
				{
					nkmeventDeckData.m_LeaderIndex = leaderIndex;
				}
			}
			return nkmeventDeckData;
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x00074358 File Offset: 0x00072558
		public static string GetCurEventDeckKey(NKMDungeonTempletBase dungeonTempletBase, NKMStageTempletV2 stageTemplet, DeckContents deckContent)
		{
			if (dungeonTempletBase != null)
			{
				if (deckContent == DeckContents.FIERCE_BATTLE_SUPPORT)
				{
					NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
					if (nkcfierceBattleSupportDataMgr != null && nkcfierceBattleSupportDataMgr.GetStatus() == NKCFierceBattleSupportDataMgr.FIERCE_STATUS.FS_ACTIVATE)
					{
						foreach (NKMFierceBossGroupTemplet nkmfierceBossGroupTemplet in NKMFierceBossGroupTemplet.Values)
						{
							if (nkmfierceBossGroupTemplet.DungeonID == dungeonTempletBase.m_DungeonID)
							{
								return string.Format(string.Format("NKM_PREPARE_EVENT_DECK_F_{0}", nkmfierceBossGroupTemplet.FierceBossGroupID), Array.Empty<object>());
							}
						}
					}
				}
				return string.Format(string.Format("NKM_PREPARE_EVENT_DECK_{0}", dungeonTempletBase.m_DungeonID), Array.Empty<object>());
			}
			if (stageTemplet != null && stageTemplet.PhaseTemplet != null)
			{
				return string.Format(string.Format("NKM_PREPARE_EVENT_DECK_{0}", stageTemplet.PhaseTemplet.Id), Array.Empty<object>());
			}
			return "";
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x00074448 File Offset: 0x00072648
		public static bool HasSavedDungeonDeck(NKMStageTempletV2 stageTemplet)
		{
			string curEventDeckKey = NKMDungeonManager.GetCurEventDeckKey(stageTemplet.DungeonTempletBase, stageTemplet, DeckContents.NORMAL);
			return !string.IsNullOrEmpty(curEventDeckKey) && PlayerPrefs.HasKey(curEventDeckKey);
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x00074474 File Offset: 0x00072674
		public static void SaveDungeonDeck(NKMEventDeckData eventDeckData, string eventDeckKey)
		{
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			StringBuilder stringBuilder = new StringBuilder();
			if (string.IsNullOrEmpty(eventDeckKey))
			{
				return;
			}
			for (int i = 0; i < 8; i++)
			{
				if (eventDeckData.m_dicUnit.ContainsKey(i))
				{
					stringBuilder.Append(string.Format("{0}/{1}&", i, eventDeckData.m_dicUnit[i]));
				}
			}
			if (eventDeckData.m_ShipUID != 0L && armyData.IsHaveShipFromUID(eventDeckData.m_ShipUID))
			{
				stringBuilder.Append(string.Format("s_{0}", eventDeckData.m_ShipUID));
			}
			if (eventDeckData.m_OperatorUID != 0L && armyData.IsHaveOperatorFromUID(eventDeckData.m_OperatorUID))
			{
				stringBuilder.Append(string.Format("o|{0}", eventDeckData.m_OperatorUID));
			}
			if (eventDeckData.m_LeaderIndex != 0)
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				long num = (nkmuserData != null) ? nkmuserData.m_UserUID : 0L;
				stringBuilder.Append(string.Format("l^{0}{1}", eventDeckData.m_LeaderIndex, num));
			}
			PlayerPrefs.SetString(eventDeckKey, stringBuilder.ToString());
		}

		// Token: 0x040013C3 RID: 5059
		private static Dictionary<long, NKMDungeonRespawnUnitTemplet> m_dicNKMDungeonRespawnUnitTemplet = new Dictionary<long, NKMDungeonRespawnUnitTemplet>();

		// Token: 0x040013C4 RID: 5060
		public static Dictionary<int, NKMDungeonTemplet> m_dicNKMDungeonTempletByID = new Dictionary<int, NKMDungeonTemplet>();

		// Token: 0x040013C5 RID: 5061
		public static Dictionary<string, NKMDungeonTemplet> m_dicNKMDungeonTempletByStrID = new Dictionary<string, NKMDungeonTemplet>();

		// Token: 0x040013C6 RID: 5062
		public static Dictionary<int, NKMDungeonEventDeckTemplet> m_dicNKMDungeonEventDeckTemplet = null;

		// Token: 0x040013C7 RID: 5063
		private static string m_DungeonTempletBaseFileName = "";

		// Token: 0x040013C8 RID: 5064
		private static string m_DungeonTempletFolderName = "";
	}
}
