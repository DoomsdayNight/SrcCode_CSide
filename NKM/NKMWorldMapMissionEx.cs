using System;
using System.Collections.Generic;
using ClientPacket.WorldMap;

namespace NKM
{
	// Token: 0x0200050C RID: 1292
	public static class NKMWorldMapMissionEx
	{
		// Token: 0x06002514 RID: 9492 RVA: 0x000BF7F5 File Offset: 0x000BD9F5
		public static NKM_ERROR_CODE CanCancelMission(this NKMWorldMapMission data, int missionID)
		{
			if (data.currentMissionID == 0)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_MISSION_DOING;
			}
			if (NKMWorldMapManager.GetMissionTemplet(missionID) == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_MISSION_ID;
			}
			if (!data.ValidMissionID(missionID))
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_MISSION_ID;
			}
			if (data.currentMissionID != missionID)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_MISSION_ID;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x000BF834 File Offset: 0x000BDA34
		public static NKM_ERROR_CODE CanStartMission(this NKMWorldMapMission data, NKMUserData userData, int missionID, NKMDeckIndex deckIndex, NKMUnitData leaderUnit)
		{
			if (data.currentMissionID != 0)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_MISSION_DOING;
			}
			NKMWorldMapMissionTemplet missionTemplet = NKMWorldMapManager.GetMissionTemplet(missionID);
			if (missionTemplet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_MISSION_ID;
			}
			if (leaderUnit.m_UnitLevel < missionTemplet.m_ReqManagerLevel)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_MISSION_LEADER_LEVEL_LOW;
			}
			if (!data.stMissionIDList.Contains(missionID))
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_MISSION_ID;
			}
			if (!data.ValidMissionID(missionID))
			{
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_MISSION_ID;
			}
			if (!NKMWorldMapManager.IsMissionLeaderOnly(missionTemplet.m_eMissionType))
			{
				if (deckIndex.m_eDeckType == NKM_DECK_TYPE.NDT_NONE)
				{
					return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_MISSION_INVALID_DECK;
				}
				if (userData.m_ArmyData.GetDeckData(deckIndex) == null)
				{
					return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_MISSION_INVALID_DECK;
				}
				NKM_ERROR_CODE nkm_ERROR_CODE = NKMMain.IsValidDeck(userData.m_ArmyData, deckIndex);
				if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
				{
					return nkm_ERROR_CODE;
				}
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x000BF8DC File Offset: 0x000BDADC
		public static void Reset(this NKMWorldMapMission data, int[] mission_list)
		{
			data.currentMissionID = 0;
			data.completeTime = 0L;
			data.stMissionIDList.Clear();
			foreach (int item in mission_list)
			{
				data.stMissionIDList.Add(item);
			}
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x000BF923 File Offset: 0x000BDB23
		public static void Reset(this NKMWorldMapMission data)
		{
			data.currentMissionID = 0;
			data.completeTime = 0L;
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x000BF934 File Offset: 0x000BDB34
		public static bool ValidMissionID(this NKMWorldMapMission data, int mission_id)
		{
			using (List<int>.Enumerator enumerator = data.stMissionIDList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == mission_id)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
