using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Warfare;

namespace NKM
{
	// Token: 0x0200052D RID: 1325
	public static class NKMWarfareGameDataExt
	{
		// Token: 0x060025C2 RID: 9666 RVA: 0x000C2728 File Offset: 0x000C0928
		public static void UpdateData(this WarfareGameData self, WarfareSyncData syncData)
		{
			if (syncData == null)
			{
				return;
			}
			if (syncData.gameState != null)
			{
				self.UpdateGameState(syncData.gameState);
			}
			foreach (WarfareUnitSyncData warfareUnitSyncData in syncData.updatedUnits)
			{
				if (warfareUnitSyncData != null)
				{
					WarfareUnitData unitData = self.GetUnitData(warfareUnitSyncData.warfareGameUnitUID);
					if (unitData != null)
					{
						self.UpdateUnitData(unitData, warfareUnitSyncData);
					}
				}
			}
			foreach (WarfareTileData warfareTileData in syncData.tiles)
			{
				WarfareTileData tileData = self.GetTileData((int)warfareTileData.index);
				if (tileData != null)
				{
					self.UpdateTileData(tileData, warfareTileData);
				}
			}
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x000C2800 File Offset: 0x000C0A00
		public static void UpdateUnitData(this WarfareGameData self, WarfareUnitData unitData, WarfareUnitSyncData syncData)
		{
			unitData.hp = syncData.hp;
			unitData.isTurnEnd = syncData.isTurnEnd;
			unitData.supply = (int)syncData.supply;
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x000C2828 File Offset: 0x000C0A28
		public static void UpdateGameState(this WarfareGameData self, WarfareGameSyncData cNKMWarfareGameSyncData)
		{
			self.warfareGameState = cNKMWarfareGameSyncData.warfareGameState;
			self.isTurnA = cNKMWarfareGameSyncData.isTurnA;
			self.turnCount = cNKMWarfareGameSyncData.turnCount;
			self.firstAttackCount = cNKMWarfareGameSyncData.firstAttackCount;
			self.assistCount = cNKMWarfareGameSyncData.assistCount;
			self.battleAllyUid = cNKMWarfareGameSyncData.battleAllyUid;
			self.battleMonsterUid = cNKMWarfareGameSyncData.battleMonsterUid;
			self.isWinTeamA = cNKMWarfareGameSyncData.isWinTeamA;
			self.holdCount = cNKMWarfareGameSyncData.holdCount;
			self.containerCount = cNKMWarfareGameSyncData.containerCount;
			self.enemiesKillCount = cNKMWarfareGameSyncData.enemiesKillCount;
			self.alliesKillCount = cNKMWarfareGameSyncData.alliesKillCount;
			self.targetKillCount = cNKMWarfareGameSyncData.targetKillCount;
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x000C28D1 File Offset: 0x000C0AD1
		public static void UpdateTileData(this WarfareGameData self, WarfareTileData currentTile, WarfareTileData syncTile)
		{
			currentTile.tileType = syncTile.tileType;
			currentTile.battleConditionId = syncTile.battleConditionId;
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x000C28EB File Offset: 0x000C0AEB
		public static WarfareTileData GetTileData(this WarfareGameData self, int index)
		{
			if (index < 0 || index >= self.warfareTileDataList.Count)
			{
				return null;
			}
			return self.warfareTileDataList[index];
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x000C290D File Offset: 0x000C0B0D
		public static bool CheckTeamA_By_GameUnitUID(this WarfareGameData self, int guuid)
		{
			return self.warfareTeamDataA != null && self.warfareTeamDataA.warfareUnitDataByUIDMap.ContainsKey(guuid);
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x000C2930 File Offset: 0x000C0B30
		public static WarfareUnitData GetUnitData(this WarfareGameData self, int guuid)
		{
			if (self.warfareTeamDataA != null && self.warfareTeamDataA.warfareUnitDataByUIDMap.ContainsKey(guuid))
			{
				return self.warfareTeamDataA.warfareUnitDataByUIDMap[guuid];
			}
			if (self.warfareTeamDataB != null && self.warfareTeamDataB.warfareUnitDataByUIDMap.ContainsKey(guuid))
			{
				return self.warfareTeamDataB.warfareUnitDataByUIDMap[guuid];
			}
			return null;
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x000C2998 File Offset: 0x000C0B98
		public static WarfareUnitData GetUnitDataByNormalDeckIndex(this WarfareGameData self, byte normalDeckIndex)
		{
			if (self.warfareTeamDataA != null)
			{
				foreach (WarfareUnitData warfareUnitData in self.warfareTeamDataA.warfareUnitDataByUIDMap.Values)
				{
					if (warfareUnitData.deckIndex.m_eDeckType == NKM_DECK_TYPE.NDT_NORMAL && warfareUnitData.deckIndex.m_iIndex == normalDeckIndex)
					{
						return warfareUnitData;
					}
				}
			}
			return null;
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x000C2A1C File Offset: 0x000C0C1C
		public static WarfareUnitData GetUnitDataByTileIndex(this WarfareGameData self, int tileIndex)
		{
			WarfareUnitData warfareUnitData = self.GetUnitDataByTileIndex_TeamA(tileIndex);
			if (warfareUnitData != null)
			{
				return warfareUnitData;
			}
			warfareUnitData = self.GetUnitDataByTileIndex_TeamB(tileIndex);
			if (warfareUnitData != null)
			{
				return warfareUnitData;
			}
			return null;
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x000C2A44 File Offset: 0x000C0C44
		public static WarfareUnitData GetUnitDataByTileIndex_TeamA(this WarfareGameData self, int tileIndex)
		{
			if (self.warfareTeamDataA != null)
			{
				foreach (WarfareUnitData warfareUnitData in self.warfareTeamDataA.warfareUnitDataByUIDMap.Values)
				{
					if ((int)warfareUnitData.tileIndex == tileIndex && warfareUnitData.hp > 0f)
					{
						return warfareUnitData;
					}
				}
			}
			return null;
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x000C2AC0 File Offset: 0x000C0CC0
		public static WarfareUnitData GetUnitDataByTileIndex_TeamB(this WarfareGameData self, int tileIndex)
		{
			if (self.warfareTeamDataB != null)
			{
				foreach (WarfareUnitData warfareUnitData in self.warfareTeamDataB.warfareUnitDataByUIDMap.Values)
				{
					if ((int)warfareUnitData.tileIndex == tileIndex && warfareUnitData.hp > 0f)
					{
						return warfareUnitData;
					}
				}
			}
			return null;
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x000C2B3C File Offset: 0x000C0D3C
		public static List<WarfareUnitData> GetUnitDataList(this WarfareGameData self)
		{
			List<WarfareUnitData> list = new List<WarfareUnitData>();
			if (self.warfareTeamDataA != null)
			{
				list.AddRange(self.warfareTeamDataA.warfareUnitDataByUIDMap.Values.ToList<WarfareUnitData>());
			}
			if (self.warfareTeamDataB != null)
			{
				list.AddRange(self.warfareTeamDataB.warfareUnitDataByUIDMap.Values.ToList<WarfareUnitData>());
			}
			return list;
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x000C2B98 File Offset: 0x000C0D98
		public static void SetUnitTurnEnd(this WarfareGameData self, bool bTurnEnd)
		{
			foreach (WarfareUnitData warfareUnitData in self.warfareTeamDataB.warfareUnitDataByUIDMap.Values)
			{
				warfareUnitData.isTurnEnd = bTurnEnd;
			}
		}
	}
}
