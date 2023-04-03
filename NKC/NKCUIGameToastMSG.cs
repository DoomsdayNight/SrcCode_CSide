using System;
using NKC.UI;
using NKM;

namespace NKC
{
	// Token: 0x020007C4 RID: 1988
	public class NKCUIGameToastMSG
	{
		// Token: 0x06004EAF RID: 20143 RVA: 0x0017B954 File Offset: 0x00179B54
		public void Reset(NKMGameData cNKMGameData, NKMGameRuntimeData cNKMGameRuntimeData)
		{
			this.m_NKMGameData = null;
			this.m_NKMGameRuntimeData = null;
			this.m_NKMDungeonTempletBase = null;
			this.m_GameTime = (int)cNKMGameRuntimeData.GetGamePlayTime();
			this.m_Cost = 0;
			this.m_fShipHP = 0f;
			this.m_fShipMaxHP = 0f;
			if (cNKMGameData == null)
			{
				return;
			}
			if (!cNKMGameData.IsPVE())
			{
				return;
			}
			this.m_NKMDungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(cNKMGameData.m_DungeonID);
			if (this.m_NKMDungeonTempletBase != null)
			{
				this.m_NKMGameData = cNKMGameData;
				this.m_NKMGameRuntimeData = cNKMGameRuntimeData;
				this.m_Cost = (int)cNKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fUsedRespawnCost;
				this.m_fShipHP = 0f;
				this.m_fShipMaxHP = 0f;
				if (cNKMGameData.m_NKMGameTeamDataA.m_MainShip != null)
				{
					for (int i = 0; i < cNKMGameData.m_NKMGameTeamDataA.m_MainShip.m_listGameUnitUID.Count; i++)
					{
						short gameUnitUID = cNKMGameData.m_NKMGameTeamDataA.m_MainShip.m_listGameUnitUID[i];
						NKMUnit unit = NKCScenManager.GetScenManager().GetGameClient().GetUnit(gameUnitUID, true, false);
						if (unit != null)
						{
							this.m_fShipHP += unit.GetUnitSyncData().GetHP();
							this.m_fShipMaxHP += unit.GetUnitFrameData().m_StatData.GetStatFinal(NKM_STAT_TYPE.NST_HP);
						}
					}
				}
			}
		}

		// Token: 0x06004EB0 RID: 20144 RVA: 0x0017BA92 File Offset: 0x00179C92
		public void SetCost(int cost)
		{
			this.m_Cost = cost;
		}

		// Token: 0x06004EB1 RID: 20145 RVA: 0x0017BA9B File Offset: 0x00179C9B
		public void Invalid()
		{
			this.m_NKMGameData = null;
			this.m_NKMGameRuntimeData = null;
			this.m_NKMDungeonTempletBase = null;
		}

		// Token: 0x06004EB2 RID: 20146 RVA: 0x0017BAB4 File Offset: 0x00179CB4
		public void Update()
		{
			if (NKCScenManager.GetScenManager().Get_SCEN_GAME().Get_NKC_SCEN_STATE() != NKC_SCEN_STATE.NSS_START)
			{
				return;
			}
			if (this.m_NKMGameData == null || this.m_NKMGameRuntimeData == null || this.m_NKMDungeonTempletBase == null)
			{
				return;
			}
			if (this.m_NKMGameData.GetGameType() == NKM_GAME_TYPE.NGT_PRACTICE)
			{
				return;
			}
			this.ProcessMission(this.m_NKMDungeonTempletBase.m_DGMissionType_1, this.m_NKMDungeonTempletBase.m_DGMissionValue_1);
			this.ProcessMission(this.m_NKMDungeonTempletBase.m_DGMissionType_2, this.m_NKMDungeonTempletBase.m_DGMissionValue_2);
		}

