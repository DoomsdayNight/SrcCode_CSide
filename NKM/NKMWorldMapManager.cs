using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.WorldMap;
using Cs.Logging;
using NKC;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x02000515 RID: 1301
	public sealed class NKMWorldMapManager
	{
		// Token: 0x06002528 RID: 9512 RVA: 0x000BFC20 File Offset: 0x000BDE20
		public static bool LoadFromLUA()
		{
			bool flag = true;
			NKMWorldMapManager.m_dicCityExpTemplet = NKMTempletLoader.LoadDictionary<NKMWorldMapCityExpTemplet>("AB_SCRIPT", "LUA_WORLDMAP_CITY_EXP_TABLE", "m_WorldmapCityExpTable", new Func<NKMLua, NKMWorldMapCityExpTemplet>(NKMWorldMapCityExpTemplet.LoadFromLUA));
			bool flag2 = flag & NKMWorldMapManager.m_dicCityExpTemplet != null;
			NKMWorldMapManager.m_dicCityTemplet = NKMTempletLoader.LoadDictionary<NKMWorldMapCityTemplet>("AB_SCRIPT", "LUA_WORLDMAP_CITY_TEMPLET", "m_WorldmapCityTemplet", new Func<NKMLua, NKMWorldMapCityTemplet>(NKMWorldMapCityTemplet.LoadFromLUA));
			bool result = flag2 & NKMWorldMapManager.m_dicCityTemplet != null;
			NKMTempletContainer<NKMWorldMapMissionTemplet>.Load("AB_SCRIPT", "LUA_WORLDMAP_MISSION_TEMPLET", "m_WorldmapMissionTemplet", new Func<NKMLua, NKMWorldMapMissionTemplet>(NKMWorldMapMissionTemplet.LoadFromLUA));
			NKMTempletContainer<NKMWorldMapEventTemplet>.Load("AB_SCRIPT", "LUA_WORLDMAP_EVENT_GROUP", "m_WorldmapEventGroup", new Func<NKMLua, NKMWorldMapEventTemplet>(NKMWorldMapEventTemplet.LoadFromLUA));
			IEnumerable<NKMWorldMapBuildingTemplet> enumerable = from e in NKMTempletLoader<NKMWorldMapBuildingTemplet.LevelTemplet>.LoadGroup("AB_SCRIPT", "LUA_WORLDMAP_CITY_BUILDING", "m_WorldmapCityBuildingTemplet", new Func<NKMLua, NKMWorldMapBuildingTemplet.LevelTemplet>(NKMWorldMapBuildingTemplet.LevelTemplet.LoadFromLUA))
			select new NKMWorldMapBuildingTemplet(e.Key, e.Value);
			NKMTempletContainer<NKMWorldMapBuildingTemplet>.Load(enumerable, null);
			NKMWorldMapManager.m_dicBuildTempletByStatEnum = (from e in enumerable
			group e by e.StatType).ToDictionary((IGrouping<NKM_CITY_BUILDING_STAT, NKMWorldMapBuildingTemplet> e) => e.Key, (IGrouping<NKM_CITY_BUILDING_STAT, NKMWorldMapBuildingTemplet> e) => e.ToList<NKMWorldMapBuildingTemplet>());
			return result;
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x000BFD84 File Offset: 0x000BDF84
		public static NKMWorldMapCityTemplet GetCityTemplet(int id)
		{
			NKMWorldMapCityTemplet result;
			if (NKMWorldMapManager.m_dicCityTemplet.TryGetValue(id, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x000BFDA4 File Offset: 0x000BDFA4
		public static NKMWorldMapCityExpTemplet GetCityExpTable(int level)
		{
			NKMWorldMapCityExpTemplet result;
			if (NKMWorldMapManager.m_dicCityExpTemplet.TryGetValue(level, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x000BFDC3 File Offset: 0x000BDFC3
		public static NKMWorldMapMissionTemplet GetMissionTemplet(int id)
		{
			return NKMTempletContainer<NKMWorldMapMissionTemplet>.Find(id);
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x000BFDCB File Offset: 0x000BDFCB
		public static NKMWorldMapBuildingTemplet.LevelTemplet GetCityBuildingTemplet(int id, int level)
		{
			NKMWorldMapBuildingTemplet nkmworldMapBuildingTemplet = NKMWorldMapBuildingTemplet.Find(id);
			return ((nkmworldMapBuildingTemplet != null) ? nkmworldMapBuildingTemplet.GetLevelTemplet(level) : null) ?? null;
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x000BFDE8 File Offset: 0x000BDFE8
		public static int GetPossibleCityCount(int userLevel)
		{
			if (userLevel == 0)
			{
				return 0;
			}
			for (int i = 0; i < NKMWorldMapManager.lstCityUnlockLevel.Count; i++)
			{
				if (userLevel < NKMWorldMapManager.lstCityUnlockLevel[i])
				{
					return i - 1;
				}
			}
			return 6;
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x000BFE22 File Offset: 0x000BE022
		public static int GetNextAreaUnlockLevel(int currentCityCount)
		{
			if (currentCityCount < NKMWorldMapManager.lstCityUnlockLevel.Count - 1)
			{
				return NKMWorldMapManager.lstCityUnlockLevel[currentCityCount + 1];
			}
			return 99;
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x000BFE44 File Offset: 0x000BE044
		public static int GetTotalBuildingPointUsed(int buildingID, int currentLevel)
		{
			int num = 0;
			for (int i = 1; i <= currentLevel; i++)
			{
				NKMWorldMapBuildingTemplet.LevelTemplet cityBuildingTemplet = NKMWorldMapManager.GetCityBuildingTemplet(buildingID, i);
				if (cityBuildingTemplet != null)
				{
					num += cityBuildingTemplet.reqBuildingPoint;
				}
			}
			return num;
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x000BFE74 File Offset: 0x000BE074
		public static int GetTotalBuildingPointUsed(NKMWorldMapBuildingTemplet.LevelTemplet levelTemplet)
		{
			return NKMWorldMapManager.GetTotalBuildingPointUsed(levelTemplet.id, levelTemplet.level);
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x000BFE87 File Offset: 0x000BE087
		public static int GetTotalBuildingPointUsed(NKMWorldmapCityBuildingData buildingData)
		{
			return NKMWorldMapManager.GetTotalBuildingPointUsed(buildingData.id, buildingData.level);
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x000BFE9C File Offset: 0x000BE09C
		public static int GetUsableBuildPoint(NKMWorldMapCityData cityData)
		{
			if (cityData == null)
			{
				return 0;
			}
			int num = 0;
			foreach (KeyValuePair<int, NKMWorldmapCityBuildingData> keyValuePair in cityData.worldMapCityBuildingDataMap)
			{
				num += NKMWorldMapManager.GetTotalBuildingPointUsed(keyValuePair.Value);
			}
			return cityData.level - num;
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x000BFF08 File Offset: 0x000BE108
		public static bool IsMissionLeaderOnly(NKMWorldMapMissionTemplet.WorldMapMissionType missionType)
		{
			return true;
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x000BFF0C File Offset: 0x000BE10C
		public static int GetCityOpenCost(NKMWorldMapData worldMapData, bool isCash)
		{
			int unlockedCityCount = worldMapData.GetUnlockedCityCount();
			if (isCash)
			{
				if (unlockedCityCount < NKMConst.Worldmap.CITY_OPEN_CASH_COST.Count)
				{
					return NKMConst.Worldmap.CITY_OPEN_CASH_COST[unlockedCityCount];
				}
			}
			else if (unlockedCityCount < NKMConst.Worldmap.CITY_OPEN_CREDIT_COST.Count)
			{
				return NKMConst.Worldmap.CITY_OPEN_CREDIT_COST[unlockedCityCount];
			}
			return 0;
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x000BFF58 File Offset: 0x000BE158
		public static bool CanLevelup(int lev, int exp, out int need_credit)
		{
			need_credit = 0;
			NKMWorldMapCityExpTemplet nkmworldMapCityExpTemplet;
			if (NKMWorldMapManager.m_dicCityExpTemplet.TryGetValue(lev, out nkmworldMapCityExpTemplet) && nkmworldMapCityExpTemplet.m_ExpRequired != 0 && nkmworldMapCityExpTemplet.m_ExpRequired <= exp)
			{
				need_credit = nkmworldMapCityExpTemplet.m_LevelUpReqCredit;
				return true;
			}
			return false;
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x000BFF94 File Offset: 0x000BE194
		public static NKM_ERROR_CODE CanSetLeader(NKMUserData userData, long leaderUID)
		{
			if (leaderUID == 0L)
			{
				return NKM_ERROR_CODE.NEC_OK;
			}
			NKMUnitData unitFromUID = userData.m_ArmyData.GetUnitFromUID(leaderUID);
			if (unitFromUID == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_NOT_EXIST;
			}
			if (unitFromUID.IsSeized)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_IS_SEIZED;
			}
			if (!NKMUnitManager.CanUnitUsedInDeck(unitFromUID))
			{
				return NKM_ERROR_CODE.NEC_FAIL_NPT_DECK_UNIT_NOALLOWED_TYPE;
			}
			using (Dictionary<int, NKMWorldMapCityData>.ValueCollection.Enumerator enumerator = userData.m_WorldmapData.worldMapCityDataMap.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.leaderUnitUID == leaderUID)
					{
						return NKM_ERROR_CODE.NEC_FAIL_UNIT_ALREADY_USE;
					}
				}
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x000C0030 File Offset: 0x000BE230
		public static bool IsMissionRunning(NKMWorldMapCityData cityData)
		{
			return cityData != null && cityData.HasMission();
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x000C003D File Offset: 0x000BE23D
		public static bool IsMissionFinished(NKMWorldMapCityData cityData, DateTime CurrentTime)
		{
			return cityData != null && cityData.IsMissionFinished(CurrentTime);
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x000C004C File Offset: 0x000BE24C
		public static int GetMissionSuccessRate(NKMWorldMapMissionTemplet missionTemplet, NKMArmyData armyData, NKMWorldMapCityData cityData)
		{
			if (armyData == null || cityData == null)
			{
				return 0;
			}
			NKMUnitData unitFromUID = armyData.GetUnitFromUID(cityData.leaderUnitUID);
			if (unitFromUID == null)
			{
				Log.Error(string.Format("Invalid UnitData. leaderUnitUid : {0}", cityData.leaderUnitUID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMWorldMapManager.cs", 406);
				return 0;
			}
			float num = (((float)unitFromUID.m_UnitLevel - (float)missionTemplet.m_ReqManagerLevel) / 100f + 0.3f) * 100f;
			int num2 = (int)cityData.CalcBuildStat(NKM_CITY_BUILDING_STAT.CBS_MISSION_SUCCSSES_RATE, (float)((int)num));
			if (num2 > 70)
			{
				num2 = 70;
			}
			if (num2 < 30)
			{
				num2 = 30;
			}
			return num2;
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x000C00D8 File Offset: 0x000BE2D8
		public static float GetCityExpPercent(NKMWorldMapCityData cityData)
		{
			if (cityData == null)
			{
				return 0f;
			}
			NKMWorldMapCityExpTemplet cityExpTable = NKMWorldMapManager.GetCityExpTable(cityData.level);
			if (cityExpTable == null)
			{
				Log.Error("City Exp Table not found! city Lv : " + cityData.level.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMWorldMapManager.cs", 446);
				return 0f;
			}
			if (cityExpTable.m_ExpRequired == 0)
			{
				return 1f;
			}
			return (float)cityData.exp / (float)cityExpTable.m_ExpRequired;
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x000C0144 File Offset: 0x000BE344
		public static NKMWorldMapManager.WorldMapLeaderState GetUnitWorldMapLeaderState(NKMUserData userData, long unitUID, int currentCityID = -1)
		{
			if (unitUID == 0L || userData == null)
			{
				return NKMWorldMapManager.WorldMapLeaderState.None;
			}
			NKMDeckIndex nkmdeckIndex;
			sbyte b;
			userData.m_ArmyData.GetUnitDeckPosition(NKM_DECK_TYPE.NDT_NORMAL, unitUID, out nkmdeckIndex, out b);
			foreach (NKMWorldMapCityData nkmworldMapCityData in userData.m_WorldmapData.worldMapCityDataMap.Values)
			{
				if (nkmworldMapCityData.leaderUnitUID == unitUID)
				{
					if (currentCityID != -1 && currentCityID != nkmworldMapCityData.cityID)
					{
						return NKMWorldMapManager.WorldMapLeaderState.CityLeaderOther;
					}
					return NKMWorldMapManager.WorldMapLeaderState.CityLeader;
				}
			}
			return NKMWorldMapManager.WorldMapLeaderState.None;
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x000C01D8 File Offset: 0x000BE3D8
		public static NKM_ERROR_CODE CanBuild(NKMUserData userData, int cityID, int buildID)
		{
			if (userData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_USER_DATA_NULL;
			}
			NKMWorldMapCityData cityData = userData.m_WorldmapData.GetCityData(cityID);
			if (cityData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_CITY_ID;
			}
			NKMWorldMapBuildingTemplet nkmworldMapBuildingTemplet = NKMWorldMapBuildingTemplet.Find(buildID);
			if (nkmworldMapBuildingTemplet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_BUILD_ID;
			}
			NKMWorldMapBuildingTemplet.LevelTemplet levelTemplet = nkmworldMapBuildingTemplet.GetLevelTemplet(1);
			if (levelTemplet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_BUILD_LEVEL;
			}
			if (cityData.GetBuildingData(buildID) != null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_BUILD_AREADY_EXIST;
			}
			if (cityData.level < levelTemplet.reqCityLevel)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_BUILD_NOT_ENOUGH_LEVEL;
			}
			if (levelTemplet.reqBuildingID != 0)
			{
				NKMWorldmapCityBuildingData buildingData = cityData.GetBuildingData(levelTemplet.reqBuildingID);
				if (buildingData == null)
				{
					return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_BUILD_NOT_EXIST_REQ_BUILDING;
				}
				if (buildingData.level < levelTemplet.reqBuildingLevel)
				{
					return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_BUILD_NOT_ENOUGH_LEVEL;
				}
			}
			if (levelTemplet.reqClearDiveId != 0 && !NKCScenManager.CurrentUserData().CheckDiveHistory(levelTemplet.reqClearDiveId))
			{
				return NKM_ERROR_CODE.NEC_FAIL_DIVE_NOT_CLEARED;
			}
			if (levelTemplet.notBuildingTogether != 0 && cityData.GetBuildingData(levelTemplet.notBuildingTogether) != null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_NOT_BUILDING_TOGETHER;
			}
			foreach (NKMWorldMapBuildingTemplet.LevelTemplet.CostItem costItem in levelTemplet.BuildCostItems)
			{
				if (!userData.CheckPrice(costItem.Count, costItem.ItemID))
				{
					return NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_CREDIT;
				}
			}
			int reqBuildingPoint = levelTemplet.reqBuildingPoint;
			int usableBuildPoint = NKMWorldMapManager.GetUsableBuildPoint(cityData);
			if (reqBuildingPoint > usableBuildPoint)
			{
				return NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_BUILD_POINT;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x000C032C File Offset: 0x000BE52C
		public static NKM_ERROR_CODE CanLevelUpBuilding(NKMUserData userData, int cityID, int buildID)
		{
			if (userData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_USER_DATA_NULL;
			}
			NKMWorldMapCityData cityData = userData.m_WorldmapData.GetCityData(cityID);
			if (cityData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_CITY_ID;
			}
			NKMWorldMapBuildingTemplet nkmworldMapBuildingTemplet = NKMWorldMapBuildingTemplet.Find(buildID);
			if (nkmworldMapBuildingTemplet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_BUILD_ID;
			}
			NKMWorldmapCityBuildingData buildingData = cityData.GetBuildingData(buildID);
			if (buildingData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_BUILD_NOT_EXIST;
			}
			if (buildingData.level >= 10)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_BUILD_ALREADY_MAX_LEVEL;
			}
			int level = buildingData.level + 1;
			NKMWorldMapBuildingTemplet.LevelTemplet levelTemplet = nkmworldMapBuildingTemplet.GetLevelTemplet(level);
			if (levelTemplet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_BUILD_LEVEL;
			}
			if (cityData.level < levelTemplet.reqCityLevel)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_BUILD_NOT_ENOUGH_LEVEL;
			}
			if (levelTemplet.reqBuildingID != 0)
			{
				NKMWorldmapCityBuildingData buildingData2 = cityData.GetBuildingData(levelTemplet.reqBuildingID);
				if (buildingData2 == null)
				{
					return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_BUILD_NOT_EXIST_REQ_BUILDING;
				}
				if (buildingData2.level < levelTemplet.reqBuildingLevel)
				{
					return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_BUILD_NOT_ENOUGH_LEVEL;
				}
			}
			if (levelTemplet.reqClearDiveId != 0 && !NKCScenManager.CurrentUserData().CheckDiveHistory(levelTemplet.reqClearDiveId))
			{
				return NKM_ERROR_CODE.NEC_FAIL_DIVE_NOT_CLEARED;
			}
			foreach (NKMWorldMapBuildingTemplet.LevelTemplet.CostItem costItem in levelTemplet.BuildCostItems)
			{
				if (!userData.CheckPrice(costItem.Count, costItem.ItemID))
				{
					return NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_CREDIT;
				}
			}
			int reqBuildingPoint = levelTemplet.reqBuildingPoint;
			int usableBuildPoint = NKMWorldMapManager.GetUsableBuildPoint(cityData);
			if (reqBuildingPoint > usableBuildPoint)
			{
				return NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_BUILD_POINT;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x000C048C File Offset: 0x000BE68C
		public static NKM_ERROR_CODE CanExpireBuilding(NKMUserData userData, int cityID, int buildID)
		{
			if (userData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_USER_DATA_NULL;
			}
			if (buildID == 1)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_CANNOT_EXPIRE_COMMAND;
			}
			NKMWorldMapCityData cityData = userData.m_WorldmapData.GetCityData(cityID);
			if (cityData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_CITY_ID;
			}
			NKMWorldMapBuildingTemplet nkmworldMapBuildingTemplet = NKMWorldMapBuildingTemplet.Find(buildID);
			if (nkmworldMapBuildingTemplet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_BUILD_ID;
			}
			NKMWorldmapCityBuildingData buildingData = cityData.GetBuildingData(buildID);
			if (buildingData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_BUILD_NOT_EXIST;
			}
			NKMWorldMapBuildingTemplet.LevelTemplet levelTemplet = nkmworldMapBuildingTemplet.GetLevelTemplet(buildingData.level);
			if (levelTemplet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_BUILD_LEVEL;
			}
			if (!userData.CheckPrice(levelTemplet.ClearCostItem.Count, levelTemplet.ClearCostItem.ItemID))
			{
				return NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_CREDIT;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x000C051C File Offset: 0x000BE71C
		public static NKMWorldMapCityTemplet GetCityTemplet(string strID)
		{
			foreach (NKMWorldMapCityTemplet nkmworldMapCityTemplet in NKMWorldMapManager.m_dicCityTemplet.Values)
			{
				if (nkmworldMapCityTemplet.m_StrID == strID)
				{
					return nkmworldMapCityTemplet;
				}
			}
			return null;
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x000C0584 File Offset: 0x000BE784
		public static NKM_ERROR_CODE IsValidDeckForWorldMapMission(NKMUserData userData, NKMDeckIndex selectDeckIndex, int cityID)
		{
			if (userData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_USER_DATA_NULL;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = NKMMain.IsValidDeck(userData.m_ArmyData, selectDeckIndex);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				return nkm_ERROR_CODE;
			}
			NKMDeckData deckData = userData.m_ArmyData.GetDeckData(selectDeckIndex);
			if (deckData != null)
			{
				foreach (long unitUID in deckData.m_listDeckUnitUID)
				{
					if (NKMWorldMapManager.GetUnitWorldMapLeaderState(userData, unitUID, cityID) == NKMWorldMapManager.WorldMapLeaderState.CityLeaderOther)
					{
						return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_MISSION_DECK_HAS_UNIT_FROM_ANOTHER_CITY;
					}
				}
				return NKM_ERROR_CODE.NEC_OK;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x000C0614 File Offset: 0x000BE814
		public static NKMWorldMapEventTemplet GetWorldMapEventTempletByStageID(int stageID)
		{
			return NKMTempletContainer<NKMWorldMapEventTemplet>.Find((NKMWorldMapEventTemplet x) => x.stageID == stageID);
		}

		// Token: 0x04002688 RID: 9864
		public const int CITY_MAX_BUILDING_SLOT = 10;

		// Token: 0x04002689 RID: 9865
		public const int CITY_MISSION_GROUP_COUNT = 3;

		// Token: 0x0400268A RID: 9866
		public const int MAX_MISSION_REWARD_COUNT = 4;

		// Token: 0x0400268B RID: 9867
		public const int MISSION_REFRESH_REQUIRE_INFORMATION = 50;

		// Token: 0x0400268C RID: 9868
		public const int MINIMUM_START_MISSION_SUCCESS_RATE = 20;

		// Token: 0x0400268D RID: 9869
		public const int MAXINUM_SUCCESS_RATE = 70;

		// Token: 0x0400268E RID: 9870
		public const int MINIMUM_SUCCESS_RATE = 30;

		// Token: 0x0400268F RID: 9871
		public const int CITY_COMMAND_BUILDING_ID = 1;

		// Token: 0x04002690 RID: 9872
		public static Dictionary<int, NKMWorldMapCityTemplet> m_dicCityTemplet = null;

		// Token: 0x04002691 RID: 9873
		public static Dictionary<int, NKMWorldMapCityExpTemplet> m_dicCityExpTemplet = null;

		// Token: 0x04002692 RID: 9874
		public static Dictionary<NKM_CITY_BUILDING_STAT, List<NKMWorldMapBuildingTemplet>> m_dicBuildTempletByStatEnum = null;

		// Token: 0x04002693 RID: 9875
		private static readonly List<int> lstCityUnlockLevel = new List<int>
		{
			0,
			1,
			10,
			30,
			45,
			60,
			75
		};

		// Token: 0x0200123E RID: 4670
		public enum WorldMapLeaderState
		{
			// Token: 0x0400954F RID: 38223
			None,
			// Token: 0x04009550 RID: 38224
			CityLeader,
			// Token: 0x04009551 RID: 38225
			CityLeaderOther
		}
	}
}
