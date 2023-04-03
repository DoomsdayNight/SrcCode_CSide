using System;
using System.Collections.Generic;
using ClientPacket.WorldMap;
using Cs.Logging;
using NKM.Templet;

namespace NKM
{
	// Token: 0x0200036F RID: 879
	public class NKMDiveGameManager
	{
		// Token: 0x060015D6 RID: 5590 RVA: 0x00058C00 File Offset: 0x00056E00
		public static int GetCost(NKMDiveTemplet templet, NKMWorldMapCityData cityData)
		{
			int num = templet.StageReqItemCount;
			if (cityData != null)
			{
				float num2 = cityData.CalcBuildStat(NKM_CITY_BUILDING_STAT.CBS_DIVE_INFORMATION_REDUCE_RATE, (float)num);
				int num3 = Math.Min(num, (int)Math.Ceiling((double)num2));
				num -= num3;
			}
			return num;
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x00058C38 File Offset: 0x00056E38
		public static NKM_ERROR_CODE CanStart(int cityID, int stageID, List<int> deckIndexes, NKMUserData userData, DateTime curTimeUTC)
		{
			NKMDiveTemplet nkmdiveTemplet = NKMDiveTemplet.Find(stageID);
			if (nkmdiveTemplet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_DIVE_INVALID_STAGE_ID;
			}
			if (!nkmdiveTemplet.IsEventDive && userData.CheckDiveClear(stageID))
			{
				return NKM_ERROR_CODE.NEC_FAIL_DIVE_ALREADY_CLEARED;
			}
			if (userData.m_DiveGameData != null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_DIVE_ALREADY_STARTED;
			}
			if (deckIndexes.Count <= 0)
			{
				return NKM_ERROR_CODE.NEC_FAIL_DIVE_NOT_ENOUGH_SQUAD_COUNT;
			}
			UnlockInfo unlockInfo = new UnlockInfo(nkmdiveTemplet.StageUnlockReqType, nkmdiveTemplet.StageUnlockReqValue);
			if (!NKMContentUnlockManager.IsContentUnlocked(userData, unlockInfo, false))
			{
				return NKM_ERROR_CODE.NEC_FAIL_DIVE_LOCKED_STAGE;
			}
			for (int i = 0; i < deckIndexes.Count; i++)
			{
				NKM_ERROR_CODE nkm_ERROR_CODE = NKMMain.IsValidDeck(userData.m_ArmyData, NKM_DECK_TYPE.NDT_DIVE, (byte)deckIndexes[i]);
				if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
				{
					return nkm_ERROR_CODE;
				}
			}
			NKMWorldMapCityData cityData = userData.m_WorldmapData.GetCityData(cityID);
			if (cityData != null)
			{
				if (cityData.worldMapEventGroup.worldmapEventID == 0)
				{
					return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_EVENT_GROUP_ID;
				}
				if (cityData.worldMapEventGroup.eventGroupEndDate < curTimeUTC)
				{
					return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_EXPIRE_EVENT;
				}
				NKMWorldMapEventTemplet nkmworldMapEventTemplet = NKMWorldMapEventTemplet.Find(cityData.worldMapEventGroup.worldmapEventID);
				if (nkmworldMapEventTemplet == null)
				{
					Log.Error(string.Format("Invalid Templet City ID. CityID : {0}", cityData.cityID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Dive/NKMDiveGameManager.cs", 85);
					return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_CITY_ID;
				}
				if (nkmworldMapEventTemplet.eventType != NKM_WORLDMAP_EVENT_TYPE.WET_DIVE || nkmworldMapEventTemplet.stageID != stageID)
				{
					return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_INVALID_EVENT_GROUP_ID;
				}
			}
			int cost = NKMDiveGameManager.GetCost(nkmdiveTemplet, cityData);
			if (!userData.CheckPrice(cost, nkmdiveTemplet.StageReqItemId))
			{
				return NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_ITEM;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x00058D94 File Offset: 0x00056F94
		public static NKM_ERROR_CODE CanMoveForward(int nextSlotIndex, NKMUserData userData)
		{
			if (userData.m_DiveGameData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_DIVE_HAS_NOT_STARTED_YET;
			}
			NKMDiveFloor floor = userData.m_DiveGameData.Floor;
			NKMDivePlayer player = userData.m_DiveGameData.Player;
			int nextSlotSetIndex = player.GetNextSlotSetIndex();
			if (player.PlayerBase.State != NKMDivePlayerState.Exploring || !player.CanMove(floor, nextSlotSetIndex, nextSlotIndex))
			{
				return NKM_ERROR_CODE.NEC_FAIL_DIVE_CANNOT_MOVE_FORWARD;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x00058DED File Offset: 0x00056FED
		public static NKM_ERROR_CODE CanGiveUp(NKMUserData userData)
		{
			if (userData.m_DiveGameData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_DIVE_HAS_NOT_STARTED_YET;
			}
			if (userData.m_DiveGameData.Player.IsInBattle())
			{
				return NKM_ERROR_CODE.NEC_FAIL_DIVE_CANNOT_GIVE_UP;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x00058E18 File Offset: 0x00057018
		public static void UpdateAllDiveDeckState(NKM_DECK_STATE deckState, NKMUserData userData)
		{
			foreach (NKMDiveSquad nkmdiveSquad in userData.m_DiveGameData.Player.Squads.Values)
			{
				NKMDeckData deckData = userData.m_ArmyData.GetDeckData(NKM_DECK_TYPE.NDT_DIVE, nkmdiveSquad.DeckIndex);
				if (deckData == null)
				{
					Log.Error(string.Format("Invalid Deck Index. UserUid:{0}, DeckType:{1}, DeckIndex:{2}", userData.m_UserUID, NKM_DECK_TYPE.NDT_DIVE, nkmdiveSquad.DeckIndex), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/Dive/NKMDiveGameManager.cs", 159);
				}
				else
				{
					if (deckData.GetState() != NKM_DECK_STATE.DECK_STATE_DIVE)
					{
						deckData.SetState(deckState);
					}
					userData.m_ArmyData.DeckUpdated(new NKMDeckIndex(NKM_DECK_TYPE.NDT_DIVE, nkmdiveSquad.DeckIndex), deckData);
				}
			}
		}
	}
}
