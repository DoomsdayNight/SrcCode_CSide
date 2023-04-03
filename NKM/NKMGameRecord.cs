using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x0200041E RID: 1054
	public sealed class NKMGameRecord : ISerializable
	{
		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06001C12 RID: 7186 RVA: 0x0007ED9B File Offset: 0x0007CF9B
		public Dictionary<short, NKMGameRecordUnitData> UnitRecordList
		{
			get
			{
				return this.unitRecords;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06001C13 RID: 7187 RVA: 0x0007EDA3 File Offset: 0x0007CFA3
		public float ToTalDamageA
		{
			get
			{
				return this.totalDamageA;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06001C14 RID: 7188 RVA: 0x0007EDAB File Offset: 0x0007CFAB
		public float TotalDamageB
		{
			get
			{
				return this.totalDamageB;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06001C15 RID: 7189 RVA: 0x0007EDB3 File Offset: 0x0007CFB3
		public int TotalDieCountA
		{
			get
			{
				return this.totalDieCountA;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06001C16 RID: 7190 RVA: 0x0007EDBB File Offset: 0x0007CFBB
		public int TotalDieCountB
		{
			get
			{
				return this.totalDieCountB;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06001C17 RID: 7191 RVA: 0x0007EDC3 File Offset: 0x0007CFC3
		public float TotalFiercePoint
		{
			get
			{
				return this.totalFiercePoint;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06001C18 RID: 7192 RVA: 0x0007EDCB File Offset: 0x0007CFCB
		public float FiercePenaltyPoint
		{
			get
			{
				return this.fiercePenaltyPoint;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06001C19 RID: 7193 RVA: 0x0007EDD3 File Offset: 0x0007CFD3
		public float TotalTrimPoint
		{
			get
			{
				return this.totalTrimPoint;
			}
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x0007EDDC File Offset: 0x0007CFDC
		public void AddDamage(short attckGameuid, NKM_TEAM_TYPE teamType, NKMUnit target, float damage)
		{
			this.unitRecords[attckGameuid].recordGiveDamage += damage;
			this.unitRecords[target.GetUnitDataGame().m_GameUnitUID].recordTakeDamage += damage;
			if (teamType - NKM_TEAM_TYPE.NTT_A1 <= 1)
			{
				this.totalDamageA += damage;
				return;
			}
			if (teamType - NKM_TEAM_TYPE.NTT_B1 > 1)
			{
				return;
			}
			this.totalDamageB += damage;
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x0007EE53 File Offset: 0x0007D053
		public void SetTotalFiercePoint(float value, float penaltyPoint)
		{
			this.totalFiercePoint = value;
			this.fiercePenaltyPoint = penaltyPoint;
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x0007EE63 File Offset: 0x0007D063
		public void SetTotalTrimPoint(float value)
		{
			this.totalTrimPoint = value;
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x0007EE6C File Offset: 0x0007D06C
		public void AddHeal(short leaderGameUid, float value)
		{
			NKMGameRecordUnitData nkmgameRecordUnitData;
			if (this.unitRecords.TryGetValue(leaderGameUid, out nkmgameRecordUnitData))
			{
				nkmgameRecordUnitData.recordHeal += value;
			}
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x0007EE98 File Offset: 0x0007D098
		public void AddSummonCount(NKMUnit summonUnit)
		{
			NKMUnitDataGame unitDataGame = summonUnit.GetUnitDataGame();
			NKMGameRecordUnitData nkmgameRecordUnitData;
			if (!this.unitRecords.TryGetValue(unitDataGame.m_GameUnitUID, out nkmgameRecordUnitData))
			{
				NKMUnitData unitData = summonUnit.GetUnitData();
				NKMGameTeamData teamData = summonUnit.GetTeamData();
				nkmgameRecordUnitData = new NKMGameRecordUnitData
				{
					unitId = unitData.m_UnitID,
					unitLevel = unitData.m_UnitLevel,
					isSummonee = (unitDataGame.m_MasterGameUnitUID != 0),
					isAssistUnit = teamData.IsAssistUnit(unitData.m_UnitUID),
					isLeader = (teamData.m_LeaderUnitUID == unitData.m_UnitUID),
					teamType = teamData.m_eNKM_TEAM_TYPE,
					changeUnitName = ((unitData.m_DungeonRespawnUnitTemplet != null) ? unitData.m_DungeonRespawnUnitTemplet.m_ChangeUnitName : null)
				};
				this.unitRecords.Add(unitDataGame.m_GameUnitUID, nkmgameRecordUnitData);
			}
			nkmgameRecordUnitData.recordSummonCount++;
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x0007EF70 File Offset: 0x0007D170
		public void AddDieCount(NKMUnit dieUnit)
		{
			this.unitRecords[dieUnit.GetUnitDataGame().m_GameUnitUID].recordDieCount++;
			if (dieUnit.IsATeam())
			{
				this.totalDieCountA++;
				return;
			}
			this.totalDieCountB++;
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x0007EFC5 File Offset: 0x0007D1C5
		public void AddKillCount(short gameUnitUid)
		{
			this.unitRecords[gameUnitUid].recordKillCount++;
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x0007EFE0 File Offset: 0x0007D1E0
		public void AddPlayTime(NKMUnit dieUnit, float value)
		{
			this.unitRecords[dieUnit.GetUnitDataGame().m_GameUnitUID].playtime += (int)value;
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x0007F006 File Offset: 0x0007D206
		public float GetTotalDamage(NKM_TEAM_TYPE teamType)
		{
			if (teamType - NKM_TEAM_TYPE.NTT_A1 <= 1)
			{
				return this.totalDamageA;
			}
			if (teamType - NKM_TEAM_TYPE.NTT_B1 > 1)
			{
				return 0f;
			}
			return this.totalDamageB;
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x0007F029 File Offset: 0x0007D229
		public NKMGameRecordUnitData GetUnitHp2(short index)
		{
			return this.unitRecords[index];
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x0007F037 File Offset: 0x0007D237
		public int GetTotalDieCount(NKM_TEAM_TYPE teamType)
		{
			if (teamType - NKM_TEAM_TYPE.NTT_A1 <= 1)
			{
				return this.totalDieCountA;
			}
			if (teamType - NKM_TEAM_TYPE.NTT_B1 > 1)
			{
				return 0;
			}
			return this.totalDieCountB;
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x0007F056 File Offset: 0x0007D256
		public int GetTotalKillCount(NKM_TEAM_TYPE teamType)
		{
			if (teamType - NKM_TEAM_TYPE.NTT_A1 <= 1)
			{
				return this.totalDieCountB;
			}
			if (teamType - NKM_TEAM_TYPE.NTT_B1 > 1)
			{
				return 0;
			}
			return this.totalDieCountA;
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x0007F078 File Offset: 0x0007D278
		public int GetSummonCount(short gameUnitUid)
		{
			NKMGameRecordUnitData nkmgameRecordUnitData;
			if (!this.unitRecords.TryGetValue(gameUnitUid, out nkmgameRecordUnitData))
			{
				return 0;
			}
			return nkmgameRecordUnitData.recordSummonCount;
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x0007F0A0 File Offset: 0x0007D2A0
		public int GetPlayTime(short gameUnitUid)
		{
			NKMGameRecordUnitData nkmgameRecordUnitData;
			if (!this.unitRecords.TryGetValue(gameUnitUid, out nkmgameRecordUnitData))
			{
				return 0;
			}
			return nkmgameRecordUnitData.playtime;
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x0007F0C5 File Offset: 0x0007D2C5
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMGameRecordUnitData>(ref this.unitRecords);
			stream.PutOrGet(ref this.totalDamageA);
			stream.PutOrGet(ref this.totalDamageB);
			stream.PutOrGet(ref this.totalFiercePoint);
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x0007F0F7 File Offset: 0x0007D2F7
		public float GetShipTakeDamage(short shipIndex)
		{
			return this.unitRecords[shipIndex].recordTakeDamage;
		}

		// Token: 0x04001BA9 RID: 7081
		private Dictionary<short, NKMGameRecordUnitData> unitRecords = new Dictionary<short, NKMGameRecordUnitData>();

		// Token: 0x04001BAA RID: 7082
		private float totalDamageA;

		// Token: 0x04001BAB RID: 7083
		private float totalDamageB;

		// Token: 0x04001BAC RID: 7084
		private int totalDieCountA;

		// Token: 0x04001BAD RID: 7085
		private int totalDieCountB;

		// Token: 0x04001BAE RID: 7086
		private float totalFiercePoint;

		// Token: 0x04001BAF RID: 7087
		private float fiercePenaltyPoint;

		// Token: 0x04001BB0 RID: 7088
		private float totalTrimPoint;
	}
}
