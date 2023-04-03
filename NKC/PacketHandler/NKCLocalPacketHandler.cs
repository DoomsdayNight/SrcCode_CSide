using System;
using ClientPacket.Game;
using ClientPacket.Mode;
using ClientPacket.User;
using Cs.Protocol;
using NKM;
using Protocol;
using UnityEngine;

namespace NKC.PacketHandler
{
	// Token: 0x0200089C RID: 2204
	public class NKCLocalPacketHandler
	{
		// Token: 0x060057FA RID: 22522 RVA: 0x001A5D81 File Offset: 0x001A3F81
		public static void SendPacketToLocalServer(ISerializable cPacket_BASE)
		{
			NKCMessage.SendMessage(NKC_EVENT_MESSAGE.NEM_NKCPACKET_SEND_TO_SERVER, (int)PacketController.Instance.GetId(cPacket_BASE), cPacket_BASE, null, null, false, NKCLocalPacketHandler.GetMessageLatency());
		}

		// Token: 0x060057FB RID: 22523 RVA: 0x001A5D9D File Offset: 0x001A3F9D
		public static void SendPacketToClient(ISerializable cPacket_BASE)
		{
			NKCMessage.SendMessage(NKC_EVENT_MESSAGE.NEM_NKCPACKET_SEND_TO_CLIENT, (int)PacketController.Instance.GetId(cPacket_BASE), cPacket_BASE, null, null, false, NKCLocalPacketHandler.GetMessageLatency());
		}

		// Token: 0x060057FC RID: 22524 RVA: 0x001A5DB9 File Offset: 0x001A3FB9
		private static float GetMessageLatency()
		{
			return 0.01f;
		}

