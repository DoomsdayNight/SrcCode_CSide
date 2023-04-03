using System;
using System.Collections.Generic;
using ClientPacket.WorldMap;
using NKM.Templet;

namespace NKM
{
	// Token: 0x0200050A RID: 1290
	public static class NKMWorldMapDataEx
	{
		// Token: 0x06002508 RID: 9480 RVA: 0x000BF358 File Offset: 0x000BD558
		public static NKM_ERROR_CODE CanOpenCity(this NKMWorldMapData data, NKMWorldMapCityTemplet cityTemplet, NKMUserData userData, bool bCash)
		{
			if (cityTemplet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_CITY_ID;
			}
			if (data.IsCityUnlocked(cityTemplet.m_ID))
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_CITY_ALREADY_OPENED;
			}
			int cityOpenCost;
			long num;
			if (!bCash)
			{
				int unlockedCityCount = data.GetUnlockedCityCount();
				if (NKMWorldMapManager.GetPossibleCityCount(userData.m_UserLevel) <= unlockedCityCount)
				{
					return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_FULL_AREA;
				}
				cityOpenCost = NKMWorldMapManager.GetCityOpenCost(data, false);
				num = userData.GetCredit();
			}
			else
			{
				cityOpenCost = NKMWorldMapManager.GetCityOpenCost(data, true);
				num = userData.GetCash();
			}
			if (num >= (long)cityOpenCost)
			{
				return NKM_ERROR_CODE.NEC_OK;
			}
			if (bCash)
			{
				return NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_CASH;
			}
			return NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_CREDIT;
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x000BF3D4 File Offset: 0x000BD5D4
		public static bool CheckOpenCount(this NKMWorldMapData data, int lev)
		{
			int possibleCityCount = NKMWorldMapManager.GetPossibleCityCount(lev);
			return data.worldMapCityDataMap.Count < possibleCityCount;
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x000BF3FC File Offset: 0x000BD5FC
		public static NKMWorldMapCityData GetCityData(this NKMWorldMapData data, int cityID)
		{
			NKMWorldMapCityData result;
			if (data.worldMapCityDataMap.TryGetValue(cityID, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x000BF41C File Offset: 0x000BD61C
		public static int GetCityID(this NKMWorldMapData data, long diveUid)
		{
			foreach (NKMWorldMapCityData nkmworldMapCityData in data.worldMapCityDataMap.Values)
			{
				NKMWorldMapEventTemplet nkmworldMapEventTemplet = NKMWorldMapEventTemplet.Find(nkmworldMapCityData.worldMapEventGroup.worldmapEventID);
				if (nkmworldMapEventTemplet != null && nkmworldMapEventTemplet.eventType == NKM_WORLDMAP_EVENT_TYPE.WET_DIVE && nkmworldMapCityData.worldMapEventGroup.eventUid == diveUid)
				{
					return nkmworldMapCityData.cityID;
				}
			}
			return 0;
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x000BF4A4 File Offset: 0x000BD6A4
		public static int GetUnlockedCityCount(this NKMWorldMapData data)
		{
			return data.worldMapCityDataMap.Count;
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x000BF4B1 File Offset: 0x000BD6B1
		public static bool IsCityUnlocked(this NKMWorldMapData data, int CityID)
		{
			return data.worldMapCityDataMap.ContainsKey(CityID);
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x000BF4C0 File Offset: 0x000BD6C0
		public static int GetCityIDByEventData(this NKMWorldMapData data, NKM_WORLDMAP_EVENT_TYPE eventType, long eventUID)
		{
			foreach (KeyValuePair<int, NKMWorldMapCityData> keyValuePair in data.worldMapCityDataMap)
			{
				if (keyValuePair.Value.worldMapEventGroup != null && keyValuePair.Value.worldMapEventGroup.eventUid == eventUID)
				{
					NKMWorldMapEventTemplet nkmworldMapEventTemplet = NKMWorldMapEventTemplet.Find(keyValuePair.Value.worldMapEventGroup.worldmapEventID);
					if (nkmworldMapEventTemplet != null && nkmworldMapEventTemplet.eventType == eventType)
					{
						return keyValuePair.Key;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x000BF560 File Offset: 0x000BD760
		public static int GetStartedEventCount(this NKMWorldMapData data, NKM_WORLDMAP_EVENT_TYPE eventType)
		{
			int num = 0;
			foreach (KeyValuePair<int, NKMWorldMapCityData> keyValuePair in data.worldMapCityDataMap)
			{
				if (keyValuePair.Value.worldMapEventGroup != null)
				{
					NKMWorldMapEventTemplet nkmworldMapEventTemplet = NKMWorldMapEventTemplet.Find(keyValuePair.Value.worldMapEventGroup.worldmapEventID);
					if (nkmworldMapEventTemplet != null && nkmworldMapEventTemplet.eventType == eventType && keyValuePair.Value.worldMapEventGroup.eventUid > 0L)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x000BF5FC File Offset: 0x000BD7FC
		public static int GetStartedEventCityID(this NKMWorldMapData data, NKM_WORLDMAP_EVENT_TYPE eventType, int targetIndex)
		{
			int num = 0;
			foreach (KeyValuePair<int, NKMWorldMapCityData> keyValuePair in data.worldMapCityDataMap)
			{
				if (keyValuePair.Value.worldMapEventGroup != null)
				{
					NKMWorldMapEventTemplet nkmworldMapEventTemplet = NKMWorldMapEventTemplet.Find(keyValuePair.Value.worldMapEventGroup.worldmapEventID);
					if (nkmworldMapEventTemplet != null && nkmworldMapEventTemplet.eventType == eventType && keyValuePair.Value.worldMapEventGroup.eventUid > 0L)
					{
						if (num == targetIndex)
						{
							return keyValuePair.Key;
						}
						num++;
					}
				}
			}
			return -1;
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x000BF6A8 File Offset: 0x000BD8A8
		public static bool CheckIfHaveSpecificEvent(this NKMWorldMapData data, NKM_WORLDMAP_EVENT_TYPE eventType)
		{
			if (data == null)
			{
				return false;
			}
			foreach (KeyValuePair<int, NKMWorldMapCityData> keyValuePair in data.worldMapCityDataMap)
			{
				NKMWorldMapCityData value = keyValuePair.Value;
				if (value != null && value.worldMapEventGroup != null)
				{
					NKMWorldMapEventTemplet nkmworldMapEventTemplet = NKMWorldMapEventTemplet.Find(value.worldMapEventGroup.worldmapEventID);
					if (nkmworldMapEventTemplet != null && nkmworldMapEventTemplet.eventType == eventType)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x000BF734 File Offset: 0x000BD934
		public static bool RemoveEvent(this NKMWorldMapData data, NKM_WORLDMAP_EVENT_TYPE eventType, long eventUID, out int cityID)
		{
			cityID = -1;
			if (data == null)
			{
				return false;
			}
			foreach (KeyValuePair<int, NKMWorldMapCityData> keyValuePair in data.worldMapCityDataMap)
			{
				NKMWorldMapCityData value = keyValuePair.Value;
				if (value != null && value.worldMapEventGroup != null)
				{
					NKMWorldMapEventTemplet nkmworldMapEventTemplet = NKMWorldMapEventTemplet.Find(value.worldMapEventGroup.worldmapEventID);
					if (nkmworldMapEventTemplet != null && nkmworldMapEventTemplet.eventType == eventType && value.worldMapEventGroup.eventUid == eventUID)
					{
						cityID = value.cityID;
						value.worldMapEventGroup.Clear();
						return true;
					}
				}
			}
			return false;
		}
	}
}
