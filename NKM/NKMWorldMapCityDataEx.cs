using System;
using System.Collections.Generic;
using ClientPacket.WorldMap;
using Cs.Logging;
using NKM.Templet;

namespace NKM
{
	// Token: 0x02000509 RID: 1289
	public static class NKMWorldMapCityDataEx
	{
		// Token: 0x060024FB RID: 9467 RVA: 0x000BEE98 File Offset: 0x000BD098
		public static void AddBuild(this NKMWorldMapCityData cityData, NKMWorldmapCityBuildingData data)
		{
			cityData.worldMapCityBuildingDataMap.Add(data.id, data);
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x000BEEAC File Offset: 0x000BD0AC
		public static float CalcBuildStat(this NKMWorldMapCityData data, NKM_CITY_BUILDING_STAT buildingStat, float value)
		{
			switch (buildingStat)
			{
			case NKM_CITY_BUILDING_STAT.CBS_MISSION_CITY_EXP_RATE:
			case NKM_CITY_BUILDING_STAT.CBS_MISSION_UNIT_EXP_RATE:
			case NKM_CITY_BUILDING_STAT.CBS_MISSION_CREDIT_RATE:
			case NKM_CITY_BUILDING_STAT.CBS_MISSION_ETERNIUM_RATE:
			case NKM_CITY_BUILDING_STAT.CBS_MISSION_INFORMATION_RATE:
			case NKM_CITY_BUILDING_STAT.CBS_MISSION_TIME_REDUCE_RATE:
			case NKM_CITY_BUILDING_STAT.CBS_MISSION_RANK_SEARCH_RATE:
			case NKM_CITY_BUILDING_STAT.CBS_DIVE_INFORMATION_REDUCE_RATE:
			case NKM_CITY_BUILDING_STAT.CBS_RAID_DEFENCE_COST_REDUCE_RATE:
			{
				int num = 0;
				List<NKMWorldMapBuildingTemplet> list;
				if (!NKMWorldMapManager.m_dicBuildTempletByStatEnum.TryGetValue(buildingStat, out list))
				{
					return 0f;
				}
				foreach (NKMWorldMapBuildingTemplet nkmworldMapBuildingTemplet in list)
				{
					NKMWorldmapCityBuildingData buildingData = data.GetBuildingData(nkmworldMapBuildingTemplet.Key);
					if (buildingData != null)
					{
						NKMWorldMapBuildingTemplet.LevelTemplet levelTemplet = nkmworldMapBuildingTemplet.GetLevelTemplet(buildingData.level);
						if (levelTemplet == null)
						{
							Log.Error(string.Format("city level templet not found. cityId:{0} build id : {1}, level:{2}", data.cityID, nkmworldMapBuildingTemplet.Key, buildingData.level), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMWorldMap.cs", 47);
							return 0f;
						}
						num += levelTemplet.cityStatValue;
					}
				}
				return value * (float)num / 100f;
			}
			case NKM_CITY_BUILDING_STAT.CBS_MISSION_SUCCSSES_RATE:
			case NKM_CITY_BUILDING_STAT.CBS_EVENT_SPECIAL_SEARCH_RATE:
			case NKM_CITY_BUILDING_STAT.CBS_DIVE_SEARCH_RATE:
			case NKM_CITY_BUILDING_STAT.CBS_RAID_DEFENCE_LEVEL:
			case NKM_CITY_BUILDING_STAT.CBS_RAID_SEARCH_RATE:
			{
				List<NKMWorldMapBuildingTemplet> list2;
				if (!NKMWorldMapManager.m_dicBuildTempletByStatEnum.TryGetValue(buildingStat, out list2))
				{
					return value;
				}
				foreach (NKMWorldMapBuildingTemplet nkmworldMapBuildingTemplet2 in list2)
				{
					NKMWorldmapCityBuildingData buildingData2 = data.GetBuildingData(nkmworldMapBuildingTemplet2.Key);
					if (buildingData2 != null)
					{
						NKMWorldMapBuildingTemplet.LevelTemplet levelTemplet2 = nkmworldMapBuildingTemplet2.GetLevelTemplet(buildingData2.level);
						if (levelTemplet2 == null)
						{
							Log.Error(string.Format("city level templet not found. cityId:{0} build id : {1}, level:{2}", data.cityID, nkmworldMapBuildingTemplet2.Key, buildingData2.level), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMWorldMap.cs", 78);
							return 0f;
						}
						value += (float)levelTemplet2.cityStatValue;
					}
				}
				return value;
			}
			default:
				throw new Exception("Invalid Stat.");
			}
			float result;
			return result;
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x000BF0A0 File Offset: 0x000BD2A0
		public static int GetBuildStatRewardRate(this NKMWorldMapCityData data, NKM_CITY_BUILDING_STAT buildingStat)
		{
			if (buildingStat - NKM_CITY_BUILDING_STAT.CBS_MISSION_UNIT_EXP_RATE > 3)
			{
				throw new Exception("Invalid Stat.");
			}
			int num = 0;
			List<NKMWorldMapBuildingTemplet> list;
			if (!NKMWorldMapManager.m_dicBuildTempletByStatEnum.TryGetValue(buildingStat, out list))
			{
				return 0;
			}
			foreach (NKMWorldMapBuildingTemplet nkmworldMapBuildingTemplet in list)
			{
				NKMWorldmapCityBuildingData buildingData = data.GetBuildingData(nkmworldMapBuildingTemplet.Key);
				if (buildingData != null)
				{
					NKMWorldMapBuildingTemplet.LevelTemplet levelTemplet = nkmworldMapBuildingTemplet.GetLevelTemplet(buildingData.level);
					if (levelTemplet == null)
					{
						Log.Error(string.Format("city level templet not found. cityId:{0} build id : {1}, level:{2}", data.cityID, nkmworldMapBuildingTemplet.Key, buildingData.level), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMWorldMap.cs", 117);
						return 0;
					}
					num += levelTemplet.cityStatValue;
				}
			}
			return num;
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x000BF180 File Offset: 0x000BD380
		public static int CalculateBuildPointLeft(this NKMWorldMapCityData data)
		{
			int num = 0;
			foreach (KeyValuePair<int, NKMWorldmapCityBuildingData> keyValuePair in data.worldMapCityBuildingDataMap)
			{
				num += keyValuePair.Value.level;
			}
			return data.level - num;
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x000BF1E8 File Offset: 0x000BD3E8
		public static NKM_ERROR_CODE CanCancelMission(this NKMWorldMapCityData data, int missionID)
		{
			return data.worldMapMission.CanCancelMission(missionID);
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x000BF1F8 File Offset: 0x000BD3F8
		public static NKM_ERROR_CODE CanLevelupCity(this NKMWorldMapCityData data, NKMWorldMapCityTemplet cityTemplet, NKMUserData userData)
		{
			if (cityTemplet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_CITY_ID;
			}
			if (data.worldMapMission != null && data.worldMapMission.currentMissionID != 0)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_CITY_LEVELUP_FAIL_MISSION;
			}
			int num;
			if (!NKMWorldMapManager.CanLevelup(data.level, data.exp, out num))
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_CITY_LEVELUP_FAIL;
			}
			if (userData.GetCredit() < (long)num)
			{
				return NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_CREDIT;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x000BF254 File Offset: 0x000BD454
		public static NKM_ERROR_CODE CanStartMission(this NKMWorldMapCityData data, NKMUserData userData, int missionID, NKMDeckIndex deckIndex)
		{
			if (data.leaderUnitUID == 0L)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_MISSION_DO_NOT_SET_LEADER;
			}
			NKMUnitData unitFromUID = userData.m_ArmyData.GetUnitFromUID(data.leaderUnitUID);
			if (unitFromUID == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_MISSION_DO_NOT_SET_LEADER;
			}
			return data.worldMapMission.CanStartMission(userData, missionID, deckIndex, unitFromUID);
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x000BF29C File Offset: 0x000BD49C
		public static NKMWorldmapCityBuildingData GetBuildingData(this NKMWorldMapCityData data, int buildID)
		{
			NKMWorldmapCityBuildingData result;
			data.worldMapCityBuildingDataMap.TryGetValue(buildID, out result);
			return result;
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x000BF2B9 File Offset: 0x000BD4B9
		public static bool HasMission(this NKMWorldMapCityData data)
		{
			return data.worldMapMission != null && data.worldMapMission.currentMissionID != 0;
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x000BF2D3 File Offset: 0x000BD4D3
		public static bool IsMissionFinished(this NKMWorldMapCityData data, DateTime CurrentTime)
		{
			return data.worldMapMission.currentMissionID != 0 && data.worldMapMission.completeTime < CurrentTime.Ticks;
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x000BF2F8 File Offset: 0x000BD4F8
		public static void Levelup(this NKMWorldMapCityData data)
		{
			data.exp = 0;
			data.level++;
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x000BF30F File Offset: 0x000BD50F
		public static void RemoveBuild(this NKMWorldMapCityData cityData, int buildID)
		{
			cityData.worldMapCityBuildingDataMap.Remove(buildID);
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x000BF31E File Offset: 0x000BD51E
		public static NKM_ERROR_CODE UpdateBuildingData(this NKMWorldMapCityData data, NKMWorldmapCityBuildingData newData)
		{
			if (newData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_BUILD_ID;
			}
			if (!data.worldMapCityBuildingDataMap.ContainsKey(newData.id))
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_BUILD_ID;
			}
			data.worldMapCityBuildingDataMap[newData.id] = newData;
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x0400265B RID: 9819
		public const int BUILD_MAX_COUNT = 10;
	}
}
