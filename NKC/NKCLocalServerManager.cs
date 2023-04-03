using System;
using ClientPacket.Common;
using ClientPacket.Game;
using ClientPacket.User;
using Cs.Protocol;
using NKC.PacketHandler;
using NKM;
using NKM.Templet;
using Protocol;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000693 RID: 1683
	public class NKCLocalServerManager
	{
		// Token: 0x06003742 RID: 14146 RVA: 0x0011C917 File Offset: 0x0011AB17
		public static NKCGameServerLocal GetGameServerLocal()
		{
			return NKCLocalServerManager.m_NKCGameServerLocal;
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x0011C91E File Offset: 0x0011AB1E
		public static long GetGameUIDIndex()
		{
			long gameUIDIndex = NKCLocalServerManager.m_GameUIDIndex;
			NKCLocalServerManager.m_GameUIDIndex = gameUIDIndex + 1L;
			return gameUIDIndex;
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x0011C936 File Offset: 0x0011AB36
		public static void Update(float fDeltaTime)
		{
			if (NKCLocalServerManager.m_NKCGameServerLocal.GetGameData() == null)
			{
				return;
			}
			if (NKCLocalServerManager.m_NKCGameServerLocal.GetGameRuntimeData().m_NKM_GAME_STATE < NKM_GAME_STATE.NGS_START || NKCLocalServerManager.m_NKCGameServerLocal.GetGameRuntimeData().m_NKM_GAME_STATE > NKM_GAME_STATE.NGS_FINISH)
			{
				return;
			}
			NKCLocalServerManager.UpdateInner(Time.deltaTime);
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x0011C974 File Offset: 0x0011AB74
		public static void UpdateInner(float fDeltaTime)
		{
			switch (NKCLocalServerManager.m_NKCGameServerLocal.GetGameRuntimeData().m_NKM_GAME_SPEED_TYPE)
			{
			case NKM_GAME_SPEED_TYPE.NGST_1:
				NKCLocalServerManager.m_NKCGameServerLocal.Update(fDeltaTime * 1.1f);
				return;
			case NKM_GAME_SPEED_TYPE.NGST_2:
				NKCLocalServerManager.m_NKCGameServerLocal.Update(fDeltaTime * 1.5f);
				return;
			case NKM_GAME_SPEED_TYPE.NGST_3:
				NKCLocalServerManager.m_NKCGameServerLocal.Update(fDeltaTime * 1.1f);
				NKCLocalServerManager.m_NKCGameServerLocal.Update(fDeltaTime * 1.1f);
				return;
			case NKM_GAME_SPEED_TYPE.NGST_05:
				NKCLocalServerManager.m_NKCGameServerLocal.Update(fDeltaTime * 0.6f);
				return;
			case NKM_GAME_SPEED_TYPE.NGST_10:
				NKCLocalServerManager.UpdateInner(fDeltaTime * 1.1f);
				NKCLocalServerManager.UpdateInner(fDeltaTime * 1.1f);
				NKCLocalServerManager.UpdateInner(fDeltaTime * 1.1f);
				NKCLocalServerManager.UpdateInner(fDeltaTime * 1.1f);
				NKCLocalServerManager.UpdateInner(fDeltaTime * 1.1f);
				NKCLocalServerManager.UpdateInner(fDeltaTime * 1.1f);
				NKCLocalServerManager.UpdateInner(fDeltaTime * 1.1f);
				NKCLocalServerManager.UpdateInner(fDeltaTime * 1.1f);
				NKCLocalServerManager.UpdateInner(fDeltaTime * 1.1f);
				NKCLocalServerManager.UpdateInner(fDeltaTime * 1.1f);
				return;
			default:
				NKCLocalServerManager.m_NKCGameServerLocal.Update(fDeltaTime);
				return;
			}
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x0011CA90 File Offset: 0x0011AC90
		public static bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			if (cNKCMessageData.m_NKC_EVENT_MESSAGE == NKC_EVENT_MESSAGE.NEM_NKCPACKET_SEND_TO_SERVER)
			{
				ClientPacketId clientPacketId = (ClientPacketId)cNKCMessageData.m_MsgID2;
				if (clientPacketId <= ClientPacketId.kNKMPacket_GAME_AUTO_RESPAWN_REQ)
				{
					if (clientPacketId <= ClientPacketId.kNKMPacket_PRACTICE_GAME_LOAD_REQ)
					{
						if (clientPacketId == ClientPacketId.kNKMPacket_GAME_LOAD_REQ)
						{
							NKCLocalServerManager.OnRecv((NKMPacket_GAME_LOAD_REQ)cNKCMessageData.m_Param1);
							return true;
						}
						if (clientPacketId == ClientPacketId.kNKMPacket_PRACTICE_GAME_LOAD_REQ)
						{
							NKCLocalServerManager.OnRecv((NKMPacket_PRACTICE_GAME_LOAD_REQ)cNKCMessageData.m_Param1);
							return true;
						}
					}
					else
					{
						if (clientPacketId == ClientPacketId.kNKMPacket_GAME_LOAD_COMPLETE_REQ)
						{
							NKCLocalServerManager.OnRecv((NKMPacket_GAME_LOAD_COMPLETE_REQ)cNKCMessageData.m_Param1);
							return true;
						}
						if (clientPacketId == ClientPacketId.kNKMPacket_GAME_PAUSE_REQ)
						{
							NKCLocalServerManager.OnRecv((NKMPacket_GAME_PAUSE_REQ)cNKCMessageData.m_Param1);
							return true;
						}
						switch (clientPacketId)
						{
						case ClientPacketId.kNKMPacket_GAME_RESPAWN_REQ:
							NKCLocalServerManager.OnRecv((NKMPacket_GAME_RESPAWN_REQ)cNKCMessageData.m_Param1);
							return true;
						case ClientPacketId.kNKMPacket_GAME_SHIP_SKILL_REQ:
							NKCLocalServerManager.OnRecv((NKMPacket_GAME_SHIP_SKILL_REQ)cNKCMessageData.m_Param1);
							return true;
						case ClientPacketId.kNKMPacket_GAME_AUTO_RESPAWN_REQ:
							NKCLocalServerManager.OnRecv((NKMPacket_GAME_AUTO_RESPAWN_REQ)cNKCMessageData.m_Param1);
							return true;
						}
					}
				}
				else if (clientPacketId <= ClientPacketId.kNKMPacket_GAME_UNIT_RETREAT_REQ)
				{
					switch (clientPacketId)
					{
					case ClientPacketId.kNKMPacket_GAME_SPEED_2X_REQ:
						NKCLocalServerManager.OnRecv((NKMPacket_GAME_SPEED_2X_REQ)cNKCMessageData.m_Param1);
						return true;
					case ClientPacketId.kNKMPacket_GAME_SPEED_2X_ACK:
					case ClientPacketId.kNKMPacket_GAME_AUTO_SKILL_CHANGE_ACK:
					case ClientPacketId.kNKMPacket_GAME_USE_UNIT_SKILL_ACK:
					case ClientPacketId.kNKMPacket_GAME_DEV_COOL_TIME_RESET_ACK:
						break;
					case ClientPacketId.kNKMPacket_GAME_AUTO_SKILL_CHANGE_REQ:
						NKCLocalServerManager.OnRecv((NKMPacket_GAME_AUTO_SKILL_CHANGE_REQ)cNKCMessageData.m_Param1);
						return true;
					case ClientPacketId.kNKMPacket_GAME_USE_UNIT_SKILL_REQ:
						NKCLocalServerManager.OnRecv((NKMPacket_GAME_USE_UNIT_SKILL_REQ)cNKCMessageData.m_Param1);
						return true;
					case ClientPacketId.kNKMPacket_GAME_DEV_COOL_TIME_RESET_REQ:
						NKCLocalServerManager.OnRecv((NKMPacket_GAME_DEV_COOL_TIME_RESET_REQ)cNKCMessageData.m_Param1);
						return true;
					case ClientPacketId.kNKMPacket_GAME_DEV_RESPAWN_REQ:
						NKCLocalServerManager.OnRecv((NKMPacket_GAME_DEV_RESPAWN_REQ)cNKCMessageData.m_Param1);
						return true;
					default:
						if (clientPacketId == ClientPacketId.kNKMPacket_GAME_UNIT_RETREAT_REQ)
						{
							NKCLocalServerManager.OnRecv((NKMPacket_GAME_UNIT_RETREAT_REQ)cNKCMessageData.m_Param1);
							return true;
						}
						break;
					}
				}
				else
				{
					if (clientPacketId == ClientPacketId.kNKMPacket_GAME_TACTICAL_COMMAND_REQ)
					{
						NKCLocalServerManager.OnRecv((NKMPacket_GAME_TACTICAL_COMMAND_REQ)cNKCMessageData.m_Param1);
						return true;
					}
					if (clientPacketId == ClientPacketId.kNKMPacket_GAME_OPTION_CHANGE_REQ)
					{
						NKCLocalServerManager.OnRecv((NKMPacket_GAME_OPTION_CHANGE_REQ)cNKCMessageData.m_Param1);
						return true;
					}
					switch (clientPacketId)
					{
					case ClientPacketId.kNKMPacket_GAME_DEV_FRAME_MOVE_REQ:
						return true;
					case ClientPacketId.kNKMPacket_GAME_DEV_DECK_CHANGE_REQ:
						return true;
					case ClientPacketId.kNKMPacket_GAME_DEV_SHIP_CHANGE_REQ:
						return true;
					case ClientPacketId.kNKMPacket_GAME_DEV_MONSTER_AUTO_RESPAWN_REQ:
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003748 RID: 14152 RVA: 0x0011CCBC File Offset: 0x0011AEBC
		public static void OnRecv(NKMPacket_GAME_LOAD_REQ cNKMPacket_GAME_LOAD_REQ)
		{
			NKCLocalServerManager.MakeNewLocalGame();
			NKMPacket_GAME_LOAD_ACK nkmpacket_GAME_LOAD_ACK = new NKMPacket_GAME_LOAD_ACK();
			NKMGameData nkmgameData = new NKMGameData();
			nkmgameData.m_DungeonID = cNKMPacket_GAME_LOAD_REQ.dungeonID;
			nkmgameData.m_TeamASupply = 2;
			NKMGameRuntimeData gameRuntimeData = new NKMGameRuntimeData();
			if (cNKMPacket_GAME_LOAD_REQ.isDev)
			{
				nkmgameData = NKCLocalServerManager.MakeDevGameData(ref nkmgameData, ref gameRuntimeData);
				NKCLocalServerManager.m_NKCGameServerLocal.SetGameData(nkmgameData);
				NKCLocalServerManager.m_NKCGameServerLocal.SetGameRuntimeData(gameRuntimeData);
			}
			nkmpacket_GAME_LOAD_ACK.gameData = new NKMGameData();
			nkmpacket_GAME_LOAD_ACK.gameData.DeepCopyFrom(nkmgameData);
			nkmpacket_GAME_LOAD_ACK.errorCode = NKM_ERROR_CODE.NEC_OK;
			NKCLocalPacketHandler.SendPacketToClient(nkmpacket_GAME_LOAD_ACK);
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x0011CD40 File Offset: 0x0011AF40
		public static void OnRecv(NKMPacket_PRACTICE_GAME_LOAD_REQ cNKMPacket_PRACTICE_GAME_LOAD_REQ)
		{
			NKCLocalServerManager.MakeNewLocalGame();
			NKMPacket_GAME_LOAD_ACK nkmpacket_GAME_LOAD_ACK = new NKMPacket_GAME_LOAD_ACK();
			if (NKMUnitManager.GetUnitTemplet(cNKMPacket_PRACTICE_GAME_LOAD_REQ.practiceUnitData.m_UnitID) == null)
			{
				nkmpacket_GAME_LOAD_ACK.errorCode = NKM_ERROR_CODE.NEC_FAIL_PRACTICE_GAME_LOAD_REQ_INVALID_UNIT_TEMPLET;
				NKCLocalPacketHandler.SendPacketToClient(nkmpacket_GAME_LOAD_ACK);
				return;
			}
			NKMGameData nkmgameData = new NKMGameData();
			nkmgameData.m_DungeonID = NKMDungeonManager.GetDungeonID("NKM_BATTLE_PRACTICE");
			nkmgameData.m_TeamASupply = 2;
			NKMGameRuntimeData gameRuntimeData = new NKMGameRuntimeData();
			nkmgameData = NKCLocalServerManager.MakePracticeGameData(ref nkmgameData, ref gameRuntimeData, cNKMPacket_PRACTICE_GAME_LOAD_REQ.practiceUnitData);
			NKCLocalServerManager.m_NKCGameServerLocal.SetGameData(nkmgameData);
			NKCLocalServerManager.m_NKCGameServerLocal.SetGameRuntimeData(gameRuntimeData);
			nkmpacket_GAME_LOAD_ACK.gameData = new NKMGameData();
			nkmpacket_GAME_LOAD_ACK.gameData.DeepCopyFrom(nkmgameData);
			nkmpacket_GAME_LOAD_ACK.errorCode = NKM_ERROR_CODE.NEC_OK;
			NKCLocalPacketHandler.SendPacketToClient(nkmpacket_GAME_LOAD_ACK);
		}

		// Token: 0x0600374A RID: 14154 RVA: 0x0011CDE9 File Offset: 0x0011AFE9
		public static void MakeNewLocalGame()
		{
			NKCLocalServerManager.m_NKCGameServerLocal.EndGame();
			NKCLocalServerManager.m_NKCGameServerLocal.Init();
		}

		// Token: 0x0600374B RID: 14155 RVA: 0x0011CE00 File Offset: 0x0011B000
		public static NKMGameData MakeDevGameData(ref NKMGameData cNKMGameData, ref NKMGameRuntimeData cNKMGameRuntimeData)
		{
			cNKMGameData.m_GameUID = NKCLocalServerManager.GetGameUIDIndex();
			cNKMGameData.m_bLocal = true;
			cNKMGameData.m_NKM_GAME_TYPE = NKM_GAME_TYPE.NGT_DEV;
			cNKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_bAutoRespawn = false;
			NKMUnitData nkmunitData = new NKMUnitData();
			nkmunitData.m_UnitUID = NpcUid.Get();
			nkmunitData.m_UnitID = 21001;
			cNKMGameData.m_NKMGameTeamDataA.m_MainShip = nkmunitData;
			cNKMGameData.m_NKMGameTeamDataA.m_Operator = NKCLocalServerManager.s_NKMOperatorTeamA_ForDev;
			nkmunitData = new NKMUnitData();
			nkmunitData.m_UnitUID = NpcUid.Get();
			nkmunitData.m_UnitID = 1001;
			cNKMGameData.m_NKMGameTeamDataA.m_listAssistUnitData.Add(nkmunitData);
			nkmunitData = new NKMUnitData();
			nkmunitData.m_UnitUID = NpcUid.Get();
			nkmunitData.m_UnitID = 1003;
			cNKMGameData.m_NKMGameTeamDataA.m_listAssistUnitData.Add(nkmunitData);
			NKMDungeonManager.MakeGameTeamData(cNKMGameData, cNKMGameRuntimeData);
			NKMBanShipData nkmbanShipData = new NKMBanShipData();
			nkmbanShipData.m_ShipGroupID = 20001;
			nkmbanShipData.m_BanLevel = 2;
			cNKMGameData.m_dicNKMBanShipData.Add(nkmbanShipData.m_ShipGroupID, nkmbanShipData);
			NKMUnitUpData nkmunitUpData = new NKMUnitUpData();
			nkmunitUpData.unitId = 1001;
			nkmunitUpData.upLevel = 2;
			cNKMGameData.m_dicNKMUpData.Add(nkmunitUpData.unitId, nkmunitUpData);
			return cNKMGameData;
		}

		// Token: 0x0600374C RID: 14156 RVA: 0x0011CF30 File Offset: 0x0011B130
		public static NKMGameData MakePracticeGameData(ref NKMGameData cNKMGameData, ref NKMGameRuntimeData cNKMGameRuntimeData, NKMUnitData practiceUnitData)
		{
			cNKMGameData.m_GameUID = NKCLocalServerManager.GetGameUIDIndex();
			cNKMGameData.m_bLocal = true;
			cNKMGameData.m_NKM_GAME_TYPE = NKM_GAME_TYPE.NGT_PRACTICE;
			cNKMGameRuntimeData.m_NKMGameRuntimeTeamDataA.m_bAutoRespawn = false;
			cNKMGameRuntimeData.m_NKMGameRuntimeTeamDataB.m_bAutoRespawn = false;
			NKMUnitData nkmunitData = new NKMUnitData();
			nkmunitData.DeepCopyFrom(practiceUnitData);
			nkmunitData.m_UnitUID = NpcUid.Get();
			for (int i = 0; i < 4; i++)
			{
				long equipUid = nkmunitData.GetEquipUid((ITEM_EQUIP_POSITION)i);
				if (equipUid != 0L)
				{
					NKMEquipItemData itemEquip = NKCScenManager.CurrentUserData().m_InventoryData.GetItemEquip(equipUid);
					if (itemEquip != null)
					{
						NKMEquipItemData nkmequipItemData = new NKMEquipItemData();
						nkmequipItemData.DeepCopyFrom(itemEquip);
						cNKMGameData.m_NKMGameTeamDataA.m_ItemEquipData.Add(equipUid, nkmequipItemData);
					}
				}
			}
			NKMUnitTemplet unitTemplet = NKMUnitManager.GetUnitTemplet(practiceUnitData.m_UnitID);
			if (unitTemplet != null && unitTemplet.m_UnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
			{
				cNKMGameData.m_NKMGameTeamDataA.m_MainShip = nkmunitData;
			}
			else
			{
				cNKMGameData.m_NKMGameTeamDataA.m_DeckData.SetListUnitDeck(0, nkmunitData.m_UnitUID);
				cNKMGameData.m_NKMGameTeamDataA.m_listUnitData.Add(nkmunitData);
			}
			cNKMGameData.m_NKMGameTeamDataA.m_LeaderUnitUID = nkmunitData.m_UnitUID;
			NKMDungeonManager.MakeGameTeamData(cNKMGameData, cNKMGameRuntimeData);
			return cNKMGameData;
		}

		// Token: 0x0600374D RID: 14157 RVA: 0x0011D051 File Offset: 0x0011B251
		public static void LocalGameUnitAllKill(bool bEnemy = false)
		{
			if (NKCLocalServerManager.m_NKCGameServerLocal == null)
			{
				return;
			}
			if (!bEnemy)
			{
				NKCLocalServerManager.GetGameServerLocal().AllKill(NKM_TEAM_TYPE.NTT_A1);
				NKCLocalServerManager.GetGameServerLocal().AllKill(NKM_TEAM_TYPE.NTT_A2);
				return;
			}
			NKCLocalServerManager.GetGameServerLocal().AllKill(NKM_TEAM_TYPE.NTT_B1);
			NKCLocalServerManager.GetGameServerLocal().AllKill(NKM_TEAM_TYPE.NTT_B2);
		}

		// Token: 0x0600374E RID: 14158 RVA: 0x0011D08B File Offset: 0x0011B28B
		public static void OnRecv(NKMPacket_GAME_LOAD_COMPLETE_REQ cNKMPacket_GAME_LOAD_COMPLETE_REQ)
		{
			NKCLocalPacketHandler.SendPacketToClient(new NKMPacket_GAME_LOAD_COMPLETE_ACK
			{
				gameRuntimeData = NKCLocalServerManager.m_NKCGameServerLocal.GetGameRuntimeData().DeepCopy<NKMGameRuntimeData>()
			});
			NKCLocalServerManager.m_NKCGameServerLocal.StartGame(false);
		}

		// Token: 0x0600374F RID: 14159 RVA: 0x0011D0B8 File Offset: 0x0011B2B8
		public static void OnRecv(NKMPacket_GAME_RESPAWN_REQ cNKMPacket_GAME_RESPAWN_REQ)
		{
			NKMPacket_GAME_RESPAWN_ACK nkmpacket_GAME_RESPAWN_ACK = new NKMPacket_GAME_RESPAWN_ACK();
			nkmpacket_GAME_RESPAWN_ACK.unitUID = cNKMPacket_GAME_RESPAWN_REQ.unitUID;
			nkmpacket_GAME_RESPAWN_ACK.assistUnit = cNKMPacket_GAME_RESPAWN_REQ.assistUnit;
			nkmpacket_GAME_RESPAWN_ACK.errorCode = NKCLocalServerManager.m_NKCGameServerLocal.OnRecv(cNKMPacket_GAME_RESPAWN_REQ, ref nkmpacket_GAME_RESPAWN_ACK.unitUID);
			if (nkmpacket_GAME_RESPAWN_ACK.unitUID <= 0L)
			{
				nkmpacket_GAME_RESPAWN_ACK.unitUID = cNKMPacket_GAME_RESPAWN_REQ.unitUID;
			}
			NKCLocalPacketHandler.SendPacketToClient(nkmpacket_GAME_RESPAWN_ACK);
		}

		// Token: 0x06003750 RID: 14160 RVA: 0x0011D116 File Offset: 0x0011B316
		public static void OnRecv(NKMPacket_GAME_UNIT_RETREAT_REQ cNKMPacket_GAME_UNIT_RETREAT_REQ)
		{
			NKCLocalPacketHandler.SendPacketToClient(new NKMPacket_GAME_UNIT_RETREAT_ACK
			{
				unitUID = cNKMPacket_GAME_UNIT_RETREAT_REQ.unitUID,
				errorCode = NKCLocalServerManager.m_NKCGameServerLocal.OnRecv(cNKMPacket_GAME_UNIT_RETREAT_REQ)
			});
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x0011D140 File Offset: 0x0011B340
		public static void OnRecv(NKMPacket_GAME_SHIP_SKILL_REQ cNKMPacket_GAME_SHIP_SKILL_REQ)
		{
			NKMPacket_GAME_SHIP_SKILL_ACK nkmpacket_GAME_SHIP_SKILL_ACK = new NKMPacket_GAME_SHIP_SKILL_ACK();
			nkmpacket_GAME_SHIP_SKILL_ACK.shipSkillID = cNKMPacket_GAME_SHIP_SKILL_REQ.shipSkillID;
			nkmpacket_GAME_SHIP_SKILL_ACK.skillPosX = cNKMPacket_GAME_SHIP_SKILL_REQ.skillPosX;
			nkmpacket_GAME_SHIP_SKILL_ACK.errorCode = NKCLocalServerManager.m_NKCGameServerLocal.OnRecv(cNKMPacket_GAME_SHIP_SKILL_REQ, nkmpacket_GAME_SHIP_SKILL_ACK);
			NKCLocalPacketHandler.SendPacketToClient(nkmpacket_GAME_SHIP_SKILL_ACK);
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x0011D184 File Offset: 0x0011B384
		public static void OnRecv(NKMPacket_GAME_TACTICAL_COMMAND_REQ cNKMPacket_GAME_TACTICAL_COMMAND_REQ)
		{
			NKMPacket_GAME_TACTICAL_COMMAND_ACK nkmpacket_GAME_TACTICAL_COMMAND_ACK = new NKMPacket_GAME_TACTICAL_COMMAND_ACK();
			nkmpacket_GAME_TACTICAL_COMMAND_ACK.cTacticalCommandData = new NKMTacticalCommandData();
			nkmpacket_GAME_TACTICAL_COMMAND_ACK.errorCode = NKCLocalServerManager.m_NKCGameServerLocal.OnRecv(cNKMPacket_GAME_TACTICAL_COMMAND_REQ, nkmpacket_GAME_TACTICAL_COMMAND_ACK);
			NKCLocalPacketHandler.SendPacketToClient(nkmpacket_GAME_TACTICAL_COMMAND_ACK);
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x0011D1BC File Offset: 0x0011B3BC
		public static void OnRecv(NKMPacket_GAME_AUTO_RESPAWN_REQ cNKMPacket_GAME_AUTO_RESPAWN_REQ)
		{
			NKMPacket_GAME_AUTO_RESPAWN_ACK nkmpacket_GAME_AUTO_RESPAWN_ACK = new NKMPacket_GAME_AUTO_RESPAWN_ACK();
			nkmpacket_GAME_AUTO_RESPAWN_ACK.errorCode = NKCLocalServerManager.m_NKCGameServerLocal.OnRecv(cNKMPacket_GAME_AUTO_RESPAWN_REQ);
			if (nkmpacket_GAME_AUTO_RESPAWN_ACK.errorCode == NKM_ERROR_CODE.NEC_OK)
			{
				nkmpacket_GAME_AUTO_RESPAWN_ACK.isAutoRespawn = cNKMPacket_GAME_AUTO_RESPAWN_REQ.isAutoRespawn;
			}
			NKCLocalPacketHandler.SendPacketToClient(nkmpacket_GAME_AUTO_RESPAWN_ACK);
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x0011D1FA File Offset: 0x0011B3FA
		public static void OnRecv(NKMPacket_GAME_OPTION_CHANGE_REQ cNKMPacket_GAME_OPTION_CHANGE_REQ)
		{
			NKCLocalPacketHandler.SendPacketToClient(new NKMPacket_GAME_OPTION_CHANGE_ACK
			{
				isActionCamera = cNKMPacket_GAME_OPTION_CHANGE_REQ.isActionCamera,
				isTrackCamera = cNKMPacket_GAME_OPTION_CHANGE_REQ.isTrackCamera,
				isViewSkillCutIn = cNKMPacket_GAME_OPTION_CHANGE_REQ.isViewSkillCutIn,
				errorCode = NKM_ERROR_CODE.NEC_OK
			});
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x0011D231 File Offset: 0x0011B431
		public static void OnRecv(NKMPacket_GAME_DEV_RESPAWN_REQ cNKMPacket_GAME_DEV_RESPAWN_REQ)
		{
			NKCLocalServerManager.m_NKCGameServerLocal.OnRecv(cNKMPacket_GAME_DEV_RESPAWN_REQ);
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x0011D23E File Offset: 0x0011B43E
		public static void OnRecv(NKMPacket_GAME_DEV_COOL_TIME_RESET_REQ cNKMPacket_GAME_DEV_COOL_TIME_RESET_REQ)
		{
			NKCLocalPacketHandler.SendPacketToClient(new NKMPacket_GAME_DEV_COOL_TIME_RESET_ACK
			{
				isSkill = cNKMPacket_GAME_DEV_COOL_TIME_RESET_REQ.isSkill,
				teamType = cNKMPacket_GAME_DEV_COOL_TIME_RESET_REQ.teamType,
				errorCode = NKCLocalServerManager.m_NKCGameServerLocal.OnRecv(cNKMPacket_GAME_DEV_COOL_TIME_RESET_REQ)
			});
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x0011D273 File Offset: 0x0011B473
		public static void OnRecv(NKMPacket_GAME_PAUSE_REQ cNKMPacket_GAME_PAUSE_REQ)
		{
			NKCLocalPacketHandler.SendPacketToClient(new NKMPacket_GAME_PAUSE_ACK
			{
				isPause = cNKMPacket_GAME_PAUSE_REQ.isPause,
				isPauseEvent = cNKMPacket_GAME_PAUSE_REQ.isPauseEvent,
				errorCode = NKCLocalServerManager.m_NKCGameServerLocal.OnRecv(cNKMPacket_GAME_PAUSE_REQ)
			});
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x0011D2A8 File Offset: 0x0011B4A8
		public static void OnRecv(NKMPacket_GAME_SPEED_2X_REQ cNKMPacket_GAME_SPEED_2X_REQ)
		{
			NKCLocalPacketHandler.SendPacketToClient(new NKMPacket_GAME_SPEED_2X_ACK
			{
				errorCode = NKCLocalServerManager.m_NKCGameServerLocal.OnRecv(cNKMPacket_GAME_SPEED_2X_REQ, NKCScenManager.GetScenManager().GetMyUserData()),
				gameSpeedType = NKCLocalServerManager.m_NKCGameServerLocal.GetGameRuntimeData().m_NKM_GAME_SPEED_TYPE
			});
		}

		// Token: 0x06003759 RID: 14169 RVA: 0x0011D2E4 File Offset: 0x0011B4E4
		public static void OnRecv(NKMPacket_GAME_AUTO_SKILL_CHANGE_REQ cNKMPacket_GAME_AUTO_SKILL_CHANGE_REQ)
		{
			NKCLocalPacketHandler.SendPacketToClient(new NKMPacket_GAME_AUTO_SKILL_CHANGE_ACK
			{
				errorCode = NKCLocalServerManager.m_NKCGameServerLocal.OnRecv(cNKMPacket_GAME_AUTO_SKILL_CHANGE_REQ, NKM_TEAM_TYPE.NTT_A1, NKCScenManager.GetScenManager().GetMyUserData()),
				gameAutoSkillType = NKCLocalServerManager.m_NKCGameServerLocal.GetGameRuntimeData().GetMyRuntimeTeamData(NKM_TEAM_TYPE.NTT_A1).m_NKM_GAME_AUTO_SKILL_TYPE
			});
		}

		// Token: 0x0600375A RID: 14170 RVA: 0x0011D334 File Offset: 0x0011B534
		public static void OnRecv(NKMPacket_GAME_USE_UNIT_SKILL_REQ cNKMPacket_GAME_USE_UNIT_SKILL_REQ)
		{
			NKMPacket_GAME_USE_UNIT_SKILL_ACK nkmpacket_GAME_USE_UNIT_SKILL_ACK = new NKMPacket_GAME_USE_UNIT_SKILL_ACK();
			byte b = 0;
			nkmpacket_GAME_USE_UNIT_SKILL_ACK.errorCode = NKCLocalServerManager.m_NKCGameServerLocal.OnRecv(cNKMPacket_GAME_USE_UNIT_SKILL_REQ, NKM_TEAM_TYPE.NTT_A1, out b, NKCScenManager.GetScenManager().GetMyUserData());
			nkmpacket_GAME_USE_UNIT_SKILL_ACK.gameUnitUID = cNKMPacket_GAME_USE_UNIT_SKILL_REQ.gameUnitUID;
			nkmpacket_GAME_USE_UNIT_SKILL_ACK.skillStateID = (sbyte)b;
			NKCLocalPacketHandler.SendPacketToClient(nkmpacket_GAME_USE_UNIT_SKILL_ACK);
		}

		// Token: 0x04003415 RID: 13333
		private static NKCGameServerLocal m_NKCGameServerLocal = new NKCGameServerLocal();

		// Token: 0x04003416 RID: 13334
		private static long m_GameUIDIndex = 0L;

		// Token: 0x04003417 RID: 13335
		public static NKMOperator s_NKMOperatorTeamA_ForDev = null;
	}
}
