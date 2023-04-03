using System;
using System.Collections.Generic;
using Cs.Math;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000418 RID: 1048
	public class NKMGameRuntimeData : ISerializable
	{
		// Token: 0x06001B59 RID: 7001 RVA: 0x00077E74 File Offset: 0x00076074
		public float GetGamePlayTime()
		{
			float val = this.m_GameTime - 4f;
			return Math.Max(0f, val);
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x00077E9B File Offset: 0x0007609B
		public NKMGameRuntimeTeamData GetMyRuntimeTeamData(NKM_TEAM_TYPE myTeamType)
		{
			if (myTeamType == NKM_TEAM_TYPE.NTT_A1 || myTeamType == NKM_TEAM_TYPE.NTT_A2)
			{
				return this.m_NKMGameRuntimeTeamDataA;
			}
			if (myTeamType == NKM_TEAM_TYPE.NTT_B1 || myTeamType == NKM_TEAM_TYPE.NTT_B2)
			{
				return this.m_NKMGameRuntimeTeamDataB;
			}
			return null;
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x00077EBC File Offset: 0x000760BC
		public NKMGameRuntimeTeamData GetEnemyRuntimeTeamData(NKM_TEAM_TYPE myTeamType)
		{
			if (myTeamType == NKM_TEAM_TYPE.NTT_A1 || myTeamType == NKM_TEAM_TYPE.NTT_A2)
			{
				return this.m_NKMGameRuntimeTeamDataB;
			}
			if (myTeamType == NKM_TEAM_TYPE.NTT_B1 || myTeamType == NKM_TEAM_TYPE.NTT_B2)
			{
				return this.m_NKMGameRuntimeTeamDataA;
			}
			return null;
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x00077EE0 File Offset: 0x000760E0
		public NKM_DUNGEON_END_TYPE GetDungeonEndType()
		{
			NKM_DUNGEON_END_TYPE result = NKM_DUNGEON_END_TYPE.NORMAL;
			if (this.m_bGiveUp)
			{
				result = NKM_DUNGEON_END_TYPE.GIVE_UP;
			}
			else if (this.m_fRemainGameTime.IsNearlyZero(1E-05f))
			{
				result = NKM_DUNGEON_END_TYPE.TIME_OUT;
			}
			return result;
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x00077F10 File Offset: 0x00076110
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_GameTime);
			stream.PutOrGetEnum<NKM_GAME_SPEED_TYPE>(ref this.m_NKM_GAME_SPEED_TYPE);
			stream.PutOrGet(ref this.m_PrevWaveEndTime);
			stream.PutOrGetEnum<NKM_GAME_STATE>(ref this.m_NKM_GAME_STATE);
			stream.PutOrGet(ref this.m_WaveID);
			stream.PutOrGet(ref this.m_fRemainGameTime);
			stream.PutOrGet(ref this.m_fShipDamage);
			stream.PutOrGetEnum<NKM_TEAM_TYPE>(ref this.m_WinTeam);
			stream.PutOrGet(ref this.m_bGameEnded);
			stream.PutOrGet(ref this.m_bPause);
			stream.PutOrGet(ref this.m_bGiveUp);
			stream.PutOrGet<NKMGameRuntimeTeamData>(ref this.m_NKMGameRuntimeTeamDataA);
			stream.PutOrGet<NKMGameRuntimeTeamData>(ref this.m_NKMGameRuntimeTeamDataB);
			stream.PutOrGet<NKMGameSyncData_DungeonEvent>(ref this.m_lstPermanentDungeonEvent);
		}

		// Token: 0x04001B30 RID: 6960
		public float m_GameTime;

		// Token: 0x04001B31 RID: 6961
		public NKM_GAME_SPEED_TYPE m_NKM_GAME_SPEED_TYPE;

		// Token: 0x04001B32 RID: 6962
		public float m_PrevWaveEndTime;

		// Token: 0x04001B33 RID: 6963
		public NKM_GAME_STATE m_NKM_GAME_STATE = NKM_GAME_STATE.NGS_STOP;

		// Token: 0x04001B34 RID: 6964
		public int m_WaveID;

		// Token: 0x04001B35 RID: 6965
		public float m_fRemainGameTime = 180f;

		// Token: 0x04001B36 RID: 6966
		public float m_fShipDamage;

		// Token: 0x04001B37 RID: 6967
		public NKM_TEAM_TYPE m_WinTeam;

		// Token: 0x04001B38 RID: 6968
		public bool m_bGameEnded;

		// Token: 0x04001B39 RID: 6969
		public bool m_bPause;

		// Token: 0x04001B3A RID: 6970
		public bool m_bGiveUp;

		// Token: 0x04001B3B RID: 6971
		public NKMGameRuntimeTeamData m_NKMGameRuntimeTeamDataA = new NKMGameRuntimeTeamData();

		// Token: 0x04001B3C RID: 6972
		public NKMGameRuntimeTeamData m_NKMGameRuntimeTeamDataB = new NKMGameRuntimeTeamData();

		// Token: 0x04001B3D RID: 6973
		public bool m_bPracticeHeal = true;

		// Token: 0x04001B3E RID: 6974
		public bool m_bPracticeFixedDamage;

		// Token: 0x04001B3F RID: 6975
		public List<NKMGameSyncData_DungeonEvent> m_lstPermanentDungeonEvent;
	}
}
