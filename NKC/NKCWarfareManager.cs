using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Community;
using ClientPacket.Warfare;
using Cs.Logging;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKC
{
	// Token: 0x020006FA RID: 1786
	public static class NKCWarfareManager
	{
		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x060045E4 RID: 17892 RVA: 0x00153657 File Offset: 0x00151857
		// (set) Token: 0x060045E5 RID: 17893 RVA: 0x0015365E File Offset: 0x0015185E
		public static List<WarfareSupporterListData> SupporterList { get; private set; } = new List<WarfareSupporterListData>();

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x060045E6 RID: 17894 RVA: 0x00153666 File Offset: 0x00151866
		// (set) Token: 0x060045E7 RID: 17895 RVA: 0x0015366D File Offset: 0x0015186D
		public static NKM_WARFARE_SERVICE_TYPE UseServiceType { get; private set; } = NKM_WARFARE_SERVICE_TYPE.NWST_NONE;

		// Token: 0x060045E8 RID: 17896 RVA: 0x00153675 File Offset: 0x00151875
		public static void SetSupportList(List<WarfareSupporterListData> friends, List<WarfareSupporterListData> guests)
		{
			NKCWarfareManager.SupporterList.Clear();
			NKCWarfareManager.SupporterList.AddRange(guests);
			NKCWarfareManager.SupporterList.AddRange(friends);
		}

		// Token: 0x060045E9 RID: 17897 RVA: 0x00153698 File Offset: 0x00151898
		public static WarfareSupporterListData FindSupporter(long friendCode)
		{
			return NKCWarfareManager.SupporterList.Find((WarfareSupporterListData v) => v.commonProfile.friendCode == friendCode);
		}

		// Token: 0x060045EA RID: 17898 RVA: 0x001536C8 File Offset: 0x001518C8
		public static bool IsGeustSupporter(long friendCode)
		{
			return friendCode != 0L && !NKCFriendManager.IsFriend(friendCode);
		}

		// Token: 0x060045EB RID: 17899 RVA: 0x001536D8 File Offset: 0x001518D8
		public static void CheckValidClientOnly()
		{
			foreach (NKMWarfareTemplet nkmwarfareTemplet in NKMTempletContainer<NKMWarfareTemplet>.Values)
			{
				nkmwarfareTemplet.ValidateClientOnly();
			}
			NKCCutScenManager.ClearCacheData();
		}

		// Token: 0x060045EC RID: 17900 RVA: 0x00153728 File Offset: 0x00151928
		public static bool CheckValidSpawnPoint(NKMWarfareMapTemplet cNKMWarfareMapTemplet, NKMWarfareTileTemplet cNKMWarfareTile, NKMUserData cNKMUserData, NKMDeckIndex cNKMDeckIndex, out bool bAssultPoint)
		{
			bAssultPoint = false;
			if (cNKMWarfareMapTemplet == null || cNKMWarfareTile == null || cNKMUserData == null)
			{
				return false;
			}
			if (cNKMWarfareTile.m_NKM_WARFARE_SPAWN_POINT_TYPE == NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_NONE)
			{
				return false;
			}
			if (cNKMWarfareTile.m_NKM_WARFARE_SPAWN_POINT_TYPE == NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_ASSAULT)
			{
				bAssultPoint = true;
				if (!NKCWarfareManager.CheckAssaultShip(cNKMUserData, cNKMDeckIndex))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060045ED RID: 17901 RVA: 0x0015375C File Offset: 0x0015195C
		public static NKM_ERROR_CODE CheckWFGameStartCond(NKMUserData cNKMUserData, NKMPacket_WARFARE_GAME_START_REQ startReq)
		{
			if (cNKMUserData == null)
			{
				Log.Error("StartWarfareGame cNKMUserData is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 83);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_INVALID_USER;
			}
			if (startReq == null)
			{
				Log.Error("StartWarfareGame cNKMWarfareGameStartData is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 89);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_INVALID_START_GAME_DATA;
			}
			if (NKCScenManager.GetScenManager().WarfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_STOP)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_IS_NOT_STOP;
			}
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(startReq.warfareTempletID);
			if (nkmwarfareTemplet == null)
			{
				Log.Error(string.Format("GetWarfareTempletByID null, m_WarfareTempletID: {0}", startReq.warfareTempletID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 104);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_FIND_WARFARE_TEMPLET;
			}
			NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(nkmwarfareTemplet.m_WarfareStrID);
			if (nkmstageTempletV == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_FIND_EPISODE_TEMPLET;
			}
			if (!NKMEpisodeMgr.CheckEpisodeMission(cNKMUserData, nkmstageTempletV))
			{
				return NKM_ERROR_CODE.NEC_FAIL_LOCKED_EPISODE;
			}
			if (startReq.unitPositionList.Count > nkmwarfareTemplet.m_UserTeamCount)
			{
				Log.Error(string.Format("Warfare, MaxUserUnitCount Overflow, Request Count : {0}", startReq.unitPositionList.Count), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 124);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_START_BY_MAX_USER_UNIT_OVERFLOW;
			}
			if (!startReq.unitPositionList.Any((NKMPacket_WARFARE_GAME_START_REQ.UnitPosition pos) => pos.isFlagShip))
			{
				Log.Error("WarfareGame.CheckWFGameStartCond, Can't Find Flag Ship", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 131);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_NOT_EXIST_USER_FLAG_SHIP;
			}
			if ((from pos in startReq.unitPositionList
			group pos by pos.deckIndex).Any((IGrouping<byte, NKMPacket_WARFARE_GAME_START_REQ.UnitPosition> e) => e.Count<NKMPacket_WARFARE_GAME_START_REQ.UnitPosition>() > 1))
			{
				Log.Error("WarfareGame, Duplicate Deck Index Found!", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 138);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_START_BY_DUPLICATE_DECK_INDEX;
			}
			NKMWarfareMapTemplet mapTemplet = nkmwarfareTemplet.MapTemplet;
			bool flag = false;
			int num = 0;
			bool flag2 = false;
			bool flag3 = false;
			for (int i = 0; i < mapTemplet.m_MapSizeX; i++)
			{
				for (int j = 0; j < mapTemplet.m_MapSizeY; j++)
				{
					NKMWarfareTileTemplet tile = mapTemplet.GetTile(i, j);
					if (tile != null)
					{
						if (tile.m_DungeonStrID != null)
						{
							if (!flag && tile.m_bFlagDungeon)
							{
								flag = true;
							}
							if (tile.m_bTargetUnit)
							{
								num++;
							}
						}
						if (!flag2 && tile.m_TileWinType == WARFARE_GAME_CONDITION.WFC_TILE_ENTER)
						{
							flag2 = true;
						}
						if (!flag3 && tile.m_TileWinType == WARFARE_GAME_CONDITION.WFC_PHASE_TILE_HOLD)
						{
							flag3 = true;
						}
					}
				}
			}
			if (nkmwarfareTemplet.m_WFWinCondition == WARFARE_GAME_CONDITION.WFC_KILL_BOSS)
			{
				if (!flag)
				{
					Log.Error("WarfareGame, Flag Dungeon Not Found", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 188);
					return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_NOT_EXIST_FLAG_DUNGEON;
				}
			}
			else if (nkmwarfareTemplet.m_WFWinCondition == WARFARE_GAME_CONDITION.WFC_KILL_TARGET)
			{
				if (nkmwarfareTemplet.m_WFWinValue != num)
				{
					Log.Error(string.Format("WarfareGame, WFWC_Kill - WinValue {0} != TileTarget {1}", nkmwarfareTemplet.m_WFWinValue, num), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 196);
					return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_WIN_CONDITION_KILL_TARGET_COUNT;
				}
			}
			else if (nkmwarfareTemplet.m_WFWinCondition == WARFARE_GAME_CONDITION.WFC_TILE_ENTER)
			{
				if (!flag2)
				{
					Log.Error("WarfareGame, WFWC_ENTER - Enter Tile Not Found", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 204);
					return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_WIN_CONDITION_ENTER_TILE_NOT;
				}
			}
			else if (nkmwarfareTemplet.m_WFWinCondition == WARFARE_GAME_CONDITION.WFC_PHASE_TILE_HOLD && !flag3)
			{
				Log.Error("WarfareGame, WFWC_HOLD - Hold Tile Not Found", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 212);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_WIN_CONDITION_HOLD_TILE_NOT;
			}
			int k = 0;
			while (k < startReq.unitPositionList.Count)
			{
				NKMPacket_WARFARE_GAME_START_REQ.UnitPosition unitPosition = startReq.unitPositionList[k];
				NKM_ERROR_CODE nkm_ERROR_CODE = NKMMain.IsValidDeck(cNKMUserData.m_ArmyData, new NKMDeckIndex(NKM_DECK_TYPE.NDT_NORMAL, (int)unitPosition.deckIndex));
				if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
				{
					return nkm_ERROR_CODE;
				}
				NKMWarfareTileTemplet tile2 = mapTemplet.GetTile((int)unitPosition.tileIndex);
				if (tile2 == null)
				{
					Log.Error(string.Format("cNKMWarfareTile is Null tileIndex : {0}", unitPosition.tileIndex), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 233);
					return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_FIND_WARFARE_TILE;
				}
				if (tile2.m_TileType == NKM_WARFARE_MAP_TILE_TYPE.NWMTT_DISABLE)
				{
					Log.Error(string.Format("cNKMWarfareTile is disable, tileIndex : {0}", unitPosition.tileIndex), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 239);
					return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_SET_UNIT_ON_DISABLE_TILE;
				}
				if (tile2.m_DungeonStrID != null)
				{
					Log.Error(string.Format("NEC_FAIL_WARFARE_GAME_CANNOT_POSITION_BY_DUNGEON, tileIndex : {0}", unitPosition.tileIndex), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 245);
					return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_POSITION_BY_DUNGEON;
				}
				bool flag4 = false;
				if (!NKCWarfareManager.CheckValidSpawnPoint(mapTemplet, tile2, cNKMUserData, new NKMDeckIndex(NKM_DECK_TYPE.NDT_NORMAL, (int)unitPosition.deckIndex), out flag4))
				{
					if (flag4)
					{
						Log.Error(string.Format("NEC_FAIL_WARFARE_GAME_CANNOT_ASSAULT_POSITION, tileIndex : {0}", unitPosition.tileIndex), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 265);
						return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_ASSAULT_POSITION;
					}
					Log.Error(string.Format("NEC_FAIL_WARFARE_GAME_CANNOT_POSITION, tileIndex : {0}", unitPosition.tileIndex), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 270);
					return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_POSITION;
				}
				else
				{
					k++;
				}
			}
			if (startReq.friendCode != 0L)
			{
				if (!nkmwarfareTemplet.m_bFriendSummon)
				{
					Log.Error("you can't play with supporter : " + nkmwarfareTemplet.m_WarfareStrID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 280);
					return NKM_ERROR_CODE.NEC_FAIL_WARFARE_FRIEND_NOT_SUPPOTABLE_MAP;
				}
				if (NKCWarfareManager.FindSupporter(startReq.friendCode) == null)
				{
					Log.Error(string.Format("fail - friend code : {0}", startReq.friendCode), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 288);
					return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_FIND_UNIT;
				}
				NKMWarfareTileTemplet tile3 = mapTemplet.GetTile((int)startReq.friendTileIndex);
				if (tile3 == null)
				{
					Log.Error(string.Format("cNKMWarfareTile is Null friend tileIndex : {0}", startReq.friendTileIndex), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 295);
					return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_FIND_WARFARE_TILE;
				}
				if (tile3.m_NKM_WARFARE_SPAWN_POINT_TYPE == NKM_WARFARE_SPAWN_POINT_TYPE.NWSPT_ASSAULT)
				{
					Log.Error("게스트/친구 소대는 강습지점에 착륙 불가", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 302);
					return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_POSITION;
				}
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x060045EE RID: 17902 RVA: 0x00153C88 File Offset: 0x00151E88
		public static void UseService(NKMUserData userData, WarfareGameData cNKMWarfareGameData, NKMPacket_WARFARE_GAME_USE_SERVICE_ACK sPacket)
		{
			if (userData == null)
			{
				return;
			}
			if (cNKMWarfareGameData == null)
			{
				return;
			}
			WarfareUnitData unitData = cNKMWarfareGameData.GetUnitData(sPacket.warfareGameUnitUID);
			if (unitData != null)
			{
				NKCWarfareManager.UseServiceType = sPacket.warfareServiceType;
				if (NKCWarfareManager.UseServiceType == NKM_WARFARE_SERVICE_TYPE.NWST_REPAIR)
				{
					unitData.hp = sPacket.hp;
					return;
				}
				if (NKCWarfareManager.UseServiceType == NKM_WARFARE_SERVICE_TYPE.NWST_RESUPPLY)
				{
					unitData.supply = (int)sPacket.supply;
				}
			}
		}

		// Token: 0x060045EF RID: 17903 RVA: 0x00153CE1 File Offset: 0x00151EE1
		public static void ResetServiceType()
		{
			NKCWarfareManager.UseServiceType = NKM_WARFARE_SERVICE_TYPE.NWST_NONE;
		}

		// Token: 0x060045F0 RID: 17904 RVA: 0x00153CEC File Offset: 0x00151EEC
		public static NKM_ERROR_CODE CanTryServiceUse(NKMUserData userData, WarfareGameData cNKMWarfareGameData, int warfareGameUnitUID, NKM_WARFARE_SERVICE_TYPE serviceType)
		{
			if (userData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_USER_DATA_NULL;
			}
			if (cNKMWarfareGameData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_DATA_NULL;
			}
			if (cNKMWarfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_PLAYING)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_IS_NOT_PLAYING_STATE;
			}
			if (!cNKMWarfareGameData.isTurnA)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CAN_ONLY_USE_THIS_ITEM_ON_TURN_A;
			}
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(cNKMWarfareGameData.warfareTempletID);
			if (nkmwarfareTemplet == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_INVALID_WARFARE_TEMPLET_ID;
			}
			NKMWarfareMapTemplet mapTemplet = nkmwarfareTemplet.MapTemplet;
			WarfareUnitData unitData = cNKMWarfareGameData.GetUnitData(warfareGameUnitUID);
			if (unitData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_FIND_UNIT;
			}
			int tileIndex = (int)unitData.tileIndex;
			WarfareTileData tileData = cNKMWarfareGameData.GetTileData(tileIndex);
			if (tileData == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_FIND_WARFARE_TILE;
			}
			if (serviceType == NKM_WARFARE_SERVICE_TYPE.NWST_REPAIR)
			{
				if (tileData.tileType != NKM_WARFARE_MAP_TILE_TYPE.NWMTT_REPAIR && tileData.tileType != NKM_WARFARE_MAP_TILE_TYPE.NWNTT_SERVICE)
				{
					return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_NOT_SERVICEABLE_TILE;
				}
			}
			else if (serviceType == NKM_WARFARE_SERVICE_TYPE.NWST_RESUPPLY && tileData.tileType != NKM_WARFARE_MAP_TILE_TYPE.NWMTT_RESUPPLY && tileData.tileType != NKM_WARFARE_MAP_TILE_TYPE.NWNTT_SERVICE)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_NOT_SERVICEABLE_TILE;
			}
			if (serviceType == NKM_WARFARE_SERVICE_TYPE.NWST_REPAIR)
			{
				if (unitData.hp >= unitData.hpMax)
				{
					return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_UNIT_HP_IS_ALREADY_FULL;
				}
			}
			else if (serviceType == NKM_WARFARE_SERVICE_TYPE.NWST_RESUPPLY && unitData.supply >= 2)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_UNIT_SUPPLY_IS_ALREADY_FULL;
			}
			if (NKCWarfareManager.IsAuto(userData, cNKMWarfareGameData.warfareTempletID))
			{
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_USE_THIS_ITEM_ON_AUTO;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x060045F1 RID: 17905 RVA: 0x00153DE8 File Offset: 0x00151FE8
		public static NKM_ERROR_CODE CheckMoveCond(NKMUserData cNKMUserData, int tileIndexFrom, int tileIndexTo)
		{
			if (cNKMUserData == null)
			{
				Log.Error("CheckMoveCond cNKMUserData is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 434);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_INVALID_USER;
			}
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData == null)
			{
				Log.Error("CheckMoveCond cNKMWarfareGameData is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 441);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_INVALID_WARFARE_GAME_DATA;
			}
			if (warfareGameData.warfareGameState != NKM_WARFARE_GAME_STATE.NWGS_PLAYING)
			{
				Log.Error("TurnFinish, game state is not playing", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 447);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_IS_NOT_PLAYING_STATE;
			}
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(warfareGameData.warfareTempletID);
			if (nkmwarfareTemplet == null)
			{
				Log.Error("CheckMoveCond cNKMWarfareTemplet is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 454);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_INVALID_WARFARE_TEMPLET_ID;
			}
			if (NKCWarfareManager.IsAuto(cNKMUserData, warfareGameData.warfareTempletID))
			{
				Log.Error("CheckMoveCond cNKMWarfareGameData is auto status", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 460);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_USER_CONTROL_MOVE_ON_AUTO;
			}
			if (tileIndexFrom < 0 || tileIndexFrom >= warfareGameData.warfareTileDataList.Count)
			{
				Log.Error("CheckMoveCond tileIndexFrom is invalid, index : " + tileIndexFrom.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 468);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_MOVE_INVALID_TILE_INDEX;
			}
			if (tileIndexTo < 0 || tileIndexTo >= warfareGameData.warfareTileDataList.Count)
			{
				Log.Error("CheckMoveCond tileIndexTo is invalid, index : " + tileIndexTo.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 474);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_MOVE_INVALID_TILE_INDEX;
			}
			WarfareUnitData unitDataByTileIndex = warfareGameData.GetUnitDataByTileIndex(tileIndexFrom);
			if (unitDataByTileIndex == null)
			{
				Log.Error("CheckMoveCond WarfareGameUnitUID at tileIndexFrom is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 490);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_MOVE_UNIT_NOT_EXIST_ON_FROMTILE;
			}
			if (unitDataByTileIndex.isTurnEnd)
			{
				Log.Error("CheckMoveCond Unit's turn already end, WarfareGameUnitUID at tileIndexFrom : " + unitDataByTileIndex.warfareGameUnitUID.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 497);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_MOVE_UNIT_TURN_ALREADY_END;
			}
			NKMWarfareMapTemplet mapTemplet = nkmwarfareTemplet.MapTemplet;
			WarfareTileData tileData = warfareGameData.GetTileData(tileIndexFrom);
			if (tileData == null)
			{
				Log.Error("CheckMoveCond tileIndexFrom is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 510);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_MOVE_INVALID_TILE_INDEX;
			}
			if (tileData.tileType == NKM_WARFARE_MAP_TILE_TYPE.NWMTT_DISABLE)
			{
				Log.Error("CheckMoveCond tileIndexFrom is disable", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 516);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_MOVE_DISABLE_TILE_INDEX;
			}
			WarfareTileData warfareTileData = warfareGameData.warfareTileDataList[tileIndexTo];
			if (warfareTileData == null)
			{
				Log.Error("CheckMoveCond cNKMWarfareGameTileDataTo is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 523);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_MOVE_INVALID_TILE_INDEX;
			}
			if (warfareTileData.tileType == NKM_WARFARE_MAP_TILE_TYPE.NWMTT_DISABLE)
			{
				Log.Error("CheckMoveCond tileIndexTo is disable", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 539);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_MOVE_DISABLE_TILE_INDEX;
			}
			WarfareTeamData warfareTeamData;
			if (warfareGameData.isTurnA)
			{
				warfareTeamData = warfareGameData.warfareTeamDataA;
			}
			else
			{
				warfareTeamData = warfareGameData.warfareTeamDataB;
			}
			if (!warfareTeamData.warfareUnitDataByUIDMap.ContainsKey(unitDataByTileIndex.warfareGameUnitUID))
			{
				Log.Error("CheckMoveCond Can't find Unit in TeamAtThisTurn, WarfareGameUnitUID at tileIndexFrom : " + unitDataByTileIndex.warfareGameUnitUID.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 556);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_MOVE_UNIT_NOT_EXIST_ON_FROMTILE;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x060045F2 RID: 17906 RVA: 0x00154070 File Offset: 0x00152270
		public static NKM_ERROR_CODE CheckPossibleAuto(NKMUserData cNKMUserData, bool bSet, bool bAutoSupply)
		{
			if (cNKMUserData == null)
			{
				Log.Error("CheckPossibleAuto cNKMUserData is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 569);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_INVALID_USER;
			}
			if (NKCScenManager.GetScenManager().WarfareGameData == null)
			{
				Log.Error("CheckPossibleAuto cNKMWarfareGameData is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 577);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_INVALID_WARFARE_GAME_DATA;
			}
			if (cNKMUserData.m_UserOption.m_bAutoWarfare == bSet && cNKMUserData.m_UserOption.m_bAutoWarfareRepair == bAutoSupply)
			{
				Log.Error(string.Format("WarfareGame's auto is already set as {0}, {1}", bSet, bAutoSupply), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 592);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_AUTO_IS_ALREADY_SET;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x060045F3 RID: 17907 RVA: 0x00154108 File Offset: 0x00152308
		public static NKM_ERROR_CODE CheckGetNextOrderCond(NKMUserData cNKMUserData)
		{
			if (cNKMUserData == null)
			{
				Log.Error("CheckGetNextOrderCond cNKMUserData is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 606);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_INVALID_USER;
			}
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			if (warfareGameData == null)
			{
				Log.Error("CheckGetNextOrderCond cNKMWarfareGameData is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 613);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_INVALID_WARFARE_GAME_DATA;
			}
			if (warfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_STOP)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_STATE_NWGS_STOP;
			}
			if (warfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_INGAME_PLAYING)
			{
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_STATE_NWGS_INGAME_PLAYING;
			}
			if (NKMWarfareTemplet.Find(warfareGameData.warfareTempletID) == null)
			{
				Log.Error("CheckGetNextOrderCond cNKMWarfareTemplet is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 626);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_INVALID_WARFARE_TEMPLET_ID;
			}
			if (warfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_PLAYING && warfareGameData.isTurnA && !cNKMUserData.m_UserOption.m_bAutoWarfare)
			{
				Log.Error(string.Format("CheckGetNextOrderCond, CANNOT_GET_NEXT_ORDER_AT_TURN_A - STATE : {0}, TurnA : {1}, Auto : {2}", warfareGameData.warfareGameState, warfareGameData.isTurnA, cNKMUserData.m_UserOption.m_bAutoWarfare), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCWarfareManager.cs", 633);
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_GET_NEXT_ORDER_AT_TURN_A;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x060045F4 RID: 17908 RVA: 0x00154204 File Offset: 0x00152404
		public static NKM_UNIT_STYLE_TYPE GetShipStyleTypeByGUUID(NKMUserData cNKMUserData, WarfareGameData cNKMWarfareGameData, int guuid)
		{
			NKM_UNIT_STYLE_TYPE result = NKM_UNIT_STYLE_TYPE.NUST_INVALID;
			if (cNKMUserData == null)
			{
				return result;
			}
			WarfareUnitData unitData = cNKMWarfareGameData.GetUnitData(guuid);
			if (unitData == null)
			{
				return result;
			}
			NKMDeckData deckData = cNKMUserData.m_ArmyData.GetDeckData(unitData.deckIndex);
			if (deckData == null)
			{
				return result;
			}
			NKMUnitData shipFromUID = cNKMUserData.m_ArmyData.GetShipFromUID(deckData.m_ShipUID);
			if (shipFromUID == null)
			{
				return result;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(shipFromUID.m_UnitID);
			if (unitTempletBase == null)
			{
				return result;
			}
			return unitTempletBase.m_NKM_UNIT_STYLE_TYPE;
		}

		// Token: 0x060045F5 RID: 17909 RVA: 0x00154270 File Offset: 0x00152470
		public static bool CheckOnTileType(NKMWarfareMapTemplet cNKMWarfareMapTemplet, int tileIndex, NKM_WARFARE_MAP_TILE_TYPE eNKM_WARFARE_MAP_TILE_TYPE)
		{
			if (cNKMWarfareMapTemplet == null)
			{
				return false;
			}
			NKMWarfareTileTemplet tile = cNKMWarfareMapTemplet.GetTile(tileIndex);
			return tile != null && tile.m_TileType == eNKM_WARFARE_MAP_TILE_TYPE;
		}

		// Token: 0x060045F6 RID: 17910 RVA: 0x0015429C File Offset: 0x0015249C
		public static bool CheckOnTileType(WarfareGameData warfareGameData, int tileIndex, NKM_WARFARE_MAP_TILE_TYPE eNKM_WARFARE_MAP_TILE_TYPE)
		{
			if (warfareGameData == null)
			{
				return false;
			}
			WarfareTileData tileData = warfareGameData.GetTileData(tileIndex);
			return tileData != null && tileData.tileType == eNKM_WARFARE_MAP_TILE_TYPE;
		}

		// Token: 0x060045F7 RID: 17911 RVA: 0x001542C8 File Offset: 0x001524C8
		public static bool CheckAssaultShip(NKMUserData cNKMUserData, NKMDeckIndex sNKMDeckIndex)
		{
			NKMDeckData deckData = cNKMUserData.m_ArmyData.GetDeckData(sNKMDeckIndex);
			if (deckData == null)
			{
				return false;
			}
			NKMUnitData shipFromUID = cNKMUserData.m_ArmyData.GetShipFromUID(deckData.m_ShipUID);
			if (shipFromUID == null)
			{
				return false;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(shipFromUID.m_UnitID);
			return unitTempletBase != null && unitTempletBase.m_NKM_UNIT_STYLE_TYPE == NKM_UNIT_STYLE_TYPE.NUST_SHIP_ASSAULT;
		}

		// Token: 0x060045F8 RID: 17912 RVA: 0x0015431C File Offset: 0x0015251C
		public static int GetCurrentMissionValue(WarfareGameData warfareGameData, WARFARE_GAME_MISSION_TYPE missionType)
		{
			if (warfareGameData == null)
			{
				return 0;
			}
			int num = 0;
			int num2 = 0;
			foreach (KeyValuePair<int, WarfareUnitData> keyValuePair in warfareGameData.warfareTeamDataB.warfareUnitDataByUIDMap)
			{
				if (keyValuePair.Value.hp <= 0f)
				{
					num2++;
				}
			}
			switch (missionType)
			{
			case WARFARE_GAME_MISSION_TYPE.WFMT_CLEAR:
				if (warfareGameData.isWinTeamA)
				{
					return num + 1;
				}
				return num;
			case WARFARE_GAME_MISSION_TYPE.WFMT_ALLKILL:
				if (num2 == warfareGameData.warfareTeamDataB.warfareUnitDataByUIDMap.Count)
				{
					return num + 1;
				}
				return num;
			case WARFARE_GAME_MISSION_TYPE.WFMT_PHASE:
				return warfareGameData.turnCount;
			case WARFARE_GAME_MISSION_TYPE.WFMT_NO_SHIPWRECK:
			{
				int num3 = 0;
				foreach (KeyValuePair<int, WarfareUnitData> keyValuePair2 in warfareGameData.warfareTeamDataA.warfareUnitDataByUIDMap)
				{
					if (keyValuePair2.Value.hp > 0f)
					{
						num3++;
					}
				}
				if (num3 == warfareGameData.warfareTeamDataA.warfareUnitDataByUIDMap.Count)
				{
					return num + 1;
				}
				return num;
			}
			case WARFARE_GAME_MISSION_TYPE.WFMT_KILL:
				using (Dictionary<int, WarfareUnitData>.Enumerator enumerator = warfareGameData.warfareTeamDataB.warfareUnitDataByUIDMap.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<int, WarfareUnitData> keyValuePair3 = enumerator.Current;
						if (keyValuePair3.Value.hp <= 0f)
						{
							num++;
						}
					}
					return num;
				}
				break;
			case WARFARE_GAME_MISSION_TYPE.WFMT_FIRST_ATTACK:
				break;
			case WARFARE_GAME_MISSION_TYPE.WFMT_ASSIST:
			case WARFARE_GAME_MISSION_TYPE.WFMT_CONTAINER:
				return num;
			case WARFARE_GAME_MISSION_TYPE.WFMT_NOSUPPLY_WIN:
				if (warfareGameData.supplyUseCount == 0)
				{
					return num + 1;
				}
				return num;
			case WARFARE_GAME_MISSION_TYPE.WFMT_NOSUPPLY_ALLKILL:
				if (warfareGameData.supplyUseCount == 0 && num2 == warfareGameData.warfareTeamDataB.warfareUnitDataByUIDMap.Count)
				{
					return num + 1;
				}
				return num;
			default:
				return num;
			}
			num = warfareGameData.firstAttackCount;
			return num;
		}

		// Token: 0x060045F9 RID: 17913 RVA: 0x00154510 File Offset: 0x00152710
		public static int GetWarfareID(string warfareStrID)
		{
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(warfareStrID);
			if (nkmwarfareTemplet != null)
			{
				return nkmwarfareTemplet.m_WarfareID;
			}
			return 0;
		}

		// Token: 0x060045FA RID: 17914 RVA: 0x00154530 File Offset: 0x00152730
		public static string GetWarfareStrID(int warfareID)
		{
			NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(warfareID);
			if (nkmwarfareTemplet != null)
			{
				return nkmwarfareTemplet.m_WarfareStrID;
			}
			return "";
		}

		// Token: 0x060045FB RID: 17915 RVA: 0x00154553 File Offset: 0x00152753
		public static bool IsAuto(NKMUserData userData, int warfareTempletID)
		{
			return userData.CheckWarfareClear(warfareTempletID) && userData.m_UserOption.m_bAutoWarfare;
		}

		// Token: 0x060045FC RID: 17916 RVA: 0x00154570 File Offset: 0x00152770
		public static List<WarfareTileData> GetNeighborTiles(NKMWarfareMapTemplet mapTemplet, int tileIndex, bool includeSelf)
		{
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			List<WarfareTileData> list = new List<WarfareTileData>();
			NKMWarfareTileTemplet tile = mapTemplet.GetTile(tileIndex);
			if (tile != null)
			{
				foreach (short num in tile.NeighborTiles)
				{
					byte b = (byte)num;
					if (includeSelf || (int)b != tileIndex)
					{
						WarfareTileData tileData = warfareGameData.GetTileData((int)b);
						list.Add(tileData);
					}
				}
			}
			return list;
		}

		// Token: 0x060045FD RID: 17917 RVA: 0x001545F0 File Offset: 0x001527F0
		public static void GetCurrWarfareAttackCost(out int itemID, out int itemCount)
		{
			itemID = 0;
			itemCount = 0;
			string warfareStrID = NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareStrID();
			if (NKMWarfareTemplet.Find(warfareStrID) == null)
			{
				return;
			}
			NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(warfareStrID);
			if (nkmstageTempletV == null)
			{
				return;
			}
			if (nkmstageTempletV.m_StageReqItemID > 0)
			{
				itemID = nkmstageTempletV.m_StageReqItemID;
				itemCount = nkmstageTempletV.m_StageReqItemCount;
			}
			else
			{
				itemID = 2;
				itemCount = 0;
			}
			if (nkmstageTempletV.m_StageReqItemID == 2)
			{
				NKCCompanyBuff.SetDiscountOfEterniumInEnteringWarfare(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref itemCount);
			}
		}

		// Token: 0x060045FE RID: 17918 RVA: 0x00154664 File Offset: 0x00152864
		public static List<WarfareUnitData> GetNeighborFriends(NKMWarfareMapTemplet mapTemplet, int tileIndex)
		{
			WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
			List<WarfareUnitData> list = new List<WarfareUnitData>();
			foreach (WarfareTileData warfareTileData in NKCWarfareManager.GetNeighborTiles(mapTemplet, tileIndex, false))
			{
				WarfareUnitData unitDataByTileIndex_TeamA = warfareGameData.GetUnitDataByTileIndex_TeamA((int)warfareTileData.index);
				if (unitDataByTileIndex_TeamA != null)
				{
					list.Add(unitDataByTileIndex_TeamA);
				}
			}
			return list;
		}

		// Token: 0x060045FF RID: 17919 RVA: 0x001546E0 File Offset: 0x001528E0
		public static int GetRecoverableUnitCount(NKMArmyData armyData)
		{
			return NKCWarfareManager.GetRecoverableDeckIndexList(armyData).Count;
		}

		// Token: 0x06004600 RID: 17920 RVA: 0x001546F0 File Offset: 0x001528F0
		public static List<int> GetRecoverableDeckIndexList(NKMArmyData armyData)
		{
			List<WarfareUnitData> list = NKCScenManager.GetScenManager().WarfareGameData.warfareTeamDataA.warfareUnitDataByUIDMap.Values.ToList<WarfareUnitData>();
			NKM_DECK_TYPE nkm_DECK_TYPE = NKM_DECK_TYPE.NDT_NORMAL;
			int unlockedDeckCount = (int)armyData.GetUnlockedDeckCount(nkm_DECK_TYPE);
			List<int> list2 = new List<int>();
			for (int i = 0; i < unlockedDeckCount; i++)
			{
				NKMDeckData deckData = armyData.GetDeckData(nkm_DECK_TYPE, i);
				if (deckData != null && deckData.GetState() == NKM_DECK_STATE.DECK_STATE_WARFARE)
				{
					NKMDeckIndex deckIndex = new NKMDeckIndex(nkm_DECK_TYPE, i);
					WarfareUnitData warfareUnitData = list.Find((WarfareUnitData v) => v.deckIndex.Compare(deckIndex));
					if (warfareUnitData == null || (warfareUnitData.unitType == WarfareUnitData.Type.User && warfareUnitData.hp <= 0f))
					{
						list2.Add(i);
					}
				}
			}
			return list2;
		}
	}
}
