using System;
using System.Collections.Generic;
using ClientPacket.Game;
using NKC.PacketHandler;
using NKM;

namespace NKC
{
	// Token: 0x02000687 RID: 1671
	public class NKCGameServerLocal : NKMGameServerHost
	{
		// Token: 0x0600363E RID: 13886 RVA: 0x00118387 File Offset: 0x00116587
		public NKCGameServerLocal()
		{
			this.m_NKM_GAME_CLASS_TYPE = NKM_GAME_CLASS_TYPE.NGCT_GAME_SERVER_LOCAL;
		}

		// Token: 0x0600363F RID: 13887 RVA: 0x00118398 File Offset: 0x00116598
		public override void ProcessGameState()
		{
			if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_START && this.m_fPlayWaitTime > 0f)
			{
				this.m_fPlayWaitTime -= this.m_fDeltaTime;
				if (this.m_fPlayWaitTime <= 0f)
				{
					this.m_fPlayWaitTime = 0f;
					this.SetGameState(NKM_GAME_STATE.NGS_PLAY);
					if (base.GetDungeonType() == NKM_DUNGEON_TYPE.NDT_WAVE)
					{
						this.m_NKMGameRuntimeData.m_WaveID = 1;
					}
					base.SyncGameStateChange(this.m_NKMGameRuntimeData.m_NKM_GAME_STATE, this.m_NKMGameRuntimeData.m_WinTeam, this.m_NKMGameRuntimeData.m_WaveID);
				}
			}
			if (this.m_NKMGameRuntimeData.m_NKM_GAME_STATE == NKM_GAME_STATE.NGS_FINISH)
			{
				if (!this.m_NKMGameRuntimeData.m_bGameEnded)
				{
					this.GameEndFlush();
					this.m_NKMGameRuntimeData.m_bGameEnded = true;
				}
				if (this.m_fFinishWaitTime > 0f)
				{
					this.m_fFinishWaitTime -= this.m_fDeltaTime;
					if (this.m_fFinishWaitTime <= 0f)
					{
						this.m_fFinishWaitTime = 0f;
						this.SetGameState(NKM_GAME_STATE.NGS_END);
					}
				}
			}
		}

		// Token: 0x06003640 RID: 13888 RVA: 0x0011849C File Offset: 0x0011669C
		public override void SetGameData(NKMGameData cNKMGameData)
		{
			base.SetGameData(cNKMGameData);
		}