		// Token: 0x06004EB3 RID: 20147 RVA: 0x0017BB38 File Offset: 0x00179D38
		private void ProcessMission(DUNGEON_GAME_MISSION_TYPE missionType, int value)
		{
			if (missionType == DUNGEON_GAME_MISSION_TYPE.DGMT_COST)
			{
				int num = (int)this.m_NKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_fUsedRespawnCost;
				if (num > this.m_Cost)
				{
					if (this.m_Cost < value && num >= value)
					{
						if (NKCScenManager.GetScenManager().GetGameClient().IsShowUI())
						{
							NKCPopupMessageManager.AddPopupMessage(string.Format(NKCUtilString.GET_STRING_DUNGEON_MISSION_COST_FAIL_ONE_PARAM, value), NKCPopupMessage.eMessagePosition.TopIngame, false, true, 0f, false);
						}
					}
					else if (this.m_Cost < (int)((float)value * 0.75f) && num >= (int)((float)value * 0.75f))
					{
						NKCPopupMessageManager.AddPopupMessage(string.Format(NKCUtilString.GET_STRING_DUNGEON_MISSION_COST_WARNING_THREE_PARAM, value, num, value), NKCPopupMessage.eMessagePosition.TopIngame, false, true, 0f, false);
					}
					else if (this.m_Cost < value / 2 && num >= value / 2 && NKCScenManager.GetScenManager().GetGameClient().IsShowUI())
					{
						NKCPopupMessageManager.AddPopupMessage(string.Format(NKCUtilString.GET_STRING_DUNGEON_MISSION_COST_THREE_PARAM, value, num, value), NKCPopupMessage.eMessagePosition.TopIngame, false, true, 0f, false);
					}
					this.m_Cost = num;
					return;
				}
			}
			else if (missionType == DUNGEON_GAME_MISSION_TYPE.DGMT_TIME)
			{
				int num2 = (int)this.m_NKMGameRuntimeData.GetGamePlayTime();
				if (num2 > this.m_GameTime)
				{
					if (this.m_GameTime < value && num2 >= value)
					{
						if (NKCScenManager.GetScenManager().GetGameClient().IsShowUI())
						{
							NKCPopupMessageManager.AddPopupMessage(string.Format(NKCUtilString.GET_STRING_DUNGEON_MISSION_TIME_FAIL_ONE_PARAM, value), NKCPopupMessage.eMessagePosition.TopIngame, false, true, 0f, false);
						}
					}
					else if (this.m_GameTime < (int)((float)value * 0.75f) && num2 >= (int)((float)value * 0.75f))
					{
						if (NKCScenManager.GetScenManager().GetGameClient().IsShowUI())
						{
							NKCPopupMessageManager.AddPopupMessage(string.Format(NKCUtilString.GET_STRING_DUNGEON_MISSION_TIME_WARNING_THREE_PARAM, value, num2, value), NKCPopupMessage.eMessagePosition.TopIngame, false, true, 0f, false);
						}
					}
					else if (this.m_GameTime < value / 2 && num2 >= value / 2 && NKCScenManager.GetScenManager().GetGameClient().IsShowUI())
					{
						NKCPopupMessageManager.AddPopupMessage(string.Format(NKCUtilString.GET_STRING_DUNGEON_MISSION_TIME_THREE_PARAM, value, num2, value), NKCPopupMessage.eMessagePosition.TopIngame, false, true, 0f, false);
					}
					this.m_GameTime = num2;
					return;
				}
			}
		}

		// Token: 0x04003E5E RID: 15966
		private NKMGameData m_NKMGameData;

		// Token: 0x04003E5F RID: 15967
		private NKMGameRuntimeData m_NKMGameRuntimeData;

		// Token: 0x04003E60 RID: 15968
		private NKMDungeonTempletBase m_NKMDungeonTempletBase;

		// Token: 0x04003E61 RID: 15969
		private int m_GameTime;

		// Token: 0x04003E62 RID: 15970
		private int m_Cost;

		// Token: 0x04003E63 RID: 15971
		private float m_fShipHP;

		// Token: 0x04003E64 RID: 15972
		private float m_fShipMaxHP;
	}
}