		// Token: 0x060057FD RID: 22525 RVA: 0x001A5DC0 File Offset: 0x001A3FC0
		public static bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			if (cNKCMessageData.m_NKC_EVENT_MESSAGE == NKC_EVENT_MESSAGE.NEM_NKCPACKET_SEND_TO_CLIENT)
			{
				ClientPacketId clientPacketId = (ClientPacketId)cNKCMessageData.m_MsgID2;
				if (clientPacketId <= ClientPacketId.kNKMPacket_GAME_DEV_RESPAWN_ACK)
				{
					if (clientPacketId == ClientPacketId.kNKMPacket_GAME_LOAD_ACK)
					{
						NKCLocalPacketHandler.OnRecv((NKMPacket_GAME_LOAD_ACK)cNKCMessageData.m_Param1);
						return true;
					}
					switch (clientPacketId)
					{
					case ClientPacketId.kNKMPacket_GAME_LOAD_COMPLETE_ACK:
						NKCLocalPacketHandler.OnRecv((NKMPacket_GAME_LOAD_COMPLETE_ACK)cNKCMessageData.m_Param1);
						return true;
					case ClientPacketId.kNKMPacket_GAME_START_NOT:
						NKCLocalPacketHandler.OnRecv((NKMPacket_GAME_START_NOT)cNKCMessageData.m_Param1);
						return true;
					case ClientPacketId.kNKMPacket_GAME_INTRUDE_START_NOT:
					case ClientPacketId.kNKMPacket_GAME_PAUSE_REQ:
					case ClientPacketId.kNKMPacket_GAME_CHECK_DIE_UNIT_REQ:
					case ClientPacketId.kNKMPacket_GAME_CHECK_DIE_UNIT_ACK:
					case ClientPacketId.kNKMPacket_GAME_RESPAWN_REQ:
					case ClientPacketId.kNKMPacket_GAME_SHIP_SKILL_REQ:
					case ClientPacketId.kNKMPacket_GAME_AUTO_RESPAWN_REQ:
						break;
					case ClientPacketId.kNKMPacket_GAME_END_NOT:
						NKCLocalPacketHandler.OnRecv((NKMPacket_GAME_END_NOT)cNKCMessageData.m_Param1);
						return true;
					case ClientPacketId.kNKMPacket_GAME_PAUSE_ACK:
						NKCLocalPacketHandler.OnRecv((NKMPacket_GAME_PAUSE_ACK)cNKCMessageData.m_Param1);
						return true;
					case ClientPacketId.kNKMPacket_GAME_RESPAWN_ACK:
						NKCLocalPacketHandler.OnRecv((NKMPacket_GAME_RESPAWN_ACK)cNKCMessageData.m_Param1);
						return true;
					case ClientPacketId.kNKMPacket_GAME_SHIP_SKILL_ACK:
						NKCLocalPacketHandler.OnRecv((NKMPacket_GAME_SHIP_SKILL_ACK)cNKCMessageData.m_Param1);
						return true;
					case ClientPacketId.kNKMPacket_GAME_AUTO_RESPAWN_ACK:
						NKCLocalPacketHandler.OnRecv((NKMPacket_GAME_AUTO_RESPAWN_ACK)cNKCMessageData.m_Param1);
						return true;
					case ClientPacketId.kNKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT:
						NKCLocalPacketHandler.OnRecv((NKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT)cNKCMessageData.m_Param1);
						return true;
					default:
						switch (clientPacketId)
						{
						case ClientPacketId.kNKMPacket_GAME_SPEED_2X_ACK:
							NKCLocalPacketHandler.OnRecv((NKMPacket_GAME_SPEED_2X_ACK)cNKCMessageData.m_Param1);
							return true;
						case ClientPacketId.kNKMPacket_GAME_AUTO_SKILL_CHANGE_ACK:
							NKCLocalPacketHandler.OnRecv((NKMPacket_GAME_AUTO_SKILL_CHANGE_ACK)cNKCMessageData.m_Param1);
							return true;
						case ClientPacketId.kNKMPacket_GAME_USE_UNIT_SKILL_ACK:
							NKCLocalPacketHandler.OnRecv((NKMPacket_GAME_USE_UNIT_SKILL_ACK)cNKCMessageData.m_Param1);
							return true;
						case ClientPacketId.kNKMPacket_GAME_DEV_COOL_TIME_RESET_ACK:
							NKCLocalPacketHandler.OnRecv((NKMPacket_GAME_DEV_COOL_TIME_RESET_ACK)cNKCMessageData.m_Param1);
							return true;
						case ClientPacketId.kNKMPacket_GAME_DEV_RESPAWN_ACK:
							NKCLocalPacketHandler.OnRecv((NKMPacket_GAME_DEV_RESPAWN_ACK)cNKCMessageData.m_Param1);
							return true;
						}
						break;
					}
				}
				else if (clientPacketId <= ClientPacketId.kNKMPacket_GAME_TACTICAL_COMMAND_ACK)
				{
					if (clientPacketId == ClientPacketId.kNKMPacket_GAME_UNIT_RETREAT_ACK)
					{
						NKCLocalPacketHandler.OnRecv((NKMPacket_GAME_UNIT_RETREAT_ACK)cNKCMessageData.m_Param1);
						return true;
					}
					if (clientPacketId == ClientPacketId.kNKMPacket_GAME_TACTICAL_COMMAND_ACK)
					{
						NKCLocalPacketHandler.OnRecv((NKMPacket_GAME_TACTICAL_COMMAND_ACK)cNKCMessageData.m_Param1);
						return true;
					}
				}
				else
				{
					if (clientPacketId == ClientPacketId.kNKMPacket_GAME_OPTION_CHANGE_ACK)
					{
						NKCLocalPacketHandler.OnRecv((NKMPacket_GAME_OPTION_CHANGE_ACK)cNKCMessageData.m_Param1);
						return true;
					}
					switch (clientPacketId)
					{
					case ClientPacketId.kNKMPacket_GAME_DEV_FRAME_MOVE_ACK:
						return true;
					case ClientPacketId.kNKMPacket_GAME_DEV_DECK_CHANGE_ACK:
						return true;
					case ClientPacketId.kNKMPacket_GAME_DEV_SHIP_CHANGE_ACK:
						return true;
					case ClientPacketId.kNKMPacket_GAME_DEV_MONSTER_AUTO_RESPAWN_ACK:
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060057FE RID: 22526 RVA: 0x001A600C File Offset: 0x001A420C
		public static void OnRecv(NKMPacket_GAME_LOAD_ACK cNKMPacket_GAME_LOAD_ACK)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_GAME_LOAD_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_LOGIN || NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME || cNKMPacket_GAME_LOAD_ACK.gameData.GetGameType() == NKM_GAME_TYPE.NGT_PRACTICE)
			{
				NKCResourceUtility.ClearResource();
				NKCScenManager.GetScenManager().GetGameClient().ClearResource();
				NKCScenManager.GetScenManager().GetGameClient().SetGameDataDummy(cNKMPacket_GAME_LOAD_ACK.gameData, false);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAME, true);
			}
		}