		// Token: 0x06003641 RID: 13889 RVA: 0x001184A5 File Offset: 0x001166A5
		public override void StartGame(bool bIntrude)
		{
			base.StartGame(bIntrude);
			NKCLocalPacketHandler.SendPacketToClient(new NKMPacket_GAME_START_NOT());
			this.SetGameState(NKM_GAME_STATE.NGS_START);
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x001184C0 File Offset: 0x001166C0
		public override void Update(float deltaTime)
		{
			if (this.m_NKMGameRuntimeData.m_bPause)
			{
				return;
			}
			base.Update(deltaTime);
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x001184D7 File Offset: 0x001166D7
		public override void SendSyncDataPackFlush(NKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT cPacket_NPT_GAME_SYNC_DATA_PACK_NOT)
		{
			NKCLocalPacketHandler.SendPacketToClient(cPacket_NPT_GAME_SYNC_DATA_PACK_NOT);
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x001184DF File Offset: 0x001166DF
		public void GameEndFlush()
		{
			NKCLocalPacketHandler.SendPacketToClient(new NKMPacket_GAME_END_NOT());
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x001184EB File Offset: 0x001166EB
		public override NKM_ERROR_CODE OnRecv(NKMPacket_GAME_PAUSE_REQ cNKMPacket_GAME_PAUSE_REQ)
		{
			return base.OnRecv(cNKMPacket_GAME_PAUSE_REQ);
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x001184F4 File Offset: 0x001166F4
		public override NKM_ERROR_CODE OnRecv(NKMPacket_GAME_DEV_COOL_TIME_RESET_REQ cNKMPacket_GAME_DEV_COOL_TIME_RESET_REQ)
		{
			return base.OnRecv(cNKMPacket_GAME_DEV_COOL_TIME_RESET_REQ);
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x00118500 File Offset: 0x00116700
		public void OnRecv(NKMPacket_GAME_DEV_RESPAWN_REQ cNKMPacket_GAME_DEV_RESPAWN_REQ)
		{
			NKMPacket_GAME_DEV_RESPAWN_ACK nkmpacket_GAME_DEV_RESPAWN_ACK = new NKMPacket_GAME_DEV_RESPAWN_ACK();
			nkmpacket_GAME_DEV_RESPAWN_ACK.errorCode = base.OnRecv(cNKMPacket_GAME_DEV_RESPAWN_REQ, ref nkmpacket_GAME_DEV_RESPAWN_ACK, cNKMPacket_GAME_DEV_RESPAWN_REQ.teamType);
			NKCLocalPacketHandler.SendPacketToClient(nkmpacket_GAME_DEV_RESPAWN_ACK);
		}

		// Token: 0x06003648 RID: 13896 RVA: 0x0011852E File Offset: 0x0011672E
		public NKM_ERROR_CODE OnRecv(NKMPacket_GAME_RESPAWN_REQ cPacket_GAME_RESPAWN_REQ, ref long respawnUnitUID)
		{
			return base.OnRecv(cPacket_GAME_RESPAWN_REQ, NKM_TEAM_TYPE.NTT_A1, ref respawnUnitUID);
		}

		// Token: 0x06003649 RID: 13897 RVA: 0x00118539 File Offset: 0x00116739
		public NKM_ERROR_CODE OnRecv(NKMPacket_GAME_UNIT_RETREAT_REQ cNKMPacket_GAME_UNIT_RETREAT_REQ)
		{
			return base.OnRecv(cNKMPacket_GAME_UNIT_RETREAT_REQ, NKM_TEAM_TYPE.NTT_A1);
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x00118543 File Offset: 0x00116743
		public NKM_ERROR_CODE OnRecv(NKMPacket_GAME_SHIP_SKILL_REQ cNKMPacket_GAME_SHIP_SKILL_REQ, NKMPacket_GAME_SHIP_SKILL_ACK cNKMPacket_GAME_SHIP_SKILL_ACK)
		{
			return base.OnRecv(cNKMPacket_GAME_SHIP_SKILL_REQ, NKM_TEAM_TYPE.NTT_A1);
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x0011854D File Offset: 0x0011674D
		public NKM_ERROR_CODE OnRecv(NKMPacket_GAME_TACTICAL_COMMAND_REQ cNKMPacket_GAME_TACTICAL_COMMAND_REQ, NKMPacket_GAME_TACTICAL_COMMAND_ACK cNKMPacket_GAME_TACTICAL_COMMAND_ACK)
		{
			return base.OnRecv(cNKMPacket_GAME_TACTICAL_COMMAND_REQ, cNKMPacket_GAME_TACTICAL_COMMAND_ACK, NKM_TEAM_TYPE.NTT_A1);
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x00118558 File Offset: 0x00116758
		public NKM_ERROR_CODE OnRecv(NKMPacket_GAME_AUTO_RESPAWN_REQ cPacket_GAME_AUTO_RESPAWN_REQ)
		{
			return base.OnRecv(cPacket_GAME_AUTO_RESPAWN_REQ, NKM_TEAM_TYPE.NTT_A1, NKCScenManager.GetScenManager().GetMyUserData());
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x0011856C File Offset: 0x0011676C
		private void SetUnitDie(NKMUnitData cNKMUnitData)
		{
			if (cNKMUnitData == null)
			{
				return;
			}
			List<short> listGameUnitUID = cNKMUnitData.m_listGameUnitUID;
			for (int i = 0; i < listGameUnitUID.Count; i++)
			{
				NKMUnit unit = this.GetUnit(listGameUnitUID[i], true, false);
				if (unit != null)
				{
					unit.GetUnitSyncData().SetHP(0f);
				}
			}
		}
	}
}
