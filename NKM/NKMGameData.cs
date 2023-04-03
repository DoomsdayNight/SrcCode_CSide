using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Logging;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x0200041B RID: 1051
	public sealed class NKMGameData : ISerializable
	{
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06001B64 RID: 7012 RVA: 0x00078120 File Offset: 0x00076320
		public NKMGameStatRate GameStatRate
		{
			get
			{
				if (!this.m_bGameStatCacheSet && this.m_GameStatRateCache == null)
				{
					this.m_bGameStatCacheSet = true;
					if (!string.IsNullOrEmpty(this.m_NKMGameStatRateID))
					{
						NKMGameStatRateTemplet nkmgameStatRateTemplet = NKMGameStatRateTemplet.Find(this.m_NKMGameStatRateID);
						if (nkmgameStatRateTemplet == null)
						{
							Log.Error("GameStatRateTemplet " + this.m_NKMGameStatRateID + " not found!", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGame.cs", 1270);
						}
						else
						{
							this.m_GameStatRateCache = nkmgameStatRateTemplet.m_StatRate;
						}
					}
				}
				return this.m_GameStatRateCache;
			}
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x00078198 File Offset: 0x00076398
		public NKMGameData()
		{
			this.m_NKMGameTeamDataA.Init();
			this.m_NKMGameTeamDataB.Init();
			this.m_NKMGameTeamDataA.m_eNKM_TEAM_TYPE = NKM_TEAM_TYPE.NTT_A1;
			this.m_NKMGameTeamDataB.m_eNKM_TEAM_TYPE = NKM_TEAM_TYPE.NTT_B1;
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x00078254 File Offset: 0x00076454
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_GameUID);
			stream.PutOrGet(ref this.m_GameUnitUIDIndex);
			stream.PutOrGet(ref this.m_bLocal);
			stream.PutOrGetEnum<NKM_GAME_TYPE>(ref this.m_NKM_GAME_TYPE);
			stream.PutOrGet(ref this.m_DungeonID);
			stream.PutOrGet(ref this.m_bBossDungeon);
			stream.PutOrGet(ref this.m_WarfareID);
			stream.PutOrGet(ref this.m_RaidUID);
			stream.PutOrGet(ref this.m_fRespawnCostMinusPercentForTeamA);
			stream.PutOrGet(ref this.m_TeamASupply);
			stream.PutOrGet(ref this.m_fTeamAAttackPowerIncRateForWarfare);
			stream.PutOrGet(ref this.m_lstTeamABuffStrIDListForRaid);
			stream.PutOrGet(ref this.fExtraRespawnCostAddForA);
			stream.PutOrGet(ref this.fExtraRespawnCostAddForB);
			stream.PutOrGet(ref this.m_TeamBLevelAdd);
			stream.PutOrGet(ref this.m_TeamBLevelFix);
			stream.PutOrGet(ref this.m_fDoubleCostTime);
			stream.PutOrGet(ref this.m_MapID);
			stream.PutOrGet(ref this.m_BattleConditionIDs);
			stream.PutOrGet<NKMGameTeamData>(ref this.m_NKMGameTeamDataA);
			stream.PutOrGet<NKMGameTeamData>(ref this.m_NKMGameTeamDataB);
			stream.PutOrGet(ref this.m_listUnitDeckTemp);
			stream.PutOrGet(ref this.m_replay);
			stream.PutOrGet<NKMBanData>(ref this.m_dicNKMBanData);
			stream.PutOrGet<NKMBanShipData>(ref this.m_dicNKMBanShipData);
			stream.PutOrGet<NKMBanOperatorData>(ref this.m_dicNKMBanOperatorData);
			stream.PutOrGet<NKMUnitUpData>(ref this.m_dicNKMUpData);
			stream.PutOrGet(ref this.m_NKMGameStatRateID);
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x000783B1 File Offset: 0x000765B1
		public void SetGameType(NKM_GAME_TYPE gameType)
		{
			this.m_NKM_GAME_TYPE = gameType;
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x000783BC File Offset: 0x000765BC
		public void SetDungeonRespawnUnitTemplet()
		{
			if (this.m_NKMGameTeamDataB.m_MainShip != null)
			{
				this.m_NKMGameTeamDataB.m_MainShip.SetDungeonRespawnUnitTemplet();
			}
			for (int i = 0; i < this.m_NKMGameTeamDataB.m_listUnitData.Count; i++)
			{
				this.m_NKMGameTeamDataB.m_listUnitData[i].SetDungeonRespawnUnitTemplet();
			}
			for (int j = 0; j < this.m_NKMGameTeamDataA.m_listEvevtUnitData.Count; j++)
			{
				this.m_NKMGameTeamDataA.m_listEvevtUnitData[j].SetDungeonRespawnUnitTemplet();
			}
			for (int k = 0; k < this.m_NKMGameTeamDataB.m_listEvevtUnitData.Count; k++)
			{
				this.m_NKMGameTeamDataB.m_listEvevtUnitData[k].SetDungeonRespawnUnitTemplet();
			}
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x00078479 File Offset: 0x00076679
		public NKM_GAME_TYPE GetGameType()
		{
			return this.m_NKM_GAME_TYPE;
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x00078481 File Offset: 0x00076681
		public NKMUnitData GetUnitDataByUnitUID(long unitUID)
		{
			return this.m_NKMGameTeamDataA.GetUnitDataByUnitUID(unitUID) ?? this.m_NKMGameTeamDataB.GetUnitDataByUnitUID(unitUID);
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x000784A0 File Offset: 0x000766A0
		public NKM_TEAM_TYPE GetTeamType(long uid)
		{
			NKMGameTeamData teamData = this.GetTeamData(uid);
			if (teamData != null)
			{
				return teamData.m_eNKM_TEAM_TYPE;
			}
			Log.Error(string.Format("Can't find Team of uid {0}", uid), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGame.cs", 1372);
			return NKM_TEAM_TYPE.NTT_INVALID;
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x000784DF File Offset: 0x000766DF
		public NKMGameTeamData GetTeamData(long uid)
		{
			if (this.m_NKMGameTeamDataA != null && this.m_NKMGameTeamDataA.m_user_uid == uid)
			{
				return this.m_NKMGameTeamDataA;
			}
			if (this.m_NKMGameTeamDataB != null && this.m_NKMGameTeamDataB.m_user_uid == uid)
			{
				return this.m_NKMGameTeamDataB;
			}
			return null;
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x0007851C File Offset: 0x0007671C
		public NKMGameTeamData GetTeamData(NKM_TEAM_TYPE myTeamType)
		{
			if (myTeamType == NKM_TEAM_TYPE.NTT_A1)
			{
				return this.m_NKMGameTeamDataA;
			}
			if (myTeamType != NKM_TEAM_TYPE.NTT_B1)
			{
				return null;
			}
			return this.m_NKMGameTeamDataB;
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x00078538 File Offset: 0x00076738
		public bool IsLeaderUnit(long unitUID)
		{
			return (this.m_NKMGameTeamDataA != null && this.m_NKMGameTeamDataA.GetLeaderUnitData() != null && this.m_NKMGameTeamDataA.GetLeaderUnitData().m_UnitUID == unitUID) || (this.m_NKMGameTeamDataB != null && this.m_NKMGameTeamDataB.GetLeaderUnitData() != null && this.m_NKMGameTeamDataB.GetLeaderUnitData().m_UnitUID == unitUID);
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x0007859A File Offset: 0x0007679A
		public NKM_TEAM_TYPE GetEnemyTeamType(NKM_TEAM_TYPE myTeamType)
		{
			if (myTeamType - NKM_TEAM_TYPE.NTT_A1 <= 1)
			{
				return this.m_NKMGameTeamDataB.m_eNKM_TEAM_TYPE;
			}
			if (myTeamType - NKM_TEAM_TYPE.NTT_B1 > 1)
			{
				Log.Error("GetEnemyTeamData Error : Invalid Teamtype", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGame.cs", 1429);
				return NKM_TEAM_TYPE.NTT_INVALID;
			}
			return this.m_NKMGameTeamDataA.m_eNKM_TEAM_TYPE;
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x000785D7 File Offset: 0x000767D7
		public NKMGameTeamData GetEnemyTeamData(NKM_TEAM_TYPE myTeamType)
		{
			if (myTeamType - NKM_TEAM_TYPE.NTT_A1 <= 1)
			{
				return this.m_NKMGameTeamDataB;
			}
			if (myTeamType - NKM_TEAM_TYPE.NTT_B1 > 1)
			{
				Log.Error("GetEnemyTeamData Error : Invalid Teamtype", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMGame.cs", 1445);
				return this.m_NKMGameTeamDataB;
			}
			return this.m_NKMGameTeamDataA;
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x0007860F File Offset: 0x0007680F
		public void ShuffleDeck()
		{
			this.ShuffleDeck(this.m_NKMGameTeamDataA);
			this.ShuffleDeck(this.m_NKMGameTeamDataB);
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x00078629 File Offset: 0x00076829
		public void ShuffleDeckForOnlyTeamA()
		{
			this.ShuffleDeck(this.m_NKMGameTeamDataA);
			this.DoNotShuffleDeck(this.m_NKMGameTeamDataB);
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x00078643 File Offset: 0x00076843
		public void DoNotShuffleDeck()
		{
			this.DoNotShuffleDeck(this.m_NKMGameTeamDataA);
			this.DoNotShuffleDeck(this.m_NKMGameTeamDataB);
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x0007865D File Offset: 0x0007685D
		public void ShuffleDeckForOnlyTeamB()
		{
			this.DoNotShuffleDeck(this.m_NKMGameTeamDataA);
			this.ShuffleDeck(this.m_NKMGameTeamDataB);
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x00078678 File Offset: 0x00076878
		public void DoNotShuffleDeck(NKMGameTeamData cNKMGameTeamData)
		{
			this.m_listUnitDeckTemp.Clear();
			for (int i = 0; i < cNKMGameTeamData.m_DeckData.GetListUnitDeckCount(); i++)
			{
				cNKMGameTeamData.m_DeckData.SetListUnitDeck(i, 0L);
			}
			for (int j = 0; j < cNKMGameTeamData.m_listUnitData.Count; j++)
			{
				NKMUnitData nkmunitData = cNKMGameTeamData.m_listUnitData[j];
				if (nkmunitData != null)
				{
					this.m_listUnitDeckTemp.Add(nkmunitData.m_UnitUID);
				}
			}
			for (int k = 0; k < this.m_listUnitDeckTemp.Count; k++)
			{
				if (k < cNKMGameTeamData.m_DeckData.GetListUnitDeckCount())
				{
					cNKMGameTeamData.m_DeckData.SetListUnitDeck(k, this.m_listUnitDeckTemp[k]);
				}
				else
				{
					cNKMGameTeamData.m_DeckData.AddListUnitDeckUsed(this.m_listUnitDeckTemp[k]);
				}
			}
			this.m_listUnitDeckTemp.Clear();
			if (cNKMGameTeamData.m_DeckData.GetListUnitDeckUsedCount() > 0)
			{
				cNKMGameTeamData.m_DeckData.SetNextDeck(cNKMGameTeamData.m_DeckData.GetListUnitDeckUsed(0));
				cNKMGameTeamData.m_DeckData.RemoveAtListUnitDeckUsed(0);
			}
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x0007877C File Offset: 0x0007697C
		public void ShuffleDeck(NKMGameTeamData cNKMGameTeamData)
		{
			this.m_listUnitDeckTemp.Clear();
			cNKMGameTeamData.m_DeckData.ClearListUnitDeckUsed();
			for (int i = 0; i < cNKMGameTeamData.m_DeckData.GetListUnitDeckCount(); i++)
			{
				cNKMGameTeamData.m_DeckData.SetListUnitDeck(i, 0L);
			}
			for (int j = 0; j < cNKMGameTeamData.m_listUnitData.Count; j++)
			{
				NKMUnitData nkmunitData = cNKMGameTeamData.m_listUnitData[j];
				if (nkmunitData != null)
				{
					this.m_listUnitDeckTemp.Add(nkmunitData.m_UnitUID);
				}
			}
			int num = 0;
			for (int k = 0; k < this.m_listUnitDeckTemp.Count; k++)
			{
				if (this.m_listUnitDeckTemp[k] == cNKMGameTeamData.m_LeaderUnitUID)
				{
					cNKMGameTeamData.m_DeckData.SetListUnitDeck(num, this.m_listUnitDeckTemp[k]);
					num++;
					this.m_listUnitDeckTemp.RemoveAt(k);
					IL_135:
					while (this.m_listUnitDeckTemp.Count > 0)
					{
						int index = NKMRandom.Range(0, this.m_listUnitDeckTemp.Count);
						if (num < cNKMGameTeamData.m_DeckData.GetListUnitDeckCount())
						{
							cNKMGameTeamData.m_DeckData.SetListUnitDeck(num, this.m_listUnitDeckTemp[index]);
						}
						else
						{
							cNKMGameTeamData.m_DeckData.AddListUnitDeckUsed(this.m_listUnitDeckTemp[index]);
						}
						num++;
						this.m_listUnitDeckTemp.RemoveAt(index);
					}
					this.m_listUnitDeckTemp.Clear();
					if (cNKMGameTeamData.m_DeckData.GetListUnitDeckUsedCount() > 0)
					{
						cNKMGameTeamData.m_DeckData.SetNextDeck(cNKMGameTeamData.m_DeckData.GetListUnitDeckUsed(0));
						cNKMGameTeamData.m_DeckData.RemoveAtListUnitDeckUsed(0);
					}
					return;
				}
			}
			goto IL_135;
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x00078908 File Offset: 0x00076B08
		public short GetGameUnitUID()
		{
			this.m_GameUnitUIDIndex += 1;
			return this.m_GameUnitUIDIndex;
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x0007891F File Offset: 0x00076B1F
		public bool IsPVE()
		{
			return NKMGame.IsPVE(this.m_NKM_GAME_TYPE);
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x0007892C File Offset: 0x00076B2C
		public bool IsPVP()
		{
			return NKMGame.IsPVP(this.m_NKM_GAME_TYPE);
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06001B7A RID: 7034 RVA: 0x00078939 File Offset: 0x00076B39
		public bool IsPVPLeague
		{
			get
			{
				return this.m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_PVP_LEAGUE;
			}
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x00078945 File Offset: 0x00076B45
		public bool IsGuildDungeon()
		{
			return NKMGame.IsGuildDungeon(this.m_NKM_GAME_TYPE);
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x00078952 File Offset: 0x00076B52
		public bool IsBanUnit(int unitID)
		{
			return this.m_dicNKMBanData.ContainsKey(unitID);
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x00078965 File Offset: 0x00076B65
		public int GetBanUnitLevel(int unitID)
		{
			if (unitID == 0)
			{
				return 0;
			}
			if (this.m_dicNKMBanData.ContainsKey(unitID))
			{
				return (int)this.m_dicNKMBanData[unitID].m_BanLevel;
			}
			return 0;
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x0007898D File Offset: 0x00076B8D
		public bool IsUpUnit(int unitID)
		{
			return this.m_dicNKMUpData.ContainsKey(unitID);
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x000789A0 File Offset: 0x00076BA0
		public bool IsBanShip(int shipGroupId)
		{
			return shipGroupId != 0 && this.m_dicNKMBanShipData.ContainsKey(shipGroupId);
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x000789B8 File Offset: 0x00076BB8
		public bool IsBanOperator(int operatorId)
		{
			return operatorId != 0 && this.m_dicNKMBanOperatorData.ContainsKey(operatorId);
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x000789CB File Offset: 0x00076BCB
		public int GetBanShipLevel(int shipGroupId)
		{
			if (shipGroupId == 0)
			{
				return 0;
			}
			if (this.m_dicNKMBanShipData.ContainsKey(shipGroupId))
			{
				return (int)this.m_dicNKMBanShipData[shipGroupId].m_BanLevel;
			}
			return 0;
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x000789F3 File Offset: 0x00076BF3
		public int GetUpUnitLevel(int unitID)
		{
			if (unitID == 0)
			{
				return 0;
			}
			if (this.m_dicNKMUpData.ContainsKey(unitID))
			{
				return (int)this.m_dicNKMUpData[unitID].upLevel;
			}
			return 0;
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x00078A1C File Offset: 0x00076C1C
		public int GetBanOperatorLevel(int operatorId)
		{
			if (operatorId == 0)
			{
				return 0;
			}
			NKMBanOperatorData nkmbanOperatorData;
			if (this.m_dicNKMBanOperatorData.TryGetValue(operatorId, out nkmbanOperatorData))
			{
				return (int)nkmbanOperatorData.m_BanLevel;
			}
			return 0;
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x00078A48 File Offset: 0x00076C48
		public NKMUnitData GetAnyTeamMainShipDataByUnitUID(long unitUID)
		{
			NKMUnitData mainShipDataByUnitUID = this.m_NKMGameTeamDataA.GetMainShipDataByUnitUID(unitUID);
			if (mainShipDataByUnitUID == null)
			{
				mainShipDataByUnitUID = this.m_NKMGameTeamDataB.GetMainShipDataByUnitUID(unitUID);
			}
			return mainShipDataByUnitUID;
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x00078A74 File Offset: 0x00076C74
		public bool IsBanGame()
		{
			return (this.m_dicNKMBanData != null && this.m_dicNKMBanData.Count > 0) || (this.m_dicNKMBanShipData != null && this.m_dicNKMBanShipData.Count > 0) || (this.m_dicNKMBanOperatorData != null && this.m_dicNKMBanOperatorData.Count > 0);
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x00078ACA File Offset: 0x00076CCA
		public bool IsUpUnitGame()
		{
			return this.m_dicNKMUpData != null && this.m_dicNKMUpData.Count > 0;
		}

		// Token: 0x04001B4B RID: 6987
		public long m_GameUID;

		// Token: 0x04001B4C RID: 6988
		public short m_GameUnitUIDIndex;

		// Token: 0x04001B4D RID: 6989
		public bool m_bLocal;

		// Token: 0x04001B4E RID: 6990
		public NKM_GAME_TYPE m_NKM_GAME_TYPE;

		// Token: 0x04001B4F RID: 6991
		public int m_DungeonID;

		// Token: 0x04001B50 RID: 6992
		public bool m_bBossDungeon;

		// Token: 0x04001B51 RID: 6993
		public int m_WarfareID;

		// Token: 0x04001B52 RID: 6994
		public long m_RaidUID;

		// Token: 0x04001B53 RID: 6995
		public float m_fRespawnCostMinusPercentForTeamA;

		// Token: 0x04001B54 RID: 6996
		public int m_TeamASupply;

		// Token: 0x04001B55 RID: 6997
		public float m_fTeamAAttackPowerIncRateForWarfare;

		// Token: 0x04001B56 RID: 6998
		public List<string> m_lstTeamABuffStrIDListForRaid = new List<string>();

		// Token: 0x04001B57 RID: 6999
		public float fExtraRespawnCostAddForA;

		// Token: 0x04001B58 RID: 7000
		public float fExtraRespawnCostAddForB;

		// Token: 0x04001B59 RID: 7001
		public int m_TeamBLevelAdd;

		// Token: 0x04001B5A RID: 7002
		public int m_TeamBLevelFix;

		// Token: 0x04001B5B RID: 7003
		public List<string> m_BanUnitBuffStrIDs = new List<string>();

		// Token: 0x04001B5C RID: 7004
		public float m_fDoubleCostTime = 60f;

		// Token: 0x04001B5D RID: 7005
		public int m_MapID;

		// Token: 0x04001B5E RID: 7006
		public Dictionary<int, int> m_BattleConditionIDs = new Dictionary<int, int>();

		// Token: 0x04001B5F RID: 7007
		public NKMGameTeamData m_NKMGameTeamDataA = new NKMGameTeamData();

		// Token: 0x04001B60 RID: 7008
		public NKMGameTeamData m_NKMGameTeamDataB = new NKMGameTeamData();

		// Token: 0x04001B61 RID: 7009
		private List<long> m_listUnitDeckTemp = new List<long>();

		// Token: 0x04001B62 RID: 7010
		public bool m_replay;

		// Token: 0x04001B63 RID: 7011
		public Dictionary<int, NKMBanData> m_dicNKMBanData = new Dictionary<int, NKMBanData>();

		// Token: 0x04001B64 RID: 7012
		public Dictionary<int, NKMBanShipData> m_dicNKMBanShipData = new Dictionary<int, NKMBanShipData>();

		// Token: 0x04001B65 RID: 7013
		public Dictionary<int, NKMBanOperatorData> m_dicNKMBanOperatorData = new Dictionary<int, NKMBanOperatorData>();

		// Token: 0x04001B66 RID: 7014
		public Dictionary<int, NKMUnitUpData> m_dicNKMUpData = new Dictionary<int, NKMUnitUpData>();

		// Token: 0x04001B67 RID: 7015
		public string m_NKMGameStatRateID;

		// Token: 0x04001B68 RID: 7016
		private NKMGameStatRate m_GameStatRateCache;

		// Token: 0x04001B69 RID: 7017
		private bool m_bGameStatCacheSet;
	}
}