		// Token: 0x060057FF RID: 22527 RVA: 0x001A608C File Offset: 0x001A428C
		public static void OnRecv(NKMPacket_GAME_LOAD_COMPLETE_ACK cNKMPacket_GAME_LOAD_COMPLETE_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME)
			{
				NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_GAME_LOAD_COMPLETE_ACK);
			}
		}

		// Token: 0x06005800 RID: 22528 RVA: 0x001A60AB File Offset: 0x001A42AB
		public static void OnRecv(NKMPacket_GAME_START_NOT cNKMPacket_GAME_START_NOT)
		{
			NKMPopUpBox.CloseWaitBox();
			NKCScenManager.GetScenManager().GetGameClient().StartGame(false);
		}

		// Token: 0x06005801 RID: 22529 RVA: 0x001A60C2 File Offset: 0x001A42C2
		public static void OnRecv(NKMPacket_GAME_END_NOT cNKMPacket_GAME_END_NOT)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnLocalGameEndRecv(cNKMPacket_GAME_END_NOT);
		}

		// Token: 0x06005802 RID: 22530 RVA: 0x001A60E2 File Offset: 0x001A42E2
		public static void OnRecv(NKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT cNKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT);
			NKCPacketObjectPool.CloseObject(cNKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT);
		}

		// Token: 0x06005803 RID: 22531 RVA: 0x001A6108 File Offset: 0x001A4308
		public static void OnRecv(NKMPacket_GAME_RESPAWN_ACK cNKMPacket_GAME_RESPAWN_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_GAME_RESPAWN_ACK);
			NKCPacketObjectPool.CloseObject(cNKMPacket_GAME_RESPAWN_ACK);
		}

		// Token: 0x06005804 RID: 22532 RVA: 0x001A612E File Offset: 0x001A432E
		public static void OnRecv(NKMPacket_GAME_UNIT_RETREAT_ACK cNKMPacket_GAME_UNIT_RETREAT_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_GAME_UNIT_RETREAT_ACK);
			NKCPacketObjectPool.CloseObject(cNKMPacket_GAME_UNIT_RETREAT_ACK);
		}

		// Token: 0x06005805 RID: 22533 RVA: 0x001A6154 File Offset: 0x001A4354
		public static void OnRecv(NKMPacket_GAME_AUTO_RESPAWN_ACK cNKMPacket_GAME_AUTO_RESPAWN_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_GAME_AUTO_RESPAWN_ACK);
			NKCPacketObjectPool.CloseObject(cNKMPacket_GAME_AUTO_RESPAWN_ACK);
		}

		// Token: 0x06005806 RID: 22534 RVA: 0x001A617A File Offset: 0x001A437A
		public static void OnRecv(NKMPacket_GAME_OPTION_CHANGE_ACK cNKMPacket_GAME_OPTION_CHANGE_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_GAME_OPTION_CHANGE_ACK);
			NKCPacketObjectPool.CloseObject(cNKMPacket_GAME_OPTION_CHANGE_ACK);
		}

		// Token: 0x06005807 RID: 22535 RVA: 0x001A61A0 File Offset: 0x001A43A0
		public static void OnRecv(NKMPacket_GAME_DEV_RESPAWN_ACK cNKMPacket_GAME_DEV_RESPAWN_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_GAME_DEV_RESPAWN_ACK);
			NKCPacketObjectPool.CloseObject(cNKMPacket_GAME_DEV_RESPAWN_ACK);
		}

		// Token: 0x06005808 RID: 22536 RVA: 0x001A61C6 File Offset: 0x001A43C6
		public static void OnRecv(NKMPacket_GAME_SHIP_SKILL_ACK cNKMPacket_GAME_SHIP_SKILL_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_GAME_SHIP_SKILL_ACK);
			NKCPacketObjectPool.CloseObject(cNKMPacket_GAME_SHIP_SKILL_ACK);
		}

		// Token: 0x06005809 RID: 22537 RVA: 0x001A61EC File Offset: 0x001A43EC
		public static void OnRecv(NKMPacket_GAME_TACTICAL_COMMAND_ACK cNKMPacket_GAME_TACTICAL_COMMAND_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_GAME_TACTICAL_COMMAND_ACK);
			NKCPacketObjectPool.CloseObject(cNKMPacket_GAME_TACTICAL_COMMAND_ACK);
		}

		// Token: 0x0600580A RID: 22538 RVA: 0x001A6212 File Offset: 0x001A4412
		public static void OnRecv(NKMPacket_GAME_DEV_COOL_TIME_RESET_ACK cNKMPacket_GAME_DEV_COOL_TIME_RESET_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_GAME_DEV_COOL_TIME_RESET_ACK);
			NKCPacketObjectPool.CloseObject(cNKMPacket_GAME_DEV_COOL_TIME_RESET_ACK);
		}

		// Token: 0x0600580B RID: 22539 RVA: 0x001A6238 File Offset: 0x001A4438
		public static void OnRecv(NKMPacket_GAME_PAUSE_ACK cNKMPacket_GAME_PAUSE_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_GAME_PAUSE_ACK);
			NKCPacketObjectPool.CloseObject(cNKMPacket_GAME_PAUSE_ACK);
		}

		// Token: 0x0600580C RID: 22540 RVA: 0x001A625E File Offset: 0x001A445E
		public static void OnRecv(NKMPacket_GAME_SPEED_2X_ACK cNKMPacket_GAME_SPEED_2X_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_GAME_SPEED_2X_ACK);
			NKCPacketObjectPool.CloseObject(cNKMPacket_GAME_SPEED_2X_ACK);
		}

		// Token: 0x0600580D RID: 22541 RVA: 0x001A6284 File Offset: 0x001A4484
		public static void OnRecv(NKMPacket_GAME_AUTO_SKILL_CHANGE_ACK cNKMPacket_GAME_AUTO_SKILL_CHANGE_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_GAME_AUTO_SKILL_CHANGE_ACK);
			NKCPacketObjectPool.CloseObject(cNKMPacket_GAME_AUTO_SKILL_CHANGE_ACK);
		}

		// Token: 0x0600580E RID: 22542 RVA: 0x001A62AA File Offset: 0x001A44AA
		public static void OnRecv(NKMPacket_GAME_USE_UNIT_SKILL_ACK cNKMPacket_GAME_USE_UNIT_SKILL_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				return;
			}
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_GAME_USE_UNIT_SKILL_ACK);
			NKCPacketObjectPool.CloseObject(cNKMPacket_GAME_USE_UNIT_SKILL_ACK);
		}

		// Token: 0x0600580F RID: 22543 RVA: 0x001A62D0 File Offset: 0x001A44D0
		public static void OnRecv(NKMPacket_RESET_STAGE_PLAY_COUNT_ACK cNKMPacket_RESET_STAGE_PLAY_COUNT_ACK)
		{
			Debug.Log("OnRecv - NKMPacket_RESET_STAGE_PLAY_COUNT_ACK - NKCLocalPacketHandlersLobby");
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(cNKMPacket_RESET_STAGE_PLAY_COUNT_ACK.errorCode, true, null, -2147483648))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				nkmuserData.m_InventoryData.UpdateItemInfo(cNKMPacket_RESET_STAGE_PLAY_COUNT_ACK.costItemData);
				nkmuserData.UpdateStagePlayData(cNKMPacket_RESET_STAGE_PLAY_COUNT_ACK.stagePlayData);
			}
		}
	}
}
