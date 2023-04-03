using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Community;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000412 RID: 1042
	public class NKMGameTeamData : ISerializable
	{
		// Token: 0x06001B46 RID: 6982 RVA: 0x000777A8 File Offset: 0x000759A8
		public void Init()
		{
			this.m_eNKM_TEAM_TYPE = NKM_TEAM_TYPE.NTT_INVALID;
			this.m_LeaderUnitUID = 0L;
			this.m_UserLevel = 0;
			this.m_UserNickname = "";
			this.m_Tier = 0;
			this.m_Score = 0;
			this.m_MainShip = null;
			this.m_user_uid = 0L;
			this.m_listUnitData.Clear();
			this.m_listAssistUnitData.Clear();
			this.m_listEvevtUnitData.Clear();
			this.m_listDynamicRespawnUnitData.Clear();
			this.m_DeckData.Init();
			this.m_fInitHP = 0f;
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x00077838 File Offset: 0x00075A38
		public virtual void Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_TEAM_TYPE>(ref this.m_eNKM_TEAM_TYPE);
			stream.PutOrGet(ref this.m_LeaderUnitUID);
			stream.PutOrGet(ref this.m_UserLevel);
			stream.PutOrGet(ref this.m_UserNickname);
			stream.PutOrGet(ref this.m_Tier);
			stream.PutOrGet(ref this.m_Score);
			stream.PutOrGet(ref this.m_WinStreak);
			stream.PutOrGet<NKMUnitData>(ref this.m_MainShip);
			stream.PutOrGet<NKMOperator>(ref this.m_Operator);
			stream.PutOrGet(ref this.m_user_uid);
			stream.PutOrGet<NKMUnitData>(ref this.m_listUnitData);
			stream.PutOrGet<NKMUnitData>(ref this.m_listAssistUnitData);
			stream.PutOrGet<NKMUnitData>(ref this.m_listEvevtUnitData);
			stream.PutOrGet<NKMUnitData>(ref this.m_listEnvUnitData);
			stream.PutOrGet<NKMDynamicRespawnUnitData>(ref this.m_listDynamicRespawnUnitData);
			stream.PutOrGet<NKMTacticalCommandData>(ref this.m_listTacticalCommandData);
			stream.PutOrGet<NKMGameTeamDeckData>(ref this.m_DeckData);
			stream.PutOrGet(ref this.m_fInitHP);
			stream.PutOrGet<NKMEquipItemData>(ref this.m_ItemEquipData);
			stream.PutOrGet(ref this.m_FriendCode);
			stream.PutOrGet<EmoticonPresetData>(ref this.m_emoticonPreset);
			stream.PutOrGet<NKMGuildSimpleData>(ref this.guildSimpleData);
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x00077950 File Offset: 0x00075B50
		public int GetLeaderLV()
		{
			NKMUnitData unitDataByUnitUID = this.GetUnitDataByUnitUID(this.m_LeaderUnitUID);
			if (unitDataByUnitUID != null)
			{
				return unitDataByUnitUID.m_UnitLevel;
			}
			return 0;
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x00077975 File Offset: 0x00075B75
		public NKMUnitData GetLeaderUnitData()
		{
			if (this.m_LeaderUnitUID > 0L)
			{
				return this.GetUnitDataByUnitUID(this.m_LeaderUnitUID);
			}
			return null;
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x00077990 File Offset: 0x00075B90
		public NKMUnitData GetFirstUnitData()
		{
			for (int i = 0; i < this.m_listUnitData.Count; i++)
			{
				NKMUnitData nkmunitData = this.m_listUnitData[i];
				if (nkmunitData != null)
				{
					return nkmunitData;
				}
			}
			for (int j = 0; j < this.m_listEvevtUnitData.Count; j++)
			{
				NKMUnitData nkmunitData = this.m_listEvevtUnitData[j];
				if (nkmunitData != null)
				{
					return nkmunitData;
				}
			}
			for (int k = 0; k < this.m_listDynamicRespawnUnitData.Count; k++)
			{
				NKMDynamicRespawnUnitData nkmdynamicRespawnUnitData = this.m_listDynamicRespawnUnitData[k];
				if (nkmdynamicRespawnUnitData != null)
				{
					return nkmdynamicRespawnUnitData.m_NKMUnitData;
				}
			}
			for (int l = 0; l < this.m_listAssistUnitData.Count; l++)
			{
				NKMUnitData nkmunitData = this.m_listAssistUnitData[l];
				if (nkmunitData != null)
				{
					return nkmunitData;
				}
			}
			return null;
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x00077A4D File Offset: 0x00075C4D
		public NKMUnitData GetMainShipDataByUnitUID(long unitUID)
		{
			if (this.m_MainShip == null || this.m_MainShip.m_UnitUID != unitUID)
			{
				return null;
			}
			return this.m_MainShip;
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x00077A70 File Offset: 0x00075C70
		public NKMUnitData GetUnitDataByUnitUID(long unitUID)
		{
			NKMUnitData nkmunitData = this.GetMainShipDataByUnitUID(unitUID);
			if (nkmunitData != null)
			{
				return nkmunitData;
			}
			if (unitUID <= 0L)
			{
				return null;
			}
			for (int i = 0; i < this.m_listUnitData.Count; i++)
			{
				nkmunitData = this.m_listUnitData[i];
				if (nkmunitData != null && nkmunitData.m_UnitUID == unitUID)
				{
					return nkmunitData;
				}
			}
			for (int j = 0; j < this.m_listAssistUnitData.Count; j++)
			{
				nkmunitData = this.m_listAssistUnitData[j];
				if (nkmunitData != null && nkmunitData.m_UnitUID == unitUID)
				{
					return nkmunitData;
				}
			}
			for (int k = 0; k < this.m_listEvevtUnitData.Count; k++)
			{
				nkmunitData = this.m_listEvevtUnitData[k];
				if (nkmunitData != null && nkmunitData.m_UnitUID == unitUID)
				{
					return nkmunitData;
				}
			}
			for (int l = 0; l < this.m_listDynamicRespawnUnitData.Count; l++)
			{
				NKMDynamicRespawnUnitData nkmdynamicRespawnUnitData = this.m_listDynamicRespawnUnitData[l];
				if (nkmdynamicRespawnUnitData != null && nkmdynamicRespawnUnitData.m_NKMUnitData.m_UnitUID == unitUID)
				{
					return nkmdynamicRespawnUnitData.m_NKMUnitData;
				}
			}
			return null;
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x00077B69 File Offset: 0x00075D69
		public NKMUnitData GetAssistUnitDataByIndex(int index)
		{
			if (this.m_listAssistUnitData.Count <= 0)
			{
				return null;
			}
			if (index < 0)
			{
				return null;
			}
			if (index >= this.m_listAssistUnitData.Count)
			{
				return null;
			}
			return this.m_listAssistUnitData[index];
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x00077BA0 File Offset: 0x00075DA0
		public bool IsFullDeck()
		{
			if (this.m_listUnitData.Count < 8)
			{
				return false;
			}
			for (int i = 0; i < this.m_listUnitData.Count; i++)
			{
				if (this.m_listUnitData[i] == null)
				{
					return false;
				}
				if (this.m_listUnitData[i].m_UnitID <= 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x00077BFC File Offset: 0x00075DFC
		public bool IsAssistUnit(long unitUID)
		{
			for (int i = 0; i < this.m_listAssistUnitData.Count; i++)
			{
				NKMUnitData nkmunitData = this.m_listAssistUnitData[i];
				if (nkmunitData != null && nkmunitData.m_UnitUID == unitUID)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x00077C3C File Offset: 0x00075E3C
		public NKMTacticalCommandData GetTacticalCommandDataByID(short TCID)
		{
			for (int i = 0; i < this.m_listTacticalCommandData.Count; i++)
			{
				if (this.m_listTacticalCommandData[i].m_TCID == TCID)
				{
					return this.m_listTacticalCommandData[i];
				}
			}
			return null;
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x00077C81 File Offset: 0x00075E81
		public void SetInfo(PvpState pvpData, NKMGuildSimpleData guildSimpleData)
		{
			this.m_Score = pvpData.Score;
			this.m_Tier = pvpData.LeagueTierID;
			this.m_WinStreak = pvpData.WinStreak;
			this.guildSimpleData = guildSimpleData;
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x00077CB0 File Offset: 0x00075EB0
		public NKMTacticalCommandData GetTC_Combo()
		{
			for (int i = 0; i < this.m_listTacticalCommandData.Count; i++)
			{
				NKMTacticalCommandData nkmtacticalCommandData = this.m_listTacticalCommandData[i];
				if (nkmtacticalCommandData != null)
				{
					NKMTacticalCommandTemplet tacticalCommandTempletByID = NKMTacticalCommandManager.GetTacticalCommandTempletByID((int)nkmtacticalCommandData.m_TCID);
					if (tacticalCommandTempletByID != null && tacticalCommandTempletByID.m_NKM_TACTICAL_COMMAND_TYPE == NKM_TACTICAL_COMMAND_TYPE.NTCT_COMBO)
					{
						return nkmtacticalCommandData;
					}
				}
			}
			return null;
		}

		// Token: 0x04001B05 RID: 6917
		public NKM_TEAM_TYPE m_eNKM_TEAM_TYPE;

		// Token: 0x04001B06 RID: 6918
		public long m_LeaderUnitUID;

		// Token: 0x04001B07 RID: 6919
		public int m_UserLevel;

		// Token: 0x04001B08 RID: 6920
		public string m_UserNickname;

		// Token: 0x04001B09 RID: 6921
		public int m_Tier;

		// Token: 0x04001B0A RID: 6922
		public int m_Score;

		// Token: 0x04001B0B RID: 6923
		public int m_WinStreak;

		// Token: 0x04001B0C RID: 6924
		public long m_FriendCode;

		// Token: 0x04001B0D RID: 6925
		public NKMGuildSimpleData guildSimpleData = new NKMGuildSimpleData();

		// Token: 0x04001B0E RID: 6926
		public NKMUnitData m_MainShip;

		// Token: 0x04001B0F RID: 6927
		public NKMOperator m_Operator;

		// Token: 0x04001B10 RID: 6928
		public long m_user_uid;

		// Token: 0x04001B11 RID: 6929
		public List<NKMUnitData> m_listUnitData = new List<NKMUnitData>();

		// Token: 0x04001B12 RID: 6930
		public List<NKMUnitData> m_listAssistUnitData = new List<NKMUnitData>();

		// Token: 0x04001B13 RID: 6931
		public List<NKMUnitData> m_listEvevtUnitData = new List<NKMUnitData>();

		// Token: 0x04001B14 RID: 6932
		public List<NKMUnitData> m_listEnvUnitData = new List<NKMUnitData>();

		// Token: 0x04001B15 RID: 6933
		public List<NKMDynamicRespawnUnitData> m_listDynamicRespawnUnitData = new List<NKMDynamicRespawnUnitData>();

		// Token: 0x04001B16 RID: 6934
		public List<NKMTacticalCommandData> m_listTacticalCommandData = new List<NKMTacticalCommandData>();

		// Token: 0x04001B17 RID: 6935
		public NKMGameTeamDeckData m_DeckData = new NKMGameTeamDeckData();

		// Token: 0x04001B18 RID: 6936
		public float m_fInitHP;

		// Token: 0x04001B19 RID: 6937
		public Dictionary<long, NKMEquipItemData> m_ItemEquipData = new Dictionary<long, NKMEquipItemData>();

		// Token: 0x04001B1A RID: 6938
		public EmoticonPresetData m_emoticonPreset = new EmoticonPresetData();
	}
}
