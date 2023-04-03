using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Warfare;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC
{
	// Token: 0x020007E8 RID: 2024
	public class NKCWarfareGameUnitMgr
	{
		// Token: 0x0600502E RID: 20526 RVA: 0x00183F81 File Offset: 0x00182181
		public NKCWarfareGameUnitMgr(Transform _NUM_WARFARE_UNIT_LIST_transform, Transform _NUM_WARFARE_UNIT_INFO_LIST_transform)
		{
			this.m_NUM_WARFARE_UNIT_LIST_transform = _NUM_WARFARE_UNIT_LIST_transform;
			this.m_NUM_WARFARE_UNIT_INFO_LIST_transform = _NUM_WARFARE_UNIT_INFO_LIST_transform;
		}

		// Token: 0x0600502F RID: 20527 RVA: 0x00183FAD File Offset: 0x001821AD
		public void Init()
		{
		}

		// Token: 0x06005030 RID: 20528 RVA: 0x00183FAF File Offset: 0x001821AF
		public NKCWarfareGameUnit GetNKCWarfareGameUnit(int gameUID)
		{
			if (this.m_dicNKCWarfareGameUnit.ContainsKey(gameUID))
			{
				return this.m_dicNKCWarfareGameUnit[gameUID];
			}
			return null;
		}

		// Token: 0x06005031 RID: 20529 RVA: 0x00183FCD File Offset: 0x001821CD
		public NKCWarfareGameUnitInfo GetNKCWarfareGameUnitInfo(int gameUID)
		{
			if (this.m_dicNKCWarfareGameUnitInfo.ContainsKey(gameUID))
			{
				return this.m_dicNKCWarfareGameUnitInfo[gameUID];
			}
			return null;
		}

		// Token: 0x06005032 RID: 20530 RVA: 0x00183FEC File Offset: 0x001821EC
		public void ShowUserUnitTileFX(WarfareUnitData cNKMWarfareUnitData, WarfareUnitSyncData cNKMWarfareUnitSyncData)
		{
			NKCWarfareGameUnit nkcwarfareGameUnit = this.GetNKCWarfareGameUnit(cNKMWarfareUnitData.warfareGameUnitUID);
			if (nkcwarfareGameUnit != null)
			{
				nkcwarfareGameUnit.ShowUserUnitTileFX(cNKMWarfareUnitSyncData);
			}
		}

		// Token: 0x06005033 RID: 20531 RVA: 0x00184018 File Offset: 0x00182218
		public int GetRemainTurnOnUserUnitCount()
		{
			int num = 0;
			for (int i = 0; i < this.m_dicNKCWarfareGameUnitInfo.Count; i++)
			{
				NKCWarfareGameUnitInfo nkcwarfareGameUnitInfo = this.m_dicNKCWarfareGameUnitInfo.Values.ToList<NKCWarfareGameUnitInfo>()[i];
				if (nkcwarfareGameUnitInfo != null && nkcwarfareGameUnitInfo.GetNKMWarfareUnitData() != null && nkcwarfareGameUnitInfo.GetNKMWarfareUnitData().unitType == WarfareUnitData.Type.User && nkcwarfareGameUnitInfo.GetNKMWarfareUnitData().hp > 0f && !nkcwarfareGameUnitInfo.GetNKMWarfareUnitData().isTurnEnd)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06005034 RID: 20532 RVA: 0x00184098 File Offset: 0x00182298
		public void UpdateGameUnitUI()
		{
			for (int i = 0; i < this.m_dicNKCWarfareGameUnitInfo.Count; i++)
			{
				NKCWarfareGameUnitInfo nkcwarfareGameUnitInfo = this.m_dicNKCWarfareGameUnitInfo.Values.ToList<NKCWarfareGameUnitInfo>()[i];
				if (nkcwarfareGameUnitInfo != null && nkcwarfareGameUnitInfo.GetNKMWarfareUnitData() != null)
				{
					this.UpdateGameUnitUI(nkcwarfareGameUnitInfo.GetNKMWarfareUnitData().warfareGameUnitUID);
				}
			}
		}

		// Token: 0x06005035 RID: 20533 RVA: 0x001840F6 File Offset: 0x001822F6
		public void UpdateGameUnitUI(int guuid)
		{
			this.UpdateGameUnitInfoUI(guuid);
			this.UpdateGameUnitTurnUI(guuid);
		}

		// Token: 0x06005036 RID: 20534 RVA: 0x00184108 File Offset: 0x00182308
		public void UpdateGameUnitInfoUI(int gameUnitUid)
		{
			NKCWarfareGameUnitInfo nkcwarfareGameUnitInfo = this.GetNKCWarfareGameUnitInfo(gameUnitUid);
			if (nkcwarfareGameUnitInfo != null)
			{
				nkcwarfareGameUnitInfo.SetUnitInfoUI();
			}
		}

		// Token: 0x06005037 RID: 20535 RVA: 0x0018412C File Offset: 0x0018232C
		public void UpdateGameUnitTurnUI(int gameUnitUid)
		{
			NKCWarfareGameUnit nkcwarfareGameUnit = this.GetNKCWarfareGameUnit(gameUnitUid);
			if (nkcwarfareGameUnit != null)
			{
				nkcwarfareGameUnit.UpdateTurnUI();
			}
		}

		// Token: 0x06005038 RID: 20536 RVA: 0x00184150 File Offset: 0x00182350
		public bool CheckExistMovingUserUnit()
		{
			for (int i = 0; i < this.m_dicNKCWarfareGameUnit.Count; i++)
			{
				NKCWarfareGameUnit nkcwarfareGameUnit = this.m_dicNKCWarfareGameUnit.Values.ToList<NKCWarfareGameUnit>()[i];
				if (nkcwarfareGameUnit.GetNKMWarfareUnitData().unitType == WarfareUnitData.Type.User && nkcwarfareGameUnit.IsMoving())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005039 RID: 20537 RVA: 0x001841A4 File Offset: 0x001823A4
		public bool CheckExistFlagUserUnit()
		{
			for (int i = 0; i < this.m_dicNKCWarfareGameUnitInfo.Count; i++)
			{
				NKCWarfareGameUnitInfo nkcwarfareGameUnitInfo = this.m_dicNKCWarfareGameUnitInfo.Values.ToList<NKCWarfareGameUnitInfo>()[i];
				if (nkcwarfareGameUnitInfo.GetNKMWarfareUnitData().unitType == WarfareUnitData.Type.User && nkcwarfareGameUnitInfo.GetFlag())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600503A RID: 20538 RVA: 0x001841F8 File Offset: 0x001823F8
		public void OnClickGameStart(NKMPacket_WARFARE_GAME_START_REQ startReq, NKMWarfareMapTemplet cNKMWarfareMapTemplet)
		{
			if (startReq == null || cNKMWarfareMapTemplet == null)
			{
				return;
			}
			List<int> list = this.m_dicNKCWarfareGameUnitInfo.Keys.ToList<int>();
			list.Sort();
			for (int i = 0; i < list.Count; i++)
			{
				NKCWarfareGameUnitInfo nkcwarfareGameUnitInfo;
				if (this.m_dicNKCWarfareGameUnitInfo.TryGetValue(list[i], out nkcwarfareGameUnitInfo) && nkcwarfareGameUnitInfo.GetNKMWarfareUnitData().unitType == WarfareUnitData.Type.User)
				{
					if (!nkcwarfareGameUnitInfo.IsSupporter)
					{
						NKMPacket_WARFARE_GAME_START_REQ.UnitPosition item = new NKMPacket_WARFARE_GAME_START_REQ.UnitPosition
						{
							isFlagShip = nkcwarfareGameUnitInfo.GetFlag(),
							deckIndex = nkcwarfareGameUnitInfo.GetNKMWarfareUnitData().deckIndex.m_iIndex,
							tileIndex = (short)nkcwarfareGameUnitInfo.TileIndex
						};
						startReq.unitPositionList.Add(item);
					}
					else
					{
						startReq.friendCode = nkcwarfareGameUnitInfo.FriendCode;
						startReq.friendTileIndex = (short)nkcwarfareGameUnitInfo.TileIndex;
					}
				}
			}
		}

		// Token: 0x0600503B RID: 20539 RVA: 0x001842C4 File Offset: 0x001824C4
		public int GetCurrentUserUnit(bool excludeSupporter = true)
		{
			int num = 0;
			for (int i = 0; i < this.m_dicNKCWarfareGameUnit.Count; i++)
			{
				NKCWarfareGameUnit nkcwarfareGameUnit = this.m_dicNKCWarfareGameUnit.Values.ToList<NKCWarfareGameUnit>()[i];
				if (nkcwarfareGameUnit.GetNKMWarfareUnitData().unitType == WarfareUnitData.Type.User && (!nkcwarfareGameUnit.IsSupporter || !excludeSupporter))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600503C RID: 20540 RVA: 0x0018431C File Offset: 0x0018251C
		public List<int> GetCurrentUserUnitTileIndex()
		{
			List<int> list = new List<int>();
			for (int i = 0; i < this.m_dicNKCWarfareGameUnit.Count; i++)
			{
				NKCWarfareGameUnit nkcwarfareGameUnit = this.m_dicNKCWarfareGameUnit.Values.ToList<NKCWarfareGameUnit>()[i];
				if (nkcwarfareGameUnit.GetNKMWarfareUnitData().unitType == WarfareUnitData.Type.User && !nkcwarfareGameUnit.IsSupporter)
				{
					list.Add(nkcwarfareGameUnit.TileIndex);
				}
			}
			return list;
		}

		// Token: 0x0600503D RID: 20541 RVA: 0x00184380 File Offset: 0x00182580
		public bool ContainSupporterUnit()
		{
			for (int i = 0; i < this.m_dicNKCWarfareGameUnit.Count; i++)
			{
				NKCWarfareGameUnit nkcwarfareGameUnit = this.m_dicNKCWarfareGameUnit.Values.ToList<NKCWarfareGameUnit>()[i];
				if (nkcwarfareGameUnit.GetNKMWarfareUnitData().unitType == WarfareUnitData.Type.User && nkcwarfareGameUnit.IsSupporter)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600503E RID: 20542 RVA: 0x001843D4 File Offset: 0x001825D4
		public void PauseUnits(bool bSet)
		{
			for (int i = 0; i < this.m_dicNKCWarfareGameUnit.Count; i++)
			{
				NKCWarfareGameUnit nkcwarfareGameUnit = this.m_dicNKCWarfareGameUnit.Values.ToList<NKCWarfareGameUnit>()[i];
				if (nkcwarfareGameUnit != null)
				{
					nkcwarfareGameUnit.SetPause(bSet);
				}
			}
		}

		// Token: 0x0600503F RID: 20543 RVA: 0x00184420 File Offset: 0x00182620
		public void ClearUnit(int gameUnitUID)
		{
			NKCWarfareGameUnitInfo nkcwarfareGameUnitInfo = this.GetNKCWarfareGameUnitInfo(gameUnitUID);
			if (nkcwarfareGameUnitInfo != null)
			{
				this.ResetDeckState(nkcwarfareGameUnitInfo.GetNKMWarfareUnitData());
				nkcwarfareGameUnitInfo.SetUnitTransform(null);
				nkcwarfareGameUnitInfo.gameObject.transform.SetParent(null);
				nkcwarfareGameUnitInfo.Close();
			}
			this.m_dicNKCWarfareGameUnitInfo.Remove(gameUnitUID);
			NKCWarfareGameUnit nkcwarfareGameUnit = this.GetNKCWarfareGameUnit(gameUnitUID);
			if (nkcwarfareGameUnit != null)
			{
				nkcwarfareGameUnit.gameObject.transform.SetParent(null);
				nkcwarfareGameUnit.Close();
			}
			this.m_dicNKCWarfareGameUnit.Remove(gameUnitUID);
		}

		// Token: 0x06005040 RID: 20544 RVA: 0x001844AC File Offset: 0x001826AC
		private void ResetDeckState(WarfareUnitData cNKMWarfareUnitData)
		{
			if (NKCScenManager.GetScenManager().WarfareGameData.warfareGameState == NKM_WARFARE_GAME_STATE.NWGS_STOP && cNKMWarfareUnitData.unitType == WarfareUnitData.Type.User)
			{
				if (cNKMWarfareUnitData.friendCode != 0L)
				{
					return;
				}
				NKMDeckData deckData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetDeckData(cNKMWarfareUnitData.deckIndex);
				if (deckData == null)
				{
					return;
				}
				deckData.SetState(NKM_DECK_STATE.DECK_STATE_NORMAL);
				NKCScenManager.CurrentUserData().m_ArmyData.DeckUpdated(cNKMWarfareUnitData.deckIndex, deckData);
			}
		}

		// Token: 0x06005041 RID: 20545 RVA: 0x00184518 File Offset: 0x00182718
		public void ResetAllDeckState()
		{
			for (int i = 0; i < this.m_dicNKCWarfareGameUnitInfo.Count; i++)
			{
				WarfareUnitData nkmwarfareUnitData = this.m_dicNKCWarfareGameUnitInfo.Values.ToList<NKCWarfareGameUnitInfo>()[i].GetNKMWarfareUnitData();
				if (nkmwarfareUnitData != null)
				{
					this.ResetDeckState(nkmwarfareUnitData);
				}
			}
		}

		// Token: 0x06005042 RID: 20546 RVA: 0x00184564 File Offset: 0x00182764
		public void SetUserUnitDeckWarfareState()
		{
			for (int i = 0; i < this.m_dicNKCWarfareGameUnitInfo.Count; i++)
			{
				WarfareUnitData nkmwarfareUnitData = this.m_dicNKCWarfareGameUnitInfo.Values.ToList<NKCWarfareGameUnitInfo>()[i].GetNKMWarfareUnitData();
				if (nkmwarfareUnitData != null && nkmwarfareUnitData.unitType == WarfareUnitData.Type.User && nkmwarfareUnitData.friendCode == 0L)
				{
					NKMDeckData deckData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetDeckData(nkmwarfareUnitData.deckIndex);
					if (deckData != null)
					{
						deckData.SetState(NKM_DECK_STATE.DECK_STATE_WARFARE);
						NKCScenManager.CurrentUserData().m_ArmyData.DeckUpdated(nkmwarfareUnitData.deckIndex, deckData);
					}
				}
			}
		}

		// Token: 0x06005043 RID: 20547 RVA: 0x001845F4 File Offset: 0x001827F4
		public void ClearUnits()
		{
			for (int i = 0; i < this.m_dicNKCWarfareGameUnitInfo.Count; i++)
			{
				NKCWarfareGameUnitInfo nkcwarfareGameUnitInfo = this.m_dicNKCWarfareGameUnitInfo.Values.ToList<NKCWarfareGameUnitInfo>()[i];
				this.ResetDeckState(nkcwarfareGameUnitInfo.GetNKMWarfareUnitData());
				nkcwarfareGameUnitInfo.SetUnitTransform(null);
				nkcwarfareGameUnitInfo.gameObject.transform.SetParent(null);
				nkcwarfareGameUnitInfo.Close();
			}
			this.m_dicNKCWarfareGameUnitInfo.Clear();
			for (int i = 0; i < this.m_dicNKCWarfareGameUnit.Count; i++)
			{
				NKCWarfareGameUnit nkcwarfareGameUnit = this.m_dicNKCWarfareGameUnit.Values.ToList<NKCWarfareGameUnit>()[i];
				nkcwarfareGameUnit.gameObject.transform.SetParent(null);
				nkcwarfareGameUnit.Close();
			}
			this.m_dicNKCWarfareGameUnit.Clear();
		}

		// Token: 0x06005044 RID: 20548 RVA: 0x001846B4 File Offset: 0x001828B4
		public void RefreshDicUnit()
		{
			Dictionary<int, NKCWarfareGameUnit> dictionary = new Dictionary<int, NKCWarfareGameUnit>(this.m_dicNKCWarfareGameUnit);
			Dictionary<int, NKCWarfareGameUnitInfo> dictionary2 = new Dictionary<int, NKCWarfareGameUnitInfo>(this.m_dicNKCWarfareGameUnitInfo);
			this.m_dicNKCWarfareGameUnit.Clear();
			this.m_dicNKCWarfareGameUnitInfo.Clear();
			foreach (KeyValuePair<int, NKCWarfareGameUnit> keyValuePair in dictionary)
			{
				NKCWarfareGameUnit value = keyValuePair.Value;
				if (value.GetNKMWarfareUnitData() != null)
				{
					this.m_dicNKCWarfareGameUnit.Add(value.GetNKMWarfareUnitData().warfareGameUnitUID, value);
				}
			}
			dictionary.Clear();
			foreach (KeyValuePair<int, NKCWarfareGameUnitInfo> keyValuePair2 in dictionary2)
			{
				NKCWarfareGameUnitInfo value2 = keyValuePair2.Value;
				if (value2.GetNKMWarfareUnitData() != null)
				{
					this.m_dicNKCWarfareGameUnitInfo.Add(value2.GetNKMWarfareUnitData().warfareGameUnitUID, value2);
				}
			}
			dictionary2.Clear();
		}

		// Token: 0x06005045 RID: 20549 RVA: 0x00184788 File Offset: 0x00182988
		public NKCWarfareGameUnit GetGameUnitByTileIndex(int tileIndex)
		{
			for (int i = 0; i < this.m_dicNKCWarfareGameUnit.Count; i++)
			{
				NKCWarfareGameUnit nkcwarfareGameUnit = this.m_dicNKCWarfareGameUnit.Values.ToList<NKCWarfareGameUnit>()[i];
				if (nkcwarfareGameUnit != null && nkcwarfareGameUnit.TileIndex == tileIndex)
				{
					return nkcwarfareGameUnit;
				}
			}
			return null;
		}

		// Token: 0x06005046 RID: 20550 RVA: 0x001847DC File Offset: 0x001829DC
		public NKCWarfareGameUnit GetWFGameUnitByDeckIndex(NKMDeckIndex sNKMDeckIndex)
		{
			for (int i = 0; i < this.m_dicNKCWarfareGameUnit.Count; i++)
			{
				NKCWarfareGameUnit nkcwarfareGameUnit = this.m_dicNKCWarfareGameUnit.Values.ToList<NKCWarfareGameUnit>()[i];
				if (nkcwarfareGameUnit != null && nkcwarfareGameUnit.GetNKMWarfareUnitData() != null && nkcwarfareGameUnit.GetNKMWarfareUnitData().unitType == WarfareUnitData.Type.User && nkcwarfareGameUnit.GetNKMWarfareUnitData().deckIndex.Compare(sNKMDeckIndex))
				{
					return nkcwarfareGameUnit;
				}
			}
			return null;
		}

		// Token: 0x06005047 RID: 20551 RVA: 0x0018484C File Offset: 0x00182A4C
		public NKCWarfareGameUnitInfo GetWFGameUnitInfoByTileIndex(int tileIndex)
		{
			for (int i = 0; i < this.m_dicNKCWarfareGameUnitInfo.Count; i++)
			{
				NKCWarfareGameUnitInfo nkcwarfareGameUnitInfo = this.m_dicNKCWarfareGameUnitInfo.Values.ToList<NKCWarfareGameUnitInfo>()[i];
				if (nkcwarfareGameUnitInfo != null && nkcwarfareGameUnitInfo.TileIndex == tileIndex)
				{
					return nkcwarfareGameUnitInfo;
				}
			}
			return null;
		}

		// Token: 0x06005048 RID: 20552 RVA: 0x001848A0 File Offset: 0x00182AA0
		public NKCWarfareGameUnitInfo GetWFGameUnitInfoByWFUnitData(WarfareUnitData cNKMWarfareUnitData)
		{
			for (int i = 0; i < this.m_dicNKCWarfareGameUnitInfo.Count; i++)
			{
				NKCWarfareGameUnitInfo nkcwarfareGameUnitInfo = this.m_dicNKCWarfareGameUnitInfo.Values.ToList<NKCWarfareGameUnitInfo>()[i];
				if (nkcwarfareGameUnitInfo != null && nkcwarfareGameUnitInfo.GetNKMWarfareUnitData() == cNKMWarfareUnitData)
				{
					return nkcwarfareGameUnitInfo;
				}
			}
			return null;
		}

		// Token: 0x06005049 RID: 20553 RVA: 0x001848F4 File Offset: 0x00182AF4
		public void SetUserFlagShip(int gameUnitUID, bool bPlayAni = false)
		{
			for (int i = 0; i < this.m_dicNKCWarfareGameUnitInfo.Count; i++)
			{
				NKCWarfareGameUnitInfo nkcwarfareGameUnitInfo = this.m_dicNKCWarfareGameUnitInfo.Values.ToList<NKCWarfareGameUnitInfo>()[i];
				if (nkcwarfareGameUnitInfo.GetNKMWarfareUnitData().unitType == WarfareUnitData.Type.User)
				{
					nkcwarfareGameUnitInfo.SetFlag(nkcwarfareGameUnitInfo.GetNKMWarfareUnitData().warfareGameUnitUID == gameUnitUID);
					nkcwarfareGameUnitInfo.SetUnitInfoUI();
					if (bPlayAni && nkcwarfareGameUnitInfo.GetFlag())
					{
						nkcwarfareGameUnitInfo.PlayFlagAni();
					}
				}
			}
		}

		// Token: 0x0600504A RID: 20554 RVA: 0x00184968 File Offset: 0x00182B68
		public void ResetUserFlagShip(bool bPlayAni = false)
		{
			bool flag = true;
			for (int i = 0; i < this.m_dicNKCWarfareGameUnitInfo.Count; i++)
			{
				NKCWarfareGameUnitInfo nkcwarfareGameUnitInfo = this.m_dicNKCWarfareGameUnitInfo.Values.ToList<NKCWarfareGameUnitInfo>()[i];
				if (nkcwarfareGameUnitInfo.GetNKMWarfareUnitData().unitType == WarfareUnitData.Type.User && !nkcwarfareGameUnitInfo.IsSupporter)
				{
					nkcwarfareGameUnitInfo.SetFlag(flag);
					nkcwarfareGameUnitInfo.SetUnitInfoUI();
					if (flag)
					{
						if (bPlayAni)
						{
							nkcwarfareGameUnitInfo.PlayFlagAni();
						}
						flag = false;
					}
				}
			}
		}

		// Token: 0x0600504B RID: 20555 RVA: 0x001849D4 File Offset: 0x00182BD4
		public void SetFlagDungeon(int gameUnitUID)
		{
			for (int i = 0; i < this.m_dicNKCWarfareGameUnitInfo.Count; i++)
			{
				NKCWarfareGameUnitInfo nkcwarfareGameUnitInfo = this.m_dicNKCWarfareGameUnitInfo.Values.ToList<NKCWarfareGameUnitInfo>()[i];
				if (nkcwarfareGameUnitInfo.GetNKMWarfareUnitData().unitType == WarfareUnitData.Type.Dungeon)
				{
					nkcwarfareGameUnitInfo.SetFlag(nkcwarfareGameUnitInfo.GetNKMWarfareUnitData().warfareGameUnitUID == gameUnitUID);
					nkcwarfareGameUnitInfo.SetUnitInfoUI();
				}
			}
		}

		// Token: 0x0600504C RID: 20556 RVA: 0x00184A38 File Offset: 0x00182C38
		public NKCWarfareGameUnit CreateNewEnemyUnit(string dungeonStrID, bool bFlag, bool bTarget, short tileIndex, NKM_WARFARE_ENEMY_ACTION_TYPE actionType, NKCWarfareGameUnit.onClickUnit onClickUnit, WarfareUnitData cNewNKMWarfareUnitData = null)
		{
			int num;
			if (cNewNKMWarfareUnitData != null)
			{
				num = cNewNKMWarfareUnitData.warfareGameUnitUID;
				if (this.m_dicNKCWarfareGameUnit.ContainsKey(num))
				{
					return null;
				}
			}
			else
			{
				num = this.m_dicNKCWarfareGameUnit.Count;
				while (this.m_dicNKCWarfareGameUnit.ContainsKey(num))
				{
					num++;
				}
			}
			NKCWarfareGameUnit newInstance = NKCWarfareGameUnit.GetNewInstance(this.m_NUM_WARFARE_UNIT_LIST_transform, onClickUnit);
			if (newInstance == null)
			{
				return null;
			}
			NKCWarfareGameUnitInfo newInstance2 = NKCWarfareGameUnitInfo.GetNewInstance(this.m_NUM_WARFARE_UNIT_INFO_LIST_transform, newInstance.gameObject.transform);
			WarfareUnitData warfareUnitData;
			if (cNewNKMWarfareUnitData != null)
			{
				warfareUnitData = cNewNKMWarfareUnitData;
			}
			else
			{
				warfareUnitData = new WarfareUnitData();
				warfareUnitData.warfareGameUnitUID = num;
				warfareUnitData.unitType = WarfareUnitData.Type.Dungeon;
				warfareUnitData.dungeonID = NKMDungeonManager.GetDungeonID(dungeonStrID);
				warfareUnitData.warfareEnemyActionType = actionType;
				warfareUnitData.tileIndex = tileIndex;
			}
			newInstance.SetNKMWarfareUnitData(warfareUnitData);
			newInstance.OneTimeSetUnitUI();
			if (newInstance2 != null)
			{
				newInstance2.SetNKMWarfareUnitData(warfareUnitData);
				newInstance2.SetFlag(bFlag);
				newInstance2.SetTartget(bTarget);
				newInstance2.SetUnitInfoUI();
			}
			this.m_dicNKCWarfareGameUnit.Add(num, newInstance);
			this.m_dicNKCWarfareGameUnitInfo.Add(num, newInstance2);
			return newInstance;
		}

		// Token: 0x0600504D RID: 20557 RVA: 0x00184B3C File Offset: 0x00182D3C
		public NKCWarfareGameUnit CreateNewUserUnit(NKMDeckIndex selectedDeckIndex, short tileIndex, NKCWarfareGameUnit.onClickUnit onClickUnit, WarfareUnitData cNewNKMWarfareUnitData = null, long friendConde = 0L)
		{
			int num;
			if (cNewNKMWarfareUnitData != null)
			{
				num = cNewNKMWarfareUnitData.warfareGameUnitUID;
				if (this.m_dicNKCWarfareGameUnit.ContainsKey(num))
				{
					return null;
				}
			}
			else
			{
				num = this.m_dicNKCWarfareGameUnit.Count;
				while (this.m_dicNKCWarfareGameUnit.ContainsKey(num))
				{
					num++;
				}
			}
			NKCWarfareGameUnit newInstance = NKCWarfareGameUnit.GetNewInstance(this.m_NUM_WARFARE_UNIT_LIST_transform, onClickUnit);
			if (newInstance == null)
			{
				return null;
			}
			NKCWarfareGameUnitInfo newInstance2 = NKCWarfareGameUnitInfo.GetNewInstance(this.m_NUM_WARFARE_UNIT_INFO_LIST_transform, newInstance.gameObject.transform);
			WarfareUnitData warfareUnitData;
			if (cNewNKMWarfareUnitData != null)
			{
				warfareUnitData = cNewNKMWarfareUnitData;
			}
			else
			{
				warfareUnitData = new WarfareUnitData();
				warfareUnitData.warfareGameUnitUID = num;
				warfareUnitData.unitType = WarfareUnitData.Type.User;
				warfareUnitData.deckIndex = selectedDeckIndex;
				warfareUnitData.supply = 2;
				warfareUnitData.tileIndex = tileIndex;
				warfareUnitData.friendCode = friendConde;
			}
			newInstance.SetNKMWarfareUnitData(warfareUnitData);
			newInstance.OneTimeSetUnitUI();
			if (newInstance2 != null)
			{
				newInstance2.SetNKMWarfareUnitData(warfareUnitData);
				newInstance2.SetUnitInfoUI();
			}
			this.m_dicNKCWarfareGameUnit.Add(num, newInstance);
			this.m_dicNKCWarfareGameUnitInfo.Add(num, newInstance2);
			return newInstance;
		}

		// Token: 0x0600504E RID: 20558 RVA: 0x00184C30 File Offset: 0x00182E30
		public bool CheckDuplicateDeckIndex(NKMDeckIndex deckIndex)
		{
			for (int i = 0; i < this.m_dicNKCWarfareGameUnitInfo.Count; i++)
			{
				NKCWarfareGameUnitInfo nkcwarfareGameUnitInfo = this.m_dicNKCWarfareGameUnitInfo.Values.ToList<NKCWarfareGameUnitInfo>()[i];
				if (nkcwarfareGameUnitInfo.GetNKMWarfareUnitData().unitType == WarfareUnitData.Type.User && nkcwarfareGameUnitInfo.GetNKMWarfareUnitData().deckIndex.Compare(deckIndex))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600504F RID: 20559 RVA: 0x00184C90 File Offset: 0x00182E90
		public void ResetIcon(int unitUID = 0)
		{
			for (int i = 0; i < this.m_dicNKCWarfareGameUnit.Count; i++)
			{
				NKCWarfareGameUnit nkcwarfareGameUnit = this.m_dicNKCWarfareGameUnit.Values.ToList<NKCWarfareGameUnit>()[i];
				if (nkcwarfareGameUnit.GetNKMWarfareUnitData().unitType == WarfareUnitData.Type.Dungeon)
				{
					if (nkcwarfareGameUnit.GetNKMWarfareUnitData().warfareGameUnitUID != unitUID)
					{
						nkcwarfareGameUnit.SetAttackIcon(false, false);
					}
				}
				else
				{
					nkcwarfareGameUnit.SetChangeIcon(false);
				}
			}
			for (int j = 0; j < this.m_dicNKCWarfareGameUnitInfo.Count; j++)
			{
				this.m_dicNKCWarfareGameUnitInfo.Values.ToList<NKCWarfareGameUnitInfo>()[j].SetBattleAssistIcon(false);
			}
		}

		// Token: 0x06005050 RID: 20560 RVA: 0x00184D2C File Offset: 0x00182F2C
		public void Hide()
		{
			List<NKCWarfareGameUnit> list = this.m_dicNKCWarfareGameUnit.Values.ToList<NKCWarfareGameUnit>();
			for (int i = 0; i < list.Count; i++)
			{
				list[i].HideFX();
			}
		}

		// Token: 0x06005051 RID: 20561 RVA: 0x00184D68 File Offset: 0x00182F68
		public void OnStartGameVoice()
		{
			for (int i = 0; i < this.m_dicNKCWarfareGameUnitInfo.Count; i++)
			{
				WarfareUnitData nkmwarfareUnitData = this.m_dicNKCWarfareGameUnitInfo.Values.ToList<NKCWarfareGameUnitInfo>()[i].GetNKMWarfareUnitData();
				if (nkmwarfareUnitData != null && nkmwarfareUnitData.unitType == WarfareUnitData.Type.User && nkmwarfareUnitData.friendCode == 0L)
				{
					NKCOperatorUtil.PlayVoice(nkmwarfareUnitData.deckIndex, VOICE_TYPE.VT_FIELD_READY, false);
				}
			}
		}

		// Token: 0x04004038 RID: 16440
		private Transform m_NUM_WARFARE_UNIT_LIST_transform;

		// Token: 0x04004039 RID: 16441
		private Transform m_NUM_WARFARE_UNIT_INFO_LIST_transform;

		// Token: 0x0400403A RID: 16442
		private Dictionary<int, NKCWarfareGameUnit> m_dicNKCWarfareGameUnit = new Dictionary<int, NKCWarfareGameUnit>();

		// Token: 0x0400403B RID: 16443
		private Dictionary<int, NKCWarfareGameUnitInfo> m_dicNKCWarfareGameUnitInfo = new Dictionary<int, NKCWarfareGameUnitInfo>();
	}
}
